var couponPerSalesMgmt;
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var customerId = AspxCommerce.utils.GetCustomerID();
    var ip = AspxCommerce.utils.GetClientIP();
    var countryName = AspxCommerce.utils.GetAspxClientCoutry();
    var sessionCode = AspxCommerce.utils.GetSessionCode();
    var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();

    couponPerSalesMgmt = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxservicePath + "AspxCommerceWebService.asmx/",
            method: "",
            url: "",
            ajaxCallMode: 0
        },

        ajaxCall: function() {
            $.ajax({
                type: couponPerSalesMgmt.config.type,
                contentType: couponPerSalesMgmt.config.contentType,
                cache: couponPerSalesMgmt.config.cache,
                async: couponPerSalesMgmt.config.async,
                url: couponPerSalesMgmt.config.url,
                data: couponPerSalesMgmt.config.data,
                dataType: couponPerSalesMgmt.config.dataType,
                success: couponPerSalesMgmt.ajaxSuccess,
                error: couponPerSalesMgmt.ajaxFailure
            });
        },

        LoadCouponPerSalesStaticImage: function() {
            $('#ajaxCouponPerSalesImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        GetCouponPerSalesDataForExport: function() {
            this.config.url = this.config.baseURL + "GetCouponDetailsPerSales";
            this.config.data = JSON2.stringify({ offset: 1, limit: null, couponCode: null, storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        BindCouponPerSalesExportData: function(data) {
            var exportData = '<thead><tr><th>Coupon Code</th><th>Number Of Uses</th><th>Total Discount Amount Gained By Coupon</th><th>Total Sales Amount</th></tr></thead><tbody>';
            if (data.d.length > 0) {
                $.each(data.d, function(index, value) {
                    alert('inside');
                    exportData += '<tr><td>' + value.CouponCode + '</td><td>' + value.UseCount + '</td>';
                    exportData += '<td>' + value.TotalAmountDiscountedbyCoupon + '</td><td>' + value.TotalSalesAmount + '</td>';
                });
            } else {
                exportData += '<tr><td>No Records Found!</td></tr>';
            }
            exportData += '</tbody>';
            $('#CouponPerSalesExportDataTbl').html(exportData);
            $("input[id$='HdnValue']").val('<table>' + exportData + '</table>');
            $("input[id$='_csvCouponPerSalesHiddenValue']").val($('#CouponPerSalesExportDataTbl').table2CSV());
            $('#CouponPerSalesExportDataTbl').html('');
        },
        BindAllCouponPerSalesList: function(SearchCouponCode) {
            this.config.method = "GetCouponDetailsPerSales";
            this.config.url = this.config.baseURL;
            this.config.data = { couponCode: SearchCouponCode, storeID: storeId, portalID: portalId };
            var data = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCouponPerSales_pagesize").length > 0) ? $("#gdvCouponPerSales_pagesize :selected").text() : 10;

            $("#gdvCouponPerSales").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'Coupon Code', name: 'coupon_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Number Of Uses', name: 'number_of_uses', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Total Discount Amount Gained By Coupon', name: 'discount_amount', cssclass: 'cssClassHeadNumber', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: 'Total Sales Amount', name: 'sales_amount', cssclass: 'cssClassHeadNumber', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' }
                ],

                rp: perpage,
                nomsg: "No Records Found!",
                param: data, //{ couponCode: SearchCouponCode, storeID: storeId, portalID: portalId },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 3: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },

        SearchItems: function() {
            var coupName = $.trim($("#txtSearchNameCoupon").val());

            if (coupName.length < 1) {
                coupName = null;
            }
            couponPerSalesMgmt.BindAllCouponPerSalesList(coupName);
        },
        ExportCouponPerSaleToCsvData: function() {
            couponPerSalesMgmt.GetCouponPerSalesDataForExport();
        },
        ExportCouponPerSalesDivDataToExcel: function() {
            couponPerSalesMgmt.GetCouponPerSalesDataForExport();
        },
        ajaxSuccess: function(data) {
            switch (couponPerSalesMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    couponPerSalesMgmt.BindCouponPerSalesExportData(data);
                    break;
            }
        },

        init: function() {
            couponPerSalesMgmt.LoadCouponPerSalesStaticImage();
            couponPerSalesMgmt.BindAllCouponPerSalesList(null);
            
            $("#" + couponPerSalesDataToExcel).bind('click', function() {
                couponPerSalesMgmt.ExportCouponPerSalesDivDataToExcel();
            });
            $('#txtSearchNameCoupon').keyup(function(event) {
                if (event.keyCode == 13) {
                    couponPerSalesMgmt.SearchItems();
                }
            });
        }
    };
    couponPerSalesMgmt.init();
});