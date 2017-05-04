using System;

namespace AuthorizeNet
{
	/// <summary>
	/// A request representing a Capture - the final transfer of funds that happens after an auth.
	/// </summary>
	public class CaptureRequest : GatewayRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CaptureRequest" /> class.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <param name="transactionId">The transaction id.</param>
		/// <param name="authCode">The auth code.</param>
		public CaptureRequest(decimal amount, string transactionId, string authCode)
		{
			base.SetApiAction(RequestAction.Capture);
			base.Queue("x_amount", amount.ToString());
			base.Queue("x_trans_id", transactionId);
			base.Queue("x_auth_code", authCode);
		}
	}
}