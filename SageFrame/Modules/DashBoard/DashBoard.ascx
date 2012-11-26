<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DashBoard.ascx.cs" Inherits="SageFrame.Modules.DashBoard.DashBoard" %>
<div>
    <asp:Repeater ID="rptDashBoard" runat="server">
        <HeaderTemplate>
            <div class="cssClassDashBoard">
                <ul>
        </HeaderTemplate>
        <FooterTemplate>
            </ul></div>
        </FooterTemplate>
        <ItemTemplate>
            <li><a id="hypPageURL" runat="server" href='<%# Eval("Url") %>'><span class="cssClassImageHeight">
                <asp:Image ID="imgDisplayImage" ImageAlign="Middle" ToolTip='<%# Eval("PageName") %>'
                    runat="server" ImageUrl='<%# Eval("IconFile") %>' 
                    meta:resourcekey="imgDisplayImageResource1" />
            </span>
                <asp:Label ID="lblPageName" runat="server" Text='<%# Eval("PageName") %>' 
                    meta:resourcekey="lblPageNameResource1"></asp:Label>
            </a>
                <asp:Label ID="lblSEOName" runat="server" Visible="False" 
                    Text='<%# Eval("PageName") %>' meta:resourcekey="lblSEONameResource1"></asp:Label>
            </li>
        </ItemTemplate>
    </asp:Repeater>
    <div class="clear">
    </div>
</div>
