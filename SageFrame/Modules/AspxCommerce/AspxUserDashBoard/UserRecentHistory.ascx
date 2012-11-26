<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserRecentHistory.ascx.cs"
    Inherits="Modules_AspxUserDashBoard_UserRecentHistory" %>

<script type="text/javascript">
    //<![CDATA[
    var RecentHistory = {
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
                type: RecentHistory.config.type,
                contentType: RecentHistory.config.contentType,
                cache: RecentHistory.config.cache,
                async: RecentHistory.config.async,
                data: RecentHistory.config.data,
                dataType: RecentHistory.config.dataType,
                url: RecentHistory.config.url,
                success: RecentHistory.ajaxSuccess,
                error: RecentHistory.ajaxFailure
            });
        },

        init: function() {
            RecentHistory.LoadUserDashRecentHistoryStaticImage();
            RecentHistory.GetUserRecentlyViewedItems();
            RecentHistory.GetUserRecentlyComparedItems();
            $("#btnDeleteMyViewed").click(function() {
                var viewedItemsIds = '';
                $('.recentlyViewedItemsChkbox').each(function() {
                    if ($(this).attr("checked")) {
                        viewedItemsIds += $(this).val() + ',';
                    }
                });
                if (viewedItemsIds != "") {
                    var properties = { onComplete: function(e) {
                        RecentHistory.ConfirmDeleteMultipleViewedItems(viewedItemsIds, e);
                    }
                    }
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete the selected items?</p>", properties);
                }
                else {
                    csscody.alert('<h2>Information Alert</h2><p>You need to select at least one item before you can do this. To select one or more items, just check the box before each item.</p>');
                }
            });
            $("#btnDeleteMyCompared").click(function() {
                var compareItemIds = '';
                $('.recentlyComparedItemsChkbox').each(function() {
                    if ($(this).attr("checked")) {
                        compareItemIds += $(this).val() + ',';
                    }
                });
                if (compareItemIds != "") {
                    var properties = { onComplete: function(e) {
                        RecentHistory.ConfirmDeleteMultipleCompareItems(compareItemIds, e);
                    }
                    }
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete the selected items?</p>", properties);
                }
                else {
                    csscody.alert('<h2>Information Alert</h2><p>You need to select at least one item before you can do this. To select one or more items, just check the box before each item.</p>');
                }
            });
        },

        LoadUserDashRecentHistoryStaticImage: function() {
            $('#ajaxUserRecentHistoryImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxUserRecentHistoryImage2').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        GetUserRecentlyViewedItems: function() {
            this.config.method = "GetUserRecentlyViewedItems";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvRecentlyViewedItems_pagesize").length > 0) ? $("#gdvRecentlyViewedItems_pagesize :selected").text() : 5;
            var defaultImage = "Modules/AspxCommerce/AspxItemsManagement/uploads/noitem.png";

            $("#gdvRecentlyViewedItems").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'ItemID', name: 'items_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'recentlyViewedItemsChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: 'Image', name: 'image', coltype: 'image', cssclass: 'cssClassImageHeader', controlclass: 'cssClassGridImage', alttext: '3', align: 'left' },
                    { display: 'AlternateText', name: 'alternate_text', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Item Name', name: 'item_name', cssclass: 'cssClassLinkHeader', controlclass: 'cssClassGridLink', coltype: 'link', align: 'left', url: 'item', queryPairs: '5' },
                    { display: 'Viewed On', name: 'viewed_on', coltype: 'label', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: 'SKU', name: 'SKU', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center', hide: true }
                ],
                buttons: [
                   { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', arguments: '2,3,5' },
			       { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', arguments: '3,5' }
			    ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 1: { sorter: false }, 6: { sorter: false} },
                defaultImage: defaultImage
            });

        },

        ConfirmDeleteMultipleViewedItems: function(ids, event) {
            RecentHistory.DeleteMultipleViewedItems(ids, event);
        },

        DeleteMultipleViewedItems: function(viewedItem_Ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteViewedItems";
                this.config.data = JSON2.stringify({ viewedItems: viewedItem_Ids, storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            }
            return false;
        },

        GetUserRecentlyComparedItems: function() {
            this.config.method = "GetUserRecentlyComparedItems";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvRecentlyComparedItems_pagesize").length > 0) ? $("#gdvRecentlyComparedItems_pagesize :selected").text() : 5;
            var defaultImage = aspxRootPath + "Modules/AspxCommerce/AspxItemsManagement/uploads/noitem.png";

            $("#gdvRecentlyComparedItems").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'ItemID', name: 'items_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'recentlyComparedItemsChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: 'Image', name: 'image', cssclass: 'cssClassImageHeader', controlclass: 'cssClassGridImage', coltype: 'image', alttext: '3', align: 'left' },
                    { display: 'AlternateText', name: 'alternate_text', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Item Name', name: 'item_name', cssclass: 'cssClassLinkHeader', controlclass: 'cssClassGridLink', coltype: 'link', align: 'left', url: 'item', queryPairs: '5' },
                    { display: 'Compared On', name: 'compared_on', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: 'SKU', name: 'SKU', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center', hide: true }
                	],
                buttons: [
                    { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', arguments: '2,3,5' },
			        { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', arguments: '3,5' }
			    ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 1: { sorter: false }, 6: { sorter: false} },
                defaultImage: defaultImage
            });
        },

        ConfirmDeleteMultipleCompareItems: function(ids, event) {
            RecentHistory.DeleteMultipleCompareItems(ids, event);
        },

        DeleteMultipleCompareItems: function(compareItem_Ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteComparedItems";
                this.config.data = JSON2.stringify({ compareItems: compareItem_Ids, storeID: storeId, portalID: portalId });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            }
            return false;
        },
        ajaxSuccess: function() {
            switch (RecentHistory.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.info('<h2>Successful Message</h2><p>Recently viewed item has been deleted successfully.</p>');
                    RecentHistory.GetUserRecentlyViewedItems();
                    break;
                case 2:
                    csscody.info('<h2>Successful Message</h2><p>Recently compared item has been deleted successfully.</p>');
                    RecentHistory.GetUserRecentlyComparedItems();
                    break;
            }
        },
        ajaxFailure: function() {
            switch (RecentHistory.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete recently viewed items!</p>');
                    break;
                case 2:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete compared items!</p>');
                    break;
            }
        }
    }
    $(function(){
        RecentHistory.init();
    });
//]]>
</script>

<div id="divUserRecentlyViewedItems">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblViewedTitle" runat="server" Text=" Viewed Items"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnDeleteMyViewed">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                    <%--<p>
                        <button type="button" id="btnAddViewedItemToWishList"><span><span>Add to WishList</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddViewedItemsToCart"><span><span>Add to Cart</span></span></button>
                    </p>--%>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="loading">                   
                     <img id="ajaxUserRecentHistoryImage2" src=""  alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvRecentlyViewedItems" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divUserRecentlyComparedItems">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblComparedTitle" runat="server" Text=" Compared Items"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnDeleteMyCompared">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                    <%--<p>
                        <button type="button" id="btnAddComparedItemstToWishList"><span><span>Add to WishList</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddComparedItemsToCart"><span><span>Add to Cart</span></span></button>
                    </p>--%>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="loading">
                    <img id="ajaxUserRecentHistoryImage" src=""  alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvRecentlyComparedItems" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
