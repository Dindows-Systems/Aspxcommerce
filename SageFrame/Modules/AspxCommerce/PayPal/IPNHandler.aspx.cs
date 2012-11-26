using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Net;
using SageFrame.Framework;
using AspxCommerce.Core;
using SageFrame.Web;
using System.Data;
using SageFrame.SageFrameClass.MessageManagement;

public partial class Modules_AspxCommerce_PayPal_IPNHandler : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            string islive = Request.Form["custom"];
            string test = string.Empty;
            string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            string strLive = "https://www.paypal.com/cgi-bin/webscr";
            if (bool.Parse(islive.Split('#')[6].ToString()))
            {
                test = strSandbox;
            }
            else
            {
                test = strLive;
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(test);
            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);
            strRequest += "&cmd=_notify-validate";
            req.ContentLength = strRequest.Length;
            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://url:port#"));
            //req.Proxy = proxy;
            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();
            string appPath = Request.PhysicalApplicationPath;
            string filePath = appPath + "IPN.txt";
            StreamWriter w;
            w = File.CreateText(filePath);
            w.WriteLine("This is a test line.");
            w.WriteLine(islive.Split('#')[6] + "," + islive.Split('#')[1]);
            w.Flush();
            w.Close();
            if (strResponse == "VERIFIED")
            {
                string payerEmail = Request.Form["payer_email"];
                string paymentStatus = Request.Form["payment_status"];
                string receiverEmail = Request.Form["receiver_email"];
                string amount = Request.Form["mc_gross"];
                string invoice = Request.Form["invoice"];
                string addressName = Request.Form["address_name"];
                string addressStreet = Request.Form["address_street"];
                string addressCity = Request.Form["address_city"];
                string addressZip = Request.Form["address_zip"];
                string addressCountry = Request.Form["address_country"];
                string transID = Request.Form["txn_id"];
                string custom = Request.Form["custom"];
                //string billing = Request.Form["x_custombilling"].Trim();
                //string shipping = Request.Form["x_customshipping"].Trim();
                //string info = Request.Form["x_custominfo"].Trim();
                //string payment = Request.Form["x_custompaymentinfo"].Trim();
                // string itemReduce = Request.Form["custom_itemQuantity"].Trim();
                // string coupon = Request.Form["custom_coupon"].Trim();
                //string billingcity = Request.Form["x_custombillingcity"].Trim();
                //string billingadd = Request.Form["x_custombillingadd"].Trim();
                //string shippingcity = Request.Form["x_customshippingcity"].Trim();
                //string shippingadd = Request.Form["x_customshippingadd"].Trim();
                string[] ids = custom.Split('#');
                int orderID = int.Parse(ids[0].ToString());
                int storeID = int.Parse(ids[1].ToString());
                int portalID = int.Parse(ids[2].ToString());
                string userName = ids[3].ToString();
                int customerID = int.Parse(ids[4].ToString());
                string sessionCode = ids[5].ToString();
                string pgid = ids[7].ToString();

                TransactionLogInfo tinfo = new TransactionLogInfo();
                TransactionLog Tlog = new TransactionLog();

                tinfo.TransactionID = transID;
                tinfo.AuthCode = "";
                tinfo.TotalAmount = decimal.Parse(amount);
                tinfo.ResponseCode = "1";
                tinfo.ResponseReasonText = "";
                tinfo.OrderID = orderID;
                tinfo.StoreID = storeID;
                tinfo.PortalID = portalID;
                tinfo.AddedBy = userName;
                tinfo.CustomerID = customerID;
                tinfo.SessionCode = sessionCode;
                tinfo.PaymentGatewayID = int.Parse(pgid);
                tinfo.PaymentStatus = paymentStatus;
                tinfo.PayerEmail = payerEmail;
                tinfo.CreditCard = "";
                tinfo.RecieverEmail = receiverEmail;
                Tlog.SaveTransactionLog(tinfo);

                //string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                //String st = Server.MapPath(modulePath + "IPN/IPN.xml");//ResolveUrl(modulePath) + @"IPN\IPN.xml"; //
                //DataSet ds = new DataSet();
                //ds.ReadXml(st);
                //DataRow r = ds.Tables[0].NewRow();
                //r[0] = transID;
                //r[1] = payerEmail;
                //r[2] = amount;
                //r[3] = paymentStatus;
                //r[4] = receiverEmail;
                //r[5] = orderID;
                //r[6] = storeID;
                //r[7] = portalID;
                //r[8] = userName;
                //r[9] = customerID;
                //r[10] = sessionCode;
                //ds.Tables[0].Rows.Add(r);
                //ds.WriteXml(st);

                if (paymentStatus.Equals("Completed"))
                {
                    //string appPath = Request.PhysicalApplicationPath;
                    //string filePath = appPath + "IPN.txt";
                    //StreamWriter w;
                    //w = File.CreateText(filePath);
                    //w.WriteLine("This is a test line.");
                    //w.WriteLine(payerEmail + " " + paymentStatus + " " + amount);
                    //w.WriteLine("This 2nd.");
                    //w.Flush();
                    //w.Close();
                    PayPalHandler pdt = PayPalHandler.ParseIPN(orderID, transID, paymentStatus, storeID, portalID, userName, customerID, sessionCode);
                    // PayPalHandler.UpdateItemQuantity(itemReduce, coupon, storeID, portalID, userName);
                }
                // Session.Remove("IsTestPayPal");

            }
            else if (strResponse == "INVALID")
            {
                //log for manual investigation
            }
            else
            {
                //log response/ipn data for manual investigation
            }
            // }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
          //  throw new Exception("This Page is not accessible!");
        }
    }


}
