<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BestSellers.ascx.cs" Inherits="Modules_AspxBestSellers_BestSellers" %>

<script type="text/javascript">
    //<![CDATA[
    var enableBestSellerItems = '<%=EnableBestSellerItems %>';
    var countBestSellerSetting = '<%=CountBestSeller %>';
    var defaultImagePath = '<%=DefaultImagePath %>';
    //]]>
</script>

<div id="divBestSellers" class="cssClassBestSeller cssClassBestSellerMargin">
    <h2>
        Best Seller Items</h2>
    <div class="cssClassBestSellerBox">
        <div class="cssClassBestSellerBoxInfo">
            <ul>
                <li></li>
            </ul>
        </div>
        <div class="cssClassclear">
        </div>
    </div>
</div>
