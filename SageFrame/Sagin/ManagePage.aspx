<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePage.aspx.cs" Inherits="SageFrame.Sagin.ManagePage" %>

<%@ Register Src="~/Controls/ctl_HeaderControl.ascx" TagName="ctl_HeaderControl"
  TagPrefix="uc1" %>
<%@ Register Src="~/Controls/LoginStatus.ascx" TagName="LoginStatus" TagPrefix="uc8" %>
<%@ Register Src="~/Controls/ctl_Footer.ascx" TagName="ctl_Footer" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/ctl_AdministratorLink.ascx" TagName="ctl_AdministratorLink"
  TagPrefix="uc6" %>
<%@ Register Src="~/Controls/ctl_AdminMenuOnly.ascx" TagName="ctl_AdminMenuOnly"
  TagPrefix="uc7" %>
<%@ Register Src="../Controls/ctl_CPanelHead.ascx" TagName="ctl_CPanelHead" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ctl_CPanleFooter.ascx" TagName="ctl_CPanleFooter" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head">
  <link type="icon shortcut" media="icon" href="favicon.ico" />
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
  <!--[if lt IE 8]><script type="text/javascript" src="/js/SageFrameCorejs/excanvas.js"></script><![endif]>
  <!--[if IE]><link rel="stylesheet" href="../css/IE.css" type="text/css" media="screen" /><![endif]-->
  <!--[if lt IE 7]> <script type="text/javascript" src="/js/SageFrameCorejs/IE8.js"></script><![endif]-->
  <link id="SageFrameCssMenuResource" runat="server" rel="stylesheet" type="text/css" />
  <link id="SageFrameCSSTemplate" runat="server" rel="stylesheet" type="text/css" />
  <link id="SageFrameCSSLayout" runat="server" rel="stylesheet" type="text/css" />
  <asp:Literal ID="SageFrameModuleCSSlinks" runat="server"></asp:Literal>
  <title>SageFrame Website</title>
