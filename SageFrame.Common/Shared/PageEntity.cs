using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Common
{
    public class PageEntity
    {
        public int PageID { get; set; }
        public string PageName { get; set; }
        public int PageOrder { get; set; }
        public int Level { get; set; }
        public int ParentID { get; set; }
        public string Prefix { get; set; }
        public bool IsRequiredPage { get; set; }
        public bool IsActive { get; set; }
        public bool IsSecure { get; set; }
        public int MaxPageOrder { get; set; }
        public int MinPageOrder { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public PageEntity() { }
    }
}
