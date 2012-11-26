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
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using SageFrame.Core.Pages;
using SageFrame.SageFrameClass;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using RegisterModule;
using SageFrame.RolesManagement;
using SageFrame.PagePermission;
using SageFrame.Permission;
using SageFrame.PortalSetting;
using System.Web.UI.HtmlControls;
using SageFrame.UserModules;
using SageFrame.UserManagement;
using SageFrame.Framework;
using SageFrame.Security.Entities;
using SageFrame.Security;


namespace SageFrame.Modules.Admin.Pages
{
    public partial class ctl_ManagePages : BaseAdministrationUserControl
    {
        PagesDataContext dbPages = new PagesDataContext(SystemSetting.SageFrameConnectionString);
        PagePermissionDataContext dbPagePermission = new PagePermissionDataContext(SystemSetting.SageFrameConnectionString);
        RolesManagementDataContext dbRoles = new RolesManagementDataContext(SystemSetting.SageFrameConnectionString);
        PermissionDataContext dbPermission = new PermissionDataContext(SystemSetting.SageFrameConnectionString);
        PortalSettingDataContext dbPortal = new PortalSettingDataContext(SystemSetting.SageFrameConnectionString);
        ModulesDataContext dbModules = new ModulesDataContext(SystemSetting.SageFrameConnectionString);
        UserModulesDataContext dbUserModules = new UserModulesDataContext(SystemSetting.SageFrameConnectionString);
        UserManagementDataContext dbUser = new UserManagementDataContext(SystemSetting.SageFrameConnectionString);
        CommonFunction LToDCon = new CommonFunction();
        System.Nullable<Int32> newPageID = 0;
        System.Nullable<Int32> _newPageCount = 0;
        DateTime _startDate = CommonHelper.AvailableStartDateTime;
        DateTime _endDate = CommonHelper.AvailableEndDateTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {                    
                    HttpContext.Current.Session["ReturnUrl"] = null;
                    PageModulePanelVisibility(true, false, false);
                    PopupPanelVisibility(false, true);
                    BindPages();
                    if (HttpContext.Current.Request.Params["hdnPageID"] != null)
                    {
                        BindParentPages(Int32.Parse(HttpContext.Current.Request.Params["hdnPageID"].ToString()));
                    }
                    BindPageControls();
                    AddImageUrls();
                    ClearRoles();
                    ClearUsername();
                    ClearUsernameModule();
                    ClearPageControls();
                    BindControlsInViewPermissionPopUp();
                    BindControlsInEditPermissionPopUp();
                    setControlsAttributes();
                    BindUserModuleGrid();
                    ModulesBindData();
                    SetDefaultModule();
                    InitializeControls();
                    BindPostionPage();
                    lblURLTitle.Text = GetSageMessage("PAGES", "Location");
                    ibSave.Attributes.Add("onclick",
                                          "javascript:return confirm('" +
                                          GetSageMessage("PAGES", "AreYouSureToSaveChanges") + "')");
                    imbPageSave.Attributes.Add("onclick",
                                               "javascript:return confirm('" +
                                               GetSageMessage("PAGES", "AreYouSureToSaveChanges") + "')");
                    if (HttpContext.Current.Request.Params["hdnPageID"] != null &&
                        HttpContext.Current.Request.Params["hdnActiveIndex"] != null &&
                        HttpContext.Current.Request.UrlReferrer.ToString().Contains("/Sagin/"))
                    {
                        TabContainerManagePages.ActiveTabIndex =
                            Int32.Parse(HttpContext.Current.Request.Params["hdnActiveIndex"].ToString());
                        hdnPageID.Value = HttpContext.Current.Request.Params["hdnPageID"].ToString();
                        BindData(Int32.Parse(HttpContext.Current.Request.Params["hdnPageID"].ToString()), true);
                        lblCreatedBy.Visible = true;
                        lblUpdatedBy.Visible = true;
                        PopupPanelVisibility(true, false);
                    }
                    BindControlInUserModulePermission();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }


        }


        public void ModulesPanelVisibility(bool ModulesPosition, bool ModuleControls)
        {
            pnlModules.Visible = ModulesPosition;
            pnlModuleControls.Visible = ModuleControls;
        }

        /// <summary>
        /// Set the all attributes of controls at runtime
        /// </summary>
        private void setControlsAttributes()
        {
            lblPane.Text = "Pane: ";
            lblModule.Text = "Module: ";
            lblModuleTitle.Text = "Module Title: ";
            lblPosition.Text = "Insert: ";
            lblPermission.Text = "Visibility: ";
            lblInstance.Text = "User Module: ";
            ibModuleControlSave.AlternateText = "Add selected module to page";
            lblShowInAllPages.Text = "Show in all pages: ";
        }

        private void AddImageUrls()
        {
            ibSave.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            ibCancel.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            ibAdd.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            imgbtnCalender1.ImageUrl = GetTemplateImageUrl("imgcalendar.png", true);
            imgbtnCalender2.ImageUrl = GetTemplateImageUrl("imgcalendar.png", true);
            imbAddUser.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            imbAddUserEditPermission.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            imbPageSave.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            ibModuleControlSave.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            imbAddModules.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            ibModuleControlCancel.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            imbSelectUsers.ImageUrl = GetTemplateImageUrl("imgsearch.png", true);
            imgSearch.ImageUrl = GetTemplateImageUrl("imgsearch.png", true);
            imgAddSelectedUsers.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            imbSelectEditUsers.ImageUrl = GetTemplateImageUrl("imgsearch.png", true);
            imgUserSearch.ImageUrl = GetTemplateImageUrl("imgsearch.png", true);
            imgAddSelectedEditUsers.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            ibSavePermission.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            ibCancelPermission.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            imbAddUserViewPermissionModule.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            imbAddUserEditPermissionModule.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            imbSelectUsersModule.ImageUrl = GetTemplateImageUrl("imgsearch.png", true);
            imbSelectEditUsersModule.ImageUrl = GetTemplateImageUrl("imgsearch.png", true);
            imgSearchUsersViewModule.ImageUrl = GetTemplateImageUrl("imgsearch.png", true);
            imgAddSelectedUsersModule.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            imgUserSearchModule.ImageUrl = GetTemplateImageUrl("imgsearch.png", true);
            imgAddSelectedEditUsersModule1.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
        }

        private void ClearPageControls()
        {
            ClearChildViewState();
            DataTable dtUserModules = new DataTable();
            dtUserModules.Columns.Add("SNo");
            dtUserModules.Columns.Add("PageID");
            dtUserModules.Columns.Add("UserModuleID");
            dtUserModules.Columns.Add("ModuleID");
            dtUserModules.Columns.Add("Title");
            dtUserModules.Columns.Add("Visibility");
            dtUserModules.Columns.Add("InheritView");
            dtUserModules.Columns.Add("PaneName");
            dtUserModules.Columns.Add("AllPages");
            dtUserModules.Columns.Add("IsActive");
            dtUserModules.Columns.Add("ModuleOrder");
            dtUserModules.Columns.Add("MaxModuleOrder");
            dtUserModules.Columns.Add("MinModuleOrder");
            dtUserModules.Columns.Add("Position");
            dtUserModules.Columns.Add("InstanceID");
            dtUserModules.AcceptChanges();
            if (ViewState["dtUserModules"] != null)
            {
                ViewState["dtUserModules"] = null;
            }
            ViewState["dtUserModules"] = dtUserModules;
            gdvUserModules.DataSource = dtUserModules;
            gdvUserModules.DataBind();
        }
        private void ClearForm()
        {
            txtPageName.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtKeyWords.Text = "";
            chkMenu.Checked = true;
            txtRefreshInterval.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtUrl.Text = "";
            chkSecure.Checked = false;
            chkShowInFooter.Checked = false;
            cboPositionTab.SelectedIndex = 0;
            rbURlType.SelectedIndex = 0;
            TabContainerManagePages.ActiveTabIndex = 0;
        }
        private void ClearRoles()
        {
            BindRolesInListBox(lstUnselectedViewRoles);
            BindRolesInListBox(lstUnselectedEditRoles);
            BindSelectedRolesInListBox(lstSelectedRolesView);
            BindSelectedRolesInListBox(lstSelectedRolesEdit);
        }
        private void ClearRolesModule()
        {
            BindRolesInListBox(lstUnselectedViewRolesModule);
            BindRolesInListBox(lstUnselectedEditRolesModule);
            BindSelectedRolesInListBox(lstSelectedRolesViewModule);
            BindSelectedRolesInListBox(lstSelectedRolesEditModule);
        }
        private void ClearUsername()
        {
            DataTable dtViewUsers = new DataTable();
            DataTable dtEditUsers = new DataTable();
            dtViewUsers.Columns.Add("Username");
            dtEditUsers.Columns.Add("Username");
            dtViewUsers.AcceptChanges();
            dtEditUsers.AcceptChanges();
            ViewState["dtViewUsers"] = dtViewUsers;
            ViewState["dtEditUsers"] = dtEditUsers;
            gdvViewUsernames.DataSource = dtViewUsers;
            gdvEditUsernames.DataSource = dtEditUsers;
            gdvEditUsernames.DataBind();
            gdvViewUsernames.DataBind();
        }
        private void ClearUsernameModule()
        {
            DataTable dtViewUsersModule = new DataTable();
            DataTable dtEditUsersModule = new DataTable();
            dtViewUsersModule.Columns.Add("Username");
            dtEditUsersModule.Columns.Add("Username");
            dtViewUsersModule.AcceptChanges();
            dtEditUsersModule.AcceptChanges();
            ViewState["dtViewUsersModules"] = dtViewUsersModule;
            ViewState["dtEditUsersModules"] = dtEditUsersModule;
        }
        private void BindPostionPage()
        {
            if (cboParentPage.SelectedIndex > 0)
            {
                var LINQMenuPages = dbPages.sp_PageGetByParentID(Int32.Parse(cboParentPage.SelectedValue), true, null, null, GetUsername, GetPortalID);
                cboPositionTab.DataSource = LINQMenuPages;
                cboPositionTab.DataTextField = "PageName";
                cboPositionTab.DataValueField = "PageID";
                cboPositionTab.DataBind();
            }
            else
            {
                var LINQMenuPages = dbPages.sp_PageGetByParentID(0, true, true, null, GetUsername, GetPortalID);
                cboPositionTab.DataSource = LINQMenuPages;
                cboPositionTab.DataTextField = "PageName";
                cboPositionTab.DataValueField = "PageID";
                cboPositionTab.DataBind();
            }
            cboPositionTab.Items.Insert(0, new ListItem("<Not Specified>", "-1"));
        }

        private void BindPageControls()
        {
            try
            {
                rbInsertPosition.DataSource = SageFrameLists.menuPagePosition();
                rbInsertPosition.DataTextField = "Value";
                rbInsertPosition.DataValueField = "Key";
                rbInsertPosition.DataBind();

                rbURlType.DataSource = SageFrameLists.urlTypeName();
                rbURlType.DataTextField = "Value";
                rbURlType.DataValueField = "Key";
                rbURlType.DataBind();
                rbURlType.SelectedIndex = rbURlType.Items.IndexOf(rbInsertPosition.Items.FindByValue("None"));
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

        }

        private void BindParentPages(int PageID)
        {
            try
            {
                cboParentPage.ClearSelection();
                var LINQParentPages = dbPages.usp_PagesGetPossibleParents("---", true, false, GetPortalID, GetUsername, null, null, PageID);
                cboParentPage.DataSource = LINQParentPages;
                cboParentPage.DataBind();
                cboParentPage.Items.Insert(0, new ListItem("<Not Specified>", "-1"));
            }
            catch (Exception ex)
            {

                ProcessException(ex);
            }

        }

        private void BindPages()
        {
            var LINQParentPages = dbPages.sp_PageGetByCustomPrefix("---", null, false, GetPortalID, GetUsername, null, null);
            gdvPageList.DataSource = LINQParentPages;
            gdvPageList.DataBind();
        }

        private void BindData(int pageID, bool isActive)
        {
            hdnIsActiveDB.Value = isActive.ToString();
            sp_PagesGetByPageIDResult LINQEditPage = dbPages.sp_PagesGetByPageID(pageID).SingleOrDefault();// .SingleOrDefault(); 
            if ((LINQEditPage != null))
            {
                txtPageName.Text = LINQEditPage.PageName;
                txtTitle.Text = LINQEditPage.Title;
                txtDescription.Text = LINQEditPage.Description;
                txtKeyWords.Text = LINQEditPage.KeyWords;
                if (LINQEditPage.IconFile != null)
                {
                    imgIcon.Visible = true;
                    imgIcon.ImageUrl = LINQEditPage.IconFile;
                }
                else
                {
                    imgIcon.ImageUrl = "";
                    imgIcon.Visible = false;
                }
                cboParentPage.ClearSelection();
                if (LINQEditPage.ParentID != 0)
                {
                    cboParentPage.SelectedIndex = cboParentPage.Items.IndexOf(cboParentPage.Items.FindByValue(LINQEditPage.ParentID.ToString()));
                }
                SageFrameConfig sageFrameConfig = new SageFrameConfig();
                string defaultPage = string.Empty;
                defaultPage = sageFrameConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage);
                if (LINQEditPage.PageName.ToLower() == defaultPage.ToLower())
                {
                    chkMenu.Enabled = false;
                }
                chkMenu.Checked = (bool)LINQEditPage.IsVisible;
                chkSecure.Checked = (bool)LINQEditPage.IsSecure;
                chkShowInFooter.Checked = (bool)LINQEditPage.IsShowInFooter;

                if (LINQEditPage.StartDate != null)
                {
                    txtStartDate.Text = CommonHelper.ShortTimeReturn(LINQEditPage.StartDate);
                }
                if (LINQEditPage.EndDate != null)
                {
                    txtEndDate.Text = CommonHelper.ShortTimeReturn(LINQEditPage.EndDate);
                }
                if (LINQEditPage.RefreshInterval != null)
                {
                    txtRefreshInterval.Text = LINQEditPage.RefreshInterval.ToString();
                }
                if (LINQEditPage.Url == null || LINQEditPage.Url == string.Empty)
                {
                    rbURlType.SelectedIndex = 0;
                }
                else
                {
                    rbURlType.SelectedIndex = 1;
                    txtUrl.Text = LINQEditPage.Url;
                }

                //Bind the roles and user datatable
                var LINQPermissionPageView = dbPermission.sp_GetPermissionByCodeAndKey("SYSTEM_VIEW", "VIEW").SingleOrDefault();
                int PermissionViewPage = LINQPermissionPageView.PermissionID;
                var LINQPermissionPageEdit = dbPermission.sp_GetPermissionByCodeAndKey("SYSTEM_EDIT", "EDIT").SingleOrDefault();
                int PermissionEditPage = LINQPermissionPageEdit.PermissionID;


                //To bind the Page Permissions
                lstSelectedRolesView.Items.Clear();
                lstUnselectedViewRoles.Items.Clear();
                lstSelectedRolesEdit.Items.Clear();
                lstUnselectedEditRoles.Items.Clear();
                List<sp_GetPagePermissionsByPageIDAndPermissionIDResult> LINQRolesAndUserNameView = dbPagePermission.sp_GetPagePermissionsByPageIDAndPermissionID(pageID, PermissionViewPage, GetPortalID).ToList();
                List<sp_GetPagePermissionsByPageIDAndPermissionIDResult> LINQRolesAndUserNameEdit = dbPagePermission.sp_GetPagePermissionsByPageIDAndPermissionID(pageID, PermissionEditPage, GetPortalID).ToList();

                var roles = dbRoles.sp_PortalRoleList(GetPortalID, true, GetUsername);
                foreach (var role in roles)
                {
                    string roleName = role.RoleName;
                    string roleID = role.RoleID.ToString();
                    if (CheckPagePermissionHasRole(role.RoleName, LINQRolesAndUserNameView))
                    {
                        if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                        {
                            lstSelectedRolesView.Items.Add(new ListItem(roleName, roleID));
                        }
                        else
                        {
                            string rolePrefix = GetPortalSEOName + "_";
                            roleName = roleName.Replace(rolePrefix, "");
                            lstSelectedRolesView.Items.Add(new ListItem(roleName, roleID));
                        }
                    }
                    else
                    {
                        if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                        {
                            lstUnselectedViewRoles.Items.Add(new ListItem(roleName, roleID));
                        }
                        else
                        {
                            string rolePrefix = GetPortalSEOName + "_";
                            roleName = roleName.Replace(rolePrefix, "");
                            lstUnselectedViewRoles.Items.Add(new ListItem(roleName, roleID));
                        }
                    }
                    if (CheckPagePermissionHasRole(role.RoleName, LINQRolesAndUserNameEdit))
                    {
                        if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                        {
                            lstSelectedRolesEdit.Items.Add(new ListItem(roleName, roleID));
                        }
                        else
                        {
                            string rolePrefix = GetPortalSEOName + "_";
                            roleName = roleName.Replace(rolePrefix, "");
                            lstSelectedRolesEdit.Items.Add(new ListItem(roleName, roleID));
                        }
                    }
                    else
                    {
                        if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                        {
                            lstUnselectedEditRoles.Items.Add(new ListItem(roleName, roleID));
                        }
                        else
                        {
                            string rolePrefix = GetPortalSEOName + "_";
                            roleName = roleName.Replace(rolePrefix, "");
                            lstUnselectedEditRoles.Items.Add(new ListItem(roleName, roleID));
                        }
                    }
                }

                List<sp_GetPagePermissionsByPageIDAndPermissionIDNoRoleNameResult> LINQUserNameView = dbPagePermission.sp_GetPagePermissionsByPageIDAndPermissionIDNoRoleName(pageID, PermissionViewPage, GetPortalID).ToList();
                List<sp_GetPagePermissionsByPageIDAndPermissionIDNoRoleNameResult> LINQUserNameEdit = dbPagePermission.sp_GetPagePermissionsByPageIDAndPermissionIDNoRoleName(pageID, PermissionEditPage, GetPortalID).ToList();

                //To bind the user name for VIEW permission
                BindDataTableUserName(LINQUserNameView, "view");

                //To bind the user name for EDIT permission
                BindDataTableUserName(LINQUserNameEdit, "edit");



                //:TODO: bind the iconbar user control with given pageID                
                List<sp_GetUserModulesByPageIDResult> AllUserModules = dbPages.sp_GetUserModulesByPageID(pageID, GetPortalID).ToList();
                //To bind the user name for VIEW permission
                BindDataTableUserModules(AllUserModules);
                lblCreatedBy.Text = GetSageMessage("PAGES", "CreatedBy") + LINQEditPage.AddedBy.ToString() + " " + LINQEditPage.AddedOn.ToString();
                if (LINQEditPage.UpdatedBy != null && LINQEditPage.UpdatedOn != null)
                {
                    lblUpdatedBy.Text = GetSageMessage("PAGES", "LastUpdatedBy") + LINQEditPage.UpdatedBy.ToString() + " " + LINQEditPage.UpdatedOn.ToString();
                }
            }
        }

