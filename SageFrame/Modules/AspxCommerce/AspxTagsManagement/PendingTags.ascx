<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PendingTags.ascx.cs" Inherits="Modules_AspxCommerce_AspxTagsManagement_PendingTags" %>

<script type="text/javascript">
    //<![CDATA[
    var lblEditTagDetails = '<%=lblEditTagDetails.ClientID %>';
    var lblTagViewHeading = '<%=lblPendingTagViewHeading.ClientID %>';
    //]]>
</script>

<div class="cssClassBodyContentWrapper" id="divShowTagDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="Pending Tags"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnApproveAllSelected">
                            <span><span>Approve All Selected</span></span></button>
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
                                    Tag:</label>
                                <input type="text" id="txtSearchTag" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnSearchPendingTags">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxPendingTagsImage" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvTags" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div class="cssClassBodyContentWrapper" id="divEditTag" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblEditTagDetails" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding">
                <tr>
                    <td>
                        <asp:Label ID="lblTagTitle" runat="server" Text="Tag:" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtTag" name="Tag" class="cssClassNormalTextBox required" maxlength="20" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStatus" runat="server" Text="Status:" CssClass="cssClassLabel"></asp:Label>
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
                <button id="btnSaveTag" type="button">
                    <span><span>Save</span></span></button>
            </p>
            <p>
                <button id="btnCancel" type="button">
                    <span><span>Cancel</span></span></button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
    <input type="hidden" id="hdnItemTagID" />
    <input type="hidden" id="hdnTag" />
    <input type="hidden" id="hdnStatusID" />
</div>
<div class="divTagedItemsDetails" id="divTagedItemsDetails" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblPendingTagViewHeading" runat="server"></asp:Label></h2>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <thead>
                        <tr class="cssClassHeading">
                            <td>
                                Item Image
                            </td>
                            <td>
                                Item Name
                            </td>
                            <td>
                                SKU
                            </td>
                            <td>
                                Price
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnBack">
                    <span><span>Back</span></span></button>
            </p>
        </div>
    </div>
</div>
