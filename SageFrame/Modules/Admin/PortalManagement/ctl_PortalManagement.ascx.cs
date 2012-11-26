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
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.PortalSetting;
using System.Web.UI.HtmlControls;
using SageFrame.Framework;
using SageFrame.Security.Helpers;
using SageFrame.Web;
using SageFrame.Utilities;
using System.IO;
using System.Collections;
using SageFrame.Shared;
using SageFrame.SageFrameClass;
using SageFrame.Message;
using SageFrame.SageFrameClass.MessageManagement;
using SageFrame.Modules;
using System.Text;
using AspxCommerce.Core;

namespace SageFrame.Modules.Admin.PortalManagement
{
    public partial class ctl_PortalManagement : BaseAdministrationUserControl
    {
        PortalSettingDataContext dbPortal = new PortalSettingDataContext(SystemSetting.SageFrameConnectionString);
        ModulesDataContext dbPortalModules = new ModulesDataContext(SystemSetting.SageFrameConnectionString);
        SageFrameConfig pagebase = new SageFrameConfig();
        bool IsUseFriendlyUrls = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
                if (!IsPostBack)
                {
                    AddImageUrls();
                    BindPortal();
                    PanelVisibility(false, true);
                    imbBtnSaveChanges.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("PortalModules", "AreYouSureToSaveChanges") + "')");
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private void AddImageUrls()
        {
            imgCancelList.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            imgCancel.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            imgAdd.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            imgSave.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            imbBtnSaveChanges.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
        }
        private void BindPortal()
        {
            var portals = dbPortal.sp_PortalGetList();
            gdvPortal.DataSource = portals;
            gdvPortal.DataBind();
        }
        private void PanelVisibility(bool VisiblePortal, bool VisiblePortalList)
        {
            pnlPortal.Visible = VisiblePortal;
            pnlPortalList.Visible = VisiblePortalList;
            if (hdnPortalID.Value == "0")
            {
                TabContainerManagePortal.Tabs[1].Visible = false;
            }
            else
            {
                TabContainerManagePortal.Tabs[1].Visible = true;
            }
        }
        private void ClearForm()
        {
            txtEmail.Text = "";
            txtPortalName.Text = "";
        }
        protected void imgAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                trEmail.Visible = true;
                ClearForm();
                txtPortalName.Enabled = true;
                hdnPortalID.Value = "0";
                PanelVisibility(true, false);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgSave_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                if (Int32.Parse(hdnPortalID.Value) > 0)
                {
                    if (txtPortalName.Text.Trim() != "")
                    {
                        SaveProtal();
                    }
                    else
                    {
                        lblPortalNameError.Visible = true;
                    }
                }
                else
                {
                    if (txtEmail.Text.Trim() != "")
                    {
                        if (txtPortalName.Text.Trim() != "")
                        {
                            SaveProtal();
                        }
                        else
                        {
                            lblPortalNameError.Visible = true;
                        }
                    }
                    else
                    {
                        lblStoreEmailError.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindPortalSetting()
        {
            Hashtable hst = new Hashtable();
            SettingProvider sep = new SettingProvider();
            DataTable dt = sep.GetSettingsByPortal(GetPortalID.ToString(), string.Empty); //GetSettingsByPortal();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                }
            }
            HttpContext.Current.Cache.Insert("SageSetting", hst);
        }


        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                PanelVisibility(false, true);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvPortal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdnPortalID = (HiddenField)e.Row.FindControl("hdnPortalID");
                    HiddenField hdnSEOName = (HiddenField)e.Row.FindControl("hdnSEOName");
                    HiddenField hdnIsParent = (HiddenField)e.Row.FindControl("hdnIsParent");
                    HyperLink hypPortalPreview = (HyperLink)e.Row.FindControl("hypPortalPreview");
                    Label lblDefaultPage = (Label)e.Row.FindControl("lblDefaultPage");
                    hypPortalPreview.Text = "Preview";
                    if (IsUseFriendlyUrls)
                    {
                        if (hdnIsParent.Value.ToLower() != "true")
                        {
                            hypPortalPreview.NavigateUrl = ResolveUrl("~/portal/" + hdnSEOName.Value.ToLower() + "/" + lblDefaultPage.Text + ".aspx");
                        }
                        else
                        {
                            hypPortalPreview.NavigateUrl = ResolveUrl("~/" + lblDefaultPage.Text + ".aspx");
                        }
                    }
                    else
                    {
                        hypPortalPreview.NavigateUrl = ResolveUrl("~/Default.aspx?ptlid=" + hdnPortalID.Value + "&ptSEO=" + hdnSEOName.Value.ToLower() + "&pgnm=" + lblDefaultPage.Text);
                    }
                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    imgDelete.Attributes.Add("onclick", "return confirm('" + GetSageMessage("UserManagement", "AreYouSureYouWantToDelete") + "')");
                    HtmlInputCheckBox chkBoxIsParentItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsParentItem");
                    if (hdnIsParent != null && chkBoxIsParentItem != null)
                    {
                        chkBoxIsParentItem.Checked = bool.Parse(hdnIsParent.Value);
                    }
                    if (bool.Parse(hdnIsParent.Value) || Int32.Parse(hdnPortalID.Value) == GetPortalID)
                    {
                        imgDelete.Visible = false;
                    }


                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvPortal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Int32 portalID = Int32.Parse(e.CommandArgument.ToString());
                if (e.CommandName == "EditPortal")
                {
                    EditPortal(portalID);
                    PanelVisibility(true, false);
                }
                else if (e.CommandName == "DeletePortal")
                {
                    DeletePortal(portalID);
                    HttpContext.Current.Cache.Remove("Portals");
                    BindPortal();
                    PanelVisibility(false, true);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void EditPortal(Int32 portalID)
        {
            trEmail.Visible = false;
            var portal = dbPortal.sp_PortalGetByPortalID(portalID, GetUsername).SingleOrDefault();
            txtPortalName.Enabled = portal.Name.Equals("Default") ? false : true;
            txtPortalName.Text = portal.Name;
            hdnPortalID.Value = portalID.ToString();
            BindPortalModulesListsGrid(Int32.Parse(hdnPortalID.Value));
        }

        private void DeletePortal(Int32 portalID)
        {
            var portal = dbPortal.sp_PortalGetByPortalID(portalID, GetUsername).SingleOrDefault();
            txtPortalName.Text = portal.Name;
            dbPortal.sp_PortalDelete(portalID, GetUsername);
            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("PortalSettings", "PortalDeleteSuccessfully"), "", SageMessageType.Success);
        }

        private void SaveProtal()
        {
            try
            {
                string newpassword = GetRandomPassword(6);
                int passowrdformat = 2;
                string password;

                int? customerID = 0;
                string passwordsalt, portalUrl;
                string portalName = txtPortalName.Text.Trim().Replace(" ", "_");
                if (IsUseFriendlyUrls)
                {
                    portalUrl = Request.ServerVariables["SERVER_NAME"] + "/portal/" + portalName + "/" + "home.aspx";
                }
                else
                {
                    portalUrl = Request.ServerVariables["SERVER_NAME"] + "/portal/" + "home";
                }

                if (!(string.IsNullOrEmpty(txtPortalName.Text.Trim())))
                {
                    if (Int32.Parse(hdnPortalID.Value) > 0)
                    {
                        dbPortal.sp_PortalUpdate(Int32.Parse(hdnPortalID.Value), txtPortalName.Text.Trim(), false,
                                                 GetUsername);
                        ShowMessage(SageMessageTitle.Information.ToString(),
                                    GetSageMessage("PortalSettings", "PortalSaveSuccessfully"), "",
                                    SageMessageType.Success);

                    }
                    else
                    {
                        PasswordHelper.EnforcePasswordSecurity(passowrdformat, newpassword, out password,
                                                               out passwordsalt);
                        string email = txtEmail.Text.Trim();
                        dbPortal.usp_Aspx_AddStoreSubscriber(portalName, "", "", email, null, false, false, "superuser",
                                                             password, passwordsalt, passowrdformat, false,
                                                             ref customerID);
                        try
                        {
                            sendEmail(customerID, newpassword, portalName, txtEmail.Text.Trim(), "superuser", "",
                                      portalUrl);
                            ShowMessage(SageMessageTitle.Information.ToString(),
                                        GetSageMessage("PortalSettings", "PortalSavedAndEmailSendSuccessfully"), "",
                                        SageMessageType.Success);
                        }
                        catch
                        {
                            ShowMessage(SageMessageTitle.Notification.ToString(),
                                        GetSageMessage("PortalSettings", "PortalAddEmailSendProblem"), "",
                                        SageMessageType.Alert);
                        }
                    }
                    BindPortalSetting();
                    HttpContext.Current.Cache.Remove("Portals");
                    BindPortal();
                    PanelVisibility(false, true);
                    HttpContext.Current.Cache.Remove("SageSetting");
                    SageFrameConfig sf = new SageFrameConfig();
                    sf.ResetSettingKeys(int.Parse(this.hdnPortalID.Value.ToString()));
                }
            }
            catch (Exception)
            {
                ShowMessage(SageMessageTitle.Notification.ToString(),
                            GetSageMessage("PortalSettings", "PortalAlreadyExist"), "", SageMessageType.Alert);
            }
        }

        private void BindPortalModulesListsGrid(int PortalID)
        {
            gdvPortalModulesLists.DataSource = dbPortalModules.sp_PortalModulesGetByPortalID(PortalID, GetUsername);
            gdvPortalModulesLists.DataBind();
        }

        protected void gdvPortalModulesLists_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvPortalModulesLists.PageIndex = e.NewPageIndex;
            BindPortalModulesListsGrid(Int32.Parse(hdnPortalID.Value));
        }

