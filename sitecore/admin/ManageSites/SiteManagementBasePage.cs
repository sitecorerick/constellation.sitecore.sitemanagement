// -----------------------------------------------------------------------------
// <copyright file="HostnameSettings.cs" company="genuine">
//      Copyright (c) @SitecoreRick. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------
namespace Constellation.Sitecore.SiteManagement.UI
{
    using System;
    using System.Web.UI;
    using global::Sitecore.Configuration;
    using global::Sitecore.Data;

    /// <summary>
    /// The site management base page.
    /// </summary>
    public abstract class SiteManagementBasePage : Page
    {
        /// <summary>
        ///     Private instance value for the Sitecore master database. Do not use directly.
        /// </summary>
        private Database masterDatabase;

        /// <summary>
        ///     Gets the master database.
        /// </summary>
        protected Database MasterDatabase
        {
            get
            {
                return this.masterDatabase ?? (this.masterDatabase = Factory.GetDatabase("master"));
            }
        }

        /// <summary>
        /// The on initialization handler.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnInit(EventArgs e)
        {
            this.Server.ScriptTimeout = 1000;

            if (!this.Context.IsDebuggingEnabled)
            {
                // Redirect the user if they are not authorized to be here.
                if (!global::Sitecore.Context.User.IsAdministrator)
                {
                    // ReSharper disable Html.PathError
                    this.Response.Redirect("/sitecore", true);

                    // ReSharper restore Html.PathError
                }
            }
        }
    }
}