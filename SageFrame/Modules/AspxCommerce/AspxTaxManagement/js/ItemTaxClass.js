    var ItemTaxClass="";
    $(function() {
        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var itemTaxClassFlag = 0;
        ItemTaxClass = {
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
                ItemTaxClass.HideAll();
                $("#divTaxItemClassGrid").show();
                ItemTaxClass.LoadItemTaxClassStaticImage();
                ItemTaxClass.BindTaxItemClasses(null);
                $("#btnAddNewTaxItemClass").click(function() {
                    ItemTaxClass.HideAll();
                    $("#" + lblItemTaxClassHeading).html("New Item Tax Class");
                    $("#divProductTaxClass").show();
                    $("#txtTaxItemClassName").val('');
                    $("#hdnTaxItemClassID").val(0);
                });

                $("#btnSaveTaxItemClass").click(function() {
                    ItemTaxClass.SaveAndUpdateTaxItemClass();
                });

                $("#btnCancel").click(function() {
                    ItemTaxClass.HideAll();
                    $("#divTaxItemClassGrid").show();
                });
                $('#txtItemClassName').keyup(function(event) {
                    if (event.keyCode == 13) {
                        ItemTaxClass.SearchItemClassName();
                    }
                });

                $('#btnDeleteSelected').click(function() {
                    var TaxItemClass_Ids = '';
                    $(".TaxItemClassChkbox").each(function(i) {
                        if ($(this).attr("checked")) {
                            TaxItemClass_Ids += $(this).val() + ',';
                        }
                    });
                    if (TaxItemClass_Ids != "") {
                        var properties = {
                            onComplete: function(e) {
                                ItemTaxClass.ConfirmDeleteTaxItemClass(TaxItemClass_Ids, e);
                            }
                        };
                        csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete the selected item tax class?</p>", properties);
                    }
                    else {
                        csscody.alert('<h2>Information Alert</h2><p>Please select at least one item tax class.</p>');
                    }
                });
            },
            HideAll: function() {
                $("#divTaxItemClassGrid").hide();
                $("#divProductTaxClass").hide();
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: ItemTaxClass.config.type,
                    contentType: ItemTaxClass.config.contentType,
                    cache: ItemTaxClass.config.cache,
                    async: ItemTaxClass.config.async,
                    data: ItemTaxClass.config.data,
                    dataType: ItemTaxClass.config.dataType,
                    url: ItemTaxClass.config.url,
                    success: ItemTaxClass.ajaxSuccess,
                    error: ItemTaxClass.ajaxFailure
                });
            },
            LoadItemTaxClassStaticImage: function() {
                $('#ajaxItemTaxClassImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            },
            BindTaxItemClasses: function(itemClassNm) {
                this.config.method = "GetTaxItemClassDetails";
                this.config.data = { itemClassName: itemClassNm, storeID: storeId, portalID: portalId, cultureName: cultureName };
                var data = this.config.data;
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvTaxItemClassDetails_pagesize").length > 0) ? $("#gdvTaxItemClassDetails_pagesize :selected").text() : 10;

                $("#gdvTaxItemClassDetails").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                    { display: 'TaxItemClass_ID', name: 'tax_item_class_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TaxItemClassChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: 'Item Tax Class Name', name: 'tax_item_class_name', cssclass: '', coltype: 'label', align: 'left' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
    				],

                    buttons: [
                      { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'ItemTaxClass.EditTaxItemClass', arguments: '1,2,3' },
                      { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'ItemTaxClass.DeleteTaxItemClass', arguments: '' }
    			    ],
                    rp: perpage,
                    nomsg: "No Records Found!",
                    param: data,
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 2: { sorter: false} }
                });
            },
            EditTaxItemClass: function(tblID, argus) {
                switch (tblID) {
                    case "gdvTaxItemClassDetails":
                        $("#" + lblItemTaxClassHeading).html("Edit Item Tax Class: '" + argus[3] + "'");
                        $("#hdnTaxItemClassID").val(argus[0]);
                        $("#txtTaxItemClassName").val(argus[3]);
                        ItemTaxClass.HideAll();
                        $("#divProductTaxClass").show();
                        break;
                    default:
                        break;
                }
            },
            DeleteTaxItemClass: function(tblID, argus) {
                switch (tblID) {
                    case "gdvTaxItemClassDetails":
                        var properties = { onComplete: function(e) {
                            ItemTaxClass.DeleteTaxItemClassByID(argus[0], e);
                        }
                        }
                        csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this item tax class?</p>", properties);
                        break;
                    default:
                        break;
                }
            },

            ConfirmDeleteTaxItemClass: function(Ids, event) {
                ItemTaxClass.DeleteTaxItemClassByID(Ids, event);
            },

            DeleteTaxItemClassByID: function(taxItemClass_Ids, event) {
                if (event) {
                    this.config.url = this.config.baseURL + "DeleteTaxItemClass";
                    this.config.data = JSON2.stringify({ taxItemClassIDs: taxItemClass_Ids, storeID: storeId, portalID: portalId, cultureName: cultureName, userName: userName });
                    this.config.ajaxCallMode = 2;
                    this.ajaxCall(this.config);
                }
                return false;
            },
            SaveAndUpdateTaxItemClass: function() {
                var taxItemClassId = $("#hdnTaxItemClassID").val();
                itemTaxClassFlag = taxItemClassId;
                var taxItemClassName = $("#txtTaxItemClassName").val();
                if (taxItemClassName != "") {
                    this.config.url = this.config.baseURL + "SaveAndUpdateTaxItemClass";
                    this.config.data = JSON2.stringify({ taxItemClassID: taxItemClassId, taxItemClassName: taxItemClassName, cultureName: cultureName, storeID: storeId, portalID: portalId, userName: userName });
                    this.config.ajaxCallMode = 1;
                    this.ajaxCall(this.config);
                }
                else {
                    csscody.alert("Item tax class can't be empty!");
                    return false;
                }
            },
            ajaxSuccess: function(data) {
                switch (ItemTaxClass.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        if (itemTaxClassFlag > 0) {
                            csscody.info('<h2>Information Message</h2><p>Item tax class has been updated successfully.</p>');
                        }
                        else {
                            csscody.info('<h2>Information Message</h2><p>Item tax class has been saved successfully.</p>');
                        }
                        ItemTaxClass.BindTaxItemClasses(null);
                        ItemTaxClass.HideAll();
                        $("#divTaxItemClassGrid").show();
                        break;
                    case 2:
                        csscody.info('<h2>Information Message</h2><p>Item tax class has been deleted successfully.</p>');
                        ItemTaxClass.BindTaxItemClasses(null);
                        break;
                }
            },
            ajaxFailure: function(data) {
                switch (ItemTaxClass.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        csscody.error('<h2>Information Message</h2><p>Failed to save item tax class!</p>');
                        break;
                    case 2:
                        csscody.error('<h2>Information Message</h2><p>Failed to delete item tax class!</p>');
                        break;
                }
            },
            SearchItemClassName: function() {
                var itemClassNm = $.trim($("#txtItemClassName").val());
                if (itemClassNm.length < 1) {
                    itemClassNm = null;
                }
                ItemTaxClass.BindTaxItemClasses(itemClassNm);
            }
        }
        ItemTaxClass.init();
    });