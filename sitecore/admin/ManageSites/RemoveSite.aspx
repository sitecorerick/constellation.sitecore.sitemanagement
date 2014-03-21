<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemoveSite.aspx.cs" Inherits="Constellation.Sitecore.SiteManagement.UI.RemoveSite" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Site Manager</title>
    <style type="text/css">
        .container
        {
            clear: both;
        }

        legend
        {
            font-weight: bold;
            font-size: xx-large;
        }

        div.row-buttons
        {
            padding-left: 20%;
        }

        .row
        {
            margin-top: 10px;
            min-height: 30px;
            clear: both;
        }
    </style>
</head>
<body>
    <form id="formMain" runat="server">
        <telerik:radscriptmanager runat="server" id="RadScriptManager" enablecdn="True" />
        <telerik:radformdecorator id="FormDecorator" runat="server" decoratedcontrols="all" decorationzoneid="container" />

        <!-- Begin page content -->
        <div class="container">
            <div class="row">
                <fieldset>
                    <legend>Remove Site</legend>
                    <label>Site</label>
                    <asp:DropDownList runat="server" ID="ddSites" AutoPostBack="True" OnSelectedIndexChanged="ddSites_OnSelectedIndexChanged" OnInit="SitesInit" />
                </fieldset>
                <p>
                    <asp:Literal runat="server" ID="ltrlSiteInfo" />
                </p>
            </div>
            <div class="row">
                <div class="row-alert">
                    <asp:Literal ID="Results" runat="server" Mode="PassThrough" />
                </div>
                <div class="row-buttons">
                    <telerik:radbutton runat="server" id="btnGo" text="Delete Site" width="150px" usesubmitbehavior="True" causesvalidation="True" enabled="False" onclick="btnGo_OnClick" singleclick="true" singleclicktext="Submitting..." style="float: left; float: left; margin: 10px 10px 10px 10px;" onclientclick="if(!confirm('This action is not reversable! Are you sure you want delete this site?')) { return false; } else { this.disabled = true; this.value = 'Deleting...';}" />
                    <telerik:radbutton runat="server" text="Cancel/Refresh" width="150px" usesubmitbehavior="False" causesvalidation="False" onclick="btnCancel_OnClick" singleclick="true" singleclicktext="Canceling..." style="float: left; float: left; margin: 10px 10px 10px 10px;" />
                </div>
            </div>
            <div class="row">
                <fieldset>
                    <legend>Log:</legend>
                    <asp:TextBox runat="server" ID="txtResults" TextMode="MultiLine" Width="95%" Rows="20" ReadOnly="True" />
                </fieldset>
            </div>
        </div>
    </form>
</body>
</html>
