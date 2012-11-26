<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryBreadCrumb.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxCategoryLister_CategoryBreadCrumb" %>

<div id="breadcrumb" class="breadCrumb">
    <ul>
        <li style="display: none;"></li>      
    </ul>
</div>
<script type="text/javascript">
    window.onload = function() {
        if ($('#breadcrumb').length > 0) {
            if (AspxCommerce.utils.GetAspxRootPath() == "/") {
                Breadcrum.getBreadcrumforlive();
            } else {
                Breadcrum.getBreadcrum();
            }
        }
    };
</script>