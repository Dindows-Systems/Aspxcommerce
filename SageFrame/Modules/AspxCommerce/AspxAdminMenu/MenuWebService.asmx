<%@ WebService Language="C#"  Class="MenuWebService" %>
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using SageFrame.Framework;
using SageFrame.SageMenu;
using System.Collections.Generic;
using SageFrame.Common;


    /// <summary>
    /// Summary description for MenuWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]   
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
public class MenuWebService : System.Web.Services.WebService
    {

        public MenuWebService()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public List<MenuInfo> GetBackEndMenu(int PortalID, string UserName, string CultureCode, int UserMode, string PortalSEOName)
        {
            try
            {
                List<MenuInfo> lstMenu = MenuController.GetBackEndMenu(UserName, PortalID, CultureCode, UserMode);
                string PortalName = PortalSEOName.ToLower() != "default" ? "/portal/" + PortalSEOName : "";
                lstMenu.Add(new MenuInfo(2, 0, "Admin", 0, PortalName + "/Admin/Admin.aspx"));
                if (UserMode == 1)
                {
                    lstMenu.Add(new MenuInfo(3, 0, "SuperUser", 0, PortalName + "/Super-User/Super-User.aspx"));
                }
                foreach (MenuInfo obj in lstMenu)
                {
                    obj.ChildCount = lstMenu.Count(
                        delegate(MenuInfo objMenu)
                        {
                            return (objMenu.ParentID == obj.PageID);
                        }
                        );
                }
                return lstMenu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<MenuInfo> GetMenuFront(int PortalID, string UserName, string CultureCode)
        {
            List<MenuInfo> lstMenu = MenuController.GetMenuFront(PortalID, UserName, CultureCode);
            foreach (MenuInfo obj in lstMenu)
            {
                obj.ChildCount = lstMenu.Count(
                    delegate(MenuInfo objMenu)
                    {
                        return (objMenu.ParentID == obj.PageID);
                    }
                    );
            }
            return lstMenu;
        }
        [WebMethod]
        public List<MenuInfo> GetPages()
        {
            List<MenuInfo> lstMenu = MenuController.GetMenuFront(1, "superuser", "en-US");
            foreach (MenuInfo obj in lstMenu)
            {
                obj.ChildCount = lstMenu.Count(
                    delegate(MenuInfo objMenu)
                    {
                        return (objMenu.ParentID == obj.PageID);
                    }
                    );
            }
            return lstMenu;
        }

        [WebMethod]
        public List<MenuInfo> GetFooterMenu(int PortalID, string UserName, string CultureCode)
        {
            List<MenuInfo> lstMenu = MenuController.GetFooterMenu(PortalID, UserName, CultureCode);
            foreach (MenuInfo obj in lstMenu)
            {
                obj.ChildCount = lstMenu.Count(
                    delegate(MenuInfo objMenu)
                    {
                        return (objMenu.ParentID == obj.PageID);
                    }
                    );
            }
            return lstMenu;
        }

        [WebMethod]
        public List<MenuInfo> GetSideMenu(int PortalID, string UserName, string PageName, string CultureCode)
        {
            List<MenuInfo> lstMenu = MenuController.GetSideMenu(PortalID, UserName, PageName, CultureCode);
            foreach (MenuInfo obj in lstMenu)
            {
                obj.ChildCount = lstMenu.Count(
                    delegate(MenuInfo objMenu)
                    {
                        return (objMenu.ParentID == obj.PageID);
                    }
                    );
            }
            return lstMenu;
        }

        [WebMethod]
        public List<MenuInfo> GetAdminMenu()
        {           
            List<MenuInfo> lstMenu = MenuController.GetAdminMenu(1);
            foreach (MenuInfo obj in lstMenu)
            {
                obj.ChildCount = lstMenu.Count(
                    delegate(MenuInfo objMenu)
                    {
                        return (objMenu.ParentID == obj.PageID);
                    }
                    );
            }
            return lstMenu;
        }

        [WebMethod]
        public SageMenuSettingInfo GetMenuSettings(int PortalID, int UserModuleID)
        {
            return (MenuController.GetMenuSetting(PortalID, UserModuleID));
        }

        [WebMethod]
        public void SaveMenuSetting(List<SageMenuSettingInfo> lstMenuSetting)
        {
            MenuController.UpdateSageMenuSettings(lstMenuSetting);
        }

    }



