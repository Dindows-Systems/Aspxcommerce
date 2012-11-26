/*
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
using System.ServiceModel;
using System.ServiceModel.Activation;
namespace AspxCommerce.Core
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WcfSession
    {
        // Add [WebGet] attribute to use HTTP GET
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }
        [OperationContract]
        public void SetSessionVariableCoupon(string key, int value)
        {
            if (System.Web.HttpContext.Current.Session[key] != null)
            {
                value = int.Parse(System.Web.HttpContext.Current.Session[key].ToString()) + 1;
            }
            else
            {
                value = value + 1;
            }

            System.Web.HttpContext.Current.Session[key] = value;
            //  string asdf = System.Web.HttpContext.Current.Session["OrderID"].ToString();
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }
        [OperationContract]
        public void SetSessionVariable(string key ,int value)
        {
            System.Web.HttpContext.Current.Session[key] = value;
            //  string asdf = System.Web.HttpContext.Current.Session["OrderID"].ToString();
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }
        [OperationContract]
        public void SetSessionVariable(string key, string value)
        {
            System.Web.HttpContext.Current.Session[key] = value;
            //  string asdf = System.Web.HttpContext.Current.Session["OrderID"].ToString();
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }
        [OperationContract]
        public void ClearSessionVariable(string key)
        {
            System.Web.HttpContext.Current.Session.Remove(key);
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }
        [OperationContract]
        public void ClearALLSessionVariable()
        {
            System.Web.HttpContext.Current.Session.Clear();
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }
        [OperationContract]
        public int GetSessionVariable(string key)
        {
            string i = System.Web.HttpContext.Current.Session[key].ToString();
            return Convert.ToInt32(i.ToString());
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
