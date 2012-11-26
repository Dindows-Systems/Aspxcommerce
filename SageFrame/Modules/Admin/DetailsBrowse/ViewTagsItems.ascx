<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewTagsItems.ascx.cs"
    Inherits="Modules_Admin_DetailsBrowse_ViewTagsItems" %>

<script type="text/javascript">
    //<![CDATA[
     
    var ViewTagItem = "";
    $(function() {
        var tagsIDs = '<%=TagsIDs %>';
        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var customerId = AspxCommerce.utils.GetCustomerID();
        var ip = AspxCommerce.utils.GetClientIP();
        var countryName = AspxCommerce.utils.GetAspxClientCoutry();
        var sessionCode = AspxCommerce.utils.GetSessionCode();
        var arrTagAllItems = new Array();
        var arrTagAllItemToBind = new Array();
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


        ViewTagItem = {
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
                    type: ViewTagItem.config.type,
                    contentType: ViewTagItem.config.contentType,
                    cache: ViewTagItem.config.cache,
                    async: ViewTagItem.config.async,
                    url: ViewTagItem.config.url,
                    data: ViewTagItem.config.data,
                    dataType: ViewTagItem.config.dataType,
                    success: ViewTagItem.ajaxSuccess,
                    error: ViewTagItem.ajaxFailure
                });
            },
            ajaxSuccess: function(msg) {
                switch (ViewTagItem.config.ajaxCallMode) {
                    case 1:
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                                var displayOptions = "<option value=" + item.DisplayItemID + ">" + item.OptionType + "</option>"
                                $("#ddlViewTagItemAs").append(displayOptions);
                            });
                        }
                        ViewTagItem.ListTagsItems(1, $("#ddlTagItemPageSize").val(), 0);
                        break;
                    case 2:
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                                var displayOptions = "<option value=" + item.SortOptionTypeID + ">" + item.OptionType + "</option>"
                                $("#ddlSortTagItemBy").append(displayOptions);
                            });
                        }
                        break;
                    case 3:
                        var UserTaggedItemElements = '';
                        var itemIds = "";
                        var rowTotal = 0;
                        if (msg.d.length > 0) {
                            ViewTagItem.TagItemHideAll();
                            $("#divTagItemPageNumber").show();
                            $("#divTagItemViewOptions").show();
                            arrTagAllItems.length = 0;
                            $.each(msg.d, function(index, value) {
                                rowTotal = value.RowTotal;
                                if (itemIds.indexOf('' + value.ItemID + '') == -1) {
                                    arrTagAllItems.push(value);
                                    itemIds = itemIds + '' + value.ItemID + '' + ',';
                                }
                            });
                        }
                        else {
                            $("#divShowTagItemResult").html('no items');
                        }
                        var items_per_page = $('#ddlTagItemPageSize').val();
                        $("#Pagination").pagination(rowTotal, {
                            callback: ViewTagItem.pageselectCallback,
                            items_per_page: items_per_page,
                            //num_display_entries: 10,
                            current_page: currentpage,
                            callfunction: true,
                            function_name: { name: ViewTagItem.ListTagsItems, limit: $('#ddlTagItemPageSize').val() },
                            prev_text: "Prev",
                            next_text: "Next",
                            prev_show_always: false,
                            next_show_always: false

                        });
                        break;
                }
            },
            ajaxFailure: function() {
            }
        , LoadViewTagITemsStaticImage: function() {
            $('#imgLoaderViewTagItems').attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/ajax-loader.gif');
        }, TagItemHideAll: function() {
            $("#divTagItemPageNumber").hide();
            $("#divTagItemViewOptions").hide();
        }, BindItemsViewAsDropDown: function() {
            this.config.method = "AspxCommerceWebService.asmx/BindItemsViewAsList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = ({});
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);

        }, BindItemsSortByDropDown: function() {
            this.config.method = "AspxCommerceWebService.asmx/BindItemsSortByList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = ({});
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);

        }, BindResults: function() {
            var viewAsOption = $("#ddlViewTagItemAs").val();
            if (arrTagAllItemToBind.length > 0) {
                switch (viewAsOption) {
                    case '1':
                        ViewTagItem.GridView();
                        break;
                    case '2':
                        ViewTagItem.ListView();
                        break;
                    case '3':
                        ViewTagItem.Grid2View();
                        break;
                    case '4':
                        ViewTagItem.Grid3View();
                        break;
                    case '5':
                        ViewTagItem.CompactListView();
                        break;
                    case '6':
                        ViewTagItem.ProductGridView();
                        break;
                    case '7':
                        ViewTagItem.ListWithoutOptionsView();
                        break;
                }
            }
        }, ListTagsItems: function(offset, limit, currentpage1) {
            currentpage = currentpage1;
            ViewTagItem.config.method = "AspxCommerceWebService.asmx/GetUserTaggedItems";
            ViewTagItem.config.url = ViewTagItem.config.baseURL + ViewTagItem.config.method;
            ViewTagItem.config.data = JSON2.stringify({ offset: offset, limit: limit, tagIDs: tagsIDs, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
            ViewTagItem.config.ajaxCallMode = 3;
            ViewTagItem.ajaxCall(this.config);
        }
       , GridView: function() {
           $("#divShowTagItemResult").html('');
           $.each(arrTagAllItemToBind, function(index, value) {
               if (value.ImagePath == "") {
                   value.ImagePath = '<%=NoImageTagItemsPath %>';
               }
               else if (value.AlternateText == "") {
                   value.AlternateText = value.Name;
               }
               var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                   imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                   loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                   price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                   //                $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid.htm', function(template) {
                   //                    $.tmpl(template, items).appendTo("#divShowTagItemResult");
                   //                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                   //                });

                   $.tmpl("scriptResultGridTemp", items).appendTo("#divShowTagItemResult");
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
           }, ListView: function() {
               $("#divShowTagItemResult").html('');
               $.each(arrTagAllItemToBind, function(index, value) {
                   if (value.ImagePath == "") {
                       value.ImagePath = '<%=NoImageTagItemsPath %>';
                   }
                   else if (value.AlternateText == "") {
                       value.AlternateText = value.Name;
                   }


                   var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                       imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                       loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                       price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                       //                    $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultList.htm', function(template) {
                       //                        $.tmpl(template, items).appendTo("#divShowTagItemResult");
                       //                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                       //                    });

                       $.tmpl("scriptResultListTemp", items).appendTo("#divShowTagItemResult");
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
               }, Grid2View: function() {
                   $("#divShowTagItemResult").html('');
                   $.each(arrTagAllItemToBind, function(index, value) {
                       if (value.ImagePath == "") {
                           value.ImagePath = '<%=NoImageTagItemsPath %>';
                       }
                       else if (value.AlternateText == "") {
                           value.AlternateText = value.Name;
                       }
                       var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                           imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                           loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                           price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                           //                        $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid2.htm', function(template) {
                           //                            $.tmpl(template, items).appendTo("#divShowTagItemResult");
                           //                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                           //                        });

                           $.tmpl("scriptResultGrid2Temp", items).appendTo("#divShowTagItemResult");
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
                   }, Grid3View: function() {
                       $("#divShowTagItemResult").html('');
                       $.each(arrTagAllItemToBind, function(index, value) {
                           if (value.ImagePath == "") {
                               value.ImagePath = '<%=NoImageTagItemsPath %>';
                           }
                           else if (value.AlternateText == "") {
                               value.AlternateText = value.Name;
                           }
                           var items = [{ AspxCommerceRoot: AspxCommerce.utils.GetAspxRedirectPath(), itemID: value.ItemID, name: value.Name, sku: value.SKU,
                               imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                               loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                               price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                               //                            $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid3.htm', function(template) {
                               //                                $.tmpl(template, items).appendTo("#divShowTagItemResult");
                               //                                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                               //                            });

                               $.tmpl("scriptResultGrid3Temp", items).appendTo("#divShowTagItemResult");
                               $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                           });
                           $("img").lazyload();
                       }, CompactListView: function() {
                           $("#divShowTagItemResult").html('');
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
                           $("#divShowTagItemResult").html(CompactListViewElements);
                           $.each(arrTagAllItemToBind, function(index, value) {
                               if (value.ImagePath == "") {
                                   value.ImagePath = '<%=NoImageTagItemsPath %>';
                               }
                               else if (value.AlternateText == "") {
                                   value.AlternateText = value.Name;
                               }
                               var items = [{ itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                   imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'),
                                   loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif", alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
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
                           }, ProductGridView: function() {
                               $("#divShowTagItemResult").html('');
                               $.each(arrTagAllItemToBind, function(index, value) {
                                   if (value.ImagePath == "") {
                                       value.ImagePath = '<%=NoImageTagItemsPath %>';
                                   }
                                   else if (value.AlternateText == "") {
                                       value.AlternateText = value.Name;
                                   }
                                   var items = [{ itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                       imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                       loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                       price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                       //                                    $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultProductGrid.htm', function(template) {
                                       //                                        $.tmpl(template, items).appendTo("#divShowTagItemResult");
                                       //                                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                       //                                    });

                                       $.tmpl("scriptResultProductGridTemp", items).appendTo("#divShowTagItemResult");
                                       $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                                       if ('<%=AllowOutStockPurchase %>'.toLowerCase() == 'false') {
                                           if (value.IsOutOfStock) {
                                               $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                               $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                               $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                               $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                           }
                                       }
                                       if ('<%=AllowWishListViewTagsItems %>'.toLowerCase() != 'true') {
                                           $('.cssClassWishListButton').hide();
                                       }
                                   });
                                   $("img").lazyload();
                               }, ListWithoutOptionsView: function() {
                                   $("#divShowTagItemResult").html('');
                                   $.each(arrTagAllItemToBind, function(index, value) {
                                       if (value.ImagePath == "") {
                                           value.ImagePath = '<%=NoImageTagItemsPath %>';
                                       }

                                       else if (value.AlternateText == "") {
                                           value.AlternateText = value.Name;
                                       }
                                       var items = [{ itemID: value.ItemID, name: value.Name, sku: value.SKU,
                                           imagePath: AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small'), alternateText: value.AlternateText, listPrice: (value.ListPrice * rate),
                                           loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                           price: (value.Price * rate), shortDescription: Encoder.htmlDecode(value.ShortDescription)}];

                                           //                                        $.get(aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultListWithoutOptions.htm', function(template) {
                                           //                                            $.tmpl(template, items).appendTo("#divShowTagItemResult");
                                           //                                            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                           //                                        });

                                           $.tmpl("scriptResultListWithoutOptionsTemp", items).appendTo("#divShowTagItemResult");
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
                                           if ('<%=AllowWishListViewTagsItems %>'.toLowerCase() != 'true') {
                                               $('.cssClassWishListWithoutOption').hide();
                                           }
                                       });
                                   $("img").lazyload();
                                   }, pageselectCallback: function(page_index, jq, execute) {
                                       if (execute) {  // Get number of elements per pagionation page from form
                                           var items_per_page = $('#ddlTagItemPageSize').val();
                                           var sortByOption = $("#ddlSortTagItemBy").val();
                                           if (arrTagAllItems.length > 0) {
                                               switch (sortByOption) {
                                                   case '1': //Newest to oldest asc
                                                       arrTagAllItems.sort(sort_by('AddedOn', true));
                                                       // Sort by start_time
                                                       //arrItemListType.sort(sort_by('_AddedOn', false, function(a){return (new Date(a)).getTime()}));

                                                       break;
                                                   case '2': //Oldest to newest desc
                                                       arrTagAllItems.sort(sort_by('AddedOn', false));
                                                       break;
                                                   case '3': //Price highest to lowest asc
                                                       // Sort by price high to low
                                                       arrTagAllItems.sort(sort_by('Price', true, parseFloat));

                                                       //arrItemListType.sort(function(a, b) { return parseFloat(a.price) - parseFloat(b.price) });
                                                       break;
                                                   case '4': //Price lowest to highest desc
                                                       // Sort by price low to high
                                                       arrTagAllItems.sort(sort_by('Price', false, parseFloat));
                                                       //arrItemListType.sort(function(a, b) { return parseFloat(b.price) - parseFloat(a.price) });
                                                       break;
                                                   //                                            case '5':                                  
                                                   //                                                SortArray();                                  
                                                   //                                                break;                                 
                                               }
                                               //                                        $.each(arrItemListType, function(index, item) {
                                               //                                            alert(item.AddedOn);
                                               //                                        });


                                               var max_elem = arrTagAllItems.length;
                                               var newcontent = '';
                                               arrTagAllItemToBind.length = 0;

                                               // Iterate through a selection of the content and build an HTML string
                                               for (var i = 0; i < max_elem; i++) {
                                                   //newcontent += '<dt>' + arrItemListType[i]._Name + '</dt>';
                                                   arrTagAllItemToBind.push(arrTagAllItems[i]);
                                               }
                                               ViewTagItem.BindResults();
                                           }
                                       }
                                       // Replace old content with new content
                                       //$('#Searchresult').html(newcontent);

                                       // Prevent click event propagation
                                       return false;
                                   },
            Init: function() {
                LoadAllAspxTemplate();
                ViewTagItem.LoadViewTagITemsStaticImage();
                $(document).ajaxStart(function() {
                    $('#divAjaxLoader').show();
                });

                $(document).ajaxStop(function() {
                    $('#divAjaxLoader').hide();
                });
                ViewTagItem.TagItemHideAll();
                ViewTagItem.BindItemsViewAsDropDown();
                ViewTagItem.BindItemsSortByDropDown();

                $("#ddlViewTagItemAs").val(1);
                $("#ddlSortTagItemBy").val(1);
                $("#ddlViewTagItemAs").bind("change", function() {
                    ViewTagItem.BindResults();
                });
                $("#ddlSortTagItemBy").bind("change", function() {
                    // Create pagination element with options
                    var items_per_page = $('#ddlTagItemPageSize').val();
                    ViewTagItem.ListTagsItems(1, items_per_page, 0);
                });

                $("#ddlTagItemPageSize").bind("change", function() {
                    var items_per_page = $(this).val();
                    ViewTagItem.ListTagsItems(1, items_per_page, 0);
                });
            }
        }
        ViewTagItem.Init();

    });
   
              //]]>                                                       
    
</script>

<div id="divAjaxLoader">
    <img id="imgLoaderViewTagItems" src="" alt="loading...." title="loading...." />    
</div>
<div id="divTagItemViewOptions" class="viewWrapper">
    <div id="divViewAs" class="view">
        View as:
        <select id="ddlViewTagItemAs" class="cssClassDropDown">
        </select>
    </div>
    <div id="divSortBy" class="sort">
        Sort by:
        <select id="ddlSortTagItemBy" class="cssClassDropDown">
        </select>
    </div>
</div>
<div id="divShowTagItemResult" class="cssClassDisplayResult">
</div>
<div class="cssClassClear">
</div>
<div class="cssClassPageNumber" id="divTagItemPageNumber">
    <div class="cssClassPageNumberLeftBg">
        <div class="cssClassPageNumberRightBg">
            <div class="cssClassPageNumberMidBg">
                <div id="Pagination">
                </div>
                <div class="cssClassViewPerPage">
                    View Per Page
                    <select id="ddlTagItemPageSize" class="cssClassDropDown">
                        <option value="5">5</option>
                        <option value="10">10</option>
                        <option value="15">15</option>
                        <option value="20">20</option>
                        <option value="25">25</option>
                        <option value="40">40</option>
                    </select></div>
                <%--<table width="84%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="40">
                            <a href="#">Prev</a>
                        </td>
                        <td>
                            <span>1</span> <a href="#">2</a> <a href="#">3</a> <a href="#">4</a> <a href="#">5</a>
                            <a href="#">6</a> <a href="#">7</a> <a href="#">8</a> <a href="#">9</a> <a href="#">
                                10</a> <a href="#">11</a> <a href="#">12</a> <a href="#">13</a> <a href="#">14</a>
                            <a href="#">15</a> <a href="#">16</a> <a href="#">17</a> <a href="#">18</a> <a href="#">
                                19</a> <a href="#">20</a>
                        </td>
                        <td width="40">
                            <a href="#">Next</a>
                        </td>
                    </tr>
                </table>--%>
            </div>
        </div>
    </div>
</div>

<%--<script id="scriptResultGrid" type="text/x-jquery-tmpl">
  <div class="cssClassProductsBox"> 
   <div class="cssClassProductsBoxInfo"> 
   <h2>${name}</h2> 
    <h3>${sku}</h3> 
    <div class="cssClassProductPicture"><img src='${imagePath}'  alt='${alternateText}'  title='${name}'></div> 
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
      <a href="#" onclick="AddToCartToJSViewTagItems(${itemID},${price},${JSON2.stringify(sku)},${1});"><span>Add to Cart</span></a>
</div></div>  
     </div>  
</script>--%>

<%--<script id="scriptResultList" type="text/x-jquery-tmpl">
<div id="divProductListView_${itemID}" class="cssClassProductListView">
<div class="cssClassProductListViewLeft">
<p class="cssClassProductPicture"><img  src=${imagePath} alt='${alternateText}' title='${name}' /></p>
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
<a href="#" onclick="AddToCartToJSViewTagItems(${itemID},${price},${JSON2.stringify(sku)},${1});"><span>Add to Cart</span></a></div>
</div>
</div>
<div class="cssClassClear"></div>
</div>
</script>--%>

<%--<script id="scriptResultGrid2" type="text/x-jquery-tmpl">
 <div class="cssClassGrid2Box">
 <div class="cssClassGrid2BoxInfo">
 <h2><a href="${AspxCommerceRoot}item/${sku}.aspx">${name}</a></h2>
 <div class="cssClassGrid2Picture"><img src=${imagePath} alt='${alternateText}' title='${name}' />
 </div>
 <div class="cssClassGrid2PriceBox">
 <div class="cssClassGrid2Price">
 <p class="cssClassGrid2OffPrice">Price : <span class="cssClassFormatCurrency">${listPrice}</span> <span class="cssClassGrid2RealPrice"> <span class="cssClassFormatCurrency">${price}</span></span> </p>
 </div>
 <div class="cssClassAddtoCard_${itemID} cssClassAddtoCard">
 <div class="cssClassButtonWrapper"> 
<a href="#" onclick="AddToCartToJSViewTagItems(${itemID},${price},${JSON2.stringify(sku)},${1});"><span>Add to Cart</span></a>
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
 <div class="cssClassGrid3Picture"><img src=${imagePath} alt='${alternateText}' title='${name}' /></a></div>
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
                  <td><div class="cssClassProductPicture"><img src='${imagePath}'  alt='${alternateText}' title='${name}' /></div></td>
                  <td><p class="cssClassCLProductInfo"><a href="${AspxCommerceRoot}item/${sku}.aspx">${name}</a></p></td>
                  <td><p>${sku}</p></td>
                  <td><p class="cssClsssCLPrice"><span class="cssClassFormatCurrency">${price}</span></p></td>
                  <td>
                  <div class="cssClassAddtoCard_${itemID} cssClassAddtoCard">
                  <div class="cssClassButtonWrapper">
                         <a href="#" onclick="AddToCartToJSViewTagItems(${itemID},${price},${JSON2.stringify(sku)},${1});"><span> Add to Cart </span></a>
                      </div>
                    </div></td>
                </tr>             
</script>--%>

<%--<script id="scriptResultProductGrid" type="text/x-jquery-tmpl">
 <div class="cssClassProductsGridBox">
 <div class="cssClassProductsGridInfo">
 <h2><a href="${AspxCommerceRoot}item/${sku}.aspx">${name}</a></h2>
 <div class="cssClassProductsGridPicture"><img src='${imagePath}'  alt='${alternateText}' title='${name}' /></div>
 <div class="cssClassProductsGridPriceBox">
 <div class="cssClassProductsGridPrice">
 <p class="cssClassProductsGridOffPrice">Price : <span class="cssClassFormatCurrency">${listPrice}</span> <span class="cssClassProductsGridRealPrice"> <span class="cssClassFormatCurrency">${price}</span></span> </p>
 </div>
 </div>
 <div class="cssClassButtonWrapper">
 <div class="cssClassWishListButton">
 <button onclick="AddToWishList(${itemID});" id="addWishList" type="button"><span>+ Add to Wishlist</span></button>
 </div>
 </div>
 <div class="cssClassAddtoCard_${itemID} cssClassAddtoCard">
 <div class="cssClassButtonWrapper"> 
<a href="#" onclick="AddToCartToJSViewTagItems(${itemID},${price},${JSON2.stringify(sku)},${1});"><span>Add to Cart</span></a> </div>
</div>
<div class="cssClassclear"></div>
</div>
 </div>
</script>--%>

<%--<script id="scriptResultListWithoutOptions" type="text/x-jquery-tmpl">
  <div class="cssClassListViewWithOutOptions">
  <div class="cssClassListViewWithOutOptionsLeft">
  <p class="cssClassProductPicture"><img src='${imagePath}'  alt='${alternateText}' title='${name}' /></p>
  </div>
  <div class="cssClassListViewWithOutOptionsRight">
  <h2><a href="${AspxCommerceRoot}item/${sku}.aspx">${name}</a></h2>
  <p class="cssClassProductCode">${sku}</p> 
  <p>{{html shortDescription}}</p> 
  <p class="cssClassListViewWithOutOptionsPrice">Price : <span class="cssClassFormatCurrency">${price}</span> <span class="cssClassListViewWithOutOptionsOffPrice"> <span class="cssClassFormatCurrency">${listPrice}</span></span> <span class="cssClassInstock_${itemID}">In stock</span></p> 
  <div class="cssClassAddtoCard_${itemID} cssClassAddtoCard">
  <div class="cssClassButtonWrapper">   
  <a href="#" onclick="AddToCartToJSViewTagItems(${itemID},${price},${JSON2.stringify(sku)},${1});"><span>Add to Cart</span></a> 
   </div> 
  </div>
   <div class="cssClassButtonWrapper cssClassWishListWithoutOption">
  <button type="button" id="addWishList" onclick="AddToWishList(${itemID});"><span><span>+ Add to Wishlist</span></span></button> 
  </div> 
  </div> 
  <div class="cssClassClear"></div> 
  </div> 
</script>--%>

