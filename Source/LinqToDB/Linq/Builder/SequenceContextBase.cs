﻿using System.Diagnostics;
using System.Linq.Expressions;

namespace LinqToDB.Linq.Builder
{
	using SqlQuery;

	[DebuggerDisplay("{BuildContextDebuggingHelper.GetContextInfo(this)}")]
	abstract class SequenceContextBase : BuildContextBase
	{
		protected SequenceContextBase(IBuildContext? parent, IBuildContext[] sequences, LambdaExpression? lambda)
			: base(sequences[0].Builder, sequences[0].SelectQuery)
		{
			Parent          = parent;
			Sequences       = sequences;
			Body            = lambda == null ? null : SequenceHelper.PrepareBody(lambda, sequences);
			Sequence.Parent = this;
		}

		protected SequenceContextBase(IBuildContext? parent, IBuildContext sequence, LambdaExpression? lambda)
			: this(parent, new[] { sequence }, lambda)
		{
		}

		public IBuildContext[]   Sequences   { get; set; }
		public Expression?       Body        { get; set; }
		public IBuildContext     Sequence    => Sequences[0];

		public override Expression? Expression => Body;

		public override Expression MakeExpression(Expression path, ProjectFlags flags)
		{
			path = SequenceHelper.CorrectExpression(path, this, Sequence);
			return Builder.MakeExpression(Sequence, path, flags);
		}

		public override void SetRunQuery<T>(Query<T> query, Expression expr)
		{
			var mapper = Builder.BuildMapper<T>(SelectQuery, expr);

			QueryRunner.SetRunQuery(query, mapper);
		}

		public override SqlStatement GetResultStatement()
		{
			return Sequence.GetResultStatement();
		}

		public override void CompleteColumns()
		{
			foreach (var sequence in Sequences)
			{
				sequence.CompleteColumns();
			}
		}

		public override void SetAlias(string? alias)
		{
			if (SelectQuery.Select.Columns.Count == 1)
			{
				SelectQuery.Select.Columns[0].Alias = alias;
			}

			if (SelectQuery.From.Tables.Count > 0)
				SelectQuery.From.Tables[SelectQuery.From.Tables.Count - 1].Alias = alias;
		}
	}
}
