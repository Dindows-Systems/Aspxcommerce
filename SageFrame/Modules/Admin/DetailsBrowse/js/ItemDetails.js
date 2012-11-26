 var ItemDetail = "";
 Variable = function(height, width, thumbWidth, thumbHeight) {
 	this.height = height;
 	this.width = width;
 	this.thumbHeight = thumbHeight;
 	this.thumbWidth = thumbWidth;
 };
    var newObject = new Variable(255, 320, 87, 75);
    $(function() {
        var userModuleID = UsrModuleID;
        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var customerId = AspxCommerce.utils.GetCustomerID();
        var userIP = AspxCommerce.utils.GetClientIP();
        var countryName = AspxCommerce.utils.GetAspxClientCoutry();
        var sessionCode = AspxCommerce.utils.GetSessionCode();
        var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
        var itemId = itemID;
        var itemName = itemNamePageBehind;
        var RelatedItems = '';
        var ItemTags = '';
        var TagNames = '';
        var MyTags = '';
        var UserTags = '';
        var ratingValues = '';
        var ItemsReview = '';
        var arrItemDetailsReviewList = new Array();
        var arrItemReviewList = new Array();
        var arrCostVariants;
        var FormCount = new Array();

        ItemDetail = {
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
                oncomplete: 0,
                ajaxCallMode: 0,
                error: 0
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: ItemDetail.config.type,
                    contentType: ItemDetail.config.contentType,
                    cache: ItemDetail.config.cache,
                    async: ItemDetail.config.async,
                    url: ItemDetail.config.url,
                    data: ItemDetail.config.data,
                    dataType: ItemDetail.config.dataType,
                    success: ItemDetail.ajaxSuccess,
                    error: ItemDetail.ajaxFailure,
                    complete: ItemDetail.oncomplete
                });
            },
            ajaxSuccess: function(msg) {
                switch (ItemDetail.config.ajaxCallMode) {
                    case 1:
                        ItemDetail.vars.countCompareItems = msg.d;
                        break;
                    case 2:
                        $("#itemQtyDiscount>tbody").html();
                        if (msg.d.length > 0) {
                            $("#bulkDiscount").html('(Bulk Discount available)');
                            var qytDiscount = '';
                            $.each(msg.d, function(index, item) {
                                qytDiscount += "<tr><td>" + item.Quantity + "</td><td><span class='cssClassFormatCurrency'>" + item.Price + "</span></td></tr>";
                            });
                            $("#itemQtyDiscount>tbody").append(qytDiscount);
                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                            $("#itemQtyDiscount > tbody tr:even").addClass("cssClassAlternativeEven");
                            $("#itemQtyDiscount > tbody tr:odd").addClass("cssClassAlternativeOdd");
                        } else {
                            $("#bulkDiscount").hide();
                            $("#divQtyDiscount").hide();
                        }
                        break;
                    case 3:
                        if (msg.d.length > 0) {
                            var CostVariant = '';
                            $.each(msg.d, function(index, item) {
                                if (CostVariant.indexOf(item.CostVariantID) == -1) {
                                    CostVariant += item.CostVariantID;
                                    var addSpan = '';
                                    addSpan += '<div id="div_' + item.CostVariantID + '" class="cssClassHalfColumn">';
                                    addSpan += '<span id="spn_' + item.CostVariantID + '" ><b>' + item.CostVariantName + '</b>: ' + '</span>';
                                    addSpan += '</div>';
                                    $('#divCostVariant').append(addSpan);
                                }
                                var valueID = '';
                                var itemCostValueName = '';
                                if (item.CostVariantsValueID != -1) {
                                    if (item.InputTypeID == 5 || item.InputTypeID == 6) {
                                        if ($('#controlCostVariant_' + item.CostVariantID + '').length == 0) {
                                            itemCostValueName += '<div class="cssClassDropDown" id="subDiv' + item.CostVariantID + '">'
                                            valueID = 'controlCostVariant_' + item.CostVariantID;
                                            itemCostValueName += ItemDetail.CreateControl(item, valueID, false);

                                            itemCostValueName += "</div>";
                                            $('#div_' + item.CostVariantID + '').append(itemCostValueName);
                                        }
                                        //Blue (+10%)
                                        //Red (+$10.00)

                                        optionValues = ItemDetail.BindInsideControl(item, valueID);
                                        // alert('#controlCostVariant_' + item.CostVariantID + '');
                                        $('#controlCostVariant_' + item.CostVariantID + '').append(optionValues);
                                        $('#controlCostVariant_' + item.CostVariantID + ' option:first-child').attr("selected", "selected");
                                    } else {
                                        if ($('#subDiv' + item.CostVariantID + '').length == 0) {
                                            itemCostValueName += '<div class="cssClassRadio" id="subDiv' + item.CostVariantID + '">'
                                            valueID = 'controlCostVariant_' + item.CostVariantID;
                                            itemCostValueName += ItemDetail.CreateControl(item, valueID, true);
                                            itemCostValueName += "</div>";
                                            $('#div_' + item.CostVariantID + '').append(itemCostValueName);
                                        } else {
                                            valueID = 'controlCostVariant_' + item.CostVariantID;
                                            itemCostValueName += ItemDetail.CreateControl(item, valueID, false);
                                            $('#subDiv' + item.CostVariantID + '').append(itemCostValueName);
                                        }
                                    }
                                }
                            });
                            $('#divCostVariant').append('<div class="cssClassClear"></div>');

                            if ($.session("ItemCostVariantData") != undefined) {
                                $.each(arrCostVariants, function(i, variant) {
                                    var itemColl = $("#divCostVariant").find("[Variantname=" + variant + "]");
                                    //alert($("#divCostVariant").html());
                                    // alert($(itemColl)
                                    if ($(itemColl).is("input[type='checkbox'] ,input[type='radio']")) {
                                        $("#divCostVariant").find("input:checkbox").removeAttr("checked");
                                        $(itemColl).attr("checked", "checked");
                                    } else if ($(itemColl).is('select>option')) {
                                        $("#divCostVariant").find("select>option").removeAttr("selected");
                                        $(itemColl).attr("selected", "selected");
                                    }

                                });
                                $.session("ItemCostVariantData", 'empty');
                            }
                            //to bind the item price according to the selection of the cost variant
                            $('#divCostVariant select,input[type=radio],input[type=checkbox]').unbind().bind("change", function() {
                                ItemDetail.ResetGallery();
                                var weightWithVariant1 = 0;
                                var priceWithVariant = 0;
                                $("#divCostVariant select option:selected").each(function() {
                                    if ($(this).attr('variantvalue') != undefined) {
                                        priceWithVariant += $(this).attr('variantvalue');
                                    }
                                    if ($(this).attr('variantwtvalue') != undefined) {
                                        weightWithVariant1 += $(this).attr('variantwtvalue');
                                    }
                                });

                                $("#divCostVariant input[type=radio]:checked").each(function() {
                                    if ($(this).attr('variantvalue') != undefined) {
                                        priceWithVariant += $(this).attr('variantvalue');
                                    }
                                    if ($(this).attr('variantwtvalue') != undefined) {
                                        weightWithVariant1 += $(this).attr('variantwtvalue');
                                    }
                                });

                                $("#divCostVariant input[type=checkbox]:checked").each(function() {
                                    if ($(this).attr('variantvalue') != undefined) {
                                        priceWithVariant += $(this).attr('variantvalue');
                                    }
                                    if ($(this).attr('variantwtvalue') != undefined) {
                                        weightWithVariant1 += $(this).attr('variantwtvalue');
                                    }
                                });
                                $("#spanPrice").html(eval($("#hdnPrice").val()) + eval(priceWithVariant));
                                var taxPriceVariant = eval($("#hdnPrice").val()) + eval(priceWithVariant);
                                var taxrate = (eval($("#hdnTaxRateValue").val()) * 100) / (eval($("#hdnPrice").val()));
                                $("#spanTax").html((taxPriceVariant * taxrate) / 100);
                                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                                //for saving percent added part with hdnListPrice added
                                var variantAddedPrice = eval($("#hdnPrice").val()) + eval(priceWithVariant);

                                var variantAddedSavingPercent = (($("#hdnListPrice").val() - variantAddedPrice) / $("#hdnListPrice").val()) * 100;
                                savingPercent2 = variantAddedSavingPercent.toFixed(2);
                                $("#spanSaving").html('<b>' + variantAddedSavingPercent.toFixed(2) + '%</b>');
                            });
                            //end 
                        }
                        break;
                    case 4:
                        if (msg.d.length > 0) {
                            $(".cssClassAddYourReview").html("Write Your Own Review");
                            $(".cssClassItemRatingBox").addClass('cssClassToolTip');
                            $.each(msg.d, function(index, item) {
                                if (index == 0) {
                                    $(".cssClassTotalReviews").html('Read Reviews [' + item.TotalReviewsCount + '] ');
                                    ItemDetail.BindStarRating(item.TotalRatingAverage);
                                }
                                ItemDetail.BindViewDetailsRatingInfo(item.ItemRatingCriteriaID, item.ItemRatingCriteria, item.RatingCriteriaAverage);
                            });
                            $('input.star').rating();
                        } else {
                            var avgRating = "<tr><td>Currently there are no reviews</td></tr>";
                            $("#tblAverageRating").append(avgRating);
                            $(".cssClassItemRatingBox").removeClass('cssClassToolTip');

                            $(".cssClassSeparator").hide();
                            $(".cssClassAddYourReview").html("Be the first to review this item.");
                        }
                        break;
                    case 5:
                        arrItemDetailsReviewList.length = 0;
                        arrItemReviewList.length = 0;
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                                ItemDetail.BindItemsRatingByUser(item, index);
                            });
                            // Create pagination element with options from form
                            var optInit = ItemDetail.getOptionsFromForm();
                            $("#Pagination").pagination(arrItemReviewList.length, optInit);
                            $("#divSearchPageNumber").show();
                        } else {
                            $("#divSearchPageNumber").hide();
                            //alert("No user rating is found!");
                            var avgRating = "<tr><td>Currently no rating and reviews are available.</td></tr>";
                            $("#tblRatingPerUser").append(avgRating);
                        }
                        break;
                    case 6:
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                                ItemDetail.RatingCriteria(item);
                            });
                        } else {
                            csscody.alert("<h2>Information Alert</h2><p>No rating criteria are found!</p>");
                        }
                        break;
                    case 7:
                        csscody.info("<h2>Information Message</h2><p>Your review has been accepted for moderation.</p>");
                        $('#fade, #popuprel2').fadeOut();

                        break;
                    case 8:
                        $('#controlload').html(msg.d);
                        break;
                    case 9:
                        if (msg.d != null) {
                            ItemDetail.BindItemsBasicInfo(msg.d);
                            ItemDetail.GetCostVariantsByitemSKU(itemSKU);
                            //BindCostVariantOptions(itemSKU);
                            $('.popupEmailAFriend').attr('imagepath', msg.d.ImagePath);
                            //This adds Recently View table and also update item's viewed count table
                            ItemDetail.AddUpdateRecentlyViewedItem(itemSKU);

                            ItemDetail.GetYouMayAlsoLikeItemsList();
                        }
                        break;
                    case 10:
                        var attributeSetId = 0;
                        var itemTypeId = 0;
                        $.each(msg.d, function(index, item) {
                            if (index == 0) {
                                attributeSetId = item.AttributeSetID;
                                itemTypeId = item.ItemTypeID;
                            }
                        });
                        ItemDetail.CreateForm(msg.d, attributeSetId, itemTypeId, ItemDetail.vars.itemSKU);
                        if (ItemDetail.vars.itemSKU.length > 0) {
                            ItemDetail.BindDataInTab(ItemDetail.vars.itemSKU, attributeSetId, itemTypeId);
                            ItemDetail.BindRatingReviewTab();
                        }
                        break;
                    case 11:
                        $.each(msg.d, function(index, item) {
                            ItemDetail.FillItemAttributes(itemSKU, item);
                        });
                        ItemDetail.GetItemTags();
                        //BindDownloadUpEvent();
                        break;
                    case 12:
                        $.each(msg.d, function(index, item) {
                            ItemDetail.BindItemTags(item, index);
                        });
                        $("#divItemTags").html(ItemTags.substring(0, ItemTags.length - 2));
                        $("#divMyTags").html(MyTags.substring(0, MyTags.length - 2));

                        break;
                    case 13:
                        ItemDetail.GetItemTags();
                        break;
                    case 14:
                        ItemDetail.GetItemTags();
                        ItemDetail.ClearTableContentTags(this);
                        csscody.info("<h2>Information Message</h2><p>your tag(s) has been accepted for moderation.</p>");

                        break;
                    case 15:
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                                ItemDetail.BindYouMayAlsoLikeItems(index, item);
                            });
                            RelatedItems += "<div class=\"cssClassClear\"></div>";
                        } else {
                            RelatedItems += "<span class=\"cssClassNotFound\">No items listed yet.</span>";
                        }
                        $("#divYouMayAlsoLike").html(RelatedItems);
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                        break;
                    case 16:
                        ItemDetail.vars.itemQuantityInCart = msg.d;
                        break;
                    case 17:
                        ItemDetail.GetFilePath(msg);
                        ItemDetail.Gallery();
                        ItemDetail.ImageZoom();
                        break;
                    case 18:
                        ItemDetail.SetValueForStyle(msg);
                        break;
                    case 20:
                        var myCartUrl;
                        if (userFriendlyURL) {
                            myCartUrl = myCartURL + '.aspx';
                        } else {
                            myCartUrl = myCartURL;
                        }
                        var addToCartProperties = {
                            onComplete: function(e) {
                                if (e) {
                                    //alert('welcome to checkout');
                                    // alert(AspxCommerce.utils.GetAspxRedirectPath());
                                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + myCartURL + '.aspx';
                                }
                            }
                        }
                        csscody.addToCart('<h2>Successful Message</h2><p>Item has been successfully added to cart.</p>', addToCartProperties);
                        HeaderControl.GetCartItemTotalCount(); //for header cart count from database
                        ShopingBag.GetCartItemCount(); //for bag count
                        ShopingBag.GetCartItemListDetails(); //for shopping bag detail
                        break;
                    case 21:
                        ItemDetail.vars.existReviewByUser = msg.d;
                        break;
                    case 22:
                        ItemDetail.vars.existReviewByIP = msg.d;
                        break;
                }
            },
            oncomplete: function() {
                switch (ItemDetail.config.oncomplete) {
                    case 20:
                        ItemDetail.config.oncomplete = 0;
                        if ($("#divCartDetails").length > 0) {
                            AspxCart.GetUserCartDetails(); //for binding mycart's tblCartList
                        }
                        if ($("#dynItemDetailsForm").length > 0) {
                            ItemDetail.BindItemBasicByitemSKU(itemSKU);
                        }
                        break;
                }
            },
            ajaxFailure: function() {
                switch (ItemDetail.config.error) {
                    case 1:
                        break;
                    case 3:
                        csscody.error('<h2>Error Message</h2><p>Failed to load cost variants!</p>');
                        break;
                    case 7:
                        csscody.error('<h2>Error Message</h2><p>Failed to save!</p>');
                        break;
                    case 12:
                        csscody.error('<h2>Error Message</h2><p>Failed to load item tags!</p>');
                        break;
                    case 14:
                        csscody.error('<h2>Error Message</h2><p>Failed to save tags!</p>');
                        break;
                    case 20:
                        csscody.error('<h2>Information Alert</h2><p>Failed to add item to cart!</p>');
                        break;
                }
            },
            vars: {
                countCompareItems: "",
                itemSKU: itemSKU,
                itemQuantityInCart: "",
                userEmail: userEmail,
                itemId: itemID,
                existReviewByUser: "",
                existReviewByIP: "",
                userFullName: userFullName
            },
            GetCompareItemsCount: function() {
                var param = { storeID: storeId, portalID: portalId, sessionCode: sessionCode, userName: userName };
                var Data = JSON2.stringify(param);
                this.config.method = "AspxCommerceWebService.asmx/GetCompareItemsCount";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = Data;
                this.config.ajaxCallMode = 1;
                this.config.error = 1;
                this.ajaxCall(this.config);
                return ItemDetail.vars.countCompareItems;
            },
            BindDownloadEvent: function() {
                $(".cssClassLink").jDownload({
                    root: aspxFilePath,
                    dialogTitle: 'AspxCommerce download sample item:'
                });
            },
            BindItemQuantityDiscountByUserName: function(itemSKU) {
                var param = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, itemSKU: itemSKU });
                this.config.method = "AspxCommerceWebService.asmx/GetItemQuantityDiscountByUserName";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = 2;
                this.config.error = 2;
                this.ajaxCall(this.config);

            },
            GetCostVariantsByitemSKU: function(itemSKU) {
                $('#divCostVariant').html('');
                var param = JSON2.stringify({ storeID: storeId, portalID: portalId, cultureName: cultureName, userName: userName, itemSKU: itemSKU });
                this.config.method = "AspxCommerceWebService.asmx/GetCostVariantsByitemSKU";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = 3;
                this.config.error = 3;
                this.ajaxCall(this.config);
            },
            CreateControl: function(item, controlID, isChecked) {
                var controlElement = '';
                var costPriceValue = item.CostVariantsPriceValue;
                var weightValue = item.CostVariantsWeightValue;
                //alert(costPriceValue + '::' + weightValue + '::' + $("#hdnWeight").val());
                if (item.InputTypeID == 5) { //MultipleSelect
                    //if (isChecked) {
                    controlElement = "<select id='" + controlID + "' multiple></select>";
                    //}
                    //else {
                    //    controlElement = "<select id='" + controlID + "' multiple></select>";
                    //}
                } else if (item.InputTypeID == 6) { //DropDown
                    controlElement = "<select id='" + controlID + "'></select>";
                } else if (item.InputTypeID == 9 || item.InputTypeID == 10) { //Radio //RadioLists
                    if (costPriceValue != '' || costPriceValue != 0) {
                        if (costPriceValue >= 0) {
                            costPriceValue = '+' + costPriceValue;
                        }

                        if (weightValue >= 0) {
                            weightValue = '+' + weightValue;
                        }
                        if (item.IsPriceInPercentage) {
                            if (item.IsWeightInPercentage) {
                                if (isChecked) {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='radio' checked='checked' value='" + item.CostVariantsValueID + "' variantvalue='" + '+' + (($("#hdnPrice").val() * costPriceValue) / 100) + "' variantwtvalue='" + '+' + (($("#hdnWeight").val() * weightValue) / 100) + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + '%)' + "</label>";
                                } else {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='radio' value='" + item.CostVariantsValueID + "'  variantvalue='" + '+' + (($("#hdnPrice").val() * costPriceValue) / 100) + "' variantwtvalue='" + '+' + (($("#hdnWeight").val() * weightValue) / 100) + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + '%)' + "</label>";
                                }
                            } else {
                                if (isChecked) {
                                    controlElement = "<input  VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='radio' checked='checked' value='" + item.CostVariantsValueID + "' variantvalue='" + '+' + (($("#hdnPrice").val() * costPriceValue) / 100) + "' variantwtvalue='" + weightValue + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + '%)' + "</label>";
                                } else {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='radio' value='" + item.CostVariantsValueID + "'  variantvalue='" + '+' + (($("#hdnPrice").val() * costPriceValue) / 100) + "' variantwtvalue='" + weightValue + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + '%)' + "</label>";
                                }
                            }
                        } else {
                            if (item.IsWeightInPercentage) {
                                if (isChecked) {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='radio' checked='checked' value='" + item.CostVariantsValueID + "'  variantvalue='" + costPriceValue + "' variantwtvalue='" + '+' + (($("#hdnWeight").val() * weightValue) / 100) + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + ')' + "</label>";
                                } else {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='radio' value='" + item.CostVariantsValueID + "'  variantvalue='" + costPriceValue + "' variantwtvalue='" + '+' + (($("#hdnWeight").val() * weightValue) / 100) + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + ')' + "</label>";
                                }
                            } else {
                                if (isChecked) {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='radio' checked='checked' value='" + item.CostVariantsValueID + "'  variantvalue='" + costPriceValue + "' variantwtvalue='" + weightValue + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + ')' + "</label>";
                                } else {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='radio' value='" + item.CostVariantsValueID + "'  variantvalue='" + costPriceValue + "' variantwtvalue='" + weightValue + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + ')' + "</label>";
                                }
                            }
                        }
                    } else {
                        if (isChecked) {
                            controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='radio' checked='checked' value='" + item.CostVariantsValueID + "'><label>" + item.CostVariantsValueName + "</label>";
                        } else {
                            controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='radio' value='" + item.CostVariantsValueID + "'><label>" + item.CostVariantsValueName + "</label>";
                        }
                    }
                } else if (item.InputTypeID == 11 || item.InputTypeID == 12) { //CheckBox //CheckBoxLists
                    if (costPriceValue != '' || costPriceValue != 0) {
                        if (costPriceValue >= 0) {
                            costPriceValue = '+' + costPriceValue;
                        }
                        if (weightValue >= 0) {
                            weightValue = '+' + weightValue;
                        }
                        if (item.IsPriceInPercentage) {
                            if (item.IsWeightInPercentage) {
                                if (isChecked) {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='checkbox' checked='checked' value='" + item.CostVariantsValueID + "' variantvalue='" + '+' + (($("#hdnPrice").val() * costPriceValue) / 100) + "' variantwtvalue='" + '+' + (($("#hdnWeight").val() * weightValue) / 100) + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + '%)' + "</label>";
                                } else {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='checkbox' value='" + item.CostVariantsValueID + "'  variantvalue='" + '+' + (($("#hdnPrice").val() * costPriceValue) / 100) + "' variantwtvalue='" + '+' + (($("#hdnWeight").val() * weightValue) / 100) + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + '%)' + "</label>";
                                }
                            } else {
                                if (isChecked) {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='checkbox' checked='checked' value='" + item.CostVariantsValueID + "' variantvalue='" + '+' + (($("#hdnPrice").val() * costPriceValue) / 100) + "' variantwtvalue='" + weightValue + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + '%)' + "</label>";
                                } else {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='checkbox' value='" + item.CostVariantsValueID + "'  variantvalue='" + '+' + (($("#hdnPrice").val() * costPriceValue) / 100) + "' variantwtvalue='" + weightValue + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + '%)' + "</label>";
                                }
                            }
                        } else {
                            if (item.IsWeightInPercentage) {
                                if (isChecked) {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='checkbox' checked='checked' value='" + item.CostVariantsValueID + "'  variantvalue='" + costPriceValue + "' variantwtvalue='" + '+' + (($("#hdnWeight").val() * weightValue) / 100) + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + ')' + "</label>";
                                } else {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='checkbox' value='" + item.CostVariantsValueID + "'  variantvalue='" + costPriceValue + "' variantwtvalue='" + '+' + (($("#hdnWeight").val() * weightValue) / 100) + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + ')' + "</label>";
                                }
                            } else {
                                if (isChecked) {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='checkbox' checked='checked' value='" + item.CostVariantsValueID + "'  variantvalue='" + costPriceValue + "' variantwtvalue='" + weightValue + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + ')' + "</label>";
                                } else {
                                    controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='checkbox' value='" + item.CostVariantsValueID + "'  variantvalue='" + costPriceValue + "' variantwtvalue='" + weightValue + "'><label>" + item.CostVariantsValueName + ' (' + costPriceValue + ')' + "</label>";
                                }
                            }
                        }
                    } else {
                        if (isChecked) {
                            controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='checkbox' checked='checked' value='" + item.CostVariantsValueID + "'><label>" + item.CostVariantsValueName + "</label>";
                        } else {
                            controlElement = "<input VariantName='" + item.CostVariantsValueName + "' name='" + controlID + "' type='checkbox' value='" + item.CostVariantsValueID + "'><label>" + item.CostVariantsValueName + "</label>";
                        }
                    }
                }
                return controlElement;
            },
            BindInsideControl: function(item, controlID) {
                var optionValues = '';
                var costPriceValue = item.CostVariantsPriceValue;
                var weightValue = item.CostVariantsWeightValue;
                if (item.InputTypeID == 5) { //MultipleSelect 
                    if (costPriceValue != '') {
                        if (costPriceValue >= 0) {
                            costPriceValue = '+' + costPriceValue;
                        }
                        if (weightValue >= 0) {
                            weightValue = '+' + weightValue;
                        }
                        if (item.IsPriceInPercentage) {
                            if (item.IsWeightInPercentage) {

                                optionValues = "<option VariantName='" + item.CostVariantsValueName + "' value=" + item.CostVariantsValueID + " variantvalue='" + '+' + (($("#hdnPrice").val() * costPriceValue) / 100) + "' variantwtvalue='" + '+' + (($("#hdnWeight").val() * weightValue) / 100) + "'>" + item.CostVariantsValueName + ' (' + costPriceValue + '%)' + "</option>";

                            } else {

                                optionValues = "<option VariantName='" + item.CostVariantsValueName + "' value=" + item.CostVariantsValueID + " variantvalue='" + '+' + (($("#hdnPrice").val() * costPriceValue) / 100) + "' variantwtvalue=" + weightValue + ">" + item.CostVariantsValueName + ' (' + costPriceValue + '%)' + "</option>";
                            }
                        } else {
                            if (item.IsWeightInPercentage) {

                                optionValues = "<option VariantName='" + item.CostVariantsValueName + "' value=" + item.CostVariantsValueID + " variantvalue=" + costPriceValue + " variantwtvalue='" + '+' + (($("#hdnWeight").val() * weightValue) / 100) + "'>" + item.CostVariantsValueName + ' (' + costPriceValue + ')' + "</option>";

                            } else {

                                optionValues = "<option VariantName='" + item.CostVariantsValueName + "' value=" + item.CostVariantsValueID + " variantvalue=" + costPriceValue + " variantwtvalue=" + weightValue + ">" + item.CostVariantsValueName + ' (' + costPriceValue + ')' + "</option>";
                            }
                        }
                    } else {

                        optionValues = "<option VariantName='" + item.CostVariantsValueName + "' value=" + item.CostVariantsValueID + ">" + item.CostVariantsValueName + "</option>";
                    }
                } else if (item.InputTypeID == 6) { //DropDown
                    if (costPriceValue != '') {
                        if (costPriceValue >= 0) {
                            costPriceValue = '+' + costPriceValue;
                        }
                        if (weightValue >= 0) {
                            weightValue = '+' + weightValue;
                        }
                        if (item.IsPriceInPercentage) {
                            if (item.IsWeightInPercentage) {

                                optionValues = "<option VariantName='" + item.CostVariantsValueName + "' value=" + item.CostVariantsValueID + " variantvalue='" + '+' + (($("#hdnPrice").val() * costPriceValue) / 100) + "' variantwtvalue='" + '+' + (($("#hdnWeight").val() * weightValue) / 100) + "'>" + item.CostVariantsValueName + ' (' + costPriceValue + '%)' + "</option>";

                            } else {

                                optionValues = "<option VariantName='" + item.CostVariantsValueName + "' value=" + item.CostVariantsValueID + " variantvalue='" + '+' + (($("#hdnPrice").val() * costPriceValue) / 100) + "' variantwtvalue=" + weightValue + ">" + item.CostVariantsValueName + ' (' + costPriceValue + '%)' + "</option>";
                            }
                        } else {
                            if (item.IsWeightInPercentage) {

                                optionValues = "<option VariantName='" + item.CostVariantsValueName + "' value=" + item.CostVariantsValueID + " variantvalue=" + costPriceValue + " variantwtvalue='" + '+' + (($("#hdnWeight").val() * weightValue) / 100) + "'>" + item.CostVariantsValueName + ' (' + costPriceValue + ')' + "</option>";
                            } else {

                                optionValues = "<option VariantName='" + item.CostVariantsValueName + "' value=" + item.CostVariantsValueID + " variantvalue=" + costPriceValue + " variantwtvalue=" + weightValue + ">" + item.CostVariantsValueName + ' (' + costPriceValue + ')' + "</option>";
                            }
                        }
                    } else {

                        optionValues = "<option VariantName='" + item.CostVariantsValueName + "'  value=" + item.CostVariantsValueID + ">" + item.CostVariantsValueName + "</option>";
                    }
                }
                return optionValues;
            },
            BindRatingReviewTab: function() {
                $("#tblRatingPerUser").html('');
                ItemDetail.GetItemRatingPerUser();
            },
            BindItemAverageRating: function() {
                var param = JSON2.stringify({ itemSKU: itemSKU, storeID: storeId, portalID: portalId, cultureName: cultureName });
                this.config.method = "AspxCommerceWebService.asmx/GetItemAverageRating";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = 4;
                this.config.error = 4;
                this.ajaxCall(this.config);
            },
            CheckReviewByUser: function(userName) {
                var param = JSON2.stringify({ itemID: itemId, storeID: storeId, portalID: portalId, userName: userName });
                this.config.method = "AspxCommerceWebService.asmx/CheckReviewByUser";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = 21;
                this.ajaxCall(this.config);
            },

            CheckReviewByIP: function(userIP) {
                var param = JSON2.stringify({ itemID: itemId, storeID: storeId, portalID: portalId, userIP: userIP });
                this.config.method = "AspxCommerceWebService.asmx/CheckReviewByIP";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = 22;
                this.ajaxCall(this.config);
            },

            BindStarRating: function(itemAvgRating) {
                var ratingStars = '';
                var ratingTitle = ["Worst", "Ugly", "Bad", "Not Bad", "Average", "OK", "Nice", "Good", "Best", "Excellent"]; //To do here tooltip for each half star
                var ratingText = ["0.5", "1", "1.5", "2", "2.5", "3", "3.5", "4", "4.5", "5"];
                var i = 0;
                ratingStars += '<tr><td>';
                for (i = 0; i < 10; i++) {
                    if (itemAvgRating == ratingText[i]) {
                        ratingStars += '<input name="avgItemRating" type="radio" class="star {split:2}" disabled="disabled" checked="checked" value="' + ratingTitle[i] + '" />';
                        $(".cssClassRatingTitle").html(ratingTitle[i]);
                    } else {
                        ratingStars += '<input name="avgItemRating" type="radio" class="star {split:2}" disabled="disabled" value="' + ratingTitle[i] + '" />';
                    }
                }
                ratingStars += '</td></tr>';
                $("#tblAverageRating").append(ratingStars);
            },
            BindViewDetailsRatingInfo: function(itemRatingCriteriaId, itemRatingCriteria, ratingCriteriaAverage) {
                var ratingStarsDetailsInfo = '';
                var ratingTitle = ["Worst", "Ugly", "Bad", "Not Bad", "Average", "OK", "Nice", "Good", "Best", "Excellent"]; //To do here tooltip for each half star
                var ratingText = ["0.5", "1", "1.5", "2", "2.5", "3", "3.5", "4", "4.5", "5"];
                var i = 0;
                ratingStarsDetailsInfo += '<div class="cssClassToolTipDetailInfo">';
                ratingStarsDetailsInfo += '<span class="cssClassCriteriaTitle">' + itemRatingCriteria + ': </span>';
                for (i = 0; i < 10; i++) {
                    if (ratingCriteriaAverage == ratingText[i]) {
                        ratingStarsDetailsInfo += '<input name="avgItemDetailRating' + itemRatingCriteriaId + '" type="radio" class="star {split:2}" disabled="disabled" checked="checked" value="' + ratingTitle[i] + '" />';
                    } else {
                        ratingStarsDetailsInfo += '<input name="avgItemDetailRating' + itemRatingCriteriaId + '" type="radio" class="star {split:2}" disabled="disabled" value="' + ratingTitle[i] + '" />';
                    }
                }
                ratingStarsDetailsInfo += '</div>';
                $("Div.cssClassToolTipInfo").append(ratingStarsDetailsInfo);
            },
            GetItemRatingPerUser: function() {
                ItemsReview = '';
                var param = JSON2.stringify({ itemSKU: itemSKU, storeID: storeId, portalID: portalId, cultureName: cultureName });
                this.config.method = "AspxCommerceWebService.asmx/GetItemRatingPerUser";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = 5;
                this.config.error = 5;
                this.ajaxCall(this.config);
            },
            BindItemsRatingByUser: function(item, index) {
                arrItemDetailsReviewList.push(item);

                if (ItemsReview.indexOf(item.ItemReviewID) == -1) {
                    ItemsReview += item.ItemReviewID;

                    arrItemReviewList.push(item);
                }
            },
            BindAverageUserRating: function(item) {
                var userRatings = '';
                userRatings += '<tr><td><div class="cssClassRateReview"><div class="cssClassItemRating">';
                userRatings += '<div class="cssClassItemRatingBox">' + ItemDetail.BindStarRatingAveragePerUser(item.ItemReviewID, item.RatingAverage) + '</div>';


                userRatings += '<div class="cssClassRatingInfo"><p><span> Review by <strong>' + item.Username + '</strong></span></p><p class="cssClassRatingReviewDate"> (Posted on <strong>' + formatDate(new Date(item.AddedOn), "yyyy/M/d hh:mm:ssa") + '</strong>)</p></div></div>';

                userRatings += '<div class="cssClassRatingdesc"><p>' + Encoder.htmlDecode(item.ReviewSummary) + '</p><p class="cssClassRatingReviewDesc">' + Encoder.htmlDecode(item.Review) + '</p></div>';

                userRatings += '</div></td></tr>';
                $("#tblRatingPerUser").append(userRatings);
                var ratingToolTip = $("#hdnRatingTitle" + item.ItemReviewID + "").val();
                $(".cssClassUserRatingTitle_" + item.ItemReviewID + "").html(ratingToolTip);
            },
            BindStarRatingAveragePerUser: function(itemReviewID, itemAvgRating) {
                var ratingStars = '';
                var ratingTitle = ["Worst", "Ugly", "Bad", "Not Bad", "Average", "OK", "Nice", "Good", "Best", "Excellent"]; //To do here tooltip for each half star
                var ratingText = ["0.5", "1", "1.5", "2", "2.5", "3", "3.5", "4", "4.5", "5"];
                var i = 0;
                var ratingTitleText = '';
                ratingStars += '<div class="cssClassRatingStar"><div class="cssClassToolTip">';
                ratingStars += '<span class="cssClassRatingTitle2 cssClassUserRatingTitle_' + itemReviewID + '"></span>';
                for (i = 0; i < 10; i++) {
                    if (itemAvgRating == ratingText[i]) {
                        ratingStars += '<input name="avgRatePerUser' + itemReviewID + '" type="radio" class="star-rate {split:2}" disabled="disabled" checked="checked" value="' + ratingTitle[i] + '" />';
                        ratingTitleText = ratingTitle[i];
                    } else {
                        ratingStars += '<input name="avgRatePerUser' + itemReviewID + '" type="radio" class="star-rate {split:2}" disabled="disabled" value="' + ratingTitle[i] + '" />';
                    }
                }
                ratingStars += '<input type="hidden" value="' + ratingTitleText + '" id="hdnRatingTitle' + itemReviewID + '"></input><span class="cssClassToolTipInfo cssClassReviewId_' + itemReviewID + '"></span></div></div><div class="cssClassClear"></div>';
                return ratingStars;
            },
            BindPerUserIndividualRatings: function(itemReviewID, itemRatingCriteria, ratingValue) {
                var userRatingStarsDetailsInfo = '';
                var ratingTitle = ["Worst", "Ugly", "Bad", "Not Bad", "Average", "OK", "Nice", "Good", "Best", "Excellent"]; //To do here tooltip for each half star
                var ratingText = ["0.5", "1", "1.5", "2", "2.5", "3", "3.5", "4", "4.5", "5"];
                var i = 0;
                userRatingStarsDetailsInfo += '<div class="cssClassToolTipDetailInfo">';
                userRatingStarsDetailsInfo += '<span class="cssClassCriteriaTitle">' + itemRatingCriteria + ': </span>';
                for (i = 0; i < 10; i++) {
                    if (ratingValue == ratingText[i]) {
                        userRatingStarsDetailsInfo += '<input name="avgUserDetailRate' + itemRatingCriteria + '_' + itemReviewID + '" type="radio" class="star-rate {split:2}" disabled="disabled" checked="checked" value="' + ratingTitle[i] + '" />';
                    } else {
                        userRatingStarsDetailsInfo += '<input name="avgUserDetailRate' + itemRatingCriteria + '_' + itemReviewID + '" type="radio" class="star-rate {split:2}" disabled="disabled" value="' + ratingTitle[i] + '" />';
                    }
                }
                userRatingStarsDetailsInfo += '</div>';
                $('#tblRatingPerUser span.cssClassReviewId_' + itemReviewID + '').append(userRatingStarsDetailsInfo);
            },
            BindPopUp: function() {
                ItemDetail.ClearReviewForm();
                $("#lblYourReviewing").html('You\'re Reviewing: ' + itemName + '');
                if (userName.toLowerCase() != "anonymouseuser") {
                    $("#txtUserName").val(userName);
                }
                $.metadata.setType("attr", "validate");
                $('.auto-submit-star').rating({
                    required: false,
                    focus: function(value, link) {
                        var ratingCriteria_id = $(this).attr("name").replace(/[^0-9]/gi, '');
                        var tip = $('#hover-test' + ratingCriteria_id);
                        tip[0].data = tip[0].data || tip.html();
                        tip.html(link.title || 'value: ' + value);
                        $("#tblRatingCriteria label.error").hide();
                    },
                    blur: function(value, link) {
                        var ratingCriteria_id = $(this).attr("name").replace(/[^0-9]/gi, '');
                        var tip = $('#hover-test' + ratingCriteria_id);
                        tip.html('<span class="cssClassToolTip">' + tip[0].data || '' + '</span>');
                        $("#tblRatingCriteria label.error").hide();
                    },

                    callback: function(value, event) {
                        var ratingCriteria_id = $(this).attr("name").replace(/[^0-9]/gi, '');
                        var starRatingValues = $(this).attr("value");
                        var len = ratingCriteria_id.length;
                        var isAppend = true;
                        if (ratingValues != '') {
                            var stringSplit = ratingValues.split('#');
                            $.each(stringSplit, function(index, item) {
                                if (item.substring(0, item.indexOf('-')) == ratingCriteria_id) {
                                    var index = ratingValues.indexOf(ratingCriteria_id + "-");
                                    var toReplace = ratingValues.substr(index, 2 + len);
                                    ratingValues = ratingValues.replace(toReplace, ratingCriteria_id + "-" + value);
                                    isAppend = false;
                                }
                            });
                            if (isAppend) {
                                ratingValues += ratingCriteria_id + "-" + starRatingValues + "#" + '';
                            }
                        } else {
                            ratingValues += ratingCriteria_id + "-" + starRatingValues + "#" + '';
                        }
                    }
                });
            },
            BindRatingCriteria: function() {
                var param = JSON2.stringify({ storeID: storeId, portalID: portalId, cultureName: cultureName, isFlag: false });
                this.config.method = "AspxCommerceWebService.asmx/GetItemRatingCriteria";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = 6;
                this.config.error = 6;
                this.ajaxCall(this.config);
            },
            RatingCriteria: function(item) {
                var ratingCriteria = '';
                ratingCriteria += '<tr><td class="cssClassReviewCriteria"><label class="cssClassLabel">' + item.ItemRatingCriteria + ':<span class="cssClassRequired">*</span></label></td><td>';
                ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star" value="1" title="Worst" validate="required:true" />';
                ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star" value="2" title="Bad" />';
                ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star" value="3" title="OK" />';
                ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star" value="4" title="Good" />';
                ratingCriteria += '<input name="star' + item.ItemRatingCriteriaID + '" type="radio" class="auto-submit-star" value="5" title="Best" />';
                ratingCriteria += '<span id="hover-test' + item.ItemRatingCriteriaID + '" class="cssClassRatingText"></span>';
                ratingCriteria += '<label for="star' + item.ItemRatingCriteriaID + '" class="error">Please rate for ' + item.ItemRatingCriteria + '</label></td></tr>';
                $("#tblRatingCriteria").append(ratingCriteria);
            },
            ClearReviewForm: function() {
                //Clear all Stars checked      
                $('.auto-submit-star').rating('drain');
                $('.auto-submit-star').removeAttr('checked');
                $('.auto-submit-star').rating('select', -1);

                $("#txtUserName").val('');
                $("#txtSummaryReview").val('');
                $("#txtReview").val('');
                $("label.error").hide();
            },
            SaveItemRatings: function() {
                var statusId = 2;
                var ratingValue = ratingValues;
                var nickName = $("#txtUserName").val();
                var summaryReview = $("#txtSummaryReview").val();
                var review = $("#txtReview").val();
                var param = JSON2.stringify({ ratingCriteriaValue: ratingValue, statusID: statusId, summaryReview: summaryReview, review: review, userIP: userIP, viewFromCountry: countryName, itemID: itemId, storeID: storeId, portalID: portalId, nickName: nickName, addedBy: userName });

                this.config.method = "AspxCommerceWebService.asmx/SaveItemRating";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = 7;
                this.config.error = 7;
                this.ajaxCall(this.config);
            },
            ShowUsingPage: function() {
                $.metadata.setType("attr", "validate");
                var ControlName = "Modules/AspxCommerce/AspxReferToFriend/ReferAFriend.ascx";
                //var ControlName = rootPath + "Modules/AspxCommerce/AspxReferToFriend/ReferAFriend.ascx";
                this.config.method = "LoadControlHandler.aspx/Result";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = "{ controlName:'" + AspxCommerce.utils.GetAspxRootPath() + ControlName + "'}";
                this.config.ajaxCallMode = 8;
                this.config.error = 8;
                this.ajaxCall(this.config);

            },
            BindItemBasicByitemSKU: function(itemSKU) {
                var checkparam = { itemSKU: itemSKU, storeID: storeId, portalID: portalId, userName: userName, culture: cultureName };
                var checkdata = JSON2.stringify(checkparam);
                this.config.method = "AspxCommerceWebService.asmx/GetItemBasicInfoByitemSKU";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = checkdata;
                this.config.ajaxCallMode = 9;
                this.config.error = 9;
                this.ajaxCall(this.config);

            },
            GetFormFieldList: function(itemSKU) {
                ItemDetail.vars.itemSKU = itemSKU;
                this.config.method = "AspxCommerceWebService.asmx/GetItemFormAttributesByitemSKUOnly";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ itemSKU: itemSKU, storeID: storeId, portalID: portalId, userName: userName, culture: cultureName });
                this.config.ajaxCallMode = 10;
                this.config.error = 10;
                this.ajaxCall(this.config);

            },
            CreateForm: function(itemFormFields, attributeSetId, itemTypeId, itemSKU) {
                var strDyn = '';
                var attGroup = new Array();
                $.each(itemFormFields, function(index, item) {
                    var isGroupExist = false;
                    for (var i = 0; i < attGroup.length; i++) {
                        if (attGroup[i].key == item.GroupID) {
                            isGroupExist = true;
                            break;
                        }
                    }
                    if (!isGroupExist) {
                        attGroup.push({ key: item.GroupID, value: item.GroupName, html: '' });
                    }
                });
                $.each(itemFormFields, function(index, item) {
                    strDynRow = ItemDetail.createRow(itemSKU, item.AttributeID, item.AttributeName, item.InputTypeID, item.InputTypeValues != "" ? eval(item.InputTypeValues) : '', item.DefaultValue, item.ToolTip, item.Length, item.ValidationTypeID, item.IsEnableEditor, item.IsUnique, item.IsRequired, item.GroupID, item.IsIncludeInPriceRule, item.IsIncludeInPromotions, item.DisplayOrder);
                    //strDynRow = '<table width="100%" border="0" cellpadding="0" cellspacing="0">' + strDynRow + '</table>';
                    for (var i = 0; i < attGroup.length; i++) {
                        if (attGroup[i].key == item.GroupID) {
                            attGroup[i].html += strDynRow;
                        }
                    }
                });
                ItemDetail.CreateTabPanel(attGroup, attributeSetId, itemTypeId);
            },
            createRow: function(itemSKU, attID, attName, attType, attTypeValue, attDefVal, attToolTip, attLen, attValType, isEditor, isUnique, isRequired, groupId, isIncludeInPriceRule, isIncludeInPromotions, displayOrder) {
                var retString = '';
                retString += '<tr><td class="cssClassTableLeftCol"><label class="cssClassLabel">' + attName + ': </label></td>';
                retString += '<td><div id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" title="' + attToolTip + '">';
                retString += '</div></td>';
                retString += '</tr>';
                return retString;
            },
            CreateTabPanel: function(attGroup, attributeSetId, itemTypeId) {
                if (FormCount) {
                    FormCount = new Array();
                }
                var FormID = "form_" + (FormCount.length * 10 + Math.floor(Math.random() * 10));
                FormCount[FormCount.length] = FormID;
                var dynHTML = '';
                var itemTabs = '';
                var tabBody = '';
                dynHTML += '<div class="cssClassTabPanelTable">';
                dynHTML += '<div id="ItemDetails_TabContainer" class="cssClassTabpanelContent cssClassTabTopBorder">';
                dynHTML += '<ul>';
                for (var i = 0; i < attGroup.length; i++) {
                    itemTabs += '<li><a href="#ItemTab-' + attGroup[i].key + '"><span>' + attGroup[i].value + '</span></a>';
                    tabBody += '<div id="ItemTab-' + attGroup[i].key + '"><table border="0" cellpadding="0" cellspacing="0">' + attGroup[i].html + '</table></div></li>';
                }
                //Add Static sections here Product Reviews, Product Tags, Customers Tagged Product
                //Tags part Starts HERE
                itemTabs += '<li><a href="#ItemTab-Tags"><span>Tags</span></a>';
                var itemTagsBody = '';
                itemTagsBody += '<div class="cssClassPopularItemTags"><strong>Popular Tags:</strong><div id="divItemTags" class="cssClassPopular-Itemstags"></div>';
                //TOSHow only if user is logged in
                if (customerId > 0 && userName.toLowerCase() != "anonymoususer") {
                    itemTagsBody += '<strong>My Tags:</strong><div id="divMyTags" class="cssClassMyTags"></div>';
                    itemTagsBody += '<table id="AddTagTable"><tr><td>';
                    itemTagsBody += '<input type="text" class="classTag" maxlength="20"/>';
                    itemTagsBody += '<button class="cssClassDecrease" type="button"><span>-</span></button>';
                    itemTagsBody += '<button class="cssClassIncrease" type="button"><span>+</span></button>';
                    itemTagsBody += '</td></tr></table>';
                    itemTagsBody += '<div class="cssClassButtonWrapper"><button type="button" id="btnTagSubmit"><span><span>Add Tag</span></span></button></div></div>';
                    //Else Show Please log in link
                } else {
                    itemTagsBody += '<a href="' + aspxRedirectPath + 'Login.aspx?ReturnUrl=' + aspxRedirectPath + 'item/' + itemSKU + '.aspx" class="cssClassLogIn"><span>Sign in to enter tags</span></a>';
                }
                tabBody += '<div  id="ItemTab-Tags"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>' + itemTagsBody + '</tr></td></table></div></li>';
                //Tags part Ends HERE

                //Review and Rating Starts Here
                itemTabs += '<li><a href="#ItemTab-Reviews"><span>Ratings & Reviews </span></a>';
                tabBody += '<div id="ItemTab-Reviews"><table cellspacing="0" cellpadding="0" width="100%" border="0" id="tblRatingPerUser"></table>';
                //Paging Parts here
                tabBody += '<div class="cssClassPageNumber" id="divSearchPageNumber"><div class="cssClassPageNumberLeftBg"><div class="cssClassPageNumberRightBg"><div class="cssClassPageNumberMidBg">';
                tabBody += '<div id="Pagination"></div><div class="cssClassViewPerPage">View Per Page<select id="ddlPageSize" class="cssClassDropDown">';
                tabBody += '<option value="5">5</option><option value="10">10</option><option value="15">15</option><option value="20">20</option><option value="25">25</option><option value="40">40</option></select></div>';
                tabBody += '</div></div></div></div></li>';
                //Review and Rating Ends Here

                dynHTML += itemTabs;
                dynHTML += '</ul>';
                dynHTML += tabBody;
                var frmIDQuoted = "'" + FormID + "'";
                var buttons = '<div class="cssClassClear"></div>';
                $("#dynItemDetailsForm").html('<div id="' + FormID + '" class="cssClassFormWrapper">' + dynHTML + buttons + '</div>');
                $("#dynItemDetailsForm").find(".cssClassIncrease").click(function() {
                    var cloneRow = $(this).closest('tr').clone(true);
                    $(cloneRow).appendTo("#AddTagTable");
                    $(cloneRow).find("input[type='text']").val('');
                    $(this).remove();
                });

                $("#dynItemDetailsForm").find(".cssClassDecrease").click(function() {
                    var cloneRow = $(this).closest('tr');
                    if (cloneRow.is(":last-child")) {
                        var prevTR = $(cloneRow).prev('tr');
                        var prevTagTitle = prevTR.find("input[type='text']").val();
                        prevTR.remove();
                        $(cloneRow).find("input[type='text']").val(prevTagTitle)
                        return false;
                    } else {
                        $(cloneRow).remove();
                    }
                });

                $("#dynItemDetailsForm").find("#btnTagSubmit").bind("click", function() {
                    ItemDetail.SubmitTag();
                });

                $("#dynItemDetailsForm").find("#ddlPageSize").bind("change", function() {
                    // Create pagination element with options from form
                    var optInit = ItemDetail.getOptionsFromForm();
                    $("#Pagination").pagination(arrItemReviewList.length, optInit);
                });

                var $tabs = $('#ItemDetails_TabContainer').tabs({ fx: [null, { height: 'show', opacity: 'show'}] }); // first tab selected
                $tabs.tabs('select', 0);
            },
            BindDataInTab: function(itemSKU, attributeSetId, itemTypeId) {
                this.config.method = "AspxCommerceWebService.asmx/GetItemDetailsByitemSKU";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ itemSKU: itemSKU, attributeSetID: attributeSetId, itemTypeID: itemTypeId, storeID: storeId, portalID: portalId, userName: userName, culture: cultureName });
                this.config.ajaxCallMode = 11;
                this.config.error = 11;
                this.ajaxCall(this.config);

            },
            GetItemTags: function() {
                ItemTags = '';
                TagNames = '';
                MyTags = '';
                UserTags = '';
                this.config.method = "AspxCommerceWebService.asmx/GetItemTags";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ itemSKU: itemSKU, storeID: storeId, portalID: portalId });
                this.config.ajaxCallMode = 12;
                this.config.error = 12;
                this.ajaxCall(this.config);

            },
            BindItemTags: function(item, index) {
                if (TagNames.indexOf(item.Tag) == -1) {
                    ItemTags += item.Tag + "(" + item.TagCount + "), ";
                    TagNames += item.Tag;
                }

                if (item.AddedBy == userName) {
                    if (UserTags.indexOf(item.Tag) == -1) {
                        MyTags += item.Tag + "<button type=\"button\" class=\"cssClassCross\" value=" + item.ItemTagID + " onclick ='ItemDetail.DeleteMyTag(this)'><span>x</span></button>, ";
                        UserTags += item.Tag;
                    }
                }
            },
            DeleteMyTag: function(obj) {
                var itemTagId = $(obj).attr("value");
                this.config.method = "AspxCommerceWebService.asmx/DeleteUserOwnTag";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ itemTagID: itemTagId, storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 13;
                this.config.error = 13;
                this.ajaxCall(this.config);
            },
            SubmitTag: function() {
                var isValid = false;
                var TagValue = '';
                $(".classTag").each(function() {
                    if ($(this).val() == '') {
                        //  alert('please add tags');
                        $(this).parents('td').find('span[class="err"]').html('');
                        $('<span class="err" style="color:red;">*<span>').insertAfter(this);
                        isValid = false;
                        return false;
                    } else {
                        isValid = true;
                        TagValue += $(this).val() + "#";
                        $(this).siblings('span').remove();
                    }
                });
                if (isValid) {
                    TagValue = TagValue.substring(0, TagValue.length - 1);
                    this.config.method = "AspxCommerceWebService.asmx/AddTagsOfItem";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({ itemSKU: itemSKU, tags: TagValue, storeID: storeId, portalID: portalId, userName: userName });
                    this.config.ajaxCallMode = 14;
                    this.config.error = 14;
                    this.ajaxCall(this.config);
                }
            },
            ClearTableContentTags: function(obj) {
                $('#AddTagTable tr:not(:last-child)').remove();
                $(".classTag").val('');
            },
            FillItemAttributes: function(itemSKU, item) {
                //var attNameNoSpace = "_" + item.AttributeName.replace(new RegExp(" ", "g"), '-');
                var id = item.AttributeID + '_' + item.InputTypeID + '_' + item.ValidationTypeID + '_' + item.IsRequired + '_' + item.GroupID
	    			+ '_' + item.IsIncludeInPriceRule + '_' + item.IsIncludeInPromotions + '_' + item.DisplayOrder;

                var val = '';
                switch (item.InputTypeID) {
                    case 1:
                        //TextField
                        if (item.ValidationTypeID == 3) {
                            $("#" + id).html(item.DecimalValue);
                            break;
                        } else if (item.ValidationTypeID == 5) {
                            $("#" + id).html(item.IntValue);
                            break;
                        } else {
                            $("#" + id).html(unescape(item.NvarcharValue));
                            break;
                        }
                    case 2:
                        //TextArea
                        $("#" + id).html(Encoder.htmlDecode(item.TextValue));
                        break;
                    case 3:
                        //Date
                        $("#" + id).html(formatDate(new Date(item.DateValue), "yyyy/M/d"));
                        break;
                    case 4:
                        //Boolean
                        $("#" + id).html(item.BooleanValue);
                        break;
                    case 5:
                        //MultipleSelect
                        $("#" + id).html(item.OptionValues);
                        break;
                    case 6:
                        //DropDown
                        $("#" + id).html(item.OptionValues);
                        break;
                    case 7:
                        //Price
                        $("#" + id).html(item.DecimalValue);
                        break;
                    case 8:
                        //File    
                        var div = $("#" + id);
                        var filePath = item.FileValue;
                        var fileName = filePath.substring(filePath.lastIndexOf("/") + 1);
                        if (filePath != "") {
                            var fileExt = (-1 !== filePath.indexOf('.')) ? filePath.replace(/.*[.]/, '') : '';
                            myregexp = new RegExp("(jpg|jpeg|jpe|gif|bmp|png|ico)", "i");
                            if (myregexp.test(fileExt)) {
                                $(div).append('<span class="response"><img src="' + aspxRootPath + filePath + '" class="uploadImage" /></span>');
                            } else {

                                $(div).append('<span class="response"><span id="spanFileUpload"  class="cssClassLink"  href="' + 'uploads/' + fileName + '" >' + fileName + '</span></span>');
                            }
                        }
                        break;
                    case 9:
                        //Radio
                        $("#" + id).html(item.OptionValues);
                        break;
                    case 10:
                        //RadioButtonList
                        $("#" + id).html(item.OptionValues);
                        break;
                    case 11:
                        //CheckBox
                        $("#" + id).html(item.OptionValues);
                        break;
                    case 12:
                        //CheckBoxList
                        $("#" + id).html(item.OptionValues);
                        break;
                    case 13:
                        //Password
                        $("#" + id).html(item.NvarcharValue);
                        break;
                }
            },
            GetYouMayAlsoLikeItemsList: function() {
                RelatedItems = '';
                this.config.method = "AspxCommerceWebService.asmx/GetYouMayAlsoLikeItemsListByitemSKU";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ itemSKU: itemSKU, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName, count: relatedItemsCount });
                this.config.ajaxCallMode = 15;
                this.config.async = false;
                this.config.error = 15;
                this.ajaxCall(this.config);

            },
            BindYouMayAlsoLikeItems: function(index, item) {
                $("#divYouMayAlsoLike").html('');
                if (item.ImagePath == "") {
                    item.ImagePath = aspxRootPath + noItemDetailImagePath;
                }
                if (item.AlternateText == "") {
                    item.AlternateText = item.Name;
                }
                if ((index + 1) % 4 == 0) {
                    RelatedItems += "<div class=\"cssClassYouMayAlsoLikeBox cssClassYouMayAlsoLikeBoxFourth\">";
                } else {
                    RelatedItems += "<div class=\"cssClassYouMayAlsoLikeBox\">";
                }
                RelatedItems += '<p class="cssClassProductPicture"><a href="' + aspxRedirectPath + 'item/' + item.SKU + '.aspx"><img  alt="' + item.AlternateText + '" title="' + item.Name + '" src="' + aspxRootPath + item.ImagePath.replace('uploads', 'uploads/Small') + '"></a></p>';
                RelatedItems += '<p class="cssClassProductRealPrice"><span>Price :<span class="cssClassFormatCurrency">' + item.Price + '</span></span></p>';
                if (allowOutStockPurchase.toLowerCase() == 'false') {
                    if (item.IsOutOfStock) {
                        RelatedItems += "<div class='cssClassButtonWrapper cssClassOutOfStock'><a href='#'><span>Out Of Stock</span></a></div></div>";
                    } else {
                    RelatedItems += "<div class='cssClassButtonWrapper'><a href='#' onclick='ItemDetail.AddToCartToJS(" + item.ItemID + "," + item.Price + "," + JSON2.stringify(item.SKU) + "," + 1 + ");'><span>Add to Cart</span></a></div></div>";

                    }
                } else {
                RelatedItems += "<div class='cssClassButtonWrapper'><a href='#' onclick='ItemDetail.AddToCartToJS(" + item.ItemID + "," + item.Price + "," + JSON2.stringify(item.SKU) + "," + 1 + ");'><span>Add to Cart</span></a></div></div>";
                }
                // $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
            },
            AddToCartToJS: function(itemId, itemPrice, itemSKU, itemQuantity) {
               AspxCommerce.RootFunction.AddToCartFromJS(itemId, itemPrice, itemSKU, itemQuantity, storeId, portalId, customerId, sessionCode, userName, cultureName);
            },
            CheckItemQuantityInCart: function(itemId) {

                this.config.method = "AspxCommerceWebService.asmx/CheckItemQuantityInCart";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ itemID: itemId, storeID: storeId, portalID: portalId, customerID: customerId, sessionCode: sessionCode });
                this.config.ajaxCallMode = 16;
                this.config.error = 16;
                this.ajaxCall(this.config);
                return ItemDetail.vars.itemQuantityInCart;
            },
            GetImageLists: function(cids, sku) {
                this.config.method = "AspxCommerceWebService.asmx/GetItemsImageGalleryInfoBySKU";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ itemSKU: sku, storeID: storeId, portalID: portalId, costVariantIDs: cids });
                this.config.ajaxCallMode = 17;
                this.config.error = 17;
                this.ajaxCall(this.config);

            },
            AddStyle: function() {
                this.config.method = "AspxCommerceWebService.asmx/ReturnDimension";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ userModuleID: userModuleID, portalID: portalId, culture: cultureName });
                this.config.ajaxCallMode = 18;
                this.config.error = 18;
                this.ajaxCall(this.config);
            },
            GetFilePath: function(msg) {
                var imgList = '';
                //$('.pika-image,.jcarousel-skin-pika').html('');
                var html = '';
                html = '<ul id="pikame" class="jcarousel-skin-pika">';
                if (msg.d.length > 0) {
                    $.each(msg.d, function(index, item) {
                        html += "<li><img id=\"image1\" src=" + aspxRootPath + item.ImagePath.replace('uploads', 'uploads/Large') + " alt=" + item.AlternateText + " title=" + item.AlternateText + " /></li>";
                    });
                    html += '</ul>';
                } else {
                    html += "<li><img id=\"image1\" src=" + aspxRootPath + noItemDetailImagePath + " /></li>";
                    html += '</ul>';
                }
                $(".pikachoose").append(html);
            },
            ImageZoom: function() {
                //$('.pika-image,.jcarousel-skin-pika').html('');
                $("#zoom01").gzoom({
                    sW: newObject.width,
                    sH: newObject.height,
                    lW: 1600,
                    lH: 1250,
                    lightbox: true
                });
            },
            Gallery: function() {
                //$('.pika-image,.jcarousel-skin-pika').html('');
                $("#pikame").PikaChoose({ showCaption: false });
                $("#pikame").jcarousel({
                    scroll: 3,
                    transition: [6],
                    initCallback: function(carousel) {
                        $(carousel.list).find('img').click(function() {
                            //console.log($(this).parents('.jcarousel-item').attr('jcarouselindex'));
                            carousel.scroll(parseInt($(this).parents('.jcarousel-item').attr('jcarouselindex')));
                        });
                    }
                });
            },
            SetValueForStyle: function(msg) {
                $('div.pika-image').css("width", msg.d[0] + 2);
                $('div.pika-image').css("height", msg.d[1] + 2);
                $('#image1').css('width', msg.d[2]);
                $('#image1').css('height', msg.d[2]);
                newObject = new Variable(msg.d[1], msg.d[0], msg.d[2], msg.d[3]);
            },
            BindItemsBasicInfo: function(item) {
                //        if (item.ImagePath == "") {
                //            item.ImagePath = aspxRootPath+ "Modules/AspxCommerce/AspxItemsManagement/uploads/noitem.png";
                //        }
                //        if (item.AlternateText == "") {
                //            item.AlternateText = item.Name;
                //        }
                //        $(".cssClassProductBigPicture").html("<img src=" + item.ImagePath + " width=\"323px\" height=\"238px\" alt=" + item.AlternateText + " title=" + item.AlternateText + "/>");
                $("#spanListPrice").html(item.ListPrice);
                $("#spanItemName").html(itemName);
                $("#spanSKU").html(item.SKU);
                $("#spanPrice").html(item.Price);
                $("#hdnPrice").val(item.Price);
                $("#hdnListPrice").val(item.ListPrice);
                $("#hdnWeight").val(item.Weight);
                //$("#spanWeight").val(item.Weight);
                $("#hdnQuantity").val(item.Quantity);
                if (allowOutStockPurchase.toLowerCase() == 'false') {
                    if (item.IsOutOfStock) {
                        $("#btnAddToMyCart span span").html('Out Of Stock');
                        $("#btnAddToMyCart").attr("disabled", "disabled");
                        $("#btnAddToMyCart").addClass('cssClassOutOfStock');
                        $("#btnAddToMyCart").show();
                    } else {
                        $("#btnAddToMyCart span span").html('Add To Cart');
                        $("#btnAddToMyCart").removeAttr("disabled");
                        $("#btnAddToMyCart").removeClass('cssClassOutOfStock');
                        $("#btnAddToMyCart").addClass('cssClassAddToCard');
                        $("#btnAddToMyCart").show();
                    }
                } else {
                    $("#btnAddToMyCart").show();
                }
                $("#txtQty").val('1');
                $("#txtQty").attr('addedValue', 1);
                $("#spanTax").html(item.TaxRateValue);
                $("#hdnTaxRateValue").val(item.TaxRateValue);
                if (item.SampleLink != '' || item.SampleFile != '') {
                    $("#dwnlDiv").show();
                    $("#spanDownloadLink").html(item.SampleLink);
                    $("#spanDownloadLink").attr("href", item.SampleFile);


                } else {
                    $("#dwnlDiv").hide();
                }

                var savingPercent = ((item.ListPrice - item.Price) / item.ListPrice) * 100;
                savingPercent = savingPercent.toFixed(2);
                $("#spanSaving").html('<b>' + savingPercent + '%</b>');
                if (allowOutStockPurchase.toLowerCase() == 'false') {
                    if (item.IsOutOfStock) {
                        $("#spanAvailability").html('<b>Out of stock</b>');
                    } else {
                        $("#spanAvailability").html('<b>In stock</b>');
                    }
                } else {
                    $("#spanAvailability").html('<b>In stock</b>');
                }

                var shortDesc = '';
                if (item.ShortDescription.length > 870) {
                    shortDesc = item.ShortDescription.substring(0, 870)
                    shortDesc += " >>>";
                } else {
                    shortDesc = item.ShortDescription;
                }
                $("#divItemShortDesc").html(Encoder.htmlDecode(shortDesc));
                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                //$("#divItemFullDesc").html(Encoder.htmlDecode(item.Description));
                //$("#divItemFullDesc").hide();
                //$("#divReadLess").hide();
            },
            AddUpdateRecentlyViewedItem: function(itemSKU) {
                this.config.method = "AspxCommerceWebService.asmx/AddUpdateRecentlyViewedItems";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ itemSKU: itemSKU, IP: userIP, sessionCode: sessionCode, countryName: countryName, userName: userName, storeID: storeId, portalID: portalId, cultureName: cultureName });
                this.config.ajaxCallMode = 19;
                this.config.error = 19;
                this.ajaxCall(this.config);
            },
            AddToMyCart: function() {
                if ($.trim($("#txtQty").val()) == "" || $.trim($("#txtQty").val()) <= 0) {
                    // alert("Invalid quantity");
                    csscody.alert('<h2>Information Alert</h2><p>Invalid quantity.</p>');
                    return false;
                }

                var itemQuantityInCart = ItemDetail.CheckItemQuantityInCart(itemId);
                if (itemQuantityInCart != 0.1) { //To know whether the item is downloadable (0.1 downloadable)
                    if (allowOutStockPurchase.toLowerCase() == 'false') {
                        if ($("#hdnQuantity").val() <= 0) {
                            csscody.alert("<h2>Information Alert</h2><p>The item is out of stock and out of stock item is not allowed to purchase!</p>");
                            return false;
                        } else {

                            if ((eval($.trim($("#txtQty").val())) + eval(itemQuantityInCart)) > eval($("#hdnQuantity").val())) {
                                csscody.alert("<h2>Information Alert</h2><p>You Can't add more than " + $("#hdnQuantity").val() + " quantity!</p>");
                                return false;
                            }
                        }
                    }
                }

                var itemPrice = $("#hdnPrice").val();
                var itemQuantity = $.trim($("#txtQty").val());
                var itemCostVariantIDs = "";
                var weightWithVariant = 0;
                var totalWeightVariant = 0;
                var costVariantPrice = 0;
                if ($('#divCostVariant').is(':empty')) {
                    itemCostVariantIDs = null;
                } else {
                    $("#divCostVariant input[type=radio]:checked").each(function() {
                        itemCostVariantIDs += $(this).val() + ",";
                        if ($(this).attr('variantvalue') != undefined) {
                            costVariantPrice += $(this).attr('variantvalue');
                        }
                        if ($(this).attr('variantwtvalue') != undefined) {
                            weightWithVariant += $(this).attr('variantwtvalue');
                        }
                    });

                    $("#divCostVariant input[type=checkbox]:checked").each(function() {
                        itemCostVariantIDs += $(this).val() + ",";
                        if ($(this).attr('variantvalue') != undefined) {
                            costVariantPrice += $(this).attr('variantvalue');
                        }
                        if ($(this).attr('variantwtvalue') != undefined) {
                            weightWithVariant += $(this).attr('variantwtvalue');
                        }
                    });

                    $("#divCostVariant select option:selected").each(function() {
                        itemCostVariantIDs += $(this).val() + ",";
                        if ($(this).attr('variantvalue') != undefined) {
                            costVariantPrice += $(this).attr('variantvalue');
                        }
                        if ($(this).attr('variantwtvalue') != undefined) {
                            weightWithVariant += $(this).attr('variantwtvalue');
                        }
                    });
                    itemCostVariantIDs = itemCostVariantIDs.substring(0, itemCostVariantIDs.length - 1);
                }
                totalWeightVariant = eval($("#hdnWeight").val()) + eval(weightWithVariant);
                itemPrice = eval(itemPrice) + eval(costVariantPrice);
                var param = { itemID: itemId, itemPrice: itemPrice, weight: totalWeightVariant, itemQuantity: itemQuantity, itemCostVariantIDs: itemCostVariantIDs, storeID: storeId, portalID: portalId, custometID: customerId, sessionCode: sessionCode, userName: userName, cultureName: cultureName };
                var data = JSON2.stringify(param);
                this.config.method = "AspxCommerceWebService.asmx/AddItemstoCartFromDetail";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = data;
                this.config.ajaxCallMode = 20;
                this.config.oncomplete = 20;
                this.config.error = 20;
                this.ajaxCall(this.config);

            },
            pageselectCallback: function(page_index, jq) {
                // Get number of elements per pagionation page from form
                var items_per_page = $('#ddlPageSize').val();
                var max_elem = Math.min((page_index + 1) * items_per_page, arrItemReviewList.length);
                $("#tblRatingPerUser").html('');
                //alert(arrItemDetailsReviewList.length + '::' + arrItemReviewList.length);
                // Iterate through a selection of the content and build an HTML string
                ItemsReview = '';
                for (var i = page_index * items_per_page; i < max_elem; i++) {
                    ItemDetail.BindAverageUserRating(arrItemReviewList[i]);
                    ItemsReview += arrItemReviewList[i].ItemReviewID;
                }
                $.each(arrItemDetailsReviewList, function(index, item) {
                    if (ItemsReview.indexOf(item.ItemReviewID) != -1) {
                        ItemDetail.BindPerUserIndividualRatings(item.ItemReviewID, item.ItemRatingCriteria, item.RatingValue);
                    }
                });

                $('input.star-rate').rating();
                $("#tblRatingPerUser tr:even").addClass("cssClassAlternativeOdd");
                $("#tblRatingPerUser tr:odd").addClass("cssClassAlternativeEven");
                // Prevent click event propagation
                return false;
            },
            getOptionsFromForm: function() {
                var opt = { callback: ItemDetail.pageselectCallback };
                opt["items_per_page"] = $('#ddlPageSize').val();
                opt["prev_text"] = "Prev";
                opt["next_text"] = "Next";
                opt["prev_show_always"] = false;
                opt["next_show_always"] = false;


                return opt;
            },
            ResetGallery: function() {
                $('.pika-image,.jcarousel-skin-pika,.pikachoose').html('');
                var ids = '';
                $("#divCostVariant input[type=radio]:checked").each(function() {
                    ids += $(this).val() + ",";
                });

                $("#divCostVariant input[type=checkbox]:checked").each(function() {
                    ids += $(this).val() + ",";
                });
                $("#divCostVariant select option:selected").each(function() {
                    ids += $(this).val() + ",";
                });
                ids = ids.substr(0, ids.length - 1);
                ItemDetail.GetImageLists(ids, itemSKU);
            },
            Init: function() {
                if (itemName != "") {
                    if (allowWishListItemDetail.toLowerCase() != 'true') {
                        $('#addWishListThis').hide();
                    }
                    if (allowCompareItemDetail.toLowerCase() != 'true') {
                        $('#addCompareListThis').hide();
                    }
                    var costVariantsData = '';

                    if ($.session("ItemCostVariantData")) {
                        costVariantsData = $.session("ItemCostVariantData");
                        arrCostVariants = costVariantsData.split(',');
                    }
                    if (userFriendlyURL) {
                        $("#lnkContinueShopping").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + 'Home.aspx');
                    } else {
                        $("#lnkContinueShopping").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + 'Home');
                    }

                    $("#divEmailAFriend").hide();
                    if (enableEmailFriend.toLowerCase() == 'true') {
                        $("#divEmailAFriend").show();
                    }

                    if (customerId <= 0 && userName.toLowerCase() == "anonymoususer") {
                        if (allowAnonymousReviewRate.toLowerCase() == 'true') {
                            $("a").addClass("popupAddReview");
                        } else {
                            $("a").removeClass("popupAddReview");
                            $(".cssClassAddYourReview").hide();
                        }
                    }
                    $("#btnAddToMyCart").hide();
                    ItemDetail.BindItemQuantityDiscountByUserName(itemSKU);
                    //ItemDetail.GetImageLists(itemSKU);
                    ItemDetail.BindItemBasicByitemSKU(itemSKU);
                    ItemDetail.GetFormFieldList(itemSKU);
                    ItemDetail.CheckReviewByUser(userName);
                    ItemDetail.CheckReviewByIP(userIP);
                    ItemDetail.BindItemAverageRating();
                    ItemDetail.BindDownloadEvent();
                    //ItemDetail.GetCostVariantsByitemSKU(itemSKU);
                    var itemVariantIds = '';
                    $("#divCostVariant input[type=radio]:checked").each(function() {
                        itemVariantIds += $(this).val() + ",";
                    });
                    $("#divCostVariant input[type=checkbox]:checked").each(function() {
                        itemVariantIds += $(this).val() + ",";
                    });
                    $("#divCostVariant select option:selected").each(function() {
                        itemVariantIds += $(this).val() + ",";
                    });
                    itemVariantIds = itemVariantIds.substr(0, itemVariantIds.length - 1);
                    ItemDetail.GetImageLists(itemVariantIds, itemSKU);
                   
                    if (customerId > 0 && userName.toLowerCase() != "anonymoususer") {
                        if (allowMultipleReviewPerUser.toLowerCase() != "true" && ItemDetail.vars.existReviewByUser == true) {
                            $("a").removeClass("popupAddReview");
                            $(".cssClassAddYourReview").hide();
                        }
                    }

                    if (allowMultipleReviewPerIP.toLowerCase() != "true" && ItemDetail.vars.existReviewByIP == true) {
                        $("a").removeClass("popupAddReview");
                        $(".cssClassAddYourReview").hide();
                    }
                    $("#txtQty").bind("contextmenu", function(e) {
                        return false;
                    });
                    $('#txtQty').bind('paste', function(e) {
                        e.preventDefault();
                    });
                    $("#txtQty").bind('focus', function(e) {
                        $(this).val('');
                        $('#lblNotification').html('');
                    });
                    $("#txtQty").bind('select', function(e) {
                        $(this).val('');
                        $('#lblNotification').html('');
                    });
                    $("#txtQty").bind('blur', function(e) {
                        $('#lblNotification').html('');
                        $("#txtQty").val($(this).attr('addedValue'));
                    });
                    $("#txtQty").bind("keypress", function(e) {
                        if (allowOutStockPurchase.toLowerCase() == 'false') {
                            if ($("#hdnQuantity").val() <= 0) {
                                return false;
                            } else {
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

                                    var itemQuantityInCart = ItemDetail.CheckItemQuantityInCart(itemId);
                                    if (itemQuantityInCart != 0.1) { //to test if the item is downloadable or simple(0.1 downloadable)

                                        if ((eval($("#txtQty").val() + '' + num) + eval(itemQuantityInCart)) > eval($("#hdnQuantity").val())) {
                                            $('#lblNotification').html('The quantity is greater than the available quantity.');
                                            return false;
                                            //$("#txtQty").val('1');
                                            //$('#lblNotification').html('');

                                        } else {
                                            $('#lblNotification').html('');
                                        }
                                    } else {
                                        $("#txtQty").val(1).attr("disabled", "disabled");
                                    }

                                }
                            }
                        }

                        if ($(this).val() == "") {
                            if (e.which != 8 && e.which != 0 && (e.which < 49 || e.which > 57)) {
                                return false;
                            }
                        } else {
                            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                                return false;
                            }
                        }
                        $("#txtQty").attr('addedValue', eval($("#txtQty").val() + '' + num));
                    });

                    $(".cssClassTotalReviews").bind("click", function() {
                        $.metadata.setType("class");
                        //BindRatingReviewTab();
                        //select the Tab for Rating
                        var $tabs = $('#ItemDetails_TabContainer').tabs();
                        $tabs.tabs('select', "ItemTab-Reviews");
                    });

                    ItemDetail.BindRatingCriteria();

                    $('a.popupAddReview').bind("click", function() {
                        ItemDetail.BindPopUp();
                        ShowPopup(this);
                    });

                    $(".cssClassClose").click(function() {
                        $('#fade, #popuprel2').fadeOut();
                    });

                    $("#btnSubmitReview").click(function() {
                        $("#form1").validate({
                            messages: {
                                urname: {
                                    required: '*',
                                    minlength: "* (at least 2 chars)"
                                },
                                uremail: {
                                    required: '*'
                                },
                                fname: {
                                    required: '*',
                                    minlength: "* (at least 2 chars)"
                                },
                                femail: {
                                    required: '*'
                                },
                                subject: {
                                    required: '*',
                                    minlength: "* (at least 2 chars)"
                                },
                                message: {
                                    required: '*',
                                    minlength: "* (at least 100 chars)"
                                },
                                name: {
                                    required: '*',
                                    minlength: "* (at least 2 chars)"
                                },
                                summary: {
                                    required: '*',
                                    minlength: "* (at least 2 chars)"
                                },
                                review: {
                                    required: '*',
                                    minlength: "*"
                                }
                            },
                            //success: "valid",
                            submitHandler: function() { ItemDetail.SaveItemRatings(); }
                        });
                    });

                    $('a.popupEmailAFriend').click(function() {
                        ItemDetail.ShowUsingPage();
                        //ShowPopup(this);
                    });

                    $("#addWishListThis").bind("click", function() {
                        AspxCommerce.RootFunction.AddToWishList(itemId);
                    });


                    $("#addCompareListThis").bind("click", function() {
                        AddItemsToMyCompare(itemId);
                    });

                    function AddItemsToMyCompare(itemId) {
                        var countCompareItems = ItemDetail.GetCompareItemsCount();
                        if (countCompareItems >= maxCompareItemCount) {
                            csscody.alert('<h2>Information Alert</h2><p>More than ' + maxCompareItemCount + ' tems are not allowed to add in compare list!</p>');
                            return false;
                        }
                        var checkparam = { ID: itemId, storeID: storeId, portalID: portalId, userName: userName, sessionCode: sessionCode };

                        var checkdata = JSON2.stringify(checkparam);
                        $.ajax({
                            type: "POST",
                            url: aspxservicePath + "AspxCommerceWebService.asmx/CheckCompareItems",
                            data: checkdata,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function(msg) {
                                if (msg.d) {
                                    csscody.alert("<h2>Information Alert</h2><p>The selected item already exist in compare list.</p>");
                                } else {
                                    AddToMyCompareList(itemId);
                                }
                            },
                            error: function(msg) {
                                csscody.error('<h2>Error Message</h2><p>Failed to add item in compare list!</p>');
                            }
                        });
                    }

                    function AddToMyCompareList(itemId) {
                        var addparam = { ID: itemId, storeID: storeId, portalID: portalId, userName: userName, IP: userIP, countryName: countryName, sessionCode: sessionCode };
                        var adddata = JSON2.stringify(addparam);
                        $.ajax({
                            type: "POST",
                            url: aspxservicePath + "AspxCommerceWebService.asmx/SaveCompareItems",
                            data: adddata,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function(msg) {
                                csscody.info("<h2>Information Message</h2><p>Item has been successfully added to compare list.</p>");
                                if ($("#h2compareitems").length > 0) {
                                    ItemsCompare.GetCompareItemList(); //for MyCompareItem 
                                }
                            },
                            error: function(msg) {
                                csscody.error('<h2>Error Message</h2><p>Failed to add item in compare list!</p>');
                            }
                        });
                    }
                } else {
                    $('#itemDetails').hide();
                    if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                        window.location = AspxCommerce.utils.GetAspxRedirectPath() + "Home.aspx";
                    } else {
                        window.location = AspxCommerce.utils.GetAspxRedirectPath() + "Home";
                    }
                }
            }
        };
        ItemDetail.Init();
    });