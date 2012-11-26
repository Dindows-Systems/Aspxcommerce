    var CustomerTaxClass="";
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
        var editFlag = 0;
        CustomerTaxClass = {
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
                CustomerTaxClass.HideAll();
                $("#divTaxCustomerClassGrid").show();
                CustomerTaxClass.LoadCustomerTaxStaticImage();
                CustomerTaxClass.BindCustomerTaxClasses(null);
                $("#btnAddNewTaxCustomerClass").click(function() {
                    CustomerTaxClass.HideAll();
                    $("#" + lblCustomerTaxClassHeading).html("New Customer Tax Class");
                    $("#divCustomerTaxClass").show();
                    $("#txtTaxCustomerClassName").val('');
                    $("#hdnTaxCustomerClass").val(0);
                });
                $("#btnCancel").click(function() {
                    CustomerTaxClass.HideAll();
                    $("#divTaxCustomerClassGrid").show();
                });
                $("#btnSaveTaxCustomerClass").bind("click", function() {
                    CustomerTaxClass.SaveAndUpdateTaxCustmerClass();
                });
                $('#txtCustomerClassName').keyup(function(event) {
                    if (event.keyCode == 13) {
                        CustomerTaxClass.SearchCustomerClassName();
                    }
                });
                $("#btnDeleteSelected").click(function() {
                    var taxCustomerClass_Ids = '';
                    $('.TaxCustomerClassChkbox').each(function() {
                        if ($(this).attr('checked')) {
                            taxCustomerClass_Ids += $(this).val() + ',';
                        }
                    });
                    if (taxCustomerClass_Ids != "") {
                        var properties = {
                            onComplete: function(e) {
                                CustomerTaxClass.ConfirmDeleteTaxCustomerClass(taxCustomerClass_Ids, e);
                            }
                        }
                        csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delect the selected customer tax class?</p>", properties);
                    } else {
                        csscody.alert('<h2>Information Alert</h2><p>Please select at least one customer tax class before delete.</p>');
                    }
                });
            },
            HideAll: function() {
                $("#divTaxCustomerClassGrid").hide();
                $("#divCustomerTaxClass").hide();
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: CustomerTaxClass.config.type,
                    contentType: CustomerTaxClass.config.contentType,
                    cache: CustomerTaxClass.config.cache,
                    async: CustomerTaxClass.config.async,
                    data: CustomerTaxClass.config.data,
                    dataType: CustomerTaxClass.config.dataType,
                    url: CustomerTaxClass.config.url,
                    success: CustomerTaxClass.ajaxSuccess,
                    error: CustomerTaxClass.ajaxFailure
                });
            },

            LoadCustomerTaxStaticImage: function() {
                $('#ajaxCustomerTaxClassImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            },

            BindCustomerTaxClasses: function(classNm) {
                this.config.method = "GetTaxCustomerClassDetails";
                this.config.data = { className: classNm, storeID: storeId, portalID: portalId, cultureName: cultureName };
                var data = this.config.data;
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvTaxCustomerClassDetails_pagesize").length > 0) ? $("#gdvTaxCustomerClassDetails_pagesize :selected").text() : 10;

                $("#gdvTaxCustomerClassDetails").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                        { display: 'TaxCostomerClass_ID', name: 'tax_customer_class_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TaxCustomerClassChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                        { display: 'Customer Tax Class Name', name: 'tax_customer_class_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'Actions', name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                    ],

                    buttons: [
                        { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'CustomerTaxClass.EditTaxCustomerClass', arguments: '1,2,3' },
                        { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'CustomerTaxClass.DeleteTaxCustomerClass', arguments: '' }
                    ],
                    txtClass: 'cssClassNormalTextBox',
                    rp: perpage,
                    nomsg: "No Records Found!",
                    param: data,
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 2: { sorter: false } }
                });
            },
            EditTaxCustomerClass: function(tblID, argus) {
                switch (tblID) {
                case "gdvTaxCustomerClassDetails":
                    $("#hdnTaxCustomerClass").val(argus[0]);
                    $("#txtTaxCustomerClassName").val(argus[3]);
                    $("#" + lblCustomerTaxClassHeading).html("Edit Customer Tax Class: '" + argus[3] + "'");
                    CustomerTaxClass.HideAll();
                    $("#divCustomerTaxClass").show();
                    break;
                default:
                    break;
                }
            },
            DeleteTaxCustomerClass: function(tblID, argus) {
                switch (tblID) {
                case "gdvTaxCustomerClassDetails":
                    var properties = {
                        onComplete: function(e) {
                            CustomerTaxClass.DeleteTaxCustomerClassByID(argus[0], e);
                        }
                    };
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this customer tax class?</p>", properties);
                    break;
                default:
                    break;
                }
            },
            SaveAndUpdateTaxCustmerClass: function() {
                var taxCustomerClassId = $("#hdnTaxCustomerClass").val();
                editFlag = taxCustomerClassId;
                var taxCustomerClassName = $("#txtTaxCustomerClassName").val();
                if (taxCustomerClassName != "") {
                    this.config.url = this.config.baseURL + "SaveAndUpdateTaxCustmerClass";
                    this.config.data = JSON2.stringify({ taxCustomerClassID: taxCustomerClassId, taxCustomerClassName: taxCustomerClassName, cultureName: cultureName, storeID: storeId, portalID: portalId, userName: userName });
                    this.config.ajaxCallMode = 1;
                    this.ajaxCall(this.config);
                    CustomerTaxClass.BindCustomerTaxClasses(null);
                    CustomerTaxClass.HideAll();
                    $("#divTaxCustomerClassGrid").show();
                } else {
                    csscody.alert("<h2>Information Alert</h2><p>Customer tax Class can not be empty.</p>");
                    return false;
                }

            },
            ConfirmDeleteTaxCustomerClass: function(Ids, event) {
                CustomerTaxClass.DeleteTaxCustomerClassByID(Ids, event);
            },

            DeleteTaxCustomerClassByID: function(_taxCustomerClass_Ids, event) {
                if (event) {
                    this.config.url = this.config.baseURL + "DeleteTaxCustomerClass";
                    this.config.data = JSON2.stringify({ taxCustomerClassIDs: _taxCustomerClass_Ids, storeID: storeId, portalID: portalId, cultureName: cultureName, userName: userName });
                    this.config.ajaxCallMode = 2;
                    this.ajaxCall(this.config);
                }
                return false;
            },
            ajaxSuccess: function() {
                switch (CustomerTaxClass.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    if (editFlag > 0) {
                        csscody.info('<h2>Successful Message</h2><p>Customer tax class has been updated successfully.</p>');
                    } else {
                        csscody.info('<h2>Successful Message</h2><p>Customer tax class has been saved successfully.</p>');
                    }
                    CustomerTaxClass.BindCustomerTaxClasses(null);
                    CustomerTaxClass.HideAll();
                    $("#divTaxCustomerClassGrid").show();
                    break;
                case 2:
                    csscody.info('<h2>Successful Message</h2><p>Customer tax class has been deleted successfully.</p>');
                    CustomerTaxClass.BindCustomerTaxClasses(null);
                    break;
                }
            },
            ajaxFailure: function() {
                switch (CustomerTaxClass.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>Error Message</h2><p>Failed to save customer tax class!</p>');
                    break;
                case 2:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete customer tax class!</p>');
                    break;
                }
            },
            SearchCustomerClassName: function() {
                var classNm = $.trim($("#txtCustomerClassName").val());
                if (classNm.length < 1) {
                    classNm = null;
                }
                CustomerTaxClass.BindCustomerTaxClasses(classNm);
            }
        };
        CustomerTaxClass.init();
    });