<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_Extensions.ascx.cs"
    Inherits="SageFrame.Modules.Admin.Extensions.ctl_Extensions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<h2 class="cssClassFormHeading">
        <asp:Label ID="lblModulesManagement" runat="server" Text="Modules Management" 
            meta:resourcekey="lblModulesManagementResource1"></asp:Label></h2>
<asp:PlaceHolder ID="ExtensionPlaceHolder" runat="server">

    <div class="cssClassFormWrapper">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td width="12%">
                    <asp:Label ID="lblSearchModule" runat="server" CssClass="cssClassFormLabel" 
                        Text="Search Module :" meta:resourcekey="lblSearchModuleResource1"></asp:Label>
                </td>
                <td width="1%">
                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="cssClassNormalTextBox" 
                        meta:resourcekey="txtSearchTextResource1"></asp:TextBox>
                </td>
                <td class="cssClassFormLabel_padding" width="43%">
                    <asp:ImageButton ID="imgSearch" runat="server" OnClick="imgSearch_Click" 
                        ToolTip="Search" meta:resourcekey="imgSearchResource1" />
                    <asp:Label ID="lblSearch" runat="server" Text="Search" AssociatedControlID="imgSearch"
                        Style="cursor: pointer;" CssClass="cssClassFormLabel" 
                        meta:resourcekey="lblSearchResource1"></asp:Label>
                </td>
                <td width="10%">
                    <asp:Label ID="lblSRow" runat="server" Text="Show rows :" 
                        CssClass="cssClassFormLabel" meta:resourcekey="lblSRowResource1"></asp:Label>
                </td>
                <td width="6%">
                    <asp:DropDownList ID="ddlRecordsPerPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRecordsPerPage_SelectedIndexChanged"
                        CssClass="cssClasslistddl cssClassPageSize" 
                        meta:resourcekey="ddlRecordsPerPageResource1">
                        <asp:ListItem Value="10" meta:resourcekey="ListItemResource1">10</asp:ListItem>
                        <asp:ListItem Value="25" meta:resourcekey="ListItemResource2">25</asp:ListItem>
                        <asp:ListItem Value="50" meta:resourcekey="ListItemResource3">50</asp:ListItem>
                        <asp:ListItem Value="100" meta:resourcekey="ListItemResource4">100</asp:ListItem>
                        <asp:ListItem Value="150" meta:resourcekey="ListItemResource5">150</asp:ListItem>
                        <asp:ListItem Value="200" meta:resourcekey="ListItemResource6">200</asp:ListItem>
                        <asp:ListItem Value="250" meta:resourcekey="ListItemResource7">250</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="cssClassButtonWrapper">
        <asp:ImageButton ID="imbBtnSaveChanges" runat="server" ToolTip="Save changes" 
            OnClick="imbBtnSaveChanges_Click" 
            meta:resourcekey="imbBtnSaveChangesResource1" />
        <asp:Label ID="lblSaveChanges" runat="server" Text="Save changes" AssociatedControlID="imbBtnSaveChanges"
            Style="cursor: pointer;" meta:resourcekey="lblSaveChangesResource1"></asp:Label>
        <asp:ImageButton ID="imbInstallModule" runat="server" CausesValidation="False" 
            OnClick="imbInstallModule_Click" meta:resourcekey="imbInstallModuleResource1" />
        <asp:Label Style="cursor: pointer;" ID="lblInstallModule" runat="server" Text="Install Module"
            AssociatedControlID="imbInstallModule" 
            meta:resourcekey="lblInstallModuleResource1" />
        <asp:ImageButton ID="imbCreateNewModule" runat="server" 
            OnClick="imbCreateNewModule_Click" 
            meta:resourcekey="imbCreateNewModuleResource1" />
        <asp:Label Style="cursor: pointer;" ID="lblCreateNewModule" runat="server" Text="Create New Module"
            AssociatedControlID="imbCreateNewModule" 
            meta:resourcekey="lblCreateNewModuleResource1" />
    </div>
    <div class="cssClassGridWrapper">
        <asp:GridView ID="gdvExtensions" runat="server" DataKeyNames="ModuleID" AutoGenerateColumns="False"
            GridLines="None" Width="100%" EmptyDataText="No Record to Show..." OnRowCommand="gdvExtensions_RowCommand"
            OnPageIndexChanging="gdvExtensions_PageIndexChanging" OnRowDataBound="gdvExtensions_RowDataBound"
            OnRowDeleting="gdvExtensions_RowDeleting" OnRowEditing="gdvExtensions_RowEditing"
            OnRowUpdating="gdvExtensions_RowUpdating" AllowPaging="true" 
            meta:resourcekey="gdvExtensionsResource1">
            <Columns>
                <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnModuleID" runat="server" Value='<%# Eval("ModuleID") %>' />
                        <asp:LinkButton ID="lnkName" runat="server" CommandArgument='<%# Eval("ModuleID") %>'
                            CommandName="Edit" Text='<%# Eval("FriendlyName") %>' 
                            meta:resourcekey="lnkNameResource1"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" CssClass="cssClassFormLabel" 
                            Text='<%# Eval("PackageType") %>' meta:resourcekey="lblTypeResource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" 
                    meta:resourcekey="TemplateFieldResource3">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" CssClass="cssClassFormLabel" 
                            Text='<%# Eval("Description") %>' meta:resourcekey="lblDescriptionResource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Version" 
                    HeaderStyle-CssClass="cssClassColumnVersion" 
                    meta:resourcekey="TemplateFieldResource4">
                    <ItemTemplate>
                        <asp:Label ID="lblVersion" runat="server" CssClass="cssClassFormLabel" 
                            Text='<%# Eval("Version") %>' meta:resourcekey="lblVersionResource1"></asp:Label>
                    </ItemTemplate>

