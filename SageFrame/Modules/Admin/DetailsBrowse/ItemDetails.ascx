<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemDetails.ascx.cs" Inherits="Modules_AspxDetails__AspxItemDetails_ItemDetails " %>

<script type="text/javascript">
    var __st_loadLate = true; //if __st_loadLate is defined then the widget will not load on domcontent ready
</script>

<script type="text/javascript">    var switchTo5x = false;</script>

<script type="text/javascript" src="http://w.sharethis.com/button/buttons.js"></script>

<script type="text/javascript">    stLight.options({ publisher: '938d4fda-409f-4c0b-b300-33191719abed' });</script>

<script type="text/javascript">
    var UsrModuleID = '<%=UserModuleID %>';
    var userEmail = '<%=userEmail %>';
    var itemID = '<%=itemID %>';
    var itemNamePageBehind = '<%=itemName%>';
    var itemSKU = '<%=itemSKU%>';
    var aspxFilePath = '<%=aspxfilePath%>';
    var maxCompareItemCount = '<%=maxCompareItemCount%>';
    var allowOutStockPurchase = '<%=allowOutStockPurchase %>';
    var allowMultipleReviewPerIP = '<%=allowMultipleReviewPerIP %>';
    var allowMultipleReviewPerUser = '<%=allowMultipleReviewPerUser %>';
    var allowAnonymousReviewRate = '<%=allowAnonymousReviewRate %>';
    var enableEmailFriend = '<%=enableEmailFriend %>';
    var allowCompareItemDetail = '<%=allowCompareItemDetail %>';
    var allowWishListItemDetail = '<%=allowWishListItemDetail %>';
    var noItemDetailImagePath = '<%=noItemDetailImagePath %>';
    var relatedItemsCount = '<%=relatedItemsCount%>';
</script>

