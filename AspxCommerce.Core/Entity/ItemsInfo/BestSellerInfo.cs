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
    [Serializable]
    [DataContract]
     public class BestSellerInfo
    {


        [DataMember]
        private string _sku;
        [DataMember]
        private string _itemName;
        [DataMember]
        private int _itemID;

        [DataMember]
        private string _imagePath;
        [DataMember]
        private System.Nullable<int> _soldItem;
        [DataMember]
        private int _count;

        public BestSellerInfo()
        {
        }

      
        public string Sku
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

      
        public string ItemName
        {
            get
            {
                return this._itemName;
            }
            set
            {
                if ((this._itemName != value))
                {
                    this._itemName = value;
                }
            }
        }

       
        public int ItemID
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

        
        public string ImagePath
        {
            get
            {
                return this._imagePath;
            }
            set
            {
                if ((this._imagePath != value))
                {
                    this._imagePath = value;
                }
            }
        }

      
        public System.Nullable<int> SoldItem
        {
            get
            {
                return this._soldItem;
            }
            set
            {
                if ((this._soldItem != value))
                {
                    this._soldItem = value;
                }
            }
        }
        public int Count
        {
            get
            {
                return this._count;
            }
            set
            {
                if ((this._count != value))
                {
                    this._count = value;
                }
            }
        }
       
    }
}
