<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingReport.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxShippingReport_ShippingReport" %>

<script type="text/javascript">
    //<![CDATA[
   

    //]]>
</script>
<div align="right">
<label><b>Show Shipping Reports:</b></label>
<select id="ddlShippingReport">
<option value="1">Show Year Report</option>
<option value="2">Show Current Month Report</option>
<option value="3">Show Today's Report</option>
</select></div>
<div id="divShippiedReport">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblOrderHeading" runat="server" Text="Shipping Reports"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnExportToExcel" class="cssClassButtonSubmit" runat="server" OnClick="Button1_Click"
                            Text="Export to Excel" OnClientClick="ShippingReport.ExportDivDataToExcel()" />
                    </p>
                    <p>
                       <%-- <button type="button" id="btnExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
<asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonShippingCsv_Click" Text="Export to CSV" OnClientClick="ShippingReport.ExportShippingToCsvData()"/> 
                    
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
                                <input type="text" id="txtShippingMethodNm" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="ShippingReport.SearchShippingReport()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxShippingReportImage" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvShippedReportDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                 <table id="ShippingDataExportTbl" cellspacing="0" cellpadding="0" border="0" width="100%" style="display:none">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvShippingHiddenValue" runat="server" />