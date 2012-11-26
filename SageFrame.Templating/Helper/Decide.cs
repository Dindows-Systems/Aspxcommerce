using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Templating.xmlparser;
using System.Web;
using System.IO;

namespace SageFrame.Templating
{
    public class Decide
    {
        public static bool HasBlock(Placeholders pch, XmlTag middleBlock)
        {
            bool status = false;
            status = middleBlock.LSTChildNodes.Exists(
                delegate(XmlTag tag)
                {
                    return (Utils.CompareStrings(Utils.GetAttributeValueByName(tag, XmlAttributeTypes.NAME), pch));
                }
                );
            return status;
        }
        public static bool IsSpotLight(XmlTag placeholder)
        {
            string pchName = Utils.GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME);
            bool status = false;
            if (Utils.CompareStrings(pchName, "spotlight"))
            {
                status = true;
            }
            return status;
        }

        public static bool IsCustomBlockDefined(XmlTag placeholder)
        {

            string activeTemplate = HttpContext.Current.Session["SageFrame.ActiveTemplate"] != null ? HttpContext.Current.Session["SageFrame.ActiveTemplate"].ToString() : "Default";
            string pchName = Utils.GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME);
            string FilePath = "E://DotNetProjects//sftemplating//SageFrame//" + activeTemplate + "//sections";
            bool status = false;
            if (Directory.Exists(FilePath))
            {
                DirectoryInfo dir = new DirectoryInfo(FilePath);
                foreach (FileInfo file in dir.GetFiles("*.htm"))
                {
                    if (Utils.CompareStrings(Path.GetFileNameWithoutExtension(file.Name), pchName))
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }


    }
}
