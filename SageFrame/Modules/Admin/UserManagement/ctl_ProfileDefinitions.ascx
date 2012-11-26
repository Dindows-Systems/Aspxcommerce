<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_ProfileDefinitions.ascx.cs"
    Inherits="SageFrame.Modules.Admin.UserManagement.ctl_ProfileDefinitions" %>
    <asp:UpdatePanel ID="udpProDef" runat="server">
    <ContentTemplate>
    <h2 class="cssClassFormHeading">
        <asp:Label ID="lblProfileDefinition" runat="server" Text="User Profile Definition"></asp:Label></h2>
    
<div id="divGridViewWrapper" runat="server" class="cssClassGridViewWrraper">
    <div class="cssClassButtonWrapper">
        <asp:ImageButton ID="imbAddNew" ToolTip="Add New" runat="server" OnClick="imbAddNew_Click" />
        <asp:Label ID="lblAddNew" runat="server" Style="cursor: pointer" AssociatedControlID="imbAddNew"
            Text="Add New"></asp:Label>
        <asp:ImageButton ID="imbSaveChanges" ToolTip="Save Changes" runat="server" OnClick="imbSaveChanges_Click" />
        <asp:Label ID="lblSaveChanges" AssociatedControlID="imbSaveChanges" Style="cursor: pointer"
            runat="server" Text="Save Changes"></asp:Label>
        <asp:ImageButton ID="imbRefresh" ToolTip="Refresh" runat="server" OnClick="imbRefresh_Click" />
        <asp:Label ID="lblRefresh" runat="server" Style="cursor: pointer" AssociatedControlID="imbRefresh"
            Text="Refresh"></asp:Label>
    </div>
    <div class="cssClassGridWrapper">
        <asp:GridView ID="gdvList" runat="server" AutoGenerateColumns="False" CssClass="tablestyle"
            GridLines="None" EmptyDataText="No Record to Show..." Width="100%" AllowPaging="True"
            PageSize="15" OnPageIndexChanging="gdvList_PageIndexChanging" OnRowCommand="gdvList_RowCommand"
            OnRowDataBound="gdvList_RowDataBound" OnRowDeleting="gdvList_RowDeleting" OnRowEditing="gdvList_RowEditing"
            OnRowUpdating="gdvList_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="Caption">
                    <ItemTemplate>
                         <asp:LinkButton ID="lnkUsername" runat="server" CommandArgument='<%#Eval("ProfileID") %>'
                            CommandName="Edit" Text='<%# Eval("Name")%>'></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label ID="lblPropertyTypeName" runat="server" Text='<%#Eval("PropertyTypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IsActive" HeaderStyle-CssClass="cssClassColumnIsActive">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%#(Eval("IsActive")) %>'
                            class="cssCheckBoxIsActiveItem" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="AddedOn" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Added On"
                    HeaderStyle-CssClass="cssClassColumnAddedOn" />
                <asp:BoundField DataField="UpdatedOn" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Updated On"
                    HeaderStyle-CssClass="cssClassColumnUpdatedOn" />
                <asp:TemplateField ShowHeader="False" HeaderStyle-CssClass="cssClassColumnOrder">
                    <ItemTemplate>
                        <div>
                            <asp:ImageButton ID="imgUp" runat="server" CausesValidation="False" CommandArgument='<%#Eval("DisplayOrder") %>'
                                CommandName="Up" ImageUrl='<%# GetTemplateImageUrl("imgup.png", true) %>' ToolTip="Up" />
                        </div>
                        <div>
                            <asp:ImageButton ID="imgDown" runat="server" CausesValidation="False" CommandArgument='<%#Eval("DisplayOrder") %>'
                                CommandName="Down" ImageUrl='<%# GetTemplateImageUrl("imgdown.png", true) %>'
                                ToolTip="Down" /></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderStyle-CssClass="cssClassColumnEdit">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%#Eval("ProfileID") %>'
                            CommandName="Edit" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>'
                            ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderStyle-CssClass="cssClassColumnDelete">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandArgument='<%#Eval("ProfileID") %>'
                            CommandName="Delete" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                            ToolTip="Delete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="cssClassHeadingOne" />
            <RowStyle CssClass="cssClassAlternativeOdd" />
            <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
        </asp:GridView>
    </div>
</div>
<div runat="server" id="divForm">
    <div class="cssClassFormWrapper">
        <table class="cssClassForm">
            <tr>
                <td width="20%">
                    <asp:Label ID="lblCaption" runat="server" Text="Caption :" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCaption" runat="server" MaxLength="50" CssClass="cssClassNormalTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPropertyType" runat="server" Text="Property Type :" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPropertyType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPropertyType_SelectedIndexChanged"
                        CssClass="cssClassDropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDataType" runat="server" Text="Data Type :" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDataType" runat="server" CssClass="cssClassDropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIsRequired" runat="server" Text="Is Required :" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkIsRequred" runat="server" />
                </td>
            </tr>
            <tr id="trListPropertyValue" runat="server">
                <td width="20%" valign="top">
                    <asp:Label ID="lblPropertyValue" runat="server" Text="Value :" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td>
                    <table class="cssTableRowFormTable">
                        
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPropertyValue" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="cssClassButtonWrapper">
                                    <asp:ImageButton ToolTip="Add" ID="imbAdd" runat="server" OnClick="imbAdd_Click" />
                                    <asp:Label ID="lblAdd" runat="server" Style="cursor: pointer" AssociatedControlID="imbAdd"
                                        Text="Add"></asp:Label>
                                    <asp:ImageButton ToolTip="Delete" ID="imbDelete" runat="server" OnClick="imbDelete_Click" />
                                    <asp:Label ID="Label1" runat="server" Style="cursor: pointer" AssociatedControlID="imbDelete"
                                        Text="Delete"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="lstvPropertyValue" runat="server" SelectionMode="Multiple" CssClass="cssClassFormList">
                                </asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="cssClassButtonWrapper">
        <asp:ImageButton ID="imbSave" runat="server" ToolTip="Save" OnClick="imbSave_Click" />
        <asp:Label ID="lblSave" runat="server" Style="cursor: pointer" AssociatedControlID="imbSave"
            Text="Save"></asp:Label>
        <asp:ImageButton ToolTip="Cancel" ID="imbCancel" runat="server" OnClick="imbCancel_Click" />
        <asp:Label ID="lblCancel" runat="server" Style="cursor: pointer" AssociatedControlID="imbCancel"
            Text="Cancel"></asp:Label>
    </div>
</div>
</ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID="imbSaveChanges" />
</Triggers>
    </asp:UpdatePanel>