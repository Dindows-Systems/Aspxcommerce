<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreSettingManage.ascx.cs"
    Inherits="Modules_AspxStoreSettings_StoreSettingManage" %>
<script type="text/javascript" >
	//<![CDATA[
   var ddlMyAccountURL="<%=ddlMyAccountURL.ClientID %>";
   var ddlShoppingCartURL="<%=ddlShoppingCartURL.ClientID%>";
   var ddlWishListURL="<%=ddlWishListURL.ClientID %>";
   var lstSSLSecurePages='<%=lstSSLSecurePages.ClientID %>';
   var ddlDftStoreTimeZone="<%=ddlDftStoreTimeZone.ClientID %>";
   var maxFilesize='<%=MaxFileSize %>';

	//]]>
</script>

<div id="divStoreSettings">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblHeading" runat="server" Text="Store Settings"></asp:Label>
            </h2>
        </div>
        <div class="cssClassTabPanelTable">
            <div id="container-7">
                <ul>
                    <li><a href="#storefragment-1">
                        <asp:Label ID="lblTabTitle1" runat="server" Text="Standard"></asp:Label>
                    </a></li>
                    <li><a href="#storefragment-2">
                        <asp:Label ID="lblTabTitle2" runat="server" Text="General"></asp:Label>
                    </a></li>
                    <li><a href="#storefragment-3">
                        <asp:Label ID="lbTabTitle3" runat="server" Text="Email"></asp:Label>
                    </a></li>
                   <%-- <li><a href="#storefragment-4">
                        <asp:Label ID="lbTabTitle4" runat="server" Text="SEO/Display"></asp:Label>
                    </a></li>--%>
                    <li><a href="#storefragment-5">
                        <asp:Label ID="lbTabTitle5" runat="server" Text="Media"></asp:Label>
                    </a></li>
                    <%-- <li><a href="#storefragment-8">
                        <asp:Label ID="lblTabTitle6" runat="server" Text="Security"></asp:Label>
                    </a></li>--%>
                    <li><a href="#storefragment-6">
                        <asp:Label ID="lbTabTitle7" runat="server" Text="Customer Profiles"></asp:Label>
                    </a></li>
                    <li><a href="#storefragment-7">
                        <asp:Label ID="lbTabTitle8" runat="server" Text="Other"></asp:Label>
                    </a></li>
                </ul>
                <div id="storefragment-1">
                    <div class="cssClassFormWrapper">
                        <h3>
                            <asp:Label ID="lblTab1Info" runat="server" Text="Standard Settings"></asp:Label>
                        </h3>
                        <table border="0" width="100%" id="tblStandardSettingsForm">
                            <tr>
                                <td>
                                    <asp:Label ID="lblDefaultImageProductURL" Text="Default Image Product URL:" runat="server"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input id="fupDefaultImageURL" type="file" class="cssClassBrowse" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div id="defaultProductImage">
                                    </div>
                                </td>
                                </tr>
                            
                            <tr>
                                <td>
                                    <asp:Label ID="lblMyAccountURL" runat="server" Text="My Account URL:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMyAccountURL" runat="server" class="cssClassDropDown" >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShoppingCartURL" runat="server" Text="Shopping Cart URL:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlShoppingCartURL" class="cssClassDropDown" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblWishListURL" runat="server" Text="WishList URL:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlWishListURL" class="cssClassDropDown" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="storefragment-2">
                    <div class="cssClassFormWrapper">
                        <h3>
                            <asp:Label ID="lblTab2Info" runat="server" Text="General Settings"></asp:Label>
                        </h3>
                        <div id="divForLaterUseGS">
                            <table id="tblForLaterUSe">
                            <tr>
                                <td>
                                    <asp:Label ID="lblDefaultCountry" runat="server" Text="Default Country:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <select id="ddlCountry" class="cssClassDropDown">
                                    </select>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <asp:Label ID="lblStoreName" runat="server" Text="Store Name: " CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtStoreName" name="StoreName" class="cssClassNormalTextBox required" />
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblSSLSecurePages" runat="server" Text="SSL Secure Pages:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <asp:ListBox ID="lstSSLSecurePages" runat="server" SelectionMode="Multiple" AutoPostBack="false"></asp:ListBox>
                                </td>
                            </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblGoogleMapAPIKey" runat="server" Text="Google Map API Key For Store Locator:"
                                            CssClass="cssClassFormLabel"></asp:Label>
                                    </td>
                                    <td class="cssClassGridRightCol">
                                        <input type="text" id="txtGoogleMapAPIKey" name="GoogleMapAPIKey" class="cssClassNormalTextBox required" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table id="tblGeneralSettingForm">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMainCurrency" runat="server" Text="Main Currency:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <select id="ddlCurrency" class="cssClassDropDown">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblWeightUnit" runat="server" Text="Weight Unit:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtWeightUnit" name="Weight" class="cssClassNormalTextBox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblLowStockQuantity" runat="server" Text="Low Stock Quantity:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtLowStockQuantity" name="LowStockQuantity" datatype="Integer"
                                        class="cssClassNormalTextBox required" />
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblShoppingOptionRange" runat="server" Text="Shopping Option Range:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtShoppingOptionRange" name="ShoppingOptionRange" datatype="Integer"
                                        class="cssClassNormalTextBox required" />
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblStoreLogo" Text="Store Logo:" runat="server"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input id="fupStoreLogo" type="file" class="cssClassBrowse" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div id="divStoreLogo">
                                    </div>
                                </td>
                                </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblStorURL" runat="server" Text="Store Close Information:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <%-- <td>
                                    <textarea id="txtStoreCloseInformation" name="StoreCloseInformation" cols="20" rows="5"></textarea>
                                </td>--%>
                               <%-- <td>
                                    <asp:DropDownList ID="ddlStoreCloseUrl" runat="server" class="cssClassDropDown">
                                    </asp:DropDownList>
                                </td>--%>
                                <td>
                                    <textarea id="txtStoreCloseInformation" name="StoreCloseInformation" cols="20" rows="5" class="cssClassTextArea"></textarea>
                                </td>
                               
                            </tr>  
                            <tr>
                                <td>
                                    <asp:Label ID="lblStoreClosed" runat="server" Text="Store Closed:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkStoreClosed" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblStoreNotAccessed" runat="server" Text="Store Not Accessed Information:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td>
                                    <textarea id="txtStoreNotAccessedInfo" name="StoreNotAccessedInfo" cols="20" rows="5" class="cssClassTextArea"></textarea>
                               </td>                                                         
                            </tr>
                              <tr>
                                <td>
                                    <asp:Label ID="lblCartAbandonTime" Text="Cart Abandon Time(In Hours):" runat="server" CssClass="cssClassFormLabel"></asp:Label>                               
                                </td>
                                <td>
                                    <input type="text" id="txtCartAbandonTime" name="CartAbandonTime"  datatype="Integer" class="cssClassNormalTextBox required number"  >
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblTimeToDeleteAbandCart" Text="Abandoned Carts Deletion Time(In Hours):" runat="server" CssClass="cssClassFormLabel"></asp:Label>                               
                                </td>
                                <td>
                                    <input type="text" id="txtTimeToDeleteAbandCart" name="TimeTodeleteAbandCart"  datatype="Integer" class="cssClassNormalTextBox required number">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowAnonymousCheckOut" Text="Allow Anonymous Checkout:" runat="server"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAllowAnonymousCheckout" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowMultipleShippingAddress" runat="server" Text="Allow Multiple Shipping Address:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAllowMultipleShippingAddress" readonly="readonly" disabled="disabled"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowOutStockPurchase" runat="server" Text="Allow purchases when out of stock:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAllowOutStockPurchase" readonly="readonly" disabled="disabled"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
               
                <div id="storefragment-3">
                    <div class="cssClassFormWrapper">
                        <h3>
                            <asp:Label ID="lblEmailSettingForm" runat="server" Text="Email Settings"></asp:Label>
                        </h3>
                         <div id="divForLaterUseEmail">
                            <table id="tblForLaterUseEmail">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSendEmailsTo" runat="server" Text="Send E-Commerce Emails To:"
                                            CssClass="cssClassFormLabel"></asp:Label>
                                    </td>
                                    <td class="cssClassGridRightCol">
                                        <input type="text" id="txtSendEmailsTo" name="EmailTo" class="cssClassNormalTextBox required" />
                                    </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblSendPaymentNotification" runat="server" Text="Send Payment Notification:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkSendPaymentNotification" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblStoreAdminEmail" runat="server" Text="Store Admin Email:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkStoreAdminEmail" />
                                </td>
                            </tr>
                            </table>
                         </div>
                        <table id="tblEmailSettingForm">
                            <tr>
                                <td>
                                    <asp:Label ID="lblSendEmailsFrom" runat="server" Text="Send E-Commerce Emails From:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtSendEmailsFrom" name="EmailFrom" class="cssClassNormalTextBox required" />
                                </td>
                            </tr>
                           
                            <tr>
                                <td>
                                    <asp:Label ID="lblSendOrderNotification" runat="server" Text="Send Order Notification:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkSendOrderNotification" />
                                </td>
                            </tr>
                           
                        </table>
                    </div>
                </div>
                <div id="storefragment-4" >
                    <div class="cssClassFormWrapper">
                        <h3>
                            <asp:Label ID="lblSEODisplay" runat="server" Text="SEO/Display Settings"></asp:Label>
                        </h3>
                        <table id="tblSEODisplayForm">
                            <tr>
                                <td>
                                    <asp:Label ID="lblEnableStoreNamePrefix" runat="server" Text="Enable Store Name Prefix:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkEnableStoreNamePrefix" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDefaultTitle" runat="server" Text="Default Title:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtDefaultTitle" name="DefaultTitle" class="cssClassNormalTextBox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDefaultMetaDescription" runat="server" Text="Default Meta Description:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtDefaultMetaDescription" name="DefaultMetaDescription" class="cssClassNormalTextBox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDefaultMetaKeywords" runat="server" Text="Default Meta KeyWords:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtDefaultMetaKeywords" name="DefaultMetaKeyWords" class="cssClassNormalTextBox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblWelcomMsg" runat="server" Text="Show Welcome Message On Home Page"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkWelcomeMsg" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNewsRssFeed" runat="server" Text="Show News RSS Feed Link in Browser AddressBar:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkNewsRssFeed" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblBlogsRssFeed" runat="server" Text="Show Blog RSS Feed Link in Browser AddressBar:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkBlogRssFeed" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="storefragment-5">
                    <div class="cssClassFormWrapper">
                        <h3>
                            <asp:Label ID="lblMediaSetting" runat="server" Text="Media Settings"></asp:Label>
                        </h3>
                        <table id="tblMediaSettingForm">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMaximumImageSize" Text="Maximum Image Size:" runat="server" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtMaximumImageSize" datatype="Integer" name="MaximumImageSize"
                                        class="cssClassNormalTextBox required" /> <b>KB</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemLargeThumbImageSize" Text="Item Large Thumbnail Image Size:" runat="server"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtItemLargeThumbnailImageSize" name="ItemLargeThumbnailImage" datatype="Integer"
                                        class="cssClassNormalTextBox required" /> <b>KB</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemMediumThumbnailImageSize" Text="Item Medium Thumbnail Image Size:" runat="server"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtItemMediumThumbnailImageSize" name="ItemMediumThumbnailImage" datatype="Integer"
                                        class="cssClassNormalTextBox required" /> <b>KB</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemSmallThumbnailImageSize" runat="server" Text="Item Small Thumbnail Image Size:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtItemSmallThumbnailImageSize" name="ItemSmallThumbnailImageSize"
                                        datatype="Integer" class="cssClassNormalTextBox required" /> <b>KB</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCategoryLargeThumbnailImageSize" runat="server" Text="Category Large Thumbnail Image Size:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtCategoryLargeThumbnailImageSize" name="CategoryLargeThumbnailImageSize"
                                        datatype="Integer" class="cssClassNormalTextBox required" /> <b>KB</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCategoryMediumThumbnailImageSize" runat="server" Text="Category Medium Thumbnail Image Size:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtCategoryMediumThumbnailImageSize" name="CategoryMediumThumbnailImageSize"
                                        datatype="Integer" class="cssClassNormalTextBox required" /> <b>KB</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCategorySmallThumbnailImageSize" runat="server" Text="Category Small Thumbnail Image Size:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtCategorySmallThumbnailImageSize" name="CategorySmallThumbnailImageSize" datatype="Integer"
                                        class="cssClassNormalTextBox required" /> <b>KB</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShowItemImagesInCart" runat="server" Text="Show Item Images in Cart:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkShowItemImagesInCart" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShowItemImagesInWishList" runat="server" Text="Show Item Images in WishList:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkShowItemImagesInWishList" />
                                </td>
                            </tr>
                        </table>
                    </div> 
                </div>
                <div id="storefragment-6">
                    <div class="cssClassFormWrapper">
                        <h3>
                            <asp:Label ID="lblTitleCustomerProfiles" runat="server" Text="Customer Profiles Settings"></asp:Label>
                        </h3>
                        <div id="divForLaterUseCPS">
                            <table id="tblForLaterUseCPS">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAllowCreditCardData" runat="server" Text="Allow Users to store CreditCard Data in Profile:"
                                            CssClass="cssClassFormLabel"></asp:Label>
                                    </td>
                                    <td class="cssClassGridRightCol">
                                        <input type="checkbox" id="chkAllowCreditCardData" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAllowUserGroup" runat="server" Text="Allow Customers to Sign User groups:"
                                            CssClass="cssClassFormLabel"></asp:Label>
                                    </td>
                                    <td class="cssClassGridRightCol">
                                        <input type="checkbox" id="chkAllowForUserGroup" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAllowRePayOrder" runat="server" Text="Allow Customers to pay order again if the transaction was declined:"
                                            CssClass="cssClassFormLabel"></asp:Label>
                                    </td>
                                    <td class="cssClassGridRightCol">
                                        <input type="checkbox" id="chkAllowRePayOrder" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAllowPrivateMessages" runat="server" Text="Allow Private Messages:"
                                            CssClass="cssClassFormLabel"></asp:Label>
                                    </td>
                                    <td class="cssClassGridRightCol">
                                        <input type="checkbox" id="chkAllowPrivateMsg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDefaultStoreTimeZone" runat="server" Text="Default Store Time Zone:"
                                            CssClass="cssClassFormLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDftStoreTimeZone" class="cssClassDropDown" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMinimumItemQuantity" runat="server" Text="Minimum Quantity To Be Purchased:"
                                            CssClass="cssClassFormLabel"></asp:Label>
                                    </td>
                                    <td class="cssClassGridRightCol">
                                        <input type="text" id="txtMinimumQuantity" name="MinimumItemQuantity" datatype="Integer"
                                            class="cssClassNormalTextBox required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMaximumItemQuantity" runat="server" Text="Maximum Purchased Quantity:"
                                            CssClass="cssClassFormLabel"></asp:Label>
                                    </td>
                                    <td class="cssClassGridRightCol">
                                        <input type="text" id="txtMaximumItemQuantity" name="MaximumItemQuantity" datatype="Integer"
                                            class="cssClassNormalTextBox required" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table id="tblCustomerProfilesSettings">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowMultipleAddress" runat="server" Text="Allow Users To Create Multiple Addresses:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAllowMultipleAddress" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <asp:Label ID="lblMinimumOrder" runat="server" Text="Minimum Order Amount:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtMinimumOrderAmount" name="MinimumOrderAmount" datatype="Integer"
                                        class="cssClassNormalTextBox required" />
                                </td>
                            </tr>                            
                        </table>
                    </div>
                </div>
                <div id="storefragment-7">
                    <div class="cssClassFormWrapper">
                        <h3>
                            <asp:Label ID="lblOtherSettings" runat="server" Text="Other Settings"></asp:Label>
                        </h3>
                        <div id="divForLaterUseOS">
                            <table id="tblForLaterUseOS">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNotifyAboutItemReviews" runat="server" Text="Notify About Item Reviews:"
                                            CssClass="cssClassFormLabel"></asp:Label>
                                    </td>
                                    <td class="cssClassGridRightCol">
                                        <input type="checkbox" id="chkNotifyAboutItemReviews" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblItemReviewsApproved" runat="server" Text="Item Review Must be Approved:"
                                            CssClass="cssClassFormLabel"></asp:Label>
                                    </td>
                                    <td class="cssClassGridRightCol">
                                        <input type="checkbox" id="chkItemReviewsApproved" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table id="tblOtherSettings">
                            <tr>
                                <td>
                                    <asp:Label ID="lblEnableCompareItems" runat="server" Text="Enable 'Compare Items':"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkEnableCompareItems" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoOfItemsCompare" runat="server" Text="Maximum Number Of Items Allowed To Compare:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtNoOfItemsToCompare" name="NumberOfItemsToCompare"
                                        datatype="Integer" class="cssClassNormalTextBox required" maxlength="2" readonly="readonly" disabled="disabled"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEnableWishList" runat="server" Text="Enable 'WishList':" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkEnableWishList" />
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblNoOfRecentAddedWishItems" runat="server" Text="Number Of Recently Added WishItems:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtNoOfRecentAddedWishItems" name="NoOfRecentAddedWishItems"
                                        datatype="Integer" class="cssClassNormalTextBox required" maxlength="2" readonly="readonly" disabled="disabled"/>
                                </td>
                            </tr>                          
                            <tr>
                                <td>
                                    <asp:Label ID="lblEnableEmailAFriend" runat="server" Text="Enable 'Email a Friend' :"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkEmailAFriend" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShowMiniShoppingCart" runat="server" Text="Show 'Mini Shopping Cart':"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkShowMiniShoppingCart" />
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowAnonymousUserToWriteReviews" runat="server" CssClass="cssClassFormLabel"
                                        Text="Allow AnonymousUser to Write Reviews and Ratings:"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAllowAnonymousUserToWriteReviews" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMultipleReviewsPerUser" runat="server" CssClass="cssClassFormLabel"
                                        Text="Allow Users To Write Multiple Reviews:"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkMultipleReviewsPerUser" />
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblMultipleReviewsPerIP" runat="server" CssClass="cssClassFormLabel"
                                        Text="Allow Users To Write Multiple Reviews From Same IP:"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkMultipleReviewsPerIP" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEnableRecentlyViewedItems" runat="server" Text="Enable 'Recently Viewed Items':"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkEnableRecentlyViewedItems" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoOfRecentlyViewedItems" runat="server" Text="No Of Recently Viewed Items:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtNoOfRecentlyViewedItems" name="RecentlyViewedCount" datatype="Integer"
                                        class="cssClassNormalTextBox required" maxlength="2" readonly="readonly" disabled="disabled"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEnableLatestItems" runat="server" Text="Enable 'Latest Items':"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkEnableLatestItems" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoOfLatestItems" runat="server" Text="No Of Latest Items:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtNoOfLatestItems" name="LatestItemsCount" datatype="Integer"
                                        class="cssClassNormalTextBox required" maxlength="2" />
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblNoOfLatestItemsInARow" runat="server" Text="No Of Latest Items In A Row:" CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtNoOfLatestItemsInARow" name="LatestItemsInARow" datatype="Integer"
                                        class="cssClassNormalTextBox required" maxlength="2" readonly="readonly" disabled="disabled"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShowBestSellers" runat="server" Text="Enable 'BestSeller Items':"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkShowBestSellers" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoOfBestSellers" runat="server" Text="No Of BestSeller Items:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtNoOfBestSellers" name="BestSellersCount" datatype="Integer"
                                        class="cssClassNormalTextBox required"  maxlength="2"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEnableSpecialItems" runat="server" Text="Enable 'Special Items':"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkEnableSpecialItems" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoOfSpecialItems" runat="server" Text="No Of Special Items:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtNoOfSpecialItems" name="ItemSpecialCount" datatype="Integer"
                                        class="cssClassNormalTextBox required" maxlength="2" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEnableRecentlyComparedItems" runat="server" Text="Enable 'Recently Compared Items':"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkEnableRecentlyComparedItems" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoOfRecentlyComparedItems" runat="server" Text="No Of Recently Compared Items:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtNoOfRecentlyComparedItems" name="RecentlyComparedCount"
                                        datatype="Integer" class="cssClassNormalTextBox required" maxlength="2" readonly="readonly" disabled="disabled" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblRelatedItemsInCart" runat="server" Text="Enable 'Related Items In Cart':"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkRelatedItemsInCart" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoOfRelatedItemsInCart" runat="server" Text="No Of Related Items In Cart:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtNoOfRelatedItemsInCart" name="RelatedItemsInCartCount"
                                        datatype="Integer" class="cssClassNormalTextBox required" maxlength="2" readonly="readonly" disabled="disabled"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoOfPopTags" runat="server" Text="No Of Popular Tags:"
                                        CssClass="cssClassFormLabel"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtNoOfPopTags" name="NoOfPopTags"
                                        datatype="Integer" class="cssClassNormalTextBox required" maxlength="2" readonly="readonly" disabled="disabled"/>
                                </td>
                            </tr>
                             
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="submit" id="btnSaveStoreSettings" class="cssClassButtonSubmit">
                    <span><span>Save Settings</span></span></button>
            </p>
        </div>
    </div>
</div>
<input type="hidden" id="hdnPrevFilePath" />
<input type="hidden" id="hdnPrevStoreLogoPath" />
