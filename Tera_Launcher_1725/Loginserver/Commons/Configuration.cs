using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Configuration
{
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
                //Log.Error("Config: Database host NOT FOUND!!!");
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
                //Log.Error("Config: Database user NOT FOUND!!!");
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
                //Log.Error("Config: Database pass NOT FOUND!!!");
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
                //Log.Error("Config: Database name NOT FOUND!!!");
            }
            return dbName;
        }
    }

    class Network
    {
        //loginserver network
        public static String GetLoginServerIp(String defaultValue = "127.0.0.1")
        {
            String serverIp = defaultValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/network/loginserverNetworkConfig.xml");
                XmlNode node = doc.SelectSingleNode("network_configuration/network_IP");
                serverIp = node.InnerText;
            }
            catch (Exception /*ex*/)
            {
                //Log.Error("Config: Network_IP NOT FOUND!!!");
            }
            return serverIp;
        }
        public static Int32 GetLoginServerPort(Int32 defaultValue = 2101)
        {
            int serverPort = defaultValue;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config/network/loginserverNetworkConfig.xml");
                XmlNode node = doc.SelectSingleNode("network_configuration/network_Port");
                serverPort = Convert.ToInt32(node.InnerText);
            }
            catch (Exception /*ex*/)
            {
                //Log.Error("Config: Network_Port NOT FOUND!!!");
            }
            return serverPort;
        }
    }

}
