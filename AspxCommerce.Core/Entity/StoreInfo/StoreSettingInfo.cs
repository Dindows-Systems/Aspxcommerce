/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 20011-2012 by AspxCommerce
Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{[DataContract]
    [Serializable]
    public class StoreSettingInfo
    {
        [DataMember]
        private string _defaultProductImageURL;

        [DataMember]
        private string _myAccountURL;

        [DataMember]
        private string _shoppingCartURL;

        [DataMember]
        private string _wishListURL;

        [DataMember]
        private string _mainCurrency;

        [DataMember]
        private string _defaultCountry;

        [DataMember]
        private string _storeName;

        [DataMember]
        private string _storeLogoURL;

        [DataMember]
        private string _storeClosePageContent;

        [DataMember]
        private string _storeClosed;

        [DataMember]
        private string _storeNOTAccessPageContent; 
       
        [DataMember]
        private string _timeToDeleteAbandonedCart;

        [DataMember]
        private string _cartAbandonedTime;


        [DataMember]
        private string _googleMapAPIKey;

        [DataMember]
        private int _lowStockQuantity;

        [DataMember]
        private string _shoppingOptionRange; 

        [DataMember]
        private string _allowAnonymousCheckOut;

        [DataMember]
        private string _allowMultipleShippingAddress;

        [DataMember]
        private string _sslSecurePages;

        [DataMember]
        private string _allowOutStockPurchase;

        [DataMember]
        private string _sendEcommerceEmailsFrom;

        [DataMember]
        private string _sendEcommerceEmailTo;

         [DataMember]
        private string _sendOrderNotification;

         [DataMember]
        private string _sendPaymentNotification;

         [DataMember]
        private string _storeAdminEmail;

         [DataMember]
        private string _enableStoreNamePrefix;

         [DataMember]
        private string _defaultTitle;

         [DataMember]
        private string _defaultMetaDescription;

         [DataMember]
        private string _defaultMetaKeyWords;

         [DataMember]
        private string _showWelcomeMessageOnHomePage;

         [DataMember]
        private string _displayNewsRSSFeedLinkInBrowserAddressBar;

         [DataMember]
        private string _displayBlogRSSFeedLinkInBrowserAddressBar;

         [DataMember]
        private string _maximumImageSize;

         [DataMember]
         private string _itemLargeThumbnailImageSize;

         [DataMember]
         private string _itemMediumThumbnailImageSize;

         [DataMember]
         private string _itemSmallThumbnailImageSize;

         [DataMember]
         private string _categoryLargeThumbnailImageSize;

         [DataMember]
         private string _categoryMediumThumbnailImageSize;

         [DataMember]
         private string _categorySmallThumbnailImageSize;

         [DataMember]
        private string _showItemImagesInCart;

         [DataMember]
        private string _showItemImagesInWishList;

         [DataMember]
        private string _allowUsersToCreateMultipleAddress;

         [DataMember]
        private string _allowUsersToStoreCreditCardDataInProfile;

         [DataMember]
        private string _minimumOrderAmount;

         [DataMember]
         private string _minimumItemQuantity;

         [DataMember]
         private string _maximumItemQuantity;

         [DataMember]
        private string _allowCustomerToSignUpForUserGroup;

         [DataMember]
        private string _allowCustomersToPayOrderAgainIfTransactionWasDeclined;

         [DataMember]
        private string _allowPrivateMessages;

         [DataMember]
        private string _defaultStoreTimeZone;

         [DataMember]
        private string _enableCompareItems;
        
        [DataMember]
         private string _maxNoOfItemsToCompare;
    
         [DataMember]
        private string _enableWishList;

         [DataMember]
         private string _noOfRecentAddedWishItems;

         [DataMember]
        private string _enableEmailAFriend;

         [DataMember]
        private string _showMiniShoppingCart;

         [DataMember]
        private string _notifyAboutNewItemReviews;

         [DataMember]
         private string _itemReviewMustBeApproved;

         [DataMember]
        private string _allowMultipleReviewsPerUser;

        [DataMember]
         private string _allowMultipleReviewsPerIP;
   
         [DataMember]
        private string _allowAnonymousUserToWriteItemRatingAndReviews;

         [DataMember]
        private string _enableRecentlyViewedItems;

         [DataMember]
        private string _noOfRecentlyViewedItemsDispay;

         [DataMember]
        private string _enableLatestItems;

         [DataMember]
        private string _noOfLatestItemsDisplay;

         [DataMember]
         private string _noOfLatestItemsInARow;

         [DataMember]
         private string _enableBestSellerItems;

         [DataMember]
         private string _noOfBestSellersItemDisplay;

         [DataMember]
         private string _enableSpecialItems;

         [DataMember]
         private string _noOfSpecialItemDisplay;

         [DataMember]
        private string _enableRecentlyComparedItems;

         [DataMember]
        private string _noOfRecentlyComparedItems;

         [DataMember]
         private string _enableRelatedCartItems;

         [DataMember]
         private string _noOfRelatedCartItems;

         [DataMember]
        private string _weightUnit;

         [DataMember]
         private string _noOfPopularTags;
    
        public StoreSettingInfo()
        {

        }


        public string DefaultProductImageURL
        {
            get
            {
                return this._defaultProductImageURL;
            }
            set
            {
                if ((this._defaultProductImageURL != value))
                {
                    this._defaultProductImageURL = value;
                }
            }
        }


        public string MyAccountURL
        {
            get
            {
                return this._myAccountURL;
            }
            set
            {
                if ((this._myAccountURL != value))
                {
                    this._myAccountURL = value;
                }
            }
        }


        public string ShoppingCartURL
        {
            get
            {
                return this._shoppingCartURL;
            }
            set
            {
                if ((this._shoppingCartURL != value))
                {
                    this._shoppingCartURL = value;
                }
            }
        }


        public string WishListURL
        {
            get
            {
                return this._wishListURL;
            }
            set
            {
                if ((this._wishListURL != value))
                {
                    this._wishListURL = value;
                }
            }
        }


        public string MainCurrency
        {
            get
            {
                return this._mainCurrency;
            }
            set
            {
                if ((this._mainCurrency != value))
                {
                    this._mainCurrency = value;
                }
            }
        }


        public string DefaultCountry
        {
            get
            {
                return this._defaultCountry;
            }
            set
            {
                if ((this._defaultCountry != value))
                {
                    this._defaultCountry = value;
                }
            }
        }


        public string StoreName
        {
            get
            {
                return this._storeName;
            }
            set
            {
                if ((this._storeName != value))
                {
                    this._storeName = value;
                }
            }
        }

        public string StoreLogoURL
        {
            get
            {
                return this._storeLogoURL;
            }
            set
            {
                if ((this._storeLogoURL != value))
                {
                    this._storeLogoURL = value;
                }
            }
        }

        public string StoreClosePageContent
        {
            get
            {
                return this._storeClosePageContent;
            }
            set
            {
                if ((this._storeClosePageContent != value))
                {
                    this._storeClosePageContent = value;
                }
            }
        }

        public string StoreClosed
        {
            get
            {
                return this._storeClosed;
            }
            set
            {
                if ((this._storeClosed != value))
                {
                    this._storeClosed = value;
                }
            }
        }
    
        public string StoreNOTAccessPageContent 
        {
            get
            {
                return this._storeNOTAccessPageContent;
            }
            set
            {
                if ((this._storeNOTAccessPageContent != value))
                {
                    this._storeNOTAccessPageContent = value;
                }
            }
        }
    
        public string TimeToDeleteAbandonedCart 
        {
            get
            {
                return this._timeToDeleteAbandonedCart;
            }
            set
            {
                if ((this._timeToDeleteAbandonedCart != value))
                {
                    this._timeToDeleteAbandonedCart = value;
                }
            }
        }

        public string CartAbandonedTime 
        {
            get
            {
                return this._cartAbandonedTime;
            }
            set
            {
                if ((this._cartAbandonedTime != value))
                {
                    this._cartAbandonedTime = value;
                }
            }
        }

        public string GoogleMapAPIKey
        {
            get
            {
                return this._googleMapAPIKey;
            }
            set
            {
                if ((this._googleMapAPIKey != value))
                {
                    this._googleMapAPIKey = value;
                }
            }
        }

        public int LowStockQuantity
        {
            get
            {
                return this._lowStockQuantity;
            }
            set
            {
                if ((this._lowStockQuantity != value))
                {
                    this._lowStockQuantity = value;
                }
            }
        }

        public string ShoppingOptionRange
        {
            get
            {
                return this._shoppingOptionRange;
            }
            set
            {
                if ((this._shoppingOptionRange != value))
                {
                    this._shoppingOptionRange = value;
                }
            }
        }

        public string AllowAnonymousCheckOut
        {
            get
            {
                return this._allowAnonymousCheckOut;
            }
            set
            {
                if ((this._allowAnonymousCheckOut != value))
                {
                    this._allowAnonymousCheckOut = value;
                }
            }
        }

        public string AllowMultipleShippingAddress
        {
            get
            {
                return this._allowMultipleShippingAddress;
            }
            set
            {
                if ((this._allowMultipleShippingAddress != value))
                {
                    this._allowMultipleShippingAddress = value;
                }
            }
        }

        public string SSLSecurePages
        {
            get
            {
                return this._sslSecurePages;
            }
            set
            {
                if ((this._sslSecurePages != value))
                {
                    this._sslSecurePages = value;
                }
            }
        }

        public string AllowOutStockPurchase
        {
            get
            {
                return this._allowOutStockPurchase;
            }
            set
            {
                if ((this._allowOutStockPurchase != value))
                {
                    this._allowOutStockPurchase = value;
                }
            }
        }

        public string SendEcommerceEmailsFrom
        {
            get
            {
                return this._sendEcommerceEmailsFrom;
            }
            set
            {
                if ((this._sendEcommerceEmailsFrom != value))
                {
                    this._sendEcommerceEmailsFrom = value;
                }
            }
        }

        public string SendEcommerceEmailTo
        {
            get
            {
                return this._sendEcommerceEmailTo;
            }
            set
            {
                if ((this._sendEcommerceEmailTo != value))
                {
                    this._sendEcommerceEmailTo = value;
                }
            }
        }

        public string SendOrderNotification
        {
            get
            {
                return this._sendOrderNotification;
            }
            set
            {
                if ((this._sendOrderNotification != value))
                {
                    this._sendOrderNotification = value;
                }
            }
        }

        public string SendPaymentNotification
        {
            get
            {
                return this._sendPaymentNotification;
            }
            set
            {
                if ((this._sendPaymentNotification != value))
                {
                    this._sendPaymentNotification = value;
                }
            }
        }

        public string StoreAdminEmail
        {
            get
            {
                return this._storeAdminEmail;
            }
            set
            {
                if ((this._storeAdminEmail != value))
                {
                    this._storeAdminEmail = value;
                }
            }
        }

        public string EnableStoreNamePrefix
        {
            get
            {
                return this._enableStoreNamePrefix;
            }
            set
            {
                if ((this._enableStoreNamePrefix != value))
                {
                    this._enableStoreNamePrefix = value;
                }
            }
        }

        public string DefaultTitle
        {
            get
            {
                return this._defaultTitle;
            }
            set
            {
                if ((this._defaultTitle != value))
                {
                    this._defaultTitle = value;
                }
            }
        }

        public string DefaultMetaDescription
        {
            get
            {
                return this._defaultMetaDescription;
            }
            set
            {
                if ((this._defaultMetaDescription != value))
                {
                    this._defaultMetaDescription = value;
                }
            }
        }

        public string DefaultMetaKeyWords
        {
            get
            {
                return this._defaultMetaKeyWords;
            }
            set
            {
                if ((this._defaultMetaKeyWords != value))
                {
                    this._defaultMetaKeyWords = value;
                }
            }
        }

        public string ShowWelcomeMessageOnHomePage
        {
            get
            {
                return this._showWelcomeMessageOnHomePage;
            }
            set
            {
                if ((this._showWelcomeMessageOnHomePage != value))
                {
                    this._showWelcomeMessageOnHomePage = value;
                }
            }
        }

        public string DisplayNewsRSSFeedLinkInBrowserAddressBar
        {
            get
            {
                return this._displayNewsRSSFeedLinkInBrowserAddressBar;
            }
            set
            {
                if ((this._displayNewsRSSFeedLinkInBrowserAddressBar != value))
                {
                    this._displayNewsRSSFeedLinkInBrowserAddressBar = value;
                }
            }
        }

        public string DisplayBlogRSSFeedLinkInBrowserAddressBar
        {
            get
            {
                return this._displayBlogRSSFeedLinkInBrowserAddressBar;
            }
            set
            {
                if ((this._displayBlogRSSFeedLinkInBrowserAddressBar != value))
                {
                    this._displayBlogRSSFeedLinkInBrowserAddressBar = value;
                }
            }
        }

        public string MaximumImageSize
        {
            get
            {
                return this._maximumImageSize;
            }
            set
            {
                if ((this._maximumImageSize != value))
                {
                    this._maximumImageSize = value;
                }
            }
        }

        public string ItemLargeThumbnailImageSize
        {
            get
            {
                return this._itemLargeThumbnailImageSize;
            }
            set
            {
                if ((this._itemLargeThumbnailImageSize != value))
                {
                    this._itemLargeThumbnailImageSize = value;
                }
            }
        }

        public string ItemMediumThumbnailImageSize
        {
            get
            {
                return this._itemMediumThumbnailImageSize;
            }
            set
            {
                if ((this._itemMediumThumbnailImageSize != value))
                {
                    this._itemMediumThumbnailImageSize = value;
                }
            }
        }

        public string ItemSmallThumbnailImageSize
        {
            get
            {
                return this._itemSmallThumbnailImageSize;
            }
            set
            {
                if ((this._itemSmallThumbnailImageSize != value))
                {
                    this._itemSmallThumbnailImageSize = value;
                }
            }
        }

        public string CategoryLargeThumbnailImageSize
        {
            get
            {
                return this._categoryLargeThumbnailImageSize;
            }
            set
            {
                if ((this._categoryLargeThumbnailImageSize != value))
                {
                    this._categoryLargeThumbnailImageSize = value;
                }
            }
        }

        public string CategoryMediumThumbnailImageSize
        {
            get
            {
                return this._categoryMediumThumbnailImageSize;
            }
            set
            {
                if ((this._categoryMediumThumbnailImageSize != value))
                {
                    this._categoryMediumThumbnailImageSize = value;
                }
            }
        }

        public string CategorySmallThumbnailImageSize
        {
            get
            {
                return this._categorySmallThumbnailImageSize;
            }
            set
            {
                if ((this._categorySmallThumbnailImageSize != value))
                {
                    this._categorySmallThumbnailImageSize = value;
                }
            }
        }

        public string ShowItemImagesInCart
        {
            get
            {
                return this._showItemImagesInCart;
            }
            set
            {
                if ((this._showItemImagesInCart != value))
                {
                    this._showItemImagesInCart = value;
                }
            }
        }

        public string ShowItemImagesInWishList
        {
            get
            {
                return this._showItemImagesInWishList;
            }
            set
            {
                if ((this._showItemImagesInWishList != value))
                {
                    this._showItemImagesInWishList = value;
                }
            }
        }

        public string AllowUsersToCreateMultipleAddress
        {
            get
            {
                return this._allowUsersToCreateMultipleAddress;
            }
            set
            {
                if ((this._allowUsersToCreateMultipleAddress != value))
                {
                    this._allowUsersToCreateMultipleAddress = value;
                }
            }
        }

        public string AllowUsersToStoreCreditCardDataInProfile
        {
            get
            {
                return this._allowUsersToStoreCreditCardDataInProfile;
            }
            set
            {
                if ((this._allowUsersToStoreCreditCardDataInProfile != value))
                {
                    this._allowUsersToStoreCreditCardDataInProfile = value;
                }
            }
        }

        public string MinimumOrderAmount
        {
            get
            {
                return this._minimumOrderAmount;
            }
            set
            {
                if ((this._minimumOrderAmount != value))
                {
                    this._minimumOrderAmount = value;
                }
            }
        }

        public string MinimumItemQuantity
        {
            get
            {
                return this._minimumItemQuantity;
            }
            set
            {
                if ((this._minimumItemQuantity != value))
                {
                    this._minimumItemQuantity = value;
                }
            }
        }

        public string MaximumItemQuantity
        {
            get
            {
                return this._maximumItemQuantity;
            }
            set
            {
                if ((this._maximumItemQuantity != value))
                {
                    this._maximumItemQuantity = value;
                }
            }
        }

        public string AllowCustomerToSignUpForUserGroup
        {
            get
            {
                return this._allowCustomerToSignUpForUserGroup;
            }
            set
            {
                if ((this._allowCustomerToSignUpForUserGroup != value))
                {
                    this._allowCustomerToSignUpForUserGroup = value;
                }
            }
        }

        public string AllowCustomersToPayOrderAgainIfTransactionWasDeclined
        {
            get
            {
                return this._allowCustomersToPayOrderAgainIfTransactionWasDeclined;
            }
            set
            {
                if ((this._allowCustomersToPayOrderAgainIfTransactionWasDeclined != value))
                {
                    this._allowCustomersToPayOrderAgainIfTransactionWasDeclined = value;
                }
            }
        }

        public string AllowPrivateMessages
        {
            get
            {
                return this._allowPrivateMessages;
            }
            set
            {
                if ((this._allowPrivateMessages != value))
                {
                    this._allowPrivateMessages = value;
                }
            }
        }

        public string DefaultStoreTimeZone
        {
            get
            {
                return this._defaultStoreTimeZone;
            }
            set
            {
                if ((this._defaultStoreTimeZone != value))
                {
                    this._defaultStoreTimeZone = value;
                }
            }
        }

        public string EnableCompareItems
        {
            get
            {
                return this._enableCompareItems;
            }
            set
            {
                if ((this._enableCompareItems != value))
                {
                    this._enableCompareItems = value;
                }
            }
        }

        public string MaxNoOfItemsToCompare
        {
            get
            {
                return this._maxNoOfItemsToCompare;
            }
            set
            {
                if ((this._maxNoOfItemsToCompare != value))
                {
                    this._maxNoOfItemsToCompare = value;
                }
            }
        }

        public string EnableWishList
        {
            get
            {
                return this._enableWishList;
            }
            set
            {
                if ((this._enableWishList != value))
                {
                    this._enableWishList = value;
                }
            }
        }

        public string NoOfRecentAddedWishItems
        {
            get
            {
                return this._noOfRecentAddedWishItems;
            }
            set
            {
                if ((this._noOfRecentAddedWishItems != value))
                {
                    this._noOfRecentAddedWishItems = value;
                }
            }
        }

        public string EnableEmailAFriend
        {
            get
            {
                return this._enableEmailAFriend;
            }
            set
            {
                if ((this._enableEmailAFriend != value))
                {
                    this._enableEmailAFriend = value;
                }
            }
        }

        public string ShowMiniShoppingCart
        {
            get
            {
                return this._showMiniShoppingCart;
            }
            set
            {
                if ((this._showMiniShoppingCart != value))
                {
                    this._showMiniShoppingCart = value;
                }
            }
        }

        public string NotifyAboutNewItemReviews
        {
            get
            {
                return this._notifyAboutNewItemReviews;
            }
            set
            {
                if ((this._notifyAboutNewItemReviews != value))
                {
                    this._notifyAboutNewItemReviews = value;
                }
            }
        }

        public string ItemReviewMustBeApproved
        {
            get
            {
                return this._itemReviewMustBeApproved;
            }
            set
            {
                if ((this._itemReviewMustBeApproved != value))
                {
                    this._itemReviewMustBeApproved = value;
                }
            }
        }

        public string AllowMultipleReviewsPerUser
        {
            get
            {
                return this._allowMultipleReviewsPerUser;
            }
            set
            {
                if ((this._allowMultipleReviewsPerUser != value))
                {
                    this._allowMultipleReviewsPerUser = value;
                }
            }
        }

        public string AllowMultipleReviewsPerIP
        {
            get
            {
                return this._allowMultipleReviewsPerIP;
            }
            set
            {
                if ((this._allowMultipleReviewsPerIP != value))
                {
                    this._allowMultipleReviewsPerIP = value;
                }
            }
        }

        public string AllowAnonymousUserToWriteItemRatingAndReviews
        {
            get
            {
                return this._allowAnonymousUserToWriteItemRatingAndReviews;
            }
            set
            {
                if ((this._allowAnonymousUserToWriteItemRatingAndReviews != value))
                {
                    this._allowAnonymousUserToWriteItemRatingAndReviews = value;
                }
            }
        }

        public string EnableRecentlyViewedItems
        {
            get
            {
                return this._enableRecentlyViewedItems;
            }
            set
            {
                if ((this._enableRecentlyViewedItems != value))
                {
                    this._enableRecentlyViewedItems = value;
                }
            }
        }

        public string NoOfRecentlyViewedItemsDispay
        {
            get
            {
                return this._noOfRecentlyViewedItemsDispay;
            }
            set
            {
                if ((this._noOfRecentlyViewedItemsDispay != value))
                {
                    this._noOfRecentlyViewedItemsDispay = value;
                }
            }
        }

        public string EnableLatestItems
        {
            get
            {
                return this._enableLatestItems;
            }
            set
            {
                if ((this._enableLatestItems != value))
                {
                    this._enableLatestItems = value;
                }
            }
        }

        public string NoOfLatestItemsDisplay
        {
            get
            {
                return this._noOfLatestItemsDisplay;
            }
            set
            {
                if ((this._noOfLatestItemsDisplay != value))
                {
                    this._noOfLatestItemsDisplay = value;
                }
            }
        }

        public string NoOfLatestItemsInARow
        {
            get
            {
                return this._noOfLatestItemsInARow;
            }
            set
            {
                if ((this._noOfLatestItemsInARow != value))
                {
                    this._noOfLatestItemsInARow = value;
                }
            }
        }


        public string EnableBestSellerItems
        {
            get
            {
                return this._enableBestSellerItems;
            }
            set
            {
                if ((this._enableBestSellerItems != value))
                {
                    this._enableBestSellerItems = value;
                }
            }
        }

        public string NoOfBestSellersItemDisplay
        {
            get
            {
                return this._noOfBestSellersItemDisplay;
            }
            set
            {
                if ((this._noOfBestSellersItemDisplay != value))
                {
                    this._noOfBestSellersItemDisplay = value;
                }
            }
        }

        public string EnableSpecialItems
        {
            get
            {
                return this._enableSpecialItems;
            }
            set
            {
                if ((this._enableSpecialItems != value))
                {
                    this._enableSpecialItems = value;
                }
            }
        }

        public string NoOfSpecialItemDisplay
        {
            get
            {
                return this._noOfSpecialItemDisplay;
            }
            set
            {
                if ((this._noOfSpecialItemDisplay != value))
                {
                    this._noOfSpecialItemDisplay = value;
                }
            }
        }

        public string EnableRecentlyComparedItems
        {
            get
            {
                return this._enableRecentlyComparedItems;
            }
            set
            {
                if ((this._enableRecentlyComparedItems != value))
                {
                    this._enableRecentlyComparedItems = value;
                }
            }
        }

        public string NoOfRecentlyComparedItems
        {
            get
            {
                return this._noOfRecentlyComparedItems;
            }
            set
            {
                if ((this._noOfRecentlyComparedItems != value))
                {
                    this._noOfRecentlyComparedItems = value;
                }
            }
        }

        public string EnableRelatedCartItems
        {
            get
            {
                return this._enableRelatedCartItems;
            }
            set
            {
                if ((this._enableRelatedCartItems != value))
                {
                    this._enableRelatedCartItems = value;
                }
            }
        }
    

        public string NoOfRelatedCartItems
        {
            get
            {
                return this._noOfRelatedCartItems;
            }
            set
            {
                if ((this._noOfRelatedCartItems != value))
                {
                    this._noOfRelatedCartItems = value;
                }
            }
        }

        public string NoOfPopularTags
        {
            get
            {
                return this._noOfPopularTags;
            }
            set
            {
                if ((this._noOfPopularTags != value))
                {
                    this._noOfPopularTags = value;
                }
            }
        }

        public string WeightUnit
        {
            get
            {
                return this._weightUnit;
            }
            set
            {
                if ((this._weightUnit != value))
                {
                    this._weightUnit = value;
                }
            }
        }      
    }
}

