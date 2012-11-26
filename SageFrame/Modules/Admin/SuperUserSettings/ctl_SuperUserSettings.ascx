<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_SuperUserSettings.ascx.cs"
    Inherits="SageFrame.Modules.Admin.SuperUserSettings.ctl_SuperUserSettings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register Src="~/Controls/sectionheadcontrol.ascx" TagName="sectionheadcontrol"
    TagPrefix="sfe" %>
<h2 class="cssClassFormHeading">
    <asp:Label ID="lblMasterSettingManagement" runat="server" Text="Master Setting Management"
        meta:resourcekey="lblMasterSettingManagementResource1"></asp:Label></h2>
<asp:Label ID="lblError" runat="server" meta:resourcekey="lblErrorResource1"></asp:Label>
<div class="cssClassButtonWrapper">
    <asp:ImageButton ID="imbSave" runat="server" OnClick="imbSave_Click" ToolTip="Save"
        meta:resourcekey="imbSaveResource1" />
    <asp:Label ID="lblSave" runat="server" Text="Save" AssociatedControlID="imbSave"
        Style="cursor: pointer;" meta:resourcekey="lblSaveResource1"></asp:Label>
    <asp:ImageButton ID="imbRestart" runat="server" OnClick="imbRestart_Click" ToolTip="Restart Application"
        meta:resourcekey="imbRestartResource1" />
    <asp:Label ID="lblRestart" runat="server" Text="Restart Application" AssociatedControlID="imbRestart"
        ToolTip="Restart Application" Style="cursor: pointer;" meta:resourcekey="lblRestartResource1"></asp:Label>
