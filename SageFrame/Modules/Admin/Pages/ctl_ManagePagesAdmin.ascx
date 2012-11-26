<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_ManagePagesAdmin.ascx.cs"
  Inherits="SageFrame.Modules.Admin.Pages.ctl_ManagePagesAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Controls/sectionheadcontrol.ascx" TagName="sectionheadcontrol"
  TagPrefix="sfe" %>
<h2 class="cssClassFormHeading">
  <asp:Label ID="lblPageManagement" runat="server" Text="Page Management" 
        meta:resourcekey="lblPageManagementResource1"></asp:Label></h2>
<asp:Panel ID="pnlPage" runat="server" meta:resourcekey="pnlPageResource1">
  <asp:HiddenField ID="hdnPageID" runat="server" Value="0" />
  <asp:HiddenField ID="hdnIsActiveDB" runat="server" Value="true" />
  <ajax:TabContainer ID="TabContainerManagePages" runat="server" ActiveTabIndex="0" 
        meta:resourcekey="TabContainerManagePagesResource1">
    <ajax:TabPanel ID="TabPanelBasicSettings" runat="server" 
          meta:resourcekey="TabPanelBasicSettingsResource1">
      <HeaderTemplate>
        <asp:Label ID="lblPBS" runat="server" Text="Basic Settings" 
              meta:resourcekey="lblPBSResource1"></asp:Label>
      </HeaderTemplate>
      <ContentTemplate>
        <asp:Label ID="lblBasicSettingsHelp" runat="server" CssClass="cssClassHelpTitle"
          Text="In this section, you can set up the basic settings for this page" 
              meta:resourcekey="lblBasicSettingsHelpResource1"></asp:Label>
        <div class="cssClassCollapseWrapper">
          <sfe:sectionheadcontrol ID="shcPageDetails" runat="server" IncludeRule="true" IsExpanded="true"
            Section="tblPageDetails" Text="Page Details" />
          <div id="tblPageDetails" runat="server" class="cssClassCollapseContent">
            <div class="cssClassFormWrapper">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="25%">
                                        <asp:HiddenField ID="hdnIsRequiredPage" runat="server" />
                                        <asp:Label ID="lblPageName" runat="server" CssClass="cssClassFormLabel" 
                                            Text="Page Name:" meta:resourcekey="lblPageNameResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPageName" runat="server" CssClass="cssClassNormalTextBox" 
                                            MaxLength="100" meta:resourcekey="txtPageNameResource1" />
                                        <asp:RequiredFieldValidator ID="valPageName" runat="server" ControlToValidate="txtPageName"
                                            Display="Dynamic" ErrorMessage="Page Name Is Required" SetFocusOnError="True"
                                            ValidationGroup="pagesettings" meta:resourcekey="valPageNameResource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblParentPage" runat="server" CssClass="cssClassFormLabel" 
                                            Text="Parent Page:" meta:resourcekey="lblParentPageResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboParentPage" runat="server" AutoPostBack="True" CssClass="cssClassDropDown"
                                            DataTextField="LevelPageName" DataValueField="PageID" 
                                            OnSelectedIndexChanged="cboParentPage_SelectedIndexChanged" 
                                            meta:resourcekey="cboParentPageResource1">
                                          
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblInsertPosition" runat="server" CssClass="cssClassFormLabel" 
                                            Text="Insert Page:" meta:resourcekey="lblInsertPositionResource1"></asp:Label>
                                    </td>
                                    <td runat="server" class="cssClassButtonListWrapper">
                                        <asp:RadioButtonList ID="rbInsertPosition" runat="server" CssClass="cssClassRadioButtonList"
                                            RepeatDirection="Horizontal" 
                                            meta:resourcekey="rbInsertPositionResource1" />
                                        <asp:DropDownList ID="cboPositionTab" runat="server" 
                                            CssClass="cssClassDropDown" meta:resourcekey="cboPositionTabResource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMenu" runat="server" CssClass="cssClassFormLabel" 
                                            Text="Is Visibled?" meta:resourcekey="lblMenuResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkMenu" runat="server" Checked="True" 
                                            CssClass="cssClassCheckBox" meta:resourcekey="chkMenuResource1" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTitle" runat="server" CssClass="cssClassFormLabel" 
                                            Text="Page Title:" meta:resourcekey="lblTitleResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="cssClassNormalTextBox" 
                                            MaxLength="200" meta:resourcekey="txtTitleResource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDescription" runat="server" CssClass="cssClassFormLabel" 
                                            Text="Description:" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="CssClassNormalTextBox"
                                            MaxLength="500" Rows="3" TextMode="MultiLine" 
                                            meta:resourcekey="txtDescriptionResource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblKeywords" runat="server" CssClass="cssClassFormLabel" 
                                            Text="Keywords:" meta:resourcekey="lblKeywordsResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtKeyWords" runat="server" CssClass="CssClassNormalTextBox" MaxLength="500"
                                            Rows="3" TextMode="MultiLine" meta:resourcekey="txtKeyWordsResource1" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
          </div>
        </div>
      </ContentTemplate>
    </ajax:TabPanel>
    <ajax:TabPanel ID="TabPagePermissionsSettings" runat="server" 
          meta:resourcekey="TabPagePermissionsSettingsResource1">
      <HeaderTemplate>
        <asp:Label ID="lblPPS" runat="server" Text="Page Permissions Settings" 
              meta:resourcekey="lblPPSResource1"></asp:Label>
      </HeaderTemplate>
      <ContentTemplate>
        <asp:Label ID="lblPagePermissionsHelp" runat="server" CssClass="cssClassHelpTitle"
          
              Text="In this section, you can set up the page permissions settings for this page" 
              meta:resourcekey="lblPagePermissionsHelpResource1"></asp:Label>
        <div class="cssClassCollapseWrapper">
          <sfe:sectionheadcontrol ID="shcPageViewPermissionsSettings" runat="server" IncludeRule="true"
            IsExpanded="true" Section="tblPageViewPermissions" Text="View Page Permissions Settings" />
          <div id="tblPageViewPermissions" runat="server" class="cssClassCollapseContent">
            <div class="cssClassFormWrapper">
              <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td width="20%">
                    <asp:UpdatePanel ID="udpViewUserRoles" runat="server">
                      <ContentTemplate>
                        <asp:Label ID="lblMessageViewUserRole" runat="server" CssClass="cssClassNormalTitle"
                          Text="View Permissions" meta:resourcekey="lblMessageViewUserRoleResource1"></asp:Label>
                        <asp:HiddenField ID="hdnUserModuleID" runat="server" Value="0" />
                        <table border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td>
                              <asp:Label ID="lblUnselected" runat="server" CssClass="cssClassFormLabel" 
                                    Text="Unselected" meta:resourcekey="lblUnselectedResource1"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                              <asp:Label ID="lblSelected" runat="server" CssClass="cssClassFormLabel" 
                                    Text="Selected" meta:resourcekey="lblSelectedResource1"></asp:Label>
                            </td>
                            <td>
                            </td>
                          </tr>
                          <tr>
                            <td valign="top" width="162px" class="cssClassSelectPadding">
                              <asp:ListBox ID="lstUnselectedViewRoles" runat="server" CssClass="cssClassFormList"
                                Height="200px" SelectionMode="Multiple" 
                                    meta:resourcekey="lstUnselectedViewRolesResource1"></asp:ListBox>
                            </td>
                            <td class="cssClassSelectLeftRight" valign="top">
                              <asp:Button ID="btnAddAllRoleView" runat="server" CausesValidation="False" CssClass="cssClassSelectAllRight"
                                OnClick="btnAddAllRoleView_Click" Text="&gt;&gt;" 
                                    meta:resourcekey="btnAddAllRoleViewResource1" /><br />
                              <asp:Button ID="btnAddRoleView" runat="server" CausesValidation="False" CssClass="cssClassSelectOneRight"
                                OnClick="btnAddRoleView_Click" Text=" &gt; " 
                                    meta:resourcekey="btnAddRoleViewResource1" /><br />
                              <asp:Button ID="btnRemoveRoleView" runat="server" CausesValidation="False" CssClass="cssClassSelectOneLeft"
                                OnClick="btnRemoveRoleView_Click" Text=" &lt; " 
                                    meta:resourcekey="btnRemoveRoleViewResource1" /><br />
                              <asp:Button ID="btnRemoveAllRoleView" runat="server" CausesValidation="False" CssClass="cssClassSelectAllLeft"
                                OnClick="btnRemoveAllRoleView_Click" Text="&lt;&lt;" 
                                    meta:resourcekey="btnRemoveAllRoleViewResource1" />
                            </td>
                            <td valign="top" width="200px">
                              <asp:ListBox ID="lstSelectedRolesView" runat="server" CssClass="cssClassFormList"
                                Height="200px" SelectionMode="Multiple" 
                                    meta:resourcekey="lstSelectedRolesViewResource1"></asp:ListBox>
                            </td>
                            <td valign="top">
                              <asp:UpdatePanel ID="udpViewPagePermissionUsername" runat="server">
                                <ContentTemplate>
                                  <asp:Panel ID="pnlAddUsersView" runat="server" Style="display: block" 
                                        meta:resourcekey="pnlAddUsersViewResource1">
                                    <div class="cssClassUserPermission"><div class="cssClassUserPermissionInside">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td width="20%">
                                          <asp:Label ID="lblViewUsername" runat="server" CssClass="cssClassFormLabel" 
                                                Text="Username: " meta:resourcekey="lblViewUsernameResource1"></asp:Label>
                                        </td>
                                        <td>
                                          <asp:TextBox ID="txtViewUsername" runat="server" CssClass="cssClassNormalTextBox"
                                            Width="150px" meta:resourcekey="txtViewUsernameResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvViewUsername" runat="server"
                                              ErrorMessage="Username is required" Text="*" ControlToValidate="txtViewUsername"
                                              ValidationGroup="PagePermissionView" 
                                                meta:resourcekey="rfvViewUsernameResource1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                          <div class="cssClassButtonWrapper">
                                            <asp:ImageButton ID="imbAddUser" runat="server" ValidationGroup="PagePermissionView"
                                              OnClick="imbAddUser_Click" meta:resourcekey="imbAddUserResource1" />
                                            <asp:Label ID="lblAddNewUser" runat="server" AssociatedControlID="imbAddUser" Style="cursor: pointer;"
                                              Text="Add" meta:resourcekey="lblAddNewUserResource1"></asp:Label>
                                            <asp:ImageButton ID="imbSelectUsers" runat="server" CausesValidation="False" 
                                                  ToolTip="Search" OnClick="imbSelectUsers_Click" 
                                                  meta:resourcekey="imbSelectUsersResource1" />
                                            <asp:Label ID="lblSelectUsers" runat="server" AssociatedControlID="imbSelectUsers"
                                              Style="cursor: pointer;" Text="Search" 
                                                  meta:resourcekey="lblSelectUsersResource1"></asp:Label>
                                          </div>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td colspan="3">
                                          <div class="cssClassViewUsers">
                                            <asp:GridView ID="gdvViewUsernames" runat="server" AutoGenerateColumns="False" GridLines="None"
                                              OnRowDataBound="gdvViewUsernames_RowDataBound" OnRowCommand="gdvViewUsernames_RowCommand" ShowHeader="False" 
                                                  meta:resourcekey="gdvViewUsernamesResource1">
                                              <Columns>
                                                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                                  <ItemTemplate>
                                                    <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>' 
                                                          meta:resourcekey="lblUsernameResource1"></asp:Label>
                                                  </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                                  <ItemTemplate>
                                                    <asp:ImageButton ID="imbDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Username") %>'
                                                      CommandName="RemoveUser" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                                                      meta:resourcekey="imbDeleteResource1" />
                                                  </ItemTemplate>
                                                    <HeaderStyle CssClass="cssClassColumnDelete" />
                                                </asp:TemplateField>
                                              </Columns>
                                              <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                                                <HeaderStyle CssClass="cssClassHeadingOne" />
                                                <RowStyle CssClass="cssClassAlternativeOdd" />
                                            </asp:GridView>
                                          </div>
                                        </td>
                                      </tr>
                                    </table></div></div>
                                  </asp:Panel>
                                  <asp:Panel ID="pnlUsersView" runat="server" CssClass="searchPopup" Height="100%"  
                                        Visible="False" meta:resourcekey="pnlUsersViewResource1">
                                    <div class="cssClassOnClickPopUp">
                                      <div class="cssClassOnClickPopUpClose">
                                        <asp:Button ID="imbCancelView" runat="server" CausesValidation="False" OnClick="imbCancelView_Click"
                                          Text="Cancel" meta:resourcekey="imbCancelViewResource1" />
                                      </div>
                                      <div class="cssClassOnClickPopUpInside">
                                        <div class="cssClassOnClickPopUpTitle">
                                          <asp:Label ID="lblSU" runat="server" Text="Select Users:" 
                                                meta:resourcekey="lblSUResource1"></asp:Label>
                                        </div>
                                        <div class="cssClassOnClickPopUpInfo">
                                          <div class="cssClassFormWrapper">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                              <tr>
                                                <td width="126px">
                                                  <asp:Label ID="lblSearchUserRole" runat="server" CssClass="cssClassFormLabel" 
                                                        Text="Search user in role:" meta:resourcekey="lblSearchUserRoleResource1"></asp:Label>
                                                </td>
                                                <td>
                                                  <asp:DropDownList ID="ddlSearchRole" runat="server" AutoPostBack="True"
                                                    CssClass="cssClassDropDown" 
                                                        OnSelectedIndexChanged="ddlSearchRole_SelectedIndexChanged" 
                                                        meta:resourcekey="ddlSearchRoleResource1">
                                                  </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                              </tr>
                                              <tr>
                                                <td>
                                                  <asp:Label ID="lblSearchUser" runat="server" CssClass="cssClassFormLabel" 
                                                        Text="Search User:" meta:resourcekey="lblSearchUserResource1"></asp:Label>
                                                </td>
                                                <td width="170px">
                                                  <asp:TextBox ID="txtSearchText" runat="server" CssClass="cssClassNormalTextBox" 
                                                        meta:resourcekey="txtSearchTextResource1"></asp:TextBox>
                                                  <ajax:AutoCompleteExtender ID="aceSearchText" runat="server" Enabled="True" MinimumPrefixLength="1"
                                                    ServiceMethod="GetUsernameList" ServicePath="~/SageFrameWebService.asmx" 
                                                        TargetControlID="txtSearchText" DelimiterCharacters="" />
                                                </td>
                                                <td>
                                                  <asp:ImageButton ID="imgSearch" runat="server" CausesValidation="False" OnClick="imgSearch_Click"
                                                    ToolTip="Search" meta:resourcekey="imgSearchResource1" />
                                                </td>
                                              </tr>
                                            </table>
                                            <div class="cssClassGridWrapper">
                                              <asp:GridView ID="gdvUser" runat="server" AutoGenerateColumns="False" 
                                                    EmptyDataText="User not found" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gdvUser_PageIndexChanging"
                                                GridLines="None" OnRowDataBound="gdvUser_RowDataBound" 
                                                    meta:resourcekey="gdvUserResource1">
                                                <Columns>
                                                  <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                                    <ItemTemplate>
                                                      <input id="chkBoxItem" runat="server" class="cssCheckBoxItem" type="checkbox" />
                                                    </ItemTemplate>
                                                      <HeaderTemplate>
                                                          <input ID="chkBoxHeader" runat="server" class="cssCheckBoxHeader" 
                                                              type="checkbox"></input>
                                                      </HeaderTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                                                    <ItemTemplate>
                                                      <asp:Label ID="lblUsernameD" runat="server" CssClass="cssClassFormLabel" 
                                                            Text='<%# Eval("Username") %>' meta:resourcekey="lblUsernameDResource1"></asp:Label>
                                                    </ItemTemplate>
                                                      <HeaderTemplate>
                                                          <asp:Label ID="lblUsername" runat="server" CssClass="cssClassFormLabel" 
                                                              meta:resourcekey="lblUsernameResource2" Text="Username"></asp:Label>
                                                      </HeaderTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                                                    <ItemTemplate>
                                                      <asp:Label ID="lblFirstNameD" runat="server" CssClass="cssClassFormLabel" 
                                                            Text='<%# Eval("FirstName") %>' meta:resourcekey="lblFirstNameDResource1"></asp:Label>
                                                    </ItemTemplate>
                                                      <HeaderTemplate>
                                                          <asp:Label ID="lblFirstName" runat="server" CssClass="cssClassFormLabel" 
                                                              meta:resourcekey="lblFirstNameResource1" Text="First name"></asp:Label>
                                                      </HeaderTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                                                    <ItemTemplate>
                                                      <asp:Label ID="lblLastNameD" runat="server" CssClass="cssClassFormLabel" 
                                                            Text='<%# Eval("LastName") %>' meta:resourcekey="lblLastNameDResource1"></asp:Label>
                                                    </ItemTemplate>
                                                      <HeaderTemplate>
                                                          <asp:Label ID="lblLastName" runat="server" CssClass="cssClassFormLabel" 
                                                              meta:resourcekey="lblLastNameResource1" Text="Last name"></asp:Label>
                                                      </HeaderTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField meta:resourcekey="TemplateFieldResource7">
                                                    <ItemTemplate>
                                                      <asp:Label ID="lblEmailD" runat="server" CssClass="cssClassFormLabel" 
                                                            Text='<%# Eval("Email") %>' meta:resourcekey="lblEmailDResource1"></asp:Label>
                                                    </ItemTemplate>
                                                      <HeaderTemplate>
                                                          <asp:Label ID="lblEmail" runat="server" meta:resourcekey="lblEmailResource1" 
                                                              Text="Email"></asp:Label>
                                                      </HeaderTemplate>
                                                  </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                                                  <HeaderStyle CssClass="cssClassHeadingOne" />
                                                  <PagerStyle CssClass="cssClassPageNumber" />
                                                  <RowStyle CssClass="cssClassAlternativeOdd" />
                                              </asp:GridView>
                                            </div>
                                          </div>
                                          <div class="cssClassButtonWrapper">
                                            <asp:ImageButton ID="imgAddSelectedUsers" runat="server" CausesValidation="False"
                                              OnClick="imgAddSelectedUsers_Click" ToolTip="Add all seleted user(s)" 
                                                  meta:resourcekey="imgAddSelectedUsersResource1" />
                                            <asp:Label ID="lblAddSelectedUsers" runat="server" AssociatedControlID="imgAddSelectedUsers"
                                              Style="cursor: pointer;" Text="Add all seleted user(s)" 
                                                  meta:resourcekey="lblAddSelectedUsersResource1"></asp:Label>
                                          </div>
                                        </div>
                                      </div>
                                    </div>
                                    </div>
                                  </asp:Panel>
                                </ContentTemplate>
                              </asp:UpdatePanel>
                            </td>
                          </tr>
                        </table>
                      </ContentTemplate>
                    </asp:UpdatePanel>
                  </td>
                </tr>
              </table>
            </div>
          </div>
        </div>
        <div class="cssClassCollapseWrapper">
          <sfe:sectionheadcontrol ID="shcPageEditPermissionsSettings" runat="server" IncludeRule="true"
            IsExpanded="true" Section="tblPageEditPermissions" Text="Edit Page Permissions Settings" />
          <div id="tblPageEditPermissions" runat="server" class="cssClassCollapseContent">
            <div class="cssClassFormWrapper">
              <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td>
                    <asp:UpdatePanel ID="udpEditUserRoles" runat="server">
                      <ContentTemplate>
                        <asp:Label ID="lblMessageEditUserRole" runat="server" CssClass="cssClassNormalTitle"
                          Text="Edit Permissions" meta:resourcekey="lblMessageEditUserRoleResource1"></asp:Label>
                        <table border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td>
                              <asp:Label ID="lblUnselected1" runat="server" CssClass="cssClassFormLabel" 
                                    Text="Unselected" meta:resourcekey="lblUnselected1Resource1"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                              <asp:Label ID="lblSelected1" runat="server" CssClass="cssClassFormLabel" 
                                    Text="Selected" meta:resourcekey="lblSelected1Resource1"></asp:Label>
                            </td>
                            <td>
                            </td>
                          </tr>
                          <tr>
                            <td valign="top" width="162px" class="cssClassSelectPadding">
                              <asp:ListBox ID="lstUnselectedEditRoles" runat="server" CssClass="cssClassFormList"
                                Height="200px" SelectionMode="Multiple" 
                                    meta:resourcekey="lstUnselectedEditRolesResource1"></asp:ListBox>
                            </td>
                            <td class="cssClassSelectLeftRight" valign="top">
                              <asp:Button ID="btnAddAllRoleEdit" runat="server" CausesValidation="False" CssClass="cssClassSelectAllRight"
                                OnClick="btnAddAllRoleEdit_Click" Text="&gt;&gt;" 
                                    meta:resourcekey="btnAddAllRoleEditResource1" /><br />
                              <asp:Button ID="btnAddRoleEdit" runat="server" CausesValidation="False" CssClass="cssClassSelectOneRight"
                                OnClick="btnAddRoleEdit_Click" Text=" &gt; " 
                                    meta:resourcekey="btnAddRoleEditResource1" /><br />
                              <asp:Button ID="btnRemoveRoleEdit" runat="server" CausesValidation="False" CssClass="cssClassSelectOneLeft"
                                OnClick="btnRemoveRoleEdit_Click" Text=" &lt; " 
                                    meta:resourcekey="btnRemoveRoleEditResource1" /><br />
                              <asp:Button ID="btnRemoveAllRoleEdit" runat="server" CausesValidation="False" CssClass="cssClassSelectAllLeft"
                                OnClick="btnRemoveAllRoleEdit_Click" Text="&lt;&lt;" 
                                    meta:resourcekey="btnRemoveAllRoleEditResource1" />
                            </td>
                            <td valign="top" width="200px">
                              <asp:ListBox ID="lstSelectedRolesEdit" runat="server" CssClass="cssClassFormList"
                                Height="200px" SelectionMode="Multiple" 
                                    meta:resourcekey="lstSelectedRolesEditResource1"></asp:ListBox>
                            </td>
                            <td valign="top">
                              <asp:UpdatePanel ID="udpEditPagePermission" runat="server">
                                <ContentTemplate>
                                  <asp:Panel ID="pnlAddUsersEdit" runat="server" Style="display: block" 
                                        meta:resourcekey="pnlAddUsersEditResource1">
                                    <div class="cssClassUserPermission"><div class="cssClassUserPermissionInside">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td width="20%">
                                          <asp:Label ID="lblEditUsername" runat="server" CssClass="cssClassFormLabel" 
                                                Text="Username: " meta:resourcekey="lblEditUsernameResource1"></asp:Label>
                                        </td>
                                        <td>
                                          <asp:TextBox ID="txtEditUsername" runat="server" CssClass="cssClassNormalTextBox"
                                            Width="150px" meta:resourcekey="txtEditUsernameResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEditUsername" runat="server"
                                              ErrorMessage="Username is required" Text="*" ControlToValidate="txtEditUsername"
                                              ValidationGroup="PagePermissionEdit" 
                                                meta:resourcekey="rfvEditUsernameResource1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                          <div class="cssClassButtonWrapper">
                                            <asp:ImageButton ID="imbAddUserEditPermission" runat="server" CausesValidation="False"
                                              ValidationGroup="PagePermissionEdit" OnClick="imbAddUserEditPermission_Click" 
                                                  meta:resourcekey="imbAddUserEditPermissionResource1" />
                                            <asp:Label ID="lblAddUserEditPermission" runat="server" AssociatedControlID="imbAddUserEditPermission"
                                              Style="cursor: pointer;" Text="Add" 
                                                  meta:resourcekey="lblAddUserEditPermissionResource1"></asp:Label>
                                            <asp:ImageButton ID="imbSelectEditUsers" runat="server" CausesValidation="False" OnClick="imbSelectEditUsers_Click"
                                              ToolTip="Search" meta:resourcekey="imbSelectEditUsersResource1" />
                                            <asp:Label ID="lblSelectEditUsers" runat="server" AssociatedControlID="imbSelectEditUsers"
                                              Style="cursor: pointer;" Text="Search" 
                                                  meta:resourcekey="lblSelectEditUsersResource1"></asp:Label>
                                          </div>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td colspan="3">
                                          <div class="cssClassEditUsers">
                                            <asp:GridView ID="gdvEditUsernames" runat="server" AutoGenerateColumns="False" DataKeyNames="Username"
                                              GridLines="None" OnRowDataBound="gdvEditUsernames_RowDataBound" OnRowCommand="gdvEditUsernames_RowCommand" ShowHeader="False" 
                                                  meta:resourcekey="gdvEditUsernamesResource1">
                                              <Columns>
                                                <asp:TemplateField meta:resourcekey="TemplateFieldResource8">
                                                  <ItemTemplate>
                                                    <asp:Label ID="lblEditUsername" runat="server" Text='<%# Eval("Username") %>' 
                                                          meta:resourcekey="lblEditUsernameResource2"></asp:Label>
                                                  </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField meta:resourcekey="TemplateFieldResource9">
                                                  <ItemTemplate>
                                                    <asp:ImageButton ID="imbDelete2" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Username") %>'
                                                      CommandName="RemoveUser" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                                                      meta:resourcekey="imbDelete2Resource1" />
                                                  </ItemTemplate>
                                                    <HeaderStyle CssClass="cssClassColumnDelete" />
                                                </asp:TemplateField>
                                              </Columns>
                                              <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                                                <HeaderStyle CssClass="cssClassHeadingOne" />
                                                <RowStyle CssClass="cssClassAlternativeOdd" />
                                            </asp:GridView>
                                          </div>
                                        </td>
                                      </tr>
                                    </table></div></div>
                                  </asp:Panel>
                                  <asp:Panel ID="pnlUsersEdit" runat="server" CssClass="searchPopup" Height="100%" 
                                        Visible="False" meta:resourcekey="pnlUsersEditResource1">
                                    <div class="cssClassOnClickPopUp">
                                      <div class="cssClassOnClickPopUpClose">
                                        <asp:Button ID="imbCancelEdit" runat="server" CausesValidation="False" OnClick="imbCancelEdit_Click"
                                          Text="Cancel" meta:resourcekey="imbCancelEditResource1" />
                                      </div>
                                      <div class="cssClassOnClickPopUpInside">
                                        <div class="cssClassOnClickPopUpTitle">
                                          <asp:Label ID="lblSUSPUT" runat="server" Text="Select Users:" 
                                                meta:resourcekey="lblSUSPUTResource1"></asp:Label>
                                        </div>
                                        <div class="cssClassOnClickPopUpInfo">
                                          <div class="cssClassFormWrapper">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                              <tr>
                                                <td>
                                                  <asp:Label ID="lblSearchEditUserRole" runat="server" CssClass="cssClassFormLabel"
                                                    Text="Search user in role:" meta:resourcekey="lblSearchEditUserRoleResource1"></asp:Label>
                                                </td>
                                                <td>
                                                  <asp:DropDownList ID="ddlSearchEditRole" runat="server" AutoPostBack="True"
                                                    CssClass="cssClassDropDown" 
                                                        OnSelectedIndexChanged="ddlSearchEditRole_SelectedIndexChanged" 
                                                        meta:resourcekey="ddlSearchEditRoleResource1">
                                                  </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                              </tr>
                                              <tr>
                                                <td>
                                                  <asp:Label ID="lblSearchEditUser" runat="server" CssClass="cssClassFormLabel" 
                                                        Text="Search User:" meta:resourcekey="lblSearchEditUserResource1"></asp:Label>
                                                </td>
                                                <td>
                                                  <asp:TextBox ID="txtSearchUserText" runat="server" 
                                                        CssClass="cssClassNormalTextBox" meta:resourcekey="txtSearchUserTextResource1"></asp:TextBox>
                                                  <ajax:AutoCompleteExtender ID="aceSearchUserText" runat="server" Enabled="True" MinimumPrefixLength="1"
                                                    ServiceMethod="GetUsernameList" ServicePath="~/SageFrameWebService.asmx" 
                                                        TargetControlID="txtSearchUserText" DelimiterCharacters="" />
                                                </td>
                                                <td>
                                                  <asp:ImageButton ID="imgUserSearch" runat="server" CausesValidation="False" OnClick="imgUserSearch_Click"
                                                    ToolTip="Search" meta:resourcekey="imgUserSearchResource1" />
                                                </td>
                                              </tr>
                                            </table>
                                            <div class="cssClassGridWrapper">
                                              <asp:GridView ID="gdvEditUser" runat="server" AutoGenerateColumns="False" 
                                                    EmptyDataText="User not found" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gdvEditUser_PageIndexChanging"
                                                GridLines="None" OnRowDataBound="gdvEditUser_RowDataBound" 
                                                    meta:resourcekey="gdvEditUserResource1">
                                                <Columns>
                                                  <asp:TemplateField meta:resourcekey="TemplateFieldResource10">
                                                    <ItemTemplate>
                                                      <input id="chkBoxItem2" runat="server" class="cssCheckBoxItem" type="checkbox" />
                                                    </ItemTemplate>
                                                      <HeaderTemplate>
                                                          <input ID="chkBoxHeader2" runat="server" class="cssCheckBoxHeader" 
                                                              type="checkbox"></input>
                                                      </HeaderTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField meta:resourcekey="TemplateFieldResource11">
                                                    <ItemTemplate>
                                                      <asp:Label ID="lblEditUsernameD" runat="server" CssClass="cssClassFormLabel" 
                                                            Text='<%# Eval("Username") %>' meta:resourcekey="lblEditUsernameDResource1"></asp:Label>
                                                    </ItemTemplate>
                                                      <HeaderTemplate>
                                                          <asp:Label ID="lblEditUsername" runat="server" CssClass="cssClassFormLabel" 
                                                              meta:resourcekey="lblEditUsernameResource3" Text="Username"></asp:Label>
                                                      </HeaderTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField meta:resourcekey="TemplateFieldResource12">
                                                    <ItemTemplate>
                                                      <asp:Label ID="lblEditFirstNameD" runat="server" CssClass="cssClassFormLabel" 
                                                            Text='<%# Eval("FirstName") %>' meta:resourcekey="lblEditFirstNameDResource1"></asp:Label>
                                                    </ItemTemplate>
                                                      <HeaderTemplate>
                                                          <asp:Label ID="lblEditFirstName" runat="server" CssClass="cssClassFormLabel" 
                                                              meta:resourcekey="lblEditFirstNameResource1" Text="First name"></asp:Label>
                                                      </HeaderTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField meta:resourcekey="TemplateFieldResource13">
                                                    <ItemTemplate>
                                                      <asp:Label ID="lblEditLastNameD" runat="server" CssClass="cssClassFormLabel" 
                                                            Text='<%# Eval("LastName") %>' meta:resourcekey="lblEditLastNameDResource1"></asp:Label>
                                                    </ItemTemplate>
                                                      <HeaderTemplate>
                                                          <asp:Label ID="lblEditLastName" runat="server" CssClass="cssClassFormLabel" 
                                                              meta:resourcekey="lblEditLastNameResource1" Text="Last name"></asp:Label>
                                                      </HeaderTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField meta:resourcekey="TemplateFieldResource14">
                                                    <ItemTemplate>
                                                      <asp:Label ID="lblEditEmailD" runat="server" CssClass="cssClassFormLabel" 
                                                            Text='<%# Eval("Email") %>' meta:resourcekey="lblEditEmailDResource1"></asp:Label>
                                                    </ItemTemplate>
                                                      <HeaderTemplate>
                                                          <asp:Label ID="lblEditEmail" runat="server" 
                                                              meta:resourcekey="lblEditEmailResource1" Text="Email"></asp:Label>
                                                      </HeaderTemplate>
                                                  </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                                                  <HeaderStyle CssClass="cssClassHeadingOne" />
                                                  <PagerStyle CssClass="cssClassPageNumber" />
                                                  <RowStyle CssClass="cssClassAlternativeOdd" />
                                              </asp:GridView>
                                            </div>
                                          </div>
                                          <div class="cssClassButtonWrapper">
                                            <asp:ImageButton ID="imgAddSelectedEditUsers" runat="server" CausesValidation="False"
                                              OnClick="imgAddSelectedEditUsers_Click" ToolTip="Add all seleted user(s)" 
                                                  meta:resourcekey="imgAddSelectedEditUsersResource1" />
                                            <asp:Label ID="lblAddSelectedEditUsers" runat="server" AssociatedControlID="imgAddSelectedEditUsers"
                                              Style="cursor: pointer;" Text="Add all seleted user(s)" 
                                                  meta:resourcekey="lblAddSelectedEditUsersResource1"></asp:Label>
                                          </div>
                                        </div>
                                      </div>
                                    </div>
                                    </div>
                                  </asp:Panel>
                                </ContentTemplate>
                              </asp:UpdatePanel>
                            </td>
                          </tr>
                          <caption>
                          </caption>
                        </table>
                      </ContentTemplate>
                    </asp:UpdatePanel>
                  </td>
                </tr>
              </table>
            </div>
          </div>
        </div>
      </ContentTemplate>
    </ajax:TabPanel>
    <ajax:TabPanel ID="TabPanelAdvancedSettings" runat="server" 
          meta:resourcekey="TabPanelAdvancedSettingsResource1">
      <HeaderTemplate>
        <asp:Label ID="lblADS" runat="server" Text="Advanced Settings" 
              meta:resourcekey="lblADSResource1"></asp:Label>
      </HeaderTemplate>
      <ContentTemplate>
        <asp:Label ID="lblAdvancedSettingsHelp" runat="server" CssClass="cssClassHelpTitle"
          Text="In this section, you can set up more advanced settings for this page." 
              meta:resourcekey="lblAdvancedSettingsHelpResource1"></asp:Label>
        <div class="cssClassCollapseWrapper">
          <sfe:sectionheadcontrol ID="shcAppearance" runat="server" IncludeRule="true" IsExpanded="true"
            Section="tblAppearance" Text="Appearance Settings" />
          <div id="tblAppearance" runat="server" class="cssClassCollapseContent">
            <div class="cssClassFormWrapper">
              <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td width="20%">
                    <asp:Label ID="lblIcon" runat="server" CssClass="cssClassFormLabel" Text="Icon:" 
                          meta:resourcekey="lblIconResource1"></asp:Label>
                  </td>
                  <td>
                    <asp:FileUpload ID="fileIcon" runat="server" OnLoad="fileIcon_Load" 
                          meta:resourcekey="fileIconResource1" />
                    <asp:Image ID="imgIcon" runat="server" Visible="False" 
                          meta:resourcekey="imgIconResource1" />
                    <asp:Literal ID="ltErrorIcon" runat="server" EnableViewState="False" 
                          meta:resourcekey="ltErrorIconResource1"></asp:Literal>
                  </td>
                </tr>
                <tr>
                  <td>
                    <asp:Label ID="lblRefreshInterval" runat="server" CssClass="cssClassFormLabel" 
                          Text="Refresh Interval (seconds):" 
                          meta:resourcekey="lblRefreshIntervalResource1"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtRefreshInterval" runat="server" 
                          CssClass="cssClassNormalTextBox" meta:resourcekey="txtRefreshIntervalResource1"></asp:TextBox>
                  </td>
                </tr>
              </table>
            </div>
          </div>
        </div>
        <div class="cssClassCollapseWrapper">
          <sfe:sectionheadcontrol ID="shcOtherSettings" runat="server" IncludeRule="true" IsExpanded="true"
            Section="tblOtherSettings" Text="Other Settings" />
          <div id="tblOtherSettings" runat="server" class="cssClassCollapseContent">
            <div class="cssClassFormWrapper">
              <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td width="20%">
                    <asp:Label ID="lblSecure" runat="server" CssClass="cssClassFormLabel" 
                          Text="Secure?" meta:resourcekey="lblSecureResource1"></asp:Label>
                  </td>
                  <td width="20%">
                    <asp:CheckBox ID="chkSecure" runat="server" CssClass="cssClassCheckBox" 
                          meta:resourcekey="chkSecureResource1" />
                  </td>
                  <td>
                  </td>
                </tr>
                <tr id="rowSEOName" runat="server" visible="False">
                  <td runat="server">
                    <asp:Label ID="lblSEOName" runat="server" CssClass="cssClassFormLabel" Text="SEOName:"></asp:Label>
                  </td>
                  <td runat="server">
                    <asp:TextBox ID="txtSEOName" runat="server" CssClass="cssClassNormalTextBox" MaxLength="200" />
                  </td>
                  <td runat="server">
                  </td>
                </tr>
                <tr>
                  <td>
                    <asp:Label ID="lblStartDate" runat="server" CssClass="cssClassFormLabel" 
                          Text="Start Date:" meta:resourcekey="lblStartDateResource1"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtStartDate" runat="server" Columns="30" CssClass="cssClassNormalTextBox"
                      MaxLength="11" meta:resourcekey="txtStartDateResource1" />
                  </td>
                  <td>
                    <ajax:CalendarExtender ID="cmdStartCalendar" runat="server" CssClass="CssClassCalendar"
                      Enabled="True" PopupButtonID="imgbtnCalender1" PopupPosition="BottomRight" TargetControlID="txtStartDate" />
                    <div class="cssClassCalendarBtn">
                      <asp:ImageButton ID="imgbtnCalender1" runat="server" AlternateText="Click here to display calendar"
                        CausesValidation="False" meta:resourcekey="imgbtnCalender1Resource1" />
                        <asp:CompareValidator ID="valtxtStartDate" runat="server"
                          ControlToValidate="txtStartDate" CssClass="cssClassNormalRed" Display="Dynamic"
                          ErrorMessage="Invalid Start Date!" Operator="DataTypeCheck" SetFocusOnError="True"
                          Type="Date" meta:resourcekey="valtxtStartDateResource1" />
                    </div>
                  </td>
                </tr>
                <tr>
                  <td>
                    <asp:Label ID="lblEndDate" runat="server" CssClass="cssClassFormLabel" 
                          Text="End Date:" meta:resourcekey="lblEndDateResource1"></asp:Label>
                  </td>
                  <td>
                    <asp:TextBox ID="txtEndDate" runat="server" Columns="30" CssClass="cssClassNormalTextBox"
                      MaxLength="11" meta:resourcekey="txtEndDateResource1" />
                  </td>
                  <td>
                    <ajax:CalendarExtender ID="cmdEndCalendar" runat="server" CssClass="cssClassCalendar"
                      Enabled="True" PopupButtonID="imgbtnCalender2" PopupPosition="BottomRight" TargetControlID="txtEndDate" />
                    <div class="cssClassCalendarBtn1">
                      <asp:ImageButton ID="imgbtnCalender2" runat="server" AlternateText="Click here to display calendar"
                        CausesValidation="False" meta:resourcekey="imgbtnCalender2Resource1" />
                      <asp:CompareValidator ID="valtxtEndDate" runat="server" ControlToValidate="txtEndDate"
                        CssClass="cssClassNormalRed" Display="Dynamic" ErrorMessage="Invalid End Date!"
                        Operator="DataTypeCheck" SetFocusOnError="True" ValidationGroup="pagesettings"
                        Type="Date" meta:resourcekey="valtxtEndDateResource1" />
                        <asp:CompareValidator ID="val2txtEndDate" runat="server" ControlToCompare="txtStartdate"
                          ControlToValidate="txtEndDate" ErrorMessage="End Date must be greater than Start Date"
                          Operator="GreaterThan" Type="Date" 
                            meta:resourcekey="val2txtEndDateResource1" />
                    </div>
                  </td>
                </tr>
                <tr>
                  <td valign="top">
                    <asp:Label ID="lblURL" runat="server" CssClass="cssClassFormLabel" 
                          Text="Link Url:" meta:resourcekey="lblURLResource1"></asp:Label>
                  </td>
                  <td colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0">
                      <tr id="TypeRow" runat="server">
                        <td runat="server">
                          <asp:Label ID="lblURLType" runat="server" CssClass="cssClassFormLabel" EnableViewState="False"
                            Font-Bold="True" Text="Link Type:" /><br />
                          <div class="cssClassButtonListWrapper">
                            <asp:RadioButtonList ID="rbURlType" runat="server" CssClass="cssClassRadioButtonList"
                              RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                          </div>
                        </td>
                      </tr>
                      <tr id="URLRow" runat="server">
                        <td runat="server">
                          <asp:Label ID="lblURLTitle" runat="server" CssClass="cssClassFormLabel" /><br />
                          <asp:TextBox ID="txtUrl" runat="server" CssClass="cssClassNormalTextBox" /><br />
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid URL"
                            Text="Invalid URL" ControlToValidate="txtUrl" ValidationGroup="pagesettings" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                          <ajax:TextBoxWatermarkExtender ID="URLWatermark" runat="server" TargetControlID="txtUrl"
                            WatermarkCssClass="cssClassWaterMark" WatermarkText="http://www.example.com" 
                                Enabled="True">
                          </ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </div>
          </div>
        </div>
      </ContentTemplate>
    </ajax:TabPanel>
    <ajax:TabPanel ID="TabPanelPageModuleSettings" runat="server" 
          meta:resourcekey="TabPanelPageModuleSettingsResource1">
      <HeaderTemplate>
        <asp:Label ID="lblPMSS" runat="server" Text="Page Module Settings" 
              meta:resourcekey="lblPMSSResource1"></asp:Label>
      </HeaderTemplate>
      <ContentTemplate>
        <asp:Label ID="lblPageModuleSettingsHelp" runat="server" CssClass="cssClassHelpTitle"
          Text="In this section, you can add or manage page modules in this page." 
              meta:resourcekey="lblPageModuleSettingsHelpResource1"></asp:Label>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <asp:Panel ID="pnlModulePermission" runat="server" Visible="False" 
                  meta:resourcekey="pnlModulePermissionResource1">
              <div id="UserModulePermissionSetting">
                <asp:Label ID="lblPageModulePermissionsHelpModule" runat="server" CssClass="cssClassHelpTitle"
                  
                      Text="In this section, you can set up the page modules permissions settings for this page" 
                      meta:resourcekey="lblPageModulePermissionsHelpModuleResource1"></asp:Label>
                <div class="cssClassCollapseWrapper">
                  <sfe:sectionheadcontrol ID="shcPageModuleViewPermissionsSettingsModule" runat="server"
                    IncludeRule="true" IsExpanded="true" Section="tblPageModuleViewPermissions" Text="View Page Module Permissions Settings" />
                  <div id="tblPageModuleViewPermissions" runat="server" class="cssClassCollapseContent">
                    <div class="cssClassFormWrapper">
                      <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                          <td width="20%">
                            <asp:UpdatePanel ID="udpViewUserRolesModule" runat="server">
                              <ContentTemplate>
                                <asp:Label ID="lblMessageViewModuleUserRole" runat="server" CssClass="cssClassNormalTitle"
                                  Text="View Permissions" 
                                      meta:resourcekey="lblMessageViewModuleUserRoleResource1"></asp:Label>
                                <table border="0" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>
                                      <asp:Label ID="lblUnselectedRolesModule" runat="server" CssClass="cssClassFormLabel"
                                        Text="Unselected" meta:resourcekey="lblUnselectedRolesModuleResource1"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                      <asp:Label ID="lblSelectedRolesModule" runat="server" CssClass="cssClassFormLabel"
                                        Text="Selected" meta:resourcekey="lblSelectedRolesModuleResource1"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td valign="top" width="162px" class="cssClassSelectPadding">
                                      <asp:ListBox ID="lstUnselectedViewRolesModule" runat="server" CssClass="cssClassFormList"
                                        Height="200px" SelectionMode="Multiple" 
                                            meta:resourcekey="lstUnselectedViewRolesModuleResource1"></asp:ListBox>
                                    </td>
                                    <td class="cssClassSelectLeftRight" valign="top">
                                      <asp:Button ID="btnAddAllRoleViewModule" runat="server" CausesValidation="False"
                                        CssClass="cssClassSelectAllRight" OnClick="btnAddAllRoleViewModule_Click" 
                                            Text="&gt;&gt;" meta:resourcekey="btnAddAllRoleViewModuleResource1" /><br />
                                      <asp:Button ID="btnAddRoleViewModule" runat="server" CausesValidation="False" CssClass="cssClassSelectOneRight"
                                        OnClick="btnAddRoleViewModule_Click" Text=" &gt; " 
                                            meta:resourcekey="btnAddRoleViewModuleResource1" /><br />
                                      <asp:Button ID="btnRemoveRoleViewModule" runat="server" CausesValidation="False"
                                        CssClass="cssClassSelectOneLeft" OnClick="btnRemoveRoleViewModule_Click" 
                                            Text=" &lt; " meta:resourcekey="btnRemoveRoleViewModuleResource1" /><br />
                                      <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="cssClassSelectAllLeft"
                                        OnClick="btnRemoveAllRoleViewModule_Click" Text="&lt;&lt;" 
                                            meta:resourcekey="Button1Resource1" />
                                    </td>
                                    <td valign="top" width="200px">
                                      <asp:ListBox ID="lstSelectedRolesViewModule" runat="server" CssClass="cssClassFormList"
                                        Height="200px" SelectionMode="Multiple" 
                                            meta:resourcekey="lstSelectedRolesViewModuleResource1"></asp:ListBox>
                                    </td>
                                    <td valign="top">
                                      <asp:UpdatePanel ID="udpViewPageModulePermissionUsername" runat="server">
                                        <ContentTemplate>
                                          <asp:Panel ID="pnlAddUsersViewModule" runat="server" Style="display: block" 
                                                meta:resourcekey="pnlAddUsersViewModuleResource1">
                                            <div class="cssClassUserPermission"><div class="cssClassUserPermissionInside">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                              <tr>
                                                <td width="20%">
                                                  <asp:Label ID="lblViewUsernameModule" runat="server" CssClass="cssClassFormLabel"
                                                    Text="Username: " meta:resourcekey="lblViewUsernameModuleResource1"></asp:Label>
                                                </td>
                                                <td>
                                                  <asp:TextBox ID="txtViewUsernameModule" runat="server" CssClass="cssClassNormalTextBox"
                                                    Width="150px" meta:resourcekey="txtViewUsernameModuleResource1"></asp:TextBox><asp:RequiredFieldValidator ID="rfvViewUsernameModule"
                                                      runat="server" ErrorMessage="Username is required" Text="*" ControlToValidate="txtViewUsernameModule"
                                                      ValidationGroup="PageModulePermissionView" 
                                                        meta:resourcekey="rfvViewUsernameModuleResource1"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                  <div class="cssClassButtonWrapper">
                                                    <asp:ImageButton ID="imbAddUserViewPermissionModule" runat="server" ValidationGroup="PageModulePermissionView"
                                                      OnClick="imbAddUserViewPermissionModule_Click" 
                                                          meta:resourcekey="imbAddUserViewPermissionModuleResource1" />
                                                    <asp:Label ID="lblAddNewUserViewModule" runat="server" AssociatedControlID="imbAddUserViewPermissionModule"
                                                      Style="cursor: pointer;" Text="Add" 
                                                          meta:resourcekey="lblAddNewUserViewModuleResource1"></asp:Label>
                                                    <asp:ImageButton ID="imbSelectUsersModule" runat="server" 
                                                          CausesValidation="False" OnClick="imbSelectUsersModule_Click"
                                                      ToolTip="Search" meta:resourcekey="imbSelectUsersModuleResource1" />
                                                    <asp:Label ID="lblSelectUsersModule" runat="server" AssociatedControlID="imbSelectUsersModule"
                                                      Style="cursor: pointer;" Text="Search" 
                                                          meta:resourcekey="lblSelectUsersModuleResource1"></asp:Label>
                                                  </div>
                                                </td>
                                              </tr>
                                              <tr>
                                                <td colspan="3">
                                                  <div class="cssClassViewUsers">
                                                    <asp:GridView ID="gdvViewUsernamesModule" runat="server" AutoGenerateColumns="False"
                                                      GridLines="None" OnRowDataBound="gdvViewUsernamesModule_RowDataBound" OnRowCommand="gdvViewUsernamesModule_RowCommand" 
                                                          ShowHeader="False" meta:resourcekey="gdvViewUsernamesModuleResource1">
                                                      <Columns>
                                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource15">
                                                          <ItemTemplate>
                                                            <asp:Label ID="lblUsernameModule" runat="server" Text='<%# Eval("Username") %>' 
                                                                  meta:resourcekey="lblUsernameModuleResource1"></asp:Label>
                                                          </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource16">
                                                          <ItemTemplate>
                                                            <asp:ImageButton ID="imbDeleteModule" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Username") %>'
                                                              CommandName="RemoveUser" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                                                              meta:resourcekey="imbDeleteModuleResource1" />
                                                          </ItemTemplate>
                                                            <HeaderStyle CssClass="cssClassColumnDelete" />
                                                        </asp:TemplateField>
                                                      </Columns>
                                                      <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                                                        <HeaderStyle CssClass="cssClassHeadingOne" />
                                                        <RowStyle CssClass="cssClassAlternativeOdd" />
                                                    </asp:GridView>
                                                  </div>
                                                </td>
                                              </tr>
                                            </table></div></div>
                                          </asp:Panel>
                                          <asp:Panel ID="pnlUsersViewModule" runat="server" CssClass="searchPopup" Height="100%"
                                            Visible="False" meta:resourcekey="pnlUsersViewModuleResource1">
                                            <div class="cssClassOnClickPopUp">
                                              <div class="cssClassOnClickPopUpClose">
                                                <asp:Button ID="imbCancelViewModule" runat="server" CausesValidation="False" OnClick="imbCancelViewModule_Click"
                                                  Text="Cancel" meta:resourcekey="imbCancelViewModuleResource1" />
                                              </div>
                                              <div class="cssClassOnClickPopUpInside">
                                                <div class="cssClassOnClickPopUpTitle">
                                                  <asp:Label ID="lblSUModuleModule" runat="server" Text="Select Users:" 
                                                        meta:resourcekey="lblSUModuleModuleResource1"></asp:Label>
                                                </div>
                                                <div class="cssClassOnClickPopUpInfo">
                                                  <div class="cssClassFormWrapper">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                      <tr>
                                                        <td width="126px">
                                                          <asp:Label ID="lblSearchUserRoleModule" runat="server" CssClass="cssClassFormLabel"
                                                            Text="Search user in role:" 
                                                                meta:resourcekey="lblSearchUserRoleModuleResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:DropDownList ID="ddlSearchRoleModule" runat="server" AutoPostBack="True"
                                                            CssClass="cssClassDropDown" 
                                                                OnSelectedIndexChanged="ddlSearchRoleModule_SelectedIndexChanged" 
                                                                meta:resourcekey="ddlSearchRoleModuleResource1">
                                                          </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                        </td>
                                                      </tr>
                                                      <tr>
                                                        <td>
                                                          <asp:Label ID="lblSearchUserModule" runat="server" CssClass="cssClassFormLabel" 
                                                                Text="Search User:" meta:resourcekey="lblSearchUserModuleResource1"></asp:Label>
                                                        </td>
                                                        <td width="170px">
                                                          <asp:TextBox ID="txtSearchTextModule" runat="server" 
                                                                CssClass="cssClassNormalTextBox" 
                                                                meta:resourcekey="txtSearchTextModuleResource1"></asp:TextBox>
                                                          <ajax:AutoCompleteExtender ID="aceSearchTextModule" runat="server" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetUsernameList" ServicePath="~/SageFrameWebService.asmx"
                                                            TargetControlID="txtSearchTextModule" DelimiterCharacters="" />
                                                        </td>
                                                        <td>
                                                          <asp:ImageButton ID="imgSearchUsersViewModule" runat="server" CausesValidation="False"
                                                            OnClick="imgSearchUsersViewModule_Click" ToolTip="Search" 
                                                                meta:resourcekey="imgSearchUsersViewModuleResource1" />
                                                        </td>
                                                      </tr>
                                                    </table>
                                                    <div class="cssClassGridWrapper">
                                                      <asp:GridView ID="gdvUserModule" runat="server" AutoGenerateColumns="False" 
                                                            EmptyDataText="User not found" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gdvUserModule_PageIndexChanging"
                                                        GridLines="None" OnRowDataBound="gdvUserModule_RowDataBound" 
                                                            meta:resourcekey="gdvUserModuleResource1">
                                                        <Columns>
                                                          <asp:TemplateField meta:resourcekey="TemplateFieldResource17">
                                                            <ItemTemplate>
                                                              <input id="chkBoxItem" runat="server" class="cssCheckBoxItem" type="checkbox" />
                                                            </ItemTemplate>
                                                              <HeaderTemplate>
                                                                  <input ID="chkBoxHeader" runat="server" class="cssCheckBoxHeader" 
                                                                      type="checkbox"></input>
                                                              </HeaderTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField meta:resourcekey="TemplateFieldResource18">
                                                            <ItemTemplate>
                                                              <asp:Label ID="lblUsernameD" runat="server" CssClass="cssClassFormLabel" 
                                                                    Text='<%# Eval("Username") %>' meta:resourcekey="lblUsernameDResource2"></asp:Label>
                                                            </ItemTemplate>
                                                              <HeaderTemplate>
                                                                  <asp:Label ID="lblUsername" runat="server" CssClass="cssClassFormLabel" 
                                                                      meta:resourcekey="lblUsernameResource3" Text="Username"></asp:Label>
                                                              </HeaderTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField meta:resourcekey="TemplateFieldResource19">
                                                            <ItemTemplate>
                                                              <asp:Label ID="lblFirstNameD" runat="server" CssClass="cssClassFormLabel" 
                                                                    Text='<%# Eval("FirstName") %>' meta:resourcekey="lblFirstNameDResource2"></asp:Label>
                                                            </ItemTemplate>
                                                              <HeaderTemplate>
                                                                  <asp:Label ID="lblFirstName" runat="server" CssClass="cssClassFormLabel" 
                                                                      meta:resourcekey="lblFirstNameResource2" Text="First name"></asp:Label>
                                                              </HeaderTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField meta:resourcekey="TemplateFieldResource20">
                                                            <ItemTemplate>
                                                              <asp:Label ID="lblLastNameD" runat="server" CssClass="cssClassFormLabel" 
                                                                    Text='<%# Eval("LastName") %>' meta:resourcekey="lblLastNameDResource2"></asp:Label>
                                                            </ItemTemplate>
                                                              <HeaderTemplate>
                                                                  <asp:Label ID="lblLastName" runat="server" CssClass="cssClassFormLabel" 
                                                                      meta:resourcekey="lblLastNameResource2" Text="Last name"></asp:Label>
                                                              </HeaderTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField meta:resourcekey="TemplateFieldResource21">
                                                            <ItemTemplate>
                                                              <asp:Label ID="lblEmailD" runat="server" CssClass="cssClassFormLabel" 
                                                                    Text='<%# Eval("Email") %>' meta:resourcekey="lblEmailDResource2"></asp:Label>
                                                            </ItemTemplate>
                                                              <HeaderTemplate>
                                                                  <asp:Label ID="lblEmail" runat="server" meta:resourcekey="lblEmailResource2" 
                                                                      Text="Email"></asp:Label>
                                                              </HeaderTemplate>
                                                          </asp:TemplateField>
                                                        </Columns>
                                                        <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                                                          <HeaderStyle CssClass="cssClassHeadingOne" />
                                                          <PagerStyle CssClass="cssClassPageNumber" />
                                                          <RowStyle CssClass="cssClassAlternativeOdd" />
                                                      </asp:GridView>
                                                    </div>
                                                  </div>
                                                  <div class="cssClassButtonWrapper">
                                                    <asp:ImageButton ID="imgAddSelectedUsersModule" runat="server" CausesValidation="False"
                                                      OnClick="imgAddSelectedUsersModule_Click" ToolTip="Add all seleted user(s)" 
                                                          meta:resourcekey="imgAddSelectedUsersModuleResource1" />
                                                    <asp:Label ID="lblAddSelectedUsersModule" runat="server" AssociatedControlID="imgAddSelectedUsersModule"
                                                      Style="cursor: pointer;" Text="Add all seleted user(s)" 
                                                          meta:resourcekey="lblAddSelectedUsersModuleResource1"></asp:Label>
                                                  </div>
                                                </div>
                                              </div>
                                            </div>
                                            </div>
                                          </asp:Panel>
                                        </ContentTemplate>
                                      </asp:UpdatePanel>
                                    </td>
                                  </tr>
                                </table>
                              </ContentTemplate>
                            </asp:UpdatePanel>
                          </td>
                        </tr>
                      </table>
                    </div>
                  </div>
                </div>
                <div class="cssClassCollapseWrapper">
                  <sfe:sectionheadcontrol ID="shcPageEditPermissionsSettingsModule" runat="server"
                    IncludeRule="true" IsExpanded="true" Section="tblPageModuleEditPermissions" Text="Edit Page Permissions Settings" />
                  <div id="tblPageModuleEditPermissions" runat="server" class="cssClassCollapseContent">
                    <div class="cssClassFormWrapper">
                      <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                          <td>
                            <asp:UpdatePanel ID="udpEditUserRolesModule" runat="server">
                              <ContentTemplate>
                                <asp:Label ID="lblMessageEditUserRoleModule" runat="server" CssClass="cssClassNormalTitle"
                                  Text="Edit Permissions" 
                                      meta:resourcekey="lblMessageEditUserRoleModuleResource1"></asp:Label>
                                <table border="0" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>
                                      <asp:Label ID="lblUnselected1Module" runat="server" CssClass="cssClassFormLabel"
                                        Text="Unselected" meta:resourcekey="lblUnselected1ModuleResource1"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                      <asp:Label ID="lblSelected1Module" runat="server" CssClass="cssClassFormLabel" 
                                            Text="Selected" meta:resourcekey="lblSelected1ModuleResource1"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td valign="top" width="162px" class="cssClassSelectPadding">
                                      <asp:ListBox ID="lstUnselectedEditRolesModule" runat="server" CssClass="cssClassFormList"
                                        Height="200px" SelectionMode="Multiple" 
                                            meta:resourcekey="lstUnselectedEditRolesModuleResource1"></asp:ListBox>
                                    </td>
                                    <td class="cssClassSelectLeftRight" valign="top">
                                      <asp:Button ID="btnAddAllRoleEditModule" runat="server" CausesValidation="False"
                                        CssClass="cssClassSelectAllRight" OnClick="btnAddAllRoleEditModule_Click" 
                                            Text="&gt;&gt;" meta:resourcekey="btnAddAllRoleEditModuleResource1" /><br />
                                      <asp:Button ID="btnAddRoleEditModule" runat="server" CausesValidation="False" CssClass="cssClassSelectOneRight"
                                        OnClick="btnAddRoleEditModule_Click" Text=" &gt; " 
                                            meta:resourcekey="btnAddRoleEditModuleResource1" /><br />
                                      <asp:Button ID="btnRemoveRoleEditModule" runat="server" CausesValidation="False"
                                        CssClass="cssClassSelectOneLeft" OnClick="btnRemoveRoleEditModule_Click" 
                                            Text=" &lt; " meta:resourcekey="btnRemoveRoleEditModuleResource1" /><br />
                                      <asp:Button ID="btnRemoveAllRoleEditModule" runat="server" CausesValidation="False"
                                        CssClass="cssClassSelectAllLeft" OnClick="btnRemoveAllRoleEditModule_Click" 
                                            Text="&lt;&lt;" meta:resourcekey="btnRemoveAllRoleEditModuleResource1" />
                                    </td>
                                    <td valign="top" width="200px">
                                      <asp:ListBox ID="lstSelectedRolesEditModule" runat="server" CssClass="cssClassFormList"
                                        Height="200px" SelectionMode="Multiple" 
                                            meta:resourcekey="lstSelectedRolesEditModuleResource1"></asp:ListBox>
                                    </td>
                                    <td valign="top">
                                      <asp:UpdatePanel ID="udpEditPagePermissionModule" runat="server">
                                        <ContentTemplate>
                                          <asp:Panel ID="pnlAddUsersEditModule" runat="server" Style="display: block" 
                                                meta:resourcekey="pnlAddUsersEditModuleResource1">
                                            <div class="cssClassUserPermission"><div class="cssClassUserPermissionInside">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                              <tr>
                                                <td width="20%">
                                                  <asp:Label ID="lblEditUsernameModule" runat="server" CssClass="cssClassFormLabel"
                                                    Text="Username: " meta:resourcekey="lblEditUsernameModuleResource1"></asp:Label>
                                                </td>
                                                <td>
                                                  <asp:TextBox ID="txtEditUsernameModule" runat="server" CssClass="cssClassNormalTextBox"
                                                    Width="150px" meta:resourcekey="txtEditUsernameModuleResource1"></asp:TextBox><asp:RequiredFieldValidator ID="rfvEditUsernameModule"
                                                      runat="server" ErrorMessage="Username is required" Text="*" ControlToValidate="txtEditUsernameModule"
                                                      ValidationGroup="PageModulePermissionEdit" 
                                                        meta:resourcekey="rfvEditUsernameModuleResource1"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                  <div class="cssClassButtonWrapper">
                                                    <asp:ImageButton ID="imbAddUserEditPermissionModule" runat="server" OnClick="imbAddUserEditPermissionModule_Click"
                                                      ValidationGroup="PageModulePermissionEdit" 
                                                          meta:resourcekey="imbAddUserEditPermissionModuleResource1" />
                                                    <asp:Label ID="lblAddUserEditPermissionModule" runat="server" AssociatedControlID="imbAddUserEditPermissionModule"
                                                      Style="cursor: pointer;" Text="Add" 
                                                          meta:resourcekey="lblAddUserEditPermissionModuleResource1"></asp:Label>
                                                    <asp:ImageButton ID="imbSelectEditUsersModule" runat="server" 
                                                          CausesValidation="False" OnClick="imbSelectEditUsersModule_Click"
                                                      ToolTip="Search" meta:resourcekey="imbSelectEditUsersModuleResource1" />
                                                    <asp:Label ID="lblSelectEditUsersModule" runat="server" AssociatedControlID="imbSelectEditUsersModule"
                                                      Style="cursor: pointer;" Text="Search" 
                                                          meta:resourcekey="lblSelectEditUsersModuleResource1"></asp:Label>
                                                  </div>
                                                </td>
                                              </tr>
                                              <tr>
                                                <td colspan="3">
                                                  <div class="cssClassEditUsers">
                                                    <asp:GridView ID="gdvEditUsernamesModule" runat="server" AutoGenerateColumns="False"
                                                      DataKeyNames="Username" GridLines="None" OnRowDataBound="gdvEditUsernamesModule_RowDataBound" OnRowCommand="gdvEditUsernamesModule_RowCommand"
                                                      ShowHeader="False" meta:resourcekey="gdvEditUsernamesModuleResource1">
                                                      <Columns>
                                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource22">
                                                          <ItemTemplate>
                                                            <asp:Label ID="lblEditUsernameModule" runat="server" 
                                                                  Text='<%# Eval("Username") %>' 
                                                                  meta:resourcekey="lblEditUsernameModuleResource2"></asp:Label>
                                                          </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource23">
                                                          <ItemTemplate>
                                                            <asp:ImageButton ID="imbDelete2Module" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Username") %>'
                                                              CommandName="RemoveUser" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                                                              meta:resourcekey="imbDelete2ModuleResource1" />
                                                          </ItemTemplate>
                                                            <HeaderStyle CssClass="cssClassColumnDelete" />
                                                        </asp:TemplateField>
                                                      </Columns>
                                                      <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                                                        <HeaderStyle CssClass="cssClassHeadingOne" />
                                                        <RowStyle CssClass="cssClassAlternativeOdd" />
                                                    </asp:GridView>
                                                  </div>
                                                </td>
                                              </tr>
                                            </table></div></div>
                                          </asp:Panel>
                                          <asp:Panel ID="pnlUsersEditModule" runat="server" CssClass="searchPopup" 
                                                Height="100%" Visible="False" meta:resourcekey="pnlUsersEditModuleResource1">
                                            <div class="cssClassOnClickPopUp">
                                              <div class="cssClassOnClickPopUpClose">
                                                <asp:Button ID="imbCancelEditModule" runat="server" CausesValidation="False" OnClick="imbCancelEditModule_Click"
                                                  Text="Cancel" meta:resourcekey="imbCancelEditModuleResource1" />
                                              </div>
                                              <div class="cssClassOnClickPopUpInside">
                                                <div class="cssClassOnClickPopUpTitle">
                                                  <asp:Label ID="lblSUSPUTModule" runat="server" Text="Select Users:" 
                                                        meta:resourcekey="lblSUSPUTModuleResource1"></asp:Label>
                                                </div>
                                                <div class="cssClassOnClickPopUpInfo">
                                                  <div class="cssClassFormWrapper">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                      <tr>
                                                        <td>
                                                          <asp:Label ID="lblSearchEditUserRoleModule" runat="server" CssClass="cssClassFormLabel"
                                                            Text="Search user in role:" 
                                                                meta:resourcekey="lblSearchEditUserRoleModuleResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:DropDownList ID="ddlSearchEditRoleModule" runat="server" AutoPostBack="True" 
                                                                CssClass="cssClassDropDown" 
                                                                OnSelectedIndexChanged="ddlSearchEditRoleModule_SelectedIndexChanged" 
                                                                meta:resourcekey="ddlSearchEditRoleModuleResource1">
                                                          </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                        </td>
                                                      </tr>
                                                      <tr>
                                                        <td>
                                                          <asp:Label ID="lblSearchEditUserModule" runat="server" CssClass="cssClassFormLabel"
                                                            Text="Search User:" meta:resourcekey="lblSearchEditUserModuleResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <asp:TextBox ID="txtSearchUserTextModule" runat="server"
                                                            CssClass="cssClassNormalTextBox" 
                                                                meta:resourcekey="txtSearchUserTextModuleResource1"></asp:TextBox>
                                                          <ajax:AutoCompleteExtender ID="aceSearchUserTextModule" runat="server" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetUsernameList" ServicePath="~/SageFrameWebService.asmx"
                                                            TargetControlID="txtSearchUserTextModule" DelimiterCharacters="" />
                                                        </td>
                                                        <td>
                                                          <asp:ImageButton ID="imgUserSearchModule" runat="server" CausesValidation="False"
                                                            OnClick="imgUserSearchModule_Click" ToolTip="Search" 
                                                                meta:resourcekey="imgUserSearchModuleResource1" />
                                                        </td>
                                                      </tr>
                                                    </table>
                                                    <div class="cssClassGridWrapper">
                                                      <asp:GridView ID="gdvEditUserModule" runat="server" AutoGenerateColumns="False" 
                                                            EmptyDataText="User not found" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gdvEditUserModule_PageIndexChanging"
                                                        GridLines="None" OnRowDataBound="gdvEditUserModule_RowDataBound" 
                                                            meta:resourcekey="gdvEditUserModuleResource1">
                                                        <Columns>
                                                          <asp:TemplateField meta:resourcekey="TemplateFieldResource24">
                                                            <ItemTemplate>
                                                              <input id="chkBoxItem2" runat="server" class="cssCheckBoxItem" type="checkbox" />
                                                            </ItemTemplate>
                                                              <HeaderTemplate>
                                                                  <input ID="chkBoxHeader2" runat="server" class="cssCheckBoxHeader" 
                                                                      type="checkbox"></input>
                                                              </HeaderTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField meta:resourcekey="TemplateFieldResource25">
                                                            <ItemTemplate>
                                                              <asp:Label ID="lblEditUsernameD" runat="server" CssClass="cssClassFormLabel" 
                                                                    Text='<%# Eval("Username") %>' meta:resourcekey="lblEditUsernameDResource2"></asp:Label>
                                                            </ItemTemplate>
                                                              <HeaderTemplate>
                                                                  <asp:Label ID="lblEditUsername" runat="server" CssClass="cssClassFormLabel" 
                                                                      meta:resourcekey="lblEditUsernameResource4" Text="Username"></asp:Label>
                                                              </HeaderTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField meta:resourcekey="TemplateFieldResource26">
                                                            <ItemTemplate>
                                                              <asp:Label ID="lblEditFirstNameD" runat="server" CssClass="cssClassFormLabel" 
                                                                    Text='<%# Eval("FirstName") %>' meta:resourcekey="lblEditFirstNameDResource2"></asp:Label>
                                                            </ItemTemplate>
                                                              <HeaderTemplate>
                                                                  <asp:Label ID="lblEditFirstName" runat="server" CssClass="cssClassFormLabel" 
                                                                      meta:resourcekey="lblEditFirstNameResource2" Text="First name"></asp:Label>
                                                              </HeaderTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField meta:resourcekey="TemplateFieldResource27">
                                                            <ItemTemplate>
                                                              <asp:Label ID="lblEditLastNameD" runat="server" CssClass="cssClassFormLabel" 
                                                                    Text='<%# Eval("LastName") %>' meta:resourcekey="lblEditLastNameDResource2"></asp:Label>
                                                            </ItemTemplate>
                                                              <HeaderTemplate>
                                                                  <asp:Label ID="lblEditLastName" runat="server" CssClass="cssClassFormLabel" 
                                                                      meta:resourcekey="lblEditLastNameResource2" Text="Last name"></asp:Label>
                                                              </HeaderTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField meta:resourcekey="TemplateFieldResource28">
                                                            <ItemTemplate>
                                                              <asp:Label ID="lblEditEmailD" runat="server" CssClass="cssClassFormLabel" 
                                                                    Text='<%# Eval("Email") %>' meta:resourcekey="lblEditEmailDResource2"></asp:Label>
                                                            </ItemTemplate>
                                                              <HeaderTemplate>
                                                                  <asp:Label ID="lblEditEmail" runat="server" 
                                                                      meta:resourcekey="lblEditEmailResource2" Text="Email"></asp:Label>
                                                              </HeaderTemplate>
                                                          </asp:TemplateField>
                                                        </Columns>
                                                        <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                                                          <HeaderStyle CssClass="cssClassHeadingOne" />
                                                          <PagerStyle CssClass="cssClassPageNumber" />
                                                          <RowStyle CssClass="cssClassAlternativeOdd" />
                                                      </asp:GridView>
                                                    </div>
                                                  </div>
                                                  <div class="cssClassButtonWrapper">
                                                    <asp:ImageButton ID="imgAddSelectedEditUsersModule1" runat="server" CausesValidation="False"
                                                      OnClick="imgAddSelectedEditUsersModule_Click" 
                                                          ToolTip="Add all seleted user(s)" 
                                                          meta:resourcekey="imgAddSelectedEditUsersModule1Resource1" />
                                                    <asp:Label ID="Label1" runat="server" AssociatedControlID="imgAddSelectedEditUsersModule1"
                                                      Style="cursor: pointer;" Text="Add all seleted user(s)" 
                                                          meta:resourcekey="Label1Resource1"></asp:Label>
                                                  </div>
                                                </div>
                                              </div>
                                            </div>
                                            </div>
                                          </asp:Panel>
                                        </ContentTemplate>
                                      </asp:UpdatePanel>
                                    </td>
                                  </tr>
                                  <caption>
                                  </caption>
                                </table>
                              </ContentTemplate>
                            </asp:UpdatePanel>
                          </td>
                        </tr>
                      </table>
                    </div>
                  </div>
                </div>
                <div class="cssClassButtonWrapper">
                  <asp:ImageButton ID="ibSavePermission" runat="server" 
                        OnClick="ibSavePermission_Click" meta:resourcekey="ibSavePermissionResource1" />
                  <asp:Label ID="lblSavePermission" runat="server" Text="Save" AssociatedControlID="ibSavePermission"
                    Style="cursor: pointer;" meta:resourcekey="lblSavePermissionResource1"></asp:Label>
                  <asp:ImageButton ID="ibCancelPermission" runat="server" CausesValidation="False"
                    OnClick="ibCancelPermission_Click" 
                        meta:resourcekey="ibCancelPermissionResource1" />
                  <asp:Label ID="lblCancelPermission" runat="server" Text="Cancel" AssociatedControlID="ibCancelPermission"
                    Style="cursor: pointer;" meta:resourcekey="lblCancelPermissionResource1"></asp:Label>
                </div>
              </div>
            </asp:Panel>
            <asp:Panel ID="pnlModules" runat="server" Visible="False" 
                  meta:resourcekey="pnlModulesResource1">
              <div class="cssClassFormWrapper">
                <table border="0" cellpadding="0" cellspacing="0">
                  <tr>
                    <td>
                      <asp:Label ID="lblModule" runat="server" CssClass="cssClassFormLabel" 
                            meta:resourcekey="lblModuleResource1" />
                    </td>
                    <td>
                      <asp:DropDownList ID="cboModules" runat="server" CssClass="cssClassDropDown" 
                            meta:resourcekey="cboModulesResource1" />
                    </td>
                    <td>
                      <asp:Label ID="lblPane" runat="server" CssClass="cssClassFormLabel" 
                            meta:resourcekey="lblPaneResource1" />
                    </td>
                    <td>
                      <asp:DropDownList ID="cboPanes" runat="server" AutoPostBack="True" CssClass="cssClassDropDown"
                        OnSelectedIndexChanged="cboPanes_SelectedIndexChanged" 
                            meta:resourcekey="cboPanesResource1" />
                      <asp:HiddenField ID="hdnPane" runat="server" />
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lblModuleTitle" runat="server" CssClass="cssClassFormLabel" 
                            meta:resourcekey="lblModuleTitleResource1" />
                    </td>
                    <td>
                      <asp:TextBox ID="txtModuleTitle" runat="server" CssClass="cssClassNormalTextBox" 
                            meta:resourcekey="txtModuleTitleResource1" />
                    </td>
                    <td>
                      <asp:Label ID="lblPosition" runat="server" CssClass="cssClassFormLabel" 
                            meta:resourcekey="lblPositionResource1" />
                    </td>
                    <td>
                      <asp:DropDownList ID="cboPosition" runat="server" AutoPostBack="True" CssClass="cssClassDropDown"
                        OnSelectedIndexChanged="cboPosition_SelectedIndexChanged" 
                            meta:resourcekey="cboPositionResource1" />
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lblPermission" runat="server" CssClass="cssClassFormLabel" 
                            meta:resourcekey="lblPermissionResource1" />
                    </td>
                    <td>
                      <asp:DropDownList ID="cboPermission" runat="server" CssClass="cssClassDropDown" 
                            meta:resourcekey="cboPermissionResource1">
                        <asp:ListItem Text="Same As Page" Value="0" 
                              meta:resourcekey="ListItemResource1" />
                        <asp:ListItem Text="Page Editors Only" Value="1" 
                              meta:resourcekey="ListItemResource2" />
                      </asp:DropDownList>
                    </td>
                    <td>
                      <asp:Label ID="lblInstance" runat="server" CssClass="cssClassFormLabel" 
                            meta:resourcekey="lblInstanceResource1" />
                    </td>
                    <td>
                      <asp:DropDownList ID="cboInstances" runat="server" CssClass="cssClassDropDown" 
                            meta:resourcekey="cboInstancesResource1">
                      </asp:DropDownList>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lblShowInAllPages" runat="server" CssClass="cssClassFormLabel" 
                            meta:resourcekey="lblShowInAllPagesResource1" />
                    </td>
                    <td>
                      <asp:CheckBox ID="chkShowInAllPages" runat="server" 
                            meta:resourcekey="chkShowInAllPagesResource1" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                  </tr>
                </table>
              </div>
              <div class="cssClassButtonWrapper">
                <asp:ImageButton ID="ibModuleControlSave" runat="server" CausesValidation="False"
                  OnClick="ibModuleControlSave_Click" 
                      meta:resourcekey="ibModuleControlSaveResource1" />
                <asp:Label ID="lblAddModule" runat="server" AssociatedControlID="ibModuleControlSave"
                  Style="cursor: pointer" Text="Add Module To Page" 
                      meta:resourcekey="lblAddModuleResource1"></asp:Label>
                <asp:ImageButton ID="ibModuleControlCancel" runat="server" CausesValidation="False"
                  OnClick="ibModuleControlCancel_Click" 
                      meta:resourcekey="ibModuleControlCancelResource1" />
                <asp:Label ID="lblModuleControlCancel" runat="server" AssociatedControlID="ibModuleControlCancel"
                  Style="cursor: pointer;" Text="Cancel" 
                      meta:resourcekey="lblModuleControlCancelResource1"></asp:Label>
              </div>
            </asp:Panel>
            <asp:Panel ID="pnlModuleControls" runat="server" Visible="False" 
                  meta:resourcekey="pnlModuleControlsResource1">
              <div class="cssClassButtonWrapper">
                <asp:ImageButton ID="imbAddModules" runat="server" CausesValidation="False" 
                      OnClick="imbAddModules_Click" meta:resourcekey="imbAddModulesResource1" />
                <asp:Label ID="lblAddModules" runat="server" AssociatedControlID="imbAddModules"
                  Style="cursor: pointer;" Text="Add New Module" 
                      meta:resourcekey="lblAddModulesResource1"></asp:Label>
              </div>
              <div class="cssClassGridWrapper">
                <asp:GridView ID="gdvUserModules" runat="server" AutoGenerateColumns="False" GridLines="None"
                  OnRowCancelingEdit="gdvUserModules_RowCancelingEdit" OnRowCommand="gdvUserModules_RowCommand"
                  OnRowDataBound="gdvUserModules_RowDataBound" OnRowEditing="gdvUserModules_RowEditing"
                  OnRowUpdating="gdvUserModules_RowUpdating" Width="100%" 
                      meta:resourcekey="gdvUserModulesResource1">
                  <Columns>
                    <asp:TemplateField HeaderText="SNo" Visible="False" 
                          meta:resourcekey="TemplateFieldResource29">
                      <ItemTemplate>
                        <asp:Label ID="lblSNo" runat="server" Text='<%# Eval("SNo") %>' 
                              meta:resourcekey="lblSNoResource1"></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PageID" Visible="False" 
                          meta:resourcekey="TemplateFieldResource30">
                      <ItemTemplate>
                        <asp:Label ID="lblPageID" runat="server" Text='<%# Eval("PageID") %>' 
                              meta:resourcekey="lblPageIDResource1"></asp:Label>
                        <asp:Label ID="lblMax" runat="server" Text='<%# Eval("MaxModuleOrder") %>' 
                              meta:resourcekey="lblMaxResource1"></asp:Label>
                        <asp:Label ID="lblMin" runat="server" Text='<%# Eval("MinModuleOrder") %>' 
                              meta:resourcekey="lblMinResource1"></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UserModuleID" Visible="False" 
                          meta:resourcekey="TemplateFieldResource31">
                      <ItemTemplate>
                        <asp:Label ID="lblUserModuleID" runat="server" Text='<%# Eval("UserModuleID") %>' 
                              meta:resourcekey="lblUserModuleIDResource1"></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ModuleID" Visible="False" 
                          meta:resourcekey="TemplateFieldResource32">
                      <ItemTemplate>
                        <asp:Label ID="lblModuleID" runat="server" Text='<%# Eval("ModuleID") %>' 
                              meta:resourcekey="lblModuleIDResource1"></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title" meta:resourcekey="TemplateFieldResource33">
                      <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>' 
                              meta:resourcekey="lblTitleResource2"></asp:Label>
                      </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtModuleTitle" runat="server" 
                                meta:resourcekey="txtModuleTitleResource2" Text='<%# Eval("Title") %>'></asp:TextBox>
                        </EditItemTemplate>
                      <HeaderStyle Width="45%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Visibility" Visible="False" 
                          meta:resourcekey="TemplateFieldResource34">
                      <ItemTemplate>
                        <asp:Label ID="lblVisibility" runat="server" Text='<%# Eval("Visibility") %>' 
                              meta:resourcekey="lblVisibilityResource1"></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Position" Visible="False" 
                          meta:resourcekey="TemplateFieldResource35">
                      <ItemTemplate>
                        <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>' 
                              meta:resourcekey="lblPositionResource2"></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="InstanceID" Visible="False" 
                          meta:resourcekey="TemplateFieldResource36">
                      <ItemTemplate>
                        <asp:Label ID="lblInstanceID" runat="server" Text='<%# Eval("InstanceID") %>' 
                              meta:resourcekey="lblInstanceIDResource1"></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="InheritView" Visible="False" 
                          meta:resourcekey="TemplateFieldResource37">
                      <ItemTemplate>
                        <asp:Label ID="lblInheritView" runat="server" 
                              Text='<%# ConvetVisibility(Convert.ToBoolean(Eval("InheritView"))) %>' 
                              meta:resourcekey="lblInheritViewResource1"></asp:Label>
                        <asp:Label ID="lblInheritView1" runat="server" Text='<%# Eval("InheritView") %>'
                          Visible="False" meta:resourcekey="lblInheritView1Resource1"></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:DropDownList ID="ddlVisibility" runat="server" 
                              meta:resourcekey="ddlVisibilityResource1">
                        </asp:DropDownList>
                        <asp:Label ID="lblVisibility1" runat="server" Text='<%# Eval("InheritView") %>' 
                              Visible="False" meta:resourcekey="lblVisibility1Resource1"></asp:Label>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pane" meta:resourcekey="TemplateFieldResource38">
                      <ItemTemplate>
                        <asp:Label ID="lblPaneName" runat="server" Text='<%# Eval("PaneName") %>' 
                              meta:resourcekey="lblPaneNameResource1"></asp:Label>
                      </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlPaneName" runat="server" 
                                meta:resourcekey="ddlPaneNameResource1">
                            </asp:DropDownList>
                            <asp:Label ID="lblPaneName1" runat="server" 
                                meta:resourcekey="lblPaneName1Resource1" Text='<%# Eval("PaneName") %>' 
                                Visible="False"></asp:Label>
                        </EditItemTemplate>
                      <HeaderStyle Width="20%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="In All Pages" 
                          meta:resourcekey="TemplateFieldResource39">
                      <ItemTemplate>
                        <asp:HiddenField ID="hdnAllPages" runat="server" Value='<%# Eval("AllPages") %>' />
                        <input id="chkBoxAllPagesItem" runat="server" class="cssCheckBoxAllPagesItem" type="checkbox" />
                      </ItemTemplate>
                      <HeaderStyle Width="8%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Active?" 
                          meta:resourcekey="TemplateFieldResource40">
                      <ItemTemplate>
                        <asp:HiddenField ID="hdnIsActive" runat="server" Value='<%# Eval("IsActive") %>' />
                        <input id="chkBoxIsActiveItem" runat="server" class="cssCheckBoxIsActiveItem" type="checkbox" />
                      </ItemTemplate>
                      <HeaderStyle Width="6%" CssClass="cssClassColumnIsActive" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ModuleOrder"
                      Visible="False" meta:resourcekey="TemplateFieldResource41">
                      <ItemTemplate>
                        <asp:Label ID="lblModuleOrder" runat="server" Text='<%# Eval("ModuleOrder") %>' 
                              meta:resourcekey="lblModuleOrderResource1"></asp:Label>
                      </ItemTemplate>
                        <HeaderStyle CssClass="cssClassColumnOrder" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" meta:resourcekey="TemplateFieldResource42">
                      <ItemTemplate>
                        <div>
                          <asp:ImageButton ID="imgUp" runat="server" CausesValidation="False" CommandArgument='<%# Eval("UserModuleID") %>'
                            CommandName="Up" ImageUrl='<%# GetTemplateImageUrl("imgup.png", true) %>' 
                                ToolTip="Up" meta:resourcekey="imgUpResource1" />
                        </div>
                        <div>
                          <asp:ImageButton ID="imgDown" runat="server" CausesValidation="False" CommandArgument='<%# Eval("UserModuleID") %>'
                            CommandName="Down" ImageUrl='<%# GetTemplateImageUrl("imgdown.png", true) %>' 
                                ToolTip="Down" meta:resourcekey="imgDownResource1" />
                        </div>
                      </ItemTemplate>
                        <HeaderStyle CssClass="cssClassColumnOrder" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" meta:resourcekey="TemplateFieldResource43">
                      <ItemTemplate>
                        <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("UserModuleID") %>'
                          CommandName="Edit" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>' 
                              ToolTip="Edit" meta:resourcekey="imbEditResource1" />
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:ImageButton ID="imbUpdate" runat="server" CommandName="Update" ImageUrl='<%# GetTemplateImageUrl("imgsave.png", true) %>'
                          ToolTip="Update" meta:resourcekey="imbUpdateResource1" />
                        <asp:ImageButton ID="imbCancels" runat="server" CausesValidation="False" CommandName="Cancel"
                          ImageUrl='<%# GetTemplateImageUrl("imgcancel.png", true) %>' ToolTip="Cancel" 
                              meta:resourcekey="imbCancelsResource1" />
                      </EditItemTemplate>
                        <HeaderStyle CssClass="cssClassColumnEdit" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" meta:resourcekey="TemplateFieldResource44">
                      <ItemTemplate>
                        <asp:ImageButton ID="imbEditUserModulePermission" runat="server" CausesValidation="False"
                          CommandArgument='<%# Eval("UserModuleID") %>' CommandName="EditUserModulePermission"
                          ImageUrl='<%# GetTemplateImageUrl("imgEditUserModulePermission.png", true) %>'
                          ToolTip="Change Module Permission" 
                              meta:resourcekey="imbEditUserModulePermissionResource1" />
                      </ItemTemplate>
                        <HeaderStyle CssClass="cssClassColumnEdit" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" meta:resourcekey="TemplateFieldResource45">
                      <ItemTemplate>
                        <asp:ImageButton ID="imbDeleteUsermodule" runat="server" CausesValidation="False"
                          CommandArgument='<%# Eval("UserModuleID") %>' CommandName="DeleteUserModule" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                          ToolTip="Delete" 
                              meta:resourcekey="imbDeleteUsermoduleResource1" />
                        <asp:ImageButton ID="imbDelete3" runat="server" CausesValidation="False" CommandArgument='<%# Eval("SNo") %>'
                          CommandName="RemoveUserModule" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                          ToolTip="Delete" 
                              meta:resourcekey="imbDeleteResource2" />
                      </ItemTemplate>
                        <HeaderStyle CssClass="cssClassColumnDelete" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource46">
                      <ItemTemplate>
                        <asp:ImageButton ID="imbEditControl" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ModuleID")+","+Eval("UserModuleID")+","+Eval("Title") %>'
                          CommandName="LoadEditControl" ImageUrl='<%# GetTemplateImageUrl("imgEditControl.png", true) %>'
                          ToolTip="Edit this PageModule" meta:resourcekey="imbEditControlResource1" />
                      </ItemTemplate>
                        <HeaderStyle CssClass="cssClassColumnEdit" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource47">
                      <ItemTemplate>
                        <asp:ImageButton ID="imbSettingsControl" runat="server" CausesValidation="False"
                          CommandArgument='<%# Eval("ModuleID")+","+Eval("UserModuleID")+","+Eval("Title") %>'
                          CommandName="LoadSettingsControl" ImageUrl='<%# GetTemplateImageUrl("imgsetting.png", true) %>'
                          ToolTip="Change the Settings of this PageModule" 
                              meta:resourcekey="imbSettingsControlResource1"></asp:ImageButton>
                      </ItemTemplate>
                        <HeaderStyle CssClass="cssClassColumnEdit" />
                    </asp:TemplateField>
                  </Columns>
                  <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                    <HeaderStyle CssClass="cssClassHeadingOne" />
                    <RowStyle CssClass="cssClassAlternativeOdd" />
                </asp:GridView>
              </div>
            </asp:Panel>
          </ContentTemplate>
        </asp:UpdatePanel>
      </ContentTemplate>
    </ajax:TabPanel>
  </ajax:TabContainer>
  <div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
          ValidationGroup="pagesettings" meta:resourcekey="ValidationSummary1Resource1" />
  </div>
  <div class="cssClassButtonWrapper">
    <asp:ImageButton ID="ibSave" runat="server" OnClick="ibSave_Click" 
          ValidationGroup="pagesettings" meta:resourcekey="ibSaveResource1" />
    <asp:Label ID="lblSave" runat="server" Text="Save" AssociatedControlID="ibSave" 
          Style="cursor: pointer;" meta:resourcekey="lblSaveResource1"></asp:Label>
    <asp:ImageButton ID="ibCancel" runat="server" CausesValidation="False" 
          OnClick="ibCancel_Click" meta:resourcekey="ibCancelResource1" />
    <asp:Label ID="lblCancel" runat="server" Text="Cancel" AssociatedControlID="ibCancel"
      Style="cursor: pointer;" meta:resourcekey="lblCancelResource1"></asp:Label>
  </div>
  <div id="auditBar" runat="server" class="cssClassAuditBar" visible="False">
    <asp:Label ID="lblCreatedBy" runat="server" CssClass="cssClassFormLabel" 
          Visible="False" meta:resourcekey="lblCreatedByResource1" /><br />
    <asp:Label ID="lblUpdatedBy" runat="server" CssClass="cssClassFormLabel" 
          Visible="False" meta:resourcekey="lblUpdatedByResource1" />
  </div>
