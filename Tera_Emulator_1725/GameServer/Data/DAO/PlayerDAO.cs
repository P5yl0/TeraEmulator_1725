using Data.Enums;
using Data.Structures.Player;
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
    public class PlayerDAO : DAOManager
    {
        private MySqlConnection PlayerDAOConnection;

        public PlayerDAO(string conStr) : base(conStr)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            PlayerDAOConnection = new MySqlConnection(conStr);
            PlayerDAOConnection.Open();

            stopwatch.Stop();
            Log.Info("DAO: PlayerDAO Initialized with {0} Players in {1}s"
            , LoadTotalPlayers()
            , (stopwatch.ElapsedMilliseconds / 1000.0).ToString("0.00"));

        }

        public List<Player> LoadAccountPlayers(string accName)
        {
            string SqlQuery = "SELECT * FROM `character` WHERE AccountName=?username AND deleted = ?delete";
            MySqlCommand SqlCommand = new MySqlCommand(SqlQuery, PlayerDAOConnection);
            SqlCommand.Parameters.AddWithValue("?username", accName);
            SqlCommand.Parameters.AddWithValue("?delete", 0);
            MySqlDataReader AccountReader = SqlCommand.ExecuteReader();

            List<Player> players = new List<Player>();
            if (AccountReader.HasRows)
            {
                while (AccountReader.Read())
                {
                    Player player = new Player()
                    {
                        Id = AccountReader.GetInt32(0),
                        AccountName = AccountReader.GetString(1),
                        PlayerLevel = AccountReader.GetInt32(2),
                        PlayerExp = AccountReader.GetInt64(3),
                        ExpRecoverable = AccountReader.GetInt64(4),
                        PlayerMount = AccountReader.GetInt32(5),
                        UiSettings = (AccountReader.GetString(6) != null) ? HexToBytes(AccountReader.GetString(6)) : new byte[0],
                        PlayerGuildAccepted = (byte)AccountReader.GetInt16(7),
                        PraiseGiven = (byte)AccountReader.GetInt16(8),
                        LastPraise = AccountReader.GetInt32(9),
                        PlayerCurrentBankSection = AccountReader.GetInt32(10),
                        CreationDate = AccountReader.GetInt32(11),
                        LastOnlineUtc = AccountReader.GetInt32(12)
                    };
                    players.Add(player);
                }
            }

            AccountReader.Close();

            foreach (var player in players)
            {
                SqlQuery = "SELECT * FROM character_data WHERE PlayerId=?id";
                SqlCommand = new MySqlCommand(SqlQuery, PlayerDAOConnection);
                SqlCommand.Parameters.AddWithValue("?id", player.Id);
                AccountReader = SqlCommand.ExecuteReader();
                if (AccountReader.HasRows)
                {
                    while (AccountReader.Read())
                    {

                        player.PlayerData = new PlayerData()
                        {
                            Name = AccountReader.GetString(1),
                            Gender = (Gender)Enum.Parse(typeof(Gender), AccountReader.GetString(2)),
                            Race = (Race)Enum.Parse(typeof(Race), AccountReader.GetString(3)),
                            Class = (PlayerClass)Enum.Parse(typeof(PlayerClass), AccountReader.GetString(4)),
                            Data = HexToBytes(AccountReader.GetString(5)),
                            Details = HexToBytes(AccountReader.GetString(6)),
                        };

                        player.Position = new Structures.World.WorldPosition()
                        {
                            MapId = AccountReader.GetInt32(7),
                            X = AccountReader.GetFloat(8),
                            Y = AccountReader.GetFloat(9),
                            Z = AccountReader.GetFloat(10),
                            Heading = AccountReader.GetInt16(11)
                        };
                    }
                }

                AccountReader.Close();
            }

            return players;
        }

        public int SaveNewPlayer(Player player)
        {
            string SqlQuery = "INSERT INTO `character` "
            + "(`AccountName`,`Level`,`Exp`,`ExpRecoverable`,`Mount`,`UiSettings`,`GuildAccepted`,`PraiseGiven`,`LastPraise`,`CurrentBankSection`,`CreationDate`, `deleted`) "
            + "VALUES (?accname, ?lvl, ?exp, ?exprev, ?mount, ?uiset, ?gaccept, ?praisgive, ?lastpraise, ?bank, ?credate, ?delete); SELECT LAST_INSERT_ID();";
            MySqlCommand SqlCommand = new MySqlCommand(SqlQuery, PlayerDAOConnection);
            SqlCommand.Parameters.AddWithValue("?accname", player.AccountName.ToLower());
            SqlCommand.Parameters.AddWithValue("?lvl", player.PlayerLevel);
            SqlCommand.Parameters.AddWithValue("?exp", player.PlayerExp);
            SqlCommand.Parameters.AddWithValue("?exprev", player.ExpRecoverable);
            SqlCommand.Parameters.AddWithValue("?mount", player.PlayerMount);
            SqlCommand.Parameters.AddWithValue("?uiset", BytesToHex(player.UiSettings));
            SqlCommand.Parameters.AddWithValue("?gaccept", player.PlayerGuildAccepted);
            SqlCommand.Parameters.AddWithValue("?praisgive", player.PraiseGiven);
            SqlCommand.Parameters.AddWithValue("?lastpraise", player.LastPraise);
            SqlCommand.Parameters.AddWithValue("?bank", player.PlayerCurrentBankSection);
            SqlCommand.Parameters.AddWithValue("?credate", player.CreationDate);
            SqlCommand.Parameters.AddWithValue("?delete", 0);

            int id;
            try
            {
                id = Convert.ToInt32(SqlCommand.ExecuteScalar());
            }
            catch (MySqlException ex)
            {
                Log.ErrorException("DAO: PLAYER SAVE ERROR!", ex);
                return 0;
            }

            SqlQuery = "INSERT INTO character_data "
            + "(`PlayerId`,`Name`,`Gender`,`Race`,`PlayerClass`,`Data`,`Details`,`MapId`,`X`,`Y`,`Z`,`H`) "
            + "VALUES (?id, ?name, ?gender, ?race, ?class, ?data, ?details, ?mapid, ?x, ?y, ?z, ?h);";
            SqlCommand = new MySqlCommand(SqlQuery, PlayerDAOConnection);
            SqlCommand.Parameters.AddWithValue("?id", id);
            SqlCommand.Parameters.AddWithValue("?name", player.PlayerData.Name);
            SqlCommand.Parameters.AddWithValue("?gender", player.PlayerData.Gender.ToString());
            SqlCommand.Parameters.AddWithValue("?race", player.PlayerData.Race.ToString());
            SqlCommand.Parameters.AddWithValue("?class", player.PlayerData.Class.ToString());
            SqlCommand.Parameters.AddWithValue("?data", BytesToHex(player.PlayerData.Data));
            SqlCommand.Parameters.AddWithValue("?details", BytesToHex(player.PlayerData.Details));
            SqlCommand.Parameters.AddWithValue("?mapid", player.Position.MapId);
            SqlCommand.Parameters.AddWithValue("?x", player.Position.X);
            SqlCommand.Parameters.AddWithValue("?y", player.Position.Y);
            SqlCommand.Parameters.AddWithValue("?z", player.Position.Z);
            SqlCommand.Parameters.AddWithValue("?h", player.Position.Heading);

            try
            {
                SqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Log.ErrorException("DAO: PLAYER DATA SAVE ERROR!", ex);
                return 0;
            }

            return id;
        }

        public void RemovePlayer(int playerId)
        {
            string SqlQuery = "UPDATE `character` SET `deleted` = 1 WHERE `id` = ?pid";
            MySqlCommand SqlCommand = new MySqlCommand(SqlQuery, PlayerDAOConnection);
            SqlCommand.Parameters.AddWithValue("?pid", playerId);

            try
            {
                Log.Info("Player Reqlinquished On PID : {0}", playerId);
                SqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.ErrorException("DAO: PLAYER REMOVE ERROR!", ex);
            }
        }

        public void UpdatePlayer(Player player)
        {
            string SqlQuery = "UPDATE `character` SET"
            + "`AccountName`=?accname,`Level`=?lvl,`Exp`=?exp,`ExpRecoverable`=?exprev,`Mount`=?mount,`UiSettings`=?uiset,`GuildAccepted`=?gaccept,`PraiseGiven`=?praisgive,`LastPraise`=?lastpraise,`CurrentBankSection`=?bank,`CreationDate`=?credate "
            + "WHERE Id=?id";
            MySqlCommand SqlCommand = new MySqlCommand(SqlQuery, PlayerDAOConnection);
            SqlCommand.Parameters.AddWithValue("?accname", player.AccountName.ToLower());
            SqlCommand.Parameters.AddWithValue("?lvl", player.PlayerLevel);
            SqlCommand.Parameters.AddWithValue("?exp", player.PlayerExp);
            SqlCommand.Parameters.AddWithValue("?exprev", player.ExpRecoverable);
            SqlCommand.Parameters.AddWithValue("?mount", player.PlayerMount);
            SqlCommand.Parameters.AddWithValue("?uiset", BytesToHex(player.UiSettings));
            SqlCommand.Parameters.AddWithValue("?gaccept", player.PlayerGuildAccepted);
            SqlCommand.Parameters.AddWithValue("?praisgive", player.PraiseGiven);
            SqlCommand.Parameters.AddWithValue("?lastpraise", player.LastPraise);
            SqlCommand.Parameters.AddWithValue("?bank", player.PlayerCurrentBankSection);
            SqlCommand.Parameters.AddWithValue("?credate", player.CreationDate);
            SqlCommand.Parameters.AddWithValue("?id", player.Id);

            try
            {
                SqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Log.ErrorException("DAO: PLAYER UPDATE ERROR!", ex);
                return;
            }

            SqlQuery = "UPDATE character_data SET "
            + "`Data`=?data,`Details`=?details,`MapId`=?mapid,`X`=?x,`Y`=?y,`Z`=?z,`H`=?h WHERE `PlayerId`=?pid";
            SqlCommand = new MySqlCommand(SqlQuery, PlayerDAOConnection);
            SqlCommand.Parameters.AddWithValue("?data", BytesToHex(player.PlayerData.Data));
            SqlCommand.Parameters.AddWithValue("?details", BytesToHex(player.PlayerData.Details));
            SqlCommand.Parameters.AddWithValue("?mapid", player.Position.MapId);
            SqlCommand.Parameters.AddWithValue("?x", player.Position.X);
            SqlCommand.Parameters.AddWithValue("?y", player.Position.Y);
            SqlCommand.Parameters.AddWithValue("?z", player.Position.Z);
            SqlCommand.Parameters.AddWithValue("?h", player.Position.Heading);
            SqlCommand.Parameters.AddWithValue("?pid", player.Id);

            try
            {
                SqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Log.ErrorException("DAO: PLAYER DATA UPDATE ERROR!", ex);
                return;
            }
        }

        public int LoadTotalPlayers()
        {
            string SqlQuery = "SELECT COUNT(*) FROM `character`";
            MySqlCommand SqlCommand = new MySqlCommand(SqlQuery, PlayerDAOConnection);
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
                Log.ErrorException("DAO: LOAD TOTAL PLAYERS ERROR!", ex);
            }
            return count;
        }

    }
}
