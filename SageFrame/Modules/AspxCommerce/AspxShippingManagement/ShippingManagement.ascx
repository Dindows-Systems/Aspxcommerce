<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShippingManagement.ascx.cs"
    Inherits="Modules_AspxShippingManagement_ShippingManagement" %>

<script type="text/javascript">
	//<![CDATA[
    var maxFileSize = '<%=MaxFileSize%>';

	 //]]>
</script>

<!-- Grid -->
<div id="divShowShippingMethodGrid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitleShippingMethods" runat="server" Text="Shipping Methods"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnDeleteSelected">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddNewShippingMethod">
                            <span><span>Add New Shipping Method</span></span></button>
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
                                    Shipping Method Name:</label>
                                <input type="text" id="txtMethodName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Delivery Time:</label>
                                <input type="text" id="txtSearchDeliveryTime" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Weight Limit From:</label>
                                <input type="text" id="txtWeightFrom" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Weight Limit To:</label>
                                <input type="text" id="txtWeightTo" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    IsActive:</label>
                                <select id="ddlIsActive" class="cssClassDropDown">
                                    <option value="">--All--</option>
                                    <option value="0">True</option>
                                    <option value="1">False</option>
                                </select>
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="ShippingManage.SearchShippingMethods()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxShippingMgmtImage1" src="" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvShippingMethod" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<!-- End of Grid -->
<!-- form -->
<div id="divAddNewShippingMethodForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblHeading" runat="server" Text="Add New Shipping Method"></asp:Label>
            </h2>
        </div>
        <div class="cssClassTabPanelTable">
            <div class="cssClassTabpanelContent" id="container-7">
                <ul>
                    <li><a href="#fragment-1">
                        <asp:Label ID="lblTabTitle1" runat="server" Text="General Settings"></asp:Label>
                    </a></li>
                    <li id="liShippingSettingChanges"><a href="#fragment-2">
                        <asp:Label ID="lblTabTitle2" runat="server" Text="Shipping Charges Settings"></asp:Label>
                    </a></li>
                </ul>
                <div id="fragment-1">
                    <div class="cssClassFormWrapper">
                        <h3>
                            <asp:Label ID="lblTab1Info" runat="server" Text="General Settings"></asp:Label>
                        </h3>
                        <table border="0" width="100%" id="tblShippingMethodForm">
                            <tr>
                                <td>
                                    <asp:Label ID="lblShippingMethodName" Text="Name:" runat="server" CssClass="cssClassLabel"></asp:Label>
                                    <span class="cssClassRequired">*</span>
                                </td>
                                <td>
                                    <input type="text" id="txtShippingMethodName" name="name" class="cssClassNormalTextBox required"
                                        minlength="2" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShippingMethodIcon" runat="server" Text="Icon:" CssClass="cssClassLabel"></asp:Label>
                                    <%--<span class="cssClassRequired">*</span>--%>
                                </td>
                                <td>
                                    <input id="fileUpload" type="file" class="cssClassBrowse" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <div id="shippingIcon">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShippingAlternateText" runat="server" Text="AlternateText:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td>
                                    <input type="text" id="txtAlternateText" class="cssClassNormalTextBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShippingDisplayOrder" runat="server" Text="Display Order:" CssClass="cssClassLabel"></asp:Label>
                                    <span class="cssClassRequired">*</span>
                                </td>
                                <td>
                                    <input type="text" id="txtDisplayOrder" name="displayOrder" class="cssClassNormalTextBox required digits displayOrder"
                                        minlength="1" />
                                    <span id="errdisplayOrder"></span><span id="erruniqueOrder"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShippingDeliveryTime" runat="server" Text="Delivery Time:" CssClass="cssClassLabel"></asp:Label>
                                    <span class="cssClassRequired">*</span>
                                </td>
                                <td>
                                    <input type="text" id="txtDeliveryTime" name="deliveryTime" class="cssClassNormalTextBox required"
                                        minlength="2" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblWeightLimitFrom" runat="server" Text="Weight Limit From:" CssClass="cssClassLabel"></asp:Label>
                                    <span class="cssClassRequired">*</span>
                                </td>
                                <td>
                                    <input type="text" id="txtWeightLimitFrom" name="weightFrom" maxlength="5" class="cssClassNormalTextBox required number weightFrom"
                                        minlength="1" /><span id='lblNotificationlf' style="color: #FF0000;"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblWeightLimitTo" runat="server" Text="Weight Limit To:" CssClass="cssClassLabel"></asp:Label>
                                    <span class="cssClassRequired">*</span>
                                </td>
                                <td>
                                    <input type="text" id="txtWeightLimitTo" name="weightTo" maxlength="5" class="cssClassNormalTextBox required number weightTo"
                                        minlength="1" /><span id='lblNotificationlt' style="color: #FF0000;"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShippingService" runat="server" Text="Shipping Provider Name:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td>
                                    <select id="ddlShippingService" class="cssClassDropDown" name="" title="Shipping Method List">
                                    <option value="0">--Select Provider--</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsActive" runat="server" Text="IsActive:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td>
                                    <input type="checkbox" id="chkIsActive" class="cssClassCheckBox" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="fragment-2">
                    <div class="cssClassFormWrapper">
                        <h3>
                            <asp:Label ID="lblTab2Info" runat="server" Text="Shipping Charges Settings"></asp:Label>
                        </h3>
                        <div>
                            <div class="cssClassCommonBox Curve">
                                <div class="cssClassHeader">
                                    <h2>
                                        <asp:Label ID="lblTitle1" runat="server" Text="Cost Dependencies:"></asp:Label>
                                    </h2>
                                    <div class="cssClassHeaderRight">
                                        <div class="cssClassButtonWrapper">
                                            <p>
                                                <button type="button" id="btnDeleteCostDependencies">
                                                    <span><span>Delete Selected Cost Dependencies</span></span></button>
                                            </p>
                                            <p>
                                                <button type="button" id="btnAddCostDependencies" rel="popuprel">
                                                    <span><span>Add Cost Dependencies</span></span></button>
                                            </p>
                                            <div class="cssClassClear">
                                            </div>
                                        </div>
                                        <div class="cssClassClear">
                                        </div>
                                    </div>
                                </div>
                                <div class="cssClassGridWrapper">
                                    <div class="cssClassGridWrapperContent">
                                        <div class="loading">
                                            <img id="ajaxShippingMgmtImage4" src="" alt="loading...." />
                                        </div>
                                        <div class="log">
                                        </div>
                                        <table id="gdvAddCostDependencies" width="100%" border="0" cellpadding="0" cellspacing="0">
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <div class="cssClassCommonBox Curve">
                                <div class="cssClassHeader">
                                    <h2>
                                        <asp:Label ID="lblWtDependencyTitle" runat="server" Text="Weight Dependencies:"></asp:Label>
                                    </h2>
                                    <div class="cssClassHeaderRight">
                                        <div class="cssClassButtonWrapper">
                                            <p>
                                                <button type="button" id="btnDeleteWeightDependencies">
                                                    <span><span>Delete Selected Weight Dependencies</span></span></button>
                                            </p>
                                            <p>
                                                <button type="button" id="btnAddWeightDependencies" rel="popuprel">
                                                    <span><span>Add Weight Dependencies</span></span></button>
                                            </p>
                                            <div class="cssClassClear">
                                            </div>
                                        </div>
                                        <div class="cssClassClear">
                                        </div>
                                    </div>
                                </div>
                                <div class="cssClassGridWrapper">
                                    <div class="cssClassGridWrapperContent">
                                        <div class="loading">
                                            <img id="ajaxShippingMgmtImage3" src="" alt="loading...." />
                                        </div>
                                        <div class="log">
                                        </div>
                                        <table id="gdvAddWeightDependencies" width="100%" border="0" cellpadding="0" cellspacing="0">
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="cssClassClear">
                    </div>
                    <div>
                        <div class="cssClassCommonBox Curve">
                            <div class="cssClassHeader">
                                <h2>
                                    <asp:Label ID="lblItemDepTitle" runat="server" Text="Item Dependencies:"></asp:Label>
                                </h2>
                                <div class="cssClassHeaderRight">
                                    <div class="cssClassButtonWrapper">
                                        <p>
                                            <button type="button" id="btnDeleteItemDependencies">
                                                <span><span>Delete Selected Item Dependencies</span></span></button>
                                        </p>
                                        <p>
                                            <button type="button" id="btnAddItemDependencies" rel="popuprel">
                                                <span><span>Add Item Dependencies</span></span></button>
                                        </p>
                                        <div class="cssClassClear">
                                        </div>
                                    </div>
                                    <div class="cssClassClear">
                                    </div>
                                </div>
                            </div>
                            <div class="cssClassGridWrapper">
                                <div class="cssClassGridWrapperContent">
                                    <div class="loading">
                                        <img id="ajaxShippingMgmtImage2" src="" alt="loading...." />
                                    </div>
                                    <div class="log">
                                    </div>
                                    <table id="gdvAddItemDependencies" width="100%" border="0" cellpadding="0" cellspacing="0">
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassButtonWrapper">
                <p>
                    <button type="button" id="btnCancel">
                        <span><span>Cancel</span></span></button>
                </p>
                <p>
                    <input type="button" id="btnReset" value="Reset" class="cssClassButtonSubmit" />
                </p>
                <p>
                    <input type="submit" id="btnSave" value="Save" class="cssClassButtonSubmit" />
                </p>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hdnShippingMethodID" />
