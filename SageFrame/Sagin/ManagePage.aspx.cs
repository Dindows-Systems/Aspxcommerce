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
using SageFrame.Framework;
using SageFrame.Web;
using System.Collections;
using SageFrame.PortalSetting;
using SageFrame.SageFrameClass;
using System.Data;
using SageFrame.ModuleControls;

namespace SageFrame.Sagin
{
    public partial class ManagePage : PageBase, SageFrameRoute
    {
        public Int32 usermoduleIDControl = 0;
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
            SetPortalCofig();
            InitializePage();
            LoadModuleControls();          
            IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            //LoadControl(phdAdministrativBreadCrumb, "~/Controls/ctl_AdminBreadCrum.ascx");
            LoadControl(phdAdminMenu, "~/Controls/ctl_AdminMenuOnly.ascx");
            if (sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalShowFooter) == "1")
            {
                divFooterWrapper.Attributes.Add("style", "display:block;");
            }
            else
            {
                divFooterWrapper.Attributes.Add("style", "display:none;");
            }
            BindModuleControls();
            HiddenField hdnPageID = new HiddenField();
            hdnPageID.ID = "hdnPageID";
            if (HttpContext.Current.Request.QueryString["pgid"] != null)
            {
                hdnPageID.Value = HttpContext.Current.Request.QueryString["pgid"].ToString();
            }
            HiddenField hdnActiveIndex = new HiddenField();
            hdnActiveIndex.ID = "hdnActiveIndex";
            if (HttpContext.Current.Request.QueryString["ActInd"] != null)
            {
                hdnActiveIndex.Value = HttpContext.Current.Request.QueryString["ActInd"].ToString();
            }
            this.Page.Form.Controls.Add(hdnPageID);
            this.Page.Form.Controls.Add(hdnActiveIndex);
            OverridePageInfo(null);
        }
        private void LoadControl(PlaceHolder ContainerControl, string controlSource)
        {
            UserControl ctl = this.Page.LoadControl(controlSource) as UserControl;
            ctl.EnableViewState = true;
            ContainerControl.Controls.Add(ctl);
        }
        private void BindModuleControls()
        {
            string preFix = string.Empty;
            string paneName = string.Empty;
            string ControlSrc = string.Empty;
            string phdContainer = string.Empty;
            string PageSEOName = string.Empty;
            SageUserControl suc = new SageUserControl();
            suc.PagePath = PagePath;
            if (Request.QueryString["pgnm"] != null)
            {
                PageSEOName = Request.QueryString["pgnm"].ToString();
            }
            else
            {
                PageSEOName ="ManagePages";
            }
            PageSEOName = "ManagePages";
            string redirecPath = string.Empty;
            if (PageSEOName != string.Empty)
            {
                DataSet dsPageSettings = new DataSet();
                SageFrameConfig sfConfig = new SageFrameConfig();
                dsPageSettings = sfConfig.GetPageSettingsByPageSEONameForAdmin("1", PageSEOName, GetUsername);
                if (bool.Parse(dsPageSettings.Tables[0].Rows[0][0].ToString()) == true)
                {    //checking IsPageAccessible true or false
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
                                string strUserModuleID = dtPageModule.Rows[i]["UserModuleID"].ToString();
                                ControlSrc = "/" + dtPageModule.Rows[i]["ControlSrc"].ToString();
                                string SupportsPartialRendering = dtPageModule.Rows[i]["SupportsPartialRendering"].ToString();
                                PlaceHolder phdPlaceHolder = (PlaceHolder)this.FindControl(paneName);
                                if (phdPlaceHolder != null)
                                {
                                    phdPlaceHolder = LoadControl(i.ToString(), bool.Parse(SupportsPartialRendering), phdPlaceHolder, ControlSrc, paneName, strUserModuleID);
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

            HttpContext.Current.Session["ModuleCss"] = null;
            HttpContext.Current.Session["ModuleJs"] = null;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Request.RawUrl.Contains(".gif") || Request.RawUrl.Contains(".jpg") || Request.RawUrl.Contains(".png")))
            {
                SagePageLoadPart();
            }
        }
        public void SetGlobalVariables()
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
            SetGlobalVariables();
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
                if (HttpContext.Current.Request.QueryString["ManageReturnUrl"] != null)
                {
                    if (IsUseFriendlyUrls)
                    {
                        btnBack.PostBackUrl = HttpContext.Current.Request.QueryString["ManageReturnUrl"].ToString();
                    }
                    else
                    {
                        string strURL = string.Empty;
                        string[] keys = Request.QueryString.AllKeys;
                        for (int i = 0; i < Request.QueryString.Count; i++)
                        {
                            string[] values = Request.QueryString.GetValues(i);
                            if (keys[i] != "ActInd" && keys[i] != "pgid" && keys[i] != "ManageReturnUrl" && keys[i] != "CtlType" && keys[i] != "ActInd" && keys[i] != "umid")
                            {
                                strURL += keys[i] + '=' + values[0] + '&';
                            }
                        }
                        if (strURL.Length > 0)
                        {
                            strURL = strURL.Remove(strURL.LastIndexOf('&'));
                        }
                        btnBack.PostBackUrl = HttpContext.Current.Request.QueryString["ManageReturnUrl"].ToString() + "?" + strURL;
                    }
                    btnBackTop.PostBackUrl = btnBack.PostBackUrl;
                }
            }
        }

        public void LoadControl(string UpdatePanelIDPrefix, bool IsPartialRendring, PlaceHolder ContainerControl, string ControlSrc)
        {
            SageUserControl ctl;
            if (ControlSrc.ToLower().EndsWith(".ascx"))
            {
                if (IsPartialRendring)
                {
                    UpdatePanel udp = CreateUpdatePanel1(UpdatePanelIDPrefix, UpdatePanelUpdateMode.Always, ContainerControl.Controls.Count);
                    ctl = this.Page.LoadControl("~" + ControlSrc) as SageUserControl;
                    udp.ContentTemplateContainer.Controls.Add(ctl);
                    ctl.SageUserModuleID = SageUserModuleID;
                    ContainerControl.Controls.Add(udp);
                }
                else
                {
                    ctl = this.Page.LoadControl("~" + ControlSrc) as SageUserControl;
                    ctl.SageUserModuleID = SageUserModuleID;
                    ContainerControl.Controls.Add(ctl);
                }
               // AddModuleCssToPage(ControlSrc, false);
            }
            
        }

        public string SageUserModuleID
        {
            get 
            {
                string strUserModuleID = string.Empty;                
                if (!string.IsNullOrEmpty(UserModuleID))
                {
                    strUserModuleID = UserModuleID;
                }
                else if (HttpContext.Current.Request.QueryString["umid"] != null)
                {
                    strUserModuleID = HttpContext.Current.Request.QueryString["umid"].ToString();
                }
                return strUserModuleID;
            }
        }
       
        public string SageControlType
        {
            get
            {
                string strControlType = string.Empty;
                if (!string.IsNullOrEmpty(ControlType))
                {
                    if (ControlType.ToLower() == "edit")
                    {
                        strControlType = "2";
                    }
                    else
                    {
                        strControlType = "3";
                    }
                }
                else if (HttpContext.Current.Request.QueryString["CtlType"] != null)
                {
                    strControlType = HttpContext.Current.Request.QueryString["CtlType"].ToString();
                }
                return strControlType;
            }
        }
        public UpdatePanel CreateUpdatePanel1(string Prefix, UpdatePanelUpdateMode Upm, int PaneUpdatePanelCount)
        {
            UpdatePanel udp = new UpdatePanel();
            udp.UpdateMode = Upm;
            PaneUpdatePanelCount++;
            udp.ID = "_udp_" + "_" + PaneUpdatePanelCount + Prefix;
            return udp;
        }

        private void LoadModuleControls()
        {
            if (string.IsNullOrEmpty(SageUserModuleID) || string.IsNullOrEmpty(SageControlType))
            {
            }
            else
            {
                ModuleControlsDataContext moduleControlDB = new ModuleControlsDataContext(SystemSetting.SageFrameConnectionString);
                var moduleControl = moduleControlDB.sp_ModuleControlGetByUserModuleIDAndControlType(Int32.Parse(SageUserModuleID), Int32.Parse(SageControlType), GetPortalID, GetUsername).SingleOrDefault();
                if (!string.IsNullOrEmpty(moduleControl.ControlSrc))
                {
                    LoadControl(new Random().Next().ToString(), false, PlaceHolderExtension, "/" + moduleControl.ControlSrc);
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string ReturnUrl = string.Empty;
            //string pageID = string.Empty;
            //string ActiveIndex = string.Empty;
            //if (HttpContext.Current.Request.QueryString["ReturnUrl"] != null)
            //{
            //    //string[] arrUrl;
            //    //arrUrl = Request.RawUrl.Split('?');
            //    //string strURL = string.Empty;
            //    //if (!IsUseFriendlyUrls)
            //    //{
            //    //    string[] keys = Request.QueryString.AllKeys;

            //    //    for (int i = 0; i < Request.QueryString.Count; i++)
            //    //    {
            //    //        string[] values = Request.QueryString.GetValues(i);
            //    //        strURL += keys[i] + '=' + values[0] + '&';
            //    //    }
            //    //    if (strURL.Length > 0)
            //    //    {
            //    //        strURL = strURL.Remove(strURL.LastIndexOf('&'));
            //    //    }
            //    //}
            //    ReturnUrl = HttpContext.Current.Request.QueryString["ReturnUrl"].ToString();
            //    if (HttpContext.Current.Request.QueryString["pgid"] != null)
            //    {
            //        pageID += "pgid=" + HttpContext.Current.Request.QueryString["pgid"].ToString();
            //    }
            //    if (HttpContext.Current.Request.QueryString["ActInd"] != null)
            //    {
            //        ActiveIndex += "ActInd=" + HttpContext.Current.Request.QueryString["ActInd"].ToString();
            //    }
            //    ReturnUrl += "?" + (pageID != string.Empty ? "&" + pageID : "") + (ActiveIndex != string.Empty ? "&" + ActiveIndex : "");
            //}
            //else
            //{
            //    if (IsUseFriendlyUrls)
            //    {
            //        if (string.IsNullOrEmpty(GetPortalSEOName))
            //        {
            //            Response.Redirect("~/" + GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
            //        }
            //        else
            //        {
            //            Response.Redirect("~/portal/" + GetPortalSEOName + "/" + GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
            //        }
            //    }
            //    else
            //    {
            //        Response.Redirect(ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage)));
            //    }
            //}
            //Response.Redirect(ReturnUrl);
        }

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
            Session["SiteMapProvider"] = PortalSEOName.ToLower().Trim() + "SiteMapProvider";
            Session["SiteMapProviderAdmin"] = PortalSEOName.ToLower().Trim() + "SiteMapProviderAdmin";
            Session["SiteMapProviderFooter"] = PortalSEOName.ToLower().Trim() + "SiteMapProviderFooter";
            Session["SageFrame.PortalSEOName"] = PortalSEOName.ToLower().Trim();
            Session["SageFrame.PortalID"] = portalID;
            suc.SetPortalID(portalID);
            SetPortalID(portalID);

            int storeID = portalID;
            //TODO:: set StoreID According the URL HERE
            Session["SageFrame.StoreID"] = storeID;
            suc.SetStoreID(storeID);
            SetStoreID(storeID);
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
                PortalSettingDataContext portaldb = new PortalSettingDataContext(SystemSetting.SageFrameConnectionString);
                var portals = portaldb.sp_PortalGetList();
                foreach (sp_PortalGetListResult portal in portals)
                {
                    hstAll.Add(portal.SEOName.ToLower().Trim(), portal.PortalID);
                }
            }
            HttpContext.Current.Cache.Insert("Portals", hstAll);
            return hstAll;
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
       
    }
}

