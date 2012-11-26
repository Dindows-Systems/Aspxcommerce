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
    public class TopCustomerOrdererInfo
    {
        public TopCustomerOrdererInfo()
        {
        }

        [DataMember(Name = "_customerName", Order = 0)]
        private string _customerName;

        [DataMember(Name = "_numberOfOrder", Order = 1)]
        private System.Nullable<int> _numberOfOrder;

        [DataMember(Name = "_averageOrderAmount", Order = 2)]
        private System.Nullable<decimal> _averageOrderAmount;

        [DataMember(Name = "_totalOrderAmount", Order = 3)]
        private System.Nullable<decimal> _totalOrderAmount;


        public string CustomerName
        {
            get
            {
                return this._customerName;
            }
            set
            {
                if ((this._customerName != value))
                {
                    this._customerName = value;
                }
            }
        }

        public System.Nullable<int> NumberOfOrder
        {
            get
            {
                return this._numberOfOrder;
            }
            set
            {
                if ((this._numberOfOrder != value))
                {
                    this._numberOfOrder = value;
                }
            }
        }

        public System.Nullable<decimal> AverageOrderAmount
        {
            get
            {
                return this._averageOrderAmount;
            }
            set
            {
                if ((this._averageOrderAmount != value))
                {
                    this._averageOrderAmount = value;
                }
            }
        }

        public System.Nullable<decimal> TotalOrderAmount
        {
            get
            {
                return this._totalOrderAmount;
            }
            set
            {
                if ((this._totalOrderAmount != value))
                {
                    this._totalOrderAmount = value;
                }
            }
        }
    }
   
}
