using Commons;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Configuration
{
    class Config
    {
        public static string _rootDir = Environment.CurrentDirectory + "\\";
        public static string _dataDir = _rootDir + "cTeraData\\";
        public static string _configFile = _rootDir + "cTlauncher.ini";

        //LoginServer Ip
        public static String GetServerIP(String defaultValue = "127.0.0.1")
        {
            String _server_ip = defaultValue;
            try
            {
                _server_ip = IniReader.ReadValue("Launcher", "serverIP", _configFile);
                if (_server_ip == "")
                {
                    _server_ip = defaultValue;
                    IniReader.WriteValue("Launcher", "serverIP", _server_ip, _configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _server_ip;
        }
        //LoginServer Ip
        public static String GetServerPort(String defaultValue = "2101")
        {
            String _server_port = defaultValue;
            try
            {
                _server_port = IniReader.ReadValue("Launcher", "serverPort", _configFile);
                if (_server_port == "")
                {
                    _server_port = defaultValue;
                    IniReader.WriteValue("Launcher", "serverPort", _server_port, _configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _server_port;
        }

        //LanguageGame String
        public static String GetLanguageGame(String defaultValue = "en")
        {
            String _language_game = defaultValue;
            try
            {
                _language_game = IniReader.ReadValue("Launcher", "languageGame", _configFile);
                if (_language_game == "")
                {
                    _language_game = defaultValue;
                    IniReader.WriteValue("Launcher", "languageGame", _language_game, _configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _language_game;
        }
        //LanguageGame Id
        public static String GetLanguageId(String defaultValue = "1")
        {
            String _language_id = defaultValue;
            try
            {
                if (GetLanguageGame() == "") { _language_id = defaultValue; }
                else if (GetLanguageGame() == "en") { _language_id = "1"; }
                else if (GetLanguageGame() == "fr") { _language_id = "2"; }
                else if (GetLanguageGame() == "de") { _language_id = "3"; }
                else if (GetLanguageGame() != "en" || GetLanguageGame() != "fr" || GetLanguageGame() != "de") { _language_id = "1"; }
            }
            catch (Exception /*ex*/)
            {
            }
            return _language_id;
        }
    }

    class Crypt
    {
        //Converts to MD5 Salt Has
        public static string StringToMD5Salt(string input, string salt)
        {
            byte[] data = Encoding.ASCII.GetBytes(salt + input);
            data = MD5.Create().ComputeHash(data);
            return Convert.ToBase64String(data);
        }
        //Converts to MD5 Hash
        public static string StringToMD5(string input)
        {
            //convert to MD5 Hash
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(input);
            byte[] result = md5.ComputeHash(textToHash);

            //MD5 to String convert
            StringBuilder s = new StringBuilder();
            foreach (byte b in result)
            {
                s.Append(b.ToString("x2").ToLower());
            }

            return s.ToString();
        }
        //Converts to Base64 Hash
        public static string StringToBase64(string input)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(input);
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
        //Converts from Base64 Hash
        public static string Base64ToString(string input)
        {
            byte[] bytes = Convert.FromBase64String(input);
            ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetString(bytes, 0, bytes.Length);
        }
        //Converts to SHA1 Hash
        public static string StringToSHA1(string input)
        {
            //convert to SHA1 Hash
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(input);
            byte[] result = sha1.ComputeHash(textToHash);

            //SHA1 Hash to String convert
            StringBuilder s = new StringBuilder();
            foreach (byte b in result)
            {
                s.AppendFormat("{0:x2}", b);
                //s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }

        public static bool IsLuck(byte chance)
        {
            if (chance >= 100)
                return true;

            if (chance <= 0)
                return false;

            return new Random().Next(0, 100) <= chance;
        }
    }

    class Converter
    {
        //Convert from ByteArray to Hex
        public static string ByteArrayToHexString(byte[] input)
        {
            char[] c = new char[input.Length * 2];
            byte b;
            for (int i = 0; i < input.Length; ++i)
            {
                b = ((byte)(input[i] >> 4));
                c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                b = ((byte)(input[i] & 0xF));
                c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            }

            return new string(c);
        }
        //Converts from Hex to ByteArray
        public static byte[] HexStringToByteArray(string input)
        {
            byte[] bytes = new byte[input.Length / 2];
            int bl = bytes.Length;
            for (int i = 0; i < bl; ++i)
            {
                bytes[i] = (byte)((input[2 * i] > 'F' ? input[2 * i] - 0x57 : input[2 * i] > '9' ? input[2 * i] - 0x37 : input[2 * i] - 0x30) << 4);
                bytes[i] |= (byte)(input[2 * i + 1] > 'F' ? input[2 * i + 1] - 0x57 : input[2 * i + 1] > '9' ? input[2 * i + 1] - 0x37 : input[2 * i + 1] - 0x30);
            }
            return bytes;
        }
        //Converts from Hex to Ascii
        public static string HexStringToAscii(string input)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= input.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(input.Substring(i, 2),
                    System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }
        //Converts from String to ByteArray
        public static byte[] StringToByteArray(string input)
        {
            try
            {
                ASCIIEncoding result = new ASCIIEncoding();
                return result.GetBytes(input);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Converts from ByteArray to Ascii
        public static string ByteArrayToAsciiString(byte[] input)
        {
            try
            {
                ASCIIEncoding result = new ASCIIEncoding();
                return result.GetString(input);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string ByteArrayToString(byte[] bytes)
        {
            return ByteArrayToString(bytes);
        }
        public static float Hex2Float(string str)
        {
            string s = str;
            return BitConverter.ToSingle(BitConverter.GetBytes(uint.Parse(s, NumberStyles.AllowHexSpecifier)), 0);
        }
        public static int HexToInt(string hex)
        {
            int intNumber = int.Parse(hex, NumberStyles.HexNumber);
            return intNumber;
        }
    }

    class Funcs
    {
        public static byte[] ExtractResource(String filename)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            foreach (string reso in a.GetManifestResourceNames())
            {
                Console.WriteLine(reso);
            }
            using (Stream resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream == null) return null;
                byte[] ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                return ba;
            }
        }
        public static bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                FileStream _FileStream = new FileStream(_FileName, FileMode.Create, FileAccess.ReadWrite);
                // Writes a block of bytes to this stream using data from a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);
                // close file stream
                _FileStream.Close();
                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }
            // error occured, return false
            return false;
        }
    }

}
