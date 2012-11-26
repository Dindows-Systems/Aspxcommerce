var AllTags="";
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

    var arrTagItems = new Array();
    var arrTagItemsToBind = new Array();
    AllTags = {
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
        init: function() {
            AllTags.HideAll();
            $("#divShowTagDetails").show();
            AllTags.LoadAllTagsStaticImage();
            AllTags.BindTagDetails(null, null);
            AllTags.GetStatusOfTag();
            $("#btnSaveTag").click(function() {
                var validTag = $("#form1").validate({
                    messages: {
                        Tag: {
                            required: '*',
                            maxlength: "*"
                        }
                    }
                });
                if (validTag.form()) {
                    AllTags.UpdateTags();
                } else return false;
            });
            $("#btnSearchTags").click(function() {
                AllTags.SearchTags();
            });
            $("#btnBack").click(function() {
                AllTags.HideAll();
                $("#divShowTagDetails").show();
            });
            $("#btnCancel").click(function() {
                AllTags.HideAll();
                $("#divShowTagDetails").show();
            });
            $("#ddlTagItemDisplay").change(function() {
                var items_per_page = $(this).val();
                $("#Pagination").pagination(arrTagItems.length, {
                    callback: pageselectCallback,
                    items_per_page: items_per_page,
                    //num_display_entries: 10,
                    //current_page: 0,
                    prev_text: "Prev",
                    next_text: "Next",
                    prev_show_always: false,
                    next_show_always: false
                });
            });
            $('#btnDeleteSelected').click(function() {
                var itemTags_Ids = '';
                //Get the multiple Ids of the item selected
                $(".TagChkbox").each(function(i) {
                    if ($(this).attr("checked")) {
                        itemTags_Ids += $(this).val() + '%';
                    }
                });
                if (itemTags_Ids != "") {
                    var properties = {
                        onComplete: function(e) {
                            AllTags.ConfirmDeleteMultipleItemTags(itemTags_Ids, e);
                        }
                    };
                    csscody.confirm("<h1>Delete Confirmation</h1><p>Are you sure you want to the selected tag(s)?</p>", properties);
                }
                else {
                    csscody.alert('<h1>Information Alert</h1><p>Please select at least one tag before delete.</p>');
                }
            });
            $('#txtSearchTag,#ddlStatus').keyup(function(event) {
                if (event.keyCode == 13) {
                    AllTags.SearchTags();
                }
            });
        },

        ajaxCall: function(config) {
            $.ajax({
                type: AllTags.config.type,
                contentType: AllTags.config.contentType,
                cache: AllTags.config.cache,
                async: AllTags.config.async,
                data: AllTags.config.data,
                dataType: AllTags.config.dataType,
                url: AllTags.config.url,
                success: AllTags.ajaxSuccess,
                error: AllTags.ajaxFailure
            });
        },
        LoadAllTagsStaticImage: function() {
            $('#ajaxAllTagsImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        UpdateTags: function(itemTagIDs) {
            var itemTagIDs = $("#hdnItemTagID").val();
            var hdnStatusID = $("#hdnStatusID").val();
            var hdnTag = $("#hdnTag").val();
            var newTag = $("#txtTag").val();
            var newStatusID = $("#selectStatus").val();
            if (hdnStatusID != newStatusID || hdnTag != newTag) {
                this.config.url = this.config.baseURL + "UpdateTag";
                this.config.data = JSON2.stringify({ itemTagIDs: itemTagIDs, newTag: newTag, statusID: newStatusID, storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            }
            else {
                AllTags.HideAll();
                $("#divShowTagDetails").show();
            }
        },

        HideAll: function() {
            $("#divEditTag").hide();
            $("#divShowTagDetails").hide();
            $("#divTagedItemsDetails").hide();
        },

        clearTagForm: function() {

            $('#txtTag').removeClass('error');
            $('#txtTag').parents('td').find('label').remove();
        },

        GetStatusOfTag: function() {
            this.config.url = this.config.baseURL + "GetStatus";
            this.config.data = JSON2.stringify({ cultureName: cultureName });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config)
        },

        BindTagDetails: function(tags, tagStatus) {
            this.config.method = "GetTagDetailsList"
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvTags_pagesize").length > 0) ? $("#gdvTags_pagesize :selected").text() : 10;

            $("#gdvTags").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                { display: 'ItemTagIDs', name: 'itemtag_ids', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TagChkbox', elemDefault: false, controlclass: 'tagHeaderChkbox' },
                { display: 'Tag', name: 'tag', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'User Counts', name: 'user_count', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', showpopup: false },
				{ display: 'Item Counts', name: 'item_count', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', showpopup: false },
				{ display: 'Status', name: 'status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				{ display: 'StatusID', name: 'status_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
				{ display: 'UserIDs', name: 'user_ids', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
				{ display: 'ItemIDs', name: 'item_ids', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },

				{ display: 'Tag Count', name: 'tag_count', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
				{ display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
				],
                buttons: [
                          { display: 'View', name: 'view_items', enable: true, _event: 'click', trigger: '3', callMethod: 'AllTags.ShowTaggedItems', arguments: '1,2,3,4,5,6,7,8,9' },
			              { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'AllTags.EditTags', arguments: '0,1,5' },
			              { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'AllTags.DeleteTags', arguments: '0' }
			            ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { tag: tags, tagStatus: tagStatus, storeId: storeId, portalId: portalId, userName: userName },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 9: { sorter: false} }
            });
        },

        EditTags: function(tblID, argus) {
            switch (tblID) {
                case "gdvTags":
                    $("#" + lblEditTagDetails).html("Edit Tag: '" + argus[4] + "'");
                    AllTags.HideAll();
                    AllTags.clearTagForm();
                    $("#divEditTag").show();
                    $("#hdnItemTagID").val(argus[3]);
                    $("#hdnTag").val(argus[4]);
                    $("#hdnStatusID").val(argus[5]);
                    $("#txtTag").val(argus[4]);
                    $("#selectStatus").val(argus[5]);
                    break;
                default:
                    break;
            }
        },

        DeleteTags: function(tblID, argus) {
            switch (tblID) {
                case "gdvTags":
                    var properties = { onComplete: function(e) {
                        AllTags.ConfirmDeleteTag(argus[3], e);
                    }
                    }
                    csscody.confirm("<h1>Delete Confirmation</h1><p>Are you sure you want to delete this tag?</p>", properties);
                    break;
                default:
                    break;
            }
        },

        ConfirmDeleteTag: function(itemTagIDs, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteTag";
                this.config.data = JSON2.stringify({ itemTagIDs: itemTagIDs, storeID: storeId, portalID: portalId });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config)
            }
            return false;
        },

        ConfirmDeleteMultipleItemTags: function(Ids, event) {
            AllTags.DeleteItemTagInfo(Ids, event);
        },

        DeleteItemTagInfo: function(_TagID_Ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteMultipleTag";
                this.config.data = JSON2.stringify({ itemTagIDs: _TagID_Ids, storeID: storeId, portalID: portalId });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config)
            }
            return false;
        },

        ShowTaggedItems: function(tblID, argus) {
            switch (tblID) {
                case "gdvTags":
                    $("#" + lblTagViewHeading).html("View Tag: '" + argus[3] + "'");
                    AllTags.BindTagedItemsDetails(argus[9]);
                    break;
                default:
                    break;
            }
        },
        BindTagedItemsDetails: function(IDs) {
            this.config.url = this.config.baseURL + "GetItemsByMultipleItemID";
            this.config.data = JSON2.stringify({ itemIDs: IDs, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config)
        },
        SearchTags: function() {
            var tags = $.trim($("#txtSearchTag").val());
            var tagStatus = '';
            if (tags.length < 1) {
                tags = null;
            }
            if ($("#ddlStatus").val() != 0) {
                tagStatus = $.trim($("#ddlStatus").val());
            }
            else {
                tagStatus = null;
            }
            AllTags.BindTagDetails(tags, tagStatus);
        },
        ajaxSuccess: function(data) {
            switch (AllTags.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.info('<h2>Successful Message</h2><p>Tag has been updated successfully.</p>');
                    AllTags.BindTagDetails(null, null);
                    AllTags.HideAll();
                    $("#divShowTagDetails").show();
                    break;
                case 2:
                    $.each(data.d, function(index, item) {
                        $("#selectStatus").append("<option value=" + item.StatusID + ">" + item.Status + "</option>");
                        $("#ddlStatus").append("<option value=" + item.StatusID + ">" + item.Status + "</option>");
                    });
                    break;
                case 3:
                    AllTags.BindTagDetails(null, null);
                    csscody.info('<h2>Successful Message</h2><p>Tag has been deleted successfully.</p>');
                    break;
                case 4:
                    AllTags.BindTagDetails(null, null);
                    csscody.info('<h2>Successful Message</h2><p>Selected tag(s) has been deleted successfully.</p>');
                    break;
                case 5:
                    var tableElements = "";
                    AllTags.HideAll();
                    $("#divTagedItemsDetails").show();
                    $.each(data.d, function(index, item) {
                        tableElements += '<tr>';
                        tableElements += '<td><img height="81" width="100" src="' + aspxRootPath + item.ImagePath + '" alt="' + item.AlternateText + '" title="' + item.Name + '" /></td>';
                        tableElements += '<td>' + item.Name + '</td>';
                        tableElements += '<td>' + item.SKU + '</td>';
                        tableElements += '<td class="cssClassAlignRight"><label class="cssClassLabel cssClassFormatCurrency">' + item.Price + '</td>';
                        tableElements += '</tr>';

                    });
                    $("#divTagedItemsDetails").find('table>tbody').html(tableElements);
                    $("#divTagedItemsDetails").find("table>tbody tr:even").addClass("cssClassAlternativeEven");
                    $("#divTagedItemsDetails").find("table>tbody tr:odd").addClass("cssClassAlternativeOdd");
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                    break;
            }
        }
    }
    AllTags.init();
});