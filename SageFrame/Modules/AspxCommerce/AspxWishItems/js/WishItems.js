var WishItems = "";
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var ip = AspxCommerce.utils.GetClientIP();
    var countryName = countryNameWishList;
    var comment = $("#txtComment").val();
    var ItemIDs = 0; var ItemComments = "";
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();

    var wishlistcount = noOfRecentAddedWishItems;
    WishItems = {
        config: {
            isPostBack: false,
            async: true,
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
                type: WishItems.config.type,
                contentType: WishItems.config.contentType,
                cache: WishItems.config.cache,
                async: WishItems.config.async,
                url: WishItems.config.url,
                data: WishItems.config.data,
                dataType: WishItems.config.dataType,
                success: WishItems.ajaxSuccess,
                error: WishItems.ajaxFailure
            });
        },
        ajaxSuccess: function(msg) {
            switch (WishItems.config.ajaxCallMode) {
                case 1:
                    HeaderControl.GetWishListCount(); // for header wish counter increase for database 
                    WishItems.BindMyWishList();
                    break;
                case 2:
                    $("#tblWishItem>tbody").html('');
                    $("#tblWishItem>thead").html('');
                    $("#tblWishItem>tfoot").html('');
                    if (msg.d.length > 0) {
                        var wishListHeadingLabel = '<tr><td> <h3><label ID="lblWishListTitle" class="cssClassGowishlistHead"><span>Last Added Items</span><label></h3></td></tr>';
                        $("#tblWishItem>thead").append(wishListHeadingLabel);
                        var myWishListLink = "";
                        if (userFriendlyURL) {
                            myWishListLink = wishListURLSetting + '.aspx';
                        } else {
                            myWishListLink = wishListURLSetting;
                        }

                        var wishListFooter = '<tr><td><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + myWishListLink + '" id="lnkGoToWishlist"> <span class="gowishlist">Go to Wishlist</span></a></td></tr>';
                        $("#tblWishItem>tfoot").append(wishListFooter);
                        $.each(msg.d, function(index, item) {
                            WishItems.BindWishItems(item, index);
                            // $('#' + lblLastAdded).show();
                        });
                    } else {
                        //  $('#' + lblLastAdded).hide();
                        $("#tblWishItem>tbody").html('<tr><td><span class=\"cssClassNotFound\">Your Wishlist is empty!</span></tr></td>');
                        $("#tblWishItem>tfoot").html('');
                    }
                    break;
            }
        },
        DeleteWishListItem: function(itemId) {
            //alert(itemId);
            var properties = {
                onComplete: function(e) {
                    WishItems.ConfirmDeleteWishItem(itemId, e);
                }
            };
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
        BindMyWishList: function() {
            var isShowAll = 0;
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName, flagShowAll: isShowAll, count: wishlistcount });
            this.config.method = "AspxCommerceWebService.asmx/GetWishItemList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);

        },
        BindWishItems: function(response, index) {
            if (response.ImagePath == "") {
                response.ImagePath = wishListNoImagePath;
            }
            if (response.AlternateText == "") {
                response.AlternateText = response.ItemName;
            }
            if (index % 2 == 0) {
                Items = '<tr class="cssClassAlternativeEven" id="trWishItem_' + response.ItemID + '"><td class="cssClassWishItemDetails">';
                if (showWishItemImage.toLowerCase() == 'true') {
                    Items += '<a href ="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '"  title="' + response.AlternateText + '"/></div></a>';
                }
                Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a><span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassDelete"><img id="imgdelete" onclick="WishItems.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
            } else {
                Items = '<tr class="cssClassAlternativeOdd" id="trWishItem_' + response.ItemID + '"><td class="cssClassWishItemDetails">';
                if (showWishItemImage.toLowerCase() == 'true') {
                    Items += '<a href ="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '"  title="' + response.AlternateText + '"/></div></a>';
                }
                Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a><span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassDelete"><img id="imgdelete" onclick="WishItems.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
            }
            $("#tblWishItem>tbody").append(Items);
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },
        Init: function() {
            if (userFriendlyURL) {
                $("#lnkGoToWishlist").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + wishListURLSetting + '.aspx');
            } else {
                $("#lnkGoToWishlist").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + wishListURLSetting);
            }
            if (allowWishItemList.toLowerCase() == 'true') {
                WishItems.BindMyWishList();
            } else {
                $('#divRecentlyAddedWishList').hide();
            }
        }
    };
    WishItems.Init();
}); 