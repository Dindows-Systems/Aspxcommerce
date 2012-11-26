<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponPerUsersManage.ascx.cs"
    Inherits="Modules_AspxCouponManagement_CouponUserManageMent" %>

<script type="text/javascript">
    //<![CDATA[
    var couponPerSalesDataToExcel = "<%=btnExportDataToExcel %>";

    //]]>
</script>

<div id="divShowCouponTypeDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="Coupon Per Customers"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnDeleteAllNonPendingCoupon">
                            <span><span>Delete All Non Pending Coupon User(s)</span></span></button>
                    </p>
                    <p>
                        <asp:Button ID="btnExportDataToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="btnExportDataToExcel_Click" Text="Export to Excel" OnClientClick="couponPerUserMgmt.ExportCouponPerUserDivDataToExcel()" />
                    </p>
                    <p>
                        <asp:Button ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit" OnClick="ButtonCouponPerUser_Click"
                            Text="Export to CSV" OnClientClick="couponPerUserMgmt.ExportCouponPerUserToCsvData()" />
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
                                    Coupon Code:</label>
                                <input type="text" id="txtSearchCouponCode" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    UserName:</label>
                                <input type="text" id="txtSearchUserName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Coupon Status:</label>
                                <select id="ddlCouponStatus" class="cssClassDropDown">
                                    <option value="0">--All--</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Valid From:</label>
                                <input type="text" id="txtValidFrom" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Valid To:</label>
                                <input type="text" id="txtValidTo" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="couponPerUserMgmt.SearchCouponCode()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCouponPerUserImageLoad" src="" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvCouponUser" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="CouponUserExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                </table>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hdnCouponUserID" />
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="_csvCouponPerUserHiddenValue" runat="server" />
