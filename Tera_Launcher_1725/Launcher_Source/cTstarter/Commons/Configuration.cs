using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class Configuration
    {
        public static string _rootDir = Environment.CurrentDirectory + "\\";
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
        
        //Server Url
        public static String GetServerUrl(String defaultValue = "127.0.0.1")
        {
            String _urlServer = defaultValue;
            try
            {
                _urlServer = IniReader.ReadValue("Launcher", "urlServer", _configFile);
                if (_urlServer == "")
                {
                    _urlServer = defaultValue;
                    IniReader.WriteValue("Launcher", "urlServer", _urlServer, _configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _urlServer;
        }
        //Server Port
        public static String GetServerPort(String defaultValue = "80")
        {
            String _portServer = defaultValue;
            try
            {
                _portServer = IniReader.ReadValue("Launcher", "portServer", _configFile);
                if (_portServer == "")
                {
                    _portServer = defaultValue;
                    IniReader.WriteValue("Launcher", "portServer", _portServer, _configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _portServer;
        }

        //Version Url
        public static String GetRemoteVersionUrl(String defaultValue = "http://127.0.0.1/launcher/cTlauncher.clv")
        {
            String _urlRemoteVersion = defaultValue;
            try
            {
                _urlRemoteVersion = IniReader.ReadValue("Launcher", "urlRemoteVersion", _configFile);
                if (_urlRemoteVersion == "")
                {
                    _urlRemoteVersion = defaultValue;
                    IniReader.WriteValue("Launcher", "urlRemoteVersion", _urlRemoteVersion, _configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _urlRemoteVersion;
        }

        //PatchDownload Url
        public static String GetPatchDownloadUrl(String defaultValue = "http://127.0.0.1/launcher/")
        {
            String _urlPatchDownload = defaultValue;
            try
            {
                _urlPatchDownload = IniReader.ReadValue("Launcher", "urlPatchDownload", _configFile);
                if (_urlPatchDownload == "")
                {
                    _urlPatchDownload = defaultValue;
                    IniReader.WriteValue("Launcher", "urlPatchDownload", _urlPatchDownload, _configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _urlPatchDownload;
        }
        //PatchDownload File
        public static String GetPatchDownloadFile(String defaultValue = "cTlauncher.zip")
        {
            String _filePatchDownload = defaultValue;
            try
            {
                _filePatchDownload = IniReader.ReadValue("Launcher", "filePatchDownload", _configFile);
                if (_filePatchDownload == "")
                {
                    _filePatchDownload = defaultValue;
                    IniReader.WriteValue("Launcher", "filePatchDownload", _filePatchDownload, _configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _filePatchDownload;
        }

    }
}
