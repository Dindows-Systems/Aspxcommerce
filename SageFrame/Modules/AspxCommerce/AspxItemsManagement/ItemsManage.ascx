<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsManage.ascx.cs" Inherits="Modules_AspxItemsManagement_ItemsManage" %>

<script type="text/javascript">
    //<![CDATA[
var maxFileSize='<%=MaximumFileSize %>';
    //]]>
</script>

<!-- Grid -->
<div id="gdvItems_grid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="Manage Items"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnDeleteSelected">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddNew">
                            <span><span>Add New Item</span></span></button>
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
                                    SKU:</label>
                                <input type="text" id="txtSearchSKU" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Name:</label>
                                <input type="text" id="txtSearchName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Item Type:</label>
                                <select id="ddlSearchItemType" class="cssClassDropDown">
                                    <option value="0">--All--</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Attribute Set Name:</label>
                                <select id="ddlAttributeSetName" class="cssClassDropDown">
                                    <option value="0">--All--</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Visibility:</label>
                                <select id="ddlVisibitity" class="cssClassDropDown">
                                    <option value="">--All--</option>
                                    <option value="True">Yes</option>
                                    <option value="False">No</option>
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    IsActive:</label>
                                <select id="ddlIsActive" class="cssClassDropDown">
                                    <option value="">--All--</option>
                                    <option value="True">True</option>
                                    <option value="False">False</option>
                                </select>
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnSearchItems">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxImageLoader" src="" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvItems" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<!-- End of Grid -->
<!-- Add New Item -->
<div id="gdvItems_form" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblHeading" runat="server" Text="Add New Item"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <h3>
                <asp:Label ID="lblTabInfo" runat="server" Text="Create Item Settings"></asp:Label>
            </h3>
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding">
                <tr>
                    <td>
                        <asp:Label ID="lblAttributeSet" runat="server" Text="Attribute Set:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlAttributeSet" class="cssClassDropDown" name="D1">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblItemType" runat="server" Text="Item Type:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlItemType" class="cssClassDropDown" name="D2">
                        </select>
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
                <button type="button" id="btnReset">
                    <span><span>Reset</span></span></button>
            </p>
            <p>
                <button type="button" id="btnContinue">
                    <span><span>Continue</span></span></button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
</div>
<div id="gdvItems_accordin" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblNewItem" runat="server" Text="Add New Item"></asp:Label>
            </h2>
        </div>
        <input type="hidden" id="ItemMgt_itemID" value="0" />
        <div id="dynItemForm" class="cssClassAccordionWrapper">
        </div>
    </div>
</div>
<input type="hidden" id="hdnSKUTxtBox" />
<!-- End of Add New Item  -->
<%--<div class="popupbox cssClassItemCostVariant" id="popuprel">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span>Close</span></button>
    </div>
</div>--%>
<div class="popupbox cssClassVariantImagews" id="popuprel2">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span>Close</span></button>
    </div>
    <div class="cssClassGridWrapperContent">
        <div id="divUploader">
        <input type="file" class="cssClassBrowse" id="imageUploader"></div>
        <table cellspacing="0" cellpadding="o" border="0" width="100%" id="VariantsImagesTable">
            <thead>
                <tr class="cssClassHeading">
                    <td>
                        Image
                    </td>
                    <td>
                        Remove
                    </td>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnSaveImages">
                    <span><span>Save</span></span></button>
            </p>
              <p>
                <button type="button" id="btnImageBack">
                    <span><span>Back</span></span></button>
            </p>
            <div class="cssClassClear">
            </div>
        </div>
    </div>
</div>
