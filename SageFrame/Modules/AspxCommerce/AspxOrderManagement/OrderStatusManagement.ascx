<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderStatusManagement.ascx.cs"
    Inherits="Modules__ModulesInstalltions_AspxOrderStatusManagement_OrderStatusManagement" %>

<script type="text/javascript">
    //<![CDATA[
    var lblHeading = "<%= lblHeading.ClientID %>";
//]]>
</script>

<div id="divOrderStatusDetail">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblOrderStatusHeading" runat="server" Text="Manage Order Status"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnDeleteSelected">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddNew">
                            <span><span>Add New Order Status</span></span></button>
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
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <%--<td>
                                <asp:Label ID="lblOrderID" runat="server" CssClass="cssClassLabel" Text="Order ID:"></asp:Label>
                                <input type="text" id="txtOrderID" class="cssClassTextBoxSmall" />
                            </td>--%>
                            <td>
                                <asp:Label ID="lblStatusName" runat="server" CssClass="cssClassLabel" Text="Status Name:"></asp:Label>
                                <input type="text" id="txtOrderStateName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" CssClass="cssClassLabel" Text="Is Active:"></asp:Label>
                                <select id="ddlVisibitity" class="cssClassDropDown">
                                    <option value="">--All--</option>
                                    <option value="True">Yes</option>
                                    <option value="False">No</option>
                                </select>
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="OrderStatusMgmt.SearchOrderStatus()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxOrderStatusMgmtImage" src="" title="loading...." alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="tblOrderStatusDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<div id="divEditOrderStatus" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblHeading" runat="server" Text="Edit Order Status ID:"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding">
                <tr>
                    <td>
                        <asp:Label ID="lblOrderStatusName" runat="server" Text="Order Status Name:" CssClass="cssClassLabel"></asp:Label><span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtOrderStatusAliasName" name="StatusName" class="cssClassNormalTextBox required" minlength="2" /><span id="osErrorLabel"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAliasToolTip" runat="server" Text="Alias Tool Tip:" CssClass="cssClassLabel"></asp:Label><span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtAliasToolTip" name="ToolTipName" class="cssClassNormalTextBox required" minlength="2"  />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAliasHelp" runat="server" Text="Alias Help:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtAliasHelp" class="cssClassNormalTextBox" />
                    </td>
                </tr>
                <tr id="isActiveTR">
                    <td>
                        <asp:Label ID="lblOrderStatusIsActive" runat="server" Text="Is Active:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <div id="chkIsActiveOrderStatus" class="cssClassCheckBox">
                            <input id="chkIsActiveOrder" type="checkbox" name="chkIsActive" />
                        </div>
                    </td>
                </tr>
                   <tr id="Tr1">
                    <td>
                        <asp:Label ID="lblReduceQuantiry" runat="server" Text="Reduce Item Quantity:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <div id="Div1" class="cssClassCheckBox">
                            <input id="chkIsReduceQuantity" type="checkbox" name="chkIsReduceQuantity" />
                        </div>
                    </td>
                </tr>               
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnBack">
                    <span><span>Back</span></span></button>
            </p>
            <p>
                <button type="reset" id="btnReset">
                    <span><span>Reset</span></span></button>
            </p>
            <p>
                <button type="button" id="btnSaveOrderStatus">
                    <span><span>Save Status</span></span></button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
</div>
<input id="hdnIsSystem" type="hidden" />