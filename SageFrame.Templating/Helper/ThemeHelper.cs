using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Templating
{
    public class ThemeHelper
    {
        public static string GetAdminTheme(int PortalID,string UserName)
        {
            SettingInfo objSetting = TemplateController.GetSettingByKey(new SettingInfo("DASHBOARD_THEME", UserName, PortalID));
            return (objSetting!=null?objSetting.SettingValue.ToString():"default");

        }
        
    }
}
