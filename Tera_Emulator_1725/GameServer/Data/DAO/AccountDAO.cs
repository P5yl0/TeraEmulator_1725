using Configuration;
using Data.DAO;
using Data.Structures.Account;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Data.DAO
{
    public class AccountDAO : DAOManager
    {
        private MySqlConnection AccountDAOConnection;

        public AccountDAO(string conStr) : base(conStr)
        {            
            Stopwatch stopwatch = Stopwatch.StartNew();
            AccountDAOConnection = new MySqlConnection(conStr);
            AccountDAOConnection.Open();

            stopwatch.Stop();
            Log.Info("DAO: AccountDAO Initialized with {0} Accounts in {1}s"
            , LoadTotalAccounts()
            , (stopwatch.ElapsedMilliseconds / 1000.0).ToString("0.00"));
        }

        public Account LoadAccount(string username)
        {
                string SqlQuery = "SELECT * FROM `accounts` WHERE `username` = ?username";
                MySqlCommand SqlCommand = new MySqlCommand(SqlQuery, AccountDAOConnection);
                SqlCommand.Parameters.AddWithValue("?username", username);
                MySqlDataReader AccountReader = SqlCommand.ExecuteReader();

                Account acc = new Account();
                if (AccountReader.HasRows)
                {
                    while (AccountReader.Read())
                    {
                        acc.AccountId = AccountReader.GetInt32(0);
                        acc.Username = AccountReader.GetString(1);
                        acc.Password = AccountReader.GetString(2);
                        acc.Email = AccountReader.GetString(3);
                        acc.AccessLevel = (byte)AccountReader.GetInt32(4);
                        acc.Membership = (byte)AccountReader.GetInt32(5);
                        acc.isGM = AccountReader.GetBoolean(6);
                        acc.LastOnlineUtc = AccountReader.GetInt64(7);
                        acc.Coins = (int)AccountReader.GetInt32(8);
                        acc.Ip = AccountReader.GetString(9);
                        acc.EmailVerify = AccountReader.GetString(10);
                        acc.FirstName = AccountReader.GetString(11);
                        acc.LastName = AccountReader.GetString(12);
                        acc.PasswordRecovery = AccountReader.GetString(13);

                    }
                }
                AccountReader.Close();
                return (acc.Username == "") ? null : acc;
        }

        public int LoadTotalAccounts()
        {
            string SqlQuery = "SELECT COUNT(*) FROM `accounts`";
            MySqlCommand SqlCommand = new MySqlCommand(SqlQuery, AccountDAOConnection);
            MySqlDataReader AccountReader = SqlCommand.ExecuteReader();

            int count = 0;
            try
            {
                while (AccountReader.Read())
                {
                    count = AccountReader.GetInt32(0);
                }
                AccountReader.Close();
                return count;
            }
            catch (Exception ex)
            {
                Log.ErrorException("DAO: LOAD TOTAL ACCOUNTS ERROR!", ex);
            }
            return count;
        }

        public bool SaveAccount(Account account)
        {
            string SqlQuery = "INSERT INTO `accounts` (`username`,`password`) VALUES(?username, ?password);";
            MySqlCommand SqlCommand = new MySqlCommand(SqlQuery, AccountDAOConnection);
            SqlCommand.Parameters.AddWithValue("?username", account.Username);
            SqlCommand.Parameters.AddWithValue("?password", account.Password);
            try
            {
                SqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorException("DAO: SAVE ACCOUNT ERROR!", ex);
            }
            return false;
        }

    }
}
