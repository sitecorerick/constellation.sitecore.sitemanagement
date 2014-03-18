namespace Constellation.Sitecore.SiteManagement
{
    using global::Sitecore.Security.Accounts;
    using global::Sitecore.StringExtensions;
    using System;
    using System.Collections.Generic;
    using System.Web.Security;

    /// <summary>
    /// Handles the creation or removal of roles for Site Manager.
    /// </summary>
    public class RoleFactory
    {
        /// <summary>
        /// Adds a role to Sitecore.
        /// </summary>
        /// <param name="roleName">The role Name.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="parentRoles">Optional role to nest.</param>
        /// <returns>
        /// The newly created <see cref="Role" />.
        /// </returns>
        public static Role AddOrUpdateRole(string roleName, ISiteManagerTransaction transaction = null, IEnumerable<Role> parentRoles = null)
        {
            Role role;
            if (Role.Exists(roleName))
            {
                role = Role.FromName(roleName);
                if (transaction != null)
                {
                    transaction.Log.AppendLine("	Role already exists.");
                }
            }
            else
            {
                Roles.CreateRole(roleName);
                role = Role.FromName(roleName);
                if (transaction != null)
                {
                    transaction.Log.AppendLine("	Role created.");
                }
            }

            if (parentRoles == null)
            {
                return role;
            }

            foreach (var parentRole in parentRoles)
            {
                RolesInRolesManager.AddRoleToRole(role, parentRole);
            }

            return role;
        }

        /// <summary>
        /// The safe role name.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public static string EnsureSafeRoleName(string roleName)
        {
            var array = roleName.ToCharArray();

            array = Array.FindAll(array, c => (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)));
            return new string(array);
        }

        /// <summary>
        /// The fully qualified role name
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <param name="domain">The domain.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public static string EnsureFullyQualifiedRoleName(string roleName, string domain = "sitecore")
        {
            var safeName = EnsureSafeRoleName(roleName);

            if (roleName.Contains("\\"))
            {
                return safeName;
            }

            return domain + "\\" + safeName;
        }

        /// <summary>
        /// The ensure system roles.
        /// </summary>
        public static void EnsureSystemRoles()
        {
            var settings = Configuration.Settings.TransSiteRoles;

            foreach (RoleConfigurationElement setting in settings)
            {
                AddOrUpdateRole(EnsureFullyQualifiedRoleName(setting.RoleName), parentRoles: ParseRoleList(setting.MemberOfTheseRoles));
            }
        }

        /// <summary>
        /// Converts a comma delimited string of parent roles to a list of roles.
        /// </summary>
        /// <param name="parentRoleAttribute">The parent role attribute of the RoleConfigurationElement.</param>
        /// <param name="siteBlueprintName">The site Blueprint Name.</param>
        /// <param name="siteName">The site Name.</param>
        /// <returns>
        /// The parent roles, if they exist.
        /// </returns>
        public static IEnumerable<Role> ParseRoleList(string parentRoleAttribute, string siteBlueprintName = null, string siteName = null)
        {
            var parentRoles = new List<Role>();

            if (parentRoleAttribute.IsNullOrEmpty())
            {
                return parentRoles;
            }

            var parentNames = parentRoleAttribute.Split(',');

            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (var parentName in parentNames)
            // ReSharper restore LoopCanBeConvertedToQuery
            {
                if (parentName.IsNullOrEmpty())
                {
                    continue;
                }

                var role = AddOrUpdateRole(AssembleRoleName(parentName, siteBlueprintName, siteName));

                if (role != null)
                {
                    parentRoles.Add(role);
                }
            }

            return parentRoles;
        }

        /// <summary>
        /// Removes site-specific roles from the system, if they exist.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        public static void RemoveSiteSpecificRoles(ISiteManagerTransaction transaction)
        {
            var folderSettings = Configuration.Settings.SiteBlueprints[transaction.SiteBlueprintName].SystemFolders;

            foreach (FolderConfigurationElement folderSetting in folderSettings)
            {
                var siteRoleSettings = folderSetting.SiteRoles;

                foreach (RoleConfigurationElement roleSetting in siteRoleSettings)
                {
                    var roleName = AssembleRoleName(roleSetting.RoleName, transaction.SiteBlueprintName, transaction.SiteName);
                    transaction.Log.AppendLine("Queuing role for deletion: " + roleName);

                    if (Role.Exists(roleName))
                    {
                        Roles.DeleteRole(roleName);
                        transaction.Log.AppendLine("	Role deleted.");
                    }
                    else
                    {
                        transaction.Log.AppendLine("	Role not found.");
                    }
                }
            }
        }

        /// <summary>
        /// The assemble role name.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <param name="blueprintName">The blueprint name.</param>
        /// <param name="siteName">The site name.</param>
        /// <param name="domain">The domain.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public static string AssembleRoleName(
            string roleName,
            string blueprintName = null,
            string siteName = null,
            string domain = "sitecore")
        {
            var output = roleName;

            if (!string.IsNullOrEmpty(blueprintName))
            {
                output = output.Replace("$blueprint", blueprintName);
            }

            if (!string.IsNullOrEmpty(siteName))
            {
                output = output.Replace("$site", siteName);
            }

            output = EnsureSafeRoleName(output);

            return EnsureFullyQualifiedRoleName(output, domain);
        }
    }
}
