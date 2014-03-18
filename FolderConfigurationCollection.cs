namespace Constellation.Sitecore.SiteManagement
{
	using System.Configuration;

	/// <summary>
	/// The system folder configuration collection.
	/// </summary>
	[ConfigurationCollection(typeof(FolderConfigurationElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class FolderConfigurationCollection : ConfigurationElementCollection
	{
		/// <summary>
		/// The this.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <returns>
		/// The <see cref="FolderConfigurationElement"/>.
		/// </returns>
		public FolderConfigurationElement this[int index]
		{
			get
			{
				return (FolderConfigurationElement)BaseGet(index);
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
		public new FolderConfigurationElement this[string key]
		{
			get
			{
				return (FolderConfigurationElement)BaseGet(key);
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
			return new FolderConfigurationElement();
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
			return ((FolderConfigurationElement)element).Key;
		}
	}
}
