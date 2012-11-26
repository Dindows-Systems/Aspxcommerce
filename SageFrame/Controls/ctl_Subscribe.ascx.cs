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
using SageFrame.NewLetterSubscriber;

namespace SageFrame.Controls
{
    public partial class ctl_Subscribe : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubscribe_Click(object sender, EventArgs e)
        {
            try
            {
                NewLetterSubscriberDataContext dbNewLetterSubscribe = new NewLetterSubscriberDataContext(SystemSetting.SageFrameConnectionString);
                string clientIP = Request.UserHostAddress.ToString();
                string email = txtNewsLetterEmail.Text;
                System.Nullable<Int32> newID = 0;
                if ((string.IsNullOrEmpty(clientIP) && string.IsNullOrEmpty(email)))
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), "Email Address is required!", "", SageMessageType.Alert);
                }
                else
                {
                    dbNewLetterSubscribe.sp_NewLetterSubscribersAdd(ref newID, email, clientIP, true, GetUsername, DateTime.Now, GetPortalID);
                    if (newID > 0)
                    {
                        ShowMessage(SageMessageTitle.Information.ToString(), "Subscribe news letter successfully", "", SageMessageType.Alert);
                        txtNewsLetterEmail.Text = "Subscribe to our newsletter";
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), "All ready subscribe news letter!", "", SageMessageType.Alert);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
    }
}