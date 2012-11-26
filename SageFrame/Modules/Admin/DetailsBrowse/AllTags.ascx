<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AllTags.ascx.cs" Inherits="Modules_AspxAllTags_AllTags" %>

<script type="text/javascript">
    //<![CDATA[
    $(function() {
        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var customerId = AspxCommerce.utils.GetCustomerID();
        var popularTagsCount = 0;
        var AllTags = {
            BindAllTags: function() {
                $.ajax({
                    type: "POST",
                    url: AspxCommerce.utils.GetAspxServicePath() + "AspxCommerceWebService.asmx/GetAllPopularTags",
                    data: JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, count: popularTagsCount }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        if (msg.d.length > 0) {
                            var popularTags = '';
                            $.each(msg.d, function(index, item) {
                                var totalTagCount = item.TagCount;
                                var fontSize = (totalTagCount / 10 < 1) ? totalTagCount / 10 + 1 + "em" : (totalTagCount / 10 > 2) ? "2em" : totalTagCount / 10 + "em";
                                popularTags += "<a href=' " + aspxRedirectPath + 'tagsitems/tags.aspx?tagsId=' + item.ItemTagIDs + "' title='See all pages tagged with " + item.Tag + "' style='font-size: " + fontSize + "'>" + item.Tag + "</a>(<span class=\"cssClassTagCloudCount\">" + totalTagCount + "</span>), ";
                            });
                            $("#divAllTags").html(popularTags.substring(0, popularTags.length - 2));
                        }
                        else {
                            $("#divAllTags").html("Not any items have neen tagged yet!");
                        }
                    }
                });
            }
        }        
        AllTags.BindAllTags();
    });

    //]]>
</script>

<div class="cssClassCommonSideBox">
    <h2>
        <asp:Label ID="lblAllTagsTitle" runat="server" Text="All Tags" CssClass="cssClassPopularTags"></asp:Label>
    </h2>
    <div id="divAllTags" class="cssClassPopularTags">
    </div>
</div>
