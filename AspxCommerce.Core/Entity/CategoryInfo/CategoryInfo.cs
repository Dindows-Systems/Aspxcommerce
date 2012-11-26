
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
{
    [DataContract]
    [Serializable]
    public class CategoryInfo
    {
        private Int32 _categoryID;
        private string _categoryName;
        private string _levelCategoryName;
        private Int32 _parentID;
        private System.Nullable<Int32> _categoryLevel;
        private string _path;
        private System.Nullable<bool> _isShowInSearch;
        private System.Nullable<bool> _isShowInCatalog;
        private System.Nullable<bool> _isShowInMenu;
        private System.Nullable<DateTime> _activeFrom;
        private System.Nullable<DateTime> _activeTo;
        private Int32 _storeID;
        private Int32 _portalID;
        private System.Nullable<bool> _isActive;
        private System.Nullable<Int32> _displayOrder;
        private System.Nullable<bool> _isChecked;

        private string _attributeValue;

        private Int32 _childCount;

        [DataMember]
        public Int32 CategoryID
        {
            get
            {
                return this._categoryID;
            }
            set
            {
                    this._categoryID = value;
            }
        }

        [DataMember]
        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                    _categoryName = value;
            }
        }

        [DataMember]
        public string LevelCategoryName
        {
            get
            {
                return _levelCategoryName;
            }
            set
            {
                _levelCategoryName = value;
            }
        }


        [DataMember]
        public Int32 ParentID
        {
            get
            {
                return this._parentID;
            }
            set
            {
                    this._parentID = value;
            }
        }
        [DataMember]
        public System.Nullable<Int32> CategoryLevel
        {
            get
            {
                return this._categoryLevel;
            }
            set
            {
                this._categoryLevel = value;
            }
        }

        [DataMember]
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }
        [DataMember]
        public System.Nullable<bool> IsShowInSearch
        {
            get
            {
                return _isShowInSearch;
            }
            set
            {
                _isShowInSearch = value;
            }
        }

        [DataMember]
        public System.Nullable<bool> IsShowInCatalog
        {
            get
            {
                return _isShowInCatalog;
            }
            set
            {
                _isShowInCatalog = value;
            }
        }

        [DataMember]
        public System.Nullable<bool> IsShowInMenu
        {
            get
            {
                return _isShowInMenu;
            }
            set
            {
                _isShowInMenu = value;
            }
        }

        [DataMember]
        public System.Nullable<DateTime> ActiveFrom
        {
            get
            {
                return _activeFrom;
            }
            set
            {
                _activeFrom = value;
            }
        }

        [DataMember]
        public System.Nullable<DateTime> ActiveTo
        {
            get
            {
                return _activeTo;
            }
            set
            {
                _activeTo = value;
            }
        }

        [DataMember]
        public Int32 StoreID
        {
            get
            {
                return this._storeID;
            }
            set
            {
                this._storeID = value;
            }
        }

        [DataMember]
        public Int32 PortalID
        {
            get
            {
                return this._portalID;
            }
            set
            {
                this._portalID = value;
            }
        }

        [DataMember]
        public System.Nullable<bool> IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }
        [DataMember]
        public System.Nullable<Int32> DisplayOrder
        {
            get
            {
                return this._displayOrder;
            }
            set
            {
                this._displayOrder = value;
            }
        }

        [DataMember]
        public System.Nullable<bool> IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
            }
        }

        [DataMember]
        public string AttributeValue
        {
            get
            {
                return this._attributeValue;
            }
            set
            {
                this._attributeValue = value;
            }
        }

        [DataMember]
        public Int32 ChildCount
        {
            get
            {
                return this._childCount;
            }
            set
            {
                this._childCount = value;
            }
        }
    }
}
