namespace Constellation.Sitecore.SiteManagement
{
	using System.Configuration;

	/// <summary>
	/// The site blueprint configuration element.
	/// </summary>
	public class SiteBlueprintConfigurationElement : ConfigurationElement
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		public string Name
		{
			get { return (string)base["name"]; }
			set { base["name"] = value; }
		}

		/// <summary>
		/// Gets the mandatory system folders that need site-specific folders
		/// created when a new site is created.
		/// </summary>
		[ConfigurationProperty("systemFolders", IsRequired = false)]
		public FolderConfigurationCollection SystemFolders
		{
			get
			{
				return (FolderConfigurationCollection)base["systemFolders"];
			}
		}
	}
}
