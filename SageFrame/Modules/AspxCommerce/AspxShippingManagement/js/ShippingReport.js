 var ShippingReport="";
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
     ShippingReport = {
         config: {
             isPostBack: false,
             async: false,
             cache: false,
             type: "POST",
             contentType: "application/json; charset=utf-8",
             baseURL: aspxservicePath + "AspxCommerceWebService.asmx/",
             data: '{}',
             dataType: "json",
             url: "",
             method: ""
         },
         ajaxCall: function(config) {
             $.ajax({
                 type: ShippingReport.config.type,
                 contentType: ShippingReport.config.contentType,
                 cache: ShippingReport.config.cache,
                 async: ShippingReport.config.async,
                 url: ShippingReport.config.url,
                 data: ShippingReport.config.data,
                 dataType: ShippingReport.config.dataType,
                 success: ShippingReport.ajaxSuccess,
                 error: ShippingReport.ajaxFailure
             });
         },
         init: function() {
             ShippingReport.LoadShippingReportStaticImage();
             ShippingReport.ShippedReportDetails(null, true, false, false);
             $("#ddlShippingReport").change(function() {
                 ShippingReport.ShowReport();
             });
             $('#txtShippingMethodNm').keyup(function(event) {
                 if (event.keyCode == 13) {
                     ShippingReport.SearchShippingReport();
                 }
             });
         },
         LoadShippingReportStaticImage: function() {
             $('#ajaxShippingReportImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
         },

         ShowReport: function() {
             var selectreport = $("#ddlShippingReport").val();
             switch (selectreport) {
             case '1':
                 ShippingReport.ShippedReportDetails(null, true, false, false);
                 break;
             case '2':
                 ShippingReport.ShippedReportDetails(null, false, true, false);
                 break;
             case '3':
                 ShippingReport.ShippedReportDetails(null, false, false, true)
                 break;
             }
         },
         GetShippingDataForExport: function() {
             var Monthly = false;
             var Weekly = false;
             var Hourly = false;
             var reportType = $('#ddlShippingReport option:selected').val();
             if (reportType == '1') {
                 Monthly = true;
             } else if (reportType == '2') {
                 Weekly = true;
             } else if (reportType == '3') {
                 Hourly = true;
             }
             this.config.url = this.config.baseURL + "GetShippedDetails";
             this.config.data = JSON2.stringify({ offset: 1, limit: null, storeID: storeId, portalID: portalId, cultureName: cultureName, shippingMethodName: null, monthly: Monthly, weekly: Weekly, hourly: Hourly });
             this.config.ajaxCallMode = 1;
             this.ajaxCall(this.config);
         },
         ShippedReportDetails: function(shippingName, monthly, weekly, hourly) {
             this.config.method = "GetShippedDetails";
             this.config.data = { storeID: storeId, portalID: portalId, cultureName: cultureName, shippingMethodName: shippingName, monthly: monthly, weekly: weekly, hourly: hourly };
             var data = this.config.data;
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvShippedReportDetails_pagesize").length > 0) ? $("#gdvShippedReportDetails_pagesize :selected").text() : 10;

             $("#gdvShippedReportDetails").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                     { display: 'Shipping Method ID', name: 'shipping_methodId', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                     { display: 'Shipping Method Name', name: 'shipping_method_name', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                     { display: 'Number Of Orders', name: 'num_of_orders', cssclass: 'cssClassLinkHeader', controlclass: '', coltype: 'label', align: 'left' },
                     { display: 'Total Shipping', name: 'ship_to_name', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'left' },
                     { display: 'Period', name: 'period', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center', hide: true }
                 ],
                 buttons: [
                 //{ display: 'View', name: 'view', enable: true, _event: 'click', trigger: '3', callMethod: 'ViewShipments', arguments: '1,2,3,4,5,6' },
                 ],
                 rp: perpage,
                 nomsg: "No Records Found!",
                 param: { storeID: storeId, portalID: portalId, cultureName: cultureName, shippingMethodName: shippingName, monthly: monthly, weekly: weekly, hourly: hourly },
                 current: current_,
                 pnew: offset_,
                 sortcol: { o: { sorter: true }, 4: { sorter: false } }
             });
             $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
         },
         BindShippingDataForExport: function(msg) {
             var exportData = '<thead><tr><th>Shipping Method Name</th><th>Number Of Orders</th><th>Total Shipping</th><th>Period</th></tr><tbody>';
             if (msg.d.length > 0) {
                 $.each(msg.d, function(index, value) {
                     exportData += '<tr><td>' + value.ShippingMethodName + '</td><td>' + value.NumberOfOrders + '</td></td>';
                     exportData += '<td>' + value.TotalShipping + '</td><td>' + value.AddedOn + '</td></tr>';
                 });
             } else {
                 exportData += '<tr><td>No Records Found!</td></tr>';
             }
             exportData += '</tbody>';
             $('#ShippingDataExportTbl').html(exportData);
             $("input[id$='HdnValue']").val('<table>' + exportData + '</table>');
             $("input[id$='_csvShippingHiddenValue']").val($('#ShippingDataExportTbl').table2CSV());
             $('#ShippingDataExportTbl').html('');
         },
         ajaxSuccess: function(msg) {
             switch (ShippingReport.config.ajaxCallMode) {
             case 0:
                 break;
             case 1:
                 ShippingReport.BindShippingDataForExport(msg);
                 break;
             }
         },
         ExportShippingToCsvData: function() {
             ShippingReport.GetShippingDataForExport();
         },
         ExportDivDataToExcel: function() {
             ShippingReport.GetShippingDataForExport();
         },
         SearchShippingReport: function() {
             var Nm = $.trim($("#txtShippingMethodNm").val());
             if (Nm.length < 1) {
                 Nm = null;
             }
             var selectreport = $("#ddlShippingReport").val();
             switch (selectreport) {
             case '1':
                 ShippingReport.ShippedReportDetails(Nm, true, false, false);
                 break;
             case '2':
                 ShippingReport.ShippedReportDetails(Nm, false, true, false);
                 break;
             case '3':
                 ShippingReport.ShippedReportDetails(Nm, false, false, true);
                 break;
             }
         }
     };
     ShippingReport.init();
 });