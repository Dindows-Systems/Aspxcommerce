<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditModuleDefinition.ascx.cs"
    Inherits="SageFrame.Modules.Admin.Extensions.Editors.EditModuleDefinition" %>
<%@ Register Src="PackageDetails.ascx" TagName="PackageDetails" TagPrefix="uc1" %>
<%@ Register Src="ModuleControlsDetails.ascx" TagName="ModuleControlsDetails" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/sectionheadcontrol.ascx" TagName="sectionheadcontrol"
    TagPrefix="sfe" %>
<div class="cssClassModuleWrapper">
    <asp:Panel ID="pnlNewModuleSettings" runat="server" 
        meta:resourcekey="pnlNewModuleSettingsResource1">
        <div class="cssClassCollapseWrapper">
            <asp:UpdatePanel ID="udpModuleSettings" runat="server">
                <ContentTemplate>
                    <sfe:sectionheadcontrol ID="shcModuleSettings" runat="server" Section="divModuleSettings"
                        IncludeRule="true" IsExpanded="true" Text="Module Settings" />
                    <div id="divModuleSettings" runat="server" class="cssClassCollapseContent">
                        <asp:Label ID="lblModuleSettingsHelp" runat="server" Text="In this section, you can set up more advanced settings for Module Controls on this Module."
                            CssClass="cssClassHelpTitle" 
                            meta:resourcekey="lblModuleSettingsHelpResource1"></asp:Label>
                        <div class="cssClassFormWrapper">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="20%">
                                        <asp:Label ID="lblCreate" runat="server" Text="Create Module From:" 
                                            CssClass="cssClassFormLabel" meta:resourcekey="lblCreateResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCreate" runat="server" CssClass="cssClassDropDown" 
                                            Width="300px" meta:resourcekey="ddlCreateResource1">
                                            <asp:ListItem Value="Control" Text="Control" 
                                                meta:resourcekey="ListItemResource1" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblOwnerFolder" runat="server" Text="Owner Folder:" 
                                            CssClass="cssClassFormLabel" meta:resourcekey="lblOwnerFolderResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlOwner" runat="server" CssClass="cssClassDropDown" Width="300px"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlOwner_SelectedIndexChanged" 
                                            meta:resourcekey="ddlOwnerResource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblModuleFolder" runat="server" Text="Module Folder:" 
                                            CssClass="cssClassFormLabel" meta:resourcekey="lblModuleFolderResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlModule" runat="server" CssClass="cssClassDropDown" Width="300px"
                                            AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddlModule_SelectedIndexChanged" 
                                            meta:resourcekey="ddlModuleResource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFiles" runat="server" Text="Source:" 
                                            CssClass="cssClassFormLabel" meta:resourcekey="lblFilesResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFiles" runat="server" Width="300px" 
                                            CssClass="cssClassDropDown" meta:resourcekey="ddlFilesResource1" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="divPackageSettingsHolder" runat="server">
                <uc1:PackageDetails ID="PackageDetails1" runat="server" />
            </div>
            <div id="divmoduleControlSettingsHolder" runat="server">
                <uc2:ModuleControlsDetails ID="ModuleControlsDetails1" runat="server" />
            </div>
        </div>
        <div class="cssClassButtonWrapper">
            <asp:ImageButton ID="imbCreate" runat="server" OnClick="imbCreate_Click" 
                ValidationGroup="vdgExtension" meta:resourcekey="imbCreateResource1"/>
            <asp:Label Style="cursor: pointer;" ID="lblCreateModule" runat="server" Text="Save Module"
                AssociatedControlID="imbCreate" 
                meta:resourcekey="lblCreateModuleResource1" />
            <asp:ImageButton ID="imbBack" runat="server" CausesValidation="False" 
                OnClick="imbBack_Click" meta:resourcekey="imbBackResource1" />
            <asp:Label Style="cursor: pointer;" ID="lblBack" runat="server" Text="Cancel" 
                AssociatedControlID="imbBack" meta:resourcekey="lblBackResource1" />
        </div>
    </asp:Panel>
</div>
