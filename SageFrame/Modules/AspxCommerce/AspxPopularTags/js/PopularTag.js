var PopularTags="";
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

    PopularTags = {
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
                type: PopularTags.config.type,
                contentType: PopularTags.config.contentType,
                cache: PopularTags.config.cache,
                async: PopularTags.config.async,
                data: PopularTags.config.data,
                dataType: PopularTags.config.dataType,
                url: PopularTags.config.url,
                success: PopularTags.ajaxSuccess,
                error: PopularTags.ajaxFailure
            });
        },

        init: function() {
            PopularTags.BindAllPopularTags();
        },

        BindAllPopularTags: function() {
            if (popularTagsCount > 0) {
                this.config.url = this.config.baseURL + "GetAllPopularTags";
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, count: popularTagsCount });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            } else {
                $("#divPopularTags").html("<span class=\"cssClassTagsNotFound\">Not any items have been tagged yet!</span>");
                $("#divViewAllTags").hide();
            }
        },

        ajaxSuccess: function(data) {
            switch (PopularTags.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    if (data.d.length > 0) {
                        var totalTagCount = 0;
                        var tagCount = 0;
                        //create list for tag links
                        $("<ul>").attr("id", "tagList").appendTo("#divPopularTags");
                        $.each(data.d, function(index, item) {
                            if (index == 0) {
                                tagCount = item.RowTotal;
                            }
                            //create item
                            var li = $("<li>");
                            // alert(item.ItemIDs);
                            if (index != data.d.length - 1) {
                                $("<a>").text(item.Tag + ", ").attr({ title: "See all items tagged with " + item.Tag, href: aspxRedirectPath + 'tagsitems/tags.aspx?tagsId=' + item.ItemTagIDs + '' }).appendTo(li);
                            }
                            else {
                                $("<a>").text(item.Tag).attr({ title: "See all items tagged with " + item.Tag, href: aspxRedirectPath + 'tagsitems/tags.aspx?tagsId=' + item.ItemTagIDs + '' }).appendTo(li);
                            }
                            totalTagCount = item.TagCount;
                            //set tag size
                            li.children().css("fontSize", (totalTagCount / 10 < 1) ? totalTagCount / 10 + 1 + "em" : (totalTagCount / 10 > 2) ? "2em" : totalTagCount / 10 + "em");

                            //add to list
                            li.appendTo("#tagList");
                        });
                        if (tagCount > popularTagsCount && tagCount > 0) {
                            $("#divViewAllTags").html('<a href="' + aspxRedirectPath + 'tags/alltags.aspx" title="View all tags">» View All Tags</a>');
                            $("#divViewAllTags").show();
                        }
                        else {
                            $("#divViewAllTags").hide();
                        }
                    }
                    else {
                        $("#divPopularTags").html("<span class=\"cssClassTagsNotFound\">Not any items have been tagged yet!</span>");
                        $("#divViewAllTags").hide();
                    }
                    break;
            }
        }
    }
    PopularTags.init();
});