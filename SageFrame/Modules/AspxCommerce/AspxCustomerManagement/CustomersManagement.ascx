<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomersManagement.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxCustomerManagement_CustomersManagement" %>

<script type="text/javascript">
    //<![CDATA[
    var checkIfSucccess = '<%=CheckIfSucccess %>';  
    //]]>
</script>
<div id="divCustomerList">
<div class="cssClassCommonBox Curve">
    <div class="cssClassHeader">
        <h2>
            <asp:Label ID="lblAttrGridHeading" runat="server" Text="Manage Customers"></asp:Label>
        </h2>
        <div class="cssClassHeaderRight">
            <div class="cssClassButtonWrapper">
                <p>
                    <button type="button" id="btnAddNewCustomer">
                        <span><span>Add New Customer</span></span>
                    </button>
                </p>
                <p>
                    <button type="button" id="btnDeleteSelectedCustomer">
                        <span><span>Delete All Selected</span> </span>
                    </button>
                </p>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
    <div class="cssClassGridWrapper">
        <div class="cssClassGridWrapperContent">
            <div class="loading">
                <img id="ajaxCutomerMgmtImageLoad" src="" alt="loading...." title="loading...."/>
            </div>
            <div class="log">
            </div>
            <table id="gdvCustomerDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
            </table>
        </div>
    </div>
    </div>
</div>

<div id="divAddNewCustomer" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCustomerHeading" runat="server" Text="Add New Customer"></asp:Label>
            </h2>
        </div>
<div class="cssClassUserRegistrationPage">
    <div class="cssClassUserRegistration" >
        <div class="cssClassFormWrapper">
        <div id="divRegistration" runat="server">
            <div class="cssClassRegistrationInformation">
                <%= headerTemplate %>
            </div>
            <span class="cssClassRequired">Fields marked with <b>*</b> are compulsory. </span>
            <div class="cssClassUserRegistrationInfoLeft">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <h3>
                                User Info</h3>
                        </td>
                    </tr>
                    <tr>
                        <td width="270">
                            <asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="FirstName" CssClass="cssClassFormLabel">First Name: </asp:Label><span
                                class="cssClassrequired">*</span>
                            <p class="cssClassRegisterInputBg">
                                <asp:TextBox ID="FirstName" runat="server" CssClass="cssClassNormalTextBox fristName"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="FirstName"
                                    ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
                            </p>
                        </td>
                        <td>
                            <asp:Label ID="LastNameLabel" runat="server" AssociatedControlID="LastName" CssClass="cssClassFormLabel">Last Name:</asp:Label><span
                                class="cssClassrequired">*</span>
                            <p class="cssClassRegisterInputBg">
                                <asp:TextBox ID="LastName" runat="server" CssClass="cssClassNormalTextBox lastName"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="LastName"
                                    ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" CssClass="cssClassFormLabel">E-mail:</asp:Label>
                            <span class="cssClassrequired">*</span>
                            <p class="cssClassRegisterInputBgBig">
                                <asp:TextBox ID="Email" runat="server" CssClass="cssClassNormalTextBox email"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmailRequired" runat="server" ControlToValidate="Email"
                                    ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="Email"
                                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="CreateUserWizard1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    CssClass="cssClasssNormalRed"></asp:RegularExpressionValidator>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <h3>
                                Create Login</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="cssClassFormLabel">User Name:</asp:Label><span
                                class="cssClassrequired">*</span>
                            <p class="cssClassRegisterInputBg">
                                <asp:TextBox ID="UserName" runat="server" CssClass="cssClassNormalTextBox userName"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUserNameRequired" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
                            </p>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="cssClassFormLabel">Password:</asp:Label><span
                                class="cssClassrequired">*</span>
                            <p class="cssClassRegisterInputBg">
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="cssClassNormalTestBox password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPasswordRequired" runat="server" ControlToValidate="Password"
                                    ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Password must be atleast of 4 character.."
                                    ControlToValidate="Password" ValidationGroup="CreateUserWizard1" ValidationExpression=".{4,}">*</asp:RegularExpressionValidator>
                            </p>
                        </td>
                        <td>
                            <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword"
                                CssClass="cssClassFormLabel">Confirm Password:</asp:Label><span class="cssClassrequired">*</span>
                            <p class="cssClassRegisterInputBg">
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="cssClassNormalTextBox confirmPassword"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                    ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvPasswordCompare" runat="server" ControlToCompare="Password"
                                    ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="*" ValidationGroup="CreateUserWizard1"
                                    CssClass="cssClasssNormalRed"></asp:CompareValidator>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question" CssClass="cssClassFormLabel">Security Question:</asp:Label><span
                                class="cssClassrequired">*</span>
                            <p class="cssClassRegisterInputBg">
                                <asp:TextBox ID="Question" runat="server" CssClass="cssClassNormalTextBox question"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvQuestionRequired" runat="server" ControlToValidate="Question"
                                    ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
                            </p>
                        </td>
                        <td>
                            <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer" CssClass="cssClassFormLabel">Security Answer:</asp:Label><span
                                class="cssClassrequired">*</span>
                            <p class="cssClassRegisterInputBg">
                                <asp:TextBox ID="Answer" runat="server" CssClass="cssClassNormalTextBox answer"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAnswerRequired" runat="server" ControlToValidate="Answer"
                                    ErrorMessage="*" ValidationGroup="CreateUserWizard1" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
                            </p>
                        </td>
                    </tr>
                 
                </table>
            </div>
            <br />
            <asp:CheckBox ID="chkIsSubscribeNewsLetter" runat="server" CssClass="cssClassCheckBox" />
             <asp:Label ID="lblIsSubscribeNewsLetter" runat="server" Text="Subscribe Newsletter" CssClass="cssClassFormLabel"></asp:Label>
           <%-- <asp:Label ID="lblIsSubscribeNewsLetter" runat="server" Text="Subscribe Newsletter:"
                AssociatedControlID="CaptchaValue" CssClass="cssClassFormLabel"></asp:Label>--%>
           
            <br />
            <div class="cssClassButtonWrapper">
             <p>
                    <asp:Button ID="FinishButton" runat="server" AlternateText="Finish" 
                        BorderStyle="None" CausesValidation="True" ValidationGroup="CreateUserWizard1"
                         CssClass="cssClassButtonSubmit" Text="Register" OnClick="FinishButton_Click" />
             </p>
                <p>
                    <button type="button" id="btnBack">
                        <span><span>Back</span></span>
                    </button>
                </p>
            </div>
            
            </div>
          <%--<div id="divRegConfirm" class="cssClassRegConfirm" runat="server">
                <h3>
                    Registration Successful</h3>
                <asp:Label ID="lblRegSuccess" runat="server" CssClass="cssClassFormLabel">
                 <asp:Literal ID="USER_RESISTER_SUCESSFUL_INFORMATION" runat="server" meta:resourcekey="USER_RESISTER_SUCESSFUL_INFORMATIONResource1"></asp:Literal>            
                 </asp:Label>
              <div class="cssClassButtonWrapper">                   
                    <span><a href='<%=LoginPath%>'>Go To Login Page</a></span>
                </div>
            </div>--%>
        </div>
    </div>
</div>
</div>
</div>
