using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.Collections;
using SageFrame.FileManager;


public partial class Modules_FileManager_FileMgr : BaseAdministrationUserControl
{
    public int UserID = 0;   
    public int UserModuleID = 0;
    public string UserName = "";
	public string cssClassWrapper = "";
    public void Initialize()
    {

        Page.ClientScript.RegisterClientScriptInclude("JQueryFileTree", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "/js/jqueryFileTree.js"));
        //Page.ClientScript.RegisterClientScriptInclude("JQueryJson", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "/js/json2.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryPager", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "/js/quickpager.jquery.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryAjaxUpload", ResolveUrl("~/js/AjaxFileUploader/ajaxupload.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryLightBox", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "/js/jquery.lightbox-0.5.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryToolTip", ResolveUrl(this.AppRelativeTemplateSourceDirectory + "/js/jquery.tools.min.js"));
        
        
        IncludeCssFile(AppRelativeTemplateSourceDirectory + "css/popup.css");
        IncludeCssFile(AppRelativeTemplateSourceDirectory + "css/jqueryFileTree.css");
        IncludeCssFile(AppRelativeTemplateSourceDirectory + "css/jquery.lightbox-0.5.css");
        IncludeCssFile(AppRelativeTemplateSourceDirectory + "module.css");


    }
    protected void Page_Init(object sender, EventArgs e)
    {
        Initialize();

        if (ViewState["UserID"] != null)
        {
            UserID = int.Parse(ViewState["UserID"].ToString());
        }
        else
        {
            UserID = FileManagerController.GetUserID(GetUsername);
        }

       

    }
    protected void Page_Load(object sender, EventArgs e)
    {
      if (HttpContext.Current.Request.RawUrl.Contains("/Admin/") || HttpContext.Current.Request.RawUrl.Contains("/Super-User/") || HttpContext.Current.Request.RawUrl.Contains("ManageReturnURL="))
        {
            cssClassWrapper = "cssClassAdminFileManager";
        }
        else
        {
            cssClassWrapper = "cssClassClientFileManager";
            
        }
        string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "FileManagerGlobalVariable1", " var FileManagerPath='" + ResolveUrl(modulePath) + "';", true);
        UserModuleID =int.Parse(SageUserModuleID.ToString());
        UserName = GetUsername;
       
       
        


    }

  
}
