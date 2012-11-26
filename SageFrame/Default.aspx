<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs"  Inherits="SageFrame._Default"%>

<%@ Register Src="~/Controls/ctl_AdministratorLink.ascx" TagName="ctl_AdministratorLink"
    TagPrefix="uc6" %>
<%@ Register Src="~/Controls/ctl_AdminMenuOnly.ascx" TagName="ctl_AdminMenuOnly"
    TagPrefix="uc7" %>
<%@ Register Src="Controls/LoginStatus.ascx" TagName="LoginStatus" TagPrefix="uc1" %>
<%@ Register Src="Controls/ctl_HeaderControl.ascx" TagName="ctl_HeaderControl" TagPrefix="uc2" %>
<%@ Register Src="Controls/ctl_CPanelHead.ascx" TagName="ctl_CPanelHead" TagPrefix="CPHead" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head" enableviewstate="false">
    <link type="icon shortcut" media="icon" href="favicon.ico" />
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type" />
    <meta content="text/javascript" http-equiv="Content-Script-Type" />
    <meta content="text/css" http-equiv="Content-Style-Type" />
    <meta id="MetaDescription" name="DESCRIPTION" />
    <meta id="MetaKeywords" name="KEYWORDS" />
    <meta id="MetaCopyright" name="COPYRIGHT" />
    <meta id="MetaGenerator" name="GENERATOR" />
    <meta id="MetaAuthor" name="AUTHOR" />
    <meta name="RESOURCE-TYPE" content="DOCUMENT" />
    <meta name="DISTRIBUTION" content="GLOBAL" />
    <meta id="MetaRobots" runat="server" name="ROBOTS" />
    <meta name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />
    <meta http-equiv="PAGE-ENTER" content="RevealTrans(Duration=0,Transition=1)" />   
    <!--[if IE 8]><script type="text/javascript" src="/js/SageFrameCorejs/excanvas.js"></script><![endif]-->
    <!--[if IE]><link rel="stylesheet" href="/css/IE.css" type="text/css" media="screen" /><![endif]-->
    <!--[if IE 7]><script type="text/javascript" src="/js/SageFrameCorejs/IE8.js"></script><![endif]-->    
    <link id="SageFrameCssMenuResource" runat="server" enableviewstate="false" rel="stylesheet"
        type="text/css" />
    <link id="SageFrameCSSTemplate" runat="server" rel="stylesheet" enableviewstate="false"
        type="text/css" />
    <link id="SageFrameCSSLayout" runat="server" rel="stylesheet" enableviewstate="false"
        type="text/css" />
    <asp:Literal ID="SageFrameModuleCSSlinks" EnableViewState="false" runat="server"></asp:Literal>
    <title>SageFrame Website</title>
</head>
<body onload="__loadScript();">
    <form id="form1" runat="server" enctype="multipart/form-data">
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
                <asp:Image ID="imgPrgress" runat="server" AlternateText="Loading..." ToolTip="Loading..." />
                <br />
                <asp:Label ID="lblPrgress" runat="server" Text="Please wait..."></asp:Label>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="frontwrap">
        <div id="frontmain">
            <div class="cssClassAdminControlPanel" id="divAdminControlPanel" runat="server" style="display: block;">
                <div class="cssClassAdminControlPanelContent">
                    <div class="cssClassHome">
                        <asp:HyperLink ID="hypHome" runat="server" />
                        <asp:HyperLink ID="hypPreview" runat="server" Text="Home"></asp:HyperLink>
                    </div>
                    <div class="cssClassAdminMenu">
                        <asp:PlaceHolder ID="phdAdminMenu" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="cssClassLogout">
                        <uc1:LoginStatus ID="LoginStatus1" runat="server" />
                    </div>
                    <div class="cssClass">
                        <asp:PlaceHolder ID="HeaderPane" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
            </div>
            <div class="cssClassOuterMainWrapper">
                <div class="cssClassHeaderWrapper">
                    <div class="cssClassHeaderWrapperContent">
                        <div id="divHeaderContent" runat="server">
                            <!--CPanel Head-->
                            <CPHead:ctl_CPanelHead ID="ctl_CPanelHead1" runat="server" />
                            <!--End of CPanel Head-->
                            <div class="cssClassHeaderLeft">
                                <asp:PlaceHolder ID="HeaderLeftPane" runat="server"></asp:PlaceHolder>
                            </div>
                            <noscript>
                                <asp:Label ID="lblnoScript" runat="server" Text="This page requires java-script to be enabled. Please adjust your browser-settings."></asp:Label>
                            </noscript>
                            <div class="cssClassHeaderCenter">
                                <asp:PlaceHolder ID="HeaderCenterPane" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="cssClassHeaderRight">
                                <asp:PlaceHolder ID="HeaderRightPane" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="cssClassclear">
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divTopWrapper" runat="server" class="cssClassTopWrapper">
                    <div class="cssClassTopWrapperCotent">
                        <asp:PlaceHolder ID="TopPane" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
                <div class="cssClassBodyContentWrapper">
                    <div class="cssClassBodyContent">
                        <div id="divCenterContent" runat="server">
                            <div class="cssClassMasterLeft">
                                <asp:PlaceHolder ID="LeftPane" runat="server"></asp:PlaceHolder>
                                <div class="cssClassLeftNaviBtn">
                                </div>
                            </div>
                            <div class="cssClassMasterCenter">
                                <asp:PlaceHolder ID="ContentPane" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="cssClassMasterRight">
                                <asp:PlaceHolder ID="RightPane" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="cssClassclear">
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
        </div>
    </div>
    <div class="cssClassFooterWrapper" id="divFooterWrapper" runat="server">
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
                <div class="cssClassclear">
                </div>
            </div>
        </div>
    </div>
    <asp:Literal ID="LitSageScript" runat="server"></asp:Literal>
    </form>
</body>
</html>
