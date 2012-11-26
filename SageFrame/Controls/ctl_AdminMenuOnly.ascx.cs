/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2010 by SageFrame
Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Security;
using System.Collections.Generic;
using SageFrame.Security.Entities;
using SageFrame.Common.Shared;

namespace SageFrame.Controls
{
    public partial class ctl_AdminMenuOnly : BaseUserControl
    {
        public int UserModuleID, PortalID;
        public int Mode = 0;
        public string CultureCode = string.Empty,UserName=string.Empty,PortalName=string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                IncludeCss("AdminMenuOnly","/Modules/SageMenu/css/superfish.css", "/Modules/SageMenu/css/FooterMenu.css", "/Modules/SageMenu/css/SideMenu.css");
                IncludeJs("AdminMenuOnly", "/js/Menu/AdminMenu.js", "/Modules/SageMenu/js/hoverIntent.js", "/Modules/SageMenu/js/superfish.js");
            //}
            Initialize();           
            IsSuperUser();
            PortalID = GetPortalID;
            UserName=GetUsername;
            CultureCode = GetCurrentCulture();
            PortalName = GetPortalSEOName;
                
            
        }
        public void Initialize()
        {
            string appPath = Request.ApplicationPath != "/" ? Request.ApplicationPath : "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuAdminGlobal1", " var SageMenuWCFPath='" + appPath + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuAdminMenu", " var pagePathAdminMenu='" + appPath + "';", true);
        }

        protected void IsSuperUser()
        {
            RoleController _role = new RoleController();
            string[] roles = _role.GetRoleNames(GetUsername, GetPortalID).ToLower().Split(',');
            if(roles.Contains(SystemSetting.SUPER_ROLE[0]))
            {
                Mode=1;
            }            
            
        }
    }
}