namespace Constellation.Sitecore.SiteManagement.UI
{
	using global::Sitecore;
	using global::Sitecore.Configuration;
	using global::Sitecore.Data;
	using System;
	using System.Web.UI;

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
		[NotNull]
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