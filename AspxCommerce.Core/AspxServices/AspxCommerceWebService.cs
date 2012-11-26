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
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using SageFrame.Security;
using SageFrame.Security.Entities;
using System.Web.Security;
using SageFrame.Security.Helpers;
using AspxCommerce.Core;

    /// <summary>
    /// Summary description for AspxCommerceWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AspxCommerceWebService : System.Web.Services.WebService
    {

        public AspxCommerceWebService()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        #region Testing Method
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public bool CheckSessionActive()
        {
            if (HttpContext.Current.User != null)
            {
                if (Membership.GetUser() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Header Menu category Lister
        [WebMethod]
        public List<CategoryInfo> GetCategoryMenuList(int storeID, int portalID, string cultureName)
        {
            List<CategoryInfo> catInfo;
            List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paramCol.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sageSQL = new SQLHandler();
            catInfo = sageSQL.ExecuteAsList<CategoryInfo>("[dbo].[usp_Aspx_GetCategoryMenuAttributes]", paramCol);

            return catInfo;
        }
        #endregion

        #region Aspx BreadCrumb
        [WebMethod]
        public string GetCategoryForItem(int storeID, int portalID, string itemSku)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@ItemSku", itemSku));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsScalar<string>("usp_Aspx_GetCategoryforItems", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region General Functions
        //--------------------Roles Lists------------------------    
        [WebMethod]
        public List<PortalRole> GetAllRoles(Int32 storeID, Int32 portalID, string userName, string culture)
        {
            try
            {
                List<PortalRole> portalRoleCollection;
                PriceRuleController priceRuleController = new PriceRuleController();
                portalRoleCollection = priceRuleController.GetPortalRoles(portalID, true, userName);
                return portalRoleCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------Store Lists------------------------    
        [WebMethod]
        public List<StoreInfo> GetAllStores(int portalID, string userName, string culture)
        {
            StoreSqlProvider storeSqlProvider = new StoreSqlProvider();
            return storeSqlProvider.GetAllStores(portalID, userName, culture);
        }

        //----------------country list------------------------------    
        [WebMethod]
        public List<CountryInfo> BindCountryList()
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CountryInfo>("usp_Aspx_BindTaxCountryList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------state list--------------------------    
        [WebMethod]
        public List<StateInfo> BindStateList()
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<StateInfo>("usp_Aspx_BindStateList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //----------------Currency List----------------------
        //[WebMethod]
        //public List<CurrencyInfo> BindCurrencyList()
        //{
        //    try
        //    {
        //        SQLHandler sqlH = new SQLHandler();
        //        return sqlH.ExecuteAsList<CurrencyInfo>("usp_Aspx_BindCurrencyList");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        #region Status Management
        //------------------Status DropDown-------------------    
        [WebMethod]
        public List<StatusInfo> GetStatus(string cultureName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                return sqlH.ExecuteAsList<StatusInfo>("usp_Aspx_GetStatusList", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Bind Users DropDown
        [WebMethod]
        public List<UserInRoleInfo> BindRoles(int portalID, bool isAll, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@IsAll", isAll));
                parameter.Add(new KeyValuePair<string, object>("@Username", userName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<UserInRoleInfo>("sp_PortalRoleList", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Attributes Management
        [WebMethod]
        public List<AttributesInputTypeInfo> GetAttributesInputTypeList()
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                return obj.GetAttributesInputType();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AttributesItemTypeInfo> GetAttributesItemTypeList(int storeId, int portalId)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                return obj.GetAttributesItemType(storeId, portalId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AttributesValidationTypeInfo> GetAttributesValidationTypeList()
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                return obj.GetAttributesValidationType();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AttributesBasicInfo> GetAttributesList(int offset, int limit, string attributeName, System.Nullable<bool> isRequired, System.Nullable<bool> comparable, System.Nullable<bool> isSystem, int storeId, int portalId, string cultureName, string userName)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                return obj.GetItemAttributes(offset, limit, attributeName, isRequired, comparable, isSystem, storeId, portalId, cultureName, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AttributesGetByAttributeIdInfo> GetAttributeDetailsByAttributeID(int attributeId, int storeId, int portalId, string userName)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                return obj.GetAttributesInfoByAttributeID(attributeId, storeId, portalId, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteMultipleAttributesByAttributeID(string attributeIds, int storeId, int portalId, string userName)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                obj.DeleteMultipleAttributes(attributeIds, storeId, portalId, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteAttributeByAttributeID(int attributeId, int storeId, int portalId, string userName)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                obj.DeleteAttribute(attributeId, storeId, portalId, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void UpdateAttributeIsActiveByAttributeID(int attributeId, int storeId, int portalId, string userName, bool isActive)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                obj.UpdateAttributeIsActive(attributeId, storeId, portalId, userName, isActive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void SaveUpdateAttributeInfo(AttributesGetByAttributeIdInfo attributeInfo)
        {
            try
            {
                AttributesGetByAttributeIdInfo attributeInfoToInsert = attributeInfo;
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                obj.SaveAttribute(attributeInfoToInsert);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region AttributeSet Management
        [WebMethod]
        public List<AttributeSetBaseInfo> GetAttributeSetGrid(int offset, int limit, string attributeSetName, System.Nullable<bool> isActive, System.Nullable<bool> usedInSystem, int storeId, int portalId, string cultureName, string userName)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                return obj.GetAttributeSetGrid(offset, limit, attributeSetName, isActive, usedInSystem, storeId, portalId, cultureName, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [WebMethod]
        public List<AttributeSetInfo> GetAttributeSetList(int storeId, int portalId)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                return obj.GetAttributeSet(storeId, portalId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [WebMethod]
        public int SaveUpdateAttributeSetInfo(int attributeSetId, int attributeSetBaseId, string attributeSetName, int storeId, int portalId,
                                              bool isActive, bool isModified, string userName, bool flag, string saveString)
        {
            try
            {
                AttributeSetInfo attributeSetInfoToInsert = new AttributeSetInfo
                                                                {
                                                                    AttributeSetID = attributeSetId,
                                                                    AttributeSetBaseID = attributeSetBaseId,
                                                                    AttributeSetName = attributeSetName,
                                                                    StoreID = storeId,
                                                                    PortalID = portalId,
                                                                    IsActive = isActive,
                                                                    IsModified = isModified,
                                                                    UpdatedBy = userName,
                                                                    AddedBy = userName,
                                                                    Flag = flag,
                                                                    SaveString = saveString
                                                                };
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                return obj.SaveUpdateAttributeSet(attributeSetInfoToInsert);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public int CheckAttributeSetUniqueness(int attributeSetId, string attributeSetName, int storeId, int portalId, bool updateFlag)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                return obj.CheckAttributeSetUniqueName(attributeSetId, attributeSetName, storeId, portalId, updateFlag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AttributeSetGetByAttributeSetIdInfo> GetAttributeSetDetailsByAttributeSetID(int attributeSetId, int storeId, int portalId, string userName, string cultureName)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                return obj.GetAttributeSetInfoByAttributeSetID(attributeSetId, storeId, portalId, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteAttributeSetByAttributeSetID(int attributeSetId, int storeId, int portalId, string userName)
        {
            try
            {
                //AttributeSetInfo attributeSetInfoToInsert = new AttributeSetInfo
                //{
                //    AttributeSetID = attributeSetId,
                //    StoreID = storeId,
                //    PortalID = portalId,
                //    DeletedBy = userName
                //};
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                obj.DeleteAttributeSet(attributeSetId, storeId, portalId, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void UpdateAttributeSetIsActiveByAttributeSetID(int attributeSetId, int storeId, int portalId, string userName, bool isActive)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                obj.UpdateAttributeSetIsActive(attributeSetId, storeId, portalId, userName, isActive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void SaveUpdateAttributeGroupInfo(int attributeSetId, string groupName, int groupID, string cultureName, string aliasName, int storeId, int portalId, string userName, bool isActive, bool isModified, bool flag)
        {
            //attributeSetId: 1, groupName: node, groupID: _groupId, cultureName: cultureName, AliasName: node, storeId: _storeId, portalId: _portalId, userName: _userName, isActive: _isActive, isModified: _isModified, flag: _updateFlag
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                obj.UpdateAttributeGroup(attributeSetId, groupName, groupID, cultureName, aliasName, storeId, portalId, userName, isActive, isModified, flag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteAttributeSetGroupByAttributeSetID(int attributeSetId, int groupId, int storeId, int portalId, string userName, string cultureName)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                obj.DeleteAttributeSetGroup(attributeSetId, groupId, storeId, portalId, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AttributeSetGroupAliasInfo> RenameAttributeSetGroupAliasByGroupID(int groupId, string cultureName, string aliasName, int attributeSetId, int storeId, int portalId, bool isActive, bool isModified, string userName)
        {
            try
            {
                AttributeSetGroupAliasInfo attributeSetInfoToUpdate = new AttributeSetGroupAliasInfo
                                                                          {
                                                                              GroupID = groupId,
                                                                              CultureName = cultureName,
                                                                              AliasName = aliasName,
                                                                              AttributeSetID = attributeSetId,
                                                                              StoreID = storeId,
                                                                              PortalID = portalId,
                                                                              IsActive = isActive,
                                                                              IsModified = isModified,
                                                                              UpdatedBy = userName
                                                                          };
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                return obj.RenameAttributeSetGroupAlias(attributeSetInfoToUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteAttributeByAttributeSetID(int attributeSetId, int groupId, int attributeId, int storeId, int portalId, string userName)
        {
            try
            {
                ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
                obj.DeleteAttribute(attributeSetId, groupId, attributeId, storeId, portalId, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Items Management
        [WebMethod]
        public List<ItemsInfo> GetItemsList(int offset, int limit, string sku, string name, string itemType, string attributesetName, string visibility, System.Nullable<bool> isActive, int storeId, int portalId, string userName, string cultureName)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                return obj.GetAllItems(offset, limit, sku, name, itemType, attributesetName, visibility, isActive, storeId, portalId, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteMultipleItemsByItemID(string itemIds, int storeId, int portalId, string userName)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                obj.DeleteMultipleItems(itemIds, storeId, portalId, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteItemByItemID(string itemId, int storeId, int portalId, string userName)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                obj.DeleteSingleItem(itemId, storeId, portalId, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AttributeFormInfo> GetItemFormAttributes(int attributeSetID, int itemTypeID, int storeID, int portalID, string userName, string culture)
        {
            ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
            List<AttributeFormInfo> frmItemFieldList = obj.GetItemFormAttributes(attributeSetID, itemTypeID, storeID, portalID, userName, culture);
            return frmItemFieldList;
        }

        [WebMethod]
        public List<AttributeFormInfo> GetItemFormAttributesByitemSKUOnly(string itemSKU, int storeID, int portalID, string userName, string culture)
        {
            ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
            List<AttributeFormInfo> frmItemFieldList = obj.GetItemFormAttributesByItemSKUOnly(itemSKU, storeID, portalID, userName, culture);
            return frmItemFieldList;
        }

        [WebMethod]
        public List<AttributeFormInfo> GetItemFormAttributesValuesByItemID(int itemID, int attributeSetID, int itemTypeID, int storeID, int portalID, string userName, string culture)
        {
            ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
            List<AttributeFormInfo> frmItemAttributes = obj.GetItemAttributesValuesByItemID(itemID, attributeSetID, itemTypeID, storeID, portalID, userName, culture);
            return frmItemAttributes;
        }

        [WebMethod]
        public void SaveItemAndAttributes(int itemID, int itemTypeID, int attributeSetID, int storeID, int portalID, string userName, string culture, int taxRuleID, string categoriesIds, string relatedItemsIds, string upSellItemsIds, string crossSellItemsIds, string downloadItemsValue, string sourceFileCol, string dataCollection, AspxNameValue[] formVars)
        {
            try
            {
                string uplodedDownlodableFormValue = string.Empty;

                if (itemTypeID == 2 && downloadItemsValue != "")
                {
                    FileHelperController downLoadableObj = new FileHelperController();
                    string tempFolder = @"Upload\temp";
                    uplodedDownlodableFormValue = downLoadableObj.MoveFileToDownlodableItemFolder(tempFolder,
                                                                                                  downloadItemsValue,
                                                                                                  @"Modules/AspxCommerce/AspxItemsManagement/DownloadableItems/",
                                                                                                  itemID, "item_");
                }

                ItemsController itemController = new ItemsController();
                itemID = itemController.SaveUpdateItemAndAttributes(itemID, itemTypeID, attributeSetID, storeID, portalID,
                                                                    userName, culture, taxRuleID,
                                                                    categoriesIds, relatedItemsIds, upSellItemsIds,
                                                                    crossSellItemsIds, uplodedDownlodableFormValue,
                                                                    formVars);
                //return "({\"returnStatus\":1,\"Message\":'Item saved successfully.'})";
                if (itemID > 0 && sourceFileCol != "" && dataCollection != "")
                {
                    StoreSettingConfig ssc = new StoreSettingConfig();
                    int itemLargeThumbNailSize = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageSize, storeID,
                                                                                           portalID, culture));
                    int itemMediumThumbNailSize = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageSize,
                                                                                            storeID, portalID, culture));
                    int itemSmallThumbNailSize = Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageSize, storeID,
                                                                                           portalID, culture));
            
                    dataCollection = dataCollection.Replace("../", "");
                    SaveImageContents(itemID, @"Modules/AspxCommerce/AspxItemsManagement/uploads/", sourceFileCol,
                                      dataCollection, itemLargeThumbNailSize, itemMediumThumbNailSize,
                                      itemSmallThumbNailSize, "item_");
                }
                else if (itemID > 0 && sourceFileCol == "" && dataCollection == "")
                {
                    DeleteImageContents(itemID);
                }

                //if (itemID==0)
                //{
                //    //SaveImageContents(itemID, @"Modules/AspxCommerce/AspxItemsManagement/uploads/", sourceFileCol, dataCollection, "item_");
                //    //TODO:: DELTE UPLOADED FILE FROM DOWNLOAD FOLDER

                //}
            }
            catch (Exception ex)
            {
                throw ex;
                //ErrorHandler errHandler = new ErrorHandler();
                //if (errHandler.LogWCFException(ex))
                //{
                //    return "({\"returnStatus\":-1,\"errorMessage\":'" + ex.Message + "'})";
                //}
                //else
                //{
                //    return "({\"returnStatus\":-1,\"errorMessage\":'Error while saving item!'})";
                //}
            }
        }

        [WebMethod]
        public void UpdateItemIsActiveByItemID(int itemId, int storeId, int portalId, string userName, bool isActive)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                obj.UpdateItemIsActive(itemId, storeId, portalId, userName, isActive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool CheckUniqueAttributeName(string attributeName, int attributeId, int storeId, int portalId, string cultureName)
        {
            try
            {
                AttributeSqlProvider obj = new AttributeSqlProvider();
                return obj.CheckUniqueName(attributeName, attributeId, storeId, portalId, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<CategoryInfo> GetCategoryList(string prefix, bool isActive, string cultureName, Int32 storeID, Int32 portalID, string userName, int itemId)
        {
            ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
            List<CategoryInfo> catList = obj.GetCategoryList(prefix, isActive, cultureName, storeID, portalID, userName, itemId);
            return catList;
        }

        [WebMethod]
        public bool CheckUniqueItemSKUCode(string SKU, int itemId, int storeId, int portalId, string cultureName)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                return obj.CheckUniqueSKUCode(SKU, itemId, storeId, portalId, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Multiple Image Uploader
        [WebMethod]
        public string SaveImageContents(Int32 itemID, string imageRootPath, string sourceFileCol, string dataCollection, int itemLargeThumbNailSize, int itemMediumThumbNailSize, int itemSmallThumbNailSize, string imgPreFix)
        {

            if (dataCollection.Contains("#"))
            {
                dataCollection = dataCollection.Remove(dataCollection.LastIndexOf("#"));
            }
            SQLHandler sageSql = new SQLHandler();
            string[] individualRow = dataCollection.Split('#');
            string[] words;

            StringBuilder sbPathList = new StringBuilder();
            StringBuilder sbIsActiveList = new StringBuilder();
            StringBuilder sbImageType = new StringBuilder();
            StringBuilder sbDescription = new StringBuilder();
            StringBuilder sbDisplayOrder = new StringBuilder();
            StringBuilder sbSourcePathList = new StringBuilder();

            foreach (string str in individualRow)
            {
                words = str.Split('%');
                sbPathList.Append(words[0] + "%");
                sbIsActiveList.Append(words[1] + "%");
                sbImageType.Append(words[2] + "%");
                sbDescription.Append(words[3] + "%");
                sbDisplayOrder.Append(words[4] + "%");
            }
            string pathList = string.Empty;
            string isActive = string.Empty;
            string imageType = string.Empty;
            string description = string.Empty;
            string displayOrder = string.Empty;

            pathList = sbPathList.ToString();
            isActive = sbIsActiveList.ToString();
            imageType = sbImageType.ToString();
            description = sbDescription.ToString();
            displayOrder = sbDisplayOrder.ToString();

            if (pathList.Contains("%"))
            {
                pathList = pathList.Remove(pathList.LastIndexOf("%"));
            }
            if (isActive.Contains("%"))
            {
                isActive = isActive.Remove(isActive.LastIndexOf("%"));
            }
            if (imageType.Contains("%"))
            {
                imageType = imageType.Remove(imageType.LastIndexOf("%"));
            }

            if (sourceFileCol.Contains("%"))
            {
                sourceFileCol = sourceFileCol.Remove(sourceFileCol.LastIndexOf("%"));
            }

            ImageUploaderSqlhandler imageManager = new ImageUploaderSqlhandler();

            try
            {
                FileHelperController fhc = new FileHelperController();
                //TODO:: delete all previous files infos lists
                fhc.FileMover(itemID, imageRootPath, sourceFileCol, pathList, isActive, imageType, description, displayOrder, imgPreFix, itemLargeThumbNailSize, itemMediumThumbNailSize, itemSmallThumbNailSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "Success";

        }

        [WebMethod]
        public List<ItemsInfoSettings> GetImageContents(int itemID)
        {
            List<ItemsInfoSettings> itemsImages;
            ImageGallerySqlProvider imageGalleryManager = new ImageGallerySqlProvider();
            itemsImages = imageGalleryManager.GetItemsImageGalleryInfoByItemID(itemID);
            return itemsImages;
        }

        [WebMethod]
        public void DeleteImageContents(Int32 itemID)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                obj.DeleteItemImageByItemID(itemID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Related, Cross Sell, Up sell Items
        [WebMethod]
        public List<ItemsInfo> GetRelatedItemsList(int offset, int limit, int storeId, int portalId, int selfItemId, string userName, string culture)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                return obj.GetRelatedItemsByItemID(offset, limit, storeId, portalId, selfItemId, userName, culture);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ItemsInfo> GetUpSellItemsList(int offset, int limit, int storeId, int portalId, int selfItemId, string userName, string culture)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                return obj.GetUpSellItemsByItemID(offset, limit, storeId, portalId, selfItemId, userName, culture);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ItemsInfo> GetCrossSellItemsList(int offset, int limit, int storeId, int portalId, int selfItemId, string userName, string culture)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                return obj.GetCrossSellItemsByItemID(offset, limit, storeId, portalId, selfItemId, userName, culture);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Item Cost Variants Management
        [WebMethod]
        public List<CostVariantInfo> GetCostVariantsOptionsList(int itemId, int storeId, int portalId, string cultureName)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                return obj.GetAllCostVariantOptions(itemId, storeId, portalId, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------bind Item Cost Variants in Grid--------------------------    
        [WebMethod]
        public List<ItemCostVariantInfo> GetItemCostVariants(int offset, int limit, int storeID, int portalID, string cultureName, int itemID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                SQLHandler sqlH = new SQLHandler();
                List<ItemCostVariantInfo> bind = sqlH.ExecuteAsList<ItemCostVariantInfo>("usp_Aspx_BindItemCostVariantsInGrid", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------ delete Item Cost Variants management------------------------    
        [WebMethod]
        public void DeleteSingleItemCostVariant(string itemCostVariantID, int itemId, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemCostVariantsID", itemCostVariantID));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteSingleItemCostVariants", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------ add Item Cost Variants ------------------------    
        [WebMethod]
        public void AddItemCostVariant(int itemId, int costVariantID, int storeId, int portalId, string cultureName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameter.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_InsertItemCostVariant", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------- bind (edit) item cost Variant details --------------------    
        [WebMethod]
        public List<CostVariantsGetByCostVariantIDInfo> GetItemCostVariantInfoByCostVariantID(int itemCostVariantId, int itemId, int costVariantID, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<CostVariantsGetByCostVariantIDInfo> bind = new List<CostVariantsGetByCostVariantIDInfo>();
                SQLHandler Sq = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemCostVariantsID", itemCostVariantId));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                return Sq.ExecuteAsList<CostVariantsGetByCostVariantIDInfo>("usp_Aspx_ItemCostVariantsGetByCostVariantID", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------- bind (edit) item cost Variant values for cost variant ID --------------------    
        [WebMethod]
        public List<CostVariantsvalueInfo> GetItemCostVariantValuesByCostVariantID(int itemCostVariantId, int itemId, int costVariantID, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<CostVariantsGetByCostVariantIDInfo> bind = new List<CostVariantsGetByCostVariantIDInfo>();
                SQLHandler Sq = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemCostVariantsID", itemCostVariantId));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                return Sq.ExecuteAsList<CostVariantsvalueInfo>("usp_Aspx_GetItemCostVariantValuesByCostVariantID", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------ delete costvariant value ------------------------    
        [WebMethod]
        public void DeleteCostVariantValue(int costVariantValueID, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                if (costVariantValueID > 0)
                {
                    List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                    parameter.Add(new KeyValuePair<string, object>("@CostVariantValueID", costVariantValueID));
                    parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                    parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                    parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                    parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                    SQLHandler sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery("usp_Aspx_DeleteCostVariantValue", parameter);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------Save and update Item Costvariant options-------------------------    
        [WebMethod]
        public void SaveAndUpdateItemCostVariant(int costVariantID, string costVariantName, string description, string cultureName, int itemId, int inputTypeID,
                                                 int displayOrder, bool showInGrid, bool showInSearch, bool showInAdvanceSearch, bool showInComparison, bool isEnableSorting, bool isUseInFilter,
                                                 bool isIncludeInPriceRule, bool isIncludeInPromotions, bool isShownInRating, int storeId, int portalId,
                                                 bool isActive, bool isModified, string userName, string variantOptions, bool isNewflag)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantName", costVariantName));
                parameterCollection.Add(new KeyValuePair<string, object>("@Description", description));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", inputTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShowInGrid", showInGrid));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShowInSearch", showInSearch));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShowInAdvanceSearch", showInAdvanceSearch));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShowInComparison", showInComparison));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsEnableSorting", isEnableSorting));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsUseInFilter", isUseInFilter));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule", isIncludeInPriceRule));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPromotions", isIncludeInPromotions));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsShownInRating", isShownInRating));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", isModified));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@VariantOption", variantOptions));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsNewFlag", isNewflag));
                parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantsValueID", 0));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemCostVariantsID", 0));
                SQLHandler Sq = new SQLHandler();
                Sq.ExecuteNonQuery("usp_Aspx_SaveAndUpdateItemCostVariants", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //------------------------ delete item costvariant value ------------------------    
        [WebMethod]
        public void DeleteItemCostVariantValue(int costVariantValueID, int itemId, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                if (costVariantValueID > 0)
                {
                    List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                    parameter.Add(new KeyValuePair<string, object>("@CostVariantValueID", costVariantValueID));
                    parameter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                    parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                    parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                    parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                    parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                    SQLHandler sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery("usp_Aspx_DeleteItemCostVariantValue", parameter);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Item Tax
        [WebMethod]
        public List<TaxRulesInfo> GetAllTaxRules(int storeID, int portalID, bool isActive)
        {
            ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
            List<TaxRulesInfo> lstTaxManageRule = obj.GetAllTaxRules(storeID, portalID, isActive);
            return lstTaxManageRule;
        }
        #endregion

        #region Downloadable Item Details
        [WebMethod]
        public List<DownLoadableItemInfo> GetDownloadableItem(int storeId, int portalId, string userName, string cultureName, int itemID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@itemID", itemID));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<DownLoadableItemInfo>("usp_Aspx_GetDownloadableItem", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region Front Image Gallery
        [WebMethod]
        public List<ItemsInfoSettings> GetItemsImageGalleryInfoBySKU(string itemSKU, int storeID, int portalID, string costVariantIDs)
        {
            List<ItemsInfoSettings> itemsInfo;
            ImageGallerySqlProvider imageSqlProvider = new ImageGallerySqlProvider();
            itemsInfo = imageSqlProvider.GetItemsImageGalleryInfoByItemSKU(itemSKU,storeID,portalID,costVariantIDs);
            return itemsInfo;
        }

        [WebMethod]
        public List<ImageGalleryItemsInfo> GetItemsImageGalleryInfo(Int32 storeID, Int32 portalID, string userName, string culture)
        {
            List<ImageGalleryItemsInfo> itemsInfo;
            ImageGallerySqlProvider imageSettingsProvider = new ImageGallerySqlProvider();
            itemsInfo = imageSettingsProvider.GetItemsImageGalleryList(storeID, portalID, userName, culture);
            return itemsInfo;
        }

        [WebMethod]
        public List<ImageGalleryItemsInfo> GetItemsGalleryInfo(Int32 storeID, Int32 portalID, string culture)
        {
            List<ImageGalleryItemsInfo> itemsInfo;
            ImageGallerySqlProvider imageSettingsProvider = new ImageGallerySqlProvider();
            itemsInfo = imageSettingsProvider.GetItemInfoList(storeID, portalID, culture);
            return itemsInfo;
        }

        [WebMethod]
        public ImageGalleryInfo ReturnSettings(Int32 userModuleID, Int32 portalID, string culture)
        {
            ImageGalleryInfo gallerySettingsInfo;
            ImageGallerySqlProvider settings = new ImageGallerySqlProvider();
            gallerySettingsInfo = settings.GetGallerySettingValues(userModuleID, portalID, culture);
            return gallerySettingsInfo;
        }

        [WebMethod]
        public List<int> ReturnDimension(Int32 userModuleID, Int32 portalID, string culture)
        {
            List<int> param = new List<int>();
            ImageGalleryInfo info = new ImageGalleryInfo();
            ImageGallerySqlProvider settings = new ImageGallerySqlProvider();

            info = settings.GetGallerySettingValues(userModuleID, portalID, culture);
            param.Add(int.Parse(info.ImageWidth));
            param.Add(int.Parse(info.ImageHeight));
            param.Add(int.Parse(info.ThumbWidth));
            param.Add(int.Parse(info.ThumbHeight));
            //param.Add(int.Parse(info.ZoomShown));
            return param;
        }
        #endregion

        #region Category Management
        [WebMethod]
        public bool CheckUniqueCategoryName(string catName, int catId, int storeId, int portalId, string cultureName)
        {
            try
            {
                CategorySqlProvider obj = new CategorySqlProvider();
                return obj.CheckUniqueName(catName, catId, storeId, portalId, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool IsUnique(Int32 storeID, Int32 portalID, Int32 itemID, Int32 attributeID, Int32 attributeType, string attributeValue)
        {
            try
            {
                AttributeSqlProvider attributeSqlProvider = new AttributeSqlProvider();
                /*
            1	TextField
            2	TextArea
            3	Date
            4	Boolean
            5	MultipleSelect
            6	DropDown
            7	Price
            8	File
            9	Radio
            10	RadioButtonList
            11	CheckBox
            12	CheckBoxList
             */
                bool isUnique = false;
                switch (attributeType)
                {
                    case 1:
                        isUnique = attributeSqlProvider.CheckUniquenessNvarchar(1, storeID, portalID, attributeID, attributeValue);
                        break;
                    case 2:
                        isUnique = attributeSqlProvider.CheckUniquenessText(1, storeID, portalID, attributeID, attributeValue);
                        break;
                    case 3:
                        isUnique = attributeSqlProvider.CheckUniquenessDate(1, storeID, portalID, attributeID, DateTime.Parse(attributeValue));
                        break;
                    case 4:
                        isUnique = attributeSqlProvider.CheckUniquenessBoolean(1, storeID, portalID, attributeID, bool.Parse(attributeValue));
                        break;
                    case 5:
                        isUnique = attributeSqlProvider.CheckUniquenessInt(1, storeID, portalID, attributeID, Int32.Parse(attributeValue));
                        break;
                    case 6:
                        isUnique = attributeSqlProvider.CheckUniquenessInt(1, storeID, portalID, attributeID, Int32.Parse(attributeValue));
                        break;
                    case 7:
                        isUnique = attributeSqlProvider.CheckUniquenessDecimal(1, storeID, portalID, attributeID, decimal.Parse(attributeValue));
                        break;
                    case 8:
                        isUnique = attributeSqlProvider.CheckUniquenessFile(1, storeID, portalID, attributeID, attributeValue);
                        break;
                    case 9:
                        isUnique = attributeSqlProvider.CheckUniquenessInt(1, storeID, portalID, attributeID, Int32.Parse(attributeValue));
                        break;
                    case 10:
                        isUnique = attributeSqlProvider.CheckUniquenessInt(1, storeID, portalID, attributeID, Int32.Parse(attributeValue));
                        break;
                    case 11:
                        isUnique = attributeSqlProvider.CheckUniquenessInt(1, storeID, portalID, attributeID, Int32.Parse(attributeValue));
                        break;
                    case 12:
                        isUnique = attributeSqlProvider.CheckUniquenessInt(1, storeID, portalID, attributeID, Int32.Parse(attributeValue));
                        break;
                }
                return isUnique;
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                errHandler.LogWCFException(ex);
                return false;
            }
        }

        [WebMethod]
        public List<AttributeFormInfo> GetCategoryFormAttributes(Int32 categoryID, Int32 portalID, Int32 storeID, string userName, string culture)
        {
            try
            {
                CategorySqlProvider categorySqlProvider = new CategorySqlProvider();
                List<AttributeFormInfo> frmFieldList = categorySqlProvider.GetCategoryFormAttributes(categoryID, portalID, storeID, userName, culture);
                return frmFieldList;
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                errHandler.LogWCFException(ex);
                throw ex;
            }
        }

        [WebMethod]
        public List<CategoryInfo> GetCategoryAll(bool isActive, Int32 storeID, Int32 portalID, string userName, string culture)
        {
            try
            {
                CategorySqlProvider categorySqlProvider = new CategorySqlProvider();
                List<CategoryInfo> catList = categorySqlProvider.GetCategoryAll(isActive, storeID, portalID, userName, culture);
                return catList;
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                errHandler.LogWCFException(ex);
                throw ex;
            }
        }

        [WebMethod]
        public List<CategoryAttributeInfo> GetCategoryByCategoryID(Int32 categoryID, Int32 storeID, Int32 portalID, string userName, string culture)
        {
            CategorySqlProvider categorySqlProvider = new CategorySqlProvider();
            List<CategoryAttributeInfo> catList = categorySqlProvider.GetCategoryByCategoryID(categoryID, storeID, portalID, userName, culture);
            return catList;
        }

        [WebMethod]
        public string SaveCategory(Int32 storeID, Int32 portalID, Int32 categoryID, Int32 parentID, AspxNameValue[] formVars, string selectedItems, string userName, string culture, int categoryLargeThumbImage, int categoryMediumThumbImage, int categorySmallThumbImage)
        {
            try
            {
                CategoryController categoryController = new CategoryController();
                categoryController.SaveCategory(storeID, portalID, categoryID, parentID, formVars, selectedItems, userName, culture, categoryLargeThumbImage, categoryMediumThumbImage, categorySmallThumbImage);
                if (parentID > 0)
                {
                    return "({\"returnStatus\":1,\"Message\":\"Sub category has been saved successfully.\"})";
                }
                else
                {
                    return "({\"returnStatus\":1,\"Message\":\"Category has been saved successfully.\"})";
                }
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({\"returnStatus\":-1,\"errorMessage\":'" + ex.Message + "'})";
                }
                else
                {
                    return "({\"returnStatus\":-1,\"errorMessage\":\"Error while saving category!\"})";
                }
            }
        }

        [WebMethod]
        public string DeleteCategory(Int32 storeID, Int32 portalID, Int32 categoryID, string userName, string culture)
        {
            try
            {
                CategorySqlProvider categorySqlProvider = new CategorySqlProvider();
                categorySqlProvider.DeleteCategory(storeID, portalID, categoryID, userName, culture);
                return "({ \"returnStatus\" : 1 , \"Message\" : \"Category has been deleted successfully.\" })";
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting category!\" })";
                }
            }
        }

        [WebMethod]
        public List<CategoryItemInfo> GetCategoryItems(Int32 offset, Int32 limit, Int32 categoryID, string sku, string name, System.Nullable<decimal> priceFrom, System.Nullable<decimal> priceTo, Int32 storeID, Int32 portalID, string userName, string culture)
        {
            try
            {
                List<CategoryItemInfo> listCategoryItem;
                CategorySqlProvider categorySqlProvider = new CategorySqlProvider();
                listCategoryItem = categorySqlProvider.GetCategoryItems(offset, limit, categoryID, sku, name, priceFrom, priceTo, storeID, portalID, userName, culture);
                return listCategoryItem;
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                errHandler.LogWCFException(ex);
                throw ex;
            }
        }

        [WebMethod]
        public string SaveChangesCategoryTree(Int32 storeID, Int32 portalID, string categoryIDs, string userName)
        {
            try
            {
                CategorySqlProvider categorySqlProvider = new CategorySqlProvider();
                categorySqlProvider.SaveChangesCategoryTree(storeID, portalID, categoryIDs, userName);
                return "({ \"returnStatus\" : 1 , \"Message\" : \"Category tree saved successfully.\" })";
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while saving category tree!\" })";
                }
            }
        }
        #endregion

        ////---------------- File Uploader --------------
        //    [WebMethod]
        //    public string SaveUploadFiles(string fileList)
        //    {
        //        try
        //        {
        //            string fileName = string.Empty;
        //            //HttpPostedFile ss; 
        //            //string strFileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
        //            //string strExtension = Path.GetExtension(HttpContext.Current.Request.Files[0].FileName).ToLower();
        //            //string strSaveLocation = HttpContext.Current.Server.MapPath("Upload") + "" + strFileName;
        //            //HttpContext.Current.Request.Files[0].SaveAs(strSaveLocation);

        //            ////contentType: "application/json; charset=utf-8",
        //            //// contentType: "multipart/form-data"
        //            ////contentType: "text/html; charset=utf-8"
        //            //HttpContext.Current.Response.ContentType = "text/plain; charset=utf-8";
        //            //HttpContext.Current.Response.Write(strSaveLocation);
        //            //HttpContext.Current.Response.End();

        //            if (HttpContext.Current.Request.Files != null)
        //            {
        //                HttpFileCollection files = HttpContext.Current.Request.Files;
        //                for (int i = 0; i < files.Count; i++)
        //                {
        //                    HttpPostedFile file = files[i];
        //                    if (file.ContentLength > 0)
        //                    {
        //                        fileName = file.FileName;
        //                    }
        //                }
        //            }
        //            ////Code ommited
        //            //string jsonClient = null;
        //            //var j = new { fileName = response.key1 };
        //            //var s = new JavaScriptSerializer();
        //            //jsonClient = s.Serialize(j);
        //            //return jsonClient;

        //            return fileName;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }

        //--------------------CategoryItems------------------------------
        //[WebMethod]
        //public List<ItemsGetCategoryIDInfo> BindCategoryItems(int categoryID, int storeID, int portalID, string userName, string cultureName)
        //{
        //    try
        //    {
        //        List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
        //        parameter.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
        //        parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        //        parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        //        parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
        //        parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        //        SQLHandler sqlH = new SQLHandler();
        //        List<ItemsGetCategoryIDInfo> Bind = sqlH.ExecuteAsList<ItemsGetCategoryIDInfo>("usp_Aspx_ItemsGetAllBycategoryID", parameter);
        //        return Bind;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //----------------General Search View As DropDown Options----------------------------
        #region Featured Items Management
        [WebMethod]
        public List<FeaturedItemsInfo> GetFeaturedItemsList(int storeId, int portalId, string userName, string cultureName, int count)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                return obj.GetFeaturedItemsByCount(storeId, portalId, userName, cultureName, count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Recently Added/ Latest Items Management
        [WebMethod]
        public List<LatestItemsInfo> GetLatestItemsList(int storeId, int portalId, string userName, string cultureName, int count)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                return obj.GetLatestItemsByCount(storeId, portalId, userName, cultureName, count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CompareItems
        [WebMethod]
        public void SaveCompareItems(int ID, int storeID, int portalID, string userName, string IP, string countryName, string sessionCode)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", ID));
                parameter.Add(new KeyValuePair<string, object>("@CompareItemID", 0));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@IP", IP));
                parameter.Add(new KeyValuePair<string, object>("@CountryName", countryName));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddItemsToCompare", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ItemsCompareInfo> GetItemCompareList(int storeID, int portalID, string userName, string cultureName, string sessionCode)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemsCompareInfo>("usp_Aspx_GetCompareItemsList", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteCompareItem(int ID, int storeID, int portalID, string userName, string sessionCode)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", ID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[usp_Aspx_DeleteCompareItem]", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void ClearAll(int storeID, int portalID, string userName, string sessionCode)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[usp_Aspx_ClearCompareItems]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool CheckCompareItems(int ID, int storeID, int portalID, string userName, string sessionCode)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", ID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsGivenType<bool>("[usp_Aspx_CheckCompareItems]", parameter, "@IsExist");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Compare Items Details view-------------------------------
        [WebMethod]
        public List<ItemBasicDetailsInfo> GetCompareListImage(string itemIDs, int storeID, int portalID, string userName, string cultureName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ItemIDs", itemIDs));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetCompareList", parameter);
        }

        [WebMethod]
        public List<CompareItemListInfo> GetCompareList(string itemIDs, int storeID, int portalID, string userName, string cultureName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ItemIDs", itemIDs));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CompareItemListInfo>("usp_Aspx_GetItemCompareList", parameter);
        }

        #region RecentlyComparedProducts
        [WebMethod]
        public void AddComparedItems(string IDs, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemIDs", IDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddComparedItems", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ItemsCompareInfo> GetRecentlyComparedItemList(int count, int storeID, int portalID, string cultureName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@Count", count));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemsCompareInfo>("usp_Aspx_GetRecentlyComparedItemList", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region WishItems
        [WebMethod]
        public bool CheckWishItems(int ID, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", ID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsGivenType<bool>("[usp_Aspx_CheckWishItems]", parameter, "@IsExist");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void SaveWishItems(int ID, int storeID, int portalID, string userName, string IP, string countryName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ItemID", ID));
            parameter.Add(new KeyValuePair<string, object>("@WishItemID", 0));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@IP", IP));
            parameter.Add(new KeyValuePair<string, object>("@CountryName", countryName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_SaveWishItems", parameter);
        }

        [WebMethod]
        public List<WishItemsInfo> GetWishItemList(int storeID, int portalID, string userName, string cultureName, string flagShowAll, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@flag", flagShowAll));
                parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<WishItemsInfo>("usp_Aspx_GetWishItemList", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteWishItem(int ID, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("ItemID", ID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteWishItem", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void UpdateWishList(string ID, string comment, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("ItemID", ID));
                parameter.Add(new KeyValuePair<string, object>("@Comment", comment));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateWishItem", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void ClearWishList(int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_ClearWishItem", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------Save AND SendEmail Messages For ShareWishList----------------
        [WebMethod]
        public void ShareWishListEmailSend(int storeID, int portalID, string itemID, string senderName, string senderEmail, string receiverEmailID, string subject, string message, string link, string cultureName)
        {
            try
            {
                string bodyDetail = link;
                ReferToFriendSqlHandler obj = new ReferToFriendSqlHandler();
                obj.SaveShareWishListEmailMessage(storeID, portalID, itemID, senderName, senderEmail, receiverEmailID, subject, message, cultureName);
                obj.SendShareWishItemEmail(storeID, portalID, senderName, senderEmail, receiverEmailID, subject, message, bodyDetail, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public int CountWishItems(int storeID, int portalID, string sessionCode, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsScalar<int>("usp_Aspx_GetWishItemsCount", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Related Items You may also like
        [WebMethod]
        public List<ItemBasicDetailsInfo> GetYouMayAlsoLikeItemsListByItemSKU(string itemSKU, int storeID, int portalID, string userName, string cultureName, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetYouMayAlsoLikeItemsByItemSKU", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RecentlyViewedItems
        [WebMethod]
        public List<RecentlyViewedItemsInfo> GetRecentlyViewedItems(int count, int storeID, int portalID, string cultureName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@Count", count));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                List<RecentlyViewedItemsInfo> view = sqlH.ExecuteAsList<RecentlyViewedItemsInfo>("usp_Aspx_GetRecentlyViewedItemList", parameter);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void AddUpdateRecentlyViewedItems(string itemSKU, string sessionCode, string IP, string countryName, string userName, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                parameter.Add(new KeyValuePair<string, object>("@ViewFromIP", IP));
                parameter.Add(new KeyValuePair<string, object>("@ViewedFromCountry", countryName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddRecentlyViewedItems", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Item Details Module
        [WebMethod]
        public ItemBasicDetailsInfo GetItemBasicInfoByitemSKU(string itemSKU, int storeID, int portalID, string userName, string culture)
        {
            ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
            ItemBasicDetailsInfo frmItemAttributes = obj.GetItemBasicInfo(itemSKU, storeID, portalID, userName, culture);
            return frmItemAttributes;
        }

        [WebMethod]
        public List<ItemCostVariantsInfo> GetCostVariantsByitemSKU(string itemSKU, int storeID, int portalID, string cultureName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemCostVariantsInfo>("usp_Aspx_GetCostVariantsByItemID", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AttributeFormInfo> GetItemDetailsByitemSKU(string itemSKU, int attributeSetID, int itemTypeID, int storeID, int portalID, string userName, string culture)
        {
            ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
            List<AttributeFormInfo> frmItemAttributes = obj.GetItemDetailsInfoByItemSKU(itemSKU, attributeSetID, itemTypeID, storeID, portalID, userName, culture);
            return frmItemAttributes;
        }

        #endregion

        #region PopularTags Module
        [WebMethod]
        public void AddTagsOfItem(string itemSKU, string tags, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@Tags", tags));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddTagsOfItem", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ItemTagsInfo> GetItemTags(string itemSKU, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemTagsInfo>("[usp_Aspx_GetTagsByItemID]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteUserOwnTag(string itemTagID, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemTagID", itemTagID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteUserOwnTag", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public void DeleteMultipleTag(string itemTagIDs, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@TagsIDS", itemTagIDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleTags", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public List<TagDetailsInfo> GetTagDetailsListPending(int offset, int limit, string tag, int storeId, int portalId, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Tags", tag));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                SQLHandler sqlH = new SQLHandler();
                List<TagDetailsInfo> nir = sqlH.ExecuteAsList<TagDetailsInfo>("[dbo].[usp_Aspx_GetAllTagsPending]", parameter);
                return nir;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<TagDetailsInfo> GetTagDetailsList(int offset, int limit, string tag, string tagStatus, int storeId, int portalId, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Tags", tag));
                parameter.Add(new KeyValuePair<string, object>("@TagStatus", tagStatus));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                SQLHandler sqlH = new SQLHandler();
                List<TagDetailsInfo> nir = sqlH.ExecuteAsList<TagDetailsInfo>("usp_Aspx_GetAllTags", parameter);
                return nir;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<TagDetailsInfo> GetAllPopularTags(int storeID, int portalID, string userName, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@Count", count));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<TagDetailsInfo>("usp_Aspx_GetPopularTags", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<TagDetailsInfo> GetTagsByUserName(string userName, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@Username", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<TagDetailsInfo>("usp_Aspx_GetTagsOfUser", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Tags Reports
        //---------------------Customer tags------------
        [WebMethod]
        public List<CustomerTagInfo> GetCustomerTagDetailsList(int offset, System.Nullable<int> limit, int storeId, int portalId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                SQLHandler sqlH = new SQLHandler();
                List<CustomerTagInfo> bhu = sqlH.ExecuteAsList<CustomerTagInfo>("usp_Aspx_GetCustomerItemTags", parameter);
                return bhu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Show Customer tags list------------
        [WebMethod]
        public List<ShowCustomerTagsListInfo> ShowCustomerTagList(int offset, System.Nullable<int> limit, int storeId, int portalId, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                List<ShowCustomerTagsListInfo> bhu = sqlH.ExecuteAsList<ShowCustomerTagsListInfo>("usp_Aspx_ShowCustomerTagList", parameter);
                return bhu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Item tags details------------
        [WebMethod]
        public List<ItemTagsDetailsInfo> GetItemTagDetailsList(int offset, System.Nullable<int> limit, int storeId, int portalId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                SQLHandler sqlH = new SQLHandler();
                List<ItemTagsDetailsInfo> bhu = sqlH.ExecuteAsList<ItemTagsDetailsInfo>("usp_Aspx_GetItemTagsDetails", parameter);
                return bhu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Show Item tags list------------
        [WebMethod]
        public List<ShowItemTagsListInfo> ShowItemTagList(int offset, System.Nullable<int> limit, int storeId, int portalId, int itemID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                SQLHandler sqlH = new SQLHandler();
                List<ShowItemTagsListInfo> bhu = sqlH.ExecuteAsList<ShowItemTagsListInfo>("usp_Aspx_ShowTagsByItems", parameter);
                return bhu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Popular tags details------------
        [WebMethod]
        public List<PopularTagsInfo> GetPopularTagDetailsList(int offset, System.Nullable<int> limit, int storeId, int portalId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                SQLHandler sqlH = new SQLHandler();
                List<PopularTagsInfo> bhu = sqlH.ExecuteAsList<PopularTagsInfo>("usp_Aspx_GetPopularityTags", parameter);
                return bhu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Show Popular tags list------------
        [WebMethod]
        public List<ShowpopulartagsDetailsInfo> ShowPopularTagList(int offset, System.Nullable<int> limit, int storeId, int portalId, string tagName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@TagName", tagName));
                SQLHandler sqlH = new SQLHandler();
                List<ShowpopulartagsDetailsInfo> bhu = sqlH.ExecuteAsList<ShowpopulartagsDetailsInfo>("usp_Aspx_ShowPopularTagsDetails", parameter);
                return bhu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ItemBasicDetailsInfo> GetUserTaggedItems(int offset,int limit,string tagIDs, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@TagIDs", tagIDs));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetItemsByTagID", parameterCollection);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Tags Management
        [WebMethod]
        public void UpdateTag(string itemTagIDs, string newTag, int statusID, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemTagIDs", itemTagIDs));
                parameter.Add(new KeyValuePair<string, object>("@NewTag", newTag));
                parameter.Add(new KeyValuePair<string, object>("@StatusID", statusID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateTag", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteTag(string itemTagIDs, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemTagIDs", itemTagIDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteTag", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ItemBasicDetailsInfo> GetItemsByMultipleItemID(string itemIDs, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemIDs", itemIDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetItemsByMultipleItemID", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region ShoppingOptions
        [WebMethod]
        public List<DisplayItemsOptionsInfo> BindItemsViewAsList()
        {
            try
            {

                SQLHandler sqlH = new SQLHandler();
                List<DisplayItemsOptionsInfo> bind = sqlH.ExecuteAsList<DisplayItemsOptionsInfo>("usp_Aspx_DisplayItemViewAsOptions");
                return bind;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------ShoppingOptionsByPrice----------------------------
        [WebMethod]
        public List<ShoppingOptionsInfo> ShoppingOptionsByPrice(int storeID, int portalID, string userName, string cultureName, int upperLimit)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@Limit", upperLimit));
                SQLHandler sqlH = new SQLHandler();
                List<ShoppingOptionsInfo> count = sqlH.ExecuteAsList<ShoppingOptionsInfo>("usp_Aspx_ShoppingOptions", parameter);
                return count;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------ShoppingOptionsByPriceResults----------------------------
        [WebMethod]
        public List<ItemBasicDetailsInfo> GetShoppingOptionsItemsResult(int offset,int limit,string itemIds, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ItemIDs", itemIds));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetShoppingOptionsItemsResult", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Search
        //Auto Complete search Box
        [WebMethod]
        public List<SearchTermList> GetSearchedTermList(string search, int storeID, int portalID)
        {
            List<SearchTermList> srInfo;

            List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paramCol.Add(new KeyValuePair<string, object>("@Search", search));
            SQLHandler sageSQL = new SQLHandler();
            srInfo = sageSQL.ExecuteAsList<SearchTermList>("[dbo].[usp_Aspx_GetListSearched]", paramCol);
            return srInfo;
        }

        #region General Search
        //----------------General Search Sort By DropDown Options----------------------------
        [WebMethod]
        public List<ItemBasicDetailsInfo> GetSimpleSearchResult(int offset,int limit,int categoryID, string searchText, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
                parameter.Add(new KeyValuePair<string, object>("@SearchText", searchText));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetSimpleSearchResult", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<SortOptionTypeInfo> BindItemsSortByList()
        {
            try
            {

                SQLHandler sqlH = new SQLHandler();
                List<SortOptionTypeInfo> bind = sqlH.ExecuteAsList<SortOptionTypeInfo>("usp_Aspx_DisplayItemSortByOptions");
                return bind;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ItemBasicDetailsInfo> GetItemsByGeneralSearch(int storeID, int portalID, string nameSearchText, float priceFrom, float priceTo,
                                                                  string skuSearchText, int categoryID, string categorySearchText, bool isByName, bool isByPrice, bool isBySKU, bool isByCategory, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@NameSearchText", nameSearchText));
                parameter.Add(new KeyValuePair<string, object>("@PriceFrom", priceFrom));
                parameter.Add(new KeyValuePair<string, object>("@PriceTo", priceTo));
                parameter.Add(new KeyValuePair<string, object>("@SKUSearchText", skuSearchText));
                parameter.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
                parameter.Add(new KeyValuePair<string, object>("@CategorySearchText", categorySearchText));
                parameter.Add(new KeyValuePair<string, object>("@IsByName", isByName));
                parameter.Add(new KeyValuePair<string, object>("@IsByPrice", isByPrice));
                parameter.Add(new KeyValuePair<string, object>("@IsBySKU", isBySKU));
                parameter.Add(new KeyValuePair<string, object>("@IsByCategory", isByCategory));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetItemsByGeneralSearch", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<CategoryInfo> GetAllCategoryForSearch(string prefix, bool isActive, string culture, int storeID, int portalID, string userName)
        {
            try
            {
                int itemID = 0;
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@Prefix", prefix));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@Username", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                SQLHandler sqlH = new SQLHandler();
                List<CategoryInfo> catList = sqlH.ExecuteAsList<CategoryInfo>("dbo.usp_Aspx_GetCategoryList", parameterCollection);
                return catList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Advance Search
        [WebMethod]
        public List<ItemTypeInfo> GetItemTypeList()
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemTypeInfo>("usp_Aspx_GetItemTypeList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AttributeFormInfo> GetAttributeByItemType(int itemTypeID, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<AttributeFormInfo>("usp_Aspx_GetAttributeByItemType", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region More Advanced Search
        //------------------get dyanamic Attributes for serach-----------------------   
        [WebMethod]
        public List<AttributeShowInAdvanceSearchInfo> GetAttributes(int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<AttributeShowInAdvanceSearchInfo>("usp_Aspx_GetAttributesShowInAdvanceSearch", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------get items by dyanamic Advance serach-----------------------
        [WebMethod]
        public List<ItemBasicDetailsInfo> GetItemsByDyanamicAdvanceSearch(int offset,int limit,int storeID, int portalID, System.Nullable<int> categoryID, string searchText, string checkValue,
                                                                          System.Nullable<float> priceFrom, System.Nullable<float> priceTo, string userName, string cultureName, string attributeIds)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
                parameter.Add(new KeyValuePair<string, object>("@SearchText", searchText));
                parameter.Add(new KeyValuePair<string, object>("@CheckValues", checkValue));
                parameter.Add(new KeyValuePair<string, object>("@PriceFrom", priceFrom));
                parameter.Add(new KeyValuePair<string, object>("@PriceTo", priceTo));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@AttributeIDs", attributeIds));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetItemsByDynamicAdvanceSearch", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region Category Details
        [WebMethod]
        public List<CategoryDetailsInfo> BindCategoryDetails(int storeID, int portalID, int categoryID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
                parameterCollection.Add(new KeyValuePair<string, object>("@Username", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CategoryDetailsInfo>("usp_Aspx_GetCategoryDetails", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<CategoryDetailsOptionsInfo> GetCategoryDetailsOptions(int offset,int limit ,string categorykey, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@categorykey", categorykey));
                parameterCollection.Add(new KeyValuePair<string, object>("@Username", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@Culture", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CategoryDetailsOptionsInfo>("usp_Aspx_CategoryDetailsOptions", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Rating/Reviews

        #region rating/ review
        //---------------------save rating/ review Items-----------------------
        [WebMethod]
        public List<ItemRatingAverageInfo> GetItemAverageRating(string itemSKU, int storeID, int portalID, string cultureName)
        {
            try
            {
                //ItemRatingAverageInfo
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                List<ItemRatingAverageInfo> avgRating = sqlH.ExecuteAsList<ItemRatingAverageInfo>("usp_Aspx_ItemRatingGetAverage", parameter);
                return avgRating;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------rating/ review Items criteria--------------------------
        [WebMethod]
        public List<RatingCriteriaInfo> GetItemRatingCriteria(int storeID, int portalID, string cultureName, bool isFlag)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@IsFlag", isFlag));
                List<RatingCriteriaInfo> rating = sqlH.ExecuteAsList<RatingCriteriaInfo>("usp_Aspx_GetItemRatingCriteria", parameter);
                return rating;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<RatingCriteriaInfo> GetItemRatingCriteriaByReviewID(int storeID, int portalID, string cultureName, int itemReviewID,bool isFlag)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
                parameter.Add(new KeyValuePair<string, object>("@IsFlag", isFlag));
                List<RatingCriteriaInfo> rating = sqlH.ExecuteAsList<RatingCriteriaInfo>("usp_Aspx_GetItemRatingCriteriaForPending", parameter);
                return rating;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------save rating/ review Items-----------------------
        [WebMethod]
        public void SaveItemRating(string ratingCriteriaValue, int statusID, string summaryReview, string review, string userIP, string viewFromCountry, int itemID, int storeID, int portalID, string nickName, string addedBy)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@RatingCriteriaValue", ratingCriteriaValue));
                parameter.Add(new KeyValuePair<string, object>("@StatusID", statusID));
                parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", 0));
                parameter.Add(new KeyValuePair<string, object>("@ReviewSummary", summaryReview));
                parameter.Add(new KeyValuePair<string, object>("@Review", review));
                parameter.Add(new KeyValuePair<string, object>("@ViewFromIP", userIP));
                parameter.Add(new KeyValuePair<string, object>("@ViewFromCountry", viewFromCountry));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@Username", nickName));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", addedBy));
                sqlH.ExecuteNonQuery("usp_Aspx_SaveItemRating", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------update rating/ review Items-----------------------
        [WebMethod]
        public void UpdateItemRating(string ratingCriteriaValue, int statusID, string summaryReview, string review, int itemReviewID, string viewFromIP, string viewFromCountry, int itemID, int storeID, int portalID, string nickName, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@RatingCriteriaValue", ratingCriteriaValue));
                parameter.Add(new KeyValuePair<string, object>("@StatusID", statusID));
                parameter.Add(new KeyValuePair<string, object>("@ReviewSummary", summaryReview));
                parameter.Add(new KeyValuePair<string, object>("@Review", review));
                parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
                parameter.Add(new KeyValuePair<string, object>("@ViewFromIP", viewFromIP));
                parameter.Add(new KeyValuePair<string, object>("@ViewFromCountry", viewFromCountry));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@Username", nickName));
                parameter.Add(new KeyValuePair<string, object>("@UserBy", userName));
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateItemRating", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Get rating/ review of Item Per User ------------------
        [WebMethod]
        public List<ItemRatingByUserInfo> GetItemRatingPerUser(string itemSKU, int storeID, int portalID, string cultureName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                return sqlH.ExecuteAsList<ItemRatingByUserInfo>("usp_Aspx_GetItemAverageRatingByUser", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Get rating/ review of Item Per User ------------------
        [WebMethod]
        public List<RatingLatestInfo> GetRecentItemReviewsAndRatings(int offset,int limit,int storeID, int portalID, string cultureName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                return sqlH.ExecuteAsList<RatingLatestInfo>("usp_Aspx_GetRecentReviewsAndRatings", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Get rating/ review of Item Per User ------------------
        [WebMethod]
        public List<ItemReviewDetailsInfo> GetItemRatingByReviewID(int itemReviewID, int storeID, int portalID, string cultureName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                return sqlH.ExecuteAsList<ItemReviewDetailsInfo>("usp_Aspx_GetItemReviewDetails", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------Item single rating management------------------------
        [WebMethod]
        public void DeleteSingleItemRating(string itemReviewID, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteSingleItemRatingInformation", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Delete multiple item rating informations--------------------------
        [WebMethod]
        public void DeleteMultipleItemRatings(string itemReviewIDs, int storeId, int portalId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemReviewIDs", itemReviewIDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleSelectionItemRating", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Bind in Item Rating Information in grid-------------------------
        [WebMethod]
        public List<UserRatingInformationInfo> GetAllUserReviewsAndRatings(int offset, int limit, int storeID, int portalID, string userName, string statusName, string itemName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@StatusName", statusName));
                parameter.Add(new KeyValuePair<string, object>("@ItemName", itemName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetAllReviewsAndRatings", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------list item names in dropdownlist/item rating management---------------------
        [WebMethod]
        public List<ItemsReviewInfo> GetAllItemList(int storeID, int portalID, string cultureName)
        {
            try
            {
                //ItemRatingAverageInfo
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                List<ItemsReviewInfo> items = sqlH.ExecuteAsList<ItemsReviewInfo>("usp_Aspx_GetAllItemsListReview", parameter);
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool CheckReviewByUser(int itemID, int storeID, int portalID, string userName)
        {

            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckReviewAlreadyExist", parameter, "@IsReviewAlreadyExist");

        }

        [WebMethod]
        public bool CheckReviewByIP(int itemID, int storeID, int portalID, string userIP)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@UserIP", userIP));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckReviewAlreadyExist", parameter, "@IsReviewAlreadyExist");
        }
   
        #endregion

        #region Item Rating Criteria Manage/Admin
        //--------------------Item Rating Criteria Manage/Admin--------------------------
        [WebMethod]
        public List<ItemRatingCriteriaInfo> ItemRatingCriteriaManage(int offset, int limit, string ratingCriteria, System.Nullable<bool> isActive, int storeId, int portalId, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@RatingCriteria", ratingCriteria));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemRatingCriteriaInfo>("usp_Aspx_GetAllItemRatingCriteria", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------- ItemRating Criteria Manage-------------------------------
        [WebMethod]
        public void AddUpdateItemCriteria(int ID, string criteria, string isActive, int storeID, int portalID, string cultureName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ID", ID));
                parameter.Add(new KeyValuePair<string, object>("@Criteria", criteria));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateItemRatingCriteria", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------- ItemRating Criteria Manage-------------------------------
        [WebMethod]
        public void DeleteItemRatingCriteria(string IDs, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CriteriaID", IDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteItemRatingCriteria", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Cost Variants Management
        //--------------------bind Cost Variants in Grid--------------------------
        [WebMethod]
        public List<CostVariantInfo> GetCostVariants(int offset, int limit, string variantName, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@VariantName", variantName));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                List<CostVariantInfo> bind = sqlH.ExecuteAsList<CostVariantInfo>("usp_Aspx_BindCostVariantsInGrid", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Delete multiple cost variants --------------------------
        [WebMethod]
        public void DeleteMultipleCostVariants(string costVariantIDs, int storeId, int portalId, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CostVariantIds", costVariantIDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleCostVariants", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------ single Cost Variants management------------------------
        [WebMethod]
        public void DeleteSingleCostVariant(string costVariantID, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteSingleCostVariants", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AttributesInputTypeInfo> GetCostVariantInputTypeList()
        {
            try
            {
                List<AttributesInputTypeInfo> ml;
                SQLHandler sqlH = new SQLHandler();
                ml = sqlH.ExecuteAsList<AttributesInputTypeInfo>("dbo.usp_Aspx_CostVariantsInputTypeGetAll");
                return ml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------- bind (edit) cost Variant management--------------------
        [WebMethod]
        public List<CostVariantsGetByCostVariantIDInfo> GetCostVariantInfoByCostVariantID(int costVariantID, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<CostVariantsGetByCostVariantIDInfo> bind = new List<CostVariantsGetByCostVariantIDInfo>();
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                return sqlH.ExecuteAsList<CostVariantsGetByCostVariantIDInfo>("usp_Aspx_CostVariantsGetByCostVariantID", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------- bind (edit) cost Variant values for cost variant ID --------------------
        [WebMethod]
        public List<CostVariantsvalueInfo> GetCostVariantValuesByCostVariantID(int costVariantID, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<CostVariantsGetByCostVariantIDInfo> bind = new List<CostVariantsGetByCostVariantIDInfo>();
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                return sqlH.ExecuteAsList<CostVariantsvalueInfo>("usp_Aspx_GetCostVariantValuesByCostVariantID", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------Save and update Costvariant options-------------------------
        [WebMethod]
        public void SaveAndUpdateCostVariant(int costVariantID, string costVariantName, string description, string cultureName, int inputTypeID,
                                             int displayOrder, bool showInGrid, bool showInSearch, bool showInAdvanceSearch, bool showInComparison, bool isEnableSorting, bool isUseInFilter,
                                             bool isIncludeInPriceRule, bool isIncludeInPromotions, bool isShownInRating, int storeId, int portalId,
                                             bool isActive, bool isModified, string userName, string variantOptions, bool isNewflag)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantName", costVariantName));
                parameterCollection.Add(new KeyValuePair<string, object>("@Description", description));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", inputTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShowInGrid", showInGrid));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShowInSearch", showInSearch));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShowInAdvanceSearch", showInAdvanceSearch));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShowInComparison", showInComparison));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsEnableSorting", isEnableSorting));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsUseInFilter", isUseInFilter));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule", isIncludeInPriceRule));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPromotions", isIncludeInPromotions));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsShownInRating", isShownInRating));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", isModified));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@VariantOption", variantOptions));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsNewFlag", isNewflag));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveAndUpdateCostVariants", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //---------------- Added for unique name check ---------------------
        [WebMethod]
        public bool CheckUniqueCostVariantName(string costVariantName, int costVariantId, int storeId, int portalId)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantName", costVariantName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                return sqlH.ExecuteNonQueryAsBool("usp_Aspx_CostVariantUniquenessCheck", parameterCollection, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Refer-A-Friend
        //-------------------------Save AND SendEmail Messages For Refer-A-Friend----------------
        [WebMethod]
        public void SaveAndSendEmailMessage(int storeID, int portalID, int itemID, string senderName, string senderEmail, string receiverName, string receiverEmail, string subject, string message, string messageBodyDetail, string cultureName)
        {
            try
            {
                ReferToFriendSqlHandler obj = new ReferToFriendSqlHandler();
                obj.SaveEmailMessage(storeID, portalID, itemID, senderName, senderEmail, receiverName, receiverEmail, subject, message);
                obj.SendEmail(storeID, portalID, senderName, senderEmail, receiverEmail, subject, message, messageBodyDetail, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------bind Email list in Grid--------------------------
        [WebMethod]
        public List<ReferToFriendInfo> GetAllReferToAFriendEmailList(int offset, int limit, string senderName, string senderEmail, string receiverName, string receiverEmail, string subject, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@SenderName", senderName));
                parameter.Add(new KeyValuePair<string, object>("@SenderEmail", senderEmail));
                parameter.Add(new KeyValuePair<string, object>("@ReceiverName", receiverName));
                parameter.Add(new KeyValuePair<string, object>("@ReceiverEmail", receiverEmail));
                parameter.Add(new KeyValuePair<string, object>("@Subject", subject));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                List<ReferToFriendInfo> bind = sqlH.ExecuteAsList<ReferToFriendInfo>("usp_Aspx_GetAllReferAFriendEmailsInGrid", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------Delete Email list --------------------------------
        [WebMethod]
        public void DeleteReferToFriendEmailUser(string emailAFriendIDs, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@EmailAFriendID", emailAFriendIDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteReferToFriendUser", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Get UserReferred Friends--------------------------
        [WebMethod]
        public List<ReferToFriendInfo> GetUserReferredFriends(int offset, int limit, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<ReferToFriendInfo>("usp_Aspx_GetUserReferredFriends", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Shipping method management
        //-----------Bind Shipping methods In grid-----------------------------
        [WebMethod]
        public List<ShippingMethodInfo> GetShippingMethodList(int offset, int limit, string shippingMethodName, string deliveryTime, System.Nullable<Decimal> weightLimitFrom, System.Nullable<Decimal> weightLimitTo, System.Nullable<bool> isActive, int storeID, int portalID, string cultureName)
        {
            try
            {
                ShippingMethodSqlProvider obj = new ShippingMethodSqlProvider();
                return obj.GetShippingMethods(offset, limit, shippingMethodName, deliveryTime, weightLimitFrom, weightLimitTo, isActive, storeID, portalID, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------delete multiple shipping methods----------------------
        [WebMethod]
        public void DeleteShippingByShippingMethodID(string shippingMethodIds, int storeId, int portalId, string userName)
        {
            try
            {
                ShippingMethodSqlProvider obj = new ShippingMethodSqlProvider();
                obj.DeleteShippings(shippingMethodIds, storeId, portalId, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------bind shipping service list---------------
        [WebMethod]
        public List<ShippingProviderListInfo> GetShippingProviderList(int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ShippingProviderListInfo>("usp_Aspx_BindShippingProvider", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------------SaveAndUpdate shipping methods-------------------
        [WebMethod]
        public void SaveAndUpdateShippingMethods(int shippingMethodID, string shippingMethodName, string prevFilePath, string newFilePath, string alternateText, int displayOrder, string deliveryTime,
                                                 decimal weightLimitFrom, decimal weightLimitTo, int shippingProviderID, int storeID, int portalID, bool isActive, string userName, string cultureName)
        {
            try
            {
                FileHelperController fileObj = new FileHelperController();
                string uplodedValue = string.Empty;
                if (newFilePath != null && prevFilePath != newFilePath)
                {
                    string tempFolder = @"Upload\temp";
                    uplodedValue = fileObj.MoveFileToSpecificFolder(tempFolder, prevFilePath, newFilePath, @"Modules\AspxCommerce\AspxShippingManagement\uploads\", shippingMethodID, "ship_");
                }
                else
                {
                    uplodedValue = prevFilePath;
                }
                ShippingMethodSqlProvider obj = new ShippingMethodSqlProvider();
                obj.SaveAndUpdateShippings(shippingMethodID, shippingMethodName, uplodedValue, alternateText, displayOrder, deliveryTime, weightLimitFrom, weightLimitTo, shippingProviderID, storeID, portalID, isActive, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------bind Cost dependencies  in Grid--------------------------
        [WebMethod]
        public List<ShippingCostDependencyInfo> GetCostDependenciesListInfo(int offset, int limit, int storeID, int portalID, int shippingMethodId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodId));
                SQLHandler sqlH = new SQLHandler();
                List<ShippingCostDependencyInfo> bind = sqlH.ExecuteAsList<ShippingCostDependencyInfo>("usp_Aspx_BindShippingCostDependencies", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------bind Weight dependencies  in Grid--------------------------
        [WebMethod]
        public List<ShippingWeightDependenciesInfo> GetWeightDependenciesListInfo(int offset, int limit, int storeID, int portalID, int shippingMethodId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodId));
                SQLHandler sqlH = new SQLHandler();
                List<ShippingWeightDependenciesInfo> bind = sqlH.ExecuteAsList<ShippingWeightDependenciesInfo>("usp_Aspx_BindWeightDependencies", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------bind Item dependencies  in Grid--------------------------
        [WebMethod]
        public List<ShippingItemDependenciesInfo> GetItemDependenciesListInfo(int offset, int limit, int storeID, int portalID, int shippingMethodId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodId));
                SQLHandler sqlH = new SQLHandler();
                List<ShippingItemDependenciesInfo> bind = sqlH.ExecuteAsList<ShippingItemDependenciesInfo>("usp_Aspx_bindItemDependencies", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Delete multiple cost Depandencies --------------------------
        [WebMethod]
        public void DeleteCostDependencies(string shippingProductCostIds, int storeId, int portalId, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ShippingProductCostIDs", shippingProductCostIds));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteCostDependencies", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Delete multiple weight Depandencies --------------------------
        [WebMethod]
        public void DeleteWeightDependencies(string shippingProductWeightIds, int storeId, int portalId, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ShippingProductWeightIDs", shippingProductWeightIds));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteShippingWeightDependencies", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Delete multiple item Depandencies --------------------------
        [WebMethod]
        public void DeleteItemDependencies(string shippingItemIds, int storeId, int portalId, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ShippingItemIDs", shippingItemIds));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteShippingItemDependencies", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------save  cost dependencies----------------
        [WebMethod]
        public void SaveCostDependencies(int shippingProductCostID, int shippingMethodID, string costDependenciesOptions, int storeID, int portalID, string userName)
        {
            try
            {

                ShippingMethodSqlProvider obj = new ShippingMethodSqlProvider();
                obj.AddCostDependencies(shippingProductCostID, shippingMethodID, costDependenciesOptions, storeID, portalID, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------- save weight dependencies-------------------------------
        [WebMethod]
        public void SaveWeightDependencies(int shippingProductWeightID, int shippingMethodID, string weightDependenciesOptions, int storeID, int portalID, string userName)
        {
            try
            {
                ShippingMethodSqlProvider obj = new ShippingMethodSqlProvider();
                obj.AddWeightDependencies(shippingProductWeightID, shippingMethodID, weightDependenciesOptions, storeID, portalID, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------- save item dependencies-------------------------------
        [WebMethod]
        public void SaveItemDependencies(int shippingItemID, int shippingMethodID, string itemDependenciesOptions, int storeID, int portalID, string userName)
        {
            try
            {
                ShippingMethodSqlProvider obj = new ShippingMethodSqlProvider();
                obj.AddItemDependencies(shippingItemID, shippingMethodID, itemDependenciesOptions, storeID, portalID, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Shipping Service Providers management
        [WebMethod]
        public List<ShippingProviderNameListInfo> GetShippingProviderNameList(int offset, int limit, int storeID, int portalID, string cultureName, string userName, string shippingProviderName, System.Nullable<bool> isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderName", shippingProviderName));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ShippingProviderNameListInfo>("[dbo].[usp_Aspx_GetShippingProviderNameList]", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ShippingProviderNameListInfo> ShippingProviderAddUpdate(Int32 shippingProviderID, int storeID, int portalID, string userName, string cultureName, string shippingProviderServiceCode, string shippingProviderName, string shippingProviderAliasHelp, bool isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderID", shippingProviderID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));

                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderServiceCode", shippingProviderServiceCode));
                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderName", shippingProviderName));
                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderAliasHelp", shippingProviderAliasHelp));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));

                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ShippingProviderNameListInfo>("[dbo].[usp_Aspx_ShippingProviderAddUpdate]", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteShippingProviderByID(int shippingProviderID, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ShippingProviderID", shippingProviderID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));

                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteShippingProviderByID]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [WebMethod]
        public void DeleteShippingProviderMultipleSelected(string shippingProviderIDs, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ShippingProviderIDs", shippingProviderIDs));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteShippingProviderMultipleSelected]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Coupon Management

        #region Coupon Type Manage
        [WebMethod]
        public List<CouponTypeInfo> GetCouponTypeDetails(int offset, int limit, string couponTypeName, int storeId, int portalId, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@CouponTypeName", couponTypeName));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CouponTypeInfo>("usp_Aspx_GetAllCouponType", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void AddUpdateCouponType(int couponTypeID, string couponType, string isActive, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", couponTypeID));
                parameter.Add(new KeyValuePair<string, object>("@CouponType", couponType));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateCouponType", parameter);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteCouponType(string IDs, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", IDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteCouponType", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Coupon Manage
        [WebMethod]
        public List<CouponInfo> GetCouponDetails(int offset, int limit, System.Nullable<int> couponTypeID, string couponCode, System.Nullable<DateTime> validateFrom, System.Nullable<DateTime> validateTo, int storeId, int portalId, string cultureName)
        {
            try
            {
                CouponManageSQLProvider cmSQLProvider = new CouponManageSQLProvider();
                return cmSQLProvider.BindAllCouponDetails(offset, limit, couponTypeID, couponCode, validateFrom, validateTo, storeId, portalId, cultureName);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public string AddUpdateCouponDetails(int couponID, int couponTypeID, string couponCode, string couponAmount, string validateFrom, string validateTo,
                                             string isActive, int storeID, int portalID, string cultureName, string userName, string settingIDs, string settingValues,
                                             string portalUserCustomerName, string portalUserEmailID, string portalUserUserName, string senderEmail, string subject, ArrayList messageBody)
        {
            string checkMessage = string.Empty;
            try
            {
                CouponManageSQLProvider cmSQLProvider = new CouponManageSQLProvider();
                try
                {
                    cmSQLProvider.AddUpdateCoupons(couponID, couponTypeID, couponCode, couponAmount, validateFrom,
                                                   validateTo, isActive, storeID, portalID, cultureName, userName,
                                                   settingIDs, settingValues, portalUserUserName);
                    checkMessage += "dataSave" + ",";
                }
                catch (Exception)
                {
                    checkMessage += "dataSaveFail" + ",";
                }

                if (checkMessage == "dataSave,")
                {
                    if (portalUserEmailID != "")
                    {
                        try
                        {
                            cmSQLProvider.SendCouponCodeEmail(senderEmail, portalUserEmailID, subject, messageBody);
                            checkMessage += "emailSend";
                        }
                        catch (Exception)
                        {
                            checkMessage += "emailSendFail";
                        }
                    }
                    else
                    {
                        checkMessage += "emailIDBlank";
                    }
                }
                else
                {
                    checkMessage += "emailSendFail";
                }

                return checkMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<CouponStatusInfo> GetCouponStatus()
        {
            try
            {
                CouponManageSQLProvider cmSqlProvider = new CouponManageSQLProvider();
                return cmSqlProvider.BindCouponStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<CouponSettingKeyValueInfo> GetSettinKeyValueByCouponID(int couponID, int storeID, int portalID)
        {
            try
            {
                CouponManageSQLProvider cmSqlProvider = new CouponManageSQLProvider();
                return cmSqlProvider.GetCouponSettingKeyValueInfo(couponID, storeID, portalID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<CouponPortalUserListInfo> GetPortalUsersByCouponID(int offset, int limit, int couponID, int storeID, int portalID, string customerName, string cultureName)
        {
            try
            {
                CouponManageSQLProvider cmSqlProvider = new CouponManageSQLProvider();
                return cmSqlProvider.GetPortalUsersList(offset, limit, couponID, storeID, portalID, customerName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------delete coupons(admin)-----------
        [WebMethod]
        public void DeleteCoupons(string couponIDs, int storeID, int portalID, string userName)
        {
            try
            {
                CouponManageSQLProvider cmSqlProvider = new CouponManageSQLProvider();
                cmSqlProvider.DeleteCoupons(couponIDs, storeID, portalID, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------Verify Coupon Code-----------------------------
        [WebMethod]
        public CouponVerificationInfo VerifyCouponCode(decimal totalCost, string couponCode, int storeID, int portalID, string userName, int appliedCount)
        {
            try
            {
                CouponManageSQLProvider cmSqlProvider = new CouponManageSQLProvider();
                CouponVerificationInfo info = cmSqlProvider.VerifyUserCoupon(totalCost, couponCode, storeID, portalID, userName, appliedCount);
                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------update wherever necessary after coupon verification is successful----------
        [WebMethod]
        public void UpdateCouponUserRecord(string couponCode, int storeID, int portalID, string userName)
        {
            try
            {
                CouponManageSQLProvider cmSQLProvider = new CouponManageSQLProvider();
                cmSQLProvider.UpdateCouponUserRecord(couponCode, storeID, portalID, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Coupons Per Sales Management
        [WebMethod]
        public List<CouponPerSales> GetCouponDetailsPerSales(int offset, System.Nullable<int> limit, string couponCode, int storeID, int portalID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponCode));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));

                return sqlH.ExecuteAsList<CouponPerSales>("usp_Aspx_GetCouponListPerSales", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Coupon User Management
        [WebMethod]
        public List<CouponUserInfo> GetCouponUserDetails(int offset, System.Nullable<int> limit, string couponCode, string userName, System.Nullable<int> couponStatusId, string validFrom, string validTo, int storeId, int portalId, string cultureName)
        {
            try
            {
                CouponManageSQLProvider cmSQLProvider = new CouponManageSQLProvider();
                return cmSQLProvider.GetCouponUserDetails(offset, limit, couponCode, userName, couponStatusId, validFrom, validTo, storeId, portalId, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<CouponUserListInfo> GetCouponUserList(int offset, int limit, int couponID, string couponCode, string userName, System.Nullable<int> couponStatusID, int storeID, int portalID, string cultureName)
        {
            try
            {
                CouponManageSQLProvider cmSQLProvider = new CouponManageSQLProvider();
                return cmSQLProvider.GetCouponUserList(offset, limit, couponID, couponCode, userName, couponStatusID, storeID, portalID, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public void DeleteCouponUser(string couponUserID, int storeID, int portalID, string userName)
        {
            try
            {
                CouponManageSQLProvider cmSQLProvider = new CouponManageSQLProvider();
                cmSQLProvider.DeleteCouponUser(couponUserID, storeID, portalID, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [WebMethod]
        public void UpdateCouponUser(int couponUserID, int couponStatusID, int storeID, int portalID, string cultureName)
        {
            try
            {
                CouponManageSQLProvider cmSQLProvider = new CouponManageSQLProvider();
                cmSQLProvider.UpdateCouponUser(couponUserID, couponStatusID, storeID, portalID, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Coupon Setting Manage/Admin
        [WebMethod]
        public void DeleteCouponSettingsKey(string settingID, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@SettingIDs", settingID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteCouponSettingsKey", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<CouponSettingKeyInfo> CouponSettingManageKey()
        {
            try
            {

                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CouponSettingKeyInfo>("usp_Aspx_GetAllCouponSettingsKey");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void AddUpdateCouponSettingKey(int ID, string settingKey, int validationTypeID, string isActive, int storeID, int portalID, string cultureName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ID", ID));
                parameter.Add(new KeyValuePair<string, object>("@SettingKey", settingKey));
                parameter.Add(new KeyValuePair<string, object>("@ValidationTypeID", validationTypeID));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateCouponSettingKey", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Front Coupon Show
        [WebMethod]
        public List<CouponDetailFrontInfo> GetCouponDetailListFront(int count, int storeID, int portalID, string userName, string cultureName, int customerID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@Count", count));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CouponDetailFrontInfo>("usp_Aspx_GetCouponDetailsForFront", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region Admin DashBoard
        [WebMethod]
        public List<SearchTermInfo> GetSearchStatistics(int count, string commandName, int storeID, int portalID, string cultureName)
        {
            SearchTermSQLProvider stSQLProvider = new SearchTermSQLProvider();
            return stSQLProvider.GetSearchStatistics(count, commandName, storeID, portalID, cultureName);
        }

        [WebMethod]
        public List<LatestOrderStaticsInfo> GetLatestOrderItems(int count, int storeID, int portalID, string cultureName)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@Count", count));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<LatestOrderStaticsInfo>("usp_Aspx_GetLatestOrderStatics", parameter);
        }

        [WebMethod]
        public List<MostViewItemInfoAdminDash> GetMostViwedItemAdmindash(int count, int storeID, int portalID)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@Count", count));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            //parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<MostViewItemInfoAdminDash>("usp_Aspx_GetMostViewdItemAdminDashboard", parameter);
        }

        [WebMethod]
        public List<StaticOrderStatusAdminDashInfo> GetStaticOrderStatusAdminDash(int count, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@Count", count));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                //parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<StaticOrderStatusAdminDashInfo>("usp_Aspx_GetStaticOrderStatusAdminDash", parameter);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<TopCustomerOrdererInfo> GetTopCustomerOrderAdmindash(int count, int storeID, int portalID)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@Count", count));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            //parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<TopCustomerOrdererInfo>("usp_Aspx_GetTopCustomerAdmindash", parameter);
        }

        [WebMethod]
        public List<TotalOrderAmountInfo> GetTotalOrderAmountAdmindash(int storeID, int portalID)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            //  parameter.Add(new KeyValuePair<string, object>("@Count", count));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            //parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<TotalOrderAmountInfo>("usp_Aspx_GetTotalOrderAmountStatus", parameter);
        }

        [WebMethod]
        public List<InventoryDetailAdminDashInfo> GetInventoryDetails(int count, int storeID, int portalID)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@LowStockCount", count));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<InventoryDetailAdminDashInfo>("usp_Aspx_GetInventoryDetailsAdminDash", parameter);
        }
        #endregion

        #region For User DashBoard

        #region Shared Wishlists
        //--------------------bind ShareWishList Email  in Grid--------------------------
        [WebMethod]
        public List<ShareWishListItemInfo> GetAllShareWishListItemMail(int offset, int limit, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));

                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ShareWishListItemInfo>("[dbo].[usp_Aspx_GetShareWishListMailDetailGrid]", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ShareWishListItemInfo> GetShareWishListItemByID(int sharedWishID, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@SharedWishID", sharedWishID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));

                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ShareWishListItemInfo>("[dbo].[usp_Aspx_GetShareWishListByID]", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------Delete ShareWishList --------------------------------
        [WebMethod]
        public void DeleteShareWishListItem(string shareWishListID, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ShareWishIDs", shareWishListID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));

                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[usp_Aspx_DeleteShareWishList]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //-------------------------Update Customer Account Information----------------------------------------  
        [WebMethod]
        public int UpdateCustomer(int storeID, int portalID, int customerID, string userName, string firstName, string lastName, string email)
        {

            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@FirstName", firstName));
                parameterCollection.Add(new KeyValuePair<string, object>("@LastName", lastName));
                parameterCollection.Add(new KeyValuePair<string, object>("@Email", email));
                parameterCollection.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                SQLHandler sqlh = new SQLHandler();
                int errorCode = sqlh.ExecuteNonQueryAsGivenType<int>("dbo.usp_Aspx_UpdateCustomer", parameterCollection, "@ErrorCode");
                return errorCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [WebMethod]
        public bool ChangePassword(int portalID, int storeID, string userName, string newPassword, string retypePassword)
        {
            MembershipController m = new MembershipController();
            try
            {
                if (newPassword != "" && retypePassword != "" && newPassword == retypePassword && userName != "")
                {
                    UserInfo sageUser = m.GetUserDetails(portalID, userName);

                    MembershipUser member = Membership.GetUser(userName);
                    // Guid userID = (Guid)member.ProviderUserKey;
                    string password, passwordSalt;
                    PasswordHelper.EnforcePasswordSecurity(m.PasswordFormat, newPassword, out password, out passwordSalt);
                    UserInfo user = new UserInfo(sageUser.UserID, password, passwordSalt, m.PasswordFormat);
                    m.ChangePassword(user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //---------------User Item Reviews and Ratings-----------------------
        [WebMethod]
        public List<UserRatingInformationInfo> GetUserReviewsAndRatings(int offset, int limit, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetUserItemReviews", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------update rating/ review Items From User DashBoard-----------------------
        [WebMethod]
        public void UpdateItemRatingByUser(string summaryReview, string review, int itemReviewID, int itemID, int storeID, int portalID, string nickName, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ReviewSummary", summaryReview));
                parameter.Add(new KeyValuePair<string, object>("@Review", review));
                parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@Username", nickName));
                parameter.Add(new KeyValuePair<string, object>("@UserBy", userName));
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateItemRatingByUser", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------User DashBoard/Recent History-------------------
        [WebMethod]
        public List<UserRecentHistoryInfo> GetUserRecentlyViewedItems(int offset, int limit, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<UserRecentHistoryInfo>("usp_Aspx_GetUserRecentlyViewedItems", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------User DashBoard/Recent History-------------------
        [WebMethod]
        public List<UserRecentHistoryInfo> GetUserRecentlyComparedItems(int offset, int limit, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<UserRecentHistoryInfo>("usp_Aspx_GetUserRecentlyComparedItems", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void AddUpdateUserAddress(int addressID, int customerID, string firstName, string lastName, string email, string company,
                                         string address1, string address2, string city, string state, string zip, string phone, string mobile,
                                         string fax, string webSite, string countryName, bool isDefaultShipping, bool isDefaultBilling, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                UserDashboardSQLProvider udsqlProvider = new UserDashboardSQLProvider();
                udsqlProvider.AddUpdateUserAddress(addressID, customerID, firstName, lastName, email, company, address1, address2, city,
                                                   state, zip, phone, mobile, fax, webSite, countryName, isDefaultShipping, isDefaultBilling, storeID, portalID, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AddressInfo> GetAddressBookDetails(int storeID, int portalID, int customerID, string userName, string cultureName)
        {
            try
            {
                UserDashboardSQLProvider sqlProvider = new UserDashboardSQLProvider();
                return sqlProvider.GetUserAddressDetails(storeID, portalID, customerID, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteAddressBook(int addressID, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                UserDashboardSQLProvider dashBoardSqlProvider = new UserDashboardSQLProvider();
                dashBoardSqlProvider.DeleteAddressBookDetails(addressID, storeID, portalID, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<UserProductReviewInfo> GetUserProductReviews(int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                UserDashboardSQLProvider dashBoardSqlProvider = new UserDashboardSQLProvider();
                return dashBoardSqlProvider.GetUserProductReviews(storeID, portalID, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void UpdateUserProductReview(int itemID, int itemReviewID, string ratingIDs, string ratingValues, string reviewSummary, string review, int storeID, int portalID, string userName)
        {
            try
            {
                UserDashboardSQLProvider dashBoardSqlProvider = new UserDashboardSQLProvider();
                dashBoardSqlProvider.UpdateUserProductReview(itemID, itemReviewID, ratingIDs, ratingValues, reviewSummary, review, storeID, portalID, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteUserProductReview(int itemID, int itemReviewID, int storeID, int portalID, string userName)
        {
            UserDashboardSQLProvider dashSqlProvider = new UserDashboardSQLProvider();
            dashSqlProvider.DeleteUserProductReview(itemID, itemReviewID, storeID, portalID, userName);
        }

        //---------------userDashBord/My Order List in grid----------------------------
        [WebMethod]
        public List<MyOrderListInfo> GetMyOrderList(int offset, int limit, int storeID, int portalID, int customerID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<MyOrderListInfo>("usp_Aspx_GetMyOrdersList", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------UserDashBoard/ My Orders-------------------
        [WebMethod]
        public List<OrderItemsInfo> GetMyOrders(int orderID, int storeID, int portalID, int customerID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlh = new SQLHandler();
                List<OrderItemsInfo> info;
                info = sqlh.ExecuteAsList<OrderItemsInfo>("usp_Aspx_GetMyOrders", parameter);
                return info;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------UserDashBoard/User Downloadable Items------------------------------
        [WebMethod]
        public List<DownloadableItemsByCustomerInfo> GetCustomerDownloadableItems(int offset, int limit, string sku, string name, int storeId, int portalId, string cultureName, string userName)
        {
            try
            {
                List<DownloadableItemsByCustomerInfo> ml;
                SQLHandler Sq = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@SKU", sku));
                parameterCollection.Add(new KeyValuePair<string, object>("@Name", name));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                ml = Sq.ExecuteAsList<DownloadableItemsByCustomerInfo>("dbo.usp_Aspx_GetCustomerDownloadableItems", parameterCollection);
                return ml;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteCustomerDownloadableItem(string orderItemID, int storeId, int portalId, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();

                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderItemID", orderItemID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));

                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteCustomerDownloadableItem", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void UpdateDownloadCount(int itemID, int orderItemID, string downloadIP, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("@OrderItemID", orderItemID));
                parameter.Add(new KeyValuePair<string, object>("@DownloadIP", downloadIP));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateDownloadCount", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool CheckRemainingDownload(int itemId, int orderItemId, int storeId, int portalId, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();

                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderItemID", orderItemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                return sqlH.ExecuteNonQueryAsBool("dbo.usp_Aspx_CheckRemainingDownloadForCustomer", parameterCollection, "@IsRemainDowload");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CartManage
        //------------------------------Check Cart--------------------------
        [WebMethod]
        public bool CheckCart(int itemID, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                CartManageSQLProvider cartSqlProvider = new CartManageSQLProvider();
                return cartSqlProvider.CheckCart(itemID, storeID, portalID, userName, cultureName);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------------Add to Cart--------------------------
        [WebMethod]
        public bool AddtoCart(int itemID, int storeID, int portalID, string userName, string cultureName)
        {

            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckCostVariantForItem", parameter, "@IsExist");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------------Cart Details--------------------------
        [WebMethod]
        public List<CartInfo> GetCartDetails(int storeID, int portalID, int customerID, string userName, string cultureName, string sessionCode)
        {
            try
            {
                CartManageSQLProvider crtManSQLProvider = new CartManageSQLProvider();
                return crtManSQLProvider.GetCartDetails(storeID, portalID, customerID, userName, cultureName, sessionCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Cart Item Qty Discount Calculations
        [WebMethod]
        public decimal GetDiscountQuantityAmount(int storeID, int portalID, string userName, int customerID, string sessionCode)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsGivenType<decimal>("usp_Aspx_GetItemQuantityDiscountAmount", parameter, "@QtyDiscount");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------------Delete Cart Items--------------------------
        [WebMethod]
        public void DeleteCartItem(int cartID, int cartItemID, int customerID, string sessionCode, int storeID, int portalID, string userName)
        {
            try
            {
                CartManageSQLProvider crtManSQLProvider = new CartManageSQLProvider();
                crtManSQLProvider.DeleteCartItem(cartID, cartItemID, customerID, sessionCode, storeID, portalID, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------Clear My Carts----------------------------
        [WebMethod]
        public void ClearAllCartItems(int cartID, int customerID, string sessionCode, int storeID, int portalID)
        {
            CartManageSQLProvider crtManSQLProvider = new CartManageSQLProvider();
            crtManSQLProvider.ClearAllCartItems(cartID, customerID, sessionCode, storeID, portalID);
        }

        [WebMethod]
        public decimal CheckItemQuantityInCart(int itemID, int storeID, int portalID, int customerID, string sessionCode)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsScalar<decimal>("usp_Aspx_CheckCustomerQuantityInCart", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool CheckCustomerCartExist(int customerID, int storeID, int portalID)
        {

            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckCartExists", parameter, "@IsCartExist");

        }

        //------------------------------Get ShippingMethodByTotalItemsWeight--------------------------
        [WebMethod]
        public List<ShippingMethodInfo> GetShippingMethodByWeight(int storeID, int portalID, int customerID, string userName, string cultureName, string sessionCode)
        {
            try
            {
                CartManageSQLProvider cmSQLProvider = new CartManageSQLProvider();
                return cmSQLProvider.GetShippingMethodByWeight(storeID, portalID, customerID, userName, cultureName, sessionCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ShippingCostInfo> GetShippingCostByItem(int storeID, int portalID, int customerID, string sessionCode, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ShippingCostInfo>("usp_Aspx_ShippingDetailsForItem", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void UpdateShoppingCart(int cartID, string quantitys, int storeID, int portalID, string cartItemIDs,string userName,string cultureName)
        {
            try
            {
                CartManageSQLProvider crtManSQLProvider = new CartManageSQLProvider();
                crtManSQLProvider.UpdateShoppingCart(cartID, quantitys, storeID, portalID, cartItemIDs, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool UpdateCartAnonymoususertoRegistered(int storeID, int portalID, int customerID, string sessionCode)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsBool("usp_Aspx_UpdateCartAnonymoususertoRegistered", parameter, "@IsUpdate");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Quantity Discount Management
        [WebMethod]
        public List<ItemQuantityDiscountInfo> GetItemQuantityDiscountsByItemID(int itemId, int storeID, int portalID, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                return sqlH.ExecuteAsList<ItemQuantityDiscountInfo>("usp_Aspx_GetQuantityDiscountByItemID", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------save quantity discount------------------
        [WebMethod]
        public void SaveItemDiscountQuantity(string discountQuantity, int itemID, int storeID, int portalID, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@DiscountQuantity", discountQuantity));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                sqlH.ExecuteNonQuery("usp_Aspx_SaveItemQuantityDiscounts", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------delete quantity discount------------------
        [WebMethod]
        public void DeleteItemQuantityDiscount(int quantityDiscountID, int itemID, int storeID, int portalID, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@QuantityDiscountID", quantityDiscountID));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteItemQuantityDiscounts", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------quantity discount shown in Item deatils ------------------
        [WebMethod]
        public List<ItemQuantityDiscountInfo> GetItemQuantityDiscountByUserName(int storeID, int portalID, string userName, string itemSKU)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                return sqlH.ExecuteAsList<ItemQuantityDiscountInfo>("usp_Aspx_GetItemQuantityDiscountByUserName", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Search Term Management
        [WebMethod]
        public void AddUpdateSearchTerm(string searchTerm, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                SearchTermSQLProvider stSQLProvider = new SearchTermSQLProvider();
                stSQLProvider.AddUpdateSearchTerm(searchTerm, storeID, portalID, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<SearchTermInfo> ManageSearchTerms(int offset, int limit, int storeID, int portalID, string cultureName, string searchTerm)
        {
            try
            {
                SearchTermSQLProvider stSQLProvider = new SearchTermSQLProvider();
                return stSQLProvider.ManageSearchTerm(offset, limit, storeID, portalID, cultureName, searchTerm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteSearchTerm(string searchTermID, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                SearchTermSQLProvider stSQLProvider = new SearchTermSQLProvider();
                stSQLProvider.DeleteSearchTerm(searchTermID, storeID, portalID, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Tax management
        //--------------item tax classes------------------
        [WebMethod]
        public List<TaxItemClassInfo> GetTaxItemClassDetails(int offset, int limit, string itemClassName, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ItemClassName", itemClassName));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<TaxItemClassInfo>("usp_Aspx_GetItemTaxClasses", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------save item tax class--------------------
        [WebMethod]
        public void SaveAndUpdateTaxItemClass(int taxItemClassID, string taxItemClassName, string cultureName, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@TaxItemClassID", taxItemClassID));
                parameter.Add(new KeyValuePair<string, object>("@TaxItemClassName", taxItemClassName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("usp_Aspx_SaveAndUpdateTaxItemClass", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------Delete tax item classes --------------------------------
        [WebMethod]
        public void DeleteTaxItemClass(string taxItemClassIDs, int storeID, int portalID, string cultureName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@TaxItemClassIDs", taxItemClassIDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteTaxItemClass", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------customer tax classes------------------
        [WebMethod]
        public List<TaxCustomerClassInfo> GetTaxCustomerClassDetails(int offset, int limit, string className, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ClassName", className));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<TaxCustomerClassInfo>("usp_Aspx_GetTaxCustomerClass", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------save customer tax class--------------------
        [WebMethod]
        public void SaveAndUpdateTaxCustmerClass(int taxCustomerClassID, string taxCustomerClassName, string cultureName, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@TaxCustomerClassID", taxCustomerClassID));
                parameter.Add(new KeyValuePair<string, object>("@TaxCustomerClassName", taxCustomerClassName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("usp_Aspx_SaveAndUpdateTaxCustomerClass", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------Delete tax customer classes --------------------------------
        [WebMethod]
        public void DeleteTaxCustomerClass(string taxCustomerClassIDs, int storeID, int portalID, string cultureName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@TaxCustomerClassIDs", taxCustomerClassIDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteTaxCustomerClass", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------tax rates------------------
        [WebMethod]
        public List<TaxRateInfo> GetTaxRateDetails(int offset, System.Nullable<int> limit, string taxName, string searchCountry, string searchState, string zip, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@TaxName", taxName));
                parameter.Add(new KeyValuePair<string, object>("@SearchCountry", searchCountry));
                parameter.Add(new KeyValuePair<string, object>("@SerachState", searchState));
                parameter.Add(new KeyValuePair<string, object>("@Zip", zip));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<TaxRateInfo>("usp_Aspx_GetTaxRates", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------- save and update tax rates--------------------------
        [WebMethod]
        public void SaveAndUpdateTaxRates(int taxRateID, string taxRateTitle, string taxCountryCode, string taxStateCode, string taxZipCode, bool isTaxZipRange, decimal taxRateValue, bool rateType, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@TaxRateID", taxRateID));
                parameter.Add(new KeyValuePair<string, object>("@TaxRateTitle", taxRateTitle));
                parameter.Add(new KeyValuePair<string, object>("@TaxCountryCode", taxCountryCode));
                parameter.Add(new KeyValuePair<string, object>("@TaxStateCode", taxStateCode));
                parameter.Add(new KeyValuePair<string, object>("@ZipPostCode", taxZipCode));
                parameter.Add(new KeyValuePair<string, object>("@IsZipPostRange", isTaxZipRange));
                parameter.Add(new KeyValuePair<string, object>("@TaxRateValue", taxRateValue));
                parameter.Add(new KeyValuePair<string, object>("@RateType", rateType));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveAndUpdateTaxRates", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------dalete Tax rates-----------------------
        [WebMethod]
        public void DeleteTaxRates(string taxRateIDs, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@TaxRateIDs", taxRateIDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteTaxRates", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------------get customer class----------------
        [WebMethod]
        public List<TaxManageRulesInfo> GetTaxRules(int offset, int limit, string ruleName, string customerClassName, string itemClassName, string rateTitle, string priority, string displayOrder, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@RuleName", ruleName));
                parameter.Add(new KeyValuePair<string, object>("@CustomerClassName", customerClassName));
                parameter.Add(new KeyValuePair<string, object>("@ItemClassName", itemClassName));
                parameter.Add(new KeyValuePair<string, object>("@RateTitle", rateTitle));
                parameter.Add(new KeyValuePair<string, object>("@SearchPriority", priority));
                parameter.Add(new KeyValuePair<string, object>("@SearchDisplayOrder", displayOrder));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsList<TaxManageRulesInfo>("usp_Aspx_GetTaxManageRules", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------bind tax customer class name list-------------------------------
        [WebMethod]
        public List<TaxCustomerClassInfo> GetCustomerTaxClass(int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<TaxCustomerClassInfo>("usp_Aspx_GetCustomerTaxClassList", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------bind tax item class name list-------------------------------
        [WebMethod]
        public List<TaxItemClassInfo> GetItemTaxClass(int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<TaxItemClassInfo>("usp_Aspx_GetItemTaxClassList", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------bind tax rate list-------------------------------
        [WebMethod]
        public List<TaxRateInfo> GetTaxRate(int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<TaxRateInfo>("usp_Aspx_GetTaxRateList", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------save and update tax rules--------------------------------------
        [WebMethod]
        public void SaveAndUpdateTaxRule(int taxManageRuleID, string taxManageRuleName, int taxCustomerClassID, int taxItemClassID, int taxRateID, int priority, int displayOrder, string cultureName, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@TaxManageRuleID", taxManageRuleID));
                parameter.Add(new KeyValuePair<string, object>("@TaxManageRuleName", taxManageRuleName));
                parameter.Add(new KeyValuePair<string, object>("@TaxCustomerClassID", taxCustomerClassID));
                parameter.Add(new KeyValuePair<string, object>("@TaxItemClassID", taxItemClassID));
                parameter.Add(new KeyValuePair<string, object>("@TaxRateID", taxRateID));
                parameter.Add(new KeyValuePair<string, object>("@Priority", priority));
                parameter.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveAndUpdateTaxRules", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-------------- delete Tax Rules----------------------------

        [WebMethod]
        public void DeleteTaxManageRules(string taxManageRuleIDs, int storeID, int portalID, string cultureName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@TaxManageRuleIDs", taxManageRuleIDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteTaxRules", parameter);
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
        #endregion

        #region Catalog Pricing Rule

        [WebMethod]
        public List<PricingRuleAttributeInfo> GetPricingRuleAttributes(int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<PricingRuleAttributeInfo> portalRoleCollection;
                PriceRuleSqlProvider priceRuleController = new PriceRuleSqlProvider();
                portalRoleCollection = priceRuleController.GetPricingRuleAttributes(portalID, storeID, userName, cultureName);
                return portalRoleCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<CatalogPriceRulePaging> GetPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, Int32 storeID, Int32 portalID, string userName, string culture, int offset, int limit)
        {
            PriceRuleController priceRuleController = new PriceRuleController();
            return priceRuleController.GetCatalogPricingRules(ruleName, startDate, endDate, isActive, storeID, portalID, userName, culture, offset, limit);
        }


        [WebMethod]
        public CatalogPricingRuleInfo GetPricingRule(Int32 catalogPriceRuleID, Int32 storeID, Int32 portalID, string userName, string culture)
        {
            CatalogPricingRuleInfo catalogPricingRuleInfo;
            PriceRuleController priceRuleController = new PriceRuleController();
            catalogPricingRuleInfo = priceRuleController.GetCatalogPricingRule(catalogPriceRuleID, storeID, portalID, userName, culture);
            return catalogPricingRuleInfo;
        }

        [WebMethod]
        public string SavePricingRule(CatalogPricingRuleInfo objCatalogPricingRuleInfo, Int32 storeID, Int32 portalID, string userName, string culture, object parentID)
        {
            try
            {
                List<KeyValuePair<string, object>> p1 = new List<KeyValuePair<string, object>>();
                p1.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                p1.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sql = new SQLHandler();
                int count = sql.ExecuteAsScalar<int>("usp_Aspx_CatalogPriceRuleCount", p1);
                int maxAllowed = 3;
                int catalogPriceRuleId = objCatalogPricingRuleInfo.CatalogPriceRule.CatalogPriceRuleID;
                if (catalogPriceRuleId > 0)
                {
                    maxAllowed++;
                }
                if (count < maxAllowed)
                {
                    PriceRuleController priceRuleController = new PriceRuleController();
                    priceRuleController.SaveCatalogPricingRule(objCatalogPricingRuleInfo, storeID, portalID, userName,
                                                               culture, parentID);
                    //return "({ \"returnStatus\" : 1 , \"Message\" : \"Saving catalog pricing rule successfully.\" })";
                    return "success";
                }
                else
                {
                    //return "({ \"returnStatus\" : -1 , \"Message\" : \"No more than 3 rules are allowed in Free version of AspxCommerce!\" })";
                    return "notify";
                }
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while saving catalog pricing rule!\" })";
                }
            }
        }

        [WebMethod]
        public string DeletePricingRule(Int32 catalogPricingRuleID, Int32 storeID, Int32 portalID, string userName, string culture)
        {
            try
            {
                PriceRuleController priceRuleController = new PriceRuleController();
                priceRuleController.CatalogPriceRuleDelete(catalogPricingRuleID, storeID, portalID, userName, culture);
                return "({ \"returnStatus\" : 1 , \"Message\" : \"Deleting catalog pricing rule successfully.\" })";
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting catalog pricing rule!\" })";
                }
            }
        }

        [WebMethod]
        public string DeleteMultipleCatPricingRules(string catRulesIds, Int32 storeID, Int32 portalID, string userName, string culture)
        {
            try
            {
                PriceRuleController priceRuleController = new PriceRuleController();
                priceRuleController.CatalogPriceMultipleRulesDelete(catRulesIds, storeID, portalID, userName, culture);
                return "({ \"returnStatus\" : 1 , \"Message\" : \"Deleting multiple catalog pricing rules successfully.\" })";
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting pricing rule!\" })";
                }
            }
        }
        #endregion

        #region Cart Pricing Rule
        [WebMethod]
        public List<ShippingMethodInfo> GetShippingMethods(System.Nullable<bool> isActive, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler Sq = new SQLHandler();
                return Sq.ExecuteAsList<ShippingMethodInfo>("usp_Aspx_GetShippingMethods", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<CartPricingRuleAttributeInfo> GetCartPricingRuleAttributes(int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<CartPricingRuleAttributeInfo> lst;
                PriceRuleSqlProvider priceRuleProvider = new PriceRuleSqlProvider();
                lst = priceRuleProvider.GetCartPricingRuleAttributes(portalID, storeID, userName, cultureName);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public string SaveCartPricingRule(CartPricingRuleInfo objCartPriceRule, Int32 storeID, Int32 portalID, string userName, string culture, object parentID)
        {
            try
            {
                List<KeyValuePair<string, object>> p1 = new List<KeyValuePair<string, object>>();
                //P1.Add(new KeyValuePair<string,object>("@StoreID", storeID));
                p1.Add(new KeyValuePair<string, object>("PortalID", portalID));
                SQLHandler sql = new SQLHandler();
                int count = sql.ExecuteAsScalar<int>("usp_Aspx_CartPrincingRuleCount", p1);
                int maxAllowed = 3;
                int cartPriceRuleId = objCartPriceRule.CartPriceRule.CartPriceRuleID;
                if (cartPriceRuleId > 0)
                {
                    maxAllowed++;
                }
                if (count < maxAllowed)
                {
                    PriceRuleController priceRuleController = new PriceRuleController();
                    priceRuleController.SaveCartPricingRule(objCartPriceRule, storeID, portalID, userName, culture, parentID);
                    //return "({ \"returnStatus\" : 1 , \"Message\" : \"Saving cart pricing rule successfully.\" })";
                    return "success";
                }
                else
                {
                    //return "({ \"returnStatus\" : -1 , \"Message\" : \"No more than 3 rules are allowed in Free version of AspxCommerce!\" })";
                    return "notify";
                }
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while saving cart pricing rule!\" })";
                }
            }
        }

        [WebMethod]
        public List<CartPriceRulePaging> GetCartPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, Int32 storeID, Int32 portalID, string userName, string culture, int offset, int limit)
        {
            PriceRuleController priceRuleController = new PriceRuleController();
            return priceRuleController.GetCartPricingRules(ruleName, startDate, endDate, isActive, storeID, portalID, userName, culture, offset, limit);
        }


        [WebMethod]
        public CartPricingRuleInfo GetCartPricingRule(Int32 cartPriceRuleID, Int32 storeID, Int32 portalID, string userName, string culture)
        {
            CartPricingRuleInfo cartPricingRuleInfo;
            PriceRuleController priceRuleController = new PriceRuleController();
            cartPricingRuleInfo = priceRuleController.GetCartPriceRules(cartPriceRuleID, storeID, portalID, userName, culture);
            return cartPricingRuleInfo;
        }

        [WebMethod]
        public string DeleteCartPricingRule(Int32 cartPricingRuleID, Int32 storeID, Int32 portalID, string userName, string culture)
        {
            try
            {
                PriceRuleController priceRuleController = new PriceRuleController();
                priceRuleController.CartPriceRuleDelete(cartPricingRuleID, storeID, portalID, userName, culture);
                return "({ \"returnStatus\" : 1 , \"Message\" : \"Deleting cart pricing rule successfully.\" })";
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting cart pricing rule!\" })";
                }
            }
        }

        [WebMethod]
        public string DeleteMultipleCartPricingRules(string cartRulesIds, Int32 storeID, Int32 portalID, string userName, string culture)
        {
            try
            {
                PriceRuleController priceRuleController = new PriceRuleController();
                priceRuleController.CartPriceMultipleRulesDelete(cartRulesIds, storeID, portalID, userName, culture);
                return "({ \"returnStatus\" : 1 , \"Message\" : \"Deleting multiple cart pricing rules successfully.\" })";
            }
            catch (Exception ex)
            {
                ErrorHandler errHandler = new ErrorHandler();
                if (errHandler.LogWCFException(ex))
                {
                    return "({ \"returnStatus\" : -1 , \"errorMessage\" : \"" + ex.Message + "\" })";
                }
                else
                {
                    return "({ \"returnStatus\" : -1, \"errorMessage\" : \"Error while deleting cart pricing rule!\" })";
                }
            }
        }
        #endregion

        #region AddToCart
        //
        //[WebMethod]
        //public bool AddtoCart(int itemID, int storeID, int portalID, string userName, string cultureName)
        //{

        //    try
        //    {
        //        List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
        //        parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
        //        parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        //        parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        //        parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
        //        parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        //        SQLHandler sqlH = new SQLHandler();
        //        return sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckCostVariantForItem", parameter, "@IsExist");

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [WebMethod]
        public bool AddItemstoCart(int itemID, decimal itemPrice, int itemQuantity, int storeID, int portalID, string userName, int custometID, string sessionCode, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("@Price", itemPrice));
                parameter.Add(new KeyValuePair<string, object>("@Quantity", itemQuantity));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckCostVariantForItem", parameter, "@IsExist");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void AddItemstoCartFromDetail(int itemID, decimal itemPrice, decimal weight, int itemQuantity, string itemCostVariantIDs, int storeID, int portalID, string userName, int custometID, string sessionCode, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("@Price", itemPrice));
                parameter.Add(new KeyValuePair<string, object>("@Weight", weight));
                parameter.Add(new KeyValuePair<string, object>("@Quantity", itemQuantity));
                parameter.Add(new KeyValuePair<string, object>("@CostVariantsValueIDs", itemCostVariantIDs));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_AddToCart", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MiniCart Display
        //----------------------Count my cart items--------------------
        [WebMethod]
        public int GetCartItemsCount(int storeID, int portalID, int customerID, string sessionCode, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsScalar<int>("usp_Aspx_GetCartItemsCount", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Reporting Module

        //--------------- New Account Reports--------------------------
        [WebMethod]
        public List<NewAccountReportInfo> GetNewAccounts(int offset, System.Nullable<int> limit, int storeID, int portalID, string cultureName, bool monthly, bool weekly, bool hourly)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                if (monthly == true)
                {
                    return sqlH.ExecuteAsList<NewAccountReportInfo>("usp_Aspx_GetNewAccountDetails", parameter);
                }
                if (weekly == true)
                {
                    return sqlH.ExecuteAsList<NewAccountReportInfo>("usp_Aspx_GetNewAccountDetailsByCurrentMonth", parameter);
                }
                if (hourly == true)
                {
                    return sqlH.ExecuteAsList<NewAccountReportInfo>("usp_Aspx_GetNewAccountDetailsBy24hours", parameter);
                }
                else
                    return new List<NewAccountReportInfo>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Sales Tax Report
        [WebMethod]
        public List<StoreTaxesInfo> GetStoreSalesTaxes(int offset, System.Nullable<int> limit, string taxRuleName, int storeID, int portalID, bool monthly, bool weekly, bool hourly)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@TaxManageRuleName", taxRuleName));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlh = new SQLHandler();
                if (monthly == true)
                {
                    return sqlh.ExecuteAsList<StoreTaxesInfo>("usp_Aspx_GetTaxRuleForStoreTaxReport", parameter);
                }
                if (weekly == true)
                {
                    return sqlh.ExecuteAsList<StoreTaxesInfo>("usp_Aspx_GetTaxDetailsByCurrentMonth", parameter);
                }
                if (hourly == true)
                {
                    return sqlh.ExecuteAsList<StoreTaxesInfo>("usp_Aspx_GetTaxReportDetailsBy24hours", parameter);
                }
                else
                    return new List<StoreTaxesInfo>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Items Reporting
        //----------------------GetMostViewedItems----------------------
        [WebMethod]
        public List<MostViewedItemsInfo> GetMostViewedItemsList(int offset, System.Nullable<int> limit, string name, int storeId, int portalId, string userName, string cultureName)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                return obj.GetAllMostViewedItems(offset, limit, name, storeId, portalId, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // --------------------------Get Low Stock Items----------------------------------------------------
        [WebMethod]
        public List<LowStockItemsInfo> GetLowStockItemsList(int offset, System.Nullable<int> limit, string sku, string name, System.Nullable<bool> isActive, int storeId, int portalId, string userName, string cultureName, int lowStock)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                return obj.GetAllLowStockItems(offset, limit, sku, name, isActive, storeId, portalId, userName, cultureName, lowStock);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------------------Get Ordered Items List-----------------------------------
        [WebMethod]
        public List<OrderItemsGroupByItemIDInfo> GetOrderedItemsList(int offset, System.Nullable<int> limit, string name, int storeId, int portalId, string userName, string cultureName)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                return obj.GetOrderedItemsList(offset, limit, name, storeId, portalId, userName, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // --------------------------Get DownLoadable Items----------------------------------------------------
        [WebMethod]
        public List<DownLoadableItemGetInfo> GetDownLoadableItemsList(int offset, System.Nullable<int> limit, string sku, string name, int storeId, int portalId, string userName, string cultureName, System.Nullable<bool> CheckUser)
        {
            try
            {
                ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
                return obj.GetDownLoadableItemsList(offset, limit, sku, name, storeId, portalId, userName, cultureName, CheckUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //---------------------Shipping Reports--------------------
        [WebMethod]
        public List<ShippedReportInfo> GetShippedDetails(int offset, System.Nullable<int> limit, int storeID, int portalID, string cultureName, string shippingMethodName, bool monthly, bool weekly, bool hourly)
        {
            try
            {
                List<ShippedReportInfo> shipInfo;
                List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
                paramCol.Add(new KeyValuePair<string, object>("@offset", offset));
                paramCol.Add(new KeyValuePair<string, object>("@limit", limit));
                paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                paramCol.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                paramCol.Add(new KeyValuePair<string, object>("@ShppingMethod", shippingMethodName));
                SQLHandler sageSQL = new SQLHandler();
                if (monthly == true)
                {
                    shipInfo = sageSQL.ExecuteAsList<ShippedReportInfo>("[dbo].[usp_Aspx_ShippingReportDetails]", paramCol);
                    return shipInfo;
                }
                if (weekly == true)
                {
                    shipInfo = sageSQL.ExecuteAsList<ShippedReportInfo>("[dbo].[usp_Aspx_GetShippingDetailsByCurrentMonth]", paramCol);
                    return shipInfo;
                }
                if (hourly == true)
                {
                    shipInfo = sageSQL.ExecuteAsList<ShippedReportInfo>("[dbo].[usp_Aspx_GetShippingReportDetailsBy24hours]", paramCol);
                    return shipInfo;
                }
                else
                    return new List<ShippedReportInfo>();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // ShoppingCartManagement ---------------------get Cart details in grid-------------------------------
        [WebMethod]
        public List<ShoppingCartInfo> GetShoppingCartItemsDetails(int offset, System.Nullable<int> limit, int storeID, string itemName, string quantity, int portalID, string userName, string cultureName, decimal timeToAbandonCart)
        {
            // quantity = quantity == "" ? null : quantity;
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ItemName", itemName));
                parameter.Add(new KeyValuePair<string, object>("@Quantity", quantity));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@TimeToAbandonCart", timeToAbandonCart));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ShoppingCartInfo>("usp_Aspx_GetLiveCarts", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------bind Abandoned cart details-------------------------
        [WebMethod]
        public List<AbandonedCartInfo> GetAbandonedCartDetails(int offset, System.Nullable<int> limit, int storeID, int portalID, string userName, string cultureName, decimal timeToAbandonCart)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@TimeToAbandonCart", timeToAbandonCart));
                SQLHandler sqlH = new SQLHandler();
                List<AbandonedCartInfo> bind = sqlH.ExecuteAsList<AbandonedCartInfo>("usp_Aspx_GetAbandonedCarts", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // OrderManagement ---------------------get order details in grid-----------------------
        [WebMethod]
        public List<MyOrderListInfo> GetOrderDetails(int offset, System.Nullable<int> limit, int storeID, int portalID, string cultureName, string orderStatusName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@StatusName", orderStatusName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<MyOrderListInfo>("usp_Aspx_GetOrderDetails", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------Send Email for status update----------------------- 
        [WebMethod]  
        public void NotifyOrderStatusUpdate(int storeID ,int portalID, string receiverEmail, string billingShipping,string itemTable,string additionalFields,string templateName)
        {
            try
            {
                EmailTemplate.SendEmailForOrderStatus(storeID,portalID, receiverEmail, billingShipping, itemTable, additionalFields, templateName);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------Update Order Status by Admin-----------------------   
        [WebMethod]
        public bool SaveOrderStatus(int storeID, int portalID, int orderStatusID, int orderID)
        {
            bool checkMsg = false;
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@OrderStatusID", orderStatusID));
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateOrderStatus", parameter);
                checkMsg = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return checkMsg;
        }

        // InvoiceListMAnagement -----------------------get invoice details-----------------------
        [WebMethod]
        public List<InvoiceDetailsInfo> GetInvoiceDetailsList(int offset, System.Nullable<int> limit, string invoiceNumber, string billToNama, string status, int storeID, int portalID, string userName, string cultureName)
        {
            //status = status == "" ? null : status;
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@InvoiceNumber", invoiceNumber));
                //parameter.Add(new KeyValuePair<string, object>("@OrderID", orderId));
                parameter.Add(new KeyValuePair<string, object>("@BillToName", billToNama));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<InvoiceDetailsInfo>("usp_Aspx_GetInvoiceDetails", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Get Invoice Details
        [WebMethod]
        public List<InvoiceDetailByorderIDInfo> GetInvoiceDetailsByOrderID(int orderID, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                SQLHandler sqlh = new SQLHandler();
                List<InvoiceDetailByorderIDInfo> info;
                info = sqlh.ExecuteAsList<InvoiceDetailByorderIDInfo>("usp_Aspx_GetInvoiceDetailsByOrderID", parameter);
                return info;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--ShipmentsListManagement
        [WebMethod]
        public List<ShipmentsDetailsInfo> GetShipmentsDetails(int offset, System.Nullable<int> limit, string shippimgMethodName, string shipToName, string orderId, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodName", shippimgMethodName));
                parameter.Add(new KeyValuePair<string, object>("@ShipToName", shipToName));
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderId));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ShipmentsDetailsInfo>("usp_Aspx_GetShipmentsDetails", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------View Shipments Details--------------------------
        [WebMethod]
        public List<ShipmentsDetailsViewInfo> BindAllShipmentsDetails(int orderID, int portalID, int storeID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ShipmentsDetailsViewInfo>("usp_Aspx_GetShipmentsDetalisForView", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Rating Reviews Reporting
        //--------------------bind Customer Reviews Roports-------------------------
        [WebMethod]
        public List<CustomerReviewReportsInfo> GetCustomerReviews(int offset, System.Nullable<int> limit, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                List<CustomerReviewReportsInfo> bind = sqlH.ExecuteAsList<CustomerReviewReportsInfo>("usp_Aspx_GetCustomerReviews", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Show All Customer Reviews-------------------------
        [WebMethod]
        public List<UserRatingInformationInfo> GetAllCustomerReviewsList(int offset, System.Nullable<int> limit, int storeID, int portalID, string cultureName, string userName, string user, string statusName, string itemName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@User", user));
                parameter.Add(new KeyValuePair<string, object>("@StatusName", statusName));
                parameter.Add(new KeyValuePair<string, object>("@ItemName", itemName));
                SQLHandler sqlH = new SQLHandler();
                List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetCustomerWiseReviewsList", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------Bind User List------------------------------
        [WebMethod]
        public List<UserListInfo> GetUserList(int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<UserListInfo>("sp_PortalUserList", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Item Reviews Reports-------------------------
        [WebMethod]
        public List<ItemReviewsInfo> GetItemReviews(int offset, System.Nullable<int> limit, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                List<ItemReviewsInfo> bind = sqlH.ExecuteAsList<ItemReviewsInfo>("usp_Aspx_GetItemReviewsList", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Show All Item Reviews-------------------------
        [WebMethod]
        public List<UserRatingInformationInfo> GetAllItemReviewsList(int offset,System.Nullable<int> limit, int storeID, int portalID, string cultureName, int itemID, string userName, string statusName, string itemName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@StatusName", statusName));
                parameter.Add(new KeyValuePair<string, object>("@ItemName", itemName));
                SQLHandler sqlH = new SQLHandler();
                List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetItemWiseReviewsList", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        //-----------------------RelatedUPSellANDCrossSellItemsByCartItems-------------------
        [WebMethod]
        public List<ItemBasicDetailsInfo> GetRelatedItemsByCartItems(int storeID, int portalID, string userName, int customerID, string sessionCode, string cultureName, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_RelatedItemsByCartItems", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------bind order status name list-------------------------------
        [WebMethod]
        public List<StatusInfo> GetStatusList(int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<StatusInfo>("usp_Aspx_BindOrderStatusList", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Special Items
        [WebMethod]
        public List<SpecialItemsInfo> GetSpecialItems(int storeID, int portalID, string userName, int count)
        {
            List<SpecialItemsInfo> slInfo;
            List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paramCol.Add(new KeyValuePair<string, object>("@UserName", userName));
            paramCol.Add(new KeyValuePair<string, object>("@count", count));
            SQLHandler sageSQL = new SQLHandler();
            slInfo = sageSQL.ExecuteAsList<SpecialItemsInfo>("[dbo].[usp_Aspx_GetSpecialItems]", paramCol);
            return slInfo;
        }
        #endregion

        #region Best Seller
        [WebMethod]
        public List<BestSellerInfo> GetBestSoldItems(int storeID, int portalID, string userName, int count)
        {
            List<BestSellerInfo> slInfo;
            List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paramCol.Add(new KeyValuePair<string, object>("@UserName", userName));
            paramCol.Add(new KeyValuePair<string, object>("@count", count));
            SQLHandler sageSQL = new SQLHandler();
            slInfo = sageSQL.ExecuteAsList<BestSellerInfo>("[dbo].[usp_Aspx_GetBestSoldItems]", paramCol);
            return slInfo;
        }
        #endregion

        #region Payment Gateway and CheckOUT PROCESS
        [WebMethod]
        public bool CheckDownloadableItemOnly(int storeID, int portalID, int customerID, string sessionCode)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckForDownloadableItemsInCart]", parameter, "@IsAllDownloadable");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<PaymentGatewayListInfo> GetPGList(int storeID, int portalID, string cultureName)
        {
            List<PaymentGatewayListInfo> pginfo;

            List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paramCol.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sageSQL = new SQLHandler();
            pginfo = sageSQL.ExecuteAsList<PaymentGatewayListInfo>("[dbo].[usp_Aspx_GetPaymentGatewayList]", paramCol);

            return pginfo;
        }

        [WebMethod]
        public List<PaymentGateway> GetPaymentGateway(int portalID, string cultureName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                List<PaymentGateway> count = sqlH.ExecuteAsList<PaymentGateway>("sp_GetPaymentGateway", parameter);
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<UserAddressInfo> GetUserAddressForCheckOut(int storeID, int portalID, string userName, string cultureName)
        {

            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<UserAddressInfo>("usp_Aspx_GetUserAddressBookDetails", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool CheckCreditCard(int storeID, int portalID, string creditCardNo)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@CreditCard", creditCardNo));
                //parameter.Add(new KeyValuePair<string, object>("@IsExist", 0));
                return sqlH.ExecuteNonQueryAsBool("usp_Aspx_CheckCreditCardBlackList", parameter, "@IsExist");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [WebMethod]
        public bool CheckEmailAddress(string email,int storeID, int portalID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@Email", email));
                //parameter.Add(new KeyValuePair<string, object>("@IsExist", 0));
                return sqlH.ExecuteNonQueryAsBool("usp_Aspx_CheckEmailIsAdmin", parameter, "@IsExist");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    

        [WebMethod(EnableSession = true)]
        public void SaveOrderDetails(OrderDetailsCollection orderDetail)
        {
            try
            {
                orderDetail.ObjOrderDetails.OrderStatusID = 7;
                AddOrderDetails(orderDetail);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public void AddOrderDetails(OrderDetailsCollection orderData)
        {
            SQLHandler sqlH = new SQLHandler();
            SqlTransaction tran;
            tran = (SqlTransaction)sqlH.GetTransaction();
            //AspxCommerceSession sn = new AspxCommerceSession();
            if (orderData.ObjOrderDetails.InvoiceNumber == null || orderData.ObjOrderDetails.InvoiceNumber == "")
            {
                orderData.ObjOrderDetails.InvoiceNumber = DateTime.Now.ToString("yyyyMMddhhmmss");
            }
            try
            {
                AspxOrderDetails ObjOrderDetails = new AspxOrderDetails();

                int billingAddressID = 0;
                int shippingAddressId = 0;
                int orderID = 0;
                if (orderData.ObjOrderDetails.IsMultipleCheckOut == false)
                {
                    if (orderData.ObjBillingAddressInfo.IsBillingAsShipping == true)
                    {
                        if (int.Parse(orderData.ObjBillingAddressInfo.AddressID) == 0 &&
                            int.Parse(orderData.ObjShippingAddressInfo.AddressID) == 0)
                        {
                            int addressID = ObjOrderDetails.AddAddress(orderData, tran);
                            billingAddressID = ObjOrderDetails.AddBillingAddress(orderData, tran, addressID);
                            shippingAddressId = ObjOrderDetails.AddShippingAddress(orderData, tran, addressID);
                        }
                    }
                    else
                    {
                        if (int.Parse(orderData.ObjBillingAddressInfo.AddressID) == 0)
                            billingAddressID = ObjOrderDetails.AddBillingAddress(orderData, tran);
                  
                        if (int.Parse(orderData.ObjShippingAddressInfo.AddressID) == 0)
                        {
                            if (!orderData.ObjOrderDetails.IsDownloadable)
                            {
                                shippingAddressId = ObjOrderDetails.AddShippingAddress(orderData, tran);
                            }

                        }
                    }
                }
                int paymentMethodID = ObjOrderDetails.AddPaymentInfo(orderData, tran);

                if (billingAddressID > 0)
                {
                    orderID = ObjOrderDetails.AddOrder(orderData, tran, billingAddressID, paymentMethodID);
                    //sn.SetSessionVariable("OrderID", orderID);
                    SetSessionVariable("OrderID", orderID);
                    orderData.ObjOrderDetails.OrderID = orderID;
                    SetSessionVariable("OrderCollection", orderData);
                }
                else
                {
                    orderID = ObjOrderDetails.AddOrderWithMultipleCheckOut(orderData, tran, paymentMethodID);

                    //sn.SetSessionVariable("OrderID", orderID);
                    SetSessionVariable("OrderID", orderID);
                    orderData.ObjOrderDetails.OrderID = orderID;
                    SetSessionVariable("OrderCollection", orderData);
                }

                if (shippingAddressId > 0)
                    ObjOrderDetails.AddOrderItems(orderData, tran, orderID, shippingAddressId);
                else
                    ObjOrderDetails.AddOrderItemsList(orderData, tran, orderID);

                tran.Commit();
            }
            catch (SqlException sqlEX)
            {

                throw new ArgumentException(sqlEX.Message);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }
        #endregion

        #region Payment Gateway Installation

        [WebMethod]
        public List<PaymentGateWayInfo> GetAllPaymentMethod(int offset, int limit, int storeId, int portalId, string paymentGatewayName, System.Nullable<bool> isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayName", paymentGatewayName));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));

                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<PaymentGateWayInfo>("usp_Aspx_GetPaymentGateWayMethod", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<TransactionInfoList> GetAllTransactionDetail(int storeId, int portalId, int paymentGatewayID, System.Nullable<int> orderID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayID", paymentGatewayID));
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<TransactionInfoList>("usp_Aspx_GetAllTransactionDetail", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeletePaymentMethod(string paymentGatewayID, int storeId, int portalId, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();

                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));

                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeletePaymentMethodName", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void UpdatePaymentMethod(int storeId, int portalId, int paymentGatewayID, string paymentGatewayName, bool isActive,bool isUse, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();


                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayID));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsUse", isUse));
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeName", paymentGatewayName));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));

                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdatePaymentMethod", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void AddUpdatePaymentGateWaySettings(int paymentGatewaySettingValueID, int paymentGatewayID, string settingKeys, string settingValues, bool isActive, int storeId, int portalId, string updatedBy, string addedBy)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewaySettingValueID", paymentGatewaySettingValueID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayID));
                parameterCollection.Add(new KeyValuePair<string, object>("@SettingKeys", settingKeys));
                parameterCollection.Add(new KeyValuePair<string, object>("@SettingValues ", settingValues));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@UpdatedBy", updatedBy));
                parameterCollection.Add(new KeyValuePair<string, object>("@AddedBy", addedBy));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_GetPaymentGatewaySettingsSaveUpdate", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<GetOrderdetailsByPaymentGatewayIDInfo> GetOrderDetailsbyPayID(int offset, int limit, string billToName, string shipToName, string orderStatusName, int paymentGatewayID, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@BillToName", billToName));
                parameter.Add(new KeyValuePair<string, object>("@ShipToName", shipToName));
                parameter.Add(new KeyValuePair<string, object>("@OrderStatusAliasName", orderStatusName));
                parameter.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<GetOrderdetailsByPaymentGatewayIDInfo>("usp_Aspx_GetOrderDetailsByPaymentGetwayID", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<OrderDetailsByOrderIDInfo> GetAllOrderDetailsByOrderID(int orderId, int storeId, int portalId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderID", orderId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<OrderDetailsByOrderIDInfo>("usp_Aspx_GetBillingAndShippingAddressDetailsByOrderID", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<OrderItemsInfo> GetAllOrderDetailsForView(int orderId, int storeId, int portalId, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderID", orderId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<OrderItemsInfo>("usp_Aspx_GetAddressDetailsByOrderID", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "StoreSetings"
        [WebMethod(EnableSession = true)]
        public StoreSettingInfo GetAllStoreSettings(int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                StoreSettingInfo DefaultStoreSettings;
                DefaultStoreSettings = sqlH.ExecuteAsObject<StoreSettingInfo>("usp_Aspx_GetAllStoreSettings", parameter);
                Session["DefaultStoreSettings"] = DefaultStoreSettings;
                return DefaultStoreSettings;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void UpdateStoreSettings(string settingKeys, string settingValues, string prevFilePath, string newFilePath,string prevStoreLogoPath,string newStoreLogoPath, int storeID, int portalID, string cultureName)
        {

            try
            {
                FileHelperController fileObj = new FileHelperController();
                string uplodedValue;
                if (newFilePath != null && prevFilePath != newFilePath)
                {
                    string tempFolder = @"Upload\temp";
                    uplodedValue = fileObj.MoveFileToSpecificFolder(tempFolder, prevFilePath, newFilePath, @"Modules\AspxCommerce\AspxStoreSettingsManagement\uploads\", storeID, "store_");
                }
                else
                {
                    uplodedValue = prevFilePath;
                }

                string uploadStorelogoValue;
                if (newStoreLogoPath != null && prevStoreLogoPath != newStoreLogoPath)
                {
                    string tempFolder = @"Upload\temp";
                    uploadStorelogoValue = fileObj.MoveFileToSpecificFolder(tempFolder, prevStoreLogoPath, newStoreLogoPath, @"Modules\AspxCommerce\AspxStoreSettingsManagement\uploads\", storeID, "storelogo_");
                }
                else
                {
                    uploadStorelogoValue = prevStoreLogoPath;
                }

                settingKeys = "DefaultProductImageURL" + '*' + "StoreLogoURL" + '*' + settingKeys;
                settingValues = uplodedValue + '*' + uploadStorelogoValue + '*' + settingValues;

                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@SettingKeys", settingKeys));
                parameter.Add(new KeyValuePair<string, object>("@SettingValues", settingValues));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_GetStoreSettingsUpdate", parameter);
                StoreSettingConfig ssc = new StoreSettingConfig();
                HttpContext.Current.Cache.Remove("AspxStoreSetting" + portalID.ToString() + storeID.ToString());
                ssc.ResetStoreSettingKeys(storeID, portalID, cultureName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CardType_Management
        //------------------------bind All CardType name list-------------------------------        
        [WebMethod]
        public List<CardTypeInfo> GetAllCardTypeList(int offset, int limit, int storeID, int portalID, string cultureName, string cardTypeName, System.Nullable<bool> isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@CardTypeName", cardTypeName));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CardTypeInfo>("usp_Aspx_GetCardTypeInGrid", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<CardTypeInfo> AddUpdateCardType(int storeID, int portalID, string cultureName, string userName, int cardTypeID, string cardTypeName, bool isActive, string newFilePath, string prevFilePath, string alternateText)
        {

            FileHelperController imageObj = new FileHelperController();
            string uploadedFile;

            if (newFilePath != "" && prevFilePath != newFilePath)
            {
                string tempFolder = @"Upload\temp";
                uploadedFile = imageObj.MoveFileToSpecificFolder(tempFolder, prevFilePath, newFilePath, @"Modules\AspxCommerce\AspxCardTypeManagement\uploads\", cardTypeID, "cardType_");

            }
            else
            {
                uploadedFile = prevFilePath;
            }
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CardTypeID", cardTypeID));
                parameter.Add(new KeyValuePair<string, object>("@CardTypeName", cardTypeName));
                parameter.Add(new KeyValuePair<string, object>("@ImagePath", uploadedFile));
                parameter.Add(new KeyValuePair<string, object>("@AlternateText", alternateText));

                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));

                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CardTypeInfo>("[dbo].[usp_Aspx_AddUpdateCardType]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteCardTypeByID(int cardTypeID, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@CardTypeID", cardTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));

                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteCardTypeByID]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [WebMethod]
        public void DeleteCardTypeMultipleSelected(string cardTypeIDs, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@CardTypeIDs", cardTypeIDs));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteCardTypeMultipleSelected]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region OrderStatusManagement
        //------------------------bind Allorder status name list-------------------------------    
        [WebMethod]
        public List<OrderStatusListInfo> GetAllStatusList(int offset, int limit, int storeID, int portalID, string cultureName, string userName, string orderStatusName, System.Nullable<bool> isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@OrderStatusName", orderStatusName));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<OrderStatusListInfo>("[dbo].[usp_Aspx_GetOrderAliasStatusList]", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<OrderStatusListInfo> AddUpdateOrderStatus(int storeID, int portalID, string cultureName, string userName, Int32 orderStatusID, string orderStatusAliasName, string aliasToolTip, string aliasHelp, bool isSystem, bool isActive, bool isReduceQuantity)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@OrderStatusID", orderStatusID));
                parameter.Add(new KeyValuePair<string, object>("@OrderStatusAliasName", orderStatusAliasName));
                parameter.Add(new KeyValuePair<string, object>("@AliasToolTip", aliasToolTip));
                parameter.Add(new KeyValuePair<string, object>("@AliasHelp", aliasHelp));
                parameter.Add(new KeyValuePair<string, object>("@IsSystem", isSystem));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@IsReduceQuantity", isReduceQuantity));

                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<OrderStatusListInfo>("[dbo].[usp_Aspx_OrderStatusAddUpdate]", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteOrderStatusByID(int orderStatusID, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderStatusID", orderStatusID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));

                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteOrderStatusByID]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [WebMethod]
        public void DeleteOrderStatusMultipleSelected(string orderStatusIDs, int storeID, int portalID, string userName, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderStatusIDs", orderStatusIDs));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteOrderStatusMultipleSelected]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #region Admin DashBoard Chart
        //------------------------bind order Chart by last week-------------------------------

        [WebMethod]
        public List<OrderChartInfo> GetOrderChartDetailsByLastWeek(int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID)); ;
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<OrderChartInfo>("usp_Aspx_GetOrderChartDetailsByLastWeek", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------bind order Chart by current month-------------------------------    
        [WebMethod]
        public List<OrderChartInfo> GetOrderChartDetailsBycurentMonth(int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID)); ;
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<OrderChartInfo>("usp_Aspx_GetOrderDetailsByCurrentMonth", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------bind order Chart by one year-------------------------------    
        [WebMethod]
        public List<OrderChartInfo> GetOrderChartDetailsByOneYear(int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID)); ;
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<OrderChartInfo>("usp_Aspx_GetOrderChartDetailsByOneYear", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------bind order Chart by last 24 hours-------------------------------    
        [WebMethod]
        public List<OrderChartInfo> GetOrderChartDetailsBy24Hours(int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID)); ;
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<OrderChartInfo>("usp_Aspx_GetOrderChartBy24hours", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Store Locator
        [WebMethod]
        public List<StoreLocatorInfo> GetAllStoresLocation(int portalID, int storeID)
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            try
            {
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<StoreLocatorInfo>("usp_Aspx_StoreLocatorGetAllStore", parameterCollection);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [WebMethod]
        public List<StoreLocatorInfo> GetLocationsNearBy(double latitude, double longitude, double searchDistance, int portalID, int storeID)
        {
            //GeoCoder.Coordinate Coordinate = GeoCoder.Geocode.GetCoordinates(Address, GoogleAPIKey);

            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@CenterLatitude", latitude));
            parameterCollection.Add(new KeyValuePair<string, object>("@CenterLongitude", longitude));
            parameterCollection.Add(new KeyValuePair<string, object>("@SearchDistance", searchDistance));
            parameterCollection.Add(new KeyValuePair<string, object>("@EarthRadius", 3961));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));

            try
            {
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<StoreLocatorInfo>("usp_Aspx_StoreLocatorGetNearbyStore", parameterCollection);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [WebMethod]
        public bool UpdateStoreLocation(int storeID, int portalID, string storeName, String storeDescription, string streetName, string localityName, string city, string state, string country, string zip, double latitude, double longitude, string userName)
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreName", storeName));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreDescription", storeDescription));
            parameterCollection.Add(new KeyValuePair<string, object>("@StreetName", streetName));
            parameterCollection.Add(new KeyValuePair<string, object>("@LocalityName", localityName));
            parameterCollection.Add(new KeyValuePair<string, object>("@City", city));
            parameterCollection.Add(new KeyValuePair<string, object>("@State", state));
            parameterCollection.Add(new KeyValuePair<string, object>("@Country", country));
            parameterCollection.Add(new KeyValuePair<string, object>("@ZIP", zip));
            parameterCollection.Add(new KeyValuePair<string, object>("@Latitude", latitude));
            parameterCollection.Add(new KeyValuePair<string, object>("@Longitude", longitude));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));

            try
            {
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_StoreLocatorLocationUpdate", parameterCollection);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        [WebMethod]
        public void AddStoreLocatorSettings(string settingKey, string settingValue, string cultureName, int storeID, int portalID, string userName)
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@SettingKey", settingKey));
            parameterCollection.Add(new KeyValuePair<string, object>("@SettingValue", settingValue));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@AddedBy", userName));
            try
            {
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_StoreLocatorSettingsAdd", parameterCollection);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Online Users
        [WebMethod]
        public List<OnLineUserBaseInfo> GetRegisteredUserOnlineCount(int offset, int limit, string searchUserName, string searchHostAddress, string searchBrowser, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@Offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("@Limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("@SearchUserName", searchUserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@HostAddress", searchHostAddress));
                parameterCollection.Add(new KeyValuePair<string, object>("@Browser", searchBrowser));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<OnLineUserBaseInfo>("usp_Aspx_GetOnlineRegisteredUsers", parameterCollection);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [WebMethod]
        public List<OnLineUserBaseInfo> GetAnonymousUserOnlineCount(int offset, int limit, string searchHostAddress, string searchBrowser, int storeID, int portalID, string userName)
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();

            parameterCollection.Add(new KeyValuePair<string, object>("@Offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@Limit", limit));

            parameterCollection.Add(new KeyValuePair<string, object>("@HostAddress", searchHostAddress));
            parameterCollection.Add(new KeyValuePair<string, object>("@Browser", searchBrowser));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<OnLineUserBaseInfo> lst;
                lst = sqlH.ExecuteAsList<OnLineUserBaseInfo>("usp_Aspx_GetOnlineAnonymousUsers", parameterCollection);
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Customer Reports By Order Total
        //--------------------bind Customer Order Total Roports-------------------------    
        [WebMethod]
        public List<CustomerOrderTotalInfo> GetCustomerOrderTotal(int offset, System.Nullable<int> limit, int storeID, int portalID, string cultureName, string user)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@User", user));
                SQLHandler sqlH = new SQLHandler();
                List<CustomerOrderTotalInfo> bind = sqlH.ExecuteAsList<CustomerOrderTotalInfo>("usp_Aspx_GetCustomerOrderTotal", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Store Access Management
        [WebMethod]
        public List<StoreAccessAutocomplete> SearchStoreAccess(string text, int keyID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessKeyID", keyID));
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessData", text));
                return sqlH.ExecuteAsList<StoreAccessAutocomplete>("[dbo].[usp_Aspx_GetSearchAutoComplete]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void SaveUpdateStoreAccess(int edit, int storeAccessKeyID, string storeAccessData, string reason, bool isActive, int storeID, int portalID, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessKeyID", storeAccessKeyID));
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessData", storeAccessData));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@Reason", reason));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessID", edit));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_StoreAccessAddUpdate]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeletStoreAccess(int storeAccessID, int storeID, int portalID, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessID", storeAccessID));
                parameter.Add(new KeyValuePair<string, object>("@DeletedBy", userName));

                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_StoreAccessDelete]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AspxUserList> GetAspxUser(string userName, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<AspxUserList>("[dbo].[usp_Aspx_GetListOfCurrentCustomer]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<AspxUserList> GetAspxUserEmail(string email, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@Email", email));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<AspxUserList>("[dbo].[usp_Aspx_GetListOfCurrentCustomerEmail]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [WebMethod]
        public List<StoreAccessKey> GetStoreKeyID()
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<StoreAccessKey>("[dbo].[usp_Aspx_GetStoreAccessKeyID]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       [WebMethod]
        public List<StoreAccessInfo> LoadStoreAccessCustomer(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, int storeID, int portalID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Search", search));
                parameter.Add(new KeyValuePair<string, object>("@StartDate", startDate));
                parameter.Add(new KeyValuePair<string, object>("@EndDate", endDate));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));

                return sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessCustomer]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<StoreAccessInfo> LoadStoreAccessEmails(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, int storeID, int portalID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Search", search));
                parameter.Add(new KeyValuePair<string, object>("@StartDate", startDate));
                parameter.Add(new KeyValuePair<string, object>("@EndDate", endDate));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));

                return sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessEmail]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<StoreAccessInfo> LoadStoreAccessIPs(int offset, int limit, string search, System.Nullable<DateTime> startDate,System.Nullable<DateTime> endDate, System.Nullable<bool> status, int storeID, int portalID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Search", search));
                parameter.Add(new KeyValuePair<string, object>("@StartDate", startDate));
                parameter.Add(new KeyValuePair<string, object>("@EndDate", endDate));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));

                return sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessIP]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<StoreAccessInfo> LoadStoreAccessDomains(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, int storeID, int portalID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Search", search));
                parameter.Add(new KeyValuePair<string, object>("@StartDate", startDate));
                parameter.Add(new KeyValuePair<string, object>("@EndDate", endDate));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));

                return sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessDomain]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<StoreAccessInfo> LoadStoreAccessCreditCards(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, int storeID, int portalID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Search", search));
                parameter.Add(new KeyValuePair<string, object>("@StartDate", startDate));
                parameter.Add(new KeyValuePair<string, object>("@EndDate", endDate));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));

                return sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessCreditCard]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Store Close
        [WebMethod]
        public void SaveStoreClose(System.Nullable<bool> temporary, System.Nullable<bool> permanent, System.Nullable<DateTime> closeFrom, System.Nullable<DateTime> closeTill, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@Temporary", temporary));
                parameter.Add(new KeyValuePair<string, object>("@Permanent", permanent));
                parameter.Add(new KeyValuePair<string, object>("@CloseFrom", closeFrom));
                parameter.Add(new KeyValuePair<string, object>("@CloseTill", closeTill));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveStoreClose", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CustomerDetails
        [WebMethod]
        public List<CustomerDetailsInfo> GetCustomerDetails(int offset, int limit, int storeID, int portalID, string cultureName, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));

                SQLHandler sqlH = new SQLHandler();
                List<CustomerDetailsInfo> bind = sqlH.ExecuteAsList<CustomerDetailsInfo>("[dbo].[usp_Aspx_GetCustomerDetails]", parameter);
                return bind;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteMultipleCustomersByCustomerID(string customerIDs, int storeId, int portalId, string userName)
        {
            try
            {
                CustomerManagementSQLProvider obj = new CustomerManagementSQLProvider();
                obj.DeleteMultipleCustomers(customerIDs, storeId, portalId, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteCustomerByCustomerID(int customerId, int storeId, int portalId, string userName)
        {
            try
            {
                CustomerManagementSQLProvider obj = new CustomerManagementSQLProvider();
                obj.DeleteCustomer(customerId, storeId, portalId, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Order Request Return
        [WebMethod]
        public void UpdateReturnRequests(int id, int status, int storeID, int portalID, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ID", id));
                parameter.Add(new KeyValuePair<string, object>("@StatusID", status));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
                sqlH.ExecuteNonQuery("[usp_Aspx_UpdateRequestReturn]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteStatus(int ID, int storeID, int portalID, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ID", ID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
                sqlH.ExecuteNonQuery("[usp_Aspx_DeleteReturnStatus]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void DeleteReason(int ID, int storeID, int portalID, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ID", ID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteReturnReason]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ReturnRequestAction> GetListReturnAction(int offset, int limit, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ReturnRequestAction>("[dbo].[usp_Aspx_GetListReturnAction]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ReturnRequestStatus> GetListReturnStatus(int offset, int limit, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ReturnRequestStatus>("[dbo].[usp_Aspx_GetListReturnStatus]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ReturnRequestsReason> GetListReturnReason(int offset, int limit, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<ReturnRequestsReason>("[usp_Aspx_GetListReturnReason]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void UpdateAction(int isupdate, string action, int displayOrder, bool isActive, int storeID, int portalID, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ID", isupdate));
                parameter.Add(new KeyValuePair<string, object>("@Action", action));
                parameter.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_UpdateReturnAction]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void SaveUpdateReason(int isupdate, string reason, int displayOrder, bool isActive, int storeID, int portalID, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@IsUpdate", isupdate));
                parameter.Add(new KeyValuePair<string, object>("@Reason", reason));
                parameter.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_SaveUpdateReturnReason]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void SaveUpdateStatus(int isupdate, string status, int displayOrder, bool isActive, int storeID, int portalID, string userName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@IsUpdate", isupdate));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));
                parameter.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_SaveUpdateReturnStatus]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<ReturnReasonList> LoadReason(int storeID, int portalID)
        {
            try
            {
                List<ReturnReasonList> catInfo;
                List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
                paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sageSQL = new SQLHandler();
                catInfo = sageSQL.ExecuteAsList<ReturnReasonList>("[usp_Aspx_GetReturnReason]", paramCol);
                return catInfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [WebMethod]
        public List<ReturnActionList> LoadAction(int storeID, int portalID)
        {
            try
            {
                List<ReturnActionList> catInfo;
                List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
                paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sageSQL = new SQLHandler();
                catInfo = sageSQL.ExecuteAsList<ReturnActionList>("[usp_Aspx_GetReturnAction]", paramCol);
                return catInfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [WebMethod]
        public List<ReturnStatusList> LoadRequestStatus(int storeID, int portalID)
        {
            try
            {
                List<ReturnStatusList> catInfo;
                List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
                paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sageSQL = new SQLHandler();
                catInfo = sageSQL.ExecuteAsList<ReturnStatusList>("[dbo].[usp_Aspx_GetRequestStatus]", paramCol);
                return catInfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [WebMethod]
        public List<ReturnRequestList> LoadReturnRequest(int offset, int limit, string customer, System.Nullable<int> status, string email, int storeID, int portalID)
        {
            try
            {
                List<ReturnRequestList> catInfo;
                List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
                paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                paramCol.Add(new KeyValuePair<string, object>("@limit", limit));
                paramCol.Add(new KeyValuePair<string, object>("@offset", offset));
                paramCol.Add(new KeyValuePair<string, object>("@Customer", customer));
                paramCol.Add(new KeyValuePair<string, object>("@Email", email));
                paramCol.Add(new KeyValuePair<string, object>("@Status", status));
                SQLHandler sageSQL = new SQLHandler();
                catInfo = sageSQL.ExecuteAsList<ReturnRequestList>("[dbo].[usp_Aspx_GetRequestReturn]", paramCol);
                return catInfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        //------------------------Multiple Delete Recently viewed Items-------------------------------    
        [WebMethod]
        public void DeleteViewedItems(string viewedItems, int storeID, int portalID, string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ViewedItems", viewedItems));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleViewedItems", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //------------------------Multiple Delete Compared viewed Items-------------------------------    
        [WebMethod]
        public void DeleteComparedItems(string compareItems, int storeID, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CompareItems", compareItems));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleComparedItems", parameter); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public string GetDiscountPriceRule(int cartID, int storeID, int portalID, string userName, string cultureName, decimal shippingCost)
        {

            try
            {
                SqlConnection sqlConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
                SqlCommand sqlCmd = new SqlCommand();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                DataSet sqlDs = new DataSet();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = "[dbo].[usp_Aspx_GetDiscountCartPriceRule]";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@CartID", cartID);
                sqlCmd.Parameters.AddWithValue("@StoreID", storeID);
                sqlCmd.Parameters.AddWithValue("@PortalID", portalID);
                sqlCmd.Parameters.AddWithValue("@CultureName", cultureName);
                sqlCmd.Parameters.AddWithValue("@UserName", userName);
                sqlCmd.Parameters.AddWithValue("@ShippingCost", shippingCost);
                sqlAdapter.SelectCommand = sqlCmd;
                sqlConn.Open();
                SqlDataReader dr = null;
            
                dr = sqlCmd.ExecuteReader();

                string discount = string.Empty;
                if (dr.Read())
                {
                    discount = dr["Discount"].ToString();

                }

                return discount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }   

        [WebMethod]
        public int GetCartId(int storeID, int portalID, int customerID, string sessionCode)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
                SqlCommand sqlCmd = new SqlCommand();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                DataSet sqlDs = new DataSet();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = "[dbo].[usp_Aspx_GetCartID]";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@CustomerID", customerID);
                sqlCmd.Parameters.AddWithValue("@StoreID", storeID);
                sqlCmd.Parameters.AddWithValue("@PortalID", portalID);
                sqlCmd.Parameters.AddWithValue("@SessionCode", sessionCode);

                sqlAdapter.SelectCommand = sqlCmd;
                sqlConn.Open();
                SqlDataReader dr = null;

                dr = sqlCmd.ExecuteReader();

                int cartId = 0;
                if (dr.Read())
                {
                    cartId = int.Parse(dr["CartID"].ToString());

                }

                return cartId;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region GetStoreSetting
        [WebMethod]
        public string GetStoreSettingValueByKey(string settingKey, int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@SettingKey", settingKey));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsGivenType<string>("usp_Aspx_GetStoreSettingValueBYKey", parameter, "@SettingValue");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Session Setting/Getting
        [WebMethod(EnableSession = true)]
        public void SetSessionVariableCoupon(string key, int value)
        {
            if (System.Web.HttpContext.Current.Session[key] != null)
            {
                value = int.Parse(System.Web.HttpContext.Current.Session[key].ToString()) + 1;
            }
            else
            {
                value = value + 1;
            }

            System.Web.HttpContext.Current.Session[key] = value;
            //  string asdf = System.Web.HttpContext.Current.Session["OrderID"].ToString();
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }

        [WebMethod(EnableSession = true)]
        public void SetSessionVariable(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
            //  string asdf = System.Web.HttpContext.Current.Session["OrderID"].ToString();
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }

        [WebMethod(EnableSession = true)]
        public void ClearSessionVariable(string key)
        {
            System.Web.HttpContext.Current.Session.Remove(key);
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }

        [WebMethod(EnableSession = true)]
        public void ClearALLSessionVariable()
        {
            System.Web.HttpContext.Current.Session.Clear();
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }

        [WebMethod(EnableSession = true)]
        public int GetSessionVariable(string key)
        {
            if (System.Web.HttpContext.Current.Session[key] != null)
            {
                string i = System.Web.HttpContext.Current.Session[key].ToString();
                return Convert.ToInt32(i.ToString());
            }
            else
            {
                return 0;
            }

            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }
        [WebMethod(EnableSession = true)]
        public string GetSessionVariableCart(string key)
        {
            string val = string.Empty;
            if (System.Web.HttpContext.Current.Session[key] != null)
            {
                val= System.Web.HttpContext.Current.Session[key].ToString();
           
            }
            return val;
       
            // return System.Web.HttpContext.Current.Session["MySessionObject"] = "OderID";
        }
        #endregion

        #region StoreSettingImplementation
        [WebMethod]
        public decimal GetTotalCartItemPrice(int storeID, int portalID, int customerID, string sessionCode)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsScalar<decimal>("usp_Aspx_GetCartItemsTotalAmount", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public int GetCompareItemsCount(int storeID, int portalID, string sessionCode,string userName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsScalar<int>("usp_Aspx_GetCompareItemsCount", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool CheckAddressAlreadyExist(int storeID, int portalID, int customerID, string sessionCode)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckForMultipleAddress", parameter, "@IsExist");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        [WebMethod]
        public List<PortalUserRoleListInfo> GetAllPortalUserList(int storeID,int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));

                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<PortalUserRoleListInfo>("usp_Aspx_GetPortalUserList", parameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "Currency conversion"

        [WebMethod]
        public List<CurrencyInfo> BindCurrencyList(int storeID, int portalID, string cultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));

                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CurrencyInfo>("usp_Aspx_BindCurrencyList", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [WebMethod]
        public double GetCurrencyRate(string from, string to)
        {
            try
            {
                double result = 0.0;
                result = CurrencyConverter.CurrencyConverter.GetRate(from, to);
                return result;
            }
            catch (Exception)
            {
                return 1;

            }
        }

        #endregion

        [WebMethod]
        public bool CheckCatalogPriorityUniqueness(int catalogPriceRuleID, int priority, int storeID, int portalID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@CatalogPriceRuleID", catalogPriceRuleID));
                parameterCollection.Add(new KeyValuePair<string, object>("@Priority", priority));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                return sqlH.ExecuteNonQueryAsBool("[usp_Aspx_CatalogPriorityUniquenessCheck]", parameterCollection, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool CheckCartPricePriorityUniqueness(int cartPriceRuleID, int priority, int portalID)
        {
            try
            {
                SQLHandler Sq = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@CartPriceRuleID", cartPriceRuleID));
                parameterCollection.Add(new KeyValuePair<string, object>("@Priority", priority));          
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                return Sq.ExecuteNonQueryAsBool("[usp_Aspx_CartPricePriorityUniquenessCheck]", parameterCollection, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public bool CheckUniquenessForDisplayOrder(int storeID, int portalID, int value, int shippingMethodID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                Parameter.Add(new KeyValuePair<string, object>("@Value", value));
                Parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
                return sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckUniquenessForDisplayOrder]", Parameter, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool CheckShippingProviderUniqueness(int storeId, int portalId, string cultureName, int shippingProviderId, string shippingProviderName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                Parameter.Add(new KeyValuePair<string, object>("@ShippingProviderID", shippingProviderId));
                Parameter.Add(new KeyValuePair<string, object>("@ShippingProviderName", shippingProviderName));
                return sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckShippingProviderUniquness]", Parameter, "@IsUnique");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //For Demo Availability Check
        [WebMethod]
        public bool IsStoreExists(string storeName)
        {
            bool i = false;
            var paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@PortalName", storeName));
            var sqlH = new SQLHandler();
            i = sqlH.ExecuteAsScalar<bool>("usp_Aspx_CheckPortal", paraMeter);
            return i;
        }

        [WebMethod]
        public bool CheckOrderStatusUniqueness(int storeId, int portalId, string cultureName, int orderStatusId, string orderStatusAliasName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                Parameter.Add(new KeyValuePair<string, object>("@OrderStatusID", orderStatusId));
                Parameter.Add(new KeyValuePair<string, object>("@OrderStatusName", orderStatusAliasName));
                return sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckOrderStatusUniquness]", Parameter, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public bool CheckExisting(int storeId, int portalId, int storeAccesskeyId, string accessData)
        {
            try
            {
                var sqlH = new SQLHandler();
                  var parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessKeyID", storeAccesskeyId));
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessData", accessData));
                return sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckExistingStoreAccess]", parameter, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool CheckCouponTypeUniqueness(int storeId, int portalId, string cultureName, int couponTypeId, string couponType)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                Parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                Parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", couponTypeId));
                Parameter.Add(new KeyValuePair<string, object>("@CouponType", couponType));
                return sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckCouponTypeUniquness]", Parameter, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public bool CheckUniqueness(int storeID, int portalID, int value, int shippingMethodID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                Parameter.Add(new KeyValuePair<string, object>("@Value", value));
                Parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
                return sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckUniquenessForDisplayOrder]", Parameter, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public bool CheckTaxUniqueness(int storeID, int portalID, int value, int taxRuleID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                Parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                Parameter.Add(new KeyValuePair<string, object>("@Value", value));
                Parameter.Add(new KeyValuePair<string, object>("@TaxRuleID", taxRuleID));
                return sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckUniquenessForTaxRuleDisplayOrder]", Parameter, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }