using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace VatLid
{
	/// <summary>
	/// Provides method to retrieve connection string from web.config
	/// and decyrpt the string using DataProtector class. 
	/// </summary>
	public class SecureConnection
	{
        //ma hoa
        static public byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();

                    }
                }
            }
            // Return the encrypted bytes from the memory stream. 
            return encrypted;
        }
        //giai ma
        static public string DecryptStringFromBytes(String strcipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            String[] tempAry = strcipherText.Split('-');
            byte[] cipherText = new byte[tempAry.Length];
            for (int i = 0; i < tempAry.Length; i++)
                cipherText[i] = Convert.ToByte(tempAry[i], 16);
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        static public string GetCnxStringDecode(string configKey)
        {
            string strCnx;
            string strEncryptedCnx = ConfigurationSettings.AppSettings[configKey];
            string key = ConfigurationSettings.AppSettings["KeyEncrypt"];

            MD5 md5Hash = MD5.Create();
            byte[] Key1 = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(key));
            byte[] IV = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(key + key));

            strCnx = DecryptStringFromBytes(strEncryptedCnx, Key1, IV);

            return strCnx;
        }
		static public string GetCnxString(string configKey)
		{
			string strCnx;
			string strEncryptedCnx = ConfigurationSettings.AppSettings[configKey];
			strCnx=strEncryptedCnx;
//			try
//			{
//				DataProtector dp = new DataProtector(DataProtector.Store.USE_MACHINE_STORE);				
//				byte[] dataToDecrypt = Convert.FromBase64String(strEncryptedCnx);
//				strCnx = Encoding.UTF8.GetString(dp.Decrypt(dataToDecrypt,null));
//			}
//			catch
//			{
//				strCnx=strEncryptedCnx.Substring(0,9) + strEncryptedCnx.Substring(14,strEncryptedCnx.Length-14) ;
//				//"Password=Kaze_sa123;User ID=sa;Initial Catalog=leadership;Data Source=10.4.4.4";
//
//			}
			return strCnx;
		}
	}
}
