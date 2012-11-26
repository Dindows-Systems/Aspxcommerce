<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponPerSalesManage.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxCouponManagement_CouponItemsManage" %>

<script type="text/javascript">
    //<![CDATA[
    var couponPerSalesDataToExcel = '<%= btnExportDataToExcel.ClientID%>';
    //]]>
</script>

<div id="gdvCouponPerSales_grid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="Coupon Per Sales"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnExportDataToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="btnExportDataToExcel_Click" Text="Export to Excel"/>
                    </p>
                    <p>
                        <%--<button type="button" id="btnExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
                            <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonCouponPerSale_Click" Text="Export to CSV" OnClientClick="couponPerSalesMgmt.ExportCouponPerSaleToCsvData()"/> 
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="cssClassClear">
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="cssClassSearchPanel cssClassFormWrapper">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel">
                                    Coupon Code:</label>
                                <input type="text" id="txtSearchNameCoupon" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="couponPerSalesMgmt.SearchItems()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCouponPerSalesImage" src=""  alt="loading...." title="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvCouponPerSales" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="CouponPerSalesExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvCouponPerSalesHiddenValue" runat="server" />
