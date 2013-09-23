namespace cLauncher
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this._buttonClose = new System.Windows.Forms.Button();
            this._buttonLogin = new System.Windows.Forms.Button();
            this._buttonInfo = new System.Windows.Forms.Button();
            this._labelTitle = new System.Windows.Forms.Label();
            this._inputboxUsername = new System.Windows.Forms.TextBox();
            this._inputboxPassword = new System.Windows.Forms.TextBox();
            this._inputboxServerlist = new System.Windows.Forms.TextBox();
            this._labelWelcome = new System.Windows.Forms.Label();
            this._buttonPlay = new System.Windows.Forms.Button();
            this._comboboxLanguage = new System.Windows.Forms.ComboBox();
            this._webbrowserNews = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // _buttonClose
            // 
            this._buttonClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this._buttonClose.FlatAppearance.BorderSize = 0;
            this._buttonClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this._buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this._buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._buttonClose.ForeColor = System.Drawing.SystemColors.Control;
            this._buttonClose.Location = new System.Drawing.Point(668, 0);
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(32, 32);
            this._buttonClose.TabIndex = 0;
            this._buttonClose.Text = "x";
            this._buttonClose.UseVisualStyleBackColor = true;
            this._buttonClose.Click += new System.EventHandler(this._btnClose_Click);
            this._buttonClose.MouseEnter += new System.EventHandler(this._btnClose_Enter);
            this._buttonClose.MouseLeave += new System.EventHandler(this._btnClose_Leave);
            // 
            // _buttonLogin
            // 
            this._buttonLogin.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this._buttonLogin.FlatAppearance.BorderSize = 0;
            this._buttonLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this._buttonLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this._buttonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._buttonLogin.ForeColor = System.Drawing.SystemColors.Control;
            this._buttonLogin.Location = new System.Drawing.Point(480, 123);
            this._buttonLogin.Name = "_buttonLogin";
            this._buttonLogin.Size = new System.Drawing.Size(200, 36);
            this._buttonLogin.TabIndex = 5;
            this._buttonLogin.Text = "login";
            this._buttonLogin.UseVisualStyleBackColor = true;
            this._buttonLogin.Click += new System.EventHandler(this._btnLogin_Click);
            this._buttonLogin.MouseEnter += new System.EventHandler(this._btnLogin_Enter);
            this._buttonLogin.MouseLeave += new System.EventHandler(this._btnLogin_Leave);
            // 
            // _buttonInfo
            // 
            this._buttonInfo.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this._buttonInfo.FlatAppearance.BorderSize = 0;
            this._buttonInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this._buttonInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this._buttonInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._buttonInfo.ForeColor = System.Drawing.SystemColors.Control;
            this._buttonInfo.Location = new System.Drawing.Point(668, 388);
            this._buttonInfo.Name = "_buttonInfo";
            this._buttonInfo.Size = new System.Drawing.Size(32, 32);
            this._buttonInfo.TabIndex = 2;
            this._buttonInfo.Text = "i";
            this._buttonInfo.UseVisualStyleBackColor = true;
            this._buttonInfo.Click += new System.EventHandler(this._btnInfo_Click);
            this._buttonInfo.MouseEnter += new System.EventHandler(this._btnInfo_Enter);
            this._buttonInfo.MouseLeave += new System.EventHandler(this._btnInfo_Leave);
            // 
            // _labelTitle
            // 
            this._labelTitle.AutoSize = true;
            this._labelTitle.BackColor = System.Drawing.Color.Transparent;
            this._labelTitle.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelTitle.ForeColor = System.Drawing.Color.White;
            this._labelTitle.Location = new System.Drawing.Point(12, 9);
            this._labelTitle.Name = "_labelTitle";
            this._labelTitle.Size = new System.Drawing.Size(26, 13);
            this._labelTitle.TabIndex = 1;
            this._labelTitle.Text = "title";
            // 
            // _inputboxUsername
            // 
            this._inputboxUsername.Location = new System.Drawing.Point(480, 42);
            this._inputboxUsername.Name = "_inputboxUsername";
            this._inputboxUsername.Size = new System.Drawing.Size(208, 20);
            this._inputboxUsername.TabIndex = 3;
            // 
            // _inputboxPassword
            // 
            this._inputboxPassword.Location = new System.Drawing.Point(480, 68);
            this._inputboxPassword.Name = "_inputboxPassword";
            this._inputboxPassword.Size = new System.Drawing.Size(208, 20);
            this._inputboxPassword.TabIndex = 4;
            // 
            // _inputboxServerlist
            // 
            this._inputboxServerlist.BackColor = System.Drawing.Color.White;
            this._inputboxServerlist.Location = new System.Drawing.Point(15, 388);
            this._inputboxServerlist.Multiline = true;
            this._inputboxServerlist.Name = "_inputboxServerlist";
            this._inputboxServerlist.Size = new System.Drawing.Size(456, 20);
            this._inputboxServerlist.TabIndex = 20;
            // 
            // _labelWelcome
            // 
            this._labelWelcome.AutoSize = true;
            this._labelWelcome.BackColor = System.Drawing.Color.Transparent;
            this._labelWelcome.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelWelcome.ForeColor = System.Drawing.Color.White;
            this._labelWelcome.Location = new System.Drawing.Point(477, 26);
            this._labelWelcome.Name = "_labelWelcome";
            this._labelWelcome.Size = new System.Drawing.Size(85, 13);
            this._labelWelcome.TabIndex = 21;
            this._labelWelcome.Text = "Welcome, Guest";
            // 
            // _buttonPlay
            // 
            this._buttonPlay.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this._buttonPlay.FlatAppearance.BorderSize = 0;
            this._buttonPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this._buttonPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this._buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._buttonPlay.ForeColor = System.Drawing.SystemColors.Control;
            this._buttonPlay.Location = new System.Drawing.Point(480, 123);
            this._buttonPlay.Name = "_buttonPlay";
            this._buttonPlay.Size = new System.Drawing.Size(200, 36);
            this._buttonPlay.TabIndex = 22;
            this._buttonPlay.Text = "play";
            this._buttonPlay.UseVisualStyleBackColor = true;
            this._buttonPlay.Click += new System.EventHandler(this._btnPlay_Click);
            this._buttonPlay.MouseEnter += new System.EventHandler(this._btnPlay_Enter);
            this._buttonPlay.MouseLeave += new System.EventHandler(this._btnPlay_Leave);
            // 
            // _comboboxLanguage
            // 
            this._comboboxLanguage.FormattingEnabled = true;
            this._comboboxLanguage.Location = new System.Drawing.Point(480, 95);
            this._comboboxLanguage.Margin = new System.Windows.Forms.Padding(4);
            this._comboboxLanguage.Name = "_comboboxLanguage";
            this._comboboxLanguage.Size = new System.Drawing.Size(99, 21);
            this._comboboxLanguage.TabIndex = 24;
            this._comboboxLanguage.SelectedIndexChanged += new System.EventHandler(this._languageBox_SelectedIndexChanged);
            // 
            // _webbrowserNews
            // 
            this._webbrowserNews.AllowNavigation = false;
            this._webbrowserNews.AllowWebBrowserDrop = false;
            this._webbrowserNews.Location = new System.Drawing.Point(15, 26);
            this._webbrowserNews.MinimumSize = new System.Drawing.Size(20, 20);
            this._webbrowserNews.Name = "_webbrowserNews";
            this._webbrowserNews.ScrollBarsEnabled = false;
            this._webbrowserNews.Size = new System.Drawing.Size(456, 356);
            this._webbrowserNews.TabIndex = 25;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(700, 420);
            this.ControlBox = false;
            this.Controls.Add(this._buttonLogin);
            this.Controls.Add(this._webbrowserNews);
            this.Controls.Add(this._comboboxLanguage);
            this.Controls.Add(this._labelWelcome);
            this.Controls.Add(this._inputboxServerlist);
            this.Controls.Add(this._inputboxPassword);
            this.Controls.Add(this._inputboxUsername);
            this.Controls.Add(this._buttonInfo);
            this.Controls.Add(this._labelTitle);
            this.Controls.Add(this._buttonClose);
            this.Controls.Add(this._buttonPlay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 420);
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cLauncher";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonClose;
        private System.Windows.Forms.Label _labelTitle;
        private System.Windows.Forms.Button _buttonInfo;
        private System.Windows.Forms.TextBox _inputboxUsername;
        private System.Windows.Forms.TextBox _inputboxPassword;
        private System.Windows.Forms.Button _buttonLogin;
        private System.Windows.Forms.TextBox _inputboxServerlist;
        private System.Windows.Forms.Label _labelWelcome;
        private System.Windows.Forms.Button _buttonPlay;
        private System.Windows.Forms.ComboBox _comboboxLanguage;
        private System.Windows.Forms.WebBrowser _webbrowserNews;
    }
}

