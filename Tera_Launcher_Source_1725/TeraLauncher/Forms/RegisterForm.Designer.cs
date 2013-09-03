namespace TeraLauncher
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._btnClose = new System.Windows.Forms.Button();
            this._btnMinimize = new System.Windows.Forms.Button();
            this._btnRegister = new System.Windows.Forms.Button();
            this._usernameInputBox = new System.Windows.Forms.TextBox();
            this._passwordInputBox = new System.Windows.Forms.TextBox();
            this._passwordReInputBox = new System.Windows.Forms.TextBox();
            this._emailInputBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _btnClose
            // 
            this._btnClose.Location = new System.Drawing.Point(786, 7);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(25, 25);
            this._btnClose.TabIndex = 0;
            this._btnClose.UseVisualStyleBackColor = true;
            this._btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // _btnMinimize
            // 
            this._btnMinimize.Location = new System.Drawing.Point(759, 7);
            this._btnMinimize.Name = "_btnMinimize";
            this._btnMinimize.Size = new System.Drawing.Size(25, 25);
            this._btnMinimize.TabIndex = 1;
            this._btnMinimize.UseVisualStyleBackColor = true;
            this._btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // _btnRegister
            // 
            this._btnRegister.Location = new System.Drawing.Point(342, 290);
            this._btnRegister.Name = "_btnRegister";
            this._btnRegister.Size = new System.Drawing.Size(149, 55);
            this._btnRegister.TabIndex = 2;
            this._btnRegister.UseVisualStyleBackColor = true;
            this._btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // _usernameInputBox
            // 
            this._usernameInputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this._usernameInputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._usernameInputBox.Font = new System.Drawing.Font("Calibri", 14F);
            this._usernameInputBox.ForeColor = System.Drawing.Color.White;
            this._usernameInputBox.Location = new System.Drawing.Point(273, 111);
            this._usernameInputBox.Name = "_usernameInputBox";
            this._usernameInputBox.Size = new System.Drawing.Size(279, 29);
            this._usernameInputBox.TabIndex = 3;
            // 
            // _passwordInputBox
            // 
            this._passwordInputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this._passwordInputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._passwordInputBox.Font = new System.Drawing.Font("Calibri", 14F);
            this._passwordInputBox.ForeColor = System.Drawing.Color.White;
            this._passwordInputBox.Location = new System.Drawing.Point(273, 153);
            this._passwordInputBox.Name = "_passwordInputBox";
            this._passwordInputBox.Size = new System.Drawing.Size(279, 29);
            this._passwordInputBox.TabIndex = 4;
            // 
            // _passwordReInputBox
            // 
            this._passwordReInputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this._passwordReInputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._passwordReInputBox.Font = new System.Drawing.Font("Calibri", 14F);
            this._passwordReInputBox.ForeColor = System.Drawing.Color.White;
            this._passwordReInputBox.Location = new System.Drawing.Point(273, 195);
            this._passwordReInputBox.Name = "_passwordReInputBox";
            this._passwordReInputBox.Size = new System.Drawing.Size(279, 29);
            this._passwordReInputBox.TabIndex = 5;
            // 
            // _emailInputBox
            // 
            this._emailInputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this._emailInputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._emailInputBox.Font = new System.Drawing.Font("Calibri", 14F);
            this._emailInputBox.ForeColor = System.Drawing.Color.White;
            this._emailInputBox.Location = new System.Drawing.Point(273, 237);
            this._emailInputBox.Name = "_emailInputBox";
            this._emailInputBox.Size = new System.Drawing.Size(279, 29);
            this._emailInputBox.TabIndex = 6;
            // 
            // RegisterForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(816, 454);
            this.Controls.Add(this._emailInputBox);
            this.Controls.Add(this._passwordReInputBox);
            this.Controls.Add(this._passwordInputBox);
            this.Controls.Add(this._usernameInputBox);
            this.Controls.Add(this._btnRegister);
            this.Controls.Add(this._btnMinimize);
            this.Controls.Add(this._btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btnClose;
        private System.Windows.Forms.Button _btnMinimize;
        private System.Windows.Forms.Button _btnRegister;
        private System.Windows.Forms.TextBox _usernameInputBox;
        private System.Windows.Forms.TextBox _passwordInputBox;
        private System.Windows.Forms.TextBox _passwordReInputBox;
        private System.Windows.Forms.TextBox _emailInputBox;
    }
}