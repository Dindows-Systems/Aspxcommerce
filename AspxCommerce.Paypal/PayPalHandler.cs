using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using System.Text;
using SageFrame.SageFrameClass.MessageManagement;


/// <summary>
/// Summary description for PayPalHandler
/// </summary>
/// 
namespace AspxCommerce.Core
{
    public class PayPalHandler
    {

        public static PayPalHandler Parse(string postData, int storeID, int portalID, string userName, int customerID, string sessionCode)
        {
            String sKey, sValue;
            PayPalHandler ph = new PayPalHandler();
            string transID = string.Empty;

            try
            {
                //split response into string array using whitespace delimeter
                String[] StringArray = postData.Split('\n');

                // NOTE:
                /*
                * loop is set to start at 1 rather than 0 because first
                string in array will be single word SUCCESS or FAIL
                Only used to verify post data
                */
                OrderDetailsCollection ot = new OrderDetailsCollection();
                OrderDetailsInfo odinfo = new OrderDetailsInfo();
                CartManageSQLProvider cms = new CartManageSQLProvider();
                CommonInfo cf = new CommonInfo();
                cf.StoreID = storeID;
                cf.PortalID = portalID;
                cf.AddedBy = userName;
                // UpdateOrderDetails
                AspxOrderDetails objad = new AspxOrderDetails();
                SQLHandler sqlH = new SQLHandler();
                // use split to split array we already have using "=" as delimiter
                WcfSession ws = new WcfSession();
                odinfo.OrderID = ws.GetSessionVariable("OrderID");
                int i;
                for (i = 1; i < StringArray.Length - 1; i++)
                {
                    String[] StringArray1 = StringArray[i].Split('=');

                    sKey = StringArray1[0];
                    sValue = HttpUtility.UrlDecode(StringArray1[1]);

                    // set string vars to hold variable names using a switch
                    switch (sKey)
                    {
                        case "payment_status":
                            odinfo.ResponseReasonText = Convert.ToString(sValue);
                            break;

                        case "mc_fee":
                            // ph.PaymentFee = Convert.ToDouble(sValue);
                            break;

                        case "payer_email":
                            // ph.PayerEmail = Convert.ToString(sValue);
                            break;

                        case "Tx Token":
                            // ph.TxToken = Convert.ToString(sValue);
                            break;

                        case "txn_id":
                            odinfo.TransactionID = Convert.ToString(sValue);
                            transID = Convert.ToString(sValue);
                            break;

                    }
                }
                // odinfo.InvoiceNumber = DateTime.Now.ToString("yyyyMMddhhmmss");
                ot.ObjOrderDetails = odinfo;
                ot.ObjCommonInfo = cf;
                odinfo.OrderStatusID = 8;
                objad.UpdateOrderDetails(ot);
                if (HttpContext.Current.Session["OrderCollection"] != null)
                {
                    OrderDetailsCollection orderdata2 = new OrderDetailsCollection();
                    orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                    objad.UpdateItemQuantity(orderdata2);
                }
                ws.ClearSessionVariable("OrderID");
                cms.ClearCartAfterPayment(customerID, sessionCode, storeID, portalID);

                //invoice  transID
                if (HttpContext.Current.Session["OrderCollection"] != null)
                {
                    //StringBuilder emailbody = new StringBuilder();
                    //emailbody.Append("<div style=\"width:600px;min-height:auto;background-color:#f2f2f2;color:#707070;font:13px/24px Verdana,Geneva,sans-serif;\"><div style=\"color:#49B8F4;  font-size:20px;margin:auto 0; font-family:Verdana,Geneva,sans-serif;\"> <label>Thank you for Order </label></div>");
                    //OrderDetailsCollection orderdata = new OrderDetailsCollection();
                    //orderdata = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                    //emailbody.Append("<p> <strong>Dear </strong> " + orderdata.ObjBillingAddressInfo.FirstName.ToString() + "</p>");
                    ////if customer is registered
                    // if (orderdata.ObjCommonInfo.AddedBy.ToString().ToLower() == "anonymoususer" && orderdata.ObjOrderDetails.CustomerID ==0)
                    //{
                    //    emailbody.Append("<div>  You have ordered items from <strong>AspxCommerce</strong>. <br /> Thanks for using <strong>AspxCommerce</strong>. You can now ship any items. To see all the transaction details, log in to your <b>AspxCommerce</b> <a href=" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/Register.aspx?session="+sessionCode +"" + ">account</a>.<br /> It may take a few moments for this transaction to appear in your account.<br /></div> ");
                    //}
                    //else
                    //{//if customer is guest..send session code 
                    //    emailbody.Append("<div> You have ordered items from <strong>AspxCommerce</strong>. <br /> Thanks for using <strong>AspxCommerce</strong>. You can now ship any items. To see all the transaction details, Please Resister and log in to your <b>AspxCommerce</b> <a href=" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/Login.aspx" + ">account</a>.<br /> It may take a few moments for this transaction to appear in your account.<br /></div>");
                    //}
                    //emailbody.Append("<div> <strong>Your Order Details </strong><br />  Transaction ID: <strong >" + odinfo.TransactionID.ToString() + "</strong><br />Invoice No: <strong>" + odinfo.InvoiceNumber.ToString() + "</strong><br />Paymenet System Used: <strong>" + orderdata.ObjPaymentInfo.PaymentMethodName + "</strong><br /> </div> ");
                    //emailbody.Append("<div> If you having Issuses with transaction or any further inquiry then you can mail info@aspxcommerce.com.</div><div> Please do not reply to this email. This mail is automatic generated after you have ordered .</div><div>Thank You, <br /><a href=\"" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "\"><img src=\"" + "http://www.aspxcommerce.com" + "/Templates/AspxCommerce/images/aspxcommerce.png\" alt=\"AspxCommerce Team\"/></a></div></div>");
                    //string body = emailbody.ToString();
                    //SageFrameConfig pagebase = new SageFrameConfig();
                    //string emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);//"milsonmun@hotmail.com";
                    //string emailSiteAdmin =  pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                    //MailHelper.SendMailNoAttachment("yourorder@aspxcommerce.com", orderdata.ObjBillingAddressInfo.EmailAddress.ToString(), "Your Order Details", body, emailSuperAdmin, emailSiteAdmin);
                    //ws.ClearSessionVariable("OrderCollection");
                    string sendEmailFrom = StoreSetting.GetStoreSettingValueByKey(StoreSetting.SendEcommerceEmailsFrom, storeID, portalID, "en-US");
                    string sendOrderNotice = StoreSetting.GetStoreSettingValueByKey(StoreSetting.SendOrderNotification, storeID, portalID, "en-US");
                    if (sendOrderNotice.ToLower() == "true")
                    {
                        StringBuilder emailbody = new StringBuilder();
                        OrderDetailsCollection orderdata = new OrderDetailsCollection();
                        orderdata = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                        emailbody.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"5\" bgcolor=\"#e0e0e0\" style=\"font: 12px Arial,Helvetica,sans-serif;\"> <tr><td align=\"center\" valign=\"top\"><table width=\"680\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr> <td><img src=\"" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/blank.gif\" width=\"1\" height=\"10\" alt=\" \" /></td></tr><tr><td><table width=\"680\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr> <td width=\"300\">");
                        emailbody.Append("<a href=\"http://www.aspxcommerce.com\" target=\"_blank\" style=\"outline: none; border: none;\"> <img src=\"http://www.aspxcommerce.com/Templates/AspxCommerce/images/aspxcommerce.png\" width=\"143\" height=\"62\" alt=\"AspxCommerce\" title=\"AspxCommerce\" /></a></td><td width=\"191\" align=\"left\" valign=\"middle\">");
                        emailbody.Append("&nbsp;</td><td width=\"189\" align=\"right\" valign=\"middle\"><b style=\"padding: 0 20px 0 0; text-shadow: 1px 1px 0 #fff;\">" + DateTime.Now.ToString("dd MMMM yyyy ") + "</b>");
                        emailbody.Append("</td> </tr></table> </td> </tr> <tr>  <td bgcolor=\"#fff\"><div style=\"border:1px solid #c7c7c7; background:#fff; padding:20px\">  <img src=\"" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/blank.gif\" width=\"1\" height=\"10\" alt=\" \" /><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#FFFFFF\">  <tr> <td> <p style=\"font-family: Arial, Helvetica, sans-serif; font-size: 17px; line-height: 16px;color: #278ee6; margin: 0; padding: 0 0 5px 0; font-weight: bold;\"> ");
                        emailbody.Append(" Dear " + orderdata.ObjBillingAddressInfo.FirstName.ToString() + "</p> <p style=\"font-family: Arial, Helvetica, sans-serif; font-size: 13px; line-height: 16px; color: #393939; margin: 0; padding: 0 0 10px 0; font-weight: bold;\">  Thank you for Order</p>");
                        emailbody.Append("<p style=\"font-family: Arial, Helvetica, sans-serif; font-size: 12px; line-height: 18px; color: #393939; margin: 0; padding: 8px 0 15px 0; font-weight: normal;\">You have ordered items from <span style=\"font-weight: bold; font-size: 11px;\">AspxCommerce</span>.<br />");
                        emailbody.Append("Thanks for using <span style=\"font-weight: bold; font-size: 11px;\">AspxCommerce</span>. You can now ship any items. To see all the transaction details,");
                        // emailbody.Append("  Please Register and log in to your <span style=\"font-weight: bold; font-size: 11px;\">AspxCommerce</span>");
                        string account = "";
                        if (orderdata.ObjCommonInfo.AddedBy.ToString().ToLower() == "anonymoususer" && orderdata.ObjOrderDetails.CustomerID == 0)
                        {   // future login process for annoymoususr 
                            emailbody.Append("Please Register and log in to your <span style=\"font-weight: bold; font-size: 11px;\">AspxCommerce</span>");

                            account = "<a href=" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/User-Registration.aspx" + ">account</a>";
                        }
                        else
                        {
                            emailbody.Append("  Please log in to your <span style=\"font-weight: bold; font-size: 11px;\">AspxCommerce</span>");

                            account = "<a href=" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/Login.aspx" + ">account</a>";
                        }


                        emailbody.Append(" " + account + ".<br />  It may take a few moments for this transaction to appear in your account</p>  </td></tr>  </table><div style=\"border: 1px solid #cfcfcf; background: #f1f1f1; padding: 10px\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tr> <td><h3 style=\"font-size: 15px; font-family: Arial, Helvetica, sans-serif; line-height: 20px; font-weight: bold; margin: 0; text-transform: capitalize; color: #000; text-shadow: 1px 1px 0 #fff;\">  Your Order Details</h3></td></tr><tr><td><img src=\"" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/blank.gif\" width=\"1\" height=\"10\" alt=\" \" /></td>");
                        emailbody.Append(" </tr><tr><td bgcolor=\"#fff\"><div style=\"border: 1px solid #c7c7c7; background: #fff; padding: 10px\">");
                        emailbody.Append("<div style=\"border: 1px solid #cfcfcf; background: #f1f1f1; padding: 10px\">");
                        emailbody.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        emailbody.Append("<tr>  <td height=\"25\" style=\"border-bottom: 1px solid #fff;\">");
                        emailbody.Append("<h3 style=\"font-size: 15px; font-family: Arial, Helvetica, sans-serif; line-height: 20px;");
                        emailbody.Append("font-weight: bold; margin: 0; text-transform: capitalize; color: #000; text-shadow: 1px 1px 0 #fff;\">Order Information</h3>");
                        emailbody.Append("</td></tr><tr> <td> <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        emailbody.Append(" <tr><td width=\"120\" height=\"20\"><span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold;\">");
                        emailbody.Append(" Merchant:</span> </td> <td>");
                        //  merchant Name
                        emailbody.Append("Aspx Commerce");
                        emailbody.Append(" </td> <td width=\"150\" style=\"font-family: Arial, Helvetica, sans-serif; font-size: 11px;");
                        emailbody.Append(" font-weight: bold; border-left: 1px solid #fff; padding-left: 20px;\"> Customer Id:  <td>");
                        // customer ID
                        emailbody.Append("" + customerID.ToString() + "");
                        emailbody.Append(" </td> </tr>  <tr>  <td height=\"20\"> <span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold;\">");
                        emailbody.Append(" Description:</span>  </td> <td> Order Remarks</td> <td style=\"font-family: Arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold;");
                        emailbody.Append(" border-left: 1px solid #fff; padding-left: 20px;\"> Phone No: </td> <td>");
                        // customer Phone No
                        emailbody.Append("" + orderdata.ObjBillingAddressInfo.Phone.ToString() + "");
                        emailbody.Append("  </td> </tr> <td height=\"20\"><span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold;\">");
                        emailbody.Append(" Invoice Number:</span> </td><td>");
                        //   invoice no 
                        emailbody.Append("" + orderdata.ObjOrderDetails.InvoiceNumber.ToString() + "");
                        // emailbody.Append("" + invoice + "");                        
                        emailbody.Append(" </td> <td style=\"font-family: Arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold;  border-left: 1px solid #fff; padding-left: 20px;\">");
                        emailbody.Append("  &nbsp;</td><td> &nbsp;</td></tr></table>  </td> </tr> </table> </div>");
                        emailbody.Append(" <div style=\"border: 1px solid #cfcfcf; background: #f1f1f1; padding: 0; margin: 10px 0 0 0;\">");
                        emailbody.Append(" <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        emailbody.Append(" <tr> <td width=\"300\" style=\"background: #e0e0e0;\"> <h2 style=\"margin: 0; padding: 0; font-weight: bold; font-family: Arial, Helvetica, sans-serif;");
                        emailbody.Append("  font-size: 15px; padding: 3px 0 3px 10px; text-shadow: 1px 1px 0 #fff;\">");
                        emailbody.Append("  Billing Information</h2>  </td><td style=\"background: #e0e0e0; border-left: 1px solid #fff;\">");
                        emailbody.Append("   <h2 style=\"margin: 0; padding: 0; font-weight: bold; font-family: Arial, Helvetica, sans-serif;");
                        emailbody.Append("  font-size: 15px; padding: 3px 0 3px 10px; text-shadow: 1px 1px 0 #fff;\">");
                        emailbody.Append(" Shipping Information</h2> </td></tr><tr><td valign=\"top\"> <p style=\"margin: 0; padding: 10px; font-family: Arial, Helvetica, sans-serif; font-weight: normal;");
                        emailbody.Append("  line-height: 18px; font-size: 12px;\">");
                        //Milson Munakami<br />
                        //                                                               Bd<br />
                        //                                                               sda<br />
                        //                                                               Dsad, Alberta 3232<br />
                        //                                                               Afghanistan<br />
                        //                                                               milson@braindigit.com<br />
                        //                                                               +000 000 0000<br />
                        //                                                               Fax: 2222222</p>
                        //                                                       </td>

                        string billing = orderdata.ObjBillingAddressInfo.FirstName.ToString() + " " +
                          orderdata.ObjBillingAddressInfo.LastName.ToString() + "<br />";

                        if (orderdata.ObjBillingAddressInfo.CompanyName != null)
                        {
                            billing += orderdata.ObjBillingAddressInfo.CompanyName.ToString() + "<br />";
                        }

                        billing += orderdata.ObjBillingAddressInfo.City.ToString() + ", " + orderdata.ObjBillingAddressInfo.Address.ToString() + "<br />" +
                        orderdata.ObjBillingAddressInfo.Country.ToString() + "<br />" +
                        orderdata.ObjBillingAddressInfo.EmailAddress.ToString() + "<br />" +
                        orderdata.ObjBillingAddressInfo.Phone.ToString() + "<br />" + "<p>";

                        emailbody.Append("" + billing + "");
                        emailbody.Append("</td><td style=\"border-left: 1px solid #fff;\" valign=\"top\"> <p style=\"margin: 0; padding: 10px; font-family: Arial, Helvetica, sans-serif; font-weight: normal;");
                        emailbody.Append("line-height: 18px; font-size: 12px;\">");
                        //Milson Munakami<br />
                        //                                                              Bd<br />
                        //                                                              sda<br />
                        //                                                              Dsad, Alberta 3232<br />
                        //                                                              Afghanistan<br />
                        //                                                              milson@braindigit.com<br />
                        //                                                              +000 000 0000<br />
                        //                                                              Fax: 2222222</p>

                        if (!orderdata.ObjOrderDetails.IsDownloadable)
                        {
                            if (orderdata.ObjOrderDetails.IsMultipleCheckOut == false)
                            {
                                string shipping = orderdata.ObjShippingAddressInfo.FirstName.ToString() + " " +
                                    orderdata.ObjShippingAddressInfo.LastName.ToString() + "<br />";
                                if (orderdata.ObjShippingAddressInfo.CompanyName != null)
                                {
                                    shipping += orderdata.ObjShippingAddressInfo.CompanyName.ToString() + "<br />";
                                }

                                shipping += orderdata.ObjShippingAddressInfo.City.ToString() + ", " + orderdata.ObjShippingAddressInfo.Address.ToString() + "<br />" +
                                      orderdata.ObjShippingAddressInfo.Country.ToString() + "<br />" +
                                      orderdata.ObjShippingAddressInfo.EmailAddress.ToString() + "<br />" +
                                      orderdata.ObjShippingAddressInfo.Phone.ToString() + "<br />" + "<p>";
                                emailbody.Append("" + shipping + "");
                            }
                            else
                            {
                                emailbody.Append("Multiple addresses<br />Plese log in to view.");
                            }

                        }
                        emailbody.Append("</td> </tr> </table></div><div style=\"border: 1px solid #cfcfcf; background: #f1f1f1; padding: 0; margin: 10px 0 0 0;\">");
                        emailbody.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        emailbody.Append("<tr><td style=\"background: #e0e0e0;\" colspan=\"2\"><h2 style=\"margin: 0; padding: 0; font-weight: bold; font-family: Arial, Helvetica, sans-serif;");
                        emailbody.Append("font-size: 15px; padding: 3px 0 3px 10px; text-shadow: 1px 1px 0 #fff;\">");
                        // card type Visa
                        emailbody.Append("" + orderdata.ObjPaymentInfo.PaymentMethodName.ToString() + "");
                        // emailbody.Append("" + orderdata.ObjPaymentInfo.CardType.ToString() + "");
                        emailbody.Append("</h2> </td></tr><tr><td style=\"padding: 5px 10px;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        emailbody.Append("<tr><td width=\"100\" height=\"20\" style=\"width: 200\">  <p style=\"font-family: Arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold;");
                        emailbody.Append(" margin: 0; padding: 0;\"> Date/Time:</p></td><td>");
                        //    order date
                        emailbody.Append("" + DateTime.Now.ToString("dddd, dd MMMM yyyy") + "");
                        emailbody.Append("</td></tr><tr><td height=\"20\"><p style=\"font-family: Arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold;");
                        emailbody.Append(" margin: 0; padding: 0;\">Transaction Id:</p> </td>  <td>");
                        //     transaction ID
                        emailbody.Append("" + transID + "");
                        //emailbody.Append("" + transID + "");
                        emailbody.Append("</td> </tr></table> </td> </tr> </table></div></div>  </td> </tr>  </table>  </div>");
                        emailbody.Append(" <p style=\"font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: normal; color: #000;\">");
                        emailbody.Append(" if you having Issues with transaction or any further inquiry then you can mail <a");
                        emailbody.Append("href=\"mailto:info@aspxcommerce.com\" style=\"color: #278ee6;\">info@aspxcommerce.com</a></p>");
                        emailbody.Append("<p style=\"margin: 0; padding: 5px 0 0 0; font: bold 11px Arial, Helvetica, sans-serif; color: #666;\">");
                        emailbody.Append("Please do not reply to this email. This mail is automatic generated after you have ordered.</a><br />");
                        emailbody.Append("<br /> Thank You,<br />  <span style=\"font-weight: normal; font-size: 12px; font-family: Arial, Helvetica, sans-serif;\">");
                        emailbody.Append("AXPXCommerce Team </span> </p> </div> </td>  </tr>");
                        emailbody.Append("  <tr> <td>   <img src=\"" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/blank.gif\" width=\"1\" height=\"20\" alt=\" \" /> </td>");
                        emailbody.Append("</tr> <tr> <td align=\"center\" valign=\"top\"> <p style=\"font-size: 11px; color: #4d4d4d\">");
                        emailbody.Append("  © " + DateTime.Now.Year.ToString() + " AspxCommerce. All Rights Reserved.</p></td>  </tr> <tr>");
                        emailbody.Append("  <td align=\"center\" valign=\"top\"> <img src=\"" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/blank.gif\" width=\"1\" height=\"10\" alt=\" \" />");
                        emailbody.Append("   </td>  </tr> </table></div>   </td> </tr> </table>");
                        string body = emailbody.ToString();
                        SageFrameConfig pagebase = new SageFrameConfig();
                        string emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);//"milsonmun@hotmail.com";
                        string emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                        MailHelper.SendMailNoAttachment(sendEmailFrom, orderdata.ObjBillingAddressInfo.EmailAddress.ToString(), "Your Order Details", body, emailSuperAdmin, emailSiteAdmin);
                    }
                    ws.ClearSessionVariable("OrderCollection");
                }