        public void BindDataTableUserModules(List<sp_GetUserModulesByPageIDResult> AllUserModules)
        {
            DataTable dtUserModules = new DataTable();
            dtUserModules.Columns.Add("SNo");
            dtUserModules.Columns.Add("PageID");
            dtUserModules.Columns.Add("UserModuleID");
            dtUserModules.Columns.Add("ModuleID");
            dtUserModules.Columns.Add("Title");
            dtUserModules.Columns.Add("Visibility");
            dtUserModules.Columns.Add("InheritView");
            dtUserModules.Columns.Add("PaneName");
            dtUserModules.Columns.Add("AllPages");
            dtUserModules.Columns.Add("IsActive");
            dtUserModules.Columns.Add("ModuleOrder");
            dtUserModules.Columns.Add("MaxModuleOrder");
            dtUserModules.Columns.Add("MinModuleOrder");
            dtUserModules.Columns.Add("Position");
            dtUserModules.Columns.Add("InstanceID");
            dtUserModules.AcceptChanges();

            for (int index = 0; index <= AllUserModules.Count - 1; index++)
            {
                sp_GetUserModulesByPageIDResult UserModules = AllUserModules[index];
                DataRow dr = dtUserModules.NewRow();
                dr["SNo"] = 0;
                dr["PageID"] = UserModules.PageID;
                dr["UserModuleID"] = UserModules.UserModuleID;
                dr["ModuleID"] = UserModules.ModuleID;
                dr["Title"] = UserModules.Title;
                dr["Visibility"] = UserModules.Visibility;
                dr["InheritView"] = UserModules.InheritView;
                dr["PaneName"] = UserModules.PaneName;
                dr["AllPages"] = UserModules.AllPages;
                dr["IsActive"] = UserModules.IsActive;
                dr["ModuleOrder"] = UserModules.ModuleOrder;
                dr["MaxModuleOrder"] = UserModules.MaxModuleOrder;
                dr["MinModuleOrder"] = UserModules.MinModuleOrder;
                dr["Position"] = "0";
                dr["InstanceID"] = "0";
                dtUserModules.Rows.Add(dr);
            }
            ViewState["dtUserModules"] = dtUserModules;
            BindUserModuleGrid();
        }

        private void BindDataTableUserName(List<sp_GetPagePermissionsByPageIDAndPermissionIDNoRoleNameResult> PagePermissionList, string PermissionType)
        {
            DataTable dtUserName = new DataTable();
            dtUserName.Columns.Add("Username");
            dtUserName.AcceptChanges();
            for (int index = 0; index <= PagePermissionList.Count - 1; index++)
            {
                sp_GetPagePermissionsByPageIDAndPermissionIDNoRoleNameResult PagePermission = PagePermissionList[index];
                if (PagePermission.Username != null)
                {
                    dtUserName = BindDataTable(PagePermission.Username, dtUserName);
                }
            }
            if (PermissionType.ToLower() == "view")
            {
                ViewState["dtViewUsers"] = dtUserName;
            }
            else if (PermissionType.ToLower() == "edit")
            {
                ViewState["dtEditUsers"] = dtUserName;
            }
            BindUsernameGrid(PermissionType);
        }

        private void BindUsernameGrid(string PermissionType)
        {
            if (PermissionType.ToLower() == "view")
            {
                if (ViewState["dtViewUsers"] != null)
                {
                    gdvViewUsernames.DataSource = (DataTable)ViewState["dtViewUsers"];
                    gdvViewUsernames.DataBind();
                }

            }
            else if (PermissionType.ToLower() == "edit")
            {
                if (ViewState["dtEditUsers"] != null)
                {
                    gdvEditUsernames.DataSource = (DataTable)ViewState["dtEditUsers"];
                    gdvEditUsernames.DataBind();
                }
            }
        }

        private DataTable BindDataTable(string UserName, DataTable dtUserName)
        {
            DataRow dr = dtUserName.NewRow();
            dr["Username"] = UserName;
            dtUserName.Rows.Add(dr);
            return dtUserName;
        }

        private bool CheckPagePermissionHasRole(string roleName, List<sp_GetPagePermissionsByPageIDAndPermissionIDResult> PagePermissionList)
        {
            bool bolPageHasRole = false;
            for (int index = 0; index <= PagePermissionList.Count - 1; index++)
            {
                sp_GetPagePermissionsByPageIDAndPermissionIDResult PagePermission = PagePermissionList[index];
                if (PagePermission.RoleName.ToLower() == roleName.ToLower())
                {
                    bolPageHasRole = true;
                    break;
                }
            }
            return bolPageHasRole;
        }

