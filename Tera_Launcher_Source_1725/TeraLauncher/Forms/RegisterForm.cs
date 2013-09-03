using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeraLauncher
{
    public partial class RegisterForm : Form
    {
        #region Variables
        public static string master_account_name { get; set; }
        public static string master_account_password { get; set; }
        public static string master_account_repassword { get; set; }
        public static string master_account_email { get; set; }
        public static string url = "http://127.0.0.1/register.php";
        public static string Response = null;
        
        #endregion Variables

        #region Main
        public RegisterForm()
        {
            InitializeComponent();
        }
        
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            //Configuration check on startup
            checkconfig();
        }

        #endregion Main

        #region Button Controls
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnMinimize_Enter(object sender, EventArgs e)
        {
            _btnMinimize.Image = Image.FromFile(LoginForm.dataDir + LoginForm._btn_minimize_hv); 
        }
        private void btnMinimize_Leave(object sender, EventArgs e)
        {
            _btnMinimize.Image = Image.FromFile(LoginForm.dataDir + LoginForm._btn_minimize_nm); 
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            LoginForm frm = new LoginForm();
            frm.Show();
            this.Hide();
        }
        private void btnClose_Enter(object sender, EventArgs e)
        {
            _btnClose.Image = Image.FromFile(LoginForm.dataDir + LoginForm._btn_close_hv); 
        }
        private void btnClose_Leave(object sender, EventArgs e)
        {
            _btnClose.Image = Image.FromFile(LoginForm.dataDir + LoginForm._btn_close_nm); 
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register();
        }
        private void btnRegister_Enter(object sender, EventArgs e)
        {
            _btnRegister.Image = Image.FromFile(LoginForm.dataDir + LoginForm._btn_register_hv);
        }
        private void btnRegister_Leave(object sender, EventArgs e)
        {
            _btnRegister.Image = Image.FromFile(LoginForm.dataDir + LoginForm._btn_register_nm);
        }
        
        #endregion Button Controls

        #region RegisterFunctions
        public void Register()
        {
            WebClient client = new WebClient();
            try
            {
                string response = client.DownloadString(url);
            }
            catch (Exception /*ex*/)
            {
                MessageBox.Show("Cant connect to RegServer", "Error");
                return;
            }

            Request();

            if (Response.Contains("In use"))
            {
                MessageBox.Show("This E-mail or Username is already in use!", "Error");
            }
            if (Response.Contains("registered succesfully!"))
            {
                MessageBox.Show("Registered Succesfully! You may now login!", "Info");

                LoginForm frm = new LoginForm();
                frm.Show();
                this.Hide();

            }
            if (Response.Contains("register failed!"))
            {
                MessageBox.Show("Register failed!", "Error");
            }
        }
        public void Request()
        {
            master_account_name = _usernameInputBox.Text;
            master_account_email = _emailInputBox.Text;
            master_account_password = Crypt.StringToMD5(_passwordInputBox.Text);
            master_account_repassword = Crypt.StringToMD5(_passwordReInputBox.Text);

            if (_usernameInputBox.Text.Equals("") || _passwordInputBox.Text.Equals("") || _passwordReInputBox.Text.Equals("") || _emailInputBox.Text.Equals(""))
            {
                MessageBox.Show("Empty email or password! Please try again.", "Error");
                Response = "register failed!";
                return;
            }
            else
                if (master_account_name != "" && master_account_password != "" && master_account_repassword != "")
                {
                    if (master_account_password != master_account_repassword)
                    {
                        MessageBox.Show("password & re-password not the same! Please try again.", "Error");
                        Response = "register failed!";
                        return; 
                    }
                    else
                    {
                        if (IsValidEmail(master_account_email))
                        {
                            // Create a request using a URL that can receive a post. 
                            WebRequest request = WebRequest.Create(url);
                            // Set the Method property of the request to POST.
                            request.Method = "POST";
                            // Create POST data and convert it to a byte array.
                            string postData = "username=" + master_account_name + "&password=" + master_account_password + "&email=" + master_account_email;
                            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                            // Set the ContentType property of the WebRequest.
                            request.ContentType = "application/x-www-form-urlencoded";
                            // Set the ContentLength property of the WebRequest.
                            request.ContentLength = byteArray.Length;
                            // Get the request stream.
                            Stream dataStream = request.GetRequestStream();
                            // Write the data to the request stream.
                            dataStream.Write(byteArray, 0, byteArray.Length);
                            // Close the Stream object.
                            dataStream.Close();
                            // Get the response.
                            WebResponse response = request.GetResponse();
                            // Display the status.
                            // Get the stream containing content returned by the server.
                            dataStream = response.GetResponseStream();
                            // Open the stream using a StreamReader for easy access.
                            StreamReader reader = new StreamReader(dataStream);
                            // Read the content.
                            string responseFromServer = reader.ReadToEnd();
                            // Display the content.
                            // Clean up the streams.
                            reader.Close();
                            dataStream.Close();
                            response.Close();

                            Response = responseFromServer;
                        }
                        else
                        {
                            MessageBox.Show("email is not a mail adress! Please try again.", "Error");
                            Response = "register failed!";
                            return; 
                        }

                    }
                }
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion RegisterFunctions

        #region StartupChecks
        //config check on start
        public void checkconfig()
        {
            // Password Hide
            _passwordInputBox.PasswordChar = '*';
            _passwordReInputBox.PasswordChar = '*';
            // Set Graphics to Gui
            setimages();
        }
        //images set on start
        public void setimages()
        {
            // Change reg-background img if Needed!
            if (LoginForm._register_background_img != null)
            { this.BackgroundImage = Image.FromFile(LoginForm.dataDir + LoginForm._register_background_img); }

            // Close / Minimize Buttons, Style CSS
            _btnMinimize.Image = Image.FromFile(LoginForm.dataDir + LoginForm._btn_minimize_nm);
            _btnMinimize.MouseEnter += new EventHandler(btnMinimize_Enter);
            _btnMinimize.MouseLeave += new EventHandler(btnMinimize_Leave);
            _btnMinimize.FlatStyle = FlatStyle.Flat;
            _btnMinimize.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            _btnMinimize.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnMinimize.FlatAppearance.BorderSize = 0;
            _btnMinimize.BackColor = Color.FromArgb(0, 255, 255, 255);

            _btnClose.Image = Image.FromFile(LoginForm.dataDir + LoginForm._btn_close_nm);
            _btnClose.MouseEnter += new EventHandler(btnClose_Enter);
            _btnClose.MouseLeave += new EventHandler(btnClose_Leave);
            _btnClose.FlatStyle = FlatStyle.Flat;
            _btnClose.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            _btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnClose.FlatAppearance.BorderSize = 0;
            _btnClose.BackColor = Color.FromArgb(0, 255, 255, 255);

            // Register Button
            _btnRegister.Image = Image.FromFile(LoginForm.dataDir + LoginForm._btn_register_nm);
            _btnRegister.MouseEnter += new EventHandler(btnRegister_Enter);
            _btnRegister.MouseLeave += new EventHandler(btnRegister_Leave);
            _btnRegister.FlatStyle = FlatStyle.Flat;
            _btnRegister.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            _btnRegister.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnRegister.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            _btnRegister.FlatAppearance.BorderSize = 0;
            _btnRegister.BackColor = Color.FromArgb(0, 255, 255, 255);

        }

        #endregion StartupChecks
    
        #region GUIoverride
        // Form mit Maus überall greifen und verschieben
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
