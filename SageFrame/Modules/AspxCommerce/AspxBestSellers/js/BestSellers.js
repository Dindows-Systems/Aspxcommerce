$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var ip = AspxCommerce.utils.GetClientIP();
    var countryName = AspxCommerce.utils.GetAspxClientCoutry();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var countSeller = countBestSellerSetting;
    var BestSellers = {
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
        ajaxCall: function(config) {
            $.ajax({
                type: BestSellers.config.type,
                contentType: BestSellers.config.contentType,
                cache: BestSellers.config.cache,
                async: BestSellers.config.async,
                data: BestSellers.config.data,
                dataType: BestSellers.config.dataType,
                url: BestSellers.config.url,
                success: BestSellers.ajaxSuccess,
                error: BestSellers.ajaxFailure
            });
        },
        init: function() {
            $("#divBestSellers").hide();
            if (enableBestSellerItems.toLowerCase() == 'true') {
                BestSellers.GetBestSoldItems();
                $("#divBestSellers").show();
            }
        },

        GetBestSoldItems: function() {
            if (countSeller > 0) {
                this.config.url = this.config.baseURL + "GetBestSoldItems";
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, count: countSeller });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            } else {
                $(".cssClassBestSellerBox").html("<span class=\"cssClassNotFound\">No item is sold in this store Yet!</span>");
                $(".cssClassBestSellerBox").removeClass("cssClassBestSellerBox");

            }
        },
        ajaxSuccess: function(data) {
            switch (BestSellers.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    if (data.d.length > 0) {
                        $(".cssClassBestSellerBoxInfo ul").html('');
                        $.each(data.d, function(index, item) {
                            var imgpath = "";
                            if (item.ImagePath == null || item.ImagePath == "") {
                                imgpath = defaultImagePath;
                            }
                            else {
                                imgpath = item.ImagePath.replace('uploads', 'uploads/Small');
                            }
                            $(".cssClassBestSellerBoxInfo ul").append('<li><a class="cssClassProductPicture" href="' + aspxRedirectPath + 'item/' + item.Sku + '.aspx" ><img class="lazy" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/loader_100x12.gif" data-original="' + aspxRootPath + imgpath + '" alt="' + item.ItemName + '" /></a><a href="' + aspxRedirectPath + 'item/' + item.Sku + '.aspx" class="cssClassItemName">' + item.ItemName + '</a></li>');
                        });
                        $('img').lazyload();
                    }
                    else {
                        $(".cssClassBestSellerBox").html("<span class=\"cssClassNotFound\">No item is sold in this store Yet!</span>");
                        $(".cssClassBestSellerBox").removeClass("cssClassBestSellerBox");
                    }
                    break;
            }
        }
    }

    BestSellers.init();
});