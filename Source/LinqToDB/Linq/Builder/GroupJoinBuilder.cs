﻿using System.Diagnostics;
using System.Linq.Expressions;
using LinqToDB.Expressions;

namespace LinqToDB.Linq.Builder
{
	using Reflection;
	using SqlQuery;

	class GroupJoinBuilder : MethodCallBuilder
	{
		protected override bool CanBuildMethodCall(ExpressionBuilder builder, MethodCallExpression methodCall, BuildInfo buildInfo)
		{
			return methodCall.IsQueryable("GroupJoin");
		}

		protected override IBuildContext BuildMethodCall(ExpressionBuilder builder, MethodCallExpression methodCall, BuildInfo buildInfo)
		{
			var outerExpression = methodCall.Arguments[0];
			var outerContext = builder.BuildSequence(new BuildInfo(buildInfo, outerExpression, buildInfo.SelectQuery));

			var innerExpression = methodCall.Arguments[1].Unwrap();

			var outerKeyLambda = methodCall.Arguments[2].UnwrapLambda();
			var innerKeyLambda = methodCall.Arguments[3].UnwrapLambda();
			var resultLambda   = methodCall.Arguments[4].UnwrapLambda();

			var outerKey = SequenceHelper.PrepareBody(outerKeyLambda, outerContext);

			var elementType = ExpressionBuilder.GetEnumerableElementType(resultLambda.Parameters[1].Type);
			var innerContext = new GroupJoinInnerContext(buildInfo.Parent, outerContext.SelectQuery, builder, 
				elementType,
				outerKey,
				innerKeyLambda, innerExpression);

			var context = new SelectContext(buildInfo.Parent, resultLambda, buildInfo.IsSubQuery, outerContext, innerContext);

			return context;
		}

		protected override SequenceConvertInfo? Convert(ExpressionBuilder builder, MethodCallExpression methodCall, BuildInfo buildInfo,
			ParameterExpression? param)
		{
			return null;
		}

		[DebuggerDisplay("{BuildContextDebuggingHelper.GetContextInfo(this)}")]
		class GroupJoinInnerContext : IBuildContext
		{
			public GroupJoinInnerContext(IBuildContext? parent, SelectQuery outerQuery, ExpressionBuilder builder, Type elementType,
				Expression outerKey, LambdaExpression innerKeyLambda,
				Expression innerExpression)
			{
				_elementType = elementType;
				Parent            = parent;
				Builder           = builder;
				OuterKey          = outerKey;
				InnerKeyLambda    = innerKeyLambda;
				InnerExpression   = innerExpression;

				SelectQuery = outerQuery;

				Builder.Contexts.Add(this);
	#if DEBUG
				ContextId = builder.GenerateContextId();
	#endif
			}

	#if DEBUG
			public string SqlQueryText => SelectQuery?.SqlText ?? "";
			public string Path         => this.GetPath();
			public int    ContextId    { get; }
	#endif

			public IBuildContext?    Parent          { get; set; }
			public ExpressionBuilder Builder         { get; }
			public Expression        OuterKey        { get; }
			public LambdaExpression  InnerKeyLambda  { get; }
			public Expression        InnerExpression { get; }
			public SelectQuery       SelectQuery     { get; set; }
			public SqlStatement?     Statement       { get; set; }

			readonly Type _elementType;

			Expression? IBuildContext.Expression    => null;

			public void BuildQuery<T>(Query<T> query, ParameterExpression queryParameter)
			{
				throw new NotImplementedException();
			}

			public Expression BuildExpression(Expression? expression, int level, bool enforceServerSide)
			{
				throw new NotImplementedException();
			}

			public SqlInfo[] ConvertToSql(Expression? expression, int level, ConvertFlags flags)
			{
				throw new NotImplementedException();
			}

			public SqlInfo[] ConvertToIndex (Expression? expression, int level, ConvertFlags flags)
			{
				throw new NotImplementedException();
			}

			public Expression MakeExpression(Expression path, ProjectFlags flags)
			{
				if (flags.HasFlag(ProjectFlags.Root) && SequenceHelper.IsSameContext(path, this))
				{
					return path;
				}

				if (SequenceHelper.IsSameContext(path, this) && (flags.HasFlag(ProjectFlags.Expression) || flags.HasFlag(ProjectFlags.Expand)) 
				                                             && !path.Type.IsAssignableFrom(_elementType))
				{
					var result = GetGroupJoinCall();
					return result;
				}

				return path;
			}

			public IBuildContext Clone(CloningContext context)
			{
				return new GroupJoinInnerContext(null, context.CloneElement(SelectQuery), Builder, _elementType,
					context.CloneExpression(OuterKey), context.CloneExpression(InnerKeyLambda), context.CloneExpression(InnerExpression));
			}

			public void SetRunQuery<T>(Query<T> query, Expression expr)
			{
				var mapper = Builder.BuildMapper<T>(expr);

				QueryRunner.SetRunQuery(query, mapper);
			}

			public IsExpressionResult IsExpression(Expression? expression, int level, RequestFor requestFlag)
			{
				throw new NotImplementedException();
			}

			public IBuildContext? GetContext (Expression? expression, int level, BuildInfo buildInfo)
			{
				var expr = GetGroupJoinCall();
				var sequence = Builder.BuildSequence(new BuildInfo(Parent, expr, new SelectQuery()));
				return sequence;
			}

			public SqlStatement GetResultStatement()
			{
				throw new NotImplementedException();
			}

			public void CompleteColumns()
			{
			}

			public int ConvertToParentIndex(int index, IBuildContext context)
			{
				throw new NotImplementedException();
			}

			public void SetAlias(string? alias)
			{
			}

			public ISqlExpression? GetSubQuery(IBuildContext context)
			{
				return null;
			}

			public Expression GetGroupJoinCall()
			{
				// Generating the following
				// innerExpression.Where(o => o.Key == innerKey)

				var filterLambda = Expression.Lambda(ExpressionBuilder.Equal(
						Builder.MappingSchema,
						OuterKey,
						InnerKeyLambda.Body),
					InnerKeyLambda.Parameters[0]);

				var expr = (Expression)Expression.Call(
					Methods.Queryable.Where.MakeGenericMethod(filterLambda.Parameters[0].Type),
					InnerExpression,
					filterLambda);

				expr = SequenceHelper.MoveToScopedContext(expr, this);

				return expr;
			}

		}
		
	}
}