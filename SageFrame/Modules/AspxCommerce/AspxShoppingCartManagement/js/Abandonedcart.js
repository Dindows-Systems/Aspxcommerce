var AbandonedCart="";
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
    AbandonedCart = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: aspxservicePath + "AspxCommerceWebService.asmx/",
            url: "",
            method: ""
        },
        ajaxCall: function(config) {
            $.ajax({
                type: AbandonedCart.config.type,
                contentType: AbandonedCart.config.contentType,
                cache: AbandonedCart.config.cache,
                async: AbandonedCart.config.async,
                url: AbandonedCart.config.url,
                data: AbandonedCart.config.data,
                dataType: AbandonedCart.config.dataType,
                success: AbandonedCart.ajaxSuccess,
                error: AbandonedCart.ajaxFailure
            });
        },
        init: function() {
            AbandonedCart.LoadAbandonAndLiveStaticImage();
            AbandonedCart.BindAbandonedCart(null);
            $("#btnAbandonedSearch").live("click", function() {
                AbandonedCart.SearchAbandonedShoppingCart();
            });
            $('#txtAbdCustomerName').keyup(function(event) {
                if (event.keyCode == 13) {
                    AbandonedCart.SearchAbandonedShoppingCart();
                }
            });
        },

        LoadAbandonAndLiveStaticImage: function() {
            $('#ajaxAbandonAndliveImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        GetAbandonCartsDataForExport: function() {
            this.config.url = this.config.baseURL + "GetAbandonedCartDetails";
            this.config.data = JSON2.stringify({ offset: 1, limit: null, storeID: storeId, portalID: portalId, userName: null, cultureName: cultureName, timeToAbandonCart: timeToAbandonCart });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        BindAbandonCartForExport: function(data) {
            var exportData = '<thead><tr><th>Customer Name</th><th>Number Of Items</th><th>Quantity Of Items</th><th>SubTotal</th><th>Actions</th></tr></thead><tbody>';
            if (data.d.length > 0) {
                $.each(msg.d, function(index, value) {
                    exportData += '<tr><td>' + value.CartID + '</td><td>' + value.ItemID + '</td>';
                    exportData += '<td>' + value.UserName + '</td><td>' + value.ItemName + '</td>';
                    exportData += '<td>' + value.SKU + '</td></tr>';
                });
            }
            else {
                exportData += '<tr><td>No Records Found!</td></tr>';
            }
            exportData += '</tbody>';
            $('#AbandonCartExportDataTbl').html(exportData);
            $("input[id$='hdnAbandonedCartValue']").val('<table>' + exportData + '</table>');
            $("input[id$='_cssAbandonCartHiddenValue']").val($("#AbandonCartExportDataTbl").table2CSV());
            $("#AbandonCartExportDataTbl").html('');
        },

        BindAbandonedCart: function(CstNm) {
            this.config.method = "GetAbandonedCartDetails";
            this.config.data = { storeID: storeId, portalID: portalId, userName: CstNm, cultureName: cultureName, timeToAbandonCart: timeToAbandonCart };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvAbandonedCart_pagesize").length > 0) ? $("#gdvAbandonedCart_pagesize :selected").text() : 10;

            $("#gdvAbandonedCart").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
				{ display: 'Customer Name', name: 'user_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				{ display: 'Number Of Items', name: 'number_OfItems', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
				{ display: 'Quantity Of Items', name: 'quantity_OfItems', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
				{ display: 'SubTotal', name: 'subTotal', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'left' },
                { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center', hide: true }
				],
                rp: perpage,
                nomsg: "No Records Found!",
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 4: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },
        ExportAbandonCartCsvData: function() {
            AbandonedCart.GetAbandonCartsDataForExport();
        },
        ExportAbandonedCartDataToExcel: function() {
            AbandonedCart.GetAbandonCartsDataForExport();
        },
        ajaxSuccess: function(data) {
            switch (AbandonedCart.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    AbandonedCart.BindAbandonCartForExport(data);
                    break;
            }
        },
        SearchAbandonedShoppingCart: function() {
            var CstNm = $.trim($("#txtAbdCustomerName").val());
            if (CstNm.length < 1) {
                CstNm = null;
            }
            AbandonedCart.BindAbandonedCart(CstNm);
        }
    }
    AbandonedCart.init();
});
    