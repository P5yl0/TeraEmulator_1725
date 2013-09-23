using cLauncher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class Configuration
    {
        public static string _rootDir = Environment.CurrentDirectory +"\\";
        public static string _dataDir = _rootDir + "cTeraData\\";
        public static string _configFile = _rootDir + "cTlauncher.ini";

        //Launcher Version
        public static String GetLauncherVersion(String defaultValue = "1.0.0.0")
        {
            String _versionLauncher = defaultValue;
            try
            {
                _versionLauncher = IniReader.ReadValue("Launcher", "versionLauncher", _configFile);
                if (_versionLauncher == "")
                {
                    _versionLauncher = defaultValue;
                    IniReader.WriteValue("Launcher", "versionLauncher", _versionLauncher, _configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _versionLauncher;
        }

        //Launcher Title
        public static String GetLauncherTitle(String defaultValue = "Tera cTLauncher")
        {
            String _titleLauncher = defaultValue;
            try
            {
                _titleLauncher = IniReader.ReadValue("Launcher", "titleLauncher", _configFile);
                if (_titleLauncher == "")
                {
                    _titleLauncher = defaultValue;
                    IniReader.WriteValue("Launcher", "titleLauncher", _titleLauncher, _configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _titleLauncher;
        }

        //LanguageGame
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

        //LanguageGame
        public static int GetLanguageId(int defaultValue = 1)
        {
            int _language_id = defaultValue;
            try
            {
                if (GetLanguageGame() == "") { _language_id = defaultValue; }
                else if (GetLanguageGame() == "en") { _language_id = 1; }
                else if (GetLanguageGame() == "fr") { _language_id = 2; }
                else if (GetLanguageGame() == "de") { _language_id = 3; }
                else if (GetLanguageGame() != "en" || GetLanguageGame() != "fr" || GetLanguageGame() != "de") { _language_id = 1; }
            }
            catch (Exception /*ex*/)
            {
            }
            return _language_id;
        }

        //Launcher Website Url
        public static String GetLauncherWebsiteUrl(String defaultValue = "http://127.0.0.1/launcher/index.php")
        {
            String _urlLauncherWebsite = defaultValue;
            try
            {
                _urlLauncherWebsite = IniReader.ReadValue("Launcher", "urlLauncherWebsite", _configFile);
                if (_urlLauncherWebsite == "")
                {
                    _urlLauncherWebsite = defaultValue;
                    IniReader.WriteValue("Launcher", "urlLauncherWebsite", _urlLauncherWebsite, _configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _urlLauncherWebsite;
        }
        //Launcher Auth Url
        public static String GetLauncherAuthUrl(String defaultValue = "http://127.0.0.1/auth.php")
        {
            String _urlLauncherAuth = defaultValue;
            try
            {
                _urlLauncherAuth = IniReader.ReadValue("Launcher", "urlLauncherAuth", _configFile);
                if (_urlLauncherAuth == "")
                {
                    _urlLauncherAuth = defaultValue;
                    IniReader.WriteValue("Launcher", "urlLauncherAuth", _urlLauncherAuth, _configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _urlLauncherAuth;
        }


    }
}
