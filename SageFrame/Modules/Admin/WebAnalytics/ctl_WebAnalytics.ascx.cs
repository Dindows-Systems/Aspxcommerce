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


using SageFrame.Setting;
using SageFrame.Web;
using SageFrame.Shared;
using SageFrame.PortalSetting;
using SageFrame.Framework;

namespace SageFrame.Modules.Admin.WebAnalytics
{
    public partial class ctl_WebAnalytics : BaseAdministrationUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindData();
                    AddImageUrls();                                       
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void AddImageUrls()
        {
            imbSave.ImageUrl = GetTemplateImageUrl("btnudate.png", true);
            imbRefresh.ImageUrl = GetTemplateImageUrl("imgrefresh.png", true);
        }

        private void SaveSettings()
        {
            try
            {
                if (txtvalue.Text.Trim() != string.Empty)
                {
                    SettingProvider sp = new SettingProvider();
                    sp.GoogleAnalyticsAddUpdate(txtvalue.Text, chkIsActive.Checked, GetPortalID, GetUsername);
                    HttpContext.Current.Cache.Remove("SageGoogleAnalytics");
                    AlertUpdate();
                    BindData();
                }
                else
                {
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("WebAnalytics", "PleaseFillJSCodeProvidedByGoogle"), "", SageMessageType.Alert);
                }

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void AlertUpdate()
        {            
            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("WebAnalytics", "GoogleAnalyticsUpdatedSuccessfully"), "", SageMessageType.Success);
        }

        private void BindData()
        {
            try
            {
                txtvalue.Text = "";
                SettingProvider sp = new SettingProvider();
                GoogleAnalyticsInfo objGA = new GoogleAnalyticsInfo();
                objGA = sp.GetGoogleAnalyticsByPortalID(GetPortalID);
                if (objGA != null && objGA.GoogleJSCode != null)
                {                   
                    txtvalue.Text = objGA.GoogleJSCode;
                    chkIsActive.Checked = objGA.IsActive;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

        }

        protected void imbSave_Click(object sender, ImageClickEventArgs e)
        {
                SaveSettings();
        }

        protected void imbRefresh_Click(object sender, ImageClickEventArgs e)
        {
            BindData();
        }        
    }
}
