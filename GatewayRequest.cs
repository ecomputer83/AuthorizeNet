using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

namespace AuthorizeNet
{
	/// <summary>
	/// An abstract base class, from which all Request classes must inherit
	/// </summary>
	public abstract class GatewayRequest : IGatewayRequest
	{
		private StringBuilder _post;

		private RequestAction _apiAction;

		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>The address.</value>
		public string Address
		{
			get
			{
				return this.Get("x_address");
			}
			set
			{
				this.Queue("x_address", value);
			}
		}

		/// <summary>
		/// Gets or sets the allow partial auth.
		/// </summary>
		/// <value>The allow partial auth.</value>
		public string AllowPartialAuth
		{
			get
			{
				return this.Get("x_allow_partial_Auth");
			}
			set
			{
				this.Queue("x_allow_partial_Auth", value);
			}
		}

		/// <summary>
		/// Gets or sets the amount.
		/// </summary>
		/// <value>The amount.</value>
		public string Amount
		{
			get
			{
				return this.Get("x_amount");
			}
			set
			{
				this.Queue("x_amount", value);
			}
		}

		public RequestAction ApiAction
		{
			get
			{
				return this._apiAction;
			}
		}

		/// <summary>
		/// Gets or sets the auth code.
		/// </summary>
		/// <value>The auth code.</value>
		public string AuthCode
		{
			get
			{
				return this.Get("x_auth_code");
			}
			set
			{
				this.Queue("x_auth_code", value);
			}
		}

		/// <summary>
		/// Gets or sets the authentication indicator.
		/// </summary>
		/// <value>The authentication indicator.</value>
		public string AuthenticationIndicator
		{
			get
			{
				return this.Get("x_authentication_indicator");
			}
			set
			{
				this.Queue("x_authentication_indicator", value);
			}
		}

		/// <summary>
		/// Gets or sets the bank ABA code.
		/// </summary>
		/// <value>The bank ABA code.</value>
		public string BankABACode
		{
			get
			{
				return this.Get("x_bank_aba_code");
			}
			set
			{
				this.Queue("x_bank_aba_code", value);
			}
		}

		/// <summary>
		/// Gets or sets the name of the bank account.
		/// </summary>
		/// <value>The name of the bank account.</value>
		public string BankAccountName
		{
			get
			{
				return this.Get("x_bank_acct_name");
			}
			set
			{
				this.Queue("x_bank_acct_name", value);
			}
		}

		/// <summary>
		/// Gets or sets the bank account number.
		/// </summary>
		/// <value>The bank account number.</value>
		public string BankAccountNumber
		{
			get
			{
				return this.Get("x_bank_acct_num");
			}
			set
			{
				this.Queue("x_bank_acct_num", value);
			}
		}

		/// <summary>
		/// Gets or sets the type of the bank account.
		/// </summary>
		/// <value>The type of the bank account.</value>
		public AuthorizeNet.BankAccountType BankAccountType
		{
			get
			{
				return (AuthorizeNet.BankAccountType)Enum.Parse(typeof(AuthorizeNet.BankAccountType), this.Get("x_bank_acct_type"), true);
			}
			set
			{
				this.Queue("x_bank_acct_type", value.ToString());
			}
		}

		/// <summary>
		/// Gets or sets the bank check number.
		/// </summary>
		/// <value>The bank check number.</value>
		public string BankCheckNumber
		{
			get
			{
				return this.Get("x_bank_check_number");
			}
			set
			{
				this.Queue("x_bank_check_number", value);
			}
		}

		/// <summary>
		/// Gets or sets the name of the bank.
		/// </summary>
		/// <value>The name of the bank.</value>
		public string BankName
		{
			get
			{
				return this.Get("x_bank_name");
			}
			set
			{
				this.Queue("x_bank_name", value);
			}
		}

		/// <summary>
		/// Gets or sets the credit card code.
		/// </summary>
		/// <value>The card code.</value>
		public string CardCode
		{
			get
			{
				return this.Get("x_card_code");
			}
			set
			{
				this.Queue("x_card_code", value);
			}
		}

