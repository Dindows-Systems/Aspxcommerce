<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_RecoverPassword.ascx.cs"
    Inherits="SageFrame.Modules.PasswordRecovery.ctl_RecoverPassword" %>
<div class="cssClassForgetPasswordPage" runat="server" id="divRecoverpwd">
    <div class="cssClassForgetPasswordPageLeft">
        <asp:Wizard ID="wzdPasswordRecover" runat="server" DisplaySideBar="false" ActiveStepIndex="0"
            DisplayCancelButton="True" CancelButtonType="Button" OnCancelButtonClick="CancelButton_Click"  StartNextButtonType="Button"
            StepNextButtonType="Button" StepPreviousButtonType="Button" FinishPreviousButtonType="Button"
            FinishCompleteButtonType="Button" OnNextButtonClick="wzdPasswordRecover_NextButtonClick"
            OnFinishButtonClick="wzdPasswordRecover_FinishButtonClick" Width="100%">
            <FinishNavigationTemplate>
                <div class="cssClassButtonWrapper">
                    <asp:Button ID="FinishButton" runat="server" AlternateText="Finish" BackColor="Transparent"
                        BorderStyle="None" CommandName="MoveComplete" CssClass="cssClassButton" Text="Finish" />
                </div>
            </FinishNavigationTemplate>
            <StartNavigationTemplate>
                <div class="cssClassButtonWrapper">
                    <asp:Button ID="StartNextButton" runat="server" AlternateText="Next" BackColor="Transparent"
                        BorderStyle="None" CommandName="MoveNext" CssClass="cssClassButton" ValidationGroup="vdgRecoveredPassword"
                        Text="Next" />
                    <asp:Button ID="CancelButton" runat="server" AlternateText="Cancel" BackColor="Transparent"
                        BorderStyle="None" CommandName="Cancel" CssClass="cssClassButton" Text="Cancel" />
                </div>
            </StartNavigationTemplate>
            <StepNavigationTemplate>
                <div class="cssClassButtonWrapper">
                    <asp:Button ID="StepNextButton" runat="server" AlternateText="Next" BackColor="Transparent"
                        BorderStyle="None" CommandName="MoveNext" CssClass="cssClassButton" ValidationGroup="vdgRecoveredPassword"
                        Text="Next" />
                </div>
            </StepNavigationTemplate>
            <WizardSteps>
                <asp:WizardStep ID="WizardStep1" runat="server" Title="Setting New Password">
                    <%= helpTemplate %>
                    <div class="cssClassFormWrapper">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text=":" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hdnRecoveryCode" runat="server" />
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="cssClassNormalTextBox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRecoveredPassword" runat="server" ControlToValidate="txtPassword"
                                        ValidationGroup="vdgRecoveredPassword" ErrorMessage="*" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblRetypePassword" runat="server" Text="Retype Password" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text=":" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRetypePassword" runat="server" TextMode="Password" CssClass="cssClassNormalTextBox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                                        ValidationGroup="vdgRecoveredPassword" ErrorMessage="*" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvPassword" runat="server" ErrorMessage="*" CssClass="cssClasssNormalRed"
                                        ControlToCompare="txtPassword" ControlToValidate="txtRetypePassword" ValidationGroup="vdgRecoveredPassword"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vdgRecoveredPassword" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep2" runat="server" Title="Finished Template">
                    <asp:Literal ID="litPasswordChangedSuccessful" runat="server"></asp:Literal>
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
    </div>
</div>
