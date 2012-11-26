/*
AspxCommerce® - http://www.aspxcommerce.com
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
using SageFrame.Web;
using SageFrame.Security;
using SageFrame.Security.Entities;
using AspxCommerce.Core;
using SageFrame.Framework;
using System.Web.Security;

public partial class Modules_AspxUserDashBoard_UserDashBoard : BaseAdministrationUserControl
{
    public int storeID, portalID, customerID;
   
    public string sessionCode = string.Empty;

    public string cultureName, userName, userEmail, userFirstName, userLastName, userEmailWishList;

    public string allowMultipleAddress, allowWishListMyAccount;

    public string userIP;
    public string countryName = string.Empty;
   
    public string aspxfilePath;
    public bool IsUseFriendlyUrls = true;

    MembershipController m = new MembershipController();
    protected void page_init(object sender, EventArgs e)
    {
        try
        {
            InitializeJS();
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            aspxfilePath = ResolveUrl(modulePath).Replace("AspxUserDashBoard", "AspxItemsManagement");//This is for Download file Path
    
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
            SageFrameConfig pagebase = new SageFrameConfig();
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            if (Membership.GetUser() != null)
            {
                if (!IsPostBack)
                {
                    IncludeCss("UserDashBoard", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/StarRating/jquery.rating.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                                "/Templates/" + TemplateName + "/css/PopUp/style.css", "/Templates/" + TemplateName + "/css/JQueryUIFront/jquery.ui.all.css", "/Templates/" + TemplateName + "/css/PasswordValidation/jquery.validate.password.css");

                    IncludeJs("UserDashBoard", "/js/JQueryUI/jquery-ui-1.8.10.custom.js", "/js/jDownload/jquery.jdownload.js", "/js/DateTime/date.js", "/js/MessageBox/jquery.easing.1.3.js",
                        "/js/MessageBox/alertbox.js", "/js/StarRating/jquery.MetaData.js", "/js/FormValidation/jquery.validate.js", "/js/PasswordValidation/jquery.validate.password.js",
                        "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js",
                        "/js/StarRating/jquery.rating.js", "/js/PopUp/custom.js");

                    storeID = GetStoreID;
                    portalID = GetPortalID;
                    customerID = GetCustomerID;
                    userName = GetUsername;
                    cultureName = GetCurrentCultureName;
                    UserInfo sageUser = m.GetUserDetails(GetPortalID, GetUsername);
                    if (HttpContext.Current.Session.SessionID != null)
                    {
                        sessionCode = HttpContext.Current.Session.SessionID.ToString();
                    }
                    if (sageUser.UserName != null)
                    {
                        userEmail = sageUser.Email;
                        userFirstName = sageUser.FirstName;
                        userLastName = sageUser.LastName;
                        MembershipUser userDetail = Membership.GetUser(GetUsername);//added later for wishlist
                        userEmailWishList = userDetail.Email;//added later for wishlist
                        userIP = HttpContext.Current.Request.UserHostAddress;
                        IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
                        ipToCountry.GetCountry(userIP, out countryName);
                    }

                    //else
                    //{
                    //    if (GetPortalID > 1)
                    //    {
                    //        Response.Redirect( "~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + ".aspx") ;

                    //    }
                    //    else
                    //    {
                    //        Response.Redirect( "~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage) + ".aspx");
                    //    }
                    //}

                    StoreSettingConfig ssc = new StoreSettingConfig();
                    allowMultipleAddress = ssc.GetStoreSettingsByKey(StoreSetting.AllowUsersToCreateMultipleAddress, storeID, portalID, cultureName);
                    allowWishListMyAccount = ssc.GetStoreSettingsByKey(StoreSetting.EnableWishList, storeID, portalID, cultureName);
                    //added for wishlist in dashboard along with required variables and js form WishItemList module
                    //noImageWishList = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, storeID, portalID, cultureName);
                    //enableWishList = ssc.GetStoreSettingsByKey(StoreSetting.EnableWishList, storeID, portalID, cultureName);
                    //showImageInWishlist = ssc.GetStoreSettingsByKey(StoreSetting.ShowItemImagesInWishList, storeID, portalID, cultureName);
                    //allowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, storeID, portalID, cultureName);





                }
            }
            else
            {
                if (IsUseFriendlyUrls)
                {
                    if (GetPortalID > 1)
                    {
                        Response.Redirect(ResolveUrl("~/portal/" + GetPortalSEOName + "/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage)) + ".aspx?ReturnUrl=" +Request.Url.ToString(),false);
                    }
                    else
                    {
                        Response.Redirect(ResolveUrl("~/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage)) + ".aspx?ReturnUrl=" + Request.Url.ToString(),false);
                    }
                }

                else
                {
                    Response.Redirect(ResolveUrl("~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage)) + "?ReturnUrl=" + Request.Url.ToString(),false);
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
         Page.ClientScript.RegisterClientScriptInclude("pack", ResolveUrl("~/js/StarRating/jquery.rating.pack.js"));
         Page.ClientScript.RegisterClientScriptInclude("J12", ResolveUrl("~/js/encoder.js"));
    }  
}
