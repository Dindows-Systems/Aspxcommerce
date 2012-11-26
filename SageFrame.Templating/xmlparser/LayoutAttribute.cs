using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Templating.xmlparser
{
    public class LayoutAttribute
    {
        public int Index = -1;
		public string Name = null;
		public string Value = null;
		public AttributeTypes AttributeType = AttributeTypes.Attribute;
		public string Text = null;
		public string TagFormat = null;
        public XmlAttributeTypes Type { get; set; }

		public LayoutAttribute()
		{

		}

        public LayoutAttribute(string name, string value, XmlAttributeTypes _type)
		{
			this.Name = name;
			this.Value = value;
            this.Type = _type;
		}
    }
}