<input type="hidden" id="hdnPrevFilePath" />
<!-- End form -->
<!--PopUP-->
<div class="popupbox" id="popuprel">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span>Close</span></button>
    </div>
    <h2>
        <label id="lblTitleDependencies">
        </label>
    </h2>
    <table border="0" width="100%" cellpadding="0" cellspacing="0" id="tblcostdependencies">
        <thead>
            <th>
                &nbsp;
            </th>
            <th>
                Cost
            </th>
            <th>
                Rate Value
            </th>
            <th>
                Rate Type
            </th>
            <th>
                &nbsp;
            </th>
        </thead>
        <tr>
            <td>
                <label id="lblCostDedendencies" class="cssClassLabel">
                    More Than $</label>
            </td>
            <td>
                <input type="text" id="txtCost" name="cost" maxlength="5" class="cssClassCost" minlength="1" />
                <span class="cssClassRequired">*</span><span id="errmsgCost"></span>
            </td>
            <td>
                <input type="text" id="txtCostRateValue" name="rateValue" maxlength="5" class="cssClassCostRateValue"
                    minlength="1" />
                <span class="cssClassRequired">*</span><span id="errmsgRateValue"></span>
            </td>
            <td>
                <select id="ddlCostDependencies" class="cssClassDropDownCostDependencies">
                    <option value="0">Absolute ($)</option>
                    <option value="1">Percent (%)</option>
                </select>
            </td>
            <td>
                <span class="nowrap">
                    <img width="13" height="18" border="0" align="top" class="cssClassAddRow" title="Add empty item"
                        alt="Add empty item" name="add" src="" />&nbsp;
                    <img width="13" height="18" border="0" align="top" class="cssClassCloneRow" alt="Clone this item"
                        title="Clone this item" name="clone" src="" />&nbsp;
                    <img width="12" height="18" border="0" align="top" class="cssClassDeleteRow" alt="Remove this item"
                        name="remove" src="" />&nbsp; </span>
            </td>
        </tr>
    </table>
    <div class="cssClassButtonWrapper" id="CostDependencyButtonWrapper">
        <p>
            <button type="button" id="btnCancelCostDependencies">
                <span><span>Cancel</span></span></button>
        </p>
        <p>
            <button type="button" id="btnCreateCost">
                <span><span>Create</span></span></button>
        </p>
    </div>
    <table border="0" width="100%" cellpadding="0" cellspacing="0" id="tblWeightDependencies">
        <thead>
            <th>
                &nbsp;
            </th>
            <th>
                Weight
            </th>
            <th>
                Rate Value
            </th>
            <th>
                Rate Type
            </th>
            <th>
                Is Per Lbs?
            </th>
            <th>
                &nbsp;
            </th>
        </thead>
        <tr>
            <td>
                <label id="lblWeightDedendencies" class="cssClassLabel">
                    More Than
                </label>
            </td>
            <td>
                <input type="text" id="txtWeight" name="weight" class="cssClassWeight" />
                lbs<span class="cssClassRequired">*</span> <span id="errmsgWeight"></span>
            </td>
            <td>
                <input type="text" id="txtWeightRateValue" name="weightRateValue" class="cssClassWeightRateValue" />
                <span class="cssClassRequired">*</span><span id="errmsgWeightRateValue"></span>
            </td>
            <td>
                <select id="ddlWeightDependencies" class="cssClassDropDownCostDependencies">
                    <option value="0">Absolute ($)</option>
                    <option value="1">Percent (%)</option>
                </select>
            </td>
            <td>
                <input type="checkbox" id="chkPerLbs" class="cssClassWeightIsActive" />
            </td>
            <td>
                <span class="nowrap">
                    <img width="13" height="18" border="0" align="top" class="cssClassWeightAddRow" title="Add empty item"
                        alt="Add empty item" name="add" src="" />&nbsp;
                    <img width="13" height="18" border="0" align="top" class="cssClassWeightCloneRow"
                        alt="Clone this item" title="Clone this item" name="clone" src="" />&nbsp;
                    <img width="12" height="18" border="0" align="top" class="cssClassWeightDeleteRow"
                        alt="Remove this item" name="remove" src="" />&nbsp; </span>
            </td>
        </tr>
    </table>
    <div class="cssClassButtonWrapper" id="WeightDependencyButtonWrapper">
        <p>
            <button type="button" id="btnCancelWeightDependencies">
                <span><span>Cancel</span></span></button>
        </p>
        <p>
            <button type="button" id="btnCreateWeight">
                <span><span>Create</span></span></button>
        </p>
    </div>
    <table border="0" width="100%" cellpadding="0" cellspacing="0" id="tblItemDependencies">
        <thead>
            <th>
                &nbsp;
            </th>
            <th>
                Quantity
            </th>
            <th>
                Rate Value
            </th>
            <th>
                Rate Type
            </th>
            <th>
                Is Per Items?
            </th>
            <th>
                &nbsp;
            </th>
        </thead>
        <tr>
            <td>
                <label id="lblItemDedendencies" class="cssClassLabel">
                    More Than
                </label>
            </td>
            <td>
                <input type="text" id="txtQuantity" name="quantity" class="cssClassQuantity" maxlength="5" />
                item(s)<span class="cssClassRequired">*</span> <span id="errmsgQty"></span>
            </td>
            <td>
                <input type="text" id="txtQuantityRateValue" name="quantityRateValue"  class="cssClassQuantityRateValue" />
                <span class="cssClassRequired">*</span><span id="errmsgQtyRateValue"></span>
            </td>
            <td>
                <select id="ddlItemDependencies" class="cssClassDropDownCostDependencies">
                    <option value="0">Absolute ($)</option>
                    <option value="1">Percent (%)</option>
                </select>
            </td>
            <td>
                <input type="checkbox" id="chkPerItems" class="cssClassItemIsActive" />
            </td>
            <td>
                <span class="nowrap">
                    <img width="13" height="18" border="0" align="top" class="cssClassItemAddRow" title="Add empty item"
                        alt="Add empty item" name="add" src="" />&nbsp;
                    <img width="13" height="18" border="0" align="top" class="cssClassItemCloneRow" alt="Clone this item"
                        title="Clone this item" name="clone" src="" />&nbsp;
                    <img width="12" height="18" border="0" align="top" class="cssClassItemDeleteRow"
                        src="" alt="Remove this item" name="remove" />&nbsp; </span>
            </td>
        </tr>
    </table>
    <div class="cssClassButtonWrapper" id="ItemDependencyButtonWrapper">
        <p>
            <button type="button" id="btnCancelItemDependencies">
                <span><span>Cancel</span></span></button>
        </p>
        <p>
            <button type="button" id="btnCreateItem">
                <span><span>Create</span></span></button>
        </p>
    </div>
</div>
<!-- End PopUP -->
