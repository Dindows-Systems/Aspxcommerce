<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponManage.ascx.cs"
    Inherits="Modules_AspxCouponManagement_CouponManage" %>

<script type="text/javascript">
    //<![CDATA[
    var lblCouponManageTitle = "<%=lblCouponManageTitle.ClientID %>";
    var lblCouponUserTitle = "<%=lblCouponUserTitle.ClientID %>";
    var userEmail= "<%=UserEmail %>";
    var ServerVariables ='<%=Request.ServerVariables["SERVER_NAME"]%>';
	//]]>
</script>

<div id="divShowCouponDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="Coupons"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnDeleteSelectedCoupon">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddNewCoupon">
                            <span><span>Add New Coupon</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnBackToCouponTbl">
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
                            <td class="cssClassddlCouponType">
                                <label class="cssClassLabel">
                                    Coupon Type:</label>
                                <select id="ddlSearchCouponType" class="cssClassDropDown">
                                    <option value="0">--All--</option>
                                </select>
                            </td>
                            <td class="cssClassddlCouponStatus">
                                <label class="cssClassLabel">
                                    Coupon Status:</label>
                                <select id="ddlCouponStatus" class="cssClassDropDown">
                                    <option value="0">--All--</option>
                                </select>
                            </td>
                            <td class="cssClasstxtSearchUserName">
                                <label class="cssClassLabel">
                                    UserName:</label>
                                <input type="text" id="txtSearchUserName" class="cssClassTextBoxSmall" />
                            </td>
                            <td class="cssClasstxtSearchCouponCode">
                                <label class="cssClassLabel">
                                    Coupon Code:</label>
                                <input type="text" id="txtSearchCouponCode" class="cssClassTextBoxSmall" />
                            </td>
                            <td class="cssClasstxtSearchValidateFrom">
                                <label class="cssClassLabel">
                                    Validate From:</label>
                                <input type="text" id="txtSearchValidateFrom" class="cssClassTextBoxSmall" />
                            </td>
                            <td class="cssClasstxtSearchValidateTo">
                                <label class="cssClassLabel">
                                    Validate To:</label>
                                <input type="text" id="txtSearchValidateTo" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="couponMgmt.SearchCouponDetails()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCouponImageLoad2" />
                </div>
                <div class="log">
                </div>
                <table id="gdvCoupons" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <table id="gdvCouponUser" cellspacing="0" cellpadding="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divCouponForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCouponManageTitle" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table border="0" width="100%" id="tblEditCouponForm" class="cssClassPadding">
                <tr>
                    <td>
                        <asp:Label ID="lblCouponType" Text="CouponType:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlCouponType" class="cssClassDropDown required">
                         <option value="0">--Select One--</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIsForFreeShipping" runat="server" Text="Is For Free Shipping:"
                            CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlIsForFreeShipping" class="cssClassDropDown">
                            <option value="1">NO</option>
                            <option value="2">YES</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCoupon" runat="server" Text="CouponCode:" CssClass="cssClassLabel"> </asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtNewCoupon" name="newCoupon" class="cssClassNormalTextBox required"
                            minlength="2" />
                        <button type="button" id="btnGenerateCode" class="cssClassButtonSubmit" onclick= "couponMgmt.GenerateCodeString()">
                            <span><span>Generate Code</span></span></button><span id="spancouponCode"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCouponAmount" Text="Amount:" runat="server" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtAmount" name="amount" class="cssClassNormalTextBox required" /><span id="couponAmountErrorLabel"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblValidFrom" Text="Valid From:" runat="server" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtValidFrom" name="validateFrom" class="from cssClassNormalTextBox required" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblValidTo" Text="Valid To:" runat="server" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtValidTo" name="validateTo" class="to cssClassNormalTextBox required" />
                        <span id ="created" style="color:#ED1C24;"></span>
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
                <tr class="cssClassUsesPerCoupon">
                    <td>
                        <asp:Label ID="lblUsesPerCoupon" runat="server" Text="Uses Per Coupon:" CssClass="cssClassLabel"
                            Visible="false"></asp:Label>
                        <%-- <span class="cssClassRequired">*</span>--%>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtUsesPerCoupon" visible="false" name="usesPerCoupon" class="cssClassNormalTextBox required" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUsesPerCustomer" runat="server" Text="Uses Per Customer:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtUsesPerCustomer" name="userPerCustomer" class="cssClassNormalTextBox required" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPortalUser" runat="server" Text="Select Customers:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <div class="cssClassCommonBox Curve">
                            <div class="cssClassHeader">
                                <h2>
                                    <span id="ctl13_lblTitle">Select whom To send the Coupon Code:</span>
                                </h2>
                            </div>
                            <div class="cssClassGridWrapper">
                                <div class="cssClassGridWrapperContent">
                                    <div class="cssClassSearchPanel cssClassFormWrapper">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <%--<td class="cssClasstxtRoleName">
                                                    <label class="cssClassLabel">
                                                        Role Name:</label>
                                                    <input type="text" id="txtSearchRoleName" class="cssClassTextBoxSmall" />
                                                </td>
                                                <td class="cssClasstxtSearchUserName">
                                                <label class="cssClassLabel">
                                                    UserName:</label>
                                                <input type="text" id="txtSearchUserName" class="cssClassTextBoxSmall" />
                                                </td>--%>
                                                <td class="cssClasstxtCustomerName">
                                                    <label class="cssClassLabel">
                                                        Customer Name:</label>
                                                    <input type="text" id="txtSearchCustomerName" class="cssClassTextBoxSmall" />
                                                </td>
                                                <td>
                                                    <div class="cssClassButtonWrapper cssClassPaddingNone">
                                                        <p>
                                                            <button type="button" onclick="couponMgmt.SearchCouponPortalUsers()">
                                                                <span><span>Search</span></span></button>
                                                        </p>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="loading">
                                        <img id="ajaxCouponMgmtImageLoad"  />
                                    </div>
                                    <div class="log">
                                    </div>
                                    <table id="gdvPortalUser" cellspacing="0" cellpadding="0" border="0" width="100%">
                                    </table>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnCancelCouponUpdate">
                    <span><span>Cancel</span></span></button>
            </p>
            <p>
                <button type="button" id="btnSubmitCoupon">
                    <span><span>Save</span></span></button>
            </p>
        </div>
    </div>
</div>
<input type="hidden" id="hdnCouponID" />
<div id="divCouponUserForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCouponUserTitle" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table border="0" width="100%" id="Table1" class="cssClassPadding tdpadding">
                <tr>
                    <td>
                        <asp:Label ID="lblCouponCode" Text="Coupon Code:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <label id="txtCouponCode" class="cssClassLabel">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUserName" runat="server" Text="User Name:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <label id="txtUserName" cssclass="cssClassLabel">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCouponStatus" Text="Coupon Status:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlCouponStatusType" class="cssClassDropDown">
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnCancelCouponUserUpdate">
                    <span><span>Cancel</span></span></button>
            </p>
            <p>
                <button type="button" id="btnSubmitCouponUser">
                    <span><span>Save</span></span></button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
</div>
<input type="hidden" id="hdnCouponUserID" />
<input type="hidden" id="hdnDeleteAllSelectedCouponUser" />
