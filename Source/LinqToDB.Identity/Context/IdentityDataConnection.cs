﻿using LinqToDB.Configuration;
using Microsoft.AspNetCore.Identity;

namespace LinqToDB.Identity
{
	/// <summary>
	/// Base class for the LinqToDB database context used for identity.
	/// </summary>
	public class IdentityDataConnection : IdentityDataConnection<IdentityUser, IdentityRole, string>
	{
		/// <summary>
		/// Constructor with options.
		/// </summary>
		/// <param name="options">Connection options.</param>
		public IdentityDataConnection(LinqToDBConnectionOptions options)
			: base(options)
		{
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public IdentityDataConnection()
		{
		}
	}
}
