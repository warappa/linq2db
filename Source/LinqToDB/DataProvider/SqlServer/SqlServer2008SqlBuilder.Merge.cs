﻿namespace LinqToDB.DataProvider.SqlServer
{
	using SqlQuery;

	partial class SqlServer2008SqlBuilder
	{
		protected override void BuildMergeInto(NullabilityContext nullability, SqlMergeStatement merge)
		{
			StringBuilder
				.Append("MERGE INTO ");

			BuildTableName(nullability, merge.Target, true, false);

			StringBuilder.Append(' ');

			if (merge.Hint != null)
			{
				StringBuilder
					.Append("WITH(")
					.Append(merge.Hint)
					.Append(") ");
			}

			BuildTableName(nullability, merge.Target, false, true);
			StringBuilder.AppendLine();
		}

		protected override void BuildMergeOperationDeleteBySource(NullabilityContext nullability, SqlMergeOperationClause operation)
		{
			StringBuilder
				.AppendLine()
				.Append("WHEN NOT MATCHED BY SOURCE");

			if (operation.Where != null)
			{
				StringBuilder.Append(" AND ");
				BuildSearchCondition(nullability, Precedence.Unknown, operation.Where, wrapCondition: true);
			}

			StringBuilder.AppendLine(" THEN DELETE");
		}

		protected override void BuildMergeTerminator(NullabilityContext nullability, SqlMergeStatement merge)
		{
			// merge command must be terminated with semicolon
			StringBuilder.AppendLine(";");

			// for identity column insert - disable explicit insert support
			if (merge.HasIdentityInsert)
				BuildIdentityInsert(nullability, merge.Target, false);
		}

		protected override void BuildMergeOperationUpdateBySource(NullabilityContext nullability,
			SqlMergeOperationClause                                                  operation)
		{
			StringBuilder
				.AppendLine()
				.Append("WHEN NOT MATCHED By Source");

			if (operation.Where != null)
			{
				StringBuilder.Append(" AND ");
				BuildSearchCondition(nullability, Precedence.Unknown, operation.Where, wrapCondition: true);
			}

			StringBuilder.AppendLine(" THEN UPDATE");

			var update = new SqlUpdateClause();
			update.Items.AddRange(operation.Items);
			BuildUpdateSet(nullability, null, update);
		}

		protected override void BuildMergeStatement(SqlMergeStatement merge)
		{
			var nullability = new NullabilityContext(merge.SelectQuery);

			// for identity column insert - enable explicit insert support
			if (merge.HasIdentityInsert)
				BuildIdentityInsert(nullability, merge.Target, true);

			base.BuildMergeStatement(merge);
		}
	}
}
