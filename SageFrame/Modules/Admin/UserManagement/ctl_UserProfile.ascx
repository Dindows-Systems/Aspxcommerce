<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_UserProfile.ascx.cs"
    Inherits="SageFrame.Modules.Admin.UserManagement.ctl_UserProfile" %>

<div runat="server" id="divForm">
  <div class="cssClassFormWrapper">
    <asp:Panel ID="pnlForm" runat="server" Width="100%">
      </asp:Panel>
  </div>
  <div class="cssClassButtonWrapper">
    <asp:ImageButton ID="imbSave" runat="server" ToolTip="Save" OnClick="imbSave_Click"
                ValidationGroup="UserProfile" />
    <asp:Label ID="lblSave" AssociatedControlID="imbSave" Style="cursor: pointer" runat="server"
                Text="Save"></asp:Label>
  </div>
</div>
