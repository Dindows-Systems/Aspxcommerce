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
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using SageFrame.Web;
using SageFrame.SiteMap;
using System.Web.Security;
using System.Collections;


namespace SageFrame.SageFrameClass
{
    public class SiteMapHelper
    {
        SageFrameConfig pagebase = new SageFrameConfig();
        bool IsUseFriendlyUrls = true;
        SiteMapDataContext dbSiteMap = new SiteMapDataContext(SystemSetting.SageFrameConnectionString);
        public void GenerateXMLFile(string sFileName, Int32 portalID, string portalName, string username)
        {
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            Encoding enc = Encoding.UTF8;
            XmlTextWriter objXMLTW = new XmlTextWriter(sFileName, enc);
            try
            {
                objXMLTW.WriteStartDocument();
                objXMLTW.WriteStartElement("siteMap");
                objXMLTW.WriteAttributeString("enableLocalization", "true");
                objXMLTW.WriteStartElement("siteMapNode");
                objXMLTW.WriteAttributeString("title", "$resources:" + portalName + "sitemap, HomeTitle");
                objXMLTW.WriteAttributeString("description", "$resources:" + portalName + "sitemap, HomeDescription");
                if (IsUseFriendlyUrls)
                {
                    if (portalID > 1)
                    {
                        objXMLTW.WriteAttributeString("url", "~/portal/" + portalName);
                    }
                    else
                    {
                        objXMLTW.WriteAttributeString("url", "~/");
                    }
                }
                else
                {
                    objXMLTW.WriteAttributeString("url", "~/Default.aspx?ptlid=" + portalID.ToString() + "&ptSEO=" + portalName);
                }
                objXMLTW.WriteAttributeString("roles", "*");
                var pages = dbSiteMap.sp_GetSiteMapNodeWithRolesByParentNodeID(0, username, portalID);
                foreach (sp_GetSiteMapNodeWithRolesByParentNodeIDResult page in pages)
                {
                    string url = string.Empty;
                    if (string.IsNullOrEmpty(page.Url))
                    {
                        url = "~" + page.TabPath;
                    }
                    else
                    {
                        url = page.Url;
                    }
                    Int32 PageID = page.PageID;
                    objXMLTW.WriteStartElement("siteMapNode");
                    objXMLTW.WriteAttributeString("title", "$resources:" + portalName + "sitemap, " + page.PageName.Replace(" ", "") + "Title");
                    objXMLTW.WriteAttributeString("description", "$resources:" + portalName + "sitemap, " + page.PageName.Replace(" ", "") + "Description");
                    objXMLTW.WriteAttributeString("url", url);
                    objXMLTW.WriteAttributeString("roles", page.PageRoles);
                    objXMLTW.WriteAttributeString("startdate", page.StartDate.ToString());
                    objXMLTW.WriteAttributeString("enddate", page.EndDate.ToString());
                    AddPages(PageID, portalID, portalName, username, objXMLTW);
                    objXMLTW.WriteEndElement();
                }
                objXMLTW.WriteEndElement();
                objXMLTW.WriteEndDocument();
            }
            finally
            {
                objXMLTW.Flush();
                objXMLTW.Close();
            }
        }
        private void AddPages(Int32 parentID, Int32 portalID, string portalName, string username, XmlTextWriter objXMLTW)
        {
            var pages = dbSiteMap.sp_GetSiteMapNodeWithRolesByParentNodeID(parentID, username, portalID);
            foreach (var page in pages)
            {
                string url = string.Empty;
                if (string.IsNullOrEmpty(page.Url))
                {
                    url = "~" + page.TabPath;
                }
                else
                {
                    url = page.Url;
                }
                Int32 PageID = page.PageID;
                objXMLTW.WriteStartElement("siteMapNode");
                objXMLTW.WriteAttributeString("title", "$resources:" + portalName + "sitemap, " + page.PageName.Replace(" ", "") + "Title");
                objXMLTW.WriteAttributeString("description", "$resources:" + portalName + "sitemap, " + page.PageName.Replace(" ", "") + "Description");
                objXMLTW.WriteAttributeString("url", url);
                objXMLTW.WriteAttributeString("roles", page.PageRoles);
                objXMLTW.WriteAttributeString("startdate", page.StartDate.ToString());
                objXMLTW.WriteAttributeString("enddate", page.EndDate.ToString());
                AddPages(PageID, portalID, portalName, username, objXMLTW);
                objXMLTW.WriteEndElement();
            }
        }
        public void GenerateXMLFileAdmin(string sFileName, Int32 portalID, string portalName, string username)
        {
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            Encoding enc = Encoding.UTF8;
            XmlTextWriter objXMLTW = new XmlTextWriter(sFileName, enc);
            try
            {
                objXMLTW.WriteStartDocument();
                objXMLTW.WriteStartElement("siteMap");
                objXMLTW.WriteAttributeString("enableLocalization", "true");
                objXMLTW.WriteStartElement("siteMapNode");
                objXMLTW.WriteAttributeString("title", "$resources:" + portalName + "sitemap, AdministrationTitle");
                objXMLTW.WriteAttributeString("description", "title", "$resources:" + portalName + "sitemap, AdministrationDescription");
                if (IsUseFriendlyUrls)
                {
                    if (portalID > 1)
                    {
                        objXMLTW.WriteAttributeString("url", "~/portal/" + portalName);
                    }
                    else
                    {
                        objXMLTW.WriteAttributeString("url", "~/");
                    }
                }
                else
                {
                    objXMLTW.WriteAttributeString("url", "~/Default.aspx?ptlid=" + portalID + "&ptSEO=" + portalName);
                }
                objXMLTW.WriteAttributeString("roles", "*");
                var pages = dbSiteMap.sp_GetAdminSiteMapNodeWithRolesByParentNodeID(0, username, portalID);
                foreach (var page in pages)
                {
                    string url = string.Empty;
                    if (string.IsNullOrEmpty(page.Url))
                    {
                        url = "~" + page.TabPath;
                    }
                    else
                    {
                        url = page.Url;
                    }
                    Int32 PageID = page.PageID;
                    objXMLTW.WriteStartElement("siteMapNode");
                    objXMLTW.WriteAttributeString("title", "$resources:" + portalName + "sitemap, " + page.PageName.Replace(" ", "") + "Title");
                    objXMLTW.WriteAttributeString("description", "$resources:" + portalName + "sitemap, " + page.PageName.Replace(" ", "") + "Description");
                    objXMLTW.WriteAttributeString("url", url);
                    objXMLTW.WriteAttributeString("roles", page.PageRoles);
                    objXMLTW.WriteAttributeString("startdate", page.StartDate.ToString());
                    objXMLTW.WriteAttributeString("enddate", page.EndDate.ToString());
                    AddPagesAdmin(PageID, portalID, portalName, username, objXMLTW);
                    objXMLTW.WriteEndElement();
                }
                objXMLTW.WriteEndElement();
                objXMLTW.WriteEndDocument();
            }
            finally
            {
                objXMLTW.Flush();
                objXMLTW.Close();
            }
        }
        private void AddPagesAdmin(Int32 parentID, Int32 portalID, string portalName, string username, XmlTextWriter objXMLTW)
        {
            var pages = dbSiteMap.sp_GetAdminSiteMapNodeWithRolesByParentNodeID(parentID, username, portalID);
            foreach (var page in pages)
            {
                string url = string.Empty;
                if (string.IsNullOrEmpty(page.Url))
                {
                    url = "~" + page.TabPath;
                }
                else
                {
                    url = page.Url;
                }
                Int32 PageID = page.PageID;
                objXMLTW.WriteStartElement("siteMapNode");
                objXMLTW.WriteAttributeString("title", "$resources:" + portalName + "sitemap, " + page.PageName.Replace(" ", "") + "Title");
                objXMLTW.WriteAttributeString("description", "$resources:" + portalName + "sitemap, " + page.PageName.Replace(" ", "") + "Description");
                objXMLTW.WriteAttributeString("url", url);
                objXMLTW.WriteAttributeString("roles", page.PageRoles);
                objXMLTW.WriteAttributeString("startdate", page.StartDate.ToString());
                objXMLTW.WriteAttributeString("enddate", page.EndDate.ToString());
                AddPagesAdmin(PageID, portalID, portalName, username, objXMLTW);
                objXMLTW.WriteEndElement();
            }
        }
        public void GenerateXMLFileFooter(string sFileName, Int32 portalID, string portalName, string username)
        {
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            Encoding enc = Encoding.UTF8;
            XmlTextWriter objXMLTW = new XmlTextWriter(sFileName, enc);
            try
            {
                objXMLTW.WriteStartDocument();
                objXMLTW.WriteStartElement("siteMap");
                objXMLTW.WriteAttributeString("enableLocalization", "true");
                objXMLTW.WriteStartElement("siteMapNode");
                objXMLTW.WriteAttributeString("title", "$resources:" + portalName + "sitemap, FooterTitle");
                objXMLTW.WriteAttributeString("description", "title", "$resources:" + portalName + "sitemap, FooterDescription");
                if (IsUseFriendlyUrls)
                {
                    if (portalID > 1)
                    {
                        objXMLTW.WriteAttributeString("url", "~/portal/" + portalName);
                    }
                    else
                    {
                        objXMLTW.WriteAttributeString("url", "~/");
                    }
                }
                else
                {
                    objXMLTW.WriteAttributeString("url", "~/Default.aspx?ptlid=" + portalID + "&ptSEO=" + portalName);
                }
                objXMLTW.WriteAttributeString("roles", "*");
                var pages = dbSiteMap.sp_GetFooterSiteMapNodeWithRolesByParentNodeID(0, username, portalID);
                foreach (var page in pages)
                {
                    Int32 PageID = page.PageID;
                    string url = string.Empty;
                    if (string.IsNullOrEmpty(page.Url))
                    {
                        url = "~" + page.TabPath;
                    }
                    else
                    {
                        url = page.Url;
                    }
                    objXMLTW.WriteStartElement("siteMapNode");
                    objXMLTW.WriteAttributeString("title", "$resources:" + portalName + "sitemap, " + page.PageName.Replace(" ", "") + "Title");
                    objXMLTW.WriteAttributeString("description", "$resources:" + portalName + "sitemap, " + page.PageName.Replace(" ", "") + "Description");
                    objXMLTW.WriteAttributeString("url", url);
                    objXMLTW.WriteAttributeString("roles", page.PageRoles);
                    objXMLTW.WriteAttributeString("startdate", page.StartDate.ToString());
                    objXMLTW.WriteAttributeString("enddate", page.EndDate.ToString());
                    AddPagesFooter(PageID, portalID, portalName, username, objXMLTW);
                    objXMLTW.WriteEndElement();
                }
                objXMLTW.WriteEndElement();
                objXMLTW.WriteEndDocument();
            }
            finally
            {
                objXMLTW.Flush();
                objXMLTW.Close();
            }
        }
        private void AddPagesFooter(Int32 parentID, Int32 portalID, string portalName, string username, XmlTextWriter objXMLTW)
        {
            var pages = dbSiteMap.sp_GetFooterSiteMapNodeWithRolesByParentNodeID(parentID, username, portalID);
            foreach (var page in pages)
            {
                string url = string.Empty;
                if (string.IsNullOrEmpty(page.Url))
                {
                    url = "~" + page.TabPath;
                }
                else
                {
                    url = page.Url;
                }
                Int32 PageID = page.PageID;
                objXMLTW.WriteStartElement("siteMapNode");
                objXMLTW.WriteAttributeString("title", "$resources:" + portalName + "sitemap, " + page.PageName.Replace(" ", "") + "Title");
                objXMLTW.WriteAttributeString("description", "$resources:" + portalName + "sitemap, " + page.PageName.Replace(" ", "") + "Description");
                objXMLTW.WriteAttributeString("url", url);
                objXMLTW.WriteAttributeString("roles", page.PageRoles);
                objXMLTW.WriteAttributeString("startdate", page.StartDate.ToString());
                objXMLTW.WriteAttributeString("enddate", page.EndDate.ToString());
                AddPagesFooter(PageID, portalID, portalName, username, objXMLTW);
                objXMLTW.WriteEndElement();
            }
        }
        public void BuidSiteMapXML(string sFileName, bool isActive, bool isDeleted, Int32 portalID, string username)
        {
            var pages = dbSiteMap.usp_GetAllPages(isActive, isDeleted, portalID, username);
            Encoding enc = Encoding.UTF8;
            XmlTextWriter objXMLTW = new XmlTextWriter(sFileName, enc);
            try
            {
                objXMLTW.WriteStartDocument();
                objXMLTW.WriteStartElement("SageFrameMenuText");
                foreach (var page in pages)
                {
                    objXMLTW.WriteStartElement(page.PageName.Replace(" ", ""));
                    objXMLTW.WriteElementString("Title", page.PageName);
                    objXMLTW.WriteElementString("Description", page.Description);
                    objXMLTW.WriteEndElement();
                }
                objXMLTW.WriteEndElement();
                objXMLTW.WriteEndDocument();
            }
            finally
            {
                objXMLTW.Flush();
                objXMLTW.Close();
            }
        }

