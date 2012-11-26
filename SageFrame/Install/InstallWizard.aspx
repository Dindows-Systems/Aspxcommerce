<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InstallWizard.aspx.cs" Inherits="Install_InstallWizard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head">
    <link type="icon shortcut" media="icon" href="favicon.ico" />
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type" />
    <meta content="text/javascript" http-equiv="Content-Script-Type" />
    <meta content="text/css" http-equiv="Content-Style-Type" />
    <meta id="MetaRefresh" runat="Server" http-equiv="Refresh" name="Refresh" />
    <meta id="MetaDescription" runat="Server" name="DESCRIPTION" />
    <meta id="MetaKeywords" runat="Server" name="KEYWORDS" />
    <meta id="MetaCopyright" runat="Server" name="COPYRIGHT" />
    <meta id="MetaGenerator" runat="Server" name="GENERATOR" />
    <meta id="MetaAuthor" runat="Server" name="AUTHOR" />
    <meta name="RESOURCE-TYPE" content="DOCUMENT" />
    <meta name="DISTRIBUTION" content="GLOBAL" />
    <meta id="MetaRobots" runat="server" name="ROBOTS" />
    <meta name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />
    <meta http-equiv="PAGE-ENTER" content="RevealTrans(Duration=0,Transition=1)" />
    <link href="~/Install/css/install.css" rel="stylesheet" type="text/css" />
    <!--[if IE]> <link href="~/Install/css/IE.css" rel="stylesheet" type="text/css" /> <![endif]-->

    <noscript>
        <asp:Label ID="lblnoScript" runat="server" Text="This page requires java-script to be enabled. Please adjust your browser-settings."></asp:Label></noscript>
    <title>SageFrame Website Installation</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/SageFrameWebService.asmx" />
        </Services>
    </asp:ScriptManager>
    <div id="sfInstallWrapper">
        <div class="sfLogo">
            <h1>
                <asp:Label ID="lblTitle" runat="server" />
            </h1>
            <div class="sfVersion">
                <asp:Label ID="lblVersion" runat="server" />
            </div>
        </div>
        <div class="sfOuter sfCurve">
            <div class="sfInner sfCurve">
                <asp:Label ID="lblInstallError" runat="server" Visible="false" />
                <asp:HiddenField ID="hdnConnectionStringForAll" runat="server" Value="" />
                <asp:HiddenField ID="hdnNextButtonClientID" runat="server" Value="0" />
                <asp:Label ID="lblPermissionsError" runat="server" CssClass="cssClasssNormalRed"
                    EnableViewState="false" Visible="false" />
                <asp:Label ID="lblDataBaseError" runat="server" CssClass="cssClasssNormalRed" EnableViewState="false" />
                <asp:Label ID="lblRequiredDatabaseName" runat="server" CssClass="cssClasssNormalRed"
                    EnableViewState="false" />
                <asp:Panel ID="pnlStartInstall" runat="server">
                    <div class="sfInstallpart">
                        <div class="sfFormwrapper">
                            <table id="tblDatabase" runat="Server" cellpadding="0" cellspacing="0" border="0"
                                width="100%">
                                <tr>
                                    <td>
                                        <h2>
                                            Database Credentials</h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFileName" runat="Server" CssClass="sfFormlabel" />
                                    </td>
                                </tr>
                                <tr class="sfTdseperator">
                                    <td>
                                        <asp:Label ID="lblServer" runat="Server" CssClass="sfFormlabel" />
                                        <asp:TextBox ID="txtServer" runat="Server" CssClass="sfInputbox" />
                                        <asp:Label ID="lblServerHelp" runat="Server" CssClass="sfHelptext" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label id="lblServerError" class="sfError">
                                        </label>
                                    </td>
                                </tr>
                                <tr class="sfTdseperator">
                                    <td>
                                        <asp:Label ID="lblIntegrated" runat="Server" CssClass="sfFormlabel" />
                                        <asp:CheckBox ID="chkIntegrated" runat="Server" AutoPostBack="True" OnCheckedChanged="chkIntegrated_CheckedChanged"
                                            CssClass="sfCheckBox" />
                                        <asp:Label ID="lblIntegratedHelp" runat="Server" CssClass="sfHelptext sfInline" />
                                    </td>
                                </tr>
                                <tr id="trDatabaseName" runat="server" visible="false" class="sfTdseperator">
                                    <td>
                                        <h2>
                                            Database Name</h2>
                                        <asp:Label ID="lblDatabase" runat="Server" CssClass="sfFormlabel" Text="Data Base:" />
                                        <asp:TextBox ID="txtDataBase" runat="Server" CssClass="sfInputbox" />
                                        <asp:Label ID="lblDatabaseNameHelp" runat="Server" CssClass="sfHelptext" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label id="lblDatabaseError" class="sfError">
                                        </label>
                                    </td>
                                </tr>
                                <tr id="trUser" runat="Server" class="sfTdseperator">
                                    <td>
                                        <asp:Label ID="lblUserID" runat="Server" CssClass="sfFormlabel" />
                                        <asp:TextBox ID="txtUserId" runat="Server" CssClass="sfInputbox" />
                                        <asp:Label ID="lblUserHelp" runat="Server" CssClass="sfHelptext" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label id="lblUserIdError" class="sfError">
                                        </label>
                                    </td>
                                </tr>
                                <tr id="trPassword" runat="Server" class="sfTdseperator">
                                    <td>
                                        <asp:Label ID="lblPassword" runat="Server" CssClass="sfFormlabel" />
                                        <asp:TextBox ID="txtPassword" runat="Server" CssClass="sfInputbox" TextMode="Password"
                                            EnableViewState="true" />
                                        <asp:Label ID="lblPasswordHelp" runat="Server" CssClass="sfHelptext" /><label id="lblPasswordError"
                                            class="sfError"></label>
                                    </td>
                                </tr>
                                <tr id="trDatabaseHeading" runat="server">
                                    <td>
                                        <h2>
                                            Database Name</h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td id="trrdbCreateDatabase" runat="server" class="sfTdseperator">
                                                    <input id="rdbCreateDatabase" type="radio" name="rdbDataBase" runat="server" />
                                                    <h3>
                                                        Create New Database</h3>
                                                </td>
                                                <td id="trrdbExistingDatabase" runat="server" class="sfgap">
                                                    <input id="rdbExistingDatabase" type="radio" name="rdbDataBase" runat="server" />
                                                    <h3>
                                                        Existing Database</h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="trnewDatabase" runat="server" class="sfgap">
                                                    <asp:Label ID="lblNewDataBaseName" runat="Server" CssClass="sfFormlabel" Text="Database Name" />
                                                    <asp:TextBox ID="txtNewDataBaseName" runat="Server" CssClass="sfInputbox" AutoPostBack="false" />
                                                    <asp:Label ID="lblNewDatabaseHelp" runat="Server" CssClass="sfHelptext" />
                                                    <label id="lblNewDatabaseError" class="sfError">
                                                    </label>
                                                </td>
                                                <td id="trExistingDatabase" runat="server" class="sfgap2">
                                                    <asp:Label ID="lblExistingDatabaseName" runat="Server" CssClass="sfFormlabel" Text="Database Name" />
                                                    <asp:TextBox ID="txtExistingDatabaseName" runat="Server" CssClass="sfInputbox" AutoPostBack="false" />
                                                    <asp:Label ID="lblExistingDatabaseHelp" runat="Server" CssClass="sfHelptext" />
                                                    <label id="lblExistingDatabaseError" class="sfError">
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <%-- <tr class="sfTdseperator">
              <td><asp:Label ID="lblOwner" runat="Server" CssClass="sfFormlabel" />
              <asp:CheckBox ID="chkOwner" runat="Server" CssClass="sfCheckBox" Checked="true" />
              <asp:Label ID="lblOwnerHelp" runat="Server" CssClass="sfHelptext sfInline" /></td>
            </tr>--%>
                            </table>
                            <div class="sfButtonwrapper">
                                <asp:Button ID="btnTestPermission" runat="server" CssClass="sfBtn" Text=" Test Configuration"
                                    OnClick="btnTestPermission_Click" />
                            </div>
                        </div>
                        <div class="sfinstalbtn">
                            <asp:Button ID="btnInstall" runat="server" CssClass="sfBtn" Text="Install Sageframe"
                                OnClick="btnInstall_Click" />
                </asp:Panel>
            </div>
            <asp:Timer runat="server" ID="UpdateTimer" Interval="1000" OnTick="UpdateTimer_Tick"
                Enabled="false" />
            <asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="UpdateTimer" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <div class="sfProcessWrapper">
                        <div class="sfmaincontent">
                            <div class="sfloadingDiv" id="loadingDiv" runat="server">
                                <asp:Label ID="lblDBProgress" runat="server" Text="Installing Database Scripts ...Please wait...This may take a moment"
                                    EnableViewState="false"></asp:Label>
                                <asp:Image ID="imgDBProgress" runat="server" AlternateText="Installing Database Scripts..."
                                    ToolTip="Installing Database Scripts..." />
                            </div>
                            <asp:TextBox ID="txtFeedback" runat="server" class="cssClassFeedBack" Columns="60"
                                Rows="6" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                            <asp:Label ID="lblInstallErrorOccur" runat="server" Visible="false" EnableViewState="false" />
                        </div>
                    </div>
                    <div class="sfButtonwrapper">
                        <asp:Button ID="btnCancel" runat="server" CssClass="sfBtn" Text="Cancel" Visible="false"
                            OnClick="btnCancel_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
