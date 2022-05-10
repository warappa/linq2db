﻿using System.Linq.Expressions;

namespace LinqToDB.Linq;

using SqlQuery;
using Mapping;
using Common.Internal.Cache;

static partial class QueryRunner
{
	public static class InsertWithIdentity<T>
	{
		static Query<object> CreateQuery(
			IDataContext           dataContext,
			EntityDescriptor       descriptor,
			T                      obj,
			InsertColumnFilter<T>? columnFilter,
			string?                tableName,
			string?                serverName,
			string?                databaseName,
			string?                schemaName,
			TableOptions           tableOptions,
			Type                   type)
		{
			var sqlTable = new SqlTable(dataContext.MappingSchema, type);

			if (tableName != null || schemaName != null || databaseName != null || databaseName != null)
			{
				sqlTable.TableName = new(
					          tableName    ?? sqlTable.TableName.Name,
					Server  : serverName   ?? sqlTable.TableName.Server,
					Database: databaseName ?? sqlTable.TableName.Database,
					Schema  : schemaName   ?? sqlTable.TableName.Schema);
			}

			if (tableOptions.IsSet()) sqlTable.TableOptions = tableOptions;

			var sqlQuery        = new SelectQuery();
			var insertStatement = new SqlInsertStatement(sqlQuery)
			{
				Insert = { Into = sqlTable, WithIdentity = true }
			};

			var ei = new Query<object>(dataContext, null)
			{
				Queries = { new QueryInfo { Statement = insertStatement, } }
			};

			foreach (var field in sqlTable.Fields.Where(x => columnFilter == null || columnFilter(obj, x.ColumnDescriptor)))
			{
				if (field.IsInsertable && !field.ColumnDescriptor.ShouldSkip(obj!, descriptor, SkipModification.Insert))
				{
					var param = GetParameter(type, dataContext, field);
					ei.Queries[0].AddParameterAccessor(param);

					insertStatement.Insert.Items.Add(new SqlSetExpression(field, param.SqlParameter));
				}
				else if (field.IsIdentity)
				{
					var sqlb = dataContext.CreateSqlProvider();
					var expr = sqlb.GetIdentityExpression(sqlTable);

					if (expr != null)
						insertStatement.Insert.Items.Add(new SqlSetExpression(field, expr));
				}
			}

			SetScalarQuery(ei);

			return ei;
		}

		public static object Query(
			IDataContext           dataContext,
			T                      obj,
			InsertColumnFilter<T>? columnFilter,
			string?                tableName,
			string?                serverName,
			string?                databaseName,
			string?                schemaName,
			TableOptions           tableOptions)
		{
			if (Equals(default(T), obj))
				return 0;

			var type             = GetType<T>(obj!, dataContext);
			var entityDescriptor = dataContext.MappingSchema.GetEntityDescriptor(type);
			var ei               = Common.Configuration.Linq.DisableQueryCache || entityDescriptor.SkipModificationFlags.HasFlag(SkipModification.Insert) || columnFilter != null
				? CreateQuery(dataContext, entityDescriptor, obj!, columnFilter, tableName, serverName, databaseName, schemaName, tableOptions, type)
				: Cache<T>.QueryCache.GetOrCreate(
					(operation: "II", dataContext.MappingSchema.ConfigurationID, dataContext.ContextID, tableName, schemaName, databaseName, serverName, tableOptions, type, dataContext.GetQueryFlags()),
					new { dataContext, entityDescriptor, obj },
					static (entry, key, context) =>
					{
						entry.SlidingExpiration = Common.Configuration.Linq.CacheSlidingExpiration;
						return CreateQuery(context.dataContext, context.entityDescriptor, context.obj, null, key.tableName, key.serverName, key.databaseName, key.schemaName, key.tableOptions, key.type);
					});

			return ei.GetElement(dataContext, Expression.Constant(obj), null, null)!;
		}

		public static async Task<object> QueryAsync(
			IDataContext           dataContext,
			T                      obj,
			InsertColumnFilter<T>? columnFilter,
			string?                tableName,
			string?                serverName,
			string?                databaseName,
			string?                schemaName,
			TableOptions           tableOptions,
			CancellationToken      token)
		{
			if (Equals(default(T), obj))
				return 0;

			var type             = GetType<T>(obj!, dataContext);
			var entityDescriptor = dataContext.MappingSchema.GetEntityDescriptor(type);
			var ei               = Common.Configuration.Linq.DisableQueryCache || entityDescriptor.SkipModificationFlags.HasFlag(SkipModification.Insert) || columnFilter != null
				? CreateQuery(dataContext, entityDescriptor, obj!, columnFilter, tableName, serverName, databaseName, schemaName, tableOptions, type)
				: Cache<T>.QueryCache.GetOrCreate(
					(operation: "II", dataContext.MappingSchema.ConfigurationID, dataContext.ContextID, tableName, schemaName, databaseName, serverName, tableOptions, type, dataContext.GetQueryFlags()),
					new { dataContext, entityDescriptor, obj },
					static (entry, key, context) =>
					{
						entry.SlidingExpiration = Common.Configuration.Linq.CacheSlidingExpiration;
						return CreateQuery(context.dataContext, context.entityDescriptor, context.obj, null, key.tableName, key.serverName, key.databaseName, key.schemaName, key.tableOptions, key.type);
					});

			return await ((Task<object>)ei.GetElementAsync(dataContext, Expression.Constant(obj), null, null, token)!).ConfigureAwait(Common.Configuration.ContinueOnCapturedContext);
		}
	}
}
