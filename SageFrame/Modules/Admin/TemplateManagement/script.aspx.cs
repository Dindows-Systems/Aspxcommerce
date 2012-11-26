using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.IO;
using SageFrame.Core.TemplateManagement;
using SageFrame.Common;
using SageFrame.Web;

public partial class Modules_extjquery_script : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dir = Request.Params["dir"].ToString();
        StringBuilder sb = new StringBuilder();

        if (dir != null)
        {

            string templatePath = Path.Combine(Request.PhysicalApplicationPath.ToString(), "Templates");
            templatePath = Path.Combine(templatePath, dir + "\\screenshots\\_Thumbs");
            DirectoryInfo dirInfo = new DirectoryInfo(templatePath);
            if (Directory.Exists(templatePath))
            {
                string filePath =
                    CommonFunction.ReplaceBackSlash("\\Templates\\" + dir + "\\screenshots\\_Thumbs\\");
                string appPath = Request.ApplicationPath != "/" ? Request.ApplicationPath : "";
                string imagePath = appPath+CommonFunction.ReplaceBackSlash("\\Templates\\" + dir + "\\screenshots\\");
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    sb.Append("<a href='#' path='" + imagePath + file.Name + "'><img src='"+appPath+ filePath + file.Name +
                              "' height='70px' width='100px'/></a>");
                }
            }
        }

        Response.Expires = -1;
        Response.ContentType = "text/plain";
        Response.Write(sb.ToString());
        Response.End();
    }
}
