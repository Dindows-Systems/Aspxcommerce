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
    public class CouponSettingKeyInfo
    {

        [DataMember(Name = "_settingID", Order = 1)]
        private System.Nullable<int> _settingID;

        [DataMember(Name = "_settingKey", Order = 2)]
        private string _settingKey;

        [DataMember(Name = "_validationTypeID", Order = 3)]
        private System.Nullable<int> _validationTypeID;

        [DataMember(Name = "_validationType", Order = 4)]
        private string _validationType;

        public CouponSettingKeyInfo()
        {
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

        public System.Nullable<int> ValidationTypeID
        {
            get
            {
                return this._validationTypeID;
            }
            set
            {
                if ((this._validationTypeID != value))
                {
                    this._validationTypeID = value;
                }
            }
        }

        public string ValidationType
        {
            get
            {
                return this._validationType;
            }
            set
            {
                if ((this._validationType != value))
                {
                    this._validationType = value;
                }
            }
        }

   

    }
}
