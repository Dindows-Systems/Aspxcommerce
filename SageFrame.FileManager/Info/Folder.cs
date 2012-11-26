using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.FileManager
{
    public class Folder
    {
        public int FolderId { get; set; }
        public int ParentID { get; set; }
        public int PortalId { get; set; }
        public string FolderPath { get; set; }
        public int StorageLocation { get; set; }
        public Guid UniqueId { get; set; }
        public Guid VersionGuid { get; set; }
        public int IsActive { get; set; }
        public bool IsEnabled { get; set; }
        public string AddedBy { get; set; }
        public bool IsRoot { get; set; }
        public Folder() { }
    }
}
