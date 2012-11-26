var PaymentGatewayManage = "";

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
    var errorcode = errorCode; var chkIsActive = true;
    PaymentGatewayManage = {
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
            url: ""
        },
        vars: {
            IsMultipleShipping: ''
        },
        init: function() {
            PaymentGatewayManage.LoadPaymentGateWayStaticImage();
            if (errorcode == 1) {
                PaymentGatewayManage.HideDivsWhenError();
            }
            else {
                PaymentGatewayManage.BindPaymentMethodGrid(null, null);
                PaymentGatewayManage.HideAllDivs();
                $("#divPaymentGateWayManagement").show();
            }
            PaymentGatewayManage.GetOrderStatus();
            $("#btnAddNewPayGateWay").click(function() {
                PaymentGatewayManage.HideAllDivs();
                $("#" + lblLoadMessage).html("");
                $("#" + lblLoadMessage).hide();
                $("#" + lblPaymentGateWay).html("Add New Payment Method");
                $("#divPaymentGateWayForm").show();
            });
            $("#btnSearchPaymentgateway").click(function() {
                PaymentGatewayManage.SearchPaymentgateway();
            });

            $("#btnSearchOrders").click(function() {
                PaymentGatewayManage.SearchOrders();
            });
            $('#txtSearchPaymentGateWayName,#ddlIsActive').keyup(function(event) {
                if (event.keyCode == 13) {
                    PaymentGatewayManage.SearchPaymentgateway();
                }
            });
            $('#txtSearchBillToName,#txtSearchShipToName,#ddlOrderStatus').keyup(function(event) {
                if (event.keyCode == 13) {
                    PaymentGatewayManage.SearchOrders();
                }
            });
            $("#btnDeletePayMethod").click(function() {
                var paymentGateway_Ids = '';
                //Get the multiple Ids of the item selected
                $(".PaymentChkbox").each(function(i) {
                    if ($(this).attr("checked")) {
                        paymentGateway_Ids += $(this).val() + ',';
                    }
                });
                if (paymentGateway_Ids != "") {
                    var properties = { onComplete: function(e) {
                        PaymentGatewayManage.ConfirmDeleteMultiplePayments(paymentGateway_Ids, e);
                    }
                    }
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete the selected payment methods?</p>", properties);
                }
                else {
                    csscody.alert('<h2>Information Alert</h2><p>Please select atleast one payment method before delete!</p>');
                }
            });

            $("#btnCancelPaymentGateway").click(function() {
                PaymentGatewayManage.HideAllDivs();
                $("#divPaymentGateWayManagement").show();
            });
            $("#btnSearchTransaction").click(function() {
                PaymentGatewayManage.BindTransactionLog($.trim($("#txtOrderID").val()), $("#hdnPaymentGatewayIDView").val());
            });

            $("#btnSubmitPayEdit").click(function() {
                PaymentGatewayManage.HideAllDivs();
                PaymentGatewayManage.UpdatePaymentGatewayMethod();
                $("#divPaymentGateWayManagement").show();
            });

            $("#btnCancelPayEdit").click(function() {
                PaymentGatewayManage.HideAllDivs();
                $("#divPaymentGateWayManagement").show();
            });

            $("#btnBacktoOrderView").click(function() {
                $("#dvTransactionDetail").hide();
                $("#divPaymentGateWayManagementEdit").show();
            });
            $("#btnBackOrder").click(function() {
                PaymentGatewayManage.HideAllDivs();
                $("#divPaymentGateWayManagementEdit").show();
            });

            $("#btnBackPaymentEdit").click(function() {
                PaymentGatewayManage.HideAllDivs();
                $("#divPaymentGateWayManagement").show();
            });

            $("#btnBackFromAddNetPaymentForm").click(function() {
                PaymentGatewayManage.HideAllDivs();
                PaymentGatewayManage.BindPaymentMethodGrid(null, null);
                $("#divPaymentGateWayManagement").show();
            });

            $('#btnPrint').click(function() {
                PaymentGatewayManage.printPage();
            });
            if (AspxCommerce.utils.GetUserName() == "superuser") {
                $("#divPaymentGateWayManagement .cssClassHeaderRight .cssClassButtonWrapper").show();
                $("#isActive").show();
                $("p#delete").show();
            } else {
                $("#divPaymentGateWayManagement .cssClassHeaderRight .cssClassButtonWrapper").remove();
                $("#isActive").remove();
                $("p#delete").remove();
            }
        },

        ajaxCall: function(config) {
            $.ajax({
                type: PaymentGatewayManage.config.type,
                contentType: PaymentGatewayManage.config.contentType,
                cache: PaymentGatewayManage.config.cache,
                async: PaymentGatewayManage.config.async,
                url: PaymentGatewayManage.config.url,
                data: PaymentGatewayManage.config.data,
                dataType: PaymentGatewayManage.config.dataType,
                success: PaymentGatewayManage.ajaxSuccess,
                error: PaymentGatewayManage.ajaxFailure
            });
        },
        LoadPaymentGateWayStaticImage: function() {
            $('#ajaxPayementGatewayImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxPaymentGateWayImage2').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        printPage: function() {
            window.print();
            //        var x = '<tr><td ><img class="cssClassImage" src="' + aspxTemplateFolderPath + '/images/AspxCommerce.png"/></td><td class="cssOrderHeading"><h3>Order Details</h3></td></tr>';
            //            $(x).insertBefore('#divPrintOrderDetail tbody:eq(0) tr');
            //            var content = $('#divPrintOrderDetail').html();
            //            $('.cssOrderHeading').parent('tr').remove();
            //            var pwin = window.open('', 'print_content', 'width=100,height=100');
            //            pwin.document.open();
            //            pwin.document.write('<html><HEAD><LINK REL="stylesheet" TYPE="text/css" MEDIA="print, handheld" HREF="' + aspxTemplateFolderPath + '/css/Test/test.css"></HEAD><body onload="window.print()">' + content + '</body></html>');
            //            //pwin.document.write('<html><body onload="window.print()">' + content + '</body></html>');

            //            pwin.document.close();
            //            setTimeout(function() { pwin.close(); }, 1000);
        },

        GenerateOrderDetailsPDF: function() {
            var orderdate = $("#OrderDate").html();
            var storeName = $("#storeName").html();
            var storeDescription = $("#storeDescription").html();
            var paymentGatewayType = $("#PaymentGatewayType").html();
            var paymentMethod = $("#PaymentMethod").html();
            var billingAddress = $("#divBillingAddressDetail").html();

            var headingDescription = {
                headingInfo: {
                    orderDate: orderdate,
                    storeName: storeName,
                    storeDescription: storeDescription,
                    paymentGatewayType: paymentGatewayType,
                    paymentMethod: paymentMethod,
                    billingAddress: billingAddress
                }
            }
            var headerString = JSON.stringify(headingDescription.headingInfo);

            var divContent = $('#orderItemDetail').html();

            var tableDataDescription = {
                totalDataInfo: {
                    itemName: '',
                    shippingMethodName: '',
                    shippingAddress: '',
                    shippingRate: '',
                    price: '',
                    quantity: '',
                    subTotal: ''
                }
            }
            var tdArrayColl = new Array();
            $('#orderItemDetail tbody tr ').each(function() {
                //$(this).find('td').each(function() {
                //  var x = $(this).find('span').length > 0 ? $(this).find('span').html() : $(this).html();
                tableDataDescription.totalDataInfo.itemName = $(this).find(' .cssClassItemName').html();
                tableDataDescription.totalDataInfo.shippingMethodName = $(this).find('.cssClassShippingMethod').html();
                tableDataDescription.totalDataInfo.shippingAddress = $(this).find('.cssClassShippingAdress').html();
                tableDataDescription.totalDataInfo.shippingRate = $(this).find('.cssClassShippingRate').html();
                tableDataDescription.totalDataInfo.price = $(this).find('.cssClassPrice').html();
                tableDataDescription.totalDataInfo.quantity = $(this).find('.cssClassQuantity b').length > 0 ? $(this).find('.cssClassQuantity b').html() : $(this).find('.cssClassQuantity ').html();
                tableDataDescription.totalDataInfo.subTotal = $(this).find('.cssClassSubTotal').html();
                tdArrayColl.push(JSON.stringify(tableDataDescription.totalDataInfo));
                // });
            });
            $("input[id$='HdnValue']").val(tdArrayColl);
            $("input[id$='hdnDescriptionValue']").val(headerString);
        },
        BindTransactionLog: function(table, data) {
            var orderid = data[3];
            var paymentGatewayID = $("#hdnPaymentGatewayIDView").val();
            PaymentGatewayManage.config.url = PaymentGatewayManage.config.baseURL + "GetAllTransactionDetail";
            PaymentGatewayManage.config.data = JSON2.stringify({ storeId: storeId, portalId: portalId, paymentGatewayID: parseInt(paymentGatewayID), orderID: parseInt(orderid) });
            PaymentGatewayManage.config.ajaxCallMode = 5;
            PaymentGatewayManage.ajaxCall(PaymentGatewayManage.config);

        },
        GetDetailTransaction: function(dataall) {
            PaymentGatewayManage.HideAllDivs();
            $("#dvTransactionDetail").show();
            if (dataall == null) {
                // $("#dvTransactionDetail .cssClassCommonBox span.cssNodata").length == 0 ? $("#dvTransactionDetail .cssClassCommonBox").append("<span class='cssNodata'>No Records Found!</span>").find("table").hide() : $("#dvTransactionDetail").find("table").hide().parent('div').find('span.cssNodata').show();
                $("#spanNodata").html("No Records Found!");
                $("#divTransactionDetail").hide();
            } else {
                $("#divTransactionDetail").show();
                $("#spanNodata").html("");
                $("#dvTransactionDetail").find("table").show();
                $("#dvTransactionDetail .cssClassCommonBox  span.cssNodata").hide();
                var data = dataall[0];
                (data.PaymentGatewayTypeName != "") ? $("#lblBindPName").html(data.PaymentGatewayTypeName) : $("#lblBindPName").html("N/A");
                (data.TransactionID != "") ? $("#lblBindtransactionId").html(data.TransactionID) : $("#lblBindtransactionId").html("N/A");
                (data.OrderID != "") ? $("#lblBindOrderId").html(data.OrderID) : $("#lblBindOrderId").html("N/A");
                (data.TotalAmount != "") ? $("#lblBindtotal").html(data.TotalAmount) : $("#lblBindtotal").html("N/A");
                (data.PaymentStatus != "") ? $("#lblBindstatus").html(data.PaymentStatus) : $("#lblBindstatus").html("N/A");
                (data.PayerEmail != "") ? $("#lblBindpayerEmail").html(data.PayerEmail) : $("#lblBindpayerEmail").html("N/A");
                (data.CreditCard != "") ? $("#lblBindcreditCard").html(data.CreditCard) : $("#lblBindcreditCard").html("N/A");
                (data.CustomerID != "") ? $("#lblBindcustomerId").html(data.CustomerID) : $("#lblBindcustomerId").html("N/A");
                (data.SessionCode != "") ? $("#lblBindsessionCode").html(data.SessionCode) : $("#lblBindsessionCode").html("N/A");
                (data.ResponseReasonText != "") ? $("#lblBindresponseText").html(data.ResponseReasonText) : $("#lblBindresponseText").html("N/A");
                //$("#lblBindPName").html(data.PaymentGatewayTypeName); //payment name
                // $("#lblBindtransactionId").html(data.TransactionID); //transactionid
                //$("#lblBindOrderId").html(data.OrderID); //orderid
                //$("#lblBindtotal").html(data.TotalAmount); //total
                // $("#lblBindstatus").html(data.PaymentStatus); //status
                // $("#lblBindcreditCard").html(data.CreditCard); //cc no
                //$("#lblBindcustomerId").html(data.CustomerID); //customerid
                //$("#lblBindsessionCode").html(data.SessionCode); //sessioncode
                // $("#").html(argus[13]); //response code
                //$("#lblBindresponseText").html(data.ResponseReasonText); //response text
                // $("#lblBindAuthCode").html(data.AuthCode); //auth code
                (data.AuthCode != "") ? $("#lblBindAuthCode").html(data.AuthCode) : $("#lblBindAuthCode").html("N/A");
                var date = new Date(parseInt(data.AddedOn.substr(6)));
                $("#lblBindAddedon").html(date.format('yyyy-mm-dd h:MM TT')); //added date
                $("#lblBindCustomerName").html(data.AddedBy);
                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                //username
            }
        },
        BindPaymentMethodGrid: function(paymentgatewayName, isAct) {
            this.config.method = "GetAllPaymentMethod";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvPaymentGateway_pagesize").length > 0) ? $("#gdvPaymentGateway_pagesize :selected").text() : 10;
            $("#gdvPaymentGateway").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                { display: 'PaymentGatewayId', name: 'paymentgateway_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'PaymentChkbox', elemDefault: false, controlclass: 'paymentGatewayHeaderChkbox' },
				{ display: 'Payment Gateway Name', name: 'paymentgateway_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				{ display: 'Is Active', name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'True/False' },
				{ display: 'Is Use', name: 'IsUSE', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'True/False' },
				{ display: 'View', name: 'Paymentedit', btntitle: 'View', cssclass: 'cssClassButtonHeader', controlclass: 'cssClassButtonSubmit', coltype: 'button', align: 'left', url: '', queryPairs: '', showpopup: true, popupid: '', poparguments: '8', popupmethod: 'PaymentGatewayManage.BindOrderList' },
				{ display: 'Setting', name: 'setting', btntitle: 'Setting', cssclass: 'cssClassButtonHeader', controlclass: 'cssClassButtonSubmit', coltype: 'button', align: 'left', url: '', queryPairs: '', showpopup: true, popupid: 'popuprel2', poparguments: '7,8', popupmethod: 'PaymentGatewayManage.LoadControl' },
				{ display: 'HdnEdit', name: 'HdnPaymentedit', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
				{ display: 'HdnSetting', name: 'Hdnsetting', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
				{ display: 'HdnPaymentGatewayID', name: 'HdnPaymentgatewayID', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
				{ display: 'Actions', name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
				],
                buttons: [{ display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'PaymentGatewayManage.EditPaymentMethod', arguments: '1,2,3,4,5' }
                //,{ display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'PaymentGatewayManage.DeletePaymentMethod', arguments: '' }
			    ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { storeId: storeId, portalId: portalId, paymentGatewayName: paymentgatewayName, isActive: isAct },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 4: { sorter: false }, 5: { sorter: false }, 6: { sorter: false }, 7: { sorter: false }, 8: { sorter: false }, 9: { sorter: false} }
            });
        },

        //Convert string to boolean
        Boolean: function(str) {
            switch (str.toLowerCase()) {
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    return false;
            }
        },

        EditPaymentMethod: function(tblID, argus) {
            switch (tblID) {
                case "gdvPaymentGateway":
                    $("#txtPaymentGatewayName").val(argus[3]);
                    $("#hdnPaymentGatewayID").val(argus[0]);
                    $("#chkIsActive").attr('checked', PaymentGatewayManage.Boolean(argus[4]));
                    chkIsActive = PaymentGatewayManage.Boolean(argus[4]);
                    $("#" + lblPaymentGatewayEdit).html("Editing PaymentGateway method: " + argus[3]);
                    PaymentGatewayManage.HideAllDivs();
                    $("#chkIsUse").attr('checked', PaymentGatewayManage.Boolean(argus[5]));
                    $("#divPaymentGatewayEditForm").show();

                    if ($("#btnDeletePay").length > 0) {
                        $("#btnDeletePay").bind("click", function() {
                            var argx = []; argx[0] = $("#hdnPaymentGatewayID").val();
                            PaymentGatewayManage.DeletePaymentMethod("gdvPaymentGateway", argx);
                        });
                    }
                    break;
                default:
                    break;
            }
        },

        HideAllDivs: function() {
            $("#divPaymentGateWayManagement").hide();
            $("#divPaymentGateWayForm").hide();
            $("#divPaymentGatewayEditForm").hide();
            $("#divPaymentEdit").hide();
            $("#divOrderDetailForm").hide();
            $("#divPaymentGateWayManagementEdit").hide();
        },

        HideDivsWhenError: function() {
            PaymentGatewayManage.HideAllDivs();
            $("#divPaymentGateWayForm").show();
        },

        LoadControl: function(argus, PopUpID) {
            var ControlName = argus[0];
            $("#hdnPaymentGatewayID").val(argus[1]);
            if (ControlName != '' && parseInt(ControlName) != 0) {
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "LoadControlHandler.aspx/Result",
                    data: "{ controlName:'" + aspxRootPath + ControlName + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        $('#' + PopUpID).html(response.d);
                        Setting.LoadPaymentGatewaySetting(argus[1], PopUpID);
                    },
                    error: function() {
                        //alert("error");
                        csscody.error('<h2>Error Message</h2><p>Failed to load!</p>');
                    }
                });
            }
            else if ($.trim(parseInt(ControlName)) == 0) {
                csscody.alert('<h2>Information Alert</h2><p>This Payment gateway is not available in your store!</p>');
            }
            else {
                csscody.alert('<h2>Information Alert</h2><p>This Payment gateway doesn\'t seem to need any settings!</p>');
            }
        },

        BindOrderList: function(argus, billNm, shipNm, orderStatusType) {
            $("#hdnPaymentGatewayIDView").val(argus);
            if (billNm == undefined) {
                billNm = null;
            }
            if (shipNm == undefined) {
                shipNm = null;
            }
            if (orderStatusType == undefined) {
                orderStatusType = null;
            }
            var paymentGatewayId = $("#hdnPaymentGatewayIDView").val();
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvPaymentGatewayEdit_pagesize").length > 0) ? $("#gdvPaymentGatewayEdit_pagesize :selected").text() : 10;
            PaymentGatewayManage.HideAllDivs();
            $("#divPaymentGateWayManagementEdit").show();
            $("#gdvPaymentGatewayEdit").sagegrid({
                url: PaymentGatewayManage.config.baseURL + "GetOrderDetailsbyPayID",
                colModel: [
                        { display: 'PaymentGatewayID', name: 'paymentgateway_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'OrderDetailChkBox', elemDefault: false, controlclass: 'paymentOrderDetailChkbox' },
				        { display: 'Order ID', name: 'order_Id', cssclass: 'cssClassLinkHeader', controlclass: 'cssClassGridLink', coltype: 'link', align: 'left', url: '', queryPairs: '', showpopup: true, popupid: '', poparguments: '1,8', popupmethod: 'PaymentGatewayManage.LoadOrderDetails' },
				        { display: 'Store ID', name: 'store_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
				        { display: 'AddedOn', name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
				        { display: 'Bill to Name', name: 'bill_to_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				        { display: 'Ship to Name', name: 'ship_to_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				        { display: 'Grand Total', name: 'grand_total', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
				        { display: 'Order Status', name: 'status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'IsMultipleShipping', name: 'IsMultiShipping', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', hide: true, format: 'Yes/No' },
                  { display: 'Actions', name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
				 ],
                buttons: [{ display: 'View Order', name: 'view', enable: true, _event: 'click', trigger: '1', callMethod: 'PaymentGatewayManage.LoadOrderDetails', arguments: '1,8' }
                , { display: 'View Transaction Log', name: 'view log', enable: true, _event: 'click', trigger: '2', callMethod: 'PaymentGatewayManage.BindTransactionLog', arguments: '1,2' }
			   ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { billToName: billNm, shipToName: shipNm, orderStatusName: orderStatusType, paymentGatewayID: paymentGatewayId, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 8: { sorter: false }, 9: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

        },

        GetOrderStatus: function() {
            this.config.url = this.config.baseURL + "GetStatusList";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, cultureName: cultureName });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        LoadOrderDetails: function(table, argus) {
            $('#' + lblOrderDetailForm).html("Order ID: #" + argus[3]);
            var orderID = argus[3];
            PaymentGatewayManage.vars.IsMultipleShipping = argus[4];
            // PaymentGatewayManage.config.url = PaymentGatewayManage.config.baseURL + "GetAllOrderDetailsByOrderID";
            PaymentGatewayManage.config.url = PaymentGatewayManage.config.baseURL + "GetAllOrderDetailsForView";
            PaymentGatewayManage.config.data = JSON2.stringify({ orderId: orderID, storeId: storeId, portalId: portalId, cultureName: cultureName });
            PaymentGatewayManage.config.ajaxCallMode = 2;
            PaymentGatewayManage.ajaxCall(PaymentGatewayManage.config);
        },

        UpdatePaymentGatewayMethod: function() {
            var paymentGatewayID = $("#hdnPaymentGatewayID").val();
            var paymentMethodName = $("#txtPaymentGatewayName").val();
            var isAct;
            if ($("#chkIsActive").length > 0)
                isAct = $("#chkIsActive").attr('checked');
            else
                isAct = chkIsActive;

            var isUse = $("#chkIsUse").attr('checked');
            this.config.url = this.config.baseURL + "UpdatePaymentMethod";
            this.config.data = JSON2.stringify({ storeId: storeId, portalId: portalId, paymentGatewayID: paymentGatewayID, paymentGatewayName: paymentMethodName, isActive: isAct, isUse: isUse, userName: userName });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },

        DeletePaymentMethod: function(tblID, argus) {
            switch (tblID) {
                case "gdvPaymentGateway":
                    var properties = { onComplete: function(e) {
                        PaymentGatewayManage.DeletePaymentGateMethod(argus[0], e);
                    }
                    }
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this payment gateway?</p>", properties);
                    break;
                default:
                    break;
            }
        },

        ConfirmDeleteMultiplePayments: function(Ids, event) {
            if (event) {
                PaymentGatewayManage.DeletePaymentGateMethod(Ids, event);
            }
        },

        DeletePaymentGateMethod: function(_paymentGatewayID, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeletePaymentMethod";
                this.config.data = JSON2.stringify({ paymentGatewayID: _paymentGatewayID, storeId: storeId, portalId: portalId, userName: userName });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
                return false;
            }
            else return false;
        },

        SearchPaymentgateway: function() {
            var paymentgatewayName = $.trim($("#txtSearchPaymentGateWayName").val());
            var isAct = $.trim($("#ddlIsActive").val()) == "" ? null : $.trim($("#ddlIsActive").val()) == 0 ? true : false;
            if (paymentgatewayName.length < 1) {
                paymentgatewayName = null;
            }
            PaymentGatewayManage.BindPaymentMethodGrid(paymentgatewayName, isAct);
        },

        SearchOrders: function() {
            var paymentGatewayID = $("#hdnPaymentGatewayIDView").val();
            var billNm = $.trim($("#txtSearchBillToName").val());
            var shipNm = $.trim($("#txtSearchShipToName").val());
            var orderStatusType = '';
            if (billNm.length < 1) {
                billNm = null;
            }
            if (shipNm.length < 1) {
                shipNm = null;
            }
            if ($("#ddlOrderStatus").val() != "0") {
                orderStatusType = $.trim($("#ddlOrderStatus :selected").text());
            }
            else {
                orderStatusType = null;
            }
            PaymentGatewayManage.BindOrderList(paymentGatewayID, billNm, shipNm, orderStatusType);
        },
        ajaxSuccess: function(data) {
            switch (PaymentGatewayManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $.each(data.d, function(index, item) {
                        var StatusElements = "<option value=" + item.OrderStatusID + ">" + item.OrderStatusName + "</option>";
                        $("#ddlOrderStatus").append(StatusElements);
                    });
                    break;
                case 2:
                    var elements = '';
                    var tableElements = '';
                    var grandTotal = '';
                    var couponAmount = '';
                    var taxTotal = '';
                    var shippingCost = '';
                    var discountAmount = '';
                    var additionalNote = "";
                    Array.prototype.clean = function(deleteValue) {
                        for (var i = 0; i < this.length; i++) {
                            if (this[i] == deleteValue) {
                                this.splice(i, 1);
                                i--;
                            }
                        }
                        return this;
                    };
                    tableElements += '<table cellspacing="0" cellpadding="0" border="0" width="100%"><thead><tr class="cssClassHeading"><td>Item Name</td><td>Shipping Method</td><td>Shipping Address</td><td>Shipping Rate</td><td>Price</td><td >Quantity</td><td>SubTotal</td></tr></thead>';
                    tableElements += '<tbody><tr>';
                    $.each(data.d, function(index, value) {
                        if (index < 1) {
                            var billAdd = '';
                            var arrBill;
                            arrBill = value.BillingAddress.split(",");
                            // arrBill.clean(" ");                           
                            billAdd += '<b>Billing Address:</b></br>';
                            billAdd += arrBill[0] + ' ' + arrBill[1] + '</br>';
                            if (arrBill[2] != undefined && arrBill[2] != " ") { billAdd += arrBill[2] + '</br>'; }
                            if (arrBill[3] != undefined && arrBill[3] != " ") { billAdd += arrBill[3] + '</br>'; }
                            if (arrBill[4] != undefined && arrBill[4] != " ") { billAdd += arrBill[4] + '</br>'; }
                            if (arrBill[5] != undefined && arrBill[5] != " ") { billAdd += arrBill[5] + ' '; }
                            if (arrBill[6] != undefined && arrBill[6] != " ") { billAdd += arrBill[6] + ' '; }
                            if (arrBill[7] != undefined && arrBill[7] != " ") { billAdd += arrBill[7] + '</br>'; }
                            if (arrBill[8] != undefined && arrBill[8] != " ") { billAdd += arrBill[8] + '</br>'; }
                            if (arrBill[9] != undefined && arrBill[9] != " ") { billAdd += arrBill[9] + '</br>'; }
                            if (arrBill[10] != undefined && arrBill[10] != " ") { billAdd += arrBill[10] + ' '; }
                            if (arrBill[11] != undefined && arrBill[11] != " ") { billAdd += arrBill[11] + '</br>'; }
                            if (arrBill[12] != undefined && arrBill[12] != " ") { billAdd += arrBill[12] + '</br>'; }
                            if (arrBill[13] != undefined && arrBill[13] != " ") { billAdd += arrBill[13] + '</br>'; }

                            $("#divBillingAddressDetail").html(billAdd);
                            $("#OrderDate").html(value.OrderedDate);
                            $("#PaymentGatewayType").html(value.PaymentGatewayTypeName);
                            $("#PaymentMethod").html(value.PaymentMethodName);
                            additionalNote = value.Remarks;
                            $("#storeName").html(value.StoreName);
                            $("#storeDescription").html(value.StoreDescription);
                        }
                        var shippingAddress = new Array();
                        var shipAdd = '';
                        shippingAddress = value.ShippingAddress.replace(",", " ").split(",");

                        shippingAddress.clean(" ");
                        //                        tableElements += '<table cellspacing="0" cellpadding="0" border="0" width="100%"><thead><tr><td>Item Name</td></tr><tr><td>Shipping Method</td></tr><tr><td>Shipping Address</td></tr><tr><td>Shipping Rate</td></tr><tr><td>Price</td></tr><tr><td>Quantity</td></tr><tr><td>SubTotal</td></tr></thead>';
                        //                        tableElements += '<tbody><tr>';
                        if (value.CostVariants != "") {
                            tableElements += '<td valign="top" class="cssClassItemName">' + value.ItemName + ' ' + '(' + value.CostVariants + ')' + '</td>';
                        }
                        else {
                            tableElements += '<td valign="top" class="cssClassItemName">' + value.ItemName + '</td>';
                        }
                        // tableElements += '<td>' + value.ItemName + '<br/>' + value.CostVariants + '</td>';
                        tableElements += '<td valign="top" class="cssClassShippingMethod">' + value.ShippingMethod + '</td>';
                        tableElements += '<td valign="top" class="cssClassShippingAdress">' + shippingAddress + '</td>';
                        tableElements += '<td valign="top" class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassShippingRate" >' + value.ShippingRate.toFixed(2) + '</span></td>';
                        tableElements += '<td valign="top" class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassPrice" >' + value.Price.toFixed(2) + '</span></td>';
                        tableElements += '<td valign="top" class="cssClassQuantity">' + value.Quantity + '</td>';
                        tableElements += '<td valign="top" class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassSubTotal" >' + (value.Price * value.Quantity).toFixed(2) + '</span></td>';
                        tableElements += '</tr>';
                        if (index == 0) {
                            taxTotal = '<tr>';
                            taxTotal += '<td class="cssClassItemName"></td><td class="cssClassShippingMethod"></td><td class="cssClassShippingAdress"></td><td class="cssClassShippingRate"></td><td class="cssClassPrice"></td><td class="cssClassLabel cssClassQuantity"><b>Tax Total:</b></td>';
                            taxTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassSubTotal">' + value.TaxTotal.toFixed(2) + '</span></td>';
                            taxTotal += '</tr>';
                            shippingCost = '<tr>';
                            shippingCost += '<td class="cssClassItemName"></td><td class="cssClassShippingMethod"></td><td class="cssClassShippingAdress"></td><td class="cssClassShippingRate"></td><td class="cssClassPrice"></td><td class="cssClassLabel cssClassQuantity"><b>Shipping Cost:</b></td>';
                            shippingCost += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassSubTotal">' + value.ShippingCost.toFixed(2) + '</span></td>';
                            shippingCost += '</tr>';
                            discountAmount = '<tr>';
                            discountAmount += '<td class="cssClassItemName"></td><td class="cssClassShippingMethod"></td><td class="cssClassShippingAdress"></td><td class="cssClassShippingRate"></td><td class="cssClassPrice"></td><td class="cssClassLabel cssClassQuantity"><b>Discount Amount:</b></td>';
                            discountAmount += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassSubTotal">' + value.DiscountAmount.toFixed(2) + '</span></td>';
                            discountAmount += '</tr>';
                            couponAmount = '<tr>';
                            couponAmount += '<td class="cssClassItemName"></td><td class="cssClassShippingMethod"></td><td class="cssClassShippingAdress"></td><td class="cssClassShippingRate"></td><td class="cssClassPrice"></td><td class="cssClassLabel cssClassQuantity"><b>Coupon Amount:</b></td>';
                            couponAmount += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassSubTotal">' + value.CouponAmount.toFixed(2) + '</span></span></td>';
                            couponAmount += '</tr>';
                            grandTotal = '<tr>';
                            grandTotal += '<td class="cssClassItemName"></td><td class="cssClassShippingMethod"></td><td class="cssClassShippingAdress"></td><td class="cssClassShippingRate"></td><td class="cssClassPrice"></td><td class="cssClassLabel cssClassQuantity"><b>Grand Total:</b></td>';
                            grandTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency cssClassSubTotal">' + value.GrandTotal.toFixed(2) + '</span></td>';
                            grandTotal += '</tr>';
                        }
                    });
                    tableElements += '</tbody></table>';
                    $("#orderItemDetail").html(tableElements);
                    $("#orderItemDetail").find('table>tbody').append(taxTotal);
                    $("#orderItemDetail").find('table>tbody').append(shippingCost);
                    $("#orderItemDetail").find('table>tbody').append(discountAmount);
                    $("#orderItemDetail").find('table>tbody').append(couponAmount);
                    $("#orderItemDetail").find('table>tbody').append(grandTotal);
                    $("#orderItemDetail").find("table>tbody tr:even").addClass("cssClassAlternativeEven");
                    $("#orderItemDetail").find("table>tbody tr:odd").addClass("cssClassAlternativeOdd");
                    if (additionalNote != '' && additionalNote != undefined) {
                        $(".remarks").html("").html("*Additional Note :- '" + additionalNote + "'");
                    } else {
                        $(".remarks").html("");
                    }
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + 'en-US' + '' });
                    PaymentGatewayManage.HideAllDivs();
                    $("#divOrderDetailForm").show();
                    break;
                case 3:
                    PaymentGatewayManage.BindPaymentMethodGrid(null, null);
                    break;
                case 4:
                    //  csscody.info('<h2>Information Message</h2><p>Payment gateway method has been deleted successfully.</p>');
                    window.location.href = urlPath + "?deleted=true";
                    PaymentGatewayManage.BindPaymentMethodGrid(null, null);
                    PaymentGatewayManage.HideAllDivs();

                    $("#divPaymentGateWayManagement").show();
                    break;
                case 5:
                    if (data.d.length > 0) {
                        PaymentGatewayManage.GetDetailTransaction(data.d);
                    }
                    else { PaymentGatewayManage.GetDetailTransaction(null); }
                    break;
            }
        }
    }
    PaymentGatewayManage.init();
});