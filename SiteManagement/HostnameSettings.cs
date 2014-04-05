// -----------------------------------------------------------------------------
// <copyright file="HostnameSettings.cs" company="genuine">
//      Copyright (c) @SitecoreRick. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------
namespace Constellation.Sitecore.SiteManagement
{
    using System.Text;

    /// <summary>
    /// The hostname settings.
    /// </summary>
    public class HostnameSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostnameSettings" /> class.
        /// </summary>
        /// <param name="authoring">The authoring server hostname.</param>
        /// <param name="debug">The local debugging hostname.</param>
        /// <param name="delivery">The delivery server hostname.</param>
        /// <param name="jenkins">The continuous integration server hostname.</param>
        /// <param name="qualityControl">The quality control server hostname.</param>
        public HostnameSettings(string authoring, string debug, string delivery, string jenkins, string qualityControl)
        {
            this.Authoring = authoring;
            this.Debug = debug;
            this.Delivery = delivery;
            this.Jenkins = jenkins;
            this.QualityControl = qualityControl;
        }

        /// <summary>
        /// Gets or sets the authoring server hostname for this site.
        /// </summary>
        public string Authoring { get; set; }

        /// <summary>
        /// Gets or sets the Debug server hostname for this site.
        /// </summary>
        public string Debug { get; set; }

        /// <summary>
        /// Gets or sets the delivery server hostname for this site.
        /// </summary>
        public string Delivery { get; set; }

        /// <summary>
        /// Gets or sets the continuous integration server hostname for this site.
        /// </summary>
        public string Jenkins { get; set; }

        /// <summary>
        /// Gets or sets the quality control server hostname for this site.
        /// </summary>
        public string QualityControl { get; set; }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder(256);

            builder.Append("Authoring: ").AppendLine(this.Authoring);
            builder.Append("Debug: ").AppendLine(this.Debug);
            builder.Append("Delivery: ").AppendLine(this.Delivery);
            builder.Append("Jenkins: ").AppendLine(this.Jenkins);
            builder.Append("QualityControl: ").AppendLine(this.QualityControl);

            return builder.ToString();
        }
    }
}