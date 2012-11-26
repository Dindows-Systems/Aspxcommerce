using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SageFrame.Templating.xmlparser;
using System.IO;

namespace SageFrame.Templating
{
    public class PresetHelper
    {
        public static List<PresetInfo> ParsePreset(string xmlFile, string startParseNode)
        {
            List<PresetInfo> lstPreset = new List<PresetInfo>();
            XmlDocument doc = XmlHelper.LoadXMLDocument(xmlFile);
            XmlNodeList sectionList = doc.SelectNodes(startParseNode);
            foreach (XmlNode preset in sectionList)
            {
                PresetInfo tag = new PresetInfo();
                tag.PresetName = preset.Attributes["preset"].Value;
                tag.LSTPages = PageList(preset.InnerText);
                lstPreset.Add(tag);

            }
            return lstPreset;
        }

        public static List<PresetInfo> ParsePreset(string xmlFile, string startParseNode,out List<string> lstAllPages)
        {
            List<PresetInfo> lstPreset = new List<PresetInfo>();
            XmlDocument doc = XmlHelper.LoadXMLDocument(xmlFile);
            XmlNodeList sectionList = doc.SelectNodes(startParseNode);
            List<string> lstAllPagesIn = new List<string>();
            foreach (XmlNode preset in sectionList)
            {
                PresetInfo tag = new PresetInfo();
                tag.PresetName = preset.Attributes["preset"].Value;
                tag.LSTPages = PageList(preset.InnerText);                
                lstPreset.Add(tag);
                if (preset.InnerText.Contains("All") || preset.InnerText.Equals("All") || preset.InnerText.Equals("All"))
                {
                    tag.IsDefault = true;
                }
                else
                {
                    tag.IsDefault = false;
                }
                foreach (string page in tag.LSTPages)
                {
                    lstAllPagesIn.Add(page);
                }

            }
            lstAllPages = lstAllPagesIn;
            return lstPreset;
        }


        static List<string> PageList(string pages)
        {
            List<string> lstPages = new List<string>();
            string[] arrPages = pages.Split(',');

            foreach (string page in arrPages)
            {
                string expr = page;
                lstPages.Add((expr.IndexOf('*') == -1 || expr.IndexOf("All") == -1) ? expr: expr);

            }
            return lstPages;
        }

        public static PresetInfo LoadPresetDetails(string xmlPath)
        {
            XmlDocument doc = XmlHelper.LoadXMLDocument(xmlPath);
            XmlNode xnpreset = doc.SelectSingleNode("preset");
            XmlNodeList xnlist = xnpreset.ChildNodes;
            PresetInfo preset = new PresetInfo();
            preset.PresetName = doc.SelectSingleNode("preset").Attributes["name"].Value;
            foreach (XmlNode node in xnlist)
            {
                switch (node.Name)
                {
                    case "activelayout":
                        preset.ActiveLayout = node.InnerText;
                        break;
                    case "activetheme":
                        preset.ActiveTheme = node.InnerText;
                        break;
                    case "activewidth":
                        preset.ActiveWidth = node.InnerText;
                        break;
                    case "cssopt":
                        preset.IsCssOptimizationEnabled = bool.Parse(node.InnerText);
                        break;
                    case "jsopt":
                        preset.IsJsOptimizationEnabled = bool.Parse(node.InnerText);
                        break;
                    case "cpanel":
                        preset.CPanel = bool.Parse(node.InnerText);
                        break;
                    case "handheld":
                        preset.HandHeld = bool.Parse(node.InnerText);
                        break;
                    case "handheldlayout":
                        preset.HandHeldLayout = node.InnerText;
                        break;

                }

            }
            return preset;

        }

        public static void WritePreset(string xmlpath, PresetInfo objPreset)
        {
            if (File.Exists(xmlpath))
            {
                File.Delete(xmlpath);
                WriteNewPreset(xmlpath, objPreset);

            }
            else
            {
                WriteNewPreset(xmlpath, objPreset);
            }
        }

