using System;

namespace AuthorizeNet
{
	/// <summary>
	/// The status for the transaction
	/// </summary>
	public enum TransactionStatus
	{
		/// <summary>
		/// Approved
		/// </summary>
		Approved = 1,
		/// <summary>
		/// Declined
		/// </summary>
		Declined = 2,
		/// <summary>
		/// Error
		/// </summary>
		Error = 3,
		/// <summary>
		/// HeldForReview
		/// </summary>
		HeldForReview = 4
	}
}