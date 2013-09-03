using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
//
using System.Diagnostics;  //process run
using System.IO;  //file io
using System.Net; //http request
using System.Reflection; //bindings prop control

namespace TeraLauncher
{
    public partial class LoginForm : Form
    {
        #region Variables

        public static string currentDir = Environment.CurrentDirectory + "\\";
        public static string dataDir = currentDir + "cTeraData\\";
        public static string configFile = currentDir + "cTeraLauncher.ini";
        //
        public static string _title_txt { get; set; }
        public static string _launcher_background_img { get; set; }
        public static string _register_background_img { get; set; }
        public static string _btn_minimize_nm { get; set; }
        public static string _btn_minimize_hv { get; set; }
        public static string _btn_close_nm { get; set; }
        public static string _btn_close_hv { get; set; }
        public static string _btn_create_nm { get; set; }
        public static string _btn_create_hv { get; set; }
        public static string _btn_login_nm { get; set; }
        public static string _btn_login_hv { get; set; }
        public static string _btn_logout_nm { get; set; }
        public static string _btn_logout_hv { get; set; }
        public static string _btn_play_nm { get; set; }
        public static string _btn_play_hv { get; set; }
        public static string _btn_register_nm { get; set; }
        public static string _btn_register_hv { get; set; }
        //
        public static int master_account_id { get; set; }
        public static string master_account_name { get; set; }
        public static string master_account_password { get; set; }
        public static string master_account_email { get; set; }
        public double coins { get; set; }
        public static string languageGame { get; set; }
        public static int languageId { get; set; }
        public static bool isLoggedIn = false;
        //
        #endregion Variables

        #region Main
        //main entry
        public LoginForm()
        {
            InitializeComponent();

            //show serverlist
            BinaryReader br = new BinaryReader(File.OpenRead(currentDir + "tera-game.exe"));
            string selectHex = null;
            for (int i = 0x4E699C; i <= 0x4E69C9; i++)
            {
                br.BaseStream.Position = i;
                selectHex += br.ReadByte().ToString("X2");
            }
            br.Close();
            _serverlistInputBox.Text = Crypt.HexAsciiConvert(selectHex);

        }

        //main form load
        private void LoginForm_Load(object sender, EventArgs e)
        {
            //Configuration check on startup
            checkconfig();
        }
        #endregion Main

        #region Button Controls
        private void btnMinimize_Click(object sender, EventArgs e)
        { this.WindowState = FormWindowState.Minimized; }
        private void btnMinimize_Enter(object sender, EventArgs e)
        {
            _btnMinimize.Image = Image.FromFile(dataDir + _btn_minimize_hv); 
        }
        private void btnMinimize_Leave(object sender, EventArgs e)
        {
            _btnMinimize.Image = Image.FromFile(dataDir + _btn_minimize_nm); 
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnClose_Enter(object sender, EventArgs e)
        {
            _btnExit.Image = Image.FromFile(dataDir + _btn_close_hv); 
        }
        private void btnClose_Leave(object sender, EventArgs e)
        {
            _btnExit.Image = Image.FromFile(dataDir + _btn_close_nm); 
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            master_account_name = _usernameInputBox.Text;
            master_account_password = Crypt.StringToMD5(_passwordInputBox.Text);

            if (_usernameInputBox.Text.Equals("") || _passwordInputBox.Text.Equals(""))
            {
                MessageBox.Show("Empty email or password! Please try again.", "Error");
            }
            else
            if (master_account_name != "" && master_account_password != "")
                {
                    string webStringResponse = getWebStringResponse("http://127.0.0.1/auth.php", "POST", "username=" + master_account_name + "&password=" + master_account_password);

                    if (webStringResponse == "username=" + master_account_name + "&password=" + master_account_password + " ")
                    {
                        // We are logged in!
                        _notloggedPanel.Visible = false;
                        _loggedPanel.Visible = true;
                        //_btnPlay.Image = Image.FromFile(dataDir + _btn_play_hv); 
                        isLoggedIn = true;
                        // Welcome Change!
                        _welcomeLabel.Text = _welcomeLabel.Text + " " + master_account_name;
                        _coinsLabel.Text = _coinsLabel.Text + " : " + coins;
                    }
                    else
                    {
                        MessageBox.Show("User: " + master_account_name + " . Authentification failed!", "Error");
                    }
                }
        }
        private void btnLogin_Enter(object sender, EventArgs e)
        {
            _btnLogin.Image = Image.FromFile(dataDir + _btn_login_hv);
        }
        private void btnLogin_Leave(object sender, EventArgs e)
        {
            _btnLogin.Image = Image.FromFile(dataDir + _btn_login_nm);
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void btnLogout_Enter(object sender, EventArgs e)
        {
            _btnLogout.Image = Image.FromFile(dataDir + _btn_logout_hv);
        }
        private void btnLogout_Leave(object sender, EventArgs e)
        {
            _btnLogout.Image = Image.FromFile(dataDir + _btn_logout_nm);
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                startTera();
            }
            else
            if (!isLoggedIn)
            {
                //MessageBox.Show("Please Login first!", "Error");
            }

        }
        private void btnPlay_Enter(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                _btnPlay.Image = Image.FromFile(dataDir + _btn_play_hv);
            }
        }
        private void btnPlay_Leave(object sender, EventArgs e)
        {
            _btnPlay.Image = Image.FromFile(dataDir + _btn_play_nm);
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            RegisterForm rg = new RegisterForm();
            this.Hide();
            rg.Show();
        }
        private void btnCreate_Enter(object sender, EventArgs e)
        {
            _btnCreate.Image = Image.FromFile(dataDir + _btn_create_hv);
        }
        private void btnCreate_Leave(object sender, EventArgs e)
        {
            _btnCreate.Image = Image.FromFile(dataDir + _btn_create_nm);
        }

