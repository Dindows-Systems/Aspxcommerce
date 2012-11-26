<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreOrdersReport.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxOrderManagement_StoreOrdersReport" %>

<script type="text/javascript">
    //<![CDATA[
    var lblOrderForm = "<%= lblOrderForm.ClientID %>";    
    //]]>
</script>

<div id="divOrderDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblOrderHeading" runat="server" Text="Order Reports"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnExportToExcel" class="cssClassButtonSubmit" runat="server" OnClick="Button1_Click"
                            Text="Export to Excel" OnClientClick="storeOrdersReport.ExportDivDataToExcel()" />
                    </p>
                    <p>
                        <asp:Button ID="Button1" runat="server" class="cssClassButtonSubmit" OnClick="Button2_Click"
                            Text="Export to CSV" OnClientClick="storeOrdersReport.ExportToCsvData()" />
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="cssClassSearchPanel cssClassFormWrapper">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel">
                                    Customer Name:</label>
                                <input type="text" id="txtCustomerName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Order Status:</label>
                                <select id="ddlOrderStatus" class="cssClassDropDown">
                                    <option value="0">--All--</option>
                                </select>
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnSearchOrderReport">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxStoreOrderReportImage" src="" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvOrderDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
               <table id="orderReportExportData" cellspacing="0" cellpadding="0" border="0" width="100%" style="display:none">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<div class="cssClassFormWrapper">
<div class="cssClassCommonBox Curve" id="divOrderDetailForm" style="display:none">
    <div class="cssClassHeader">
        <h2>
            <asp:Label ID="lblOrderForm" runat="server"></asp:Label>
        </h2>
    </div>
    <div id="divOrderDetailHead">
        <div class="cssClassStoreDetail">
        <b><span class="cssClassLabel">Ordered Date: </span></b><span id="OrderDate"></span>
                <br />
            <b><span class="cssClassLabel">Store Name: </span></b><span id="storeName"></span><br />
            <b><span class="cssClassLabel">Store Description: </span></b><span id="storeDescription"></span><br />
             <div class="cssPaymentDetail">                
                <b><span class="cssClassLabel">Payment Gateway Type: </span></b><span id="PaymentGatewayType">
                </span>
                <br />
                <b><span class="cssClassLabel">Payment Method: </span></b><span id="PaymentMethod">
                </span>
            </div>
        </div>
       
            <div class="cssClassBillingAddress cssClassStorePayment">
                <ul class="cssClassLabel">
                </ul>
            </div>        
        <br />
    </div>
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                Ordered Items:</h2>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <thead>
                        <tr class="cssClassHeading">
                            <td >
                                Item Name
                            </td>
                            <td >
                                SKU
                            </td>
                            <td >
                                Shipping Address
                            </td>
                            <td >
                                Shipping Rate
                            </td>
                            <td >
                                Price
                            </td>
                            <td >
                                Quantity
                            </td>
                            <td >
                                SubTotal
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="cssClassButtonWrapper">
        <p>
            <button type="button" id="btnBack">
                <span><span>Back</span></span></button>
        </p>
    </div>
</div>
</div>
<asp:HiddenField ID="_csvHiddenValue" runat="server" />