<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShoppingOptions.ascx.cs"
    Inherits="Modules_AspxShoppingOptions_ShoppingOptions" %>

<script type="text/javascript">
    //<![CDATA[
    var upperLimit = '<%=ShoppingOptionRange %>';
    var lblPriceTitle = '<%=lblShoppingTitle.ClientID %>';   
    //]]>
</script>

<div class="cssClassCommonSideBox">
    <h2>
        <asp:Label ID="lblShoppingTitle" runat="server" Text="Shopping Options" CssClass="cssClassShoppingOptions"></asp:Label></h2>   
    <div class="cssClassCommonSideBoxTable">
        <table id="tblShoppingOptionsByPrice" width="100%" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr><td></td></tr>
            </tbody>
        </table>
    </div>
</div>
