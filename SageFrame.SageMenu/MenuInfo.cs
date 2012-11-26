using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.SageMenu
{
    public class MenuInfo
    {
        public int PageID { get; set; }
        public int PageOrder { get; set; }
        public string PageName { get; set; }
        public int ParentID { get; set; }
        public int Level { get; set; }
        public string LevelPageName { get; set; }
        public int ChildCount { get; set; }
        public string SEOName { get; set; }
        public string TabPath { get; set; }
        public bool IsVisible { get; set; }
        public bool ShowInMenu { get; set; }
        public List<MenuInfo> LISTMenu { get; set; }
        public MenuInfo() { }
        public MenuInfo(int _PageID, int _PageOrder, string _PageName, int _ParentID, int _Level,string _LevelPageName,string _SEOName,string _TabPath,bool _IsVisible,bool _ShowInMenu)
        {
            this.PageID = _PageID;
            this.PageOrder = _PageOrder;
            this.PageName = _PageName;
            this.ParentID = _ParentID;
            this.Level = _Level;
            this.LevelPageName = _LevelPageName;
            this.SEOName = _SEOName;
            this.TabPath = _TabPath;
            this.IsVisible = _IsVisible;
            this.ShowInMenu = _ShowInMenu;
        }

        public MenuInfo(int _PageID, int _ParentID, string _PageName, int _Level,string _TabPath)
        {
            this.PageID = _PageID;
            this.ParentID = _ParentID;
            this.PageName = _PageName;
            this.Level = _Level;
            this.TabPath = _TabPath;
        }
    }
}
