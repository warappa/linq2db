﻿using LinqToDB.CodeModel;
using LinqToDB.DataModel;
using LinqToDB.Naming;
using LinqToDB.Schema;
using LinqToDB.SqlQuery;

namespace LinqToDB.Scaffold;

/// <summary>
/// Implements database schema load and conversion to data model.
/// </summary>
public sealed partial class DataModelLoader
{
	private static readonly TypeMapping _unmappedType = new(WellKnownTypes.System.Object, null);

	private record TableWithEntity(TableLikeObject TableOrView, EntityModel Entity);

	// language-specific naming services for initial normalization of identifiers in data model
	// (generation of valid identifiers without name conflicts resolution)
	private readonly NamingServices         _namingServices;
	/// language services provider
	private readonly ILanguageProvider      _languageProvider;
	// database schema provider
	private readonly ISchemaProvider        _schemaProvider;
	// database to .net type mapping provider
	private readonly ITypeMappingProvider   _typeMappingsProvider;

	// lookups for created data model objects:
	// entity model lookup by schema table/view object (e.g. for conversion of foreign keys to associations)
	private readonly Dictionary<SqlObjectName, TableWithEntity>   _entities = new ();
	// column model lookup
	private readonly Dictionary<EntityModel, Dictionary<string, ColumnModel>> _columns  = new ();

	// various settings to customize scaffolding process
	private readonly ScaffoldOptions        _options;
	private readonly ScaffoldInterceptors   _interceptors;

	public DataModelLoader(
		NamingServices         namingServices,
		ILanguageProvider      languageProvider,
		ISchemaProvider        schemaProvider,
		ITypeMappingProvider   typeMappingsProvider,
		ScaffoldOptions        options,
		ScaffoldInterceptors?  interceptors)
	{
		_namingServices       = namingServices;
		_languageProvider     = languageProvider;
		_schemaProvider       = schemaProvider;
		_typeMappingsProvider = typeMappingsProvider;
		_options              = options;
		_interceptors   = interceptors ?? NoOpScaffoldInterceptors.Instance;
	}

