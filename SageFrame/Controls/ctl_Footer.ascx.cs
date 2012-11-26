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


namespace SageFrame.Web
{
    public partial class Modules_ctl_Footer : BaseUserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SageFrameConfig sfConfig = new SageFrameConfig();
                litCopyWrite.Text = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalCopyright);
            }
        }
        private void LoadControl(PlaceHolder ContainerControl, string controlSource)
        {
            UserControl ctl = this.Page.LoadControl(controlSource) as UserControl;
            ctl.EnableViewState = true;
            ContainerControl.Controls.Add(ctl);
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            SageFrameConfig sfConfig = new SageFrameConfig();
            if (sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalShowFooterLink) == "1")
            {
                LoadControl(phdFooterMenu, "~/Controls/ctl_FooterMenu.ascx");
                divFooterLink.Attributes.Add("style", "display:block;");
            }
            else
            {
                phdFooterMenu.Controls.Clear();
                divFooterLink.Attributes.Add("style", "display:none;");
            }
            if (sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalShowCopyRight) == "1")
            {
                divCopyWrite.InnerHtml = Server.HtmlDecode(sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalCopyright));
                divCopyWrite.Attributes.Add("style", "display:block;");
            }
            else
            {
                divCopyWrite.InnerHtml = "";
                divCopyWrite.Attributes.Add("style", "display:none;");
            }
            
        }
    }
}
