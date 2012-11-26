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
using System.Data;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using SageFrame.Framework;
using SageFrame.UserManagement;
using SageFrame.Web;
using SageFrame.Message;
using SageFrameClass.MessageManagement;
using SageFrame.SageFrameClass.MessageManagement;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Security.Helpers;

namespace SageFrame.Modules.PasswordRecovery
{
    public partial class ctl_RecoverPassword : BaseUserControl
    {
        public string helpTemplate = string.Empty;
        SageFrameConfig pb = new SageFrameConfig();
        bool IsUseFriendlyUrls = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageTemplateDataContext dbMessageTemplate = new MessageTemplateDataContext(SystemSetting.SageFrameConnectionString);
            var template = dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.PASSWORD_RECOVERED_HELP, GetPortalID).SingleOrDefault();
            if (template != null)
            {
                helpTemplate = template.Body;
            }
            if (!IsPostBack)
            {
                string RecoveringCode = string.Empty;
                if (Request.QueryString["RecoveringCode"] != null)
                {
                    RecoveringCode = Request.QueryString["RecoveringCode"].ToString();
                    try
                    {
                        RecoveringCode = EncryptionMD5.Decrypt(RecoveringCode);
                        hdnRecoveryCode.Value = RecoveringCode;
                        AddImageUrls();
                        UserManagementDataContext dbUser = new UserManagementDataContext(SystemSetting.SageFrameConnectionString);
                        var sageframeuser = dbUser.sp_GetUsernameByActivationOrRecoveryCode(hdnRecoveryCode.Value, GetPortalID).SingleOrDefault();
                        if (sageframeuser.CodeForUsername != null)
                        {
                            if ((bool)(sageframeuser.IsAlreadyUsed))
                            {
                                ShowMessage("", GetSageMessage("PasswordRecovery", "RecoveryCodeAlreadyActivated"), "", SageMessageType.Alert);
                                divRecoverpwd.Visible = false;

                            }
                            else
                            {
                                divRecoverpwd.Visible = true;
                            }
                        }
                        else
                        {

                            divRecoverpwd.Visible = false;
                            ShowMessage("", GetSageMessage("UserManagement", "UserDoesNotExist"), "", SageMessageType.Alert);
                        }
                    }
                    catch
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("PasswordRecovery", "InvalidRecoveringCode"), "", SageMessageType.Alert);
                        divRecoverpwd.Visible = false;
                    }
                }
            
                else
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("PasswordRecovery", "RecoveringCodeIsNotAvailable"), "", SageMessageType.Error);
                    divRecoverpwd.Visible = false;
                }
                SetValidatorErrorMessage();
            }
        }
    

        private void AddImageUrls()
        {
            wzdPasswordRecover.CancelButtonImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            wzdPasswordRecover.StartNextButtonImageUrl = GetTemplateImageUrl("imgforward.png", true);
            wzdPasswordRecover.StepNextButtonImageUrl = GetTemplateImageUrl("imgforward.png", true);
            wzdPasswordRecover.StepPreviousButtonImageUrl = GetTemplateImageUrl("imgback.png", true);
            wzdPasswordRecover.FinishPreviousButtonImageUrl = GetTemplateImageUrl("imgback.png", true);
            wzdPasswordRecover.FinishCompleteButtonImageUrl = GetTemplateImageUrl("imgfinished.png", true);
        }

        private void SetValidatorErrorMessage()
        {
            rfvRecoveredPassword.Text = "*";
            rfvRetypePassword.Text = "*";
            cvPassword.ErrorMessage = GetSageMessage("PasswordRecovery", "PasswordDoesnotMatched");
            rfvRecoveredPassword.ErrorMessage = GetSageMessage("PasswordRecovery", "PasswordIsRequired");
            rfvRetypePassword.ErrorMessage = GetSageMessage("PasswordRecovery", "RetypePassword");
            cvPassword.Text = "*";
        }

        protected void wzdPasswordRecover_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            hdnRecoveryCode.Value = "";
            GotoLoginPage();
        }
        private void GotoLoginPage()
        {
            if (IsUseFriendlyUrls)
            {
                if (GetPortalID > 1)
                {
                    Response.Redirect(ResolveUrl("~/portal/" + GetPortalSEOName + "/" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + ".aspx"));
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + ".aspx"));
                }
            }
            else
            {
                Response.Redirect(ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage)));
            }
        }
        protected void wzdPasswordRecover_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                MessageTemplateDataContext dbMessageTemplate = new MessageTemplateDataContext(SystemSetting.SageFrameConnectionString);
                if (txtPassword.Text != null && txtRetypePassword.Text != "" && txtRetypePassword.Text == txtPassword.Text)
                {
                    if (txtPassword.Text.Length < 4)
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("PasswordRecovery", "PasswordLength"), "", SageMessageType.Alert);
                        e.Cancel = true;
                    }
                    else
                    {
                        if (hdnRecoveryCode.Value != "")
                        {
                            UserManagementDataContext dbUser = new UserManagementDataContext(SystemSetting.SageFrameConnectionString);
                            var sageframeuser = dbUser.sp_GetUsernameByActivationOrRecoveryCode(hdnRecoveryCode.Value, GetPortalID).SingleOrDefault();
                            if (sageframeuser.CodeForUsername != null)
                            {
                                MembershipController m = new MembershipController();
                                UserInfo sageUser = m.GetUserDetails(GetPortalID, sageframeuser.CodeForUsername);
                                //MembershipUser user = Membership.GetUser(sageframeuser.CodeForUsername);
                                string Password, PasswordSalt;
                                PasswordHelper.EnforcePasswordSecurity(m.PasswordFormat, txtPassword.Text, out Password, out PasswordSalt);
                                UserInfo user1 = new UserInfo(sageUser.UserID, Password, PasswordSalt, m.PasswordFormat);
                                m.ChangePassword(user1);
                                //string oldPassword = user.ResetPassword();

                                //user.ChangePassword(oldPassword, txtPassword.Text);

                              
                                var messageTemplates = dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.PASSWORD_RECOVERED_SUCCESSFUL_EMAIL, GetPortalID);
                                foreach (var messageTemplate in messageTemplates)
                                {
                                    MessageTokenDataContext messageTokenDB = new MessageTokenDataContext(SystemSetting.SageFrameConnectionString);
                                    var messageTokenValues = messageTokenDB.sp_GetPasswordRecoverySuccessfulTokenValue(sageUser.UserName, GetPortalID);
                                    CommonFunction comm = new CommonFunction();
                                    DataTable dtTokenValues = comm.LINQToDataTable(messageTokenValues);
                                    string replacedMessageSubject = MessageToken.ReplaceAllMessageToken(messageTemplate.Subject, dtTokenValues);
                                    string replacedMessageTemplate = MessageToken.ReplaceAllMessageToken(messageTemplate.Body, dtTokenValues);
                                    try
                                    {
                                    	MailHelper.SendMailNoAttachment(messageTemplate.MailFrom, sageUser.Email, replacedMessageSubject, replacedMessageTemplate, string.Empty, string.Empty);
                                    }
                                    catch (Exception)
                                    {

                                        ShowMessage("", GetSageMessage("PasswordRecovery", "SecureConnectionFPRError"), "", SageMessageType.Alert);
                                        e.Cancel = true;
                                        divRecoverpwd.Visible = false;

                                    }
                                }
                                UserManagementController.DeactivateRecoveryCode(sageUser.UserName, GetPortalID);
 							   var template = dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.PASSWORD_RECOVERED_SUCESSFUL_INFORMATION, GetPortalID).SingleOrDefault();
                                if (template != null)
                                {
                                    ((Literal)WizardStep2.FindControl("litPasswordChangedSuccessful")).Text = template.Body;
                                }
                            }
                            else
                            {
                                //var template = dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.PASSWORD_RECOVERED_SUCESSFUL_INFORMATION, GetPortalID).SingleOrDefault();
                                //if (template != null)
                                //{
                                //    ((Literal)WizardStep2.FindControl("litPasswordChangedSuccessful")).Text = template.Body;
                                //}
                                e.Cancel = true;
                                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("PasswordRecovery", "UnknownErrorPleaseTryAgaing"), "", SageMessageType.Alert);
                            }
                        }
                        else
                        {
                            //var template = dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.PASSWORD_RECOVERED_SUCESSFUL_INFORMATION, GetPortalID).SingleOrDefault();
                            //if (template != null)
                            //{
                            //    ((Literal)WizardStep2.FindControl("litPasswordChangedSuccessful")).Text = template.Body;
                            //}
                            e.Cancel = true;
                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("PasswordRecovery", "UnknownError"), "", SageMessageType.Alert);
                        }
                    }
                }
                else
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("PasswordRecovery", "PleaseEnterAllRequiredFields"), "", SageMessageType.Alert);
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            hdnRecoveryCode.Value = "";
            GotoLoginPage();
        }
    }
}