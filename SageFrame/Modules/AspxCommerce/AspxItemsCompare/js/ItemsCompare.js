var ItemsCompare = "";
var itemCompareCount = 0;
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
    var IDs = "";
    ItemsCompare = {
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
                type: ItemsCompare.config.type,
                contentType: ItemsCompare.config.contentType,
                cache: ItemsCompare.config.cache,
                async: ItemsCompare.config.async,
                data: ItemsCompare.config.data,
                dataType: ItemsCompare.config.dataType,
                url: ItemsCompare.config.url,
                success: ItemsCompare.ajaxSuccess,
                error: ItemsCompare.ajaxFailure
            });
        },

        init: function() {

            //            var x = window.location.href;
            //            if (x.indexOf("CompareItemList") != -1) {
            //                $("#divCompareItems").remove();
            //                $("#dvCompareList").show();
            //                ItemsCompare.LoadControl("Modules/AspxCommerce/AspxItemsCompare/ItemCompareDetails.ascx");

            //            }
            //            else {
            $("#divCompareItems").hide();
            if (enableCompareItem.toLowerCase() == 'true') {
                ItemsCompare.GetCompareItemList();
                $("#divCompareItems").show();
                $("#btncompare").bind("click", function() {
                    var ds = IDs;
                    if ($("#divLatestItems").length > 0) {
                        if (itemCompareCount > 1) {
                            $.cookies.set("ItemCompareDetail", ds);
                            $.cookies.set("showCompareList", 'true');
                            window.location.href = aspxRedirectPath + "Compare-Item-List.aspx";
                        } else {
                            csscody.alert("<h2>Information Alert</h2><p>You must have more than one item to compare!</p>");

                        }
                    } else {
                        if (itemCompareCount > 1) {
                            $.cookies.set("ItemCompareDetail", ds);
                            $.cookies.set("showCompareList", 'true');
                            window.location.href = aspxRedirectPath + "Compare-Item-List.aspx";
                        } else {
                            csscody.alert("<h2>Information Alert</h2><p>You must have more than one item to compare!</p>");

                        }
                    }
                    //  ItemsCompare.LoadControl("Modules/AspxCommerce/AspxItemsCompare/ItemCompareDetails.ascx");
                });

                $(".cssClassClose").click(function() {
                    $('#fade, #popuprel6').fadeOut();
                });
            }
            //   }
        },
        GetCompareItemList: function() {
            this.config.url = this.config.baseURL + "GetItemCompareList";
            this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName, sessionCode: sessionCode });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        DeleteCompareItem: function(itemId) {

            var properties = {
                onComplete: function(e) {
                    ItemsCompare.ConfirmDelete(itemId, e);
                }
            };
            // Ask user's confirmation before delete records
            csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this item?</p>", properties);
        },

        ConfirmDelete: function(id, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteCompareItem";
                this.config.data = JSON2.stringify({ ID: id, storeID: storeId, portalID: portalId, userName: userName, sessionCode: sessionCode });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            }
        },

        ClearAll: function() {
            itemCompareCount = 0;
            var properties = {
                onComplete: function(e) {
                    ItemsCompare.ConfirmClear(e);
                }
            };
            // Ask user's confirmation before delete records
            csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to clear compare list?</p>", properties);
        },

        ConfirmClear: function(event) {
            if (event) {
                this.config.url = this.config.baseURL + "ClearAll";
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, sessionCode: sessionCode });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            }
        },

        LoadControl: function(ControlName) {
            $.ajax({
                type: "POST",
                url: aspxservicePath + "LoadControlHandler.aspx/Result",
                data: "{ controlName:'" + aspxRootPath + ControlName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {
                    $('#divCompareElementsPopUP').html(response.d);
                    // ShowPopupControl("popuprel6");
                }
            });
        },
        ajaxSuccess: function(data) {
            switch (ItemsCompare.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    IDs = '';
                    $("#tblCompareItemList>tbody").html('');
                    $("#h2compareitems").html("<span class='cssClassCompareItem'>My Compared Items [" + data.d.length + "]</span>");
                    itemCompareCount = data.d.length;
                    if (data.d.length > 0) {                        
                        $.each(data.d, function(index, response) {
                            var ItemID = response.ItemID;
                            IDs += ItemID + "#";
                            if (index % 2 == 0) {
                                Items = '<tr class="cssClassAlternativeEven"><td class="cssClassCompareItemInfo"><a href="' + aspxRedirectPath + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a></td><td class="cssClassDelete"><img src="' + aspxTemplateFolderPath + '/images/admin/btndelete.png" onclick="ItemsCompare.DeleteCompareItem(' + ItemID + ');"/></td></tr>';
                            } else {
                                Items = '<tr class="cssClassAlternativeOdd"><td class="cssClassCompareItemInfo"><a href="' + aspxRedirectPath + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a></td><td class="cssClassDelete"><img src="' + aspxTemplateFolderPath + '/images/admin/btndelete.png" onclick="ItemsCompare.DeleteCompareItem(' + ItemID + ');"/></td></tr>';
                            }
                            $("#tblCompareItemList>tbody").append(Items);
                        });
                        IDs = IDs.substring(0, IDs.length - 1);
                        $("#compareItemBottons").show();
                    } else {
                        $("#compareItemBottons").hide();
                        $("#tblCompareItemList>tbody").html("<tr><td><span class=\"cssClassNotFound\">No items have been compared yet!</span></tr></td>");
                    }
                    break;
                case 2:
                    ItemsCompare.GetCompareItemList();
                    break;
                case 3:
                    ItemsCompare.GetCompareItemList();
                    if ($("#divLatestItems").length > 0) {
                        LatestItems.vars.countCompareItems = 0;
                    }
                    break;
            }
        }
    };
    ItemsCompare.init();
});    