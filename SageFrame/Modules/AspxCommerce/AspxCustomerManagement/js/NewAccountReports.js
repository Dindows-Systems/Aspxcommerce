var NewAccountReport;
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
    NewAccountReport = {
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
        init: function() {
            NewAccountReport.LoadCustomerNewAccountStaticImage();
            NewAccountReport.BindNewAccountReport(true, false, false);
            $("#ddlNewAccountReport").change(function() {
                NewAccountReport.ShowReport();
            });
//            $("#btnExportToCSV").click(function() {
//                $('#gdvNewAccountList').table2CSV();
//            });
            $("#" + btnExportToExcelNAR).click(function() {
                NewAccountReport.ExportNARDivDataToExcel()
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: NewAccountReport.config.type,
                contentType: NewAccountReport.config.contentType,
                cache: NewAccountReport.config.cache,
                async: NewAccountReport.config.async,
                url: NewAccountReport.config.url,
                data: NewAccountReport.config.data,
                dataType: NewAccountReport.config.dataType,
                success: NewAccountReport.ajaxSuccess,
                error: NewAccountReport.ajaxFailure
            });
        },
        LoadCustomerNewAccountStaticImage: function() {
            $('#ajaxNewAccountReportImageLoad').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        ShowReport: function() {
            var selectreport = $("#ddlNewAccountReport").val();
            switch (selectreport) {
                case '1':
                    NewAccountReport.BindNewAccountReport(true, false, false);
                    break;
                case '2':
                    NewAccountReport.BindNewAccountReport(false, true, false);
                    break;
                case '3':
                    NewAccountReport.BindNewAccountReport(false, false, true);
                    break;
            }
        },
        GetNewAccountDataForExport: function() {
            var Monthly = false;
            var Weekly = false;
            var Hourly = false;
            var reportType = $('#ddlNewAccountReport  option:selected').val();
            if (reportType == '1') {
                Monthly = true;
            }
            else if (reportType == '2') {
                Weekly = true;
            }
            else if (reportType == '3') {
                Hourly = true;
            }
            this.config.url = this.config.baseURL + "GetNewAccounts";
            this.config.data = JSON2.stringify({ offset: 1, limit: null, storeID: storeId, portalID: portalId, cultureName: cultureName, monthly: Monthly, weekly: Weekly, hourly: Hourly });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        BinNewAccountExportData: function(msg) {
            var exportData = '<thead><tr><th>Period</th><th>Number Of New Accounts</th></tr><tbody>';
            if (msg.d.length > 0) {
                $.each(msg.d, function(index, value) {
                    exportData += '<tr><td>' + value.AddedOn + '</td><td>' + value.NumberOfNewAccounts + '</td></tr>';
                });
            }
            else {
                exportData += '<tr><td>No Records Found!</td></tr>';
            }
            exportData += '</tbody>';
            $('#NewAccountExportDataTbl').html(exportData);
            $("input[id$='HdnValue']").val('<table>' + exportData + '</table>');
            $("input[id$='_csvNewAccountHiddenValue']").val($('#NewAccountExportDataTbl').table2CSV());
            $('#NewAccountExportDataTbl').html('');
        },

        BindNewAccountReport: function(monthly, weekly, hourly) {
            this.config.method = "GetNewAccounts";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvNewAccountList_pagesize").length > 0) ? $("#gdvNewAccountList_pagesize :selected").text() : 10;

            $("#gdvNewAccountList").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'Period', name: 'period', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Number Of New Accounts', name: 'new_acounts', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center', hide: true }
    				],

                buttons: [

    			    ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { storeID: storeId, portalID: portalId, cultureName: cultureName, monthly: monthly, weekly: weekly, hourly: hourly },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 1: { sorter: false }, 13: { sorter: false} }
            });
        },
        ajaxSuccess: function(msg) {
            switch (NewAccountReport.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    NewAccountReport.BinNewAccountExportData(msg);
                    break;
            }
        },
        ExportNewAccountToCsvData: function() {
            NewAccountReport.GetNewAccountDataForExport();          
        },
        ExportNARDivDataToExcel: function() {
            NewAccountReport.GetNewAccountDataForExport();     
        }
    }
    NewAccountReport.init();
});