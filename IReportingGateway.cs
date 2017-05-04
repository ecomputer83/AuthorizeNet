using System;
using System.Collections.Generic;

namespace AuthorizeNet
{
	public interface IReportingGateway
	{
		Batch GetBatchStatistics(string batchId);

		List<Batch> GetSettledBatchList(bool includeStats);

		List<Batch> GetSettledBatchList(DateTime from, DateTime to, bool includeStats);

		List<Batch> GetSettledBatchList(DateTime from, DateTime to);

		List<Batch> GetSettledBatchList();

		Transaction GetTransactionDetails(string transactionID);

		List<Transaction> GetTransactionList(DateTime from, DateTime to);

		List<Transaction> GetTransactionList();

		List<Transaction> GetTransactionList(string batchId);

		List<Transaction> GetUnsettledTransactionList();
	}
}