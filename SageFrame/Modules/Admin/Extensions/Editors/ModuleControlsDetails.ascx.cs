﻿/*
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
using SageFrame.ModuleControls;
using SageFrame.SageFrameClass;
using System.IO;
using Microsoft.VisualBasic;

namespace SageFrame.Modules.Admin.Extensions.Editors
{
    public partial class ModuleControlsDetails : BaseAdministrationUserControl
    {
        ModuleControlsDataContext db = new ModuleControlsDataContext(SystemSetting.SageFrameConnectionString);
        CommonFunction LToDCon = new CommonFunction();
        System.Nullable<Int32> _newmoduleControlID = 0;
        System.Nullable<Int32> _controlCount = 0;
        string ApplicationPath = HttpContext.Current.Server.MapPath("~/");

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    AddImageUrls();
                    LoadControlType();

                    if (Request.QueryString["moduledef"] != null || Request.QueryString["modulecontrol"] != null)
                    {
                        BindControls();
                    }
                    else
                    {
                        rowModuleEdit.Visible = false;
                        rowDefinitionEdit.Visible = false;
                        rowSource.Visible = false;
                        pUpdatePane.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void AddImageUrls()
        {
            imbUpdateModlueControl.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            imbCancelModlueControl.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
        }

        private void BindControls()
        {
            try
            {
                rowSource.Visible = true;
                pUpdatePane.Visible = true;
                rowModuleEdit.Visible = true;
                rowDefinitionEdit.Visible = true;
                if (HttpContext.Current.Session["ModuleName"] != null)
                {
                    lblModuleD.Text = HttpContext.Current.Session["ModuleName"].ToString();
                }
                if (HttpContext.Current.Session["ModuleDefinitionName"] != null)
                {
                    lblDefinitionD.Text = HttpContext.Current.Session["ModuleDefinitionName"].ToString();
                }
                //}
                LoadSources(Server.MapPath("~/Modules"));
                LoadIcons(SystemSetting.glbImageFileTypes);

                if (Request.QueryString["modulecontrol"] != null)
                {
                    var LINQModuleControlsInfo = db.sp_ModuleControlsGetByModuleControlID(int.Parse(Request.QueryString["modulecontrol"])).SingleOrDefault();
                    if (LINQModuleControlsInfo.ControlSrc != null)
                    {
                        ddlSource.ClearSelection();
                        ddlSource.SelectedIndex = ddlSource.Items.IndexOf(ddlSource.Items.FindByText(LINQModuleControlsInfo.ControlSrc.ToString()));
                    }

                    if (LINQModuleControlsInfo.IconFile != null)
                    {
                        ddlIcon.SelectedIndex = ddlIcon.Items.IndexOf(ddlIcon.Items.FindByText(LINQModuleControlsInfo.IconFile.ToString()));
                    }
                    ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(LINQModuleControlsInfo.ControlType.ToString()));

                    txtKey.Text = LINQModuleControlsInfo.ControlKey;
                    txtTitle.Text = LINQModuleControlsInfo.ControlTitle;
                    txtDisplayOrder.Text = LINQModuleControlsInfo.DisplayOrder.ToString();
                    txtHelpURL.Text = LINQModuleControlsInfo.HelpUrl;
                    chkSupportsPartialRendering.Checked = (bool)LINQModuleControlsInfo.SupportsPartialRendering;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void LoadSources(string strRoot)
        {
            try
            {
                string strPath = strRoot;
                if (Directory.Exists(strPath))
                {
                    DirectoryInfo dti = new DirectoryInfo(strPath);
                    DirectoryInfo[] colldti = dti.GetDirectories();
                    foreach (DirectoryInfo dir in colldti)
                    {
                        if (dir.Name != ".svn")
                        {
                            LoadSources(dir.FullName);
                            FileInfo[] collFile = dir.GetFiles("*.ascx");
                            foreach (FileInfo mfile in collFile)
                            {
                                string FileName = mfile.FullName.Remove(0, ApplicationPath.Length);
                                FileName = FileName.Replace("\\", "/");
                                ddlSource.Items.Add(new ListItem(FileName, FileName.ToLower()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        /// <summary>
        /// Loads the control type 
        /// </summary>
        public void LoadControlType()
        {
            try
            {
                ddlType.DataSource = SageFrameLists.ControlType();
                ddlType.DataTextField = "Value";
                ddlType.DataValueField = "Key";
                ddlType.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbUpdateModlueControl_Click(object sender, ImageClickEventArgs e)
        {
            string ExtensionMessage = string.Empty;
            if (Request.QueryString["moduledef"] != null)
            {
                try
                {
                    //add
                    int _moduledefid = int.Parse(Request.QueryString["moduledef"]);
                    string _moduleControlKey = txtKey.Text;
                    string _moduleControlTitle = txtTitle.Text;
                    string _moduleControlSrc = ddlSource.SelectedItem.ToString();
                    string _moduleControlHelpUrl = txtHelpURL.Text;
                    bool _moduleSupportsPartialRendering = chkSupportsPartialRendering.Checked;
                    int _controlType = int.Parse(ddlType.SelectedItem.Value);

                    int isUnique = CheckUniqueControlType(0, _moduledefid, _controlType, GetPortalID, false);
                    if (isUnique == 0)
                    {
                        string _iconFile = "";
                        if (ddlIcon.SelectedIndex != -1)
                        {
                            _iconFile = ddlIcon.SelectedItem.Value;
                        }
                        int _displayOrder = int.Parse(txtDisplayOrder.Text);
                        //add into module control table
                        db.sp_ModuleControlsAdd(ref _newmoduleControlID, _moduledefid, _moduleControlKey, _moduleControlTitle, _moduleControlSrc,
                            _iconFile, _controlType, _displayOrder, _moduleControlHelpUrl, _moduleSupportsPartialRendering, true, DateTime.Now,
                            GetPortalID, GetUsername);
                        ExtensionMessage = GetSageMessage("Extensions_Editors", "ModuleControlIsAddedSuccessfully");
                        ClearSessions();
                        string ControlPath = "/Modules/Admin/Extensions/Editors/ModuleEditor.ascx&moduleid=" + HttpContext.Current.Session["moduleid"] + "&ExtensionMessage=" + ExtensionMessage;
                        ProcessSourceControlUrl(Request.RawUrl, ControlPath, "extension");
                    }
                    else
                    {
                        lblErrorControlType.Visible = true;
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions_Editors", "ModuleControlAlreadyExists"), "", SageMessageType.Alert);
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }

            else if (Request.QueryString["modulecontrol"] != null)
            {
                //update
                try
                {
                    int _modulecontriolid = int.Parse(Request.QueryString["modulecontrol"]);
                    string _moduleControlKey = txtKey.Text;
                    string _moduleControlTitle = txtTitle.Text;
                    string _moduleControlSrc = ddlSource.SelectedItem.ToString();
                    string _moduleControlHelpUrl = txtHelpURL.Text;
                    bool _moduleSupportsPartialRendering = chkSupportsPartialRendering.Checked;
                    int _controlType = int.Parse(ddlType.SelectedItem.Value);

                    int isUnique = CheckUniqueControlType(_modulecontriolid, 0, _controlType, GetPortalID, true);
                    if (isUnique == 0)
                    {
                        string _iconFile = "";
                        if (ddlIcon.SelectedIndex != -1)
                        {
                            _iconFile = ddlIcon.SelectedItem.Value;
                        }
                        int _displayOrder = int.Parse(txtDisplayOrder.Text);

                        //update into module control table
                        db.sp_ModuleControlsUpdate(_modulecontriolid, _moduleControlKey, _moduleControlTitle, _moduleControlSrc,
                            _iconFile, _controlType, _displayOrder, _moduleControlHelpUrl, _moduleSupportsPartialRendering, true, true, DateTime.Now,
                            GetPortalID, GetUsername);
                        ExtensionMessage = GetSageMessage("Extensions_Editors", "ModuleControlIsUpdatedSuccessfully");
                        ClearSessions();
                        string ControlPath = "/Modules/Admin/Extensions/Editors/ModuleEditor.ascx&moduleid=" + HttpContext.Current.Session["moduleid"] + "&ExtensionMessage=" + ExtensionMessage;
                        ProcessSourceControlUrl(Request.RawUrl, ControlPath, "extension");
                    }
                    else
                    {
                        lblErrorControlType.Visible = true;
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions_Editors", "ModuleControlAlreadyExists"), "", SageMessageType.Alert);
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }            
        }

        private Int32 CheckUniqueControlType(int _modulecontriolid, int _moduledefid, int _controlType, int _portalId, bool _isEdit)
        {
            db.sp_CheckUnquieModuleControlsControlType(_modulecontriolid, _moduledefid, _controlType, _portalId, _isEdit, ref _controlCount);
            return Int32.Parse(_controlCount.ToString());
        }

        protected void imbCancelModlueControl_Click(object sender, ImageClickEventArgs e)
        {
            ClearSessions();
            string ControlPath = "/Modules/Admin/Extensions/Editors/ModuleEditor.ascx&moduleid=" + HttpContext.Current.Session["moduleid"];
            ProcessSourceControlUrl(Request.RawUrl, ControlPath, "extension");
        }

        protected void ClearSessions()
        {
            HttpContext.Current.Session["ModuleName"] = null;
            HttpContext.Current.Session["ModuleDefinitionName"] = null;
            HttpContext.Current.Session["ActiveTabID"] = 2;
        }

        protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSource.SelectedIndex != -1)
            {
                LoadIcons(SystemSetting.glbImageFileTypes);
            }
        }

        private void LoadIcons(string strExtensions)
        {
            try
            {
                string strRoot = null;
                string[] arrFiles = null;
                string strExtension = null;
                ddlIcon.Items.Clear();
                ddlIcon.Items.Insert(0, new ListItem("<Not Specified>", ""));

                strRoot = ddlSource.SelectedItem.Value;
                strRoot = HttpContext.Current.Server.MapPath("~/" + strRoot.Substring(0, strRoot.LastIndexOf("/")));

                if (Directory.Exists(strRoot))
                {
                    arrFiles = Directory.GetFiles(strRoot);
                    foreach (string strFile in arrFiles)
                    {
                        strExtension = Path.GetExtension(strFile).Replace(".", "");
                        if (strExtensions.Contains(strExtension))
                        {
                            ddlIcon.Items.Add(new ListItem(Path.GetFileName(strFile), Path.GetFileName(strFile).ToLower()));
                        }
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