using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SageFrame.Templating
{
    public interface IHTMLBuilder
    {
        string GenerateOuterWrappers();
        string GenerateDefaultSectionWrappers();
        string GenerateBlockWrappers(string placeholder);
        string GetHTMLContent(string htmlfile,string placeholder);
        void PutPlaceHolders(string name, ref string html,string placeholder);
    }
}