</asp:Panel>
<asp:Panel ID="pnlPageList" runat="server" 
    meta:resourcekey="pnlPageListResource1">
  <div class="cssClassButtonWrapper">
    <asp:ImageButton ID="ibAdd" runat="server" CausesValidation="False" 
          OnClick="ibAdd_Click" meta:resourcekey="ibAddResource1" />
    <asp:Label ID="lblAddPage" runat="server" Text="Add Page" AssociatedControlID="ibAdd"
      Style="cursor: pointer;" meta:resourcekey="lblAddPageResource1"></asp:Label>
    <asp:ImageButton ID="imbPageSave" runat="server" CausesValidation="False" 
          OnClick="imbPageSave_Click" meta:resourcekey="imbPageSaveResource1" />
    <asp:Label ID="lblPageSave" runat="server" Text="Save Changes" AssociatedControlID="imbPageSave"
      Style="cursor: pointer;" meta:resourcekey="lblPageSaveResource1"></asp:Label>
  </div>
  <div class="cssClassGridWrapper">
    <asp:GridView ID="gdvPageList" runat="server" AutoGenerateColumns="False" OnRowCommand="gdvPageList_RowCommand"
      GridLines="None" OnRowDataBound="gdvPageList_RowDataBound" Width="100%" 
          meta:resourcekey="gdvPageListResource1">
      <Columns>
        <asp:TemplateField HeaderText="Page Name" 
              meta:resourcekey="TemplateFieldResource48">
          <ItemTemplate>
            <asp:HiddenField ID="hdnPageID" runat="server" Value='<%# Eval("PageID") %>' />
            <asp:HiddenField ID="hdnIsRequiredPage" runat="server" Value='<%# Eval("IsRequiredPage") %>' />
            <asp:LinkButton ID="lnkPageName" runat="server" CommandArgument='<%# Eval("PageID") + "," + Eval("IsActive") %>'
              CommandName="PageEdit" Text='<%# Eval("PageName") %>' 
                  meta:resourcekey="lnkPageNameResource1"></asp:LinkButton>
          </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="PageName" HeaderText="Page Title" 
              meta:resourcekey="BoundFieldResource1" />
        <asp:BoundField DataField="IsSecure" HeaderText="Is Secure?" 
              meta:resourcekey="BoundFieldResource2" >
            <HeaderStyle CssClass="cssClassColumnAddedOn" />
          </asp:BoundField>
        <asp:BoundField DataField="AddedOn" DataFormatString="{0:yyyy/MM/dd}" 
              HeaderText="Added On" meta:resourcekey="BoundFieldResource3" >
            <HeaderStyle CssClass="cssClassColumnAddedOn" />
          </asp:BoundField>
        <asp:BoundField DataField="UpdatedOn" DataFormatString="{0:yyyy/MM/dd}" 
              HeaderText="Updated On" meta:resourcekey="BoundFieldResource4" >
            <HeaderStyle CssClass="cssClassColumnUpdatedOn" />
          </asp:BoundField>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource49">
          <ItemTemplate>
            <asp:HiddenField ID="hdnIsActive" runat="server" Value='<%# Eval("IsActive") %>' />
            <input id="chkBoxIsActiveItem" runat="server" class="cssCheckBoxIsActiveItem" type="checkbox" />
          </ItemTemplate>
            <HeaderTemplate>
                <input ID="chkBoxIsActiveHeader" runat="server" 
                    class="cssCheckBoxIsActiveHeader" type="checkbox"></input>
                <asp:Label ID="lblIsActive" runat="server" 
                    meta:resourcekey="lblIsActiveResource1" Text="Is Active"></asp:Label>
            </HeaderTemplate>
          <HeaderStyle CssClass="cssClassColumnIsActive" />
        </asp:TemplateField>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource50">
          <ItemTemplate>
            <div>
              <asp:ImageButton ID="imbMovePageUp" ImageUrl='<%# GetTemplateImageUrl("imgup.png", true) %>'
                runat="server" CommandName="PageUp" AlternateText="Move Up" 
                    CommandArgument='<%# Eval("PageID") %>' 
                    meta:resourcekey="imbMovePageUpResource1" />
            </div>
            <div>
              <asp:ImageButton ID="imbMovePageDown" ImageUrl='<%# GetTemplateImageUrl("imgdown.png", true) %>'
                runat="server" CommandName="PageDown" AlternateText="Move Down" 
                    CommandArgument='<%# Eval("PageID") %>' 
                    meta:resourcekey="imbMovePageDownResource1" />
            </div>
          </ItemTemplate>
            <HeaderStyle CssClass="cssClassColumnOrder" />
        </asp:TemplateField>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource51">
          <ItemTemplate>
            <asp:ImageButton ID="imbEditPage" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>'
              runat="server" CommandName="PageEdit" AlternateText="Edit" 
                  CommandArgument='<%# Eval("PageID") + "," + Eval("IsActive") %>' 
                  meta:resourcekey="imbEditPageResource1" />
            <asp:HiddenField ID="hdfPageOrder" Value='<%# Eval("PageOrder") %>' runat="server" />
            <asp:HiddenField ID="hdfMaxPageOrder" Value='<%# Eval("MaxPageOrder") %>' runat="server" />
            <asp:HiddenField ID="hdfMinPageOrder" Value='<%# Eval("MinPageOrder") %>' runat="server" />
          </ItemTemplate>
            <HeaderStyle CssClass="cssClassColumnEdit" />
        </asp:TemplateField>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource52">
          <ItemTemplate>
            <asp:ImageButton ID="imbDeletePage" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
              runat="server" CommandName="PageDelete" AlternateText="Delete" CommandArgument='<%# Eval("PageID") %>'
              meta:resourcekey="imbDeletePageResource1" />
          </ItemTemplate>
            <HeaderStyle CssClass="cssClassColumnDelete" />
        </asp:TemplateField>
      </Columns>
      <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
        <HeaderStyle CssClass="cssClassHeadingOne" />
        <RowStyle CssClass="cssClassAlternativeOdd" />
    </asp:GridView>
  </div>
</asp:Panel>
