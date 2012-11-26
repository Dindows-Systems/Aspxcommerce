<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponTypeManage.ascx.cs"
    Inherits="Modules_AspxCouponManagement_CouponTypeManage" %>

<script type="text/javascript">
    //<![CDATA[
    var lblCouponTypeFormTitle = "<%=lblCouponTypeFormTitle.ClientID %>";
    //]]>
</script>

<div id="divShowCouponTypeDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCouponTypeGridTitle" runat="server" Text="Manage Coupon Types"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnDeleteSelectedCouponType">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddNewCouponType">
                            <span><span>Add New Coupon Type</span></span></button>
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
                                    Coupon Type:</label>
                                <input type="text" id="txtSearchCouponType" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="couponTypeMgmt.SearchCouponType()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCouponTypeImageLoad" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvCouponType" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divCouponTypeProviderForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCouponTypeFormTitle" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table border="0" width="100%" id="tblEditCouponForm" class="cssClassPadding tdpadding">
                <tr>
                    <td>
                        <asp:Label ID="lblCouponType" Text="CouponType:" runat="server" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtNewCouponType" name="CouponTypeName" class="cssClassNormalTextBox required" minlength="2" /><span id="ctErrorLabel"></span>
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
                <button type="button" id="btnCancelCouponTypeUpdate">
                    <span><span>Cancel</span></span></button>
            </p>
            <p>
                <button type="button" id="btnSubmitCouponType">
                    <span><span>Save</span></span></button>
            </p>
        </div>
    </div>
</div>
