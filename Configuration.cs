namespace Constellation.Sitecore.SiteManagement
{
	using System.Configuration;

	/// <summary>
	/// The configuration for SiteManager
	/// </summary>
	public class Configuration : ConfigurationSection
	{
		/// <summary>
		/// The config instance for the application instance.
		/// </summary>
		private static Configuration config;

		/// <summary>
		/// Gets the settings.
		/// </summary>
		public static Configuration Settings
		{
			get
			{
				return config ?? (config = ConfigurationManager.GetSection("siteManagement") as Configuration);
			}
		}

		/// <summary>
		/// Gets the roles used by authors that have control of content
		/// across all sites.
		/// </summary>
		/// <remarks>
		/// Examples: "All Site News Author", "All Site News Approver"
		/// </remarks>
		[ConfigurationProperty("transSiteRoles", IsRequired = false)]
		public RoleConfigurationCollection TransSiteRoles
		{
			get
			{
				return (RoleConfigurationCollection)base["transSiteRoles"];
			}
		}

		/// <summary>
		/// Gets the folder and rights kits for various site types.
		/// </summary>
		[ConfigurationProperty("siteBlueprints", IsRequired = false)]
		public SiteBlueprintConfigurationCollection SiteBlueprints
		{
			get
			{
				return (SiteBlueprintConfigurationCollection)base["siteBlueprints"];
			}
		}

		/// <summary>
		/// Gets the target systems.
		/// </summary>
		[ConfigurationProperty("targetSystems", IsRequired = false)]
		public TargetSystemConfigurationCollection TargetSystems
		{
			get
			{
				return (TargetSystemConfigurationCollection)base["targetSystems"];
			}
		}
	}
}