        protected void gdvPortalModulesLists_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnIsActive = (HiddenField)e.Row.FindControl("hdnIsActive");
                HiddenField hdnIsAdmin = (HiddenField)e.Row.FindControl("hdnIsAdmin");

                HtmlInputCheckBox chkIsActiveItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveItem");
                chkIsActiveItem.Attributes.Add("onclick", "javascript:Check(this,'cssCheckBoxIsActiveHeader','" + gdvPortalModulesLists.ClientID + "','cssCheckBoxIsActiveItem');");
                chkIsActiveItem.Checked = bool.Parse(hdnIsActive.Value);
                if (bool.Parse(hdnIsAdmin.Value))
                {
                    chkIsActiveItem.Disabled = true;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                HtmlInputCheckBox chkIsActiveHeader = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveHeader");
                chkIsActiveHeader.Attributes.Add("onclick", "javascript:SelectAllCheckboxesSpecific(this,'" + gdvPortalModulesLists.ClientID + "','cssCheckBoxIsActiveItem');");
            }
        }

        protected void gdvPortalModulesLists_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gdvPortalModulesLists_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gdvPortalModulesLists_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gdvPortalModulesLists_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void imbBtnSaveChanges_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string seletedModulesID = string.Empty;
                string IsActive = string.Empty;
                int SelectedPortalID = Int32.Parse(hdnPortalID.Value);

