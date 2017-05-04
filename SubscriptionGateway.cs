using AuthorizeNet.APICore;
using System;

namespace AuthorizeNet
{
	public class SubscriptionGateway : ISubscriptionGateway
	{
		private HttpXmlUtility _gateway;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.SubscriptionGateway" /> class.
		/// </summary>
		/// <param name="apiLogin">The API login.</param>
		/// <param name="transactionKey">The transaction key.</param>
		/// <param name="mode">The mode.</param>
		public SubscriptionGateway(string apiLogin, string transactionKey, ServiceMode mode)
		{
			if (mode == ServiceMode.Live)
			{
				this._gateway = new HttpXmlUtility(ServiceMode.Live, apiLogin, transactionKey);
				return;
			}
			this._gateway = new HttpXmlUtility(ServiceMode.Test, apiLogin, transactionKey);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.SubscriptionGateway" /> class.
		/// </summary>
		/// <param name="apiLogin">The API login.</param>
		/// <param name="transactionKey">The transaction key.</param>
		public SubscriptionGateway(string apiLogin, string transactionKey) : this(apiLogin, transactionKey, ServiceMode.Test)
		{
		}

		/// <summary>
		/// Cancels the subscription.
		/// </summary>
		/// <param name="subscriptionID">The subscription ID.</param>
		/// <returns></returns>
		public bool CancelSubscription(string subscriptionID)
		{
			ARBCancelSubscriptionRequest req = new ARBCancelSubscriptionRequest()
			{
				subscriptionId = subscriptionID
			};
			ARBCancelSubscriptionResponse aRBCancelSubscriptionResponse = (ARBCancelSubscriptionResponse)this._gateway.Send(req);
			return true;
		}

		/// <summary>
		/// Creates a new subscription
		/// </summary>
		/// <param name="subscription">The subscription to create - requires that you add a credit card and billing first and last.</param>
		public ISubscriptionRequest CreateSubscription(ISubscriptionRequest subscription)
		{
			ARBCreateSubscriptionRequest req = new ARBCreateSubscriptionRequest()
			{
				subscription = subscription.ToAPI()
			};
			ARBCreateSubscriptionResponse response = (ARBCreateSubscriptionResponse)this._gateway.Send(req);
			subscription.SubscriptionID = response.subscriptionId;
			return subscription;
		}

		/// <summary>
		/// Gets the subscription status.
		/// </summary>
		/// <param name="subscriptionID">The subscription ID.</param>
		/// <returns></returns>
		public ARBSubscriptionStatusEnum GetSubscriptionStatus(string subscriptionID)
		{
			ARBGetSubscriptionStatusRequest req = new ARBGetSubscriptionStatusRequest()
			{
				subscriptionId = subscriptionID
			};
			return ((ARBGetSubscriptionStatusResponse)this._gateway.Send(req)).status;
		}

		/// <summary>
		/// Updates the subscription.
		/// </summary>
		/// <param name="subscription">The subscription to update. Can't change billing intervals however.</param>
		/// <returns></returns>
		public bool UpdateSubscription(ISubscriptionRequest subscription)
		{
			ARBSubscriptionType sub = subscription.ToUpdateableAPI();
			ARBUpdateSubscriptionRequest req = new ARBUpdateSubscriptionRequest()
			{
				subscription = sub,
				subscriptionId = subscription.SubscriptionID
			};
			ARBUpdateSubscriptionResponse aRBUpdateSubscriptionResponse = (ARBUpdateSubscriptionResponse)this._gateway.Send(req);
			return true;
		}
	}
}