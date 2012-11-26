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
using System.Data.SqlClient;
using System.Net;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using System.IO;
using AspxCommerce.Core;
using System.Text;
using System.Web.UI;
using System.Data;
using SageFrame.SageFrameClass.MessageManagement;
using System.Web.Services;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

public class AspxCommerceWCFService
{
    #region Testing Method
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public string GetCallWCF()
    {
        try
        {
            return "Do your WORK";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Header Menu category Lister
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CategoryInfo> GetCategoryMenuList(int storeID, int portalID, string cultureName)
    {
        List<CategoryInfo> catInfo = new List<CategoryInfo>();

        List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
        paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        paramCol.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        SQLHandler sageSQL = new SQLHandler();
        catInfo = sageSQL.ExecuteAsList<CategoryInfo>("[dbo].[usp_aspx_GetCategoryMenuAttributes]", paramCol);

        return catInfo;
    }
    #endregion

    #region Aspx BreadCrumb
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public string GetCategoryForItem(int storeID, int portalID, string itemSku)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemSku", itemSku));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsScalar<string>("usp_Aspx_GetCategoryforItems", ParaMeter);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    #endregion

    #region General Functions
    //--------------------Roles Lists------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<PortalRole> GetAllRoles(Int32 storeID, Int32 portalID, string userName, string culture)
    {
        try
        {
            List<PortalRole> portalRoleCollection = new List<PortalRole>();
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<StoreInfo> GetAllStores(int portalID, string userName, string culture)
    {
        StoreSqlProvider storeSqlProvider = new StoreSqlProvider();
        return storeSqlProvider.GetAllStores(portalID, userName, culture);
    }

    //----------------country list------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CurrencyInfo> BindCurrencyList()
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CurrencyInfo>("usp_Aspx_BindCurrencyList");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Status Management
    //------------------Status DropDown-------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<StatusInfo> GetStatus(string cultureName)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            return sqlH.ExecuteAsList<StatusInfo>("usp_Aspx_GetStatusList", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Bind Users DropDown
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<AttributesBasicInfo> GetAttributesList(int offset, int limit, string attributeName, System.Nullable<bool> isRequired, System.Nullable<bool> comparable, System.Nullable<bool> IsSystem, int storeId, int portalId, string cultureName, string userName)
    {
        try
        {
            ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
            return obj.GetItemAttributes(offset, limit, attributeName, isRequired, comparable, IsSystem, storeId, portalId, cultureName, userName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveUpdateAttributeInfo(int attributeId, string attributeName, int inputTypeID, string defaultValue, int validationTypeID,
        System.Nullable<int> length, string aliasName, string aliasToolTip, string aliasHelp, int displayOrder, bool isUnique, bool isRequired, bool isEnableEditor,
        bool showInGrid, bool showInSearch, bool showInAdvanceSearch, bool showInComparison, bool isIncludeInPriceRule,
        bool isIncludeInPromotions, bool isEnableSorting, bool isUseInFilter, bool isShownInRating, int storeId, int portalId,
        bool isActive, bool isModified, string userName, string cultureName, string itemTypes, bool flag, bool isUsedInConfigItem, string saveOptions)
    {
        try
        {
            AttributesGetByAttributeIdInfo attributeInfoToInsert = new AttributesGetByAttributeIdInfo
            {
                AttributeID = attributeId,
                AttributeName = attributeName,
                InputTypeID = inputTypeID,
                DefaultValue = defaultValue,
                ValidationTypeID = validationTypeID,
                Length = length > 0 ? length : null,
                AliasName = aliasName,
                AliasToolTip = aliasToolTip,
                AliasHelp = aliasHelp,
                DisplayOrder = displayOrder,
                IsUnique = isUnique,
                IsRequired = isRequired,
                IsEnableEditor = isEnableEditor,
                ShowInGrid = showInGrid,
                ShowInSearch = showInSearch,
                ShowInAdvanceSearch = showInAdvanceSearch,
                ShowInComparison = showInComparison,
                IsIncludeInPriceRule = isIncludeInPriceRule,
                IsIncludeInPromotions = isIncludeInPromotions,
                IsEnableSorting = isEnableSorting,
                IsUseInFilter = isUseInFilter,
                IsShownInRating = isShownInRating,
                StoreID = storeId,
                PortalID = portalId,
                IsActive = isActive,
                IsModified = isModified,
                UpdatedBy = userName,
                AddedBy = userName,
                CultureName = cultureName,
                ItemTypes = itemTypes,
                Flag = flag,
                IsUsedInConfigItem = isUsedInConfigItem,
                SaveOptions = saveOptions
            };
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveUpdateAttributeGroupInfo(int attributeSetId, string groupName, int GroupID, string CultureName, string Aliasname, int StoreId, int PortalId, string UserName, bool isActive, bool isModified, bool flag)
    {
        //attributeSetId: 1, groupName: node, GroupID: _groupId, CultureName: cultureName, AliasName: node, storeId: _storeId, portalId: _portalId, userName: _userName, isActive: _isActive, isModified: _isModified, flag: _updateFlag
        try
        {
            ItemAttributesManagementSqlProvider obj = new ItemAttributesManagementSqlProvider();
            obj.UpdateAttributeGroup(attributeSetId, groupName, GroupID, CultureName, Aliasname, StoreId, PortalId, UserName, isActive, isModified, flag);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]

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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemsInfo> GetItemsList(int offset, int limit, string Sku, string name, string itemType, string attributesetName, string visibility, System.Nullable<bool> isActive, int storeId, int portalId, string userName, string cultureName)
    {
        try
        {
            ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
            return obj.GetAllItems(offset, limit, Sku, name, itemType, attributesetName, visibility, isActive, storeId, portalId, userName, cultureName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<AttributeFormInfo> GetItemFormAttributes(int attributeSetID, int itemTypeID, int storeID, int portalID, string userName, string culture)
    {
        ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
        List<AttributeFormInfo> frmItemFieldList = obj.GetItemFormAttributes(attributeSetID, itemTypeID, storeID, portalID, userName, culture);
        return frmItemFieldList;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<AttributeFormInfo> GetItemFormAttributesByitemSKUOnly(string itemSKU, int storeID, int portalID, string userName, string culture)
    {
        ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
        List<AttributeFormInfo> frmItemFieldList = obj.GetItemFormAttributesByItemSKUOnly(itemSKU, storeID, portalID, userName, culture);
        return frmItemFieldList;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<AttributeFormInfo> GetItemFormAttributesValuesByItemID(int itemID, int attributeSetID, int itemTypeID, int storeID, int portalID, string userName, string culture)
    {
        ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
        List<AttributeFormInfo> frmItemAttributes = obj.GetItemAttributesValuesByItemID(itemID, attributeSetID, itemTypeID, storeID, portalID, userName, culture);
        return frmItemAttributes;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveItemAndAttributes(int itemID, int itemTypeID, int attributeSetID, int storeID, int portalID, string userName, string culture, bool isActive, bool isModified, int taxRuleID, string categoriesIds, string relatedItemsIds, string upSellItemsIds, string crossSellItemsIds, string downloadItemsValue, bool updateFlag, string sourceFileCol, string dataCollection,int itemLargeThumbNailSize,int itemMediumThumbNailSize, int itemSmallThumbNailSize, AspxNameValue[] formVars)
    {
        try
        {
            string uplodedDownlodableFormValue = string.Empty;

            if (itemTypeID == 2 && downloadItemsValue != "")
            {
                FileHelperController downLoadableObj = new FileHelperController();
                string tempFolder = @"Upload\temp";
                uplodedDownlodableFormValue = downLoadableObj.MoveFileToDownlodableItemFolder(tempFolder, downloadItemsValue, @"Modules/AspxCommerce/AspxItemsManagement/DownloadableItems/", itemID, "item_");
            }

            ItemsController itemController = new ItemsController();
            itemID = itemController.SaveUpdateItemAndAttributes(itemID, itemTypeID, attributeSetID, storeID, portalID, userName, culture, taxRuleID, categoriesIds, relatedItemsIds, upSellItemsIds, crossSellItemsIds, uplodedDownlodableFormValue, formVars);
            //return "({\"returnStatus\":1,\"Message\":'Item saved successfully.'})";
            if (itemID > 0 && sourceFileCol != "" && dataCollection != "")
            {
                dataCollection = dataCollection.Replace("../", "");
                SaveImageContents(itemID, @"Modules/AspxCommerce/AspxItemsManagement/uploads/", sourceFileCol, dataCollection,itemLargeThumbNailSize,itemMediumThumbNailSize,itemSmallThumbNailSize, "item_");
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CategoryInfo> GetCategoryList(string prefix, bool isActive, string cultureName, Int32 storeID, Int32 portalID, string userName, int itemId)
    {
        ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
        List<CategoryInfo> catList = obj.GetCategoryList(prefix, isActive, cultureName, storeID, portalID, userName, itemId);
        return catList;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public string SaveImageContents(Int32 itemID, string imageRootPath, string sourceFileCol, string dataCollection,int itemLargeThumbNailSize,int itemMediumThumbNailSize, int itemSmallThumbNailSize, string imgPreFix)
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
            words = str.Split(',');
            sbPathList.Append(words[0] + ",");
            sbIsActiveList.Append(words[1] + ",");
            sbImageType.Append(words[2] + ",");
            sbDescription.Append(words[3] + ",");
            sbDisplayOrder.Append(words[4] + ",");
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

        if (pathList.Contains(","))
        {
            pathList = pathList.Remove(pathList.LastIndexOf(","));
        }
        if (isActive.Contains(","))
        {
            isActive = isActive.Remove(isActive.LastIndexOf(","));
        }
        if (imageType.Contains(","))
        {
            imageType = imageType.Remove(imageType.LastIndexOf(","));
        }

        if (sourceFileCol.Contains(","))
        {
            sourceFileCol = sourceFileCol.Remove(sourceFileCol.LastIndexOf(","));
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemsInfoSettings> GetImageContents(int itemID)
    {
        List<ItemsInfoSettings> itemsImages = new List<ItemsInfoSettings>();
        ImageGallerySqlProvider imageGalleryManager = new ImageGallerySqlProvider();
        itemsImages = imageGalleryManager.GetItemsImageGalleryInfoByItemID(itemID);
        return itemsImages;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemCostVariantInfo> GetItemCostVariants(int offset, int limit, int storeID, int portalID, string cultureName, int itemID)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            SQLHandler sqlH = new SQLHandler();
            List<ItemCostVariantInfo> bind = sqlH.ExecuteAsList<ItemCostVariantInfo>("usp_Aspx_BindItemCostVariantsInGrid", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //------------------------ delete Item Cost Variants management------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteSingleItemCostVariant(string itemCostVariantID, int itemId, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemCostVariantsID", itemCostVariantID));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteSingleItemCostVariants", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //------------------------ add Item Cost Variants ------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void AddItemCostVariant(int itemId, int costVariantID, int storeId, int portalId, string cultureName, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
            ParaMeter.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_InsertItemCostVariant", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //--------------- bind (edit) item cost Variant details --------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CostVariantsGetByCostVariantIDInfo> GetItemCostVariantInfoByCostVariantID(int itemCostVariantId, int itemId, int costVariantID, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<CostVariantsGetByCostVariantIDInfo> bind = new List<CostVariantsGetByCostVariantIDInfo>();
            SQLHandler Sq = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ItemCostVariantsID", itemCostVariantId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            return Sq.ExecuteAsList<CostVariantsGetByCostVariantIDInfo>("usp_Aspx_ItemCostVariantsGetByCostVariantID", ParaMeterCollection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //--------------- bind (edit) item cost Variant values for cost variant ID --------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CostVariantsvalueInfo> GetItemCostVariantValuesByCostVariantID(int itemCostVariantId, int itemId, int costVariantID, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<CostVariantsGetByCostVariantIDInfo> bind = new List<CostVariantsGetByCostVariantIDInfo>();
            SQLHandler Sq = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ItemCostVariantsID", itemCostVariantId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            return Sq.ExecuteAsList<CostVariantsvalueInfo>("usp_Aspx_GetItemCostVariantValuesByCostVariantID", ParaMeterCollection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //------------------------ delete costvariant value ------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteCostVariantValue(int costVariantValueID, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            if (costVariantValueID > 0)
            {
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("@CostVariantValueID", costVariantValueID));
                ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
                ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteCostVariantValue", ParaMeter);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //-----------Save and update Item Costvariant options-------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveAndUpdateItemCostVariant(int costVariantID, string costVariantName, string description, string cultureName, int itemId, int inputTypeID,
        int displayOrder, bool showInGrid, bool showInSearch, bool showInAdvanceSearch, bool showInComparison, bool isEnableSorting, bool isUseInFilter,
        bool isIncludeInPriceRule, bool isIncludeInPromotions, bool isShownInRating, int storeId, int portalId,
        bool isActive, bool isModified, string userName, string variantOptions, bool isNewflag)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CostVariantName", costVariantName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Description", description));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", inputTypeID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ShowInGrid", showInGrid));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ShowInSearch", showInSearch));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ShowInAdvanceSearch", showInAdvanceSearch));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ShowInComparison", showInComparison));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsEnableSorting", isEnableSorting));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsUseInFilter", isUseInFilter));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule", isIncludeInPriceRule));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPromotions", isIncludeInPromotions));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsShownInRating", isShownInRating));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsModified", isModified));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@VariantOption", variantOptions));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsNewFlag", isNewflag));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CostVariantsValueID", 0));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ItemCostVariantsID", 0));
            SQLHandler Sq = new SQLHandler();
            Sq.ExecuteNonQuery("usp_Aspx_SaveAndUpdateItemCostVariants", ParaMeterCollection);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    //------------------------ delete item costvariant value ------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteItemCostVariantValue(int itemCostVariantId, int costVariantValueId, int itemId, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            if (itemCostVariantId > 0 && costVariantValueId > 0)
            {
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("@ItemCostVariantsID", itemCostVariantId));
                ParaMeter.Add(new KeyValuePair<string, object>("@CostVariantValueID", costVariantValueId));
                ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
                ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteItemCostVariantValue", ParaMeter);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    #endregion

    #region Item Tax

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TaxRulesInfo> GetAllTaxRules(int storeID, int portalID, bool isActive)
    {
        ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
        List<TaxRulesInfo> lstTaxManageRule = obj.GetAllTaxRules(storeID, portalID, isActive);
        return lstTaxManageRule;
    }
    #endregion

    #region Downloadable Item Details
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<DownLoadableItemInfo> GetDownloadableItem(int storeId, int portalId, string userName, string cultureName, int ItemID)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", ItemID));
            SQLHandler sqlh = new SQLHandler();
            return sqlh.ExecuteAsList<DownLoadableItemInfo>("usp_Aspx_GetDownloadableItem", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #endregion

    #region Front Image Gallery
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemsInfoSettings> GetItemsImageGalleryInfoBySKU(string itemSKU,int storeID, int portalID,string costVariantIDs)
    {
        List<ItemsInfoSettings> itemsInfo = new List<ItemsInfoSettings>();
        ImageGallerySqlProvider imageSqlProvider = new ImageGallerySqlProvider();
        itemsInfo = imageSqlProvider.GetItemsImageGalleryInfoByItemSKU(itemSKU,storeID,portalID,costVariantIDs);
        return itemsInfo;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ImageGalleryItemsInfo> GetItemsImageGalleryInfo(Int32 storeID, Int32 portalID, string userName, string culture)
    {
        List<ImageGalleryItemsInfo> itemsInfo = new List<ImageGalleryItemsInfo>();
        ImageGallerySqlProvider imageSettingsProvider = new ImageGallerySqlProvider();
        itemsInfo = imageSettingsProvider.GetItemsImageGalleryList(storeID, portalID, userName, culture);
        return itemsInfo;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ImageGalleryItemsInfo> GetItemsGalleryInfo(Int32 storeID, Int32 portalID, string culture)
    {
        List<ImageGalleryItemsInfo> itemsInfo = new List<ImageGalleryItemsInfo>();
        ImageGallerySqlProvider imageSettingsProvider = new ImageGallerySqlProvider();
        itemsInfo = imageSettingsProvider.GetItemInfoList(storeID, portalID, culture);
        return itemsInfo;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public ImageGalleryInfo ReturnSettings(Int32 UserModuleID, Int32 PortalID, string culture)
    {
        ImageGalleryInfo gallerySettingsInfo = new ImageGalleryInfo();
        ImageGallerySqlProvider settings = new ImageGallerySqlProvider();
        gallerySettingsInfo = settings.GetGallerySettingValues(UserModuleID, PortalID, culture);
        return gallerySettingsInfo;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<int> ReturnDimension(Int32 UserModuleID, Int32 PortalID, string culture)
    {
        List<int> param = new List<int>();
        ImageGalleryInfo info = new ImageGalleryInfo();
        ImageGallerySqlProvider settings = new ImageGallerySqlProvider();

        info = settings.GetGallerySettingValues(UserModuleID, PortalID, culture);
        param.Add(int.Parse(info.ImageWidth));
        param.Add(int.Parse(info.ImageHeight));
        param.Add(int.Parse(info.ThumbWidth));
        param.Add(int.Parse(info.ThumbHeight));
        //param.Add(int.Parse(info.ZoomShown));
        return param;
    }
    #endregion

    #region Category Management

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST")]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CategoryAttributeInfo> GetCategoryByCategoryID(Int32 categoryID, Int32 storeID, Int32 portalID, string userName, string culture)
    {
        CategorySqlProvider categorySqlProvider = new CategorySqlProvider();
        List<CategoryAttributeInfo> catList = categorySqlProvider.GetCategoryByCategoryID(categoryID, storeID, portalID, userName, culture);
        return catList;
    }

    [OperationContract]
    [WebInvoke(Method = "POST")]
    public string SaveCategory(Int32 storeID, Int32 portalID, Int32 categoryID, Int32 parentID, AspxNameValue[] formVars, string selectedItems, string userName, string culture, int categoryLargeThumbImage, int categoryMediumThumbImage, int categorySmallThumbImage)
    {
        try
        {
            CategoryController categoryController = new CategoryController();
            categoryController.SaveCategory(storeID, portalID, categoryID, parentID, formVars, selectedItems, userName, culture, categoryLargeThumbImage, categoryMediumThumbImage, categorySmallThumbImage);
            return "({\"returnStatus\":1,\"Message\":\"Category save successfully.\"})";
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

    [OperationContract]
    [WebInvoke(Method = "POST")]
    public string DeleteCategory(Int32 storeID, Int32 portalID, Int32 categoryID, string userName, string culture)
    {
        try
        {
            CategorySqlProvider categorySqlProvider = new CategorySqlProvider();
            categorySqlProvider.DeleteCategory(storeID, portalID, categoryID, userName, culture);
            return "({ \"returnStatus\" : 1 , \"Message\" : \"Category delete successfully.\" })";
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

    [OperationContract]
    [WebInvoke(Method = "POST")]
    public List<CategoryItemInfo> GetCategoryItems(Int32 offset, Int32 limit, Int32 categoryID, string sku, string name, System.Nullable<decimal> priceFrom, System.Nullable<decimal> priceTo, Int32 storeID, Int32 portalID, string userName, string culture)
    {
        try
        {
            List<CategoryItemInfo> listCategoryItem = new List<CategoryItemInfo>();
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

    [OperationContract]
    [WebInvoke(Method = "POST")]
    public string SaveChangesCategoryTree(Int32 storeID, Int32 portalID, string categoryIDs, string userName)
    {
        try
        {
            CategorySqlProvider categorySqlProvider = new CategorySqlProvider();
            categorySqlProvider.SaveChangesCategoryTree(storeID, portalID, categoryIDs, userName);
            return "({ \"returnStatus\" : 1 , \"Message\" : \"Save category tree successfully.\" })";
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
    //    [OperationContract]
    //    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //public List<ItemsGetCategoryIDInfo> BindCategoryItems(int categoryID, int storeID, int portalID, string userName, string cultureName)
    //{
    //    try
    //    {
    //        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
    //        ParaMeter.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
    //        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
    //        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
    //        ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
    //        ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
    //        SQLHandler sqlH = new SQLHandler();
    //        List<ItemsGetCategoryIDInfo> Bind = sqlH.ExecuteAsList<ItemsGetCategoryIDInfo>("usp_Aspx_ItemsGetAllBycategoryID", ParaMeter);
    //        return Bind;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //----------------General Search View As DropDown Options----------------------------

    #region Featured Items Management
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveCompareItems(int ID, int storeID, int portalID, string userName, string IP, string countryName, string sessionCode)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", ID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CompareItemID", 0));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@IP", IP));
            ParaMeter.Add(new KeyValuePair<string, object>("@CountryName", countryName));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_AddItemsToCompare", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemsCompareInfo> GetItemCompareList(int storeID, int portalID, string userName, string cultureName, string sessionCode)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ItemsCompareInfo>("usp_Aspx_GetCompareItemsList", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteCompareItem(int ID, int storeID, int portalID, string userName, string sessionCode)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", ID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("[usp_Aspx_DeleteCompareItem]", ParaMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void ClearAll(int storeID, int portalID, string userName, string sessionCode)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("[usp_Aspx_ClearCompareItems]", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public bool CheckCompareItems(int ID, int storeID, int portalID, string userName, string sessionCode)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", ID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsGivenType<bool>("[usp_Aspx_CheckCompareItems]", ParaMeter, "@IsExist");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------Compare Items Details view-------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemBasicDetailsInfo> GetCompareListImage(string itemIDs, int storeID, int portalID, string userName, string cultureName)
    {
        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
        ParaMeter.Add(new KeyValuePair<string, object>("@ItemIDs", itemIDs));
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
        ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        SQLHandler sqlH = new SQLHandler();
        return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetCompareList", ParaMeter);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CompareItemListInfo> GetCompareList(string itemIDs, int storeID, int portalID, string userName, string cultureName)
    {
        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
        ParaMeter.Add(new KeyValuePair<string, object>("@ItemIDs", itemIDs));
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
        ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        SQLHandler sqlH = new SQLHandler();
        return sqlH.ExecuteAsList<CompareItemListInfo>("usp_Aspx_GetItemCompareList", ParaMeter);
    }

    #region RecentlyComparedProducts
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void AddComparedItems(string IDs, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemIDs", IDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_AddComparedItems", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemsCompareInfo> GetRecentlyComparedItemList(int count, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@Count", count));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ItemsCompareInfo>("usp_Aspx_GetRecentlyComparedItemList", ParaMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
    #endregion

    #region WishItems
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public bool CheckWishItems(int ID, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", ID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsGivenType<bool>("[usp_Aspx_CheckWishItems]", ParaMeter, "@IsExist");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveWishItems(int ID, int storeID, int portalID, string userName, string IP, string countryName)
    {
        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
        ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", ID));
        ParaMeter.Add(new KeyValuePair<string, object>("@WishItemID", 0));
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
        ParaMeter.Add(new KeyValuePair<string, object>("@IP", IP));
        ParaMeter.Add(new KeyValuePair<string, object>("@CountryName", countryName));
        SQLHandler sqlH = new SQLHandler();
        sqlH.ExecuteNonQuery("usp_Aspx_SaveWishItems", ParaMeter);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<WishItemsInfo> GetWishItemList(int storeID, int portalID, string userName, string cultureName, string flagShowAll, int count)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@flag", flagShowAll));
            ParaMeter.Add(new KeyValuePair<string, object>("@Count", count));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<WishItemsInfo>("usp_Aspx_GetWishItemList", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteWishItem(int ID, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("ItemID", ID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteWishItem", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void UpdateWishList(string ID, string comment, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("ItemID", ID));
            ParaMeter.Add(new KeyValuePair<string, object>("@Comment", comment));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_UpdateWishItem", ParaMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void ClearWishList(int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_ClearWishItem", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //-------------------------Save AND SendEmail Messages For ShareWishList----------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void ShareWishListEmailSend(int StoreID, int PortalID, string ItemID, string SenderName, string SenderEmail, string ReceiverEmailID, string Subject, string Message, string Link, string CultureName)
    {
        try
        {
            string bodyDetail = Link;
            ReferToFriendSqlHandler obj = new ReferToFriendSqlHandler();
            obj.SaveShareWishListEmailMessage(StoreID, PortalID, ItemID, SenderName, SenderEmail, ReceiverEmailID, Subject, Message, CultureName);
            obj.SendShareWishItemEmail(StoreID, PortalID, SenderName, SenderEmail, ReceiverEmailID, Subject,Message ,bodyDetail, CultureName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public int CountWishItems(int storeID, int portalID, string sessionCode, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsScalar<int>("usp_Aspx_GetWishItemsCount", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Related Items You may also like
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemBasicDetailsInfo> GetYouMayAlsoLikeItemsListByItemSKU(string itemSKU, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetYouMayAlsoLikeItemsByItemSKU", ParaMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region RecentlyViewedItems

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<RecentlyViewedItemsInfo> GetRecentlyViewedItems(int count, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@Count", count));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            List<RecentlyViewedItemsInfo> view = sqlH.ExecuteAsList<RecentlyViewedItemsInfo>("usp_Aspx_GetRecentlyViewedItemList", ParaMeter);
            return view;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void AddUpdateRecentlyViewedItems(string itemSKU, string sessionCode, string IP, string countryName, string userName, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            ParaMeter.Add(new KeyValuePair<string, object>("@ViewFromIP", IP));
            ParaMeter.Add(new KeyValuePair<string, object>("@ViewedFromCountry", countryName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_AddRecentlyViewedItems", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Item Details Module
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public ItemBasicDetailsInfo GetItemBasicInfoByitemSKU(string itemSKU, int storeID, int portalID, string userName, string culture)
    {
        ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
        ItemBasicDetailsInfo frmItemAttributes = obj.GetItemBasicInfo(itemSKU, storeID, portalID, userName, culture);
        return frmItemAttributes;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemCostVariantsInfo> GetCostVariantsByitemSKU(string _itemSKU, int _storeID, int _portalID, string _cultureName, string _userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@itemSKU", _itemSKU));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", _storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", _portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", _cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", _userName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ItemCostVariantsInfo>("usp_Aspx_GetCostVariantsByItemID", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<AttributeFormInfo> GetItemDetailsByitemSKU(string itemSKU, int attributeSetID, int itemTypeID, int storeID, int portalID, string userName, string culture)
    {
        ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
        List<AttributeFormInfo> frmItemAttributes = obj.GetItemDetailsInfoByItemSKU(itemSKU, attributeSetID, itemTypeID, storeID, portalID, userName, culture);
        return frmItemAttributes;
    }

    #endregion

    #region PopularTags Module

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void AddTagsOfItem(string itemSKU, string Tags, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@Tags", Tags));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_AddTagsOfItem", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemTagsInfo> GetItemTags(string itemSKU, int storeID, int portalID)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ItemTagsInfo>("[usp_Aspx_GetTagsByItemID]", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteUserOwnTag(string itemTagID, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemTagID", itemTagID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteUserOwnTag", ParaMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TagDetailsInfo> GetTagDetailsListPending(int offset, int limit, string tag, int storeId, int portalId, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@Tags", tag));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            SQLHandler sqlH = new SQLHandler();
            List<TagDetailsInfo> nir = sqlH.ExecuteAsList<TagDetailsInfo>("[dbo].[usp_Aspx_GetAllTagsPending]", ParaMeter);
            return nir;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TagDetailsInfo> GetTagDetailsList(int offset, int limit, string tag, string tagStatus, int storeId, int portalId, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@Tags", tag));
            ParaMeter.Add(new KeyValuePair<string, object>("@TagStatus", tagStatus));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            SQLHandler sqlH = new SQLHandler();
            List<TagDetailsInfo> nir = sqlH.ExecuteAsList<TagDetailsInfo>("usp_Aspx_GetAllTags", ParaMeter);
            return nir;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TagDetailsInfo> GetAllPopularTags(int storeID, int portalID, int count)
    {
        try
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@Count", count));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<TagDetailsInfo>("usp_Aspx_GetPopularTags", parameterCollection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CustomerTagInfo> GetCustomerTagDetailsList(int offset, int limit, int storeId, int portalId)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            SQLHandler sqlH = new SQLHandler();
            List<CustomerTagInfo> bhu = sqlH.ExecuteAsList<CustomerTagInfo>("usp_Aspx_GetCustomerItemTags", ParaMeter);
            return bhu;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //---------------------Show Customer tags list------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShowCustomerTagsListInfo> ShowCustomerTagList(int offset, int limit, int storeId, int portalId, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            List<ShowCustomerTagsListInfo> bhu = sqlH.ExecuteAsList<ShowCustomerTagsListInfo>("usp_Aspx_ShowCustomerTagList", ParaMeter);
            return bhu;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //---------------------Item tags details------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemTagsDetailsInfo> GetItemTagDetailsList(int offset, int limit, int storeId, int portalId)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            SQLHandler sqlH = new SQLHandler();
            List<ItemTagsDetailsInfo> bhu = sqlH.ExecuteAsList<ItemTagsDetailsInfo>("usp_Aspx_GetItemTagsDetails", ParaMeter);
            return bhu;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //---------------------Show Item tags list------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShowItemTagsListInfo> ShowItemTagList(int offset, int limit, int storeId, int portalId, int itemID)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            SQLHandler sqlH = new SQLHandler();
            List<ShowItemTagsListInfo> bhu = sqlH.ExecuteAsList<ShowItemTagsListInfo>("usp_Aspx_ShowTagsByItems", ParaMeter);
            return bhu;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //---------------------Popular tags details------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<PopularTagsInfo> GetPopularTagDetailsList(int offset, int limit, int storeId, int portalId)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            SQLHandler sqlH = new SQLHandler();
            List<PopularTagsInfo> bhu = sqlH.ExecuteAsList<PopularTagsInfo>("usp_Aspx_GetPopularityTags", ParaMeter);
            return bhu;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //---------------------Show Popular tags list------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShowpopulartagsDetailsInfo> ShowPopularTagList(int offset, int limit, int storeId, int portalId, string tagName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeter.Add(new KeyValuePair<string, object>("@TagName", tagName));
            SQLHandler sqlH = new SQLHandler();
            List<ShowpopulartagsDetailsInfo> bhu = sqlH.ExecuteAsList<ShowpopulartagsDetailsInfo>("usp_Aspx_ShowPopularTagsDetails", ParaMeter);
            return bhu;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemBasicDetailsInfo> GetUserTaggedItems(string TagIDs, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@TagIDs", TagIDs));
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void UpdateTag(string ItemTagIDs, string newTag, int StatusID, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemTagIDs", ItemTagIDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@NewTag", newTag));
            ParaMeter.Add(new KeyValuePair<string, object>("@StatusID", StatusID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_UpdateTag", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteTag(string ItemTagIDs, int storeID, int portalID)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemTagIDs", ItemTagIDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteTag", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemBasicDetailsInfo> GetItemsByMultipleItemID(string ItemIDs, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemIDs", ItemIDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetItemsByMultipleItemID", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    #endregion

    #region ShoppingOptions
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<DisplayItemsOptionsInfo> BindItemsViewAsList()
    {
        try
        {

            SQLHandler sqlH = new SQLHandler();
            List<DisplayItemsOptionsInfo> Bind = sqlH.ExecuteAsList<DisplayItemsOptionsInfo>("usp_Aspx_DisplayItemViewAsOptions");
            return Bind;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //--------------------ShoppingOptionsByPrice----------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShoppingOptionsInfo> ShoppingOptionsByPrice(int storeID, int portalID, string userName, string cultureName, int upperLimit)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@Limit", upperLimit));
            SQLHandler sqlH = new SQLHandler();
            List<ShoppingOptionsInfo> Count = sqlH.ExecuteAsList<ShoppingOptionsInfo>("usp_Aspx_ShoppingOptions", ParaMeter);
            return Count;


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //--------------------ShoppingOptionsByPriceResults----------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemBasicDetailsInfo> GetShoppingOptionsItemsResult(string itemIds, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemIDs", itemIds));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetShoppingOptionsItemsResult", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Search
    //Auto Complete search Box
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<SearchTermList> GetSearchedTermList(string search, int storeID, int portalID)
    {
        List<SearchTermList> srInfo = new List<SearchTermList>();

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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemBasicDetailsInfo> GetSimpleSearchResult(int categoryID, string searchText, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SearchText", searchText));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetSimpleSearchResult", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<SortOptionTypeInfo> BindItemsSortByList()
    {
        try
        {

            SQLHandler sqlH = new SQLHandler();
            List<SortOptionTypeInfo> Bind = sqlH.ExecuteAsList<SortOptionTypeInfo>("usp_Aspx_DisplayItemSortByOptions");
            return Bind;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemBasicDetailsInfo> GetItemsByGeneralSearch(int storeID, int portalID, string NameSearchText, float PriceFrom, float PriceTo,
        string SKUSearchText, int CategoryID, string CategorySearchText, bool IsByName, bool IsByPrice, bool IsBySKU, bool IsByCategory, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@NameSearchText", NameSearchText));
            ParaMeter.Add(new KeyValuePair<string, object>("@PriceFrom", PriceFrom));
            ParaMeter.Add(new KeyValuePair<string, object>("@PriceTo", PriceTo));
            ParaMeter.Add(new KeyValuePair<string, object>("@SKUSearchText", SKUSearchText));
            ParaMeter.Add(new KeyValuePair<string, object>("@CategoryID", CategoryID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CategorySearchText", CategorySearchText));
            ParaMeter.Add(new KeyValuePair<string, object>("@IsByName", IsByName));
            ParaMeter.Add(new KeyValuePair<string, object>("@IsByPrice", IsByPrice));
            ParaMeter.Add(new KeyValuePair<string, object>("@IsBySKU", IsBySKU));
            ParaMeter.Add(new KeyValuePair<string, object>("@IsByCategory", IsByCategory));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetItemsByGeneralSearch", ParaMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<AttributeShowInAdvanceSearchInfo> GetAttributes(int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<AttributeShowInAdvanceSearchInfo>("usp_Aspx_GetAttributesShowInAdvanceSearch", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //------------------get items by dyanamic Advance serach-----------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemBasicDetailsInfo> GetItemsByDyanamicAdvanceSearch(int storeID, int portalID, System.Nullable<int> categoryID, string SearchText, string checkValue,
        System.Nullable<float> PriceFrom, System.Nullable<float> PriceTo, string userName, string cultureName, string attributeIDS)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SearchText", SearchText));
            ParaMeter.Add(new KeyValuePair<string, object>("@CheckValues", checkValue));
            ParaMeter.Add(new KeyValuePair<string, object>("@PriceFrom", PriceFrom));
            ParaMeter.Add(new KeyValuePair<string, object>("@PriceTo", PriceTo));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@AttributeIDs", attributeIDS));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetItemsByDynamicAdvanceSearch", ParaMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    #endregion

    #region Category Details
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CategoryDetailsOptionsInfo> GetCategoryDetailsOptions(string categorykey, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemRatingAverageInfo> GetItemAverageRating(string itemSKU, int storeID, int portalID, string cultureName)
    {
        try
        {
            //ItemRatingAverageInfo
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            List<ItemRatingAverageInfo> avgRating = sqlH.ExecuteAsList<ItemRatingAverageInfo>("usp_Aspx_ItemRatingGetAverage", ParaMeter);
            return avgRating;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------rating/ review Items criteria--------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<RatingCriteriaInfo> GetItemRatingCriteria(int storeID, int portalID, string cultureName, bool isFlag)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@IsFlag", isFlag));
            List<RatingCriteriaInfo> rating = sqlH.ExecuteAsList<RatingCriteriaInfo>("usp_Aspx_GetItemRatingCriteria", ParaMeter);
            return rating;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------save rating/ review Items-----------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveItemRating(string ratingCriteriaValue, int statusID, string summaryReview, string review, string userIP, string viewFromCountry, int itemID, int storeID, int portalID, string nickName, string addedBy)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@RatingCriteriaValue", ratingCriteriaValue));
            ParaMeter.Add(new KeyValuePair<string, object>("@StatusID", statusID));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemReviewID", 0));
            ParaMeter.Add(new KeyValuePair<string, object>("@ReviewSummary", summaryReview));
            ParaMeter.Add(new KeyValuePair<string, object>("@Review", review));
            ParaMeter.Add(new KeyValuePair<string, object>("@ViewFromIP", userIP));
            ParaMeter.Add(new KeyValuePair<string, object>("@ViewFromCountry", viewFromCountry));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@Username", nickName));
            ParaMeter.Add(new KeyValuePair<string, object>("@AddedBy", addedBy));
            sqlH.ExecuteNonQuery("usp_Aspx_SaveItemRating", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------update rating/ review Items-----------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void UpdateItemRating(string ratingCriteriaValue, int statusID, string summaryReview, string review, int itemReviewID, string viewFromIP, string viewFromCountry, int itemID, int storeID, int portalID, string nickName, string userName)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@RatingCriteriaValue", ratingCriteriaValue));
            ParaMeter.Add(new KeyValuePair<string, object>("@StatusID", statusID));
            ParaMeter.Add(new KeyValuePair<string, object>("@ReviewSummary", summaryReview));
            ParaMeter.Add(new KeyValuePair<string, object>("@Review", review));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
            ParaMeter.Add(new KeyValuePair<string, object>("@ViewFromIP", viewFromIP));
            ParaMeter.Add(new KeyValuePair<string, object>("@ViewFromCountry", viewFromCountry));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@Username", nickName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserBy", userName));
            sqlH.ExecuteNonQuery("usp_Aspx_UpdateItemRating", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------Get rating/ review of Item Per User ------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemRatingByUserInfo> GetItemRatingPerUser(string itemSKU, int storeID, int portalID, string cultureName)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            return sqlH.ExecuteAsList<ItemRatingByUserInfo>("usp_Aspx_GetItemAverageRatingByUser", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------Get rating/ review of Item Per User ------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<RatingLatestInfo> GetRecentItemReviewsAndRatings(int storeID, int portalID, string cultureName)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            return sqlH.ExecuteAsList<RatingLatestInfo>("usp_Aspx_GetRecentReviewsAndRatings", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------Get rating/ review of Item Per User ------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemReviewDetailsInfo> GetItemRatingByReviewID(int itemReviewID, int storeID, int portalID, string cultureName)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            return sqlH.ExecuteAsList<ItemReviewDetailsInfo>("usp_Aspx_GetItemReviewDetails", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //------------------------Item single rating management------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteSingleItemRating(string ItemReviewID, int storeID, int portalID)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemReviewID", ItemReviewID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteSingleItemRatingInformation", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //---------------Delete multiple item rating informations--------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteMultipleItemRatings(string itemReviewIDs, int storeId, int portalId)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemReviewIDs", itemReviewIDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleSelectionItemRating", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------Bind in Item Rating Information in grid-------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<UserRatingInformationInfo> GetAllUserReviewsAndRatings(int offset, int limit, int storeID, int portalID, string userName, string statusName, string itemName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@StatusName", statusName));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemName", itemName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetAllReviewsAndRatings", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //-------------------------list item names in dropdownlist/item rating management---------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemsReviewInfo> GetAllItemList(int storeID, int portalID, string cultureName)
    {
        try
        {
            //ItemRatingAverageInfo
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();

            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            List<ItemsReviewInfo> items = sqlH.ExecuteAsList<ItemsReviewInfo>("usp_Aspx_GetAllItemsListReview", ParaMeter);
            return items;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Item Rating Criteria Manage/Admin
    //--------------------Item Rating Criteria Manage/Admin--------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemRatingCriteriaInfo> ItemRatingCriteriaManage(int offset, int limit, string ratingCriteria, System.Nullable<bool> isActive, int storeId, int portalId, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@RatingCriteria", ratingCriteria));
            ParaMeter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ItemRatingCriteriaInfo>("usp_Aspx_GetAllItemRatingCriteria", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //--------------- ItemRating Criteria Manage-------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void AddUpdateItemCriteria(int ID, string criteria, string IsActive, int storeID, int portalID, string cultureName, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ID", ID));
            ParaMeter.Add(new KeyValuePair<string, object>("@Criteria", criteria));
            ParaMeter.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateItemRatingCriteria", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //--------------- ItemRating Criteria Manage-------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteItemRatingCriteria(string IDs, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@CriteriaID", IDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteItemRatingCriteria", ParaMeter);
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CostVariantInfo> GetCostVariants(int offset, int limit, string variantName, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@VariantName", variantName));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            List<CostVariantInfo> bind = sqlH.ExecuteAsList<CostVariantInfo>("usp_Aspx_BindCostVariantsInGrid", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------Delete multiple cost variants --------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteMultipleCostVariants(string costVariantIDs, int storeId, int portalId, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@CostVariantIds", costVariantIDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleCostVariants", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //------------------------ single Cost Variants management------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteSingleCostVariant(string CostVariantID, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@CostVariantID", CostVariantID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteSingleCostVariants", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    public List<AttributesInputTypeInfo> GetCostVariantInputTypeList()
    {
        try
        {
            List<AttributesInputTypeInfo> ml = new List<AttributesInputTypeInfo>();
            SQLHandler Sq = new SQLHandler();
            ml = Sq.ExecuteAsList<AttributesInputTypeInfo>("dbo.usp_Aspx_CostVariantsInputTypeGetAll");
            return ml;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //--------------- bind (edit) cost Variant management--------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CostVariantsGetByCostVariantIDInfo> GetCostVariantInfoByCostVariantID(int costVariantID, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<CostVariantsGetByCostVariantIDInfo> bind = new List<CostVariantsGetByCostVariantIDInfo>();
            SQLHandler Sq = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            return Sq.ExecuteAsList<CostVariantsGetByCostVariantIDInfo>("usp_Aspx_CostVariantsGetByCostVariantID", ParaMeterCollection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //--------------- bind (edit) cost Variant values for cost variant ID --------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CostVariantsvalueInfo> GetCostVariantValuesByCostVariantID(int costVariantID, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<CostVariantsGetByCostVariantIDInfo> bind = new List<CostVariantsGetByCostVariantIDInfo>();
            SQLHandler Sq = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            return Sq.ExecuteAsList<CostVariantsvalueInfo>("usp_Aspx_GetCostVariantValuesByCostVariantID", ParaMeterCollection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //-----------Save and update Costvariant options-------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveAndUpdateCostVariant(int costVariantID, string costVariantName, string description, string cultureName, int inputTypeID,
        int displayOrder, bool showInGrid, bool showInSearch, bool showInAdvanceSearch, bool showInComparison, bool isEnableSorting, bool isUseInFilter,
        bool isIncludeInPriceRule, bool isIncludeInPromotions, bool isShownInRating, int storeId, int portalId,
        bool isActive, bool isModified, string userName, string variantOptions, bool isNewflag)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CostVariantName", costVariantName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Description", description));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", inputTypeID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ShowInGrid", showInGrid));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ShowInSearch", showInSearch));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ShowInAdvanceSearch", showInAdvanceSearch));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ShowInComparison", showInComparison));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsEnableSorting", isEnableSorting));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsUseInFilter", isUseInFilter));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule", isIncludeInPriceRule));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPromotions", isIncludeInPromotions));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsShownInRating", isShownInRating));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsModified", isModified));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@VariantOption", variantOptions));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsNewFlag", isNewflag));
            SQLHandler Sq = new SQLHandler();
            Sq.ExecuteNonQuery("usp_Aspx_SaveAndUpdateCostVariants", ParaMeterCollection);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    //---------------- Added for unique name check ---------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public bool CheckUniqueCostVariantName(string costVariantName, int costVariantId, int storeId, int portalId)
    {
        try
        {
            SQLHandler Sq = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CostVariantName", costVariantName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            return Sq.ExecuteNonQueryAsBool("usp_Aspx_CostVariantUniquenessCheck", ParaMeterCollection, "@IsUnique");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Refer-A-Friend
    //-------------------------Save AND SendEmail Messages For Refer-A-Friend----------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ReferToFriendInfo> GetAllReferToAFriendEmailList(int offset, int limit, string senderName, string senderEmail, string receiverName, string receiverEmail, string subject, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@SenderName", senderName));
            ParaMeter.Add(new KeyValuePair<string, object>("@SenderEmail", senderEmail));
            ParaMeter.Add(new KeyValuePair<string, object>("@ReceiverName", receiverName));
            ParaMeter.Add(new KeyValuePair<string, object>("@ReceiverEmail", receiverEmail));
            ParaMeter.Add(new KeyValuePair<string, object>("@Subject", subject));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            List<ReferToFriendInfo> bind = sqlH.ExecuteAsList<ReferToFriendInfo>("usp_Aspx_GetAllReferAFriendEmailsInGrid", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //-----------------Delete Email list --------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteReferToFriendEmailUser(string emailAFriendIDs, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@EmailAFriendID", emailAFriendIDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteReferToFriendUser", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------Get UserReferred Friends--------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ReferToFriendInfo> GetUserReferredFriends(int offset, int limit, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            paraMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paraMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlh = new SQLHandler();
            return sqlh.ExecuteAsList<ReferToFriendInfo>("usp_Aspx_GetUserReferredFriends", paraMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Shipping method management
    //-----------Bind Shipping methods In grid-----------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShippingProviderListInfo> GetShippingProviderList(int StoreID, int PortalID, string UserName, string CultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            SQLHandler Sq = new SQLHandler();
            return Sq.ExecuteAsList<ShippingProviderListInfo>("usp_Aspx_BindShippingProvider", ParaMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    ////--------------------------SaveAndUpdate shipping methods-------------------
    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //public void SaveAndUpdateShippingMethods(int shippingMethodID, string shippingMethodName, string prevFilePath, string newFilePath, string alternateText, int displayOrder, string deliveryTime,
    //        decimal weightLimitFrom, decimal weightLimitTo, int shippingProviderID, int storeID, int portalID, bool isActive, string userName, string cultureName)
    //{
    //    try
    //    {
    //        FileHelperController fileObj = new FileHelperController();
    //        string uplodedValue = string.Empty;
    //        if (newFilePath != null && prevFilePath != newFilePath)
    //        {
    //            string tempFolder = @"Upload\temp";
    //            uplodedValue = fileObj.MoveFileToSpecificFolder(tempFolder, prevFilePath, newFilePath, @"Modules\AspxCommerce\AspxShippingManagement\uploads\", shippingMethodID, "ship_");
    //        }
    //        else
    //        {
    //            uplodedValue = prevFilePath;
    //        }
    //        ShippingMethodSqlProvider obj = new ShippingMethodSqlProvider();
    //        obj.SaveAndUpdateShippings(shippingMethodID, shippingMethodName, uplodedValue, alternateText, displayOrder, deliveryTime, weightLimitFrom, weightLimitTo, shippingProviderID, storeID, portalID, isActive, userName, cultureName);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //--------------------bind Cost dependencies  in Grid--------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShippingCostDependencyInfo> GetCostDependenciesListInfo(int offset, int limit, int storeID, int portalID, int shippingMethodId)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodId));
            SQLHandler sqlH = new SQLHandler();
            List<ShippingCostDependencyInfo> bind = sqlH.ExecuteAsList<ShippingCostDependencyInfo>("usp_Aspx_BindShippingCostDependencies", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //--------------------bind Weight dependencies  in Grid--------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShippingWeightDependenciesInfo> GetWeightDependenciesListInfo(int offset, int limit, int storeID, int portalID, int shippingMethodId)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodId));
            SQLHandler sqlH = new SQLHandler();
            List<ShippingWeightDependenciesInfo> bind = sqlH.ExecuteAsList<ShippingWeightDependenciesInfo>("usp_ASPx_BindWeightDependencies", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //--------------------bind Item dependencies  in Grid--------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShippingItemDependenciesInfo> GetItemDependenciesListInfo(int offset, int limit, int storeID, int portalID, int shippingMethodId)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodId));
            SQLHandler sqlH = new SQLHandler();
            List<ShippingItemDependenciesInfo> bind = sqlH.ExecuteAsList<ShippingItemDependenciesInfo>("usp_Aspx_BindItemDependencies", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------Delete multiple cost Depandencies --------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteCostDependencies(string shippingProductCostIds, int storeId, int portalId, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ShippingProductCostIDs", shippingProductCostIds));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteCostDependencies", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------Delete multiple weight Depandencies --------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteWeightDependencies(string shippingProductWeightIds, int storeId, int portalId, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ShippingProductWeightIDs", shippingProductWeightIds));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteShippingWeightDependencies", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------Delete multiple item Depandencies --------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteItemDependencies(string shippingItemIds, int storeId, int portalId, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ShippingItemIDs", shippingItemIds));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteShippingItemDependencies", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //------------------save  cost dependencies----------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    #region Coupon Management
    #region Coupon Type Manage

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CouponTypeInfo> GetCouponTypeDetails(int offset, int limit, string couponTypeName, int storeId, int portalId, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@CouponTypeName", couponTypeName));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponTypeInfo>("usp_Aspx_GetAllCouponType", ParaMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void AddUpdateCouponType(int couponTypeID, string couponType, string isActive, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@CouponTypeID", couponTypeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CouponType", couponType));
            ParaMeter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateCouponType", ParaMeter);
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteCouponType(string IDs, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@CouponTypeID", IDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteCouponType", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Coupon Manage
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void AddUpdateCouponDetails(int couponID, int couponTypeID, string couponCode, string couponAmount, string validateFrom, string validateTo,
         string isActive, int storeID, int portalID, string cultureName, string userName, string settingIDs, string settingValues,
         string PortalUser_CustomerName, string PortalUser_EmailID, string PortalUser_UserName, string SenderEmail, string Subject, ArrayList MessageBody)
    {
        try
        {
            CouponManageSQLProvider cmSQLProvider = new CouponManageSQLProvider();
            cmSQLProvider.AddUpdateCoupons(couponID, couponTypeID, couponCode, couponAmount, validateFrom, validateTo, isActive, storeID, portalID, cultureName, userName, settingIDs, settingValues, PortalUser_UserName);
            if (PortalUser_EmailID != "")
            {
                cmSQLProvider.SendCouponCodeEmail(SenderEmail, PortalUser_EmailID, Subject, MessageBody);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    //----------------delete coupons(admin)-----------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void VerifyCouponCode(string couponCode, int storeID, int portalID, string userName)
    {
        try
        {
            //CouponManageSQLProvider cmSqlProvider = new CouponManageSQLProvider();
            //CouponVerificationInfo info = cmSqlProvider.VerifyUserCoupon(couponCode, storeID, portalID, userName);
            //return info;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //--------update wherever necessary after coupon verification is successful----------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void UpdateCouponUserRecord(string CouponCode, int storeID, int portalID, string userName)
    {
        try
        {
            CouponManageSQLProvider cmSQLProvider = new CouponManageSQLProvider();
            cmSQLProvider.UpdateCouponUserRecord(CouponCode, storeID, portalID, userName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Coupons Per Sales Management
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CouponPerSales> GetCouponDetailsPerSales(int offset, int limit, string couponCode, int storeID, int portalID)
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CouponUserInfo> GetCouponUserDetails(int offset, int limit, string couponCode, string userName, int couponStatusId, string validFrom, string validTo, int storeId, int portalId, string cultureName)
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteCouponSettingsKey(string SettingID, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@SettingIDs", SettingID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteCouponSettingsKey", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void AddUpdateCouponSettingKey(int ID, string settingKey, int validationTypeID, string isActive, int storeID, int portalID, string cultureName, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ID", ID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SettingKey", settingKey));
            ParaMeter.Add(new KeyValuePair<string, object>("@ValidationTypeID", validationTypeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateCouponSettingKey", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
    #endregion

    #region Admin DashBoard

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<SearchTermInfo> GetSearchStatistics(int count, string commandName, int storeID, int portalID, string cultureName)
    {
        SearchTermSQLProvider stSQLprovider = new SearchTermSQLProvider();
        return stSQLprovider.GetSearchStatistics(count, commandName, storeID, portalID, cultureName);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<LatestOrderStaticsInfo> GetLatestOrderItems(int count, int storeID, int portalID, string cultureName)
    {
        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
        ParaMeter.Add(new KeyValuePair<string, object>("@Count", count));
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        SQLHandler sqlH = new SQLHandler();
        return sqlH.ExecuteAsList<LatestOrderStaticsInfo>("usp_Aspx_GetLatestOrderStatics", ParaMeter);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<MostViewItemInfoAdminDash> GetMostViwedItemAdmindash(int count, int storeID, int portalID)
    {
        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
        ParaMeter.Add(new KeyValuePair<string, object>("@Count", count));
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        //ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        SQLHandler sqlH = new SQLHandler();
        return sqlH.ExecuteAsList<MostViewItemInfoAdminDash>("usp_Aspx_GetMostViewdItemAdminDashboard", ParaMeter);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<StaticOrderStatusAdminDashInfo> GetStaticOrderStatusAdminDash(int count, int storeID, int portalID)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@Count", count));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            //ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<StaticOrderStatusAdminDashInfo>("usp_Aspx_GetStaticOrderStatusAdminDash", ParaMeter);

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TopCustomerOrdererInfo> GetTopCustomerOrderAdmindash(int count, int storeID, int portalID)
    {
        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
        ParaMeter.Add(new KeyValuePair<string, object>("@Count", count));
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        //ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        SQLHandler sqlH = new SQLHandler();
        return sqlH.ExecuteAsList<TopCustomerOrdererInfo>("usp_Aspx_GetTopCustomerAdmindash", ParaMeter);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TotalOrderAmountInfo> GetTotalOrderAmountAdmindash(int storeID, int portalID)
    {
        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
        //  ParaMeter.Add(new KeyValuePair<string, object>("@Count", count));
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        //ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        SQLHandler sqlH = new SQLHandler();
        return sqlH.ExecuteAsList<TotalOrderAmountInfo>("usp_Aspx_GetTotalOrderAmountStatus", ParaMeter);
    }
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<InventoryDetailAdminDashInfo> GetInventoryDetails(int count, int storeID, int portalID)
    {
        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
        ParaMeter.Add(new KeyValuePair<string, object>("@LowStockCount", count));
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        SQLHandler sqlH = new SQLHandler();
        return sqlH.ExecuteAsList<InventoryDetailAdminDashInfo>("usp_Aspx_GetInventoryDetailsAdminDash", ParaMeter);
    }
    #endregion

    #region For User DashBoard
    //-------------------------Update Customer Account Information----------------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public int UpdateCustomer(int storeID, int portalID, int customerID, string userName, string firstName, string lastName, string email)
    {

        try
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FirstName", firstName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@LastName", lastName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Email", email));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            SQLHandler sqlh = new SQLHandler();
            int errorCode = sqlh.ExecuteNonQueryAsGivenType<int>("dbo.usp_Aspx_UpdateCustomer", ParaMeterCollection, "@ErrorCode");
            return errorCode;
        }
        catch (Exception)
        {
            throw;
        }


    }

    //---------------User Item Reviews and Ratings-----------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<UserRatingInformationInfo> GetUserReviewsAndRatings(int offset, int limit, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetUserItemReviews", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------update rating/ review Items From User DashBoard-----------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void UpdateItemRatingByUser(string ratingCriteriaValue, string summaryReview, string review, int itemReviewID, int itemID, int storeID, int portalID, string nickName, string userName)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@RatingCriteriaValue", ratingCriteriaValue));
            ParaMeter.Add(new KeyValuePair<string, object>("@ReviewSummary", summaryReview));
            ParaMeter.Add(new KeyValuePair<string, object>("@Review", review));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@Username", nickName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserBy", userName));
            sqlH.ExecuteNonQuery("usp_Aspx_UpdateItemRatingByUser", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //-----------User DashBoard/Recent History-------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<UserRecentHistoryInfo> GetUserRecentlyViewedItems(int offset, int limit, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<UserRecentHistoryInfo>("usp_Aspx_GetUserRecentlyViewedItems", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //-----------User DashBoard/Recent History-------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<UserRecentHistoryInfo> GetUserRecentlyComparedItems(int offset, int limit, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<UserRecentHistoryInfo>("usp_Aspx_GetUserRecentlyComparedItems", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteAddressBook(int addressID, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            UserDashboardSQLProvider DashBoardSqlProvider = new UserDashboardSQLProvider();
            DashBoardSqlProvider.DeleteAddressBookDetails(addressID, storeID, portalID, userName, cultureName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<UserProductReviewInfo> GetUserProductReviews(int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            UserDashboardSQLProvider DashBoardSqlProvider = new UserDashboardSQLProvider();
            return DashBoardSqlProvider.GetUserProductReviews(storeID, portalID, userName, cultureName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void UpdateUserProductReview(int itemID, int itemReviewID, string ratingIDs, string ratingValues, string reviewSummary, string review, int storeID, int portalID, string userName)
    {
        try
        {
            UserDashboardSQLProvider DashBoardSqlProvider = new UserDashboardSQLProvider();
            DashBoardSqlProvider.UpdateUserProductReview(itemID, itemReviewID, ratingIDs, ratingValues, reviewSummary, review, storeID, portalID, userName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteUserProductReview(int itemID, int itemReviewID, int storeID, int portalID, string userName)
    {
        UserDashboardSQLProvider dashSqlProvider = new UserDashboardSQLProvider();
        dashSqlProvider.DeleteUserProductReview(itemID, itemReviewID, storeID, portalID, userName);
    }

    //---------------userDashBord/My Order List in grid----------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<MyOrderListInfo> GetMyOrderList(int offset, int limit, int StoreID, int PortalID, int CustomerID, string CultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CustomerID", CustomerID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<MyOrderListInfo>("usp_Aspx_GetMyOrdersList", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //-----------------------UserDashBoard/ My Orders-------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<OrderItemsInfo> GetMyOrders(int orderID, int storeID, int portalID, int customerID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlh = new SQLHandler();
            List<OrderItemsInfo> info;
            info = sqlh.ExecuteAsList<OrderItemsInfo>("usp_Aspx_GetMyOrders", ParaMeter);
            return info;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    //-------------------------UserDashBoard/User Downloadable Items------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<DownloadableItemsByCustomerInfo> GetCustomerDownloadableItems(int offset, int limit, string Sku, string name, int storeId, int portalId, string cultureName, string userName)
    {
        try
        {
            List<DownloadableItemsByCustomerInfo> ml = new List<DownloadableItemsByCustomerInfo>();
            SQLHandler Sq = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@SKU", Sku));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Name", name));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            ml = Sq.ExecuteAsList<DownloadableItemsByCustomerInfo>("dbo.usp_Aspx_GetCustomerDownloadableItems", ParaMeterCollection);
            return ml;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void UpdateDownloadCount(int itemID, int orderItemID, string DownloadIP, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            ParaMeter.Add(new KeyValuePair<string, object>("@OrderItemID", orderItemID));
            ParaMeter.Add(new KeyValuePair<string, object>("@DownloadIP", DownloadIP));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_UpdateDownloadCount", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public bool CheckRemainingDownload(int itemId, int orderItemId, int storeId, int portalId, string userName)
    {
        try
        {
            SQLHandler Sq = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();

            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@OrderItemID", orderItemId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            return Sq.ExecuteNonQueryAsBool("dbo.usp_Aspx_CheckRemainingDownloadForCustomer", ParaMeterCollection, "@IsRemainDowload");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //---------------------------------------------------------------------------------------------------
    #endregion

    #region CartManage
    //------------------------------Check Cart--------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public bool AddtoCart(int itemID, int storeID, int portalID, string userName, string cultureName)
    {

        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckCostVariantForItem", ParaMeter, "@IsExist");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //------------------------------Cart Details--------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public decimal GetDiscountQuantityAmount(int storeID, int portalID, string userName, int customerID, string sessionCode)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsGivenType<decimal>("usp_Aspx_GetItemQuantityDiscountAmount", ParaMeter, "@QtyDiscount");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //------------------------------Delete Cart Items--------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    //------------------------------Get ShippingMethodByTotalItemsWeight--------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShippingCostInfo> GetShippingCostByItem(int storeID, int portalID, int customerID, string sessionCode, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();

            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ShippingCostInfo>("usp_Aspx_ShippingDetailsForItem", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void UpdateShoppingCart(int cartID, string quantitys, int storeID, int portalID, string cartItemIDs, string userName, string cultureName)
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public bool UpdateCartAnonymoususertoRegistered(int storeID, int portalID, int customerID, string sessionCode)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsBool("usp_Aspx_UpdateCartAnonymoususertoRegistered", ParaMeter, "@IsUpdate");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Quantity Discount Management
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemQuantityDiscountInfo> GetItemQuantityDiscountsByItemID(int itemId, int storeID, int portalID, string userName)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            return sqlH.ExecuteAsList<ItemQuantityDiscountInfo>("usp_Aspx_GetQuantityDiscountByItemID", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //------------------------save quantity discount------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveItemDiscountQuantity(string discountQuantity, int itemID, int storeID, int portalID, string userName)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@DiscountQuantity", discountQuantity));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            sqlH.ExecuteNonQuery("usp_Aspx_SaveItemQuantityDiscounts", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //------------------------delete quantity discount------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteItemQuantityDiscount(int quantityDiscountID, int itemID, int storeID, int portalID, string userName)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@QuantityDiscountID", quantityDiscountID));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteItemQuantityDiscounts", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //------------------------quantity discount shown in Item deatils ------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemQuantityDiscountInfo> GetItemQuantityDiscountByUserName(int storeID, int portalID, string userName, string itemSKU)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
            return sqlH.ExecuteAsList<ItemQuantityDiscountInfo>("usp_Aspx_GetItemQuantityDiscountByUserName", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Search Term Management
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void AddUpdateSearchTerm(string searchTerm, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            SearchTermSQLProvider stSQLprovider = new SearchTermSQLProvider();
            stSQLprovider.AddUpdateSearchTerm(searchTerm, storeID, portalID, userName, cultureName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<SearchTermInfo> ManageSearchTerms(int offset, int limit, int storeID, int portalID, string cultureName,string searchTerm)
    {
        try
        {
            SearchTermSQLProvider stSQLprovider = new SearchTermSQLProvider();
            return stSQLprovider.ManageSearchTerm(offset, limit, storeID, portalID, cultureName,searchTerm);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteSearchTerm(string searchTermID, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            SearchTermSQLProvider stSQLprovider = new SearchTermSQLProvider();
            stSQLprovider.DeleteSearchTerm(searchTermID, storeID, portalID, userName, cultureName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Tax management
    //--------------item tax classes------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TaxItemClassInfo> GetTaxItemClassDetails(int offset, int limit, string itemClassName, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            paraMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            paraMeter.Add(new KeyValuePair<string, object>("@ItemClassName", itemClassName));
            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paraMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlh = new SQLHandler();
            return sqlh.ExecuteAsList<TaxItemClassInfo>("usp_Aspx_GetItemTaxClasses", paraMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //-------------------save item tax class--------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveAndUpdateTaxItemClass(int taxItemClassID, string taxItemClassName, string cultureName, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@TaxItemClassID", taxItemClassID));
            paraMeter.Add(new KeyValuePair<string, object>("@TaxItemClassName", taxItemClassName));
            paraMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paraMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlh = new SQLHandler();
            sqlh.ExecuteNonQuery("usp_Aspx_SaveAndUpdateTaxItemClass", paraMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //-----------------Delete tax item classes --------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteTaxItemClass(string taxItemClassIDs, int storeID, int portalID, string cultureName, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@TaxItemClassIDs", taxItemClassIDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteTaxItemClass", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //--------------customer tax classes------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TaxCustomerClassInfo> GetTaxCustomerClassDetails(int offset, int limit, string className, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            paraMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            paraMeter.Add(new KeyValuePair<string, object>("@ClassName", className));
            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paraMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlh = new SQLHandler();
            return sqlh.ExecuteAsList<TaxCustomerClassInfo>("usp_Aspx_GetTaxCustomerClass", paraMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //-------------------save customer tax class--------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveAndUpdateTaxCustmerClass(int taxCustomerClassID, string taxCustomerClassName, string cultureName, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@TaxCustomerClassID", taxCustomerClassID));
            paraMeter.Add(new KeyValuePair<string, object>("@TaxCustomerClassName", taxCustomerClassName));
            paraMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paraMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlh = new SQLHandler();
            sqlh.ExecuteNonQuery("usp_Aspx_SaveAndUpdateTaxCustomerClass", paraMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //-----------------Delete tax customer classes --------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteTaxCustomerClass(string taxCustomerClassIDs, int storeID, int portalID, string cultureName, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@TaxCustomerClassIDs", taxCustomerClassIDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteTaxCustomerClass", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //--------------tax rates------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TaxRateInfo> GetTaxRateDetails(int offset, int limit, string taxName, string searchCountry, string searchState, string zip, int storeID, int portalID)
    {
        try
        {
            List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            paraMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            paraMeter.Add(new KeyValuePair<string, object>("@TaxName", taxName));
            paraMeter.Add(new KeyValuePair<string, object>("@SearchCountry", searchCountry));
            paraMeter.Add(new KeyValuePair<string, object>("@SerachState", searchState));
            paraMeter.Add(new KeyValuePair<string, object>("@Zip", zip));
            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            SQLHandler sqlh = new SQLHandler();
            return sqlh.ExecuteAsList<TaxRateInfo>("usp_Aspx_GetTaxRates", paraMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //----------------- save and update tax rates--------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveAndUpdateTaxRates(int taxRateID, string taxRateTitle, string taxCountryCode, string taxStateCode, string taxZipCode, bool isTaxZipRange, decimal taxRateValue, bool rateType, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
            Parameter.Add(new KeyValuePair<string, object>("@TaxRateID", taxRateID));
            Parameter.Add(new KeyValuePair<string, object>("@TaxRateTitle", taxRateTitle));
            Parameter.Add(new KeyValuePair<string, object>("@TaxCountryCode", taxCountryCode));
            Parameter.Add(new KeyValuePair<string, object>("@TaxStateCode", taxStateCode));
            Parameter.Add(new KeyValuePair<string, object>("@ZipPostCode", taxZipCode));
            Parameter.Add(new KeyValuePair<string, object>("@IsZipPostRange", isTaxZipRange));
            Parameter.Add(new KeyValuePair<string, object>("@TaxRateValue", taxRateValue));
            Parameter.Add(new KeyValuePair<string, object>("@RateType", rateType));
            Parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            Parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            Parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_SaveAndUpdateTaxRates", Parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //-------------dalete Tax rates-----------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteTaxRates(string taxRateIDs, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@TaxRateIDs", taxRateIDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteTaxRates", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //--------------------------get customer class----------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TaxManageRulesInfo> GetTaxRules(int offset, int limit, string ruleName, string customerClassName, string itemClassName, string rateTitle, string priority, string displayOrder, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            paraMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            paraMeter.Add(new KeyValuePair<string, object>("@RuleName", ruleName));
            paraMeter.Add(new KeyValuePair<string, object>("@CustomerClassName", customerClassName));
            paraMeter.Add(new KeyValuePair<string, object>("@ItemClassName", itemClassName));
            paraMeter.Add(new KeyValuePair<string, object>("@RateTitle", rateTitle));
            paraMeter.Add(new KeyValuePair<string, object>("@SearchPriority", priority));
            paraMeter.Add(new KeyValuePair<string, object>("@SearchDisplayOrder", displayOrder));
            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paraMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlh = new SQLHandler();
            return sqlh.ExecuteAsList<TaxManageRulesInfo>("usp_Aspx_GetTaxManageRules", paraMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //------------------------bind tax customer class name list-------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TaxCustomerClassInfo> GetCustomerTaxClass()
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<TaxCustomerClassInfo>("usp_Aspx_GetCustomerTaxClassList");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //------------------------bind tax item class name list-------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TaxItemClassInfo> GetItemTaxClass()
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<TaxItemClassInfo>("usp_Aspx_GetItemTaxClassList");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //------------------------bind tax rate list-------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TaxRateInfo> GetTaxRate()
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<TaxRateInfo>("usp_Aspx_GetTaxRateList");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //-------------------save and update tax rules--------------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<PricingRuleAttributeInfo> GetPricingRuleAttributes(int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<PricingRuleAttributeInfo> portalRoleCollection = new List<PricingRuleAttributeInfo>();
            PriceRuleSqlProvider priceRuleController = new PriceRuleSqlProvider();
            portalRoleCollection = priceRuleController.GetPricingRuleAttributes(portalID, storeID, userName, cultureName);
            return portalRoleCollection;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CatalogPriceRulePaging> GetPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, Int32 storeID, Int32 portalID, string userName, string culture, int offset, int limit)
    {
        PriceRuleController priceRuleController = new PriceRuleController();
        return priceRuleController.GetCatalogPricingRules(ruleName, startDate, endDate, isActive, storeID, portalID, userName, culture, offset, limit);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public CatalogPricingRuleInfo GetPricingRule(Int32 catalogPriceRuleID, Int32 storeID, Int32 portalID, string userName, string culture)
    {
        CatalogPricingRuleInfo catalogPricingRuleInfo = new CatalogPricingRuleInfo();
        PriceRuleController priceRuleController = new PriceRuleController();
        catalogPricingRuleInfo = priceRuleController.GetCatalogPricingRule(catalogPriceRuleID, storeID, portalID, userName, culture);
        return catalogPricingRuleInfo;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public string SavePricingRule(CatalogPricingRuleInfo catalogPricingRuleInfo, Int32 storeID, Int32 portalID, string userName, string culture, Array parentID)
    {
        try
        {
            PriceRuleController priceRuleController = new PriceRuleController();
            priceRuleController.SaveCatalogPricingRule(catalogPricingRuleInfo, storeID, portalID, userName, culture, parentID);
            return "({ \"returnStatus\" : 1 , \"Message\" : \"Saving catalog pricing rule successfully.\" })";
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShippingMethodInfo> GetShippingMethods(System.Nullable<bool> isActive, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler Sq = new SQLHandler();
            return Sq.ExecuteAsList<ShippingMethodInfo>("usp_Aspx_GetShippingMethods", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CartPricingRuleAttributeInfo> GetCartPricingRuleAttributes(int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<CartPricingRuleAttributeInfo> lst = new List<CartPricingRuleAttributeInfo>();
            PriceRuleSqlProvider priceRuleProvider = new PriceRuleSqlProvider();
            lst = priceRuleProvider.GetCartPricingRuleAttributes(portalID, storeID, userName, cultureName);
            return lst;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public string SaveCartPricingRule(CartPricingRuleInfo objCartPriceRule, Int32 storeID, Int32 portalID, string userName, string culture, Array parentID)
    {
        try
        {
            PriceRuleController priceRuleController = new PriceRuleController();
            priceRuleController.SaveCartPricingRule(objCartPriceRule, storeID, portalID, userName, culture, parentID);
            return "({ \"returnStatus\" : 1 , \"Message\" : \"Saving cart pricing rule successfully.\" })";
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CartPriceRulePaging> GetCartPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, Int32 storeID, Int32 portalID, string userName, string culture, int offset, int limit)
    {
        PriceRuleController priceRuleController = new PriceRuleController();
        return priceRuleController.GetCartPricingRules(ruleName, startDate, endDate, isActive, storeID, portalID, userName, culture, offset, limit);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public CartPricingRuleInfo GetCartPricingRule(Int32 cartPriceRuleID, Int32 storeID, Int32 portalID, string userName, string culture)
    {
        CartPricingRuleInfo cartPricingRuleInfo = new CartPricingRuleInfo();
        PriceRuleController priceRuleController = new PriceRuleController();
        cartPricingRuleInfo = priceRuleController.GetCartPriceRules(cartPriceRuleID, storeID, portalID, userName, culture);
        return cartPricingRuleInfo;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //public bool AddtoCart(int itemID, int storeID, int portalID, string userName, string cultureName)
    //{

    //    try
    //    {
    //        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
    //        ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
    //        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
    //        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
    //        ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
    //        ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
    //        SQLHandler sqlH = new SQLHandler();
    //        return sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckCostVariantForItem", ParaMeter, "@IsExist");

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public bool AddItemstoCart(int itemID, decimal itemPrice, int itemQuantity, int storeID, int portalID, string userName, int custometID, string sessionCode, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            ParaMeter.Add(new KeyValuePair<string, object>("@Price", itemPrice));
            ParaMeter.Add(new KeyValuePair<string, object>("@Quantity", itemQuantity));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckCostVariantForItem", ParaMeter, "@IsExist");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void AddItemstoCartFromDetail(int itemID, decimal itemPrice, decimal weight, int itemQuantity, string itemCostVariantIDs, int storeID, int portalID, string userName, int custometID, string sessionCode, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            ParaMeter.Add(new KeyValuePair<string, object>("@Price", itemPrice));
            ParaMeter.Add(new KeyValuePair<string, object>("@Weight", weight));
            ParaMeter.Add(new KeyValuePair<string, object>("@Quantity", itemQuantity));
            ParaMeter.Add(new KeyValuePair<string, object>("@CostVariantsValueIDs", itemCostVariantIDs));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("dbo.usp_Aspx_AddToCart", ParaMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region MiniCart Display
    //----------------------Count my cart items--------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    #region Items Reporting
    //----------------------GetMostViewedItems----------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<MostViewedItemsInfo> GetMostViewedItemsList(int offset, int limit, string name, int storeId, int portalId, string userName, string cultureName)
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
    //------------------------------------------------------------------
    // --------------------------Get Low Stock Items----------------------------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<LowStockItemsInfo> GetLowStockItemsList(int offset, int limit, string Sku, string name, System.Nullable<bool> isActive, int storeId, int portalId, string userName, string cultureName ,int lowStock)
    {
        try
        {
            ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
            return obj.GetAllLowStockItems(offset, limit, Sku, name, isActive, storeId, portalId, userName, cultureName,lowStock);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //---------------------------------------------------------------------------------------------
    //------------------------------------Get Ordered Items List-----------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<OrderItemsGroupByItemIDInfo> GetOrderedItemsList(int offset, int limit, string name, int storeId, int portalId, string userName, string cultureName)
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
    //------------------------------------------------------------------------------------------------
    // --------------------------Get DownLoadable Items----------------------------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<DownLoadableItemGetInfo> GetDownLoadableItemsList(int offset, int limit, string Sku, string name, int storeId, int portalId, string userName, string cultureName, System.Nullable<bool> CheckUser)
    {
        try
        {
            ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
            return obj.GetDownLoadableItemsList(offset, limit, Sku, name, storeId, portalId, userName, cultureName, CheckUser);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    // ShoppingCartManagement ---------------------get Cart details in grid-------------------------------
    //Live Carts
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShoppingCartInfo> GetShoppingCartItemsDetails(int offset, int limit, int storeID, string itemName, string quantity, string orderId, int portalID, string userName, string cultureName)
    {
        // quantity = quantity == "" ? null : quantity;
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@ItemName", itemName));
            parameter.Add(new KeyValuePair<string, object>("@Quantity", quantity));
            //parameter.Add(new KeyValuePair<string, object>("@CartID", cartId));
            parameter.Add(new KeyValuePair<string, object>("@OrderID", orderId));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<ShoppingCartInfo>("usp_Aspx_GetLiveCarts", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------bind Abandoned cart details-------------------------
    //Abondaned Carts
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<AbandonedCartInfo> GetAbandonedCartDetails(int offset, int limit, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            List<AbandonedCartInfo> bind = sqlH.ExecuteAsList<AbandonedCartInfo>("usp_Aspx_GetAbandonedCarts", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // OrderManagement ---------------------get order details in grid-----------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<MyOrderListInfo> GetOrderDetails(int offset, int limit, int storeID, int portalID, string cultureName, string orderStatusName, string userName)
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
	[OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void NotifyOrderStatusUpdate(string senderEmail, string receiverEmail, string subject, string message)
    {
        try
        {
            string emailSuperAdmin;
            string emailSiteAdmin;
            SageFrameConfig pagebase = new SageFrameConfig();
            emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
            emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
            MailHelper.SendMailNoAttachment(senderEmail, receiverEmail, subject, message, emailSiteAdmin, emailSuperAdmin);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

 	//-----------------------Update Order Status by Admin-----------------------   
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveOrderStatus(int storeID, int portalID, int orderStatusID, int orderID)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@OrderStatusID", orderStatusID));
            parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_UpdateOrderStatus", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // InvoiceListMAnagement -----------------------get invoice details-----------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<InvoiceDetailsInfo> GetInvoiceDetailsList(int offset, int limit, string invoiceNumber, string billToNama, string status, int storeID, int portalID, string userName, string cultureName)
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<InvoiceDetailByorderIDInfo> GetInvoiceDetailsByOrderID(int orderID, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
            SQLHandler sqlh = new SQLHandler();
            List<InvoiceDetailByorderIDInfo> info;
            info = sqlh.ExecuteAsList<InvoiceDetailByorderIDInfo>("usp_Aspx_GetInvoiceDetailsByOrderID", ParaMeter);
            return info;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    //--ShipmentsListManagement
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShipmentsDetailsInfo> GetShipmentsDetails(int offset, int limit, string shippimgMethodName, string shipToName, string orderId, int storeID, int portalID, string userName, string cultureName)
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ShipmentsDetailsViewInfo> BindAllShipmentsDetails(int shippingMethodID, int portalID, int storeID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ShippingMethodID", shippingMethodID));
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CustomerReviewReportsInfo> GetCustomerReviews(int offset, int limit, int storeID, int portalID)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            SQLHandler sqlH = new SQLHandler();
            List<CustomerReviewReportsInfo> bind = sqlH.ExecuteAsList<CustomerReviewReportsInfo>("usp_Aspx_GetCustomerReviews", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------Show All Customer Reviews-------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<UserRatingInformationInfo> GetAllCustomerReviewsList(int offset, int limit, int storeID, int portalID, string cultureName, string userName, string user, string statusName, string itemName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@User", user));
            ParaMeter.Add(new KeyValuePair<string, object>("@StatusName", statusName));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemName", itemName));
            SQLHandler sqlH = new SQLHandler();
            List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetCustomerWiseReviewsList", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //------------------Bind User List------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ItemReviewsInfo> GetItemReviews(int offset, int limit, int storeID, int portalID)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            SQLHandler sqlH = new SQLHandler();
            List<ItemReviewsInfo> bind = sqlH.ExecuteAsList<ItemReviewsInfo>("usp_Aspx_GetItemReviewsList", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------Show All Item Reviews-------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<UserRatingInformationInfo> GetAllItemReviewsList(int offset, int limit, int storeID, int portalID, string cultureName, int itemID, string userName, string statusName, string itemName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@StatusName", statusName));
            ParaMeter.Add(new KeyValuePair<string, object>("@ItemName", itemName));
            SQLHandler sqlH = new SQLHandler();
            List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetItemWiseReviewsList", ParaMeter);
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<SpecialItemsInfo> GetSpecialItems(int storeID, int portalID, int count)
    {
        List<SpecialItemsInfo> slInfo = new List<SpecialItemsInfo>();
        List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
        paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        paramCol.Add(new KeyValuePair<string, object>("@count", count));
        SQLHandler sageSQL = new SQLHandler();
        slInfo = sageSQL.ExecuteAsList<SpecialItemsInfo>("[dbo].[usp_Aspx_GetSpecialItems]", paramCol);
        return slInfo;
    }
    #endregion

    #region Best Seller
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<BestSellerInfo> GetBestSoldItems(int storeID, int portalID, int count)
    {
        List<BestSellerInfo> slInfo = new List<BestSellerInfo>();
        List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
        paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        paramCol.Add(new KeyValuePair<string, object>("@count", count));
        SQLHandler sageSQL = new SQLHandler();
        slInfo = sageSQL.ExecuteAsList<BestSellerInfo>("[dbo].[usp_Aspx_GetBestSoldItems]", paramCol);
        return slInfo;
    }
    #endregion

    #region Payment Gateway and CheckOUT PROCESS

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<PaymentGatewayListInfo> GetPGList(int storeID, int portalID, string cultureName)
    {
        List<PaymentGatewayListInfo> pginfo = new List<PaymentGatewayListInfo>();

        List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
        paramCol.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        paramCol.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        SQLHandler sageSQL = new SQLHandler();
        pginfo = sageSQL.ExecuteAsList<PaymentGatewayListInfo>("[dbo].[usp_Aspx_GetPaymentGatewayList]", paramCol);

        return pginfo;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<PaymentGateway> GetPaymentGateway(int _portalID, string _cultureName, string _userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", _portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", _cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", _userName));
            SQLHandler sqlH = new SQLHandler();
            List<PaymentGateway> Count = sqlH.ExecuteAsList<PaymentGateway>("sp_GetPaymentGateway", ParaMeter);
            return Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<UserAddressInfo> GetUserAddressForCheckOut(int storeID, int portalID, string userName, string CultureName)
    {

        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<UserAddressInfo>("usp_Aspx_GetUserAddressBookDetails", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveOrderDetails(OrderDetailsCollection OrderDetail)
    {
        try
        {
            OrderDetail.ObjOrderDetails.OrderStatusID = 7;
            AddOrderDetails(OrderDetail);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void AddOrderDetails(OrderDetailsCollection OrderData)
    {
        SQLHandler sqlH = new SQLHandler();
        SqlTransaction tran;
        tran = (SqlTransaction)sqlH.GetTransaction();
        WcfSession sn = new WcfSession();
        try
        {
            AspxOrderDetails ObjOrderDetails = new AspxOrderDetails();

            int billingAddressID = 0;
            int shippingAddressId = 0;
            int orderID = 0;
            if (OrderData.ObjOrderDetails.IsMultipleCheckOut == false)
            {
                if (OrderData.ObjBillingAddressInfo.IsBillingAsShipping == true)
                {
                    if (int.Parse(OrderData.ObjBillingAddressInfo.AddressID) == 0 &&
                        int.Parse(OrderData.ObjShippingAddressInfo.AddressID) == 0)
                    {
                        int addressID = ObjOrderDetails.AddAddress(OrderData, tran);
                        billingAddressID = ObjOrderDetails.AddBillingAddress(OrderData, tran, addressID);
                        shippingAddressId = ObjOrderDetails.AddShippingAddress(OrderData, tran, addressID);
                    }
                }
                else
                {
                    if (int.Parse(OrderData.ObjBillingAddressInfo.AddressID) == 0)
                        billingAddressID = ObjOrderDetails.AddBillingAddress(OrderData, tran);
                    if (int.Parse(OrderData.ObjShippingAddressInfo.AddressID) == 0)
                        shippingAddressId = ObjOrderDetails.AddShippingAddress(OrderData, tran);
                }
            }
            int paymentMethodID = ObjOrderDetails.AddPaymentInfo(OrderData, tran);

            if (billingAddressID > 0)
            {
                orderID = ObjOrderDetails.AddOrder(OrderData, tran, billingAddressID, paymentMethodID);

                sn.SetSessionVariable("OrderID", orderID);

            }
            else
            {
                orderID = ObjOrderDetails.AddOrderWithMultipleCheckOut(OrderData, tran, paymentMethodID);

                sn.SetSessionVariable("OrderID", orderID);
            }

            if (shippingAddressId > 0)
                ObjOrderDetails.AddOrderItems(OrderData, tran, orderID, shippingAddressId);
            else
                ObjOrderDetails.AddOrderItemsList(OrderData, tran, orderID);

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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public bool CheckDownloadableItemOnly(int storeID, int portalID, int customerID, string sessionCode)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsGivenType<bool>("[usp_Aspx_CheckForDownloadableItemsInCart]", ParaMeter, "@IsAllDownloadable");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    #endregion

    #region Payment Gateway Installation
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void UpdatePaymentMethod(int storeId, int portalId, int paymentGatewayID, string paymentGatewayName, bool isActive, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();


            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayID));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<GetOrderdetailsByPaymentGatewayIDInfo> GetOrderDetailsbyPayID(int offset, int limit, string billToName, string ShipToName, string orderStatusName, int paymentGatewayID, int storeID, int portalID, string userName, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@BillToName", billToName));
            parameter.Add(new KeyValuePair<string, object>("@ShipToName", ShipToName));
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public StoreSettingInfo GetAllStoreSettings(int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsObject<StoreSettingInfo>("usp_Aspx_GetAllStoreSettings", ParaMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void UpateStoreSettings(string settingKeys, string settingValues, string prevFilePath, string newFilePath, int storeID, int portalID, string cultureName)
    {

        try
        {
            FileHelperController fileObj = new FileHelperController();
            string uplodedValue = string.Empty;
            if (newFilePath != null && prevFilePath != newFilePath)
            {
                string tempFolder = @"Upload\temp";
                uplodedValue = fileObj.MoveFileToSpecificFolder(tempFolder, prevFilePath, newFilePath, @"Modules\AspxCommerce\AspxStoreSettingsManagement\uploads\", storeID, "store_");
            }
            else
            {
                uplodedValue = prevFilePath;
            }
            settingKeys = "DefaultProductImageURL" + '#' + settingKeys;
            settingValues = uplodedValue + '#' + settingValues;

            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@SettingKeys", settingKeys));
            parameter.Add(new KeyValuePair<string, object>("@SettingValues", settingValues));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_GetStoreSettingsUpdate", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region CardType_Management
    //------------------------bind All CardType name list-------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CardTypeInfo> GetAllCardTypeList(int offset, int limit, int StoreID, int PortalID, string CultureName, string CardTypeName, System.Nullable<bool> IsActive)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            parameter.Add(new KeyValuePair<string, object>("@CardTypeName", CardTypeName));
            parameter.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CardTypeInfo>("usp_Aspx_GetCardTypeInGrid", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CardTypeInfo> AddUpdateCardType(int StoreID, int PortalID, string CultureName, string UserName, int CardTypeID, string CardTypeName, bool IsActive)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

            parameter.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            parameter.Add(new KeyValuePair<string, object>("@UserName", UserName));
            parameter.Add(new KeyValuePair<string, object>("@CardTypeID", CardTypeID));
            parameter.Add(new KeyValuePair<string, object>("@CardTypeName", CardTypeName));
            parameter.Add(new KeyValuePair<string, object>("@IsActive", IsActive));

            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CardTypeInfo>("[dbo].[usp_Aspx_AddUpdateCardType]", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteCardTypeByID(int CardTypeID, int StoreID, int PortalID, string UserName, string CultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CardTypeID", CardTypeID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", CultureName));

            SQLHandler Sq = new SQLHandler();
            Sq.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteCardTypeByID]", ParaMeterCollection);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteCardTypeMultipleSelected(string CardTypeIDs, int StoreID, int PortalID, string UserName, string CultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CardTypeIDs", CardTypeIDs));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            SQLHandler Sq = new SQLHandler();
            Sq.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteCardTypeMultipleSelected]", ParaMeterCollection);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    #endregion

    #region OrderStatusManagement

    //------------------------bind Allorder status name list-------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<OrderStatusListInfo> GetAllStatusList(int offset, int limit, int StoreID, int PortalID, string CultureName, string UserName, string orderStatusName, System.Nullable<bool> IsActive)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            parameter.Add(new KeyValuePair<string, object>("@UserName", UserName));
            parameter.Add(new KeyValuePair<string, object>("@OrderStatusName", orderStatusName));
            parameter.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<OrderStatusListInfo>("[dbo].[usp_Aspx_GetOrderAliasStatusList]", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<OrderStatusListInfo> AddUpdateOrderStatus(int StoreID, int PortalID, string CultureName, string UserName, Int32 OrderStatusID, string OrderStatusAliasName, string AliasToolTip, string AliasHelp, bool IsSystem, bool IsActive)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

            parameter.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            parameter.Add(new KeyValuePair<string, object>("@UserName", UserName));
            parameter.Add(new KeyValuePair<string, object>("@OrderStatusID", OrderStatusID));
            parameter.Add(new KeyValuePair<string, object>("@OrderStatusAliasName", OrderStatusAliasName));
            parameter.Add(new KeyValuePair<string, object>("@AliasToolTip", AliasToolTip));
            parameter.Add(new KeyValuePair<string, object>("@AliasHelp", AliasHelp));
            parameter.Add(new KeyValuePair<string, object>("@IsSystem", IsSystem));
            parameter.Add(new KeyValuePair<string, object>("@IsActive", IsActive));

            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<OrderStatusListInfo>("[dbo].[usp_Aspx_OrderStatusAddUpdate]", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteOrderStatusByID(int OrderStatusID, int StoreID, int PortalID, string UserName, string CultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@OrderStatusID", OrderStatusID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", CultureName));

            SQLHandler Sq = new SQLHandler();
            Sq.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteOrderStatusByID]", ParaMeterCollection);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteOrderStatusMultipleSelected(string OrderStatusIDs, int StoreID, int PortalID, string UserName, string CultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@OrderStatusIDs", OrderStatusIDs));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            SQLHandler Sq = new SQLHandler();
            Sq.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteOrderStatusMultipleSelected]", ParaMeterCollection);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    #endregion

    #region Admin DashBoard Chart
    //------------------------bind order Chart by last week-------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<OrderChartInfo> GetOrderChartDetailsBy24hours(int storeID, int portalID)
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<StoreLocatorInfo> GetAllStoresLocation(int PortalID)
    {
        List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
        ParameterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
        try
        {
            SQLHandler SageHandler = new SQLHandler();
            return SageHandler.ExecuteAsList<StoreLocatorInfo>("usp_Aspx_GetAllStoreLocations", ParameterCollection);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<StoreLocatorInfo> GetLocationsNearBy(string Address, double SearchDistance, string GoogleAPIKey)
    {
        GeoCoder.Coordinate Coordinate = GeoCoder.Geocode.GetCoordinates(Address, GoogleAPIKey);

        List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
        ParameterCollection.Add(new KeyValuePair<string, object>("@CenterLatitude", Coordinate.Latitude));
        ParameterCollection.Add(new KeyValuePair<string, object>("@CenterLongitude", Coordinate.Longitude));
        ParameterCollection.Add(new KeyValuePair<string, object>("@SearchDistance", SearchDistance));
        ParameterCollection.Add(new KeyValuePair<string, object>("@EarthRadius", 3961));

        try
        {
            SQLHandler SageHandler = new SQLHandler();
            return SageHandler.ExecuteAsList<StoreLocatorInfo>("usp_Aspx_GetNearbyStoreLocations", ParameterCollection);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<GeoCoder.Coordinate> GetCoordinates(string Address, string City, string Country, string ZIP, string GoogleAPIKey)
    {
        try
        {
            string strAddress = String.Format("{0}, {1}, {2} {3}", Address.Trim(), City.Trim(), Country, ZIP.Trim());
            // Geocode Address
            GeoCoder.Coordinate Coordinate = GeoCoder.Geocode.GetCoordinates(strAddress, GoogleAPIKey);
            List<GeoCoder.Coordinate> lst = new List<GeoCoder.Coordinate>();
            lst.Add(Coordinate);
            return lst;
        }
        catch (Exception ex)
        {
            return null;
            throw ex;
        }
    }

    //#region NOT USED YET
    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //public bool AddStoreLocation(StoreLocatorInfo objLocation)
    //{
    //    try
    //    {
    //        List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();

    //        ParameterCollection.Add(new KeyValuePair<string, object>("@LocationName", objLocation.LocationName));
    //        ParameterCollection.Add(new KeyValuePair<string, object>("@Description", objLocation.Description));
    //        ParameterCollection.Add(new KeyValuePair<string, object>("@Address", objLocation.Address));
    //        ParameterCollection.Add(new KeyValuePair<string, object>("@City", objLocation.City));
    //        ParameterCollection.Add(new KeyValuePair<string, object>("@State", objLocation.State));
    //        ParameterCollection.Add(new KeyValuePair<string, object>("@Country", objLocation.Country));
    //        ParameterCollection.Add(new KeyValuePair<string, object>("@ZIP", objLocation.ZIP));
    //        ParameterCollection.Add(new KeyValuePair<string, object>("@Latitude", objLocation.Latitude));
    //        ParameterCollection.Add(new KeyValuePair<string, object>("@Longitude", objLocation.Longitude));
    //        ParameterCollection.Add(new KeyValuePair<string, object>("@PortalID", objLocation.PortalID));
    //        ParameterCollection.Add(new KeyValuePair<string, object>("@AddedBy", objLocation.AddedBy));
    //        SQLHandler SageHandler = new SQLHandler();
    //        SageHandler.ExecuteNonQuery("usp_Aspx_AddStoreLocations", ParameterCollection);
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //        throw ex;
    //    }
    //}

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //public void AddStoreLocatorSettings(string SettingKey, string SettingValue, int PortalID, string UserName)
    //{
    //    List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
    //    ParameterCollection.Add(new KeyValuePair<string, object>("@SettingKey", SettingKey));
    //    ParameterCollection.Add(new KeyValuePair<string, object>("@SettingValue", SettingValue));
    //    ParameterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
    //    ParameterCollection.Add(new KeyValuePair<string, object>("@AddedBy", UserName));
    //    try
    //    {
    //        SQLHandler SageHandler = new SQLHandler();
    //        SageHandler.ExecuteNonQuery("usp_Aspx_AddStoreLocatorSettings", ParameterCollection);
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //} 
    //#endregion
    #endregion

    #region Online Users
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<OnLineUserBaseInfo> GetRegisteredUserOnlineCount(int offset, int limit, string SearchUserName, string SearchHostAddress, string SearchBrowser, int StoreID, int PortalID, string UserName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
            ParameterCollection.Add(new KeyValuePair<string, object>("@Offset", offset));
            ParameterCollection.Add(new KeyValuePair<string, object>("@Limit", limit));
            ParameterCollection.Add(new KeyValuePair<string, object>("@SearchUserName", SearchUserName));
            ParameterCollection.Add(new KeyValuePair<string, object>("@HostAddress", SearchHostAddress));
            ParameterCollection.Add(new KeyValuePair<string, object>("@Browser", SearchBrowser));
            ParameterCollection.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
            ParameterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParameterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            SQLHandler SageHandler = new SQLHandler();
            return SageHandler.ExecuteAsList<OnLineUserBaseInfo>("usp_Aspx_GetOnlineRegisteredUsers", ParameterCollection);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<OnLineUserBaseInfo> GetAnonymousUserOnlineCount(int offset, int limit, string SearchHostAddress, string SearchBrowser, int StoreID, int PortalID, string UserName)
    {
        List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();

        ParameterCollection.Add(new KeyValuePair<string, object>("@Offset", offset));
        ParameterCollection.Add(new KeyValuePair<string, object>("@Limit", limit));

        ParameterCollection.Add(new KeyValuePair<string, object>("@HostAddress", SearchHostAddress));
        ParameterCollection.Add(new KeyValuePair<string, object>("@Browser", SearchBrowser));
        ParameterCollection.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
        ParameterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
        ParameterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
        try
        {
            SQLHandler SageHandler = new SQLHandler();
            List<OnLineUserBaseInfo> lst = new List<OnLineUserBaseInfo>();
            lst = SageHandler.ExecuteAsList<OnLineUserBaseInfo>("usp_Aspx_GetOnlineAnonymousUsers", ParameterCollection);
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CustomerOrderTotalInfo> GetCustomerOrderTotal(int offset, int limit, int storeID, int portalID, string cultureName, string user)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@User", user));
            SQLHandler sqlH = new SQLHandler();
            List<CustomerOrderTotalInfo> bind = sqlH.ExecuteAsList<CustomerOrderTotalInfo>("usp_Aspx_GetCustomerOrderTotal", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Store Access Management
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<StoreAccessAutocomplete> SearchStoreAccess(string text, int KeyID)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreAccessKeyID", KeyID));
            parameter.Add(new KeyValuePair<string, object>("@StoreAccessData", text));
            return sqlH.ExecuteAsList<StoreAccessAutocomplete>("[dbo].[usp_Aspx_GetSearchAutoComplete]", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveUpdateStoreAccess(int Edit, int StoreAccessKeyID, string StoreAccessData, string Reason, bool IsActive, int storeID, int portalID, string userName)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreAccessKeyID", StoreAccessKeyID));
            parameter.Add(new KeyValuePair<string, object>("@StoreAccessData", StoreAccessData));
            parameter.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
            parameter.Add(new KeyValuePair<string, object>("@Reason", Reason));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@StoreAccessID", Edit));
            parameter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_StoreAccessAddUpdate]", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

            sqlH.ExecuteNonQuery("[dbo].[Aspx_StoreAccessDelete]", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void AddUpdateStoreAccess(int storeAccessID, int storeID, int portalID, string userName)
    {
        try
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@StoreAccessID", storeAccessID));
            parameter.Add(new KeyValuePair<string, object>("@DeletedBy", userName));

            sqlH.ExecuteNonQuery("[dbo].[Aspx_StoreAccessAddUpdate]", parameter);
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
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
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
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<AspxUserList>("[dbo].[usp_Aspx_GetListOfCurrentCustomerEmail]", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<StoreAccessInfo> LoadStoreAccessCustomer(int offset, int limit, string search, System.Nullable<DateTime> addedon, System.Nullable<bool> status, int storeID, int portalID)
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
            parameter.Add(new KeyValuePair<string, object>("@AddedOn", addedon));
            parameter.Add(new KeyValuePair<string, object>("@Status", status));

            return sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessCustomer]", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<StoreAccessInfo> LoadStoreAccessEmails(int offset, int limit, string search, System.Nullable<DateTime> addedon, System.Nullable<bool> status, int storeID, int portalID)
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
            parameter.Add(new KeyValuePair<string, object>("@AddedOn", addedon));
            parameter.Add(new KeyValuePair<string, object>("@Status", status));

            return sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessEmail]", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<StoreAccessInfo> LoadStoreAccessIPs(int offset, int limit, string search, System.Nullable<DateTime> addedon, System.Nullable<bool> status, int storeID, int portalID)
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
            parameter.Add(new KeyValuePair<string, object>("@AddedOn", addedon));
            parameter.Add(new KeyValuePair<string, object>("@Status", status));

            return sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessIP]", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<StoreAccessInfo> LoadStoreAccessDomains(int offset, int limit, string search, System.Nullable<DateTime> addedon, System.Nullable<bool> status, int storeID, int portalID)
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
            parameter.Add(new KeyValuePair<string, object>("@AddedOn", addedon));
            parameter.Add(new KeyValuePair<string, object>("@Status", status));

            return sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessDomain]", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<StoreAccessInfo> LoadStoreAccessCreditCards(int offset, int limit, string search, System.Nullable<DateTime> addedon, System.Nullable<bool> status, int storeID, int portalID)
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
            parameter.Add(new KeyValuePair<string, object>("@AddedOn", addedon));
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SaveStoreClose(System.Nullable<bool> temporary, System.Nullable<bool> permanent, System.Nullable<DateTime> closeFrom, System.Nullable<DateTime> closeTill, int storeID, int portalID, string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@Temporary", temporary));
            ParaMeter.Add(new KeyValuePair<string, object>("@Permanent", permanent));
            ParaMeter.Add(new KeyValuePair<string, object>("@CloseFrom", closeFrom));
            ParaMeter.Add(new KeyValuePair<string, object>("@CloseTill", closeTill));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_SaveStoreClose", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region CustomerDetails
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CustomerDetailsInfo> GetCustomerDetails(int offset, int limit, int StoreID, int PortalID, string CultureName,string userName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
            ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));

            SQLHandler sqlH = new SQLHandler();
            List<CustomerDetailsInfo> bind = sqlH.ExecuteAsList<CustomerDetailsInfo>("[dbo].[usp_Aspx_GetCustomerDetails]", ParaMeter);
            return bind;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteMultipleCustomersByCustomerID(string CustomerIDs, int storeId, int portalId, string userName)
    {
        try
        {
            CustomerManagementSQLProvider obj = new CustomerManagementSQLProvider();
            obj.DeleteMultipleCustomers(CustomerIDs, storeId, portalId, userName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
            sqlH.ExecuteNonQuery("[dbo].[Aspx_SaveUpdateReturnReason]", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
            sqlH.ExecuteNonQuery("[dbo].[Aspx_SaveUpdateReturnStatus]", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ReturnReasonList> LoadReason(int storeID, int portalID)
    {
        try
        {
            List<ReturnReasonList> catInfo = new List<ReturnReasonList>();
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ReturnActionList> LoadAction(int storeID, int portalID)
    {
        try
        {
            List<ReturnActionList> catInfo = new List<ReturnActionList>();
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ReturnStatusList> LoadRequestStatus(int storeID, int portalID)
    {
        try
        {
            List<ReturnStatusList> catInfo = new List<ReturnStatusList>();
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<ReturnRequestList> LoadReturnRequest(int offset, int limit, string customer, System.Nullable<int> status, string email, int storeID, int portalID)
    {
        try
        {
            List<ReturnRequestList> catInfo = new List<ReturnRequestList>();
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

    #region Session
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void SetSessionValue(string key, string value)
    {
        WcfSession myCartSession = new WcfSession();
        myCartSession.SetSessionVariable(key, value);
    }
    #endregion

    //------------------------Multiple Delete Recently viewed Items-------------------------------
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
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
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public void DeleteComparedItems(string compareItems, int storeID, int portalID, string userName)
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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public string GetDiscountPriceRule(int cartID, int storeID, int portalID, string userName, string cultureName, decimal shippingCost)
    {

        try
        {
            SqlConnection SQLConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
            SqlCommand SQLCmd = new SqlCommand();
            SqlDataAdapter SQLAdapter = new SqlDataAdapter();
            DataSet SQLds = new DataSet();
            SQLCmd.Connection = SQLConn;
            SQLCmd.CommandText = "[dbo].[usp_Aspx_GetDiscountCartPriceRule]";
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.Parameters.AddWithValue("@CartID", cartID);
            SQLCmd.Parameters.AddWithValue("@StoreID", storeID);
            SQLCmd.Parameters.AddWithValue("@PortalID", portalID);
            SQLCmd.Parameters.AddWithValue("@CultureName", cultureName);
            SQLCmd.Parameters.AddWithValue("@UserName", userName);
            SQLCmd.Parameters.AddWithValue("@ShippingCost", shippingCost);
            SQLAdapter.SelectCommand = SQLCmd;
            SQLConn.Open();
            SqlDataReader dr = null;

            dr = SQLCmd.ExecuteReader();

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

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public int GetCartId(int storeID, int portalID, int customerID, string sessionCode)
    {

        try
        {
            SqlConnection SQLConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
            SqlCommand SQLCmd = new SqlCommand();
            SqlDataAdapter SQLAdapter = new SqlDataAdapter();
            DataSet SQLds = new DataSet();
            SQLCmd.Connection = SQLConn;
            SQLCmd.CommandText = "[dbo].[usp_Aspx_GetCartID]";
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.Parameters.AddWithValue("@CustomerID", customerID);
            SQLCmd.Parameters.AddWithValue("@StoreID", storeID);
            SQLCmd.Parameters.AddWithValue("@PortalID", portalID);
            SQLCmd.Parameters.AddWithValue("@SessionCode", sessionCode);

            SQLAdapter.SelectCommand = SQLCmd;
            SQLConn.Open();
            SqlDataReader dr = null;

            dr = SQLCmd.ExecuteReader();

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

    #region StoreSettingImplementation
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public decimal GetTotalCartItemPrice(int storeID, int portalID, int customerID, string sessionCode)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsScalar<decimal>("usp_Aspx_GetCartItemsTotalAmount", ParaMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public bool CheckAddressAlreadyExist(int storeID, int portalID, int customerID, string sessionCode)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckForMultipleAddress", ParaMeter, "@IsExist");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}

