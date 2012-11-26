<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CatalogPriceRule.ascx.cs"
    Inherits="Modules_AspxCatalogPricingRule_CatalogPriceRule" %>

<script type="text/javascript">
//<![CDATA[

  //]]>
</script>

<div class="cssClassTabMenu" id="pricingRuleTabMenu" style="display:none">
    <div id="divCatalogPricingRuleForm" class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCatalogPricingRuleFormHeading" runat="server" Text="Catalog Pricing Rule"></asp:Label>
            </h2>
        </div>        
        <div id="placeholder-templates">
        </div>
        <div class="cssClassTabPanelTable">
            <div id="CatalogPriceRule-TabContainer" class="cssClassTabpanelContent">
                <ul>
                    <li><a href="#CatalogPriceRule-1"><span id="lblRuleInformation">Rule Information</span>
                    </a></li>
                    <li class="ui-tabs"><a href="#CatalogPriceRule-2"><span id="lblCondition">Condition</span>
                    </a></li>
                    <li><a href="#CatalogPriceRule-3"><span id="lblAction">Action</span></a></li>
                </ul>
                <div id="CatalogPriceRule-1" class="cssClassFormWrapper">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <span id="CatalogPriceRule-lblName" class="cssClassLabel">Rule Name:</span>
                            </td>
                            <td>
                                <input type="text" id="CatalogPriceRule-txtName" name="RuleName" class="cssClassNormalTextBox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CatalogPriceRule-lblDescription" class="cssClassLabel">Description:</span>
                            </td>
                            <td>
                                <textarea id="CatalogPriceRule-txtDescription" rows="2" cols="80" name="Description" class="cssClassTextArea"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CatalogPriceRule-lblRoles" class="cssClassLabel">Roles:</span>
                            </td>
                            <td>
                                <select id="CatalogPriceRule-mulRoles" multiple="multiple" name="Roles" class="cssClassMultiSelect required">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CatalogPriceRule-lblFromDate" class="cssClassLabel">From Date:</span>
                            </td>
                            <td>
                                <input type="text" id="CatalogPriceRule-txtFromDate" name="FromDate" class="from cssClassNormalTextBox required" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CatalogPriceRule-lblToDate" class="cssClassLabel">To Date:</span>
                            </td>
                            <td>
                                <input type="text" id="CatalogPriceRule-txtToDate" name="ToDate" class="to cssClassNormalTextBox required" />
    							<span id ="created" style="color:#ED1C24;"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CatalogPriceRule-lblPriority" class="cssClassLabel">Priority:</span>
                            </td>
                            <td>
                                <input type="text" id="CatalogPriceRule-txtPriority" name="name="Priority" class="cssClassNormalTextBox required" maxlength="2"/>
                                <span id="priority" style="color:Red;"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="CatalogPriceRule-lblIsActive" class="cssClassLabel">Is Active:</span>
                            </td>
                            <td>
                                <input type="checkbox" id="CatalogPriceRule-chkIsActive" class="cssClassCheckBox" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="CatalogPriceRule-2">
                    <div class="cssClassFieldSet">
                        <h2>
                            Conditions (leave blank for all products)</h2>
                        <div class="cssClassFieldSetContent">
                            <span class="cssClassOnClick">IF
                                <input type="hidden" name="type_0" id="type_0" title="type" value="combination" />
                                <input type="hidden" name="pricingRuleID" id="pricingRuleID" value="0" />
                                <a class="cssClassFieldSetLabel" href="#" onclick="CatalogPricingRule.Edit(this)">ALL</a> <span class="cssClassElement">
                                    <select name="aggregator_0" id="aggregator_0" class="element-value-changer select"
                                        onblur="CatalogPricingRule.GetDropdownValue(this)" onchange="CatalogPricingRule.GetDropdownValue(this)">
                                        <option value="ALL" selected="selected">ALL</option>
                                        <option value="ANY">ANY</option>
                                    </select>
                                </span></span>&nbsp; of these conditions are <span class="cssClassOnClick"><a class="cssClassFieldSetLabel"
                                    onclick="CatalogPricingRule.Edit(this)">TRUE</a><span class="cssClassElement">
                                        <select name="value_0" id="value_0" title="value" class="element-value-changer select"
                                            onblur="CatalogPricingRule.GetDropdownValue(this)" onchange="CatalogPricingRule.GetDropdownValue(this)">
                                            <option value="TRUE" selected="selected">TRUE</option>
                                            <option value="FALSE">FALSE</option>
                                        </select>
                                    </span></span>&nbsp;
                            <ul class="cssClassOnClickChildren" id="1">
                            </ul>
                        </div>
                    </div>
                </div>
                <div id="CatalogPriceRule-3" class="cssClassFormWrapper">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="CatalogPriceRule-lblApply" class="cssClassLabel">Apply:</span>
                            </td>
                            <td>
                                <select id="CatalogPriceRule-cboApply" class="cssClassDropDown">
                                    <option value="1">By Percentage of the Original Price</option>
                                    <option value="2">By Fixed Amount</option>
                                    <option value="3">To Percentage of the Original Price</option>
                                    <option value="4">To Fixed Amount</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="CatalogPriceRule-lblValue" class="cssClassLabel">Value:</span>
                            </td>
                            <td>
                                <input type="text" id="CatalogPriceRule-txtValue" name="Value" class="cssClassNormalTextBox required"/>
                             <span id="percError" style="color:#ED1C24;" ></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssClassTableLeftCol">
                                <span id="Span3" class="cssClassLabel">Further Rule Processing:</span>
                            </td>
                            <td>
                                <input type="checkbox" id="CatalogPriceRule-chkFurtherRuleProcessing" class="cssClassCheckBox" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="cssClassButtonWrapper">
                <p>
                    <button type="submit" id="btnSavePricingRule">
                        <span>Save</span></button>
                </p>
                <p>
                    <button type="button" onclick="CatalogPricingRule.CancelPricingRule()">
                        <span>Cancel</span></button>
                </p>
                <p>
                    <button type="button" onclick="CatalogPricingRule.ResetPricingRule()" id="resetPricngRule">
                        <span>Reset</span></button>
                </p>
            </div>
        </div>
    </div>
