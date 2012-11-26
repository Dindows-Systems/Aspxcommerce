<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerByOrderTotal.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxCustomerManagement_CustomerByOrderTotal" %>

<script type="text/javascript">
    //<![CDATA[
     var btnExportToExcelCTO='<%=btnExportToExcelCTO.ClientID %>';

    //]]>
</script>

<div id="divCustomerOrderTotal">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblReviewHeading" runat="server" Text="Customers by Total Orders"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnExportToExcelCTO" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel"  />
                    </p>
                    <p>
                        <%--<button type="button" id="btnExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
                            <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonCustomerByOrder_Click" Text="Export to CSV" OnClientClick="CustomerTotalOrders.ExportCustomerByOrderToCsvData()"/> 
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
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
                                <input type="text" id="txtSearchUserNm" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnSearchCustomerTotalOrders">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCustomerByOrderTotal" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvCustomerOrderTotal" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="CustomerTotalOrderExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvCustomerByOrderHiddenValue" runat="server" />
