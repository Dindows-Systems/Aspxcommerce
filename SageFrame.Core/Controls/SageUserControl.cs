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
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using SageFrame.ErrorLog;
using SageFrame.Web;
using SageFrame.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using SageFrame.Web.Common.SEO;
using SageFrame.Web.Utilities;
using System.Collections;
using SageFrame.Common;
using System.IO;
//using AjaxControlToolkit;

namespace SageFrame.Web
{
    public class SageUserControl : System.Web.UI.UserControl
    {
        int PortalID = 1;
		int StoreID = 1;
        int CustomerID = 0;
        string PortalSEOName = string.Empty;
        public string SageUserModuleID
        {
            get
            {
                if (ViewState["__SageUserModuleID"] != null)
                {
                    return ViewState["__SageUserModuleID"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["__SageUserModuleID"] = value;
            }
        }
        private static string _PagePath = string.Empty;
        public string PagePath
        {
            get { return _PagePath; }
            set { _PagePath = value; }
        }
        public SageUserControl()
        {
        }
        protected void SetCulture(string name, string locale)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(name);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);
            Session["SageUICulture"] = Thread.CurrentThread.CurrentUICulture;
            Session["SageCulture"] = Thread.CurrentThread.CurrentCulture;
        }
        protected string GetCurrentUICulture()
        {
            return Thread.CurrentThread.CurrentUICulture.ToString();
        }
        protected string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.ToString();
        }
        public string GetSageMessage(string ModuleName, string MessageNode)
        {
            try
            {
                return SageMessage.ProcessSageMessage(CultureInfo.CurrentCulture.Name, ModuleName, MessageNode);
            }
            catch (Exception)
            {                
                return string.Empty;
            }
        }

        protected void ShowMessage(string MessageTitle, string Message, string CompleteMessage, SageMessageType MessageType)
        {
            ScriptManager scp = (ScriptManager)this.Page.FindControl("ScriptManager1");
            if (scp != null)
            {
                bool isSageAsyncPostBack = false;
                if (scp.IsInAsyncPostBack)
                {
                    isSageAsyncPostBack = true;
                }

                if (this.Page == null)
                    return;

                Page SagePage = this.Page;
                if (SagePage == null)
                    return;

                PageBase mSagePage = SagePage as PageBase;
                if (mSagePage != null)
                    mSagePage.ShowMessage(MessageTitle, Message, CompleteMessage, isSageAsyncPostBack, MessageType);
            }
        }

        public bool IsControlPostBack
        {
            get
            {
                bool retValue = false;
                if (ViewState["IsControlPostBack"] != null)
                {
                    retValue = true;
                }
                return retValue;
            }
            set { ViewState["IsControlPostBack"] = value; }
        }

        public string GetTemplateImageUrl(string imageName, bool isServerControl)
        {
            string path = string.Empty;
            if (isServerControl == true)
            {
                path = "~/Templates/" + TemplateName + "/images/admin/" + imageName;
            }
            else
            {
                path = this.Page.ResolveUrl("~/" + "Templates/" + TemplateName + "/images/admin/" + imageName);
            }
            return path;
        }

        public Int32 GetPortalID
        {
            get
            {
                if (HttpContext.Current.Session["SageFrame.PortalID"] != null && HttpContext.Current.Session["SageFrame.PortalID"].ToString() != "")
                {
                    PortalID = Int32.Parse(HttpContext.Current.Session["SageFrame.PortalID"].ToString());
                }
                return PortalID;
            }
        }

        public void SetPortalID(int portalID)
        {
            PortalID = portalID;
        }
		public Int32 GetStoreID
        {
            get
            {
                if (HttpContext.Current.Session["SageFrame.StoreID"] != null && HttpContext.Current.Session["SageFrame.StoreID"].ToString() != "")
                {
                    StoreID = Int32.Parse(HttpContext.Current.Session["SageFrame.StoreID"].ToString());
                }
                return StoreID;
            }
        }

        public void SetStoreID(int storeID)
        {
            StoreID = storeID;
        }

        public Int32 GetCustomerID
        {
            get
            {
                if (HttpContext.Current.Session["SageFrame.CustomerID"] != null && HttpContext.Current.Session["SageFrame.CustomerID"].ToString() != "")
                {
                    CustomerID = Int32.Parse(HttpContext.Current.Session["SageFrame.CustomerID"].ToString());
                }
                return CustomerID;
            }
        }

        public void SetCustomerID(int customerID)
        {
            CustomerID = customerID;
        }

