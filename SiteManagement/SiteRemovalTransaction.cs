// -----------------------------------------------------------------------------
// <copyright file="SiteRemovalTransaction.cs" company="genuine">
//      Copyright (c) @SitecoreRick. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------
namespace Constellation.Sitecore.SiteManagement
{
	using System.Text;

	using global::Sitecore;

	/// <summary>
	/// The site removal results.
	/// </summary>
	public class SiteRemovalTransaction : ISiteManagerTransaction
	{
		/// <summary>
		/// The log.
		/// </summary>
		private readonly StringBuilder log = new StringBuilder(1024);

		/// <summary>
		/// Initializes a new instance of the <see cref="SiteRemovalTransaction"/> class.
		/// </summary>
		/// <param name="siteBlueprintName">
		/// The site Blueprint Name.
		/// </param>
		/// <param name="siteName">
		/// The site name.
		/// </param>
		public SiteRemovalTransaction([NotNull] string siteBlueprintName, [NotNull] string siteName)
		{
			this.SiteName = siteName;
			this.SiteBlueprintName = siteBlueprintName;
		}

		/// <summary>
		/// Gets the transaction name.
		/// </summary>
		public string TransactionName
		{
			get
			{
				return "RemoveSite";
			}
		}

		/// <summary>
		/// Gets the log.
		/// </summary>
		public StringBuilder Log
		{
			get
			{
				return this.log;
			}
		}

		/// <summary>
		/// Gets the site name.
		/// </summary>
		public string SiteName { get; private set; }

		/// <summary>
		/// Gets the site blueprint name.
		/// </summary>
		public string SiteBlueprintName { get; private set; }
	}
}
