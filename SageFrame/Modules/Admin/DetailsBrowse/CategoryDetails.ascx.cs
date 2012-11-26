using System;
using System.Collections.Generic;
using System.Web;
using SageFrame.Web;
using SageFrame;
using SageFrame.Framework;
using SageFrame.Web.Common.SEO;
using SageFrame.Web.Utilities;
using AspxCommerce.Core;

public partial class Modules_AspxDetails_AspxCategoryDetails_CategoryDetails : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID;
    public string UserName, CultureName;
    public string UserIP;
    public string CountryName = string.Empty;
    public string SessionCode = string.Empty;
    //public string categoryName = "";
    public string Categorykey = "";
    public string NoImageCategoryDetailPath, AllowOutStockPurchase, AllowWishListCategory;
    protected void page_init(object sender, EventArgs e)
    {
        try
        {
            // categoryId = "";// Int32.Parse(Request.QueryString["catId"]);
            // categoryName = ""; // Request.QueryString["catName"];
            SageFrameRoute parentPage = (SageFrameRoute)this.Page;
            Categorykey = parentPage.Key;
            Categorykey = Categorykey.Replace("ampersand", "&").Replace("-", " ");
            if (!IsPostBack)
            {

                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CustomerID = GetCustomerID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                if (HttpContext.Current.Session.SessionID != null)
                {
                    SessionCode = HttpContext.Current.Session.SessionID.ToString();
                }
                OverRideSEOInfo(Categorykey, StoreID, PortalID, UserName, CultureName);

                UserIP = HttpContext.Current.Request.UserHostAddress;
                IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
                ipToCountry.GetCountry(UserIP, out CountryName);

                StoreSettingConfig ssc = new StoreSettingConfig();
                NoImageCategoryDetailPath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID, CultureName);
                AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, StoreID, PortalID, CultureName);
                AllowWishListCategory = ssc.GetStoreSettingsByKey(StoreSetting.EnableWishList, StoreID, PortalID, CultureName);
               
            }
            
            InitializeJS();
           
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void OverRideSEOInfo(string categorykey, int storeID, int portalID, string userName, string cultureName)
    {
        CategorySEOInfo dtCatSEO = GetSEOSettingsByCategoryName(categorykey, storeID, portalID, userName, cultureName);
        if (dtCatSEO != null)
        {
            string PageTitle = dtCatSEO.MetaTitle.ToString();
            string PageKeyWords = dtCatSEO.MetaKeywords.ToString();
            string PageDescription = dtCatSEO.MetaDescription.ToString();

            if (!string.IsNullOrEmpty(PageTitle))
                SEOHelper.RenderTitle(this.Page, PageTitle, false, true, this.GetPortalID);

            if (!string.IsNullOrEmpty(PageKeyWords))
                SEOHelper.RenderMetaTag(this.Page, "KEYWORDS", PageKeyWords, true);

            if (!string.IsNullOrEmpty(PageDescription))
                SEOHelper.RenderMetaTag(this.Page, "DESCRIPTION", PageDescription, true);
        }
    }

    private CategorySEOInfo GetSEOSettingsByCategoryName(string categorykey, int storeID, int portalID, string userName, string cultureName)
    {
        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
        ParaMeter.Add(new KeyValuePair<string, object>("@CatName", categorykey));
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        ParaMeter.Add(new KeyValuePair<string, object>("@Username", userName));
        ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        SQLHandler sqlH = new SQLHandler();
        return sqlH.ExecuteAsObject<CategorySEOInfo>("usp_Aspx_CategorySEODetailsByCatName", ParaMeter);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        IncludeCss("CategoryDetails", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/CategoryBanner/module.css");
        IncludeJs("CategoryDetails", "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js");
    }

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("template", ResolveUrl("~/js/Templating/tmpl.js"));
        Page.ClientScript.RegisterClientScriptInclude("J12", ResolveUrl("~/js/encoder.js"));
        Page.ClientScript.RegisterClientScriptInclude("Paging", ResolveUrl("~/js/Paging/jquery.pagination.js"));
        Page.ClientScript.RegisterClientScriptInclude("aspxTemplate", ResolveUrl("~/js/Templating/AspxTemplate.js"));
        Page.ClientScript.RegisterClientScriptInclude("bxslider", ResolveUrl("~/js/Sliderjs/jquery.bxSlider.js"));
        Page.ClientScript.RegisterClientScriptInclude("categoryDetail", ResolveUrl("~/Modules/Admin/DetailsBrowse/js/CategoryDetails.js"));
    }
}
