var HeaderControl = "";
$(function() {

    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    var myAccountURL = myAccountURLSetting;
    var shoppingCartURL = shoppingCartURLSetting;
    var wishListURL = wishListURLSetting;
    var frmLoginCheck = frmLogin;
    HeaderControl = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCommerceWebService.asmx/",
            url: "",
            method: ""
        },
        vars: {
            totalPrice: ""
        },
        ajaxCall: function(config) {
            $.ajax({
                type: HeaderControl.config.type,
                contentType: HeaderControl.config.contentType,
                cache: HeaderControl.config.cache,
                async: HeaderControl.config.async,
                data: HeaderControl.config.data,
                dataType: HeaderControl.config.dataType,
                url: HeaderControl.config.url,
                success: HeaderControl.ajaxSuccess,
                error: HeaderControl.ajaxFailure
            });
        },
        init: function() {
            HeaderControl.GetCartItemTotalCount();
            if (allowWishListSetting.toLowerCase() == 'true') {
                HeaderControl.GetWishListCount();
            } else {
                $('.cssClassWishList').hide();
            }
            if (customerId > 0 && userName.toLowerCase() != 'anonymoususer') {
                if (userFriendlyURL) {
                    $("#lnkMyAccount").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + myAccountURL + '.aspx');
                }
                else {
                    $("#lnkMyAccount").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + myAccountURL);
                }
            }
            else {
                if (userFriendlyURL) {
                    $("#lnkMyAccount").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + 'Login.aspx');
                }
                else {
                    $("#lnkMyAccount").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + 'Login');
                }
            }

            $("#lnkCheckOut , .cssClassBlueBtn ").click(function() {

                if ($(".cssClassBlueBtn ").length > 0) {
                    if ($("#txtTotalCost").val().replace(/[^-0-9\.]+/g, "") < 0) {
                        csscody.alert("<h2>Information Alert</h2><p>You can't proceed to checkout your amount is not applicable!</p>");
                        return false;
                    }
                }

                var totalCartItemPrice = HeaderControl.GetTotalCartItemPrice();
                //alert(totalCartItemPrice);
                if (totalCartItemPrice == 0) {
                    csscody.alert('<h2>Information Alert</h2><p>You have not added any items in cart yet!</p>');
                    return false;
                }
                if (totalCartItemPrice < minOrderAmountSetting) {
                    csscody.alert('<h2>Information Alert</h2><p>You are not eligible to proceed further: Your order amount is too low!</p>');
                }
                else {

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

                    var newMultiShippingDiv = '';
                    newMultiShippingDiv = '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + singleAddressLink + '">Checkout With Single Address</a>';
                    if (customerId > 0 && userName.toLowerCase() != 'anonymoususer') {
                        if (allowMultipleShippingSetting.toLowerCase() == 'true') {
                            newMultiShippingDiv += '<span class="cssClassOR">OR</span><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + multipleAddressLink + '">Checkout With Multiple Addresses</a>';
                        }
                    }
                    else {
                        if (allowAnonymousCheckOutSetting.toLowerCase() == 'true') {
                            newMultiShippingDiv = '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + singleAddressLink + '">Checkout With Single Address</a>';
                        }
                        else {
                            csscody.alert('<h2>Information Alert</h2><p>Checkout is not allowed for anonymous user!</p>');
                            return false;
                        }
                    }
                    if (allowMultipleShippingSetting.toLowerCase() == 'false' && allowAnonymousCheckOutSetting.toLowerCase() == 'true') {
                        window.location = AspxCommerce.utils.GetAspxRedirectPath() + singleAddressLink;
                    } else {

                        if (customerId == 0) {
                            window.location = AspxCommerce.utils.GetAspxRedirectPath() + singleAddressLink;
                        } else {
                            $("#divCheckoutTypes").html(newMultiShippingDiv);
                            ShowPopupControl('popuprel3');
                        }
                    }
                }

            });
            $(".cssClassClose").click(function() {
                $('#fade, #popuprel3').fadeOut();
            });
        },

        GetWishListCount: function() {
            this.config.url = this.config.baseURL + "CountWishItems";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, sessionCode: sessionCode, userName: userName });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        GetCartItemTotalCount: function() {
            this.config.url = this.config.baseURL + "GetCartItemsCount";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, sessionCode: sessionCode, userName: userName });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        GetTotalCartItemPrice: function() {
            this.config.url = this.config.baseURL + "GetTotalCartItemPrice";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, sessionCode: sessionCode });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
            return HeaderControl.vars.totalPrice;
        },
        ajaxSuccess: function(msg) {
            switch (HeaderControl.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $("#lnkMyWishlist").html("My Wishlist <span class=\"cssClassTotalCount\">[" + msg.d + "]</span>");
                    var myWishlistLink = '';
                    var loginLink = '';
                    if (userFriendlyURL) {
                        myWishlistLink = wishListURL + '.aspx';
                        loginLink = 'Login.aspx';
                    }
                    else {
                        myWishlistLink = wishListURL;
                        loginLink = 'Login';
                    }
                    if (customerId > 0 && userName.toLowerCase() != 'anonymoususer') {
                        $("#lnkMyWishlist").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + myWishlistLink + '');
                    }
                    else {
                        $("#lnkMyWishlist").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + loginLink + '');
                    }
                    break;
                case 2:
                    var myCartLink = '';
                    if (userFriendlyURL) {
                        myCartLink = shoppingCartURL + '.aspx';
                    }
                    else {
                        myCartLink = shoppingCartURL;
                    }
                    $("#lnkMyCart").html("My Cart <span class=\"cssClassTotalCount\">[" + msg.d + "]</span>");
                    if (msg.d == 0) {
                        frmLoginCheck = "false";
                    }
                    if (frmLoginCheck.toLowerCase() == "true") {
                        if (msg.d > 0) {
                            var properties = { onComplete: function(e) {
                                if (e) {
                                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + myCartLink;
                                }
                            }
                            }
                            // Ask user's confirmation before delete records
                            csscody.messageInfo("<h2>Notice Information</h2><p>Your cart contains items. Do you want to look at them?</p>", properties);
                        }
                    }
                    $("#lnkMyCart").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + myCartLink + '');
                    break;
                case 3:
                    HeaderControl.vars.totalPrice = msg.d;
                    break;
            }
        }
    }
    HeaderControl.init();
});