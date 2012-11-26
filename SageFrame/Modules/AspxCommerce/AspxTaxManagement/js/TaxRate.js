    var TaxRate="";
    $(function() {
        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var taxRateFlag = 0;
        TaxRate = {
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
                TaxRate.HideAll();
                $("#divTaxRatesGrid").show();
                TaxRate.LoadTaxRateStaticImage();
                TaxRate.BindTaxRates(null, null, null, null);
                TaxRate.GetCountryList();
                TaxRate.GetStateList();
                $("#btnAddNewTaxRate").click(function() {
                    $("#ddlState").hide();
                    $("#trZipPostCode").show();
                    $("#trRangeFrom").hide();
                    $("#trRangeTo").hide();
                    TaxRate.ClearForm();
                    TaxRate.HideAll();
                    $("#divTaxRateInformation").show();
                    $("#hdnTaxRateID").val(0);
                });

                $("#ddlCountry").change(function() {
                    if ($(this).val() == "US") {
                        $("#ddlState").show();
                        $("#txtState").hide();
                    }
                    else {
                        $("#ddlState").hide();
                        $("#txtState").show();
                    }
                });

                $("#chkIsTaxZipRange").click(function() {
                    if ($(this).is(':checked')) {
                        $("#trZipPostCode").hide();
                        $("#trRangeFrom").show();
                        $("#trRangeTo").show();
                    }
                    else {
                        $("#trRangeFrom").hide();
                        $("#trRangeTo").hide();
                        $("#trZipPostCode").show();
                    }
                });

                $("#btnDeleteSelected").click(function() {
                    var taxRate_Ids = '';
                    $('.TaxRateChkbox').each(function() {
                        if ($(this).attr('checked')) {
                            taxRate_Ids += $(this).val() + ',';
                        }
                    });
                    if (taxRate_Ids != "") {
                        var properties = {
                            onComplete: function(e) {
                                TaxRate.ConfirmDeleteTaxRates(taxRate_Ids, e);
                            }
                        };
                        csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete the selected tax rate(s)?</p>", properties);
                    }
                    else {
                        csscody.alert('<h2>Information Alert</h2><p>Please select at least one tax rate before delete.</p>');
                    }
                });

                var tr = $("#form1").validate({
                    ignore: ':hidden',
                    rules: {
                        rateTitle: "required",
                        state: "required",
                        zipCode: "required",
                        rangefrom: "required",
                        rangeTo: "required",
                        taxRate: {
                            required: true,
                            number: true
                        }
                    },
                    messages: {
                        rateTitle: "* (at least 2 chars)",
                        state: "* (at least 2 chars)",
                        zipCode: "* (at least 5 chars)",
                        rangefrom: "* (at least 5 chars)",
                        rangeTo: "* (at least 5 chars)",
                        taxRate: "*"
                    }
                });

                $("#btnSaveTaxRate").click(function() {
                    if ($('#ddlCountry option:selected').val() != 0) {
                        if (tr.form()) {
                            TaxRate.SaveAndUpdateTaxRate();
                            return false;
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        $('#ddlCountry ').attr('class', 'cssClassDropDown error');
                    }
                });

                $("#btnCancel").click(function() {
                    TaxRate.HideAll();
                    $("#divTaxRatesGrid").show();
                });

                $("#txtZipPostCode").DigitOnly('.zipPostcode', '#errmsgZipPostCode');
                $("#txtRangeFrom").DigitOnly('.rangeFrom', '#errmsgRangeFrom');
                $("#txtRangeTo").DigitOnly('.rangeTo', '#errmsgRangeTo');

                $("#ddlTaxRateType").change(function() {
                    if ($.trim($("#ddlTaxRateType").val().toLowerCase()) == "false") {
                    }
                    else {
                        if ($("#txtTaxRateValue").val() < 1000) {
                        } else {
                            $("#txtTaxRateValue").val('');
                        }
                    }
                });
                $("#txtTaxRateValue").keypress(function(e) {
                    if ($.trim($("#ddlTaxRateType").val().toLowerCase()) == "false") {
                        if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                            $("#errmsgTaxRateValue").html("Enter Digits And Decimal Only").css("color", "red").show().fadeOut(1600);
                            return false;
                        }
                    }
                    else {
                        if (e.which == 8 || e.which == 0 || e.which == 46)
                            return true;
                        //if the letter is not digit 
                        if (e.which < 48 || e.which > 57)
                            return false;
                        // check max range 
                        var dest = e.which - 48;
                        var result = this.value + dest.toString();
                        if (result > 999.99) {
                            return false;
                        }
                    }
                });
                $("#btnExportToCSV").click(function() {
                    $('#gdvTaxRateDetails').table2CSV();
                });
                $('#txtRateTitle,#ddlSearchCountry,#txtSearchState,#txtSearchZip').keyup(function(event) {
                    if (event.keyCode == 13) {
                        TaxRate.SearchTaxRate();
                    }
                });
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: TaxRate.config.type,
                    contentType: TaxRate.config.contentType,
                    cache: TaxRate.config.cache,
                    async: TaxRate.config.async,
                    data: TaxRate.config.data,
                    dataType: TaxRate.config.dataType,
                    url: TaxRate.config.url,
                    success: TaxRate.ajaxSuccess,
                    error: TaxRate.ajaxFailure
                });
            },
            LoadTaxRateStaticImage: function() {
                $('#ajaxTaxRateImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            },
            HideAll: function() {
                $("#divTaxRatesGrid").hide();
                $("#divTaxRateInformation").hide();
            },

            ClearForm: function() {
                $("#" + lblTaxRateHeading).html("New Tax Rate Information");
                $("#txtTaxRateTitle").val('');
                $("#ddlCountry").val(0);
                $("#ddlState").val('');
                $("#txtZipPostCode").val('');
                $("#chkIsTaxZipRange").removeAttr('checked');
                $("#txtRangeFrom").val('');
                $("#txtRangeTo").val('');
                $("#txtTaxRateValue").val('');
                $("#txtState").val('');
                $('#txtTaxRateTitle').removeClass('error');
                $('#txtTaxRateTitle').parents('td').find('label').remove();
                $('#txtZipPostCode').removeClass('error');
                $('#txtZipPostCode').parents('td').find('label').remove();
                $('#txtRangeFrom').removeClass('error');
                $('#txtRangeFrom').parents('td').find('label').remove();
                $('#txtRangeTo').removeClass('error');
                $('#txtRangeTo').parents('td').find('label').remove();
                $('#txtState').removeClass('error');
                $('#txtState').parents('td').find('label').remove();
                $('#txtTaxRateValue').removeClass('error');
                $('#txtTaxRateValue').parents('td').find('label').remove();
            },
            GetTaxRateDataForExport: function() {
                this.config.url = this.config.baseURL + "GetTaxRateDetails";
                this.config.data = JSON2.stringify({ offset: 1, limit: null, taxName: null, searchCountry: null, searchState: null, zip: null, storeID: storeId, portalID: portalId });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            },
            ExportTaxRateToCsvData: function() {
                TaxRate.GetTaxRateDataForExport();
            },
            ExportDivDataToExcel: function() {
                TaxRate.GetTaxRateDataForExport();
            },
            BindTaxRateExportData: function(data) {
                var exportData = '<thead><tr><th>Tax Rate Title</th><th>Country</th><th>State/Province</th><th>Zip/Post Code</th><th>Zip/Post Is Range</th><th>Tax Rate Value</th><th>Is Rate Percentage</th></tr></thead><tbody>';
                if (data.d.length > 0) {
                    $.each(data.d, function(index, value) {
                        exportData += '<tr><td>' + value.TaxRateTitle + '</td><td>' + value.Country + '</td>';
                        exportData += '<td>' + value.State + '</td><td>' + value.ZipPostCode + '</td>';
                        exportData += '<td>' + value.IsZipPostRange + '</td><td>' + value.TaxRateValue + '</td>';
                        exportData += '<td>' + value.IsPercent + '</td></tr>';
                    });
                }
                else {
                    exportData += '<tr><td>No Records Found!</td></tr>';
                }
                exportData += '</tbody>';

                $('#TaxRateDetailsExportTbl').html(exportData);
                $("input[id$='HdnValue']").val('<table>' + exportData + '</table>');
                $("input[id$='_csvTaxRateHdnValue']").val($("#TaxRateDetailsExportTbl").table2CSV());
                $("#TaxRateDetailsExportTbl").html('');
            },
            BindTaxRates: function(taxName, country, state, zipPostCode) {
                this.config.method = "GetTaxRateDetails";
                this.config.data = { taxName: taxName, searchCountry: country, searchState: state, zip: zipPostCode, storeID: storeId, portalID: portalId };
                var data = this.config.data;
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvTaxRateDetails_pagesize").length > 0) ? $("#gdvTaxRateDetails_pagesize :selected").text() : 10;

                $("#gdvTaxRateDetails").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                        { display: 'TaxRate_ID', name: 'taxrate_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TaxRateChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                        { display: 'Tax Rate Title', name: 'tax_rate_title', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'Country', name: 'country', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'State/Province', name: 'state_region', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'Zip/Post Code', name: 'tax_zip_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'Zip/Post Is Range', name: 'is_tax_zip_range', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'Tax Rate Value', name: 'tax_rate_value', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'Is Rate Percentage', name: 'tax_rate_value', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
    				    ],

                    buttons: [
                              { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'TaxRate.EditTaxRate', arguments: '1,2,3,4,5,6,7' },
    			              { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'TaxRate.DeleteTaxRate', arguments: '' }
    			        ],
                    rp: perpage,
                    nomsg: "No Records Found!",
                    param: data, // { taxName: taxName, searchCountry: country, searchState: state, zip: zipPostCode, storeID: storeId, portalID: portalId },
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 8: { sorter: false} }
                });
            },
            EditTaxRate: function(tblID, argus) {
                switch (tblID) {
                    case "gdvTaxRateDetails":
                        $("#" + lblTaxRateHeading).html("Edit Tax Rate: '" + argus[3] + "'");
                        $("#hdnTaxRateID").val(argus[0]);
                        $("#txtTaxRateTitle").val(argus[3]);

                        $("#ddlCountry option").each(function() {
                            if ($(this).text() == argus[4]) {
                                $(this).attr("selected", "selected");
                            }
                        });

                        if ($("#ddlCountry").val() == "US") {
                            $("#ddlState").val(argus[5]);
                            $("#txtState").hide();
                            $("#ddlState").show();
                        }
                        else {
                            $("#txtState").val(argus[5]);
                            $("#ddlState").hide();
                            $("#txtState").show();
                        }
                        $("#txtZipPostCode").val(argus[6]);
                        var range = argus[6];
                        var subStr = range.split('-');
                        $("#txtRangeFrom").val(subStr[0]);
                        $("#txtRangeTo").val(subStr[1]);
                        $("#txtTaxRateValue").val(argus[8]);
                        $("#ddlTaxRateType").val(argus[9]);
                        TaxRate.HideAll();
                        $("#divTaxRateInformation").show();
                        $("#chkIsTaxZipRange").attr('checked', $.parseJSON(argus[7].toLowerCase()));
                        if ($("#chkIsTaxZipRange").is(':checked')) {
                            $("#trZipPostCode").hide();
                            $("#trRangeFrom").show();
                            $("#trRangeTo").show();
                            $("#txtZipPostCode").val('');
                        }
                        else {
                            $("#trRangeFrom").hide();
                            $("#trRangeTo").hide();
                            $("#trZipPostCode").show();
                            $("#txtRangeFrom").val('');
                            $("#txtRangeTo").val('');
                        }
                        break;
                    default:
                        break;

                }
            },
            DeleteTaxRate: function(tblID, argus) {
                switch (tblID) {
                    case "gdvTaxRateDetails":
                        var properties = { onComplete: function(e) {
                            TaxRate.DeleteTaxRateByID(argus[0], e);
                        }
                        }
                        csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this tax rate?</p>", properties);
                        break;
                    default:
                        break;
                }
            },

            ConfirmDeleteTaxRates: function(Ids, event) {
                TaxRate.DeleteTaxRateByID(Ids, event);
            },

            DeleteTaxRateByID: function(_taxRate_Ids, event) {
                if (event) {
                    this.config.url = this.config.baseURL + "DeleteTaxRates";
                    this.config.data = JSON2.stringify({ taxRateIDs: _taxRate_Ids, storeID: storeId, portalID: portalId, userName: userName });
                    this.config.ajaxCallMode = 3;
                    this.ajaxCall(this.config);
                }
                return false;
            },
            GetCountryList: function() {
                this.config.url = this.config.baseURL + "BindCountryList";
                this.config.data = '{}';
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            GetStateList: function() {
                this.config.url = this.config.baseURL + "BindStateList";
                this.config.data = '{}';
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            },
            SaveAndUpdateTaxRate: function() {
                var TaxRateId = $("#hdnTaxRateID").val();
                taxRateFlag = TaxRateId;
                var TaxRateTitle = $("#txtTaxRateTitle").val();
                if (TaxRateTitle != "") {
                    var TaxCountryCode = '';
                    if ($("#ddlCountry option:selected").val() != "0") {
                        TaxCountryCode = $("#ddlCountry option:selected").val();
                    }
                    else {
                        csscody.alert("<h2>Information Alert</h2><p>Please select country!</p>");
                        return false;
                    }
                    var TaxStateCode = '';
                    if ($("#ddlCountry").val() == "US") {
                        if ($("#ddlState").val() != 0) {
                            TaxStateCode = $("#ddlState option:selected").val();
                        }
                        else {
                            csscody.alert("<h2>Information Alert</h2><p>Please select state!</p>");
                            return false;
                        }
                    }
                    else {
                        if ($("#txtState").val() != "") {
                            TaxStateCode = $("#txtState").val();
                        }
                        else {
                            csscody.alert("<h2>Information Alert</h2><p>State can not be empty!</p>");
                            return false;
                        }
                    }
                    var IsTaxZipRange = $("#chkIsTaxZipRange").attr('checked');
                    var zipPostRange = '';
                    if ($("#chkIsTaxZipRange").is(':checked')) {
                        zipPostRange = $("#txtRangeFrom").val() + '-' + $("#txtRangeTo").val();
                    }
                    else {
                        zipPostRange = $("#txtZipPostCode").val();
                    }
                    var TaxRateValue = $("#txtTaxRateValue").val();
                    var RateType = $("#ddlTaxRateType").val();
                    this.config.url = this.config.baseURL + "SaveAndUpdateTaxRates";
                    this.config.data = JSON2.stringify({ taxRateID: TaxRateId, taxRateTitle: TaxRateTitle, taxCountryCode: TaxCountryCode, taxStateCode: TaxStateCode, taxZipCode: zipPostRange, isTaxZipRange: IsTaxZipRange, taxRateValue: TaxRateValue, rateType: RateType, storeID: storeId, portalID: portalId, userName: userName });
                    this.config.ajaxCallMode = 4;
                    this.ajaxCall(this.config);
                }
                else {
                    csscody.alert("<h2>Information Alert</h2><p>Tax rate title can not be empty!</p>");
                    return false;
                }
            },
            ajaxSuccess: function(data) {
                switch (TaxRate.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        $.each(data.d, function(index, item) {
                            $("#ddlCountry").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                            $("#ddlSearchCountry").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                        });
                        break;
                    case 2:
                        $.each(data.d, function(index, item) {
                            $("#ddlState").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                            $("#ddlSearchState").append("<option value" + item.Value + ">" + item.Text + "</option>");
                        });
                        break;
                    case 3:
                        csscody.info('<h2>Successful Message</h2><p>Tax rate has been deleted successfully.</p>');
                        TaxRate.BindTaxRates(null, null, null, null);
                        break;
                    case 4:
                        if (taxRateFlag > 0) {
                            csscody.info('<h2>Successful Message</h2><p>Tax rate has been updated successfully.</p>');
                        }
                        else {
                            csscody.info('<h2>Successful Message</h2><p>Tax rate has been saved successfully.</p>');
                        }
                        TaxRate.BindTaxRates(null, null, null, null);
                        TaxRate.HideAll();
                        $("#divTaxRatesGrid").show();
                        break;
                    case 5:
                        TaxRate.BindTaxRateExportData(data);
                        break;
                }
            },
            ajaxFailure: function() {
                switch (TaxRate.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        csscody.error('<h2>Error Message</h2><p>Failed to delete tax rate!</p>');
                        break;
                    case 4:
                        csscody.error('<h2>Error Message</h2><p>Failed to save tax rate!</p>');
                        break;
                }
            },
            SearchTaxRate: function() {
                var taxName = $.trim($("#txtRateTitle").val());
                var country = '';
                var state = $.trim($("#txtSearchState").val());
                var zipPostCode = $.trim($("#txtSearchZip").val());
                if (taxName.length < 1) {
                    taxName = null;
                }
                if ($("#ddlSearchCountry").val() != "0") {
                    country = $.trim($("#ddlSearchCountry option:selected").val());
                }
                else {
                    country = null;
                }
                if (state.length < 1) {
                    state = null;
                }
                if (zipPostCode.length < 1) {
                    zipPostCode = null;
                }
                TaxRate.BindTaxRates(taxName, country, state, zipPostCode);
            }
        }
        TaxRate.init();
    });