                return ph;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static PayPalHandler ParseIPN(int orderID, string transID, string status, int storeID, int portalID, string userName, int customerID, string sessionCode)
        {
            PayPalHandler ph = new PayPalHandler();
            try
            {

                OrderDetailsCollection ot = new OrderDetailsCollection();
                OrderDetailsInfo odinfo = new OrderDetailsInfo();
                CartManageSQLProvider cms = new CartManageSQLProvider();
                CommonInfo cf = new CommonInfo();
                cf.StoreID = storeID;
                cf.PortalID = portalID;
                cf.AddedBy = userName;
                // UpdateOrderDetails
                AspxOrderDetails objad = new AspxOrderDetails();
                SQLHandler sqlH = new SQLHandler();
                // use split to split array we already have using "=" as delimiter
                // WcfSession ws = new WcfSession();
                odinfo.OrderID = orderID;//ws.GetSessionVariable("OrderID");
                odinfo.ResponseReasonText = status;
                odinfo.TransactionID = transID;
                ot.ObjOrderDetails = odinfo;
                ot.ObjCommonInfo = cf;
                odinfo.OrderStatusID = 8;
                objad.UpdateOrderDetails(ot);

                return ph;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static PayPalHandler ParseAfterIPN(string postData, int storeID, int portalID, string userName, int customerID, string sessionCode, string TemplateName, string addressPath)
        {
            String sKey, sValue;
            PayPalHandler ph = new PayPalHandler();
            string transID = string.Empty;

            try
            {
                //split response into string array using whitespace delimeter
                String[] StringArray = postData.Split('\n');

                // NOTE:
                /*
                * loop is set to start at 1 rather than 0 because first
                string in array will be single word SUCCESS or FAIL
                Only used to verify post data
                */
                OrderDetailsCollection ot = new OrderDetailsCollection();
                OrderDetailsInfo odinfo = new OrderDetailsInfo();
                CartManageSQLProvider cms = new CartManageSQLProvider();
                CommonInfo cf = new CommonInfo();
                cf.StoreID = storeID;
                cf.PortalID = portalID;
                cf.AddedBy = userName;
                // UpdateOrderDetails
                AspxOrderDetails objad = new AspxOrderDetails();
                SQLHandler sqlH = new SQLHandler();
                // use split to split array we already have using "=" as delimiter
                WcfSession ws = new WcfSession();
                odinfo.OrderID = ws.GetSessionVariable("OrderID");
                int i;
                for (i = 1; i < StringArray.Length - 1; i++)
                {
                    String[] StringArray1 = StringArray[i].Split('=');

                    sKey = StringArray1[0];
                    sValue = HttpUtility.UrlDecode(StringArray1[1]);

                    // set string vars to hold variable names using a switch
                    switch (sKey)
                    {
                        case "payment_status":
                            odinfo.ResponseReasonText = Convert.ToString(sValue);
                            break;

                        case "mc_fee":
                            // ph.PaymentFee = Convert.ToDouble(sValue);
                            break;

                        case "payer_email":
                            // ph.PayerEmail = Convert.ToString(sValue);
                            break;

                        case "Tx Token":
                            // ph.TxToken = Convert.ToString(sValue);
                            break;

                        case "txn_id":
                            odinfo.TransactionID = Convert.ToString(sValue);
                            transID = Convert.ToString(sValue);
                            break;

                    }
                }

                ot.ObjOrderDetails = odinfo;
                ot.ObjCommonInfo = cf;
                //odinfo.OrderStatusID = 8;
                //objad.UpdateOrderDetails(ot);
                if (odinfo.ResponseReasonText.ToLower().Trim() == "completed")
                {
                    if (HttpContext.Current.Session["OrderCollection"] != null)
                    {
                        OrderDetailsCollection orderdata2 = new OrderDetailsCollection();
                        orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                        objad.UpdateItemQuantity(orderdata2);
                    }
                }
                ws.ClearSessionVariable("OrderID");
                cms.ClearCartAfterPayment(customerID, sessionCode, storeID, portalID);

                //invoice  transID
                if (HttpContext.Current.Session["OrderCollection"] != null)
                {
                    OrderDetailsCollection orderdata = new OrderDetailsCollection();
                    orderdata = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                    EmailTemplate.SendEmailForOrder(portalID, orderdata, addressPath, TemplateName, transID);
                }
                ws.ClearSessionVariable("OrderCollection");

                return ph;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateItemQuantity(string ItemIDS, string coupon, int storeID, int portalID, string userName)
        {
            try
            {
                string[] coupondetails = coupon.Split('#');
                if (coupondetails[0] != null && coupondetails[1] != null)
                {
                    List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                    ParaMeter.Add(new KeyValuePair<string, object>("@CouponCode", coupondetails[0]));
                    ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                    ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                    ParaMeter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
                    ParaMeter.Add(new KeyValuePair<string, object>("@CouponUsedCount", coupondetails[1]));
                    SQLHandler sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery("usp_Aspx_UpdateCouponUserRecord", ParaMeter);
                }
                string[] ids = ItemIDS.Split('#');
                //id,quantity,isdownloadable
                for (int i = 0; i < ids.Length; i++)
                {
                    string[] itemdetails = ids[i].Split(',');
                    if (bool.Parse(itemdetails[0].ToString()) != true)
                    {
                        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                        ParaMeter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
                        ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemdetails[0]));
                        ParaMeter.Add(new KeyValuePair<string, object>("@Quantity", itemdetails[1]));
                        SQLHandler sqlH = new SQLHandler();
                        sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_UpdateItemQuantitybyOrder]", ParaMeter);
                    }

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static PayPalHandler SaveErrorLog(string postData)
        {
            String sKey, sValue;
            PayPalHandler ph = new PayPalHandler();
            string transID = string.Empty;

            try
            {
                //split response into string array using whitespace delimeter
                String[] StringArray = postData.Split('\n');

                // NOTE:
                /*
                * loop is set to start at 1 rather than 0 because first
                string in array will be single word SUCCESS or FAIL
                Only used to verify post data
                */

                int i;
                for (i = 1; i < StringArray.Length - 1; i++)
                {
                    String[] StringArray1 = StringArray[i].Split('=');

                    sKey = StringArray1[0];
                    sValue = HttpUtility.UrlDecode(StringArray1[1]);

                    // set string vars to hold variable names using a switch
                    switch (sKey)
                    {
                        case "payment_status":
                            //  odinfo.ResponseReasonText = Convert.ToString(sValue);
                            break;

                        case "mc_fee":
                            // ph.PaymentFee = Convert.ToDouble(sValue);
                            break;

                        case "payer_email":
                            // ph.PayerEmail = Convert.ToString(sValue);
                            break;

                        case "Tx Token":
                            // ph.TxToken = Convert.ToString(sValue);
                            break;

                        case "txn_id":
                            //  odinfo.TransactionID = Convert.ToString(sValue);
                            transID = Convert.ToString(sValue);
                            break;

                    }
                }

                return ph;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

