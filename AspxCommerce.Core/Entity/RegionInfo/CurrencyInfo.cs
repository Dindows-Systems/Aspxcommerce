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
   public class CurrencyInfo
    {
        private string _currencyName;
        private string _currencyCode;
        private string _countryName;
        private string _currencySymbol;
        private string _baseImage;
        private string _region;

        public CurrencyInfo()
        {
        }

        [DataMember]
        public string CurrencyName
        {
            get
            {
                return this._currencyName;
            }
            set
            {
                if ((this._currencyName) != value)
                {
                    this._currencyName = value;
                }
            }

        }
        
        [DataMember]
        public string CurrencyCode
        {
            get
            {
                return this._currencyCode;
            }
            set
            {
                if ((this._currencyCode) != value)
                {
                    this._currencyCode = value;
                }
            }
           
        }
        [DataMember]
        public string CountryName
        {
            get
            {
                return this._countryName;
            }
            set
            {
                if ((this._countryName) != value)
                {
                    this._countryName = value;
                }
            }

        }
        [DataMember]
        public string CurrencySymbol
        {
            get
            {
                return this._currencySymbol;
            }
            set
            {
                if ((this._currencySymbol) != value)
                {
                    this._currencySymbol = value;
                }
            }

        }
        [DataMember]
        public string BaseImage
        {
            get
            {
                return this._baseImage;
            }
            set
            {
                if ((this._baseImage) != value)
                {
                    this._baseImage = value;
                }
            }

        }
        [DataMember]
        public string Region
        {
            get
            {
                return this._region;
            }
            set
            {
                if ((this._region) != value)
                {
                    this._region = value;
                }
            }

        }
    }
}