</head>
<body onload="__loadScript();">
  <form id="form2" runat="server" enctype="multipart/form-data">
  <asp:ScriptManager ID="ScriptManager1" runat="server" LoadScriptsBeforeUI="false"
    ScriptMode="Release">
    <Services>
      <asp:ServiceReference Path="~/SageFrameWebService.asmx" />
    </Services>
  </asp:ScriptManager>
  <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
    <ProgressTemplate>
      <div class="cssClassLoadingBG">
        &nbsp;</div>
      <div class="cssClassloadingDiv">
        <asp:Image ID="imgPrgress" runat="server" AlternateText="Loading..." ToolTip="Loading..." /><br />
        <asp:Label ID="lblPrgress" runat="server" Text="Please wait..."></asp:Label>
      </div>
    </ProgressTemplate>
  </asp:UpdateProgress>
  <div id="wrap">
    <div id="main">
      <div class="cssClassAdminControlPanel" id="divAdminControlPanel" runat="server" style="display: block;">
        <div class="cssClassAdminControlPanelContent">
          <div class="loginStatus">
            <asp:Literal ID="litUserName" runat="server" Text="Loged In as"></asp:Literal>&nbsp;<strong><%= Page.User == null ? "" : Page.User.Identity.Name %></strong>
          </div>
          <div class="cssClassAdminMenu">
            <asp:PlaceHolder ID="phdAdminMenu" runat="server"></asp:PlaceHolder>
          </div>
          <div class="cssClassLogout">
            <uc8:LoginStatus ID="LoginStatus1" runat="server" />
          </div>
          <div class="cssClassadminHome">
            <asp:HyperLink ID="hypHome" runat="server" Target="_blank" />
            <asp:HyperLink ID="hypPreview" runat="server" Text="Preview" Target="_blank"></asp:HyperLink>
          </div>
          <div class="cssClass">
            <asp:PlaceHolder ID="HeaderPane" runat="server"></asp:PlaceHolder>
          </div>
        </div>
      </div>
      <div class="cssClassOuterMainWrapper">
        <div class="cssClassHeaderWrapper">
          <div class="cssClassHeaderWrapperContent">
            <!--CPanel Head-->
            <uc2:ctl_CPanelHead ID="ctl_CPanelHead1" runat="server" />
            <!--End of CPanel Head-->
          </div>
          <noscript>
            <asp:Label ID="lblnoScript" runat="server" Text="This page requires java-script to be enabled. Please adjust your browser-settings."></asp:Label></noscript>
          <%--<div class="cssClassHeaderWrapperContent">
                    <div id="divHeaderContent" runat="server">
                        <div class="cssClassHeaderLeft">
                            <asp:PlaceHolder ID="HeaderLeftPane" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="cssClassHeaderCenter">
                            <asp:PlaceHolder ID="HeaderCenterPane" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="cssClassHeaderRight">
                            <asp:PlaceHolder ID="HeaderRightPane" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="cssClassclear"></div>
                    </div>
                </div>--%>
        </div>
        <div class="cssClassTopWrapper">
          <div class="cssClassTopWrapperCotent">
            <asp:PlaceHolder ID="TopPane" runat="server"></asp:PlaceHolder>
          </div>
        </div>
        <div class="cssClassAdminBreadCrum">
          <asp:PlaceHolder ID="phdAdministrativBreadCrumb" runat="server"></asp:PlaceHolder>
        </div>
        <div class="cssClassBodyContentWrapper">
          <div class="cssClassBodyContent">
            <div id="divCenterContent" runat="server">
              <div class="cssClassMasterLeft">
                <asp:PlaceHolder ID="LeftPane" runat="server"></asp:PlaceHolder>
              </div>
              <div class="cssClassMasterCenter">
                <div class="cssClassButtonWrapperTop">
                  <div class="cssClassButtonWrapper">
                    <asp:Button ID="btnBackTop" runat="server" Text="Return Back" OnClick="btnBack_Click"
                      CssClass="cssClassButtonEditor" CausesValidation="false" />
                  </div>
                </div>
                <br class="cssClassclear" />
                <asp:PlaceHolder ID="ContentPane" runat="server"></asp:PlaceHolder>
                <asp:PlaceHolder ID="PlaceHolderExtension" runat="server" Visible="true"></asp:PlaceHolder>
                <div class="cssClassButtonWrapperBottom">
                  <div class="cssClassButtonWrapper">
                    <asp:Button ID="btnBack" runat="server" Text="Return Back" OnClick="btnBack_Click"
                      CssClass="cssClassButtonEditor" CausesValidation="false" />
                  </div>
                </div>
              </div>
              <div class="cssClassMasterRight">
                <asp:PlaceHolder ID="RightPane" runat="server"></asp:PlaceHolder>
              </div>
            </div>
          </div>
        </div>
        <div class="cssClassBottomWrapper">
          <div class="cssClassBottomContent">
            <asp:PlaceHolder ID="BottomPane" runat="server"></asp:PlaceHolder>
          </div>
        </div>
      </div>
      <%-- <div class="cssClassFooterWrapper" id="divFooterWrapper" runat="server">
            <div class="cssClassfooter">
                <div id="divFooterContent" runat="server">
                    <div class="cssClassFooterLeft">
                        <asp:PlaceHolder ID="FooterLeftPane" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="cssClassFooterCenter">
                        <asp:PlaceHolder ID="FooterCenterPane" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="cssClassFooterRight">
                        <asp:PlaceHolder ID="FooterRightPane" runat="server"></asp:PlaceHolder>
                    </div>
                     <div class="cssClassclear"></div>
                </div>
            </div>
        </div>--%>
    </div>
  </div>
  <div class="stickFooterWrapper" id="divFooterWrapper" runat="server">
    <div class="cssClassfooter">
      <!--CPanel Footer-->
      <uc3:ctl_CPanleFooter ID="ctl_CPanleFooter1" runat="server" />
      <!--End of CPanel Footer-->
    </div>
  </div>
  <asp:Literal ID="LitSageScript" runat="server"></asp:Literal>
  </form>
</body>
</html>
