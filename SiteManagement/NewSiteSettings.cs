// -----------------------------------------------------------------------------
// <copyright file="NewSiteSettings.cs" company="genuine">
//      Copyright (c) @SitecoreRick. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------
namespace Constellation.Sitecore.SiteManagement
{
    using System.Collections.Generic;
    using System.Text;
    using global::Sitecore.Configuration;
    using global::Sitecore.Data;
    using global::Sitecore.Globalization;

    /// <summary>
    /// The new site settings.
    /// </summary>
    public class NewSiteSettings
    {
        /// <summary>
        /// The default language id.
        /// </summary>
        private ID defaultLanguageID;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewSiteSettings"/> class.
        /// </summary>
        public NewSiteSettings()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewSiteSettings" /> class.
        /// </summary>
        /// <param name="siteName">The site name.</param>
        /// <param name="siteBlueprintName">The site Blueprint Name.</param>
        /// <param name="siteType">The site type.</param>
        /// <param name="virtualFolderName">The city subfolder name.</param>
        /// <param name="defaultLanguageID">The default language id.</param>
        /// <param name="additionalLanguages">The additional languages.</param>
        public NewSiteSettings(string siteName, string siteBlueprintName, string siteType, string virtualFolderName, ID defaultLanguageID, IEnumerable<ID> additionalLanguages)
        {
            this.SiteName = siteName;
            this.SiteBlueprintName = siteBlueprintName;
            this.SiteType = siteType;
            this.VirtualFolderName = virtualFolderName;
            this.AdditionalLanguages = additionalLanguages;
            this.DefaultLanguageID = defaultLanguageID;
        }

        /// <summary>
        /// Gets or sets the site name.
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets the name of the Blueprint to use when creating the site.
        /// </summary>
        public string SiteBlueprintName { get; set; }

        /// <summary>
        /// Gets or sets the site type.
        /// </summary>
        public string SiteType { get; set; }

        /// <summary>
        /// Gets or sets the virtual subfolder name.
        /// </summary>
        public string VirtualFolderName { get; set; }

        /// <summary>
        /// Gets or sets the additional languages.
        /// </summary>
        public IEnumerable<ID> AdditionalLanguages { get; set; }

        /// <summary>
        /// Gets the default language.
        /// </summary>
        public Language DefaultLanguage { get; private set; }

        /// <summary>
        /// Gets or sets the default language id.
        /// </summary>
        public ID DefaultLanguageID
        {
            get
            {
                return this.defaultLanguageID;
            }

            set
            {
                this.defaultLanguageID = value;

                var masterDatabase = Factory.GetDatabase("master");
                var defaultLangItem = masterDatabase.GetItem(this.defaultLanguageID);
                var defaultLanguage = Language.Parse(defaultLangItem.Name);
                this.DefaultLanguage = defaultLanguage;
            }
        }

        /// <summary>
        /// Returns a diagnostic representation of the values in this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder(256);
            builder.Append("SiteName: ").AppendLine(this.SiteName);
            builder.Append("VirtualFolderName: ").AppendLine(this.VirtualFolderName);
            builder.Append("DefaultLanguage: ").AppendLine(this.DefaultLanguage.Name);
            builder.Append("AdditionalLanguages: ").AppendLine(string.Join(", ", this.AdditionalLanguages));

            return builder.ToString();
        }
    }
}