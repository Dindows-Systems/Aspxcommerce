$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var StaticOrderStatusCount = 10;
    var orderOverViews = {
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
                type: orderOverViews.config.type,
                contentType: orderOverViews.config.contentType,
                cache: orderOverViews.config.cache,
                async: orderOverViews.config.async,
                url: orderOverViews.config.url,
                data: orderOverViews.config.data,
                dataType: orderOverViews.config.dataType,
                success: orderOverViews.ajaxSuccess,
                error: orderOverViews.ajaxFailure
            });
        },
        GetStaticOrderStatusAdminDash: function() {
            this.config.url = this.config.baseURL + "GetStaticOrderStatusAdminDash";
            this.config.data = JSON2.stringify({ count: StaticOrderStatusCount, storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (orderOverViews.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    if (msg.d.length > 0) {
                        var bodyElements = '';
                        var headELements = '';
                        headELements += '<table class="classTableWrapper" width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>';
                        headELements += '';
                        headELements += '<tr class="cssClassHeading"><td >Status Name</td>';
                        headELements += '<td >This Day</td>';
                        headELements += '<td >This Week</td>';
                        headELements += '<td >This Month</td>';
                        headELements += '<td >This Year</td>';
                        headELements += '</tr></tbody></table>';
                        $("#divStaticOrderStatusAdmindash").html(headELements);

                        $.each(msg.d, function(index, value) {

                            var last = msg.d.length;
                            if (index != last - 1) {
                                bodyElements += '<tr ><td><label class="cssClassLabel">' + value.StatusName + '</label></td>';
                                bodyElements += '<td><label class="cssClassLabel">' + value.ThisDay + '</label></td>';
                                bodyElements += '<td><label class="cssClassLabel">' + value.ThisWeek + '</label></td>';
                                bodyElements += '<td><label class="cssClassLabel">' + value.ThisMonth + '</label></td>';
                                bodyElements += '<td><label class="cssClassLabel">' + value.ThisYear + '</label>';
                                bodyElements += '</tr>';
                            }
                            else {
                                bodyElements += '<tr ><td><label class="cssClassLabel">' + value.StatusName + '</label></td>';
                                bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + value.ThisDay.toFixed(2) + '</label></td>';
                                bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + value.ThisWeek.toFixed(2) + '</label></td>';
                                bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + value.ThisMonth.toFixed(2) + '</label></td>';
                                bodyElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + value.ThisYear.toFixed(2) + '</label>';
                                bodyElements += '</tr >';
                            }
                        });

                        $("#divStaticOrderStatusAdmindash").find('table>tbody').append(bodyElements);
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        $(".classTableWrapper > tbody tr:even").addClass("cssClassAlternativeEven");
                        $(".classTableWrapper > tbody tr:odd").addClass("cssClassAlternativeOdd");
                    }
                    else {
                        $("#divStaticOrderStatusAdmindash").html("<span class=\"cssClassNotFound\">No Data Found!!</span>");
                    }
                    break;
            }
        },
        ajaxFailure: function(msg) {
            switch (orderOverViews.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>Error Message</h1><p>Failed to load Order Overview.</p>');
                    break;
            }
        },
        init: function(config) {
            orderOverViews.GetStaticOrderStatusAdminDash();
        }
    }
    orderOverViews.init();
});