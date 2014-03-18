namespace Constellation.Sitecore.SiteManagement
{
	using global::Sitecore.Data.Items;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// The site manager rollback.
	/// </summary>
	public class SiteCreationTransaction : ISiteManagerTransaction
	{
		#region Fields
		/// <summary>
		/// The info logger.
		/// </summary>
		private readonly StringBuilder log = new StringBuilder();
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="SiteCreationTransaction"/> class.
		/// </summary>
		/// <param name="settings">
		/// The settings.
		/// </param>
		public SiteCreationTransaction(NewSiteSettings settings)
		{
			this.Settings = settings;
			this.Roles = new List<string>();
			this.Items = new List<Item>();
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
				return "CreateSite";
			}
		}

		/// <summary>
		/// Gets the items created.
		/// </summary>
		public IList<Item> Items { get; private set; }

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
		/// Gets the roles created.
		/// </summary>
		public IList<string> Roles { get; private set; }

		/// <summary>
		/// Gets the settings.
		/// </summary>
		public NewSiteSettings Settings { get; private set; }

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
		/// Gets the site name.
		/// </summary>
		public string SiteName
		{
			get
			{
				return this.Settings.SiteName;
			}
		}

		/// <summary>
		/// Gets the site blueprint name.
		/// </summary>
		public string SiteBlueprintName
		{
			get
			{
				return this.Settings.SiteBlueprintName;
			}
		}
		#endregion

		/// <summary>
		/// Rolls back all Sitecore Items and Roles that have been created in case of an error in the <see cref="SiteManager"/> process.
		/// </summary>
		public void RollBack()
		{
			this.Log.AppendLine("SiteManagerTransaction beginning rollBack process.");

			if (this.Items != null)
			{
				foreach (var item in this.Items)
				{
					item.Delete();
					this.log.AppendFormat("Deleted Item: {0}\n", item.Paths.FullPath);
				}

				this.Items.Clear();
			}

			RoleFactory.RemoveSiteSpecificRoles(this);

			this.log.AppendLine("SiteManagerTransaction rollBack process completed.");
		}
	}
}