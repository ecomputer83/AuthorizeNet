using System;

namespace AuthorizeNet
{
	/// <summary>
	/// The type of API Request being performed
	/// </summary>
	public enum RequestAction
	{
		/// <summary>
		/// Credit Card authorization
		/// </summary>
		Authorize,
		/// <summary>
		/// Settlement of a previously authorized transaction
		/// </summary>
		Capture,
		/// <summary>
		/// An authorization and capture all in one
		/// </summary>
		AuthorizeAndCapture,
		/// <summary>
		/// A Credit
		/// </summary>
		Credit,
		/// <summary>
		/// Voiding of a previously authorized transaction
		/// </summary>
		Void,
		/// <summary>
		/// Capturing of a prior authorization
		/// </summary>
		PriorAuthCapture,
		/// <summary>
		/// Issue a credit for a transaction not based with the API
		/// </summary>
		UnlinkedCredit
	}
}