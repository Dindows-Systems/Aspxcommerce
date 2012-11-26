var DownloadedItems;
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
    DownloadedItems = {
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
            url: ""
        },
        init: function() {
            DownloadedItems.LoadDownloadableItemStaticImage();
            DownloadedItems.BindDownLoadableItemsGrid(null, null);
            $("#" + btnExportToExcel).click(function() {
                DownloadedItems.ExportDivDataToExcel();
            });
            $("#btnSearchDownloadedItems").click(function() {
                DownloadedItems.SearchItems();
            });

            $('.cssClassDownload').jDownload({
                root: rootPath,
                dialogTitle: 'AspxCommerce Download Sample Item:'
            });
            $('#txtSearchName,#txtSearchSKU').keyup(function(event) {
                if (event.keyCode == 13) {
                    DownloadedItems.SearchItems();
                }
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: DownloadedItems.config.type,
                contentType: DownloadedItems.config.contentType,
                cache: DownloadedItems.config.cache,
                async: DownloadedItems.config.async,
                url: DownloadedItems.config.url,
                data: DownloadedItems.config.data,
                dataType: DownloadedItems.config.dataType,
                success: DownloadedItems.ajaxSuccess,
                error: DownloadedItems.ajaxFailure
            });
        },
        LoadDownloadableItemStaticImage: function() {
            $('#ajaxDownloadableItemImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        GetOrderedItemsDataForExport: function() {
            this.config.url = this.config.baseURL + "GetDownLoadableItemsList";
            this.config.data = JSON2.stringify({ offset: 1, limit: null, sku: null, name: null, storeId: storeId, portalId: portalId, userName: userName, cultureName: cultureName, CheckUser: false });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ExportDownloadedToCsvData: function() {
            DownloadedItems.GetOrderedItemsDataForExport();
        },
        ExportDivDataToExcel: function() {
            DownloadedItems.GetOrderedItemsDataForExport();
        },

        SearchItems: function() {
            var sku = $.trim($("#txtSearchSKU").val());
            var Nm = $.trim($("#txtSearchName").val());
            if (sku.length < 1) {
                sku = null;
            }
            if (Nm.length < 1) {
                Nm = null;
            }
            var isAct = $.trim($("#ddlIsActive").val()) == "" ? null : ($.trim($("#ddlIsActive").val()) == "True" ? true : false);

            DownloadedItems.BindDownLoadableItemsGrid(sku, Nm);
        },
        BindDownLodableExportData: function(msg) {
            var exportData = '<thead><tr><th>SKU</th><th>Item Name</th><th>Sample Link</th><th>Actual Link</th><th>Purchases</th><th>Downloads</th></tr></thead><tbody>';
            if (msg.d.length > 0) {
                $.each(msg.d, function(index, value) {
                    exportData += '<tr><td>' + value.SKU + '</td><td>' + value.ItemName + '</td>';
                    exportData += '<td>' + value.SampleLink + '</td><td>' + value.ActualLink + '</td>';
                    exportData += '<td>' + value.Purchases + '</td><td>' + value.Downloads + '</td></tr>';
                });
            } else {
                exportData += '<tr><td>No Records Found!</td></tr>';
            }
            exportData += '</tbody>';

            $('#DownLoadableExportDataTbl').html(exportData);
            $("input[id$='HdnValue']").val('<table>' + exportData + '</table>');
            $("input[id$='_csvDownloadedHiddenCsv']").val($("#DownLoadableExportDataTbl").table2CSV());
            $("#DownLoadableExportDataTbl").html('');
        },

        BindDownLoadableItemsGrid: function(sku, Nm) {
            this.config.method = "GetDownLoadableItemsList";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvDownLoadableItems_pagesize").length > 0) ? $("#gdvDownLoadableItems_pagesize :selected").text() : 10;

            $("#gdvDownLoadableItems").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'SKU', name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Item Name', name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Sample Link', name: 'sample_link', cssclass: '', controlclass: 'cssClassDownload', coltype: 'linklabel', align: 'left', value: '4', downloadarguments: '', downloadmethod: '' },
                    { display: 'Actual Link', name: 'actual_link', cssclass: '', controlclass: 'cssClassDownload', coltype: 'linklabel', align: 'left', value: '5', downloadarguments: '', downloadmethod: '' },
                    { display: 'Sample File', name: 'sample_file', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Actual File', name: 'actual_file', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Purchases', name: 'purchase', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Downloads', name: 'download', cssclass: '', controlclass: '', coltype: 'label', align: 'left' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { sku: sku, name: Nm, storeId: storeId, portalId: portalId, userName: userName, cultureName: cultureName, CheckUser: false },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false } }
            });
        },
        ajaxSuccess: function(msg) {
            switch (DownloadedItems.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                DownloadedItems.BindDownLodableExportData(msg);
                break;
            }
        }
    };
    DownloadedItems.init();
});
   