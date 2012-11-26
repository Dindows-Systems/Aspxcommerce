 var ItemRatingCriterai= "";

 $(function() {
     var storeId = AspxCommerce.utils.GetStoreID();
     var portalId = AspxCommerce.utils.GetPortalID();
     var userName = AspxCommerce.utils.GetUserName();
     var cultureName = AspxCommerce.utils.GetCultureName();
     var customerId = AspxCommerce.utils.GetCustomerID();
     var userIP = AspxCommerce.utils.GetClientIP();
     var countryName = AspxCommerce.utils.GetAspxClientCoutry();
     var sessionCode = AspxCommerce.utils.GetSessionCode();
     var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
     var itemRatingCriteriaFlag = 0;
     ItemRatingCriterai = {
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
             ItemRatingCriterai.LoadItemRatingCriteriaStaticImage();
             ItemRatingCriterai.BindItemRatingCriterialDetails(null, null);
             ItemRatingCriterai.HideAll();
             $("#divShowItemCriteriaDetails").show();

             $("#btnAddNewCriteria").click(function() {
                 $("#" + lblItemRatingFormTitle).html("Add New Rating Criteria");
                 $("#hdnItemCriteriaID").val(0);
                 ItemRatingCriterai.HideAll();
                 $("#divItemCriteriaForm").show();
                 $("#txtNewCriteria").val('');
                 $("#chkIsActive").attr("checked", true);
             });

             $("#btnCriteriaSearch").click(function() {
                 ItemRatingCriterai.SearchCriteria();
             });
             $('#txtSearchCriteria,#ddlIsActive').keyup(function(event) {
                 if (event.keyCode == 13) {
                     ItemRatingCriterai.SearchCriteria();
                 }
             });
             $("#btnSubmitCriteria").click(function() {
                 //   AddUpdateCriteria();
                 var v = $("#form1").validate({
                     rules: {
                         CriteriaTypeName: {
                             minlength: 2
                         }
                     },
                     messages: {
                         CriteriaTypeName: {
                             required: '*',
                             minlength: "(at least 2 chars)"
                         }
                     }
                 });
                 if (v.form()) {
                     ItemRatingCriterai.AddUpdateCriteria();
                 } else {
                     return false;
                 }
             });

             $("#btnCancelCriteriaUpdate").click(function() {
                 ItemRatingCriterai.HideAll();
                 $("#divShowItemCriteriaDetails").show();
                 $('#txtNewCriteria').removeClass('error');
                 $('#txtNewCriteria').parents('td').find('label').remove();
             });

             $('#btnDeleteSelectedCriteria').click(function() {
                 var criteria_Ids = '';
                 //Get the multiple Ids of the item selected
                 $(".itemRatingCriteriaChkbox").each(function(i) {
                     if ($(this).attr("checked")) {
                         criteria_Ids += $(this).val() + ',';
                     }
                 });
                 if (criteria_Ids != "") {
                     var properties = {
                         onComplete: function(e) {
                             ItemRatingCriterai.DeleteMultipleCriteria(criteria_Ids, e);
                         }
                     }
                     csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete all selected rating criteria(s)?</p>", properties);
                 } else {
                     csscody.alert('<h2>Information Alert</h2><p>Please select at least one item rating criteria before delete.</p>');
                 }
             });
         },
         ajaxCall: function(config) {
             $.ajax({
                 type: ItemRatingCriterai.config.type,
                 contentType: ItemRatingCriterai.config.contentType,
                 cache: ItemRatingCriterai.config.cache,
                 async: ItemRatingCriterai.config.async,
                 data: ItemRatingCriterai.config.data,
                 dataType: ItemRatingCriterai.config.dataType,
                 url: ItemRatingCriterai.config.url,
                 success: ItemRatingCriterai.ajaxSuccess,
                 error: ItemRatingCriterai.ajaxFailure
             });
         },
         LoadItemRatingCriteriaStaticImage: function() {
             $('#ajaxItemRatingCriteriaImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
         },

         DeleteMultipleCriteria: function(Ids, event) {
             ItemRatingCriterai.DeleteRatingCriteria(Ids, event);
         },

         HideAll: function() {
             $("#divShowItemCriteriaDetails").hide();
             $("#divItemCriteriaForm").hide();
         },

         AddUpdateCriteria: function() {
             var criteriaName = $("#txtNewCriteria").val();
             var criteriaId = $("#hdnItemCriteriaID").val();
             var isActive = $("#chkIsActive").attr("checked");
             itemRatingCriteriaFlag = criteriaId;
             this.config.url = this.config.baseURL + "AddUpdateItemCriteria";
             this.config.data = JSON2.stringify({ ID: criteriaId, criteria: criteriaName, isActive: isActive, storeID: storeId, portalID: portalId, cultureName: cultureName, userName: userName });
             this.config.ajaxCallMode = 1;
             this.ajaxCall(this.config);
         },

         BindItemRatingCriterialDetails: function(criteria, isAct) {
             this.config.method = "ItemRatingCriteriaManage";
             this.config.data = { ratingCriteria: criteria, isActive: isAct, storeId: storeId, portalId: portalId, cultureName: cultureName };
             var data = this.config.data;
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvItemRatingCriteria_pagesize").length > 0) ? $("#gdvItemRatingCriteria_pagesize :selected").text() : 10;

             $("#gdvItemRatingCriteria").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                     { display: 'Rating Criteria ID', name: 'ratingcriteria_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'itemRatingCriteriaChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                     { display: 'Rating Criteria', name: 'ratingcriteria', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: 'Added On', name: 'added_on', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                     { display: 'Is Active', name: 'isactive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                     { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                 ],
                 buttons: [
                     { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'ItemRatingCriterai.EditItemRatingCriteria', arguments: '1,3' },
                     { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'ItemRatingCriterai.DeleteItemRatingCriteria', arguments: '' }
                 ],
                 rp: perpage,
                 nomsg: "No Records Found!",
                 param: data,
                 current: current_,
                 pnew: offset_,
                 sortcol: { 0: { sorter: false }, 4: { sorter: false} }
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

         EditItemRatingCriteria: function(tblID, argus) {
             switch (tblID) {
                 case "gdvItemRatingCriteria":
                     $("#" + lblItemRatingFormTitle).html("Edit Rating Criteria: '" + argus[3] + "'");
                     $("#hdnItemCriteriaID").val(argus[0]);
                     $("#txtNewCriteria").val(argus[3]);
                     $("#chkIsActive").attr("checked", ItemRatingCriterai.Boolean(argus[4]));
                     ItemRatingCriterai.HideAll();
                     $("#divItemCriteriaForm").show();
                     break;
                 default:
                     break;
             }
         },

         DeleteItemRatingCriteria: function(tblID, argus) {
             switch (tblID) {
                 case "gdvItemRatingCriteria":
                     var properties = {
                         onComplete: function(e) {
                             ItemRatingCriterai.DeleteRatingCriteria(argus[0], e);
                         }
                     };
                     csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this item rating criteria?</p>", properties);
                     break;
                 default:
                     break;
             }
         },

         DeleteRatingCriteria: function(criteriaId, event) {
             if (event) {
                 this.config.url = this.config.baseURL + "DeleteItemRatingCriteria";
                 this.config.data = JSON2.stringify({ IDs: criteriaId, storeID: storeId, portalID: portalId, userName: userName });
                 this.config.ajaxCallMode = 2;
                 this.ajaxCall(this.config);
             }
             return false;
         },

         SearchCriteria: function() {
             var criteria = $.trim($("#txtSearchCriteria").val());
             var isAct = $("#ddlIsActive").val() == "" ? null : $("#ddlIsActive").val() == "True" ? true : false;
             if (criteria.length < 1) {
                 criteria = null;
             }
             ItemRatingCriterai.BindItemRatingCriterialDetails(criteria, isAct);
         },
         ajaxSuccess: function() {
             switch (ItemRatingCriterai.config.ajaxCallMode) {
                 case 0:
                     break;
                 case 1:
                     if (itemRatingCriteriaFlag > 0) {
                         csscody.info('<h2>Successful Message</h2><p>Item rating criteria has been updated successfully.</p>');
                     } else {
                         csscody.info('<h2>Successful Message</h2><p>Item rating criteria has been saved successfully.</p>');
                     }
                     ItemRatingCriterai.BindItemRatingCriterialDetails(null, null);
                     ItemRatingCriterai.HideAll();
                     $("#divShowItemCriteriaDetails").show();
                     break;
                 case 2:
                     ItemRatingCriterai.BindItemRatingCriterialDetails(null, null);
                     csscody.info('<h2>Successful Message</h2><p>Item rating criteria has been deleted successfully.</p>');
                     break;
             }
         }
     };
     ItemRatingCriterai.init();
 });
