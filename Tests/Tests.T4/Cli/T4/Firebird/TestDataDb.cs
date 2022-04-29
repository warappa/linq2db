// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using FirebirdSql.Data.Types;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.T4.Firebird
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

		public ITable<AllType>           AllTypes            => this.GetTable<AllType>();
		public ITable<CamelCaseName>     CamelCaseNames      => this.GetTable<CamelCaseName>();
		public ITable<Child>             Children            => this.GetTable<Child>();
		public ITable<CollatedTable>     CollatedTables      => this.GetTable<CollatedTable>();
		public ITable<DataTypeTest>      DataTypeTests       => this.GetTable<DataTypeTest>();
		public ITable<Doctor>            Doctors             => this.GetTable<Doctor>();
		public ITable<Dual>              Duals               => this.GetTable<Dual>();
		public ITable<GrandChild>        GrandChildren       => this.GetTable<GrandChild>();
		public ITable<InheritanceChild>  InheritanceChildren => this.GetTable<InheritanceChild>();
		public ITable<InheritanceParent> InheritanceParents  => this.GetTable<InheritanceParent>();
		public ITable<LinqDataType>      LinqDataTypes       => this.GetTable<LinqDataType>();
		public ITable<Parent>            Parents             => this.GetTable<Parent>();
		public ITable<Patient>           Patients            => this.GetTable<Patient>();
		public ITable<Person>            People              => this.GetTable<Person>();
		public ITable<SequenceTest>      SequenceTests       => this.GetTable<SequenceTest>();
		public ITable<TestIdentity>      TestIdentities      => this.GetTable<TestIdentity>();
		public ITable<TestMerge1>        TestMerge1          => this.GetTable<TestMerge1>();
		public ITable<TestMerge2>        TestMerge2          => this.GetTable<TestMerge2>();
		public ITable<PersonView>        PersonViews         => this.GetTable<PersonView>();
	}

	[Table("AllTypes", Schema = "SYSDBA")]
	public partial class AllType
	{
		[Column("ID"                 , IsPrimaryKey = true)] public int              ID                  { get; set; } // integer
		[Column("bigintDataType"                          )] public long?            BigintDataType      { get; set; } // bigint
		[Column("smallintDataType"                        )] public short?           SmallintDataType    { get; set; } // smallint
		[Column("decimalDataType"                         )] public decimal?         DecimalDataType     { get; set; } // decimal(18,0)
		[Column("intDataType"                             )] public int?             IntDataType         { get; set; } // integer
		[Column("floatDataType"                           )] public float?           FloatDataType       { get; set; } // float
		[Column("realDataType"                            )] public float?           RealDataType        { get; set; } // float
		[Column("doubleDataType"                          )] public double?          DoubleDataType      { get; set; } // double precision
		[Column("timestampDataType"                       )] public DateTime?        TimestampDataType   { get; set; } // timestamp
		[Column("charDataType"                            )] public char?            CharDataType        { get; set; } // char(1)
		[Column("char20DataType"                          )] public string?          Char20DataType      { get; set; } // char(20)
		[Column("varcharDataType"                         )] public string?          VarcharDataType     { get; set; } // varchar(20)
		[Column("textDataType"                            )] public string?          TextDataType        { get; set; } // blob sub_type 1
		[Column("ncharDataType"                           )] public string?          NcharDataType       { get; set; } // char(20)
		[Column("nvarcharDataType"                        )] public string?          NvarcharDataType    { get; set; } // varchar(20)
		[Column("timestampTZDataType"                     )] public FbZonedDateTime? TimestampTZDataType { get; set; } // timestamp with time zone
		[Column("timeTZDataType"                          )] public FbZonedTime?     TimeTZDataType      { get; set; } // time with time zone
		[Column("decfloat16DataType"                      )] public FbDecFloat?      Decfloat16DataType  { get; set; } // decfloat
		[Column("decfloat34DataType"                      )] public FbDecFloat?      Decfloat34DataType  { get; set; } // decfloat
		[Column("int128DataType"                          )] public BigInteger?      Int128DataType      { get; set; } // int128
		[Column("blobDataType"                            )] public byte[]?          BlobDataType        { get; set; } // blob
	}

	public static partial class ExtensionMethods
	{
		#region Table Extensions
		public static AllType? Find(this ITable<AllType> table, int id)
		{
			return table.FirstOrDefault(e => e.ID == id);
		}

		public static CamelCaseName? Find(this ITable<CamelCaseName> table, int id)
		{
			return table.FirstOrDefault(e => e.Id == id);
		}

		public static DataTypeTest? Find(this ITable<DataTypeTest> table, int dataTypeId)
		{
			return table.FirstOrDefault(e => e.DataTypeID == dataTypeId);
		}

		public static Doctor? Find(this ITable<Doctor> table, int personId)
		{
			return table.FirstOrDefault(e => e.PersonID == personId);
		}

		public static InheritanceChild? Find(this ITable<InheritanceChild> table, int inheritanceChildId)
		{
			return table.FirstOrDefault(e => e.InheritanceChildId == inheritanceChildId);
		}

		public static InheritanceParent? Find(this ITable<InheritanceParent> table, int inheritanceParentId)
		{
			return table.FirstOrDefault(e => e.InheritanceParentId == inheritanceParentId);
		}

		public static Patient? Find(this ITable<Patient> table, int personId)
		{
			return table.FirstOrDefault(e => e.PersonID == personId);
		}

		public static Person? Find(this ITable<Person> table, int personId)
		{
			return table.FirstOrDefault(e => e.PersonID == personId);
		}

		public static SequenceTest? Find(this ITable<SequenceTest> table, int id)
		{
			return table.FirstOrDefault(e => e.ID == id);
		}

		public static TestIdentity? Find(this ITable<TestIdentity> table, int id)
		{
			return table.FirstOrDefault(e => e.ID == id);
		}

		public static TestMerge1? Find(this ITable<TestMerge1> table, int id)
		{
			return table.FirstOrDefault(e => e.Id == id);
		}

		public static TestMerge2? Find(this ITable<TestMerge2> table, int id)
		{
			return table.FirstOrDefault(e => e.Id == id);
		}
		#endregion

		#region Stored Procedures
		#region AddIssue792Record
		public static int AddIssue792Record(this TestDataDB dataConnection)
		{
			return dataConnection.ExecuteProc("\"AddIssue792Record\"");
		}
		#endregion

		#region OutRefEnumTest
		public static IEnumerable<OutRefEnumTestResult> OutRefEnumTest(this TestDataDB dataConnection, string? str, string? inInputoutputstr)
		{
			var parameters = new []
			{
				new DataParameter("STR", str, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("IN_INPUTOUTPUTSTR", inInputoutputstr, DataType.NVarChar)
				{
					Size = 50
				}
			};
			return dataConnection.QueryProc<OutRefEnumTestResult>("\"OutRefEnumTest\"", parameters);
		}

		public partial class OutRefEnumTestResult
		{
			[Column("INPUTOUTPUTSTR")] public string? INPUTOUTPUTSTR { get; set; }
			[Column("OUTPUTSTR"     )] public string? OUTPUTSTR      { get; set; }
		}
		#endregion

		#region OutRefTest
		public static IEnumerable<OutRefTestResult> OutRefTest(this TestDataDB dataConnection, int? id, int? inInputoutputid, string? str, string? inInputoutputstr)
		{
			var parameters = new []
			{
				new DataParameter("ID", id, DataType.Int32)
				{
					Size = 4
				},
				new DataParameter("IN_INPUTOUTPUTID", inInputoutputid, DataType.Int32)
				{
					Size = 4
				},
				new DataParameter("STR", str, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("IN_INPUTOUTPUTSTR", inInputoutputstr, DataType.NVarChar)
				{
					Size = 50
				}
			};
			return dataConnection.QueryProc<OutRefTestResult>("\"OutRefTest\"", parameters);
		}

		public partial class OutRefTestResult
		{
			[Column("INPUTOUTPUTID" )] public int?    INPUTOUTPUTID  { get; set; }
			[Column("INPUTOUTPUTSTR")] public string? INPUTOUTPUTSTR { get; set; }
			[Column("OUTPUTID"      )] public int?    OUTPUTID       { get; set; }
			[Column("OUTPUTSTR"     )] public string? OUTPUTSTR      { get; set; }
		}
		#endregion

		#region PatientSelectAll
		public static IEnumerable<PatientSelectAllResult> PatientSelectAll(this TestDataDB dataConnection)
		{
			return dataConnection.QueryProc<PatientSelectAllResult>("\"Patient_SelectAll\"");
		}

		public partial class PatientSelectAllResult
		{
			[Column("PERSONID"  )] public int?    PERSONID   { get; set; }
			[Column("FIRSTNAME" )] public string? FIRSTNAME  { get; set; }
			[Column("LASTNAME"  )] public string? LASTNAME   { get; set; }
			[Column("MIDDLENAME")] public string? MIDDLENAME { get; set; }
			[Column("GENDER"    )] public string? GENDER     { get; set; }
			[Column("DIAGNOSIS" )] public string? DIAGNOSIS  { get; set; }
		}
		#endregion

		#region PatientSelectByName
		public static IEnumerable<PatientSelectByNameResult> PatientSelectByName(this TestDataDB dataConnection, string? firstname, string? lastname)
		{
			var parameters = new []
			{
				new DataParameter("FIRSTNAME", firstname, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("LASTNAME", lastname, DataType.NVarChar)
				{
					Size = 50
				}
			};
			return dataConnection.QueryProc<PatientSelectByNameResult>("\"Patient_SelectByName\"", parameters);
		}

		public partial class PatientSelectByNameResult
		{
			[Column("PERSONID"  )] public int?    PERSONID   { get; set; }
			[Column("MIDDLENAME")] public string? MIDDLENAME { get; set; }
			[Column("GENDER"    )] public string? GENDER     { get; set; }
			[Column("DIAGNOSIS" )] public string? DIAGNOSIS  { get; set; }
		}
		#endregion

		#region PersonDelete
		public static int PersonDelete(this TestDataDB dataConnection, int? personid)
		{
			var parameters = new []
			{
				new DataParameter("PERSONID", personid, DataType.Int32)
				{
					Size = 4
				}
			};
			return dataConnection.ExecuteProc("\"Person_Delete\"", parameters);
		}
		#endregion

		#region PersonInsert
		public static IEnumerable<PersonInsertResult> PersonInsert(this TestDataDB dataConnection, string? firstname, string? lastname, string? middlename, char? gender)
		{
			var parameters = new []
			{
				new DataParameter("FIRSTNAME", firstname, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("LASTNAME", lastname, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("MIDDLENAME", middlename, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("GENDER", gender, DataType.NChar)
				{
					Size = 1
				}
			};
			return dataConnection.QueryProc<PersonInsertResult>("\"Person_Insert\"", parameters);
		}

		public partial class PersonInsertResult
		{
			[Column("PERSONID")] public int? PERSONID { get; set; }
		}
		#endregion

		#region PersonInsertOutputParameter
		public static IEnumerable<PersonInsertOutputParameterResult> PersonInsertOutputParameter(this TestDataDB dataConnection, string? firstname, string? lastname, string? middlename, char? gender)
		{
			var parameters = new []
			{
				new DataParameter("FIRSTNAME", firstname, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("LASTNAME", lastname, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("MIDDLENAME", middlename, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("GENDER", gender, DataType.NChar)
				{
					Size = 1
				}
			};
			return dataConnection.QueryProc<PersonInsertOutputParameterResult>("\"Person_Insert_OutputParameter\"", parameters);
		}

		public partial class PersonInsertOutputParameterResult
		{
			[Column("PERSONID")] public int? PERSONID { get; set; }
		}
		#endregion

		#region PersonSelectAll
		public static IEnumerable<PersonSelectAllResult> PersonSelectAll(this TestDataDB dataConnection)
		{
			return dataConnection.QueryProc<PersonSelectAllResult>("\"Person_SelectAll\"");
		}

		public partial class PersonSelectAllResult
		{
			[Column("PERSONID"  )] public int?    PERSONID   { get; set; }
			[Column("FIRSTNAME" )] public string? FIRSTNAME  { get; set; }
			[Column("LASTNAME"  )] public string? LASTNAME   { get; set; }
			[Column("MIDDLENAME")] public string? MIDDLENAME { get; set; }
			[Column("GENDER"    )] public string? GENDER     { get; set; }
		}
		#endregion

		#region PersonSelectByKey
		public static IEnumerable<PersonSelectByKeyResult> PersonSelectByKey(this TestDataDB dataConnection, int? id)
		{
			var parameters = new []
			{
				new DataParameter("ID", id, DataType.Int32)
				{
					Size = 4
				}
			};
			return dataConnection.QueryProc<PersonSelectByKeyResult>("\"Person_SelectByKey\"", parameters);
		}

		public partial class PersonSelectByKeyResult
		{
			[Column("PERSONID"  )] public int?    PERSONID   { get; set; }
			[Column("FIRSTNAME" )] public string? FIRSTNAME  { get; set; }
			[Column("LASTNAME"  )] public string? LASTNAME   { get; set; }
			[Column("MIDDLENAME")] public string? MIDDLENAME { get; set; }
			[Column("GENDER"    )] public string? GENDER     { get; set; }
		}
		#endregion

		#region PersonSelectByName
		public static IEnumerable<PersonSelectByNameResult> PersonSelectByName(this TestDataDB dataConnection, string? inFirstname, string? inLastname)
		{
			var parameters = new []
			{
				new DataParameter("IN_FIRSTNAME", inFirstname, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("IN_LASTNAME", inLastname, DataType.NVarChar)
				{
					Size = 50
				}
			};
			return dataConnection.QueryProc<PersonSelectByNameResult>("\"Person_SelectByName\"", parameters);
		}

		public partial class PersonSelectByNameResult
		{
			[Column("PERSONID"  )] public int?    PERSONID   { get; set; }
			[Column("FIRSTNAME" )] public string? FIRSTNAME  { get; set; }
			[Column("LASTNAME"  )] public string? LASTNAME   { get; set; }
			[Column("MIDDLENAME")] public string? MIDDLENAME { get; set; }
			[Column("GENDER"    )] public string? GENDER     { get; set; }
		}
		#endregion

		#region PersonUpdate
		public static int PersonUpdate(this TestDataDB dataConnection, int? personid, string? firstname, string? lastname, string? middlename, char? gender)
		{
			var parameters = new []
			{
				new DataParameter("PERSONID", personid, DataType.Int32)
				{
					Size = 4
				},
				new DataParameter("FIRSTNAME", firstname, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("LASTNAME", lastname, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("MIDDLENAME", middlename, DataType.NVarChar)
				{
					Size = 50
				},
				new DataParameter("GENDER", gender, DataType.NChar)
				{
					Size = 1
				}
			};
			return dataConnection.ExecuteProc("\"Person_Update\"", parameters);
		}
		#endregion

		#region ScalarDataReader
		public static IEnumerable<ScalarDataReaderResult> ScalarDataReader(this TestDataDB dataConnection)
		{
			return dataConnection.QueryProc<ScalarDataReaderResult>("\"Scalar_DataReader\"");
		}

		public partial class ScalarDataReaderResult
		{
			[Column("INTFIELD"   )] public int?    INTFIELD    { get; set; }
			[Column("STRINGFIELD")] public string? STRINGFIELD { get; set; }
		}
		#endregion

		#region ScalarOutputParameter
		public static IEnumerable<ScalarOutputParameterResult> ScalarOutputParameter(this TestDataDB dataConnection)
		{
			return dataConnection.QueryProc<ScalarOutputParameterResult>("\"Scalar_OutputParameter\"");
		}

		public partial class ScalarOutputParameterResult
		{
			[Column("OUTPUTINT"   )] public int?    OUTPUTINT    { get; set; }
			[Column("OUTPUTSTRING")] public string? OUTPUTSTRING { get; set; }
		}
		#endregion

		#region ScalarReturnParameter
		public static IEnumerable<ScalarReturnParameterResult> ScalarReturnParameter(this TestDataDB dataConnection)
		{
			return dataConnection.QueryProc<ScalarReturnParameterResult>("\"Scalar_ReturnParameter\"");
		}

		public partial class ScalarReturnParameterResult
		{
			[Column("RETURN_VALUE")] public int? RETURNVALUE { get; set; }
		}
		#endregion

		#region TestV4Types
		public static IEnumerable<TestV4TypesResult> TestV4Types(this TestDataDB dataConnection, FbZonedDateTime? tstz, FbZonedTime? ttz, FbDecFloat? decfloat16, FbDecFloat? decfloat34, BigInteger? int128)
		{
			var parameters = new []
			{
				new DataParameter("TSTZ", tstz, DataType.DateTimeOffset)
				{
					Size = 12
				},
				new DataParameter("TTZ", ttz, DataType.TimeTZ)
				{
					Size = 8
				},
				new DataParameter("DECFLOAT16", decfloat16, DataType.DecFloat)
				{
					Size = 8
				},
				new DataParameter("DECFLOAT34", decfloat34, DataType.DecFloat)
				{
					Size = 16
				},
				new DataParameter("INT_128", int128, DataType.Int128)
				{
					Size = 16
				}
			};
			return dataConnection.QueryProc<TestV4TypesResult>("TEST_V4_TYPES", parameters);
		}

		public partial class TestV4TypesResult
		{
			[Column("COL_TSTZ"      )] public FbZonedDateTime? COLTSTZ       { get; set; }
			[Column("COL_TTZ"       )] public FbZonedTime?     COLTTZ        { get; set; }
			[Column("COL_DECFLOAT16")] public FbDecFloat?      COLDECFLOAT16 { get; set; }
			[Column("COL_DECFLOAT34")] public FbDecFloat?      COLDECFLOAT34 { get; set; }
			[Column("COL_INT_128"   )] public BigInteger?      COLINT128     { get; set; }
		}
		#endregion
		#endregion
	}

	[Table("CamelCaseName", Schema = "SYSDBA")]
	public partial class CamelCaseName
	{
		[Column("Id"    , IsPrimaryKey = true)] public int     Id    { get; set; } // integer
		[Column("NAME1"                      )] public string? Name1 { get; set; } // varchar(20)
		[Column("Name2"                      )] public string? Name2 { get; set; } // varchar(20)
		[Column("NAME3"                      )] public string? Name3 { get; set; } // varchar(20)
		[Column("_NAME4"                     )] public string? Name4 { get; set; } // varchar(20)
		[Column("NAME 5"                     )] public string? Name5 { get; set; } // varchar(20)
	}

	[Table("Child", Schema = "SYSDBA")]
	public partial class Child
	{
		[Column("ParentID")] public int? ParentID { get; set; } // integer
		[Column("ChildID" )] public int? ChildID  { get; set; } // integer
	}

	[Table("CollatedTable", Schema = "SYSDBA")]
	public partial class CollatedTable
	{
		[Column("Id"             )] public int     Id              { get; set; } // integer
		[Column("CaseSensitive"  )] public string? CaseSensitive   { get; set; } // varchar(20)
		[Column("CaseInsensitive")] public string? CaseInsensitive { get; set; } // varchar(20)
	}

	[Table("DataTypeTest", Schema = "SYSDBA")]
	public partial class DataTypeTest
	{
		[Column("DataTypeID", IsPrimaryKey = true)] public int       DataTypeID { get; set; } // integer
		[Column("Binary_"                        )] public byte[]?   Binary     { get; set; } // blob
		[Column("Boolean_"                       )] public char?     Boolean    { get; set; } // char(1)
		[Column("Byte_"                          )] public short?    Byte       { get; set; } // smallint
		[Column("Bytes_"                         )] public byte[]?   Bytes      { get; set; } // blob
		[Column("CHAR_"                          )] public char?     Char       { get; set; } // char(1)
		[Column("DateTime_"                      )] public DateTime? DateTime   { get; set; } // timestamp
		[Column("Decimal_"                       )] public decimal?  Decimal    { get; set; } // decimal(10,2)
		[Column("Double_"                        )] public double?   Double     { get; set; } // double precision
		[Column("Guid_"                          )] public string?   Guid       { get; set; } // char(16)
		[Column("Int16_"                         )] public short?    Int16      { get; set; } // smallint
		[Column("Int32_"                         )] public int?      Int32      { get; set; } // integer
		[Column("Int64_"                         )] public decimal?  Int64      { get; set; } // numeric(11,0)
		[Column("Money_"                         )] public decimal?  Money      { get; set; } // decimal(18,4)
		[Column("SByte_"                         )] public short?    SByte      { get; set; } // smallint
		[Column("Single_"                        )] public float?    Single     { get; set; } // float
		[Column("Stream_"                        )] public byte[]?   Stream     { get; set; } // blob
		[Column("String_"                        )] public string?   String     { get; set; } // varchar(50)
		[Column("UInt16_"                        )] public short?    UInt16     { get; set; } // smallint
		[Column("UInt32_"                        )] public int?      UInt32     { get; set; } // integer
		[Column("UInt64_"                        )] public decimal?  UInt64     { get; set; } // numeric(11,0)
		[Column("Xml_"                           )] public string?   Xml        { get; set; } // char(1000)
	}

	[Table("Doctor", Schema = "SYSDBA")]
	public partial class Doctor
	{
		[Column("PersonID", IsPrimaryKey = true )] public int    PersonID { get; set; } // integer
		[Column("Taxonomy", CanBeNull    = false)] public string Taxonomy { get; set; } = null!; // varchar(50)

		#region Associations
		/// <summary>
		/// FK_Doctor_Person
		/// </summary>
		[Association(CanBeNull = false, ThisKey = nameof(PersonID), OtherKey = nameof(Firebird.Person.PersonID))]
		public Person Person { get; set; } = null!;
		#endregion
	}

	[Table("Dual", Schema = "SYSDBA")]
	public partial class Dual
	{
		[Column("Dummy")] public string? Dummy { get; set; } // varchar(10)
	}

	[Table("GrandChild", Schema = "SYSDBA")]
	public partial class GrandChild
	{
		[Column("ParentID"    )] public int? ParentID     { get; set; } // integer
		[Column("ChildID"     )] public int? ChildID      { get; set; } // integer
		[Column("GrandChildID")] public int? GrandChildID { get; set; } // integer
	}

	[Table("InheritanceChild", Schema = "SYSDBA")]
	public partial class InheritanceChild
	{
		[Column("InheritanceChildId" , IsPrimaryKey = true)] public int     InheritanceChildId  { get; set; } // integer
		[Column("InheritanceParentId"                     )] public int     InheritanceParentId { get; set; } // integer
		[Column("TypeDiscriminator"                       )] public int?    TypeDiscriminator   { get; set; } // integer
		[Column("Name"                                    )] public string? Name                { get; set; } // varchar(50)
	}

	[Table("InheritanceParent", Schema = "SYSDBA")]
	public partial class InheritanceParent
	{
		[Column("InheritanceParentId", IsPrimaryKey = true)] public int     InheritanceParentId { get; set; } // integer
		[Column("TypeDiscriminator"                       )] public int?    TypeDiscriminator   { get; set; } // integer
		[Column("Name"                                    )] public string? Name                { get; set; } // varchar(50)
	}

	[Table("LinqDataTypes", Schema = "SYSDBA")]
	public partial class LinqDataType
	{
		[Column("ID"            )] public int?      ID             { get; set; } // integer
		[Column("MoneyValue"    )] public decimal?  MoneyValue     { get; set; } // decimal(10,4)
		[Column("DateTimeValue" )] public DateTime? DateTimeValue  { get; set; } // timestamp
		[Column("DateTimeValue2")] public DateTime? DateTimeValue2 { get; set; } // timestamp
		[Column("BoolValue"     )] public char?     BoolValue      { get; set; } // char(1)
		[Column("GuidValue"     )] public string?   GuidValue      { get; set; } // char(16)
		[Column("BinaryValue"   )] public byte[]?   BinaryValue    { get; set; } // blob
		[Column("SmallIntValue" )] public short?    SmallIntValue  { get; set; } // smallint
		[Column("IntValue"      )] public int?      IntValue       { get; set; } // integer
		[Column("BigIntValue"   )] public long?     BigIntValue    { get; set; } // bigint
		[Column("StringValue"   )] public string?   StringValue    { get; set; } // varchar(50)
	}

	[Table("Parent", Schema = "SYSDBA")]
	public partial class Parent
	{
		[Column("ParentID")] public int? ParentID { get; set; } // integer
		[Column("Value1"  )] public int? Value1   { get; set; } // integer
	}

	[Table("Patient", Schema = "SYSDBA")]
	public partial class Patient
	{
		[Column("PersonID" , IsPrimaryKey = true )] public int    PersonID  { get; set; } // integer
		[Column("Diagnosis", CanBeNull    = false)] public string Diagnosis { get; set; } = null!; // varchar(256)

		#region Associations
		/// <summary>
		/// INTEG_14411
		/// </summary>
		[Association(CanBeNull = false, ThisKey = nameof(PersonID), OtherKey = nameof(Firebird.Person.PersonID))]
		public Person Person { get; set; } = null!;
		#endregion
	}

	[Table("Person", Schema = "SYSDBA")]
	public partial class Person
	{
		[Column("PersonID"  , IsPrimaryKey = true )] public int     PersonID   { get; set; } // integer
		[Column("FirstName" , CanBeNull    = false)] public string  FirstName  { get; set; } = null!; // varchar(50)
		[Column("LastName"  , CanBeNull    = false)] public string  LastName   { get; set; } = null!; // varchar(50)
		[Column("MiddleName"                      )] public string? MiddleName { get; set; } // varchar(50)
		[Column("Gender"                          )] public char    Gender     { get; set; } // char(1)

		#region Associations
		/// <summary>
		/// FK_Doctor_Person backreference
		/// </summary>
		[Association(ThisKey = nameof(PersonID), OtherKey = nameof(Firebird.Doctor.PersonID))]
		public Doctor? Doctor { get; set; }

		/// <summary>
		/// INTEG_14411 backreference
		/// </summary>
		[Association(ThisKey = nameof(PersonID), OtherKey = nameof(Patient.PersonID))]
		public Patient? Integ { get; set; }
		#endregion
	}

	[Table("SequenceTest", Schema = "SYSDBA")]
	public partial class SequenceTest
	{
		[Column("ID"    , IsPrimaryKey = true )] public int    ID    { get; set; } // integer
		[Column("Value_", CanBeNull    = false)] public string Value { get; set; } = null!; // varchar(50)
	}

	[Table("TestIdentity", Schema = "SYSDBA")]
	public partial class TestIdentity
	{
		[Column("ID", IsPrimaryKey = true)] public int ID { get; set; } // integer
	}

	[Table("TestMerge1", Schema = "SYSDBA")]
	public partial class TestMerge1
	{
		[Column("Id"             , IsPrimaryKey = true)] public int       Id              { get; set; } // integer
		[Column("Field1"                              )] public int?      Field1          { get; set; } // integer
		[Column("Field2"                              )] public int?      Field2          { get; set; } // integer
		[Column("Field3"                              )] public int?      Field3          { get; set; } // integer
		[Column("Field4"                              )] public int?      Field4          { get; set; } // integer
		[Column("Field5"                              )] public int?      Field5          { get; set; } // integer
		[Column("FieldInt64"                          )] public long?     FieldInt64      { get; set; } // bigint
		[Column("FieldBoolean"                        )] public char?     FieldBoolean    { get; set; } // char(1)
		[Column("FieldString"                         )] public string?   FieldString     { get; set; } // varchar(20)
		[Column("FieldNString"                        )] public string?   FieldNString    { get; set; } // varchar(20)
		[Column("FieldChar"                           )] public char?     FieldChar       { get; set; } // char(1)
		[Column("FieldNChar"                          )] public char?     FieldNChar      { get; set; } // char(1)
		[Column("FieldFloat"                          )] public float?    FieldFloat      { get; set; } // float
		[Column("FieldDouble"                         )] public double?   FieldDouble     { get; set; } // double precision
		[Column("FieldDateTime"                       )] public DateTime? FieldDateTime   { get; set; } // timestamp
		[Column("FieldBinary"                         )] public byte[]?   FieldBinary     { get; set; } // blob
		[Column("FieldGuid"                           )] public string?   FieldGuid       { get; set; } // char(16)
		[Column("FieldDecimal"                        )] public decimal?  FieldDecimal    { get; set; } // decimal(18,10)
		[Column("FieldDate"                           )] public DateTime? FieldDate       { get; set; } // date
		[Column("FieldTime"                           )] public DateTime? FieldTime       { get; set; } // timestamp
		[Column("FieldEnumString"                     )] public string?   FieldEnumString { get; set; } // varchar(20)
		[Column("FieldEnumNumber"                     )] public int?      FieldEnumNumber { get; set; } // integer
	}

	[Table("TestMerge2", Schema = "SYSDBA")]
	public partial class TestMerge2
	{
		[Column("Id"             , IsPrimaryKey = true)] public int       Id              { get; set; } // integer
		[Column("Field1"                              )] public int?      Field1          { get; set; } // integer
		[Column("Field2"                              )] public int?      Field2          { get; set; } // integer
		[Column("Field3"                              )] public int?      Field3          { get; set; } // integer
		[Column("Field4"                              )] public int?      Field4          { get; set; } // integer
		[Column("Field5"                              )] public int?      Field5          { get; set; } // integer
		[Column("FieldInt64"                          )] public long?     FieldInt64      { get; set; } // bigint
		[Column("FieldBoolean"                        )] public char?     FieldBoolean    { get; set; } // char(1)
		[Column("FieldString"                         )] public string?   FieldString     { get; set; } // varchar(20)
		[Column("FieldNString"                        )] public string?   FieldNString    { get; set; } // varchar(20)
		[Column("FieldChar"                           )] public char?     FieldChar       { get; set; } // char(1)
		[Column("FieldNChar"                          )] public char?     FieldNChar      { get; set; } // char(1)
		[Column("FieldFloat"                          )] public float?    FieldFloat      { get; set; } // float
		[Column("FieldDouble"                         )] public double?   FieldDouble     { get; set; } // double precision
		[Column("FieldDateTime"                       )] public DateTime? FieldDateTime   { get; set; } // timestamp
		[Column("FieldBinary"                         )] public byte[]?   FieldBinary     { get; set; } // blob
		[Column("FieldGuid"                           )] public string?   FieldGuid       { get; set; } // char(16)
		[Column("FieldDecimal"                        )] public decimal?  FieldDecimal    { get; set; } // decimal(18,10)
		[Column("FieldDate"                           )] public DateTime? FieldDate       { get; set; } // date
		[Column("FieldTime"                           )] public DateTime? FieldTime       { get; set; } // timestamp
		[Column("FieldEnumString"                     )] public string?   FieldEnumString { get; set; } // varchar(20)
		[Column("FieldEnumNumber"                     )] public int?      FieldEnumNumber { get; set; } // integer
	}

	[Table("PersonView", Schema = "SYSDBA", IsView = true)]
	public partial class PersonView
	{
		[Column("PersonID"  )] public int?    PersonID   { get; set; } // integer
		[Column("FirstName" )] public string? FirstName  { get; set; } // varchar(50)
		[Column("LastName"  )] public string? LastName   { get; set; } // varchar(50)
		[Column("MiddleName")] public string? MiddleName { get; set; } // varchar(50)
		[Column("Gender"    )] public char?   Gender     { get; set; } // char(1)
	}
}