		/// <summary>
		/// Gets or sets the cardholder authentication value.
		/// </summary>
		/// <value>The cardholder authentication value.</value>
		public string CardholderAuthenticationValue
		{
			get
			{
				return this.Get("x_cardholder_authentication_value");
			}
			set
			{
				this.Queue("x_cardholder_authentication_value", value);
			}
		}

		/// <summary>
		/// Gets or sets the credit card number.
		/// </summary>
		/// <value>The card num.</value>
		public string CardNum
		{
			get
			{
				return this.Get("x_card_num");
			}
			set
			{
				this.Queue("x_card_num", value);
			}
		}

		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		/// <value>The city.</value>
		public string City
		{
			get
			{
				return this.Get("x_city");
			}
			set
			{
				this.Queue("x_city", value);
			}
		}

		/// <summary>
		/// Gets or sets the company.
		/// </summary>
		/// <value>The company.</value>
		public string Company
		{
			get
			{
				return this.Get("x_company");
			}
			set
			{
				this.Queue("x_company", value);
			}
		}

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		/// <value>The country.</value>
		public string Country
		{
			get
			{
				return this.Get("x_country");
			}
			set
			{
				this.Queue("x_country", value);
			}
		}

		/// <summary>
		/// Gets or sets the cust id.
		/// </summary>
		/// <value>The cust id.</value>
		public string CustId
		{
			get
			{
				return this.Get("x_cust_id");
			}
			set
			{
				this.Queue("x_cust_id", value);
			}
		}

		/// <summary>
		/// Gets or sets the customer ip.
		/// </summary>
		/// <value>The customer ip.</value>
		public string CustomerIp
		{
			get
			{
				return this.Get("x_cust_ip");
			}
			set
			{
				this.Queue("x_cust_ip", value);
			}
		}

		/// <summary>
		/// Gets or sets the delim char.
		/// </summary>
		/// <value>The delim char.</value>
		public string DelimChar
		{
			get
			{
				return this.Get("x_delim_char");
			}
			set
			{
				this.Queue("x_delim_char", value);
			}
		}

