﻿/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 20011-2012 by AspxCommerce
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
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxStoreAccessManagement_StoreAccessControl : BaseAdministrationUserControl
{
    public int PortalID, StoreID;
    public string LblStoreAccessValueID;
    public string UserName;
    public string LblAddEditStoreAccessTitleID;
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            InitializeJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("StoreAccessControl", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/JQueryUI/jquery.ui.all.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/PopUp/style.css");
                IncludeJs("StoreAccessControl", "/js/FormValidation/jquery.validate.js", "/js/JQueryUI/jquery-ui-1.8.10.custom.js", "/js/GridView/jquery.grid.js", 
                            "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js",
                            "/js/MessageBox/jquery.easing.1.3.js", "/js/PopUp/custom.js", "/js/MessageBox/alertbox.js",
                            "/Modules/AspxCommerce/AspxStoreAccessManagement/js/StoreAccess.js");

                StoreID = int.Parse(GetStoreID.ToString());
                PortalID = int.Parse(GetPortalID.ToString());
                LblStoreAccessValueID = lblStoreAccessValue.ClientID;
                LblAddEditStoreAccessTitleID = lblAddEditStoreAccessTitle.ClientID;
                UserName = GetUsername;
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
    } 
}
