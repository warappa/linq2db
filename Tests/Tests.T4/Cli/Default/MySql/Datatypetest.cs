// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB.Mapping;
using System;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.Default.MySql
{
	[Table("datatypetest")]
	public class Datatypetest
	{
		[Column("DataTypeID", IsPrimaryKey = true, IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public int       DataTypeId { get; set; } // int
		[Column("Binary_"                                                                                     )] public byte[]?   Binary     { get; set; } // binary(50)
		[Column("Boolean_"                                                                                    )] public bool      Boolean    { get; set; } // bit(1)
		[Column("Byte_"                                                                                       )] public sbyte?    Byte       { get; set; } // tinyint
		[Column("Bytes_"                                                                                      )] public byte[]?   Bytes      { get; set; } // varbinary(50)
		[Column("Char_"                                                                                       )] public char?     Char       { get; set; } // char(1)
		[Column("DateTime_"                                                                                   )] public DateTime? DateTime   { get; set; } // datetime
		[Column("Decimal_"                                                                                    )] public decimal?  Decimal    { get; set; } // decimal(20,2)
		[Column("Double_"                                                                                     )] public float?    Double     { get; set; } // float
		[Column("Guid_"                                                                                       )] public byte[]?   Guid       { get; set; } // varbinary(50)
		[Column("Int16_"                                                                                      )] public short?    Int16      { get; set; } // smallint
		[Column("Int32_"                                                                                      )] public int?      Int32      { get; set; } // int
		[Column("Int64_"                                                                                      )] public long?     Int64      { get; set; } // bigint
		[Column("Money_"                                                                                      )] public decimal?  Money      { get; set; } // decimal(20,4)
		[Column("SByte_"                                                                                      )] public sbyte?    SByte      { get; set; } // tinyint
		[Column("Single_"                                                                                     )] public double?   Single     { get; set; } // double
		[Column("Stream_"                                                                                     )] public byte[]?   Stream     { get; set; } // varbinary(50)
		[Column("String_"                                                                                     )] public string?   String     { get; set; } // varchar(50)
		[Column("UInt16_"                                                                                     )] public short?    UInt16     { get; set; } // smallint
		[Column("UInt32_"                                                                                     )] public int?      UInt32     { get; set; } // int
		[Column("UInt64_"                                                                                     )] public long?     UInt64     { get; set; } // bigint
		[Column("Xml_"                                                                                        )] public string?   Xml        { get; set; } // varchar(1000)
	}
}
