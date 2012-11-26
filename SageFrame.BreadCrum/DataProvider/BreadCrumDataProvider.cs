using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace SageFrame.BreadCrum
{
    public class BreadCrumDataProvider
    {

        public static BreadCrumInfo GetBreadCrumb(string SEOName, int PortalID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@SEOName", SEOName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            try
            {
                SQLHandler SQLH = new SQLHandler();
                return SQLH.ExecuteAsObject<BreadCrumInfo>("[dbo].[usp_BreadCrumbGetFromPageName]", ParaMeterCollection);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
    }
}
