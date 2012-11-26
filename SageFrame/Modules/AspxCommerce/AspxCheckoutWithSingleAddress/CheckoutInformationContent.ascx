<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckoutInformationContent.ascx.cs"
    Inherits="Modules_AspxCheckoutInformationContent_CheckoutInformationContent" %>

<script type="text/javascript" >
    //<![CDATA[
    var IsFShipping = '<%=IsFShipping %>';
    var myAccountURL = '<%=myAccountURL%>';
    var Discount = '<%=Discount %>';
    var CouponCode = '<%=CouponCode %>';
    var noImageCheckOutInfoPath = '<%=noImageCheckOutInfoPath %>';  
    //]]>
</script>

<div id="SingleCheckOut">
</div>
<div class="cssClassCheckout">
    <div class="cssClassAccordionWrapper">
        <div id="accordion" class="accordion">
            <div class="accordionHeading">
                <h2>
                    <span>1</span><b>Checkout Method</b></h2>
            </div>
            <div class="cssClassFormWrapper">
                <div class="cssClassCheckOutMethod">
                    <div class="cssClassCheckOutMethodLeft">
                        <p>
                            Checkout as a <b>Guest</b> or <b>Register</b> with us for future convenience:</p>
                        <p class="cssClassPadding">
                            <input id="rdbGuest" type="radio" class="cssClassRadioBtn" name="guestOrRegister" />
                            <label id="lblguest">
                                <b>Checkout as Guest</b></label>
                            <br />
                            <input id="rdbRegister" type="radio" class="cssClassRadioBtn" name="guestOrRegister" />
                            <label>
                                <b>Registered User</b></label>
                        </p>
                     <br />
                        <p>
                            <span class="cssClassRegisterlnk">Register</span> with us for future convenience.<br />
                        </p>
                        <p class="cssClassSmallFont">
                            - Fast and easy check out<br />
                            - Easy access to your order history and status<br />
                            - To Track your Digital Purchase
                        </p>
                        <div class="cssClassButtonWrapper ">
                            <button id="btnCheckOutMethodContinue" type="button">
                                <span><span>Continue</span></span></button>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="udpLogin" runat="server">
                    <ContentTemplate>
                        <div id="dvLogin" class="cssClassCheckOutMethodRight" style="display:none">
                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="View1" runat="server">
                                    <div class="cssClassloginbox">
                                        <div class="cssClassloginboxInside">
                                            <div class="cssClassloginboxInsideDetails">
                                                <div class="cssClassLoginLeftBox">
                                                    <div class="cssClassadminloginHeading">
                                                        <h1>
                                                            <asp:Label ID="lblAdminLogin" runat="server" Text="Login" meta:resourcekey="lblAdminLoginResource1"></asp:Label>
                                                        </h1>
                                                    </div>
                                                    <div class="cssClassadminloginInfo">
                                                        <table border="0" cellpadding="0" width="100%" class="cssClassnormalborder">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="cssClassNormalText"
                                                                        meta:resourcekey="UserNameLabelResource1">User Name:</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <p class="cssClassTextBox">
                                                                        <asp:TextBox ID="UserName" runat="server" meta:resourcekey="UserNameResource1"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                            ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1"
                                                                            CssClass="cssClassusernotfound" meta:resourcekey="UserNameRequiredResource1">*</asp:RequiredFieldValidator>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="PasswordAspx" CssClass="cssClassNormalText"
                                                                        meta:resourcekey="PasswordLabelResource1">Password:</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <p class="cssClassTextBox">
                                                                        <asp:TextBox ID="PasswordAspx" runat="server" TextMode="Password" meta:resourcekey="PasswordResource1"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="PasswordAspx"
                                                                            ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1"
                                                                            CssClass="cssClassusernotfound" meta:resourcekey="PasswordRequiredResource1">*</asp:RequiredFieldValidator>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="118">
                                                                    <table width="118" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td width="18">
                                                                                <asp:CheckBox ID="RememberMe" runat="server" CssClass="cssClassCheckBox" meta:resourcekey="RememberMeResource1" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblrmnt" runat="server" Text="Remember me." CssClass="cssClassRemember"
                                                                                    meta:resourcekey="lblrmntResource1"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <span class="cssClassForgetPass">
                                                                                <asp:HyperLink ID="hypForgetPassword" runat="server" Text="Forgot Password?" meta:resourcekey="hypForgetPasswordResource1"></asp:HyperLink>
                                                                            </span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="120">
                                                                                <div class="cssClassButtonWrapper">
                                                                                    <span><span>
                                                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Sign In" ValidationGroup="Login1"
                                                                            OnClick="LoginButton_Click" meta:resourcekey="LoginButtonResource1" />
                                                                    </span></span>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="cssClassusernotfound">
                                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False" meta:resourcekey="FailureTextResource1"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="cssClassLoginRighttBox" runat="server" id="divSignUp">
                                                    <h2>
                                                        <span>New here?</span>
                                                    </h2>
                                                    <p>
                                                        <a href="/User-Registration.aspx" runat="server" id="signup">Sign up</a> for a new
                                                        account</p>
                                                    <div class="cssClassNewSIgnUp">
                                                        <span>»</span><a href="/User-Registration.aspx" runat="server" id="signup1">Sign up</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                </asp:View>
                            </asp:MultiView>
                        </div>
                        <div class="cssClassclear">
                        </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="accordionHeading">
                <h2>
                    <span>2</span><b>Billing Information</b></h2>
            </div>
            <div id="dvBilling" class="cssClassCheckoutInformationContent" style="display:none">
                <div id="dvBillingInfo" class="cssClassCheckoutLeftBox">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                <tr>
                    <td>
                        <asp:Label ID="lblFirstName" runat="server" Text="FirstName:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td >
                        <input type="text" id="txtFirstName" name="FirstName" class="required"  maxlength="40"/>
                    </td>
                    <td>
                        <asp:Label ID="lblLastName" runat="server" Text="LastName:" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtLastName" name="LastName" class="required" maxlength="40" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtEmailAddress" name="Email" class="required email"/>
                    </td>
                     <td>
                        <asp:Label ID="lblCompany" Text="Company:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtCompanyName" maxlength="40" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAddress1" Text="Address1:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtAddress1" name="Address1" class="required" maxlength="250" />
                    </td>
                     <td>
                        <asp:Label ID="lblAddress2" Text="Address2:" runat="server" CssClass="cssClassLabel" ></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtAddress2" maxlength="250" name="Address2"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCountry" Text="Country:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <select id="ddlBLCountry" class="cssClassDropDown">
                            <option></option>
                        </select>
                    </td>
                   
                     <td>
                        <asp:Label ID="lblState" Text="State/Province:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtState" name="stateprovince" class="required" maxlength="250" />
                         <select id="ddlBLState" class="cssClassDropDown">
                             <option></option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblZip" Text="Zip/Postal Code:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtZip" name="biZip" class="required number" maxlength="10"/>
                    </td>
                    <td>
                        <asp:Label ID="lblCity" Text="City:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtCity" name="City" class="required" maxlength="250"  />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPhone" Text="Phone:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtPhone" name="Phone" class="required number" maxlength="20" />
                    </td>
                    <td>
                        <asp:Label ID="lblMobile" Text="Mobile:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtMobile"  class="number" name="mobile" maxlength="20" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFax" Text="Fax:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtFax" name="Fax" class="number" maxlength="20" />
                    </td>
                    <td>
                        <asp:Label ID="lblWebsite" Text="Website:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtWebsite" class="url" maxlength="50"  />
                    </td>
                </tr>
                <tr id="trShippingAddress">
                    <td>
                        <input type="checkbox" id="chkShippingAddress" />
                    </td>
                    <td>
                        <asp:Label ID="lblDefaultShipping" Text=" Use as Default Shipping Address" runat="server"
                            CssClass="cssClassLabel"></asp:Label>
                    </td>
                </tr>
                <tr id="trBillingAddress">
                    <td>
                        <input type="checkbox" id="chkBillingAddress" />
                    </td>
                    <td>
                        <asp:Label ID="lblDefaultBilling" Text="Use as Default Billing Address" runat="server"
                            CssClass="cssClassLabel"></asp:Label>
                    </td>
                </tr>
               
            </tbody>
                    </table>
                    <input type="hidden" id="hdnAddressID" />
                    
                </div>
                <div id="dvBillingSelect">
                    <label>
                        Billing Address : <span class="cssClassRequired">*</span></label>
                    <select id="ddlBilling">
                        <option></option>
                    </select>
                    <div class="cssClassButtonWrapper cssClassRightBtn">
                        <button id="addBillingAddress" type="button" value="Add Billing Address">
                            <span><span>Add Billing Address</span></span></button>
                    </div>
                </div>
                <p class="cssClassCheckBox">
                    <input id="chkBillingAsShipping" type="checkbox" /><span> Use Billing Address As Shipping
                        Address</span>
                </p>
                <div class="cssClassButtonWrapper cssClassRightBtn">
                    <button id="btnBillingBack" type="button" value="back" class="back">
                        <span><span>Back</span></span></button>
                    <button id="btnBillingContinue" type="button" value="next" class="next">
                        <span><span>Continue</span></span></button>
                </div>
                <div class="cssClassClear">
                </div>
            </div>
            <div class="accordionHeading">
                <h2>
                    <span>3</span><b>Shipping Information</b></h2>
            </div>
            <div id="dvShipping" class="cssClassCheckoutInformationContent" style="display:none">
                <div id="dvShippingInfo" class="cssClassCheckoutLeftBox">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                <tr>
                    <td>
                        <asp:Label ID="lblSPFirstName" runat="server" Text="FirstName" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td >
                           <input id="txtSPFirstName" name="spFName" type="text" class="required" maxlength="40" />
                    </td>
                    <td>
                        <asp:Label ID="lblSPLastName" runat="server" Text="LastName:" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                          <input id="txtSPLastName" name="spLName" type="text" class="required" maxlength="40" />
                    </td>
                </tr>
                <tr>  
                <td>
                        <asp:Label ID="lblSPEmail" runat="server" Text="Email:" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtSPEmailAddress" name="Email" class="required email"/>
                    </td>                
                     <td>
                        <asp:Label ID="lblSPCompany" Text="Company:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input id="txtSPCompany" type="text" maxlength="50" name="SPCompany" />
                    </td>
                   
                </tr>
                <tr> 
                 <td>
                        <asp:Label ID="lblSPAddress1" Text="Address1:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                            <input id="txtSPAddress" name="spAddress1" type="text" class="required"  maxlength="250" />
                    </td>                   
                     <td>
                        <asp:Label ID="lblSPAddress2" Text="Address2:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtSPAddress2" maxlength="250" name="SPAddress2" />
                    </td>
                    
                </tr>
                <tr>
                      <td>
                        <asp:Label ID="lblSPCountry" Text="Country:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <select id="ddlSPCountry">
                            <option></option>                           
                        </select>
                    </td>                 
                     <td>
                        <asp:Label ID="lblSPState" Text="State/Province:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                         <input type="text" id="txtSPState" name="spstateprovince" class="required" maxlength="250"  /> 
                          <select id="ddlSPState" class="cssClassDropDown">
                              <option></option>
                        </select>
                    </td>
                   
                </tr>
                <tr>
                  
                   <td>
                        <asp:Label ID="lblSPCity" Text="City:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtSPCity" name="City" class="required" maxlength="250" />
                    </td>  <td>
                        <asp:Label ID="lblSPZip" Text="Zip/Postal Code:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                         <input id="txtSPZip" name="spZip" type="text" class="required number" maxlength="10" />
                    </td> 
                                      
                </tr>
                <tr>
                   <td>
                        <asp:Label ID="lblSPPhone" Text="Phone:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                          <input id="txtSPPhone" name="spPhone" type="text" class="required number"  maxlength="20" />
                    </td>  
                    <td>
                        <asp:Label ID="lblSPMobile" Text="Mobile:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtSPMobile" name="spmobile"   class="number" maxlength="20" />
                    </td>                       
                </tr>                  
          
            </tbody>
                    </table>
                    <p class="cssClassRequired">
                        * required</p>
                </div>
                <div id="dvShippingSelect">
                    <label>
                        Shipping Address : <span class="cssClassRequired">*</span></label>
                    <select id="ddlShipping">
                        <option></option>
                    </select>
                    <div class="cssClassButtonWrapper cssClassRightBtn">
                        <button id="addShippingAddress" type="button" value="Add Shipping Address">
                            <span><span>Add Shipping Address</span></span></button>
                    </div>
                </div>
                <div class="cssClassClear">
                </div>
                    <div class="cssClassButtonWrapper cssClassRightBtn">
                        <button id="btnShippingBack" type="button" value="back" class="back">
                            <span><span>Back</span></span></button>
                        <button id="btnShippingContinue" type="button" value="continue" class="continue">
                            <span><span>Continue</span></span></button>
                    </div>
                    <div class="cssClassClear">
                    </div>
            </div>
            <div class="accordionHeading">
                <h2>
                    <span>4</span><b>Shipping Method </b>
                </h2>
            </div>
            <div id="dvPaymentsMethod" class="cssClassButtonWrapper" style="display:none">
                <div id="divShippingMethod" class="cssClassShippingMethodInfo cssClassCartInformation">
                </div>
                <div class="cssClassButtonWrapper cssClassRightBtn">
                    <button id="btnShippingMethodBack" type="button" value="back" class="back">
                        <span><span>Back</span></span></button>
                    <button id="btnShippingMethodContinue" type="button" value="continue" class="continue">
                        <span><span>Continue</span></span></button>
                </div>
                <div class="cssClassClear">
                </div>
            </div>
            <%--      <div class="accordionHeading">
                <h2>
                    <span>4</span><b>Shipping Method </b></h2>
            </div>
            <div id="dvPaymentsMethod" class="cssClassButtonWrapper">
            <div id="divPaymentMethods">
                            
                        
                </div>
				<div id="divPaymentSubTypes" class="cssClassButtonWrapper"></div>
				 <div class="cssClassButtonWrapper cssClassRightBtn">
                
                 <button id="btnPaymentGatewayTypeBack"  type="button" value="back" class="back" ><span><span>Back</span></span></button>
                        <button id="btnPaymentGatewayTypeContinue"  type="button" value="continue" class="continue" ><span><span>Continue</span></span></button>
                    
                        </div>
				
                <div class="cssClassClear">
                </div>
            </div>--%>
            <div class="accordionHeading">
                <h2>
                    <span>5</span><b>Payment Information</b></h2>
            </div>
            <div id="dvPaymentInfo" class="cssClassPaymentMethods" style="display:none">
                <div id="dvPGList">
                </div>

                <div class="cssClassButtonWrapper cssClassRightBtn">
                    <button id="btnPaymentInfoBack" type="button" value="back" class="back">
                        <span><span>Back</span></span></button>
                    <button id="btnPaymentInfoContinue" type="button" value="continue" class="continue">
                        <span><span>Continue</span></span></button>
                </div>
                <div class="cssClassClear">
                </div>
            </div>
            <div class="accordionHeading">
                <h2>
                    <span>6</span><b>Order Review </b>
                </h2>
            </div>
            <div id="dvPlaceOrder" class="cssClassOrderReview" style="display:none">
                <div class="cssClassCartInformationDetails" id="divCartDetails">
                </div>
                <table width="100%" class="noborder">
                    <tbody>
                        <tr class="cssClassSubTotalAmount">
                            <td>
                                <strong>Grand SUBTOTAL:</strong>
                            </td>
                            <td>
                                <input type="text" class="total-box cssClassFormatCurrency" value="$0" id="product-subtotal" readonly="readonly" />
                            </td>
                        </tr>
                        <tr class="cssClassSubTotalAmount">
                            <td>
                                <strong>Shipping Cost:</strong>
                            </td>
                            <td>
                                <input type="text" class="cssClassFormatCurrency" id="txtShippingTotal" readonly="readonly" value="0.00" />
                            </td>
                        </tr>
                        <tr class="cssClassSubTotalAmount">
                            <td>
                                <strong>Total Tax:</strong>
                            </td>
                            <td>
                                <input type="text" class="tax-box cssClassFormatCurrency" id="txtTax" readonly="readonly" value="0.00" />
                            </td>
                        </tr>
                        <tr class="cssClassSubTotalAmount">
                            <td>
                                <strong>Total Discount:</strong>
                            </td>
                            <td>
                                <input type="text" id="txtDiscountAmount" class="cssClassFormatCurrency" readonly="readonly" value="0.00" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="cssClassLeftRightBtn">
                    <div class="cssClassCartInformation">
                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="noborder">
                            <tbody>
                                <tr class="cssClassHeadeTitle cssClassAlternativeEven">
                                    <td class="cssClassSubTotalAmountWidth">
                                        <strong>Grand TOTAL:</strong>
                                    </td>
                                    <td class="cssClassGrandTotalAmountWidth">
                                        <input type="text" readonly="readonly" id="txtTotalCost" class="cssClassFormatCurrency" value="0" />
                                    </td>
                                </tr>
                             </tbody>
                        </table>
                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="noborder">
                         <tr><td >  <strong>Additional Note:</strong></td><td>
                                <textarea id="txtAdditionalNote" class="cssClassTextarea" rows="3" cols="90"></textarea></td></tr>
                        </table>
                    </div>
                    <div class="cssClassButtonWrapper ">
                        <button id="btnPlaceBack" type="button" value="back" class="back">
                            <span><span>Back</span></span></button>
                        <%--<button id="btnPlaceOrder" type="submit" class ="submit" ><span><span>Place Order</span></span></button>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="cssClassRightAccordainMenu">
    <div class="cssClassRightAccordainTab">
        <h1>
            Checkout progress</h1>
        <div class="cssClassRightAccordainMenuInfo">
            <h2>
                Billing Address</h2>
        </div>
        <div id="dvCPBilling" >
        </div>
        <div class="cssClassRightAccordainMenuInfo">
            <h2>
                Shipping Address</h2>
        </div>
        <div id="dvCPShipping">
        </div>
        <div class="cssClassRightAccordainMenuInfo">
            <h2>
                Shipping Method</h2>
        </div>
        <div id="dvCPShippingMethod">
        </div>
        <div class="cssClassRightAccordainMenuInfoSelected">
            <h2>
                Payment Method</h2>
        </div>
        <div id="dvCPPaymentMethod">
        </div>
        <%--<div class="cssClassRightAccordainMenuInfoSelected">
            <h2>
                Payment Gateway Type</h2>
        </div>
        <div id="dvPaymentGatewayTypeMethod">
        </div>--%>
    </div>
</div>
<div class="popupbox" id="popuprel">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span>Close</span></button>
    </div>
    <h2>
        <asp:Label ID="lblAddressTitle" runat="server" Text="Address Details"></asp:Label>
    </h2>
    <div class="cssClassFormWrapper">
        <div class="cssClassButtonWrapper">
            <button type="button" id="btnSubmitAddress" class="cssClassButtonSubmit">
                <span><span>Save</span></span></button>
        </div>
    </div>
</div>
