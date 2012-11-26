   var CustomerTags="";
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
       var reviewedCustomerName = '';
       CustomerTags = {
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
               CustomerTags.LoadCustoerTagsStaticImage();
               CustomerTags.BindCustomerTags();
               CustomerTags.HideDiv();
               $("#divCustomerTagDetails").show();
               $("#btnBack").click(function() {
                   CustomerTags.HideDiv();
                   $("#divCustomerTagDetails").show();
               });
               $("#<%=btnExportDataToExcel.ClientID %>").click(function() {
                   CustomerTags.ExportDataToExcel();
               });
               $("#<%=btnExportToExcel.ClientID %>").click(function() {
                   CustomerTags.ExportDivDataToExcel();
               });
           },
           ajaxCall: function(config) {
               $.ajax({
                   type: CustomerTags.config.type,
                   contentType: CustomerTags.config.contentType,
                   cache: CustomerTags.config.cache,
                   async: CustomerTags.config.async,
                   data: CustomerTags.config.data,
                   dataType: CustomerTags.config.dataType,
                   url: CustomerTags.config.url,
                   success: CustomerTags.ajaxSuccess,
                   error: CustomerTags.ajaxFailure
               });
           },
           LoadCustoerTagsStaticImage: function() {
               $('#ajaxCustomerImageLoad').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
               $('#ajaxCustomerTagsImage2').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
           },

           HideDiv: function() {
               $("#divCustomerTagDetails").hide();
               $("#divShowCustomerTagList").hide();
           },
           GetCustomerTagsDataForExport: function() {
               this.config.url = this.config.baseURL + "GetCustomerTagDetailsList";
               this.config.data = JSON2.stringify({ offset: 1, limit: null, storeId: storeId, portalId: portalId });
               this.config.ajaxCallMode = 1;
               this.ajaxCall(this.config);
           },
           BindCustomerTagsExportData: function(data) {
               var exportData = '<thead><tr><th>Customer Name</th><th>Total Tags</th></tr></thead><tbody>';
               if (data.d.length > 0) {
                   $.each(data.d, function(index, value) {
                       exportData += '<tr><td>' + value.UserName + '</td>';
                       exportData += '<td>' + value.Tag + '</td></tr>';
                   });
               }
               else {
                   exportData += '<tr><td>No Records Found!</td></tr>';
               }
               exportData += '</tbody>';

               $('#CustomerTagExportDataTbl').html(exportData);
               $("input[id$='HdnValue']").val('<table>' + exportData + '</table>');
               $("input[id$='_csvCustomerTagHdn']").val($("#CustomerTagExportDataTbl").table2CSV());
               $("#CustomerTagExportDataTbl").html('');
           },
           BindCustomerTags: function() {
               this.config.method = "GetCustomerTagDetailsList";
               var offset_ = 1;
               var current_ = 1;
               var perpage = ($("#gdvCusomerTag_pagesize").length > 0) ? $("#gdvCusomerTag_pagesize :selected").text() : 10;

               $("#gdvCusomerTag").sagegrid({
                   url: this.config.baseURL,
                   functionMethod: this.config.method,
                   colModel: [
                    { display: 'Customer Name', name: 'user_name', cssclass: '', coltype: 'label', align: 'left' },
                    { display: 'Total Tags', name: 'total_tags', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
    				],

                   buttons: [
                    { display: 'View', name: 'showtags', enable: true, _event: 'click', trigger: '1', callMethod: 'CustomerTags.ShowTagsList', arguments: '0' }
    			    ],
                   rp: perpage,
                   nomsg: "No Records Found!",
                   param: { storeId: storeId, portalId: portalId },
                   current: current_,
                   pnew: offset_,
                   sortcol: { 2: { sorter: false} }
               });
           },

           ShowTagsList: function(tblID, argus) {
               switch (tblID) {
                   case "gdvCusomerTag":
                       $("#" + lblShowHeading).html("Tags Submitted By: '" + argus[3] + "'");
                       CustomerTags.BindShowCustomerTagList(argus[0]);
                       reviewedCustomerName = argus[0];
                       CustomerTags.HideDiv();
                       $("#divShowCustomerTagList").show();
                       break;
               }
           },
           GetCustomerTagDetailListForExport: function() {
               this.config.url = this.config.baseURL + "ShowCustomerTagList";
               this.config.data = JSON2.stringify({ offset: 1, limit: null, storeId: storeId, portalId: portalId, userName: reviewedCustomerName });
               this.config.ajaxCallMode = 2;
               this.ajaxCall(this.config);
           },
           BindShowCustomreTagListExportData: function(data) {
               var exportData = '<thead><tr><th>Item Name</th><th>Tag Name</th><th>Taged On</th></tr></thead><tbody>';
               if (data.d.length > 0) {
                   $.each(data.d, function(index, value) {
                       exportData += '<tr><td>' + value.ItemName + '</td><td>' + value.Tag + '</td>';
                       exportData += '</td><td>' + value.AddedOn + '</td></tr>';
                   });
               }
               else {
                   exportData += '<tr><td>No Records Found!</td></tr>';
               }
               exportData += '</tbody>';

               $('#ShowTagListExportDataTbl').html(exportData);
               $("input[id$='HdnGridData']").val('<table>' + exportData + '</table>');
               $("input[id$='_csvCustomerTagDetailHdn']").val($("#ShowTagListExportDataTbl").table2CSV());
               $("#ShowTagListExportDataTbl").html('');
           },
           BindShowCustomerTagList: function(UserName) {
               //var UserName = argus[0];
               this.config.method = "ShowCustomerTagList";
               var offset_ = 1;
               var current_ = 1;
               var perpage = ($("#grdShowTagsList_pagesize").length > 0) ? $("#grdShowTagsList_pagesize :selected").text() : 10;

               $("#grdShowTagsList").sagegrid({
                   url: this.config.baseURL,
                   functionMethod: this.config.method,
                   colModel: [
                    { display: 'Item ID', name: 'itemId', cssclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Item Name', name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Tag Name', name: 'tag_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Taged On', name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', format: 'yyyy/MM/dd' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center', hide: true }
    				],

                   buttons: [
    			    ],
                   rp: perpage,
                   nomsg: "No Records Found!",
                   param: { storeId: storeId, portalId: portalId, userName: UserName },
                   current: current_,
                   pnew: offset_,
                   sortcol: { 4: { sorter: false} }
               });
           },
           ExportCustomertagToCsvData: function() {
               CustomerTags.GetCustomerTagsDataForExport();
               // $("input[id$='_csvCustomerTagHdn']").val($('#gdvCusomerTag').table2CSV());
           },
           ExportCustomertagDetailToCsvData: function() {
               CustomerTags.GetCustomerTagDetailListForExport();
               //$("input[id$='_csvCustomerTagDetailHdn']").val($('#grdShowTagsList').table2CSV());
           },
           ExportCustomerTagDataToExcel: function() {
               CustomerTags.GetCustomerTagsDataForExport();
               //               var headerArr = $("#gdvCusomerTag thead tr th");
               //               var header = "<tr>";
               //               $.each(headerArr, function() {
               //                   if (!$(this).hasClass("cssClassAction")) {
               //                       header += '<th>' + $(this).text() + '</th>';
               //                   }
               //               });
               //               header += '</tr>'
               //               var data = $("#gdvCusomerTag tbody tr");
               //               // var table = $("#Export1_lblTitle").text();
               //               var table = '<table>';
               //               table += header;
               //               $.each(data, function(index, item) {
               //                   var cells = $(this).find("td");
               //                   var td = "";
               //                   $.each(cells, function(i, itm) {

               //                       if ($(this).find("div").hasClass("cssClassActionOnClick")) {
               //                           //do not add
               //                       }
               //                       else {
               //                           td += '<td>' + $(this).text() + '</td>';
               //                       }
               //                   });
               //                   table += '<tr>' + td + '</tr>';
               //               });

               //               table += '</tr></table>';
               //               table = $.trim(table);
               //               table = table.replace(/>/g, '&gt;');
               //               table = table.replace(/</g, '&lt;');
               //               $("input[id$='HdnValue']").val(table);
           },

           ExportCustomerTagListDataToExcel: function() {
               CustomerTags.GetCustomerTagDetailListForExport();
               //               var headerArr = $("#grdShowTagsList thead tr th");
               //               var header = "<tr>";
               //               $.each(headerArr, function() {
               //                   if (!$(this).hasClass("cssClassAction")) {
               //                       header += '<th>' + $(this).text() + '</th>';
               //                   }
               //               });
               //               header += '</tr>'
               //               var data = $("#grdShowTagsList tbody tr");
               //               // var table = $("#Export1_lblTitle").text();
               //               var table = '<table>';
               //               table += header;
               //               $.each(data, function(index, item) {
               //                   var cells = $(this).find("td");
               //                   var td = "";
               //                   $.each(cells, function(i, itm) {

               //                       if ($(this).find("div").hasClass("cssClassActionOnClick")) {
               //                           //do not add
               //                       }
               //                       else {
               //                           td += '<td>' + $(this).text() + '</td>';
               //                       }
               //                   });
               //                   table += '<tr>' + td + '</tr>';
               //               });

               //               table += '</tr></table>';
               //               table = $.trim(table);
               //               table = table.replace(/>/g, '&gt;');
               //               table = table.replace(/</g, '&lt;');
               //               $("input[id$='HdnGridData']").val(table);
           },
           ajaxSuccess: function(data) {
               switch (CustomerTags.config.ajaxCallMode) {
                   case 0:
                       break;
                   case 1:
                       CustomerTags.BindCustomerTagsExportData(data);
                   case 2:
                       CustomerTags.BindShowCustomreTagListExportData(data);
                       break;
               }
           }
       }
       CustomerTags.init();
   });