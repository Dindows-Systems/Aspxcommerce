using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.FileManager;
using System.IO;

public partial class Modules_FileManager_FileMgrEdit : BaseAdministrationUserControl
{
    protected string BaseDir;
    protected void Page_Load(object sender, EventArgs e)
    {
        BaseDir = GetAbsolutePath(HttpContext.Current.Request.PhysicalApplicationPath.ToString());
        BaseDir = BaseDir.Substring(0, BaseDir.Length - 1);
        Initialize();
        if (!IsPostBack)
        {
            if (Request.QueryString["d"] != null)
            {
                BindTree();
                TreeView1.Nodes[0].Selected = true;
            }
            else
            {
                BindTree();
            }
            GetRootFolders();
            LoadPagerDDL(int.Parse(ViewState["RowCount"].ToString()));
        }

    }
    protected  void Initialize()
    {
        IncludeCssFile(AppRelativeTemplateSourceDirectory + "css/popup.css");
    }
    public void LoadPagerDDL(int gridRowsCount)
    {
        ddlPageSize.Items.Clear();
        for (int i = 0; i < gridRowsCount; i += 10)
        {
            if (i == 0)
            {
                ddlPageSize.Items.Add(new ListItem("All", i.ToString(), true));
            }
            else
            {
                ddlPageSize.Items.Add(new ListItem(i.ToString(), i.ToString(), true));
            }
        }
        ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue("10"));
    }
    private void BindTree()
    {
        TreeView1.Nodes.Clear();
        string rootFolder = BaseDir;
        TreeNode rootNode = new TreeNode();
        rootNode.Text = BaseDir;
        rootNode.Expanded = true;
        rootNode.Value = rootFolder.Replace("\\", "~").Replace(" ", "|");
        TreeView1.Nodes.Add(rootNode);
        TreeView1.ShowLines = true;
        BuildTreeDirectory(rootFolder, rootNode);

    }
    public string GetAbsolutePath(string filepath)
    {
        return (FileManagerHelper.ReplaceBackSlash(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath.ToString(), filepath)));
    }
    private void BuildTreeDirectory(string dirPath, TreeNode parentNode)
    {
        string[] subDirectories = Directory.GetDirectories(dirPath);
        foreach (string directory in subDirectories)
        {
            string[] parts = directory.Split('\\');
            string name = parts[parts.Length - 1];
            TreeNode node = new TreeNode();
            node.Text = name;
            node.ImageUrl = "images/folder.gif";
            node.Expanded = false;
            parentNode.ChildNodes.Add(node);
            BuildSubDirectory(directory, node);
        }

    }
    private void BuildSubDirectory(string dirPath, TreeNode parentNode)
    {
        string[] subDirectories = Directory.GetDirectories(dirPath);

        foreach (string directory in subDirectories)
        {
            string[] parts = directory.Split('\\');
            string name = parts[parts.Length - 1];
            TreeNode node = new TreeNode();
            node.Text = name;
            node.ImageUrl = "images/folder.gif";
            parentNode.ChildNodes.Add(node);
            node.Expanded = false;
            BuildSubDirectory(directory, node);
        }

    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        this.PopUp.Hide();
        AddRootFolder(TreeView1.SelectedNode.ValuePath.ToString());

    }
    protected void btnShowPopUp_Click(object sender, EventArgs e)
    {
        this.PopUp.Show();
    }
    protected void AddRootFolder(string path)
    {
        Folder folder = new Folder();
        folder.PortalId = GetPortalID;
        folder.FolderPath = path.Replace(BaseDir + "/", "");
        folder.StorageLocation = 0;
        folder.UniqueId = Guid.NewGuid();
        folder.VersionGuid = Guid.NewGuid();
        folder.IsActive =1;
        folder.AddedBy = GetUsername;
        try
        {
            FileManagerController.AddRootFolder(folder);
            CacheHelper.Clear("FileManagerRootFolders");
            CacheHelper.Clear("FileManagerFolders");
            GetRootFolders();
        }
        catch (Exception)
        {

            throw;
        }

    }
    protected void GetRootFolders()
    {
        List<Folder> lstRootFolders = FileManagerController.GetRootFolders();
        gdvRootFolders.DataSource = lstRootFolders;
        gdvRootFolders.DataBind();
        ViewState["RowCount"] = lstRootFolders.Count;
    }
    protected void gdvRootFolders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            this.gdvRootFolders.HeaderRow.Cells[0].Visible = false;
        }
    }
    protected void gdvRootFolders_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DeleteRootFolder"))
        {
            FileManagerController.DeleteRootFolder(int.Parse(e.CommandArgument.ToString()));
            CacheHelper.Clear("FileManagerRootFolders");
            CacheHelper.Clear("FileManagerFolders");
            GetRootFolders();
        }
    }
    protected void gdvRootFolders_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvRootFolders.PageIndex = e.NewPageIndex;
        GetRootFolders();
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPageSize.SelectedValue != "0")
        {
            gdvRootFolders.AllowPaging = true;
            gdvRootFolders.PageSize = int.Parse(ddlPageSize.SelectedValue);
            gdvRootFolders.PageIndex = 0;
        }
        else
        {
            gdvRootFolders.AllowPaging = false;
        }
        GetRootFolders();
    }
    protected void chkIsActive_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        GridViewRow objItem = (GridViewRow)chk.Parent.Parent;
        int FolderID = int.Parse(gdvRootFolders.Rows[objItem.RowIndex].Cells[0].Text);
        try
        {
            FileManagerController.EnableRootFolder(FolderID,chk.Checked);
            CacheHelper.Clear("FileManagerRootFolders");
            CacheHelper.Clear("FileManagerFolders");
            GetRootFolders();
        }
        catch (Exception ex)
        {
                
            ProcessException(ex);
        }


    }
}
