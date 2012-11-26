<%@ Control Language="C#" AutoEventWireup="true" Inherits="SageFrame.Web.Modules_ctl_SearchBox"
    CodeFile="ctl_SearchBox.ascx.cs" %>
<ul>
    <li>
        <asp:TextBox ID="txtSearchTerms" runat="server" CssClass="inputbox" SkinID="SearchBoxText" />&nbsp;
    </li>
    <li>
        <asp:Button runat="server" ID="btnSearch" CssClass="searchButton" OnClick="btnSearch_Click"
            Text="Search" />
    </li>
</ul>
