<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenu.ascx.cs" Inherits="Modules_AspxCommerce_AspxAdminMenu_AdminMenu" %>

 <script type="text/javascript">
    $(function(){    
       $(this).SageMenuBuilder({
                        PortalID:'<%=PortalID%>',
                        UserModuleID:'<%=UserModuleID%>',                        
                        UserName:'<%=UserName%>',
                        PageName:'<%=PageName%>',
                        ContainerClientID:'#'+'<%=ContainerClientID%>',
                        CultureCode:'<%=CultureCode%>',
                        baseURL:Path+'MenuWebService.asmx/'         
       });  
    });
    </script>
 <div class="cssClassNavigationWrapper">
<asp:Literal ID="ltrNav" runat="server" EnableViewState="false"></asp:Literal>
</div>

