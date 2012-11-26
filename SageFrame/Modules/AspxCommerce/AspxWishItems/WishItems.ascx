<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WishItems.ascx.cs" Inherits="Modules_AspxWishItems_WishItems" %>

<script type="text/javascript" language="javascript">
    //<![CDATA[
    var countryNameWishList = '<%=CountryName %>';
   
    var wishListNoImagePath = '<%=NoImageWishItemPath %>';
    var allowWishItemList = '<%=AllowWishItemList %>';
    var showWishItemImage = '<%=ShowWishedItemImage %>';
    var wishListURLSetting = '<%=WishListURL%>';
    var noOfRecentAddedWishItems = '<%=NoOfRecentAddedWishItems%>';
    //]]>
</script>


<div class="cssClassCommonSideBox" id="divRecentlyAddedWishList">
    <h2>
        <asp:Label ID="lblRecentAddedWishItemsTitle" runat="server" Text="Recently Added Wish Items"
            CssClass="cssClassWishItem"></asp:Label></h2>
    <div class="cssClassCommonSideBoxTable">
        <table cellspacing="0" cellpadding="0" border="0" class="cssClassMyWishItemTable" id="tblWishItem" width="100%">                
              <thead><tr><td></td></tr></thead>
               <tfoot>
                 <tr><td></td></tr>
            </tfoot>
            <tbody>
                <tr><td></td></tr>
            </tbody>
        </table>
    </div>
</div>
