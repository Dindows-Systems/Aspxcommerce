<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SageMenu.ascx.cs" Inherits="Modules_SageMenu_SageMenu" %>
 <script type="text/javascript">
     //<![CDATA[
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
    //]]>
    </script>
<asp:Literal ID="ltrNav" runat="server" EnableViewState="false"></asp:Literal>
   
   
     
   
    
 
