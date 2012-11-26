<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemLists.ascx.cs" Inherits="Modules_Admin_DetailsBrowse_ItemLists" %>

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
        var searchText = '<%=SearchText%>';
        var categoryID = '<%=CategoryID %>';
        var arrItemSimpleSearch = new Array();
        var arrSearchResultToBind = new Array();
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
                        $("#divShowSimpleSearchResult").html('');
                        arrItemSimpleSearch.length = 0;
                        $("#divShowSimpleSearchResult").show();
                        // $("#ddlSimpleViewAs").val(1);
                        //  $("#ddlSimpleSortBy").val(1);
                        if (msg.d.length > 0) {
                            $("#divShowSimpleSearchResult").html('');
                            $("#divSimpleSearchItemViewOptions").show();
                            $("#divSimpleSearchPageNumber").show();
                            $("#divSimpleSearchViewAs").val(1);
                            $.each(msg.d, function(index, item) {
                                rowTotal = item.RowTotal;
                                arrItemSimpleSearch.push(item);
                            });
                            // Create SimpleSearchPagination element with options from form
                            var items_per_page = $('#ddlSimpleSearchPageSize').val();
                            $("#SimpleSearchPagination").pagination(rowTotal, {
                                callback: ItemList.pageselectCallback,
                                items_per_page: items_per_page,
                                //num_display_entries: 10,
                                current_page: ItemList.vars.currentpage,
                                callfunction: true,
                                function_name: { name: ItemList.BindSimpleSearchResultItems, limit: $('#ddlSimpleSearchPageSize').val() },
                                prev_text: "Prev",
                                next_text: "Next",
                                prev_show_always: false,
                                next_show_always: false

                            });
                            $("img").lazyload();
                            //ItemList.BindSimpleSearchResults();
                        }
                        else {
                            $("#divSimpleSearchItemViewOptions").hide();
                            $("#divSimpleSearchPageNumber").hide();
                            $("#divShowSimpleSearchResult").html("No items found!");
                        }
                        break;
                    case 2:
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                                var displayOptions = "<option value=" + item.DisplayItemID + ">" + item.OptionType + "</option>"
                                $("#ddlSimpleViewAs").append(displayOptions);
                            });
                            ItemList.BindSimpleSearchResultItems(1, $("#ddlSimpleSearchPageSize").val(), 0);

                        }
                        break;
                    case 3:
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                                var displayOptions = "<option value=" + item.SortOptionTypeID + ">" + item.OptionType + "</option>"
                                $("#ddlSimpleSortBy").append(displayOptions);
                            });
                        }
                        break;
                }
            },
            ajaxFailure: function() {
                switch (ItemList.config.error) {
                    case 1:
                        csscody.error("<h2>Error Message</h2><p>Failed to load search result!</p>");
                        break;
                    case 4:
                        csscody.error("<h2>Error Message</h2><p>Failded to save search term!</p>");
                        break;
                }
            },
            vars: {
                currentpage: ""
            }, BindSimpleSearchResultItems: function(offset, limit, currentpage) {
                ItemList.vars.currentpage = currentpage;
                ItemList.config.method = "AspxCommerceWebService.asmx/GetSimpleSearchResult";
                ItemList.config.url = ItemList.config.baseURL + ItemList.config.method;
                ItemList.config.data = JSON2.stringify({ offset: offset, limit: limit, categoryID: categoryID, searchText: searchText, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
                ItemList.config.ajaxCallMode = 1;
                ItemList.config.error = 1;
                ItemList.ajaxCall(ItemList.config);

            }, SimpleSearchHideAll: function() {
                $("#divSimpleSearchItemViewOptions").hide();
                $("#divSimpleSearchPageNumber").hide();
                $("#divShowSimpleSearchResult").hide();
            }, BindSimpleSearchResults: function() {
                var viewAsOption = $("#ddlSimpleViewAs").val();
                if (arrSearchResultToBind.length > 0) {
                    switch (viewAsOption) {
                        case '1':
                            ItemList.SimpleSearchGridView();
                            break;
                        case '2':
                            ItemList.SimpleSearchListView();
                            break;
                        case '3':
                            ItemList.SimpleSearchGrid2View();
                            break;
                        case '4':
                            ItemList.SimpleSearchGrid3View();
                            break;
                        case '5':
                            ItemList.SimpleSearchCompactListView();
                            break;
                        case '6':
                            ItemList.SimpleSearchProductGridView();
                            break;
                        case '7':
                            ItemList.SimpleSearchListWithoutOptionsView();
                            break;
                    }
                }
            }, BindItemsSimpleViewAsDropDown: function() {
                this.config.method = "AspxCommerceWebService.asmx/BindItemsViewAsList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = ({});
                this.config.ajaxCallMode = 2;
                this.config.error = 2;
                this.ajaxCall(this.config);

            }, BindItemsSimpleSortByDropDown: function() {
                this.config.method = "AspxCommerceWebService.asmx/BindItemsSortByList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = ({});
                this.config.ajaxCallMode = 3;
                this.config.error = 3;
                this.ajaxCall(this.config);

            }, SimpleSearchGridView: function() {
                $("#divShowSimpleSearchResult").html('');
                $.each(arrSearchResultToBind, function(index, value) {
                    if (value.ImagePath == "") {
                        value.ImagePath = '<%=NoImageItemListPath %>';
                    }
                    else if (value.AlternateText == "") {
                        value.AlternateText = value.Name;
                    }
                    var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                        imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                        loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                        price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                        //                $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid.htm', function(template) {
                        //                    $.tmpl(template, items).appendTo("#divShowSimpleSearchResult");
                        //                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        //                });

                        $.tmpl("scriptResultGridTemp", items).appendTo("#divShowSimpleSearchResult");
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
                    $("img").lazyload();

                }, SimpleSearchListView: function() {
                    $("#divShowSimpleSearchResult").html('');
                    $.each(arrSearchResultToBind, function(index, value) {
                        if (value.ImagePath == "") {
                            value.ImagePath = '<%=NoImageItemListPath %>';
                        }
                        else if (value.AlternateText == "") {
                            value.AlternateText = value.Name;
                        }

                        var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                            imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                            loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                            price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                            //                    $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultList.htm', function(template) {
                            //                        $.tmpl(template, items).appendTo("#divShowSimpleSearchResult");
                            //                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                            //                    });

                            $.tmpl("scriptResultListTemp", items).appendTo("#divShowSimpleSearchResult");
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
                        $("img").lazyload();
                    }, SimpleSearchGrid2View: function() {
                        $("#divShowSimpleSearchResult").html('');
                        $.each(arrSearchResultToBind, function(index, value) {
                            if (value.ImagePath == "") {
                                value.ImagePath = '<%=NoImageItemListPath %>';
                            }
                            else if (value.AlternateText == "") {
                                value.AlternateText = value.Name;
                            }
                            var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                //                        $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid2.htm', function(template) {
                                //                            $.tmpl(template, items).appendTo("#divShowSimpleSearchResult");
                                //                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                //                        });

                                $.tmpl("scriptResultGrid2Temp", items).appendTo("#divShowSimpleSearchResult");
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
                            $("img").lazyload();

                        }, SimpleSearchGrid3View: function() {
                            $("#divShowSimpleSearchResult").html('');
                            $.each(arrSearchResultToBind, function(index, value) {
                                if (value.ImagePath == "") {
                                    value.ImagePath = '<%=NoImageItemListPath %>';
                                }
                                else if (value.AlternateText == "") {
                                    value.AlternateText = value.Name;
                                }
                                var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                    imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                    loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                    price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                    //                            $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid3.htm', function(template) {
                                    //                                $.tmpl(template, items).appendTo("#divShowSimpleSearchResult");
                                    //                                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                    //                            });

                                    $.tmpl("scriptResultGrid3Temp", items).appendTo("#divShowSimpleSearchResult");
                                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                });
                                $("img").lazyload();

                            }, SimpleSearchCompactListView: function() {
                                $("#divShowSimpleSearchResult").html('');
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
                                $("#divShowSimpleSearchResult").html(CompactListViewElements);
                                $.each(arrSearchResultToBind, function(index, value) {
                                    if (value.ImagePath == "") {
                                        value.ImagePath = '<%=NoImageItemListPath %>';
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
                                    $("img").lazyload();

                                }, SimpleSearchProductGridView: function() {
                                    $("#divShowSimpleSearchResult").html('');
                                    $.each(arrSearchResultToBind, function(index, value) {
                                        if (value.ImagePath == "") {
                                            value.ImagePath = '<%=NoImageItemListPath %>';
                                        }
                                        else if (value.AlternateText == "") {
                                            value.AlternateText = value.Name;
                                        }
                                        var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                            imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                            loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                            price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                            //                                    $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultProductGrid.htm', function(template) {
                                            //                                        $.tmpl(template, items).appendTo("#divShowSimpleSearchResult");
                                            //                                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                            //                                    });

                                            $.tmpl("scriptResultProductGridTemp", items).appendTo("#divShowSimpleSearchResult");
                                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                                            if ('<%=AllowOutStockPurchase %>'.toLowerCase() == 'false') {
                                                if (value.IsOutOfStock) {
                                                    $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                                    $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                                    $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                                    $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                                }
                                            }
                                            if ('<%=AllowWishListItemList %>'.toLowerCase() != 'true') {
                                                $('.cssClassWishListButton').hide();
                                            }
                                        });
                                        $("img").lazyload();

                                    }, SimpleSearchListWithoutOptionsView: function() {
                                        $("#divShowSimpleSearchResult").html('');
                                        $.each(arrSearchResultToBind, function(index, value) {
                                            if (value.ImagePath == "") {
                                                value.ImagePath = '<%=NoImageItemListPath %>';
                                            }

                                            else if (value.AlternateText == "") {
                                                value.AlternateText = value.Name;
                                            }
                                            var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                                imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                                loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                                price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                                //                                        $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultListWithoutOptions.htm', function(template) {
                                                //                                            $.tmpl(template, items).appendTo("#divShowSimpleSearchResult");
                                                //                                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                                //                                        });

                                                $.tmpl("scriptResultListWithoutOptionsTemp", items).appendTo("#divShowSimpleSearchResult");
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
                                                if ('<%=AllowWishListItemList %>'.toLowerCase() != 'true') {
                                                    $('.cssClassWishListWithoutOption').hide();
                                                }
                                            });
                                            $("img").lazyload();

                                        }, pageselectCallback: function(page_index, jq, execute) {
                                            if (execute) {
                                                // Get number of elements per pagionation page from form
                                                var items_per_page = $('#ddlSimpleSearchPageSize').val();
                                                var sortByOption = $("#ddlSimpleSortBy").val();
                                                if (arrItemSimpleSearch.length > 0) {
                                                    switch (sortByOption) {
                                                        case '1': //Newest to oldest asc
                                                            arrItemSimpleSearch.sort(sort_by('AddedOn', true));
                                                            // Sort by start_time
                                                            //arrItemSimpleSearch.sort(sort_by('_AddedOn', false, function(a){return (new Date(a)).getTime()}));

                                                            break;
                                                        case '2': //Oldest to newest desc
                                                            arrItemSimpleSearch.sort(sort_by('AddedOn', false));
                                                            break;
                                                        case '3': //Price highest to lowest asc
                                                            // Sort by price high to low
                                                            arrItemSimpleSearch.sort(sort_by('Price', true, parseFloat));

                                                            //arrItemSimpleSearch.sort(function(a, b) { return parseFloat(a.price) - parseFloat(b.price) });
                                                            break;
                                                        case '4': //Price lowest to highest desc
                                                            // Sort by price low to high
                                                            arrItemSimpleSearch.sort(sort_by('Price', false, parseFloat));
                                                            //arrItemSimpleSearch.sort(function(a, b) { return parseFloat(b.price) - parseFloat(a.price) });
                                                            break;
                                                        //                                            case '5':                                     
                                                        //                                                SortArray();                                     
                                                        //                                                break;                                    
                                                    }
                                                    //                                        $.each(arrItemSimpleSearch, function(index, item) {
                                                    //                                            alert(item.AddedOn);
                                                    //                                        });


                                                    var max_elem = arrItemSimpleSearch.length;
                                                    var newcontent = '';
                                                    arrSearchResultToBind.length = 0;

                                                    // Iterate through a selection of the content and build an HTML string                                           
                                                    for (var i = 0; i < max_elem; i++) {
                                                        //newcontent += '<dt>' + arrItemSimpleSearch[i]._Name + '</dt>';
                                                        arrSearchResultToBind.push(arrItemSimpleSearch[i]);
                                                    }

                                                    ItemList.BindSimpleSearchResults();
                                                }
                                            }

                                            // Replace old content with new content
                                            //$('#Searchresult').html(newcontent);

                                            // Prevent click event propagation
                                            return false;


                                        }, getOptionsFromForm: function() {
                                            var opt = { callback: pageselectCallback };
                                            //parseInt(
                                            opt.items_per_page = $('#ddlSimpleSearchPageSize').val();
                                            //opt.num_display_entries = 10;
                                            //opt.current_page = 0;

                                            opt.prev_text = "Prevs";
                                            opt.next_text = "Nexts";
                                            //opt.prev_show_always = false;
                                            //opt.next_show_always = false;
                                            return opt;
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

                                        }, Init: function() {
                                            LoadAllAspxTemplate();
                                            ItemList.AddUpdateSearchTerm();
                                            ItemList.SimpleSearchHideAll();
                                            ItemList.BindItemsSimpleViewAsDropDown();
                                            ItemList.BindItemsSimpleSortByDropDown();

                                            $("#ddlSimpleViewAs").change(function() {
                                                ItemList.BindSimpleSearchResults();
                                            });

                                            $("#ddlSimpleSortBy").change(function() {
                                                // Create SimpleSearchPagination element with options
                                                var items_per_page = $('#ddlSimpleSearchPageSize').val();
                                                var offset = 1;
                                                ItemList.BindSimpleSearchResultItems(offset, items_per_page, 0);
                                            });

                                            $("#ddlSimpleSearchPageSize").change(function() {
                                                var items_per_page = $(this).val();
                                                var offset = 1;
                                                ItemList.BindSimpleSearchResultItems(offset, items_per_page, 0);
                                            });

                                        }
                                    }
                                    ItemList.Init();

                                });

  
  //]]>                              
</script>

<div id="divSimpleSearchItemViewOptions" class="viewWrapper">
    <div id="divSimpleSearchViewAs" class="view">
        View as:
        <select id="ddlSimpleViewAs" class="cssClassDropDown">
        </select>
    </div>
    <div id="divSortBy" class="sort">
        Sort by:
        <select id="ddlSimpleSortBy" class="cssClassDropDown">
        </select>
    </div>
</div>
<div id="divShowSimpleSearchResult" class="cssClassDisplayResult">
</div>
<div class="cssClassClear">
</div>
<!-- TODO:: paging Here -->
<div class="cssClassPageNumber" id="divSimpleSearchPageNumber">
    <div class="cssClassPageNumberLeftBg">
        <div class="cssClassPageNumberRightBg">
            <div class="cssClassPageNumberMidBg">
                <div id="SimpleSearchPagination">
                </div>
                <div class="cssClassViewPerPage">
                    View Per Page
                    <select id="ddlSimpleSearchPageSize" class="cssClassDropDown">
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


