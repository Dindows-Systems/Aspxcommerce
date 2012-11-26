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
using SageFrame.Web;
using SageFrame.Framework;
using AspxCommerce.Core;

public partial class Modules_AspxLatestItems_LatestItems : BaseAdministrationUserControl
{
    public string UserIp;
    public string CountryName = string.Empty;
    public string SessionCode = string.Empty;
    public int StoreID, PortalID, CustomerID;
    public string UserName, CultureName;
    public string DefaultImagePath, EnableLatestItems, AllowOutStockPurchase, AllowWishListLatestItem, AllowAddToCompareLatest;
    public int NoOfLatestItems,NoOfLatestItemsInARow ,MaxCompareItemCount;
    protected void Page_Init(object sender, EventArgs e)
    {
      
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("LatestItems", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Modules/AspxCommerce/AspxLatestItems/latestitems.css");
                IncludeJs("LatestItems", "/js/DateTime/date.js", "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js", "/js/CurrencyFormat/jquery.formatCurrency.all.js",
                    "/Modules/AspxCommerce/AspxLatestItems/js/LatestItems.js", "/Modules/AspxCommerce/AspxLatestItems/js/jquery.tipsy.js");

                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CustomerID = GetCustomerID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                if (HttpContext.Current.Session.SessionID != null)
                {
                    SessionCode = HttpContext.Current.Session.SessionID.ToString();
                }

                //StoreSettingInfo DefaultStoreSettings = (StoreSettingInfo)Session["DefaultStoreSettings"];
                //DefaultStoreSettings.AllowAnonymousCheckOut
                UserIp = HttpContext.Current.Request.UserHostAddress;
                IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
                ipToCountry.GetCountry(UserIp, out CountryName);

               StoreSettingConfig ssc = new StoreSettingConfig();
               DefaultImagePath= ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID, CultureName);
               NoOfLatestItems = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.NoOfLatestItemsDisplay, StoreID, PortalID, CultureName));
               EnableLatestItems = ssc.GetStoreSettingsByKey(StoreSetting.EnableLatestItems, StoreID, PortalID, CultureName);
               AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, StoreID, PortalID, CultureName);
               MaxCompareItemCount = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MaxNoOfItemsToCompare, StoreID, PortalID, CultureName));
               NoOfLatestItemsInARow = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.NoOfLatestItemsInARow, StoreID, PortalID, CultureName));
               AllowWishListLatestItem = ssc.GetStoreSettingsByKey(StoreSetting.EnableWishList, StoreID, PortalID, CultureName);
               AllowAddToCompareLatest = ssc.GetStoreSettingsByKey(StoreSetting.EnableCompareItems, StoreID, PortalID, CultureName);
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
