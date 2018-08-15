using System;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace VatLid
{
	/// <summary>
	/// Summary description for SaltedHash.
	/// </summary>
	public class SaltedHash
	{

		static public bool ValidatePassword (string password, string saltedHash) 
		{
			// Extract hash and salt string
			string saltString = saltedHash.Substring (saltedHash.Length - 24);
			string hash1 = saltedHash.Substring (0, saltedHash.Length - 24);

			// Append the salt string to the password
			string saltedPassword = password + saltString;

			// Hash the salted password
			string hash2 = FormsAuthentication.HashPasswordForStoringInConfigFile
				(saltedPassword, "SHA1");

			// Compare the hashes
			return (hash1.CompareTo (hash2) == 0);
		}

		static public string CreateSaltedPasswordHash (string password) 
		{
			// Generate random salt string
			RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider ();
			byte[] saltBytes = new byte[16];
			csp.GetNonZeroBytes (saltBytes);
			string saltString = Convert.ToBase64String (saltBytes);

			// Append the salt string to the password
			string saltedPassword = password + saltString;

			// Hash the salted password
			string hash = FormsAuthentication.HashPasswordForStoringInConfigFile
				(saltedPassword, "SHA1");

			// Append the salt to the hash
			string saltedHash = hash + saltString;
			return saltedHash;
		}
		static public string EncodeMD5(string s)
		{
			MD5CryptoServiceProvider md5Hasher =new MD5CryptoServiceProvider();
			byte[] hashedDataBytes;
			UTF8Encoding encoder=new UTF8Encoding();
			hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(s));
			StringBuilder sb = new StringBuilder();
			foreach(byte b in hashedDataBytes)
			{
				sb.Append(b.ToString("x2").ToLower());
			}
			return sb.ToString();
		}
		private static byte[] Unicode2Bytes(String strUnicode)
		{
			Encoding unicode = Encoding.UTF8;
			return unicode.GetBytes(strUnicode);
		}
		private static String Bytes2Unicode(byte[] unicodeBytes)
		{
			Decoder utf8Decoder = Encoding.UTF8.GetDecoder();
			int charCount = utf8Decoder.GetCharCount(unicodeBytes, 0, unicodeBytes.Length);
			Char[] chars = new Char[charCount];
			int charsDecodedCount = utf8Decoder.GetChars(unicodeBytes, 0, unicodeBytes.Length, chars, 0);
			String strUnicode = new String(chars);
			return strUnicode;
		}
		public static string From64StringToUnicode(string s)
		{
			byte[] t=Convert.FromBase64String(s);
			return Bytes2Unicode(t);
		}
		public static string UnicodeTo64String(string s)
		{
			byte[] t=Unicode2Bytes(s);
			return Convert.ToBase64String(t);
		}

        //GGTD
        public static string Encode(string password)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            Byte[] hashBytes = encoding.GetBytes(password);
            // Compute the SHA-1 hash
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            Byte[] cryptPassword = sha1.ComputeHash(hashBytes);
            return BitConverter.ToString(cryptPassword);
        }

	}
}
