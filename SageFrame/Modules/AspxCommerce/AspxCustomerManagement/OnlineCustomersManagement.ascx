<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OnlineCustomersManagement.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxCustomerManagement_OnlineCustomersManagement" %>
<script type="text/javascript">
    //<![CDATA[
   
//]]>
</script>

<div id="divAttrForm">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblAttrFormHeading" runat="server" Text="Online Customers"></asp:Label>
            </h2>
        </div>
        <div class="cssClassTabPanelTable">
            <div id="container-7">
                <ul>
                    <li><a href="#fragment-1">
                        <asp:Label ID="lblTabTitle1" runat="server" Text="Registered Customers"></asp:Label>
                    </a></li>
                    <li><a href="#fragment-2">
                        <asp:Label ID="lblTabTitle2" runat="server" Text="Anonymous Users"></asp:Label>
                    </a></li>
                </ul>
                <div id="fragment-1">
                    <div class="cssClassFormWrapper">
                        <div id="divRegisteredUsers">
                            <div class="cssClassCommonBox Curve">
                                <div class="cssClassGridWrapper">
                                    <div class="cssClassGridWrapperContent">
                                        <div class="cssClassSearchPanel cssClassFormWrapper">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <label class="cssClassLabel">
                                                            User Name:</label>
                                                        <input type="text" id="txtSearchUserName1" class="cssClassTextBoxSmall" />
                                                    </td>
                                                    <td>
                                                        <label class="cssClassLabel">
                                                            Host Address:</label>
                                                        <input type="text" id="txtSearchHostAddress1" class="cssClassTextBoxSmall" />
                                                    </td>
                                                    <td>
                                                        <label class="cssClassLabel">
                                                            Browser Name:</label>
                                                        <input type="text" id="txtBrowserName1" class="cssClassTextBoxSmall" />
                                                    </td>
                                                    <td>
                                                        <div class="cssClassButtonWrapper cssClassPaddingNone">
                                                            <p>
                                                                <button type="button" id="btnSearchRegisteredUser">
                                                                    <span><span>Search</span></span></button>
                                                            </p>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="loading">
                                            <img id="ajaxCustomerOnline" src=""  alt="loading...."/>
                                        </div>
                                        <div class="log">
                                        </div>
                                        <table id="gdvOnlineRegisteredUser" cellspacing="0" cellpadding="0" border="0" width="100%">
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="fragment-2">
                    <div class="cssClassFormWrapper">
                        <div id="divAnonymousUser">
                            <div class="cssClassCommonBox Curve">
                                <div class="cssClassGridWrapper">
                                    <div class="cssClassGridWrapperContent">
                                        <div class="cssClassSearchPanel cssClassFormWrapper">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <label class="cssClassLabel">
                                                            Host Address:</label>
                                                        <input type="text" id="txtSearchHostAddress0" class="cssClassTextBoxSmall" />
                                                    </td>
                                                    <td>
                                                        <label class="cssClassLabel">
                                                            Browser Name:</label>
                                                        <input type="text" id="txtBrowserName0" class="cssClassTextBoxSmall" />
                                                    </td>
                                                    <td>
                                                        <div class="cssClassButtonWrapper cssClassPaddingNone">
                                                            <p>
                                                                <button type="button" id="btnSearchAnonymousUser">
                                                                    <span><span>Search</span></span></button>
                                                            </p>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="loading">
                                            <img id="ajaxCustomerOnlie2" src=""  alt="loading...."/>
                                        </div>
                                        <div class="log">
                                        </div>
                                        <table id="gdvOnlineAnonymousUser" cellspacing="0" cellpadding="0" border="0" width="100%">
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
