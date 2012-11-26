using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SageFrame.Message;
using SageFrame.Web;
using SageFrame.SageFrameClass.MessageManagement;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class EmailTemplate
   {
       public EmailTemplate()
       {
       }

        public static string[] GetAllToken(string template)
        {
            List<string> returnValue = new List<string> { };
            int preIndex = template.IndexOf('%', 0);
            int postIndex = template.IndexOf('%', preIndex + 1);
            while (preIndex > -1)
            {
                returnValue.Add(template.Substring(preIndex, (postIndex - preIndex) + 1));
                template = template.Substring(postIndex + 1, (template.Length - postIndex) - 1);
                preIndex = template.IndexOf('%', 0);
                postIndex = template.IndexOf('%', preIndex + 1);
            }
            return returnValue.ToArray();
        }

        public static void SendEmailForOrder(int portalID, OrderDetailsCollection orderdata, string addressPath, string templateName,string transID)
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
           // sendEmailFrom = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, storeID, portalID, "en-US");
            string sendOrderNotice = ssc.GetStoreSettingsByKey(StoreSetting.SendOrderNotification, orderdata.ObjCommonInfo.StoreID, portalID,orderdata.ObjCommonInfo.CultureName);
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, orderdata.ObjCommonInfo.StoreID, portalID, orderdata.ObjCommonInfo.CultureName);
            string inquiry = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailTo, orderdata.ObjCommonInfo.StoreID, portalID, orderdata.ObjCommonInfo.CultureName);
            string storeName = ssc.GetStoreSettingsByKey(StoreSetting.StoreName, orderdata.ObjCommonInfo.StoreID, portalID, orderdata.ObjCommonInfo.CultureName);
         
            if (bool.Parse(sendOrderNotice))
            {
                MessageTemplateDataContext dbMessageTemplate = new MessageTemplateDataContext(SystemSetting.SageFrameConnectionString);
                MessageTokenDataContext messageTokenDB = new MessageTokenDataContext(SystemSetting.SageFrameConnectionString);
                SageFrameConfig pagebase = new SageFrameConfig();
                var template = dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.ORDER_PLACED, portalID).SingleOrDefault();
                string messageTemplate = template.Body;
                if (template != null)
                {
                    string[] tokens = GetAllToken(messageTemplate);
                    foreach (string token in tokens)
                    {
                        switch (token)
                        {  
							case "%OrderRemarks%":
                                messageTemplate = messageTemplate.Replace(token, orderdata.ObjOrderDetails.Remarks);
                                break;
                            case "%InvoiceNo%":
                                messageTemplate = messageTemplate.Replace(token, orderdata.ObjOrderDetails.InvoiceNumber);
                                break;
                            case "%OrderID%":
                                messageTemplate = messageTemplate.Replace(token, orderdata.ObjOrderDetails.OrderID.ToString());
                                break;
                            case "%BillingAddress%":
                                string billing = orderdata.ObjBillingAddressInfo.FirstName.ToString() + " " +
                                    orderdata.ObjBillingAddressInfo.LastName.ToString() + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";

                                if (orderdata.ObjBillingAddressInfo.CompanyName != null)
                                {
                                    billing += orderdata.ObjBillingAddressInfo.CompanyName.ToString() + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                }

                                billing += orderdata.ObjBillingAddressInfo.City.ToString() + ", " + orderdata.ObjBillingAddressInfo.Address.ToString() + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                orderdata.ObjBillingAddressInfo.Country.ToString() + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                orderdata.ObjBillingAddressInfo.EmailAddress.ToString() + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                orderdata.ObjBillingAddressInfo.Phone.ToString() ;
                                messageTemplate = messageTemplate.Replace(token, billing);
                                break;
                            case "%ShippingAddress%":
                                string shipping = "";
                                if (!orderdata.ObjOrderDetails.IsDownloadable)
                                {
                                    if (orderdata.ObjOrderDetails.IsMultipleCheckOut == false)
                                    {
                                        shipping = orderdata.ObjShippingAddressInfo.FirstName.ToString() + " " +
                                           orderdata.ObjShippingAddressInfo.LastName.ToString() + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                        if (orderdata.ObjShippingAddressInfo.CompanyName != null)
                                        {
                                            shipping += orderdata.ObjShippingAddressInfo.CompanyName.ToString() + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                        }

                                        shipping += orderdata.ObjShippingAddressInfo.City.ToString() + ", " + orderdata.ObjShippingAddressInfo.Address.ToString() + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                              orderdata.ObjShippingAddressInfo.Country.ToString() + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                              orderdata.ObjShippingAddressInfo.EmailAddress.ToString() + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                              orderdata.ObjShippingAddressInfo.Phone.ToString() + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";

                                    }
                                    else
                                    {
                                        shipping = "Multiple addresses<br />Plese log in to view." + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                    }

                                }
                                else
                                {
                                    shipping = "Your Ordered Item is Downloadable Item." + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                }

                                messageTemplate = messageTemplate.Replace(token, shipping);
                                break;
                            case "%UserFirstName%":
                                messageTemplate = messageTemplate.Replace(token, orderdata.ObjBillingAddressInfo.FirstName);
                                break;
                            case "%UserLastName%":
                                messageTemplate = messageTemplate.Replace(token, orderdata.ObjBillingAddressInfo.LastName);
                                break;
                            case "%TransactionID%":
                                messageTemplate = messageTemplate.Replace(token, transID);
                                break;
                            case "%PaymentMethodName%":
                                messageTemplate = messageTemplate.Replace(token, orderdata.ObjPaymentInfo.PaymentMethodName);
                                break;
                            case "%DateTimeDay%":
                                messageTemplate = messageTemplate.Replace(token, DateTime.Now.ToString("dddd, dd MMMM yyyy"));
                                break;
                            case "%DateYear%":
                                messageTemplate = messageTemplate.Replace(token, DateTime.Now.Year.ToString());
                                break;
                            case "%CustomerID%":
                                messageTemplate = messageTemplate.Replace(token, orderdata.ObjOrderDetails.CustomerID.ToString());
                                break;
                            case "%PhoneNo%":
                                messageTemplate = messageTemplate.Replace(token, orderdata.ObjBillingAddressInfo.Phone);
                                break;
                            case "%AccountLogin%":
                                string account = "";
                                if (orderdata.ObjCommonInfo.AddedBy.ToString().ToLower() == "anonymoususer" && orderdata.ObjOrderDetails.CustomerID == 0)
                                {   // future login process for annoymoususr 
                                    account += "Please Register and log in to your <span style=\"font-weight: bold; font-size: 11px;\">AspxCommerce</span>";

                                    account += "<a  style=\"color: rgb(39, 142, 230);\"  href=" + addressPath + "User-Registration.aspx" + ">account</a>";
                                }
                                else
                                {
                                    account += "  Please log in to your <span style=\"font-weight: bold; font-size: 11px;\">AspxCommerce</span>";

                                    account += " <a style=\"color: rgb(39, 142, 230);\"  href=" + addressPath + "Login.aspx" + ">account</a>";
                                }
                                messageTemplate = messageTemplate.Replace(token, account);
                                break;
                            case "%LogoSource%":
                                // string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/Templates/" + templateName + "/images/aspxcommerce.png";
                                string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/" + logosrc;
                                messageTemplate = messageTemplate.Replace(token, src);
                                break;
                            case "%DateTime%":
                                messageTemplate = messageTemplate.Replace(token, DateTime.Now.ToString("dd MMMM yyyy "));

                                break;
                            case "%InquiryEmail%":
                                string x =
                                    "<a  target=\"_blank\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" href=\"mailto:" +
                                    inquiry + "\" >" + inquiry + "</a>";
                                messageTemplate = messageTemplate.Replace(token, x);
                                break;
                            case "%StoreName%":
                                messageTemplate = messageTemplate.Replace(token, storeName);
                                break;
                        }
                    }
                    // return messageTemplate;
                    MailHelper.SendMailNoAttachment(template.MailFrom, orderdata.ObjBillingAddressInfo.EmailAddress, template.Subject, messageTemplate, string.Empty, string.Empty);

                }
            }
            
            
           
        }

        public static void SendEmailForOrderSIM(int orderId,int storeID,int portalID,string custom,string billing, string billingadd,string billingcity, string shipping,string shippingadd, string shippingcity,string  payment, string info,string templateName,string transID,string remarks)
        {
              string[] infos = info.Split('#');            
                string[] payments = payment.Split('#');
                string[] ids = custom.Split('#');
                StoreSettingConfig ssc = new StoreSettingConfig();
                // sendEmailFrom = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, storeID, portalID, "en-US");
                string sendOrderNotice = ssc.GetStoreSettingsByKey(StoreSetting.SendOrderNotification, storeID, portalID,"en-US" );
                string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, storeID, portalID, "en-US");
                string storeName = ssc.GetStoreSettingsByKey(StoreSetting.StoreName, storeID, portalID, "en-US");
                string inquiry = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailTo, storeID, portalID, "en-US");
         
                if (bool.Parse(sendOrderNotice))
                {
                    MessageTemplateDataContext dbMessageTemplate = new MessageTemplateDataContext(SystemSetting.SageFrameConnectionString);
                    MessageTokenDataContext messageTokenDB = new MessageTokenDataContext(SystemSetting.SageFrameConnectionString);
                    SageFrameConfig pagebase = new SageFrameConfig();
                    var template = dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.ORDER_PLACED, portalID).SingleOrDefault();
                    string messageTemplate = template.Body;
                    if (template != null)
                    {
                        string[] tokens = GetAllToken(messageTemplate);
                        foreach (string token in tokens)
                        {
                            switch (token)
                            {
                                case "%OrderRemarks%":
                                    messageTemplate = messageTemplate.Replace(token, remarks);
                                    break;
                                case "%InvoiceNo%":
                                    messageTemplate = messageTemplate.Replace(token, infos[3].ToString());
                                    break;
                                case "%OrderID%":
                                    messageTemplate = messageTemplate.Replace(token, orderId.ToString());
                                    break;
                                case "%BillingAddress%":
                                    string billingfull = billing + billingadd + billingcity;
                                    messageTemplate = messageTemplate.Replace(token, billingfull);
                                    break;
                                case "%ShippingAddress%":
                                    string shippingFull = "";
                                    if (!bool.Parse(infos[5].ToString()))
                                    {
                                        if (bool.Parse(infos[6].ToString()) == false)
                                        {
                                            shippingFull = shipping + shippingcity + shippingadd;
                                        }
                                        else
                                        {
                                            shippingFull = "Multiple addresses<br />Plese log in to view." + "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                        }
                                    }
                                    else
                                    {
                                        shippingFull = "Your Ordered Item is Downloadable Item." + "</td></tr><tr><td  height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                    }

                                    messageTemplate = messageTemplate.Replace(token, shippingFull);
                                    break;
                                case "%UserFirstName%":
                                    messageTemplate = messageTemplate.Replace(token, infos[0].ToString());
                                    break;
                                case "%UserLastName%":
                                    messageTemplate = messageTemplate.Replace(token, "");
                                    break;
                                case "%TransactionID%":
                                    messageTemplate = messageTemplate.Replace(token, transID);
                                    break;
                                case "%PaymentMethodName%":
                                    messageTemplate = messageTemplate.Replace(token, payments[0].ToString());
                                    break;
                                case "%DateTimeDay%":
                                    messageTemplate = messageTemplate.Replace(token, payments[1].ToString());
                                    break;
                                case "%DateYear%":
                                    messageTemplate = messageTemplate.Replace(token, DateTime.Now.Year.ToString());
                                    break;
                                case "%CustomerID%":
                                    messageTemplate = messageTemplate.Replace(token, infos[2].ToString());
                                    break;
                                case "%PhoneNo%":
                                    messageTemplate = messageTemplate.Replace(token, infos[4].ToString());
                                    break;
                                case "%AccountLogin%":
                                    string account = "";
                                    if (infos[1].ToString().ToLower() == "anonymoususer" && int.Parse(infos[2].ToString()) == 0)
                                    {   // future login process for annoymoususr 
                                        account = "Please Register and log in to your <span style=\"font-weight: bold; font-size: 11px;\">AspxCommerce</span>";

                                        account += "<a  style=\"color: rgb(39, 142, 230);\" href=" + ids[6].Replace("Home", "User - Registration") + ">account</a>";
                                    }
                                    else
                                    {
                                        account = "Please log in to your <span style=\"font-weight: bold; font-size: 11px;\">AspxCommerce</span>";

                                        account += "<a style=\"color: rgb(39, 142, 230);\" href=" + ids[6].Replace("Home", "Login") + ">account</a>";
                                    }
                                    messageTemplate = messageTemplate.Replace(token, account);
                                    break;
                                case "%LogoSource%":
                                    //    string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/Templates/" + templateName + "/images/aspxcommerce.png";
                                    string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/" + logosrc;
                                    messageTemplate = messageTemplate.Replace(token, src);
                                    break;
                                case "%DateTime%":
                                    messageTemplate = messageTemplate.Replace(token, DateTime.Now.ToString("dd MMMM yyyy "));
                                    break;
                                case "%StoreName%":
                                    messageTemplate = messageTemplate.Replace(token, storeName);
                                    break;
                                case "%InquiryEmail%":
                                    string x =
                                     "<a  target=\"_blank\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" href=\"mailto:" +
                                     inquiry + "\" >" + inquiry + "</a>"; 
                                     messageTemplate = messageTemplate.Replace(token, x);
                                    break;

                            }
                        }
                        // return messageTemplate;
                        MailHelper.SendMailNoAttachment(template.MailFrom, infos[7].ToString(), template.Subject, messageTemplate, string.Empty, string.Empty);

                    }
                }


        }
       
        public static void SendEmailForOrderStatus(int storeID,int portalID, string recieverEmail, string billingshipping, string tablebody, string additionalFields, string templateName)
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, storeID, portalID, "en-US");
            string inquiry = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailTo, storeID, portalID, "en-US");
            MessageTemplateDataContext dbMessageTemplate = new MessageTemplateDataContext(SystemSetting.SageFrameConnectionString);
            MessageTokenDataContext messageTokenDB = new MessageTokenDataContext(SystemSetting.SageFrameConnectionString);
            SageFrameConfig pagebase = new SageFrameConfig();
            var template = dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.ORDER_STATUS_CHANGED, portalID).SingleOrDefault();
            string messageTemplate = template.Body.ToString();
            if (template != null)
            {
                string[] tokens = GetAllToken(messageTemplate);
                string[] fields = additionalFields.Split('#');

                string orderstatus = fields[0];
                string storeName = fields[1];
                string storeDescription = fields[2];
                string customerName = fields[3];
                string orderID = fields[4];
                string paymentMethod = fields[5];
                string shipingMethod = fields[6];
                string invoice = fields[7];
              string fullname=  GetFullName(portalID,int.Parse(orderID));
                foreach (string token in tokens)
                {
                    switch (token)
                    {
                    case "%OrderStatus%":
                            messageTemplate = messageTemplate.Replace(token, orderstatus);
                            break;
                        case "%StoreName%":
                            messageTemplate = messageTemplate.Replace(token, storeName);
                            break;
                        case "%StoreDescription%":
                            messageTemplate = messageTemplate.Replace(token, storeDescription);
                            break;
                        case "%ShippingMethod%":
                            messageTemplate = messageTemplate.Replace(token, shipingMethod);
                            break;
                        case "%InvoiceNo%":
                            messageTemplate = messageTemplate.Replace(token, invoice);
                            break;
                        case "%OrderID%":
                            messageTemplate = messageTemplate.Replace(token, orderID);
                            break;
                        case "%BillingShipping%":
                            messageTemplate = messageTemplate.Replace(token, billingshipping);
                            break;
                        case "%PaymentMethodName%":
                            messageTemplate = messageTemplate.Replace(token, paymentMethod);
                            break;
                        case "%DateTimeWithTime%":
                            messageTemplate = messageTemplate.Replace(token, DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                            break;
                        case "%DateTime%":
                            messageTemplate = messageTemplate.Replace(token, DateTime.Now.ToString("MM/dd/yyyy"));
                            break;
                        case "%CustomerName%":
                            messageTemplate = messageTemplate.Replace(token, fullname);
                            break;
                        case "%LogoSource%":
                            string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/" + logosrc;
                            messageTemplate = messageTemplate.Replace(token, src);
                            break;
                        case "%ItemDetailsTable%":
                            messageTemplate = messageTemplate.Replace(token, tablebody);
                            break;
                        case "%UserFirstName%":
                            messageTemplate = messageTemplate.Replace(token, fullname);
                            break;
                        case "%UserLastName%":
                            messageTemplate = messageTemplate.Replace(token, "");
                            break;
                        case "%InquiryEmail%":
                            string x =
                                     "<a  target=\"_blank\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" href=\"mailto:" +
                                     inquiry + "\" >" + inquiry + "</a>"; messageTemplate = messageTemplate.Replace(token, x);
                            break;

                    }
                }
                // return messageTemplate;

                //  string replacedMessageTemplate = EmailTemplate.GetTemplateForOrderStatus(template.Body, billingShipping, itemTable, additionalFields);
                MailHelper.SendMailNoAttachment(template.MailFrom, recieverEmail, template.Subject, messageTemplate, string.Empty, string.Empty);

            }
        }

        public static void SendEmailForSharedWishList(int storeId, int portalId, string  cultureName,string  senderName, string senderEmail, string receiverEmailDs ,string subject,string message,string bodyDetail)
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL,storeId,portalId,cultureName);
            MessageTemplateDataContext dbMessageTemplate = new MessageTemplateDataContext(SystemSetting.SageFrameConnectionString);
            MessageTokenDataContext messageTokenDB = new MessageTokenDataContext(SystemSetting.SageFrameConnectionString);
            var template =  dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.SHARED_WISHED_LIST, portalId).SingleOrDefault();
            string messageTemplate =  template.Body.ToString();
            string src =  HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
            if(template != null)
            {
                string[] tokens = GetAllToken(messageTemplate);
                foreach (string token in tokens)
                {
                    switch (token)
                    {
                        case "%DateTime%":
                            messageTemplate = messageTemplate.Replace(token, System.DateTime.Now.ToString("MM/dd/yyyy"));
                            break;
                        case "%Username%":
                            messageTemplate = messageTemplate.Replace(token, senderName);
                            break;
                        case "%UserEmail%":
                            messageTemplate = messageTemplate.Replace(token, senderEmail);
                            break;
                        case "%MessageDetails%":
                            messageTemplate = messageTemplate.Replace(token, message);
                            break;
                        case "%ItemDetailsTable%":
                            messageTemplate = messageTemplate.Replace(token, bodyDetail);
                            break;
                        case "%LogoSource%":
                            string imgSrc = "http://" + src + logosrc;
                            messageTemplate = messageTemplate.Replace(token, imgSrc);
                            break;
                        case "%ServerPath%":
                            messageTemplate = messageTemplate.Replace(token, "http://" + src);
                            break;
                        case "%DateYear%":
                            messageTemplate = messageTemplate.Replace(token, System.DateTime.Now.Year.ToString());
                            break;
                    }
                }
            }

            char[] spliter = {','};
            string[] receiverIDs = receiverEmailDs.Split(spliter);

            for (int i = 0; i < receiverIDs.Length; i++)
            {
                string receiverEmailID = receiverIDs[i];
                string emailSuperAdmin;
                string emailSiteAdmin;
                SageFrameConfig pagebase = new SageFrameConfig();
                emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                MailHelper.SendMailNoAttachment(senderEmail, receiverEmailID, subject, messageTemplate, emailSiteAdmin, emailSuperAdmin);
            }
        }

        public static void SendEmailForReferFriend(int storeId, int portalId, string cultureName, string senderName, string senderEmail, string receiverEmailID, string subject, string message, string messageBodyDetail)
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, storeId, portalId, cultureName);
            MessageTemplateDataContext dbMessageTemplate = new MessageTemplateDataContext(SystemSetting.SageFrameConnectionString);
            MessageTokenDataContext messageTokenDB = new MessageTokenDataContext(SystemSetting.SageFrameConnectionString);
            var template =dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.REFER_A_FRIEND_EMAIL, portalId).SingleOrDefault();
            string messageTemplate = template.Body.ToString();
            string src = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
            if (template != null)
            {
                string[] tokens = GetAllToken(messageTemplate);
                foreach (string token in tokens)
                {
                    switch (token)
                    {
                        case "%DateTime%":
                            messageTemplate = messageTemplate.Replace(token, System.DateTime.Now.ToString("MM/dd/yyyy"));
                            break;
                        case "%Username%":
                            messageTemplate = messageTemplate.Replace(token, senderName);
                            break;
                            //case "%senderEmail%":
                            //    messageTemplate = messageTemplate.Replace(token, senderEmail);
                            //    break;
                        case "%MessageDetails%":
                            messageTemplate = messageTemplate.Replace(token, message);
                            break;
                        case "%ItemDetailsTable%":
                            messageTemplate = messageTemplate.Replace(token, messageBodyDetail);
                            break;
                        case "%LogoSource%":
                            string imgSrc = "http://" + src + logosrc;
                            messageTemplate = messageTemplate.Replace(token, imgSrc);
                            break;
                        case "%serverPath%":
                            messageTemplate = messageTemplate.Replace(token, "http://" + src);
                            break;
                        case "%DateYear%":
                            messageTemplate = messageTemplate.Replace(token, System.DateTime.Now.Year.ToString());
                            break;
                    }
                }
            }

            string emailSuperAdmin;
            string emailSiteAdmin;
            SageFrameConfig pagebase = new SageFrameConfig();
            emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
            emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
            MailHelper.SendMailNoAttachment(senderEmail, receiverEmailID, subject, messageTemplate, emailSiteAdmin, emailSuperAdmin);
        }
        
       public static string GetFullName(int portalId,int orderid)
       {
           List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
           paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalId));
           paramCol.Add(new KeyValuePair<string, object>("@OrderID", orderid));
           SQLHandler sageSQL = new SQLHandler();
           return sageSQL.ExecuteAsScalar<string>("[dbo].[usp_Aspx_GetUserFirstandLastNamebyorder]", paramCol);

       }
   }
}
