using AuthorizeNet.APICore;
using System;
using System.Collections.Generic;

namespace AuthorizeNet
{
	public class CustomerGateway : ICustomerGateway
	{
		private HttpXmlUtility _gateway;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CustomerGateway" /> class.
		/// </summary>
		/// <param name="apiLogin">The API login.</param>
		/// <param name="transactionKey">The transaction key.</param>
		/// <param name="mode">Test or Live.</param>
		public CustomerGateway(string apiLogin, string transactionKey, ServiceMode mode)
		{
			if (mode == ServiceMode.Live)
			{
				this._gateway = new HttpXmlUtility(ServiceMode.Live, apiLogin, transactionKey);
				return;
			}
			this._gateway = new HttpXmlUtility(ServiceMode.Test, apiLogin, transactionKey);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CustomerGateway" /> class.
		/// </summary>
		/// <param name="apiLogin">The API login.</param>
		/// <param name="transactionKey">The transaction key.</param>
		public CustomerGateway(string apiLogin, string transactionKey) : this(apiLogin, transactionKey, ServiceMode.Test)
		{
		}

		/// <summary>
		/// Adds a credit card profile to the user and returns the profile ID
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="cardNumber">The card number.</param>
		/// <param name="expirationMonth">The expiration month.</param>
		/// <param name="expirationYear">The expiration year.</param>
		/// <param name="cardCode">The card code.</param>
		/// <returns></returns>
		public string AddCreditCard(string profileID, string cardNumber, int expirationMonth, int expirationYear, string cardCode)
		{
			return this.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear, cardCode, null);
		}

		/// <summary>
		/// Adds a credit card profile to the user and returns the profile ID
		/// </summary>
		/// <returns></returns>
		public string AddCreditCard(string profileID, string cardNumber, int expirationMonth, int expirationYear, string cardCode, Address billToAddress)
		{
			createCustomerPaymentProfileRequest req = new createCustomerPaymentProfileRequest()
			{
				customerProfileId = profileID,
				paymentProfile = new customerPaymentProfileType()
				{
					payment = new paymentType()
				}
			};
			creditCardType card = new creditCardType()
			{
				cardCode = cardCode,
				cardNumber = cardNumber
			};
			string sMonth = expirationMonth.ToString();
			if (sMonth.Length == 1)
			{
				sMonth = string.Concat("0", sMonth);
			}
			card.expirationDate = string.Format("{0}-{1}", expirationYear.ToString(), sMonth);
			req.paymentProfile.payment.Item = card;
			if (billToAddress != null)
			{
				req.paymentProfile.billTo = billToAddress.ToAPIType();
			}
			return ((createCustomerPaymentProfileResponse)this._gateway.Send(req)).customerPaymentProfileId;
		}

		/// <summary>
		/// Adds a Shipping Address to the customer profile
		/// </summary>
		public string AddShippingAddress(string profileID, string first, string last, string street, string city, string state, string zip, string country, string phone)
		{
			Address address = new Address()
			{
				First = first,
				Last = last,
				City = city,
				State = state,
				Country = country,
				Zip = zip,
				Phone = phone,
				Street = street
			};
			return this.AddShippingAddress(profileID, address);
		}

		/// <summary>
		/// Adds a Shipping Address to the customer profile
		/// </summary>
		public string AddShippingAddress(string profileID, Address address)
		{
			createCustomerShippingAddressRequest req = new createCustomerShippingAddressRequest()
			{
				address = address.ToAPIType(),
				customerProfileId = profileID
			};
			return ((createCustomerShippingAddressResponse)this._gateway.Send(req)).customerAddressId;
		}

		/// <summary>
		/// Authorizes a transaction using the supplied profile information.
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="paymentProfileID">The payment profile ID.</param>
		/// <param name="amount">The amount.</param>
		/// <returns></returns>
		public IGatewayResponse Authorize(string profileID, string paymentProfileID, decimal amount)
		{
			return this.Authorize(profileID, paymentProfileID, amount, new decimal(0), new decimal(0));
		}

