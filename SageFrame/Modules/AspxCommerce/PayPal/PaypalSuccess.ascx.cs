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
using System.Net;
using System.IO;
using AspxCommerce.Core;
using SageFrame.Framework;
using System.Collections;
using SageFrame.Web;
using AspxCommerce.PayPal;

public partial class Modules_AspxCommerce_AspxPaymentSuccess_PaypalSuccess : BaseAdministrationUserControl
{
    string authToken, txToken, query;
    string strResponse;
    string transID, invoice, addressPath;
    bool IsUseFriendlyUrls = true;    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                SageFrameConfig sfConfig = new SageFrameConfig();
                IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
                string sageRedirectPath = string.Empty;
                if (IsUseFriendlyUrls)
                {
                    if (GetPortalID > 1)
                    {
                        sageRedirectPath = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                        addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/portal/" + GetPortalSEOName + "/";
                    }
                    else
                    {
                        sageRedirectPath = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                        addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
                    }
                }
                else
                {
                    sageRedirectPath = ResolveUrl("{~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
                }

                Image imgProgress = (Image)UpdateProgress1.FindControl("imgPrgress");
                if (imgProgress != null)
                {
                    imgProgress.ImageUrl = GetTemplateImageUrl("ajax-loader.gif", true);
                }
                hlnkHomePage.NavigateUrl = sageRedirectPath;

                if (Session["OrderID"] != null)
                {
                    int storeID = int.Parse(GetStoreID.ToString());
                    int portalID = int.Parse(GetPortalID.ToString());
                    string userName = GetUsername.ToString();
                    int customerID = int.Parse(GetCustomerID.ToString());
                    OrderDetailsCollection orderdata = new OrderDetailsCollection();
                    List<PayPalSettingInfo> setting;
                    if (HttpContext.Current.Session["OrderCollection"] != null)
                    {

                        orderdata = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                        invoice = orderdata.ObjOrderDetails.InvoiceNumber.ToString();
                        PayPalWCFService pw = new PayPalWCFService();
                        int i = int.Parse(orderdata.ObjOrderDetails.PaymentGatewayTypeID.ToString());
                        setting = pw.GetAllPayPalSetting(i, storeID, portalID);
                        authToken = setting[0].AuthToken.ToString();
                    }

                    // authToken = "QMtOC54_YHYUkoggkMZ81ivNWSxPXduIqS5oMynafeUGRL1Rv5OTtUd4rvq";

                    //read in txn token from querystring
                    txToken = Request.QueryString.Get("tx");
                    query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);
                    // Create the request back
                    // string url = "https://www.sandbox.paypal.com/cgi-bin/webscr";
                    string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
                    string strLive = "https://www.paypal.com/cgi-bin/webscr";
                    string test = string.Empty;
                    if (Session["IsTestPayPal"] != null)
                    {

                        if (bool.Parse(Session["IsTestPayPal"].ToString()))
                        {
                            test = strSandbox;
                        }
                        else
                        {
                            test = strLive;
                        }
                    }
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(test);

                    // Set values for the request back
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.ContentLength = query.Length;

                    // Write the request back IPN strings
                    StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                    stOut.Write(query);
                    stOut.Close();

                    // Do the request to PayPal and get the response
                    StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
                    strResponse = stIn.ReadToEnd();
                    stIn.Close();

                    // If response was SUCCESS, parse response string and output details
                    if (strResponse.StartsWith("SUCCESS"))
                    {
                        string sessionCode = HttpContext.Current.Session.SessionID.ToString();
                        //for localhost
                       // PayPalHandler pdt = PayPalHandler.Parse(strResponse, storeID, portalID, userName, customerID, sessionCode);
                        //for live site
                        try
                        {
                            PayPalHandler pdtt = PayPalHandler.ParseAfterIPN(strResponse, storeID, portalID, userName, customerID, sessionCode, TemplateName, addressPath);

                        }
                        catch (Exception)
                        {
                            lblerror.Text = GetSageMessage("Payment", "PaymentParsingIPNError");                           
                        } 
                        AspxCommerceWebService clSes = new AspxCommerceWebService();

                        String sKey, sValue;
                        String[] StringArray = strResponse.Split('\n');
                        int i;
                        string status = string.Empty;
                        for (i = 1; i < StringArray.Length - 1; i++)
                        {
                            String[] StringArray1 = StringArray[i].Split('=');

                            sKey = StringArray1[0];
                            sValue = HttpUtility.UrlDecode(StringArray1[1]);

                            // set string vars to hold variable names using a switch
                            switch (sKey)
                            {
                                case "txn_id":
                                    transID = Convert.ToString(sValue);
                                    break;
                                case "payment_status":
                                    status = Convert.ToString(sValue);
                                    break;  

                            }
                        }

                        lblTransaction.Text = transID;
                        lblInvoice.Text = invoice;
                        lblPaymentMethod.Text = "Paypal";
                        lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy ");
                        if (status.ToLower().Trim() == "completed")
                        {
                            lblerror.Text = GetSageMessage("Payment", "PaymentProcessed");        
                        }
                        else if (status.ToLower().Trim() == "pending")
                        {
                            lblerror.Text = GetSageMessage("Payment", "PaymentPending");  
                        }

                        if (Session["IsFreeShipping"] != null)
                        {
                            clSes.ClearSessionVariable("IsFreeShipping");

                        }
                        if (Session["DiscountAmount"] != null)
                        {
                            clSes.ClearSessionVariable("DiscountAmount");

                        }
                        if (Session["CouponCode"] != null)
                        {
                            clSes.ClearSessionVariable("CouponCode");

                        }
                        if (Session["CouponApplied"] != null)
                        {
                            HttpContext.Current.Session.Remove("CouponApplied");
                        }
                        Session.Remove("IsTestPayPal");
                        if (Session["DiscountAll"] != null)
                        {
                            HttpContext.Current.Session.Remove("DiscountAll");
                        }
                        if (Session["TaxAll"] != null)
                        {
                            HttpContext.Current.Session.Remove("TaxAll");
                        }
                        if (Session["ShippingCostAll"] != null)
                        {
                            HttpContext.Current.Session.Remove("ShippingCostAll");
                        }
                        if (Session["GrandTotalAll"] != null)
                        {
                            HttpContext.Current.Session.Remove("GrandTotalAll");
                        }
                        if (Session["Gateway"] != null)
                        {
                            HttpContext.Current.Session.Remove("Gateway");
                        }
                    }
                    else
                    {

                        lblerror.Text = GetSageMessage("Payment", "PaymentError");
                    }
                }
                else
                {
                    Response.Redirect(sageRedirectPath, false);
                }
            }
            catch (Exception ex)
            {

                ProcessException(ex);
            }
        }
    }
}
