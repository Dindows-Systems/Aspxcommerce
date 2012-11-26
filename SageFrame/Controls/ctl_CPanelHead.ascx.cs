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
using SageFrame.Web;
using SageFrame.Framework;

public partial class Controls_ctl_CPanelHead : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (HttpContext.Current.Request.RawUrl.Contains("/Admin/") || HttpContext.Current.Request.RawUrl.Contains("/Admin.aspx") || HttpContext.Current.Request.RawUrl.Contains("/Super-User/") || HttpContext.Current.Request.RawUrl.Contains("/Super-User.aspx") || HttpContext.Current.Request.RawUrl.Contains("ManageReturnURL=")) 
            {
                LoadHeadContent();
            }
            else
            {
                this.Visible = false;
            }
            //LoadControl(phdAdministrativBreadCrumb, "~/Controls/ctl_AdminBreadCrum.ascx");
        }
    }

    private void LoadHeadContent()
    {
        try
        {
            SageFrameConfig sfConfig = new SageFrameConfig();
            string strCPanleHeader = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLogoTemplate);
            litCPanleTitle.Text = strCPanleHeader;
            SageFrame.Application.Application app = new SageFrame.Application.Application();
            litSFVersion.Text = app.Version.ToString();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void LoadControl(PlaceHolder ContainerControl, string controlSource)
    {
        UserControl ctl = this.Page.LoadControl(controlSource) as UserControl;
        ctl.EnableViewState = true;
        ContainerControl.Controls.Add(ctl);
    }
}
