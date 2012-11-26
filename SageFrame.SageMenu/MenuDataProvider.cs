using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using System.Data;
using System.Data.SqlClient;
using SageFrame.Web;
using SageFrame.Core;

namespace SageFrame.SageMenu
{
    public class MenuDataProvider
    {
        public static List<MenuInfo> GetMenuFront(int PortalID, string UserName, string CultureCode)
        {
            List<MenuInfo> lstPages = new List<MenuInfo>();
            string StoredProcedureName = "[dbo].[usp_SageMenuGetClientView]";
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@prefix","---"));           
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsDeleted", 0));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Username", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode", CultureCode));  
            SqlDataReader SQLReader;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName,ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }

            while (SQLReader.Read())
            {
                lstPages.Add(new MenuInfo(int.Parse(SQLReader["PageID"].ToString()), int.Parse(SQLReader["PageOrder"].ToString()), SQLReader["PageName"].ToString(), int.Parse(SQLReader["ParentID"].ToString()), int.Parse(SQLReader["Level"].ToString()),SQLReader["LevelPageName"].ToString(),SQLReader["SEOName"].ToString(),SQLReader["TabPath"].ToString(),bool.Parse(SQLReader["IsVisible"].ToString()),bool.Parse(SQLReader["ShowInMenu"].ToString())));                               
            }
            return lstPages;

        }
        public static List<MenuInfo> GetFooterMenu(int PortalID, string UserName,string CultureCode)
        {
            List<MenuInfo> lstPages = new List<MenuInfo>();
            string StoredProcedureName = "[dbo].[usp_SageMenuGetFooter]";
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Username", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode", CultureCode));
            SqlDataReader SQLReader;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName,ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }

            while (SQLReader.Read())
            {
                lstPages.Add(new MenuInfo(int.Parse(SQLReader["PageID"].ToString()), int.Parse(SQLReader["PageOrder"].ToString()), SQLReader["PageName"].ToString(), int.Parse(SQLReader["ParentID"].ToString()), int.Parse(SQLReader["Level"].ToString()), SQLReader["LevelPageName"].ToString(), SQLReader["SEOName"].ToString(), SQLReader["TabPath"].ToString(), bool.Parse(SQLReader["IsVisible"].ToString()), bool.Parse(SQLReader["ShowInMenu"].ToString())));                               
               
               
            }
            return lstPages;

        }
        public static List<MenuInfo> GetAdminMenu()
        {
            List<MenuInfo> lstPages = new List<MenuInfo>();
            string StoredProcedureName = "[dbo].[usp_sagemenugetadminmenu]";
            SqlDataReader SQLReader;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName);
            }
            catch (Exception e)
            {
                throw e;
            }

            while (SQLReader.Read())
            {
                lstPages.Add(new MenuInfo(int.Parse(SQLReader["PageID"].ToString()), int.Parse(SQLReader["PageOrder"].ToString()), SQLReader["PageName"].ToString(), int.Parse(SQLReader["ParentID"].ToString()), int.Parse(SQLReader["Level"].ToString()), SQLReader["LevelPageName"].ToString(), SQLReader["SEOName"].ToString(), SQLReader["TabPath"].ToString(), bool.Parse(SQLReader["IsVisible"].ToString()), bool.Parse(SQLReader["ShowInMenu"].ToString())));                               
               
                
            }
            return lstPages;

        }
        public static List<MenuInfo> GetSideMenu(int PortalID,string UserName,string PageName,string CultureCode)
        {
            List<MenuInfo> lstPages = new List<MenuInfo>();
            string StoredProcedureName = "[dbo].[usp_SageMenuGetSideMenu]";
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Username", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PageName", PageName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode", CultureCode));
            SqlDataReader SQLReader;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName,ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }

            while (SQLReader.Read())
            {                
                MenuInfo objMenu = new MenuInfo();
                objMenu.PageID = int.Parse(SQLReader["PageID"].ToString());
                objMenu.PageOrder = int.Parse(SQLReader["PageOrder"].ToString());
                objMenu.PageName = SQLReader["PageName"].ToString();
                objMenu.ParentID = int.Parse(SQLReader["ParentID"].ToString());
                objMenu.Level = int.Parse(SQLReader["Level"].ToString());
                objMenu.TabPath = SQLReader["TabPath"].ToString();
                lstPages.Add(objMenu);
                
            }
            return lstPages;

        }

        public static void UpdateSageMenuSettings(List<SageMenuSettingInfo> lstSetting)
        {
            foreach (SageMenuSettingInfo obj in lstSetting)
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", obj.UserModuleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SettingKey", obj.SettingKey));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SettingValue", obj.SettingValue));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", obj.IsActive));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", obj.PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedBy", obj.AddedBy));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", obj.AddedBy));

                try
                {
                    SQLHandler sagesql = new SQLHandler();
                    sagesql.ExecuteNonQuery("[dbo].[usp_SageMenuSettingAddUpdate]", ParaMeterCollection);

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static SageMenuSettingInfo GetMenuSetting(int PortalID, int UserModuleID)
        {
            SageMenuSettingInfo objSetting = new SageMenuSettingInfo();
            string StoredProcedureName = "[dbo].[usp_SageMenuSettingGetAll]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            try
            {
                objSetting = sagesql.ExecuteAsObject<SageMenuSettingInfo>(StoredProcedureName, ParaMeterCollection);

            }
            catch (Exception e)
            {
                throw e;
            }


            return objSetting;
        }

        public static List<MenuInfo> GetBackEndMenu(int ParentNodeID,string UserName,int PortalID,string CultureCode)
        {
            List<MenuInfo> lstPages = new List<MenuInfo>();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ParentNodeID", ParentNodeID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Username", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode",CultureCode));
            string StoredProcedureName = "[usp_SageMenuAdminGet]";
            SqlDataReader SQLReader;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName,ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }

            while (SQLReader.Read())
            {
                MenuInfo objMenu = new MenuInfo();
                objMenu.PageID=int.Parse(SQLReader["PageID"].ToString());
                objMenu.PageOrder= int.Parse(SQLReader["PageOrder"].ToString());
                objMenu.PageName=SQLReader["PageName"].ToString();
                objMenu.ParentID= int.Parse(SQLReader["ParentID"].ToString());
                objMenu.Level=int.Parse(SQLReader["Level"].ToString());
                objMenu.TabPath=SQLReader["TabPath"].ToString();
                lstPages.Add(objMenu);


            }
            return lstPages;
        }

		public static List<MenuInfo> GetAdminMenu(int PortalID)
        {
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> Parameters = new List<KeyValuePair<string, object>>();
            Parameters.Add(new KeyValuePair<string, object>("PortalID", PortalID));
            List<MenuInfo> lstPages = sagesql.ExecuteAsList<MenuInfo>("[usp_AdminMenuGet]", Parameters);
            return lstPages;
        }
    }
}
