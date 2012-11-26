var Shipments="";
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
    Shipments = {
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
            Shipments.HideDivs();
            Shipments.LoadShipmentMgmtStaticImage();
            $("#divShipmentsDetails").show();
            Shipments.BindShipmentsDetails(null, null, null);
            $("#btnShipmentBack").click(function() {
                Shipments.HideDivs();
                $("#divShipmentsDetails").show();
                $("#divShipmentsDetailForm table >tbody").html('');
            });
            $("#btnShipmentsSearch").live("click", function() {
                Shipments.SearchShipments();
            });

            $('#txtShippingMethodName,#txtOrderID,#txtSearchShipToName').keyup(function(event) {
                if (event.keyCode == 13) {
                    $("#btnShipmentsSearch").click();
                }
            });
        },
        ajaxCall: function(config) {
            $.ajax({
                type: Shipments.config.type,
                contentType: Shipments.config.contentType,
                cache: Shipments.config.cache,
                async: Shipments.config.async,
                data: Shipments.config.data,
                dataType: Shipments.config.dataType,
                url: Shipments.config.url,
                success: Shipments.ajaxSuccess,
                error: Shipments.ajaxFailure
            });
        },
        LoadShipmentMgmtStaticImage: function() {
            $('#ajaxShipmentsMgmtImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        HideDivs: function() {
            $("#divShipmentsDetails").hide();
            $("#divShipmentsDetailForm").hide();
        },
        GetShipmentDataForExport: function() {
            this.config.url = this.config.baseURL + "GetShipmentsDetails";
            this.config.data = JSON2.stringify({ offset: 1, limit: null, shippimgMethodName: null, shipToName: null, orderId: null, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        ExportShipmentDataToCsv: function() {
            Shipments.GetShipmentDataForExport();
        },
        ExportShipmentDataToExcel: function() {
            Shipments.GetShipmentDataForExport();
        },

        BindShipmentsDetails: function(shippingMethodNm, orderID, shipNm) {
            this.config.method = "GetShipmentsDetails";
            this.config.data = { shippimgMethodName: shippingMethodNm, shipToName: shipNm, orderId: orderID, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvShipmentsDetails_pagesize").length > 0) ? $("#gdvShipmentsDetails_pagesize :selected").text() : 10;

            $("#gdvShipmentsDetails").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'Shipping Method ID', name: 'shipping_methodId', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Shipping Method Name', name: 'shipping_method_name', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Order ID', name: 'order_id', cssclass: 'cssClassLinkHeader', controlclass: 'cssClassGridLink', coltype: 'link', align: 'left', url: '', queryPairs: '' },
                    { display: 'Ship to Name', name: 'ship_to_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Shipping Rate', name: 'shipping_rate', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: 'View', name: 'view', enable: true, _event: 'click', trigger: '3', callMethod: 'Shipments.ViewShipments', arguments: '1,2,3,4,5,6' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { o: { sorter: true }, 5: { sorter: false} }
            });
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },

        ViewShipments: function(tblID, argus) {
            switch (tblID) {
                case "gdvShipmentsDetails":
                    Shipments.HideDivs();
                    $("#<%=lblShipmentForm.ClientID %>").html("Shipping Method ID: #" + argus[0]);
                    $("#divShipmentsDetailForm").show();
                    Shipments.BindShippindMethodDetails(argus[4]);
                    break;
            }
        },

        BindShippindMethodDetails: function(orderId) {
            this.config.url = this.config.baseURL + "BindAllShipmentsDetails";
            this.config.data = JSON2.stringify({ orderID: orderId, storeID: storeId, portalID: portalId, cultureName: cultureName });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        SearchShipments: function(data) {
            var shippingMethodNm = $.trim($("#txtShippingMethodName").val());
            var orderID = $.trim($("#txtOrderID").val());
            var shipNm = $.trim($("#txtSearchShipToName").val());
            if (shippingMethodNm.length < 1) {
                shippingMethodNm = null;
            }
            if (orderID.length < 1) {
                orderID = null;
            }
            if (shipNm.length < 1) {
                shipNm = null;
            }
            Shipments.BindShipmentsDetails(shippingMethodNm, orderID, shipNm);
        },

        BindShipmentDataForExport: function(data) {
            var exportData = '<thead><tr><th>Shipping Method Name</th><th>Order ID</th><th>Ship to Name</th><th>Shipping Rate</th></tr></thead><tbody>';
            if (data.d.length > 0) {
                $.each(data.d, function(index, value) {
                    exportData += '<tr><td>' + value.ShippingMethodName + '</td><td>' + value.OrderID + '</td>';
                    exportData += '<td>' + value.ShipToName + '</td><td>' + value.ShippingRate + '</td></tr>';
                });
            } else {
                exportData += '<tr><td>No Records Found!</td></tr>';
            }
            exportData += '</tbody>';

            $('#shpipmentExportData').html(exportData);
            $("input[id$='HdnValue']").val('<table>' + exportData + '</table>');
            $("input[id$='_csvShipmentHiddenValue']").val($("#shpipmentExportData").table2CSV());
            $("#shpipmentExportData").html('');
        },
        ajaxSuccess: function(data) {
            switch (Shipments.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    var tableElements = '';
                    var grandTotal = '';
                    var dicountAmount = '';
                    var couponAmount = '';
                    var taxTotal = '';
                    var shippingCost = '';
                    var additionalNote = "";
                    $.each(data.d, function(index, value) {
                        var cv = "";
                        if (value.CostVariants != "") {
                            cv = "(" + value.CostVariants + ")";
                        }
                        tableElements += '<tr>';
                        tableElements += '<td>' + value.ItemName + cv + '</td>';
                        $("#shipmentDate").html(value.ShipmentDate);
                        tableElements += '<td>' + value.SKU + '</td>';
                        tableElements += '<td>' + value.ShippingAddress + '</td>';
                        tableElements += '<td>' + value.ShippingMethod + '</td>';
                        tableElements += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.ShippingRate + '</span></td>';
                        tableElements += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.Price.toFixed(2) + '</span></td>';
                        tableElements += '<td>' + value.Quantity + '</td>';
                        tableElements += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + (value.Price * value.Quantity).toFixed(2) + '</span></td>';
                        tableElements += '</tr>';
                        if (index == 0) {
                            taxTotal = '<tr>';
                            taxTotal += '<td></td><td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel"><b>Total Tax Amount:</b></td>';
                            taxTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.TaxTotal.toFixed(2) + '</span></td>';
                            taxTotal += '</tr>';
                            shippingCost = '<tr>';
                            shippingCost += '<td></td><td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel"><b>Total Shipping Rate:</b></td>';
                            shippingCost += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.TotalShippingRate.toFixed(2) + '</span></td>';
                            shippingCost += '</tr>';
                            dicountAmount = '<tr>';
                            dicountAmount += '<td></td><td></td><td></td><td></td><td></td><td></td><td  class="cssClassLabel"><b>Total Discount Amount:</b></td>';
                            dicountAmount += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.DiscountAmount.toFixed(2) + '</span></td>';
                            dicountAmount += '</tr>';
                            couponAmount = '<tr>';
                            couponAmount += '<td></td><td></td><td></td><td></td><td></td><td></td><td  class="cssClassLabel"><b>Total Coupon Amount:</b></td>';
                            couponAmount += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.TotalCoupon.toFixed(2) + '</span></td>';
                            couponAmount += '</tr>';
                            grandTotal = '<tr>';
                            grandTotal += '<td></td><td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel"><b>Total Amount:</b></td>';
                            grandTotal += '<td class="cssClassAlignRight"><span class="cssClassFormatCurrency" >' + value.GrandTotal.toFixed(2) + '</span></td>';
                            grandTotal += '</tr>';
                            additionalNote = value.Remarks;
                        }

                    });
                    $("#divShipmentsDetailForm").find('table>tbody').html(tableElements);
                    $("#divShipmentsDetailForm").find('table>tbody').append(taxTotal);
                    $("#divShipmentsDetailForm").find('table>tbody').append(shippingCost);
                    $("#divShipmentsDetailForm").find('table>tbody').append(dicountAmount);
                    $("#divShipmentsDetailForm").find('table>tbody').append(couponAmount);
                    $("#divShipmentsDetailForm").find('table>tbody').append(grandTotal);
                    $("#divShipmentsDetailForm").find("table>tbody tr:even").addClass("cssClassAlternativeEven");
                    $("#divShipmentsDetailForm").find("table>tbody tr:odd").addClass("cssClassAlternativeOdd");
                    if (additionalNote != '' && additionalNote != undefined) {
                        $(".remarks").html("").html("*Additional Note :- '" + additionalNote + "'");
                    } else {
                        $(".remarks").html("");
                    }
                    Shipments.HideDivs();
                    $("#divShipmentsDetailForm").show();
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                    break;
                case 2:
                    Shipments.BindShipmentDataForExport(data);
                    break;
            }
        }
    };
    Shipments.init();
});