using AuthorizeNet.APICore;
using System;
using System.Runtime.CompilerServices;

namespace AuthorizeNet
{
	/// <summary>
	/// This is the abstracted SubscriptionRequest class - it provides a simplified way of dealing with the underlying
	/// ARB API. This class uses a Fluent Interface to build out the request - creating only what you need.
	/// </summary>
	public class SubscriptionRequest : ISubscriptionRequest
	{
		public decimal Amount
		{
			get;
			set;
		}

		public Address BillingAddress
		{
			get;
			set;
		}

		public short BillingCycles
		{
			get;
			set;
		}

		public short BillingInterval
		{
			get;
			set;
		}

		public AuthorizeNet.BillingIntervalUnits BillingIntervalUnits
		{
			get;
			set;
		}

		public string CardCode
		{
			get;
			set;
		}

		public int CardExpirationMonth
		{
			get;
			set;
		}

		public int CardExpirationYear
		{
			get;
			set;
		}

		public string CardNumber
		{
			get;
			set;
		}

		public string CustomerEmail
		{
			get;
			set;
		}

		public Address ShippingAddress
		{
			get;
			set;
		}

		public DateTime StartsOn
		{
			get;
			set;
		}

		public string SubscriptionID
		{
			get;
			set;
		}

		public string SubscriptionName
		{
			get;
			set;
		}

		public decimal TrialAmount
		{
			get;
			set;
		}

		public short TrialBillingCycles
		{
			get;
			set;
		}

		private SubscriptionRequest()
		{
			this.BillingIntervalUnits = AuthorizeNet.BillingIntervalUnits.Months;
			this.BillingInterval = 1;
			this.BillingCycles = 9999;
			this.StartsOn = DateTime.Today;
		}

		public static SubscriptionRequest CreateAnnual(string email, string subscriptionName, decimal amount)
		{
			return SubscriptionRequest.CreateAnnual(email, subscriptionName, amount, 9999);
		}

		/// <summary>
		/// Creates an annual subscription.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="subscriptionName">Name of the subscription.</param>
		/// <param name="amount">The amount.</param>
		/// <param name="numberOfBillings">The number of billings. So if you wanted to create a yearly subscription that lasts for a year - this would be 1</param>
		/// <returns></returns>
		public static SubscriptionRequest CreateAnnual(string email, string subscriptionName, decimal amount, short numberOfBillings)
		{
			SubscriptionRequest sub = new SubscriptionRequest()
			{
				CustomerEmail = email,
				Amount = amount,
				SubscriptionName = subscriptionName,
				BillingCycles = numberOfBillings,
				BillingInterval = 12
			};
			return sub;
		}

		public static SubscriptionRequest CreateMonthly(string email, string subscriptionName, decimal amount)
		{
			return SubscriptionRequest.CreateMonthly(email, subscriptionName, amount, 9999);
		}

		/// <summary>
		/// Creates a monthly subscription request.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="subscriptionName">Name of the subscription.</param>
		/// <param name="amount">The amount.</param>
		/// <param name="numberOfBillings">The number of billings. So if you wanted to create a monthly subscription that lasts for a year - this would be 12</param>
		/// <returns></returns>
		public static SubscriptionRequest CreateMonthly(string email, string subscriptionName, decimal amount, short numberOfBillings)
		{
			SubscriptionRequest sub = new SubscriptionRequest()
			{
				CustomerEmail = email,
				Amount = amount,
				SubscriptionName = subscriptionName,
				BillingCycles = numberOfBillings
			};
			return sub;
		}

		/// <summary>
		/// Creates a weekly subscription that bills every 7 days.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="subscriptionName">Name of the subscription.</param>
		/// <param name="amount">The amount.</param>
		/// <returns></returns>
		public static SubscriptionRequest CreateWeekly(string email, string subscriptionName, decimal amount)
		{
			return SubscriptionRequest.CreateWeekly(email, subscriptionName, amount, 9999);
		}

		/// <summary>
		/// Creates a weekly subscription that bills every 7 days. 
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="subscriptionName">Name of the subscription.</param>
		/// <param name="amount">The amount.</param>
		/// <param name="numberOfBillings">The number of billings. If you want this subscription to last for a month, this should be set to 4</param>
		/// <returns></returns>
		public static SubscriptionRequest CreateWeekly(string email, string subscriptionName, decimal amount, short numberOfBillings)
		{
			SubscriptionRequest sub = new SubscriptionRequest()
			{
				CustomerEmail = email,
				Amount = amount,
				SubscriptionName = subscriptionName,
				BillingCycles = numberOfBillings,
				BillingIntervalUnits = AuthorizeNet.BillingIntervalUnits.Days,
				BillingInterval = 7
			};
			return sub;
		}

		/// <summary>
		/// Sets a trial period for the subscription. This is part of the overall subscription plan.
		/// </summary>
		/// <param name="trialBillingCycles">The trial billing cycles.</param>
		/// <param name="trialAmount">The trial amount.</param>
		/// <returns></returns>
		public SubscriptionRequest SetTrialPeriod(short trialBillingCycles, decimal trialAmount)
		{
			this.TrialBillingCycles = trialBillingCycles;
			this.TrialAmount = trialAmount;
			return this;
		}

