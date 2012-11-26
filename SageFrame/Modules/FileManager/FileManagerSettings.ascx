<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileManagerSettings.ascx.cs"
    Inherits="Modules_FileManager_FileManagerSettings" %>
<div class="cssClassFormWrapper">
    <h2 class="cssClassFormHeading">
        Permitted Extensions</h2>
    <asp:TextBox ID="txtExtensions" runat="server" Height="50px" TextMode="MultiLine"
        Width="250px"></asp:TextBox>
    <div class="cssClassButtonWrapper">
        <asp:Button ID="btnAddExtension" runat="server" CssClass="cssClassBtn" Text="Update"
            OnClick="btnAddExtension_Click" />
    </div>
</div>
