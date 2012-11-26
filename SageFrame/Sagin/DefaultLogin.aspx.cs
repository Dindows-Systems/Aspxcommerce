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
using System.Web.Security;
using System.Web.UI.WebControls;
using SageFrame.RolesManagement;
using SageFrame.Framework;
using SageFrame.Web;

namespace SageFrame
{
    public partial class Sagin_DefaultLogin : PageBase, SageFrameRoute
    {
        string strRoles = string.Empty;
        SageFrameConfig pagebase = new SageFrameConfig();
        bool IsUseFriendlyUrls = true;
        protected void Page_Init(object sender, EventArgs e)
        {
            InitializePage();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            if (!IsPostBack)
            {
                
                HyperLink hypForgetPassword = (HyperLink)Login1.FindControl("hypForgetPassword");
                hypForgetPassword.Text = "Forget Password?";
                if (IsUseFriendlyUrls)
                {
                    if (GetPortalID > 1)
                    {
                        hypForgetPassword.NavigateUrl = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalForgotPassword) + ".aspx");
                    }
                    else
                    {
                        hypForgetPassword.NavigateUrl = ResolveUrl("~/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalForgotPassword) + ".aspx");
                    }
                }
                else
                {
                    hypForgetPassword.NavigateUrl = ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalForgotPassword));
                }
            }
        }
        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            try
            {
                RolesManagementDataContext dbRole = new RolesManagementDataContext(SystemSetting.SageFrameConnectionString);
                var userRoles = dbRole.sp_RoleGetByUsername(Login1.UserName,GetPortalID).ToList();
                
                foreach (var userRole in userRoles)
                {
                    strRoles += userRole.RoleId + ",";
                }
                if (strRoles.Length > 1)
                {
                    strRoles = strRoles.Substring(0, strRoles.Length - 1);
                }
                if (strRoles.Length > 0)
                {
                    SetUserRoles(strRoles);
                    if (Request.QueryString["ReturnUrl"] == null)
                    {
                        if (IsUseFriendlyUrls)
                        {
                            if (string.IsNullOrEmpty(PortalSEOName))
                            {
                                Response.Redirect("~/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                            }
                            else
                            {
                                Response.Redirect("~/portal/" + PortalSEOName + "/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                            }
                        }
                        else
                        {
                            Response.Redirect(ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage)));
                        }
                    }
                }
                else
                {
                    FormsAuthentication.SignOut();
                    Login1.FailureText = "failure text";
                    
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
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