<div class="cssClassCommonWrapper" id="itemDetails">
    <div class="cssClassProductInformation">
        <div class="cssClassProductImage">
            <div class="cssClassProductBigPicture">
                <div class="pikachoose">
                    <%--                    <ul id="pikame" class="jcarousel-skin-pika">
                    </ul>--%>
                </div>
            </div>
        </div>
        <div class="cssClassProductPictureDetails">
            <div class="cssClassLeftSide">
                <h2>
                    <asp:Label ID="lblItemTitle" runat="server" Text="Item Name: "></asp:Label>
                    <span id="spanItemName"></span>
                </h2>
                <li class="cssClassItems">
                    <asp:Label ID="lblSKU" runat="server" Text="SKU: "></asp:Label>
                    <span id="spanSKU"></span></li>
                <li><span class="cssClssQTY">
                    <asp:Label ID="lblQty" runat="server" Text="Qty: "></asp:Label>
                    <input type="text" id="txtQty" /><label id='lblNotification' style="color: #FF0000;"></label>
                </span></li>
                <li id="divQtyDiscount">
                    <p>
                        Our quantity discounts:</p>
                    <div class="cssClassCommonGrid">
                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassQtyDiscount"
                            id="itemQtyDiscount">
                            <thead>
                                <tr class="cssClassHeadeTitle">
                                    <th>
                                        Quantity (more than)
                                    </th>
                                    <th>
                                        Price
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </li>
                <li>
                    <div class="cssClassDownload cssClassRight" id="dwnlDiv">
                        <asp:Label ID="lblDownloadTitle" runat="server" Text="Sample Item Download: "></asp:Label>
                        <span id="spanDownloadLink" class="cssClassLink"></span>
                    </div>
                </li>
                <li>
                    <div id="divCostVariant">
                    </div>
                </li>
                <div class="cssClassItemRating">
                    <div class="cssClassItemRatingBox cssClassToolTip">
                        <div class="cssClassRatingStar">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tblAverageRating">
                            </table>
                        </div>
                        <span class="cssClassRatingTitle"></span>
                        <%-- For detail star rating info --%>
                        <div class="cssClassToolTipInfo">
                        </div>
                        <div class="cssClassClear">
                        </div>
                    </div>
                    <span class="cssClassTotalReviews"></span><span class="cssClassSeparator">|</span>
                    <%--<a href="#">Popup goes her to show add new review</a>--%>
                    <a href="#" rel="popuprel2" class="popupAddReview" value=""><span class="cssClassAddYourReview">
                    </span></a>
                </div>
            </div>
            <div class="cssClassRightSide">
                <ul>
                    <li><span class="cssClassProductOffPrice">
                        <asp:Label ID="lblListPrice" runat="server" Text="List Price: "></asp:Label>
                        <span id="spanListPrice" class="cssClassFormatCurrency"></span></span><span class="cssClassProductRealPrice">
                            <asp:Label ID="lblPrice" runat="server" Text="Price: "></asp:Label>
                            <span id="spanPrice" class="cssClassFormatCurrency"></span>
                            <br />
                            <span id="bulkDiscount" class="cssClassBulkDiscount">(Bulk Discount available)</span></span></li>
                    <li class="cssClassYouSave"><span class="cssClassYouSave">
                        <asp:Label ID="lblSaving" runat="server" Text="You save: "></asp:Label>
                        <span id="spanSaving"></span></span></li>
                    <%--<li class="cssClassRating">
                        <asp:Label ID="lblRating" runat="server" Text="Rating Summary : "></asp:Label>
                        <span id="spanRating"></span></li>--%>
                    <li>
                        <div class="cssClassTax">
                            <asp:Label ID="lblTax" runat="server" Text="Tax: "></asp:Label>
                            <span id="spanTax" class="cssClassFormatCurrency"></span>
                        </div>
                    </li>
                    <li>
                        <div class="cssClassAvailiability">
                            <asp:Label ID="lblAvailability" runat="server" Text="Availability: "></asp:Label>
                            <span id="spanAvailability"></span>
                        </div>
                    </li>
                </ul>
                <div class="cssClassButtonWrapper">
                    <button id="addWishListThis" type="button">
                        <span><span><span>+</span>Wishlist</span></span></button>
                    <button id="addCompareListThis" type="button">
                        <span><span><span>+</span>Compare</span></span></button>
                    <button id="btnAddToMyCart" type="button" onclick="ItemDetail.AddToMyCart();">
                        <span><span>Add to Cart</span></span></button>
                </div>
            </div>
        </div>
        <div id="divBookMark" class="cssClassLinkShare">
            <%--<div class="addthis_toolbox addthis_default_style">
                        <a class="addthis_button_facebook_like" fb: like:layout="button_count"></a>
                        <a class="addthis_button_tweet" tw:via="AspxCommerce"></a>
                        <a class="addthis_button_google_plusone" g: plusone:size="medium"></a>
                        <a class="addthis_counter addthis_pill_style"></a>
                    </div>
                    <script type="text/javascript">                        var addthis_config = { "data_track_clickback": false };</script>
                    <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pubid=ra-4da3f76e591920f0"></script>--%>
            <span class='st_linkedin_hcount' displaytext='LinkedIn'></span><span class='st_twitter_hcount'
                displaytext='Tweet'></span><span class='st_facebook_hcount' displaytext='Facebook'>
            </span><span class='st_email_hcount' displaytext='Email'></span><span class='st_sharethis_hcount'
                displaytext='ShareThis'></span><span class='st_fblike_hcount'></span>
        </div>
        <div class="cssClassButtonWrapper">
            <div class="cssClassButton">
                <a href="#" id="lnkContinueShopping"><span>Continue Shopping</span></a>
            </div>
            <div id="divEmailAFriend" class="cssClassButton cssClassDetailPageRightBtn">
                <a href="#" rel="popuprel" class="popupEmailAFriend" value=""><span>Email a Friend</span>
                </a>
            </div>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
    <div class="cssClassItemQuickOverview">
        <h2>
            <asp:Label ID="lblQuickOverview" Text="Quick Overview :" runat="server" />
        </h2>
        <table width="100%" cellspacing="0" cellpadding="0" border="0" class="tdpadding">
            <tbody>
                <tr class="cssClassItemOverviewContent">
                    <td id="divItemShortDesc">
                    </td>
                </tr>
            </tbody>
        </table>
        <%--  <div class="cssClassReadMore" id="divReadMore">
                    <span>Read More</span></div>
                <div class="cssClassReadMore" id="divReadLess">
                    <span>Read Less</span></div>--%>
    </div>
    <div class="cssClassProductDetailInformation">
        <div id="dynItemDetailsForm" class="cssClassFormWrapper">
        </div>
    </div>
    <div class="cssClassProductDetailInformation cssClassYouMayAlsoLike">
        <h2>
            <asp:Label ID="lblYouMayAlsoLike" Text="You may also like :" runat="server" />
        </h2>
        <div class="cssClassYouMayAlsoLikeWrapper" id="divYouMayAlsoLike">
        </div>
    </div>
    <div id="controlload">
    </div>
    <div class="popupbox" id="popuprel2">
        <div class="cssClassCloseIcon">
            <button type="button" class="cssClassClose">
                <span>Close</span></button>
        </div>
        <h2>
            <asp:Label ID="lblWriteReview" runat="server" Text="Write Your Own Review"></asp:Label>
        </h2>
        <div class="cssClassFormWrapper">
            <%--<div class="cssClassReviewInfos"> Submit a review now and earn 15 reward points once the review is approved. <a href="#"> Learn more...</a></div>
      <div class="cssClassReviewInfos">Applies only to registered customers, may vary when logged in.</div>
      --%>
            <div class="cssClassPopUpHeading">
                <h3>
                    <label id="lblYourReviewing">
                    </label>
                </h3>
            </div>
            <asp:Label ID="lblHowToRate" runat="server" Text="How do you rate this item?"></asp:Label>
            <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tblRatingCriteria">
            </table>
            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td>
                        <label class="cssClassLabel">
                            Nickname:<span class="cssClassRequired">*</span></label>
                    </td>
                    <td>
                        <input type="text" id="txtUserName" name="name" class="required" minlength="2" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="cssClassLabel">
                            Summary Of Review:<span class="cssClassRequired">*</span></label>
                    </td>
                    <td>
                        <input type="text" id="txtSummaryReview" name="summary" class="required" minlength="2" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="cssClassLabel">
                            Review:<span class="cssClassRequired">*</span></label>
                    </td>
                    <td>
                        <textarea id="txtReview" cols="50" rows="10" name="review" class="cssClassTextarea required"
                            maxlength="300"></textarea>
                    </td>
                </tr>
            </table>
            <div class="cssClassButtonWrapper cssClassWriteaReview">
                <%-- <input  type="submit" value="Submit" id="btnSubmitReview"/>--%>
                <button type="submit" id="btnSubmitReview">
                    <span><span>Submit Review</span></span></button>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hdnPrice" />
<input type="hidden" id="hdnWeight" />
<input type="hidden" id="hdnQuantity" />
<input type="hidden" id="hdnListPrice" />
<input type="hidden" id="hdnTaxRateValue" />