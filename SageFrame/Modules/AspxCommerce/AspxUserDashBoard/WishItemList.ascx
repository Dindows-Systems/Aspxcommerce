<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WishItemList.ascx.cs"
    Inherits="WishItemList" %>

<script type="text/javascript" language="javascript">
//<![CDATA[
 var DashWishItem;
    $(function() {
         DashWishItem = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath(),
                method: "",
                url: "",
                ajaxCallMode: 0,
                error: 0
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: DashWishItem.config.type,
                    contentType: DashWishItem.config.contentType,
                    cache: DashWishItem.config.cache,
                    async: DashWishItem.config.async,
                    url: DashWishItem.config.url,
                    data: DashWishItem.config.data,
                    dataType: DashWishItem.config.dataType,
                    success: DashWishItem.ajaxSuccess,
                    error: DashWishItem.ajaxFailure
                });
            },
            ajaxSuccess: function(msg) {
                switch (DashWishItem.config.ajaxCallMode) {
                    case 1:
                        $("#tblWishItemList>tbody").html('');
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                                DashWishItem.BindWishListItems(item, index);
                            });
                            $(".comment").each(function() {
                                if ($(this).val() == "") {
                                    $(this).addClass("lightText").val("enter a comment..");
                                }
                            });

                            $(".comment").bind("focus", function() {
                                if ($(this).val() == "enter a comment..") {
                                    $(this).removeClass("lightText").val("");
                                }
                                // focus lost action
                            });
                            $(".comment").bind("blur", function() {
                                if ($(this).val() == "") {
                                    $(this).val("enter a comment..").addClass("lightText");
                                }
                            });
                        }
                        else {
                            $("#tblWishItemList>thead").hide();
                            $("#wishitemBottom").hide();
                            $("#tblWishItemList").html("<p>Your Wishlist is empty!</p>");
                        }
                        break;
                    case 2:
                        if ($(".cssClassWishListCount").html() != null) {
                            DashWishItem.UpdateHeaderWishlistCount();
                        }
                        DashWishItem.GetWishItemList();
                        alert('Success');
                        break;
                    case 3:
                        alert("success");
                        break;
                    case 4:
                        alert("Successfully cleared your wishlist!");
                        break;

                }
            }, ajaxFailure: function() {

            },
            GetWishItemList: function() {
                this.config.method = "AspxCommerceWebService.asmx/GetWishItemList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName, flagShowAll: isAll, count: count });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);

            }, BindWishListItems: function(response, index) {
                if (response.ImagePath == "") {
                    response.ImagePath = AspxCommerce.utils.GetAspxRootPath() + "Modules/AspxCommerce/AspxItemsManagement/uploads/noitem.png";
                }
                else if (response.AlternateText == "") {
                    response.AlternateText = response.ItemName;
                }
                ItemIDs = response.ItemID + "#";
                ItemComments = $("#comment" + response.ItemID + "").innerText;

                var WishDate = DashWishItem.DateDeserialize(response.WishDate, "yyyy/M/d");
                if (index % 2 == 0) {
                    Items = '<tr class="cssClassAlternativeEven" id="tr_' + response.ItemID + '"><td class="cssClassWishItemDetails"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '"  title="' + response.AlternateText + '"/><a href="' +  AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a><span class="cssClassPrice">' + response.Price + '</span></td><td class="cssClassWishComment"><textarea id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart"> <div class="cssClassButtonWrapper"><a href="' +AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx"><span>Add To Cart</span></a></div></td><td class="cssClassDelete"><img id="imgdelete" onclick="DashWishItem.DeleteWishItem(' + response.ItemID + ')" src="'+ AspxCommerce.utils.GetAspxTemplateFolderPath() + 'asdf/images/admin/btndelete.png" alt="delete" title="Delete Item"/></td></tr>';
                }
                else {
                    Items = '<tr class="cssClassAlternativeOdd" id="tr_' + response.ItemID + '"><td class="cssClassWishItemDetails"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '"  title="' + response.AlternateText + '"/><a href="' +  AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a><span class="cssClassPrice">' + response.Price + '</span></td><td class="cssClassWishComment"><textarea id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart"> <div class="cssClassButtonWrapper"><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx"><span>Add To Cart</span></a></div></td><td class="cssClassDelete"><img id="imgdelete" onclick="DashWishItem.DeleteWishItem(' + response.ItemID + ')" src="'+ AspxCommerce.utils.GetAspxTemplateFolderPath() + 'asdf/images/admin/btndelete.png" alt="delete" title="Delete Item"/></td></tr>';
                }
                $("#tblWishItemList>tbody").append(Items);
            }, DeleteWishItem: function(itemId) {
                var properties = { onComplete: function(e) {
                    DashWishItem.ConfirmSingleDelete(itemId, e);
                }
                }
                // Ask user's confirmation before delete records        
                csscody.confirm("<h2>Delete Confirmation</h2><p>Do you want to delete this item from your wishlist?</p>", properties);
            }, ConfirmSingleDelete: function(id, event) {
                if (event) {
                    this.config.method = "AspxCommerceWebService.asmx/DeleteWishItem";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({ ID: id, storeID: storeId, portalID: portalId, userName: userName });
                    this.config.ajaxCallMode = 2;
                    this.ajaxCall(this.config);

                }
            },
            UpdateHeaderWishlistCount: function() {
                var wishListCount = $(".cssClassWishListCount").html().replace(/[^0-9]/gi, '');
                wishListCount = parseInt(wishListCount) - 1;
                $(".cssClassWishListCount").html("[" + wishListCount + "]");
            },
            UpdateWishList: function() {
                var comment = '';
                var itemId = '';
                $(".comment").each(function() {
                    comment += $(this).val() + ',';
                    itemId += parseInt($(this).attr("id").replace(/[^0-9]/gi, '')) + ',';
                    //UpdateList(itemId, comment);
                });
                comment = comment.substring(0, comment.length - 1);
                itemId = itemId.substring(0, itemId.length - 1);

                this.config.method = "AspxCommerceWebService.asmx/UpdateWishList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ ID: itemId, comment: comment, storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);

            }, ClearWishList: function() {
                this.config.method = "AspxCommerceWebService.asmx/ClearWishList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);

            }, DateDeserialize: function(content, format) {
                content = eval('new ' + content.replace(/[/]/gi, ''));
                  return formatDate(content, format);
            }, Init: function() {
                DashWishItem.GetWishItemList();
           }

        }
        DashWishItem.Init();
    });
   
  
   //]]>
