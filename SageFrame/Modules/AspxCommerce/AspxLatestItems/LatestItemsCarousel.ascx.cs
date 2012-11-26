using System;
using System.Web;
using SageFrame.Web;
using SageFrame.Framework;
using AspxCommerce.Core;

public partial class Modules_AspxCommerce_AspxLatestItems_LatestItemsCarousel : BaseAdministrationUserControl
{
    public string UserIp;
    public string CountryName = string.Empty;
    public string SessionCode = string.Empty;
    public int StoreID, PortalID, CustomerID;
    public string UserName, CultureName;
    public string DefaultImagePath, EnableLatestItems, AllowOutStockPurchase;
    public int NoOfLatestItems, NoOfLatestItemsInARow;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("LatestItems", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Modules/AspxCommerce/AspxLatestItems/latestitems.css", "/Templates/" + TemplateName + "/css/Slider/style.css");
                IncludeJs("LatestItems", "/js/DateTime/date.js", "/js/MessageBox/jquery.easing.1.3.js",
                          "/js/MessageBox/alertbox.js", "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js",
                          "/js/CurrencyFormat/jquery.formatCurrency.all.js",
                          "/Modules/AspxCommerce/AspxLatestItems/js/jquery.ItembxSlider.js",
                          "/Modules/AspxCommerce/AspxLatestItems/js/LatestItemsCarousel.js",
                          "/Modules/AspxCommerce/AspxLatestItems/js/jquery.tipsy.js", "/js/Templating/tmpl.js");

                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CustomerID = GetCustomerID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                if (HttpContext.Current.Session.SessionID != null)
                {
                    SessionCode = HttpContext.Current.Session.SessionID.ToString();
                }
                UserIp = HttpContext.Current.Request.UserHostAddress;
                IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
                ipToCountry.GetCountry(UserIp, out CountryName);

                StoreSettingConfig ssc = new StoreSettingConfig();
                DefaultImagePath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID,CultureName);
                NoOfLatestItems =
                    int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.NoOfLatestItemsDisplay, StoreID, PortalID,CultureName));
                EnableLatestItems = ssc.GetStoreSettingsByKey(StoreSetting.EnableLatestItems, StoreID, PortalID,CultureName);
                AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, StoreID, PortalID,CultureName);
                NoOfLatestItemsInARow = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.NoOfLatestItemsInARow, StoreID, PortalID,CultureName));
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
