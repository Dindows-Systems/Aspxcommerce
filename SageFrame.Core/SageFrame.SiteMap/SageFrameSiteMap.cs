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
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using SageFrame.SiteMap;
using SageFrame.Web;

using System.Collections;


namespace SageFrame
{
    public class SageFrameSiteMap : StaticSiteMapProvider
    {
        private readonly object siteMapLock = new object();
        public const string CacheDependencyKey = "SageFrameSiteMapProviderCacheDependency";
        private SiteMapNode root = null;
        private SageFrameConfig pageBase = new SageFrameConfig();
        public override SiteMapNode BuildSiteMap()
        {
            try
            {
                lock (this)
                {
                    if (root != null)
                        return root;
                    base.Clear();
                    string nodeKey, nodeTitle, nodeUrl, nodeDescription;
                    SiteMapDataContext dbSiteMap = new SiteMapDataContext(SystemSetting.SageFrameConnectionString);
                    string userName = SystemSetting.AnonymousUsername;
                    if (HttpContext.Current.User != null)
                    {
                        if (Membership.GetUser() != null)
                        {
                            userName = Membership.GetUser().UserName;
                        }
                    }
                    IList rootRoles = new ArrayList();
                    rootRoles.Add("*");
                    root = new SiteMapNode(this, "root", "~/" + CommonHelper.DefaultPage, "Home", "", rootRoles, null, null, null);
                    AddNode(root);
                    var pages = dbSiteMap.sp_GetSiteMapNodeWithRolesByParentNodeID(0, userName, GetPortalID);
                    foreach (var page in pages)
                    {
                        nodeKey = page.PageID.ToString();
                        nodeTitle = page.PageName;
                        if (string.IsNullOrEmpty(page.Url))
                        {
                            nodeUrl = "~" + page.TabPath;
                        }
                        else
                        {
                            nodeUrl = page.Url;
                        }
                        nodeDescription = page.Description;
                        string[] PageRoles = page.PageRoles.Split(',');
                        IList roles = new ArrayList();
                        for (int i = 0; i < PageRoles.Length; i++)
                        {
                            roles.Add(PageRoles[i]);
                        }

                        SiteMapNode childNode = FindSiteMapNodeFromKey(nodeKey);
                        if (childNode == null)
                        {
                            childNode = new SiteMapNode(this, nodeKey, nodeUrl, nodeTitle, nodeDescription, roles, null, null, null);
                            AddNode(childNode, root);
                            AddPages(childNode);
                        }
                    }
                    SqlCacheDependency pagesTableDependency = new SqlCacheDependency(SystemSetting.SageFrameDBName, "Pages");
                    SqlCacheDependency MembershipTableDependency = new SqlCacheDependency(SystemSetting.SageFrameDBName, "aspnet_Membership");
                    SqlCacheDependency UsersTableDependency = new SqlCacheDependency(SystemSetting.SageFrameDBName, "aspnet_users");
                    AggregateCacheDependency aggregateDependencies = new AggregateCacheDependency();
                    aggregateDependencies.Add(pagesTableDependency);
                    aggregateDependencies.Add(MembershipTableDependency);
                    aggregateDependencies.Add(UsersTableDependency);
                    HttpRuntime.Cache.Insert(CacheDependencyKey, DateTime.Now, aggregateDependencies
                        , DateTime.Now
                        , Cache.NoSlidingExpiration
                        , CacheItemPriority.Normal
                        , new CacheItemRemovedCallback(OnSiteMapChanged));
                    return root;
                }
            }
            catch
            {
                return root;
            }
        }
        private void AddPages(SiteMapNode parentNode)
        {
            SiteMapDataContext dbSiteMap = new SiteMapDataContext(SystemSetting.SageFrameConnectionString);
            string userName = SystemSetting.AnonymousUsername;
            if (HttpContext.Current.User != null)
            {
                if (Membership.GetUser() != null)
                {
                    userName = Membership.GetUser().UserName;
                }
            }
            var pages = dbSiteMap.sp_GetSiteMapNodeWithRolesByParentNodeID(Int32.Parse(parentNode.Key.ToString()), userName, GetPortalID);
            foreach (var page in pages)
            {
                string[] PageRoles = page.PageRoles.Split(',');
                IList roles = new ArrayList();
                for (int i = 0; i < PageRoles.Length; i++)
                {
                    roles.Add(PageRoles[i]);
                }
                string url = string.Empty;
                if (string.IsNullOrEmpty(page.Url))
                {
                    url = "~" + page.TabPath;
                }
                else
                {
                    url = page.Url;
                }
                SiteMapNode pageNode = new SiteMapNode(this,
                                            page.PageID.ToString(),
                                            url,
                                            page.PageName, page.Description, roles, null, null, null);

                AddNode(pageNode, parentNode);
                AddPages(pageNode);
            }
        }
        protected override SiteMapNode GetRootNodeCore()
        {
            return BuildSiteMap();
        }
        protected void OnSiteMapChanged(string key, object value, CacheItemRemovedReason reason)
        {
            lock (siteMapLock)
            {
                if (string.Compare(key, CacheDependencyKey) == 0)
                {
                    root = null;
                }
            }
        }
        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            try
            {
                if (node == null)
                    throw new ArgumentNullException("node");

                if (context == null)
                    throw new ArgumentNullException("context");

                if (!this.SecurityTrimmingEnabled)
                    return true;

                if ((node.Roles != null) && (node.Roles.Count > 0))
                {
                    foreach (string role in node.Roles)
                    {
                        if (HttpContext.Current.User != null)
                        {
                            MembershipUser user = Membership.GetUser();
                            if (user != null)
                            {
                                if (role == user.UserName)
                                {

                                    return true;
                                }
                            }
                        }
                        if ( role == "*" || role.ToUpper() == SystemSetting.ANONYMOUS_ROLEID.ToUpper())
                        {
                            return true;
                        }
                        else if (context.User!=null)
                        {
                            if (context.User.IsInRole(role))
                            {
                                return true;
                            }
                        }
                            string[] userRoles = GetUserRoles();
                            for (int i = 0; i < userRoles.Length; i++)
                            {
                                if (role == userRoles[i].ToUpper())
                                {
                                    return true;
                                }
                            }
                        
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        public DateTime? CachedDate
        {
            get
            {
                return HttpRuntime.Cache[CacheDependencyKey] as DateTime?;
            }
        }

        public Int32 GetPortalID
        {
            get
            {
                return pageBase.GetPortalID;
            }
        }


        public string[] GetUserRoles()
        {
            string strRoles = string.Empty;
            if (HttpContext.Current.Session["SageUserRoles"] != null)
            {
                strRoles = HttpContext.Current.Session["SageUserRoles"].ToString();
            }
            else
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["SageUserRolesCookie"];
                if (cookie != null)
                {
                    strRoles = cookie["SageUserRolesProtected"].ToString();
                }
            }
            string[] arrRoles = strRoles.Split(',');
            return arrRoles;
        }
    }
}