        static void WriteNewPreset(string xmlpath, PresetInfo objPreset)
        {
            using (XmlTextWriter writer = new XmlTextWriter(xmlpath, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("preset");
                writer.WriteAttributeString("name", Path.GetFileNameWithoutExtension(objPreset.PresetName));
                writer.WriteStartElement("activetheme");
                writer.WriteString(objPreset.ActiveTheme);
                writer.WriteEndElement();

                writer.WriteStartElement("activelayout");
                writer.WriteString(objPreset.ActiveLayout);
                writer.WriteEndElement();

                writer.WriteStartElement("activewidth");
                writer.WriteString(objPreset.ActiveWidth);
                writer.WriteEndElement();

                writer.WriteStartElement("cssopt");
                writer.WriteString(objPreset.IsCssOptimizationEnabled.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("jsopt");
                writer.WriteString(objPreset.IsJsOptimizationEnabled.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("cpanel");
                writer.WriteString(objPreset.CPanel.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("handheld");
                writer.WriteString(objPreset.HandHeld.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("handheldlayout");
                writer.WriteString(objPreset.HandHeldLayout);
                writer.WriteEndElement();


                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
        }

        public static int UpdatePresetPages(PresetInfo objPreset, string xmlpath)
        {
            XmlDocument doc = XmlHelper.LoadXMLDocument(xmlpath);
            XmlNode xnpreset = doc.SelectSingleNode("pagepresets");
            XmlNodeList xnlist = xnpreset.ChildNodes;
                        
            bool isApplied = false;
            bool isAllPageApplied = false;
            bool isPageApplied = false;
            int nodecount = 0;
            foreach (XmlNode node in xnlist)
            {

                if (node.Attributes["preset"].Value == objPreset.PresetName || node.Attributes["preset"].Value == Path.GetFileNameWithoutExtension(objPreset.PresetName.ToLower()))
                {
                    isApplied = true;
                    node.InnerText = objPreset.Pages;                   
                }
                if (node.InnerText.ToLower() == "" || node.InnerText.ToLower() == "none")
                {
                    xnlist[nodecount].ParentNode.RemoveChild(xnlist[nodecount]);
                }
                else if (node.InnerText.ToLower() == "all" && objPreset.Pages.ToLower()=="all")
                {
                    isAllPageApplied = true;
                }
                if (node.InnerText.ToLower() != "all" && Path.GetFileNameWithoutExtension(objPreset.Pages.ToLower()) != "all")
                {
                    string[] arrPages = node.InnerText.ToLower().Split(',');
                    string[] arrPagesNew=objPreset.Pages.ToLower().Split(',');
                    List<string> arrFinalPages=new List<string>();
                    
                    foreach (string page in arrPagesNew)
                    {
                        if (!arrPages.Contains(page) && page!="all")
                        {
                            arrFinalPages.Add(page);
                        }
                        else
                        {
                            isPageApplied = true;
                        }
                    }
                    objPreset.Pages = string.Join(",", arrFinalPages.ToArray());
                }
                nodecount++;
                
            }
            
            if (!isApplied && !isAllPageApplied && objPreset.Pages!="" && objPreset.Pages!="none")
            {
                XmlElement elem = doc.CreateElement("page");
                elem.SetAttribute("preset",Path.GetFileNameWithoutExtension(objPreset.PresetName));
                elem.InnerText = objPreset.Pages;
                xnpreset.AppendChild(elem);
            }
            doc.Save(xmlpath);

            return(isAllPageApplied?1:isPageApplied?2:0);
            ///Return Login
            ///0:Everything is normal
            ///1:A few pages are already applied to presets
            ///2:All Page is already applied


        }
        public static void DeletePresetFromPresetPages(string presetName, string xmlpath)
        {
            XmlDocument doc = XmlHelper.LoadXMLDocument(xmlpath);
            XmlNode xnpreset = doc.SelectSingleNode("pagepresets");
            XmlNodeList xnlist = xnpreset.ChildNodes;
            int nodecount = 0;
            foreach (XmlNode node in xnlist)
            {

                if (node.InnerText.ToLower() == "" || node.InnerText.ToLower() == "none" || node.Attributes["preset"].Value == Path.GetFileNameWithoutExtension(presetName.ToLower()))
                {
                    xnlist[nodecount].ParentNode.RemoveChild(xnlist[nodecount]);
                }
                nodecount++;            
            }

            doc.Save(xmlpath);

        }

      

        public static string LoadActivePresetForPage(string TemplateName,string PageName)
        {
            string presetPath = Utils.GetPresetPath(TemplateName);
            List<PresetInfo> lstActivePresets = new List<PresetInfo>();
            string pagepreset = presetPath + "/" + TemplateConstants.PagePresetFile;
            List<string> lstAllPages = new List<string>();
            if (File.Exists(pagepreset))
            {

                lstActivePresets = PresetHelper.ParsePreset(pagepreset, "pagepresets/page",out lstAllPages);
            }
            else
            {
                lstActivePresets.Add(PresetInfo.GetPresetPages("default", "*"));

            }

            string pagepresetactive = "layout.ascx";
            foreach (PresetInfo preset in lstActivePresets)
            {
                if (preset.IsDefault)
                {
                    string presetPathFull = presetPath + "/" + preset.PresetName + ".xml";
                    PresetInfo presetdetail = LoadPresetDetails(presetPathFull);
                    pagepresetactive = presetdetail.ActiveLayout.ToLower() + ".ascx";
                }
                else
                {
                    string presetPathFull =presetPath+ "/" + preset.PresetName + ".xml";
                    PresetInfo presetdetail = LoadPresetDetails(presetPathFull);
                    foreach (string page in preset.LSTPages)
                    {
                        if (page.ToLower().Equals(PageName.ToLower()))
                        {
                            pagepresetactive = presetdetail.ActiveLayout.ToLower() + ".ascx";
                            break;
                        }
                    }
                }              
               
            }
            
            pagepresetactive = "~/Templates/"+TemplateName+ "/" + pagepresetactive;
           
            return pagepresetactive;

        }

        public static string LoadHandheldControl(string TemplateName)
        {
            string templatePath = Utils.GetTemplatePath(TemplateName);
            string handheldpath = "";
            if (File.Exists(templatePath + "/handheld.ascx"))
            {
                handheldpath = "~/Templates/" + TemplateName+"/handheld.ascx";
            }
            return handheldpath;
        }

        public static PresetInfo LoadActivePagePreset(string TemplateName, string PageName)
        {
            string presetPath = Utils.GetPresetPath(TemplateName);
            List<PresetInfo> lstActivePresets = new List<PresetInfo>();
            string pagepreset = presetPath + "/" + TemplateConstants.PagePresetFile;
            List<string> lstAllPages = new List<string>();
            PresetInfo pagepresetactive = new PresetInfo();
            if (File.Exists(pagepreset))
            {

                lstActivePresets = PresetHelper.ParsePreset(pagepreset, "pagepresets/page", out lstAllPages);
            }
            else
            {
                lstActivePresets.Add(PresetInfo.GetPresetPages("default", "*"));

            }

           
            foreach (PresetInfo preset in lstActivePresets)
            {
                if (preset.IsDefault)
                {
                    string presetPathFull = presetPath + "/" + preset.PresetName + ".xml";
                    PresetInfo presetdetail = LoadPresetDetails(presetPathFull);
                    pagepresetactive = presetdetail;
                }
                else
                {
                    string presetPathFull = presetPath + "/" + preset.PresetName + ".xml";
                    PresetInfo presetdetail = LoadPresetDetails(presetPathFull);
                    foreach (string page in preset.LSTPages)
                    {
                        if (page.ToLower().Equals(PageName.ToLower()))
                        {
                            pagepresetactive = LoadPresetDetails(presetPathFull);
                            break;
                        }
                    }
                }


            }


            return pagepresetactive;

        }

        public static void UpdatePreset(PresetInfo preset, string TemplateName)
        {
            string presetPath = Utils.GetPresetPath(TemplateName);
            try
            {
                WritePreset(presetPath + "/" + preset.PresetName + ".xml", preset);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        
    }
}