		/// <summary>
		/// This is mostly for internal processing needs - it takes the SubscriptionRequest and turns it into something the Gateway can serialize.
		/// </summary>
		/// <returns></returns>
		public ARBSubscriptionType ToAPI()
		{
			ARBSubscriptionType sub = new ARBSubscriptionType()
			{
				name = this.SubscriptionName
			};
			if (string.IsNullOrEmpty(this.CardNumber))
			{
				throw new InvalidOperationException("Need a credit card number to set up this subscription");
			}
			creditCardType creditCard = new creditCardType()
			{
				cardNumber = this.CardNumber,
				expirationDate = string.Format("{0}-{1}", this.CardExpirationYear, this.CardExpirationMonth)
			};
			sub.payment = new paymentType()
			{
				Item = creditCard
			};
			if (this.BillingAddress != null)
			{
				sub.billTo = this.BillingAddress.ToAPINameAddressType();
			}
			if (this.ShippingAddress != null)
			{
				sub.shipTo = this.ShippingAddress.ToAPINameAddressType();
			}
			sub.paymentSchedule = new paymentScheduleType()
			{
				startDate = this.StartsOn,
				startDateSpecified = true,
				totalOccurrences = this.BillingCycles,
				totalOccurrencesSpecified = true
			};
			if (this.TrialBillingCycles > 0)
			{
				sub.paymentSchedule.trialOccurrences = this.TrialBillingCycles;
				sub.paymentSchedule.trialOccurrencesSpecified = true;
			}
			if (this.TrialAmount > new decimal(0))
			{
				sub.trialAmount = this.TrialAmount;
				sub.trialAmountSpecified = true;
			}
			sub.amount = this.Amount;
			sub.amountSpecified = true;
			sub.paymentSchedule.interval = new paymentScheduleTypeInterval()
			{
				length = this.BillingInterval
			};
			if (this.BillingIntervalUnits != AuthorizeNet.BillingIntervalUnits.Months)
			{
				sub.paymentSchedule.interval.unit = ARBSubscriptionUnitEnum.days;
			}
			else
			{
				sub.paymentSchedule.interval.unit = ARBSubscriptionUnitEnum.months;
			}
			sub.customer = new customerType()
			{
				email = this.CustomerEmail
			};
			return sub;
		}

		/// <summary>
		/// The Update function won't accept a change to some values - specifically the billing interval. This creates a request
		/// that the API can understand for updates only
		/// </summary>
		/// <returns></returns>
		public ARBSubscriptionType ToUpdateableAPI()
		{
			ARBSubscriptionType sub = new ARBSubscriptionType()
			{
				name = this.SubscriptionName
			};
			if (string.IsNullOrEmpty(this.CardNumber))
			{
				throw new InvalidOperationException("Need a credit card number to set up this subscription");
			}
			creditCardType creditCard = new creditCardType()
			{
				cardNumber = this.CardNumber,
				expirationDate = string.Format("{0}-{1}", this.CardExpirationYear, this.CardExpirationMonth)
			};
			sub.payment = new paymentType()
			{
				Item = creditCard
			};
			if (this.BillingAddress != null)
			{
				sub.billTo = this.BillingAddress.ToAPINameAddressType();
			}
			if (this.ShippingAddress != null)
			{
				sub.shipTo = this.ShippingAddress.ToAPINameAddressType();
			}
			sub.paymentSchedule = new paymentScheduleType()
			{
				startDate = this.StartsOn,
				startDateSpecified = true,
				totalOccurrences = this.BillingCycles,
				totalOccurrencesSpecified = true
			};
			sub.amount = this.Amount;
			sub.amountSpecified = true;
			sub.customer = new customerType()
			{
				email = this.CustomerEmail
			};
			return sub;
		}

		/// <summary>
		/// Adds a credit card payment to the subscription. This is required.
		/// </summary>
		/// <param name="firstName">The first name.</param>
		/// <param name="lastName">The last name.</param>
		/// <param name="cardNumber">The card number.</param>
		/// <param name="cardExpirationYear">The card expiration year.</param>
		/// <param name="cardExpirationMonth">The card expiration month.</param>
		/// <returns></returns>
		public SubscriptionRequest UsingCreditCard(string firstName, string lastName, string cardNumber, int cardExpirationYear, int cardExpirationMonth)
		{
			this.CardNumber = cardNumber;
			this.CardExpirationYear = cardExpirationYear;
			this.CardExpirationMonth = cardExpirationMonth;
			Address address = new Address()
			{
				First = firstName,
				Last = lastName
			};
			this.BillingAddress = address;
			return this;
		}

		/// <summary>
		/// Adds a full billing address - which is required for a credit card.
		/// </summary>
		/// <param name="add">The add.</param>
		/// <returns></returns>
		public SubscriptionRequest WithBillingAddress(Address add)
		{
			this.BillingAddress = add;
			return this;
		}

		/// <summary>
		/// Adds a shipping address to the request.
		/// </summary>
		/// <param name="add">The address to ship to</param>
		/// <returns></returns>
		public SubscriptionRequest WithShippingAddress(Address add)
		{
			this.ShippingAddress = add;
			return this;
		}
	}
}