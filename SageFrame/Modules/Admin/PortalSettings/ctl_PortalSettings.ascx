<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_PortalSettings.ascx.cs"
    Inherits="SageFrame.PortalSetting.ctl_PortalSettings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Controls/sectionheadcontrol.ascx" TagName="sectionheadcontrol"
    TagPrefix="sfe" %>
    <h2 class="cssClassFormHeading">
        <asp:Label ID="lblPortalSetting" runat="server" 
            Text="Portal Setting Management" meta:resourcekey="lblPortalSettingResource1"></asp:Label></h2>
<asp:Label ID="lblError" runat="server" meta:resourcekey="lblErrorResource1"></asp:Label>
<ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0" 
    meta:resourcekey="TabContainerResource1">
    <ajax:TabPanel ID="tabBasicSetting" runat="server" 
        meta:resourcekey="tabBasicSettingResource1">
        <HeaderTemplate>
            <asp:Label ID="lblBasicSetting" runat="server" Text="Basic Setting" 
                meta:resourcekey="lblBasicSettingResource1"></asp:Label>
        </HeaderTemplate>
        <ContentTemplate>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcSite" runat="server" Section="tblSite" IncludeRule="false"
                    IsExpanded="true" Text="Site Details" />
                <div id="tblSite" runat="server" class="cssClassCollapseContent">
                    <asp:Label ID="lblBasicSettingsHelp" runat="server" CssClass="cssClassHelpTitle"
                        Text="In this section, you can set up the basic settings for your site." 
                        meta:resourcekey="lblBasicSettingsHelpResource1"></asp:Label>
                    <div class="cssClassFormWrapper">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="16%">
                                    <asp:Label ID="lblPortalTitle" runat="server" CssClass="cssClassFormLabel" Text="Title:"
                                        
                                        ToolTip="This is the Title for your portal. The text you enter will show up in the Title Bar." 
                                        meta:resourcekey="lblPortalTitleResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPortalTitle" runat="server" MaxLength="256" 
                                        CssClass="cssClassNormalTextBox" meta:resourcekey="txtPortalTitleResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDescription" runat="server" CssClass="cssClassFormLabel" Text="Description:"
                                        ToolTip="Enter a description about your site here." 
                                        meta:resourcekey="lblDescriptionResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" 
                                        MaxLength="256" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblKeyWords" runat="server" CssClass="cssClassFormLabel" Text="Key Words:"
                                        
                                        ToolTip="Enter some keywords for your site (separated by commas). These keywords are used by search engines to help index your site." 
                                        meta:resourcekey="lblKeyWordsResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKeyWords" runat="server" TextMode="MultiLine" Rows="5" 
                                        MaxLength="256" meta:resourcekey="txtKeyWordsResource1"></asp:TextBox>
                                </td>
                            </tr>                            
                            
                        </table>
                        <asp:Label ID="lblMetaHelpText" runat="server" CssClass="cssClassNote" Text="<dl>
  <dt>Note:</dt>
  <dd> These Meta information is for the default value of page meta tags, if site administrator will leave any of these meta tags while adding and updating a pages then these meta information will be ovewritten for that meta value for a page.</dd>
