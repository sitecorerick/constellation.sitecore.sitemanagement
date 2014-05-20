namespace Constellation.Sitecore.SiteManagement
{
	using global::Sitecore;
	using global::Sitecore.Configuration;
	using global::Sitecore.Data;
	using global::Sitecore.Data.Items;
	using global::Sitecore.Exceptions;
	using global::Sitecore.IO;
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
		[NotNull]
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
		/// <param name="settings">The settings.</param>
		/// <returns>
		/// The <see cref="SiteCreationResults" />.
		/// </returns>
		/// <exception cref="System.Exception">Create Site failed.</exception>
		[NotNull]
		public SiteCreationResults CreateSite([NotNull] NewSiteSettings settings)
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
				this.ValidateSystemFolders(settings);

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
		[NotNull]
		public SiteRemovalTransaction RemoveSite([NotNull] string siteBlueprintName, [NotNull] string siteName)
		{
			var transaction = new SiteRemovalTransaction(siteBlueprintName, siteName);

			var section = SiteManagementConfiguration.GetSection();

			global::Sitecore.Diagnostics.Assert.IsNotNull(section, "The 'Constellation/siteManagement' is missing!");
			var folderSettings = section.SiteBlueprints[siteBlueprintName].SiteFolders;

			transaction.Log.AppendLine("Removing site: " + siteName);

			try
			{
				foreach (SiteFolder setting in folderSettings)
				{
					// Only delete folders that were created for the site. Don't delete "system" or "blueprint" folders.
					if (!setting.AddSubFolderForSite)
					{
						continue;
					}

					string path = setting.Path;

					if (!setting.AddSubFolderForBlueprint)
					{
						path = FileUtil.MakePath(path, siteName);
					}

					if (setting.AddSubFolderForBlueprint)
					{
						path = FileUtil.MakePath(FileUtil.MakePath(path, siteBlueprintName), siteName);
					}

					/*
					 * Note that there is no choice for AddSubFolderForBlueprint && !AddSubFolderForSite
					 * This is INTENTIONAL, as we don't want to delete a Blueprint folder, and accidentally
					 * delete ALL the site subfolders within it. We always assume there's some sort of site
					 * delineation or we do nothing.
					 */

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
		private static void WriteLogFile([NotNull] ISiteManagerTransaction transaction)
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
		[NotNull]
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
		private void ValidateSystemFolders([NotNull] NewSiteSettings settings)
		{
			var section = SiteManagementConfiguration.GetSection();

			global::Sitecore.Diagnostics.Assert.IsNotNull(section, "The 'Constellation/siteManagement' is missing!");
			var folderSettings = section.SiteBlueprints[settings.SiteBlueprintName].SiteFolders;

			foreach (SiteFolder setting in folderSettings)
			{
				var systemFolderParent = this.MasterDatabase.GetItem(setting.Path);

				if (systemFolderParent == null)
				{
					throw new Exception("The required item: " + setting.Path + " was not present in the master database. Site creation cannot continue.");
				}
			}
		}

		/// <summary>
		/// The create site folders.
		/// </summary>
		/// <param name="transaction">
		/// The transaction.
		/// </param>
		private void CreateSiteFolders([NotNull] SiteCreationTransaction transaction)
		{
			var section = SiteManagementConfiguration.GetSection();

			global::Sitecore.Diagnostics.Assert.IsNotNull(section, "The 'Constellation/siteManagement' is missing!");
			var folderSettings = section.SiteBlueprints[transaction.Settings.SiteBlueprintName].SiteFolders;

			foreach (SiteFolder setting in folderSettings)
			{
				var systemFolder = this.MasterDatabase.GetItem(setting.Path);

				if (setting.AddSubFolderForBlueprint || setting.AddSubFolderForSite)
				{
					Item siteFolder = null;

					if (setting.AddSubFolderForBlueprint && !setting.AddSubFolderForSite)
					{
						siteFolder = this.CreateItem(systemFolder, setting.SubFolderTemplateID, transaction.SiteBlueprintName);
					}

					if (!setting.AddSubFolderForBlueprint && setting.AddSubFolderForSite)
					{
						siteFolder = this.CreateItem(systemFolder, setting.SubFolderTemplateID, transaction.SiteName);
					}

					if (setting.AddSubFolderForBlueprint && setting.AddSubFolderForSite)
					{
						siteFolder = this.CreateItem(systemFolder, setting.SubFolderTemplateID, transaction.SiteBlueprintName);
						siteFolder = this.CreateItem(siteFolder, setting.SubFolderTemplateID, transaction.SiteName);
					}

					if (siteFolder != null)
					{
						// Assign Security
						transaction.Log.AppendLine("	Assigning security...");
						FolderSecurityManager.SetFolderSecurityForSiteFolder(siteFolder, setting, transaction);
					}
					else
					{
						transaction.Log.AppendLine("WARNING: Folder not found! SubFolderTemplateID - " + setting.SubFolderTemplateID);
					}
				}
				else
				{
					// Assign Security
					transaction.Log.AppendLine("	Assigning security...");
					FolderSecurityManager.SetFolderSecurityForSiteFolder(systemFolder, setting, transaction);
				}
			}
		}

		/// <summary>
		/// The create item.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="templateID">The template id.</param>
		/// <param name="itemName">The item name.</param>
		/// <returns>
		/// The <see cref="Item" />.
		/// </returns>
		[NotNull]
		private Item CreateItem([NotNull] Item parent, Guid templateID, [NotNull] string itemName)
		{
			Item output;

			var template = this.MasterDatabase.GetTemplate(new ID(templateID));
			if (template != null)
			{
				output = parent.Add(itemName, template);
			}
			else
			{
				var branchID = new BranchId(new ID(templateID));
				output = parent.Add(itemName, branchID);
			}

			return output;
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
		[NotNull]
		private IEnumerable<string> GenerateSiteConfigs([NotNull] SiteCreationTransaction transaction)
		{
			var siteConfigs = new List<string>();
			var settings = transaction.Settings;

			/* Normally I'd use some version of an XML writing utility,
			 * but since this is an XML fragment, it's easier to simply
			 * use a StringBuilder.
			 */
			var section = SiteManagementConfiguration.GetSection();

			global::Sitecore.Diagnostics.Assert.IsNotNull(section, "The 'Constellation/siteManagement' is missing!");
			var targetSystems = section.TargetSystems;

			var blueprint = section.SiteBlueprints[transaction.SiteBlueprintName];

			Item formsFolder = null;
			var rootPath = blueprint.SiteRootMask.Replace("$blueprint", transaction.SiteBlueprintName).Replace("$site", transaction.SiteName);
			var siteName = transaction.SiteName;

			if (targetSystems.IncludeFormsRoot)
			{
				// Create the Forms folder.
				formsFolder = this.CreateFormsFolder(rootPath);
			}

			foreach (TargetSystem system in targetSystems)
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
		[NotNull]
		private Item CreateFormsFolder([NotNull] string rootPath)
		{
			var siteRoot = this.MasterDatabase.GetItem(rootPath);

			// The ID on the next line is the Web Forms for Marketers "Forms Folder" template.
			var formFolderTemplate = this.MasterDatabase.GetTemplate(new ID("{C0A68A37-3C0A-4EEB-8F84-76A7DF7C840E}"));
			return siteRoot.Add("forms", formFolderTemplate);
		}

		#endregion
	}
}
