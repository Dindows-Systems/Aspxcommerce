<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CartPriceRule.ascx.cs"
    Inherits="Modules_AspxCartPricingRule_CartPriceRule" %>

<script type="text/javascript">
//<![CDATA[       

    //]]>
</script>

<div class="cssClassTabMenu" id="cartPricingRuleTabMenu" style="display:none">
    <div id="divCartPricingRuleForm" class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCartPricingRuleFormHeading" runat="server" Text="Shopping Cart Price Rules"></asp:Label>
            </h2>
        </div>        
        <div id="placeholder-templates">
        </div>
        <div class="cssClassTabPanelTable">
            <div id="CartPriceRule-TabContainer" class="cssClassTabpanelContent">
                <ul>
                    <li><a href="#CartPriceRule-1"><span id="lblRuleInformation">Cart Rule Information</span>
                    </a></li>
                    <li class="ui-tabs"><a href="#CartPriceRule-2"><span id="lblCondition">Condition</span>
                    </a></li>
                    <li><a href="#CartPriceRule-3"><span id="lblAction">Action</span></a></li>
                </ul>
                <div id="CartPriceRule-1" class="cssClassFormWrapper">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblName" class="cssClassLabel">Rule Name:</span>
                            </td>
                            <td class="cssClassTableRightCol">
                                <input type="text" id="CartPriceRule-txtName" name="RuleName" class="cssClassNormalTextBox  required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblDescription" class="cssClassLabel">Description:</span>
                            </td>
                            <td>
                                <textarea id="CartPriceRule-txtDescription" rows="4" cols="80" name="Description"
                                    class="cssClassTextArea"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblStores" class="cssClassLabel">Stores:</span>
                            </td>
                            <td>
                                <select id="CartPriceRule-mulStores" multiple="multiple" name="Stores" class="cssClassMultiSelect required">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblRoles" class="cssClassLabel">Roles:</span>
                            </td>
                            <td>
                                <select id="CartPriceRule-mulRoles" multiple="multiple" name="Roles" class="cssClassMultiSelect required">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblFromDate" class="cssClassLabel">From Date:</span>
                            </td>
                            <td>
                                <input type="text" id="CartPriceRule-txtFromDate" name="FromDate" class="from cssClassNormalTextBox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblToDate" class="cssClassLabel">To Date:</span>
                            </td>
                            <td>
                                <input type="text" id="CartPriceRule-txtToDate" name="ToDate" class="to cssClassNormalTextBox required" />
                                <span id ="created" style="color:#ED1C24;"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblPriority" class="cssClassLabel">Priority:</span>
                            </td>
                            <td>
                                <input type="text" id="CartPriceRule-txtPriority" name="Priority" class="cssClassNormalTextBox required" maxlength="2"/>
                                <span id="spanPriority" style="color:Red"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CartPriceRule-lblIsActive" class="cssClassLabel">Is Active:</span>
                            </td>
                            <td>
                                <input type="checkbox" id="CartPriceRule-chkIsActive" class="cssClassCheckBox" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="CartPriceRule-2">
                    <div class="cssClassFieldSet">
                        <h2>
                            Conditions (leave blank for all products)</h2>
                        <div class="cssClassFieldSetContent">
                            <span class="cssClassOnClick">IF
                                <input type="hidden" name="type_0" id="type_0" title="type" value="combination" />
                                <input type="hidden" name="pricingRuleID" id="pricingRuleID" value="0" />
                                <a class="cssClassFieldSetLabel" href="#" onclick="cartPriceRuleFormat.Edit(this)">ALL</a> <span class="cssClassElement">
                                    <select name="aggregator_0" id="aggregator_0" class="element-value-changer select"
                                        onblur="cartPriceRuleFormat.GetDropdownValue(this)" onchange="cartPriceRuleFormat.GetDropdownValue(this)">
                                        <option value="ALL" selected="selected">ALL</option>
                                        <option value="ANY">ANY</option>
                                    </select>
                                </span></span>&nbsp; of these conditions are <span class="cssClassOnClick"><a class="cssClassFieldSetLabel"
                                    onclick="cartPriceRuleFormat.Edit(this)">TRUE</a><span class="cssClassElement">
                                        <select name="value_0" id="value_0" title="value" class="element-value-changer select"
                                            onblur="cartPriceRuleFormat.GetDropdownValue(this)" onchange="cartPriceRuleFormat.GetDropdownValue(this)">
                                            <option value="TRUE" selected="selected">TRUE</option>
                                            <option value="FALSE">FALSE</option>
                                        </select>
                                    </span></span>&nbsp;
                            <ul class="cssClassOnClickChildren" id="">
                            </ul>
                        </div>
                    </div>
                </div>
                <div id="CartPriceRule-3" class="cssClassFormWrapper">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tdbordernone">
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblApply" class="cssClassLabel">Apply:</span>
                            </td>
                            <td class="cssClassTableRightCol">
                                <select id="CartPriceRule-cboApply" class="cssClassDropDown">
                                    <option value="1">Percent of product price discount</option>
                                    <option value="2">Fixed amount discount</option>
                                    <option value="3">Fixed amount discount for whole cart</option>
                                    <option value="4">Buy X get Y free (discount Qty[Y] is Value)</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblValue" class="cssClassLabel">Value:</span>
                            </td>
                            <td>
                                <input type="text" id="CartPriceRule-txtValue" name="Value" class="cssClassNormalTextBox required"/>
                           <span id="percError" style="color:#ED1C24;" ></span>
                             </td>
                        </tr>
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblDiscountQuantity" class="cssClassLabel">Maximum Qty Discount
                                    is Applied To:</span>
                            </td>
                            <td>
                                <input type="text" id="CartPriceRule-txtDiscountQuantity" name="MaximumQtyDiscountAppliedTo"
                                    class="cssClassNormalTextBox" />
                            </td>
                        </tr>
                        <tr style="display:none;" id="trBuyX" >
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblDiscountStep" class="cssClassLabel">Discount Qty Step (Buy
                                    X):</span>
                            </td>
                            <td>
                                <input type="text" id="CartPriceRule-txtDiscountStep" name="DiscountQtyStep" class="cssClassNormalTextBox" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblApplytoShippingAmount" class="cssClassLabel">Apply To Shipping
                                    Amount:</span>
                            </td>
                            <td>
                                <input type="checkbox" id="CartPriceRule-chkApplytoShippingAmount" class="cssClassCheckBox" />
                            </td>
                        </tr>
                        <tr style="display:none;" id="trApplytoShipping">
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblFreeShipping" class="cssClassLabel">Shipping Discount:</span>
                            </td>
                            <td>
                                <select id="CartPriceRule-cboFreeShipping" class="cssClassDropDown">
                                    <option value="0" selected="selected">By Percentage Off</option>
                                    <option value="1">By Fixed Amount</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="CartPriceRule-lblFurtherRuleProcessing" class="cssClassLabel">Further Rule
                                    Processing:</span>
                            </td>
                            <td>
                                <input type="checkbox" id="CartPriceRule-chkFurtherRuleProcessing" class="cssClassCheckBox" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="cssClassButtonWrapper">
                <p>
                    <button type="submit" id="btnSaveCartPricingRule">
                        <span>Save</span></button>
                </p>
                <p>
                    <button type="button" id="btnCancelCartPricingRule" >
                        <span>Cancel</span></button>
                </p>
                <p>
                    <button type="button" id="btnResetCartPricingRule" >
                        <span>Reset</span></button>
                </p>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
    </div>
