using AuthorizeNet.APICore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AuthorizeNet
{
	/// <summary>
	/// A representational class for a Transaction
	/// </summary>
	public class Transaction
	{
		private List<LineItem> _lineItems;

		/// <summary>
		/// Gets or sets the authorization amount.
		/// </summary>
		/// <value>The authorization amount.</value>
		public decimal AuthorizationAmount
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the authorization code.
		/// </summary>
		/// <value>The authorization code.</value>
		public string AuthorizationCode
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the AVS code.
		/// </summary>
		/// <value>The AVS code.</value>
		public string AVSCode
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the AVS response.
		/// </summary>
		/// <value>The AVS response.</value>
		public string AVSResponse
		{
			get
			{
				string aVSCode = this.AVSCode;
				string str = aVSCode;
				if (aVSCode != null)
				{
					switch (str)
					{
						case "A":
						{
							return "Address (Street): matches, ZIP does not";
						}
						case "B":
						{
							return "Address information not provided for AVS check";
						}
						case "E":
						{
							return "AVS error ";
						}
						case "G":
						{
							return "Non-U.S. Card Issuing Bank ";
						}
						case "N":
						{
							return "No Match on Address (Street) or ZIP ";
						}
						case "P":
						{
							return "AVS not applicable for this transaction ";
						}
						case "R":
						{
							return "Retry — System unavailable or timed out ";
						}
						case "S":
						{
							return "Service not supported by issuer ";
						}
						case "U":
						{
							return "Address information is unavailable ";
						}
						case "W":
						{
							return "Nine digit ZIP matches, Address (Street): does not ";
						}
						case "X":
						{
							return "Address (Street) and nine digit ZIP match ";
						}
						case "Y":
						{
							return "Address (Street) and five digit ZIP match ";
						}
						case "Z":
						{
							return "Five digit ZIP matches, Address (Street) does not";
						}
					}
				}
				return "";
			}
		}

		/// <summary>
		/// Gets or sets the batch settled on.
		/// </summary>
		/// <value>The batch settled on.</value>
		public DateTime BatchSettledOn
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the batch settlement ID.
		/// </summary>
		/// <value>The batch settlement ID.</value>
		public string BatchSettlementID
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the state of the batch settlement.
		/// </summary>
		/// <value>The state of the batch settlement.</value>
		public string BatchSettlementState
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the billing address.
		/// </summary>
		/// <value>The billing address.</value>
		public Address BillingAddress
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the card expiration.
		/// </summary>
		/// <value>The card expiration.</value>
		public string CardExpiration
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the card number.
		/// </summary>
		/// <value>The card number.</value>
		public string CardNumber
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the card response.
		/// </summary>
		/// <value>The card response.</value>
		public string CardResponse
		{
			get
			{
				string cardResponseCode = this.CardResponseCode;
				string str = cardResponseCode;
				if (cardResponseCode != null)
				{
					if (str == "M")
					{
						return "Match";
					}
					if (str == "N")
					{
						return "No Match";
					}
					if (str == "P")
					{
						return "Not Processed";
					}
					if (str == "S")
					{
						return "Should have been present";
					}
					if (str == "U")
					{
						return "Issuer unable to process request";
					}
				}
				return "";
			}
		}

		/// <summary>
		/// Gets or sets the card response code.
		/// </summary>
		/// <value>The card response code.</value>
		public string CardResponseCode
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the type of the card.
		/// </summary>
		/// <value>The type of the card.</value>
		public string CardType
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the CAVV code.
		/// </summary>
		/// <value>The CAVV code.</value>
		public string CAVVCode
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the CAVV response.
		/// </summary>
		/// <value>The CAVV response.</value>
		public string CAVVResponse
		{
			get
			{
				string cAVVCode = this.CAVVCode;
				string str = cAVVCode;
				if (cAVVCode != null)
				{
					switch (str)
					{
						case "0":
						{
							return "CAVV not validated because erroneous data was submitted";
						}
						case "1":
						{
							return "CAVV failed validation";
						}
						case "2":
						{
							return "CAVV passed validation";
						}
						case "3":
						{
							return "CAVV validation could not be performed; issuer attempt incomplete";
						}
						case "4":
						{
							return "CAVV validation could not be performed; issuer system error";
						}
						case "5":
						{
							return "Reserved for future use";
						}
						case "6":
						{
							return "Reserved for future use";
						}
						case "7":
						{
							return "CAVV attempt — failed validation — issuer available (U.S.-issued card/non-U.S acquirer)";
						}
						case "8":
						{
							return "CAVV attempt — passed validation — issuer available (U.S.-issued card/non-U.S. acquirer)";
						}
						case "9":
						{
							return "CAVV attempt — failed validation — issuer unavailable (U.S.-issued card/non-U.S. acquirer)";
						}
						case "A":
						{
							return "CAVV attempt — passed validation — issuer unavailable (U.S.-issued card/non-U.S. acquirer)";
						}
						case "B":
						{
							return "CAVV passed validation, information only, no liability shift";
						}
					}
				}
				return "Blank or not present - CAVV not validated";
			}
		}

		/// <summary>
		/// Gets or sets the customer email.
		/// </summary>
		/// <value>The customer email.</value>
		public string CustomerEmail
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the customer ID.
		/// </summary>
		/// <value>The customer ID.</value>
		public string CustomerID
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the date submitted.
		/// </summary>
		/// <value>The date submitted.</value>
		public DateTime DateSubmitted
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the duty.
		/// </summary>
		/// <value>The duty.</value>
		public decimal Duty
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the duty description.
		/// </summary>
		/// <value>The duty description.</value>
		public string DutyDescription
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>The first name.</value>
		public string FirstName
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the fraud filters.
		/// </summary>
		/// <value>The fraud filters.</value>
		public IList<string> FraudFilters
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the invoice number.
		/// </summary>
		/// <value>The invoice number.</value>
		public string InvoiceNumber
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is recurring.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is recurring; otherwise, <c>false</c>.
		/// </value>
		public bool IsRecurring
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>The last name.</value>
		public string LastName
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the line items.
		/// </summary>
		/// <value>The line items.</value>
		public IList<LineItem> LineItems
		{
			get
			{
				return this._lineItems;
			}
		}

		/// <summary>
		/// Gets or sets the order description.
		/// </summary>
		/// <value>The order description.</value>
		public string OrderDescription
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the PO number.
		/// </summary>
		/// <value>The PO number.</value>
		public string PONumber
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the requested amount.
		/// </summary>
		/// <value>The requested amount.</value>
		public decimal RequestedAmount
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the response code.
		/// </summary>
		/// <value>The response code.</value>
		public int ResponseCode
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the response reason.
		/// </summary>
		/// <value>The response reason.</value>
		public string ResponseReason
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the settle amount.
		/// </summary>
		/// <value>The settle amount.</value>
		public decimal SettleAmount
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the shipping.
		/// </summary>
		/// <value>The shipping.</value>
		public decimal Shipping
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the shipping address.
		/// </summary>
		/// <value>The shipping address.</value>
		public Address ShippingAddress
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the shipping description.
		/// </summary>
		/// <value>The shipping description.</value>
		public string ShippingDescription
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		public string Status
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the tax.
		/// </summary>
		/// <value>The tax.</value>
		public decimal Tax
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the tax description.
		/// </summary>
		/// <value>The tax description.</value>
		public string TaxDescription
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether [tax exempt].
		/// </summary>
		/// <value><c>true</c> if [tax exempt]; otherwise, <c>false</c>.</value>
		public bool TaxExempt
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the transaction ID.
		/// </summary>
		/// <value>The transaction ID.</value>
		public string TransactionID
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the type of the transaction.
		/// </summary>
		/// <value>The type of the transaction.</value>
		public string TransactionType
		{
			get;
			set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.Transaction" /> class.
		/// </summary>
		public Transaction()
		{
			this.FraudFilters = new List<string>();
			this._lineItems = new List<LineItem>();
		}

		/// <summary>
		/// Creates a Transaction directly from the API Response
		/// </summary>
		/// <param name="trans">The trans.</param>
		/// <returns></returns>
		public static Transaction NewFromResponse(transactionSummaryType trans)
		{
			Transaction result = new Transaction()
			{
				TransactionID = trans.transId,
				DateSubmitted = trans.submitTimeUTC,
				Status = trans.transactionStatus,
				LastName = trans.lastName,
				InvoiceNumber = trans.invoiceNumber,
				FirstName = trans.firstName,
				SettleAmount = trans.settleAmount,
				CardNumber = trans.accountNumber
			};
			return result;
		}

		/// <summary>
		/// Creates a Transaction directly from the API Response
		/// </summary>
		/// <param name="trans">The trans.</param>
		/// <returns></returns>
		public static Transaction NewFromResponse(transactionDetailsType trans)
		{
			Transaction result = new Transaction()
			{
				TransactionID = trans.transId,
				DateSubmitted = trans.submitTimeUTC,
				TransactionType = trans.transactionType,
				Status = trans.transactionStatus,
				ResponseCode = trans.responseCode,
				ResponseReason = trans.responseReasonDescription,
				AuthorizationCode = trans.authCode,
				AVSCode = trans.AVSResponse,
				CardResponseCode = trans.cardCodeResponse,
				CAVVCode = trans.CAVVResponse
			};
			if (trans.FDSFilters != null)
			{
				for (int i = 0; i < (int)trans.FDSFilters.Length; i++)
				{
					FDSFilterType filter = trans.FDSFilters[i];
					result.FraudFilters.Add(filter.name);
				}
			}
			if (trans.batch != null)
			{
				result.BatchSettlementID = trans.batch.batchId;
				result.BatchSettlementState = trans.batch.settlementState;
				result.BatchSettledOn = trans.batch.settlementTimeUTC;
			}
			if (trans.order != null)
			{
				result.InvoiceNumber = trans.order.invoiceNumber;
				result.PONumber = trans.order.purchaseOrderNumber;
				result.OrderDescription = trans.order.description;
			}
			result.RequestedAmount = trans.requestedAmount;
			result.AuthorizationAmount = trans.authAmount;
			result.SettleAmount = trans.settleAmount;
			if (trans.tax != null)
			{
				result.Tax = trans.tax.amount;
				result.TaxDescription = trans.tax.description;
			}
			if (trans.shipping != null)
			{
				result.Shipping = trans.shipping.amount;
				result.ShippingDescription = trans.shipping.description;
			}
			if (trans.duty != null)
			{
				result.Duty = trans.duty.amount;
				result.DutyDescription = trans.duty.description;
			}
			if (trans.lineItems != null)
			{
				for (int i = 0; i < (int)trans.lineItems.Length; i++)
				{
					lineItemType item = trans.lineItems[i];
					LineItem line = new LineItem()
					{
						Description = item.description,
						ID = item.itemId,
						Name = item.name,
						Quantity = item.quantity,
						Taxable = item.taxable,
						UnitPrice = item.unitPrice
					};
					result._lineItems.Add(line);
				}
			}
			if (trans.payment != null && trans.payment.Item.GetType() == typeof(creditCardType))
			{
				creditCardType cc = (creditCardType)trans.payment.Item;
				result.CardNumber = cc.cardNumber;
				result.CardExpiration = cc.expirationDate;
			}
			if (trans.customer != null)
			{
				result.CustomerID = trans.customer.id;
				result.CustomerEmail = trans.customer.email;
			}
			if (trans.billTo != null)
			{
				result.BillingAddress = new Address(trans.billTo);
			}
			if (trans.shipTo != null)
			{
				result.ShippingAddress = new Address(trans.shipTo);
			}
			result.IsRecurring = trans.recurringBilling;
			result.TaxExempt = trans.taxExempt;
			return result;
		}

		/// <summary>
		/// Creates a list of Transactions directly from the API Response.
		/// </summary>
		/// <param name="transactions">The transactions.</param>
		/// <returns></returns>
		public static List<Transaction> NewListFromResponse(transactionSummaryType[] transactions)
		{
			List<Transaction> result = new List<Transaction>();
			if (transactions != null)
			{
				for (int i = 0; i < (int)transactions.Length; i++)
				{
					result.Add(Transaction.NewFromResponse(transactions[i]));
				}
			}
			return result;
		}
	}
}