		/// <summary>
		/// Authorizes a transaction using the supplied profile information with Tax and Shipping specified. If you want
		/// to add more detail here, use the 3rd option - which is to add an Order object
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="paymentProfileID">The payment profile ID.</param>
		/// <param name="amount">The amount.</param>
		/// <param name="tax">The tax.</param>
		/// <param name="shipping">The shipping.</param>
		/// <returns></returns>
		public IGatewayResponse Authorize(string profileID, string paymentProfileID, decimal amount, decimal tax, decimal shipping)
		{
			Order order = new Order(profileID, paymentProfileID, "")
			{
				Amount = amount
			};
			if (tax > new decimal(0))
			{
				order.SalesTaxAmount = tax;
				order.SalesTaxName = "Sales Tax";
			}
			if (shipping > new decimal(0))
			{
				order.ShippingAmount = shipping;
				order.ShippingName = "Shipping";
			}
			return this.Authorize(order);
		}

		/// <summary>
		/// Authorizes a transaction using the supplied profile information, abstracted through an Order object. Using the Order
		/// you can add line items, specify shipping and tax, etc.
		/// </summary>
		/// <param name="order">The order.</param>
		/// <returns>A string representing the approval code</returns>
		public IGatewayResponse Authorize(Order order)
		{
			createCustomerProfileTransactionRequest req = new createCustomerProfileTransactionRequest();
			profileTransAuthOnlyType trans = new profileTransAuthOnlyType()
			{
				customerProfileId = order.CustomerProfileID,
				customerPaymentProfileId = order.PaymentProfileID,
				amount = order.Total,
				order = new orderExType()
				{
					description = order.Description,
					invoiceNumber = order.InvoiceNumber,
					purchaseOrderNumber = order.PONumber
				}
			};
			if (!string.IsNullOrEmpty(order.ShippingAddressProfileID))
			{
				trans.customerShippingAddressId = order.ShippingAddressProfileID;
			}
			if (order.SalesTaxAmount > new decimal(0))
			{
				extendedAmountType _extendedAmountType = new extendedAmountType()
				{
					amount = order.SalesTaxAmount,
					description = order.SalesTaxName,
					name = order.SalesTaxName
				};
				trans.tax = _extendedAmountType;
			}
			if (order.ShippingAmount > new decimal(0))
			{
				extendedAmountType _extendedAmountType1 = new extendedAmountType()
				{
					amount = order.ShippingAmount,
					description = order.ShippingName,
					name = order.ShippingName
				};
				trans.shipping = _extendedAmountType1;
			}
			if (order._lineItems.Count > 0)
			{
				trans.lineItems = order._lineItems.ToArray();
			}
			if (order.TaxExempt.HasValue)
			{
				trans.taxExempt = order.TaxExempt.Value;
				trans.taxExemptSpecified = true;
			}
			if (order.RecurringBilling.HasValue)
			{
				trans.recurringBilling = order.RecurringBilling.Value;
				trans.recurringBillingSpecified = true;
			}
			if (!string.IsNullOrEmpty(order.CardCode))
			{
				trans.cardCode = order.CardCode;
			}
			req.transaction = new profileTransactionType()
			{
				Item = trans
			};
			string str = ((createCustomerProfileTransactionResponse)this._gateway.Send(req)).directResponse;
			char[] chrArray = new char[] { ',' };
			return new GatewayResponse(str.Split(chrArray));
		}

		/// <summary>
		/// Authorizes and Captures a transaction using the supplied profile information.
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="paymentProfileID">The payment profile ID.</param>
		/// <param name="amount">The amount.</param>
		/// <returns></returns>
		public IGatewayResponse AuthorizeAndCapture(string profileID, string paymentProfileID, decimal amount)
		{
			return this.AuthorizeAndCapture(profileID, paymentProfileID, amount, new decimal(0), new decimal(0));
		}

		/// <summary>
		/// Authorizes and Captures a transaction using the supplied profile information with Tax and Shipping specified. If you want
		/// to add more detail here, use the 3rd option - which is to add an Order object
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="paymentProfileID">The payment profile ID.</param>
		/// <param name="amount">The amount.</param>
		/// <param name="tax">The tax.</param>
		/// <param name="shipping">The shipping.</param>
		/// <returns></returns>
		public IGatewayResponse AuthorizeAndCapture(string profileID, string paymentProfileID, decimal amount, decimal tax, decimal shipping)
		{
			Order order = new Order(profileID, paymentProfileID, "")
			{
				Amount = amount
			};
			if (tax > new decimal(0))
			{
				order.SalesTaxAmount = tax;
				order.SalesTaxName = "Sales Tax";
			}
			if (shipping > new decimal(0))
			{
				order.ShippingAmount = shipping;
				order.ShippingName = "Shipping";
			}
			return this.AuthorizeAndCapture(order);
		}

