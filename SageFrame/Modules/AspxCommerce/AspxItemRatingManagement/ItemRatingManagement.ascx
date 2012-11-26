<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemRatingManagement.ascx.cs"
    Inherits="Modules_AspxItemRatingManagement_ItemRatingManagement" %>

<script type="text/javascript">
    //<![CDATA[
    var lblReviewsFromHeading='<%=lblReviewsFromHeading.ClientID %>';

    //]]>
</script>

<div id="divShowItemRatingDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblReviewsGridHeading" runat="server" Text="Comments and Reviews"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" class="" id="btnDeleteSelected">
                            <span><span>Delete All Selected</span> </span>
                        </button>
                    </p>
                    <p>
                        <button type="button" class="" id="btnAddNewReview">
                            <span><span>Add New Review & Rating </span></span>
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
                                <input type="text" id="txtSearchItemNme" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="ItemRatingManage.SearchItemRatings()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxItemRatingMgmtImage" src="" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvReviewsNRatings" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divItemRatingForm" style="display:none">
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
                        <a href="#" id="lnkItemName" class="cssClassLabel"></a>
                        <select id="selectItemList" class="cssClassDropDown required">
                            <option value="0" selected="selected">--Select One--</option>
                        </select>
                    </td>
                </tr>
                <tr id="trUserList">
                    <td>
                        <label class="cssClassLabel">
                            User Name:</label>
                    </td>
                    <td>
                        <select id="selectUserName" class="cssClassDropDown required">
                         <option value="0" selected="selected">--Select One--</option>
                        </select>
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
                            Nick Name:<span class="cssClassRequired">*</span></label>
                    </td>
                    <td>
                        <input type="text" id="txtNickName" name="name" class="cssClassNormalTextBox required"
                            minlength="2" />
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
                            Summary Of Review:<span class="cssClassRequired">*</span></label>
                    </td>
                    <td>
                        <input type="text" id="txtSummaryReview" name="summary" class="cssClassNormalTextBox required" />
                    </td>
                </tr>
                <tr>
                    <td class="cssClassTableLeftCol">
                        <label class="cssClassLabel">
                            Review:<span class="cssClassRequired">*</span></label>
                    </td>
                    <td>
                        <textarea id="txtReview" cols="50" rows="10" name="review" class="cssClassTextArea required"
                            onkeydown="limitMaxText(this);" onkeyup="limitMaxText(this);"></textarea>
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
            <p>
                <button type="button" id="btnReset">
                    <span><span>Reset</span></span></button>
            </p>
            <p>
                <button type="button" id="btnSubmitReview">
                    <span><span>Submit</span></span></button>
            </p>
            <p>
                <button type="button" id="btnDeleteReview">
                    <span><span>Delete</span></span></button>
            </p>
        </div>
    </div>
</div>
<input type="hidden" id="hdnItemReview" />
