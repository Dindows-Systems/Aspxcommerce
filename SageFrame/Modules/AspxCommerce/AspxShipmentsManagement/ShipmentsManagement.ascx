<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShipmentsManagement.ascx.cs"
    Inherits="Modules_AspxShipmentsManagement_ShipmentsManagement" %>

<script type="text/javascript">
    //<![CDATA[
    var btnExportToExcel='<%=btnExportToExcel.ClientID %>';
    //]]>
</script>

<div id="divShipmentsDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblOrderHeading" runat="server" Text="Shipments"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                    <asp:Button ID="btnExportToExcel" class="cssClassButtonSubmit" runat="server" OnClick="Button1_Click"
                            Text="Export to Excel" OnClientClick="Shipments.ExportShipmentDataToExcel()" />
                    </p>
                    <p>
                     <asp:Button ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit" OnClick="Button2_Click"
                            Text="Export to CSV" OnClientClick="Shipments.ExportShipmentDataToCsv()" />                       
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
                                    Shipping Method Name:</label>
                                <input type="text" id="txtShippingMethodName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    OrderID:</label>
                                <input type="text" id="txtOrderID" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Ship To Name:</label>
                                <input type="text" id="txtSearchShipToName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnShipmentsSearch"">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxShipmentsMgmtImage" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvShipmentsDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <table id="shpipmentExportData" cellspacing="0" cellpadding="0" border="0" width="100%" style="display:none">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<div class="cssClassFormWrapper">
<div class="cssClassCommonBox  Curve" id="divShipmentsDetailForm" style="display:none">
    <div class="cssClassHeader">
        <h2>
            <asp:Label ID="lblShipmentForm" runat="server"></asp:Label>
        </h2>
    </div>
    <span class="cssClassLabel"><b>Shipment Date:</b> </span><span id="shipmentDate"></span>
    <br />
<%--    <span class="cssClassLabel">Shipping Method Name: </span><span id="shippingMethodName">
    </span>--%>
    <br />
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                Shipments Items:</h2>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <thead>
                        <tr class="cssClassHeading">
                            <td>
                                Item Name
                            </td>
                            <td>
                                SKU
                            </td>
                            <td>
                                ShippingAddress
                            </td>
                            <td>Shipping Method Name</td>
                           <td>
                                Shipping Rate
                            </td>
                            <td>
                                Price
                            </td>
                            <td>
                                Quantity
                            </td>
                            <td>
                                Sub Total
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <spam class="remarks"></spam>
            </div>
        </div>
        <%-- </div>--%></div>
    <div class="cssClassButtonWrapper">
        <p>
            <button type="button" id="btnShipmentBack">
                <span><span>Back</span></span></button>
        </p>
    </div>
</div>
</div>
<asp:HiddenField ID="_csvShipmentHiddenValue" runat="server" />