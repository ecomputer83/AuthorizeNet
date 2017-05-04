using System;

namespace AuthorizeNet
{
	public interface IGatewayResponse
	{
		decimal Amount
		{
			get;
		}

		bool Approved
		{
			get;
		}

		string AuthorizationCode
		{
			get;
		}

		string CardNumber
		{
			get;
		}

		string InvoiceNumber
		{
			get;
		}

		string Message
		{
			get;
		}

		string ResponseCode
		{
			get;
		}

		string TransactionID
		{
			get;
		}
	}
}