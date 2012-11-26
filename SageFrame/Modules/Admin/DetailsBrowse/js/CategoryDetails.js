 var categoryDetails = '';

 $(function() {
     fixedEncodeURIComponent = function(str) {
         return encodeURIComponent(str).replace(/!/g, '%21').replace(/'/g, '%27').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/-/g, '_').replace(/\*/g, '%2A').replace(/%26/g, 'ampersand').replace(/%20/g, '-');
         // return encodeURIComponent(str).replace(/!/g, '%21').replace(/'/g, '%27').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/\*/g, '%2A').replace(/%20/g, '+');.replace(/-/g, '')
     };
     var storeId = AspxCommerce.utils.GetStoreID();
     var portalId = AspxCommerce.utils.GetPortalID();
     var userName = AspxCommerce.utils.GetUserName();
     var cultureName = AspxCommerce.utils.GetCultureName();
     var customerId = AspxCommerce.utils.GetCustomerID();
     var ip = AspxCommerce.utils.GetClientIP();
     var countryName = AspxCommerce.utils.GetAspxClientCoutry();
     var sessionCode = AspxCommerce.utils.GetSessionCode();
     var categorykey = CategoryKey;
     var arrItemListType = new Array();
     var arrResultToBind = new Array();
     var rowTotal = 0;
     var currentPage = 0;
     var catNames = [];
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
         };
     };
     IsExistedCategory = function(arr,cat) {
         var isExist = false;
         for (var i = 0; i < arr.length; i++) {
             if (this[i] == cat) {
                 isExist = true;
                 break;
             }
         }
         return isExist;
     };

     categoryDetails = {
         config: {
             isPostBack: false,
             async: false,
             cache: false,
             type: 'POST',
             contentType: "application/json; charset=utf-8",
             data: '{}',
             dataType: 'json',
             baseURL: aspxservicePath + "AspxCommerceWebService.asmx/",
             method: "",
             url: "",
             ajaxCallMode: 0
         },
         ajaxCall: function(config) {
             $.ajax({
                 type: categoryDetails.config.type,
                 contentType: categoryDetails.config.contentType,
                 cache: categoryDetails.config.cache,
                 async: categoryDetails.config.async,
                 url: categoryDetails.config.url,
                 data: categoryDetails.config.data,
                 dataType: categoryDetails.config.dataType,
                 success: categoryDetails.ajaxSuccess,
                 error: categoryDetails.ajaxFailure
             });
         },
         LoadCategoryDetailStaticImage: function() {
             $('#imgLoader').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
         },
         CategoryListingHideAll: function() {
             $("#divItemViewOptions").hide();
             $("#divSearchPageNumber").hide();
             //  $("#divShowGeneralSearchResult").hide();
         },
         GridView: function() {
             $("#divShowCategoryItemsList").html('');
             var itemIds = [];
             $.each(arrResultToBind, function(index, value) {
                 if (!IsExistedCategory(itemIds, value.ItemID )) {
                     itemIds.push(value.ItemID);
                     if (value.BaseImage == "") {
                         value.BaseImage = noImageCategoryDetailPath;
                     }
                     var items = [{
                         itemID: value.ItemID,
                         name: value.Name,
                         AspxCommerceRoot: aspxRedirectPath,
                         sku: value.SKU,
                         imagePath: aspxRootPath + value.BaseImage.replace('uploads', 'uploads/Small'),
                         loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                         alternateText: value.Name,
                         listPrice: (value.ListPrice * rate),
                         price: (value.Price * rate),
                         shortDescription: Encoder.htmlDecode(value.ShortDescription)
}];

                         $.tmpl("scriptResultGridTemp", items).appendTo("#divShowCategoryItemsList");
                         $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                         if (allowOutStockPurchase.toLowerCase() == 'false') {
                             if (value.IsOutOfStock) {
                                 $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                 $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                 $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                 $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                             }
                         }
                     }
                 });
                 $("img").lazyload();
             },

             ListView: function() {
                 $("#divShowCategoryItemsList").html('');
                 var itemIds =[];
                 $.each(arrResultToBind, function(index, value) {
                 if (!IsExistedCategory(itemIds, value.ItemID)) {
                     itemIds.push(value.ItemID);
                         if (value.BaseImage == "") {
                             value.BaseImage = noImageCategoryDetailPath;
                         }
                         var items = [{
                             itemID: value.ItemID,
                             name: value.Name,
                             AspxCommerceRoot: aspxRedirectPath,
                             sku: value.SKU,
                             imagePath: aspxRootPath + value.BaseImage.replace('uploads', 'uploads/Small'),
                             loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                             alternateText: value.Name,
                             listPrice: (value.ListPrice * rate),
                             price: (value.Price * rate),
                             shortDescription: Encoder.htmlDecode(value.ShortDescription)
}];

                             $.tmpl("scriptResultListTemp", items).appendTo("#divShowCategoryItemsList");
                             $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                             if (value.IsFeatured.toLowerCase() == 'no') {
                                 $(".cssClassFeaturedBg_" + value.ItemID).hide();
                             }
                             if (allowOutStockPurchase.toLowerCase() == 'false') {
                                 if (value.IsOutOfStock) {
                                     $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                     $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                     $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                     $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                 }
                             }
                         }
                     });
                     $("img").lazyload();
                 },

                 Grid2View: function() {
                     $("#divShowCategoryItemsList").html('');
                     var itemIds = [];
                     $.each(arrResultToBind, function(index, value) {

                     if (!IsExistedCategory(itemIds, value.ItemID)) {
                         itemIds.push(value.ItemID);
                             if (value.BaseImage == "") {
                                 value.BaseImage = noImageCategoryDetailPath;
                             }
                             var items = [{
                                 itemID: value.ItemID,
                                 name: value.Name,
                                 AspxCommerceRoot: aspxRedirectPath,
                                 sku: value.SKU,
                                 imagePath: aspxRootPath + value.BaseImage.replace('uploads', 'uploads/Small'),
                                 loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                 alternateText: value.Name,
                                 listPrice: (value.ListPrice * rate),
                                 price: (value.Price * rate),
                                 shortDescription: Encoder.htmlDecode(value.ShortDescription)
}];

                                 $.tmpl("scriptResultGrid2Temp", items).appendTo("#divShowCategoryItemsList");
                                 $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                                 if (allowOutStockPurchase.toLowerCase() == 'false') {
                                     if (value.IsOutOfStock) {
                                         $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                         $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                         $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                         $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                     }
                                 }
                             }
                         });
                         $("img").lazyload();
                     },

                     Grid3View: function() {
                         $("#divShowCategoryItemsList").html('');
                         var itemIds = [];
                         $.each(arrResultToBind, function(index, value) {
                         if (!IsExistedCategory(itemIds, value.ItemID)) {
                             itemIds.push(value.ItemID);
                                 if (value.BaseImage == "") {
                                     value.BaseImage = noImageCategoryDetailPath;
                                 }
                                 var items = [{
                                     itemID: value.ItemID,
                                     name: value.Name,
                                     AspxCommerceRoot: aspxRedirectPath,
                                     AspxCommerceRoot: aspxRedirectPath,
                                     sku: value.SKU,
                                     imagePath: aspxRootPath + value.BaseImage.replace('uploads', 'uploads/Small'),
                                     loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                     alternateText: value.Name,
                                     listPrice: (value.ListPrice * rate),
                                     price: (value.Price * rate),
                                     shortDescription: Encoder.htmlDecode(value.ShortDescription)
}];

                                     $.tmpl("scriptResultGrid3Temp", items).appendTo("#divShowCategoryItemsList");
                                     $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                                 }
                             });
                             $("img").lazyload();
                         },

                         CompactListView: function() {
                             $("#divShowCategoryItemsList").html('');
                             var itemIds = [];
                             var CompactListViewElements = '';
                             CompactListViewElements += '<div class="cssClassCompactList">';
                             CompactListViewElements += '<table width="100%" cellspacing="0" id="tblCompactList" cellpadding="0" border="0">';
                             CompactListViewElements += '<thead><tr class="cssClassHeadeTitle">';
                             CompactListViewElements += '<td class="cssClassCLPicture">&nbsp;</td>';
                             CompactListViewElements += '<td class="cssClassCLProduct">Item</td>';
                             CompactListViewElements += '<td class="cssClassCLProductCode">SKU code </td>';
                             CompactListViewElements += '<td class="cssClassCLPrice">Price</td>';
                             CompactListViewElements += '<td class="cssClassCLAddtoCart">&nbsp;</td>';
                             CompactListViewElements += '</tr></thead><tbody><div>';
                             CompactListViewElements += '</div></tbody></table></div>';
                             $("#divShowCategoryItemsList").html(CompactListViewElements);

                             $.each(arrResultToBind, function(index, value) {
                             if (!IsExistedCategory(itemIds, value.ItemID)) {
                                 itemIds.push(value.ItemID);
                                     if (value.BaseImage == "") {
                                         value.BaseImage = noImageCategoryDetailPath;
                                     }
                                     var items = [{
                                         itemID: value.ItemID,
                                         name: value.Name,
                                         AspxCommerceRoot: aspxRedirectPath,
                                         sku: value.SKU,
                                         imagePath: aspxRootPath + value.BaseImage.replace('uploads', 'uploads/Small'),
                                         loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                         alternateText: value.Name,
                                         listPrice: (value.ListPrice * rate),
                                         price: (value.Price * rate),
                                         shortDescription: Encoder.htmlDecode(value.ShortDescription)
}];

                                         $.tmpl("scriptCompactListTemp", items).appendTo("#tblCompactList");
                                         $("#tblCompactList tr:even").addClass("cssClassAlternativeEven");
                                         $("#tblCompactList tr:odd").addClass("cssClassAlternativeOdd");
                                         $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                                         if (allowOutStockPurchase.toLowerCase() == 'false') {
                                             if (value.IsOutOfStock) {
                                                 $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                                 $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                                 $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                                 $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                             }
                                         }
                                     }
                                 });
                                 $("img").lazyload();
                             },

                             ProductGridView: function() {
                                 $("#divShowCategoryItemsList").html('');
                                 var itemIds = [];
                                 $.each(arrResultToBind, function(index, value) {

                                 if (!IsExistedCategory(itemIds, value.ItemID)) {
                                     itemIds.push(value.ItemID);
                                         if (value.BaseImage == "") {
                                             value.BaseImage = noImageCategoryDetailPath;
                                         }
                                         var items = [{
                                             itemID: value.ItemID,
                                             name: value.Name,
                                             AspxCommerceRoot: aspxRedirectPath,
                                             sku: value.SKU,
                                             imagePath: aspxRootPath + value.BaseImage.replace('uploads', 'uploads/Small'),
                                             loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                             alternateText: value.Name,
                                             listPrice: (value.ListPrice * rate),
                                             price: (value.Price * rate),
                                             shortDescription: Encoder.htmlDecode(value.ShortDescription)
}];

                                             $.tmpl("scriptResultProductGridTemp", items).appendTo("#divShowCategoryItemsList");
                                             $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                                             if (allowOutStockPurchase.toLowerCase() == 'false') {
                                                 if (value.IsOutOfStock) {
                                                     $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                                     $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                                     $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                                     $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                                 }
                                             }
                                             if (allowWishListCategory.toLowerCase() != 'true') {
                                                 $('.cssClassWishListButton').hide();
                                             }
                                         }
                                     });
                                     $("img").lazyload();
                                 },

                                 ListWithoutOptionsView: function() {
                                     $("#divShowCategoryItemsList").html('');
                                     var itemIds = [];
                                     $.each(arrResultToBind, function(index, value) {
                                     if (!IsExistedCategory(itemIds, value.ItemID)) {
                                         itemIds.push(value.ItemID);
                                             if (value.BaseImage == "") {
                                                 value.BaseImage = noImageCategoryDetailPath;
                                             }
                                             var items = [{
                                                 itemID: value.ItemID,
                                                 name: value.Name,
                                                 AspxCommerceRoot: aspxRedirectPath,
                                                 sku: value.SKU,
                                                 imagePath: aspxRootPath + value.BaseImage.replace('uploads', 'uploads/Small'),
                                                 loaderpath: AspxCommerce.utils.GetAspxTemplateFolderPath() + "/images/loader_100x12.gif",
                                                 alternateText: value.Name,
                                                 listPrice: (value.ListPrice * rate),
                                                 price: (value.Price * rate),
                                                 shortDescription: Encoder.htmlDecode(value.ShortDescription)
}];

                                                 $.tmpl("scriptResultListWithoutOptionsTemp", items).appendTo("#divShowCategoryItemsList");
                                                 $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                                                 if (allowOutStockPurchase.toLowerCase() == 'false') {
                                                     if (value.IsOutOfStock) {
                                                         $(".cssClassInstock_" + value.ItemID).html('Out Of Stock');
                                                         $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                                                         $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                                                         $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                                                         $(".cssClassAddtoCard_" + value.ItemID + " a").removeAttr('onclick');
                                                     }
                                                 }
                                                 if (allowWishListCategory.toLowerCase() != 'true') {
                                                     $('.cssClassWishListWithoutOption').hide();
                                                 }
                                             }
                                         });
                                         $("img").lazyload();
                                     },
                                     BindResults: function() {
                                         var viewAsOption = $("#ddlViewAs").val();
                                         if (arrResultToBind.length > 0) {

                                             switch (viewAsOption) {
                                                 case '1':
                                                     categoryDetails.GridView();
                                                     break;
                                                 case '2':
                                                     categoryDetails.ListView();
                                                     break;
                                                 case '3':
                                                     categoryDetails.Grid2View();
                                                     break;
                                                 case '4':
                                                     categoryDetails.Grid3View();
                                                     break;
                                                 case '5':
                                                     categoryDetails.CompactListView();
                                                     break;
                                                 case '6':
                                                     categoryDetails.ProductGridView();
                                                     break;
                                                 case '7':
                                                     categoryDetails.ListWithoutOptionsView();
                                                     break;
                                             }
                                         }
                                     },
                                     BindItemsViewAsDropDown: function() {
                                         this.config.url = this.config.baseURL + "BindItemsViewAsList";
                                         this.config.data = "{}";
                                         this.config.ajaxCallMode = 1;
                                         this.ajaxCall(this.config);
                                     },
                                     BindItemsSortByDropDown: function() {
                                         this.config.url = this.config.baseURL + "BindItemsSortByList";
                                         this.config.data = "{}";
                                         this.config.ajaxCallMode = 2;
                                         this.ajaxCall(this.config);
                                     },
                                     LoadAllCategoryContents: function(offset, limit, current) {
                                         currentPage = current;
                                         categoryDetails.config.url = categoryDetails.config.baseURL + "GetCategoryDetailsOptions";
                                         categoryDetails.config.data = JSON2.stringify({ offset: offset, limit: limit, categorykey: categorykey, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
                                         categoryDetails.config.ajaxCallMode = 3;
                                         categoryDetails.ajaxCall(categoryDetails.config);
                                     },
                                     pageselectCallback: function(page_index, jq, execute) {
                                         //paginationNo = new_current_page;
                                         if (execute) {
                                             // Get number of elements per pagionation page from form
                                             var items_per_page = $('#ddlPageSize').val();
                                             var sortByOption = $("#ddlSortBy").val();
                                             if (arrItemListType.length > 0) {
                                                 switch (sortByOption) {
                                                     case '1':
                                                         //Newest to oldest asc
                                                         arrItemListType.sort(sort_by('ItemAddedOn', true));
                                                         // Sort by start_time
                                                         //arrItemListType.sort(sort_by('_AddedOn', false, function(a){return (new Date(a)).getTime()}));

                                                         break;
                                                     case '2':
                                                         //Oldest to newest desc
                                                         arrItemListType.sort(sort_by('ItemAddedOn', false));
                                                         break;
                                                     case '3':
                                                         //Price highest to lowest asc
                                                         // Sort by price high to low
                                                         arrItemListType.sort(sort_by('Price', true, parseFloat));

                                                         //arrItemListType.sort(function(a, b) { return parseFloat(a.price) - parseFloat(b.price) });
                                                         break;
                                                     case '4':
                                                         //Price lowest to highest desc
                                                         // Sort by price low to high
                                                         arrItemListType.sort(sort_by('Price', false, parseFloat));
                                                         //arrItemListType.sort(function(a, b) { return parseFloat(b.price) - parseFloat(a.price) });
                                                         break;
                                                     //                                            case '5':                                                                             
                                                     //                                                SortArray();                                                                             
                                                     //                                                break;                                                                            
                                                 }
                                                 //                                        $.each(arrItemListType, function(index, item) {
                                                 //                                            alert(item.AddedOn);
                                                 //                                        });


                                                 var max_elem = arrItemListType.length;
                                                 var newcontent = '';
                                                 arrResultToBind.length = 0;

                                                 // Iterate through a selection of the content and build an HTML string
                                                 for (var i = 0; i < max_elem; i++) {
                                                     //newcontent += '<dt>' + arrItemListType[i]._Name + '</dt>';
                                                     arrResultToBind.push(arrItemListType[i]);
                                                 }

                                                 categoryDetails.BindResults();
                                             }

                                             // Replace old content with new content
                                             //$('#Searchresult').html(newcontent);

                                             // Prevent click event propagation
                                             return false;
                                         }
                                     },

                                     ajaxSuccess: function(msg) {
                                         switch (categoryDetails.config.ajaxCallMode) {
                                             case 0:
                                                 break;
                                             case 1:
                                                 if (msg.d.length > 0) {
                                                     $.each(msg.d, function(index, item) {
                                                         var displayOptions = "<option value=" + item.DisplayItemID + ">" + item.OptionType + "</option>"
                                                         $("#ddlViewAs").append(displayOptions);
                                                     });
                                                     categoryDetails.LoadAllCategoryContents(1, parseInt($("#ddlPageSize").val()), 0);
                                                 }
                                                 break;
                                             case 2:
                                                 if (msg.d.length > 0) {
                                                     $.each(msg.d, function(index, item) {
                                                         var displayOptions = "<option value=" + item.SortOptionTypeID + ">" + item.OptionType + "</option>"
                                                         $("#ddlSortBy").append(displayOptions);
                                                     });
                                                 }
                                                 break;
                                             case 3:
                                                 var ListItems = '';
                                                 var catIds = '';
                                                 $("#divShowCategoryItemsList").show();
                                                 arrItemListType.length = 0;
                                                 // $("#ddlViewAs").val(1);
                                                 // $("#ddlSortBy").val(1);
                                                 if (msg.d.length > 0) {
                                                     $("#divShowCategoryItemsList").html('');
                                                     $("#divItemViewOptions").show();
                                                     $("#divSearchPageNumber").show();
                                                     $("#divViewAs").val(1);
                                                     var itemIds =[];
                                                     var headerElements = '';
                                                     var imgCount = 0;

                                                     $.each(msg.d, function(index, value) {
                                                         rowTotal = value.RowTotal;
                                                         if (value.ItemID != null) {
                                                             if (!IsExistedCategory(itemIds, value.ItemID)) {
                                                                 itemIds.push(value.ItemID);
                                                                 arrItemListType.push(value);
                                                             }
                                                         }
                                                         if (value.CategoryImage != '') {
                                                             if (!IsExistedCategory(catNames,value.CategoryName)) {
                                                                 var href = window.location.href.split('category/')[0] + "category/" + fixedEncodeURIComponent(value.CategoryName) + ".aspx";
                                                                 imgCount++;
                                                                 headerElements += "<li><a href=" + href + "><img src='" + aspxRootPath + value.CategoryImage.replace("Small", "Large") + "' alt='" + value.CategoryName + "' title='" + value.CategoryName + "' /></a></li>";
                                                                 catNames.push(value.CategoryName);
                                                             }
                                                         }
                                                         // headerElements += "" + value.CategoryName + "";                                                     
                                                         if (index < 1) {

                                                             parentCatID = value.CategoryID;
                                                             catIds = catIds + '' + value.CategoryID + '' + ",";

                                                         } else {
                                                             $("#categoryListings").html('');
                                                             if (catIds.indexOf('' + value.CategoryID + '') == -1 && value.ParentID == parentCatID) {
                                                                 var childCategories = '<a href="' + aspxRedirectPath + 'category/' + fixedEncodeURIComponent(value.CategoryName) + '.aspx">' + value.CategoryName + '</a>';
                                                                 //$("#categoryListings").append(childCategories);
                                                                 catIds = catIds + '' + value.CategoryID + '' + ",";
                                                             }
                                                         }

                                                     });
                                                     $("#divHeader").html('').html("<ul>" + headerElements + "</ul>");
                                                     if (imgCount > 1) {
                                                         $("#divHeader ul").bxSlider({
                                                             mode: 'horizontal',
                                                             infiniteLoop: true,
                                                             speed: 2000,
                                                             pause: 5000,
                                                             auto: true,
                                                             pager: false,
                                                             controls: true,
                                                             caption: true,
                                                             captionsSelector: '#CategoryCaption'
                                                         });
                                                     }
                                                     if (arrItemListType.length > 0) {
                                                         var items_per_page = $('#ddlPageSize').val();
                                                         $("#Pagination").pagination(rowTotal, {
                                                             callback: categoryDetails.pageselectCallback,
                                                             items_per_page: items_per_page,
                                                             //num_display_entries: 10,
                                                             current_page: currentPage,
                                                             callfunction: true,
                                                             function_name: { name: categoryDetails.LoadAllCategoryContents, limit: $('#ddlPageSize').val() },
                                                             prev_text: "Prev",
                                                             next_text: "Next",
                                                             prev_show_always: false,
                                                             next_show_always: false
                                                         });
                                                     } else {
                                                         $("#divItemViewOptions").hide();
                                                         $("#divSearchPageNumber").hide();
                                                         $("#divShowCategoryItemsList").html("No items found!");
                                                     }
                                                 } else {
                                                     $("#divItemViewOptions").hide();
                                                     $("#divSearchPageNumber").hide();
                                                     $("#divShowCategoryItemsList").html("No items found!");
                                                 }
                                                 break;
                                             //                                                case 4:                                                      
                                             //                                                    if (msg.d) {                                                      
                                             //                                                        csscody.alert('<h2>Information Alert</h2><p>The selected item already in your wishlist.</p>');                                                      
                                             //                                                    }                                                      
                                             //                                                    else {                                                      
                                             //                                                        AddToWishListFromJS(itemID, storeId, portalId, userName, ip, countryName);                                                      
                                             //                                                    }                                                      
                                             //                                                    break;                                                      
                                         }
                                     },
                                     init: function(config) {

                                         $(document).ajaxStart(function() {
                                             $('#divAjaxLoader').show();
                                         });
                                         $(document).ajaxStop(function() {
                                             $('#divAjaxLoader').hide();
                                         });
                                         categoryDetails.LoadCategoryDetailStaticImage();
                                         LoadAllAspxTemplate();
                                         categoryDetails.BindItemsViewAsDropDown();
                                         categoryDetails.BindItemsSortByDropDown();

                                         $("#ddlViewAs").change(function() {
                                             categoryDetails.BindResults();
                                         });
                                         $("#ddlSortBy").change(function() {
                                             // Create pagination element with options
                                             var items_per_page = $('#ddlPageSize').val();
                                             var offset = 1; //parseInt($.trim($(".pagination >span[class=current]").html()));
                                             categoryDetails.LoadAllCategoryContents(offset, items_per_page, 0);
                                         });

                                         $("#ddlPageSize").change(function() {
                                             var items_per_page = $(this).val();
                                             var offset = 1; //parseInt($.trim($(".pagination >span[class=current]").html()));
                                             categoryDetails.LoadAllCategoryContents(offset, items_per_page, 0);
                                         });
                                     }
                                 };
                                 categoryDetails.init();
                             });