using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Templating.factory
{
    public abstract class AbstractTagFactory
    {
        public abstract SFHeader CreateHeader();
        public abstract SFFooter CreateFooter();
    }
}