                for (int i = 0; i < gdvPortalModulesLists.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem = (HtmlInputCheckBox)gdvPortalModulesLists.Rows[i].FindControl("chkBoxIsActiveItem");
                    HiddenField hdnModuleID = (HiddenField)gdvPortalModulesLists.Rows[i].FindControl("hdnModuleID");
                    seletedModulesID = seletedModulesID + hdnModuleID.Value.Trim() + ",";
                    IsActive = IsActive + (chkBoxItem.Checked ? "1" : "0") + ",";
                }
                if (seletedModulesID.Length > 1 && IsActive.Length > 0)
                {
                    seletedModulesID = seletedModulesID.Substring(0, seletedModulesID.Length - 1);
                    IsActive = IsActive.Substring(0, IsActive.Length - 1);
                    dbPortalModules.sp_PortalModulesUpdate(seletedModulesID, IsActive, SelectedPortalID, GetUsername);
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("PortalModules", "SelectedChangesAreSavedSuccessfully"), "", SageMessageType.Success);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        public string GetRandomPassword(int length)
        {
            char[] chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            string password = string.Empty;
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int x = random.Next(1, chars.Length);
                //Don't Allow Repetation of Characters
                if (!password.Contains(chars.GetValue(x).ToString()))
                    password += chars.GetValue(x);
                else
                    i--;
            }
            return password;
        }

        public void sendEmail(int? custID, string newpassword, string portalName, string userEmail, string firstname, string lastname, string portalUrl)
        {
            StringBuilder emailbody = new StringBuilder();
            portalUrl = "http://" + portalUrl;
            emailbody.Append(
                "<table align=\"center\" width=\"700\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"background-color: #f5f5f5;font: 12px Arial, Helvetica, sans-serif;color: #797979;text-shadow: 1px 1px 0px #fff;\"> <tbody> <tr> <td> <table width=\"700\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"margin:0px auto; background: #f5f5f5\"> <tbody> <tr> <td valign=\"top\" colspan=\"1\"> <table width=\"622\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"margin:0px auto; background=#f5f5f5\"> <tbody> <tr> <td width=\"473\"><a href=\"#\"><img width=\"148\" height=\"81\" style=\"border:none;\" alt=\"logo\" src=\"http://www.aspxcommerce.com/Upload/image/logo.png\" /></a></td> <td width=\"139\" style=\"text-align:right\">&nbsp;</td> </tr> <tr> <td colspan=\"2\">&nbsp;</td> </tr> <tr> <td colspan=\"2\"> <p style=\"margin:10px 0 10px 0; line-height:20px\">Dear <b>");

            emailbody.Append(firstname + " " + lastname + ",");
            emailbody.Append(
                "<br /></b></p></td></tr><tr><td valign=\"top\" colspan=\"2\">&nbsp;</td> </tr> <tr> <td valign=\"top\" colspan=\"2\"> <p style=\"margin:0 0 15px 0\"><a target=\"_blank\" href=\"http://www.aspxcommerce.com\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" title=\"AspxCommerce\"><span style=\"mso-bidi-font-family: Arial;color:black\">Congratulation, Your AspxCommerce Store has been created successfully.</span></a></p> </td> </tr> <tr> <td valign=\"top\" colspan=\"2\"> <p style=\"margin:0px\">&nbsp;</p> </td> </tr> <tr> <td height=\"15\" colspan=\"2\">To sign in to your account, please visit <a target='_blank' href='" + portalUrl + "'>" + portalUrl + "</a>");
            emailbody.Append(
                "</td></tr><tr><td height=\"15\" colspan=\"2\"><br /><strong>Your Store Login Information: </strong></td> </tr> <tr> <td height=\"15\" colspan=\"2\"> Your Store Url : ");
            emailbody.Append("<a target='_blank' href='" + portalUrl + "'>" + portalUrl + "</a>");
            emailbody.Append(
                "</td></tr><tr><td colspan=\"2\">&nbsp;</td></tr><tr><tr><td height=\"15\" colspan=\"2\"><strong> For Frontend Login: </strong></td></tr><td height=\"15\" colspan=\"2\">UserName: ");
            emailbody.Append("customer_" + custID.ToString());
            emailbody.Append("</td></tr><tr><td height=\"15\" colspan=\"2\"> Password: ");
            emailbody.Append(newpassword);

            emailbody.Append("</td></tr><tr><td colspan=\"2\">&nbsp;</td></tr><tr><tr><td height=\"15\" colspan=\"2\"><strong> For Backend Login: </strong></td> </tr><td height=\"15\" colspan=\"2\"> Username: ");

            emailbody.Append("storeadmin_" + custID.ToString());
            emailbody.Append("</td></tr><tr><td height=\"15\" colspan=\"2\"> Password: ");
            emailbody.Append(newpassword);

            emailbody.Append(
                "</td></tr><tr><td colspan=\"2\">&nbsp;</td></tr><tr><td colspan=\"2\"><strong>If you have any questions regarding your account, click 'Reply' in your email client and we'll be only too happy to help. </strong></td></tr><tr><td colspan=\"2\">&nbsp;</td></tr><tr><td colspan=\"2\"><table width=\"661\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"> <tbody> <tr> <td width=\"217\">Best Regards,<br /> <strong style=\"color:#282929\">AspxCommerce Team</strong><br /> <a title=\"AspxCommerce\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" href=\"http://www.aspxcommerce.com\" target=\"_blank\">http://www.aspxcommerce.com</a></td> <td width=\"401\">&nbsp;</td> </tr> <tr> <td style=\"border-top:0px solid #cacaca; font: 10px Arial, Helvetica, sans-serif;\" colspan=\"2\">&nbsp;</td> </tr> <tr> <td style=\"border-top:1px solid #cacaca; font: 5px Arial, Helvetica, sans-serif;\" colspan=\"2\">&nbsp;</td> </tr> </tbody> </table> </td> </tr> <tr> <td colspan=\"1\"><span style=\"font: italic 11px Arial, Helvetica, sans-serif\">This message is confidential and intended for the recipient only. It is not allowed to copy this message, or to make it accessible for third parties. If you are not the intended recipient, please notify the sender by email.</span></td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table>");


            //emailbody.Append("<div style=\"width:600px;min-height:auto;background-color:#f2f2f2;color:#707070;font:13px/24px Verdana,Geneva,sans-serif;\"><div style=\"color:#49B8F4;font-size:20px;margin:auto 0;font-family:Verdana,Geneva,sans-serif\"> <label>");
            //emailbody.Append("Thank you for Subscribing AspxCommerce </label></div><p> <strong>Dear </strong> ");
            //emailbody.Append(" " + firstname + " " + lastname + "</p><div> Your <strong>AspxCommerce</strong> Login Information: <br/> Thank You for using <strong>Free 30 Days AspxCommerce Demo Store</strong>.<br/>Your Portal Url : <a href=\"" + portalUrl + "\" target=\"_blank\" style=\"color: rgb(39, 142, 230);\" >" + portalUrl + "</a>  <br /> <strong>Frontend Login </strong> <br/> UserName: customer_" + custID.ToString() + " <br/> Password: ");
            //emailbody.Append(newpassword + "<br /> <strong>For Backend Login </strong><br /> Username: storeadmin_" + custID.ToString() + " <br/> Password: " + newpassword + "</div><div>If you are having any issues with login or any further inquiry then you can mail <a target=\"_blank\" href=\"mailto:info@AspxCommerce.com\" >info@AspxCommerce.com</a>.</div>");
            //emailbody.Append("<div> Please do not reply to this email. This mail is automatically generated after you subscribed.</div><div>Thank You <br/> AspxCommerce Team</div></div>");
            StoreSettingConfig ssc = new StoreSettingConfig();
            string sendEmailFrom = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, GetStoreID, GetPortalID, GetCurrentCultureName);

            SageFrameConfig pagebase = new SageFrameConfig();
            string emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);//milsonmun@hotmail.com";
            string emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
            MailHelper.SendMailNoAttachment(sendEmailFrom, userEmail, "Thank You, for subscribing with AspxCommerce.", emailbody.ToString(), emailSuperAdmin, emailSiteAdmin);
        }

    }
}