﻿/*
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
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    public class CategoryDetailsOptionsInfo
    {
        private System.Nullable<int> _rowTotal;
        private System.Nullable<int> _categoryID;

        private System.Nullable<int> _parentID;

        private string _path;

        private System.Nullable<bool> _isRootCategory;

        private System.Nullable<int> _portalID;

        private System.Nullable<int> _storeID;

        private string _categoryName;

        private string _categoryImage;

        private System.Nullable<int> _itemID;

        private string _sku;

        private System.Nullable<int> _itemTypeID;

        private string _itemTypeName;

        private System.Nullable<int> _attributeSetID;

        private string _attributeSetName;

        private string _name;

        private string _price;

        private string _listPrice;

        private string _quantity;

        private string _visibility;

        private string _description;

        private string _shortDescription;

        private System.Nullable<bool> _isOutOfStock;

        private string _isFeatured;

        private string _baseImage;

        private System.Nullable<System.DateTime> _itemAddedOn;

        private System.Nullable<bool> _isActiveItem;

        public CategoryDetailsOptionsInfo()
        {
        }

        [DataMember]
        public System.Nullable<int> RowTotal
        {
            get
            {
                return this._rowTotal;
            }
            set
            {
                if ((this._rowTotal != value))
                {
                    this._rowTotal = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> CategoryID
        {
            get
            {
                return this._categoryID;
            }
            set
            {
                if ((this._categoryID != value))
                {
                    this._categoryID = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> ParentID
        {
            get
            {
                return this._parentID;
            }
            set
            {
                if ((this._parentID != value))
                {
                    this._parentID = value;
                }
            }
        }

        [DataMember]
        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                if ((this._path != value))
                {
                    this._path = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsRootCategory
        {
            get
            {
                return this._isRootCategory;
            }
            set
            {
                if ((this._isRootCategory != value))
                {
                    this._isRootCategory = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> PortalID
        {
            get
            {
                return this._portalID;
            }
            set
            {
                if ((this._portalID != value))
                {
                    this._portalID = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> StoreID
        {
            get
            {
                return this._storeID;
            }
            set
            {
                if ((this._storeID != value))
                {
                    this._storeID = value;
                }
            }
        }

        [DataMember]
        public string CategoryName
        {
            get
            {
                return this._categoryName;
            }
            set
            {
                if ((this._categoryName != value))
                {
                    this._categoryName = value;
                }
            }
        }

        [DataMember]
        public string CategoryImage
        {
            get
            {
                return this._categoryImage;
            }
            set
            {
                if ((this._categoryImage != value))
                {
                    this._categoryImage = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> ItemID
        {
            get
            {
                return this._itemID;
            }
            set
            {
                if ((this._itemID != value))
                {
                    this._itemID = value;
                }
            }
        }

        [DataMember]
        public string SKU
        {
            get
            {
                return this._sku;
            }
            set
            {
                if ((this._sku != value))
                {
                    this._sku = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> ItemTypeID
        {
            get
            {
                return this._itemTypeID;
            }
            set
            {
                if ((this._itemTypeID != value))
                {
                    this._itemTypeID = value;
                }
            }
        }

        [DataMember]
        public string ItemTypeName
        {
            get
            {
                return this._itemTypeName;
            }
            set
            {
                if ((this._itemTypeName != value))
                {
                    this._itemTypeName = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> AttributeSetID
        {
            get
            {
                return this._attributeSetID;
            }
            set
            {
                if ((this._attributeSetID != value))
                {
                    this._attributeSetID = value;
                }
            }
        }

        [DataMember]
        public string AttributeSetName
        {
            get
            {
                return this._attributeSetName;
            }
            set
            {
                if ((this._attributeSetName != value))
                {
                    this._attributeSetName = value;
                }
            }
        }

        [DataMember]
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if ((this._name != value))
                {
                    this._name = value;
                }
            }
        }

        [DataMember]
        public string Price
        {
            get
            {
                return this._price;
            }
            set
            {
                if ((this._price != value))
                {
                    this._price = value;
                }
            }
        }

        [DataMember]
        public string ListPrice
        {
            get
            {
                return this._listPrice;
            }
            set
            {
                if ((this._listPrice != value))
                {
                    this._listPrice = value;
                }
            }
        }

        [DataMember]
        public string Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                if ((this._quantity != value))
                {
                    this._quantity = value;
                }
            }
        }

        [DataMember]
        public string Visibility
        {
            get
            {
                return this._visibility;
            }
            set
            {
                if ((this._visibility != value))
                {
                    this._visibility = value;
                }
            }
        }

        [DataMember]
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                if ((this._description != value))
                {
                    this._description = value;
                }
            }
        }

        [DataMember]
        public string ShortDescription
        {
            get
            {
                return this._shortDescription;
            }
            set
            {
                if ((this._shortDescription != value))
                {
                    this._shortDescription = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsOutOfStock
        {
            get
            {
                return this._isOutOfStock;
            }
            set
            {
                if ((this._isOutOfStock != value))
                {
                    this._isOutOfStock = value;
                }
            }
        }

        [DataMember]
        public string IsFeatured
        {
            get
            {
                return this._isFeatured;
            }
            set
            {
                if ((this._isFeatured != value))
                {
                    this._isFeatured = value;
                }
            }
        }        

        [DataMember]
        public string BaseImage
        {
            get
            {
                return this._baseImage;
            }
            set
            {
                if ((this._baseImage != value))
                {
                    this._baseImage = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<System.DateTime> ItemAddedOn
        {
            get
            {
                return this._itemAddedOn;
            }
            set
            {
                if ((this._itemAddedOn != value))
                {
                    this._itemAddedOn = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<bool> IsActiveItem
        {
            get
            {
                return this._isActiveItem;
            }
            set
            {
                if ((this._isActiveItem != value))
                {
                    this._isActiveItem = value;
                }
            }
        }
    }
}

