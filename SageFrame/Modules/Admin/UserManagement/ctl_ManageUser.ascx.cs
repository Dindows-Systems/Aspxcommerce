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
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using SageFrame.Message;
using SageFrame.SageFrameClass.MessageManagement;
using SageFrame.Web;
using SageFrame.RolesManagement;
using SageFrame.UserManagement;
using SageFrame.Security.Entities;
using SageFrame.Security.Crypto;
using SageFrame.Security.Helpers;
using SageFrame.Security.Providers;
using SageFrame.Security;
using System.Text.RegularExpressions;
using SageFrame.Security.Enums;
using SageFrameClass.MessageManagement;

namespace SageFrame.Modules.Admin.UserManagement
{
    
    public partial class ctl_ManageUser : BaseAdministrationUserControl
    {
        UserManagementDataContext dbUser = new UserManagementDataContext(SystemSetting.SageFrameConnectionString);
        MembershipController m = new MembershipController();
        RoleController role = new RoleController();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    aceSearchText.CompletionSetCount = GetPortalID;
                    BindRolesInListBox(lstAvailableRoles);
                    BindUsers(string.Empty);
                    PanelVisibility(false, true, false);
                    pnlSettings.Visible = false;                    
                    BindRolesInDropDown(ddlSearchRole);
                    AddImageUrls();
                    imgBtnSaveChanges.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("UserManagement", "AreYouSureToSaveChanges") + "')");
                    
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
       
        private void AddImageUrls()
        {
            imgBack.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            imgUserInfoSave.ImageUrl = GetTemplateImageUrl("imgupdate.png", true);
            imgManageRoleSave.ImageUrl = GetTemplateImageUrl("imgupdate.png", true);
            imgSearch.ImageUrl = GetTemplateImageUrl("imgpreview.png", true);
            imgAddUser.ImageUrl = GetTemplateImageUrl("imgadduser.png", true);
            imbBackinfo.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            imgBtnDeleteSelected.ImageUrl = GetTemplateImageUrl("imgdelete.png", true);
            imgBtnSaveChanges.ImageUrl = GetTemplateImageUrl("imgupdate.png", true);
            imbCreateUser.ImageUrl = GetTemplateImageUrl("btnadduser1.png", true);           
            imgBtnSettings.ImageUrl = GetTemplateImageUrl("settings.png", true);  
        }

        private void PanelVisibility(bool VisibleUserPanel, bool VisibleUserListPanel, bool VisibleManageUserPanel)
        {
            pnlUser.Visible = VisibleUserPanel;
            pnlUserList.Visible = VisibleUserListPanel;
            pnlManageUser.Visible = VisibleManageUserPanel;
        }

        private DataTable GetAllRoles()
        {
            DataTable dtRole = new DataTable();
            dtRole.Columns.Add("RoleID");
            dtRole.Columns.Add("RoleName");
            dtRole.AcceptChanges();
            RolesManagementDataContext dbRoles = new RolesManagementDataContext(SystemSetting.SageFrameConnectionString);
            var roles = dbRoles.sp_PortalRoleList(GetPortalID,false,GetUsername);
            foreach (var role in roles)
            {
                string roleName = role.RoleName;
                if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                {
                    DataRow dr = dtRole.NewRow();
                    dr["RoleID"] = role.RoleID;
                    dr["RoleName"] = roleName;
                    dtRole.Rows.Add(dr);
                }
                else
                {
                    string rolePrefix = GetPortalSEOName + "_";
                    roleName = roleName.Replace(rolePrefix, "");
                    DataRow dr = dtRole.NewRow();
                    dr["RoleID"] = role.RoleID;
                    dr["RoleName"] = roleName;
                    dtRole.Rows.Add(dr);
                }
            }
            return dtRole;
        }

        private void BindRolesInListBox(ListBox lst)
        {
            DataTable dtRoles = GetAllRoles();
            lst.DataSource = dtRoles;
            lst.DataTextField = "RoleName";
            lst.DataValueField = "RoleName";
            lst.DataBind();
        }

