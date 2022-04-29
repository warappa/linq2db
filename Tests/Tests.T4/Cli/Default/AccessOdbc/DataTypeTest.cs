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

namespace Cli.Default.Access.Odbc
{
	[Table("DataTypeTest")]
	public class DataTypeTest
	{
		[Column("DataTypeID", IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public int       DataTypeId { get; set; } // COUNTER
		[Column("Binary_"                                                                )] public byte[]?   Binary     { get; set; } // LONGBINARY
		[Column("Boolean_"                                                               )] public int?      Boolean    { get; set; } // INTEGER
		[Column("Byte_"                                                                  )] public byte?     Byte       { get; set; } // BYTE
		[Column("Bytes_"                                                                 )] public byte[]?   Bytes      { get; set; } // LONGBINARY
		[Column("Char_"                                                                  )] public char?     Char       { get; set; } // VARCHAR(1)
		[Column("DateTime_"                                                              )] public DateTime? DateTime   { get; set; } // DATETIME
		[Column("Decimal_"                                                               )] public decimal?  Decimal    { get; set; } // CURRENCY
		[Column("Double_"                                                                )] public double?   Double     { get; set; } // DOUBLE
		[Column("Guid_"                                                                  )] public Guid?     Guid       { get; set; } // GUID
		[Column("Int16_"                                                                 )] public short?    Int16      { get; set; } // SMALLINT
		[Column("Int32_"                                                                 )] public int?      Int32      { get; set; } // INTEGER
		[Column("Int64_"                                                                 )] public int?      Int64      { get; set; } // INTEGER
		[Column("Money_"                                                                 )] public decimal?  Money      { get; set; } // CURRENCY
		[Column("SByte_"                                                                 )] public byte?     SByte      { get; set; } // BYTE
		[Column("Single_"                                                                )] public float?    Single     { get; set; } // REAL
		[Column("Stream_"                                                                )] public byte[]?   Stream     { get; set; } // LONGBINARY
		[Column("String_"                                                                )] public string?   String     { get; set; } // VARCHAR(50)
		[Column("UInt16_"                                                                )] public short?    UInt16     { get; set; } // SMALLINT
		[Column("UInt32_"                                                                )] public int?      UInt32     { get; set; } // INTEGER
		[Column("UInt64_"                                                                )] public int?      UInt64     { get; set; } // INTEGER
		[Column("Xml_"                                                                   )] public string?   Xml        { get; set; } // LONGCHAR
	}
}
