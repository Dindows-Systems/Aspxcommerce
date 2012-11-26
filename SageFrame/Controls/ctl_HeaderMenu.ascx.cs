/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2010 by SageFrame
Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Framework;

namespace SageFrame.Web
{
    public partial class Modules_ctl_HeaderMenu : BaseUserControl //System.Web.UI.UserControl
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["SiteMapProvider"] != null && Session["SiteMapProvider"].ToString() != "")
            //{
            //    smdsSageFrame.SiteMapProvider = Session["SiteMapProvider"].ToString();
            //    smdsSageFrame.DataBind();
            //}
        }

        //protected void mnuSageFrame_MenuItemDataBound(object sender, MenuEventArgs e)
        //{
        //    if (e.Item != null)
        //    {
        //        SageFrameConfig sfConfig = new SageFrameConfig();
        //        string IsPortalMenuIsImage = sfConfig.GetSettingsByKey(SageFrameSettingKeys.IsPortalMenuIsImage);
        //        if (IsPortalMenuIsImage == "1")
        //        {
        //            e.Item.ImageUrl = Page.ResolveUrl(GetMenuItemImageUrl(e.Item.Text));//"~/PageImages/mediumthumbs/1_519201033700PM.gif");
        //            e.Item.Text = string.Empty;
        //        }
        //        if (Request.RawUrl.Contains(e.Item.NavigateUrl.Replace("~", "")))
        //        {                   
        //            e.Item.Selected = true;
        //        }
        //    }
        //}

        //private string GetMenuItemImageUrl(string menuItmeName)
        //{
        //    SageFrameConfig sfConfig = new SageFrameConfig();
        //    string TemplateName = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalCssTemplate);
        //    string PortalMenuImageExtension = sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalMenuImageExtension);
        //    string imageUrl = "~/Templates/" + TemplateName + "/images/" + menuItmeName + "_" + GetCurrentCultureName;
        //    if (PortalMenuImageExtension.Contains("."))
        //    {
        //        imageUrl += PortalMenuImageExtension;
        //    }
        //    else
        //    {
        //        imageUrl += "." + PortalMenuImageExtension;
        //    }
        //    return imageUrl;
        //}
    }
}
