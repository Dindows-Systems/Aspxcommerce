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
using AspxCommerce.Core;
using SageFrame.Web;


public partial class Modules_AspxCommerce_AspxCurrencyConverter_Currencyconversion : BaseAdministrationUserControl
{
    public string MainCurrency="";
    public string Region;
    public int StoreID, PortalID;
    public string  CultureName;

    protected void Page_Init(object sender, EventArgs e)
    {
      
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeJs("Currencyconversion", "/js/Session.js", "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js", "/js/CurrencyFormat/jquery.formatCurrency.all.js", "/js/FancyDropDown/fancyDropDown.js",
                    "/Modules/AspxCommerce/AspxCurrencyConverter/js/Currencyconversion.js");
                StoreID = GetStoreID;
                PortalID = GetPortalID;               
                CultureName = GetCurrentCultureName;
                StoreSettingConfig ssc = new StoreSettingConfig();
                MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, StoreID, PortalID, CultureName);
                Region = StoreSetting.GetRegionFromCurrencyCode(MainCurrency, StoreID, PortalID);

            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }       
    }  
}
