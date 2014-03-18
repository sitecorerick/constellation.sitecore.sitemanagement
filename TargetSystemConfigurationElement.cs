namespace Constellation.Sitecore.SiteManagement
{
    using System.Configuration;

    /// <summary>
    /// The target system configuration element.
    /// </summary>
    public class TargetSystemConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the system name.
        /// </summary>
        [ConfigurationProperty("systemName", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string SystemName
        {
            get { return (string)base["systemName"]; }
            set { base["systemName"] = value; }
        }

        /// <summary>
        /// Gets or sets the host name mask, used when creating site definitions.
        /// Use $site to represent the site name portion of the mask.
        /// </summary>
        [ConfigurationProperty("hostNameMask", DefaultValue = "", IsRequired = true)]
        public string HostNameMask
        {
            get { return (string)base["hostNameMask"]; }
            set { base["hostNameMask"] = value; }
        }

        /// <summary>
        /// Gets or sets the allow debug flag for a site. Used when creating site definitions.
        /// Set this to "true" or "false".
        /// </summary>
        [ConfigurationProperty("allowDebug", DefaultValue = "true", IsRequired = false)]
        public string AllowDebug
        {
            get { return (string)base["allowDebug"]; }
            set { base["allowDebug"] = value; }
        }

        /// <summary>
        /// Gets or sets the cache html flag for a site. Used when creating site definitions.
        /// Set this to "true" or "false".
        /// </summary>
        [ConfigurationProperty("cacheHtml", DefaultValue = "true", IsRequired = false)]
        public string CacheHtml
        {
            get { return (string)base["cacheHtml"]; }
            set { base["cacheHtml"] = value; }
        }

        /// <summary>
        /// Gets or sets the html cache size for a site. Used when creating site definitions.
        /// Default is 25MB.
        /// </summary>
        [ConfigurationProperty("htmlCacheSize", DefaultValue = "25MB", IsRequired = false)]
        public string HtmlCacheSize
        {
            get { return (string)base["htmlCacheSize"]; }
            set { base["htmlCacheSize"] = value; }
        }

        /// <summary>
        /// Gets or sets the enable preview flag for a site. Used when creating site definitions.
        /// Set this to "true" or "false".
        /// </summary>
        [ConfigurationProperty("enablePreview", DefaultValue = "true", IsRequired = false)]
        public string EnablePreview
        {
            get { return (string)base["enablePreview"]; }
            set { base["enablePreview"] = value; }
        }

        /// <summary>
        /// Gets or sets the enable web edit flag for a site. Used when creating site definitions.
        /// Set this to "true" or "false".
        /// </summary>
        [ConfigurationProperty("enableWebEdit", DefaultValue = "true", IsRequired = false)]
        public string EnableWebEdit
        {
            get { return (string)base["enableWebEdit"]; }
            set { base["enableWebEdit"] = value; }
        }

        /// <summary>
        /// Gets or sets the enable debugger flag for a site. Used when creating site definitions.
        /// Set this to "true" or "false".
        /// </summary>
        [ConfigurationProperty("enableDebugger", DefaultValue = "true", IsRequired = false)]
        public string EnableDebugger
        {
            get { return (string)base["enableDebugger"]; }
            set { base["enableDebugger"] = value; }
        }

        /// <summary>
        /// Gets or sets the disable client data flag for a site. Used when creating site definitions.
        /// Set this to "true" or "false".
        /// </summary>
        [ConfigurationProperty("disableClientData", DefaultValue = "true", IsRequired = false)]
        public string DisableClientData
        {
            get { return (string)base["disableClientData"]; }
            set { base["disableClientData"] = value; }
        }
    }
}
