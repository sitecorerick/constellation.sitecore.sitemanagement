namespace Constellation.Sitecore.SiteManagement
{
    using System.Text;

    /// <summary>
    /// Basic contract for a class used to report on
    /// SiteManager activity.
    /// </summary>
    public interface ISiteManagerTransaction
    {
        /// <summary>
        /// Gets the transaction name.
        /// </summary>
        string TransactionName { get; }

        /// <summary>
        /// Gets the site name.
        /// </summary>
        string SiteName { get; }

        /// <summary>
        /// Gets the site blueprint name.
        /// </summary>
        string SiteBlueprintName { get; }

        /// <summary>
        /// Gets the full site name.
        /// </summary>
        string FullSiteName { get; }

        /// <summary>
        /// Gets the StringBuilder used to log activity.
        /// </summary>
        StringBuilder Log { get; }
    }
}
