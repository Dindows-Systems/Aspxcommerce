var AspxCart;
function DisableRightClick(event) {
    //For mouse right click
    if (event.button == 2) {
       //  csscody.alert("<h2>Information Message</h2><p>Right Clicking not allowed!</p>");
                   
    }
}
var couponCode = '';
var CartPriceDiscount = 0;
$(function() {
    var itemQuantityInCart;
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var ip = AspxCommerce.utils.GetClientIP();
    var countryName = AspxCommerce.utils.GetAspxClientCoutry();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    var updateCart = true;
    var qtydx = 0;

    AspxCart = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath(),
            method: "",
            url: "",
            ajaxCallMode: 0,
            error: 0
        },
        Vars: {
            CartID: 0,
            CartPriceDiscount: 0

        },
        ajaxCall: function(config) {
            $.ajax({
                type: AspxCart.config.type,
                contentType: AspxCart.config.contentType,
                cache: AspxCart.config.cache,
                async: AspxCart.config.async,
                url: AspxCart.config.url,
                data: AspxCart.config.data,
                dataType: AspxCart.config.dataType,
                success: AspxCart.ajaxSuccess,
                error: AspxCart.ajaxFailure
            });
        },
        ajaxSuccess: function(msg) {
            switch (AspxCart.config.ajaxCallMode) {
                case 1:
                    if ($("#lnkMyCart").length > 0) {
                        HeaderControl.GetCartItemTotalCount(); //for header cart count from database
                    }
                    if ($("#lnkShoppingBag").length > 0) {
                        ShopingBag.GetCartItemCount(); //for bag count
                        ShopingBag.GetCartItemListDetails(); //for shopping bag detail
                    }
                    if ($("#divRelatedItems").length > 0) {
                        RelatedItemsInCart.GetItemRetatedUpSellAndCrossSellList();
                    }
                    AspxCart.GetUserCartDetails();
                    // getdiscount();
                    csscody.info("<h2>Successful Message</h2><p>Your cart has been updated successfully.</p>");

                    break;
                case 2:
                    qtydx = parseFloat(msg.d).toFixed(2);
                    // $("#txtDiscountAmount").val(parseFloat(msg.d).toFixed(2));
                    //$('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    AspxCart.getdiscount();
                    break;
                case 3:
                    var dx = 0;
                    dx = parseFloat(msg.d).toFixed(2);  //12.25  
                    var tt = parseFloat($.trim($("#txtTotalCost").val()).replace(/[^0-9\.]+/g, ""));
                    var sum = tt - dx - qtydx;
                    $("#txtDiscountAmount").val('').val(eval(dx) + eval(qtydx) + eval(AspxCart.Vars.CartPriceDiscount));
                    $("#txtTotalCost").val(sum);
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                    break;
                case 4:
                    if (msg.d.length > 0) {
                        var cartHeading = '';
                        var cartElements = '';
                        cartHeading += '<table cellspacing="0" cellpadding="0" border="0" width="100%" id="tblCartList">';
                        cartHeading += '<tbody><tr class="cssClassHeadeTitle">';
                        cartHeading += '<td class="cssClassSN">Sn.';
                        if (showItemImagesOnCartSetting.toLowerCase() == 'true') {
                            cartHeading += '</td><td class="cssClassItemImageWidth">';
                            cartHeading += 'Item Image';
                        }
                        cartHeading += '</td><td>';
                        cartHeading += 'Description';
                        cartHeading += '</td>';
                        cartHeading += '<td>';
                        cartHeading += 'Variants';
                        cartHeading += '</td>';
                        cartHeading += '<td class="cssClassQTY">';
                        cartHeading += 'Qty';
                        cartHeading += '</td>';
                        cartHeading += '<td class="cssClassTimes">';
                        cartHeading += 'X';
                        cartHeading += '</td>';
                        cartHeading += '<td class="cssClassItemPrice">';
                        cartHeading += 'Unit Price';
                        cartHeading += '</td>';
                        cartHeading += '<td class="cssClassEquals">';
                        cartHeading += '=';
                        cartHeading += '</td>';
                        cartHeading += '<td class="cssClassSubTotal">';
                        cartHeading += 'Sub Total';
                        cartHeading += '</td>';
                        cartHeading += '<td class="cssClassTaxRate">';
                        cartHeading += 'Unit Tax';
                        cartHeading += '</td>';
                        //                    cartHeading += '<td>';
                        //                    cartHeading += 'Remark';
                        //                    cartHeading += '</td>';
                        cartHeading += '<td class="cssClassAction">';
                        cartHeading += 'Action';
                        cartHeading += '</td>';
                        cartHeading += '</tr>';
                        cartHeading += '</table>';
                        $("#divCartDetails").html(cartHeading);
                        $.each(msg.d, function(index, value) {
                            index = index + 1;
                            if (value.ImagePath == "") {
                                value.ImagePath = noImageMyCartPathSetting;
                            }
                            else if (value.AlternateText == "") {
                                value.AlternateText = value.Name;
                            }
                            cartElements += '<tr >';
                            cartElements += '<td>';
                            cartElements += '<b>' + index + "." + '</b>';
                            cartElements += '</td>';
                            if (showItemImagesOnCartSetting.toLowerCase() == 'true') {
                                cartElements += '<td>';
                                cartElements += '<p class="cssClassCartPicture">';
                                cartElements += '<img src="' + AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + value.AlternateText + '" title="' + value.AlternateText + '"></p>';
                                cartElements += '</td>';
                            }
                            cartElements += '<td>';
                            cartElements += '<div class="cssClassCartPictureInformation">';
                            cartElements += '<h3>';
                            cartElements += '<a href="item/' + value.SKU + '.aspx"  costvariants="' + value.CostVariants + '" onclick=AspxCart.SetCostVartSession(this);>' + value.ItemName + ' </a></h3>';
                            cartElements += '<p>';
                            //cartElements += '<textarea  id="itemDescription" readonly="readonly" style="width: 153px; height: 75px;">' + Encoder.htmlDecode(value.ShortDescription) + '</textarea>';
                            cartElements += '' + Encoder.htmlDecode(value.ShortDescription) + '';
                            cartElements += '</p>';
                            cartElements += '</div>';
                            cartElements += '</td>';
                            cartElements += '<td class="row-variants">';
                            cartElements += '' + value.CostVariants + '';
                            cartElements += '</td>';
                            cartElements += '<td class="cssClassQTYInput">';
                            cartElements += '<input class="num-pallets-input" rate="' + value.TaxRateValue.toFixed(2) + '" price="' + (value.Price * rate).toFixed(2) + '" id="txtQuantity_' + value.CartItemID + '" type="text" cartID="' + value.CartID + '" value="' + value.Quantity + '" quantityInCart="' + value.Quantity + '" actualQty="' + value.ItemQuantity + '" costVariantID="' + value.CostVariantsValueIDs + '" itemID="' + value.ItemID + '" addedValue="' + value.Quantity + '">';
                            cartElements += '<label class="lblNotification" style="color: #FF0000;"></label></td>';
                            cartElements += '<td class="cssClassTimes">';
                            cartElements += ' X';
                            cartElements += '</td>';
                            cartElements += '<td class="price-per-pallet">';
                            cartElements += '<span class="cssClassFormatCurrency">' + (value.Price * rate).toFixed(2) + '</span>';
                            cartElements += '</td>';
                            cartElements += '<td class="cssClassEquals">';
                            cartElements += '=';
                            cartElements += '</td>';
                            cartElements += '<td class="row-total">';
                            cartElements += '<input class="row-total-input cssClassFormatCurrency" id="txtRowTotal_' + value.CartID + '" value="' + value.TotalItemCost.toFixed(2) + '"  readonly="readonly" type="text" />';
                            cartElements += '</td>';
                            cartElements += '<td class="row-taxRate">';
                            cartElements += '<span class="cssClassFormatCurrency">' + (value.TaxRateValue * rate).toFixed(2) + '</span>';
                            cartElements += '</td>';
                            //                        cartElements += '<td class="rowremark">';
                            //                        cartElements += '' + value.Remarks + '';
                            //                        cartElements += '</td>';
                            cartElements += '<td>';
                            cartElements += ' <img class="ClassDeleteCartItems" src="' + aspxTemplateFolderPath + '/images/admin/btndelete.png" alt="Delete" title="Delete" value="' + value.CartItemID + '" cartID="' + value.CartID + '" width="14" height="17"/>';
                            cartElements += '</td>';
                            cartElements += '</tr>';
                            AspxCart.Vars.CartID = value.CartID;
                        });
                        AspxCart.GetDiscountCartPriceRule(AspxCart.Vars.CartID, 0);
                        $("#tblCartList").append(cartElements);
                        $("#tblCartList tr:even ").addClass("cssClassAlternativeEven");
                        $("#tblCartList tr:odd ").addClass("cssClassAlternativeOdd");

                        //QuantitityDiscountAmount();

                    }
                    else {
                        $(".cssClassCartInformation").html("<span class=\"cssClassNotFound\">Your shopping cart is empty!</span>");
                    }

                    $(".ClassDeleteCartItems").bind("click", function() {
                        var cartId = $(this).attr("cartID");
                        var cartItemId = $(this).attr("value");
                        var properties = { onComplete: function(e) {
                            AspxCart.DeleteCartItem(cartId, cartItemId, e);
                        }
                        }
                        // Ask user's confirmation before delete records        
                        csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this item from your cart?</p>", properties);
                    });

                    var subTotalAmount = 0.00;
                    $(".row-total-input").each(function() {
                        // alert($(this).val());
                        subTotalAmount = parseFloat(subTotalAmount) + parseFloat($(this).val());
                        //  alert(subTotalAmount);                 
                    });
                    $(".total-box").val('').attr("value", subTotalAmount.toFixed(2));
                    var totalTax = 0.00;
                    $(".num-pallets-input").each(function() {
                        totalTax += ($(this).val() * $(this).attr("rate"));
                    });
                    $(".tax-box").val('').attr("value", totalTax.toFixed(2));

                    $("#txtTotalCost").val((parseFloat($(".total-box").val()) + parseFloat($(".tax-box").val())).toFixed(2) - parseFloat(AspxCart.Vars.CartPriceDiscount));
                    AspxCart.QuantitityDiscountAmount();

                    $(".num-pallets-input").bind("contextmenu", function(e) {
                        return false;
                    });
                    $('.num-pallets-input').bind('paste', function(e) {
                        e.preventDefault();
                    });

                    $(".num-pallets-input").bind('focus', function(e) {
                        $(this).val('');
                        $(this).parents('.cssClassQTYInput').find('.lblNotification').html('');
                        var subTotalAmount = 0.00;
                        var cartId = parseInt($(this).attr("cartID"));
                        $(this).closest('tr').find("#txtRowTotal_" + cartId + "").val($(this).val() * $(this).attr("price"));
                        $(".row-total-input").each(function() {
                            // alert($(this).val());
                            subTotalAmount = parseFloat(subTotalAmount) + parseFloat($(this).val().replace(/[^0-9\.]+/g, ""));

                        });
                        $(".total-box").val('').attr("value", subTotalAmount.toFixed(2));
                        var totalTax = 0.00;
                        $(".num-pallets-input").each(function() {
                            totalTax += ($(this).val() * $(this).attr("rate"));
                        });

                        $("#txtTotalTax").val('').val(totalTax);

                        $(".tax-box").val('').attr("value", totalTax.toFixed(2));
                        //$("#txtTotalCost").val(parseFloat($(".total-box").val()));


                        $("#txtTotalCost").val((parseFloat($(".total-box").val()) + parseFloat($(".tax-box").val())).toFixed(2));
                        var oldd = parseFloat($.trim($("#txtDiscountAmount").val()).replace(/[^0-9\.]+/g, ""));
                        //  var q = parseFloat($.trim($("#txtDiscountAmount").val()).replace(/[^0-9\.]+/g, ""));
                        var tt = parseFloat($.trim($("#txtTotalCost").val()).replace(/[^0-9\.]+/g, ""));
                        if (tt != 0.00) {
                            var sum = tt - oldd;
                            $("#txtTotalCost").val(sum);
                        }
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    });

                    $(".num-pallets-input").bind('select', function(e) {
                        // $(this).val('');
                        $(this).val('');
                        $(this).parents('.cssClassQTYInput').find('.lblNotification').html('');
                        var subTotalAmount = 0.00;
                        var cartId = parseInt($(this).attr("cartID"));
                        $(this).closest('tr').find("#txtRowTotal_" + cartId + "").val($(this).val() * $(this).attr("price"));
                        $(".row-total-input").each(function() {
                            // alert($(this).val());
                            subTotalAmount = parseFloat(subTotalAmount) + parseFloat($(this).val().replace(/[^0-9\.]+/g, ""));

                        });
                        $(".total-box").val('').attr("value", subTotalAmount.toFixed(2));
                        var totalTax = 0.00;
                        $(".num-pallets-input").each(function() {
                            totalTax += ($(this).val() * $(this).attr("rate"));
                        });

                        $("#txtTotalTax").val('').val(totalTax);

                        $(".tax-box").val('').attr("value", totalTax.toFixed(2));
                        //$("#txtTotalCost").val(parseFloat($(".total-box").val()));
                        $("#txtTotalCost").val((parseFloat($(".total-box").val()) + parseFloat($(".tax-box").val())).toFixed(2));
                        var oldd = parseFloat($.trim($("#txtDiscountAmount").val()).replace(/[^0-9\.]+/g, ""));
                        //  var q = parseFloat($.trim($("#txtDiscountAmount").val()).replace(/[^0-9\.]+/g, ""));
                        var tt = parseFloat($.trim($("#txtTotalCost").val()).replace(/[^0-9\.]+/g, ""));
                        if (tt != 0.00) {
                            var sum = tt - oldd;
                            $("#txtTotalCost").val(sum);
                        }
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                    });
                    $(".num-pallets-input").bind('blur', function(e) {
                        $(this).parents('.cssClassQTYInput').find('.lblNotification').html('');
                        $(this).val($(this).attr('addedValue'));
                    });

                    $(".num-pallets-input").bind("keypress", function(e) {
                        if (allowOutStockPurchaseSetting.toLowerCase() == 'false') {
                            if (eval($(this).attr("actualQty")) <= 0) {
                                return false;
                            }
                            else {
                                if ((e.which >= 48 && e.which <= 57)) {
                                    var num;
                                    if (e.which == 48)
                                        num = 0;
                                    if (e.which == 49)
                                        num = 1;
                                    if (e.which == 50)
                                        num = 2;
                                    if (e.which == 51)
                                        num = 3;
                                    if (e.which == 52)
                                        num = 4;
                                    if (e.which == 53)
                                        num = 5;
                                    if (e.which == 54)
                                        num = 6;
                                    if (e.which == 55)
                                        num = 7;
                                    if (e.which == 56)
                                        num = 8;
                                    if (e.which == 57)
                                        num = 9;

                                    var initQtyTxtBox = 0;
                                    var totquantityInCart = 0;
                                    var itemId = $(this).attr("itemID");
                                    if ($(this).attr("costVariantID") != '') {
                                        $(".num-pallets-input[itemID=" + itemId + "]").each(function() {
                                            if ($(this).val() != '') {
                                                initQtyTxtBox += eval($(this).val());
                                            }

                                            totquantityInCart = totquantityInCart + eval($(this).attr("quantityInCart"));
                                        });
                                    }
                                    else {
                                        totquantityInCart = eval($(this).attr("quantityInCart"));
                                    }

                                    //alert(initQty);
                                    var itemQuantityInCart = AspxCart.CheckItemQuantityInCart(itemId);
                                    if (itemQuantityInCart != 0.1) {//to test if the item is downloadable or simple(0.1 downloadable)
                                        if ((eval(($(this).val() + '' + num)) + itemQuantityInCart + initQtyTxtBox - totquantityInCart) > eval($(this).attr("actualQty"))) {
                                            // csscody.alert('<h2>Information Message</h2><p>The Quantity Is Greater Than The Available Quantity.</p>');
                                            $(this).parents('.cssClassQTYInput').find('.lblNotification:eq(0)').html('The Quantity Is Greater Than The Available Quantity.');
                                            return false;

                                        }
                                        else {
                                            $(this).parents('.cssClassQTYInput').find('.lblNotification').html('');
                                        }
                                    }
                                    //}
                                }

                            }
                        }

                        if ($(this).val() == "") {
                            if (e.which != 8 && e.which != 0 && (e.which < 49 || e.which > 57)) {
                                return false;
                            }
                        }
                        else {
                            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                                return false;
                            }
                        }
                        $(this).attr("addedValue", eval(($(this).val() + '' + num)));
                    });

                    $(".num-pallets-input").bind("keyup", function(e) {
                        var subTotalAmount = 0.00;
                        var cartId = parseInt($(this).attr("cartID"));
                        $(this).closest('tr').find("#txtRowTotal_" + cartId + "").val($(this).val() * $(this).attr("price"));
                        $(".row-total-input").each(function() {
                            // alert($(this).val());
                            subTotalAmount = parseFloat(subTotalAmount) + parseFloat($(this).val().replace(/[^0-9\.]+/g, ""));

                        });
                        $(".total-box").val('').attr("value", subTotalAmount.toFixed(2));
                        //  $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        var totalTax = 0.00;
                        $(".num-pallets-input").each(function() {
                            totalTax += ($(this).val() * $(this).attr("rate"));
                        });

                        $("#txtTotalTax").val('').val(totalTax);

                        $(".tax-box").val('').attr("value", totalTax.toFixed(2));
                        //$("#txtTotalCost").val(parseFloat($(".total-box").val()));
                        $("#txtTotalCost").val((parseFloat($(".total-box").val().replace(/[^0-9\.]+/g, "")) + parseFloat($(".tax-box").val().replace(/[^0-9\.]+/g, ""))).toFixed(2));
                        var oldd = parseFloat($.trim($("#txtDiscountAmount").val()).replace(/[^0-9\.]+/g, ""));
                        //  var q = parseFloat($.trim($("#txtDiscountAmount").val()).replace(/[^0-9\.]+/g, ""));
                        var tt = parseFloat($.trim($("#txtTotalCost").val()).replace(/[^0-9\.]+/g, ""));
                        if (tt != 0.00) {
                            var sum = tt - oldd;
                            $("#txtTotalCost").val(sum);
                        }
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    });
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                    break;
                case 5:
                    if ($("#lnkMyCart").length > 0) {
                        HeaderControl.GetCartItemTotalCount(); //for header cart count from database
                    }
                    if ($("#lnkShoppingBag").length > 0) {
                        ShopingBag.GetCartItemCount();
                        ShopingBag.GetCartItemListDetails(); //for details in shopping bag
                    }
                    if ($("#divRelatedItems").length > 0) {
                        RelatedItemsInCart.GetItemRetatedUpSellAndCrossSellList();
                    }
                    AspxCart.GetUserCartDetails();
                    //  getdiscount();
                    csscody.info("<h2>Successful Message</h2><p>Cart's item has been deleted successfully.</p>");

                    break;
                case 6:
                    if ($("#lnkMyCart").length > 0) {
                        HeaderControl.GetCartItemTotalCount(); //for header cart count from database
                    }
                    if ($("#lnkShoppingBag").length > 0) {
                        ShopingBag.GetCartItemCount();
                        ShopingBag.GetCartItemListDetails(); //for details in shopping bag
                    }
                    if ($("#divRelatedItems").length > 0) {
                        RelatedItemsInCart.GetItemRetatedUpSellAndCrossSellList();
                    }
                    AspxCart.GetUserCartDetails();
                    //getdiscount();
                    csscody.info("<h2>Successful Message</h2><p>Cart's items has been cleared successfully.<p>");

                    break;
                case 7:
                    itemQuantityInCart = msg.d;
                    break;
                case 8:
                    var item = msg.d;
                    if (item.Verification) {
                        // UpdateCouponUserRecord(item.CouponID);
                        AspxCart.SetSessionValue('CouponCode', couponCode);
                        AspxCart.AddCouponAppliedCount('CouponApplied', 0);

                        if (item.IsForFreeShipping.toLowerCase() == "yes") {
                            $("#txtShippingTotal").val(0.00);
                            AspxCart.SetSessionValue('IsFreeShipping', 'true');
                            $("#txtCouponCode").val('');
                            csscody.info("<h2>Information Message</h2><p>Congratulation! you need not to worry about shipping cost. It's free!!</p>");

                        }
                        else {
                            //alert(item.CouponAmount);
                            $("#txtDiscountAmount").val(parseFloat($("#txtDiscountAmount").val().replace(/[^0-9\.]+/g, "")) + parseFloat(item.CouponAmount));
                            $("#txtTotalCost").val((parseFloat($("#txtTotalCost").val().replace(/[^0-9\.]+/g, ""))) - parseFloat(item.CouponAmount));
                            $("#txtCouponCode").val('');
                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                            csscody.info("<h2>Information Message</h2><p>Congratulation! you have got discount amount of $" + item.CouponAmount + ".</p>");
                            AspxCart.SetSession();
                        }
                    }
                    else {
                        csscody.alert("<h2>Information Alert</h2><p>Coupon is either invalid, expired, reached it's usage limit or exceeded your cart total purchase amount!</p>");
                        $("#txtCouponCode").val('');
                    }
                    break;
                case 9:
                    break;
                case 10:
                    csscody.info("<h2>Successful Message</h2><p>Coupon user has been updated successfully.</p>");
                    break;
                case 11:
                    break;
                case 12:
                    AspxCart.Vars.CartPriceDiscount = msg.d;
                    break;
            }

        },
        ajaxFailure: function() {
            switch (AspxCart.config.error) {
                case 1:
                    csscody.error('<h2>Error Message</h2><p>Failed to update cart!</p>');
                    break;
                case 4:
                    csscody.error("<h2>Error Message</h2><p>Failed to load cart's details!</p>");
                    break;
                case 5:
                    csscody.error("<h2>Error Message</h2><p>Failed to delete cart's items!</p>");
                    break;
                case 6:
                    csscody.error("<h2>Error Message</h2><p>Failed to clear cart's items!</p>");
                    break;
                case 10:
                    csscody.error('<h2>Error Message</h2><p>Failed to update coupon user!</p>');
                    break;
            }
        }
             , UpdateCart: function(cartItemId, cartID, quantity) {
                 this.config.method = "AspxCommerceWebService.asmx/UpdateShoppingCart";
                 this.config.url = this.config.baseURL + this.config.method;
                 this.config.data = JSON2.stringify({ cartID: cartID, quantitys: quantity, storeID: storeId, portalID: portalId, cartItemIDs: cartItemId, userName: userName, cultureName: cultureName });
                 this.config.ajaxCallMode = 1;
                 this.ajaxCall(this.config);

             }, QuantitityDiscountAmount: function() {
                 var param = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, customerID: customerId, sessionCode: sessionCode });
                 this.config.method = "AspxCommerceWebService.asmx/GetDiscountQuantityAmount";
                 this.config.url = this.config.baseURL + this.config.method;
                 this.config.data = param;
                 this.config.ajaxCallMode = 2;
                 this.ajaxCall(this.config);

             }, getdiscount: function() {
                 this.config.method = "AspxCommerceWebService.asmx/GetSessionVariable";
                 this.config.url = this.config.baseURL + this.config.method;
                 this.config.data = JSON2.stringify({ key: 'DiscountAmount' });
                 this.config.ajaxCallMode = 3;
                 this.ajaxCall(this.config);

             }, SetCostVartSession: function(obj) {
                 if ($(obj).attr("costvariants") != null) {
                     itemCostVariantData = $(obj).attr("costvariants");
                     $.session("ItemCostVariantData", 'empty');
                     $.session("ItemCostVariantData", itemCostVariantData);
                 }
             }, GetUserCartDetails: function() {
                 this.config.method = "AspxCommerceWebService.asmx/GetCartDetails";
                 this.config.url = this.config.baseURL + this.config.method;
                 this.config.async = false;
                 this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, userName: userName, cultureName: cultureName, sessionCode: sessionCode });
                 this.config.ajaxCallMode = 4;
                 this.config.error = 4;
                 this.ajaxCall(this.config);

             },

        GetDiscountCartPriceRule: function(CartID, SpCost) {
            this.config.method = "AspxCommerceWebService.asmx/GetDiscountPriceRule";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.async = false;
            this.config.data = JSON2.stringify({ cartID: CartID, storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName(), shippingCost: SpCost });
            this.config.ajaxCallMode = 12;
            this.ajaxCall(this.config);
        },

        DeleteCartItem: function(cartId, cartItemId, event) {
            if (event) {
                this.config.method = "AspxCommerceWebService.asmx/DeleteCartItem";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ cartID: cartId, cartItemID: cartItemId, customerID: customerId, sessionCode: sessionCode, storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 5;
                this.config.error = 5;
                this.ajaxCall(this.config);
            }
        },
        ClearCartItems: function(event) {
            if (event) {
                var cartID = $("#tblCartList .ClassDeleteCartItems").attr("cartid");
                this.config.method = "AspxCommerceWebService.asmx/ClearAllCartItems";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ cartID: cartID, customerID: customerId, sessionCode: sessionCode, storeID: storeId, portalID: portalId });
                this.config.ajaxCallMode = 6;
                this.config.error = 6;
                this.ajaxCall(this.config);
            }
        }, CheckItemQuantityInCart: function(itemId) {

            this.config.method = "AspxCommerceWebService.asmx/CheckItemQuantityInCart";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ itemID: itemId, storeID: storeId, portalID: portalId, customerID: customerId, sessionCode: sessionCode });
            this.config.ajaxCallMode = 7;
            this.config.error = 7;
            this.ajaxCall(this.config);
            return itemQuantityInCart;
        },
        VerifyCouponCode: function() {
            couponCode = $.trim($("#txtCouponCode").val());
            var totalCost = $("#txtTotalCost").val().replace(/[^0-9\.]+/g, "");
            if (couponCode == "") {
                csscody.alert("<h2>Information Alert</h2><p>Please enter coupon code!</p>");
                return false;
            }
            else {
                var aCount = '';
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "AspxCommerceWebService.asmx/GetSessionVariable",
                    data: JSON2.stringify({ key: 'CouponApplied' }),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function(msg) {
                        // alert(msg.d);
                        //console.debug(msg.d);
                        var x = parseInt(msg.d);
                        aCount = x;
                    },
                    error: function() {
                    	csscody.error("<h2>Error Message</h2><p>Sorry! error occured!</p>");
                    }
                });

                if (aCount != 0) {
                    aCount = parseInt(aCount) + 1;
                }
                // alert(aCount);
                this.config.method = "AspxCommerceWebService.asmx/VerifyCouponCode";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ totalCost: totalCost, couponCode: couponCode, storeID: storeId, portalID: portalId, userName: userName, appliedCount: aCount });
                this.config.ajaxCallMode = 8;
                this.config.error = 8;
                this.ajaxCall(this.config);
            }
        }, AddCouponAppliedCount: function(sessionKey, sessionValue) {
            this.config.method = "AspxCommerceWebService.asmx/SetSessionVariableCoupon";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ key: sessionKey, value: sessionValue });
            this.config.ajaxCallMode = 9;
            this.config.error = 9;
            this.ajaxCall(this.config);

        }, UpdateCouponUserRecord: function(id) {
            this.config.method = "AspxCommerceWebService.asmx/UpdateCouponUserRecord";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ couponCode: id, storeID: storeId, portalID: portalId, userName: userName });
            this.config.ajaxCallMode = 10;
            this.config.error = 10;
            this.ajaxCall(this.config);

        }, SetSessionValue: function(sessionKey, sessionValue) {
            this.config.method = "AspxCommerceWebService.asmx/SetSessionVariable";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ key: sessionKey, value: sessionValue });
            this.config.ajaxCallMode = 11;
            this.config.error = 11;
            this.ajaxCall(this.config);
        },
        SetSession: function() {
            var totalCost = $.trim($("#txtTotalCost").val()).replace(/[^0-9\.]+/g, "");
            if (eval($.trim($("#txtTotalCost").val()).replace(/[^0-9\.]+/g, "")) < eval(minOrderAmountSetting)) {
                csscody.alert('<h2>Information Message</h2><p>You are not eligible to proceed further because your order amount is too low!</p>');
                $("a").removeAttr("href");
                return false;
            }
            AspxCart.SetSessionValue("DiscountAmount", (parseFloat($.trim($("#txtDiscountAmount").val()).replace(/[^0-9\.]+/g, "")) - parseFloat(AspxCart.Vars.CartPriceDiscount)));
        },
        Init: function() {

            $('.num-pallets-input').val('');
            if (userFriendlyURL) {
                  $("#lnkContinueShopping").bind("click", function() { window.location.href = aspxRedirectPath + "Home.aspx"; });
            }
            else {
                   $("#lnkContinueShopping").bind("click", function() { window.location.href = aspxRedirectPath + "Home"; });
            }

            $("#txtCouponCode").val('');
            $("#divCheckOutMultiple").hide();
            if (customerId > 0 && userName.toLowerCase() != 'anonymoususer' && allowMultipleAddShippingSetting.toLowerCase() == 'true') {
                $("#divCheckOutMultiple").show();
            }

            $("#btnSubmitCouponCode").bind("click", function() {
                AspxCart.VerifyCouponCode();
            });

            $("#btnClear").bind("click", function() {
                var properties = { onComplete: function(e) {
                    AspxCart.ClearCartItems(e);
                }
                }
                // Ask user's confirmation before delete records        
                csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to clear all cart's items?</p>", properties);
            });

            $("#btnUpdateShoppingCart").bind("click", function() {
                var cartItemId = '';
                var quantity = '';
                var cartID = 0;
                var updateCart = true;
                $(".num-pallets-input").each(function() {
                    if ($(this).val() == "" || $(this).val() <= 0) {
                        updateCart = false;
                        //alert("Invalid quantity");
                        return false;
                    }
                    var totQtyInTxtBox = 0;
                    var initQtyInCart = 0;
                    var itemId = $(this).attr("itemID");
                    var itemQuantityInCart = AspxCart.CheckItemQuantityInCart(itemId);
                    if ($(this).attr("costVariantID") != '') {
                        $(".num-pallets-input[itemID=" + itemId + "]").each(function() {
                            totQtyInTxtBox = totQtyInTxtBox + eval($(this).val());
                            initQtyInCart = initQtyInCart + eval($(this).attr("quantityInCart"));
                        });
                    }
                    else {
                        totQtyInTxtBox = eval($(this).val());
                        initQtyInCart = eval($(this).attr("quantityInCart"));
                    }
                    if (itemQuantityInCart != 0.1) {//to test if the item is downloadable or simple(-0.1 downloadable)
                        if (allowOutStockPurchaseSetting.toLowerCase() == 'false') {
                            if ((totQtyInTxtBox + itemQuantityInCart - initQtyInCart) > eval($(this).attr("actualQty"))) {
                                // csscody.alert('<h2>Information Message</h2><p>The Quantity Is Greater Than The Available Quantity.</p>');
                                $(this).parents('.cssClassQTYInput').find('.lblNotification:eq(0)').html('The Quantity Is Greater Than The Available Quantity.');
                                updateCart = false;
                                return false;
                            }
                        }
                        else {
                            $(this).parents('.cssClassQTYInput').find('.lblNotification').html('');
                            updateCart = true;
                        }
                    }
                });
                if (updateCart == true) {
                    $(".num-pallets-input").each(function() {
                        cartItemId += parseInt($(this).attr("id").replace(/[^0-9]/gi, '')) + ',';
                        quantity += $(this).val() + ',';
                        cartID = $(this).attr("cartID");
                    });
                    AspxCart.UpdateCart(cartItemId, cartID, quantity);
                } else {
                    csscody.alert("<h2>Information Message</h2><p>Your cart contains invalid quantity!</p>");
                    //alert("Invalid quantity");
                }
            });

            AspxCart.GetUserCartDetails();

            $('.cssClassCouponHelp').hide();
            $('#txtCouponCode').bind("focus", function() {
                // alert('');
                $('.cssClassCouponHelp').show();

            });
            $('#txtCouponCode').bind("focusout", function() {
                $('.cssClassCouponHelp').hide();

            });
        }
    }
    AspxCart.Init();
});
    