using System;

namespace AuthorizeNet
{
	/// <summary>
	/// A request representing a Void of a previously authorized transaction
	/// </summary>
	public class VoidRequest : GatewayRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.VoidRequest" /> class.
		/// </summary>
		/// <param name="transactionId">The transaction id.</param>
		public VoidRequest(string transactionId)
		{
			base.SetApiAction(RequestAction.Void);
			base.Queue("x_trans_id", transactionId);
		}
	}
}