</div>
<div id="pricingRuleGrid" class="cssClassCommonBox Curve">
    <div class="cssClassHeader">
        <h2>
            <asp:Label ID="lblPricingRuleGridHeading" runat="server" Text="Catalog Price Rules"></asp:Label>
        </h2>
        <div class="cssClassHeaderRight">
            <div class="cssClassButtonWrapper">
                <p>
                    <button type="button" id="btnDeleteCatRules">
                        <span><span>Delete All Selected</span></span></button>
                </p>
                <p>
                    <button type="button" id="btnAddNewCatRule">
                        <span><span>Add Pricing Rule</span></span></button>
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
                            <input type="text" id="txtCatalogPriceRuleSrc" class="cssClassTextBoxSmall" />
                        </td>
                        <td>
                            <label class="cssClassLabel">
                                From Date:</label>
                            <input type="text" id="txtPricingRuleStartDate" class="cssClassTextBoxSmall" />
                        </td>
                        <td>
                            <label class="cssClassLabel">
                                To Date:</label>
                            <input type="text" id="txtPricingRuleEndDate" class="cssClassTextBoxSmall" />
                        </td>
                        <td>
                            <label class="cssClassLabel">
                                Status:</label>
                            <select id="ddlPricingRuleIsActive">
                                <option value="">-- All --</option>
                                <option value="True">Active</option>
                                <option value="False">Inactive</option>
                            </select>
                        </td>
                        <td>
                            <div class="cssClassButtonWrapper cssClassPaddingNone">
                                <p>
                                    <button type="button" onclick="CatalogPricingRule.SearchPricingRule()">
                                        <span><span>Search</span></span></button>
                                </p>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="loading">
                <img id="ajaxCatalogPriceImageLoad" src=""  alt="loading...."/>
            </div>
            <div class="log">
            </div>
            <table id="gdvCatalogPricingRules" width="100%" border="0" cellpadding="0" cellspacing="0">
            </table>
        </div>
    </div>
</div>
