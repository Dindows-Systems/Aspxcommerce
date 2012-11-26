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
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.ListManagement;
using SageFrame.Web.Utilities;
using SageFrame.Core.ListManagement;

namespace SageFrame.Modules.Admin.ControlPanel
{
    public partial class ctl_ListEditor : BaseAdministrationUserControl
    {
        //CommonFunction LToDCon = null;
        
        
        string _listName = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (!Page.IsPostBack)
                {
                    AddImageUrls();
                    GetParentList();
                    PopulateTreeRootLevel();
                    BindGridOnPageLoad();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

        }

        private void BindGridOnPageLoad()
        {
            try
            {

                bool Issystem = true;

                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", -1));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", GetCurrentCultureName));

                SQLHandler sqlH = new SQLHandler();
                List<sp_GetDefaultListResult> defaultList = sqlH.ExecuteAsList<sp_GetDefaultListResult>("dbo.sp_GetDefaultList", ParaMeterCollection);
                
                //var defaultList = dbList.sp_GetDefaultList(-1,GetCurrentCultureName);
                //{
                foreach (sp_GetDefaultListResult topList in defaultList)
                {
                    BindGrid(topList.ListName, topList.ParentKey);//culture
                    //var listSystemCheck = dbList.sp_GetListsByPortalID(-1, GetCurrentCultureName);

                    List<sp_GetListsByPortalIDResult> listSystemCheck = sqlH.ExecuteAsList<sp_GetListsByPortalIDResult>("dbo.sp_GetListsByPortalID", ParaMeterCollection);

                    foreach (sp_GetListsByPortalIDResult system in listSystemCheck)
                    {
                        if (system.ListName == topList.ListName)
                            Issystem = system.SystemList;
                    }
                    lblListName.Text = topList.ListName;
                    lblDeleteList.Text = "Delete "+topList.ListName+" List";
                    ViewState["PARENTKEY"] = topList.ParentKey;
                    ViewState["LISTNAME"] = topList.ListName;
                }


                if (Issystem == true)
                {
                    gdvSubList.Columns[4].Visible = false;

                }
                else
                {
                    gdvSubList.Columns[4].Visible = true;
                }
                lblParent.Visible = false;
                if (ViewState["LIST"] != null)
                {
                    lblEntry.Text = ViewState["LIST"].ToString() + " " + GetSageMessage("ListSettings", "Entries");
                }
                ViewMode();
                pnlListAll.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void AddImageUrls()
        {
            imgAddNewList.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            imgAddList1.ImageUrl = GetTemplateImageUrl("imgadd.png", true);
            imgDeleteList.ImageUrl = GetTemplateImageUrl("imgdelete.png", true);
            imgCancelAll.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            imgSave.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            imgCancel.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
        }

        private void PopulateTreeRootLevel()
        {
            try
            {
                //var nodeList = dbList.sp_GetListsByPortalID(-1,GetCurrentCultureName); //GetPortalID;

                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", -1));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", GetCurrentCultureName));

                SQLHandler sqlH = new SQLHandler();
                List<sp_GetListsByPortalIDResult> nodeList = sqlH.ExecuteAsList<sp_GetListsByPortalIDResult>("dbo.sp_GetListsByPortalID", ParaMeterCollection);
                
