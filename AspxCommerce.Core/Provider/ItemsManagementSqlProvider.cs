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
using System.Collections.Generic;

using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class ItemsManagementSqlProvider
    {
        public ItemsManagementSqlProvider()
        {
        }

        /// <summary>
        /// To Bind grid with all Items
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="isActive"></param>
        /// <param name="storeId"></param>
        /// <param name="portalId"></param>
        /// <param name="sku"></param>
        /// <param name="name"></param>
        /// <param name="itemType"></param>
        /// <param name="attributesetName"></param>
        /// <param name="visibility"></param>
        /// <param name="userName"></param>
        /// <param name="cultureName"></param>
        /// <returns></returns>

        public List<ItemsInfo> GetAllItems(int offset, int limit, string sku, string name, string itemType, string attributesetName, string visibility, System.Nullable<bool> isActive, int storeId, int portalId, string userName, string cultureName)
        {
            List<ItemsInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@SKU", sku));
            parameterCollection.Add(new KeyValuePair<string, object>("@Name", name));
            parameterCollection.Add(new KeyValuePair<string, object>("@ItemType", itemType));
            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetName", attributesetName));
            parameterCollection.Add(new KeyValuePair<string, object>("@Visibility", visibility));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ml = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_ItemsGetAll", parameterCollection);
            return ml;
        }
        /// <summary>
        /// To Bind grid with all Related Items
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="storeId"></param>
        /// <param name="portalId"></param>
        /// <param name="selfItemId"></param>
        /// <param name="userName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public List<ItemsInfo> GetRelatedItemsByItemID(int offset, int limit, int storeId, int portalId, int selfItemId, string userName, string culture)
        {
            List<ItemsInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameterCollection.Add(new KeyValuePair<string, object>("@SelfItemID", selfItemId));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
            ml = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_GetRelatedItemsByItemID", parameterCollection);
            return ml;
        }
        /// <summary>
        /// To Bind grid with all UP Sell Items
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="storeId"></param>
        /// <param name="portalId"></param>
        /// <param name="selfItemId"></param>
        /// <param name="userName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public List<ItemsInfo> GetUpSellItemsByItemID(int offset, int limit, int storeId, int portalId, int selfItemId, string userName, string culture)
        {
            List<ItemsInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameterCollection.Add(new KeyValuePair<string, object>("@SelfItemID", selfItemId));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
            ml = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_GetUpSellItemsByItemID", parameterCollection);
            return ml;
        }

        /// <summary>
        /// To Bind grid with all Cross Sell Items
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="storeId"></param>
        /// <param name="portalId"></param>
        /// <param name="selfItemId"></param>
        /// <param name="userName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public List<ItemsInfo> GetCrossSellItemsByItemID(int offset, int limit, int storeId, int portalId, int selfItemId, string userName, string culture)
        {
            List<ItemsInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameterCollection.Add(new KeyValuePair<string, object>("@SelfItemID", selfItemId));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
            ml = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_GetCrossSellItemsByItemID", parameterCollection);
            return ml;
        }
       
        /// <summary>
        /// To Delete Multiple Item IDs
        /// </summary>
        /// <param name="itemIds"></param>
        /// <param name="storeId"></param>
        /// <param name="portalId"></param>
        /// <param name="userName"></param>
        public void DeleteMultipleItems(string itemIds, int storeId, int portalId, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemIDs", itemIds));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_ItemsDeleteMultipleSelected", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// To Delete Single Item ID
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="storeId"></param>
        /// <param name="portalId"></param>
        /// <param name="userName"></param>
        public void DeleteSingleItem(string itemId, int storeId, int portalId, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_DeleteItemByItemID", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributeFormInfo> GetItemFormAttributes(int attributeSetID, int itemTypeID, int storeID, int portalID, string userName, string culture)
        {
            List<AttributeFormInfo> formAttributeList;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetID));
            parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemTypeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));            
            parameterCollection.Add(new KeyValuePair<string, object>("@Username", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
            formAttributeList = sqlH.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetItemFormAttributes", parameterCollection);
            return formAttributeList;
        }

        public List<AttributeFormInfo> GetItemFormAttributesByItemSKUOnly(string itemSKU, int storeID, int portalID, string userName, string culture)
        {
            List<AttributeFormInfo> formAttributeList;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@Username", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
            formAttributeList = sqlH.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetItemFormAttributesByItemSKU", parameterCollection);
            return formAttributeList;
        }

        public List<TaxRulesInfo> GetAllTaxRules(int storeID, int portalID, bool isActive)
        {
            List<TaxRulesInfo> lstTaxManageRule;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));

            lstTaxManageRule = sqlH.ExecuteAsList<TaxRulesInfo>("dbo.usp_Aspx_TaxRuleGetAll", parameterCollection);
            return lstTaxManageRule;
        }

        public int AddItem(int itemID, int itemTypeID, int attributeSetID, int taxRuleID, int storeID, int portalID, string userName, string culture, bool isActive, bool isModified,
			string sku, string activeFrom, string activeTo, string hidePrice, string isHideInRSS, string isHideToAnonymous,
            string categoriesIDs, string relatedItemsIDs, string upSellItemsIDs, string crossSellItemsIDs, string downloadItemsValue, bool updateFlag)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@SKU", sku));
                parameterCollection.Add(new KeyValuePair<string, object>("@TaxRuleID", taxRuleID));
                parameterCollection.Add(new KeyValuePair<string, object>("@ActiveFrom", activeFrom));
                parameterCollection.Add(new KeyValuePair<string, object>("@ActiveTo", activeTo));
                parameterCollection.Add(new KeyValuePair<string, object>("@HidePrice", hidePrice));
                parameterCollection.Add(new KeyValuePair<string, object>("@HideInRSSFeed", isHideInRSS));
                parameterCollection.Add(new KeyValuePair<string, object>("@HideToAnonymous", isHideToAnonymous));
                //For Static tabs
                parameterCollection.Add(new KeyValuePair<string, object>("@CategoriesIDs", categoriesIDs));
                parameterCollection.Add(new KeyValuePair<string, object>("@RelatedItemsIDs", relatedItemsIDs));
                parameterCollection.Add(new KeyValuePair<string, object>("@UpSellItemsIDs", upSellItemsIDs));
                parameterCollection.Add(new KeyValuePair<string, object>("@CrossSellItemsIDs", crossSellItemsIDs));                

                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", isModified));
                parameterCollection.Add(new KeyValuePair<string, object>("@DownloadInfos", downloadItemsValue));
                parameterCollection.Add(new KeyValuePair<string, object>("@UpdateFlag", updateFlag)); 
                
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsGivenType<int>("dbo.usp_Aspx_ItemAddUpdate", parameterCollection, "@NewItemID");

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveUpdateItemAttributes(int itemID, int attributeSetID, int storeID, int portalID, string userName, string culture, bool isActive, bool isModified, string attribValue, int attributeID, int inputTypeID, int validationTypeID, int attributeSetGroupID, bool isIncludeInPriceRule, bool isIncludeInPromotions, int displayOrder)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", isModified));
                //parameterCollection.Add(new KeyValuePair<string, object>("@UpdateFlag", updateFlag));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", attribValue));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", inputTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@ValidationTypeID", validationTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetGroupID", attributeSetGroupID));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule", isIncludeInPriceRule));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPromotions", isIncludeInPromotions));
                parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                SQLHandler sqlH = new SQLHandler();
                //inputTypeID //validationTypeID
                string valueType = string.Empty;
                if (inputTypeID == 1)
                {
                    if (validationTypeID == 3)
                    {
                        valueType = "DECIMAL";
                    }
                    else if (validationTypeID == 5)
                    {
                        valueType = "INT";
                    }
                    else
                    {
                        valueType = "NVARCHAR";
                    }
                }
                else if (inputTypeID == 2)
                {
                    valueType = "TEXT";
                }
                else if (inputTypeID == 3)
                {
                    valueType = "DATE";
                }
                else if (inputTypeID == 4)
                {
                    valueType = "Boolean";
                }
                else if (inputTypeID == 5 || inputTypeID == 6 || inputTypeID == 9 || inputTypeID == 10 ||
                         inputTypeID == 11 || inputTypeID == 12)
                {
                    valueType = "OPTIONS";
                }
                else if (inputTypeID == 7)
                {
                    valueType = "DECIMAL";
                }
                else if (inputTypeID == 8)
                {
                    valueType = "FILE";
                }
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_ItemAttributesValue" + valueType + "AddUpdate", parameterCollection);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// make the Item active deactive
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="storeId"></param>
        /// <param name="portalId"></param>
        /// <param name="userName"></param>
        /// <param name="isActive"></param>
        public void UpdateItemIsActive(int itemId, int storeId, int portalId, string userName, bool isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_UpdateItemIsActiveByItemID", parameterCollection);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributeFormInfo> GetItemAttributesValuesByItemID(int itemID, int attributeSetID, int itemTypeID, int storeID, int portalID, string userName, string culture)
        {
            try
            {
                List<AttributeFormInfo> itemAttributes;
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@Username", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
                itemAttributes = sqlH.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetItemFormAttributesValuesByItemID", parameterCollection);
                return itemAttributes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryInfo> GetCategoryList(string prefix, bool isActive, string cultureName, Int32 storeID, Int32 portalID, string userName, int itemId)
        {
            List<CategoryInfo> catList;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@Prefix", prefix));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@Username", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
            catList = sqlH.ExecuteAsList<CategoryInfo>("dbo.usp_Aspx_GetCategoryListForCatalog", parameterCollection);
            return catList;
        }

        public bool CheckUniqueSKUCode(string sku, int itemId, int storeId, int portalId, string cultureName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@SKU", sku));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                return sqlH.ExecuteNonQueryAsBool("dbo.usp_Aspx_ItemSKUCodeUniquenessCheck", parameterCollection, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteItemImageByItemID(int itemId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_DeleteItemImageByItemID", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FeaturedItemsInfo> GetFeaturedItemsByCount(int storeId, int portalId, string userName, string cultureName, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<FeaturedItemsInfo>("dbo.usp_Aspx_FeaturedItemsGetByCount", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LatestItemsInfo> GetLatestItemsByCount(int storeId, int portalId, string userName, string cultureName, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));                
                parameterCollection.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<LatestItemsInfo>("dbo.usp_Aspx_LatestItemsGetByCount", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AttributeFormInfo> GetItemDetailsInfoByItemSKU(string itemSKU, int attributeSetID, int itemTypeID, int storeID, int portalID, string userName, string culture)
        {
            try
            {
                List<AttributeFormInfo> itemAttributes;
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@Username", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
                itemAttributes = sqlH.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetItemDetailsByItemSKU", parameterCollection);
                return itemAttributes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ItemBasicDetailsInfo GetItemBasicInfo(string itemSKU, int storeID, int portalID, string userName, string culture)
        {
            try
            {
                ItemBasicDetailsInfo itemBasicDetails;
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@Username", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
                itemBasicDetails = sqlH.ExecuteAsObject<ItemBasicDetailsInfo>("dbo.usp_Aspx_ItemsGetBasicInfos", parameterCollection);
                return itemBasicDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CostVariantInfo> GetAllCostVariantOptions(int itemId, int storeId, int portalId, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CostVariantInfo>("dbo.usp_Aspx_BindCostVariantsInDropdownList", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}

        //--------------------------GetMostViewedItems---------------------------
        public List<MostViewedItemsInfo> GetAllMostViewedItems(int offset, System.Nullable<int> limit, string name, int storeId, int portalId, string userName, string cultureName)
        {
            List<MostViewedItemsInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@Name", name));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ml = sqlH.ExecuteAsList<MostViewedItemsInfo>("usp_Aspx_GetMostViewedItems", parameterCollection);
            return ml;
        }
        //----------------------------------------------------------------------------------------------
        //-----------------------------------Get Low Stock Items----------------------------------------
        public List<LowStockItemsInfo> GetAllLowStockItems(int offset, System.Nullable<int> limit, string Sku, string name, System.Nullable<bool> isActive, int storeId, int portalId, string userName, string cultureName, int lowStock)
        {
            List<LowStockItemsInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@SKU", Sku));
            parameterCollection.Add(new KeyValuePair<string, object>("@Name", name));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("@LowStockQuantity", lowStock));
            ml = sqlH.ExecuteAsList<LowStockItemsInfo>("dbo.usp_Aspx_GetLowStockItems", parameterCollection);
            return ml;
        }
        //-------------------------------------------------------------------------------------------------
        //--------------------------Get Ordered Items List------------------------------------------------
        public List<OrderItemsGroupByItemIDInfo> GetOrderedItemsList(int offset, System.Nullable<int> limit, string name, int storeId, int portalId, string userName, string cultureName)
        {
            List<OrderItemsGroupByItemIDInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@Name", name));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ml = sqlH.ExecuteAsList<OrderItemsGroupByItemIDInfo>("dbo.usp_Aspx_GetItemsOrdered", parameterCollection);
            return ml;
        }
        //----------------------------------------------------------------------------------------------
        //-----------------------------------Get DownLoadable Items----------------------------------------
        public List<DownLoadableItemGetInfo> GetDownLoadableItemsList(int offset, System.Nullable<int> limit, string Sku, string name, int storeId, int portalId, string userName, string cultureName, System.Nullable<bool> checkUser)
        {
            List<DownLoadableItemGetInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@SKU", Sku));
            parameterCollection.Add(new KeyValuePair<string, object>("@Name", name));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CheckUser", checkUser));

            ml = sqlH.ExecuteAsList<DownLoadableItemGetInfo>("dbo.usp_Aspx_GetDownloadableItemsForReport", parameterCollection);
            return ml;
        }
    }
}