		/// <summary>
		/// Gets or sets the delim data.
		/// </summary>
		/// <value>The delim data.</value>
		public string DelimData
		{
			get
			{
				return this.Get("x_delim_data");
			}
			set
			{
				this.Queue("x_delim_data", value);
			}
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description
		{
			get
			{
				return this.Get("x_description");
			}
			set
			{
				this.Queue("x_description", value);
			}
		}

		/// <summary>
		/// Gets or sets the duplicate window - the am.
		/// </summary>
		/// <value>The duplicate window.</value>
		public string DuplicateWindow
		{
			get
			{
				return this.Get("x_duplicate_window");
			}
			set
			{
				this.Queue("x_duplicate_window", value);
			}
		}

		/// <summary>
		/// Gets or sets the duty.
		/// </summary>
		/// <value>The duty.</value>
		public string Duty
		{
			get
			{
				return this.Get("x_duty");
			}
			set
			{
				this.Queue("x_duty", value);
			}
		}

		/// <summary>
		/// Gets or sets the type of the echeck.
		/// </summary>
		/// <value>The type of the echeck.</value>
		public AuthorizeNet.EcheckType EcheckType
		{
			get
			{
				return (AuthorizeNet.EcheckType)Enum.Parse(typeof(AuthorizeNet.EcheckType), this.Get("x_echeck_type"), true);
			}
			set
			{
				this.Queue("x_echeck_type", value.ToString());
			}
		}

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>The email.</value>
		public string Email
		{
			get
			{
				return this.Get("x_email");
			}
			set
			{
				this.Queue("x_email", value);
			}
		}

		/// <summary>
		/// Gets or sets the email customer.
		/// </summary>
		/// <value>The email customer.</value>
		public string EmailCustomer
		{
			get
			{
				return this.Get("x_email");
			}
			set
			{
				this.Queue("x_email", value);
			}
		}

		/// <summary>
		/// Gets or sets the encap char.
		/// </summary>
		/// <value>The encap char.</value>
		public string EncapChar
		{
			get
			{
				return this.Get("x_encap_char");
			}
			set
			{
				this.Queue("x_encap_char", value);
			}
		}

		/// <summary>
		/// Gets or sets the exp date.
		/// </summary>
		/// <value>The exp date.</value>
		public string ExpDate
		{
			get
			{
				return this.Get("x_exp_date");
			}
			set
			{
				this.Queue("x_exp_date", value);
			}
		}

		/// <summary>
		/// Gets or sets the fax.
		/// </summary>
		/// <value>The fax.</value>
		public string Fax
		{
			get
			{
				return this.Get("x_fax");
			}
			set
			{
				this.Queue("x_fax", value);
			}
		}

		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>The first name.</value>
		public string FirstName
		{
			get
			{
				return this.Get("x_first_name");
			}
			set
			{
				this.Queue("x_first_name", value);
			}
		}

		/// <summary>
		/// Gets or sets the footer email receipt.
		/// </summary>
		/// <value>The footer email receipt.</value>
		public string FooterEmailReceipt
		{
			get
			{
				return this.Get("x_footer_email_receipt");
			}
			set
			{
				this.Queue("x_footer_email_receipt", value);
			}
		}

		/// <summary>
		/// Gets or sets the freight.
		/// </summary>
		/// <value>The freight.</value>
		public string Freight
		{
			get
			{
				return this.Get("x_frieght");
			}
			set
			{
				this.Queue("x_frieght", value);
			}
		}

		/// <summary>
		/// Gets or sets the header email receipt.
		/// </summary>
		/// <value>The header email receipt.</value>
		public string HeaderEmailReceipt
		{
			get
			{
				return this.Get("x_header_email_receipt");
			}
			set
			{
				this.Queue("x_header_email_receipt", value);
			}
		}

		/// <summary>
		/// Gets or sets the invoice num.
		/// </summary>
		/// <value>The invoice num.</value>
		public string InvoiceNum
		{
			get
			{
				return this.Get("x_invoice_num");
			}
			set
			{
				this.Queue("x_invoice_num", value);
			}
		}

		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>The last name.</value>
		public string LastName
		{
			get
			{
				return this.Get("x_last_name");
			}
			set
			{
				this.Queue("x_last_name", value);
			}
		}

		/// <summary>
		/// Gets or sets the line item.
		/// </summary>
		/// <value>The line item.</value>
		public string LineItem
		{
			get
			{
				return this.Get("x_line_item");
			}
			set
			{
				this.Queue("x_line_item", value);
			}
		}

		/// <summary>
		/// Gets or sets the login.
		/// </summary>
		/// <value>The login.</value>
		public string Login
		{
			get
			{
				return this.Get("x_login");
			}
			set
			{
				this.Queue("x_login", value);
			}
		}

		/// <summary>
		/// Gets or sets the method.
		/// </summary>
		/// <value>The method.</value>
		public string Method
		{
			get
			{
				return this.Get("x_method");
			}
			set
			{
				this.Queue("x_method", value);
			}
		}

		/// <summary>
		/// Gets or sets the phone.
		/// </summary>
		/// <value>The phone.</value>
		public string Phone
		{
			get
			{
				return this.Get("x_phone");
			}
			set
			{
				this.Queue("x_phone", value);
			}
		}

		/// <summary>
		/// Gets or sets the po number.
		/// </summary>
		/// <value>The po num.</value>
		public string PoNum
		{
			get
			{
				return this.Get("x_po_num");
			}
			set
			{
				this.Queue("x_po_num", value);
			}
		}

		public Dictionary<string, string> Post
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the recurring billing.
		/// </summary>
		/// <value>The recurring billing.</value>
		public string RecurringBilling
		{
			get
			{
				return this.Get("x_recurring_billing");
			}
			set
			{
				this.Queue("x_recurring_billing", value);
			}
		}

		/// <summary>
		/// Gets or sets the relay response.
		/// </summary>
		/// <value>The relay response.</value>
		public string RelayResponse
		{
			get
			{
				return this.Get("x_relay_response");
			}
			set
			{
				this.Queue("x_relay_response", value);
			}
		}

		/// <summary>
		/// Gets or sets the ship to address.
		/// </summary>
		/// <value>The ship to address.</value>
		public string ShipToAddress
		{
			get
			{
				return this.Get("x_ship_to_address");
			}
			set
			{
				this.Queue("x_ship_to_address", value);
			}
		}

		/// <summary>
		/// Gets or sets the ship to city.
		/// </summary>
		/// <value>The ship to city.</value>
		public string ShipToCity
		{
			get
			{
				return this.Get("x_ship_to_city");
			}
			set
			{
				this.Queue("x_ship_to_city", value);
			}
		}

		/// <summary>
		/// Gets or sets the ship to company.
		/// </summary>
		/// <value>The ship to company.</value>
		public string ShipToCompany
		{
			get
			{
				return this.Get("x_ship_to_company");
			}
			set
			{
				this.Queue("x_ship_to_company", value);
			}
		}

		/// <summary>
		/// Gets or sets the ship to country.
		/// </summary>
		/// <value>The ship to country.</value>
		public string ShipToCountry
		{
			get
			{
				return this.Get("x_ship_to_country");
			}
			set
			{
				this.Queue("x_ship_to_country", value);
			}
		}

		/// <summary>
		/// Gets or sets the first name of the ship to.
		/// </summary>
		/// <value>The first name of the ship to.</value>
		public string ShipToFirstName
		{
			get
			{
				return this.Get("x_ship_to_first_name");
			}
			set
			{
				this.Queue("x_ship_to_first_name", value);
			}
		}

		/// <summary>
		/// Gets or sets the last name of the ship to.
		/// </summary>
		/// <value>The last name of the ship to.</value>
		public string ShipToLastName
		{
			get
			{
				return this.Get("x_ship_to_last_name");
			}
			set
			{
				this.Queue("x_ship_to_last_name", value);
			}
		}

		/// <summary>
		/// Gets or sets the state of the ship to.
		/// </summary>
		/// <value>The state of the ship to.</value>
		public string ShipToState
		{
			get
			{
				return this.Get("x_ship_to_state");
			}
			set
			{
				this.Queue("x_ship_to_state", value);
			}
		}

		/// <summary>
		/// Gets or sets the ship to zip.
		/// </summary>
		/// <value>The ship to zip.</value>
		public string ShipToZip
		{
			get
			{
				return this.Get("x_ship_to_zip");
			}
			set
			{
				this.Queue("x_ship_to_zip", value);
			}
		}

		/// <summary>
		/// Gets or sets the split tender id.
		/// </summary>
		/// <value>The split tender id.</value>
		public string SplitTenderId
		{
			get
			{
				return this.Get("x_split_tender_id");
			}
			set
			{
				this.Queue("x_split_tender_id", value);
			}
		}

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>The state.</value>
		public string State
		{
			get
			{
				return this.Get("x_state");
			}
			set
			{
				this.Queue("x_state", value);
			}
		}

		/// <summary>
		/// Gets or sets the tax.
		/// </summary>
		/// <value>The tax.</value>
		public string Tax
		{
			get
			{
				return this.Get("x_tax");
			}
			set
			{
				this.Queue("x_tax", value);
			}
		}

		/// <summary>
		/// Gets or sets the tax exempt.
		/// </summary>
		/// <value>The tax exempt.</value>
		public string TaxExempt
		{
			get
			{
				return this.Get("x_tax_exempt");
			}
			set
			{
				this.Queue("x_tax_exempt", value);
			}
		}

		/// <summary>
		/// Gets or sets the test request.
		/// </summary>
		/// <value>The test request.</value>
		public string TestRequest
		{
			get
			{
				return this.Get("x_test_request");
			}
			set
			{
				this.Queue("x_test_request", value);
			}
		}

		/// <summary>
		/// Gets or sets the tran key.
		/// </summary>
		/// <value>The tran key.</value>
		public string TranKey
		{
			get
			{
				return this.Get("x_tran_key");
			}
			set
			{
				this.Queue("x_tran_key", value);
			}
		}

		/// <summary>
		/// Gets or sets the trans id.
		/// </summary>
		/// <value>The trans id.</value>
		public string TransId
		{
			get
			{
				return this.Get("x_trans_id");
			}
			set
			{
				this.Queue("x_trans_id", value);
			}
		}

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		public string Type
		{
			get
			{
				return this.Get("x_type");
			}
			set
			{
				this.Queue("x_type", value);
			}
		}

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
		public string Version
		{
			get
			{
				return this.Get("x_version");
			}
			set
			{
				this.Queue("x_version", value);
			}
		}

		/// <summary>
		/// Gets or sets the zip.
		/// </summary>
		/// <value>The zip.</value>
		public string Zip
		{
			get
			{
				return this.Get("x_zip");
			}
			set
			{
				this.Queue("x_zip", value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.GatewayRequest" /> class.
		/// </summary>
		public GatewayRequest()
		{
			this.Post = new Dictionary<string, string>();
			this._post = new StringBuilder();
			this.LoadCoreValues();
			this._apiAction = RequestAction.AuthorizeAndCapture;
		}

		/// <summary>
		/// The 3-digit Credit Card Code (CCV) on the back of the card
		/// </summary>
		public IGatewayRequest AddCardCode(string cardCode)
		{
			this.Queue("x_card_code", cardCode);
			return this;
		}

		/// <summary>
		/// Adds a Customer record to the current request
		/// </summary>
		public IGatewayRequest AddCustomer(string ID, string first, string last, string address, string state, string zip)
		{
			this.Queue("x_first_name", first);
			this.Queue("x_last_name", last);
			this.Queue("x_address", address);
			this.Queue("x_state", state);
			this.Queue("x_zip", zip);
			this.Queue("x_cust_id", ID);
			return this;
		}

		/// <summary>
		/// Adds a detailed tax value to the request
		/// </summary>
		public IGatewayRequest AddDuty(decimal amount, string name, string description)
		{
			this.Duty = string.Format("{0}<|>{1}<|>{2}", name, description, amount.ToString());
			return this;
		}

		/// <summary>
		/// Adds a tax value to the request
		/// </summary>
		public IGatewayRequest AddDuty(decimal amount)
		{
			this.Duty = amount.ToString();
			return this;
		}

		/// <summary>
		/// This method adds the required values for Fraud Detection Suite. Your merchant must sign up for this service with Authorize.Net
		/// </summary>
		/// <param name="customerIP">The Customer's IP address</param>
		/// <returns></returns>
		public IGatewayRequest AddFraudCheck(string customerIP)
		{
			this.CustomerIp = customerIP;
			return this;
		}

		/// <summary>
		/// This method will pull the user's IP address for use with FDS. Only valid for Web-based transactions.
		/// </summary>
		/// <returns></returns>
		public IGatewayRequest AddFraudCheck()
		{
			if (HttpContext.Current != null)
			{
				this.CustomerIp = HttpContext.Current.Request.UserHostAddress;
			}
			return this;
		}

		/// <summary>
		/// Adds a detailed tax value to the request
		/// </summary>
		public IGatewayRequest AddFreight(decimal amount, string name, string description)
		{
			this.Freight = string.Format("{0}<|>{1}<|>{2}", name, description, amount.ToString());
			return this;
		}

		/// <summary>
		/// Adds a tax value to the request
		/// </summary>
		public IGatewayRequest AddFreight(decimal amount)
		{
			this.Freight = amount.ToString();
			return this;
		}

		/// <summary>
		/// Adds an InvoiceNumber to the request
		/// </summary>
		public IGatewayRequest AddInvoice(string invoiceNumber)
		{
			this.Queue("x_invoice_num", invoiceNumber);
			return this;
		}

		/// <summary>
		/// Adds a line item to the current order
		/// </summary>
		/// <returns></returns>
		public IGatewayRequest AddLineItem(string itemID, string name, string description, int quantity, decimal price, bool taxable)
		{
			object[] objArray = new object[] { itemID, name, description, quantity.ToString(), price.ToString(), taxable.ToString() };
			this.Queue("x_line_item", string.Format("{0}<|>{1}<|>{2}<|>{3}<|>{4}<|>{5}", objArray));
			return this;
		}

		/// <summary>
		/// This is where you can add custom values to the request, which will be returned to you
		/// in the response
		/// </summary>
		public IGatewayRequest AddMerchantValue(string key, string value)
		{
			this.Queue(key, value);
			return this;
		}

		/// <summary>
		/// Adds a Shipping Record to the current request
		/// </summary>
		public IGatewayRequest AddShipping(string ID, string first, string last, string address, string state, string zip)
		{
			this.Queue("x_ship_to_first_name", first);
			this.Queue("x_ship_to_last_name", last);
			this.Queue("x_ship_to_address", address);
			this.Queue("x_ship_to_state", state);
			this.Queue("x_ship_to_zip", zip);
			return this;
		}

		/// <summary>
		/// Adds a detailed tax value to the request
		/// </summary>
		public IGatewayRequest AddTax(decimal amount, string name, string description)
		{
			this.Tax = string.Format("{0}<|>{1}<|>{2}", name, description, amount.ToString());
			return this;
		}

		/// <summary>
		/// Adds a tax value to the request
		/// </summary>
		public IGatewayRequest AddTax(decimal amount)
		{
			this.Tax = amount.ToString();
			return this;
		}

		/// <summary>
		/// Gets the specified key from the request.
		/// </summary>
		/// <param name="key">The key.</param>
		public string Get(string key)
		{
			if (!this.Post.ContainsKey(key))
			{
				return null;
			}
			return this.Post[key];
		}

		/// <summary>
		/// Loads the core values tp the API request, including auth and basic settings.
		/// </summary>
		private void LoadCoreValues()
		{
			this.Queue("x_delim_data", "TRUE");
			this.Queue("x_delim_char", "|");
			this.Queue("x_relay_response", "FALSE");
			this.Queue("x_method", "CC");
			this.Queue("x_version", "3.1");
		}

		/// <summary>
		/// Queues the specified key into the request.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public void Queue(string key, string value)
		{
			if (this.Post.ContainsKey(key))
			{
				this.Post.Remove(key);
			}
			this.Post.Add(key, value);
			this._post.AppendFormat("{0}={1}&", key, HttpUtility.UrlEncode(value));
		}

		internal void SetApiAction(RequestAction action)
		{
			this._apiAction = action;
			string apiValue = "AUTH_CAPTURE";
			switch (action)
			{
				case RequestAction.Authorize:
				{
					apiValue = "AUTH_ONLY";
					this.Queue("x_type", apiValue);
					return;
				}
				case RequestAction.Capture:
				{
					apiValue = "PRIOR_AUTH_CAPTURE";
					this.Queue("x_type", apiValue);
					return;
				}
				case RequestAction.AuthorizeAndCapture:
				{
					this.Queue("x_type", apiValue);
					return;
				}
				case RequestAction.Credit:
				case RequestAction.UnlinkedCredit:
				{
					apiValue = "CREDIT";
					this.Queue("x_type", apiValue);
					return;
				}
				case RequestAction.Void:
				{
					apiValue = "VOID";
					this.Queue("x_type", apiValue);
					return;
				}
				case RequestAction.PriorAuthCapture:
				{
					apiValue = "PRIOR_AUTH_CAPTURE";
					this.Queue("x_type", apiValue);
					return;
				}
				default:
				{
					this.Queue("x_type", apiValue);
					return;
				}
			}
		}

		/// <summary>
		/// Outputs the queue as a delimited, URL-safe string for sending to Authorize.net as a form POST
		/// </summary>
		public string ToPostString()
		{
			return this._post.ToString().TrimEnd(new char[] { '&' });
		}
	}
}