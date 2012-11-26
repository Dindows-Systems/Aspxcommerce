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
using System.IO;
using SageFrame.Framework;
using AspxCommerce.Core;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using SageFrame.Web;
using SageFrame.SageFrameClass.MessageManagement;
using System.Collections;
using SageFrame.Message;

public partial class Modules_AspxCommerce_PaymentGateways_CashOnDeliverySuccess : BaseAdministrationUserControl
{

    //  string authToken, txToken, query;
    // string strResponse;
    public string sendEmailFrom, sendOrderNotice;
    bool IsUseFriendlyUrls = true;
    string sageRedirectPath, addressPath = string.Empty;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                SageFrameConfig sfConfig = new SageFrameConfig();
                IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);

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

                StoreSettingConfig ssc = new StoreSettingConfig();
                sendEmailFrom = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, GetStoreID, GetPortalID, GetCurrentCultureName);
                sendOrderNotice = ssc.GetStoreSettingsByKey(StoreSetting.SendOrderNotification, GetStoreID, GetPortalID, GetCurrentCultureName);

                SendConfrimMessage();
            }
        }
        catch
        {
        }
    }

    protected void SendConfrimMessage()
    {
        try
        {
            if (Session["OrderID"] != null)
            {
                string transID = string.Empty; // transaction ID from Relay Response
                int responseCode = 1; // response code, defaulted to Invalid
                string responsereasontext = string.Empty;
                responsereasontext = "Transaction occured Successfully";
                int responsereasonCode = 1;
                string purchaseorderNo = string.Empty;
                string invoice = string.Empty;
                string paymentmethod = string.Empty;
                OrderDetailsCollection orderdata2 = new OrderDetailsCollection();
                if (HttpContext.Current.Session["OrderCollection"] != null)
                {
                  
                    orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                   
                }
                invoice = orderdata2.ObjOrderDetails.InvoiceNumber.ToString();
                Random random = new Random();
                purchaseorderNo = (random.Next(0, 1000)).ToString();
                string timeStamp = ((int)(DateTime.UtcNow - new DateTime(2011, 1, 1)).TotalSeconds).ToString();
                transID = (random.Next(99999, 111111)).ToString();
                lblTransaction.Text = transID;
                lblInvoice.Text = invoice;
                lblPaymentMethod.Text = "Cash On Delivery";
                lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy ");
                int storeID = int.Parse(GetStoreID.ToString());
                int portalID = int.Parse(GetPortalID.ToString());
                string userName = GetUsername.ToString();
                int customerID = int.Parse(GetCustomerID.ToString());
                string sessionCode = HttpContext.Current.Session.SessionID.ToString();
                string result = CashOnDelivery.Parse(transID, invoice, purchaseorderNo, responseCode, responsereasonCode, responsereasontext, storeID, portalID, userName, customerID, sessionCode);
                lblerror.Text = result.ToString();
                lblerror.Text = GetSageMessage("Payment", "PaymentProcessed"); 
                TransactionLogInfo tinfo = new TransactionLogInfo();
                TransactionLog Tlog = new TransactionLog();

                tinfo.TransactionID = transID;
                tinfo.AuthCode = "";
                tinfo.TotalAmount = decimal.Parse(orderdata2.ObjOrderDetails.GrandTotal.ToString());
                tinfo.ResponseCode = responseCode.ToString();
                tinfo.ResponseReasonText = responsereasontext;
                tinfo.OrderID = orderdata2.ObjOrderDetails.OrderID;
                tinfo.StoreID =  orderdata2.ObjCommonInfo.StoreID;
                tinfo.PortalID = orderdata2.ObjCommonInfo.PortalID;
                tinfo.AddedBy = orderdata2.ObjCommonInfo.AddedBy;
                tinfo.CustomerID = orderdata2.ObjOrderDetails.CustomerID;
                tinfo.SessionCode = orderdata2.ObjOrderDetails.SessionCode;
                tinfo.PaymentGatewayID = orderdata2.ObjOrderDetails.PaymentGatewayTypeID;
                tinfo.PaymentStatus = "Processed";
                tinfo.CreditCard = "";
                Tlog.SaveTransactionLog(tinfo);

                AspxCommerceWebService clSes = new AspxCommerceWebService();
                if (Session["IsFreeShipping"] != null)
                {
                    HttpContext.Current.Session.Remove("IsFreeShipping");
                }
                if (Session["DiscountAmount"] != null)
                {
                    HttpContext.Current.Session.Remove("DiscountAmount");

                }
                if (Session["CouponCode"] != null)
                {
                    HttpContext.Current.Session.Remove("CouponCode");
                }
                if (Session["CouponApplied"] != null)
                {
                    HttpContext.Current.Session.Remove("CouponApplied");
                }
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

                //invoice  transID

                if (Session["OrderCollection"] != null)
                {
                    OrderDetailsCollection orderdata = new OrderDetailsCollection();
                    orderdata = (OrderDetailsCollection)Session["OrderCollection"];
                    try
                    {
                        EmailTemplate.SendEmailForOrder(GetPortalID, orderdata, addressPath, TemplateName, transID);
                    }
                    catch
                    {
                        lblerror.Text = "";
                        lblerror.Text= GetSageMessage("Payment", "EmailSendOrderProblem");
                    }
                    clSes.ClearSessionVariable("OrderCollection");
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