        public string GetUsername
        {
            get
            {
                try
                {
                    MembershipUser user = Membership.GetUser();
                    if (user != null)
                    {
                        return user.UserName;
                    }
                    else
                    {
                        return "anonymoususer";
                    }
                }
                catch
                {
                    return "anonymoususer";
                }
            }
        }

        public string TemplateName
        {
            get
            {
                SageFrameConfig pb = new SageFrameConfig();
                return pb.GetSettingsByKey(SageFrameSettingKeys.PortalCssTemplate);
            }
        }

        public void ProcessCancelRequestBase(string RedirectUrl)
        {           
            string strURL = string.Empty;
            SageFrameConfig pagebase = new SageFrameConfig();
            bool IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            if (!IsUseFriendlyUrls)
            {
                string[] arrUrl;
                arrUrl = Request.RawUrl.Split('&');
                if (arrUrl.Length > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        strURL += arrUrl[i] + "&";                        
                    }
                   strURL = strURL.Remove(strURL.LastIndexOf('&'));
                }
            }
            else
            {
                if (RedirectUrl.Contains("?"))
                {
                    string[] d = RedirectUrl.Split('?');
                    strURL = d[0];                    
                }
            }
            HttpContext.Current.Response.Redirect(strURL, false);
        }

        protected string GetPortalSEOName
        {
            get
            {
                if (HttpContext.Current.Session["SageFrame.PortalSEOName"] != null && HttpContext.Current.Session["SageFrame.PortalSEOName"].ToString() != "")
                {
                    PortalSEOName = HttpContext.Current.Session["SageFrame.PortalSEOName"].ToString();
                }
                return PortalSEOName;
            }
        }

        public void SetPortalSEOName(string portalSEOName)
        {
            PortalSEOName = portalSEOName;
        }

        public void ProcessSourceControlUrlBase(string rawUrl, string controlPath, string parameter)
        {
            //Added For unique Control ID generation
            //int controlUniqueIDPrefix = GetRandomNumber(System.Int32.Parse(DateTime.Now.ToString()), System.Int32.MaxValue);

            string strURL = string.Empty;
            SageFrameConfig pagebase = new SageFrameConfig();
            bool IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            if (!IsUseFriendlyUrls)
            {
                if (rawUrl.Contains(parameter))
                {
                    rawUrl = rawUrl.Remove(rawUrl.IndexOf(parameter) - 1);
                }
                strURL = rawUrl + "&" + parameter + "=" + controlPath;
            }
            else
            {
                if (rawUrl.Contains("?"))
                {
                    string[] d = rawUrl.Split('?');
                    strURL = d[0];
                    strURL = strURL + "?" + parameter + "=" + controlPath;
                }
                else
                {
                    strURL = rawUrl + "?" + parameter + "=" + controlPath;
                }
            }
            HttpContext.Current.Response.Redirect(strURL, false);
        }

        //protected void SelectTab(TabContainer tabContainer, string TabID)
        //{
        //    if (tabContainer == null)
        //        throw new ArgumentNullException("tabContainer");

        //    if (!String.IsNullOrEmpty(TabID))
        //    {
        //        AjaxControlToolkit.TabPanel tab = tabContainer.FindControl(TabID) as AjaxControlToolkit.TabPanel;
        //        if (tab != null)
        //        {
        //            tabContainer.ActiveTab = tab;
        //        }
        //    }
        //}

        //protected string GetActiveTabID(TabContainer tabContainer)
        //{
        //    if (tabContainer == null)
        //        throw new ArgumentNullException("tabContainer");

        //    if (tabContainer.ActiveTab != null)
        //        return tabContainer.ActiveTab.ID;

        //    return string.Empty;
        //}

        //public string GetUserModuleID(TabContainer tabContainer)
        //{
        //    if (tabContainer == null)
        //        throw new ArgumentNullException("tabContainer");

        //    if (tabContainer.ActiveTab != null)
        //        return tabContainer.ActiveTab.ID;

        //    return string.Empty;
        //}

        public string GetCurrentCultureName
        {
            get
            {
                return CultureInfo.CurrentCulture.Name;
                //return "hi-IN";
            }
        }

        public void Modules_Message_ShowMessage(Control ctl, string MessageTitle, string Message, string CompleteMessage, bool isSageAsyncPostBack, SageMessageType MessageType, string strCssClass)
        {

            Label lblUdpSageMesageTitle = ctl.FindControl("lblUdpSageMesageTitle") as Label;
            Label lblUdpSageMesageCustom = ctl.FindControl("lblUdpSageMesageCustom") as Label;
            Label lblUdpSageMesageDetail = ctl.FindControl("lblUdpSageMesageDetail") as Label;
            System.Web.UI.HtmlControls.HtmlGenericControl divUdpSageMessage = ctl.FindControl("divUdpSageMessage") as System.Web.UI.HtmlControls.HtmlGenericControl;


            isSageAsyncPostBack = true;
            bool isudpSageMessageVisible = false;
            if (isSageAsyncPostBack)
            {

                if (lblUdpSageMesageTitle != null && lblUdpSageMesageCustom != null && lblUdpSageMesageDetail != null && divUdpSageMessage != null)
                {
                    lblUdpSageMesageTitle.Text = MessageTitle;
                    if (MessageTitle == string.Empty)
                        lblUdpSageMesageTitle.Visible = false;

                    lblUdpSageMesageCustom.Text = Message;
                    if (Message == string.Empty)
                        lblUdpSageMesageCustom.Visible = false;

                    lblUdpSageMesageDetail.Text = CompleteMessage;
                    if (CompleteMessage == string.Empty)
                        lblUdpSageMesageDetail.Visible = false;

                    divUdpSageMessage.Attributes.Add("class", strCssClass);
                    isudpSageMessageVisible = true;
                }
            }

            System.Web.UI.HtmlControls.HtmlGenericControl divUdpMessage = ctl.FindControl("divUdpMessage") as System.Web.UI.HtmlControls.HtmlGenericControl;

            if (divUdpMessage != null)
            {
                if (isudpSageMessageVisible == true)
                {
                    divUdpMessage.Style.Add("display", "block");

                }

            }
        }

        /// <summary>
        /// File Url must be in "~/" nature
        /// like ~/js/scriptfile.js
        /// Note: Use only when script is required in head section other wise use IncludeScriptFile function
        /// </summary>
        /// <param name="FileUrl">File Url must be in "~/" nature</param>
        public void IncludeScriptFileInHeadSection(string FileUrl)
        {
            Literal LitSageScript = this.Page.FindControl("SageFrameModuleCSSlinks") as Literal;
            if (LitSageScript != null)
            {
                string strScripts = string.Empty;
                if (FileUrl != string.Empty)
                {
                    strScripts += "<script src=\"" + ResolveUrl(FileUrl) + "\" type=\"text/javascript\"></script>";
                }
                if (!LitSageScript.Text.Contains(strScripts))
                {
                    LitSageScript.Text += strScripts;
                }
            }
        }

        /// <summary>
        /// File Url must be in "~/" nature
        /// like ~/js/scriptfile.js
        /// Note:
        /// For this block of code your page musth have Literal of ID "LitSageScript"
        /// </summary>
        /// <param name="FileUrls"></param>
        public void IncludeScriptFile(ArrayList FileUrls)
        {
            Literal LitSageScript = this.Page.FindControl("LitSageScript") as Literal;            
            if (LitSageScript != null)
            {
                if (FileUrls.Count > 0)
                {
                    for (int i = 0; i < FileUrls.Count; i++)
                    {
                        string strScripts = string.Empty;
                        strScripts = "<script src=\"" + ResolveUrl(FileUrls[i].ToString()) + "\" type=\"text/javascript\"></script>";
                        if (!LitSageScript.Text.Contains(strScripts))
                        {
                            LitSageScript.Text += strScripts;
                        }
                    }
                }                
            }
        }

        /// <summary>
        /// File Url must be in "~/" nature
        /// like ~/js/scriptfile.js
        /// Note:
        /// For this block of code your page musth have Literal of ID "LitSageScript"
        /// </summary>
        /// <param name="FileUrl"></param>
        public void IncludeScriptFile(string FileUrl)
        {
            Literal LitSageScript = this.Page.FindControl("LitSageScript") as Literal;            
            if (LitSageScript != null)
            {
                string strScripts = string.Empty;
                if (FileUrl != string.Empty)
                {
                    strScripts += "<script src=\"" + ResolveUrl(FileUrl) + "\" type=\"text/javascript\"></script>";
                }
                if (!LitSageScript.Text.Contains(strScripts))
                {
                    LitSageScript.Text += strScripts;
                }
            }
        }

        /// <summary>
        /// File Url must be in "~/" nature
        /// "~/Modules/ModuleName/cssfile.css
        /// Note :
        /// For This block of code your page must have Literal of ID "SageFrameModuleCSSlinks"
        /// Use only css file except having name other than module.css, module.css file will automatically inlcude in the file is in
        /// root of module
        /// </summary>
        /// <param name="FileUrl"></param>
        public void IncludeCssFile(string FileUrl)
        {
            Literal SageFrameModuleCSSlinks = this.Page.FindControl("SageFrameModuleCSSlinks") as Literal;
            if (SageFrameModuleCSSlinks != null)
            {               
                string linkText = "<link href=\"" + Page.ResolveUrl(FileUrl) + "\" rel=\"stylesheet\" type=\"text/css\" />";
                if (!SageFrameModuleCSSlinks.Text.Contains(linkText))
                {
                    SageFrameModuleCSSlinks.Text += linkText;
                }
            }
        }

        public void IncludeJs(string ModuleName, params string[] files)
        {
            List<CssScriptInfo> lstResources = new List<CssScriptInfo>();
            foreach (string FilePath in files)
            {
                string folderPath = FilePath.LastIndexOf("/") > -1 ? FilePath.Substring(0, FilePath.LastIndexOf("/") + 1) : "";
                string FileName = Path.GetFileName(FilePath);
                if (HttpContext.Current.Session["ModuleJs"] != null)
                {
                    lstResources = HttpContext.Current.Session["ModuleJs"] as List<CssScriptInfo>;
                }
                bool exists = lstResources.Exists(
                                    delegate(CssScriptInfo obj)
                                    {
                                        return (obj.FileName == FileName);
                                    }
                                );
                if (!exists)
                {
                    lstResources.Add(new CssScriptInfo(ModuleName, FileName, folderPath, 1));
                }
            }
            HttpContext.Current.Session["ModuleJs"] = lstResources;
        }

        public void IncludeJsTop(string ModuleName, params string[] files)
        {
            List<CssScriptInfo> lstResources = new List<CssScriptInfo>();
            foreach (string FilePath in files)
            {
                string folderPath = FilePath.LastIndexOf("/") > -1 ? FilePath.Substring(0, FilePath.LastIndexOf("/") + 1) : "";
                string FileName = Path.GetFileName(FilePath);
                if (HttpContext.Current.Session["ModuleJs"] != null)
                {
                    lstResources = HttpContext.Current.Session["ModuleJs"] as List<CssScriptInfo>;
                }
                bool exists = lstResources.Exists(
                                    delegate(CssScriptInfo obj)
                                    {
                                        return (obj.FileName == FileName && obj.ModuleName == ModuleName);
                                    }
                                );
                if (!exists)
                {
                    lstResources.Add(new CssScriptInfo(ModuleName, FileName, folderPath, 0));
                }
            }
            HttpContext.Current.Session["ModuleJs"] = lstResources;
        }

        public void IncludeCss(string ModuleName, params string[] files)
        {
            List<CssScriptInfo> lstResources = new List<CssScriptInfo>();
            foreach (string FilePath in files)
            {
                string folderPath = FilePath.LastIndexOf("/") > -1 ? FilePath.Substring(0, FilePath.LastIndexOf("/") + 1) : "";
                string FileName = Path.GetFileName(FilePath);
                if (HttpContext.Current.Session["ModuleCss"] != null)
                {
                    lstResources = HttpContext.Current.Session["ModuleCss"] as List<CssScriptInfo>;
                }
                bool exists = lstResources.Exists(
                                    delegate(CssScriptInfo obj)
                                    {
                                        return (obj.FileName == FileName && obj.Path == folderPath);
                                    }
                                );
                if (!exists)
                {
                    lstResources.Add(new CssScriptInfo(ModuleName, FileName, folderPath, 1));
                }
            }
            HttpContext.Current.Session["ModuleCss"] = lstResources;
        }

        public void RegisterClientScriptToPage(string Key, string Value, bool checkMode)
        {
            if (checkMode)
            {
                if (!Page.ClientScript.IsClientScriptIncludeRegistered(Key))
                {
                    Page.ClientScript.RegisterClientScriptInclude(Key, Value);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptInclude(Key, Value);
            }
        }

        public bool ChekIsSuperUser(string userName)
        {
            bool IsSuperUser = false;
            if (userName != null)
            {
                MembershipUser user = Membership.GetUser();
                if (user != null)
                {
                    string[] sysRoles = SystemSetting.SUPER_ROLE;
                    foreach (string role in sysRoles)
                    {
                        if (Roles.IsUserInRole(user.UserName, role))
                        {
                            IsSuperUser = true;
                            break;
                        }
                    }
                }
            }
            return IsSuperUser;
        }
       
    }
}
