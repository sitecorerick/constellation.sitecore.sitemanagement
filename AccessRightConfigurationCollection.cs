namespace Constellation.Sitecore.SiteManagement
{
	using System.Configuration;

	/// <summary>
	/// The access right configuration collection.
	/// </summary>
	[ConfigurationCollection(typeof(AccessRightConfigurationElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class AccessRightConfigurationCollection : ConfigurationElementCollection
	{
		/// <summary>
		/// The this.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <returns>
		/// The <see cref="AccessRightConfigurationElement"/>.
		/// </returns>
		public AccessRightConfigurationElement this[int index]
		{
			get
			{
				return (AccessRightConfigurationElement)BaseGet(index);
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
		/// The <see cref="AccessRightConfigurationElement"/>.
		/// </returns>
		public new AccessRightConfigurationElement this[string key]
		{
			get
			{
				return (AccessRightConfigurationElement)BaseGet(key);
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
			return new AccessRightConfigurationElement();
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
			return ((AccessRightConfigurationElement)element).AccessRight;
		}
	}
}
