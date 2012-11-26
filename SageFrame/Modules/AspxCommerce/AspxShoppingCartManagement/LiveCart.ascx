<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LiveCart.ascx.cs" Inherits="Modules_AspxShoppingCartManagement_LiveCart" %>

<script type="text/javascript">
    //<![CDATA[
    var timeToAbandonCart = '<%= TimeToAbandonCart%>'
    //]]>
</script>

<div id="divShoppingCartItems">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCartItemGridHeading" runat="server" Text="Items In Carts"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnLiveCartExportToExcel" class="cssClassButtonSubmit" runat="server"
                            OnClick="btnLiveCartExportToExcel_Click" Text="Export to Excel" OnClientClick="LiveCart.ExportLiveCartDataToExcel()" />
                    </p>
                    <p>
                      <%--  <button type="button" id="btnLiveCartExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
                            <asp:Button  ID="btnLiveCartExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonLiveCart_Click" Text="Export to CSV" OnClientClick="LiveCart.ExportLiveCartCsvData()"/> 
                    </p>
                    <%--                    <p>
                        <button type="button" id="btnDeleteAllSearchTerm">
                            <span><span>Delete All Selected</span> </span>
                        </button>
                    </p>--%>
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
                                    Item Name:</label>
                                <input type="text" id="txtSearchItemName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Customer Name</label>
                                <input type="text" id="txtCustomerName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Quantity:</label>
                                <input type="text" id="txtQuantity" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnLiveSearch">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxLiveCartImage" src=""  alt="loading...." title="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvShoppingCart" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                 <table id="liveCartExportData" cellspacing="0" cellpadding="0" border="0" width="100%" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="hdnLiveCartValue" runat="server" />
<asp:HiddenField ID="_csvLiveCartHiddenValue" runat="server" />
