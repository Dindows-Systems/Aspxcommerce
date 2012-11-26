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
using SageFrame.Framework;
using SageFrame.Web;
using SageFrame.Template;
using System.IO;
using RegisterModule;
using SageFrame.Core.TemplateManagement;
using System.Web.UI.HtmlControls;
using SageFrame.Shared;
using SageFrame.PortalSetting;
using SageFrame.Setting;
public partial class Modules_Admin_TemplateManagement_ctl_TemplateManagement : BaseAdministrationUserControl
{
    TemplateDataContext dbTemplate = new TemplateDataContext(SystemSetting.SageFrameConnectionString);
    public string UnexpectedEOF = "Unexpected EOF";
    public string TemplateURL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindTemplate();
                PanelVisibility(false, true);
                Initialize();
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }


    private void Initialize()
    {
        AddImageUrls();
        IncludeCssFile(AppRelativeTemplateSourceDirectory + "css/module.css");
        IncludeCssFile(AppRelativeTemplateSourceDirectory + "css/popup.css");
        string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "TemplateManagementVariable", " var TemplateMgrVar='" + ResolveUrl(modulePath) + "';", true);
        TemplateURL = GetTemplateImageUrl("close.gif", false);
    }

    private bool DeleteTemplate(Int32 templateID)
    {
        var result = dbTemplate.sp_TemplateDelete(templateID, string.Empty, GetPortalID, GetUsername).SingleOrDefault();
        if (result != null)
        {
            if (result.ReturnCode > 0)
            {
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("TemplateManagement", "TemplateDeletedSuccessfully"), "", SageMessageType.Success);
                return true;
            }
            else
            {
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("TemplateManagement", "TemplateIsInUse"), "", SageMessageType.Alert);
                return false;
            }
        }
        else
        {
            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("TemplateManagement", "UnknownError"), "", SageMessageType.Error);
            return false;
        }
    }

    private void AddImageUrls()
    {
        imgAddTemplate.ImageUrl = GetTemplateImageUrl("imgaddtemplate.png", true);
        imgInstall.ImageUrl = GetTemplateImageUrl("imginstall.png", true);
        imgCancel.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
    }

    private void BindTemplate()
    {
        List<TemplateInfo> lstTemplate = TemplateController.GetTemplateList(GetPortalID, GetUsername);
        GetTemplateImages(ref lstTemplate);
        rptrTemplates.DataSource = lstTemplate;
        rptrTemplates.DataBind();

    }
    private void GetTemplateImages(ref List<TemplateInfo> lstTemplate)
    {
        string templatePath =  Request.ApplicationPath!="/"?Request.ApplicationPath+"/Templates":"/Templates";
        string absTemplatePath = Path.Combine(Request.PhysicalApplicationPath, "Templates");
        foreach (TemplateInfo obj in lstTemplate)
        {
            string thumbPath = CommonFunction.ReplaceBackSlash(Path.Combine(templatePath, Path.Combine(obj.TemplateTitle, "screenshots\\_Thumbs\\")));
            if (File.Exists(Path.Combine(absTemplatePath, Path.Combine(obj.TemplateTitle, "screenshots\\_Thumbs\\front.jpg"))))
            {
                obj.ThumbNail = CommonFunction.ReplaceBackSlash(Path.Combine(templatePath, Path.Combine(obj.TemplateTitle, "screenshots\\_Thumbs\\front.jpg")));
            }
            else
            {
                obj.ThumbNail = GetRandomImage(Path.Combine(templatePath, obj.TemplateTitle).ToString(), thumbPath);
            }

            if (obj.Description.Length > 100)
            {
                obj.Description = obj.Description.Substring(0, 100);
            }
        }
    }

    private string GetRandomImage(string directoryPath, string folderPath)
    {
        DirectoryInfo dir = new DirectoryInfo(Path.Combine(directoryPath, "screenshots\\_Thumbs"));
        string filePath = "";
        if (Directory.Exists(Path.Combine(directoryPath, "screenshots\\_Thumbs")))
        {
            foreach (FileInfo file in dir.GetFiles())
            {
                filePath = folderPath + file.Name;
                break;
            }
        }
        if (filePath == "")
        {
            filePath = "/" + TemplateSettings.BaseDir + "/images/noimage.jpg";
        }
        return filePath;
    }


    protected void imgInstall_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (fileTemplateZip.HasFile && fileTemplateZip.PostedFile.FileName != string.Empty)
            {
                string fileName = fileTemplateZip.PostedFile.FileName;
                string cntType = fileTemplateZip.PostedFile.ContentType;
                if (fileName.Substring(fileName.Length - 3, 3).ToLower() == "zip")
                {
                    string path = HttpContext.Current.Server.MapPath("~/");
                    string temPath = SageFrame.Core.RegisterModule.Common.TemporaryFolder;
                    string destPath = Path.Combine(path, temPath);
                    string downloadPath = SageFrame.Core.RegisterModule.Common.TemporaryTemplateFolder;
                    string downloadDestPath = Path.Combine(path, downloadPath);
                    string templateName = ParseFileNameWithoutPath(fileName.Substring(0, fileName.Length - 4));
                    string templateFolderPath = path + "Templates\\" + templateName;
                    if (!Directory.Exists(templateFolderPath))
                    {
                        if (!Directory.Exists(destPath))
                            Directory.CreateDirectory(destPath);

                        string filePath = destPath + "\\" + fileTemplateZip.FileName;
                        fileTemplateZip.SaveAs(filePath);
                        string ExtractedPath = string.Empty;
                        ZipUtil.UnZipFiles(filePath, destPath, ref ExtractedPath, SageFrame.Core.RegisterModule.Common.Password, SageFrame.Core.RegisterModule.Common.RemoveZipFile);
                        string[] dirs = Directory.GetDirectories(ExtractedPath);
                        bool foundCSSFolder = false;
                        bool foundImagesFolder = false;
                        bool foundTemplateCss = false;
                        bool foundLayoutCss = false;
                        foreach (string dir in dirs)
                        {
                            string folderName = ParseFileNameWithoutPath(dir);
                            if (folderName.ToLower() == "css")
                            {
                                foundCSSFolder = true;
                                string[] files = Directory.GetFiles(dir);
                                foreach (string file in files)
                                {
                                    if (file.Contains("template.css"))
                                    {
                                        foundTemplateCss = true;
                                    }
                                    if (file.Contains("layout.css"))
                                    {
                                        foundLayoutCss = true;
                                    }
                                }
                            }
                            if (folderName.ToLower() == "images")
                            {
                                foundImagesFolder = true;
                            }
                        }
                        if (foundCSSFolder && foundImagesFolder && foundTemplateCss && foundLayoutCss)
                        {
                            if (!Directory.Exists(downloadDestPath))
                                Directory.CreateDirectory(downloadDestPath);
                            fileTemplateZip.SaveAs(downloadDestPath + "\\" + fileTemplateZip.FileName);

                            Directory.Move(ExtractedPath, templateFolderPath);
                            TemplateController.AddTemplate(new TemplateInfo(templateName, txtAuthor.Text, txtTemplateDesc.Text, txtAuthorURL.Text, GetPortalID, GetUsername));
                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("TemplateManagement", "TemplateInstallSuccessfully"), "", SageMessageType.Success);
                            BindTemplate();
                            //PanelVisibility(false, true);
                            Response.Redirect(Request.Url.OriginalString);
                        }
                        else
                        {
                            Directory.Delete(destPath, true);
                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("TemplateManagement", "InvalidTemplate"), "", SageMessageType.Alert);
                        }
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("TemplateManagement", "TemplateAlreadyInstall"), "", SageMessageType.Error);
                    }
                }
                else
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("TemplateManagement", "InvalidTemplateZip"), "", SageMessageType.Alert);
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Equals(UnexpectedEOF, StringComparison.OrdinalIgnoreCase))
            {
                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("TemplateManagement", "ZipFileIsCorrupt"), "", SageMessageType.Alert);

            }
            else
            {
                ProcessException(ex);
            }

        }
    }
    private static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
    {
        bool ret = false;
        try
        {
            SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
            DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

            if (Directory.Exists(SourcePath))
            {
                if (Directory.Exists(DestinationPath) == false)
                    Directory.CreateDirectory(DestinationPath);
                foreach (string fls in Directory.GetFiles(SourcePath))
                {
                    FileInfo flinfo = new FileInfo(fls);
                    flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                }
                foreach (string drs in Directory.GetDirectories(SourcePath))
                {
                    DirectoryInfo drinfo = new DirectoryInfo(drs);
                    if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                        ret = false;
                }
            }
            ret = true;
        }
        catch
        {
            ret = false;
        }
        return ret;
    }
    private void CopyDirectory(string SourceDirectory, string DestinationDirectory)
    {
        if (Directory.Exists(SourceDirectory))
        {
            string[] files = Directory.GetFiles(SourceDirectory);
            if (!Directory.Exists(DestinationDirectory))
            {
                Directory.CreateDirectory(DestinationDirectory);
            }
            foreach (string s in files)
            {
                string fileName = Path.GetFileName(s);
                string destFile = Path.Combine(DestinationDirectory, ParseFileNameWithoutPath(fileName));
                File.Copy(s, destFile, true);
            }
            string[] directories = Directory.GetDirectories(SourceDirectory);
            foreach (string d in directories)
            {
                char splitter = '\\';
                string[] directory = d.Split(splitter);
                string directoryName = directory[directory.Length - 1];
                string destDirectory = Path.Combine(DestinationDirectory, directoryName);
                CopyDirectory(d, destDirectory);
            }
        }
    }
    private string ParseFileNameWithoutPath(string path)
    {
        if (path != null && path != string.Empty)
        {
            char seperator = '\\';
            string[] file = path.Split(seperator);
            return file[file.Length - 1];
        }
        return string.Empty;
    }
    protected void imgCancel_Click(object sender, ImageClickEventArgs e)
    {
        //PanelVisibility(false, true);
        Response.Redirect(Request.Url.OriginalString);
    }

    protected void imgAddTemplate_Click(object sender, ImageClickEventArgs e)
    {
        PanelVisibility(true, false);
    }

    public void PanelVisibility(bool IsVisibleTemplate, bool IsVisibleTemplateList)
    {
        pnlTemplate.Visible = IsVisibleTemplate;
        pnlTemplateList.Visible = IsVisibleTemplateList;
    }

    private string strDeleteAllert = string.Empty;
    public string DeleteAllert
    {
        get
        {
            if (strDeleteAllert == string.Empty)
            {
                strDeleteAllert = GetSageMessage("TemplateManagement", "AreYouSureToDelete");
            }
            return strDeleteAllert;
        }
    }



    protected void rptrTemplates_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lbl = (Label)e.Item.FindControl("lblTemplateTitle");
        HtmlGenericControl div = e.Item.FindControl("divMain") as HtmlGenericControl;
        SageFrameConfig sfConfig = new SageFrameConfig();
        string ExistingTemplate = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalCssTemplate);
        if (lbl.Text.Equals(ExistingTemplate))
        {

            div.Attributes.Remove("class");
            div.Attributes.Add("class", "divTemplateActive");
        }
        else
        {
            div.Attributes.Remove("class");
            div.Attributes.Add("class", "divTemplate");
        }

        LinkButton lb = e.Item.FindControl("lnkBtnDelete") as LinkButton;
        lb.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("TemplateManagement", "AreYouSureToDelete") + "')");



    }



    protected void rptrTemplates_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.Equals("Activate"))
        {
            int TemplateID = int.Parse(e.CommandArgument.ToString());
            Label lbl = (Label)e.Item.FindControl("lblTemplateTitle");
            SettingProvider sageSP = new SettingProvider();
            sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.PortalCssTemplate, lbl.Text, GetUsername, GetPortalID.ToString());
            SageFrameConfig sfConfig = new SageFrameConfig();
            sfConfig.ResetSettingKeys(GetPortalID);
            BindTemplate();
        }
        else if (e.CommandName.Equals("DeleteTemplate"))
        {
            string[] args = e.CommandArgument.ToString().Split('_');
            string templateName = args[0];
            if (templateName.ToLower() == "default")
            {
                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("TemplateManagement", "TemplateCannotDeleted"), "", SageMessageType.Alert);
            }
            else
            {
                if (DeleteTemplate(Int32.Parse(args[1].ToString())))
                {
                    string templateFolderPath = HttpContext.Current.Server.MapPath("~/") + "Templates\\" + args[0];
                    DeleteDirectory(templateFolderPath);
                    BindTemplate();
                    PanelVisibility(false, true);
                }
            }
        }
    }

    public static bool DeleteDirectory(string target_dir)
    {
        bool result = false;
        if (Directory.Exists(target_dir))
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        return result;
    }
}

