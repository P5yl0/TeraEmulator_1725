﻿﻿﻿// P5yl0

using System;
using System.Text;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using Configuration;
using Commons;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;
using System.Resources;
using System.Collections;
using System.Windows.Threading;
using System.Windows.Resources;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace cTlauncher
{
    public partial class MainWindow : Window
    {
        #region Properties
        //Game
        public static bool isLoggedIn = false;
        public static string languageGame { get; set; }
        public static int languageId { get; set; }

        //Server
        IPAddress ipAddr = IPAddress.Parse("127.0.0.1");//127.0.0.1;
        Int32 port = 2101;//2101;
        //Data Receiving  
        byte[] bytes = new byte[1024];
        Socket senderSock;

        #endregion Properties

        #region Main
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Set Title
            this.Title = "[Tera-Online] {ProjectS1.Launcher}";

            ipAddr = IPAddress.Parse(Config.GetServerIP());
            port = Convert.ToInt32(Config.GetServerPort());
            //load language
            languageGame = Config.GetLanguageGame();
            languageId = Convert.ToInt32(Config.GetLanguageId());

            _comboboxLanguage.Items.Add("");
            _comboboxLanguage.Items.Add("English");
            _comboboxLanguage.Items.Add("French");
            _comboboxLanguage.Items.Add("German");

            _comboboxLanguage.SelectedIndex = Convert.ToInt32(Config.GetLanguageId());

            _btnPlay.Visibility = Visibility.Hidden;
            _inputServerlist.Visibility = Visibility.Hidden;
            _btnSaveServerlist.Visibility = Visibility.Hidden;

            //rreate exe file
            CreateGameFile();
            //read serverlist string
            OpenServerlist();
        }
        #endregion Main

        #region Login
        private void _btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create one SocketPermission for socket access restrictions 
                SocketPermission permission = new SocketPermission(
                    NetworkAccess.Connect,    // Connection permission 
                    TransportType.Tcp,        // Defines transport types 
                    "",                       // Gets the IP addresses 
                    SocketPermission.AllPorts // All ports 
                    );

                // Ensures the code to have permission to access a Socket 
                permission.Demand();

                // Resolves a host name to an IPHostEntry instance            
                //IPHostEntry ipHost = Dns.GetHostEntry("");

                // Creates a network endpoint 
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

                // Create one Socket object to setup Tcp connection 
                senderSock = new Socket(
                    ipAddr.AddressFamily,// Specifies the addressing scheme 
                    SocketType.Stream,   // The type of socket  
                    ProtocolType.Tcp     // Specifies the protocols  
                    );

                senderSock.NoDelay = false;   // Using the Nagle algorithm 

                // Establishes a connection to a remote host 
                senderSock.Connect(ipEndPoint);

                GUI_Log("connecting auth-server: " + ((IPEndPoint)(senderSock.RemoteEndPoint)).Address);

                SendLoginMessage();
            }
            catch (Exception)//ex) 
            {
                GUI_Log("LoginServer not reachable, please try again later!");
                //MessageBox.Show("LoginServer not reachable, please try again later!","connection error",MessageBoxButton.OK); 
                //MessageBox.Show(ex.ToString()); 
            }
        }
        private void SendLoginMessage()
        {
            try
            {
                //Sending message 
                String username = _inputUsername.Text;
                String password = StringToMD5(_inputPassword.Password);

                string authString = "Auth&" + username + "&" + password;
                GUI_Log("Checking Authorization...");

                byte[] msg = Encoding.Unicode.GetBytes(authString);

                // Sends data to a connected Socket. 
                int bytesSend = senderSock.Send(msg);

                ReceiveMessage();
            }
            catch (Exception)//ex)
            {
                //MessageBox.Show(ex.ToString()); 
            }
        }
        #endregion Login

        #region Register
        private void _btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create one SocketPermission for socket access restrictions 
                SocketPermission permission = new SocketPermission(
                    NetworkAccess.Connect,    // Connection permission 
                    TransportType.Tcp,        // Defines transport types 
                    "",                       // Gets the IP addresses 
                    SocketPermission.AllPorts // All ports 
                    );

                // Ensures the code to have permission to access a Socket 
                permission.Demand();

                // Resolves a host name to an IPHostEntry instance            
                //IPHostEntry ipHost = Dns.GetHostEntry("");

                // Creates a network endpoint 
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

                // Create one Socket object to setup Tcp connection 
                senderSock = new Socket(
                    ipAddr.AddressFamily,// Specifies the addressing scheme 
                    SocketType.Stream,   // The type of socket  
                    ProtocolType.Tcp     // Specifies the protocols  
                    );

                senderSock.NoDelay = false;   // Using the Nagle algorithm 

                // Establishes a connection to a remote host 
                senderSock.Connect(ipEndPoint);

                GUI_Log("connecting reg-server: " + ((IPEndPoint)(senderSock.RemoteEndPoint)).Address);

                SendRegisterMessage();
            }
            catch (Exception)//ex) 
            {
                GUI_Log("RegServer not reachable, please try again later!");
                //MessageBox.Show("LoginServer not reachable, please try again later!","connection error",MessageBoxButton.OK); 
                //MessageBox.Show(ex.ToString()); 
            }
        }
        private void SendRegisterMessage()
        {
            try
            {
                //Sending message 
                String username = _inputUsername.Text;
                String password = StringToMD5(_inputPassword.Password);

                string regString = "Register&" + username + "&" + password;
                GUI_Log("Checking Registration...");

                byte[] msg = Encoding.Unicode.GetBytes(regString);

                // Sends data to a connected Socket. 
                int bytesSend = senderSock.Send(msg);

                ReceiveMessage();
            }
            catch (Exception)//ex)
            {
                //MessageBox.Show(ex.ToString()); 
            }
        }
        #endregion Register

        #region Server
        private void ReceiveMessage()
        {
            try
            {
                // Receives data from a bound Socket. 
                int bytesRec = senderSock.Receive(bytes);

                // Converts byte array to string 
                String msgToReceive = Encoding.Unicode.GetString(bytes, 0, bytesRec);

                // Continues to read the data till data isn't available 
                while (senderSock.Available > 0)
                {
                    bytesRec = senderSock.Receive(bytes);
                    msgToReceive += Encoding.Unicode.GetString(bytes, 0, bytesRec);
                }

                GUI_Log("The server reply: " + msgToReceive);

                //Auth Request
                if (msgToReceive == "Auth_OK")
                {
                    isLoggedIn = true;

                    GUI_Log("Authorization successful!");

                    if (isLoggedIn)
                    {
                        Dispatcher.Invoke(new Action(delegate
                        {
                            _btnLogin.Visibility = Visibility.Hidden;
                            _btnPlay.Visibility = Visibility.Visible;
                        }));
                    }
                    //MessageBox.Show("Authorization ok!");
                }
                if (msgToReceive == "Auth_FAILED")
                {
                    GUI_Log("Authorization failed!");
                    //MessageBox.Show("Authorization failed!");
                }
                //Register Request
                if (msgToReceive == "Register_OK")
                {
                    GUI_Log("Registeration successful!");
                    //MessageBox.Show("Registeration ok!");
                }
                if (msgToReceive == "Register_FAILED")
                {
                    GUI_Log("Registeration failed!");
                    //MessageBox.Show("Registeration failed!");
                }
                if (msgToReceive == "Register_EXIST")
                {
                    GUI_Log("Username already exists!");
                    //MessageBox.Show("Username already Exists!");
                }

                Disconnect();
            }
            catch (Exception)// ex) 
            {
                //MessageBox.Show(ex.ToString()); 
            }
        }
        private void Disconnect()
        {
            try
            {
                // Disables sends and receives on a Socket. 
                senderSock.Shutdown(SocketShutdown.Both);

                //Closes the Socket connection and releases all resources 
                senderSock.Close();
                return;
            }
            catch (Exception)// ex) 
            {
                //MessageBox.Show(ex.ToString()); 
            }
        }
        #endregion Server

        #region Crypt
        // returns MD5 Hash from string
        public static string StringToMD5(string input)
        {
            //Umwandlung des Eingastring in den MD5 Hash
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(input);
            byte[] result = md5.ComputeHash(textToHash);

            //MD5 Hash in String konvertieren
            StringBuilder s = new StringBuilder();
            foreach (byte b in result)
            {
                s.Append(b.ToString("x2").ToLower());
            }

            return s.ToString();
        }
        #endregion Crypt

        #region Gui
        public void GUI_Log(string msg)
        {
            string Date = DateTime.Now.ToShortDateString() + " | " + DateTime.Now.ToShortTimeString();

            Dispatcher.Invoke(new Action(delegate
            {
                _scrollviewerStatus.Content += (Date + " | " + msg + "\n");
                _scrollviewerStatus.ScrollToEnd();
            }));

        }
        #endregion Gui

        #region Buttons
        private void _comboboxLanguage_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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

            IniReader.WriteValue("Launcher", "languageGame", languageGame, Config._configFile);
        }
        private void _inputUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            this._inputUsername.Text = "";
        }
        private void _inputPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            this._inputPassword.Password = "";
        }
        #endregion Buttons

        #region StartProcess
        private void _btnPlay_Click(object sender, RoutedEventArgs e)
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
        //run tera game process
        public void startTera()
        {
            if (isLoggedIn)
            {
                try
                {
                    //params
                    string LaunchString = " 1 " + StringToMD5(_inputPassword.Password) + " 1 0 " + _inputUsername.Text + " " + languageGame;

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = Environment.CurrentDirectory + "\\cTgame.clf";
                    startInfo.Arguments = LaunchString;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.RedirectStandardInput = true;

                    Process.Start(startInfo);
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: Could not start the Game\n" + ex.Message, "ERROR: Game Start Failed!");
                    Environment.Exit(0);
                }
            }
        }
        public void CreateGameFile()
        {
            GUI_Log("Checking Game File...");
            string file2write = Config._rootDir + "cTgame.clf";

            if (File.Exists(file2write))
            {
                GUI_Log("Game File: OK!");
                //File.Delete(file2write);
                return;
            }
            else
            {
                GUI_Log("Game File: failed, re-creating!");
                //Extract Exe from Resource
                byte[] file2writeBinary = Funcs.ExtractResource("cTlauncher.Resources.cTbin.clf");
                //Create Exe
                Funcs.ByteArrayToFile(file2write, file2writeBinary);
            }
        }  

        #endregion StartProcess

        #region Serverlist
        private void _btnSaveServerlist_Click(object sender, RoutedEventArgs e)
        {
            string selectHex = _inputServerlist.Text;
            if (_inputServerlist.Text.Length != 46)
            {
                MessageBox.Show("To prevent file corruption, length of url must be exact 46 characters long!", "ERROR", MessageBoxButton.OK);
                return;
            }
            else
            {
                SaveServerlist();
                MessageBox.Show("Your new serverlist path has been saved!");
                GUI_Log("Your new serverlist path has been saved!");

                OpenServerlist();
            }

        }
        private void _btnSwitchServerlist_Click(object sender, RoutedEventArgs e)
        {
            if (_inputServerlist.Visibility == Visibility.Hidden)
            {
                _inputServerlist.Visibility = Visibility.Visible;
                _btnSaveServerlist.Visibility = Visibility.Visible;
            }
            else
            {
                _inputServerlist.Visibility = Visibility.Hidden;
                _btnSaveServerlist.Visibility = Visibility.Hidden;
            }
        }
        private void OpenServerlist()
        {
            if (File.Exists(Config._rootDir + "cTgame.clf"))
            {
                //show serverlist
                BinaryReader br = new BinaryReader(File.OpenRead(Config._rootDir + "cTgame.clf"));
                string selectHex = null;
                for (int i = 0x4E699C; i <= 0x4E69C9; i++)
                {
                    br.BaseStream.Position = i;
                    selectHex += br.ReadByte().ToString("X2");
                }
                br.Close();
                _inputServerlist.Text = Configuration.Converter.HexStringToAscii(selectHex);
            }
            else
            {
                _inputServerlist.Text = "Cant read Serverlist string, cTgame.clf not readable or found!";
                return;
            }
        }
        private void SaveServerlist()
        {
            if (File.Exists(Config._rootDir + "cTgame.clf"))
            {
                string selectHex = _inputServerlist.Text;
                int x = -1;
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(selectHex);
                BinaryWriter bw = new BinaryWriter(File.OpenWrite((Config._rootDir + "cTgame.clf")));
                for (int i = 0x4E699C; i <= 0x4E69C9; i++)
                {
                    x++;
                    bw.BaseStream.Position = i;
                    bw.Write(ba[x]);
                }
                bw.Close();
            }
            else
            {
                GUI_Log("Cant save Serverlist string to file!");
                return;
            }
        }

        #endregion Serverlist
    }
}
