 var SearchTerm="";

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
     SearchTerm = {
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
             SearchTerm.LoadSearchTermStaticImage();
             SearchTerm.GetSearchTermDetails(null);
             $("#btnDeleteAllSearchTerm").click(function() {
                 var searchTermIds = '';
                 $(".searchTermCheckbox").each(function() {
                     if ($(this).attr("checked")) {
                         searchTermIds += $(this).val() + ',';
                     }
                 });
                 if (searchTermIds == "") {
                     csscody.alert('<h2>Information Alert</h2><p>Please select at least search term(s) before delete.</p>');
                     return false;
                 }
                 var properties = {
                     onComplete: function(e) {
                         SearchTerm.DeleteSearchTerm(searchTermIds, e);
                     }
                 };
                 csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete selected search term(s)?</p>", properties);

             });
             $("#btnExportToCSV").click(function() {
                 $('#gdvSearchTerm').table2CSV();
             });
             $("#btnSearchTerm").click(function() {
                 SearchTerm.SearchTerm();
             });
             $('#txtSearchTerm').keyup(function(event) {
                 if (event.keyCode == 13) {
                     $("#btnSearchTerm").click();
                 }
             });
             $("#" + btnExportToExcel).click(function() {
                 SearchTerm.ExportDivDataToExcel();
             });
         },
         LoadSearchTermStaticImage: function() {
             $('#ajaxSearchTermImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
         },

         ajaxCall: function(config) {
             $.ajax({
                 type: SearchTerm.config.type,
                 contentType: SearchTerm.config.contentType,
                 cache: SearchTerm.config.cache,
                 async: SearchTerm.config.async,
                 url: SearchTerm.config.url,
                 data: SearchTerm.config.data,
                 dataType: SearchTerm.config.dataType,
                 success: SearchTerm.ajaxSuccess,
                 error: SearchTerm.ajaxFailure
             });
         },

         ExportDivDataToExcel: function() {
             var headerArr = $("#gdvSearchTerm thead tr th");
             var header = "<tr>";
             $.each(headerArr, function() {
                 if (!$(this).hasClass("cssClassAction")) {
                     header += '<th>' + $(this).text() + '</th>';
                 }
             });
             header += '</tr>'
             var data = $("#gdvSearchTerm tbody tr");
             // var table = $("#Export1_lblTitle").text();
             var table = '<table>';
             table += header;
             $.each(data, function(index, item) {
                 var cells = $(this).find("td");
                 var td = "";
                 $.each(cells, function(i, itm) {

                     if ($(this).find("div").hasClass("cssClassActionOnClick")) {
                         //do not add
                     } else {
                         td += '<td>' + $(this).text() + '</td>';
                     }
                 });
                 table += '<tr>' + td + '</tr>';
             });

             table += '</tr></table>';
             table = $.trim(table);
             table = table.replace( />/g , '&gt;');
             table = table.replace( /</g , '&lt;');
             $("input[id$='HdnValue']").val(table);
         },

         GetSearchTermDetails: function(searchTerm) {
             this.config.method = "ManageSearchTerms";
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvSearchTerm_pagesize").length > 0) ? $("#gdvSearchTerm_pagesize :selected").text() : 10;

             $("#gdvSearchTerm").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                     { display: 'SearchTermID', name: 'search_term_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'searchTermCheckbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                     { display: 'Search Term', name: 'search_term', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: 'No Of Use', name: 'no_of_use', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                     { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                 ],
                 buttons: [
                 // { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'EditSearchTerms',arguments: '2,3,5' },
                     { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'SearchTerm.DeleteSearchTerms', arguments: '' }
                 ],
                 rp: perpage,
                 nomsg: "No Records Found!",
                 param: { storeID: storeId, portalID: portalId, cultureName: cultureName, searchTerm: searchTerm },
                 current: current_,
                 pnew: offset_,
                 sortcol: { 0: { sorter: false }, 3: { sorter: false } }
             });
         },

         DeleteSearchTerms: function(tblID, argus) {
             switch (tblID) {
             case "gdvSearchTerm":
                 var properties = {
                     onComplete: function(e) {
                         SearchTerm.DeleteSearchTerm(argus[0], e);
                     }
                 };
                 csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this search term?</p>", properties);
                 break;
             default:
                 break;
             }
         },

         DeleteSearchTerm: function(Ids, event) {
             if (event) {
                 this.config.url = this.config.baseURL + "DeleteSearchTerm";
                 this.config.data = JSON2.stringify({ searchTermID: Ids, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
                 this.config.ajaxCallMode = 1;
                 this.ajaxCall(this.config);
             }
             return false;
         },

         SearchTerm: function() {
             var search = $.trim($("#txtSearchTerm").val());
             if (search.length < 1) {
                 search = null;
             }
             SearchTerm.GetSearchTermDetails(search);
         },
         ajaxSuccess: function() {
             switch (SearchTerm.config.ajaxCallMode) {
             case 0:
                 break;
             case 1:
                 SearchTerm.GetSearchTermDetails(null);
                 csscody.info("<h2>Sucessful Message</h2><p>Search term has been deleted sucessfully.</p>");
                 break;
             }
         }
     };
     SearchTerm.init();
 });