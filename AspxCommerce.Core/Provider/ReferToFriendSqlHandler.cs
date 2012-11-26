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
using SageFrame.Web.Utilities;
using SageFrame.Web;
using SageFrame.SageFrameClass.MessageManagement;

namespace AspxCommerce.Core
{
    public class ReferToFriendSqlHandler
    {
        public void SaveEmailMessage(int storeID, int portalID, int itemID, string senderName, string senderEmail, string receiverName, string receiverEmail, string subject, string message)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            parameter.Add(new KeyValuePair<string, object>("@SenderName", senderName));
            parameter.Add(new KeyValuePair<string, object>("@SenderEmail", senderEmail));
            parameter.Add(new KeyValuePair<string, object>("@ReceiverName", receiverName));
            parameter.Add(new KeyValuePair<string, object>("@Receiveremail", receiverEmail));
            parameter.Add(new KeyValuePair<string, object>("@Subject", subject));
            parameter.Add(new KeyValuePair<string, object>("@Message", message));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_SaveMessage", parameter);

        }

        public void SendEmail(int storeId, int portalId,string senderName, string senderEmail, string receiverEmail, string subject, string message, string messageBodyDetail,string cultureName)
        {
            try
            {
                EmailTemplate.SendEmailForReferFriend(storeId,portalId,cultureName,senderName,senderEmail, receiverEmail,subject,message,messageBodyDetail);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
   
        public void SaveShareWishListEmailMessage(int storeID, int portalID, string itemID, string senderName, string senderEmail, string receiverEmailID, string subject, string message, string cultureName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@ItemIDs", itemID));
            parameter.Add(new KeyValuePair<string, object>("@SenderName", senderName));
            parameter.Add(new KeyValuePair<string, object>("@SenderEmail", senderEmail));
            parameter.Add(new KeyValuePair<string, object>("@ReceiverEmailID", receiverEmailID));
            parameter.Add(new KeyValuePair<string, object>("@Subject", subject));
            parameter.Add(new KeyValuePair<string, object>("@Message", message));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("[usp_Aspx_SaveShareWishListEmail]", parameter);

        }

        public void SendShareWishItemEmail(int storeId, int portalId, string senderName, string senderEmail, string receiverEmailDs, string subject, string message, string bodyDetail, string cultureName)
        {
            try
            {
                EmailTemplate.SendEmailForSharedWishList(storeId, portalId, cultureName, senderName, senderEmail, receiverEmailDs, subject, message, bodyDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
