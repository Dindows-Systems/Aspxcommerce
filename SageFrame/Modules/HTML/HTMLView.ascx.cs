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
using SageFrame.HTMLText;
using System.Web.Security;

using System.Globalization;
using System.Security.Permissions;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections;
using SageFrame.Web.Utilities;

namespace SageFrame.Modules.HTML
{
    public partial class HTMLView : BaseAdministrationUserControl
    {
        System.Nullable<Int32> _newHTMLContentID = 0;
        //System.Nullable<Int32> _newHTMLCommentID = 0;        

        protected void Page_Init(object sender, EventArgs e)
        {
            hdnUserModuleID.Value = SageUserModuleID; 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                

                if (!IsPostBack)
                {
                    HideAll();
                    AddImageUrls();                    
                 
                    string strCommentErrorMSG = GetSageMessage("HTML", "PleaseWriteComments");
                    imbAdd.Attributes.Add("onclick", string.Format("return ValidateHTMLComments('{0}','{1}','{2}');", txtComment.ClientID, lblErrorMessage.ClientID, strCommentErrorMSG));
                }
                BindContent(); 
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private bool IsAuthenticatedToEdit()
        {
            //To check the authentication of modulecontrol for current users
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PermissionKey", "EDIT"));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", Int32.Parse(hdnUserModuleID.Value)));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Username", GetUsername));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
            SQLHandler sagesql = new SQLHandler();
            return sagesql.ExecuteAsScalar<bool>("sp_CheckUserModulePermissionByPermissionKeyADO", ParaMeterCollection);            
        }

        private void AddImageUrls()
        {
            imbAddComment.ImageUrl = GetTemplateImageUrl("imgedit.png", true);
            imbAdd.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            imbBack.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);

