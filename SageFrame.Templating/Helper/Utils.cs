using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SageFrame.Templating.xmlparser;
using System.Web;
using System.Text.RegularExpressions;

namespace SageFrame.Templating
{
    public class Utils
    {
        public static bool ValidateThumbImage(FileInfo file)
        {
            bool isValid = false;
            if (file.Extension == ".png" || file.Extension == ".jpg" || file.Extension == ".gif")
            {
                isValid = true;
            }
            return isValid;
        }

        public static bool IsValidTag(XmlTag tag)
        {
            return(Enum.IsDefined(typeof(XmlTagTypes),tag.TagName.ToUpper())); 
        }


        public static string GetTemplateInfoFilePath(string TemplateName)
        {
            return (GetAbsolutePath(TemplateConstants.TemplateDirectory + TemplateName + TemplateConstants.TemplateInfo));
        }

        public static string GetTemplatePath(string TemplateName)
        {
            return (GetAbsolutePath(TemplateConstants.TemplateDirectory + TemplateName));
        }

        public static string GetThemePath(string TemplateName)
        {
            return (GetAbsolutePath(TemplateConstants.TemplateDirectory + TemplateName + TemplateConstants.ThemeDirectory));
        }
        public static string GetAdminTemplatePath()
        {
            return (GetAbsolutePath("Administrator/Templates/"));
        }
        public static string GetPresetPath(string TemplateName)
        {
            return (GetAbsolutePath(TemplateConstants.TemplateDirectory + TemplateName + TemplateConstants.PresetDirectory));
        }

        public static string ReplaceBackSlash(string filepath)
        {
            if (filepath != null)
            {
                filepath = filepath.Replace("\\", "/");
            }
            return filepath;
        }

        public static string GetAbsolutePath(string filepath)
        {
            return (Utils.ReplaceBackSlash(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath.ToString(), filepath)));
        }

        public static bool CompareStrings(object string1, object string2)
        {
            return (string1.ToString().ToLower().Equals(string2.ToString().ToLower()));
        }
        public static bool CompareStrings(string string1, string string2)
        {
            return (string1.ToString().ToLower().Equals(string2.ToString().ToLower()));
        }
        public static bool CompareStrings(string string1, object string2)
        {
            return (string1.ToString().ToLower().Equals(string2.ToString().ToLower()));
        }
        public static bool CompareStrings(object string1, string string2)
        {
            return (string1.ToString().ToLower().Equals(string2.ToString().ToLower()));
        }

        public static string ExtractNumbers(string expr)
        {
            return string.Join(null, System.Text.RegularExpressions.Regex.Split(expr, "[^\\d]"));
        }

        public static void DeleteDirectory(string target_dir)
        {
            if (Directory.Exists(target_dir))
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string dir in dirs)
                {
                    DeleteDirectory(dir);
                }

                Directory.Delete(target_dir, false);
            }
        }

        public static void DeleteFile(string target_file)
        {
            if (File.Exists(target_file))
            {
                File.Delete(target_file);
            }
        }

        public static string GetFileNameWithExtension(string filename, string ext)
        {
            
            if (Path.HasExtension(filename))
            {
                filename = Path.GetFileNameWithoutExtension(filename);
                filename = string.Format("{0}{1}{2}", filename, ".", ext);
            }
            else
            {
                filename = string.Format("{0}{1}{2}", filename, ".", ext);
            }
            return filename;
        }

        public static bool ContainsXmlHeader(string xml)
        {
               // string pattern = "\\s*<\\?xml\\s*version\\s*=\\s*\"[^\"]*\"\\s*encoding\\s*=\\s*\"\\s*utf-8\\s*\"\\s*\\?>";

               // string text = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
               // Match m = Regex.Match(text, pattern);
                return(xml.Contains("<?xml"));
                //return(m.Success?true:false);
        }

        public static string GetAttributeValueByName(XmlTag tag, XmlAttributeTypes _type)
        {
            string value = string.Empty;
            string name = _type.ToString();
            LayoutAttribute attr = new LayoutAttribute();
            attr = tag.LSTAttributes.Find(
                delegate(LayoutAttribute attObj)
                {
                    return (Utils.CompareStrings(attObj.Name, name));
                }
                );
            return attr == null ? "" : attr.Value;
        }
        public static string GetAttributeValueByName(XmlTag tag, XmlAttributeTypes _type, string defaultValue)
        {
            string value = string.Empty;
            string name = _type.ToString();
            LayoutAttribute attr = new LayoutAttribute();
            attr = tag.LSTAttributes.Find(
                delegate(LayoutAttribute attObj)
                {
                    return (Utils.CompareStrings(attObj.Name, name));
                }
                );
            return attr == null ? defaultValue : attr.Value;
        }

        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
