var couponMgmt;

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
    var senderEmail = userEmail;
    var serverLocation = ServerVariables;
    var deleteAllSelectedCouponUser = 0;
    var seachByCouponUser = 0;
    var portalusers_emailid = '';
    var portalusers_customername = '';
    var portalusers_username = '';

    couponMgmt = {
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
                type: couponMgmt.config.type,
                contentType: couponMgmt.config.contentType,
                cache: couponMgmt.config.cache,
                async: couponMgmt.config.async,
                url: couponMgmt.config.url,
                data: couponMgmt.config.data,
                dataType: couponMgmt.config.dataType,
                success: couponMgmt.ajaxSuccess,
                error: couponMgmt.ajaxFailure
            });
        },
        LoadCouponAjaxImage: function() {
            $('#ajaxCouponMgmtImageLoad').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxCouponImageLoad2').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        HideAllCouponDivs: function() {
            $("#divShowCouponDetails").hide();
            $("#divCouponForm").hide();
        },
        ClearCouponForm: function() {
            document.getElementById('btnGenerateCode').disabled = false;
            $("#ddlCouponType").val(0);
            //  $("#txtUsesPerCoupon").val('');
            $("#txtUsesPerCustomer").val('');
            $("#txtAmount").val('');
            $("#txtNewCoupon").val('');
            $("#txtValidTo").val('');
            $("#txtValidFrom").val('');
            // $("#txtNewCoupon").removeAttr("disabled");
            $("#txtAmount").removeAttr("disabled");
            $("#txtAmount").parents('tr').show();
            $("#txtUsesPerCustomer").removeAttr("disabled");
            $("#ddlCouponType").removeAttr("disabled");
            $("#ddlIsForFreeShipping").removeAttr("disabled");
            $("#chkIsActive").attr("checked", true);
            //$("#chkIsActive").removeAttr("checked");
            $("#ddlIsForFreeShipping").val(1);
            couponMgmt.BindAllPortalUsersByCouponID(0, null);
            couponMgmt.ClearCouponFormError();
            portalusers_customername = '';
            portalusers_emailid = '';
            portalusers_username = '';
        },

        ClearCouponFormError: function() {
            // $('#txtUsesPerCoupon').removeClass('error');
            // $('#txtUsesPerCoupon').parents('td').find('label').remove();
            $('#txtUsesPerCustomer').removeClass('error');
            $('#txtUsesPerCustomer').parents('td').find('label').remove();
            $('#txtAmount').removeClass('error');
            $('#txtAmount').parents('td').find('label').remove();
            $('#txtNewCoupon').removeClass('error');
            $('#txtNewCoupon').parents('td').find('label').remove();
            $('#txtValidTo').removeClass('error');
            $('#txtValidTo').parents('td').find('label').remove();
            $('#txtValidFrom').removeClass('error');
            $('#txtValidFrom').parents('td').find('label').remove();
            $('#ddlCouponType').removeClass('error');
            $('#ddlCouponType').parents('td').find('label').remove();
            $('#created').html('');
            $('.to').parents('td').find('input').attr("style", '');
            $('#ddlIsForFreeShipping').removeClass('error');
            $('#couponAmountErrorLabel').html(''); //.css("color", "red");
        },
        GenerateCodeString: function() {
            var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
            var string_length = 15;
            var codeString = '';
            for (var i = 0; i < string_length; i++) {
                var rnum = Math.floor(Math.random() * chars.length);
                codeString += chars.substring(rnum, rnum + 1);
            }
            $("#txtNewCoupon").val(codeString);
        },
        GetAllCouponType: function() {
            var offset = 0;
            var limit = 0;
            var couponTypeName = null;

            this.config.method = "GetCouponTypeDetails";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ offset: offset, limit: limit, couponTypeName: couponTypeName, storeId: storeId, portalId: portalId, cultureName: cultureName });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        GetCouponStatus: function() {
            this.config.url = this.config.baseURL + "GetCouponStatus";
            this.config.data = "{}";
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        AddUpdateCoupon: function(settingId, settingValue) {
            //            var portalusers_customername = '';
            //            var portalusers_emailid = '';
            //            var portalusers_username = '';

            $("#gdvPortalUser .portalUserChkbox").each(function(i) {
                if ($(this).attr("checked") && $(this).attr('disabled') == false) {
                    portalusers_username += $(this).parent('td').next('td').text() + '#';
                    portalusers_customername += $(this).parent('td').next('td').next('td').text() + '#';
                    portalusers_emailid += $(this).parent('td').next('td').next('td').next('td').text() + '#';
                }
            });

            portalusers_username = portalusers_username.substring(0, portalusers_username.length - 1);
            portalusers_customername = portalusers_customername.substring(0, portalusers_customername.length - 1);
            portalusers_emailid = portalusers_emailid.substring(0, portalusers_emailid.length - 1);

            var couponId = $("#hdnCouponID").val();
            var couponTypeId = $("#ddlCouponType").val();
            var couponCode = $("#txtNewCoupon").val();
            var couponAmount = $("#txtAmount").val() == "" ? null : $("#txtAmount").val();
            if (couponAmount == 0 && $('#ddlIsForFreeShipping').val() == 2) {
                $('#txtAmount').addClass('error');
                $('#couponAmountErrorLabel').html('Zero coupon amount is not allowed!!').css("color", "red");
                return false;
            } else {
                $('#txtAmount').removeClass('error');
                $('#couponAmountErrorLabel').html('');
            }
            var couponLife = $('#txtUsesPerCustomer').val();
            var validFrom = $("#txtValidFrom").val();
            var validTo = $("#txtValidTo").val();
            var isActive = $("#chkIsActive").attr("checked");
            var couponName = $('#ddlCouponType option:selected').text();

            var serverHostLoc = 'http://' + serverLocation;
            var subject = "Congratulation You Got a CouponCode ";

            var fullDate = new Date();
            var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : (fullDate.getMonth() + 1);
            if (twoDigitMonth.length == 2) {
            } else if (twoDigitMonth.length == 1) {
                twoDigitMonth = '0' + twoDigitMonth;
            }
            var currentDate = fullDate.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();
            var dateyear = fullDate.getFullYear();

            var emailtemplate = [];
            var unames = [];
            unames = portalusers_customername.split('#');
            var messageBodyHtml = '';
            for (var nn in unames) {
                messageBodyHtml += '<table width="100%" border="0" align="center" cellpadding="0" cellspacing="5" bgcolor="#e0e0e0" style="font:12px Arial, Helvetica, sans-serif;"><tr><td align="center" valign="top"><table style="font:12px Arial, Helvetica, sans-serif;" width="680" border="0" cellspacing="0" cellpadding="0">';
                messageBodyHtml += '<tr><td><img src="' + serverHostLoc + '/blank.gif" width="1" height="10" alt=" " /></td></tr>';
                messageBodyHtml += '<tr><td><img src="' + serverHostLoc + '/blank.gif" width="1" height="10" alt=" " /></td></tr><tr><td><table style="font:12px Arial, Helvetica, sans-serif;" width="680" border="0" cellspacing="0" cellpadding="0"><tr><td width="300">';
                messageBodyHtml += '<a href="' + serverHostLoc + '" target="_blank" style="outline:none; border:none;"><img src="' + serverHostLoc + '/' + aspxTemplateFolderPath + '/images/AspxCommerce.png" width="143" height="62" alt="AspxCommerce" title="AspxCommerce"/></a></td>';
                messageBodyHtml += '<td width="191" align="left" valign="middle">&nbsp;</td><td width="189" align="right" valign="middle"><b style="padding:0 20px 0 0; text-shadow:1px 1px 0 #fff;">' + currentDate + '</b></td></tr></table></td></tr>';
                messageBodyHtml += '<tr><td><img src="' + serverHostLoc + '/blank.gif" width="1" height="10" alt=" " /></td></tr>';
                messageBodyHtml += '<tr><td bgcolor="#fff"><div style="border:1px solid #c7c7c7; background:#fff; padding:20px">';
                messageBodyHtml += '<table style="font:12px Arial, Helvetica, sans-serif;" width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF"><tr><td><p style="font-family:Arial, Helvetica, sans-serif; font-size:17px; line-height:16px; color:#278ee6; margin:0; padding:0 0 10px 0; font-weight:bold; text-align:left;">Congratulation !! ';
                messageBodyHtml += unames[nn].toUpperCase();
                messageBodyHtml += ' You have got Coupon code for shopping!!</p></td></tr><tr><td><span style="font-weight:normal; font-size:12px; font-family:Arial, Helvetica, sans-serif;">Enjoy your Shopping !!</span></td></tr></table>';
                //content of coupon
                messageBodyHtml += '<div style="border:1px solid #cfcfcf; background:#f1f1f1; padding:10px"><table style="font:12px Arial, Helvetica, sans-serif;" width="100%" border="0" cellspacing="0" cellpadding="0"><tr><td><table  style="font:12px Arial, Helvetica, sans-serif;" width="100%" border="0" cellspacing="0" cellpadding="0"><tr>';
                messageBodyHtml += '<td width="120" height="20"><span style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold;">Coupon Type: </span></td> <td>' + couponName + '</td>';
                messageBodyHtml += '<td width="150" style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold; border-left:1px solid #fff; padding-left:20px;">Valid From: </td><td>' + validFrom + '</td></tr>';
                messageBodyHtml += '<tr><td height="20"><span style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold;">Coupon Code: </span></td> <td>' + couponCode + '</td>';
                messageBodyHtml += ' <td  style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold; border-left:1px solid #fff; padding-left:20px;">Valid Upto: </td><td>' + validTo + '</td></tr>';
                messageBodyHtml += '<tr><td height="20"><span style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold;">Coupon Life: </span></td><td>' + couponLife + '</td>';
                messageBodyHtml += '<td  style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold; border-left:1px solid #fff; padding-left:20px;">&nbsp;</td><td>&nbsp;</td></tr>';
                if ($('#ddlIsForFreeShipping option:selected').val() == 2) {
                    messageBodyHtml += '<tr><td height="20"><span style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold;">Coupon Amount: </span></td><td>Do not worry!! Its Free Shipping Coupon.</td>';
                } else {
                    messageBodyHtml += '<tr><td height="20"><span style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold;">Coupon Amount: </span></td><td>$' + couponAmount + '</td>';
                }
                messageBodyHtml += ' <td  style="font-family:Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold; border-left:1px solid #fff; padding-left:20px;">&nbsp;</td><td>&nbsp;</td></tr></table></td></tr></table></div>';
                messageBodyHtml += '<p style="margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;">Thank You,<br /><span style="font-weight:normal; font-size:12px; font-family:Arial, Helvetica, sans-serif;">AspxCommerce Team </span></p></div></td></tr>';
                messageBodyHtml += '<tr><td><img src="' + serverHostLoc + '/blank.gif" width="1" height="20" alt=" "/></td></tr>';
                messageBodyHtml += '<tr><td align="center" valign="top"><p style="font-size:11px; color:#4d4d4d"> © ' + dateyear + ' AspxCommerce. All Rights Reserved.</p></td></tr>';
                messageBodyHtml += '    <tr><td align="center" valign="top"><img src="' + serverHostLoc + '/blank.gif" width="1" height="10" alt=" " /></td></tr></table></td></tr></table>';
                emailtemplate.push(messageBodyHtml);
                messageBodyHtml = '';
            }
            this.config.method = "AddUpdateCouponDetails";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({
                couponID: couponId,
                couponTypeID: couponTypeId,
                couponCode: couponCode,
                couponAmount: couponAmount,
                validateFrom: validFrom,
                validateTo: validTo,
                isActive: isActive,
                storeID: storeId,
                portalID: portalId,
                cultureName: cultureName,
                userName: userName,
                settingIDs: settingId,
                settingValues: settingValue,
                portalUserCustomerName: portalusers_customername,
                portalUserEmailID: portalusers_emailid,
                portalUserUserName: portalusers_username,
                senderEmail: senderEmail,
                subject: subject,
                messageBody: emailtemplate
            });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },

        BindAllPortalUsersByCouponID: function(couponId, customerName) {
            this.config.method = "GetPortalUsersByCouponID";
            this.config.url = this.config.baseURL;
            this.config.data = { couponID: couponId, storeID: storeId, portalID: portalId, customerName: customerName, cultureName: cultureName };
            var data2 = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvPortalUser_pagesize").length > 0) ? $("#gdvPortalUser_pagesize :selected").text() : 10;

            $("#gdvPortalUser").sagegrid({
                url: aspxservicePath + "AspxCommerceWebService.asmx/",
                functionMethod: 'GetPortalUsersByCouponID',
                colModel: [
                    { display: 'Portal User ID', name: 'portal_user_ID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', checkFor: '4', elemClass: 'portalUserChkbox', elemDefault: false, controlclass: 'userHeaderChkbox' },
                    { display: 'User Name', name: 'user_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Customer Name', name: 'customer_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Email', name: 'email', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'IsAlreadySent', name: 'is_already_sent', cssclass: '', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true }
                ],
                rp: perpage,
                nomsg: "No Customers Found!",
                param: data2, //{ couponID: couponId, storeID: storeId, portalID: portalId, customerName: customerName, cultureName: cultureName },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 4: { sorter: false} }
            });
        },
        BindCouponDetails: function(SearchCouponTypeId, SearchCouponCode, validateFromDate, validateToDate) {
            this.config.method = "GetCouponDetails";
            this.config.url = this.config.baseURL;
            this.config.data = { couponTypeID: SearchCouponTypeId, couponCode: SearchCouponCode, validateFrom: validateFromDate, validateTo: validateToDate, storeId: storeId, portalId: portalId, cultureName: cultureName };
            var data = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCoupons_pagesize").length > 0) ? $("#gdvCoupons_pagesize :selected").text() : 10;

            $("#gdvCoupons").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'CouponID', name: 'coupon_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'CouponChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: 'Coupon Type ID', name: 'coupon_type_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Coupon Type', name: 'coupon_type', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Coupon Code', name: 'coupon_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Number Of Uses', name: 'number_of_uses', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Validate From', name: 'validate_from', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Validate To', name: 'validate_to', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Coupon Amount', name: 'balance_amount', cssclass: 'cssClassHeadNumber', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: 'IsFreeShipping', name: 'IsFreeShipping', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Added On', name: 'added_on', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Updated On', name: 'updated_on', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Is Active', name: 'is_active', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: 'View', name: 'view', enable: true, _event: 'click', trigger: '3', callMethod: 'couponMgmt.ViewCoupons', arguments: '1' },
                    { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'couponMgmt.EditCoupons', arguments: '1,3,5,6,7,8,9,11' },
                    { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'couponMgmt.DeleteCoupons', arguments: '' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 12: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

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
        ViewCoupons: function(tblID, argus) {
            switch (tblID) {
                case "gdvCoupons":
                    deleteAllSelectedCouponUser = 1;
                    seachByCouponUser = 1;
                    $('#gdvCoupons').hide();
                    $('#gdvCoupons_Pagination').hide();
                    $('#btnBackToCouponTbl').show();
                    $('#gdvCouponUser').show();
                    $('#btnAddNewCoupon').hide();
                    $('.cssClassddlCouponStatus').show();
                    $('.cssClasstxtSearchUserName').show();

                    $('.cssClassddlCouponType').hide();
                    $('.cssClasstxtSearchCouponCode').hide();
                    $('.cssClasstxtSearchValidateFrom').hide();
                    $('.cssClasstxtSearchValidateTo').hide();
                    $("#hdnCouponID").val(argus[0]);
                    var couponID = argus[0];
                    var couponCode = "";
                    couponMgmt.BindCouponUsers(couponID, null, null, null);

                    break;
                default:
                    break;
            }
        },
        BindCouponUsers: function(couponID, SearchCouponCode, userName, couponStatusID) {
            this.config.method = "GetCouponUserList";
            this.config.url = this.config.baseURL;
            this.config.data = { couponID: couponID, couponCode: SearchCouponCode, userName: userName, couponStatusID: couponStatusID, storeID: storeId, portalID: portalId, cultureName: cultureName };
            var dataCouponUsers = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCouponUser_pagesize").length > 0) ? $("#gdvCouponUser_pagesize :selected").text() : 10;

            $("#gdvCouponUser").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'CouponUserID', name: 'couponUserID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'CouponViewChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: 'CouponID', name: 'coupon_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Coupon Code', name: 'coupon_code', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'User Name', name: 'userName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Coupon Amount', name: 'balance_amount', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Coupon Status ID', name: 'coupon_status_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Coupon Status', name: 'coupon_status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Coupon Life', name: 'couponLife', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Number Of Uses', name: 'number_of_uses', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Validate From', name: 'validate_from', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: 'Validate To', name: 'validate_to', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'couponMgmt.EditCouponsStatus', arguments: '1,2,3,4,5' },
                    { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'couponMgmt.DeleteCouponsUser', arguments: '1,2,3,5' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: dataCouponUsers,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 11: { sorter: false} }
            });

        },
        EditCoupons: function(tblID, argus) {
            switch (tblID) {
                case "gdvCoupons":
                    //BInd PortalUserCoupon Grid HERE
                    couponMgmt.ClearCouponFormError();
                    couponMgmt.BindAllPortalUsersByCouponID(argus[0], null);
                    document.getElementById('btnGenerateCode').disabled = true;
                    $("#" + lblCouponManageTitle).html("Edit Coupon: '" + argus[4] + "'");
                    $("#hdnCouponID").val(argus[0]);
                    $("#ddlCouponType").val(argus[3]);
                    $("#txtNewCoupon").val(argus[4]);
                    $("#txtValidFrom").val(argus[5]);
                    $("#txtValidTo").val(argus[6]);
                    $("#txtAmount").val(argus[7]);
                    $("#chkIsActive").attr('checked', couponMgmt.Boolean(argus[10]));
                    var couponId = argus[0];
                    var userNamesColl = "";
                    couponMgmt.BindSetting(couponId);
                    $("#txtNewCoupon").attr("disabled", "disabled");
                    $("#txtAmount").attr("disabled", "disabled");
                    $("#txtUsesPerCustomer").attr("disabled", "disabled");
                    $("#ddlCouponType").attr("disabled", "disabled");
                    $("#ddlIsForFreeShipping").attr("disabled", "disabled");
                    $("#spancouponCode").html('');
                    couponMgmt.HideAllCouponDivs();
                    $("#divCouponForm").show();
                    portalusers_customername = '';
                    portalusers_emailid = '';
                    portalusers_username = '';
                    if (argus[8].toLowerCase() == 'no') {
                        $("#ddlIsForFreeShipping").val(1);
                    } else {
                        $("#ddlIsForFreeShipping").val(2);
                    }
                    break;
                default:
                    break;
            }
        },

        BindSetting: function(couponId) {
            this.config.url = this.config.baseURL + "GetSettinKeyValueByCouponID";
            this.config.data = JSON2.stringify({ couponID: couponId, storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },

        DeleteCoupons: function(tblID, argus) {
            switch (tblID) {
                case "gdvCoupons":
                    var properties = {
                        onComplete: function(e) {
                            couponMgmt.DeleteCouponByID(argus[0], e);
                        }
                    };
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this coupn code?</p>", properties);
                    break;
                default:
                    break;
            }
        },

        DeleteCouponByID: function(Ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteCoupons";
                this.config.data = JSON2.stringify({ couponIDs: Ids, storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            }
        },

        DeleteMultipleCoupons: function(Ids, event) {
            couponMgmt.DeleteCouponByID(Ids, event);
        },

        EditCouponsStatus: function(tblID, argus) {
            switch (tblID) {
                case "gdvCouponUser":
                    $("#hdnCouponID").val('');
                    $("#divShowCouponDetails").hide();
                    $("#hdnCouponUserID").val(argus[0]);
                    $("#hdnCouponID").val(argus[3]);
                    $("#txtCouponCode").text(argus[4]);
                    $("#" + lblCouponUserTitle).html("Edit Coupon Provided to: " + argus[5] + " ");
                    $("#txtUserName").text(argus[5]);
                    $("#ddlCouponStatusType ").val(argus[7]);
                    $("#divCouponUserForm").show();
                    break;
                default:
                    break;
            }
        },

        DeleteCouponsUser: function(tblID, argus) {
            switch (tblID) {
                case "gdvCouponUser":
                    var couponUserIDs = argus[0];
                    if (argus[6] == 3) {
                        csscody.alert("<h2>Information Alert</h2><p>This coupon has been provided to '" + argus[5] + "'. Deleting prevents '" + argus[5] + "' from using this coupon!</p>");
                        //return false;
                    }
                    var properties = {
                        onComplete: function(e) {
                            couponMgmt.DeleteCouponUserByID(couponUserIDs, e);
                        }
                    };
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this coupon user?</p>", properties);
                    break;
                default:
                    break;
            }
        },

        DeleteCouponUserByID: function(couponUserIDs, event) {
            if (event) {
                //  var couponID = $("#hdnCouponID").val();
                this.config.url = this.config.baseURL + "DeleteCouponUser";
                this.config.data = JSON2.stringify({ couponUserID: couponUserIDs, storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
            }
        },

        UpdateCouponUser: function() {
            //  var couponID = $("#hdnCouponID").val();
            var couponUserID = $("#hdnCouponUserID").val();
            var couponStatusID = $("#ddlCouponStatusType").val();

            this.config.url = this.config.baseURL + "UpdateCouponUser";
            this.config.data = JSON2.stringify({ couponUserID: couponUserID, couponStatusID: couponStatusID, storeID: storeId, portalID: portalId, cultureName: cultureName });
            this.config.ajaxCallMode = 7;
            this.ajaxCall(this.config);
        },

        SearchCouponPortalUsers: function() {
            var couponId = $("#hdnCouponID").val();
            var searchCustomerName = $('#txtSearchCustomerName').val();
            couponMgmt.BindAllPortalUsersByCouponID(couponId, searchCustomerName);
        },

        SearchCouponDetails: function() {
            var SearchCouponTypeId = $("#ddlSearchCouponType").val();
            var SearchCouponCode = $.trim($("#txtSearchCouponCode").val());
            var validateFromDate = $.trim($("#txtSearchValidateFrom").val());
            var validateToDate = $.trim($("#txtSearchValidateTo").val());
            if (SearchCouponTypeId != "0") {
                SearchCouponTypeId = $("#ddlSearchCouponType").val();
            } else {
                SearchCouponTypeId = null;
            }
            //        if (validateFromDate) {
            //            var splitFromDate = String(validateFromDate).split('/');
            //            validateFromDate = new Date(Date.UTC(splitFromDate[0], splitFromDate[1] * 1 - 1, splitFromDate[2], 12, 0, 0, 0));
            //            validateFromDate = validateFromDate.toMSJSON();
            //        }
            //        if (validateToDate) {
            //            var splitToDate = String(validateToDate).split('/');
            //            validateToDate = new Date(Date.UTC(splitToDate[0], splitToDate[1] * 1 - 1, splitToDate[2], 12, 0, 0, 0));
            //            validateToDate = validateToDate.toMSJSON();
            //        }
            if (validateFromDate.length < 1) {
                validateFromDate = null;
            } else {
                validateFromDate = $.trim($("#txtSearchValidateFrom").val());
            }
            if (validateToDate.length < 1) {
                validateToDate = null;
            } else {
                validateToDate = $.trim($("#txtSearchValidateTo").val());
            }
            if (SearchCouponCode.length < 1) {
                SearchCouponCode = null;
            }

            var searchcouponID = $("#hdnCouponID").val();
            var userName = $.trim($("#txtSearchUserName").val());
            var couponStatusID = $.trim($("#ddlCouponStatus").val());
            if (couponStatusID == "0") {
                couponStatusID = null;
            }
            if (userName.length < 1) {
                userName = null;
            }
            if (seachByCouponUser == 1) {
                couponMgmt.BindCouponUsers(searchcouponID, null, userName, couponStatusID);
            } else {
                couponMgmt.BindCouponDetails(SearchCouponTypeId, SearchCouponCode, validateFromDate, validateToDate);
            }
        },

        ajaxSuccess: function(msg) {
            switch (couponMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $.each(msg.d, function(index, value) {
                        var couponTypeElements = "<option value=" + value.CouponTypeID + ">" + value.CouponType + "</coupon>";
                        $("#ddlCouponType").append(couponTypeElements);
                        $("#ddlSearchCouponType").append(couponTypeElements);
                    });
                    break;
                case 2:
                    $.each(msg.d, function(index, value) {
                        var couponStatusElements = "<option value=" + value.CouponStatusID + ">" + value.CouponStatus + "</option>";
                        $("#ddlCouponStatusType").append(couponStatusElements);
                        $("#ddlCouponStatus").append(couponStatusElements);
                    });
                    break;
                case 3:
                    var checkMessage = msg.d.split(',');
                    couponMgmt.BindCouponDetails(null, null, null, null);
                    couponMgmt.HideAllCouponDivs();
                    debugger;
                    $("#divShowCouponDetails").show();
                    $('#gdvCouponUser').hide();
                    if (checkMessage[1] == "emailSend" && checkMessage[0] == "dataSave") {
                        csscody.info("<h2>Information Message</h2><p>Email has been send successfully for selected customer.<br/> And coupon code has been saved successfully.</p>");
                    } else if (checkMessage[1] == "emailIDBlank" && checkMessage[0] == "dataSave") {
                        if (portalusers_emailid == '') {
                            csscody.info("<h2>Information Message</h2><p>Coupon code has been saved sucessfully.</p>");
                        }
                        //else {
                        //                            csscody.info("<h2>Information Message</h2><p>Coupon code has been saved sucessfully. <br/> But Email send fail for selected Customer.</p>");
                        //                        }
                    } else if (checkMessage[1] == "emailSendFail" && checkMessage[0] == "dataSave") {
                        csscody.info("<h2>Information Message</h2><p>Coupon code has been saved sucessfully. <br/> But Email send fail for selected Customer.</p>");
                    }
                    //                    else if (checkMessage[1] == "emailSend" && checkMessage[0] == "dataSaveFail") {
                    //                        csscody.error("<h2>Error Message</h2><p>Email has been send successfully for selected customer. <br/> But fail to save coupon code!</p>");
                    //                    }
                    else if (checkMessage[1] == "emailSendFail" && checkMessage[0] == "dataSaveFail") {
                        csscody.error("<h2>Error Message</h2><p>Email send fail for selected Customer.<br/> And fail to save coupon code!</p>");
                    }
                    break;
                case 4:
                    $.each(msg.d, function(index, value) {
                        if (value.SettingID == 1) {
                            $("#txtUsesPerCoupon").val(value.SettingValue);
                        } else if (value.SettingID == 2) {
                            $("#txtUsesPerCustomer").val(value.SettingValue);
                        } else {
                            $("#ddlIsForFreeShipping").val(value.SettingValue);
                        }
                    });
                    break;
                case 5:
                    csscody.info("<h2>Information Message</h2><p>Coupon code has been deleted successfully.</p>");
                    couponMgmt.BindCouponDetails(null, null, null, null);
                    break;
                case 6:
                    csscody.info("<h2>Information Message</h2><p>Coupon user has been deleted successfully.</p>");
                    var couponID = $("#hdnCouponID").val();
                    couponMgmt.BindCouponUsers(couponID, null, null, null);
                    break;
                case 7:
                    var couponID = $("#hdnCouponID").val();
                    couponMgmt.BindCouponUsers(couponID, null, null, null);
                    csscody.info("<h2>Information Message</h2><p>Coupon user has been updated successfully.</p>");
                    $("#divShowCouponDetails").show();
                    $("#divCouponUserForm").hide();
                    break;
            }
        },

        ajaxFailure: function(data) {
            switch (couponMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>Error Message</h1><p>Failed to load coupon type!</p>');
                    break;
                case 2:
                    csscody.error('<h1>Error Message</h1><p>Failed to load coupon status!</p>');
                    break;
                case 3:
                    csscody.alert("<h2>Information Alert</h2><p>Failed to send mail to selected customer!</p>");
                    break;
                case 4:
                    csscody.alert("<h2>Information Alert</h2><p>Failed to load coupon details!</p>");
                    break;
                case 5:
                    csscody.alert("<h2>Information Alert</h2><p>Failed to delete!</p>");
                    break;
                case 6:
                    csscody.alert("<h2>Information Alert</h2><p>Failed to delete coupon user!</p>");
                    break;
                case 7:
                    csscody.alert("<h2>Information Alert</h2><p>Failed to update!</p>");
                    break;
            }
        },

        init: function() {
            couponMgmt.LoadCouponAjaxImage();
            $('.cssClassUsesPerCoupon').hide();
            $('.cssClassddlCouponStatus').hide();
            $('.cssClasstxtSearchUserName').hide();
            $('#gdvCouponUser').hide();
            $('#btnBackToCouponTbl').hide();
            $("#divCouponUserForm").hide();
            $("#txtValidFrom").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtValidTo").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtSearchValidateFrom").datepicker({ dateFormat: 'yy/mm/dd' });
            $("#txtSearchValidateTo").datepicker({ dateFormat: 'yy/mm/dd' });
            couponMgmt.BindCouponDetails(null, null, null, null);
            couponMgmt.GetAllCouponType();
            couponMgmt.GetCouponStatus();
            couponMgmt.HideAllCouponDivs();
            $("#divShowCouponDetails").show();
            $(".hasDatepicker").bind("contextmenu", function(e) {
                return false;
            });
            $('#txtUsesPerCustomer,#txtAmount,.hasDatepicker').bind('paste', function(e) {
                e.preventDefault();
            });
            $("#btnAddNewCoupon").bind('click', function() {

                $("#" + lblCouponManageTitle).html("Add new coupon");
                $("#hdnCouponID").val(0);
                $("#txtNewCoupon").attr("disabled", "disabled");
                couponMgmt.ClearCouponForm();
                couponMgmt.HideAllCouponDivs();
                $("#divCouponForm").show();
                $("#spancouponCode").html('');
            });
            $("#btnGenerateCode").click(function() {
                $("#spancouponCode").html('');
            });
            $("#btnCancelCouponUpdate").click(function() {
                couponMgmt.HideAllCouponDivs();
                $("#divShowCouponDetails").show();
            });
            $('#txtAmount').bind('focusout', function() {
                if ($('#txtAmount').val() == 0) {
                    $('#txtAmount').addClass('error');
                    $('#couponAmountErrorLabel').html('Zero coupon amount is not allowed!!').css("color", "red");
                } else {
                    $('#txtAmount').removeClass('error');
                    $('#couponAmountErrorLabel').html('').css("color", "red");
                }
            });
            var c = $("#form1").validate({
                rules: {
                    newCoupon: "required",
                    amount: "required",
                    validateFrom: "required",
                    userPerCustomer: "required",
                    // usesPerCoupon: "required",
                    usesPerCustomer: "required"
                },
                messages: {
                    newCoupon: "at least 2 chars",
                    amount: "*",
                    validateFrom: "*",
                    validateTo: "*",
                    userPerCustomer: "*",
                    //    usesPerCoupon: "*",
                    usesPerCustomer: "*"
                },
                ignore: ':hidden'
            });
            $("#btnSubmitCoupon").click(function() {
                if (Date.parse($('.from').val()) > Date.parse($('.to').val())) {
                    $('.to').parents('td').find('input').css({ "background-color": "#FCC785" });
                    $('#created').html('').html('Valid To date must be higher than Valid From date!');
                    return false;
                } else {
                    $('#created').html('');
                    $('.to').parents('td').find('input').attr("style", '');
                    $(this).removeClass('error');
                    $('.to').parents('td').find('label').remove();
                }
                var UsesPerCoupon = 0;
                var settingId = "1,2,3";
                //  var settingValue = $("#txtUsesPerCoupon").val() + "," + $("#txtUsesPerCustomer").val() + "," + $("#ddlIsForFreeShipping").val();
                var settingValue = UsesPerCoupon + "," + $("#txtUsesPerCustomer").val() + "," + $("#ddlIsForFreeShipping option:selected").text();

                if ($('#ddlCouponType option:selected').val() != 0) {
                    if (c.form()) {
                        if ($("#txtNewCoupon").val() == '') {
                            $("#spancouponCode").html("Coupon Code Is Required").css("color", "red");
                            return false;
                        } else {
                            couponMgmt.AddUpdateCoupon(settingId, settingValue);
                            return false;
                        }
                    } else {
                        return false;
                    }
                } else {
                    $('#ddlCouponType').attr('class', 'cssClassDropDown error');
                    return false;
                }
            });

            $('#btnBackToCouponTbl').click(function() {
                $('#gdvCouponUser').hide();
                $('#btnBackToCouponTbl').hide();
                $('#gdvCoupons').show();
                $('#gdvCoupons_Pagination').show();
                $('#gdvCouponUser_Pagination').hide();
                $("#btnAddNewCoupon").show();
                deleteAllSelectedCouponUser = 0;
                seachByCouponUser = 0;

                $('.cssClassddlCouponStatus').hide();
                $('.cssClasstxtSearchUserName').hide();

                $('.cssClassddlCouponType').show();
                $('.cssClasstxtSearchCouponCode').show();
                $('.cssClasstxtSearchValidateFrom').show();
                $('.cssClasstxtSearchValidateTo').show();
            });

            $("#btnCancelCouponUserUpdate").click(function() {
                $("#divShowCouponDetails").show();
                $("#divCouponUserForm").hide();
                $('#ddlCouponType').val(0);
            });
            $("#btnSubmitCouponUser").click(function() {
                couponMgmt.UpdateCouponUser();
            });
            $("#btnDeleteSelectedCoupon").click(function() {
                var coupon_Ids = '';
                //Get the multiple Ids of the item selected
                $(".CouponChkbox").each(function(i) {
                    if ($(this).attr("checked")) {
                        coupon_Ids += $(this).val() + ',';
                    }
                });
                if (coupon_Ids == "") {
                    if (deleteAllSelectedCouponUser == 1) {
                        csscody.alert('<h2>Information Alert</h2><p>Please select at least one coupon user before delete.</p>');
                        return false;
                    } else {
                        csscody.alert('<h2>Information Alert</h2><p>Please select at least one coupon before delete.</p>');
                        return false;
                    }

                }
                var properties = {
                    onComplete: function(e) {
                        if (deleteAllSelectedCouponUser == 1) {
                            couponMgmt.DeleteCouponUserByID(coupon_Ids, e);
                        } else {
                            couponMgmt.DeleteMultipleCoupons(coupon_Ids, e);
                        }
                    }
                };
                if (deleteAllSelectedCouponUser == 1) {
                    csscody.confirm("<h1>Delete Confirmation</h1><p>Are you sure you want to delete the selected coupon user(s)?</p>", properties);
                } else {
                    csscody.confirm("<h1>Delete Confirmation</h1><p>Are you sure you want to delete the selected coupon code(s)?</p>", properties);
                }
            });
            $('#ddlIsForFreeShipping').change(function() {
                if ($('#ddlIsForFreeShipping option:selected').val() == 2) {
                    $("#txtAmount").attr("disabled", "disabled");
                    $("#txtAmount").val('');
                    $("#txtAmount").parents('tr').hide();
                } else {
                    $("#txtAmount").removeAttr("disabled");
                    $("#txtAmount").parents('tr').show();
                }
            });
            $("#txtValidFrom").bind("change", function() {
                $('#created').html('');
                $('.to').parents('td').find('input').attr("style", '');
                $(this).removeClass('error');
                $('.to').parents('td').find('label').remove();
            });
            $("#txtValidTo").bind("change", function() {
                if ($(this).val() != "") {
                    $('#created').html('');
                    $('.to').parents('td').find('input').attr("style", '');
                    $(this).removeClass('error');
                    $('.to').parents('td').find('label').remove();
                }
                $(this).removeClass('error');
                $('.from').parents('td').find('label').remove();
            });

            $("#txtAmount").keypress(function(e) {
                if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtUsesPerCoupon").keypress(function(e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtUsesPerCustomer").keypress(function(e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $('#ddlSearchCouponType,#txtSearchCouponCode,#txtSearchValidateFrom,#txtSearchValidateTo').keyup(function(event) {
                if (event.keyCode == 13) {
                    couponMgmt.SearchCouponDetails();
                }
            });
        }
    };
    couponMgmt.init();
});