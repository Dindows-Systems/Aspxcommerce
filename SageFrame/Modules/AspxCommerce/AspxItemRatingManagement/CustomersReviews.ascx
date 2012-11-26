<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomersReviews.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxItemRatingManagement_CustomersReviews" %>

<script type="text/javascript">
    //<![CDATA[
        var lblCRHeading='<%=lblCRHeading.ClientID %>';
        var lblReviewsFromHeading='<%=lblReviewsFromHeading.ClientID %>';
    //]]>
</script>

<div id="divCustomerReviews">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblReviewHeading" runat="server" Text="Customers Reviews"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnExportToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel" OnClientClick="CustomerReviews.ExportDivDataToExcel()" />
                    </p>
                    <p>
                            <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonCustomerReview_Click" Text="Export to CSV" OnClientClick="CustomerReviews.ExportCustomerReviewToCsvData()"/>
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
                    <img id="ajaxCustomerItemReviewImage2" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvCustomerReviews" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                 <table id="CustomerReviewExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<div id="divShowCustomerReviewList" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCRHeading" runat="server" Text=""></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">                    
                    <p>
                        <asp:Button ID="btnExportReviewsToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button2_Click" Text="Export to Excel" OnClientClick="CustomerReviews.ExportDataToExcel()" />
                    </p>
                    <p>                    
                            <asp:Button  ID="btnExportReviews" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonCustomerReviewDetail_Click" Text="Export to CSV" OnClientClick="CustomerReviews.ExportCustomerReviewDetailToCsvData()"/>
                    </p>
                    <p>
                        <button type="button" id="btnBackCustomerReviews">
                            <span><span>Back</span></span></button>
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
                                    Nick Name:</label>
                                <input type="text" id="txtSearchUserName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Status:</label>
                                <select id="ddlStatus" class="cssClassDropDown">
                                    <option value="">--All--</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Item Name:</label>
                                <input type="text" id="txtsearchItemNm" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="CustomerReviews.SearchItemRatings()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCustomerItemReviewImage1" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvShowCustomerReviewList" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                 <table id="ShowCustomerReviewListExportTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnReviews" runat="server" />
<div id="divCustomerItemRatingForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblReviewsFromHeading" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table border="0" width="100%" id="tblEditReviewForm" class="cssClassPadding">
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel">
                            Item:</label>
                    </td>
                    <td>                     
                        <label id="lnkItemNames" class="cssClassLabel"></label>
                      </td>
                </tr>
                <tr id="trPostedBy">
                    <td>
                        <label class="cssClassLabel">
                            Posted By:</label>
                    </td>
                    <td>
                        <label id="lblPostedBy" class="cssClassLabel">
                        </label>
                    </td>
                </tr>          
                <tr id="trViewedIP">
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel">
                            View From IP:</label>
                    </td>
                    <td>
                        <label id="lblViewFromIP" class="cssClassLabel">
                        </label>
                    </td>
                </tr>
                <tr id="trSummaryRating">
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel">
                            Summary Rating:</label>
                    </td>
                    <td>
                        <div id="divAverageRating">
                        </div>
                        <span class="cssClassRatingTitle"></span>
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel">
                            Detailed Rating:</label>
                    </td>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tblRatingCriteria">
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel">
                            Nick Name:</label>
                    </td>
                    <td>
                        <input type="text" id="txtNickName" class="cssClassNormalTextBox "/>
                    </td>
                </tr>
                <tr id="trAddedOn">
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel">
                            Added On:</label>
                    </td>
                    <td>
                        <label id="lblAddedOn">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel">
                            Summary Of Review:</label>
                    </td>
                    <td>
                        <input type="text" id="txtSummaryReview" name="summary" class="cssClassNormalTextBox "/>
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel">
                            Review:</label>
                    </td>
                    <td>
                        <textarea id="txtReview" cols="50" rows="10" class="cssClassTextArea"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel">
                            Status:</label>
                    </td>
                    <td>
                        <select id="selectStatus" class="cssClassDropDown">
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnReviewBack">
                    <span><span>Back</span></span></button>
            </p>           
        </div>
    </div>
</div>
<input type="hidden" id="hdnUser" />
<input type="hidden" id="hdnItemReviewID" />
<asp:HiddenField ID="_csvCustomerReviewHiddenValue" runat="server" />
<asp:HiddenField ID="_csvCustomerReviewDetailValue" runat="server" />