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
using SageFrame.SageFrameClass.Services;
using System.Collections;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using System.Xml;
using RegisterModule;
using System.IO;

namespace SageFrame.DesktopModules.Admin.Extensions
{
    public partial class ctl_ModuleInstaller : BaseAdministrationUserControl
    {
        Installers installhelp = new Installers();
        ModuleInfo module = new ModuleInfo();
        string Exceptions = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (ViewState["ModuleInfo"] != null)
                    {
                        module = (ModuleInfo)ViewState["ModuleInfo"];
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
            wizInstall.CancelButtonImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            wizInstall.StartNextButtonImageUrl = GetTemplateImageUrl("imgforward.png", true);
            wizInstall.StepNextButtonImageUrl = GetTemplateImageUrl("imgforward.png", true);
            wizInstall.StepPreviousButtonImageUrl = GetTemplateImageUrl("imgback.png", true);
            wizInstall.FinishPreviousButtonImageUrl = GetTemplateImageUrl("imgback.png", true);
            wizInstall.FinishCompleteButtonImageUrl = GetTemplateImageUrl("imgfinished.png", true);
        }

        protected void chkRepairInstall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRepairInstall.Checked)
                {
                    lblWarningMessage.Text = GetSageMessage("Extensions_Editors", "WarningMessageWillDeleteAllFiles");
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void wizInstall_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                //System.Threading.Thread.Sleep(9000);
                int activeIndex = 0;
                switch (e.CurrentStepIndex)
                {
                    case 0://Upload Page         
                        ArrayList arrColl = installhelp.Step0CheckLogic(this.fileModule);
                        int ReturnValue;
                        if (arrColl != null && arrColl.Count > 0)
                        {
                            ReturnValue = (int)arrColl[0];
                            module = (ModuleInfo)arrColl[1];
                            ViewState["ModuleInfo"] = module;
                            if (ReturnValue == 0)
                            {
                                lblLoadMessage.Text = GetSageMessage("Extensions_Editors", "YouNeedToSelectAFileToUploadFirst");
                                lblLoadMessage.Visible = true;
                                e.Cancel = true;
                                activeIndex = 0;
                                break;
                            }
                            else if (ReturnValue == -1)
                            {                                
                                lblLoadMessage.Text = GetSageMessage("Extensions_Editors", "InvalidFileExtension ") + this.fileModule.FileName;
                                lblLoadMessage.Visible = true;
                                e.Cancel = true;
                                activeIndex = 0;
                                break;
                            }
                            else if (ReturnValue == 1)
                            {
                                BindPackage();
                                activeIndex = 2;
                                break;
                            }
                            else if (ReturnValue == 2)
                            {
                                activeIndex = 1;
                                BindPackage();
                                break;
                            }
                            else if (ReturnValue == 3)
                            {
                                lblLoadMessage.Text = GetSageMessage("Extensions_Editors", "ThisPackageIsNotValid");
                                lblLoadMessage.Visible = true;
                                e.Cancel = true;
                                activeIndex = 0;
                                break;
                            }
                            else if (ReturnValue == 4)
                            {
                                lblLoadMessage.Text = GetSageMessage("Extensions_Editors", "ThisPackageDoesNotAppearToBeValid");
                                lblLoadMessage.Visible = true;
                                e.Cancel = true;
                                activeIndex = 0;
                                break;
                            }
                            else
                            {
                                lblLoadMessage.Text = GetSageMessage("Extensions_Editors", "ThereIsErrorWhileInstallingThisModule");
                                lblLoadMessage.Visible = true;
                                e.Cancel = true;
                                activeIndex = 0;
                                break;
                            }
                        }
                        break;
                    case 1://Warning Page    
                        module = (ModuleInfo)ViewState["ModuleInfo"];
                        if (chkRepairInstall.Checked)
                        {                            
                            UninstallModule(module, true);     
                            activeIndex = 2;
                        }
                        else
                        {
                            UninstallModule(module, false); 
                            activeIndex = 1;
                        }
                        break;
                    case 2:
                        activeIndex = 3;
                        break;
                    case 3:
                        activeIndex = 4;
                        break;
                    case 4://Accept Terms
                        if (chkAcceptLicense.Checked)
                        {
                            module = (ModuleInfo)ViewState["ModuleInfo"];
                            if (module != null)
                            {
                                installhelp.InstallPackage(module);
                                if (module.ModuleID <= 0)
                                {
                                    lblLoadMessage.Text = GetSageMessage("Extensions_Editors", "ThereIsErrorWhileInstalling");
                                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions_Editors", "ErrorWhileInstalling"), "", SageMessageType.Error);
                                    lblLoadMessage.Visible = true;
                                    chkAcceptLicense.Checked = false;
                                    ViewState["ModuleInfo"] = null;
                                    activeIndex = 0;
                                }
                                else
                                {
                                    lblInstallMessage.Text = GetSageMessage("Extensions_Editors", "ModuleInstalledSuccessfully");
                                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("Extensions_Editors", "TheModuleIsInstalledSuccessfully"), "", SageMessageType.Success);
                                    wizInstall.DisplayCancelButton = false;
                                    activeIndex = 5;
                                }
                            }
                        }
                        else
                        {
                            lblAcceptMessage.Text = GetSageMessage("Extensions_Editors", "AcceptThePackageLicenseAgreementFirst");
                            e.Cancel = true;
                            activeIndex = 4;
                        }
                        break;
                }
                wizInstall.ActiveStepIndex = activeIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        protected void wizInstall_ActiveStepChanged(object sender, EventArgs e)
        {
            switch (wizInstall.ActiveStepIndex)
            {
                case 1:
                    lblWarningMessage.Text = GetSageMessage("Extensions_Editors", "WarningMessageWillDeleteAllFiles");
                    chkRepairInstall.Checked = true;
                    break;
                case 2:
                    if (chkRepairInstall.Checked)
                    {
                        Button prevButton = GetWizardButton("StepNavigationTemplateContainerID", "CancelButton");                        
                        prevButton.Visible = false;
                    }
                    break;
            }
        }

