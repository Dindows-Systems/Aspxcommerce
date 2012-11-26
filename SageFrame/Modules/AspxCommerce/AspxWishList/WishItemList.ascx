<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WishItemList.ascx.cs"
    Inherits="WishItemList" %>

<script type="text/javascript" language="javascript">
	//<![CDATA[
    var countryName = '<%=CountryName %>';
    var userEmailIDWishList = '<%=UserEmailWishList %>';
    var serverNameVariables = '<%=Request.ServerVariables["SERVER_NAME"]%>';
    var allowOutStockPurchaseSetting = '<%=AllowOutStockPurchase %>';
    var showImageInWishlistSetting = '<%=ShowImageInWishlist %>';
    var enableWishListSetting = '<%=EnableWishList %>';
    var noImageWishListSetting = '<%=NoImageWishList%>';
    var WishList ="";
    $(function() {
        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var ip = AspxCommerce.utils.GetClientIP();
        var countryName = countryName;
        var customerId = AspxCommerce.utils.GetCustomerID();
        var sessionCode = AspxCommerce.utils.GetSessionCode();
        var userEmailWishList = userEmailIDWishList;
        var serverLocation = serverNameVariables;
        var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();

        WishList = {
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
                    type: WishList.config.type,
                    contentType: WishList.config.contentType,
                    cache: WishList.config.cache,
                    async: WishList.config.async,
                    url: WishList.config.url,
                    data: WishList.config.data,
                    dataType: WishList.config.dataType,
                    success: WishList.ajaxSuccess,
                    error: WishList.ajaxFailure
                });
            },
            ajaxSuccess: function(msg) {
                switch (WishList.config.ajaxCallMode) {
                case 1:
                    HeaderControl.GetWishListCount(); // for header wish counter increase for database
                    WishList.GetWishItemList(); // for rebinding the wishlist item 
                    csscody.info("<h2>Successful Message</h2><p>Wished item has been deleted successfully.</p>");
                    break;
                case 2:
                    $("#tblWishItemList>tbody").html('');
                    if (msg.d.length > 0) {
                        $.each(msg.d, function(index, item) {
                            WishList.BindWishListItems(item, index);
                        });
                        $(".comment").each(function() {
                            if ($(this).val() == "") {
                                $(this).addClass("lightText").val("enter a comment..");
                            }
                        });

                        $(".comment").bind("focus", function() {
                            if ($(this).val() == "enter a comment..") {
                                $(this).removeClass("lightText").val("");
                            }
                            // focus lost action
                        });
                        $(".comment").bind("blur", function() {
                            if ($(this).val() == "") {
                                $(this).val("enter a comment..").addClass("lightText");
                            }
                        });
                        $("#tblWishItemList>thead").css("display", "");
                        $("#wishitemBottom").show();
                    } else {
                        $("#tblWishItemList>thead").hide();
                        $("#wishitemBottom").hide();
                        $("#tblWishItemList").html("<tr><td class=\"cssClassNotFound\">Your wishlist is empty!</td></tr>");
                    }
                    break;
                case 3:
                    HeaderControl.GetWishListCount(); // for header wish counter increase for database
                    WishList.GetWishItemList(); // for rebinding the wishlist item                         
                    csscody.info("<h2>Successful Message</h2><p>Wished item has been deleted successfully.</p>");

                    break;
                case 4:
                    csscody.info("<h2>Successful Message</h2><p>Your wishlist has been updated successfully.</p>");
                    break;
                case 5:
                    HeaderControl.GetWishListCount(); // for header wish counter increase for database
                    WishList.GetWishItemList(); // for rebinding the wishlist item                        
                    csscody.info("<h2>Successful Message</h2><p>Your wishlist has been cleared successfully.</p>");
                    break;
                case 6:
                    csscody.info("<h2>Successful Message</h2><p>Email has been sent successfully.</p>");
                    WishList.ClearShareWishItemForm();
                    $('#divWishListContent').show();
                    $('#divShareWishList').hide();
                    $('#fade, #popuprel5, .cssClassClose').fadeOut();

                    break;
                case 7:
                    break;
                }
            },
            ajaxFailure: function() {
                switch (WishList.config.erroe) {
                case 3:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete wished item!</p>');
                    break;
                case 4:
                    csscody.error('<h2>Error Message</h2><p>Failed to update wish list!</p>');
                    break;
                case 5:
                    csscody.error('<h2>Error Message</h2><p>Failed to clear wish list!</p>');
                    break;
                case 6:
                        //   WishList.ClearShareWishItemForm();
                    $('#fade, #popuprel5, .cssClassClose').fadeOut();
                    csscody.error('<h2>Error Message</h2><p>Failed to sending mail!</p>');
                    break;
                }
            },

            trim: function(str, chars) {
                return WishList.ltrim(WishList.rtrim(str, chars), chars);
            },
            ltrim: function(str, chars) {
                chars = chars || "\\s";
                return str.replace(new RegExp("^[" + chars + "]+", "g"), "");
            },
            rtrim: function(str, chars) {
                chars = chars || "\\s";
                return str.replace(new RegExp("[" + chars + "]+$", "g"), "");
            },
            validateMultipleEmailsCommaSeparated: function(value) {
                var result = value.split(",");
                for (var i = 0; i < result.length; i++)
                    if (!WishList.validateEmail(result[i]))
                        return false;
                return true;
            },
            validateEmail: function(field) {
                var regex = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/ ;
                return (regex.test(WishList.trim(field))) ? true : false;
            },
            ClearShareWishItemForm: function() {
                $('#txtEmailID').val('');
                $('#txtEmailMessage').val('');
                $('.cssClassWishComment textarea').val('');
                $(".comment").each(function() {
                    if ($(this).val() == "") {
                        $(this).addClass("lightText").val("enter a comment..");
                    }
                });
                $('#tblWishItemList tbody tr').each(function() {
                    $(this).find('td input[type="checkbox"]').removeAttr('checked');
                });
                $('#chkHeading').removeAttr('checked');
            },

            DeleteWishListItem: function(itemId) {
                var properties = {
                    onComplete: function(e) {
                        WishList.ConfirmDeleteWishItem(itemId, e);
                    }
                }
                // Ask user's confirmation before delete records        
                csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this wished item?</p>", properties);
            },

            ConfirmDeleteWishItem: function(id, event) {
                if (event) {
                    this.config.method = "AspxCommerceWebService.asmx/DeleteWishItem";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({ ID: id, storeID: storeId, portalID: portalId, userName: userName });
                    this.config.ajaxCallMode = 1;
                    this.ajaxCall(this.config);
                }
            },

            GetWishItemList: function() {
                var count = 10;
                var isAll = 1;
                this.config.method = "AspxCommerceWebService.asmx/GetWishItemList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName, flagShowAll: isAll, count: count });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            },

            BindWishListItems: function(response, index) {
                if (response.ImagePath == "") {
                    response.ImagePath = noImageWishListSetting;
                } else if (response.AlternateText == "") {
                    response.AlternateText = response.ItemName;
                }
                ItemIDs = response.ItemID + "#";
                ItemComments = $("#comment" + response.ItemID + "").innerText;

                var WishDate = WishList.DateDeserialize(response.WishDate, "yyyy/M/d");

                var itemSKU = JSON2.stringify(response.SKU);
                if (index % 2 == 0) {

                    if (allowOutStockPurchaseSetting.toLowerCase() == 'false') {
                        if (response.IsOutOfStock) {
                            //  Items = "<tr class='cssClassAlternativeEven' id='tr_" + response.ItemID + "'><td class='cssClassWishItemDetails'><div class='cssClassImage'><img src='" + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + "' alt=' "+ response.AlternateText + "' title='" + response.AlternateText + "'/></div><a href='" + aspxRedirectPath + "item/" + response.SKU + ".aspx'>" + response.ItemName + "</a><span class='cssClassPrice cssClassFormatCurrency'>" + (response.Price * rate).toFixed(2) + "</span></td><td class='cssClassWishComment'><textarea maxlength='300' onkeyup="+ismaxlength(this)+" id='comment_" + response.ItemID + "' class='comment'>" + response.Comment + "</textarea></td><td class='cssClassWishDate'>" + WishDate + "</td><td class='cssClassWishToCart'><div class='cssClassButtonWrapper cssClassOutOfStock'><a href='#'><span>Out Of Stock</span></a></div></td><td class='cssClassDelete'><img onclick='DeleteWishListItem(" + response.ItemID + ")' src='" + aspxTemplateFolderPath + "/images/admin/btndelete.png'/></td></tr>";
                            Items = '<tr class="cssClassAlternativeEven" id="tr_' + response.ItemID + '"><td class="cssClassWishItemChkbox"><input type="checkbox" class="cssClassWishItem" /></td><td class="cssClassWishItemDetails"><div class="cssClassImage"><img src="' + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div>';
                            Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a>';
                            Items += '<span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart">';
                            Items += "<div class='cssClassButtonWrapper cssClassOutOfStock'><a href=\"#\"><span>Out Of Stock</span></a></div></td>";
                            Items += '<td class="cssClassDelete"><img onclick="WishList.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';

                        } else {
                            //  Items = "<tr class='cssClassAlternativeEven' id='tr_" + response.ItemID + "'><td class='cssClassWishItemDetails'><div class='cssClassImage'><img src='" + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + "' alt='" + response.AlternateText + "' title='" + response.AlternateText + "'/></div><a href='" + aspxRedirectPath + "item/" + response.SKU + ".aspx'>" + response.ItemName + "</a><span class='cssClassPrice cssClassFormatCurrency'>" + (response.Price * rate).toFixed(2) + "</span></td><td class='cssClassWishComment'><textarea maxlength='300' onkeyup="+ismaxlength(this)+" id='comment_" + response.ItemID + "' class='comment'>" + response.Comment + "</textarea></td><td class='cssClassWishDate'>" + WishDate + "</td><td class='cssClassWishToCart'><div class='cssClassButtonWrapper '><a href='#' onclick='AddToCartToJS(" + response.ItemID + "," + response.Price  + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td><td class='cssClassDelete'><img onclick='DeleteWishListItem(" + response.ItemID + ")' src='" + aspxTemplateFolderPath + "/images/admin/btndelete.png'/></td></tr>";
                            Items = '<tr class="cssClassAlternativeEven" id="tr_' + response.ItemID + '"><td class="cssClassWishItemChkbox"><input type="checkbox" class="cssClassWishItem" /></td><td class="cssClassWishItemDetails"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div>';
                            Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a>';
                            Items += '<span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart">';
                            Items += "<div class='cssClassButtonWrapper'><a href=\"#\" onclick='WishList.AddToCartToJS(" + response.ItemID + "," + response.Price + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td>";
                            Items += '<td class="cssClassDelete"><img onclick="WishList.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
                        }
                    } else {
                        //  Items = "<tr class='cssClassAlternativeEven' id='tr_" + response.ItemID + "'><td class='cssClassWishItemDetails'><div class='cssClassImage'><img src='" + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + "' alt='" + response.AlternateText + "' title='" + response.AlternateText + "'/></div><a href='" + aspxRedirectPath + "item/" + response.SKU + ".aspx'>" + response.ItemName + "</a><span class='cssClassPrice cssClassFormatCurrency'>" + (response.Price * rate).toFixed(2)+ "</span></td><td class='cssClassWishComment'><textarea maxlength='300' onkeyup="+ismaxlength(this)+" id='comment_" + response.ItemID + "' class='comment'>" + response.Comment + "</textarea></td><td class='cssClassWishDate'>" + WishDate + "</td><td class='cssClassWishToCart'><div class='cssClassButtonWrapper '><a href='#' onclick='AddToCartToJS(" + response.ItemID + "," + response.Price  + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td><td class='cssClassDelete'><img onclick='DeleteWishListItem(" + response.ItemID + ")' src='" + aspxTemplateFolderPath + "/images/admin/btndelete.png'/></td></tr>";
                        Items = '<tr class="cssClassAlternativeEven" id="tr_' + response.ItemID + '"><td class="cssClassWishItemChkbox"><input type="checkbox" class="cssClassWishItem" /></td><td class="cssClassWishItemDetails"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div>';
                        Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a>';
                        Items += '<span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart">';
                        Items += "<div class='cssClassButtonWrapper'><a href=\"#\" onclick='WishList.AddToCartToJS(" + response.ItemID + "," + response.Price + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td>";
                        Items += '<td class="cssClassDelete"><img onclick="WishList.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';

                    }
                } else {
                    if (allowOutStockPurchaseSetting.toLowerCase() == 'false') {
                        if (response.IsOutOfStock) {
                            // Items = "<tr class='cssClassAlternativeOdd' id='tr_" + response.ItemID + "'><td class='cssClassWishItemDetails'><div class='cssClassImage'><img src='" + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + "' alt='" + response.AlternateText + "' title='" + response.AlternateText + "'/></div><a href='" + aspxRedirectPath + "item/" + response.SKU + ".aspx'>" + response.ItemName + "</a><span class='cssClassPrice cssClassFormatCurrency'>" + (response.Price * rate).toFixed(2) + "</span></td><td class='cssClassWishComment'><textarea maxlength='300' onkeyup="+ismaxlength(this)+" id='comment_" + response.ItemID + "' class='comment'>" + response.Comment + "</textarea></td><td class='cssClassWishDate'>" + WishDate + "</td><td class='cssClassWishToCart'><div class='cssClassButtonWrapper cssClassOutOfStock'><a href='#'><span>Out Of Stock</span></a></div></td><td class='cssClassDelete'><img onclick='DeleteWishListItem(" + response.ItemID + ")' src='" + aspxTemplateFolderPath + "/images/admin/btndelete.png'/></td></tr>";
                            Items = '<tr class="cssClassAlternativeOdd" id="tr_' + response.ItemID + '"><td class="cssClassWishItemChkbox"><input type="checkbox" class="cssClassWishItem" /></td><td class="cssClassWishItemDetails"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div>';
                            Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a>';
                            Items += '<span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart">';
                            Items += "<div class='cssClassButtonWrapper cssClassOutOfStock'><a href=\"#\"><span>Out Of Stock</span></a></div></td>";
                            Items += '<td class="cssClassDelete"><img onclick="WishList.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + ' /images/admin/btndelete.png"/></td></tr>';

                        } else {
                            //  Items = "<tr class='cssClassAlternativeOdd' id='tr_" + response.ItemID + "'><td class='cssClassWishItemDetails'><div class='cssClassImage'><img src='" + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + "' alt='" + response.AlternateText + "' title='" + response.AlternateText + "'/></div><a href='" + aspxRedirectPath + "item/" + response.SKU + ".aspx'>" + response.ItemName + "</a><span class='cssClassPrice cssClassFormatCurrency'>" + (response.Price * rate).toFixed(2)+ "</span></td><td class='cssClassWishComment'><textarea maxlength='300' onkeyup="+ismaxlength(this)+" id='comment_" + response.ItemID + "' class='comment'>" + response.Comment + "</textarea></td><td class='cssClassWishDate'>" + WishDate + "</td><td class='cssClassWishToCart'><div class='cssClassButtonWrapper '><a href='#' onclick='AddToCartToJS(" + response.ItemID + "," + response.Price + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td><td class='cssClassDelete'><img onclick='DeleteWishListItem(" + response.ItemID + ")' src='" + aspxTemplateFolderPath + "/images/admin/btndelete.png'/></td></tr>";
                            Items = '<tr class="cssClassAlternativeOdd" id="tr_' + response.ItemID + '"><td class="cssClassWishItemChkbox"><input type="checkbox" class="cssClassWishItem" /></td><td class="cssClassWishItemDetails"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div>';
                            Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a>';
                            Items += '<span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart">';
                            Items += "<div class='cssClassButtonWrapper'><a href=\"#\" onclick='WishList.AddToCartToJS(" + response.ItemID + "," + response.Price + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td>";
                            Items += '<td class="cssClassDelete"><img onclick="WishList.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
                        }
                    } else {
                        //   Items = "<tr class='cssClassAlternativeOdd' id='tr_" + response.ItemID + "'><td class='cssClassWishItemDetails'><div class='cssClassImage'><img src='" + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + "' alt='" + response.AlternateText + "' title='" + response.AlternateText + "'/></div><a href='" + aspxRedirectPath + "item/" + response.SKU + ".aspx'>" + response.ItemName + "</a><span class='cssClassPrice cssClassFormatCurrency'>" + (response.Price * rate).toFixed(2)+ "</span></td><td class='cssClassWishComment'><textarea maxlength='300' onkeyup="+ismaxlength(this)+" id='comment_" + response.ItemID + "' class='comment'>" + response.Comment + "</textarea></td><td class='cssClassWishDate'>" + WishDate + "</td><td class='cssClassWishToCart'><div class='cssClassButtonWrapper '><a href='#' onclick='AddToCartToJS(" + response.ItemID + "," + response.Price  + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td><td class='cssClassDelete'><img onclick='DeleteWishListItem(" + response.ItemID + ")' src='" + aspxTemplateFolderPath + "/images/admin/btndelete.png'/></td></tr>";
                        Items = '<tr class="cssClassAlternativeOdd" id="tr_' + response.ItemID + '"><td class="cssClassWishItemChkbox"><input type="checkbox" class="cssClassWishItem" /></td><td class="cssClassWishItemDetails"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div>';
                        Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a>';
                        Items += '<span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart">';
                        Items += "<div class='cssClassButtonWrapper'><a href=\"#\" onclick='WishList.AddToCartToJS(" + response.ItemID + "," + response.Price + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td>";
                        Items += '<td class="cssClassDelete"><img onclick="WishList.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';

                    }
                }

                $("#tblWishItemList>tbody").append(Items);
                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                if (showImageInWishlistSetting.toLowerCase() == 'false') {
                    // $('.cssClassWishItemDetails>img').hide();
                    $('.cssClassWishItemDetails div').hide();
                }
                $(".comment").keypress(function(e) {
                    if (e.which == 35) {
                        return false;
                    }
                });
            },
            ismaxlength: function(obj) {
                var mlength = obj.getAttribute ? parseInt(obj.getAttribute("maxlength")) : ""
                if (obj.getAttribute && obj.value.length > mlength)
                    obj.value = obj.value.substring(0, mlength)
            },
            AddToCartToJS: function(itemId, itemPrice, itemSKU, itemQuantity) {
                AspxCommerce.RootFunction.AddToCartFromJS(itemId, itemPrice, itemSKU, itemQuantity, storeId, portalId, customerId, sessionCode, userName, cultureName);
            },
            ConfirmSingleDelete: function(id, event) {
                if (event) {
                    this.config.method = "AspxCommerceWebService.asmx/DeleteWishItem";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({ ID: id, storeID: storeId, portalID: portalId, userName: userName });
                    this.config.ajaxCallMode = 3;
                    this.ajaxCall(this.config);
                }
            },
            DeleteWishItem: function(itemId) {
                var properties = {
                    onComplete: function(e) {
                        WishList.ConfirmSingleDelete(itemId, e);
                    }
                }
                // Ask user's confirmation before delete records        
                csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete wished item?</p>", properties);
            },
            UpdateWishList: function() {
                var comment = '';
                var itemId = '';
                $(".comment").each(function() {
                    comment += $(this).val() + '#';
                    itemId += parseInt($(this).attr("id").replace( /[^0-9]/gi , '')) + '#';
                });
                comment = comment.substring(0, comment.length - 1);
                itemId = itemId.substring(0, itemId.length - 1);

                this.config.method = "AspxCommerceWebService.asmx/UpdateWishList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ ID: itemId, comment: comment, storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 4;
                this.config.error = 4;
                this.ajaxCall(this.config);

            },
            DeleteAllWishList: function() {
                this.config.method = "AspxCommerceWebService.asmx/ClearWishList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 5;
                this.config.error = 5;
                this.ajaxCall(this.config);
            },
            ClearWishList: function() {
                var properties = {
                    onComplete: function(e) {
                        if (e) {
                            WishList.DeleteAllWishList(e);
                        }
                    }
                };
                csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to clear wish list items?</p>", properties);
            },
            DateDeserialize: function(content, format) {
                content = eval('new ' + content.replace( /[/]/gi , ''));
                return formatDate(content, format);
            },
            SendShareItemEmail: function() {
                var emailID = '';
                var message = '';
                var itemId = '';
                var arr = new Array;
                var elems = '';
                $(".comment").each(function() {
                    itemId += parseInt($(this).attr("id").replace( /[^0-9]/gi , '')) + ',';
                });
                itemId = itemId.substring(0, itemId.length - 1);
                emailID = $('#txtEmailID').val();
                message = $('#txtEmailMessage').val();
                var senderName = userFullName;
                var senderEmail = userEmailWishList;
                var receiverEmailID = emailID;
                var subject = "Take A Look At " + senderName + "'s " + " WishList";
                var msgbodyhtml = '';
                var msgCommenthtml = '';
                var serverHostLoc = 'http://' + serverLocation;
                var fullDate = new Date();
                var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : (fullDate.getMonth() + 1);
                if (twoDigitMonth.length == 2) {
                } else if (twoDigitMonth.length == 1) {
                    twoDigitMonth = '0' + twoDigitMonth;
                }
                var currentDate = fullDate.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();
                var dateyear = fullDate.getFullYear();

                var trLength = $('#tblWishItemList tbody tr').lenght;
                var tdContent = '';
                var tdContentArray = [];
                var shareWishMailHtml = '';
                shareWishMailHtml += '<table width="100%" border="0" cellspacing="0" cellpadding="0"><tr><td>';
                shareWishMailHtml += '  <table width="100%" border="0" cellspacing="0" cellpadding="0"><tr>';
                // loop function here
                $('#tblWishItemList tbody tr').each(function() {
                    if ($(this).find('td input[type="checkbox"]').attr('checked')) {
                        var src = $(this).find('td div.cssClassImage img').attr('src');
                        var alt = $(this).find('td div.cssClassImage img').attr('alt');
                        var title = $(this).find('td div.cssClassImage img').attr('title');
                        var price = $(this).find('td.cssClassWishItemDetails span').html();
                        var href = $(this).find('td.cssClassWishItemDetails a').attr('href');
                        var hrefHtml = $(this).find('td.cssClassWishItemDetails a').html();
                        var htmlComment = $(this).find('td.cssClassWishComment textarea').val();
                        tdContent += '<td width="33%"><div style="border:1px solid #cfcfcf; background:#f1f1f1; padding:10px; text-align:center;"> <img src=' + serverHostLoc + src + ' alt="' + alt + '" width="80" height="50" />';
                        tdContent += ' <p style="margin:0; padding:5px 0 0 0; font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; line-height:18px;">';
                        tdContent += '<span style="font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;">' + title + '</span><br />'; //item name
                        tdContent += '<span style="font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;"> <a href="' + serverHostLoc + href + '">' + hrefHtml + '</a></span><br />'; //item name
                        tdContent += '<span style="font-weight:bold; font-size:11px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;">Price:</span> ' + price + '<br />'; //price
                        tdContent += '<span style="font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;">Comments:</span> ' + htmlComment + '</p></div></td>'; //comment
                        tdContentArray.push(tdContent);
                        tdContent = '';
                    } else {
                        return true;
                    }

                }); //loop finishes
                for (var i in tdContentArray) {
                    if (i % 3 == 0) {
                        shareWishMailHtml += '</tr><tr>' + tdContentArray[i];
                    } else {
                        shareWishMailHtml += tdContentArray[i];
                    }
                }
                shareWishMailHtml += '</tr></table></td></tr></table>';
                this.config.method = "AspxCommerceWebService.asmx/ShareWishListEmailSend";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, itemID: itemId, senderName: senderName, senderEmail: userEmailWishList, receiverEmailID: receiverEmailID, subject: subject, message: message, link: shareWishMailHtml, cultureName: cultureName });
                this.config.ajaxCallMode = 6;
                this.config.error = 6;
                this.ajaxCall(this.config);
            },
            HideMessage: function() {
                $('.errorMessage').hide();
            },
            Init: function() {
                if (enableWishListSetting.toLowerCase() == 'true') {
                    WishList.GetWishItemList();
                    $("#divWishListContent").show();
                    $('.errorMessage').hide();
                    $('#divShareWishList').hide();
                    if (userFriendlyURL) {
                        $("#lnkContinueShopping").attr("href", '' + aspxRedirectPath + 'Home.aspx');
                    } else {
                        $("#lnkContinueShopping").attr("href", '' + aspxRedirectPath + 'Home');
                    }
                    $("#continueInStore").bind("click", function() {
                        if (userFriendlyURL) {
                            window.location.href = aspxRedirectPath + 'Home.aspx';
                        } else {
                            window.location.href = aspxRedirectPath + 'Home';
                        }
                        return false;
                    });

                    //		$('#btnShareWishBack').click(function() {
                    //            $('#divWishListContent').show();
                    //            $('#divShareWishList').hide();
                    //        });

                    $('#shareWishList').bind("click", function() {
                        //  $('#divWishListContent').hide();
                        $('#divShareWishList').show();
                        WishList.HideMessage();
                        // WishList.ClearShareWishItemForm();
                        var wishChecked = false;
                        $('#tblWishItemList tbody tr').each(function() {
                            if ($(this).find('td input[type="checkbox"]').attr('checked')) {
                                wishChecked = true;
                                return true;
                            }
                        });
                        if (wishChecked == true) {
                            ShowPopupControl('popuprel5');
                        } else {
                            csscody.alert("<h2>Information Alert</h2><p>Please select at least one item.</p>");
                        }
                    });

                    $(".cssClassClose").bind("click", function() {
                        $('#fade, #popuprel5').fadeOut();
                        //  WishList.ClearShareWishItemForm();
                    });

                    $('#btnShareWishItem').bind("click", function() {
                        var emailIDsColln = $('#txtEmailID').val();
                        if (WishList.validateMultipleEmailsCommaSeparated(emailIDsColln)) {
                            WishList.SendShareItemEmail();
                        } else {
                            // alert('Eener Valid email with comma separated');
                            $('.errorMessage').show();
                        }
                    });

                    $("#chkHeading").change(function() {
                        if ($(this).attr("checked")) {
                            $('#tblWishItemList tbody tr').each(function() {
                                $(this).find('td input[type="checkbox"]').attr('checked', 'checked');
                            });
                        } else {
                            $('#tblWishItemList tbody tr').each(function() {
                                $(this).find('td input[type="checkbox"]').removeAttr('checked');
                            });
                        }
                    });

                    $('#tblWishItemList tbody tr').find(".cssClassWishItemChkbox").change(function() {
                        var totalitems = $('#tblWishItemList tbody tr').find('td input[type="checkbox"]').length;
                        var matchedcount = 0;
                        $('#tblWishItemList tbody tr').find('td input[type="checkbox"]').each(function() {

                            if ($(this).attr('checked'))
                                matchedcount++;

                        });
                        if (matchedcount == totalitems) {
                            $("#chkHeading").attr('checked', 'checked');
                        } else {
                            $("#chkHeading").removeAttr('checked');
                        }
                    });
                } else {
                    csscody.alert('<h2>Information Alert</h2><p>WishList is not enabled.</p>');
                }
            }
        };
        WishList.Init();

    });
	//]]> 
