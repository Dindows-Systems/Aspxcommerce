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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame;
using SageFrame.Framework;
using System.Web.Services;
using System.IO;
using AspxCommerce.PayPal;
using AspxCommerce.Core;
using System.Text;

public partial class Modules_AspxCommerce_PayPalGateWay_PayThroughPaypal : PageBase
{
    public string aspxPaymentModulePath;
    public int storeID;
    public int portalID;
    public int customerID;
    public string userName;
    public string cultureName;
    public string sessionCode = string.Empty;
    public int PayPal;
    public string Spath;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["PaypalData"] != null)
            {
                string[] data = Session["PaypalData"].ToString().Split('#');
                storeID = int.Parse(data[0].ToString());
                portalID = int.Parse(data[1].ToString());
                userName = data[2];
                customerID = int.Parse(data[3].ToString());
                sessionCode = data[4].ToString();
                cultureName = data[5];
                Spath = ResolveUrl("~/Modules/AspxCommerce/AspxCommerceServices/");
                LoadSetting();             
            }
            else
            {
                lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
                clickhere.Visible = false;
            }
           

        }
        catch (Exception ex)
        {
            lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
            clickhere.Visible = false;
            ProcessException(ex);      
        }       

    } 

    [WebMethod]
    public static void SetSessionVariable(string key, string value)
    {
        HttpContext.Current.Session[key] = value;

    }
    public void LoadSetting()
    {
        PayPalWCFService pw = new PayPalWCFService();
        List<PayPalSettingInfo> sf;
        OrderDetailsCollection orderdata2 = new OrderDetailsCollection();
        orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
        string postURL=string.Empty;
       
        try
        {
            sf = pw.GetAllPayPalSetting(int.Parse(Session["GateWay"].ToString()), storeID, portalID);

            if (bool.Parse(sf[0].IsTestPaypal.ToString()))
            {
                postURL = "https://www.sandbox.paypal.com/us/cgi-bin/webscr";
                HttpContext.Current.Session["IsTestPayPal"] = true;

            }
            else
            {
                postURL = "https://www.paypal.com/us/cgi-bin/webscr";
                HttpContext.Current.Session["IsTestPayPal"] = false;

            }
            string ids = Session["OrderID"].ToString() + "#" + storeID + "#" + portalID + "#" + userName + "#" + customerID + "#" + sessionCode + "#" + Session["IsTestPayPal"].ToString() + "#" + Session["GateWay"].ToString();


            StringBuilder url = new StringBuilder();

            url.Append(postURL + "?cmd=_cart&business=" +
                HttpUtility.UrlEncode(sf[0].BusinessAccount.ToString()));

            List<CartInfoforPaypal> cd;

            cd = pw.GetCartDetails(storeID, portalID, customerID, userName, GetCurrentCultureName, sessionCode);
            int nCount = 1;


            foreach (CartInfoforPaypal oItem in cd)
            {
                url.AppendFormat("&item_name_" + nCount.ToString() + "={0}", HttpUtility.UrlEncode(oItem.ItemName.ToString()));
                url.AppendFormat("&amount_" + nCount.ToString() + "={0}", HttpUtility.UrlEncode(oItem.Price.ToString()));
                url.AppendFormat("&quantity_" + nCount.ToString() + "={0}", HttpUtility.UrlEncode(oItem.Quantity.ToString()));
                nCount++;
            }
            nCount--;
            url.AppendFormat("&num_cart_items={0}", HttpUtility.UrlEncode(nCount.ToString()));
            url.AppendFormat("&discount_amount_cart={0}", HttpUtility.UrlEncode(Session["DiscountAll"].ToString()));
            url.AppendFormat("&tax_cart={0}", HttpUtility.UrlEncode(Session["TaxAll"].ToString()));
            url.AppendFormat("&no_shipping={0}", HttpUtility.UrlEncode("1"));
            url.AppendFormat("&shipping_1={0}", HttpUtility.UrlEncode(Session["ShippingCostAll"].ToString()));

            if (sf[0].ReturnUrl.ToString() != null && sf[0].ReturnUrl.ToString() != "")
                url.AppendFormat("&return={0}", HttpUtility.UrlEncode(sf[0].ReturnUrl.ToString()));
            if (sf[0].VerificationUrl != null && sf[0].VerificationUrl != "")
                url.AppendFormat("&notify_url={0}", HttpUtility.UrlEncode(sf[0].VerificationUrl));
            if (sf[0].CancelUrl != null && sf[0].CancelUrl != "")
                url.AppendFormat("&cancel_return={0}", HttpUtility.UrlEncode(sf[0].CancelUrl));

            url.AppendFormat("&upload={0}", HttpUtility.UrlEncode("1"));
            url.AppendFormat("&rm={0}", HttpUtility.UrlEncode("1"));

            url.AppendFormat("&custom={0}", HttpUtility.UrlEncode(ids));

            Response.Redirect(url.ToString(), false);
        }
        catch (Exception ex)
        {
            lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
            clickhere.Visible = false;
            ProcessException(ex);
        }

    }


    protected void clickhere_Click(object sender, EventArgs e)
    {
        LoadSetting();
    }
}
