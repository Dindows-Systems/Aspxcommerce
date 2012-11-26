 var StoreSettings="";
 $(function() {

     var storeId = AspxCommerce.utils.GetStoreID();
     var portalId = AspxCommerce.utils.GetPortalID();
     var userName = AspxCommerce.utils.GetUserName();
     var cultureName = AspxCommerce.utils.GetCultureName();
     var customerId = AspxCommerce.utils.GetCustomerID();
     var ip = AspxCommerce.utils.GetClientIP();
     var countryName = AspxCommerce.utils.GetAspxClientCoutry();
     var sessionCode = AspxCommerce.utils.GetSessionCode();
     var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
     StoreSettings = {
         config: {
             isPostBack: false,
             async: false,
             cache: false,
             type: 'POST',
             contentType: "application/json; charset=utf-8",
             data: '{}',
             dataType: 'json',
             baseURL: aspxservicePath + "AspxCommerceWebService.asmx/",
             method: "",
             url: ""
         },
         init: function() {
             StoreSettings.HideForLaterUseDivs();
             StoreSettings.InitializeTabs();
             StoreSettings.GetAllCountry();
             StoreSettings.GetAllCurrency();
             StoreSettings.GetAllStoreSettings();
             $("input[DataType='Integer']").keypress(function(e) {
                 if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                     return false;
                 }
             });
             $('#txtMaximumImageSize,#txtItemLargeThumbnailImageSize,#txtItemMediumThumbnailImageSize,#txtItemSmallThumbnailImageSize,#txtCategoryLargeThumbnailImageSize,#txtCategoryMediumThumbnailImageSize,#txtCategorySmallThumbnailImageSize').keyup(function() {
                 if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                     this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                 }
             });
             $("#ddlCurrency").attr("disabled", "disabled");
             $("#txtWeightUnit").attr("disabled", "disabled");
             $("#form1").validate({
                 messages: {
                     DefaultImageProductURL: { required: '*'
                     },
                     MyAccountURL: { required: '*'
                     },
                     ShoppingCartURL: { required: '*'
                     },
                     WishListURL: { required: '*'
                     },
                     MainCurrency: { required: '*'
                     },
                     Weight: { required: '*'
                     },
                     StoreName: { required: '*'
                     },
                     StoreCloseInformation: { required: '*'
                     },
                     StoreNotAccessedInfo: { required: '*'
                     },
                     TimeTodeleteAbandCart: { required: '*', number: true
                     },
                     CartAbandonTime: { required: '*', number: true
                     },
                     LowStockQuantity: { required: '*'
                     },
                     ShoppingOptionRange: { required: '*'
                     },
                     EmailFrom: { required: '*'
                     },
                     EmailTo: { required: '*'
                     },
                     DefaultTitle: { required: '*'
                     },
                     DefaultMetaDescription: { required: '*'
                     },
                     DefaultMetaKeyWords: { required: '*'
                     },
                     MaximumImageSize: { required: '*'
                     },
                     ItemLargeThumbnailImage: { required: '*'
                     },
                     ItemMediumThumbnailImage: { required: '*'
                     },
                     ItemSmallThumbnailImageSize: { required: '*'
                     },
                     CategoryLargeThumbnailImageSize: { required: '*'
                     },
                     CategoryMediumThumbnailImageSize: { required: '*'
                     },
                     CategorySmallThumbnailImageSize: { required: '*'
                     },
                     DefaultTimeZone: { required: '*'
                     },
                     MinimumOrderAmount: { required: '*'
                     },
                     RecentlyViewedCount: { required: '*',
                         maxlength: "* (no more than 2 digits)"
                     },
                     LatestItemsCount: { required: '*',
                         maxlength: "* (no more than 2 digits)"
                     },
                     LatestItemsInARow: { required: '*',
                         maxlength: "* (no more than 2 digits)"
                     },
                     BestSellersCount: { required: '*',
                         maxlength: "* (no more than 2 digits)"
                     },
                     ItemSpecialCount: { required: '*',
                         maxlength: "* (no more than 2 digits)"
                     },
                     RecentlyComparedCount: { required: '*',
                         maxlength: "* (no more than 2 digits)"
                     },
                     RelatedItemsInCartCount: { required: '*',
                         maxlength: "* (no more than 2 digits)"
                     },
                     NumberOfItemsToCompare: { required: '*',
                         maxlength: "* (no more than 2 digits)"
                     },
                     NoOfPopTags: { required: '*',
                         maxlength: "* (no more than 2 digits)"
                     },

                     NoOfRecentAddedWishItems: { required: '*',
                         maxlength: "* (no more than 2 digits)"
                     }
                 },
                 //success: "valid",
                 submitHandler: function() {
                     AspxCommerce.CheckSessionActive();
                     if (AspxCommerce.vars.IsAlive) {
                         StoreSettings.UpdateStoreSettings();
                     }
                     else {
                         window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + 'Login.aspx';
                     }
                 }
             });
             StoreSettings.ImageUploader("fupDefaultImageURL");
             StoreSettings.ImageUploader("fupStoreLogo");
         },

         ajaxCall: function(config) {
             $.ajax({
                 type: StoreSettings.config.type,
                 contentType: StoreSettings.config.contentType,
                 cache: StoreSettings.config.cache,
                 async: StoreSettings.config.async,
                 url: StoreSettings.config.url,
                 data: StoreSettings.config.data,
                 dataType: StoreSettings.config.dataType,
                 success: StoreSettings.ajaxSuccess,
                 error: StoreSettings.ajaxFailure
             });
         },
         HideForLaterUseDivs: function() {
             $("#storefragment-4").hide();
             $("#divForLaterUseEmail").hide();
             $("#divForLaterUseGS").hide();
             $("#divForLaterUseCPS").hide();
             $("#divForLaterUseOS").hide();
         },

         InitializeTabs: function() {
             var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show'}] });
             $tabs.tabs('select', 0);
         },

         GetAllStoreSettings: function() {
             this.config.url = this.config.baseURL + "GetAllStoreSettings";
             this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, cultureName: cultureName });
             this.config.ajaxCallMode = 1;
             this.ajaxCall(this.config);
         },

         GetAllCountry: function() {
             this.config.url = this.config.baseURL + "BindCountryList";
             this.config.data = '{}';
             this.config.ajaxCallMode = 2;
             this.ajaxCall(this.config);
         },

         GetAllCurrency: function() {
             this.config.url = this.config.baseURL + "BindCurrencyList";
             this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, cultureName: cultureName });
             this.config.ajaxCallMode = 3;
             this.ajaxCall(this.config);
         },

         BindAllValue: function(obj) {
             //Standard Settings
             $("#hdnPrevFilePath").val(obj.DefaultProductImageURL);
             $("#defaultProductImage").html('<img src="' + aspxRootPath + obj.DefaultProductImageURL + '" class="uploadImage" height="90px" width="100px"/>');
             $("#" + ddlMyAccountURL).val(obj.MyAccountURL);
             $("#" + ddlShoppingCartURL).val(obj.ShoppingCartURL);
             $("#" + ddlWishListURL).val(obj.WishListURL);

             //General Settings
             if (obj.SSLSecurePages != null) {
                 var sslSecurePages = (obj.SSLSecurePages).split(',');
                 for (var i in sslSecurePages)
                     $("#" + lstSSLSecurePages + " [value=" + $.trim(sslSecurePages[i]) + "]").attr('selected', "selected");
             }
             $("#ddlCurrency").val(obj.MainCurrency);
             $("#ddlCountry").val(obj.DefaultCountry);
             $("#txtWeightUnit").val(obj.WeightUnit);
             $("#txtStoreName").val(obj.StoreName);
             $("#hdnPrevStoreLogoPath").val(obj.StoreLogoURL);
             $("#divStoreLogo").html('<img src="' + aspxRootPath + obj.StoreLogoURL + '" class="uploadImage" height="90px" width="100px"/>');
             $("#chkStoreClosed").attr("checked", $.parseJSON(obj.StoreClosed.toLowerCase()));
             $("#txtStoreCloseInformation").val(obj.StoreClosePageContent);
             $("#txtStoreNotAccessedInfo").val(obj.StoreNOTAccessPageContent);
             $("#txtCartAbandonTime").val(obj.CartAbandonedTime);
             $("#txtTimeToDeleteAbandCart").val(obj.TimeToDeleteAbandonedCart);
             $("#txtGoogleMapAPIKey").val(obj.GoogleMapAPIKey);
             $("#txtLowStockQuantity").val(obj.LowStockQuantity);
             $("#txtShoppingOptionRange").val(obj.ShoppingOptionRange);
             $("#chkAllowAnonymousCheckout").attr("checked", $.parseJSON(obj.AllowAnonymousCheckOut.toLowerCase()));
             $("#chkAllowMultipleShippingAddress").attr("checked", $.parseJSON(obj.AllowMultipleShippingAddress.toLowerCase()));
             $("#chkAllowOutStockPurchase").attr("checked", $.parseJSON(obj.AllowOutStockPurchase.toLowerCase()));

             //Email Settings
             $("#txtSendEmailsFrom").val(obj.SendEcommerceEmailsFrom);
             $("#txtSendEmailsTo").val(obj.SendEcommerceEmailTo);
             $("#chkSendOrderNotification").attr("checked", $.parseJSON(obj.SendOrderNotification.toLowerCase()));
             $("#chkSendPaymentNotification").attr("checked", $.parseJSON(obj.SendPaymentNotification.toLowerCase()));
             $("#chkStoreAdminEmail").attr("checked", $.parseJSON(obj.StoreAdminEmail.toLowerCase()));

             //SEO/Display Settings
             $("#chkEnableStoreNamePrefix").attr("checked", $.parseJSON(obj.EnableStoreNamePrefix.toLowerCase()));
             $("#txtDefaultTitle").val(obj.DefaultTitle);
             $("#txtDefaultMetaDescription").val(obj.DefaultMetaDescription);
             $("#txtDefaultMetaKeywords").val(obj.DefaultMetaKeyWords);
             $("#chkWelcomeMsg").attr("checked", $.parseJSON(obj.ShowWelcomeMessageOnHomePage.toLowerCase()));
             $("#chkNewsRssFeed").attr("checked", $.parseJSON(obj.DisplayNewsRSSFeedLinkInBrowserAddressBar.toLowerCase()));
             $("#chkBlogRssFeed").attr("checked", $.parseJSON(obj.DisplayBlogRSSFeedLinkInBrowserAddressBar.toLowerCase()));

             //Media Settings
             $("#txtMaximumImageSize").val(obj.MaximumImageSize);
             $("#txtItemLargeThumbnailImageSize").val(obj.ItemLargeThumbnailImageSize);
             $("#txtItemMediumThumbnailImageSize").val(obj.ItemMediumThumbnailImageSize);
             $("#txtItemSmallThumbnailImageSize").val(obj.ItemSmallThumbnailImageSize);
             $("#txtCategoryLargeThumbnailImageSize").val(obj.CategoryLargeThumbnailImageSize);
             $("#txtCategoryMediumThumbnailImageSize").val(obj.CategoryMediumThumbnailImageSize);
             $("#txtCategorySmallThumbnailImageSize").val(obj.CategorySmallThumbnailImageSize);
             $("#chkShowItemImagesInCart").attr("checked", $.parseJSON(obj.ShowItemImagesInCart.toLowerCase()));
             $("#chkShowItemImagesInWishList").attr("checked", $.parseJSON(obj.ShowItemImagesInWishList.toLowerCase()));

             //Customer Profiles Settings
             $("#chkAllowMultipleAddress").attr("checked", $.parseJSON(obj.AllowUsersToCreateMultipleAddress.toLowerCase()));
             $("#chkAllowCreditCardData").attr("checked", $.parseJSON(obj.AllowUsersToStoreCreditCardDataInProfile.toLowerCase()));
             $("#txtMinimumOrderAmount").val(obj.MinimumOrderAmount);
             $("#txtMinimumQuantity").val(obj.MinimumItemQuantity);
             $("#txtMaximumItemQuantity").val(obj.MaximumItemQuantity);
             $("#chkAllowForUserGroup").attr("checked", $.parseJSON(obj.AllowCustomerToSignUpForUserGroup.toLowerCase()));
             $("#chkAllowRePayOrder").attr("checked", $.parseJSON(obj.AllowCustomersToPayOrderAgainIfTransactionWasDeclined.toLowerCase()));
             $("#chkAllowPrivateMsg").attr("checked", $.parseJSON(obj.AllowPrivateMessages.toLowerCase()));
             $("#" + ddlDftStoreTimeZone).val(obj.DefaultStoreTimeZone);

             //Other Settings
             $("#chkEnableCompareItems").attr("checked", $.parseJSON(obj.EnableCompareItems.toLowerCase()));
             $("#txtNoOfItemsToCompare").val(obj.MaxNoOfItemsToCompare);
             $("#chkEnableWishList").attr("checked", $.parseJSON(obj.EnableWishList.toLowerCase()));
             $("#txtNoOfRecentAddedWishItems").val(obj.NoOfRecentAddedWishItems);
             $("#chkEmailAFriend").attr("checked", $.parseJSON(obj.EnableEmailAFriend.toLowerCase()));
             $("#chkShowMiniShoppingCart").attr("checked", $.parseJSON(obj.ShowMiniShoppingCart.toLowerCase()));
             $("#chkNotifyAboutItemReviews").attr("checked", $.parseJSON(obj.NotifyAboutNewItemReviews.toLowerCase()));
             $("#chkItemReviewsApproved").attr("checked", $.parseJSON(obj.ItemReviewMustBeApproved.toLowerCase()));
             $("#chkMultipleReviewsPerUser").attr("checked", $.parseJSON(obj.AllowMultipleReviewsPerUser.toLowerCase()));
             $("#chkMultipleReviewsPerIP").attr("checked", $.parseJSON(obj.AllowMultipleReviewsPerIP.toLowerCase()));
             $("#chkAllowAnonymousUserToWriteReviews").attr("checked", $.parseJSON(obj.AllowAnonymousUserToWriteItemRatingAndReviews.toLowerCase()));
             $("#chkEnableRecentlyViewedItems").attr("checked", $.parseJSON(obj.EnableRecentlyViewedItems.toLowerCase()));
             $("#txtNoOfRecentlyViewedItems").val(obj.NoOfRecentlyViewedItemsDispay);
             $("#chkEnableLatestItems").attr("checked", $.parseJSON(obj.EnableLatestItems.toLowerCase()));
             $("#txtNoOfLatestItems").val(obj.NoOfLatestItemsDisplay);
             $("#txtNoOfLatestItemsInARow").val(obj.NoOfLatestItemsInARow);
             $("#chkShowBestSellers").attr("checked", $.parseJSON(obj.EnableBestSellerItems.toLowerCase()));
             $("#txtNoOfBestSellers").val(obj.NoOfBestSellersItemDisplay);
             $("#chkEnableSpecialItems").attr("checked", $.parseJSON(obj.EnableSpecialItems.toLowerCase()));
             $("#txtNoOfSpecialItems").val(obj.NoOfSpecialItemDisplay);
             $("#chkEnableRecentlyComparedItems").attr("checked", $.parseJSON(obj.EnableRecentlyComparedItems.toLowerCase()));
             $("#txtNoOfRecentlyComparedItems").val(obj.NoOfRecentlyComparedItems);
             $("#chkRelatedItemsInCart").attr("checked", $.parseJSON(obj.EnableRelatedCartItems.toLowerCase()));
             $("#txtNoOfRelatedItemsInCart").val(obj.NoOfRelatedCartItems);
             $("#txtNoOfPopTags").val(obj.NoOfPopularTags);

         },

         UpdateStoreSettings: function() {
             //Standard Settings
             if (aspxRootPath != "/") {
                 var defaultImageProductURL = $("#defaultProductImage>img").attr("src").split(aspxRootPath)[1];
             }
             else {
                 var defaultImageProductURL = $("#defaultProductImage>img").attr("src").replace(aspxRootPath, "");
             }
             var prevFilePath = $("#hdnPrevFilePath").val();
             var myAccountURL = $("#" + ddlMyAccountURL).val();
             var shoppingCartURL = $("#" + ddlShoppingCartURL).val();
             var myWishListURL = $("#" + ddlWishListURL).val();

             //General Settings
             var sslSecurePages = "";
             $("#" + lstSSLSecurePages + " option:selected").each(function() {
                 sslSecurePages += $(this).val() + ",";
             });
             var currency = $("#ddlCurrency option:selected").val();
             var country = $("#ddlCountry option:selected").val();
             var weightUnit = $("#txtWeightUnit").val();
             var storeName = $("#txtStoreName").val();
             if (aspxRootPath != "/") {
                 var storeLogoURL = $("#divStoreLogo>img").attr("src").split(aspxRootPath)[1];
             }
             else {
                 var storeLogoURL = $("#divStoreLogo>img").attr("src").replace(aspxRootPath, "");
             }
             var prevStoreLogoPath = $("#hdnPrevStoreLogoPath").val();
             var storeClosePageContent = $("#txtStoreCloseInformation").val();
             var storeClosed = $("#chkStoreClosed").attr("checked");
             var storeNOTAccessPageContent = $("#txtStoreNotAccessedInfo").val();
             var cartAbandonedTime = $("#txtCartAbandonTime").val();
             var timeToDeleteAbandonedCart = $("#txtTimeToDeleteAbandCart").val();
             var googleMapAPI = $("#txtGoogleMapAPIKey").val();
             var lowStockQuantity = $("#txtLowStockQuantity").val();
             var shoppingOptionRange = $("#txtShoppingOptionRange").val();
             var allowAnonymousCheckout = $("#chkAllowAnonymousCheckout").attr("checked");
             var allowMultipleShippingAddress = $("#chkAllowMultipleShippingAddress").attr("checked");
             var allowOutStockPurchase = $("#chkAllowOutStockPurchase").attr("checked");

             //Email Settings
             var emailFrom = $("#txtSendEmailsFrom").val();
             var emailTo = $("#txtSendEmailsTo").val();
             var SendOrderNotification = $("#chkSendOrderNotification").attr("checked");
             var sendPaymentNotification = $("#chkSendPaymentNotification").attr("checked");
             var storeAdminEmail = $("#chkStoreAdminEmail").attr("checked");

             //SEO/Display Settings
             var enableStoreNamePrefix = $("#chkEnableStoreNamePrefix").attr("checked");
             var defaultTitle = $("#txtDefaultTitle").val();
             var defaultMetaDescription = $("#txtDefaultMetaDescription").val();
             var defaultMetaKeywords = $("#txtDefaultMetaKeywords").val();
             var showWelcomeMsg = $("#chkWelcomeMsg").attr("checked");
             var showNewsRssFeed = $("#chkNewsRssFeed").attr("checked");
             var showBlogRssFeed = $("#chkBlogRssFeed").attr("checked");

             //Media Settings
             var maximumImageSize = $("#txtMaximumImageSize").val();
             var ItemLargeThumbnailImageSize = $("#txtItemLargeThumbnailImageSize").val();
             var ItemMediumThumbnailImageSize = $("#txtItemMediumThumbnailImageSize").val();
             var ItemSmallThumbnailImageSize = $("#txtItemSmallThumbnailImageSize").val();
             var CategoryLargeThumbnailImageSize = $("#txtCategoryLargeThumbnailImageSize").val();
             var CategoryMediumThumbnailImageSize = $("#txtCategoryMediumThumbnailImageSize").val();
             var CategorySmallThumbnailImageSize = $("#txtCategorySmallThumbnailImageSize").val();
             var showItemImagesInCart = $("#chkShowItemImagesInCart").attr("checked");
             var showItemImagesInWishList = $("#chkShowItemImagesInWishList").attr("checked");

             //Customer Profiles Settings
             var allowMultipleAddress = $("#chkAllowMultipleAddress").attr("checked");
             var allowSavingCreditCartData = $("#chkAllowCreditCardData").attr("checked");
             var minimumOrderAmount = $("#txtMinimumOrderAmount").val();
             var minimumItemQuantity = $("#txtMinimumQuantity").val();
             var maximumItemQuantity = $("#txtMaximumItemQuantity").val();
             var AllowForUserGroup = $("#chkAllowForUserGroup").attr("checked");
             var AllowRePayOrder = $("#chkAllowRePayOrder").attr("checked");
             var allowPrivateMessages = $("#chkAllowPrivateMsg").attr("checked");
             var defaultStoreTimeZone = $("#" + ddlDftStoreTimeZone).val();


             //Other Settings
             var enableCompareItems = $("#chkEnableCompareItems").attr("checked");
             var maxNoOfItemsToCompare = $("#txtNoOfItemsToCompare").val();
             var enableWishList = $("#chkEnableWishList").attr("checked");
             var noOfRecentAddedWishItems = $("#txtNoOfRecentAddedWishItems").val();
             var enableEmailAFriend = $("#chkEmailAFriend").attr("checked");
             var showMiniShoppingCart = $("#chkShowMiniShoppingCart").attr("checked");
             var notifyItemReviews = $("#chkNotifyAboutItemReviews").attr("checked");
             var itemReviewsApproved = $("#chkItemReviewsApproved").attr("checked");
             var allowMultipleReviewsPerUser = $("#chkMultipleReviewsPerUser").attr("checked");
             var allowMultipleReviewsPerIP = $("#chkMultipleReviewsPerIP").attr("checked");
             var allowAnonymousUserToWriteReviews = $("#chkAllowAnonymousUserToWriteReviews").attr("checked");
             var enableRecentlyViewedItems = $("#chkEnableRecentlyViewedItems").attr("checked");
             var noOfRecentlyViewedItems = $("#txtNoOfRecentlyViewedItems").val();
             var enableLatestItems = $("#chkEnableLatestItems").attr("checked");
             var noOfLatestItems = $("#txtNoOfLatestItems").val();
             var noOfLatestItemsInARow = $("#txtNoOfLatestItemsInARow").val();
             var showBestSellers = $("#chkShowBestSellers").attr("checked");
             var noOfBestSellers = $("#txtNoOfBestSellers").val();
             var enableSpecial = $("#chkEnableSpecialItems").attr("checked");
             var noOfSpecialItems = $("#txtNoOfSpecialItems").val();
             var enableRecentlyComparedItems = $("#chkEnableRecentlyComparedItems").attr("checked");
             var noOfRecentlyComparedItems = $("#txtNoOfRecentlyComparedItems").val();
             var enableRelatedCartItems = $("#chkRelatedItemsInCart").attr("checked");
             var noOfRelatedCartItems = $("#txtNoOfRelatedItemsInCart").val();
             var noOfPopTags = $("#txtNoOfPopTags").val();


             var settingValues = '';
             settingValues += myAccountURL + '*' + shoppingCartURL + '*' + myWishListURL + '*';
             settingValues += currency + '*' + country + '*' + weightUnit + '*' + storeName + '*' + storeClosePageContent + '*' + storeClosed + '*' + storeNOTAccessPageContent + '*' + cartAbandonedTime + '*' + timeToDeleteAbandonedCart + '*' + googleMapAPI + '*' + lowStockQuantity + '*' + shoppingOptionRange + '*' + allowAnonymousCheckout + '*' + allowMultipleShippingAddress + '*' + sslSecurePages + '*';
             settingValues += emailFrom + '*' + emailTo + '*' + SendOrderNotification + '*' + sendPaymentNotification + '*' + storeAdminEmail + '*';
             settingValues += enableStoreNamePrefix + '*' + defaultTitle + '*' + defaultMetaDescription + '*' + defaultMetaKeywords + '*' + showWelcomeMsg + '*' + showNewsRssFeed + '*' + showBlogRssFeed + '*';
             settingValues += maximumImageSize + '*' + ItemLargeThumbnailImageSize + '*' + ItemMediumThumbnailImageSize + '*' + ItemSmallThumbnailImageSize + '*' + CategoryLargeThumbnailImageSize + '*' + CategoryMediumThumbnailImageSize + '*' + CategorySmallThumbnailImageSize + '*' + showItemImagesInCart + '*' + showItemImagesInWishList + '*';
             settingValues += allowMultipleAddress + '*' + allowSavingCreditCartData + '*' + minimumOrderAmount + '*' + minimumItemQuantity + '*' + maximumItemQuantity + '*' + AllowForUserGroup + '*' + AllowRePayOrder + '*' + allowPrivateMessages + '*' + defaultStoreTimeZone + '*' + allowOutStockPurchase + '*';
             settingValues += enableCompareItems + '*' + maxNoOfItemsToCompare + '*' + enableWishList + '*' + noOfRecentAddedWishItems + '*' + enableEmailAFriend + '*' + showMiniShoppingCart + '*' + notifyItemReviews + '*' + itemReviewsApproved + '*' + allowMultipleReviewsPerUser + '*' + allowMultipleReviewsPerIP
                                 + '*' + allowAnonymousUserToWriteReviews + '*' + enableRecentlyViewedItems + '*' + noOfRecentlyViewedItems + '*' + enableLatestItems + '*' + noOfLatestItems + '*' + noOfLatestItemsInARow + '*' + showBestSellers
                                 + '*' + noOfBestSellers + '*' + enableSpecial + '*' + noOfSpecialItems + '*' + enableRecentlyComparedItems + '*' + noOfRecentlyComparedItems + '*' + enableRelatedCartItems + '*' + noOfRelatedCartItems + '*' + noOfPopTags;


             var settingKeys = '';
             settingKeys += 'MyAccountURL' + '*' + 'ShoppingCartURL' + '*' + 'WishListURL' + '*';
             settingKeys += 'MainCurrency' + '*' + 'DefaultCountry' + '*' + 'WeightUnit' + '*' + 'StoreName' + '*' + 'StoreClosePageContent' + '*' + 'StoreClosed' + '*' + 'StoreNOTAccessPageContent' + '*' + 'CartAbandonedTime' + '*' + 'TimeToDeleteAbandonedCart' + '*' + 'GoogleMapAPIKey' + '*' + 'LowStockQuantity' + '*' + 'ShoppingOptionRange' + '*' + 'AllowAnonymousCheckOut' + '*' + 'AllowMultipleShippingAddress' + '*' + 'SSLSecurePages' + '*';
             settingKeys += 'SendEcommerceEmailsFrom' + '*' + 'SendEcommerceEmailTo' + '*' + 'SendOrderNotification' + '*' + 'SendPaymentNotification' + '*' + 'StoreAdminEmail' + '*';
             settingKeys += 'EnableStoreNamePrefix' + '*' + 'DefaultTitle' + '*' + 'DefaultMetaDescription' + '*' + 'DefaultMetaKeyWords' + '*' + 'ShowWelcomeMessageOnHomePage' + '*' + 'DisplayNewsRSSFeedLinkInBrowserAddressBar' + '*' + 'DisplayBlogRSSFeedLinkInBrowserAddressBar' + '*';
             settingKeys += 'MaximumImageSize' + '*' + 'ItemLargeThumbnailImageSize' + '*' + 'ItemMediumThumbnailImageSize' + '*' + 'ItemSmallThumbnailImageSize' + '*' + 'CategoryLargeThumbnailImageSize' + '*' + 'CategoryMediumThumbnailImageSize' + '*' + 'CategorySmallThumbnailImageSize' + '*' + 'ShowItemImagesInCart' + '*' + 'ShowItemImagesInWishList' + '*';
             settingKeys += 'AllowUsersToCreateMultipleAddress' + '*' + 'AllowUsersToStoreCreditCardDataInProfile' + '*' + 'MinimumOrderAmount' + '*' + 'MinimumItemQuantity' + '*' + 'MaximumItemQuantity' + '*' + 'AllowCustomerToSignUpForUserGroup' + '*' + 'AllowCustomersToPayOrderAgainIfTransactionWasDeclined' + '*' + 'AllowPrivateMessages' + '*' + 'DefaultStoreTimeZone' + '*' + 'AllowOutStockPurchase' + '*';
             settingKeys += 'Enable.CompareItems' + '*' + 'MaxNoOfItemsToCompare' + '*' + 'Enable.WishList' + '*' + 'NoOfRecentAddedWishItems' + '*' + 'Enable.EmailAFriend' + '*' + 'Show.MiniShoppingCart' + '*' + 'NotifyAboutNewItemReviews' + '*' + 'ItemReviewMustBeApproved' + '*' + 'AllowMultipleReviewsPerUser' + '*' + 'AllowMultipleReviewsPerIP'
                                + '*' + 'AllowAnonymousUserToWriteItemRatingAndReviews' + '*' + 'Enable.RecentlyViewedItems' + '*' + 'NoOfRecentlyViewedItemsDispay' + '*' + 'Enable.LatestItems' + '*' + 'NoOfLatestItemsDisplay' + '*' + 'NoOfLatestItemsInARow' + '*' + 'Enable.BestSellerItems'
                                + '*' + 'NoOfBestSellersItemDisplay' + '*' + 'Enable.SpecialItems' + '*' + 'NoOfSpecialItemDisplay' + '*' + 'Enable.RecentlyComparedItems' + '*' + 'NoOfRecentlyComparedItems' + '*' + 'Enable.RelatedCartItems' + '*' + 'NoOfRelatedCartItems' + '*' + 'NoOfPopularTags';

             this.config.url = this.config.baseURL + "UpdateStoreSettings";
             this.config.data = JSON2.stringify({ settingKeys: settingKeys, settingValues: settingValues, prevFilePath: prevFilePath, newFilePath: defaultImageProductURL, prevStoreLogoPath: prevStoreLogoPath, newStoreLogoPath: storeLogoURL, storeID: storeId, portalID: portalId, cultureName: cultureName });
             this.config.ajaxCallMode = 4;
             this.ajaxCall(this.config);
         },

         ImageUploader: function(obj) {
             maxFileSize = maxFilesize;
             var upload = new AjaxUpload($('#' + obj), {
                 action: aspxStoreSetModulePath + "MultipleFileUploadHandler.aspx",
                 name: 'myfile[]',
                 multiple: false,
                 data: {},
                 autoSubmit: true,
                 responseType: 'json',
                 onChange: function(file, ext) {
                     //alert('changed');
                 },
                 onSubmit: function(file, ext) {
                     if (ext != "exe") {
                         if (ext && /^(jpg|jpeg|jpe|gif|bmp|png|ico)$/i.test(ext)) {
                             this.setData({
                                 'MaxFileSize': maxFileSize
                             });
                         } else {
                             csscody.alert('<h2>Alert Message</h2><p>Not a valid image!</p>');
                             return false;
                         }
                     }
                     else {
                         csscody.alert('<h2>Alert Message</h2><p>Not a valid image!</p>');
                         return false;
                     }
                 },
                 onComplete: function(file, response) {
                     var res = eval(response);
                     if (res.Message != null && res.Status > 0) {
                         //alert(res.Message);
                         StoreSettings.AddNewImages(res, obj);
                         return false;
                     }
                     else {
                         csscody.error('<h2>Error Message</h2><p>Sorry! image can not be uploaded.</p>');
                         return false;
                     }
                 }
             });
         },

         AddNewImages: function(response, obj) {
             if (obj == "fupDefaultImageURL") {
                 $("#defaultProductImage").html('<img src="' + aspxRootPath + response.Message + '" class="uploadImage" height="90px" width="100px"/>');
             } else {
                 $("#divStoreLogo").html('<img src="' + aspxRootPath + response.Message + '" class="uploadImage" height="90px" width="100px"/>');
             }
         },

         ajaxSuccess: function(data) {
             switch (StoreSettings.config.ajaxCallMode) {
                 case 0:
                     break;
                 case 1:
                     var value = data.d;
                     if (value != null) {
                         StoreSettings.BindAllValue(value);
                     }
                     break;
                 case 2:
                     var countryElements = '';
                     $.each(data.d, function(index, value) {
                         countryElements += '<option value=' + value.Value + '>' + value.Text + '</option>';
                     });
                     $("#ddlCountry").html(countryElements);
                     break;
                 case 3:
                     var currencyElements = '';
                     $.each(data.d, function(index, value) {
                         currencyElements += '<option value=' + value.CurrencyCode + '>' + value.CurrencyName + '</option>';
                     });
                     $("#ddlCurrency").html(currencyElements);
                     break;
                 case 4:
                     AspxCommerce.Busy.LoadingHide();
                     StoreSettings.GetAllStoreSettings();
                     csscody.info('<h2>Information Message</h2><p>Store Settings  has been updated successfully.</p>');
                     break;
             }
         }
     }
     StoreSettings.init();
 });