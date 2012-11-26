using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Templating.xmlparser;
using System.IO;

namespace SageFrame.Templating
{
    public class LayoutHelper
    {
        public static void CreateLayoutControls(string TemplateName, PresetInfo PresetObj)
        {

            string templatePath = Utils.GetTemplatePath(TemplateName);
            string presetPath = Utils.GetPresetPath(TemplateName);
            LayoutControlGenerator lg = new LayoutControlGenerator();
            XmlParser parser = new XmlParser();

            PresetInfo presetdetails = PresetHelper.LoadPresetDetails(presetPath + "/" + PresetObj.PresetName.Replace(".xml", "") + ".xml");
            List<XmlTag> lstXmlTag = parser.GetXmlTags(templatePath + "/layouts/default/" + presetdetails.ActiveLayout.Replace(".xml", "") + ".xml", "layout/section");
            string html = lg.GenerateHTML(lstXmlTag);
            string controlname = PresetObj.ActiveLayout + ".ascx";
            if (!File.Exists(templatePath + "/" + controlname))
            {
                FileStream fs = null;
                using (fs = File.Create(templatePath + "/" + controlname))
                {

                }

            }
            else
            {
                File.Delete(templatePath + "/" + controlname);
                FileStream fs = null;
                using (fs = File.Create(templatePath + "/" + controlname))
                {

                }
            }

            using (StreamWriter sw = new StreamWriter(templatePath + "/" + controlname))
            {
                sw.Write("<%@ Control Language=\"C#\" ClassName=" + PresetObj.PresetName + " %>");
                sw.Write(html);
            }


        }
    
    }
}
