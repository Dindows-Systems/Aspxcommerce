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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.Collections;
using System.Web.Script.Serialization;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Security.Helpers;
using System.Web.Security;
using System.IO;
using SageFrame.RolesManagement;
using SageFrame.Web.Utilities;
using SageFrame.Security.Crypto;
using SageFrame.Shared;
using AspxCommerce.Core;


public partial class Modules_AspxCheckoutInformationContent_CheckoutInformationContent : BaseAdministrationUserControl
{
    public int storeID, portalID, customerID;
    public string userName;
    public string cultureName;
    public string sessionCode = string.Empty;
    string strRoles = string.Empty;
    public bool RegisterURL = true;
    public bool IsFShipping = false;
    public decimal Discount = 0;
    public string CouponCode = string.Empty;
    public string userIP = string.Empty;
    public string noImageCheckOutInfoPath, myAccountURL;
    public bool IsUseFriendlyUrls = true;

    SageFrameConfig pagebase = new SageFrameConfig();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
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
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);

            if (HttpContext.Current.Session.SessionID != null)
            {
                sessionCode = HttpContext.Current.Session.SessionID.ToString();
            }

            if (Session["IsFreeShipping"] != null)
            {
                IsFShipping = bool.Parse(Session["IsFreeShipping"].ToString());

            }
            if (Session["DiscountAmount"] != null)
            {
                Discount = decimal.Parse(Session["DiscountAmount"].ToString());

            }
            if (Session["CouponCode"] != null)
            {
                CouponCode = Session["CouponCode"].ToString();

            }

            if (!IsPostBack)
            {
                IncludeCss("CheckOutInformationContent", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/JQueryUIFront/jquery.ui.all.css");
                IncludeJs("CheckOutInformationContent", "/js/JQueryUI/jquery-ui-1.8.10.custom.js", "/js/FormValidation/jquery.validate.js", "/js/jquery.cookie.js", "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", "/Modules/AspxCommerce/AspxCheckoutWithSingleAddress/js/SingleCheckOut.js");
                storeID = GetStoreID;
                portalID = GetPortalID;
                userName = GetUsername;
                customerID = GetCustomerID;
                cultureName = GetCurrentCultureName;
                userIP = HttpContext.Current.Request.UserHostAddress;
                if (HttpContext.Current.Session.SessionID != null)
                {
                    sessionCode = HttpContext.Current.Session.SessionID.ToString();
                }

                StoreSettingConfig ssc = new StoreSettingConfig();
                noImageCheckOutInfoPath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, storeID, portalID, cultureName);
                myAccountURL = ssc.GetStoreSettingsByKey(StoreSetting.MyAccountURL, storeID, portalID, cultureName);
                
                HideSignUp();
                PasswordAspx.Attributes.Add("onkeypress", "return clickButton(event,'" + LoginButton.ClientID + "')");
                hypForgetPassword.Text = "Forget Password?";
                if (IsUseFriendlyUrls)
                {
                    if (GetPortalID > 1)
                    {
                        hypForgetPassword.NavigateUrl = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalForgotPassword) + ".aspx");
                    }
                    else
                    {
                        hypForgetPassword.NavigateUrl = ResolveUrl("~/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalForgotPassword) + ".aspx");
                    }
                    signup.Attributes.Add("href", ResolveUrl("~/User-Registration.aspx"));
                    signup1.Attributes.Add("href", ResolveUrl("~/User-Registration.aspx"));
                }
                else
                {
                    hypForgetPassword.NavigateUrl = ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalForgotPassword));
                    signup.Attributes.Add("href", ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=/User-Registration"));
                    signup1.Attributes.Add("href", ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=/User-Registration"));
                }
                if (pagebase.GetSettingBollByKey(SageFrameSettingKeys.RememberCheckbox))
                {
                    RememberMe.Visible = true;
                    lblrmnt.Visible = true;
                }
                else
                {
                    RememberMe.Visible = false;
                    lblrmnt.Visible = false;
                }
            }

            if (HttpContext.Current.User != null)
            {
                if (Membership.GetUser() != null)
                {
                    MultiView1.ActiveViewIndex = 1;
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                }
            }
            else
            {
                MultiView1.ActiveViewIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("J12", ResolveUrl("~/js/encoder.js"));     
    }

    private void HideSignUp()
    {
        int UserRegistrationType = pagebase.GetSettingIntByKey(SageFrameSettingKeys.PortalUserRegistration);
        RegisterURL = UserRegistrationType > 0 ? true : false;
        if (!RegisterURL)
        {
            this.divSignUp.Visible = false;
        }
    }

    public void SetUserRoles(string strRoles)
    {
        Session["SageUserRoles"] = strRoles;
        HttpCookie cookie = HttpContext.Current.Request.Cookies["SageUserRolesCookie"];
        if (cookie == null)
        {
            cookie = new HttpCookie("SageUserRolesCookie");
        }
        cookie["SageUserRolesProtected"] = strRoles;
        HttpContext.Current.Response.Cookies.Add(cookie);
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        MembershipController member = new MembershipController();
        RoleController role = new RoleController();
        UserInfo user = member.GetUserDetails(GetPortalID, UserName.Text);
        if (user.UserExists && user.IsApproved)
        {
            if (!(string.IsNullOrEmpty(UserName.Text) && string.IsNullOrEmpty(PasswordAspx.Text)))
            {
                if (PasswordHelper.ValidateUser(user.PasswordFormat, PasswordAspx.Text, user.Password, user.PasswordSalt))
                {
                    string userRoles = role.GetRoleNames(user.UserName, GetPortalID);
                    strRoles += userRoles;
                    if (strRoles.Length > 0)
                    {
                        SetUserRoles(strRoles);
                        SessionTracker sessionTracker = (SessionTracker)Session["Tracker"];
                        sessionTracker.PortalID = GetPortalID.ToString();
                        sessionTracker.Username = UserName.Text;
                        Session["Tracker"] = sessionTracker;
                        SageFrame.Web.SessionLog SLog = new SageFrame.Web.SessionLog();
                        SLog.SessionTrackerUpdateUsername(sessionTracker, sessionTracker.Username, GetPortalID.ToString());

                        if (Request.QueryString["ReturnUrl"] != null)
                        {
                            FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked);
                            string PageNotFoundPage = Path.Combine(this.Request.ApplicationPath.ToString(), pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalPageNotFound) + ".aspx").Replace("\\", "/"); ;
                            string UserRegistrationPage = Path.Combine(this.Request.ApplicationPath.ToString(), pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalRegistrationPage) + ".aspx").Replace("\\", "/"); ;
                            string PasswordRecoveryPage = Path.Combine(this.Request.ApplicationPath.ToString(), pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalPasswordRecovery) + ".aspx").Replace("\\", "/"); ;
                            string ForgotPasswordPage = Path.Combine(this.Request.ApplicationPath.ToString(), pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalForgotPassword) + ".aspx").Replace("\\", "/"); ;
                            string PageNotAccessiblePage = Path.Combine(this.Request.ApplicationPath.ToString(), pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalPageNotAccessible) + ".aspx").Replace("\\", "/"); ;

                            string ReturnUrlPage = Request.QueryString["ReturnUrl"].Replace("%2f", "-").ToString();

                            if (ReturnUrlPage == PageNotFoundPage || ReturnUrlPage == UserRegistrationPage || ReturnUrlPage == PasswordRecoveryPage || ReturnUrlPage == ForgotPasswordPage || ReturnUrlPage == PageNotAccessiblePage)
                            {
                                Response.Redirect("~/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx", false);
                            }
                            else
                            {
                                Response.Redirect(ResolveUrl(Request.QueryString["ReturnUrl"].ToString()), false);
                            }
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked);
                            if (IsUseFriendlyUrls)
                            {
                                if (GetPortalID > 1)
                                {
                                    Response.Redirect("~/portal/" + GetPortalSEOName + "/" + "Single-Address-Checkout.aspx", false);
                                }
                                else
                                {
                                    Response.Redirect("~/" + "Single-Address-Checkout" + ".aspx", false);
                                }
                            }
                            else if (!IsUseFriendlyUrls)
                            {
                                if (GetPortalID > 1)
                                {
                                    Response.Redirect("~/portal/" + GetPortalSEOName + "/" + "Single-Address-Checkout", false);
                                }
                                else
                                {
                                    Response.Redirect("~/" + "Single-Address-Checkout", false);
                                }
                            }
                            else
                            {
                                Response.Redirect(ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage)), false);
                            }
                        }

                        //update Cart for that User in AspxCommerce
                        //TODO:: get customerID from userNAme 
                        int customerID = GetCustomerID;
                        if (customerID == 0)
                        {
                            SettingProvider objSP = new SettingProvider();
                            CustomerGeneralInfo sageUserCust = objSP.CustomerIDGetByUsername(user.UserName, GetPortalID, GetStoreID);
                            if (sageUserCust != null)
                            {
                                customerID = sageUserCust.CustomerID;
                            }
                        }
                        UpdateCartAnonymoususertoRegistered(GetStoreID, GetPortalID, customerID, sessionCode);
                    }
                    else
                    {
                        FailureText.Text = GetSageMessage("UserLogin", "Youarenotauthenticatedtothisportal");//"You are not authenticated to this portal!";
                    }
                }
                else
                {
                    FailureText.Text = GetSageMessage("UserLogin", "UsernameandPasswordcombinationdoesntmatched");//"Username and Password combination doesn't matched!";
                }
            }
        }
        else
        {
            FailureText.Text = GetSageMessage("UserLogin", "UserDoesnotExist");
        }
    }

    public int GetCustomerIDByUserName(int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsScalar<int>("usp_Aspx_GetCustomerIDByUserName", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool UpdateCartAnonymoususertoRegistered(int storeID, int portalID, int customerID, string sessionCode)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsBool("usp_Aspx_UpdateCartAnonymoususertoRegistered", ParaMeter, "@IsUpdate");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}