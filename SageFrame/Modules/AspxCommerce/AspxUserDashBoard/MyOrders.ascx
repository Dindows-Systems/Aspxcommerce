<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyOrders.ascx.cs" Inherits="Modules_AspxUserDashBoard_MyOrders" %>

<script type="text/javascript">
    //<![CDATA[
    var MyOrders = {
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
        ajaxCall: function(config) {
            $.ajax({
                type: MyOrders.config.type,
                contentType: MyOrders.config.contentType,
                cache: MyOrders.config.cache,
                async: MyOrders.config.async,
                data: MyOrders.config.data,
                dataType: MyOrders.config.dataType,
                url: MyOrders.config.url,
                success: MyOrders.ajaxSuccess,
                error: MyOrders.ajaxFailure
            });
        },
        init: function() {
            MyOrders.LoadUserMyOrderStaticImage();
            MyOrders.GetMyOrders();
            MyOrders.OrderHideAll();
            $("#divTrackMyOrder").show();
            $("#divMyOrders").show();
            $("#lnkBack").bind("click", function() {
                MyOrders.OrderHideAll();
                $("#divTrackMyOrder").show();
                $("#divMyOrders").show();
            });

            $("#txtOrderID").keypress(function(e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    if (e.which != 13) {
                        $("#errmsgOrderID").html("Digits Only").css("color", "red").show().fadeOut(1600);
                        return false;
                    }
                }
            });

            $("#txtOrderID").keyup(function(event) {
                if (event.keyCode == 13) {
                    $("#btnGetOrderDetails").click();
                }
            });
            $("#btnGetOrderDetails").click(function() {
                var orderID = $.trim($("#txtOrderID").val());
                if (orderID == "") {
                    csscody.alert("<h2>Information Alert</h2><p>Please enter order ID.</p>");
                } else {
                    MyOrders.GetAllOrderDetails(orderID);
                }
            });
        },

        LoadUserMyOrderStaticImage: function() {
            $('#ajaxUserDashMyOrder').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        OrderHideAll: function() {
            $("#divMyOrders").hide();
            $("#divOrderDetails").hide();
            $("#divTrackMyOrder").hide();
        },

        GetMyOrders: function() {
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvMyOrder_pagesize").length > 0) ? $("#gdvMyOrder_pagesize :selected").text() : 10;

            $("#gdvMyOrder").sagegrid({
                url: this.config.baseURL,
                functionMethod: 'GetMyOrderList',
                colModel: [
                    { display: 'Order ID', name: 'order_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Invoice Number', name: 'invoice_number', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Customer ID', name: 'customerID', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Customer Name', name: 'customer_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Email', name: 'email', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Order Status', name: 'order_status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Grand Total', name: 'grand_total', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Payment Gateway Type Name', name: 'payment_gateway_typename', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Payment Method Name', name: 'payment_method_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Ordered Date', name: 'ordered_date', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                    { display: 'View', enable: true, _event: 'click', trigger: '1', callMethod: 'MyOrders.GetOrderDetails', arguments: '' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { storeID: storeId, portalID: portalId, customerID: customerId, cultureName: cultureName },
                current: current_,
                pnew: offset_,
                sortcol: { 10: { sorter: false} }
            });
        },

        GetOrderDetails: function(tblID, argus) {
            switch (tblID) {
                case "gdvMyOrder":
                    MyOrders.GetAllOrderDetails(argus[0]);
                    break;
            }
        },

        GetAllOrderDetails: function(argus) {
            var orderId = argus;
            this.config.url = this.config.baseURL + "GetMyOrders";
            this.config.data = JSON2.stringify({ orderID: orderId, storeID: storeId, portalID: portalId, customerID: customerId, userName: userName, cultureName: cultureName });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        GetCheckOutPage: function(tdlID, argus) {
            switch (tdlID) {
                case "gdvMyOrder":
                    //TODO:: Reorder SP [dbo].[usp_Aspx_GetReOrderItems] call and redirect too checkoutpage.aspx;
                    break;
            }
        },
        ajaxSuccess: function(msg) {
            switch (MyOrders.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    if (msg.d.length > 0) {
                        var elements = '';
                        var tableElements = '';
                        var grandTotal = '';
                        var couponAmount = '';
                        var taxTotal = '';
                        var shippingCost = '';
                        var discountAmount = '';
                        $.each(msg.d, function(index, value) {
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
                                billAdd += '<li>' + arrBill[0] + ' ' + arrBill[1] + '</li>';
                                billAdd += '<li>' + arrBill[2] + '</li><li>' + arrBill[3] + '</li><li>' + arrBill[4] + '</li>';
                                billAdd += '<li>' + arrBill[5] + ' ' + arrBill[6] + ' ' + arrBill[7] + '</li>' + arrBill[8] + '<li>' + arrBill[9] + '</li><li>' + arrBill[10] + ' ' + arrBill[11] + '</li><li>' + arrBill[12] + '</li>';
                                billAdd += '<li>' + arrBill[13] + '</li>';
                                $("#divOrderDetails").find('ul').html(billAdd);
                                $("#orderedDate").html(value.OrderedDate);
                                $("#paymentGatewayType").html(value.PaymentGatewayTypeName);
                                $("#paymentMethod").html(value.PaymentMethodName);
                            }

                            var shippingAddress = new Array();
                            var shipAdd = '';
                            shippingAddress = value.ShippingAddress.replace(",", " ").split(",");
                            shippingAddress.clean(" ");

                            tableElements += '<tr>';
                            tableElements += '<td>' + value.ItemName + '<br/>' + value.CostVariants + '</td>';
                            tableElements += '<td>' + value.SKU + '</td>';
                            tableElements += '<td>' + shippingAddress + '</td>';
                            tableElements += '<td><span class="cssClassFormatCurrency">' + value.ShippingRate.toFixed(2) + '</span></td>';
                            tableElements += '<td><span class="cssClassFormatCurrency">' + value.Price.toFixed(2) + '</span></td>';
                            tableElements += '<td>' + value.Quantity + '</td>';
                            tableElements += '<td><span class="cssClassFormatCurrency">' + (value.Price * value.Quantity).toFixed(2) + '</span></td>';
                            tableElements += '</tr>';
                            if (index == 0) {
                                taxTotal = '<tr>';
                                taxTotal += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">Tax Total:</td>';
                                taxTotal += '<td><span class="cssClassFormatCurrency">' + value.TaxTotal.toFixed(2) + '</span></td>';
                                taxTotal += '</tr>';
                                shippingCost = '<tr>';
                                shippingCost += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">Shipping Cost:</td>';
                                shippingCost += '<td><span class="cssClassFormatCurrency">' + value.ShippingRate.toFixed(2) + '</span></td>';
                                shippingCost += '</tr>';
                                discountAmount = '<tr>';
                                discountAmount += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">Discount Amount:</td>';
                                discountAmount += '<td><span class="cssClassFormatCurrency">' + value.DiscountAmount.toFixed(2) + '</span></td>';
                                discountAmount += '</tr>';
                                couponAmount = '<tr>';
                                couponAmount += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">Coupon Amount:</td>';
                                couponAmount += '<td><span class="cssClassFormatCurrency">' + value.CouponAmount.toFixed(2) + '</span></td>';
                                couponAmount += '</tr>';
                                grandTotal = '<tr>';
                                grandTotal += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">Grand Total:</td>';
                                grandTotal += '<td><span class="cssClassFormatCurrency">' + value.GrandTotal.toFixed(2) + '</span></td>';
                                grandTotal += '</tr>';
                            }
                        });

                        $("#divOrderDetails").find('table>tbody').html(tableElements);
                        $("#divOrderDetails").find('table>tbody').append(taxTotal);
                        $("#divOrderDetails").find('table>tbody').append(shippingCost);
                        $("#divOrderDetails").find('table>tbody').append(discountAmount);
                        $("#divOrderDetails").find('table>tbody').append(couponAmount);
                        $("#divOrderDetails").find('table>tbody').append(grandTotal);
                        MyOrders.OrderHideAll();
                        $("#divOrderDetails").show();
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        $("#txtOrderID").val('');
                    } else {

                        csscody.alert("<h2>Information Alert</h2><p>Order ID does not exist!</p>");
                        $("#txtOrderID").val('');
                        return false;
                    }
                    break;
            }
        }
    };
    $(function(){
        MyOrders.init();
    });
    //]]>
</script>

<div id="divTrackMyOrder" class="cssClassSearchPanel cssClassFormWrapper">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="style2">
                <label class="cssClassLabel">
                    Order ID :
                </label>
                <input type="text" id="txtOrderID" class="cssClassTextBoxSmall" /><span id="errmsgOrderID"></span>
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnGetOrderDetails">
                            <span><span>GO</span></span></button>
                    </p>
                </div>
            </td>
        </tr>
    </table>
</div>
<div id="divMyOrders">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="My Orders"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="loading">
                    <img id="ajaxUserDashMyOrder" src=""  alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvMyOrder" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divOrderDetails" class="cssClassFormWrapper">
    <span class="cssClassLabel">Ordered Date: </span><span id="orderedDate"></span>
    <ul>
    </ul>
    <span class="cssClassLabel">PaymentGateway Type: </span><span id="paymentGatewayType"></span>
    <br />
    <span class="cssClassLabel">Payment Method: </span><span id="paymentMethod"></span>
    <br />
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                Ordered Items:</h2>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <thead>
                        <tr class="cssClassHeading">
                            <td class="header">
                                Item Name
                            </td>
                            <td class="header">
                                SKU
                            </td>
                            <td class="header">
                                Shipping Address
                            </td>
                            <td class="header">
                                Shipping Rate
                            </td>
                            <td class="header">
                                Price
                            </td>
                            <td class="header">
                                Quantity
                            </td>
                            <td class="header">
                                Sub Total
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="cssClassButtonWrapper">
            <button type="button" id="lnkBack" class="cssClassButtonSubmit">
                <span><span>Go back</span></span></button>
        </div>
    <%--<a href="#" id="lnkBack" class="cssClassBack">Go back</a>--%>
</div>
