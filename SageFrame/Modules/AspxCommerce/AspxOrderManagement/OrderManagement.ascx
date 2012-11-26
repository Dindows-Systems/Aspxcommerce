<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderManagement.ascx.cs"
    Inherits="Modules_AspxOrderManagement_OrderManagement" %>

<script type="text/javascript">
    //<![CDATA[
    var lblOrderForm1 = "<%= lblOrderForm.ClientID %>";
	//]]>
</script>

<div id="divOrderDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblOrderHeading" runat="server" Text="Orders"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <%--<p>
                        <button type="button" id="btnCreateNewOrder">
                            <span><span>Create New Order</span></span></button>
                    </p>--%>
                    <p>
                        <asp:Button ID="btnExportToExcel" class="cssClassButtonSubmit" runat="server" OnClick="Button1_Click"
                            Text="Export to Excel" OnClientClick="OrderManage.ExportDivDataToExcel()" />
                    </p>
                    <p>
                            <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonOrder_Click" Text="Export to CSV" OnClientClick="OrderManage.ExportOrdersToCsvData()"/> 
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
                                        <button type="button" onclick="OrderManage.SearchOrders()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxOrderMgmtStaticImage" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvOrderDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <table id="orderExportData" cellspacing="0" cellpadding="0" border="0" width="100%" style="display:none">
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
                                Shippig Method
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
                <span class="remarks"></span>
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
<div id="divEditOrderStatus" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblOrderStatusHeading" runat="server" Text="Edit Order Status :"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table id="tblOrderStatusEditForm" cellspacing="0" cellpadding="0" border="0" width="100%"
                class="cssClassPadding">
                <tr>
                    <td>
                        <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name :" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <span id="customerNameEdit"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOrderDate" runat="server" Text="Ordered Date :" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <span id="spanOrderDate"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOrderGrandTotal" runat="server" Text="Order Total :" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <span id="OrderGrandTotal"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status :" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <select id="selectStatus" class="cssClassDropDown" name="" title="Order Status List">
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnSPBack">
                    <span><span>Back</span></span></button>
            </p>
            <p>
                <button type="button" id="btnUpdateOrderStatus" class="cssClassButtonSubmit" value="">
                    <span><span>Update</span></span></button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
</div>
<input type="hidden" id="hdnOrderID" />
<input type="hidden" id="hdnReceiverEmail" />
<input type="hidden" id="hdnInvoice" />
<asp:HiddenField ID="_csvOrderHdnValue" runat="server" />