using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace SageFrame.Templating.xmlparser
{
    public class XmlHelper
    {
        public static XmlDocument LoadXMLDocument(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            return doc;
        }

        public static XmlNodeList GetXMLNodes(XmlDocument doc,string selectednode)
        {
            XmlNodeList xnLst=doc.SelectNodes(selectednode);
            return xnLst;
        }
        //public static XmlNodeList GetChildNodes(XmlNode xmlNode)
        //{
        //    XmlNodeList xnLst=xmlNode.SelectNodes();
        //    return xnLst;
        //}
        public static string GetXMLString(string filePath)
        {
           
            StreamReader sr = null;
            string xml = null;
            try
            {
                sr = new StreamReader(filePath);
                xml = sr.ReadToEnd();
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }
            }
            return xml;
        }

       
        

    }
}
