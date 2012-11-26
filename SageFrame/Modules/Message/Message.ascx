<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Message.ascx.cs" Inherits="Modules_Message_Message" %>

<div id="divSageMessageWrapper" runat="server" class="cssClassMessageWrapper" enableviewstate="false">
  <asp:UpdatePanel ID="udpSageMessage" runat="server">
    <ContentTemplate>
      <div id="divUdpSageMessage" runat="server" enableviewstate="False">
        <div class="cssClassMessageBox" enableviewstate="False" id="divUdpMessage" runat="server"
                        style="display: none;">
          <asp:Label ID="lblUdpSageMesageTitle" CssClass="cssClassMessageTitle" EnableViewState="False"
                                                            runat="server" 
                meta:resourcekey="lblUdpSageMesageTitleResource1"></asp:Label>
          <asp:Label ID="lblUdpSageMesageCustom" CssClass="cssClassCustomMessage" EnableViewState="False"
                                                            runat="server" 
                meta:resourcekey="lblUdpSageMesageCustomResource1"></asp:Label>
          <asp:Label ID="lblUdpSageMesageDetail" CssClass="cssClassDetailMessage" EnableViewState="False"
                                                            runat="server" 
                meta:resourcekey="lblUdpSageMesageDetailResource1"></asp:Label>
        </div>
      </div>   
    </ContentTemplate>
  </asp:UpdatePanel>
</div>
