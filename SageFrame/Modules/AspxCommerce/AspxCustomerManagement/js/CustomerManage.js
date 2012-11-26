var customersManagement;
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
    customersManagement = {
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
            url: "",
            ajaxCallMode: 0
        },
        ajaxCall: function(config) {
            $.ajax({
                type: customersManagement.config.type,
                contentType: customersManagement.config.contentType,
                cache: customersManagement.config.cache,
                async: customersManagement.config.async,
                url: customersManagement.config.url,
                data: customersManagement.config.data,
                dataType: customersManagement.config.dataType,
                success: customersManagement.ajaxSuccess,
                error: customersManagement.ajaxFailure
            });
        },
        LoadCustomerMgmtStaticImage: function() {
            $('#ajaxCutomerMgmtImageLoad').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        HideDiv: function() {
            $("#divCustomerList").hide();
            $("#divAddNewCustomer").hide();
        },

        ClearForm: function() {
            $(".fristName").val('');
            $(".lastName").val('');
            $(".email").val('');
            $(".userName").val('');
            $(".password").val('');
            $(".confirmPassword").val('');
            $(".question").val('');
            $(".answer").val('');
            $("#gdvCustomerDetails .attrChkbox").each(function(i) {
                $(this).removeAttr("checked");
            });
        },
        DeleteMultipleCustomer: function(_CustomerIDs, _storeId, _portalId, _userName) {
            this.config.url = this.config.baseURL + "DeleteMultipleCustomersByCustomerID";
            this.config.data = JSON2.stringify({ customerIDs: _CustomerIDs, storeId: _storeId, portalId: _portalId, userName: _userName });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        ConfirmDeleteMultipleCustomer: function(CustomerIDs, event) {
            if (event) {
                customersManagement.DeleteMultipleCustomer(CustomerIDs, storeId, portalId, userName);
            }
        },
        DeleteSingleCustomer: function(_customerid, _storeId, _portalId, _userName) {
            this.config.url = this.config.baseURL + "DeleteCustomerByCustomerID";
            this.config.data = JSON2.stringify({ customerId: parseInt(_customerid), storeId: _storeId, portalId: _portalId, userName: _userName });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ConfirmSingleDelete: function(_customerid, _storeId, _portalId, _userName, event) {
            if (event) {
                customersManagement.DeleteSingleCustomer(_customerid, _storeId, _portalId, _userName);
            }
            return false;
        },
        DeleteCustomer: function(_customerid, _storeId, _portalId, _userName) {
            var properties = { onComplete: function(e) {
                customersManagement.ConfirmSingleDelete(_customerid, _storeId, _portalId, _userName, e);
            }
            }
            // Ask user's confirmation before delete records        
            csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete customer?</p>", properties);
        },
        DeleteCustomers: function(tblID, argus) {
            switch (tblID) {
                case "gdvCustomerDetails":
                    if (argus[3].toLowerCase() != "yes") {
                        customersManagement.DeleteCustomer(argus[0], storeId, portalId, userName);
                    }
                    else {
                        csscody.alert('<h2>Information Alert</h2><p>Sorry! You can not delete yourself.</p>');
                    }
                    break;
                default:
                    break;
            }
        },
        BindCustomerDetails: function() {
            this.config.url = this.config.baseURL;
            this.config.method = "GetCustomerDetails";
            this.config.data = { storeID: storeId, portalID: portalId, cultureName: cultureName, userName: userName };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCustomerDetails_pagesize").length > 0) ? $("#gdvCustomerDetails_pagesize :selected").text() : 10;
            $("#gdvCustomerDetails").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [

                    { display: 'Customer ID', name: 'Customer_ID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', checkFor: '5', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
                    { display: 'Customer Name', name: 'Customer_Name', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Culture Name', name: 'Culture_Name', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'AddedOn', name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: 'UpdatedOn', name: 'UpdatedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: 'is Same User', name: 'is_same_user', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                      ],
                buttons: [
			        { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '1', callMethod: 'customersManagement.DeleteCustomers', arguments: '5' }
			         ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 6: { sorter: false} }
            });
        },
        ajaxSuccess: function(msg) {
            switch (customersManagement.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    customersManagement.BindCustomerDetails();
                    csscody.info('<h2>Successful Message</h2><p>Customer has been deleted successfully.</p>');
                    $('#divAttribForm').hide();
                    $('#divAttribGrid').show();
                    break;
                case 2:
                    customersManagement.BindCustomerDetails();
                    csscody.info('<h2>Successful Message</h2><p>Selected customer(s) has been deleted successfully.</p>');
                    break;
            }
        },
        ajaxFailure: function() {
            switch (customersManagement.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete Customer!</p>');
                    break;
                case 2:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete Customer!</p>');
                    break;
            }
        },
        sucessMessage: function() {
            csscody.info("<h2>Successful Message</h2><p>Customer has been created successfully.</p>");
        },
        init: function(config) {
            customersManagement.LoadCustomerMgmtStaticImage();
            if (checkIfSucccess == 1) {
                customersManagement.BindCustomerDetails();
                customersManagement.HideDiv();
                $("#divCustomerList").show();
            }
            else {
                customersManagement.HideDiv();
                $("#divAddNewCustomer").show();
                customersManagement.ClearForm();
                return false;
            }

            $("#divCustomerList").show();
            $("#btnAddNewCustomer").click(function() {
                customersManagement.HideDiv();
                $("#divAddNewCustomer").show();
                customersManagement.ClearForm();
            });
            $("#btnBack").click(function() {
                customersManagement.HideDiv();
                $("#divCustomerList").show();
            });
            $('#btnDeleteSelectedCustomer').click(function() {
                var CustomerIDs = '';
                //Get the multiple Ids of the attribute selected
                $("#gdvCustomerDetails .attrChkbox").each(function(i) {
                    if ($(this).attr("checked")) {
                        CustomerIDs += $(this).val() + ',';
                    }
                });
                if (CustomerIDs != "") {
                    var properties = { onComplete: function(e) {
                        customersManagement.ConfirmDeleteMultipleCustomer(CustomerIDs, e);
                    }
                    }
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete the selected customer(s)?</p>", properties);
                }
                else {
                    csscody.alert('<h2>Information Alert</h2><p>Please select at least one customer before delete.</p>');
                }
            });
        }
    }

    customersManagement.init();
});