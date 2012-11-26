<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AbandonedCart.ascx.cs"
    Inherits="Modules_AspxShoppingCartManagement_AbandonedCart" %>

<script type="text/javascript">
    //<![CDATA[
        var timeToAbandonCart = '<%= TimeToAbandonCart%>'
    //]]>
</script>

<div class="cssClassBodyContentWrapper" id="divShoppingCartItems">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblAttrGridHeading" runat="server" Text="Abandoned Carts"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <%--     <p>
                        <button type="button" id="btnDeleteShoppingCart">
                            <span><span>Delete All Selected</span></span></button>
                    </p>--%>
                    <p>
                        <asp:Button ID="btnAbandonedCartExportToExcel" runat="server" OnClick="btnAbandonedCartExportToExcel_Click"
                            Text="Export to Excel" OnClientClick="AbandonedCart.ExportAbandonedCartDataToExcel()" CssClass="cssClassButtonSubmit" />
                    </p>
                    <p>    
                     <asp:Button  ID="btnAbandonedCartExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonAbandonCart_Click" Text="Export to CSV" OnClientClick="AbandonedCart.ExportAbandonCartCsvData()"/>
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
                                    Customer Name</label>
                                <input type="text" id="txtAbdCustomerName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnAbandonedSearch">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxAbandonAndliveImage" src=""  alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvAbandonedCart" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <table id="AbandonCartExportDataTbl" cellspacing="0" cellpadding="0" border="0" width="100%" style="display:none">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="hdnAbandonedCartValue" runat="server" />
<asp:HiddenField ID="_cssAbandonCartHiddenValue" runat="server" />
