using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.Collections;
using SageFrame.Framework;
using System.Web.Security;
using SageFrame;
using SageFrame.Web.Utilities;
using SageFrame.Web.Common.SEO;
using AspxCommerce.Core;

public partial class Modules_AspxDetails__AspxItemDetails_ItemDetails : BaseAdministrationUserControl
{
    public string itemSKU;
    public int itemID;
    public string itemName;
    public int storeID, portalID, UserModuleID, customerID, minimumItemQuantity, maximumItemQuantity, maxCompareItemCount,relatedItemsCount;
    public bool allowMultipleReviewPerIP, allowMultipleReviewPerUser;
    public string userName, cultureName;
    public string userEmail = string.Empty;
    //public string attributeSetId;
    //public string itemTypeId;
    public string userIP;
    public string countryName = string.Empty;
    public string sessionCode = string.Empty;
    //public string userEmail = string.Empty;
    public string aspxfilePath;
    public string noItemDetailImagePath, enableEmailFriend,allowAnonymousReviewRate,allowOutStockPurchase, allowWishListItemDetail, allowCompareItemDetail;
    public bool IsUseFriendlyUrls = true;
    //public string costVariantData = string.Empty;   

    protected void page_init(object sender, EventArgs e)
    {
        // modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        ////This is for Download file Path  
        aspxfilePath = ResolveUrl("~") + "Modules/AspxCommerce/AspxItemsManagement/";

        try
        {
            SageFrameConfig pagebase = new SageFrameConfig();
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            SageFrameRoute parentPage = (SageFrameRoute)this.Page;

            itemSKU = parentPage.Key;//Request.QueryString["itemId"];
            //itemName = "item3"; //Request.QueryString["itemName"];

            if (!IsPostBack)
            {
                storeID = GetStoreID;
                portalID = GetPortalID;
                customerID = GetCustomerID;
                userName = GetUsername;
                cultureName = GetCurrentCultureName;
                if (HttpContext.Current.Session.SessionID != null)
                {
                    sessionCode = HttpContext.Current.Session.SessionID.ToString();
                }
                OverRideSEOInfo(itemSKU, storeID, portalID, userName, cultureName);
                userIP = HttpContext.Current.Request.UserHostAddress;
                IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
                ipToCountry.GetCountry(userIP, out countryName);

                if (Membership.GetUser() != null)
                {
                    MembershipUser userDetail = Membership.GetUser(GetUsername);
                    userEmail = userDetail.Email;
                }

                StoreSettingConfig ssc = new StoreSettingConfig();
                noItemDetailImagePath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, storeID, portalID, cultureName);
                enableEmailFriend = ssc.GetStoreSettingsByKey(StoreSetting.EnableEmailAFriend, storeID, portalID, cultureName);
                allowAnonymousReviewRate = ssc.GetStoreSettingsByKey(StoreSetting.AllowAnonymousUserToWriteItemRatingAndReviews, storeID, portalID, cultureName);
                minimumItemQuantity =int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MinimumItemQuantity, storeID, portalID, cultureName));
                maximumItemQuantity =int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MaximumItemQuantity, storeID, portalID, cultureName));
                allowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, storeID, portalID, cultureName);
                maxCompareItemCount =int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MaxNoOfItemsToCompare, storeID, portalID, cultureName));
                relatedItemsCount =int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.NoOfRelatedCartItems, storeID, portalID, cultureName));
                allowWishListItemDetail = ssc.GetStoreSettingsByKey(StoreSetting.EnableWishList, storeID, portalID, cultureName);
                allowCompareItemDetail = ssc.GetStoreSettingsByKey(StoreSetting.EnableCompareItems, storeID, portalID, cultureName);
                allowMultipleReviewPerUser = bool.Parse(ssc.GetStoreSettingsByKey(StoreSetting.AllowMultipleReviewsPerUser, storeID, portalID, cultureName));
                allowMultipleReviewPerIP = bool.Parse(ssc.GetStoreSettingsByKey(StoreSetting.AllowMultipleReviewsPerIP, storeID, portalID, cultureName));
            }

            if (SageUserModuleID != "")
            {
                UserModuleID = int.Parse(SageUserModuleID);
            }
            else
            {
                UserModuleID = 0;
            }

            InitializeJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void OverRideSEOInfo(string itemSKU, int storeID, int portalID, string userName, string cultureName)
    {
        ItemSEOInfo dtItemSEO = GetSEOSettingsBySKU(itemSKU, storeID, portalID, userName, cultureName);
         if (dtItemSEO != null)
         {
             itemID = int.Parse(dtItemSEO.ItemID.ToString());
             itemName = dtItemSEO.Name.ToString();
             string PageTitle = dtItemSEO.MetaTitle.ToString();
             string PageKeyWords = dtItemSEO.MetaKeywords.ToString();
             string PageDescription = dtItemSEO.MetaDescription.ToString();           

             if (!string.IsNullOrEmpty(PageTitle))
                 SEOHelper.RenderTitle(this.Page, PageTitle, false, true, this.GetPortalID);

             if (!string.IsNullOrEmpty(PageKeyWords))
                 SEOHelper.RenderMetaTag(this.Page, "KEYWORDS", PageKeyWords, true);

             if (!string.IsNullOrEmpty(PageDescription))
                 SEOHelper.RenderMetaTag(this.Page, "DESCRIPTION", PageDescription, true);
         }
    }

    public ItemSEOInfo GetSEOSettingsBySKU(string itemSKU, int storeID, int portalID, string userName, string cultureName)
    {
        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
        ParaMeter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        ParaMeter.Add(new KeyValuePair<string, object>("@Username", userName));
        ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        SQLHandler sqlH = new SQLHandler();
        return sqlH.ExecuteAsObject <ItemSEOInfo>("usp_Aspx_ItemsSEODetailsBySKU", ParaMeter);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IncludeCss("ItemDetails", "/Templates/" + TemplateName + "/css/ImageGallery/styles.css", "/Templates/" + TemplateName + "/css/ImageGallery/jquery.gzoom.css", "/Templates/" + TemplateName + "/css/PopUp/style.css", "/Templates/" + TemplateName + "/css/StarRating/jquery.rating.css", "/Templates/" + TemplateName + "/css/JQueryUIFront/jquery-ui.all.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css");

            IncludeJs("ItemDetails", "/js/ImageGallery/jquery.pikachoose.js", "/js/ImageGallery/jquery.gzoom.js", "/js/ImageGallery/jquery.mousewheel.js", "/js/JQueryUI/jquery-ui-1.8.10.custom.js",
                                "/js/jDownload/jquery.jdownload.js", "/js/MessageBox/alertbox.js", "/js/DateTime/date.js", "/js/PopUp/custom.js", "/js/FormValidation/jquery.validate.js",
                                "/js/StarRating/jquery.rating.js", "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js", "/js/CurrencyFormat/jquery.formatCurrency.all.js", "/js/Session.js"
                                ,"/Modules/Admin/DetailsBrowse/js/ItemDetails.js");
        }
       
    }

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("J12", ResolveUrl("~/js/encoder.js"));
        Page.ClientScript.RegisterClientScriptInclude("pack", ResolveUrl("~/js/StarRating/jquery.rating.pack.js"));
        Page.ClientScript.RegisterClientScriptInclude("metadata", ResolveUrl("~/js/StarRating/jquery.MetaData.js"));
        Page.ClientScript.RegisterClientScriptInclude("Paging", ResolveUrl("~/js/Paging/jquery.pagination.js"));
    }
}
