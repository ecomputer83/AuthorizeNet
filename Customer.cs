using AuthorizeNet.APICore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AuthorizeNet
{
	/// <summary>
	/// This is an abstraction for use with the CIM API. It's a partial class so you can combine it with your class as needed.
	/// </summary>
	public class Customer
	{
		public Address BillingAddress
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		public string ID
		{
			get;
			set;
		}

		public IList<PaymentProfile> PaymentProfiles
		{
			get;
			set;
		}

		public string ProfileID
		{
			get;
			set;
		}

		public IList<Address> ShippingAddresses
		{
			get;
			set;
		}

		public Customer()
		{
			this.ID = Guid.NewGuid().ToString();
			this.ShippingAddresses = new List<Address>();
			this.PaymentProfiles = new List<PaymentProfile>();
		}

		internal static validationModeEnum ToValidationMode(ValidationMode mode)
		{
			switch (mode)
			{
				case ValidationMode.None:
				{
					return validationModeEnum.none;
				}
				case ValidationMode.TestMode:
				{
					return validationModeEnum.testMode;
				}
				case ValidationMode.LiveMode:
				{
					return validationModeEnum.liveMode;
				}
			}
			return (validationModeEnum)mode;
		}
	}
}