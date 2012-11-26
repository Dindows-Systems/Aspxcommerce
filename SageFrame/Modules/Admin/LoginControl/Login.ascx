<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login.ascx.cs" Inherits="SageFrame.Modules.Admin.LoginControl.Login" %>
<%@ Register Src="../../../Controls/LoginStatus.ascx" TagName="LoginStatus" TagPrefix="uc1" %>

<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
  <asp:View ID="View1" runat="server">
    <div class="cssClassloginbox">
      <div class="cssClassloginboxInside">
        <div class="cssClassloginboxInsideDetails">
          <div class="cssClassLoginLeftBox">
            <div class="cssClassadminloginHeading">
              <h1>
                <asp:Label ID="lblAdminLogin" runat="server" Text="Login" 
                      meta:resourcekey="lblAdminLoginResource1"></asp:Label>
              </h1>
            </div>
            <div class="cssClassadminloginInfo">
              <table border="0" cellpadding="0" width="100%" class="cssClassnormalborder">
                <tr>
                  <td colspan="2"><asp:Label ID="UserNameLabel" runat="server" 
                          AssociatedControlID="UserName" CssClass="cssClassNormalText" 
                          meta:resourcekey="UserNameLabelResource1">User Name:</asp:Label></td>
                </tr>
                <tr>
                  <td colspan="2"><p class="cssClassTextBox">
                      <asp:TextBox ID="UserName" runat="server" meta:resourcekey="UserNameResource1"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                ErrorMessage="User Name is required." 
                          ToolTip="User Name is required." ValidationGroup="Login1"
                                CssClass="cssClassusernotfound" 
                          meta:resourcekey="UserNameRequiredResource1">*</asp:RequiredFieldValidator>
                    </p></td>
                </tr>
                <tr>
                  <td colspan="2"><asp:Label ID="PasswordLabel" runat="server" 
                          AssociatedControlID="Password" CssClass="cssClassNormalText" 
                          meta:resourcekey="PasswordLabelResource1">Password:</asp:Label></td>
                </tr>
                <tr>
                  <td colspan="2"><p class="cssClassTextBox">
                      <asp:TextBox ID="Password" runat="server" TextMode="Password" 
                          meta:resourcekey="PasswordResource1"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1"
                                CssClass="cssClassusernotfound" 
                          meta:resourcekey="PasswordRequiredResource1">*</asp:RequiredFieldValidator>
                    </p></td>
                </tr>
                <tr>
                  <td width="118"><table width="118" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="18"><asp:CheckBox ID="RememberMe" runat="server" 
                                CssClass="cssClassCheckBox" meta:resourcekey="RememberMeResource1" /></td>
                        <td><asp:Label ID="lblrmnt" runat="server" Text="Remember me." 
                                CssClass="cssClassRemember" meta:resourcekey="lblrmntResource1"></asp:Label></td>
                      </tr>
                      <tr>
                        <td colspan="2"><span class="cssClassForgetPass">
                          <asp:HyperLink ID="hypForgetPassword" runat="server" Text="Forgot Password?" 
                                meta:resourcekey="hypForgetPasswordResource1"></asp:HyperLink>
                          </span></td>
                      </tr>
                    </table></td>
                  <td width="120"><div class="cssClassButtonWrapper"><span><span>
                      <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Sign In" ValidationGroup="Login1"
                                OnClick="LoginButton_Click" 
                          meta:resourcekey="LoginButtonResource1" />
                      </span></span></div></td>
                </tr>
                <tr>
                  <td colspan="2" class="cssClassusernotfound"><asp:Literal ID="FailureText" 
                          runat="server" EnableViewState="False" meta:resourcekey="FailureTextResource1"></asp:Literal></td>
                </tr>
              </table>
            </div>
          </div>
          <div class="cssClassLoginRighttBox"  runat="server" id="divSignUp" >
            <h2><span>New here?</span></h2>
            <p><a href="/User-Registration.aspx" runat="server" id="signup">Sign up</a> for a
              new account</p>
            <div class="cssClassNewSIgnUp"  ><span>»</span> <a href="/User-Registration.aspx"  runat="server" id="signup1">Sign up</a> </div>
          </div>
        </div>
      </div>
    </div>
  </asp:View>
  <asp:View ID="View2" runat="server">
    <uc1:LoginStatus ID="LoginStatus1" runat="server" />
  </asp:View>
</asp:MultiView>
