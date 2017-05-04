using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace AuthorizeNet
{
	public class SIMResponse : IGatewayResponse
	{
		private NameValueCollection _post;

		private string _merchantHash;

		public decimal Amount
		{
			get
			{
				string sAmount = this.FindKey("x_amount");
				decimal result = new decimal(0, 0, 0, false, 2);
				decimal.TryParse(sAmount, out result);
				return result;
			}
		}

		public bool Approved
		{
			get
			{
				return this.ResponseCode == "1";
			}
		}

		public string AuthorizationCode
		{
			get
			{
				return this.FindKey("x_auth_code");
			}
		}

		public string CardNumber
		{
			get
			{
				return this.FindKey("x_card_num");
			}
		}

		public string InvoiceNumber
		{
			get
			{
				return this.FindKey("x_invoice_num");
			}
		}

		public string MD5Hash
		{
			get
			{
				return this.FindKey("x_MD5_Hash");
			}
		}

		public string Message
		{
			get
			{
				return this.FindKey("x_response_reason_text");
			}
		}

		public string ResponseCode
		{
			get
			{
				return this.FindKey("x_response_code");
			}
		}

		public string TransactionID
		{
			get
			{
				return this.FindKey("x_trans_id");
			}
		}

		public SIMResponse(NameValueCollection post)
		{
			this._post = post;
		}

		public SIMResponse() : this(HttpContext.Current.Request.Form)
		{
		}

		private string FindKey(string key)
		{
			string result = null;
			if (this._post[key] != null)
			{
				result = this._post[key];
			}
			return result;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("<li>Code = {0}", this.ResponseCode);
			sb.AppendFormat("<li>Auth = {0}", this.AuthorizationCode);
			sb.AppendFormat("<li>Message = {0}", this.Message);
			sb.AppendFormat("<li>TransID = {0}", this.TransactionID);
			sb.AppendFormat("<li>Approved = {0}", this.Approved);
			return sb.ToString();
		}

		/// <summary>
		/// Validates that what was passed by Auth.net is valid
		/// </summary>
		public bool Validate(string merchantHash, string apiLogin)
		{
			return Crypto.IsMatch(merchantHash, apiLogin, this.TransactionID, this.Amount, this.MD5Hash);
		}
	}
}