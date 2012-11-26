using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.AccessControl;

namespace SageFrame.Common
{
    public class CssScriptInfo
    {
        public int Index { get; set; }
        public string ModuleName { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public int MyProperty { get; set; }
        public bool AllowOptimization { get; set; }
        public bool AllowCombination { get; set; }
        public int Position { get; set; }
        public ResourceType rtype { get; set; }


        public CssScriptInfo(string _ModuleName, string _FileName, string _Path, int _Position)
        {
            this.ModuleName = _ModuleName;
            this.FileName = _FileName;
            this.Path = _Path;
            this.Position = _Position;

        }
        public CssScriptInfo(string _ModuleName, string _FileName)
        {
            this.ModuleName = _ModuleName;
            this.FileName = _FileName;
        }



    }
}