using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SageFrame.FileManager
{
    public class FileCacheInfo
    {
        public int FolderID { get; set; }
        public List<ATTFile> LSTFiles { get; set; }
        public FileCacheInfo() { }

    }
   
}
