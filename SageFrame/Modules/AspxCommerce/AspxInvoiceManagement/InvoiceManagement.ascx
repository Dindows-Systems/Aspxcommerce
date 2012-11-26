<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InvoiceManagement.ascx.cs"
    Inherits="Modules_AspxInvoiceManagement_InvoiceManagement" %>

<script type="text/javascript">
    //<![CDATA[
    var lblInvoiceForm = '<%=lblInvoiceForm.ClientID %>';
    var lblInvoiceHeader = '<%=lblInvoiceHeading.ClientID %>';
	//]]>
</script>


<div id="divOrderDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassCommonBox Curve">
            <div class="cssClassHeader">
                <h2>
                    <asp:Label ID="lblInvoiceHeading" runat="server" Text="Invoices"></asp:Label>
                </h2>
                <div class="cssClassHeaderRight">
                    <div class="cssClassButtonWrapper">
                        <p>
                            <asp:Button ID="btnExportToExcel" class="cssClassButtonSubmit" runat="server" OnClick="Button1_Click"
                                Text="Export to Excel" OnClientClick="invoiceMgmt.ExportDivDataToExcel()" />
                        </p>
                        <p>
                            <asp:Button ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit" OnClick="ButtonInvoice_Click"
                                Text="Export to CSV" OnClientClick="invoiceMgmt.ExportInvoiceToCsvData()" />
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
                                        Invoice Number:
                                    </label>
                                    <input type="text" id="txtInvoiceNumber" class="cssClassTextBoxSmall" />
                                </td>
                                <td>
                                    <label class="cssClassLabel">
                                        Bill To Name:
                                    </label>
                                    <input type="text" id="txtbillToName" class="cssClassTextBoxSmall" />
                                </td>
                                <td>
                                    <label class="cssClassLabel">
                                        Status:
                                    </label>
                                    <select id="ddlStatus" class="cssClassDropDown">
                                        <option value="0">--All--</option>
                                    </select>
                                </td>
                                <td>
                                    <div class="cssClassButtonWrapper cssClassPaddingNone">
                                        <p>
                                            <button type="button" onclick="invoiceMgmt.SearchInvoices()">
                                                <span><span>Search</span></span></button>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="loading">
                        <img id="ajaxInvoiceMgmtImageLoad" src="" alt="loading...." />
                    </div>
                    <div class="log">
                    </div>
                    <table id="gdvInvoiceDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                    </table>
                    <table id="invoiceExportDataTbl" cellspacing="0" cellpadding="0" border="0" width="100%" style="display:none">
                    </table>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<%--Invoice Form --%>
<div id="divInvoiceForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblInvoiceForm" runat="server"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnSavePDFForm2" runat="server" Text="Save As Pdf" OnClick="btnSavePDFForm2_Click"
                            OnClientClick="invoiceMgmt.GenerateInvoicePDF()" CssClass="cssClassButtonSubmit" />
                    </p>
                    <p>
                        <button type="button" id="btnPrint">
                            <span><span>Print</span></span></button>
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div id="divPrintInvoiceForm" class="cssClassFormWrapper">
            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                <tr>
                    <td><br />
                       <b><asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No:" CssClass="cssClassLabel"></asp:Label></b><span
                            id="spanInvoiceNo"></span><br />
                        <b><asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date: " CssClass="cssClassLabel"></asp:Label></b><span
                            id="spanInvoiceDate"></span>
                    </td>
                    <td>                           
                    </td>
                </tr>
                <tr>
                    <td>
                        <b><asp:Label ID="lblStoreName" runat="server" Text="Store Name: " CssClass="cssClassLabel"></asp:Label></b><span
                            id="spanStoreName"></span><br />
                        <b><asp:Label ID="lblStoreDescription" runat="server" Text="Store Description: " CssClass="cssClassLabel"></asp:Label></b><span
                            id="spanStoreDescription"></span><br />
                        <b><asp:Label ID="lblCustomerName" runat="server" Text="Customer Name: " CssClass="cssClassLabel"></asp:Label></b><span
                            id="spanCustomerName"></span><br />
                        <b><asp:Label ID="lblCustomeEmail" runat="server" Text="Customer Email: " CssClass="cssClassLabel"></asp:Label></b><span
                            id="spanCustomerEmail"></span>
                    </td>
                    <td>
                        <b><asp:Label ID="lblOrderID" runat="server" Text="Order ID: " CssClass="cssClassLabel"></asp:Label></b><span
                            id="spanOrderID"></span>
                        <br />
                        <b><asp:Label ID="lblOrderDate" runat="server" Text="ORDER DATE: " CssClass="cssClassLabel"></asp:Label></b>
                        <span id="spanOrderDate"></span>
                        <br />
                        <b><asp:Label ID="lblOrderStatus" runat="server" Text="STATUS: " CssClass="cssClassLabel"></asp:Label></b><span
                            id="spanOrderStatus"></span>
                        <br />
                        <b><asp:Label ID="lblPaymentMethod" runat="server" Text="PAYMENT METHOD: " CssClass="cssClassLabel"></asp:Label></b><span
                            id="spanPaymentMethod"></span>
                        <br />
                        <b><asp:Label ID="lblShippingMethod" runat="server" Text="SHIPPING METHOD: " CssClass="cssClassLabel"></asp:Label></b><span
                            id="spanShippingMethod"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="cssClassShipping" id="divShippingAddressInfo">
                        </div>
                    </td>
                    <td class="cssClassTableLeftCol">
                        <div class="cssClassBilling" id="divBillingAddressInfo">
                        </div>
                    </td>
                </tr>
                </table>
         <div class="cssClassCommonBox Curve" id="invoiceOrderedDetailGrid">
         <div class="cssClassHeader">
            <h2>
                Ordered Items:</h2>
        </div>
         <div class="cssClassGridWrapper">
                  <div id="divOrderItemDetails" class="cssClassGridWrapperContent">                 
                </div> 
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
<asp:HiddenField ID="_csvInvoiceHiddenValue" runat="server" />
<asp:HiddenField ID="invoiceHeaderDetails" runat="server" />
<asp:HiddenField ID="hdnIsMultipleShipping" runat="server" />
<asp:HiddenField ID="hdnRemarks" runat="server" />