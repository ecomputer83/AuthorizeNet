using System;
using System.Collections.Generic;

namespace AuthorizeNet
{
	public class GatewayResponse : ResponseBase, IGatewayResponse
	{
		public string Address
		{
			get
			{
				return base.ParseResponse(16);
			}
		}

		public decimal Amount
		{
			get
			{
				return base.ParseDecimal(9);
			}
		}

		public bool Approved
		{
			get
			{
				return this.Code == 1;
			}
		}

		public string AuthorizationCode
		{
			get
			{
				return base.ParseResponse(4);
			}
		}

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
							return "Address (Street) matches, ZIP does not";
						}
						case "B":
						{
							return "Address information not provided for AVS check";
						}
						case "E":
						{
							return "AVS error";
						}
						case "G":
						{
							return "Non-U.S. Card Issuing Bank";
						}
						case "N":
						{
							return "No Match on Address (Street) or ZIP";
						}
						case "P":
						{
							return "AVS not applicable for this transaction";
						}
						case "R":
						{
							return "Retry — System unavailable or timed out";
						}
						case "S":
						{
							return "Service not supported by issuer";
						}
						case "U":
						{
							return "Address information is unavailable";
						}
						case "W":
						{
							return "Nine digit ZIP matches, Address (Street) does not";
						}
						case "X":
						{
							return "Address (Street) and nine digit ZIP match";
						}
						case "Y":
						{
							return "Address (Street) and five digit ZIP match";
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

		public string CardNumber
		{
			get
			{
				return base.ParseResponse(50);
			}
		}

		public string CardType
		{
			get
			{
				return base.ParseResponse(51);
			}
		}

		public string CAVResponse
		{
			get
			{
				string code = base.ParseResponse(39);
				string result = "Blank or not present = CAVV not validated";
				string str = code;
				string str1 = str;
				if (str != null)
				{
					switch (str1)
					{
						case "0":
						{
							result = "CAVV not validated because erroneous data was submitted";
							break;
						}
						case "1":
						{
							result = "CAVV failed validation";
							break;
						}
						case "2":
						{
							result = "CAVV passed validation";
							break;
						}
						case "3":
						{
							result = "CAVV validation could not be performed; issuer attempt incomplete";
							break;
						}
						case "4":
						{
							result = "CAVV validation could not be performed; issuer system error";
							break;
						}
						case "5":
						{
							result = "Reserved for future use";
							break;
						}
						case "6":
						{
							result = "Reserved for future use";
							break;
						}
						case "7":
						{
							result = "CAVV attempt — failed validation — issuer available (U.S.-issued card/non-U.S acquirer)";
							break;
						}
						case "8":
						{
							result = "CAVV attempt — passed validation — issuer available (U.S.-issued card/non-U.S. acquirer)";
							break;
						}
						case "9":
						{
							result = "CAVV attempt — failed validation — issuer unavailable (U.S.-issued card/non-U.S. acquirer";
							break;
						}
						case "A":
						{
							result = "CAVV attempt — passed validation — issuer unavailable (U.S.-issued card/non-U.S. acquirer";
							break;
						}
						case "B":
						{
							result = "CAVV passed validation, information only, no liability shift";
							break;
						}
					}
				}
				return result;
			}
		}

		public string CCVResponse
		{
			get
			{
				return base.ParseResponse(38);
			}
		}

		public string City
		{
			get
			{
				return base.ParseResponse(17);
			}
		}

		public int Code
		{
			get
			{
				return base.ParseInt(0);
			}
		}

		public string Company
		{
			get
			{
				return base.ParseResponse(15);
			}
		}

		public string Country
		{
			get
			{
				return base.ParseResponse(20);
			}
		}

		public bool Declined
		{
			get
			{
				return this.Code == 2;
			}
		}

		public string Description
		{
			get
			{
				return base.ParseResponse(8);
			}
		}

		public string Email
		{
			get
			{
				return base.ParseResponse(23);
			}
		}

		public bool Error
		{
			get
			{
				return this.Code == 3;
			}
		}

		public string FirstName
		{
			get
			{
				return base.ParseResponse(13);
			}
		}

		public bool HeldForReview
		{
			get
			{
				return this.Code == 4;
			}
		}

		public string InvoiceNumber
		{
			get
			{
				return base.ParseResponse(7);
			}
		}

		public string LastName
		{
			get
			{
				return base.ParseResponse(14);
			}
		}

		public string MD5Hash
		{
			get
			{
				return base.ParseResponse(37);
			}
		}

		public string Message
		{
			get
			{
				return base.ParseResponse(3);
			}
		}

		public string Method
		{
			get
			{
				return base.ParseResponse(10);
			}
		}

		public string ResponseCode
		{
			get
			{
				return base.ParseResponse(0);
			}
		}

		public string ShipAddress
		{
			get
			{
				return base.ParseResponse(27);
			}
		}

		public string ShipCity
		{
			get
			{
				return base.ParseResponse(28);
			}
		}

		public string ShipCompany
		{
			get
			{
				return base.ParseResponse(26);
			}
		}

		public string ShipCountry
		{
			get
			{
				return base.ParseResponse(31);
			}
		}

		public string ShipFirstName
		{
			get
			{
				return base.ParseResponse(24);
			}
		}

		public string ShipLastName
		{
			get
			{
				return base.ParseResponse(25);
			}
		}

		public string ShipState
		{
			get
			{
				return base.ParseResponse(29);
			}
		}

		public string ShipZipCode
		{
			get
			{
				return base.ParseResponse(30);
			}
		}

		public string SplitTenderId
		{
			get
			{
				return base.ParseResponse(52);
			}
		}

		public string State
		{
			get
			{
				return base.ParseResponse(18);
			}
		}

		public int SubCode
		{
			get
			{
				return base.ParseInt(1);
			}
		}

		public decimal Tax
		{
			get
			{
				return base.ParseDecimal(32);
			}
		}

		public string TransactionID
		{
			get
			{
				return base.ParseResponse(6);
			}
		}

		public string TransactionType
		{
			get
			{
				return base.ParseResponse(11);
			}
		}

		public string ZipCode
		{
			get
			{
				return base.ParseResponse(19);
			}
		}

		public GatewayResponse(string[] rawResponse)
		{
			this.RawResponse = rawResponse;
			this.LoadKeys();
		}

		private void LoadKeys()
		{
			Dictionary<int, string> result = base.APIResponseKeys;
			result.Add(1, "Response Code");
			result.Add(2, "Response Subcode");
			result.Add(3, "Response Reason Code");
			result.Add(4, "Response Reason Text");
			result.Add(5, "Authorization Code");
			result.Add(6, "AVS Response");
			result.Add(7, "Transaction ID");
			result.Add(8, "Invoice Number");
			result.Add(9, "Description");
			result.Add(10, "Amount");
			result.Add(11, "Method");
			result.Add(12, "Transaction Type");
			result.Add(13, "Customer ID");
			result.Add(14, "First Name");
			result.Add(15, "Last Name");
			result.Add(16, "Company");
			result.Add(17, "Address");
			result.Add(18, "City");
			result.Add(19, "State");
			result.Add(20, "ZIP Code");
			result.Add(21, "Country");
			result.Add(22, "Phone");
			result.Add(23, "Fax");
			result.Add(24, "Email Address");
			result.Add(25, "Ship To First Name");
			result.Add(26, "Ship To Last Name");
			result.Add(27, "Ship To Company");
			result.Add(28, "Ship To Address");
			result.Add(29, "Ship To City");
			result.Add(30, "Ship To State");
			result.Add(31, "Ship To ZIP Code");
			result.Add(32, "Ship To Country");
			result.Add(33, "Tax");
			result.Add(34, "Duty");
			result.Add(35, "Freight");
			result.Add(36, "Tax Exempt");
			result.Add(37, "Purchase Order Number");
			result.Add(38, "MD5 Hash");
			result.Add(39, "Card Code Response");
			result.Add(40, "Cardholder Authentication Verification Response");
			result.Add(41, "Account Number");
			result.Add(42, "Card Type");
			result.Add(43, "Split Tender ID");
			result.Add(44, "Requested Amount");
			result.Add(45, "Balance On Card");
		}
	}
}