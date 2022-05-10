﻿using LinqToDB.CodeModel;
using LinqToDB.Mapping;

namespace LinqToDB.Metadata;

/// <summary>
/// Association (foreign key relation) mapping attributes.
/// </summary>
public sealed class AssociationMetadata
{
	/// <summary>
	/// Specify type of join operation to generate. When <c>true</c>, linq2db will generate outer join orapply operation.
	/// When <c>false</c> - inner join/apply will be generated.
	/// </summary>
	public bool             CanBeNull             { get; set; }
	/// <summary>
	/// AST of code expression, used and value for <see cref="AssociationAttribute.ThisKey"/> property setter.
	/// </summary>
	public ICodeExpression? ThisKeyExpression     { get; set; }
	/// <summary>
	/// AST of code expression, used and value for <see cref="AssociationAttribute.OtherKey"/> property setter.
	/// </summary>
	public ICodeExpression? OtherKeyExpression    { get; set; }
	/// <summary>
	/// Mapping configuration name.
	/// </summary>
	public string?          Configuration         { get; set; }
	/// <summary>
	/// Optional table alias name to use in generated SQL for relation instead of name, generated by linq2db.
	/// </summary>
	public string?          Alias                 { get; set; }
	/// <summary>
	/// Field or property name to use as association data storage in eager load operations with assiation materialization.
	/// </summary>
	public string?          Storage               { get; set; }
	/// <summary>
	/// Comma-separated list of properties/fields, mapped to this (source) side of relation keys, used to generate join condition. Must have same order
	/// ass <see cref="OtherKey"/> value.
	/// </summary>
	public string?          ThisKey               { get; set; }
	/// <summary>
	/// Comma-separated list of properties/fields, mapped to other (target) side of relation keys, used to generate join condition. Must have same order
	/// ass <see cref="ThisKey"/> value.
	/// </summary>
	public string?          OtherKey              { get; set; }
	/// <summary>
	/// Name of static method or property, that provides additional predicate, used by join/apply operation.
	/// </summary>
	public string?          ExpressionPredicate   { get; set; }
	/// <summary>
	/// Name of static method or property, that provides assocation query expression, which will be used for SQL generation instead of
	/// join by foreign key fields.
	/// </summary>
	public string?          QueryExpressionMethod { get; set; }

	// options below not used by linq2db but used by T4 generator so we can use it for backward compatibility
	// even if it doesn't enable any linq2db functionality it could have been used by users
	// for now we plan to obsolete them in v4 and remove or restore later based on feedback
}
