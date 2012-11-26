<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModuleEditor.ascx.cs"
    Inherits="SageFrame.Modules.Admin.Extensions.Editors.ModuleEditor" %>
<%@ Register Src="PackageDetails.ascx" TagName="PackageDetails" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/sectionheadcontrol.ascx" TagName="sectionheadcontrol"
    TagPrefix="sfe" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<div class="cssClassButtonWrapper">
    <asp:ImageButton ID="imbUninstall" runat="server" CausesValidation="False" OnClick="imbUninstall_Click"
        meta:resourcekey="imbUninstallResource1" />
    <asp:Label Style="cursor: pointer;" ID="lblUnInstallExtension" runat="server" Text="Uninstall Extension"
        AssociatedControlID="imbUninstall" meta:resourcekey="lblUnInstallExtensionResource1" />
    <%--<asp:ImageButton ID="imbPackage" runat="server" CausesValidation="False" />
    <asp:Label Style="cursor: pointer;" ID="lblCreatePackage" runat="server" Text="Create Package"
        AssociatedControlID="imbPackage" />--%>
    <asp:ImageButton ID="imbCancel" runat="server" CausesValidation="False" OnClick="imbCancel_Click"
        meta:resourcekey="imbCancelResource1" />
    <asp:Label Style="cursor: pointer;" ID="lblCancel" runat="server" Text="Cancel" AssociatedControlID="imbCancel"
        meta:resourcekey="lblCancelResource1" />
