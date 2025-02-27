﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqToDB.Linq.Builder
{
	using Common;
	using LinqToDB.Expressions;
	using Reflection;
	using SqlQuery;

	using static LinqToDB.Reflection.Methods.LinqToDB.Merge;

	internal sealed partial class MergeBuilder : MethodCallBuilder
	{
		static readonly MethodInfo[] _supportedMethods =
		{
			ExecuteMergeMethodInfo,
			MergeWithOutput,
			MergeWithOutputSource,
			MergeWithOutputInto,
			MergeWithOutputIntoSource
		};

		protected override bool CanBuildMethodCall(ExpressionBuilder builder, MethodCallExpression methodCall, BuildInfo buildInfo)
		{
			return methodCall.IsSameGenericMethod(_supportedMethods);
		}

		enum MergeKind
		{
			Merge,
			MergeWithOutput,
			MergeWithOutputSource,
			MergeWithOutputInto,
			MergeWithOutputIntoSource
		}

		protected override IBuildContext BuildMethodCall(ExpressionBuilder builder, MethodCallExpression methodCall, BuildInfo buildInfo)
		{
			var mergeContext = (MergeContext)builder.BuildSequence(new BuildInfo(buildInfo, methodCall.Arguments[0]));

			var kind =
				methodCall.IsSameGenericMethod(MergeWithOutput)           ? MergeKind.MergeWithOutput :
				methodCall.IsSameGenericMethod(MergeWithOutputSource)     ? MergeKind.MergeWithOutputSource :
				methodCall.IsSameGenericMethod(MergeWithOutputInto)       ? MergeKind.MergeWithOutputInto :
				methodCall.IsSameGenericMethod(MergeWithOutputIntoSource) ? MergeKind.MergeWithOutputIntoSource :
				                                                            MergeKind.Merge;

			if (kind is not MergeKind.Merge)
			{
				var args          = methodCall.Method.GetGenericArguments();
				var objectType    = args[0];
				var ed            = builder.MappingSchema.GetEntityDescriptor(objectType, builder.DataOptions.ConnectionOptions.OnEntityDescriptorCreated);
				var actionField   = SqlField.FakeField(new DbDataType(typeof(string)), "$action", false);
				var insertedTable = SqlTable.Inserted(ed);
				var deletedTable  = SqlTable.Deleted (ed);

				mergeContext.Merge.Output = new SqlOutputClause()
				{
					InsertedTable = insertedTable,
					DeletedTable  = deletedTable,
				};

				var selectQuery = new SelectQuery();

				var actionFieldContext   = new SingleExpressionContext(null, builder, actionField, selectQuery);
				var deletedTableContext  = new TableBuilder.TableContext(builder, selectQuery, deletedTable);
				var insertedTableContext = new TableBuilder.TableContext(builder, selectQuery, insertedTable);

				IBuildContext? sourceTableContext = null;

				if (kind is MergeKind.MergeWithOutputSource or MergeKind.MergeWithOutputIntoSource)
					sourceTableContext = mergeContext.SourceContext;

				if (kind is MergeKind.MergeWithOutput or MergeKind.MergeWithOutputSource)
				{
					var outputExpression = (LambdaExpression)methodCall.Arguments[1].Unwrap();

					var outputContext = new MergeOutputContext(
						buildInfo.Parent,
						outputExpression,
						mergeContext,
						actionFieldContext,
						deletedTableContext,
						insertedTableContext,
						sourceTableContext
					);

					return outputContext;
				}
				else
				{
					var outputExpression = (LambdaExpression)methodCall.Arguments[2].Unwrap();

					var outputTable = methodCall.Arguments[1];
					var destination = builder.BuildSequence(new BuildInfo(buildInfo, outputTable, new SelectQuery()));

					UpdateBuilder.BuildSetterWithContext(
						builder,
						buildInfo,
						outputExpression,
						destination,
						mergeContext.Merge.Output.OutputItems,
						sourceTableContext is null ?
							new IBuildContext[] { actionFieldContext, deletedTableContext, insertedTableContext } :
							new IBuildContext[] { actionFieldContext, deletedTableContext, insertedTableContext, sourceTableContext }
					);

					mergeContext.Merge.Output.OutputTable = ((TableBuilder.TableContext)destination).SqlTable;
				}
			}

			return mergeContext;
		}

		sealed class MergeOutputContext : SelectContext
		{
			public MergeOutputContext(
				IBuildContext?   parent,
				LambdaExpression lambda,
				MergeContext     mergeContext,
				IBuildContext    emptyTable,
				IBuildContext    deletedTable,
				IBuildContext    insertedTable,
				IBuildContext?   sourceTable)
				: base(parent, lambda,
					sourceTable is not null?
						new[] { emptyTable, deletedTable, insertedTable, sourceTable } :
						new[] { emptyTable, deletedTable, insertedTable })
			{
				Statement = mergeContext.Statement;
				Sequence[0].SelectQuery.Select.Columns.Clear();
				Sequence[1].SelectQuery = Sequence[0].SelectQuery;
				Sequence[2].SelectQuery = Sequence[0].SelectQuery;
			}

			public override void BuildQuery<T>(Query<T> query, ParameterExpression queryParameter)
			{
				var expr   = BuildExpression(null, 0, false);
				var mapper = Builder.BuildMapper<T>(expr);

				var mergeStatement = (SqlMergeStatement)Statement!;

				mergeStatement.Output!.OutputColumns = Sequence[0].SelectQuery.Select.Columns.Select(c => c.Expression).ToList();

				QueryRunner.SetRunQuery(query, mapper);
			}
		}

		private static SelectQuery RemoveContextFromQuery(TableBuilder.TableContext tableContext, SelectQuery query)
		{
			var clonedTableSource = tableContext.SelectQuery.From.Tables[0];
			while (clonedTableSource.Joins.Count > 0)
			{
				var join = clonedTableSource.Joins[0];
				tableContext.SelectQuery.From.Tables.Add(join.Table);
				clonedTableSource.Joins.RemoveAt(0);
			}

			tableContext.SelectQuery.From.Tables.RemoveAt(0);
			query.Visit(query, static (query, e) =>
			{
				if (e is SelectQuery selectQuery && selectQuery.From.Tables.Count > 0)
				{
					if (selectQuery.From.Tables[0].Source is SelectQuery subSelect)
					{
						if (subSelect.From.Tables.Count == 0)
						{
							if (!subSelect.Where.IsEmpty)
							{
								selectQuery.Where.ConcatSearchCondition(subSelect.Where.SearchCondition);
							}

							selectQuery.From.Tables.RemoveAt(0);

							query.Walk(WalkOptions.Default, subSelect, static (subSelect, qe) =>
								{
									if (qe is SqlColumn column && column.Parent == subSelect)
									{
										return column.Expression;
									}

									return qe;
								});
						}
					}
				}
			});

			return query;
		}

		public static SqlSearchCondition BuildSearchCondition(ExpressionBuilder builder, SqlStatement statement, IBuildContext onContext, IBuildContext? secondContext, LambdaExpression condition)
		{
			SqlSearchCondition result;

			var isTableContext = onContext.IsExpression(null, 0, RequestFor.Table);
			if (isTableContext.Result)
			{
				var tableContext  = (TableBuilder.TableContext)onContext;
				var clonedContext = new TableBuilder.TableContext(builder, new SelectQuery(), tableContext.SqlTable);

				var targetParameter = Expression.Parameter(tableContext.ObjectType);

				if (secondContext != null)
				{
					var secondContextRefExpression = new ContextRefExpression(condition.Parameters[1].Type, secondContext);
					var newBody                    = condition.GetBody(targetParameter, secondContextRefExpression);

					condition = Expression.Lambda(newBody, targetParameter);
				}
				else
				{
					var newBody = condition.GetBody(targetParameter);
					condition   = Expression.Lambda(newBody, targetParameter);
				}

				var subqueryContext = new SubQueryContext(clonedContext);
				var contextRef      = new ContextRefExpression(typeof(IQueryable<>).MakeGenericType(tableContext.ObjectType), subqueryContext);
				var whereMethodInfo = Methods.Queryable.Where.MakeGenericMethod(tableContext.ObjectType);
				var whereCall       = Expression.Call(whereMethodInfo, contextRef, condition);

				var buildSequence = builder.BuildSequence(new BuildInfo((IBuildContext?)null, whereCall, new SelectQuery()));

				var query = buildSequence.SelectQuery;
				query     = RemoveContextFromQuery(clonedContext, query);

				//TODO: Why it is not handled by main optimizer
				var sqlFlags    = builder.DataContext.SqlProviderFlags;

				new SelectQueryOptimizer(sqlFlags, builder.DataContext.Options, query, query, 0, statement)
					.FinalizeAndValidate(sqlFlags.IsApplyJoinSupported);

				if (query.From.Tables.Count == 0)
				{
					result = query.Where.SearchCondition;
				}
				else
				{
					result = new SqlSearchCondition();
					result.Conditions.Add(new SqlCondition(false,
						new SqlPredicate.FuncLike(SqlFunction.CreateExists(query))));
				}
			}
			else
			{
				var conditionExpr = builder.ConvertExpression(condition.Body.Unwrap());
				result = new SqlSearchCondition();

				builder.BuildSearchCondition(
					new ExpressionContext(null, secondContext == null? new[] { onContext } : new[] { onContext, secondContext }, condition),
					conditionExpr,
					result.Conditions);
			}

			return result;
		}
	}
}
