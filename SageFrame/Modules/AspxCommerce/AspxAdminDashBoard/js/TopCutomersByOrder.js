$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var TopCustomerOrderCount = 5;
    var topCutomersByOrder = {
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
                type: topCutomersByOrder.config.type,
                contentType: topCutomersByOrder.config.contentType,
                cache: topCutomersByOrder.config.cache,
                async: topCutomersByOrder.config.async,
                url: topCutomersByOrder.config.url,
                data: topCutomersByOrder.config.data,
                dataType: topCutomersByOrder.config.dataType,
                success: topCutomersByOrder.ajaxSuccess,
                error: topCutomersByOrder.ajaxFailure
            });
        },
        GetTopCustomerOrdererAdmindash: function() {
            this.config.url = this.config.baseURL + "GetTopCustomerOrderAdmindash";
            this.config.data = JSON2.stringify({ count: TopCustomerOrderCount, storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (topCutomersByOrder.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    if (msg.d.length > 0) {
                        var bodyElements = '';
                        var headELements = '';
                        headELements += '<table class="classTableWrapper"  width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>';
                        headELements += '<tr class="cssClassHeading"><td class="cssClassNormalHeading">Customer Name</td>';
                        headELements += '<td class="cssClassNormalHeading">Number of Order</td>';
                        headELements += '<td class="cssClassNormalHeading">Average Order Amount</td>';
                        headELements += '<td class="cssClassNormalHeading">Total Order Amount</td>';
                        headELements += '</tr></tbody></table>';
                        $("#divTopCustomerOrderAdmindash").html(headELements);

                        $.each(msg.d, function(index, value) {
                            bodyElements += '<tr><td><label class="cssClassLabel">' + value.CustomerName + '</label></td>';
                            bodyElements += '<td><label class="cssClassLabel">' + value.NumberOfOrder + '</label></td>';
                            bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + value.AverageOrderAmount.toFixed(2) + '</label></td>';
                            bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + value.TotalOrderAmount.toFixed(2) + '</label>';
                            bodyElements += '</tr>';
                        });
                        $("#divTopCustomerOrderAdmindash").find('table>tbody').append(bodyElements);
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        $(".classTableWrapper > tbody tr:even").addClass("cssClassAlternativeEven");
                        $(".classTableWrapper > tbody tr:odd").addClass("cssClassAlternativeOdd");
                    }
                    else {
                        $("#divTopCustomerOrderAdmindash").html("<span class=\"cssClassNotFound\">&nbsp;&nbsp;&nbsp;No Data Found!!</span>");
                    }
                    break;
            }
        },
        ajaxFailure: function(msg) {
            switch (topCutomersByOrder.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>Error Message</h1><p>Failed to load Top Customers.</p>');
                    break;
            }
        },
        init: function(config) {
            topCutomersByOrder.GetTopCustomerOrdererAdmindash();
        }
    }
    topCutomersByOrder.init();
});