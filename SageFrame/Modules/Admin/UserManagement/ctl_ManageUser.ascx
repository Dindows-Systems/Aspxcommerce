<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_ManageUser.ascx.cs"
    Inherits="SageFrame.Modules.Admin.UserManagement.ctl_ManageUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Modules/Admin/UserManagement/ctl_UserProfile.ascx" TagName="UserProfile"
    TagPrefix="UC" %>
<%@ Register Src="~/Modules/Admin/UserManagement/ctl_ProfileDefinitions.ascx" TagName="ProfileDefinition"
    TagPrefix="UC" %>

<script language="javascript" type="text/javascript">
    function CheckLength(events, args) {
        var x = args.Value;
        args.IsValid = x.length >= 4;
    }

    function ValidateCheckBoxSelection() {
        var valid = false;
        var gv = '#' + '<%=gdvUser.ClientID%>' + ' tr';
        $.each($(gv), function() {
            if ($(this).find("td:eq(0) input[type='checkbox']").attr("checked")) {
                valid = true;
            }
        });
        if (!valid)
            alert("Please select at least one user");
        else {
            valid = confirm("Are you sure you want to delete?");
        }

        return valid;

    }

</script>

<h2 class="cssClassFormHeading">
    <asp:Label ID="lblUserManagement" runat="server" Text="User Management"></asp:Label>