        //Language Selection ComboBox
        private void _languageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = _languageBox.Items[_languageBox.SelectedIndex].ToString();

            if (_languageBox.SelectedIndex == 0)
            {
                languageId = 1;
                languageGame = "en";
            }
            else if (_languageBox.SelectedIndex == 1)
            {
                languageId = 1;
                languageGame = "en";
            }
            else if (_languageBox.SelectedIndex == 2)
            {
                languageId = 2;
                languageGame = "fr";
            }
            else if (_languageBox.SelectedIndex == 3)
            {
                languageId = 3;
                languageGame = "de";
            }

            IniReader.WriteValue("Launcher", "languageGame", languageGame, configFile);
        }
        
        #endregion Button Controls

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
                    str = streamReader.ReadToEnd();
            }
            catch
            {
                str = "An error occured while attempting to identify.";
            }
            return str;
        }
        #endregion WebLoginCheck

        #region StartProcess
        //run tera.exe process
        private void startTera()
        {
            if (isLoggedIn)
            {                
                //params
                string LaunchString = " 1 " + master_account_password + " 1 0 " + master_account_name + " " + languageGame;

                Process.Start(new ProcessStartInfo()
                {
                    FileName = "tera-game.exe",
                    Arguments = LaunchString
                });

                //close launcher
                Thread.Sleep(100);
                Application.Exit();
            }
        }
        #endregion StartProcess

        #region StartupChecks
        //config check on start
        public void checkconfig()
        {
            if (!File.Exists(configFile))
            {
                //empty config file on start
                File.Create(configFile);
                Application.Restart();
            }
            else
            {
                //Skin Config
                _title_txt = Config.GetLauncherTitle();
                _launcher_background_img = Config.GetLauncherBackground();
                _register_background_img = Config.GetRegisterBackground();
                _btn_minimize_nm = Config.GetMinimizeButtonNormal();
                _btn_minimize_hv = Config.GetMinimizeButtonHover();
                _btn_close_nm = Config.GetCloseButtonNormal();
                _btn_close_hv = Config.GetCloseButtonHover();
                _btn_create_nm = Config.GetCreateButtonNormal();
                _btn_create_hv = Config.GetCreateButtonHover();
                _btn_login_nm = Config.GetLoginButtonNormal();
                _btn_login_hv = Config.GetLoginButtonHover();
                _btn_logout_nm = Config.GetLogoutButtonNormal();
                _btn_logout_hv = Config.GetLogoutButtonHover();
                _btn_play_nm = Config.GetPlayButtonNormal();
                _btn_play_hv = Config.GetPlayButtonHover();
                _btn_register_nm = Config.GetRegisterButtonNormal();
                _btn_register_hv = Config.GetRegisterButtonHover();

                //Language Config
                languageGame = Config.GetLanguageGame();
                languageId = Config.GetLanguageId();

                _languageBox.Items.AddRange(new object[4]
                {
                    "",
                    "English",
                    "French",
                    "German",
                });
                _languageBox.SelectedIndex = languageId;

                // Default Hide Login Panel
                _loggedPanel.Visible = false;
                _notloggedPanel.Visible = true;

                // Turn On Password mode!
                _passwordInputBox.PasswordChar = '*';
                _usernameInputBox.MaxLength = 32;
                _passwordInputBox.MaxLength = 32;

                // Set Graphics to Gui
                setimages();
            }
        }
        //images set on start
        public void setimages()
        {
            // Change launcher-background img if Needed!
            if (_launcher_background_img != null)
            { this.BackgroundImage = Image.FromFile(dataDir + _launcher_background_img); }
            // Change title text if Needed!
            if (_title_txt != null)
            { this._titleLabel.Text = _title_txt; }

            // Minimize Button, Style CSS
            _btnMinimize.Image = Image.FromFile(dataDir + _btn_minimize_nm);
            _btnMinimize.MouseEnter += new EventHandler(btnMinimize_Enter);
            _btnMinimize.MouseLeave += new EventHandler(btnMinimize_Leave);
            _btnMinimize.FlatStyle = FlatStyle.Flat;
            _btnMinimize.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            _btnMinimize.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnMinimize.FlatAppearance.BorderSize = 0;
            _btnMinimize.BackColor = Color.FromArgb(0, 255, 255, 255);
            // Close Button, Style CSS
            _btnExit.Image = Image.FromFile(dataDir + _btn_close_nm);
            _btnExit.MouseEnter += new EventHandler(btnClose_Enter);
            _btnExit.MouseLeave += new EventHandler(btnClose_Leave);
            _btnExit.FlatStyle = FlatStyle.Flat;
            _btnExit.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            _btnExit.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnExit.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnExit.FlatAppearance.BorderSize = 0;
            _btnExit.BackColor = Color.FromArgb(0, 255, 255, 255);
            // Login Button, Style CSS
            _btnLogin.Image = Image.FromFile(dataDir + _btn_login_nm);
            _btnLogin.MouseEnter += new EventHandler(btnLogin_Enter);
            _btnLogin.MouseLeave += new EventHandler(btnLogin_Leave);
            _btnLogin.FlatStyle = FlatStyle.Flat;
            _btnLogin.FlatAppearance.BorderSize = 0;
            _btnLogin.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            _btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnLogin.BackColor = Color.FromArgb(0, 255, 255, 255);
            // Create Button, Style CSS
            _btnCreate.Image = Image.FromFile(dataDir + _btn_create_nm);
            _btnCreate.MouseEnter += new EventHandler(btnCreate_Enter);
            _btnCreate.MouseLeave += new EventHandler(btnCreate_Leave);
            _btnCreate.FlatStyle = FlatStyle.Flat;
            _btnCreate.FlatAppearance.BorderSize = 0;
            _btnCreate.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            _btnCreate.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnCreate.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnCreate.BackColor = Color.FromArgb(0, 255, 255, 255);
            // Logout Button, Style CSS
            _btnLogout.Image = Image.FromFile(dataDir + _btn_logout_nm);
            _btnLogout.MouseEnter += new EventHandler(btnLogout_Enter);
            _btnLogout.MouseLeave += new EventHandler(btnLogout_Leave);
            _btnLogout.FlatStyle = FlatStyle.Flat;
            _btnLogout.FlatAppearance.BorderSize = 0;
            _btnLogout.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            _btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnLogout.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnLogout.BackColor = Color.FromArgb(0, 255, 255, 255);
            // Play Button, Style CSS
            _btnPlay.Image = Image.FromFile(dataDir + _btn_play_nm);
            _btnPlay.MouseEnter += new EventHandler(btnPlay_Enter);
            _btnPlay.MouseLeave += new EventHandler(btnPlay_Leave);
            _btnPlay.FlatStyle = FlatStyle.Flat;
            _btnPlay.FlatAppearance.BorderSize = 0;
            _btnPlay.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            _btnPlay.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnPlay.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnPlay.BackColor = Color.FromArgb(0, 255, 255, 255);
        }
        #endregion StartupChecks

        #region GUIoverride
        // grab & move form 
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
        #endregion GUIoverride
    }
}
