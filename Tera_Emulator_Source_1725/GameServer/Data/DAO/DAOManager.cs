using Data.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Data.DAO
{
    public abstract class DAOManager
    {
        private string ConnectionString = string.Empty;
        public static MySqlConnection Connection;

        public static AccountDAO accountDAO;
        public static GuildDAO guildDAO;
        public static InventoryDAO inventoryDAO;
        public static PlayerDAO playerDAO;
        public static QuestDAO questDAO;
        public static SkillsDAO skillDAO;
        
        public DAOManager(string con)
        {
            this.ConnectionString = con;
            Connection = new MySqlConnection(this.ConnectionString);

            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                Log.ErrorException("Cannot connect to MySQL", ex);
            }
            finally
            {
                Connection.Close();
            }
        }

        public static string MySql_Host
        {
            get
            {
                return (string)Configuration.Database.GetDatabaseHost();
            }
            set
            {
                MySql_Host = (string)Configuration.Database.GetDatabaseHost();
            }

        }
        public static short MySql_Port
        {
            get
            {
                return (short)Convert.ToInt32(Configuration.Database.GetDatabasePort());
            }
            set
            {
                MySql_Port = (short)Convert.ToInt32(Configuration.Database.GetDatabasePort());
            }
        }
        public static string MySql_User
        {
            get
            {
                return (string)Configuration.Database.GetDatabaseUser();
            }
            set
            {
                MySql_User = (string)Configuration.Database.GetDatabaseUser();
            }
        }
        public static string MySql_Password
        {
            get
            {
                return (string)Configuration.Database.GetDatabasePass();
            }
            set
            {
                MySql_Password = (string)Configuration.Database.GetDatabasePass();
            }
        }
        public static string MySql_Database
        {
            get
            {
                return (string)Configuration.Database.GetDatabaseName();
            }
            set
            {
                MySql_Database = (string)Configuration.Database.GetDatabaseName();
            }
        }

        public static void Initialize(string constr)
        {
            accountDAO = new AccountDAO(constr);
            guildDAO = new GuildDAO(constr);
            inventoryDAO = new InventoryDAO(constr);
            playerDAO = new PlayerDAO(constr);
            questDAO = new QuestDAO(constr);
            skillDAO = new SkillsDAO(constr);
        }

        public byte[] HexToBytes(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public string BytesToHex(byte[] bytes)
        {
            return (bytes != null) ? BitConverter.ToString(bytes).Replace("-", "") : "";
        }
    }
}
