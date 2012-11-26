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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Collections;
using System.Xml.XPath;
using System.Xml;
using SageFrame.Modules;
using SageFrame.ModuleControls;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using RegisterModule;
using SageFrame.Permission;


namespace SageFrame.SageFrameClass.Services
{
    public class Installers : BaseAdministrationUserControl
    {
        #region "Private Members"

        ModulesDataContext db = new ModulesDataContext(SystemSetting.SageFrameConnectionString);
        System.Nullable<Int32> _newModuleID = 0;
        System.Nullable<Int32> _newModuleDefID = 0;
        System.Nullable<Int32> _moduleControlID = 0;
        System.Nullable<Int32> _newPortalmoduleID = 0;
        PermissionDataContext dbPermission = new PermissionDataContext(SystemSetting.SageFrameConnectionString);
        System.Nullable<Int32> _newModuleDefPermissionID = 0;
        System.Nullable<Int32> _newPortalModulePermissionID = 0;

        string Exceptions = string.Empty;

        public enum ControlType
        {
            View = 1,
            Edit = 2,
            Setting = 3
        }

        #endregion

        #region "Public Properties"

        #endregion

        public Installers()
        {

        }

        /// <summary>
        /// index 0 will contain integer part of the function
        /// index 1 will contain module object
        /// </summary>
        /// <param name="fileModule"></param>
        /// <returns></returns>
        public ArrayList Step0CheckLogic(FileUpload fileModule)
        {
            ModuleInfo module = new ModuleInfo();
            int ReturnValue = 0;
            try
            {
                if (fileModule.HasFile)//check for Empty entry
                {
                    if (IsVAlidZipContentType(fileModule.FileName))//Check if valid Zip file submitted
                    {
                        string path = HttpContext.Current.Server.MapPath("~/");
                        string temPath = SageFrame.Core.RegisterModule.Common.TemporaryFolder;
                        string destPath = Path.Combine(path, temPath);
                        if (!Directory.Exists(destPath))
                            Directory.CreateDirectory(destPath);

                        string filePath = destPath + "\\" + fileModule.FileName;
                        fileModule.SaveAs(filePath);
                        string ExtractedPath = string.Empty;
                        ZipUtil.UnZipFiles(filePath, destPath, ref ExtractedPath, SageFrame.Core.RegisterModule.Common.Password, SageFrame.Core.RegisterModule.Common.RemoveZipFile);
                        module.TempFolderPath = ExtractedPath;
                        fileModule.FileContent.Dispose();
                        if (!string.IsNullOrEmpty(module.TempFolderPath) && Directory.Exists(module.TempFolderPath))
                        {
                            switch (Step1CheckLogic(module.TempFolderPath, module))
                            {
                                case 0://No manifest file
                                    DeleteTempDirectory(module.TempFolderPath);
                                    ReturnValue = 3;
                                    break;
                                case -1://Invalid Manifest file
                                    DeleteTempDirectory(module.TempFolderPath);
                                    ReturnValue = 4;
                                    break;
                                case 1://Already exist
                                    ReturnValue = 2;
                                    break;
                                case 2://Fresh Installation
                                    ReturnValue = 1;
                                    break;
                            }
                        }
                        else
                        {
                            ReturnValue = 0;
                        }
                    }
                    else
                    {
                        ReturnValue = -1;//"Invalid Zip file submited to upload!";
                    }
                }
                else
                {
                    ReturnValue = 0;// "No package file is submited to upload!";
                }
            }
            catch
            {
                ReturnValue = -1;
            }
            ArrayList arrColl = new ArrayList();
            arrColl.Add(ReturnValue);
            arrColl.Add(module);
            return arrColl;
        }

        public bool checkValidManifestFile(XmlElement root, ModuleInfo module)
        {
            if (root.Name == "sageframe")//need to change the root node for valid manifest file at root node  
            {
                string PackageType = root.GetAttribute("type"); //root.NodeType
                module.PackageType = PackageType;
                switch (PackageType.ToLower())
                {
                    //need to check for many cases for like skin /..
                    case "module":
                        return true;
                }
            }
            return false;
        }

