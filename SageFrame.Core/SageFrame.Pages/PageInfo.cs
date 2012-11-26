/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2010 by SageFrame
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
using System.Linq;
using System.Web;

namespace SageFrame.Web
{
    [Serializable]
    public class PageInfo
    {

        #region "Private Members"

        private int _PageID;
        private int _PageOrder;
        private int _PortalID;
        private string _PageName;
        private bool _IsVisible;
        private int _ParentId;
        private int _Level;
        private string _IconFile;
        private bool _DisableLink= false;
        private string _Title;
        private string _Description;
        private string _KeyWords;
        private bool _IsActive;
        private string _SEOName;
        private bool _IsDeleted;
        private string _Url;
        private string _PagePath;
        private System.DateTime _StartDate;
        private System.DateTime _EndDate;
        private decimal _RefreshInterval;
        private string _PageHeadText;
        private bool _IsSecure;
        private bool _IsShownInFooter;
        private bool _IsRequiredPage;


        //private Security.Permissions.PagePermissionCollection _PagePermissions;
        //private HashPagele _PageSettings;

        #endregion

        #region "Constructors"

        public PageInfo()
        {
            //initialize the properties that
            //can be null in the Pages
            
        }

        #endregion

        #region "Public Properties"

        #region "Page Properties"


        public int PageID
        {
            get { return _PageID; }
            set { _PageID = value; }
        }


        public int PageOrder
        {
            get { return _PageOrder; }
            set { _PageOrder = value; }
        }


        public int PortalID
        {
            get { return _PortalID; }
            set { _PortalID = value; }
        }


        public string PageName
        {
            get { return _PageName; }
            set { _PageName = value; }
        }


        public bool IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }


        public int ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }


        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }


        public string IconFile
        {
            get { return _IconFile; }
            set { _IconFile = value; }
        }

        public bool DisableLink
        {
            get { return _DisableLink; }
            set { _DisableLink = value; }
        }


        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }


        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


        public string KeyWords
        {
            get { return _KeyWords; }
            set { _KeyWords = value; }
        }


        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public string SEOName
        {
            get { return _SEOName; }
            set { _SEOName = value; }
        }                 
        
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        public string PagePath
        {
            get { return _PagePath; }
            set { _PagePath = value; }
        }


        public System.DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }


        public System.DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public decimal RefreshInterval
        {
            get { return _RefreshInterval; }
            set { _RefreshInterval = value; }
        }


        public string PageHeadText
        {
            get { return _PageHeadText; }
            set { _PageHeadText = value; }
        }

        public bool IsSecure
        {
            get { return _IsSecure; }
            set { _IsSecure = value; }
        }

        public bool IsShownInFooter
        {
            get { return _IsShownInFooter; }
            set { _IsShownInFooter = value; }
        }

        public bool IsRequiredPage
        {
            get { return _IsRequiredPage; }
            set { _IsRequiredPage = value; }
        }

        #endregion

        #region "Page Permission Properties"


        //public Security.Permissions.PagePermissionCollection PagePermissions
        //{
        //    get
        //    {
        //        if (_PagePermissions == null)
        //        {
        //            _PagePermissions = new PagePermissionCollection(PagePermissionController.GetPagePermissions(PageID, PortalID));
        //        }
        //        return _PagePermissions;
        //    }
        //}

        #endregion

        #endregion
    }
}
