var couponPerUserMgmt;
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

    couponPerUserMgmt = {
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
                type: couponPerUserMgmt.config.type,
                contentType: couponPerUserMgmt.config.contentType,
                cache: couponPerUserMgmt.config.cache,
                async: couponPerUserMgmt.config.async,
                url: couponPerUserMgmt.config.url,
                data: couponPerUserMgmt.config.data,
                dataType: couponPerUserMgmt.config.dataType,
                success: couponPerUserMgmt.ajaxSuccess,
                error: couponPerUserMgmt.ajaxFailure
            });
        },
        HideAll: function() {
            $("#divShowCouponTypeDetails").hide();
            $("#divCouponUserForm").hide();
        },
        GetCouponUserDataForExport: function() {
            this.config.url = this.config.baseURL + "GetCouponUserDetails";
            this.config.data = JSON2.stringify({ offset: 1, limit: null, couponCode: null, userName: null, couponStatusId: null, validFrom: null, validTo: null, storeId: storeId, portalId: portalId, cultureName: cultureName });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },
        BindCouponUserDetails: function(searchCouponCode, userName, couponStatusID, validateFrom, validateTo) {
            this.config.method = "GetCouponUserDetails";
            this.config.data = { couponCode: searchCouponCode, userName: userName, couponStatusId: couponStatusID, validFrom: validateFrom, validTo: validateTo, storeId: storeId, portalId: portalId, cultureName: cultureName };
            var data = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCouponUser_pagesize").length > 0) ? $("#gdvCouponUser_pagesize :selected").text() : 10;

            $("#gdvCouponUser").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'Coupon User ID', name: 'coupon_type_id', cssclass: 'cssClassHide', coltype: 'checkbox', align: 'center', elemClass: 'CouponUserChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox', hide: true },
                    { display: 'Coupon ID', name: 'coupon_id', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Coupon Code', name: 'coupon_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'User Name', name: 'user', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Coupon Life', name: 'couponLife', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'No Of Uses', name: 'no_of_use', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Coupon Status ID', name: 'coupon_status_id', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Coupon Status', name: 'coupon_status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Valid From', name: 'valid_from', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Valid To', name: 'valid_to', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 2: { sorter: false }, 10: { sorter: false} }
            });
        },
        LoadCouponPerUserStaticImage: function() {
            $('#ajaxCouponPerUserImageLoad').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        GetCouponStatus: function() {
            this.config.url = this.config.baseURL + "GetCouponStatus";
            this.config.data = "{}";
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ExportCouponPerUserToCsvData: function() {
            couponPerUserMgmt.GetCouponUserDataForExport();
        },
        ExportCouponPerUserDivDataToExcel: function() {
            couponPerUserMgmt.GetCouponUserDataForExport();
        },
        DeleteCouponUserByID: function(ids, event) {
            if (event) {
                this.config.method = "DeleteCouponUser";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ couponUserID: ids, storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);

            }
        },
        SearchCouponCode: function() {
            var searchCouponCode = $.trim($("#txtSearchCouponCode").val());
            var userName = $.trim($("#txtSearchUserName").val());
            var couponStatusID = $.trim($("#ddlCouponStatus").val());
            var validateFrom = $.trim($("#txtValidFrom").val());
            var validateTo = $.trim($("#txtValidTo").val());
            if (couponStatusID == "0") {
                couponStatusID = null;
            }
            if (searchCouponCode.length < 1) {
                searchCouponCode = null;
            }
            if (userName.length < 1) {
                userName = null;
            }
            if (validateFrom.length < 1) {
                validateFrom = null;
            } else {
                var splitFromDate = String(validateFrom).split('/');
                validateFrom = new Date(Date.UTC(splitFromDate[0], splitFromDate[1] * 1 - 1, splitFromDate[2], 12, 0, 0, 0));
                validateFrom = validateFrom.toMSJSON();
            }
            if (validateTo.length < 1) {
                validateTo = null;
            } else {
                var splitToDate = String(validateTo).split('/');
                validateTo = new Date(Date.UTC(splitToDate[0], splitToDate[1] * 1 - 1, splitToDate[2], 12, 0, 0, 0));
                validateTo = validateTo.toMSJSON();
            }
            couponPerUserMgmt.BindCouponUserDetails(searchCouponCode, userName, couponStatusID, validateFrom, validateTo);
        },
        BindCouponUserDataForExport: function(data) {
            var exportData = '<thead><tr><th>Coupon Code</th><th>User Name</th><th>Coupon Life</th><th>No Of Uses</th><th>Coupon Status</th><th>Valid From</th><th>Valid To</th></tr></thead><tbody>';
            if (data.d.length > 0) {
                $.each(data.d, function(index, value) {
                    exportData += '<tr><td>' + value.CouponCode + '</td><td>' + value.UserName + '</td>';
                    exportData += '<td>' + value.CouponLife + '</td><td>' + value.NoOfUse + '</td>';
                    exportData += '</td><td>' + value.CouponStatus + '</td>';
                    exportData += '<td>' + value.ValidateFrom + '</td><td>' + value.ValidateTo + '</td></tr>';
                });
            } else {
                exportData += '<tr><td>No Records Found!</td></tr>';
            }
            exportData += '</tbody>';
            $('#CouponUserExportDataTbl').html(exportData);
            $("input[id$='HdnValue']").val('<table>' + exportData + '</table>');
            $("input[id$='_csvCouponPerUserHiddenValue']").val($('#CouponUserExportDataTbl').table2CSV());
            $('#CouponUserExportDataTbl').html('');
        },
        ajaxSuccess: function(data) {
            switch (couponPerUserMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $.each(data.d, function(index, value) {
                        var couponStatusElements = "<option value=" + value.CouponStatusID + ">" + value.CouponStatus + "</option>";
                        $("#ddlCouponStatusType").append(couponStatusElements);
                        $("#ddlCouponStatus").append(couponStatusElements);
                    });
                    break;
                case 2:
                    couponPerUserMgmt.BindCouponUserDetails(null, null, null, null, null);
                    break;
                case 3:
                    couponPerUserMgmt.BindCouponUserDataForExport(data);
                    break;
            }
        },
        ajaxFailure: function(data) {
            switch (couponPerUserMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>Error Message</h1><p>Could not load Order Status!!!</p>');
                    break;
                case 2:
                    csscody.error('<h1>Error Message</h1><p>Error Occured!!</p>');
                    break;
            }
        },
        init: function() {
            $("#txtValidFrom").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtValidTo").datepicker({ dateFormat: 'yy/mm/dd' });
            couponPerUserMgmt.HideAll();
            $("#divShowCouponTypeDetails").show();
            couponPerUserMgmt.BindCouponUserDetails(null, null, null, null, null);
            couponPerUserMgmt.LoadCouponPerUserStaticImage();
            couponPerUserMgmt.GetCouponStatus();
            $("#btnDeleteAllNonPendingCoupon").bind('click', function() {
                var properties = {
                    onComplete: function(e) {
                        DeleteCouponUserByID(0, e);
                    }
                };
                csscody.confirm("<h1>Delete Confirmation</h1><p>Do you want to delete all non pending coupon User(s)?</p>", properties);
            });
            $("#" + couponPerSalesDataToExcel).bind('click', function() {
                couponPerUserMgmt.ExportCouponPerUserDivDataToExcel();
            });
            $('#txtSearchCouponCode,#txtSearchUserName,#ddlCouponStatus,#txtValidFrom,#txtValidTo').keyup(function(event) {
                if (event.keyCode == 13) {
                    couponPerUserMgmt.SearchCouponCode();
                }
            });
        }
    };
    couponPerUserMgmt.init();
});