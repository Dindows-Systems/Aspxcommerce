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

<div id="divMiniShoppingCart" class="cssClassShoppingCart">
    <p>
        <a onclick="if(!this.disabled){ShopingBag.showShoppingCart();};" href="javascript:void(0);"
            id="lnkShoppingBag">
            <img id="fullShoppingBag" src="" width="32" height="32" alt="Shopping Basket" title="Shopping Basket" align="right" /></a>
        <img id="emptyShoppingBag" src="" width="32" height="32" alt="Shopping Basket" title="Shopping Basket" align="right" />
    </p>
    <p>
        <span id="cartItemCount"></span>
        <img id="imgarrow" src="" alt="down" title="down" height="10px" width="10px" onclick="if(!this.disabled){ShopingBag.showShoppingCart();}"/></p>
</div>
<div id="ShoppingCartPopUp" style="display:none;">
    <h2>
        <asp:Label ID="lblMyCartTitle" runat="server" Text="My Cart Item(s)" CssClass="cssClassShoppingBag"></asp:Label></h2>
    <div class="cssClassCommonSideBoxTable">
        <table id="tblListCartItems" cellspacing="0" cellpadding="0" border="0" width="100%">
            <tr><td></td></tr>
        </table>
        <a id="lnkViewCart">View Cart</a> <a id="lnkMiniCheckOut">Checkout</a>
    </div>
</div>
<div id="popuprel4" class="popupbox cssClassCenterPopBox">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span>Close</span></button>
    </div>
    <h2>
        <asp:Label ID="lblTitleCheckoutOpt" runat="server" Text="Checkout using one of the following options: "></asp:Label>
    </h2>
    <div id="divMiniCheckoutTypes" class="cssClassFormWrapper">
    </div>
</div>
<%--<input type="hidden" id="hdnItemCostVariants" />--%>
