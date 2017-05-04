using System;

namespace AuthorizeNet
{
	public interface ICardPresentGateway
	{
		IGatewayResponse Send(IGatewayRequest request, string description);
	}
}