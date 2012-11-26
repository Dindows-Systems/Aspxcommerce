var couponTypeMgmt;
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
    var couponTypeFlag = 0;
    var isUnique = false;
    couponTypeMgmt = {
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
                type: couponTypeMgmt.config.type,
                contentType: couponTypeMgmt.config.contentType,
                cache: couponTypeMgmt.config.cache,
                async: couponTypeMgmt.config.async,
                url: couponTypeMgmt.config.url,
                data: couponTypeMgmt.config.data,
                dataType: couponTypeMgmt.config.dataType,
                success: couponTypeMgmt.ajaxSuccess,
                error: couponTypeMgmt.ajaxFailure
            });
        },
        ClearForm: function() {
            $("#txtNewCouponType").val('');
            $("#chkIsActive").attr("checked", true);
            $('#ctErrorLabel').html('');
        },

        LoadCouponTypeStaticImage: function() {
            $('#ajaxCouponTypeImageLoad').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        HideAllCouponTypeDivs: function() {
            $("#divShowCouponTypeDetails").hide();
            $("#divCouponTypeProviderForm").hide();
        },

        BindCouponTypeDetails: function(searchCouponType) {
            this.config.method = "GetCouponTypeDetails";
            this.config.url = this.config.baseURL;
            this.config.data = { couponTypeName: searchCouponType, storeId: storeId, portalId: portalId, cultureName: cultureName };
            var data = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCouponType_pagesize").length > 0) ? $("#gdvCouponType_pagesize :selected").text() : 10;

            $("#gdvCouponType").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'Coupon Type ID', name: 'coupon_type_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'CouponTypeChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: 'Coupon Type', name: 'setting_key', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Added On', name: 'added_on', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: 'Is Active', name: 'is_active', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'couponTypeMgmt.EditCouponType', arguments: '1,3' },
                    { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'couponTypeMgmt.DeleteCouponType', arguments: '' }
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

        EditCouponType: function(tblID, argus) {
            switch (tblID) {
                case "gdvCouponType":
                    $("#" + lblCouponTypeFormTitle).html("Edit Coupon Type: '" + argus[3] + "'");
                    couponTypeMgmt.ClearForm();
                    couponTypeFlag = argus[0];
                    $("#txtNewCouponType").val(argus[3]);
                    $("#chkIsActive").attr("checked", couponTypeMgmt.Boolean(argus[4]));
                    couponTypeMgmt.HideAllCouponTypeDivs();
                    $("#divCouponTypeProviderForm").show();
                    break;
                default:
                    break;
            }
        },

        DeleteCouponType: function(tblID, argus) {
            switch (tblID) {
                case "gdvCouponType":
                    var properties = {
                        onComplete: function(e) {
                            couponTypeMgmt.DeleteCouponTypeByID(argus[0], e);
                        }
                    }
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this coupon type?</p>", properties);
                    break;
                default:
                    break;
            }
        },

        DeleteCouponTypeByID: function(ids, event) {
            if (event) {

                this.config.method = "DeleteCouponType";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ IDs: ids, storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            }
        },

        DeleteMultipleCouponTypes: function(ids, event) {
            couponTypeMgmt.DeleteCouponTypeByID(ids, event);
        },

        SearchCouponType: function() {
            var searchCouponType = $.trim($("#txtSearchCouponType").val());
            //var isAct = $("#ddlIsActive").val() == "" ? null : $("#ddlIsActive").val() == "True" ? true : false;
            if (searchCouponType.length < 1) {
                searchCouponType = null;
            }
            couponTypeMgmt.BindCouponTypeDetails(searchCouponType);
        },

        AddUpdateCouponType: function() {
            var couponType_id = couponTypeFlag;
            var isActive = $("#chkIsActive").attr("checked");
            var couponType = $("#txtNewCouponType").val();
            //  couponTypeFlag = couponType_id;
            this.config.method = "AddUpdateCouponType";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ couponTypeID: couponType_id, couponType: couponType, isActive: isActive, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
            var data = this.config.data;
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        CheckCouponTypeUniquness: function(couponTypeId) {
            var couponTypeName = $.trim($('#txtNewCouponType').val());
            this.config.method = "CheckCouponTypeUniqueness";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ storeId: storeId, portalId: portalId, cultureName: cultureName, couponTypeId: couponTypeId, couponType: couponTypeName });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
            return isUnique;
        },
        ajaxSuccess: function(data) {
            switch (couponTypeMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    couponTypeMgmt.BindCouponTypeDetails(null);
                    if (couponTypeFlag > 0) {
                        csscody.info('<h2>Successful Message</h2><p>Coupon type has been updated successfully.</p>');
                    } else {
                        csscody.info('<h2>Successful Message</h2><p>Coupon type has been saved successfully.</p>');
                    }
                    couponTypeMgmt.HideAllCouponTypeDivs();
                    $("#divShowCouponTypeDetails").show();
                    break;
                case 2:
                    couponTypeMgmt.BindCouponTypeDetails(null);
                    csscody.info('<h2>Successful Message</h2><p>Coupon type has been deleted successfully.</p>');
                    break;
                case 3:
                    isUnique = data.d;
                    if (data.d == true) {
                        $('#txtNewCouponType').removeClass('error');
                        $('#ctErrorLabel').html('');
                    } else {
                        $('#txtNewCouponType').addClass('error');
                        $('#ctErrorLabel').html('This coupon type already exist!').css("color", "red");
                        return false;
                    }
                    break;
            }
        },

        ajaxFailure: function() {
            switch (couponTypeMgmt.config.ajaxCallModw) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>Error Message</h2><p>Failed to save coupon type!</p>');
                    break;
                case 2:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete coupon type!</p>');
                    break;
            }
        },

        init: function() {
            couponTypeMgmt.LoadCouponTypeStaticImage();
            couponTypeMgmt.BindCouponTypeDetails(null);
            couponTypeMgmt.HideAllCouponTypeDivs();
            $("#divShowCouponTypeDetails").show();
            $("#btnAddNewCouponType").bind('click', function() {
                $("#" + lblCouponTypeFormTitle).html("Add New Coupon Type");
                couponTypeFlag = 0;
                couponTypeMgmt.HideAllCouponTypeDivs();
                $("#divCouponTypeProviderForm").show();
                couponTypeMgmt.ClearForm();
            });
            $("#btnCancelCouponTypeUpdate").click(function() {
                couponTypeMgmt.HideAllCouponTypeDivs();
                $("#divShowCouponTypeDetails").show();
                $('#txtNewCouponType').removeClass('error');
                $('#txtNewCouponType').parents('td').find('label').remove();
            });
            $("#txtNewCouponType").bind('focusout', function() {
                couponTypeMgmt.CheckCouponTypeUniquness(couponTypeFlag);
            });
            $("#btnSubmitCouponType").click(function() {
                var v = $("#form1").validate({
                    messages: {
                        CouponTypeName: {
                            required: '*',
                            minlength: "* (at least 2 chars)"
                        }
                    }
                });
                if (v.form() && couponTypeMgmt.CheckCouponTypeUniquness(couponTypeFlag)) {
                    couponTypeMgmt.AddUpdateCouponType();
                } else {
                    return false;
                }
            });
            $('#txtSearchCouponType').keyup(function(event) {
                if (event.keyCode == 13) {
                    couponTypeMgmt.SearchCouponType();
                }
            });
            $("#btnDeleteSelectedCouponType").click(function() {
                var coupontype_ids = '';
                //Get the multiple Ids of the item selected
                $(".CouponTypeChkbox").each(function(i) {
                    if ($(this).attr("checked")) {
                        coupontype_ids += $(this).val() + ',';
                    }
                });
                if (coupontype_ids == "") {
                    csscody.alert('<h2>Information Alert</h2><p>Please select at least on coupon type before delete.</p>');
                    return false;
                }
                var properties = {
                    onComplete: function(e) {
                        couponTypeMgmt.DeleteMultipleCouponTypes(coupontype_ids, e);
                    }
                };
                csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete the selected coupon type(s)?</p>", properties);
            });
        }
    };
    couponTypeMgmt.init();
});