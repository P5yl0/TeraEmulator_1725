using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Commons;
using System.Net;
using System.IO;
using Ionic.Zip;
using System.Diagnostics;

namespace cLauncher
{
    public partial class MainWindow : Form
    {
        #region Vars

        public static TcpClient tcpPatchServer_client = new TcpClient(); //client to check server connection
        public static WebClient webPatchVersion_client = new WebClient(); //client to check remote version file
        public static WebClient webPatchDownload_client = new WebClient(); //client for download process
        
        public static string sLocalVersion;  //Local Version String
        public static int iLocalVersion;   //Local Version Int
        
        public static string sRemoteVersion; //Remote Version String
        public static int iRemoteVersion;   //Remote Version Int

        public static string sPatchFile = Configuration.GetPatchDownloadFile();  //patch filename
        public static string sPatchDownloadUrl = Configuration.GetPatchDownloadUrl() + sPatchFile; //full url to patch file

        #endregion Vars

        #region Main
        //main entry
        public MainWindow()
        {
            InitializeComponent();
        }
        //form load
        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.Show();
            InitializeConfiguration();
        }

        public void InitializeConfiguration()
        {
            //set title
            _labelTitle.Text = "";
            //load title text
            try { _labelTitle.Text = Configuration.GetLauncherTitle() + " Patcher v." + Configuration.GetLauncherVersion();; }
            catch { _labelTitle.Text = "Tera cTLauncher" + " Patcher v." + Configuration.GetLauncherVersion(); ; }

            //check connection
            CheckServerConnection();


        }
        
        #endregion Main

