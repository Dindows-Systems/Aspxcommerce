using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AspxCommerce.PayPal
{
    [DataContract]
    [Serializable]   
    public class PayPalSettingInfo
    {
         private string _ReturnUrl;
 
		private string _CancelUrl;
    
		private string _BusinessAccount;
 
		private string _VerificationUrl;
        private string  _IsTestPaypal;
        private string _authToken;
        public PayPalSettingInfo()
        {
        }
        [DataMember]
		public string ReturnUrl
		{
			get
			{
				return this._ReturnUrl;
			}
			set
			{
				if ((this._ReturnUrl != value))
				{
					this._ReturnUrl = value;
				}
			}
		}

        [DataMember]
		public string CancelUrl
		{
			get
			{
				return this._CancelUrl;
			}
			set
			{
				if ((this._CancelUrl != value))
				{
					this._CancelUrl = value;
				}
			}
		}

        [DataMember]
		public string BusinessAccount
		{
			get
			{
				return this._BusinessAccount;
			}
			set
			{
				if ((this._BusinessAccount != value))
				{
					this._BusinessAccount = value;
				}
			}
		}

        [DataMember]
		public string VerificationUrl
		{
			get
			{
				return this._VerificationUrl;
			}
			set
			{
				if ((this._VerificationUrl != value))
				{
					this._VerificationUrl = value;
				}
			}
		}
        [DataMember]
        public string IsTestPaypal
        {
            get
            {
                return this._IsTestPaypal;
            }
            set
            {
                if ((this._IsTestPaypal != value))
                {
                    this._IsTestPaypal = value;
                }
            }
        }
        [DataMember]
        public string AuthToken
        {
            get
            {
                return this._authToken;
            }
            set
            {
                if ((this._authToken != value))
                {
                    this._authToken = value;
                }
            }
        }
		
    }
}
