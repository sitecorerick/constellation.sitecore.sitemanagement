// -----------------------------------------------------------------------------
// <copyright file="FolderSecurityManager.cs" company="genuine">
//      Copyright (c) @SitecoreRick. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------
namespace Constellation.Sitecore.SiteManagement
{

	using global::Sitecore.Data.Items;
	using global::Sitecore.Security.AccessControl;
	using global::Sitecore.SecurityModel;

	/// <summary>
	/// Creates Folder Security settings based upon the app config and the site 
	/// being created.
	/// </summary>
	public class FolderSecurityManager
	{
		/// <summary>
		/// Updates the Security attributes for the folder supplied. Creates or updates
		/// Site-specific roles required based upon the app config file.
		/// </summary>
		/// <param name="folder">
		/// The folder that needs security.
		/// </param>
		/// <param name="config">
		/// The configuration element for this folder.
		/// </param>
		/// <param name="transaction">
		/// The transaction.
		/// </param>
		public static void SetFolderSecurityForSiteFolder(Item folder, SiteFolder config, ISiteManagerTransaction transaction)
		{
			var siteRoleSettings = config.SiteRoles;

			foreach (RoleRule roleSetting in siteRoleSettings)
			{
				// Ensure the role exists
				var roleName = RoleFactory.AssembleRoleName(roleSetting.RoleName, transaction.SiteBlueprintName, transaction.SiteName);
				transaction.Log.AppendLine("Creating role: " + roleName);

				var role = RoleFactory.AddOrUpdateRole(roleName, transaction, RoleFactory.ParseRoleList(roleSetting.MemberOfTheseRoles));

				// Assign security to the folder
				transaction.Log.AppendLine("		Assigning security to folder: " + folder.Paths.FullPath + " for role: " + roleName);
				var rules = folder.Security.GetAccessRules();

				foreach (AccessRightRuleSetting right in roleSetting.AccessRights)
				{
					transaction.Log.AppendLine("		" + right.AccessRight);
					rules.Helper.AddAccessPermission(role, AccessRight.FromName(string.Concat("item:", right.AccessRight).ToLowerInvariant()), right.PropagationType, right.AccessPermission);
				}

				using (new SecurityDisabler())
				{
					using (new EditContext(folder))
					{
						folder.Security.SetAccessRules(rules);
					}
				}
			}
		}
	}
}