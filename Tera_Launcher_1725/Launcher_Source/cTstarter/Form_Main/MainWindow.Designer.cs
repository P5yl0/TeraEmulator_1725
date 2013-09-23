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
            this._labelStatus = new System.Windows.Forms.Label();
            this._progressbarStatus = new System.Windows.Forms.ProgressBar();
            this._labelTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _labelStatus
            // 
            this._labelStatus.AutoSize = true;
            this._labelStatus.Location = new System.Drawing.Point(12, 42);
            this._labelStatus.Name = "_labelStatus";
            this._labelStatus.Size = new System.Drawing.Size(40, 13);
            this._labelStatus.TabIndex = 0;
            this._labelStatus.Text = "Status:";
            // 
            // _progressbarStatus
            // 
            this._progressbarStatus.Location = new System.Drawing.Point(50, 58);
            this._progressbarStatus.Name = "_progressbarStatus";
            this._progressbarStatus.Size = new System.Drawing.Size(200, 10);
            this._progressbarStatus.Step = 1;
            this._progressbarStatus.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this._progressbarStatus.TabIndex = 1;
            // 
            // _labelTitle
            // 
            this._labelTitle.AutoSize = true;
            this._labelTitle.Location = new System.Drawing.Point(12, 9);
            this._labelTitle.Name = "_labelTitle";
            this._labelTitle.Size = new System.Drawing.Size(23, 13);
            this._labelTitle.TabIndex = 2;
            this._labelTitle.Text = "title";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 80);
            this.ControlBox = false;
            this.Controls.Add(this._labelTitle);
            this.Controls.Add(this._progressbarStatus);
            this.Controls.Add(this._labelStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cTL-updater";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _labelStatus;
        private System.Windows.Forms.ProgressBar _progressbarStatus;
        private System.Windows.Forms.Label _labelTitle;
    }
}

