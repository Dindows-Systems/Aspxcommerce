<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DiscountBar.ascx.cs" Inherits="Modules_AspxDiscountBanner_DiscountBar" %>
<script type="text/javascript">
    //<![CDATA[
    $(document).ready(function() {
        LoadDiscountBarImage();
    });
//    $(window).load(function() {
//        LoadDiscountBarImage();
//    });
    function LoadDiscountBarImage() {
        $('#imgDiscountImage1').attr('src', '' + aspxTemplateFolderPath + '/images/discount.png');
    }
    //]]>
</script>

<div class="cssClassDiscount">
    <div class="cssClassDiscountBox">
        <a href="http://aspxcommerce.codeplex.com/releases/view/78747">
            <img id="imgDiscountImage1" src="" alt="AspxCommerce Discount" /></a></div>
    <div class="cssClassclear">
    </div>
</div>
