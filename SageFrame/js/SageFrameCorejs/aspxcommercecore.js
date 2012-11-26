//<![CDATA[
var AspxCommerce = {};
$(function() {
    AspxCommerce = {
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
            url: "",
            ajaxCallMode: 0

        },

        vars: {
            IsAlive: true
        },

        ajaxCall: function(config) {
            $.ajax({
                type: AspxCommerce.config.type,
                contentType: AspxCommerce.config.contentType,
                cache: AspxCommerce.config.cache,
                async: AspxCommerce.config.async,
                url: AspxCommerce.config.url,
                data: AspxCommerce.config.data,
                dataType: AspxCommerce.config.dataType,
                success: AspxCommerce.ajaxSuccess,
                error: AspxCommerce.ajaxFailure
            });
        },

        utils: {
            GetStoreID: function() {
                return storeID;
            },
            GetPortalID: function() {
                return portalID;
            },
            GetUserName: function() {
                return userName;
            },
            GetCultureName: function() {
                return cultureName;
            },
            GetCustomerID: function() {
                return customerID;
            },
            GetTemplateName: function() {
                return templateName;
            },
            IsUserFriendlyUrl: function() {
                return Boolean.parse(IsUseFriendlyUrls);
            },
            GetSessionCode: function() {
                return sessionCode;
            },
            GetClientIP: function() {
                return clientIPAddress;
            },
            GetAspxServicePath: function() {
                return aspxservicePath;
            },
            GetAspxRedirectPath: function() {
                return aspxRedirectPath;
            },
            GetAspxRootPath: function() {
                return aspxRootPath;
            },
            GetAspxTemplateFolderPath: function() {
                return aspxTemplateFolderPath;
            }
            ,
            GetAspxClientCoutry: function() {
                return aspxCountryName;
            }

        },

        Busy: {
            LoadingShow: function() {
                //                var id = "#dialog";
                //                //Get the screen height and width
                //                var maskHeight = $(document).height();
                //                var maskWidth = $(window).width();
                //                //Set heigth and width to mask to fill up the whole screen
                //                $('#mask').css({ 'width': maskWidth, 'height': maskHeight });
                //                //transition effect		
                //                $('#mask').fadeIn(500);
                //                $('#mask').fadeTo("slow", 0.8);
                //                //Get the window height and width
                //                var winH = $(window).height();
                //                var winW = $(window).width();
                //                //Set the popup window to center
                //                $(id).css('top', winH / 2 - $(id).height() / 2);
                //                $(id).css('left', winW / 2 - $(id).width() / 2);
                //                //transition effect
                //                $(id).fadeIn(3000);
                $(".cssClassloadingDiv").parent('div').show();
                $(window).scrollTop(0);
            },

            LoadingHide: function() {
                // $('#mask').hide();
                // $('#dialog').hide();
                $(".cssClassloadingDiv").parent('div').hide();
            }
        },

        CheckSessionActive: function() {
            AspxCommerce.config.url = AspxCommerce.config.baseURL + "AspxCommerceWebService.asmx/" + "CheckSessionActive";
            AspxCommerce.config.data = '{}';
            AspxCommerce.config.ajaxCallMode = 1;
            AspxCommerce.ajaxCall(AspxCommerce.config);
        },


        RootFunction: {
            BindRecentlyComparedItem: function(response, index) {
                var RecentlyComparedItems = '';
                if (index % 2 == 0) {
                    RecentlyComparedItems = '<tr class="cssClassAlternativeEven"><td><a href="' + aspxRedirectPath + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a></td></tr>';
                }
                else {
                    RecentlyComparedItems = '<tr class="cssClassAlternativeOdd"><td><a href="' + aspxRedirectPath + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a></td></tr>';
                }
                $("#tblRecentlyComparedItemList>tbody").append(RecentlyComparedItems);
            },

            GetRecentlyComparedItemList: function(storeId, portalId, userName, cultureName, aspxRootPath, CompareCount) {
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "AspxCommerceWebService.asmx/GetRecentlyComparedItemList",
                    data: JSON2.stringify({ count: CompareCount, storeID: storeId, portalID: portalId, cultureName: cultureName }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        $("#tblRecentlyComparedItemList>tbody").html('');
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                                AspxCommerce.RootFunction.BindRecentlyComparedItem(item, index);
                            });
                        }
                        else {
                            $("#tblRecentlyComparedItemList>tbody").html("<tr><td><span class=\"cssClassNotFound\">No items in recent compare list!</span></tr></td>");
                        }
                    },
                    error: function(msg) {
                        csscody.error('<h2>Error Message</h2><p>Failed to add item in compare list!</p>');
                    }
                });
            },

            Login: function(returnUrl) {
                //                if (returnUrl == "" || returnUrl == null) {
                //                    window.location.href = AspxCommerce.utils.GetAspxRootPath() + 'Login.aspx';
                //                    return false;
                //                }
                //                else {
                returnUrl = window.location.href;
                window.location.href = AspxCommerce.utils.GetAspxRootPath() + 'Login.aspx?' + "ReturnUrl=" + returnUrl;
                return false;
                // }
            },

            AddToWishList: function(itemID) {
                if (AspxCommerce.utils.GetUserName().toLowerCase() != 'anonymoususer') {
                    var checkparam = { ID: itemID, storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), userName: AspxCommerce.utils.GetUserName() };
                    var checkdata = JSON2.stringify(checkparam);
                    $.ajax({
                        type: "POST",
                        url: aspxservicePath + "AspxCommerceWebService.asmx/CheckWishItems",
                        data: checkdata,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function(msg) {
                            if (msg.d) {
                                csscody.alert('<h2>Information Alert</h2><p>The selected item already exist in wishlist.</p>');
                            }
                            else {
                                AspxCommerce.RootFunction.AddToWishListFromJS(itemID, AspxCommerce.utils.GetStoreID(), AspxCommerce.utils.GetPortalID(), AspxCommerce.utils.GetUserName(), AspxCommerce.utils.GetClientIP(), AspxCommerce.utils.GetAspxClientCoutry());


                            }
                        },
                        error: function(msg) {
                            csscody.error('<h2>Error Message</h2><p>Failed to add item in whish list!</p>');
                        }
                    });
                }
                else {
                    AspxCommerce.RootFunction.Login(window.location.href);
                }
            },

            AddToCartToJSFromTemplate: function(itemId, itemPrice, itemSKU, itemQuantity) {
                AspxCommerce.RootFunction.AddToCartFromJS(itemId, itemPrice, itemSKU, itemQuantity, AspxCommerce.utils.GetStoreID(), AspxCommerce.utils.GetPortalID(), AspxCommerce.utils.GetCustomerID(), AspxCommerce.utils.GetSessionCode(), AspxCommerce.utils.GetUserName(), AspxCommerce.utils.GetCultureName());
            },

            AddToWishListFromJS: function(itemID, storeId, portalId, userName, ip, countryName) {
                // alert("from core Js");      
                var addparam = { ID: itemID, storeID: storeId, portalID: portalId, userName: userName, IP: ip, countryName: countryName };
                var adddata = JSON2.stringify(addparam);
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "AspxCommerceWebService.asmx/SaveWishItems",
                    data: adddata,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        //MyWishList();
                        if ($('#lnkMyWishlist').length > 0) {
                            //IncreaseWishListCount(); // for header counter increase                            
                            HeaderControl.GetWishListCount(); // for header wish counter increase for database
                        }
                        if ($('#divRecentlyAddedWishList').length > 0) {
                            WishItems.BindMyWishList(); //for wishlist item in rightside
                        }
                        csscody.info('<h2>Successful Message</h2><p>Item has been successfully added to wishlist.</p>');
                    },
                    error: function(msg) {
                        csscody.error('<h2>Error Message</h2><p>Failed to add item in wishlist!</p>');
                    }
                });
            },

            IncreaseWishListCount: function() {
                var wishListCount = $('#lnkMyWishlist span').html().replace(/[^0-9]/gi, '');
                wishListCount = parseInt(wishListCount) + 1;
                $('.cssClassLoginStatusInfo ul li a#lnkMyWishlist span').html("[" + wishListCount + "]");
            }
  ,
            AddToCartFromJS: function(itemId, itemPrice, itemSKU, itemQuantity, storeId, portalId, customerId, sessionCode, userName, cultureName) {
                //alert("from core Js");
                var param = { itemID: itemId, itemPrice: itemPrice, itemQuantity: itemQuantity, storeID: storeId, portalID: portalId, custometID: customerId, sessionCode: sessionCode, userName: userName, cultureName: cultureName };
                var data = JSON2.stringify(param);
                var myCartUrl;
                var addToCartProperties = { onComplete: function(e) {
                    if (e) {
                        if (AspxCommerce.utils.IsUserFriendlyUrl) {
                            myCartUrl = myCartURL + '.aspx';
                        }
                        else {
                            myCartUrl = myCartURL;
                        }

                        window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + myCartUrl;
                    }
                }
                }
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "AspxCommerceWebService.asmx/AddItemstoCart",
                    data: data,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        if (msg.d) {
                            AspxCommerce.RootFunction.RedirectToItemDetails(itemSKU);
                        }
                        else {
                            /////////////////////******Fly To basket****///////////////////
                            if ($("#divMiniShoppingCart1").offset() != null) {
                                $("#CartItemLoader").html('<img src="./Modules/ShoppingCart/image/loader.gif">');
                                var basketX = '';
                                var basketY = '';
                                var Itemid = "productImageWrapID_" + itemId;
                                var productIDValSplitter = $((Itemid).split("_"));
                                var productIDVal = productIDValSplitter[1];
                                var productX = $("#productImageWrapID_" + productIDVal).offset().left;
                                var productY = $("#productImageWrapID_" + productIDVal).offset().top;
                                if ($("#productID_" + productIDVal).length > 0) {
                                    basketX = $("#productID_" + productIDVal).offset().left;
                                    basketY = $("#productID_" + productIDVal).offset().top;
                                }
                                else {
                                    basketX = $("#divMiniShoppingCart1").offset().left;
                                    basketY = $("#divMiniShoppingCart1").offset().top;
                                }
                                var gotoX = basketX - productX;
                                var gotoY = basketY - productY;

                                var newImageWidth = $("#productImageWrapID_" + productIDVal).width() / 5;
                                var newImageHeight = $("#productImageWrapID_" + productIDVal).height() / 5;

                                $("#productImageWrapID_" + productIDVal + " img")
		            .clone()
		            .prependTo("#productImageWrapID_" + productIDVal)
		            .css({ 'position': 'absolute' })
		            .animate({ opacity: 0.4 }, 50)
            		.animate({ opacity: 0.4,
            		    marginLeft: gotoX,
            		    marginTop: gotoY,
            		    width: newImageWidth,
            		    height: newImageHeight
            		}, 1500, function() {
            		    $(this).remove();

            		    csscody.addToCart('<h2>Successful Message</h2><p>Item has been successfully added to cart.</p>', addToCartProperties);
            		    //TODO:: Add jQuery Counter increament HERE :: done
            		    //  IncreaseMyCartItemCount(); //for header cart count
            		    HeaderControl.GetCartItemTotalCount(); //for header cart count from database
            		    if ($("#cartItemCount").offset() != null) {
            		        ShopingBag.GetCartItemCount(); //for shopping bag counter from database
            		        //  IncreaseShoppingBagCount(); // for shopping bag counter from static
            		        ShopingBag.GetCartItemListDetails(); //for details in shopping bag
            		    }

            		    if ($("#divMiniShoppingCart1").offset() != null) {
            		        ShopingCart.GetCartItemCount();
            		        ShopingCart.GetCartItemListDetails();
            		    }
            		    if ($("#divCartDetails").length > 0) {
            		        AspxCart.GetUserCartDetails(); //for binding mycart's tblCartList
            		    }
            		    if ($("#divLatestItems").length > 0) {
            		        LatestItems.GetLatestItems();
            		    }
            		    if ($("#divShowCategoryItemsList").length > 0) {
            		        categoryDetails.LoadAllCategoryContents(1, parseInt($("#ddlPageSize").val()), 0);
            		    }
            		    if ($("#divYouMayAlsoLike").length > 0) {
            		        ItemDetail.GetYouMayAlsoLikeItemsList();
            		    }
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
            		        RelatedItemsInCart.GetItemRetatedUpSellAndCrossSellList();
            		    }
            		    if ($("#productID_" + productIDVal).length > 0) {
            		        $("#productID_" + productIDVal).animate({ opacity: 0 }, 100);
            		        $("#productID_" + productIDVal).animate({ opacity: 0 }, 100);
            		        $("#productID_" + productIDVal).animate({ opacity: 1 }, 100);
            		        $("#CartItemLoader").empty();

            		    } else {
            		        $("#tblCartListItems tr:last").hide();
            		        $("#tblCartListItems tr:last").show("slow");
            		        $("#CartItemLoader").empty();
            		    }
            		});
                            }
                            else {
                                csscody.addToCart('<h2>Successful Message</h2><p>Item has been successfully added to cart.</p>', addToCartProperties);
                                //TODO:: Add jQuery Counter increament HERE :: done
                                //  IncreaseMyCartItemCount(); //for header cart count
                                HeaderControl.GetCartItemTotalCount(); //for header cart count from database
                                ShopingBag.GetCartItemCount(); //for shopping bag counter from database
                                //  IncreaseShoppingBagCount(); // for shopping bag counter from static
                                ShopingBag.GetCartItemListDetails(); //for details in shopping bag
                                if ($("#divCartDetails").length > 0) {
                                    AspxCart.GetUserCartDetails(); //for binding mycart's tblCartList
                                }
                                if ($("#divLatestItems").length > 0) {
                                    LatestItems.GetLatestItems();
                                }
                                if ($("#divShowCategoryItemsList").length > 0) {
                                    categoryDetails.LoadAllCategoryContents(1, parseInt($("#ddlPageSize").val()), 0);
                                }
                                if ($("#divYouMayAlsoLike").length > 0) {
                                    ItemDetail.GetYouMayAlsoLikeItemsList();
                                }
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
                                    RelatedItemsInCart.GetItemRetatedUpSellAndCrossSellList();
                                }
                            }
                        }
                    }
                });
            },

            RedirectToItemDetails: function(itemSKU) {
                window.location.href = aspxRedirectPath + 'item/' + itemSKU + '.aspx';
                return false;
            }

        },

        ajaxSuccess: function(data) {
            switch (AspxCommerce.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    AspxCommerce.vars.IsAlive = data.d;
                    break;
            }
        },

        init: function() {

            //  $('body').append('<div id="ajaxBusy"><div id="dialog" style="background-color:#AAAAAA; position:absolute;left:50%;top:50%;display:none;z-index:9999;" >Please Wait...<br /><img src="' + AspxCommerce.utils.GetAspxRootPath() + 'Templates/Default/images/progress_bar.gif" alt="" title="Loading"/></div><div id="mask" style=" position:absolute;left:0;top:0;z-index:9000;background-color:#000;display:none;"></div></div>');


        }
    };
    AspxCommerce.init();

});

//]]>