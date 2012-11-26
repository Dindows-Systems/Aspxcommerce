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
    public class ReturnRequestsReason
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
            private System.Nullable<int> _rowTotal;
        [DataMember(Name = "_reasonID", Order = 1)]
            private int _reasonID;
        [DataMember(Name = "_reason", Order = 2)]
            private string _reason;
        [DataMember(Name = "_displayOrder", Order = 3)]
            private int _displayOrder;

        [DataMember(Name = "_isActive", Order = 4)]
        private System.Nullable<bool> _isActive;

            private System.Nullable<long> _rowNumber;

            public ReturnRequestsReason()
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

            public int ReasonID
            {
                get
                {
                    return this._reasonID;
                }
                set
                {
                    if ((this._reasonID != value))
                    {
                        this._reasonID = value;
                    }
                }
            }

            public string Reason
            {
                get
                {
                    return this._reason;
                }
                set
                {
                    if ((this._reason != value))
                    {
                        this._reason = value;
                    }
                }
            }

            public int DisplayOrder
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

