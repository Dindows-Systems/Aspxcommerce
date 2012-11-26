<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewAccountReport.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxNewAccountReport_NewAccountReport" %>

<script type="text/javascript">
    //<![CDATA[
    var btnExportToExcelNAR='<%=btnExportToExcelNAR.ClientID %>';
//]]>
</script>

<div align="right">
    <label>
        <b>Show Reports:</b></label>
    <select id="ddlNewAccountReport">
        <option value="1">Show Year Monthly Report</option>
        <option value="2">Show Current Month Weekly Report</option>
        <option value="3">Show Today's Report</option>
    </select></div>
<div id="divNewAccountDetailsByMonthly">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblReviewsGridHeading" runat="server" Text="New Accounts"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnExportToExcelNAR" class="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel" />
                    </p>
                    <p>
                       <%-- <button type="button" id="btnExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
                            <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonNewAccount_Click" Text="Export to CSV" OnClientClick="NewAccountReport.ExportNewAccountToCsvData()"/>
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
                <div class="loading">
                    <img id="ajaxNewAccountReportImageLoad" src="" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvNewAccountList" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="NewAccountExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvNewAccountHiddenValue" runat="server" />
