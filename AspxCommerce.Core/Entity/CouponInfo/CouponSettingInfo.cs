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
    public class CouponSettingInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_settingID", Order = 1)]
        private System.Nullable<int> _settingID;

        [DataMember(Name = "_settingKey", Order = 2)]
        private string _settingKey;

        [DataMember(Name = "_inputTypeID", Order = 3)]
        private System.Nullable<int> _inputTypeID;

        [DataMember(Name = "_inputType", Order = 4)]
        private string _inputType;

        [DataMember(Name = "_addedOn", Order = 5)]
        private System.Nullable<System.DateTime> _addedOn;

        [DataMember(Name = "_isActive", Order = 6)]
        private System.Nullable<bool> _isActive;


        public CouponSettingInfo()
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


        public System.Nullable<int> SettingID
        {
            get
            {
                return this._settingID;
            }
            set
            {
                if ((this._settingID != value))
                {
                    this._settingID = value;
                }
            }
        }


        public string SettingKey
        {
            get
            {
                return this._settingKey;
            }
            set
            {
                if ((this._settingKey != value))
                {
                    this._settingKey = value;
                }
            }
        }

        public System.Nullable<int> InputTypeID
        {
            get
            {
                return this._inputTypeID;
            }
            set
            {
                if ((this._inputTypeID != value))
                {
                    this._inputTypeID = value;
                }
            }
        }

        public string InputType
        {
            get
            {
                return this._inputType;
            }
            set
            {
                if ((this._inputType != value))
                {
                    this._inputType = value;
                }
            }
        }

        public System.Nullable<System.DateTime> AddedOn
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

    }
}
