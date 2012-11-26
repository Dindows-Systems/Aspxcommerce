<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecentlyViewedItems.ascx.cs"
    Inherits="Modules_AspxRecentlyViewedItems_RecentlyViewedItems" %>

<script type="text/javascript">
    //<![CDATA[
    var recentlyViewedCounts = '<%=CountViewedItems %>';
    var enableRecentlyViewed='<%=EnableRecentlyViewed%>';
       //]]>              
</script>

<div id="divRecentViewedItems" class="cssClassCommonSideBox">
    <h2>
        <asp:Label ID="lblRecentlyViewedItems" Text="Recently Viewed Items" runat="server" CssClass="cssClassRecentlyViewedItems" /></h2>
    <div class="cssClassCommonSideBoxTable">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblRecentlyViewedItems">
            <tbody>
                <tr><td></td></tr>
            </tbody>
        </table>
    </div>
</div>
