using System;

namespace AuthorizeNet
{
	/// <summary>
	/// A Credit transaction
	/// </summary>
	public class CardPresentCredit : GatewayRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CardPresentCredit" /> class.
		/// </summary>
		/// <param name="transactionID">The transaction ID.</param>
		public CardPresentCredit(string transactionID)
		{
			base.SetApiAction(RequestAction.Credit);
			base.Queue("x_ref_trans_id", transactionID);
		}
	}
}