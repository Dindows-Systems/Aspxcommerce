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
    public class ReturnRequestList
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
       private System.Nullable<int> _rowTotal;
        [DataMember(Name = "_returnID", Order =1)]
		private int _returnID;
        [DataMember(Name = "_userName", Order = 3)]
		private string _userName;
        [DataMember(Name = "_reason", Order = 4)]
		private string _reason;
        [DataMember(Name = "_addedOn", Order = 5)]
        private DateTime _addedOn;
        [DataMember(Name = "_status", Order = 2)]
		private string _status;
        [DataMember(Name = "_orderID", Order = 7)]
		private System.Nullable<int> _orderID;
        [DataMember(Name = "_quantity", Order = 8)]
		private System.Nullable<int> _quantity;
        [DataMember(Name = "_returnAction", Order = 6)]
		private string _returnAction;
		
		private System.Nullable<long> _rowNumber;

        public ReturnRequestList()
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
		
		
		public int ReturnID
		{
			get
			{
				return this._returnID;
			}
			set
			{
				if ((this._returnID != value))
				{
					this._returnID = value;
				}
			}
		}
		
		
		public string UserName
		{
			get
			{
				return this._userName;
			}
			set
			{
				if ((this._userName != value))
				{
					this._userName = value;
				}
			}
		}

        public DateTime AddedOn
        {
            get
            {
                return this._addedOn;
            }
            set
            {
                if ((this._addedOn != value))
                {
                    this._addedOn = value;
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
		
		
		public System.Nullable<int> OrderID
		{
			get
			{
				return this._orderID;
			}
			set
			{
				if ((this._orderID != value))
				{
					this._orderID = value;
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
		
		
		public string ReturnAction
		{
			get
			{
				return this._returnAction;
			}
			set
			{
				if ((this._returnAction != value))
				{
					this._returnAction = value;
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
