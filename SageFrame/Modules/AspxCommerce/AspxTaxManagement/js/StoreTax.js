  var StoreTax="";
  $(function() {
      var storeId = AspxCommerce.utils.GetStoreID();
      var portalId = AspxCommerce.utils.GetPortalID();
      var userName = AspxCommerce.utils.GetUserName();
      var cultureName = AspxCommerce.utils.GetCultureName();
      StoreTax = {
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
                  type: StoreTax.config.type,
                  contentType: StoreTax.config.contentType,
                  cache: StoreTax.config.cache,
                  async: StoreTax.config.async,
                  url: StoreTax.config.url,
                  data: StoreTax.config.data,
                  dataType: StoreTax.config.dataType,
                  success: StoreTax.ajaxSuccess,
                  error: StoreTax.ajaxFailure
              });
          },
          init: function() {
              StoreTax.LoadStoreTaxImageStaticImage();
              StoreTax.BindStoreTaxesGrid(null, true, false, false);
              $("#ddlTaxReport").change(function() {
                  StoreTax.ShowReport();
              });
              $('#txtSearchName').keyup(function(event) {
                  if (event.keyCode == 13) {
                      StoreTax.SearchItems();
                  }
              });
          },
          LoadStoreTaxImageStaticImage: function() {
              $('#ajaxStoreTaxImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
          },
          GetTaxReportDataForExport: function() {
              var Monthly = false;
              var Weekly = false;
              var Hourly = false;
              var reportType = $("#ddlTaxReport option:selected").val();
              if (reportType == '1') {
                  Monthly = true;
              }
              else if (reportType == '2') {
                  Weekly = true;
              }
              else if (reportType == '3') {
                  Hourly = true;
              }
              this.config.url = this.config.baseURL + "GetStoreSalesTaxes";
              this.config.data = JSON2.stringify({ offset: 1, limit: null, taxRuleName: null, storeID: storeId, portalID: portalId, monthly: Monthly, weekly: Weekly, hourly: Hourly });
              this.config.ajaxCallMode = 1;
              this.ajaxCall(this.config);
          },
          ExportTaxToCsvData: function() {
              StoreTax.GetTaxReportDataForExport();
          },
          ShowReport: function() {
              var selectreport = $("#ddlTaxReport").val();
              switch (selectreport) {
                  case '1':
                      StoreTax.BindStoreTaxesGrid(null, true, false, false);
                      break;
                  case '2':
                      StoreTax.BindStoreTaxesGrid(null, false, true, false);
                      break;
                  case '3':
                      StoreTax.BindStoreTaxesGrid(null, false, false, true)
                      break;
              }
          },
          BindStoreTaxesGrid: function(Nm, monthly, weekly, hourly) {
              this.config.method = "GetStoreSalesTaxes";
              this.config.data = { taxRuleName: Nm, storeID: storeId, portalID: portalId, monthly: monthly, weekly: weekly, hourly: hourly };
              var data = this.config.data;
              var offset_ = 1;
              var current_ = 1;
              var perpage = ($("#gdvStoreTaxes_pagesize").length > 0) ? $("#gdvStoreTaxes_pagesize :selected").text() : 10;
              $("#gdvStoreTaxes").sagegrid({
                  url: this.config.baseURL,
                  functionMethod: this.config.method,
                  colModel: [
                        { display: 'Tax Name', name: 'tax_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				        { display: 'Rate', name: 'rate', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				        { display: 'Quantity', name: 'quantity', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				        { display: 'Is Percent', name: 'is_percent', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
  				        { display: 'No. of orders', name: 'noOfOrders', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'Tax Amount', name: 'taxamount', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                        { display: 'Period', name: 'period', cssclass: '', controlclass: '', coltype: 'label', align: 'left' }
                     ],
                  rp: perpage,
                  nomsg: "No Records Found!",
                  param: { taxRuleName: Nm, storeID: storeId, portalID: portalId, monthly: monthly, weekly: weekly, hourly: hourly },
                  current: current_,
                  pnew: offset_,
                  sortcol: { 0: { sorter: false} }
              });
              $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

          },
          SearchItems: function() {
              var Nm = $.trim($("#txtSearchName").val());
              if (Nm.length < 1) {
                  Nm = null;
              }
              var selectreport = $("#ddlTaxReport").val();
              switch (selectreport) {
                  case '1':
                      StoreTax.BindStoreTaxesGrid(Nm, true, false, false);
                      break;
                  case '2':
                      StoreTax.BindStoreTaxesGrid(Nm, false, true, false);
                      break;
                  case '3':
                      StoreTax.BindStoreTaxesGrid(Nm, false, false, true);
                      break;
              }
          },
          BindStoreTaxDataForExport: function(msg) {
              var exportData = '<thead><tr><th>Tax Name</th><th>Rate</th><th>Quantity</th><th>Is Percent</th><th>No. of orders</th><th>Tax Amount</th><th>Period</th></tr></thead><tbody>';
              if (msg.d.length > 0) {
                  $.each(msg.d, function(index, value) {
                      exportData += '<tr><td>' + value.TaxManageRuleName + '</td><td>' + value.TaxRate + '</td>';
                      exportData += '<td>' + value.Quantity + '</td><td>' + value.IsPercent + '</td>';
                      exportData += '<td>' + value.NoOfOrders + '</td><td>' + value.TotalTaxAmount + '</td>';
                      exportData += '<td>' + value.AddedOn + '</td></tr>';
                  });
              }
              else {
                  exportData += '<tr><td>No Records Found!</td></tr>';
              }
              exportData += '</tbody>';
              $('#storeTaxReportExportTbl').html(exportData);
              $("input[id$='HdnValue']").val('<table>' + exportData + '</table>');
              $("input[id$='_csvTaxHiddenValue']").val($('#storeTaxReportExportTbl').table2CSV());
              $('#storeTaxReportExportTbl').html('');
          },
          ajaxSuccess: function(msg) {
              switch (StoreTax.config.ajaxCallMode) {
                  case 0:
                      break;
                  case 1:
                      StoreTax.BindStoreTaxDataForExport(msg);
                      break;
              }
          },
          ExportDivDataToExcel: function() {
              StoreTax.GetTaxReportDataForExport();
          }
      };
      StoreTax.init();
  });