<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_AdminBreadCrum.ascx.cs"
    Inherits="SageFrame.Controls.ctl_AdminBreadCrum" %>

<script type="text/javascript">
  $(function(){  
   $(this).BreadCrumbBuilder({
                    baseURL:BreadCrumPagePath+'/Modules/BreadCrumb/BreadCrumbWebService.asmx/',
                    PagePath:BreadCrumPageLink,
                    PortalID:'<%=PortalID%>',                                    
                    PageName:'<%=PageName%>',
                    Container:'div.cssClassAdminBreadCrum'
        });
    });
</script>
<%--<div class="cssClassAdminBreadCrum">
</div>--%>
