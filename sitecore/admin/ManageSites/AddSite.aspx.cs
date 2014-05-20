namespace Constellation.Sitecore.SiteManagement.UI
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Web.UI.WebControls;
	using global::Sitecore;
	using global::Sitecore.Configuration;
	using global::Sitecore.Data;
	using global::Sitecore.Diagnostics;
	using global::Sitecore.Resources;
	using global::Sitecore.Web.UI;

	/// <summary>
	///     Page for creating new sites.
	/// </summary>
	public partial class AddSite : SiteManagementBasePage
	{
		#region Methods

		/// <summary>
		/// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.txtSiteName.EmptyMessage = "Enter alphanumeric and dashes only ...";
		}

		/// <summary>
		/// Cancel button click handler.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
		protected void btnCancel_OnClick([NotNull] object sender, [NotNull] EventArgs e)
		{
			this.Response.Redirect(this.Context.Request.Url.PathAndQuery, false);
		}

		/// <summary>
		/// Go button click handler
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
		protected void btnGo_OnClick([NotNull] object sender, [NotNull] EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.CreateNewSite();
		}

		/// <summary>
		/// Language Selection changed handler.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
		protected void cblLanguages_OnSelectedIndexChanged([NotNull] object sender, [NotNull] EventArgs e)
		{
			this.ddDefaultLanguage.Items.Clear();

			var selectedItems = from ListItem item in this.cblLanguages.Items where item.Selected select item;

			foreach (var i in selectedItems)
			{
				var item = this.MasterDatabase.GetItem(i.Value);

				this.ddDefaultLanguage.Items.Add(new ListItem(item.DisplayName, item.ID.ToString()));
			}
		}

		/// <summary>
		/// Server-side validation of site name.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="args">The args.</param>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
		protected void cvSiteName_OnServerValidate([NotNull] object source, [NotNull] ServerValidateEventArgs args)
		{
			if (Settings.Sites.Any(s => string.Equals(s.Name, args.Value, StringComparison.OrdinalIgnoreCase)))
			{
				this.cvSiteName.ErrorMessage = "Site Name Already in use";
				args.IsValid = false;
				return;
			}

			var siteItems = this.MasterDatabase.SelectItems("/sitecore/content/sites/*");

			if (siteItems.Any(s => string.Equals(s.Name, args.Value, StringComparison.OrdinalIgnoreCase)))
			{
				this.cvSiteName.ErrorMessage = "Site Name Already in use";
				args.IsValid = false;
			}

			foreach (var c in args.Value)
			{
				if (!char.IsLetterOrDigit(c) && c != '-')
				{
					this.cvSiteName.ErrorMessage = "Site Name is invalid. (Alphanumeric and dashes only)";
					args.IsValid = false;
				}
			}
		}

		/// <summary>
		/// Site Blueprint selection changed handler.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
		protected void ddSiteBlueprint_OnSelectedIndexChanged([NotNull] object sender, [NotNull] EventArgs e)
		{
			// We don't need to do anything here yet.
		}

		/// <summary>
		/// Site Type selection changed handler.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
		protected void ddSiteType_OnSelectedIndexChanged([NotNull] object sender, [NotNull] EventArgs e)
		{
			this.pnlVirtualFolder.Visible = false;
			this.valVirtualFolderName.Enabled = false;

			if (string.Equals(this.ddSiteType.SelectedItem.Text, "Subfolder", StringComparison.InvariantCultureIgnoreCase))
			{
				this.pnlVirtualFolder.Visible = true;
				this.valVirtualFolderName.Enabled = true;
			}
			else
			{
				this.txtVirtualFolderName.Text = string.Empty;
			}
		}

		/// <summary>
		/// Server-side validation of language selection.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="args">The args.</param>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
		protected void valLanguages_OnServerValidate([NotNull] object source, [NotNull] ServerValidateEventArgs args)
		{
			if (this.cblLanguages.Items.Cast<ListItem>().Any(item => item.Selected))
			{
				args.IsValid = true;
				return;
			}

			args.IsValid = false;
		}

		/// <summary>
		/// Handles the Init event of the ddSiteBlueprint control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void SiteBlueprintInit([NotNull] object sender, [NotNull] EventArgs e)
		{
			var section = SiteManagementConfiguration.GetSection();

			Assert.IsNotNull(section, "The 'Constellation/siteManagement' is missing!");

			foreach (SiteBlueprint setting in section.SiteBlueprints)
			{
				this.ddSiteBlueprint.Items.Add(new ListItem(setting.Name));
			}

			if (this.ddSiteBlueprint.Items.Count > 0)
			{
				this.ddSiteBlueprint.SelectedIndex = 0;
			}
		}

		/// <summary>
		/// Languageses the initialize.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected void LanguagesInit([NotNull] object sender, [NotNull] EventArgs e)
		{
			var languages = this.MasterDatabase.SelectItems("/sitecore/system/Languages/*");

			foreach (var lang in languages.OrderBy(l => l.DisplayName))
			{
				var listItem = new ListItem
								   {
									   Text = string.Format("<img src=\"{0}\" />{1}", Images.GetThemedImageSource(this.MasterDatabase.GetItem(lang.ID).Appearance.Icon, ImageDimension.id16x16), lang.DisplayName),
									   Value = lang.ID.ToString()
								   };

				this.cblLanguages.Items.Add(listItem);
			}

			this.cblLanguages.RepeatColumns = 6;
		}

		/// <summary>
		///     Assembles the data for SiteManager and attempts to create a new Site.
		/// </summary>
		private void CreateNewSite()
		{
			IList<ID> languages = (from ListItem item in this.cblLanguages.Items where item.Selected select global::Sitecore.Data.ID.Parse(item.Value)).ToList();

			var newSite = new NewSiteSettings(
				this.txtSiteName.Text,
				this.ddSiteBlueprint.SelectedValue,
				this.ddSiteType.SelectedValue,
				this.txtVirtualFolderName.Text,
				global::Sitecore.Data.ID.Parse(this.ddDefaultLanguage.SelectedValue),
				languages);

			var result = new SiteManager().CreateSite(newSite);

			this.txtResults.Text = result.Log;
			this.txtConfigs.Text = string.Join("\n\n", result.SiteConfigFileChanges);

			this.Results.Text = result.Success ?
					@"<div class=""alert-success""><strong>Success!</strong> Site Created Successfully.</div>" :
					@"<div class=""alert-error""><strong>Error!</strong> Please check log for more information.</div>";
		}

		#endregion
	}
}