        #region UpdateGui
        //Write status
        public void updateStatus(int value, string msg)
        {
            //invoke progressbar
            _progressbarStatus.Invoke((MethodInvoker)(delegate()
            {
                _progressbarStatus.Value = value;

                if (_progressbarStatus.Value == 100)
                {
                    _labelStatus.ForeColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(121)))), ((int)(((byte)(203)))));
                }
            }));
            //invoke status label
            _labelStatus.Invoke((MethodInvoker)(delegate()
            {
                _labelStatus.Text = msg;
            }));
        }
        #endregion UpdateGui
        
        #region Checks
        //Check Server Connection
        public void CheckServerConnection()
        {
            updateStatus(10, "Status: checking patchserver connection...");
            Funcs.pause(1000);
            //check connection
            try
            {
                tcpPatchServer_client = new TcpClient(Configuration.GetServerUrl(), Convert.ToInt32(Configuration.GetServerPort()));

                if (tcpPatchServer_client.Connected)
                {
                    updateStatus(100, "Status: patchserver connection established...");
                    tcpPatchServer_client.Close();
                    Funcs.pause(1000);

                    //connection ok, check & compare versions
                    CheckVersion();
                }
            }
            catch
            {
                if (!tcpPatchServer_client.Connected)
                {
                    updateStatus(100, "Status: patchserver connection failed, starting launcher!");
                    Funcs.pause(1000);
                    //connection fail, run launcher
                    StartLauncher();
                }
            }
        }
        //check & compare remote/local version
        public void CheckVersion()
        {
            CheckLocalVersion();
            CheckRemoteVersion();

            //compare versions
            if (iLocalVersion < iRemoteVersion)
            {
                updateStatus(100, "Status: new version exists, starting download!");
                Funcs.pause(1000);

                //kick off download
                Thread _downloadLauncher = new Thread(UpdateLauncher);
                _downloadLauncher.Start();

            }
            else if (iLocalVersion == iRemoteVersion)
            {
                updateStatus(100, "Status: no updates! starting launcher!");
                Funcs.pause(1000);
                StartLauncher();
            }
            else if (iLocalVersion > iRemoteVersion)
            {
                updateStatus(100, "Error: version missmatch, pls redownload launcher!");
                Funcs.pause(2000);
                Application.Exit();
            }
        
        }
        //check version local
        private void CheckLocalVersion()
        {
            //check local version
            try
            {
                updateStatus(25, "Status: checking local version...");
                Funcs.pause(500);
                updateStatus(100, "Status: local version: " + Configuration.GetLauncherVersion());
                Funcs.pause(500);

                //convert for version compare
                try
                {
                    string sVersionLocal = Configuration.GetLauncherVersion();
                    string[] sVersionLocalSplit = sVersionLocal.Split(new Char[] { '.' });
                    StringBuilder _TEMP = new StringBuilder();
                    int i = 0;
                    while (i < sVersionLocalSplit.Length)
                    {
                        _TEMP.Append(sVersionLocalSplit[i]);
                        i++;
                    }
                    sVersionLocal = _TEMP.ToString();
                    iLocalVersion = Convert.ToInt32(sVersionLocal);
                }
                catch
                {
                    updateStatus(100, "Error: local version has false Parameters!");
                    Funcs.pause(1000);
                }
            }
            catch
            {
                updateStatus(100, "Error: local version check failed, version file not found!");
                Funcs.pause(1000);
                StartLauncher();
            }
        }
        //check version on server
        private void CheckRemoteVersion()
        {
            //check remote version
            try
            {
                updateStatus(25, "Status: checking remote version...");
                Funcs.pause(500);
                sRemoteVersion = webPatchVersion_client.DownloadString(Configuration.GetRemoteVersionUrl());
                Funcs.pause(500);
                updateStatus(100, "Status: remote version: " + sRemoteVersion +" found!");
                Funcs.pause(500);

                //convert for version compare
                string[] sVersionTempSplit = sRemoteVersion.Split(new Char[] { '.' });
                int i = 0;
                
                try
                {
                    string sVersion_Temp;
                    StringBuilder _TEMP = new StringBuilder();
                    while (i < sVersionTempSplit.Length)
                    {
                        _TEMP.Append(sVersionTempSplit[i]);
                        i++;
                    }
                    sVersion_Temp = _TEMP.ToString();
                    iRemoteVersion = Convert.ToInt32(sVersion_Temp);
                }
                catch
                {
                    updateStatus(100, "Error: remote version has false Parameters!");
                    Funcs.pause(1000);
                }
            }
            catch
            {
                updateStatus(100, "Error: remote version check failed, version file not found!");
                Funcs.pause(1000);
            }
        }
        #endregion Checks

        #region UpdateLauncher
        //prepare download & update
        private void UpdateLauncher()
        {
            try
            {
                updateStatus(1, "Status: starting download...");
                Funcs.pause(500);
                
                //Download Process
                DownloadLauncher();
                Funcs.pause(500);

                updateStatus(10, "Status: unpacking files...");
                //Processing files
                UnpackLauncher();
                Funcs.pause(500);
            }
            catch
            {
                updateStatus(100, "Error: Could not download launcher file!");
                Funcs.pause(500);
                return;
            }
        }
        //downloading Process
        public void DownloadLauncher()
        {
            Uri url = new Uri(sPatchDownloadUrl);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();

            Int64 iSize = response.ContentLength;
            Int64 iRunningByteTotal = 0;

            using (WebClient webPatchDownload_client = new WebClient())
            {
                using (Stream streamRemote = webPatchDownload_client.OpenRead(new Uri(sPatchDownloadUrl)))
                {
                    using (Stream streamLocal = new FileStream(Configuration._rootDir + sPatchFile, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        int iByteSize = 0;
                        byte[] byteBuffer = new byte[iSize];
                        while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {
                            streamLocal.Write(byteBuffer, 0, iByteSize);
                            iRunningByteTotal += iByteSize;

                            double dIndex = (double)(iRunningByteTotal);
                            double dTotal = (double)byteBuffer.Length;
                            double dProgressPercentage = (dIndex / dTotal);
                            int iProgressPercentage = (int)(dProgressPercentage * 100);

                            updateStatus(iProgressPercentage, "Status: downloading...");
                            Funcs.pause(25);
                        }
                        streamLocal.Close();
                    }
                    streamRemote.Close();
                }
            }

            Funcs.pause(500);
            updateStatus(100, "Status: download complete...");
            Funcs.pause(500);
            return;
        }
        //unpack & remove temp files
        public void UnpackLauncher()
        {
            //unzip
            using (ZipFile zip = ZipFile.Read(Configuration._rootDir + sPatchFile))
            {
                foreach (ZipEntry zipFiles in zip)
                {
                    zipFiles.Extract(Configuration._rootDir, true);
                }
            }

                //write new version to config file
                IniReader.WriteValue("Launcher", "versionLauncher", sRemoteVersion, Configuration._configFile);
                //Delete Zip File
                Funcs.deleteFile(Configuration._rootDir + sPatchFile);
                updateStatus(70, "Status: remove temp files...");
                Funcs.pause(1000);
                //all done!
                updateStatus(100, "Status: patch process done...");
                Funcs.pause(500);

                //run launcher now
                StartLauncher();
        }

        #endregion UpdateLauncher

        #region GameStart
        //starting launcher file
        public void StartLauncher()
        {
            updateStatus(100, "Status: Starting launcher...");

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Configuration._rootDir + "cTlauncher.clf";
                //startInfo.Arguments = Configuration.GetLauncherUrl();
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
                MessageBox.Show("ERROR: Could not start the Launcher\n" + ex.Message, "ERROR: Launcher Start Failed!");
                Application.Exit();
            }

        }
        #endregion GameStart
    }
}
