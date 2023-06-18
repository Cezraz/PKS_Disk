using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Trigada_Project_WPF
{
    class MyEncryption
    {
        public static string Encrypt(string data, string key)
        {
            using (var des = new TripleDESCryptoServiceProvider { Mode = CipherMode.ECB, Key = UTF8Encoding.UTF8.GetBytes(key), Padding = PaddingMode.PKCS7 })
            using (var desEncrypt = des.CreateEncryptor())
            {
                var buffer = Encoding.UTF8.GetBytes(data);

                return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
        }

        public static string Decrypt(string data, string key)
        {
            using (var des = new TripleDESCryptoServiceProvider { Mode = CipherMode.ECB, Key = UTF8Encoding.UTF8.GetBytes(key), Padding = PaddingMode.PKCS7 })
            using (var desEncrypt = des.CreateDecryptor())
            {
                var buffer = Convert.FromBase64String(data.Replace(" ", "+"));

                return Encoding.UTF8.GetString(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
        }

        public static string GenerateKey()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabsdefghijklmnopqrstuvwxyz!$%&";
            string key = "";
            var r = new Random();
            for (int i = 0; i < 16; i++)
                key += chars[r.Next(0, chars.Length - 1)];
            return key;
        }
    }
}
