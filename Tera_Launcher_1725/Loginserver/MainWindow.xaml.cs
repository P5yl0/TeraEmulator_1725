﻿﻿﻿// Houssem Dellai    
// houssem.dellai@ieee.org    
// +216 95 325 964    
// Studying Software Engineering    
// in the National Engineering School of Sfax (ENIS)   

using System;
using System.Text;
using System.Windows;
using System.Net; 
using System.Net.Sockets;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;
//
using Configuration;
using AccountService;
using MySql.Data.MySqlClient;
using System.Data;


namespace TeraLoginServer
{
    public partial class MainWindow : Window
    {
        #region Properties
        //Database
        String MyConString = "SERVER=" + Database.GetDatabaseHost() + ";DATABASE=" + Database.GetDatabaseName() + ";UID=" + Database.GetDatabaseUser() + ";PASSWORD=" + Database.GetDatabasePass() + ";PORT=" + Database.GetDatabasePort() + ";charset=utf8";

        //Server
        IPAddress ipAddr;//IPAddress.Any;
        Int32 port;//2101;
        SocketPermission permission;
        Socket sListener;
        IPEndPoint ipEndPoint;
        Socket handler;
        #endregion Properties

        #region Main
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Set Title
            this.Title = "[Tera-Online] {ProjectS1.LoginServer}";

            ipAddr = IPAddress.Parse(Network.GetLoginServerIp()); //IPAddress.Any;
            port = Network.GetLoginServerPort();//2101;
            String MyConString = "SERVER=" + Database.GetDatabaseHost() + ";DATABASE=" + Database.GetDatabaseName() + ";UID=" + Database.GetDatabaseUser() + ";PASSWORD=" + Database.GetDatabasePass() + ";PORT=" + Database.GetDatabasePort() + ";charset=utf8";

        }
        #endregion Main

        #region Server
        private void StartLoginServer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Creates one SocketPermission object for access restrictions
                permission = new SocketPermission(
                NetworkAccess.Accept,     // Allowed to accept connections 
                TransportType.Tcp,        // Defines transport types 
                "",                       // The IP addresses of local host 
                SocketPermission.AllPorts // Specifies all ports 
                );

                // Listening Socket object 
                sListener = null;

                // Ensures the code to have permission to access a Socket 
                permission.Demand();

                // Resolves a host name to an IPHostEntry instance 
                IPHostEntry ipHost = Dns.GetHostEntry("");

                // Gets first IP address associated with a localhost 
                //IPAddress ipAddr = ipHost.AddressList[0];

                // Creates a network endpoint 
                //ipEndPoint = new IPEndPoint(ipAddr, 4510);
                ipEndPoint = new IPEndPoint(ipAddr, port);

                // Create one Socket object to listen the incoming connection 
                sListener = new Socket(
                    ipAddr.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp
                    );

                // Associates a Socket with a local endpoint 
                sListener.Bind(ipEndPoint);

                Start_Button.IsEnabled = false;

