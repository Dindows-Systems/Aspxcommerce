<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FrontStoreWelcome.ascx.cs"
    Inherits="Modules_AspxFrontStoreWelcome_FrontStoreWelcome" %>

<script type="text/javascript">
        //<![CDATA[
        $(document).ready(function() {
            LoadShoppingStepsImage();
        });
    function LoadShoppingStepsImage() {
        $('#imgMoreImage').attr('src', '' + aspxTemplateFolderPath + '/images/more.png');
    }
    //]]>
</script>

<div class="cssClassWelcome">
    <div class="cssClassWelcomePicture">
    </div>
    <div class="cssClassclear">
    </div>
</div>
