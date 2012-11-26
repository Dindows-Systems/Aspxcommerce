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
    public class TotalOrderAmountInfo
    {
        public TotalOrderAmountInfo()
        {
        }

        [DataMember(Name = "_revenue", Order = 0)]
        private System.Nullable<decimal> _revenue;

        [DataMember(Name = "_taxTotal", Order = 1)]
        private System.Nullable<decimal> _taxTotal;

        [DataMember(Name = "_quantity", Order = 2)]
        private System.Nullable<int> _quantity;

        [DataMember(Name = "_shippingCost", Order = 3)]
        private System.Nullable<decimal> _shippingCost;


        public System.Nullable<decimal> Revenue
        {
            get
            {
                return this._revenue;
            }
            set
            {
                if ((this._revenue != value))
                {
                    this._revenue = value;
                }
            }
        }

        public System.Nullable<decimal> TaxTotal
        {
            get
            {
                return this._taxTotal;
            }
            set
            {
                if ((this._taxTotal != value))
                {
                    this._taxTotal = value;
                }
            }
        }

        public System.Nullable<int> Quantity
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

        public System.Nullable<decimal> ShippingCost
        {
            get
            {
                return this._shippingCost;
            }
            set
            {
                if ((this._shippingCost != value))
                {
                    this._shippingCost = value;
                }
            }
        }
    }
}
