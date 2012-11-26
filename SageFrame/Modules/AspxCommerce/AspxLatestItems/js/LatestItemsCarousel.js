

var LatestItems_New = "";
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var ip = AspxCommerce.utils.GetClientIP();
    var sessionCode = AspxCommerce.utils.GetSessionCode();

    LatestItems_New = {
        config: {
            isPostBack: false,
            async: true,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxservicePath,
            method: "",
            url: "",
            ajaxCallMode: 0,
            itemid: 0
        },
        vars: {
            countCompareItems: 0
        },
        ajaxCall: function(config) {
            $.ajax({
                type: LatestItems_New.config.type,
                contentType: LatestItems_New.config.contentType,
                cache: LatestItems_New.config.cache,
                async: LatestItems_New.config.async,
                url: LatestItems_New.config.url,
                data: LatestItems_New.config.data,
                dataType: LatestItems_New.config.dataType,
                success: LatestItems_New.ajaxSuccess,
                error: LatestItems_New.ajaxFailure
            });
        },

        GetLatestItems: function() {
            this.config.method = "AspxCommerceWebService.asmx/GetLatestItemsList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ storeId: storeId, portalId: portalId, userName: userName, cultureName: cultureName, count: noOfLatestItems });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(data) {
            switch (LatestItems_New.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                $('#divLatestItemTemp').html('');
                $.each(data.d, function(index, value) {
                    var discount = ((value.ListPrice - value.Price) / value.ListPrice) * 100;
                    var items = [{
                        AspxCommerceRoot: aspxRootPath,
                        itemID: value.ItemID,
                        name: value.Name,
                        sku: value.SKU,
                        imagePath: aspxRootPath + value.ImagePath.replace('uploads', 'uploads/Small'),
                        alternateText: value.AlternateText,
                        listPrice: value.ListPrice,
                        price: value.Price,
                        discountOffer: discount.toFixed(2)
                    }];
                    $("#scriptResultGrid").tmpl(items).appendTo("#divLatestItemTemp");
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                    if (allowOutStockPurchase.toLowerCase() == 'false') {
                        if (value.IsOutOfStock) {
                            $(".cssClassAddtoCard_" + value.ItemID + " span").html('Out Of Stock');
                            $(".cssClassAddtoCard_" + value.ItemID).removeClass('cssClassAddtoCard');
                            $(".cssClassAddtoCard_" + value.ItemID).addClass('cssClassOutOfStock');
                            $(".cssClassAddtoCard_" + value.ItemID + " button").removeAttr('onclick');
                        }
                    }
                });
                $('#divLatestItemTemp').ItembxSlider({
                    moveSlideQty: 1,
                    displaySlideQty: 3
                });
                $('.cssClassProductPicture a img[title]').tipsy({ gravity: 'n' });
                        // $('#divLatestItemTemp li').css('width', '200px');
                break;
            }
        },
        init: function() {
            $("#divlatestItemsNew").hide();
            if (enableLatestItems.toLowerCase() == 'true') {
                LatestItems_New.GetLatestItems();
                $("#divlatestItemsNew").show();
            }
        }
    };
    LatestItems_New.init();
});