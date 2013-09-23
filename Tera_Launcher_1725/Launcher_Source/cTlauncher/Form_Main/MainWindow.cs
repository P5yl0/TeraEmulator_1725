using Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cLauncher
{
    public partial class MainWindow : Form
    {
        #region Properties

        Uri browserUrl = new Uri(Configuration.GetLauncherWebsiteUrl());

        public static string languageGame { get; set; }
        public static int languageId { get; set; }
        public static bool isLoggedIn = false;

        #endregion Properties

        #region Main
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            InitializeConfiguration();
        }

        private void InitializeConfiguration()
        {
            //set title
            _labelTitle.Text = "";
            //set buttons
            _buttonClose.Text = "";
            _buttonInfo.Text = "";
            _buttonLogin.Text = "";
            _buttonLogin.Show();
            _buttonPlay.Text = "";
            _buttonPlay.Hide();

            //load title text
            try { _labelTitle.Text = Configuration.GetLauncherTitle(); }
            catch { _labelTitle.Text = "Tera cTLauncher"; }
            //load exit button 
            try { _buttonClose.Image = Image.FromFile(Configuration._dataDir + InterfaceSkin.GetCloseButtonNormal()); }
            catch { _buttonClose.Text = "X"; }
            //load info button 
            try { _buttonInfo.Image = Image.FromFile(Configuration._dataDir + InterfaceSkin.GetInfoButtonNormal()); }
            catch { _buttonInfo.Text = "I"; }
            //load login button 
            try { _buttonLogin.Image = Image.FromFile(Configuration._dataDir + InterfaceSkin.GetLoginButtonNormal()); }
            catch { _buttonLogin.Text = "Login"; }
            //load play button 
            try { _buttonPlay.Image = Image.FromFile(Configuration._dataDir + InterfaceSkin.GetPlayButtonNormal()); }
            catch { _buttonPlay.Text = "Play"; }

            //load language
            languageGame = Configuration.GetLanguageGame();
            languageId = Configuration.GetLanguageId();

            _comboboxLanguage.Items.AddRange(new object[4]
                {
                    "",
                    "English",
                    "French",
                    "German",
                });
            _comboboxLanguage.SelectedIndex = languageId;

            //load browser
            try { _webbrowserNews.Url = browserUrl; }
            catch { }
        }
        #endregion Main

        #region Mouse Events
        //Close button Events
        private void _btnClose_Enter(object sender, EventArgs e)
        {
            try { _buttonClose.Image = Image.FromFile(Configuration._dataDir + InterfaceSkin.GetCloseButtonHover()); }
            catch { _buttonClose.Text = "x"; }
        }
        private void _btnClose_Leave(object sender, EventArgs e)
        {
            try { _buttonClose.Image = Image.FromFile(Configuration._dataDir + InterfaceSkin.GetCloseButtonNormal()); }
            catch { _buttonClose.Text = "X"; }
        }

        //Info button Events
        private void _btnInfo_Enter(object sender, EventArgs e)
        {
            try { _buttonInfo.Image = Image.FromFile(Configuration._dataDir + InterfaceSkin.GetInfoButtonHover()); }
            catch { _buttonInfo.Text = "i"; }
        }
        private void _btnInfo_Leave(object sender, EventArgs e)
        {
            try { _buttonInfo.Image = Image.FromFile(Configuration._dataDir + InterfaceSkin.GetInfoButtonNormal()); }
            catch { _buttonInfo.Text = "I"; }
        }

        //Login button Events
        private void _btnLogin_Enter(object sender, EventArgs e)
        {
            try { _buttonLogin.Image = Image.FromFile(Configuration._dataDir + InterfaceSkin.GetLoginButtonHover()); }
            catch { _buttonLogin.Text = "login"; }
        }
        private void _btnLogin_Leave(object sender, EventArgs e)
        {
            try { _buttonLogin.Image = Image.FromFile(Configuration._dataDir + InterfaceSkin.GetLoginButtonNormal()); }
            catch { _buttonLogin.Text = "Login"; }
        }
        
        //Play button Events
        private void _btnPlay_Enter(object sender, EventArgs e)
        {
            try { _buttonPlay.Image = Image.FromFile(Configuration._dataDir + InterfaceSkin.GetPlayButtonHover()); }
            catch { _buttonPlay.Text = "play"; }
        }
        private void _btnPlay_Leave(object sender, EventArgs e)
        {
            try { _buttonPlay.Image = Image.FromFile(Configuration._dataDir + InterfaceSkin.GetPlayButtonNormal()); }
            catch { _buttonPlay.Text = "Play"; }
        }

        #endregion Mouse Events

        #region Button Click Events
        //Login Button Click
        private void _btnLogin_Click(object sender, EventArgs e)
        {
            string webStringResponse = getWebStringResponse(Configuration.GetLauncherAuthUrl(), "POST", "username=" + _inputboxUsername.Text + "&password=" + _inputboxPassword.Text);

            if (webStringResponse == "1")
            {
                //MessageBox.Show("login success","login");
                isLoggedIn = true;
                _labelWelcome.Text = "Welcome, " + _inputboxUsername.Text;

                if (isLoggedIn)
                {
                    _buttonLogin.Hide();
                    _buttonPlay.Show();
                }

            }
            if (webStringResponse == "0")
            {
                MessageBox.Show("login failed", "login");
            }
        }
        //Play Button Click
        private void _btnPlay_Click(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                startTera();
            }
            else
                if (!isLoggedIn)
                {
                    MessageBox.Show("Please Login first!", "Error");
                }
        }
        //Close Button Click
        private void _btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Info Button Click
        private void _btnInfo_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
            about.Update();
            about.StartPosition = FormStartPosition.CenterScreen;
            int deskHeight = Screen.PrimaryScreen.Bounds.Height;
            int deskWidth = Screen.PrimaryScreen.Bounds.Width;
            about.Location = new Point(deskWidth / 2 - about.Width / 2, deskHeight / 2 - about.Height / 2);
            Functions.pause(5000);
            about.Hide();
        }

        //Language Selection ComboBox
        private void _languageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = _comboboxLanguage.Items[_comboboxLanguage.SelectedIndex].ToString();

            if (_comboboxLanguage.SelectedIndex == 0)
            {
                languageId = 1;
                languageGame = "en";
            }
            else if (_comboboxLanguage.SelectedIndex == 1)
            {
                languageId = 1;
                languageGame = "en";
            }
            else if (_comboboxLanguage.SelectedIndex == 2)
            {
                languageId = 2;
                languageGame = "fr";
            }
            else if (_comboboxLanguage.SelectedIndex == 3)
            {
                languageId = 3;
                languageGame = "de";
            }

            IniReader.WriteValue("Launcher", "languageGame", languageGame, Configuration._configFile);
        }

        #endregion Button Click Events

        #region WebLoginCheck
        //Identify Login Web
        public string getWebStringResponse(string _Url, string _Method, string _Post)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(_Url);
            httpWebRequest.Method = _Method;
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/13.0.782.220 Safari/535.1";
            httpWebRequest.CookieContainer = new CookieContainer();
            httpWebRequest.Timeout = 30000;
            httpWebRequest.KeepAlive = true;
            string str = (string)null;
            try
            {
                if (_Method.Equals("POST"))
                {
                    httpWebRequest.AllowAutoRedirect = false;
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    byte[] bytes = Encoding.UTF8.GetBytes(_Post);
                    httpWebRequest.ContentLength = (long)bytes.Length;
                    ((WebRequest)httpWebRequest).GetRequestStream().Write(bytes, 0, bytes.Length);
                }
                using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream()))
                {
                    str = streamReader.ReadToEnd();
                }
            }
            catch
            {
                str = "An error occured while attempting to identify.";
            }
            return str;
        }
        #endregion WebLoginCheck

        #region GuiOverrides
        //Grab&Move Form 
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTCAPTION = 0x02;

            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTCAPTION;
            }
            else
            {
                base.WndProc(ref m);
            }
        }
        #endregion GuiOverrides

        #region StartProcess
        //run tera game process
        private void startTera()
        {
            if (isLoggedIn)
            {
                try
                {
                    //params
                    string LaunchString = " 1 " + Crypt.StringToMD5(_inputboxPassword.Text) + " 1 0 " + _inputboxUsername.Text + " " + languageGame;
                    _inputboxServerlist.Text = LaunchString;

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = Configuration._rootDir + "cTgame.clf";
                    startInfo.Arguments = LaunchString;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.RedirectStandardInput = true;

                    Process.Start(startInfo);
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: Could not start the Game\n" + ex.Message, "ERROR: Game Start Failed!");
                    Application.Exit();
                }
            }


        }

        #endregion StartProcess

    }

}
