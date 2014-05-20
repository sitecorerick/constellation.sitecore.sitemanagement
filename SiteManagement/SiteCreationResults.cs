// -----------------------------------------------------------------------------
// <copyright file="SiteCreationResults.cs" company="genuine">
//      Copyright (c) @SitecoreRick. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------
namespace Constellation.Sitecore.SiteManagement
{
	using System.Collections.Generic;

	using global::Sitecore;

	/// <summary>
	/// The new site results.
	/// </summary>
	public class SiteCreationResults
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SiteCreationResults"/> class.
		/// </summary>
		public SiteCreationResults()
		{
			this.Log = string.Empty;
			this.Success = false;
		}

		/// <summary>
		/// Gets or sets the log.
		/// </summary>
		[NotNull]
		public string Log { get; set; }

		/// <summary>
		/// Gets or sets the site nodes for the Sitecore site configuration section.
		/// </summary>
		[NotNull]
		public IEnumerable<string> SiteConfigFileChanges { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether success.
		/// </summary>
		public bool Success { get; set; }
	}
}