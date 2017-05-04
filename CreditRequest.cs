using System;

namespace AuthorizeNet
{
	/// <summary>
	/// Credits, or refunds, the amount back to the user
	/// </summary>
	public class CreditRequest : GatewayRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CreditRequest" /> class.
		/// </summary>
		/// <param name="transactionId">The transaction id.</param>
		/// <param name="amount">The amount.</param>
		/// <param name="cardNumber">The card number.</param>
		public CreditRequest(string transactionId, decimal amount, string cardNumber)
		{
			base.SetApiAction(RequestAction.Credit);
			base.Queue("x_trans_id", transactionId);
			base.Queue("x_amount", amount.ToString());
			base.Queue("x_card_num", cardNumber);
		}
	}
}