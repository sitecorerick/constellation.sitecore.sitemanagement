// -----------------------------------------------------------------------------
// <copyright file="RemoveSite.aspx.cs" company="genuine">
//      Copyright (c) @SitecoreRick. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------
namespace Constellation.Sitecore.SiteManagement.UI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.UI.WebControls;

    using global::Sitecore.Configuration;
    using global::Sitecore.Data;

    /// <summary>
    ///     Page for dealing with the Removal of a Site.
    /// </summary>
    public partial class RemoveSite : SiteManagementBasePage
    {
        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        public IList<ID> ItemsToBeRemoved
        {
            get
            {
                var list = (IList<ID>)(this.ViewState["ItemsToBeRemoved"] ?? new List<ID>());
                this.ViewState["ItemsToBeRemoved"] = list;
                return list;
            }

            set
            {
                this.ViewState["ItemsToBeRemoved"] = value;
            }
        }

        /// <summary>
        ///     Gets or sets the site to be removed.
        /// </summary>
        public string SiteToBeRemoved
        {
            get
            {
                var name = (string)(this.ViewState["SiteToBeRemoved"] ?? string.Empty);
                this.ViewState["SiteToBeRemoved"] = name;
                return name;
            }

            set
            {
                this.ViewState["SiteToBeRemoved"] = value;
            }
        }

        /// <summary>
        /// The compute status.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        protected string ComputeStatus(object item)
        {
            string status = "...<span class=\"label label-important\">MISSING</span>";

            if (item != null)
            {
                status = "...<span class=\"label label-success\">FOUND</span>";
            }

            return status;
        }

        /// <summary>
        /// The on click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect(this.Context.Request.Url.PathAndQuery, false);
        }

        /// <summary>
        /// The on click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            var site = Factory.GetSite(this.ddSites.SelectedItem.Text);

            var siteItem = this.MasterDatabase.GetItem(site.RootPath);
            var siteBlueprintItem = siteItem.Parent;

            var manager = new SiteManager();
            var results = manager.RemoveSite(siteBlueprintItem.Name, siteItem.Name);
            this.ltrlSiteInfo.Text = string.Empty;
            this.txtResults.Text = results.Log.ToString();
            this.ltrlSiteInfo.Text = string.Empty;
        }

        /// <summary>
        /// The on selected index changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
        protected void ddSites_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ddSites.SelectedValue))
            {
                this.btnGo.Enabled = false;
                this.ltrlSiteInfo.Text = string.Empty;
                return;
            }

            this.SiteToBeRemoved = this.ddSites.SelectedItem.Text;
            this.btnGo.Enabled = true;
        }

        /// <summary>
        /// Siteses the initialize.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SitesInit(object sender, EventArgs e)
        {
            var sites = Factory.GetSiteInfoList().Where(x => !string.IsNullOrWhiteSpace(x.TargetHostName));
            var list = (DropDownList)sender;
            list.Items.Add(new ListItem("Select ...", string.Empty));
            foreach (var siteInfo in sites)
            {
                list.Items.Add(new ListItem(siteInfo.Name));
            }
        }
    }
}