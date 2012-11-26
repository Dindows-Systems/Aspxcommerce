var OrderManage;
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
    var senderEmail = '<%=SenderEmail %>';
    var msgbody = '';

    OrderManage = {
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
            OrderManage.LoadOrderMgmtStaticImage();
            OrderManage.HideAll();
            $("#divOrderDetails").show();
            OrderManage.BindOrderDetails(null, null);
            OrderManage.GetOrderStatus();
            $("#btnBack").click(function() {
                OrderManage.HideAll();
                $("#divOrderDetails").show();
            });
            $("#btnSPBack").click(function() {
                OrderManage.HideAll();
                $("#divOrderDetails").show();
                $("#hdnReceiverEmail").val('');
            });
            $("#btnUpdateOrderStatus").click(function() {
                var orderId = $("#hdnOrderID").val();
                OrderManage.SaveorderStatus(orderId);
                //                if (!$("#hdnReceiverEmail").val()) {
                //                    OrderManage.SaveorderStatus(orderId);
                //                } else {
                //                    OrderManage.SaveorderStatus(orderId);
                //                    OrderManage.NotifyOrderStatusUpdate(orderId);
                //                }
            });
            $('#txtCustomerName,#ddlOrderStatus').keyup(function(event) {
                if (event.keyCode == 13) {
                    OrderManage.SearchOrders();
                }
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: OrderManage.config.type,
                contentType: OrderManage.config.contentType,
                cache: OrderManage.config.cache,
                async: OrderManage.config.async,
                data: OrderManage.config.data,
                dataType: OrderManage.config.dataType,
                url: OrderManage.config.url,
                success: OrderManage.ajaxSuccess,
                error: OrderManage.ajaxFailure
            });
        },

        LoadOrderMgmtStaticImage: function() {
            $('#ajaxOrderMgmtStaticImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        HideAll: function() {
            $("#divOrderDetails").hide();
            $("#divOrderDetailForm").hide();
            $("#divEditOrderStatus").hide();
        },
        GetDataForExport: function() {
            this.config.url = this.config.baseURL + "GetOrderDetails";
            this.config.data = JSON2.stringify({ offset: 1, limit: null, storeID: storeId, portalID: portalId, cultureName: cultureName, orderStatusName: null, userName: null });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
        },
        ExportOrdersToCsvData: function() {
            OrderManage.GetDataForExport();
        },
        ExportDivDataToExcel: function() {
            OrderManage.GetDataForExport();
        },

        BindOrderDetails: function(customerNm, orderStatusType) {
            this.config.method = "GetOrderDetails";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvOrderDetails_pagesize").length > 0) ? $("#gdvOrderDetails_pagesize :selected").text() : 10;

            $("#gdvOrderDetails").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'Order ID', name: 'order_id', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left' },
                    { display: 'Invoice Number', name: 'invoice_number', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'CustomerID', name: 'customerID', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Customer Name', name: 'customer_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Email', name: 'email', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Order Status', name: 'order_status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Grand Total', name: 'grand_total', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: 'Payment Gateway Type Name', name: 'payment_gateway_typename', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Payment Method Name', name: 'payment_method_name', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Ordered Date', name: 'ordered_date', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                    { display: 'View', name: 'view', enable: true, _event: 'click', trigger: '3', callMethod: 'OrderManage.ViewOrders' },
                    { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'OrderManage.EditOrders', arguments: '1,2,3,4,5,6,7,8,9,10' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { storeID: storeId, portalID: portalId, cultureName: cultureName, orderStatusName: orderStatusType, userName: customerNm },
                current: current_,
                pnew: offset_,
                sortcol: { 10: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },

        ViewOrders: function(tblID, argus) {
            switch (tblID) {
                case "gdvOrderDetails":
                    OrderManage.HideAll();
                    $('#' + lblOrderForm1).html("Order ID: #" + argus[0]);
                    $("#divOrderDetailForm").show();
                    OrderManage.BindAllOrderDetailsForm(argus[0]);
                    break;
                default:
                    break;
            }
        },

        EditOrders: function(tblID, argus) {
            switch (tblID) {
                case "gdvOrderDetails":
                    OrderManage.HideAll();
                    $("#divEditOrderStatus").show();
                    $("#customerNameEdit").html(argus[5]);
                    $("#spanOrderDate").html(argus[11]);
                    $("#OrderGrandTotal").html(argus[8]);
                    $('#selectStatus').val($('#ddlOrderStatus option:contains(' + argus[7] + ')').attr('value'));
                    $("#hdnOrderID").val(argus[0]);
                    $("#hdnReceiverEmail").val(argus[6]);
                    $("#hdnInvoice").val(argus[3]);
                    break;
            }
        },

        SaveorderStatus: function(orderId) {
            AspxCommerce.CheckSessionActive();
            if (AspxCommerce.vars.IsAlive) {
                var StatusID = $("#selectStatus").val();
                this.config.url = this.config.baseURL + "SaveOrderStatus";
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, orderStatusID: StatusID, orderID: orderId });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            } else {
                window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + 'Login.aspx';
            }
        },

        getItemInvoiceDetail: function(orderId) {
            //OrderManage.vars.orderStatusName = $('#selectStatus option:selected').text();
            this.config.url = this.config.baseURL + "GetInvoiceDetailsByOrderID";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName, orderID: orderId });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);

        },

        NotifyOrderStatusUpdate: function(orderId) {
            OrderManage.getItemInvoiceDetail(orderId);
        },

        BindAllOrderDetailsForm: function(argus) {
            var orderId = argus;
            this.config.url = this.config.baseURL + "GetAllOrderDetailsForView";
            this.config.data = JSON2.stringify({ orderId: orderId, storeId: storeId, portalId: portalId, cultureName: cultureName });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },

        GetOrderStatus: function() {
            this.config.url = this.config.baseURL + "GetStatusList";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, cultureName: cultureName });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },
        BindDataForExport: function(data) {
            var exportData = '<thead><tr><th>Order ID</th><th>Invoice Number</th><th>Customer ID</th><th>Customer Name</th><th>Email</th><th>Order Status</th><th>Grand Total</th><th>Payment Gateway Type Name</th><th>Payment Method Name</th><th>Ordered Date</th></tr><tbody>';
            if (data.d.length > 0) {
                $.each(data.d, function(index, value) {
                    exportData += '<tr><td>' + value.OrderID + '</td><td>' + value.InVoiceNumber + '</td>';
                    exportData += '<td>' + value.CustomerID + '</td><td>' + value.UserName + '</td>';
                    exportData += '<td>' + value.Email + '</td><td>' + value.OrderStatus + '</td>';
                    exportData += '<td>' + value.GrandTotal + '</td><td>' + value.PaymentGatewayTypeName + '</td>';
                    exportData += '<td>' + value.PaymentMethodName + '</td><td>' + value.OrderedDate + '</td></tr>';
                });
            } else {
                exportData += '<tr><td>No Records Found!</td></tr>';
            }
            exportData += '</tbody>';
            $('#orderExportData').html(exportData);
            $("input[id$='HdnValue']").val('<table>' + exportData + '</table>');
            $("input[id$='_csvOrderHdnValue']").val($('#orderExportData').table2CSV());
            $('#orderExportData').html('');
        },
        SearchOrders: function() {
            var customerNm = $.trim($("#txtCustomerName").val());
            var orderStatusType = '';
            if (customerNm.length < 1) {
                customerNm = null;
            }
            if ($("#ddlOrderStatus").val() == "0") {
                orderStatusType = null;
            } else {
                orderStatusType = $("#ddlOrderStatus option:selected").text();
            }
            OrderManage.BindOrderDetails(customerNm, orderStatusType);
        },
        ajaxSuccess: function(data) {
            switch (OrderManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    OrderManage.BindOrderDetails(null, null);
                    // csscody.info("<h2>Sucess Message</h2><p>Order status updated sucessfully.</p>");
                    var orderId = $("#hdnOrderID").val();
                    if (data.d) {
                        OrderManage.NotifyOrderStatusUpdate(orderId);
                    } else {
                        csscody.alert('<h2>Information Message</h2><p>Failed to update order status and Failed to send email to the customer!</p>');
                    }
                    OrderManage.HideAll();
                    $("#divOrderDetails").show();

                    break;
                case 2:
                    var orderStatusName = $('#selectStatus option:selected').text();
                    //var param = JSON2.stringify({ storeID: storeId, portalID: portalId, cultureName: cultureName, orderID: orderId });
                    var additional = "";
                    var billingshipping = "";
                    var itemDetails = "";
                    var itemOrderDetails = "";
                    var span = '';
                    var span1 = '';
                    var orderID = 0;
                    if (data.d.length > 0) {
                        $.each(data.d, function(index, item) {
                            if (index == 0) {
                                additional += orderStatusName + "#";
                                additional += item.StoreName + "#";
                                additional += item.StoreDescription + "#";
                                additional += $('#customerNameEdit').html() + "#";
                                additional += item.OrderID + "#";
                                additional += item.PaymentMethodName + "#";
                                if (item.IsMultipleShipping != true) {
                                    additional += item.ShippingMethodName + "#";
                                } else {
                                    additional += 'Multiple Shipping Exist' + "#";
                                }
                                additional += $("#hdnInvoice").val();

                                span = ' <table cellspacing="1" cellpadding="1" border="0" align="left" width="300"> <tbody> <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: bold 14px Arial, Helvetica, sans-serif;"> <strong>Billing To:</strong> </td> </tr>';
                                if (item.BillingName != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.BillingName + '</td></tr>';

                                if (item.Address1 != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Address1 + '</td></tr>';

                                if (item.Address2 != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Address2 + '</td></tr>';

                                if (item.Company != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Company + '</td></tr>';

                                if (item.City != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.City + '</td></tr>';

                                if (item.State != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.State + '</td></tr>';

                                if (item.Zip != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Zip + '</td></tr>';
                                if (item.Country != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Country + '</td></tr>';

                                if (item.Email != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Email + '</td></tr>';

                                if (item.Phone != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Phone + '</td></tr>';

                                if (item.Mobile != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Mobile + '</td></tr>';

                                if (item.Fax != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Fax + '</td></tr>';

                                if (item.Website != "")
                                    span += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.Website + '</td></tr>';

                                span += "</table>";
                                if (item.IsMultipleShipping != true) {
                                    span1 = '<table cellspacing="1" cellpadding="1" border="0" align="left" width="280"> <tbody> <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: bold 14px Arial, Helvetica, sans-serif;"> Shipping To: </td> </tr>';
                                    if (item.ShippingName != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShippingName + '</td></tr>';

                                    if (item.ShipAddress1 != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipAddress1 + '</td></tr>';

                                    if (item.ShipAddress2 != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipAddress2 + '</td></tr>';

                                    if (item.ShipCompany != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipCompany + '</td></tr>';

                                    if (item.ShipCity != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipCity + '</td></tr>';

                                    if (item.ShipState != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipState + '</td></tr>';

                                    if (item.ShipZip != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipZip + '</td></tr>';

                                    if (item.ShipCountry != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipCountry + '</td></tr>';

                                    if (item.ShipEmail != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipEmail + '</td></tr>';

                                    if (item.ShipPhone != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipPhone + '</td></tr>';

                                    if (item.ShipMobile != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipMobile + '</td></tr>';

                                    if (item.ShipFax != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipFax + '</td></tr>';

                                    if (item.ShipWebsite != "")
                                        span1 += ' <tr> <td style="border-bottom: thin dashed #d1d1d1; padding: 10px 0 5px 10px; font: normal 12px Arial, Helvetica, sans-serif">' + item.ShipWebsite + '</td></tr>';

                                    // var itemOrderDetails = '<table cellspacing="0" cellpadding="0" border="0" width="620" style="border: 1px solid #dcdccc;"><thead><tr style="background: #e5e5de;"> <td width="383" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>Item Name</strong> </td> <td width="46" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>Price</strong> </td> <td width="89" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>Qty</strong> </td> <td width="100" style="padding: 10px; padding-left: 5px"> <strong>Subtotal</strong> </td> </tr> </thead>';
                                    span1 += "</table>";
                                    itemOrderDetails = ' <table cellspacing="0" cellpadding="0" border="0" width="620" style="border: 1px solid #dcdccc;"> <tbody> <tr style="background: #e5e5de;"> <td width="383" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>Item Name</strong> </td> <td width="46" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>Price</strong> </td> <td width="89" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>Qty</strong> </td> <td width="100" style="padding: 10px; padding-left: 5px"> <strong>Subtotal</strong> </td> </tr> ';
                                    // var itemOrderDetails = '<table width="100%" border="0"  cellpadding="0" cellspacing="1" style="" ><thead ><tr ><th ><b>ItemName</b></th><th ><b>Price</b></th><th style="background-color:#eeeeee;padding:6px 10px;white-space:nowrap;font-size:12px;font-family:Arial" ><b>Quantity</b></th><th style="background-color:#eeeeee;padding:6px 10px;white-space:nowrap;font-size:12px;font-family:Arial" ><b>SubTotal</b></th></td></tr></thead><tbody>';
                                    $.each(data.d, function(index, item) {
                                        var cv = "";
                                        if (item.CostVariants != "") {
                                            cv = "(" + item.CostVariants + ")";
                                        }
                                        itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + item.ItemName + cv + '</td>';
                                        itemOrderDetails += '<td  style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f">' + item.Price.toFixed(2) + '</td>';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + item.Quantity + '</td>';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + item.SubTotal.toFixed(2) + '</td></tr>';
                                    });
                                    if (index == 0) {
                                        itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;" rowspan="5" colspan="2"> &nbsp; </td> <td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;"> SubTotal </td> ';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.GrandSubTotal.toFixed(2) + ' </td></tr>';
                                        itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;"> Taxes </td> ';
                                        //<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;" rowspan="4" colspan="2"> &nbsp; </td> 
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.TaxTotal.toFixed(2) + '</td></tr>';
                                        itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">Shipping Cost </td>';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.ShippingCost.toFixed(2) + '</td></tr>';
                                        itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">Discount</td>';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.DiscountAmount.toFixed(2) + '</td></tr>';
                                        itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">Coupon </td>';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.CouponAmount.toFixed(2) + '</td></tr>';
                                        itemOrderDetails += '<tr><td style="padding: 5px; border-right: 1px solid #dcdccc;" colspan="2"> &nbsp; </td> <td style="padding: 5px; border-right: 1px solid #dcdccc; font: bold 14px Arial, Helvetica, sans-serif; color: #000;"> Total Cost </td>';
                                        itemOrderDetails += ' <td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f">' + item.GrandTotal.toFixed(2) + '</td></tr>';
                                    }
                                    itemOrderDetails += '</table>';
                                } else {
                                    itemOrderDetails = ""; //'<table style="border:1px solid #e6e6e6;" width="80%" border="0" cellspacing="1" cellpadding="0"><thead><tr  width="70%" style="background-color:#eeeeee;padding:6px 10px;white-space:nowrap;font-size:12px;font-family:Arial" ><th><b>ItemName</b></th><th style="background-color:#eeeeee;padding:6px 10px;white-space:nowrap;font-size:12px;font-family:Arial" ><b>Price</b></th><th style="background-color:#eeeeee;padding:6px 10px;white-space:nowrap;font-size:12px;font-family:Arial"  ><b>Quantity</b></th><th style="background-color:#eeeeee;padding:6px 10px;white-space:nowrap;font-size:12px;font-family:Arial" ><b>Shipping Method</b></th><th style="background-color:#eeeeee;padding:6px 10px;white-space:nowrap;font-size:12px;font-family:Arial" ><b>Shipping To</b></th><thstyle="background-color:#eeeeee;padding:6px 10px;white-space:nowrap;font-size:12px;font-family:Arial" ><b>SubTotal</b></th></tr></thead><tbody style="background-color: rgb(255, 255, 255);">';
                                    itemOrderDetails = '<table cellspacing="0" cellpadding="0" border="0" width="620" style="border: 1px solid #dcdccc;"><thead><tr style="background: #e5e5de;"> <td width="383" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>Item Name</strong> </td> <td width="46" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>Price</strong> </td> <td width="89" style="padding: 10px; border-right: 1px solid #dcdccc; padding-left: 5px"> <strong>Qty</strong> </td> <td width="100" style="padding: 10px; padding-left: 5px"> <strong>Shipping Method</strong> </td> <td width="100" style="padding: 10px; padding-left: 5px"> <strong>Shipping To</strong> </td> <td width="100" style="padding: 10px; padding-left: 5px"> <strong>Subtotal</strong> </td> </tr> </thead>';
                                    // var itemOrderDetails = '<table width="100%" border="0"  cellpadding="0" cellspacing="1" style="" ><thead ><tr ><th ><b>ItemName</b></th><th style="background-color:#eeeeee;padding:6px 10px;white-space:nowrap;font-size:12px;font-family:Arial" ><b>Price</b></th><th style="background-color:#eeeeee;padding:6px 10px;white-space:nowrap;font-size:12px;font-family:Arial" ><b>Quantity</b></th><th style="background-color:#eeeeee;padding:6px 10px;white-space:nowrap;font-size:12px;font-family:Arial" ><b>SubTotal</b></th></td></tr></thead><tbody>';
                                    $.each(msg.d, function(index, item) {
                                        var cv = "";
                                        if (item.CostVariants != "") {
                                            cv = "(" + item.CostVariants + ")";
                                        }
                                        itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + item.ItemName + cv + '</td>';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + item.Price.toFixed(2) + '</td>';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f">' + item.Quantity + '</td>';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + item.ShippingMethodName + '</td>';
                                        var shippingDetails = "";
                                        if (item.ShippingName != "")
                                            shippingDetails += "<br/>" + item.ShippingName;

                                        if (item.ShipAddress1 != "")
                                            shippingDetails += ",<br/>" + item.ShipAddress1;

                                        if (item.ShipAddress2 != "")
                                            shippingDetails += "," + item.ShipAddress2;

                                        if (item.ShipCompany != "")
                                            shippingDetails += ",<br/>" + item.ShipCompany;

                                        if (item.ShipCity != "")
                                            shippingDetails += ",<br/>" + item.ShipCity;

                                        if (item.ShipState != "")
                                            shippingDetails += "," + item.ShipState;

                                        if (item.ShipZip != "")
                                            shippingDetails += ",<br/>" + item.ShipZip;

                                        if (item.ShipCountry != "")
                                            shippingDetails += ",<br/>" + item.ShipCountry;

                                        if (item.ShipEmail != "")
                                            shippingDetails += ",<br/>" + item.ShipEmail;

                                        if (item.ShipPhone != "")
                                            shippingDetails += ",<br/>" + item.ShipPhone;

                                        if (item.ShipMobile != "")
                                            shippingDetails += ", " + item.ShipMobile;

                                        if (item.ShipFax != "")
                                            shippingDetails += ",<br/>" + item.ShipFax;

                                        if (item.ShipWebsite != "")
                                            shippingDetails += ",<br/>" + item.ShipWebsite;

                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f" >' + shippingDetails + "</td>";
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f"  >' + item.SubTotal.toFixed(2) + "</td></tr>";
                                    });

                                    if (index == 0) {
                                        itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;" rowspan="5" colspan="4"> &nbsp; </td> <td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;"> SubTotal </td> ';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.GrandSubTotal.toFixed(2) + ' </td></tr>';
                                        itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;"> Taxes </td> ';
                                        //<td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;" rowspan="4" colspan="2"> &nbsp; </td> 
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.TaxTotal.toFixed(2) + '</td></tr>';
                                        itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">Shipping Cost </td>';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.ShippingCost.toFixed(2) + '</td></tr>';
                                        itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">Discount</td>';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.DiscountAmount.toFixed(2) + '</td></tr>';
                                        itemOrderDetails += '<tr><td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc;">Coupon </td>';
                                        itemOrderDetails += '<td style="border-bottom: 1px solid #dcdccc; padding: 5px;"> ' + item.CouponAmount.toFixed(2) + '</td></tr>';
                                        itemOrderDetails += '<tr><td style="padding: 5px; border-right: 1px solid #dcdccc;" colspan="2"> &nbsp; </td> <td style="padding: 5px; border-right: 1px solid #dcdccc; font: bold 14px Arial, Helvetica, sans-serif; color: #000;"> Total Cost </td>';
                                        itemOrderDetails += ' <td style="border-bottom: 1px solid #dcdccc; padding: 5px; border-right: 1px solid #dcdccc; color: #605f5f">' + item.GrandTotal.toFixed(2) + '</td></tr>';
                                    }
                                    itemOrderDetails += '</table>';
                                }

                                //                                if (span1.startsWith("<br/> <b>Shipping To: </b>")) {
                                //                                    span1 = "";
                                //                                }
                                billingshipping = span + '</td><td width="280" valign="top" style="border: 1px solid #dcdccc; border-right: none;border-bottom: none; float: right">' +
                                span1;
                                itemDetails = itemOrderDetails;
                            }
                        });

                        var FromEmail = senderEmail;
                        var receiverEmail = $("#hdnReceiverEmail").val();
                        var param = JSON2.stringify({ storeID: storeId, portalID: portalId, receiverEmail: receiverEmail, billingShipping: billingshipping, itemTable: itemDetails, additionalFields: additional, templateName: '<%=templateName%>' });

                        $.ajax({
                            type: "POST",
                            url: aspxservicePath + "AspxCommerceWebService.asmx/NotifyOrderStatusUpdate",
                            data: param,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function() {
                                csscody.info('<h2>Information Message</h2><p>The order status had been updated and confirmation email has been send to the customer successfully!</p>');
                            },
                            error: function() {
                                csscody.alert('<h2>Information Message</h2><p>The order status has been updated but failed to send email to the customer!</p>');
                            }
                        });
                    }
                    break;
                case 3:
                    var elements = '';
                    var tableElements = '';
                    var grandTotal = '';
                    var couponAmount = '';
                    var taxTotal = '';
                    var shippingCost = '';
                    var discountAmount = '';
                    var additionalNote = "";
                    $.each(data.d, function(index, value) {
                        Array.prototype.clean = function(deleteValue) {
                            for (var i = 0; i < this.length; i++) {
                                if (this[i] == deleteValue) {
                                    this.splice(i, 1);
                                    i--;
                                }
                            }
                            return this;
                        };
                        if (index < 1) {
                            var billAdd = '';
                            var arrBill;
                            arrBill = value.BillingAddress.split(',');
                            billAdd += '<li><b>Billing Address:</b></li>';
                            billAdd += '<li>' + arrBill[0] + ' ' + arrBill[1] + '</li>';
                            billAdd += '<li>' + arrBill[2] + '</li><li>' + arrBill[3] + '</li><li>' + arrBill[4] + '</li>';
                            billAdd += '<li>' + arrBill[5] + ' ' + arrBill[6] + ' ' + arrBill[7] + '</li>' + arrBill[8] + '<li>' + arrBill[9] + '</li><li>' + arrBill[10] + ' ' + arrBill[11] + '</li><li>' + arrBill[12] + '</li>';
                            billAdd += '<li>' + arrBill[13] + '</li>';
                            $("#divOrderDetailForm").find('ul').html(billAdd);
                            $("#OrderDate").html(value.OrderedDate);
                            $("#PaymentGatewayType").html(value.PaymentGatewayTypeName);
                            $("#PaymentMethod").html(value.PaymentMethodName);
                            additionalNote = value.Remarks;
                            $("#storeName").html(value.StoreName);
                            $("#storeDescription").html(value.StoreDescription);
                        }
                        tableElements += '<tr>';
                        if (value.CostVariants != "") {
                            tableElements += '<td>' + value.ItemName + '<br/>' + '(' + value.CostVariants + ')' + '</td>';
                        } else {
                            tableElements += '<td>' + value.ItemName + '<br/></td>';
                        }
                        var shippingAddress = new Array();
                        var shipAdd = '';
                        shippingAddress = value.ShippingAddress.replace(",", " ").split(",");

                        shippingAddress.clean(" ");
                        // tableElements += '<td>' + value.ItemName + '<br/>' + value.CostVariants + '</td>';
                        tableElements += '<td>' + value.ShippingMethod + '</td>';
                        tableElements += '<td>' + shippingAddress + '</td>';
                        tableElements += '<td><span class="cssClassFormatCurrency" >' + value.ShippingRate.toFixed(2) + '</span></td>';
                        tableElements += '<td><span class="cssClassFormatCurrency" >' + value.Price.toFixed(2) + '</span></td>';
                        tableElements += '<td>' + value.Quantity + '</td>';
                        tableElements += '<td><span class="cssClassFormatCurrency" >' + (value.Price * value.Quantity).toFixed(2) + '</span></td>';
                        tableElements += '</tr>';
                        if (index == 0) {
                            taxTotal = '<tr>';
                            taxTotal += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel"><b>Tax Total:</b></td>';
                            taxTotal += '<td><span class="cssClassFormatCurrency" >' + value.TaxTotal.toFixed(2) + '</span></td>';
                            taxTotal += '</tr>';
                            shippingCost = '<tr>';
                            shippingCost += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel"><b>Shipping Cost:</b></td>';
                            shippingCost += '<td><span class="cssClassFormatCurrency" >' + value.ShippingCost.toFixed(2) + '</span></td>';
                            shippingCost += '</tr>';
                            discountAmount = '<tr>';
                            discountAmount += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel"><b>Discount Amount:</b></td>';
                            discountAmount += '<td><span class="cssClassFormatCurrency" >' + value.DiscountAmount.toFixed(2) + '</span></td>';
                            discountAmount += '</tr>';
                            couponAmount = '<tr>';
                            couponAmount += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel"><b>Coupon Amount:</b></td>';
                            couponAmount += '<td><span class="cssClassFormatCurrency" >' + value.CouponAmount.toFixed(2) + '</span></span></td>';
                            couponAmount += '</tr>';
                            grandTotal = '<tr>';
                            grandTotal += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel"><b>Grand Total:</b></td>';
                            grandTotal += '<td><span class="cssClassFormatCurrency" >' + value.GrandTotal.toFixed(2) + '</span></td>';
                            grandTotal += '</tr>';
                        }
                    });
                    $("#divOrderDetailForm").find('table>tbody').html(tableElements);
                    $("#divOrderDetailForm").find('table>tbody').append(taxTotal);
                    $("#divOrderDetailForm").find('table>tbody').append(shippingCost);
                    $("#divOrderDetailForm").find('table>tbody').append(discountAmount);
                    $("#divOrderDetailForm").find('table>tbody').append(couponAmount);
                    $("#divOrderDetailForm").find('table>tbody').append(grandTotal);
                    $("#divOrderDetailForm").find("table>tbody tr:even").addClass("cssClassAlternativeEven");
                    $("#divOrderDetailForm").find("table>tbody tr:odd").addClass("cssClassAlternativeOdd");
                    if (additionalNote != '' && additionalNote != undefined) {
                        $(".remarks").html("").html("*Additional Note :- '" + additionalNote + "'");
                    } else {
                        $(".remarks").html("");
                    }
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    OrderManage.HideAll();
                    $("#divOrderDetailForm").show();
                    break;
                case 4:
                    $.each(data.d, function(index, item) {
                        var couponStatusElements = "<option value=" + item.OrderStatusID + ">" + item.OrderStatusName + "</option>";
                        $("#ddlOrderStatus").append(couponStatusElements);
                        $("#selectStatus").append(couponStatusElements);
                    });
                    break;
                case 5:
                    OrderManage.BindDataForExport(data);
                    break;
            }
        }
    };
    OrderManage.init();
});