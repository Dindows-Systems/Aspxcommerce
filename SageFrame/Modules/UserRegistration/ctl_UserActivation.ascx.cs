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
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Framework;
using SageFrame.UserManagement;
using SageFrame.Message;
using SageFrameClass.MessageManagement;
using SageFrame.SageFrameClass.MessageManagement;
using SageFrame.Security;
using SageFrame.Security.Entities;
using System.Web.Security;
using SageFrame.Security.Helpers;

namespace SageFrame.Modules.UserRegistration
{
    public partial class ctl_UserActivation : BaseUserControl
    {
        MessageTokenDataContext messageTokenDB = new MessageTokenDataContext(SystemSetting.SageFrameConnectionString);
        MembershipController _member = new MembershipController();
       // public string LoginPath = "";
        bool IsUseFriendlyUrls = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            SageFrameConfig pagebase = new SageFrameConfig();
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            hypGotoLogin.Text = GetSageMessage("UserRegistration", "GoToLogin");
            if (IsUseFriendlyUrls)
            {
                if (GetPortalID > 1)
                {
                    hypGotoLogin.NavigateUrl = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + ".aspx");
                }
                else
                {
                    hypGotoLogin.NavigateUrl = ResolveUrl("~/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + ".aspx");
                }
            }
            else
            {
                hypGotoLogin.NavigateUrl = ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage));
            }
            if (!IsPostBack)
            {
                try
                {
                    MessageTemplateDataContext dbMessageTemplate = new MessageTemplateDataContext(SystemSetting.SageFrameConnectionString);
                    
                    string ActivationCode = string.Empty;
                    if (Request.QueryString["ActivationCode"] != null)
                    {
                        ActivationCode = Request.QueryString["ActivationCode"].ToString();
                        try
                        {
                            ActivationCode = EncryptionMD5.Decrypt(ActivationCode);
                        }
                        catch
                        {
                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserRegistration", "InvalidActivationCode"), "", SageMessageType.Alert);
                            return;
                        }

                        UserManagementDataContext dbUser = new UserManagementDataContext(SystemSetting.SageFrameConnectionString);
                        var sageframeuser = dbUser.sp_GetUsernameByActivationOrRecoveryCode(ActivationCode, GetPortalID).SingleOrDefault();
                        if (sageframeuser.CodeForUsername != null)
                        {

                            if (!(bool)(sageframeuser.IsAlreadyUsed))
                            {
                                string UserName = _member.ActivateUser(ActivationCode, GetPortalID, GetStoreID);
                                if (!String.IsNullOrEmpty(UserName))
                                {
                                    UserInfo user = _member.GetUserDetails(GetPortalID, UserName);
                                    if (user.UserExists)
                                    {
                                        var messageTemplates = dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.ACTIVATION_SUCCESSFUL_EMAIL, GetPortalID);
                                        foreach (var messageTemplate in messageTemplates)
                                        {
                                            var linqActivationTokenValues = messageTokenDB.sp_GetActivationSuccessfulTokenValue(user.UserName, GetPortalID);
                                            CommonFunction comm = new CommonFunction();
                                            DataTable dtActivationSuccessfulTokenValues = comm.LINQToDataTable(linqActivationTokenValues);
                                            string replaceMessageSubject = MessageToken.ReplaceAllMessageToken(messageTemplate.Subject, dtActivationSuccessfulTokenValues);
                                            string replacedMessageTemplate = MessageToken.ReplaceAllMessageToken(messageTemplate.Body, dtActivationSuccessfulTokenValues);
                                            try
                                            {
                                                MailHelper.SendMailNoAttachment(messageTemplate.MailFrom, user.Email, replaceMessageSubject, replacedMessageTemplate, string.Empty, string.Empty);
                                            }
                                            catch (Exception)
                                            {

                                                ShowMessage("", GetSageMessage("UserRegistration", "SecureConnectionUAEmailError"), "", SageMessageType.Alert);
                                                return;
                                            }
                                        }
                                        ACTIVATION_INFORMATION.Text = GetSageMessage("UserRegistration", "ActivationSuccessfulInfo");
                                        //ShowMessage("", GetSageMessage("UserRegistration", "ActivationSuccessfulInfo"), "", SageMessageType.Alert);
                                    }
                                    else
                                    {
                                        ShowMessage("", GetSageMessage("UserManagement", "UserDoesNotExist"), "", SageMessageType.Alert);
                                    }
                                }
                                else
                                {
                                    var template = dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.ACTIVATION_FAIL_INFORMATION, GetPortalID).SingleOrDefault();
                                    if (template != null)
                                    {
                                        ACTIVATION_INFORMATION.Text = template.Body;
                                    };
                                }
                            }
                            else
                            {
                                ShowMessage("", GetSageMessage("UserRegistration", "ActivationCodeAlreadyUsed"), "", SageMessageType.Alert);
                            }
                        }
                        else
                        {

                            ShowMessage("", GetSageMessage("UserManagement", "UserDoesNotExist"), "", SageMessageType.Alert);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        protected string GetActivationCode(string pagePath)
        {
            string ActivationCode = string.Empty;
            if (string.IsNullOrEmpty(ActivationCode))
            {
                ActivationCode = "Home";
            }
            else
            {
                string[] pagePaths = pagePath.Split('/');
                ActivationCode = pagePaths[pagePaths.Length - 1];
                if (string.IsNullOrEmpty(ActivationCode))
                {
                    ActivationCode = pagePaths[pagePaths.Length - 2];
                }
                ActivationCode = ActivationCode.Replace(".aspx", "");

            }
            return ActivationCode;
        }

        //private void RedirectToLoginPage(UserInfo user)
        //{
        //    //string strRoles = string.Empty;           
        //    //RoleController role = new RoleController();
        //    SageFrameConfig sfConfig = new SageFrameConfig();

        //    //        string userRoles = role.GetRoleNames(user.UserName, GetPortalID);
        //    //        strRoles += userRoles;
        //    //        if (strRoles.Length > 0)
        //    //        {
        //    //            SetUserRoles(strRoles);
        //    //            SessionTracker sessionTracker = (SessionTracker)Session["Tracker"];
        //    //            sessionTracker.PortalID = GetPortalID.ToString();
        //    //            sessionTracker.Username = user.UserName;
        //    //            Session["Tracker"] = sessionTracker;
        //    //            SageFrame.Web.SessionLog SLog = new SageFrame.Web.SessionLog();
        //    //            SLog.SessionTrackerUpdateUsername(sessionTracker, sessionTracker.Username, GetPortalID.ToString());
        //    //            {
        //    //                FormsAuthentication.SetAuthCookie(user.UserName, true);
        //    bool IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
        //    if (IsUseFriendlyUrls)
        //    {
        //        if (GetPortalID > 1)
        //        {
        //            Response.Redirect(ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + ".aspx"), false);
        //        }
        //        else
        //        {
        //            Response.Redirect(ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + ".aspx"), false);
        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect(ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage)), false);
        //    }

        //}

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