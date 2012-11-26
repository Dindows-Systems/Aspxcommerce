var rate = 1;
var region;

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
    var BaseCurrency = '<%=MainCurrency %>';
    var SelectedCurrency = '<%=MainCurrency %>';
    region = '<%=Region %>';
    var curencyConversion = {
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
                type: curencyConversion.config.type,
                contentType: curencyConversion.config.contentType,
                cache: curencyConversion.config.cache,
                async: curencyConversion.config.async,
                url: curencyConversion.config.url,
                data: curencyConversion.config.data,
                dataType: curencyConversion.config.dataType,
                success: curencyConversion.ajaxSuccess,
                error: curencyConversion.ajaxFailure
            });
        },
        GetRate: function() {
            this.config.url = this.config.baseURL + "GetCurrencyRate";
            this.config.data = JSON2.stringify({ from: BaseCurrency, to: $("#ddlCurrency").val() });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        bindCurrencyList: function() {
            this.config.url = this.config.baseURL + "BindCurrencyList";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, cultureName: cultureName });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (curencyConversion.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    if (msg.d) {
                        rate = msg.d;
                    }
                    break;
                case 2:
                    if (msg.d.length > 0) {
                        var options = "";
                        $.each(msg.d, function(index, item) {
                            if (item.CurrencyCode == SelectedCurrency) {
                                options += '<option selected="selected" data-icon="' + aspxRootPath + 'images/flags/' + item.BaseImage + '"  data-html-text="' + item.CurrencyName + '-' + item.CurrencyCode + '" region="' + item.Region + '"  value="' + item.CurrencyCode + '" >' + item.CurrencyName + '-' + item.CurrencyCode + '</option>';
                            } else {
                                options += '<option data-icon="' + aspxRootPath + 'images/flags/' + item.BaseImage + '"  data-html-text="' + item.CurrencyName + '-' + item.CurrencyCode + '" region="' + item.Region + '"  value="' + item.CurrencyCode + '" >' + item.CurrencyName + '-' + item.CurrencyCode + '</option>';
                            }
                        });
                        $("#ddlCurrency").html(options);
                        MakeFancyDropDown();
                    }
                    break;
            }
        },
        ajaxFailure: function(msg) {
            switch (curencyConversion.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        },
        init: function(config) {
            if ($.session("Rate")) {
                rate = $.session("Rate");
                region = $.session("Region"); //$('select.makeMeFancy').find(":selected").text()
                SelectedCurrency = $.session("SelectedCurrency");
            }
            curencyConversion.bindCurrencyList();
            $('#ddlCurrency').change(function() {
                if ($("#ddlCurrency").val() == BaseCurrency) {
                    rate = 1;
                    region = $("#ddlCurrency option:selected").attr("region");
                    SelectedCurrency = $("#ddlCurrency option:selected").val();
                } else {
                    curencyConversion.GetRate();
                    region = $("#ddlCurrency option:selected").attr("region");
                    SelectedCurrency = $("#ddlCurrency option:selected").val();
                }
                $.session("Region", region);
                $.session("Rate", rate);
                $.session("SelectedCurrency", SelectedCurrency);
                if ($("#divLatestItems").length) {
                    LatestItems.GetLatestItems();
                }
            });
        }
    };
    curencyConversion.init();
});