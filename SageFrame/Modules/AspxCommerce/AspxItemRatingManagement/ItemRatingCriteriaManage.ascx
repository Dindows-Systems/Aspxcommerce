<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemRatingCriteriaManage.ascx.cs"
    Inherits="Modules_AspxItemRatingManagement_ItemRatingCriteriaManage" %>

<script type="text/javascript">
    //<![CDATA[
   var lblItemRatingFormTitle='<%=lblItemRatingFormTitle.ClientID %>';
    //]]>
</script>

<div id="divShowItemCriteriaDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="Manage Item Rating Criteria"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" class="" id="btnDeleteSelectedCriteria">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                    <%--<p> <input type="button" class="" id="btnDeactivateSelected" value="Deactivate All Selected" /> </p>--%>
                    <p>
                        <button type="button" class="" id="btnAddNewCriteria">
                            <span><span>Add New Criteria</span></span></button>
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
                                    Rating Criteria:</label>
                                <input type="text" id="txtSearchCriteria" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Is Active:</label>
                                <select id="ddlIsActive" class="cssClassDropDown">
                                    <option value="">--All--</option>
                                    <option value="True">Yes</option>
                                    <option value="False">No</option>
                                </select>
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnCriteriaSearch" >
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxItemRatingCriteriaImage" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvItemRatingCriteria" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divItemCriteriaForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblItemRatingFormTitle" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table border="0" width="100%" id="tblEditReviewForm" class="cssClassPadding tdpadding">
                <tr>
                    <td>
                        <asp:Label ID="lblCriteria" runat="server" Text="Criteria:" CssClass="cssClassLabel"> </asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtNewCriteria" name="CriteriaTypeName" class="cssClassNormalTextBox required" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIsActive" runat="server" Text="Is Active:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="checkbox" id="chkIsActive" class="cssClassCheckBox" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnCancelCriteriaUpdate">
                    <span><span>Cancel</span></span></button>
            </p>
            <p>
                <button type="button" id="btnSubmitCriteria">
                <span><span>Save</span></span></button>
            </p>
        </div>
        <input type="hidden" id="hdnItemCriteriaID" />
    </div>
</div>
