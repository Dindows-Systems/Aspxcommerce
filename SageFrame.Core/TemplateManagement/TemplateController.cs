using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Core.TemplateManagement
{
    public class TemplateController
    {
        public static List<TemplateInfo> GetTemplateList(int PortalID, string UserName)
        {
            try
            {
                return (TemplateDataProvider.GetTemplateList(PortalID, UserName));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public static bool AddTemplate(TemplateInfo obj)
        {
            try
            {
                return (TemplateDataProvider.AddTemplate(obj));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