            imbEdit.ImageUrl = GetTemplateImageUrl("imgedit.png", true);
            imbSave.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            imbCancel.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);

        }

        private void HideAll()
        {
            try
            {
                //divEditContent.Visible = false;
                divViewWrapper.Visible = false;
                divEditWrapper.Visible = false;               
                
                //divAddComment.Visible = false;
                //divViewComment.Visible = false;
                divEditComment.Visible = false;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        
        private void BindContent()
        {
            try
            {
                HTMLContentInfo contentInfo = GetHTMLContent(GetPortalID, Int32.Parse(hdnUserModuleID.Value), GetCurrentCultureName);

                if (contentInfo != null)
                {
                    hdfHTMLTextID.Value = contentInfo.HtmlTextID.ToString();
                    ltrContent.Text = contentInfo.Content.ToString();
                    //hdfIsActive.Value = contentInfo.IsActive.ToString();
                    //chkAllowComment.Checked = contentInfo.IsAllowedToComment;
                    if (contentInfo.IsActive == true)
                    {
                        //ltrContent.Visible = true;
                        divViewWrapper.Visible = true;

                        if (IsAuthenticatedToEdit() && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                        {
                            divEditContent.Visible = true;
                        }
                        else
                        {
                            divEditContent.Visible = false;
                        }

                        if (IsAuthenticatedForComment() && contentInfo.IsAllowedToComment == true && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                        {
                            divAddComment.Visible = true;
                            divViewComment.Visible = true;
                            //divEditComment.Visible = true;
                            if (!IsPostBack)
                            {
                                BindComment();
                            }
                        }                     
                        else
                        {
                            divAddComment.Visible = false;
                            divViewComment.Visible = true;
                            divEditComment.Visible = false;
                            if (!IsPostBack)
                            {
                                BindComment();
                            }
                        }
                    }
                    else
                    {
                        HideAll();
                        //divEditContent.Visible = false;
                        divAddComment.Visible = false;
                        divViewComment.Visible = false;
                        divEditComment.Visible = false;
                        if (IsAuthenticatedToEdit() && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                        {
                            divViewWrapper.Visible = true;
                            divEditContent.Visible = true;
                        }  
                    }
                }
                else if (contentInfo == null && Request.QueryString["ManageReturnUrl"] != null && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                {
                    HideAll();
                    divEditWrapper.Visible = true;
                    //divViewComment.Visible = true;
                    divAddComment.Visible = false;
                    divViewComment.Visible = false;
                    BindEditor();
                }
                else
                {
                    if (IsAuthenticatedToEdit() && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                    {
                        HideAll();
                        divViewWrapper.Visible = true;
                        divEditContent.Visible = true;
                        divAddComment.Visible = false;
                        divViewComment.Visible = false;
                    }
                    else
                    {
                        HideAll();
                        //divViewWrapper.Visible = true;
                        divEditContent.Visible = false;
                        divAddComment.Visible = false;
                        divViewComment.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }           
        }

        private HTMLContentInfo GetHTMLContent(int portalID, int userModuleID, string cultureName)
        {
            List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();

            ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", GetPortalID.ToString()));
            ParaMeterCollection.Add(new KeyValuePair<string, string>("@UsermoduleID", userModuleID.ToString()));
            ParaMeterCollection.Add(new KeyValuePair<string, string>("@CultureName", cultureName.ToString()));

            SQLHandler sagesql = new SQLHandler();
            //ArrayList arrColl = DataSourceHelper.FillCollection(sagesql.ExecuteAsDataReader("dbo.sp_HtmlTextGetByPortalAndUserModule", ParaMeterCollection), typeof(HTMLContentInfo));
            HTMLContentInfo objHtmlInfo = sagesql.ExecuteAsObject<HTMLContentInfo>("dbo.sp_HtmlTextGetByPortalAndUserModule", ParaMeterCollection);
            //if (arrColl != null)
            //{
            //    objHtmlInfo = (HTMLContentInfo)arrColl[0];
            //}
            return objHtmlInfo;
        }

        private void BindComment()
        {
            try
            {
                SQLHandler Sq = new SQLHandler();
                if (IsSuperUser() && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                {     
                    List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLTextID", hdfHTMLTextID.Value));

                    List<sp_HtmlCommentGetAllByHTMLTextIDResult> ml = Sq.ExecuteAsList<sp_HtmlCommentGetAllByHTMLTextIDResult>("dbo.sp_HtmlCommentGetAllByHTMLTextID", ParaMeterCollection);
 
                    if (ml != null)
                    {
                        gdvHTMLList.DataSource = ml;
                        gdvHTMLList.DataBind();
                        if (gdvHTMLList.Rows.Count > 0)
                        {
                            //Setting comment Count
                            Label lblCommentCount = gdvHTMLList.HeaderRow.FindControl("lblCommentCount") as Label;
                            if (lblCommentCount != null)
                            {
                                lblCommentCount.Text = gdvHTMLList.Rows.Count.ToString();
                            }
                            gdvHTMLList.Columns[gdvHTMLList.Columns.Count - 1].Visible = true;
                            gdvHTMLList.Columns[gdvHTMLList.Columns.Count - 2].Visible = true;

                            rowApprove.Visible = true;
                            rowIsActive.Visible = true;
                        }
                    }
                }
                else
                {
                    List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLTextID", hdfHTMLTextID.Value));

                    List<sp_HtmlActiveCommentGetByHTMLTextIDResult> nl = Sq.ExecuteAsList<sp_HtmlActiveCommentGetByHTMLTextIDResult>("dbo.sp_HtmlActiveCommentGetByHTMLTextID", ParaMeterCollection);
                    if (nl != null)
                    {
                        gdvHTMLList.DataSource = nl;
                        gdvHTMLList.DataBind();
                        if (gdvHTMLList.Rows.Count > 0)
                        {
                            //Setting comment Count
                            Label lblCommentCount = gdvHTMLList.HeaderRow.FindControl("lblCommentCount") as Label;
                            if (lblCommentCount != null)
                            {
                                lblCommentCount.Text = gdvHTMLList.Rows.Count.ToString();
                            }
                            divViewComment.Style.Add("display", "block");
                            gdvHTMLList.Columns[gdvHTMLList.Columns.Count - 1].Visible = false;
                            gdvHTMLList.Columns[gdvHTMLList.Columns.Count - 2].Visible = false;
                        }

                        rowApprove.Visible = false;
                        rowIsActive.Visible = false;
                    }
                }
            } 
            catch (Exception ex)
            {
                ProcessException(ex);
            }    
        }

        private bool IsSuperUser()
        {
            bool flag = false;
            string[] allowRoles = SystemSetting.SYSTEM_SUPER_ROLES;
            for (int i = 0; i < allowRoles.Length; i++)
            {
                if (Roles.IsUserInRole(GetUsername, allowRoles[i]))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private bool IsAuthenticatedForComment()
        {
            bool isAllow = false;
            string[] allowRoles = SystemSetting.SYSTEM_ROLES_ALLOW_HTMLCOMMENT; //SYSTEM_SUPER_ROLES;
            for (int i = 0; i < allowRoles.Length; i++)
            {
                if (Roles.IsUserInRole(GetUsername, allowRoles[i]))
                {
                    isAllow = true;
                    break;
                }
            }
            return isAllow;
        }    
                
        protected void imbEdit_Click(object sender, ImageClickEventArgs e)
        {
             try
             {
                 HideAll();
                 divEditWrapper.Visible = true;
                 //divViewComment.Visible = true;                 
                 BindEditor();
             }
             catch (Exception ex)
             {
                 ProcessException(ex);
             }
        }

        private void BindEditor()
        {
            try
            {
                HTMLTextDataContext db = new HTMLTextDataContext(SystemSetting.SageFrameConnectionString);
                var LINQContent = db.sp_HtmlTextGetByPortalAndUserModule(GetPortalID, Int32.Parse(hdnUserModuleID.Value), GetCurrentCultureName).SingleOrDefault();
                 if (LINQContent != null)
                 {
                     txtBody.Value = LINQContent.Content;
                     chkPublish.Checked = bool.Parse(LINQContent.IsActive.ToString());
                     chkAllowComment.Checked = bool.Parse(LINQContent.IsAllowedToComment.ToString());
                     ViewState["EditHtmlTextID"] = LINQContent.HtmlTextID;
                     divEditWrapper.Visible = true;     
                 }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (IsAuthenticatedToEdit() && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                {
                    ArrayList arrColl = null;
                    arrColl = IsContentValid(txtBody.Value.ToString());
                    if (arrColl.Count > 0 && arrColl[0].ToString().ToLower().Trim() == "true")
                    {
                        SQLHandler sq = new SQLHandler();
                        string HTMLBodyText = arrColl[1].ToString().Trim();
                        if (ViewState["EditHtmlTextID"] != null)
                        {
                            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", hdnUserModuleID.Value));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Content", HTMLBodyText));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", GetCurrentCultureName));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsAllowedToComment", chkAllowComment.Checked));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", chkPublish.Checked));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsModified", true));

                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedOn", DateTime.Now));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedBy", GetUsername));
                            sq.ExecuteNonQuery("dbo.sp_HtmlTextUpdate", ParaMeterCollection);
                            //db.sp_HtmlTextUpdate(int.Parse(SageUserModuleID), txtBody.Value.ToString(), GetCurrentCultureName, chkAllowComment.Checked, chkPublish.Checked, true, DateTime.Now, GetPortalID, GetUsername);
                            AfterSaveContent();
                            ViewState["EditHtmlTextID"] = null;
                            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("HTML", "HTMLContentIsUpdatedSuccessfully"), "", SageMessageType.Success);
                        }
                        else
                        {
                            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", hdnUserModuleID.Value));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Content", HTMLBodyText));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", GetCurrentCultureName));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsAllowedToComment", chkAllowComment.Checked));                            
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsModified", true));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", chkPublish.Checked));

                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedOn", DateTime.Now));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                            ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", GetUsername));
                            sq.ExecuteNonQuery("dbo.sp_HtmlTextAdd", ParaMeterCollection, "@HTMLTextID");
                            AfterSaveContent();
                            if (_newHTMLContentID != 0)
                            {
                                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("HTML", "HTMLContentIsAddedSuccessfully"), "", SageMessageType.Success);
                            }
                        }
                        BindComment();
                    }
                    else
                    {
						divEditWrapper.Visible = true;   
                        lblError.Text = GetSageMessage("HTML", "PleaseEnterSomeContent");
                        lblError.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private ArrayList IsContentValid(string str)
        {
            bool isValid = true;
            str = RemoveUnwantedHTMLTAG(str);
            if (str == string.Empty)
                isValid = false;
            ArrayList arrColl = new ArrayList();
            arrColl.Add(isValid);
            arrColl.Add(str);
            return arrColl;
        }

        public string RemoveUnwantedHTMLTAG(string str)
        {            
            str = System.Text.RegularExpressions.Regex.Replace(str, "<br/>$", "");
            
            str = System.Text.RegularExpressions.Regex.Replace(str, "<br />$", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, "^&nbsp;", "");
            //str = System.Text.RegularExpressions.Regex.Replace(str, "/(<form[^\>]*\>)([\s\S]*)(\<\/form\>)/i", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, "<form[^>]*>", "");            
            str = System.Text.RegularExpressions.Regex.Replace(str, "</form>", "");
            return str; //Regex.Replace(str, @"<(.|\n)*?>", string.Empty);
        }

        protected void AfterSaveContent()
        {
            try
            {
                HideAll();  
                BindContent();                             
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbAddComment_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                HideAll();

                divViewWrapper.Visible = true;
                divViewComment.Visible = false;
                divEditComment.Visible = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                SQLHandler sq = new SQLHandler();

                HTMLTextDataContext db = new HTMLTextDataContext(SystemSetting.SageFrameConnectionString);
                if (Session["EditCommentID"] != null)
                {
                    List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLCommentID", Session["EditCommentID"]));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@Comment", txtComment.Text));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsApproved", chkApprove.Checked));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", chkIsActive.Checked));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsModified", true));

                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedOn", DateTime.Now));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedBy", GetUsername));
                    sq.ExecuteNonQuery("dbo.sp_HtmlCommentUpdate", ParaMeterCollection);

                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("HTML", "CommentIsUpdatedSuccessfully"), "", SageMessageType.Success);
                }
                else
                {
                    List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                    //ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLCommentID", _newHTMLCommentID));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLTextID", hdfHTMLTextID.Value));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@Comment", txtComment.Text));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsApproved", chkApprove.Checked));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", chkIsActive.Checked));

                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedOn", DateTime.Now));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", GetUsername));
                    sq.ExecuteNonQuery("dbo.sp_HtmlCommentAdd", ParaMeterCollection, "@HTMLCommentID");

                    if (chkApprove.Checked && chkIsActive.Checked)
                    {
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("HTML", "CommentIsAddedSuccessfully"), "", SageMessageType.Success);
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("HTML", "CommentWillBeAddedAfterApproved"), "", SageMessageType.Alert);
                    }
                }

                HideAll();
                BindComment();

                divViewWrapper.Visible = true;
                divViewComment.Visible = true;

                ClearComment();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void ClearComment()
        {
            Session["EditCommentID"] = null;
            txtComment.Text = "";
            chkApprove.Checked = false;
            chkIsActive.Checked = false;

        }

        protected void gdvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gdvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToString())
                {
                    case "Edit":
                        int EditID = Int32.Parse(e.CommandArgument.ToString());
                        Edit(EditID);
                        break;
                    case "Delete":
                        int DeleteID = Int32.Parse(e.CommandArgument.ToString());
                        Delete(DeleteID);
                        break;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void Edit(int EditID)
        {
            try
            {
                SQLHandler sq = new SQLHandler();
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLCommentID", EditID));
                sp_HtmlCommentGetByHTMLCommentIDResult LINQCommentInfo = sq.ExecuteAsObject<sp_HtmlCommentGetByHTMLCommentIDResult>("dbo.sp_HtmlCommentGetByHTMLCommentID", ParaMeterCollection);
                    
                if (LINQCommentInfo != null)
                {
                    txtComment.Text = LINQCommentInfo.Comment;
                    chkApprove.Checked = (bool)LINQCommentInfo.IsApproved;
                    chkIsActive.Checked = (bool)LINQCommentInfo.IsActive;
                    Session["EditCommentID"] = EditID;
                    HideAll();
                    //rowApprove.Visible = true;
                    //rowIsActive.Visible = true;
                    divViewWrapper.Visible = true;
                    divEditComment.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void Delete(int DeleteID)
        {
            try
            {
                SQLHandler sq = new SQLHandler();
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLCommentID", DeleteID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", GetUsername));
                sq.ExecuteNonQuery("dbo.sp_HTMLCommentDeleteByCommentID", ParaMeterCollection);
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("HTML", "CommentIsDeletedSuccessfully"), "", SageMessageType.Success);
                BindComment();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("HTML", "AreYouSureToDelete") + "')");
            }            
        }
        
        protected void gdvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gdvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gdvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void imbCancel_Click(object sender, ImageClickEventArgs e)
        {
            HideAll();            
            divViewWrapper.Visible = true;
            //divViewComment.Visible = true;
            ClearComment();
        }

        protected void imbBack_Click(object sender, ImageClickEventArgs e)
        {
            HideAll();
            divViewWrapper.Visible = true;
            divViewComment.Visible = true;
            ClearComment();
        }

        //protected void cvFck_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    if ((txtBody.Value == "&nbsp;") || (txtBody.Value == "<br />") || (txtBody.Value.Length == 0))
        //    {
        //       // cvFck.ErrorMessage = "Please enter some content";
        //        cvContent.ErrorMessage = GetSageMessage("HTML", "PleaseEnterSomeContent");
        //        args.IsValid = false;
        //    }
        //    else
        //    {
        //        args.IsValid = true;
        //    }
        //}

        protected void gdvHTMLList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCustomizeEditor_Click(object sender, EventArgs e)
        {
            txtBody.ToolbarSet = "Default";
            btnCustomizeEditor.Visible = false;
            btnDefault.Visible = true;
            HideAll();
            divEditWrapper.Visible = true;
        }

        protected void btnDefault_Click(object sender, EventArgs e)
        {
            txtBody.ToolbarSet = "SageFrameLimited";
            btnCustomizeEditor.Visible = true;
            btnDefault.Visible = false;
            HideAll();
            divEditWrapper.Visible = true;
        }
    }
}