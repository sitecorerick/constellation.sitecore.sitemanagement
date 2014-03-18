namespace Constellation.Sitecore.SiteManagement
{
	using System.Configuration;

	/// <summary>
	/// Details a system level folder and the rules applying to it
	/// when requisitioning a new site.
	/// </summary>
	public class FolderConfigurationElement : ConfigurationElement
	{
		/// <summary>
		/// Gets or sets the key for the folder record.
		/// </summary>
		[ConfigurationProperty("key", IsKey = true, IsRequired = true)]
		public string Key
		{
			get { return (string)base["key"]; }
			set { base["key"] = value; }
		}

		/// <summary>
		/// Gets or sets the folder name.
		/// </summary>
		[ConfigurationProperty("folderName", IsRequired = true)]
		public string FolderName
		{
			get { return (string)base["folderName"]; }
			set { base["folderName"] = value; }
		}

		/// <summary>
		/// Gets or sets the template id to use when creating the folder.
		/// The default is a common folder.
		/// </summary>
		[ConfigurationProperty("templateID", DefaultValue = "{A87A00B1-E6DB-45AB-8B54-636FEC3B5523}", IsRequired = false)]
		public string TemplateID
		{
			get { return (string)base["templateID"]; }
			set { base["templateID"] = value; }
		}

		/// <summary>
		/// Gets or sets the template id to use when creating children of this folder.
		/// The default is a common folder.
		/// </summary>
		[ConfigurationProperty("childTemplateID", DefaultValue = "{A87A00B1-E6DB-45AB-8B54-636FEC3B5523}", IsRequired = false)]
		public string ChildTemplateID
		{
			get { return (string)base["childTemplateID"]; }
			set { base["childTemplateID"] = value; }
		}

		/// <summary>
		/// Gets the folder path.
		/// </summary>
		public string FolderPath
		{
			get
			{
				return this.ParentItemPath + "/" + this.FolderName;
			}
		}

		/// <summary>
		/// Gets or sets the folder's parent item path.
		/// </summary>
		[ConfigurationProperty("parentItemPath", DefaultValue = "/sitecore/content", IsRequired = false)]
		public string ParentItemPath
		{
			get { return (string)base["parentItemPath"]; }
			set { base["parentItemPath"] = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the folder is marked Protected.
		/// </summary>
		[ConfigurationProperty("readOnly", DefaultValue = false, IsRequired = false)]
		public bool ReadOnly
		{
			get { return (bool)base["readOnly"]; }
			set { base["readOnly"] = value; }
		}

		/// <summary>
		/// Gets or sets the icon field value for the folder.
		/// </summary>
		[ConfigurationProperty("icon", DefaultValue = "applications/16x16/folder.png", IsRequired = false)]
		public string Icon
		{
			get { return (string)base["icon"]; }
			set { base["icon"] = value; }
		}

		/// <summary>
		/// Gets the site role rules for this folder.
		/// </summary>
		[ConfigurationProperty("siteLevelRoles", IsDefaultCollection = true)]
		public RoleConfigurationCollection SiteRoles
		{
			get { return (RoleConfigurationCollection)base["siteLevelRoles"]; }
		}
	}
}
