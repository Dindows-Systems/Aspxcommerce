var ShopingBag="";
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

    ShopingBag = {
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
            ShopingBag.LoadAllImages();
            ShopingBag.hideShoppingCart();
            $("#divMiniShoppingCart").hide();
            if (showMiniShopCart.toLowerCase() == 'true') {
                ShopingBag.GetCartItemCount();
                $("#divMiniShoppingCart").show();
            }
            //        $("#lnkMyCart").click(function() {
            //            GetCartItemListDetails();
            //        });
            if (userFriendlyURL) {
                $("#lnkViewCart").attr("href", AspxCommerce.utils.GetAspxRedirectPath() + shoppingCartURL + ".aspx");
            }
            else {
                $("#lnkViewCart").attr("href", AspxCommerce.utils.GetAspxRedirectPath() + shoppingCartURL);
            }

            $("#lnkMiniCheckOut").click(function() {

                if (customerId <= 0 && userName.toLowerCase() == 'anonymoususer') {
                    if (allowAnonymousCheckOut.toLowerCase() == 'false') {
                        csscody.alert('<h2>Information Alert</h2><p>Checkout is not allowed for Anonymous User!</p>');
                        return false;
                    }
                }
                var singleAddressLink = '';
                var multipleAddressLink = '';
                if (userFriendlyURL) {
                    singleAddressLink = 'Single-Address-Checkout.aspx';
                    multipleAddressLink = 'Multiple-Address-Checkout.aspx';
                }
                else {
                    singleAddressLink = 'Single-Address-Checkout';
                    multipleAddressLink = 'Multiple-Address-Checkout';
                }
                var totalCartItemPrice = ShopingBag.GetTotalCartItemPrice();
                if (totalCartItemPrice < minOrderAmount) {
                    csscody.alert('<h2>Information Alert</h2><p>You are not eligible to proceed further:Your Order Amount is too low!!!</p>');
                    return false;
                }
                else {
                    var newMultiShippingDiv = '';
                    if (userFriendlyURL) {
                        newMultiShippingDiv = '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + singleAddressLink + '" >Checkout With Single Address</a>';
                    }
                    else {
                        newMultiShippingDiv = '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + singleAddressLink + '">Checkout With Single Address</a>';
                    }
                    if (customerId > 0 && userName.toLowerCase() != 'anonymoususer') {
                        if (userFriendlyURL) {
                            if (allowMultipleAddChkOut.toLowerCase() == 'true') {
                                newMultiShippingDiv += '<span class="cssClassOR">OR</span><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + multipleAddressLink + '">Checkout With Multiple Address</a>';
                            }

                        }
                        else {
                            if (allowMultipleAddChkOut.toLowerCase() == 'true') {
                                newMultiShippingDiv += '<span class="cssClassOR">OR</span><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + multipleAddressLink + '">Checkout With Multiple Address</a>';
                            }
                        }
                    }
                }
                if (allowMultipleAddChkOut.toLowerCase() == 'false' && allowAnonymousCheckOut.toLowerCase() == 'true') {
                    window.location = AspxCommerce.utils.GetAspxRedirectPath() + singleAddressLink;
                } else {
                    $("#divMiniCheckoutTypes").html(newMultiShippingDiv);
                    ShowPopupControl('popuprel4');
                }

            });
            $(".cssClassClose").bind("click", function() {
                $('#fade, #popuprel4').fadeOut();
            });
        },

        ajaxCall: function(config) {
            $.ajax({
                type: ShopingBag.config.type,
                contentType: ShopingBag.config.contentType,
                cache: ShopingBag.config.cache,
                async: ShopingBag.config.async,
                url: ShopingBag.config.url,
                data: ShopingBag.config.data,
                dataType: ShopingBag.config.dataType,
                success: ShopingBag.ajaxSuccess,
                error: ShopingBag.ajaxFailure
            });
        },

        ajaxSuccess: function(data) {
            switch (ShopingBag.config.ajaxCallMode) {
                case 1:
                    ShopingBag.vars.totalPrice = data.d;
                    break;
                case 2:
                    if (data.d > 0) {
                        $("#imgarrow").show();
                        $("#fullShoppingBag").show();
                        $("#emptyShoppingBag").hide();
                        $("#cartItemCount").html('<a onclick="if(!this.disabled){ShopingBag.showShoppingCart();};" href="javascript:void(0);" id="lnkshoppingcart">My Shopping Bag [ ' + data.d + ' ]</a>');
                    }
                    else {
                        $("#imgarrow").hide();
                        $("#fullShoppingBag").hide();
                        $("#emptyShoppingBag").show();
                        $("#cartItemCount").html("[ <b>Your shopping cart is empty!</b> ]");
                    }
                    break;
                case 3:
                    $("#tblListCartItems").html('');
                    if (data.d.length > 0) {
                        $.each(data.d, function(index, item) {
                            ShopingBag.BindCartItemslist(item, index);
                        });
                        $("a").bind("click", function(e) {
                            //   alert($(this).attr("costvariants"));
                            if ($(this).attr("costvariants") != null) {
                                itemCostVariantData = $(this).attr("costvariants");
                                //alert(itemCostVariantData);
                                $.session("ItemCostVariantData", 'empty');
                                $.session("ItemCostVariantData", itemCostVariantData);
                            }
                        });
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });



                        $(".imgCartItemDelete").bind("click", function() {
                            var cartId = parseInt($(this).attr("id").replace(/[^0-9]/gi, ''));
                            var cartItemId = parseInt($(this).attr("name").replace(/[^0-9]/gi, ''));
                            var properties = { onComplete: function(e) {
                                if ($("#divMiniShoppingCart1").offset() != null) {
                                    ShopingCart.DeleteCartItemByID(cartId, cartItemId, e);
                                    ShopingBag.DeleteCartItemByID(cartId, cartItemId, e);
                                }
                                else {
                                    ShopingBag.DeleteCartItemByID(cartId, cartItemId, e);

                                }
                            }
                            }
                            csscody.confirm("<h1>Delete Confirmation</h1><p>Do you want to delete this item from your cart list?</p>", properties);
                        });
                    }
                    else {
                        $("#ShoppingCartPopUp").hide();
                    }
                    break;
                case 4:
                    ShopingBag.GetCartItemCount(); //for bag count
                    ShopingBag.GetCartItemListDetails(); //for shopping bag detail
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
            switch (ShopingBag.config.error) {
            }
        }, LoadAllImages: function() {
            $('#fullShoppingBag').attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/shopping-basket_full.png');
            $("#emptyShoppingBag").attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/shopping-basket_empty.png');
            $("#imgarrow").attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/arrow_down.gif');
        }, hideShoppingCart: function() {
            $('#ShoppingCartPopUp').hide();
            $('#imgarrow').attr("src", AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/arrow_down.gif");
        }, GetCartItemCount: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, sessionCode: sessionCode, userName: userName });
            this.config.method = "AspxCommerceWebService.asmx/GetCartItemsCount";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);

        }, showShoppingCart: function() {
            ShopingBag.GetCartItemListDetails();
            var obj = $('#lnkshoppingcart');
            var pos = findPos(obj);
            $('#ShoppingCartPopUp').css('left', pos[0] - 180);
            $('#ShoppingCartPopUp').css('top', pos[1] + 20);

            $('#ShoppingCartPopUp').slideToggle("slow");

            if ($('#imgarrow').attr("src").indexOf("arrow_up.gif") > -1) {
                $('#imgarrow').attr("src", AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/arrow_down.gif");

            } else {
                $('#imgarrow').attr("src", AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/arrow_up.gif");
                //            if (CheckCartToProceed('Header') == false) {
                //                document.getElementById("ctl00_ctl00_ctrlHeader_btnProceed").style.display = "none";
                //            } else {
                //                document.getElementById("ctl00_ctl00_ctrlHeader_btnProceed").style.display = "inline";
                //            }
            }
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
                $('#tblListCartItems').append('<tr><td><a  costvariants="' + item.CostVariants + '" href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + item.SKU + '.aspx">' + item.ItemName + ' (' + item.CostVariants + ')' + '</a></td><td>' + item.Quantity + '&nbsp;&nbsp;*&nbsp;&nbsp;<span class="cssClassFormatCurrency">' + item.Price + '</span>&nbsp;&nbsp;=&nbsp;&nbsp;<span class="cssClassFormatCurrency">' + item.TotalItemCost + '</span></td><td><img class="imgCartItemDelete" name="' + item.CartItemID + '" id="btnDelete_' + item.CartID + '" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>');
            }
            else {
                $('#tblListCartItems').append('<tr><td><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + item.SKU + '.aspx">' + item.ItemName + '</a></td><td>' + item.Quantity + '&nbsp;&nbsp;*&nbsp;&nbsp;<span class="cssClassFormatCurrency">' + item.Price + '</span>&nbsp;&nbsp;=&nbsp;&nbsp;<span class="cssClassFormatCurrency">' + item.TotalItemCost + '</span></td><td><img class="imgCartItemDelete" name="' + item.CartItemID + '" id="btnDelete_' + item.CartID + '" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>');
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
            return ShopingBag.vars.totalPrice;
        }
    }

    ShopingBag.init();

});