using Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cLauncher
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            try
            {
                //invoke to send to other thread
                _lblInfo.Invoke((MethodInvoker)(delegate()
                {
                    _lblInfo.Text =
                        Configuration.GetLauncherTitle() + "\n" +
                        "build: " + Configuration.GetLauncherVersion() + "\n" +
                        "\n" +
                        "(C)P5yl0, 2013"
                        ;
                }));
                Functions.pause(100);
            }
            catch
            {
            }
        }
    }
}
