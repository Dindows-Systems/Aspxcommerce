<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LatestFiveOrderItems.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxAdminDashBoard_LatestFiveOrderItems" %>

<script type="text/javascript">
    //<![CDATA[

    //]]>
</script>

<div id="divLatestOrderStaticsByCustomer" class="cssClssRoundedBoxTable">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblInventoryDetail" CssClass="cssClassLabel" runat="server" Text="List of Latest Orders"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <div id="divLatestOrderStatics">
            </div>
        </div>
    </div>
</div>
