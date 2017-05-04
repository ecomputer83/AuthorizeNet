using AuthorizeNet.APICore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AuthorizeNet
{
	/// <summary>
	/// A class representing a batch-settlement
	/// </summary>
	public class Batch
	{
		/// <summary>
		/// Gets or sets the charges.
		/// </summary>
		/// <value>The charges.</value>
		public List<Charge> Charges
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the ID.
		/// </summary>
		/// <value>The ID.</value>
		public string ID
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the payment method.
		/// </summary>
		/// <value>The payment method.</value>
		public string PaymentMethod
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the settled on.
		/// </summary>
		/// <value>The settled on.</value>
		public DateTime SettledOn
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>The state.</value>
		public string State
		{
			get;
			set;
		}

		public Batch()
		{
		}

		/// <summary>
		/// Creates a new batch from a stats response
		/// </summary>
		public static Batch NewFromResponse(getBatchStatisticsResponse batch)
		{
			Batch batch1 = new Batch()
			{
				ID = batch.batchDetails.batchId,
				PaymentMethod = batch.batchDetails.paymentMethod,
				SettledOn = batch.batchDetails.settlementTimeUTC,
				State = batch.batchDetails.settlementState,
				Charges = Charge.NewFromStat(batch.batchDetails.statistics)
			};
			return batch1;
		}

		/// <summary>
		/// Creates a list of batches directly from the API Response
		/// </summary>
		/// <param name="batches">The batches.</param>
		/// <returns></returns>
		public static List<Batch> NewFromResponse(getSettledBatchListResponse batches)
		{
			List<Batch> result = new List<Batch>();
			for (int i = 0; i < (int)batches.batchList.Length; i++)
			{
				batchDetailsType item = batches.batchList[i];
				Batch batch = new Batch()
				{
					Charges = Charge.NewFromStat(item.statistics),
					ID = item.batchId,
					PaymentMethod = item.paymentMethod,
					SettledOn = item.settlementTimeUTC,
					State = item.settlementState
				};
				result.Add(batch);
			}
			return result;
		}
	}
}