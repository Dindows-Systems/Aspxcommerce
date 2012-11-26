<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsListByIds.ascx.cs"
    Inherits="Modules_Admin_DetailsBrowse_ItemsListByIds" %>

<script type="text/javascript">
    //<![CDATA[
    var ItemList = "";
    $(function() {
        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var customerId = AspxCommerce.utils.GetCustomerID();
        var ip = AspxCommerce.utils.GetClientIP();
        var countryName = AspxCommerce.utils.GetAspxClientCoutry();
        var sessionCode = AspxCommerce.utils.GetSessionCode();
        var itemIds = "<%=ItemIds%>";
        var arrItemsOption = new Array();
        var arrItemsOptionToBind = new Array();
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


        ItemList = {
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
            ajaxCall: function(config) {
                $.ajax({
                    type: ItemList.config.type,
                    contentType: ItemList.config.contentType,
                    cache: ItemList.config.cache,
                    async: ItemList.config.async,
                    url: ItemList.config.url,
                    data: ItemList.config.data,
                    dataType: ItemList.config.dataType,
                    success: ItemList.ajaxSuccess,
                    error: ItemList.ajaxFailure
                });
            },
            ajaxSuccess: function(msg) {
                switch (ItemList.config.ajaxCallMode) {
                    case 1:
                        var rowTotal = 0;
                        $("#divOptionsSearchResult").html('');
                        arrItemsOption.length = 0;
                        $("#divOptionsSearchResult").show();
                        // $("#ddlOptionViewAs").val(1);
                        // $("#ddlOptionSortBy").val(1);
                        if (msg.d.length > 0) {
                            $("#divOptionsSearchResult").html('');
                            $("#divOptionItemViewOptions").show();
                            $("#divOptionPageNumber").show();
                            $("#divOptionViewAs").val(1);
                            $.each(msg.d, function(index, item) {
                                rowTotal = item.RowTotal;
                                arrItemsOption.push(item);
                            });
                            // Create OptionPagination element with options from form
                            var items_per_page = $('#ddlOptionPageSize').val();
                            $("#OptionPagination").pagination(rowTotal, {
                                callback: ItemList.pageselectCallback,
                                items_per_page: items_per_page,
                                //num_display_entries: 10,
                                current_page: currentpage,
                                callfunction: true,
                                function_name: { name: ItemList.BindShoppingOptionResultItems, limit: $('#ddlOptionPageSize').val() },
                                prev_text: "Prev",
                                next_text: "Next",
                                prev_show_always: false,
                                next_show_always: false

                            });
                            $('img').lazyload();
                        }
                        else {
                            $("#divOptionItemViewOptions").hide();
                            $("#divOptionPageNumber").hide();
                            $("#divOptionsSearchResult").html("No items found!");
                        }
                        break;
                    case 2:
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                                var displayOptions = "<option value=" + item.DisplayItemID + ">" + item.OptionType + "</option>"
                                $("#ddlOptionViewAs").append(displayOptions);
                            });
                        }
                        ItemList.BindShoppingOptionResultItems(1, $('#ddlOptionPageSize').val(), 0);
                        break;
                    case 3:
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                                var displayOptions = "<option value=" + item.SortOptionTypeID + ">" + item.OptionType + "</option>"
                                $("#ddlOptionSortBy").append(displayOptions);
                            });
                        }
                        break;

                }

            }, ajaxFailure: function() {
                switch (ItemList.config.error) {
                    case 1:
                        csscody.error("<h2>Error Message</h2><p>Failed to load search result!</p>");
                        break;
                    case 4:
                        csscody.error("<h2>Error Message</h2><p>Failded to save search term!</p>");
                        break;
                }
            }, BindShoppingOptionResultItems: function(offset, limit, currentpage1) {
                currentpage = currentpage1;
                ItemList.config.method = "AspxCommerceWebService.asmx/GetShoppingOptionsItemsResult";
                ItemList.config.url = ItemList.config.baseURL + ItemList.config.method;
                ItemList.config.data = JSON2.stringify({ offset: offset, limit: limit, itemIds: itemIds, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
                ItemList.config.ajaxCallMode = 1;
                ItemList.config.error = 1;
                ItemList.ajaxCall(ItemList.config);

            }, ShoppingOptionsHideAll: function() {
                $("#divOptionItemViewOptions").hide();
                $("#divOptionPageNumber").hide();
                $("#divOptionsSearchResult").hide();
            }, BindItemOptionsResults: function() {
                var viewAsOption = $("#ddlOptionViewAs").val();
                if (arrItemsOptionToBind.length > 0) {
                    switch (viewAsOption) {
                        case '1':
                            ItemList.ShoppingOptionGridView();
                            break;
                        case '2':
                            ItemList.ShoppingOptionListView();
                            break;
                        case '3':
                            ItemList.ShoppingOptionGrid2View();
                            break;
                        case '4':
                            ItemList.ShoppingOptionGrid3View();
                            break;
                        case '5':
                            ItemList.ShoppingOptionCompactListView();
                            break;
                        case '6':
                            ItemList.ShoppingOptionProductGridView();
                            break;
                        case '7':
                            ItemList.ShoppingOptionListWithoutOptionsView();
                            break;
                    }
                }
            }, BindItemsOptionViewAsDropDown: function() {
                this.config.method = "AspxCommerceWebService.asmx/BindItemsViewAsList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = ({});
                this.config.ajaxCallMode = 2;
                this.config.error = 2;
                this.ajaxCall(this.config);

            }, BindItemsOptionSortByDropDown: function() {
                this.config.method = "AspxCommerceWebService.asmx/BindItemsSortByList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = ({});
                this.config.ajaxCallMode = 3;
                this.config.error = 3;
                this.ajaxCall(this.config);

            }, ShoppingOptionGridView: function() {
                $("#divOptionsSearchResult").html('');
                $.each(arrItemsOptionToBind, function(index, value) {
                    if (value.ImagePath == "") {
                        value.ImagePath = '<%=DefaultShoppingOptionImgPath%>';
                    }
                    else if (value.AlternateText == "") {
                        value.AlternateText = value.Name;
                    }
                    var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                        imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                        loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                        price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                        //                $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid.htm', function(template) {
                        //                    $.tmpl(template, items).appendTo("#divOptionsSearchResult");
                        //                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        //                });

                        $.tmpl("scriptResultGridTemp", items).appendTo("#divOptionsSearchResult");
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
                    $('img').lazyload();
                }, ShoppingOptionListView: function() {
                    $("#divOptionsSearchResult").html('');
                    $.each(arrItemsOptionToBind, function(index, value) {
                        if (value.ImagePath == "") {
                            value.ImagePath = '<%=DefaultShoppingOptionImgPath%>';
                        }
                        else if (value.AlternateText == "") {
                            value.AlternateText = value.Name;
                        }

                        var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                            imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                            loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                            price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                            //                    $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultList.htm', function(template) {
                            //                        $.tmpl(template, items).appendTo("#divOptionsSearchResult");
                            //                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                            //                    });

                            $.tmpl("scriptResultListTemp", items).appendTo("#divOptionsSearchResult");
                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                            if (value.IsFeatured.toLowerCase() == 'no') {
                                $(".cssClassFeaturedBg_" + value.ItemID).hide();
                            }
                            if ('<%=AllowOutStockPurchase %>'.toLowerCase() == 'false') {
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
                        $('img').lazyload();
                    }, ShoppingOptionGrid2View: function() {
                        $("#divOptionsSearchResult").html('');
                        $.each(arrItemsOptionToBind, function(index, value) {
                            if (value.ImagePath == "") {
                                value.ImagePath = '<%=DefaultShoppingOptionImgPath%>';
                            }
                            else if (value.AlternateText == "") {
                                value.AlternateText = value.Name;
                            }
                            var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                //                        $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid2.htm', function(template) {
                                //                            $.tmpl(template, items).appendTo("#divOptionsSearchResult");
                                //                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                //                        });

                                $.tmpl("scriptResultGrid2Temp", items).appendTo("#divOptionsSearchResult");
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
                            $('img').lazyload();
                        }, ShoppingOptionGrid3View: function() {
                            $("#divOptionsSearchResult").html('');
                            $.each(arrItemsOptionToBind, function(index, value) {
                                if (value.ImagePath == "") {
                                    value.ImagePath = '<%=DefaultShoppingOptionImgPath%>';
                                }
                                else if (value.AlternateText == "") {
                                    value.AlternateText = value.Name;
                                }
                                var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                    imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                    loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                    price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                    //                            $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid3.htm', function(template) {
                                    //                                $.tmpl(template, items).appendTo("#divOptionsSearchResult");
                                    //                                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                    //                            });

                                    $.tmpl("scriptResultGrid3Temp", items).appendTo("#divOptionsSearchResult");
                                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                });
                                $('img').lazyload();
                            }, ShoppingOptionCompactListView: function() {
                                $("#divOptionsSearchResult").html('');
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
                                $("#divOptionsSearchResult").html(CompactListViewElements);
                                $.each(arrItemsOptionToBind, function(index, value) {
                                    if (value.ImagePath == "") {
                                        value.ImagePath = '<%=DefaultShoppingOptionImgPath%>';
                                    }
                                    else if (value.AlternateText == "") {
                                        value.AlternateText = value.Name;
                                    }
                                    var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                        imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
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

                                        if ('<%=AllowOutStockPurchase %>'.toLowerCase() == 'false') {
                                            if (value.IsOutOfStock) {
                                                $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                                $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                                $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                                $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                            }
                                        }

                                    });
                                    $('img').lazyload();
                                }, ShoppingOptionProductGridView: function() {
                                    $("#divOptionsSearchResult").html('');
                                    $.each(arrItemsOptionToBind, function(index, value) {
                                        if (value.ImagePath == "") {
                                            value.ImagePath = '<%=DefaultShoppingOptionImgPath%>';
                                        }
                                        else if (value.AlternateText == "") {
                                            value.AlternateText = value.Name;
                                        }
                                        var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                            imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                            loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                            price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                            //                                    $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultProductGrid.htm', function(template) {
                                            //                                        $.tmpl(template, items).appendTo("#divOptionsSearchResult");
                                            //                                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                            //                                    });

                                            $.tmpl("scriptResultProductGridTemp", items).appendTo("#divOptionsSearchResult");
                                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                                            if ('<%=AllowOutStockPurchase %>'.toLowerCase() == 'false') {
                                                if (value.IsOutOfStock) {
                                                    $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                                    $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                                    $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                                    $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                                }
                                            }
                                            if ('<%=AllowWishListItemsById %>'.toLowerCase() != 'true') {
                                                $('.cssClassWishListButton').hide();
                                            }
                                        });
                                        $('img').lazyload();
                                    }, ShoppingOptionListWithoutOptionsView: function() {
                                        $("#divOptionsSearchResult").html('');
                                        $.each(arrItemsOptionToBind, function(index, value) {
                                            if (value.ImagePath == "") {
                                                value.ImagePath = '<%=DefaultShoppingOptionImgPath%>';
                                            }

                                            else if (value.AlternateText == "") {
                                                value.AlternateText = value.Name;
                                            }
                                            var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                                imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                                loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                                price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                                //                                        $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultListWithoutOptions.htm', function(template) {
                                                //                                            $.tmpl(template, items).appendTo("#divOptionsSearchResult");
                                                //                                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                                //                                        });

                                                $.tmpl("scriptResultListWithoutOptionsTemp", items).appendTo("#divOptionsSearchResult");
                                                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                                                if ('<%=AllowOutStockPurchase %>'.toLowerCase() == 'false') {
                                                    if (value.IsOutOfStock) {
                                                        $(".cssClassInstock_" + value.ItemID).html('Out Of Stock');
                                                        $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                                        $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                                        $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                                        $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                                    }
                                                }
                                                if ('<%=AllowWishListItemsById %>'.toLowerCase() != 'true') {
                                                    $('.cssClassWishListWithoutOption').hide();
                                                }
                                            });
                                            $('img').lazyload();
                                        }, pageselectCallback: function(page_index, jq, execute) {

                                            if (execute) {
                                                // Get number of elements per pagionation page from form
                                                var items_per_page = $('#ddlOptionPageSize').val();
                                                var sortByOption = $("#ddlOptionSortBy").val();
                                                if (arrItemsOption.length > 0) {
                                                    switch (sortByOption) {
                                                        case '1': //Newest to oldest asc
                                                            arrItemsOption.sort(sort_by('AddedOn', true));
                                                            // Sort by start_time
                                                            //arrItemsOption.sort(sort_by('_AddedOn', false, function(a){return (new Date(a)).getTime()}));

                                                            break;
                                                        case '2': //Oldest to newest desc
                                                            arrItemsOption.sort(sort_by('AddedOn', false));
                                                            break;
                                                        case '3': //Price highest to lowest asc
                                                            // Sort by price high to low
                                                            arrItemsOption.sort(sort_by('Price', true, parseFloat));

                                                            //arrItemsOption.sort(function(a, b) { return parseFloat(a.price) - parseFloat(b.price) });
                                                            break;
                                                        case '4': //Price lowest to highest desc
                                                            // Sort by price low to high
                                                            arrItemsOption.sort(sort_by('Price', false, parseFloat));
                                                            //arrItemsOption.sort(function(a, b) { return parseFloat(b.price) - parseFloat(a.price) });
                                                            break;
                                                        //                                            case '5':                               
                                                        //                                                SortArray();                               
                                                        //                                                break;                              
                                                    }
                                                    //                                        $.each(arrItemsOption, function(index, item) {
                                                    //                                            alert(item.AddedOn);
                                                    //                                        });


                                                    var max_elem = arrItemsOption.length;
                                                    var newcontent = '';
                                                    arrItemsOptionToBind.length = 0;

                                                    // Iterate through a selection of the content and build an HTML string
                                                    for (var i = 0; i < max_elem; i++) {
                                                        //newcontent += '<dt>' + arrItemsOption[i]._Name + '</dt>';
                                                        arrItemsOptionToBind.push(arrItemsOption[i]);
                                                    }
                                                    ItemList.BindItemOptionsResults();
                                                }
                                            }

                                            // Replace old content with new content
                                            //$('#Searchresult').html(newcontent);

                                            // Prevent click event propagation
                                            return false;
                                        }, AddUpdateSearchTerm: function() {
                                            if (searchText == "") {
                                                return false;
                                            }
                                            this.config.method = "AspxCommerceWebService.asmx/AddUpdateSearchTerm";
                                            this.config.url = this.config.baseURL + this.config.method;
                                            this.config.data = JSON2.stringify({ searchTerm: searchText, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
                                            this.config.ajaxCallMode = 4;
                                            this.config.error = 4;
                                            this.ajaxCall(this.config);
                                        },
                                        Init: function() {
                                            LoadAllAspxTemplate(); //from template js
                                            ItemList.ShoppingOptionsHideAll();
                                            ItemList.BindItemsOptionSortByDropDown();
                                            ItemList.BindItemsOptionViewAsDropDown();


                                            $("#ddlOptionViewAs").bind("change", function() {
                                                ItemList.BindItemOptionsResults();
                                            });

                                            $("#ddlOptionSortBy").bind("change", function() {
                                                // Create OptionPagination element with options
                                                var items_per_page = $("#ddlOptionPageSize").val();
                                                var offset = 1;
                                                ItemList.BindShoppingOptionResultItems(offset, items_per_page, 0);
                                            });

                                            $("#ddlOptionPageSize").bind("change", function() {
                                                var items_per_page = $(this).val();
                                                var offset = 1;
                                                ItemList.BindShoppingOptionResultItems(offset, items_per_page, 0);

                                            });
                                        }
                                    }
                                    ItemList.Init();
                                });
    
    
 

   
   //]]>
    
</script>

<div id="divOptionItemViewOptions" class="viewWrapper">
    <div id="divOptionViewAs" class="view">
        View as:
        <select id="ddlOptionViewAs" class="cssClassDropDown">
        </select>
    </div>
    <div id="divSortBy" class="sort">
        Sort by:
        <select id="ddlOptionSortBy" class="cssClassDropDown">
        </select>
    </div>
</div>
<div id="divOptionsSearchResult" class="cssClassDisplayResult">
</div>
<div class="cssClassClear">
</div>
<!-- TODO:: paging Here -->
<div class="cssClassPageNumber" id="divOptionPageNumber">
    <div class="cssClassPageNumberLeftBg">
        <div class="cssClassPageNumberRightBg">
            <div class="cssClassPageNumberMidBg">
                <div id="OptionPagination">
                </div>
                <div class="cssClassViewPerPage">
                    View Per Page
                    <select id="ddlOptionPageSize" class="cssClassDropDown">
                        <option value="5">5</option>
                        <option value="10">10</option>
                        <option value="15">15</option>
                        <option value="20">20</option>
                        <option value="25">25</option>
                        <option value="40">40</option>
                    </select></div>
            </div>
        </div>
    </div>
</div>

<%--<script id="scriptResultGrid" type="text/x-jquery-tmpl">
  <div class="cssClassProductsBox"> 
   <div class="cssClassProductsBoxInfo"> 
   <h2>${name}</h2> 
    <h3>${sku}</h3> 
    <div class="cssClassProductPicture"><img  src='${imagePath}' alt='${alternateText}'  title='${name}'></div> 
     <div class="cssClassProductPriceBox"> 
     <div class="cssClassProductPrice"> 
     <p class="cssClassProductOffPrice">Regular Price : <span class="cssClassFormatCurrency">${listPrice}</span></p> 
     <p class="cssClassProductRealPrice">Our Offer : <span class="cssClassFormatCurrency">${price}</span></p> 
    </div>  
    </div>
    <div class="cssClassProductDetail">  
     <p><a href="${AspxCommerceRoot}item/${sku}.aspx">Details</a></p>  
    </div>  
    <div class="cssClassclear"></div>  
    </div>  
    <div class="cssClassAddtoCard_${itemID} cssClassAddtoCard">
     <div class="cssClassButtonWrapper"> 
     <a href="#" onclick="AddToCartToJSShoppingOption(${itemID},${price},${JSON2.stringify(sku)},${1});"><span>Add to Cart</span></a>
</div></div>  
     </div>  
</script>--%>

<%--<script id="scriptResultList" type="text/x-jquery-tmpl">
<div class="cssClassProductListView">
<div class="cssClassProductListViewLeft">
<p class="cssClassProductPicture"><img  alt='${alternateText}' src='${imagePath}' title='${name}' /></p>
<p class="cssClassFeaturedBg cssClassFeaturedBg_${itemID}" ><a href="#">FEATURED</a></p>
</div>
<div class="cssClassProductListViewRight">
<h2>${name}</h2>
<p>{{html shortDescription}}</p>
<p class="cssClassListViewProductPrice">Price : <b><span class="cssClassFormatCurrency">${listPrice}</span></b> <span class="cssClassFormatCurrency">${price}</span></p>
<div class="cssClassViewDetailsAddtoCart"> 
<div class="cssClassButtonWrapper">
<a href="${AspxCommerceRoot}item/${sku}.aspx"><span>View Details</span></a>
</div>
<div class="cssClassAddtoCard_${itemID} cssClassAddtoCard">
<div class="cssClassButtonWrapper">
<a href="#" onclick="AddToCartToJSShoppingOption(${itemID},${price},${JSON2.stringify(sku)},${1});"><span>Add to Cart</span></a></div>
</div>
</div>
<div class="cssClassClear"></div>
</div>
</script>--%>

<%--<script id="scriptResultGrid2" type="text/x-jquery-tmpl">
 <div class="cssClassGrid2Box">
 <div class="cssClassGrid2BoxInfo">
 <h2><a href="${AspxCommerceRoot}item/${sku}.aspx">${name}</a></h2>
 <div class="cssClassGrid2Picture"><img  alt='${alternateText}' src='${imagePath}' title='${name}' />
 </div>
 <div class="cssClassGrid2PriceBox">
 <div class="cssClassGrid2Price">
 <p class="cssClassGrid2OffPrice">Price : <span class="cssClassFormatCurrency">${listPrice}</span> <span class="cssClassGrid2RealPrice"> <span class="cssClassFormatCurrency">${price}</span></span> </p>
 </div>
 <div class="cssClassAddtoCard_${itemID} cssClassAddtoCard">
 <div class="cssClassButtonWrapper">
<a href="#" onclick="AddToCartToJSShoppingOption(${itemID},${price},${JSON2.stringify(sku)},${1});"><span>Add to Cart</span></a>
 </div>
 </div>
<div class="cssClassclear"></div>
 </div>
 </div>
</script>--%>

<%--<script id="scriptResultGrid3" type="text/x-jquery-tmpl">
 <div class="cssClassGrid3Box">
 <div class="cssClassGrid3BoxInfo">
 <h2><a href="${AspxCommerceRoot}item/${sku}.aspx">${name}</a></h2>
 <div class="cssClassGrid3Picture"><img  alt='${alternateText}' src='${imagePath}' title='${name}' /></a></div>
 <div class="cssClassGrid3PriceBox">
 <div class="cssClassGrid3Price">
 <p class="cssClassGrid3OffPrice">Price : <span class="cssClassFormatCurrency">${listPrice}</span> <span class="cssClassGrid3RealPrice"> <span class="cssClassFormatCurrency">${price}</span></span> </p>
 </div>
 <div class="cssClassclear"></div>
 </div>
 </div>
</div>
</script>--%>

<%--<script id="scriptCompactList" type="text/x-jquery-tmpl">
            <tr>
                  <td><div class="cssClassProductPicture"><img  src='${imagePath}' alt='${alternateText}' title='${name}' /></div></td>
                  <td><p class="cssClassCLProductInfo"><a href="${AspxCommerceRoot}item/${sku}.aspx">${name}</a></p></td>
                  <td><p>${sku}</p></td>
                  <td><p class="cssClsssCLPrice"><span class="cssClassFormatCurrency">${price}</span></p></td>
                  <td>
                  <div class="cssClassAddtoCard_${itemID} cssClassAddtoCard">
                  <div class="cssClassButtonWrapper">
                         <a href="#" onclick="AddToCartToJSShoppingOption(${itemID},${price},${JSON2.stringify(sku)},${1});"><span>Add to Cart</span></a>
                      </div>
                    </div></td>
                </tr>             
</script>--%>

<%--<script id="scriptResultProductGrid" type="text/x-jquery-tmpl">
 <div class="cssClassProductsGridBox">
 <div class="cssClassProductsGridInfo">
 <h2><a href="${AspxCommerceRoot}item/${sku}.aspx">${name}</a></h2>
 <div class="cssClassProductsGridPicture"><img alt='${alternateText}' src='${imagePath}' title='${name}' /></div>
 <div class="cssClassProductsGridPriceBox">
 <div class="cssClassProductsGridPrice">
 <p class="cssClassProductsGridOffPrice">Price : <span class="cssClassFormatCurrency">${listPrice}</span> <span class="cssClassProductsGridRealPrice"> <span class="cssClassFormatCurrency">${price}</span></span> </p>
 </div>
 </div>
 <div class="cssClassButtonWrapper">
 <div class="cssClassWishListButton">
 <button onclick="AddToWishList(${itemID});" id="addWishList" type="button"><span><span>Add to Wishlist</span></span></button>
 </div>
 </div>
 <div class="cssClassAddtoCard_${itemID} cssClassAddtoCard">
 <div class="cssClassButtonWrapper"> 
<a href="#" onclick="AddToCartToJSShoppingOption(${itemID},${price},${JSON2.stringify(sku)},${1});"><span>Add to Cart</span></a> </div>
</div>
<div class="cssClassclear"></div>
</div>
 </div>
</script>--%>

<%--<script id="scriptResultListWithoutOptions" type="text/x-jquery-tmpl">
  <div class="cssClassListViewWithOutOptions">
  <div class="cssClassListViewWithOutOptionsLeft">
  <p class="cssClassProductPicture"><img  alt='${alternateText}' src='${imagePath}' title='${name}' /></p>
  </div>
  <div class="cssClassListViewWithOutOptionsRight">
  <h2><a href="${AspxCommerceRoot}item/${sku}.aspx">${name}</a></h2>
  <p class="cssClassProductCode">${sku}</p> 
  <p>{{html shortDescription}}</p> 
  <p class="cssClassListViewWithOutOptionsPrice">Price : <span class="cssClassFormatCurrency">${price}</span> <span class="cssClassListViewWithOutOptionsOffPrice"> <span class="cssClassFormatCurrency">${listPrice}</span></span> <span class="cssClassInstock_${itemID}">In stock</span></p> 
  <div class="cssClassAddtoCard_${itemID} cssClassAddtoCard">
  <div class="cssClassButtonWrapper">   
  <a href="#" onclick="AddToCartToJSShoppingOption(${itemID},${price},${JSON2.stringify(sku)},${1});"><span>Add to Cart</span></a> 
  </div> 
  </div>
   <div class="cssClassButtonWrapper cssClassWishListWithoutOption">
  <button type="button" id="addWishList" onclick="AddToWishList(${itemID});"><span><span>+ Add to Wishlist</span></span></button> 
  </div> 
  </div> 
  <div class="cssClassClear"></div> 
  </div> 
</script>--%>

