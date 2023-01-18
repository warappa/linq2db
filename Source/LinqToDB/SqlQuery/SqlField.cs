﻿using System;

namespace LinqToDB.SqlQuery
{
	using Common;
	using Mapping;

	public class SqlField : ISqlExpression
	{
		internal static SqlField All(ISqlTableSource table)
		{
			return new SqlField(table, "*", "*");
		}

		public SqlField(ISqlTableSource table, string name)
		{
			Table     = table;
			Name      = name;
			CanBeNull = true;
		}

		public SqlField(DbDataType dbDataType, string? name, bool canBeNull)
		{
			Type      = dbDataType;
			Name      = name!;
			CanBeNull = canBeNull;
		}

		SqlField(ISqlTableSource table, string name, string physicalName)
		{
			Table        = table;
			Name         = name;
			PhysicalName = physicalName;
			CanBeNull    = true;
		}

		public SqlField(string name, string physicalName)
		{
			Name         = name;
			PhysicalName = physicalName;
			CanBeNull    = true;
		}

		public SqlField(SqlField field)
		{
			Type             = field.Type;
			Alias            = field.Alias;
			Name             = field.Name;
			PhysicalName     = field.PhysicalName;
			CanBeNull        = field.CanBeNull;
			IsPrimaryKey     = field.IsPrimaryKey;
			PrimaryKeyOrder  = field.PrimaryKeyOrder;
			IsIdentity       = field.IsIdentity;
			IsInsertable     = field.IsInsertable;
			IsUpdatable      = field.IsUpdatable;
			CreateFormat     = field.CreateFormat;
			CreateOrder      = field.CreateOrder;
			ColumnDescriptor = field.ColumnDescriptor;
			IsDynamic        = field.IsDynamic;
		}

		public SqlField(ColumnDescriptor column)
		{
			Type              = column.GetDbDataType(true);
			Name              = column.MemberName;
			PhysicalName      = column.ColumnName;
			CanBeNull         = column.CanBeNull;
			IsPrimaryKey      = column.IsPrimaryKey;
			PrimaryKeyOrder   = column.PrimaryKeyOrder;
			IsIdentity        = column.IsIdentity;
			IsInsertable      = !column.SkipOnInsert;
			IsUpdatable       = !column.SkipOnUpdate;
			SkipOnEntityFetch = column.SkipOnEntityFetch;
			CreateFormat      = column.CreateFormat;
			CreateOrder       = column.Order;
			ColumnDescriptor  = column;
		}

		public DbDataType        Type              { get; set; }
		public string?           Alias             { get; set; }
		public string            Name              { get; set; } = null!; // not always true, see ColumnDescriptor notes
		public bool              IsPrimaryKey      { get; set; }
		public int               PrimaryKeyOrder   { get; set; }
		public bool              IsIdentity        { get; set; }
		public bool              IsInsertable      { get; set; }
		public bool              IsUpdatable       { get; set; }
		public bool              IsDynamic         { get; set; }
		public bool              SkipOnEntityFetch { get; set; }
		public string?           CreateFormat      { get; set; }
		public int?              CreateOrder       { get; set; }

		public ISqlTableSource?  Table             { get; set; }
		public ColumnDescriptor  ColumnDescriptor  { get; set; } = null!; // TODO: not true, we probably should introduce something else for non-column fields

		Type ISqlExpression.SystemType => Type.SystemType;

		string? _physicalName;
		public  string   PhysicalName
		{
			get => _physicalName ?? Name;
			set => _physicalName = value;
		}

		#region Overrides

//#if OVERRIDETOSTRING

		public override string ToString()
		{
			return this.ToDebugString();
		}

//#endif

		#endregion

		#region ISqlExpression Members

		public bool CanBeNullable(NullabilityContext nullability) => nullability.CanBeNull(this);

		public bool CanBeNull { get; set; }

		public bool Equals(ISqlExpression other, Func<ISqlExpression, ISqlExpression, bool> comparer)
		{
			return this == other;
		}

		public int Precedence => SqlQuery.Precedence.Primary;

		#endregion

		#region ISqlExpressionWalkable Members

		ISqlExpression ISqlExpressionWalkable.Walk<TContext>(WalkOptions options, TContext context, Func<TContext, ISqlExpression, ISqlExpression> func)
		{
			return func(context, this);
		}

		#endregion

		#region IEquatable<ISqlExpression> Members

		bool IEquatable<ISqlExpression>.Equals(ISqlExpression? other)
		{
			return this == other;
		}

		#endregion

		#region IQueryElement Members

		public QueryElementType ElementType => QueryElementType.SqlField;

		QueryElementTextWriter IQueryElement.ToString(QueryElementTextWriter writer)
		{
			if (Table != null)
				writer
					.Append('t')
					.Append(Table.SourceID)
					.Append('.');

			writer.Append(Name);
			if (CanBeNull)
				writer.Append("?");
			return writer;
		}

		#endregion

		internal static SqlField FakeField(DbDataType dataType, string fieldName, bool canBeNull)
		{
			var field = new SqlField(fieldName, fieldName);
			field.Type = dataType;
			return field;
		}
	}
}
