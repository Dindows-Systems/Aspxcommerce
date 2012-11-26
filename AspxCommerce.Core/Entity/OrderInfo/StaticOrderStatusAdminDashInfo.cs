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
    public class StaticOrderStatusAdminDashInfo
    {
        public StaticOrderStatusAdminDashInfo()
        {
        }

        [DataMember]
        private string _statusName;

        [DataMember]
        private System.Nullable<decimal> _thisDay;

        [DataMember]
        private System.Nullable<decimal> _thisWeek;

        [DataMember]
        private System.Nullable<decimal> _thisMonth;

        [DataMember]
        private System.Nullable<decimal> _thisYear;

        public string StatusName
        {
            get
            {
                return this._statusName;
            }
            set
            {
                if ((this._statusName != value))
                {
                    this._statusName = value;
                }
            }
        }

        public System.Nullable<decimal> ThisDay
        {
            get
            {
                return this._thisDay;
            }
            set
            {
                if ((this._thisDay != value))
                {
                    this._thisDay = value;
                }
            }
        }

        public System.Nullable<decimal> ThisWeek
        {
            get
            {
                return this._thisWeek;
            }
            set
            {
                if ((this._thisWeek != value))
                {
                    this._thisWeek = value;
                }
            }
        }

        public System.Nullable<decimal> ThisMonth
        {
            get
            {
                return this._thisMonth;
            }
            set
            {
                if ((this._thisMonth != value))
                {
                    this._thisMonth = value;
                }
            }
        }

        public System.Nullable<decimal> ThisYear
        {
            get
            {
                return this._thisYear;
            }
            set
            {
                if ((this._thisYear != value))
                {
                    this._thisYear = value;
                }
            }
        }


    }
}
