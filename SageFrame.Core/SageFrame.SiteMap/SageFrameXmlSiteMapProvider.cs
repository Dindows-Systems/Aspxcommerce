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
using SageFrame.Web;

namespace SageFrame.SageFrameClass
{
    public class SageFrameXmlSiteMapProvider: System.Web.XmlSiteMapProvider
    {
        public override bool IsAccessibleToUser(System.Web.HttpContext context, System.Web.SiteMapNode node)
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
                        if (role == "*" || role.ToUpper() == SystemSetting.ANONYMOUS_ROLEID.ToUpper())
                        {
                            return true;
                        }
                        else if (context.User != null)
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
