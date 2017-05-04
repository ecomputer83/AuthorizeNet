using System;

namespace AuthorizeNet
{
	/// <summary>
	/// Capture only function
	/// </summary>
	public class CardPresentCaptureOnly : GatewayRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CardPresentCaptureOnly" /> class.
		/// </summary>
		/// <param name="authCode">The auth code.</param>
		public CardPresentCaptureOnly(string authCode)
		{
			base.SetApiAction(RequestAction.Capture);
			base.Queue("x_auth_code", authCode);
		}
	}
}