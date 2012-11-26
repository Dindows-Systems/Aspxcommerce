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
using System.Web.Security;
using System.Globalization;
using System.Threading;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using SageFrame.Web.Common.SEO;
using System.Web.UI.WebControls;
using SageFrame.Web.Utilities;
using System.Collections;
using SageFrame.Web;
using SageFrame.Shared;
using SageFrame.ErrorLog;
using System.Text;
using SageFrame.Utilities;
using SageFrame.Common;
using System.IO;
using System.Xml;
using SageFrame.Common.Shared;
using SageFrame.Core;
using SageFrame.Templating;


namespace SageFrame.Framework
{
    public enum DivClassType
    {
        HeaderCenter,
        HeaderLeftCenter,
        HeaderCenterRight,
        HeaderLeftCenterRight,
        Center,
        LeftCenter,
        CenterRight,
        LeftCenterRight,
        FooterCenter,
        FooterLeftCenter,
        FooterCenterRight,
        FooterLeftCenterRight
    }
    public class PageBase : System.Web.UI.Page
    {
        #region "Public Properties"

        //string Comment = "";
        string Description = "";
        string KeyWords = "";
        string Copyright = "";
        string Generator = "";
        string Author = "";
        string SageTitle = "";
        string Refresh = "";
        string Robots = "";
        string ResourceType = "";
        string Distribution = "";
        string RevisitAfter = "";
        string PageEnter = "";
        #endregion
        #region Private Property
		    int PortalID = 1;
            string PortalSEOName = string.Empty;
			int StoreID = 1;
            int CustomerID = 0;
	    #endregion
        public virtual void ShowMessage(string MessageTitle, string Message, string CompleteMessage, bool isSageAsyncPostBack, SageMessageType MessageType)
        {

        }

        public string GetCurrentCultureName
        {
            get
            {                
                return CultureInfo.CurrentCulture.Name; 
            }
        }

        protected override void InitializeCulture()
        {
            //string preferredCulture = null;
            //string preferredUICulture = null;
            //if (preferredUICulture != null && preferredCulture != null)
            //{
            //    SetCulture(preferredUICulture, preferredCulture);
            //}
            //else
            //{
            //    SetCulture("en-US", "en-US");
            //}
            string IsInstalled = Config.GetSetting("IsInstalled").ToString();
            string InstallationDate = Config.GetSetting("InstallationDate").ToString();
            if ((IsInstalled != "" && IsInstalled != "false") && InstallationDate != "")
            {
                SageFrameConfig sfConf = new SageFrameConfig();
                string portalCulture = sfConf.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultLanguage);
                if (Session["SageUICulture"] != null)
                {
                    Thread.CurrentThread.CurrentUICulture = (CultureInfo)Session["SageUICulture"];
                }
                else
                {
                    CultureInfo newUICultureInfo = new CultureInfo(portalCulture);
                    Thread.CurrentThread.CurrentUICulture = newUICultureInfo;
                    Session["SageUICulture"] = newUICultureInfo;
                }
                if (Session["SageCulture"] != null)
                {
                    Thread.CurrentThread.CurrentCulture = (CultureInfo)Session["SageCulture"];
                }
                else
                {
                    CultureInfo newCultureInfo = new CultureInfo(portalCulture);
                    Thread.CurrentThread.CurrentCulture = newCultureInfo;
                    Session["SageCulture"] = newCultureInfo;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect(ResolveUrl("~/Install/InstallWizard.aspx"));
            }
            

            base.InitializeCulture();
        }

