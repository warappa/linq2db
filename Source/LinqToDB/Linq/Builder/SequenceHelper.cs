﻿using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace LinqToDB.Linq.Builder
{
	using System.Linq;
	using LinqToDB.Expressions;

	static class SequenceHelper
	{
		public static Expression PrepareBody(LambdaExpression lambda, params IBuildContext[] sequences)
		{
			var body = lambda.Parameters.Count == 0
				? lambda.Body
				: lambda.GetBody(sequences
					.Select((s, idx) =>
					{
						var parameter = lambda.Parameters[idx];
						return (Expression)new ContextRefExpression(parameter.Type, s, parameter.Name);
					}).ToArray());

			return body;
		}

		public static bool IsSameContext(Expression? expression, IBuildContext context)
		{
			return expression == null || expression is ContextRefExpression contextRef && contextRef.BuildContext == context;
		}

		[return: NotNullIfNotNull("expression")]
		public static Expression? CorrectExpression(Expression? expression, IBuildContext current, IBuildContext underlying)
		{
			if (expression != null)
			{
				return ReplaceContext(expression, current, underlying);
				/*var root = current.Builder.GetRootObject(expression);
				if (root is ContextRefExpression refExpression)
				{
					if (refExpression.BuildContext == current)
					{
						expression = expression.Replace(root, new ContextRefExpression(root.Type, underlying), EqualityComparer<Expression>.Default);
					}
				}*/
			}

			return expression;
		}

		[return: NotNullIfNotNull("expression")]
		public static Expression? CorrectTrackingPath(Expression? expression, IBuildContext current, IBuildContext underlying)
		{
			if (expression != null)
			{
				var transformed = expression.Transform((current, underlying), static (ctx, e) =>
				{
					if (e is SqlPlaceholderExpression placeholder && placeholder.TrackingPath != null)
					{
						e = placeholder.WithTrackingPath(CorrectExpression(placeholder.TrackingPath, 
							ctx.current,
							ctx.underlying));
					}

					return e;
				});

				return transformed;
			}

			return expression;
		}

		public static Expression ReplaceContext(Expression expression, IBuildContext current, IBuildContext onContext)
		{
			var newExpression = expression.Transform((expression, current, onContext), (ctx, e) =>
			{
				if (e.NodeType              == ExpressionType.Extension && e is ContextRefExpression contextRef &&
				    contextRef.BuildContext == ctx.current)
				{
					return new ContextRefExpression(contextRef.Type, ctx.onContext, contextRef.Alias);
				}

				return e;
			});

			return newExpression;
		}

		public static Expression MoveAllToScopedContext(Expression expression, IBuildContext upTo)
		{
			var newExpression = expression.Transform((expression, upTo), (ctx, e) =>
			{
				if (e.NodeType == ExpressionType.Extension)
				{
					if (e is ContextRefExpression contextRef)
						return contextRef.WithContext(new ScopeContext(contextRef.BuildContext, ctx.upTo));
				}

				return e;
			});

			return newExpression;
		}

		public static Expression MoveToScopedContext(Expression expression, IBuildContext upTo)
		{
			var scoped        = new ScopeContext(upTo, upTo);
			var newExpression = ReplaceContext(expression, upTo, scoped);
			return newExpression;
		}

		public static TableBuilder.TableContext? GetTableContext(IBuildContext context)
		{
			var contextRef = new ContextRefExpression(typeof(object), context);

			var rootContext = context.Builder.MakeExpression(context, contextRef, ProjectFlags.Table) as ContextRefExpression;

			var tableContext = rootContext?.BuildContext as TableBuilder.TableContext;

			return tableContext;
		}

		public static TableBuilder.CteTableContext? GetCteContext(IBuildContext context)
		{
			var contextRef = new ContextRefExpression(typeof(object), context);

			var rootContext = context.Builder.MakeExpression(context, contextRef, ProjectFlags.Table) as ContextRefExpression;

			var tableContext = rootContext?.BuildContext as TableBuilder.CteTableContext;

			return tableContext;
		}

		public static IBuildContext UnwrapSubqueryContext(IBuildContext context)
		{
			while (context is SubQueryContext sc)
			{
				context = sc.SubQuery;
			}

			return context;
		}

		public static bool IsDefaultIfEmpty(IBuildContext context)
		{
			return UnwrapSubqueryContext(context) is DefaultIfEmptyBuilder.DefaultIfEmptyContext;
	}

		public static Expression RequireSqlExpression(this IBuildContext context, Expression? path)
		{
			var sql = context.Builder.MakeExpression(context, path, ProjectFlags.SQL);
			if (sql == null)
				throw new LinqException("'{0}' cannot be converted to SQL.", path);

			return sql;
}
	}
}
