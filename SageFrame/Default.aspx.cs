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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SageFrame.Framework;
using System.Data;
using SageFrame.Web;
using System.Collections;
using SageFrame.Web.Utilities;
using SageFrame.SageFrameClass;
using SageFrame.Utilities;
using SageFrame.Shared;
using AspxCommerce.Core;

namespace SageFrame
{
    public partial class _Default : PageBase, SageFrameRoute
    {
        public string ControlPath = string.Empty;
        bool IsUseFriendlyUrls = true;
        public string prevpage;
        string sageRedirectPath = string.Empty;
        string sageNavigateUrl = string.Empty;
        SageFrameConfig sfConfig = new SageFrameConfig();
        protected void Page_Init(object sender, EventArgs e)
        {
            //Response.Write(Request.Url.ToString().Replace("http://", "https://"));
            SageInitPart();

            if (!(Request.CurrentExecutionFilePath.Contains(".js") || Request.CurrentExecutionFilePath.Contains("loaderpath") || Request.CurrentExecutionFilePath.Contains("aspxRootPath") || Request.CurrentExecutionFilePath.Contains("fonts") || Request.CurrentExecutionFilePath.Contains(".gif") || Request.CurrentExecutionFilePath.Contains(".jpg") || Request.CurrentExecutionFilePath.Contains(".png")))
            {
                if (Session["Ssl"] == null)
                {
                    Session["Ssl"] = "True";
                    //check logic redirect to or not
                    //btn click login and logout prob
                    List<SecurePageInfo> sp = GetSecurePage(GetStoreID, GetPortalID, GetCurrentCulture());
                    string pagename = GetPageSEOName(PagePath);
                    if (pagename != "Page-Not-Found")
                    {
                        if (Session["pagename"] != null)
                        {
                            prevpage = Session["pagename"].ToString();
                        }

                        if (prevpage != pagename)
                        {

                            Session["pagename"] = pagename;

                            for (int i = 0; i < sp.Count; i++)
                            {
                                if (pagename.ToLower() == sp[i].SecurePageName.ToString().ToLower())
                                {
                                    if (bool.Parse(sp[i].IsSecure.ToString()))
                                    {
                                        if (!HttpContext.Current.Request.IsSecureConnection)
                                        {
                                            if (!HttpContext.Current.Request.Url.IsLoopback) //Don't check when in development mode (i.e. localhost)
                                            {
                                                Session["prevurl"] = "https";
                                                Response.Redirect(Request.Url.ToString().Replace("http://", "https://"));

                                            }
                                        }
                                    }
                                    else
                                    {
                                        Session["prevurl"] = "http";
                                        Response.Redirect(Request.Url.ToString().Replace("https://", "http://"));
                                    }
                                }
                            }
                        }
                        else if (Session["prevurl"] != null)
                        {

                            if (Session["prevurl"].ToString() != Request.Url.ToString().Split(':')[0].ToString())
                            {
                                for (int i = 0; i < sp.Count; i++)
                                {
                                    if (pagename.ToLower() == sp[i].SecurePageName.ToString().ToLower())
                                    {
                                        if (bool.Parse(sp[i].IsSecure.ToString()))
                                        {
                                            if (!HttpContext.Current.Request.IsSecureConnection)
                                            {
                                                if (!HttpContext.Current.Request.Url.IsLoopback) //Don't check when in development mode (i.e. localhost)
                                                {
                                                    Response.Redirect(Request.Url.ToString().Replace("http://", "https://"));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Response.Redirect(Request.Url.ToString().Replace("https://", "http://"));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Request.QueryString["Re"] != null)
            {
                if (bool.Parse(Request.QueryString["Re"].ToString()))
                {
                    if (Session["IsFreeShipping"] != null)
                    {
                        HttpContext.Current.Session.Remove("IsFreeShipping");
                    }
                    if (Session["DiscountAmount"] != null)
                    {
                        HttpContext.Current.Session.Remove("DiscountAmount");

                    }
                    if (Session["CouponCode"] != null)
                    {
                        HttpContext.Current.Session.Remove("CouponCode");
                    }
                    if (Session["CouponApplied"] != null)
                    {
                        HttpContext.Current.Session.Remove("CouponApplied");
                    }
                }
            }
            if (Session["Ssl"] != null)
            {
                Session.Remove("Ssl");
            }
            if (GetPageSEOName(PagePath).ToString().ToLower() == "home" || GetPageSEOName(PagePath).ToString().ToLower() == "default")
            {

                if (Session["DiscountAll"] != null)
                {
                    HttpContext.Current.Session.Remove("DiscountAll");
                }
                if (Session["TaxAll"] != null)
                {
                    HttpContext.Current.Session.Remove("TaxAll");
                }
                if (Session["ShippingCostAll"] != null)
                {
                    HttpContext.Current.Session.Remove("ShippingCostAll");
                }
                if (Session["GrandTotalAll"] != null)
                {
                    HttpContext.Current.Session.Remove("GrandTotalAll");
                }
                if (Session["Gateway"] != null)
                {
                    HttpContext.Current.Session.Remove("Gateway");
                }
                if (Session["IsTestPayPal"] != null)
                {
                    HttpContext.Current.Session.Remove("IsTestPayPal");
                }
            }
        }

        private void SageInitPart()
        {
            try
            {
                string IsInstalled = Config.GetSetting("IsInstalled").ToString();
                string InstallationDate = Config.GetSetting("InstallationDate").ToString();
                if ((IsInstalled != "" && IsInstalled != "false") && InstallationDate != "")
                {
                    if (!(Request.CurrentExecutionFilePath.Contains(".js") || Request.CurrentExecutionFilePath.Contains("loaderpath") || Request.CurrentExecutionFilePath.Contains("aspxRootPath") || Request.CurrentExecutionFilePath.Contains(".gif") || Request.CurrentExecutionFilePath.Contains(".jpg") || Request.CurrentExecutionFilePath.Contains(".png")))
                    {
                        SetPortalCofig();
                        InitializePage();

                        //SageFrameConfig sfConfig = new SageFrameConfig();
                        //IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
                        SetAdminParts();
                        if (!IsPostBack)
                        {
                            GetStoreSettings();
                            StoreSettingConfig ssc = new StoreSettingConfig();
                            decimal timeToDeleteCartItems = decimal.Parse(ssc.GetStoreSettingsByKey(StoreSetting.TimeToDeleteAbandonedCart, GetStoreID, GetPortalID, GetCurrentCultureName));
                            decimal timeToAbandonCart = Convert.ToDecimal(ssc.GetStoreSettingsByKey(StoreSetting.CartAbandonedTime, GetStoreID, GetPortalID, GetCurrentCultureName));
                            DeleteAbandonedCartItems(GetStoreID, GetPortalID, timeToDeleteCartItems, timeToAbandonCart);
                        }
                        BindModuleControls();
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect(ResolveUrl("~/Install/InstallWizard.aspx"));
                }
            }
            catch
            {
            }
        }

        public List<SecurePageInfo> GetSecurePage(int storeID, int portalID, string culture)
        {
            try
            {
                List<SecurePageInfo> portalRoleCollection = new List<SecurePageInfo>();
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", culture));
                return sqlH.ExecuteAsList<SecurePageInfo>("usp_Aspx_GetAllSecurePagesByPortalAndStore", ParaMeter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetAdminParts()
        {
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
                LoadControl(phdAdminMenu, "~/Controls/ctl_AdminMenuOnly.ascx");
                divAdminControlPanel.Visible = true;
            }
            else
            {
                divAdminControlPanel.Visible = false;
            }
        }

        private void SetPortalCofig()
        {
            Hashtable hstPortals = GetPortals();
            SageUserControl suc = new SageUserControl();
            int portalID = 1;
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
            if (!(Request.CurrentExecutionFilePath.Contains(".js") || Request.CurrentExecutionFilePath.Contains("loaderpath") || Request.CurrentExecutionFilePath.Contains("aspxRootPath") || Request.CurrentExecutionFilePath.Contains(".gif") || Request.CurrentExecutionFilePath.Contains(".jpg") || Request.CurrentExecutionFilePath.Contains(".png")))
            {
                SagePageLoadPart();
            }

        }

        public void CreateGlobalVariables()
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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "templateName", " var templateName='" + TemplateName + "';", true);
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
            string myCartURL = ssc.GetStoreSettingsByKey(StoreSetting.ShoppingCartURL, GetStoreID, GetPortalID, GetCurrentCultureName);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myCartURL", " var myCartURL='" + myCartURL + "';", true);
            string userFullName = GetFullName(GetPortalID, GetUsername);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "userFullName", " var userFullName='" + userFullName + "';", true);

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
                sageRedirectPath = ResolveUrl("{~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
                sageNavigateUrl = ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalRedirectPath", " var aspxRedirectPath='" + sageRedirectPath + "';", true);

        }
        private void SagePageLoadPart()
        {
            try
            {
                CreateGlobalVariables();
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

                }
                if ((SessionTracker)Session["Tracker"] != null)
                {
                    SessionTracker sessionTracker = (SessionTracker)Session["Tracker"];
                    if (string.IsNullOrEmpty(sessionTracker.PortalID))
                    {
                        sessionTracker.PortalID = GetPortalID.ToString();
                        sessionTracker.Username = GetUsername;
                        SageFrameConfig sfConfig = new SageFrameConfig();
                        sessionTracker.InsertSessionTrackerPages = sfConfig.GetSettingsByKey(SageFrameSettingKeys.InsertSessionTrackingPages);

                        SageFrame.Web.SessionLog SLog = new SageFrame.Web.SessionLog();
                        SLog.SessionTrackerUpdateUsername(sessionTracker, GetUsername, GetPortalID.ToString());
                        SLog.StoreSessionTrackerAdd(sessionTracker, GetStoreID, GetPortalID);
                        Session["Tracker"] = sessionTracker;
                    }
                }
                //if ((StoreSettingInfo)Session["DefaultStoreSettings"] == null)
                //{
                //    AspxCommerceWebService AspxCommerceWebService = new AspxCommerceWebService();
                //    StoreSettingInfo DefaultStoreSettings = AspxCommerceWebService.GetAllStoreSettings(GetStoreID, GetPortalID, GetCurrentCultureName);
                //    Session["DefaultStoreSettings"] = DefaultStoreSettings;
                //}

            }
            catch
            {
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
            if (PagePath != null)
            {
                suc.PagePath = PagePath;
            }
            else
            {
                SageFrameConfig sfConfig = new SageFrameConfig();
                suc.PagePath = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx";
            }
            if (Request.QueryString["pgnm"] != null)
            {
                PageSEOName = Request.QueryString["pgnm"].ToString();
            }
            else
            {
                PageSEOName = GetPageSEOName(PagePath);
            }

            //:TODO: Need to get controlType and pageID from the selected page from routing path
            //string controlType = "0";
            //string pageID = "2";
            string redirecPath = string.Empty;
            if (PageSEOName != string.Empty)
            {
                SageFrameConfig sfConfig = new SageFrameConfig();
                string SEOName = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage);
                if (SEOName.ToLower() == PageSEOName.ToLower())
                {
                    divTopWrapper.Attributes.Add("class", " cssClassTopWrapper cssClassIndexPage");
                }
                //TODO:: check store access control here Based on @IPAddress, @Domain, @CustomerName, @email, @StoreID, @PortalID
                StoreAccessDetailsInfo storeAccessTracker = new StoreAccessDetailsInfo();
                storeAccessTracker.PortalID = GetPortalID.ToString();
                storeAccessTracker.StoreID = GetStoreID.ToString();
                storeAccessTracker.Username = GetUsername;
                if (Membership.GetUser() != null)
                {
                    MembershipUser userDetail = Membership.GetUser(GetUsername);
                    storeAccessTracker.UserEmail = userDetail.Email;
                }
                else
                {
                    storeAccessTracker.UserEmail = "";
                }
                StoreAccessResultInfo saResults = (StoreAccessResultInfo)GetStoreAccessByCurrentData(storeAccessTracker);
                bool storeClosed = (bool)saResults.StoreClosed;
                bool storeAccessible = (bool)saResults.IsAccess;
                if (!storeClosed)
                {
                    if (storeAccessible)
                    {
                        DataSet dsPageSettings = new DataSet();
                        dsPageSettings = sfConfig.GetPageSettingsByPageSEOName("1", PageSEOName, GetUsername);
                        if (bool.Parse(dsPageSettings.Tables[0].Rows[0][0].ToString()) == true)
                        {
                            //if (bool.Parse(dsPageSettings.Tables[0].Rows[0][2].ToString()) != true)
                            //{
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
                                        var SupportsPartialRendering = dtPageModule.Rows[i]["SupportsPartialRendering"].ToString();
                                        PlaceHolder phdPlaceHolder = (PlaceHolder)this.FindControl(paneName);
                                        if (phdPlaceHolder != null)
                                        {
                                            phdPlaceHolder = LoadControl(i.ToString(), bool.Parse(SupportsPartialRendering), phdPlaceHolder, ControlSrc, paneName, UserModuleID);
                                        }
                                        //AddModuleCssToPage(ControlSrc, false);
                                    }
                                }
                            }
                            else
                            {
                                if (IsUseFriendlyUrls)
                                {
                                    if (GetPortalID > 1)
                                    {
                                        redirecPath = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalPageNotAccessible) + ".aspx");
                                    }
                                    else
                                    {
                                        redirecPath = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalPageNotAccessible) + ".aspx");
                                    }
                                }
                                else
                                {
                                    redirecPath = ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalPageNotAccessible));
                                }
                                Response.Redirect(redirecPath);
                            }
                            //}
                            //else
                            //{
                            //    if (IsUseFriendlyUrls)
                            //    {
                            //        if (GetPortalID > 1)
                            //        {
                            //            redirecPath = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + PageSEOName + ".aspx");
                            //        }
                            //        else
                            //        {
                            //            redirecPath = ResolveUrl("~/" + PageSEOName + ".aspx");
                            //        }
                            //    }
                            //    else
                            //    {
                            //        redirecPath = ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + PageSEOName);
                            //    }
                            //    CommonHelper.EnsureSSL(true, redirecPath);
                            //}
                        }
                        else
                        {
                            if (IsUseFriendlyUrls)
                            {
                                if (GetPortalID > 1)
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
                    else
                    {
                        //Store NOT Accessed Page
                        string blockedPortalUrl = string.Empty;
                        if (GetPortalID > 1)
                        {
                            if (IsUseFriendlyUrls)
                            {
                                blockedPortalUrl = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                            }
                            else
                            {
                                blockedPortalUrl = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));

                            }
                        }
                        else
                        {

                            if (IsUseFriendlyUrls)
                            {
                                blockedPortalUrl = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                            }
                            else
                            {
                                blockedPortalUrl = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));

                            }
                        }
                        Session["StoreBlocked"] = blockedPortalUrl;
                        HttpContext.Current.Response.Redirect(ResolveUrl("~/Modules/AspxCommerce/Store-Not-Accessed.aspx"));

                    }
                }
                else
                {
                    //Store Closed Page
                    string closePortalUrl = string.Empty;
                    if (GetPortalID > 1)
                    {
                        if (IsUseFriendlyUrls)
                        {
                            closePortalUrl = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                        }
                        else
                        {
                            closePortalUrl = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));

                        }
                    }
                    else
                    {

                        if (IsUseFriendlyUrls)
                        {
                            closePortalUrl = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                        }
                        else
                        {
                            closePortalUrl = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));

                        }
                    }
                    Session["StoreClosed"] = closePortalUrl;
                    HttpContext.Current.Response.Redirect(ResolveUrl("~/Modules/AspxCommerce/Store-Closed.aspx"));
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

        public override void ShowMessage(string MessageTitle, string Message, string CompleteMessage, bool isSageAsyncPostBack, SageMessageType MessageType)
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

        public StoreAccessResultInfo GetStoreAccessByCurrentData(StoreAccessDetailsInfo storeAccessTracker)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();

                ParaMeterCollection.Add(new KeyValuePair<string, string>("@IPAddress", storeAccessTracker.UserIPAddress));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@Domain", storeAccessTracker.UserDomainURL));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@CustomerName", storeAccessTracker.Username));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@Email", storeAccessTracker.UserEmail));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", storeAccessTracker.PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@StoreID", storeAccessTracker.StoreID));
                SQLHandler sqlH = new SQLHandler();
                StoreAccessResultInfo obj = sqlH.ExecuteAsObject<StoreAccessResultInfo>("dbo.usp_Aspx_CheckStoreAccess", ParaMeterCollection);
                return obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteAbandonedCartItems(int storeID, int portalID, decimal timeToDeleteCartItems, decimal timeToAbandonCart)
        {
            try
            {
                List<SecurePageInfo> portalRoleCollection = new List<SecurePageInfo>();
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParaMeter.Add(new KeyValuePair<string, object>("@TimeToDeleteCartItems", timeToDeleteCartItems));
                ParaMeter.Add(new KeyValuePair<string, object>("@AbandonedCartTime", timeToAbandonCart));
                sqlH.ExecuteAsList<SecurePageInfo>("usp_Aspx_DeleteAbandonedCartItems", ParaMeter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetFullName(int portalId, string userName)
        {
            var paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            paramCol.Add(new KeyValuePair<string, object>("@UserName", userName));
            var sageSQL = new SQLHandler();
            return sageSQL.ExecuteAsScalar<string>("[dbo].[usp_Aspx_GetUserFirstandLastName]", paramCol);
        }

        private void GetStoreSettings()
        {
            Hashtable hst = new Hashtable();
            StoreSettingProvider sep = new StoreSettingProvider();
            if (HttpContext.Current.Cache["AspxStoreSetting" + GetPortalID.ToString() + GetStoreID.ToString()] != null)
            {
                hst = (Hashtable)HttpContext.Current.Cache["AspxStoreSetting" + GetPortalID.ToString() + GetStoreID.ToString()];
            }
            else
            {
                DataTable dt = sep.GetStoreSettings(GetStoreID, GetPortalID, GetCurrentCultureName); //GetSettingsByPortal();   
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                    }
                }
            }
            HttpContext.Current.Cache.Insert("AspxStoreSetting" + GetPortalID.ToString() + GetStoreID.ToString(), hst);
        }
    }
}