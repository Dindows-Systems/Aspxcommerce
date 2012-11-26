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
using System.Web.Security;
using SageFrame.NewLetterSubscriber;

public partial class Modules_ctl_HeaderControl : BaseUserControl //System.Web.UI.UserControl
{
    public string RegisterURL = string.Empty;
    public string HomeURL = string.Empty;
    public string AdminHomeURL = string.Empty;
    public string profileURL = string.Empty;
    public string profileText = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SetRegisterUrl();
            profileURL = Page.ResolveUrl("~/User_Profile.aspx");
            profileText = "My Profile";
            HomeURL = Page.ResolveUrl("~/" + CommonHelper.DefaultPage);

            if (!IsPostBack)
            {
                
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        SageFrameConfig sfConfig = new SageFrameConfig();
        if (sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalShowLogo) == "1")
        {
            LoadControl(phdLogo, "~/Controls/ctl_Logo.ascx");
            divLogo.Attributes.Add("style", "display:block;");
        }
        else
        {
            phdLogo.Controls.Clear();
            divLogo.Attributes.Add("style", "display:none;");
        }
        if (sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalShowSubscribe) == "1")
        {
            LoadControl(phdSubscribe, "~/Controls/ctl_Subscribe.ascx");
            divSubscribe.Attributes.Add("style", "display:block;");
        }
        else
        {
            phdSubscribe.Controls.Clear();
            divSubscribe.Attributes.Add("style", "display:none;");
        }
        if (sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalShowLoginStatus) == "1")
        {
            LoadControl(phdLoginStatus, "~/Controls/ctl_LoginStatus.ascx");
            divTopMenu.Attributes.Add("style", "display:block;");
        }
        else
        {
            phdLoginStatus.Controls.Clear();
            divTopMenu.Attributes.Add("style", "display:none;");
        }
    }

    private void LoadControl(PlaceHolder ContainerControl,string controlSource)
    {
        UserControl ctl = this.Page.LoadControl(controlSource) as UserControl;
        ctl.EnableViewState = true;
        ContainerControl.Controls.Add(ctl);
    }

    private void SetRegisterUrl()
    {
        SageFrameConfig sfConfig = new SageFrameConfig();
        RegisterURL = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalRegistrationPage) + ".aspx");
        bool IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
        if (!IsUseFriendlyUrls)
        {
            RegisterURL = ResolveUrl("~/" + CommonHelper.DefaultPage) + "?ptlid=" + GetPortalID + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalRegistrationPage);
        }
    }

    protected void SageStatus2_LoggedOut(object sender, EventArgs e)
    {
        HttpContext.Current.Session["User_RoleID"] = null;
        Response.Redirect("~/" + CommonHelper.DefaultPage);
    }

    protected void SageStatus2_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        HttpContext.Current.Session["User_RoleID"] = null;
    }   
}
