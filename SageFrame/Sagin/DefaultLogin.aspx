<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultLogin.aspx.cs" Inherits="SageFrame.Sagin_DefaultLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type" />
    <meta content="text/javascript" http-equiv="Content-Script-Type" />
    <meta content="text/css" http-equiv="Content-Style-Type" />
    <meta id="MetaDescription" runat="Server" name="DESCRIPTION" />
    <meta id="MetaKeywords" runat="Server" name="KEYWORDS" />
    <meta id="MetaCopyright" runat="Server" name="COPYRIGHT" />
    <meta id="MetaGenerator" runat="Server" name="GENERATOR" />
    <meta id="MetaAuthor" runat="Server" name="AUTHOR" />
    <meta name="RESOURCE-TYPE" content="DOCUMENT" />
    <meta name="DISTRIBUTION" content="GLOBAL" />
    <meta id="MetaRobots" runat="server" name="ROBOTS" />
    <meta name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />
    <meta http-equiv="PAGE-ENTER" content="RevealTrans(Duration=0,Transition=1)" />
    <link id="SageFrameCssMenuResource" runat="server" rel="stylesheet" type="text/css" />
    <link id="SageFrameCSSTemplate" runat="server" rel="stylesheet" type="text/css" />
    <link id="SageFrameCSSLayout" runat="server" rel="stylesheet" type="text/css" />
    <asp:Literal ID="SageFrameModuleCSSlinks" runat="server"></asp:Literal>
    <title>SageFrame Website</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="loginbox">
        <asp:Login ID="Login1" runat="server" LoginButtonText="Sign In" OnLoggedIn="Login1_LoggedIn">
            <LayoutTemplate>
                <div class="cssClassloginbox">
                    <table width="350" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="200">
                            </td>
                        </tr>
                        <tr>
                            <td height="50">
                                <h1>
                                    <asp:Label ID="lblAdminLogin" runat="server" Text="Login"></asp:Label></h1>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssClassloginboxbg">
                                <table border="0" cellpadding="0" width="100%" class="cssClassnormalborder">
                                    <tr>
                                        <td>
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="cssClassnotmaltext">User Name:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1"
                                                CssClass="cssClassusernotfound">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="cssClassnotmaltext">Password:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1"
                                                CssClass="cssClassusernotfound">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hypForgetPassword" runat="server">Foget Password?</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="RememberMe" runat="server" CssClass="cssClassCheckBox" />
                                                    </td>
                                                    <td>
                                                        <span class="cssClassnotmaltext">Remember me next time.</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Sign In" ValidationGroup="Login1"
                                                CssClass="cssClasssubmitbtn" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="cssClassusernotfound">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </LayoutTemplate>
        </asp:Login>
    </div>    
    <asp:Literal ID="LitSageScript" runat="server"></asp:Literal>
    </form>
</body>
</html>
