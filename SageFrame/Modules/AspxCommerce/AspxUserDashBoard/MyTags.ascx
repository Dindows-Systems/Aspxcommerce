<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyTags.ascx.cs" Inherits="Modules_AspxMyTags_MyTags" %>

<script type="text/javascript">

    //<![CDATA[
    var Tags = {
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
                type: Tags.config.type,
                contentType: Tags.config.contentType,
                cache: Tags.config.cache,
                async: Tags.config.async,
                data: Tags.config.data,
                dataType: Tags.config.dataType,
                url: Tags.config.url,
                success: Tags.ajaxSuccess,
                error: Tags.ajaxFailure
            });
        },
        init: function() {
            Tags.GetMyTags();
        },

        GetMyTags: function() {
            this.config.url = this.config.baseURL + "GetTagsByUserName";
            this.config.data = JSON2.stringify({ userName: userName, storeID: storeId, portalID: portalId, cultureName: cultureName });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        DeleteMyTag: function(obj) {
            var properties = { onComplete: function(e) {
                if (e) {
                    var itemTagId = $(obj).attr("value");
                    Tags.config.url = Tags.config.baseURL + "DeleteUserOwnTag";
                    Tags.config.data = JSON2.stringify({ itemTagID: itemTagId, storeID: storeId, portalID: portalId, userName: userName });
                    Tags.config.ajaxCallMode = 2;
                    Tags.ajaxCall(Tags.config);
                }
                else return false;
            }
            }
            csscody.confirm("<h1>Delete Confirmation</h1><p>Do you want to delete this tag?</p>", properties);
        },
        ajaxSuccess: function(data) {
            switch (Tags.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    var MyTags = '';
                    if (data.d.length > 0) {
                        $.each(data.d, function(index, value) {
                            MyTags += '<li class="tag_content"><a href="' + aspxRedirectPath + 'tagsitems/tags.aspx?tagsId=' + value.ItemTagIDs + '"><label>' + value.Tag + '</label></a>';
                            MyTags += "<button type=\"button\" class=\"cssClassCross\" value=" + value.ItemTagIDs + " onclick ='Tags.DeleteMyTag(this)'><span>x</span></button></li>";
                        });
                    }
                    else {
                        MyTags = "<span class=\"cssClassNotFound\">Your tag list is empty!</span>";
                    }
                    $("#divMyTags >ul").html(MyTags);
                    break;
                case 2:
                    csscody.info('<h2>Information Alert</h2><p>Your Tag is deleted successfully!</p>');
                    Tags.GetMyTags();
                    break;
            }
        },
        ajaxFailure: function(data) {
            switch (Tags.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>Error Message</h2><p>Failed to delete!</p>');
                    break;
            }
        }
    }
    $(function(){
        Tags.init();
    });
    //]]>  
</script>

<div class="cssClassFormWrapper">
    <div class="cssClassCommonCenterBox">
        <h2>
            <asp:Label ID="lblMyTagsTitle" runat="server" Text="My Tags Content" CssClass="cssClassTags"></asp:Label></h2>
        <div class="cssClassCommonCenterBoxTable">
            <div id="divMyTags">
                <ul>
                </ul>
            </div>
        </div>
    </div>
</div>
