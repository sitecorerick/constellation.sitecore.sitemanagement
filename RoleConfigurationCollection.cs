namespace Constellation.Sitecore.SiteManagement
{
	using System.Configuration;

	/// <summary>
	/// A collection of mandatory Site Roles from the configuration file.
	/// </summary>
	[ConfigurationCollection(typeof(RoleConfigurationElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class RoleConfigurationCollection : ConfigurationElementCollection
	{
		/// <summary>
		/// The this.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <returns>
		/// The <see cref="RoleConfigurationElement"/>.
		/// </returns>
		public RoleConfigurationElement this[int index]
		{
			get
			{
				return (RoleConfigurationElement)BaseGet(index);
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
		/// The <see cref="RoleConfigurationElement"/>.
		/// </returns>
		public new RoleConfigurationElement this[string key]
		{
			get
			{
				return (RoleConfigurationElement)BaseGet(key);
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
			return new RoleConfigurationElement();
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
			return ((RoleConfigurationElement)element).RoleName;
		}
	}
}
