<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TickerEdit.ascx.cs" Inherits="Modules_Ticker_TickerEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Namespace="SageFrameAJaxEditorControls" Assembly="SageFrame.Core" TagPrefix="CustomEditor" %>
<div id="divEditTicker" class="cssClassFormWrapper">
    <div class="cssClassFormHeading">
        <asp:Label ID="lblHeading" runat="server" CssClass="cssClassFormLabel" Text=' Here you can add and update Ticker' />
    </div>
    <div class="cssClassButtonWrapper" id="divAddBtnTicker" runat="server">
        <asp:ImageButton ID="ImgAddNewTicker" runat="server" ImageUrl="~/Templates/Default/images/admin/imgadd.png"
            OnClick="ImgAddNewTicker_Click" />
        <asp:Label ID="lblAddNewDoc" runat="server" Text="Add Ticker News" AssociatedControlID="ImgAddNewTicker"></asp:Label>
    </div>
    <asp:GridView ID="gdvTickerData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        CssClass="cssClassGridWrapper" EmptyDataText="..........No Data Found.........."
        GridLines="None" Width="100%" PageSize="6" OnRowCommand="gdvTickerData_RowCommand"
        OnRowDataBound="gdvTickerData_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="TickerData">
                <ItemTemplate>
                    <asp:Label ID="lblTickNews" runat="server" Font-Bold="true" Text='<%# Eval("TickerNews")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="StartDate">
                <ItemTemplate>
                    <asp:Label ID="lblGStartDate" runat="server" Font-Bold="true" Text='<%# Eval("StartDate")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End Date">
                <ItemTemplate>
                    <asp:Label ID="lblGEndDate" runat="server" Font-Bold="true" Text='<%# Eval("EndDate")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="IsActive">
                <ItemTemplate>
                    <asp:Label ID="lblIsActive" runat="server" Font-Bold="true" Text='<%# Eval("IsActive")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("TickerID") %>'
                        CommandName="DeleteTicker" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>' />
                </ItemTemplate>
                <HeaderStyle CssClass="cssClassColumnDelete" VerticalAlign="Top" />
                <ItemStyle VerticalAlign="Top" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("TickerID") %>'
                        CommandName="EditTicker" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>' />
                </ItemTemplate>
                <HeaderStyle CssClass="cssClassColumnDelete" VerticalAlign="Top" />
                <ItemStyle VerticalAlign="Top" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="cssClassAlternativeOdd" />
        <HeaderStyle CssClass="cssClassHeadingOne" />
        <PagerStyle CssClass="cssClassPageNumber" />
    </asp:GridView>
    <div id="divTickerForm" runat="server" visible="false">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblTickerNews" runat="server" CssClass="cssClassFormLabel">Ticker Text</asp:Label>
                </td>
                <td>
                <CustomEditor:Lite ID="txtTickerNews" runat="server" ActiveMode="Design" Height="100px"
                            Width="475px" />
                    
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblStartDate" runat="server" CssClass="cssClassFormLabel">Published Date</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEndDate" runat="server" CssClass="cssClassFormLabel">End Date</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTickerIsActive" runat="server" CssClass="cssClassFormLabel">IsActive</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkTickerIsActive" runat="server" />
                </td>
            </tr>
        </table>
        <div class="cssClassButtonWrapper">
            <asp:ImageButton runat="server" ID="imgSaveTicker" OnClick="imgSaveTicker_Click"
                Style="width: 14px" />
            <asp:Label ID="lblSaveTicker" runat="server" Text="Save" AssociatedControlID="imgSaveTicker"
                CssClass="cssClassHtmlViewCursor"></asp:Label>
            <asp:ImageButton ID="imbCancelTicker" runat="server" OnClick="imbCancelTicker_Click"
                Style="height: 16px;" />
            <asp:Label ID="lblCancelTicker" runat="server" Text="Cancel" AssociatedControlID="imbCancelTicker"
                CssClass="cssClassHtmlViewCursor"></asp:Label>
        </div>
    </div>
</div>