                Listen_Connection();

            }
            catch (Exception)// ex) 
            { 
                //MessageBox.Show(ex.ToString()); 
            }

        }
        private void Listen_Connection()
        {
            try
            {
                // Places a Socket in a listening state and specifies the maximum 
                // Length of the pending connections queue 
                sListener.Listen(10);

                // Begins an asynchronous operation to accept an attempt 
                AsyncCallback aCallback = new AsyncCallback(AcceptCallback);
                sListener.BeginAccept(aCallback, sListener);

                GUI_Log("Login-Server listening on " + ipEndPoint.Address + " port: " + ipEndPoint.Port);
            }
            catch (Exception)// ex) 
            { 
                //MessageBox.Show(ex.ToString()); 
            }
        }
        public void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = null;

            // A new Socket to handle remote host communication 
            Socket handler = null;
            try
            {
                // Receiving byte array 
                byte[] buffer = new byte[1024];
                // Get Listening Socket object 
                listener = (Socket)ar.AsyncState;
                // Create a new socket 
                handler = listener.EndAccept(ar);

                // Using the Nagle algorithm 
                handler.NoDelay = false;

                // Creates one object array for passing data 
                object[] obj = new object[2];
                obj[0] = buffer;
                obj[1] = handler;

                // Begins to asynchronously receive data 
                handler.BeginReceive(
                    buffer,        // An array of type Byt for received data 
                    0,             // The zero-based position in the buffer  
                    buffer.Length, // The number of bytes to receive 
                    SocketFlags.None,// Specifies send and receive behaviors 
                    new AsyncCallback(ReceiveCallback),//An AsyncCallback delegate 
                    obj            // Specifies infomation for receive operation 
                    );

                // Begins an asynchronous operation to accept an attempt 
                AsyncCallback aCallback = new AsyncCallback(AcceptCallback);
                listener.BeginAccept(aCallback, listener);
            }
            catch (Exception)//ex) 
            {
                //MessageBox.Show(ex.ToString()); 
            }
        }
        public void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Fetch a user-defined object that contains information 
                object[] obj = new object[2];
                obj = (object[])ar.AsyncState;
                // Received byte array 
                byte[] buffer = (byte[])obj[0];
                // A Socket to handle remote host communication. 
                handler = (Socket)obj[1];
                // Received message 
                string content = string.Empty;
                // The number of bytes received. 
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    content += Encoding.Unicode.GetString(buffer, 0, bytesRead);
                    
                    string[] mainSplit = content.Split(new Char[] { '&' });
                    string method = mainSplit[0];
                    string username = mainSplit[1];
                    string password = mainSplit[2];

                    #region Auth
                    if (method == "Auth")
                    {
                        GUI_Log("Auth request for: " + username + " , " + password);

                        if (isLoginValid(username, password))
                        {
                            //Send Auth Ok

                            GUI_Log("Auth OK for: " + username + " , " + password);
                            SendMessage("Auth_OK");
                        }
                        else
                        {
                            //Send Auth Failed
                            GUI_Log( "Auth Failed for User: " + username);
                            SendMessage("Auth_FAILED");
                        }
                    }
                    #endregion Auth

                    #region Register
                    if (method == "Register")
                    {
                        GUI_Log("Registration Request User: " + username + " & Password: " + password);

                        if (isRegisterValid(username, password) == "ok")
                        {
                            register_account(username, password);
                            GUI_Log("Registration for: " + username + " done!");
                            SendMessage("Register_OK");
                        }
                        else
                            if (isRegisterValid(username, password) == "failed")
                            {
                                //Send Reg Failed
                                GUI_Log("Registration failed: " + username);
                                SendMessage("Register_FAILED");
                            }
                            else
                                if (isRegisterValid(username, password) == "exist")
                                {
                                    GUI_Log("Account already exists: " + username);
                                    SendMessage("Register_EXIST");
                                }
                    }
                    #endregion Register

                    else
                    {
                        // Continues to asynchronously receive data
                        byte[] buffernew = new byte[1024];
                        obj[0] = buffernew;
                        obj[1] = handler;
                        handler.BeginReceive(buffernew, 0, buffernew.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), obj);
                    }
                }
            }
            catch (Exception)// ex) 
            { 
                //MessageBox.Show(ex.ToString());
            }
        }
        private void SendMessage(string msg)
        {
            try
            {
                // Prepare the reply message 
                byte[] byteData = Encoding.Unicode.GetBytes(msg);
                // Sends data asynchronously to a connected Socket 
                handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);

                Close_Connection();
            }
            catch (Exception) //ex) 
            { 
                //MessageBox.Show(ex.ToString()); 
            }
        }
        public void SendCallback(IAsyncResult ar)
        {
            try
            {
                // A Socket which has sent the data to remote host 
                Socket handler = (Socket)ar.AsyncState;

                // The number of bytes sent to the Socket 
                int bytesSend = handler.EndSend(ar);
                GUI_Log("Sent "+  bytesSend +" bytes to Client");
            }
            catch (Exception)// ex)
            { 
                //MessageBox.Show(ex.ToString()); 
            }
        }
        private void Close_Connection()
        {
            try
            {
                if (sListener.Connected)
                {
                    sListener.Shutdown(SocketShutdown.Receive);
                    sListener.Close();
                }
            }
            catch (Exception)// ex) 
            { 
                //MessageBox.Show(ex.ToString()); 
            }
        }
        #endregion Server

        #region Account Login&Register
        public Account loadAccount(string accountName)
        {
            MySqlConnection SQLconnection = new MySqlConnection(MyConString);
            Account account = null;
            try
            {
                MySqlCommand cmd = SQLconnection.CreateCommand();
                SQLconnection.Open();
                cmd.CommandText = "SELECT Id,Username,Email,Password,AccessLevel,Membership,isGM,LastOnlineUtc,Coins,Ip,UiSettings FROM accounts WHERE Username='" + accountName + "'";
                cmd.CommandType = CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    account = new Account(accountName);
                    account.Id = reader.GetInt32("Id");
                    account.Username = reader.GetString("Username");
                    account.Email = reader.GetString("Email");
                    account.Password = reader.GetString("Password");
                    account.AccessLevel = reader.GetInt32("AccessLevel");
                    account.Membership = reader.GetString("Membership");
                    account.isGM = Convert.ToBoolean(reader.GetInt32("isGM"));
                    account.LastOnlineUtc = reader.GetInt32("LastOnlineUtc");
                    account.Coins = reader.GetInt32("Coins");
                    account.Ip = reader.GetString("Ip");
                    account.UiSettings = reader.GetString("UiSettings");
                }
                reader.Close();
                SQLconnection.Close();
            }
            catch (Exception)// ex)
            {
                SQLconnection.Close();
                //MessageBox.Show(ex.ToString()); 
                return null;
            }

            SQLconnection.Close();
            return account;
        }
        public bool isLoginValid(string accountName, string accountPass)
        {
            try
            {
                //GUI_Log("Checking Account: " + accountName);
                Account account = loadAccount(accountName);
                //Check User & Pass
                if (accountName.Equals(account.Username) && accountPass.Equals(account.Password) && account.AccessLevel >= 1)
                {
                    //GUI_Log("Account authorization: Ok!");
                    return true;
                }
                //
                else
                {
                    //GUI_Log("Account authorization: Failed!");
                    return false;
                }
            }
            catch (Exception)//ex)
            {
                //MessageBox.Show(ex.ToString()); 
                return false;
            }
        }
        public bool doesAccountExist(string accountName)
        {
            MySqlConnection SQLconnection = new MySqlConnection(MyConString);
            bool accountexist = true;

            try
            {
                MySqlCommand cmd = SQLconnection.CreateCommand();
                SQLconnection.Open();
                cmd.CommandText = "SELECT Username FROM accounts WHERE Username='" + accountName + "'";
                cmd.CommandType = CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                string db_accountexist = reader.GetString("Username");

                if (accountName.Equals(db_accountexist))
                {
                    accountexist = true;
                }
                else
                {
                    accountexist = false;
                }
                reader.Close();
                SQLconnection.Close();
            }
            catch (Exception) //ex)
            {
                //MessageBox.Show(ex.ToString()); 
                accountexist = false;
            }
            SQLconnection.Close();
            return accountexist;
        }
        public String isRegisterValid(string accountName, string accountPass)
        {
            try
            {
                Account account = loadAccount(accountName);
                //Check if account exists
                if (doesAccountExist(accountName))
                {
                    return "exist";
                }
                else
                {
                    return "ok";
                }
            }
            catch (Exception)//ex)
            {
                //MessageBox.Show(ex.ToString()); 
                return "failed";
            }
        }
        public void register_account(string username, string password)
        {
            MySqlConnection SQLconnection = new MySqlConnection(MyConString);
            SQLconnection.Open();

            string SqlQuery = "INSERT INTO `accounts` "
            + "(`Username`,`Password`,`AccessLevel`,`Membership`,`LastOnlineUtc`) "
            + "VALUES (?username, ?password, ?accesslevel, ?membership, ?lastonlineutc); SELECT LAST_INSERT_ID();";
            MySqlCommand SqlCommand = new MySqlCommand(SqlQuery, SQLconnection);
            SqlCommand.Parameters.AddWithValue("?username", username);
            SqlCommand.Parameters.AddWithValue("?password", password);
            SqlCommand.Parameters.AddWithValue("?accesslevel", "1");
            SqlCommand.Parameters.AddWithValue("?membership", "20001");
            SqlCommand.Parameters.AddWithValue("?lastonlineutc", DateTime.Now.ToString());

            try
            {
                int id = Convert.ToInt32(SqlCommand.ExecuteScalar());
            }
            catch (Exception)//ex)
            {
                //MessageBox.Show(ex.ToString()); 
                SQLconnection.Close();
            }
            SQLconnection.Close();
        }
        #endregion Account Login&Register

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
    
    }
}
