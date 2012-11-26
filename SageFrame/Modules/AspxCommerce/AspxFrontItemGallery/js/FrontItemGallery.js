$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var FrontGallery = {
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
                type: FrontGallery.config.type,
                contentType: FrontGallery.config.contentType,
                cache: FrontGallery.config.cache,
                async: FrontGallery.config.async,
                url: FrontGallery.config.url,
                data: FrontGallery.config.data,
                dataType: FrontGallery.config.dataType,
                success: FrontGallery.ajaxSuccess,
                error: FrontGallery.ajaxFailure
            });
        },
        ajaxSuccess: function(msg) {
            switch (FrontGallery.config.ajaxCallMode) {
                case 1:
                    var featuredItemGalleryContents = '';
                    var itemCaption = '';
                    var featuredItemGallery = '';
                    if (msg.d.length > 0) {
                        $.each(msg.d, function(index, item) {
                            if (item.ImagePath == "") {
                                item.ImagePath = aspxRootPath + noImageFeaturedItemPathSetting;
                            }
                            if (item.AlternateText == "") {
                                item.AlternateText = item.Name;
                            }
                            var medpath = item.ImagePath;
                            medpath = medpath.replace('uploads', 'uploads/Medium');
                            featuredItemGalleryContents += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + item.SKU + '.aspx"><img alt="' + item.AlternateText + '" src="' + AspxCommerce.utils.GetAspxRootPath() + medpath + '" class=\"cssClassItemImage\" width=\"240"\ height=\"240"\ title="#Caption-' + item.ItemID + '" /></a>';
                            itemCaption += FrontGallery.BindItemCaption(item.ItemID, item.Name, Encoder.htmlDecode(item.ShortDescription), item.Price, item.SKU);
                        });
                        featuredItemGallery += '<div id="slider" class="nivoSlider">' + featuredItemGalleryContents + '</div>' + itemCaption;
                        //_ItemID; _DateFrom; _DateTo; _IsFeatured; _SKU; _Name; _ShortDescription; _Price; _ListPrice; _HidePrice; _HideInRSSFeed; _HideToAnonymous; _AddedOn;
                        $("#slider-wrapper").html(featuredItemGallery);
                        $('#slider').nivoSlider();
                    }
                    else {
                        featuredItemGallery += "<div class=\"nivoSlider\"><div class=\"cssClassNotFound\">This store has no featured items found!</div></div>";
                        //_ItemID; _DateFrom; _DateTo; _IsFeatured; _SKU; _Name; _ShortDescription; _Price; _ListPrice; _HidePrice; _HideInRSSFeed; _HideToAnonymous; _AddedOn;
                        $("#slider-wrapper").html(featuredItemGallery);
                    }
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    break;
                case 2:
                    break;
            }
        }, BindFeaturedItemsGallery: function() {
            var count = 5;
            var params = { storeId: storeId, portalId: portalId, userName: userName, cultureName: cultureName, count: count };
            var mydata = JSON2.stringify(params);
            this.config.method = "AspxCommerceWebService.asmx/GetFeaturedItemsList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = mydata;
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);

        }, BindItemCaption: function(itemId, itemName, itemShortDesc, itemPrice, itemSKU) {
            return '<div id="Caption-' + itemId + '" class="nivo-html-caption"><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + itemSKU + '.aspx">' + itemName + '</a><span >Price : <span class="cssClassFormatCurrency">' + itemPrice + '</span></span></div>';
        }
            , Init: function() {
                FrontGallery.BindFeaturedItemsGallery();
            }

    }
    FrontGallery.Init();
});