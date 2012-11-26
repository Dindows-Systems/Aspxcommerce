using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Templating.xmlparser;

namespace SageFrame.Templating
{
    public class LayoutControlGenerator
    {
        public string GenerateHTML(List<XmlTag> lstTags)
        {
            string markup = "";
            foreach (XmlTag tag in lstTags)
            {
                if (tag.TagType == XmlTagTypes.Section)
                {
                    markup += GenerateSectionMarkup(tag);
                }

            }

            // txtTest.Text = GenerateExternalWrapper(markup);
            return (GenerateExternalWrapper(markup));
        }

        public string GeneratePlaceHolderMarkup(XmlTag Section)
        {
            foreach (XmlTag tag in Section.LSTChildNodes)
            {
                //if(tag.
            }
            return "";
        }

        public string GenerateSectionMarkup(XmlTag section)
        {
            string markup = "";
            if (section.AttributeCount > 0)
            {
                foreach (LayoutAttribute attr in section.LSTAttributes)
                {
                    switch (attr.Type)
                    {
                        case XmlAttributeTypes.NAME:
                            markup = GetSectionMarkup(attr.Value, section);
                            break;
                        case XmlAttributeTypes.TYPE:
                            break;
                        case XmlAttributeTypes.SPLIT:
                            break;
                    }
                }
            }
            return markup;
        }


        public string GetSectionMarkup(string name, XmlTag section)
        {
            string html = "";
            if (Enum.IsDefined(typeof(SectionTypes), name.ToUpper()))
            {
                SectionTypes _type = (SectionTypes)Enum.Parse(typeof(SectionTypes), name.ToUpper());


                switch (_type)
                {
                    case SectionTypes.TOP:
                        html = GetTopMarkup(section);
                        break;
                    case SectionTypes.MIDDLE:
                        html = GetMiddleWrapper(section);
                        break;
                    case SectionTypes.BOTTOM:
                        html = GetBottomMarkup(section);
                        break;

                }
            }
            return html;
        }

        public string GenerateExternalWrapper(string markup)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"sfOuterWrapper\">");
            sb.Append(markup);
            sb.Append("</div>");
            return sb.ToString();
        }

        public string GetTopMarkup(XmlTag section)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(LayoutControlBuilder.GetTopBlocks(section));
            return sb.ToString();
        }

        public string GetMiddleWrapper(XmlTag section)
        {
            StringBuilder sb = new StringBuilder();

            string test = LayoutControlBuilder.GenerateMiddleBlockStart(section);
            sb.Append(test);
            return sb.ToString(); ;
        }
        public string GetBottomMarkup(XmlTag section)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(LayoutControlBuilder.GetBottomBlocks(section));
            return sb.ToString();
        }



        public string GetPrefix(int count)
        {
            string prefix = "";
            for (int i = 0; i < count; i++)
            {
                prefix += "--";
            }
            return prefix;
        }

        public string GetAttributeString(List<LayoutAttribute> lstAttr)
        {


            StringBuilder sb = new StringBuilder();
            foreach (LayoutAttribute attr in lstAttr)
            {
                sb.Append("[");
                sb.Append(attr.Name);
                sb.Append(":");
                sb.Append(attr.Value);
                sb.Append("],");
            }
            return sb.ToString();
        }
    }
}
