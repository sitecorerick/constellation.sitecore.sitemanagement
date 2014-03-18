namespace Constellation.Sitecore.SiteManagement
{
	using global::Sitecore;

	using global::Sitecore.Configuration;
	using global::Sitecore.Data;
	using global::Sitecore.Data.Fields;
	using global::Sitecore.Data.Items;
	using global::Sitecore.Exceptions;
	using global::Sitecore.SecurityModel;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Text;

	/// <summary>
	/// The site manager.
	/// </summary>
	public class SiteManager
	{
		/// <summary>
		/// The master database.
		/// </summary>
		private Database masterDatabase;

		/// <summary>
		/// Gets the master database.
		/// </summary>
		protected Database MasterDatabase
		{
			get
			{
				return this.masterDatabase ?? (this.masterDatabase = Factory.GetDatabase("master"));
			}
		}

		#region Public Methods
		/// <summary>
		/// The create site.
		/// </summary>
		/// <param name="settings">
		/// The settings.
		/// </param>
		/// <returns>
		/// The <see cref="SiteCreationResults"/>.
		/// </returns>
		public SiteCreationResults CreateSite(NewSiteSettings settings)
		{
			var results = new SiteCreationResults();
			var transaction = new SiteCreationTransaction(settings);

			transaction.Log.AppendLine("SiteManager Create Site started with the following settings:");
			transaction.Log.AppendLine(settings.ToString());
			var timer = new Stopwatch();
			timer.Start();

			try
			{
				// Ensure System Roles
				transaction.Log.AppendLine("Ensuring System Roles exist.");
				RoleFactory.EnsureSystemRoles();

				// Ensure System Folders
				transaction.Log.AppendLine("Ensuring System Folders exist...");
				this.EnsureSystemFolders(settings);

				// Create Site Folders
				transaction.Log.AppendLine("Creating Site Folders...");
				this.CreateSiteFolders(transaction);

				// Generate Site XmlElement text.
				transaction.Log.AppendLine("Creating Site configuration XML...");
				results.SiteConfigFileChanges = this.GenerateSiteConfigs(transaction);

				results.Success = true;
				transaction.Log.AppendLine("Site Creation Successful.");
			}
			catch (Exception ex)
			{
				transaction.Log.AppendLine(ex.Message);
				transaction.RollBack();
				throw new Exception("Create Site failed.", ex);
			}
			finally
			{
				timer.Stop();
				transaction.Log.AppendLine(string.Format("Elapsed Time: {0}ms.", timer.ElapsedMilliseconds));
				WriteLogFile(transaction);
				results.Log = transaction.Log.ToString();
			}

			return results;
		}

		/// <summary>
		/// The delete site.
		/// </summary>
		/// <param name="siteBlueprintName">The site Blueprint Name.</param>
		/// <param name="siteName">The site name.</param>
		/// <returns>
		/// The <see cref="SiteRemovalTransaction" />.
		/// </returns>
		/// <exception cref="System.Exception">Error while removing site.</exception>
		public SiteRemovalTransaction RemoveSite(string siteBlueprintName, string siteName)
		{
			var transaction = new SiteRemovalTransaction(siteBlueprintName, siteName);
			var folderSettings = Configuration.Settings.SiteBlueprints[siteBlueprintName].SystemFolders;

			transaction.Log.AppendLine("Removing site: " + siteName);

			try
			{
				foreach (FolderConfigurationElement setting in folderSettings)
				{
					// delete folders.
					var path = setting.ParentItemPath + "/" + setting.FolderName + "/" + transaction.FullSiteName;
					transaction.Log.AppendLine("Queueing path for deletion: " + path);
					var item = this.MasterDatabase.GetItem(path);

					if (item == null)
					{
						transaction.Log.AppendLine("	Item did not exist.");
						continue;
					}

					item.Delete();
					transaction.Log.AppendLine("	Item deleted.");
				}

				// delete roles
				RoleFactory.RemoveSiteSpecificRoles(transaction);
			}
			catch (Exception ex)
			{
				transaction.Log.AppendLine("==========================");
				transaction.Log.AppendLine(ex.Message);
				transaction.Log.AppendLine(ex.StackTrace);
				transaction.Log.AppendLine("==========================");
				throw new Exception("Error while removing site.", ex);
			}
			finally
			{
				WriteLogFile(transaction);
			}

			return transaction;
		}
		#endregion

		#region Utility Methods
		/// <summary>
		/// Writes the log information to a file.
		/// </summary>
		/// <param name="transaction">
		/// The transaction.
		/// </param>
		private static void WriteLogFile(ISiteManagerTransaction transaction)
		{
			var logFolder = GetLogFilePath();
			var fileName = string.Empty;

			try
			{
				if (!Directory.Exists(logFolder))
				{
					Directory.CreateDirectory(logFolder);
				}

				fileName = string.Format("{0}_{1}_{2}.txt", transaction.TransactionName, transaction.SiteName, DateTime.Now.ToFileTime());

				transaction.Log.AppendFormat("Writing Log File {0} to {1}\n", fileName, logFolder);
				File.WriteAllText(Path.Combine(logFolder, fileName), transaction.Log.ToString());
			}
			catch (UnauthorizedAccessException e)
			{
				transaction.Log.AppendFormat("Failed write log file {0} to {1}\n", fileName, logFolder);
				transaction.Log.AppendLine(e.ToString());
			}
		}

		/// <summary>
		/// Returns the expected location of SiteManager log files.
		/// </summary>
		/// <returns>
		/// The <see cref="Path"/>.
		/// </returns>
		private static string GetLogFilePath()
		{
			return Path.Combine(Settings.DataFolder, "logs", "sitemanager");
		}
		#endregion

		#region Major Plumbing
		/// <summary>
		/// The ensure system folders.
		/// </summary>
		/// <param name="settings">
		/// The settings.
		/// </param>
		private void EnsureSystemFolders(NewSiteSettings settings)
		{
			var folderSettings = Configuration.Settings.SiteBlueprints[settings.SiteBlueprintName].SystemFolders;

			foreach (FolderConfigurationElement setting in folderSettings)
			{
				var systemFolderPath = setting.ParentItemPath + "/" + setting.FolderName;
				var systemFolder = this.MasterDatabase.GetItem(systemFolderPath);

				if (systemFolder != null)
				{
					continue;
				}

				// Make folder item.
				var systemFolderParent = this.MasterDatabase.GetItem(setting.ParentItemPath);

				if (systemFolderParent == null)
				{
					throw new Exception("The required item: " + setting.ParentItemPath + " was not present in the master database. Site creation cannot continue.");
				}

				var systemFolderTemplate = this.MasterDatabase.GetTemplate(new ID(setting.TemplateID));
				var folder = systemFolderParent.Add(setting.FolderName, systemFolderTemplate);

				using (new SecurityDisabler())
				{
					using (new EditContext(folder))
					{
						folder[FieldIDs.Icon] = setting.Icon;

						CheckboxField field = folder.Fields[FieldIDs.ReadOnly];
						field.Checked = setting.ReadOnly;
					}
				}
			}
		}

		/// <summary>
		/// The create site folders.
		/// </summary>
		/// <param name="transaction">
		/// The transaction.
		/// </param>
		private void CreateSiteFolders(SiteCreationTransaction transaction)
		{
			var folderSettings = Configuration.Settings.SiteBlueprints[transaction.Settings.SiteBlueprintName].SystemFolders;

			foreach (FolderConfigurationElement setting in folderSettings)
			{
				var folderPath = setting.ParentItemPath + "/" + setting.FolderName + "/" + transaction.FullSiteName;
				var siteFolder = this.MasterDatabase.GetItem(folderPath);

				if (siteFolder != null)
				{
					transaction.Log.AppendLine("	Folder " + setting.FolderName + " already exists.");
				}
				else
				{
					transaction.Log.AppendLine("	Creating folder: " + folderPath);
					var systemFolderPath = setting.ParentItemPath + "/" + setting.FolderName;
					var systemFolder = this.MasterDatabase.GetItem(systemFolderPath);

					var template = this.MasterDatabase.GetTemplate(new ID(setting.ChildTemplateID));

					if (template != null)
					{
						siteFolder = systemFolder.Add(transaction.FullSiteName, template);
					}
					else
					{
						var branchID = new BranchId(new ID(setting.ChildTemplateID));
						siteFolder = systemFolder.Add(transaction.FullSiteName, branchID);
					}
				}

				// Assign Security
				transaction.Log.AppendLine("	Assigning security...");
				FolderSecurityManager.SetFolderSecurityForSiteFolder(siteFolder, setting, transaction);
			}
		}

		/// <summary>
		/// Creates the XML needed to have Sitecore pick up the site.
		/// </summary>
		/// <param name="transaction">
		/// The transaction.
		/// </param>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		private IEnumerable<string> GenerateSiteConfigs(SiteCreationTransaction transaction)
		{
			var siteConfigs = new List<string>();
			var settings = transaction.Settings;

			/* Normally I'd use some version of an XML writing utility,
			 * but since this is an XML fragment, it's easier to simply
			 * use a StringBuilder.
			 */

			var targetSystems = Configuration.Settings.TargetSystems;

			Item formsFolder = null;
			var rootPath = targetSystems.RootPath.Replace("$blueprint", transaction.SiteBlueprintName).Replace("$site", transaction.FullSiteName);
			var siteName = transaction.FullSiteName;

			if (targetSystems.IncludeFormsRoot)
			{
				// Create the Forms folder.
				formsFolder = this.CreateFormsFolder(rootPath);
			}

			foreach (TargetSystemConfigurationElement system in targetSystems)
			{
				var node = new StringBuilder();
				string language;

				try
				{
					language = settings.DefaultLanguage.Name;

					if (string.IsNullOrEmpty(language))
					{
						throw new InvalidValueException("Iso field cannot be null.");
					}
				}
				catch (SitecoreException ex)
				{
					transaction.Log.AppendLine("Error trying to look up iso field on default language item.");
					transaction.Log.AppendLine(ex.ToString());
					throw new Exception("Could not locate the Language referenced in the site settings.", ex);
				}

				node.AppendLine("<!--");
				node.AppendLine("	Site Definition for " + system.SystemName);
				node.AppendLine("-->");
				node.AppendFormat("<site name=\"{0}\"\n", siteName);
				node.AppendFormat("   hostName=\"{0}\"\n", system.HostNameMask.Replace("$site", siteName));
				node.AppendFormat("   targetHostName=\"{0}\"\n", system.HostNameMask.Replace("$site", siteName));
				node.AppendFormat("   virtualFolder=\"/{0}\"\n", settings.VirtualFolderName);
				node.AppendFormat("   physicalFolder=\"{0}\"\n", targetSystems.PhysicalFolder.Replace("$site", siteName));
				node.AppendFormat("   rootPath=\"{0}\"\n", rootPath);
				node.AppendFormat("   startItem=\"{0}\"\n", targetSystems.StartItem);
				node.AppendFormat("   language=\"{0}\"\n", language);
				node.AppendFormat("   database=\"{0}\"\n", targetSystems.Database);
				node.AppendFormat("   domain=\"{0}\"\n", targetSystems.Domain);
				node.AppendFormat("   allowDebug=\"{0}\"\n", system.AllowDebug);
				node.AppendFormat("   cacheHtml=\"{0}\"\n", system.CacheHtml);
				node.AppendFormat("   htmlCacheSize=\"{0}\"\n", system.HtmlCacheSize);
				node.AppendFormat("   enablePreview=\"{0}\"\n", system.EnablePreview);
				node.AppendFormat("   enableWebEdit=\"{0}\"\n", system.EnableWebEdit);
				node.AppendFormat("   enableDebugger=\"{0}\"\n", system.EnableDebugger);
				node.AppendFormat("   disableClientData=\"{0}\"\n", system.DisableClientData);
				node.AppendFormat("   loginPage=\"{0}\"\n", targetSystems.LoginPage.Replace("$site", siteName));

				if (targetSystems.IncludeFormsRoot)
				{
					// Create the Forms folder.
					// ReSharper disable PossibleNullReferenceException
					node.AppendFormat("   formsRoot=\"{0}\"\n", formsFolder.ID);
					// ReSharper restore PossibleNullReferenceException
				}

				node.AppendLine("   patch:before=\"*[@name='website']\"");
				node.AppendLine("   xdt:Transform=\"Insert\"");
				node.AppendLine("/>");

				siteConfigs.Add(node.ToString());
			}

			return siteConfigs;
		}

		/// <summary>
		/// Creates a WebForms for Marketers Form folder that is unique to the new site.
		/// </summary>
		/// <param name="rootPath">
		/// The root path.
		/// </param>
		/// <returns>
		/// The Form Folder <see cref="Item"/>.
		/// </returns>
		private Item CreateFormsFolder(string rootPath)
		{
			var siteRoot = this.MasterDatabase.GetItem(rootPath);

			// The ID on the next line is the Web Forms for Marketers "Forms Folder" template.
			var formFolderTemplate = this.MasterDatabase.GetTemplate(new ID("{C0A68A37-3C0A-4EEB-8F84-76A7DF7C840E}"));
			return siteRoot.Add("forms", formFolderTemplate);
		}
		#endregion
	}
}
