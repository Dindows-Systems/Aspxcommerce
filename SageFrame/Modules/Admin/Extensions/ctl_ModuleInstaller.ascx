<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_ModuleInstaller.ascx.cs"
    Inherits="SageFrame.DesktopModules.Admin.Extensions.ctl_ModuleInstaller" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Data" %>
<div class="cssClassModuleInstaller">
    <asp:Wizard ID="wizInstall" runat="server" DisplaySideBar="False" 
        ActiveStepIndex="0" DisplayCancelButton="True" OnNextButtonClick="wizInstall_NextButtonClick"
        OnCancelButtonClick="wizInstall_CancelButtonClick" 
        OnFinishButtonClick="wizInstall_FinishButtonClick" 
        onactivestepchanged="wizInstall_ActiveStepChanged" 
        meta:resourcekey="wizInstallResource1">
        <%--<StepStyle VerticalAlign="Top" />
        <NavigationButtonStyle CssClass="CommandButton" BorderStyle="None" BackColor="Transparent" />--%>
        <FinishNavigationTemplate>
            <div class="cssClassButtonWrapper">
                <asp:Button ID="FinishButton" runat="server" AlternateText="Finish" BackColor="Transparent"
                    BorderStyle="None" CausesValidation="False" CommandName="MoveComplete" CssClass="cssClassButton"
                    Text="Finish" meta:resourcekey="FinishButtonResource1" />
            </div>
        </FinishNavigationTemplate>
        <HeaderTemplate>
            <asp:Label ID="lblTitle" CssClass="cssClassNormalTitle" runat="server" 
                Text="Install Module" meta:resourcekey="lblTitleResource1"></asp:Label>
            <asp:Label ID="lblHelp" CssClass="cssClassHelpTitle" runat="server" 
                Text="Help for Installing Module" meta:resourcekey="lblHelpResource1"></asp:Label>
        </HeaderTemplate>
        <StartNavigationTemplate>
            <div class="cssClassButtonWrapper">
                <asp:Button ID="StartNextButton" runat="server" AlternateText="Next" BackColor="Transparent"
                    BorderStyle="None" CausesValidation="False" CommandName="MoveNext" CssClass="cssClassButton"
                    Text="Next" meta:resourcekey="StartNextButtonResource1" />
                <asp:Button ID="CancelButton" runat="server" AlternateText="Cancel" BackColor="Transparent"
                    BorderStyle="None" CausesValidation="False" CommandName="Cancel" CssClass="cssClassButton"
                    Text="Cancel" meta:resourcekey="CancelButtonResource1" />
            </div>
        </StartNavigationTemplate>
        <WizardSteps>
            <asp:WizardStep ID="Step0" runat="Server" Title="Introduction" StepType="Start" 
                AllowReturn="false" meta:resourcekey="Step0Resource1">
                <div class="cssClassFormWrapper">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblBrowseFileHelp" runat="server" CssClass="cssClassHelpTitle" 
                                    Text="Use the browse button to browse your local file system to find the extension package you wish to install, then click Next to continue." 
                                    meta:resourcekey="lblBrowseFileHelpResource1"> 
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--<BD:ToolTipLabelControl ID="lblModulebrowse" Text="Select zip module:" ToolTip="Select zip module"
                            runat="server" CssClass="AdminLable" ToolTipImage="~/Templates/darkOrange/images/ico-help.gif" />
                        --%>
                                &nbsp; <asp:FileUpload ID="fileModule" runat="server" 
                                    meta:resourcekey="fileModuleResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblLoadMessage" runat="server" CssClass="cssClasssNormalRed" 
                                    Visible="true" meta:resourcekey="lblLoadMessageResource1" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="Step1" runat="server" Title="Warnings" StepType="Step" 
                AllowReturn="false" meta:resourcekey="Step1Resource1">
                <div class="cssClassFormWrapper">
                    <asp:Label ID="lblWarningMessage" runat="server" CssClass="cssClasssNormalRed" 
                        EnableViewState="False" meta:resourcekey="lblWarningMessageResource1" />
                  
                    <asp:Panel ID="pnlRepair" runat="server" Visible="true" 
                        meta:resourcekey="pnlRepairResource1">
                        <asp:Label ID="lblRepairInstallHelp" runat="server" CssClass="cssClassHelpTitle"
                            
                            Text="Repair Install the previous installed Module overwrite all database and files contents." 
                            meta:resourcekey="lblRepairInstallHelpResource1" /><br />
                        <asp:CheckBox ID="chkRepairInstall" runat="server" CssClass="cssClassCheckBox" 
                            meta:resourcekey="chkRepairInstallResource1" />
                    </asp:Panel>
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="Step2" runat="Server" Title="PackageInfo" StepType="Step" 
                AllowReturn="false" meta:resourcekey="Step2Resource1">
                <div class="cssClassFormWrapper">
                    <asp:Panel ID="pnlPackage" runat="server" 
                        meta:resourcekey="pnlPackageResource1">
                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblName" runat="server" Text="Name:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblNameResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNameD" runat="server" Text="" 
                                        CssClass="cssClassFormLabelField" meta:resourcekey="lblNameDResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPackageType" runat="server" Text="PackageType:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblPackageTypeResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPackageTypeD" runat="server" Text="" 
                                        CssClass="cssClassFormLabelField" meta:resourcekey="lblPackageTypeDResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFriendlyName" runat="server" Text="FriendlyName:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblFriendlyNameResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFriendlyNameD" runat="server" Text="" 
                                        CssClass="cssClassFormLabelField" meta:resourcekey="lblFriendlyNameDResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDescription" runat="server" Text="Description:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDescriptionD" runat="server" Text="" 
                                        CssClass="cssClassFormLabelField" meta:resourcekey="lblDescriptionDResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblVersion" runat="server" Text="Version:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblVersionResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblVersionD" runat="server" Text="" 
                                        CssClass="cssClassFormLabelField" meta:resourcekey="lblVersionDResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblOwner" runat="server" Text="Owner:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblOwnerResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblOwnerD" runat="server" Text="" 
                                        CssClass="cssClassFormLabelField" meta:resourcekey="lblOwnerDResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblOrganization" runat="server" Text="Organization:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblOrganizationResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblOrganizationD" runat="server" Text="" 
                                        CssClass="cssClassFormLabelField" meta:resourcekey="lblOrganizationDResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUrl" runat="server" Text="Url:" CssClass="cssClassFormLabel" 
                                        meta:resourcekey="lblUrlResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblUrlD" runat="server" Text="" 
                                        CssClass="cssClassFormLabelField" meta:resourcekey="lblUrlDResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEmail" runat="server" Text="Email:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblEmailResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEmailD" runat="server" Text="" 
                                        CssClass="cssClassFormLabelField" meta:resourcekey="lblEmailDResource1"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="Step3" runat="Server" Title="ReleaseNotes" StepType="Step" 
                AllowReturn="false" meta:resourcekey="Step3Resource1">
                <div class="cssClassFormWrapper">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlReleaseNotes" runat="server" CssClass="" Width="650px" 
                                    meta:resourcekey="pnlReleaseNotesResource1">
                                    <asp:Label ID="lblReleaseNotes" runat="server" Text="ReleaseNotes:" 
                                        CssClass="cssClassFormLabel" meta:resourcekey="lblReleaseNotesResource1"></asp:Label>
                                    <asp:Label ID="lblReleaseNotesD" runat="server" Text="" 
                                        CssClass="cssClassFormLabelField" meta:resourcekey="lblReleaseNotesDResource1"></asp:Label>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="Step4" runat="server" Title="License" StepType="Step" 
                AllowReturn="false" meta:resourcekey="Step4Resource1">
                <div class="cssClassFormWrapper">
                    <div class="cssClassLicense"><table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server" CssClass="" 
                                    meta:resourcekey="Panel1Resource1">
                                    <asp:Label ID="lblLicense" runat="server" Text="License:" 
                                        meta:resourcekey="lblLicenseResource1"></asp:Label>
                                    <asp:Label ID="lblLicenseD" runat="server" Text="" 
                                        CssClass="cssClassFormLabelField" meta:resourcekey="lblLicenseDResource1"></asp:Label>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkAcceptLicense" runat="server" CssClass="cssClassCheckBox" TextAlign="Left"
                                    Text="Accept License?" meta:resourcekey="chkAcceptLicenseResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAcceptMessage" runat="server" EnableViewState="False" 
                                    CssClass="cssClasssNormalRed" meta:resourcekey="lblAcceptMessageResource1" />
                            </td>
                        </tr>
                    </table></div>
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="Step5" runat="Server" Title="InstallResults" 
                StepType="Finish" meta:resourcekey="Step5Resource1">
                <div class="cssClassFormWrapper">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblInstallMessage" runat="server" EnableViewState="False" 
                                    CssClass="cssClasssNormalGreen" meta:resourcekey="lblInstallMessageResource1" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:WizardStep>
        </WizardSteps>
        <StepNavigationTemplate>
            <div class="cssClassButtonWrapper">
                <asp:Button ID="StepNextButton" runat="server" AlternateText="Next" BackColor="Transparent"
                    BorderStyle="None" CausesValidation="False" CommandName="MoveNext" CssClass="cssClassButton"
                    Text="Next" meta:resourcekey="StepNextButtonResource1" />
                <asp:Button ID="CancelButton" runat="server" AlternateText="Cancel" BackColor="Transparent"
                    BorderStyle="None" CausesValidation="False" CommandName="Cancel" CssClass="cssClassButton"
                    Text="Cancel" meta:resourcekey="CancelButtonResource2" />
            </div>
        </StepNavigationTemplate>
    </asp:Wizard>
</div>
