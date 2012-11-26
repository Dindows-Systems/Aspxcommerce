<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderControl.ascx.cs"
    Inherits="Modules_AspxHeaderControl_HeaderControl" %>

<script type="text/javascript">
    //<![CDATA[
    var myAccountURLSetting = '<%=MyAccountURL%>';
    var shoppingCartURLSetting = '<%=ShoppingCartURL%>';
    var wishListURLSetting = '<%=WishListURL%>';
    var allowWishListSetting = '<%=AllowWishList %>';
    var minOrderAmountSetting = '<%=MinOrderAmount%>';
    var allowMultipleShippingSetting = '<%=AllowMultipleShipping%>';
    var allowAnonymousCheckOutSetting = '<%=AllowAnonymousCheckOut%>';
    var frmLogin = '<%=FrmLogin%>';
    //]]>
</script>

<div class="cssClassLoginStatusWrapper">
    <div class="cssClassLoginStatusInfo">
        <ul>
            <li class="cssClassAccount"><a id="lnkMyAccount">My Account</a></li>
            <li class="cssClassWishList"><a id="lnkMyWishlist"></a></li>
            <li class="cssClassCart"><a id="lnkMyCart"></a></li>
            <li class="cssClassCheckOut"><a id="lnkCheckOut" rel="popuprel3" href="#">Checkout</a></li>
        </ul>
        <div class="cssClassclear">
        </div>
    </div>
</div>
<div id="popuprel3" class="popupbox cssClassCenterPopBox">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span>Close</span></button>
    </div>
    <h2>
        <asp:Label ID="lblCheckoutOptions" runat="server" Text="Checkout using one of the following options: "></asp:Label>
    </h2>
    <div id="divCheckoutTypes" class="cssClassFormWrapper">
    </div>
</div>