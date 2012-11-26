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
using SageFrame.Web.Utils;
using System.Text.RegularExpressions;
using SageFrame.Web;
using System.Data.Common;
using System.Data.SqlClient;
using SageFrame.Utilities;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Configuration;
using SageFrame.Templating;
using System.Web.Hosting;
using System.IO;

public partial class Install_InstallWizard : System.Web.UI.Page
{
    #region "Private Members"
    private static string connectionString = string.Empty;
    //private XmlDocument _installTemplate; 
    #endregion

    #region Protected Members
    protected bool PermissionsValid
    {
        get
        {
            bool _Valid = false;
            if ((ViewState["PermissionsValid"] != null))
            {
                _Valid = Convert.ToBoolean(ViewState["PermissionsValid"]);
            }
            return _Valid;
        }
        set { ViewState["PermissionsValid"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.ClientScript.RegisterClientScriptInclude("J1Context", ResolveUrl("~/js/jquery-1.4.4.js"));
        Page.ClientScript.RegisterClientScriptInclude("J2Context", ResolveUrl("~/js/FormValidation/jquery.validate.js"));

        if (!IsPostBack)
        {
            BindLabelText();

            //BindTemplate();
            BindConnectionString();
            TimedPanel.Visible = false;
            AddImageUrls();
            if (TestDatabaseConnection())
            {
                Initialise();
            }
        }
    }

    private void AddImageUrls()
    {
        imgDBProgress.ImageUrl = "~/Install/images/progressbar.gif";
    }

    private void BindLabelText()
    {
        //lblChooseDatabase.Text = "Select Database:";
        lblServer.Text = "Server:";
        lblServerHelp.Text = "Enter the Name or IP Address of the computer where the Database is located.";
        //lblDataBase.Text = "Database:";
        //lblDatabaseHelp.Text = "Enter the Database name";
        lblIntegrated.Text = "Integrated Security:";
        lblIntegratedHelp.Text =
            "Click on the check box, if the access to SQL server is in Windows Authentication mode. Make sure to have an existing database in Windows Authentication mode. Likewise, uncheck if the access to the server is in SQL Server Authentication mode; where you will need to fill up the username and password.";
        lblUserID.Text = "User ID:";
        lblUserHelp.Text = "Enter the UserID for the Database";
        lblPassword.Text = "Password:";
        lblPasswordHelp.Text = "Enter the Password for the Database";

        lblNewDatabaseHelp.Text = "Enter the New Database Name";
        lblExistingDatabaseHelp.Text =
            "Enter the Existing Database Name. If Database does not exist, new one is created";
        lblDatabaseNameHelp.Text = "Enter an Existing Database Name";

        //lblOwner.Text = "Run as db Owner:";
        //lblOwnerHelp.Text = "Check this if you are running the database as Database Owner - if left unchecked you will be running as the User ID specified";
    }

    //public List<TemplateInfo> GetTemplateList()
    //{
    //    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Templates"));
    //    List<TemplateInfo> lstTemplates = new List<TemplateInfo>();

    //    foreach (DirectoryInfo temp in dir.GetDirectories())
    //    {
    //        TemplateInfo tempObj = new TemplateInfo(temp.Name, temp.FullName, GetThumbPath(temp.FullName, temp.Name), false, false);
    //        lstTemplates.Add(tempObj);
    //    }
    //    return lstTemplates;
    //}

    //public string GetThumbPath(string TemplatePath, string TemplateName)
    //{
    //    string thumbpath = "../images/" + TemplateConstants.NoImageImag;
    //    if (Directory.Exists(TemplatePath + TemplateConstants.ThumbPath))
    //    {
    //        DirectoryInfo dir = new DirectoryInfo(TemplatePath + TemplateConstants.ThumbPath);
    //        foreach (FileInfo file in dir.GetFiles())
    //        {
    //            if (Utils.ValidateThumbImage(file))
    //            {
    //                thumbpath = "../Templates/" + TemplateName + TemplateConstants.ThumbPath + "/" + file.Name;
    //                break;
    //            }
    //        }
    //    }
    //    return thumbpath;
    //}


    //private void BindTemplate()
    //{
    //    List<TemplateInfo> lstTemplate = GetTemplateList();
    //    rptrTemplateList.DataSource = lstTemplate;
    //    rptrTemplateList.DataBind();
    //}
    //private void GetTemplateImages(ref List<TemplateInfo> lstTemplate)
    //{
    //    string templatePath = Request.ApplicationPath != "/" ? Request.ApplicationPath + "/Templates" : "/Templates";
    //    string absTemplatePath = Path.Combine(Request.PhysicalApplicationPath, "Templates");
    //    foreach (TemplateInfo obj in lstTemplate)
    //    {
    //        string thumbPath = CommonFunction.ReplaceBackSlash(Path.Combine(templatePath, Path.Combine(obj.TemplateName, "screenshots\\_Thumbs\\")));
    //        if (File.Exists(Path.Combine(absTemplatePath, Path.Combine(obj.TemplateName, "screenshots\\_Thumbs\\front.jpg"))))
    //        {
    //            obj.ThumbImage = CommonFunction.ReplaceBackSlash(Path.Combine(templatePath, Path.Combine(obj.TemplateName, "screenshots\\_Thumbs\\front.jpg")));
    //        }
    //        if (obj.Description.Length > 100)
    //        {
    //            obj.Description = obj.Description.Substring(0, 100);
    //        }
    //    }
    //}

    private void BindConnectionString()
    {
        string connection = SystemSetting.SageFrameConnectionString;
        string[] connectionParams = connection.Split(';');
        foreach (string connectionParam in connectionParams)
        {
            int index = connectionParam.IndexOf("=");
            if (index > 0)
            {
                string key = connectionParam.Substring(0, index);
                string value = connectionParam.Substring(index + 1);
                switch (key.ToLower())
                {
                    case "server":
                    case "data source":
                    case "address":
                    case "addr":
                    case "network address":
                        txtServer.Text = value;
                        break;
                    case "database":
                    case "initial catalog":
                        //-------------change
                        //if (chkIntegrated.Checked && txtDataBase.Text != string.Empty)
                        //{
                        //    txtDataBase.Text = value;
                        //}
                        //if (txtNewDataBaseName.Text != string.Empty)
                        //{
                        //    txtNewDataBaseName.Text = value;
                        //}
                        //if (txtExistingDatabaseName.Text != string.Empty)
                        //{
                        //    txtNewDataBaseName.Text = value;
                        //}
                        if (value != string.Empty)
                        {
                            rdbExistingDatabase.Checked= true;
                            rdbExistingDatabase.Attributes.Add("checked", "checked");
                            txtExistingDatabaseName.Text = value;
                        }
                        else
                        {
                            txtNewDataBaseName.Text = value;
                        }
                        break;
                    case "uid":
                    case "user id":
                    case "user":
                        txtUserId.Text = value;
                        break;
                    case "pwd":
                    case "password":
                        txtPassword.Text = value;
                        txtPassword.Attributes.Add("value", value);
                        Session["UserPassword"] = value;
                        break;
                    case "integrated security":
                        chkIntegrated.Checked = value.ToLower() == "true" ? true : false;
                        //chkIntegrated.Attributes.Add("checked", value.ToLower() == "true" ? "checked" : "");
                        IntegratedSecurityChange();
                        break;
                }
            }
        }
    }

    protected void chkIntegrated_CheckedChanged(object sender, EventArgs e)
    {
        IntegratedSecurityChange();
    }

    protected void IntegratedSecurityChange()
    {
        trUser.Visible = !chkIntegrated.Checked;
        trPassword.Visible = !chkIntegrated.Checked;
        trrdbCreateDatabase.Visible = !chkIntegrated.Checked;
        trnewDatabase.Visible = !chkIntegrated.Checked;
        trrdbExistingDatabase.Visible = !chkIntegrated.Checked;
        trExistingDatabase.Visible = !chkIntegrated.Checked;
        trDatabaseHeading.Visible = !chkIntegrated.Checked;

        trDatabaseName.Visible = chkIntegrated.Checked;
        //txtUserId.Text = string.Empty;
        //txtNewDataBaseName.Text = string.Empty;
        //txtExistingDatabaseName.Text = string.Empty;
        //txtPassword.Text = "";
        if (chkIntegrated.Checked)
        {
            if (txtExistingDatabaseName.Text.Trim()==string.Empty)
            {
                txtExistingDatabaseName.Text = txtDataBase.Text.Trim();
            }
            txtDataBase.Text = txtExistingDatabaseName.Text.Trim();
        }
    }

    protected void btnTestPermission_Click(object sender, EventArgs e)
    {
        BindPermissions(false);
        TestDatabaseConnection();
        if (!string.IsNullOrEmpty(Session["UserPassword"].ToString()))
        {
            txtPassword.Text = Session["UserPassword"].ToString();
            txtPassword.Attributes.Add("value", Session["UserPassword"].ToString());
            ViewState["UserPassword"] = Session["UserPassword"].ToString();
        }
    }

    private bool BindPermissions(bool test)
    {
        ///Test Strategy 1
        bool status = true;
        try
        {
            SageFrame.Application.Application app = new SageFrame.Application.Application();

            PermissionsValid = true;

            //FolderCreate
            ListItem permissionItem = new ListItem();
            if (test)
                status = VerifyFolderCreate();
            permissionItem.Selected = status;
            if (!status)
            {
            }
            permissionItem.Enabled = false;
            permissionItem.Text = "Create Folder";


            //FileCreate
            permissionItem = new ListItem();
            if (test)
                status = VerifyFileCreate();
            permissionItem.Selected = status;
            if (!status)
            {
            }
            permissionItem.Enabled = false;
            permissionItem.Text = "Create File";


            //FileDelete
            permissionItem = new ListItem();
            if (test)
                status = VerifyFileDelete();
            permissionItem.Selected = status;
            if (!status)
            {
            }
            permissionItem.Enabled = false;
            permissionItem.Text = "Delete File";


            //FolderDelete
            permissionItem = new ListItem();
            if (test)
                status = VerifyFolderDelete();
            permissionItem.Selected = status;
            if (!status)
            {
            }
            permissionItem.Enabled = false;
            permissionItem.Text = "Delete Folder";

        }
        catch (Exception ex)
        {
            throw ex;
        }

        //Test Strategy 2
        bool checkPermission = true;
        string rootDir = HttpContext.Current.Server.MapPath("~/");
        List<KeyValuePair<string, string>> dirsToCheck = new List<KeyValuePair<string, string>>();
        dirsToCheck.Add(new KeyValuePair<string, string>(rootDir, Request.ApplicationPath.ToString()));
        dirsToCheck.Add(new KeyValuePair<string, string>(rootDir + "App_GlobalResources", "App_GlobalResources"));
        dirsToCheck.Add(new KeyValuePair<string, string>(rootDir + "Install", "Install"));
        dirsToCheck.Add(new KeyValuePair<string, string>(rootDir + "XMLMessage", "XMLMessage"));
        dirsToCheck.Add(new KeyValuePair<string, string>(rootDir + "SiteMap", "SiteMap"));

        StringBuilder sb = new StringBuilder();
        sb.Append("Permission Test Failed For : ");

        List<string> fileList = new List<string>();
        foreach (KeyValuePair<string, string> kvp in dirsToCheck)
        {
            if (!CheckPermissions(kvp.Key, false, true, true, true) && checkPermission)
            {
                fileList.Add(kvp.Value);
            }
        }

        List<KeyValuePair<string, string>> filesToCheck = new List<KeyValuePair<string, string>>();
        filesToCheck.Add(new KeyValuePair<string, string>(rootDir + "web.config", "web.config"));
        foreach (KeyValuePair<string, string> kvp in filesToCheck)
        {
            if (!CheckPermissions(kvp.Key, false, true, true, true) && checkPermission)
            {
                fileList.Add(kvp.Value);
                PermissionsValid = false;
            }
        }
        if (fileList.Count > 0)
        {
            sb.Append(String.Join(",", fileList.ToArray()).ToString());
            string strPermissionsError = sb.ToString() + "<br> Your site failed the permissions check. Using Windows Explorer, browse to the root folder of the website ( {0} ). Right-click the folder and select Sharing and Security from the popup menu. Select the Security tab. Add the appropriate User Account and set the Permissions.<h3>If using Windows 2000 - IIS5</h3>The {Server}\\ASPNET User Account must have Read, Write, and Change Control of the virtual root of your website.<h3>If using Windows 2003, Windows Vista or Windows Server 2008 and  IIS6 or IIS7</h3>The NT AUTHORITY\\NETWORK SERVICE User Account must have Read, Write, and Change Control of the virtual root of your Website.<h3>If using Windows 7 or Windows Server 2008 R2 and  IIS7.5</h3>The IIS AppPool\\DefaultAppPool User Account must have Read, Write, and Change Control of the virtual root of your Website.";
            lblPermissionsError.Text = strPermissionsError.Replace("{0}", Request.ApplicationPath);
            lblPermissionsError.CssClass = "cssClasssNormalRed";

        }
        else
        {
            lblPermissionsError.Text = "Your site passed all the permissions check.";
            lblPermissionsError.CssClass = "sfSuccess";
            PermissionsValid = true;
        }
        lblPermissionsError.Visible = true;

        return PermissionsValid;
    }

    private bool VerifyFolderCreate()
    {
        string verifyPath = Server.MapPath("~/Verify");
        bool verified = true;

        //Attempt to create the Directory
        try
        {
            if (Directory.Exists(verifyPath))
            {
                Directory.Delete(verifyPath, true);
            }

            Directory.CreateDirectory(verifyPath);
        }
        catch (Exception ex)
        {
            verified = false;
            throw ex;
        }
        if (!verified)
            PermissionsValid = false;

        return verified;
    }

    private bool VerifyFolderDelete()
    {
        string verifyPath = Server.MapPath("~/Verify");
        bool verified = VerifyFolderCreate();

        if (verified)
        {
            //Attempt to delete the Directory
            try
            {
                Directory.Delete(verifyPath);
            }
            catch (Exception ex)
            {
                verified = false;
                throw ex;
            }
        }
        if (!verified)
            PermissionsValid = false;

        return verified;
    }

    private bool VerifyFileCreate()
    {
        string verifyPath = Server.MapPath("~/Verify/Verify.txt");
        bool verified = VerifyFolderCreate();

        if (verified)
        {
            //Attempt to create the File
            try
            {
                if (File.Exists(verifyPath))
                {
                    File.Delete(verifyPath);
                }

                Stream fileStream = File.Create(verifyPath);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                verified = false;
                throw ex;
            }
        }
        if (!verified)
            PermissionsValid = false;

        return verified;
    }

    private bool VerifyFileDelete()
    {
        string verifyPath = Server.MapPath("~/Verify/Verify.txt");
        bool verified = VerifyFileCreate();

        if (verified)
        {
            //Attempt to delete the File
            try
            {
                File.Delete(verifyPath);
            }
            catch (Exception ex)
            {
                verified = false;
                throw ex;
            }
        }
        if (!verified)
            PermissionsValid = false;

        return verified;
    }

    private bool CheckPermissions(string path, bool checkRead, bool checkWrite, bool checkModify, bool checkDelete)
    {
        bool flag = false;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        bool flag5 = false;
        bool flag6 = false;
        bool flag7 = false;
        bool flag8 = false;
        WindowsIdentity current = WindowsIdentity.GetCurrent();
        System.Security.AccessControl.AuthorizationRuleCollection rules = null;
        try
        {
            rules = Directory.GetAccessControl(path).GetAccessRules(true, true, typeof(SecurityIdentifier));
        }
        catch
        {
            return true;
        }
        try
        {
            foreach (FileSystemAccessRule rule in rules)
            {
                if (!current.User.Equals(rule.IdentityReference))
                {
                    continue;
                }
                if (AccessControlType.Deny.Equals(rule.AccessControlType))
                {
                    if ((FileSystemRights.Delete & rule.FileSystemRights) == FileSystemRights.Delete)
                        flag4 = true;
                    if ((FileSystemRights.Modify & rule.FileSystemRights) == FileSystemRights.Modify)
                        flag3 = true;

                    if ((FileSystemRights.Read & rule.FileSystemRights) == FileSystemRights.Read)
                        flag = true;

                    if ((FileSystemRights.Write & rule.FileSystemRights) == FileSystemRights.Write)
                        flag2 = true;

                    continue;
                }
                if (AccessControlType.Allow.Equals(rule.AccessControlType))
                {
                    if ((FileSystemRights.Delete & rule.FileSystemRights) == FileSystemRights.Delete)
                    {
                        flag8 = true;
                    }
                    if ((FileSystemRights.Modify & rule.FileSystemRights) == FileSystemRights.Modify)
                    {
                        flag7 = true;
                    }
                    if ((FileSystemRights.Read & rule.FileSystemRights) == FileSystemRights.Read)
                    {
                        flag5 = true;
                    }
                    if ((FileSystemRights.Write & rule.FileSystemRights) == FileSystemRights.Write)
                    {
                        flag6 = true;
                    }
                }
            }
            foreach (IdentityReference reference in current.Groups)
            {
                foreach (FileSystemAccessRule rule2 in rules)
                {
                    if (!reference.Equals(rule2.IdentityReference))
                    {
                        continue;
                    }
                    if (AccessControlType.Deny.Equals(rule2.AccessControlType))
                    {
                        if ((FileSystemRights.Delete & rule2.FileSystemRights) == FileSystemRights.Delete)
                            flag4 = true;
                        if ((FileSystemRights.Modify & rule2.FileSystemRights) == FileSystemRights.Modify)
                            flag3 = true;
                        if ((FileSystemRights.Read & rule2.FileSystemRights) == FileSystemRights.Read)
                            flag = true;
                        if ((FileSystemRights.Write & rule2.FileSystemRights) == FileSystemRights.Write)
                            flag2 = true;
                        continue;
                    }
                    if (AccessControlType.Allow.Equals(rule2.AccessControlType))
                    {
                        if ((FileSystemRights.Delete & rule2.FileSystemRights) == FileSystemRights.Delete)
                            flag8 = true;
                        if ((FileSystemRights.Modify & rule2.FileSystemRights) == FileSystemRights.Modify)
                            flag7 = true;
                        if ((FileSystemRights.Read & rule2.FileSystemRights) == FileSystemRights.Read)
                            flag5 = true;
                        if ((FileSystemRights.Write & rule2.FileSystemRights) == FileSystemRights.Write)
                            flag6 = true;
                    }
                }
            }
            bool flag9 = !flag4 && flag8;
            bool flag10 = !flag3 && flag7;
            bool flag11 = !flag && flag5;
            bool flag12 = !flag2 && flag6;
            bool flag13 = true;
            if (checkRead)
            {
                flag13 = flag13 && flag11;
            }
            if (checkWrite)
            {
                flag13 = flag13 && flag12;
            }
            if (checkModify)
            {
                flag13 = flag13 && flag10;
            }
            if (checkDelete)
            {
                flag13 = flag13 && flag9;
            }
            return flag13;
        }
        catch (IOException)
        {
        }
        return false;
    }

    private bool GetAccessStatus(FileSystemRights mode, FileSystemAccessRule rule)
    {
        return ((mode & rule.FileSystemRights) == mode ? true : false);
    }

    //protected void btnTestDatabaseCon_Click(object sender, EventArgs e)
    //{
    //    TestDatabaseConnection();

    //}

    private bool TestDatabaseConnection()
    {
        bool success = false;
        string DatabaseName = "";

        DbConnectionStringBuilder builder = new DbConnectionStringBuilder();

        if (!string.IsNullOrEmpty(txtServer.Text))
        {
            builder["Data Source"] = txtServer.Text;
            ViewState["DataSource"] = builder["Data Source"];
        }

        //////////////////////change/////////////////////////
        /// 
        /// 
        
        if (chkIntegrated.Checked)
        {
            builder["integrated security"] = "true";
            ViewState["IntegratedSecurity"] = builder["integrated security"];
            if (!string.IsNullOrEmpty(txtDataBase.Text))
            {
                builder["Initial Catalog"] = txtDataBase.Text;
            }
        }
        else
        {
            ViewState["IntegratedSecurity"] = null;
            if (rdbCreateDatabase.Checked && !string.IsNullOrEmpty(txtNewDataBaseName.Text))
            {
                builder["Initial Catalog"] = txtNewDataBaseName.Text;
                DatabaseName = txtNewDataBaseName.Text;
            }
            else if (rdbExistingDatabase.Checked)
            {
                builder["Initial Catalog"] = txtExistingDatabaseName.Text;
                DatabaseName = txtExistingDatabaseName.Text;
            }
        }
        if (!string.IsNullOrEmpty(txtUserId.Text))
        {
            builder["uid"] = txtUserId.Text;
            ViewState["uid"] = this.txtUserId.Text;

        }
        if (!string.IsNullOrEmpty(txtPassword.Text))
        {
            builder["pwd"] = txtPassword.Text;
            Session["UserPassword"] = builder["pwd"];
            ViewState["UserPassword"] = this.txtPassword.Text;
        }
        else
        {
            builder["pwd"] = txtPassword.Text;
            Session["UserPassword"] = builder["pwd"];
            ViewState["UserPassword"] = this.txtPassword.Text;
        }

        string owner = txtUserId.Text + ".";
            owner = "dbo.";

        if (DatabaseName != string.Empty)
        {
            connectionString = CreateConnectionString(this.TrustedConnection, ViewState["DataSource"].ToString(), "master", ViewState["uid"].ToString(), ViewState["UserPassword"].ToString(), 120);
            CreateDatabase(DatabaseName, connectionString);
            hdnConnectionStringForAll.Value = CreateConnectionString(this.TrustedConnection, ViewState["DataSource"].ToString(), DatabaseName, ViewState["uid"].ToString(), ViewState["UserPassword"].ToString(), 120);
        }
        else
        {
            connectionString = TestConnection(builder, owner);
            hdnConnectionStringForAll.Value = connectionString;
        }

        connectionString = TestConnection(builder, owner);


        if (connectionString.StartsWith("ERROR:"))
        {
            lblDataBaseError.Text = string.Format("Connection Error(s):<br/>{0}", connectionString.Replace("ERROR:", ""));
            lblDataBaseError.CssClass = "sfError";
        }
        else
        {
            success = true;
            lblDataBaseError.Text = "Connection Success";
            lblDataBaseError.CssClass = "sfSuccess";
        }
        return success;
    }

    private string TestConnection(DbConnectionStringBuilder builder, string owner)
    {
        try
        {
            string connStr = builder.ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                return connStr;
            }
        }
        catch (Exception ex)
        {
            return "ERROR:" + ex.Message;
        }
    }

    private void Initialise()
    {
        if (TestDataBaseInstalled())
        {
            //running current version, so redirect to site home page
            Response.Redirect(Page.ResolveUrl("~/" + CommonHelper.DefaultPage), true);
        }
        //else
        //{
        //    SageFrame.Application.Application app = new SageFrame.Application.Application();

        //    if (DatabaseVersion > new System.Version(0, 0, 0))
        //    {
        //        //Upgrade
        //        lblStepfirstTitle.Text = string.Format("SageFrame Upgrade Wizard - Version {0}",
        //                                               app.FormatVersion(ApplicationVersion, true));
        //        lblStepfirstDetail.Text =
        //            string.Format(
        //                "<p>SageFrame version {0} is already Installed. Click the Next button to upgrade to the new version.</p>",
        //                app.FormatVersion(DatabaseVersion, true));
        //    }
        //    else
        //    {
        //        //Install
        //        UpdateMachineKey();
        //    }
        //}
    }

    #region Database Creation

    /// <summary>
    /// DatabaseCreation 
    /// </summary>
    /// <param name="DatabaseName"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>

    public static string CreateDatabase(string DatabaseName, string connectionString)
    {
        try
        {
            //string query = string.Format("CREATE DATABASE [{0}] COLLATE SQL_Latin1_General_CP1_CI_AS", DatabaseName);
            string query = string.Format("CREATE DATABASE [{0}]", DatabaseName);
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = new SqlCommand(query, conn);
                command.ExecuteNonQuery();
            }
            return string.Empty;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string CreateConnectionString(bool trustedConnection, string serverName, string databaseName, string userName, string password, int timeout)
    {
        var builder = new SqlConnectionStringBuilder();
        builder.IntegratedSecurity = trustedConnection;
        builder.DataSource = serverName;
        builder.InitialCatalog = databaseName;
        if (!trustedConnection)
        {
            builder.UserID = userName;
            builder.Password = password;
        }
        builder.PersistSecurityInfo = false;
        builder.ConnectTimeout = timeout;
        return builder.ConnectionString;
    }

    private bool TrustedConnection
    {
        get
        {
            if (ViewState["IntegratedSecurity"] != null)
                return bool.Parse(ViewState["IntegratedSecurity"].ToString());
            return false;
        }
        set
        {
            ViewState["IntegratedSecurity"] = value;
        }
    }

    #endregion

    private static bool TestDataBaseInstalled()
    {
        bool success = false;
        string isInstalled = Config.GetSetting("IsInstalled").ToString();
        string installationDate = Config.GetSetting("InstallationDate").ToString();
        if ((isInstalled != "" && isInstalled != "false") && installationDate != "")
        {
            success = true;
        }

        return success;
    }

    private void UpdateMachineKey()
    {
        string isinstalled = Config.GetSetting("IsInstalled");

        if (isinstalled == null || string.IsNullOrEmpty(isinstalled) || isinstalled == "false" || isinstalled == "False")
        {
            string strError = Config.UpdateMachineKey();

            if (string.IsNullOrEmpty(strError))
            {
                //Force an App Restart
                Config.Touch();
                //running current version, so redirect to site home page
                System.Web.Security.FormsAuthentication.SignOut();
                // send a new request to the application to initiate website
                Response.Redirect(Page.ResolveUrl("~/" + CommonHelper.DefaultPage), true);
                //Response.Redirect(HttpContext.Current.Request.RawUrl.ToString(), true);
            }
            else
            {
                //403-3 Error - Redirect to ErrorPage
                string strURL = "~/ErrorPage.aspx?status=403_3&error=" + strError;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Server.Transfer(strURL);
            }
        }
    }

    #region Script Loader
    StringBuilder feed = new StringBuilder();

    public void RunSqlScript(string connectionString1, string actualpath, int Counter)
    {
        lock (this)
        {

            try
            {
                switch (Counter)
                {
                    case 0:
                        txtFeedback.Text += "Started running Sql Script...";
                        string[] ScriptFiles = System.IO.Directory.GetFiles(actualpath);
                        ArrayList arrColl = new ArrayList();
                        foreach (string scriptFile in ScriptFiles)
                        {
                            arrColl.Add(scriptFile);
                        }
                        Session["_SageScriptFile"] = arrColl;
                        break;
                    default:
                        ArrayList arrC = (ArrayList)Session["_SageScriptFile"];
                        string strFile = arrC[0].ToString();
                        RunGivenSQLScript(strFile, connectionString1);
                        FileInfo fileName = new FileInfo(strFile);
                        if (fileName != null)
                        {
                            string strFileName = fileName.Name;
                            txtFeedback.Text = txtFeedback.Text + Environment.NewLine + strFileName + " Script run";
                        }
                        if (arrC.Count != 0)
                        {
                            arrC.RemoveAt(0);
                        }
                        Session["_SageScriptFile"] = arrC;
                        break;
                }

            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                txtFeedback.Text += Environment.NewLine + "There was an error while running the script.";
                UpdateTimer.Enabled = false;
                lblInstallErrorOccur.Text = "Installation Error(s): There was an error while running the script.";
                lblInstallError.CssClass = "sfNormalRed";
                btnCancel.Visible = true;

            }
        }
    }

    private void RunGivenSQLScript(string scriptFile, string conn)
    {
        SqlConnection sqlcon = new SqlConnection(conn);
        sqlcon.Open();
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlcon;
        sqlcmd.CommandType = CommandType.Text;
        StreamReader reader = null;
        reader = new StreamReader(scriptFile);
        string script = reader.ReadToEnd();
        try
        {
            ExecuteLongSql(sqlcon, script);

        }
        catch (Exception ex)
        {
            btnCancel.Visible = true;
            string sss = ex.Message;
            txtFeedback.Text += Environment.NewLine + "There was an error while running the script.";
            txtFeedback.CssClass = "sfError";
            UpdateTimer.Enabled = false;
            lblInstallErrorOccur.Text = "Installation Error(s): There was an error while running the script.";
            lblInstallError.CssClass = "sfError";
            btnCancel.Visible = true;

        }
        finally
        {
            reader.Close();
            sqlcon.Close();
        }

    }

    public void ExecuteLongSql(SqlConnection connection, string Script)
    {
        string sql = "";
        sql = Script;
        Regex regex = new Regex("^GO", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        string[] lines = regex.Split(sql);
        SqlTransaction transaction = connection.BeginTransaction();
        using (SqlCommand cmd = connection.CreateCommand())
        {
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            foreach (string line in lines)
            {

                if (line.Length > 0)
                {
                    cmd.CommandText = line;
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        transaction.Commit();
    }
    #endregion

    protected void btnInstall_Click(object sender, EventArgs e)
    {
        if (TestDatabaseConnection() && BindPermissions(true))
        {
            UpdateTimer.Enabled = true;
            TimedPanel.Visible = true;
            pnlStartInstall.Visible = false;
        }
    }


    //public void UpdateTemplate()
    //{
    //    TemplateController.UpdActivateTemplate(ViewState["ActiveTemplate"].ToString(), hdnConnectionStringForAll.Value.ToString());

    //}

    public static bool IsBusy = false;
    protected void UpdateTimer_Tick(object sender, EventArgs e)
    {
        if (!hdnConnectionStringForAll.Value.Contains("ERROR:"))
        {

            if (IsBusy == false)
            {
                if (loadingDiv != null)
                {
                    loadingDiv.Visible = true;
                }
                IsBusy = true;
                if (Session["_SageScriptFile"] != null)
                {
                    ArrayList arrColl = (ArrayList)Session["_SageScriptFile"];
                    if (arrColl.Count == 0)
                    {
                        txtFeedback.Text += Environment.NewLine + "Script executed successfully ...";
                        UpdateTimer.Enabled = false;
                        IsBusy = true;
                        lblInstallError.CssClass = "sfSuccess";
                        lblInstallErrorOccur.Text = "Script executed successfully ...";
                        Config.UpdateConnectionString(hdnConnectionStringForAll.Value);
                        //UpdateTemplate();
                        UpdateMachineKey();
                        Response.Redirect(Page.ResolveUrl("~/" + CommonHelper.DefaultPage), true);
                    }
                    else
                    {
                        RunSqlScript(hdnConnectionStringForAll.Value, Server.MapPath("~/Install/Providers/DataProviders/SqlDataProvider/"), arrColl.Count);
                        IsBusy = false;
                    }
                }
                else
                {
                    RunSqlScript(hdnConnectionStringForAll.Value, Server.MapPath("~/Install/Providers/DataProviders/SqlDataProvider/"), 0);
                    IsBusy = false;
                }
            }
        }
        else
        {
            txtFeedback.Text += "There is an error while running the script.";
            UpdateTimer.Enabled = false;
            lblInstallErrorOccur.Text = "Installation Error(s): There was an error while running the script.";
            lblInstallError.CssClass = "sfError";

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
    }
}
