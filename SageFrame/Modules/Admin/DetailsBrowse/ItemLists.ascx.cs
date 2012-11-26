using System;
using System.Web;
using SageFrame.Web;
using SageFrame.Framework;
using AspxCommerce.Core;

public partial class Modules_Admin_DetailsBrowse_ItemLists : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID, CategoryID;
    public string UserName, CultureName, UserIP, CountryName;
    public string SessionCode = string.Empty;
    public string SearchText = string.Empty;
    public string NoImageItemListPath, AllowOutStockPurchase, AllowWishListItemList;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("ItemLists", "/Templates/" + TemplateName + "/css/MessageBox/style.css");
                IncludeJs("ItemLists", "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js");
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CustomerID = GetCustomerID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                if (HttpContext.Current.Session.SessionID != null)
                {
                    SessionCode = HttpContext.Current.Session.SessionID.ToString();
                }

                UserIP = HttpContext.Current.Request.UserHostAddress;
                IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
                ipToCountry.GetCountry(UserIP, out CountryName);

                CategoryID = Int32.Parse(Request.QueryString["cid"]);
                SearchText = Request.QueryString["q"];                
               
                StoreSettingConfig ssc = new StoreSettingConfig();
                NoImageItemListPath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID, CultureName);
                AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, StoreID, PortalID, CultureName);
                AllowWishListItemList = ssc.GetStoreSettingsByKey(StoreSetting.EnableWishList, StoreID, PortalID, CultureName);

            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void page_init(object sender, EventArgs e)
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

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("template", ResolveUrl("~/js/Templating/tmpl.js"));
        Page.ClientScript.RegisterClientScriptInclude("J12", ResolveUrl("~/js/encoder.js"));
        Page.ClientScript.RegisterClientScriptInclude("Paging", ResolveUrl("~/js/Paging/jquery.pagination.js"));
        Page.ClientScript.RegisterClientScriptInclude("aspxTemplate", ResolveUrl("~/js/Templating/AspxTemplate.js"));
    }
}
