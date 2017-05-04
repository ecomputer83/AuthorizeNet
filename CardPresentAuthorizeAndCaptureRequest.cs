using System;

namespace AuthorizeNet
{
	public class CardPresentAuthorizeAndCaptureRequest : CardPresentAuthorizationRequest
	{
		public CardPresentAuthorizeAndCaptureRequest(decimal amount, string track1, string track2) : base(amount, track1, track2)
		{
			base.SetApiAction(RequestAction.AuthorizeAndCapture);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CardPresentAuthorizeAndCaptureRequest" /> class.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <param name="cardNumber">The card number.</param>
		/// <param name="expirationMonth">The expiration month.</param>
		/// <param name="expirationYear">The expiration year.</param>
		public CardPresentAuthorizeAndCaptureRequest(decimal amount, string cardNumber, string expirationMonth, string expirationYear) : base(amount, cardNumber, expirationMonth, expirationYear)
		{
			base.SetApiAction(RequestAction.AuthorizeAndCapture);
		}
	}
}