namespace Constellation.Sitecore.SiteManagement
{
	using System.Configuration;

	/// <summary>
	/// The Site Blueprint configuration collection.
	/// </summary>
	[ConfigurationCollection(typeof(SiteBlueprintConfigurationElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class SiteBlueprintConfigurationCollection : ConfigurationElementCollection
	{
		/// <summary>
		/// The this.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <returns>
		/// The <see cref="SiteBlueprintConfigurationElement"/>.
		/// </returns>
		public SiteBlueprintConfigurationElement this[int index]
		{
			get
			{
				return (SiteBlueprintConfigurationElement)BaseGet(index);
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
		/// The <see cref="FolderConfigurationElement"/>.
		/// </returns>
		public new SiteBlueprintConfigurationElement this[string key]
		{
			get
			{
				return (SiteBlueprintConfigurationElement)BaseGet(key);
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
			return new SiteBlueprintConfigurationElement();
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
			return ((SiteBlueprintConfigurationElement)element).Name;
		}
	}
}
