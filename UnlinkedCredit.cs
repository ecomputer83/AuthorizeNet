using System;

namespace AuthorizeNet
{
	public class UnlinkedCredit : GatewayRequest
	{
		public UnlinkedCredit(decimal amount, string cardNumber)
		{
			base.SetApiAction(RequestAction.UnlinkedCredit);
			base.Queue("x_amount", amount.ToString());
			base.Queue("x_card_num", cardNumber);
		}
	}
}