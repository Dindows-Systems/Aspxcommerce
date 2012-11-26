<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RelatedItemsInCart.ascx.cs"
    Inherits="Modules_AspxRelatedItemsInCart_RelatedItemsInCart" %>

<script type="text/javascript">
    //<![CDATA[
    var RelatedItemsInCart;
    $(function() {
        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var customerId = AspxCommerce.utils.GetCustomerID();
        var ip = AspxCommerce.utils.GetClientIP();
        var countryName = AspxCommerce.utils.GetAspxClientCoutry();
        var sessionCode = AspxCommerce.utils.GetSessionCode();
        var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
        var relatedItemCount = '<%=NoOfRelatedItemsInCart%>';
        var RelatedUpSellCrossSellItems = '';
        RelatedItemsInCart = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: "json",
                baseURL: aspxservicePath + "AspxCommerceWebService.asmx/",
                url: "",
                method: ""
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: RelatedItemsInCart.config.type,
                    contentType: RelatedItemsInCart.config.contentType,
                    cache: RelatedItemsInCart.config.cache,
                    async: RelatedItemsInCart.config.async,
                    data: RelatedItemsInCart.config.data,
                    dataType: RelatedItemsInCart.config.dataType,
                    url: RelatedItemsInCart.config.url,
                    success: RelatedItemsInCart.ajaxSuccess,
                    error: RelatedItemsInCart.ajaxFailure
                });
            },
            init: function() {
                $("#divRelatedItems").hide();
                if ('<%=EnableRelatedItemsInCart%>'.toLowerCase() == 'true') {
                    RelatedItemsInCart.GetItemRetatedUpSellAndCrossSellList();
                    $("#divRelatedItems").show();
                }
            },

            GetItemRetatedUpSellAndCrossSellList: function() {
                RelatedUpSellCrossSellItems = '';
                this.config.url = this.config.baseURL + "GetRelatedItemsByCartItems";
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, sessionCode: sessionCode, userName: userName, cultureName: cultureName, count: relatedItemCount });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },

            AddToCartToJS: function(itemId, itemPrice, itemSKU, itemQuantity) {
                AspxCommerce.RootFunction.AddToCartFromJS(itemId, itemPrice, itemSKU, itemQuantity, storeId, portalId, customerId, sessionCode, userName, cultureName);
            },
            ajaxSuccess: function(msg) {
                switch (RelatedItemsInCart.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                                if (item.ImagePath == "") {
                                    item.ImagePath = '<%=NoImageRelatedItemsInCartPath %>';
                                }
                                if (item.AlternateText == "") {
                                    item.AlternateText = item.Name;
                                }
                                if ((index + 1) % 4 == 0) {
                                    RelatedUpSellCrossSellItems += "<div class=\"cssClassYouMayAlsoLikeBox cssClassYouMayAlsoLikeBoxFourth\">";
                                }
                                else {
                                    RelatedUpSellCrossSellItems += "<div class=\"cssClassYouMayAlsoLikeBox\">";
                                }
                                RelatedUpSellCrossSellItems += '<p class="cssClassCartPicture"><a href="' + aspxRedirectPath + 'item/' + item.SKU + '.aspx"><img height="92px" width="134px" alt="' + item.AlternateText + '" title="' + item.Name + '" src="' + aspxRootPath + item.ImagePath + '"></a></p>';
                                RelatedUpSellCrossSellItems += '<p class="cssClassProductRealPrice"><span>Price : ' + item.Price + '</span></p>';
                                if ('<%=AllowOutStockPurchase %>'.toLowerCase() == 'false') {
                                    if (item.IsOutOfStock) {
                                        RelatedUpSellCrossSellItems += "<div class='cssClassButtonWrapper cssClassOutOfStock'><a href='#'><span>Out Of Stock</span></a></div></div>";
                                    }
                                    else {
                                        RelatedUpSellCrossSellItems += "<div class='cssClassButtonWrapper'><a href='#' onclick='RelatedItemsInCart.AddToCartToJS(" + item.ItemID + "," + item.Price + "," + JSON2.stringify(item.SKU) + "," + 1 + ");'><span>Add to Cart</span></a></div></div>";
                                    }
                                }
                                else {

                                    RelatedUpSellCrossSellItems += "<div class='cssClassButtonWrapper'><a href='#' onclick='RelatedItemsInCart.AddToCartToJS(" + item.ItemID + "," + item.Price + "," + JSON2.stringify(item.SKU) + "," + 1 + ");'><span>Add to Cart</span></a></div></div>";
                                }
                            });
                            RelatedUpSellCrossSellItems += "<div class=\"cssClassClear\"></div>";
                            $("#divRelatedItemsInCart").html(RelatedUpSellCrossSellItems);
                        }
                        else {
                            $("#divRelatedItemsInCart").html("<span class=\"cssClassNotFound\">No Data found.</span>");
                        }
                        break;
                }
            }
        }

        RelatedItemsInCart.init();
    });
   //]]>
</script>


<div id="divRelatedItems" class="cssClassProductDetailInformation cssClassYouMayAlsoLike">
    <h2>
        <asp:Label ID="lblYouMayAlsoLike" Text="You may also like :" runat="server" />
    </h2>
    <div class="cssClassYouMayAlsoLikeWrapper" id="divRelatedItemsInCart">
    </div>
</div>
