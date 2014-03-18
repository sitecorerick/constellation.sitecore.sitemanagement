namespace Constellation.Sitecore.SiteManagement
{
	using global::Sitecore.Security.AccessControl;
	using System.Configuration;

	/// <summary>
	/// Defines the access right for a role in the SiteManagement configuration file.
	/// </summary>
	public class AccessRightConfigurationElement : ConfigurationElement
	{
		/// <summary>
		/// Gets or sets the specific access right.
		/// </summary>
		[ConfigurationProperty("accessRight", IsKey = true, IsRequired = true)]
		public string AccessRight
		{
			get { return (string)base["accessRight"]; }
			set { base["accessRight"] = value; }
		}

		/// <summary>
		/// Gets or sets the propagation type for the access right.
		/// </summary>
		[ConfigurationProperty("propagationType", DefaultValue = PropagationType.Any, IsRequired = false)]
		public PropagationType PropagationType
		{
			get { return (PropagationType)base["propagationType"]; }
			set { base["propagationType"] = value; }
		}

		/// <summary>
		/// Gets or sets the access permission for the access right.
		/// </summary>
		[ConfigurationProperty("accessPermission", DefaultValue = AccessPermission.Allow, IsRequired = false)]
		public AccessPermission AccessPermission
		{
			get { return (AccessPermission)base["accessPermission"]; }
			set { base["accessPermission"] = value; }
		}
	}
}
