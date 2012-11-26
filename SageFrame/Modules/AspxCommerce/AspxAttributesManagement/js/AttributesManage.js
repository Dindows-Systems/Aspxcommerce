var attributesManage = '';
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var isUnique = false;
    var editFlag = 0;
    attributesManage = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCommerceWebService.asmx/",
            method: "",
            url: "",
            ajaxCallMode: 0
        },
        ajaxCall: function(config) {
            $.ajax({
                type: attributesManage.config.type,
                contentType: attributesManage.config.contentType,
                cache: attributesManage.config.cache,
                async: attributesManage.config.async,
                url: attributesManage.config.url,
                data: attributesManage.config.data,
                dataType: attributesManage.config.dataType,
                success: attributesManage.ajaxSuccess,
                error: attributesManage.ajaxFailure
            });
        },
        LoadAttributeStaticImage: function() {
            $('#ajaxAttributeImageLoader').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('.cssClassSuccessImg').attr('src', '' + aspxTemplateFolderPath + '/images/right.jpg');
        },
        ClearOptionTable: function(btnAddOption) {
            btnAddOption.closest("tr(0)").find("input:not(:last)").each(function(i) {
                $(this).val('');
                $(this).removeAttr('checked');
            });
        },
        onInit: function() {
            attributesManage.SetFirstTabActive();
            $("#ddlApplyTo").val('0');
            $('.itemTypes').hide();
            $('#btnReset').hide();
            $('.cssClassRight').hide();
            $('.cssClassError').hide();
            $("#lstItemType").each(function() {
                $("#lstItemType option").removeAttr("selected");
            });
            //$("#txtAttributeName").focus();
        },
        SetFirstTabActive: function() {
            var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show'}] });
            $tabs.tabs('select', 0);
        },
        Boolean: function(str) {
            switch (str) {
                case "1":
                    return true;
                case "0":
                    return false;
                default:
                    //throw new Error("Boolean.parse: Cannot convert string to boolean.");
            }
        },
        ClearForm: function() {
            attributesManage.onInit();
            $('#' + lblAttrFormHeading).html("New Item Attribute");
            $(".delbutton").removeAttr("id");
            $("#btnSaveAttribute").removeAttr("name");
            $('#' + lblLength).html("Length:");
            $(".delbutton").hide();
            $("#btnReset").show();
            $(".required:enabled").each(function() {

                if ($(this).parent("td").find("span.error").length == 1) {
                    $(this).removeClass("error").addClass("required");
                    $(this).parent("td").find("span.error").remove();
                }

            });
            $('#txtAttributeName').val('');
            $('#ddlAttributeType').val('1');
            $('#ddlAttributeType').removeAttr('disabled');

            $("#default_value_text").val('');
            $("#default_value_textarea").val('');
            $("#default_value_date").val('');
            $("#trdefaultValue").show();
            $("#default_value_text").show();
            $("#fileDefaultTooltip").html('');
            $("#fileDefaultTooltip").hide();
            $("#fileDefaultTooltip").html('');
            $("#default_value_textarea").hide();
            $("#div_default_value_date").hide();
            $("#default_value_yesno").hide();

            $('#default_value_text').val('');
            $("#dataTable tr:gt(1)").remove();
            attributesManage.ClearOptionTable($("input[type='button'].AddOption"));
            $('#trOptionsAdd').hide();

            $('#ddlTypeValidation').val('8');

            $('#ddlTypeValidation').removeAttr('disabled');
            $('#txtLength').val('');
            $('#txtLength').removeAttr('disabled');
            $('#txtAliasName').val('');
            $('#txtAliasToolTip').val('');
            $('#txtAliasHelp').val('');
            $('#txtDisplayOrder').val('');
            $('#ddlApplyTo').val('0');
            $('.itemTypes').hide();

            $('input[name=chkUniqueValue]').removeAttr('checked');
            $('input[name=chkValuesRequired]').removeAttr('checked');
            $('input[name=chkActive]').attr('checked', 'checked');
            $('#activeTR').show();

            //Next Tab
            $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
            //$('input[name=chkShowInSearch]').removeAttr('checked');
            //$('input[name=chkShowInGrid]').removeAttr('checked');
            $('input[name=chkUseInAdvancedSearch]').removeAttr('checked');
            $('input[name=chkComparable]').removeAttr('checked');
            $('input[name=chkUseForPriceRule]').removeAttr('checked');
            //$('input[name=chkUseForPromoRule]').removeAttr('checked');
            //$('input[name=chkIsEnableSorting]').removeAttr('checked');
            //$('input[name=chkIsUseInFilter]').removeAttr('checked');
            //$('input[name=chkUseForRating]').removeAttr('checked');


            return false;
        },
        BindAttributeOptionsValues: function(_fillOptionValues) {
            var _fillOptions = _fillOptionValues;
            if (_fillOptions != undefined && _fillOptions != "") {
                var arr = _fillOptions.split("!#!");
                var htmlContent = '';
                $.each(arr, function(i) {
                    var btnOption = "Add More";
                    var btnName = "AddMore";
                    if (i > 0) {
                        btnOption = "Delete Option";
                        var btnName = "DeleteOption";
                    }
                    var arr2 = arr[i].split("#!#");
                    var cloneRow = $('#dataTable tbody>tr:last').clone(true);
                    $(cloneRow).find("input").each(function(j) {
                        if (this.name == "value") {
                            $(this).val(arr2[0]);
                        } else if (this.name == "position") {
                            $(this).val(arr2[1]);
                        } else if (this.name == "Alias") {
                            $(this).val(arr2[2]);
                        } else if ($(this).hasClass("class-isdefault")) {
                            this.checked = attributesManage.Boolean(arr2[3]);
                        } else if ($(this).hasClass("AddOption")) {
                            $(this).attr("name", btnName);
                            $(this).attr("value", btnOption);
                        }
                    });
                    $(cloneRow).appendTo("#dataTable");
                });
                $('#dataTable>tbody tr:first').remove();
            }
        },
        ValidationTypeEnableDisable: function(fillOptionValues, isChanged) {
            var selectedVal = $("#ddlAttributeType :selected").val();
            switch (selectedVal) {
                case "1":
                    $("#ddlTypeValidation").removeAttr('disabled');
                    $('#' + lblDefaultValue).html("Default Value:");
                    $('#' + lblLength).html("Length:");
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#txtLength").removeAttr('disabled');
                    $("#trdefaultValue").show();
                    $("#default_value_text").show();
                    $("#fileDefaultTooltip").html('');
                    $("#fileDefaultTooltip").hide();
                    $("#default_value_textarea").hide();
                    $("#div_default_value_date").hide();
                    $("#default_value_yesno").hide();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
                    break;
                case "2":
                    $("#ddlTypeValidation").removeAttr('disabled');
                    $('#' + lblDefaultValue).html("Default Value:");
                    $('#' + lblLength).html("Rows:");
                    if (isChanged) {
                        $('#txtLength').val(3);
                    }
                    $("#txtLength").removeAttr('disabled');
                    $("#trdefaultValue").show();
                    $("#default_value_text").hide();
                    $("#fileDefaultTooltip").html('');
                    $("#fileDefaultTooltip").hide();
                    $("#default_value_textarea").show();
                    $("#div_default_value_date").hide();
                    $("#default_value_yesno").hide();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').removeAttr('disabled');
                    break;
                case "3":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblDefaultValue).html("Default Value:");
                    $('#' + lblLength).html("Length:");
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").attr('disabled', 'disabled');
                    $("#txtLength").attr('disabled', 'disabled');
                    $("#trdefaultValue").show();
                    $("#default_value_text").hide();
                    $("#fileDefaultTooltip").html('');
                    $("#fileDefaultTooltip").hide();
                    $("#default_value_textarea").hide();
                    $("#div_default_value_date").show();
                    $("#default_value_date").datepicker({ dateFormat: 'yy/mm/dd' });
                    $("#default_value_yesno").hide();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
                    break;
                case "4":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblDefaultValue).html("Default Value:");
                    $('#' + lblLength).html("Length:");
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").attr('disabled', 'disabled');
                    $("#txtLength").attr('disabled', 'disabled');
                    $("#trdefaultValue").show();
                    $("#default_value_text").hide();
                    $("#fileDefaultTooltip").html('');
                    $("#fileDefaultTooltip").hide();
                    $("#default_value_textarea").hide();
                    $("#div_default_value_date").hide();
                    $("#default_value_yesno").show();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
                    break;
                case "5":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblLength).html("Length:");
                    $("#ddlTypeValidation").attr('disabled', 'disabled');
                    $("#ddlTypeValidation").attr('disabled', 'disabled');
                    $('#' + lblLength).html("Size:");
                    $("#txtLength").removeAttr('disabled');
                    if (isChanged) {
                        $('#txtLength').val(3);
                    }
                    $("#trdefaultValue").hide();
                    $('#trOptionsAdd').show();
                    //$("input[name=defaultChk]").show();
                    //$("input[name=defaultRdo]").hide();
                    $("#tddefault").html('<input type=\"checkbox\" name=\"defaultChk\" class=\"class-isdefault\">');
                    $(".AddOption").show();
                    attributesManage.BindAttributeOptionsValues(fillOptionValues);
                    $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
                    break;
                case "6":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblLength).html("Length:");
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").attr('disabled', 'disabled');
                    $("#txtLength").attr('disabled', 'disabled');
                    $("#trdefaultValue").hide();
                    $('#trOptionsAdd').show();
                    //$("input[name=defaultChk]").hide();
                    //$("input[name=defaultRdo]").show();
                    $("#tddefault").html('<input type=\"radio\" name=\"defaultRdo\" class=\"class-isdefault\">');
                    $(".AddOption").show();
                    attributesManage.BindAttributeOptionsValues(fillOptionValues);
                    $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
                    break;
                case "7":
                    $('#ddlTypeValidation').val('6');
                    $('#' + lblDefaultValue).html("Default Value:");
                    $('#' + lblLength).html("Length:");
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").attr('disabled', 'disabled');
                    $("#txtLength").removeAttr('disabled');
                    $("#trdefaultValue").show();
                    $("#default_value_text").show();
                    $("#fileDefaultTooltip").html('');
                    $("#fileDefaultTooltip").hide();
                    $("#default_value_textarea").hide();
                    $("#div_default_value_date").hide();
                    $("#default_value_yesno").hide();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
                    break;
                case "8":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblDefaultValue).html("Allowed File Extension(s):");
                    $("#fileDefaultTooltip").html('- Separate each file extensions with space');
                    $("#fileDefaultTooltip").show();
                    $('#' + lblLength).html("Size:(KB)");
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").attr('disabled', 'disabled');
                    $("#txtLength").removeAttr('disabled');
                    $("#trdefaultValue").show();
                    $("#default_value_text").show();
                    $("#default_value_textarea").hide();
                    $("#div_default_value_date").hide();
                    $("#default_value_yesno").hide();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
                    break;
                case "9":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblLength).html("Length:");
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").attr('disabled', 'disabled');
                    $("#txtLength").attr('disabled', 'disabled');
                    $("#trdefaultValue").hide();
                    $('#trOptionsAdd').show();

                    //$("input[name=defaultChk]").hide();
                    //$("input[name=defaultRdo]").show();
                    $("#tddefault").html('<input type=\"radio\" name=\"defaultRdo\" class=\"class-isdefault\">');
                    $(".AddOption").hide();
                    attributesManage.BindAttributeOptionsValues(fillOptionValues);
                    $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
                    break;
                case "10":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblLength).html("Length:");
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").attr('disabled', 'disabled');
                    $("#txtLength").attr('disabled', 'disabled');
                    $("#trdefaultValue").hide();
                    $('#trOptionsAdd').show();
                    //$("input[name=defaultChk]").hide();
                    //$("input[name=defaultRdo]").show();
                    $("#tddefault").html('<input type=\"radio\" name=\"defaultRdo\" class=\"class-isdefault\">');
                    $(".AddOption").show();
                    attributesManage.BindAttributeOptionsValues(fillOptionValues);
                    $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
                    break;
                case "11":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblLength).html("Length:");
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").attr('disabled', 'disabled');
                    $("#txtLength").attr('disabled', 'disabled');
                    $("#trdefaultValue").hide();
                    $('#trOptionsAdd').show();
                    //$("input[name=defaultChk]").show();
                    //$("input[name=defaultRdo]").hide();
                    $("#tddefault").html('<input type=\"checkbox\" name=\"defaultChk\" class=\"class-isdefault\">');
                    $(".AddOption").hide();
                    attributesManage.BindAttributeOptionsValues(fillOptionValues);
                    $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
                    break;
                case "12":
                    $('#ddlTypeValidation').val('8');
                    $('#' + lblLength).html("Length:");
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#ddlTypeValidation").attr('disabled', 'disabled');
                    $("#txtLength").attr('disabled', 'disabled');
                    $("#trdefaultValue").hide();
                    $('#trOptionsAdd').show();
                    //$("input[name=defaultChk]").show();
                    //$("input[name=defaultRdo]").hide();
                    $("#tddefault").html('<input type=\"checkbox\" name=\"defaultChk\" class=\"class-isdefault\">');
                    $(".AddOption").show();
                    attributesManage.BindAttributeOptionsValues(fillOptionValues);
                    $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
                    break;
                case "13":
                    $("#ddlTypeValidation").removeAttr('disabled');
                    $('#' + lblDefaultValue).html("Default Value:");
                    $('#' + lblLength).html("Length:");
                    if (isChanged) {
                        $('#txtLength').val('');
                    }
                    $("#txtLength").removeAttr('disabled');
                    $("#trdefaultValue").show();
                    $("#default_value_text").show();
                    $("#fileDefaultTooltip").html('');
                    $("#fileDefaultTooltip").hide();
                    $("#default_value_textarea").hide();
                    $("#div_default_value_date").hide();
                    $("#default_value_yesno").hide();
                    $('#trOptionsAdd').hide();
                    $('input[name=chkIsEnableEditor]').attr('disabled', 'disabled');
                    break;
                default:
                    break;
            }
        },

        BindAttributeGrid: function(attributeNm, required, SearchComparable, isSystem) {
            this.config.url = this.config.baseURL;
            this.config.method = "GetAttributesList";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvAttributes_pagesize").length > 0) ? $("#gdvAttributes_pagesize :selected").text() : 10;

            $("#gdvAttributes").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'Attribute ID', name: 'attr_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', checkFor: '5', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
                    { display: 'Attribute Name', name: 'attr_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Attribute Alias', name: 'attr_alias', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'IsRequired', name: 'IsRequired', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: 'IsActive', name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: 'System', name: 'IsSystemUsed', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: 'Searchable', name: 'ShowInSearch', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true },
                    { display: 'Used In Configurable Item', name: 'IsUsedInConfigItem:', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true },
                    { display: 'Comparable', name: 'ShowInComparison', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: 'AddedOn', name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                //{ display: 'UpdatedOn', name: 'UpdatedOn', cssclass: 'cssClassHeadDate', controlclass:'',coltype: 'label', align: 'left', type: 'date', format: 'dd/MM/yyyy' },
                    {display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [{ display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'attributesManage.EditAttributes', arguments: '1,2,3,4,5,6,7' },
                    { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'attributesManage.DeleteAttributes', arguments: '5' },
                //{ display: 'View', name: 'view', enable: true, _event: 'click', trigger: '3', callMethod: 'ViewAttributes' },
                    {display: 'Active', name: 'active', enable: true, _event: 'click', trigger: '4', callMethod: 'attributesManage.ActiveAttributes', arguments: '5' },
                    { display: 'Deactive', name: 'deactive', enable: true, _event: 'click', trigger: '5', callMethod: 'attributesManage.DeactiveAttributes', arguments: '5' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { attributeName: attributeNm, isRequired: required, comparable: SearchComparable, isSystem: isSystem, storeId: storeId, portalId: portalId, cultureName: cultureName, userName: userName },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 10: { sorter: false} }
            });
        },
        FillDefaultValue: function(defaultVal) {
            var selectedAttributeType = $("#ddlAttributeType :selected").val();
            switch (selectedAttributeType) {
                case "1":
                    $('#default_value_text').val(defaultVal);
                    break;
                case "2":
                    $('textarea#default_value_textarea').val(defaultVal);
                    break;
                case "3":
                    //var formattedDate = formatDate(new Date(DateDeserialize(defaultVal)), "yyyy/M/d");
                    $('#default_value_date').val(defaultVal);
                    break;
                case "4":
                    $('#default_value_yesno').val(defaultVal);
                    break;
                case "8":
                    $('#default_value_text').val(defaultVal);
                    break;
                default:
                    break;
            }
        },
        FillForm: function(response) {
            $.each(response.d, function(index, item) {

                $('#txtAttributeName').val(item.AttributeName);
                $('#ddlAttributeType').val(item.InputTypeID);
                $('#ddlAttributeType').attr('disabled', 'disabled');

                attributesManage.FillDefaultValue(item.DefaultValue);
                //$('#txtDefaultValue').val(item.DefaultValue);

                $('#ddlTypeValidation').val(item.ValidationTypeID);
                $('#txtLength').val(item.Length);
                $('#txtAliasName').val(item.AliasName);
                $('#txtAliasToolTip').val(item.AliasToolTip);
                $('#txtAliasHelp').val(item.AliasHelp);
                $('#txtDisplayOrder').val(item.DisplayOrder);

                $('input[name=chkUniqueValue]').attr('checked', item.IsUnique);
                $('input[name=chkValuesRequired]').attr('checked', item.IsRequired);
                $('input[name=chkActive]').attr('checked', item.IsActive);

                //Next Tab
                $('input[name=chkIsEnableEditor]').attr('checked', item.IsEnableEditor);
                //$('input[name=chkShowInSearch]').attr('checked', item.ShowInSearch);
                //$('input[name=chkShowInGrid]').attr('checked', item.ShowInGrid);
                $('input[name=chkUseInAdvancedSearch]').attr('checked', item.ShowInAdvanceSearch);
                //$('input[name=chkVisibleOnFrontend]').attr('checked', item.ShowInGrid);
                //$('input[name=chkUsedInItemListing]').attr('checked', item.IsActive);
                //chkComparable chkUseForPriceRule chkUseForPromoRule chkUseForRating
                $('input[name=chkComparable]').attr('checked', item.ShowInComparison);
                $('input[name=chkUseForPriceRule]').attr('checked', item.IsIncludeInPriceRule);
                //$('input[name=chkUseForPromoRule]').attr('checked', item.IsIncludeInPromotions);
                //$('input[name=chkIsEnableSorting]').attr('checked', item.IsEnableSorting);
                //$('input[name=chkIsUseInFilter]').attr('checked', item.IsUseInFilter);
                //$('input[name=chkUseForRating]').attr('checked', item.IsShownInRating);

                attributesManage.ValidationTypeEnableDisable(item.FillOptionValues, false);

                //BindAttributeOptionsValues(item.FillOptionValues);

                if (item.ItemTypes.length > 0) {
                    $('#ddlApplyTo').val('1');
                    $('.itemTypes').show();
                    var itemsType = item.ItemTypes;
                    var arr = itemsType.split(",");
                    $.each(arr, function(i) {
                        $("#lstItemType option[value=" + arr[i] + "]").attr("selected", "selected");
                    });
                } else {
                    $('#ddlApplyTo').val('0');
                }
            });
        },
        EditAttributes: function(tblID, argus) {           
            attributesManage.ClearForm();
            switch (tblID) {
                case "gdvAttributes":
                    if (argus[7].toLowerCase() != "yes") {
                        $('#' + lblAttrFormHeading).html("Edit Item Attribute: '" + argus[3] + "'");
                        if (argus[7].toLowerCase() != "yes") {
                            $(".delbutton").attr("id", 'attributeid' + argus[0]);
                            $(".delbutton").show();
                            $('#activeTR').show();
                        } else {
                            $(".delbutton").hide();
                            $('#activeTR').hide();
                        }
                        $("#btnSaveAttribute").attr("name", argus[0]);
                        attributesManage.onInit();
                        //GlobalClearGrid('gdvAttributes');
                        //offset_ = argus[1];
                        //current_ = argus[2];

                        attributesManage.config.url = attributesManage.config.baseURL + "GetAttributeDetailsByAttributeID";
                        attributesManage.config.data = JSON2.stringify({ attributeId: argus[0], storeId: storeId, portalId: portalId, userName: userName });
                        attributesManage.config.ajaxCallMode = 4;
                        attributesManage.ajaxCall(attributesManage.config);
                    } else {
                        csscody.alert('<h2>Information Alert</h2><p>Sorry! System attribute can not be updated.</p>');
                    }
                    break;
                default:
                    break;
            }
        },
        DateDeserialize: function(dateStr) {
            return eval('new' + dateStr.replace(/\//g, ' '));
        },

        DeleteAttributes: function(tblID, argus) {
            switch (tblID) {
                case "gdvAttributes":
                    if (argus[3].toLowerCase() != "yes") {
                        attributesManage.DeleteAttribute(argus[0], storeId, portalId, userName);
                    } else {
                        csscody.alert('<h2>Information Alert</h2><p>Sorry! System attribute can not be deleted.</p>');
                    }
                    break;
                default:
                    break;
            }
        },
        ConfirmDeleteMultiple: function(attribute_ids, event) {
            if (event) {
                attributesManage.DeleteMultipleAttribute(attribute_ids, storeId, portalId, userName);
            }
        },

        DeleteMultipleAttribute: function(_attributeIds, _storeId, _portalId, _userName) {
            //Pass the selected attribute id and other parameters
            this.config.url = this.config.baseURL + "DeleteMultipleAttributesByAttributeID";
            this.config.data = JSON2.stringify({ attributeIds: _attributeIds, storeId: _storeId, portalId: _portalId, userName: _userName });
            this.config.ajaxCallMode = 6;
            this.ajaxCall(this.config);
            return false;
        },

        DeleteAttribute: function(_attributeId, _storeId, _portalId, _userName) {
            var properties = {
                onComplete: function(e) {
                    attributesManage.ConfirmSingleDelete(_attributeId, _storeId, _portalId, _userName, e);
                }
            };
            // Ask user's confirmation before delete records        
            csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this attribute?</p>", properties);
        },

        ConfirmSingleDelete: function(attribute_id, _storeId, _portalId, _userName, event) {
            if (event) {
                attributesManage.DeleteSingleAttribute(attribute_id, _storeId, _portalId, _userName);
            }
            return false;
        },

        DeleteSingleAttribute: function(_attributeId, _storeId, _portalId, _userName) {
            //Pass the selected attribute id and other parameters
            this.config.url = this.config.baseURL + "DeleteAttributeByAttributeID";
            this.config.data = JSON2.stringify({ attributeId: parseInt(_attributeId), storeId: _storeId, portalId: _portalId, userName: _userName });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
        },

        ActivateAttribute: function(_attributeId, _storeId, _portalId, _userName, _isActive) {
            //Pass the selected attribute id and other parameters
            this.config.url = this.config.baseURL + "UpdateAttributeIsActiveByAttributeID";
            this.config.data = JSON2.stringify({ attributeId: parseInt(_attributeId), storeId: _storeId, portalId: _portalId, userName: _userName, isActive: _isActive });
            this.config.ajaxCallMode = 7;
            this.ajaxCall(this.config);
            return false;
        },
        DeactiveAttributes: function(tblID, argus) {
            switch (tblID) {
                case "gdvAttributes":
                    if (argus[3].toLowerCase() != "yes") {
                        attributesManage.ActivateAttribute(argus[0], storeId, portalId, userName, false);
                    } else {
                        csscody.alert('<h2>Information Alert</h2><p>Sorry! System attribute can not be deactivated.</p>');
                    }
                    break;
                default:
                    break;
            }
        },
        ActiveAttributes: function(tblID, argus) {
            switch (tblID) {
                case "gdvAttributes":
                    if (argus[3].toLowerCase() != "yes") {
                        attributesManage.ActivateAttribute(argus[0], storeId, portalId, userName, true);
                    } else {
                        csscody.alert('<h2>Information Alert</h2><p>Sorry! System attribute can not be activated.</p>');
                    }
                    break;
                default:
                    break;
            }
        },
        IsUnique: function(attributeName, attributeId) {
            //  var isUnique = false;
            this.config.url = this.config.baseURL + "CheckUniqueAttributeName";
            this.config.data = JSON2.stringify({ attributeName: attributeName, attributeId: attributeId, storeId: storeId, portalId: portalId, cultureName: cultureName });
            this.config.ajaxCallMode = 8;
            this.ajaxCall(this.config);
            return isUnique;
        },
        SaveAttribute: function(_attributeId, _storeId, _portalId, _userName, _cultureName, _flag) {
            var selectedItemTypeID = '';
            var validateErrorMessage = '';
            var itemSelected = false;
            var isUsedInConfigItem = false;

            // Validate name
            var attributeName = $('#txtAttributeName').val();
            if (!attributeName) {
                validateErrorMessage += 'Please enter attribute name.<br/>';
            } else if (!attributesManage.IsUnique(attributeName, _attributeId)) {
                validateErrorMessage += 'Please enter unique attribute name. "' + attributeName.trim() + '" already exists.<br/>';
            }
            //Validate ddlApplyTo and lstItemType selected at least one item
            var selectedValue = $("#ddlApplyTo").val();
            if (selectedValue !== "0") {
                $("#lstItemType").each(function() {
                    if ($("#lstItemType :selected").length != 0) {
                        itemSelected = true;
                        $("#lstItemType option:selected").each(function(i) {
                            //alert($(this).text() + " : " + $(this).val());
                            selectedItemTypeID += $(this).val() + ',';
                            if ($(this).val() == '3') {
                                isUsedInConfigItem = true;
                            }
                        });
                    }
                });
                if (!itemSelected) {
                    validateErrorMessage += 'Please select at least one item type.<br/>';
                }
            } else {
                isUsedInConfigItem = true;
                $("#lstItemType option").each(function(i) {
                    selectedItemTypeID += $(this).val() + ',';
                });
            }

            selectedItemTypeID = selectedItemTypeID.substring(0, selectedItemTypeID.length - 1);

            // Validate attribute max length
            if ($('#toggleElement').is(':checked'))
                var _Length = '';
            if (!($('#txtLength').is(':disabled'))) {
                _Length = $('#txtLength').val();
                if (_Length) {
                    var value = _Length.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
                    var intRegex = /^\d+$/;
                    if (!intRegex.test(value)) {
                        $("#txtLength").focus();
                        validateErrorMessage += 'Attribute max length must be numeric only.<br/>';
                    }
                } else {
                    _Length = null;
                    validateErrorMessage += 'Please enter attribute Length.<br/>';
                }
            }

            // Validate attribute Display Order
            var attributeDisplayOrder = $('#txtDisplayOrder').val(); // $("#").val();
            if (!attributeDisplayOrder) {
                //   $("#txtDisplayOrder").focus();

                validateErrorMessage += 'Please enter attribute display order.<br/>';
            } else {
                var value = attributeDisplayOrder.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
                var intRegex = /^\d+$/;
                if (!intRegex.test(value)) {
                    //   $("#txtDisplayOrder").focus();                  
                    validateErrorMessage += 'Attribute display order must be numeric only.<br/>';
                }
            }

            // Validate attribute alias name
            var attributeDescription = $('#txtAliasName').val(); // $("#txtAliasName").val();
            if (!attributeDescription) {
                // $("#txtAliasName").focus();
                validateErrorMessage += 'Please enter attribute alias name.';
            }

            // Validate options value inputs filled
            var selectedVal = $("#ddlAttributeType :selected").val();
            var _saveOptions = '';
            if (selectedVal == 5 || selectedVal == 6 || selectedVal == 9 || selectedVal == 10 || selectedVal == 11 || selectedVal == 12) {
                $("#dataTable").find("tr input").each(function(i) {
                    //if ($(this).is(":visible")) {
                    //  if (!$(this).attr('name', 'Alias')) {
                    var optionsText = $(this).val();
                    if ($(this).hasClass("class-text")) {
                        if (!optionsText && $(this).attr("name") != "Alias") {
                            validateErrorMessage = 'Please enter all option values and display order for your attribute.<br/>';
                            SetFirstTabActive();
                            $(this).focus();
                        } else {
                            if ($(this).attr("name") == "position") {
                                var value = optionsText.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
                                var intRegex = /^\d+$/;
                                if (!intRegex.test(value)) {
                                    validateErrorMessage = 'Display order is numeric value.<br/>';
                                    SetFirstTabActive();
                                    $(this).focus();
                                }
                            }
                            _saveOptions += optionsText + "#!#";
                        }
                    } else if ($(this).hasClass("class-isdefault")) { //&& $(this).is(":visible") && $("#container-7 ul li:first").hasClass("ui-tabs-selected")) {
                        var _IsChecked = $(this).attr('checked');
                        _saveOptions += _IsChecked + "!#!";
                    }
                    // }
                    //}           
                });
            }
            _saveOptions = _saveOptions.substring(0, _saveOptions.length - 3);
            if (validateErrorMessage) {

                $(".required:visible:enabled").each(function() {
                    if ($(this).attr("id") == "txtAttributeName") {
                        $(this).parent("td").find("span.error").length == 0 ? $(this).addClass("error").parent("td").append("<span class='error' style='color:#FF0000;'>*</span>") : $(this).parent("td").find("span.error").html('*');
                    }
                    if ($(this).attr("id") == "txtLength") {
                        var _Length = $.trim($(this).val());
                        if (_Length) {
                            var value = _Length.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
                            var intRegex = /^\d+$/;
                            if (!intRegex.test(value)) {
                                $(this).parent("td").find("span.error").length == 0 ? $(this).addClass("error").parent("td").append("<span class='error' style='color:#FF0000;'> numeric only.</span>") : $(this).parent("td").find("span.error").html("<span class='error' style='color:#FF0000;'> numeric only.</span>").show();

                            } else {
                                $(this).removeClass("error").parent("td").find("span.error").hide();

                            }
                        } else {
                            $(this).parent("td").find("span.error").length == 0 ? $(this).addClass("error").parent("td").append("<span class='error' style='color:#FF0000;'>*</span>") : $(this).parent("td").find("span.error").html('*').show();

                        }
                    }
                    if ($(this).attr("id") == "txtAliasName") {
                        var _Length = $.trim($(this).val());
                        if (_Length) {
                            $(this).removeClass("error").parent("td").find("span.error").hide();
                        } else {
                            $(this).parent("td").find("span.error").length == 0 ? $(this).addClass("error").parent("td").append("<span class='error' style='color:#FF0000;'>*</span>") : $(this).parent("td").find("span.error").html('*').show();

                        }
                    }
                    if ($(this).attr("id") == "txtDisplayOrder") {
                        var _Length = $.trim($(this).val());
                        if (_Length != "") {
                            var value = _Length.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
                            var intRegex = /^\d+$/;
                            if (!intRegex.test(value)) {
                                $(this).parent("td").find("span.error").length == 0 ? $(this).addClass("error").parent("td").append("<span class='error' style='color:#FF0000;'> numeric only.</span>") : $(this).parent("td").find("span.error").html("<span class='error' style='color:#FF0000;'> numeric only.</span>").show();

                            } else {
                                $(this).removeClass("error").parent("td").find("span.error").hide();
                            }
                        } else {
                            $(this).parent("td").find("span.error").length == 0 ? $(this).addClass("error").parent("td").append("<span class='error' style='color:#FF0000;'>*</span>") : $(this).parent("td").find("span.error").html('*').show();

                        }
                    }
                    //                    else {
                    //                        //$(this).parent("td").find("span.error").length == 0 ? "" : $(this).parent("td").find("span.error").html('*').hide();
                    //                        $(this).parent("td").find("span.error").length == 0 ? $(this).parent("td").append("<span class='error' style='color:#FF0000;'>*</span>") : $(this).parent("td").find("span.error").html('*');
                    //                    }

                });
                return false;
            } else {
                var _StoreID = _storeId;
                var _PortalID = _portalId;
                var _CultureName = _cultureName;
                var _UserName = _userName;

                var _attributeName = $('#txtAttributeName').val();
                var _inputTypeID = $('#ddlAttributeType').val();

                var selectedAttributeType = $("#ddlAttributeType :selected").val();
                var _DefaultValue = "";
                switch (selectedAttributeType) {
                    case "1":
                        _DefaultValue = $("#default_value_text").val();
                        break;
                    case "2":
                        _DefaultValue = $("textarea#default_value_textarea").val();
                        break;
                    case "3":
                        _DefaultValue = $("#default_value_date").val();
                        break;
                    case "4":
                        _DefaultValue = $("#default_value_yesno").val();
                        break;
                    case "8":
                        _DefaultValue = $("#default_value_text").val();
                        break;
                    default:
                        _DefaultValue = '';
                }

                var _ValidationTypeID = $('#ddlTypeValidation').val();
                var _AliasName = $('#txtAliasName').val();
                var _AliasToolTip = $('#txtAliasToolTip').val();
                var _AliasHelp = $('#txtAliasHelp').val();
                var _DisplayOrder = $('#txtDisplayOrder').val();

                var _IsUnique = $('input[name=chkUniqueValue]').attr('checked');
                var _IsRequired = $('input[name=chkValuesRequired]').attr('checked');
                var _IsEnableEditor = $('input[name=chkIsEnableEditor]').attr('checked');
                var _ShowInGrid = false; //$('input[name=chkShowInGrid]').attr('checked');
                var _ShowInSearch = false; //$('input[name=chkShowInSearch]').attr('checked');
                var _ShowInAdvanceSearch = $('input[name=chkUseInAdvancedSearch]').attr('checked');
                var _ShowInComparison = $('input[name=chkComparable]').attr('checked');
                var _IsEnableSorting = false; //$('input[name=chkIsEnableSorting]').attr('checked');
                var _IsUseInFilter = false; //$('input[name=chkIsUseInFilter]').attr('checked');
                var _IsIncludeInPriceRule = $('input[name=chkUseForPriceRule]').attr('checked');
                var _IsIncludeInPromotions = false; //$('input[name=chkUseForPromoRule]').attr('checked');
                var _IsShownInRating = false; //$('input[name=chkUseForRating]').attr('checked');
                var _IsActive = $('input[name=chkActive]').attr('checked');
                var _IsModified = true;

                var _ItemTypes = selectedItemTypeID;
                var _Flag = _flag;
                var _IsUsedInConfigItem = isUsedInConfigItem;

                attributesManage.AddAttributeInfo(_attributeId, _attributeName, _inputTypeID, _DefaultValue,
                    _ValidationTypeID, _Length, _AliasName, _AliasToolTip, _AliasHelp, _DisplayOrder, _IsUnique, _IsRequired,
                    _IsEnableEditor, _ShowInGrid, _ShowInSearch, _ShowInAdvanceSearch, _ShowInComparison, _IsEnableSorting, _IsUseInFilter,
                    _IsIncludeInPriceRule, _IsIncludeInPromotions, _IsShownInRating, _StoreID, _PortalID, _IsActive, _IsModified, _UserName,
                    _CultureName, _ItemTypes, _Flag, _IsUsedInConfigItem, _saveOptions);
            }
            return false;
        },

        AddAttributeInfo: function(_attributeId, _attributeName, _inputTypeID, _DefaultValue,
            _ValidationTypeID, _Length, _AliasName, _AliasToolTip, _AliasHelp, _DisplayOrder,
            _IsUnique, _IsRequired, _IsEnableEditor, _ShowInGrid, _ShowInSearch,
            _ShowInAdvanceSearch, _ShowInComparison, _IsEnableSorting, _IsUseInFilter, _IsIncludeInPriceRule, _IsIncludeInPromotions,
            _IsShownInRating, _storeId, _portalId, _IsActive, _IsModified, _userName, _CultureName, _ItemTypes, _flag, _isUsedInConfigItem, _saveOptions) {

            var info = {
                AttributeID: parseInt(_attributeId),
                AttributeName: _attributeName,
                InputTypeID: _inputTypeID,
                DefaultValue: _DefaultValue,
                ValidationTypeID: _ValidationTypeID,
                Length: _Length > 0 ? _Length : null,
                AliasName: _AliasName,
                AliasToolTip: _AliasToolTip,
                AliasHelp: _AliasHelp,
                DisplayOrder: _DisplayOrder,
                IsUnique: _IsUnique,
                IsRequired: _IsRequired,
                IsEnableEditor: _IsEnableEditor,
                ShowInGrid: _ShowInGrid,
                ShowInSearch: _ShowInSearch,
                ShowInAdvanceSearch: _ShowInAdvanceSearch,
                ShowInComparison: _ShowInComparison,
                IsIncludeInPriceRule: _IsIncludeInPriceRule,
                IsIncludeInPromotions: _IsIncludeInPromotions,
                IsEnableSorting: _IsEnableSorting,
                IsUseInFilter: _IsUseInFilter,
                IsShownInRating: _IsShownInRating,
                StoreID: _storeId,
                PortalID: _portalId,
                IsActive: _IsActive,
                IsModified: _IsModified,
                UpdatedBy: _userName,
                AddedBy: _userName,
                CultureName: _CultureName,
                ItemTypes: _ItemTypes,
                Flag: _flag,
                IsUsedInConfigItem: _isUsedInConfigItem,
                SaveOptions: _saveOptions
            };

            this.config.url = this.config.baseURL + "SaveUpdateAttributeInfo";
            this.config.data = JSON2.stringify({ attributeInfo: info });
            this.config.ajaxCallMode = 9;
            this.ajaxCall(this.config);
            return false;
        },
        BindAttributesInputType: function() {
            this.config.url = this.config.baseURL + "GetAttributesInputTypeList";
            this.config.data = "{}";
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        BindAttributesValidationType: function() {
            this.config.url = this.config.baseURL + "GetAttributesValidationTypeList";
            this.config.data = "{}";
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        BindAttributesItemType: function() {
            this.config.url = this.config.baseURL + "GetAttributesItemTypeList";
            this.config.data = JSON2.stringify({ storeId: storeId, portalId: portalId });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },
        SearchAttributeName: function() {
            var attributeNm = $.trim($("#txtSearchAttributeName").val());
            var required = $.trim($('#ddlIsRequired').val()) == "" ? null : $.trim($('#ddlIsRequired').val()) == "True" ? true : false;
            var SearchComparable = $.trim($("#ddlComparable").val()) == "" ? null : $.trim($("#ddlComparable").val()) == "True" ? true : false;
            var isSystem = $.trim($("#ddlIsSystem").val()) == "" ? null : $.trim($("#ddlIsSystem").val()) == "True" ? true : false;
            if (attributeNm.length < 1) {
                attributeNm = null;
            }
            attributesManage.BindAttributeGrid(attributeNm, required, SearchComparable, isSystem);
        },
        ajaxSuccess: function(msg) {
            switch (attributesManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $("#ddlAttributeType").get(0).options.length = 0;
                    //$("#ddlAttributeType").get(0).options[0] = new Option("Select Type", "-1");
                    $.each(msg.d, function(index, item) {
                        $("#ddlAttributeType").get(0).options[$("#ddlAttributeType").get(0).options.length] = new Option(item.InputType, item.InputTypeID);
                    });
                    break
                case 2:
                    $.each(msg.d, function(index, item) {
                        $("#ddlTypeValidation").get(0).options[$("#ddlTypeValidation").get(0).options.length] = new Option(item.ValidationType, item.ValidationTypeID);
                    });
                    break;
                case 3:
                    $('#lstItemType').get(0).options.length = 0;
                    $('#lstItemType').attr('multiple', 'multiple');
                    $('#lstItemType').attr('size', '5');
                    //$('#lstItemType').removeAttr('multiple');
                    $.each(msg.d, function(index, item) {
                        $("#lstItemType").get(0).options[$("#lstItemType").get(0).options.length] = new Option(item.ItemTypeName, item.ItemTypeID);
                    });
                    break;
                case 4:
                    attributesManage.FillForm(msg);
                    $('#divAttribGrid').hide();
                    $('#divAttribForm').show();
                    break;
                case 5:
                    attributesManage.BindAttributeGrid(null, null, null, null);
                    csscody.info("<h2>Sucessful Message</h2><p>Attribute has been deleted successfully.</p>")
                    $('#divAttribForm').hide();
                    $('#divAttribGrid').show();
                    break;
                case 6:
                    attributesManage.BindAttributeGrid(null, null, null, null);
                    csscody.info("<h2>Sucessful Message</h2><p>Selected attribute(s) has been deleted successfully.</p>")
                    break;
                case 7:
                    attributesManage.BindAttributeGrid(null, null, null, null);
                    break;
                case 8:
                    isUnique = msg.d;
                    break;
                case 9:
                    attributesManage.BindAttributeGrid(null, null, null, null);
                    $('#divAttribGrid').show();
                    if (editFlag > 0) {
                        csscody.info("<h2>Sucessful Message</h2><p>Attribute has been updated successfully.</p>")
                    } else {
                        csscody.info("<h2>Sucessful Message</h2><p>Attribute has been saved successfully.</p>")
                    }
                    attributesManage.ClearForm();
                    $('#divAttribForm').hide();
                    break;
            }
        },
        ajaxFailure: function(msg) {
            switch (attributesManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>Error Message</h2><p>Failed to load attributes input type.</p>');
                    break;
                case 2:
                    csscody.error('<h2>Error Message</h2><p>Failed to load validation type.</p>');
                    break;
                case 3:
                    csscody.error('<h2>Error Message</h2><p>Failed to load attributes item type.</p>');
                    break;
                case 4:
                    csscody.error('<h2>Error Message</h2><p>Failed to update attributes.</p>');
                    break;
                case 5:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete attribute.</p>');
                    break;
                case 6:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete attributes.</p>');
                    break;
                case 7:
                    csscody.error('<h2>Error Message</h2><p>Failed to operate.</p>');
                    break;
                case 8:
                    break;
                case 9:
                    csscody.error('<h2>Error Message</h2><p>Failed to save attribute.</p>');
                    break;
            }
        },
        init: function(config) {
            attributesManage.LoadAttributeStaticImage();
            attributesManage.BindAttributeGrid(null, null, null, null);
            $('#divAttribForm').hide();
            $('#divAttribGrid').show();
            attributesManage.BindAttributesInputType();
            attributesManage.BindAttributesValidationType();
            attributesManage.BindAttributesItemType();
            $('.itemTypes').hide();
            $('#ddlApplyTo').change(function() {
                var selectedValue = $(this).val();
                if (selectedValue !== "0") {
                    $('.itemTypes').show();
                } else {
                    $('.itemTypes').hide();
                }
            });

            $('#btnDeleteSelected').click(function() {
                var attribute_ids = '';
                //Get the multiple Ids of the attribute selected
                $("#gdvAttributes .attrChkbox").each(function(i) {
                    if ($(this).attr("checked")) {
                        attribute_ids += $(this).val() + ',';
                    }
                });
                if (attribute_ids != "") {
                    var properties = {
                        onComplete: function(e) {
                            attributesManage.ConfirmDeleteMultiple(attribute_ids, e);
                        }
                    };
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete selected attribute(s)?</p>", properties);
                } else {
                    csscody.alert('<h2>Information Alert</h2><p>Please select at least one attribute before delete.</p>');
                }
            });

            $('#btnAddNew').bind("click", function() {
                $('#divAttribGrid').hide();
                $('#divAttribForm').show();
                attributesManage.ClearForm();
            });

            $('#btnBack').bind("click", function() {
                $('#divAttribForm').hide();
                $('#divAttribGrid').show();
                attributesManage.ClearForm();
            });

            $('#btnReset').bind("click", function() {
                attributesManage.ClearForm();
            });

            $('#btnSaveAttribute').bind("click", function() {
                //check if its update or save new 
                //Get the Id of the attribute to update
                var attribute_id = $(this).attr("name");
                if (attribute_id != '') {
                    editFlag = attribute_id;
                    attributesManage.SaveAttribute(attribute_id, storeId, portalId, userName, cultureName, false);
                } else {
                    editFlag = 0;
                    attributesManage.SaveAttribute(0, storeId, portalId, userName, cultureName, true);
                }
            });

            //validate name on focus lost
            $('#txtAttributeName').blur(function() {
                // Validate name
                var errors = '';
                var attributeName = $(this).val();
                var attribute_id = $('#btnSaveAttribute').attr("name");
                if (attribute_id == '') {
                    attribute_id = 0;
                }
                if (!attributeName) {
                    errors += 'Please enter attribute name';
                }
                //check uniqueness
                else if (!attributesManage.IsUnique(attributeName, attribute_id)) {
                    errors += 'Please enter unique attribute name"' + attributeName.trim() + '" already exists.<br/>';
                }

                if (errors) {
                    $('.cssClassRight').hide();
                    $('.cssClassError').show();
                    $(".cssClassError").parent('div').addClass("diverror");
                    $('.cssClassError').prevAll("input:first").addClass("error");
                    $('.cssClassError').html(errors);
                    return false;
                } else {
                    $(this).parent("td").find("span.error").hide();
                    $('.cssClassRight').show();
                    $('.cssClassError').hide();
                    $(".cssClassError").parent('div').removeClass("diverror");
                    $('.cssClassError').prevAll("input:first").removeClass("error");
                }
            });

            $(".delbutton").click(function() {
                //Get the Id of the attribute to delete
                var attribute_id = $(this).attr("id").replace(/[^0-9]/gi, '');
                attributesManage.DeleteAttribute(attribute_id, storeId, portalId, userName);
            });

            $("#ddlAttributeType").bind("change", function() {
                //$("#dataTable>tbody tr:not(:first)").remove();
                if ($(this).val() == 1 || $(this).val() == 2 || $(this).val() == 3 || $(this).val() == 7) {
                    $("input[name=chkValuesRequired]").attr('checked', false).attr("disabled", false);
                } else {
                    $("input[name=chkValuesRequired]").attr('checked', false).attr("disabled", true);
                }
                $("#dataTable tr:gt(1)").remove();
                $("#dataTable>tbody tr").find("input:not(:last)").each(function(i) {
                    if (this.name == "value") {
                        $(this).val('');
                    } else if (this.name == "position") {
                        $(this).val('');
                    } else if ($(this).hasClass("class-isdefault")) {
                        this.checked = false;
                    }

                });

                attributesManage.ValidationTypeEnableDisable("", true);
                if ($(this).val() == 10) {
                    $("#dataTable #tddefault").find('input[name=defaultRdo]').attr('checked', true);
                }
            });

            $("input[type=button].AddOption").click(function() {
                var checkedState = false;
                if ($(this).attr("name") == "DeleteOption") {
                    var t = $(this).closest('tr');
                    t.find("td")
                        .wrapInner("<div style='DISPLAY: block'/>")
                        .parent().find("td div")
                        .slideUp(300, function() {
                            t.remove();
                        });
                } else if ($(this).attr("name") == "AddMore") {
                    checkedState = $('#dataTable>tbody tr:first').find('input[type="radio"]').attr("checked");
                    var cloneRow = $(this).closest('tr').clone(true)
                    $(cloneRow).find("input").each(function(i) {
                        if (this.name == "value") {
                            $(this).val('');
                        } else if (this.name == "position") {
                            $(this).val('');
                        } else if (this.name == "Alias") {
                            $(this).val('');
                        } else if ($(this).hasClass("class-isdefault")) {
                            this.checked = false;
                        } else if ($(this).hasClass("AddOption")) {
                            $(this).attr("name", "DeleteOption");
                            $(this).attr("value", "Delete Option");
                        }
                    });
                    $(cloneRow).appendTo("#dataTable");
                    $('#dataTable>tbody tr:first').find('input[type="radio"]').attr("checked", checkedState);
                    $('#dataTable tr:last').hide();
                    $('#dataTable tr:last td').fadeIn('slow');
                    $('#dataTable tr:last').show();
                    $('#dataTable tr:last td').show();
                }
            });
            $("#btnSearchAttribute").bind("click", function() {
                attributesManage.SearchAttributeName();
            });

            $('#txtSearchAttributeName,#ddlIsRequired,#ddlComparable,#ddlIsSystem').keyup(function(event) {
                if (event.keyCode == 13) {
                    $("#btnSearchAttribute").click();
                }
            });
        }
    };
    attributesManage.init();
});