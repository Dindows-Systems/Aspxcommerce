<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginStatus.ascx.cs" Inherits="Modules_Admin_LoginControl_LoginStatus" %>
<%@ Register Src="~/Controls/LoginStatus.ascx" TagName="LoginStatus" TagPrefix="uc1" %>

<div class="cssClassLoginStatusWrapper">
  <div class="cssClassLoginStatusInside">
    <div class="cssClassLoginStatus"></div>
    <div class="cssClassLoginStatusInfo">
      <ul>
        <asp:LoginView ID="LoginView1" runat="server">
          <AnonymousTemplate>
            <li>
              <uc1:LoginStatus ID="LoginStatus1" runat="server" />
            </li>
            <li><%=RegisterURL%></li>
          </AnonymousTemplate>
          <LoggedInTemplate>           
            <asp:Label ID="lblProfileURL" runat="server" Visible="False" 
                  meta:resourcekey="lblProfileURLResource1"></asp:Label>
            <li>
              <uc1:LoginStatus ID="LoginStatus2" runat="server" />
            </li>
          </LoggedInTemplate>
        </asp:LoginView>
      </ul>
      <div class="cssClassclear"></div>
    </div>
  </div>
</div>
