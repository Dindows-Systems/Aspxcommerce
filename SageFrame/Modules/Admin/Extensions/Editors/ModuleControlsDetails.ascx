<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModuleControlsDetails.ascx.cs"
    Inherits="SageFrame.Modules.Admin.Extensions.Editors.ModuleControlsDetails" %>
<%@ Register Src="~/Controls/sectionheadcontrol.ascx" TagName="sectionheadcontrol"
    TagPrefix="sfe" %>
<asp:UpdatePanel ID="udpModuleControlSettings" runat="server">
    <ContentTemplate>
        <div class="cssClassModuleWrapper">
            <div class="cssClassCollapseWrapper">
                <sfe:sectionheadcontrol ID="shcModuleControlSettings" runat="server" CssClass="Head"
                    Section="divModuleControlSetting" IncludeRule="true" IsExpanded="true" Text="Module Control Settings" />
                <div id="divModuleControlSetting" runat="server" class="cssClassCollapseContent">
                    <asp:Label ID="lblModuleControlSettingsHelp" runat="server" Text="In this section, you can set up more advanced settings for Module Controls on this Module."
                        CssClass="cssClassHelpTitle" 
                        meta:resourcekey="lblModuleControlSettingsHelpResource1"></asp:Label>
                    <div class="cssClassFormWrapper">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr id="rowModuleEdit" runat="server" visible="False">
                                <td width="20%" runat="server">
                                    <asp:Label ID="lblModuleEdit" runat="server" Text="Module:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td runat="server">
                                    <asp:Label ID="lblModuleD" runat="server" CssClass="cssClassFormLabelField"></asp:Label>
                                </td>
                            </tr>
                            <tr id="rowDefinitionEdit" runat="server" visible="False">
                                <td runat="server">
                                    <asp:Label ID="lblDefinitionEdit" runat="server" Text="Definition:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td runat="server">
                                    <asp:Label ID="lblDefinitionD" runat="server" CssClass="cssClassFormLabelField"></asp:Label>
                                </td>
                            </tr>
                            <tr id="rowSource" runat="server" visible="False">
                                <td runat="server">
                                    <asp:Label ID="lblSource" runat="server" Text="Source:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td valign="top" style="width: 575px" runat="server">
                                    <asp:DropDownList ID="ddlSource" runat="server" CssClass="cssClassDropDown" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlSource_SelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblKey" runat="server" Text="Key:" CssClass="cssClassFormLabel" 
                                        meta:resourcekey="lblKeyResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKey" runat="server" CssClass="cssClassNormalTextBox" 
                                        meta:resourcekey="txtKeyResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvModulekey" runat="server" ControlToValidate="txtKey"
                                        ValidationGroup="vdgExtension" ErrorMessage="*" SetFocusOnError="True" 
                                        CssClass="cssClasssNormalRed" meta:resourcekey="rfvModulekeyResource1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTitle" runat="server" Text="Title:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblTitleResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="cssClassNormalTextBox" 
                                        meta:resourcekey="txtTitleResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvModuleTitle" runat="server" ControlToValidate="txtTitle"
                                        ValidationGroup="vdgExtension" ErrorMessage="*" SetFocusOnError="True" 
                                        CssClass="cssClasssNormalRed" meta:resourcekey="rfvModuleTitleResource1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblType" runat="server" Text="Type:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblTypeResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="cssClassDropDown" 
                                        meta:resourcekey="ddlTypeResource1" />
                                    <asp:Label ID="lblErrorControlType" runat="server" CssClass="cssClasssNormalRed"
                                        Text="*" Visible="False" meta:resourcekey="lblErrorControlTypeResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr id="rowDisplayOrder" runat="server" visible="False">
                                <td runat="server">
                                    <asp:Label ID="lblDisplayOrder" runat="server" Text="Display Order:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td runat="server">
                                    <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="cssClassNormalTextBox"
                                        MaxLength="2" Text="0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIcon" runat="server" Text="Icon:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblIconResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlIcon" runat="server" CssClass="cssClassDropDown" 
                                        meta:resourcekey="ddlIconResource1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblHelpURL" runat="server" Text="Help URL:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblHelpURLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHelpURL" runat="server" CssClass="cssClassNormalTextBox" 
                                        meta:resourcekey="txtHelpURLResource1"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revHelpUrl" runat="server" ControlToValidate="txtHelpURL"
                                        CssClass="cssClasssNormalRed" ErrorMessage="The Help Url is not valid." SetFocusOnError="True"
                                        
                                        ValidationExpression="^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&amp;?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$" 
                                        meta:resourcekey="revHelpUrlResource1"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSupportsPartialRendering" runat="server" Text="Supports Partial Rendering?"
                                        CssClass="cssClassFormLabel" 
                                        meta:resourcekey="lblSupportsPartialRenderingResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkSupportsPartialRendering" runat="server" 
                                        CssClass="cssClassCheckBox" 
                                        meta:resourcekey="chkSupportsPartialRenderingResource1" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div runat="server" id="pUpdatePane" visible="False" 
                class="cssClassButtonWrapper">
                <asp:ImageButton ID="imbUpdateModlueControl" runat="server"
                    OnClick="imbUpdateModlueControl_Click" ValidationGroup="vdgExtension" 
                    meta:resourcekey="imbUpdateModlueControlResource1" />
                <asp:Label Style="cursor: hand;" ID="lblCreateModule" runat="server" Text="Save"
                    AssociatedControlID="imbUpdateModlueControl" 
                    meta:resourcekey="lblCreateModuleResource1" />
                <asp:ImageButton ID="imbCancelModlueControl" runat="server" CausesValidation="False"
                    OnClick="imbCancelModlueControl_Click" 
                    meta:resourcekey="imbCancelModlueControlResource1" />
                <asp:Label Style="cursor: hand;" ID="lblCancel" runat="server" Text="Cancel " 
                    AssociatedControlID="imbCancelModlueControl" 
                    meta:resourcekey="lblCancelResource1" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
