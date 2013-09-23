using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utils;

namespace Configuration
{
    class Server
    {
        //server
        public static int GetServerMode(int defaultValue = 0)
        {
            int serverMode = defaultValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/server/serverConfig.xml");
                XmlNode node = doc.SelectSingleNode("server_configuration/server_Mode");
                serverMode = Convert.ToInt32(node.InnerText);
            }
            catch (Exception /*ex*/)
            {
                Log.Error("Config: server_Mode NOT FOUND!!!");
            }
            return serverMode;
        }
        public static String GetWelcomeMessage(String defaultValue = "Welcome")
        {
            String welcomeMessage = defaultValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/server/serverConfig.xml");
                XmlNode node = doc.SelectSingleNode("server_configuration/welcome_Message");
                welcomeMessage = node.InnerText;
            }
            catch (Exception /*ex*/)
            {
                Log.Error("Config: welcome_Message NOT FOUND!!!");
            }
            return welcomeMessage;
        }
    }

    class Database
    {
        //database
        public static string GetDatabaseHost()
        {
            string dbHost = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/network/databaseConfig.xml");
                XmlNode node = doc.SelectSingleNode("database_configuration/database_Host");
                dbHost = node.InnerText;
            }
            catch (Exception /*ex*/)
            {
                Log.Error("Config: Database host NOT FOUND!!!");
            }
            return dbHost;
        }
        public static string GetDatabasePort()
        {
            string dbPort = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/network/databaseConfig.xml");
                XmlNode node = doc.SelectSingleNode("database_configuration/database_Port");
                dbPort = node.InnerText;
            }
            catch (Exception /*ex*/)
            {
                //Log.Error("Config: Database port NOT FOUND!!!");
            }
            return dbPort;
        }
        public static string GetDatabaseUser()
        {
            string dbUser = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/network/databaseConfig.xml");
                XmlNode node = doc.SelectSingleNode("database_configuration/database_User");
                dbUser = node.InnerText;
            }
            catch (Exception /*ex*/)
            {
                Log.Error("Config: Database user NOT FOUND!!!");
            }
            return dbUser;
        }
        public static string GetDatabasePass()
        {
            string dbPass = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/network/databaseConfig.xml");
                XmlNode node = doc.SelectSingleNode("database_configuration/database_Pass");
                dbPass = node.InnerText;
            }
            catch (Exception /*ex*/)
            {
                Log.Error("Config: Database pass NOT FOUND!!!");
            }
            return dbPass;
        }
        public static string GetDatabaseName()
        {
            string dbName = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/network/databaseConfig.xml");
                XmlNode node = doc.SelectSingleNode("database_configuration/database_Name");
                dbName = node.InnerText;
            }
            catch (Exception /*ex*/)
            {
                Log.Error("Config: Database name NOT FOUND!!!");
            }
            return dbName;
        }
    }

    class Network
    {
        //server network
        public static String GetServerIp(String defaultValue = "127.0.0.1")
        {
            String serverIp = defaultValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/network/networkConfig.xml");
                XmlNode node = doc.SelectSingleNode("network_configuration/network_IP");
                serverIp = node.InnerText;
            }
            catch (Exception /*ex*/)
            {
                Log.Error("Config: Network_IP NOT FOUND!!!");
            }
            return serverIp;
        }
        public static int GetServerPort(int defaultValue = 11101)
        {
            int serverPort = defaultValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/network/networkConfig.xml");
                XmlNode node = doc.SelectSingleNode("network_configuration/network_Port");
                serverPort = Convert.ToInt32(node.InnerText);
            }
            catch (Exception /*ex*/)
            {
                Log.Error("Config: Network_Port NOT FOUND!!!");
            }
            return serverPort;
        }
        public static int GetServerMaxCon(int defaultValue = 100)
        {
            int serverMaxConnections = defaultValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/network/networkConfig.xml");
                XmlNode node = doc.SelectSingleNode("network_configuration/network_MaxCon");
                serverMaxConnections = Convert.ToInt32(node.InnerText);
            }
            catch (Exception /*ex*/)
            {
                Log.Error("Config: Network_MaxCon NOT FOUND!!!");
            }
            return serverMaxConnections;
        }
    }

    class GamePlay
    {
        //gameplay
        public static int GetLevelCap(int defaultValue = 60)
        {
            int levelCap = defaultValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/server/serverConfig.xml");
                XmlNode node = doc.SelectSingleNode("server_configuration/level_Cap");
                levelCap = Convert.ToInt32(node.InnerText);
            }
            catch (Exception /*ex*/)
            {
                Log.Error("Config: level_Cap NOT FOUND!!!");
            }
            return levelCap;
        }
        public static int GetServerRates(int defaultValue = 1)
        {
            int serverRates = defaultValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/server/serverConfig.xml");
                XmlNode node = doc.SelectSingleNode("server_configuration/server_Rates");
                serverRates = Convert.ToInt32(node.InnerText);
            }
            catch (Exception /*ex*/)
            {
                Log.Error("Config: server_Rates NOT FOUND!!!");
            }
            return serverRates;
        }
    }

}
