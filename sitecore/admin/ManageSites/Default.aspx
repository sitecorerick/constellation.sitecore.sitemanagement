<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Constellation.Sitecore.SiteManagement.UI.HomePage" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Site Manager</title>
    <style type="text/css">
        legend
        {
            font-weight: bold;
            font-size: xx-large;
        }

    </style>
</head>
<body>
    <form id="formMain" runat="server">
        <telerik:radscriptmanager runat="server" id="RadScriptManager" enablecdn="True" />
        <telerik:radformdecorator id="FormDecorator" runat="server" decoratedcontrols="all" decorationzoneid="container" />
        <!-- Begin page content -->
        <div class="container">
            <fieldset>
                <legend>Manage Sites</legend>
                <div class="row-buttons">
                    <telerik:radbutton id="btnAdd" onclick="btnAdd_OnClick" runat="server" text="Add Site" width="150px" usesubmitbehavior="False" causesvalidation="False" style="float: left; float: left; margin: 10px 10px 10px 10px;" />
                    <telerik:radbutton id="btnRemove" onclick="btnRemove_OnClick" runat="server" text="Remove Site" width="150px" usesubmitbehavior="False" causesvalidation="False" style="float: left; float: left; margin: 10px 10px 10px 10px;" />
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
