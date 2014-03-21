<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSite.aspx.cs" Inherits="Constellation.Sitecore.SiteManagement.UI.AddSite" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Site Manager</title>
    <style type="text/css">
        .container
        {
            float: none;
            clear: both;
        }

        legend
        {
            font-weight: bold;
            font-size: xx-large;
        }

        .rows
        {
            float: none;
        }

        .row
        {
            margin-top: 10px;
            min-height: 30px;
            clear: both;
        }

        .left-row
        {
            text-align: right;
            font-weight: bold;
            width: 20%;
            float: left;
            margin: 5px 10px 0 0;
        }

        .right-row
        {
            float: left;
            width: 70%;
        }

        label.bold
        {
            font-weight: bold;
        }

        .cbl
        {
            padding-top: 10px;
        }

        td
        {
            width: 85px;
        }

        label img
        {
            vertical-align: central;
            margin-right: 5px;
        }

        div.row-buttons
        {
            padding-left: 20%;
        }

        div.alert-success
        {
            padding-left: 20%;
            margin: 15px 15px 15px 15px;
            color: green;
            font-size: larger;
        }

        div.alert-error
        {
            padding-left: 20%;
            margin: 15px 15px 15px 15px;
            color: red;
            font-size: larger;
        }
    </style>
</head>
<body>
    <form id="formMain" runat="server">
        <telerik:radscriptmanager runat="server" id="RadScriptManager" enablecdn="True" />
        <telerik:radformdecorator id="FormDecorator" runat="server" decoratedcontrols="all" decorationzoneid="container" />
        <!-- Begin page content -->
        <fieldset>
            <legend>Add Site</legend>
            <div class="row">
                <div class="left-row">
                    <label for="txtSiteName">Site Name: </label>
                </div>
                <div class="right-row">
                    <telerik:radtextbox id="txtSiteName" runat="server" width="300" />
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Site Name Required" ForeColor="Red" ControlToValidate="txtSiteName" />
                    <asp:RegularExpressionValidator runat="server" SetFocusOnError="True" ControlToValidate="txtSiteName" ForeColor="Red" ValidationExpression="^[\w-]{1,}$" />
                    <asp:CustomValidator ID="cvSiteName" runat="server" ErrorMessage="Site Name Already in use" ControlToValidate="txtSiteName" ForeColor="Red" OnServerValidate="cvSiteName_OnServerValidate" />
                </div>
            </div>
            <div class="row">
                <div class="left-row">
                    <label>Site Blueprint: </label>
                </div>
                <div class="right-row">
                    <asp:DropDownList ID="ddSiteBlueprint" Width="300" AutoPostBack="True" OnSelectedIndexChanged="ddSiteBlueprint_OnSelectedIndexChanged" runat="server" OnInit="SiteBlueprintInit" />
                    <asp:RequiredFieldValidator ID="valSiteBlueprint" runat="server" ErrorMessage="Site Blueprint Required" ForeColor="Red" ControlToValidate="ddSiteBlueprint" />
                </div>
            </div>
            <div class="row">
                <div class="left-row">
                    <label>Site Type: </label>
                </div>
                <div class="right-row">
                    <asp:DropDownList ID="ddSiteType" AutoPostBack="True" Width="300" OnSelectedIndexChanged="ddSiteType_OnSelectedIndexChanged" runat="server">
                        <Items>
                            <asp:ListItem Text="Root Level" Selected="True" />
                            <asp:ListItem Text="Subfolder" />
                        </Items>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="valSiteType" runat="server" ErrorMessage="Site Type Required" ForeColor="Red" ControlToValidate="ddSiteType" InitialValue="" />
                </div>
                <div class="row" runat="server" id="pnlVirtualFolder" visible="False">
                    <div class="left-row">
                        <label>Subfolder Name:</label>
                    </div>
                    <div class="right-row">
                        <asp:TextBox runat="server" ID="txtVirtualFolderName" Width="300" />
                        <asp:RequiredFieldValidator ID="valVirtualFolderName" runat="server" ErrorMessage="City Site Subfolder Name Required" ForeColor="Red" ControlToValidate="txtVirtualFolderName" Enabled="False" />
                    </div>
                </div>
            </div>
            <!-- Languages -->
            <div class="row">
                <div class="left-row">
                    <label>Languages: </label>
                </div>
                <div class="right-row">
                    <asp:CheckBoxList runat="server" ID="cblLanguages" CssClass="cbl" RepeatColumns="4" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="cblLanguages_OnSelectedIndexChanged" OnInit="LanguagesInit" CellPadding="2" CellSpacing="2" />

                    <asp:CustomValidator runat="server" ID="valLanguages" ErrorMessage="At least one Language is required." ForeColor="Red" OnServerValidate="valLanguages_OnServerValidate" />
                </div>

            </div>
            <div class="row">
                <div class="left-row">
                    <label>Default Language: </label>
                </div>
                <div class="right-row">
                    <asp:DropDownList ID="ddDefaultLanguage" AppendDataBoundItems="True" runat="server" Width="150">
                        <Items>
                            <asp:ListItem Text="" Selected="True" />
                        </Items>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="valDefaultLanguage" runat="server" ErrorMessage="Default Language Required" ForeColor="Red" ControlToValidate="ddDefaultLanguage" InitialValue="" />
                </div>
            </div>
        </fieldset>
        <div class="container">
            <div class="row-alert">
                <asp:Literal ID="Results" runat="server" Mode="PassThrough" />
            </div>
            <div class="row-buttons">
                <telerik:radbutton id="btnGo" runat="server" text="Create Site" width="150px" usesubmitbehavior="True" causesvalidation="True" onclick="btnGo_OnClick" singleclick="true" singleclicktext="Submitting..." style="float: left; float: left; margin: 10px 10px 10px 10px;" />
                <telerik:radbutton id="btnCancel" runat="server" text="Cancel/Refresh" width="150px" usesubmitbehavior="False" causesvalidation="False" onclick="btnCancel_OnClick" singleclick="true" singleclicktext="Canceling..." style="float: left; float: left; margin: 10px 10px 10px 10px;" />
            </div>
        </div>
        <div class="container">
            <fieldset>
                <legend>Steps after site creation:</legend>
                <div class="row-list">
                    <ol>
                        <li>Copy config settings to /App_Settings/Includes/_sites.config</li>
                        <li>Wire up navigation items for the site in Sitecore.</li>
                        <li>Specify the target link for the cookie warning &quot;learn more&quot; link in the Site item.</li>
                    </ol>
                </div>

                <div class="row-config">
                    <div>
                        <label class="bold">Site Creation Log:</label>
                    </div>
                    <div>
                        <asp:TextBox runat="server" ID="txtResults" TextMode="MultiLine" Width="95%" Rows="15" ReadOnly="True" />
                    </div>
                </div>
                <br />
                <div class="row-config">
                    <div>
                        <label class="bold">Site config nodes:</label>
                    </div>
                    <div>
                        <asp:TextBox runat="server" ID="txtConfigs" TextMode="MultiLine" Width="95%" Rows="30" />
                    </div>
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
