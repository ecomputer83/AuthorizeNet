using System;

namespace AuthorizeNet
{
	public class EcheckRequest : GatewayRequest
	{
		/// <summary>
		/// Creates an ECheck transaction (defaulted to WEB) request for use with the AIM gateway
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="bankABACode">The valid routing number of the customer’s bank</param>
		/// <param name="bankAccountNumber">The customer’s valid bank account number</param>
		/// <param name="acctType">CHECKING, BUSINESSCHECKING, SAVINGS</param>
		/// <param name="bankName">The name of the bank that holds the customer’s account</param>
		/// <param name="acctName">The name associated with the bank account</param>
		/// <param name="bankCheckNumber">The check number on the customer’s paper check</param>
		public EcheckRequest(decimal amount, string bankABACode, string bankAccountNumber, AuthorizeNet.BankAccountType acctType, string bankName, string acctName, string bankCheckNumber) : this(AuthorizeNet.EcheckType.WEB, amount, bankABACode, bankAccountNumber, acctType, bankName, acctName, bankCheckNumber)
		{
		}

		/// <summary>
		/// Creates an ECheck transaction request for use with the AIM gateway
		/// </summary>
		/// <param name="type">The Echeck Transaction type: ARC, BOC, CCD, PPD, TEL, WEB</param>
		/// <param name="amount"></param>
		/// <param name="bankABACode">The valid routing number of the customer’s bank</param>
		/// <param name="bankAccountNumber">The customer’s valid bank account number</param>
		/// <param name="acctType">CHECKING, BUSINESSCHECKING, SAVINGS</param>
		/// <param name="bankName">The name of the bank that holds the customer’s account</param>
		/// <param name="acctName">The name associated with the bank account</param>
		/// <param name="bankCheckNumber">The check number on the customer’s paper check</param>
		public EcheckRequest(AuthorizeNet.EcheckType type, decimal amount, string bankABACode, string bankAccountNumber, AuthorizeNet.BankAccountType acctType, string bankName, string acctName, string bankCheckNumber)
		{
			base.Queue("x_method", "ECHECK");
			base.BankABACode = bankABACode;
			base.BankAccountName = acctName;
			base.BankAccountNumber = bankAccountNumber;
			base.BankAccountType = acctType;
			base.BankCheckNumber = bankCheckNumber;
			base.Amount = amount.ToString();
		}

		/// <summary>
		/// Sets the eCheck request as a recurring payment
		/// </summary>
		/// <returns></returns>
		public EcheckRequest AsRecurring()
		{
			base.RecurringBilling = "Y";
			return this;
		}
	}
}