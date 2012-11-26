using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Localization
{
    public class LanguageSwitchKeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string AddedBy { get; set; }
        public bool IsActive { get; set; }
        public LanguageSwitchKeyValue() { }
        public LanguageSwitchKeyValue(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
