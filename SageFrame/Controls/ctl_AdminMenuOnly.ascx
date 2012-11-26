<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_AdminMenuOnly.ascx.cs"
    Inherits="SageFrame.Controls.ctl_AdminMenuOnly" %>
<script type="text/javascript">
  
 $(function(){
  $(this).SageAdminMenuBuilder({                                                
                                    baseURL:SageMenuWCFPath+'/Modules/SageMenu/MenuWebService.asmx/',                                  
                                    UserModuleID:'<%=UserModuleID%>',
                                    PortalID:'<%=PortalID%>',
                                    CultureCode:'<%=CultureCode%>',
                                    UserName:'<%=UserName%>',
                                    UserMode:'<%=Mode%>',
                                    PagePath:pagePathAdminMenu,
                                    PortalSEOName:'<%=PortalName%>'                                                                        
                  });                  
   
    });
    
    </script>
<div class="cssClassAdminMenu">
    <div id="mnuAdminSageFrame" class="AspNet-Menu-Horizontal">
       
    </div>
</div>
