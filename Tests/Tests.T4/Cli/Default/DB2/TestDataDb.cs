// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB.Configuration;
using LinqToDB.Data;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.Default.DB2
{
	public partial class TestDataDB : DataConnection
	{
		#region Schemas
		public void InitSchemas()
		{
			Db2Admin = new Db2AdminSchema.DataContext(this);
		}

		public Db2AdminSchema.DataContext Db2Admin { get; set; } = null!;
		#endregion

		public TestDataDB()
		{
			InitSchemas();
			InitDataContext();
		}

		public TestDataDB(string configuration)
			: base(configuration)
		{
			InitSchemas();
			InitDataContext();
		}

		public TestDataDB(LinqToDBConnectionOptions<TestDataDB> options)
			: base(options)
		{
			InitSchemas();
			InitDataContext();
		}

		partial void InitDataContext();
	}
}