		/// <summary>
		/// Authorizes and Captures a transaction using the supplied profile information, abstracted through an Order object. Using the Order
		/// you can add line items, specify shipping and tax, etc.
		/// </summary>
		/// <param name="order">The order.</param>
		/// <returns></returns>
		public IGatewayResponse AuthorizeAndCapture(Order order)
		{
			createCustomerProfileTransactionRequest req = new createCustomerProfileTransactionRequest();
			profileTransAuthCaptureType trans = new profileTransAuthCaptureType()
			{
				customerProfileId = order.CustomerProfileID,
				customerPaymentProfileId = order.PaymentProfileID,
				amount = order.Total
			};
			if (!string.IsNullOrEmpty(order.ShippingAddressProfileID))
			{
				trans.customerShippingAddressId = order.ShippingAddressProfileID;
			}
			if (order.SalesTaxAmount > new decimal(0))
			{
				extendedAmountType _extendedAmountType = new extendedAmountType()
				{
					amount = order.SalesTaxAmount,
					description = order.SalesTaxName,
					name = order.SalesTaxName
				};
				trans.tax = _extendedAmountType;
			}
			if (order.ShippingAmount > new decimal(0))
			{
				extendedAmountType _extendedAmountType1 = new extendedAmountType()
				{
					amount = order.ShippingAmount,
					description = order.ShippingName,
					name = order.ShippingName
				};
				trans.shipping = _extendedAmountType1;
			}
			if (order._lineItems.Count > 0)
			{
				trans.lineItems = order._lineItems.ToArray();
			}
			if (order.TaxExempt.HasValue)
			{
				trans.taxExempt = order.TaxExempt.Value;
				trans.taxExemptSpecified = true;
			}
			if (order.RecurringBilling.HasValue)
			{
				trans.recurringBilling = order.RecurringBilling.Value;
				trans.recurringBillingSpecified = true;
			}
			if (!string.IsNullOrEmpty(order.CardCode))
			{
				trans.cardCode = order.CardCode;
			}
			req.transaction = new profileTransactionType()
			{
				Item = trans
			};
			string str = ((createCustomerProfileTransactionResponse)this._gateway.Send(req)).directResponse;
			char[] chrArray = new char[] { ',' };
			return new GatewayResponse(str.Split(chrArray));
		}

		/// <summary>
		/// Captures the specified transaction.
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="paymentProfileId">The payment profile id.</param>
		/// <param name="cardCode">The 3 digit card code.</param>
		/// <param name="amount">The amount.</param>
		/// <param name="approvalCode">The approval code.</param>
		/// <returns></returns>
		public IGatewayResponse Capture(string profileID, string paymentProfileId, string cardCode, decimal amount, string approvalCode)
		{
			createCustomerProfileTransactionRequest req = new createCustomerProfileTransactionRequest();
			profileTransCaptureOnlyType trans = new profileTransCaptureOnlyType()
			{
				approvalCode = approvalCode,
				customerProfileId = profileID,
				amount = amount,
				cardCode = cardCode,
				customerPaymentProfileId = paymentProfileId
			};
			req.transaction = new profileTransactionType()
			{
				Item = trans
			};
			string str = ((createCustomerProfileTransactionResponse)this._gateway.Send(req)).directResponse;
			char[] chrArray = new char[] { ',' };
			return new GatewayResponse(str.Split(chrArray));
		}

		public Customer CreateCustomer(string email, string description)
		{
			customerProfileType newCustomer = new customerProfileType()
			{
				description = description,
				email = email
			};
			createCustomerProfileRequest req = new createCustomerProfileRequest()
			{
				profile = newCustomer
			};
			createCustomerProfileResponse response = (createCustomerProfileResponse)this._gateway.Send(req);
			Customer customer = new Customer()
			{
				Email = email,
				Description = description,
				ProfileID = response.customerProfileId
			};
			return customer;
		}

