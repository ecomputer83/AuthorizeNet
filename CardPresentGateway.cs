using System;

namespace AuthorizeNet
{
	/// <summary>
	/// The gateway which runs the credit card transaction
	/// </summary>
	public class CardPresentGateway : Gateway, ICardPresentGateway
	{
		private string _serviceUrl = "https://cardpresent.authorize.net/gateway/transact.dll";

		private DeviceType _deviceType = DeviceType.PersonalComputerBasedTerminal;

		private string _marketType = "2";

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CardPresentGateway" /> class.
		/// </summary>
		/// <param name="apiLogin">The API login.</param>
		/// <param name="transactionKey">The transaction key.</param>
		/// <param name="isTest">if set to <c>true</c> [is test].</param>
		public CardPresentGateway(string apiLogin, string transactionKey, bool isTest) : base(apiLogin, transactionKey, isTest)
		{
			if (isTest)
			{
				this._serviceUrl = "https://test.authorize.net/gateway/transact.dll";
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CardPresentGateway" /> class.
		/// </summary>
		/// <param name="apiLogin">The API login.</param>
		/// <param name="transactionKey">The transaction key.</param>
		/// <param name="marketType">Type of the market.</param>
		/// <param name="deviceType">Type of the device.</param>
		/// <param name="isTest">if set to <c>true</c> [is test].</param>
		public CardPresentGateway(string apiLogin, string transactionKey, string marketType, DeviceType deviceType, bool isTest) : base(apiLogin, transactionKey, isTest)
		{
			this._deviceType = deviceType;
			this._marketType = marketType;
			if (isTest)
			{
				this._serviceUrl = "https://test.authorize.net/gateway/transact.dll";
			}
		}

		/// <summary>
		/// Sends the specified request.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="description">The description.</param>
		/// <returns></returns>
		public override IGatewayResponse Send(IGatewayRequest request, string description)
		{
			int device = (int)this._deviceType;
			request.Queue("x_cpversion", "1.0");
			request.Queue("x_market_type", this._marketType);
			request.Queue("x_device_type", device.ToString());
			request.Queue("x_response_format", "1");
			base.LoadAuthorization(request);
			request.Queue("x_description", description);
			request.RelayResponse = "";
			string response = base.SendRequest(this._serviceUrl, request);
			char[] chrArray = new char[] { '|' };
			return new CardPresentResponse(response.Split(chrArray));
		}
	}
}