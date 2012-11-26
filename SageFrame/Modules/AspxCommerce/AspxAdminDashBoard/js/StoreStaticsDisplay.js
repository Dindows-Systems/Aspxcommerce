$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var storeStaticsDisplay = {
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
                type: storeStaticsDisplay.config.type,
                contentType: storeStaticsDisplay.config.contentType,
                cache: storeStaticsDisplay.config.cache,
                async: storeStaticsDisplay.config.async,
                url: storeStaticsDisplay.config.url,
                data: storeStaticsDisplay.config.data,
                dataType: storeStaticsDisplay.config.dataType,
                success: storeStaticsDisplay.ajaxSuccess,
                error: storeStaticsDisplay.ajaxFailure
            });
        },
        HideDiv: function() {
            $("#divLW").hide();
            $("#div24hours").hide();
            $("#divCM").hide();
            $("#divYear").hide();
        },
        RemoveCharts: function() {
            $('.visualize-bar').remove();
            $('.visualize-pie').remove();
            $('.visualize-line').remove();
        },

        ShowCharts: function(id) {
            var optionChart = $("#ddlChartType").val();
            switch (optionChart) {
                case '1':
                    storeStaticsDisplay.RemoveCharts();
                    if (id == "24h" || id == null) {
                        $("#div24hours").visualize();
                    }
                    if (id == "7d") {
                        $("#divLW").visualize();
                    }
                    if (id == "1m") {
                        $("#divCM").visualize();
                    }
                    if (id == "1y") {
                        $("#divYear").visualize();
                    }
                    break;
                case '2':
                    storeStaticsDisplay.RemoveCharts();
                    if (id == "24h" || id == null) {
                        $("#div24hours").visualize({ type: 'pie' });
                    }
                    if (id == "7d") {
                        $("#divLW").visualize({ type: 'pie' });
                    }
                    if (id == "1m") {
                        $("#divCM").visualize({ type: 'pie' });
                    }
                    if (id == "1y") {
                        $("#divYear").visualize({ type: 'pie' });
                    }
                    break;
                case '3':
                    storeStaticsDisplay.RemoveCharts();
                    if (id == "24h" || id == null) {
                        $("#div24hours").visualize({ type: 'line' });
                    }
                    if (id == "7d") {
                        $("#divLW").visualize({ type: 'line' });
                    }
                    if (id == "1m") {
                        $("#divCM").visualize({ type: 'line' });
                    }
                    if (id == "1y") {
                        $("#divYear").visualize({ type: 'line' });
                    }
                    break;
            }
        },

        ShowChartRange: function() {
            var optionRange = $("#ddlRange").val();
            switch (optionRange) {
                case '24h':
                    storeStaticsDisplay.RemoveCharts();
                    $('#divChartType').show();
                    $("#div24hours").visualize().hide();
                    $("#ddlChartType").val('1');
                    $("#div24hours").visualize();
                    $("#ddlChartType").change(function() {
                        storeStaticsDisplay.ShowCharts(optionRange);
                    });
                    break;
                case '7d':
                    storeStaticsDisplay.RemoveCharts();
                    $('#divChartType').show();
                    $("#divLW").visualize().hide();
                    $("#ddlChartType").val('1');
                    $("#divLW").visualize();
                    $("#ddlChartType").change(function() {
                        storeStaticsDisplay.ShowCharts(optionRange);
                    });
                    break;
                case '1m':
                    storeStaticsDisplay.RemoveCharts();
                    $('#divChartType').show();
                    $("#divCM").visualize().hide();
                    $("#ddlChartType").val('1');
                    $("#divCM").visualize();
                    $("#ddlChartType").change(function() {
                        storeStaticsDisplay.ShowCharts(optionRange);
                    });
                    break;
                case '1y':
                    storeStaticsDisplay.RemoveCharts();
                    $('#divChartType').show();
                    $("#divYear").visualize().hide();
                    $("#ddlChartType").val('1');
                    $("#divYear").visualize();
                    $("#ddlChartType").change(function() {
                        storeStaticsDisplay.ShowCharts(optionRange);
                    });
                    break;
            }
        },
        BindChartByLastWeekAmount: function() {
            this.config.url = this.config.baseURL + "GetOrderChartDetailsByLastWeek";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        BindChartByCurrentMonthAmount: function() {
            this.config.url = this.config.baseURL + "GetOrderChartDetailsBycurentMonth";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        BindChartByOneYearAmount: function() {
            this.config.url = this.config.baseURL + "GetOrderChartDetailsByOneYear";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },
        BindChartBy24hoursAmount: function() {
            this.config.url = this.config.baseURL + "GetOrderChartDetailsBy24Hours";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (storeStaticsDisplay.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    if (msg.d.length > 0) {
                        var orderChart = '<table><thead><tr><td></td>';
                        $.each(msg.d, function(index, item) {
                            orderChart += "<th>" + item.Date + "</th></td>";
                        });
                        //                        orderChart += '</tr></thead><tbody><tr><th scope="row">Amount(A)</th>';
                        //                        $.each(msg.d, function(index, item) {
                        //                        orderChart += "<td>" + item.GrandTotal + "</td>";
                        //                        });
                        orderChart += '</tr><tr><th scope="row">Registered Customers(C)</th>';
                        $.each(msg.d, function(index, item) {
                            orderChart += "<td>" + item.CustomerVisit + "</td>";
                        });
                        orderChart += '</tr><tr><th scope="row">Orders(O)</th>';
                        $.each(msg.d, function(index, item) {
                            orderChart += "<td>" + item.Orders + "</td>";
                        });
                        orderChart += '</tr></tbody></table>';
                        $("#divLW").append(orderChart);
                    }
                    break;
                case 2:
                    if (msg.d.length > 0) {
                        var orderChart = '<table><thead><tr><td></td>';
                        $.each(msg.d, function(index, item) {
                            orderChart += "<th>" + item.Date + "Week</th></td>";
                        });
                        //                        orderChart += '</tr></thead><tbody><tr><th scope="row">Amount(A)</th>';
                        //                        $.each(msg.d, function(index, item) {
                        //                        orderChart += "<td>" + item.GrandTotal + "</td>";
                        //                        });
                        orderChart += '</tr><tr><th scope="row">Registered Customers(C)</th>';
                        $.each(msg.d, function(index, item) {
                            orderChart += "<td>" + item.CustomerVisit + "</td>";
                        });
                        orderChart += '</tr><tr><th scope="row">Orders(O)</th>';
                        $.each(msg.d, function(index, item) {
                            orderChart += "<td>" + item.Orders + "</td>";
                        });
                        orderChart += '</tr></tbody></table>';
                        $("#divCM").append(orderChart);
                    }
                    break;
                case 3:
                    if (msg.d.length > 0) {
                        var orderChart = '<table><thead><tr><td></td>';
                        $.each(msg.d, function(index, item) {
                            orderChart += "<th>" + item.Date + "</th></td>";
                        });
                        //                        orderChart += '</tr></thead><tbody><tr><th scope="row">Amount(A)</th>';
                        //                        $.each(msg.d, function(index, item) {
                        //                        orderChart += "<td>" + item.GrandTotal.toFixed(2) + "</td>";
                        //                        });
                        orderChart += '</tr><tr><th scope="row">Registered Customers(C)</th>';
                        $.each(msg.d, function(index, item) {
                            orderChart += "<td>" + item.CustomerVisit + "</td>";
                        });
                        orderChart += '</tr><tr><th scope="row">Orders(O)</th>';
                        $.each(msg.d, function(index, item) {
                            orderChart += "<td>" + item.Orders + "</td>";
                        });
                        orderChart += '</tr></tbody></table>';
                        $("#divYear").append(orderChart);
                    }
                    break;
                case 4:
                    if (msg.d.length > 0) {
                        var orderChart = '<table><thead><tr><td></td>';
                        $.each(msg.d, function(index, item) {
                            orderChart += "<th>" + item.Date + ":00</th></td>";
                        });
                        //                        orderChart += '</tr></thead><tbody><tr><th scope="row">Amount(A)</th>';
                        //                        $.each(msg.d, function(index, item) {
                        //                        orderChart += "<td>" + item.GrandTotal + "</td>";
                        //                        });
                        orderChart += '</tr><tr><th scope="row">Registered Customers(C)</th>';
                        $.each(msg.d, function(index, item) {
                            orderChart += "<td>" + item.CustomerVisit + "</td>";
                        });
                        orderChart += '</tr><tr><th scope="row">Orders(O)</th>';
                        $.each(msg.d, function(index, item) {
                            orderChart += "<td>" + item.Orders + "</td>";
                        });
                        orderChart += '</tr></tbody></table>';
                        $("#div24hours").append(orderChart);
                    }
                    break;
            }
        },
        ajaxFailure: function(msg) {
            switch (storeStaticsDisplay.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>Error Message</h1><p>Failed to load Charts By Week.</p>');
                    break;
                case 2:
                    csscody.error('<h1>Error Message</h1><p>Failed to load Charts By Month.</p>');
                    break;
                case 3:
                    csscody.error('<h1>Error Message</h1><p>Failed to load Charts By Year.</p>');
                    break;
                case 4:
                    csscody.error('<h1>Error Message</h1><p>Failed to load Charts By Day.</p>');
                    break;
            }
        },
        init: function(config) {
            storeStaticsDisplay.HideDiv();
            $("#ddlChartType").show();
            $("#lbla").show();
            storeStaticsDisplay.BindChartByLastWeekAmount();
            storeStaticsDisplay.BindChartByCurrentMonthAmount();
            storeStaticsDisplay.BindChartByOneYearAmount();
            storeStaticsDisplay.BindChartBy24hoursAmount();
            $(window).load(function() {
                storeStaticsDisplay.ShowCharts();
            });

            $("#ddlRange").change(function() {
                storeStaticsDisplay.ShowChartRange();
            });
            $("#ddlChartType").change(function() {
                storeStaticsDisplay.ShowCharts(null);
            });
        }
    }
    storeStaticsDisplay.init();
});
