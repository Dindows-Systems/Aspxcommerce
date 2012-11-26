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
using SageFrame.ProfileManagement;
using SageFrame.Profile;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using SageFrame.SageFrameClass;
using AjaxControlToolkit;
using SageFrame.ListManagement;
using System.Collections.Specialized;
using SageFrame.Framework;
using System.IO;


namespace SageFrame.Modules.Admin.UserManagement
{
    
    public partial class ctl_UserProfile : BaseAdministrationUserControl
    {        
        public string EditUserName
        {
            get {
                if (ViewState["EditUsername"] != null)
                {
                    return ViewState["EditUsername"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set 
            {
                ViewState["EditUsername"] = value;
                try
                {
                    FillFormData();
                    ShowHideStaterow();
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }   

        ProfileManagementDataContext db = new ProfileManagementDataContext(SystemSetting.SageFrameConnectionString);
        private void GenrateForm()
        {
            try
            {
                if (pnlForm.Controls.Count == 1)
                {
                    var LINQProfile = db.sp_ProfileListActive(GetPortalID);
                    CommonFunction LToDCon = new CommonFunction();
                    DataTable dt = LToDCon.LINQToDataTable(LINQProfile);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Table tblSageForm = new Table();
                        tblSageForm.CssClass = "cssClassForm";
                        tblSageForm.ID = "tblSageForm";
                        tblSageForm.EnableViewState = true;
                        string parentKey = string.Empty;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int ProfileID = Int32.Parse(dt.Rows[i]["ProfileID"].ToString());
                            TableRow tbrSageRow = new TableRow();
                            tbrSageRow.ID = "tbrSageRow_" + ProfileID;
                            tbrSageRow.EnableViewState = true;

                            TableCell tcleftSagetd = new TableCell();
                            tcleftSagetd.ID = "tcleftSagetd_" + ProfileID;
                            tcleftSagetd.CssClass = "cssClassFormtdLleft";
                            tcleftSagetd.EnableViewState = true;
                            tcleftSagetd.Width = Unit.Percentage(20);

                            Label lblValue = new Label();
                            lblValue.ID = "lblValue_" + ProfileID;
                            lblValue.Text = dt.Rows[i]["Name"].ToString();
                            lblValue.ToolTip = dt.Rows[i]["Name"].ToString();
                            lblValue.EnableViewState = true;
                            lblValue.CssClass = "cssClassFormLabel";

                            tcleftSagetd.Controls.Add(lblValue);

                            tbrSageRow.Cells.Add(tcleftSagetd);

                            
                            int PropertyTypeID = Int32.Parse(dt.Rows[i]["PropertyTypeID"].ToString());

                            TableCell tcrightSagetd = new TableCell();
                            tcrightSagetd.ID = "tcrightSagetd_" + ProfileID;
                            tcrightSagetd.CssClass = "cssClassFormtdRight";
                            tcrightSagetd.EnableViewState = true;

                            
                            

                            switch (PropertyTypeID)
                            {
                                case 1://TextBox
                                    TextBox BDTextBox = new TextBox();
                                    BDTextBox.ID = "BDTextBox_" + ProfileID;
                                    BDTextBox.CssClass = "cssClassNormalTextBox";
                                    BDTextBox.EnableViewState = true;



                                    int DataType = Int32.Parse(dt.Rows[i]["DataType"].ToString());
                                    switch (DataType)
                                    {
                                        case 0://String
                                            //Adding in Pandel
                                            tcrightSagetd.Controls.Add(BDTextBox);
                                            break;
                                        case 1://Integer
                                            BDTextBox.Attributes.Add("OnKeydown", "return NumberKey(event)");
                                            //Adding in Pandel
                                            tcrightSagetd.Controls.Add(BDTextBox);
                                            break;
                                        case 2://Decimal
                                            BDTextBox.Attributes.Add("OnKeydown", "return NumberKeyWithDecimal(event)");
                                            //Adding in Pandel
                                            tcrightSagetd.Controls.Add(BDTextBox);
                                            break;
                                        case 3://DateTime
                                            ImageButton imb = new ImageButton();
                                            imb.ID = "imb_" + ProfileID;
                                            imb.ImageUrl = GetTemplateImageUrl("imgcalendar.png", true);

                                            CalendarExtender Cex = new CalendarExtender();
                                            Cex.ID = "Cex_" + ProfileID;
                                            Cex.TargetControlID = BDTextBox.ID;
                                            Cex.PopupButtonID = imb.ID;
                                            Cex.SelectedDate = DateTime.Now;
                                            BDTextBox.ToolTip = "DateTime";
                                            BDTextBox.Enabled = false;

                                            //Adding in Panel
                                            tcrightSagetd.Controls.Add(BDTextBox);
                                            tcrightSagetd.Controls.Add(imb);
                                            tcrightSagetd.Controls.Add(Cex);
                                            break;

                                    }

                                    

                                    bool IsRequred = bool.Parse(dt.Rows[i]["IsRequired"].ToString());
                                    if (IsRequred)
                                    {
                                        RequiredFieldValidator rfv = new RequiredFieldValidator();
                                        rfv.ID = "rfv_" + ProfileID;
                                        rfv.ControlToValidate = BDTextBox.ID;
                                        rfv.ErrorMessage = "*";
                                        rfv.ValidationGroup = "UserProfile";
                                        tcrightSagetd.Controls.Add(rfv);
                                    }
                                    

                                    break;
                                case 2://DropDownList
                                    DropDownList ddl = new DropDownList();
                                    ddl.ID = "BDTextBox_" + ProfileID;
                                    ddl.CssClass = "cssClassDropDown";
                                    //ddl.Width = 200;
                                    ddl.EnableViewState = true;

                                    //Setting Data Source
                                    var LinqProvileValue = db.sp_ProfileValueGetActiveByProfileID(ProfileID, GetPortalID);
                                    ddl.DataSource = LinqProvileValue;
                                    ddl.DataValueField = "ProfileValueID";
                                    ddl.DataTextField = "Name";
                                    ddl.DataBind();
                                    if (ddl.Items.Count > 0)
                                    {
                                        ddl.SelectedIndex = 0;
                                    }

                                    //Adding in Pandel
                                    tcrightSagetd.Controls.Add(ddl);
                                    break;
                                case 3://CheckBoxList
                                    CheckBoxList chbl = new CheckBoxList();
                                    chbl.ID = "BDTextBox_" + ProfileID;
                                    chbl.CssClass = "cssClassCheckBox";
                                    chbl.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal;
                                    chbl.RepeatColumns = 2;
                                    chbl.EnableViewState = true;

                                    //Setting Data Source
                                    LinqProvileValue = db.sp_ProfileValueGetActiveByProfileID(ProfileID, GetPortalID);
                                    chbl.DataSource = LinqProvileValue;
                                    chbl.DataValueField = "ProfileValueID";
                                    chbl.DataTextField = "Name";
                                    chbl.DataBind();
                                    if (chbl.Items.Count > 0)
                                    {
                                        chbl.SelectedIndex = 0;
                                    }

                                    //Adding in Pandel
                                    tcrightSagetd.Controls.Add(chbl);
                                    break;
                                case 4://RadioButtonList
                                    RadioButtonList rdbl = new RadioButtonList();
                                    rdbl.ID = "BDTextBox_" + ProfileID;
                                    rdbl.CssClass = "cssClassRadioButtonList";
                                    rdbl.EnableViewState = true;
                                    rdbl.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal;
                                    rdbl.RepeatColumns = 2;

                                    //Setting Data Source
                                    LinqProvileValue = db.sp_ProfileValueGetActiveByProfileID(ProfileID, GetPortalID);
                                    rdbl.DataSource = LinqProvileValue;
                                    rdbl.DataValueField = "ProfileValueID";
                                    rdbl.DataTextField = "Name";
                                    rdbl.DataBind();
                                    if (rdbl.Items.Count > 0)
                                    {
                                        rdbl.SelectedIndex = 0;
                                    }
									tcrightSagetd.CssClass = "cssClassButtonListWrapper";
                                    //Adding in Pandel
                                    tcrightSagetd.Controls.Add(rdbl);
                                    break;
                                case 5://DropDownList
                                    DropDownList cddl = new DropDownList();
                                    cddl.ID = "BDTextBox_" + ProfileID;
                                    cddl.CssClass = "cssClassDropDown";
                                    //cddl.Width = 200;
                                    cddl.EnableViewState = true;
                                    cddl.SelectedIndexChanged +=new EventHandler(cddl_SelectedIndexChanged);
                                    cddl.AutoPostBack = true;
                                    ViewState["cddlID"] = cddl.ID;
                                    //Setting Data Source
                                    ListManagementDataContext cdb = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
                                    var CLINQ = cdb.sp_GetListEntrybyNameAndID("Country", -1,GetCurrentCultureName);                                   
                                    cddl.DataSource = CLINQ;
                                    cddl.DataValueField = "Value";
                                    cddl.DataTextField = "Text";
                                    cddl.DataBind();
                                    if (cddl.Items.Count > 0)
                                    {
                                        cddl.SelectedIndex = 0;
                                        parentKey = "Country." + cddl.SelectedItem.Value;
                                    }

                                    //Adding in Pandel
                                    tcrightSagetd.Controls.Add(cddl);
                                    break;
                                case 6://DropDownList
                                    DropDownList Sddl = new DropDownList();
                                    Sddl.ID = "BDTextBox_" + ProfileID;
                                    Sddl.CssClass = "cssClassNormalTextBox";
                                    //Sddl.Width = 200;
                                    Sddl.EnableViewState = true;
                                    ViewState["SddlID"] = Sddl.ID;
                                    //Setting Data Source
                                    string listName = "Region";
                                    ListManagementDataContext Sdb = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
                                    var listDetail = Sdb.sp_GetListEntriesByNameParentKeyAndPortalID(listName, parentKey, -1,GetCurrentCultureName);
                                    Sddl.DataSource = listDetail;
                                    Sddl.DataValueField = "Value";
                                    Sddl.DataTextField = "Text";
                                    Sddl.DataBind();
                                    if (Sddl.Items.Count > 0)
                                    {
                                        Sddl.SelectedIndex = 0;
                                    }
                                    ViewState["StateRow"] = tbrSageRow.ID;

                                    //Adding in Pandel
                                    tcrightSagetd.Controls.Add(Sddl);
                                    break;
                                case 7://DropDownList
                                    DropDownList TimeZoneddl = new DropDownList();
                                    TimeZoneddl.ID = "BDTextBox_" + ProfileID;
                                    TimeZoneddl.CssClass = "cssClassDropDown";
                                    //TimeZoneddl.Width = 200;
                                    TimeZoneddl.EnableViewState = true;

                                    //Setting Data Source
                                    NameValueCollection nvlTimeZone = SageFrame.Localization.Localization.GetTimeZones(((PageBase)this.Page).GetCurrentCultureName);
                                    TimeZoneddl.DataSource = nvlTimeZone;                                    
                                    TimeZoneddl.DataBind();
                                    if (TimeZoneddl.Items.Count > 0)
                                    {
                                        TimeZoneddl.SelectedIndex = 0;                                       
                                    }

                                    //Adding in Pandel
                                    tcrightSagetd.Controls.Add(TimeZoneddl);
                                    break;
                                case 8://File Upload
                                    FileUpload AsyFlu = new FileUpload();
                                    AsyFlu.ID = "BDTextBox_" + ProfileID;
                                    AsyFlu.CssClass = "cssClassNormalFileUpload";                                    
                                    AsyFlu.EnableViewState = true;
                                    tcrightSagetd.Controls.Add(AsyFlu);

                                    System.Web.UI.HtmlControls.HtmlGenericControl gcDiv = new HtmlGenericControl("div");
                                    gcDiv.ID = "BDDiv_" + ProfileID;
                                    gcDiv.Attributes.Add("class","cssClassProfileImageWrapper");
                                    gcDiv.EnableViewState = true;
                                    gcDiv.Visible = false;

                                    Image img = new Image();
                                    img.ID = "Bdimg_" + ProfileID;
                                    img.CssClass = "cssClassProfileImage";
                                    img.EnableViewState = true;
                                    gcDiv.Controls.Add(img);
                                    tcrightSagetd.Controls.Add(gcDiv);
                                    break;
                            }
                            tbrSageRow.Cells.Add(tcrightSagetd);
                            tblSageForm.Rows.Add(tbrSageRow);
                            pnlForm.Controls.Add(tblSageForm);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void cddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["SddlID"] != null)
                {
                    DropDownList ddl = (DropDownList)sender;
                    string listName = "Region";
                    string parentKey = "Country." + ddl.SelectedItem.Value;
                    ListManagementDataContext Sdb = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
                    var listDetail = Sdb.sp_GetListEntriesByNameParentKeyAndPortalID(listName, parentKey, -1,GetCurrentCultureName);
                    DropDownList Sddl = (DropDownList)this.FindControl(ViewState["SddlID"].ToString());
                    if (Sddl != null)
                    {
                        Sddl.DataSource = listDetail;
                        Sddl.DataValueField = "Value";
                        Sddl.DataTextField = "Text";
                        Sddl.DataBind();
                    }
                    ShowHideStaterow();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void ShowHideStaterow()
        {
            if (ViewState["StateRow"] != null && ViewState["SddlID"] != null)
            {
                TableRow tr = (TableRow)this.FindControl(ViewState["StateRow"].ToString());
                DropDownList Sddl = (DropDownList)this.FindControl(ViewState["SddlID"].ToString());
                if (tr != null && Sddl != null)
                {
                    if (Sddl.Items.Count > 0)
                    {
                        tr.Visible = true;
                    }
                    else
                    {
                        tr.Visible = false;
                    }
                }
            }
        }

        private void GetFormData()
        {
            #region "Loop for all controls"

            if (EditUserName == string.Empty)
            {
                EditUserName = GetUsername;
                //EditUserName = GetUsername;
            }

            foreach (Control cont in pnlForm.Controls)//grdList.TemplateControl.Controls
            {

                if (cont.GetType() == typeof(Table))
                {
                    foreach (Control pnlCon1 in cont.Controls)
                    {
                        if (pnlCon1.GetType() == typeof(TableRow))
                        {
                            foreach (Control pnlCon2 in pnlCon1.Controls)
                            {
                                if (pnlCon2.GetType() == typeof(TableCell))
                                {
                                    foreach (Control pnlCon3 in pnlCon2.Controls)
                                    {
                                        int ProfileID = 0;
                                        string ProfileValue = string.Empty;
                                        //TextBox
                                        if (pnlCon3.GetType() == typeof(TextBox))
                                        {
                                            TextBox txt = (TextBox)pnlCon3;
                                            if (txt != null)
                                            {
                                                string conID = txt.ID;
                                                string[] IDColl = conID.Split("_".ToCharArray());
                                                if (IDColl.Length > 0)
                                                {
                                                    ProfileID = Int32.Parse(IDColl[1].ToString());
                                                    ProfileValue = txt.Text.Trim();
                                                    if (txt.ToolTip != null && txt.ToolTip.ToLower() == "DateTime".ToLower())
                                                    {
                                                        DateTime mdate = new DateTime();
                                                        bool isProperDate = DateTime.TryParse(txt.Text.Trim(), out mdate);
                                                        if (isProperDate)
                                                        {
                                                            if (DateTime.Now == mdate)
                                                            {
                                                               // ShowMessage(SageMessageTitle.Notification.ToString(), "Please provide actual date of birth!", "", SageMessageType.Error);
                                                                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "PleaseProvideActualDateOfBirth"), "", SageMessageType.Error);
                                                                return;
                                                            }
                                                            if (DateTime.Now < mdate)
                                                            {
                                                               // ShowMessage(SageMessageTitle.Notification.ToString(), "Please provide actual date of birth!", "", SageMessageType.Error);
                                                                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "ProvideActualDateOfBirth"), "", SageMessageType.Error);
                                                                return;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //DropDownList
                                        if (pnlCon3.GetType() == typeof(DropDownList))
                                        {
                                            DropDownList ddl = (DropDownList)pnlCon3;
                                            if (ddl != null && ddl.Items.Count > 0)
                                            {
                                                string conID = ddl.ID;
                                                string[] IDColl = conID.Split("_".ToCharArray());
                                                if (IDColl.Length > 0)
                                                {
                                                    ProfileID = Int32.Parse(IDColl[1].ToString());
                                                    ProfileValue = ddl.SelectedItem.Text;
                                                }
                                            }
                                        }
                                        //CheckBoxList
                                        if (pnlCon3.GetType() == typeof(CheckBoxList))
                                        {
                                            CheckBoxList chbl = (CheckBoxList)pnlCon3;
                                            if (chbl != null)
                                            {
                                                string conID = chbl.ID;
                                                string[] IDColl = conID.Split("_".ToCharArray());
                                                if (IDColl.Length > 0)
                                                {
                                                    ProfileID = Int32.Parse(IDColl[1].ToString());
                                                    ProfileValue = chbl.SelectedItem.Text;
                                                }
                                            }
                                        }
                                        //RadioButtonList
                                        if (pnlCon3.GetType() == typeof(RadioButtonList))
                                        {
                                            RadioButtonList rdbl = (RadioButtonList)pnlCon3;
                                            if (rdbl != null)
                                            {
                                                string conID = rdbl.ID;
                                                string[] IDColl = conID.Split("_".ToCharArray());
                                                if (IDColl.Length > 0)
                                                {
                                                    ProfileID = Int32.Parse(IDColl[1].ToString());
                                                    ProfileValue = rdbl.SelectedItem.Text;
                                                }
                                            }
                                        }

                                        //File Upload
                                        if (pnlCon3.GetType() == typeof(FileUpload))
                                        {
                                            FileUpload asFlu = (FileUpload)pnlCon3;
                                            if (asFlu != null)
                                            {
                                                if (asFlu.HasFile)
                                                {
                                                    string Ext = asFlu.PostedFile.ContentType;
                                                    if (!string.IsNullOrEmpty(Ext))
                                                        Ext = Ext.Replace(".", "");
                                                    SageFrameConfig pagebase = new SageFrameConfig();
                                                    if (PictureManager.IsValidIImageTypeWitMime(Ext))
                                                    {
                                                        int MaxFileSize = (pagebase.GetSettingIntByKey(SageFrameSettingKeys.PortalUserProfileMaxImageSize) * 1024);//Converted MB in to  KB
                                                        if (asFlu.PostedFile.ContentLength <= MaxFileSize)
                                                        {
                                                            string conID = asFlu.ID;
                                                            string[] IDColl = conID.Split("_".ToCharArray());
                                                            if (IDColl.Length > 0)
                                                            {
                                                                ProfileID = Int32.Parse(IDColl[1].ToString());
                                                                string path = HttpContext.Current.Server.MapPath("~/");
                                                                string MapPath = "Modules\\Admin\\UserManagement\\ProfileImage";
                                                                MapPath = Path.Combine(path, MapPath);
                                                                string savedPathMedium = "Modules\\Admin\\UserManagement\\ProfileImage\\MediumProfileImage";
                                                                savedPathMedium = Path.Combine(path, savedPathMedium);
                                                                string savedPathSmall = "Modules\\Admin\\UserManagement\\ProfileImage\\SmallProfileImage";
                                                                savedPathSmall = Path.Combine(path, savedPathSmall);

                                                                var folderPaths = db.sp_ProfileImageFoldersGet(EditUserName, ProfileID, GetPortalID);
                                                                foreach (sp_ProfileImageFoldersGetResult imageFolders in folderPaths)
                                                                {
                                                                    DeleteImageFiles(MapPath + '\\' + imageFolders.Value);
                                                                    DeleteImageFiles(savedPathMedium + '\\' + imageFolders.Value);
                                                                    DeleteImageFiles(savedPathSmall + '\\' + imageFolders.Value);
                                                                }

                                                                char[] separator = new char[] { '.' };
                                                                string[] fileNames = asFlu.FileName.Split(separator);
                                                                string fileName = fileNames[0];
                                                                string extension = string.Empty;
                                                                if (fileNames.Length > 1)
                                                                {
                                                                    extension = fileNames[fileNames.Length - 1];
                                                                }
                                                                fileName = PictureManager.GetFileName(fileName);
                                                                string strLargeImagePath = PictureManager.SaveImage(asFlu, fileName + "." + extension, MapPath);
                                                                string strMediumImagePath = PictureManager.CreateMediumThumnail(strLargeImagePath, GetPortalID,fileName + "." + extension, savedPathMedium, pagebase.GetSettingIntByKey(SageFrameSettingKeys.PortalUserProfileMediumImageSize));
                                                                string strSmallImagePath = PictureManager.CreateSmallThumnail(strLargeImagePath, GetPortalID, fileName + "." + extension, savedPathSmall, pagebase.GetSettingIntByKey(SageFrameSettingKeys.PortalUserProfileSmallImageSize));

                                                                //string FullPath = Server.MapPath(MapPath + asFlu.PostedFile.FileName);                                                        
                                                                //asFlu.SaveAs(FullPath);
                                                                ProfileValue = fileName + "." + extension;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "InvalidImageFileSize"), "", SageMessageType.Error);
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "InvalidImageFile"), "", SageMessageType.Error);
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    string conID = asFlu.ID;
                                                    string[] IDColl = conID.Split("_".ToCharArray());
                                                    if (IDColl.Length > 0)
                                                    {
                                                        ProfileID = Int32.Parse(IDColl[1].ToString());
                                                        HtmlGenericControl sageDiv = (HtmlGenericControl)pnlCon2.FindControl("BDDiv_" + ProfileID);
                                                        Image BDimg = (Image)sageDiv.FindControl("Bdimg_" + ProfileID);
                                                        if (BDimg != null)
                                                        {
                                                            string strImage = BDimg.ImageUrl;
                                                            if (!string.IsNullOrEmpty(strImage))
                                                            {
                                                                strImage = strImage.Remove(0, strImage.LastIndexOf("/"));
                                                                ProfileValue = strImage;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        //Inserting in Data Base                                
                                        System.Nullable<int> UserProfileID = 0;
                                        if (ProfileID != 0)
                                        {
                                            db.sp_UserProfileAdd(ref UserProfileID, EditUserName, ProfileID, ProfileValue, true, DateTime.Now,
                                                GetPortalID, GetUsername);
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }

            #endregion
        }

        private void DeleteImageFiles(string imagePath)
        {
            FileInfo imgInfo = new FileInfo(imagePath);
            if (imgInfo != null)
            {
                imgInfo.Delete();
            }
        }

        private void FillFormData()
        {            
            try
            {

                //Geting Profile Exists Data
                #region "Loop For All Controls in Form To Fill Data"

                if (EditUserName == string.Empty)
                    EditUserName = GetUsername;

                
                Hashtable htProfile = new Hashtable();
                var LinqUserProfile = db.sp_UserProfileActiveListByUsername(EditUserName, GetPortalID);// UserProfileActiveListByUserID(mUserID, GetPortalID);

                if (LinqUserProfile != null)
                {
                    foreach (sp_UserProfileActiveListByUsernameResult objResult in LinqUserProfile)
                    {
                        htProfile.Add(objResult.ProfileID.ToString(), objResult.Value);
                    }
                }

                if (htProfile.Count > 0)
                {
                    #region "Loop Section"

                    foreach (Control cont in pnlForm.Controls)
                    {
                        if (cont.GetType() == typeof(Table))
                        {
                            foreach (Control pnlCon1 in cont.Controls)
                            {
                                if (pnlCon1.GetType() == typeof(TableRow))
                                {
                                    foreach (Control pnlCon2 in pnlCon1.Controls)
                                    {
                                        if (pnlCon2.GetType() == typeof(TableCell))
                                        {
                                            foreach (Control pnlCon3 in pnlCon2.Controls)
                                            {
                                                #region "Data Sections"
                                                //TextBox
                                                if (pnlCon3.GetType() == typeof(TextBox))
                                                {
                                                    TextBox txt = (TextBox)pnlCon3;
                                                    if (txt != null)
                                                    {
                                                        string conID = txt.ID;
                                                        string[] IDColl = conID.Split("_".ToCharArray());
                                                        if (IDColl.Length > 0)
                                                        {
                                                            string Key = IDColl[1].ToString();
                                                            if (htProfile.ContainsKey(Key))
                                                            {
                                                                txt.Text = htProfile[Key].ToString();
                                                            }
                                                        }
                                                    }
                                                }
                                                //DropDownList
                                                if (pnlCon3.GetType() == typeof(DropDownList))
                                                {
                                                    DropDownList ddl = (DropDownList)pnlCon3;
                                                    if (ddl != null)
                                                    {
                                                        string strStateddlID = string.Empty;
                                                        if (ViewState["SddlID"] != null)
                                                        {
                                                            strStateddlID = ViewState["SddlID"].ToString();
                                                        }
                                                        string conID = ddl.ID;
                                                        if (strStateddlID == conID)
                                                        {
                                                            if (ViewState["cddlID"] != null)
                                                            {
                                                                DropDownList ccddl = (DropDownList)this.FindControl(ViewState["cddlID"].ToString());
                                                                if (ccddl != null)
                                                                {
                                                                    if (ccddl.Items.Count > 0)
                                                                    {
                                                                        string parentKey = "Country." + ccddl.SelectedItem.Value;
                                                                        string listName = "Region";
                                                                        ListManagementDataContext Sdb = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
                                                                        var listDetail = Sdb.sp_GetListEntriesByNameParentKeyAndPortalID(listName, parentKey, -1,GetCurrentCultureName);
                                                                        ddl.DataSource = listDetail;
                                                                        ddl.DataValueField = "Value";
                                                                        ddl.DataTextField = "Text";
                                                                        ddl.DataBind();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        string[] IDColl = conID.Split("_".ToCharArray());
                                                        if (IDColl.Length > 0)
                                                        {
                                                            string Key = IDColl[1].ToString();
                                                            if (htProfile.ContainsKey(Key))
                                                            {
                                                                ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(htProfile[Key].ToString()));
                                                            }
                                                        }

                                                    }
                                                }
                                                //CheckBoxList
                                                if (pnlCon3.GetType() == typeof(CheckBoxList))
                                                {
                                                    CheckBoxList chbl = (CheckBoxList)pnlCon3;
                                                    if (chbl != null)
                                                    {
                                                        string conID = chbl.ID;
                                                        string[] IDColl = conID.Split("_".ToCharArray());
                                                        if (IDColl.Length > 0)
                                                        {
                                                            string Key = IDColl[1].ToString();
                                                            if (htProfile.ContainsKey(Key))
                                                            {
                                                                int Index = chbl.Items.IndexOf(chbl.Items.FindByText(htProfile[Key].ToString()));
                                                                chbl.Items[Index].Selected = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                //RadioButtonList
                                                if (pnlCon3.GetType() == typeof(RadioButtonList))
                                                {
                                                    RadioButtonList rdbl = (RadioButtonList)pnlCon3;
                                                    if (rdbl != null)
                                                    {
                                                        string conID = rdbl.ID;
                                                        string[] IDColl = conID.Split("_".ToCharArray());
                                                        if (IDColl.Length > 0)
                                                        {
                                                            string Key = IDColl[1].ToString();
                                                            if (htProfile.ContainsKey(Key))
                                                            {
                                                                int Index = rdbl.Items.IndexOf(rdbl.Items.FindByText(htProfile[Key].ToString()));
                                                                rdbl.Items[Index].Selected = true;
                                                            }
                                                        }
                                                    }
                                                }

                                                //FileUpload
                                                if (pnlCon3.GetType() == typeof(FileUpload))
                                                {
                                                    FileUpload asFlu = (FileUpload)pnlCon3;
                                                    if (asFlu != null)
                                                    {
                                                        string conID = asFlu.ID;
                                                        string[] IDColl = conID.Split("_".ToCharArray());
                                                        if (IDColl.Length > 0)
                                                        {
                                                            string Key = IDColl[1].ToString();
                                                            if (htProfile.ContainsKey(Key))
                                                            {
                                                                //System.Web.UI.HtmlControls.HtmlGenericControl gcDiv = new HtmlGenericControl("div");
                                                                //gcDiv.ID = "BDDiv_" + ProfileID;
                                                                HtmlGenericControl sageDiv = (HtmlGenericControl)pnlCon2.FindControl("BDDiv_" + Key);
                                                                Image BDimg = (Image)sageDiv.FindControl("Bdimg_" + Key);
                                                                if (sageDiv != null && BDimg != null)
                                                                {
                                                                    string ImageName = htProfile[Key].ToString();
                                                                    string MapPath = "~/Modules/Admin/UserManagement/ProfileImage/MediumProfileImage/";
                                                                    string CurrentPath = MapPath + ImageName;
                                                                    BDimg.ImageUrl = CurrentPath;//GetTemplateImageUrl("imgedit.png", true); //this.Page.ResolveUrl(CurrentPath); ;
                                                                    sageDiv.Visible = true;
                                                                    //BDimg.Visible = true;
                                                                    //BDimg.Width = 200;
                                                                    //BDimg.Height = 200;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                HtmlGenericControl sageDiv = (HtmlGenericControl)pnlCon2.FindControl("BDDiv_" + Key);
                                                                Image BDimg = (Image)sageDiv.FindControl("Bdimg_" + Key);
                                                                if (sageDiv != null && BDimg != null)
                                                                {
                                                                   
                                                                    BDimg.ImageUrl = "";//GetTemplateImageUrl("imgedit.png", true); //this.Page.ResolveUrl(CurrentPath); ;
                                                                    sageDiv.Visible = false;
                                                                    //BDimg.Visible = true;
                                                                    //BDimg.Width = 200;
                                                                    //BDimg.Height = 200;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                                #endregion
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    //Clear Form Data.
                    #region "Loop Section For Clear"

                    foreach (Control cont in pnlForm.Controls)
                    {
                        if (cont.GetType() == typeof(Table))
                        {
                            foreach (Control pnlCon1 in cont.Controls)
                            {
                                if (pnlCon1.GetType() == typeof(TableRow))
                                {
                                    foreach (Control pnlCon2 in pnlCon1.Controls)
                                    {
                                        if (pnlCon2.GetType() == typeof(TableCell))
                                        {
                                            foreach (Control pnlCon3 in pnlCon2.Controls)
                                            {
                                                #region "Data Sections For Clear"
                                                //TextBox
                                                if (pnlCon3.GetType() == typeof(TextBox))
                                                {
                                                    TextBox txt = (TextBox)pnlCon3;
                                                    if (txt != null)
                                                    {
                                                        string conID = txt.ID;
                                                        string[] IDColl = conID.Split("_".ToCharArray());
                                                        if (IDColl.Length > 0)
                                                        {
                                                            string Key = IDColl[1].ToString();
                                                            txt.Text = "";                                                            
                                                        }
                                                    }
                                                }
                                                //DropDownList
                                                if (pnlCon3.GetType() == typeof(DropDownList))
                                                {
                                                    DropDownList ddl = (DropDownList)pnlCon3;
                                                    if (ddl != null)
                                                    {
                                                        string conID = ddl.ID;
                                                        string[] IDColl = conID.Split("_".ToCharArray());
                                                        if (IDColl.Length > 0)
                                                        {
                                                            string Key = IDColl[1].ToString();
                                                            if (ddl.Items.Count > 0)
                                                            {
                                                                ddl.SelectedIndex = 0;
                                                            }                                                           
                                                        }
                                                    }
                                                }
                                                //CheckBoxList
                                                if (pnlCon3.GetType() == typeof(CheckBoxList))
                                                {
                                                    CheckBoxList chbl = (CheckBoxList)pnlCon3;
                                                    if (chbl != null)
                                                    {
                                                        string conID = chbl.ID;
                                                        string[] IDColl = conID.Split("_".ToCharArray());
                                                        if (IDColl.Length > 0)
                                                        {
                                                            string Key = IDColl[1].ToString();
                                                            if (chbl.Items.Count > 0)
                                                            {
                                                                chbl.SelectedIndex = 0;
                                                            }                                                            
                                                        }
                                                    }
                                                }
                                                //RadioButtonList
                                                if (pnlCon3.GetType() == typeof(RadioButtonList))
                                                {
                                                    RadioButtonList rdbl = (RadioButtonList)pnlCon3;
                                                    if (rdbl != null)
                                                    {
                                                        string conID = rdbl.ID;
                                                        string[] IDColl = conID.Split("_".ToCharArray());
                                                        if (IDColl.Length > 0)
                                                        {
                                                            string Key = IDColl[1].ToString();
                                                            if (rdbl.Items.Count > 0)
                                                            {
                                                                rdbl.SelectedIndex = 0;
                                                            }                                                            
                                                        }
                                                    }
                                                }

                                                //FileUpload
                                                if (pnlCon3.GetType() == typeof(FileUpload))
                                                {
                                                    FileUpload asFlu = (FileUpload)pnlCon3;
                                                    if (asFlu != null)
                                                    {
                                                        string conID = asFlu.ID;
                                                        string[] IDColl = conID.Split("_".ToCharArray());
                                                        if (IDColl.Length > 0)
                                                        {
                                                            string Key = IDColl[1].ToString();
                                                            //if (htProfile.ContainsKey(Key))
                                                            //{
                                                                //System.Web.UI.HtmlControls.HtmlGenericControl gcDiv = new HtmlGenericControl("div");
                                                                //gcDiv.ID = "BDDiv_" + ProfileID;
                                                                HtmlGenericControl sageDiv = (HtmlGenericControl)pnlCon2.FindControl("BDDiv_" + Key);
                                                                Image BDimg = (Image)sageDiv.FindControl("Bdimg_" + Key);
                                                                if (sageDiv != null && BDimg != null)
                                                                {
                                                                    //string ImageName = htProfile[Key].ToString();
                                                                    //string MapPath = "~/Modules/Admin/UserManagement/ProfileImage/MediumProfileImage/";
                                                                    //string CurrentPath = MapPath + ImageName;
                                                                    BDimg.ImageUrl = "";//GetTemplateImageUrl("imgedit.png", true); //this.Page.ResolveUrl(CurrentPath); ;
                                                                    sageDiv.Visible = false;
                                                                    //BDimg.Visible = true;
                                                                    //BDimg.Width = 200;
                                                                    //BDimg.Height = 200;
                                                                }
                                                            //}
                                                        }
                                                    }
                                                }

                                                #endregion
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }

                    #endregion
                }

                #endregion
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {            
            GenrateForm();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("ID", System.Type.GetType("System.String"));
            //dt.Columns.Add("Name", System.Type.GetType("System.String"));
            //DataRow newrow1 = dt.NewRow();
            //dt.Rows.Add(newrow1);
            //dt.Rows[dt.Rows.Count - 1]["ID"] = dt.Rows.Count - 1;
            //dt.Rows[dt.Rows.Count - 1]["Name"] = "Dummy Data";
            //dt.AcceptChanges();
            //gdvList.DataSource = dt;
            //gdvList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    AddImageUrls();
                    FillFormData();
                    ShowHideStaterow();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void AddImageUrls()
        {
            imbSave.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            //imbCancel.ImageUrl = GetTemplateImageUrl("imgback.png", true);
        }

        protected void imbSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GetFormData();
                FillFormData();
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("UserManagement", "UserProfileSavedSuccessfully"), "", SageMessageType.Success);
            }
            catch (Exception ex)
            {

                ProcessException(ex);
            }
           
        }        

        //protected void imbCancel_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        if (!IsPostBack)
        //        {
        //            FillFormData();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ProcessException(ex);
        //    }
        //}

       
    }
}