                int count = 1;
                foreach (sp_GetListsByPortalIDResult node in nodeList)
                {
                    if (node.ParentKey.ToString() == "")
                    {
                        if (node.ListName != _listName)
                        {
                            TreeNode tn = new TreeNode();
                            tn.Text = node.ListName.ToString();
                            tn.Value = node.ParentKey.ToString() + ":" + count.ToString();
                            PopulateSubLevel(tn, node.ParentKey.ToString());
                            //tn.PopulateOnDemand = true;                    
                            tvList.Nodes.Add(tn);
                            _listName = node.ListName;
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            tvList.CollapseAll();
        }
        private void PopulateSubLevel(TreeNode node, string parentKey)
        {
            try
            {
                //var nodeList = dbList.sp_GetListsByPortalID(-1,GetCurrentCultureName); //GetPortalID;

                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", -1));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", GetCurrentCultureName));

                SQLHandler sqlH = new SQLHandler();
                List<sp_GetListsByPortalIDResult> nodeList = sqlH.ExecuteAsList<sp_GetListsByPortalIDResult>("dbo.sp_GetListsByPortalID", ParaMeterCollection);

                int count = 1;
                foreach (sp_GetListsByPortalIDResult resultNode in nodeList)
                {
                    if (parentKey == "")
                    {
                        if (resultNode.ParentList == node.Text)
                        {
                            TreeNode nNode = new TreeNode();
                            nNode.Text = resultNode.Parent + ":" + resultNode.ListName.ToString();
                            nNode.Value = resultNode.ParentKey.ToString() + ":" + count;
                            PopulateSubLevel(nNode, resultNode.ParentKey.ToString());
                            node.ChildNodes.Add(nNode);
                        }
                    }
                    else
                    {
                        string[] tempNodes = SplitString(node.Text);
                        string tempNode = tempNodes[1];
                        string temp = parentKey + ":" + tempNode;
                        if (resultNode.ParentList == temp)
                        {
                            TreeNode nNode = new TreeNode();
                            nNode.Text = resultNode.Parent + ":" + resultNode.ListName.ToString();
                            nNode.Value = resultNode.ParentKey.ToString() + ":" + count;
                            PopulateSubLevel(nNode, resultNode.ParentKey.ToString());
                            node.ChildNodes.Add(nNode);

                        }
                    }
                    count = count + 1;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }               

        protected void tvList_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                string listName = tvList.SelectedNode.Text;
                string parentKey = tvList.SelectedNode.Value;
                string deleteText  = lblDeleteList.Text;
                string[] texts = deleteText.Split(' ');
                if(texts.Length>0)
                {
                    lblDeleteList.Text = texts[0] + " " + listName + " " + ((texts.Length - 1) > 0 ? texts[texts.Length - 1] : "");
                }
                if (tvList.SelectedNode.Value.Contains(":"))
                {
                    string[] parentKeys = SplitString(parentKey);
                    parentKey = parentKeys[0];
                }

                if (tvList.SelectedNode.Text.Contains(":"))
                {
                    string[] listNames = SplitString(listName);
                    listName = listNames[1];
                    lblParent.Visible = true;
                    lblParentText.Visible = true;
                }
                else
                {
                    lblParent.Visible = false;
                    lblParentText.Visible = false;
                }

                //var listSystemCheck = dbList.sp_GetListsByPortalID(-1, GetCurrentCultureName); //GetPortalID;
                //
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", -1));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", GetCurrentCultureName));

                SQLHandler sqlH = new SQLHandler();
                List<sp_GetListsByPortalIDResult> listSystemCheck = sqlH.ExecuteAsList<sp_GetListsByPortalIDResult>("dbo.sp_GetListsByPortalID", ParaMeterCollection);


                bool Issystem = true;
                foreach (sp_GetListsByPortalIDResult system in listSystemCheck)
                {
                    if (system.ListName == listName)
                        Issystem = system.SystemList;
                }
                lblListName.Text = listName;
                ViewState["PARENTKEY"] = parentKey;
                ViewState["LISTNAME"] = listName;
                BindGrid(listName, parentKey);

                if (Issystem == true)
                {
                    gdvSubList.Columns[4].Visible = false;

                }
                else
                {
                    gdvSubList.Columns[4].Visible = true;
                }
                lblParent.Text = parentKey;
                // lblEntry.Text = gdvSubList.Rows.Count.ToString() + " Entries";
                if (ViewState["LIST"] != null)
                {
                    lblEntry.Text = ViewState["LIST"].ToString() + " " + GetSageMessage("ListSettings", "Entries");
                }
                ViewMode();
                pnlListAll.Visible = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            
        }

        //Adding New List Started
        protected void imgAddNewList_Click(object sender, EventArgs e)
        {
            AddMode();
            ShowControls();
            ViewState["NEWLIST"] = "Add";
            ddlParentEntry.Items.Clear();
            ddlParentEntry.Enabled = false;
            ClearForm();
            GetParentList();
            BindTreeView();
        }

        #region "DropDown List Initialization"

        private void GetParentList()
        {
            try
            {
                ddlParentList.Items.Clear();
                //var LINQ = dbList.sp_GetListsByPortalID(-1,GetCurrentCultureName); //GetPortalID;

                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", -1));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", GetCurrentCultureName));

                SQLHandler sqlH = new SQLHandler();
                List<sp_GetListsByPortalIDResult> LINQ = sqlH.ExecuteAsList<sp_GetListsByPortalIDResult>("dbo.sp_GetListsByPortalID", ParaMeterCollection);

