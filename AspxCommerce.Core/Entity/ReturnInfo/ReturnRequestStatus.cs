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
    public class ReturnRequestStatus
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;
        [DataMember(Name = "_statusID", Order = 1)]
        private int _statusID;
        [DataMember(Name = "_status", Order = 2)]
        private string _status;
        [DataMember(Name = "_displayOrder", Order = 3)]
        private System.Nullable<int> _displayOrder;
        [DataMember(Name = "_isActive", Order = 4)]
        private System.Nullable<bool> _isActive;

        private System.Nullable<long> _rowNumber;

        public ReturnRequestStatus()
        {
        }

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
        public System.Nullable<bool> IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if ((this._isActive != value))
                {
                    this._isActive = value;
                }
            }
        }

        public int StatusID
        {
            get
            {
                return this._statusID;
            }
            set
            {
                if ((this._statusID != value))
                {
                    this._statusID = value;
                }
            }
        }
        
        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if ((this._status != value))
                {
                    this._status = value;
                }
            }
        }

        public System.Nullable<int> DisplayOrder
        {
            get
            {
                return this._displayOrder;
            }
            set
            {
                if ((this._displayOrder != value))
                {
                    this._displayOrder = value;
                }
            }
        }

        public System.Nullable<long> RowNumber
        {
            get
            {
                return this._rowNumber;
            }
            set
            {
                if ((this._rowNumber != value))
                {
                    this._rowNumber = value;
                }
            }
        }

    }
}
