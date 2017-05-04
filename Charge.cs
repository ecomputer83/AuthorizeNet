using AuthorizeNet.APICore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AuthorizeNet
{
	/// <summary>
	/// A helper class for representing a charge
	/// </summary>
	public class Charge
	{
		/// <summary>
		/// Gets or sets the amount.
		/// </summary>
		/// <value>The amount.</value>
		public decimal Amount
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the type of the card.
		/// </summary>
		/// <value>The type of the card.</value>
		public string CardType
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the charge back amount.
		/// </summary>
		/// <value>The charge back amount.</value>
		public decimal ChargeBackAmount
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the charge back count.
		/// </summary>
		/// <value>The charge back count.</value>
		public decimal ChargeBackCount
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the correction notice count.
		/// </summary>
		/// <value>The correction notice count.</value>
		public int CorrectionNoticeCount
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the decline count.
		/// </summary>
		/// <value>The decline count.</value>
		public int DeclineCount
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the refund amount.
		/// </summary>
		/// <value>The refund amount.</value>
		public decimal RefundAmount
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the returned items amount.
		/// </summary>
		/// <value>The returned items amount.</value>
		public decimal ReturnedItemsAmount
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the returned items count.
		/// </summary>
		/// <value>The returned items count.</value>
		public int ReturnedItemsCount
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the void count.
		/// </summary>
		/// <value>The void count.</value>
		public int VoidCount
		{
			get;
			set;
		}

		public Charge()
		{
		}

		/// <summary>
		/// Creates a List of Charges from the Statistics return from the batch.
		/// </summary>
		/// <param name="stats">The stats.</param>
		/// <returns></returns>
		public static List<Charge> NewFromStat(batchStatisticType[] stats)
		{
			List<Charge> result = new List<Charge>();
			if (stats != null)
			{
				for (int i = 0; i < (int)stats.Length; i++)
				{
					batchStatisticType stat = stats[i];
					Charge charge = new Charge()
					{
						Amount = stat.chargeAmount,
						CardType = stat.accountType,
						ChargeBackAmount = stat.chargebackAmount,
						ChargeBackCount = stat.chargebackCount,
						CorrectionNoticeCount = stat.correctionNoticeCount,
						DeclineCount = stat.declineCount,
						RefundAmount = stat.refundAmount,
						ReturnedItemsAmount = stat.refundReturnedItemsAmount,
						ReturnedItemsCount = stat.refundReturnedItemsCount,
						VoidCount = stat.voidCount
					};
					result.Add(charge);
				}
			}
			return result;
		}
	}
}