</div>
<div id="cartPricingRuleGrid" class="cssClassCommonBox Curve">
    <div class="cssClassHeader">
        <h2>
            <asp:Label ID="lblCartPriceRulesGridHeading" runat="server" Text="Shopping Cart Price Rules"></asp:Label>
        </h2>
        <div class="cssClassHeaderRight">
            <div class="cssClassButtonWrapper">
                <p>
                    <button type="button" id="btnDeleteCartRules">
                        <span><span>Delete All Selected</span></span></button>
                </p>
                <p>
                    <button type="button" id="btnAddCartPricingRule" >
                        <span>Add Pricing Rule</span></button>
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
                                Rule Name:</label>
                            <input type="text" id="txtCartPriceRuleSrc" class="cssClassTextBoxSmall " class="cssClassTextBoxSmall" />
                        </td>
                        <td>
                            <label class="cssClassLabel">
                                From Date:</label>
                            <input type="text" id="txtCartPricingRuleStartDate" class="cssClassTextBoxSmall" />
                        </td>
                        <td>
                            <label class="cssClassLabel">
                                To Date:</label>
                            <input type="text" id="txtCartPricingRuleEndDate" class="cssClassTextBoxSmall" />
						</td>
                        <td>
                            <label class="cssClassLabel">
                                Status</label>
                            <select id="ddlCartPricingRuleIsActive">
                                <option value="">-- All -- </option>
                                <option value="True">Active </option>
                                <option value="False">Inactive </option>
                            </select>
                        </td>
                        <td class="cssClassNone">
                            <div class="cssClassButtonWrapper cssClassPaddingNone">
                                <p>
                                    <button type="button" onclick="cartPriceRuleFormat.SearchCartPricingRule()">
                                        <span>Search</span></button>
                                </p>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="loading">
                <img id="ajaxCartPriceImageLoad" src=""  alt="loading...." />
            </div>
            <div class="log">
            </div>
            <table id="gdvCartPricingRules" width="100%">
            </table>
        </div>
    </div>
</div>