		/// <summary>
		/// Deletes a customer from the AuthNET servers.
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <returns></returns>
		public bool DeleteCustomer(string profileID)
		{
			deleteCustomerProfileRequest req = new deleteCustomerProfileRequest()
			{
				customerProfileId = profileID
			};
			deleteCustomerProfileResponse _deleteCustomerProfileResponse = (deleteCustomerProfileResponse)this._gateway.Send(req);
			return true;
		}

		/// <summary>
		/// Deletes a payment profile for a customer from the AuthNET servers.
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="paymentProfileID">The payment profile ID.</param>
		/// <returns></returns>
		public bool DeletePaymentProfile(string profileID, string paymentProfileID)
		{
			deleteCustomerPaymentProfileRequest req = new deleteCustomerPaymentProfileRequest()
			{
				customerPaymentProfileId = paymentProfileID,
				customerProfileId = profileID
			};
			deleteCustomerPaymentProfileResponse _deleteCustomerPaymentProfileResponse = (deleteCustomerPaymentProfileResponse)this._gateway.Send(req);
			return true;
		}

		/// <summary>
		/// Deletes a shipping address for a given user from the AuthNET servers.
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="shippingAddressID">The shipping address ID.</param>
		/// <returns></returns>
		public bool DeleteShippingAddress(string profileID, string shippingAddressID)
		{
			deleteCustomerShippingAddressRequest req = new deleteCustomerShippingAddressRequest()
			{
				customerAddressId = shippingAddressID,
				customerProfileId = profileID
			};
			deleteCustomerShippingAddressResponse _deleteCustomerShippingAddressResponse = (deleteCustomerShippingAddressResponse)this._gateway.Send(req);
			return true;
		}

		/// <summary>
		/// Retrieve an existing customer profile along with all the associated customer payment profiles and customer shipping addresses. 
		/// </summary>
		/// <param name="profileID">The profile ID</param>
		/// <returns></returns>
		public Customer GetCustomer(string profileID)
		{
			getCustomerProfileResponse response = new getCustomerProfileResponse();
			getCustomerProfileRequest req = new getCustomerProfileRequest()
			{
				customerProfileId = profileID
			};
			response = (getCustomerProfileResponse)this._gateway.Send(req);
			Customer result = new Customer()
			{
				Email = response.profile.email,
				Description = response.profile.description,
				ProfileID = response.profile.customerProfileId,
				ID = response.profile.merchantCustomerId
			};
			if (response.profile.shipToList != null)
			{
				for (int i = 0; i < (int)response.profile.shipToList.Length; i++)
				{
					result.ShippingAddresses.Add(new Address(response.profile.shipToList[i]));
				}
			}
			if (response.profile.paymentProfiles != null)
			{
				for (int i = 0; i < (int)response.profile.paymentProfiles.Length; i++)
				{
					result.PaymentProfiles.Add(new PaymentProfile(response.profile.paymentProfiles[i]));
				}
			}
			return result;
		}

		/// <summary>
		/// Returns all Customer IDs stored at Authorize.NET
		/// </summary>
		/// <returns></returns>
		public string[] GetCustomerIDs()
		{
			getCustomerProfileIdsRequest req = new getCustomerProfileIdsRequest();
			return ((getCustomerProfileIdsResponse)this._gateway.Send(req)).ids;
		}

		/// <summary>
		/// Gets a shipping address for a customer.
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="shippingAddressID">The shipping address ID.</param>
		/// <returns></returns>
		public Address GetShippingAddress(string profileID, string shippingAddressID)
		{
			getCustomerShippingAddressRequest req = new getCustomerShippingAddressRequest()
			{
				customerAddressId = shippingAddressID,
				customerProfileId = profileID
			};
			getCustomerShippingAddressResponse response = (getCustomerShippingAddressResponse)this._gateway.Send(req);
			return new Address(response.address);
		}

		/// <summary>
		/// Refunds a transaction for the specified amount
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="paymentProfileId">The payment profile id.</param>
		/// <param name="transactionId">The transaction id.</param>
		/// <param name="approvalCode">The approval code.</param>
		/// <param name="amount">The amount.</param>
		/// <returns></returns>
		public IGatewayResponse Refund(string profileID, string paymentProfileId, string transactionId, string approvalCode, decimal amount)
		{
			createCustomerProfileTransactionRequest req = new createCustomerProfileTransactionRequest();
			profileTransRefundType trans = new profileTransRefundType()
			{
				amount = amount,
				customerProfileId = profileID,
				customerPaymentProfileId = paymentProfileId,
				transId = transactionId
			};
			req.transaction = new profileTransactionType()
			{
				Item = trans
			};
			string str = ((createCustomerProfileTransactionResponse)this._gateway.Send(req)).directResponse;
			char[] chrArray = new char[] { ',' };
			return new GatewayResponse(str.Split(chrArray));
		}

