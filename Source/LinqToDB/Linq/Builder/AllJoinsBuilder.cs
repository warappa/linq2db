﻿using System;
using System.Linq.Expressions;

namespace LinqToDB.Linq.Builder
{
	using LinqToDB.Expressions;
	using SqlQuery;

	[BuildsMethodCall("InnerJoin", "LeftJoin", "RightJoin", "FullJoin")]
	[BuildsMethodCall("Join", CanBuildName = nameof(CanBuildJoin))]
	sealed class AllJoinsBuilder : MethodCallBuilder
	{
		public static bool CanBuildJoin(MethodCallExpression call, BuildInfo info, ExpressionBuilder builder)
			=> call.IsQueryable() && call.Arguments.Count == 3;

		public static bool CanBuildMethod(MethodCallExpression call, BuildInfo info, ExpressionBuilder builder)
			=> call.IsQueryable() && call.Arguments.Count == 2;

		protected override IBuildContext BuildMethodCall(ExpressionBuilder builder, MethodCallExpression methodCall, BuildInfo buildInfo)
		{
			var argument = methodCall.Arguments[0];
			if (buildInfo.Parent != null)
			{
				argument = SequenceHelper.MoveToScopedContext(argument, buildInfo.Parent);
			}

			var sequence = builder.BuildSequence(new BuildInfo(buildInfo, argument));

			JoinType joinType;
			var conditionIndex = 1;

			switch (methodCall.Method.Name)
			{
				case "InnerJoin" : joinType = JoinType.Inner; break;
				case "LeftJoin"  : joinType = JoinType.Left;  break;
				case "RightJoin" : joinType = JoinType.Right; break;
				case "FullJoin"  : joinType = JoinType.Full;  break;
				default:
					conditionIndex = 2;

					joinType = (SqlJoinType) methodCall.Arguments[1].EvaluateExpression()! switch
					{
						SqlJoinType.Inner => JoinType.Inner,
						SqlJoinType.Left  => JoinType.Left,
						SqlJoinType.Right => JoinType.Right,
						SqlJoinType.Full  => JoinType.Full,
						_                 => throw new InvalidOperationException($"Unexpected join type: {(SqlJoinType)methodCall.Arguments[1].EvaluateExpression()!}")
					};
					break;
			}

			buildInfo.JoinType = joinType;

			DefaultIfEmptyBuilder.DefaultIfEmptyContext? sequenceDefaultIfEmpty = null;
			if (joinType == JoinType.Left || joinType == JoinType.Full)
				sequence = sequenceDefaultIfEmpty = new DefaultIfEmptyBuilder.DefaultIfEmptyContext(buildInfo.Parent, sequence, null, false);
			
			sequence = new SubQueryContext(sequence);

			if (methodCall.Arguments[conditionIndex] != null)
			{
				var condition = (LambdaExpression)methodCall.Arguments[conditionIndex].Unwrap();

				var result = builder.BuildWhere(buildInfo.Parent, sequence,
					condition: condition, checkForSubQuery: false, enforceHaving: false,
					isTest: buildInfo.AggregationTest);

				result.SetAlias(condition.Parameters[0].Name);
				return result;
			}

			return sequence;
		}
	}
}
