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
using SageFrame.Web;
using SageFrame.Framework;

namespace SageFrame.Controls
{
    public partial class LoginStatus : BaseAdministrationUserControl
    {
        
         bool IsUseFriendlyUrls = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SageFrameConfig sfConfig = new SageFrameConfig();
                IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
                if (HttpContext.Current.User != null)
                {
                    if (Membership.GetUser() != null)
                    {
                        lnkloginStatus.Text = SageLogOutText;
                        lnkloginStatus.CommandName = "LOGOUT";
                    }
                    else
                    {
                        lnkloginStatus.Text = SageLogInText;
                        lnkloginStatus.CommandName = "LOGIN";
                    }
                }
                else
                {
                    lnkloginStatus.Text = SageLogInText;
                    lnkloginStatus.CommandName = "LOGIN";
                }
            }
            catch
            {
            }
        }

        private string strLogOut = string.Empty;
        private string strLogIn = string.Empty;
        public string SageLogInText
        {
            get
            {
                if (strLogIn == string.Empty)
                {
                    strLogIn = GetSageMessage("LoginStatus", "Login");
                }
                return strLogIn;
            }
        }
        public string SageLogOutText
        {
            get 
            {
                if (strLogOut == string.Empty)
                {
                    strLogOut = GetSageMessage("LoginStatus", "Logout");
                }
                return strLogOut;
            }            
        }

        protected void lnkloginStatus_Click(object sender, EventArgs e)
        {
            try
            {
                ///Update the Session Tracker
                SessionTracker sessionTracker = (SessionTracker)Session["Tracker"];
                SageFrame.Web.SessionLog sLog = new SageFrame.Web.SessionLog();
                sLog.SessionLogEnd(sessionTracker);

                SessionTracker sessionTrackerNew = new SessionTracker();
                if (sessionTrackerNew != null)
                {
                    SageFrame.Web.SessionLog sLogNew = new SageFrame.Web.SessionLog();
                    sLogNew.SessionLogStart(sessionTrackerNew);
                }
                HttpContext.Current.Session["Tracker"] = sessionTrackerNew;


                string ReturnUrl = string.Empty;
                string RedUrl = string.Empty;
                SageFrameConfig sfConfig = new SageFrameConfig();
                if (lnkloginStatus.CommandName == "LOGIN")
                {
                    if (IsUseFriendlyUrls)
                    {
                        if (Request.QueryString["ReturnUrl"] == null)
                        {
                            ReturnUrl = Request.RawUrl.ToString();
                            if (!(ReturnUrl.ToLower().Contains(".aspx")))
                            {
                                //ReturnUrl = ReturnUrl.Remove(strURL.LastIndexOf('/'));
                                if (ReturnUrl.EndsWith("/"))
                                {
                                    ReturnUrl += sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx";
                                }
                                else
                                {
                                    ReturnUrl += '/' + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx";
                                }
                            }
                        }
                        else
                        {
                            ReturnUrl = Request.QueryString["ReturnUrl"].ToString();
                        }
                        if (GetPortalID > 1)
                        {
                            RedUrl = "~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + ".aspx?ReturnUrl=" + ReturnUrl;

                        }
                        else
                        {
                            RedUrl = "~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + ".aspx?ReturnUrl=" + ReturnUrl;
                        }
                    }
                    else
                    {
                        string[] arrUrl;
                        string strURL = string.Empty;
                        arrUrl = Request.RawUrl.Split('?');
                        string[] keys = Request.QueryString.AllKeys;
                        for (int i = 0; i < Request.QueryString.Count; i++)
                        {
                            string[] values = Request.QueryString.GetValues(i);
                            if (values != null) strURL += keys[i] + '=' + values[0] + '&';
                        }
                        if (strURL.Length > 0)
                        {
                            strURL = strURL.Remove(strURL.LastIndexOf('&'));
                        }
                        ReturnUrl = arrUrl[0] + Server.UrlEncode(strURL.Length > 0 ? "?" + strURL : "");
                        //Response.Redirect("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + "&ReturnUrl=" + (Request.QueryString["pgnm"] == null ? pb.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) : Request.QueryString["pgnm"].ToString()));
                        RedUrl = "~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + "&ReturnUrl=" + ReturnUrl;
                    }
                }
                else
                {
                    //TODO:: Clear the session HERE
                    RegenerateSessionID();
                    FormsAuthentication.SignOut();
                    lnkloginStatus.Text = "Login";
                    SetUserRoles(string.Empty);                    
                    if (IsUseFriendlyUrls)
                    {
                        if (GetPortalID > 1)
                        {
                            RedUrl = "~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx";
                        }
                        else
                        {
                            RedUrl = "~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx";
                        }
                    }
                    else
                    {
                        RedUrl = "~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage);                        
                    }
                }
                Response.Redirect(RedUrl, false);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        public void RegenerateSessionID()
        {
            System.Web.SessionState.SessionIDManager manager = new System.Web.SessionState.SessionIDManager();
            string oldId = manager.GetSessionID(Context);
            string newId = manager.CreateSessionID(Context);
            bool isAdd = false, isRedir = false;
            manager.SaveSessionID(Context, newId, out isRedir, out isAdd);
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
    }
}