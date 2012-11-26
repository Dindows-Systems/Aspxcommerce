$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var SpecialItems = {
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
                type: SpecialItems.config.type,
                contentType: SpecialItems.config.contentType,
                cache: SpecialItems.config.cache,
                async: SpecialItems.config.async,
                url: SpecialItems.config.url,
                data: SpecialItems.config.data,
                dataType: SpecialItems.config.dataType,
                success: SpecialItems.ajaxSuccess,
                error: SpecialItems.ajaxFailure
            });
        },
        ajaxSuccess: function(msg) {
            switch (SpecialItems.config.ajaxCallMode) {
                case 1:
                    if (msg.d.length > 0) {
                        $(".cssClassSpecialBoxInfo ul").html('');
                        $.each(msg.d, function(index, item) {
                            $(".cssClassSpecialBoxInfo ul").append('<li><a class="cssClassProductPicture" href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + item.ItemSku + '.aspx" ><img class="lazy" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/loader_100x12.gif" data-original="' + AspxCommerce.utils.GetAspxRootPath() + item.Imagepath.replace('uploads', 'uploads/Small') + '"  alt="' + item.ItemName + '" /></a><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + item.ItemSku + '.aspx" class="cssClassItemName">' + item.ItemName + '</a></li>');
                        });
                    } else {
                        $(".cssClassSpecialBox").html("<span class=\"cssClassNotFound\">No special item found in this store!</span>");
                        $(".cssClassSpecialBox").removeClass("cssClassSpecialBox");
                    }
                    break;
            }
        },
        GetSpecialItems: function() {
            if (countSpecials > 0) {
                this.config.method = "AspxCommerceWebService.asmx/GetSpecialItems";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, count: countSpecials });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            } else {
                $(".cssClassSpecialBox").html("<span class=\"cssClassNotFound\">No special item found in this store!</span>");
                $(".cssClassSpecialBox").removeClass("cssClassSpecialBox");
            }
        },
        Init: function() {
            $("#divSpecialItems").hide();
            if (enableSpecialItems.toLowerCase() == 'true') {
                SpecialItems.GetSpecialItems();
                $("#divSpecialItems").show();
            }
        }
    };
    SpecialItems.Init();
});
  