        public bool IsModuleExist(string moduleName, ModuleInfo module)
        {
            var LINQModule = db.usp_ModuleGetAllExisting();
            CommonFunction LToDCon = new CommonFunction();
            DataTable dt1 = LToDCon.LINQToDataTable(LINQModule);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i]["ModuleName"].ToString().ToLower() == moduleName)
                {
                    module.ModuleID = Int32.Parse(dt1.Rows[i]["ModuleID"].ToString());
                    return true;
                }
            }
            return false;
        }

        public int Step1CheckLogic(string TempUnzippedPath, ModuleInfo module)
        {
            if (checkFormanifestFile(TempUnzippedPath, module) != "")
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(TempUnzippedPath + '\\' + module.ManifestFile);
                XmlElement root = doc.DocumentElement;
                if (checkValidManifestFile(root, module))
                {
                    XmlNodeList xnList = doc.SelectNodes("sageframe/folders/folder");
                    foreach (XmlNode xn in xnList)
                    {
                        module.ModuleName = xn["modulename"].InnerXml.ToString();
                        module.FolderName = xn["foldername"].InnerXml.ToString();
                        if (!String.IsNullOrEmpty(module.ModuleName) && IsModuleExist(module.ModuleName.ToLower(), module))
                        {
                            string path = HttpContext.Current.Server.MapPath("~/");
                            string targetPath = path + SageFrame.Core.RegisterModule.Common.ModuleFolder + '\\' + module.FolderName;
                            module.InstalledFolderPath = targetPath;
                            return 1;//Already exist
                        }
                        else
                        {
                            return 2;//Not Exists
                        }
                    }
                }
                else
                {
                    return -1;//Invalid Manifest file
                }
            }
            return 0;//No manifest file
        }

        public string checkFormanifestFile(string TempUnzippedPath, ModuleInfo module)
        {
            DirectoryInfo dir = new DirectoryInfo(TempUnzippedPath);
            foreach (FileInfo f in dir.GetFiles("*.*"))
            {
                if (f.Extension.ToLower() == ".sfe")
                {
                    module.ManifestFile = f.Name;
                    return module.ManifestFile;
                }
                else
                {
                    module.ManifestFile = "";
                }
            }
            return module.ManifestFile;
        }

        private bool IsVAlidZipContentType(string p)
        {
            // extract and store the file extension into another variable
            String fileExtension = p.Substring(p.Length - 3, 3);
            // array of allowed file type extensions
            string[] validFileExtensions = { "zip" };
            var flag = false;
            // loop over the valid file extensions to compare them with uploaded file
            for (var index = 0; index < validFileExtensions.Length; index++)
            {
                if (fileExtension.ToLower() == validFileExtensions[index].ToString().ToLower())
                {
                    flag = true;
                }
            }
            return flag;
        }

        private int checkControlType(string _controlType)
        {
            int returnValue = 0;
            switch (_controlType)
            {
                case "View":
                    returnValue = (int)ControlType.View;
                    break;
                case "Edit":
                    returnValue = (int)ControlType.Edit;
                    break;
                case "Setting":
                    returnValue = (int)ControlType.Setting;
                    break;
                default:
                    returnValue = 0;
                    break;
            }
            return returnValue;
        }

        public void ModulesRollBack(int ModuleID, int PortalID)
        {
            try
            {
                SQLHandler objSQL = new SQLHandler();
                objSQL.ModulesRollBack(ModuleID, PortalID);
            }
            catch (Exception e)
            {
                ProcessException(e);
            }
        }

        public string ReadSQLFile(string TempUnzippedPath, string _sqlProvidername)
        {
            string Exceptions = string.Empty;
            try
            {
                StreamReader objReader = new StreamReader(TempUnzippedPath + '\\' + _sqlProvidername);
                string sLine = "";
                string scriptDetails = "";
                ArrayList arrText = new ArrayList();

                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        arrText.Add(sLine);
                }
                objReader.Close();
                foreach (string sOutput in arrText)
                {
                    scriptDetails += sOutput + "\r\n";
                }
                SQLHandler sqlHandler = new SQLHandler();
                Exceptions = sqlHandler.ExecuteScript(scriptDetails, true);
            }
            catch (Exception ex)
            {
                Exceptions += ex.Message.ToString();
            }
            return Exceptions;
        }

        public ModuleInfo fillModuleInfo(ModuleInfo module)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(module.TempFolderPath + '\\' + module.ManifestFile);
            XmlElement root = doc.DocumentElement;
            if (!String.IsNullOrEmpty(root.ToString()))
            {
                XmlNodeList xnList = doc.SelectNodes("sageframe/folders/folder");
                foreach (XmlNode xn in xnList)
                {
                    module.ModuleName = xn["modulename"].InnerXml.ToString();
                    module.Name = xn["name"].InnerXml.ToString();
                    module.FriendlyName = xn["friendlyname"].InnerXml.ToString();
                    module.Description = xn["description"].InnerXml.ToString();
                    module.Version = xn["version"].InnerXml.ToString();
                    module.BusinessControllerClass = xn["businesscontrollerclass"].InnerXml.ToString();
                    module.FolderName = xn["foldername"].InnerXml.ToString();
                    module.CompatibleVersions = xn["compatibleversions"].InnerXml.ToString();
                    module.Owner = xn["owner"].InnerXml.ToString();
                    module.Organization = xn["organization"].InnerXml.ToString();
                    module.URL = xn["url"].InnerXml.ToString();
                    module.Email = xn["email"].InnerXml.ToString();
                    module.ReleaseNotes = xn["releasenotes"].InnerXml.ToString();
                    module.License = xn["license"].InnerXml.ToString();
                }
            }
            return module;
        }

        public void InstallPackage(ModuleInfo module)
        {
            XmlDocument doc = new XmlDocument();
            ArrayList dllFiles = new ArrayList();
            string _unistallScriptFile = string.Empty;
            doc.Load(module.TempFolderPath + '\\' + module.ManifestFile);
            XmlElement root = doc.DocumentElement;
            if (!String.IsNullOrEmpty(root.ToString()))
            {
                XmlNodeList xnList = doc.SelectNodes("sageframe/folders/folder");
                foreach (XmlNode xn in xnList)
                {
                    #region Module Exist check
                    try
                    {
                        #region "Module Creation Logic"
                        // add into module table
                        var LINQModuleInfo = db.sp_ModulesAdd(ref _newModuleID, ref _newModuleDefID, module.Name, module.PackageType, module.License, module.Owner, module.Organization, module.URL, module.Email, module.ReleaseNotes, module.FriendlyName, module.Description, module.Version, true, false, module.BusinessControllerClass, module.FolderName, module.ModuleName, 0, module.CompatibleVersions, "", "", 0, true, DateTime.Now, GetPortalID, GetUsername);
                        module.ModuleID = (Int32)_newModuleID;

                        //insert into ProtalModule table
                        db.sp_PortalModulesAdd(ref _newPortalmoduleID, GetPortalID, _newModuleID, true, DateTime.Now, GetUsername);

                        //install permission for the installed module in ModuleDefPermission table with ModuleDefID and PermissionID
                        try
                        {
                            // get the default module VIEW permissions
                            var LINQModuleViewPermission = dbPermission.sp_GetPermissionByCodeAndKey("SYSTEM_VIEW", "VIEW").SingleOrDefault();
                            int _permissionIDView = LINQModuleViewPermission.PermissionID;
                            //insert into module permissions i.e., ModuleDefPermission and PortalModulePermission
                            dbPermission.sp_ModulesPermissionAdd(ref _newModuleDefPermissionID, _newModuleDefID, _permissionIDView, GetPortalID, ref _newPortalModulePermissionID, _newPortalmoduleID, true, null, true, DateTime.Now, GetUsername);


                            // get the default module EDIT permissions
                            var LINQModuleEditPermission = dbPermission.sp_GetPermissionByCodeAndKey("SYSTEM_EDIT", "EDIT").SingleOrDefault();
                            int _permissionIDEdit = LINQModuleEditPermission.PermissionID;
                            //insert into module permissions i.e., ModuleDefPermission and PortalModulePermission
                            dbPermission.sp_ModulesPermissionAdd(ref _newModuleDefPermissionID, _newModuleDefID, _permissionIDEdit, GetPortalID, ref _newPortalModulePermissionID, _newPortalmoduleID, true, null, true, DateTime.Now, GetUsername); 
                        }
                        catch (Exception ex)
                        {
                            Exceptions += ex.Message;
                            break;
                        }       

                        XmlNodeList xnList2 = doc.SelectNodes("sageframe/folders/folder/modules/module/controls/control");
                        foreach (XmlNode xn2 in xnList2)
                        {
                            string _moduleControlKey = null;
                            if (xn2["key"] != null)
                            {
                                _moduleControlKey = xn2["key"].InnerXml;// exists
                            }
                            string _moduleControlTitle = xn2["title"].InnerXml;
                            string _moduleControlSrc = xn2["src"].InnerXml;
                            string _controlType = xn2["type"].InnerXml;
                            string _moduleControlHelpUrl = xn2["helpurl"].InnerXml;
                            bool _moduleSupportsPartialRendering = false;
                            if (xn2["supportspartialrendering"] != null)
                            {
                                string _moduleControlSupportsPartialRendering = xn2["supportspartialrendering"].InnerXml;

                                if (_moduleControlSupportsPartialRendering == "true")
                                {
                                    _moduleSupportsPartialRendering = true;
                                }
                            }
                            int controlType = 0;
                            controlType = checkControlType(_controlType);
                            //add into module control table
                            ModuleControlsDataContext dbMC = new ModuleControlsDataContext(SystemSetting.SageFrameConnectionString);
                            dbMC.sp_ModuleControlsAdd(ref _moduleControlID, _newModuleDefID, _moduleControlKey, _moduleControlTitle, _moduleControlSrc, null, controlType, 0, _moduleControlHelpUrl, _moduleSupportsPartialRendering, true, DateTime.Now, GetPortalID, GetUsername);
                        }
                        XmlNodeList xnList3 = doc.SelectNodes("sageframe/folders/folder/files/file");
                        if (xnList3.Count != 0)
                        {
                            foreach (XmlNode xn3 in xnList3)
                            {
                                string _fileName = xn3["name"].InnerXml;
                                try
                                {
                                    #region CheckValidDataSqlProvider
                                    if (!String.IsNullOrEmpty(_fileName) && _fileName.Contains(module.Version + ".SqlDataProvider"))
                                    {
                                        Exceptions = ReadSQLFile(module.TempFolderPath, _fileName);
                                    }
                                    #endregion

                                    #region CheckAlldllFiles
                                    if (!String.IsNullOrEmpty(_fileName) && _fileName.Contains(".dll"))
                                    {
                                        dllFiles.Add(_fileName);
                                    }
                                    #endregion
                                    #region ReadUninstall SQL FileName
                                    if (!String.IsNullOrEmpty(_fileName) && _fileName.Contains("Uninstall.SqlDataProvider"))
                                    {
                                        _unistallScriptFile = _fileName;
                                    }
                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    Exceptions += ex.Message;
                                    break;
                                }
                            }
                        }

                        if (Exceptions != string.Empty)
                        {
                            if (module.ModuleID.ToString() != null && module.ModuleID > 0 && _newModuleDefID != null && _newModuleDefID > 0)
                            {
                                //Run unstallScript
                                if (_unistallScriptFile != "")
                                {
                                    Exceptions = ReadSQLFile(module.TempFolderPath, _unistallScriptFile);
                                }
                                //Delete Module info from data base
                                ModulesRollBack(module.ModuleID, GetPortalID);
                                module.ModuleID = -1;
                            }
                        }
                        #endregion
                    }
                    catch
                    {
                        if (module.ModuleID.ToString() != null && module.ModuleID > 0 && _newModuleDefID != null && _newModuleDefID > 0)
                        {
                            //Run unstallScript
                            if (_unistallScriptFile != "")
                            {
                                Exceptions = ReadSQLFile(module.TempFolderPath, _unistallScriptFile);
                            }
                            //Delete Module info from data base
                            ModulesRollBack(module.ModuleID, GetPortalID);
                            module.ModuleID = -1;
                        }
                    }
                    #endregion
                }
            }

            if (module.ModuleID.ToString() != null && module.ModuleID > 0 && Exceptions == string.Empty)
            {
                string path = HttpContext.Current.Server.MapPath("~/");
                string targetPath = path + SageFrame.Core.RegisterModule.Common.ModuleFolder + '\\' + module.FolderName;
                CopyDirectory(module.TempFolderPath, targetPath);
                for (int i = 0; i < dllFiles.Count; i++)
                {
                    string sourcedllFile = module.TempFolderPath + '\\' + dllFiles[i].ToString();
                    string targetdllPath = path + SageFrame.Core.RegisterModule.Common.DLLTargetPath + '\\' + dllFiles[i].ToString();
                    File.Copy(sourcedllFile, targetdllPath, true);
                    //File.Move();
                }
            }
            DeleteTempDirectory(module.TempFolderPath);
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

        public void DeleteTempDirectory(string TempDirectory)
        {
            try
            {
                if (!string.IsNullOrEmpty(TempDirectory))
                {
                    if (Directory.Exists(TempDirectory))
                        Directory.Delete(TempDirectory, true);
                }
            }
            catch (IOException ex)
            {
                throw ex;//cant delete folder
            }
        }
    }
}