        private Button GetWizardButton(string containerID, string buttonID)
        {
            Control navContainer = wizInstall.FindControl(containerID);
            Button button = null;
            if ((navContainer != null))
            {
                button = navContainer.FindControl(buttonID) as Button;
            }
            return button;
        }
        #region Uninstall Existing Module

        private void UninstallModule(ModuleInfo moduleInfo, bool deleteModuleFolder)
        {
            Installers installerClass = new Installers();
            string path = HttpContext.Current.Server.MapPath("~/");

            //checked if directory exist for current module foldername
            string moduleFolderPath = moduleInfo.InstalledFolderPath;
            if (!string.IsNullOrEmpty(moduleFolderPath))
            {
                if (Directory.Exists(moduleFolderPath))
                {
                    //check for valid .sfe file exist or not
                    if (installerClass.checkFormanifestFile(moduleFolderPath, moduleInfo) != "")
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(moduleFolderPath + '\\' + moduleInfo.ManifestFile);
                        XmlElement root = doc.DocumentElement;
                        if (installerClass.checkValidManifestFile(root, moduleInfo))
                        {
                            XmlNodeList xnList = doc.SelectNodes("sageframe/folders/folder");
                            foreach (XmlNode xn in xnList)
                            {
                                moduleInfo.ModuleName = xn["modulename"].InnerXml.ToString();
                                moduleInfo.FolderName = xn["foldername"].InnerXml.ToString();

                                if (!String.IsNullOrEmpty(moduleInfo.ModuleName) && !String.IsNullOrEmpty(moduleInfo.FolderName) && installerClass.IsModuleExist(moduleInfo.ModuleName.ToLower(), moduleInfo))
                                {

                                }
                                else
                                {
                                    ShowMessage(SageMessageTitle.Exception.ToString(), GetSageMessage("Extensions_Editors", "ThisModuleSeemsToBeCorrupted"), "", SageMessageType.Error);
                                }
                            }
                            try
                            {
                                if (moduleInfo.ModuleID > 0)
                                {
                                    //Run script  
                                    ReadUninstallScriptAndDLLFiles(doc, moduleFolderPath, installerClass);
                                    //Rollback moduleid
                                    installerClass.ModulesRollBack(moduleInfo.ModuleID, GetPortalID);
                                    if (deleteModuleFolder == true)
                                    {
                                        //Delete Module's Folder
                                        installerClass.DeleteTempDirectory(moduleInfo.InstalledFolderPath);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Exceptions = ex.Message;
                            }

                            if (Exceptions != string.Empty)
                            {
                                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions_Editors", "ModuleExtensionIsUninstallError"), "", SageMessageType.Alert);
                            }
                            else
                            {
                                string ExtensionMessage = GetSageMessage("Extensions_Editors", "ModuleExtensionIsUninstalledSuccessfully");
                                //UninstallProcessCancelRequestBase(Request.RawUrl, true, ExtensionMessage);
                            }
                        }
                        else
                        {
                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions_Editors", "ThisPackageIsNotValid"), "", SageMessageType.Alert);
                        }
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions_Editors", "ThisPackageDoesNotAppearToBeValid"), "", SageMessageType.Alert);
                    }
                }
                else
                {
                    ShowMessage(SageMessageTitle.Exception.ToString(), GetSageMessage("Extensions_Editors", "ModuleFolderDoesnotExist"), "", SageMessageType.Error);
                }
            }
        }

        private void ReadUninstallScriptAndDLLFiles(XmlDocument doc, string moduleFolderPath, Installers installerClass)
        {
            XmlElement root = doc.DocumentElement;
            if (!String.IsNullOrEmpty(root.ToString()))
            {
                ArrayList dllFiles = new ArrayList();
                string _unistallScriptFile = string.Empty;
                XmlNodeList xnFileList = doc.SelectNodes("sageframe/folders/folder/files/file");
                if (xnFileList.Count != 0)
                {
                    foreach (XmlNode xn in xnFileList)
                    {
                        string _fileName = xn["name"].InnerXml;
                        try
                        {
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
                            Exceptions = ex.Message;
                        }
                    }
                    if (_unistallScriptFile != "")
                    {
                        RunUninstallScript(_unistallScriptFile, moduleFolderPath, installerClass);
                    }
                    DeleteAllDllsFromBin(dllFiles, moduleFolderPath);
                }
            }
        }

        private void RunUninstallScript(string _unistallScriptFile, string moduleFolderPath, Installers installerClass)
        {
            Exceptions = installerClass.ReadSQLFile(moduleFolderPath, _unistallScriptFile);
        }

        private void DeleteAllDllsFromBin(ArrayList dllFiles, string moduleFolderPath)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/");

                foreach (string dll in dllFiles)
                {
                    string targetdllPath = path + SageFrame.Core.RegisterModule.Common.DLLTargetPath + '\\' + dll;
                    FileInfo imgInfo = new FileInfo(targetdllPath);
                    if (imgInfo != null)
                    {
                        imgInfo.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions = ex.Message;
            }
        } 

        #endregion

        private void BindPackage()
        {
            if (ViewState["ModuleInfo"] != null)
            {
                ModuleInfo moduleInfo = installhelp.fillModuleInfo(module);
                ViewState["ModuleInfo"] = moduleInfo;
                lblNameD.Text = moduleInfo.Name;
                lblPackageTypeD.Text = moduleInfo.PackageType;
                lblFriendlyNameD.Text = moduleInfo.FriendlyName;
                lblDescriptionD.Text = moduleInfo.Description;
                lblVersionD.Text = moduleInfo.Version;
                lblOwnerD.Text = moduleInfo.Owner;
                lblOrganizationD.Text = moduleInfo.Organization;
                lblUrlD.Text = moduleInfo.URL;
                lblEmailD.Text = moduleInfo.Email;
                lblReleaseNotesD.Text = moduleInfo.ReleaseNotes;
                lblLicenseD.Text = moduleInfo.License;
            }
        }

        protected void wizInstall_CancelButtonClick(object sender, EventArgs e)
        {
            try
            {
                module = (ModuleInfo)ViewState["ModuleInfo"];
                if (module != null)
                {
                    installhelp.DeleteTempDirectory(module.TempFolderPath);
                    ViewState["ModuleInfo"] = null;
                }
                //Redirect to Definitions page
                ReturnBack();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void wizInstall_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                //installhelp.DeleteTempDirectory(((ModuleInfo)(ViewState["ModuleInfo"])).TempFolderPath);
                ViewState["ModuleInfo"] = null;
                ReturnBack();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void ReturnBack()
        {
            ProcessCancelRequest(Request.RawUrl);
        }
    }
}




   

   
