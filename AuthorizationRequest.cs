using System;
using System.Collections.Specialized;

namespace AuthorizeNet
{
	/// <summary>
	/// A request that authorizes a transaction, no capture
	/// </summary>
	public class AuthorizationRequest : GatewayRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.AuthorizationRequest" /> class.
		/// </summary>
		/// <param name="cardNumber">The card number.</param>
		/// <param name="expirationMonthAndYear">The expiration month and year.</param>
		/// <param name="amount">The amount.</param>
		/// <param name="description">The description.</param>
		public AuthorizationRequest(string cardNumber, string expirationMonthAndYear, decimal amount, string description)
		{
			base.SetApiAction(RequestAction.AuthorizeAndCapture);
			this.SetQueue(cardNumber, expirationMonthAndYear, amount, description);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.AuthorizationRequest" /> class.
		/// </summary>
		/// <param name="cardNumber">The card number.</param>
		/// <param name="expirationMonthAndYear">The expiration month and year.</param>
		/// <param name="amount">The amount.</param>
		/// <param name="description">The description.</param>
		/// <param name="includeCapture">if set to <c>true</c> [include capture].</param>
		public AuthorizationRequest(string cardNumber, string expirationMonthAndYear, decimal amount, string description, bool includeCapture)
		{
			if (!includeCapture)
			{
				base.SetApiAction(RequestAction.Authorize);
			}
			else
			{
				base.SetApiAction(RequestAction.AuthorizeAndCapture);
			}
			this.SetQueue(cardNumber, expirationMonthAndYear, amount, description);
		}

		/// <summary>
		/// Loader for use with form POSTS from web
		/// </summary>
		/// <param name="post"></param>
		public AuthorizationRequest(NameValueCollection post)
		{
			base.SetApiAction(RequestAction.AuthorizeAndCapture);
			base.Queue("x_card_num", post["x_card_num"]);
			base.Queue("x_exp_date", post["x_exp_date"]);
			base.Queue("x_amount", post["x_amount"]);
		}

		protected virtual void SetQueue(string cardNumber, string expirationMonthAndYear, decimal amount, string description)
		{
			base.Queue("x_card_num", cardNumber);
			base.Queue("x_exp_date", expirationMonthAndYear);
			base.Queue("x_amount", amount.ToString());
			base.Queue("x_description", description);
		}
	}
}