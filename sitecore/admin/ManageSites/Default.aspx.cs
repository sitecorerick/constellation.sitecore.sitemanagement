namespace Constellation.Sitecore.SiteManagement.UI
{
	using System;
	using System.Diagnostics.CodeAnalysis;

	using global::Sitecore;

	/// <summary>
	///     The default.
	/// </summary>
	public partial class HomePage : SiteManagementBasePage
	{
		#region Methods

		/// <summary>
		/// The on click handler.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
		protected void btnAdd_OnClick([NotNull] object sender, [NotNull] EventArgs e)
		{
			this.Response.Redirect("/sitecore/admin/managesites/AddSite.aspx", false);
		}

		/// <summary>
		/// The on click handler.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
		protected void btnRemove_OnClick([NotNull] object sender, [NotNull] EventArgs e)
		{
			this.Response.Redirect("/sitecore/admin/managesites/RemoveSite.aspx", false);
		}

		#endregion
	}
}