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
using SageFrame.Application;
using SageFrame.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Collections;
using SageFrame.ListManagement;
using SageFrame.Framework;
using SageFrame.Shared;
using SageFrame.SageFrameClass;
using System.Net.Mail;
using SageFrame.SageFrameClass.MessageManagement;
using SageFrame.PortalSetting;
using SageFrame.FileManager;

namespace SageFrame.Modules.Admin.SuperUserSettings
{
    public partial class ctl_SuperUserSettings : BaseAdministrationUserControl
    {
        protected string BaseDir;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BaseDir = GetAbsolutePath(HttpContext.Current.Request.PhysicalApplicationPath.ToString());
                BaseDir = BaseDir.Substring(0, BaseDir.Length - 1);
                Initialize();
                if (!IsPostBack)
                {
                    AddImageUrls();
                    //AddAttributes();
                    BindData();
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
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void AddImageUrls()
        {
            imbSave.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            imbRestart.ImageUrl = GetTemplateImageUrl("imgrestart.png", true);
            btnShowPopUp.ImageUrl = GetTemplateImageUrl("folder_add.png", true);
        }


        //private void AddAttributes()
        //{
        //    txtHostingFee.Attributes.Add("OnKeydown", "return NumberKeyWithDecimal(event)");
        //    txtHostingSpace.Attributes.Add("OnKeydown", "return NumberKeyWithDecimal(event)");
        //    txtPageQuota.Attributes.Add("OnKeydown", "return NumberKeyWithDecimal(event)");
        //    txtUserQuota.Attributes.Add("OnKeydown", "return NumberKey(event)");
        //    txtDemoPeriod.Attributes.Add("OnKeydown", "return NumberKey(event)");
        //    txtWebRequestTimeOut.Attributes.Add("OnKeydown", "return NumberKey(event)");
        //}

        private void BindData()
        {
            SageFrame.Application.Application app = new SageFrame.Application.Application();
            SageFrameConfig pagebase = new SageFrameConfig();

            lblVProduct.Text = app.Description;
            lblVVersion.Text = app.FormatVersion(app.Version, true);

            //imbIsUpgradeAvilable.ImageUrl = GetTemplateImageUrl("imgupgrade.png", true);

            lblVDataProvider.Text = app.DataProvider;
            lblVDotNetFrameWork.Text = app.NETFrameworkIISVersion.ToString();
            lblVASPDotNetIdentiy.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            lblVServerName.Text = app.DNSName;
            lblVIpAddress.Text = app.ServerIPAddress;
            lblVPermissions.Text = Framework.SecurityPolicy.Permissions;
            lblVRelativePath.Text = app.ApplicationPath;

            lblVPhysicalPath.Text = app.ApplicationMapPath;
            lblVServerTime.Text = DateTime.Now.ToString();

            lblVGUID.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.GUID);
            //ServerController svc = new ServerController();
            //chkIsWebFarm.Checked = svc.IsWebFarm;
            BinSitePortal();
            if (ddlHostPortal.Items.Count > 0)
            {
                ddlHostPortal.SelectedIndex = ddlHostPortal.Items.IndexOf(ddlHostPortal.Items.FindByValue(SageFrameSettingKeys.SuperUserPortalId));
            }

            txtHostTitle.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserTitle);
            txtHostUrl.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserURL);
            txtHostEmail.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);

            //Apprence
            chkCopyright.Checked = pagebase.GetSettingBollByKey(SageFrameSettingKeys.SuperUserCopyright);
            chkUseCustomErrorMessages.Checked = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseCustomErrorMessages);

            BinSiteTemplates();
            if (ddlTemplate.Items.Count > 0)
            {
                string Template = pagebase.GetSettingsByKey(SageFrameSettingKeys.SageFrameCSS);
                ddlTemplate.SelectedIndex = ddlTemplate.Items.IndexOf(ddlTemplate.Items.FindByText(Template));
            }

            ////Payment
            //BindPaymentProcessor();
            //if (ddlPaymentProcessor.Items.Count > 0)
            //{
            //    string ExistsPaymentProcessor = pagebase.GetSettingsByKey(SageFrameSettingKeys.PaymentProcessor);
            //    ddlPaymentProcessor.SelectedIndex = ddlPaymentProcessor.Items.IndexOf(ddlPaymentProcessor.Items.FindByText(ExistsPaymentProcessor));
            //}
            //txtPaymentProcessorUserID.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.ProcessorUserId);
            //txtPaymentProcessorPassword.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.ProcessorPassword);
            //txtHostingFee.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserFee);
            //BinCurrency();
            //if (ddlHostingCurrency.Items.Count > 0)
            //{
            //    string ExistsCurrency = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserCurrency);
            //    ddlHostingCurrency.SelectedIndex = ddlHostingCurrency.Items.IndexOf(ddlHostingCurrency.Items.FindByText(ExistsCurrency));
            //}
            //txtHostingSpace.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserSpace);
            //txtPageQuota.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.PageQuota);
            //txtUserQuota.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.UserQuota);
            //txtDemoPeriod.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.DemoPeriod);
            //chkAnonymousDemoSignup.Checked = pagebase.GetSettingBollByKey(SageFrameSettingKeys.DemoSignup);

            //Advance Setting
            chkUseFriendlyUrls.Checked = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            //txtProxyServer.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.ProxyServer);
            //txtProxyPort.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.ProxyPort);
            //txtProxyUsername.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.ProxyUsername);
            //string ProxyPassord = string.Empty;
            //ProxyPassord = pagebase.GetSettingsByKey(SageFrameSettingKeys.ProxyPassword);
            //if (!string.IsNullOrEmpty(ProxyPassord))
            //{
            //    txtProxyPassword.Attributes.Add("value", ProxyPassord);
            //}
            //txtWebRequestTimeOut.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.WebRequestTimeout);

            //SMTP
            txtSMTPServerAndPort.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.SMTPServer);
            BindSMTPAuthntication();
            if (rblSMTPAuthentication.Items.Count > 0)
            {
                string ExistsSMTPAuth = pagebase.GetSettingsByKey(SageFrameSettingKeys.SMTPAuthentication);
                if (!string.IsNullOrEmpty(ExistsSMTPAuth))
                {
                    rblSMTPAuthentication.SelectedIndex = rblSMTPAuthentication.Items.IndexOf(rblSMTPAuthentication.Items.FindByValue(ExistsSMTPAuth));
                }
            }
            chkSMTPEnableSSL.Checked = pagebase.GetSettingBollByKey(SageFrameSettingKeys.SMTPEnableSSL);
            txtSMTPUserName.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.SMTPUsername);
            string ExistsSMTPPassword = pagebase.GetSettingsByKey(SageFrameSettingKeys.SMTPPassword);
            if (!string.IsNullOrEmpty(ExistsSMTPPassword))
            {
                txtSMTPPassword.Attributes.Add("value", ExistsSMTPPassword);
            }
            ShowHideSMTPCredentials();

            ////JQuery Section
            //lblVQueryVersion.Text = "3.5";//Need to implemented for relese mode
            //chkUseJQueryDebugVersion.Checked = pagebase.GetSettingBollByKey(SageFrameSettingKeys.jQueryDebug);
            //chkUseHostedJQueryVersion.Checked = pagebase.GetSettingBollByKey(SageFrameSettingKeys.jQuerySuperUsered);
            //txtHostedJQueryURL.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.jQueryUrl);

            //Others
            txtFileExtensions.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.FileExtensions);
            txtHelpUrl.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.HelpURL);
        }

       


        private void BinSitePortal()
        {
            try
            {
                SettingProvider spr = new SettingProvider();
                ddlHostPortal.DataSource = spr.GetAllPortals();
                ddlHostPortal.DataTextField = "Name";
                ddlHostPortal.DataValueField = "PortalID";
                ddlHostPortal.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BinSiteTemplates()
        {
            try
            {
                string FolderName = "~/Templates";
                string FullPath = Server.MapPath(FolderName);
                string[] dirColl = Directory.GetDirectories(FullPath);
                ArrayList arrColl = new ArrayList();
                if (dirColl.Length > 0)
                {
                    foreach (string str in dirColl)
                    {
                        string tempstr = str.Remove(0, str.LastIndexOf("\\") + 1);
                        if (tempstr.ToLower() != ".svn".ToLower())
                        {
                            arrColl.Add(tempstr);
                        }
                    }
                }

                ddlTemplate.DataSource = arrColl;
                ddlTemplate.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        //private void BindPaymentProcessor()
        //{
        //    ListManagementDataContext db = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
        //    var LINQ = db.sp_GetListEntrybyNameAndID("Processor", -1,GetCurrentCultureName);
        //    ddlPaymentProcessor.DataSource = LINQ;
        //    ddlPaymentProcessor.DataValueField = "Value";
        //    ddlPaymentProcessor.DataTextField = "Text";
        //    ddlPaymentProcessor.DataBind();
        //}

        //private void BinCurrency()
        //{
        //    ListManagementDataContext db = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
        //    var LINQ = db.sp_GetListEntrybyNameAndID("Currency", -1,GetCurrentCultureName);
        //    ddlHostingCurrency.DataSource = LINQ;
        //    ddlHostingCurrency.DataValueField = "Value";
        //    ddlHostingCurrency.DataTextField = "Text";
        //    ddlHostingCurrency.DataBind();
        //}

        private void BindSMTPAuthntication()
        {
            rblSMTPAuthentication.DataSource = SageFrameLists.SMTPAuthntication();
            rblSMTPAuthentication.DataTextField = "value";
            rblSMTPAuthentication.DataValueField = "key";
            rblSMTPAuthentication.DataBind();
            if (rblSMTPAuthentication.Items.Count > 0)
            {
                rblSMTPAuthentication.SelectedIndex = 0;
            }
        }

        protected void imbIsUpgradeAvilable_Click(object sender, ImageClickEventArgs e)
        {

        }

        //protected void lnkPaymentProcessor_Click(object sender, EventArgs e)
        //{
        //    RedirectToPaymentProcessor();
        //}

        //private void RedirectToPaymentProcessor()
        //{
        //    try
        //    {
        //        if (ddlPaymentProcessor.Items.Count > 0)
        //        {
        //            string ProcessorURL = ddlPaymentProcessor.SelectedItem.Value;
        //            if (!string.IsNullOrEmpty(ProcessorURL))
        //            {
        //                Response.Redirect(ProcessorURL);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ProcessException(ex);
        //    }
        //}

        private void ShowHideSMTPCredentials()
        {
            if (Int32.Parse(rblSMTPAuthentication.SelectedItem.Value) == 1)
            {
                trSMTPUserName.Style.Add("display", "");
                trSMTPPassword.Style.Add("display", "");
            }
            else
            {
                trSMTPUserName.Style.Add("display", "none");
                trSMTPPassword.Style.Add("display", "none");
            }
        }

        protected void rblSMTPAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideSMTPCredentials();
        }

        protected void lnkTestSMTP_Click(object sender, EventArgs e)
        {
            TestEmailServer();
        }

        private void TestEmailServer()
        {
            try
            {
                if (txtSMTPServerAndPort.Text.Trim() != string.Empty)
                {
                    MailHelper.SendMailNoAttachment(txtHostEmail.Text, txtHostEmail.Text, "Test Email for configration", "Test Email", string.Empty, string.Empty);

                    lblSMTPEmailTestResult.Text = GetSageMessage("PortalSettings", "SMTPServerIsWorking");
                    lblSMTPEmailTestResult.Visible = true;
                    lblSMTPEmailTestResult.CssClass = "Normalbold";
                }
                else
                {
                    // lblSMTPEmailTestResult.Text = "Please Fill Server Address";
                    lblSMTPEmailTestResult.Text = GetSageMessage("PortalSettings", "PleaseFillServerAddress");
                    lblSMTPEmailTestResult.Visible = true;
                    lblSMTPEmailTestResult.CssClass = "NormalRed";
                }
            }
            catch (Exception ex)
            {
                lblSMTPEmailTestResult.Text = ex.Message;
                lblSMTPEmailTestResult.Visible = true;
                lblSMTPEmailTestResult.CssClass = "NormalRed";
            }
        }

        protected void imbSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveSettings();
        }

        protected void imbRestart_Click(object sender, ImageClickEventArgs e)
        {
            RestartApplication();
        }

        private void RestartApplication()
        {
            //System.Web.HttpRuntime.UnloadAppDomain();
            //Response.Redirect("./");
            SageFrame.Application.Application app = new SageFrame.Application.Application();
            File.SetLastWriteTime((app.ApplicationMapPath + "\\web.config"), System.DateTime.Now);
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect(Page.ResolveUrl("~/" + CommonHelper.DefaultPage), true);
        }

        private void SaveSettings()
        {
            try
            {
                StringBuilder sbSettingKey = new StringBuilder();
                StringBuilder sbSettingValue = new StringBuilder();
                StringBuilder sbSettingType = new StringBuilder();

                //Collecting Setting Values
                sbSettingKey.Append(SageFrameSettingKeys.SuperUserPortalId + ",");
                sbSettingValue.Append(ddlHostPortal.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.SuperUserTitle                
                sbSettingKey.Append(SageFrameSettingKeys.SuperUserTitle + ",");
                sbSettingValue.Append(txtHostTitle.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.SuperUserURL
                sbSettingKey.Append(SageFrameSettingKeys.SuperUserURL + ",");
                sbSettingValue.Append(txtHostUrl.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.SuperUserEmail
                sbSettingKey.Append(SageFrameSettingKeys.SuperUserEmail + ",");
                sbSettingValue.Append(txtHostEmail.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.SuperUserCopyright
                sbSettingKey.Append(SageFrameSettingKeys.SuperUserCopyright + ",");
                sbSettingValue.Append(chkCopyright.Checked + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.UseCustomErrorMessages
                sbSettingKey.Append(SageFrameSettingKeys.UseCustomErrorMessages + ",");
                sbSettingValue.Append(chkUseCustomErrorMessages.Checked + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.SageFrameCSS
                sbSettingKey.Append(SageFrameSettingKeys.SageFrameCSS + ",");
                sbSettingValue.Append(ddlTemplate.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.PaymentProcessor
                //sbSettingKey.Append(SageFrameSettingKeys.PaymentProcessor + ",");
                //sbSettingValue.Append(ddlPaymentProcessor.SelectedItem.Value + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.ProcessorUserId
                //sbSettingKey.Append(SageFrameSettingKeys.ProcessorUserId + ",");
                //sbSettingValue.Append(txtPaymentProcessorUserID.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.ProcessorPassword
                //sbSettingKey.Append(SageFrameSettingKeys.ProcessorPassword + ",");
                //sbSettingValue.Append(txtPaymentProcessorPassword.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.SuperUserFee
                //sbSettingKey.Append(SageFrameSettingKeys.SuperUserFee + ",");
                //sbSettingValue.Append(txtHostingFee.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.SuperUserCurrency
                //sbSettingKey.Append(SageFrameSettingKeys.SuperUserCurrency + ",");
                //sbSettingValue.Append(ddlHostingCurrency.SelectedItem.Value + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.SuperUserSpace
                //sbSettingKey.Append(SageFrameSettingKeys.SuperUserSpace + ",");
                //sbSettingValue.Append(txtHostingSpace.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.PageQuota
                //sbSettingKey.Append(SageFrameSettingKeys.PageQuota + ",");
                //sbSettingValue.Append(txtPageQuota.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.UserQuota
                //sbSettingKey.Append(SageFrameSettingKeys.UserQuota + ",");
                //sbSettingValue.Append(txtUserQuota.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.DemoPeriod
                //sbSettingKey.Append(SageFrameSettingKeys.DemoPeriod + ",");
                //sbSettingValue.Append(txtDemoPeriod.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.DemoSignup
                //sbSettingKey.Append(SageFrameSettingKeys.DemoSignup + ",");
                //sbSettingValue.Append(chkAnonymousDemoSignup.Checked + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.UseFriendlyUrls
                sbSettingKey.Append(SageFrameSettingKeys.UseFriendlyUrls + ",");
                sbSettingValue.Append(chkUseFriendlyUrls.Checked + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.ProxyServer
                //sbSettingKey.Append(SageFrameSettingKeys.ProxyServer + ",");
                //sbSettingValue.Append(txtProxyServer.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.ProxyPort
                //sbSettingKey.Append(SageFrameSettingKeys.ProxyPort + ",");
                //sbSettingValue.Append(txtProxyPort.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.ProxyUsername
                //sbSettingKey.Append(SageFrameSettingKeys.ProxyUsername + ",");
                //sbSettingValue.Append(txtProxyUsername.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.ProxyPassword
                //sbSettingKey.Append(SageFrameSettingKeys.ProxyPassword + ",");
                //sbSettingValue.Append(txtProxyPassword.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.WebRequestTimeout
                //sbSettingKey.Append(SageFrameSettingKeys.WebRequestTimeout + ",");
                //sbSettingValue.Append(txtWebRequestTimeOut.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.SMTPServer
                sbSettingKey.Append(SageFrameSettingKeys.SMTPServer + ",");
                sbSettingValue.Append(txtSMTPServerAndPort.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.SMTPAuthentication
                sbSettingKey.Append(SageFrameSettingKeys.SMTPAuthentication + ",");
                sbSettingValue.Append(rblSMTPAuthentication.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.SMTPEnableSSL
                sbSettingKey.Append(SageFrameSettingKeys.SMTPEnableSSL + ",");
                sbSettingValue.Append(chkSMTPEnableSSL.Checked + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.SMTPUsername
                sbSettingKey.Append(SageFrameSettingKeys.SMTPUsername + ",");
                sbSettingValue.Append(txtSMTPUserName.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.SMTPPassword
                sbSettingKey.Append(SageFrameSettingKeys.SMTPPassword + ",");
                sbSettingValue.Append(txtSMTPPassword.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.jQueryDebug
                //sbSettingKey.Append(SageFrameSettingKeys.jQueryDebug + ",");
                //sbSettingValue.Append(chkUseJQueryDebugVersion.Checked + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.jQuerySuperUsered
                //sbSettingKey.Append(SageFrameSettingKeys.jQuerySuperUsered + ",");
                //sbSettingValue.Append(chkUseHostedJQueryVersion.Checked + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                ////SageFrameSettingKeys.jQueryUrl
                //sbSettingKey.Append(SageFrameSettingKeys.jQueryUrl + ",");
                //sbSettingValue.Append(txtHostedJQueryURL.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.FileExtensions
                sbSettingKey.Append(SageFrameSettingKeys.FileExtensions + ",");
                sbSettingValue.Append(txtFileExtensions.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                //SageFrameSettingKeys.HelpURL
                sbSettingKey.Append(SageFrameSettingKeys.HelpURL + ",");
                sbSettingValue.Append(txtHelpUrl.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SuperUser + ",");

                string SettingTypes = sbSettingType.ToString();
                if (SettingTypes.Contains(","))
                {
                    SettingTypes = SettingTypes.Remove(SettingTypes.LastIndexOf(","));
                }
                string SettingKeys = sbSettingKey.ToString();
                if (SettingKeys.Contains(","))
                {
                    SettingKeys = SettingKeys.Remove(SettingKeys.LastIndexOf(","));
                }
                string SettingValues = sbSettingValue.ToString();
                if (SettingValues.Contains(","))
                {
                    SettingValues = SettingValues.Remove(SettingValues.LastIndexOf(","));
                }
                SettingProvider sageSP = new SettingProvider();
                sageSP.SaveSageSettings(SettingTypes, SettingKeys, SettingValues, GetUsername, "1");
                HttpContext.Current.Cache.Remove("SageSetting");               
                BindData();
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("PortalSettings", "SettingUpdatedSuccessfully"), "", SageMessageType.Success);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
                lblError.Text = ex.Message;
            }
        }




        #region FileManagerSettings

        protected void Initialize()
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
          
            string relativePath = FileManagerHelper.ReplaceBackSlash(Request.PhysicalApplicationPath.ToString());
            relativePath = relativePath.Substring(0, relativePath.LastIndexOf("/"));
            string root = Request.ApplicationPath.ToString();
            rootNode.Text = Path.Combine(BaseDir.Replace(relativePath, ""), root); 
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
            shcFileManager.IsExpanded = true;
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
            folder.IsActive = 1;
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
                shcFileManager.IsExpanded = true;
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
                FileManagerController.EnableRootFolder(FolderID, chk.Checked);
                CacheHelper.Clear("FileManagerRootFolders");
                CacheHelper.Clear("FileManagerFolders");
                GetRootFolders();
            }
            catch (Exception ex)
            {

                ProcessException(ex);
            }


        }

        protected void imgClosePopUp_Click(object sender, EventArgs e)
        {
            PopUp.Hide();
            shcFileManager.IsExpanded = true;
        }

        #endregion
    }
}