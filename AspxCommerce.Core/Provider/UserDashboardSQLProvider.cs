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
using System.Collections.Generic;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class UserDashboardSQLProvider
    {
       public void AddUpdateUserAddress(int addressID, int customerID, string firstName, string lastName, string email, string company,
        string address1, string address2, string city, string state, string zip, string phone, string mobile,
   string fax,string webSite,string countryName, bool isDefaultShipping, bool isDefaultBilling, int storeID, int portalID, string userName, string cultureName)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@AddressID", addressID));
           parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
           parameter.Add(new KeyValuePair<string, object>("@FirstName", firstName));
           parameter.Add(new KeyValuePair<string, object>("@LastName", lastName));
           parameter.Add(new KeyValuePair<string, object>("@Email", email));
           parameter.Add(new KeyValuePair<string, object>("@Company", company));
           parameter.Add(new KeyValuePair<string, object>("@Address1", address1));
           parameter.Add(new KeyValuePair<string,object>("@Address2",address2));
           parameter.Add(new KeyValuePair<string, object>("@City", city));
           parameter.Add(new KeyValuePair<string, object>("@State", state));
           parameter.Add(new KeyValuePair<string, object>("@Zip", zip));
           parameter.Add(new KeyValuePair<string, object>("@Phone", phone));
           parameter.Add(new KeyValuePair<string, object>("@Mobile", mobile));
           parameter.Add(new KeyValuePair<string, object>("@Fax", fax));
           parameter.Add(new KeyValuePair<string, object>("@WebSite", webSite));
           parameter.Add(new KeyValuePair<string, object>("@Country", countryName));
           parameter.Add(new KeyValuePair<string, object>("@IsDefaultShipping", isDefaultShipping));
           parameter.Add(new KeyValuePair<string, object>("@IsDefaultBilling", isDefaultBilling));
           parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
           parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateUserAddress", parameter);
       }
       public List<AddressInfo> GetUserAddressDetails(int storeID, int portalID, int customerID, string userName, string cultureName)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
           parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
           parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
           SQLHandler sqlh = new SQLHandler();
           return sqlh.ExecuteAsList<AddressInfo>("usp_Aspx_GetUserAddressBookDetails", parameter);
       }
       public void DeleteAddressBookDetails(int addressID, int storeID, int portalID, string userName, string cultureName)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@AddressID", addressID));
           parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
           parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_DeleteAddressBook", parameter);
       }
       public List<UserProductReviewInfo> GetUserProductReviews(int storeID, int portalID, string userName, string cultureName)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
           parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
           SQLHandler sqlH = new SQLHandler();
          return  sqlH.ExecuteAsList<UserProductReviewInfo>("usp_Aspx_GetUserProductReviews", parameter);
       }
       public void UpdateUserProductReview(int itemID, int itemReviewID, string ratingIDs, string ratingValues, string reviewSummary, string review, int storeID, int portalID, string userName)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
           parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
           parameter.Add(new KeyValuePair<string, object>("@RatingIDs", ratingIDs));
           parameter.Add(new KeyValuePair<string, object>("@RatingValues", ratingValues));
           parameter.Add(new KeyValuePair<string, object>("@ReviewSummary", reviewSummary));
           parameter.Add(new KeyValuePair<string, object>("@Review", review));
           parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_GetUserProductReviewUpdate", parameter);
           
       }
       public void DeleteUserProductReview(int itemID, int itemReviewID, int storeID, int portalID, string userName)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
           parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
           parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_DeleteUserProductReview", parameter);
       }
    }
}
