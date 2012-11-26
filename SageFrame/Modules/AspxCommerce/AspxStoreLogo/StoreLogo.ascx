<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreLogo.ascx.cs" Inherits="Modules_AspxCommerce_AspxStoreLogo_StoreLogo" %>

<script type="text/javascript">
    //<![CDATA[
    var storeLogoImg = '<%=StoreLogoImg%>';
    $(function() {
        $('#storeLogo').attr('src', aspxRootPath + storeLogoImg);
    });
    //]]>
</script>

<div class="cssClassLogo">
    <a href="#">
        <img src="" id="storeLogo" alt="AspxCommerce" title="AspxCommerce" /></a>
</div>