</script>

<div id="divWishListContent" class="cssClassFormWrapper">
    <div class="cssClassCommonCenterBox">
        <h2>
            <asp:Label ID="lblMyWishListTitle" runat="server" Text="My WishList Content" CssClass="cssClassWishItem"></asp:Label></h2>
        <div class="cssClassCommonCenterBoxTable">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblWishItemList"
                class="cssClassMyWishItemTable">
          <thead style="display:none">
                    <tr class="cssClassCommonCenterBoxTableHeading">
                    <td class="cssClassWishItemChkbox">
                    <input type="checkbox" id="chkHeading" />
                    </td>
                        <td class="cssClassWishItemDetails">
                            <asp:Label ID="lblItem" runat="server" Text="Item"></asp:Label>
                        </td>
                        <td class="cssClassWishListComment">
                            <asp:Label ID="lblComment" runat="server" Text="Comment"></asp:Label>
                        </td>
                        <td class="cssClassAddedOn">
                            <asp:Label ID="lblAddedOn" runat="server" Text="Added On"></asp:Label>
                        </td>
                        <td class="cssClassAddToCart">
                            <asp:Label ID="lblAddToCart" runat="server" Text="Add To Cart"></asp:Label>
                        </td>
                        <td class="cssClassDelete">
                        </td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="cssClassButtonWrapper" id="wishitemBottom" style="display:none">
                <button type="button" id="shareWishList" rel="popuprel5">
                    <span><span>Share Wishlist</span></span></button>
                <%--<button type="button">
                    <span><span>Add All to Cart</span></span></button>--%>
                <button type="button" id="updateWishList" onclick="WishList.UpdateWishList();">
                    <span><span>Update WishList</span></span></button>
                <button type="button" id="clearWishList" onclick="WishList.ClearWishList();">
                    <span><span>Clear WishList</span></span></button>
                <button type="button" id="continueInStore">
                    <span><span>Continue to Shopping</span></span></button>                    
            </div>
        </div>
    </div>
