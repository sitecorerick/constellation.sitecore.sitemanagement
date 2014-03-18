<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="8d9e424b-7523-41f3-82be-d339bea30839" namespace="Constellation.Sitecore.SiteManagement.Configuration" xmlSchemaNamespace="urn:Constellation.Sitecore.SiteManagement" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
    <externalType name="System" namespace="Guid" />
    <externalType name="IList&lt;string&gt;" namespace="System.Collections.Generic" />
    <enumeratedType name="PropagationType" namespace="Sitecore.Security.AccessControl" documentation="Sitecore propagation type for the access right" codeGenOptions="None">
      <literals>
        <enumerationLiteral name="Unknown" value="Unknown" />
        <enumerationLiteral name="Descendants" value="Descendants" />
        <enumerationLiteral name="Entity" value="Entity" />
        <enumerationLiteral name="Any" value="Any" />
      </literals>
    </enumeratedType>
    <enumeratedType name="AccessPermission" namespace="Sitecore.Security.AccessControl" codeGenOptions="None">
      <literals>
        <enumerationLiteral name="NotSet" value="NotSet" />
        <enumerationLiteral name="Allow" value="Allow" />
        <enumerationLiteral name="Deny" value="Deny" />
      </literals>
    </enumeratedType>
    <externalType name="Guid" namespace="System" />
    <enumeratedType name="ItemAccessRight" namespace="Constellation.Sitecore.SiteManagement.Configuration" codeGenOptions="None">
      <literals>
        <enumerationLiteral name="Read" />
        <enumerationLiteral name="Create" />
        <enumerationLiteral name="Write" />
        <enumerationLiteral name="Delete" />
        <enumerationLiteral name="Rename" />
      </literals>
    </enumeratedType>
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="SiteManagementConfigurationSection" namespace="Constellation.Sitecore.SiteManagement" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="constellationSiteManagement">
      <elementProperties>
        <elementProperty name="TransSiteRoles" isRequired="false" isKey="false" isDefaultCollection="true" xmlName="transSiteRoles" isReadOnly="false" documentation="The roles used by authors that have control of content across all sites" displayName="Examples: &quot;All Site News Author&quot;, &quot;All Site News Approver&quot;">
          <type>
            <configurationElementCollectionMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/RoleElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="SiteBlueprints" isRequired="false" isKey="false" isDefaultCollection="true" xmlName="siteBlueprints" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/SiteBluePrintConfigurationElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="TargetSystems" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="targetSystems" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/TargetSysteConfigurationElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="RoleElementCollection" collectionType="AddRemoveClearMapAlternate" xmlItemName="role" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/RoleConfigurationElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="RoleConfigurationElement">
      <attributeProperties>
        <attributeProperty name="RoleName" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="roleName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="MemberOfTheseRoles" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="memberOfTheseRoles" isReadOnly="false" documentation="A comma-delimited list of roles. The newly created role will become a member of these roles">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="AccessRights" isRequired="false" isKey="false" isDefaultCollection="true" xmlName="accessRights" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/AccessRightConfigurationElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="SiteBluePrintConfigurationElementCollection" xmlItemName="siteBluePrint" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/SiteBlueprintConfigurationElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="SiteBlueprintConfigurationElement">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="SystemFolders" isRequired="true" isKey="false" isDefaultCollection="true" xmlName="systemFolders" isReadOnly="false" typeConverter="WhiteSpaceTrimStringConverter">
          <type>
            <configurationElementCollectionMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/SystemFoldersConfigurationElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="SystemFolderConfigurationElement">
      <attributeProperties>
        <attributeProperty name="Key" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="key" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="FolderName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="folderName" isReadOnly="false" typeConverter="WhiteSpaceTrimStringConverter">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="TemplateID" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="templateID" isReadOnly="false" defaultValue="&quot;A87A00B1-E6DB-45AB-8B54-636FEC3B5523&quot;">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/Guid" />
          </type>
        </attributeProperty>
        <attributeProperty name="ChildTemplateID" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="childTemplateID" isReadOnly="false" defaultValue="&quot;A87A00B1-E6DB-45AB-8B54-636FEC3B5523&quot;">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/Guid" />
          </type>
        </attributeProperty>
        <attributeProperty name="ParentItemPath" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="parentItemPath" isReadOnly="false" typeConverter="WhiteSpaceTrimStringConverter" defaultValue="&quot;/sitecore/content&quot;">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ReadOnly" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="readOnly" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="Icon" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="icon" isReadOnly="false" defaultValue="&quot;applications/16x16/folder.png&quot;">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="SiteRoles" isRequired="false" isKey="false" isDefaultCollection="true" xmlName="siteLevelRoles" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/RoleElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="AccessRightConfigurationElementCollection" collectionType="AddRemoveClearMapAlternate" xmlItemName="accessRight" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/AccessRightConfigurationElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="AccessRightConfigurationElement">
      <attributeProperties>
        <attributeProperty name="AccessRight" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="accessRight" isReadOnly="false" documentation="Item access right">
          <type>
            <enumeratedTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/ItemAccessRight" />
          </type>
        </attributeProperty>
        <attributeProperty name="PropagationType" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="propagationType" isReadOnly="false" defaultValue="global::Sitecore.Security.AccessControl.PropagationType.Any">
          <type>
            <enumeratedTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/PropagationType" />
          </type>
        </attributeProperty>
        <attributeProperty name="AccessPermission" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="accessPermission" isReadOnly="false" defaultValue="global::Sitecore.Security.AccessControl.AccessPermission.Allow">
          <type>
            <enumeratedTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/AccessPermission" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="SystemFoldersConfigurationElementCollection" collectionType="AddRemoveClearMapAlternate" xmlItemName="systemFolder" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/SystemFolderConfigurationElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="TargetSystemConfigurationElement">
      <attributeProperties>
        <attributeProperty name="SystemName" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="systemName" isReadOnly="false" documentation="The system name">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="HostNameMask" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="hostNameMask" isReadOnly="false" documentation="The host name mask, used when creating site definitions. Use $site to represent the site name portion of the mask." typeConverter="WhiteSpaceTrimStringConverter">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="AllowDebug" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="allowDebug" isReadOnly="false" documentation="The allow debug flag for a site. Used when creating site definitions">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="CacheHtml" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="cacheHtml" isReadOnly="false" documentation="The cache html flag for a site. Used when creating site definitions" defaultValue="true">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="HtmlCacheSize" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="htmlCacheSize" isReadOnly="false" documentation="The html cache size for a site. Used when creating site definitions. Default is 25MB" defaultValue="&quot;25MB&quot;">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="EnablePreview" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="enablePreview" isReadOnly="false" documentation="The enable preview flag for a site. Used when creating site definitions" defaultValue="true">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="EnableWebEdit" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="enableWebEdit" isReadOnly="false" displayName="The enable web edit flag for a site. Used when creating site definitions." defaultValue="true">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="EnableDebugger" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="enableDebugger" isReadOnly="false" documentation="The enable debugger flag for a site. Used when creating site definitions." defaultValue="true">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="DisableClientData" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="disableClientData" isReadOnly="false" documentation="The disable client data flag for a site. Used when creating site definitions." defaultValue="true">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="TargetSysteConfigurationElementCollection" collectionType="AddRemoveClearMapAlternate" xmlItemName="targetSystem" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <attributeProperties>
        <attributeProperty name="PhysicalFolder" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="physicalFolder" isReadOnly="false" documentation="The physical folder path, used when creating site definitions. Use $site to represent the site name portion of the mask." defaultValue="&quot;/&quot;">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="RootPath" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="rootPathMask" isReadOnly="false" documentation="The root path. Used when creating site definitions. Use $site to represent the site name portion of the mask." defaultValue="&quot;/sitecore/content/site/$site&quot;">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="StartItem" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="startItem" isReadOnly="false" documentation="The start item for a site. Used when creating site definitions." defaultValue="&quot;/home&quot;">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Database" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="database" isReadOnly="false" documentation="The database for a site. Used when creating site definitions." defaultValue="&quot;web&quot;">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Domain" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="domain" isReadOnly="false" documentation="The security domain for a site. Used when creating site definitions." defaultValue="&quot;extranet&quot;">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="LoginPage" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="loginPage" isReadOnly="false" documentation="The login page path, used when creating site definitions. Use $site to represent the site name portion of the mask." defaultValue="&quot;/login.aspx&quot;">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="IncludeFormsRoot" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="includeFormsRoot" isReadOnly="false" documentation="The FormsRoot attribute. Set it to true if Web Forms for Marketers is installed.">
          <type>
            <externalTypeMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <itemType>
        <configurationElementMoniker name="/8d9e424b-7523-41f3-82be-d339bea30839/TargetSystemConfigurationElement" />
      </itemType>
    </configurationElementCollection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>