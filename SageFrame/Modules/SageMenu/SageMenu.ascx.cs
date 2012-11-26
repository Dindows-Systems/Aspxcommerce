using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SageFrame.Web;
using SageFrame.SageMenu;
using System.IO;
using SageFrame.Common.Shared;


public partial class Modules_SageMenu_SageMenu : BaseAdministrationUserControl
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
                IncludeCss("SageMenu", "/Templates/"+TemplateName+"/css/SageMenu/superfish.css");
                IncludeJs("SageMenu", "/Modules/SageMenu/js/hoverIntent.js", "/Modules/SageMenu/js/superfish.js", "/Modules/SageMenu/js/SageMenu.js");
                CreateDynamicNav();
                UserModuleID = int.Parse(SageUserModuleID);
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureCode = GetCurrentCulture();
                PageName = Path.GetFileNameWithoutExtension(PagePath);
                string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuGlobal", " var Path='" + ResolveUrl(modulePath) + "';", true);
                string pagePath = Request.ApplicationPath != "/" ? Request.ApplicationPath : "";
                pagePath = GetPortalID == 1 ? pagePath : pagePath + "/portal/" + GetPortalSEOName;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuGlobal1", " var PagePath='" + pagePath + "';", true);
            //}
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
        try
        {
            string appPath = Request.ApplicationPath != "/" ? Request.ApplicationPath : "";        
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
