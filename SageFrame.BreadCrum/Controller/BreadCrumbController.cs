using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.BreadCrum.Controller
{
    public class BreadCrumbController
    {
        public static BreadCrumInfo GetBreadCrumb(string SEOName, int PortalID)
        {
            try
            {
                return (BreadCrumDataProvider.GetBreadCrumb(SEOName, PortalID));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
