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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using AjaxControlToolkit;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using AjaxControlToolkit.HTMLEditor.Popups;
using AjaxControlToolkit.HTMLEditor.CustomToolbarButton;
using AjaxControlToolkit.HTMLEditor.CustomPopups;
using SageFrame.Framework;
using System.Data;
using SageFrame.Web;
using SageFrame.PortalSetting;
using System.Collections;
using SageFrame.Web.Utilities;
using SageFrame.SageFrameClass;
using SageFrame.Utilities;
using SageFrame.RolesManagement;
using SageFrame.Shared;
using System.IO;
using AspxCommerce.Core;

namespace SageFrame
{
    public partial class Sagin_Default : PageBase, SageFrameRoute
    {
        public string ControlPath = string.Empty;
        bool IsUseFriendlyUrls = true;
        string sageRedirectPath = string.Empty;
        string sageNavigateUrl = string.Empty;
		SageFrameConfig sfConfig = new SageFrameConfig();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!(Request.RawUrl.Contains(".gif") || Request.RawUrl.Contains(".jpg") || Request.RawUrl.Contains(".png")))
            {
                SageInitPart();
            }
        }

        private void SageInitPart()
        {
            string IsInstalled = Config.GetSetting("IsInstalled").ToString();
            string InstallationDate = Config.GetSetting("InstallationDate").ToString();
            if ((IsInstalled != "" && IsInstalled != "false") && InstallationDate != "")
            {
                if (!(Request.RawUrl.Contains(".gif") || Request.RawUrl.Contains(".jpg") || Request.RawUrl.Contains(".png")))
                {
                    SetPortalCofig();
                    InitializePage();
                    SageFrameConfig sfConfig = new SageFrameConfig();
                    IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
                    LoadControl(phdAdministrativBreadCrumb, "~/Controls/ctl_AdminBreadCrum.ascx");
                    LoadControl(phdAdminMenu, "~/Controls/ctl_AdminMenuOnly.ascx");
                    BindModuleControls();
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect(ResolveUrl("~/Install/InstallWizard.aspx"));
            }
        }

        private void SetPortalCofig()
        {
            Hashtable hstPortals = GetPortals();
            SageUserControl suc = new SageUserControl();

            suc.PagePath = PagePath;
            int portalID = 1;
            //ptlid=-9&ptSEO=contruction&pgnm=faqs
            if (string.IsNullOrEmpty(Request.QueryString["ptSEO"]))
            {
                if (string.IsNullOrEmpty(PortalSEOName))
                {
                    PortalSEOName = "default";
                }
                else if (!hstPortals.ContainsKey(PortalSEOName.ToLower().Trim()))
                {
                    PortalSEOName = "default";
                }
                else
                {
                    portalID = int.Parse(hstPortals[PortalSEOName.ToLower().Trim()].ToString());
                }
            }
            else
            {
                PortalSEOName = Request.QueryString["ptSEO"].ToString().ToLower().Trim();
                portalID = Int32.Parse(Request.QueryString["ptlid"].ToString());
            }
            suc.SetPortalSEOName(PortalSEOName.ToLower().Trim());
            Session["SageFrame.PortalSEOName"] = PortalSEOName.ToLower().Trim();
            Session["SageFrame.PortalID"] = portalID;
            suc.SetPortalID(portalID);
            SetPortalID(portalID);

            int storeID = portalID;
            //TODO:: set StoreID According the URL HERE
            Session["SageFrame.StoreID"] = storeID;
            suc.SetStoreID(storeID);
            SetStoreID(storeID);

            if (HttpContext.Current.User != null)
            {
                SettingProvider objSP = new SettingProvider();
                int customerID = 0;
                if (Membership.GetUser() != null)
                {
                    string strRoles = string.Empty;
                    //RolesManagementDataContext dbRole = new RolesManagementDataContext(SystemSetting.SageFrameConnectionString);
                    //var userRoles = dbRole.sp_RoleGetByUsername(HttpContext.Current.User.Identity.Name, GetPortalID).ToList();
                    //foreach (var userRole in userRoles)
                    //{
                    //    strRoles += userRole.RoleId + ",";
                    //}
                    List<SageUserRole> sageUserRolles = objSP.RoleListGetByUsername(HttpContext.Current.User.Identity.Name, GetPortalID);
                    if (sageUserRolles != null)
                    {
                        foreach (SageUserRole userRole in sageUserRolles)
                        {
                            strRoles += userRole.RoleId + ",";
                        }
                    }
                    if (strRoles.Length > 1)
                    {
                        strRoles = strRoles.Substring(0, strRoles.Length - 1);
                    }
                    if (strRoles.Length > 0)
                    {
                        SetUserRoles(strRoles);
                    }

                    //TO Get Customer Details

                    CustomerGeneralInfo sageUserCust = objSP.CustomerIDGetByUsername(HttpContext.Current.User.Identity.Name, GetPortalID, GetStoreID);
                    if (sageUserCust != null)
                    {
                        customerID = sageUserCust.CustomerID;
                    }
                    Session["SageFrame.CustomerID"] = customerID;
                    suc.SetCustomerID(customerID);
                    SetCustomerID(customerID);
                }
                else
                {
                    //TO Get Customer Details
                    CustomerGeneralInfo sageUserCust = objSP.CustomerIDGetByUsername("anonymoususer", GetPortalID, GetStoreID);
                    if (sageUserCust != null)
                    {
                        customerID = sageUserCust.CustomerID;
                    }
                    Session["SageFrame.CustomerID"] = customerID;
                    suc.SetCustomerID(customerID);
                    SetCustomerID(customerID);
                }
            }
        }

        public void SetUserRoles(string strRoles)
        {
            Session["SageUserRoles"] = strRoles;
            HttpCookie cookie = HttpContext.Current.Request.Cookies["SageUserRolesCookie"];
            if (cookie == null)
            {
                cookie = new HttpCookie("SageUserRolesCookie");
            }
            cookie["SageUserRolesProtected"] = strRoles;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        private Hashtable GetPortals()
        {
            
            Hashtable hstAll = new Hashtable();
            if (HttpContext.Current.Cache["Portals"] != null)
            {
                hstAll = (Hashtable)HttpContext.Current.Cache["Portals"];
            }
            else
            {
                SettingProvider objSP = new SettingProvider();
                List<SagePortals> sagePortals = objSP.PortalGetList();
                foreach (SagePortals portal in sagePortals)
                {
                    hstAll.Add(portal.SEOName.ToLower().Trim(), portal.PortalID);
                }
            }
            HttpContext.Current.Cache.Insert("Portals", hstAll);
            return hstAll;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Request.RawUrl.Contains(".gif") || Request.RawUrl.Contains(".jpg") || Request.RawUrl.Contains(".png")))
            {
                
                SagePageLoadPart();
            }
        }

        public void LoadGlobalVariables()
        {
            IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalServicePath", " var aspxservicePath='" + ResolveUrl("~/") + "Modules/AspxCommerce/AspxCommerceServices/" + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalRootPath", " var aspxRootPath='" + ResolveUrl("~/") + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalTemplateFolderPath", " var aspxTemplateFolderPath='" + ResolveUrl("~/") + "Templates/" + TemplateName + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "storeID", " var storeID='" + GetStoreID + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "portalID", " var portalID='" + GetPortalID + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "culturename", " var cultureName='" + GetCurrentCultureName + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "templatename", " var templateName='" + TemplateName + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "customerid", " var customerID='" + GetCustomerID + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "username", " var userName='" + GetUsername + "';", true);
            //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "noimagepath", " var aspxTemplateFolderPath='" + ResolveUrl("~/") + "Templates/" + TemplateName + "';", true);
            //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "defaultlogourl", " var aspxTemplateFolderPath='" + ResolveUrl("~/") + "Templates/" + TemplateName + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isfriendlyurl", " var IsUseFriendlyUrls='" + IsUseFriendlyUrls + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sessioncode", " var sessionCode='" + HttpContext.Current.Session.SessionID.ToString() + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "clientIPAddress", " var clientIPAddress='" + HttpContext.Current.Request.UserHostAddress + "';", true);
            string userIP = HttpContext.Current.Request.UserHostAddress;
            string countryName = "";
            IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
            ipToCountry.GetCountry(userIP, out countryName);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "clientCountry", " var aspxCountryName='" + countryName + "';", true);
            StoreSettingConfig ssc = new StoreSettingConfig();
            string currencyCode = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, GetStoreID, GetPortalID, GetCurrentCultureName);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "region", " var region='" + StoreSetting.GetRegionFromCurrencyCode(currencyCode, GetStoreID, GetPortalID) + "';", true);
            if (IsUseFriendlyUrls)
            {
                if (GetPortalID > 1)
                {
                    sageRedirectPath = ResolveUrl("~/portal/" + GetPortalSEOName + "/");
                    sageNavigateUrl = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                }
                else
                {
                    sageRedirectPath = ResolveUrl("~/");
                    sageNavigateUrl = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                }
            }
            else
            {
                sageRedirectPath = ResolveUrl("{~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=");
                sageNavigateUrl = ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalRedirectPath", " var aspxRedirectPath='" + sageRedirectPath + "';", true);

        }

        private void SagePageLoadPart()
        {
            LoadGlobalVariables();
            if (!IsPostBack)
            {
                hypHome.NavigateUrl = sageNavigateUrl;
                hypHome.Text = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage);
                hypHome.ImageUrl = GetTemplateImageUrl("home.png", true);
                hypPreview.NavigateUrl = hypHome.NavigateUrl;
                Image imgProgress = (Image)UpdateProgress1.FindControl("imgPrgress");
                if (imgProgress != null)
                {
                    imgProgress.ImageUrl = GetTemplateImageUrl("ajax-loader.gif", true);
                }
                bool IsAdmin = false;
                if (HttpContext.Current.User != null)
                {
                    MembershipUser user = Membership.GetUser();
                    if (user != null)
                    {
                        string[] sysRoles = SystemSetting.SYSTEM_SUPER_ROLES;
                        foreach (string role in sysRoles)
                        {
                            if (Roles.IsUserInRole(user.UserName, role))
                            {
                                IsAdmin = true;
                                break;
                            }
                        }
                    }
                }
                if (IsAdmin)
                {
                    divAdminControlPanel.Attributes.Add("style", "display:block");
                }
                else
                {
                    divAdminControlPanel.Attributes.Add("style", "display:none");
                }
                Session["payMentGateWayRefresh"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            SessionTracker sessionTracker = (SessionTracker)Session["Tracker"];
            if (string.IsNullOrEmpty(sessionTracker.PortalID))
            {
                sessionTracker.PortalID = GetPortalID.ToString();
                sessionTracker.Username = GetUsername;
                SageFrameConfig sfConfig = new SageFrameConfig();
                sessionTracker.InsertSessionTrackerPages = sfConfig.GetSettingsByKey(SageFrameSettingKeys.InsertSessionTrackingPages);
                SageFrame.Web.SessionLog SLog = new SageFrame.Web.SessionLog();
                SLog.SessionTrackerUpdateUsername(sessionTracker, GetUsername, GetPortalID.ToString());
                Session["Tracker"] = sessionTracker;
            }
        }

        private void BindModuleControls()
        {
            string preFix = string.Empty;
            string paneName = string.Empty;
            string ControlSrc = string.Empty;
            string phdContainer = string.Empty;
            string PageSEOName = string.Empty;
            SageUserControl suc = new SageUserControl();
           
            string PageName = PagePath;
            if (PagePath == null)
            {
                string PageUrl = Request.RawUrl;
                PageName = Path.GetFileNameWithoutExtension(PageUrl);
            }
            else
            {
                PageName = PagePath;
            }
            suc.PagePath = PageName;
            if (Request.QueryString["pgnm"] != null)
            {
                PageSEOName = Request.QueryString["pgnm"].ToString();
            }
            else
            {
                PageSEOName = GetPageSEOName(PageName);
            }
            //:TODO: Need to get controlType and pageID from the selected page from routing path
            //string controlType = "0";
            //string pageID = "2";
            string redirecPath = string.Empty;
            if (PageSEOName != string.Empty)
            {
                DataSet dsPageSettings = new DataSet();
                SageFrameConfig sfConfig = new SageFrameConfig();
                dsPageSettings = sfConfig.GetPageSettingsByPageSEONameForAdmin("1", PageSEOName, GetUsername);
                if (bool.Parse(dsPageSettings.Tables[0].Rows[0][0].ToString()) == true)
                {
                    if (bool.Parse(dsPageSettings.Tables[0].Rows[0][1].ToString()) == true)
                    {
                        // Get ModuleControls data table
                        DataTable dtPages = dsPageSettings.Tables[1];
                        if (dtPages != null && dtPages.Rows.Count > 0)
                        {
                            OverridePageInfo(dtPages);
                        }

                        // Get ModuleDefinitions data table
                        DataTable dtPageModule = dsPageSettings.Tables[2];
                        if (dtPageModule != null && dtPageModule.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtPageModule.Rows.Count; i++)
                            {

                                paneName = dtPageModule.Rows[i]["PaneName"].ToString();
                                if (string.IsNullOrEmpty(paneName))
                                    paneName = "ContentPane";
                                string UserModuleID = dtPageModule.Rows[i]["UserModuleID"].ToString();
                                ControlSrc = "/" + dtPageModule.Rows[i]["ControlSrc"].ToString();
                                string SupportsPartialRendering = dtPageModule.Rows[i]["SupportsPartialRendering"].ToString();
                                PlaceHolder phdPlaceHolder = (PlaceHolder)this.FindControl(paneName);
                                if (phdPlaceHolder != null)
                                {
                                    phdPlaceHolder = LoadControl(i.ToString(), bool.Parse(SupportsPartialRendering), phdPlaceHolder, ControlSrc, paneName, UserModuleID);
                                }
                               // AddModuleCssToPage(ControlSrc, false);
                            }
                        }
                    } 
                    else
                    {
                        if (IsUseFriendlyUrls)
                        {
                            if (GetPortalID > 1)
                            {
                              
                                    redirecPath =
                                        ResolveUrl("~/portal/" + GetPortalSEOName + "/" +
                                                   sfConfig.GetSettingsByKey(
                                                       SageFrameSettingKeys.PortalPageNotAccessible) + ".aspx");
                              
                            }
                            else
                            {
                             
                                  redirecPath = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + ".aspx");
                              
                            }
                        }
                        else
                        {
                            redirecPath = ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalPageNotAccessible));
                        }
                        Response.Redirect(redirecPath);
                    }
                }
                else
                {
                    if (IsUseFriendlyUrls)
                    {
                        if (GetPortalID>1)
                        {
                            redirecPath = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalPageNotFound) + ".aspx");
                        }
                        else
                        {
                            redirecPath = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalPageNotFound) + ".aspx");
                        }
                    }
                    else
                    {
                        redirecPath = ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalPageNotFound));
                    }
                    Response.Redirect(redirecPath);
                }
            }

            HttpContext.Current.Session["ModuleCss"] = null;
            HttpContext.Current.Session["ModuleJs"] = null;
        }

        private void LoadControl(PlaceHolder ContainerControl, string controlSource)
        {
            UserControl ctl = this.Page.LoadControl(controlSource) as UserControl;
            ctl.EnableViewState = true;
            ContainerControl.Controls.Add(ctl);
        }

        #region SageFrameRoute Members

        public string PagePath
        {
            get;
            set;
        }

        public string PortalSEOName
        {
            get;
            set;
        }
        public string UserModuleID
        { 
            get; 
            set; 
        }
        public string ControlType
        {
            get;
            set;
        }
		public string ControlMode { get; set; }
        public string Key { get; set; }
        #endregion

        public override void ShowMessage(string MessageTitle,  string Message, string CompleteMessage, bool isSageAsyncPostBack, SageMessageType MessageType)
        {

            string strCssClass = GetMessageCsssClass(MessageType);

            int Cont = this.Page.Controls.Count;
            List<SageFrameStringKeyValue> lst = SageFrameLists.ModulePanes();
            for (int i = 0; i < lst.Count; i++)
            {
                string strName = lst[i].Key;
                PlaceHolder phd = this.Page.FindControl(strName) as PlaceHolder;
                if (phd != null)
                {
                    foreach (Control ctl in phd.Controls)
                    {
                        if (ctl.GetType().FullName.ToLower() == "ASP.modules_message_message_ascx".ToLower())
                        {
                            SageUserControl tt = (SageUserControl)ctl;
                            tt.Modules_Message_ShowMessage(tt, MessageTitle, Message, CompleteMessage, isSageAsyncPostBack, MessageType, strCssClass);
                        }
                    }
                }
            }         
        }
    }
}
