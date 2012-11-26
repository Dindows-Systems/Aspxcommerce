<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemCompareDetails.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxItemsCompare_ItemCompareDetails" %>

<script type="text/javascript" language="javascript">
	//<![CDATA[ 
    var storeId, portalId, userName, cultureName, customerId, ip, countryName, sessionCode, userFriendlyURL;

    var IDs = "";

    $(document).ready(function() {
        storeId = AspxCommerce.utils.GetStoreID();
        portalId = AspxCommerce.utils.GetPortalID();
        userName = AspxCommerce.utils.GetUserName();
        cultureName = AspxCommerce.utils.GetCultureName();
        customerId = AspxCommerce.utils.GetCustomerID();
        ip = AspxCommerce.utils.GetClientIP();
        countryName = AspxCommerce.utils.GetAspxClientCoutry();
        sessionCode = AspxCommerce.utils.GetSessionCode();
        userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
        IDs = $.cookies.get("ItemCompareDetail");
        if (IDs != null && IDs != '') {            
            GetCompareListImage(IDs);
            GetCompareList(IDs);
            RecentAdd(IDs);
        } else {
            $("#divCompareElementsPopUP").html('<span class="cssClassNotFound">No Items found in you Compare Item List.</span>');
        }
        if ($("#tblRecentlyComparedItemList").length > 0) {
            RecentlyComparedItems.RecentlyCompareItemsList();
        }
        $('#btnPrintItemCompare').click(function() {
            printPage();
        });
    });
    printPage = function() {
       window.print();
    };
    function GetCompareList(IDs) {
        $.ajax({
            type: "POST",
            url: aspxservicePath + "AspxCommerceWebService.asmx/GetCompareList",
            data: JSON2.stringify({ itemIDs: IDs, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName }),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function(msg) {
                var myIds = new Array();
                var myAttributes = new Array();
                $("#tblItemCompareList >tbody").html('');
                $("#divItemCompareElements").html("");
                $("#scriptStaticField").tmpl().appendTo("#divItemCompareElements");
                Array.prototype.RemoveNA = function(arr, obj) {
                    var i = this.length;
                    while (i--) {//alert(i);
                        if (this[i].AttributeID === obj) {
                            arr.splice(i, 1);
                        }
                    } 
                };
                Array.prototype.Count = function(obj) {
                    var i = this.length;
                    var cc = 0;
                    while (i--) {
                        if (this[i] === obj) {
                            cc++;
                        }
                    }
                    return cc;
                };
                var oldc;
                var itemCount;
                var emArr = [];
                $.each(msg.d, function(index, value) {

                    if (index == 0) {
                        oldc = value.AttributeID; itemCount = 1;
                    }
                    if (index != 0 && value.AttributeID == oldc) {
                        itemCount++;
                    }
                    if (value.AttributeValue == "") {
                        emArr.push(value.AttributeID);
                    }
                });
                $.each(emArr, function(index, value) {

                    if (itemCount == emArr.Count(value)) {
                        msg.d.RemoveNA(msg.d, value);
                    }
                });
               
                $.each(msg.d, function(index, value) {
                    var cssClass = '';
                    var noAttValue = [];
                    cssClass = 'cssClassCompareAttributeClass';
                    var pattern = '"', re = new RegExp(pattern, "g");
                    if (value.InputTypeID == 7) {
                        cssClass = 'cssClassFormatCurrency';
                    }
                    if (jQuery.inArray(value.AttributeID, myAttributes) < 0) {
                        $("#tblItemCompareList >tbody").append('<tr id="trCompare_' + index + '"></tr>');
                        $("#tblItemCompareList >tbody> tr:last").append('<td class="' + cssClass + '"><span class="cssClassLabel">' + value.AttributeName + ': </span></td>');
                        var valz;
                        if (value.AttributeValue == "") {
                            valz = "n/a"; noAttValue.push(value.AttributeID);
                        } else {
                            valz = value.AttributeValue;
                        }
                        var y = Encoder.htmlDecode(valz);
                        y = y.replace(re, '\\');
                        var attributValue = [{ CssClass: cssClass, AttributeValue: y}];
                        $("#scriptAttributeValue").tmpl(attributValue).appendTo("#tblItemCompareList tbody#itemDetailBody>tr:last");

                        myAttributes.push(value.AttributeID);
                    }
                    else {
                        var valz1;
                        if (value.AttributeValue == "") {
                            valz1 = "n/a";
                        } else {
                            valz1 = value.AttributeValue;
                        }
                        var z = Encoder.htmlDecode(valz1);
                        z = z.replace(re, '\\');
                        var i = index % (myAttributes.length);
                        attributValue = [{ CssClass: cssClass, AttributeValue: z}]; //{{html shortDescription}}

                        $("#scriptAttributeValue").tmpl(attributValue).appendTo("#trCompare_" + i + "");
                    }
                });
                $("#tblItemCompareList tr:even").addClass("cssClassAlternativeEven");
                $("#tblItemCompareList tr:odd").addClass("cssClassAlternativeOdd");
                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
            },
            error: function() {
                csscody.error('<h2>Error Message</h2><p>Sorry, Compare list error occured!</p>');
            }
        });
    }

    function RecentAdd(Id) {
        var param = JSON2.stringify({ IDs: Id, storeID: storeId, portalID: portalId, userName: userName });
        $.ajax({
            type: "Post",
            url: aspxservicePath + "AspxCommerceWebService.asmx/AddComparedItems",
            data: param,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function() {
            },
            error: function() {
            csscody.error('<h2>Error Message</h2><p>Sorry, error occured!</p>');
            }
        });
    }

    function GetCompareListImage(IDs) {
        $.ajax({
            type: "POST",
            url: aspxservicePath + "AspxCommerceWebService.asmx/GetCompareListImage",
            data: JSON2.stringify({ itemIDs: IDs, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName }),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function(msg) {
                var htMl = '';
                $("#divCompareElements").html("");
                //  $("#scriptStaticField").tmpl().appendTo("#divCompareElements");
                $.each(msg.d, function(index, value) {

                    if (value.ImagePath == "") {
                        value.ImagePath = '<%=NoImageItemComparePath %>';
                    }
                    else if (value.AlternateText == "") {
                        value.AlternateText = value.Name;
                    }
                    var items = [{ AspxCommerceRoot: aspxRootPath, itemID: value.ItemID, name: value.Name, sku: value.SKU,
                    imagePath: aspxRootPath + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: value.ListPrice,
                        price: value.Price, shortDescription: Encoder.htmlDecode(value.ShortDescription)}];
                        $("#scriptResultProductGrid2").tmpl(items).appendTo("#tblItemCompareList thead > tr");
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        if ('<%=AllowOutStockPurchase %>'.toLowerCase() == 'false') {
                            if (value.IsOutOfStock) {
                                $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                            }
                        }

                    });
                },
                error: function() {
                csscody.error('<h2>Error Message</h2><p>Sorry, compare list error occured!</p>');
                }
            });
        }

        function CheckWishListUniqueness(itemID) {
            if (customerId > 0 && userName.toLowerCase() != "anonymoususer") {
                var checkparam = { ID: itemID, storeID: storeId, portalID: portalId, userName: userName };
                var checkdata = JSON2.stringify(checkparam);
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "AspxCommerceWebService.asmx/CheckWishItems",
                    data: checkdata,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        if (msg.d) {
                            csscody.alert('<h2>Information Alert</h2><p>The selected item already in your wishlist.</p>');
                        } else {
                            AddToWishListFromJS(itemID, storeId, portalId, userName, ip, countryName);
                        }
                    }
                });
            } else {
                window.location.href = aspxRootPath + 'Login.aspx';
                return false;
            }
        }

        function AddToCartToJSs(itemId, itemPrice, itemSKU, itemQuantity) {
            AddToCartFromJS(itemId, itemPrice, itemSKU, itemQuantity, storeId, portalId, customerId, sessionCode, userName, cultureName);
        }
