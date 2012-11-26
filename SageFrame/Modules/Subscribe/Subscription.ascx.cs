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
using System.Web.UI.HtmlControls;
using SageFrame.Web.Utilities;

public partial class Modules_Subscribe_Subscription : BaseUserControl
{

    public Int32 usermoduleIDControl = 0;
    NewsLetterSettingsInfo newsLetterSettingObj = new NewsLetterSettingsInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            usermoduleIDControl = Int32.Parse(SageUserModuleID);
            GetNewsLetterSettings();
            //if (!Page.IsPostBack)
            //{                
            BindToNewsDesign();
            //}
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void GetNewsLetterSettings()
    {
        List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
        ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", usermoduleIDControl));
        ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
        SQLHandler objSql = new SQLHandler();
        newsLetterSettingObj = objSql.ExecuteAsObject<NewsLetterSettingsInfo>("dbo.sp_NewsLetterSettingsGetAll", ParaMeterCollection);
        lblHelpText.Text = "<p>" + newsLetterSettingObj.SubscriptionHelpText.ToString() + "</p>";
        txtWatermarkExtender.WatermarkText = newsLetterSettingObj.TextBoxWaterMarkText.ToString() != "" ? newsLetterSettingObj.TextBoxWaterMarkText.ToString() : "Email Address Required";
        btnSubscribe.Text = newsLetterSettingObj.SubmitButtonText.ToString();
        btnSubscribe.ToolTip = newsLetterSettingObj.SubmitButtonText.ToString();
    }

    private void BindToNewsDesign()
    {
        try
        {
            if (newsLetterSettingObj.SubscriptionType == "0")
            {
                mainContainer.Visible = false;
            }
            else
            {
                newsLetter.Visible = false;
                lblHelpTextModal.Visible = true;
                lblTitleModal.Text = newsLetterSettingObj.SubscriptionModuleTitle;
                lblHelpTextModal.Text = newsLetterSettingObj.SubscriptionHelpText;
                txtWatermarkExtenderModal.WatermarkText = newsLetterSettingObj.TextBoxWaterMarkText;
                btnSubscribeModal.Text = newsLetterSettingObj.SubmitButtonText;

                // HtmlGenericControl Top1Info = new HtmlGenericControl("div");
                //// Top1Info.Attributes.Add("class", "cssClassRightBox cssClassLatestForumActivity");
                // Top1Info.Attributes.Add("class", "cssClassNewsLetter");
                // HtmlGenericControl Top2Info = new HtmlGenericControl("div");
                // Top2Info.Attributes.Add("class", "cssClassRightBox_TopBg");

                // HtmlGenericControl Top3Info = new HtmlGenericControl("div");
                // Top3Info.Attributes.Add("class", "cssClassRightBox_BtnBg");

                // HtmlGenericControl Top4Info = new HtmlGenericControl("div");
                // Top4Info.Attributes.Add("class", "cssClassRightBox_MidBg");

                // HtmlGenericControl Top5Info = new HtmlGenericControl("h1");
                // Label title = new Label();
                // title.Text = newsLetterSettingObj.SubscriptionModuleTitle;
                // Top5Info.Controls.Add(title);              
                // Top4Info.Controls.Add(Top5Info);
                // Top4Info.Controls.Add(newsLetter);
                // Top3Info.Controls.Add(Top4Info);
                // Top2Info.Controls.Add(Top3Info);
                // Top1Info.Controls.Add(Top2Info);
                // mainContainer.Controls.Add(Top1Info);
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void btnSubscribe_Click(object sender, EventArgs e)
    {
        try
        {
            NewLetterSubscriberDataContext dbNewLetterSubscribe = new NewLetterSubscriberDataContext(SystemSetting.SageFrameConnectionString);
            string clientIP = Request.UserHostAddress.ToString();
            string email=string.Empty;
            if (newsLetterSettingObj.SubscriptionType == "0")
            {
                email = txtNewsLetterEmail.Text;
            }
            else
            {
                email = txtEmailModal.Text;
            }
          
            System.Nullable<Int32> newID = 0;
            if ((string.IsNullOrEmpty(clientIP) && string.IsNullOrEmpty(email)))
            {
                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("NewsLetterSubscription", "EmailIsRequired"), "", SageMessageType.Alert);
            }
            else
            {
                dbNewLetterSubscribe.sp_NewLetterSubscribersAdd(ref newID, email, clientIP, true, GetUsername, DateTime.Now, GetPortalID);
                if (newID > 0)
                {
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("NewsLetterSubscription", "SubscribeSuccessfully"), "", SageMessageType.Success);
                    txtNewsLetterEmail.Text = "Subscribe to our newsletter";
                }
                else
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("NewsLetterSubscription", "AlreadySubscribed"), "", SageMessageType.Alert);
                }
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
