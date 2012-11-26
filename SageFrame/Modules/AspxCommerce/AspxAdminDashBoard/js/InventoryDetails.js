$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID(); 

    var inventoryDetails = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCommerceWebService.asmx/",
            method: "",
            url: "",
            ajaxCallMode: 0
        },
        ajaxCall: function(config) {
            $.ajax({
                type: inventoryDetails.config.type,
                contentType: inventoryDetails.config.contentType,
                cache: inventoryDetails.config.cache,
                async: inventoryDetails.config.async,
                url: inventoryDetails.config.url,
                data: inventoryDetails.config.data,
                dataType: inventoryDetails.config.dataType,
                success: inventoryDetails.ajaxSuccess,
                error: inventoryDetails.ajaxFailure
            });
        },
        GetInventoryDetails: function() {
            this.config.url = this.config.baseURL + "GetInventoryDetails";
            this.config.data = JSON2.stringify({ count: lowStock, storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (inventoryDetails.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    if (msg.d.length > 0) {
                        $.each(msg.d, function(index, item) {
                            $('#lblItemtotal').html(item.TotalItem);
                            $('#lblAvtive').html(item.Active);
                            $('#lblHidden').html(item.Hidden);
                            $('#lblDownloadable').html(item.DItemscountNo);
                            $('#lblSpecial').html(item.SItemsCountNo);
                            $('#lblLowstock').html(item.LowStockItemCount);
                        });
                    }
                    else {
                        $('#lblItemtotal').html('0');
                        $('#lblAvtive').html('0');
                        $('#lblHidden').html('0');
                        $('#lblDownloadable').html('0');
                        $('#lblSpecial').html('0');
                        $('#lblLowstock').html('0');
                    }
                    break;
            }
        },
        ajaxFailure: function(data) {
            switch (inventoryDetails.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>Error Message</h1><p>Failed to load Inventory Deatils.</p>');
                    break;
            }
        },
        init: function() {
            inventoryDetails.GetInventoryDetails();
        }
    }
    inventoryDetails.init();
});