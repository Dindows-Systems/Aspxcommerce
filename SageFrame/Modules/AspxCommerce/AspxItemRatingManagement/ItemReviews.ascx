<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemReviews.ascx.cs" Inherits="Modules_AspxCommerce_AspxItemRatingManagement_ItemReviews" %>

<script type="text/javascript">
    //<![CDATA[
  var lbIRHeading='<%=lbIRHeading.ClientID %>';
  var lblReviewsFromHeading='<%=lblReviewsFromHeading.ClientID %>';
    //]]>
</script>

<div id="divItemReviews">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblReviewHeading" runat="server" Text="Items Reviews"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnExportToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel" OnClientClick="ItemReviews.ExportDivDataToExcel()" />
                    </p>
                    <p>
                        <%--<button type="button" id="btnExportToCSV">
                            <span><span>Export to CSV</span></span></button>--%>
                            <asp:Button  ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonItemReview_Click" Text="Export to CSV" OnClientClick="ItemReviews.ExportItemReviewToCsvData()"/>
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
                <img id="ajaxItemReviewImage2" src=""  alt="loading...." />                   
                </div>
                <div class="log">
                </div>
                <table id="gdvItemReviews" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                 <table id="ItemReviewsExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="HdnReviews" runat="server" />
<div id="divShowItemReview" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lbIRHeading" runat="server" Text=""></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnExportReviewsToExcel" CssClass="cssClassButtonSubmit" runat="server"
                             Text="Export to Excel" OnClientClick="ItemReviews.ExportItemReviewDataToExcel()" 
                            onclick="Button2_Click" />
                    </p>
                    <p>
                        <%--<button type="button" id="btnExportItemReviews">
                            <span><span>Export to CSV</span></span></button>--%>
                            <asp:Button  ID="btnExportItemReviews" runat="server" class="cssClassButtonSubmit"
                            OnClick="ButtonItemReviewDetail_Click" Text="Export to CSV" OnClientClick="ItemReviews.ExportItemReviewDetailToCsvData()"/> 
                    </p>                
                    <p>
                        <button type="button" id="btnBack">
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
                                <input type="text" id="txtsearchItemName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="ItemReviews.SearchItemRatings()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxItemReviewImage" src=""  alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvShowItemReviewList" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                 <table id="ShowItemReviewExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divItemReviewRatingForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblReviewsFromHeading" runat="server" Text=""></asp:Label>
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
                    <label id="lnkItemName" class="cssClassLabel"></label>                  
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
                        <input type="text" id="txtSummaryReview" class="cssClassNormalTextBox"/>
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel">
                            Review:</label>
                    </td>
                    <td>
                        <textarea id="txtReview" cols="50" rows="10" class="cssClassTextArea "></textarea>
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
<input type="hidden" id="hdnItemID" />
<input type="hidden" id="hdnItemReviewId" />
<asp:HiddenField ID="_csvItemReviewHdn" runat="server" />
<asp:HiddenField ID="_csvItemReviewDetailHdnValue" runat="server" />