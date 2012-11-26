<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sectionheadcontrol.ascx.cs" Inherits="SageFrame.Controls.sectionheadcontrol" %>
<div class="cssClassSectionHeadHeader">
    <asp:ImageButton ID="imgIcon" runat="server" EnableViewState="False"  TabIndex="-1">
    </asp:ImageButton>&nbsp;
    <asp:Label ID="lblTitle" runat="server" EnableViewState="False" CssClass="cssClassSectionHeadTitle" AssociatedControlID="imgIcon"></asp:Label>
</div>
<asp:Panel ID="pnlRule" runat="server" EnableViewState="False" CssClass="cssClassSectionHeadRule">
   <!-- <hr noshade="noshade" size="1" class="cssClassSectionHeadHr" />-->
</asp:Panel>