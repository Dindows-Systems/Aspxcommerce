var RecentlyComparedItems="";
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
        RecentlyComparedItems = {
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
                    type: RecentlyComparedItems.config.type,
                    contentType: RecentlyComparedItems.config.contentType,
                    cache: RecentlyComparedItems.config.cache,
                    async: RecentlyComparedItems.config.async,
                    data: RecentlyComparedItems.config.data,
                    dataType: RecentlyComparedItems.config.dataType,
                    url: RecentlyComparedItems.config.url,
                    success: RecentlyComparedItems.ajaxSuccess,
                    error: RecentlyComparedItems.ajaxFailure
                });
            },

            init: function() {
                if (enableRecentlyCompared.toLowerCase() != 'true' || enableRecentlyCompared.toLowerCase() != 'true') {
                    $("#divRecentComparedItems").hide();
                }
                else {
                    RecentlyComparedItems.RecentlyCompareItemsList();
                }
            },
            RecentlyCompareItemsList: function() {
                var recentlyCompareCount = recentlyCompareCounts;
                this.config.url = this.config.baseURL + "GetRecentlyComparedItemList";
                this.config.data = JSON2.stringify({ count: recentlyCompareCount, storeID: storeId, portalID: portalId, cultureName: cultureName, userName: userName });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },

            ajaxSuccess: function(data) {
                switch (RecentlyComparedItems.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        $("#tblRecentlyComparedItemList>tbody").html('');
                        if (data.d.length > 0) {
                            $.each(data.d, function(index, item) {
                                var RecentlyCompareItems = '';
                                if (index % 2 == 0) {
                                    RecentlyCompareItems = '<tr class="cssClassAlternativeEven"><td><a href="' + aspxRedirectPath + 'item/' + item.SKU + '.aspx">' + item.ItemName + '</a></td></tr>';
                                }
                                else {
                                    RecentlyCompareItems = '<tr class="cssClassAlternativeOdd"><td><a href="' + aspxRedirectPath + 'item/' + item.SKU + '.aspx">' + item.ItemName + '</a></td></tr>';
                                }
                                $("#tblRecentlyComparedItemList>tbody").append(RecentlyCompareItems);
                            });
                        }
                        else {
                            $("#tblRecentlyComparedItemList>tbody").html("<tr><td><span class=\"cssClassNotFound\">You have not viewed any items yet!</span></tr></td>");
                        }
                        break;
                }
            }
        }

        RecentlyComparedItems.init();
    });