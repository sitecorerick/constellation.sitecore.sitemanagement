namespace Constellation.Sitecore.SiteManagement
{
	using System.Configuration;

	/// <summary>
	/// Details a role that should be created when a new Site is requisitioned.
	/// </summary>
	public class RoleConfigurationElement : ConfigurationElement
	{
		/// <summary>
		/// Gets or sets the root role name to be used
		/// when creating site-specific names.
		/// </summary>
		[ConfigurationProperty("roleName", IsKey = true, IsRequired = true)]
		public string RoleName
		{
			get { return (string)base["roleName"]; }
			set { base["roleName"] = value; }
		}

		/// <summary>
		/// Gets or sets a comma-delimited list of roles. The newly
		/// created role will become a member of these roles.
		/// </summary>
		[ConfigurationProperty("memberOfTheseRoles", DefaultValue = "", IsRequired = false)]
		public string MemberOfTheseRoles
		{
			get { return (string)base["memberOfTheseRoles"]; }
			set { base["memberOfTheseRoles"] = value; }
		}

		/// <summary>
		/// Gets the role's access rights for the containing folder definition.
		/// </summary>
		[ConfigurationProperty("accessRights", IsDefaultCollection = true)]
		public AccessRightConfigurationCollection AccessRights
		{
			get { return (AccessRightConfigurationCollection)base["accessRights"]; }
		}
	}
}
