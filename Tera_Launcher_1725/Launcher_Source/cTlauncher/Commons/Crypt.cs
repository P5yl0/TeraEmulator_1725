using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class Crypt
    {
        // returns MD5 Hash from string
        public static string StringToMD5(string input)
        {
            //Umwandlung des Eingastring in den MD5 Hash
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(input);
            byte[] result = md5.ComputeHash(textToHash);

            //MD5 Hash in String konvertieren
            StringBuilder s = new StringBuilder();
            foreach (byte b in result)
            {
                s.Append(b.ToString("x2").ToLower());
            }

            return s.ToString();
        }

    }
}
