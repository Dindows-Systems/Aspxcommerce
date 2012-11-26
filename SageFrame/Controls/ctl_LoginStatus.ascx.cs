using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Framework;

namespace SageFrame.Controls
{
    public partial class ctl_LoginStatus : SageUserControl
    {
        public string RegisterURL = string.Empty;
        public string profileURL = string.Empty;
        public string profileText = string.Empty;
        
        bool IsUseFriendlyUrls = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            SageFrameConfig sfConfig = new SageFrameConfig();
            IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            if (!IsPostBack)
            {
                profileText = GetSageMessage("LoginStatus", "MyProfile");//"My Profile";
               
                Literal lnkProfileUrl = (Literal)LoginView1.TemplateControl.FindControl("lnkProfileUrl");
                RegisterURL = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalRegistrationPage) + ".aspx";
                if (sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalShowProfileLink) == "1")
                {
                    
                    if (IsUseFriendlyUrls)
                    {
                        if (GetPortalID > 1)
                        {
                            profileURL = "<a  href='" + ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalUserProfilePage) + ".aspx") + "'>" + profileText + "</a>";
                        }
                        else
                        {
                            profileURL = "<a  href='" + ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalUserProfilePage) + ".aspx") + "'>" + profileText + "</a>";
                        }
                    }
                    else
                    {
                        profileURL = "<a  href='" + ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalUserProfilePage)) + "'>" + profileText + "</a>";
                    }
                }
                else
                {
                    profileURL = "";
                }
                if (IsUseFriendlyUrls)
                {
                    if (GetPortalID > 1)
                    {
                        RegisterURL = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalRegistrationPage) + ".aspx");
                    }
                    else
                    {
                        RegisterURL = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalRegistrationPage) + ".aspx");
                    }
                }
                else
                {
                    RegisterURL = ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalRegistrationPage));
                }
            }
        }

        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            SetUserRoles(string.Empty);
            SageFrameConfig sfConfig = new SageFrameConfig();
            bool IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            if (IsUseFriendlyUrls)
            {
                if (GetPortalID > 1)
                {
                    Response.Redirect("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                }
                else
                {
                    Response.Redirect("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
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
    }
}