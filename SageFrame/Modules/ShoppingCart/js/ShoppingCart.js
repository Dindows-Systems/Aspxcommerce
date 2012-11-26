var ShopingCart="";
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    var itemCostVariantData = '';

    findPos = function(obj) {
        var curleft = curtop = 0;
        if (obj.offsetParent) {
            curleft = obj.offsetLeft;
            curtop = obj.offsetTop;
            while (obj = obj.offsetParent) {
                curleft += obj.offsetLeft;
                curtop += obj.offsetTop;
            }
        }
        return [curleft, curtop];
    };

    ShopingCart = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxservicePath,
            method: "",
            url: ""
        },

        init: function() {
            ShopingCart.showShoppingCart();
            ShopingCart.LoadAllImages();

            $("#divMiniShoppingCart1").show();
            if (showMiniShopCart.toLowerCase() == 'true') {
                ShopingCart.GetCartItemCount();
                $("#divMiniShoppingCart").show();
            }
            if (userFriendlyURL) {
                $("#lnkViewShoppingCart").attr("href", AspxCommerce.utils.GetAspxRedirectPath() + shoppingCartURL + ".aspx");
            }
            else {
                $("#lnkViewShoppingCart").attr("href", AspxCommerce.utils.GetAspxRedirectPath() + shoppingCartURL);
            }

            $("#lnkMiniShoppingCheckOut").click(function() {
                $(".cssClassCheckOut a").click();
            });
            $(".cssClassClose").bind("click", function() {
                $('#fade, #popuprel4').fadeOut();
            });
        },

        ajaxCall: function(config) {
            $.ajax({
                type: ShopingCart.config.type,
                contentType: ShopingCart.config.contentType,
                cache: ShopingCart.config.cache,
                async: ShopingCart.config.async,
                url: ShopingCart.config.url,
                data: ShopingCart.config.data,
                dataType: ShopingCart.config.dataType,
                success: ShopingCart.ajaxSuccess,
                error: ShopingCart.ajaxFailure
            });
        },

        ajaxSuccess: function(data) {
            switch (ShopingCart.config.ajaxCallMode) {
                case 1:
                    ShopingCart.vars.totalPrice = data.d;
                    break;
                case 2:
                    if (data.d) {
                        $("#fullShoppingCartBag").show();
                        $("#emptyShoppingCartBag").hide();
                        $("#divShoppingCart").show();
                        $("#cartBagItemCount").html('');
                        $("#h2myshoppingCart").html('<span class="cssClassMyShoppingCart">My Shopping Bag [ ' + data.d + ' ]<img id="fullShoppingCartBag" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/shopping-basket_full.png" width="32" height="32" alt="Shopping Basket" title="Shopping Basket" align="right" /></span>');
                    }
                    else {
                        $("#fullShoppingCartBag").hide();
                        $("#emptyShoppingCartBag").show();
                        $("#divShoppingCart").hide();
                        $("#cartBagItemCount").html("[ <b>Your shopping cart is empty!</b> ]");
                        $("#h2myshoppingCart").html('<span class="cssClassMyShoppingCart">Your shopping cart is empty!<img id="emptyShoppingCartBag" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/shopping-basket_empty.png" width="25" height="25" alt="Shopping Basket" title="Shopping Basket" align="right" /></span>');
                    }
                    break;
                case 3:
                    $("#tblCartListItems").html('');
                    if (data.d.length > 0) {
                        $.each(data.d, function(index, item) {
                            ShopingCart.BindCartItemslist(item, index);
                        });
                        $("a").bind("click", function(e) {
                            if ($(this).attr("costvariants") != null) {
                                itemCostVariantData = $(this).attr("costvariants");
                                $.session("ItemCostVariantData", 'empty');
                                $.session("ItemCostVariantData", itemCostVariantData);
                            }
                        });
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                        $(".imgCartItemListDelete").bind("click", function() {
                            var cartId = parseInt($(this).attr("id").replace(/[^0-9]/gi, ''));
                            var cartItemId = parseInt($(this).attr("name").replace(/[^0-9]/gi, ''));
                            var properties = { onComplete: function(e) {//
                                if ($("#cartItemCount").offset() != null) {
                                    ShopingBag.DeleteCartItemByID(cartId, cartItemId, e);
                                    ShopingCart.DeleteCartItemByID(cartId, cartItemId, e);
                                }
                                else {
                                    ShopingCart.DeleteCartItemByID(cartId, cartItemId, e);
                                }
                                ShopingCart.DeleteCartItemByID(cartId, cartItemId, e);

                            }
                            }
                            csscody.confirm("<h1>Delete Confirmation</h1><p>Do you want to delete this item from your cart list?</p>", properties);
                        });
                    }
                    else {

                    }
                    break;
                case 4:
                    if ($("#cartItemCount").offset() != null) {
                        ShopingBag.GetCartItemCount();
                        ShopingBag.GetCartItemListDetails();
                    }
                    ShopingCart.GetCartItemCount(); //for bag count                   
                    ShopingCart.GetCartItemListDetails(); //for shopping bag detail

                    //For my Cart Link Header
                    if ($("#lnkMyCart").length > 0) {
                        HeaderControl.GetCartItemTotalCount();
                    }
                    if ($('#divCartDetails').length > 0) {
                        AspxCart.GetUserCartDetails(); //for my cart's tblitemList table
                    }
                    if ($("#divLatestItems").length > 0) {
                        LatestItems.GetLatestItems();
                    }
                    if ($("#divShowCategoryItemsList").length > 0) {
                        categoryDetails.LoadAllCategoryContents(1, parseInt($("#ddlPageSize").val()), 0);
                    }
                    //                    if ($("#divYouMayAlsoLike").length > 0) {
                    //                        GetYouMayAlsoLikeItemsList();
                    //                    }
                    if ($("#divShowSimpleSearchResult").length > 0) {
                        ItemList.BindSimpleSearchResultItems(1, $("#ddlSimpleSearchPageSize").val(), 0);
                    }
                    if ($("#divOptionsSearchResult").length > 0) {
                        ItemList.BindShoppingOptionResultItems(1, $('#ddlOptionPageSize').val(), 0);
                    }
                    if ($("#divShowTagItemResult").length > 0) {
                        var items_per_page = $('#ddlTagItemPageSize').val();
                        ViewTagItem.ListTagsItems(1, items_per_page, 0);
                    }
                    if ($("#divShowAdvanceSearchResult").length > 0) {
                        AdvanceSearch.ShowSearchResult(1, $('#ddlPageSize').val(), 0);
                    }
                    if ($("#divWishListContent").length > 0) {
                        WishList.GetWishItemList();
                    }
                    if ($("#divRelatedItems").length > 0) {
                        GetItemRetatedUpSellAndCrossSellList();
                    }

                    if ($("#dynItemDetailsForm").length > 0) {
                        BindItemBasicByitemSKU(itemSKU);
                    }
                    return false;
                    break;
                case 5:
                    break;
            }
        },
        ajaxFailure: function() {
            switch (ShopingCart.config.error) {
            }
        }, LoadAllImages: function() {
            $('#fullShoppingCartBag').attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/shopping-basket_full.png');
            $("#emptyShoppingCartBag").attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/shopping-basket_empty.png');
        },
        GetCartItemCount: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, sessionCode: sessionCode, userName: userName });
            this.config.method = "AspxCommerceWebService.asmx/GetCartItemsCount";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);

        }, showShoppingCart: function() {

            ShopingCart.GetCartItemListDetails();
            var obj = $('#lnkshoppingcart');
            var pos = findPos(obj);
            return false;

        }, GetCartItemListDetails: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, userName: userName, cultureName: cultureName, sessionCode: sessionCode });
            this.config.method = "AspxCommerceWebService.asmx/GetCartDetails";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);

        }, BindCartItemslist: function(item, index) {
            if (item.CostVariants != '') {
                $('#tblCartListItems').append('<tr id="productID_' + item.ItemID + '"><td><a  costvariants="' + item.CostVariants + '" href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + item.SKU + '.aspx">' + item.ItemName + ' (' + item.CostVariants + ')' + '</a></td><td>' + item.Quantity + '&nbsp;&nbsp;*&nbsp;&nbsp;<span class="cssClassFormatCurrency">' + item.Price + '</span>&nbsp;&nbsp;=&nbsp;&nbsp;<span class="cssClassFormatCurrency">' + item.TotalItemCost + '</span></td><td><img class="imgCartItemListDelete" name="' + item.CartItemID + '" id="btnItemDelete_' + item.CartID + '" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>');
            }
            else {
                $('#tblCartListItems').append('<tr id="productID_' + item.ItemID + '"><td><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + item.SKU + '.aspx">' + item.ItemName + '</a></td><td>' + item.Quantity + '&nbsp;&nbsp;*&nbsp;&nbsp;<span class="cssClassFormatCurrency">' + item.Price + '</span>&nbsp;&nbsp;=&nbsp;&nbsp;<span class="cssClassFormatCurrency">' + item.TotalItemCost + '</span></td><td><img class="imgCartItemListDelete" name="' + item.CartItemID + '" id="btnItemDelete_' + item.CartID + '" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>');
            }

        }, DeleteCartItemByID: function(id, cartItemId, event) {
            if (event) {
                var param = JSON2.stringify({ cartID: id, cartItemID: cartItemId, customerID: customerId, sessionCode: sessionCode, storeID: storeId, portalID: portalId, userName: userName });
                this.config.method = "AspxCommerceWebService.asmx/DeleteCartItem";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
            }
            return false;
        },
        vars: {
            totalPrice: ""
        }, GetTotalCartItemPrice: function() {
            this.config.method = "AspxCommerceWebService.asmx/GetTotalCartItemPrice";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, sessionCode: sessionCode });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
            return ShopingCart.vars.totalPrice;
        }
    }

    ShopingCart.init();

});