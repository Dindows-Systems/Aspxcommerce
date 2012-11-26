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
using AspxCommerce.Core;

public partial class MyCart : BaseAdministrationUserControl
{

    public int StoreID, PortalID,CustomerID,MinimumItemQuantity,MaximumItemQuantity;
    public string UserName, CultureName;
    public string SessionCode = string.Empty;
    public string NoImageMyCartPath, AllowMultipleAddShipping, ShowItemImagesOnCart, MinOrderAmount, AllowOutStockPurchase;
    public bool IsUseFriendlyUrls = true;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SageFrameConfig pagebase = new SageFrameConfig();
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            if (!IsPostBack)
            {
                IncludeCss("MyCart", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css");
                IncludeJs("MyCart", "/js/MessageBox/alertbox.js", "/js/MessageBox/jquery.easing.1.3.js", "/js/Session.js", "/Modules/AspxCommerce/AspxCart/js/MyCart.js");

                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CustomerID = GetCustomerID;
                CultureName = GetCurrentCultureName;
                if (HttpContext.Current.Session.SessionID != null)
                {
                    SessionCode = HttpContext.Current.Session.SessionID.ToString();
                }

                StoreSettingConfig ssc=new StoreSettingConfig();
                NoImageMyCartPath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID, CultureName);
                AllowMultipleAddShipping = ssc.GetStoreSettingsByKey(StoreSetting.AllowMultipleShippingAddress, StoreID, PortalID, CultureName);
                ShowItemImagesOnCart = ssc.GetStoreSettingsByKey(StoreSetting.ShowItemImagesInCart, StoreID, PortalID, CultureName);
                MinOrderAmount = ssc.GetStoreSettingsByKey(StoreSetting.MinimumOrderAmount, StoreID, PortalID, CultureName);
                MinimumItemQuantity = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MinimumItemQuantity, StoreID, PortalID, CultureName));
                MaximumItemQuantity = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MaximumItemQuantity, StoreID, PortalID, CultureName));
                AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, StoreID, PortalID, CultureName);

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
}
