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

public partial class Modules_SageMenu_SageMenuSettings :BaseAdministrationUserControl
{
    public string UserName;
    public int UserModuleID, PortalID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IncludeCss("SageMenuEdit", "/Templates/Default/css/SageMenu/superfish.css");
            IncludeJs("SageMenuEdit", "/Modules/SageMenu/js/hoverIntent.js", "/Modules/SageMenu/js/superfish.js");
            UserModuleID = int.Parse(SageUserModuleID);
            PortalID = GetPortalID;
            UserName = GetUsername;
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuSettingsGlobal", " var SageMenuSettingPath='" + ResolveUrl(modulePath) + "';", true);
            string pagePath = ResolveUrl(Request.ApplicationPath);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SageMenuSettingsGlobal1", " var SageMenuSettingPagePath='" + ResolveUrl(pagePath) + "';", true);
        }
    }
}
