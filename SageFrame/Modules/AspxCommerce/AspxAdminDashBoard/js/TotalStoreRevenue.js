﻿$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var totalStoreRevenue = {
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
                type: totalStoreRevenue.config.type,
                contentType: totalStoreRevenue.config.contentType,
                cache: totalStoreRevenue.config.cache,
                async: totalStoreRevenue.config.async,
                url: totalStoreRevenue.config.url,
                data: totalStoreRevenue.config.data,
                dataType: totalStoreRevenue.config.dataType,
                success: totalStoreRevenue.ajaxSuccess,
                error: totalStoreRevenue.ajaxFailure
            });
        },
        GetTotalOrdererRevenueAdmindash: function() {
            this.config.url = this.config.baseURL + "GetTotalOrderAmountAdmindash";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (totalStoreRevenue.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    if (msg.d.length > 0) {
                        var bodyElements = '';
                        var headELements = '';
                        headELements += '<table class="classTableWrapper"  width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>';
                        headELements += '<tr class="cssClassHeading"><td class="cssClassNormalHeading">Revenue</td>';
                        headELements += '<td class="cssClassNormalHeading">Tax</td>';
                        headELements += '<td class="cssClassNormalHeading">Shipping Cost</td>';
                        headELements += '<td class="cssClassNormalHeading">Quantity</td>';
                        headELements += '</tr></tbody></table>';
                        $("#divTotalOrderRevenueAdmindash").html(headELements);

                        $.each(msg.d, function(index, value) {
                        bodyElements += '<tr class="cssClassAlignRight"><td><label class="cssClassLabel cssClassFormatCurrency">' + value.Revenue.toFixed(2) + '</label></td>';
                        bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + value.TaxTotal.toFixed(2) + '</label></td>';
                        bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + value.ShippingCost.toFixed(2) + '</label></td>';
                            bodyElements += '<td><label class="cssClassLabel">' + value.Quantity + '</label>';
                            bodyElements += '</tr>';
                        });
                        $("#divTotalOrderRevenueAdmindash").find('table>tbody').append(bodyElements);
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        $(".classTableWrapper > tbody tr:even").addClass("cssClassAlternativeEven");
                        $(".classTableWrapper > tbody tr:odd").addClass("cssClassAlternativeOdd");
                    }
                    else {
                        $("#divTotalOrderRevenueAdmindash").html("<span class=\"cssClassNotFound\">No Data Found!!</span>");
                    }
                    break;
            }
        },
        ajaxFailure: function(msg) {
            switch (totalStoreRevenue.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>Error Message</h1><p>Failed to load Total Store Revenue.</p>');
                    break;
            }
        },
        init: function(config) {
            totalStoreRevenue.GetTotalOrdererRevenueAdmindash();
        }
    }
    totalStoreRevenue.init();
});
