var LatestItems = "";
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var ip = AspxCommerce.utils.GetClientIP();
    var sessionCode = AspxCommerce.utils.GetSessionCode();

    LatestItems = {
        config: {
            isPostBack: false,
            async: true,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxservicePath,
            method: "",
            url: "",
            ajaxCallMode: 0, ///0 for get categories and bind, 1 for notification,2 for versions bind 
            itemid: 0



        },
        vars: {
            countCompareItems: 0
        },
        ajaxCall: function(config) {
            $.ajax({
                type: LatestItems.config.type,
                contentType: LatestItems.config.contentType,
                cache: LatestItems.config.cache,
                async: LatestItems.config.async,
                url: LatestItems.config.url,
                data: LatestItems.config.data,
                dataType: LatestItems.config.dataType,
                success: LatestItems.ajaxSuccess,
                error: LatestItems.ajaxFailure
            });
        },

        GetLatestItems: function() {
            this.config.method = "AspxCommerceWebService.asmx/GetLatestItemsList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ storeId: storeId, portalId: portalId, userName: userName, cultureName: cultureName, count: noOfLatestItems });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        BindRecentItems: function(msg) {
            var RecentItemContents = '';
            if (msg.d.length > 0) {
                $.each(msg.d, function(index, item) {
                    //if (!item.HideToAnonymous) {
                    if (item.ImagePath == "") {
                        item.ImagePath = defaultImagePath;
                    }
                    if (item.AlternateText == "") {
                        item.AlternateText = item.Name;
                    }
                    if ((index + 1) % eval(noOfLatestItemsInARow) == 0) {
                        RecentItemContents += "<div class=\"cssClassProductsBox cssClassProductsBoxNoMargin\">";
                    }
                    else {
                        RecentItemContents += "<div class=\"cssClassProductsBox\">";
                    }
                    var hrefItem = aspxRedirectPath + "item/" + LatestItems.fixedEncodeURIComponent(item.SKU) + ".aspx";
                    RecentItemContents += '<div id="productImageWrapID_' + item.ItemID + '" class="cssClassProductsBoxInfo" itemid="' + item.ItemID + '"><h2>' + item.Name + '</h2><h3>' + item.SKU + '</h3><div id="divitemImage" class="cssClassProductPicture"><a href="' + hrefItem + '"><img class="lazy"  alt="' + item.AlternateText + '"  title="' + item.AlternateText + '" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/loader_100x12.gif" data-original="' + aspxRootPath + item.ImagePath.replace('uploads', 'uploads/Small') + '"></a></div>';

                    if (!item.HidePrice) {
                        RecentItemContents += "<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\"><p class=\"cssClassProductOffPrice\">Regular Price : <span class=\"cssClassFormatCurrency\" value=" + (item.ListPrice).toFixed(2) + ">" + (item.ListPrice * rate).toFixed(2) + "</span></p><p class=\"cssClassProductRealPrice \" >Our Offer : <span class=\"cssClassFormatCurrency\" value=" + (item.Price).toFixed(2) + ">" + (item.Price * rate).toFixed(2) + "</span></p></div></div>";
                    }
                    else {
                        RecentItemContents += "<div class=\"cssClassProductPriceBox\"></div>";
                    }
                    RecentItemContents += '<div class="cssClassProductDetail"><p><a href="' + aspxRedirectPath + 'item/' + item.SKU + '.aspx">Details</a></p></div>';

                    RecentItemContents += "<div class=\"cssClassButtonWrapper\">";
                    if (allowWishListLatestItem.toLowerCase() == 'true') {
                        if (customerId > 0 && userName.toLowerCase() != "anonymoususer") {
                            RecentItemContents += "<div class=\"cssClassWishListButton\"><button type=\"button\" id=\"addWishList\" onclick='LatestItems.CheckWishListUniqueness(" + item.ItemID + ");'><span><span><span>+</span>Wishlist</span></span></button></div>";
                        }
                        else {
                            RecentItemContents += "<div class=\"cssClassWishListButton\"><button type=\"button\" id=\"addWishList\" onclick='AspxCommerce.RootFunction.Login();'><span><span><span>+</span>Wishlist</span></span></button></div>";
                        }
                    }
                    //RecentItemContents+="<input type=\"button\" id=\"addWishList\" value=\"Add To Wishlist\" onclick='AddToWishList(" + item.ItemID + ");'/>";
                    //RecentItemContents += "<div class=\"cssClassWishListDetail\"><p><a href='addtowishlist.aspx?itemId="+ item.ItemID + "'>Add to Wishlist</a></p>";
                    if (allowAddToCompareLatest.toLowerCase() == 'true') {
                        RecentItemContents += "<div class=\"cssClassCompareButton\"><button type=\"button\" id=\"btnAddCompare\" onclick='LatestItems.AddItemsToCompare(" + item.ItemID + ");'><span><span><span>+</span>Compare</span></span></button></div>";
                    }
                    RecentItemContents += "</div>";
                    RecentItemContents += "<div class=\"cssClassclear\"></div>";
                    var itemSKU = JSON2.stringify(item.SKU);
                    var itemName = JSON2.stringify(item.Name);
                    if (allowOutStockPurchase.toLowerCase() == 'false') {
                        if (item.IsOutOfStock) {
                            //  RecentItemContents += "</div><div class=\"cssClassAddtoCard\"><div class=\"cssClassButtonWrapper cssClassOutOfStock\"><a href=\"#\"><span>Out Of Stock</span></a></div></div>";
                            RecentItemContents += "</div><div class=\"cssClassAddtoCard\"><div class=\"cssClassButtonWrapper cssClassOutOfStock\"><button type=\"button\"><span>Out Of Stock</span></button></div></div>";
                        }
                        else {
                            // RecentItemContents += "</div><div class=\"cssClassAddtoCard\"><div class=\"cssClassButtonWrapper\"><a href=\"#\" title=" + itemName + "  onclick='LatestItems.AddToCartToJS(" + item.ItemID + "," + item.Price.toFixed(2) + "," + itemSKU + "," + 1 + ");'><span>Add to cart</span></a></div></div>";
                            RecentItemContents += "</div><div class=\"cssClassAddtoCard\"><div class=\"cssClassButtonWrapper\"><button type=\"button\" id=\"addtoCart\" title=" + itemName + "  onclick='LatestItems.AddToCartToJS(" + item.ItemID + "," + item.Price.toFixed(2) + "," + itemSKU + "," + 1 + ");'><span><span>Add to cart</span></span></button></div></div>";
                        }
                    }
                    else {
                        // RecentItemContents += "</div><div class=\"cssClassAddtoCard\"><div class=\"cssClassButtonWrapper\"><a href=\"#\" title=" + itemName + "  onclick='LatestItems.AddToCartToJS(" + item.ItemID + "," + item.Price.toFixed(2) + "," + itemSKU + "," + 1 + ");'><span>Add to cart</span></a></div></div>";
                        RecentItemContents += "</div><div class=\"cssClassAddtoCard\"><div class=\"cssClassButtonWrapper\"><button type=\"button\" id=\"addtoCart\" title=" + itemName + "  onclick='LatestItems.AddToCartToJS(" + item.ItemID + "," + item.Price.toFixed(2) + "," + itemSKU + "," + 1 + ");'><span><span>Add to cart</span></span></button></div></div>";
                    }
                    RecentItemContents += "</div>";



                    // }
                });

                //$('#divitemImage img[title]').tipsy();
            }
            else {
                RecentItemContents += "<span class=\"cssClassNotFound\">This store has no items listed yet!</span>";
            }
            //_ItemID; _DateFrom; _DateTo; _IsFeatured; _SKU; _Name; _Price; _ListPrice; _HidePrice; _HideInRSSFeed; _HideToAnonymous; _AddedOn; _ImagePath; _AlternateText
            $("#tblRecentItems").html(RecentItemContents);
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
            $('#divitemImage a img[title]').tipsy({ gravity: 'n' });
            //$('.cssClassProductRealPrice span').formatCurrency({ colorize: true, region: '' + region + '' });
            //$(".cssClassProductsBoxInfo").draggable({ helper: 'clone', opacity: 0.5, cursor: 'crosshair', revert: true });
            $("img").lazyload();
        },

        AddItemsToCompare: function(itemId) {
            LatestItems.vars.countCompareItems = itemCompareCount;            
            LatestItems.config.itemid = itemId;
            LatestItems.GetCompareItemsCount(itemId);
            if (LatestItems.vars.countCompareItems >= parseInt(maxCompareItemCount)) {
                csscody.alert('<h2>Information Alert</h2><p>More than ' + maxCompareItemCount + ' items are not allowed to add in compare list!</p>');
                return false;
            }
            this.config.method = "AspxCommerceWebService.asmx/CheckCompareItems";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ ID: itemId, storeID: storeId, portalID: portalId, userName: userName, sessionCode: sessionCode });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
            return false;

        },

        AddToMyCompare: function() {
            this.config.method = "AspxCommerceWebService.asmx/SaveCompareItems";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ ID: LatestItems.config.itemid, storeID: storeId, portalID: portalId, userName: userName, IP: ip, countryName: countryName, sessionCode: sessionCode });
            this.config.ajaxCallMode = 3;
            this.config.async = false;
            this.ajaxCall(this.config);
        },

        GetCompareItemsCount: function(itemId) {
            this.config.method = "AspxCommerceWebService.asmx/CheckCompareItems";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ ID: itemId, storeID: storeId, portalID: portalId, userName: userName, IP: ip, countryName: countryName, sessionCode: sessionCode });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        }, CheckWishListUniqueness: function(itemID) {
            this.config.itemid = itemID;
            var checkparam = { ID: itemID, storeID: storeId, portalID: portalId, userName: userName };
            var checkdata = JSON2.stringify(checkparam);
            this.config.method = "AspxCommerceWebService.asmx/CheckWishItems",
	            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = checkdata;
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
        },
        fixedEncodeURIComponent: function(str) {
            return encodeURIComponent(str).replace(/!/g, '%21').replace(/'/g, '%27').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/\*/g, '%2A');
        }, IncreaseWishListCount: function() {
            var wishListCount = $('#lnkMyWishlist span ').html().replace(/[^0-9]/gi, '');
            wishListCount = parseInt(wishListCount) + 1;
            $('.cssClassLoginStatusInfo ul li a#lnkMyWishlist span ').html("[" + wishListCount + "]");
        }, AddToCartToJS: function(itemId, itemPrice, itemSKU, itemQuantity) {
            AspxCommerce.RootFunction.AddToCartFromJS(itemId, itemPrice, itemSKU, itemQuantity, storeId, portalId, customerId, sessionCode, userName, cultureName);
        }, IncreaseShoppingBagCount: function() {
            var myShoppingBagCount = $('#lnkshoppingcart').html().replace(/[^0-9]/gi, '');
            myShoppingBagCount = parseInt(myShoppingBagCount) + 1;
            $('#lnkshoppingcart').html(" My Shopping Bag [" + myShoppingBagCount + "]");
        },
        ajaxSuccess: function(data) {
            switch (LatestItems.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    LatestItems.BindRecentItems(data);
                    break;
                case 2:
                    LatestItems.config.ajaxCallMode = 0;
                    if (data.d) {
                        csscody.alert('<h2>Information Alert</h2><p>The selected item already exist in compare list.</p>');

                        return false;
                    }
                    else {
                        LatestItems.AddToMyCompare();
                    }
                    break;
                case 3:
                    LatestItems.vars.countCompareItems++;
                    LatestItems.config.ajaxCallMode = 0;
                    csscody.info('<h2>Information Message</h2><p>Item has been successfully added to compare list.</p>');
                    if ($("#h2compareitems").length > 0) {
                        ItemsCompare.GetCompareItemList(); //for MyCompareItem
                    }
                    break;
                case 4:
                    if (data.d != true && data.d != false) {
                        LatestItems.vars.countCompareItems = data.d;
                    }

                    break;
                case 5:
                    if (data.d) {
                        csscody.alert('<h2>Information Alert</h2><p>The selected item already exist in your wishlist.</p>');
                    }
                    else {
                        AspxCommerce.RootFunction.AddToWishListFromJS(LatestItems.config.itemid, storeId, portalId, userName, ip, countryName); // AddToList ==> AddToWishList
                    }
                    break;

            }
        }, init: function() {
            $("#divLatestItems").hide();
            // LatestItems.GetLatestItems();
            if (enableLatestItems.toLowerCase() == 'true') {
                LatestItems.GetLatestItems();
                $("#divLatestItems").show();
            }

        }

    }
    LatestItems.init();
});