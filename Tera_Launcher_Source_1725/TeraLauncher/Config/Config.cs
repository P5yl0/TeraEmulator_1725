using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeraLauncher
{
    public static class Config
    {
        //Launcher Title
        public static String GetLauncherTitle(String defaultValue = "tera clauncher rev.1")
        {
            String _titleLauncher = defaultValue;
            try
            {
                _titleLauncher = IniReader.ReadValue("Launcher", "launcherTitle", LoginForm.configFile);
                if (_titleLauncher == "")
                {
                    _titleLauncher = defaultValue;
                    IniReader.WriteValue("Launcher", "launcherTitle", "tera clauncher rev.1", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _titleLauncher;
        }
        //Launcher Background
        public static String GetLauncherBackground(String defaultValue = "launcher-bg.png")
        {
            String _backgroundLauncher = defaultValue;
            try
            {
                _backgroundLauncher = IniReader.ReadValue("Skin", "launcherBackground", LoginForm.configFile);
                if (_backgroundLauncher == "")
                {
                    _backgroundLauncher = defaultValue;
                    IniReader.WriteValue("Skin", "launcherBackground", "launcher-bg.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _backgroundLauncher;
        }
        //Register Background
        public static String GetRegisterBackground(String defaultValue = "register-bg.png")
        {
            String _backgroundRegister = defaultValue;
            try
            {
                _backgroundRegister = IniReader.ReadValue("Skin", "registerBackground", LoginForm.configFile);
                if (_backgroundRegister == "")
                {
                    _backgroundRegister = defaultValue;
                    IniReader.WriteValue("Skin", "registerBackground", "register-bg.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _backgroundRegister;
        }
        //MinimizeButton-normal
        public static String GetMinimizeButtonNormal(String defaultValue = "btn-minimize-nm.png")
        {
            String _btn_minimize_nm = defaultValue;
            try
            {
                _btn_minimize_nm = IniReader.ReadValue("Skin", "btnMinimizeNm", LoginForm.configFile);
                if (_btn_minimize_nm == "")
                {
                    _btn_minimize_nm = defaultValue;
                    IniReader.WriteValue("Skin", "btnMinimizeNm", "btn-minimize-nm.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_minimize_nm;
        }
        //MinimizeButton-hover
        public static String GetMinimizeButtonHover(String defaultValue = "btn-minimize-hv.png")
        {
            String _btn_minimize_hv = defaultValue;
            try
            {
                _btn_minimize_hv = IniReader.ReadValue("Skin", "btnMinimizeHv", LoginForm.configFile);
                if (_btn_minimize_hv == "")
                {
                    _btn_minimize_hv = defaultValue;
                    IniReader.WriteValue("Skin", "btnMinimizeHv", "btn-minimize-hv.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_minimize_hv;
        }       
        //CloseButton-normal
        public static String GetCloseButtonNormal(String defaultValue = "btn-close-nm.png")
        {
            String _btn_close_nm = defaultValue;
            try
            {
                _btn_close_nm = IniReader.ReadValue("Skin", "btnCloseNm", LoginForm.configFile);
                if (_btn_close_nm == "")
                {
                    _btn_close_nm = defaultValue;
                    IniReader.WriteValue("Skin", "btnCloseNm", "btn-close-nm.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_close_nm;
        }
        //CloseButton-hover
        public static String GetCloseButtonHover(String defaultValue = "btn-close-hv.png")
        {
            String _btn_close_hv = defaultValue;
            try
            {
                _btn_close_hv = IniReader.ReadValue("Skin", "btnCloseHv", LoginForm.configFile);
                if (_btn_close_hv == "")
                {
                    _btn_close_hv = defaultValue;
                    IniReader.WriteValue("Skin", "btnCloseHv", "btn-close-hv.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_close_hv;
        }       
        //CreateButton-normal
        public static String GetCreateButtonNormal(String defaultValue = "btn-create-nm.png")
        {
            String _btn_create_nm = defaultValue;
            try
            {
                _btn_create_nm = IniReader.ReadValue("Skin", "btnCreateNm", LoginForm.configFile);
                if (_btn_create_nm == "")
                {
                    _btn_create_nm = defaultValue;
                    IniReader.WriteValue("Skin", "btnCreateNm", "btn-create-nm.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_create_nm;
        }
        //CreateButton-hover
        public static String GetCreateButtonHover(String defaultValue = "btn-create-hv.png")
        {
            String _btn_create_hv = defaultValue;
            try
            {
                _btn_create_hv = IniReader.ReadValue("Skin", "btnCreateHv", LoginForm.configFile);
                if (_btn_create_hv == "")
                {
                    _btn_create_hv = defaultValue;
                    IniReader.WriteValue("Skin", "btnCreateHv", "btn-create-hv.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_create_hv;
        }       
        //LoginButton-normal
        public static String GetLoginButtonNormal(String defaultValue = "btn-login-nm.png")
        {
            String _btn_login_nm = defaultValue;
            try
            {
                _btn_login_nm = IniReader.ReadValue("Skin", "btnLoginNm", LoginForm.configFile);
                if (_btn_login_nm == "")
                {
                    _btn_login_nm = defaultValue;
                    IniReader.WriteValue("Skin", "btnLoginNm", "btn-login-nm.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_login_nm;
        }
        //LoginButton-hover
        public static String GetLoginButtonHover(String defaultValue = "btn-login-hv.png")
        {
            String _btn_login_hv = defaultValue;
            try
            {
                _btn_login_hv = IniReader.ReadValue("Skin", "btnLoginHv", LoginForm.configFile);
                if (_btn_login_hv == "")
                {
                    _btn_login_hv = defaultValue;
                    IniReader.WriteValue("Skin", "btnLoginHv", "btn-login-hv.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_login_hv;
        }       
        //LogoutButton-normal
        public static String GetLogoutButtonNormal(String defaultValue = "btn-logout-nm.png")
        {
            String _btn_logout_nm = defaultValue;
            try
            {
                _btn_logout_nm = IniReader.ReadValue("Skin", "btnLogoutNm", LoginForm.configFile);
                if (_btn_logout_nm == "")
                {
                    _btn_logout_nm = defaultValue;
                    IniReader.WriteValue("Skin", "btnLogoutNm", "btn-logout-nm.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_logout_nm;
        }
        //LoginButton-hover
        public static String GetLogoutButtonHover(String defaultValue = "btn-logout-hv.png")
        {
            String _btn_logout_hv = defaultValue;
            try
            {
                _btn_logout_hv = IniReader.ReadValue("Skin", "btnLogoutHv", LoginForm.configFile);
                if (_btn_logout_hv == "")
                {
                    _btn_logout_hv = defaultValue;
                    IniReader.WriteValue("Skin", "btnLogoutHv", "btn-logout-hv.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_logout_hv;
        }       
        //PlayButton-normal
        public static String GetPlayButtonNormal(String defaultValue = "btn-play-nm.png")
        {
            String _btn_play_nm = defaultValue;
            try
            {
                _btn_play_nm = IniReader.ReadValue("Skin", "btnPlayNm", LoginForm.configFile);
                if (_btn_play_nm == "")
                {
                    _btn_play_nm = defaultValue;
                    IniReader.WriteValue("Skin", "btnPlayNm", "btn-play-nm.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_play_nm;
        }
        //PlayButton-hover
        public static String GetPlayButtonHover(String defaultValue = "btn-play-hv.png")
        {
            String _btn_play_hv = defaultValue;
            try
            {
                _btn_play_hv = IniReader.ReadValue("Skin", "btnPlayHv", LoginForm.configFile);
                if (_btn_play_hv == "")
                {
                    _btn_play_hv = defaultValue;
                    IniReader.WriteValue("Skin", "btnPlayHv", "btn-play-hv.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_play_hv;
        }       
        //RegisterButton-normal
        public static String GetRegisterButtonNormal(String defaultValue = "btn-register-nm.png")
        {
            String _btn_register_nm = defaultValue;
            try
            {
                _btn_register_nm = IniReader.ReadValue("Skin", "btnRegisterNm", LoginForm.configFile);
                if (_btn_register_nm == "")
                {
                    _btn_register_nm = defaultValue;
                    IniReader.WriteValue("Skin", "btnRegisterNm", "btn-register-nm.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_register_nm;
        }
        //RegisterButton-hover
        public static String GetRegisterButtonHover(String defaultValue = "btn-register-hv.png")
        {
            String _btn_register_hv = defaultValue;
            try
            {
                _btn_register_hv = IniReader.ReadValue("Skin", "btnRegisterHv", LoginForm.configFile);
                if (_btn_register_hv == "")
                {
                    _btn_register_hv = defaultValue;
                    IniReader.WriteValue("Skin", "btnRegisterHv", "btn-register-hv.png", LoginForm.configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_register_hv;
        }

        //LanguageGame
        public static String GetLanguageGame(String defaultValue = "en")
        {
            String _language_game = defaultValue;
            try
            {
                _language_game = IniReader.ReadValue("Launcher", "languageGame", LoginForm.configFile);
                if (_language_game == "")
                {
                    _language_game = defaultValue;
                    IniReader.WriteValue("Launcher", "languageGame", "en", LoginForm.configFile);
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

    }
}
