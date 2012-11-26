 var CostVariants="";
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
     var isUnique = false;
     var VariantID = '';
     var parentRow = '';
     var editFlag = '';
     CostVariants = {
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
             CostVariants.LoadAllImages();
             CostVariants.BindCostVariantInGrid(null);
             CostVariants.HideAllDiv();
             $("#divShowOptionDetails").show();
             CostVariants.BindCostVariantsInputType();

             CostVariants.InitializeVariantTable();

             $('#btnDeleteSelected').click(function() {
                 var costVariant_ids = '';
                 $(".costVariantChkbox").each(function(i) {
                     if ($(this).attr("checked")) {
                         costVariant_ids += $(this).val() + ',';
                     }
                 });
                 if (costVariant_ids != "") {
                     var properties = {
                         onComplete: function(e) {
                             CostVariants.ConfirmDeleteMultipleCostVariants(costVariant_ids, e);
                         }
                     };
                     csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delet the selected variant option?</p>", properties);
                 } else {
                     csscody.alert('<h2>Information Alert</h2><p>Pleade select at least one variant option.</p>');
                 }
             });

             $("#btnSaveVariantOption").click(function() {
                 var counter = 0;
                 var checkfristMsg = false;
                 $('#tblVariantTable>tbody tr:eq(0)').each(function() {
                     if (($(this).find('inpur.cssClassPriceModifier,input.cssClassWeightModifier').val() != '' && $(this).find('input.cssClassDisplayOrder,input.cssClassVariantValueName').val() == '') || ($(this).find('input.cssClassDisplayOrder').val() != '' && $(this).find('input.cssClassVariantValueName').val() == '') || ($(this).find('input.cssClassDisplayOrder').val() == '' && $(this).find('input.cssClassVariantValueName').val() != '')) {
                         csscody.alert('<h2>Informaion Message</h2><p>Please enter Item Cost Variant values.</p>');
                         counter++;
                         checkfristMsg = true;
                         return false;
                     }
                 });
                 var variantsProperties = $("#tblVariantTable tr:gt(1)").find("input.cssClassDisplayOrder,input.cssClassVariantValueName,inpur.cssClassPriceModifier,input.cssClassWeightModifier");
                 var count = 0;
                 if (checkfristMsg != true) {
                     $.each(variantsProperties, function(index, item) {
                         if ($(this).val() <= '') {
                             csscody.alert('<h2>Informaion Message</h2><p>Please enter Item Cost Variant Properties.</p>');
                             count++;
                             return false;
                         }
                     });
                 }
                 if (count == 0 && counter == 0)
                     CostVariants.SaveCostVariantsInfo();
             });

             $('#ddlAttributeType').change(function() {
                 CostVariants.HideAllCostVariantImages();
             });

             $("#btnSearchCostVariants").live("click", function() {
                 CostVariants.SearchCostVariantName();
             });
             $('#txtVariantName').keyup(function(event) {
                 if (event.keyCode == 13) {
                     $("#btnSearchCostVariants").click();
                 }
             });
             $("#btnAddNewVariantOption").click(function() {
                 CostVariants.OnInit();
                 CostVariants.ClearForm();
                 CostVariants.HideAllDiv();

                 $("#divAddNewOptions").show();
                 $("#txtPos").DigitOnly('.cssClassDisplayOrder', '');
                 $("#txtPriceModifier").DigitAndDecimal('.cssClassPriceModifier', '');
                 $("#txtWeightModifier").DigitAndDecimal('.cssClassWeightModifier', '');
             });
             $("#txtPriceModifier").bind("contextmenu", function(e) {
                 return false;
             });
             $("#txtWeightModifier").bind("contextmenu", function(e) {
                 return false;
             });
             $("#btnBack").click(function() {
                 CostVariants.HideAllDiv();
                 $("#divShowOptionDetails").show();
             });

             $("#btnReset").click(function() {
                 CostVariants.OnInit();
                 CostVariants.ClearForm();
             });
             $("#txtDisplayOrder").keypress(function(e) {
                 if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                     $("#dispalyOrder").html("Digits Only").css("color", "red").show().fadeOut(1600);
                     return false;
                 }
             });

             //validate name on focus lost
             $('#txtCostVariantName').blur(function() {
                 // Validate name
                 var errors = '';
                 var costVariantName = $(this).val();
                 var variant_id = $('#btnSaveVariantOption').attr("name");
                 if (variant_id == '') {
                     variant_id = 0;
                 }
                 if (!costVariantName) {
                     errors += 'Please enter cost variant name';
                 }
                 //check uniqueness
                 else if (!CostVariants.IsUnique(costVariantName, variant_id)) {
                     errors += 'Please enter unique cost variant name. "' + costVariantName.trim() + '" already exists.<br/>';
                 }

                 if (errors) {
                     $('.cssClassRight').hide();
                     $('.cssClassError').show();
                     $(".cssClassError").parent('div').addClass("diverror");
                     $('.cssClassError').prevAll("input:first").addClass("error");
                     $('.cssClassError').html(errors);
                     return false;
                 } else {
                     $('.cssClassRight').show();
                     $('.cssClassError').hide();
                     $(".cssClassError").parent('div').removeClass("diverror");
                     $('.cssClassError').prevAll("input:first").removeClass("error");
                 }

             });

             $(".delbutton").click(function() {
                 //Get the Id of the option to delete
                 var costVariantId = $(this).attr("id").replace(/[^0-9]/gi, '');
                 CostVariants.DeleteCostVariants(costVariantId);
                 CostVariants.HideAllDiv();
                 $("#divShowOptionDetails").show();
             });
         },
         ajaxCall: function(config) {
             $.ajax({
                 type: CostVariants.config.type,
                 contentType: CostVariants.config.contentType,
                 cache: CostVariants.config.cache,
                 async: CostVariants.config.async,
                 data: CostVariants.config.data,
                 dataType: CostVariants.config.dataType,
                 url: CostVariants.config.url,
                 success: CostVariants.ajaxSuccess,
                 error: CostVariants.ajaxFailure
             });
         },
         LoadAllImages: function() {
             $("#ajaxLoad").attr("src", '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
             $('.cssClassSuccessImg').attr("src", '' + aspxTemplateFolderPath + '/images/right.jpg');
             $('.cssClassAddRow').attr("src", '' + aspxTemplateFolderPath + '/images/admin/icon_add.gif');
             $('.cssClassCloneRow').attr("src", '' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif');
             $('.cssClassDeleteRow').attr("src", '' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif');
         },

         InitializeVariantTable: function() {
             $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassDeleteRow").hide();

             $("img.cssClassAddRow").live("click", function() {
                 var cloneRow = $(this).closest('tr').clone(true);
                 $(cloneRow).appendTo("#tblVariantTable");
                 $(cloneRow).find("input[type='text']").val('');
                 $(cloneRow).find("input[type='hidden']").val('0');
                 $(cloneRow).find(".cssClassDeleteRow").show();
             });

             $("img.cssClassCloneRow").live("click", function() {
                 var cloneRow = $(this).closest('tr').clone(true);
                 $(cloneRow).appendTo("#tblVariantTable");
                 $(cloneRow).find("input[type='hidden']").val('0');
                 $(cloneRow).find(".cssClassDeleteRow").show();
             });

             $("img.cssClassDeleteRow").live("click", function() {
                 parentRow = $(this).closest('tr');
                 if (parentRow.is(":first-child")) {
                     return false;
                 } else {
                     var costVariantValueId = $(parentRow).find("input[type='hidden']").val();
                     if (costVariantValueId > 0) {
                         CostVariants.DeleteCostVaraiantValue(costVariantValueId, parentRow);
                     } else {
                         $(parentRow).remove();
                     }
                 }
             });
         },

         HideAllCostVariantImages: function() {
             var selectedVal = $("#ddlAttributeType").val();
             if (selectedVal == 9 || selectedVal == 11) { //Radio //CheckBox
                 $("#tblVariantTable>tbody").find("tr:gt(0)").remove();
                 $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassAddRow").hide();
                 $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassCloneRow").hide();
             } else {
                 $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassAddRow").show();
                 $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassCloneRow").show();
             }
         },

         DeleteCostVaraiantValue: function(costVariantValueId, parentRow) {
             var properties = {
                 onComplete: function(e) {
                     CostVariants.ConfirmDeleteCostVariantValue(costVariantValueId, storeId, portalId, userName, cultureName, parentRow, e);
                 }
             };
             // Ask user's confirmation before delete records
             csscody.confirm("<h2>Delete Confirmation</h2><p>Do you want to delete this cost variant value?</p>", properties);
         },

         ConfirmDeleteCostVariantValue: function(costVariantValueId, storeId, portalId, userName, cultureName, parentRow, event) {
             if (event) {
                 this.config.url = this.config.baseURL + "DeleteCostVariantValue";
                 this.config.data = JSON2.stringify({ costVariantValueID: costVariantValueId, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
                 this.config.ajaxCallMode = 1;
                 this.ajaxCall(this.config);
             }
         },

         BindCostVariantInGrid: function(costVariantNm) {
             this.config.method = "GetCostVariants";
             this.config.data = { variantName: costVariantNm, storeID: storeId, portalID: portalId, cultureName: cultureName };
             var data = this.config.data;
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvCostVariantGrid_pagesize").length > 0) ? $("#gdvCostVariantGrid_pagesize :selected").text() : 10;

             $("#gdvCostVariantGrid").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                     { display: 'Cost Variant ID', name: 'costvariant_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'costVariantChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                     { display: 'Cost Variant Name', name: 'cost_variant_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: false },
                     { display: 'Actions', name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                 ],

                 buttons: [{ display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'CostVariants.EditCostVariant', arguments: '1' },
                     { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'CostVariants.DeleteCostVariant', arguments: '' }
                 ],
                 rp: perpage,
                 nomsg: "No Records Found!",
                 param: data, // { variantName: costVariantNm, storeID: storeId, portalID: portalId, cultureName: cultureName },
                 current: current_,
                 pnew: offset_,
                 sortcol: { 0: { sorter: false }, 2: { sorter: false} }
             });
         },

         EditCostVariant: function(tblID, argus) {
             VariantID = argus[0];
             switch (tblID) {
                 case "gdvCostVariantGrid":
                     editFlag = argus[0];
                     CostVariants.ClearForm();
                     CostVariants.OnInit();
                     $(".delbutton").attr("id", 'variantid_' + argus[0]);
                     $(".delbutton").show();

                     $("#btnSaveVariantOption").attr("name", argus[0]);
                     $("#" + lblCostVarFormHeading).html("Edit Cost variant Option: '" + argus[3] + "'");
                     CostVariants.CostVariantsInfoByID(VariantID);
             }
         },
         CostVariantsInfoByID: function(ID) {
             this.config.url = this.config.baseURL + "GetCostVariantInfoByCostVariantID";
             this.config.data = JSON2.stringify({ costVariantID: ID, storeID: storeId, portalID: portalId, cultureName: cultureName });
             this.config.ajaxCallMode = 2;
             this.ajaxCall(this.config);
         },
         FillForm: function(response) {
             $.each(response.d, function(index, item) {
                 //General properties Tab
                 $('#txtCostVariantName').val(item.CostVariantName);
                 $('#ddlAttributeType').val(item.InputTypeID);
                 $('#ddlAttributeType').attr('disabled', 'disabled');
                 $('#txtDisplayOrder').val(item.DisplayOrder);
                 $("#txtDescription").val(item.Description);
                 $('input[name=chkActive]').attr('checked', item.IsActive);

             });
         },
         BindCostVariantValueByCostVariantID: function(costVariantId) {
             this.config.url = this.config.baseURL + "GetCostVariantValuesByCostVariantID";
             this.config.data = JSON2.stringify({ costVariantID: costVariantId, storeID: storeId, portalID: portalId, cultureName: cultureName });
             this.config.ajaxCallMode = 3;
             this.ajaxCall(this.config);
             //$("#hdnCostVariantID").val('');     
         },
         DeleteCostVariant: function(tblID, argus) {
             switch (tblID) {
                 case "gdvCostVariantGrid":
                     CostVariants.DeleteCostVariants(argus[0]);
                     break;
                 default:
                     break;
             }
         },
         DeleteCostVariants: function(_costVariantId) {
             var properties = {
                 onComplete: function(e) {
                     CostVariants.ConfirmSingleDeleteCostVariant(_costVariantId, e);
                 }
             };
             // Ask user's confirmation before delete records
             csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delect this variant option?</p>", properties);
         },
         ConfirmSingleDeleteCostVariant: function(costVariantID, event) {
             if (event) {
                 this.config.url = this.config.baseURL + "DeleteSingleCostVariant";
                 this.config.data = JSON2.stringify({ costVariantID: costVariantID, storeID: storeId, portalID: portalId, userName: userName });
                 this.config.ajaxCallMode = 4;
                 this.ajaxCall(this.config);
             }
             return false;
         },
         ConfirmDeleteMultipleCostVariants: function(costVariant_ids, event) {
             if (event) {
                 CostVariants.DeleteMultipleCostVariants(costVariant_ids, storeId, portalId, userName);
             }
         },
         DeleteMultipleCostVariants: function(_costVariant_ids, _storeId, _portalId, userName) {
             this.config.url = this.config.baseURL + "DeleteMultipleCostVariants";
             this.config.data = JSON2.stringify({ costVariantIDs: _costVariant_ids, storeId: _storeId, portalId: _portalId, userName: userName });
             this.config.ajaxCallMode = 5;
             this.ajaxCall(this.config);
             return false;
         },
         HideAllDiv: function() {
             $("#divShowOptionDetails").hide();
             $("#divAddNewOptions").hide();
         },
         BindCostVariantsInputType: function() {
             this.config.url = this.config.baseURL + "GetCostVariantInputTypeList";
             this.config.data = '{}';
             this.config.ajaxCallMode = 6;
             this.ajaxCall(this.config);
         },
         ClearForm: function() {
             $(".delbutton").removeAttr("id");
             $("#btnSaveVariantOption").removeAttr("name");
             $(".delbutton").hide();
             $("#btnReset").show();
             $("#txtCostVariantName").val('');
             $("#txtDescription").val('');
             $("#ddlAttributeType").attr('selectedIndex', 0);
             $('#ddlAttributeType').removeAttr('disabled');
             $('#txtDisplayOrder').val('');
             $('input[name=chkActive]').attr('checked', 'checked');

             $("#" + lblCostVarFormHeading).html("Add New Cost Variant Option");
             //Clear variant tab
             $("#tblVariantTable>tbody").find("tr:gt(0)").remove();
             $("#tblVariantTable>tbody").find("input[type='text']").val('');
             $("#tblVariantTable>tbody").find("select").val(1);
             $("#tblVariantTable>tbody").find("input[type='hidden']").val('0');
             $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassDeleteRow").hide();
             return false;
         },
         OnInit: function() {
             $('#btnReset').hide();
             $('.cssClassRight').hide();
             $('.cssClassError').hide();
             CostVariants.SelectFirstTab();
         },
         SelectFirstTab: function() {
             var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show'}] });
             $tabs.tabs('select', 0);
         },
         IsUnique: function(costVariantName, costVariantId) {
             this.config.url = this.config.baseURL + "CheckUniqueCostVariantName";
             this.config.data = JSON2.stringify({ costVariantName: costVariantName, costVariantId: costVariantId, storeId: storeId, portalId: portalId });
             this.config.ajaxCallMode = 7;
             this.ajaxCall(this.config);
             return isUnique;
         },
         SaveCostVariantsInfo: function() {
             var variant_id = $('#btnSaveVariantOption').attr("name");
             if (variant_id != '') {
                 CostVariants.SaveCostVariant(variant_id, storeId, portalId, userName, cultureName, false);
             } else {
                 CostVariants.SaveCostVariant(0, storeId, portalId, userName, cultureName, true);
             }
         },
         SaveCostVariant: function(_costVariantId, _storeId, _portalId, _userName, _cultureName, _isNewflag) {
             editFlag = _costVariantId;
             var validateErrorMessage = '';
             // Validate name
             var costVariantName = $('#txtCostVariantName').val();
             if (!costVariantName) {
                 validateErrorMessage += 'Please enter cost variant option name.<br/>';
             } else if (!CostVariants.IsUnique(costVariantName, _costVariantId)) {
                 validateErrorMessage += 'Please enter unique cost variant name."' + costVariantName.trim() + '" already exists.<br/>';
             }

             // Validate cost variant Display Order
             var costVariantDisplayOrder = $("#txtDisplayOrder").val();
             if (!costVariantDisplayOrder) {
                 $("#txtDisplayOrder").focus();
                 validateErrorMessage += 'Please enter cost variant display order.<br/>';
             } else {
                 var value = costVariantDisplayOrder.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
                 var intRegex = /^\d+$/;
                 if (!intRegex.test(value)) {
                     $("#txtDisplayOrder").focus();
                     validateErrorMessage += 'Cost variant display order is numeric value.<br/>';
                 }
             }

             if (validateErrorMessage) {
                 validateErrorMessage = 'The following value are required: <br/>' + validateErrorMessage;
                 csscody.alert('<h2>Information Alert</h2><p>' + validateErrorMessage + '</p>');
                 return false;
             } else {
                 var _StoreID = _storeId;
                 var _PortalID = _portalId;
                 var _CultureName = _cultureName;
                 var _UserName = _userName;

                 var _costVariantName = $('#txtCostVariantName').val();
                 var _inputTypeID = $('#ddlAttributeType').val();

                 var selectedCostVariantType = $("#ddlAttributeType :selected").val();

                 var _Description = $('#txtDescription').val();
                 var _DisplayOrder = $('#txtDisplayOrder').val();
                 var _ShowInGrid = false; //$('input[name=chkShowInGrid]').attr('checked');
                 var _ShowInSearch = false; //$('input[name=chkShowInSearch]').attr('checked');
                 var _ShowInAdvanceSearch = false; //$('input[name=chkUseInAdvancedSearch]').attr('checked'); //h
                 var _ShowInComparison = false; //$('input[name=chkComparable]').attr('checked'); //h
                 var _IsEnableSorting = false; //$('input[name=chkIsEnableSorting]').attr('checked');
                 var _IsUseInFilter = false; //$('input[name=chkIsUseInFilter]').attr('checked');
                 var _IsIncludeInPriceRule = false; //$('input[name=chkUseForPriceRule]').attr('checked'); //h 
                 var _IsIncludeInPromotions = false; //$('input[name=chkUseForPromoRule]').attr('checked');
                 var _IsShownInRating = false; //$('input[name=chkUseForRating]').attr('checked');
                 var _IsActive = $('input[name=chkActive]').attr('checked');
                 var _IsModified = !(_isNewflag);
                 var _IsNewflag = _isNewflag;

                 var _VariantOptions = '';
                 $('#tblVariantTable>tbody tr').each(function() {
                     _VariantOptions += $(this).find(".cssClassVariantValue").val() + '%';
                     _VariantOptions += $(this).find(".cssClassDisplayOrder").val() + '%'; //{required:true,digits:true,minlength:1}
                     _VariantOptions += $(this).find(".cssClassVariantValueName").val() + '%'; //{required:true,minlength:2}
                     if ($(this).find(".cssClassVariantValueName").val() != '' && $(this).find(".cssClassPriceModifier").val() == '') {
                         _VariantOptions += 0.00 + '%';
                     } else {
                         _VariantOptions += $(this).find(".cssClassPriceModifier").val() + '%'; //{required:true,number:true,minlength:1}
                     }
                     _VariantOptions += $(this).find(".cssClassPriceModifierType").val() + '%';
                     if ($(this).find(".cssClassVariantValueName").val() != '' && $(this).find(".cssClassWeightModifier").val() == '') {
                         _VariantOptions += 0.00 + '%';
                     } else {
                         _VariantOptions += $(this).find(".cssClassWeightModifier").val() + '%'; //{required:true,number:true,minlength:1}
                     }
                     _VariantOptions += $(this).find(".cssClassWeightModifierType").val() + '%';
                     _VariantOptions += $(this).find(".cssClassIsActive").val() + '#';
                 });

                 CostVariants.AddCostVariantInfo(_costVariantId, _costVariantName, _Description, _CultureName, _inputTypeID, _DisplayOrder, _ShowInGrid, _ShowInSearch,
                     _ShowInAdvanceSearch, _ShowInComparison, _IsEnableSorting, _IsUseInFilter, _IsIncludeInPriceRule, _IsIncludeInPromotions, _IsShownInRating,
                     _StoreID, _PortalID, _IsActive, _IsModified, _UserName, _VariantOptions, _IsNewflag);
             }
             return false;
         },
         AddCostVariantInfo: function(_costVariantId, _costVariantName, _Description, _CultureName, _inputTypeID, _DisplayOrder, _ShowInGrid, _ShowInSearch,
             _ShowInAdvanceSearch, _ShowInComparison, _IsEnableSorting, _IsUseInFilter, _IsIncludeInPriceRule, _IsIncludeInPromotions, _IsShownInRating,
             _StoreID, _PortalID, _IsActive, _IsModified, _UserName, _VariantOptions, _IsNewflag) {
             var params = {
                 costVariantID: _costVariantId,
                 costVariantName: _costVariantName,
                 description: _Description,
                 cultureName: _CultureName,
                 inputTypeID: _inputTypeID,
                 displayOrder: _DisplayOrder,
                 showInGrid: _ShowInGrid,
                 showInSearch: _ShowInSearch,
                 showInAdvanceSearch: _ShowInAdvanceSearch,
                 showInComparison: _ShowInComparison,
                 isEnableSorting: _IsEnableSorting,
                 isUseInFilter: _IsUseInFilter,
                 isIncludeInPriceRule: _IsIncludeInPriceRule,
                 isIncludeInPromotions: _IsIncludeInPromotions,
                 isShownInRating: _IsShownInRating,
                 storeId: _StoreID,
                 portalId: _PortalID,
                 isActive: _IsActive,
                 isModified: _IsModified,
                 userName: _UserName,
                 variantOptions: _VariantOptions,
                 isNewflag: _IsNewflag
             };
             this.config.url = this.config.baseURL + "SaveAndUpdateCostVariant";
             this.config.data = JSON2.stringify(params);
             this.config.ajaxCallMode = 8;
             this.ajaxCall(this.config);
         },
         SearchCostVariantName: function() {
             var costVariantNm = $.trim($("#txtVariantName").val());
             if (costVariantNm.length < 1) {
                 costVariantNm = null;
             }
             CostVariants.BindCostVariantInGrid(costVariantNm);
         },
         ajaxSuccess: function(data) {
             switch (CostVariants.config.ajaxCallMode) {
                 case 0:
                     break;
                 case 1:
                     $(parentRow).remove();
                     csscody.info('<h2>Successful Message</h2><p>Cost variants properties value has been deleted successfully.</p>');
                     return false;
                     break;
                 case 2:
                     CostVariants.FillForm(data);
                     //varinants Tab
                     CostVariants.BindCostVariantValueByCostVariantID(VariantID);
                     CostVariants.HideAllDiv();
                     $("#divAddNewOptions").show();
                     break;
                 case 3:
                     if (data.d.length > 0) {
                         $("#tblVariantTable>tbody").html('');
                         $.each(data.d, function(index, item) {
                             if (item.DisplayOrder == null) {
                                 item.DisplayOrder = '';
                             }
                             var newVariantRow = '';
                             newVariantRow += '<tr><td><input type="hidden" size="3" class="cssClassVariantValue" value="' + item.CostVariantsValueID + '"><input type="text" size="3" class="cssClassDisplayOrder" value="' + item.DisplayOrder + '"></td>';
                             newVariantRow += '<td><input type="text" class="cssClassVariantValueName" value="' + item.CostVariantsValueName + '"></td>';
                             newVariantRow += '<td><input type="text" size="5" class="cssClassPriceModifier" value="' + item.CostVariantsPriceValue + '">&nbsp;/&nbsp;';
                             newVariantRow += '<select class="cssClassPriceModifierType priceModifierType_' + item.CostVariantsValueID + '"><option value="false">$</option><option value="true">%</option></select></td>';
                             newVariantRow += '<td><input type="text" size="5" class="cssClassWeightModifier" value="' + item.CostVariantsWeightValue + '">&nbsp;/&nbsp;';
                             newVariantRow += '<select class="cssClassWeightModifierType weightModifierType_' + item.CostVariantsValueID + '"><option value="false">lbs</option><option value="true">%</option></select></td>';
                             newVariantRow += '<td><select class="cssClassIsActive isActive_' + item.CostVariantsValueID + '"><option value="true">Active</option><option value="false">Disabled</option></select></td>';
                             newVariantRow += '<td><span class="nowrap">';
                             newVariantRow += '<img width="13" height="18" border="0" align="top" class="cssClassAddRow" title="Add empty item" alt="Add empty item" name="add" src="' + aspxTemplateFolderPath + '/images/admin/icon_add.gif">&nbsp;';
                             newVariantRow += '<img width="13" height="18" border="0" align="top" class="cssClassCloneRow" alt="Clone this item" title="Clone this item" name="clone" src="' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif">&nbsp;';
                             newVariantRow += '<img width="12" height="18" border="0" align="top" class="cssClassDeleteRow" alt="Remove this item" name="remove" src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif">&nbsp;';
                             newVariantRow += '</span></td></tr>';
                             $("#tblVariantTable>tbody").append(newVariantRow);

                             $('.priceModifierType_' + item.CostVariantsValueID).val('' + item.IsPriceInPercentage + '');
                             $('.weightModifierType_' + item.CostVariantsValueID).val('' + item.IsWeightInPercentage + '');
                             $('.isActive_' + item.CostVariantsValueID).val('' + item.IsActive + '');
                             $("#divAddNewOptions").show();
                             $("#txtPos").DigitOnly('.cssClassDisplayOrder', '');
                             $("#txtPriceModifier").DigitAndDecimal('.cssClassPriceModifier', '');
                             $("#txtWeightModifier").DigitAndDecimal('.cssClassWeightModifier', '');
                             $(".cssClassWeightModifier").bind("contextmenu", function(e) {
                                 return false;
                             });
                             $(".cssClassPriceModifier").bind("contextmenu", function(e) {
                                 return false;
                             });
                         });
                         $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassDeleteRow").hide();
                     }
                     break;
                 case 4:
                     CostVariants.BindCostVariantInGrid(null);
                     csscody.info('<h2>Successful Message</h2><p>Cost variants option has been deleted successfully.</p>');
                     break;
                 case 5:
                     CostVariants.BindCostVariantInGrid(null);
                     csscody.info('<h2>Successful Message</h2><p>Cost variants options has been deleted successfully.</p>');
                     break;
                 case 6:
                     $.each(data.d, function(index, item) {
                         $("#ddlAttributeType").append("<option value=" + item.InputTypeID + ">" + item.InputType + "</option>");
                     });
                     break;
                 case 7:
                     isUnique = data.d;
                     break;
                 case 8:
                     CostVariants.BindCostVariantInGrid(null);
                     CostVariants.HideAllDiv();
                     $("#divShowOptionDetails").show();
                     if (editFlag > 0) {
                         csscody.info('<h2>Successful Message</h2><p>Cost variants option has been updated successfully.</p>');
                     } else {
                         csscody.info('<h2>Successful Message</h2><p>Cost variants option has been saved successfully.</p>');
                     }
                     break;
             }
         }
     };
     CostVariants.init();
 });