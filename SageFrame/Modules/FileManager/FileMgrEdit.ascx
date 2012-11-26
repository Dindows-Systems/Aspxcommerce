<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileMgrEdit.ascx.cs" Inherits="Modules_FileManager_FileMgrEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="cssClassFormWrapper">
    <h2 class="cssClassHeader">
        Add Root Folders</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="cssClassButtonWrapper">
                <asp:Button ID="btnShowPopUp" runat="server" CssClass="cssClassBtn" Text="Add Root Folder"
                    OnClick="btnShowPopUp_Click" />
            </div>
            <span>
                <asp:Label ID="lblSelectPageSize" runat="server" CssClass="cssClassFormLabel">Select PageSize:</asp:Label>
            </span>
            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="cssClasslistddl" AutoPostBack="True"
                OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
            </asp:DropDownList>
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
    <cc1:modalpopupextender id="PopUp" runat="server" behaviorid="programmaticModalPopupBehavior"
        dropshadow="False" runat="server" targetcontrolid="popUpBtn1" backgroundcssclass="modalBackground"
        popupcontrolid="pnlPopUp1" cancelcontrolid="imgClosePopUp">
    </cc1:modalpopupextender>
    <asp:Panel ID="pnlPopUp1" CssClass="cssClassDirList" Style="display: none" runat="server">
        <asp:Panel ID="Panel2" CssClass="controlDiv" runat="server" Width="100%" Style="cursor: move;
            text-align: center; height: 15px">
            <label class="stdHeaderLabel">
                Select Directory</label></asp:Panel>
        <span class="closePopUp">
            <asp:Image runat="server" ID="imgClosePopUp" ImageUrl="~/Modules/FileManager/images/cancel.png" /></span>
        <asp:Button ID="popUpBtn1" runat="server" Style="display: none" Text="OK" />
        <div id="divDirList">
            <asp:TreeView ID="TreeView1" runat="server" SelectedNodeStyle-CssClass="TreeSelectedNode"
                OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" SelectedNodeStyle-Font-Underline="true">
            </asp:TreeView>
        </div>
    </asp:Panel>
</div>