        private void BindRolesInDropDown(DropDownList ddl)
        {
            DataTable dtRoles = GetAllRoles();
            ddl.DataSource = dtRoles;
            ddl.DataTextField = "RoleName";
            ddl.DataValueField = "RoleID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("<Not Specified >", ""));
        }

        private void BindUsers(string searchText)
        {
            string RoleID = ddlSearchRole.SelectedValue.ToString();
            List<UserInfo> lstUsers = m.SearchUsers(RoleID, searchText, GetPortalID, GetUsername).UserList;           
            ViewState["UserList"] = lstUsers;
            gdvUser.PageSize = int.Parse(ddlRecordsPerPage.Text);
            gdvUser.DataSource =ReorderUserList(lstUsers);
            gdvUser.DataBind();
        }

        private List<UserInfo> ReorderUserList(List<UserInfo> lstUsers)
        {
            List<UserInfo> lstNewUsers = new List<UserInfo>();
            foreach (UserInfo user in lstUsers)
            {
                if (Regex.Replace(user.UserName.ToLower(), @"\s", "") == "superuser")
                {
                    lstNewUsers.Insert(0, user);
                }                
                else
                {
                    lstNewUsers.Add(user);
                }
            }
            return lstNewUsers;
        }

        private void CheckForSuperuser(ref List<UserInfo> lstUsers)
        {
            foreach (UserInfo obj in lstUsers)
            {
                if (obj.UserName.ToLower().Equals("superuser"))
                {
                    lstUsers.Remove(obj);
                }
            }
        }

