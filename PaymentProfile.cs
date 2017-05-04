using AuthorizeNet.APICore;
using System;
using System.Runtime.CompilerServices;

namespace AuthorizeNet
{
	/// <summary>
	/// An abstraction for the AuthNET API, allowing you store credit card information with Authorize.net
	/// </summary>
	public class PaymentProfile
	{
		public Address BillingAddress
		{
			get;
			set;
		}

		public string CardCode
		{
			get;
			set;
		}

		public string CardExpiration
		{
			get;
			set;
		}

		public string CardNumber
		{
			get;
			set;
		}

		public string CardType
		{
			get;
			set;
		}

		public string DriversLicenseDOB
		{
			get;
			set;
		}

		public string DriversLicenseNumber
		{
			get;
			set;
		}

		public string DriversLicenseState
		{
			get;
			set;
		}

		public bool IsBusiness
		{
			get;
			set;
		}

		public string ProfileID
		{
			get;
			set;
		}

		public string TaxID
		{
			get;
			set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.PaymentProfile" /> class, using the passed-in API type to create the profile.
		/// </summary>
		/// <param name="apiType">Type of the API.</param>
		public PaymentProfile(customerPaymentProfileMaskedType apiType)
		{
			if (apiType.billTo != null)
			{
				this.BillingAddress = new Address(apiType.billTo);
			}
			this.ProfileID = apiType.customerPaymentProfileId;
			if (apiType.driversLicense != null)
			{
				this.DriversLicenseNumber = apiType.driversLicense.number;
				this.DriversLicenseState = apiType.driversLicense.state;
				this.DriversLicenseDOB = apiType.driversLicense.dateOfBirth;
			}
			if (!apiType.customerTypeSpecified)
			{
				this.IsBusiness = false;
			}
			else
			{
				this.IsBusiness = apiType.customerType == customerTypeEnum.business;
			}
			if (apiType.payment != null)
			{
				creditCardMaskedType card = (creditCardMaskedType)apiType.payment.Item;
				this.CardType = card.cardType;
				this.CardNumber = card.cardNumber;
				this.CardExpiration = card.expirationDate;
			}
			this.TaxID = apiType.taxId;
		}

		/// <summary>
		/// Creates an API object, ready to send to AuthNET servers.
		/// </summary>
		/// <returns></returns>
		public customerPaymentProfileExType ToAPI()
		{
			customerPaymentProfileExType result = new customerPaymentProfileExType()
			{
				billTo = this.BillingAddress.ToAPIType(),
				customerPaymentProfileId = this.ProfileID
			};
			if (!string.IsNullOrEmpty(this.DriversLicenseNumber))
			{
				result.driversLicense = new driversLicenseType()
				{
					dateOfBirth = this.DriversLicenseDOB,
					number = this.DriversLicenseNumber,
					state = this.DriversLicenseState
				};
			}
			if (!this.IsBusiness)
			{
				result.customerType = customerTypeEnum.individual;
			}
			else
			{
				result.customerType = customerTypeEnum.business;
			}
			result.customerTypeSpecified = true;
			if (!string.IsNullOrEmpty(this.CardNumber))
			{
				creditCardType card = new creditCardType()
				{
					cardCode = this.CardCode,
					cardNumber = this.CardNumber,
					expirationDate = this.CardExpiration
				};
				result.payment.Item = card;
			}
			if (!string.IsNullOrEmpty(this.TaxID))
			{
				result.taxId = this.TaxID;
			}
			return result;
		}
	}
}