/*
AspxCommerce® - http://www.AspxCommerce.com
Copyright (c) 20011-2012 by AspxCommerce
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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.SageMenu;
using System.IO;
using SageFrame.Common;
using SageFrame.Common.Shared;

public partial class Modules_AspxCommerce_AspxAdminMenu_AdminMenu : BaseAdministrationUserControl
{
    public int UserModuleID, PortalID;
    public string ContainerClientID = string.Empty;
    public string UserName = string.Empty, PageName = string.Empty, CultureCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Initialize();
            //if (!IsPostBack)
            //{
               IncludeCss("AdminMenu", "/Modules/AspxCommerce/AspxAdminMenu/css/superfish.css");
               IncludeJs("AdminMenu", "/Modules/AspxCommerce/AspxAdminMenu/js/hoverIntent.js", "/Modules/AspxCommerce/AspxAdminMenu/js/superfish.js", "/Modules/AspxCommerce/AspxAdminMenu/js/SageAdminMenu.js");
            //}
            UserModuleID = int.Parse(SageUserModuleID);
            PortalID = GetPortalID;
            UserName = GetUsername;
            CultureCode = GetCurrentCulture();
            CreateDynamicNav();

            PageName = Path.GetFileNameWithoutExtension(PagePath);
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuGlobal", " var Path='" + ResolveUrl(modulePath) + "';", true);
            string pagePath = Request.ApplicationPath != "/" ? Request.ApplicationPath : "";
            pagePath = GetPortalID == 1 ? pagePath : pagePath + "/portal/" + GetPortalSEOName;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuGlobal1", " var PagePath='" + pagePath + "';", true);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    public void CreateDynamicNav()
    {
        ContainerClientID = "divNav_" + SageUserModuleID;
        ltrNav.Text = "<div id='" + ContainerClientID + "'></div>";
    }

    public void Initialize()
    {
        string appPath = Request.ApplicationPath != "/" ? Request.ApplicationPath : "";     
    }
}