</div>
<div class="popupbox" id="popuprel5">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span>Close</span></button>
    </div>
     <h2>
                <asp:Label ID="lblWishHeading" runat="server" Text="Share Your WishList" CssClass="cssClassWishItem"></asp:Label>
            </h2>
           
    <div id="divShareWishList" class="cssClassFormWrapper">
        <div class="cssClassCommonCenterBox">
          <div class="cssClassPopUpHeading">
                <h3>
                   <asp:Label ID="lblShareHeading" runat="server" Text="Sharing Information" CssClass="cssClassLabel"></asp:Label>
                </h3>
            </div>
            <div class="cssClassCommonCenterBoxTable">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblShareWishList"
                    class="cssClassMyWishItemTable">
                    <tbody>
                        <tr>
                            <td>
                                <ul>
                                    <li>
                                        <asp:Label ID="lblEmailHeading" runat="server" Text="Email addresses, separated by commas"></asp:Label>
                                        <span class="cssClassRequired">*</span>
                                        <br />
                                        <textarea id="txtEmailID" name="receiveremailIDs" class="required email" rows="5"
                                            cols="60" onclick="WishList.HideMessage();"></textarea>
                                        <br />
                                        <p class="errorMessage">
                                            <span class="cssClassRequired">Enter Valid EmailID with comma separated</span></p>
                                    </li>
                                    <li>
                                        <asp:Label ID="lblEmailMessage" runat="server" Text="Message"></asp:Label><br />
                                        <textarea id="txtEmailMessage" class="emailMessage" rows="5" cols="60" name="emailMessage"></textarea>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="cssClassButtonWrapper">
                    <%--<button type="button" id="btnShareWishBack" >
                    <span><span>Back</span></span></button>--%>
                    <button type="button" id="btnShareWishItem">
                        <span><span>Share WishList</span></span></button>
                </div>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hdnWishItem" />