</h2>
<asp:Panel ID="pnlManageUser" runat="server">
    <asp:HiddenField ID="hdnEditUsername" runat="server" />
    <asp:HiddenField ID="hdnEditUserID" runat="server" />
    <asp:HiddenField ID="hdnCurrentEmail" runat="server" />
    <ajax:TabContainer ID="TabContainerManageUser" runat="server" ActiveTabIndex="0"
        Visible="true">
        <ajax:TabPanel ID="tabUserInfo" runat="server">
            <HeaderTemplate>
                <asp:Label ID="lblUIH" runat="server" Text="User Information"></asp:Label>
            </HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="udpUserInfo" runat="server">
                    <ContentTemplate>
                        <div class="cssClassFormWrapper">
                            <h2 class="cssClassFormHeading">
                                <asp:Label ID="lblEditUser" runat="server" Text="Edit User"></asp:Label>
                            </h2>
                            <table id="tblUserInformationSettings" runat="server" cellpadding="0" cellspacing="0"
                                border="0">
                                <tr>
                                    <td width="20%">
                                        <asp:Label ID="lblManageUsername" runat="server" CssClass="cssClassFormLabel" Text="Username"></asp:Label>
                                    </td>
                                    <td width="30px" align="center">
                                        <asp:Label ID="Label21" runat="server" Text=":"></asp:Label>
                                    </td>
                                    <td width="10%">
                                        <asp:Label ID="txtManageUsername" runat="server" CssClass="cssClassFormLabelField"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                        <td width="20%">
                                            <asp:Label ID="lblCreatedDate" runat="server" CssClass="cssClassFormLabel" Text="Created Date"></asp:Label>
                                        </td>
                                        <td width="30px" align="center">
                                            <asp:Label ID="Label39" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtCreatedDate" runat="server" Text="Created Date" CssClass="cssClassFormLabelField"></asp:Label>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblManageFirstName" runat="server" CssClass="cssClassFormLabel" Text="First Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Text=":"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtManageFirstName" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtManageFirstName"
                                            ErrorMessage="First name is required" ValidationGroup="vgManageUserInfo">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td width="10px">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLastLoginDate" runat="server" CssClass="cssClassFormLabel" Text="Last Login Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label38" runat="server" Text=":"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLastLoginDate" runat="server" Text="Last Login Date" CssClass="cssClassFormLabelField"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblManageLastName" runat="server" CssClass="cssClassFormLabel" Text="Last Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" Text=":"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtManageLastName" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtManageLastName"
                                            ErrorMessage="Last name is required" ValidationGroup="vgManageUserInfo">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLastActivity" runat="server" CssClass="cssClassFormLabel" Text="Last Activity"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label33" runat="server" Text=":"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLastActivity" runat="server" Text="Last Activity" CssClass="cssClassFormLabelField"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblManageEmail" runat="server" CssClass="cssClassFormLabel" Text="Email"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label25" runat="server" Text=":"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtManageEmail" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtManageEmail"
                                            ErrorMessage="Email is required" ValidationGroup="vgManageUserInfo">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtManageEmail"
                                            ErrorMessage="Enter valid email." Text="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="vgManageUserInfo"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLastPasswordChanged" runat="server" CssClass="cssClassFormLabel"
                                            Text="Last Password Changed"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label36" runat="server" Text=":"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLastPasswordChanged" runat="server" Text="Last Password Changed"
                                            CssClass="cssClassFormLabelField"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblIsUserActive" runat="server" CssClass="cssClassFormLabel" Text="Is Active"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text=":"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsActive" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="cssClassButtonWrapper">
                            <asp:ImageButton ID="imgUserInfoSave" runat="server" OnClick="imgUserInfoSave_Click"
                                ToolTip="Update" ValidationGroup="vgManageUserInfo" />
                            <asp:Label ID="lblUserInfoSave" runat="server" Text="Update" AssociatedControlID="imgUserInfoSave"
                                Style="cursor: pointer;" ValidationGroup="vgManageUserInfo"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </ajax:TabPanel>
        <ajax:TabPanel ID="tabUserRoles" runat="server">
            <HeaderTemplate>
                <asp:Label ID="lblURH" runat="server" Text="User Roles"></asp:Label>
            </HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="udpUserRoles" runat="server">
                    <ContentTemplate>
                        <div class="cssClassFormWrapper">
                            <table id="tblUserRolesSettings" runat="server" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td width="20%">
                                        <asp:Label ID="lblUnselected" runat="server" CssClass="cssClassFormLabel" Text="Unselected"></asp:Label>
                                    </td>
                                    <td width="1%">
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSelected" runat="server" CssClass="cssClassFormLabel" Text="Selected"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="235px" valign="top">
                                        <asp:ListBox ID="lstUnselectedRoles" runat="server" SelectionMode="Multiple" Height="200px"
                                            CssClass="cssClassFormList"></asp:ListBox>
                                    </td>
                                    <td class="cssClassSelectLeftRight">
                                        <asp:Button ID="btnAddAllRole" runat="server" CausesValidation="false" OnClick="btnAddAllRole_Click"
                                            CssClass="cssClassSelectAllRight" Text="&gt;&gt;" />
                                        <br />
                                        <asp:Button ID="btnAddRole" runat="server" CausesValidation="false" OnClick="btnAddRole_Click"
                                            CssClass="cssClassSelectOneRight" Text=" &gt; " />
                                        <br />
                                        <asp:Button ID="btnRemoveRole" runat="server" CausesValidation="false" OnClick="btnRemoveRole_Click"
                                            CssClass="cssClassSelectOneLeft" Text=" &lt; " />
                                        <br />
                                        <asp:Button ID="btnRemoveAllRole" runat="server" CausesValidation="false" OnClick="btnRemoveAllRole_Click"
                                            Text="&lt;&lt;" CssClass="cssClassSelectAllLeft" />
                                    </td>
                                    <td valign="top">
                                        <asp:ListBox ID="lstSelectedRoles" runat="server" SelectionMode="Multiple" Height="200px"
                                            CssClass="cssClassFormList"></asp:ListBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <div class="cssClassButtonWrapper">
                                <asp:ImageButton ID="imgManageRoleSave" runat="server" OnClick="imgManageRoleSave_Click" />
                                <asp:Label ID="lblManageRoleSave" runat="server" Text="Update" AssociatedControlID="imgManageRoleSave"
                                    Style="cursor: pointer;"></asp:Label>
                            </div>
                            <div>
                                &nbsp;</div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </ajax:TabPanel>
        <ajax:TabPanel ID="tabUserPassword" runat="server">
            <HeaderTemplate>
                <asp:Label ID="lblCPH" runat="server" Text="Change Password"></asp:Label>
            </HeaderTemplate>
            <ContentTemplate>
                <p class="cssClassHelpTitle">
                    <asp:Label ID="lblCPM" runat="server" Text="To change a password for this user enter the new password and confirm the entry
                    by typing it again."></asp:Label>
                </p>
                <asp:UpdatePanel ID="udpUserPassword" runat="server">
                    <ContentTemplate>
                        <div class="cssClassFormWrapper">
                            <table id="tblChangePasswordSettings" runat="server" cellpadding="0" cellspacing="0"
                                border="0">
                                <tr>
                                    <td width="20%">
                                        <asp:Label ID="lblNewPassword" runat="server" CssClass="cssClassFormLabel" Text="New Password"></asp:Label>
                                    </td>
                                    <td width="2%">
                                        <asp:Label ID="Label1" runat="server" Text=":"></asp:Label>
                                    </td>
                                    <td width="15%">
                                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="cssClassNormalTextBox"
                                            TextMode="Password" ValidationGroup="vgManagePassword"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewPassword"
                                            ErrorMessage="Password is required." ValidationGroup="vgManagePassword"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cvPasswordLength" runat="server" ControlToValidate="txtNewPassword"
                                            ValidationGroup="vgManagePassword" ErrorMessage="Password must be at least 4 characters long"
                                            ClientValidationFunction="CheckLength"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRetypeNewPassword" runat="server" CssClass="cssClassFormLabel"
                                            Text="Retype New Password"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text=":"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRetypeNewPassword" runat="server" CssClass="cssClassNormalTextBox"
                                            TextMode="Password" ValidationGroup="vgManagePassword"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRetypeNewPassword"
                                            ErrorMessage="Type password again." ValidationGroup="vgManagePassword"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtRetypeNewPassword"
                                            ValidationGroup="vgManagePassword" ErrorMessage="Password must be at least 4 characters long"
                                            ClientValidationFunction="CheckLength"></asp:CustomValidator>
                                    </td>
                                </tr>
                            </table>
                            <div class="cssClassButtonWrapper cssClassLeftPadding">
                                <asp:Button ID="btnManagePasswordSave" runat="server" ValidationGroup="vgManagePassword"
                                    CssClass="cssClassButton" CausesValidation="true" Text="Change" OnClick="btnManagePasswordSave_Click" />
                            </div>
                            <div class="cssClassValidationSummary">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword"
                                    ControlToValidate="txtRetypeNewPassword" ErrorMessage="Retype password doesnt matched."
                                    ValidationGroup="vgManagePassword"></asp:CompareValidator>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </ajax:TabPanel>
        <ajax:TabPanel ID="tabUserProfile" runat="server">
            <HeaderTemplate>
                <asp:Label ID="lblUP" runat="server" Text="User Profile"></asp:Label>
            </HeaderTemplate>
            <ContentTemplate>
                <UC:UserProfile ID="userProfile1" runat="server"></UC:UserProfile>
            </ContentTemplate>
        </ajax:TabPanel>
    </ajax:TabContainer>
    <div class="cssClassButtonWrapper">
        <asp:ImageButton ID="imgBack" runat="server" OnClick="imgBack_Click" ToolTip="Go Back"
            CausesValidation="false" />
        <asp:Label ID="lblBack" runat="server" Text="Cancel" AssociatedControlID="imgBack"
            Style="cursor: pointer;"></asp:Label>
    </div>
