 var ItemMangement = {};
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
     var listImages = new Array();
     var DatePickerIDs = new Array();
     var FileUploaderIDs = new Array();
     var htmlEditorIDs = new Array();
     var editorList = new Array();
     var rowCount = 0;
     var contents = '';
     var isSaved = false;
     var FormCount = new Array();
     var itemEditFlag = 0;
     ItemMangement = {
         config: {
             isPostBack: false,
             async: false,
             cache: false,
             type: 'POST',
             contentType: "application/json; charset=utf-8",
             data: '{}',
             dataType: 'json',
             baseURL: aspxservicePath + 'AspxCommerceWebService.asmx/',
             method: "",
             url: ""
         },
         vars: {
             isUnique: false,
             itemId: 0,
             itemCostVariantId: 0,
             costVariantId: 0,
             attributeSetId: 1,
             itemTypeId: 1,
             showDeleteBtn: false,
             arrRoles: new Array(),
             parentRow: ''
             //,
             //ID: 0,
             //Editor: ''
         },
         init: function() {

             //         $('#txtMaxDownload').bind('paste', function(e) {
             //             e.preventDefault();       
             //         });
             $("#btnSaveItemVariantOption").click(function() {
                 var variantsProperties = $("#tblVariantTable tr:gt(1)").find("input.cssClassDisplayOrder,input.cssClassVariantValueName,inpur.cssClassPriceModifier,input.cssClassWeightModifier");
                 var count = 0;
                 $.each(variantsProperties, function(index, item) {
                     if ($(this).val() <= '') {
                         //alert("Enter Item Cost Variant Properties");
                         csscody.alert("<h2>Information Alert</h2><p>Please enter item cost variant properties.</P");
                         count++;
                         return false;
                     } else {
                         return true;
                     }
                 });
                 var counter = 0;
                 $('#tblVariantTable>tbody tr').each(function() {
                     if ($(this).find('inpur.cssClassPriceModifier,input.cssClassWeightModifier').val() != '' && $(this).find('input.cssClassDisplayOrder,input.cssClassVariantValueName').val() == '') {
                         csscody.alert("<h2>Information Alert</h2><p>Please enter item cost variant properties.</P");
                         counter++;
                         return false;
                     } else {
                         return true;
                     }
                 });
                 if (count == 0 && counter == 0)
                     ItemMangement.SaveItemCostVariantsInfo();
             });

             $('#ddlAttributeType').change(function() {
                 ItemMangement.HideAllCostVariantImages();
             });
             ItemMangement.LoadItemStaticImage();
             // ItemMangement.BindCostVariantsInputType();

             ItemMangement.InitializeVariantTable();

             ItemMangement.BindItemsGrid(null, null, null, null, null, null);
             ItemMangement.BindItemType();
             ItemMangement.BindAttributeSet();
             //CreateCategoryTree();
             $("#gdvItems_grid").show();
             $("#gdvItems_form").hide();
             $("#gdvItems_accordin").hide();
             //$("#ItemMgt_itemID").val(0);

             $('#btnDeleteSelected').click(function() {
                 AspxCommerce.CheckSessionActive();
                 if (AspxCommerce.vars.IsAlive) {
                     var item_ids = '';
                     //Get the multiple Ids of the item selected
                     $(".itemsChkbox").each(function(i) {
                         if ($(this).attr("checked")) {
                             item_ids += $(this).val() + ',';
                         }
                     });
                     if (item_ids != "") {
                         var properties = {
                             onComplete: function(e) {
                                 ItemMangement.ConfirmDeleteMultiple(item_ids, e);
                             }
                         };
                         csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete the selected item(s)?</p>", properties);
                     } else {
                         csscody.alert('<h2>Information Alert</h2><p>Please select at least one item before delete.</p>');
                     }
                 } else {
                     window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + 'Login.aspx';
                 }
             });

             $('#btnReset').click(function() {
                 ItemMangement.ClearForm();
             });

             $('#btnContinue').click(function() {
                 AspxCommerce.Busy.LoadingShow();
                 $('#Todatevalidation').attr('class', '');
                 $('#Fromdatevalidation').attr('class', '');
                 $("#ItemMgt_itemID").val(0);
                 var attributeSetId = '';
                 var itemTypeId = '';
                 attributeSetId = $("#ddlAttributeSet").val();
                 itemTypeId = $("#ddlItemType").val();
                 $("#spanSample").html("");
                 $("#spanActual").html("");
                 ItemMangement.ContinueForm(false, attributeSetId, itemTypeId, 0);
             });

             $('#btnSearchItems').live('click', function() {
                 ItemMangement.SearchItems();
             });
             $('#txtSearchSKU,#txtSearchName,#ddlSearchItemType,#ddlAttributeSetName,#ddlVisibitity,#ddlIsActive').keyup(function(event) {
                 if (event.keyCode == 13) {
                     $('#btnSearchItems').click();
                 }
             });

             $("#btnAddNew").click(function() {
                 AspxCommerce.Busy.LoadingShow();
                 $("#btnDelete").hide();
                 // ItemMangement.ClearAttributeForm();
                 $("#gdvItems_grid").hide();
                 $("#gdvItems_form").show();
                 $("#gdvItems_accordin").hide();
                 //            $("#ddlSearchItemType>option").remove();
                 //            $("#ddlAttributeSetName>option").remove();
                 $("#ddlSearchItemType>option").val(1);
                 $("#ddlAttributeSetName>option").val(1);
                 AspxCommerce.Busy.LoadingHide();
             });

             $("#btnBack").click(function() {
                 $("#gdvItems_grid").show();
                 $("#gdvItems_form").hide();
                 $("#gdvItems_accordin").hide();
             });

             $('#btnReturn').live('click', function() {
                 ItemMangement.BackToItemGrid();
             });

             $('#btnResetForm').click(function() {
                 ItemMangement.ClearAttributeForm();
             });

             $('#btnDelete').live('click', function() {
                 ItemMangement.vars.itemId = $("#ItemMgt_itemID").val();
                 ItemMangement.ClickToDelete(ItemMangement.vars.itemId);
             });

             $(".cssClassClose").click(function() {
                 $('#fade, #popuprel2').fadeOut();
                 $("#VariantsImagesTable").hide();
             });

             $("#btnImageBack").click(function() {
                 $('#fade, #popuprel2').fadeOut();
                 $("#VariantsImagesTable").hide();
             });

             //validate name on focus lost
             $('#txtCostVariantName').live('blur', function() {
                 // Validate name
                 var errors = '';
                 var costVariantName = $(this).val();
                 var variant_id = $('#btnSaveItemVariantOption').attr("name");
                 if (variant_id == '') {
                     variant_id = 0;
                 }
                 if (!costVariantName) {
                     errors += 'Please enter cost variant name';
                 }
                 //check uniqueness
                 else if (!ItemMangement.IsUniqueCostVariant(costVariantName, variant_id)) {
                     errors += 'Please enter unique cost variant name! "' + costVariantName.trim() + '" already exists.<br/>';
                     $('#txtCostVariantName').val('');
                 }

                 if (errors) {
                     $('.cssClassCostVarRight').hide();
                     $('.cssClassCostVarError').show();
                     $(".cssClassCostVarError").parent('div').addClass("diverror");
                     $('.cssClassCostVarError').prevAll("input:first").addClass("error");
                     $('.cssClassCostVarError').html(errors);
                     return false;
                 } else {
                     $('.cssClassCostVarRight').show();
                     $('.cssClassCostVarError').hide();
                     $(".cssClassCostVarError").parent('div').removeClass("diverror");
                     $('.cssClassCostVarError').prevAll("input:first").removeClass("error");
                 }
             });

             $('#btnApplyExisingOption').click(function() {
                 var variant_Id = $('#ddlExistingOptions').val();
                 var item_Id = $("#ItemMgt_itemID").val();
                 if (variant_Id != null && item_Id != null) {
                     var params = { itemId: item_Id, costVariantID: variant_Id, storeId: storeId, portalId: portalId, cultureName: cultureName, userName: userName };
                     var mydata = JSON2.stringify(params);
                     $.ajax({
                         type: "POST",
                         url: aspxservicePath + "AspxCommerceWebService.asmx/AddItemCostVariant",
                         data: mydata,
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         success: function() {
                             $('#fade, #popuprel').fadeOut();
                             ItemMangement.BindItemCostVariantInGrid(item_Id);
                             ItemMangement.BindCostVariantsOptions(item_Id);
                             csscody.info("<h2>Successful Message</h2><p>Cost variant option has been applied successfully.</p>");
                         },
                         error: function() {
                             csscody.error('<h2>Error Message</h2><p>Failed to save item cost variant!</p>');
                         }
                     });
                 }
             });
         },
         ajaxCall: function(config) {
             $.ajax({
                 type: ItemMangement.config.type,
                 contentType: ItemMangement.config.contentType,
                 cache: ItemMangement.config.cache,
                 async: ItemMangement.config.async,
                 url: ItemMangement.config.url,
                 data: ItemMangement.config.data,
                 dataType: ItemMangement.config.dataType,
                 success: ItemMangement.ajaxSuccess,
                 error: ItemMangement.ajaxFailure
             });
         },
         LoadItemStaticImage: function() {
             $('.cssClassAddRow').attr('src', '' + aspxTemplateFolderPath + '/images/admin/icon_add.gif');
             $('.cssClassCloneRow').attr('src', '' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif');
             $('.cssClassDeleteRow').attr('src', '' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif');
             $('.cssClassSuccessImg').attr('src', '' + aspxTemplateFolderPath + '/images/right.jpg');
             $('#ajaxImageLoader').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
         },
         BindTaxManageRule: function() {
             var isActive = true;
             this.config.url = this.config.baseURL + "GetAllTaxRules";
             this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, isActive: isActive });
             this.config.ajaxCallMode = 1;
             this.ajaxCall(this.config);
         },
         BindItemQuantityDiscountsByItemID: function(itemId) {
             this.config.url = this.config.baseURL + "GetItemQuantityDiscountsByItemID";
             this.config.data = JSON2.stringify({ itemId: itemId, storeID: storeId, portalID: portalId, userName: userName });
             this.config.ajaxCallMode = 2;
             this.ajaxCall(this.config);
         },

         GetUserInRoleList: function(arrRoles) {
             var IsAll = true;
             this.config.url = this.config.baseURL + "BindRoles";
             this.config.data = JSON2.stringify({ portalID: portalId, isAll: IsAll, userName: userName });
             this.vars.arrRoles = arrRoles;
             this.config.ajaxCallMode = 3;
             this.ajaxCall(this.config);
         },

         BindRolesList: function(item) {
             var RoleInCheckbox = '<input type="checkbox" class="cssClassCheckBox"  value="' + item.RoleID + '" /><label>' + item.RoleName + '</label>';
             $('.cssClassUsersInRoleCheckBox').append(RoleInCheckbox);
         },

         SaveItemDiscountQuantity: function() {
             var _DiscountQuantityOptions = '';
             var item_Id = $("#ItemMgt_itemID").val();
             $("#tblQuantityDiscount>tbody tr").each(function() {
                 _DiscountQuantityOptions += $(this).find(".cssClassQuantityDiscount").val() + ',';
                 if ($(this).find(".cssClassQuantity").val() != '') {
                     _DiscountQuantityOptions += $(this).find(".cssClassQuantity").val() + ',';
                 } else {
                     _DiscountQuantityOptions += '0' + ',';
                 }
                 if ($(this).find(".cssClassPrice").val() != '') {
                     _DiscountQuantityOptions += $(this).find(".cssClassPrice").val() + '%';
                 } else {
                     _DiscountQuantityOptions += '0' + '%';
                 }
                 var check = $(this).find("input[type='checkbox']:checked");
                 if (check.length != 0) {
                     $.each(check, function() {
                         _DiscountQuantityOptions += $(this).val() + ',';
                     });
                     _DiscountQuantityOptions = _DiscountQuantityOptions.substring(0, _DiscountQuantityOptions.length - 1);
                 } else {
                     _DiscountQuantityOptions += '0';
                 }
                 _DiscountQuantityOptions += '#';
             });

             this.config.url = this.config.baseURL + "SaveItemDiscountQuantity";
             this.config.data = JSON2.stringify({ discountQuantity: _DiscountQuantityOptions, itemID: item_Id, storeID: storeId, portalID: portalId, userName: userName });
             this.config.ajaxCallMode = 4;
             this.ajaxCall(this.config);
         },
         HideAllCostVariantImages: function() {
             var selectedVal = $("#ddlAttributeType").val();
             if (selectedVal == 9 || selectedVal == 11) {//Radio //CheckBox
                 $("#tblVariantTable>tbody").find("tr:gt(0)").remove();
                 $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassAddRow").hide();
                 $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassCloneRow").hide();
             }
             else {
                 $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassAddRow").show();
                 $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassCloneRow").show();
             }
         },

         InitializeVariantTable: function() {
             $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassDeleteRow").hide();

             $("img.cssClassAddRow").live("click", function() {
                 var cloneRow = $(this).closest('tr').clone(true);
                 $(cloneRow).appendTo("#tblVariantTable");
                 $(cloneRow).find("input[type='text']").val('');
                 $(cloneRow).find("input[type='hidden']").val('0');
                 $(cloneRow).find(".cssClassDeleteRow").show();
                 $(cloneRow).find(".classAddImages").removeAttr("name");
                 var x = $("#tblVariantTable>tbody>tr").length - 1;
                 $(cloneRow).find(".classAddImages").attr("value", x);
                 var rid = $("#tblVariantTable>tbody>tr").length - 1;
                 $(cloneRow).find(".classAddImagesEdit").removeAttr("name").removeAttr("value");
                 $(cloneRow).find(".classAddImagesEdit").attr("value", rid);
                 $("#VariantsImagesTable>tbody").html('');
             });

             $("img.cssClassCloneRow").live("click", function() {
                 var cloneRow = $(this).closest('tr').clone(true);
                 $(cloneRow).appendTo("#tblVariantTable");
                 $(cloneRow).find("input[type='hidden']").val('0');
                 $(cloneRow).find(".cssClassDeleteRow").show();
             });

             $("img.cssClassDeleteRow").live("click", function() {
                 x--;
                 var parentRow = $(this).closest('tr');
                 if (parentRow.is(":first-child")) {
                     return false;
                 } else {
                     var costVariantValueID = $(parentRow).find("input[type='hidden']").val();
                     if (costVariantValueID > 0) {
                         var item_Id = $("#ItemMgt_itemID").val();
                         ItemMangement.vars.parentRow = $(parentRow);
                         ItemMangement.DeleteItemCostVaraiantValue(costVariantValueID, item_Id, parentRow);
                     } else {
                         $(parentRow).remove();
                     }
                 }
             });

             //FOR Item Quantity Discount
             $("img.cssClassAddDiscountRow").live("click", function() {
                 var cloneRow = $(this).closest('tr').clone(true);
                 $(cloneRow).appendTo("#tblQuantityDiscount");
                 $(cloneRow).find("input[type='text']").val('');
                 $(cloneRow).find("input[type='hidden']").val('0');
                 $(cloneRow).find("input[type='checkbox']").removeAttr('checked');
                 $(cloneRow).find("img.cssClassDeleteDiscountRow").show();
             });
             $("img.cssClassCloneDiscountRow").live("click", function() {
                 var cloneRow = $(this).closest('tr').clone(true);
                 $(cloneRow).appendTo("#tblQuantityDiscount");
                 $(cloneRow).find("input[type='hidden']").val('0');
                 $(cloneRow).find("img.cssClassDeleteDiscountRow").show();
             });

             $("img.cssClassDeleteDiscountRow").live("click", function() {
                 var parentRow = $(this).closest('tr');
                 if (parentRow.is(":first-child")) {
                     return false;
                 } else {
                     var quantityDiscountID = $(parentRow).find("input[type='hidden']").val();
                     if (quantityDiscountID > 0) {
                         ItemMangement.vars.parentRow = $(parentRow);
                         ItemMangement.DeleteItemQuantityDiscount(quantityDiscountID, parentRow);
                     } else {
                         $(parentRow).remove();
                     }
                 }
             });
         },

         //For item quantity discount
         DeleteItemQuantityDiscount: function(quantityDiscountID, parentRow) {
             var properties = {
                 onComplete: function(e) {
                     ItemMangement.ConfirmDeleteItemQuantityDiscount(quantityDiscountID, storeId, portalId, userName, parentRow, e);
                 }
             };
             // Ask user's confirmation before delete records
             csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this item quatity discount value?</p>", properties);
         },

         ConfirmDeleteItemQuantityDiscount: function(quantityDiscountID, storeId, portalId, userName, parentRow, event) {
             if (event) {
                 var _itemId = $("#ItemMgt_itemID").val();
                 this.config.url = this.config.baseURL + "DeleteItemQuantityDiscount";
                 this.config.data = JSON2.stringify({ quantityDiscountID: quantityDiscountID, itemID: _itemId, storeID: storeId, portalID: portalId, userName: userName });
                 this.config.ajaxCallMode = 5;
                 this.ajaxCall(this.config);
             }
         },

         DeleteItemCostVaraiantValue: function(costVariantValueID, itemId, parentRow) {
             var properties = {
                 onComplete: function(e) {
                     ItemMangement.ConfirmDeleteItemCostVariantValue(costVariantValueID, itemId, storeId, portalId, userName, cultureName, parentRow, e);
                 }
             };
             // Ask user's confirmation before delete records
             csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this item cost variant value?</p>", properties);
         },

         ConfirmDeleteItemCostVariantValue: function(costVariantValueID, itemId, storeId, portalId, userName, cultureName, parentRow, event) {
             if (event) {
                 this.config.url = this.config.baseURL + "DeleteItemCostVariantValue";
                 this.config.data = JSON2.stringify({ costVariantValueID: costVariantValueID, itemId: itemId, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
                 this.config.ajaxCallMode = 6;
                 this.ajaxCall(this.config);
             }
             return false;
         },

         BindItemCostVariantInGrid: function(itemId) {

             this.config.method = "GetItemCostVariants";
             this.config.data = { storeID: storeId, portalID: portalId, cultureName: cultureName, itemID: itemId };
             var data = this.config.data;
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvItemCostVariantGrid_pagesize").length > 0) ? $("#gdvItemCostVariantGrid_pagesize :selected").text() : 10;

             $("#gdvItemCostVariantGrid").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                 { display: 'Item Cost Variant ID', name: 'item_costvariant_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                 { display: 'Item ID', name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                 { display: 'Cost Variant ID', name: 'cost_variant_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                 { display: 'Cost Variant Name', name: 'cost_variant_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Actions', name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
         		],
                 buttons: [
                 { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'ItemMangement.EditItemCostVariant', arguments: '1,2,3' },
                 { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'ItemMangement.DeleteItemCostVariant', arguments: '1' }
         		],
                 rp: perpage,
                 nomsg: "No Records Found!",
                 param: data,
                 current: current_,
                 pnew: offset_,
                 sortcol: { 4: { sorter: false} }
             });
         },

         EditItemCostVariant: function(tblID, argus) {
             $(".cssClassDisplayOrder").live('keypress', function(e) {
                 if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                     if (e.which == 45) { return true; }
                     return false;
                 }
             });

             $(".cssClassPriceModifier").live('keypress', function(e) {
                 if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                     if (e.which == 45) { return true; }
                     return false;
                 }
             });

             $(".cssClassWeightModifier").live('keypress', function(e) {
                 if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                     if (e.which == 45) { return true; }
                     return false;
                 }
             });
             $(".cssClassWeightModifier").live("contextmenu", function(e) {
                 return false;
             });

             $(".cssClassPriceModifier").live("contextmenu", function(e) {
                 return false;
             });
             switch (tblID) {
                 case "gdvItemCostVariantGrid":
                     ItemMangement.ClearVariantForm();
                     $('#ddlAttributeType').html('');
                     ItemMangement.BindCostVariantsInputType();
                     ItemMangement.OnInit();
                     $("#tabFrontDisplay").hide();

                     $("#hdnItemCostVar").val(argus[0]);

                     $("#btnSaveItemVariantOption").attr("name", argus[4]);
                     $("#lblHeading").html('Edit Cost variant Option: ' + argus[5]);
                     ItemMangement.BindItemCostVariantsDetail(argus[0], argus[3], argus[4]);
                     break;
                 default:
                     break;
             }
         },

         BindItemCostVariantsDetail: function(itemCostVariantId, itemId, costVariantID) {
             this.config.url = this.config.baseURL + "GetItemCostVariantInfoByCostVariantID";
             this.config.data = JSON2.stringify({ itemCostVariantId: itemCostVariantId, itemId: itemId, costVariantID: costVariantID, storeID: storeId, portalID: portalId, cultureName: cultureName });
             this.vars.itemCostVariantId = itemCostVariantId;
             this.vars.itemId = itemId;
             this.vars.costVariantId = costVariantID;
             this.config.ajaxCallMode = 7;
             this.ajaxCall(this.config);
         },

         SubmitForm: function(frmID, attributeSetId, itemTypeId) {
             var itemId = $("#ItemMgt_itemID").val()
             var frm = $("#" + frmID);
             for (var i = 0; i < editorList.length; i++) {
                 var id = String(editorList[i].ID);
                 var textArea = $("#" + id.replace("_editor", ""));
                 textArea.val(Encoder.htmlEncode(editorList[i].Editor.getData()));
             }

             // Prevent submit if validation fails
             var itemSKUTxtBoxID = $("#hdnSKUTxtBox").val();
             var itemSKU = $("#" + itemSKUTxtBoxID).val();
             var validPrice = false;
             var validNewDate = false;
             var validActiveDate = false;
             var validFeaturedDate = false;
             var validSpecialDate = false;

             validPrice = ItemMangement.ValidateExtraField("classItemPrice", "classItemListPrice", "price", "List Price should be equal or greater than Price!");
             validNewDate = ItemMangement.ValidateExtraField("classNewFrom", "classNewTo", "date", "To date must be higher date than From date!");
             validActiveDate = ItemMangement.ValidateExtraField("classActiveFrom", "classActiveTo", "date", "Active To date must be higher date than Active From date!");

             if ($('.FeaturedDropDown').val() == 7) {
                 validFeaturedDate = ItemMangement.ValidateExtraField("classFeaturedFrom", "classFeaturedTo", "date", "Featured To date must be higher date than Featured From date!");
             }
             else {
                 $('.classFeaturedFrom').removeClass('error');
                 $('.classFeaturedTo').removeClass('error');
                 $('.classFeaturedFrom').parent('div').removeClass('diverror');
                 $('.classFeaturedTo').parent('div').removeClass('diverror');
                 validFeaturedDate = true;
             }

             if ($('.SpecialDropDown').val() == 9) {
                 validSpecialDate = ItemMangement.ValidateExtraField("classSpecialFrom", "classSpecialTo", "date", "Special To date must be higher date than Special From date!");
             }
             else {
                 $('.classSpecialFrom').removeClass('error');
                 $('.classSpecialTo').removeClass('error');
                 $('.classSpecialFrom').parent('div').removeClass('diverror');
                 $('.classSpecialFrom').parent('div').removeClass('diverror');
                 validSpecialDate = true;
             }
             if (checkForm(frm) && ItemMangement.CheckUniqueness(itemSKU, itemId) && validPrice && validNewDate && validActiveDate && validFeaturedDate && validSpecialDate) {
                 ItemMangement.SaveItem("#" + frmID, attributeSetId, itemTypeId, itemId);
             }
             else {
                 var errorAccr = $("#accordion").find('.diverror:first').parents('div').prev('.accordionHeading').html();
                 //alert($("#accordion").find('.diverror').parents('table').prev('.accordionHeading').html());
                 //alert(errorAccr);
                 var accrHeading = $("#accordion").find('.accordionHeading');

                 //alert(accrHeading.length);
                 $.each(accrHeading, function(i, item) {
                     //alert($(item).html() + '::' + errorAccr);
                     if ($(item).html() == errorAccr) {
                         $("#accordion").accordion("option", "active", i);
                     }
                 });
                 return false;
             }
         },

         FillForm: function(response) {
             $.each(response.d, function(index, item) {
                 //General properties Tab
                 $('#txtCostVariantName').val(item.CostVariantName);
                 $('#txtCostVariantName').attr('disabled', 'disabled');
                 $('#ddlAttributeType').val(item.InputTypeID);
                 $('#ddlAttributeType').attr('disabled', 'disabled');
                 $('#txtDisplayOrder').val(item.DisplayOrder);
                 $("#txtDescription").val(item.Description);
                 $("#txtDescription").attr('disabled', 'disabled');
                 $('input[name=chkActive]').attr('checked', item.IsActive);
                 //frontend properties tab  
                 //$('input[name=chkShowInSearch]').attr('checked', item.ShowInSearch);
                 //$('input[name=chkShowInGrid]').attr('checked', item.ShowInGrid);
                 $('input[name=chkUseInAdvancedSearch]').attr('checked', item.ShowInAdvanceSearch);
                 $('input[name=chkComparable]').attr('checked', item.ShowInComparison);
                 $('input[name=chkUseForPriceRule]').attr('checked', item.IsIncludeInPriceRule);
                 //$('input[name=chkUseForPromoRule]').attr('checked', item.IsIncludeInPromotions);
                 //$('input[name=chkIsEnableSorting]').attr('checked', item.IsEnableSorting);
                 //$('input[name=chkIsUseInFilter]').attr('checked', item.IsUseInFilter);
                 //$('input[name=chkUseForRating]').attr('checked', item.IsShownInRating);
             });
         },

         BindItemCostVariantValueByCostVariantID: function(itemCostVariantId, itemId, costVariantId) {

             this.config.url = this.config.baseURL + "GetItemCostVariantValuesByCostVariantID";
             this.config.data = JSON2.stringify({ itemCostVariantId: itemCostVariantId, itemId: itemId, costVariantID: costVariantId, storeID: storeId, portalID: portalId, cultureName: cultureName });
             this.config.ajaxCallMode = 8;
             this.ajaxCall(this.config);
         },
         DeleteItemCostVariant: function(tblID, argus) {
             switch (tblID) {
                 case "gdvItemCostVariantGrid":
                     ItemMangement.DeleteItemCostVariantByID(argus[0], argus[3], storeId, portalId, userName);
                     break;
                 default:
                     break;
             }
         },

         DeleteItemCostVariantByID: function(_itemCostVariantId, _itemId, _storeId, _portalId, _userName) {
             var properties = {
                 onComplete: function(e) {
                     ItemMangement.ConfirmSingleDeleteItemCostVariant(_itemCostVariantId, _itemId, _storeId, _portalId, _userName, e);
                 }
             };
             // Ask user's confirmation before delete records
             csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this item cost variant option?</p>", properties);
         },

         ConfirmSingleDeleteItemCostVariant: function(itemCostVariantID, itemId, storeId, portalId, userName, event) {
             if (event) {
                 this.config.url = this.config.baseURL + "DeleteSingleItemCostVariant";
                 this.config.data = JSON2.stringify({ itemCostVariantID: itemCostVariantID, itemId: itemId, storeID: storeId, portalID: portalId, userName: userName });
                 this.config.ajaxCallMode = 9;
                 this.ajaxCall(this.config);
             }
             return false;
         },

         BindCostVariantsInputType: function() {
             this.config.url = this.config.baseURL + "GetCostVariantInputTypeList";
             this.config.data = "{}";
             this.config.ajaxCallMode = 10;
             this.ajaxCall(this.config);
         },

         BindInputTypeDropDown: function(item) {
             //$("#ddlAttributeType").get(0).options[$("#ddlAttributeType").get(0).options.length] = new Option(item.InputType, item.InputTypeID);
             $("#ddlAttributeType").append("<option value=" + item.InputTypeID + ">" + item.InputType + "</option>");
         },

         BindCostVariantsOptions: function(itemId) {
             this.config.url = this.config.baseURL + "GetCostVariantsOptionsList";
             this.config.data = JSON2.stringify({ itemId: itemId, storeId: storeId, portalId: portalId, cultureName: cultureName });
             this.config.ajaxCallMode = 11;
             this.ajaxCall(this.config);
         },

         BindCostVariantsDropDown: function(item) {
             //$("#ddlExistingOptions").get(0).options[$("#ddlExistingOptions").get(0).options.length] = new Option(item.InputType, item.InputTypeID);
             $("#ddlExistingOptions").append("<option value=" + item.CostVariantID + ">" + item.CostVariantName + "</option>");
         },

         ClearVariantForm: function() {
             $("#btnSaveItemVariantOption").removeAttr("name");
             $("#btnResetVariantOptions").show();

             $("#txtCostVariantName").val('');
             $('#txtCostVariantName').removeAttr('disabled');
             $("#txtDescription").val('');
             $("#txtDescription").removeAttr('disabled');
             $('#ddlAttributeType').val(1);
             $('#ddlAttributeType').removeAttr('disabled');
             $('#txtDisplayOrder').val('');
             $('input[name=chkActive]').attr('checked', 'checked');
             $('.cssClassPriceModifierType,.cssClassWeightModifierType').val(0);
             $('.cssClassIsActive').val(1);
             $("#lblHeading").html('Add New Cost Variant Option');

             //Next Tab
             //$('input[name=chkShowInSearch]').removeAttr('checked');
             //$('input[name=chkShowInGrid]').removeAttr('checked');
             $('input[name=chkUseInAdvancedSearch]').removeAttr('checked');
             $('input[name=chkComparable]').removeAttr('checked');
             $('input[name=chkUseForPriceRule]').removeAttr('checked');
             //$('input[name=chkUseForPromoRule]').removeAttr('checked');
             //$('input[name=chkIsEnableSorting]').removeAttr('checked');
             //$('input[name=chkIsUseInFilter]').removeAttr('checked');
             //$('input[name=chkUseForRating]').removeAttr('checked');

             //Clear variant tab
             $("#tblVariantTable>tbody").find("tr:gt(0)").remove();
             $("#tblVariantTable>tbody").find("input[type='text']").val('');
             $("#tblVariantTable>tbody").find("select").val(1);
             $("#tblVariantTable>tbody").find("input[type='hidden']").val('0');
             $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassDeleteRow").hide();

             return false;
         },

         OnInit: function() {
             $('#btnResetVariantOptions').hide();
             $("#hdnItemCostVar").val('0');
             $('.cssClassCostVarRight').hide();
             $('.cssClassCostVarError').hide();
             ItemMangement.SelectFirstTab();
         },

         SelectFirstTab: function() {
             var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show'}] });
             $tabs.tabs('select', 0);
         },

         IsUniqueCostVariant: function(costVariantName, costVariantId) {
             this.config.url = this.config.baseURL + "CheckUniqueCostVariantName";
             this.config.data = JSON2.stringify({ costVariantName: costVariantName, costVariantId: costVariantId, storeId: storeId, portalId: portalId });
             this.config.ajaxCallMode = 12;
             this.ajaxCall(this.config);
             return ItemMangement.vars.isUnique;
         },

         SaveItemCostVariantsInfo: function() {
             var variant_id = $('#btnSaveItemVariantOption').attr("name");

             if (variant_id != '') {
                 ItemMangement.SaveItemCostVariant(variant_id, storeId, portalId, userName, cultureName, false);
             }
             else {
                 ItemMangement.SaveItemCostVariant(0, storeId, portalId, userName, cultureName, true);
             }
         },

         SaveItemCostVariant: function(_costVariantId, _storeId, _portalId, _userName, _cultureName, _isNewflag) {
             var validateErrorMessage = '';
             // Validate name
             var costVariantName = $('#txtCostVariantName').val();
             if (!costVariantName) {
                 validateErrorMessage += 'Please enter cost variant name<br/>';
             }
             else if (!ItemMangement.IsUnique(costVariantName, _costVariantId)) {
                 validateErrorMessage += 'Please enter unique cost variant name! "' + costVariantName.trim() + '" already exists.<br/>';
             }

             // Validate cost variant Display Order
             var costVariantDisplayOrder = $("#txtDisplayOrder").val();
             if (!costVariantDisplayOrder) {
                 $("#txtDisplayOrder").focus();
                 validateErrorMessage += 'Please enter cost variant display order<br/>';
             }
             else {
                 var value = costVariantDisplayOrder.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
                 var intRegex = /^\d+$/;
                 if (!intRegex.test(value)) {
                     $("#txtDisplayOrder").focus();
                     validateErrorMessage += 'Cost variant display order must be numeric only.<br/>';
                 }
             }

             if (validateErrorMessage) {
                 validateErrorMessage = 'The following values are required:<br/>' + validateErrorMessage;
                 csscody.alert('<h2>Information Alert</h2><p>' + validateErrorMessage + '</p>');
                 return false;
             }
             else {
                 var _StoreID = _storeId;
                 var _PortalID = _portalId;
                 var _CultureName = _cultureName;
                 var _UserName = _userName;
                 var _itemId = $("#ItemMgt_itemID").val();

                 var _costVariantName = $('#txtCostVariantName').val();
                 var _inputTypeID = $('#ddlAttributeType').val();

                 var selectedCostVariantType = $("#ddlAttributeType :selected").val();

                 var _Description = $('#txtDescription').val();
                 var _DisplayOrder = $('#txtDisplayOrder').val();
                 var _ShowInGrid = false; //$('input[name=chkShowInGrid]').attr('checked');
                 var _ShowInSearch = false; //$('input[name=chkShowInSearch]').attr('checked');
                 var _ShowInAdvanceSearch = false; //$('input[name=chkUseInAdvancedSearch]').attr('checked');
                 var _ShowInComparison = false; //$('input[name=chkComparable]').attr('checked');
                 var _IsEnableSorting = false; //$('input[name=chkIsEnableSorting]').attr('checked');
                 var _IsUseInFilter = false; //$('input[name=chkIsUseInFilter]').attr('checked');
                 var _IsIncludeInPriceRule = false; //$('input[name=chkUseForPriceRule]').attr('checked');
                 var _IsIncludeInPromotions = false; //$('input[name=chkUseForPromoRule]').attr('checked');
                 var _IsShownInRating = false; //$('input[name=chkUseForRating]').attr('checked');
                 var _IsActive = $('input[name=chkActive]').attr('checked');
                 var _IsModified = !(_isNewflag);
                 var _IsNewflag = _isNewflag;

                 var _VariantOptions = '';
                 //if ($('#variantTab').is(':visible')) {
                 $('#tblVariantTable>tbody tr').each(function(index) {
                     _VariantOptions += $(this).find(".cssClassVariantValue").val() + ',';
                     _VariantOptions += $(this).find(".cssClassDisplayOrder").val() + ','; //{required:true,digits:true,minlength:1}
                     _VariantOptions += $(this).find(".cssClassItemCostVariantValueName").val() + ','; //{required:true,minlength:2}
                     if ($(this).find(".cssClassVariantValueName").val() != '' && $(this).find(".cssClassPriceModifier").val() == '') {
                         _VariantOptions += 0.00 + ',';
                     }
                     else {
                         _VariantOptions += $(this).find(".cssClassPriceModifier").val() + ','; //{required:true,number:true,minlength:1}
                     }
                     _VariantOptions += $(this).find(".cssClassPriceModifierType").val() + ',';
                     if ($(this).find(".cssClassVariantValueName").val() != '' && $(this).find(".cssClassWeightModifier").val() == '') {
                         _VariantOptions += 0.00 + ',';
                     }
                     else {
                         _VariantOptions += $(this).find(".cssClassWeightModifier").val() + ','; //{required:true,number:true,minlength:1}
                     }
                     _VariantOptions += $(this).find(".cssClassWeightModifierType").val() + ',';
                     _VariantOptions += $(this).find(".cssClassIsActive").val() + ',';

                     //                        if ($(this).find(".cssClassVariantValueName").val() != '' && $(this).find(".cssClassQuantity").val() == '') {
                     //                            _VariantOptions += 0.00 + ',';
                     //                        }
                     if ($(this).find(".cssClassVariantValueName").val() != '') {
                         _VariantOptions += 0.00 + ',';
                     }
                     else {
                         //_VariantOptions += $(this).find(".cssClassQuantity").val() + ','; //{required:true,number:true,minlength:1}
                         _VariantOptions += 1 + ',';
                     }
                     if (listImages[index] != '') {
                         _VariantOptions += listImages[index] + '%';
                     }
                     else {
                         _VariantOptions += "undefined" + '%';
                     }
                 });
                 //}
                 //TODO:: validation HERE First
                 //            // Validate cost variant options 
                 //            var costVariantDisplayOrder = $("#txtDisplayOrder").val();
                 //            if (!costVariantDisplayOrder) {
                 //                $("#txtDisplayOrder").focus();
                 //                validateErrorMessage += ' - Please enter cost variant display order<br/>';
                 //            }
                 //            else {
                 //                var value = costVariantDisplayOrder.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
                 //                var intRegex = /^\d+$/;
                 //                if (!intRegex.test(value)) {
                 //                    $("#txtDisplayOrder").focus();
                 //                    validateErrorMessage += ' - Cost variant display order must be numeric only.<br/>';
                 //                }
                 //            }

                 ItemMangement.AddItemCostVariantInfo(_costVariantId, _costVariantName, _Description, _CultureName, _itemId, _inputTypeID, _DisplayOrder, _ShowInGrid, _ShowInSearch,
            _ShowInAdvanceSearch, _ShowInComparison, _IsEnableSorting, _IsUseInFilter, _IsIncludeInPriceRule, _IsIncludeInPromotions, _IsShownInRating,
            _StoreID, _PortalID, _IsActive, _IsModified, _UserName, _VariantOptions, _IsNewflag);

             }
             return false;
         },

         AddItemCostVariantInfo: function(_costVariantId, _costVariantName, _Description, _CultureName, _itemId, _inputTypeID, _DisplayOrder, _ShowInGrid, _ShowInSearch,
            _ShowInAdvanceSearch, _ShowInComparison, _IsEnableSorting, _IsUseInFilter, _IsIncludeInPriceRule, _IsIncludeInPromotions, _IsShownInRating,
            _StoreID, _PortalID, _IsActive, _IsModified, _UserName, _VariantOptions, _IsNewflag) {

             var params = { costVariantID: _costVariantId, costVariantName: _costVariantName, description: _Description, cultureName: _CultureName, itemId: _itemId, inputTypeID: _inputTypeID,
                 displayOrder: _DisplayOrder, showInGrid: _ShowInGrid, showInSearch: _ShowInSearch, showInAdvanceSearch: _ShowInAdvanceSearch,
                 showInComparison: _ShowInComparison, isEnableSorting: _IsEnableSorting, isUseInFilter: _IsUseInFilter, isIncludeInPriceRule: _IsIncludeInPriceRule,
                 isIncludeInPromotions: _IsIncludeInPromotions, isShownInRating: _IsShownInRating,
                 storeId: _StoreID, portalId: _PortalID, isActive: _IsActive, isModified: _IsModified, userName: _UserName, variantOptions: _VariantOptions,
                 isNewflag: _IsNewflag
             };
             this.config.url = this.config.baseURL + "SaveAndUpdateItemCostVariant";
             this.config.data = JSON2.stringify(params);
             this.config.ajaxCallMode = 13;
             this.vars.itemId = _itemId;
             this.ajaxCall(this.config);
         },

         ClickToDeleteImage: function(objImg) {
             $(objImg).closest('span').html('');
             return false;
         },

         ConfirmDeleteMultiple: function(item_ids, event) {
             if (event) {
                 //AspxCommerce.Busy.LoadingShow();
                 ItemMangement.DeleteMultipleItems(item_ids, storeId, portalId, userName);
             }
             return false;
         },

         DeleteMultipleItems: function(_itemIds, _storeId, _portalId, _userName) {
             //Pass the selected attribute id and other parameters
             this.config.url = this.config.baseURL + "DeleteMultipleItemsByItemID";
             this.config.data = JSON2.stringify({ itemIds: _itemIds, storeId: _storeId, portalId: _portalId, userName: _userName });
             this.config.ajaxCallMode = 14;
             this.ajaxCall(this.config);
         },

         BindItemsGrid: function(sku, Nm, itemType, attributeSetNm, visibility, isAct) {
             this.config.method = "GetItemsList";
             this.config.data = { sku: sku, name: Nm, itemType: itemType, attributesetName: attributeSetNm, visibility: visibility, isActive: isAct, storeId: storeId, portalId: portalId, userName: userName, cultureName: cultureName };
             var data = this.config.data;
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvItems_pagesize").length > 0) ? $("#gdvItems_pagesize :selected").text() : 10;

             $("#gdvItems").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                 { display: 'ItemID', name: 'id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'itemsChkbox', elemDefault: false, controlclass: 'classClassCheckBox' },
                 { display: 'Item ID', name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'SKU', name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Name', name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'ItemType ID', name: 'itemtype_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Type', name: 'item_type', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'AttributeSet ID', name: 'attributeset_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Attribute Set Name', name: 'attribute_set_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Price', name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                 { display: 'List Price', name: 'listprice', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                 { display: 'Quantity', name: 'qty', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Visibility', name: 'visibility', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Is Active?', name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Added On', name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                 { display: 'IDTobeChecked', name: 'id_to_check', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                 { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
         		],
                 buttons: [
                 { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'ItemMangement.EditItems', arguments: '4,6' },
                 { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'ItemMangement.DeleteItems', arguments: '' },
                 { display: 'Activate', name: 'active', enable: true, _event: 'click', trigger: '4', callMethod: 'ItemMangement.ActiveItems', arguments: '' },
                 { display: 'Deactivate', name: 'deactive', enable: true, _event: 'click', trigger: '5', callMethod: 'ItemMangement.DeactiveItems', arguments: '' }
         		],
                 rp: perpage,
                 nomsg: "No Records Found!",
                 param: data,
                 current: current_,
                 pnew: offset_,
                 sortcol: { 0: { sorter: false }, 14: { sorter: false }, 15: { sorter: false} }
             });
             $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
         },

         EditItems: function(tblID, argus) {
             $(window).scrollTop(0);
             AspxCommerce.Busy.LoadingShow();
             $('#Todatevalidation').attr('class', '');
             $('#Fromdatevalidation').attr('class', '');
             //alert(argus);
             switch (tblID) {
                 case "gdvItems":
                     ItemMangement.ContinueForm(true, argus[4], argus[3], argus[0]);
                     $("#ItemMgt_itemID").val(argus[0]);
                     ItemMangement.BindCostVariantsOptions(argus[0]);
                     AspxCommerce.Busy.LoadingHide();
                     break;
                 default:
                     break;
             }
         },

         ClickToDelete: function(itemId) {
             ItemMangement.DeleteItemByID(itemId, storeId, portalId, userName);
         },

         DeleteItems: function(tblID, argus) {
             switch (tblID) {
                 case "gdvItems":
                     AspxCommerce.CheckSessionActive();
                     if (AspxCommerce.vars.IsAlive) {
                         ItemMangement.DeleteItemByID(argus[0], storeId, portalId, userName);
                     }
                     else {
                         window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + 'Login.aspx';
                     }
                     break;
                 default:
                     break;
             }
         },

         DeleteItemByID: function(_itemId, _storeId, _portalId, _userName) {
             var properties = { onComplete: function(e) {
                     ItemMangement.ConfirmSingleDelete(_itemId, _storeId, _portalId, _userName, e);
                 }
             }
             // Ask user's confirmation before delete records        
             csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this item?</p>", properties);
         },

         ConfirmSingleDelete: function(item_id, _storeId, _portalId, _userName, event) {
             if (event) {
                 // AspxCommerce.Busy.LoadingShow();
                 ItemMangement.DeleteSingleItem(item_id, _storeId, _portalId, _userName);
             }
             return false;
         },

         DeleteSingleItem: function(_itemId, _storeId, _portalId, _userName) {
             //Pass the selected attribute id and other parameters
             this.config.url = this.config.baseURL + "DeleteItemByItemID";
             this.config.data = JSON2.stringify({ itemId: parseInt(_itemId), storeId: _storeId, portalId: _portalId, userName: _userName });
             this.config.ajaxCallMode = 15;
             this.ajaxCall(this.config);
         },

         ActiveItems: function(tblID, argus) {
             switch (tblID) {
                 case "gdvItems":
                     AspxCommerce.Busy.LoadingShow();
                     ItemMangement.ActivateItemID(argus[0], storeId, portalId, userName, true);
                     break;
                 default:
                     break;
             }
         },

         DeactiveItems: function(tblID, argus) {
             switch (tblID) {
                 case "gdvItems":
                     AspxCommerce.Busy.LoadingShow();
                     ItemMangement.DeActivateItemID(argus[0], storeId, portalId, userName, false);
                     break;
                 default:
                     break;
             }
         },

         DeActivateItemID: function(_itemId, _storeId, _portalId, _userName, _isActive) {
             //Pass the selected attribute id and other parameters
             AspxCommerce.CheckSessionActive();
             if (AspxCommerce.vars.IsAlive) {
                 this.config.url = this.config.baseURL + "UpdateItemIsActiveByItemID";
                 this.config.data = JSON2.stringify({ itemId: parseInt(_itemId), storeId: _storeId, portalId: _portalId, userName: _userName, isActive: _isActive });
                 this.config.ajaxCallMode = 16;
                 this.ajaxCall(this.config);
                 return false;
             }
             else {
                 window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + 'Login.aspx';
             }
         },

         ActivateItemID: function(_itemId, _storeId, _portalId, _userName, _isActive) {
             //Pass the selected attribute id and other parameters
             AspxCommerce.CheckSessionActive();
             if (AspxCommerce.vars.IsAlive) {
                 this.config.url = this.config.baseURL + "UpdateItemIsActiveByItemID";
                 this.config.data = JSON2.stringify({ itemId: parseInt(_itemId), storeId: _storeId, portalId: _portalId, userName: _userName, isActive: _isActive });
                 this.config.ajaxCallMode = 17;
                 this.ajaxCall(this.config);
                 return false;
             }
             else {
                 window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + 'Login.aspx';
             }
         },

         BindRelatedItemsGrid: function(selfItemID) {
             this.config.method = "GetRelatedItemsList";
             this.config.data = { storeId: storeId, portalId: portalId, selfItemId: selfItemID, userName: userName, culture: cultureName };
             var data = this.config.data;
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvRelatedItems_pagesize").length > 0) ? $("#gdvRelatedItems_pagesize :selected").text() : 10;

             $("#gdvRelatedItems").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                 { display: 'ItemID', name: 'id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'chkRelatedControls', controlclass: 'classClassCheckBox', checkedItems: '14' },
                 { display: 'Item ID', name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'SKU', name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Name', name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'ItemType ID', name: 'itemtype_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Type', name: 'item_type', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'AttributeSet ID', name: 'attributeset_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Attribute Set Name', name: 'attribute_set_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Price', name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                 { display: 'List Price', name: 'listprice', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                 { display: 'Quantity', name: 'qty', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Visibility', name: 'visibility', cssclass: 'cssClassHeadBoolean', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Is Active?', name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Added On', name: 'AddedOn', cssclass: 'cssClassHeadDate', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                 { display: 'IDTobeChecked', name: 'id_to_check', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' }
         		],
                 rp: perpage,
                 nomsg: "No Records Found!",
                 param: data,
                 current: current_,
                 pnew: offset_,
                 sortcol: { 0: { sorter: false }, 14: { sorter: false} }
             });
         },

         BindUpSellItemsGrid: function(selfItemID) {
             this.config.method = "GetUpSellItemsList";
             this.config.data = { storeId: storeId, portalId: portalId, selfItemId: selfItemID, userName: userName, culture: cultureName };
             var data = this.config.data;
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvUpSellItems_pagesize").length > 0) ? $("#gdvUpSellItems_pagesize :selected").text() : 10;

             $("#gdvUpSellItems").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                 { display: 'ItemID', name: 'id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'chkUpSellControls', controlclass: 'classClassCheckBox', checkedItems: '14' },
                 { display: 'Item ID', name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'SKU', name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Name', name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'ItemType ID', name: 'itemtype_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Type', name: 'item_type', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'AttributeSet ID', name: 'attributeset_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Attribute Set Name', name: 'attribute_set_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Price', name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                 { display: 'List Price', name: 'listprice', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                 { display: 'Quantity', name: 'qty', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Visibility', name: 'visibility', cssclass: 'cssClassHeadBoolean', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Is Active?', name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Added On', name: 'AddedOn', cssclass: 'cssClassHeadDate', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                 { display: 'IDTobeChecked', name: 'id_to_check', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' }
         		],
                 rp: perpage,
                 nomsg: "No Records Found!",
                 param: data,
                 current: current_,
                 pnew: offset_,
                 sortcol: { 0: { sorter: false }, 14: { sorter: false} }
             });
         },

         BindCrossSellItemsGrid: function(selfItemID) {
             this.config.method = "GetCrossSellItemsList";
             this.config.data = { storeId: storeId, portalId: portalId, selfItemId: selfItemID, userName: userName, culture: cultureName };
             var data = this.config.data;
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvCrossSellItems_pagesize").length > 0) ? $("#gdvCrossSellItems_pagesize :selected").text() : 10;

             $("#gdvCrossSellItems").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                 { display: 'ItemID', name: 'id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'chkCrossSellControls', controlclass: 'classClassCheckBox', checkedItems: '14' },
                 { display: 'Item ID', name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'SKU', name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Name', name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'ItemType ID', name: 'itemtype_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Type', name: 'item_type', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'AttributeSet ID', name: 'attributeset_id', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Attribute Set Name', name: 'attribute_set_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Price', name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                 { display: 'List Price', name: 'listprice', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                 { display: 'Quantity', name: 'qty', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Visibility', name: 'visibility', cssclass: 'cssClassHeadBoolean', hide: true, controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Is Active?', name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                 { display: 'Added On', name: 'AddedOn', cssclass: 'cssClassHeadDate', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                 { display: 'IDTobeChecked', name: 'id_to_check', cssclass: 'cssClassHeadNumber', hide: true, controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' }
         		],
                 rp: perpage,
                 nomsg: "No Records Found!",
                 param: data,
                 current: current_,
                 pnew: offset_,
                 sortcol: { 0: { sorter: false }, 14: { sorter: false} }
             });
         },

         BindAttributeSet: function() {
             this.config.url = this.config.baseURL + "GetAttributeSetList";
             this.config.data = JSON2.stringify({ storeId: storeId, portalId: portalId });
             this.config.ajaxCallMode = 18;
             this.ajaxCall(this.config);
         },

         BindItemType: function() {
             this.config.url = this.config.baseURL + "GetAttributesItemTypeList";
             this.config.data = JSON2.stringify({ storeId: storeId, portalId: portalId });
             this.config.ajaxCallMode = 19;
             this.ajaxCall(this.config);
         },

         ClearForm: function() {
             $('#ddlAttributeSet').val('2');
             $('#ddlItemType').val('1');
         },

         ContinueForm: function(showDeleteBtn, attributeSetId, itemTypeId, itemId) {
             ItemMangement.ResetHTMLEditors();
             ItemMangement.GetFormFieldList(attributeSetId, itemTypeId, showDeleteBtn, itemId);
         },

         FillItemAttributes: function(itemId, item) {
             var attNameNoSpace = "_" + item.AttributeName.replace(new RegExp(" ", "g"), '-');
             var id = item.AttributeID + '_' + item.InputTypeID + '_' + item.ValidationTypeID + '_' + item.IsRequired + '_' + item.GroupID
        + '_' + item.IsIncludeInPriceRule + '_' + item.IsIncludeInPromotions + '_' + item.DisplayOrder;

             var val = '';
             //alert(htmlEditorIDs.length + '::' + editorList.length);
             switch (item.InputTypeID) {
                 case 1: //TextField
                     if (item.ValidationTypeID == 3) {
                         $("#" + id).val(item.DecimalValue);
                         break;
                     }
                     else if (item.ValidationTypeID == 5) {
                         $("#" + id).val(item.IntValue);
                         break;
                     }
                     else {
                         //alert(item.NvarcharValue);
                         $("#" + id).val(unescape(item.NvarcharValue));
                         break;
                     }
                     //$("#" + id).removeClass('hint');
                 case 2: //TextArea
                     $("#" + id).val(item.TextValue);
                     //alert(item.TextValue + '::' + editorList.length);
                     //$("#" + id).removeClass('hint');
                     for (var i = 0; i < editorList.length; i++) {
                         if (editorList[i].ID == id + "_editor") {
                             editorList[i].Editor.setData(Encoder.htmlDecode(item.TextValue));
                         }
                     }
                     break;
                 case 3: //Date
                     $("#" + id).val(formatDate(new Date(item.DateValue), "yyyy/MM/dd"));
                     //$("#" + id).val(formatDate(new Date(DateDeserialize(item.DateValue)), "yyyy/MM/dd"));
                     //$("#" + id).removeClass('hint');
                     break;
                 case 4: //Boolean
                     if (item.BooleanValue.toLowerCase() == "true") {
                         $("#" + id).attr("checked", "checked");
                     }
                     else if (item.BooleanValue.toLowerCase() == "false") {
                         $("#" + id).removeAttr("checked");
                     }
                     break;
                 case 5: //MultipleSelect
                     $("#" + id).val('');
                     val = item.OptionValues;
                     vals = val.split(',');
                     $.each(vals, function(i) {
                         $("#" + id + " option[value=" + vals[i] + "]").attr("selected", "selected");
                     });
                     break;
                 case 6: //DropDown
                     $("#" + id).val('');
                     val = item.OptionValues;
                     vals = val.split(',');
                     $.each(vals, function(i) {
                         $("#" + id + " option[value=" + vals[i] + "]").attr("selected", "selected");
                     });
                     break;
                 case 7: //Price
                     $("#" + id).val(item.DecimalValue);
                     //$("#" + id).removeClass('hint');
                     break;
                 case 8: //File
                     //alert(item.FileValue);
                     var d = $("#" + id).parent();
                     var filePath = item.FileValue;
                     var fileName = filePath.substring(filePath.lastIndexOf("/") + 1);
                     if (filePath != "") {
                         var fileExt = (-1 !== filePath.indexOf('.')) ? filePath.replace(/.*[.]/, '') : '';
                         myregexp = new RegExp("(jpg|jpeg|jpe|gif|bmp|png|ico)", "i");
                         if (myregexp.test(fileExt)) {
                             $(d).find('span.response').html('<div class="cssClassLeft"><img src="' + aspxRootPath + filePath + '" class="uploadImage" /></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="ItemMangement.ClickToDeleteImage(this)" alt="Delete" title="Delete"/></div>');
                             //alert($(d).find('span.response').html());
                         }
                         else {
                             $(d).find('span.response').html('<div class="cssClassLeft"><a href="' + aspxRootPath + filePath + '" class="uploadFile" target="_blank">' + fileName + '</a></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="ItemMangement.ClickToDeleteImage(this)" alt="Delete" title="Delete"/></div>');
                         }
                         $(d).find('input[type="hidden"]').val(filePath);
                     }
                     break;
                 case 9: //Radio
                     if (item.OptionValues == "") {
                         $("#" + id).removeAttr("checked");
                     }
                     else {
                         $("#" + id).attr("checked", "checked");
                     }
                     break;
                 case 10: //RadioButtonList
                     $("input[value=" + item.OptionValues + "]:radio").attr("checked", "checked");
                     break;
                 case 11: //CheckBox
                     if (item.OptionValues == "") {
                         $("#" + id).removeAttr("checked");
                     }
                     else {
                         $("#" + id).attr("checked", "checked");
                     }
                     break;
                 case 12: //CheckBoxList
                     var inputs = $("input[name=" + id + "]");
                     $.each(inputs, function(i) {
                         $(this).removeAttr("checked");
                     });
                     val = item.OptionValues;
                     vals = val.split(',');
                     $.each(vals, function(i) {
                         $("input[value=" + vals[i] + "]").attr("checked", "checked");
                     });
                     break;
                 case 13: //Password  
                     $("#" + id).val(item.NvarcharValue);
                     //$("#" + id).removeClass('hint');  
                     break;
             }
         },

         DateDeserialize: function(dateStr) {
             //return eval(dateStr.replace(/\//g, ' '));
             return dateStr.replace(new RegExp("\/", "g"), ' ');
         },

         GetFormFieldList: function(attributeSetId, itemTypeId, showDeleteBtn, itemId) {
             this.config.url = this.config.baseURL + "GetItemFormAttributes";
             this.config.data = JSON2.stringify({ attributeSetID: attributeSetId, itemTypeID: itemTypeId, storeID: storeId, portalID: portalId, userName: userName, culture: cultureName });
             this.vars.attributeSetId = attributeSetId;
             this.vars.itemTypeId = itemTypeId;
             this.vars.showDeleteBtn = showDeleteBtn;
             this.vars.itemId = itemId;
             this.config.ajaxCallMode = 20;
             this.ajaxCall(this.config);
         },

         BindDataInImageTab: function(itemId) {
             //alert(itemId);
             if (itemId > 0) {
                 //  alert("Reached");
                 this.config.url = this.config.baseURL + "GetImageContents";
                 this.config.data = JSON2.stringify({ itemID: itemId });
                 this.config.ajaxCallMode = 21;
                 this.ajaxCall(this.config);
             }
         },

         BindToTable: function(msg) {
             //ItemMangement.RemoveHtml();
             //ItemMangement.CreateHtml();
             ItemMangement.CreateTableHeader();
             //rowCount = msg.d.length;
             $.each(msg.d, function(index, item) {
                 // create table elements
                 rowCount = index;
                 var j = rowCount + 1;
                 var newRowImage = '';
                 newRowImage += '<tr class="classRowData' + j + '">';
                 newRowImage += '<td><img src="' + aspxRootPath + item.ImagePath.replace('uploads', 'uploads/Small') + '" class="uploadImage"/></td>';
                 newRowImage += '<td><div class="field required"><input type="textbox" class="cssClassNormalTextBox cssClassImageDiscription" maxlength="256" value="' + item.AlternateText + '"/><span class="iferror"></span></div></td>';
                 newRowImage += '<td><div class="field required"><input type="textbox" class="cssClassDisplayOrder" maxlength="3" value="' + item.DisplayOrder + '"/><span class="iferror">Integer Number</span></div></td>';
                 newRowImage += '<td><input type="radio" name="itemimage_' + j + '" value="Base Image" class="notTest" /></td>';
                 newRowImage += '<td><input type="radio" name="itemimage_' + j + '" value="Small Image" class="notTest" /></td>';
                 newRowImage += '<td><input type="radio" name="itemimage_' + j + '"  value="ThumbNail" class="notTest" /></td>';
                 newRowImage += '<td><input type="checkbox" class="notTest" id="chkIsActive_' + j + '" /></td>';
                 newRowImage += '<td><img class="imgDelete" src="' + aspxTemplateFolderPath + '/images/admin/btndelete.png" id="btn' + j + '" onclick="ItemMangement.DeleteImage(this)" /></td>';
                 newRowImage += '</tr>';
                 $("#multipleUpload .classTableWrapper > tbody").append(newRowImage);
                 $(".cssClassDisplayOrder").bind("contextmenu", function(e) {
                     return false;
                 });
                 $('.cssClassDisplayOrder').bind('paste', function(e) {
                     e.preventDefault();
                 });
                 if (item.IsActive) {
                     $('#chkIsActive_' + j + '').attr('checked', item.IsActive);
                 }

                 // alert("Image type is " + item.ImageType);
                 if (item.ImageType == "Base Image") {
                     $("tbody>tr.classRowData" + j + ">td:eq(3) input:radio").attr("checked", "checked");
                 }
                 else if (item.ImageType == "Small Image") {
                     $("tbody>tr.classRowData" + j + ">td:eq(4) input:radio").attr("checked", "checked");
                 }
                 else if (item.ImageType == "ThumbNail") {
                     $("tbody>tr.classRowData" + j + ">td:eq(5) input:radio").attr("checked", "checked");
                 }
                 //code to delete row
                 $("img.imgDelete").click(function() {
                     $(this).parent().parent().remove();
                     //  DeleteSelectedItemImage(index,);
                     $("#multipleUpload .classTableWrapper > tbody tr").removeClass("cssClassAlternativeEven");
                     $("#multipleUpload .classTableWrapper > tbody tr").removeClass("cssClassAlternativeOdd");
                     $("#multipleUpload .classTableWrapper > tbody tr:even").addClass("cssClassAlternativeEven");
                     $("#multipleUpload .classTableWrapper > tbody tr:odd").addClass("cssClassAlternativeOdd");
                 });
                 rowCount++;
             });
             $("#multipleUpload .classTableWrapper > tbody tr").removeClass("cssClassAlternativeEven");
             $("#multipleUpload .classTableWrapper > tbody tr").removeClass("cssClassAlternativeOdd");
             $("#multipleUpload .classTableWrapper > tbody tr:even").addClass("cssClassAlternativeEven");
             $("#multipleUpload .classTableWrapper > tbody tr:odd").addClass("cssClassAlternativeOdd");
             $(".cssClassImageDiscription").keypress(function(e) {
                 if (e.which == 35 || e.which == 37) {
                     return false;
                 }
             });
         },

         ImageUploader: function(maxFileSize) {
             var upload = new AjaxUpload($('#fileUpload'), {
                 action: aspxItemModulePath + "MultipleFileUploadHandler.aspx",
                 name: 'myfile[]',
                 multiple: true,
                 data: {},
                 autoSubmit: true,
                 responseType: 'json',
                 onChange: function(file, ext) {
                     //alert('changed');
                 },
                 onSubmit: function(file, ext) {
                     if (ext != "exe") {
                         if (ext && /^(jpg|jpeg|jpe|gif|bmp|png|ico)$/i.test(ext)) {
                             this.setData({
                                 'MaxFileSize': maxFileSize
                             });
                         } else {
                             csscody.alert('<h2>Alert Message</h2><p>Not a valid image type!</p>');
                             return false;
                         }
                     }
                     else {
                         csscody.alert('<h2>Alert Message</h2><p>Not a valid image type!</p>');
                         return false;
                     }
                 },
                 onComplete: function(file, response) {
                     var res = eval(response);
                     if (res.Message != null && res.Status > 0) {
                         ItemMangement.CreateTableHeader();
                         //          CreateTableElements(response);
                         //$('#btnUpload').show();
                         ItemMangement.AddNewImages(res);
                         $("#multipleUpload .classTableWrapper > tbody tr").removeClass("cssClassAlternativeEven");
                         $("#multipleUpload .classTableWrapper > tbody tr").removeClass("cssClassAlternativeOdd");
                         $("#multipleUpload .classTableWrapper > tbody tr:even").addClass("cssClassAlternativeEven");
                         $("#multipleUpload .classTableWrapper > tbody tr:odd").addClass("cssClassAlternativeOdd");
                         $(".cssClassDisplayOrder").keypress(function(e) {
                             if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                                 return false;
                             }
                         });
                         // var code = (e.keyCode ? e.keyCode : e.which);
                         $(".cssClassDescription").keypress(function(e) {
                             if (e.which == 35 || e.which == 37) {
                                 return false;
                             }
                         });

                     }
                     else {
                         csscody.error('<h2>Error Message</h2><p>' + res.Message + '</p>');
                         return false;
                     }
                 }
             });
         },

         AddNewImages: function(response) {
             // create table elements
             var j = rowCount + 1;
             var newRowImage = '';

             newRowImage += '<tr class="classRowData' + j + '">';
             newRowImage += '<td><img src="' + aspxRootPath + response.Message + '" class="uploadImage" height="93px" width="125px"/></td>';
             newRowImage += '<td><div class="field required"><input type="textbox" class="cssClassNormalTextBox cssClassDescription" maxlength="256" /><span class="iferror"></span></div></td>';
             newRowImage += '<td><div class="field required"><input type="textbox" class="cssClassDisplayOrder" maxlength="3" /><span class="iferror">Integer Number</span></div></td>';
             newRowImage += '<td><input type="radio" name="itemimage_' + j + '" value="Base Image" class="notTest" /></td>';
             newRowImage += '<td><input type="radio" name="itemimage_' + j + '" value="Small Image" class="notTest" /></td>';
             newRowImage += '<td><input type="radio" name="itemimage_' + j + '"  value="ThumbNail" class="notTest" checked="checked" /></td>';
             newRowImage += '<td><input type="checkbox" class="notTest" checked="checked"/></td>';
             newRowImage += '<td><img class="imgDelete" src="' + aspxTemplateFolderPath + '/images/admin/btndelete.png"  id="btn' + j + '" onclick="ItemMangement.DeleteImage(this)" /></td>';
             newRowImage += '</tr>';
             $("#multipleUpload .classTableWrapper > tbody").append(newRowImage);
             if (j == 1) {
                 $('input[value="Base Image"]').attr("checked", "checked");
             }
             rowCount++;
             $(".cssClassDescription").keypress(function(e) {
                 if (e.which == 35 || e.which == 37) {
                     return false;
                 }
             });
             $(".cssClassDisplayOrder").bind("contextmenu", function(e) {
                 return false;
             });
             $('.cssClassDisplayOrder').bind('paste', function(e) {
                 e.preventDefault();
             });

         },

         DeleteImage: function(onjImg) { //code to delete row                
             $(onjImg).parent().parent().remove();
             $("#multipleUpload .classTableWrapper > tbody tr").removeClass("cssClassAlternativeEven");
             $("#multipleUpload .classTableWrapper > tbody tr").removeClass("cssClassAlternativeOdd");
             $("#multipleUpload .classTableWrapper > tbody tr:even").addClass("cssClassAlternativeEven");
             $("#multipleUpload .classTableWrapper > tbody tr:odd").addClass("cssClassAlternativeOdd");
         },

         CreateForm: function(itemFormFields, attributeSetId, itemTypeId, showDeleteBtn, itemId) {
             var strDynRow = '';
             var attGroup = new Array();
             attGroup.length = 0;
             $.each(itemFormFields, function(index, item) {
                 var isGroupExist = false;
                 for (var i = 0; i < attGroup.length; i++) {
                     if (attGroup[i].key == item.GroupID) {
                         isGroupExist = true;
                         break;
                     }
                 }
                 if (!isGroupExist) {
                     attGroup.push({ key: item.GroupID, value: item.GroupName, html: '' });
                 }
             });
             FileUploaderIDs = new Array();
             $.each(itemFormFields, function(index, item) {
                 strDynRow = ItemMangement.createRow(itemId, itemTypeId, item.AttributeID, item.AttributeName, item.InputTypeID, item.InputTypeValues != "" ? eval(item.InputTypeValues) : '', item.DefaultValue, item.ToolTip, item.Length, item.ValidationTypeID, item.IsEnableEditor, item.IsUnique, item.IsRequired, item.GroupID, item.IsIncludeInPriceRule, item.IsIncludeInPromotions, item.DisplayOrder);
                 //strDynRow = '<table width="100%" border="0" cellpadding="0" cellspacing="0">' + strDynRow + '</table>';
                 for (var i = 0; i < attGroup.length; i++) {
                     if (attGroup[i].key == item.GroupID) {
                         attGroup[i].html += strDynRow;
                     }
                 }
             });

             ItemMangement.CreateAccordion(attGroup, attributeSetId, itemTypeId, showDeleteBtn);
             $("#newCostvariants").hide();
             ItemMangement.BindTaxManageRule();
             //Functions for static Tree and Grid Binding

             ItemMangement.CreateCategoryMultiSelect(itemId);
             ItemMangement.BindRelatedItemsGrid(itemId);
             ItemMangement.BindUpSellItemsGrid(itemId);
             ItemMangement.BindCrossSellItemsGrid(itemId);
             //               $(".cssClassSKU").keyup(function(){
             //                    var text=$(this).val();
             //                    $(this).val(text.replace(/[^\w\d\s]/,""));
             //                });
             $('.cssClassSKU').keyup(function() {
                 if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                     this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, ''); // block all kinds of specials characters
                 }
                 this.value = this.value.replace(/\s/g, '').replace(' ', ''); // remove available space
             });

             //Hide all blur Error and Success divs
             $('.cssClassRight').hide();
             $('.cssClassError').hide();
             $('.cssClassError').html('');
             ItemMangement.BindPopUP();
             ItemMangement.BindTierPriceCommand();
             $(".classItemPrice,.classItemListPrice,.verifyDecimal,.verifyInteger").DigitAndDecimal('.classItemListPrice,.classItemPrice,.verifyDecimal,.verifyInteger ', '');
             $(".classItemPrice,.classItemListPrice,.verifyDecimal,.verifyInteger,.hasDatepicker").bind("contextmenu", function(e) {
                 return false;
             });
             $('.classItemPrice,.classItemListPrice,.verifyDecimal,.verifyInteger,.hasDatepicker').bind('paste', function(e) {
                 e.preventDefault();
             });
             $(".SpecialDropDown").bind("change", function() {
                 if ($(this).val() == 10) {
                     $('.classSpecialFrom').removeClass('error');
                     $('.classSpecialTo').removeClass('error');
                     $('.classSpecialFrom').parent('div').removeClass('diverror');
                     $('.classSpecialFrom').parent('div').removeClass('diverror');
                     $('.classSpecialTo').next('span').html('');
                 }
             });
             $(".FeaturedDropDown").bind("change", function() {
                 if ($(this).val() == 8) {
                     $('.classFeaturedFrom').removeClass('error');
                     $('.classFeaturedTo').removeClass('error');
                     $('.classFeaturedFrom').parent('div').removeClass('diverror');
                     $('.classFeaturedFrom').parent('div').removeClass('diverror');
                     $('.classFeaturedTo').next('span').html('');
                 }
             });
         },

         BindDownloadableForm: function(itemId) {
             this.config.url = this.config.baseURL + "GetDownloadableItem";
             this.config.data = JSON2.stringify({ storeId: storeId, portalId: portalId, cultureName: cultureName, userName: userName, itemID: itemId });
             this.config.ajaxCallMode = 22;
             this.ajaxCall(this.config);
         },

         FillDownlodableItemForm: function(response) {
             $.each(response.d, function(index, msg) {
                 $("#txtDownloadTitle").val(msg.Title);
                 if (msg.MaxDownload == 0) {
                     $("#txtMaxDownload").val('');
                 }
                 else {
                     $("#txtMaxDownload").val(msg.MaxDownload);
                 }
                 //$('input[name=chkIsSharable]').attr('checked', msg.IsSharable);
                 $("#fileSample").attr("title", msg.SampleFile);
                 if (msg.SampleFile == '') {
                     $("#spanSample").html("");
                 }
                 else
                     $("#spanSample").html("Previous: ");
                 $("#spanSample").append(msg.SampleFile);
                 $("#fileActual").attr("title", msg.ActualFile);
                 if (msg.ActualFile == '') {
                     $("#spanActual").html("");
                 }
                 else
                     $("#spanActual").html("Previous: ");
                 $("#spanActual").append(msg.ActualFile);
                 if (msg.DisplayOrder == 0) {
                     $("#txtDownDisplayOrder").val('');
                 } else {
                     $("#txtDownDisplayOrder").val(msg.DisplayOrder);
                 }
                 $("#btnSave").attr("name", msg.DownloadableID);
             });
         },

         BindPopUP: function() {
             $('#btnAddExistingOption').live('click', function() {
                 var item_Id = $("#ItemMgt_itemID").val();
                 ItemMangement.BindCostVariantsOptions(item_Id);
                 $("#variantsGrid,#divNewVariant").hide();
                 $("#newCostvariants,#divExistingVariant").show();
             });
             $('#btnAddNewOption').live('click', function() {
                 $('.cssClassPriceModifierType option[value*=false]').each(function() {
                     $(this).val($(this).val().replace('false', '0'));
                 });
                 $('.cssClassPriceModifierType option[value*=true]').each(function() {
                     $(this).val($(this).val().replace('true', '1'));
                 });
                 $('.cssClassWeightModifierType option[value*=false]').each(function() {
                     $(this).val($(this).val().replace('false', '0'));
                 });
                 $('.cssClassWeightModifierType option[value*=true]').each(function() {
                     $(this).val($(this).val().replace('true', '1'));
                 });
                 $('.cssClassIsActive option[value*=false]').each(function() {
                     $(this).val($(this).val().replace('false', '0'));
                 });
                 $('.cssClassIsActive option[value*=true]').each(function() {
                     $(this).val($(this).val().replace('true', '1'));
                 });
                 ItemMangement.OnInit();
                 ItemMangement.ClearVariantForm();
                 $('#ddlAttributeType').html('');
                 ItemMangement.BindCostVariantsInputType();
                 $("#tabFrontDisplay").show();
                 $("#variantsGrid,#divExistingVariant").hide();
                 $("#newCostvariants,#divNewVariant").show();
                 $("#VariantsImagesTable>tbody").html('');
                 listImages = new Array();
             });
             $('.classAddImages').live('click', function() {
                 var len = $("#VariantsImagesTable tbody tr").length - 1;
                 if (len == -1) {
                     $("#VariantsImagesTable").hide();
                     $('#btnSaveImages').hide();
                     $('#btnImageBack').hide();
                 } else {
                     $("#VariantsImagesTable>tbody").html('');
                     $("#VariantsImagesTable").show();
                     $('#btnSaveImages').show();
                     $('#btnImageBack').show();
                 }
                 var value = $(this).val();
                 $("#btnSaveImages").attr("value", value);
                 //$("#btnSaveImages").show();
                 $("#imageUploader").show();
                 ShowPopupControl('popuprel2');
                 ItemMangement.CostVariantsImageUploader(maxFileSize);
                 var subStr = listImages[value].split('@');
                 var List = '';
                 $.each(subStr, function(index) {
                     List += '<tr>';
                     List += '<td><img src="' + aspxRootPath + subStr[index] + '" class="uploadImage" height="20px" width="30px"/></td>';
                     List += '<td><img class="imgDelete" src="' + aspxTemplateFolderPath + '/images/admin/btndelete.png" id="btn" onclick="ItemMangement.DeleteImage(this)" /></td>';
                     List += '</tr>';
                 });
                 $("#VariantsImagesTable>tbody").append(List);
                 $("#VariantsImagesTable>tbody tr:even").addClass("cssClassAlternativeEven");
                 $("#VariantsImagesTable>tbody tr:odd").addClass("cssClassAlternativeOdd");
             });
             $('.classAddImagesEdit').live('click', function() {
                 var value = $(this).val();
                 $("#btnSaveImages").attr("value", value);
             });

             $("#btnSaveImages").click(function() {
                 var i = $(this).val();
                 $('#fade, #popuprel2').fadeOut();
                 var list = '';
                 $('#VariantsImagesTable>tbody>tr').each(function() {
                     list += $(this).find("img").attr("src").replace(aspxRootPath, "") + '@';
                 });
                 list = list.substring(0, list.length - 1);
                 listImages[i] = list;
                 $('#tblVariantTable>tbody tr input[type="button"]').find("name").text('');
                 $('#tblVariantTable>tbody tr input[type="button"]').find("name").append(listImages[i]);
                 $("#VariantsImagesTable").hide();
                 $("#btnSaveImages").removeAttr("value");
             });
         },

         BindTierPriceCommand: function() {
             $("#btnSaveQuantityDiscount").bind("click", function() {
                 ItemMangement.SaveItemDiscountQuantity();
             });
         },

         HideAllVariantDivs: function() {
             $("#divExistingVariant").hide();
             $("#divNewVariant").hide();
         },

         BindDataInAccordin: function(itemId, attributeSetId, itemTypeId) {
             //alert(itemId + '::' + attributeSetId + '::' + itemTypeId);
             this.config.url = this.config.baseURL + "GetItemFormAttributesValuesByItemID";
             this.config.data = JSON2.stringify({ itemID: itemId, attributeSetID: attributeSetId, itemTypeID: itemTypeId, storeID: storeId, portalID: portalId, userName: userName, culture: cultureName });
             this.config.ajaxCallMode = 23;
             this.ajaxCall(this.config);
         },

         CreateCategoryMultiSelect: function(itemId) {
             this.config.url = this.config.baseURL + "GetCategoryList";
             this.config.data = JSON2.stringify({ prefix: '---', isActive: true, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName, itemId: itemId });
             this.config.ajaxCallMode = 24;
             this.ajaxCall(this.config);
         },

         FillMultiSelect: function(msg) {
             $('#lstCategories').get(0).options.length = 0;
             $('#lstCategories').attr('multiple', 'multiple');
             $('#lstCategories').attr('size', '5');
             $.each(msg.d, function(index, item) {
                 $("#lstCategories").get(0).options[$("#lstCategories").get(0).options.length] = new Option(item.LevelCategoryName, item.CategoryID);
                 if (item.IsChecked) {
                     $("#lstCategories option[value=" + item.CategoryID + "]").attr("selected", "selected");
                 }
             });
         },

         createRow: function(itemId, itemTypeId, attID, attName, attType, attTypeValue, attDefVal, attToolTip, attLen, attValType, isEditor, isUnique, isRequired, groupId, isIncludeInPriceRule, isIncludeInPromotions, displayOrder) {
             var retString = '';
             //var attNameNoSpace = attName.replace(new RegExp("_", "g"), '%')--> this gives probelm in loading calender
             //var attNameNoSpace = "_" + attName.replace(new RegExp(" ", "g"), '-');
             //searchval.replace(/ /g, '+')
             //date.replace(/\//g, '*'); -->> replace / -->> *
             retString += '<tr><td><label class="cssClassLabel">' + attName + ': </label></td>';
             switch (attType) {
                 case 1: //TextField
                     //alert(attID);
                     if (attID == 4) {
                         $("#hdnSKUTxtBox").val(attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder);
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="cssClassNormalTextBox cssClassSKU dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '" onblur="ItemMangement.CheckUniqueness(this.value, ' + itemId + ' )"/>';
                         retString += '<span class="cssClassRight"><img class="cssClassSuccessImg" height="13" width="18" alt="Right" src="' + aspxTemplateFolderPath + '/images/right.jpg"></span><b class="cssClassError">Ops! found something error, must be unique with no spaces</b>';
                         retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     }
                     else if (attID == 5 && itemTypeId == 2) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="cssClassNormalTextBox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '" readonly="readonly"/>';
                         retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     }
                     else if (attID == 15 && itemTypeId == 2) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="cssClassNormalTextBox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '" readonly="readonly"/>';
                         retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     }
                     else {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="cssClassNormalTextBox cssClassItemName dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '"/>';
                         retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     }

                     break;
                 case 2: //TextArea
                     var editorDiv = '';
                     if (isEditor) {
                         htmlEditorIDs[htmlEditorIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + "_editor";
                         editorDiv = '<div id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '_editor"></div>';
                     }
                     retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><textarea id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" ' + ((isEditor == true) ? ' style="display: none !important;" ' : '') + ' rows="' + attLen + '"  class="cssClassTextArea dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" title="' + attToolTip + ItemMangement.GetValidationTypeErrorMessage(attValType) + '">' + attDefVal + '</textarea>' + editorDiv + '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     //alert(retString);
                     break;
                 case 4: //Boolean
                     retString += '<td class="cssClassTableRightCol"><div class="cssClassCheckBox ' + (isRequired == true ? "required" : "") + '">';
                     if (attDefVal == 1) {
                         retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '" checked="checked"/>';
                     }
                     else {
                         retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/>';
                     }
                     retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 case 3: //Date
                     DatePickerIDs[DatePickerIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder;
                     if (attID == 6) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text"  class="cssClassNormalTextBox dynFormItem classNewFrom ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /field --></p></div></td>';
                     }
                     else if (attID == 7) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text"  class="cssClassNormalTextBox dynFormItem classNewTo ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /field --></p></div></td>';
                     }
                     else if (attID == 22) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text"  class="cssClassNormalTextBox dynFormItem classActiveFrom ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /field --></p></div></td>';
                     }
                     else if (attID == 23) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text"  class="cssClassNormalTextBox dynFormItem classActiveTo ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /field --></p></div></td>';
                     }
                     else if (attID == 30) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text"  class="cssClassNormalTextBox dynFormItem classFeaturedFrom ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /field --></p></div></td>';
                     }
                     else if (attID == 31) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text"  class="cssClassNormalTextBox dynFormItem classFeaturedTo ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /field --></p></div></td>';
                     }
                     else if (attID == 33) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text"  class="cssClassNormalTextBox dynFormItem classSpecialFrom ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /field --></p></div></td>';
                     }
                     else if (attID == 34) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text"  class="cssClassNormalTextBox dynFormItem classSpecialTo ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /field --></p></div></td>';
                     }
                     else {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text"  class="cssClassNormalTextBox dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     }
                     break;
                 case 5: //MultipleSelect
                     retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><select id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '"  title="' + attToolTip + '" size="' + attLen + '" class="cssClassMultiSelect dynFormItem" multiple>';
                     if (attTypeValue.length > 0) {
                         for (var i = 0; i < attTypeValue.length; i++) {
                             var val = attTypeValue[i];
                             //alert(val.text);
                             //var vals = attTypeValue[i].split(':');
                             if (val.isdefault == 1) {
                                 retString += '<option value="' + val.value + '" selected="selected">' + val.text + '</option>';
                             }
                             else {
                                 retString += '<option value="' + val.value + '">' + val.text + '</option>';
                             }
                         }
                     }
                     retString += '</select><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 case 6: //DropDown
                     if (attID == 29) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><select id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '"  title="' + attToolTip + '" class="cssClassDropDown dynFormItem FeaturedDropDown">';
                     }
                     else if (attID == 32) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><select id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '"  title="' + attToolTip + '" class="cssClassDropDown dynFormItem SpecialDropDown">';
                     }
                     else {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><select id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '"  title="' + attToolTip + '" class="cssClassDropDown dynFormItem">';
                     }
                     for (var i = 0; i < attTypeValue.length; i++) {
                         var val = attTypeValue[i];
                         if (val.isdefault == 1) {
                             retString += '<option value="' + val.value + '" selected="selected">' + val.text + '</option>';
                         }
                         else {
                             retString += '<option value="' + val.value + '">' + val.text + '</option>';
                         }
                     }
                     retString += '</select><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';

                     break;
                 case 7: //Price
                     if (attID == 8) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text"  class="cssClassNormalTextBox dynFormItem classItemPrice ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" maxlength="' + attLen + '" title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     }
                     else if (attID == 13) {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text"  class="cssClassNormalTextBox dynFormItem classItemListPrice ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" maxlength="' + attLen + '" title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     }
                     else {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text"  class="cssClassNormalTextBox dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" maxlength="' + attLen + '" title="' + attToolTip + '"/><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     }
                     break;
                 case 8: //File
                     FileUploaderIDs[FileUploaderIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder;
                     retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><div class="' + attDefVal + '" name="Upload/temp" lang="' + attLen + '"><input type="hidden" id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '_hidden" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '_hidden" value="" class="cssClassBrowse dynFormItem"/>';
                     retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="file" class="cssClassBrowse dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" title="' + attToolTip + '" />';
                     //retString += '<span id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '_span" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="file" class="cssClassBrowse">Browse</span>';
                     retString += ' <span class="response"></span></div><span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 case 9: //Radio
                     if (attTypeValue.length > 0) {
                         retString += '<td class="cssClassTableRightCol"><div class="cssClassRadioBtn ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">';
                         for (var i = 0; i < attTypeValue.length; i++) {
                             var val = attTypeValue[i];
                             if (val.isdefault == 1) {
                                 retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="radio"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" checked="checked" title="' + attToolTip + '"/><label>' + val.text + '</label>';
                             }
                             else {
                                 retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="radio"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" title="' + attToolTip + '"/><label>' + val.text + '</label>';
                             }
                         }
                         retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     }
                     break;
                 case 10: //RadioButtonList
                     retString += '<td class="cssClassTableRightCol"><div class="cssClassRadioBtn ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">'
                     for (var i = 0; i < attTypeValue.length; i++) {
                         var val = attTypeValue[i];
                         if (val.isdefault == 1) {
                             retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '_' + i + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="radio"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" checked="checked"/><label>' + val.text + '</label>';
                         }
                         else {
                             retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '_' + i + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="radio"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '"/><label>' + val.text + '</label>';
                         }
                     }
                     retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 case 11: //CheckBox
                     retString += '<td class="cssClassTableRightCol"><div class="cssClassCheckBox ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">';
                     if (attTypeValue.length > 0) {
                         for (var i = 0; i < attTypeValue.length; i++) {
                             var val = attTypeValue[i];
                             if (val.isdefault == 1) {
                                 retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + val.value + '" checked="checked"/><label>' + val.text + '</label>';
                             }
                             else {
                                 retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" value="' + val.value + '"/><label>' + val.text + '</label>';
                             }
                         }
                     }
                     retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 case 12: //CheckBoxList
                     retString += '<td class="cssClassTableRightCol"><div class="cssClassCheckBox ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">';
                     if (attTypeValue.length > 0) {
                         for (var i = 0; i < attTypeValue.length; i++) {
                             var val = attTypeValue[i];
                             if (val.isdefault == 1) {
                                 //var vals = attTypeValue[i].split(':');
                                 retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '_' + i + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '" checked="checked"/><label>' + val.text + '</label>';
                             }
                             else {
                                 retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '_' + i + '" value="' + val.value + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="checkbox"  class="text dynFormItem ' + ItemMangement.createValidation(attID, attType, attValType, isUnique, isRequired) + '"/><label>' + val.text + '</label>';
                             }
                         }
                     }
                     retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 case 13: //Password
                     retString += '<td class="cssClassTableRightCol"><div class="field ' + ItemMangement.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + '_' + groupId + '_' + isIncludeInPriceRule + '_' + isIncludeInPromotions + '_' + displayOrder + '" type="text" maxlength="' + attLen + '"  class="cssClassNormalTextBox dynFormItem ' + ItemMangement.createValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + ' Password" value="' + attDefVal + '" title="' + attToolTip + '"/>'
                     retString += '<span class="iferror">' + ItemMangement.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 default:
                     break;
             }
             retString += '</tr>';
             return retString;
         },

         SampleFileUploader: function(maxFileSize) {
             var upload = new AjaxUpload($('#fileSample'), {
                 action: aspxItemModulePath + "MultipleFileUploadHandler.aspx",
                 name: 'myfile[]',
                 multiple: true,
                 data: {},
                 autoSubmit: true,
                 responseType: 'json',
                 onChange: function(file, ext) {
                     //alert('changed');
                 },
                 onSubmit: function(file, ext) {
                     if (ext != "exe") {
                         this.setData({
                             'MaxFileSize': maxFileSize
                         });
                     }
                     else {
                         csscody.alert('<h2>Alert Message</h2><p>Not a valid file type!</p>');
                         return false;
                     }
                 },
                 onComplete: function(file, response) {
                     var res = eval(response);
                     if (res.Message != null) {
                         // alert(res.Message);
                         ItemMangement.showSampleLoadedFile(res);
                         return false;
                     }
                     else {
                         csscody.error('<h2>Error Message</h2><p>Failed to upload file!</p>');
                         return false;
                     }
                 }
             });
         },

         showSampleLoadedFile: function(response) {
             //alert(response.Message);
             $("#spanSample").html('LoadedFile: ');
             $("#spanSample").append(response.Message);
             $("#fileSample").attr('name', response.Message);

         },

         showActualLoadedFile: function(response) {
             // alert(response.Message);
             $("#spanActual").html('LoadedFile: ');
             $("#spanActual").append(response.Message);
             $("#fileActual").attr('name', response.Message);
         },

         ActualFileUploader: function(maxFileSize) {
             var upload = new AjaxUpload($('#fileActual'), {
                 action: aspxItemModulePath + "MultipleFileUploadHandler.aspx",
                 name: 'myfile[]',
                 multiple: true,
                 data: {},
                 autoSubmit: true,
                 responseType: 'json',
                 onChange: function(file, ext) {
                     //alert('changed');
                 },
                 onSubmit: function(file, ext) {
                     if (ext != "exe") {
                         this.setData({
                             'MaxFileSize': maxFileSize
                         });
                     }
                     else {
                         csscody.alert('<h2>Alert Message</h2><p>Not a valid file type!</p>');
                         return false;
                     }
                 },
                 onComplete: function(file, response) {
                     var res = eval(response);
                     if (res.Message != null) {
                         //   alert(res.Message);
                         ItemMangement.showActualLoadedFile(res);
                         return false;
                     }
                     else {
                         csscody.error('<h2>Error Message</h2><p>Failed to upload the image!</p>');
                         return false;
                     }
                 }
             });
         },
         //    FileUpload(FileUploaderID) {
         //        var previousFile = $(FileUploaderID).attr("title");
         //        var downlodablefileuploaderID = $(FileUploaderID).attr('id');
         //        alert(downlodablefileuploaderID);

         //        var upload = new AjaxUpload(String(downlodablefileuploaderID), {
         //            action: aspxItemModulePath + "MultipleFileUploadHandler.aspx",
         //            name: 'myfile[]',
         //          //  multiple: false,
         //          //  data: {},
         //            autoSubmit: true,
         //           // responseType: 'json',
         //           // onChange: function(file, ext) {
         //                //alert('changed');
         //           // },
         //            onSubmit: function(file, ext) {
         //                if (ext != "exe") {
         //                    alert("load");
         //                    //                    if (ext && /^(jpg|jpeg|jpe|gif|bmp|png|ico)$/i.test(ext)) {
         //                    //                    } else {
         //                    //                        csscody.alert('<h2>Alert Message</h2><p>Not a valid image!</p>');
         //                    //                        return false;
         //                    //                    }
         //                }
         //                else {
         //                    csscody.alert('<h2>Alert Message</h2><p>Not a valid image!</p>');
         //                    return false;
         //                }
         //            },
         //            onComplete: function(file, response) {
         //                var res = eval(response);
         //                if (res.Message != null) {
         //                   alert(res.Message);
         //                }
         //                else {
         //                    csscody.error('<h2>Error Message</h2><p>Can\'t upload the file!</p>');
         //                }
         //            }
         //        });

         //    }

         CreateAccordion: function(attGroup, attributeSetId, itemTypeId, showDeleteBtn) {
             //alert($("#dynItemForm").html());
             if (FormCount) {
                 FormCount = new Array();
             }
             var FormID = "form_" + (FormCount.length * 10 + Math.floor(Math.random() * 10));
             FormCount[FormCount.length] = FormID;
             var dynHTML = '';
             var tabs = '';

             for (var i = 0; i < attGroup.length; i++) {
                 tabs += '<div class="accordionHeading"><h3><a href="#" name="' + attGroup[i].key + '">' + attGroup[i].value + '</a></h3></div>';
                 tabs += '<div><table width="100%" border="0" cellpadding="0" cellspacing="0">' + attGroup[i].html + '</table></div>';
             }
             //Add Static sections here
             //In Add New:: need to add some static accordin tabs :: Image, Inventory, Categories, Related Products, Up-sells, Cross-sells, Custom Options
             //In edit:: Product Reviews, Product Tags, Customers Tagged Product
             if (itemTypeId == 2) {
                 tabs += '<div class="accordionHeading"><h3><a href="#">Download Information</a></h3></div>';
                 tabs += '<div id="divDownloadInfo">';
                 tabs += '<table class="cssClassFormWrapper" width="100%" border="0" cellpadding="o" cellspacing="0">';
                 tabs += '<tbody>';
                 tabs += '<tr><td><span class="cssClassLabel">Title: </span></td><td class="cssClassTableRightCol"><div class="field required"><input type="textbox" id="txtDownloadTitle" class="cssClassNormalTextBox" maxlength="256"/><span class="iferror"></span></div></td></tr>';
                 tabs += '<tr><td><span class="cssClassLabel">Maximum Download: </span></td><td class="cssClassTableRightCol"><div class="field required"><input type="textbox" id="txtMaxDownload" class="cssClassNormalTextBox" maxlength="3"/><span class="iferror">Integer Number</span></div></td></tr>';
                 //tabs += '<tr><td><span class="cssClassLabel">Is Sharable? </span></td><td class="cssClassTableRightCol"><input type="checkbox" name="chkIsSharable" class="cssClassCheckBox notTest" /></td></tr>';
                 tabs += '<tr><td><span class="cssClassLabel">Sample File: </span></td><td class="cssClassTableRightCol"><input id="fileSample" type="file" class="cssClassBrowse notTest" /><span id="spanSample" class="cssClassLabel"></span></td></tr>';
                 tabs += '<tr><td><span class="cssClassLabel">Actual File: </span></td><td class="cssClassTableRightCol"><input id="fileActual" type="file" class="cssClassBrowse notTest" /><span id="spanActual" class="cssClassLabel"></span></td></tr>';
                 //tabs += '<tr><td><span class="cssClassLabel">Display Order: </span></td><td class="cssClassTableRightCol"><div class="field required"><input type="textbox" id="txtDownDisplayOrder" class="cssClassNormalTextBox" maxlength="3"/><span class="iferror">Integer Number</span></div></td></tr>';
                 tabs += '</tbody>';
                 tabs += '</table></div>';
             }
             tabs += '<div class="accordionHeading"><h3><a href="#">Tax</a></h3></div><div id="divTax"><span class="cssClassLabel">Tax Rule Name: </span><select id="ddlTax" class="cssClassDropDown" /></div>';
             tabs += '<div class="accordionHeading"><h3><a href="#">Images</a></h3></div><div id="multipleUpload"><div id="divUploader"><input id="fileUpload" type="file" class="cssClassBrowse" /></div>';
             tabs += '<div id="divTableWrapper" class="cssClassGridWrapperContent"><table class="classTableWrapper" width="100%" border="0" cellpadding="o" cellspacing="0"><thead></thead><tbody></tbody></table></div></div>';
             tabs += '<div class="accordionHeading"><h3><a href="#">Categories</a></h3></div><div id="tblCategoryTree" width="100%" border="0" cellpadding="0" cellspacing="0"><select id="lstCategories" class="cssClassMultiSelect"></select><span id="spanNoCat" class="cssClassLabel"></span></div>';
             if (showDeleteBtn) {
                 tabs += '<div class="accordionHeading"><h3><a href="#">Cost Variant Options</a></h3></div><div><div class="cssClassGridWrapper" id="variantsGrid"><table id="gdvItemCostVariantGrid" width="100%" border="0" cellpadding="0" cellspacing="0"></table>';
                 tabs += '<div class="cssClassButtonWrapper"><p><button type="button" id="btnAddExistingOption" ><span><span>Add Existing Option</span></span></button></p><P><button type="button" id="btnAddNewOption"><span><span>Add New Option</span></span></button></p></div></div>';

                 tabs += '<div id="newCostvariants"><h2><label id="lblCostVariantOptionTitle">Item Cost Variant Option</label></h2><div id="divExistingVariant" class="cssClassFormWrapper"><label for="ddlExistingOptions" id="lblExistingOptions" class="cssClassLabel">Existing Cost Variant Options:</label><div id="divExisitingDropDown"><select id="ddlExistingOptions" class="cssClassDropDown"></select><div class="cssClassButtonWrapper"><p> <button type="button" id="btnApplyExisingOption"><span><span>Apply</span></span></button></p><p> <button type="button" id="btnExisingBack"><span><span>Back</span></span></button></p></div></div></div>';
                 tabs += '<div id="divNewVariant"><input type="hidden" id="hdnItemCostVar" value="0" /><div id="divAddNewOptions"><div class="cssClassTabpanelContent"> <div id="container-7" class="cssClassMargin"><ul><li><a href="#fragment-1"><span>Cost Variant Option Properties</span></a></li><li><a href="#fragment-3"><span id="lblTabTitle3">Variants Properties</span></a></li></ul>';
                 tabs += '<div id="fragment-1"><table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassFormWrapper"><tr><td><label id="lblCostVariantName">Cost Variant Name:</label><span class="cssClassRequired">*</span></td><td class="cssClassTableRightCol"><input type="text" id="txtCostVariantName" class="cssClassNormalTextBox" /> <span class="cssClassCostVarRight"> <img class="cssClassSuccessImg" height="13" width="18" src="' + aspxTemplateFolderPath + '/images/right.jpg" alt="Right" /></span><b class="cssClassCostVarError">Ops! found something error, must be unique with no spaces</b></td></tr><tr><td><label id="lblCostVariantDescription">Description:</label></td><td class="cssClassTableRightCol"> <textarea id="txtDescription" name="txtDescription" title="Cost Variant Description"rows="2" cols="15" class="cssClassTextArea"></textarea> </td></tr><tr><td><label id="lblType">Type:</label></td><td class="cssClassTableRightCol"><select id="ddlAttributeType" class="cssClassDropDown" name="" title="Cost Variant Input Type"></select></td></tr><tr><td><label id="lblDisplayOrder">Display Order:</label><span class="cssClassRequired">*</span></td><td class="cssClassTableRightCol"><input class="cssClassNormalTextBox" id="txtDisplayOrder" type="text" /><span id="displayOrder"></span></td></tr><tr><td><label id="lblActive">Is Active:</label></td><td><div id="" class="cssClassCheckBox"> <input type="checkbox" name="chkActive" /></div></td></tr></table></div>';
                 tabs += '<div id="fragment-3"><table width="100%" cellspacing="0" cellpadding="0" id="tblVariantTable"><thead><tr><td>Pos.</td><td>Name</td><td>Modifier&nbsp;/Type </td><td>Weight modifier&nbsp;/&nbsp;Type</td><td>Status</td></tr></thead><tr><td><input type="hidden" size="3" class="cssClassVariantValue" value="0" /><input type="text" size="3" class="cssClassDisplayOrder" /></td> <td><input type="text" class="cssClassItemCostVariantValueName" /></td><td><input type="text" size="5" class="cssClassPriceModifier" /> &nbsp;/&nbsp;<select class="cssClassPriceModifierType"> <option value="0">$</option><option value="1">%</option></select></td><td><input type="text" size="5" class="cssClassWeightModifier" />&nbsp;/&nbsp;<select class="cssClassWeightModifierType"><option value="0">lbs</option><option value="1">%</option></select></td><td><select class="cssClassIsActive"><option value="1">Active</option><option value="0">Disabled</option></select></td><td><span class="nowrap"><img width="13" height="18" border="0" align="top" class="cssClassAddRow" title="Add empty item"alt="Add empty item" name="add" src="' + aspxTemplateFolderPath + '/images/admin/icon_add.gif" />&nbsp;<img width="13" height="18" border="0" align="top" class="cssClassCloneRow" alt="Clone this item"title="Clone this item" name="clone" src="' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif" />&nbsp; <img width="12" height="18" border="0" align="top" class="cssClassDeleteRow" alt="Remove this item"name="remove" src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" />&nbsp;<button type="button" value="0" class="classAddImages" rel="popuprel2"><span><span>Add Images</span></span></button> </td></tr></table></div> </div></div></div>';
                 tabs += '<div class="cssClassButtonWrapper"><p><button type="button" id="btnBackVariantOptions"><span><span>Back</span></span></button></p><p><button type="button" id="btnResetVariantOptions"><span><span>Reset</span></span></button></p><p><button type="button" id="btnSaveItemVariantOption"><span><span>Save Option</span></span></button></p><div class="cssClassClear"></div></div></div></div></div>';

                 //Item Quantity Discounts
                 tabs += '<div class="accordionHeading"><h3><a href="#">Item Quantity Discounts (Tier Price Options)</a></h3></div><div class="cssClassFormWrapper"><table width="100%" cellspacing="0" cellpadding="0" id="tblQuantityDiscount"><thead><tr class="cssClassHeading"><td>Quantity More Than:</td><td>Unit Price($):</td><td>User In Role:</td><td>&nbsp;</td></tr></thead><tbody></tbody></table>';
                 tabs += '<div class="cssClassButtonWrapper"><p><button type="button" id="btnSaveQuantityDiscount" ><span><span>Save</span></span></button></p></div></div>';
             }

             tabs += '<div class="accordionHeading"><h3><a href="#">Related Items</a></h3></div><div class="cssClassGridWrapper"><table id="gdvRelatedItems" width="100%" border="0" cellpadding="0" cellspacing="0"></table></div>';
             tabs += '<div class="accordionHeading"><h3><a href="#">Up-sells</a></h3></div><div class="cssClassGridWrapper"><table id="gdvUpSellItems" width="100%" border="0" cellpadding="0" cellspacing="0"></table></div>';
             tabs += '<div class="accordionHeading"><h3><a href="#">Cross-sells</a></h3></div><div class="cssClassGridWrapper"><table id="gdvCrossSellItems" width="100%" border="0" cellpadding="0" cellspacing="0"></table></div>';
             dynHTML += tabs;
             var frmIDQuoted = "'" + FormID + "'";
             //Create buttons
             var buttons = '<div class="cssClassButtonWrapper"><p><button type="button" id="btnReturn"><span><span>Back</span></span></button></p>';
             if (!showDeleteBtn) {
                 buttons += '<p><button type="button" id="btnResetForm" ><span><span>Reset</span></span> </button></p>';
             }
             else {
                 buttons += '<p><button type="button" id="btnDelete" class="delbutton" ><span><span>Delete Item</span></span> </button></p>';
             }
             buttons += '<p><button type="button" id="saveForm"  onclick="ItemMangement.SubmitForm(' + frmIDQuoted + ',' + attributeSetId + ',' + itemTypeId + ')" ><span><span>Save Item</span></span></button></p>';
             buttons += '<div class="cssClassClear"></div></div>'
             $("#dynItemForm").html('<div id="' + FormID + '" class="cssClassFormWrapper"><div id="accordion" class="cssClassAccordion">' + dynHTML + '</div>' + buttons + '</div>');
             ItemMangement.EnableAccordion();
             ItemMangement.EnableFormValidation(FormID);
             ItemMangement.EnableDatePickers();
             ItemMangement.EnableFileUploaders();
             ItemMangement.EnableHTMLEditors();
             // ItemMangement.activatedatetimevalidation();
             $('#btnResetForm').bind("click", function() {
                 ItemMangement.ClearAttributeForm();
                 ItemMangement.OnInit();
                 ItemMangement.ClearVariantForm();
                 ItemMangement.BindCostVariantsInputType();
                 ItemMangement.LoadItemStaticImage();
             });

         },

         EnableAccordion: function() {
             //set icon and autoheight and active index
             $("#accordion").accordion({ autoHeight: false,
                 icons: { 'header': 'ui-icon-triangle-1-e', 'headerSelected': 'ui-icon-triangle-1-s' },
                 //animated: 'bounceslide',
                 active: 0
             });
             //alert($("#dynItemForm").html());
         },

         EnableFormValidation: function(frmID) {
             mustCheck = true;
             $("#" + frmID + " ." + classprefix + "Cancel").click(function(event) {
                 mustCheck = false;
             });
             var fe = $("#" + frmID + " input");
             for (var j = 0; j < fe.length; j++) {
                 if ((fe[j]).title.indexOf("**") == 0) {
                     if ((fe[j]).value == "" || (fe[j]).value == titleHint) {
                         var titleHint = (fe[j]).title.substring(2);
                         (fe[j]).value = titleHint;
                     }
                 } else if (((fe[j]).type == "text" || (fe[j]).type == "password" || (fe[j]).type == "textarea") && (fe[j]).title.indexOf("*") == 0) {
                     addHint((fe[j]));
                     $(fe[j]).blur(function(event) { addHint(this); });
                     $(fe[j]).focus(function(event) { removeHint(this); });
                 }
             }
         },

         EnableDatePickers: function() {
             for (var i = 0; i < DatePickerIDs.length; i++) {
                 //$(selector).datepicker($.datepicker.regional['fr']);
                 $("#" + DatePickerIDs[i]).datepicker({ dateFormat: 'yy/mm/dd' });
             }
         },

         HTMLEditor: function(editorID, editorObject) {
             this.ID = editorID;
             this.Editor = editorObject;
         },

         EnableHTMLEditors: function() {
             for (var i = 0; i < htmlEditorIDs.length; i++) {
                 config = { skin: "v2" };
                 var html = "Initially Text if necessary";

                 var editorID = htmlEditorIDs[i];
                 //alert(editorID + '::' + htmlEditorIDs.length + '::' + editorList.length);
                 var instance = CKEDITOR.instances[editorID];
                 if (instance) {
                     CKEDITOR.remove(instance);
                     //delete instance;
                 }
                 var editor = CKEDITOR.replace(editorID, config, html);

                 var obj = new ItemMangement.HTMLEditor(editorID, editor);
                 //obj.enterMode == CKEDITOR.ENTER_BR //CKEDITOR.ENTER_DIV CKEDITOR.ENTER_P
                 editorList[editorList.length] = obj;
             }
         },

         ResetHTMLEditors: function() {
             htmlEditorIDs.length = 0;
             editorList.length = 0;
         },

         EnableFileUploaders: function() {
             for (var i = 0; i < FileUploaderIDs.length; i++) {
                 ItemMangement.CreateFileUploader(String(FileUploaderIDs[i]));
             }
         },

         GetValidationTypeClasses: function(attValType, isUnique, isRequired) {
             var returnClass = ''
             if (isRequired == true) {
                 returnClass = "required";
             }
             return returnClass;
         },

         GetValidationTypeErrorMessage: function(attValType) {
             var retString = ''
             switch (attValType) {
                 case 1: //AlphabetsOnly
                     retString = 'Alphabets Only';
                     break;
                 case 2: //AlphaNumeric
                     retString = 'AlphaNumeric';
                     break;
                 case 3: //DecimalNumber
                     retString = 'Decimal Number';
                     break;
                 case 4: //Email
                     retString = 'Email Address';
                     break;
                 case 5: //IntegerNumber
                     retString = 'Integer Number';
                     break;
                 case 6: //Price
                     retString = 'Price error';
                     break;
                 case 7: //WebURL
                     retString = 'Web URL';
                     break;
             }
             return retString;
         },
         CheckUniqueness: function(sku, itemId) {
             var errors = '';
             sku = $.trim(sku);
             if (!sku) {
                 errors += 'Please enter item Sku code';
                 $('.cssClassRight').hide();
                 $('.cssClassError').show();
                 $('.cssClassError').html(' Please enter Sku code.<br/>');
             }
             //check uniqueness
             else if (!ItemMangement.IsUnique(sku, itemId)) {
                 errors += 'Please enter unique item Sku code! "' + sku.trim() + '" already exists.<br/>';
                 $('.cssClassRight').hide();
                 $('.cssClassError').show();
                 $('.cssClassError').html('Please enter unique Sku code! "' + sku.trim() + '" already exists.<br/>');
                 $(".cssClassError").parent('div').addClass("diverror");
                 $('.cssClassError').prevAll("input:first").addClass("error");
             }

             if (errors) {
                 // csscody.alert('<h2>Information Alert</h2><p>' + errors + '</p>');
                 return false;
             }
             else {
                 $('.cssClassRight').show();
                 $('.cssClassError').html('');
                 $('.cssClassError').hide();
                 $(".cssClassError").parent('div').removeClass("diverror");
                 $('.cssClassError').prevAll("input:first").removeClass("error");
                 return true;
             }
         },

         IsUnique: function(sku, itemId) {
             this.config.url = this.config.baseURL + "CheckUniqueItemSKUCode";
             this.config.data = JSON2.stringify({ SKU: sku, itemId: itemId, storeId: storeId, portalId: portalId, cultureName: cultureName }),
                this.config.ajaxCallMode = 25;
             this.ajaxCall(this.config);
             return ItemMangement.vars.isUnique;
         },

         CheckUnique: function(id) {
             var val = $('#' + id).val();
             if (val) {
                 var arrID = id.split('_');

                 this.config.url = this.config.baseURL + "IsUnique";
                 this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, ItemID: 1, AttributeID: arrID[0], AttributeType: arrID[1], AttributeValue: val });
                 this.config.ajaxCallMode = 26;
                 this.ajaxCall(this.config);
                 return ItemMangement.vars.isUnique;
             }
             else {
                 return false;
             }
         },

         createValidation: function(id, attType, attValType, isUnique, isRequired) {
             var retString = '';
             var validationClass = '';

             switch (attValType) {
                 case 1: //AlphabetsOnly
                     validationClass += 'verifyAlphabetsOnly" ';
                     break;
                 case 2: //AlphaNumeric
                     validationClass += 'verifyAlphaNumeric" ';
                     break;
                 case 3: //DecimalNumber
                     validationClass += 'verifyDecimal" ';
                     break;
                 case 4: //Email
                     validationClass += 'verifyEmail';
                     break;
                 case 5: //IntegerNumber
                     validationClass += 'verifyInteger';
                     break;
                 case 6: //Price
                     validationClass += 'verifyPrice';
                     break;
                 case 7: // URL
                     validationClass += 'verifyUrl';
                     break;
             }

             retString = validationClass;
             return retString;
         },

         BackToItemGrid: function() {
             ItemMangement.ResetHTMLEditors();
             var n = $("#btnDelete").length;
             if (n != 0) {
                 $("#gdvItems_grid").show();
                 $("#gdvItems_form").hide();
                 $("#gdvItems_accordin").hide();
             }
             else {
                 $("#gdvItems_form").show();
                 $("#gdvItems_grid").hide();
                 $("#gdvItems_accordin").hide();
             }
         },

         ValidateExtraField: function(cssClassFirst, cssClassSecond, validateType, ErrorMessage) {
             var valFirst = $('.' + cssClassFirst + '').val();
             var valSecond = $('.' + cssClassSecond + '').val();
             var prevFirstDiv = $('.' + cssClassFirst + '').parent('div');
             var prevSecondDiv = $('.' + cssClassSecond + '').parent('div');
             if (prevFirstDiv.length > 0 && prevSecondDiv.length > 0) {
                 switch (validateType) {
                     case "price":
                         valFirst = parseFloat(valFirst);
                         valSecond = parseFloat(valSecond);
                         break;
                     case "date":
                         valFirst = Date.parse(valFirst);
                         valSecond = Date.parse(valSecond);
                         break;
                     default:
                         valFirst = eval(valFirst);
                         valSecond = eval(valSecond);
                 }
                 if (valSecond >= valFirst) {
                     $('.' + cssClassFirst + '').removeClass('error');
                     $('.' + cssClassSecond + '').removeClass('error');
                     prevFirstDiv.removeClass('diverror');
                     prevSecondDiv.removeClass('diverror');
                     return true;
                 }
                 else {
                     $('.' + cssClassSecond + '').next('span').html(ErrorMessage);
                     $('.' + cssClassFirst + '').addClass('error');
                     prevFirstDiv.addClass('diverror');
                     $('.' + cssClassSecond + '').addClass('error');
                     prevSecondDiv.addClass('diverror');
                     return false;
                 }
             }
             else {
                 prevFirstDiv.removeClass('diverror');
                 prevSecondDiv.removeClass('diverror');
                 return true;
             }
         },
         CostVariantsImageUploader: function(maxFileSize) {
             var upload = new AjaxUpload($('#imageUploader'), {
                 action: aspxItemModulePath + "ItemCostVariantsFileUpload.aspx",
                 name: 'myfile[]',
                 multiple: true,
                 data: {},
                 autoSubmit: true,
                 responseType: 'json',
                 onChange: function(file, ext) {
                     //alert('changed');
                 },
                 onSubmit: function(file, ext) {
                     if (ext != "exe") {
                         if (ext && /^(jpg|jpeg|jpe|gif|bmp|png|ico)$/i.test(ext)) {
                             this.setData({
                                 'MaxFileSize': maxFileSize
                             });
                         } else {
                             csscody.alert('<h1>Alert Message</h1><p>Not a valid image!</p>');
                             return false;
                         }
                     }
                     else {
                         csscody.alert('<h1>Alert Message</h1><p>Not a valid image!</p>');
                         return false;
                     }
                 },
                 onComplete: function(file, response) {
                     var res = eval(response);
                     if (res.Message != null && res.Status > 0) {
                         //alert(res.Message);
                         ItemMangement.AddNewVariantsImages(res);
                         return false;
                     }
                     else {
                         csscody.error('<h1>Error Message</h1><p>' + res.Message + '</p>');
                         return false;
                     }
                 }
             });
         },

         AddNewVariantsImages: function(response) {
             $("#VariantsImagesTable").show();
             $('#btnSaveImages').show();
             $('#btnImageBack').show();
             var imageList = '<tr>';
             imageList += '<td><img src="' + aspxRootPath + response.Message + '" class="uploadImage" height="20px" width="30px"/></td>';
             imageList += '<td><img class="imgDeleteCostVariant" src="' + aspxTemplateFolderPath + '/images/admin/btndelete.png" id="btnDeleteCV" onclick="ItemMangement.DeleteCVImage(this)" /></td>';
             imageList += '</tr>';
             $("#VariantsImagesTable>tbody").append(imageList);
             $("#VariantsImagesTable>tbody tr:even").addClass("cssClassAlternativeEven");
             $("#VariantsImagesTable>tbody tr:odd").addClass("cssClassAlternativeOdd");
         },
         DeleteCVImage: function(onjImg) { //code to delete row                
             $(onjImg).parent().parent().remove();
         },
         ClearAttributeForm: function() {

             $('#Todatevalidation').attr('class', '');
             $('#Fromdatevalidation').attr('class', '');
             $("#ItemMgt_itemID").val(0);
             var attributeSetId = '';
             var itemTypeId = '';
             attributeSetId = $("#ddlAttributeSet").val();
             itemTypeId = $("#ddlItemType").val();
             $("#spanSample").html("");
             $("#spanActual").html("");
             ItemMangement.ContinueForm(false, attributeSetId, itemTypeId, 0);

             //             var inputs = $("#accordion").find('INPUT, SELECT, TEXTAREA');
             //             $.each(inputs, function(i, item) {
             //                 rmErrorClass(item);
             //                 $(this).val('');
             //                 $(this).attr('checked', false);
             //             });
             //             $('.required').find('.cke_skin_v2 iframe').each(function() {
             //                 $(this).contents().find("body").text('')
             //             });
             //            // $("#gdvCrossSellItems input[type=checkbox],#gdvUpSellItems input[type=checkbox],#gdvRelatedItems input[type=checkbox]").each(function(i) {
             //             //    $(this).attr("checked", false);
             //            // });
             //             $('.cssClassRight').hide();
             //             $('.cssClassError').hide();
             //             $('.cssClassError').html('');
             //             ItemMangement.ResetImageTab();
             //             return false;
         },

         ResetImageTab: function() {
             $("#divTableWrapper>table>thead").html('');
             $("#divTableWrapper>table>tbody").html('');
         },

         SaveItem: function(formID, attributeSetId, itemTypeId, itemId) {
             //        var arForm = { storeID: storeId, portalID: portalId, userName: userName, culture: cultureName, isActive: 1, isModified: 0, itemID: itemID, itemTypeID: itemTypeId, attributeSetID: attributeSetId, updateFlag: 0, formVars: SerializeForm(formID)};
             //        arForm = JSON2.stringify(arForm);
             itemEditFlag = itemId;
             //Image tab save here
             var sourceFileCollection = '';
             var filepath = '';
             var contents = '';
             var counter = 0;
             var categoriesSelected = false;
             //no need to validate image 
             //        if ($("#multipleUpload .classTableWrapper > tbody >tr ").length >= 1) {

             //            if ($("#multipleUpload .classTableWrapper > tbody >tr ").length == 1) {
             //                $("#multipleUpload .classTableWrapper > tbody >tr:first td:eq(3) input:radio ").attr('checked', true);
             //            }
             //        }

             //        else {
             //            csscody.alert('<h2>Information Alert</h2><p>You need to upload at least one Base Image first!</p>');
             //            return false;
             //        }

             $("#multipleUpload .classTableWrapper > tbody >tr").each(function() {
                 // filepath = $(this).find(" td:first >img").attr("src").replace(aspxRootPath, "");
                 if (aspxRootPath != "/") {
                     filepath = $(this).find(" td:first >img").attr("src").split(aspxRootPath)[1];
                 }
                 else {
                     filepath = $(this).find(" td:first >img").attr("src").replace('/', '')
                 }
                 //var replacedpath = filepath.replace("../", "");
                 // alert("File after replacing is " + replacedpath);
                 filepath = filepath.replace("/Small", "");
                 filepath = filepath.replace("/Medium", "");
                 filepath = filepath.replace("/Large", "");
                 var path_array = filepath.split('/');
                 var sizeofArray = path_array.length;

                 var fileName = path_array[sizeofArray - 1];

                 sourceFileCollection += fileName + '%';
                 contents += filepath + "%"; //aspxRootPath + '/Modules/AspxCommerce/AspxItemsManagement/uploads/' + fileName + ','; // +$(this).find(" td:eq(3) input:radio:checked").attr("value");
                 //DestFilePathCol += aspxRootPath + '/Modules/AspxCommerce/AspxItemsManagement/uploads/' + fileName + ',';
                 // alert(contents);
                 if ($(this).find(" td:eq(6) input:checkbox").is(":checked")) {
                     contents += 1;
                     contents += '%';
                 }
                 else {
                     contents += 0;
                     contents += '%';
                 }
                 if ($(this).find(" td:eq(3) input:radio").is(":checked")) {
                     counter += 1;
                     contents += $(this).find(" td:eq(3) input:radio:checked").attr("value");
                     contents += '%';
                 }
                 else if ($(this).find(" td:eq(4) input:radio").is(":checked")) {
                     contents += $(this).find(" td:eq(4) input:radio:checked").attr("value");
                     contents += '%';
                 }
                 else if ($(this).find(" td:eq(5) input:radio").is(":checked")) {
                     contents += $(this).find(" td:eq(5) input:radio:checked").attr("value");
                     contents += '%';
                 }
                 else {
                     contents += "None";
                     contents += '%';
                 }
                 if ($(this).find(" td:eq(1) input").attr("value") != null) {
                     contents += $(this).find(" td:eq(1) input").attr("value");
                     contents += '%';
                 }
                 else {
                     contents += " ";
                     contents += '%';
                 }
                 if ($(this).find(" td:eq(2) input").attr("value") != null) {
                     contents += $(this).find(" td:eq(2) input").attr("value");
                 }
                 contents += '#';
             });

             //alert(sourceFileCollection + '::' + DestFilePathCol + '::' + contents + '::' + counter);

             //RemoveHtml();
             //CreateHtml();
             //CreateTableHeader();
             //BindData()
             if (counter <= 1) {

                 var relatedItems_ids = '';
                 $("#gdvRelatedItems .chkRelatedControls").each(function(i) {
                     if ($(this).attr("checked")) {
                         relatedItems_ids += $(this).val() + ',';
                     }
                 });

                 var upSellItems_ids = '';
                 $("#gdvUpSellItems .chkUpSellControls").each(function(i) {
                     if ($(this).attr("checked")) {
                         upSellItems_ids += $(this).val() + ',';
                     }
                 });

                 var crossSellItems_ids = '';
                 $("#gdvCrossSellItems .chkCrossSellControls").each(function(i) {
                     if ($(this).attr("checked")) {
                         crossSellItems_ids += $(this).val() + ',';
                     }
                 });
                 if ($('#ddlTax').val() > 0)
                 { var taxRuleId = $('#ddlTax').val(); }
                 else {
                     csscody.alert('<h2>Information Alert</h2><p>Please select at least one tax rule.</p>');
                     return false;
                 }

                 var categoriesSelectedID = "";
                 $("#lstCategories").each(function() {
                     if ($("#lstCategories :selected").length != 0) {
                         categoriesSelected = true;
                         $("#lstCategories option:selected").each(function(i) {
                             categoriesSelectedID += $(this).val() + ',';
                         });
                     }
                 });

                 if (categoriesSelected) {
                     //save downloadable items
                     AspxCommerce.CheckSessionActive();
                     if (AspxCommerce.vars.IsAlive) {
                         AspxCommerce.Busy.LoadingShow();
                         arForm = '{"storeID":"' + storeId + '","portalID":"' + portalId + '","userName":"' + userName + '","culture":"' + cultureName + '","taxRuleID":"' + taxRuleId + '","itemID":"' + itemId + '","itemTypeID":"' + itemTypeId + '","attributeSetID":"' + attributeSetId + '","categoriesIds":"' + categoriesSelectedID + '","relatedItemsIds":"' + relatedItems_ids + '","upSellItemsIds":"' + upSellItems_ids + '","crossSellItemsIds":"' + crossSellItems_ids + '","downloadItemsValue":"' + ItemMangement.GetDownloadableFormData(itemTypeId) + '","sourceFileCol":"' + sourceFileCollection + '","dataCollection":"' + contents + '","formVars":' + ItemMangement.SerializeForm(formID) + '}';
                         this.config.url = this.config.baseURL + "SaveItemAndAttributes";
                         this.config.data = arForm;
                         this.config.ajaxCallMode = 27;
                         this.ajaxCall(this.config);
                     }
                     else {
                         window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + 'Login.aspx';
                     }
                 }
                 else {
                     csscody.alert('<h2>Information Alert</h2><p>Please select at least one category where it belongs.</p>');
                     return false;
                 }

             }
             else {
                 csscody.alert('<h2>Information Alert</h2><p>Please select only one base image for item.</p>');
                 return false;
             }
         },

         GetDownloadableFormData: function(itemTypeId) {
             var downloadabelItem = "";
             if (itemTypeId == 2) {
                 var titleHead = $("#txtDownloadTitle").val();
                 var maxDownload = $("#txtMaxDownload").val();
                 var isSharable = false; //$('input[name=chkIsSharable]').attr('checked');
                 var fileSamplePrevious = $("#fileSample").attr("title"); //$("#fileSample").val();
                 var fileSampleNewPath = $("#fileSample").attr('name');
                 var fileActualPrevious = $("#fileActual").attr("title"); //$("#fileActual").val();
                 var fileActualNewPath = $("#fileActual").attr('name');
                 var displayorder = 1; //$("#txtDownDisplayOrder").val();

                 downloadabelItem = '' + titleHead + '%' + maxDownload + '%' + isSharable + '%' + fileSamplePrevious + '%' + fileSampleNewPath + '%' + fileActualPrevious + '%' + fileActualNewPath + '%' + displayorder + '';
             }
             return downloadabelItem;
         },

         RemoveHtml: function() {
             $('#multipleUpload div.cssClassGridWrapperContent>table>tbody').html('');
             // $('table.classTableWrapper').remove();
             //  alert("Given value is " + $("#multipleUpload .classTableWrapper > tbody >trtd:first >img").attr("src"));
         },

         CreateHtml: function() {
             $('#multipleUpload div.cssClassGridWrapperContent').html("<table class=\"classTableWrapper\" width=\"100%\" border=\"0\" cellpadding=\"o\" cellspacing=\"0\"> <thead></thead><tbody></tbody></table>");
         },

         CreateTableHeader: function() {
             if ($("#multipleUpload .classTableWrapper > thead>tr").val() == null) {
                 $("<tr class=\"cssClassHeading\"><td>Image</td><td>Description</td><td>Display Order</td><td>Base Image</td><td>Small Image</td><td>Thumbnail</td><td>IsActive</td><td>Remove</td></tr>").appendTo("#multipleUpload .classTableWrapper > thead");
                 //$("<tr><input type=\"button\" value=\"Upload\" id=\"btnUpload\"/></tr>").appendTo("#multipleUpload .classTableWrapper > tfoot");
             }
         },

         SerializeForm: function(formID) {
             var jsonStr = '';
             var frmValues = new Array();
             radioGroups = new Array();
             checkboxGroups = new Array();
             selectGroups = new Array();
             inputs = $(formID).find('INPUT, SELECT, TEXTAREA');
             $.each(inputs, function(i, item) {
                 input = $(item);
                 if (input.hasClass("dynFormItem")) {
                     var found = false;
                     switch (input.attr('type')) {
                         case 'text':
                             jsonStr += '{"name":"' + input.attr('name') + '","value":"' + $.trim(input.val()) + '"},';
                             break;
                         case 'select-multiple':
                             for (var i = 0; i < selectGroups.length; i++) {
                                 if (selectGroups[i] == input.attr('name')) {
                                     found = true;
                                     break;
                                 }
                             }
                             if (!found) {
                                 selectGroups[selectGroups.length] = input.attr('name');
                             }
                             break;
                         case 'select-one':
                             jsonStr += '{"name":"' + input.attr('name') + '","value":"' + input.get(0)[input.attr('selectedIndex')].value + '"},';
                             break;

                         case 'checkbox':
                             var ids = String(input.attr('name')).split("_");
                             if (ids[1] == 4) {
                                 jsonStr += '{"name":"' + input.attr('name') + '","value":"' + input.is(':checked') + '"},';
                             }
                             else {
                                 for (var i = 0; i <= checkboxGroups.length; i++) {
                                     if (checkboxGroups[i] == input.attr('name')) {
                                         found = true;
                                         break;
                                     }
                                 }
                                 if (!found) {
                                     checkboxGroups[checkboxGroups.length] = input.attr('name');
                                 }
                             }
                             break;

                         case 'radio':
                             for (var i = 0; i < radioGroups.length; i++) {
                                 if (radioGroups[i] == input.attr('name')) {
                                     found = true;
                                     break;
                                 }
                             }
                             if (!found) {
                                 radioGroups[radioGroups.length] = input.attr('name');
                             }
                             break;

                         case 'file':
                             var d = input.parent();
                             var img = $(d).find('span.response img.uploadImage');
                             if (img.length > 0) {
                                 var imgToUpload = "";
                                 if (img.attr("src") != undefined) {
                                     imgToUpload = img.attr("src");
                                 }
                                 jsonStr += '{"name":"' + input.attr('name') + '","value":"' + imgToUpload.replace(aspxRootPath, "") + '"},';
                             }
                             else {
                                 var a = $(d).find('span.response a.uploadFile');
                                 var fileToUpload = "";
                                 if (a.attr("href") != undefined) {
                                     fileToUpload = a.attr("href");
                                 }
                                 if (a) {
                                     jsonStr += '{"name":"' + input.attr('name') + '","value":"' + fileToUpload.replace(aspxRootPath, "") + '"},';
                                 }
                             }
                             var hdn = $(d).find('input[type="hidden"]');
                             if (hdn) {
                                 jsonStr += '{"name":"' + hdn.attr('name') + '","value":"' + hdn.val() + '"},';
                             }
                             break;

                         case 'password':
                             jsonStr += '{"name":"' + input.attr('name') + '","value":"' + $.trim(input.val()) + '"},';
                             break;
                         case 'textarea':
                             // jsonStr += '{"name":"' + input.attr('name') + '","value":"' + $.trim(input.val()) + '"},';
                             jsonStr += '{"name":"' + input.attr('name') + '","value":"' + $.trim(input.val().replace(/(&nbsp;)*/g, "")) + '"},';

                             break;
                         default:
                             break;
                     }
                 }
             });
             for (var i = 0; i < selectGroups.length; i++) {
                 var selIDs = '';
                 $('#' + selectGroups[i] + ' :selected').each(function(i, selected) {
                     selIDs += $(selected).val() + ",";
                 });
                 selIDs = selIDs.substr(0, selIDs.length - 1);
                 jsonStr += '{"name":"' + selectGroups[i] + '","value":"' + selIDs + '"},';
             }

             for (var i = 0; i < checkboxGroups.length; i++) {
                 var chkValues = '';
                 $('input[name=' + checkboxGroups[i] + ']').each(function(i, item) {
                     if ($(this).is(':checked')) {
                         chkValues += $(this).val() + ",";
                     }
                 });
                 chkValues = chkValues.substr(0, chkValues.length - 1);
                 jsonStr += '{"name":"' + checkboxGroups[i] + '","value":"' + chkValues + '"},';
             }

             for (var i = 0; i < radioGroups.length; i++) {
                 var radValues = '';
                 radValues = $('input[name=' + radioGroups[i] + ']:checked').val();
                 jsonStr += '{"name":"' + radioGroups[i] + '","value":"' + radValues + '"},';
             }
             jsonStr = jsonStr.substr(0, jsonStr.length - 1);
             return '[' + jsonStr + ']';
         },

         CreateFileUploader: function(uploaderID) {
             //alert(d.html());
             new AjaxUpload(String(uploaderID), {
                 action: aspxItemModulePath + 'FileUploader.aspx',
                 name: 'myfile',
                 onSubmit: function(file, ext) {
                     d = $('#' + uploaderID).parent();
                     baseLocation = d.attr("name");
                     validExt = d.attr("class");
                     maxFileSize = d.attr("lang");
                     var regExp = /\s+/g;
                     myregexp = new RegExp("(" + validExt.replace(regExp, "|") + ")", "i");
                     if (ext != "exe") {
                         if (ext && myregexp.test(ext)) {
                             this.setData({
                                 'BaseLocation': baseLocation, 'ValidExtension': validExt, 'MaxFileSize': maxFileSize
                             });
                         } else {
                             csscody.alert('<h2>Information Alert</h2><p>You are trying to upload invalid file type!</p>');
                             return false;
                         }
                     }
                     else {
                         csscody.alert('<h2>Information Alert</h2><p>You are trying to upload invalid file type!</p>');
                         return false;
                     }
                 },
                 onComplete: function(file, ajaxFileResponse) {
                     d = $('#' + uploaderID).parent();
                     var res = eval(ajaxFileResponse);
                     if (res.Status > 0) {
                         baseLocation = d.attr("name");
                         validExt = d.attr("class");
                         var fileExt = (-1 !== file.indexOf('.')) ? file.replace(/.*[.]/, '') : '';
                         myregexp = new RegExp("(jpg|jpeg|jpe|gif|bmp|png|ico)", "i");
                         if (myregexp.test(fileExt)) {
                             $(d).find('span.response').html('<div class="cssClassLeft"><img src="' + aspxRootPath + res.UploadedPath + '" class="uploadImage" height="90px" width="100px" /></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="ItemMangement.ClickToDeleteImage(this)" alt="Delete" title="Delete"/></div>');
                         }
                         else {
                             $(d).find('span.response').html('<div class="cssClassLeft"><a href="' + aspxRootPath + res.UploadedPath + '" class="uploadFile" target="_blank">' + file + '</a></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="ItemMangement.ClickToDeleteImage(this)" alt="Delete" title="Delete"/></div>');
                         }
                     }
                     else {
                         csscody.error('<h2>Error Message</h2><p>' + res.Message + '</p>');
                     }
                 }
             });
         },

         SearchItems: function() {
             var sku = $.trim($("#txtSearchSKU").val());
             var Nm = $.trim($("#txtSearchName").val());
             if (sku.length < 1) {
                 sku = null;
             }
             if (Nm.length < 1) {
                 Nm = null;
             }
             var itemType = '';
             if ($("#ddlSearchItemType").val() != 0) {
                 itemType = $.trim($("#ddlSearchItemType").val());
             }
             else {
                 itemType = null;
             }
             var attributeSetNm = '';
             if ($("#ddlAttributeSetName").val() != 0) {
                 attributeSetNm = $.trim($("#ddlAttributeSetName").val());
             }
             else {
                 attributeSetNm = null;
             }
             //  var visibility = $.trim($("#ddlVisibitity").val()) == "" ? null : ($.trim($("#ddlVisibitity").val()) == "True" ? true : false);
             var visibility = ''
             if ($("#ddlVisibitity").val() != 0) {
                 visibility = $.trim($("#ddlVisibitity :selected").text());
             }
             else {
                 visibility = null;
             }
             var isAct = $.trim($("#ddlIsActive").val()) == "" ? null : ($.trim($("#ddlIsActive").val()) == "True" ? true : false);
             ItemMangement.BindItemsGrid(sku, Nm, itemType, attributeSetNm, visibility, isAct);
         },
         AddImages: function(data, val) {
             var lst = $(data).attr("name");
             //             if (lst != '') {
             //                 $("#VariantsImagesTable>tbody").html('');
             //                 $("#VariantsImagesTable").show();
             //                 $('#btnSaveImages').show();
             //                 $('#btnImageBack').show();
             //             } else {
             //                 $("#VariantsImagesTable>tbody").html('');
             //                 $("#VariantsImagesTable").hide();
             //                 $('#btnSaveImages').hide();
             //                 $('#btnImageBack').hide();
             //             }
             var subStr = lst.split('@');
             var List = '';
             $.each(subStr, function(index) {
                 List += '<tr>';
                 List += '<td><img src="' + aspxRootPath + subStr[index] + '" class="uploadImage" height="20px" width="30px"/></td>';
                 List += '<td><img class="imgDelete" src="' + aspxTemplateFolderPath + '/images/admin/btndelete.png" id="btn" onclick="ItemMangement.DeleteImage(this)" /></td>';
                 List += '</tr>';
             });
             if (lst != '' && lst != "undefined") {
                 $("#VariantsImagesTable>tbody").html('');
                 $("#VariantsImagesTable").show();
                 $("#VariantsImagesTable>tbody").append(List);
                 $("#VariantsImagesTable>tbody tr:even").addClass("cssClassAlternativeEven");
                 $("#VariantsImagesTable>tbody tr:odd").addClass("cssClassAlternativeOdd");
                 $('#btnSaveImages').show();
                 $('#btnImageBack').show();
             } else {
                 $("#VariantsImagesTable>tbody").html('');
                 $("#VariantsImagesTable").hide();
                 $('#btnSaveImages').hide();
                 $('#btnImageBack').hide();
             }
             $("#imageUploader").show();
             ShowPopupControl('popuprel2');
             ItemMangement.CostVariantsImageUploader(maxFileSize);
         },
         ajaxFailure: function(msg) {
             switch (ItemMangement.config.ajaxCallMode) {
                 case 1:
                     csscody.error('<h2>Error Message</h2><p>Failed to bind tax rules!</p>');
                     break;
                 case 2:
                     csscody.error('<h2>Error Message</h2><p>Failed to bind item quantity discount!</p>');
                     break;
                 case 3:
                     csscody.error('<h2>Error Message</h2><p>Failed to bind roles!</p>');
                     break;
                 case 4:
                     // alert("Failed to save Item Quantity Discount!");
                     csscody.error('<h2>Error Message</h2><p>Failed to save item quantity discount!</p>');
                     break;
                 case 5:
                     // alert("Failed to delete Item Quantity Discount!");
                     csscody.error('<h2>Error Message</h2><p>Failed to delete item quantity discount!!</p>');
                     break;
                 case 6:
                     //  alert("Failed to delete Item Cost Variant Value!");
                     csscody.error('<h2>Error Message</h2><p>Failed to delete item cost variant value!</p>');
                     break;
                 case 13:
                     // alert("Failed to save Item Cost Variant!");
                     csscody.error('<h2>Error Message</h2><p>Failed to save item cost variant!</p>');
                     break;
             }
         },
         ajaxSuccess: function(msg) {
             switch (ItemMangement.config.ajaxCallMode) {
                 case 1:
                     var option = '<option value=0>--select one--</option>';
                     $.each(msg.d, function(ind, item) {
                         option += '<option value="' + item.TaxManageRuleID + '">' + item.TaxManageRuleName + '</option>';
                     });
                     $("#ddlTax").append(option);
                     return true;
                     break;
                 case 2:
                     $("#tblQuantityDiscount>tbody").html('');
                     if (msg.d.length > 0) {
                         var arrItems = new Array();
                         $.each(msg.d, function(index, item) {
                             var newQuantityDiscountRow = '';
                             newQuantityDiscountRow += '<tr><td><input type="hidden" size="3" class="cssClassQuantityDiscount" value="' + item.QuantityDiscountID + '"><input type="text" size="3" class="cssClassQuantity" value="' + item.Quantity + '"></td>';
                             newQuantityDiscountRow += '<td><input type="text" size="5" class="cssClassPrice" value="' + item.Price + '"></td>';
                             newQuantityDiscountRow += '<td><div class="cssClassUsersInRoleCheckBox"></div></td>';
                             newQuantityDiscountRow += '<td><span class="nowrap">';
                             newQuantityDiscountRow += '<img width="13" height="18" border="0" align="top" class="cssClassAddDiscountRow" title="Add empty item" alt="Add empty item" name="add" src="' + aspxTemplateFolderPath + '/images/admin/icon_add.gif" >&nbsp;';
                             newQuantityDiscountRow += '<img width="13" height="18" border="0" align="top" class="cssClassCloneDiscountRow" alt="Clone this item" title="Clone this item" name="clone" src="' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif" >&nbsp;';
                             newQuantityDiscountRow += '<img width="12" height="18" border="0" align="top" class="cssClassDeleteDiscountRow" alt="Remove this item" name="remove" src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" >&nbsp;';
                             newQuantityDiscountRow += '</span></td></tr>';
                             $("#tblQuantityDiscount>tbody").append(newQuantityDiscountRow);
                             arrItems.push(item.RoleIDs);
                         });
                         ItemMangement.GetUserInRoleList(arrItems);
                     }
                     else {
                         var arrItems = new Array();
                         var newQuantityDiscountRow = '';
                         newQuantityDiscountRow += '<tr><td><input type="hidden" size="3" class="cssClassQuantityDiscount" value="0"><input type="text" size="3" class="cssClassQuantity"></td>';
                         newQuantityDiscountRow += '<td><input type="text" size="5" class="cssClassPrice"></td>';
                         newQuantityDiscountRow += '<td><div class="cssClassUsersInRoleCheckBox"></div></td>';
                         newQuantityDiscountRow += '<td><span class="nowrap">';
                         newQuantityDiscountRow += '<img width="13" height="18" border="0" align="top" class="cssClassAddDiscountRow" title="Add empty item" alt="Add empty item" name="add" src="' + aspxTemplateFolderPath + '/images/admin/icon_add.gif" >&nbsp;';
                         newQuantityDiscountRow += '<img width="13" height="18" border="0" align="top" class="cssClassCloneDiscountRow" alt="Clone this item" title="Clone this item" name="clone" src="' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif" >&nbsp;';
                         newQuantityDiscountRow += '<img width="12" height="18" border="0" align="top" class="cssClassDeleteDiscountRow" alt="Remove this item" name="remove" src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" >&nbsp;';
                         newQuantityDiscountRow += '</span></td></tr>';
                         $("#tblQuantityDiscount>tbody").append(newQuantityDiscountRow);
                         ItemMangement.GetUserInRoleList(arrItems);
                     }
                     $("#tblQuantityDiscount>tbody tr:even").addClass("cssClassAlternativeEven");
                     $("#tblQuantityDiscount>tbody tr:odd").addClass("cssClassAlternativeOdd");
                     $("#tblQuantityDiscount>tbody").find("tr:eq(0)").find("img.cssClassDeleteDiscountRow").hide();
                     $(".cssClassPrice,.cssClassQuantity,").DigitAndDecimal('.cssClassPrice,.cssClassQuantity', '');
                     $(".cssClassPrice,.cssClassQuantity").bind("contextmenu", function(e) {
                         return false;
                     });
                     break;
                 case 3:
                     $.each(msg.d, function(index, item) {
                         ItemMangement.BindRolesList(item);
                     });
                     arrRoles = ItemMangement.vars.arrRoles;
                     if (arrRoles.length > 0) {
                         var divData = $('div.cssClassUsersInRoleCheckBox');
                         $.each(divData, function(index, item) {
                             $.each(arrRoles, function(i) {
                                 if (i == index) {
                                     var arr = arrRoles[i].split(",");
                                     $.each(arr, function(j) {
                                         $(item).find("input[value=" + arr[j] + "]").attr("checked", "checked");
                                     });
                                 }
                             });
                         });
                     }
                     break;
                 case 4:
                     var item_Id = $("#ItemMgt_itemID").val();
                     ItemMangement.BindItemQuantityDiscountsByItemID(item_Id);
                     csscody.info("<h2>Successful Information</h2><p>Item discount quantity has been saved successfully</p>");
                     break;
                 case 5:
                     ItemMangement.vars.parentRow.remove();
                     csscody.info("<h2>Successful Information</h2><p>Item discount quantity  has been deleted successfully</p>");
                     return false;
                     break;
                 case 6:
                     ItemMangement.vars.parentRow.remove();
                     csscody.info("<h2>Successful Information</h2><p>Item cost variant value has been deleted successfully.</p>");
                     return false;
                     break;
                 case 7:
                     ItemMangement.FillForm(msg);
                     //variants Tab
                     ItemMangement.BindItemCostVariantValueByCostVariantID(ItemMangement.vars.itemCostVariantId, ItemMangement.vars.itemId, ItemMangement.vars.costVariantId);
                     //ItemMangement.HideAllVariantDivs();
                     $("#variantsGrid,#divExistingVariant").hide();
                     $("#newCostvariants,#divNewVariant").show();
                     //ShowPopupControl('popuprel');
                     break;
                 case 8:
                     if (msg.d.length > 0) {
                         $("#tblVariantTable>tbody").html('');
                         $.each(msg.d, function(index, item) {
                             listImages.push(item.ImagePath);
                             if (item.DisplayOrder == null) {
                                 item.DisplayOrder = '';
                             }
                             var newVariantRow = '';
                             newVariantRow += '<tr><td><input type="hidden" size="3" class="cssClassVariantValue" value="' + item.CostVariantsValueID + '"><input type="text" size="3" class="cssClassDisplayOrder" value="' + item.DisplayOrder + '"></td>';
                             newVariantRow += '<td><input type="text" class="cssClassItemCostVariantValueName" value="' + item.CostVariantsValueName + '"></td>';
                             newVariantRow += '<td><input type="text" size="5" class="cssClassPriceModifier" value="' + item.CostVariantsPriceValue + '">&nbsp;/&nbsp;';
                             newVariantRow += '<select class="cssClassPriceModifierType priceModifierType_' + item.CostVariantsValueID + '"><option value="false">$</option><option value="true">%</option></select></td>';
                             newVariantRow += '<td><input type="text" size="5" class="cssClassWeightModifier" value="' + item.CostVariantsWeightValue + '">&nbsp;/&nbsp;';
                             newVariantRow += '<select class="cssClassWeightModifierType weightModifierType_' + item.CostVariantsValueID + '"><option value="false">lbs</option><option value="true">%</option></select></td>';
                             newVariantRow += '<td><select class="cssClassIsActive isActive_' + item.CostVariantsValueID + '"><option value="true">Active</option><option value="false">Disabled</option></select></td>';
                             //newVariantRow += '<td><input type="text" size="5" class="cssClassQuantity" value="' + item.Quantity + '">';
                             newVariantRow += '<td><span class="nowrap">';
                             newVariantRow += '<img width="13" height="18" border="0" align="top" class="cssClassAddRow" title="Add empty item" alt="Add empty item" name="add" src="' + aspxTemplateFolderPath + '/images/admin/icon_add.gif" >&nbsp;';
                             newVariantRow += '<img width="13" height="18" border="0" align="top" class="cssClassCloneRow" alt="Clone this item" title="Clone this item" name="clone" src="' + aspxTemplateFolderPath + '/images/admin/icon_clone.gif" >&nbsp;';
                             newVariantRow += '<img width="12" height="18" border="0" align="top" class="cssClassDeleteRow" alt="Remove this item" name="remove" src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" >&nbsp;';
                             newVariantRow += '<button type="button" value=' + index + ' name="' + item.ImagePath + '" class="classAddImagesEdit" rel="popuprel2" onclick="ItemMangement.AddImages(this,' + index + ')" ><span><span>Add Images</span></span></button>';
                             newVariantRow += '</span></td></tr>';
                             $("#tblVariantTable>tbody").append(newVariantRow);
                             $('.priceModifierType_' + item.CostVariantsValueID).val('' + item.IsPriceInPercentage + '');
                             $('.weightModifierType_' + item.CostVariantsValueID).val('' + item.IsWeightInPercentage + '');
                             $('.isActive_' + item.CostVariantsValueID).val('' + item.IsActive + '');
                         });
                         $("#tblVariantTable>tbody").find("tr:eq(0)").find("img.cssClassDeleteRow").hide();
                     }
                     break;
                 case 9:
                     ItemMangement.BindCostVariantsOptions(itemId);
                     ItemMangement.BindItemCostVariantInGrid(itemId);
                     break;
                 case 10:
                     $.each(msg.d, function(index, item) {
                         ItemMangement.BindInputTypeDropDown(item);
                     });
                     break;
                 case 11:
                     if (msg.d.length > 0) {
                         $('#lblExistingOptions').html('Existing Cost Variant Options:');
                         $("select[id$=ddlExistingOptions] > option").remove();
                         $.each(msg.d, function(index, item) {
                             ItemMangement.BindCostVariantsDropDown(item);
                         });
                         $("#divExisitingDropDown").show();
                     }
                     else {
                         $("#ddlExistingOptions").hide();
                         $("#btnApplyExisingOption").hide();
                         $("#lblExistingOptions").html('There is no any existing cost variant options available!');
                         $('#btnExisingBack').show();
                     }
                     break;
                 case 12:
                     ItemMangement.vars.isUnique = msg.d;
                     break;
                 case 13:
                     csscody.info('<h2>Successful Message</h2><p>Item cost variant has been saved successfully.</p>');
                     ItemMangement.BindItemCostVariantInGrid(ItemMangement.vars.itemId);
                     $("#newCostvariants").hide();
                     $("#variantsGrid").show();
                     $('.classAddImagesEdit').removeAttr("name").removeAttr("onclick").removeClass("classAddImagesEdit").addClass("classAddImages");
                     break;
                 case 14:
                     csscody.info('<h2>Successful Message</h2><p>Selected item(s) has been deleted successfully.</p>');
                     ItemMangement.BindItemsGrid(null, null, null, null, null, null);
                     // AspxCommerce.Busy.LoadingHide();
                     break;
                 case 15:
                     csscody.info('<h2>Successful Message</h2><p>Item has been deleted successfully.</p>');
                     ItemMangement.BindItemsGrid(null, null, null, null, null, null);
                     $("#gdvItems_form").hide();
                     $("#gdvItems_accordin").hide();
                     $("#ItemMgt_itemID").val(0);
                     $("#gdvItems_grid").show();
                     // AspxCommerce.Busy.LoadingHide();
                     break;
                 case 16:
                     ItemMangement.BindItemsGrid(null, null, null, null, null, null);
                     AspxCommerce.Busy.LoadingHide();
                     break;
                 case 17:
                     ItemMangement.BindItemsGrid(null, null, null, null, null, null);
                     AspxCommerce.Busy.LoadingHide();
                     break;
                 case 18:
                     $("#ddlAttributeSet").get(0).options.length = 0;
                     $.each(msg.d, function(index, item) {
                         $("#ddlAttributeSet").get(0).options[$("#ddlAttributeSet").get(0).options.length] = new Option(item.AttributeSetName, item.AttributeSetID);
                         $("#ddlAttributeSetName").get(0).options[$("#ddlAttributeSetName").get(0).options.length] = new Option(item.AttributeSetName, item.AttributeSetID);
                     });
                     break;
                 case 19:
                     $('#ddlItemType').get(0).options.length = 0;
                     $.each(msg.d, function(index, item) {
                         $("#ddlItemType").get(0).options[$("#ddlItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                         $("#ddlSearchItemType").get(0).options[$("#ddlSearchItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                     });
                     break;
                 case 20:
                     maxFileSize = maxFileSize;
                     attributeSetId = ItemMangement.vars.attributeSetId;
                     itemTypeId = ItemMangement.vars.itemTypeId;
                     showDeleteBtn = ItemMangement.vars.showDeleteBtn;
                     itemId = ItemMangement.vars.itemId;
                     ItemMangement.CreateForm(msg.d, attributeSetId, itemTypeId, showDeleteBtn, itemId);
                     if (itemId > 0) {
                         ItemMangement.BindDataInAccordin(itemId, attributeSetId, itemTypeId);
                         ItemMangement.BindDataInImageTab(itemId);
                         ItemMangement.BindItemCostVariantInGrid(itemId);
                         ItemMangement.BindItemQuantityDiscountsByItemID(itemId);
                         if (itemTypeId == 2) {
                             ItemMangement.BindDownloadableForm(itemId);
                         }
                     }
                     if (itemTypeId == 2) {
                         //actual and sample file uploader                    
                         ItemMangement.SampleFileUploader(maxFileSize);
                         ItemMangement.ActualFileUploader(maxFileSize);
                     }
                     //Multiple Image Uploader
                     ItemMangement.ImageUploader(maxFileSize);
                     $("#gdvItems_grid").hide();
                     $("#gdvItems_form").hide();
                     $("#gdvItems_accordin").show();

                     $("#txtDownloadTitle").keypress(function(e) {
                         if (e.which == 37 || e.which == 44) {
                             return false;
                         }
                     });
                     $("#txtMaxDownload").keypress(function(e) {
                         if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                             return false;
                         }
                     });
                     $("#txtDownDisplayOrder").keypress(function(e) {
                         if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                             return false;
                         }
                     });

                     $('#txtMaxDownload').bind('paste', function(e) {
                         e.preventDefault();
                     });


                     $(".cssClassDisplayOrder").keypress(function(e) {
                         if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                             return false;
                         }
                     });

                     $(".cssClassPriceModifier").keypress(function(e) {
                         if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                             if (e.which == 45) { return true; }
                             return false;
                         }
                     });

                     $(".cssClassWeightModifier").keypress(function(e) {
                         if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                             if (e.which == 45) { return true; }
                             return false;
                         }
                     });
                     $(".cssClassWeightModifier").bind("contextmenu", function(e) {
                         return false;
                     });
                     $(".cssClassPriceModifier").bind("contextmenu", function(e) {
                         return false;
                     });

                     $(".cssClassSKU").keypress(function(e) {
                         if (e.which == 39 || e.which == 34) {
                             return false;
                         }
                     });
                     $("#btnSaveItemVariantOption").click(function() {
                         var variantsProperties = $("#tblVariantTable tr:gt(1)").find("input.cssClassDisplayOrder,input.cssClassVariantValueName,inpur.cssClassPriceModifier,input.cssClassWeightModifier,input.cssClassQuantity");
                         var count = 0;
                         var checkfristMsg = false;
                         $.each(variantsProperties, function(index, item) {
                             if ($(this).val() <= '') {
                                 csscody.alert("<h2>Information Alert</h2><p>Please enter item cost variant properties.</P");
                                 count++;
                                 checkfristMsg = true;
                                 return false;
                             }
                         });
                         var counter = 0;
                         if (checkfristMsg != true) {
                             $('#tblVariantTable>tbody tr').each(function() {
                                 if ($(this).find('inpur.cssClassPriceModifier,input.cssClassWeightModifier,input.cssClassQuantity').val() != '' && $(this).find('input.cssClassDisplayOrder,input.cssClassVariantValueName').val() == '') {
                                     csscody.alert("<h2>Information Alert</h2><p>Please enter item cost variant properties.</P");
                                     counter++;
                                     return false;
                                 }
                             });
                         }
                         if (count == 0 && counter == 0)
                             ItemMangement.SaveItemCostVariantsInfo()
                     });
                     $("#btnBackVariantOptions").bind("click", function() {
                         ItemMangement.OnInit();
                         $("#variantsGrid").show();
                         $("#newCostvariants").hide();
                         $('.classAddImages').removeAttr("name");
                         $('.classAddImagesEdit').removeAttr("name").removeAttr("onclick").removeClass("classAddImagesEdit").addClass("classAddImages");
                     });
                     $("#btnExisingBack").click(function() {
                         $("#variantsGrid").show();
                         $("#newCostvariants").hide();
                     });
                     $("#btnResetVariantOptions").click(function() {
                         ItemMangement.OnInit();
                         ItemMangement.ClearVariantForm();
                     });
                     $('#btnApplyExisingOption').click(function() {
                         var variant_Id = $('#ddlExistingOptions').val();
                         var item_Id = $("#ItemMgt_itemID").val();
                         if (variant_Id != null && item_Id != null) {
                             var params = { itemId: item_Id, costVariantID: variant_Id, storeId: storeId, portalId: portalId, cultureName: cultureName, userName: userName };
                             var mydata = JSON2.stringify(params);
                             $.ajax({
                                 type: "POST",
                                 url: aspxservicePath + "ASPXCommerceWebService.asmx/AddItemCostVariant",
                                 data: mydata,
                                 contentType: "application/json; charset=utf-8",
                                 dataType: "json",
                                 success: function() {
                                     $('#fade, #popuprel').fadeOut();
                                     ItemMangement.BindItemCostVariantInGrid(item_Id);
                                     ItemMangement.BindCostVariantsOptions(item_Id);
                                     csscody.info('<h2>Successful Message</h2><p>Item cost variant has been saved successfully.</p>');

                                 },
                                 error: function() {
                                     csscody.error('<h1>Error Message</h1><p>Failed to save item cost variant</p>');
                                 }
                             });
                         }
                     });
                     $("#txtDownDisplayOrder,#txtMaxDownload").bind("contextmenu", function(e) {
                         return false;
                     });

                     AspxCommerce.Busy.LoadingHide();
                     break;
                 case 21:
                     if (msg.d.length > 0) {
                         ItemMangement.BindToTable(msg);
                     }
                     break;
                 case 22:
                     $.each(msg.d, function(index, item) {
                         ItemMangement.FillDownlodableItemForm(msg);
                     });
                     break;
                 case 23:
                     $.each(msg.d, function(index, item) {
                         ItemMangement.FillItemAttributes(itemId, item);
                         if (index == 0) {
                             $('#ddlTax').val(item.ItemTaxRule);
                         }
                     });
                     break;
                 case 24:
                     ItemMangement.FillMultiSelect(msg);
                     break;
                 case 25:
                     ItemMangement.vars.isUnique = msg.d;
                     break;
                 case 26:
                     ItemMangement.vars.isUnique = msg.d;
                     ItemMangement.FillMultiSelect(msg);
                     break;
                 case 27:
                     //        var jEl = $("#divMessage");
                     //        jEl.html(result.d).fadeIn(1000);
                     //        setTimeout(function() { jEl.fadeOut(1000) }, 5000);    
                     $("#dynItemForm").html('');
                     $("#gdvItems_form").hide();
                     $("#gdvItems_accordin").hide();
                     ItemMangement.BindItemsGrid(null, null, null, null, null, null);
                     $("#gdvItems_grid").show();
                     AspxCommerce.Busy.LoadingHide();
                     if (itemEditFlag > 0) {
                         csscody.info('<h2>Successful Message</h2><p>Item has been updated successfully.</p>');
                     }
                     else {
                         csscody.info('<h2>Successful Message</h2><p>Item has been saved successfully.</p>');
                     }
                     ItemMangement.RemoveHtml();
                     //ItemMangement.CreateHtml();
                     //ItemMangement.CreateTableHeader();
                     break;
             }
         }
     }
     ItemMangement.init();
 });