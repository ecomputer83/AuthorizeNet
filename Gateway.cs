using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace AuthorizeNet
{
	public class Gateway : IGateway
	{
		public const string TEST_URL = "https://test.authorize.net/gateway/transact.dll";

		public const string LIVE_URL = "https://secure.authorize.net/gateway/transact.dll";

		public string ApiLogin
		{
			get;
			set;
		}

		public bool TestMode
		{
			get;
			set;
		}

		public string TransactionKey
		{
			get;
			set;
		}

		public Gateway(string apiLogin, string transactionKey, bool testMode)
		{
			this.ApiLogin = apiLogin;
			this.TransactionKey = transactionKey;
			this.TestMode = testMode;
		}

		public Gateway(string apiLogin, string transactionKey) : this(apiLogin, transactionKey, true)
		{
		}

		/// <summary>
		/// Decides the response.
		/// </summary>
		/// <param name="rawResponse">The raw response.</param>
		/// <returns></returns>
		public IGatewayResponse DecideResponse(string[] rawResponse)
		{
			if ((int)rawResponse.Length == 1)
			{
				throw new InvalidDataException(string.Concat("There was an error returned from AuthorizeNet: ", rawResponse[0], "; this usually means your data sent along was incorrect. Please recheck that all dates and amounts are formatted correctly"));
			}
			return new GatewayResponse(rawResponse);
		}

		protected void LoadAuthorization(IGatewayRequest request)
		{
			request.Queue("x_login", this.ApiLogin);
			request.Queue("x_tran_key", this.TransactionKey);
		}

		public IGatewayResponse Send(IGatewayRequest request)
		{
			return this.Send(request, null);
		}

		public virtual IGatewayResponse Send(IGatewayRequest request, string description)
		{
			string serviceUrl = "https://test.authorize.net/gateway/transact.dll";
			if (!this.TestMode)
			{
				serviceUrl = "https://secure.authorize.net/gateway/transact.dll";
			}
			this.LoadAuthorization(request);
			if (string.IsNullOrEmpty(request.Description))
			{
				request.Queue("x_description", description);
			}
			string response = this.SendRequest(serviceUrl, request);
			char[] chrArray = new char[] { '|' };
			return this.DecideResponse(response.Split(chrArray));
		}

		protected string SendRequest(string serviceUrl, IGatewayRequest request)
		{
			string postData = request.ToPostString();
			string result = "";
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
			webRequest.Method = "POST";
			webRequest.ContentLength = (long)postData.Length;
			webRequest.ContentType = "application/x-www-form-urlencoded";
			StreamWriter myWriter = null;
			myWriter = new StreamWriter(webRequest.GetRequestStream());
			myWriter.Write(postData);
			myWriter.Close();
			using (StreamReader responseStream = new StreamReader(((HttpWebResponse)webRequest.GetResponse()).GetResponseStream()))
			{
				result = responseStream.ReadToEnd();
				responseStream.Close();
			}
			return result;
		}

		private class PolicyOverride : ICertificatePolicy
		{
			public PolicyOverride()
			{
			}

			bool System.Net.ICertificatePolicy.CheckValidationResult(ServicePoint srvPoint, X509Certificate cert, WebRequest request, int certificateProblem)
			{
				return true;
			}
		}
	}
}