</script>

<div class="cssClassFormWrapper">
    <div class="cssClassCommonCenterBox">
        <h2>
            <asp:Label ID="lblMyWishListTitle" runat="server" Text="My WishList Content" CssClass="cssClassWishItem"></asp:Label></h2>
        <div class="cssClassCommonCenterBoxTable">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblWishItemList"
                class="cssClassMyWishItemTable">
                <thead>
                    <tr class="cssClassCommonCenterBoxTableHeading">
                        <td class="cssClassWishItemDetails">
                            <asp:Label ID="lblItem" runat="server" Text="Item"></asp:Label>
                        </td>
                        <td class="cssClassWishListComment">
                            <asp:Label ID="lblComment" runat="server" Text="Comment"></asp:Label>
                        </td>
                        <td class="cssClassAddedOn">
                            <asp:Label ID="lblAddedOn" runat="server" Text="Added On"></asp:Label>
                        </td>
                        <td class="cssClassAddToCart">
                            <asp:Label ID="lblAddToCart" runat="server" Text="Add To Cart"></asp:Label>
                        </td>
                        <td class="cssClassDelete">
                        </td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="cssClassButtonWrapper" id="wishitemBottom">
                <%--<button type="button">
                    <span><span>Share Wishlist</span></span></button>
                <button type="button">
                    <span><span>Add All to Cart</span></span></button>--%>
                <button type="button" id="updateWishList" onclick="DashWishItem.UpdateWishList();">
                    <span><span>Update WishList</span></span></button>
                <button type="button" id="clearWishList" onclick="DashWishItem.ClearWishList();">
                    <span><span>Clear WishList</span></span></button>
                <%--<button type="button">
                    <span><span>Continue shopping</span></span></button>--%>
            </div>
        </div>
    </div>
</div>
