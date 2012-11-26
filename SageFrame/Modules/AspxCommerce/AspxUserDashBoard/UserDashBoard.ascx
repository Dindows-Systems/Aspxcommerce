<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserDashBoard.ascx.cs"
    Inherits="Modules_AspxUserDashBoard_UserDashBoard" %>
    
<script type="text/javascript"  id="tst">
    //<![CDATA[
    $(document).ready(function() {
        $('#ajaxUserDashbaoardImage').attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/ajax-loader.gif');
    });
    var storeId, portalId,userName,cultureName, userEmail , sessionCode, customerId , userFirstName, userLastName ,
     allowMultipleAddress,downloadIP, aspxfilePath , userIP , ReviewID, status, ratingValues, countryName,count , isAll, allowWishListMyAccount;
    window.onload = function() {
        storeId = AspxCommerce.utils.GetStoreID();
        portalId = AspxCommerce.utils.GetPortalID();
        userName = AspxCommerce.utils.GetUserName();
        cultureName = AspxCommerce.utils.GetCultureName();
        userEmail = '<%=userEmail %>';
        sessionCode = AspxCommerce.utils.GetSessionCode();
        customerId = AspxCommerce.utils.GetCustomerID();
        userFirstName = '<%=userFirstName%>';
        userLastName = '<%=userLastName%>';
        allowMultipleAddress = '<%=allowMultipleAddress %>'
        downloadIP = AspxCommerce.utils.GetClientIP();
        aspxfilePath = '<%=aspxfilePath %>';
        userIP = AspxCommerce.utils.GetClientIP();
        ReviewID = '';
        status = '';
        ratingValues = '';
        countryName = '<%=countryName %>';
        count = 1;
        isAll = 1;
        allowWishListMyAccount = '<%=allowWishListMyAccount %>';

        $(function() {

            var UserDashBoard = {
                LoadAjaxUserDashBoardStaticImage: function() {
                    $('#ajaxUserDashbaoardImage').attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/ajax-loader.gif');
                }, LoadControl: function(controlName) {
                    $.ajax({
                        type: "POST",
                        url: AspxCommerce.utils.GetAspxServicePath() + "LoadControlHandler.aspx/Result",
                        data: "{ controlName:'" + AspxCommerce.utils.GetAspxRootPath() + controlName + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function(response) {
                            $('#divLoadUserControl').html(response.d);
                            if (controlName.indexOf("WishItemList") > 0) {
                                WishList.Init();
                            }
                        },
                        error: function() {
                            //alert("Error!");
                            csscody.error('<h2>Error Message</h2><p>Failed to load control!.</p>');
                        }
                    });
                },
                Init: function() {
                    if (allowWishListMyAccount.toLowerCase() != 'true') {
                        $('.cssClassMyDashBoard li').each(function(index) {
                            if ($(this).find('a').attr('name') == 'MyWishList')
                            { $(this).hide(); }
                            if ($(this).find('a').attr('name') == 'SharedWishList')
                            { $(this).hide(); }
                        });
                    }
                    if (customerId > 0 && userName.toLowerCase() != 'anonymoususer') {
                        $(document).ajaxStart(function() {
                            $('#divAjaxLoader').show();
                        });

                        $(document).ajaxStop(function() {
                            $('#divAjaxLoader').hide();
                        });
                       // UserDashBoard.LoadAjaxUserDashBoardStaticImage();
                        UserDashBoard.LoadControl("Modules/AspxCommerce/AspxUserDashBoard/AccountDashboard.ascx");
                        $("#spanName").html(' (' + userName + ')');
                        $("ul.cssClassMyDashBoard li a").bind("click", function() {
                            $("ul.cssClassMyDashBoard li a").removeClass("cssClassmyAccountActive");
                            $(this).addClass("cssClassmyAccountActive");

                            var linkId = $(this).attr("name");
                            var ControlName = '';
                            switch (linkId) {
                                case 'AccountDashBoard':
                                    ControlName = "Modules/AspxCommerce/AspxUserDashBoard/AccountDashboard.ascx";
                                    break;
                                case 'AccountInformation':
                                    ControlName = "Modules/AspxCommerce/AspxUserDashBoard/AccountInformation.ascx";
                                    break;
                                case 'AccountPassword':
                                    ControlName = "Modules/AspxCommerce/AspxUserDashBoard/AccountPassword.ascx";
                                    break;
                                case 'AddressBook':
                                    ControlName = "Modules/AspxCommerce/AspxUserDashBoard/AddressBook.ascx";
                                    break;
                                case 'MyOrders':
                                    ControlName = "Modules/AspxCommerce/AspxUserDashBoard/MyOrders.ascx";
                                    break;
                                //                case 'BillingAgreements':                                
                                //                    break;                                
                                //                case 'RecurringProfiles':                                
                                //                    break;                                
                                case 'MyItemReviews':
                                    //ControlName = "Modules/AspxCommerce/AspxUserDashBoard/UserProductReviews.ascx";
                                    ControlName = "Modules/AspxCommerce/AspxUserDashBoard/UserItemReviews.ascx";
                                    break;
                                case 'MyTags':
                                    ControlName = "Modules/AspxCommerce/AspxUserDashBoard/MyTags.ascx";
                                    break;
                                case 'MyWishList':
                                    ControlName = "Modules/AspxCommerce/AspxWishList/WishItemList.ascx";
                                    break;
                                case 'SharedWishList':
                                    ControlName = "Modules/AspxCommerce/AspxUserDashboard/ShareWishListItems.ascx";
                                    break;
                                case 'MyDownloadableItems':
                                    ControlName = "Modules/AspxCommerce/AspxUserDashBoard/UserDownloadableProducts.ascx";
                                    break;
                                //                case 'NewsLetterSubscriptions':                                
                                //                    break;                                
                                case 'ReferredFriends':
                                    ControlName = "Modules/AspxCommerce/AspxUserDashBoard/ReferredFriends.ascx";
                                    break;
                                //                case 'StoreCredit':                                
                                //                    break;                                
                                //                case 'GiftCard':                                
                                //                    break;                                
                                //                case 'RewardPoints':                                
                                //                    break;                                
                                case 'RecentHistory':
                                    ControlName = "Modules/AspxCommerce/AspxUserDashboard/UserRecentHistory.ascx";
                                    break;
                            }
                            UserDashBoard.LoadControl(ControlName);
                        });
                    }
                    else {
                        var loginPage = '';
                        if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                            loginPage = 'Login.aspx';
                        }
                        else {
                            loginPage = 'Login';
                        }
                        window.location = AspxCommerce.utils.GetAspxRedirectPath() + loginPage;
                    }
                }
            }
            UserDashBoard.Init();
        });
    };
    //]]>