        public void BuidSiteMapResx(string sFileName, bool isActive, bool isDeleted, Int32 portalID, string username)
        {
            var pages = dbSiteMap.usp_GetAllPages(isActive, isDeleted, portalID, username);
            List<KeyValue> lstResDef = new List<KeyValue>();
            try
            {
                string key = string.Empty;
                string value = string.Empty;
                lstResDef.Add(new KeyValue("RootTitle", "Root"));
                lstResDef.Add(new KeyValue("RootDescription", "Root"));
                lstResDef.Add(new KeyValue("AdministrationTitle", "Administration"));
                lstResDef.Add(new KeyValue("AdministrationDescription", "Administration"));
                lstResDef.Add(new KeyValue("YouAreInTitle", "You Are In"));
                lstResDef.Add(new KeyValue("YouAreIntDescription", "You Are In"));
                foreach (var page in pages)
                {
                    key = page.PageName.Replace(" ", "") + "Title";
                    value = page.PageName;
                    KeyValue keyValueTitle = new KeyValue(key, value);
                    lstResDef.Add(keyValueTitle);

                    key = page.PageName.Replace(" ", "") + "Description";
                    value = page.Description;
                    KeyValue keyValueDescription = new KeyValue(key, value);
                    lstResDef.Add(keyValueDescription);
                }
            }
            finally
            {
                CommonFunction.WriteResX(sFileName, lstResDef);
            }
        }
    }
}