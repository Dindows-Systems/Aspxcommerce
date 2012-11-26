<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserDownloadableProducts.ascx.cs"
    Inherits="Modules_AspxUserDashBoard_UserDownloadableProducts" %>

<script type="text/javascript">
    //<![CDATA[
    var UserDownloadable = {
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
        vars: {
            isRemainDownload: ''
        },
        ajaxCall: function(config) {
            $.ajax({
                type: UserDownloadable.config.type,
                contentType: UserDownloadable.config.contentType,
                cache: UserDownloadable.config.cache,
                async: UserDownloadable.config.async,
                data: UserDownloadable.config.data,
                dataType: UserDownloadable.config.dataType,
                url: UserDownloadable.config.url,
                success: UserDownloadable.ajaxSuccess,
                error: UserDownloadable.ajaxFailure
            });
        },
        init: function() {
            UserDownloadable.LoadUserDashDownloadableImage();
            UserDownloadable.BindCustomerDownLoadItemsGrid(null, null);

            $("#btnDeleteCustDownloadableItem").click(function() {
                var orderItem_Ids = '';
                //Get the multiple Ids of the item selected
                $(".orderitemsChkbox").each(function(i) {
                    if ($(this).attr("checked")) {
                        orderItem_Ids += $(this).val() + ',';
                    }
                });
                if (orderItem_Ids != "") {
                    var properties = { onComplete: function(e) {
                        UserDownloadable.ConfirmDeleteMultipleOrderItem(orderItem_Ids, e);
                    }
                    }
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Do you want to delete all selected Order Items?</p>", properties);
                }
                else {
                    csscody.alert('<h2>Information Alert</h2><p>You need to select at least one item before you can do this.<br/> To select one or more items, just check the box before each item.</p>');
                }
            });
        },

        LoadUserDashDownloadableImage: function() {
            $('#ajaxUserDashBoardDownloadImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },
        SearchItems: function() {
            var sku = $.trim($("#txtSearchSKU").val());
            var Nm = $.trim($("#txtSearchName").val());
            if (sku.length < 1) {
                sku = null;
            }
            if (Nm.length < 1) {
                Nm = null;
            }
            var isAct = $.trim($("#ddlIsActive").val()) == "" ? null : ($.trim($("#ddlIsActive").val()) == "True" ? true : false);

            UserDownloadable.BindCustomerDownLoadItemsGrid(sku, Nm);
        },

        BindCustomerDownLoadItemsGrid: function(sku, Nm) {
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCustomerDownLoadItems_pagesize").length > 0) ? $("#gdvCustomerDownLoadItems_pagesize :selected").text() : 10;

            $("#gdvCustomerDownLoadItems").sagegrid({
                url: this.config.baseURL,
                functionMethod: 'GetCustomerDownloadableItems',
                colModel: [
                { display: 'OrderItemID', name: 'orderitemid', cssclass: 'cssClassHeadCheckBox', controlclass: 'classClassCheckBox', coltype: 'checkbox', align: 'center', elemDefault: false, elemClass: 'orderitemsChkbox' },
                { display: 'OrderItemID#', name: 'order_item_id', cssclass: '', coltype: 'label', align: 'left', controlclass: '', hide: true },
                { display: 'OrderID#', name: 'orderid', cssclass: '', coltype: 'label', align: 'left', controlclass: '', hide: true },
                { display: 'RandomNo', name: 'random_no', cssclass: '', controlclass: '', coltype: 'label', align: 'left', controlclass: '', hide: true },
                { display: 'ItemID', name: 'itemid', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: 'SKU', name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: 'Item Name', name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'Sample Link', name: 'sample_link', cssclass: '', controlclass: 'cssSClassDownload', coltype: 'linklabel', align: 'left', value: '9', downloadarguments: '14,4', downloadmethod: 'UserDownloadable.DownloadSampleFile', hide: true },
                { display: 'Actual Link', name: 'actual_link', cssclass: '', controlclass: 'cssAClassDownload cssDClassDownload', coltype: 'download', align: 'left', value: '10', randomValue: '3', downloadarguments: '14,4,11,1,3', downloadmethod: 'UserDownloadable.DownloadActualFile' },
                { display: 'Sample File', name: 'sample_file', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: 'Actual File', name: 'actual_file', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                { display: 'Order Status ID', name: 'orderstatusid', cssclass: '', coltype: 'label', align: 'left', controlclass: '', hide: true },
                { display: 'Status', name: 'status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'Download', name: 'download', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'Remaining Download', name: 'remaindownload', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'Last Download Date', name: 'lastdownload', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
				],

                buttons: [
                { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'UserDownloadable.DeleteCustomerDownloadItem', arguments: '2' }
			    ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { sku: sku, name: Nm, storeId: storeId, portalId: portalId, cultureName: cultureName, userName: userName },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 16: { sorter: false} }
            });
        },

        DownloadActualFile: function(argus) {
            var itemid = argus[1];
            var orderItemId = argus[3];
            UserDownloadable.config.url = UserDownloadable.config.baseURL + "CheckRemainingDownload";
            UserDownloadable.config.data = JSON2.stringify({ itemId: itemid, orderItemId: orderItemId, storeId: storeId, portalId: portalId, userName: userName });
            UserDownloadable.config.ajaxCallMode = 1;
            UserDownloadable.ajaxCall(UserDownloadable.config);

            if (!UserDownloadable.vars.isRemainDownload) {
                csscody.alert('<h2>Information Alert</h2><p>The download exceeds the maximum download limit!</p>');
                return false;
            }
            else if (argus[2] == 3 && argus[0] > 0 && UserDownloadable.vars.isRemainDownload) {
                $(".cssDClassDownload_" + argus[4] + "").jDownload({
                    root: aspxfilePath,
                    dialogTitle: 'AspxCommerce download actual item:',
                    stop: function() {
                        UserDownloadable.UpdateDownloadCount(itemid, orderItemId);
                    }
                });
            }
            else {
                csscody.alert('<h2>Information Alert</h2><p>Your order is not completed. Try later!!</p>');
                return false;
            }
        },

        DownloadSampleFile: function(argus) {
            $(".cssSClassDownload").jDownload({
                root: aspxfilePath,
                dialogTitle: 'AspxCommerce download sample item:'
            });
        },

        UpdateDownloadCount: function(itemid, orderItemId) {
            var itemID = itemid;
            this.config.url = this.config.baseURL + "UpdateDownloadCount";
            this.config.data = JSON2.stringify({ itemID: itemID, orderItemID: orderItemId, downloadIP: downloadIP, storeID: storeId, portalID: portalId, userName: userName });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        DeleteCustomerDownloadItem: function(tblID, argus) {
            switch (tblID) {
                case "gdvCustomerDownLoadItems":
                    var properties = { onComplete: function(e) {
                        UserDownloadable.DeleteCustomerDownloadableItem(argus[0], e);
                    }
                    }
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this?</p>", properties);
                    break;
                default:
                    break;
            }
        },

        ConfirmDeleteMultipleOrderItem: function(Ids, event) {
            UserDownloadable.DeleteCustomerDownloadableItem(Ids, event);
        },

        DeleteCustomerDownloadableItem: function(_OrderItemID, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteCustomerDownloadableItem";
                this.config.data = JSON2.stringify({ orderItemID: _OrderItemID, storeId: storeId, portalId: portalId, userName: userName });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            }
            return false;
        },
        ajaxSuccess: function(data) {
            switch (UserDownloadable.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    UserDownloadable.vars.isRemainDownload = data.d;
                    break;
                case 2:
                    UserDownloadable.BindCustomerDownLoadItemsGrid(null, null);
                    break;
                case 3:
                    csscody.alert('<h2>Successful Message</h2><p>Downloadable item has been deleted successfully.</p>');
                    UserDownloadable.BindCustomerDownLoadItemsGrid(null, null);
                    break;
            }
        },
        ajaxFailure: function(data) {
            switch (UserDownloadable.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    csscody.error('<h2>Error Message</h2><p>Failed to downloadable item delete!</p>');
                    break;
            }
        }
    }
    $(function(){
        UserDownloadable.init();
    });
    //]]>
</script>

<div id="gdvDownLoadableItems_grid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="Customer Downloadable Items"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnDeleteCustDownloadableItem">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="cssClassSearchPanel cssClassFormWrapper">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel">
                                    Name:</label>
                                <input type="text" id="txtSearchName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    SKU:</label>
                                <input type="text" id="txtSearchSKU" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="SearchItems()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxUserDashBoardDownloadImage" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvCustomerDownLoadItems" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
