﻿namespace Constellation.Sitecore.SiteManagement.UI
{
	using global::Sitecore;
	using global::Sitecore.Configuration;
	using global::Sitecore.Data;
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Web.UI.WebControls;

	/// <summary>
	///     Page for dealing with the Removal of a Site.
	/// </summary>
	public partial class RemoveSite : SiteManagementBasePage
	{
		/// <summary>
		///     Gets or sets the items.
		/// </summary>
		[NotNull]
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
		[NotNull]
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
		[NotNull]
		protected string ComputeStatus([NotNull] object item)
		{
			string status = "...<span class=\"label label-important\">MISSING</span>";

			// ReSharper disable ConditionIsAlwaysTrueOrFalse
			if (item != null)
			// ReSharper restore ConditionIsAlwaysTrueOrFalse
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
		protected void btnCancel_OnClick([NotNull] object sender, [NotNull] EventArgs e)
		{
			this.Response.Redirect(this.Context.Request.Url.PathAndQuery, false);
		}

		/// <summary>
		/// The on click.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
		protected void btnGo_OnClick([NotNull] object sender, [NotNull] EventArgs e)
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
		protected void ddSites_OnSelectedIndexChanged([NotNull] object sender, [NotNull] EventArgs e)
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
		protected void SitesInit([NotNull] object sender, [NotNull] EventArgs e)
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