        protected void SetCulture(string name, string locale)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(name);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);
            Session["SageUICulture"] = Thread.CurrentThread.CurrentUICulture;
            Session["SageCulture"] = Thread.CurrentThread.CurrentCulture;
        }

        public static void SetCultureInfo(string name, string locale)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(name);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);
            HttpContext.Current.Session["SageUICulture"] = Thread.CurrentThread.CurrentUICulture;
            HttpContext.Current.Session["SageCulture"] = Thread.CurrentThread.CurrentCulture;
        }


        protected string GetCurrentUICulture()
        {
            return Thread.CurrentThread.CurrentUICulture.ToString();
        }
        protected string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.ToString();
        }
        public void InitializePage()
        {
            
            #region "Page Meta Section"

            SageFrameConfig sfConfig = new SageFrameConfig();
            SageTitle = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PageTitle);
            Description = sfConfig.GetSettingsByKey(SageFrameSettingKeys.MetaDescription);
            KeyWords = sfConfig.GetSettingsByKey(SageFrameSettingKeys.MetaKeywords);
            Refresh = sfConfig.GetSettingsByKey(SageFrameSettingKeys.MetaRefresh);
            Copyright = sfConfig.GetSettingsByKey(SageFrameSettingKeys.MetaCopyright);
            Generator = sfConfig.GetSettingsByKey(SageFrameSettingKeys.MetaGenerator);
            Author = sfConfig.GetSettingsByKey(SageFrameSettingKeys.MetaAuthor);
            ResourceType = sfConfig.GetSettingsByKey(SageFrameSettingKeys.MetaRESOURCE_TYPE);
            Distribution = sfConfig.GetSettingsByKey(SageFrameSettingKeys.MetaDISTRIBUTION);
            Robots = sfConfig.GetSettingsByKey(SageFrameSettingKeys.MetaRobots);
            PageEnter = sfConfig.GetSettingsByKey(SageFrameSettingKeys.MetaPAGE_ENTER);
            RevisitAfter = sfConfig.GetSettingsByKey(SageFrameSettingKeys.MetaREVISIT_AFTER);
            
            SEOHelper.RenderTitle(this.Page, SageTitle, false, true, this.GetPortalID);
            SEOHelper.RenderMetaTag(this.Page, "Refresh", Refresh, true);
            SEOHelper.RenderMetaTag(this.Page, "DESCRIPTION", Description, true);
            SEOHelper.RenderMetaTag(this.Page, "KEYWORDS", KeyWords, true);
            SEOHelper.RenderMetaTag(this.Page, "COPYRIGHT", Copyright, true);
            SEOHelper.RenderMetaTag(this.Page, "GENERATOR", Generator, true);
            SEOHelper.RenderMetaTag(this.Page, "AUTHOR", Author, true);
            SEOHelper.RenderMetaTag(this.Page, "RESOURCE-TYPE", ResourceType, false);
            SEOHelper.RenderMetaTag(this.Page, "DISTRIBUTION", Distribution, false);
            SEOHelper.RenderMetaTag(this.Page, "ROBOTS", Robots, true);
            SEOHelper.RenderMetaTag(this.Page, "REVISIT-AFTER", RevisitAfter, false);
            SEOHelper.RenderMetaTag(this.Page, "PAGE-ENTER", PageEnter, false); 

            #endregion

            #region "Set Site Template"

            ////Set Site Template
            //string TemplateName = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalCssTemplate);
            //string CssTemplatePath = string.Empty;
            //string CssLayoutPath = string.Empty;//"~/Templates/" + TemplateName + "/css/layout.css";
            //if (HttpContext.Current.Request.RawUrl.Contains("/Admin/") || HttpContext.Current.Request.RawUrl.Contains("/Admin.aspx") || HttpContext.Current.Request.RawUrl.Contains("/Super-User/") || HttpContext.Current.Request.RawUrl.Contains("/Super-User.aspx") || HttpContext.Current.Request.RawUrl.Contains("ManageReturnURL=")) 
            //{
            //    CssTemplatePath = "~/Templates/" + TemplateName + "/css/admintemplate.css";
            //}
            //else
            //{
            //    CssTemplatePath = "~/Templates/" + TemplateName + "/css/template.css";
            //    CssLayoutPath = "~/Templates/" + TemplateName + "/css/layout.css";
            //}
            //CssPath = Page.ResolveUrl(CssPath);
            //SEOHelper.RenderCSSPath(this.Page, "SageFrameCSSTemplate", ResolveUrl(CssTemplatePath), true);
            //if (!string.IsNullOrEmpty(CssLayoutPath))
            //{
            //    SEOHelper.RenderCSSPath(this.Page, "SageFrameCSSLayout", ResolveUrl(CssLayoutPath), true);
            //} 

            #endregion

            #region "Not in use"

            //register SageFrame ClientAPI scripts
            ////Page.ClientScript.RegisterClientScriptInclude("sageframecore", ResolveUrl("~/js/SageFrameCorejs/sageframecore.js"));
            ////Page.ClientScript.RegisterClientScriptInclude("jquery", ResolveUrl("~/js/jquery-1.2.6.min.js"));
            ////Page.ClientScript.RegisterClientScriptInclude("bannerjquery", ResolveUrl("~/js/jquery.cycle.all.js"));
            ////Page.ClientScript.RegisterClientScriptInclude("MessageTemplate", ResolveUrl("~/js/SageFrameCorejs/MessageTemplate.js"));
            //Page.ClientScript.RegisterClientScriptInclude("IE8", ResolveUrl("~/js/SageFrameCorejs/IE8.js"));
            //LitSageScript 

            #endregion

            #region "Script Section"


            //Literal LitSageScript = Page.Header.FindControl("LitSageScript") as Literal;
            //Literal SageFrameModuleCSSlinks = Page.Header.FindControl("SageFrameModuleCSSlinks") as Literal;
            //StringBuilder strbScripts = new StringBuilder();
            //if (LitSageScript != null && SageFrameModuleCSSlinks != null)
            //{
            //    //strbScripts.Append("<script src=\"" + ResolveUrl("~/js/SageFrameCorejs/sageframecore.js") + "\" type=\"text/javascript\"></script>");
            //    strbScripts.Append("<script src=\"" + ResolveUrl("~/js/jquery-1.4.4.js") + "\" type=\"text/javascript\"></script>");
            //    strbScripts.Append("<script src=\"" + ResolveUrl("~/js/json2.js") + "\" type=\"text/javascript\"></script>");
            //    if (!SageFrameModuleCSSlinks.Text.Contains(strbScripts.ToString()))
            //    {
            //        SageFrameModuleCSSlinks.Text += strbScripts.ToString();
            //    }
            //}

            //Literal LitSageScript = Page.Header.FindControl("LitSageScript") as Literal;
            //Literal SageFrameModuleCSSlinks = Page.Header.FindControl("SageFrameModuleCSSlinks") as Literal;
            //StringBuilder strbScripts = new StringBuilder();
            //if (LitSageScript != null && SageFrameModuleCSSlinks != null)
            //{
            //    strbScripts.Append("<script src=\"" + ResolveUrl("~/js/jquery-1.4.4.js") + "\" type=\"text/javascript\"></script>");
            //    strbScripts.Append("<script src=\"" + ResolveUrl("~/js/json2.js") + "\" type=\"text/javascript\"></script>");
            //    strbScripts.Append("<script src=\"" + ResolveUrl("~/js/SageFrameCorejs/aspxcommercecore.js") + "\" type=\"text/javascript\"></script>");
            //    strbScripts.Append("<script src=\"" + ResolveUrl("~/js/SageFrameCorejs/sageframecore.js") + "\" type=\"text/javascript\"></script>");
            //    if (!SageFrameModuleCSSlinks.Text.Contains(strbScripts.ToString()))
            //    {
            //        SageFrameModuleCSSlinks.Text += strbScripts.ToString();
            //    }

            //} 


            //} 

            #endregion

            if (!IsPostBack)
            {
                ProcessHttpRequestValidationException();
            }
        }


        public void SetTemplateCss()
        {
            string TemplateName = GetActiveTemplate;
            string CssTemplatePath = string.Empty;
            string cssColoredTemplate = "";
            string CssLayoutPath = string.Empty;//"~/Templates/" + TemplateName + "/css/layout.css";
            if (HttpContext.Current.Request.RawUrl.Contains("/Admin/") || HttpContext.Current.Request.RawUrl.Contains("/Sagin/") || HttpContext.Current.Request.RawUrl.Contains("/Admin.aspx") || HttpContext.Current.Request.RawUrl.Contains("/Super-User/") || HttpContext.Current.Request.RawUrl.Contains("/Super-User.aspx") || HttpContext.Current.Request.RawUrl.Contains("ManageReturnURL="))
            {
                //string adminTheme = GetActiveAdminTheme;

                CssTemplatePath = "~/Templates/Default/css/admintemplate.css";

            }
            else
            {

                List<CssScriptInfo> lstModuleResources = new List<CssScriptInfo>();
                StringBuilder modulecss = new StringBuilder();
                lstModuleResources = HttpContext.Current.Session["ModuleCss"] as List<CssScriptInfo>;
                CssTemplatePath = !IsHandheld() ? "~/Templates/" + TemplateName + "/css/template.css" : "~/Templates/" + TemplateName + "/css/handheld/template.css";
                CssLayoutPath = !IsHandheld() ? "~/Templates/" + TemplateName + "/css/layout.css" : "~/Templates/" + TemplateName + "/css/handheld/layout.css";
            }

            SEOHelper.RenderCSSPath(this.Page, "SageFrameCSSTemplate", ResolveUrl(CssTemplatePath), true);
            SEOHelper.RenderCSSPath(this.Page, "SageFrameCSSLayout", ResolveUrl(cssColoredTemplate), true);
            if (!string.IsNullOrEmpty(CssLayoutPath))
            {
                SEOHelper.RenderCSSPath(this.Page, "SageFrameCSSLayout", ResolveUrl(CssLayoutPath), true);
            }


        }

        public string GetTemplateCssPath()
        {
            string CssTemplatePath = "";
            List<CssScriptInfo> lstModuleResources = new List<CssScriptInfo>();
            StringBuilder modulecss = new StringBuilder();
            lstModuleResources = HttpContext.Current.Session["ModuleCss"] as List<CssScriptInfo>;
            CssTemplatePath = !IsHandheld() ? "~/Templates/" + GetActiveTemplate + "/css/template.css" : "~/Templates/" + GetActiveTemplate + "/css/handheld/template.css";
            return Server.MapPath(CssTemplatePath);

        }

        public string GetTemplateLayoutCssPath()
        {
            string CssTemplatePath = "";
            List<CssScriptInfo> lstModuleResources = new List<CssScriptInfo>();
            StringBuilder modulecss = new StringBuilder();
            lstModuleResources = HttpContext.Current.Session["ModuleCss"] as List<CssScriptInfo>;
            CssTemplatePath = !IsHandheld() ? "~/Templates/" + GetActiveTemplate + "/css/layout.css" : "~/Templates/" + GetActiveTemplate + "/css/handheld/layout.css";
            return Server.MapPath(CssTemplatePath);
        }

        public string GetAdminTemplatePath()
        {
            return (Server.MapPath("~/Templates/Default/css/admintemplate.css"));
        }

        public bool IsHandheld()
        {
            string strUserAgent = Request.UserAgent.ToString().ToLower();
            bool status = false;
            if (strUserAgent != null)
            {
                if (Request.Browser.IsMobileDevice == true || strUserAgent.Contains("iphone") ||
                    strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                    strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                    strUserAgent.Contains("palm"))
                {
                    status = true;
                }
            }
            return status;
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!(Request.CurrentExecutionFilePath.Contains(".gif") || Request.CurrentExecutionFilePath.Contains(".jpg") || Request.CurrentExecutionFilePath.Contains(".png")))
            {
                base.OnPreRender(e);
                Control ctlphdHeaderLeftContainer = this.FindControl("HeaderLeftPane");
                Control ctlphdHeaderRightContainer = this.FindControl("HeaderRightPane");
                PlaceHolder phdHeaderLeftContainer = (PlaceHolder)ctlphdHeaderLeftContainer;
                PlaceHolder phdHeaderRightContainer = (PlaceHolder)ctlphdHeaderRightContainer;
                Control ctlphdLeftContainer = this.FindControl("LeftPane");
                Control ctlphdRightContainer = this.FindControl("RightPane");
                PlaceHolder phdLeftContainer = (PlaceHolder)ctlphdLeftContainer;
                PlaceHolder phdRightContainer = (PlaceHolder)ctlphdRightContainer;
                Control ctlphdFooterLeftContainer = this.FindControl("FooterLeftPane");
                Control ctlphdFooterRightContainer = this.FindControl("FooterRightPane");
                PlaceHolder phdFooterLeftContainer = (PlaceHolder)ctlphdFooterLeftContainer;
                PlaceHolder phdFooterRightContainer = (PlaceHolder)ctlphdFooterRightContainer;
                #region "Conditions"

                if (phdHeaderLeftContainer != null)
                {
                    if (phdHeaderLeftContainer.HasControls())
                    {
                        if (phdHeaderRightContainer.HasControls())
                        {
                            SetCssClasses("divHeaderContent", DivClassType.HeaderLeftCenterRight);
                        }
                        else
                        {
                            SetCssClasses("divHeaderContent", DivClassType.HeaderLeftCenter);
                        }
                    }
                    else
                    {
                        if (phdHeaderRightContainer.HasControls())
                        {
                            SetCssClasses("divHeaderContent", DivClassType.HeaderCenterRight);
                        }
                        else
                        {
                            SetCssClasses("divHeaderContent", DivClassType.HeaderCenter);
                        }
                    }
                }
                if (phdLeftContainer != null)
                {
                    if (phdLeftContainer.HasControls())
                    {
                        if (phdRightContainer.HasControls())
                        {
                            SetCssClasses("divCenterContent", DivClassType.LeftCenterRight);
                        }
                        else
                        {
                            SetCssClasses("divCenterContent", DivClassType.LeftCenter);
                        }
                    }
                    else
                    {
                        if (phdRightContainer.HasControls())
                        {
                            SetCssClasses("divCenterContent", DivClassType.CenterRight);
                        }
                        else
                        {
                            SetCssClasses("divCenterContent", DivClassType.Center);
                        }
                    }
                }
                if (phdFooterLeftContainer != null)
                {
                    if (phdFooterLeftContainer.HasControls())
                    {
                        if (phdFooterRightContainer.HasControls())
                        {
                            SetCssClasses("divFooterContent", DivClassType.FooterLeftCenterRight);
                        }
                        else
                        {
                            SetCssClasses("divFooterContent", DivClassType.FooterLeftCenter);
                        }
                    }
                    else
                    {
                        if (phdFooterRightContainer.HasControls())
                        {
                            SetCssClasses("divFooterContent", DivClassType.FooterCenterRight);
                        }
                        else
                        {
                            SetCssClasses("divFooterContent", DivClassType.FooterCenter);
                        }
                    }
                } 

                #endregion
                SetGoogleAnalytics();
                    LoadModuleJs();
                    LoadModuleCss();
            }
        }

        public bool IsAdmin()
        {
            bool status = HttpContext.Current.Request.RawUrl.Contains("/Admin/") || HttpContext.Current.Request.RawUrl.Contains("/Sagin/") || HttpContext.Current.Request.RawUrl.Contains("/Admin.aspx") || HttpContext.Current.Request.RawUrl.Contains("/Super-User/") || HttpContext.Current.Request.RawUrl.Contains("/Super-User.aspx") || HttpContext.Current.Request.RawUrl.Contains("ManageReturnURL=");
            return status;
        }

        public void LoadModuleCss()
        {
            List<CssScriptInfo> lstModuleResources = new List<CssScriptInfo>();
            StringBuilder modulecss = new StringBuilder();
            lstModuleResources = HttpContext.Current.Session["ModuleCss"] as List<CssScriptInfo>;
            List<KeyValue> lstCssInclude = new List<KeyValue>();
            List<string> lstCss = new List<string>();
            if (lstModuleResources != null)
            {
                foreach (CssScriptInfo css in lstModuleResources)
                {
                    lstCss.Add(css.ModuleName.ToLower());
                    string fullPath_module = Server.MapPath(string.Format("~/{0}", css.Path));
                                    
                    ///Strategy 3-Priority-3:Check at the module level(the default fallback)
                    if (Directory.Exists(fullPath_module))
                    {
                        ///Check to see if the file exists in the root level
                        if (File.Exists(string.Format("{0}/{1}", fullPath_module, css.FileName)))
                        {
                            lstCssInclude.Add(new KeyValue(string.Format("~/{0}/{1}", css.Path, css.FileName), css.Path));
                        }
                        ///Check to see if the file exists in the css folder
                        else if (File.Exists(string.Format("{0}/css/{1}", fullPath_module, css.FileName)))
                        {
                            lstCssInclude.Add(new KeyValue(string.Format("~/{0}/{1}", css.Path, css.FileName), css.Path));
                        }
                    }
                }

                ///Check for Compression Mode
                SageFrameConfig pagebase = new SageFrameConfig();
                bool IsCompressCss = bool.Parse(pagebase.GetSettingsByKey(SageFrameSettingKeys.OptimizeCss));               
                if (IsCompressCss)
                {
                    ///1.Loop through the list
                    ///2.Read the Css File
                    ///3.Rewrite the Image Paths in Css Files
                    ///4.Compress the Css file 
                    ///5.Include it in the Css Literal

                    lstCss.Insert(0, IsAdmin() ? "admintemplate" : "template");
                    string[] cssArr = lstCss.ToArray().Distinct().ToArray();

                    ///Check cache and refresh it if the files optimized folder do not exist
                    ///Synchronize the cache and the map data in the files
                    Hashtable hst = new Hashtable();
                    if (HttpContext.Current.Cache["SageFrameCss"] != null)
                    {
                        hst = (Hashtable)HttpContext.Current.Cache["SageFrameCss"];
                        Hashtable hstNew = new Hashtable();
                        foreach (string modulekey in hst.Keys)
                        {
                            string file = string.Format("{0}.css", hst[modulekey].ToString());
                            if (File.Exists(Server.MapPath(string.Format("~/Optimized/{0}", file))))
                            {
                                hstNew.Add(modulekey, hst[modulekey].ToString());
                            }
                        }
                        HttpContext.Current.Cache["SageFrameCss"] = hstNew;
                    }
                    ///Read the map file and check if the css for this combination already exists

                    if (HttpContext.Current.Cache["SageFrameCss"] != null)
                    {
                        hst = (Hashtable)HttpContext.Current.Cache["SageFrameCss"];
                    }
                    else
                    {
                        XmlDocument doc = SageFrame.Templating.xmlparser.XmlHelper.LoadXMLDocument(Server.MapPath("~/Optimized/map_css.xml"));
                        XmlNode xnresourcemap = doc.SelectSingleNode("resourcemaps");
                        XmlNodeList xnlist = xnresourcemap.ChildNodes;
                        foreach (XmlNode node in xnlist)
                        {
                            string modules = node.SelectSingleNode("modules").InnerText;
                            string map = node.SelectSingleNode("map").InnerText;
                            if (modules != "" && !hst.Contains(modules))
                                hst.Add(modules, map);
                        }

                    }

                    bool IsExists = false;
                    string optimizedcss = string.Empty;
                    foreach (string modulekey in hst.Keys)
                    {
                        string modules = modulekey;
                        string[] modulesArr = modules.Split(',');
                        if (ArrayHelper.ArraysEqual<string>(cssArr, modulesArr))
                        {
                            IsExists = true;
                            optimizedcss = string.Format("{0}.css", hst[modulekey].ToString());
                            break;
                        }
                    }
                    if (IsExists)
                    {
                        IsExists = File.Exists(Server.MapPath(string.Format("~/Optimized/{0}", optimizedcss)));
                    }
                    if (!IsExists)
                    {
                        string uniqueid = GenerateUniqueId();
                        XmlDocument doc = SageFrame.Templating.xmlparser.XmlHelper.LoadXMLDocument(Server.MapPath("~/Optimized/map_css.xml"));
                        XmlNode xnresourcemap = doc.SelectSingleNode("resourcemaps");
                        string optimized_css_path = Server.MapPath(string.Format("~/Optimized/{0}.css", uniqueid));
                        ///Write the combination into the map file
                        XmlElement resourcemap = doc.CreateElement("resourcemap");
                        XmlElement modules = doc.CreateElement("modules");
                        XmlElement map = doc.CreateElement("map");
                        modules.InnerText = string.Join(",", cssArr);
                        map.InnerText = uniqueid;
                        resourcemap.AppendChild(modules);
                        resourcemap.AppendChild(map);
                        xnresourcemap.AppendChild(resourcemap);
                        if (cssArr.Length > 0 && !hst.Contains(string.Join(",", cssArr)))
                        {
                            doc.Save(Server.MapPath("~/Optimized/map_css.xml"));

                            ///Hashtable+Cache needs to be reset

                            hst.Add(string.Join(",", cssArr), uniqueid);
                            HttpContext.Current.Cache["SageFrameCss"] = hst;
                            ///Only when the optimized file does not exists..else the development mode needs to be on to recreate the css file
                            if (!File.Exists(optimized_css_path))
                            {
                                using (StreamWriter sw = new StreamWriter(optimized_css_path))
                                {
                                    ///Read the template.css file
                                    string compressedcss = "";
                                    string uncompcss = "";
                                    string templatecsspath = IsAdmin() ? GetAdminTemplatePath() : GetTemplateCssPath();
                                    string imagerewrite = IsAdmin() ? "/Templates/Default/css/" : string.Format("/Templates/{0}/css", GetActiveTemplate);
                                    string cssText = IsAdmin() ? "admintemplate.css" : "template.css";

                                        using (StreamReader rdr = new StreamReader(templatecsspath))
                                        {
                                            uncompcss = rdr.ReadToEnd();
                                        }
                                        compressedcss = CssJscriptOptimizer.Minifiers.CssMinifier.CssMinify(uncompcss);
                                        compressedcss = CssJscriptOptimizer.Minifiers.CssMinifier.RewriteCssImagePath(compressedcss, imagerewrite, Request.ApplicationPath == "/" ? "" : Request.ApplicationPath, "images");
                                        sw.Write("\n");
                                        sw.Write("/*-----'" + cssText + "'----*/");
                                        sw.Write("\n");
                                        sw.Write(compressedcss);
                                        sw.Write("\n");

                                    //Read layout.css file
                                    if (!IsAdmin())
                                    {
                                        using (StreamReader rdr = new StreamReader(GetTemplateLayoutCssPath()))
                                        {
                                            uncompcss = rdr.ReadToEnd();
                                        }
                                        compressedcss = CssJscriptOptimizer.Minifiers.CssMinifier.CssMinify(uncompcss);
                                        compressedcss = CssJscriptOptimizer.Minifiers.CssMinifier.RewriteCssImagePath(compressedcss, imagerewrite, Request.ApplicationPath == "/" ? "" : Request.ApplicationPath, "images");
                                        sw.Write("\n");
                                        sw.Write("/*-----layout.css----*/");
                                        sw.Write("\n");
                                        sw.Write(compressedcss);
                                        sw.Write("\n");
                                    }

                                    ///Read the module files                               
                                    foreach (KeyValue cssfile in lstCssInclude)
                                    {
                                        using (StreamReader rdr = new StreamReader(Server.MapPath(cssfile.Key)))
                                        {
                                            uncompcss = rdr.ReadToEnd();
                                        }
                                        compressedcss = CssJscriptOptimizer.Minifiers.CssMinifier.CssMinify(uncompcss);
                                        compressedcss = CssJscriptOptimizer.Minifiers.CssMinifier.RewriteCssImagePath(compressedcss, string.Format("{0}", cssfile.Value), Request.ApplicationPath == "/" ? "" : Request.ApplicationPath, "images");
                                        sw.Write("\n");
                                        sw.Write("/*-----" + Path.GetFileName(cssfile.Key) + "----*/");
                                        sw.Write("\n");
                                        sw.Write(compressedcss);
                                        sw.Write("\n");
                                    }
                                }

                                Literal SageFrameModuleCSSlinks = this.Page.FindControl("SageFrameModuleCSSlinks") as Literal;
                                if (SageFrameModuleCSSlinks != null)
                                {
                                    string linkText = "<link href=\"" + Page.ResolveUrl(string.Format("~/Optimized/{0}.css", uniqueid)) + "\" rel=\"stylesheet\" type=\"text/css\" />";
                                    SageFrameModuleCSSlinks.Text += linkText;
                                }
                            }
                        }
                        else
                        {
                            Literal SageFrameModuleCSSlinks = this.Page.FindControl("SageFrameModuleCSSlinks") as Literal;
                            if (SageFrameModuleCSSlinks != null)
                            {
                                string linkText = "<link href=\"" + Page.ResolveUrl("~/Optimized/" + optimizedcss) + "\" rel=\"stylesheet\" type=\"text/css\" />";
                                SageFrameModuleCSSlinks.Text += linkText;
                            }
                        }


                    }
                    else
                    {
                        Literal SageFrameModuleCSSlinks = this.Page.FindControl("SageFrameModuleCSSlinks") as Literal;
                        if (SageFrameModuleCSSlinks != null)
                        {
                            string linkText = "<link href=\"" + Page.ResolveUrl("~/Optimized/" + optimizedcss) + "\" rel=\"stylesheet\" type=\"text/css\" />";
                            SageFrameModuleCSSlinks.Text += linkText;
                        }
                    }
                }
                else
                {
                    SetTemplateCss();
                    foreach (KeyValue cssfile in lstCssInclude)
                    {
                        AddModuleCssToPage(cssfile.Key);
                    }
                }
            }

        }

        public bool IsUserLoggedIn()
        {
            bool IsLoggedIn = false;
            if (HttpContext.Current.User != null)
            {
                MembershipUser user = Membership.GetUser();
                if (user != null)
                {
                    IsLoggedIn = true;

                }
            }
            return IsLoggedIn;
        }

        private string GenerateUniqueId()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public void LoadModuleJs()
        {
            List<CssScriptInfo> lstJsColl = new List<CssScriptInfo>();
            lstJsColl.AddRange(GetCorejsFiles());
            List<CssScriptInfo> lstJsTop = new List<CssScriptInfo>();
            List<CssScriptInfo> lstJsBottom = new List<CssScriptInfo>();
            if (HttpContext.Current.Session["ModuleJs"] != null)
            {
                lstJsColl.AddRange(HttpContext.Current.Session["ModuleJs"] as List<CssScriptInfo>);
            }

            foreach (CssScriptInfo script in lstJsColl)
            {
                if (script.Position == 0)
                {
                    lstJsTop.Add(script);
                }
                else
                {
                    lstJsBottom.Add(script);
                }
            }
            OptimizeJs(lstJsTop, 0);
            OptimizeJs(lstJsBottom, 1);
        }

        private void OptimizeJs(List<CssScriptInfo> lstJsColl, int Mode)
        {
            Literal LitSageScript = Mode == 1 ? Page.Header.FindControl("LitSageScript") as Literal : Page.Header.FindControl("SageFrameModuleCSSlinks") as Literal;
            List<string> lstJs = new List<string>();
            foreach (CssScriptInfo js in lstJsColl)
            {
                lstJs.Add(js.ModuleName);
            }
            //lstJs.Insert(0, IsAdmin() ? "admin" : "portal");
            ///Check for Compression Mode
            SageFrameConfig pagebase = new SageFrameConfig();
            bool IsCompressJs = bool.Parse(pagebase.GetSettingsByKey(SageFrameSettingKeys.OptimizeJs));         
            if (IsCompressJs)
            {

                Hashtable hst = new Hashtable();

                ///Check cache and refresh it if the files optimized folder do not exist              
                if (HttpContext.Current.Cache["SageFrameJs"] != null)
                {
                    hst = (Hashtable)HttpContext.Current.Cache["SageFrameJs"];
                    Hashtable hstNew = new Hashtable();
                    foreach (string modulekey in hst.Keys)
                    {
                        string file = string.Format("{0}.js", hst[modulekey].ToString());
                        if (File.Exists(Server.MapPath(string.Format("~/Optimized/{0}", file))))
                        {
                            hstNew.Add(modulekey, hst[modulekey].ToString());
                        }
                    }
                    HttpContext.Current.Cache["SageFrameJs"] = hstNew;
                }


                if (HttpContext.Current.Cache["SageFrameJs"] != null)
                {
                    hst = (Hashtable)HttpContext.Current.Cache["SageFrameJs"];
                }
                else
                {
                    XmlDocument doc = SageFrame.Templating.xmlparser.XmlHelper.LoadXMLDocument(Server.MapPath("~/Optimized/map_js.xml"));
                    XmlNode xnresourcemap = doc.SelectSingleNode("resourcemaps");
                    XmlNodeList xnlist = xnresourcemap.ChildNodes;
                    foreach (XmlNode node in xnlist)
                    {
                        string modules = node.SelectSingleNode("modules").InnerText;
                        string map = node.SelectSingleNode("map").InnerText;
                        if (modules != "" && !hst.Contains(modules))
                            hst.Add(modules, map);
                    }

                }
                string[] jsArr = lstJs.ToArray().Distinct().ToArray();
                ///Read the map file and check if the css for this combination already exists

                bool IsExists = false;
                string optimizedjs = string.Empty;
                foreach (string modulekey in hst.Keys)
                {
                    string modules = modulekey;
                    string[] modulesArr = modules.Split(',');
                    if (ArrayHelper.ArraysEqual<string>(jsArr, modulesArr))
                    {
                        IsExists = true;
                        optimizedjs = string.Format("{0}.js", hst[modulekey].ToString());
                        break;
                    }
                }
                if (IsExists)
                {
                    IsExists = File.Exists(Server.MapPath(string.Format("~/Optimized/{0}", optimizedjs)));
                }
                if (!IsExists)
                {
                    string uniqueid = GenerateUniqueId();
                    XmlDocument doc = SageFrame.Templating.xmlparser.XmlHelper.LoadXMLDocument(Server.MapPath("~/Optimized/map_js.xml"));
                    XmlNode xnresourcemap = doc.SelectSingleNode("resourcemaps");
                    string optimized_js_path = Server.MapPath(string.Format("~/Optimized/{0}.js", uniqueid));
                    ///Write the combination into the map file
                    XmlElement resourcemap = doc.CreateElement("resourcemap");
                    XmlElement modules = doc.CreateElement("modules");
                    XmlElement map = doc.CreateElement("map");
                    modules.InnerText = string.Join(",", jsArr);
                    map.InnerText = uniqueid;
                    resourcemap.AppendChild(modules);
                    resourcemap.AppendChild(map);
                    xnresourcemap.AppendChild(resourcemap);
                    if (jsArr.Length > 0 && !hst.Contains(string.Join(",", jsArr)))
                    {
                        doc.Save(Server.MapPath("~/Optimized/map_js.xml"));
                        ///Hashtable+Cache needs to be reset

                        hst.Add(string.Join(",", jsArr), uniqueid);
                        HttpContext.Current.Cache["SageFrameJs"] = hst;
                        ///Only when the optimized file does not exists..else the development mode needs to be on to recreate the css file
                        if (!File.Exists(optimized_js_path))
                        {
                            using (StreamWriter sw = new StreamWriter(optimized_js_path))
                            {

                                foreach (CssScriptInfo obj in lstJsColl)
                                {
                                    string uncompjs = "";
                                    string fullPath = string.Format("{0}/{1}/{2}", Request.PhysicalApplicationPath, obj.Path, obj.FileName);
                                    using (StreamReader rdr = new StreamReader(fullPath))
                                    {
                                        uncompjs = rdr.ReadToEnd();
                                    }
                                    string compressedjs = CssJscriptOptimizer.Minifiers.JsMinifier.GetMinifiedCode(uncompjs);
                                    sw.Write("\n");
                                    sw.Write("/*-----" + obj.FileName + "----*/");
                                    sw.Write("\n");
                                    sw.Write(compressedjs);
                                    sw.Write("\n");
                                }
                            }

                            string js = "<script src=\"" + ResolveUrl(string.Format("~/Optimized/{0}.js", uniqueid)) + "\" type=\"text/javascript\"></script>";
                            if (LitSageScript != null)
                            {
                                LitSageScript.Text += js;
                            }

                        }
                    }
                    else
                    {
                        string js = "<script src=\"" + ResolveUrl(string.Format("~/Optimized/{0}", optimizedjs)) + "\" type=\"text/javascript\"></script>";
                        if (LitSageScript != null)
                        { LitSageScript.Text += js; }
                    }
                }
                else
                {
                    string js = "<script src=\"" + ResolveUrl(string.Format("~/Optimized/{0}", optimizedjs)) + "\" type=\"text/javascript\"></script>";
                     if (LitSageScript != null)
                    { LitSageScript.Text += js; }
                }
            }
            else
            {
                foreach (CssScriptInfo obj in lstJsColl)
                {
                    string js = "<script src=\"" + ResolveUrl(string.Format("~/{0}/{1}", obj.Path, obj.FileName)) + "\" type=\"text/javascript\"></script>";
                    if (LitSageScript != null)
                    { LitSageScript.Text += js; }
                }
            }
        }

        private List<CssScriptInfo> GetCorejsFiles()
        {
            return (CoreJs.GetList(IsAdmin(), IsUserLoggedIn()));
        }
       
        private void SetGoogleAnalytics()
        {
            try
            {
                if (!Request.RawUrl.Contains("Admin") || !Request.RawUrl.Contains("Super-User"))
                {

                    Hashtable hst = new Hashtable();
                    if (HttpContext.Current.Cache["SageGoogleAnalytics"] != null)
                    {
                        hst = (Hashtable)HttpContext.Current.Cache["SageGoogleAnalytics"];
                    }
                    else
                    {
                        SettingProvider sp = new SettingProvider();
                        List<GoogleAnalyticsInfo> objList = sp.GetGoogleAnalyticsActiveOnlyByPortalID(GetPortalID);
                        foreach (GoogleAnalyticsInfo objl in objList)
                        {
                            hst.Add("SageGoogleAnalytics_" + objl.PortalID, objl.GoogleJSCode);
                        }
                        HttpContext.Current.Cache.Insert("SageGoogleAnalytics", hst);
                    }
                    if (hst != null && hst.Count > 0 && hst.ContainsKey("SageGoogleAnalytics_" + GetPortalID))
                    {
                        Literal LitSageScript = Page.Header.FindControl("LitSageScript") as Literal;
                        if (LitSageScript != null)
                        {
                            string strGoogleJS = hst["SageGoogleAnalytics_" + GetPortalID].ToString();
                            if (!strGoogleJS.Contains("<script type=\"text/javascript\">"))
                            {
                                strGoogleJS = "<script type=\"text/javascript\">" + strGoogleJS + "</script>";
                            }
                            LitSageScript.Text += strGoogleJS;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void SetCssClasses(string divID,DivClassType divClassType)
        {
            Control ctl = this.FindControl(divID);
            if (ctl != null)
            {
                HtmlGenericControl div = (HtmlGenericControl)ctl;
                switch (divClassType)
                {
                    case DivClassType.HeaderCenter:
                        div.Attributes.Add("class", "cssClassHeaderWrapperCenter");
                        break;
                    case DivClassType.HeaderLeftCenter:
                        div.Attributes.Add("class", "cssClassHeaderWrapperLeftCenter");
                        break;
                    case DivClassType.HeaderCenterRight:
                        div.Attributes.Add("class", "cssClassHeaderWrapperCenterRight");
                        break;
                    case DivClassType.HeaderLeftCenterRight:
                        div.Attributes.Add("class", "cssClassHeaderWrapperLeftCenterRight");
                        break;
                    case DivClassType.Center:
                        div.Attributes.Add("class", "cssClassMasterWrapperCenter");
                        break;
                    case DivClassType.LeftCenter:
                        div.Attributes.Add("class", "cssClassMasterWrapperLeftCenter");
                        break;
                    case DivClassType.CenterRight:
                        div.Attributes.Add("class", "cssClassMasterWrapperCenterRight");
                        break;
                    case DivClassType.LeftCenterRight:
                        div.Attributes.Add("class", "cssClassMasterWrapperLeftCenterRight");
                        break;
                    case DivClassType.FooterCenter:
                        div.Attributes.Add("class", "cssClassFooterWrapperCenter");
                        break;
                    case DivClassType.FooterLeftCenter:
                        div.Attributes.Add("class", "cssClassFooterWrapperLeftCenter");
                        break;
                    case DivClassType.FooterCenterRight:
                        div.Attributes.Add("class", "cssClassFooterWrapperCenterRight");
                        break;
                    case DivClassType.FooterLeftCenterRight:
                        div.Attributes.Add("class", "cssClassFooterWrapperLeftCenterRight");
                        break;
                }
            }
        }

        public int GetPortalID
        {
            get
            {
                try
                {
                    if (Session["SageFrame.PortalID"] != null && Session["SageFrame.PortalID"].ToString() != "")
                    {
                        return int.Parse(Session["SageFrame.PortalID"].ToString());
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch
                {
                    return 1;
                }
            }
        }

        public string GetActiveTemplate
        {
            get
            {
                try
                {
                    if (Session["SageFrame.ActiveTemplate"] != null && Session["SageFrame.ActiveTemplate"].ToString() != "")
                    {
                        return Session["SageFrame.ActiveTemplate"].ToString();
                    }
                    else
                    {
                        //return (TemplateController.GetActiveTemplate(GetPortalID).TemplateSeoName);
                        SageFrameConfig sfc=new SageFrameConfig();
                        return (sfc.GetSettingsByKey(SageFrameSettingKeys.PortalCssTemplate));
                    }
                }
                catch
                {
                    return "Default";
                }
            }
        }

        //public PresetInfo GetPresetDetails
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (Session["SageFrame.ActivePreset"] != null)
        //            {
        //                return Session["SageFrame.ActivePreset"] as PresetInfo;
        //            }
        //            else
        //            {
        //                return (PresetHelper.LoadActivePagePreset(GetActiveTemplate, GetPageSEOName(Request.Url.ToString())));
        //            }
        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //    }
        //}

        public string GetActiveAdminTheme
        {
            get
            {
                try
                {
                    if (Session["SageFrame.AdminTheme"] != null)
                    {
                        return Session["SageFrame.AdminTheme"].ToString();
                    }
                    else
                    {
                        return (ThemeHelper.GetAdminTheme(GetPortalID, GetUsername));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void SetActiveTemplate(string ActiveTemplate)
        {
            Session["SageFrame.ActiveTemplate"] = ActiveTemplate;
        }

        public void SetPortalID(int portalID)
        {
            PortalID = portalID;
		}

        public int GetStoreID
        {
            get
            {
                try
                {
                    if (Session["SageFrame.StoreID"] != null && Session["SageFrame.StoreID"].ToString() != "")
                    {
                        return int.Parse(Session["SageFrame.StoreID"].ToString());
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch
                {
                    return 1;
                }
            }
        }

        public void SetStoreID(int storeID)
        {
            StoreID = storeID;
        }

        public System.Nullable<Int32> GetCustomerID
        {
            get
            {
                try
                {
                    if (Session["SageFrame.CustomerID"] != null && Session["SageFrame.CustomerID"].ToString() != "")
                    {
                        return int.Parse(Session["SageFrame.CustomerID"].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch
                {
                    return 0;
                }
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

        public PlaceHolder LoadControl(string UpdatePanelIDPrefix, bool IsPartialRendring, PlaceHolder ContainerControl, string ControlSrc, string PaneName, string strUserModuleID)
        {
            try
            {
                SageUserControl ctl;
                if (ControlSrc.ToLower().EndsWith(".ascx"))
                {
                    if (IsPartialRendring)
                    {
                        UpdatePanel udp = CreateUpdatePanel(UpdatePanelIDPrefix, UpdatePanelUpdateMode.Always, ContainerControl.Controls.Count);
                        ctl = this.Page.LoadControl("~" + ControlSrc) as SageUserControl;
                        ctl.EnableViewState = true;
                        ctl.SageUserModuleID = strUserModuleID;
                        udp.ContentTemplateContainer.Controls.Add(ctl);
                        ContainerControl.Controls.Add(udp);
                    }
                    else
                    {
                        ctl = this.Page.LoadControl("~" + ControlSrc) as SageUserControl;
                        ctl.EnableViewState = true;
                        ctl.SageUserModuleID = strUserModuleID;
                        ContainerControl.Controls.Add(ctl);
                    }
                }
                else
                {
                }
                return ContainerControl;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
                return ContainerControl;
            }
        }

        public UpdatePanel CreateUpdatePanel(string Prefix, UpdatePanelUpdateMode Upm, int PaneUpdatePanelCount)
        {
            UpdatePanel udp = new UpdatePanel();
            udp.UpdateMode = Upm;
            PaneUpdatePanelCount++;
            udp.ID = "_udp_" + "_" + PaneUpdatePanelCount + Prefix;
            //udp.EnableViewState = false;
            return udp;
        }

        public string ConvetVisibility(bool i)
        {
            string Visible = "Same As Page";
            if (i == false)
            {
                Visible = "Page Editor Only";
            }
            return Visible;
        }        

        private string SettingPortal
        {
            get
            {
                string strPortalName = "default";
                try
                {
                    if (HttpContext.Current.Session["SageFrame.PortalSEOName"] != null)
                    {
                        strPortalName = HttpContext.Current.Session["SageFrame.PortalSEOName"].ToString();
                    }
                }
                catch
                {
                    strPortalName = "default";
                }
                return strPortalName;
            }
        }        

        protected void ProcessHttpRequestValidationException()
        {
            if (HttpContext.Current.Request.QueryString["sagealert"] != null && HttpContext.Current.Request.QueryString["sagealert"].ToString() != string.Empty)
            {
                string ShortAlert = "Malicious activity found, your activity is recorded, if you repeat the same action, you may not able to browse this site in future.";
                ShortAlert += " Your IP Address: " + HttpContext.Current.Request.UserHostAddress;
                ShortAlert += " Mechine Name: " + HttpContext.Current.Request.UserHostName;
                string FullAllert = string.Empty;//"A potentially dangerous Request.Form value was detected from the client. Please remove < and > from your entry and re-submit information";
                ShowMessage(SageMessageTitle.Notification.ToString(), ShortAlert, FullAllert, SageMessageType.Alert);
            }
           
        }

        protected void ProcessException(Exception exc)
        {
            ErrorLogDataContext db = new ErrorLogDataContext(SystemSetting.SageFrameConnectionString);
            System.Nullable<int> inID = 0;
            db.sp_LogInsert(ref inID, (int)SageFrame.Web.SageFrameEnums.ErrorType.AdministrationArea, 11, exc.Message, exc.ToString(),
                HttpContext.Current.Request.UserHostAddress, Request.RawUrl, true, GetPortalID, GetUsername);
            ShowMessage(SageMessageTitle.Exception.ToString(), exc.Message, exc.ToString(), SageMessageType.Error);
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

        public string TemplateName
        {
            get
            {
                SageFrameConfig sfConfig = new SageFrameConfig();
                return sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalCssTemplate);
            }
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
                path = this.Page.ResolveUrl("~/") + "Templates/" + TemplateName + "/images/admin/" + imageName;
            }
            return path;
        }

        public string GetMessageCsssClass(SageMessageType MessageType)
        {
            string cssClass = string.Empty;
            switch (MessageType)
            {
                case SageMessageType.Alert:
                    cssClass = "cssClassAlert";
                    break;
                case SageMessageType.Error:
                    cssClass = "cssClassError";
                    break;
                case SageMessageType.Success:
                    cssClass = "cssClassSuccess";
                    break;                    
            }
            return cssClass;
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

        public string GetPageSEOName(string pagePath)
        {
            string SEOName = string.Empty;
            if (string.IsNullOrEmpty(pagePath))
            {
                SageFrameConfig sfConfig = new SageFrameConfig();
                SEOName = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage);
            }
            else
            {
                string[] pagePaths = pagePath.Split('/');
                SEOName = pagePaths[pagePaths.Length - 1];
                if (string.IsNullOrEmpty(SEOName))
                {
                    SEOName = pagePaths[pagePaths.Length - 2];
                }
                SEOName = SEOName.Replace(".aspx", "");

            }
            return SEOName;
        }        

        public void OverridePageInfo(DataTable dt)
        {
            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                string PageTitle = dt.Rows[0]["Title"].ToString();
                string PageRefresh = dt.Rows[0]["RefreshInterval"].ToString();
                string PageDescription = dt.Rows[0]["Description"].ToString();
                string PageKeyWords = dt.Rows[0]["KeyWords"].ToString();

                if (!string.IsNullOrEmpty(PageTitle))
                    SEOHelper.RenderTitle(this.Page, PageTitle, false, true, this.GetPortalID);

                if (!string.IsNullOrEmpty(PageRefresh) && PageRefresh != "0.00")
                    SEOHelper.RenderMetaTag(this.Page, "Refresh", PageRefresh, true);
                else
                {
                    foreach (Control control in this.Page.Header.Controls)
                        if (control is HtmlMeta)
                        {
                            HtmlMeta meta = (HtmlMeta)control;
                            if (meta.Name.ToLower().Equals("Refresh".ToLower()))
                            {
                                meta.Visible = false;
                            }
                        }
                }

                if (!string.IsNullOrEmpty(PageDescription))
                    SEOHelper.RenderMetaTag(this.Page, "DESCRIPTION", PageDescription, true);

                if (!string.IsNullOrEmpty(PageKeyWords))
                    SEOHelper.RenderMetaTag(this.Page, "KEYWORDS", PageKeyWords, true);

            }
            else
            {
                foreach (Control control in this.Page.Header.Controls)
                    if (control is HtmlMeta)
                    {
                        HtmlMeta meta = (HtmlMeta)control;
                        if (meta.Name.ToLower().Equals("Refresh".ToLower()))
                        {
                            meta.Visible = false;
                        }
                    }
            }
            
        }

        public void AddModuleCssToPage(string ControlSrc, bool IsModuleFolerName)
        {
            string ModuleRootLocation = string.Empty;
            if (IsModuleFolerName)
            {
                ModuleRootLocation = "~/Modules/" + ControlSrc + "/module.css";
            }
            else
            {
                ControlSrc = ControlSrc.Replace("/Modules/", "");
                while (ControlSrc.Contains("/"))
                {
                    ControlSrc = ControlSrc.Remove(ControlSrc.LastIndexOf("/"));
                }
            }
            ModuleRootLocation = "~/Modules/" + ControlSrc + "/module.css";
            string FullPath = Server.MapPath(ModuleRootLocation);
            if(System.IO.File.Exists(FullPath))
            {
                Literal SageFrameModuleCSSlinks = this.Page.FindControl("SageFrameModuleCSSlinks") as Literal;
                if (SageFrameModuleCSSlinks != null)
                {
                    string linkText = "<link href=\"" + Page.ResolveUrl(ModuleRootLocation) + "\" rel=\"stylesheet\" type=\"text/css\" />";
                    SageFrameModuleCSSlinks.Text += linkText;
                }
            }
        }

        public void AddModuleCssToPage(string cssFilePath)
        {
            string ModuleRootLocation = string.Empty;
            ModuleRootLocation = cssFilePath;
            string FullPath = Server.MapPath(ModuleRootLocation);
            if (System.IO.File.Exists(FullPath))
            {
                Literal SageFrameModuleCSSlinks = this.Page.FindControl("SageFrameModuleCSSlinks") as Literal;
                if (SageFrameModuleCSSlinks != null)
                {
                    string linkText = "<link href=\"" + Page.ResolveUrl(ModuleRootLocation) + "\" rel=\"stylesheet\" type=\"text/css\" />";
                    SageFrameModuleCSSlinks.Text += linkText;
                }
            }
        }
    }
}
