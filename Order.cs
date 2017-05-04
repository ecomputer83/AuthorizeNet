using AuthorizeNet.APICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AuthorizeNet
{
	/// <summary>
	/// This is an abstraction for the AuthNET API so you can specify detailed order information.
	/// </summary>
	public class Order
	{
		internal List<lineItemType> _lineItems;

		public decimal Amount
		{
			get;
			set;
		}

		public string CardCode
		{
			get;
			set;
		}

		public string CustomerProfileID
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string ID
		{
			get;
			set;
		}

		public string InvoiceNumber
		{
			get;
			set;
		}

		public string PaymentProfileID
		{
			get;
			set;
		}

		public string PONumber
		{
			get;
			set;
		}

		public bool? RecurringBilling
		{
			get;
			set;
		}

		public decimal SalesTaxAmount
		{
			get;
			set;
		}

		public string SalesTaxName
		{
			get;
			set;
		}

		public string ShippingAddressProfileID
		{
			get;
			set;
		}

		public decimal ShippingAmount
		{
			get;
			set;
		}

		public string ShippingName
		{
			get;
			set;
		}

		public decimal SubTotal
		{
			get
			{
				if (this._lineItems.Count <= 0)
				{
					return this.Amount;
				}
				return this._lineItems.Sum<lineItemType>((lineItemType x) => x.quantity * x.unitPrice);
			}
		}

		public bool? TaxExempt
		{
			get;
			set;
		}

		public decimal Total
		{
			get
			{
				return (this.SubTotal + this.SalesTaxAmount) + this.ShippingAmount;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.Order" /> class.
		/// </summary>
		/// <param name="profileID">The profile ID.</param>
		/// <param name="paymentProfileId">The payment profile id.</param>
		/// <param name="shippingAddressID">The shipping address ID.</param>
		public Order(string profileID, string paymentProfileId, string shippingAddressID)
		{
			this._lineItems = new List<lineItemType>();
			this.ID = Guid.NewGuid().ToString();
			this.CustomerProfileID = profileID;
			this.PaymentProfileID = paymentProfileId;
			if (!string.IsNullOrEmpty(shippingAddressID))
			{
				this.ShippingAddressProfileID = shippingAddressID;
			}
		}

		public void AddLineItem(string ID, string name, string description, int quantity, decimal unitPrice, bool? taxable)
		{
			if (this._lineItems.Any<lineItemType>((lineItemType x) => x.itemId == ID))
			{
				lineItemType _lineItemType = this._lineItems.First<lineItemType>((lineItemType x) => x.itemId == ID);
				_lineItemType.quantity = _lineItemType.quantity + quantity;
				return;
			}
			lineItemType _lineItemType1 = new lineItemType()
			{
				description = this.Description,
				itemId = ID,
				name = name,
				quantity = quantity,
				unitPrice = unitPrice
			};
			lineItemType item = _lineItemType1;
			if (taxable.HasValue)
			{
				item.taxable = taxable.Value;
				item.taxableSpecified = true;
			}
			this._lineItems.Add(item);
		}

		public void RemoveLineItem(string ID)
		{
			lineItemType line = this._lineItems.FirstOrDefault<lineItemType>((lineItemType x) => x.itemId == ID);
			if (line != null)
			{
				this._lineItems.Remove(line);
			}
		}
	}
}