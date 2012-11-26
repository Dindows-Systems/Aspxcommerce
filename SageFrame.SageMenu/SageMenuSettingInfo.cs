using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.SageMenu
{
    public class SageMenuSettingInfo
    {
        public string SettingKey { get; set; }
        public string SettingValue{ get; set; }
        public string MenuType { get; set; }
        public int PortalID { get; set; }
        public int UserModuleID { get; set; }
        public string AddedBy { get; set; }
        public bool IsActive { get; set; }
        public string UpdatedBy { get; set; }

        public SageMenuSettingInfo() { }
    }
}
