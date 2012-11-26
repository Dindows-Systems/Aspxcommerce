<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BreadCrumb.ascx.cs"
    Inherits="Modules_BreadCrumb_BreadCrumb" %>

<script type="text/javascript">
  $(function(){  
   $(this).BreadCrumbBuilder({
                    baseURL:BreadCrumPagePath+'/Modules/BreadCrumb/BreadCrumbWebService.asmx/',
                    PagePath:BreadCrumPageLink,
                    PortalID:'<%=PortalID%>',                                    
                    PageName:'<%=PageName%>',
                    Container:"div.cssClassBreadCrumb"
        });
    });
</script>
<div class="cssClassBreadCrumbWrapper">
<div class="cssClassBreadCrumb">
</div>
</div>