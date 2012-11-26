<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecentReviewsAndRatings.ascx.cs"
    Inherits="Modules_AspxItemRatingManagement_RecentReviewsAndRatings" %>

<script type="text/javascript">
    //<![CDATA[

    //]]>
</script>

<div id="tblLatestReviews">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblFrontReviewsHeading" runat="server" Text="Latest comments & reviews"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <div class="cssClassRateReview">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tblRatingPerUser">
                </table>
            </div>
            <div class="cssClassPagination" id="divSearchPageNumber">
               <div class="cssClassPageNumber">
                    <div id="Pagination">
                    </div>
                </div>
                <div class="cssClassViewPerPage">
                    View Per Page
                    <select id="ddlPageSize">
                        <option value="5">5</option>
                        <option value="10">10</option>
                        <option value="15">15</option>
                        <option value="20">20</option>
                        <option value="25">25</option>
                        <option value="40">40</option>
                    </select>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="popupbox" id="popuprel5">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span>Close</span></button>
    </div>
    <h2>
        <asp:Label ID="Label1" runat="server" Text="Edit this item Rating & Review"></asp:Label>
    </h2>
    <%--<div id="tblEditReviews">
  <div class="cssClassCommonBox Curve">
    <div class="cssClassHeader">
      <h2>
        <asp:Label ID="lblReviewFromHeading" runat="server" Text="Edit this item Rating & Review"></asp:Label>
      </h2>
    </div>--%>
    <div class="cssClassFormWrapper">
        <table cellspacing="0" cellpadding="0" border="0" width="100%" id="tblEditReviewForm"
            class="cssClassPadding">
            <tr>
                <td>
                    <label class="cssClassLabel">
                        Item:</label>
                </td>
                <td class="cssClassTableRightCol">
                    <a href="#" id="lnkItemName"></a>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="cssClassLabel">
                        Posted By:</label>
                </td>
                <td class="cssClassTableRightCol">
                    <label id="lblPostedBy" class="cssClassLabel">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="cssClassLabel">
                        View From IP:</label>
                </td>
                <td class="cssClassTableRightCol">
                    <label id="lblViewFromIP" class="cssClassLabel">
                    </label>
                    <%--<input type="text" id="txtViewFromIP" />--%>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="cssClassLabel">
                        Summary Rating:</label>
                </td>
                <td class="cssClassTableRightCol">
                    <div id="divAverageRating">
                    </div>
                    <span class="cssClassRatingTitle1" class="cssClassLabel"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="cssClassLabel">
                        Detailed Rating:</label>
                </td>
                <td class="cssClassTableRightCol">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tblRatingCriteria">
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="cssClassLabel">
                        Nick Name:<span class="cssClassRequired">*</span></label>
                </td>
                <td class="cssClassTableRightCol">
                    <input type="text" id="txtNickName" name="name" class="cssClassNormalTextBox required"
                        minlength="2" />
                </td>
            </tr>
            <tr>
                <td>
                    <label class="cssClassLabel">
                        Added On:</label>
                </td>
                <td class="cssClassTableRightCol">
                    <label id="lblAddedOn">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="cssClassLabel">
                        Summary Of Review:<span class="cssClassRequired">*</span></label>
                </td>
                <td class="cssClassTableRightCol">
                    <input type="text" id="txtSummaryReview" name="summary" class="cssClassNormalTextBox required"
                        minlength="2" />
                </td>
            </tr>
            <tr>
                <td>
                    <label class="cssClassLabel">
                        Review:<span class="cssClassRequired">*</span></label>
                </td>
                <td class="cssClassTableRightCol">
                    <textarea id="txtReview" cols="50" rows="10" name="review" class="cssClassTextarea required" maxlength="300"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="cssClassLabel">
                        Status:</label>
                </td>
                <td class="cssClassTableRightCol">
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
            <input type="submit" value="Submit" id="btnSubmitReview" class="cssClassButtonSubmit"/>
            <%--<button type="submit" id="" ><span><span>Save</span></span></button>--%>
            <%-- <input  type="submit" value="Submit" id="btnSubmitReview"/>--%>
        </p>
        <p>
            <button type="button" id="btnDeleteReview">
                <span><span>Delete</span></span></button>
        </p>
    </div>
    <div class="cssClassClear">
    </div>
</div>
