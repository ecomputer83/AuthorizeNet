using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizeNet
{
	public abstract class ResponseBase
	{
		public string[] RawResponse;

		internal Dictionary<int, string> APIResponseKeys
		{
			get
			{
				return new Dictionary<int, string>();
			}
		}

		protected ResponseBase()
		{
		}

		public int FindByValue(string val)
		{
			int result = 0;
			int i = 0;
			while (i < (int)this.RawResponse.Length)
			{
				if (this.RawResponse[i].ToString() != val)
				{
					i++;
				}
				else
				{
					result = i;
					break;
				}
			}
			return result;
		}

		internal decimal ParseDecimal(int index)
		{
			decimal result = new decimal(0);
			if ((int)this.RawResponse.Length > index)
			{
				decimal.TryParse(this.RawResponse[index].ToString(), out result);
			}
			return result;
		}

		internal int ParseInt(int index)
		{
			int result = 0;
			if ((int)this.RawResponse.Length > index)
			{
				int.TryParse(this.RawResponse[index].ToString(), out result);
			}
			return result;
		}

		internal string ParseResponse(int index)
		{
			string result = "";
			if ((int)this.RawResponse.Length > index)
			{
				result = this.RawResponse[index].ToString();
			}
			return result;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			int index = 0;
			foreach (int key in this.APIResponseKeys.Keys)
			{
				sb.AppendFormat("{0} = {1}\n", this.APIResponseKeys[key], this.ParseResponse(index));
				index++;
			}
			return sb.ToString();
		}
	}
}