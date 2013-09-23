using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class InterfaceSkin
    {
        //Splash Image File
        public static String GetSplashImage(String defaultValue = "Splash.png")
        {
            String _splashImage = defaultValue;
            try
            {
                _splashImage = IniReader.ReadValue("Skin", "splashImage", Configuration._configFile);
                if (_splashImage == "")
                {
                    _splashImage = defaultValue;
                    IniReader.WriteValue("Skin", "splashImage", _splashImage, Configuration._configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _splashImage;
        }

        //CloseButton-normal
        public static String GetCloseButtonNormal(String defaultValue = "btn-close-nm.png")
        {
            String _btn_close_nm = defaultValue;
            try
            {
                _btn_close_nm = IniReader.ReadValue("Skin", "btnCloseNm", Configuration._configFile);
                if (_btn_close_nm == "")
                {
                    _btn_close_nm = defaultValue;
                    IniReader.WriteValue("Skin", "btnCloseNm", _btn_close_nm, Configuration._configFile);
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
                _btn_close_hv = IniReader.ReadValue("Skin", "btnCloseHv", Configuration._configFile);
                if (_btn_close_hv == "")
                {
                    _btn_close_hv = defaultValue;
                    IniReader.WriteValue("Skin", "btnCloseHv", _btn_close_hv, Configuration._configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_close_hv;
        }

        //InfoButton-normal
        public static String GetInfoButtonNormal(String defaultValue = "btn-info-nm.png")
        {
            String _btn_info_nm = defaultValue;
            try
            {
                _btn_info_nm = IniReader.ReadValue("Skin", "btnInfoNm", Configuration._configFile);
                if (_btn_info_nm == "")
                {
                    _btn_info_nm = defaultValue;
                    IniReader.WriteValue("Skin", "btnInfoNm", _btn_info_nm, Configuration._configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_info_nm;
        }
        //InfoButton-hover
        public static String GetInfoButtonHover(String defaultValue = "btn-info-hv.png")
        {
            String _btn_info_hv = defaultValue;
            try
            {
                _btn_info_hv = IniReader.ReadValue("Skin", "btnInfoHv", Configuration._configFile);
                if (_btn_info_hv == "")
                {
                    _btn_info_hv = defaultValue;
                    IniReader.WriteValue("Skin", "btnInfoHv", _btn_info_hv, Configuration._configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_info_hv;
        }

        //LoginButton-normal
        public static String GetLoginButtonNormal(String defaultValue = "btn-login-nm.png")
        {
            String _btn_login_nm = defaultValue;
            try
            {
                _btn_login_nm = IniReader.ReadValue("Skin", "btnLoginNm", Configuration._configFile);
                if (_btn_login_nm == "")
                {
                    _btn_login_nm = defaultValue;
                    IniReader.WriteValue("Skin", "btnLoginNm", _btn_login_nm, Configuration._configFile);
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
                _btn_login_hv = IniReader.ReadValue("Skin", "btnLoginHv", Configuration._configFile);
                if (_btn_login_hv == "")
                {
                    _btn_login_hv = defaultValue;
                    IniReader.WriteValue("Skin", "btnLoginHv", _btn_login_hv, Configuration._configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_login_hv;
        }

        //PlayButton-normal
        public static String GetPlayButtonNormal(String defaultValue = "btn-play-nm.png")
        {
            String _btn_play_nm = defaultValue;
            try
            {
                _btn_play_nm = IniReader.ReadValue("Skin", "btnPlayNm", Configuration._configFile);
                if (_btn_play_nm == "")
                {
                    _btn_play_nm = defaultValue;
                    IniReader.WriteValue("Skin", "btnPlayNm", _btn_play_nm, Configuration._configFile);
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
                _btn_play_hv = IniReader.ReadValue("Skin", "btnPlayHv", Configuration._configFile);
                if (_btn_play_hv == "")
                {
                    _btn_play_hv = defaultValue;
                    IniReader.WriteValue("Skin", "btnPlayHv", _btn_play_hv, Configuration._configFile);
                }
            }
            catch (Exception /*ex*/)
            {
            }
            return _btn_play_hv;
        }

    }
}