                if (LINQ != null)
                {
                    ddlParentList.Items.Insert(0, new ListItem("None Specified", "0"));
                    int i = 1;
                    foreach (sp_GetListsByPortalIDResult LPR in LINQ)
                    {
                        if (LPR.Parent.ToString() != "")
                        {
                            ddlParentList.Items.Insert(i, new ListItem(LPR.Parent.ToString() + ":" + LPR.ListName.ToString(), LPR.ParentKey.ToString() + ":" + i.ToString()));
                        }
                        else
                        {
                            ddlParentList.Items.Insert(i, new ListItem(LPR.ListName.ToString(), ":" + i.ToString()));
                        }
                        i++;
                    }
                    ddlParentList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void ddlParentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlParentList.SelectedIndex != 0)
                {
                    ddlParentEntry.Enabled = true;
                    string listName = string.Empty;
                    string[] listNames = SplitString(ddlParentList.SelectedItem.Text);

                    if (listNames.Length == 2)
                    {
                        listName = listNames[1];
                    }
                    else
                    {
                        listName = listNames[0];
                    }
                    string[] parentId = SplitString(ddlParentList.SelectedValue.ToString());

                    ddlParentEntry.Items.Clear();
                    //var listParentEntry = dbList.sp_GetListEntriesByNameParentKeyAndPortalID(listName, parentId[0], -1,GetCurrentCultureName); //GetPortalID;

                    List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@ListName", listName));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@ParentKey", parentId[0]));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", -1));
                    ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", GetCurrentCultureName));

                    SQLHandler sqlH = new SQLHandler();
                    List<sp_GetListEntriesByNameParentKeyAndPortalIDResult> listParentEntry = sqlH.ExecuteAsList<sp_GetListEntriesByNameParentKeyAndPortalIDResult>("dbo.sp_GetListEntriesByNameParentKeyAndPortalID", ParaMeterCollection);
                    
                    if (listParentEntry != null)
                    {

                        int i = 0;
                        foreach (sp_GetListEntriesByNameParentKeyAndPortalIDResult list in listParentEntry)
                        {
                            ddlParentEntry.Items.Insert(i, new ListItem(list.ListName.ToString() + ":" + list.Text.ToString(), list.EntryID.ToString()));
                            i++;

                        }
                    }
                    ddlParentEntry.DataBind();
                }
                else
                {
                    ddlParentEntry.Items.Clear();
                    ddlParentEntry.Enabled = false;
                }
                lblCurrencyCode.Visible = false;
                lblDisplayLocale.Visible = false;
                txtDisplayLocale.Visible = false;
                txtCurrencyCode.Visible = false;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        #endregion

        protected void imgSave_Click(object sender, EventArgs e)
        {
            try
            {
                string value = txtEntryValue.Text;
                string text = txtEntryText.Text;
                string currencyCode = txtCurrencyCode.Text.Trim();
                string displayLocale = txtDisplayLocale.Text.Trim();
                string createdBy = GetUsername;
                bool isActive = false;
                ListManagementDataContext dbList = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
                if (chkActive.Checked == true)
                {
                    isActive = true;
                }

                if (ViewState["NEWLIST"] != null)
                {
                    ViewState["NEWLIST"] = null;
                    AddNewList();
                }
                else if (ViewState["LISTNAME"] != null && ViewState["ADDSUBLIST"] != null)
                {
                    ViewState["ADDSUBLIST"] = null;
                    string listName = ViewState["LISTNAME"].ToString();
                    int parentId = 0;
                    int level = 0;
                    int definitionId = -1;
                    int portalId = -1;
                    bool displayOrder = true;
                    if (ViewState["PARENTKEY"] != null)
                    {
                        var getListDetail = dbList.sp_GetListEntriesByNameParentKeyAndPortalID(listName, ViewState["PARENTKEY"].ToString(), -1,GetCurrentCultureName);
                        foreach (sp_GetListEntriesByNameParentKeyAndPortalIDResult listDetail in getListDetail)
                        {
                            parentId = listDetail.ParentID;
                            level = listDetail.LEVEL;
                            definitionId = listDetail.DefinitionID;
                            portalId = listDetail.PortalID;
                        }
                    }
                    try
                    {
                        var insertList = dbList.sp_ListEntryAdd(listName, value, text, parentId, level, currencyCode, displayLocale, displayOrder, definitionId, "", portalId, isActive, createdBy,GetCurrentCultureName);

                        ViewMode();
                        BindGrid(ViewState["LISTNAME"].ToString(), ViewState["PARENTKEY"].ToString());
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                }
                else if (ViewState["LISTNAME"] != null && ViewState["ENTRYID"] != null)
                {

                    int entryId = int.Parse(ViewState["ENTRYID"].ToString());
                    ViewState["ENTRYID"] = null;
                    try
                    {
                        var updateList = dbList.sp_ListEntryUpdate(entryId, value, text, currencyCode, displayLocale, "", isActive, createdBy,GetCurrentCultureName);

                        ViewMode();
                        BindGrid(ViewState["LISTNAME"].ToString(), ViewState["PARENTKEY"].ToString());
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }

                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void AddNewList()
        {
            string listName = txtListName.Text.Trim();
            string value = txtEntryValue.Text.Trim();
            string text = txtEntryText.Text.Trim();
            int parentId = 0;
            int level = 0;
            int definitionId = -1;
            int portalId = -1;// GetPortalID;
            string createdBy = GetUsername;
            bool displayOrder = false;
            bool isActive = false;
            string currencyCode = txtCurrencyCode.Text.Trim();
            string displayLocale = txtDisplayLocale.Text.Trim();
            ListManagementDataContext dbList = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
            if (chkShort.Checked == true)
            {
                displayOrder = true;
            }

            if (chkActive.Checked == true)
            {
                isActive = true;
            }
            if (ddlParentList.SelectedIndex != 0)
            {
                try
                {
                    parentId = int.Parse(ddlParentEntry.SelectedValue.ToString());
                    string selectedListName = string.Empty;
                    string[] selectedListNames = SplitString(ddlParentEntry.SelectedItem.Text);
                    selectedListName = selectedListNames[0];

                    var listLevel = dbList.sp_GetListEntrybyNameValueAndEntryID(selectedListName, "", int.Parse(ddlParentEntry.SelectedValue.ToString()),GetCurrentCultureName);
                    foreach (sp_GetListEntrybyNameValueAndEntryIDResult parentLevel in listLevel)
                    {
                        level = int.Parse(parentLevel.LEVEL.ToString()) + 1;
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
            try
            {
                int ListID = ListManagementController.AddNewList(new ListInfo(listName, value, text, parentId, level, currencyCode, displayLocale, displayOrder, definitionId, "", portalId, isActive, createdBy, GetCurrentCultureName));

                if (ListID==0)
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("ListSettings", "ListAlreadyExists"), "", SageMessageType.Alert);

                }
                else
                {
                    BindTreeView();
                    ViewMode();
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ListSettings", "ListIsAddedSuccessfully"), "", SageMessageType.Success);
                    BindGridOnPageLoad();
                }


            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private string[] SplitString(string textToSplit)
        {
            string[] splitedText = textToSplit.Split(new char[] { ':' });
            return splitedText;
        }

        protected void imgAddSubList_Click(object sender, EventArgs e)
        {
            AddEditMode();
            HideControls();
            ViewState["ADDSUBLIST"] = "add";
            ClearForm();
            BindTreeView();
        }

      #region "Deleting list according to selected level"

        protected void imgDeleteList_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["LISTNAME"] != null)
                {
                    ListManagementDataContext dbList = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
                    var listByName = dbList.sp_GetListEntriesByNameParentKeyAndPortalID(ViewState["LISTNAME"].ToString(), ViewState["PARENTKEY"].ToString(), -1,GetCurrentCultureName);//GetPortalID;
                    foreach (sp_GetListEntriesByNameParentKeyAndPortalIDResult listEntry in listByName)
                    {
                        GetListByEntryId(listEntry.EntryID);
                        DeleteList(listEntry.EntryID);
                        BindTreeView();
                        pnlListAll.Visible = false;
                        BindGridOnPageLoad();
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ListSettings", "ListIsDeletedSuccessfully"), "", SageMessageType.Success);
                        

                    }
                }
                
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void UpdateDeleteLabel()
        {
                string listName = tvList.SelectedNode.Text;
                string deleteText = lblDeleteList.Text;
                string[] texts = deleteText.Split(' ');
                lblDeleteList.Text = texts[0] + " " + listName + " " + ((texts.Length - 1) > 0 ? texts[texts.Length - 1] : "");
        }

        private void GetListByEntryId(int entryId)
        {
            try
            {
                
                //var listByEntryId = dbList.sp_GetListEntrybyParentId(entryId,GetCurrentCultureName);

                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@EntryID", entryId));               
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", GetCurrentCultureName));

                SQLHandler sqlH = new SQLHandler();
                List<sp_GetListEntrybyParentIdResult> listByEntryId = sqlH.ExecuteAsList<sp_GetListEntrybyParentIdResult>("dbo.sp_GetListEntrybyParentId", ParaMeterCollection);

                foreach (sp_GetListEntrybyParentIdResult listbyParent in listByEntryId)
                {
                    GetListByEntryId(listbyParent.EntryID);
                    DeleteList(listbyParent.EntryID);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private void DeleteList(int entryId)
        {
            try
            {
                ListManagementDataContext dbList = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
                var deleteList = dbList.sp_ListEntryDeleteByID(entryId, true,GetCurrentCultureName);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        #endregion



        protected void gdvSubList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ListManagementDataContext dbList = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
                int entryId = int.Parse(e.CommandArgument.ToString());
                if (e.CommandName == "Delete")
                {
                    try
                    {
                        var deleteList = dbList.sp_ListEntryDeleteByID(entryId, true,GetCurrentCultureName);
                       // ShowMessage(SageMessageTitle.Information.ToString(), "List is deleted successfully", "", SageMessageType.Success);
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ListSettings", "ListIsDeletedSuccessfully"), "", SageMessageType.Success);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                }
                else if (e.CommandName == "SortUp")
                {
                    try
                    {
                        var sortList = dbList.sp_ListSortOrderUpdate(entryId, true,GetCurrentCultureName);
                      //  ShowMessage(SageMessageTitle.Information.ToString(), "The List is shifted up successfully", "", SageMessageType.Success);
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ListSettings", "TheListIsShiftedUpSuccessfully"), "", SageMessageType.Success);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                }
                else if (e.CommandName == "SortDown")
                {
                    try
                    {
                        var sortList = dbList.sp_ListSortOrderUpdate(entryId, false,GetCurrentCultureName);
                       // ShowMessage(SageMessageTitle.Information.ToString(), "The List is shifted downn successfully", "", SageMessageType.Success);
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ListSettings", "TheListIsShiftedDownSuccessfully"), "", SageMessageType.Success);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                }
                else if (e.CommandName == "Edit")
                {
                    try
                    {
                        HideControls();
                        ViewState["ENTRYID"] = entryId;
                        var editList = dbList.sp_GetListEntrybyNameValueAndEntryID("", "", entryId,GetCurrentCultureName);
                        foreach (sp_GetListEntrybyNameValueAndEntryIDResult getListEntry in editList)
                        {
                            txtEntryText.Text = getListEntry.Text;
                            txtEntryValue.Text = getListEntry.Value;
                            txtCurrencyCode.Text = getListEntry.CurrencyCode;
                            txtDisplayLocale.Text = getListEntry.DisplayLocale;
                            chkActive.Checked = (bool)getListEntry.IsActive;
                            AddEditMode();
                          //  ShowMessage(SageMessageTitle.Information.ToString(), "List is edited successfully", "", SageMessageType.Success);
                            //ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("ListSettings", "ListIsEditedSuccessfully"), "", SageMessageType.Success);
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                }
                if (ViewState["LISTNAME"] != null)
                {
                    BindGrid(ViewState["LISTNAME"].ToString(), ViewState["PARENTKEY"].ToString());
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void AddMode()
        {
            pnlAddList.Visible = true;
            pnlListAll.Visible = false;
        }
        private void AddEditMode()
        {
            pnlAddList.Visible = true;
            pnlViewList.Visible = false;

        }

        private void ViewMode()
        {
            pnlAddList.Visible = false;
            pnlViewList.Visible = true;
            
        }
        private void HideControls()
        {
            lblListNameText.Visible = false;
            txtListName.Visible = false;
            chkShort.Visible = false;
            ddlParentEntry.Visible = false;
            ddlParentList.Visible = false;
            lblParentEntryText.Visible = false;
            lblParentListText.Visible = false;
            lblCurrencyCode.Visible = false;
            lblDisplayLocale.Visible = false;
            txtDisplayLocale.Visible = false;
            txtCurrencyCode.Visible = false;

            lblSortOrder.Visible = false;
            if (ViewState["LISTNAME"] != null)
            {

                if (ViewState["LISTNAME"].ToString() == "Country")
                {
                    lblCurrencyCode.Visible = true;
                    lblDisplayLocale.Visible = true;
                    txtDisplayLocale.Visible = true;
                    txtCurrencyCode.Visible = true;
                }
            }
        }
        private void ShowControls()
        {
            lblListNameText.Visible = true;
            txtListName.Visible = true;
            txtDisplayLocale.Visible = false;
            txtCurrencyCode.Visible = false;
            chkShort.Visible = true;
            ddlParentEntry.Visible = true;
            ddlParentList.Visible = true;
            lblParentEntryText.Visible = true;
            lblParentListText.Visible = true;
            lblCurrencyCode.Visible = false;
            lblDisplayLocale.Visible = false;
            lblSortOrder.Visible = true;
        }

        protected void gdvSubList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void gdvSubList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void gdvSubList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");               
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("ListSettings", "WantToDelete") + "')");
                
            }
        }


        private void BindGrid(string listName,string parentKey)
        {
            try
            {                
                //var listDetail = dbList.sp_GetListEntriesByNameParentKeyAndPortalID(listName, parentKey, -1,GetCurrentCultureName);//GetPortalID;

                

                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@ListName", listName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@ParentKey", parentKey));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", -1));                
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Culture", GetCurrentCultureName));

                SQLHandler sqlH = new SQLHandler();
                DataSet ds = sqlH.ExecuteAsDataSet("dbo.sp_GetListEntriesByNameParentKeyAndPortalID", ParaMeterCollection);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    DataTable dtList = ds.Tables[0];
                    //LToDCon = new CommonFunction();
                    //dtList = LToDCon.LINQToDataTable(listDetail);
                    gdvSubList.DataSource = dtList;
                    gdvSubList.DataBind();
                    ViewState["LISTTABLE"] = dtList;
                    ViewState["LIST"] = dtList.Rows.Count;
                    if (gdvSubList.Rows.Count > 0)
                    {
                        lblEntry.Text = dtList.Rows.Count + " " + GetSageMessage("ListSettings", "Entries");
                        if (gdvSubList.PageIndex == 0)
                        {
                            gdvSubList.Rows[0].FindControl("imgListUp").Visible = false;

                        }
                        if (gdvSubList.PageIndex == (gdvSubList.PageCount - 1))
                        {
                            gdvSubList.Rows[gdvSubList.Rows.Count - 1].FindControl("imgListDown").Visible = false;
                        }

                    }
                }
                
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            
        }

        private void BindTreeView()
        {
            try
            {
                tvList.Nodes.Clear();
                PopulateTreeRootLevel();
                tvList.CollapseAll();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void OnCancelShowGrid()
        {
            try
            {
                if (ViewState["LISTTABLE"] != null)
                {
                    gdvSubList.DataSource = (DataTable)ViewState["LISTTABLE"];
                    gdvSubList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgCancel_Click(object sender, EventArgs e)
        {
            OnCancelShowGrid();
            ViewMode();
        }

        private void ClearForm()
        {
            txtCurrencyCode.Text = "";
            txtDisplayLocale.Text = "";
            txtEntryText.Text = "";
            txtEntryValue.Text = "";
            txtListName.Text = "";
            chkActive.Checked = false;
            chkShort.Checked = false;
        }

        protected void imgCancelAll_Click(object sender, EventArgs e)
        {
            pnlListAll.Visible = false;
            BindTreeView();
        }




        #region "Pager Region" 

        protected void gdvSubList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvSubList.PageIndex = e.NewPageIndex;
            BindGridForSameList();
            
        }        
        
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGridPageSize.SelectedValue != "0")
            {
                gdvSubList.AllowPaging = true;
                gdvSubList.PageSize = int.Parse(ddlGridPageSize.SelectedValue);
                gdvSubList.PageIndex = 0;
            }
            else
            {
                gdvSubList.AllowPaging = false;
            }
            BindGridForSameList();


        }


        private void BindGridForSameList()
        {
            if (ViewState["LISTNAME"] != null && ViewState["PARENTKEY"] != null)
            {
                BindGrid(ViewState["LISTNAME"].ToString(), ViewState["PARENTKEY"].ToString());
            }
        }


        #endregion
    }
}