</script>

<div class="cssClassMyDashBoard" onload=tst.int;>
    <div class="cssClassCommonSideBox">
        <h2>
            <span>My Account</span>
        </h2>
        <div class="cssClasMyAccount">
            <ul class="cssClassMyDashBoard">
                <li><a href="#" name="AccountDashBoard" class="cssClassmyAccountActive">Account Dashboard</a></li>
                <li><a href="#" name="AccountInformation">Account Information</a></li>
                <li><a href="#" name="AccountPassword">Change Password</a></li>
                <li><a href="#" name="AddressBook">Address Book</a></li>
                <li><a href="#" name="MyOrders">My Orders</a></li>
                <%--<li><a href="#" name="BillingAgreements">Billing Agreements</a></li>
                <li><a href="#" name="RecurringProfiles">Recurring Profiles</a></li>--%>
                <li><a href="#" name="MyItemReviews">My Item Reviews</a></li>
                <li><a href="#" name="MyTags">My Tags</a></li>
                <li><a href="#" name="MyWishList">My Wishlist</a></li>
                <li><a href="#" name="SharedWishList">Shared Wishlist</a></li>
                <li><a href="#" name="MyDownloadableItems">My Digital Items</a></li>
                <%--<li><a href="#" name="NewsLetterSubscriptions">Newsletter Subscriptions</a></li>--%>
                <li><a href="#" name="ReferredFriends">Referred Friends</a></li>
                <%--<li><a href="#" name="StoreCredit">Store Credit</a></li>
                <li><a href="#" name="GiftCard">Gift Card</a></li>
                <li><a href="#" name="RewardPoints">Reward Points</a></li>--%>
                <li><a href="#" name="RecentHistory">Recent History</a></li>
            </ul>
        </div>
    </div>
    <div id="divAjaxLoader">
        <img id="ajaxUserDashbaoardImage" src="" title="loading...." alt="loading...." />
    </div>  
    <div id="divLoadUserControl" class="cssClasMyAccountInformation">
   
       <div class="cssClassMyDashBoardInformation">
       <%-- <p>
            Hello,<span id="spanName"></span> From your My Account Dashboard you
            have the ability to view a snapshot of your recent account activity and update your
            account information. Select a link below to view or edit information.
        </p>--%>
    </div>
    </div>
    <div class="cssClassclear">
    </div>
    
</div>