//]]>      
</script>
<script id="scriptStaticField" type="text/x-jquery-tmpl">
</script>

<script id="scriptAttributeValue" type="txt/x-jquery-tmpl">
<td class="${CssClass}">{{html AttributeValue}}</td>
</script>

<script id="scriptResultProductGrid2" type="text/x-jquery-tmpl">                        
 <td>
 <div id="comparePride" class="cssClassProductsGridBox">
 <div class="cssClassProductsGridInfo">
 <h2><a href="${AspxCommerceRoot}item/${sku}.aspx">${name}</a></h2>
 <div class="cssClassProductsGridPicture"><img src='${imagePath}' alt='${alternateText}' title='${name}' /></div>
 <div class="cssClassProductsGridPriceBox">
 <div class="cssClassProductsGridPrice">
 <p class="cssClassProductsGridOffPrice">Price :<del><span class="cssClassFormatCurrency">${listPrice}</span></del> <span class="cssClassProductsGridRealPrice"> <span class="cssClassFormatCurrency">${price}</span></span> </p>
 </div>
 </div>

 <div id="compareAddToWishlist" class="cssClassButtonWrapper">
 <div class="cssClassWishListButton">
 <button onclick="AspxCommerce.RootFunction.AddToWishList(${itemID});" id="addWishList" type="button"><span>+ Add to Wishlist</span></button>
 </div>
 </div>
 <div id="compareAddToCart" class="cssClassAddtoCard_${itemID} cssClassAddtoCard">
 <div class="cssClassButtonWrapper"> 
<a href="#" onclick="AspxCommerce.RootFunction.AddToCartToJSFromTemplate(${itemID},${price},${JSON2.stringify(sku)},${1});"><span>Add to Cart</span></a> </div>
</div>
<div class="cssClassclear"></div>
</div>
 </div>
 </td>
</script>

<div id="divItemCompareElements" class="cssClassFormWrapper">
</div>
 <div id="dvCompareList" class="cssClassCommonBox cssClassCompareBox" >
    <div class="cssClassHeader">
             <h2>
        <asp:Label ID="lblCompareTitle" runat="server" class="cssClassCompareItem" Text="Compare following Items"></asp:Label>
    </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
        <p>
            <button type="button" id="btnPrintItemCompare">
                <span><span>Print</span></span></button>
        </p>
        <div class="cssClassClear">
        </div>
    </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        

    <div id="divCompareElementsPopUP" class="cssClassFormWrapper">
    <table id="tblItemCompareList" width="100%" border="0" cellspacing="0" cellpadding="0">
    <thead>
        <tr>
            <td>
            </td>
        </tr>
    </thead>
    <tbody id="itemDetailBody">
        <tr>
        </tr>
    </tbody>
</table>
    </div>
</div>


