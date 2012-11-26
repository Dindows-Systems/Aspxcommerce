var TaxRules="";
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var taxRuleFlag = 0;
    var TaxRuleID = 0;
    TaxRules = {
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
            TaxRules.HideAll();
            $("#divTaxManageRulesGrid").show();
            TaxRules.LoadTaxRuleMgmtStaticImage();
            TaxRules.BindTaxManageRules(null, null, null, null, null, null);
            TaxRules.BindCustomerTaxClass();
            TaxRules.BindItemTaxClass();
            TaxRules.BindTaxRates();

            $("#btnAddNewTaxRule").click(function() {
                TaxRules.ClearForm();
                TaxRules.HideAll();
                $("#divTaxRuleInformation").show();
                $("#hdnTaxManageRuleID").val(0);
                TaxRuleID = 0;
                $('#errDisplayOrder').hide();
            });

            $("#btnDeleteSelected").click(function() {
                var taxManageRule_Id = '';
                $('.TaxRuleChkbox').each(function() {
                    if ($(this).attr('checked')) {
                        taxManageRule_Id += $(this).val() + ',';
                    }
                });
                if (taxManageRule_Id != "") {
                    var properties = {
                        onComplete: function(e) {
                            TaxRules.ConfirmDeleteTaxRules(taxManageRule_Id, e);
                        }
                    };
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete the selected tax rule(s)?</p>", properties);
                } else {
                    csscody.alert('<h2>Information Alert</h2><p>Please select at least one tax rule before delete.</p>');
                }
            });

            $("#txtPriority").DigitOnly('.priority', '#errmsgPriority');
            $("#txtDisplayOrder").DigitOnly('.displayOrder', '#errmsgDisplayOrder');

            $("#txtDisplayOrder").blur(function() {
                TaxRules.CheckUniqueness($("#txtDisplayOrder").val(), TaxRuleID);
                $("#errDisplayOrder").show();
            });

            var trm = $("#form1").validate({
                ignore: ':hidden',
                rules: {
                    ruleName: "required",
                    priority: "required",
                    displayOrder: "required"
                },
                messages: {
                    ruleName: "* (at least 2 chars)",
                    priority: "*",
                    displayOrder: "*"
                }
            });

            $("#btnSaveTaxRule").click(function() {
                if ($('#ddlCustomerTaxClass option:selected').val() == 0) {
                    $('#ddlCustomerTaxClass').attr('class', 'cssClassDropDown error');
                    return false;
                } else if ($('#ddlItemTaxClass option:selected').val() == 0) {
                    $('#ddlItemTaxClass').attr('class', 'cssClassDropDown error');
                    return false;
                } else if ($('#ddlTaxRate option:selected').val() == 0) {
                    $('#ddlTaxRate').attr('class', 'cssClassDropDown error');
                    return false;
                } else if (trm.form()) {
                    TaxRules.SaveAndUpdateTaxRules();
                    return false;
                } else {
                    return false;
                }
            });

            $("#btnCancel").click(function() {
                TaxRules.HideAll();
                $("#divTaxManageRulesGrid").show();
            });
            $("#btnExportToCSV").click(function() {
                $('#gdvTaxRulesDetails').table2CSV();
            });
            $('#txtRuleName,#ddlCustomerClassName,#ddlItemClassName,#ddlTaxRateTitle,#txtSearchPriority,#txtSearchDisplayOrder').keyup(function(event) {
                if (event.keyCode == 13) {
                    TaxRules.SearchTaxManageRules();
                }
            });
        },
        LoadTaxRuleMgmtStaticImage: function() {
            $('#ajaxTaxRuleMgmtImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        ajaxCall: function(config) {
            $.ajax({
                type: TaxRules.config.type,
                contentType: TaxRules.config.contentType,
                cache: TaxRules.config.cache,
                async: TaxRules.config.async,
                data: TaxRules.config.data,
                dataType: TaxRules.config.dataType,
                url: TaxRules.config.url,
                success: TaxRules.ajaxSuccess,
                error: TaxRules.ajaxFailure
            });
        },
        HideAll: function() {
            $("#divTaxManageRulesGrid").hide();
            $("#divTaxRuleInformation").hide();
        },

        ClearForm: function() {
            $("#" + lblTaxRuleHeading).html("New Tax Rule Information");
            $("#txtTaxManageRuleName").val('');
            $("#ddlCustomerTaxClass").val(0);
            $("#ddlItemTaxClass").val(0);
            $("#ddlTaxRate").val(0);
            $("#txtPriority").val('');
            $("#txtDisplayOrder").val('');
            TaxRules.ClearErrorLabel();
        },

        CheckUniqueness: function(value, id) {
            this.config.url = this.config.baseURL + "CheckTaxUniqueness";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, value: value, taxRuleID: id });
            this.config.ajaxCallMode = 6;
            this.ajaxCall(this.config);
        },

        ClearErrorLabel: function() {
            $('#txtTaxManageRuleName').removeClass('error');
            $('#txtTaxManageRuleName').parents('td').find('label').remove();
            $('#txtPriority').removeClass('error');
            $('#txtPriority').parents('td').find('label').remove();
            $('#txtDisplayOrder').removeClass('error');
            $('#txtDisplayOrder').parents('td').find('label').remove();

            $('#ddlCustomerTaxClass').removeClass('error');
            $('#ddlItemTaxClass').removeClass('error');
            $('#ddlTaxRate').removeClass('error');
        },
        BindTaxManageRules: function(ruleNm, customerClassNm, itemClassNm, taxRateTitle, searchPriority, searchDisplayOrder) {
            this.config.method = "GetTaxRules";
            this.config.data = { ruleName: ruleNm, customerClassName: customerClassNm, itemClassName: itemClassNm, rateTitle: taxRateTitle, priority: searchPriority, displayOrder: searchDisplayOrder, storeID: storeId, portalID: portalId, cultureName: cultureName };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvTaxRulesDetails_pagesize").length > 0) ? $("#gdvTaxRulesDetails_pagesize :selected").text() : 10;

            $("#gdvTaxRulesDetails").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                { display: 'TaxManageRule_ID', name: 'taxManageRule_ID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TaxRuleChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                { display: 'Tax Rule Name', name: 'taxManageRuleName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'Customer Tax Class Name', name: 'taxCustomerClassName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'Item Tax Class Name', name: 'taxItemClassName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'Tax Rate Name', name: 'taxRateTitle', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'Priority', name: 'priority', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'Display Order', name: 'displayOrder', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'TaxRules.EditTaxRule', arguments: '1,2,3,4,5,6,7' },
                { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'TaxRules.DeleteTaxRule', arguments: '' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: data, // { ruleName: ruleNm, customerClassName: customerClassNm, itemClassName: itemClassNm, rateTitle: taxRateTitle, priority: searchPriority, displayOrder: searchDisplayOrder, storeID: storeId, portalID: portalId, cultureName: cultureName },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 7: { sorter: false} }
            });
        },

        EditTaxRule: function(tblID, argus) {
            switch (tblID) {
                case "gdvTaxRulesDetails":
                    TaxRules.ClearErrorLabel();
                    TaxRuleID = argus[0];
                    $("#hdnTaxManageRuleID").val(argus[0]);
                    $("#txtTaxManageRuleName").val(argus[3]);
                    $("#" + lblTaxRuleHeading).html("Edit Tax Rule: '" + argus[3] + "'");
                    $("#ddlCustomerTaxClass option").each(function() {
                        if ($(this).text() == argus[4]) {
                            $(this).attr("selected", "selected");
                        }
                    });
                    $("#ddlItemTaxClass option").each(function() {
                        if ($(this).text() == argus[5]) {
                            $(this).attr("selected", "selected");
                        }
                    });
                    $("#ddlTaxRate option").each(function() {
                        if ($(this).text() == argus[6]) {
                            $(this).attr("selected", "selected");
                        }
                    });
                    $("#txtPriority").val(argus[7]);
                    $("#txtDisplayOrder").val(argus[8]);
                    TaxRules.HideAll();
                    $("#divTaxRuleInformation").show();
                    break;
                default:
                    break;
            }
        },

        DeleteTaxRule: function(tblID, argus) {
            switch (tblID) {
                case "gdvTaxRulesDetails":
                    var properties = {
                        onComplete: function(e) {
                            TaxRules.DeleteTaxRuleByID(argus[0], e);
                        }
                    };
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this tax rule?</p>", properties);
                    break;
                default:
                    break;
            }
        },

        ConfirmDeleteTaxRules: function(Ids, event) {
            TaxRules.DeleteTaxRuleByID(Ids, event);
        },

        DeleteTaxRuleByID: function(_taxRule_Ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteTaxManageRules";
                this.config.data = JSON2.stringify({ taxManageRuleIDs: _taxRule_Ids, storeID: storeId, portalID: portalId, cultureName: cultureName, userName: userName });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            }
            return false;
        },
        BindCustomerTaxClass: function() {
            this.config.url = this.config.baseURL + "GetCustomerTaxClass";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },

        BindItemTaxClass: function() {
            this.config.url = this.config.baseURL + "GetItemTaxClass";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },

        BindTaxRates: function() {
            this.config.url = this.config.baseURL + "GetTaxRate";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
        },
        SaveAndUpdateTaxRules: function() {
            var taxManageRuleId = $("#hdnTaxManageRuleID").val();
            taxRuleFlag = taxManageRuleId;
            var taxManageRuleName = $("#txtTaxManageRuleName").val();
            var TaxCustomerClassId = $("#ddlCustomerTaxClass").val();
            var TaxItemClassId = $("#ddlItemTaxClass").val();
            var TaxRateId = $("#ddlTaxRate").val();
            var Priority = $("#txtPriority").val();
            var DispalyOrder = $("#txtDisplayOrder").val();
            this.config.url = this.config.baseURL + "SaveAndUpdateTaxRule";
            this.config.data = JSON2.stringify({ taxManageRuleID: taxManageRuleId, taxManageRuleName: taxManageRuleName, taxCustomerClassID: TaxCustomerClassId, taxItemClassID: TaxItemClassId, taxRateID: TaxRateId, priority: Priority, displayOrder: DispalyOrder, cultureName: cultureName, storeID: storeId, portalID: portalId, userName: userName });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(data) {
            switch (TaxRules.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.info('<h2>Successful Message</h2><p>Tax rule has been deleted successfully.</p>');
                    TaxRules.BindTaxManageRules(null, null, null, null, null, null);
                    break;
                case 2:
                    if (taxRuleFlag > 0) {
                        csscody.info('<h2>Successful Message</h2><p>Tax rule has been updated successfully.</p>');
                    } else {
                        csscody.info('<h2>Successful Message</h2><p>Tax rule has been saved successfully.</p>');
                    }
                    TaxRules.BindTaxManageRules(null, null, null, null, null, null);
                    TaxRules.HideAll();
                    $("#divTaxManageRulesGrid").show();
                    break;
                case 3:
                    $.each(data.d, function(index, item) {
                        $("#ddlCustomerTaxClass").append("<option value=" + item.TaxCustomerClassID + ">" + item.TaxCustomerClassName + "</option>");
                        $("#ddlCustomerClassName").append("<option value=" + item.TaxCustomerClassID + ">" + item.TaxCustomerClassName + "</option>");
                    });
                    break;
                case 4:
                    $.each(data.d, function(index, item) {
                        $("#ddlItemTaxClass").append("<option value=" + item.TaxItemClassID + ">" + item.TaxItemClassName + "</option>");
                        $("#ddlItemClassName").append("<option value=" + item.TaxItemClassID + ">" + item.TaxItemClassName + "</option>");
                    });
                    break;
                case 5:
                    $.each(data.d, function(index, item) {
                        $("#ddlTaxRate").append("<option value=" + item.TaxRateID + ">" + item.TaxRateTitle + "</option>");
                        $("#ddlTaxRateTitle").append("<option value=" + item.TaxRateID + ">" + item.TaxRateTitle + "</option>");
                    });
                case 6:
                    if (data.d == 0) {
                        $("#errDisplayOrder").html("Already Exist").css("color", "red");
                        $("#txtDisplayOrder").val('');
                    } else {
                        $("#errDisplayOrder").html('');
                    }
                    break;
            }
        },
        ajaxFailure: function(data) {
            switch (TaxRules.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete tax rule!</p>');
                    break;
                case 2:
                    csscody.error('<h2>Error Message</h2><p>Failed to save tax rate!</p>');
                    break;
            }
        },
        SearchTaxManageRules: function() {
            var ruleNm = $.trim($("#txtRuleName").val());
            var customerClassNm = '';
            var itemClassNm = '';
            var taxRateTitle = '';
            var searchPriority = $.trim($("#txtSearchPriority").val());
            var searchDisplayOrder = $.trim($("#txtSearchDisplayOrder").val());
            if (ruleNm.length < 1) {
                ruleNm = null;
            }
            if (searchPriority.length < 1) {
                searchPriority = null;
            }

            if (searchDisplayOrder.length < 1) {
                searchDisplayOrder = null;
            }

            if ($("#ddlCustomerClassName").val() != 0) {
                customerClassNm = $.trim($("#ddlCustomerClassName").val());
            } else {
                customerClassNm = null;
            }
            if ($("#ddlItemClassName").val() != 0) {
                itemClassNm = $.trim($("#ddlItemClassName").val());
            } else {
                itemClassNm = null;
            }
            if ($("#ddlTaxRateTitle").val() != 0) {
                taxRateTitle = $.trim($("#ddlTaxRateTitle").val());
            } else {
                taxRateTitle = null;
            }
            TaxRules.BindTaxManageRules(ruleNm, customerClassNm, itemClassNm, taxRateTitle, searchPriority, searchDisplayOrder);
        }
    };
    TaxRules.init();
});