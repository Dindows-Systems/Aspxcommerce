<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopSearchTerms.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxAdminDashBoard_TopSearchTerms" %>

<script type="text/javascript">
    //<![CDATA[

    //]]>
</script>



<div class="cssClassTabPanelTable">
    <div id="container-8">
        <ul>
            <li><a href="#fragment-1">
                <asp:Label ID="lblTabTopSearch" runat="server" Text="Top Search "></asp:Label>
            </a></li>
            <li><a href="#fragment-2">
                <asp:Label ID="lblTabLatestSearch" runat="server" Text="Latest Search"></asp:Label>
            </a></li>
        </ul>
        <div id="fragment-1">
            <div class="cssClassFormWrapper">
                <div id="divTopSearchTerms">
                </div>
            </div>
        </div>
        <div id="fragment-2">
            <div class="cssClassFormWrapper">
                <div id="divLatestSearchTerms">
                </div>
            </div>
        </div>
    </div>
</div>
