﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LinqToDB.SqlQuery
{
	public class SqlColumn : IEquatable<SqlColumn>, ISqlExpression
	{
		public SqlColumn(SelectQuery? parent, ISqlExpression expression, string? alias)
		{
			Parent      = parent;
			_expression = expression ?? throw new ArgumentNullException(nameof(expression));
			RawAlias    = alias;

#if DEBUG
			_columnNumber = ++_columnCounter;
#endif
		}

		public SqlColumn(SelectQuery builder, ISqlExpression expression)
			: this(builder, expression, null)
		{
		}

#if DEBUG
		readonly int _columnNumber;
		public   int  ColumnNumber => _columnNumber;
		static   int _columnCounter;
#endif

		ISqlExpression _expression;

		public ISqlExpression Expression
		{
			get => _expression;
			set
			{
				if (_expression == value)
					return;
				if (value == this)
					throw new InvalidOperationException();
				_expression = value;
				_hashCode   = null;
			}
		}

		SelectQuery? _parent;

		public SelectQuery? Parent
		{
			get => _parent;
			set
			{
				if (_parent == value)
					return;
				_parent   = value;
				_hashCode = null;
			}
		}

		internal string? RawAlias   { get; set; }

		public ISqlExpression UnderlyingExpression()
		{
			var current = QueryHelper.UnwrapExpression(Expression, true);
			while (current.ElementType == QueryElementType.Column)
			{
				var column      = (SqlColumn)current;
				var columnQuery = column.Parent;
				if (columnQuery == null || columnQuery.HasSetOperators || QueryHelper.EnumerateLevelSources(columnQuery).Take(2).Count() > 1)
					break;
				current = QueryHelper.UnwrapExpression(column.Expression, true);
			}

			return current;
		}

		public string? Alias
		{
			get
			{
				if (RawAlias == null)
					return GetAlias(Expression);

				return RawAlias;
			}
			set => RawAlias = value;
		}

		static string? GetAlias(ISqlExpression? expr)
		{
			switch (expr)
			{
				case SqlField    field  : return field.Alias ?? field.PhysicalName;
				case SqlColumn   column : return column.Alias;
				case SelectQuery query  :
					{
						if (query.Select.Columns.Count == 1 && query.Select.Columns[0].Alias != "*")
							return query.Select.Columns[0].Alias;
						break;
					}
				case SqlExpression e
					when e.Expr is "{0}": return GetAlias(e.Parameters[0]);
			}

			return null;
		}

		int? _hashCode;

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
		public override int GetHashCode()
		{
			if (_hashCode.HasValue)
				return _hashCode.Value;

			var hashCode = Parent?.GetHashCode() ?? 0;

			hashCode = unchecked(hashCode + (hashCode * 397) ^ Expression.GetHashCode());

			_hashCode = hashCode;

			return hashCode;
		}

		public bool Equals(SqlColumn? other)
		{
			if (other == null)
				return false;

			if (ReferenceEquals(this, other))
				return true;

			if (!Equals(Parent, other.Parent))
				return false;

			if (Expression.Equals(other.Expression))
				return false;

			return false;
		}

		public override string ToString()
		{
#if OVERRIDETOSTRING
			var writer = new SqlTextWriter();
			var dic    = new Dictionary<IQueryElement, IQueryElement>();

			writer
				.Append('t')
				.Append(Parent?.SourceID ?? -1)
#if DEBUG
				.Append('[').Append(_columnNumber).Append(']')
#endif
				.Append('.')
				.Append(Alias ?? "c")
				.Append(" => ")
				.Append(Expression, dic);

			var underlying = UnderlyingExpression();
			if (!ReferenceEquals(underlying, Expression))
			{
				writer
					.Append(" := ")
					.Append(underlying, dic);
			}

			if (CanBeNull)
				writer.Append('?');

			return writer.ToString();

#else
			if (Expression is SqlField)
				return ((IQueryElement)this).ToString(new StringBuilder(), new Dictionary<IQueryElement,IQueryElement>()).ToString();

			return base.ToString()!;
#endif
		}

		#region ISqlExpression Members

		public bool CanBeNullable(NullabilityContext nullability)
		{
			return nullability.CanBeNull(this);
		}

		public bool         CanBeNull => Expression.CanBeNull;

		public bool Equals(ISqlExpression other, Func<ISqlExpression,ISqlExpression,bool> comparer)
		{
			if (this == other)
				return true;

			if (!(other is SqlColumn otherColumn))
				return false;

			if (Parent != otherColumn.Parent)
				return false;

			if (Parent!.HasSetOperators)
				return false;

			return
				Expression.Equals(
					otherColumn.Expression,
					(ex1, ex2) =>
					{
//							var c = ex1 as Column;
//							if (c != null && c.Parent != Parent)
//								return false;
//							c = ex2 as Column;
//							if (c != null && c.Parent != Parent)
//								return false;
						return comparer(ex1, ex2);
					})
				&&
				comparer(this, other);
		}

		public int   Precedence => SqlQuery.Precedence.Primary;
		public Type? SystemType => Expression.SystemType;

		#endregion

		#region IEquatable<ISqlExpression> Members

		bool IEquatable<ISqlExpression>.Equals(ISqlExpression? other)
		{
			if (this == other)
				return true;

			return other is SqlColumn column && Equals(column);
		}

		#endregion

		#region ISqlExpressionWalkable Members

		public ISqlExpression Walk<TContext>(WalkOptions options, TContext context, Func<TContext, ISqlExpression, ISqlExpression> func)
		{
			Expression = Expression.Walk(options, context, func)!;

			if (options.ProcessParent)
				Parent = (SelectQuery)func(context, Parent!);

			return func(context, this);
		}

		#endregion

		#region IQueryElement Members

		public QueryElementType ElementType => QueryElementType.Column;

		StringBuilder IQueryElement.ToString(StringBuilder sb, Dictionary<IQueryElement,IQueryElement> dic)
		{
			var parentIndex = -1;
			if (Parent != null)
			{
				parentIndex = Parent.Select.Columns.IndexOf(this);
			}

			sb
				.Append('t')
				.Append(Parent?.SourceID ?? - 1)
#if DEBUG
				.Append('[').Append(_columnNumber).Append(']')
#endif
				.Append('.')
				.Append(Alias ?? "c" + (parentIndex >= 0 ? parentIndex + 1 : parentIndex));

				if (CanBeNull)
					sb.Append('?');

			return sb;
		}

		#endregion
	}
}
