﻿namespace Constellation.Sitecore.SiteManagement
{
	using System.Text;

	/// <summary>
	/// The site removal results.
	/// </summary>
	public class SiteRemovalTransaction : ISiteManagerTransaction
	{
		#region Fields
		/// <summary>
		/// The log.
		/// </summary>
		private readonly StringBuilder log = new StringBuilder();
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="SiteRemovalTransaction"/> class.
		/// </summary>
		/// <param name="siteBlueprintName">
		/// The site Blueprint Name.
		/// </param>
		/// <param name="siteName">
		/// The site name.
		/// </param>
		public SiteRemovalTransaction(string siteBlueprintName, string siteName)
		{
			this.SiteName = siteName;
			this.SiteBlueprintName = siteBlueprintName;
		}
		#endregion

		#region Properties

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
		/// Gets the full site name.
		/// </summary>
		public string FullSiteName
		{
			get
			{
				return (this.SiteBlueprintName + this.SiteName).Replace(" ", string.Empty).ToLower();
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
		#endregion
	}
}
