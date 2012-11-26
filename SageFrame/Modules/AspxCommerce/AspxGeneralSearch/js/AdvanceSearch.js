var AdvanceSearch = "";
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var ip = AspxCommerce.utils.GetClientIP();
    var countryName = AspxCommerce.utils.GetAspxClientCoutry();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();

    var arrItemListType = new Array();
    var arrResultToBind = new Array();
    var currentpage = 0;

    var sort_by = function(field, reverse, primer) {
        reverse = (reverse) ? -1 : 1;
        return function(a, b) {
            a = a[field];
            b = b[field];
            if (typeof (primer) != 'undefined') {
                a = primer(a);
                b = primer(b);
            }
            if (a < b) return reverse * -1;
            if (a > b) return reverse * 1;
            return 0;
        }
    }
    function LoadAdvanceSearchStaticImage() {
        $('#ajaxAdvanceSearchImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
    }
    function AddUpdateAdvanceSearchTerm() {
        var searchTerm = $.trim($("#txtSearchFor").val());
        if (searchTerm == "") {
            return false;
        }
        if (searchTerm == "What are you shopping today?") {
            searchTerm = "";
            return false;
        }

        $.ajax({
            type: "POST",
            url: aspxservicePath + "AspxCommerceWebService.asmx/AddUpdateSearchTerm",
            data: JSON2.stringify({ searchTerm: searchTerm, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName }),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function() {
            }
        });
    }
    AdvanceSearch = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: aspxservicePath + "AspxCommerceWebService.asmx/",
            url: "",
            method: ""
        },

        ajaxCall: function(config) {
            $.ajax({
                type: AdvanceSearch.config.type,
                contentType: AdvanceSearch.config.contentType,
                cache: AdvanceSearch.config.cache,
                async: AdvanceSearch.config.async,
                data: AdvanceSearch.config.data,
                dataType: AdvanceSearch.config.dataType,
                url: AdvanceSearch.config.url,
                success: AdvanceSearch.ajaxSuccess,
                error: AdvanceSearch.ajaxFailure
            });
        },

        init: function() {
            $(document).ajaxStart(function() {
                $('#divAjaxLoader').show();
            });

            $(document).ajaxStop(function() {
                $('#divAjaxLoader').hide();
            });

            $("#txtPriceFrom").bind("keypress", function(e) {
                if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                    if (e.which != 13) {
                        $("#errmsgPriceFrom").html("Valid Digits And Decimal Only").css("color", "red").show().fadeOut(1600);
                        return false;
                    }
                }

            });
            $("#txtPriceFrom,#txtPriceTo").bind('paste', function(e) {
                e.preventDefault();
            });
            $("#txtPriceFrom,#txtPriceTo").bind('contextmenu', function(e) {
                e.preventDefault();
            });
            $("#txtPriceTo").keypress(function(e) {
                if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {

                    if (e.which != 13) {
                        $("#errmsgPriceTo").html("Valid Digits And Decimal Only").css("color", "red").show().fadeOut(1600);
                        return false;
                    }
                }
            });

            LoadAllAspxTemplate();
            LoadAdvanceSearchStaticImage();
            AdvanceSearch.LoadAllCategoryForSearch();
            AdvanceSearch.BindItemsViewAsDropDown();
            AdvanceSearch.AdvanceSearchHideAll();
            AdvanceSearch.BindAttributes();
            $("#ddlViewAs").val(1);
            AdvanceSearch.BindItemsSortByDropDown();
            $("#ddlSortBy").val(1);
            $("#ddlViewAs").change(function() {
                AdvanceSearch.BindResults();
            });
            $("#ddlSortBy").change(function() {
                // Create pagination element with options       
                var offset = 1;
                var limit = $('#ddlPageSize').val();
                AdvanceSearch.ShowSearchResult(offset, limit, 0);
            });

            $("#ddlPageSize").change(function() {
                var offset = 1;
                var limit = $(this).val();
                AdvanceSearch.ShowSearchResult(offset, limit, 0);
            });

            $("#btnAdvanceSearch").click(function() {
                var offset = 1;
                var limit = $("#ddlPageSize").val();
                AdvanceSearch.ShowSearchResult(offset, limit, 0);
                AddUpdateAdvanceSearchTerm();
            });


            $('#txtSearchFor').autocomplete({
                source: function(request, response) {
                    $.ajax({
                        url: aspxservicePath + "AspxCommerceWebService.asmx/GetSearchedTermList",
                        data: JSON2.stringify({ search: $('#txtSearchFor').val(), storeID: storeId, portalID: portalId }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function(data) { return data; },
                        success: function(data) {
                            response($.map(data.d, function(item) {
                                return {
                                    value: item.SearchTerm
                                }
                            }))
                        },
                        error: function(XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 2
            });


            $(".searchForTextBox").each(function() {
                if ($(this).val() == "") {
                    $(this).addClass("lightText").val("What are you shopping today?");
                }
            });

            $(".searchForTextBox").bind("focus", function() {
                if ($(this).val() == "What are you shopping today?") {
                    $(this).removeClass("lightText").val("");
                }
                // focus lost action
            });

            $(".searchForTextBox").bind("blur", function() {
                if ($(this).val() == "") {
                    $(this).val("What are you shopping today?").addClass("lightText");
                }
            });

            $("#txtSearchFor,#txtPriceTo,#txtPriceFrom").keyup(function(event) {
                if (event.keyCode == 13) {
                    $("#btnAdvanceSearch").click();
                }
            });
            if ($('#divCheckBox ul li input[id=8]').is("checked")) {
                $('.pricebox input,.pricebox span').show();
                $("#txtPriceFrom").val('');
                $("#txtPriceTo").val('');
            }
            else {
                $('.pricebox input,.pricebox span').hide();
                $("#txtPriceFrom").val('');
                $("#txtPriceTo").val('');
            }
        },

        BindAttributes: function() {
            this.config.url = this.config.baseURL + "GetAttributes";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, cultureName: cultureName });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        AdvanceSearchHideAll: function() {
            $("#divItemViewOptions").hide();
            $("#divSearchPageNumber").hide();
            $("#divShowAdvanceSearchResult").hide();
        },
        BindItemsViewAsDropDown: function() {
            this.config.url = this.config.baseURL + "BindItemsViewAsList";
            this.config.data = '{}';
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        BindItemsSortByDropDown: function() {
            this.config.url = this.config.baseURL + "BindItemsSortByList";
            this.config.data = '{}';
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },

        AddToWishList: function(itemID) {
            if (userName.toLowerCase() != 'anonymoususer') {
                this.config.url = this.config.baseURL + "CheckWishItems";
                this.config.data = JSON2.stringify({ ID: itemID, storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
            }
            else {
                Login();
            }
        },

        LoadAllCategoryForSearch: function() {
            var isActive = true;
            this.config.url = this.config.baseURL + "GetAllCategoryForSearch";
            this.config.data = JSON2.stringify({ prefix: '---', isActive: isActive, culture: cultureName, storeID: storeId, portalID: portalId, userName: userName });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
        },

        ShowSearchResult: function(offset, limit, currentpage1) {
            currentpage = currentpage1;
            var categoryId = $("#ddlCategory").val();
            var chkIDs = '';
            var priceFrom = $.trim($("#txtPriceFrom").val());
            var priceTo = $.trim($("#txtPriceTo").val());
            var searchText = $.trim($("#txtSearchFor").val());
            if (searchText == "What are you shopping today?") {
                //  searchText = "";          
            }
            var attributeIds = '';
            if (categoryId == "0") {
                categoryId = null;
            }
            if (searchText == "") {
                alert("Enter Search Text");
                return false;
            }
            if (searchText == "What are you shopping today?") {
                searchText = "";
            }           
            if (priceTo != "") {
                if (!/^[0-9]\d*(\.\d+)?$/.test(priceTo)) {
                    $("#errmsgPriceTo").html("Valid Digits And Decimal Only").css("color", "red").show().fadeOut(1600);
                    return false;
                }
            }
            if (priceFrom != "") {
                if (!/^[0-9]\d*(\.\d+)?$/.test(priceFrom)) {
                    $("#errmsgPriceFrom").html("Valid Digits And Decimal Only").css("color", "red").show().fadeOut(1600);
                    return false;
                }
            }
            priceFrom = parseFloat(priceFrom);
            priceTo = parseFloat(priceTo);
            if (priceFrom == "" && priceTo == "") {
                priceFrom = null;
                priceTo = null;
            }
            else if (parseInt(priceTo, 10) < parseInt(priceFrom, 10)) {
                csscody.alert('<h2>Information Alert</h2><p>To Price must be greater than From Price</p>');
                return false;
            }        

            if (priceFrom != "" && priceTo == "") {
                priceTo = null;
            }

            if (priceTo != "" && priceFrom == "") {
                priceFrom = null;
            }


            $("#divCheckBox ul").each(function() {
                var check = $(this).find("input[type='checkbox']");
                $.each(check, function() {
                    if ($(this).attr("checked")) {
                        attributeIds += $(this).attr('id') + ',';
                        chkIDs += $(this).attr('value');
                        chkIDs += ',';
                        chkIDs += $(this).attr('name');
                        chkIDs += '#';
                    }
                });
                attributeIds = attributeIds.substr(0, attributeIds.length - 1);

            });
            var params = JSON2.stringify({ offset: offset, limit: limit, storeID: storeId, portalID: portalId, categoryID: categoryId, searchText: searchText,
                checkValue: chkIDs, priceFrom: priceFrom,
                priceTo: priceTo, userName: userName, cultureName: cultureName, attributeIds: attributeIds
            });
            AdvanceSearch.config.url = AdvanceSearch.config.baseURL + "GetItemsByDyanamicAdvanceSearch";
            AdvanceSearch.config.data = params;
            AdvanceSearch.config.ajaxCallMode = 6;
            AdvanceSearch.ajaxCall(AdvanceSearch.config);
        },
        BindResults: function() {
            var viewAsOption = $("#ddlViewAs").val();
            if (arrResultToBind.length > 0) {
                switch (viewAsOption) {
                    case '1':
                        AdvanceSearch.GridView();
                        break;
                    case '2':
                        AdvanceSearch.ListView();
                        break;
                    case '3':
                        AdvanceSearch.Grid2View();
                        break;
                    case '4':
                        AdvanceSearch.Grid3View();
                        break;
                    case '5':
                        AdvanceSearch.CompactListView();
                        break;
                    case '6':
                        AdvanceSearch.ProductGridView();
                        break;
                    case '7':
                        AdvanceSearch.ListWithoutOptionsView();
                        break;
                }
            }
        },

        AddToCartToJSAdvanceSearch: function(itemId, itemPrice, itemSKU, itemQuantity) {

            AddToCartFromJS(itemId, itemPrice, itemSKU, itemQuantity, storeId, portalId, customerId, sessionCode, userName, cultureName);
        },

        GridView: function() {
            $("#divShowAdvanceSearchResult").html('');
            $.each(arrResultToBind, function(index, value) {
                if (value.ImagePath == "") {
                    value.ImagePath = noImageAdSearchPathSetting;
                }
                else if (value.AlternateText == "") {
                    value.AlternateText = value.Name;
                }
                var items = [{ AspxCommerceRoot: aspxRedirectPath, itemID: value.ItemID, name: value.Name, sku: value.SKU,
                    imagePath: aspxRootPath + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                    loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                    price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                    //                $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid.htm', function(template) {
                    //                    $.tmpl(template, items).appendTo("#divShowAdvanceSearchResult");
                    //                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    //                });

                    $.tmpl("scriptResultGridTemp", items).appendTo("#divShowAdvanceSearchResult");
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                    if (allowOutStockPurchaseSetting.toLowerCase() == 'false') {
                        if (value.IsOutOfStock) {
                            $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                            $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                            $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                            $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                        }
                    }
                });
                $("img").lazyload();
            },

            ListView: function() {
                $("#divShowAdvanceSearchResult").html('');
                $.each(arrResultToBind, function(index, value) {
                    if (value.ImagePath == "") {
                        value.ImagePath = noImageAdSearchPathSetting;
                    }
                    else if (value.AlternateText == "") {
                        value.AlternateText = value.Name;
                    }

                    var items = [{ AspxCommerceRoot: aspxRedirectPath, itemID: value.ItemID, name: value.Name, sku: value.SKU,
                        imagePath: aspxRootPath + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                        loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                        price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                        //                    $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultList.htm', function(template) {
                        //                        $.tmpl(template, items).appendTo("#divShowAdvanceSearchResult");
                        //                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        //                    });

                        $.tmpl("scriptResultListTemp", items).appendTo("#divShowAdvanceSearchResult");
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                        if (value.IsFeatured.toLowerCase() == 'no') {
                            $(".cssClassFeaturedBg_" + value.ItemID).hide();
                        }
                        if (allowOutStockPurchaseSetting.toLowerCase() == 'false') {
                            if (value.IsOutOfStock) {
                                $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                            }
                        }

                        if (value.HidePrice == true) {
                            $("#divProductListView_" + value.ItemID + " [class=\"cssClassListViewProductPrice\"]").html('');

                        }
                        if (value.IsFeatured != null) {
                            if (value.IsFeatured.toLowerCase() == "no") {
                                $("#divProductListView_" + value.ItemID + " [class=\"cssClassFeaturedBg\"]").hide();
                            }
                        }
                    });
                    $("img").lazyload();
                },

                Grid2View: function() {
                    $("#divShowAdvanceSearchResult").html('');
                    $.each(arrResultToBind, function(index, value) {
                        if (value.ImagePath == "") {
                            value.ImagePath = noImageAdSearchPathSetting;
                        }
                        else if (value.AlternateText == "") {
                            value.AlternateText = value.Name;
                        }
                        var items = [{ AspxCommerceRoot: aspxRedirectPath, itemID: value.ItemID, name: value.Name, sku: value.SKU,
                            imagePath: aspxRootPath + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                            loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                            price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                            //                        $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid2.htm', function(template) {
                            //                            $.tmpl(template, items).appendTo("#divShowAdvanceSearchResult");
                            //                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                            //                        });

                            $.tmpl("scriptResultGrid2Temp", items).appendTo("#divShowAdvanceSearchResult");
                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                            if (allowOutStockPurchaseSetting.toLowerCase() == 'false') {
                                if (value.IsOutOfStock) {
                                    $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                    $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                    $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                    $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                }
                            }
                        });
                        $("img").lazyload();
                    },

                    Grid3View: function() {
                        $("#divShowAdvanceSearchResult").html('');
                        $.each(arrResultToBind, function(index, value) {
                            if (value.ImagePath == "") {
                                value.ImagePath = noImageAdSearchPathSetting;
                            }
                            else if (value.AlternateText == "") {
                                value.AlternateText = value.Name;
                            }
                            var items = [{ AspxCommerceRoot: aspxRedirectPath, itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                imagePath: aspxRootPath + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                //                            $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid3.htm', function(template) {
                                //                                $.tmpl(template, items).appendTo("#divShowAdvanceSearchResult");
                                //                                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                //                            });

                                $.tmpl("scriptResultGrid3Temp", items).appendTo("#divShowAdvanceSearchResult");
                                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                            });
                            $("img").lazyload();
                        },

                        CompactListView: function() {
                            $("#divShowAdvanceSearchResult").html('');
                            var CompactListViewElements = '';
                            CompactListViewElements += '<div class="cssClassCompactList">';
                            CompactListViewElements += '<table width="100%" cellspacing="0" id="tblCompactList" cellpadding="0" border="0">';
                            CompactListViewElements += '<tr class="cssClassHeadeTitle">';
                            CompactListViewElements += '<td class="cssClassCLPicture">&nbsp;</td>';
                            CompactListViewElements += '<td class="cssClassCLProduct">Item</td>';
                            CompactListViewElements += '<td class="cssClassCLProductCode">SKU code</td>';
                            CompactListViewElements += '<td class="cssClassCLPrice">Price</td>';
                            CompactListViewElements += '<td class="cssClassCLAddtoCart">&nbsp;</td>';
                            CompactListViewElements += '</tr>';
                            CompactListViewElements += '</table></div>'
                            $("#divShowAdvanceSearchResult").html(CompactListViewElements);
                            $.each(arrResultToBind, function(index, value) {
                                if (value.ImagePath == "") {
                                    value.ImagePath = noImageAdSearchPathSetting;
                                }
                                else if (value.AlternateText == "") {
                                    value.AlternateText = value.Name;
                                }
                                var items = [{ AspxCommerceRoot: aspxRedirectPath, itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                    imagePath: aspxRootPath + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                    loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                    price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                    //                                $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptCompactList.htm', function(template) {
                                    //                                    var t = $("<div></div>").append(template).find("table tbody").html();
                                    //                                    var pattern = "%7B", re = new RegExp(pattern, "g");
                                    //                                    var pattern2 = "%7D", re2 = new RegExp(pattern2, "g");
                                    //                                    t = t.replace(re, "{");
                                    //                                    t = t.replace(re2, "}");
                                    //                                    $.tmpl(t, items).appendTo("#tblCompactList");
                                    //                                    $("#tblCompactList tr:even").addClass("cssClassAlternativeEven");
                                    //                                    $("#tblCompactList tr:odd").addClass("cssClassAlternativeOdd");
                                    //                                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                    //                                });

                                    $.tmpl("scriptCompactListTemp", items).appendTo("#tblCompactList");
                                    $("#tblCompactList tr:even").addClass("cssClassAlternativeEven");
                                    $("#tblCompactList tr:odd").addClass("cssClassAlternativeOdd");
                                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                                    if (allowOutStockPurchaseSetting.toLowerCase() == 'false') {
                                        if (value.IsOutOfStock) {
                                            $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                            $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                            $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                            $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                        }
                                    }

                                });
                                $("img").lazyload();
                            },

                            ProductGridView: function() {
                                $("#divShowAdvanceSearchResult").html('');
                                $.each(arrResultToBind, function(index, value) {
                                    if (value.ImagePath == "") {
                                        value.ImagePath = noImageAdSearchPathSetting;
                                    }
                                    else if (value.AlternateText == "") {
                                        value.AlternateText = value.Name;
                                    }
                                    var items = [{ AspxCommerceRoot: aspxRedirectPath, itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                        imagePath: aspxRootPath + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                        loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                        price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                        //                                    $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultProductGrid.htm', function(template) {
                                        //                                        $.tmpl(template, items).appendTo("#divShowAdvanceSearchResult");
                                        //                                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                        //                                    });

                                        $.tmpl("scriptResultProductGridTemp", items).appendTo("#divShowAdvanceSearchResult");
                                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                                        if (allowOutStockPurchaseSetting.toLowerCase() == 'false') {
                                            if (value.IsOutOfStock) {
                                                $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                                $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                                $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                                $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                            }
                                        }
                                        if (allowWishListAdvSearchSetting.toLowerCase() != 'true') {
                                            $('.cssClassWishListButton').hide();
                                        }
                                    });
                                    $("img").lazyload();
                                },

                                ListWithoutOptionsView: function() {
                                    $("#divShowAdvanceSearchResult").html('');
                                    $.each(arrResultToBind, function(index, value) {
                                        if (value.ImagePath == "") {
                                            value.ImagePath = noImageAdSearchPathSetting;
                                        }

                                        else if (value.AlternateText == "") {
                                            value.AlternateText = value.Name;
                                        }
                                        var items = [{ AspxCommerceRoot: aspxRedirectPath, itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                            imagePath: aspxRootPath + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                            loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                            price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                            //                                        $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultListWithoutOptions.htm', function(template) {
                                            //                                            $.tmpl(template, items).appendTo("#divShowAdvanceSearchResult");
                                            //                                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                            //                                        });

                                            $.tmpl("scriptResultListWithoutOptionsTemp", items).appendTo("#divShowAdvanceSearchResult");
                                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                                            if (allowOutStockPurchaseSetting.toLowerCase() == 'false') {
                                                if (value.IsOutOfStock) {
                                                    $(".cssClassInstock_" + value.ItemID).html('Out Of Stock');
                                                    $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                                    $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                                    $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                                    $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                                }
                                            }
                                            if (allowWishListAdvSearchSetting.toLowerCase() != 'true') {
                                                $('.cssClassWishListWithoutOption').hide();
                                            }
                                        });
                                        $("img").lazyload();
                                    },

                                    /**
                                    * Callback function that displays the content.
                                    *
                                    * Gets called every time the user clicks on a pagination link.
                                    *
                                    * @param {int}page_index New Page index
                                    * @param {jQuery} jq the container with the pagination links as a jQuery object
                                    */
                                    pageselectCallback: function(page_index, jq, execute) {
                                        if (execute) {
                                            // Get number of elements per pagionation page from form
                                            var items_per_page = $('#ddlPageSize').val();
                                            var sortByOption = $("#ddlSortBy").val();
                                            if (arrItemListType.length > 0) {
                                                switch (sortByOption) {
                                                    case '1': //Newest to oldest asc
                                                        arrItemListType.sort(sort_by('AddedOn', true));
                                                        // Sort by start_time
                                                        //arrItemListType.sort(sort_by('_AddedOn', false, function(a){return (new Date(a)).getTime()}));

                                                        break;
                                                    case '2': //Oldest to newest desc
                                                        arrItemListType.sort(sort_by('AddedOn', false));
                                                        break;
                                                    case '3': //Price highest to lowest asc
                                                        // Sort by price high to low
                                                        arrItemListType.sort(sort_by('Price', true, parseFloat));

                                                        //arrItemListType.sort(function(a, b) { return parseFloat(a.price) - parseFloat(b.price) });
                                                        break;
                                                    case '4': //Price lowest to highest desc
                                                        // Sort by price low to high
                                                        arrItemListType.sort(sort_by('Price', false, parseFloat));
                                                        //arrItemListType.sort(function(a, b) { return parseFloat(b.price) - parseFloat(a.price) });
                                                        break; break;
                                                }

                                                var max_elem = arrItemListType.length;
                                                var newcontent = '';
                                                arrResultToBind.length = 0;

                                                // Iterate through a selection of the content and build an HTML string
                                                for (var i = 0; i < max_elem; i++) {
                                                    //newcontent += '<dt>' + arrItemListType[i]._Name + '</dt>';
                                                    arrResultToBind.push(arrItemListType[i]);
                                                }
                                                AdvanceSearch.BindResults();
                                            }
                                        }

                                        // Replace old content with new content
                                        //$('#Searchresult').html(newcontent);

                                        // Prevent click event propagation
                                        return false;
                                    },

                                    ajaxSuccess: function(msg) {
                                        switch (AdvanceSearch.config.ajaxCallMode) {
                                            case 0:
                                                break;
                                            case 1:
                                                $.each(msg.d, function(index, item) {
                                                    var check = "<li><input type='checkbox' id=" + item.AttributeID + " value=" + item.InputTypeID + " name=" + item.ValidationTypeID + "><label> " + item.AttributeName + "</label></li>";
                                                    $("#divCheckBox ul").append(check);
                                                });
                                                if ($('#divCheckBox ul li input[id=8]').length > 0) {

                                                    $('#divCheckBox ul li input[id=8]').bind("click", function() {

                                                        if ($(this).attr('checked') == true) {
                                                            $("#txtPriceFrom").val('');
                                                            $("#txtPriceTo").val('');
                                                            $('.pricebox input,.pricebox span').show();
                                                        }
                                                        else {
                                                            $("#txtPriceFrom").val('');
                                                            $("#txtPriceTo").val('');
                                                            $('.pricebox input,.pricebox span').hide();
                                                        }


                                                    });

                                                }
                                                else {

                                                }
                                                break;
                                            case 2:
                                                if (msg.d.length > 0) {
                                                    $.each(msg.d, function(index, item) {
                                                        var displayOptions = "<option value=" + item.DisplayItemID + ">" + item.OptionType + "</option>"
                                                        $("#ddlViewAs").append(displayOptions);
                                                    });
                                                }
                                                break;
                                            case 3:
                                                if (msg.d.length > 0) {
                                                    $.each(msg.d, function(index, item) {
                                                        var displayOptions = "<option value=" + item.SortOptionTypeID + ">" + item.OptionType + "</option>"
                                                        $("#ddlSortBy").append(displayOptions);
                                                    });
                                                }
                                                break;
                                            case 4:
                                                if (msg.d) {
                                                    csscody.alert('<h2>Information Alert</h2><p>The selected item already in your wishlist.</p>');
                                                }
                                                else {
                                                    //  AddToList(itemID);
                                                    AdvanceSearch.AddToWishListFromJS(itemID, storeId, portalId, userName, ip, countryName);
                                                }
                                                break;
                                            case 5:
                                                var Elements = '';
                                                Elements += '<option value="0">--All Category--</option>';
                                                $.each(msg.d, function(index, item) {
                                                    Elements += "<option value=" + item.CategoryID + ">" + item.LevelCategoryName + "</option>";
                                                });
                                                $("#ddlCategory").html(Elements);
                                                break;
                                            case 6:
                                                var rowTotal = 0;
                                                $("#divShowAdvanceSearchResult").html('');
                                                arrItemListType.length = 0;
                                                $("#divShowAdvanceSearchResult").show();
                                                if (msg.d.length > 0) {
                                                    $("#divShowAdvanceSearchResult").html('');
                                                    $("#divItemViewOptions").show();
                                                    $("#divSearchPageNumber").show();
                                                    $.each(msg.d, function(index, item) {
                                                        rowTotal = item.RowTotal;
                                                        arrItemListType.push(item);
                                                    });
                                                    // Create pagination element with options from form
                                                    var items_per_page = $('#ddlPageSize').val();
                                                    $("#Pagination").pagination(rowTotal, {
                                                        callback: AdvanceSearch.pageselectCallback,
                                                        items_per_page: items_per_page,
                                                        //num_display_entries: 10,
                                                        current_page: currentpage,
                                                        callfunction: true,
                                                        function_name: { name: AdvanceSearch.ShowSearchResult, limit: $('#ddlPageSize').val() },
                                                        prev_text: "Prev",
                                                        next_text: "Next",
                                                        prev_show_always: false,
                                                        next_show_always: false

                                                    });
                                                    $("img").lazyload();
                                                }
                                                else {
                                                    $("#divItemViewOptions").hide();
                                                    $("#divSearchPageNumber").hide();
                                                    $("#divShowAdvanceSearchResult").html("No items found!");
                                                }
                                                break;
                                        }
                                    }
                                }
                                AdvanceSearch.init();
                            });
                             