</dl>
" meta:resourcekey="lblMetaHelpTextResource1"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcMarketing" runat="server" Section="tblMarketing" IncludeRule="false"
                    IsExpanded="false" Text="Site Marketing" />
                <div id="tblMarketing" runat="server" class="cssClassCollapseContent">
                    <div class="cssClassFormWrapper">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="16%">
                                    <asp:Label ID="lblPortalGoogleAdSenseID" runat="server" Text="Google AdSense ID:"
                                        CssClass="cssClassFormLabel" 
                                        ToolTip="Google AdSense ID used for google adsence." 
                                        meta:resourcekey="lblPortalGoogleAdSenseIDResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPortalGoogleAdSenseID" runat="server" MaxLength="100" 
                                        CssClass="cssClassNormalTextBox" 
                                        meta:resourcekey="txtPortalGoogleAdSenseIDResource1"></asp:TextBox>
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
                                <td width="16%">
                                    <asp:Label ID="lblTemplate" runat="server" CssClass="cssClassFormLabel" Text="Template:"
                                        ToolTip="Select the template to use for your site" 
                                        meta:resourcekey="lblTemplateResource1"></asp:Label>
                                </td>
                                <td class="cssClassButtonListWrapper">
                                    <asp:DropDownList ID="ddlTemplate" runat="server" CssClass="cssClassDropDown" 
                                        meta:resourcekey="ddlTemplateResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsPortalMenuIsImage" runat="server" CssClass="cssClassFormLabel"
                                        Text="Is Image Menu?" ToolTip="Set yes if you like to get menu in image" 
                                        meta:resourcekey="lblIsPortalMenuIsImageResource1"></asp:Label>
                                </td>
                                <td class="cssClassButtonListWrapper">
                                    <asp:RadioButtonList ID="rblIsPortalMenuIsImage" runat="server" 
                                        CssClass="cssClassRadioButtonList" 
                                        meta:resourcekey="rblIsPortalMenuIsImageResource1">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPortalMenuImageExtension" runat="server" CssClass="cssClassFormLabel"
                                        Text="Image extension:" ToolTip="like .jpg/.gif/.png" 
                                        meta:resourcekey="lblPortalMenuImageExtensionResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPortalMenuImageExtension" runat="server" MaxLength="10" 
                                        CssClass="cssClassNormalTextBox" 
                                        meta:resourcekey="txtPortalMenuImageExtensionResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPortalShowProfileLink" runat="server" CssClass="cssClassFormLabel"
                                        Text="Show Profile Link:" ToolTip="Show Profile Link" 
                                        meta:resourcekey="lblPortalShowProfileLinkResource1"></asp:Label>
                                </td>
                                <td class="cssClassButtonListWrapper">
                                    <asp:RadioButtonList ID="rblPortalShowProfileLink" runat="server" 
                                        CssClass="cssClassRadioButtonList" 
                                        meta:resourcekey="rblPortalShowProfileLinkResource1">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEnableRememberme" runat="server" CssClass="cssClassFormLabel" Text="Enable Remember me?"
                                        
                                        ToolTip="Sets the remember me checkbox on login controls. If remember me is allowed, users can create cookies that are persisted over multiple visits." 
                                        meta:resourcekey="lblEnableRemembermeResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkEnableRememberme" runat="server" 
                                        CssClass="cssClassCheckBox" meta:resourcekey="chkEnableRemembermeResource1"></asp:CheckBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcOptimization" runat="server" Section="tblOptimization"
                    IncludeRule="false" IsExpanded="false" Text="Optimization" />
                <div id="tblOptimization" runat="server" class="cssClassCollapseContent">
                    <div class="cssClassFormWrapper">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="Label1" runat="server" CssClass="cssClassFormLabel" Text="Optimize CSS"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkOptCss" CssClass="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" CssClass="cssClassFormLabel" Text="Optimize Js?"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkOptJs" runat="server" CssClass="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" CssClass="cssClassFormLabel" Text="Refresh Cache"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnRefreshCache" runat="server" Text="Refresh"  CssClass=""
                                        OnClick="btnRefreshCache_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </ajax:TabPanel>
    <ajax:TabPanel ID="tabAdvanceSetting" runat="server" 
        meta:resourcekey="tabAdvanceSettingResource1">
        <HeaderTemplate>
            <asp:Label ID="lblAdvanceSetting" runat="server" Text="Advanced Settings" 
                meta:resourcekey="lblAdvanceSettingResource1"></asp:Label>
        </HeaderTemplate>
        <ContentTemplate>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcSecurity" runat="server" Section="tblSecurity" IncludeRule="false"
                    IsExpanded="false" Text="Security Settings" />
                <div id="tblSecurity" runat="server" class="cssClassCollapseContent">
                    <div class="cssClassFormWrapper">
                        <asp:Label ID="lblAdvancedSettingsHelp" runat="server" CssClass="cssClassHelpTitle"
                            
                            Text="In this section, you can set up more advanced settings for your site." 
                            meta:resourcekey="lblAdvancedSettingsHelpResource1"></asp:Label>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td width="15%" valign="top">
                                    <asp:Label ID="lblUserRegistration" runat="server" CssClass="cssClassFormLabel" Text="User Registration:"
                                        ToolTip="The type of user registration allowed for this site." 
                                        meta:resourcekey="lblUserRegistrationResource1"></asp:Label>
                                </td>
                                <td class="cssClassButtonListWrapper" valign="top">
                                    <asp:RadioButtonList ID="rblUserRegistration" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                        CssClass="cssClassRadioButtonList" 
                                        meta:resourcekey="rblUserRegistrationResource1">
                                    </asp:RadioButtonList>                                    
                                </td>                                                               
                            </tr>
                        </table>
                        <asp:Label ID="lblUserRegistrationHelpText" CssClass="cssClassNote" 
                            runat="server" Text="<h3>Note:</h3>
