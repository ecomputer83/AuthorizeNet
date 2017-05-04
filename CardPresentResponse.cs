using System;

namespace AuthorizeNet
{
	/// <summary>
	/// Parses the response from the transaction (a piped string) into a represenational class
	/// </summary>
	public class CardPresentResponse : ResponseBase, IGatewayResponse
	{
		/// <summary>
		/// Gets the amount.
		/// </summary>
		/// <value>The amount.</value>
		public decimal Amount
		{
			get
			{
				return base.ParseDecimal(24);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:AuthorizeNet.CardPresentResponse" /> is approved.
		/// </summary>
		/// <value><c>true</c> if approved; otherwise, <c>false</c>.</value>
		public bool Approved
		{
			get
			{
				return base.ParseInt(1) == 1;
			}
		}

		/// <summary>
		/// Gets the authorization code.
		/// </summary>
		/// <value>The authorization code.</value>
		public string AuthorizationCode
		{
			get
			{
				return base.ParseResponse(4);
			}
		}

		/// <summary>
		/// Gets the AVS response.
		/// </summary>
		/// <value>The AVS response.</value>
		public string AVSResponse
		{
			get
			{
				string str = base.ParseResponse(5);
				string str1 = str;
				if (str != null)
				{
					switch (str1)
					{
						case "A":
						{
							return "Address (Street): matches, ZIP does not";
						}
						case "B":
						{
							return "Address information not provided for AVS check";
						}
						case "E":
						{
							return "AVS error ";
						}
						case "G":
						{
							return "Non-U.S. Card Issuing Bank ";
						}
						case "N":
						{
							return "No Match on Address (Street) or ZIP ";
						}
						case "P":
						{
							return "AVS not applicable for this transaction ";
						}
						case "R":
						{
							return "Retry — System unavailable or timed out ";
						}
						case "S":
						{
							return "Service not supported by issuer ";
						}
						case "U":
						{
							return "Address information is unavailable ";
						}
						case "W":
						{
							return "Nine digit ZIP matches, Address (Street): does not ";
						}
						case "X":
						{
							return "Address (Street) and nine digit ZIP match ";
						}
						case "Y":
						{
							return "Address (Street) and five digit ZIP match ";
						}
						case "Z":
						{
							return "Five digit ZIP matches, Address (Street) does not";
						}
					}
				}
				return "";
			}
		}

		/// <summary>
		/// Gets the card code response.
		/// </summary>
		/// <value>The card code response.</value>
		public string CardCodeResponse
		{
			get
			{
				string code = base.ParseResponse(6);
				string result = "No result";
				string str = code;
				string str1 = str;
				if (str != null)
				{
					if (str1 == "M")
					{
						return "Match";
					}
					if (str1 == "N")
					{
						return "No Match";
					}
					if (str1 == "P")
					{
						return "Not Processed";
					}
					if (str1 == "S")
					{
						return "Should have been present";
					}
					if (str1 == "U")
					{
						return "Issuer unable to process request";
					}
				}
				return result;
			}
		}

		/// <summary>
		/// Gets the card number.
		/// </summary>
		/// <value>The card number.</value>
		public string CardNumber
		{
			get
			{
				return base.ParseResponse(20);
			}
		}

		/// <summary>
		/// Gets the invoice number.
		/// </summary>
		/// <value>The invoice number.</value>
		public string InvoiceNumber
		{
			get
			{
				return base.ParseResponse(9);
			}
		}

		/// <summary>
		/// Gets the MD5 hash.
		/// </summary>
		/// <value>The MD5 hash.</value>
		public string MD5Hash
		{
			get
			{
				return base.ParseResponse(8);
			}
		}

		/// <summary>
		/// Gets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message
		{
			get
			{
				return base.ParseResponse(3);
			}
		}

		/// <summary>
		/// Gets the remaining balance on card.
		/// </summary>
		/// <value>The remaining balance on card.</value>
		public decimal RemainingBalanceOnCard
		{
			get
			{
				return base.ParseDecimal(25);
			}
		}

		/// <summary>
		/// Gets the requested amount.
		/// </summary>
		/// <value>The requested amount.</value>
		public decimal RequestedAmount
		{
			get
			{
				return base.ParseDecimal(23);
			}
		}

		/// <summary>
		/// Gets the response code.
		/// </summary>
		/// <value>The response code.</value>
		public string ResponseCode
		{
			get
			{
				return base.ParseResponse(1);
			}
		}

		/// <summary>
		/// Gets the split tender ID.
		/// </summary>
		/// <value>The split tender ID.</value>
		public string SplitTenderID
		{
			get
			{
				return base.ParseResponse(22);
			}
		}

		/// <summary>
		/// Gets the transaction ID.
		/// </summary>
		/// <value>The transaction ID.</value>
		public string TransactionID
		{
			get
			{
				return base.ParseResponse(7);
			}
		}

		/// <summary>
		/// Gets the user reference.
		/// </summary>
		/// <value>The user reference.</value>
		public string UserReference
		{
			get
			{
				return base.ParseResponse(9);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:AuthorizeNet.CardPresentResponse" /> class.
		/// </summary>
		/// <param name="RawResponse">The raw response.</param>
		public CardPresentResponse(string[] RawResponse)
		{
			this.RawResponse = RawResponse;
		}
	}
}