        protected void ibSave_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                if (Page.IsValid)
                {
                    if (gdvUserModules.EditIndex > -1)
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(),
                                    GetSageMessage("PAGES", "UserModuleTableEditMode"), "", SageMessageType.Error);
                    }
                    else
                    {
                        SavePageData();
                        gdvUserModules.EditIndex = -1;
                        gdvUserModules.DataSource = (DataTable)ViewState["dtUserModules"];
                        gdvUserModules.DataBind();
                        BindPages();
                        PopupPanelVisibility(false, true);
                    }
                }
            }
            catch (Exception exc)
            {
                ProcessException(exc);
            }

        }


      

        protected void cboParentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPostionPage();
        }

        private void SavePageData()
        {
            if (cboParentPage.SelectedItem != null && hdnPageID.Value != cboParentPage.SelectedValue)
            {
                string strIcon = "";
                strIcon = "";
                string error = "";
                int activeTabIndex = 0;
                PageInfo objPage = new PageInfo();
                objPage.PageName = txtPageName.Text.Trim();
                objPage.Title = txtTitle.Text.Trim();
                objPage.Description = txtDescription.Text.Trim();
                objPage.KeyWords = txtKeyWords.Text.Trim();
                objPage.IsVisible = chkMenu.Checked;
                objPage.DisableLink = false;
                if (cboParentPage.SelectedItem != null)
                {
                    int parentPageID = Int32.Parse(cboParentPage.SelectedItem.Value);
                    if (parentPageID != -1)
                    {
                        objPage.ParentId = parentPageID;
                    }
                    else
                    {
                        objPage.ParentId = 0;
                    }
                }
                objPage.IconFile = strIcon;
                objPage.IsDeleted = false;
                objPage.IsActive = bool.Parse(hdnIsActiveDB.Value);
                objPage.Url = "";
                if (!string.IsNullOrEmpty(txtStartDate.Text))
                {
                    objPage.StartDate = Convert.ToDateTime(txtStartDate.Text);
                }
                else
                {
                    objPage.StartDate = CommonHelper.AvailableStartDateTime;
                }
                if (!string.IsNullOrEmpty(txtEndDate.Text))
                {
                    objPage.EndDate = Convert.ToDateTime(txtEndDate.Text);
                }
                else
                {
                    objPage.EndDate = CommonHelper.AvailableEndDateTime;//Convert.ToDateTime(DBNull.Value.ToString()); 
                }
                if (txtRefreshInterval.Text.Length > 0)
                {
                    objPage.RefreshInterval = decimal.Parse(txtRefreshInterval.Text.ToString());
                }
                objPage.IsSecure = chkSecure.Checked;
                objPage.IsShownInFooter = chkShowInFooter.Checked;
                objPage.IsRequiredPage = false;
                string strLargeImagePath = string.Empty;
                string strMediumImagePath = string.Empty;
                string strSmallImagePath = string.Empty;
                string strImageExtension = string.Empty;
                string strLocalImageName = string.Empty;
                string fileName = string.Empty;

                #region Image Save
                if (fileIcon.HasFile)
                {
                    //For valid Image Extension check
                    if (PictureManager.IsVAlidImageContentType(fileIcon.FileName))
                    {
                        // Get the size in bytes of the file to upload.
                        int fileSize = fileIcon.PostedFile.ContentLength;

                        // Allow only files less than 10,48,576 bytes (approximately 1 MB) to be uploaded.
                        if (fileSize < 1048576)
                        {
                            string path = HttpContext.Current.Server.MapPath("~/");
                            string largeImagePath = SageFrame.Core.RegisterModule.Common.LargeImagePath;
                            string savedPathLarge = Path.Combine(path, largeImagePath);
                            string mediumImagePath = SageFrame.Core.RegisterModule.Common.MediumImagePath;
                            string savedPathMedium = Path.Combine(path, mediumImagePath);
                            string smallImagePath = SageFrame.Core.RegisterModule.Common.SmallImagePath;
                            string savedPathSmall = Path.Combine(path, smallImagePath);
                            try
                            {
                                string strExtension = fileIcon.PostedFile.FileName.Substring(fileIcon.PostedFile.FileName.LastIndexOf("."));
                                if (fileIcon.FileName.Contains("."))
                                {
                                    char[] separator = new char[] { '.' };
                                    string[] fileNames = fileIcon.FileName.Split(separator);
                                    fileName = fileNames[0];
                                    strLocalImageName = PictureManager.GetFileName(fileName);
                                }

                                strLargeImagePath = PictureManager.SaveImage(fileIcon, strLocalImageName + strExtension, savedPathLarge);
                                strMediumImagePath = PictureManager.CreateMediumThumnail(strLargeImagePath, GetPortalID, strLocalImageName + strExtension, savedPathMedium, 200);
                                strSmallImagePath = PictureManager.CreateSmallThumnail(strLargeImagePath, GetPortalID, strLocalImageName + strExtension, savedPathSmall, 150);

                                if (!string.IsNullOrEmpty(strSmallImagePath))
                                {
                                    string SavedToPAth = "~/" + smallImagePath + "/" + strLocalImageName + strExtension;
                                    string IconPath = SavedToPAth.Replace('\\', '/');
                                    objPage.IconFile = IconPath;
                                }

                                imgIcon.Visible = true;
                                imgIcon.ImageUrl = strSmallImagePath;
                            }

                            catch (Exception ex)
                            {
                                ProcessException(ex);
                            }
                        }
                        else
                        {
                            // Notify the user why their file was not uploaded.
                            activeTabIndex = 2;
                            ltErrorIcon.Text = GetSageMessage("PAGES", "YourFile ") + fileIcon.FileName + " " + GetSageMessage("PAGES", " CannotBeUploadedAsItExceedsTheSizelimit");
                            ltErrorIcon.Visible = true;
                            fileIcon.FileContent.Dispose();
                            objPage.IconFile = null;
                            error = ltErrorIcon.Text;
                        }
                    }
                    else
                    {
                        activeTabIndex = 2;
                        ltErrorIcon.Text = GetSageMessage("PAGES", "File ") + fileIcon.FileName + " " + GetSageMessage("PAGES", " FileTypeNotValid ");
                        ltErrorIcon.Visible = true;
                        fileIcon.FileContent.Dispose();
                        objPage.IconFile = null;
                        error = ltErrorIcon.Text;
                    }
                }
                else
                {
                    objPage.IconFile = null;
                }
                #endregion
                string BeforeID = "-1";
                string AfterID = "-1";
                if (cboPositionTab.SelectedIndex > 0 && cboPositionTab.SelectedValue != hdnPageID.Value)
                {
                    if (rbInsertPosition.SelectedValue == "After")
                    {
                        AfterID = cboPositionTab.SelectedItem.Value;
                    }
                    else if (rbInsertPosition.SelectedValue == "Before")
                    {
                        BeforeID = cboPositionTab.SelectedItem.Value;
                    }
                    else if (rbInsertPosition.SelectedValue.ToLower() == "add to end")
                    {
                        AfterID = "0";
                        BeforeID = "0";
                    }
                }
                if (rbURlType.SelectedIndex == 0)
                {
                    objPage.Url = null;
                }
                else
                {
                    if (rbURlType.SelectedIndex == 1 & txtUrl.Text.Length > 0)
                    {
                        objPage.Url = txtUrl.Text.ToString();
                    }
                    else
                    {
                        objPage.Url = null;
                    }
                }
                string selectedRolesView = GetListBoxText(lstSelectedRolesView);
                string selectedRolesEdit = GetListBoxText(lstSelectedRolesEdit);
                string EditUsers = GetEditUsers();
                string ViewUsers = GetViewUsers();
                string UserModuleIDs = string.Empty;
                string ModuleIDs = string.Empty;
                string ModuleTitles = string.Empty;
                string PaneNames = string.Empty;
                string Visibilities = string.Empty;
                string ModuleAllPages = string.Empty;
                string ModuleIsActives = string.Empty;
                string ModulePositions = string.Empty;
                string ModuleInstances = string.Empty;

                if (gdvUserModules.Rows.Count > 0)
                {
                    for (int i = 0; i < gdvUserModules.Rows.Count; i++)
                    {
                        Label lblPageID = gdvUserModules.Rows[i].FindControl("lblPageID") as Label;
                        Label lblSNo = gdvUserModules.Rows[i].FindControl("lblSNo") as Label;
                        Label lblUserModuleID = gdvUserModules.Rows[i].FindControl("lblUserModuleID") as Label;
                        HtmlInputCheckBox chkBoxItem = gdvUserModules.Rows[i].FindControl("chkBoxIsActiveItem") as HtmlInputCheckBox;
                        HtmlInputCheckBox chkAllPagesItem = gdvUserModules.Rows[i].FindControl("chkBoxAllPagesItem") as HtmlInputCheckBox;
                        Label lblModuleID = gdvUserModules.Rows[i].FindControl("lblModuleID") as Label;
                        Label lblTitle = gdvUserModules.Rows[i].FindControl("lblTitle") as Label;
                        Label lblInheritView = gdvUserModules.Rows[i].FindControl("lblInheritView1") as Label;
                        Label lblPaneName = gdvUserModules.Rows[i].FindControl("lblPaneName") as Label;
                        Label lblModuleOrder = gdvUserModules.Rows[i].FindControl("lblModuleOrder") as Label;
                        Label lblPosition = gdvUserModules.Rows[i].FindControl("lblPosition") as Label;
                        Label lblInstanceID = gdvUserModules.Rows[i].FindControl("lblInstanceID") as Label;
                        if (lblUserModuleID != null && lblModuleID != null && lblTitle != null && lblInheritView != null && lblPaneName != null && lblPosition != null && lblInstanceID != null && chkAllPagesItem != null && chkBoxItem != null)
                        {
                            UserModuleIDs += lblUserModuleID.Text + ",";
                            ModuleIDs += lblModuleID.Text + ",";
                            ModuleTitles += lblTitle.Text + ",";
                            Visibilities += lblInheritView.Text.ToLower() == "true" ? "1," : "0,";
                            PaneNames += lblPaneName.Text + ",";
                            ModulePositions += lblPosition.Text + ",";
                            ModuleInstances += lblInstanceID.Text + ",";
                            ModuleAllPages += chkAllPagesItem.Checked == true ? "1," : "0,";
                            ModuleIsActives += chkBoxItem.Checked == true ? "1," : "0,";
                        }
                    }
                    UserModuleIDs = UserModuleIDs.Substring(0, UserModuleIDs.Length - 1);
                    ModuleIDs = ModuleIDs.Substring(0, ModuleIDs.Length - 1);
                    ModuleTitles = ModuleTitles.Substring(0, ModuleTitles.Length - 1);
                    Visibilities = Visibilities.Substring(0, Visibilities.Length - 1);
                    PaneNames = PaneNames.Substring(0, PaneNames.Length - 1);
                    ModulePositions = ModulePositions.Substring(0, ModulePositions.Length - 1);
                    ModuleInstances = ModuleInstances.Substring(0, ModuleInstances.Length - 1);
                    ModuleAllPages = ModuleAllPages.Substring(0, ModuleAllPages.Length - 1);
                    ModuleIsActives = ModuleIsActives.Substring(0, ModuleIsActives.Length - 1);
                }


                //Do database insertion only if No error in image file upload
                if (error == "")
                {
                    //:TODO: need to do this portion after we get the clik event with page ID
                    if (Int32.Parse(hdnPageID.Value) > 0)
                    {
                        objPage.PageID = int.Parse(hdnPageID.Value);
                        dbPages.sp_CheckUnquiePageName(objPage.PageID, GetPortalID, objPage.PageName, true, ref _newPageCount);
                        if (_newPageCount == 0 && objPage.PageID != Int32.Parse(cboParentPage.SelectedItem.Value))
                        {
                            dbPages.sp_PageAddBlock(Int32.Parse(hdnPageID.Value), 1, objPage.PageName, objPage.IsVisible, objPage.ParentId, objPage.IconFile,
                               objPage.DisableLink, objPage.Title, objPage.Description, objPage.KeyWords, objPage.Url,
                               objPage.StartDate, objPage.EndDate, objPage.RefreshInterval, objPage.PageHeadText, objPage.IsSecure,
                               objPage.IsActive, DateTime.Now, objPage.IsShownInFooter, objPage.IsRequiredPage, Int32.Parse(BeforeID), Int32.Parse(AfterID),
                               selectedRolesView, selectedRolesEdit, ViewUsers, EditUsers, ModuleIDs, UserModuleIDs, ModuleTitles, PaneNames, ModuleAllPages,
                               Visibilities, Visibilities, ModulePositions, ModuleInstances, ModuleIsActives, GetPortalID, GetUsername, ref newPageID);
                            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("PAGES", "PageUpdatedSuccessfully"), "", SageMessageType.Success);
                            BindPages();
                            BindPageControls();
                            PopupPanelVisibility(false, true);
                        }
                        else
                        {
                            ShowMessage(SageMessageTitle.Notification.ToString(), txtPageName.Text.Trim() + " " + GetSageMessage("PAGES", "NameAlreadyExists"), "", SageMessageType.Error);
                        }
                    }
                    else if (Int32.Parse(hdnPageID.Value) == 0)
                    {
                        // add or copy
                        dbPages.sp_CheckUnquiePageName(0, GetPortalID, objPage.PageName, false, ref _newPageCount);
                        if (_newPageCount == 0)
                        {
                            dbPages.sp_PageAddBlock(0, 1, objPage.PageName, objPage.IsVisible, objPage.ParentId, objPage.IconFile,
                                objPage.DisableLink, objPage.Title, objPage.Description, objPage.KeyWords, objPage.Url,
                                objPage.StartDate, objPage.EndDate, objPage.RefreshInterval, objPage.PageHeadText, objPage.IsSecure,
                                objPage.IsActive, DateTime.Now, objPage.IsShownInFooter, objPage.IsRequiredPage, Int32.Parse(BeforeID), Int32.Parse(AfterID),
                                selectedRolesView, selectedRolesEdit, ViewUsers, EditUsers, ModuleIDs, UserModuleIDs, ModuleTitles, PaneNames, ModuleAllPages,
                                Visibilities, Visibilities, ModulePositions, ModuleInstances, ModuleIsActives, GetPortalID, GetUsername, ref newPageID);

                            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("PAGES", "PageAddedSuccessfully"), "", SageMessageType.Success);
                          
                           
                        }
                        else
                        {
                            ShowMessage(SageMessageTitle.Notification.ToString(), txtPageName.Text.Trim() + " " + GetSageMessage("PAGES", "NameAlreadyExist"), "", SageMessageType.Error);
                        }
                    }
                   
                }
                TabContainerManagePages.ActiveTabIndex = activeTabIndex;
            }
            else
            {
                ShowMessage(SageMessageTitle.Notification.ToString(), txtPageName.Text.Trim() + " " + GetSageMessage("PAGES", "ParentNotSame"), "", SageMessageType.Error);
            }
        }

        private string GetViewUsers()
        {
            string arrUsers1 = string.Empty;
            if (ViewState["dtViewUsers"] != null)
            {
                DataTable dtViewUsers = (DataTable)ViewState["dtViewUsers"];
                DataTable newdtViewUsers = dtViewUsers.Clone();
                for (int k = 0; k < dtViewUsers.Rows.Count; k++)
                {
                    arrUsers1 += dtViewUsers.Rows[k]["Username"].ToString() + ",";
                }
                if (arrUsers1 != "")
                {
                    arrUsers1 = arrUsers1.Remove(arrUsers1.LastIndexOf(","));
                }
            }
            return arrUsers1;
        }

        private string GetEditUsers()
        {
            string arrUsers2 = string.Empty;
            if (ViewState["dtEditUsers"] != null)
            {
                DataTable dtEditUsers = (DataTable)ViewState["dtEditUsers"];
                DataTable newdtEditUsers = dtEditUsers.Clone();
                for (int l = 0; l < dtEditUsers.Rows.Count; l++)
                {
                    arrUsers2 += dtEditUsers.Rows[l]["Username"].ToString() + ",";
                }
                if (arrUsers2 != "")
                {
                    arrUsers2 = arrUsers2.Remove(arrUsers2.LastIndexOf(","));
                }
            }
            return arrUsers2;
        }


        private string GetListBoxText(ListBox lstBox)
        {
            string arrRolesID = string.Empty;
            string selectedRolesID = string.Empty;
            foreach (ListItem li in lstBox.Items)
            {
                selectedRolesID = li.Value;
                arrRolesID += selectedRolesID + ",";
            }
            if (arrRolesID.Length > 0)
            {
                selectedRolesID = arrRolesID.Remove(arrRolesID.LastIndexOf(","));
            }
            return selectedRolesID;
        }

        public bool CheckValidPageName(string pageName)
        {
            List<sp_PagesGetByPortalIDResult> allPages = dbPages.sp_PagesGetByPortalID(GetPortalID).ToList();

            int pagesCount = allPages.Count;
            bool flag = false;

            for (int index = 0; index <= pagesCount - 1; index++)
            {
                sp_PagesGetByPortalIDResult objPage = allPages[index];
                if (objPage.PageName == pageName)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        protected void fileIcon_Load(object sender, EventArgs e)
        {
            ltErrorIcon.Visible = false;
        }

        protected void ShowError(object sender, EventArgs e)
        {
            TabContainerManagePages.ActiveTabIndex = 2;
            ltErrorIcon.Text = GetSageMessage("PAGES", "File ") + fileIcon.FileName + " " + GetSageMessage("PAGES", " UploadedFileError");
            ltErrorIcon.Visible = true;
        }

        protected void ibCancel_Click(object sender, ImageClickEventArgs e)
        {
            gdvUserModules.EditIndex = -1;
            gdvUserModules.DataSource = (DataTable)ViewState["dtUserModules"];
            gdvUserModules.DataBind();
            BindPages();
            PopupPanelVisibility(false, true);
            
        }

        #region Dinesh Modifications

        private void addUsername(string userName, string permissionType)
        {
            try
            {               
                MembershipController _member=new MembershipController();
                UserInfo _user = _member.GetUserDetails(GetPortalID, userName);
                if (_user.UserExists)
                {
                    bool isUsernameExists = false;
                    DataTable dtBackupUsername = new DataTable();
                    if (permissionType.ToLower() == "view")
                    {
                        if (ViewState["dtViewUsers"] != null)
                        {
                            dtBackupUsername = (DataTable)ViewState["dtViewUsers"];
                        }
                    }
                    else if (permissionType.ToLower() == "edit")
                    {
                        if (ViewState["dtEditUsers"] != null)
                        {
                            dtBackupUsername = (DataTable)ViewState["dtEditUsers"];
                        }
                    }

                    for (int i = 0; i < dtBackupUsername.Rows.Count; i++)
                    {
                        if (dtBackupUsername.Rows[i]["Username"].ToString() == userName)
                        {
                            isUsernameExists = true;
                        }
                    }
                    if (!isUsernameExists)
                    {
                        if (dtBackupUsername.Columns.Count == 0)
                        {
                            dtBackupUsername.Columns.Add("Username");
                        }
                        dtBackupUsername = BindDataTable(userName, dtBackupUsername);
                        if (permissionType.ToLower() == "view")
                        {
                            ViewState["dtViewUsers"] = dtBackupUsername;
                            BindUsernameGrid("view");
                        }
                        else if (permissionType.ToLower() == "edit")
                        {
                            ViewState["dtEditUsers"] = dtBackupUsername;
                            BindUsernameGrid("edit");
                        }
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("PAGES", "UsernameAlreadyExist"), "", SageMessageType.Alert);
                    }
                }
                else
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("PAGES", "UsernameDoesnotExist"), "", SageMessageType.Alert);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvEditUsernames_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveUser")
                {
                    DataTable dtEditUsers = (DataTable)ViewState["dtEditUsers"];
                    DataTable newdtEditUsers = dtEditUsers.Clone();
                    for (int i = 0; i < dtEditUsers.Rows.Count; i++)
                    {
                        if (e.CommandArgument.ToString() != dtEditUsers.Rows[i]["Username"].ToString())
                        {
                            DataRow newDR = newdtEditUsers.NewRow();
                            newDR["Username"] = dtEditUsers.Rows[i]["Username"].ToString();
                            newdtEditUsers.Rows.Add(newDR);
                        }
                    }
                    ViewState["dtEditUsers"] = newdtEditUsers;
                    gdvEditUsernames.DataSource = newdtEditUsers;
                    gdvEditUsernames.DataBind();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvViewUsernames_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imbDelete");
                    imgDelete.Attributes.Add("onclick", "return confirm('" + GetSageMessage("PAGES", "AreYouSureToDelete") + "')");
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvViewUsernames_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveUser")
                {
                    DataTable dtViewUsers = (DataTable)ViewState["dtViewUsers"];
                    DataTable newdtEditUsers = dtViewUsers.Clone();
                    for (int i = 0; i < dtViewUsers.Rows.Count; i++)
                    {
                        if (e.CommandArgument.ToString() != dtViewUsers.Rows[i]["Username"].ToString())
                        {
                            DataRow newDR = newdtEditUsers.NewRow();
                            newDR["Username"] = dtViewUsers.Rows[i]["Username"].ToString();
                            newdtEditUsers.Rows.Add(newDR);
                        }
                    }
                    ViewState["dtViewUsers"] = newdtEditUsers;
                    gdvViewUsernames.DataSource = newdtEditUsers;
                    gdvViewUsernames.DataBind();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void PopupPanelVisibility(bool IsPageVisible, bool IsPageListVisible)
        {
            pnlPage.Visible = IsPageVisible;
            pnlPageList.Visible = IsPageListVisible;
        }

        protected void gdvPageList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                HttpContext.Current.Session["ActiveTabIndex"] = TabContainerManagePages.ActiveTabIndex;
                string[] commandArgsAccept = e.CommandArgument.ToString().Split(new char[] { ',' });
                Int32 PageId = Int32.Parse(commandArgsAccept[0].ToString());//it gives PageId                

                if (e.CommandName == "PageEdit")
                {
                    bool IsActive = bool.Parse(commandArgsAccept[1].ToString());//it gives IsActive
                    hdnPageID.Value = PageId.ToString();
                    LoadInstances();
                    BindParentPages(PageId);
                    ClearRolesModule();
                    ClearUsernameModule();
                    BindData(PageId, IsActive);
                    BindPostionPage();
                    lblCreatedBy.Visible = true;
                    lblUpdatedBy.Visible = true;
                    PopupPanelVisibility(true, false);
                    PageModulePanelVisibility(true, false, false);
                    TabContainerManagePages.ActiveTabIndex = 0;
                    SetDefaultModule();
                    cboPanes.SelectedIndex = cboPanes.Items.IndexOf(cboPanes.Items.FindByValue(SystemSetting.glbDefaultPane));
                    cboPosition.SelectedIndex = cboPosition.Items.Count - 1;
                    rbInsertPosition.SelectedIndex = -1;
                }
                else if (e.CommandName == "PageDelete")
                {
                    DeletePage(PageId);                   
                    BindPages();
                }
                else if (e.CommandName == "PageUp")
                {
                    MovePage(PageId, true);
                    BindPages();
                }
                else if (e.CommandName == "PageDown")
                {
                    MovePage(PageId, false);
                    BindPages();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvPageList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imbDeletePage");
                    imgDelete.Attributes.Add("onclick", "return confirm('" + GetSageMessage("PAGES", "DeletePageConfirm") + "')");
                    SageFrameConfig sageFrameConfig = new SageFrameConfig();
                    HiddenField hdnIsActive = (HiddenField)e.Row.FindControl("hdnIsActive");
                    HiddenField hdnPageName = (HiddenField)e.Row.FindControl("hdnPageName");
                    ImageButton imbDeletePage = (ImageButton)e.Row.FindControl("imbDeletePage");
                    HtmlInputCheckBox chkIsActiveItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveItem");
                    HiddenField hdfPageOrder = (HiddenField)e.Row.FindControl("hdfPageOrder");
                    HiddenField hdfMinPageOrder = (HiddenField)e.Row.FindControl("hdfMinPageOrder");
                    HiddenField hdfMaxPageORder = (HiddenField)e.Row.FindControl("hdfMaxPageOrder");
                    ImageButton imbMovePageUp = (ImageButton)e.Row.FindControl("imbMovePageUp");
                    ImageButton imbMovePageDown = (ImageButton)e.Row.FindControl("imbMovePageDown");

                    chkIsActiveItem.Checked = bool.Parse(hdnIsActive.Value);
                    string defaultPage = string.Empty;
                    defaultPage = sageFrameConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage);
                    if (hdnPageName.Value.ToLower() == defaultPage.ToLower().Replace("-"," "))
                    {
                        imbDeletePage.Enabled = false;
                        imbDeletePage.Visible = false;
                        chkIsActiveItem.Disabled = true;
                        chkIsActiveItem.Attributes.Add("class", "disableCheckBox");
                    }
                    else
                    {
                        chkIsActiveItem.Attributes.Add("onclick", "javascript:Check(this,'cssCheckBoxIsActiveHeader','" + gdvPageList.ClientID + "','cssCheckBoxIsActiveItem');");
                    }
                    if (hdfPageOrder.Value == hdfMinPageOrder.Value)
                    {
                        e.Row.FindControl("imbMovePageUp").Visible = false;
                    }
                    else
                    {
                        e.Row.FindControl("imbMovePageUp").Visible = true;
                    }
                    if (hdfPageOrder.Value == hdfMaxPageORder.Value)
                    {
                        e.Row.FindControl("imbMovePageDown").Visible = false;
                    }
                    else
                    {
                        e.Row.FindControl("imbMovePageDown").Visible = true;
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Header)
                {
                    HtmlInputCheckBox chkIsActiveHeader = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveHeader");
                    chkIsActiveHeader.Attributes.Add("onclick", "javascript:SelectAllCheckboxesSpecific(this,'" + gdvPageList.ClientID + "','cssCheckBoxIsActiveItem');");
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void MovePage(int pageId, bool up)
        {
            try
            {
                var pageinfo = dbPages.sp_PageSortOrderUpdate(pageId, up);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void DeletePage(int PageId)
        {
            try
            {
                dbPages.sp_PagesDelete(PageId, GetUsername, GetPortalID);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void ibAdd_Click(object sender, ImageClickEventArgs e)
        {
            ClearForm();
            ClearRoles();
            ClearUsername();
            BindParentPages(0);
            ClearPageControls();
            lblCreatedBy.Visible = false;
            lblUpdatedBy.Visible = false;
            imgIcon.Visible = false;
            hdnPageID.Value = "0";
            PopupPanelVisibility(true, false);
            PageModulePanelVisibility(true, false, false);
            txtTitle.Text = "";
            cboParentPage.SelectedIndex = -1;
            cboPositionTab.SelectedIndex = -1;
            cboPanes.SelectedIndex = cboPanes.Items.IndexOf(cboPanes.Items.FindByValue(SystemSetting.glbDefaultPane));
            rbInsertPosition.SelectedIndex = rbInsertPosition.Items.IndexOf(rbInsertPosition.Items.FindByValue("Add to End"));
            chkMenu.Enabled = true;
        }

        #endregion

        #region Updated Version
        protected void btnAddAllRoleView_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in lstUnselectedViewRoles.Items)
            {
                lstSelectedRolesView.Items.Add(li);
            }
            lstUnselectedViewRoles.Items.Clear();
        }

        protected void btnAddRoleView_Click(object sender, EventArgs e)
        {
            if (lstUnselectedViewRoles.SelectedIndex != -1)
            {
                int[] selectedIndexs = lstUnselectedViewRoles.GetSelectedIndices();
                for (int i = selectedIndexs.Length - 1; i >= 0; i--)
                {
                    lstSelectedRolesView.Items.Add(lstUnselectedViewRoles.Items[selectedIndexs[i]]);
                    lstUnselectedViewRoles.Items.Remove(lstUnselectedViewRoles.Items[selectedIndexs[i]]);
                }
                lstUnselectedViewRoles.SelectedIndex = -1;
            }
        }

        protected void btnRemoveRoleView_Click(object sender, EventArgs e)
        {
            if (lstSelectedRolesView.SelectedIndex != -1)
            {
                int[] selectedIndexs = lstSelectedRolesView.GetSelectedIndices();
                for (int i = selectedIndexs.Length - 1; i >= 0; i--)
                {
                    if (!SystemSetting.SYSTEM_SUPER_ROLES.Contains(lstSelectedRolesView.Items[selectedIndexs[i]].Text, StringComparer.OrdinalIgnoreCase))
                    {
                        lstUnselectedViewRoles.Items.Add(lstSelectedRolesView.Items[selectedIndexs[i]]);
                        lstSelectedRolesView.Items.Remove(lstSelectedRolesView.Items[selectedIndexs[i]]);
                    }
                }
                lstSelectedRolesView.SelectedIndex = -1;
            }
        }

        protected void btnRemoveAllRoleView_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in lstSelectedRolesView.Items)
            {
                if (!SystemSetting.SYSTEM_SUPER_ROLES.Contains(li.Text, StringComparer.OrdinalIgnoreCase))
                {
                    lstUnselectedViewRoles.Items.Add(li);
                }
            }
            lstSelectedRolesView.Items.Clear();
            BindSelectedRolesInListBox(lstSelectedRolesView);
        }

        protected void btnAddAllRoleEdit_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in lstUnselectedEditRoles.Items)
            {
                lstSelectedRolesEdit.Items.Add(li);
            }
            lstUnselectedEditRoles.Items.Clear();
        }

        protected void btnAddRoleEdit_Click(object sender, EventArgs e)
        {
            if (lstUnselectedEditRoles.SelectedIndex != -1)
            {
                int[] selectedIndexs = lstUnselectedEditRoles.GetSelectedIndices();
                for (int i = selectedIndexs.Length - 1; i >= 0; i--)
                {
                    lstSelectedRolesEdit.Items.Add(lstUnselectedEditRoles.Items[selectedIndexs[i]]);
                    lstUnselectedEditRoles.Items.Remove(lstUnselectedEditRoles.Items[selectedIndexs[i]]);
                }
                lstUnselectedEditRoles.SelectedIndex = -1;
            }
        }

        protected void btnRemoveRoleEdit_Click(object sender, EventArgs e)
        {
            if (lstSelectedRolesEdit.SelectedIndex != -1)
            {
                int[] selectedIndexs = lstSelectedRolesEdit.GetSelectedIndices();
                for (int i = selectedIndexs.Length - 1; i >= 0; i--)
                {
                    if (!SystemSetting.SYSTEM_SUPER_ROLES.Contains(lstSelectedRolesEdit.Items[selectedIndexs[i]].Text, StringComparer.OrdinalIgnoreCase))
                    {
                        lstUnselectedEditRoles.Items.Add(lstSelectedRolesEdit.Items[selectedIndexs[i]]);
                        lstSelectedRolesEdit.Items.Remove(lstSelectedRolesEdit.Items[selectedIndexs[i]]);
                    }
                }
                lstSelectedRolesEdit.SelectedIndex = -1;
            }
        }

        protected void btnRemoveAllRoleEdit_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in lstSelectedRolesEdit.Items)
            {
                if (!SystemSetting.SYSTEM_SUPER_ROLES.Contains(li.Text, StringComparer.OrdinalIgnoreCase))
                {
                    lstUnselectedEditRoles.Items.Add(li);
                }
            }
            lstSelectedRolesEdit.Items.Clear();
            BindSelectedRolesInListBox(lstSelectedRolesEdit);
        }

        private void BindRolesInListBox(ListBox lst)
        {
            try
            {
                DataTable dtRoles = GetAllRoles();
                lst.DataSource = dtRoles;
                lst.DataTextField = "RoleName";
                lst.DataValueField = "RoleID";
                lst.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private DataTable GetAllRoles()
        {
            DataTable dtRole = new DataTable();
            if (HttpContext.Current.Cache["AllRoles"] != null)
            {
                return (DataTable)HttpContext.Current.Cache["AllRoles"];
            }
            else
            {
                dtRole.Columns.Add("RoleID");
                dtRole.Columns.Add("RoleName");
                dtRole.AcceptChanges();
                RolesManagementDataContext dbRoles = new RolesManagementDataContext(SystemSetting.SageFrameConnectionString);
                var roles = dbRoles.sp_PortalRoleList(GetPortalID, true, GetUsername);
                foreach (var role in roles)
                {
                    string roleName = role.RoleName;
                    if (!SystemSetting.SYSTEM_SUPER_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                    {
                        string rolePrefix = GetPortalSEOName + "_";
                        roleName = roleName.Replace(rolePrefix, "");
                        DataRow dr = dtRole.NewRow();
                        dr["RoleID"] = role.RoleID;
                        dr["RoleName"] = roleName;
                        dtRole.Rows.Add(dr);
                    }
                }
                HttpContext.Current.Cache["AllRoles"] = dtRole;
                return dtRole;
            }
        }

        private void BindSelectedRolesInListBox(ListBox lst)
        {
            try
            {
                DataTable dtRoles = GetAllSuperRoles();
                if (dtRoles != null)
                {
                    lst.DataSource = dtRoles;
                    lst.DataTextField = "RoleName";
                    lst.DataValueField = "RoleID";
                    lst.DataBind();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private DataTable GetAllSuperRoles()
        {
            DataTable dtRole = new DataTable();
            if (HttpContext.Current.Cache["AllSuperRoles"] != null)
            {
                return (DataTable)HttpContext.Current.Cache["AllSuperRoles"];
            }
            else
            {
                dtRole.Columns.Add("RoleID");
                dtRole.Columns.Add("RoleName");
                dtRole.AcceptChanges();

                RolesManagementDataContext dbRoles = new RolesManagementDataContext(SystemSetting.SageFrameConnectionString);
                var roles = dbRoles.sp_PortalRoleList(GetPortalID, true, GetUsername);
                foreach (var role in roles)
                {
                    string roleName = role.RoleName;
                    if (SystemSetting.SYSTEM_SUPER_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                    {
                        DataRow dr = dtRole.NewRow();
                        dr["RoleID"] = role.RoleID;
                        dr["RoleName"] = roleName;
                        dtRole.Rows.Add(dr);
                    }
                }
            }
            return dtRole;
        }

        #endregion

        protected void imbAddUser_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtViewUsername.Text != "")
                {
                    addUsername(txtViewUsername.Text, "view");
                    txtViewUsername.Text = "";
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbAddUserEditPermission_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtEditUsername.Text != "")
                {
                    addUsername(txtEditUsername.Text, "edit");
                    txtEditUsername.Text = "";
                    //mpePagePermissionEdit.Hide();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void imbPageSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string seletedPagesID = string.Empty;
                string IsActive = string.Empty;
                for (int i = 0; i < gdvPageList.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem = (HtmlInputCheckBox)gdvPageList.Rows[i].FindControl("chkBoxIsActiveItem");
                    HiddenField hdnPageID = (HiddenField)gdvPageList.Rows[i].FindControl("hdnPageID");
                    seletedPagesID = seletedPagesID + hdnPageID.Value.Trim() + ",";
                    IsActive = IsActive + (chkBoxItem.Checked ? "1" : "0") + ",";
                }
                if (seletedPagesID.Length > 1 && IsActive.Length > 0)
                {
                    seletedPagesID = seletedPagesID.Substring(0, seletedPagesID.Length - 1);
                    IsActive = IsActive.Substring(0, IsActive.Length - 1);
                    dbPages.sp_PagesUpdateChanges(seletedPagesID, IsActive, GetPortalID, GetUsername);
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("PAGES", "SelectedChangesAreSavedSuccessfully"), "", SageMessageType.Success);
                }
               
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

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
        private void BindUsers(string searchText, string dropDownValue, GridView gdvUsers)
        {
            var users = dbUser.sp_SageFrameUserListSearch(dropDownValue, searchText, GetPortalID, GetUsername);
            gdvUsers.DataSource = users;
            gdvUsers.DataBind();
        }

        protected void BindControlsInViewPermissionPopUp()
        {
            txtSearchText.Value = string.Empty;
            BindRolesInDropDown(ddlSearchRole);
            BindUsers(txtSearchText.Value.Trim(), ddlSearchRole.SelectedValue.ToString(), gdvUser);
        }

        protected void gdvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlInputCheckBox chkItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxItem");
                chkItem.Attributes.Add("onclick", "javascript:Check(this,'cssCheckBoxHeader','" + gdvUser.ClientID + "','cssCheckBoxItem');");
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                HtmlInputCheckBox chkHeader = (HtmlInputCheckBox)e.Row.FindControl("chkBoxHeader");
                chkHeader.Attributes.Add("onclick", "javascript:SelectAllCheckboxesSpecific(this,'" + gdvUser.ClientID + "','cssCheckBoxItem');");
            }
        }

        protected void imgSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindUsers(txtSearchText.Value.Trim(), ddlSearchRole.SelectedValue.ToString(), gdvUser);
                PopupPanelVisibility(pnlUsersView, true);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgAddSelectedUsers_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string seletedUsername = string.Empty;
                for (int i = 0; i < gdvUser.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem = (HtmlInputCheckBox)gdvUser.Rows[i].FindControl("chkBoxItem");
                    if (chkBoxItem.Checked == true)
                    {
                        Label lblUsernameD = (Label)gdvUser.Rows[i].FindControl("lblUsernameD");
                        seletedUsername = lblUsernameD.Text.Trim();
                        if (seletedUsername.Length > 1)
                        {
                            addUsername(seletedUsername, "view");
                        }
                    }
                }
                PopupPanelVisibility(pnlUsersView, false);
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
                BindUsers(txtSearchText.Value.Trim(), ddlSearchRole.SelectedValue.ToString(), gdvUser);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void BindControlsInEditPermissionPopUp()
        {
            txtSearchUserText.Value = string.Empty;
            BindRolesInDropDown(ddlSearchEditRole);

            BindUsers(txtSearchUserText.Value.Trim(), ddlSearchEditRole.SelectedValue.ToString(), gdvEditUser);
        }

        protected void gdvEditUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlInputCheckBox chkItem2 = (HtmlInputCheckBox)e.Row.FindControl("chkBoxItem2");
                chkItem2.Attributes.Add("onclick", "javascript:Check(this,'cssCheckBoxHeader','" + gdvEditUser.ClientID + "','cssCheckBoxItem');");
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                HtmlInputCheckBox chkHeader2 = (HtmlInputCheckBox)e.Row.FindControl("chkBoxHeader2");
                chkHeader2.Attributes.Add("onclick", "javascript:SelectAllCheckboxesSpecific(this,'" + gdvEditUser.ClientID + "','cssCheckBoxItem');");
            }
        }

        protected void imgUserSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindUsers(txtSearchUserText.Value.Trim(), ddlSearchEditRole.SelectedValue.ToString(), gdvEditUser);
                PopupPanelVisibility(pnlUsersEdit, true);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgAddSelectedEditUsers_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string seletedUsername = string.Empty;
                for (int i = 0; i < gdvEditUser.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem2 = (HtmlInputCheckBox)gdvEditUser.Rows[i].FindControl("chkBoxItem2");
                    if (chkBoxItem2.Checked == true)
                    {
                        Label lblEditUsernameD = (Label)gdvEditUser.Rows[i].FindControl("lblEditUsernameD");
                        seletedUsername = lblEditUsernameD.Text.Trim();
                        if (seletedUsername.Length > 1)
                        {
                            addUsername(seletedUsername, "edit");
                        }
                    }
                }
                PopupPanelVisibility(pnlUsersEdit, false);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void ddlSearchEditRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindUsers(txtSearchUserText.Value.Trim(), ddlSearchEditRole.SelectedValue.ToString(), gdvEditUser);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        public void InitializeControls()
        {
            cboPanes.DataSource = SageFrameLists.ModulePanes();
            cboPanes.DataTextField = "Value";
            cboPanes.DataValueField = "Key";
            cboPanes.DataBind();
            if ((cboPanes.Items.FindByValue(SystemSetting.glbDefaultPane) != null))
            {
                cboPanes.SelectedIndex = cboPanes.Items.IndexOf(cboPanes.Items.FindByValue(SystemSetting.glbDefaultPane));
            }

            if (cboPermission.Items.Count > 0)
            {
                // view
                cboPermission.SelectedIndex = 0;
            }
            LoadPositions();
        }

        /// <summary>
        /// This function load position dropdown control
        /// </summary>
        public void LoadPositions()
        {

            cboPosition.Items.Clear();
            if (cboInstances.Items.Count > 0)
            {
                cboPosition.DataSource = SageFrameLists.PositionType();
                cboPosition.DataTextField = "Value";
                cboPosition.DataValueField = "Key";
            }
            else
            {
                cboPosition.Items.Insert(cboPosition.Items.Count, new ListItem("Bottom", "Bottom"));
            }
            cboPosition.DataBind();
            cboPosition.SelectedIndex = cboPosition.Items.Count - 1;
        }

        /// <summary>
        /// This function loads the module instances of currently active tab page
        /// </summary>
        private void LoadInstances()
        {
            try
            {
                cboInstances.Items.Clear();
                cboInstances.DataSource = dbModules.sp_GetPageModulesByPageID(Int32.Parse(hdnPageID.Value), GetPortalID, cboPanes.SelectedItem.Value);
                cboInstances.DataTextField = "UserModuleTitle";
                cboInstances.DataValueField = "UserModuleID";
                cboInstances.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        /// <summary>
        /// This function displays Instance of modules if it is Above or Below position
        /// </summary>
        private void DisplayInstances()
        {
            LoadInstances();
            if ((cboPosition.SelectedItem != null))
            {
                switch (cboPosition.SelectedItem.Value.ToLower())
                {
                    case "top":
                    case "bottom":
                        cboInstances.Visible = false;
                        break;
                    case "above":
                    case "below":
                        cboInstances.Visible = true;
                        break;
                }
            }
            lblInstance.Visible = cboInstances.Visible;
        }

        /// <summary>
        /// This function binds the add module panel dropdowns
        /// </summary>
        public void ModulesBindData()
        {
            try
            {
                // new module
                cboModules.Visible = true;
                txtTitle.Visible = true;
                cboPermission.Enabled = true;

                // get list of modules
                try
                {
                    cboModules.DataSource = dbModules.sp_ModulesGetByPortalID(GetPortalID);
                    cboModules.DataTextField = "FriendlyName";
                    cboModules.DataValueField = "ModuleID";
                    cboModules.DataBind();
                    cboModules.Items.Insert(0, new ListItem("<Select A Module>", "-1"));

                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetDefaultModule()
        {
            string DefaultModule = "SFE_HTML";
            int intModuleID = -1;
            if (!string.IsNullOrEmpty(DefaultModule))
            {
                try
                {
                    var objModule = dbModules.sp_ModulesGetByModuleName(DefaultModule.ToLower(), GetPortalID).SingleOrDefault();
                    if ((objModule != null))
                    {
                        intModuleID = objModule.ModuleID;
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }

            if (intModuleID != -1 && ((cboModules.Items.FindByValue(intModuleID.ToString()) != null)))
            {
                cboModules.SelectedIndex = cboModules.Items.IndexOf(cboPanes.Items.FindByValue(intModuleID.ToString()));
            }
            else
            {
                cboModules.SelectedIndex = 0;
            }
        }

        //Need To add to the Ctl_pageMangaement control
        private void AddNewUserModule(int SNo, string title, int PageID, int ModuleID, Int32 Position, Int32 InstanceID, string paneName, SageFrameEnums.ViewPermissionType permissionType, string align)
        {
            bool InheritViewPermissions = false;
            bool IsSameAsPageVisibility = true;
            try
            {
                // add module
                if (ModuleID > 0)
                {
                    switch (permissionType)
                    {
                        case SageFrameEnums.ViewPermissionType.View:
                            InheritViewPermissions = true;
                            IsSameAsPageVisibility = true;
                            break;
                        case SageFrameEnums.ViewPermissionType.Edit:
                            InheritViewPermissions = false;
                            IsSameAsPageVisibility = false;
                            break;
                    }
                    bool AllPages = chkShowInAllPages.Checked;
                    Int32 ModuleOrder = 0;
                    //To add to the datatable and bind to grid on run time
                    DataTable dtUserModules;
                    if (ViewState["dtUserModules"] == null)
                    {
                        dtUserModules = new DataTable();
                    }
                    else
                    {
                        dtUserModules = (DataTable)ViewState["dtUserModules"];
                    }
                    SNo = dtUserModules.Rows.Count + 1;
                    DataRow UserModulesRow = dtUserModules.NewRow();
                    UserModulesRow["SNo"] = SNo;
                    UserModulesRow["PageID"] = 0;
                    UserModulesRow["UserModuleID"] = 0;
                    UserModulesRow["ModuleID"] = ModuleID;
                    UserModulesRow["Title"] = title;
                    UserModulesRow["Visibility"] = IsSameAsPageVisibility;
                    UserModulesRow["InheritView"] = InheritViewPermissions;
                    UserModulesRow["PaneName"] = paneName;
                    UserModulesRow["AllPages"] = AllPages;
                    UserModulesRow["ModuleOrder"] = ModuleOrder;
                    UserModulesRow["IsActive"] = true;
                    UserModulesRow["MaxModuleOrder"] = "";
                    UserModulesRow["MinModuleOrder"] = "";
                    UserModulesRow["Position"] = Position;
                    UserModulesRow["InstanceID"] = InstanceID;
                    dtUserModules.Rows.Add(UserModulesRow);
                    dtUserModules.AcceptChanges();
                    ViewState["dtUserModules"] = dtUserModules;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void cboPanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Int32.Parse(hdnPageID.Value) != 0)
            {
                if (hdnPane.Value == "" || hdnPane.Value != cboPanes.SelectedValue)
                {
                    lblPosition.Visible = true;
                    cboPosition.Visible = true;
                    LoadPositions();
                }
                else
                {
                    lblPosition.Visible = false;
                    cboPosition.Visible = false;
                    cboInstances.Visible = false;
                }
            }
            DisplayInstances();
        }

        protected void cboPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayInstances();
        }

        protected void ibModuleControlSave_Click(object sender, ImageClickEventArgs e)
        {
            string ModuleNotSelected = string.Empty;
            try
            {
                if (cboModules.SelectedIndex > 0)
                {
                    string title = string.Empty;
                    //:TODO: need to check page permission to add for current user
                    if (txtModuleTitle.Text.Length > 0)
                    {
                        title = txtModuleTitle.Text;
                    }
                    else
                    {
                        title = cboModules.SelectedItem.Text;
                    }
                    SageFrameEnums.ViewPermissionType permissionType = SageFrameEnums.ViewPermissionType.View;
                    if ((cboPermission.SelectedItem != null))
                    {
                        permissionType = (SageFrameEnums.ViewPermissionType)Enum.Parse(typeof(SageFrameEnums.ViewPermissionType), cboPermission.SelectedItem.Value);
                    }
                    int position = -1;
                    Int32 InstanceID = 0;
                    if ((cboPosition.SelectedItem != null))
                    {
                        switch (cboPosition.SelectedItem.Value.ToLower())
                        {
                            case "top":
                                position = 0;
                                InstanceID = 0;
                                break;
                            case "above":
                                position = 1;
                                if (!string.IsNullOrEmpty(cboInstances.SelectedValue))
                                {
                                    InstanceID = Int32.Parse(cboInstances.SelectedValue);
                                }
                                else
                                {
                                    InstanceID = 0;
                                }

                                break;
                            case "below":
                                position = 2;
                                if (!string.IsNullOrEmpty(cboInstances.SelectedValue))
                                {
                                    InstanceID = Int32.Parse(cboInstances.SelectedValue);
                                }
                                else
                                {
                                    InstanceID = 0;
                                }

                                break;
                            case "bottom":
                                position = 3;
                                InstanceID = 0;
                                break;
                        }
                    }
                    AddNewUserModule(0, title, Int32.Parse(hdnPageID.Value), int.Parse(cboModules.SelectedItem.Value), position, InstanceID, cboPanes.SelectedItem.Value, permissionType, "");
                    BindUserModuleGrid();
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ControlPanel", "ModuleAddedSuccessfully"), "", SageMessageType.Success);
                    cboPanes.SelectedIndex = cboPanes.Items.IndexOf(cboPanes.Items.FindByValue(SystemSetting.glbDefaultPane));
                    PageModulePanelVisibility(true, false, false);
                }
                else
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("ControlPanel", "ModuleIsNotSelected"), "", SageMessageType.Alert);
                    PageModulePanelVisibility(false, true, false);
                }
            }
            catch (Exception exc)
            {
                ProcessException(exc);
            }
        }

        protected void gdvUserModules_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                SageFrameConfig pagebase = new SageFrameConfig();
                bool IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
                if (e.CommandName == "EditUserModulePermission")
                {
                    if (ViewState["dtViewUsersModules"] != null)
                    {
                        DataTable dtViewUsers = (DataTable)ViewState["dtViewUsersModules"];
                        dtViewUsers.Clear();
                    }
                    if (ViewState["dtEditUsersModules"] != null)
                    {
                        DataTable dtEditUsers = (DataTable)ViewState["dtEditUsersModules"];
                        dtEditUsers.Clear();
                    }
                    int userModuleId = int.Parse(e.CommandArgument.ToString());
                    hdnUserModuleID.Value = e.CommandArgument.ToString();
                    BindModuleData(userModuleId);
                    PageModulePanelVisibility(false, false, true);
                }
                else if (e.CommandName == "RemoveUserModule")
                {
                    DataTable dtUserModules = (DataTable)ViewState["dtUserModules"];
                    DataTable newdtUserModules = dtUserModules.Clone();
                    for (int i = 0; i < dtUserModules.Rows.Count; i++)
                    {
                        if (e.CommandArgument.ToString() != dtUserModules.Rows[i]["SNo"].ToString())
                        {
                            DataRow newDR = newdtUserModules.NewRow();
                            newDR["SNo"] = dtUserModules.Rows[i]["SNo"].ToString();
                            newDR["PageID"] = dtUserModules.Rows[i]["PageID"].ToString();
                            newDR["UsermoduleID"] = dtUserModules.Rows[i]["UsermoduleID"].ToString();
                            newDR["ModuleID"] = dtUserModules.Rows[i]["ModuleID"].ToString();
                            newDR["Title"] = dtUserModules.Rows[i]["Title"].ToString();
                            newDR["Visibility"] = dtUserModules.Rows[i]["Visibility"].ToString();
                            newDR["InheritView"] = dtUserModules.Rows[i]["InheritView"].ToString();
                            newDR["PaneName"] = dtUserModules.Rows[i]["PaneName"].ToString();
                            newDR["AllPages"] = dtUserModules.Rows[i]["AllPages"].ToString();
                            newDR["IsActive"] = dtUserModules.Rows[i]["IsActive"].ToString();
                            newDR["ModuleOrder"] = dtUserModules.Rows[i]["ModuleOrder"].ToString();
                            newDR["MaxModuleOrder"] = dtUserModules.Rows[i]["MaxModuleOrder"].ToString();
                            newDR["MinModuleOrder"] = dtUserModules.Rows[i]["MinModuleOrder"].ToString();
                            newDR["Position"] = dtUserModules.Rows[i]["Position"];
                            newDR["InstanceID"] = dtUserModules.Rows[i]["InstanceID"];
                            newdtUserModules.Rows.Add(newDR);
                        }
                    }
                    ViewState["dtUserModules"] = newdtUserModules;
                    gdvUserModules.DataSource = newdtUserModules;
                    gdvUserModules.DataBind();
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ControlPanel", "ModuleDeletedSuccessfully"), "", SageMessageType.Success);
                }
                else if (e.CommandName == "DeleteUserModule")
                {
                    int userModuleId = int.Parse(e.CommandArgument.ToString());
                    //:TODO: Delete the usermoduleID with this usermoduleID
                    dbPages.sp_UserModulesDelete(userModuleId, GetPortalID, GetUsername);
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ControlPanel", "ModuleAgainDeletedSuccessfully"), "", SageMessageType.Success);
                    BindGridAfterOrder(Int32.Parse(hdnPageID.Value), false);

                }
                else if (e.CommandName == "Up")
                {
                    int userModuleId = int.Parse(e.CommandArgument.ToString());
                    var moduleSort = dbPages.sp_PageModulesSortOrderUpdate(userModuleId, true);
                    BindGridAfterOrder(Int32.Parse(hdnPageID.Value), false);
                   
                }
                else if (e.CommandName == "Down")
                {
                    int userModuleId = int.Parse(e.CommandArgument.ToString());
                    var moduleSort = dbPages.sp_PageModulesSortOrderUpdate(userModuleId, false);
                    BindGridAfterOrder(Int32.Parse(hdnPageID.Value), false);
                   
                }
                else if (e.CommandName == "LoadEditControl")
                {
                    string ReturnUrl = string.Empty;
                    string[] commandArgsAccept = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Int32 ModuleID = Int32.Parse(commandArgsAccept[0].ToString());
                    string userModuleID = commandArgsAccept[1].ToString();
                    string userModuleName = commandArgsAccept[2].ToString();
                    userModuleName = userModuleName.Replace(" ", "_");
                    var moduleControls = dbModules.sp_ModuleControlsGetModuleDefinitionsAndControlTypeByModuleID(ModuleID, GetPortalID, (int)SageFrameEnums.ControlType.Edit).SingleOrDefault();
                    string ControlPath = "/" + moduleControls.ControlSrc;
                    SageUserModuleID = commandArgsAccept[1].ToString();
                    string[] arrUrl;
                    string strURL = string.Empty;
                    arrUrl = Request.RawUrl.Split('?');
                    if (!IsUseFriendlyUrls)
                    {
                        string[] keys = Request.QueryString.AllKeys;
                        for (int i = 0; i < Request.QueryString.Count; i++)
                        {
                            string[] values = Request.QueryString.GetValues(i);
                            strURL += keys[i] + '=' + values[0] + '&';
                        }
                        if (strURL.Length > 0)
                        {
                            strURL = strURL.Remove(strURL.LastIndexOf('&'));
                        }
                        ReturnUrl = arrUrl[0]; //+ '?' + strURL;
                        strURL = "~/Sagin/ManagePage.aspx?" + strURL + "&umid=" + commandArgsAccept[1].ToString() + "&ManageReturnUrl=" + ReturnUrl + "&ActInd=" + TabContainerManagePages.ActiveTabIndex.ToString() + "&CtlType=2&pgid=" + hdnPageID.Value;
                        HttpContext.Current.Response.Redirect(strURL, false);
                    }
                    else
                    {
                        string redir = string.Empty;
                        redir = Request.RawUrl.ToString();
                        HttpContext.Current.Session["ReturnUrl"] = redir;
                        string redirectPath = string.Empty;
                        if (GetPortalID > 1)
                        {
                            redirectPath = "~/portal/" + GetPortalSEOName + "/" + commandArgsAccept[1].ToString() + "/Sagin/Edit/";
                        }
                        else
                        {
                            redirectPath = "~/" + commandArgsAccept[1].ToString() + "/Sagin/Edit/";
                        }
                        strURL = redirectPath + userModuleName + ".aspx?ManageReturnUrl=" + redir + "&ActInd=" + TabContainerManagePages.ActiveTabIndex.ToString() + "&pgid=" + hdnPageID.Value; ;
                        HttpContext.Current.Response.Redirect(strURL, false);
                    }

                }
                else if (e.CommandName == "LoadSettingsControl")
                {
                    string ReturnUrl = string.Empty;
                    string[] commandArgsAccept = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Int32 ModuleID = Int32.Parse(commandArgsAccept[0].ToString());
                    string userModuleID = commandArgsAccept[1].ToString();
                    string userModuleName = commandArgsAccept[2].ToString();
                    userModuleName = userModuleName.Replace(" ", "_");
                    var moduleControls = dbModules.sp_ModuleControlsGetModuleDefinitionsAndControlTypeByModuleID(ModuleID, GetPortalID, (int)SageFrameEnums.ControlType.Setting).SingleOrDefault();
                    string ControlPath = "/" + moduleControls.ControlSrc;
                    string[] arrUrl;
                    arrUrl = Request.RawUrl.Split('?');
                    string strURL = string.Empty;
                    if (!IsUseFriendlyUrls)
                    {
                        string[] keys = Request.QueryString.AllKeys;
                        for (int i = 0; i < Request.QueryString.Count; i++)
                        {
                            string[] values = Request.QueryString.GetValues(i);
                            strURL += keys[i] + '=' + values[0] + '&';
                        }
                        if (strURL.Length > 0)
                        {
                            strURL = strURL.Remove(strURL.LastIndexOf('&'));
                        }
                        ReturnUrl = arrUrl[0];// +'?' + strURL;
                        strURL = "~/Sagin/ManagePage.aspx?" + strURL + "&umid=" + commandArgsAccept[1].ToString() + "&ManageReturnUrl=" + ReturnUrl + "&ActInd=" + TabContainerManagePages.ActiveTabIndex.ToString() + "&CtlType=3&pgid=" + hdnPageID.Value;
                        HttpContext.Current.Response.Redirect(strURL, false);
                    }
                    else
                    {
                        string redir = string.Empty;
                        redir = Request.RawUrl.ToString();
                        string redirectPath = string.Empty;
                        if (GetPortalID > 1)
                        {
                            redirectPath = "~/portal/" + GetPortalSEOName + "/" + commandArgsAccept[1].ToString() + "/Sagin/Setting/";
                        }
                        else
                        {
                            redirectPath = "~/" + commandArgsAccept[1].ToString() + "/Sagin/Setting/";
                        }
                        strURL = redirectPath + userModuleName + ".aspx?ManageReturnUrl=" + redir + "&ActInd=" + TabContainerManagePages.ActiveTabIndex.ToString() + "&pgid=" + hdnPageID.Value;
                        HttpContext.Current.Response.Redirect(strURL, false);
                    }

                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private int GetPageIdFromGrid()
        {
            HiddenField hdfPageID = (HiddenField)gdvUserModules.Rows[0].FindControl("hdfPageID");
            int pageId = int.Parse(hdfPageID.Value.ToString());
            return pageId;
        }

        public void BindGridAfterOrder(int pageId, bool isSaving)
        {
            //:TODO: bind the iconbar user control with given pageID 
            try
            {
                List<sp_GetUserModulesByPageIDResult> AllUserModules = dbPages.sp_GetUserModulesByPageID(pageId, GetPortalID).ToList();
                //To bind the user name for VIEW permission
                ModulesBindDataTableUserModules(AllUserModules, isSaving);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

        }

        public void ModulesBindDataTableUserModules(List<sp_GetUserModulesByPageIDResult> AllUserModules, bool isSaving)
        {
            DataTable dtUserModules = new DataTable();
            dtUserModules.Columns.Add("SNo");
            dtUserModules.Columns.Add("PageID");
            dtUserModules.Columns.Add("UserModuleID");
            dtUserModules.Columns.Add("ModuleID");
            dtUserModules.Columns.Add("Title");
            dtUserModules.Columns.Add("Visibility");
            dtUserModules.Columns.Add("InheritView");
            dtUserModules.Columns.Add("PaneName");
            dtUserModules.Columns.Add("AllPages");
            dtUserModules.Columns.Add("IsActive");
            dtUserModules.Columns.Add("ModuleOrder");
            dtUserModules.Columns.Add("MaxModuleOrder");
            dtUserModules.Columns.Add("MinModuleOrder");
            dtUserModules.Columns.Add("Position");
            dtUserModules.Columns.Add("InstanceID");
            dtUserModules.AcceptChanges();
            for (int index = 0; index <= AllUserModules.Count - 1; index++)
            {
                sp_GetUserModulesByPageIDResult UserModules = AllUserModules[index];
                DataRow dr = dtUserModules.NewRow();
                dr["SNo"] = 0;
                dr["PageID"] = UserModules.PageID;
                dr["UserModuleID"] = UserModules.UserModuleID;
                dr["ModuleID"] = UserModules.ModuleID;
                dr["Title"] = UserModules.Title;
                dr["Visibility"] = UserModules.Visibility;
                dr["InheritView"] = UserModules.InheritView;
                dr["PaneName"] = UserModules.PaneName;
                dr["AllPages"] = UserModules.AllPages;
                dr["IsActive"] = UserModules.IsActive;
                dr["ModuleOrder"] = UserModules.ModuleOrder;
                dr["MaxModuleOrder"] = UserModules.MaxModuleOrder;
                dr["MinModuleOrder"] = UserModules.MinModuleOrder;
                dr["Position"] = "0";
                dr["InstanceID"] = "0";
                dtUserModules.Rows.Add(dr);
            }
            if (ViewState["dtUserModules"] == null)
            {
                ViewState["dtUserModules"] = dtUserModules;
            }
            else
            {
                DataTable dtTemp = new DataTable();
                dtTemp = (DataTable)ViewState["dtUserModules"];
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    if (drTemp["PageID"].ToString() == "0")
                    {
                        DataRow dr = dtUserModules.NewRow();
                        dr["SNo"] = 0;
                        dr["PageID"] = drTemp["PageID"];
                        dr["UserModuleID"] = drTemp["UserModuleID"];
                        dr["ModuleID"] = drTemp["ModuleID"];
                        dr["Title"] = drTemp["Title"];
                        dr["Visibility"] = drTemp["Visibility"];
                        dr["InheritView"] = drTemp["InheritView"];
                        dr["PaneName"] = drTemp["PaneName"];
                        dr["AllPages"] = drTemp["AllPages"];
                        dr["IsActive"] = drTemp["IsActive"];
                        dr["ModuleOrder"] = drTemp["ModuleOrder"];
                        dr["MaxModuleOrder"] = drTemp["MaxModuleOrder"];
                        dr["MinModuleOrder"] = drTemp["MinModuleOrder"];
                        dr["Position"] = drTemp["Position"];
                        dr["InstanceID"] = drTemp["InstanceID"];
                        dtUserModules.Rows.Add(dr);
                    }
                }
                ViewState["dtUserModules"] = dtUserModules;
            }
            BindUserModuleGrid();
        }

        private void BindUserModuleGrid()
        {
            DataTable dtUserModules;
            if (ViewState["dtUserModules"] == null)
            {
                dtUserModules = new DataTable();
                dtUserModules.Columns.Add("SNo");
                dtUserModules.Columns.Add("UserModuleID");
                dtUserModules.Columns.Add("PageID");
                dtUserModules.Columns.Add("ModuleID");
                dtUserModules.Columns.Add("Title");
                dtUserModules.Columns.Add("Visibility");
                dtUserModules.Columns.Add("InheritView");
                dtUserModules.Columns.Add("PaneName");
                dtUserModules.Columns.Add("AllPages");
                dtUserModules.Columns.Add("IsActive");
                dtUserModules.Columns.Add("ModuleOrder");
                dtUserModules.Columns.Add("MaxModuleOrder");
                dtUserModules.Columns.Add("MinModuleOrder");
                dtUserModules.Columns.Add("Position");
                dtUserModules.Columns.Add("InstanceID");
                dtUserModules.AcceptChanges();
                ViewState["dtUserModules"] = dtUserModules;
                gdvUserModules.DataSource = dtUserModules;
            }
            else
            {
                gdvUserModules.DataSource = (DataTable)ViewState["dtUserModules"];

            }
            gdvUserModules.DataBind();
        }

        protected void gdvUserModules_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imbDeleteUsermodule");
                imgDelete.Attributes.Add("onclick", "return confirm('" + GetSageMessage("PAGES", "AreYouSureToDelete") + "')");
                ImageButton imgDelete2 = (ImageButton)e.Row.FindControl("imbDelete3");
                imgDelete2.Attributes.Add("onclick", "return confirm('" + GetSageMessage("PAGES", "AreYouSureToDelete") + "')");

                HiddenField hdnIsActive = (HiddenField)e.Row.FindControl("hdnIsActive");
                HtmlInputCheckBox chkIsActiveItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveItem");
                chkIsActiveItem.Checked = bool.Parse(hdnIsActive.Value);
                HiddenField hdnAllPages = (HiddenField)e.Row.FindControl("hdnAllPages");
                HtmlInputCheckBox chkBoxAllPagesItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxAllPagesItem");
                chkBoxAllPagesItem.Checked = bool.Parse(hdnAllPages.Value);
                Label lblPageID = (Label)e.Row.FindControl("lblPageID");
                Label lblMax = (Label)e.Row.FindControl("lblMax");
                Label lblMin = (Label)e.Row.FindControl("lblMin");
                Label lblModuleOrder = (Label)e.Row.FindControl("lblModuleOrder");
                ImageButton imbEditControl = (ImageButton)e.Row.FindControl("imbEditControl");
                ImageButton imbSettingsControl = (ImageButton)e.Row.FindControl("imbSettingsControl");
                Label lblModuleID = (Label)e.Row.FindControl("lblModuleID");
                Int32 ModuleID = Int32.Parse(lblModuleID.Text);
                var ModuleControlEditSettingCount = dbModules.sp_CheckEditSettingControlByModuleID(ModuleID, GetPortalID, GetUsername).SingleOrDefault();
                if (ModuleControlEditSettingCount.EditControlCount > 0)
                    e.Row.FindControl("imbEditControl").Visible = true;
                else
                    e.Row.FindControl("imbEditControl").Visible = false;

                if (ModuleControlEditSettingCount.SettingControlCount > 0)
                    e.Row.FindControl("imbSettingsControl").Visible = true;
                else
                    e.Row.FindControl("imbSettingsControl").Visible = false;

                if (lblPageID.Text == "0")
                {
                    e.Row.FindControl("imbDeleteUsermodule").Visible = false;
                    e.Row.FindControl("imgDown").Visible = false;
                    e.Row.FindControl("imgUp").Visible = false;
                    e.Row.FindControl("imbEdit").Visible = false;
                    e.Row.FindControl("imbEditControl").Visible = false;
                    e.Row.FindControl("imbSettingsControl").Visible = false;
                    e.Row.FindControl("imbEditUserModulePermission").Visible = false;
                }
                else
                {
                    e.Row.FindControl("imbDelete3").Visible = false;
                    e.Row.FindControl("imbEditUserModulePermission").Visible = true;
                }
                if (lblMax.Text == lblModuleOrder.Text && Int32.Parse(hdnPageID.Value) != 0)
                {
                    e.Row.FindControl("imgDown").Visible = false;
                }
                if (lblMin.Text == lblModuleOrder.Text && Int32.Parse(hdnPageID.Value) != 0)
                {
                    e.Row.FindControl("imgUp").Visible = false;
                }

                
            }

            else if (e.Row.RowType == DataControlRowType.Header)
            {
                HtmlInputCheckBox chkIsActiveHeader = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveHeader");
                chkIsActiveHeader.Attributes.Add("onclick", "javascript:SelectAllCheckboxesSpecific(this,'" + gdvUserModules.ClientID + "','cssCheckBoxIsActiveItem');");
            }
        }

        protected void gdvUserModules_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gdvUserModules.EditIndex = e.NewEditIndex;
            gdvUserModules.DataSource = (DataTable)ViewState["dtUserModules"];
            gdvUserModules.DataBind();
            Label lblVisibility1 = (Label)gdvUserModules.Rows[gdvUserModules.EditIndex].FindControl("lblVisibility1");
            DropDownList ddlVisibility = (DropDownList)gdvUserModules.Rows[e.NewEditIndex].FindControl("ddlVisibility");
            ddlVisibility.DataValueField = "Visibility";
            ddlVisibility.Items.Insert(0, new ListItem("Same As Page", "true"));
            ddlVisibility.Items.Insert(0, new ListItem("Page Editor Only", "false"));
            ddlVisibility.DataBind();
            ddlVisibility.SelectedIndex = ddlVisibility.Items.IndexOf(ddlVisibility.Items.FindByValue(lblVisibility1.Text.ToLower()));
            DropDownList ddlPane = (DropDownList)gdvUserModules.Rows[e.NewEditIndex].FindControl("ddlPaneName");
            Label lblPaneName = (Label)gdvUserModules.Rows[gdvUserModules.EditIndex].FindControl("lblPaneName1");
            ddlPane.DataSource = SageFrameLists.ModulePanes();
            ddlPane.DataTextField = "Value";
            ddlPane.DataValueField = "Key";
            ddlPane.DataBind();
            ddlPane.SelectedIndex = ddlPane.Items.IndexOf(ddlPane.Items.FindByValue(lblPaneName.Text));
        }

        public string ConvetVisibility(bool i)
        {
            string Visible = "Same As Page";
            if (i == false)
            {
                Visible = "Page Editor Only";
            }
            return Visible;
        }

        protected void gdvUserModules_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gdvUserModules.EditIndex = -1;
            gdvUserModules.DataSource = (DataTable)ViewState["dtUserModules"];
            gdvUserModules.DataBind();
        }

        protected void gdvUserModules_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gdvUserModules.Rows[e.RowIndex];
            Label lblUSerModuleId = (Label)row.FindControl("lblUserModuleID");
            Label lblPageId = (Label)row.FindControl("lblPageID");
            int userModuleID = int.Parse(lblUSerModuleId.Text);
            TextBox txtModuleTitle = (TextBox)row.FindControl("txtModuleTitle");
            DropDownList ddlPaneName = (DropDownList)row.FindControl("ddlPaneName");
            HtmlInputCheckBox chkBoxAllPagesItem = (HtmlInputCheckBox)row.FindControl("chkBoxAllPagesItem");
            HtmlInputCheckBox chkBoxIsActiveItem = (HtmlInputCheckBox)row.FindControl("chkBoxIsActiveItem");
            DropDownList ddlVisibility = (DropDownList)row.FindControl("ddlVisibility");
            Label lblPaneName = (Label)row.FindControl("lblPaneName1");
            string title = txtModuleTitle.Text;
            string paneName = ddlPaneName.SelectedValue;
            string visibility = ddlVisibility.SelectedValue;
            int pageId = int.Parse(lblPageId.Text.ToString());
            bool inheritView = Convert.ToBoolean(visibility);
            bool isPaneChanged = false;
            if (lblPaneName.Text != paneName)
            {
                isPaneChanged = true;
            }
            try
            {
                var updateResult = dbPages.sp_UserModuleUpdate(userModuleID, pageId, GetPortalID, title, inheritView, paneName, GetUsername, chkBoxAllPagesItem.Checked, chkBoxIsActiveItem.Checked, lblPaneName.Text, isPaneChanged);
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ControlPanel", "ModuleUpdatedSuccessfully"), "", SageMessageType.Success);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            gdvUserModules.EditIndex = -1;
            BindGridAfterOrder(Int32.Parse(hdnPageID.Value), false);

        }

        protected void imbAddModules_Click(object sender, ImageClickEventArgs e)
        {
            PageModulePanelVisibility(false, true, false);
            SetDefaultModule();
            cboPanes.SelectedIndex = cboPanes.Items.IndexOf(cboPanes.Items.FindByValue(SystemSetting.glbDefaultPane));
            LoadPositions();
            DisplayInstances();
            txtModuleTitle.Text = "";

        }

        protected void ibModuleControlCancel_Click(object sender, ImageClickEventArgs e)
        {
            //PageModulePanelVisibility(false, true);
            PageModulePanelVisibility(true, false, false);
        }

        protected void imbCancelEdit_Click(object sender, EventArgs e)
        {
            //mpePagePermissionEdit.Hide();
            PopupPanelVisibility(pnlUsersEdit, false);
        }

        protected void imbCancelView_Click(object sender, EventArgs e)
        {
            //mpePagePermissionView.Hide();
            PopupPanelVisibility(pnlUsersView, false);
        }

        protected void imbSelectUsersModule_Click(object sender, ImageClickEventArgs e)
        {
            //txtViewSearchTextModule.Value = "";
            BindUsers(txtViewSearchTextModule.Value.Trim(), ddlSearchRoleModule.SelectedValue.ToString(), gdvUserModule);
            //mpePagePermissionViewModule.Show();
            PopupPanelVisibility(pnlUsersViewModule, true);
        }

        protected void imbSelectEditUsersModule_Click(object sender, ImageClickEventArgs e)
        {
            //txtEditSearchTextModule.Value = "";
            BindUsers(txtEditSearchTextModule.Value.Trim(), ddlSearchEditRoleModule.SelectedValue.ToString(), gdvEditUserModule);
            //mpePagePermissionEditModule.Show();
            PopupPanelVisibility(pnlUsersEditModule, true);
        }

        protected void imbSelectUsers_Click(object sender, ImageClickEventArgs e)
        {
            //txtSearchText.Value = "";
            BindUsers(txtSearchText.Value.Trim(), ddlSearchRole.SelectedValue.ToString(), gdvUser);
            PopupPanelVisibility(pnlUsersView, true);
        }

        protected void imbSelectEditUsers_Click(object sender, ImageClickEventArgs e)
        {
            //txtSearchUserText.Value = "";
            BindUsers(txtSearchUserText.Value.Trim(), ddlSearchEditRole.SelectedValue.ToString(), gdvEditUser);
            //mpePagePermissionEdit.Show();
            PopupPanelVisibility(pnlUsersEdit, true);
        }

        //private void ConfigSearchButton()
        //{
        //    imgSearchUsersViewModule.Attributes.Add("click", "SetSearchValue('" + txtViewSearchTextModule.ClientID + "','" + hdnViewSearchTextModule.ClientID + "')");
        //    imgUserSearchModule.Attributes.Add("click", "SetSearchValue('" + txtEditSearchTextModule.ClientID + "','" + hdnEditSearchTextModule.ClientID + "')");
        //    imgSearch.Attributes.Add("click", "SetSearchValue('" + txtSearchText.ClientID + "','" + hdnSearchText.ClientID + "')");
        //    imgUserSearch.Attributes.Add("click", "SetSearchValue('" + txtSearchUserText.ClientID + "','" + hdnSearchUserText.ClientID + "')");
        //}

        #region UserModule Permission
        protected void btnAddAllRoleViewModule_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in lstUnselectedViewRolesModule.Items)
            {
                lstSelectedRolesViewModule.Items.Add(li);
            }
            lstUnselectedViewRolesModule.Items.Clear();
        }
        protected void btnAddRoleViewModule_Click(object sender, EventArgs e)
        {
            if (lstUnselectedViewRolesModule.SelectedIndex != -1)
            {
                int[] selectedIndexs = lstUnselectedViewRolesModule.GetSelectedIndices();
                for (int i = selectedIndexs.Length - 1; i >= 0; i--)
                {
                    lstSelectedRolesViewModule.Items.Add(lstUnselectedViewRolesModule.Items[selectedIndexs[i]]);
                    lstUnselectedViewRolesModule.Items.Remove(lstUnselectedViewRolesModule.Items[selectedIndexs[i]]);
                }
                lstUnselectedViewRolesModule.SelectedIndex = -1;
            }
        }
        protected void btnRemoveRoleViewModule_Click(object sender, EventArgs e)
        {
            if (lstSelectedRolesViewModule.SelectedIndex != -1)
            {
                int[] selectedIndexs = lstSelectedRolesViewModule.GetSelectedIndices();
                for (int i = selectedIndexs.Length - 1; i >= 0; i--)
                {
                    if (!SystemSetting.SYSTEM_SUPER_ROLES.Contains(lstSelectedRolesViewModule.Items[selectedIndexs[i]].Text, StringComparer.OrdinalIgnoreCase))
                    {
                        lstUnselectedViewRolesModule.Items.Add(lstSelectedRolesViewModule.Items[selectedIndexs[i]]);
                        lstSelectedRolesViewModule.Items.Remove(lstSelectedRolesViewModule.Items[selectedIndexs[i]]);
                    }
                }
                lstSelectedRolesViewModule.SelectedIndex = -1;
            }
        }
        protected void btnRemoveAllRoleViewModule_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in lstSelectedRolesViewModule.Items)
            {
                if (!SystemSetting.SYSTEM_SUPER_ROLES.Contains(li.Text, StringComparer.OrdinalIgnoreCase))
                {
                    lstUnselectedViewRolesModule.Items.Add(li);
                }
            }
            lstSelectedRolesViewModule.Items.Clear();
            BindSelectedRolesInListBox(lstSelectedRolesViewModule);
        }

        protected void imbAddUserModule_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtViewUsernameModule.Text != "")
                {
                    addUsernameModule(txtViewUsernameModule.Text, "view");
                    //txtViewUsernameModule.Text = "";
                    PopupPanelVisibility(pnlUsersViewModule, false);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvViewUsernamesModule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imbDeleteModule");
                    imgDelete.Attributes.Add("onclick", "return confirm('" + GetSageMessage("PAGES", "AreYouSureToDelete") + "')");
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvViewUsernamesModule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveUser")
                {
                    DataTable dtViewUsers = (DataTable)ViewState["dtViewUsersModules"];
                    DataTable newdtEditUsers = dtViewUsers.Clone();
                    for (int i = 0; i < dtViewUsers.Rows.Count; i++)
                    {
                        if (e.CommandArgument.ToString() != dtViewUsers.Rows[i]["Username"].ToString())
                        {
                            DataRow newDR = newdtEditUsers.NewRow();
                            newDR["Username"] = dtViewUsers.Rows[i]["Username"].ToString();
                            newdtEditUsers.Rows.Add(newDR);
                        }
                    }
                    ViewState["dtViewUsersModules"] = newdtEditUsers;
                    gdvViewUsernamesModule.DataSource = newdtEditUsers;
                    gdvViewUsernamesModule.DataBind();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void ddlSearchRoleModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindUsers(txtViewSearchTextModule.Value.Trim(), ddlSearchRoleModule.SelectedValue.ToString(), gdvUserModule);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void imgAddSelectedEditUsersModule_Click(object sender, EventArgs e)
        {
            try
            {
                BindUsers(txtEditSearchTextModule.Value.Trim(), ddlSearchRoleModule.SelectedValue.ToString(), gdvUserModule);
                //mpePagePermissionEditModule.Show();
                PopupPanelVisibility(pnlUsersEditModule, true);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void gdvUserModule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlInputCheckBox chkItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxItem");
                chkItem.Attributes.Add("onclick", "javascript:Check(this,'cssCheckBoxHeader','" + gdvUserModule.ClientID + "','cssCheckBoxItem');");
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                HtmlInputCheckBox chkHeader = (HtmlInputCheckBox)e.Row.FindControl("chkBoxHeader");
                chkHeader.Attributes.Add("onclick", "javascript:SelectAllCheckboxesSpecific(this,'" + gdvUserModule.ClientID + "','cssCheckBoxItem');");
            }
        }
        protected void imgAddSelectedUsersModule_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string seletedUsername = string.Empty;
                for (int i = 0; i < gdvUserModule.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem = (HtmlInputCheckBox)gdvUserModule.Rows[i].FindControl("chkBoxItem");
                    if (chkBoxItem.Checked == true)
                    {
                        Label lblUsernameD = (Label)gdvUserModule.Rows[i].FindControl("lblUsernameD");
                        seletedUsername = lblUsernameD.Text.Trim();
                        if (seletedUsername.Length > 1)
                        {
                            addUsernameModule(seletedUsername, "view");
                        }
                    }
                }
                PopupPanelVisibility(pnlUsersViewModule, false);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void btnAddAllRoleEditModule_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in lstUnselectedEditRolesModule.Items)
            {
                lstSelectedRolesEditModule.Items.Add(li);
            }
            lstUnselectedEditRolesModule.Items.Clear();
        }
        protected void btnAddRoleEditModule_Click(object sender, EventArgs e)
        {
            if (lstUnselectedEditRolesModule.SelectedIndex != -1)
            {
                int[] selectedIndexs = lstUnselectedEditRolesModule.GetSelectedIndices();
                for (int i = selectedIndexs.Length - 1; i >= 0; i--)
                {
                    lstSelectedRolesEditModule.Items.Add(lstUnselectedEditRolesModule.Items[selectedIndexs[i]]);
                    lstUnselectedEditRolesModule.Items.Remove(lstUnselectedEditRolesModule.Items[selectedIndexs[i]]);
                }
                lstUnselectedEditRolesModule.SelectedIndex = -1;
            }
        }
        protected void btnRemoveRoleEditModule_Click(object sender, EventArgs e)
        {
            if (lstSelectedRolesEditModule.SelectedIndex != -1)
            {
                int[] selectedIndexs = lstSelectedRolesEditModule.GetSelectedIndices();
                for (int i = selectedIndexs.Length - 1; i >= 0; i--)
                {
                    if (!SystemSetting.SYSTEM_SUPER_ROLES.Contains(lstSelectedRolesEditModule.Items[selectedIndexs[i]].Text, StringComparer.OrdinalIgnoreCase))
                    {
                        lstUnselectedEditRolesModule.Items.Add(lstSelectedRolesEditModule.Items[selectedIndexs[i]]);
                        lstSelectedRolesEditModule.Items.Remove(lstSelectedRolesEditModule.Items[selectedIndexs[i]]);
                    }
                }
                lstSelectedRolesEditModule.SelectedIndex = -1;
            }
        }
        protected void btnRemoveAllRoleEditModule_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in lstSelectedRolesEditModule.Items)
            {
                if (!SystemSetting.SYSTEM_SUPER_ROLES.Contains(li.Text, StringComparer.OrdinalIgnoreCase))
                {
                    lstUnselectedEditRolesModule.Items.Add(li);
                }
            }
            lstSelectedRolesEditModule.Items.Clear();
            BindSelectedRolesInListBox(lstSelectedRolesEditModule);
        }
        protected void imbAddUserEditPermissionModule_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtEditUsernameModule.Text != "")
                {
                    addUsernameModule(txtEditUsernameModule.Text, "edit");
                    txtEditUsernameModule.Text = "";
                    //mpePagePermissionEditModule.Hide();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void ddlSearchEditRoleModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindUsers(txtEditSearchTextModule.Value.Trim(), ddlSearchEditRoleModule.SelectedValue.ToString(), gdvEditUserModule);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void imgUserSearchModule_Click(object sender, EventArgs e)
        {
            try
            {
                BindUsers(txtEditSearchTextModule.Value.Trim(), ddlSearchEditRoleModule.SelectedValue.ToString(), gdvEditUserModule);
                //mpePagePermissionEditModule.Show();
                PopupPanelVisibility(pnlUsersEditModule, true);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void gdvEditUserModule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlInputCheckBox chkItem2 = (HtmlInputCheckBox)e.Row.FindControl("chkBoxItem2");
                chkItem2.Attributes.Add("onclick", "javascript:Check(this,'cssCheckBoxHeader','" + gdvEditUserModule.ClientID + "','cssCheckBoxItem');");
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                HtmlInputCheckBox chkHeader2 = (HtmlInputCheckBox)e.Row.FindControl("chkBoxHeader2");
                chkHeader2.Attributes.Add("onclick", "javascript:SelectAllCheckboxesSpecific(this,'" + gdvEditUserModule.ClientID + "','cssCheckBoxItem');");
            }
        }

        protected void imgSearchUsersViewModule_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BindUsers(txtViewSearchTextModule.Value.Trim(), ddlSearchRoleModule.SelectedValue, gdvUserModule);
                //mpePagePermissionViewModule.Show();
                PopupPanelVisibility(pnlUsersViewModule, true);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void imgAddSelectedEditUsersModule_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string seletedUsername = string.Empty;
                for (int i = 0; i < gdvEditUserModule.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem2 = (HtmlInputCheckBox)gdvEditUserModule.Rows[i].FindControl("chkBoxItem2");
                    if (chkBoxItem2.Checked == true)
                    {
                        Label lblEditUsernameD = (Label)gdvEditUserModule.Rows[i].FindControl("lblEditUsernameD");
                        seletedUsername = lblEditUsernameD.Text.Trim();
                        if (seletedUsername.Length > 1)
                        {
                            addUsernameModule(seletedUsername, "edit");
                        }
                    }
                }
                //mpePagePermissionEdit.Hide();
                PopupPanelVisibility(pnlUsersEdit, false);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void imbAddUserViewPermissionModule_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtViewUsernameModule.Text != "")
                {
                    addUsernameModule(txtViewUsernameModule.Text, "view");
                    txtViewUsernameModule.Text = "";
                    //mpePagePermissionViewModule.Hide();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private void addUsernameModule(string userName, string permissionType)
        {
            try
            {
                MembershipUserCollection users = Membership.FindUsersByName(userName);
                if (users.Count > 0)
                {
                    bool isUsernameExists = false;
                    DataTable dtBackupUsername = new DataTable();
                    if (permissionType.ToLower() == "view")
                    {
                        if (ViewState["dtViewUsersModules"] != null)
                        {
                            dtBackupUsername = (DataTable)ViewState["dtViewUsersModules"];
                        }
                    }
                    else if (permissionType.ToLower() == "edit")
                    {
                        if (ViewState["dtEditUsersModules"] != null)
                        {
                            dtBackupUsername = (DataTable)ViewState["dtEditUsersModules"];
                        }
                    }

                    for (int i = 0; i < dtBackupUsername.Rows.Count; i++)
                    {
                        if (dtBackupUsername.Rows[i]["Username"].ToString() == userName)
                        {
                            isUsernameExists = true;
                        }
                    }
                    if (!isUsernameExists)
                    {
                        if (dtBackupUsername.Columns.Count == 0)
                        {
                            dtBackupUsername.Columns.Add("Username");
                        }
                        dtBackupUsername = BindDataTable(userName, dtBackupUsername);
                        if (permissionType.ToLower() == "view")
                        {
                            ViewState["dtViewUsersModules"] = dtBackupUsername;
                            BindUsernameGridModule("view");
                        }
                        else if (permissionType.ToLower() == "edit")
                        {
                            ViewState["dtEditUsersModules"] = dtBackupUsername;
                            BindUsernameGridModule("edit");
                        }
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("PAGES", "UsernameAlreadyExist"), "", SageMessageType.Alert);
                    }
                }
                else
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("PAGES", "UsernameDoesnotExist"), "", SageMessageType.Alert);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private void BindUsernameGridModule(string PermissionType)
        {
            if (PermissionType.ToLower() == "view")
            {
                if (ViewState["dtViewUsersModules"] != null)
                {
                    gdvViewUsernamesModule.DataSource = (DataTable)ViewState["dtViewUsersModules"];
                    gdvViewUsernamesModule.DataBind();
                }

            }
            else if (PermissionType.ToLower() == "edit")
            {
                if (ViewState["dtEditUsersModules"] != null)
                {
                    gdvEditUsernamesModule.DataSource = (DataTable)ViewState["dtEditUsersModules"];
                    gdvEditUsernamesModule.DataBind();
                }
            }
        }

        protected void gdvEditUsernames_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imbDelete2");
                    imgDelete.Attributes.Add("onclick", "return confirm('" + GetSageMessage("PAGES", "AreYouSureToDelete") + "')");
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvEditUsernamesModule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imbDelete2Module");
                    imgDelete.Attributes.Add("onclick", "return confirm('" + GetSageMessage("PAGES", "AreYouSureToDelete") + "')");
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvEditUsernamesModule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveUser")
                {
                    DataTable dtEditUsers = (DataTable)ViewState["dtEditUsersModules"];
                    DataTable newdtEditUsers = dtEditUsers.Clone();
                    for (int i = 0; i < dtEditUsers.Rows.Count; i++)
                    {
                        if (e.CommandArgument.ToString() != dtEditUsers.Rows[i]["Username"].ToString())
                        {
                            DataRow newDR = newdtEditUsers.NewRow();
                            newDR["Username"] = dtEditUsers.Rows[i]["Username"].ToString();
                            newdtEditUsers.Rows.Add(newDR);
                        }
                    }
                    ViewState["dtEditUsersModules"] = newdtEditUsers;
                    gdvEditUsernamesModule.DataSource = newdtEditUsers;
                    gdvEditUsernamesModule.DataBind();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private void BindModuleData(int moduleID)
        {
            UserModulesDataContext dbUserModule = new UserModulesDataContext(SystemSetting.SageFrameConnectionString);
            var userModule = dbUserModule.sp_GetUserModulePermissionsByUserModuleID(moduleID, GetPortalID, GetUsername).SingleOrDefault();

            //To bind the Page Modules Permissions
            string[] arrPermittedRoles = userModule.RoleIDs.Split(',');
            string[] arrPermittedUsers = userModule.UserNames.Split(',');
            DataTable dtViewUsers = new DataTable();
            DataTable dtEditUsers = new DataTable();
            if (ViewState["dtViewUsersModules"] != null)
                dtViewUsers = (DataTable)ViewState["dtViewUsersModules"];
            if (ViewState["dtEditUsersModules"] != null)
                dtEditUsers = (DataTable)ViewState["dtEditUsersModules"];
            for (int i = 0; i < arrPermittedUsers.Length; i++)
            {
                string[] arrPermission = arrPermittedUsers[i].Split('#');
                if (arrPermission[0] == "1")
                {
                    DataRow newDR = dtViewUsers.NewRow();
                    newDR["Username"] = arrPermission[1];
                    dtViewUsers.Rows.Add(newDR);
                }
                else if (arrPermission[0] == "2")
                {
                    DataRow newDR = dtEditUsers.NewRow();
                    newDR["Username"] = arrPermission[1];
                    dtEditUsers.Rows.Add(newDR);
                }
            }
            ViewState["dtViewUsersModules"] = dtViewUsers;
            ViewState["dtEditUsersModules"] = dtEditUsers;
            BindUsernameGridModule("edit");
            BindUsernameGridModule("view");
            List<string> viewRoles = new List<string>();
            List<string> editRoles = new List<string>();

            for (int j = 0; j < arrPermittedRoles.Length; j++)
            {
                string[] arrPermission = arrPermittedRoles[j].Split('#');
                if (arrPermission[0] == "1")
                {
                    viewRoles.Add(arrPermission[1]);
                }
                else if (arrPermission[0] == "2")
                {
                    editRoles.Add(arrPermission[1]);
                }
            }

            lstSelectedRolesViewModule.Items.Clear();
            lstUnselectedViewRolesModule.Items.Clear();
            lstSelectedRolesEditModule.Items.Clear();
            lstUnselectedEditRolesModule.Items.Clear();

            var roles = dbRoles.sp_PortalRoleList(GetPortalID, true, GetUsername);
            foreach (var role in roles)
            {
                string roleName = role.RoleName;
                string roleID = role.RoleID.ToString();
                if (CheckPageModulePermissionHasRole(role.RoleName, viewRoles))
                {
                    if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                    {
                        lstSelectedRolesViewModule.Items.Add(new ListItem(roleName, roleID));
                    }
                    else
                    {
                        string rolePrefix = GetPortalSEOName + "_";
                        roleName = roleName.Replace(rolePrefix, "");
                        lstSelectedRolesViewModule.Items.Add(new ListItem(roleName, roleID));
                    }
                }
                else
                {
                    if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                    {
                        lstUnselectedViewRolesModule.Items.Add(new ListItem(roleName, roleID));
                    }
                    else
                    {
                        string rolePrefix = GetPortalSEOName + "_";
                        roleName = roleName.Replace(rolePrefix, "");
                        lstUnselectedViewRolesModule.Items.Add(new ListItem(roleName, roleID));
                    }
                }
                if (CheckPageModulePermissionHasRole(role.RoleName, editRoles))
                {
                    if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                    {
                        lstSelectedRolesEditModule.Items.Add(new ListItem(roleName, roleID));
                    }
                    else
                    {
                        string rolePrefix = GetPortalSEOName + "_";
                        roleName = roleName.Replace(rolePrefix, "");
                        lstSelectedRolesEditModule.Items.Add(new ListItem(roleName, roleID));
                    }
                }
                else
                {
                    if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                    {
                        lstUnselectedEditRolesModule.Items.Add(new ListItem(roleName, roleID));
                    }
                    else
                    {
                        string rolePrefix = GetPortalSEOName + "_";
                        roleName = roleName.Replace(rolePrefix, "");
                        lstUnselectedEditRolesModule.Items.Add(new ListItem(roleName, roleID));
                    }
                }
            }
            PageModulePanelVisibility(false, false, true);

        }


        private bool CheckPageModulePermissionHasRole(string roleName, List<string> PageModulePermissionList)
        {
            bool bolPageHasRole = false;
            for (int index = 0; index <= PageModulePermissionList.Count - 1; index++)
            {
                string PageModulePermission = PageModulePermissionList[index];
                if (PageModulePermission.ToLower() == roleName.ToLower())
                {
                    bolPageHasRole = true;
                    break;
                }
            }
            return bolPageHasRole;
        }

        private void PageModulePanelVisibility(bool isModuleGridVisible, bool isAddModuleVisible, bool isModulePermissionVisible)
        {
            pnlModulePermission.Visible = isModulePermissionVisible;
            pnlModules.Visible = isAddModuleVisible;
            pnlModuleControls.Visible = isModuleGridVisible;
        }

        protected void ibSavePermission_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Int32 userModuleID = Int32.Parse(hdnUserModuleID.Value);
                if (userModuleID > 0)
                {
                    string selectedRolesView = GetListBoxText(lstSelectedRolesViewModule);
                    string selectedRolesEdit = GetListBoxText(lstSelectedRolesEditModule);
                    string EditUsers = GetModuleEditUsers();
                    string ViewUsers = GetModuleViewUsers();
                    UserModulesDataContext dbUserModule = new UserModulesDataContext(SystemSetting.SageFrameConnectionString);
                    dbUserModule.sp_UserModulePermissionSave(userModuleID, selectedRolesView, selectedRolesEdit, ViewUsers, EditUsers, GetPortalID, GetUsername);
                }
                PageModulePanelVisibility(true, false, false);
                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("PAGES", "ModulePermissionSaveSuccessfully"), "", SageMessageType.Success);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void ibCancelPermission_Click(object sender, ImageClickEventArgs e)
        {
            PageModulePanelVisibility(true, false, false);
        }

        private string GetModuleViewUsers()
        {
            string arrUsers1 = string.Empty;
            if (ViewState["dtViewUsersModules"] != null)
            {
                DataTable dtViewUsers = (DataTable)ViewState["dtViewUsersModules"];
                DataTable newdtViewUsers = dtViewUsers.Clone();
                for (int k = 0; k < dtViewUsers.Rows.Count; k++)
                {
                    arrUsers1 += dtViewUsers.Rows[k]["Username"].ToString() + ",";
                }
                if (arrUsers1 != "")
                {
                    arrUsers1 = arrUsers1.Remove(arrUsers1.LastIndexOf(","));
                }
            }
            return arrUsers1;
        }
        private void BindControlInUserModulePermission()
        {
            BindRolesInDropDown(ddlSearchRoleModule);
            BindRolesInDropDown(ddlSearchEditRoleModule);
            BindUsers(txtEditSearchTextModule.Value.Trim(), ddlSearchRoleModule.SelectedValue.ToString(), gdvEditUserModule);
            BindUsers(txtViewSearchTextModule.Value.Trim(), ddlSearchEditRoleModule.SelectedValue.ToString(), gdvUserModule);
        }
        private string GetModuleEditUsers()
        {
            string arrUsers2 = string.Empty;
            if (ViewState["dtEditUsersModules"] != null)
            {
                DataTable dtEditUsers = (DataTable)ViewState["dtEditUsersModules"];
                DataTable newdtEditUsers = dtEditUsers.Clone();
                for (int l = 0; l < dtEditUsers.Rows.Count; l++)
                {
                    arrUsers2 += dtEditUsers.Rows[l]["Username"].ToString() + ",";
                }
                if (arrUsers2 != "")
                {
                    arrUsers2 = arrUsers2.Remove(arrUsers2.LastIndexOf(","));
                }
            }
            return arrUsers2;
        }

        protected void gdvEditUserModule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvEditUserModule.PageIndex = e.NewPageIndex;
            BindUsers(txtEditSearchTextModule.Value.Trim(), ddlSearchEditRoleModule.SelectedValue, gdvEditUserModule);
        }
        protected void gdvUserModule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvUserModule.PageIndex = e.NewPageIndex;
            BindUsers(txtViewSearchTextModule.Value.Trim(), ddlSearchRoleModule.SelectedValue, gdvUserModule);
        }
        protected void gdvEditUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvEditUser.PageIndex = e.NewPageIndex;
            BindUsers(txtSearchUserText.Value.Trim(), ddlSearchEditRole.SelectedValue, gdvEditUser);
        }

        protected void gdvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvUser.PageIndex = e.NewPageIndex;
            BindUsers(txtSearchText.Value.Trim(), ddlSearchRole.SelectedValue, gdvUser);
        }

        protected void imbCancelViewModule_Click(object sender, EventArgs e)
        {
            //mpePagePermissionViewModule.Hide();
            PopupPanelVisibility(pnlUsersViewModule, false);
        }

        protected void imbCancelEditModule_Click(object sender, EventArgs e)
        {
            //mpePagePermissionEditModule.Hide();
            PopupPanelVisibility(pnlUsersEditModule, false);
        }
        #endregion

        private void PopupPanelVisibility(Panel panel, bool isVisible)
        {
            panel.Visible = isVisible;
        }
    }
}