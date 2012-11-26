using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Framework;
public partial class Modules_Admin_LoginControl_LoginStatus : SageUserControl
{
    public string RegisterURL = string.Empty;
    public string profileURL = string.Empty;
    public string profileText = string.Empty;
    
    bool IsUseFriendlyUrls = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        SageFrameConfig pb = new SageFrameConfig();
        IsUseFriendlyUrls = pb.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
        //if (!IsPostBack)
        //{
            profileText = GetSageMessage("LoginStatus", "MyProfile");
            Literal lnkProfileUrl = (Literal)LoginView1.TemplateControl.FindControl("lnkProfileUrl");
            RegisterURL = pb.GetSettingsByKey(SageFrameSettingKeys.PortalRegistrationPage) + ".aspx";
            if (pb.GetSettingsByKey(SageFrameSettingKeys.PortalShowProfileLink) == "1")
            {

                if (IsUseFriendlyUrls)
                {
                    if (GetPortalID > 1)
                    {
                        profileURL = "<a  href='" + ResolveUrl("~/portal/" + GetPortalSEOName + "/" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalUserProfilePage) + ".aspx") + "'>" + profileText + "</a>";
                    }
                    else
                    {
                        profileURL = "<a  href='" + ResolveUrl("~/" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalUserProfilePage) + ".aspx") + "'>" + profileText + "</a>";
                    }
                }
                else
                {
                    profileURL = "<a  href='" + ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalUserProfilePage)) + "'>" + profileText + "</a>";
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
                    RegisterURL = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalRegistrationPage) + ".aspx");
                }
                else
                {
                    RegisterURL = ResolveUrl("~/" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalRegistrationPage) + ".aspx");
                }
            }
            else
            {
                RegisterURL = ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalRegistrationPage));
            }
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated == true)
                {
                    Label lblProfileURL = LoginView1.FindControl("lblProfileURL") as Label;
                    if (lblProfileURL != null)
                    {
                        if (profileURL != "")
                        {
                            lblProfileURL.Text = "<li>" + profileURL + "</li>";
                            lblProfileURL.Visible = true;
                        }
                        else
                        {
                            lblProfileURL.Visible = false;
                        }
                    }
                    else
                    {
                        Response.Redirect(pb.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");

                    }
                    
                }
            }
            int UserRegistrationType = pb.GetSettingIntByKey(SageFrameSettingKeys.PortalUserRegistration);
            if (UserRegistrationType > 0)
            {
                RegisterURL = "<span class='cssClassRegister'><a href='" + RegisterURL + "'>" + GetSageMessage("LoginStatus", "Register") +"</a></span>";
            }
            else
            {
                RegisterURL = "";
            }
        //}
    }

    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        SetUserRoles(string.Empty);
        SageFrameConfig pb = new SageFrameConfig();
        bool IsUseFriendlyUrls = pb.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
        if (IsUseFriendlyUrls)
        {
            if (GetPortalID > 1)
            {
                Response.Redirect("~/portal/" + GetPortalSEOName + "/" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
            }
            else
            {
                Response.Redirect("~/" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
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
