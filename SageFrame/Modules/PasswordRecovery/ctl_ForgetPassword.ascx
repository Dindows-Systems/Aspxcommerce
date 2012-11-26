<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_ForgetPassword.ascx.cs"
    Inherits="SageFrame.Modules.PasswordRecovery.ctl_ForgetPassword" %>

<div class="cssClassForgetPasswordPage" runat="server" id="divForgotPwd">
<div class="cssClassForgetPasswordPageLeft">
  <asp:Wizard ID="wzdForgetPassword" runat="server" DisplaySideBar="False" ActiveStepIndex="0"
            CellPadding="0" CellSpacing="0" DisplayCancelButton="True" CancelButtonType="Button"
            StartNextButtonType="Button" StepNextButtonType="Button" StepPreviousButtonType="Button"
            FinishPreviousButtonType="Button" FinishCompleteButtonType="Button" OnFinishButtonClick="wzdForgetPassword_FinishButtonClick"
            OnNextButtonClick="wzdForgetPassword_NextButtonClick">
    <StartNavigationTemplate>
      <div class="cssClassButtonWrapper">
        <asp:Button ID="StartNextButton" runat="server" AlternateText="Next" BackColor="Transparent"
                        BorderStyle="None" CausesValidation="True" CommandName="MoveNext" CssClass="cssClassButton"
                        Text="Next" ValidationGroup="vdgForgetPassword" />
        <asp:Button ID="CancelButton" runat="server" AlternateText="Cancel" BackColor="Transparent"
                        BorderStyle="None" CausesValidation="False" CommandName="Cancel" CssClass="cssClassButton"
                        Text="Cancel" OnClick="CancelButton_Click" />
      </div>
    </StartNavigationTemplate>
    <StepNavigationTemplate>
      <div class="cssClassButtonWrapper">
        <asp:Button ID="StepNextButton" runat="server" AlternateText="Next" BackColor="Transparent"
                        BorderStyle="None" CausesValidation="False" CommandName="MoveNext" CssClass="cssClassButton"
                        Text="Next" />
      </div>
    </StepNavigationTemplate>
    <FinishNavigationTemplate>
      <div class="cssClassButtonWrapper">
        <asp:Button ID="FinishButton" runat="server" AlternateText="Finish" BackColor="Transparent"
                        BorderStyle="None" CausesValidation="False" CommandName="MoveComplete" CssClass="cssClassButton"
                        Text="Finish" />
      </div>
    </FinishNavigationTemplate>
    <WizardSteps>
      <asp:WizardStep ID="WizardStep1" runat="server" Title="Prompt for Email Address"> 
        <p class="cssClassForgetYourPassWordTopInfo"><%= helpTemplate %></p>
        <div class="cssClassForgetPasswordInfo">
          <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
              <td width="75px"><asp:Label ID="lblUsername" runat="server" Text="User Name" CssClass="cssClassFormLabel"></asp:Label></td>
              <td width="20px" align="left"><asp:Label ID="Label3" runat="server" Text=":" CssClass="cssClassFormLabel"></asp:Label></td>
              <td><p class="cssClassNormalTextBox">
                  <asp:TextBox ID="txtUsername" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                  <asp:RequiredFieldValidator Display="Dynamic"
                                        ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ValidationGroup="vdgForgetPassword"
                                        ErrorMessage="*" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
                </p></td>
            </tr>
            <tr>
              <td><asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="cssClassFormLabel"></asp:Label></td>
              <td><asp:Label ID="Label1" runat="server" Text=":" CssClass="cssClassFormLabel"></asp:Label></td>
              <td><p class="cssClassNormalTextBox">
                  <asp:TextBox ID="txtEmail" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                  <asp:RequiredFieldValidator  Display="Dynamic"
                                        ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="vdgForgetPassword"
                                        ErrorMessage="*" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="revEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail"
                                        CssClass="cssClasssNormalRed" SetFocusOnError="true" ValidationGroup="vdgForgetPassword"
                                        ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </p></td>
            </tr>
          </table>
        </div>
      </asp:WizardStep>
      <asp:WizardStep ID="WizardStep2" runat="server" Title="Sending Email" StepType="Finish">
        <asp:Literal ID="litInfoEmailFinish" runat="server"></asp:Literal>
      </asp:WizardStep>
    </WizardSteps>
  </asp:Wizard>
  </div>
</div>
