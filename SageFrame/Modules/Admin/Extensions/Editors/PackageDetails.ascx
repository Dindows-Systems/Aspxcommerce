<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PackageDetails.ascx.cs"
    Inherits="SageFrame.Modules.Admin.Extensions.Editors.PackageDetails" %>
<%@ Register Src="~/Controls/sectionheadcontrol.ascx" TagName="sectionheadcontrol"
    TagPrefix="sfe" %>
<div class="cssClassCollapseWrapper">
    <sfe:sectionheadcontrol ID="shcPackageSettings" runat="server" Section="divPackageSettings"
        IncludeRule="true" IsExpanded="true" Text="Package Settings" />
    <div id="divPackageSettings" runat="server" class="cssClassCollapseContent">
        <asp:Label ID="lblPackageSettingsHelp" runat="server" CssClass="cssClassHelpTitle"
            
            Text="In this section you can configure the package information for this Module." 
            meta:resourcekey="lblPackageSettingsHelpResource1"></asp:Label>
        <div class="cssClassFormWrapper">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="20%">
                        <asp:Label ID="lblPackageName" runat="server" Text="Package Name:" 
                            CssClass="cssClassFormLabel" meta:resourcekey="lblPackageNameResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPackageName" runat="server" 
                            CssClass="cssClassNormalTextBox" meta:resourcekey="txtPackageNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPackageName" runat="server" 
                            ControlToValidate="txtPackageName" ValidationGroup="vdgExtension"
                                        ErrorMessage="*" SetFocusOnError="True" 
                            CssClass="cssClasssNormalRed" meta:resourcekey="rfvPackageNameResource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDescription" runat="server" Text="Description:" 
                            CssClass="cssClassFormLabel" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="cssClassNormalTextBox"
                            Rows="5" TextMode="MultiLine" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVersion" runat="server" Text="Version:" 
                            CssClass="cssClassFormLabel" meta:resourcekey="lblVersionResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFirst" runat="server" 
                            CssClass="cssClassNormalDropDown" meta:resourcekey="ddlFirstResource1">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSecond" runat="server" 
                            CssClass="cssClassNormalDropDown" meta:resourcekey="ddlSecondResource1">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlLast" runat="server" CssClass="cssClassNormalDropDown" 
                            meta:resourcekey="ddlLastResource1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLicense" runat="server" Text="License:" 
                            CssClass="cssClassFormLabel" meta:resourcekey="lblLicenseResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLicense" runat="server" CssClass="cssClassNormalTextBox" Rows="5"
                            TextMode="MultiLine" meta:resourcekey="txtLicenseResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblReleaseNotes" runat="server" Text="Release Notes:" 
                            CssClass="cssClassFormLabel" meta:resourcekey="lblReleaseNotesResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtReleaseNotes" runat="server" CssClass="cssClassNormalTextBox"
                            Rows="5" TextMode="MultiLine" meta:resourcekey="txtReleaseNotesResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOwner" runat="server" Text="Owner:" 
                            CssClass="cssClassFormLabel" meta:resourcekey="lblOwnerResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOwner" runat="server" CssClass="cssClassNormalTextBox" 
                            meta:resourcekey="txtOwnerResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOrganization" runat="server" Text="Organization:" 
                            CssClass="cssClassFormLabel" meta:resourcekey="lblOrganizationResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrganization" runat="server" 
                            CssClass="cssClassNormalTextBox" meta:resourcekey="txtOrganizationResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUrl" runat="server" Text="Url:" CssClass="cssClassFormLabel" 
                            meta:resourcekey="lblUrlResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUrl" runat="server" CssClass="cssClassNormalTextBox" 
                            meta:resourcekey="txtUrlResource1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revUrl" runat="server" ControlToValidate="txtUrl"
                            CssClass="cssClasssNormalRed" ErrorMessage="The Url is not valid." SetFocusOnError="True"
                            
                            ValidationExpression="^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&amp;?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$" 
                            meta:resourcekey="revUrlResource1"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Email:" 
                            CssClass="cssClassFormLabel" meta:resourcekey="lblEmailResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="cssClassNormalTextBox" 
                            meta:resourcekey="txtEmailResource1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email address is not valid."
                            CssClass="cssClasssNormalRed" ControlToValidate="txtEmail" SetFocusOnError="True"
                            ValidationExpression="^[a-zA-Z][a-zA-Z0-9_-]+@[a-zA-Z]+[.]{1}[a-zA-Z]+$" 
                            meta:resourcekey="revEmailResource1"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
