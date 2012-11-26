<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingProviderManagement.ascx.cs" Inherits="Modules_AspxCommerce_AspxShippingManagement_ShippingProviderManagement" %>
<script type="text/javascript">
    //<![CDATA[
   var lblSPHeading='<%=lblSPHeading.ClientID %>';
//]]>
</script>

<div id="divShippingProviderDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblShippingProvider" runat="server" Text="Shipping Providers"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnSPDeleteSelected">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnSPAddNew">
                            <span><span>Add New Shipping Provider</span></span></button>
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
          <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="cssClassSearchPanel cssClassFormWrapper">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>                            
                            <td>
                                <asp:Label ID="lblShippingProviderName" runat="server" CssClass="cssClassLabel" Text="Shipping Provider Name:"></asp:Label>
                                <input type="text" id="txtSearchShippingProviderName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <asp:Label ID="lblShippingProviderIsActive" runat="server" CssClass="cssClassLabel" Text="Is Active:"></asp:Label>
                                <select id="ddlSPVisibitity" class="cssClassDropDown">
                                    <option value="">--All--</option>
                                    <option value="True">Yes</option>
                                    <option value="False">No</option>
                                </select>
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="shippingProviderMgmt.SearchShippingProvider()" >
                                            <span><span>Search</span></span></button> <!--onclick="SearchOrderStatus()"-->
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxShippingProviderImage" src=""  alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="tblShippingProviderList" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
        
    </div>
</div>

<div id="divEditShippingProvider" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblSPHeading" runat="server" Text="Edit Shipping Provider ID:"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding">
                 <tr>
                    <td>
                        <asp:Label ID="lblSPName" runat="server" Text="Shipping Provider Name:" CssClass="cssClassLabel"></asp:Label><span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtSPName" name="name2" class="cssClassNormalTextBox required" minlength="2" />
                        <span id="sperrorLabel"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSPServiceCode" runat="server" Text="Shipping Provider Code:" CssClass="cssClassLabel"></asp:Label><span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtSPServiceCode" name="name" class="cssClassNormalTextBox required" minlength="2" />                        
                    </td>
                </tr>               
                <tr>
                    <td>
                        <asp:Label ID="lblSPAliasHelp" runat="server" Text="Shipping Provider Alias Help:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtSPAliasHelp" class="cssClassNormalTextBox" />
                    </td>
                </tr>
                <tr id="isActiveSp">
                    <td>
                        <asp:Label ID="lblSPIsActive" runat="server" Text="Is Active:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <div id="chkIsActiveShippingProvider" class="cssClassCheckBox">
                            <input id="chkIsActiveSP" type="checkbox" name="chkIsActive" />
                        </div>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        <asp:Label ID="lblOrderStatusIsDeleted" runat="server" Text="Is Deleted:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <div id="Div1" class="cssClassCheckBox">
                            <input id="chkIsDeleted" type="checkbox" name="chkIsDeleted" />
                        </div>
                    </td>
                </tr>--%>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnSPBack">
                    <span><span>Cancel</span></span></button>
            </p>
            <p>
                <button type="reset" id="btnSPReset">
                    <span><span>Reset</span></span></button>
            </p>
            <p>
                <button type="button" id="btnSaveShippingProvider"  class="cssClassButtonSubmit" type="submit" value="Save">
                    <span><span>Save</span></span></button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
</div>
