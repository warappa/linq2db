// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB.Mapping;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.Default.PostgreSQL
{
	[Table("LeaveRequestDateEntry")]
	public class LeaveRequestDateEntry
	{
		[Column("Id"            , IsPrimaryKey = true)] public int      Id             { get; set; } // integer
		[Column("EndHour"                            )] public decimal? EndHour        { get; set; } // numeric
		[Column("StartHour"                          )] public decimal? StartHour      { get; set; } // numeric
		[Column("LeaveRequestId"                     )] public int      LeaveRequestId { get; set; } // integer
	}
}