</div>
<ajax:TabContainer ID="TabContainerManageModules" runat="server" ActiveTabIndex="0"
    meta:resourcekey="TabContainerManageModulesResource1">
    <ajax:TabPanel ID="TabPanelModuleEditor" runat="server" meta:resourcekey="TabPanelModuleEditorResource1">
        <HeaderTemplate>
            Module Settings
        </HeaderTemplate>
        <ContentTemplate>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcEditExtension" runat="server" IncludeRule="false"
                    IsExpanded="true" Section="divExtensionSettings" Text="Edit Extension"></sfe:sectionheadcontrol>
                <div id="divExtensionSettings" runat="server" class="cssClassCollapseContent">
                    <asp:Label ID="lblExtensionSettingsHelp" runat="server" CssClass="cssClassHelpTitle"
                        Text="In this section, you can set up more advanced settings for Module Controls on this Module."
                        meta:resourcekey="lblExtensionSettingsHelpResource1"></asp:Label>
                    <div class="cssClassFormWrapper">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblModuleName" runat="server" CssClass="cssClassFormLabel" Text="Module Name:"
                                        meta:resourcekey="lblModuleNameResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblModuleNameD" runat="server" CssClass="cssClassFormLabelField" meta:resourcekey="lblModuleNameDResource1"></asp:Label>
                                    <asp:HiddenField ID="hdnModuleName" runat="server"></asp:HiddenField>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFolderName" runat="server" CssClass="cssClassFormLabel" Text="Folder Name:"
                                        meta:resourcekey="lblFolderNameResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFolderName" runat="server" CssClass="cssClassNormalTextBox" MaxLength="200"
                                        ReadOnly="True" meta:resourcekey="txtFolderNameResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblBusinessControllerClass" runat="server" CssClass="cssClassFormLabel"
                                        Text="Business Controller Class:" meta:resourcekey="lblBusinessControllerClassResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBusinessControllerClass" runat="server" CssClass="cssClassNormalTextBox"
                                        MaxLength="500" meta:resourcekey="txtBusinessControllerClassResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDependencies" runat="server" CssClass="cssClassFormLabel" Text="Dependencies:"
                                        meta:resourcekey="lblDependenciesResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDependencies" runat="server" CssClass="cssClassNormalTextBox"
                                        meta:resourcekey="txtDependenciesResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPermissions" runat="server" CssClass="cssClassFormLabel" Text="Permissions:"
                                        meta:resourcekey="lblPermissionsResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPermissions" runat="server" CssClass="cssClassNormalTextBox"
                                        meta:resourcekey="txtPermissionsResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsPortable" runat="server" CssClass="cssClassFormLabel" Text="Is Portable?"
                                        meta:resourcekey="lblIsPortableResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsPortable" runat="server" CssClass="cssClassCheckBox" Enabled="False"
                                        meta:resourcekey="chkIsPortableResource1"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsSearchable" runat="server" CssClass="cssClassFormLabel" Text="Is Searchable?"
                                        meta:resourcekey="lblIsSearchableResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsSearchable" runat="server" Checked="True" CssClass="cssClassCheckBox"
                                        Enabled="False" meta:resourcekey="chkIsSearchableResource1"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsUpgradable" runat="server" CssClass="cssClassFormLabel" Text="Is Upgradable?"
                                        meta:resourcekey="lblIsUpgradableResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsUpgradable" runat="server" Checked="True" CssClass="cssClassCheckBox"
                                        Enabled="False" meta:resourcekey="chkIsUpgradableResource1"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsPremium" runat="server" CssClass="cssClassFormLabel" Text="Is Premium Module?"
                                        meta:resourcekey="lblIsPremiumResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsPremium" runat="server" CssClass="cssClassCheckBox" meta:resourcekey="chkIsPremiumResource1">
                                    </asp:CheckBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div id="divPackageSettings" runat="server" style="">
                <uc1:PackageDetails ID="PackageDetails1" runat="server"></uc1:PackageDetails>
            </div>
            <div class="cssClassButtonWrapper">
                <asp:ImageButton ID="imbUpdate" runat="server" OnClick="imbUpdate_Click" meta:resourcekey="imbUpdateResource1" />
                <asp:Label Style="cursor: pointer;" ID="lblUpdateModule" runat="server" Text="Update Module"
                    AssociatedControlID="imbUpdate" meta:resourcekey="lblUpdateModuleResource1" />
            </div>
        </ContentTemplate>
    </ajax:TabPanel>
    <ajax:TabPanel ID="TabPanelDefinitions" runat="server" meta:resourcekey="TabPanelDefinitionsResource1">
        <HeaderTemplate>
            Module Definition Settings
        </HeaderTemplate>
        <ContentTemplate>
            <asp:UpdatePanel ID="udpDefinitions" runat="server" Visible="False">
                <ContentTemplate>
                    <div class="cssClassCollapseWrapper">
                        <sfe:sectionheadcontrol ID="Sectionheadcontrol1" runat="server" Section="divModuleDefinitions"
                            IncludeRule="true" IsExpanded="true" Text="Module Definitions" />
                        <div id="divModuleDefinitions" runat="server" class="cssClassCollapseContent">
                            <asp:Label ID="lblModuleDefinitionsHelp" runat="server" Text="In this section, you can set update information for Module Definitions on this Module."
                                CssClass="cssClassHelpTitle" meta:resourcekey="lblModuleDefinitionsHelpResource1"></asp:Label>
                            <div class="cssClassFormWrapper">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="20%">
                                            <asp:Label ID="lblDefinition" runat="server" CssClass="cssClassFormLabel" Text="Select Definition:"
                                                meta:resourcekey="lblDefinitionResource1" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDefinitions" runat="server" AutoPostBack="True" CssClass="cssClassDropDown"
                                                OnSelectedIndexChanged="ddlDefinitions_SelectedIndexChanged" Width="150px" meta:resourcekey="ddlDefinitionsResource1" />
                                            <asp:HiddenField ID="hdnModuleDefinition" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFriendlyName" runat="server" CssClass="cssClassFormLabel" Text="Friendly Name:"
                                                meta:resourcekey="lblFriendlyNameResource1" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFriendlyName" ValidationGroup="ModuleDef" runat="server" CssClass="cssClassNormalTextBox"
                                                MaxLength="200" meta:resourcekey="txtFriendlyNameResource1" />
                                            <asp:Label ID="lblDefinitionError" runat="server" CssClass="NormalRed" Visible="False"
                                                meta:resourcekey="lblDefinitionErrorResource1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDefaultCacheTime" runat="server" Text="Default Cache Time:" CssClass="cssClassFormLabel"
                                                meta:resourcekey="lblDefaultCacheTimeResource1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDefaultCacheTime" CssClass="cssClassNormalTextBox" ValidationGroup="ModuleDef"
                                                runat="server" MaxLength="200" meta:resourcekey="txtDefaultCacheTimeResource1" />
                                            <asp:RequiredFieldValidator ID="rfvCacheTime" ValidationGroup="ModuleDef" ControlToValidate="txtDefaultCacheTime"
                                                runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="cssClassButtonWrapper">
                        <asp:ImageButton class="CommandButton" ID="imbUpdateDefinition" runat="server" ValidationGroup="ModuleDef"
                            CausesValidation="True" OnClick="imbUpdateDefinition_Click" meta:resourcekey="imbUpdateDefinitionResource1" />
                        <asp:Label Style="cursor: pointer;" ID="lblUpdateDefinition" runat="server" Text="Update Definition"
                            AssociatedControlID="imbUpdateDefinition" meta:resourcekey="lblUpdateDefinitionResource1" />
                        <asp:HiddenField ID="hdfModuleDefID" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </ajax:TabPanel>
    <ajax:TabPanel ID="TabPanelModuleControls" runat="server" meta:resourcekey="TabPanelModuleControlsResource1">
        <HeaderTemplate>
            Module Controls Settings
        </HeaderTemplate>
        <ContentTemplate>
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="Sectionheadcontrol2" runat="server" Section="divModuleControls"
                    IncludeRule="true" IsExpanded="true" Text="Module controls" />
                <div id="divModuleControls" runat="server" class="cssClassCollapseContent">
                    <asp:Label ID="lblModuleControlsHelp" runat="server" Text="In this section, you can update settings for Module Controls on this Module."
                        CssClass="cssClassHelpTitle" meta:resourcekey="lblModuleControlsHelpResource1"></asp:Label>
                    <div class="cssClassButtonWrapper">
                        <asp:ImageButton class="CommandButton" ID="imbAddControl" runat="server" CausesValidation="False"
                            OnClick="imbAddControl_Click" meta:resourcekey="imbAddControlResource1" />
                        <asp:Label Style="cursor: pointer;" ID="lblAddControl" runat="server" Text="Add Module Control"
                            AssociatedControlID="imbAddControl" meta:resourcekey="lblAddControlResource1" />
                    </div>
                    <div class="cssClassGridWrapper">
                        <asp:GridView ID="gdvControls" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="None" BorderWidth="0px" Visible="False" EmptyDataText="No Modulecontrols to Show..."
                            OnRowDataBound="gdvControls_RowDataBound" OnRowCommand="gdvControls_RowCommand"
                            OnRowDeleting="gdvControls_RowDeleting" OnRowEditing="gdvControls_RowEditing"
                            meta:resourcekey="gdvControlsResource1">
                            <Columns>
                                <asp:TemplateField HeaderText="Control" meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblControlKey" runat="server" Text='<%# Eval("ControlKey") %>' meta:resourcekey="lblControlKeyResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title" meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblControlTitle" runat="server" Text='<%# Eval("ControlTitle") %>'
                                            meta:resourcekey="lblControlTitleResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source" meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <asp:Label ID="lblControlSrc" runat="server" Text='<%# Eval("ControlSrc") %>' meta:resourcekey="lblControlSrcResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnModuleDefID" runat="server" Value='<%# Eval("ModuleDefID") %>' />
                                        <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ModuleControlID") %>'
                                            CommandName="Edit" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>'
                                            ToolTip="Edit ModuleControl" meta:resourcekey="imbEditResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cssClassColumnEdit" />
                                </asp:TemplateField>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ModuleControlID") %>'
                                            CommandName="Delete" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                                            ToolTip="Delete ModuleControl" meta:resourcekey="imbDeleteResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cssClassColumnDelete" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="cssClassHeadingOne" />
                            <RowStyle CssClass="cssClassAlternativeOdd" />
                            <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </ajax:TabPanel>
</ajax:TabContainer>
<div id="auditBar" runat="server" class="cssClassAuditBar" visible="false">
    <asp:Label ID="lblCreatedBy" runat="server" CssClass="cssClassFormLabel" Visible="False"
        meta:resourcekey="lblCreatedByResource1" />
    <br />
    <asp:Label ID="lblUpdatedBy" runat="server" CssClass="cssClassFormLabel" Visible="False"
        meta:resourcekey="lblUpdatedByResource1" />
</div>
