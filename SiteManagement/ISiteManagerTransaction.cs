// -----------------------------------------------------------------------------
// <copyright file="ISiteManagerTransaction.cs" company="genuine">
//      Copyright (c) @SitecoreRick. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------
namespace Constellation.Sitecore.SiteManagement
{
	using System.Text;

	using global::Sitecore;

	/// <summary>
	/// Basic contract for a class used to report on
	/// SiteManager activity.
	/// </summary>
	public interface ISiteManagerTransaction
	{
		/// <summary>
		/// Gets the transaction name.
		/// </summary>
		[NotNull]
		string TransactionName { get; }

		/// <summary>
		/// Gets the site name.
		/// </summary>
		[NotNull]
		string SiteName { get; }

		/// <summary>
		/// Gets the site blueprint name.
		/// </summary>
		[NotNull]
		string SiteBlueprintName { get; }

		/// <summary>
		/// Gets the StringBuilder used to log activity.
		/// </summary>
		[NotNull]
		StringBuilder Log { get; }
	}
}
