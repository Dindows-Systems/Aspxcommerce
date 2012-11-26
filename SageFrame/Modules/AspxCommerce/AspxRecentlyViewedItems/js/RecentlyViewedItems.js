var RecentlyViewedItems="";
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
        RecentlyViewedItems = {
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
                    type: RecentlyViewedItems.config.type,
                    contentType: RecentlyViewedItems.config.contentType,
                    cache: RecentlyViewedItems.config.cache,
                    async: RecentlyViewedItems.config.async,
                    data: RecentlyViewedItems.config.data,
                    dataType: RecentlyViewedItems.config.dataType,
                    url: RecentlyViewedItems.config.url,
                    success: RecentlyViewedItems.ajaxSuccess,
                    error: RecentlyViewedItems.ajaxFailure
                });
            },
            init: function() {
                $("#divRecentViewedItems").hide();
                if (enableRecentlyViewed.toLowerCase() == 'true') {
                    RecentlyViewedItems.RecentlyViewedItemsList();
                    $("#divRecentViewedItems").show();
                }

            },

            RecentlyViewedItemsList: function() {
                var recentlyViewedCount = recentlyViewedCounts;
                this.config.url = this.config.baseURL + "GetRecentlyViewedItems";
                this.config.data = JSON2.stringify({ count: recentlyViewedCount, storeID: storeId, portalID: portalId, cultureName: cultureName, userName: userName });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            ajaxSuccess: function(data) {
                switch (RecentlyViewedItems.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        $("#tblRecentlyViewedItems>tbody").html('');
                        if (data.d.length > 0) {
                            $.each(data.d, function(index, item) {
                                var RecentlyViewedItems = '';
                                if (index % 2 == 0) {
                                    RecentlyViewedItems = '<tr class="cssClassAlternativeEven"><td><a href="' + aspxRedirectPath + 'item/' + item.SKU + '.aspx">' + item.ItemName + '</a></td></tr>';
                                }
                                else {
                                    RecentlyViewedItems = '<tr class="cssClassAlternativeOdd"><td><a href="' + aspxRedirectPath + 'item/' + item.SKU + '.aspx">' + item.ItemName + '</a></td></tr>';
                                }
                                $("#tblRecentlyViewedItems>tbody").append(RecentlyViewedItems);
                            });
                        }
                        else {
                            $("#tblRecentlyViewedItems>tbody").html("<tr><td><span class=\"cssClassNotFound\">You have not viewed any items yet!</span></tr></td>");
                        }
                        break;
                }
            }
        }

        RecentlyViewedItems.init();
    });