		/// <summary>
		/// Updates a customer's record.
		/// </summary>
		/// <param name="customer">The customer.</param>
		/// <returns></returns>
		public bool UpdateCustomer(Customer customer)
		{
			updateCustomerProfileRequest req = new updateCustomerProfileRequest();
			req.profile.customerProfileId = customer.ProfileID;
			req.profile.description = customer.Description;
			req.profile.email = customer.Email;
			req.profile.merchantCustomerId = customer.ID;
			updateCustomerProfileResponse _updateCustomerProfileResponse = (updateCustomerProfileResponse)this._gateway.Send(req);
			return true;
		}

		/// <summary>
		/// Updates a payment profile for a user.
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="profile">The profile.</param>
		/// <returns></returns>
		public bool UpdatePaymentProfile(string profileID, PaymentProfile profile)
		{
			updateCustomerPaymentProfileRequest req = new updateCustomerPaymentProfileRequest()
			{
				customerProfileId = profileID,
				paymentProfile = profile.ToAPI()
			};
			updateCustomerPaymentProfileResponse _updateCustomerPaymentProfileResponse = (updateCustomerPaymentProfileResponse)this._gateway.Send(req);
			return true;
		}

		/// <summary>
		/// Updates a shipping address for a user.
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public bool UpdateShippingAddress(string profileID, Address address)
		{
			updateCustomerShippingAddressRequest req = new updateCustomerShippingAddressRequest()
			{
				customerProfileId = profileID,
				address = address.ToAPIExType()
			};
			updateCustomerShippingAddressResponse _updateCustomerShippingAddressResponse = (updateCustomerShippingAddressResponse)this._gateway.Send(req);
			return true;
		}

		public string ValidateProfile(string profileID, string paymentProfileID, ValidationMode mode)
		{
			return this.ValidateProfile(profileID, paymentProfileID, null, mode);
		}

		/// <summary>
		/// This function validates the information on a profile - making sure what you have stored at AuthNET is valid. You can
		/// do this in two ways: in TestMode it will just run a validation to be sure all required fields are present and valid. If 
		/// you specify "live" - a live authorization request will be performed.
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="paymentProfileID">The payment profile ID.</param>
		/// <param name="shippingAddressID">The shipping address ID.</param>
		/// <param name="mode">The mode.</param>
		/// <returns></returns>
		public string ValidateProfile(string profileID, string paymentProfileID, string shippingAddressID, ValidationMode mode)
		{
			validateCustomerPaymentProfileRequest req = new validateCustomerPaymentProfileRequest()
			{
				customerProfileId = profileID,
				customerPaymentProfileId = paymentProfileID
			};
			if (!string.IsNullOrEmpty(shippingAddressID))
			{
				req.customerShippingAddressId = shippingAddressID;
			}
			req.validationMode = Customer.ToValidationMode(mode);
			return ((validateCustomerPaymentProfileResponse)this._gateway.Send(req)).directResponse;
		}

		/// <summary>
		/// Voids a previously authorized transaction
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="paymentProfileId">The payment profile id.</param>
		/// <param name="transactionId">The transaction id.</param>
		/// <param name="approvalCode">The approval code.</param>
		/// <returns></returns>
		public IGatewayResponse Void(string profileID, string paymentProfileId, string transactionId, string approvalCode)
		{
			createCustomerProfileTransactionRequest req = new createCustomerProfileTransactionRequest();
			profileTransVoidType trans = new profileTransVoidType()
			{
				customerProfileId = profileID,
				customerPaymentProfileId = paymentProfileId,
				transId = transactionId
			};
			req.transaction = new profileTransactionType()
			{
				Item = trans
			};
			string str = ((createCustomerProfileTransactionResponse)this._gateway.Send(req)).directResponse;
			char[] chrArray = new char[] { ',' };
			return new GatewayResponse(str.Split(chrArray));
		}
	}
}