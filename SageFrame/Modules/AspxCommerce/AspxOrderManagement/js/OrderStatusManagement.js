var OrderStatusMgmt = "";

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
    var isUnique = false;
    OrderStatusMgmt = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxservicePath,
            method: "",
            url: "",
            ajaxCallMode: 0 ///0 for get categories and bind, 1 for notification,2 for versions bind   
        },

        ajaxCall: function(config) {
            $.ajax({
                type: OrderStatusMgmt.config.type,
                contentType: OrderStatusMgmt.config.contentType,
                cache: OrderStatusMgmt.config.cache,
                async: OrderStatusMgmt.config.async,
                url: OrderStatusMgmt.config.url,
                data: OrderStatusMgmt.config.data,
                dataType: OrderStatusMgmt.config.dataType,
                success: OrderStatusMgmt.ajaxSuccess,
                error: OrderStatusMgmt.ajaxFailure
            });
        },

        HideAlldiv: function() {
            $('#divOrderStatusDetail').hide();
            $('#divEditOrderStatus').hide();
        },

        Reset: function() {
            $('#txtOrderStatusAliasName').val('');
            $('#txtAliasToolTip').val('');
            $('#txtAliasHelp').val('');
            $("#chkIsActiveOrder").removeAttr('checked');
        },

        ClearForm: function() {
            $("#btnSaveOrderStatus").removeAttr("name");
            $('#' + lblHeading).html("Add New Order Status");

            $('#txtOrderStatusAliasName').val('');
            $('#txtAliasToolTip').val('');
            $('#txtAliasHelp').val('');
            $("#chkIsActiveOrder").removeAttr('checked');
            $("#isActiveTR").show();
            $("#hdnIsSystem").val(false);

            $('#txtOrderStatusAliasName').removeClass('error');
            $('#txtOrderStatusAliasName').parents('td').find('label').remove();
            $('#txtAliasToolTip').removeClass('error');
            $('#txtAliasToolTip').parents('td').find('label').remove();
            $('#osErrorLabel').html('');
        },
        LoadOrderStatusStaticImage: function() {
            //$(".loading").html('<img src="' + aspxTemplateFolderPath + '/images/ajax-loader.gif" alt="loading..." title="loading..."/>');
            $('#ajaxOrderStatusMgmtImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        BindOrdersStatusInGrid: function(OrderSatatusName, isAct) {
            this.config.method = "GetAllStatusList";
            this.config.url = this.config.baseURL;
            this.config.data = { storeID: storeId, portalID: portalId, cultureName: cultureName, userName: userName, orderStatusName: OrderSatatusName, isActive: isAct };
            var data = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#tblOrderStatusDetails_pagesize").length > 0) ? $("#tblOrderStatusDetails_pagesize :selected").text() : 10;

            $("#tblOrderStatusDetails").sagegrid({
                url: this.config.baseURL + "AspxCommerceWebService.asmx/",
                functionMethod: this.config.method,
                colModel: [
                    { display: 'Order Status ID', name: 'OrderStatusID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', checkFor: '5', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
                    { display: 'Order Status Name', name: 'OrderStatusAliasName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Alias Tool Tip', name: 'AliasToolTip', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Alias Help', name: 'AliasHelp', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'AddedOn', name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd', hide: true },
                    { display: 'System', name: 'IsSystemUsed', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: 'IsActive', name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: 'IsReduce Item Quantity', name: 'ReduceQuantity', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [{ display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'OrderStatusMgmt.EditOrderStatus', arguments: '1,2,3,4,5,6,7' },
                    { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'OrderStatusMgmt.DeleteOrderStatus', arguments: '1,5' }
                // { display: 'Active', name: 'active', enable: true, _event: 'click', trigger: '3', callMethod: 'ActiveOrderStatus', arguments: '1,6' },
                // { display: 'Deactive', name: 'deactive', enable: true, _event: 'click', trigger: '4', callMethod: 'DeactiveOrderStatus', arguments: '' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 7: { sorter: false} }
            });
        },

        Boolean: function(str) {
            switch (str.toLowerCase()) {
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return false;
            }
        },

        EditOrderStatus: function(tblID, argus) {
            switch (tblID) {
                case "tblOrderStatusDetails":
                    editFlag = argus[0];
                    OrderStatusMgmt.ClearForm();
                    $("#btnReset").hide();
                    $('#divOrderStatusDetail').hide();
                    $('#divEditOrderStatus').show();

                    $('#' + lblHeading).html("Edit Order Status: '" + argus[3] + "'");
                    $('#txtOrderStatusAliasName').val(argus[3]);
                    $('#txtAliasToolTip').val(argus[4]);
                    $('#txtAliasHelp').val(argus[5]);
                    $("#chkIsActiveOrder").attr('checked', OrderStatusMgmt.Boolean(argus[8]));
                    $("#btnSaveOrderStatus").attr("name", argus[0]);
                    $("#chkIsReduceQuantity").attr("checked", OrderStatusMgmt.Boolean(argus[9]));
                    if (argus[7].toLowerCase() != "yes") {
                        $("#isActiveTR").show();
                        $("#hdnIsSystem").val(false);
                    } else {
                        $("#isActiveTR").hide();
                        $("#hdnIsSystem").val(true);
                        $('#divOrderStatusDetail').show();
                        $('#divEditOrderStatus').hide();
                        csscody.alert('<h2>Information Alert</h2><p>Sorry! System status can not be edited.</p>');
                    }
                    break;
                default:
                    break;
            }
        },
        DeleteOrderStatus: function(tblID, argus) {
            switch (tblID) {
                case "tblOrderStatusDetails":
                    if (argus[4].toLowerCase() != "yes") {
                        OrderStatusMgmt.DeleteAttribute(argus[0], storeId, portalId, cultureName, userName);
                    } else {
                        csscody.alert('<h2>Information Alert</h2><p>Sorry! System status can not be deleted.</p>');
                    }
                    break;
                default:
                    break;
            }
        },

        DeleteAttribute: function(_orderStatusId, _storeId, _portalId, _cultureName, _userName) {
            var properties = {
                onComplete: function(e) {
                    OrderStatusMgmt.ConfirmSingleDelete(_orderStatusId, _storeId, _portalId, _cultureName, _userName, e);

                }
            };
            // Ask user's confirmation before delete records        
            csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this order status?</p>", properties);
        },

        ConfirmSingleDelete: function(_orderStatusId, _storeId, _portalId, _cultureName, _userName, event) {
            if (event) {
                OrderStatusMgmt.DeleteSingleAttribute(_orderStatusId, _storeId, _portalId, _cultureName, _userName);
            }
            return false;
        },

        ConfirmDeleteMultiple: function(orderStatus_ids, event) {
            if (event) {
                OrderStatusMgmt.DeleteMultipleAttribute(orderStatus_ids, storeId, portalId, userName, cultureName);
            }
        },

        DeleteSingleAttribute: function(_orderStatusId, _storeId, _portalId, _cultureName, _userName) {
            this.config.method = "AspxCommerceWebService.asmx/DeleteOrderStatusByID";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ orderStatusID: parseInt(_orderStatusId), storeID: _storeId, portalID: _portalId, userName: _userName, cultureName: _cultureName });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        DeleteMultipleAttribute: function(orderStatus_ids, storeId, portalId, userName, cultureName) {
            this.config.method = "AspxCommerceWebService.asmx/DeleteOrderStatusMultipleSelected";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ orderStatusIDs: orderStatus_ids, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },

        SaveOrderStatus: function(OrderStatusID, storeId, portalId, userName, cultureName) {
            editFlag = OrderStatusID;
            var OrderStatusAliasName = $('#txtOrderStatusAliasName').val();
            var AliasToolTip = $('#txtAliasToolTip').val();
            var AliasHelp = $('#txtAliasHelp').val();
            var IsActive = $("#chkIsActiveOrder").attr('checked');
            var IsReduceQuantity = $("#chkIsReduceQuantity").attr('checked');
            var IsDeleted = $("#chkIsDeleted").attr('checked');
            var IsSystemUsed = $("#hdnIsSystem").val();

            this.config.method = "AspxCommerceWebService.asmx/AddUpdateOrderStatus";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({
                storeID: storeId,
                portalID: portalId,
                cultureName: cultureName,
                userName: userName,
                orderStatusID: OrderStatusID,
                orderStatusAliasName: OrderStatusAliasName,
                aliasToolTip: AliasToolTip,
                aliasHelp: AliasHelp,
                isSystem: IsSystemUsed,
                isActive: IsActive,
                isReduceQuantity: IsReduceQuantity
            });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        SearchOrderStatus: function() {

            var OrderStatusAliasName = $.trim($("#txtOrderStateName").val());
            if (OrderStatusAliasName.length < 1) {
                OrderStatusAliasName = null;
            }
            var isAct = $.trim($("#ddlVisibitity").val()) == "" ? null : ($.trim($("#ddlVisibitity").val()) == "True" ? true : false);

            OrderStatusMgmt.BindOrdersStatusInGrid(OrderStatusAliasName, isAct);
        },
        CheckOrderStatusUniquness: function(orderStatusId) {
            var orderStatusName = $.trim($('#txtOrderStatusAliasName').val());
            this.config.method = "AspxCommerceWebService.asmx/CheckOrderStatusUniqueness";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ storeId: storeId, portalId: portalId, cultureName: cultureName, orderStatusId: orderStatusId, orderStatusAliasName: orderStatusName });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
            return isUnique;
        },

        ajaxSuccess: function(data) {
            switch (OrderStatusMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    OrderStatusMgmt.BindOrdersStatusInGrid(null, null);
                    OrderStatusMgmt.ClearForm();
                    if (editFlag > 0) {
                        csscody.info('<h2>Information Message</h2><p>Order status has been updated successfully.</p>');
                    } else {
                        csscody.info('<h2>Information Message</h2><p>Order status has been saved successfully.</p>');
                    }
                    $('#divOrderStatusDetail').show();
                    $('#divEditOrderStatus').hide();
                    break;
                case 2:
                    OrderStatusMgmt.BindOrdersStatusInGrid(null, null);
                    OrderStatusMgmt.ClearForm();
                    csscody.info('<h2>Sucessful Message</h2><p>Order status has been deleted sucessfully.</p>');
                    $('#divOrderStatusDetail').show();
                    $('#divEditOrderStatus').hide();
                    break;
                case 3:
                    OrderStatusMgmt.BindOrdersStatusInGrid(null, null);
                    OrderStatusMgmt.ClearForm();
                    csscody.info('<h2>Sucessful Message</h2><p>Order status has been deleted sucessfully.</p>');
                    $('#divOrderStatusDetail').show();
                    $('#divEditOrderStatus').hide();
                    break;
                case 4:
                    isUnique = data.d;
                    if (data.d == true) {
                        $('#txtOrderStatusAliasName').removeClass('error');
                        $('#osErrorLabel').html('');
                    } else {
                        $('#txtOrderStatusAliasName').addClass('error');
                        $('#osErrorLabel').html('This order status already exist!').css("color", "red");
                        return false;
                    }
                    break;
            }
        },
        ajaxFailure: function(data) {
            switch (OrderStatusMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>Error Message</h2><p>Failed to save order status</p>');
                    break;
                case 2:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete order status</p>');
                    break;
                case 3:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete selected order status</p>');
                    break;
            }
        },
        init: function() {
            OrderStatusMgmt.LoadOrderStatusStaticImage();
            OrderStatusMgmt.HideAlldiv();
            $('#divOrderStatusDetail').show();
            OrderStatusMgmt.BindOrdersStatusInGrid(null, null);

            $('#btnAddNew').bind('click', function() {
                $("#btnReset").show();
                $('#divOrderStatusDetail').hide();
                $('#divEditOrderStatus').show();
                OrderStatusMgmt.ClearForm();
                editFlag = 0;
            });

            $("#btnBack").bind('click', function() {
                $("#divOrderStatusDetail").show();
                $("#divEditOrderStatus").hide();
            });

            $("#btnReset").bind('click', function() {
                OrderStatusMgmt.Reset();
                OrderStatusMgmt.ClearForm();
            });

            $('#btnDeleteSelected').bind('click', function() {
                var orderStatus_ids = '';
                $("#tblOrderStatusDetails .attrChkbox").each(function(i) {
                    if ($(this).attr("checked")) {
                        orderStatus_ids += $(this).val() + ',';
                    }
                });
                if (orderStatus_ids != "") {
                    var properties = {
                        onComplete: function(e) {
                            OrderStatusMgmt.ConfirmDeleteMultiple(orderStatus_ids, e);
                        }
                    };
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this selected order status?</p>", properties);
                } else {
                    csscody.alert('<h2>Information Alert</h2><p>Please select at least one order status.</p>');
                }
            });


            $('#btnSaveOrderStatus').bind('click', function() {
                AspxCommerce.CheckSessionActive();
                if (AspxCommerce.vars.IsAlive) {
                    var v = $("#form1").validate({
                        messages: {
                            StatusName: {
                                required: '*',
                                minlength: "* (at least 2 chars)"
                            },
                            ToolTipName: {
                                required: '*',
                                minlength: "* (at least 2 chars)"
                            }
                        }
                    });

                    if (v.form() && OrderStatusMgmt.CheckOrderStatusUniquness(editFlag)) {
                        var orderStatus_id = $(this).attr("name");
                        if (orderStatus_id != '') {
                            OrderStatusMgmt.SaveOrderStatus(orderStatus_id, storeId, portalId, userName, cultureName);
                        } else {
                            OrderStatusMgmt.SaveOrderStatus(0, storeId, portalId, userName, cultureName);
                        }
                    }
                } else {
                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + 'Login.aspx';
                }
            });
            $("#txtOrderStatusAliasName").bind('focusout', function() {

                OrderStatusMgmt.CheckOrderStatusUniquness(editFlag);
            });
            $('#txtOrderStateName,#ddlVisibitity').keyup(function(event) {
                if (event.keyCode == 13) {
                    OrderStatusMgmt.SearchOrderStatus();
                }
            });
        }
    };
    OrderStatusMgmt.init();
});