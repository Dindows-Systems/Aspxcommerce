<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaxRate.ascx.cs" Inherits="Modules_AspxTaxManagement_TaxRate" %>

<script type="text/javascript">
 //<![CDATA[   
    var lblTaxRateHeading='<%=lblTaxRateHeading.ClientID %>';
    
    //]]>
</script>

<div id="divTaxRatesGrid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="Manage Tax Rates"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnDeleteSelected">
                            <span><span>Delete All Selected</span> </span>
                        </button>
                    </p>
                    <p>
                        <button type="button" id="btnAddNewTaxRate">
                            <span><span>Add New Tax Rate</span> </span>
                        </button>
                    </p>
                    <p>
                        <asp:Button ID="btnExportToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel" OnClientClick="TaxRate.ExportDivDataToExcel()" />
                    </p>
                    <p>
                       <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonTaxRate_Click" Text="Export to CSV" OnClientClick="TaxRate.ExportTaxRateToCsvData()"/> 
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
                                    Tax Rate Title:</label>
                                <input type="text" id="txtRateTitle" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Country:</label>
                                <select id="ddlSearchCountry" class="cssClassDropDown">
                                    <option value="0">--Select Country--</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    State/Province:</label>
                                <input type="text" id="txtSearchState" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Zip/Post Code:</label>
                                <input type="text" id="txtSearchZip" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="TaxRate.SearchTaxRate()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxTaxRateImage" src=""  alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvTaxRateDetails" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="TaxRateDetailsExportTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<div id="divTaxRateInformation" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTaxRateHeading" runat="server" Text="Tax Rate Information"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding tdpadding">
                <tr>
                    <td>
                        <asp:Label ID="lblTaxRateTitle" runat="server" Text="Tax Rate Title:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtTaxRateTitle" name="rateTitle" minlength="2" class="cssClassNormalTextBox required" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCountry" runat="server" Text="Country:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <select id="ddlCountry" class="cssClassDropDown required">
                            <option value="0">--Select Country--</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblState" runat="server" Text="State/Province:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <select id="ddlState" class="cssClassDropDown">
                            <option value="0">--Select State--</option>
                        </select>
                        <input type="text" id="txtState" name="state" minlength="2" class="cssClassNormalTextBox required" />
                    </td>
                </tr>
                <tr id="trZipPostCode">
                    <td>
                        <asp:Label ID="lblZipPostCode" runat="server" Text="Zip/Post Code:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtZipPostCode" name="zipCode" minlength="5" class="cssClassNormalTextBox required zipPostcode" />
                        <span id="errmsgZipPostCode"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIsZipPostRange" runat="server" Text="Is Zip/Post Range:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="checkbox" id="chkIsTaxZipRange" class="cssClassCheckBox" />
                    </td>
                </tr>
                <tr id="trRangeFrom">
                    <td>
                        <asp:Label ID="lblRangeFrom" runat="server" Text="Range From:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtRangeFrom" name="rangefrom" minlength="5" class="cssClassNormalTextBox required rangeFrom" />
                        <span id="errmsgRangeFrom"></span>
                    </td>
                </tr>
                <tr id="trRangeTo">
                    <td>
                        <asp:Label ID="lblRangeTo" runat="server" Text="Range To:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtRangeTo" name="rangeTo" minlength="5" class="cssClassNormalTextBox required rangeTo" />
                        <span id="errmsgRangeTo"></span>
                    </td>
                </tr>
                  <tr>
                    <td>
                        <asp:Label ID="lblRateType" runat="server" Text="Rate Type:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <select id="ddlTaxRateType" class="cssClassDropDown">
                            <option value="False">Absolute ($)</option>
                            <option value="True">Percent (%)</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTaxRateValue" runat="server" Text="Tax Rate:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtTaxRateValue" name="taxRate" class="cssClassNormalTextBox required" />
                        <span id="errmsgTaxRateValue"></span>
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
                <button type="button" id="btnSaveTaxRate">
                    <span><span>Save</span> </span>
                </button>
            </p>
        </div>
    </div>
</div>
<input type="hidden" id="hdnTaxRateID" />
<asp:HiddenField ID="_csvTaxRateHdnValue" runat="server" />