<dl>
  <dt>None:</dt>
  <dd> This removes the registration link from your website. The administrators of your website can only add new users manually. </dd>
  <dt>Private:</dt>
  <dd>The register link appears. When a user registers, the administrators have to approve the user before the user will be granted access.</dd>
  <dt>Public:</dt>
  <dd>This is the default setting for your SageFrame portal. The register link appears. When a user registers, they are given instant access to your site as a member without any verification.</dd>
  <dt>Verified:</dt>
  <dd>The registration link appears. When a user registers, they are sent an email with a verification code. The first time they login, they are asked to enter the verification code, if they are verified correctly; they are given access to your site as a member. Once they are verified, they no longer need to enter the verification code..</dd>
</dl>" meta:resourcekey="lblUserRegistrationHelpTextResource1"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcPages" runat="server" Section="tblPages" IncludeRule="false"
                    IsExpanded="false" Text="Page Management" />
                <div id="tblPages" runat="server" class="cssClassCollapseContent">
                    <div class="cssClassFormWrapper">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblLoginPage" runat="server" CssClass="cssClassFormLabel" Text=" Login Page:"
                                        ToolTip="The Login Page for your site." 
                                        meta:resourcekey="lblLoginPageResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLoginPage" runat="server" CssClass="cssClassDropDown" 
                                        meta:resourcekey="ddlLoginPageResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPortalDefaultPage" runat="server" CssClass="cssClassFormLabel"
                                        Text="Portal Default Page:" ToolTip="The Home Page" 
                                        meta:resourcekey="lblPortalDefaultPageResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPortalDefaultPage" runat="server" 
                                        CssClass="cssClassDropDown" meta:resourcekey="ddlPortalDefaultPageResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPortalUserProfilePage" runat="server" CssClass="cssClassFormLabel"
                                        Text="User Profile Page:" ToolTip="The user profile page" 
                                        meta:resourcekey="lblPortalUserProfilePageResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPortalUserProfilePage" runat="server" 
                                        CssClass="cssClassDropDown" 
                                        meta:resourcekey="ddlPortalUserProfilePageResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUserRegistrationPage" runat="server" CssClass="cssClassFormLabel"
                                        Text="User Registration:" ToolTip="The User Registration Page" 
                                        meta:resourcekey="lblUserRegistrationPageResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUserRegistrationPage" runat="server" 
                                        CssClass="cssClassDropDown" meta:resourcekey="ddlUserRegistrationPageResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPortalUserActivation" runat="server" CssClass="cssClassFormLabel"
                                        Text="User Activation:" ToolTip="The User Activation Page" 
                                        meta:resourcekey="lblPortalUserActivationResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPortalUserActivation" runat="server" 
                                        CssClass="cssClassDropDown" meta:resourcekey="ddlPortalUserActivationResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPortalForgotPassword" runat="server" CssClass="cssClassFormLabel"
                                        Text="User Forgot Password:" ToolTip="The User Forgot Password Page" 
                                        meta:resourcekey="lblPortalForgotPasswordResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPortalForgotPassword" runat="server" 
                                        CssClass="cssClassDropDown" meta:resourcekey="ddlPortalForgotPasswordResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPortalPageNotAccessible" runat="server" CssClass="cssClassFormLabel"
                                        Text="Page Not Accessible Page:" ToolTip="The Page Not Accessible Page" 
                                        meta:resourcekey="lblPortalPageNotAccessibleResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPortalPageNotAccessible" runat="server" 
                                        CssClass="cssClassDropDown" 
                                        meta:resourcekey="ddlPortalPageNotAccessibleResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPortalPageNotFound" runat="server" CssClass="cssClassFormLabel"
                                        Text="Page Not Found Page:" ToolTip="The Page Not Found Page" 
                                        meta:resourcekey="lblPortalPageNotFoundResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPortalPageNotFound" runat="server" 
                                        CssClass="cssClassDropDown" meta:resourcekey="ddlPortalPageNotFoundResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPortalPasswordRecovery" runat="server" CssClass="cssClassFormLabel"
                                        Text="Password Recovery Page:" ToolTip="The Password Recovery Page" 
                                        meta:resourcekey="lblPortalPasswordRecoveryResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPortalPasswordRecovery" runat="server" 
                                        CssClass="cssClassDropDown" 
                                        meta:resourcekey="ddlPortalPasswordRecoveryResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
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
                                    <asp:Label ID="lblDefaultLanguage" runat="server" CssClass="cssClassFormLabel" Text="Default Language:"
                                        ToolTip="Select a default Language for the web Site" 
                                        meta:resourcekey="lblDefaultLanguageResource1"></asp:Label>
                                </td>
                                <td>
                                     <asp:DropDownList ID="ddlDefaultLanguage" CssClass="cssClassDropDown" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlDefaultLanguage_SelectedIndexChanged"
                                meta:resourcekey="ddlDefaultLanguageResource1">
                            </asp:DropDownList>
                            <asp:Image ID="imgFlag" runat="server" meta:resourcekey="imgFlagResource1" />
                            
                                </td>
                            </tr>
                                 <tr>
                                <td width="20%">
                                   
                                </td>
                                <td>
                                    <div class="cssClassButtonListWrapper">
                                <asp:RadioButtonList ID="rbLanguageType" CssClass="cssClassRadioButtonList" runat="server"
                                    AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbLanguageType_SelectedIndexChanged"
                                    meta:resourcekey="rbLanguageTypeResource1">
                                    <asp:ListItem Text="English" Value="0" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                    <asp:ListItem Text="Native" Value="1" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPortalTimeZone" runat="server" CssClass="cssClassFormLabel" Text="Portal TimeZone:"
                                        ToolTip="The TimeZone for the location of the site." 
                                        meta:resourcekey="lblPortalTimeZoneResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPortalTimeZone" runat="server" 
                                        CssClass="cssClassDropDown" meta:resourcekey="ddlPortalTimeZoneResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSiteAdminEmailAddress" runat="server" CssClass="cssClassFormLabel"
                                        Text="Site Email Address:" ToolTip="Site Email Address." 
                                        meta:resourcekey="lblSiteAdminEmailAddressResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSiteAdminEmailAddress" runat="server" MaxLength="50" 
                                        CssClass="cssClassNormalTextBox" 
                                        meta:resourcekey="txtSiteAdminEmailAddressResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCPanelTitle" runat="server" CssClass="cssClassFormLabel" Text="CPanel Title:"
                                        ToolTip="This is the Title for your CPanle" 
                                        meta:resourcekey="lblCPanelTitleResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLogoTemplate" runat="server" MaxLength="255" 
                                        CssClass="cssClassNormalTextBox" meta:resourcekey="txtLogoTemplateResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCPanleCopyright" runat="server" CssClass="cssClassFormLabel" Text="CPanel Copyright:"
                                        ToolTip="This is the Title for your CPanle Copyright" 
                                        meta:resourcekey="lblCPanleCopyrightResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCopyright" runat="server" MaxLength="255" 
                                        CssClass="cssClassNormalTextBox" meta:resourcekey="txtCopyrightResource1"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </ajax:TabPanel>
</ajax:TabContainer>
<div class="cssClassButtonWrapper">
    <asp:ImageButton ID="imbSave" runat="server" OnClick="imbSave_Click" 
        ToolTip="Save" meta:resourcekey="imbSaveResource1" />
    <asp:Label ID="lblSave" runat="server" AssociatedControlID="imbSave" 
        Text="Save" meta:resourcekey="lblSaveResource1"></asp:Label>
    <asp:ImageButton ID="imbRefresh" runat="server" ToolTip="Refresh" 
        OnClick="imbRefresh_Click" meta:resourcekey="imbRefreshResource1" />
    <asp:Label ID="lblRefresh" runat="server" AssociatedControlID="imbRefresh" 
        Text="Refresh" meta:resourcekey="lblRefreshResource1"></asp:Label>
    <div class="cssClassclear">
    </div>
</div>
