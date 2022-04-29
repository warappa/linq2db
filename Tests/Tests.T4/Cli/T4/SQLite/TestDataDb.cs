// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.T4.SQLite
{
	public partial class TestDataDB : DataConnection
	{
		public TestDataDB()
		{
			InitDataContext();
		}

		public TestDataDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
		}

		public TestDataDB(LinqToDBConnectionOptions options)
			: base(options)
		{
			InitDataContext();
		}

		public TestDataDB(LinqToDBConnectionOptions<TestDataDB> options)
			: base(options)
		{
			InitDataContext();
		}

		partial void InitDataContext();

		public ITable<Dual>              Duals               => this.GetTable<Dual>();
		public ITable<InheritanceParent> InheritanceParents  => this.GetTable<InheritanceParent>();
		public ITable<InheritanceChild>  InheritanceChildren => this.GetTable<InheritanceChild>();
		public ITable<Person>            People              => this.GetTable<Person>();
		public ITable<Doctor>            Doctors             => this.GetTable<Doctor>();
		public ITable<Patient>           Patients            => this.GetTable<Patient>();
		public ITable<Parent>            Parents             => this.GetTable<Parent>();
		public ITable<Child>             Children            => this.GetTable<Child>();
		public ITable<GrandChild>        GrandChildren       => this.GetTable<GrandChild>();
		public ITable<LinqDataType>      LinqDataTypes       => this.GetTable<LinqDataType>();
		public ITable<TestIdentity>      TestIdentities      => this.GetTable<TestIdentity>();
		public ITable<AllType>           AllTypes            => this.GetTable<AllType>();
		public ITable<PrimaryKeyTable>   PrimaryKeyTables    => this.GetTable<PrimaryKeyTable>();
		public ITable<ForeignKeyTable>   ForeignKeyTables    => this.GetTable<ForeignKeyTable>();
		public ITable<FKTestPosition>    FKTestPositions     => this.GetTable<FKTestPosition>();
		public ITable<TestMerge1>        TestMerge1          => this.GetTable<TestMerge1>();
		public ITable<TestMerge2>        TestMerge2          => this.GetTable<TestMerge2>();
		public ITable<TestT4Casing>      TestT4Casings       => this.GetTable<TestT4Casing>();
	}

	[Table("Dual")]
	public partial class Dual
	{
		[Column("Dummy")] public string? Dummy { get; set; } // varchar(10)
	}

	[Table("InheritanceParent")]
	public partial class InheritanceParent
	{
		[Column("InheritanceParentId")] public long    InheritanceParentId { get; set; } // integer
		[Column("TypeDiscriminator"  )] public long?   TypeDiscriminator   { get; set; } // integer
		[Column("Name"               )] public string? Name                { get; set; } // nvarchar(50)
	}

	[Table("InheritanceChild")]
	public partial class InheritanceChild
	{
		[Column("InheritanceChildId" )] public long    InheritanceChildId  { get; set; } // integer
		[Column("InheritanceParentId")] public long    InheritanceParentId { get; set; } // integer
		[Column("TypeDiscriminator"  )] public long?   TypeDiscriminator   { get; set; } // integer
		[Column("Name"               )] public string? Name                { get; set; } // nvarchar(50)
	}

	[Table("Person")]
	public partial class Person
	{
		[Column("PersonID"  , IsPrimaryKey = true , IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public long    PersonID   { get; set; } // integer
		[Column("FirstName" , CanBeNull    = false                                                             )] public string  FirstName  { get; set; } = null!; // nvarchar(50)
		[Column("LastName"  , CanBeNull    = false                                                             )] public string  LastName   { get; set; } = null!; // nvarchar(50)
		[Column("MiddleName"                                                                                   )] public string? MiddleName { get; set; } // nvarchar(50)
		[Column("Gender"                                                                                       )] public char    Gender     { get; set; } // char(1)

		#region Associations
		/// <summary>
		/// FK_Doctor_0_0 backreference
		/// </summary>
		[Association(ThisKey = nameof(PersonID), OtherKey = nameof(SQLite.Doctor.PersonID))]
		public Doctor? Doctor { get; set; }

		/// <summary>
		/// FK_Patient_0_0 backreference
		/// </summary>
		[Association(ThisKey = nameof(PersonID), OtherKey = nameof(SQLite.Patient.PersonID))]
		public Patient? Patient { get; set; }
		#endregion
	}

	public static partial class ExtensionMethods
	{
		#region Table Extensions
		public static Person? Find(this ITable<Person> table, long personId)
		{
			return table.FirstOrDefault(e => e.PersonID == personId);
		}

		public static Doctor? Find(this ITable<Doctor> table, long personId)
		{
			return table.FirstOrDefault(e => e.PersonID == personId);
		}

		public static Patient? Find(this ITable<Patient> table, long personId)
		{
			return table.FirstOrDefault(e => e.PersonID == personId);
		}

		public static TestIdentity? Find(this ITable<TestIdentity> table, long id)
		{
			return table.FirstOrDefault(e => e.ID == id);
		}

		public static AllType? Find(this ITable<AllType> table, long id)
		{
			return table.FirstOrDefault(e => e.ID == id);
		}

		public static PrimaryKeyTable? Find(this ITable<PrimaryKeyTable> table, long id)
		{
			return table.FirstOrDefault(e => e.ID == id);
		}

		public static FKTestPosition? Find(this ITable<FKTestPosition> table, long company, long department, long positionId)
		{
			return table.FirstOrDefault(e => e.Company == company && e.Department == department && e.PositionID == positionId);
		}
		#endregion
	}

	[Table("Doctor")]
	public partial class Doctor
	{
		[Column("PersonID", IsPrimaryKey = true )] public long   PersonID { get; set; } // integer
		[Column("Taxonomy", CanBeNull    = false)] public string Taxonomy { get; set; } = null!; // nvarchar(50)

		#region Associations
		/// <summary>
		/// FK_Doctor_0_0
		/// </summary>
		[Association(CanBeNull = false, ThisKey = nameof(PersonID), OtherKey = nameof(SQLite.Person.PersonID))]
		public Person Person { get; set; } = null!;
		#endregion
	}

	[Table("Patient")]
	public partial class Patient
	{
		[Column("PersonID" , IsPrimaryKey = true )] public long   PersonID  { get; set; } // integer
		[Column("Diagnosis", CanBeNull    = false)] public string Diagnosis { get; set; } = null!; // nvarchar(256)

		#region Associations
		/// <summary>
		/// FK_Patient_0_0
		/// </summary>
		[Association(CanBeNull = false, ThisKey = nameof(PersonID), OtherKey = nameof(SQLite.Person.PersonID))]
		public Person Person { get; set; } = null!;
		#endregion
	}

	[Table("Parent")]
	public partial class Parent
	{
		[Column("ParentID")] public int? ParentID { get; set; } // int
		[Column("Value1"  )] public int? Value1   { get; set; } // int
	}

	[Table("Child")]
	public partial class Child
	{
		[Column("ParentID")] public int? ParentID { get; set; } // int
		[Column("ChildID" )] public int? ChildID  { get; set; } // int
	}

	[Table("GrandChild")]
	public partial class GrandChild
	{
		[Column("ParentID"    )] public int? ParentID     { get; set; } // int
		[Column("ChildID"     )] public int? ChildID      { get; set; } // int
		[Column("GrandChildID")] public int? GrandChildID { get; set; } // int
	}

	[Table("LinqDataTypes")]
	public partial class LinqDataType
	{
		[Column("ID"            )] public int?      ID             { get; set; } // int
		[Column("MoneyValue"    )] public decimal?  MoneyValue     { get; set; } // decimal
		[Column("DateTimeValue" )] public DateTime? DateTimeValue  { get; set; } // datetime
		[Column("DateTimeValue2")] public DateTime? DateTimeValue2 { get; set; } // datetime2
		[Column("BoolValue"     )] public bool?     BoolValue      { get; set; } // boolean
		[Column("GuidValue"     )] public Guid?     GuidValue      { get; set; } // uniqueidentifier
		[Column("BinaryValue"   )] public byte[]?   BinaryValue    { get; set; } // binary
		[Column("SmallIntValue" )] public short?    SmallIntValue  { get; set; } // smallint
		[Column("IntValue"      )] public int?      IntValue       { get; set; } // int
		[Column("BigIntValue"   )] public long?     BigIntValue    { get; set; } // bigint
		[Column("StringValue"   )] public string?   StringValue    { get; set; } // nvarchar(50)
	}

	[Table("TestIdentity")]
	public partial class TestIdentity
	{
		[Column("ID", IsPrimaryKey = true, IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public long ID { get; set; } // integer
	}

	[Table("AllTypes")]
	public partial class AllType
	{
		[Column("ID"                      , IsPrimaryKey = true, IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public long      ID                       { get; set; } // integer
		[Column("bigintDataType"                                                                                            )] public long?     BigintDataType           { get; set; } // bigint
		[Column("numericDataType"                                                                                           )] public decimal?  NumericDataType          { get; set; } // numeric
		[Column("bitDataType"                                                                                               )] public bool?     BitDataType              { get; set; } // bit
		[Column("smallintDataType"                                                                                          )] public short?    SmallintDataType         { get; set; } // smallint
		[Column("decimalDataType"                                                                                           )] public decimal?  DecimalDataType          { get; set; } // decimal
		[Column("intDataType"                                                                                               )] public int?      IntDataType              { get; set; } // int
		[Column("tinyintDataType"                                                                                           )] public byte?     TinyintDataType          { get; set; } // tinyint
		[Column("moneyDataType"                                                                                             )] public decimal?  MoneyDataType            { get; set; } // money
		[Column("floatDataType"                                                                                             )] public double?   FloatDataType            { get; set; } // float
		[Column("realDataType"                                                                                              )] public double?   RealDataType             { get; set; } // real
		[Column("datetimeDataType"                                                                                          )] public DateTime? DatetimeDataType         { get; set; } // datetime
		[Column("charDataType"                                                                                              )] public char?     CharDataType             { get; set; } // char(1)
		[Column("char20DataType"                                                                                            )] public string?   Char20DataType           { get; set; } // char(20)
		[Column("varcharDataType"                                                                                           )] public string?   VarcharDataType          { get; set; } // varchar(20)
		[Column("textDataType"                                                                                              )] public string?   TextDataType             { get; set; } // text(max)
		[Column("ncharDataType"                                                                                             )] public string?   NcharDataType            { get; set; } // char(20)
		[Column("nvarcharDataType"                                                                                          )] public string?   NvarcharDataType         { get; set; } // nvarchar(20)
		[Column("ntextDataType"                                                                                             )] public string?   NtextDataType            { get; set; } // ntext(max)
		[Column("binaryDataType"                                                                                            )] public byte[]?   BinaryDataType           { get; set; } // binary
		[Column("varbinaryDataType"                                                                                         )] public byte[]?   VarbinaryDataType        { get; set; } // varbinary
		[Column("imageDataType"                                                                                             )] public byte[]?   ImageDataType            { get; set; } // image
		[Column("uniqueidentifierDataType"                                                                                  )] public Guid?     UniqueidentifierDataType { get; set; } // uniqueidentifier
		[Column("objectDataType"                                                                                            )] public object?   ObjectDataType           { get; set; } // object
	}

	[Table("PrimaryKeyTable")]
	public partial class PrimaryKeyTable
	{
		[Column("ID"  , IsPrimaryKey = true )] public long   ID   { get; set; } // integer
		[Column("Name", CanBeNull    = false)] public string Name { get; set; } = null!; // nvarchar(50)

		#region Associations
		/// <summary>
		/// FK_ForeignKeyTable_0_0 backreference
		/// </summary>
		[Association(ThisKey = nameof(ID), OtherKey = nameof(ForeignKeyTable.PrimaryKeyTableID))]
		public IEnumerable<ForeignKeyTable> ForeignKeyTables { get; set; } = null!;
		#endregion
	}

	[Table("ForeignKeyTable")]
	public partial class ForeignKeyTable
	{
		[Column("PrimaryKeyTableID"                   )] public long   PrimaryKeyTableID { get; set; } // integer
		[Column("Name"             , CanBeNull = false)] public string Name              { get; set; } = null!; // nvarchar(50)

		#region Associations
		/// <summary>
		/// FK_ForeignKeyTable_0_0
		/// </summary>
		[Association(CanBeNull = false, ThisKey = nameof(PrimaryKeyTableID), OtherKey = nameof(SQLite.PrimaryKeyTable.ID))]
		public PrimaryKeyTable PrimaryKeyTable { get; set; } = null!;
		#endregion
	}

	[Table("FKTestPosition")]
	public partial class FKTestPosition
	{
		[Column("Company"   , IsPrimaryKey = true , PrimaryKeyOrder = 0)] public long   Company    { get; set; } // integer
		[Column("Department", IsPrimaryKey = true , PrimaryKeyOrder = 1)] public long   Department { get; set; } // integer
		[Column("PositionID", IsPrimaryKey = true , PrimaryKeyOrder = 2)] public long   PositionID { get; set; } // integer
		[Column("Name"      , CanBeNull    = false                     )] public string Name       { get; set; } = null!; // nvarchar(50)
	}

	[Table("TestMerge1")]
	public partial class TestMerge1
	{
		[Column("Id"             )] public long      Id              { get; set; } // integer
		[Column("Field1"         )] public long?     Field1          { get; set; } // integer
		[Column("Field2"         )] public long?     Field2          { get; set; } // integer
		[Column("Field3"         )] public long?     Field3          { get; set; } // integer
		[Column("Field4"         )] public long?     Field4          { get; set; } // integer
		[Column("Field5"         )] public long?     Field5          { get; set; } // integer
		[Column("FieldInt64"     )] public long?     FieldInt64      { get; set; } // bigint
		[Column("FieldBoolean"   )] public bool?     FieldBoolean    { get; set; } // bit
		[Column("FieldString"    )] public string?   FieldString     { get; set; } // varchar(20)
		[Column("FieldNString"   )] public string?   FieldNString    { get; set; } // nvarchar(20)
		[Column("FieldChar"      )] public char?     FieldChar       { get; set; } // char(1)
		[Column("FieldNChar"     )] public char?     FieldNChar      { get; set; } // char(1)
		[Column("FieldFloat"     )] public double?   FieldFloat      { get; set; } // float
		[Column("FieldDouble"    )] public double?   FieldDouble     { get; set; } // float
		[Column("FieldDateTime"  )] public DateTime? FieldDateTime   { get; set; } // datetime
		[Column("FieldBinary"    )] public byte[]?   FieldBinary     { get; set; } // varbinary
		[Column("FieldGuid"      )] public Guid?     FieldGuid       { get; set; } // uniqueidentifier
		[Column("FieldDate"      )] public DateTime? FieldDate       { get; set; } // date
		[Column("FieldEnumString")] public string?   FieldEnumString { get; set; } // varchar(20)
		[Column("FieldEnumNumber")] public int?      FieldEnumNumber { get; set; } // int
	}

	[Table("TestMerge2")]
	public partial class TestMerge2
	{
		[Column("Id"             )] public long      Id              { get; set; } // integer
		[Column("Field1"         )] public long?     Field1          { get; set; } // integer
		[Column("Field2"         )] public long?     Field2          { get; set; } // integer
		[Column("Field3"         )] public long?     Field3          { get; set; } // integer
		[Column("Field4"         )] public long?     Field4          { get; set; } // integer
		[Column("Field5"         )] public long?     Field5          { get; set; } // integer
		[Column("FieldInt64"     )] public long?     FieldInt64      { get; set; } // bigint
		[Column("FieldBoolean"   )] public bool?     FieldBoolean    { get; set; } // bit
		[Column("FieldString"    )] public string?   FieldString     { get; set; } // varchar(20)
		[Column("FieldNString"   )] public string?   FieldNString    { get; set; } // nvarchar(20)
		[Column("FieldChar"      )] public char?     FieldChar       { get; set; } // char(1)
		[Column("FieldNChar"     )] public char?     FieldNChar      { get; set; } // char(1)
		[Column("FieldFloat"     )] public double?   FieldFloat      { get; set; } // float
		[Column("FieldDouble"    )] public double?   FieldDouble     { get; set; } // float
		[Column("FieldDateTime"  )] public DateTime? FieldDateTime   { get; set; } // datetime
		[Column("FieldBinary"    )] public byte[]?   FieldBinary     { get; set; } // varbinary
		[Column("FieldGuid"      )] public Guid?     FieldGuid       { get; set; } // uniqueidentifier
		[Column("FieldDate"      )] public DateTime? FieldDate       { get; set; } // date
		[Column("FieldEnumString")] public string?   FieldEnumString { get; set; } // varchar(20)
		[Column("FieldEnumNumber")] public int?      FieldEnumNumber { get; set; } // int
	}

	[Table("TEST_T4_CASING")]
	public partial class TestT4Casing
	{
		[Column("ALL_CAPS"             )] public int AllCaps             { get; set; } // int
		[Column("CAPS"                 )] public int Caps                { get; set; } // int
		[Column("PascalCase"           )] public int PascalCase          { get; set; } // int
		[Column("Pascal_Snake_Case"    )] public int PascalSnakeCase     { get; set; } // int
		[Column("PascalCase_Snake_Case")] public int PascalCaseSnakeCase { get; set; } // int
		[Column("snake_case"           )] public int SnakeCase           { get; set; } // int
		[Column("camelCase"            )] public int CamelCase           { get; set; } // int
	}
}
