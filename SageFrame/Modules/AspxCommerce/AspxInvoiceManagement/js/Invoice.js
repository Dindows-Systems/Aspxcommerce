var invoiceMgmt;
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
    var customerEmail = '';

    invoiceMgmt = {
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
                type: invoiceMgmt.config.type,
                contentType: invoiceMgmt.config.contentType,
                cache: invoiceMgmt.config.cache,
                async: invoiceMgmt.config.async,
                url: invoiceMgmt.config.url,
                data: invoiceMgmt.config.data,
                dataType: invoiceMgmt.config.dataType,
                success: invoiceMgmt.ajaxSuccess,
                error: invoiceMgmt.ajaxFailure
            });
        },
        HideAll: function() {
            $('#divOrderDetails').hide();
            $('#divInvoiceForm').hide();
        },
        LoadInvoiceAjaxStaticImage: function() {
            $('#ajaxInvoiceMgmtImageLoad').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        ClearInvoiceForm: function() {
            $('#spanInvoiceNo').html('');
            $('#spanInvoiceDate').html('');
            $("#spanCustomerName").html('');
            $("#spanCustomerEmail").html('');
            $("#spanOrderID").html('');
            $("#spanOrderDate").html('');
            $("#spanOrderStatus").html('');
            $("#spanPaymentMethod").html('');
            $("#spanShippingMethod").html('');
            $("#divBillingAddressInfo").html('');
            $("#divShippingAddressInfo").html('');
            $('#divOrderItemDetails>table').empty();
        },
        GetInvoiceDataForExport: function() {
            this.config.url = this.config.baseURL + "GetInvoiceDetailsList";
            this.config.data = JSON2.stringify({ offset: 1, limit: null, invoiceNumber: null, billToNama: null, status: null, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },
        ExportInvoiceToCsvData: function() {
            invoiceMgmt.GetInvoiceDataForExport();
        },
        ExportDivDataToExcel: function() {
            invoiceMgmt.GetInvoiceDataForExport();
        },
        BindInvoiceDataForExport: function(msg) {
            var exportData = '<thead><tr><th>Invoice No.</th><th>Invoice Date</th><th>Order ID</th><th>Customer Name</th><th>Order Date</th><th>Bill to Name</th><th>Ship to Name</th><th>Status</th><th>Amount</th><th>Customer Email</th></tr><tbody>';
            if (msg.d.length > 0) {
                $.each(msg.d, function(index, value) {
                    exportData += '<tr><td>' + value.InvoiceNumber + '</td><td>' + value.InvoiceDate + '</td>';
                    exportData += '<td>' + value.OrderID + '</td><td>' + value.CustomerName + '</td>';
                    exportData += '<td>' + value.OrderDate + '</td><td>' + value.BillToName + '</td>';
                    exportData += '<td>' + value.ShipToName + '</td><td>' + value.OrderStatusName + '</td>';
                    exportData += '<td>' + value.Amount + '</td><td>' + value.CustomerEmail + '</td></tr>';
                });
            } else {
                exportData += '<tr><td>No Records Found!</td></tr>';
            }
            exportData += '</tbody>';
            $("#invoiceExportDataTbl").html(exportData);
            $("input[id$='HdnValue']").val('<table>' + exportData + '</table>');
            $("input[id$='_csvInvoiceHiddenValue']").val($('#invoiceExportDataTbl').table2CSV());
            $("#invoiceExportDataTbl").html('');
        },
        BindInvoiceInformation: function(invoiceNum, billToNm, statusType) {
            this.config.url = this.config.baseURL;
            this.config.method = "GetInvoiceDetailsList";
            this.config.data = { invoiceNumber: invoiceNum, billToNama: billToNm, status: statusType, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName };
            var invoiceData = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvInvoiceDetails_pagesize").length > 0) ? $("#gdvInvoiceDetails_pagesize :selected").text() : 10;

            $("#gdvInvoiceDetails").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'Invoice Number', name: 'invoice_number', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', url: '', queryPairs: '' },
                    { display: 'Invoice Date', name: 'invoice_date', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Order ID', name: 'order_id', cssclass: 'cssClassLinkHeader', controlclass: 'cssClassGridLink', coltype: 'link', align: 'left', url: '', queryPairs: '' },
                    { display: 'Customer Name', name: 'CustomerName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Order Date', name: 'order_date', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Bill to Name', name: 'bill_to_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Ship to Name', name: 'ship_to_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Status', name: 'status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Amount', name: 'amount', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: 'Customer Email', name: 'CustomerEmail', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                //{ display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'EditAttributes', arguments: '1,5' },
                //{display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'DeleteAttributes', arguments: '' },
                    {display: 'View', name: 'view Invoice', enable: true, _event: 'click', trigger: '3', callMethod: 'invoiceMgmt.ViewAttributes', arguments: '1,2,3,4,5,6,7,8,9' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: invoiceData, //{ invoiceNumber: invoiceNum, billToNama: billToNm, status: statusType, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 10: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },
        ViewAttributes: function(tblID, argus) {
            switch (tblID) {
                case "gdvInvoiceDetails":
                    //  alert(argus);
                    customerEmail = argus[11];
                    $("#" + lblInvoiceForm).html("Invoice Number: #" + argus[0]);
                    $('#divOrderDetails').hide();
                    $('#divInvoiceForm').show();
                    $('#spanInvoiceNo').html(argus[0]);
                    $('#spanInvoiceDate').html(argus[3]);
                    $("#spanCustomerName").html(argus[5]);
                    $("#spanCustomerEmail").html(argus[11]);
                    invoiceMgmt.getItemInvoiceDetail(argus[4]);

                    break;
                default:
                    break;
            }
        },
        GetInvoiceStatus: function() {
            this.config.url = this.config.baseURL + "GetStatusList";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, cultureName: cultureName });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        getItemInvoiceDetail: function(orderId) {
            this.config.url = this.config.baseURL + "GetInvoiceDetailsByOrderID";
            this.config.data = JSON2.stringify({ orderID: orderId, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        BindInvoiceDetail: function(msg) {
            var span = '';
            var span1 = '';
            var orderID = 0;
            var itemOrderDetails = '';
            var additionalNote = "";
            if (msg.d.length > 0) {
                $.each(msg.d, function(index, item) {
                    if (index == 0) {
                        $("input[id$='hdnIsMultipleShipping']").val(item.IsMultipleShipping);
                        $("#spanOrderID").html(item.OrderID);
                        $("#spanOrderDate").html(item.OrderDate);
                        $("#spanOrderStatus").html(item.OrderStatusName);
                        $("#spanPaymentMethod").html(item.PaymentMethodName);
                        additionalNote = item.Remarks;
                        if (item.IsMultipleShipping != true) {
                            if (item.ShippingMethodName != '') {
                                $("#spanShippingMethod").html(item.ShippingMethodName);
                            } else {
                                $("#spanShippingMethod").html("Downloadable Items don't need Shipping Method");
                            }
                        } else {
                            $('#spanShippingMethod').html('Multiple Shipping Exist');
                        }

                        $("#spanStoreName").html(item.StoreName);
                        $("#spanStoreDescription").html(item.StoreDescription);

                        span = "<br/><b>Billing Address: </b>";
                        if (item.BillingName != "")
                            span += "<br/>" + $.trim(item.BillingName);

                        if (item.Company != "")
                            span += "<br/>" + $.trim(item.Company);

                        if (item.Address1 != "")
                            span += "<br/>" + $.trim(item.Address1);

                        if (item.Address2 != "")
                            span += "<br/>" + $.trim(item.Address2);

                        if (item.City != "")
                            span += "<br/>" + $.trim(item.City);

                        if (item.State != "")
                            span += " " + $.trim(item.State);

                        if (item.Zip != "")
                            span += " " + $.trim(item.Zip);

                        if (item.Country != "")
                            span += "<br/>" + $.trim(item.Country);

                        if (item.Email != "")
                            span += "<br/>" + $.trim(item.Email);

                        if (item.Phone != "")
                            span += "<br/>" + $.trim(item.Phone);

                        if (item.Mobile != "")
                            span += ", " + $.trim(item.Mobile);

                        if (item.Fax != "")
                            span += "<br/>" + $.trim(item.Fax);

                        if (item.Website != "")
                            span += "<br/>" + $.trim(item.Website);

                        if (item.IsMultipleShipping != true) {
                            if (item.ShippingName != '') {
                                span1 = "<br/><b>Shipping Address: </b>";
                            } else {
                                span1 = "<br/><b>Shipping Address do not needed for Downloadable Items</b>";
                            }
                            if (item.ShippingName != "")
                                span1 += "<br/>" + $.trim(item.ShippingName);

                            if (item.ShipCompany != "")
                                span1 += "<br/>" + $.trim(item.ShipCompany);

                            if (item.ShipAddress1 != "")
                                span1 += "<br/>" + $.trim(item.ShipAddress1);

                            if (item.ShipAddress2 != "")
                                span1 += "<br/>" + $.trim(item.ShipAddress2);

                            if (item.ShipCity != "")
                                span1 += "<br/>" + $.trim(item.ShipCity);

                            if (item.ShipState != "")
                                span1 += " " + $.trim(item.ShipState);

                            if (item.ShipZip != "")
                                span1 += " " + $.trim(item.ShipZip);

                            if (item.ShipCountry != "")
                                span1 += "<br/>" + $.trim(item.ShipCountry);

                            if (item.ShipEmail != "")
                                span1 += "<br/>" + $.trim(item.ShipEmail);

                            if (item.ShipPhone != "")
                                span1 += "<br/>" + $.trim(item.ShipPhone);

                            if (item.ShipMobile != "")
                                span1 += ", " + $.trim(item.ShipMobile);

                            if (item.ShipFax != "")
                                span1 += "<br/>" + $.trim(item.ShipFax);

                            if (item.ShipWebsite != "")
                                span1 += "<br/>" + $.trim(item.ShipWebsite);

                            itemOrderDetails = '<table width="100%" border="0" cellspacing="0" cellpadding="0" class="OrderDetailsTable"><thead><tr align="left" class="cssClassLabel"><th class="cssClassItemName"><b>Item Name</b></th><th class="cssClassPrice"><b>Price</b></th><th class="cssClassQuantity"><b>Quantity</b></th><th class="cssClassSubTotal"><b>Sub Total</b></th></td></tr></thead><tbody>';
                            $.each(msg.d, function(index, item) {
                                if (item.CostVariants == "") {
                                    itemOrderDetails += "<tr><td class='cssClassItemName'>" + item.ItemName + "</td>";
                                    itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassPrice' >" + item.Price.toFixed(2) + "</span></td>";
                                    itemOrderDetails += "<td class='cssClassQuantity'>" + item.Quantity + "</td>";
                                    itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.SubTotal.toFixed(2) + "</span></td></tr>";
                                } else {
                                    itemOrderDetails += "<tr><td class='cssClassItemName'>" + item.ItemName + " (" + item.CostVariants + ")" + "</td>";
                                    itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassPrice' >" + item.Price.toFixed(2) + "</td>";
                                    itemOrderDetails += "<td class='cssClassQuantity'>" + item.Quantity + "</td>";
                                    itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.SubTotal.toFixed(2) + "</span></td></tr>";
                                }
                            });
                            if (index == 0) {
                                itemOrderDetails += "<tr><td class='cssClassItemName'></td><td class='cssClassPrice'></td><td class='cssClassLabel cssClassQuantity'><b>Sub Total:</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.GrandSubTotal.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemName'></td><td class='cssClassPrice'></td><td class='cssClassLabel cssClassQuantity'><b>Taxes:</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.TaxTotal.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemName'></td><td class='cssClassPrice'></td><td class='cssClassLabel cssClassQuantity'><b>Shipping Cost:</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.ShippingCost.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemName'></td><td class='cssClassPrice'></td><td class='cssClassLabel cssClassQuantity'><b>Discount Amount:</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.DiscountAmount.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemName'></td><td class='cssClassPrice'></td><td class='cssClassLabel cssClassQuantity'><b>Coupon Amount:</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.CouponAmount.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemName'></td><td class='cssClassPrice'></td><td class='cssClassLabel cssClassQuantity'><b>Total Cost:</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.GrandTotal.toFixed(2) + "</span></td></tr>";
                            }
                            itemOrderDetails += '</tbody></table>';
                        } else {
                            itemOrderDetails = '<table class="classTableWrapper" width="100%" border="0" cellspacing="0" cellpadding="0"><thead><tr align="left" class="cssClassLabel"><th class="cssClassItemName"><b>ItemName</b></th><th class="cssClassShippingMethod"><b>Shipping Method</b></th><th class="cssClassShippingAddress"><b>Shipping To</b></th><th class="cssClassPrice"><b>Price</b></th><th class="cssClassQuantity"><b>Quantity</b></th><th class="cssClassSubTotal"><b>SubTotal</b></th></td></tr></thead><tbody>';
                            $.each(msg.d, function(index, item) {

                                var shippingDetails = "";
                                if (item.ShippingName != "")
                                    shippingDetails += $.trim(item.ShippingName);

                                if (item.ShipAddress1 != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipAddress1);

                                if (item.ShipAddress2 != "")
                                    shippingDetails += "," + $.trim(item.ShipAddress2);

                                if (item.ShipCompany != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipCompany);

                                if (item.ShipCity != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipCity);

                                if (item.ShipState != "")
                                    shippingDetails += " " + $.trim(item.ShipState);

                                if (item.ShipZip != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipZip);

                                if (item.ShipCountry != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipCountry);

                                if (item.ShipEmail != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipEmail);

                                if (item.ShipPhone != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipPhone);

                                if (item.ShipMobile != "")
                                    shippingDetails += ", " + $.trim(item.ShipMobile);

                                if (item.ShipFax != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipFax);

                                if (item.ShipWebsite != "")
                                    shippingDetails += "<br/>" + $.trim(item.ShipWebsite);


                                if (item.CostVariants == "") {
                                    itemOrderDetails += "<tr><td class='cssClassItemName'>" + item.ItemName + "</td>";
                                    itemOrderDetails += "<td class='cssClassShippingMethod'>" + item.ShippingMethodName + "</td>";
                                    itemOrderDetails += "<td class='cssClassShippingAddress'>" + shippingDetails + "</td>";
                                    itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassPrice' >" + item.Price.toFixed(2) + "</span></td>";
                                    itemOrderDetails += "<td class='cssClassQuantity'>" + item.Quantity + "</td>";

                                } else {

                                    itemOrderDetails += "<tr><td class='cssClassItemName'>" + item.ItemName + " (" + item.CostVariants + ")" + "</td>";
                                    itemOrderDetails += "<td class='cssClassShippingMethod'>" + item.ShippingMethodName + "</td>";
                                    itemOrderDetails += "<td class='cssClassShippingAddress'>" + shippingDetails + "</td>";
                                    itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassPrice' >" + item.Price.toFixed(2) + "</span></td>";
                                    itemOrderDetails += "<td class='cssClassQuantity'>" + item.Quantity + "</td>";

                                }

                                itemOrderDetails += "<td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.SubTotal.toFixed(2) + "</span></td></tr>";
                            });

                            if (index == 0) {
                                itemOrderDetails += "<tr><td class='cssClassItemName'></td><td class='cssClassShippingMethod'></td><td class='cssClassShippingAddress'></td><td class='cssClassPrice'></td><td class='cssClassLabel cssClassQuantity'><b>Sub Total:</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.GrandSubTotal.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemName'></td><td class='cssClassShippingMethod'></td><td class='cssClassShippingAddress'></td><td class='cssClassPrice'></td><td class='cssClassLabel cssClassQuantity'><b>Taxes:</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.TaxTotal.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemName'></td><td class='cssClassShippingMethod'></td><td class='cssClassShippingAddress'></td><td class='cssClassPrice'></td><td class='cssClassLabel cssClassQuantity'><b>Shipping Cost:</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.ShippingCost.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemName'></td><td class='cssClassShippingMethod'></td><td class='cssClassShippingAddress'></td><td class='cssClassPrice'></td><td class='cssClassLabel cssClassQuantity'><b>Discount Amount:</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.DiscountAmount.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemName'></td><td class='cssClassShippingMethod'></td><td class='cssClassShippingAddress'></td><td class='cssClassPrice'></td><td class='cssClassLabel cssClassQuantity'><b>Coupon Amount:</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.CouponAmount.toFixed(2) + "</span></td></tr>";
                                itemOrderDetails += "<tr><td class='cssClassItemName'></td><td class='cssClassShippingMethod'></td><td class='cssClassShippingAddress'></td><td class='cssClassPrice'></td><td class='cssClassLabel cssClassQuantity'><b>Total Cost:</b></td>";
                                itemOrderDetails += " <td class='cssClassAlignRight'><span class='cssClassFormatCurrency cssClassSubTotal' >" + item.GrandTotal.toFixed(2) + "</span></td></tr>";
                            }
                            itemOrderDetails += '</tbody></table>';
                        }
                        $("#divOrderItemDetails").html(itemOrderDetails);
                        $("#divOrderItemDetails").append("<span class='remarks'></span>");
                        $(".OrderDetailsTable>tbody tr:even").addClass("cssClassAlternativeEven");
                        $(".OrderDetailsTable>tbody tr:odd").addClass("cssClassAlternativeOdd");
                        if (additionalNote != '' && additionalNote != undefined) {
                            $(".remarks").html("").html("*Additional Note :- '" + additionalNote + "'");
                        } else {
                            $(".remarks").html("");
                        }
                    }

                    $("#divOrderDetailForm").show();
                });
                $("#divBillingAddressInfo").html(span);
                $("#divShippingAddressInfo").html(span1);
                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
            } else {
                csscody.alert("<h1>Information</h1><p>No Invoice is Available for this Order</p>");
                $('#divOrderDetails').show();
                $('#divInvoiceForm').hide();
            }
        },
        GenerateInvoicePDF: function() {
            var invoceDetailInfo = {
                headerInfo: {
                    InvoiceNo: $('#spanInvoiceNo').html(),
                    InvoiceDate: $('#spanInvoiceDate').html(),
                    StoreName: $('#spanStoreName').html(),
                    StoreDescription: $('#spanStoreDescription').html(),
                    CustomerName: $('#spanCustomerName').html(),
                    CustomerEmail: $('#spanCustomerEmail').html(),
                    OrderId: $('#spanOrderID').html(),
                    OrderDate: $('#spanOrderDate').html(),
                    Status: $('#spanOrderStatus').html(),
                    PaymentMethod: $('#spanPaymentMethod').html(),
                    ShippingMethod: $('#spanShippingMethod').html(),
                    BillingAddress: $('#divBillingAddressInfo').html(),
                    ShippingAddress: $('#divShippingAddressInfo').html()
                },
                tableDataInfo: {
                    ItemName: '',
                    ShippingMethodName: '',
                    ShippingAddressDetail: '',
                    Price: '',
                    Quantity: '',
                    SubTotal: ''
                }
            };

            var headerString = JSON.stringify(invoceDetailInfo.headerInfo);
            var tdArrayColl = new Array();
            $('#divOrderItemDetails tr ').each(function() {

                invoceDetailInfo.tableDataInfo.ItemName = $(this).find('.cssClassItemName b').length > 0 ? $(this).find('.cssClassItemName b').html() : $(this).find('.cssClassItemName ').html();
                invoceDetailInfo.tableDataInfo.ShippingMethodName = $(this).find('.cssClassShippingMethod b').length > 0 ? $(this).find('.cssClassShippingMethod b').html() : $(this).find('.cssClassShippingMethod ').html();
                invoceDetailInfo.tableDataInfo.ShippingAddressDetail = $(this).find('.cssClassShippingAddress b').length > 0 ? $(this).find('.cssClassShippingAddress b').html() : $(this).find('.cssClassShippingAddress ').html();
                invoceDetailInfo.tableDataInfo.Price = $(this).find('.cssClassPrice b').length > 0 ? $(this).find('.cssClassPrice b').html() : $(this).find('.cssClassPrice ').html();
                invoceDetailInfo.tableDataInfo.Quantity = $(this).find('.cssClassQuantity b').length > 0 ? $(this).find('.cssClassQuantity b').html() : $(this).find('.cssClassQuantity ').html();
                invoceDetailInfo.tableDataInfo.SubTotal = $(this).find('.cssClassSubTotal b').length > 0 ? $(this).find('.cssClassSubTotal b').html() : $(this).find('.cssClassSubTotal ').html();
                tdArrayColl.push(JSON.stringify(invoceDetailInfo.tableDataInfo));
            });

            $("input[id$='HdnValue']").val(tdArrayColl);
            $("input[id$='invoiceHeaderDetails']").val(headerString);
            $("input[id$='hdnRemarks']").val($('.remarks').html());
        },
        printPage: function() {
            window.print();
            //        var content = $('#divPrintInvoiceForm').html();
            //        var pwin = window.open('', 'print_content', 'width=100,height=100');
            //        pwin.document.open();
            //        pwin.document.write('<html><head><title></title></head><body onload="window.print()">' + content + '</body></html>');      
            //        pwin.document.close();
            //        setTimeout(function() { pwin.close(); }, 1000);
        },
        SearchInvoices: function() {
            var invoiceNum = $.trim($("#txtInvoiceNumber").val());
            //var orderID = trim($("#txtOrderID").val());
            var billToNm = $.trim($("#txtbillToName").val());
            var statusType = '';
            if (invoiceNum.length < 1) {
                invoiceNum = null;
            }
            if ($("#ddlStatus").val() != "0") {
                statusType = $.trim($("#ddlStatus").val());
            } else {
                statusType = null;
            }
            if (billToNm.length < 1) {
                billToNm = null;
            }
            invoiceMgmt.BindInvoiceInformation(invoiceNum, billToNm, statusType);
        },
        ajaxSuccess: function(msg) {
            switch (invoiceMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $.each(msg.d, function(index, item) {
                        var couponStatusElements = "<option value=" + item.OrderStatusID + ">" + item.OrderStatusName + "</option>";
                        $("#ddlStatus").append(couponStatusElements);
                    });
                    break;
                case 2:
                    invoiceMgmt.BindInvoiceDetail(msg);
                    break;
                case 3:
                    invoiceMgmt.BindInvoiceDataForExport(msg);
                    break;
            }
        },
        ajaxFailure: function(data) {
            switch (invoiceMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>Error Message</h1><p>Failed to load Order Status list  !!</p>');
                    break;
                case 2:
                    csscody.error('<h1>Error Message</h1><p>Failed to load Invoice Details !!</p>');
                    break;
            }
        },
        init: function() {
            invoiceMgmt.LoadInvoiceAjaxStaticImage();
            invoiceMgmt.BindInvoiceInformation(null, null, null);
            invoiceMgmt.GetInvoiceStatus();
            invoiceMgmt.HideAll();
            //   $("#btnExportToCSV").click(function() {
            //       $('#gdvInvoiceDetails').table2CSV();
            //   });
            $('#divOrderDetails').show();

            $("#btnBack").click(function() {
                invoiceMgmt.HideAll();
                $('#divOrderDetails').show();
                invoiceMgmt.ClearInvoiceForm();
            });
            $('#btnPrint').click(function() {
                invoiceMgmt.printPage();
            });
            $('#txtInvoiceNumber,#txtbillToName,#ddlStatus').keyup(function(event) {
                if (event.keyCode == 13) {
                    invoiceMgmt.SearchInvoices();
                }
            });
            var path = window.location.href;
            var pathArr = path.split('/');
            var pageName = pathArr[pathArr.length - 1];
            pageName = pageName.split('.');
            if (new RegExp('\\b' + "Reports" + '\\b').test(pageName)) {
                $('#' + lblInvoiceHeader).html('Invoiced Reports');
            } else {
                $('#' + lblInvoiceHeader).html('Invoices');
            }
        }
    };
    invoiceMgmt.init();
});