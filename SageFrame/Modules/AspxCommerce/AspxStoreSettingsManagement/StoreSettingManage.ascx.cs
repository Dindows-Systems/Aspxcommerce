/*
AspxCommerce® - http://www.aspxcommerce.com
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
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Core.Pages;
using System.Collections.Specialized;
using SageFrame.Framework;
using AspxCommerce.Core;

public partial class Modules_AspxStoreSettings_StoreSettingManage : BaseAdministrationUserControl
{
    public int StoreID, PortalID,MaxFileSize;
    public string UserName, CultureName;
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalRootPath", " var aspxRootPath='" + ResolveUrl("~/") + "';", true);                    
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalServicePath", " var aspxservicePath='" + ResolveUrl("~/") + "Modules/AspxCommerce/AspxCommerceServices/" + "';", true);
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalVariables", " var aspxStoreSetModulePath='" + ResolveUrl(modulePath) + "';", true);
            InitializeJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("StoreSettingManage", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/JQueryUI/jquery.ui.all.css");
                IncludeJs("StoreSettingManage", "/js/FormValidation/jquery.validate.js", "/js/JQueryUI/jquery-ui-1.8.10.custom.js", "/js/AjaxFileUploader/ajaxupload.js",
                           "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js",
                            "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js",
                            "Modules/AspxCommerce/AspxStoreSettingsManagement/js/StoreSetting.js");

                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
				BindPageDropDown();
            	BindTimeZoneDropDown();

                StoreSettingConfig ssc = new StoreSettingConfig();
                MaxFileSize = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MaximumImageSize, StoreID, PortalID, CultureName));
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void BindPageDropDown()
    {
        try
        {
            PagesDataContext db = new PagesDataContext(SystemSetting.SageFrameConnectionString);
            var LINQParentPages = db.sp_PagePortalGetByCustomPrefix("---", true, false, GetPortalID, GetUsername, null, null);

            ddlMyAccountURL.Items.Clear();
            ddlMyAccountURL.DataSource = LINQParentPages;
            ddlMyAccountURL.DataTextField = "LevelPageName";
            ddlMyAccountURL.DataValueField = "SEOName";
            ddlMyAccountURL.DataBind();
            ddlMyAccountURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

            ddlShoppingCartURL.Items.Clear();
            ddlShoppingCartURL.DataSource = LINQParentPages;
            ddlShoppingCartURL.DataTextField = "LevelPageName";
            ddlShoppingCartURL.DataValueField = "SEOName";
            ddlShoppingCartURL.DataBind();
            ddlShoppingCartURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

            ddlWishListURL.Items.Clear();
            ddlWishListURL.DataSource = LINQParentPages;
            ddlWishListURL.DataTextField = "LevelPageName";
            ddlWishListURL.DataValueField = "SEOName";
            ddlWishListURL.DataBind();
            ddlWishListURL.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

            //ddlStoreCloseUrl.Items.Clear();
            //ddlStoreCloseUrl.DataSource = LINQParentPages;
            //ddlStoreCloseUrl.DataTextField = "LevelPageName";
            //ddlStoreCloseUrl.DataValueField = "SEOName";
            //ddlStoreCloseUrl.DataBind();
            //ddlStoreCloseUrl.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

            //ddlStoreNotAccessedUrl.Items.Clear();
            //ddlStoreNotAccessedUrl.DataSource = LINQParentPages;
            //ddlStoreNotAccessedUrl.DataTextField = "LevelPageName";
            //ddlStoreNotAccessedUrl.DataValueField = "SEOName";
            //ddlStoreNotAccessedUrl.DataBind();
            //ddlStoreNotAccessedUrl.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

            lstSSLSecurePages.Items.Clear();
            lstSSLSecurePages.DataSource = LINQParentPages;
            lstSSLSecurePages.DataTextField = "LevelPageName";
            lstSSLSecurePages.DataValueField = "SEOName";
            lstSSLSecurePages.DataBind();
            lstSSLSecurePages.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void BindTimeZoneDropDown()
    {
        //Setting Data Source
        NameValueCollection nvlTimeZone = SageFrame.Localization.Localization.GetTimeZones(((PageBase)this.Page).GetCurrentCultureName);
        ddlDftStoreTimeZone.DataSource = nvlTimeZone;
        ddlDftStoreTimeZone.DataBind();
        if (ddlDftStoreTimeZone.Items.Count > 0)
        {
            ddlDftStoreTimeZone.SelectedIndex = 0;
        }

    }
}
