 function getSelect() {
     var select = $('.ui-tree-selected', 'ul#categoryTree');
     if (select.length) return select;
     else return null;
 }

 function getLI(node) {
            node = node.length ? node : $(node);
            if (NodeName(node) == 'span') return node.parent();
            else if (NodeName(node) == 'ul') return node.parent();
            else return node;
    }
    
    function NodeName(node) {
            if (node.attr != undefined) {
                return (node.length ? node.attr('nodeName') : $(node).attr('nodeName')).toLowerCase();
            }
}
 var categoryMgmt ;
 
 $(function() {
     var arrTree = [];
     var storeId = AspxCommerce.utils.GetStoreID();
     var portalId = AspxCommerce.utils.GetPortalID();
     var userName = AspxCommerce.utils.GetUserName();
     var cultureName = AspxCommerce.utils.GetCultureName();
     var customerId = AspxCommerce.utils.GetCustomerID();
     var ip = AspxCommerce.utils.GetClientIP();
     var countryName = AspxCommerce.utils.GetAspxClientCoutry();
     var sessionCode = AspxCommerce.utils.GetSessionCode();
     var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();

     var treeHTML = '';
     var CategoryList = null;
     var catGroup;
     var DatePickerIDs = new Array();
     var FileUploaderIDs = new Array();
     var htmlEditorIDs = new Array();
     var from = ''; var to = '';
     var editorList = new Array();
     var isAlreadyClickAddSubCategory = true;
     var checkID = 0;
     categoryMgmt = {
         variables: {
             cateID: "",
             hdnCatNameTxtBox: "",
             isUnique: false
             // editorList: new Array()
         },
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
         ajaxCall: function() {
             $.ajax({
                 type: categoryMgmt.config.type,
                 contentType: categoryMgmt.config.contentType,
                 cache: categoryMgmt.config.cache,
                 async: categoryMgmt.config.async,
                 url: categoryMgmt.config.url,
                 data: categoryMgmt.config.data,
                 dataType: categoryMgmt.config.dataType,
                 success: categoryMgmt.ajaxSuccess,
                 error: categoryMgmt.ajaxFailure
             });
         },
         GetCategoryAll: function() {
             this.config.url = this.config.baseURL + "GetCategoryAll";
             this.config.data = JSON2.stringify({ isActive: true, storeID: storeId, portalID: portalId, userName: userName, culture: cultureName });
             this.config.ajaxCallMode = 1;
             this.ajaxCall(this.config);
         },
         BindTreeViewChild: function(CategoryID, CategoryName, ParentID, CategoryLevel, deepLevel) {
             deepLevel = deepLevel + 1;
             var hasChild = false;
             var html = '';
             $.each(CategoryList, function(index, item) {
                 if (item.CategoryLevel == CategoryLevel) {
                     if (item.ParentID == ParentID) {
                         html += '<li id="category_' + item.CategoryID + '" class="file-folder"><b>' + item.CategoryName + '</b>';
                         htmlChild = categoryMgmt.BindTreeViewChild(item.CategoryID, item.CategoryName, item.CategoryID, item.CategoryLevel + 1, deepLevel);
                         if (htmlChild != "") {
                             html += "<ul>" + htmlChild + "</ul>";
                         }
                         html += '</li>';
                     }
                 }
             });
             return html;
         },
         AddDragDrop: function() {
             $('#categoryTree').tree({
                 expand: '*',
                 //For Category management to have multiple level
                 //            // drop options: object or array of object
                 droppable: [
            		{
            		    element: 'li.ui-tree-node',
            		    tolerance: 'around',
            		    aroundTop: '25%',
            		    aroundBottom: '25%',
            		    aroundLeft: 0,
            		    aroundRight: 0
            		},
            		{
            		    element: 'li.ui-tree-list',
            		    tolerance: 'around',
            		    aroundTop: '25%',
            		    aroundBottom: '25%',
            		    aroundLeft: 0,
            		    aroundRight: 0
            		}
            	],
                 drop: function(event, ui) {
                     $('.ui-tree-droppable').removeClass('ui-tree-droppable ui-tree-droppable-top ui-tree-droppable-center ui-tree-droppable-bottom');
                     //debugger;
                     //alert(ui.target.getJSON(ui.droppable));
                     switch (ui.overState) {
                         case 'top':
                             ui.target.before(ui.sender.getJSON(ui.draggable), ui.droppable);
                             ui.sender.remove(ui.draggable);
                             //$(ui.droppable).parent('li').addClass('ui-tree-expanded');
                             break;

                         case 'bottom':
                             ui.target.after(ui.sender.getJSON(ui.draggable), ui.droppable);
                             ui.sender.remove(ui.draggable);
                             //$(ui.droppable).parent('li').addClass('ui-tree-expanded');
                             break;

                         case 'center':
                             ui.target.append(ui.sender.getJSON(ui.draggable), ui.droppable);
                             ui.sender.remove(ui.draggable);
                             $(ui.droppable).parent('li').addClass('ui-tree-expanded');
                             $(ui.droppable).parent('li').removeClass('ui-tree-list');
                             $(ui.droppable).parent('li').addClass('ui-tree-node');
                             break;
                     }
                 },
                 over: function(event, ui) {
                     $(ui.droppable).addClass('ui-tree-droppable');
                 },
                 out: function(event, ui) {
                     $(ui.droppable).removeClass('ui-tree-droppable');
                 },
                 overtop: function(event, ui) {
                     $(ui.droppable).addClass('ui-tree-droppable-top');
                 },
                 overcenter: function(event, ui) {
                     $(ui.droppable).addClass('ui-tree-droppable-center');
                 },
                 overbottom: function(event, ui) {
                     $(ui.droppable).addClass('ui-tree-droppable-bottom');
                 },
                 outtop: function(event, ui) {
                     $(ui.droppable).removeClass('ui-tree-droppable-top');
                 },
                 outcenter: function(event, ui) {
                     $(ui.droppable).removeClass('ui-tree-droppable-center');
                 },
                 outbottom: function(event, ui) {
                     $(ui.droppable).removeClass('ui-tree-droppable-bottom');
                 }
                 //                ,
                 //                click: function(event, ui) {
                 //                    categoryMgmt.ResetImageTab();
                 //                    var id = ui.draggable[0].id;
                 //                    id = ui.draggable[0].id.replace(/[^0-9]/gi, '');
                 //                    categoryMgmt.GetCategoryByCagetoryID(id);                   
                 //                }
             });
         },

         BindTreeCategory: function(data) {
             if (data.d.length > 0) {
                 var treeHTML = '';
                 CategoryList = data.d;
                 treeHTML += '<ul id="categoryTree">';
                 var deepLevel = 0;
                 $.each(CategoryList, function(i, item) {
                     if (item.CategoryLevel == 0) {
                         treeHTML += '<li id="category_' + item.CategoryID + '" class="file-folder"><b>' + item.CategoryName + '</b>';
                         htmlChild = categoryMgmt.BindTreeViewChild(item.CategoryID, item.CategoryName, item.CategoryID, item.CategoryLevel + 1, deepLevel);
                         if (htmlChild != "") {
                             treeHTML += "<ul>" + htmlChild + "</ul>";
                         }
                         treeHTML += "</li>";
                     }
                 });
                 treeHTML += '</ul>';
                 treeHTML += '<div class="cssClassButtonWrapper"><p><button type="button" id="btnCatTreeSave" onclick="categoryMgmt.SaveChangesCategoryTree()"><span><span>Save Changes</span></span></button></p></div>'
                 $("#CategoryTree_Container").html(treeHTML);
                 categoryMgmt.AddDragDrop();
                 $('#categoryTree li').live('click', function() {
                    //AspxCommerce.Busy.LoadingShow();
                     var selected = getSelect();
                     var li = getLI(selected);
                     var id = li.attr('id').replace( /[^0-9]/gi , '');
                     //categoryMgmt.ResetImageTab();
                     if (checkID != id) {
                         categoryMgmt.GetCategoryByCagetoryID(id);
                     }
                     checkID = id;
                 });
             }
             else {
                 $("#CategoryTree_Container").html("<span class=\"cssClassNotFound\">This store has no Category listed yet!</span>");
             }
         },
         GetCategoryByCagetoryID: function(catID) {
             categoryMgmt.variables.cateID = catID;
             isAlreadyClickAddSubCategory = false;
             $("#lblCategoryID").html(catID);
             $("#CagetoryMgt_categoryID").val(catID);

             this.config.url = this.config.baseURL + "GetCategoryByCategoryID";
             this.config.data = JSON2.stringify({ categoryID: catID, storeID: storeId, portalID: portalId, userName: userName, culture: cultureName });
             this.config.ajaxCallMode = 2;
             this.config.async = false;
             this.ajaxCall(this.config);
         },

         SelectFirstTab: function() {
             var $tabs = $('#CategorManagement_TabContainer').tabs({ fx: [null, { height: 'show', opacity: 'show'}] });
             $tabs.tabs('select', 0);
         },

         EditCategory: function(data) {
             $("#CategorManagement_TabContainer").find("input[type=reset]").click();
             $(".error").removeClass("error");
             $(".diverror").removeClass("diverror");
             $.each(data, function(index, item) {
                 if (index == 0) {
                     $("#CagetoryMgt_categoryID").val(item.CategoryID);
                     $("#CagetoryMgt_parentCagetoryID").val(item.ParentID);
                 }
                 categoryMgmt.FillCategoryForm(item);
             });

             categoryMgmt.SelectFirstTab();
         },

         FillCategoryForm: function(item) {
             if (item.CategoryLevel > 0) {
                 $("#" + categoryTitleLabel).html("Sub Category (ID:");
             }
             else {
                 $("#" + categoryTitleLabel).html("Category (ID:");
             }
             //item.AttributeID, item.AttributeName, item.InputTypeID, item.BooleanValue, item.IntValue, item.DateValue, item.DecimalValue, item.FileValue, item.OptionValues, item.ValidationTypeID, item.IsUnique, item.IsRequired
             var attNameNoSpace = "_" + item.AttributeName.replace(' ', '-');
             attNameNoSpace = '';
             var id = item.AttributeID + '_' + item.InputTypeID + '_' + item.ValidationTypeID + '_' + item.IsRequired + attNameNoSpace;
             var val;
             switch (item.InputTypeID) {
                 case 1: //TextField
                     $("#" + id).val(unescape(item.NvarcharValue));
                     $("#" + id).removeClass('hint');
                     break;
                 case 2: //TextArea
                     $("#" + id).val(item.TextValue);
                     for (var i = 0; i < editorList.length; i++) {
                         if (editorList[i].ID == id + "_editor") {
                             editorList[i].Editor.setData(Encoder.htmlDecode(item.TextValue));
                         }
                     }
                     $("#" + id).removeClass('hint');
                     break;
                 case 3: //Date
                     var test='new '+item.DateValue.replace(/[/]/gi, '');
                     date = eval(test);
                     $("#" + id).val(formatDate(date, "yyyy/MM/dd"));
                     $("#" + id).removeClass('hint');
                     break;
                 case 4: //Boolean
                     if (item.BooleanValue) {
                         $("#" + id).attr("checked", "checked");
                     }
                     else {
                         $("#" + id).removeAttr("checked");
                     }
                     break;
                 case 5: //MultipleSelect
                     val = item.OptionValues;
                     vals = val.split(',');
                     $.each(vals, function(i) {
                         $("#" + id + " option[value=" + vals[i] + "]").attr("selected", "selected");
                     });
                     break;
                 case 6: //DropDown
                     val = item.OptionValues;
                     vals = val.split(',');
                     $.each(vals, function(i) {
                         $("#" + id + " option[value=" + vals[i] + "]").attr("selected", "selected");
                     });
                     break;
                 case 7: //Price
                     $("#" + id).val(item.DecimalValue);
                     $("#" + id).removeClass('hint');
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
                             $(d).find('span.response').html('<div class="cssClassLeft"><img src="' + aspxRootPath + filePath + '" class="uploadImage" height="90px" width="100px" /></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="categoryMgmt.ClickToDeleteImage(this)" alt="Delete" title="Delete"/></div>');
                             // alert($(d).find('span.response').html());
                         }
                         else {
                             $(d).find('span.response').html('<div class="cssClassLeft"><a href="' + aspxRootPath + filePath + '" class="uploadFile" target="_blank">' + fileName + '</a></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="categoryMgmt.ClickToDeleteImage(this)" alt="Delete" title="Delete"/></div>');
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
                     $("#" + id).removeClass('hint');
                     break;
             }
         },

         ClickToDeleteImage: function(objImg) {
             $(objImg).closest('span').html('');
             return false;
         },

         BindCategoryItemsGrid: function(categoryId, itemSKU, itemName, itemPriceFrom, itemPriceTo) {
             this.config.url = this.config.baseURL;
             this.config.method = "GetCategoryItems";
             this.config.data = { categoryID: categoryId, sku: itemSKU, name: itemName, priceFrom: itemPriceFrom, priceTo: itemPriceTo, storeID: storeId, portalID: portalId, userName: userName, culture: cultureName };
             var categoryDataItemsGrid = this.config.data;
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvCategoryItems_pagesize").length > 0) ? $("#gdvCategoryItems_pagesize :selected").text() : 10;

             var isChecked = false;
             if (categoryId * 1 > 0) {
                 isChecked = true;
             }
             $("#gdvCategoryItems").sagegrid({
                 url: this.config.url,
                 functionMethod: this.config.method,
                 colModel: [
                        { display: 'ItemID', name: 'id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'categoryCheckBox', elemDefault: isChecked, controlclass: 'mainchkbox2' },
                        { display: 'Item ID', name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'center' },
				        { display: 'SKU', name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				        { display: 'Name', name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				        { display: 'Price', name: 'price', cssclass: 'cssClassHeadNumber', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' }
				      ],
                 txtClass: 'cssClassNormalTextBox',
                 rp: perpage,
                 nomsg: "No Records Found!",
                 param: categoryDataItemsGrid,
                 current: current_,
                 pnew: offset_,
                 sortcol: { 0: { sorter: false} }
             });
         },

         ResetImageTab: function() {
             var tabHeading = $(".ui-tabs-panel").find('div>.response');
             $.each(tabHeading, function(i) {
                 tabHeading.html('');
                 tabHeading.siblings('input').val('');
             });
         },

         GetFormFieldList: function() {
             this.config.url = this.config.baseURL + "GetCategoryFormAttributes";
             this.config.data = JSON2.stringify({ categoryID: 0, portalID: portalId, storeID: storeId, userName: userName, culture: cultureName });
             this.config.ajaxCallMode = 3;
             this.ajaxCall(this.config);
         },

         GetValidationTypeClasses: function(attValType, isUnique, isRequired) {
             var returnClass = '';
             if (isRequired == true) {
                 returnClass = "required";
             }
             return returnClass;
         },

         GetValidationTypeErrorMessage: function(attValType) {
             var retString = '';
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
                     retString = 'Price';
                     break;
                 case 7: //WebURL
                     retString = 'Web URL';
                     break;
             }
             return retString;
         },

         CreateValidation: function(id, attType, attValType, isUnique, isRequired) {
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

         CheckUniqueness: function(catName) {
             // Validate name

             var CatId = $("#CagetoryMgt_categoryID").val();
             var errors = '';
             catName = $.trim(catName);
             categoryMgmt.IsUnique(catName, CatId);
             var isUniqe = categoryMgmt.variables.isUnique;
             if (!catName) {
                 errors += 'Please enter Category Name';
                 $('.cssClassRight').hide();
                 $('.cssClassError').show();
                 $('.cssClassError').prevAll("input:first").addClass("error");
                 $('.cssClassError').html('Please enter unique category name.<br/>');
             }         //check uniqueness          
             else if (!isUniqe) {
                 errors += 'Please enter unique category name! "' + catName.trim() + '" already exists.<br/>';
                 $('.cssClassRight').hide();
                 $('.cssClassError').show();
                 $('.cssClassError').html('Please enter unique category name! "' + catName.trim() + '" already exists.<br/>');
                 $(".cssClassError").parent('div').addClass("diverror");
                 $('.cssClassError').prevAll("input:first").addClass("error");
             }

             if (errors) {
                 //  csscody.alert('<h2>Information Alert</h2><p>' + errors + '</p>');
                 return false;
             }
             else {
                 $('.cssClassRight').show();
                 $('.cssClassError').html('');
                 $('.cssClassError').hide();
                 $('.cssClassError').prevAll("input:first").removeClass("error");
                 return true;
             }
         },

         IsUnique: function(catName, CatId) {
             this.variables.isUnique = false;
             this.config.url = this.config.baseURL + "CheckUniqueCategoryName";
             this.config.data = JSON2.stringify({ catName: catName, catId: CatId, storeId: storeId, portalId: portalId, cultureName: cultureName });
             this.config.ajaxCallMode = 4;
             this.ajaxCall(this.config);
             return categoryMgmt.variables.isUnique;
         },
         //var hdnCatNameTxtBox="";
         createRow: function(attID, attName, attType, attTypeValue, attDefVal, attLen, attValType, isEditor, isUnique, isRequired, attToolTip) {
             var retString = '';
             var attNameNoSpace = "_" + attName.replace(new RegExp(" ", "g"), '-');
             attNameNoSpace = '';
             retString += '<tr><td class="cssClassTableLeftCol"><label class="cssClassLabel">' + attName + ': </label></td>';
             switch (attType) {
                 case 1: //TextField
                     if (attID == 1) {//Name of Category  

                         //hdnCatNameTxtBox = attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace;
                         this.variables.hdnCatNameTxtBox = attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace;
                         // alert( $("#hdnCatNameTxtBox").val());
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="text" maxlength="' + attLen + '"  class="cssClassNormalTextBox dynFormItem cssClassCategoryName' + categoryMgmt.CreateValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + '" onblur="categoryMgmt.CheckUniqueness(this.value)"/>'
                         retString += '<span class="cssClassRight"><img class="cssClassSuccessImg" height="13" width="18" alt="Right" src="' + aspxTemplateFolderPath + '/images/right.jpg"></span><b class="cssClassError">Ops! found something error, must be unique with no spaces</b>';
                         retString += '<span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     }
                     else {
                         retString += '<td class="cssClassTableRightCol"><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="text" maxlength="' + attLen + '"  class="cssClassNormalTextBox dynFormItem ' + categoryMgmt.CreateValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '" title="' + attToolTip + ' "/>'
                         retString += '<span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     }
                     break;
                 case 2: //TextArea                    
                     var editorDiv = '';
                     if (isEditor) {
                         htmlEditorIDs[htmlEditorIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + "_editor";
                         editorDiv = '<div id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '_editor"></div>';
                     }
                     retString += '<td><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><textarea id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" ' + ((isEditor == true) ? ' style="display: none !important;" ' : '') + ' rows="' + attLen + '"  class="cssClassTextArea dynFormItem ' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" title="' + attToolTip + '">' + attDefVal + '</textarea>' + editorDiv + '<span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 case 3: //Date
                     if (attID == 22 || attID == 23) {

                         switch (attID) {
                             case 22:
                                 DatePickerIDs[DatePickerIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace;
                                 retString += '<td class="cssClassBigBox"><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="text"  class="cssClassNormalTextBox dynFormItem activefrom' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /field --></p></div></td>';
                                 from = "#" + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace;
                                 break;
                             case 23:

                                 DatePickerIDs[DatePickerIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace;
                                 retString += '<td class="cssClassBigBox"><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="text"  class="cssClassNormalTextBox dynFormItem activeto' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /field --></p></div></td>';
                                 to = "#" + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace;
                                 break;

                         }
                         break;
                     }
                     else {

                         DatePickerIDs[DatePickerIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace;
                         retString += '<td class="cssClassBigBox"><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="text"  class="cssClassNormalTextBox dynFormItem ' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span><p><!-- /field --></p></div></td>';
                         break;
                     }
                 case 4: //Boolean
                     retString += '<td class="cssClassBigBox"><div class="cssClassCheckBox"><div class="field ' + (isRequired == true ? "required" : "") + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" value="1" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="checkbox"  class="cssClassCheckBox dynFormItem ' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></div></td>';
                     break;
                 case 5: //MultipleSelect
                     retString += '<td><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><select id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '"  title="' + attToolTip + '" size="' + attLen + '" multiple class="cssClassMultiSelect dynFormItem" >';
                     if (attTypeValue.length > 0) {
                         for (var i = 0; i < attTypeValue.length; i++) {
                             var val = attTypeValue[i];
                             //var vals = attTypeValue[i].split(':');
                             retString += '<option value="' + val.value + '">' + val.text + '</option>';
                         }
                     }
                     retString += '</select><span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 case 6: //DropDown
                     retString += '<td><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><select id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '"  title="' + attToolTip + '" class="cssClassDropDown dynFormItem" >';
                     var arr = new Array()
                     for (var i = 0; i < attTypeValue.length; i++) {
                         var val = attTypeValue[i];
                         retString += '<option value="' + val.value + '">' + val.text + '</option>';
                     }
                     retString += '</select><span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 case 7: //Price
                     retString += '<td class="cssClassBigBox"><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="text"  class="text dynFormItem ' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + attDefVal + '"  title="' + attToolTip + '"/><span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 case 8: //File                  
                     FileUploaderIDs[FileUploaderIDs.length] = attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace;
                     retString += '<td><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><div class="' + attDefVal + '" name="Upload/temp" lang="' + attLen + '"><input type="hidden" id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '_hidden" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '_hidden" value="" class="cssClassBrowse dynFormItem"/>';
                     retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="file" class="cssClassBrowse dynFormItem ' + categoryMgmt.CreateValidation(attID, attType, attValType, isUnique, isRequired) + '" title="' + attToolTip + '" />';
                     //retString += '<span id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '_span" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="file" class="cssClassBrowse">Browse</span>';
                     retString += ' <span class="response"></span></div><span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 case 9: //Radio
                     if (attDefVal) {
                         retString += '<td><div class="cssClassRadioBtn"><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" checked value="' + attID + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="radio"  class="text dynFormItem ' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + (attDefVal.toString().length > 0 ? attDefVal.toString() : "") + '"  title="' + attToolTip + '"/><label>' + attName + '</label><span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></div></td>';
                     }
                     else {
                         retString += '<td><div class="cssClassRadioBtn"><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" value="' + attID + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="radio"  class="text dynFormItem ' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + (attDefVal.toString().length > 0 ? attDefVal.toString() : "") + '"  title="' + attToolTip + '"/><label>' + attName + '</label><span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></div></td>';
                     }
                     break;
                 case 10: //RadioButtonList
                     retString += '<td><div class="cssClassRadioBtn"><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">'
                     for (var i = 0; i < attTypeValue.length; i++) {
                         var option = attTypeValue[i];
                         if (i == 0) {
                             retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '_' + i + '" value="' + attID + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="radio"  class="text dynFormItem ' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + option.value + '" checked /><label>' + option.text + '</label>';
                         }
                         else {
                             if (option.isDefault) {
                                 retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '_' + i + '" value="' + attID + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="radio"  class="text dynFormItem ' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + option.value + '" checked /><label>' + option.text + '</label>';
                             }
                             else {
                                 retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '_' + i + '" value="' + attID + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="radio"  class="text dynFormItem ' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + option.value + '" /><label>' + option.text + '</label>';
                             }
                         }
                     }
                     retString += '<span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></div></td>';
                     break;
                 case 11: //CheckBox
                     retString += '<td><div class="cssClassRadioBtn"><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="checkbox"  class="text dynFormItem ' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + attID + '"  /><label>' + attName + '</label><span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></div></td>';
                     break;
                 case 12: //CheckBoxList
                     retString += '<td><div class="cssClassRadioBtn"><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '">'
                     for (var i = 0; i < attTypeValue.length; i++) {
                         var option = attTypeValue[i];
                         if (option.isDefault) {
                             retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '_' + i + '" value="' + attID + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="checkbox"  class="text dynFormItem ' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + option.value + '" checked /><label>' + option.text + '</label>';
                         }
                         else {
                             retString += '<input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '_' + i + '" value="' + attID + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="checkbox"  class="text dynFormItem ' + categoryMgmt.CreateValidation(attID + attNameNoSpace, attType, attValType, isUnique, isRequired) + '" value="' + option.value + '" /><label>' + option.text + '</label>';
                         }
                     }
                     retString += '<span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></div></td>';
                     break;
                 case 13: //Password
                     retString += '<td class="cssClassBigBox"><div class="field ' + categoryMgmt.GetValidationTypeClasses(attValType, isUnique, isRequired) + '"><input id="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" name="' + attID + '_' + attType + '_' + attValType + '_' + isRequired + attNameNoSpace + '" type="text" maxlength="' + attLen + '"  class="text dynFormItem ' + categoryMgmt.CreateValidation(attID + '_' + attName, attType, attValType, isUnique, isRequired) + ' Password" value="' + attDefVal + '" title="*"/>'
                     retString += '<span class="iferror">' + categoryMgmt.GetValidationTypeErrorMessage(attValType) + '</span></div></td>';
                     break;
                 default:
                     break;
             }
             retString += '</tr>';
             return retString;
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
                 $("#" + DatePickerIDs[i]).datepicker({ dateFormat: 'yy/mm/dd' });
             }
         },

         CreateFileUploader: function(uploaderID) {
             d = $('#' + uploaderID).parent();
             baseLocation = d.attr("name");
             validExt = d.attr("class");
             maxFileSize = d.attr("lang");
             //alert(d.html());
             new AjaxUpload(String(uploaderID), {
                 action: aspxCatModulePath + "FileUploader.aspx",
                 name: 'myfile',
                 onSubmit: function(file, ext) {
                     var regExp = /\s+/g;
                     myregexp = new RegExp("(" + validExt.replace(regExp, "|") + ")", "i");
                     if (ext != "exe") {
                         if (ext && myregexp.test(ext)) {
                             this.setData({
                                 'BaseLocation': baseLocation, 'ValidExtension': validExt, 'MaxFileSize': maxFileSize
                             });
                         } else {
                             csscody.alert('<h2>Information Alert</h2><p>You are trying to upload invalid File!</p>');
                             return false;
                         }
                     }
                     else {
                         csscody.alert('<h2>Information Alert</h2><p>You are trying to upload invalid File!</p>');
                         return false;
                     }
                 },
                 onComplete: function(file, ajaxFileResponse) {
                     d = $('#' + uploaderID).parent();
                     var res = eval(ajaxFileResponse);
                     if (res.Message != null && res.Status > 0) {
                         baseLocation = d.attr("name");
                         validExt = d.attr("class");
                         var fileExt = (-1 !== file.indexOf('.')) ? file.replace(/.*[.]/, '') : '';
                         myregexp = new RegExp("(jpg|jpeg|jpe|gif|bmp|png|ico)", "i");
                         if (myregexp.test(fileExt)) {
                             $(d).find('span.response').html('<div class="cssClassLeft"><img src="' + aspxRootPath + res.UploadedPath + '" class="uploadImage" height="90px" width="100px" /></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="categoryMgmt.ClickToDeleteImage(this)" alt="Delete" title="Delete"/></div>');
                         }
                         else {
                             $(d).find('span.response').html('<div class="cssClassLeft"><a href="' + aspxRootPath + res.UploadedPath + '" class="uploadFile" target="_blank">' + file + '</a></div><div class="cssClassRight"><img src="' + aspxTemplateFolderPath + '/images/admin/icon_delete.gif" class="cssClassDelete" onclick="categoryMgmt.ClickToDeleteImage(this)" alt="Delete" title="Delete"/></div>');
                         }
                     }
                     else {
                         csscody.error('<h2>Error Message</h2><p>' + res.Message + '</p>');
                     }
                 }
             });
         },

         EnableFileUploaders: function() {
             for (var i = 0; i < FileUploaderIDs.length; i++) {
                 categoryMgmt.CreateFileUploader(String(FileUploaderIDs[i]));
             }
         },

         HTMLEditor: function(editorID, editorObject) {
             this.ID = editorID;
             this.Editor = editorObject;
         },
         // var editorList = new Array();
         EnableHTMLEditors: function() {
             for (var i = 0; i < htmlEditorIDs.length; i++) {
                 config = { skin: "v2" };
                 var html = "Initially Text if necessary";
                 var editor = CKEDITOR.replace(htmlEditorIDs[i], config, html);
                 var obj = new categoryMgmt.HTMLEditor(htmlEditorIDs[i], editor);
                 editorList[editorList.length] = obj;
             }
         },

         ResetHTMLEditors: function() {
             for (var i = 0; i < htmlEditorIDs.length; i++) {
                 editorList[i].Editor.setData('');
             }
         },

         SerializeForm: function(formID, remove) {
             jsonStr = '';
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
                             for (var i = 0; i <= radioGroups.length; i++) {
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
                             var imgToUpload = "";
                             if (img.attr("src") != undefined) {
                                 imgToUpload = img.attr("src");
                             }
                             if (img) {
                                 jsonStr += '{"name":"' + input.attr('name') + '","value":"' + imgToUpload.replace(aspxRootPath, "") + '"},';
                             }
                             else {
                                 var a = $(d).find('span.response a');
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
                             jsonStr += '{"name":"' + input.attr('name') + '","value":"' + $.trim(input.val()) + '"},';
                             break;
                         case 'text':
                             jsonStr += '{"name":"' + input.attr('name') + '","value":"' + $.trim(input.val()) + '"},';
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
                         chkValues += chkValues + $(this).val() + ",";
                     }
                 });
                 chkValues = chkValues.substr(0, chkValues.length - 1);
                 jsonStr += '{"name":"' + checkboxGroups[i] + '","value":"' + chkValues + '"},';
             }
             for (var i = 0; i < radioGroups.length; i++) {
                 var radValues = '';
                 radValues = $('input[name=' + radioGroups[i] + ']:radio').val();
                 radValues = radValues.substr(0, radValues.length - 1);
                 jsonStr += '{"name":"' + radioGroups[i] + '","value":"' + radValues + '"},';
             }
             jsonStr = jsonStr.substr(0, jsonStr.length - 1);
             return '[' + jsonStr + ']';
         },

         AddCategory: function() {
             $("#lblCategoryID").html(0);
             $("#" + categoryTitleLabel).html("Category (ID:");
             $("#CagetoryMgt_categoryID").val(0);
             $("#CagetoryMgt_parentCagetoryID").val(0);
             $("#CategorManagement_TabContainer").find("input[type=reset]").click();
             categoryMgmt.ResetHTMLEditors();
             categoryMgmt.ResetImageTab();
             categoryMgmt.SelectFirstTab();
             categoryMgmt.BindCategoryItemsGrid(0, '', '', null, null)
             $('#categoryReset').show();
         },

         AddSubCategory: function() {
             $("#lblCategoryID").html(0);
             if ($("#CagetoryMgt_categoryID").val() == 0) {
                 csscody.alert("<h2>Information Alert</h2><p>Please first select the category.</p>");
             }
             if (!isAlreadyClickAddSubCategory) {
                 isAlreadyClickAddSubCategory = true;
                 var ParentID = $("#CagetoryMgt_parentCagetoryID").val();
                 var CategoryID = $("#CagetoryMgt_categoryID").val();
                 $("#CategorManagement_TabContainer").find("input[type=reset]").click();
                 categoryMgmt.ResetHTMLEditors();
                 categoryMgmt.ResetImageTab();
                 $("#CagetoryMgt_categoryID").val(0);
                 $("#CagetoryMgt_parentCagetoryID").val(CategoryID);
             }
             $("#" + categoryTitleLabel).html("Sub Category (ID:");
             categoryMgmt.SelectFirstTab();
             categoryMgmt.BindCategoryItemsGrid(0, '', '', null, null)
             $('#categoryReset').show();
         },

         SaveCategory: function(formID) {
             AspxCommerce.Busy.LoadingShow();
             var catID = $("#CagetoryMgt_categoryID").val();
             var parID = $("#CagetoryMgt_parentCagetoryID").val();
             var item_ids = '';
             $("#gdvCategoryItems .categoryCheckBox").each(function(i) {
                 if ($(this).attr("checked")) {
                     item_ids += $(this).val() + ',';
                 }
             });
             if (item_ids.length > 0) {
                 item_ids = item_ids.substr(0, item_ids.length - 1);
             }

             this.config.url = this.config.baseURL + "SaveCategory";
             this.config.data = '{"storeID":"' + storeId + '", "portalID":"' + portalId + '", "categoryID":"' + catID + '","parentID":"' + parID + '","formVars":' + categoryMgmt.SerializeForm(formID) + ',"selectedItems" : "' + item_ids + '" , "userName" : "' + userName + '", "culture": "' + cultureName + '","categoryLargeThumbImage":"' + categoryLargeThumbImageSetting + '","categoryMediumThumbImage":"' + categoryMediumThumbImageSetting + '","categorySmallThumbImage":"' + categorySmallThumbImageSetting + '"}';
             this.config.ajaxCallMode = 5;
             this.ajaxCall(this.config);
         },

         saveCategorySuccess: function(result) {

             var res = eval(result.d);
             if (res.returnStatus > 0) {
                 $("#lblCategoryID").html(0);
                 $("#CagetoryMgt_categoryID").val(0);
                 $("#CagetoryMgt_parentCagetoryID").val(0);
                 $("#CategoryTree_Container").html('');
                 categoryMgmt.GetCategoryAll();
                 AspxCommerce.Busy.LoadingHide();
                 $("#CategorManagement_TabContainer").find("input[type=reset]").click();
                 categoryMgmt.ResetHTMLEditors();
                 categoryMgmt.ResetImageTab();
                 categoryMgmt.SelectFirstTab();
                 csscody.info('<h2>Successful Message</h2><p>' + res.Message + '</p>');
             }
             else {
                 csscody.error('<h2>Error Message</h2><p>' + res.ErrorMessage + '</p>');
             }
         },

         SaveChangesCategoryTree: function() {
             AspxCommerce.Busy.LoadingShow();
             arrTree = [];
             var saveString = $.toJSON(categoryMgmt.parseTree($("#categoryTree")));

             this.config.url = this.config.baseURL + "SaveChangesCategoryTree";
             this.config.data = '{"storeID":' + storeId + ', "portalID" : ' + portalId + ', "categoryIDs":' + saveString + ', "userName" : "' + userName + '"}';
             this.config.ajaxCallMode = 6;
             this.ajaxCall(this.config);
         },

         SaveChangesCategoryTreeSuccess: function(response) {
             AspxCommerce.Busy.LoadingHide();
             var res = eval(response.d);
             if (res.returnStatus > 0) {
                 csscody.info('<h2>Information Message</h2><p>' + res.Message + '</p>');
             }
             else {
                 csscody.error('<h2>Error Message</h2><p>' + res.errorMessage + '</p>');
             }
         },

         parseTree: function(ul) {
             var strChild = "";
             var saveString = "";
             ul.children("li").each(function() {
                 if ($(this).parents("li").length > 0) {
                     var strChild = $(this).attr("id").replace(/[^0-9]/gi, '');

                     var strcc = "";
                     $(this).parents("li").each(function() {
                         if (strcc == "") {
                             strcc = $(this).attr("id").replace(/[^0-9]/gi, '') + '/' + strcc + '/';
                         }
                         else {
                             strcc = $(this).attr("id").replace(/[^0-9]/gi, '') + '/' + strcc;
                         }
                     });
                     strcc = strcc.substr(0, strcc.length - 1);
                     strChild = '/' + strcc + strChild;
                 }
                 else {
                     strChild = '/' + $(this).attr("id").replace(/[^0-9]/gi, '');
                 }
                 arrTree.push(strChild);

                 var subtree = $(this).children("ul");
                 if (subtree.size() > 0)
                     categoryMgmt.parseTree(subtree);
             });
             return arrTree.join('#');
         },

         submitForm: function(frmID) {
             AspxCommerce.CheckSessionActive();
             if (AspxCommerce.vars.IsAlive) {
                 var frm = $("#" + frmID);
                 for (var i = 0; i < editorList.length; i++) {
                     var id = String(editorList[i].ID);
                     var textArea = $("#" + id.replace("_editor", ""));
                     textArea.val(Encoder.htmlEncode(editorList[i].Editor.getData()));
                 }
                 // Prevent submit if validation fails
                 var catNameTxtBoxID = categoryMgmt.variables.hdnCatNameTxtBox; //hdnCatNameTxtBox; //$("input["hidden"]#hdnCatNameTxtBox").val();
                 var CatName = $("#" + catNameTxtBoxID).val();
                 //alert(checkForm(frm) && CheckUniqueness($("#"+catNameTxtBoxID+"").val()));           
                 if (checkForm(frm) && categoryMgmt.CheckUniqueness(CatName)) { //$(".cssClassError").html().length == 0                              
                     categoryMgmt.SaveCategory("#" + frmID);
                 }
                 else {
                     var errorTabName = $("#CategorManagement_TabContainer").find('.diverror:first').parents('div').attr("id");
                     var $tabs = $('#CategorManagement_TabContainer').tabs();
                     $tabs.tabs('select', errorTabName);
                     return false;
                 }
             }
             else {
                 window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + 'Login.aspx';
             }
         },

         errorFn: function(xhr, status) {
             var err = null;
             if (xhr.readyState == 4) {
                 var res = xhr.responseText;
                 if (res && res.charAt(0) == '{' && status != "parsererror")
                     var err = JSON.parse(res);
                 if (!err) {
                     if (xhr.status && xhr.status != 200)
                         err = new CallbackException(xhr.status + " " + xhr.statusText);
                     else {
                         if (status == "parsererror")
                             status = "Unable to parse JSON response.";
                         else if (status == "timeout")
                             status = "Request timed out.";
                         else if (status == "error")
                             status = "Unknown error";
                         err = new CallbackException("Callback Error: " + status);
                     }
                     err.detail = res;
                 }
             }
             if (!err)
                 err = new CallbackException("Callback Error: " + status);
             csscody.error('<h2>Error Message</h2><p>Failed to save Category Tree!' + err + '</p>');
         },

         CreateTabPanel: function(attGroup) {
             var FormCount = new Array();
             if (FormCount) {
                 FormCount = new Array();
             }
             var FormID = "form_" + (FormCount.length * 10 + Math.floor(Math.random() * 10));
             FormCount[FormCount.length] = FormID;
             var dynHTML = '';
             var tabs = '';
             var tabBody = '';
             dynHTML += '<div class="cssClassTabPanelTable">';

             dynHTML += '<div id="CategorManagement_TabContainer" class="cssClassTabpanelContent">';
             dynHTML += '<ul>';
             for (var i = 0; i < attGroup.length; i++) {
                 tabs += '<li><a href="#CategoryTab-' + attGroup[i].key + '"><span>' + attGroup[i].value + '</span></a>';
                 tabBody += '<div id="CategoryTab-' + attGroup[i].key + '"><table width="100%" border="0" cellpadding="0" cellspacing="0">' + attGroup[i].html + '</table></div></li>';
             }
             tabs += '<li><a href="#CategoryTab-' + eval(attGroup.length + 1) + '"><span>Category Products</span></a>';

             tabBody += '<div id="CategoryTab-' + eval(attGroup.length + 1) + '">';

             tabBody += '<div class="cssClassCommonBox Curve">';
             tabBody += '<div class="cssClassGridWrapper"><div class="cssClassGridWrapperContent"><div id="ItemSearchPanel" class="cssClassSearchPanel cssClassFormWrapper"></div><div class="loading"><img src="' + aspxTemplateFolderPath + '/images/ajax-loader.gif" /></div><div class="log"></div>';
             tabBody += '<table id="gdvCategoryItems" cellspacing="0" cellpadding="0" border="0" width="100%"></table></div></div>';
             tabBody += '</div></div></li>';

             dynHTML += tabs;
             dynHTML += '</ul>';
             dynHTML += tabBody;
             var frmIDQuoted = "'" + FormID + "'";
             var buttons = '<div class="cssClassButtonWrapper"><p><button type="button" id="saveForm" onclick="categoryMgmt.submitForm(' + frmIDQuoted + ')"><span><span>Save</span></span></button> </p>';
             buttons += '<p><input id="categoryReset" type="reset" value="Reset" class="cssClassButtonSubmit" /></p><p><button type="button" onclick="categoryMgmt.DeleteCategory()" ><span><span>Delete</span></span></button></div><div class="cssClassClear"></div>';
             $("#dynForm").html('<div id="' + FormID + '">' + dynHTML + buttons + '</div>');

             $('#CategorManagement_TabContainer').tabs({ fx: [null, { height: 'show', opacity: 'show'}] });

             categoryMgmt.EnableFormValidation(FormID);
             categoryMgmt.EnableDatePickers();
             categoryMgmt.EnableFileUploaders();
             categoryMgmt.EnableHTMLEditors();

             var searchTable = '';
             searchTable += '<table cellspacing="0" cellpadding="0" border="0" width="100%"><tr>';
             searchTable += '<td><label class="cssClassLabel">SKU:</label><input type="text" id="txtCategoryItemSKU" name="txtCategoryItemSKU" class="cssClassTextBoxSmall"/></td>';
             searchTable += '<td><label class="cssClassLabel">Name:</label><input type="text" id="txtCategoryItemName" name="txtCategoryItemName" class="cssClassTextBoxSmall" /></td>';
             searchTable += '<td><label class="cssClassLabel">Price From:</label><input type="text" id="txtCategoryItemPriceFrom" name="txtCategoryItemPriceFrom" class="cssClassTextBoxSmall cssClassPrice" /></td>';
             searchTable += '<td><label class="cssClassLabel">Price To:</label><input type="text" id="txtCategoryItemPriceTo" name="txtCategoryItemPriceTo" class="cssClassTextBoxSmall cssClassPrice" /></td>';
             searchTable += '<td><div class="cssClassButtonWrapper cssClassPaddingNone"> <p><button type="button" onclick="categoryMgmt.SearchCategoryItems()"><span><span>Search</span></span></button></p></div></td></tr></table>';
             $("#ItemSearchPanel").html(searchTable);
             categoryMgmt.activatedatetimevalidation();
         },

         activatedatetimevalidation: function() {
             if (to != '') {
                 $(to).bind('change', function() {
                     if (Date.parse($(from).val()) <= Date.parse($(to).val())) {
                     }
                     else {
                         csscody.alert("<h2>Alert Message</h2><p>You must select active to date higher or equal to active from date!</p>");
                         $(to).val('');
                         return false;
                     }
                 });
             }
         },

         CreateForm: function(CategoryFormFields) {
             var strDynRow = '';
             var attGroup = new Array();

             $.each(CategoryFormFields, function(index, item) {
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

             $.each(CategoryFormFields, function(index, item) {
                 strDynRow = categoryMgmt.createRow(item.AttributeID, item.AttributeName, item.InputTypeID, item.InputTypeValues != "" ? eval(item.InputTypeValues) : '', item.DefaultValue, item.Length, item.ValidationTypeID, item.IsEnableEditor, item.IsUnique, item.IsRequired, item.ToolTip);
                 for (var i = 0; i < attGroup.length; i++) {
                     if (attGroup[i].key == item.GroupID) {
                         attGroup[i].html += strDynRow;
                     }
                 }
             });
             categoryMgmt.CreateTabPanel(attGroup);
               $('.cssClassCategoryName').keyup(function() {
                 if (this.value.match(/[^a-zA-Z0-9\-\&\_\'\ ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9\-\&\_\'\ ]/g, '');
                     return false;
                    // $('.charactererror').html('INvalid').css("color", "red").show().fadeOut(16000);
                 } 
                   else {
                     return true;
                 }
               });
               $('.cssClassCategoryName').bind('focusout',function() {
                 if (this.value.match(/[^a-zA-Z0-9\-\&\_\'\ ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9\-\&\_\'\ ]/g, '');
                     return false;
                 } 
                   else {
                     return true;
                 }
               });
             $('.cssClassRight').hide();
             $('.cssClassError').hide();
             $("#categoryReset").bind('click', function() {
                 categoryMgmt.ResetImageTab();
                 $('.error').removeClass("error");
                 $('.iferror').html('');
                 $('.cssClassRight').hide();
                 $('.cssClassError').hide();
                 $("#" + categoryTitleLabel).html("Category (ID:");
                 $('.required').find('.cke_skin_v2 iframe').each(function() {
                     $(this).contents().find("body").text('')
                 });
             });
         },

         DeleteCategoryItem: function(categoryID) {
             this.config.url = this.config.baseURL + "DeleteCategory";
             this.config.data = '{"storeID":' + storeId + ', "portalID" : ' + portalId + ', "categoryID":"' + categoryID + '", "userName" : "' + userName + '", "culture": "' + cultureName + '"}';
             this.config.ajaxCallMode = 7;
             this.ajaxCall(this.config);
         },

         DeleteCategory: function() {
             var categoryID = $("#CagetoryMgt_categoryID").val() * 1;
             if (categoryID > 0) {
                 var cofig = {
                     onComplete: function(e) {
                         if (e) {
                             categoryMgmt.DeleteCategoryItem(categoryID);
                         }
                     }
                 }
                 csscody.confirm("<h2>Confirmation Message</h2><p>Are you sure you want to delete this category?</p>", cofig);
             } else {
                 csscody.alert("<h2>Information Alert</h2><p>Please select category before delete.</p>");
             }
         },

         deleteCategorySuccess: function(response) {
             var res = eval(response.d);
             if (res.returnStatus > 0) {
                 $("#CagetoryMgt_categoryID").val(0);
                 $("#CagetoryMgt_parentCagetoryID").val(0);
                 $("#CategoryTree_Container").html('');
                 csscody.info('<h2>Information Message</h2><p>' + res.Message + '</p>');
                 categoryMgmt.GetCategoryAll();
                 $("#CategorManagement_TabContainer input[type=reset]").click();
                 categoryMgmt.BindCategoryItemsGrid(0, '', '', null, null);
                 categoryMgmt.ResetHTMLEditors();
                 categoryMgmt.SelectFirstTab();
             }
             else {
                 csscody.error('<h2>Error Message</h2><p>' + res.errorMessage + '</p>');
             }
         },

         SearchCategoryItems: function() {
             var searchCatID = $.trim($("#CagetoryMgt_categoryID").val());
             var sku = $.trim($("#txtCategoryItemSKU").val());
             var name = $.trim($("#txtCategoryItemName").val());
             var priceFrom = $.trim($("#txtCategoryItemPriceFrom").val());
             var priceTo = $.trim($("#txtCategoryItemPriceTo").val());
             //if(priceFrom.length > 0 && isNaN(priceFrom) && priceTo.length>0 && isNaN(
             if (priceFrom.length > 0) {
                 if (isNaN(priceFrom)) {
                     csscody.alert('<h2>Alert Message</h2><p>Invalid price! Price should be number..</p>');
                     return;
                 }
             }
             else {
                 priceFrom = null;
             }
             if (priceTo.length > 0) {
                 if (isNaN(priceTo)) {
                     csscody.alert('<h2>Alert Message</h2><p>Invalid price! Price should be number..</p>');
                     return;
                 }
             }
             else {
                 priceTo = null
             }
             if (parseInt(priceFrom, 10) > parseInt(priceTo, 10)) {
                 csscody.alert('<h2>Alert Message</h2><p>Invalid price range! Price From should be less than Price To</p>');
                 return false;
             }
             categoryMgmt.BindCategoryItemsGrid(searchCatID, sku, name, priceFrom, priceTo);
         },

         ajaxSuccess: function(data) {
             switch (categoryMgmt.config.ajaxCallMode) {
                 case 0:
                     break;
                 case 1:
                     categoryMgmt.BindTreeCategory(data);
                     break;
                 case 2:
                    // AspxCommerce.Busy.LoadingHide();
                     var searchTable = '';
                     searchTable += '<table cellspacing="0" cellpadding="0" border="0" width="100%"><tr>';
                     searchTable += '<td><label class="cssClassLabel">SKU:</label><input type="text" id="txtCategoryItemSKU" name="txtCategoryItemSKU" class="cssClassTextBoxSmall"/></td>';
                     searchTable += '<td><label class="cssClassLabel">Name:</label><input type="text" id="txtCategoryItemName" name="txtCategoryItemName" class="cssClassTextBoxSmall" /></td>';
                     searchTable += '<td><label class="cssClassLabel">Price From:</label><input type="text" id="txtCategoryItemPriceFrom" name="txtCategoryItemPriceFrom" class="cssClassTextBoxSmall" /></td>';
                     searchTable += '<td><label class="cssClassLabel">Price To:</label><input type="text" id="txtCategoryItemPriceTo" name="txtCategoryItemPriceTo" class="cssClassTextBoxSmall" /></td>';
                     searchTable += '<td><div class="cssClassButtonWrapper cssClassPaddingNone"> <p><button type="button" onclick="categoryMgmt.SearchCategoryItems()"><span><span>Search</span></span></button></p></div></td></tr></table>';
                     $("#ItemSearchPanel").html(searchTable);
                     for (var i = 0; i < editorList.length; i++) {
                         editorList[i].Editor.setData('');
                     }
                     categoryMgmt.EditCategory(data.d);
                     $('#categoryReset').hide();
                     categoryMgmt.BindCategoryItemsGrid(categoryMgmt.variables.cateID, '', '', null, null);
                     $("#gdvCategoryItems .categoryCheckBox").each(function(i) {
                         $(this).attr("checked", "checked");
                     });
                     break;
                 case 3:
                     categoryMgmt.CreateForm(data.d);
                     categoryMgmt.BindCategoryItemsGrid(0, '', '', null, null);
                     break;
                 case 4:
                     categoryMgmt.variables.isUnique = data.d;
                     //isUnique = data.d;
                     break;
                 case 5:
                     // save category
                     categoryMgmt.saveCategorySuccess(data);
                     break;
                 case 6:
                     //save category tree
                     categoryMgmt.SaveChangesCategoryTreeSuccess(data);
                     break;
                 case 7:
                     categoryMgmt.deleteCategorySuccess(data);
                     $("#lblCategoryID").html(0);
                     // $("#"+categoryTitleLabel).html("Category (ID:");                    
                     break;
             }
         },

         ajaxFailure: function(data) {
             switch (categoryMgmt.config.ajaxCallMode) {
                 case 0:
                     break;
                 case 1:
                     csscody.error('<h2>Error Message</h2><p>' + JSON2.stringify(err) + '</p>');
                     break;
                 case 2:
                     break;
                 case 3:
                     csscody.error('<h2>Error Message</h2><p> Error Occured !!</p>');
                     break;
                 case 4:
                     //check uniqueness                    
                     break;
                 case 5:
                     categoryMgmt.errorFn(data);
                     break;
                 case 6:
                     categoryMgmt.errorFn(data);
                     break;
                 case 7:
                     categoryMgmt.errorFn(data);
                     break;
             }
         },

         init: function() {
             categoryMgmt.GetCategoryAll();
             categoryMgmt.GetFormFieldList();
             $("#txtCategoryItemPriceFrom").DigitAndDecimal('.cssClassPrice', '');
             $("#txtCategoryItemPriceTo").DigitAndDecimal('.cssClassPrice', '');
             $("#txtCategoryItemPriceFrom,#txtCategoryItemPriceTo").bind("contextmenu", function(e) {
                 return false;
             });
             $(".activefrom,.activeto").bind("contextmenu", function(e) {
                 return false;
             });
             $('#txtCategoryItemPriceFrom,#txtCategoryItemPriceTo,.activefrom,.activeto').bind('paste', function(e) {
                         e.preventDefault();
                     });
             $('#txtCategoryItemSKU,#txtCategoryItemName,#txtCategoryItemPriceFrom,#txtCategoryItemPriceTo').keyup(function(event) {
                 if (event.keyCode == 13) {
                     categoryMgmt.SearchCategoryItems();
                 }
             });
         }
     };
     categoryMgmt.init();
 });