using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SageFrame.Core.TemplateManagement
{
    public static class TemplateSettings
    {
        public static string BaseDir
        {
            get {
                string root = HttpContext.Current.Request.ApplicationPath.ToString();
                root = root.Substring(1, root.Length - 1);
                return root;
            }
            
        }

        public static string TemplatePath = "\\"+BaseDir + "\\Templates\\";
    }
}