</asp:Panel>
<asp:Panel ID="pnlUser" runat="server"> 
            <div class="cssClassFormWrapper">
                <h2 class="cssClassFormHeading">
                    <asp:Label ID="lblAddUserHeading" runat="server" Text="Add User"></asp:Label>
                </h2>
                <table cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td colspan="6">
                            all <span class="cssClassRequiredField">* </span>are required fields
                        </td>
                    </tr>
                    <tr>
                        <td width="16%">
                            <asp:Label ID="lblUsername" runat="server" CssClass="cssClassFormLabel" Text="Username"></asp:Label>
                            <span class="cssClassRequiredField">*</span>
                        </td>
                        <td width="2%" align="center">
                            <asp:Label ID="Label2" runat="server" Text=":"></asp:Label>
                        </td>
                        <td width="30%">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="Username is required" Text="*" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                        </td>
                        <td width="17%">
                            <asp:Label ID="lblEmail" runat="server" CssClass="cssClassFormLabel" Text="Email"></asp:Label>
                            <span class="cssClassRequiredField">*</span>
                        </td>
                        <td width="2%">
                            <asp:Label ID="Label14" runat="server" Text=":"></asp:Label>
                        </td>
                        <td width="40%">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Email is required." Text="*" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Enter valid email." Text="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="CreateUser"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFirstName" runat="server" CssClass="cssClassFormLabel" Text="First Name"></asp:Label>
                            <span class="cssClassRequiredField">*</span>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text=":"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                                ErrorMessage="First name is required" Text="*" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblLastName" runat="server" CssClass="cssClassFormLabel" Text="Last Name"></asp:Label>
                            <!--<span class="cssClassRequiredField">*</span>-->
                        </td>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text=":"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                                ErrorMessage="Last name is required" Text="*" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdnPassword" runat="server" />
                            <asp:Label ID="lblPassword" runat="server" CssClass="cssClassFormLabel" Text="Password (min 4 chars)"></asp:Label>
                            <span class="cssClassRequiredField">*</span>
                        </td>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text=":"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="cssClassNormalTextBox" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Password is required" Text="*" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Password must be atleast of 4 character.."
                                ControlToValidate="txtPassword" ValidationGroup="CreateUser" ValidationExpression=".{4,}">*</asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblRetypePassword" runat="server" CssClass="cssClassFormLabel" Text="Re-type Password"></asp:Label>
                            <span class="cssClassRequiredField">*</span>
                        </td>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text=":"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRetypePassword" runat="server" CssClass="cssClassNormalTextBox"
                                TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                                ErrorMessage="Re-type password is required" Text="*" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSecurityQuestion" runat="server" CssClass="cssClassFormLabel" Text="Security Question"></asp:Label>
                            <span class="cssClassRequiredField">*</span>
                        </td>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text=":"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSecurityQuestion" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSecurityQuestion" runat="server" ControlToValidate="txtSecurityQuestion"
                                ErrorMessage="Security question is required" Text="*" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblSecurityAnswer" runat="server" CssClass="cssClassFormLabel" Text="Security Answer"></asp:Label>
                            <span class="cssClassRequiredField">*</span>
                        </td>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text=":"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSecurityAnswer" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSecurityAnswer" runat="server" ControlToValidate="txtSecurityAnswer"
                                ErrorMessage="Security answer is required" Text="*" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td colspan="5">
                            <asp:CompareValidator ID="cvRetypePassword" runat="server" ControlToCompare="txtPassword"
                                ControlToValidate="txtRetypePassword" ErrorMessage="Retype password doesnt matched."
                                ValidationGroup="CreateUser"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <asp:Label ID="lblSLM" runat="server" Text="Select Roles" CssClass="cssClassFormLabel"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text=":"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:ListBox ID="lstAvailableRoles" Width="258px" runat="server" SelectionMode="Multiple"
                                Rows="10"></asp:ListBox>
                        </td>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="CreateUser"
                                CssClass="cssClassvalidationsummary" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <div class="cssClassButtonWrapper">
                                <asp:ImageButton ID="imbCreateUser" ValidationGroup="CreateUser" CausesValidation="true"
                                    runat="server" ToolTip="Add Users" OnClick="imbCreateUser_Click" />
                                <asp:Label ID="lblCreateUser" runat="server" Text="Create User" AssociatedControlID="imbCreateUser"
                                    Style="cursor: pointer;"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    </tr>
                </table>
            </div>     
    <div class="cssClassButtonWrapper">
        <asp:ImageButton ID="imbBackinfo" runat="server" OnClick="imgBack_Click" ToolTip="Back" />
        <asp:Label ID="lblBackinfo" runat="server" Text="Back" AssociatedControlID="imbBackinfo"></asp:Label>
    </div>
