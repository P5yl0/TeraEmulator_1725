using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Security.Cryptography;

namespace TeraLauncher
{
    public static class Crypt
    {
        // returns MD5 Salt Hash from string
        public static string StringToMD5Salt(string value, string salt)
        {
            byte[] data = Encoding.ASCII.GetBytes(salt + value);
            data = MD5.Create().ComputeHash(data);
            return Convert.ToBase64String(data);
        }
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
        // returns SHA1 Hash from string
        public static string StringToSHA1(string input)
        {
            //Umwandlung des Eingastring in den SHA1 Hash
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(input);
            byte[] result = sha1.ComputeHash(textToHash);

            //SHA1 Hash in String konvertieren
            StringBuilder s = new StringBuilder();
            foreach (byte b in result)
            {
                s.AppendFormat("{0:x2}", b);
                //s.Append(b.ToString("x2").ToLower());
            }

            return s.ToString();
        }
        // returns Base64 Hash from string
        public static string StringToBase64(string input)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(input);
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
        // returns String from Base64 Hash 
        public static string Base64ToString(string input)
        {

            byte[] bytes = Convert.FromBase64String(input);
            ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetString(bytes, 0, bytes.Length);
        }

        // convert Hex to Ascii
        public static string HexAsciiConvert(string hex)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= hex.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(i, 2),
                    System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }

    }
}
