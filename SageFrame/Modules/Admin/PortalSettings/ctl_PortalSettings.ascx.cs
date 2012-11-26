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
using SageFrame.Core.Pages;
using SageFrame.Web;
using SageFrame.Localization;
using SageFrame.Framework;
using System.Collections.Specialized;
using SageFrame.ListManagement;
using System.Collections;
using System.IO;
using SageFrame.SageFrameClass;
using System.Text;
using SageFrame.Shared;
using SageFrame.Template;
using SageFrame.Common;
namespace SageFrame.PortalSetting
{
    public partial class ctl_PortalSettings : BaseAdministrationUserControl
    {
        private string languageMode = "Normal";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    AddImageUrls();
                    BinDDls();
                    BindData();
                    SageFrameConfig sfConf = new SageFrameConfig();
                    ViewState["SelectedLanguageCulture"] = sfConf.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultLanguage);
                    GetLanguageList();
                    GetFlagImage();
                  
                    //imbSave.Attributes.Add("onclick", string.Format("sageHtmlEncoder('{0}')", txtLogoTemplate.ClientID + "," + txtCopyright.ClientID));
                    //txtPortalUserProfileMaxImageSize.Attributes.Add("OnKeydown", "return NumberKeyWithDecimal(event)");
                    //txtPortalUserProfileMediumImageSize.Attributes.Add("OnKeydown", "return NumberKey(event)");
                    //txtPortalUserProfileSmallImageSize.Attributes.Add("OnKeydown", "return NumberKey(event)");
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
            imbRefresh.ImageUrl = GetTemplateImageUrl("imgrefresh.png", true);
        }

        private void BinDDls()
        {
            BinSiteTemplates();

            BindPageDlls();

            Bindlistddls();


            BindddlTimeZone();

            //BinSearchEngines();

            BindRegistrationTypes();

            BindYesNoRBL();
        }

        private void BindData()
        {
            SageFrameConfig pagebase = new SageFrameConfig();
            txtPortalTitle.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.PageTitle);
            txtDescription.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.MetaDescription);
            txtKeyWords.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.MetaKeywords);
            txtCopyright.Text = Server.HtmlDecode(pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalCopyright));
            txtLogoTemplate.Text = Server.HtmlDecode(pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLogoTemplate));
            txtPortalGoogleAdSenseID.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalGoogleAdSenseID);
            chkOptCss.Checked=bool.Parse(pagebase.GetSettingsByKey(SageFrameSettingKeys.OptimizeCss));
            chkOptJs.Checked=bool.Parse(pagebase.GetSettingsByKey(SageFrameSettingKeys.OptimizeJs));
            txtSiteAdminEmailAddress.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
            //txtPortalUserProfileMaxImageSize.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalUserProfileMaxImageSize);
            //txtPortalUserProfileMediumImageSize.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalUserProfileMediumImageSize);
            //txtPortalUserProfileSmallImageSize.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalUserProfileSmallImageSize);
            txtPortalMenuImageExtension.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalMenuImageExtension);

            //if (ddlSearchEngine.Items.Count > 0)
            //{
            //    string ExistingSearchEngine = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalSearchEngine);
            //    ddlSearchEngine.SelectedIndex = ddlSearchEngine.Items.IndexOf(ddlSearchEngine.Items.FindByValue(ExistingSearchEngine));
            //}

            if (ddlTemplate.Items.Count > 0)
            {
                string ExistingTemplate = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalCssTemplate);
                ddlTemplate.SelectedIndex = ddlTemplate.Items.IndexOf(ddlTemplate.Items.FindByValue(ExistingTemplate));
            }

            if (rblPortalShowProfileLink.Items.Count > 0)
            {
                string ExistingPortalShowProfileLink = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalShowProfileLink);
                rblPortalShowProfileLink.SelectedIndex = rblPortalShowProfileLink.Items.IndexOf(rblPortalShowProfileLink.Items.FindByValue(ExistingPortalShowProfileLink));
            }

            //RemeberMe setting
            chkEnableRememberme.Checked = pagebase.GetSettingBollByKey(SageFrameSettingKeys.RememberCheckbox);           

            //if (rblPortalShowSubscribe.Items.Count > 0)
            //{
            //    string ExistingPortalShowSubscribe = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalShowSubscribe);
            //    rblPortalShowSubscribe.SelectedIndex = rblPortalShowSubscribe.Items.IndexOf(rblPortalShowSubscribe.Items.FindByValue(ExistingPortalShowSubscribe));
            //}

            //if (rblPortalShowLogo.Items.Count > 0)
            //{
            //    string ExistingPortalShowLogo = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalShowLogo);
            //    rblPortalShowLogo.SelectedIndex = rblPortalShowLogo.Items.IndexOf(rblPortalShowLogo.Items.FindByValue(ExistingPortalShowLogo));
            //}

            //if (rblPortalShowFooterLink.Items.Count > 0)
            //{
            //    string ExistingPortalShowFooterLink = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalShowFooterLink);
            //    rblPortalShowFooterLink.SelectedIndex = rblPortalShowFooterLink.Items.IndexOf(rblPortalShowFooterLink.Items.FindByValue(ExistingPortalShowFooterLink));
            //}

            //if (rblPortalShowFooter.Items.Count > 0)
            //{
            //    string ExistingPortalShowFooter = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalShowFooter);
            //    rblPortalShowFooter.SelectedIndex = rblPortalShowFooter.Items.IndexOf(rblPortalShowFooter.Items.FindByValue(ExistingPortalShowFooter));
            //}

            //if (rblPortalShowBreadCrum.Items.Count > 0)
            //{
            //    string ExistingPortalShowBreadCrum = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalShowBreadCrum);
            //    rblPortalShowBreadCrum.SelectedIndex = rblPortalShowBreadCrum.Items.IndexOf(rblPortalShowBreadCrum.Items.FindByValue(ExistingPortalShowBreadCrum));
            //}

            //if (rblPortalShowCopyRight.Items.Count > 0)
            //{
            //    string ExistingPortalShowCopyRight = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalShowCopyRight);
            //    rblPortalShowCopyRight.SelectedIndex = rblPortalShowCopyRight.Items.IndexOf(rblPortalShowCopyRight.Items.FindByValue(ExistingPortalShowCopyRight));
            //}

            //if (rblPortalShowLoginStatus.Items.Count > 0)
            //{
            //    string ExistingPortalShowLoginStatus = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalShowLoginStatus);
            //    rblPortalShowLoginStatus.SelectedIndex = rblPortalShowLoginStatus.Items.IndexOf(rblPortalShowLoginStatus.Items.FindByValue(ExistingPortalShowLoginStatus));
            //}

            if (rblUserRegistration.Items.Count > 0)
            {
                string ExistingData = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalUserRegistration);
                rblUserRegistration.SelectedIndex = rblUserRegistration.Items.IndexOf(rblUserRegistration.Items.FindByValue(ExistingData));
            }

            if (rblIsPortalMenuIsImage.Items.Count > 0)
            {
                string IsPortalMenuIsImage = pagebase.GetSettingsByKey(SageFrameSettingKeys.IsPortalMenuIsImage);
                rblIsPortalMenuIsImage.SelectedIndex = rblIsPortalMenuIsImage.Items.IndexOf(rblIsPortalMenuIsImage.Items.FindByValue(IsPortalMenuIsImage));
            }

            if (ddlLoginPage.Items.Count > 0)
            {
                string ExistingPortalLoginpage = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage);
                ddlLoginPage.SelectedIndex = ddlLoginPage.Items.IndexOf(ddlLoginPage.Items.FindByValue(ExistingPortalLoginpage));

            }

            if (ddlUserRegistrationPage.Items.Count > 0)
            {
                string ExistingPortalUserActivation = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalUserActivation);
                ddlPortalUserActivation.SelectedIndex = ddlPortalUserActivation.Items.IndexOf(ddlPortalUserActivation.Items.FindByValue(ExistingPortalUserActivation));
            }

            if (ddlUserRegistrationPage.Items.Count > 0)
            {
                string ExistingPortalRegistrationPage = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalRegistrationPage);
                ddlUserRegistrationPage.SelectedIndex = ddlUserRegistrationPage.Items.IndexOf(ddlUserRegistrationPage.Items.FindByValue(ExistingPortalRegistrationPage));
            }
            
            if (ddlPortalForgotPassword.Items.Count > 0)
            {
                string ExistingPortalForgotPassword = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalForgotPassword);
                ddlPortalForgotPassword.SelectedIndex = ddlPortalForgotPassword.Items.IndexOf(ddlPortalForgotPassword.Items.FindByValue(ExistingPortalForgotPassword));
            }

            //ddlPortalPageNotAccessible
            if (ddlPortalPageNotAccessible.Items.Count > 0)
            {
                string ExistingPortalPageNotAccessible = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalPageNotAccessible);
                ddlPortalPageNotAccessible.SelectedIndex = ddlPortalPageNotAccessible.Items.IndexOf(ddlPortalPageNotAccessible.Items.FindByValue(ExistingPortalPageNotAccessible));
            }

            //ddlPortalPageNotFound
            if (ddlPortalPageNotFound.Items.Count > 0)
            {
                string ExistingPortalPageNotFound = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalPageNotFound);
                ddlPortalPageNotFound.SelectedIndex = ddlPortalPageNotFound.Items.IndexOf(ddlPortalPageNotFound.Items.FindByValue(ExistingPortalPageNotFound));
            }

            //ddlPortalPasswordRecovery
            if (ddlPortalPasswordRecovery.Items.Count > 0)
            {
                string ExistingPortalPasswordRecovery = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalPasswordRecovery);
                ddlPortalPasswordRecovery.SelectedIndex = ddlPortalPasswordRecovery.Items.IndexOf(ddlPortalPasswordRecovery.Items.FindByValue(ExistingPortalPasswordRecovery));
            }

            //ddlPortalDefaultPage
            if (ddlPortalDefaultPage.Items.Count > 0)
            {
                string ExistingPortalDefaultPage = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage);
                ddlPortalDefaultPage.SelectedIndex = ddlPortalDefaultPage.Items.IndexOf(ddlPortalDefaultPage.Items.FindByValue(ExistingPortalDefaultPage));
            }

            //ddlPortalUserProfilePage
            if (ddlPortalUserProfilePage.Items.Count > 0)
            {
                string ExistingPortalUserProfilePage = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalUserProfilePage);
                ddlPortalUserProfilePage.SelectedIndex = ddlPortalUserProfilePage.Items.IndexOf(ddlPortalUserProfilePage.Items.FindByValue(ExistingPortalUserProfilePage));
            }

            //if (ddlCurrency.Items.Count > 0)
            //{
            //    string ExistingPortalCurrency = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalCurrency);
            //    ddlCurrency.SelectedIndex = ddlCurrency.Items.IndexOf(ddlCurrency.Items.FindByValue(ExistingPortalCurrency));
            //}

            //if (ddlPaymentProcessor.Items.Count > 0)
            //{
            //    string ExistingPortalPaymentProcessor = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalPaymentProcessor);
            //    ddlPaymentProcessor.SelectedIndex = ddlPaymentProcessor.Items.IndexOf(ddlPaymentProcessor.Items.FindByValue(ExistingPortalPaymentProcessor));
            //}

            //txtProcessorUserId.Text = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalProcessorUserId);
            //string ExistingPortalProcessorPassword = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalProcessorPassword);
            //txtProcessorPassword.Attributes.Add("value", ExistingPortalProcessorPassword);

            if (ddlDefaultLanguage.Items.Count > 0)
            {
                string ExistingDefaultLanguage = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultLanguage);
                ddlDefaultLanguage.SelectedIndex = ddlDefaultLanguage.Items.IndexOf(ddlDefaultLanguage.Items.FindByValue(ExistingDefaultLanguage));
            }

            if (ddlPortalTimeZone.Items.Count > 0)
            {
                string ExistingPortalTimeZone = pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalTimeZone);
                ddlPortalTimeZone.SelectedIndex = ddlPortalTimeZone.Items.IndexOf(ddlPortalTimeZone.Items.FindByValue(ExistingPortalTimeZone));
            }
        }

        private void BinSiteTemplates()
        {
            try
            {
                TemplateDataContext dbTemplage = new TemplateDataContext(SystemSetting.SageFrameConnectionString);
                var LINQ = dbTemplage.sp_TemplateGetList(GetPortalID, GetUsername);
                ddlTemplate.DataSource = LINQ;
                ddlTemplate.DataTextField = "TemplateTitle";
                ddlTemplate.DataValueField = "TemplateTitle";
                ddlTemplate.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindPageDlls()
        {
            try
            {
                PagesDataContext db = new PagesDataContext(SystemSetting.SageFrameConnectionString);
                var LINQParentPages = db.sp_PagePortalGetByCustomPrefix("---", true, false, GetPortalID, GetUsername, null, null);
                ddlLoginPage.Items.Clear();
                ddlLoginPage.DataSource = LINQParentPages;
                ddlLoginPage.DataTextField = "LevelPageName";
                ddlLoginPage.DataValueField = "SEOName";
                ddlLoginPage.DataBind();
                ddlLoginPage.Items.Insert(0, new ListItem("<Not Specified>", "-1"));


                ddlUserRegistrationPage.Items.Clear();
                ddlUserRegistrationPage.DataSource = LINQParentPages;
                ddlUserRegistrationPage.DataTextField = "LevelPageName";
                ddlUserRegistrationPage.DataValueField = "SEOName";
                ddlUserRegistrationPage.DataBind();
                ddlUserRegistrationPage.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                //ddlPortalUserActivation
                ddlPortalUserActivation.Items.Clear();
                ddlPortalUserActivation.DataSource = LINQParentPages;
                ddlPortalUserActivation.DataTextField = "LevelPageName";
                ddlPortalUserActivation.DataValueField = "SEOName";
                ddlPortalUserActivation.DataBind();
                ddlPortalUserActivation.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                //ddlPortalForgotPassword
                ddlPortalForgotPassword.Items.Clear();
                ddlPortalForgotPassword.DataSource = LINQParentPages;
                ddlPortalForgotPassword.DataTextField = "LevelPageName";
                ddlPortalForgotPassword.DataValueField = "SEOName";
                ddlPortalForgotPassword.DataBind();
                ddlPortalForgotPassword.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                //ddlPortalPageNotAccessible
                ddlPortalPageNotAccessible.Items.Clear();
                ddlPortalPageNotAccessible.DataSource = LINQParentPages;
                ddlPortalPageNotAccessible.DataTextField = "LevelPageName";
                ddlPortalPageNotAccessible.DataValueField = "SEOName";
                ddlPortalPageNotAccessible.DataBind();
                ddlPortalPageNotAccessible.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                //ddlPortalPageNotFound
                ddlPortalPageNotFound.Items.Clear();
                ddlPortalPageNotFound.DataSource = LINQParentPages;
                ddlPortalPageNotFound.DataTextField = "LevelPageName";
                ddlPortalPageNotFound.DataValueField = "SEOName";
                ddlPortalPageNotFound.DataBind();
                ddlPortalPageNotFound.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                //ddlPortalPasswordRecovery
                ddlPortalPasswordRecovery.Items.Clear();
                ddlPortalPasswordRecovery.DataSource = LINQParentPages;
                ddlPortalPasswordRecovery.DataTextField = "LevelPageName";
                ddlPortalPasswordRecovery.DataValueField = "SEOName";
                ddlPortalPasswordRecovery.DataBind();
                ddlPortalPasswordRecovery.Items.Insert(0, new ListItem("<Not Specified>", "-1"));

                //ddlPortalDefaultPage
                ddlPortalDefaultPage.Items.Clear();
                ddlPortalDefaultPage.DataSource = LINQParentPages;
                ddlPortalDefaultPage.DataTextField = "LevelPageName";
                ddlPortalDefaultPage.DataValueField = "SEOName";
                ddlPortalDefaultPage.DataBind();
                ddlPortalDefaultPage.Items.Insert(0, new ListItem("<Not Specified>", "-1"));


                //ddlPortalUserProfilePage
                ddlPortalUserProfilePage.Items.Clear();
                ddlPortalUserProfilePage.DataSource = LINQParentPages;
                ddlPortalUserProfilePage.DataTextField = "LevelPageName";
                ddlPortalUserProfilePage.DataValueField = "SEOName";
                ddlPortalUserProfilePage.DataBind();
                ddlPortalUserProfilePage.Items.Insert(0, new ListItem("<Not Specified>", "-1"));
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void Bindlistddls()
        {
            try
            {
                ListManagementDataContext db = new ListManagementDataContext(SystemSetting.SageFrameConnectionString);
                var LINQ = db.sp_GetListEntrybyNameAndID("Country", -1,GetCurrentCultureName);
                ddlDefaultLanguage.DataSource = LINQ;
                ddlDefaultLanguage.DataTextField = "Text";
                ddlDefaultLanguage.DataValueField = "Value";
                ddlDefaultLanguage.DataBind();
                //LINQ = db.sp_GetListEntrybyNameAndID("Processor", -1,GetCurrentCultureName);
                //ddlPaymentProcessor.DataSource = LINQ;
                //ddlPaymentProcessor.DataTextField = "Text";
                //ddlPaymentProcessor.DataValueField = "Value";
                //ddlPaymentProcessor.DataBind();

                ////ddlCurrency
                //LINQ = db.sp_GetListEntrybyNameAndID("Currency", -1,GetCurrentCultureName);
                //ddlCurrency.DataSource = LINQ;
                //ddlCurrency.DataTextField = "Text";
                //ddlCurrency.DataValueField = "Value";
                //ddlCurrency.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindddlTimeZone()
        {
            try
            {
                NameValueCollection nvlTimeZone = SageFrame.Localization.Localization.GetTimeZones(((PageBase)this.Page).GetCurrentCultureName);
                ddlPortalTimeZone.DataSource = nvlTimeZone;
                ddlPortalTimeZone.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            
        }

        private void BindRegistrationTypes()
        {
            rblUserRegistration.DataSource = SageFrameLists.UserRegistrationTypes();
            rblUserRegistration.DataValueField = "key";
            rblUserRegistration.DataTextField = "value";
            rblUserRegistration.DataBind();
        }

        //private void BinSearchEngines()
        //{
        //    ddlSearchEngine.DataSource = SageFrameLists.SearchEngines();
        //    ddlSearchEngine.DataTextField = "value";
        //    ddlSearchEngine.DataValueField = "key";
        //    ddlSearchEngine.DataBind();
        //}

        private void BindRBLWithREF(RadioButtonList rbl)
        {
            rbl.DataSource = SageFrameLists.YESNO();
            rbl.DataTextField = "value";
            rbl.DataValueField = "key";
            rbl.DataBind();
            rbl.RepeatColumns = 2;
            rbl.RepeatDirection = RepeatDirection.Horizontal;
        }

        private void BindYesNoRBL()
        {
            //rblPortalShowProfileLink
            BindRBLWithREF(rblPortalShowProfileLink);            

            ////rblPortalShowSubscribe
            //BindRBLWithREF(rblPortalShowSubscribe);            

            ////rblPortalShowLogo
            //BindRBLWithREF(rblPortalShowLogo);            

            ////rblPortalShowFooterLink
            //BindRBLWithREF(rblPortalShowFooterLink);

            ////rblPortalShowFooter
            //BindRBLWithREF(rblPortalShowFooter);

            ////rblPortalShowBreadCrum
            //BindRBLWithREF(rblPortalShowBreadCrum);            

            ////rblPortalShowCopyRight
            //BindRBLWithREF(rblPortalShowCopyRight);

            ////rblPortalShowLoginStatus
            //BindRBLWithREF(rblPortalShowLoginStatus);

            //rblIsPortalMenuIsImage
            BindRBLWithREF(rblIsPortalMenuIsImage);
        }

        private void RefreshPage()
        {
            try
            {
                HttpContext.Current.Cache.Remove("SageSetting");
                BindData();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void SavePortalSettings()
        {
            try
            {
                SettingProvider sageSP = new SettingProvider();
                //Add Single Key Values that may contain Comma values so need to be add sepratly
                #region "Single Key Value Add/Updatge"

                //SageFrameSettingKeys.PageTitle
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.PageTitle,
                    txtPortalTitle.Text.Trim(), GetUsername, GetPortalID.ToString());

                //SageFrameSettingKeys.MetaDescription
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.MetaDescription,
                    txtDescription.Text, GetUsername, GetPortalID.ToString());

                //SageFrameSettingKeys.MetaKeywords
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.MetaKeywords,
                    txtKeyWords.Text, GetUsername, GetPortalID.ToString());

                //SageFrameSettingKeys.PortalLogoTemplate
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.PortalLogoTemplate,
                    txtLogoTemplate.Text.Trim(), GetUsername, GetPortalID.ToString());

                //SageFrameSettingKeys.PortalCopyright
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.PortalCopyright,
                    txtCopyright.Text.Trim(), GetUsername, GetPortalID.ToString());

                //SageFrameSettingKeys.PortalTimeZone
                sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.PortalTimeZone,
                    ddlPortalTimeZone.SelectedItem.Value, GetUsername, GetPortalID.ToString());

                #endregion

                //For Multiple Keys and Values
                #region "Multiple Key Value Add/Update"

                StringBuilder sbSettingKey = new StringBuilder();
                StringBuilder sbSettingValue = new StringBuilder();
                StringBuilder sbSettingType = new StringBuilder();

                //Collecting Setting Values
                

                //SageFrameSettingKeys.SiteAdminEmailAddress
                sbSettingKey.Append(SageFrameSettingKeys.SiteAdminEmailAddress + ",");
                sbSettingValue.Append(txtSiteAdminEmailAddress.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalUserProfileMaxImageSize
                //sbSettingKey.Append(SageFrameSettingKeys.PortalUserProfileMaxImageSize + ",");
                //sbSettingValue.Append(txtPortalUserProfileMaxImageSize.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                ////SageFrameSettingKeys.PortalUserProfileMediumImageSize
                //sbSettingKey.Append(SageFrameSettingKeys.PortalUserProfileMediumImageSize + ",");
                //sbSettingValue.Append(txtPortalUserProfileMediumImageSize.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                ////SageFrameSettingKeys.PortalUserProfileSmallImageSize
                //sbSettingKey.Append(SageFrameSettingKeys.PortalUserProfileSmallImageSize + ",");
                //sbSettingValue.Append(txtPortalUserProfileSmallImageSize.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");


                ////SageFrameSettingKeys.PortalSearchEngine
                //sbSettingKey.Append(SageFrameSettingKeys.PortalSearchEngine + ",");
                //sbSettingValue.Append(ddlSearchEngine.SelectedItem.Value + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                
                //SageFrameSettingKeys.PortalGoogleAdSenseID
                sbSettingKey.Append(SageFrameSettingKeys.PortalGoogleAdSenseID + ",");
                sbSettingValue.Append(txtPortalGoogleAdSenseID.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalCssTemplate
                sbSettingKey.Append(SageFrameSettingKeys.PortalCssTemplate + ",");
                sbSettingValue.Append(ddlTemplate.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");


                //SageFrameSettingKeys.PortalShowProfileLink
                sbSettingKey.Append(SageFrameSettingKeys.PortalShowProfileLink + ",");
                sbSettingValue.Append(rblPortalShowProfileLink.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.RememberCheckbox
                sbSettingKey.Append(SageFrameSettingKeys.RememberCheckbox + ",");
                sbSettingValue.Append(chkEnableRememberme.Checked + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //CssJs Optimization
                sbSettingKey.Append(SageFrameSettingKeys.OptimizeCss+",");
                sbSettingValue.Append(chkOptCss.Checked + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                sbSettingKey.Append(SageFrameSettingKeys.OptimizeJs + ",");
                sbSettingValue.Append(chkOptJs.Checked + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");


                
                ////SageFrameSettingKeys.PortalShowSubscribe
                //sbSettingKey.Append(SageFrameSettingKeys.PortalShowSubscribe + ",");
                //sbSettingValue.Append(rblPortalShowSubscribe.SelectedItem.Value + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                
                //SageFrameSettingKeys.PortalShowLogo
                //sbSettingKey.Append(SageFrameSettingKeys.PortalShowLogo + ",");
                //sbSettingValue.Append(rblPortalShowLogo.SelectedItem.Value + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                
                //SageFrameSettingKeys.PortalShowFooterLink
                //sbSettingKey.Append(SageFrameSettingKeys.PortalShowFooterLink + ",");
                //sbSettingValue.Append(rblPortalShowFooterLink.SelectedItem.Value + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                
                ////SageFrameSettingKeys.PortalShowFooter
                //sbSettingKey.Append(SageFrameSettingKeys.PortalShowFooter + ",");
                //sbSettingValue.Append(rblPortalShowFooter.SelectedItem.Value + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                
                ////SageFrameSettingKeys.PortalShowBreadCrum
                //sbSettingKey.Append(SageFrameSettingKeys.PortalShowBreadCrum + ",");
                //sbSettingValue.Append(rblPortalShowBreadCrum.SelectedItem.Value + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                
                ////SageFrameSettingKeys.PortalShowCopyRight
                //sbSettingKey.Append(SageFrameSettingKeys.PortalShowCopyRight + ",");
                //sbSettingValue.Append(rblPortalShowCopyRight.SelectedItem.Value + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                
                ////SageFrameSettingKeys.PortalShowLoginStatus
                //sbSettingKey.Append(SageFrameSettingKeys.PortalShowLoginStatus + ",");
                //sbSettingValue.Append(rblPortalShowLoginStatus.SelectedItem.Value + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalUserRegistration
                sbSettingKey.Append(SageFrameSettingKeys.PortalUserRegistration + ",");
                sbSettingValue.Append(rblUserRegistration.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

               
                //SageFrameSettingKeys.IsPortalMenuIsImage
                sbSettingKey.Append(SageFrameSettingKeys.IsPortalMenuIsImage + ",");
                sbSettingValue.Append(rblIsPortalMenuIsImage.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalMenuImageExtension
                sbSettingKey.Append(SageFrameSettingKeys.PortalMenuImageExtension + ",");
                sbSettingValue.Append(txtPortalMenuImageExtension.Text.Trim() + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");



                //SageFrameSettingKeys.PortalLoginpage
                sbSettingKey.Append(SageFrameSettingKeys.PortalLoginpage + ",");
                sbSettingValue.Append(ddlLoginPage.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalUserActivation
                sbSettingKey.Append(SageFrameSettingKeys.PortalUserActivation + ",");
                sbSettingValue.Append(ddlPortalUserActivation.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalRegistrationPage
                sbSettingKey.Append(SageFrameSettingKeys.PortalRegistrationPage + ",");
                sbSettingValue.Append(ddlUserRegistrationPage.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalForgotPassword
                sbSettingKey.Append(SageFrameSettingKeys.PortalForgotPassword + ",");
                sbSettingValue.Append(ddlPortalForgotPassword.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");
                
                //SageFrameSettingKeys.PortalPageNotAccessible
                sbSettingKey.Append(SageFrameSettingKeys.PortalPageNotAccessible + ",");
                sbSettingValue.Append(ddlPortalPageNotAccessible.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalPageNotFound
                sbSettingKey.Append(SageFrameSettingKeys.PortalPageNotFound + ",");
                sbSettingValue.Append(ddlPortalPageNotFound.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                
                //SageFrameSettingKeys.PortalPasswordRecovery
                sbSettingKey.Append(SageFrameSettingKeys.PortalPasswordRecovery + ",");
                sbSettingValue.Append(ddlPortalPasswordRecovery.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //PortalUserProfilePage
                sbSettingKey.Append(SageFrameSettingKeys.PortalUserProfilePage + ",");
                sbSettingValue.Append(ddlPortalUserProfilePage.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                //PortalDefaultPage
                sbSettingKey.Append(SageFrameSettingKeys.PortalDefaultPage + ",");
                sbSettingValue.Append(ddlPortalDefaultPage.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

                ////SageFrameSettingKeys.PortalCurrency
                //sbSettingKey.Append(SageFrameSettingKeys.PortalCurrency + ",");
                //sbSettingValue.Append(ddlCurrency.SelectedItem.Value + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                ////SageFrameSettingKeys.PortalPaymentProcessor
                //sbSettingKey.Append(SageFrameSettingKeys.PortalPaymentProcessor + ",");
                //sbSettingValue.Append(ddlPaymentProcessor.SelectedItem.Value + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                ////SageFrameSettingKeys.PortalProcessorUserId
                //sbSettingKey.Append(SageFrameSettingKeys.PortalProcessorUserId + ",");
                //sbSettingValue.Append(txtProcessorUserId.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                ////SageFrameSettingKeys.PortalProcessorPassword
                //sbSettingKey.Append(SageFrameSettingKeys.PortalProcessorPassword + ",");
                //sbSettingValue.Append(txtProcessorPassword.Text.Trim() + ",");
                //sbSettingType.Append(SettingType.SiteAdmin + ",");

                //SageFrameSettingKeys.PortalDefaultLanguage
                sbSettingKey.Append(SageFrameSettingKeys.PortalDefaultLanguage + ",");
                sbSettingValue.Append(ddlDefaultLanguage.SelectedItem.Value + ",");
                sbSettingType.Append(SettingType.SiteAdmin + ",");

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
                

                sageSP.SaveSageSettings(SettingTypes, SettingKeys, SettingValues, GetUsername, GetPortalID.ToString());
                HttpContext.Current.Cache.Remove("SageSetting");
                BindData();

                #endregion

                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("PortalSettings", "PortalSettingIsSavedSuccessfully"), "", SageMessageType.Success);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }       

        protected void imbSave_Click(object sender, ImageClickEventArgs e)
        {
            SavePortalSettings();
        }

        //protected void lnkSave_Click(object sender, EventArgs e)
        //{
        //    SavePortalSettings();
        //}

        protected void imbRefresh_Click(object sender, ImageClickEventArgs e)
        {
            RefreshPage();
        }

        //protected void lnkRefresh_Click(object sender, EventArgs e)
        //{
        //    RefreshPage();
        //}
        protected void ddlDefaultLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFlagImage();
            ViewState["SelectedLanguageCulture"] = this.ddlDefaultLanguage.SelectedValue;
        }
        protected void GetFlagImage()
        {
            string code = this.ddlDefaultLanguage.SelectedValue;
            imgFlag.ImageUrl = ResolveUrl(this.Request.ApplicationPath+ "/images/flags/" + code.Substring(code.IndexOf("-") + 1) + ".png");
        }
        protected void rbLanguageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbLanguageType.SelectedIndex)
            {
                case 0:
                    GetLanguageList();                   
                    break;
                case 1:
                    LoadNativeNames();
                    break;
            }
        }
        protected void LoadNativeNames()
        {
            languageMode = "Native";
            GetLanguageList();           
        }
        public void GetLanguageList()
        {
            string mode = languageMode == "Native" ? "NativeName" : "LanguageName";
            List<Language> lstAvailableLocales = LocaleController.AddNativeNamesToList(LocalizationSqlDataProvider.GetAvailableLocales());

            ddlDefaultLanguage.DataSource = lstAvailableLocales;
            ddlDefaultLanguage.DataTextField = mode;
            ddlDefaultLanguage.DataValueField = "LanguageCode";
            ddlDefaultLanguage.DataBind();
            ddlDefaultLanguage.SelectedIndex = ddlDefaultLanguage.Items.IndexOf(ddlDefaultLanguage.Items.FindByValue(ViewState["SelectedLanguageCulture"].ToString()));
            ViewState["RowCount"] = lstAvailableLocales.Count;

        }

        protected void btnRefreshCache_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Cache.Remove("SageFrameCss");
            HttpContext.Current.Cache.Remove("SageFrameJs");
            string optimized_path = Server.MapPath(SageFrameConstants.OptimizedResourcePath);
            IOHelper.DeleteDirectoryFiles(optimized_path,".js,.css");
            if (File.Exists(Server.MapPath(SageFrameConstants.OptimizedCssMap)))
            {
                XmlHelper.DeleteNodes(Server.MapPath(SageFrameConstants.OptimizedCssMap),"resourcemap/resourcemap");
            } 
            if (File.Exists(Server.MapPath(SageFrameConstants.OptimizedJsMap)))
            {
                XmlHelper.DeleteNodes(Server.MapPath(SageFrameConstants.OptimizedJsMap), "resourcemap/resourcemap");
            }
        }
}
}