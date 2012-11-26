    var PendingTags="";
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
        PendingTags = {
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
                PendingTags.HideAll();
                PendingTags.LoadPendingTagsStaticImage();
                $("#divShowTagDetails").show();
                PendingTags.BindTagDetails(null);
                PendingTags.GetStatusOfTag();
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
                        PendingTags.UpdateTags();
                    } else return false;
                });
                $("#btnBack").click(function() {
                    PendingTags.HideAll();
                    $("#divShowTagDetails").show();
                });
                $("#btnCancel").click(function() {
                    PendingTags.HideAll();
                    $("#divShowTagDetails").show();
                });
                $("#btnSearchPendingTags").click(function() {
                    PendingTags.SearchTags();
                });
                $('#txtSearchTag').keyup(function(event) {
                    if (event.keyCode == 13) {
                        PendingTags.SearchTags();
                    }
                });
                $('#btnApproveAllSelected').click(function() {
                    var tags_ids = '';
                    $("#gdvTags .attrChkbox").each(function(i) {
                        if ($(this).attr("checked")) {
                            tags_ids += $(this).val() + ',';
                        }
                    });
                    if (tags_ids != "") {
                        var properties = { onComplete: function(e) {
                            PendingTags.ApproveAllSelectedTags(tags_ids, e);
                        }
                        }
                        csscody.messageInfo("<h2>Approve Confirmation</h2><p>Are you sure you want to approve the selected tag(s)?</p>", properties);
                    }
                    else {
                        csscody.alert('<h2>Information Alert</h2><p>Please select at least one pending tag before approve it.</p>');
                    }
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
            },
            LoadPendingTagsStaticImage: function() {
                $('#ajaxPendingTagsImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: PendingTags.config.type,
                    contentType: PendingTags.config.contentType,
                    cache: PendingTags.config.cache,
                    async: PendingTags.config.async,
                    data: PendingTags.config.data,
                    dataType: PendingTags.config.dataType,
                    url: PendingTags.config.url,
                    success: PendingTags.ajaxSuccess,
                    error: PendingTags.ajaxFailure
                });
            },

            ApproveAllSelectedTags: function(tagsIDs, event) {
                if (event) {
                    var newTags = "";
                    var newTagStatus = 3;
                    this.config.url = this.config.baseURL + "UpdateTag";
                    this.config.data = JSON2.stringify({ itemTagIDs: tagsIDs, newTag: newTags, statusID: newTagStatus, storeID: storeId, portalID: portalId, userName: userName });
                    this.config.ajaxCallMode = 1;
                    this.ajaxCall(this.config)
                }
                return false;
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
                    this.config.ajaxCallMode = 2;
                    this.ajaxCall(this.config);
                }
                else {
                    PendingTags.HideAll();
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
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config)
            },

            BindTagDetails: function(tags) {
                this.config.method = "GetTagDetailsListPending";
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvTags_pagesize").length > 0) ? $("#gdvTags_pagesize :selected").text() : 10;

                $("#gdvTags").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                { display: 'ItemTagIDs', name: 'itemtag_ids', cssclass: 'cssClassHeadCheckBox', controlclass: 'attribHeaderChkbox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkbox', elemDefault: false },
                { display: 'Tag', name: 'tag', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'User Counts', name: 'user_count', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', showpopup: false },
				{ display: 'Item Counts', name: 'item_count', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', showpopup: false },
				{ display: 'Status', name: 'status', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
				{ display: 'StatusID', name: 'status_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
				{ display: 'UserIDs', name: 'user_ids', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
				{ display: 'ItemIDs', name: 'item_ids', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
				{ display: 'Tag Count', name: 'tag_count', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
				{ display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
				],
                    buttons: [
                    //{ display: 'View', name: 'view_items', enable: true, _event: 'click', trigger: '3', callMethod: 'PendingTags.ShowTaggedItems', arguments: '7' },
                   {display: 'View', name: 'view_items', enable: true, _event: 'click', trigger: '3', callMethod: 'PendingTags.ShowTaggedItems', arguments: '1,2,3,4,5,6,7,8,9' },
                    { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'PendingTags.EditTags', arguments: '0,1,5' },
			        { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'PendingTags.DeleteTags', arguments: '0' }
			    ],
                    rp: perpage,
                    nomsg: "No Records Found!",
                    param: { tag: tags, storeId: storeId, portalId: portalId, userName: userName },
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 9: { sorter: false} }
                });
            },

            EditTags: function(tblID, argus) {
                switch (tblID) {
                    case "gdvTags":
                        $("#" + lblEditTagDetails).html("Edit Tag: '" + argus[4] + "'");
                        PendingTags.HideAll();
                        PendingTags.clearTagForm();
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
            ShowTaggedItems: function(tblID, argus) {
                switch (tblID) {
                    case "gdvTags":
                        $("#" + lblTagViewHeading).html("View Pending Tag: '" + argus[3] + "'");
                        PendingTags.BindTagedItemsDetails(argus[9]);
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
            DeleteTags: function(tblID, argus) {
                switch (tblID) {
                    case "gdvTags":
                        var properties = { onComplete: function(e) {
                            PendingTags.ConfirmDeleteTag(argus[3], e);
                        }
                        }
                        csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this tag?</p>", properties);
                        break;
                    default:
                        break;
                }
            },

            ConfirmDeleteTag: function(itemTagIDs, event) {
                if (event) {
                    this.config.url = this.config.baseURL + "DeleteTag";
                    this.config.data = JSON2.stringify({ itemTagIDs: itemTagIDs, storeID: storeId, portalID: portalId });
                    this.config.ajaxCallMode = 4;
                    this.ajaxCall(this.config)
                }
                return false;
            },

            pageselectCallback: function(page_index, jq) {
                // Get number of elements per pagionation page from form
                var items_per_page = $('#ddlTagItemDisplay').val();


                var max_elem = Math.min((page_index + 1) * items_per_page, arrTagItems.length);
                var newcontent = '';
                arrTagItemsToBind.length = 0;

                // Iterate through a selection of the content and build an HTML string
                for (var i = page_index * items_per_page; i < max_elem; i++) {
                    //newcontent += '<dt>' + arrItemListType[i]._Name + '</dt>';
                    arrTagItemsToBind.push(arrTagItems[i]);
                }
                PendingTags.BindResults();


                // Replace old content with new content
                //$('#Searchresult').html(newcontent);

                // Prevent click event propagation
                return false;
            },

            BindResults: function() {

                $("#divShowTagItemsResult").html('');
                $("#divShowTagItemsResult").html('<table><tbody><tr></tr></tbody></table>');
                $.each(arrTagItemsToBind, function(index, value) {
                    if (value.ImagePath == "") {
                        value.ImagePath = '<%=NoImagePendingTagsPath %>';
                    }
                    if (value.AlternateText == "") {
                        value.AlternateText = value.Name;
                    }
                    var tagItems = '';
                    var isAppend = false;
                    var isNewRow = false;
                    var istrue = (index + 1) % 6;
                    if (istrue != 0) {
                        isAppend = true;
                        tagItems += '<td>';
                        tagItems += ' <div class="cssClassGrid3Box">';
                        tagItems += '<div class="cssClassGrid3BoxInfo">';
                        tagItems += '<h2><a href="' + aspxRedirectPath + 'item/' + value.SKU + '.aspx" target="blank">' + value.Name + '</a></h2>';
                        tagItems += '<div class="cssClassGrid3Picture"><img height="81" width="123" src="' + aspxRootPath + value.ImagePath + '" alt="' + value.AlternateText + '" title="' + value.Name + '" /></div>';
                        tagItems += '<div class="cssClassGrid3PriceBox">';
                        tagItems += '<div class="cssClassGrid3PriceBox"><div class="cssClassGrid3Price">';
                        tagItems += ' <p class="cssClassGrid3OffPrice">Price :<span class="cssClassGrid3RealPrice"> <span>' + value.Price + '</span></span> </p>';
                        tagItems += '<div class="cssClassclear"></div></div></div></div>';
                        tagItems += '</td>';
                    }
                    else {
                        isNewRow = true;
                        tagItems += '<tr>';
                        tagItems += '<td>';
                        tagItems += ' <div class="cssClassGrid3Box">';
                        tagItems += '<div class="cssClassGrid3BoxInfo">';
                        tagItems += '<h2><a href="' + aspxRedirectPath + 'item/' + value.SKU + '.aspx">' + value.Name + '</a></h2>';
                        tagItems += '<div class="cssClassGrid3Picture"><img height="81" width="123" src="' + aspxRootPath + value.ImagePath + '" alt="' + value.AlternateText + '" title="' + value.Name + '" /></div>';
                        tagItems += '<div class="cssClassGrid3PriceBox">';
                        tagItems += '<div class="cssClassGrid3PriceBox"><div class="cssClassGrid3Price">';
                        tagItems += ' <p class="cssClassGrid3OffPrice">Price :<span class="cssClassGrid3RealPrice"> <span>' + value.Price + '</span></span> </p>';
                        tagItems += '<div class="cssClassclear"></div></div></div></div>';
                        tagItems += '</td>';
                        tagItems += '</tr>';
                    }
                    if (isAppend) {
                        $("#divShowTagItemsResult").find('table>tbody tr:last').append(tagItems);
                    }
                    if (isNewRow) {
                        $("#divShowTagItemsResult").find('table>tbody').append(tagItems);
                    }
                });
            },

            SearchTags: function() {
                var tags = $.trim($("#txtSearchTag").val());
                if (tags.length < 1) {
                    tags = null;
                }
                PendingTags.BindTagDetails(tags);
            },
            ajaxSuccess: function(data) {
                switch (PendingTags.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        PendingTags.BindTagDetails(null);
                        PendingTags.HideAll();
                        $("#divShowTagDetails").show();
                        break;
                    case 2:
                        csscody.info('<h2>Successful Message</h2><p>Tags has been updated successfully.</p>');
                        PendingTags.BindTagDetails(null);
                        PendingTags.HideAll();
                        $("#divShowTagDetails").show();
                        break;
                    case 3:
                        $.each(data.d, function(index, item) {
                            $("#selectStatus").append("<option value=" + item.StatusID + ">" + item.Status + "</option>");
                            $("#ddlStatus").append("<option value=" + item.StatusID + ">" + item.Status + "</option>");
                        });
                        break;
                    case 4:
                        PendingTags.BindTagDetails(null);
                        csscody.info('<h2>Successful Message</h2><p>Tags has been deleted successfully.</p>');
                        break;
                    case 5:
                        var tableElements = "";
                        PendingTags.HideAll();
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
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                        $("#divTagedItemsDetails").find("table>tbody tr:even").addClass("cssClassAlternativeEven");
                        $("#divTagedItemsDetails").find("table>tbody tr:odd").addClass("cssClassAlternativeOdd");
                        break;
                }
            }
        }
        PendingTags.init();
    });