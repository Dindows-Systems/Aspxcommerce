<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShoppingSteps.ascx.cs"
    Inherits="Modules_AspxShoppingSteps_ShoppingSteps" %>
<script type="text/javascript">
    //<![CDATA[

    $(function() {
        var ShoppingStep = {
            LoadShoppingStepsImage: function() {
                $('#imgAdvertise').attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/original.png');
                $("#imgDelivery").attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/quality.png');
                $("#imgFestiveldeals").attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/bestprice.png');
                $("#imgStoreLocator").attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/secure.png');
            }
            , Init: function() {
                ShoppingStep.LoadShoppingStepsImage();
//                if (AspxCommerce.utils.IsUserFriendlyUrl()) {
//                    $("#lnkStoreLocator").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + 'Store-Locator-Front.aspx');
//                }
//                else {
//                    $("#lnkStoreLocator").attr("href", '' + AspxCommerce.utils.GetAspxRedirectPath() + 'Store-Locator-Front');
//                }
            }
        }
        ShoppingStep.Init();
    });

    //]]>
</script>

<div class="cssClassBottomContent">
    <div class="cssClassBottomWrapperBox">
        <a href="#">
            <img id="imgAdvertise" src=""  alt="All Brands Original No Duplicates" title="All Brands Original No Duplicates"/></a></div>
    <div class="cssClassBottomWrapperBox">
        <a href="#">
            <img id="imgDelivery" src="" alt="100% Original Guarantee on Products" title="100% Original Guarantee on Products"/></a></div>
    <div class="cssClassBottomWrapperBox">
        <a href="#">
            <img id="imgFestiveldeals" src="" alt="Lowest Prices on all Products" title="Lowest Prices on all Products"/></a></div>
    <div class="cssClassBottomWrapperBox cssClassRemoveMargin">
        <a href="#" id="lnkStoreLocator">
            <img id="imgStoreLocator" src=""  alt="100% Safe and Secure Shopping" title="100% Safe and Secure Shopping"/></a></div>
    <div class="cssClassclear">
    </div>
</div>
