using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TrueMainForm.Models
{
    public class MyEcoding
    {
        private static byte[] key = { 60, 193, 108, 168, 202, 110, 29, 20, 202, 228, 187, 190, 27, 241, 75, 68, 81, 119, 125, 85, 190, 246, 40, 176, 145, 54, 27, 174, 67, 173, 46, 45 };
        private static byte[] iv = { 132, 193, 67, 244, 8, 174, 175, 121, 190, 248, 26, 32, 149, 151, 33, 89 };


        public static void Encrypt(string InputFile, string OutputFile)
        {
            using (var rijndael = new RijndaelManaged())
            {
                rijndael.IV = iv;
                rijndael.Key = key;
                using (var inputStream=File.OpenRead(InputFile))
                using (var outputStream= new FileStream(OutputFile,FileMode.Create,FileAccess.Write))
                using (var encStream= new CryptoStream (outputStream,rijndael.CreateEncryptor(),CryptoStreamMode.Write))
                {
                    outputStream.SetLength(0);
                    inputStream.CopyTo(encStream);
                }
            }
           
        }

        public static void Decrypt(string InputFile, string OutputFile)
        {
            using (var rijndael = new RijndaelManaged())
            {
                rijndael.IV = iv;
                rijndael.Key = key;
                using (var inputStream = File.OpenRead(InputFile))
                using (var decStream = new CryptoStream(inputStream, rijndael.CreateDecryptor(), CryptoStreamMode.Read))
                using (var outputStream = new FileStream(OutputFile, FileMode.Create, FileAccess.Write))
                {
                    
                    decStream.CopyTo(outputStream);
                }
            }
        }

       
    }
}
