using System;

namespace AuthorizeNet
{
	public class CardPresentAuthorizationRequest : GatewayRequest
	{
		private string _track1 = "";

		private string _track2 = "";

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CardPresentAuthorizationRequest" /> class using track data from a card reader.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <param name="track1">The track1 data.</param>
		/// <param name="track2">The track2 data.</param>
		public CardPresentAuthorizationRequest(decimal amount, string track1, string track2)
		{
			base.SetApiAction(RequestAction.Authorize);
			track1 = track1.Replace("%", "").Replace("?", "");
			track2 = track2.Replace(";", "").Replace("?", "");
			if (!string.IsNullOrEmpty(track1))
			{
				base.Queue("x_track1", track1);
			}
			if (!string.IsNullOrEmpty(track2))
			{
				base.Queue("x_track2", track2);
			}
			base.Queue("x_amount", amount.ToString());
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CardPresentAuthorizationRequest" /> class.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <param name="cardNumber">The card number.</param>
		/// <param name="expirationMonth">The expiration month.</param>
		/// <param name="expirationYear">The expiration year.</param>
		public CardPresentAuthorizationRequest(decimal amount, string cardNumber, string expirationMonth, string expirationYear)
		{
			base.SetApiAction(RequestAction.Authorize);
			base.Queue("x_card_num", cardNumber);
			base.Queue("x_exp_date", string.Format("{0}{1}", expirationMonth, expirationYear));
			base.Queue("x_amount", amount.ToString());
		}
	}
}