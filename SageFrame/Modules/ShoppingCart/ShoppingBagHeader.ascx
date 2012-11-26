<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShoppingBagHeader.ascx.cs"
    Inherits="Modules_AspxShoppingBagHeader_ShoppingBagHeader" %>

<script type="text/javascript">
    //<![CDATA[
    var showMiniShopCart='<%=ShowMiniShopCart%>';
    var allowAnonymousCheckOut="<%=AllowAnonymousCheckOut%>";
    var minOrderAmount='<%=MinOrderAmount%>';
    var allowMultipleAddChkOut='<%=AllowMultipleAddChkOut%>';
    var shoppingCartURL = '<%=ShoppingCartURL %>';
   //]]> 
</script>
<div class="cssClassCommonSideBox" style="display: block;">
    <h2 id="h2myshoppingCart">
       
    </h2>

        
<div id="divMiniShoppingCart1" class="cssClassShoppingBag"> 
    <p>
        <span id="cartBagItemCount" class="cssClassNotFound"></span>
        <span id="CartItemLoader"></span>
        </p>
</div>
<div id="divShoppingCart">
    <div class="cssClassCommonSideBoxCartTable">
        <table id="tblCartListItems" cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr><td></td></tr>
        </table>
        <a id="lnkViewShoppingCart">View Cart</a> <a id="lnkMiniShoppingCheckOut">Checkout</a>
    </div>
</div>

