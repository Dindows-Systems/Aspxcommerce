using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Templating.factory
{
    public class TagFactory:AbstractTagFactory
    {
        public override SFHeader CreateHeader()
        {
            return new SFHeader();
        }
        public override SFFooter CreateFooter()
        {
            return new SFFooter();
        }
    }
}
