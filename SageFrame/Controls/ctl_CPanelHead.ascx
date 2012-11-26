<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_CPanelHead.ascx.cs" Inherits="Controls_ctl_CPanelHead" %>
<div id="divadminHeaderContent" runat="server">
    <div class="adminLogo">
        <h1>
            <asp:Literal ID="litCPanleTitle" runat="server"></asp:Literal></h1>
    </div>    
    <div class="version">
        <asp:Label ID="lblVersion" runat="server" Text="Version"></asp:Label>&nbsp;
        <asp:Literal ID="litSFVersion" runat="server"></asp:Literal>
    </div>
    <div class="cssClassclear">
    </div>
</div>
