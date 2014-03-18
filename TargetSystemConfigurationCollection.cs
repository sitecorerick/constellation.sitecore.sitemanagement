namespace Constellation.Sitecore.SiteManagement
{
	using System.Configuration;

	/// <summary>
	/// The target system configuration collection.
	/// </summary>
	[ConfigurationCollection(typeof(TargetSystemConfigurationElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class TargetSystemConfigurationCollection : ConfigurationElementCollection
	{
		/// <summary>
		/// Gets or sets the physical folder path, used when creating site definitions.
		/// Use $site to represent the site name portion of the mask.
		/// </summary>
		[ConfigurationProperty("physicalFolder", DefaultValue = "/", IsRequired = false)]
		public string PhysicalFolder
		{
			get { return (string)base["physicalFolder"]; }
			set { base["physicalFolder"] = value; }
		}

		/// <summary>
		/// Gets or sets the root path. Used when creating site definitions.
		/// Use $site to represent the site name portion of the mask.
		/// </summary>
		[ConfigurationProperty("rootPathMask", DefaultValue = "/sitecore/content/site/$site", IsRequired = true)]
		public string RootPath
		{
			get { return (string)base["rootPathMask"]; }
			set { base["rootPathMask"] = value; }
		}

		/// <summary>
		/// Gets or sets the start item for a site. Used when creating site definitions.
		/// Typically this is "/home".
		/// </summary>
		[ConfigurationProperty("startItem", DefaultValue = "/home", IsRequired = true)]
		public string StartItem
		{
			get { return (string)base["startItem"]; }
			set { base["startItem"] = value; }
		}

		/// <summary>
		/// Gets or sets the database for a site. Used when creating site definitions.
		/// Typically this is "web".
		/// </summary>
		[ConfigurationProperty("database", DefaultValue = "web", IsRequired = false)]
		public string Database
		{
			get { return (string)base["database"]; }
			set { base["database"] = value; }
		}

		/// <summary>
		/// Gets or sets the security domain for a site. Used when creating site definitions.
		/// Typically this is "extranet".
		/// </summary>
		[ConfigurationProperty("domain", DefaultValue = "extranet", IsRequired = false)]
		public string Domain
		{
			get { return (string)base["domain"]; }
			set { base["domain"] = value; }
		}

		/// <summary>
		/// Gets or sets the login page path, used when creating site definitions.
		/// Use $site to represent the site name portion of the mask.
		/// </summary>
		[ConfigurationProperty("loginPage", DefaultValue = "/login.aspx", IsRequired = false)]
		public string LoginPage
		{
			get { return (string)base["loginPage"]; }
			set { base["loginPage"] = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to include the FormsRoot attribute.
		/// Set this to "true" or "false". Set it to true if Web Forms for Marketers is installed.
		/// </summary>
		[ConfigurationProperty("includeFormsRoot", DefaultValue = "false", IsRequired = false)]
		public bool IncludeFormsRoot
		{
			get { return (bool)base["includeFormsRoot"]; }
			set { base["includeFormsRoot"] = value; }
		}

		/// <summary>
		/// The this.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <returns>
		/// The <see cref="TargetSystemConfigurationElement"/>.
		/// </returns>
		public TargetSystemConfigurationElement this[int index]
		{
			get
			{
				return (TargetSystemConfigurationElement)BaseGet(index);
			}

			set
			{
				if (this.BaseGet(index) != null)
				{
					this.BaseRemoveAt(index);
				}

				this.BaseAdd(index, value);
			}
		}

		/// <summary>
		/// The this.
		/// </summary>
		/// <param name="key">
		/// The key.
		/// </param>
		/// <returns>
		/// The <see cref="TargetSystemConfigurationElement"/>.
		/// </returns>
		public new TargetSystemConfigurationElement this[string key]
		{
			get
			{
				return (TargetSystemConfigurationElement)BaseGet(key);
			}
		}

		/// <summary>
		/// The create new element.
		/// </summary>
		/// <returns>
		/// The <see cref="ConfigurationElement"/>.
		/// </returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new TargetSystemConfigurationElement();
		}

		/// <summary>
		/// The get element key.
		/// </summary>
		/// <param name="element">
		/// The element.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TargetSystemConfigurationElement)element).SystemName;
		}
	}
}
