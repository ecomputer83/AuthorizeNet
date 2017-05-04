using System;

namespace AuthorizeNet
{
	/// <summary>
	/// Captures a prior authorization
	/// </summary>
	public class CardPresentPriorAuthCapture : GatewayRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CardPresentPriorAuthCapture" /> class.
		/// </summary>
		/// <param name="transactionID">The transaction ID.</param>
		/// <param name="amount">The amount.</param>
		public CardPresentPriorAuthCapture(string transactionID, decimal amount)
		{
			base.SetApiAction(RequestAction.PriorAuthCapture);
			base.Queue("x_ref_trans_id", transactionID);
		}
	}
}