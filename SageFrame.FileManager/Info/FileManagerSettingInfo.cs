using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.FileManager
{
    public class FileManagerSettingInfo
    {
        public int FileManagerSettingValueID { get; set; }
        public int UserModuleID { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string AddedOn { get; set; }
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int PortalID { get; set; }
        
        public FileManagerSettingInfo()
        {
        }
    }
}