</asp:Panel>
<asp:Panel ID="pnlUserList" runat="server">
    <%-- <ajax:TabContainer ID="TabContainerUserList" runat="server" ActiveTabIndex="0">
        <ajax:TabPanel ID="TabPanelUserList" runat="server">
            <HeaderTemplate>
                <asp:Label ID="lblUL" runat="server" Text="User List"></asp:Label>
            </HeaderTemplate>
            <ContentTemplate>--%>
    <div class="cssClassButtonWrapper">
        <asp:ImageButton ID="imgAddUser" runat="server" OnClick="imgAddUser_Click" ToolTip="Add User" />
        <asp:Label ID="lblAddUser" runat="server" Text="Add User" AssociatedControlID="imgAddUser"
            Style="cursor: pointer;"></asp:Label>
        <asp:ImageButton ID="imgBtnDeleteSelected" runat="server" OnClientClick="return ValidateCheckBoxSelection()"
            OnClick="imgBtnDeleteSelected_Click" ToolTip="Delete all seleted" />
        <asp:Label ID="lblManageCategories" runat="server" Text="Delete all seleted" AssociatedControlID="imgBtnDeleteSelected"
            Style="cursor: pointer;"></asp:Label>
        <asp:ImageButton ID="imgBtnSaveChanges" runat="server" OnClick="imgBtnSaveChanges_Click"
            ToolTip="Update changes" />
        <asp:Label ID="Label5" runat="server" Text="Update changes" AssociatedControlID="imgBtnSaveChanges"
            Style="cursor: pointer;"></asp:Label>
        <asp:ImageButton ID="imgBtnSettings" runat="server" ToolTip="UserSettings" OnClick="imgBtnSettings_Click" />
        <asp:Label ID="lblSettings" runat="server" Text="UserSettings" AssociatedControlID="imgBtnSettings"
            Style="cursor: pointer;"></asp:Label>
    </div>
    <div class="cssClassFormWrapper">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td width="14%">
                    <asp:Label ID="lblSearchUserRole" runat="server" CssClass="cssClassFormLabel" Text="Search user in role :"></asp:Label>
                </td>
                <td width="10%">
                    <asp:DropDownList ID="ddlSearchRole" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchRole_SelectedIndexChanged"
                        CssClass="cssClassDropDown">
                    </asp:DropDownList>
                </td>
                <td width="10%">
                    <asp:Label ID="lblSearchUser" runat="server" CssClass="cssClassFormLabel" Text="Search User :"></asp:Label>
                </td>
                <td width="10%">
                    <asp:TextBox ID="txtSearchText" runat="server" OnTextChanged="txtSearchText_TextChanged"
                        CssClass="cssClassNormalTextBox"></asp:TextBox>
                    <ajax:AutoCompleteExtender runat="server" ID="aceSearchText" TargetControlID="txtSearchText"
                        ServicePath="~/SageFrameWebService.asmx" ServiceMethod="GetUsernameList" MinimumPrefixLength="1"
                        DelimiterCharacters="" Enabled="True" />
                </td>
                <td class="cssClassFormLabel_padding">
                    <asp:ImageButton ID="imgSearch" runat="server" OnClick="imgSearch_Click" ToolTip="Search" />
                    <asp:Label ID="lblSearch" runat="server" Text="Search" AssociatedControlID="imgSearch"
                        Style="cursor: pointer;" CssClass="cssClassFormLabel"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 110px;">
                    <asp:Label ID="lblSRow" runat="server" Text="Show rows :" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRecordsPerPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRecordsPerPage_SelectedIndexChanged"
                        CssClass="cssClasslistddl cssClassPageSize">
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                        <asp:ListItem Value="150">150</asp:ListItem>
                        <asp:ListItem Value="200">200</asp:ListItem>
                        <asp:ListItem Value="250">250</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblShowMode" runat="server" Text="Filter Mode:" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td colspan="2" class="cssClassButtonListWrapper">
                    <asp:RadioButtonList ID="rbFilterMode" CssClass="cssClassRadioButtonList" RepeatDirection="Horizontal"
                        runat="server" AutoPostBack="True" 
                        onselectedindexchanged="rbFilterMode_SelectedIndexChanged">
                        <asp:ListItem Text="All" Selected="True" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                        <asp:ListItem Text="UnApproved" Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    </div>
    <div class="cssClassGridWrapper">
        <asp:GridView ID="gdvUser" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvUser_RowCommand"
            AllowPaging="true" AllowSorting="true" GridLines="None" OnRowDataBound="gdvUser_RowDataBound"
            Width="100%" EmptyDataText="User not found" DataKeyNames="UserId,Username" OnPageIndexChanging="gdvUser_PageIndexChanging">
            <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input id="chkBoxHeader" runat="server" class="cssCheckBoxHeader" type="checkbox" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input id="chkBoxItem" runat="server" class="cssCheckBoxItem" type="checkbox" enableviewstate="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblUsername" runat="server" CssClass="cssClassFormLabel" Text="Username"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkUsername" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                            CommandName="EditUser" Text='<%# Eval("Username")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblFirstName" runat="server" Text="First name"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("FirstName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblLastName" runat="server" Text="Last name"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("LastName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("Email")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input id="chkBoxIsActiveHeader" runat="server" class="cssCheckBoxIsActiveHeader"
                            type="checkbox" />
                        <asp:Label ID="lblIsActive" runat="server" Text="Is Active"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnIsActive" runat="server" Value='<%# Eval("IsActive") %>' />
                        <input id="chkBoxIsActiveItem" runat="server" disabled="disabled" class="cssCheckBoxIsActiveItem" type="checkbox" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="cssClassColumnIsActive" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                            CommandName="EditUser" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>'
                            ToolTip="Edit User" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="cssClassColumnEdit" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                            CommandName="DeleteUser" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                            ToolTip="Delete User" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="cssClassColumnDelete" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="cssClassPageNumber" />
            <HeaderStyle CssClass="cssClassHeadingOne" />
            <RowStyle CssClass="cssClassAlternativeOdd" />
        </asp:GridView>
    </div>
    <%-- </ContentTemplate>
        </ajax:TabPanel>
        <ajax:TabPanel ID="TabPanelUserProfile" runat="server">
            <HeaderTemplate>
                <asp:Label ID="lblMP" runat="server" Text="Management Profile"></asp:Label>
            </HeaderTemplate>
            <ContentTemplate>
                <UC:ProfileDefinition ID="ProfileDefinition1" runat="server" />
            </ContentTemplate>
        </ajax:TabPanel>
    </ajax:TabContainer>--%>
</asp:Panel>
<asp:Panel ID="pnlSettings" runat="server">
    <div class="cssClassFormWrapper">
        <h2 class="cssClassFormHeading">
            <asp:Label ID="Label7" runat="server" Text="User Settings"></asp:Label>
        </h2>
        <table>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDupNames" CssClass="cssClassFormLabel">Enable Duplicate UserNames Across Portals:</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkEnableDupNames" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDupEmail" CssClass="cssClassFormLabel">Enable Duplicate Email Across Portals:</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkEnableDupEmail" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDupRoles" CssClass="cssClassFormLabel">Enable Duplicate Roles Across Portals:</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkEnableDupRole" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblEnableCaptcha" CssClass="cssClassFormLabel">Enable Captcha For User Registration:</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkEnableCaptcha" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlPasswordEncTypes" runat="server" GroupingText="Password Storage Mode"
                        CssClass="cssClassFormLabel">
                        <asp:RadioButtonList ID="rdbLst" runat="server">
                            <asp:ListItem Enabled="false" Value="1">PlainText</asp:ListItem>
                            <asp:ListItem Value="2">One Way Hashed</asp:ListItem>
                            <asp:ListItem Value="3">Encrypted</asp:ListItem>
                        </asp:RadioButtonList>
                        <%--<asp:RadioButtonList ID="rdbLstEncTypes" RepeatDirection="Horizontal" Style="margin-left: 20px;
                            width: 120px;" runat="server">
                            <asp:ListItem Selected="True" Value="3">AES</asp:ListItem>
                            <asp:ListItem Value="4">RSA</asp:ListItem>
                        </asp:RadioButtonList>--%>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="cssClassButtonWrapper">
                        <asp:Button ID="btnSaveSetting" CssClass="cssClassButton" runat="server" Text="Save"
                            OnClick="btnSaveSetting_Click" />
                        <asp:Button ID="btnCancel" CssClass="cssClassButton" runat="server" Text="Cancel"
                            OnClick="btnCancel_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
