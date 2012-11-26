using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace SageFrame.Templating
{
    public class Tools
    {
        public static string CompressCSS(string body)
        {
            body = Regex.Replace(body, "/\\*.+?\\*/", "", RegexOptions.Singleline);
            body = body.Replace("  ", string.Empty);
            body = body.Replace(Environment.NewLine + Environment.NewLine + Environment.NewLine, string.Empty);
            body = body.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
            body = body.Replace(Environment.NewLine, string.Empty);
            body = body.Replace("\\t", string.Empty);
            body = body.Replace(" {", "{");
            body = body.Replace(" :", ":");
            body = body.Replace(": ", ":");
            body = body.Replace(", ", ",");
            body = body.Replace("; ", ";");
            body = body.Replace(";}", "}");
            body = Regex.Replace(body, "/\\*[^\\*]*\\*+([^/\\*]*\\*+)*/", "$1");
            body = Regex.Replace(body, "(?<=[>])\\s{2,}(?=[<])|(?<=[>])\\s{2,}(?=&nbsp;)|(?<=&ndsp;)\\s{2,}(?=[<])", string.Empty);

            return body;
        }


        //public static IList<System.IO.FileInfo> GetFiles(string serverPath, string extention)
        //{
        //    if (!serverPath.StartsWith("~/"))
        //    {
        //        if (serverPath.StartsWith("/"))
        //            serverPath = "~" + serverPath;
        //        else
        //            serverPath = "~/" + serverPath;
        //    }

        //    string path = HttpContext.Current.Server.MapPath(serverPath);

        //    if (!path.EndsWith("/"))
        //        path = path + "/";

        //    if (!Directory.Exists(path))
        //        throw new System.IO.DirectoryNotFoundException();

        //    IList<FileInfo> files = new List<FileInfo>();

        //    string[] fileNames = Directory.GetFiles(path, "*." + extention, System.IO.SearchOption.AllDirectories);
        //    foreach (string name in fileNames)
        //        files.Add(new FileInfo(name));

        //    return files;
        //}

        //public static string CombineCSS()
        //{
        //    string allCSS = string.Empty;

        //    foreach (FileInfo fi in Logic.Files.GetFiles("~/Content/CSS/", "css"))
        //    {
        //        using (StreamReader sr = new StreamReader(fi.FullName))
        //            allCSS += sr.ReadToEnd();
        //    }

        //    allCSS = allCSS.Replace("~/", Global.BaseURL);

        //    allCSS = CompressCSS(allCSS);

        //    return allCSS;
        //}
    }
}
