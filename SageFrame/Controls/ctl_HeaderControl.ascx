<%@ Control Language="C#" AutoEventWireup="true" Inherits="Modules_ctl_HeaderControl"
    CodeFile="ctl_HeaderControl.ascx.cs" %>
<%@ Register src="~/Controls/ctl_Logo.ascx" tagname="ctl_Logo" tagprefix="uc1" %>

<div class="cssClassLogo" id="divLogo" runat="server">
  <asp:PlaceHolder ID="phdLogo" runat="server"></asp:PlaceHolder>
</div>
<div class="cssClassRegister">
  <div class="cssClassTopMenu" id="divTopMenu" runat="server">
    <asp:PlaceHolder ID="phdLoginStatus" runat="server"></asp:PlaceHolder>
  </div>
  <div style="clear:both;"></div>
  <div class="cssClassSubscribe" id="divSubscribe" runat="server" >
    <asp:PlaceHolder ID="phdSubscribe" runat="server"></asp:PlaceHolder>
  </div>
</div>