        protected void imgAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                PanelVisibility(true, false, false);
                ClearForm();
                lstAvailableRoles.SelectedIndex = lstAvailableRoles.Items.IndexOf(lstAvailableRoles.Items.FindByValue("Registered User"));
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }       

 

        private string GetListBoxText(ListBox lstBox)
        {
            string selectedRoles = string.Empty;
            foreach (ListItem li in lstBox.Items)
            {
                string roleName = li.Text;
                if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                {
                    selectedRoles += roleName + ",";
                }
                else
                {                   
                    selectedRoles += roleName + ",";
                }
            }
            if (selectedRoles.Length > 0)
            {
                selectedRoles = selectedRoles.Substring(0, selectedRoles.Length - 1);
            }
            return selectedRoles;
        }

        private string SelectedRoles()
        {
            string selectedRoles = string.Empty;
            foreach (ListItem li in lstAvailableRoles.Items)
            {
                if (li.Selected == true)
                {
                    string roleName = li.Text;
                    if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                    {
                        selectedRoles += roleName + ",";
                    }
                    else
                    {
                        string rolePrefix = GetPortalSEOName + "_";
                         selectedRoles += rolePrefix + roleName + ",";
                    }
                }
            }
            if (selectedRoles.Length > 0)
            {
                selectedRoles = selectedRoles.Substring(0, selectedRoles.Length - 1);
            }
            return selectedRoles;
        }

        private void ClearForm()
        {
          txtUserName.Text = "";
          txtFirstName.Text = "";
          txtLastName.Text = "";
          txtEmail.Text = "";
          txtPassword.Text = "";
          txtRetypePassword.Text = "";
          txtSecurityQuestion.Text = "";
          txtSecurityAnswer.Text = "";
        }      

        protected void imbFinish_Click(object sender, EventArgs e)
        {
            try
            {
                BindUsers(string.Empty);
                PanelVisibility(false, true, false);               
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {         
            try
            {
                int rownum = Convert.ToInt32(e.CommandArgument);
                string username = gdvUser.DataKeys[rownum]["Username"].ToString();
                string userId = gdvUser.DataKeys[rownum]["UserId"].ToString();

                hdnEditUsername.Value = username;
                hdnEditUserID.Value = userId;

                switch (e.CommandName)
                {
                    case "EditUser":
                        string[] userRoles = Roles.GetRolesForUser(username);

                        UserInfo sageUser = m.GetUserDetails(GetPortalID, hdnEditUsername.Value);
                        hdnCurrentEmail.Value = sageUser.Email;
                        txtManageEmail.Text = sageUser.Email;
                        txtManageFirstName.Text = sageUser.FirstName;
                        txtManageLastName.Text = sageUser.LastName;
                        txtManageUsername.Text = sageUser.UserName;
                        chkIsActive.Checked = sageUser.IsApproved == true ? true : false;

                        if (SystemSetting.SYSTEM_USERS.Contains(hdnEditUsername.Value) || hdnEditUsername.Value == GetUsername)
                        {
                            chkIsActive.Enabled = false;
                            chkIsActive.Attributes.Add("class", "disabledClass");
                        }

                        txtCreatedDate.Text = sageUser.AddedOn.ToString();
                        txtLastActivity.Text = sageUser.LastActivityDate.ToShortDateString();
                        txtLastLoginDate.Text = sageUser.LastLoginDate.ToShortDateString();
                        txtLastPasswordChanged.Text = sageUser.LastPasswordChangeDate.ToShortDateString();
                        lstSelectedRoles.Items.Clear();
                        lstUnselectedRoles.Items.Clear();
                        RolesManagementDataContext dbRoles = new RolesManagementDataContext(SystemSetting.SageFrameConnectionString);
                        var roles = dbRoles.sp_PortalRoleList(GetPortalID, false, GetUsername);
                        foreach (var role in roles)
                        {
                            string roleName = role.RoleName;
                            if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                            {
                                if (userRoles.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                                {
                                    lstSelectedRoles.Items.Add(new ListItem(roleName, roleName));
                                }
                                else
                                {
                                    lstUnselectedRoles.Items.Add(new ListItem(roleName, roleName));
                                }
                            }
                            else
                            {
                                if (userRoles.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                                {
                                    string rolePrefix = GetPortalSEOName + "_";
                                    roleName = roleName.Replace(rolePrefix, "");
                                    lstSelectedRoles.Items.Add(new ListItem(roleName, roleName));
                                }
                                else
                                {
                                    string rolePrefix = GetPortalSEOName + "_";
                                    roleName = roleName.Replace(rolePrefix, "");
                                    lstUnselectedRoles.Items.Add(new ListItem(roleName, roleName));
                                }
                            }
                        }
                        PanelVisibility(false, false, true);
                        userProfile1.EditUserName = hdnEditUsername.Value;
                        break;
                    case "DeleteUser":
                        if (hdnEditUsername.Value != "")
                        {
                            UserInfo user = new UserInfo(hdnEditUsername.Value, GetPortalID, Membership.ApplicationName, GetUsername, GetStoreID);
                            m.DeleteUser(user);
                            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("UserManagement", "UserDeletedSuccessfully"), "", SageMessageType.Success);
                            BindUsers(string.Empty);
                        }
                        else
                        {
                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "SelectDeleteButtonOnceAgain"), "", SageMessageType.Alert);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgUserInfoSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnEditUsername.Value != "")
                {
                    if (txtManageFirstName.Text != "" && txtManageLastName.Text != "" && txtManageEmail.Text != "")
                    {
                        MembershipUser member = Membership.GetUser(hdnEditUsername.Value);
                        member.Email = txtManageEmail.Text;
                        
                        if (!EmailAddressExists(txtManageEmail.Text,m.RequireUniqueEmail))
                        {
                            UserInfo user = new UserInfo(Membership.ApplicationName, hdnEditUsername.Value,new Guid(hdnEditUserID.Value), txtManageFirstName.Text,txtManageLastName.Text, txtManageEmail.Text, GetPortalID,chkIsActive.Checked, GetUsername, GetStoreID);
                            UserUpdateStatus status=new UserUpdateStatus();
                            m.UpdateUser(user, out status);
                            if (status == UserUpdateStatus.DUPLICATE_EMAIL_NOT_ALLOWED)
                            {
                                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "EmailAddressAlreadyIsInUse"), "", SageMessageType.Alert);
                                
                            }
                            else if (status == UserUpdateStatus.USER_UPDATE_SUCCESSFUL)
                            {
                                BindUsers(string.Empty);
                                if(user.IsActive)
                                {
                                    SendActivateMail(user);
                                }
                                ShowMessage(SageMessageTitle.Notification.ToString(),GetSageMessage("UserManagement", "UserInformationSaveSuccessfully"), "",SageMessageType.Success);
                            }
                            
                        }
                        else
                        {
                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "EmailAddressAlreadyIsInUse"), "", SageMessageType.Alert);

                        }
                    }
                    else
                    {
                      ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "PleaseEnterTheRequiredFields"), "", SageMessageType.Alert);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private void SendActivateMail(UserInfo user)
        {
            var dbMessageTemplate = new MessageTemplateDataContext(SystemSetting.SageFrameConnectionString);
            MessageTokenDataContext messageTokenDB = new MessageTokenDataContext(SystemSetting.SageFrameConnectionString);
             
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
        }

        protected  bool EmailAddressExists(string email,bool AllowDuplicateEmail)
        {
            bool status = false;
            Guid UserID = new Guid(hdnEditUserID.Value);
            if (!AllowDuplicateEmail)
            {
                SageFrameUserCollection userColl = m.GetAllUsers();
                status = userColl.UserList.Exists(
                            delegate(UserInfo obj)
                            {
                                return (obj.Email == email && obj.UserID!=UserID);
                            }
                        );
            }
            return status;
        }

        protected void imgManageRoleSave_Click(object sender, EventArgs e)
        {
            try
            {
                string unselectedRoles = GetListBoxText(lstUnselectedRoles);
                string selectedRoles = GetListBoxText(lstSelectedRoles);
                if (hdnEditUsername.Value != "")
                {
                    string userRoles = role.GetRoleNames(hdnEditUsername.Value, GetPortalID);
                    string[] arrRoles = userRoles.Split(',');
                    UserInfo user = new UserInfo(Membership.ApplicationName, new Guid(hdnEditUserID.Value), userRoles, GetPortalID);
                    if (arrRoles.Length > 0 && selectedRoles.Length > 0)
                    {
                        role.ChangeUserInRoles(Membership.ApplicationName, new Guid(hdnEditUserID.Value), userRoles, selectedRoles, GetPortalID);
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("UserManagement", "UserRolesUpdatedSuccessfully"), "", SageMessageType.Success);
                       
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
               ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("UserManagement", "UnknownErrorOccur"), "", SageMessageType.Error);
            }
        }

        protected void btnManagePasswordSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewPassword.Text != "" && txtRetypeNewPassword.Text != "" && txtNewPassword.Text == txtRetypeNewPassword.Text && hdnEditUsername.Value != "")
                {
                     MembershipUser member = Membership.GetUser(hdnEditUsername.Value);
                     string Password, PasswordSalt;
                     PasswordHelper.EnforcePasswordSecurity(m.PasswordFormat, txtNewPassword.Text, out Password, out PasswordSalt);
                     UserInfo user = new UserInfo(new Guid(hdnEditUserID.Value), Password, PasswordSalt,m.PasswordFormat);
                     m.ChangePassword(user);
                     ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("UserManagement", "UserPasswordChangedSuccessfully"), "", SageMessageType.Success);
                }
                else
                {
                  ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "PleaseEnterTheRequiredField"), "", SageMessageType.Alert);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void btnAddAllRole_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in lstUnselectedRoles.Items)
            {
                if (li.Text.ToLower() != "super user")
                {
                    Session["_isJustAdded"] = true;
                }
                lstSelectedRoles.Items.Add(li);
            }
            lstUnselectedRoles.Items.Clear();
        }

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUnselectedRoles.SelectedIndex != -1)
                {
                    int[] selectedIndexs = lstUnselectedRoles.GetSelectedIndices();
                    for (int i = selectedIndexs.Length - 1; i >= 0; i--)
                    {
                        if (lstUnselectedRoles.Items[selectedIndexs[i]].Text.ToLower() != "super user")
                        {
                            Session["_isJustAdded"] = true;
                        }

                        lstSelectedRoles.Items.Add(lstUnselectedRoles.Items[selectedIndexs[i]]);
                        lstUnselectedRoles.Items.Remove(lstUnselectedRoles.Items[selectedIndexs[i]]);
                    }
                    lstUnselectedRoles.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void btnRemoveRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstSelectedRoles.SelectedIndex != -1)
                {
                    int[] selectedIndexs = lstSelectedRoles.GetSelectedIndices();
                    for (int i = selectedIndexs.Length - 1; i >= 0; i--)
                    {
                        if (lstSelectedRoles.Items.Count > 1)
                        {
                            bool justAdded = false;
                            if (Session["_isJustAdded"] != null)
                            {
                                justAdded = bool.Parse(Session["_isJustAdded"].ToString());
                            }
                            if (lstSelectedRoles.Items[selectedIndexs[i]].Text.ToLower() != "super user" || justAdded != false)
                            {
                                lstUnselectedRoles.Items.Add(lstSelectedRoles.Items[selectedIndexs[i]]);
                                lstSelectedRoles.Items.Remove(lstSelectedRoles.Items[selectedIndexs[i]]);
                            }
                        }
                    }
                    lstSelectedRoles.SelectedIndex = -1;
                }
               
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void btnRemoveAllRole_Click(object sender, EventArgs e)
        {
            try
            {
                int Count = lstSelectedRoles.Items.Count;
                List<string> remRoles = new List<string>();
                for (int i = 0; i < Count; i++)
                {
                    bool justAdded = false;
                    if (Session["_isJustAdded"] != null)
                    {
                        justAdded = bool.Parse(Session["_isJustAdded"].ToString());
                    }
                    if (lstSelectedRoles.Items[i].Text.ToLower() != "super user" || justAdded != false)
                    {

                        lstUnselectedRoles.Items.Add(lstSelectedRoles.Items[i]);
                        remRoles.Add(lstSelectedRoles.Items[i].Text);

                    }
                }
                foreach (string remRole in remRoles)
                {
                    lstSelectedRoles.Items.Remove(remRole);
                }
               
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgBack_Click(object sender, EventArgs e)
        {
            try
            {
                PanelVisibility(false, true, false);
                BindUsers(string.Empty);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void txtSearchText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BindUsers(txtSearchText.Text);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindUsers(txtSearchText.Text);
                this.rbFilterMode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnIsActive = (HiddenField)e.Row.FindControl("hdnIsActive");
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                LinkButton lnkUsername = (LinkButton)e.Row.FindControl("lnkUsername");
                imgDelete.Attributes.Add("onclick", "return confirm('" + GetSageMessage("UserManagement", "AreYouSureYouWantToDelete") + "')");
                
                HtmlInputCheckBox chkItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxItem");
                chkItem.Attributes.Add("onclick", "javascript:Check(this,'cssCheckBoxHeader','" + gdvUser.ClientID + "','cssCheckBoxItem');");
                HtmlInputCheckBox chkIsActiveItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveItem");
                chkIsActiveItem.Attributes.Add("onclick", "javascript:Check(this,'cssCheckBoxIsActiveHeader','" + gdvUser.ClientID + "','cssCheckBoxIsActiveItem');");
                chkIsActiveItem.Checked = bool.Parse(hdnIsActive.Value);
                if (SystemSetting.SYSTEM_USERS.Contains(lnkUsername.Text) || lnkUsername.Text == GetUsername)
                {
                    imgDelete.Visible = false;
                    chkIsActiveItem.Disabled = true;
                    chkItem.Disabled = true;
                    chkItem.Attributes.Add("class", "disabledClass");
                    chkIsActiveItem.Attributes.Add("class", "disabledClass");
                }
                else { chkIsActiveItem.Disabled = false; }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                HtmlInputCheckBox chkHeader = (HtmlInputCheckBox)e.Row.FindControl("chkBoxHeader");
                chkHeader.Attributes.Add("onclick", "javascript:SelectAllCheckboxesSpecific(this,'" + gdvUser.ClientID + "','cssCheckBoxItem');");
                HtmlInputCheckBox chkIsActiveHeader = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveHeader");
                chkIsActiveHeader.Attributes.Add("onclick", "javascript:SelectAllCheckboxesSpecific(this,'" + gdvUser.ClientID + "','cssCheckBoxIsActiveItem');");
            }
        }

        protected void imgBtnDeleteSelected_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string seletedUsername = string.Empty;
                for (int i = 0; i < gdvUser.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem = (HtmlInputCheckBox)gdvUser.Rows[i].FindControl("chkBoxItem");
                    if (chkBoxItem.Checked == true)
                    {
                        LinkButton lnkUsername = (LinkButton)gdvUser.Rows[i].FindControl("lnkUsername");
                        if (!SystemSetting.SYSTEM_USERS.Contains(lnkUsername.Text.Trim()))
                        {
                            seletedUsername = seletedUsername + lnkUsername.Text.Trim() + ",";
                        }
                       
                    }
                }
                if (seletedUsername.Length > 1)
                {
                    seletedUsername = seletedUsername.Substring(0, seletedUsername.Length - 1);
                    dbUser.sp_UsersDeleteSeleted(seletedUsername, GetPortalID,GetStoreID, GetUsername);
                    BindUsers(string.Empty);
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("UserManagement", "SelectedUsersAreDeletedSuccessfully"), "", SageMessageType.Success);
                }
               
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgBtnSaveChanges_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string seletedUsername = string.Empty;
                string IsActive = string.Empty;
                for (int i = 0; i < gdvUser.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem = (HtmlInputCheckBox)gdvUser.Rows[i].FindControl("chkBoxIsActiveItem");
                    LinkButton lnkUsername = (LinkButton)gdvUser.Rows[i].FindControl("lnkUsername");
                    seletedUsername = seletedUsername + lnkUsername.Text.Trim() + ",";
                    IsActive = IsActive + (chkBoxItem.Checked ? "1" : "0") + ",";
                }
                if (seletedUsername.Length > 1 && IsActive.Length > 0)
                {
                    seletedUsername = seletedUsername.Substring(0, seletedUsername.Length - 1);
                    IsActive = IsActive.Substring(0, IsActive.Length - 1);
                    dbUser.sp_UsersUpdateChanges(seletedUsername, IsActive, GetPortalID, GetUsername,GetStoreID);
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("UserManagement", "SelectedChangesAreSavedSuccessfully"), "", SageMessageType.Success);
                    BindUsers(string.Empty);
                    
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void ddlSearchRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindUsers(string.Empty);
                this.rbFilterMode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ProcessCancelRequest(Request.RawUrl);
        }

        protected void ddlRecordsPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            gdvUser.PageSize = int.Parse(ddlRecordsPerPage.SelectedValue.ToString());
            BindUsers(txtSearchText.Text);
        }

        protected void gdvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvUser.PageIndex = e.NewPageIndex;
            BindUsers(txtSearchText.Text);
        }
        protected void imbCreateUser_Click(object sender, ImageClickEventArgs e)
        {
             try
             {

                 if (txtUserName.Text != "" && txtSecurityQuestion.Text != "" && txtSecurityAnswer.Text != "" && txtFirstName.Text != "" && txtLastName.Text != "" && txtEmail.Text != "")
                 {
                     if (lstAvailableRoles.SelectedIndex > -1)
                     {
                         UserInfo objUser = new UserInfo();
                         objUser.ApplicationName = Membership.ApplicationName;
                         objUser.FirstName = txtFirstName.Text;
                         objUser.UserName = txtUserName.Text;
                         objUser.LastName = txtLastName.Text;
                         string Password, PasswordSalt;
                         PasswordHelper.EnforcePasswordSecurity(m.PasswordFormat, txtPassword.Text, out Password, out PasswordSalt);
                         objUser.Password = Password;
                         objUser.PasswordSalt = PasswordSalt;
                         objUser.Email = txtEmail.Text;
                         objUser.SecurityQuestion = txtSecurityQuestion.Text;
                         objUser.SecurityAnswer = txtSecurityAnswer.Text;
                         objUser.IsApproved = true;
                         objUser.CurrentTimeUtc = DateTime.Now;
                         objUser.CreatedDate = DateTime.Now;
                         objUser.UniqueEmail = 0;
                         objUser.PasswordFormat = m.PasswordFormat;
                         objUser.PortalID = GetPortalID;
                         objUser.AddedOn = DateTime.Now;
                         objUser.AddedBy = GetUsername;
                         objUser.UserID = Guid.NewGuid();
                         objUser.RoleNames = GetSelectedRoleNameString();
                         objUser.StoreID = GetStoreID;
                         objUser.CustomerID = 0;

                         UserCreationStatus status = new UserCreationStatus();
                         try
                         {
                             MembershipDataProvider.CreatePortalUser(objUser, out status, UserCreationMode.CREATE);

                             if (status == UserCreationStatus.DUPLICATE_USER)
                             {
                                 ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "NameAlreadyExists"), "", SageMessageType.Alert);
                             }
                             else if (status == UserCreationStatus.DUPLICATE_EMAIL)
                             {
                                 ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "EmailAddressAlreadyIsInUse"), "", SageMessageType.Alert);

                             }
                             else if (status == UserCreationStatus.SUCCESS)
                             {
                                 PanelVisibility(false, true, false);
                                 BindUsers(string.Empty);
                                 ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "UserCreatedSuccessfully"), "", SageMessageType.Success);
                             }
                         }
                         catch (Exception)
                         {

                             throw;
                         }
                     }
                     else
                     {
                         ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "PleaseSelectRole"), "", SageMessageType.Alert);
                     }
                 }
                 else
                 {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "PleaseEnterAllRequiredFields"), "", SageMessageType.Alert);
                 }             
                
                 
             }
             catch (Exception ex)
             {
                 ProcessException(ex);
             }
        }

        private string GetSelectedRoleNameString()
        {
            List<string> roleList = new List<string>();
            foreach (ListItem li in lstAvailableRoles.Items)
            {
                if (li.Selected == true)
                {
                    roleList.Add(li.Text);
                }
            }

            return (String.Join(",", roleList.ToArray()));
        }

        protected void imgBtnSettings_Click(object sender, ImageClickEventArgs e)
        {
            PanelVisibility(false, false, false);
            pnlSettings.Visible = true;
            LoadSettings();
        }
        private void LoadSettings()
        {
            foreach (SettingInfo obj in MembershipDataProvider.GetSettings())
            {
                switch (obj.SettingKey)
                {
                    case "DUPLICATE_USERS_ACROSS_PORTALS":
                        chkEnableDupNames.Checked = obj.SettingValue.Equals("1")? true : false;
                        break;
                    case "DUPLICATE_ROLES_ACROSS_PORTALS":
                        chkEnableDupRole.Checked = obj.SettingValue.Equals("1") ? true : false;
                        break;
                    case  "SELECTED_PASSWORD_FORMAT":
                        SetPasswordFormat(int.Parse(obj.SettingValue));
                        break;
                    case "DUPLICATE_EMAIL_ALLOWED":
                        chkEnableDupEmail.Checked = obj.SettingValue.Equals("1") ? true : false;
                        break;
                    case "ENABLE_CAPTCHA":
                        chkEnableCaptcha.Checked = obj.SettingValue.Equals("1") ? true : false;
                        break;
                }
            }
        }

        protected void btnSaveSetting_Click(object sender, EventArgs e)
        {
            List<SettingInfo> lstSettings = new List<SettingInfo>();
            SettingInfo dupUsers = new SettingInfo();
            dupUsers.SettingKey = SettingsEnum.DUPLICATE_USERS_ACROSS_PORTALS.ToString();
            dupUsers.SettingValue = chkEnableDupNames.Checked ? "1" : "0";
            SettingInfo dupRoles = new SettingInfo();
            dupRoles.SettingKey = SettingsEnum.DUPLICATE_ROLES_ACROSS_PORTALS.ToString();
            dupRoles.SettingValue = chkEnableDupRole.Checked ? "1" : "0";
            SettingInfo passwordFormat = new SettingInfo();
            passwordFormat.SettingKey = SettingsEnum.SELECTED_PASSWORD_FORMAT.ToString();
            passwordFormat.SettingValue = GetPasswordFormat().ToString();
            SettingInfo dupEmail = new SettingInfo();
            dupEmail.SettingKey = SettingsEnum.DUPLICATE_EMAIL_ALLOWED.ToString();
            dupEmail.SettingValue = chkEnableDupEmail.Checked ? "1" : "0";
            SettingInfo enableCaptcha = new SettingInfo();
            enableCaptcha.SettingKey = SettingsEnum.ENABLE_CAPTCHA.ToString();
            enableCaptcha.SettingValue = chkEnableCaptcha.Checked ? "1" : "0";
            lstSettings.Add(dupUsers);
            lstSettings.Add(dupRoles);
            lstSettings.Add(passwordFormat);
            lstSettings.Add(dupEmail);
            lstSettings.Add(enableCaptcha);

            try
            {
                MembershipDataProvider.SaveSettings(lstSettings);
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("UserManagement", "SettingSavedSuccessfully"), "", SageMessageType.Success);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int GetPasswordFormat()
        {
            int pwdFormat = (int)SettingsEnum.DEFAULT_PASSWORD_FORMAT;

            pwdFormat = int.Parse(rdbLst.SelectedValue.ToString());
            if (pwdFormat == 3)
            {
                pwdFormat = 3;
            }
            return pwdFormat;            
        }

        private void SetPasswordFormat(int PasswordFormat)
        {
            if (PasswordFormat<3)
            {
                rdbLst.SelectedIndex = PasswordFormat - 1;
            }
            else
            {
                rdbLst.SelectedIndex = 2; 
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlSettings.Visible = false;
            PanelVisibility(false, true, false);            
        }
        
        protected void rbFilterMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterUserGrid(int.Parse(rbFilterMode.SelectedValue.ToString()));
        }

        protected void FilterUserGrid(int FilterMode)
        {
            List<UserInfo> lstUsers = (List<UserInfo>)ViewState["UserList"];
            List<UserInfo> lstNew = new List<UserInfo>();
            switch (FilterMode)
            {
                case 0:
                    gdvUser.DataSource = ReorderUserList(lstUsers);
                    gdvUser.DataBind();
                    break;
                case 1:
                   
                    foreach (UserInfo user in lstUsers)
                    {
                        if (user.IsActive)
                        {
                            lstNew.Add(user);
                        }
                    }
                    gdvUser.DataSource = ReorderUserList(lstNew);
                    gdvUser.DataBind();
                  
                    break;
                case 2:
                   
                    foreach (UserInfo user in lstUsers)
                    {
                        if (!user.IsActive)
                        {
                            lstNew.Add(user);
                        }
                    }
                    gdvUser.DataSource = ReorderUserList(lstNew);
                    gdvUser.DataBind();
                  
                    break;
            }
        }
}
}