<HeaderStyle CssClass="cssClassColumnVersion"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Use" 
                    HeaderStyle-CssClass="cssClassColumnInUse" 
                    meta:resourcekey="TemplateFieldResource5">
                    <ItemTemplate>
                        <asp:Label ID="lblInUse" runat="server" CssClass="cssClassFormLabel" 
                            Text='<%# ConvertToYesNo(Eval("InUse").ToString()) %>' 
                            meta:resourcekey="lblInUseResource1"></asp:Label>
                    </ItemTemplate>

<HeaderStyle CssClass="cssClassColumnInUse"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                    <HeaderTemplate>
                        <input id="chkBoxIsActiveHeader" runat="server" class="cssCheckBoxIsActiveHeader"
                            type="checkbox" /> <asp:Label ID="lblIsActive" runat="server" 
                            Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnIsActive" runat="server" Value='<%# Eval("IsActive") %>' />
                        <asp:HiddenField ID="hdnIsAdmin" runat="server" Value='<%# Eval("IsAdmin") %>' />
                        <input id="chkBoxIsActiveItem" runat="server" class="cssCheckBoxIsActiveItem" type="checkbox" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="cssClassColumnIsActive" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="cssClassColumnEdit" 
                    meta:resourcekey="TemplateFieldResource7">
                    <ItemTemplate>
                        <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ModuleID") %>'
                            CommandName="Edit" 
                            ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>' 
                            ToolTip="Edit Module" meta:resourcekey="imbEditResource1"/>
                    </ItemTemplate>

<HeaderStyle CssClass="cssClassColumnEdit"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="cssClassColumnDelete" 
                    meta:resourcekey="TemplateFieldResource8">
                    <ItemTemplate>
                        <asp:ImageButton ID="imbDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ModuleID") %>'
                            CommandName="Delete" 
                            ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>' 
                            ToolTip="Delete Module" meta:resourcekey="imbDeleteResource1"/>
                    </ItemTemplate>

<HeaderStyle CssClass="cssClassColumnDelete"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="cssClassPageNumber" />
            <HeaderStyle CssClass="cssClassHeadingOne" />
            <RowStyle CssClass="cssClassAlternativeOdd" />
            <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
        </asp:GridView>
    </div>
</asp:PlaceHolder>
