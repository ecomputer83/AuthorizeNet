using AuthorizeNet.APICore;
using System;
using System.Collections.Generic;

namespace AuthorizeNet
{
	/// <summary>
	/// The gateway for requesting Reports from Authorize.Net
	/// </summary>
	public class ReportingGateway : IReportingGateway
	{
		private HttpXmlUtility _gateway;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CustomerGateway" /> class.
		/// </summary>
		/// <param name="apiLogin">The API login.</param>
		/// <param name="transactionKey">The transaction key.</param>
		/// <param name="mode">Test or Live.</param>
		public ReportingGateway(string apiLogin, string transactionKey, ServiceMode mode)
		{
			if (mode == ServiceMode.Live)
			{
				this._gateway = new HttpXmlUtility(ServiceMode.Live, apiLogin, transactionKey);
				return;
			}
			this._gateway = new HttpXmlUtility(ServiceMode.Test, apiLogin, transactionKey);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CustomerGateway" /> class.
		/// </summary>
		/// <param name="apiLogin">The API login.</param>
		/// <param name="transactionKey">The transaction key.</param>
		public ReportingGateway(string apiLogin, string transactionKey) : this(apiLogin, transactionKey, ServiceMode.Test)
		{
		}

		/// <summary>
		/// Returns charges and statistics for a given batch
		/// </summary>
		/// <param name="batchId">the batch id</param>
		/// <returns>a batch with statistics</returns>
		public Batch GetBatchStatistics(string batchId)
		{
			getBatchStatisticsRequest req = new getBatchStatisticsRequest()
			{
				batchId = batchId
			};
			return Batch.NewFromResponse((getBatchStatisticsResponse)this._gateway.Send(req));
		}

		/// <summary>
		/// Returns all Settled Batches for the last 30 days
		/// </summary>
		public List<Batch> GetSettledBatchList(bool includeStats)
		{
			DateTime from = DateTime.Today.AddDays(-30);
			return this.GetSettledBatchList(from, DateTime.Today, includeStats);
		}

		/// <summary>
		/// Returns all Settled Batches for the last 30 days
		/// </summary>
		public List<Batch> GetSettledBatchList()
		{
			DateTime from = DateTime.Today.AddDays(-30);
			return this.GetSettledBatchList(from, DateTime.Today, false);
		}

		/// <summary>
		/// Returns batch settlements for the specified date range
		/// </summary>
		public List<Batch> GetSettledBatchList(DateTime from, DateTime to)
		{
			return this.GetSettledBatchList(from, to, false);
		}

		/// <summary>
		/// Returns batch settlements for the specified date range
		/// </summary>
		public List<Batch> GetSettledBatchList(DateTime from, DateTime to, bool includeStats)
		{
			getSettledBatchListRequest req = new getSettledBatchListRequest()
			{
				firstSettlementDate = from.ToUniversalTime(),
				lastSettlementDate = to.ToUniversalTime(),
				firstSettlementDateSpecified = true,
				lastSettlementDateSpecified = true
			};
			if (includeStats)
			{
				req.includeStatistics = true;
				req.includeStatisticsSpecified = true;
			}
			return Batch.NewFromResponse((getSettledBatchListResponse)this._gateway.Send(req));
		}

		/// <summary>
		/// Returns Transaction details for a given transaction ID (transid)
		/// </summary>
		/// <param name="transactionID"></param>
		public Transaction GetTransactionDetails(string transactionID)
		{
			getTransactionDetailsRequest req = new getTransactionDetailsRequest()
			{
				transId = transactionID
			};
			getTransactionDetailsResponse response = (getTransactionDetailsResponse)this._gateway.Send(req);
			return Transaction.NewFromResponse(response.transaction);
		}

		/// <summary>
		/// Returns all transaction within a particular batch
		/// </summary>
		public List<Transaction> GetTransactionList(string batchId)
		{
			getTransactionListRequest req = new getTransactionListRequest()
			{
				batchId = batchId
			};
			getTransactionListResponse response = (getTransactionListResponse)this._gateway.Send(req);
			return Transaction.NewListFromResponse(response.transactions);
		}

		/// <summary>
		/// Returns all transactions for the last 30 days
		/// </summary>
		/// <returns></returns>
		public List<Transaction> GetTransactionList()
		{
			DateTime today = DateTime.Today;
			return this.GetTransactionList(today.AddDays(-30), DateTime.Today);
		}

		/// <summary>
		/// Returns all transactions for a given time period. This can result in a number of calls to the API
		/// </summary>
		public List<Transaction> GetTransactionList(DateTime from, DateTime to)
		{
			List<Batch> batches = this.GetSettledBatchList(from, to);
			List<Transaction> result = new List<Transaction>();
			foreach (Batch batch in batches)
			{
				result.AddRange(this.GetTransactionList(batch.ID.ToString()));
			}
			return result;
		}

		/// <summary>
		/// returns the most recent 1000 transactions that are unsettled
		/// </summary>
		/// <returns></returns>
		public List<Transaction> GetUnsettledTransactionList()
		{
			getUnsettledTransactionListResponse response = (getUnsettledTransactionListResponse)this._gateway.Send(new getUnsettledTransactionListRequest());
			return Transaction.NewListFromResponse(response.transactions);
		}
	}
}