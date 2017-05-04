using AuthorizeNet.APICore;
using System;
using System.Runtime.CompilerServices;

namespace AuthorizeNet
{
	/// <summary>
	/// This is an Address abstraction used for Billing and Shipping
	/// </summary>
	public class Address
	{
		public string City
		{
			get;
			set;
		}

		public string Company
		{
			get;
			set;
		}

		public string Country
		{
			get;
			set;
		}

		public string Fax
		{
			get;
			set;
		}

		public string First
		{
			get;
			set;
		}

		public string ID
		{
			get;
			set;
		}

		public string Last
		{
			get;
			set;
		}

		public string Phone
		{
			get;
			set;
		}

		public string State
		{
			get;
			set;
		}

		public string Street
		{
			get;
			set;
		}

		public string Zip
		{
			get;
			set;
		}

		public Address()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.Address" /> class, resolving the given API Type
		/// </summary>
		/// <param name="fromType">From type.</param>
		public Address(nameAndAddressType fromType)
		{
			this.City = fromType.city;
			this.Company = fromType.company;
			this.Country = fromType.country;
			this.Last = fromType.lastName;
			this.First = fromType.firstName;
			this.Street = fromType.address;
			this.Zip = fromType.zip;
			this.State = fromType.state;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.Address" /> class, resolving the given API Type
		/// </summary>
		/// <param name="fromType">From type.</param>
		public Address(customerAddressType fromType)
		{
			this.City = fromType.city;
			this.Company = fromType.company;
			this.Country = fromType.country;
			this.Last = fromType.lastName;
			this.First = fromType.firstName;
			this.Street = fromType.address;
			this.Fax = fromType.faxNumber;
			this.Phone = fromType.phoneNumber;
			this.Zip = fromType.zip;
			this.State = fromType.state;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.Address" /> class, resolving the given API Type
		/// </summary>
		/// <param name="fromType">From type.</param>
		public Address(customerAddressExType fromType)
		{
			this.City = fromType.city;
			this.Company = fromType.company;
			this.Country = fromType.country;
			this.ID = fromType.customerAddressId;
			this.Last = fromType.lastName;
			this.First = fromType.firstName;
			this.Street = fromType.address;
			this.Fax = fromType.faxNumber;
			this.Phone = fromType.phoneNumber;
			this.State = fromType.state;
			this.Zip = fromType.zip;
		}

		/// <summary>
		/// Creates an API type for use with outbound requests to the Gateway. Mostly for internal use.
		/// </summary>
		/// <returns></returns>
		public customerAddressExType ToAPIExType()
		{
			customerAddressExType result = new customerAddressExType()
			{
				address = this.Street,
				city = this.City,
				company = this.Company,
				country = this.Country,
				faxNumber = this.Fax,
				firstName = this.First,
				lastName = this.Last,
				phoneNumber = this.Phone,
				state = this.State,
				zip = this.Zip,
				customerAddressId = this.ID
			};
			return result;
		}

		/// <summary>
		/// Creates an API type for use with outbound requests to the Gateway. Mostly for internal use.
		/// </summary>
		/// <returns></returns>
		public nameAndAddressType ToAPINameAddressType()
		{
			nameAndAddressType result = new nameAndAddressType()
			{
				address = this.Street,
				city = this.City,
				company = this.Company,
				country = this.Country,
				firstName = this.First,
				lastName = this.Last,
				state = this.State,
				zip = this.Zip
			};
			return result;
		}

		/// <summary>
		/// Creates an API type for use with outbound requests to the Gateway. Mostly for internal use.
		/// </summary>
		/// <returns></returns>
		public customerAddressType ToAPIType()
		{
			customerAddressType result = new customerAddressType()
			{
				address = this.Street,
				city = this.City,
				company = this.Company,
				country = this.Country,
				faxNumber = this.Fax,
				firstName = this.First,
				lastName = this.Last,
				phoneNumber = this.Phone,
				state = this.State,
				zip = this.Zip
			};
			return result;
		}
	}
}