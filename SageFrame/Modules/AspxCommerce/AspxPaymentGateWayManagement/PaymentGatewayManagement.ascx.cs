/*
AspxCommerce® - http://www.AspxCommerce.com
Copyright (c) 20011-2012 by AspxCommerce
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
using System.Web;
using System.Web.UI;
using AspxCommerce.Core.Controller;
using SageFrame.Web;
using System.Collections;
using AspxCommerce.Core;
using System.Xml;
using System.IO;

public partial class Modules_PaymentGatewayManagement_PaymentGatewayManagement : BaseAdministrationUserControl
{
    PaymentGatewayInstaller installhelp = new PaymentGatewayInstaller();
    PaymentGateWayModuleInfo paymentModule = new PaymentGateWayModuleInfo();
    string _exceptions = string.Empty;
    public int StoreID, PortalID;
    public string UserName, CultureName;
    public int ErrorCode = 0;
    public string pageURL = string.Empty;

    protected void page_init(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalServicePath", " var aspxservicePath='" + ResolveUrl("~/") + "Modules/AspxCommerce/AspxCommerceServices/" + "';", true);
            
            lblRepairInstallHelp.Text = SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "WarningMessageWillDeleteAllFiles");
            chkRepairInstall.Checked = true;
            //chkRepairInstall.Enabled = false;
            pnlRepair.Visible = true;

            InitializeJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            StoreID = GetStoreID;
            PortalID = GetPortalID;
            UserName = GetUsername;
            CultureName = GetCurrentCultureName;
            if (!IsPostBack)
            {
                IncludeCss("StoreOrdersReport", "/Templates/" + TemplateName + "/css/GridView/tablesort.css",
                           "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                           "/Templates/" + TemplateName + "/css/PopUp/style.css",
                           "/Templates/" + TemplateName + "/css/Print/print.css");
                IncludeJs("PaymentGateWayManagement", "/js/MessageBox/jquery.easing.1.3.js",
                          "/js/MessageBox/alertbox.js", "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js",
                          "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js", "/js/PopUp/custom.js",
                           "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js","/js/CurrencyFormat/jquery.formatCurrency.all.js",
                          "/Modules/AspxCommerce/AspxPaymentGateWayManagement/js/PaymentGatewayManagement.js");

                Session["payMentGateWayRefresh"] = Server.UrlEncode(System.DateTime.Now.ToString());
                // Assign the Session["update"] with unique value

            }
            pageURL = HttpContext.Current.Request.Url.ToString();
            string x = pageURL;
            string[] url = pageURL.Split('?');
            pageURL = url[0];

            if (Request.QueryString["deleted"] != null)
            {
                if (bool.Parse(Request.QueryString["deleted"]) == true)
                {
                    ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "PaymentGatewayDeletedSuccessfully"), "", SageMessageType.Success);
                }
            }
            if (Request.QueryString["installed"] != null)
            {
                if (bool.Parse(Request.QueryString["installed"]) == true)
                {
                        ShowMessage(SageMessageTitle.Information.ToString(),
                                    SageMessage.GetSageModuleLocalMessageByVertualPath(
                                        "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                                        "PaymentGatewayInstalledSuccessfully"), "", SageMessageType.Success);
                 
                }
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
    }

    #region Install New GateWay
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (Session["payMentGateWayRefresh"] != null)
        {
            if (Session["payMentGateWayRefresh"].ToString() == ViewState["payMentGateWayRefresh"].ToString())
                // If page not Refreshed
            {
                InstallPaymentGateWay();
                Session["payMentGateWayRefresh"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            else
            {
                //nothing
              //  Response.Redirect(pageURL, false);
            }
        }
    }
    private  void InstallPaymentGateWay()
    {
        try
        {
            ArrayList arrColl = installhelp.Step0CheckLogic(fuPGModule);
            int ReturnValue;
            if (arrColl != null && arrColl.Count > 0)
            {
                ReturnValue = (int)arrColl[0];
                paymentModule = (PaymentGateWayModuleInfo)arrColl[1];
                ViewState["PaymentGateway"] = paymentModule.PaymentGatewayTypeID.ToString();
                if (ReturnValue == 0)
                {
                    // ViewState["PaymentGateway"] = null;
                    ShowMessage(SageMessageTitle.Notification.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "YouNeedToSelectAFileToUploadFirst"), "", SageMessageType.Alert);
                    lblLoadMessage.Text = SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "YouNeedToSelectAFileToUploadFirst");
                    lblLoadMessage.Visible = true;
                    ErrorCode = 1;
                    return;
                }
                else if (ReturnValue == -1)
                {
                    // ViewState["PaymentGateway"] = null;
                    ShowMessage(SageMessageTitle.Exception.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "InvalidFileExtension") + this.fuPGModule.FileName, "", SageMessageType.Alert);
                    lblLoadMessage.Text = SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "InvalidFileExtension") + this.fuPGModule.FileName;
                    lblLoadMessage.Visible = true;
                    ErrorCode = 1;
                    return;
                }
                else if (ReturnValue == 1)
                {
                    //paymentModule = (PaymentGateWayModuleInfo)ViewState["PaymentGateway"];
                    //if (paymentModule != null)
                    //{
                    installhelp.InstallPackage(paymentModule, 0);
                    ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "PaymentGatewayInstalledSuccessfully"), "", SageMessageType.Success);
                    ErrorCode = 0;
                    Response.Redirect(pageURL + "?installed=true", false);
                    return;
                    //}
                }
                else if (ReturnValue == 2)
                {
                    if (chkRepairInstall.Checked == true)
                    {
                        //paymentModule = (PaymentGateWayModuleInfo)ViewState["PaymentGateway"];
                        int gatewayID = int.Parse(ViewState["PaymentGateway"].ToString());
                        if (paymentModule != null)
                        {
                            UninstallPaymentGateway(paymentModule, true, gatewayID);
                            ViewState["PaymentGateway"] = null;
                            ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "PaymentGatewayInstalledSuccessfully"), "", SageMessageType.Success);
                            ErrorCode = 0;
                            Response.Redirect(pageURL + "?installed=true", false);
                            return;
                        }
                    }
                    else
                    {
                        //ViewState["PaymentGateway"] = null;                    
                        ShowMessage(SageMessageTitle.Notification.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "AlreadyExistPaymentGateway"), "", SageMessageType.Alert);
                     //   lblLoadMessage.Text = SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "AlreadyExistPaymentGateway");
                     //   lblLoadMessage.Visible = true;
                        ErrorCode = 1;
                        return;
                    }
                }
                else if (ReturnValue == 3)
                {
                    // ViewState["PaymentGateway"] = null;
                    ShowMessage(SageMessageTitle.Notification.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "ThisPackageIsNotValid"), "", SageMessageType.Alert);
                    lblLoadMessage.Text = SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "ThisPackageIsNotValid");
                    lblLoadMessage.Visible = true;
                    ErrorCode = 1;
                    return;
                }
                else if (ReturnValue == 4)
                {
                    // ViewState["PaymentGateway"] = null;
                    ShowMessage(SageMessageTitle.Notification.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "ThisPackageDoesNotAppearToBeValid"), "", SageMessageType.Alert);
                    lblLoadMessage.Text = SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "ThisPackageDoesNotAppearToBeValid");
                    lblLoadMessage.Visible = true;
                    ErrorCode = 1;
                    return;
                }
                else
                {
                    // ViewState["PaymentGateway"] = null;
                    ShowMessage(SageMessageTitle.Exception.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "ThereIsErrorWhileInstallingThisModule"), "", SageMessageType.Error);
                    lblLoadMessage.Text = SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "ThereIsErrorWhileInstallingThisModule");
                    lblLoadMessage.Visible = true;
                    ErrorCode = 1;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        if (Session["payMentGateWayRefresh"] == null && ViewState["payMentGateWayRefresh"] != null)
        {
            Session["payMentGateWayRefresh"] = ViewState["payMentGateWayRefresh"];
        }
        else
        {
            ViewState["payMentGateWayRefresh"] = Session["payMentGateWayRefresh"];
        }
    }
    #endregion

    #region Uninstall Existing Payment Gateway

    private void UninstallPaymentGateway(PaymentGateWayModuleInfo paymentGateWay, bool deleteModuleFolder, int gatewayID)
    {
        PaymentGatewayInstaller installerClass = new PaymentGatewayInstaller();
        string path = HttpContext.Current.Server.MapPath("~/");

        //checked if directory exist for current Payment Gateway foldername
        string paymentGatewayFolderPath = paymentGateWay.InstalledFolderPath;
        if (!string.IsNullOrEmpty(paymentGatewayFolderPath))
        {
            if (Directory.Exists(paymentGatewayFolderPath))
            {
                //check for valid .sfe file exist or not
                XmlDocument doc = new XmlDocument();
                doc.Load(paymentGatewayFolderPath + '\\' + paymentGateWay.ManifestFile);

                try
                {
                    if (paymentGateWay.PaymentGatewayTypeID > 0)
                    {
                        //Run script  
                        ReadUninstallScriptAndDLLFiles(doc, paymentGatewayFolderPath, installerClass);
                        //Rollback PaymentGatewayTypeID
                        //installerClass.PaymentGatewayRollBack(paymentGateWay.PaymentGatewayTypeID, GetPortalID, GetStoreID);
                        if (deleteModuleFolder == true)
                        {
                            //Delete Payment GateWay's Original Folder
                            installerClass.DeleteTempDirectory(paymentGateWay.InstalledFolderPath);
                        }
                        installhelp.InstallPackage(paymentModule, gatewayID);
                    }
                }
                catch (Exception ex)
                {
                    _exceptions = ex.Message;
                }
            }
            else if (!Directory.Exists(paymentGatewayFolderPath))
            {
                installhelp.InstallPackage(paymentModule, gatewayID);
            }
            else
            {
                ShowMessage(SageMessageTitle.Exception.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "PaymentGatewayFolderDoesnotExist"), "", SageMessageType.Error);
            }
        }
    }

    private void ReadUninstallScriptAndDLLFiles(XmlDocument doc, string paymentGatewayFolderPath, PaymentGatewayInstaller installerClass)
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
                        _exceptions = ex.Message;
                    }
                }
                if (_unistallScriptFile != "")
                {
                    RunUninstallScript(_unistallScriptFile, paymentGatewayFolderPath, installerClass);
                }
                DeleteAllDllsFromBin(dllFiles, paymentGatewayFolderPath);
            }
        }
    }

    private void RunUninstallScript(string _unistallScriptFile, string paymentGatewayFolderPath, PaymentGatewayInstaller installerClass)
    {
        _exceptions = installerClass.ReadSQLFile(paymentGatewayFolderPath, _unistallScriptFile);
    }

    private void DeleteAllDllsFromBin(ArrayList dllFiles, string paymentGatewayFolderPath)
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
            _exceptions = ex.Message;
        }
    }

    #endregion

    protected void btnSavePDFForm2_Click(object sender, EventArgs e)
    {
        try
        {
            string tableContent = "["+HdnValue.Value+"]";
            string hiddenValue = hdnDescriptionValue.Value;
            string templateFolderPath = TemplateName;
            GeneratePDF gPdf = new GeneratePDF();
            gPdf.GenerateOrderDetailsPDF(tableContent,hiddenValue,templateFolderPath);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
