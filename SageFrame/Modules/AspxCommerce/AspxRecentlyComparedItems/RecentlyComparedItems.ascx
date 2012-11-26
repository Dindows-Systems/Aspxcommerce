<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecentlyComparedItems.ascx.cs"
    Inherits="Modules_AspxRecentlyComparedItems_RecentlyComparedItems" %>

<script type="text/javascript" language="javascript">
    //<![CDATA[
    var recentlyCompareCounts = '<%=CountCompare %>';
    var enableRecentlyCompared='<%=EnableRecentlyCompared %>';
    //]]>
</script>

<div id="divRecentComparedItems" class="cssClassCommonSideBox">
    <h2>
        <asp:Label ID="lblRecebtlyComparedTitle" runat="server" Text="Recently Compared Items"
            CssClass="cssClassRecentlyComparedItems"></asp:Label></h2>
    <div class="cssClassCommonSideBoxTable">
        <table id="tblRecentlyComparedItemList" width="100%" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr><td></td></tr>
            </tbody>
        </table>
    </div>
</div>
