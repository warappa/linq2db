﻿using System.Linq.Expressions;

namespace LinqToDB.Linq.Builder
{
	using SqlQuery;

	internal interface IBuildContext
	{
#if DEBUG
		string? SqlQueryText  { get; }
		string  Path          { get; }
		int     ContextId     { get; }
#endif

		ExpressionBuilder  Builder     { get; }
		Expression?        Expression  { get; }
		SelectQuery        SelectQuery { get; }
		SqlStatement?      Statement   { get; set; } // TODO: remove
		IBuildContext?     Parent      { get; set; } // TODO: probably not needed

		Expression     MakeExpression(Expression path, ProjectFlags flags);
		IBuildContext  Clone(CloningContext      context);
		void           SetRunQuery<T>(Query<T>   query,      Expression expr);
		IBuildContext? GetContext(Expression     expression, BuildInfo  buildInfo);
		void           SetAlias(string?          alias);
		SqlStatement   GetResultStatement();
		void           CompleteColumns();
	}
}
