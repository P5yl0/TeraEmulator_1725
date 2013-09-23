using Data.Enums;
using Data.Structures.Player;
using Data.Structures.Quest;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Data.DAO
{
    public class QuestDAO : DAOManager
    {
        private MySqlConnection QuestDAOConnection;

        public QuestDAO(string conStr) : base(conStr)
        {
            QuestDAOConnection = new MySqlConnection(conStr);
            QuestDAOConnection.Open();
            Log.Info("DAO: QuestDAO Initialized!");
        }

        public void AddQuest(Player player, QuestData questdata)
        {
            string SQL = "SELECT * FROM `quests` WHERE "
                + "`questid` = ?qid AND `characterid` = ?pid";
            MySqlCommand cmd = new MySqlCommand(SQL, QuestDAOConnection);
            cmd.Parameters.AddWithValue("?qid", questdata.QuestId);
            cmd.Parameters.AddWithValue("?pid", player.Id);
            MySqlDataReader AddQuestReader = cmd.ExecuteReader();
            bool isExists = AddQuestReader.HasRows;
            AddQuestReader.Close();

            if (!isExists)
            {
                SQL = "INSERT INTO `quests` "
                    + "(`characterid`, `questid`, `status`, `step`, `counters`) "
                    + "VALUES (?pid, ?qid, ?status, ?step, ?counter);";
                cmd = new MySqlCommand(SQL, QuestDAOConnection);
                cmd.Parameters.AddWithValue("?pid", player.Id);
                cmd.Parameters.AddWithValue("?qid", questdata.QuestId);
                cmd.Parameters.AddWithValue("?status", questdata.Status.ToString());
                cmd.Parameters.AddWithValue("?step", questdata.Step);
                cmd.Parameters.AddWithValue("?counter", string.Join(",", questdata.Counters));
            }
            else
            {
                SQL = "UPDATE `quests` SET "
                    + "`status` = ?status, `step` = ?step, `counters` = ?counter "
                    + "WHERE `questid` = ?qid AND `characterid` = ?pid";
                cmd = new MySqlCommand(SQL, QuestDAOConnection);
                cmd.Parameters.AddWithValue("?status", questdata.Status.ToString());
                cmd.Parameters.AddWithValue("?step", questdata.Step);
                cmd.Parameters.AddWithValue("?counter", string.Join(",", questdata.Counters));
                cmd.Parameters.AddWithValue("?qid", questdata.QuestId);
                cmd.Parameters.AddWithValue("?pid", player.Id);
            }

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.ErrorException("DAO: ADD QUEST ERROR!", e);
            }

            AddQuestReader.Close();
        }

        public void AddQuests(Player player)
        {
            foreach (var quest in player.Quests.ToList())
            {
                AddQuest(player, quest.Value);
            }
        }

        public QuestData LoadQuest(Player player, int questid)
        {
            string SQL = "SELECT * FROM `quests` WHERE "
                + "`questid` = ?qid AND `characterid` = ?pid";
            MySqlCommand cmd = new MySqlCommand(SQL, QuestDAOConnection);
            cmd.Parameters.AddWithValue("?qid", questid);
            cmd.Parameters.AddWithValue("?pid", player.Id);
            MySqlDataReader LoadQuestReader = cmd.ExecuteReader();

            QuestData quest = new QuestData(questid);
            if (LoadQuestReader.HasRows)
            {
                while (LoadQuestReader.Read())
                {
                    quest = new QuestData(questid)
                    {
                        QuestId = LoadQuestReader.GetInt32(1),
                        Status = (QuestStatus)Enum.Parse(typeof(QuestStatus), LoadQuestReader.GetString(2)),
                        Step = LoadQuestReader.GetInt32(3),
                        Counters = LoadQuestReader.GetString(4).Split(',').Select(n => int.Parse(n)).ToList()
                    };
                }
            }
            LoadQuestReader.Close();

            return quest;
        }

        public Dictionary<int, QuestData> LoadQuests(Player player)
        {
            string SQL = "SELECT * FROM `quests` WHERE `characterid` = ?pid";
            MySqlCommand cmd = new MySqlCommand(SQL, QuestDAOConnection);
            cmd.Parameters.AddWithValue("?pid", player.Id);
            MySqlDataReader LoadQuestsReader = cmd.ExecuteReader();

            Dictionary<int, QuestData> questlist = new Dictionary<int, QuestData>();
            if (LoadQuestsReader.HasRows)
            {
                while (LoadQuestsReader.Read())
                {
                    QuestData quest = new QuestData(0)
                    {
                        QuestId = LoadQuestsReader.GetInt32(2),
                        Status = (QuestStatus)Enum.Parse(typeof(QuestStatus), LoadQuestsReader.GetString(3)),
                        Step = LoadQuestsReader.GetInt32(4),
                        Counters = LoadQuestsReader.GetString(5).Split(',').Select(n => int.Parse(n)).ToList()
                    };
                    questlist.Add(quest.QuestId, quest);
                }
            }
            LoadQuestsReader.Close();

            return questlist;
        }
    }
}
