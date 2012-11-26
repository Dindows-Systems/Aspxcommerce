<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerTags.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxTagsManagement_CustomerTags" %>

<script type="text/javascript">
    //<![CDATA[
    var lblShowHeading='<%=lblShowHeading.ClientID %>';
    //]]>
</script>

<div id="divCustomerTagDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTagHeading" runat="server" Text="Customers Tags"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnExportDataToExcel" runat="server" OnClick="Button1_Click" Text="Export to Excel"
                             CssClass="cssClassButtonSubmit" OnClientClick="CustomerTags.ExportCustomerTagDataToExcel()"/>
                    </p>
                    <p>
                            <asp:Button  ID="btnExport" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonCustomerTags_Click" Text="Export to CSV" OnClientClick="CustomerTags.ExportCustomertagToCsvData()"/> 
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <%--<div class="cssClassSearchPanel cssClassFormWrapper">
          <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td><label class="cssClassLabel"> Cost Variant Name:</label>
                <input type="text" id="txtVariantName" class="cssClassTextBoxSmall" /></td>
              <td><div class="cssClassButtonWrapper cssClassPaddingNone">
                  <p>
                    <button type="button" onclick="SearchCostVariantName()"> <span><span>Search</span></span></button>
                  </p>
                </div></td>
            </tr>
          </table>
        </div>--%>
                <div class="loading">
                    <img id="ajaxCustomerTagsImage2" src=""  alt="loading...." title="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvCusomerTag" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                 <table id="CustomerTagExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divShowCustomerTagList" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblShowHeading" runat="server" Text=""></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnExportToExcel" runat="server" OnClick="Button2_Click" Text="Export to Excel"
           CssClass="cssClassButtonSubmit"  OnClientClick="CustomerTags.ExportCustomerTagListDataToExcel()"/>
                    </p>
                    <p>
                            <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonCustomerTagsDetail_Click" Text="Export to CSV" OnClientClick="CustomerTags.ExportCustomertagDetailToCsvData()"/> 
                    </p>
                    <p>
                        <button type="button" id="btnBack">
                            <span><span>Back</span></span>
                        </button>
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="loading">
                    <img id="ajaxCustomerImageLoad" src=""  alt="loading...." title="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="grdShowTagsList" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="ShowTagListExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="HdnGridData" runat="server" />
<asp:HiddenField ID="_csvCustomerTagHdn" runat="server" />
<asp:HiddenField ID="_csvCustomerTagDetailHdn" runat="server" />