	/// <summary>
	/// Loads database schema into <see cref="DatabaseModel"/> object.
	/// </summary>
	/// <returns>Loaded database model instance.</returns>
	public DatabaseModel LoadSchema()
	{
		// create empty data model and set initial options
		var dataContext             = BuildDataContext();
		var model                   = new DatabaseModel(dataContext);
		model.NRTEnabled            = _languageProvider.NRTSupported && _options.CodeGeneration.EnableNullableReferenceTypes;
		model.DisableXmlDocWarnings = _languageProvider.MissingXmlCommentWarnCodes.Length > 0 && _options.CodeGeneration.SuppressMissingXmlDocWarnings;

		// parse user-specified open-generic type for association
		if (!_options.DataModel.AssociationCollectionAsArray && _options.DataModel.AssociationCollectionType != null)
		{
			model.AssociationCollectionType = _languageProvider.TypeParser.Parse(_options.DataModel.AssociationCollectionType, false);
			if (model.AssociationCollectionType is not OpenGenericType associationType || associationType.OpenGenericArgCount != 1)
				throw new InvalidOperationException($"{nameof(DataModelOptions)}.{nameof(DataModelOptions.AssociationCollectionType)} must be open generic type with one type argument (was: {_options.DataModel.AssociationCollectionType})");
		}

		if (_options.CodeGeneration.MarkAsAutoGenerated)
		{
			// default header
			model.AutoGeneratedHeader = _options.CodeGeneration.AutoGeneratedHeader ?? @"This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.";
		}

		// base type for entities (if specified)
		IType? baseEntityType = null;
		if (_options.DataModel.BaseEntityClass != null)
			baseEntityType = _languageProvider.TypeParser.Parse(_options.DataModel.BaseEntityClass, false);

		// list of default database schemas (objects in those schemas will be added to main data context)
		var defaultSchemas = _schemaProvider.GetDefaultSchemas();
		// load enabled database objects and convert them to data model

		// load tables as entities
		if (_options.Schema.LoadedObjects.HasFlag(SchemaObjects.Table))
		{
			foreach (var table in _interceptors.GetTables(_schemaProvider.GetTables()))
				BuildEntity(dataContext, table, defaultSchemas, baseEntityType);
		}

		// load views as entities
		if (_options.Schema.LoadedObjects.HasFlag(SchemaObjects.View))
		{
			foreach (var view in _interceptors.GetViews(_schemaProvider.GetViews()))
				BuildEntity(dataContext, view, defaultSchemas, baseEntityType);
		}

		// load foreign keys as associations
		if (_options.Schema.LoadedObjects.HasFlag(SchemaObjects.ForeignKey))
		{
			Dictionary<(SqlObjectName from, SqlObjectName to), List<ISet<ForeignKeyColumnMapping>>>? duplicateFKs = null;

			foreach (var fk in _interceptors.GetForeignKeys(_schemaProvider.GetForeignKeys()))
			{
				// detect and skip duplicate foreign keys
				if (_options.Schema.IgnoreDuplicateForeignKeys)
				{
					var currentKeyColumns = new HashSet<ForeignKeyColumnMapping>(fk.Relation);

					if (duplicateFKs != null && duplicateFKs.TryGetValue((fk.Source, fk.Target), out var keys))
					{
						var isDuplicate = false;

						foreach (var knowKey in keys)
						{
							if (knowKey.Count == currentKeyColumns.Count)
							{
								var keysDiffer = false;
								foreach (var pair in currentKeyColumns)
								{
									if (!knowKey.Contains(pair))
									{
										keysDiffer = true;
										break;
									}
								}

								if (!keysDiffer)
								{
									isDuplicate = true;
									break;
								}
							}
						}

						// skip duplicate key
						if (isDuplicate)
							continue;
					}
					else
						(duplicateFKs ??= new()).Add((fk.Source, fk.Target), new() { currentKeyColumns });
				}

				var association = BuildAssociations(fk, defaultSchemas);
				if (association != null)
				{
					_interceptors.PreprocessAssociation(_languageProvider.TypeParser, association);
					dataContext.Associations.Add(association);
				}
			}
		}

		// load stored procedures
		if (_options.Schema.LoadedObjects.HasFlag(SchemaObjects.StoredProcedure))
		{
			foreach (var proc in _interceptors.GetProcedures(_schemaProvider.GetProcedures(_options.Schema.LoadProceduresSchema, _options.Schema.UseSafeSchemaLoad)))
				BuildStoredProcedure(dataContext, proc, defaultSchemas);
		}

		// load table functions
		if (_options.Schema.LoadedObjects.HasFlag(SchemaObjects.TableFunction))
		{
			foreach (var func in _interceptors.GetTableFunctions(_schemaProvider.GetTableFunctions()))
				BuildTableFunction(dataContext, func, defaultSchemas);
		}

		// load scalar functions
		if (_options.Schema.LoadedObjects.HasFlag(SchemaObjects.ScalarFunction))
		{
			foreach (var func in _interceptors.GetScalarFunctions(_schemaProvider.GetScalarFunctions()))
				BuildScalarFunction(dataContext, func, defaultSchemas);
		}

		// load aggregate functions
		if (_options.Schema.LoadedObjects.HasFlag(SchemaObjects.AggregateFunction))
		{
			foreach (var func in _interceptors.GetAggregateFunctions(_schemaProvider.GetAggregateFunctions()))
				BuildAggregateFunction(dataContext, func, defaultSchemas);
		}

		return model;
	}

	private readonly Dictionary<DatabaseType, TypeMapping> _typeResolveCache = new();
	private TypeMapping MapType(DatabaseType databaseType)
	{
		if (_typeResolveCache.TryGetValue(databaseType, out var mapping))
		{
			return mapping;
		}

		mapping = _typeMappingsProvider.GetTypeMapping(databaseType);
		mapping = _interceptors.GetTypeMapping(databaseType, _languageProvider.TypeParser, mapping);
		if (mapping == null)
		{
			Console.Error.WriteLine($"Database type {databaseType} cannot be mapped to know .NET type and will be mapped to System.Object. You can specify .NET type for this database type manually using {nameof(ScaffoldInterceptors)}.{nameof(ScaffoldInterceptors.GetTypeMapping)} interceptor");
			mapping = _unmappedType;
		}

		_typeResolveCache.Add(databaseType, mapping);

		return mapping;
	}
}
