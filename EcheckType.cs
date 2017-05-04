using System;

namespace AuthorizeNet
{
	/// <summary>
	/// The type of eCheck transaction
	/// </summary>
	public enum EcheckType
	{
		/// <summary>
		/// Accounts Receivable Conversion
		/// </summary>
		ARC,
		/// <summary>
		/// Back Office Conversion
		/// </summary>
		BOC,
		/// <summary>
		/// Cash Concentration or Disbursement
		/// </summary>
		CCD,
		/// <summary>
		/// Prearranged Payment and Deposit Entry
		/// </summary>
		PPD,
		/// <summary>
		/// Telephone-Initiated Entry
		/// </summary>
		TEL,
		/// <summary>
		/// Internet-Initiated Entry
		/// </summary>
		WEB
	}
}