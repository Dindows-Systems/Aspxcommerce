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
using System.Collections;

namespace AspxCommerce.Core
{
    public class CouponManageSQLProvider
    {
        public List<CouponInfo> BindAllCouponDetails(int offset, int limit, System.Nullable<int> couponTypeId, string couponCode, System.Nullable<DateTime> validateFrom, System.Nullable<DateTime> validateTo, int storeId, int portalId, string cultureName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", couponTypeId));
            parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponCode));
            parameter.Add(new KeyValuePair<string, object>("@ValidateFrom", validateFrom));
            parameter.Add(new KeyValuePair<string, object>("@ValidateTo", validateTo));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponInfo>("usp_Aspx_GetCouponDetails", parameter);
        }

        public List<CouponPortalUserListInfo> GetPortalUsersList(int offset, int limit, int couponID, int storeID, int portalID, string customerName, string cultureName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@CouponID", couponID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CustomerName", customerName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponPortalUserListInfo>("usp_Aspx_GetAllPortalUserLists", parameter);
        }

        public void AddUpdateCoupons(int couponID, int couponTypeID, string couponCode, string couponAmount, string validateFrom, string validateTo,
        string isActive, int storeID, int portalID, string cultureName, string userName, string settingIDs, string settingValues, string portalUser_UserName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CouponID", couponID));
                parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", couponTypeID));
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponCode));
                parameter.Add(new KeyValuePair<string, object>("@CouponAmount", couponAmount));
                parameter.Add(new KeyValuePair<string, object>("@ValidateFrom", validateFrom));
                parameter.Add(new KeyValuePair<string, object>("@ValidateTo", validateTo));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@SettingIDs", settingIDs));
                parameter.Add(new KeyValuePair<string, object>("@SettingValues", settingValues));
                parameter.Add(new KeyValuePair<string, object>("@portalUser_UserName", portalUser_UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateCoupons", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CouponStatusInfo> BindCouponStatus()
        {
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponStatusInfo>("usp_Aspx_GetCouponStatus");
        }

        public List<CouponSettingKeyValueInfo> GetCouponSettingKeyValueInfo(int couponID, int storeID, int portalID)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CouponID", couponID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponSettingKeyValueInfo>("usp_Aspx_GetCouponSettingKeyValueByCouponID", parameter);
        }

        public void DeleteCoupons(string couponIDs, int storeID, int portalID, string userName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CouponID", couponIDs));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteCoupons", parameter);
        }

        public CouponVerificationInfo VerifyUserCoupon(decimal totalCost, string couponCode, int storeID, int portalID, string userName, int appliedCount)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@totalCost", totalCost));
            parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponCode));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@AppliedCount", appliedCount));            
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsObject<CouponVerificationInfo>("[usp_Aspx_VerifyCouponCode]",parameter);
        }

        public void UpdateCouponUserRecord(string couponCode, int storeID, int portalID, string userName)
        {
            if (System.Web.HttpContext.Current.Session["CouponApplied"] != null)
            {
                int ac =int.Parse( System.Web.HttpContext.Current.Session["CouponApplied"].ToString());
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponCode));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CouponUsedCount", ac));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateCouponUserRecord", parameter);
            }
        }

        public List<CouponUserInfo> GetCouponUserDetails(int offset, System.Nullable<int> limit, string couponCode, string userName, System.Nullable<int> couponStatusId, string validFrom, string validTo, int storeID, int portalID, string cultureName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponCode));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@CouponStatusID", couponStatusId));
            parameter.Add(new KeyValuePair<string, object>("@ValidFrom", validFrom));
            parameter.Add(new KeyValuePair<string, object>("@ValidTo", validTo));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponUserInfo>("usp_Aspx_CouponUserDetails", parameter);
        }

        public List<CouponUserListInfo> GetCouponUserList(int offset, int limit, int couponID, string couponCode, string userName, System.Nullable<int> couponStatusID, int storeID, int portalID, string cultureName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@CouponID", couponID));
            parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponCode));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@CouponStatusID", couponStatusID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponUserListInfo>("[usp_Aspx_GetCouponUserList]", parameter);
        }


        public void DeleteCouponUser(string couponUserID, int storeID, int portalID, string userName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CouponUserID", couponUserID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteCouponUser", parameter);
        }
        public void UpdateCouponUser(int couponUserID,int couponStatusID, int storeID, int portalID, string cultureName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CouponUserID", couponUserID));
            parameter.Add(new KeyValuePair<string, object>("@CouponStatusID", couponStatusID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_UpdateCouponUser", parameter);
        }

        public void SendCouponCodeEmail(string senderEmail, string receiverEmailDs, string subject,ArrayList messageBody)
        {
            string[] receiverIDs;
            char[] spliter = { '#' };
            receiverIDs = receiverEmailDs.Split(spliter);

            for (int i = 0; i < receiverIDs.Length; i++)
            {
                string receiverEmailID = receiverIDs[i];
                string emailSuperAdmin;
                string emailSiteAdmin;
                SageFrameConfig pagebase = new SageFrameConfig();
                emailSuperAdmin =pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
              string individualMsgBody = messageBody[i].ToString();
              MailHelper.SendMailNoAttachment(senderEmail, receiverEmailID, subject, individualMsgBody, emailSiteAdmin, emailSuperAdmin);
            }
        }
    }
}
