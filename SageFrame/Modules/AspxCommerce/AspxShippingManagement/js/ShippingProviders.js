 var shippingProviderMgmt;
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
     shippingProviderMgmt = {
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
                 type: shippingProviderMgmt.config.type,
                 contentType: shippingProviderMgmt.config.contentType,
                 cache: shippingProviderMgmt.config.cache,
                 async: shippingProviderMgmt.config.async,
                 url: shippingProviderMgmt.config.url,
                 data: shippingProviderMgmt.config.data,
                 dataType: shippingProviderMgmt.config.dataType,
                 success: shippingProviderMgmt.ajaxSuccess,
                 error: shippingProviderMgmt.ajaxFailure
             });
         },
         LoadSippingProviderStaticImage: function() {
             $('#ajaxShippingProviderImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
         },

         HideAllDiv: function() {
             $('#divShippingProviderDetails').hide();
             $('#divEditShippingProvider').hide();
         },

         ClearForm: function() {
             $("#btnSaveShippingProvider").removeAttr("name");
             $("#" + lblSPHeading).html("Add New Shipping Provider");

             $('#txtSPServiceCode').val('');
             $('#txtSPName').val('');
             $('#txtSPAliasHelp').val('');
             $("#chkIsActiveSP").removeAttr('checked');
             $("#isActiveSp").show();

             $('#txtSPServiceCode').removeClass('error');
             $('#txtSPServiceCode').parents('td').find('label').remove();
             $('#txtSPName').removeClass('error');
             $('#txtSPName').parents('td').find('label').remove();
             $("#tblShippingProviderList .attrChkbox").each(function(i) {
                 $(this).removeAttr("checked")
             });
             $('#sperrorLabel').html('');
             editFlag = 0;
         },
         BindShippingProviderNameInGrid: function(shippingProviderName, isAct) {
             this.config.url = this.config.baseURL;
             this.config.method = "GetShippingProviderNameList";
             this.config.data = { storeID: storeId, portalID: portalId, cultureName: cultureName, userName: userName, shippingProviderName: shippingProviderName, isActive: isAct };
             var shippingProviderData = this.config.data;

             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#tblShippingProviderList_pagesize").length > 0) ? $("#tblShippingProviderList_pagesize :selected").text() : 10;

             $("#tblShippingProviderList").sagegrid({
                 url: aspxservicePath + "AspxCommerceWebService.asmx/",
                 functionMethod: 'GetShippingProviderNameList',
                 colModel: [
                     { display: 'ShippingProvider ID', name: 'ShippingProvderID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
                     { display: 'Shipping Provider Name', name: 'ShippingProviderName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: 'Shipping Provider Code', name: 'ShippingProviderServiceCode', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: 'Shipping Provider Alias Help', name: 'ShippingProviderAliasHelp', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: 'IsActive', name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                     { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                 ],

                 buttons: [{ display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'shippingProviderMgmt.EditShippingProvider', arguments: '1,2,3,4,5' },
                     { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'shippingProviderMgmt.DeleteShippingProvider', arguments: '1' }
                 ],
                 rp: perpage,
                 nomsg: "No Records Found!",
                 param: shippingProviderData,
                 current: current_,
                 pnew: offset_,
                 sortcol: { 0: { sorter: false }, 5: { sorter: false} }
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
         EditShippingProvider: function(tblID, argus) {
             switch (tblID) {
                 case 'tblShippingProviderList':
                     shippingProviderMgmt.ClearForm();
                     $('#divShippingProviderDetails').hide();
                     $('#divEditShippingProvider').show();
                     $('#btnSPReset').hide();
                     $("#" + lblSPHeading).html("Edit Shipping Provider ID: '" + argus[0] + "'");
                     $('#txtSPServiceCode').val(argus[4]);
                     $('#txtSPName').val(argus[3]);
                     $('#txtSPAliasHelp').val(argus[5]);
                     $("#chkIsActiveSP").attr('checked', shippingProviderMgmt.Boolean(argus[6]));
                     $("#btnSaveShippingProvider").attr("name", argus[0]);
                     editFlag = argus[0];
                     break;
                 default:
                     break;
             }
         },
         SaveShippingProvider: function(shippingProviderID) {
             var spServiceCode = $('#txtSPServiceCode').val();
             var spName = $('#txtSPName').val();
             var spAliasName = $('#txtSPAliasHelp').val();
             var IsActive = $("#chkIsActiveSP").attr('checked');

             this.config.url = this.config.baseURL + "ShippingProviderAddUpdate";
             this.config.data = JSON2.stringify({
                 shippingProviderID: shippingProviderID,
                 storeID: storeId,
                 portalID: portalId,
                 userName: userName,
                 cultureName: cultureName,
                 shippingProviderServiceCode: spServiceCode,
                 shippingProviderName: spName,
                 shippingProviderAliasHelp: spAliasName,
                 isActive: IsActive
             });
             this.config.ajaxCallMode = 1;
             this.ajaxCall(this.config);
         },
         DeleteShippingProvider: function(tblID, argus) {
             switch (tblID) {
                 case 'tblShippingProviderList':
                     shippingProviderMgmt.DeleteSingleSP(argus[0]);
                     break;
                 default:
                     break;
             }
         },
         DeleteSingleSP: function(_shippingProviderID) {
             var properties = {
                 onComplete: function(e) {
                     shippingProviderMgmt.ConfirmSingleSPDelete(_shippingProviderID, e);
                 }
             }
             csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this shipping provider?</p>", properties);
         },

         ConfirmSingleSPDelete: function(_shippingProviderID, event) {
             if (event) {
                 shippingProviderMgmt.DeleteSingleShippingProvider(_shippingProviderID);
             }
             return false;
         },

         DeleteSingleShippingProvider: function(_shippingProviderID) {
             this.config.url = this.config.baseURL + "DeleteShippingProviderByID";
             this.config.data = JSON2.stringify({ shippingProviderID: parseInt(_shippingProviderID), storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
             this.config.ajaxCallMode = 2;
             this.ajaxCall(this.config);
         },
         ConfirmDeleteMultipleSP: function(shippingProvider_ids, event) {

             if (event) {
                 shippingProviderMgmt.DeleteMultipleShippingProviders(shippingProvider_ids);
             }
         },

         DeleteMultipleShippingProviders: function(shippingProvider_ids) {
             this.config.url = this.config.baseURL + "DeleteShippingProviderMultipleSelected";
             this.config.data = JSON2.stringify({ shippingProviderIDs: shippingProvider_ids, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
             this.config.ajaxCallMode = 3;
             this.ajaxCall(this.config);
             return false;
         },

         SearchShippingProvider: function() {
             var shippingProviderName = $.trim($('#txtSearchShippingProviderName').val());
             if (shippingProviderName.length < 1) {
                 shippingProviderName = null;
             }
             var isAct = $.trim($("#ddlSPVisibitity").val()) == "" ? null : ($.trim($("#ddlSPVisibitity").val()) == "True" ? true : false);

             shippingProviderMgmt.BindShippingProviderNameInGrid(shippingProviderName, isAct);
         },
         ajaxSuccess: function(data) {
             switch (shippingProviderMgmt.config.ajaxCallMode) {
                 case 0:
                     break;
                 case 1:
                     if (editFlag > 0) {
                         csscody.info('<h2>Successful Message</h2><p>Shipping provider has been updated successfully.</p>');
                     } else {
                         csscody.info('<h2>Successful Message</h2><p>Shipping provider has been saved successfully.</p>');
                     }
                     shippingProviderMgmt.BindShippingProviderNameInGrid(null, null);
                     $('#divShippingProviderDetails').show();
                     $('#divEditShippingProvider').hide();
                     shippingProviderMgmt.ClearForm();
                     break;
                 case 2:
                     shippingProviderMgmt.ClearForm();
                     csscody.info('<h2>Successful Message</h2><p>Shipping provider has been deleted successfully.</p>');
                     shippingProviderMgmt.BindShippingProviderNameInGrid(null, null);
                     $('#divShippingProviderDetails').show();
                     $('#divEditShippingProvider').hide();
                     break;
                 case 3:
                     shippingProviderMgmt.ClearForm();
                     csscody.info('<h2>Successful Message</h2><p>Selected shipping provider(s) has been deleted successfully.</p>');
                     shippingProviderMgmt.BindShippingProviderNameInGrid(null, null);
                     $('#divShippingProviderDetails').show();
                     $('#divEditShippingProvider').hide();
                     break;
                 case 4:
                     isUnique = data.d;
                     if (data.d == true) {
                         $('#txtSPName').removeClass('error');
                         $('#sperrorLabel').html('');
                     } else {
                         $('#txtSPName').addClass('error');
                         $('#sperrorLabel').html('This provider name already exist!').css("color", "red");
                         return false;
                     }
                     break;
             }
         },
         ajaxFailure: function(data) {
             switch (shippingProviderMgmt.config.ajaxCallMode) {
                 case 0:
                     break;
                 case 1:
                     csscody.error('<h2>Error Message</h2><p>Failed to save shipping provider!</p>');
                     break;
                 case 2:
                     csscody.error('<h2>Error Message</h2><p>Failed to delete shipping provider</p>');
                     break;
                 case 3:
                     csscody.error('<h2>Error Message</h2><p>Failed to delete selected shipping provider(s)</p>');
                     break;
             }
         },
         CheckShippingProviderUniqueness: function(shippingProviderId) {
             var sPServiceCode = $.trim($('#txtSPServiceCode').val());
             var sPServiceName = $.trim($('#txtSPName').val());
             this.config.url = this.config.baseURL + "CheckShippingProviderUniqueness";
             this.config.data = JSON2.stringify({ storeId: storeId, portalId: portalId, cultureName: cultureName, shippingProviderId: shippingProviderId, shippingProviderName: sPServiceName });
             this.config.ajaxCallMode = 4;
             this.ajaxCall(this.config);
             return isUnique;
         },
         init: function() {
             shippingProviderMgmt.HideAllDiv();
             shippingProviderMgmt.LoadSippingProviderStaticImage();
             $('#divShippingProviderDetails').show();
             shippingProviderMgmt.BindShippingProviderNameInGrid(null, null);

             $("#btnSPBack").click(function() {
                 $("#divShippingProviderDetails").show();
                 $("#divEditShippingProvider").hide();
             });
             $("#btnSPReset").click(function() {
                 shippingProviderMgmt.ClearForm();
             });
             $('#btnSPAddNew').click(function() {
                 $("#btnSPReset").show();
                 $('#divShippingProviderDetails').hide();
                 $('#divEditShippingProvider').show();
                 shippingProviderMgmt.ClearForm();
                 $("#btnSaveShippingProvider").attr("name", 0);
             });
             $('#txtSearchShippingProviderName,#ddlSPVisibitity').keyup(function(event) {
                 if (event.keyCode == 13) {
                     shippingProviderMgmt.SearchShippingProvider();
                 }
             });
             $('#btnSaveShippingProvider').click(function() {
                 var v = $("#form1").validate({
                     messages: {
                         name: {
                             required: '*',
                             minlength: "* (at least 2 chars)"
                         },
                         name2: {
                             required: '*',
                             minlength: "* (at least 2 chars)"
                         }
                     }
                 });

                 if (v.form() && shippingProviderMgmt.CheckShippingProviderUniqueness(editFlag)) {
                     var shippingProvider_id = $(this).attr("name");
                     editFlag = shippingProvider_id;
                     if (shippingProvider_id != '') {
                         shippingProviderMgmt.SaveShippingProvider(shippingProvider_id);
                     } else {
                         shippingProviderMgmt.SaveShippingProvider(0);
                     }
                 } else {
                     return false;
                 }
             });
             $('#txtSPName').bind('focusout', function() {
                 shippingProviderMgmt.CheckShippingProviderUniqueness(editFlag);
             });
             $('#btnSPDeleteSelected').click(function() {
                 var shippingProvider_ids = '';
                 $("#tblShippingProviderList .attrChkbox").each(function(i) {
                     if ($(this).attr("checked")) {
                         shippingProvider_ids += $(this).val() + ',';
                     }
                 });
                 if (shippingProvider_ids != "") {
                     var properties = {
                         onComplete: function(e) {
                             shippingProviderMgmt.ConfirmDeleteMultipleSP(shippingProvider_ids, e);
                         }
                     };
                     csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete the selected shipping provider(s)?</p>", properties);
                 } else {
                     csscody.alert('<h2>Information Alert</h2><p>Please select at least one shipping provider before delete.</p>');
                 }
             });
         }
     };

     shippingProviderMgmt.init();
 });
