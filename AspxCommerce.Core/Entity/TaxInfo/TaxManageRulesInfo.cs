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
using System;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class TaxManageRulesInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_taxManageRuleID", Order = 1)]
		private int _taxManageRuleID;

        [DataMember(Name = "_taxManageRuleName", Order = 2)]
		private string _taxManageRuleName;

        [DataMember(Name = "_taxCustomerClassName", Order = 3)]
		private string _taxCustomerClassName;

        [DataMember(Name = "_taxItemClassName", Order = 4)]
		private string _taxItemClassName;

        [DataMember(Name = "_taxRateTitle", Order = 5)]
		private string _taxRateTitle;

        [DataMember(Name = "_priority", Order = 6)]
		private System.Nullable<int> _priority;

        [DataMember(Name = "_displayOrder", Order = 7)]
		private System.Nullable<int> _displayOrder;

        public TaxManageRulesInfo()
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

		public int TaxManageRuleID
		{
			get
			{
				return this._taxManageRuleID;
			}
			set
			{
				if ((this._taxManageRuleID != value))
				{
					this._taxManageRuleID = value;
				}
			}
		}

		public string TaxManageRuleName
		{
			get
			{
				return this._taxManageRuleName;
			}
			set
			{
				if ((this._taxManageRuleName != value))
				{
					this._taxManageRuleName = value;
				}
			}
		}

		public string TaxCustomerClassName
		{
			get
			{
				return this._taxCustomerClassName;
			}
			set
			{
				if ((this._taxCustomerClassName != value))
				{
					this._taxCustomerClassName = value;
				}
			}
		}

		public string TaxItemClassName
		{
			get
			{
				return this._taxItemClassName;
			}
			set
			{
				if ((this._taxItemClassName != value))
				{
					this._taxItemClassName = value;
				}
			}
		}

		public string TaxRateTitle
		{
			get
			{
				return this._taxRateTitle;
			}
			set
			{
				if ((this._taxRateTitle != value))
				{
					this._taxRateTitle = value;
				}
			}
		}

		public System.Nullable<int> Priority
		{
			get
			{
				return this._priority;
			}
			set
			{
				if ((this._priority != value))
				{
					this._priority = value;
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
    }
}
