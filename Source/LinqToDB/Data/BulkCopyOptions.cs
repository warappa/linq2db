﻿using System;

namespace LinqToDB.Data
{
	using Common;
	using Common.Internal;

	/// <param name="MaxBatchSize">
	/// Number of rows in each batch. At the end of each batch, the rows in the batch are sent to the server.
	/// Returns an integer value or zero if no value has been set.
	/// </param>
	/// <param name="BulkCopyTimeout">
	/// Number of seconds for the operation to complete before it times out.
	/// </param>
	/// <param name="BulkCopyType">
	/// Default bulk copy mode, used by <see cref="DataConnectionExtensions.BulkCopy{T}(DataConnection, IEnumerable{T})"/>
	/// methods, if mode is not specified explicitly.
	/// </param>
	/// <param name="CheckConstraints">
	/// Enables database constrains enforcement during bulk copy operation.
	/// Supported with <see cref="BulkCopyType.ProviderSpecific" /> bulk copy mode for following databases:
	/// <list type="bullet">
	/// <item>Oracle</item>
	/// <item>SQL Server</item>
	/// <item>SAP/Sybase ASE</item>
	/// </list>
	/// </param>
	/// <param name="KeepIdentity">
	/// If this option set to true, bulk copy will use values of columns, marked with IsIdentity flag.
	/// SkipOnInsert flag in this case will be ignored.
	/// Otherwise those columns will be skipped and values will be generated by server.
	/// Not compatible with <see cref="BulkCopyType.RowByRow"/> mode.
	/// </param>
	/// <param name="TableLock">
	/// Applies table lock during bulk copy operation.
	/// Supported with <see cref="BulkCopyType.ProviderSpecific" /> bulk copy mode for following databases:
	/// <list type="bullet">
	/// <item>DB2</item>
	/// <item>Informix (using DB2 provider)</item>
	/// <item>SQL Server</item>
	/// <item>SAP/Sybase ASE</item>
	/// </list>
	/// </param>
	/// <param name="KeepNulls">
	/// Enables instert of <c>NULL</c> values instead of values from colum default constraint during bulk copy operation.
	/// Supported with <see cref="BulkCopyType.ProviderSpecific" /> bulk copy mode for following databases:
	/// <list type="bullet">
	/// <item>SQL Server</item>
	/// <item>SAP/Sybase ASE</item>
	/// </list>
	/// </param>
	/// <param name="FireTriggers">
	/// Enables insert triggers during bulk copy operation.
	/// Supported with <see cref="BulkCopyType.ProviderSpecific" /> bulk copy mode for following databases:
	/// <list type="bullet">
	/// <item>Oracle</item>
	/// <item>SQL Server</item>
	/// <item>SAP/Sybase ASE</item>
	/// </list>
	/// </param>
	/// <param name="UseInternalTransaction">
	/// Enables automatic transaction creation during bulk copy operation.
	/// Supported with <see cref="BulkCopyType.ProviderSpecific" /> bulk copy mode for following databases:
	/// <list type="bullet">
	/// <item>Oracle</item>
	/// <item>SQL Server</item>
	/// <item>SAP/Sybase ASE</item>
	/// </list>
	/// </param>
	/// <param name="ServerName">
	/// Gets or sets explicit name of target server instead of one, configured for copied entity in mapping schema.
	/// See <see cref="LinqExtensions.ServerName{T}(ITable{T}, string)"/> method for support information per provider.
	/// Also note that it is not supported by provider-specific insert method.
	/// </param>
	/// <param name="DatabaseName">
	/// Gets or sets explicit name of target database instead of one, configured for copied entity in mapping schema.
	/// See <see cref="LinqExtensions.DatabaseName{T}(ITable{T}, string)"/> method for support information per provider.
	/// </param>
	/// <param name="SchemaName">
	/// Gets or sets explicit name of target schema/owner instead of one, configured for copied entity in mapping schema.
	/// See <see cref="LinqExtensions.SchemaName{T}(ITable{T}, string)"/> method for support information per provider.
	/// </param>
	/// <param name="TableName">
	/// Gets or sets explicit name of target table instead of one, configured for copied entity in mapping schema.
	/// </param>
	/// <param name="TableOptions">
	/// Gets or sets <see cref="LinqToDB.TableOptions"/> flags overrides instead of configured for copied entity in mapping schema.
	/// See <see cref="TableExtensions.IsTemporary{T}(ITable{T}, bool)"/> method for support information per provider.
	/// </param>
	/// <param name="NotifyAfter">
	/// Gets or sets counter after how many copied records <see cref="RowsCopiedCallback"/> should be called.
	/// E.g. if you set it to 10, callback will be called after each 10 copied records.
	/// To disable callback, set this option to 0 (default value).
	/// </param>
	/// <param name="RowsCopiedCallback">
	/// Gets or sets callback method that will be called by BulkCopy operation after each <see cref="NotifyAfter"/> rows copied.
	/// This callback will not be used if <see cref="NotifyAfter"/> set to 0.
	/// </param>
	/// <param name="UseParameters">
	/// Gets or sets whether to Always use Parameters for MultipleRowsCopy. Default is false.
	/// If True, provider's override for <see cref="DataProvider.BasicBulkCopy.MaxParameters"/> will be used to determine the maximum number of rows per insert,
	/// Unless overridden by <see cref="MaxParametersForBatch"/>.
	/// </param>
	/// <param name="MaxParametersForBatch">
	/// If set, will override the Maximum parameters per batch statement from <see cref="DataProvider.BasicBulkCopy.MaxParameters"/>.
	/// </param>
	/// <param name="MaxDegreeOfParallelism">
	/// Implemented only by ClickHouse.Client provider. Defines number of connections, used for parallel insert in <see cref="BulkCopyType.ProviderSpecific"/> mode.
	/// </param>
	/// <param name="WithoutSession">
	/// Implemented only by ClickHouse.Client provider. When set, provider-specific bulk copy will use session-less connection even if called over connection with session.
	/// Note that session-less connections cannot be used with session-bound functionality like temporary tables.
	/// </param>
	/// <summary>
	/// Defines behavior of <see cref="DataConnectionExtensions.BulkCopy{T}(DataConnection, BulkCopyOptions, IEnumerable{T})"/> method.
	/// </summary>
	public sealed record BulkCopyOptions
	(
		int?                        MaxBatchSize           = default,
		int?                        BulkCopyTimeout        = default,
		BulkCopyType                BulkCopyType           = default,
		bool?                       CheckConstraints       = default,
		bool?                       KeepIdentity           = default,
		bool?                       TableLock              = default,
		bool?                       KeepNulls              = default,
		bool?                       FireTriggers           = default,
		bool?                       UseInternalTransaction = default,
		string?                     ServerName             = default,
		string?                     DatabaseName           = default,
		string?                     SchemaName             = default,
		string?                     TableName              = default,
		TableOptions                TableOptions           = default,
		int                         NotifyAfter            = default,
		Action<BulkCopyRowsCopied>? RowsCopiedCallback     = default,
		bool                        UseParameters          = default,
		int?                        MaxParametersForBatch  = default,
		int?                        MaxDegreeOfParallelism = default,
		bool                        WithoutSession         = default
	)
		: IOptionSet
	{
		public BulkCopyOptions() : this((int?)null)
		{
		}

		BulkCopyOptions(BulkCopyOptions options)
		{
			MaxBatchSize           = options.MaxBatchSize;
			BulkCopyTimeout        = options.BulkCopyTimeout;
			BulkCopyType           = options.BulkCopyType;
			CheckConstraints       = options.CheckConstraints;
			KeepIdentity           = options.KeepIdentity;
			TableLock              = options.TableLock;
			KeepNulls              = options.KeepNulls;
			FireTriggers           = options.FireTriggers;
			UseInternalTransaction = options.UseInternalTransaction;
			ServerName             = options.ServerName;
			DatabaseName           = options.DatabaseName;
			SchemaName             = options.SchemaName;
			TableName              = options.TableName;
			TableOptions           = options.TableOptions;
			NotifyAfter            = options.NotifyAfter;
			RowsCopiedCallback     = options.RowsCopiedCallback;
			MaxDegreeOfParallelism = options.MaxDegreeOfParallelism;
			WithoutSession         = options.WithoutSession;
		}

		public static readonly BulkCopyOptions Empty = new();

		int? _configurationID;
		int IConfigurationID.ConfigurationID => _configurationID ??= new IdentifierBuilder()
			.Add(MaxBatchSize)
			.Add(BulkCopyTimeout)
			.Add(BulkCopyType)
			.Add(CheckConstraints)
			.Add(KeepIdentity)
			.Add(TableLock)
			.Add(KeepNulls)
			.Add(FireTriggers)
			.Add(UseInternalTransaction)
			.Add(ServerName)
			.Add(DatabaseName)
			.Add(SchemaName)
			.Add(TableName)
			.Add(TableOptions)
			.Add(NotifyAfter)
			.Add(RowsCopiedCallback)
			.Add(MaxDegreeOfParallelism )
			.Add(WithoutSession)
			.CreateID();

		#region IEquatable implementation

		public bool Equals(BulkCopyOptions? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return ((IOptionSet)this).ConfigurationID == ((IOptionSet)other).ConfigurationID;
		}

		public override int GetHashCode()
		{
			return ((IOptionSet)this).ConfigurationID;
		}

		#endregion
	}
}
