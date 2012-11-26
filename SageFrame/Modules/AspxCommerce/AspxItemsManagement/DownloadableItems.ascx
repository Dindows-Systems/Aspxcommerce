<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DownloadableItems.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxItemsManagement_DownloadableItems" %>

<script type="text/javascript">
    //<![CDATA[
    var btnExportToExcel='<%=btnExportToExcel.ClientID %>';
    var rootPath='<%=AspxfilePath %>';
    //]]>
</script>

<div id="gdvDownLoadableItems_grid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="Downloadable"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnExportToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel" />
                    </p>
                    <p>
                       <%-- <button type="button" id="btnExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
                            <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonDownLoaded_Click" Text="Export to CSV" OnClientClick="DownloadedItems.ExportDownloadedToCsvData()"/>
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
                                    Item Name:</label>
                                <input type="text" id="txtSearchName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    SKU:</label>
                                <input type="text" id="txtSearchSKU" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnSearchDownloadedItems">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxDownloadableItemImage" src=""  alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvDownLoadableItems" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="DownLoadableExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvDownloadedHiddenCsv" runat="server" />
