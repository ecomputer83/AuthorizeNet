using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AuthorizeNet
{
	public class Crypto
	{
		public Crypto()
		{
		}

		/// <summary>
		/// Encrypts the key/value pair supplied using HMAC-MD5
		/// </summary>
		public static string EncryptHMAC(string key, string value)
		{
			byte[] HMACkey = (new ASCIIEncoding()).GetBytes(key);
			byte[] HMACdata = (new ASCIIEncoding()).GetBytes(value);
			byte[] HMAChash = (new HMACMD5(HMACkey)).ComputeHash(HMACdata);
			string fingerprint = "";
			for (int i = 0; i < (int)HMAChash.Length; i++)
			{
				fingerprint = string.Concat(fingerprint, HMAChash[i].ToString("x").PadLeft(2, '0'));
			}
			return fingerprint;
		}

		/// <summary>
		/// Generates the HMAC-encrypted hash to send along with the SIM form
		/// </summary>
		/// <param name="transactionKey">The merchant's transaction key</param>
		/// <param name="login">The merchant's Authorize.NET API Login</param>
		/// <param name="amount">The amount of the transaction</param>
		/// <returns></returns>
		public static string GenerateFingerprint(string transactionKey, string login, decimal amount, string sequence, string timeStamp)
		{
			object[] objArray = new object[] { login, sequence, timeStamp.ToString(), amount.ToString() };
			return Crypto.EncryptHMAC(transactionKey, string.Format("{0}^{1}^{2}^{3}^", objArray));
		}

		/// <summary>
		/// Generates a 4-place sequence number randomly
		/// </summary>
		/// <returns></returns>
		public static string GenerateSequence()
		{
			return (new Random()).Next(0, 1000).ToString();
		}

		/// <summary>
		/// Generates a timestamp in seconds from 1970
		/// </summary>
		public static int GenerateTimestamp()
		{
			TimeSpan utcNow = DateTime.UtcNow - new DateTime(1970, 1, 1);
			return (int)utcNow.TotalSeconds;
		}

		/// <summary>
		/// Decrypts provided string parameter
		/// </summary>
		public static bool IsMatch(string key, string apiLogin, string transactionID, decimal amount, string expected)
		{
			object[] objArray = new object[] { key, apiLogin, transactionID, amount.ToString() };
			string unencrypted = string.Format("{0}{1}{2}{3}", objArray);
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			string hashed = Regex.Replace(BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(unencrypted))), "-", "");
			return hashed.Equals(expected);
		}
	}
}