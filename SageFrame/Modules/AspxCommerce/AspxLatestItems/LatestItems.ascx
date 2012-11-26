<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LatestItems.ascx.cs" Inherits="Modules_AspxLatestItems_LatestItems" %>

<script type="text/javascript">

    var countryName = '<%=CountryName %>';
    var defaultImagePath = '<%=DefaultImagePath %>';
    var noOfLatestItems = '<%=NoOfLatestItems %>';
    var noOfLatestItemsInARow = '<%=NoOfLatestItemsInARow%>';
    var allowWishListLatestItem = '<%=AllowWishListLatestItem %>';
    var allowAddToCompareLatest = '<%=AllowAddToCompareLatest %>';
    var allowOutStockPurchase = '<%=AllowOutStockPurchase %>';
    var maxCompareItemCount = '<%=MaxCompareItemCount%>';
    var enableLatestItems = '<%=EnableLatestItems %>';
    
</script>

<div id="divLatestItems" class="cssClassProducts">
    <h1>
        <asp:Label ID="lblRecentItemsHeading" runat="server" Text="Recently Added Items"
            CssClass="cssClassLabel"></asp:Label></h1>
    <div id="tblRecentItems">
    </div>
    <div class="cssClassclear">
    </div>
</div>
