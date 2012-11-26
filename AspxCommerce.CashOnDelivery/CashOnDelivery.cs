using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class CashOnDelivery
    {
        public static string Parse(string transId, string invoice, string POrderno, int responseCode, int responsereasonCode, string responsetext, int storeID, int portalID, string userName, int customerID, string sessionCode)
        {
            try
            {
                OrderDetailsCollection ot = new OrderDetailsCollection();
                OrderDetailsInfo odinfo = new OrderDetailsInfo();
                CartManageSQLProvider cms = new CartManageSQLProvider();
                CommonInfo cf = new CommonInfo();
                cf.StoreID = storeID;
                cf.PortalID = portalID;
                cf.AddedBy = userName;
                // UpdateOrderDetails
                AspxOrderDetails objad = new AspxOrderDetails();
                SQLHandler sqlH = new SQLHandler();
                WcfSession ws = new WcfSession();
                odinfo.OrderID = ws.GetSessionVariable("OrderID");
                odinfo.TransactionID = odinfo.ResponseCode.ToString(transId);
                odinfo.InvoiceNumber = Convert.ToString(invoice);
                odinfo.PurchaseOrderNumber = Convert.ToString(POrderno);
                odinfo.ResponseCode = Convert.ToInt32(responseCode);
                odinfo.ResponseReasonCode = Convert.ToInt32(responsereasonCode);
                odinfo.ResponseReasonText = Convert.ToString(responsetext);
                ot.ObjOrderDetails = odinfo;
                ot.ObjCommonInfo = cf;
                odinfo.OrderStatusID = 8;
                objad.UpdateOrderDetails(ot);
                if (HttpContext.Current.Session["OrderCollection"] != null)
                {
                    OrderDetailsCollection orderdata2 = new OrderDetailsCollection();
                    orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                    objad.UpdateItemQuantity(orderdata2);
                }
                ws.ClearSessionVariable("OrderID");
                cms.ClearCartAfterPayment(customerID, sessionCode, storeID, portalID);
                return "This transaction has been approved";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
