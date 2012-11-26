<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LatestItemsCarousel.ascx.cs" Inherits="Modules_AspxCommerce_AspxLatestItems_LatestItemsCarousel" %>
<script type="text/javascript">
    var countryName = '<%=CountryName %>';
    var defaultImagePath = '<%=DefaultImagePath %>';
    var noOfLatestItems = '<%=NoOfLatestItems %>';
    var noOfLatestItemsInARow = '<%=NoOfLatestItemsInARow%>';
    var allowOutStockPurchase = '<%=AllowOutStockPurchase %>';
    var enableLatestItems = '<%=EnableLatestItems %>';
</script>

<div id="divlatestItemsNew" class="cssClassCommonSideBox">
    <h2>
        <asp:Label ID="lblLatestItemHeading" runat="server" class="cssClassLatestItems" Text="New Arrivals"></asp:Label>
    </h2>
    <div class="cssClassClear">
    </div>
    <div class="test">
        <ul id="divLatestItemTemp" class="cssClassLatestItems sfHorizontalbx-wrapper">
            <li></li>
        </ul>
    </div>
</div>

<script id="scriptResultGrid" type="text/x-jquery-tmpl">
  <li class="cssClassLatestItemList">
        <div class="cssClassProductsBox">
            <div class="cssClassProductsBoxInfo">
                               
                <div class="cssClassProductPicture">
                    <a href="${aspxRedirectPath}item/${sku}.aspx">
                        <img class="lazy" src='${imagePath}' alt='${alternateText}' title='${name}' /></a>                                                
                        </div>
                        <div class="cssClassDiscountPrice"><span > ${discountOffer}%</span></div>
                        <h2>
                    ${name}</h2> 
                <div class="cssClassProductPriceBox">
                    <div class="cssClassProductPrice">
                        <p class="cssClassProductOffPrice">
                             <span class="cssRegularPrice cssClassFormatCurrency">${listPrice}</span></p>
                        <p class="cssClassProductRealPrice">
                            <span class="cssOfferPrice cssClassFormatCurrency">${price}</span></p>
                    </div>
                </div>              
                <div class="cssClassclear">
                </div>
                 <div class="cssClassAddtoCard_${itemID} cssClassAddtoCard">
                <div class="cssClassButtonWrapper">
                    <button type="button" onclick="AspxCommerce.RootFunction.AddToCartToJSFromTemplate(${itemID},${price},${JSON2.stringify(sku)},${1});">
                        <span>Add to Cart</span></button>
                </div>
            </div>
            </div>
           
        </div>
    </li> 
</script>