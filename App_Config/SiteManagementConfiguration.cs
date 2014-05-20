namespace Constellation.Sitecore.SiteManagement
{
	using System;
	using System.Configuration;

	using global::Sitecore;
	using global::Sitecore.Diagnostics;

	/// <summary>
	/// The SiteManagementConfiguration Configuration Section.
	/// </summary>
	public partial class SiteManagementConfiguration
	{
		/// <summary>
		/// Gets the section.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The Constellation SiteManagement</returns>
		[NotNull]
		public static SiteManagementConfiguration GetSection([NotNull] string path = "Constellation/siteManagement")
		{
			/* Constellation path  */
			var section = (SiteManagementConfiguration)ConfigurationManager.GetSection(path);
			if (section != null)
			{
				return section;
			}

			/* Default CSD path */
			section = SiteManagementConfiguration.Instance;
			if (section != null)
			{
				return section;
			}

			var ex = new ArgumentException("SiteManagementConfiguration");
			Log.Fatal("The config section :" + path + "not found!", ex);

			throw ex;
		}
	}
}
