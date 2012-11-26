<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminDashBoard.ascx.cs" Inherits="Modules_AspxCommerce_AspxAdminDashBoard_AdminDashBoard" %>

<%@ Register src="OrderOverViews.ascx" tagname="OrderOverViews" tagprefix="uc1" %>
<%@ Register src="TotalStoreRevenue.ascx" tagname="TotalStoreRevenue" tagprefix="uc2" %>
<%@ Register src="RecentReviewsAndRatings.ascx" tagname="RecentReviewsAndRatings" tagprefix="uc3" %>
<%@ Register src="LatestFiveOrderItems.ascx" tagname="LatestFiveOrderItems" tagprefix="uc4" %>
<%@ Register src="MostViewedItems.ascx" tagname="MostViewedItems" tagprefix="uc5" %>
<%@ Register src="StoreStaticsDisplay.ascx" tagname="StoreStaticsDisplay" tagprefix="uc6" %>
<%@ Register src="TopCutomersByOrder.ascx" tagname="TopCutomersByOrder" tagprefix="uc7" %>
<%@ Register src="TopSearchTerms.ascx" tagname="TopSearchTerms" tagprefix="uc8" %>
<%--<%@ Register src="LatestSearchTerms.ascx" tagname="LatestSearchTerms" tagprefix="uc9" %>--%>

<%@ Register src="InventoryDetails.ascx" tagname="InventoryDetails" tagprefix="uc10" %>

<div class="cssClassDashBoardLeft">

<!--Order OverView-->
<uc1:OrderOverViews ID="OrderOverViews1" runat="server" />
<!--Revenue-->
<uc2:TotalStoreRevenue ID="TotalStoreRevenue1" runat="server" />

<!--Chart-->
<uc6:StoreStaticsDisplay ID="StoreStaticsDisplay1" runat="server" />

</div>

<div class="cssClassDashBoardRight">
<!--Inventory Details-->
<uc10:InventoryDetails ID="InventoryDetails1" runat="server" />
<!--Tab Panels-->
<uc4:LatestFiveOrderItems ID="LatestFiveOrderItems1" runat="server" />
<uc7:TopCutomersByOrder ID="TopCutomersByOrder1" runat="server" />
<uc5:MostViewedItems ID="MostViewedItems1" runat="server" />

<!--Side Boxes-->
<uc8:TopSearchTerms ID="TopSearchTerms1" runat="server" />
<%--<uc9:LatestSearchTerms ID="LatestSearchTerms1" runat="server" />--%>
</div>
<div class="cssClassDashBoardBottom">
    <!--Recent Review-->
    <uc3:RecentReviewsAndRatings ID="RecentReviewsAndRatings1" runat="server" />
</div>



