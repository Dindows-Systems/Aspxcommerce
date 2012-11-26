<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReferredFriends.ascx.cs"
    Inherits="Modules_AspxUserDashBoard_ReferredFriends" %>

<script type="text/javascript">
	//<![CDATA[
    var ReferredFriends = {
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
                type: ReferredFriends.config.type,
                contentType: ReferredFriends.config.contentType,
                cache: ReferredFriends.config.cache,
                async: ReferredFriends.config.async,
                data: ReferredFriends.config.data,
                dataType: ReferredFriends.config.dataType,
                url: ReferredFriends.config.url,
                success: ReferredFriends.ajaxSuccess,
                error: ReferredFriends.ajaxFailure
            });
        },
        init: function() {
            ReferredFriends.LoadUserDashReferFriendStaticImage();
            ReferredFriends.GetMyReferredFriendsDetails();
            $('#btnDeleteSelected').click(function() {
                var emailAFriend_Ids = '';
                //Get the multiple Ids of the item selected
                $(".EmailsChkbox").each(function(i) {
                    if ($(this).attr("checked")) {
                        emailAFriend_Ids += $(this).val() + ',';
                    }
                });
                if (emailAFriend_Ids != "") {
                    var properties = { onComplete: function(e) {
                        ReferredFriends.ConfirmDeleteMultipleEmails(emailAFriend_Ids, e);
                    }
                    }
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete the selected items?</p>", properties);
                }
                else {
                    csscody.alert('<h2>Information Alert</h2><p>You need to select at least one item before you can do this. To select one or more items, just check the box before each item.</p>');
                }
            })
        },

        LoadUserDashReferFriendStaticImage: function() {
            $('#ajaxUserDashReferFriendImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        GetMyReferredFriendsDetails: function() {
            this.config.method = "GetUserReferredFriends";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvReferredFriends_pagesize").length > 0) ? $("#gdvReferredFriends_pagesize :selected").text() : 10;

            $("#gdvReferredFriends").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'EmailAFriendID', name: 'emailafriend_id', cssclass: 'cssClassHeadCheckBox', controlclass: 'itemsHeaderChkbox', coltype: 'checkbox', align: 'center', elemClass: 'EmailsChkbox', elemDefault: false },
                    { display: 'Item ID', name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Sendet Name', name: 'sender_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Sender Email', name: 'sender_email', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Receiver Name', name: 'receiver_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: false },
                    { display: 'Receiver Email', name: 'receiver_email', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Subject', name: 'subject', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Message', name: 'massage', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'StoreID', name: 'store_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'PortalID', name: 'portal_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'IsActive', name: 'isActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'IsDeleted', name: 'isActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'IsModified', name: 'isActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Added On', name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd', hide: false },
                    { display: 'Updated On', name: 'UpdatedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd', hide: true },
                    { display: 'Deleted On', name: 'DeletedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd', hide: true },
                    { display: 'Added By', name: 'AddedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Updated By', name: 'UpdatedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Deleted By', name: 'DeletedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Item Name', name: 'item_name', cssclass: '', controlclass: '', coltype: 'link', align: 'left', url: 'item', queryPairs: '20', showpopup: false, popupid: '', poparguments: '', popupmethod: '' },
                    { display: 'SKU', name: '_sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
    				],

                buttons: [
                { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'ReferredFriends.DeleteUserReferredFriends', arguments: '' }
			    ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { storeID: storeId, portalID: portalId, userName: userName },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 1: { sorter: false }, 21: { sorter: false} }
            });
        },

        ConfirmDeleteMultipleEmails: function(Ids, event) {
            ReferredFriends.DeleteMultipleEmailsInfo(Ids, event);
        },

        DeleteUserReferredFriends: function(tblID, argus) {
            switch (tblID) {
                case "gdvReferredFriends":
                    var properties = { onComplete: function(e) {
                        ReferredFriends.DeleteMultipleEmailsInfo(argus[0], e);
                    }
                    }
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this item?</p>", properties);
                    break;
                default:
                    break;
            }
        },


        DeleteMultipleEmailsInfo: function(emailAFriend_Ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteReferToFriendEmailUser";
                this.config.data = JSON2.stringify({ emailAFriendIDs: emailAFriend_Ids, storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            }
            return false;
        },
        ajaxSuccess: function() {
            switch (ReferredFriends.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.info('<h2>Successful Message</h2><p>Refered item has been deleted successfully.</p>');
                    ReferredFriends.GetMyReferredFriendsDetails();
                    break;
            }
        },
        ajaxFailure: function() {
            switch (ReferredFriends.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete refered item!</p>');
                    break;
            }
        }
    }
    $(function(){
        ReferredFriends.init();
    });
 	//]]>
</script>

<div class="cssClassCommonBox Curve">
    <div class="cssClassHeader">
        <h2>
            <asp:Label ID="lblReferredEmailGridHeading" runat="server" Text="My Referred Friends"></asp:Label>
        </h2>
        <div class="cssClassHeaderRight">
            <div class="cssClassButtonWrapper">
                <p>
                    <button type="button" id="btnDeleteSelected">
                        <span><span>Delete All Selected</span></span></button>
                </p>
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
                <img id="ajaxUserDashReferFriendImage" src=""  alt="loading...." title="loading...."/>
            </div>
            <div class="log">
            </div>
            <table id="gdvReferredFriends" cellspacing="0" cellpadding="0" border="0" width="100%">
            </table>
        </div>
    </div>
</div>
<input type="hidden" id="hdnEmailaFriendID" />
