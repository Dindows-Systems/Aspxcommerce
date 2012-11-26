$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var MostViewedItemCount = 5;

    var mostViewedItems = {
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
                type: mostViewedItems.config.type,
                contentType: mostViewedItems.config.contentType,
                cache: mostViewedItems.config.cache,
                async: mostViewedItems.config.async,
                url: mostViewedItems.config.url,
                data: mostViewedItems.config.data,
                dataType: mostViewedItems.config.dataType,
                success: mostViewedItems.ajaxSuccess,
                error: mostViewedItems.ajaxFailure
            });
        },
        GetMostViewedItemAdmindash: function() {
            this.config.url = this.config.baseURL + "GetMostViwedItemAdmindash";
            this.config.data = JSON2.stringify({ count: MostViewedItemCount, storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (mostViewedItems.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    if (msg.d.length > 0) {
                        var bodyElements = '';
                        var headELements = '';
                        headELements += '<table class="classTableWrapper"  width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>';
                        headELements += '<tr class="cssClassHeading"><td class="cssClassNormalHeading">Item Name</td>';
                        headELements += '<td class="cssClassNormalHeading">Price</td>';
                        headELements += '<td class="cssClassNormalHeading">Number of View</td>';
                        headELements += '</tr></tbody></table>';
                        $("#divMostViewedItemAdmindash").html(headELements);

                        $.each(msg.d, function(index, value) {
                            bodyElements += '<tr><td><label class="cssClassLabel">' + value.ItemTypeName + '</label></td>';
                            bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + value.Price.toFixed(2) + '</label></td>';
                            bodyElements += '<td><label class="cssClassLabel">' + value.ViewCount + '</label>';
                            bodyElements += '</tr>';
                        });
                        $("#divMostViewedItemAdmindash").find('table>tbody').append(bodyElements);
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        $(".classTableWrapper > tbody tr:even").addClass("cssClassAlternativeEven");
                        $(".classTableWrapper > tbody tr:odd").addClass("cssClassAlternativeOdd");
                    }
                    else {
                        $("#divMostViewedItemAdmindash").html("<span class=\"cssClassNotFound\">&nbsp;&nbsp;&nbsp;No Data Found!!</span>");
                    }
                    break;
            }
        },
        ajaxFailure: function(msg) {
            switch (mostViewedItems.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>Error Message</h1><p>Failed to load Most Viewed Items.</p>');
                    break;
            }
        },
        init: function(config) {
            mostViewedItems.GetMostViewedItemAdmindash();
        }
    }
    mostViewedItems.init();
});
