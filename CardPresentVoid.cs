using System;

namespace AuthorizeNet
{
	/// <summary>
	/// A Cardpresent Void transaction
	/// </summary>
	public class CardPresentVoid : GatewayRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CardPresentVoid" /> class.
		/// </summary>
		/// <param name="transactionID">The transaction ID.</param>
		public CardPresentVoid(string transactionID)
		{
			base.SetApiAction(RequestAction.Void);
			base.Queue("x_ref_trans_id", transactionID);
		}
	}
}