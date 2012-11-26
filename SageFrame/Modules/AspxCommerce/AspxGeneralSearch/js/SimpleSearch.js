
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
    var simpleSearch = {
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
                type: simpleSearch.config.type,
                contentType: simpleSearch.config.contentType,
                cache: simpleSearch.config.cache,
                async: simpleSearch.config.async,
                url: simpleSearch.config.url,
                data: simpleSearch.config.data,
                dataType: simpleSearch.config.dataType,
                success: simpleSearch.ajaxSuccess,
                error: simpleSearch.ajaxFailure
            });
        },
        PassSimpleSearchTerm: function() {
            var categoryId = $("#ddlSimpleSearchCategory").val();
            var searchText = $.trim($("#txtSimpleSearchText").val());

            if (categoryId == "0") {
                categoryId = 0;
            }
            if (searchText == "What are you shopping today?" || searchText == "") {
                //alert("Enter search text");
                $("#txtSimpleSearchText").focus().val(''); 
                return false;
            }
            else if ($("#txtSimpleSearchText").val() != '') {
                //Redirect HERE
                window.location.href = aspxRedirectPath + "search/simplesearch.aspx?cid=" + categoryId + " &q=" + searchText;
                return false;
            }
        },
        LoadAllCategoryForSimpleSearch: function() {
            var isActive = true;
            this.config.url = this.config.baseURL + "GetAllCategoryForSearch";
            this.config.data = JSON2.stringify({ prefix: '---', isActive: isActive, culture: cultureName, storeID: storeId, portalID: portalId, userName: userName });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (simpleSearch.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    var Elements = '';
                    Elements += '<option value="0">--All Category--</option>';
                    $.each(msg.d, function(index, item) {
                        Elements += "<option value=" + item.CategoryID + ">" + item.LevelCategoryName + "</option>";
                    });
                    $("#ddlSimpleSearchCategory").html(Elements);
                    break;
                case 2:
                    response($.map(msg.d, function(item) {                       
                        return {
                            value: item.SearchTerm
                        }
                    }));
                    break;
            }
        },
        init: function(config) {
            $('#txtSimpleSearchText').val('');

            simpleSearch.LoadAllCategoryForSimpleSearch();

            $('#txtSimpleSearchText').autocomplete({
                source: function(request, response) {
                    $.ajax({
                        url: aspxservicePath + "AspxCommerceWebService.asmx/GetSearchedTermList",
                        data: JSON2.stringify({ search: $('#txtSimpleSearchText').val(), storeID: storeId, portalID: portalId }),
                        dataType: "json",
                        async: false,
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function(data) { return data; },
                        success: function(data) {
                            response($.map(data.d, function(item) {
                                return {
                                    value: item.SearchTerm
                                }
                            }))
                        },
                        error: function(XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 2
            });

            $("#btnSimpleSearch").bind("click", function() {
                simpleSearch.PassSimpleSearchTerm();
            });
            $("#lnkAdvanceSearch").click(function() {
                if (userFriendlyURL) {
                    window.location.href = aspxRedirectPath + "Advance-Search.aspx";
                }
                else {
                    window.location.href = aspxRedirectPath + "Advance-Search";
                }
            });
            $(".cssClassSageSearchBox").each(function() {
                if ($(this).val() == "") {
                    $(this).addClass("lightText").val("What are you shopping today?");
                }
            });

            $(".cssClassSageSearchBox").bind("focus", function() {
                if ($(this).val() == "What are you shopping today?") {
                    $(this).removeClass("lightText").val("");
                }
                // focus lost action
            });

            $(".cssClassSageSearchBox").bind("blur", function() {
                if ($(this).val() == "") {
                    $(this).val("What are you shopping today?").addClass("lightText");
                }
            });

            $("#txtSimpleSearchText").bind("focus", function() {
                $("#txtSimpleSearchText").val("");
            });
            $("#txtSimpleSearchText").keyup(function(event) {
                if (event.keyCode == 13) {
                    $("#btnSimpleSearch").click();
                }
            });
            $(".cssClassSageSearchBox").bind("focusout", function() {
                if ($(this).val() == "") {
                    $(this).val("What are you shopping today?").addClass("lightText");
                }
            });
        }
    }
    simpleSearch.init();
});