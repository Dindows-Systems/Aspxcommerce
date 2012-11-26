<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaxRulesManage.ascx.cs"
    Inherits="Modules_AspxTaxManagement_TaxRulesManage" %>

<script type="text/javascript">
 //<![CDATA[
    var lblTaxRuleHeading='<%=lblTaxRuleHeading.ClientID %>';
    //]]>
</script>

<div id="divTaxManageRulesGrid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="Manage Tax Rules"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnDeleteSelected">
                            <span><span>Delete All Selected</span> </span>
                        </button>
                    </p>
                    <p>
                        <button type="button" id="btnAddNewTaxRule">
                            <span><span>Add New Tax Rule</span> </span>
                        </button>
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
                                    Tax Rule Name:</label>
                                <input type="text" id="txtRuleName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Customer Tax Class Name:</label>
                                <select id="ddlCustomerClassName" class="cssClassDropDown">
                                    <option value="0">--All--</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Item Tax Class Name:</label>
                                <select id="ddlItemClassName" class="cssClassDropDown">
                                    <option value="0">--All--</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Tax Rate Name:</label>
                                <select id="ddlTaxRateTitle" class="cssClassDropDown">
                                    <option value="0">--All--</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Priority:</label>
                                <input type="text" id="txtSearchPriority" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Display Order:</label>
                                <input type="text" id="txtSearchDisplayOrder" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="TaxRules.SearchTaxManageRules()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxTaxRuleMgmtImage" src=""  alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvTaxRulesDetails" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divTaxRuleInformation" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTaxRuleHeading" runat="server" Text="Tax Rule Information:"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding tdpadding">
                <tr>
                    <td>
                        <asp:Label ID="lblTaxManageRuleName" runat="server" Text="Tax Manage Rule Name:"
                            CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtTaxManageRuleName" name="ruleName" class="cssClassNormalTextBox" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCustomerTaxClass" runat="server" Text="Customer Tax Class:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <select id="ddlCustomerTaxClass" class="cssClassDropDown required">
                        <option value="0">--Select One--</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblItemTaxClass" runat="server" Text="Item Tax Class:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <select id="ddlItemTaxClass" class="cssClassDropDown required">
                        <option value="0">--Select One--</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTaxRate" runat="server" Text="Tax Rate:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <select id="ddlTaxRate" class="cssClassDropDown required">
                        <option value="0">--Select One--</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPriority" runat="server" Text="Priority:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtPriority" name="priority" class="cssClassNormalTextBox priority" />
                        <span id="errmsgPriority"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDisplayOrder" runat="server" Text="Display Order:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtDisplayOrder" name="displayOrder" class="cssClassNormalTextBox displayOrder" />
                        <span id="errmsgDisplayOrder"></span><span id="errDisplayOrder"></span>
                    </td>
                </tr>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnCancel">
                    <span><span>Cancel</span> </span>
                </button>
            </p>
            <p>
                <button type="button" id="btnSaveTaxRule">
                    <span><span>Save</span> </span>
                </button>
            </p>
        </div>
    </div>
</div>
<input type="hidden" id="hdnTaxManageRuleID" />
