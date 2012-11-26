<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CostVariantOptions.ascx.cs"
    Inherits="Modules_AspxCostVariantOptionsManagement_CostVariantOptions" %>

<script type="text/javascript">
	//<![CDATA[
    var lblCostVarFormHeading='<%=lblCostVarFormHeading %>';
    //]]>
</script>

<!-- Grid -->
<div id="divShowOptionDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCostVarGridHeading" runat="server" Text="Variant Options"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnDeleteSelected">
                            <span><span>Delete All Selected</span></span>
                        </button>
                    </p>
                    <p>
                        <button type="button" id="btnAddNewVariantOption">
                            <span><span>Add New Cost Variant Option</span></span></button>
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
                                    Cost Variant Name:</label>
                                <input type="text" id="txtVariantName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnSearchCostVariants">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxLoad" src="" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvCostVariantGrid" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<!-- End of Grid -->
<!-- form -->
<div id="divAddNewOptions" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCostVarFormHeading" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="cssClassTabPanelTable">
            <div id="container-7" class="cssClassMargin">
                <ul>
                    <li><a href="#fragment-1">
                        <asp:Label ID="lblTabTitle1" runat="server" Text="Cost Variant Option
                                                                                Properties"></asp:Label>
                    </a></li>
                   <%-- <li><a href="#fragment-2">
                        <asp:Label ID="lblTabTitle2" runat="server" Text="Frontend Properties"></asp:Label>
                    </a></li>--%>
                    <li><a href="#fragment-3">
                        <asp:Label ID="lblTabTitle3" runat="server" Text="Variants Properties"></asp:Label>
                    </a></li>
                </ul>
                <div id="fragment-1">
                    <div class="cssClassFormWrapper">
                        <h3>
                            <asp:Label ID="lblTab1Info" runat="server" Text="General Information"></asp:Label>
                        </h3>
                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="tdpadding">
                            <tr>
                                <td>
                                    <asp:Label ID="lblCostVariantName" runat="server" Text="Cost Variant Name:" CssClass="cssClassLabel"></asp:Label>
                                    <span class="cssClassRequired">*</span>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="text" id="txtCostVariantName" class="cssClassNormalTextBox required"/>
                                    <span class="cssClassRight">
                                        <img class="cssClassSuccessImg" height="13" width="18" alt="Right" ></span>
                                    <b class="cssClassError">Ops! found something error, must be unique with no spaces</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCostVariantDescription" runat="server" Text="Description:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <textarea id="txtDescription" name="txtDescription" title="Cost Variant Description"
                                        rows="2" cols="15" class="cssClassTextArea"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblType" runat="server" Text="Type:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <select id="ddlAttributeType" class="cssClassDropDown" name="" title="Cost Variant Input Type">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDisplayOrder" runat="server" Text="Display Order:" CssClass="cssClassLabel"></asp:Label>
                                    <span class="cssClassRequired">*</span>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input class="cssClassNormalTextBox required" id="txtDisplayOrder" type="text"/><span id="dispalyOrder"></span>
                                </td>
                            </tr>
                            <%--<tr>
                                                                            <td>
                                                                                <asp:Label ID="lblIsSystemUse" runat="server" Text="Is System Use:" CssClass="cssClassLabel"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <div id="" class="cssClassCheckBox">
                                                                                    <input type="checkbox" name="chkIsSystemUse" />
                                                                                </div>
                                                                            </td>
                                                                        </tr>--%>
                            <tr>
                                <td>
                                    <asp:Label ID="lblActive" runat="server" Text="Is Active:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkActive" class="cssClassCheckBox" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
              <%--  <div id="fragment-2">
                    <div class="cssClassFormWrapper">
                        <h3>
                            <asp:Label ID="lblTab2Info" runat="server" Text="Frontend Display Settings"></asp:Label>
                        </h3>
                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="tdpadding">--%>
                            <%--<tr>
                                <td>
                                    <asp:Label ID="lblShowInGrid" runat="server" Text="Show in Grid:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkShowInGrid" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsEnableSorting" runat="server" Text="Is Enable Sorting:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkIsEnableSorting" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsUseInFilter" runat="server" Text="Is Use in Filter:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkIsUseInFilter" class="cssClassCheckBox" />
                                </td>
                            </tr>--%>
                            <%--<tr>
                                <td>
                                    <asp:Label ID="lblShowInSearch" runat="server" Text="Use in Search:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkShowInSearch" class="cssClassCheckBox" />
                                </td>
                            </tr>--%>
                          <%--h  <tr>
                                <td>
                                    <asp:Label ID="lblUseInAdvancedSearch" runat="server" Text="Use in Advanced Search:"
                                        CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkUseInAdvancedSearch" class="cssClassCheckBox" />
                                </td>
                            </tr>--%>
                            <%--<tr>
                                                                            <td>
                                                                                <asp:Label ID="lblVisibleOnFrontend" runat="server" Text="Visible on Item View Page on Front-end:"
                                                                                    CssClass="cssClassLabel"></asp:Label>
                                                                            </td>
                                                                            <td class="cssClassTableRightCol">
                                                                                <div class="cssClassCheckBox">
                                                                                    <input type="checkbox" name="chkVisibleOnFrontend" />
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblUsedInItemListing" runat="server" Text="Used in Item Listing:"
                                                                                    CssClass="cssClassLabel"></asp:Label>
                                                                            </td>
                                                                            <td class="cssClassTableRightCol">
                                                                                <div class="cssClassCheckBox">
                                                                                    <input type="checkbox" name="chkUsedInItemListing" />
                                                                                </div>
                                                                            </td>
                                                                        </tr>--%>
                           <%-- h<tr>
                                <td>
                                    <asp:Label ID="lblComparable" runat="server" Text="Comparable on Front-end:" CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkComparable" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUseForPriceRule" runat="server" Text="Use for Price Rule Conditions:"
                                        CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkUseForPriceRule" class="cssClassCheckBox" />
                                </td>
                            </tr>--%>
                            <%--<tr>
                                <td>
                                    <asp:Label ID="lblUseForPromoRule" runat="server" Text="Use for Promo Rule Conditions:"
                                        CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkUseForPromoRule" class="cssClassCheckBox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUseForRating" runat="server" Text="Use for Rating Conditions:"
                                        CssClass="cssClassLabel"></asp:Label>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="checkbox" name="chkUseForRating" class="cssClassCheckBox" />
                                </td>
                            </tr>--%>
                       <%-- </table>
                    </div>
                </div>--%>
                <div id="fragment-3">
                    <div class="cssClassFormWrapper">
                        <h3>
                            <asp:Label ID="lblTab3Info" runat="server" Text="Cost Variants Settings"></asp:Label>
                        </h3>
                        <div class="cssClassGridWrapper">
                            <div class="cssClassGridWrapperContent cssClassPadding">
                                <table width="100%" cellspacing="0" cellpadding="0" id="tblVariantTable" class="tdpadding">
                                    <thead>
                                        <tr class="cssClassHeading">
                                            <th align="left">
                                                Pos.
                                            </th>
                                            <th align="left">
                                                Name
                                            </th>
                                            <th align="left">
                                                Modifier&nbsp;/Type
                                            </th>
                                            <th align="left">
                                                Weight modifier&nbsp;/&nbsp;Type
                                            </th>
                                            <th align="left">
                                                Status
                                            </th>
                                            <th align="left">
                                                &nbsp;
                                            </th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>
                                            <input type="hidden"  class="cssClassVariantValue" value="0"/>
                                            <input type="text" size="3" id="txtPos" class="cssClassDisplayOrder"/>
                                        </td>
                                        <td>
                                            <input type="text" class="cssClassVariantValueName"/>
                                        </td>
                                        <td>
                                            <input type="text" id="txtPriceModifier" size="5" class="cssClassPriceModifier"/>
                                            &nbsp;/&nbsp;
                                            <select class="cssClassPriceModifierType">
                                                <option value="false">$</option>
                                                <option value="true">%</option>
                                            </select>
                                        </td>
                                        <td>
                                            <input type="text" id="txtWeightModifier" size="5" class="cssClassWeightModifier"/>
                                            &nbsp;/&nbsp;
                                            <select class="cssClassWeightModifierType">
                                                <option value="false">lbs</option>
                                                <option value="true">%</option>
                                            </select>
                                        </td>
                                        <td>
                                            <select class="cssClassIsActive">
                                                <option value="true">Active</option>
                                                <option value="false">Disabled</option>
                                            </select>
                                        </td>
                                        <td>
                                            <span class="nowrap">
                                                <img width="13" height="18" border="0" align="top" class="cssClassAddRow" title="Add empty item"
                                                  src="" alt="Add empty item" name="add" />&nbsp;
                                                <img width="13" height="18" border="0" align="top" class="cssClassCloneRow" src="" alt="Clone this item"
                                                    title="Clone this item" name="clone" />&nbsp;
                                                <img width="12" height="18" border="0" align="top" class="cssClassDeleteRow" src="" alt="Remove this item"
                                                    name="remove" />&nbsp;
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnBack">
                    <span><span>Back</span></span>
                </button>
            </p>
            <p>
                <button type="button" id="btnReset">
                <span><span>Reset</span></span> </button>
            </p>
            <p>
                <button type="button" id="btnSaveVariantOption" >
                <span><span>Save</span></span> </button>
            </p>
            <p>
                <button type="button" class="delbutton">
                <span><span>Delete</span></span> </button>
            </p>
        </div>
    </div>
</div>
<!-- End form -->