</div>
<ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0" meta:resourcekey="TabContainerResource1">
    <ajax:TabPanel ID="tabBasicSetting" runat="server" meta:resourcekey="tabBasicSettingResource1">
        <HeaderTemplate>
            <asp:Label ID="lblBasicSetting" runat="server" Text="Basic Setting" meta:resourcekey="lblBasicSettingResource1"></asp:Label>
        </HeaderTemplate>
        <ContentTemplate>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcConfiguration" runat="server" Section="tblConfiguration"
                    IncludeRule="false" IsExpanded="true" Text="Configuration" />
                <div id="tblConfiguration" runat="server" class="cssClassCollapseContent">
                    <asp:Label ID="lblBasicSettingsHelp" runat="server" CssClass="cssClassHelpTitle"
                        Text="Enter basic settings for your Hosting Account" meta:resourcekey="lblBasicSettingsHelpResource1"></asp:Label>
                    <div class="cssClassFormWrapper">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblProduct" runat="server" CssClass="cssClassFormLabel" Text="SageFrame Product:"
                                        ToolTip="The SageFrame application you are running" meta:resourcekey="lblProductResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVProduct" runat="server" meta:resourcekey="lblVProductResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblVersion" runat="server" CssClass="cssClassFormLabel" Text="SageFrame Version:"
                                        ToolTip="The SageFrame application version you are running" meta:resourcekey="lblVersionResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVVersion" runat="server" meta:resourcekey="lblVVersionResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDataProvider" runat="server" CssClass="cssClassFormLabel" Text="Data Provider:"
                                        ToolTip="The provider name which is identified as the default data provider in the web.config file"
                                        meta:resourcekey="lblDataProviderResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVDataProvider" runat="server" meta:resourcekey="lblVDataProviderResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDotNetFrameWork" runat="server" CssClass="cssClassFormLabel" Text=".Net Framework:"
                                        ToolTip="The .NET Framework version which the application is running on - specified through IIS"
                                        meta:resourcekey="lblDotNetFrameWorkResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVDotNetFrameWork" runat="server" meta:resourcekey="lblVDotNetFrameWorkResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblASPDotNetIdentiy" runat="server" CssClass="cssClassFormLabel" Text="ASP.NET Identity:"
                                        ToolTip="The Windows user account under which the application is running. This is the account which needs to be granted folder permissions on the server."
                                        meta:resourcekey="lblASPDotNetIdentiyResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVASPDotNetIdentiy" runat="server" meta:resourcekey="lblVASPDotNetIdentiyResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblServerName" runat="server" CssClass="cssClassFormLabel" Text="Server Name:"
                                        ToolTip="The Name of the Server." meta:resourcekey="lblServerNameResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVServerName" runat="server" meta:resourcekey="lblVServerNameResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIpAddress" runat="server" CssClass="cssClassFormLabel" Text="IP Address:"
                                        ToolTip="The IP Address of the Server." meta:resourcekey="lblIpAddressResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVIpAddress" runat="server" meta:resourcekey="lblVIpAddressResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPermissions" runat="server" CssClass="cssClassFormLabel" Text="Permissions:"
                                        ToolTip="The code access permissions available in the hosting environment." meta:resourcekey="lblPermissionsResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVPermissions" runat="server" meta:resourcekey="lblVPermissionsResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblRelativePath" runat="server" CssClass="cssClassFormLabel" Text="Relative Path:"
                                        ToolTip="The relative location of the application in relation to the root of the site."
                                        meta:resourcekey="lblRelativePathResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVRelativePath" runat="server" meta:resourcekey="lblVRelativePathResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPhysicalPath" runat="server" CssClass="cssClassFormLabel" Text="Physical Path:"
                                        ToolTip="The physical location of the site root on the server." meta:resourcekey="lblPhysicalPathResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVPhysicalPath" runat="server" meta:resourcekey="lblVPhysicalPathResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblServerTime" runat="server" CssClass="cssClassFormLabel" Text="Server Time:"
                                        ToolTip="The current date and time for the web server" meta:resourcekey="lblServerTimeResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVServerTime" runat="server" meta:resourcekey="lblVServerTimeResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblGUID" runat="server" CssClass="cssClassFormLabel" Text="GUID:"
                                        ToolTip="The globally unique identifier which can be used to identify this application."
                                        meta:resourcekey="lblGUIDResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVGUID" runat="server" meta:resourcekey="lblVGUIDResource1"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcHost" runat="server" Section="tblHost" IncludeRule="false"
                    IsExpanded="false" Text="Super User Details" />
                <div id="tblHost" runat="server" class="cssClassCollapseContent">
                    <div class="cssClassFormWrapper">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblHostPortal" runat="server" CssClass="cssClassFormLabel" Text="Default Portal:"
                                        ToolTip="Select the default portal" meta:resourcekey="lblHostPortalResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHostPortal" runat="server" CssClass="cssClassDropDown" Enabled="False"  meta:resourcekey="ddlHostPortalResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblHostTitle" runat="server" CssClass="cssClassFormLabel" Text="Site Title:"
                                        ToolTip="Enter the name of your Hosting Account" meta:resourcekey="lblHostTitleResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHostTitle" runat="server" CssClass="cssClassNormalTextBox" meta:resourcekey="txtHostTitleResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblHostUrl" runat="server" CssClass="cssClassFormLabel" Text="Site Url:"
                                        ToolTip="Enter the url of your Hosting Account" meta:resourcekey="lblHostUrlResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHostUrl" runat="server" CssClass="cssClassNormalTextBox" meta:resourcekey="txtHostUrlResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblHostEmail" runat="server" CssClass="cssClassFormLabel" Text="Site Email:"
                                        ToolTip="Enter a support email adress for your Hosting Account" meta:resourcekey="lblHostEmailResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHostEmail" runat="server" CssClass="cssClassNormalTextBox" meta:resourcekey="txtHostEmailResource1"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcAppearance" runat="server" Section="tblAppearance"
                    IncludeRule="false" IsExpanded="false" Text="Appearance" />
                <div id="tblAppearance" runat="server" class="cssClassCollapseContent">
                    <div class="cssClassFormWrapper">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblCopyright" runat="server" CssClass="cssClassFormLabel" Text="Show Copyright Credits?"
                                        ToolTip="Select this to add the SageFrame copyright credits to the Page Source"
                                        meta:resourcekey="lblCopyrightResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkCopyright" runat="server" CssClass="cssClassCheckBox" meta:resourcekey="chkCopyrightResource1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUseCustomErrorMessages" runat="server" CssClass="cssClassFormLabel"
                                        Text="Use Custom Error Messages?" ToolTip="Select this to use Custom Error Messages"
                                        meta:resourcekey="lblUseCustomErrorMessagesResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkUseCustomErrorMessages" runat="server" CssClass="cssClassCheckBox"
                                        meta:resourcekey="chkUseCustomErrorMessagesResource1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTemplate" runat="server" CssClass="cssClassFormLabel" Text="Template:"
                                        ToolTip="Select the default template to use for your sites" meta:resourcekey="lblTemplateResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTemplate" runat="server" CssClass="cssClassDropDown" meta:resourcekey="ddlTemplateResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </ajax:TabPanel>
    <ajax:TabPanel ID="tabAdvanceSetting" runat="server" meta:resourcekey="tabAdvanceSettingResource1">
        <HeaderTemplate>
            <asp:Label ID="lblAdvanceSetting" runat="server" Text="Advanced Settings" meta:resourcekey="lblAdvanceSettingResource1"></asp:Label>
        </HeaderTemplate>
        <ContentTemplate>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcFriendlyUrl" runat="server" Section="tblFriendlyUrl"
                    IncludeRule="false" IsExpanded="false" Text="Friendly Url Settings" />
                <div id="tblFriendlyUrl" runat="server" class="cssClassCollapseContent">
                    <asp:Label ID="lblAdvancedSettingsHelp" runat="server" CssClass="cssClassHelpTitle"
                        Text="Enter advanced settings for your Hosting Account" meta:resourcekey="lblAdvancedSettingsHelpResource1"></asp:Label>
                    <div class="cssClassFormWrapper">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblUseFriendlyUrls" runat="server" CssClass="cssClassFormLabel" Text="Use Friendly Urls?"
                                        ToolTip="Check to enable search engine friendly urls." meta:resourcekey="lblUseFriendlyUrlsResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkUseFriendlyUrls" runat="server" CssClass="cssClassCheckBox"
                                        meta:resourcekey="chkUseFriendlyUrlsResource1" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcSMTP" runat="server" Section="tblSMTP" IncludeRule="false"
                    IsExpanded="false" Text="SMTP Server Settings" />
                <div id="tblSMTP" runat="server" class="cssClassCollapseContent">
                    <div class="cssClassFormWrapper">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblSMTPServerAndPort" runat="server" CssClass="cssClassFormLabel"
                                        Text="SMTP Server and port:" ToolTip="Enter the SMTP Server Address. You can also specify an alternate port by adding a colon and the port number (e.g. smtp.googlemail.com:587). Enter the SMTP server name only to use default port number (25)"
                                        meta:resourcekey="lblSMTPServerAndPortResource1"></asp:Label>
                                </td>
                                <td>
                                    <div class="cssClassSmtpAddress">
                                        <asp:TextBox ID="txtSMTPServerAndPort" runat="server" MaxLength="50" CssClass="cssClassNormalTextBox"
                                            meta:resourcekey="txtSMTPServerAndPortResource1"></asp:TextBox><div class="cssClassMailTest">
                                                <asp:LinkButton ID="lnkTestSMTP" runat="server" Text="Test" OnClick="lnkTestSMTP_Click"
                                                    CausesValidation="False" meta:resourcekey="lnkTestSMTPResource1"></asp:LinkButton>
                                                <asp:Label ID="lblSMTPEmailTestResult" runat="server" CssClass="NormalRed" meta:resourcekey="lblSMTPEmailTestResultResource1" />
                                            </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSMTPAuthentication" runat="server" CssClass="cssClassFormLabel"
                                        Text="SMTP Authentication:" ToolTip="Enter the SMTP Server Address. You can also specify an alternate port by adding a colon and the port number (e.g. smtp.googlemail.com:587). Enter the SMTP server name only to use default port number (25)"
                                        meta:resourcekey="lblSMTPAuthenticationResource1"></asp:Label>
                                </td>
                                <td id="tdSMTPAuthentication" class="cssClassButtonListWrapper">
                                    <asp:RadioButtonList ID="rblSMTPAuthentication" runat="server" AutoPostBack="True"
                                        RepeatDirection="Horizontal" RepeatColumns="3" OnSelectedIndexChanged="rblSMTPAuthentication_SelectedIndexChanged"
                                        CssClass="cssClassRadioButtonList" meta:resourcekey="rblSMTPAuthenticationResource1">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSMTPEnableSSL" runat="server" CssClass="cssClassFormLabel" Text="SMTP Enable SSL: "
                                        ToolTip="Enter the SMTP Server Address. You can also specify an alternate port by adding a colon and the port number (e.g. smtp.googlemail.com:587). Enter the SMTP server name only to use default port number (25)"
                                        meta:resourcekey="lblSMTPEnableSSLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkSMTPEnableSSL" runat="server" CssClass="cssClassCheckBox" meta:resourcekey="chkSMTPEnableSSLResource1" />
                                </td>
                            </tr>
                            <tr id="trSMTPUserName" runat="server">
                                <td runat="server">
                                    <asp:Label ID="lblSMTPUserName" runat="server" CssClass="cssClassFormLabel" Text="SMTP Username:"
                                        ToolTip="Enter the Username for the SMTP Server"></asp:Label>
                                </td>
                                <td runat="server">
                                    <asp:TextBox ID="txtSMTPUserName" runat="server" MaxLength="50" CssClass="cssClassNormalTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trSMTPPassword" runat="server">
                                <td runat="server">
                                    <asp:Label ID="lblSMTPPassword" runat="server" CssClass="cssClassFormLabel" Text="SMTP Password:"
                                        ToolTip="Enter the Password for the SMTP Server"></asp:Label>
                                </td>
                                <td runat="server">
                                    <asp:TextBox ID="txtSMTPPassword" runat="server" MaxLength="50" CssClass="cssClassNormalTextBox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcFileManager" runat="server" Section="tblFileMgr"
                    IncludeRule="false" IsExpanded="false" Text="File Manager Settings" />
                <div id="tblFileMgr" runat="server" class="cssClassCollapseContent">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="cssClassButtonWrapper">
                                    <asp:ImageButton ID="btnShowPopUp" runat="server"  OnClick="btnShowPopUp_Click" />
                                    <asp:Label ID="lblAddFolder" runat="server" AssociatedControlID="btnShowPopUp" Text="Add Root Folder"></asp:Label>
                                  
                          <span class="cssClassPageSize">                           
                            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="cssClasslistddl" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            </asp:DropDownList>
                            </span>
                            </div>                           
                                
                            <div class="cssClassGridWrapper">
                                <asp:GridView ID="gdvRootFolders" runat="server" Width="100%" AutoGenerateColumns="false"
                                    AllowPaging="true" OnRowDataBound="gdvRootFolders_RowDataBound" OnRowCommand="gdvRootFolders_RowCommand"
                                    OnPageIndexChanging="gdvRootFolders_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="FolderID" />
                                        <asp:BoundField DataField="FolderPath" HeaderText="FolderPath" />
                                        <asp:TemplateField HeaderText="Is Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%#Eval("IsEnabled") %>' AutoPostBack="True"
                                                    OnCheckedChanged="chkIsActive_CheckedChanged" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="cssClassIsFolderEnabled" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbDelete" runat="server" ImageUrl="~/Modules/FileManager/images/cross.png"
                                                    CommandName="DeleteRootFolder" CommandArgument='<%#Eval("FolderID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="cssClassColumnEdit" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                                    <HeaderStyle CssClass="cssClassHeadingOne" />
                                    <PagerStyle CssClass="cssClassPageNumber" />
                                    <RowStyle CssClass="cssClassAlternativeOdd" />
                                </asp:GridView>
                            </div>
                              </ContentTemplate>
                    </asp:UpdatePanel>
                       
                    <ajax:modalpopupextender id="PopUp" runat="server" behaviorid="programmaticModalPopupBehavior"
                        dropshadow="False" targetcontrolid="popUpBtn1" backgroundcssclass="modalBackground"
                        popupcontrolid="pnlPopUp1" CancelControlID="closeButtonMock">
                    </ajax:modalpopupextender>
                    <asp:Panel ID="pnlPopUp1" CssClass="cssClassDirList" Style="display: none" runat="server">
                        <asp:Panel ID="Panel2" CssClass="controlDiv" runat="server" Width="100%" Style="cursor: move;
                            text-align: center; height: 15px">
                          </asp:Panel>
                        <span class="closePopUp">
                            <asp:ImageButton runat="server" ID="imgClosePopUp" OnClick="imgClosePopUp_Click" ImageUrl="~/Modules/FileManager/images/cancel.png" /></span>
                        <asp:Button ID="popUpBtn1" runat="server" Style="display: none" Text="OK" />
                        <asp:Button ID="closeButtonMock" runat="server" Style="display: none" Text="OK" />
                        <div id="divDirList">
                            <asp:TreeView ID="TreeView1" runat="server" SelectedNodeStyle-CssClass="TreeSelectedNode"
                                OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" SelectedNodeStyle-Font-Underline="true">
                            </asp:TreeView>
                        </div>
                    </asp:Panel>
                   
                </div>
            </div>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcOther" runat="server" Section="tblOther" IncludeRule="false"
                    IsExpanded="false" Text="Other Settings" />
                <div id="tblOther" runat="server" class="cssClassCollapseContent">
                    <div class="cssClassFormWrapper">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblFileExtensions" runat="server" CssClass="cssClassFormLabel" Text="Allowable File Extensions:"
                                        ToolTip="Enter the allowable file extensions (separated by commas)" meta:resourcekey="lblFileExtensionsResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFileExtensions" runat="server" TextMode="MultiLine" Rows="5"
                                        meta:resourcekey="txtFileExtensionsResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblHelpUrl" runat="server" CssClass="cssClassFormLabel" Text="Help URL:"
                                        ToolTip="Enter the URL for the Online Help you will be providing. If you leave the entry blank then no Online Help will be offered for the Admin/Host areas of SageFrame."
                                        meta:resourcekey="lblHelpUrlResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHelpUrl" runat="server" CssClass="cssClassNormalTextBox" meta:resourcekey="txtHelpUrlResource1"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </ajax:TabPanel>
</ajax:TabContainer>
