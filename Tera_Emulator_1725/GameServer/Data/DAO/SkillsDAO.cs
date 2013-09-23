using Data.Structures.Player;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Utils;

namespace Data.DAO
{
    public class SkillsDAO : DAOManager
    {
        private MySqlConnection SkillsDAOConnection;

        public SkillsDAO(string conStr) : base(conStr)
        {
            SkillsDAOConnection = new MySqlConnection(conStr);
            SkillsDAOConnection.Open();
            Log.Info("DAO: SkillsDAO Initialized.");
        }

        public void SaveSkill(Player player, int SkillId)
        {
            string cmdString = "SELECT * FROM skills WHERE SkillId=?sid AND PlayerId=?pid";
            MySqlCommand command = new MySqlCommand(cmdString, SkillsDAOConnection);
            command.Parameters.AddWithValue("?sid", SkillId);
            command.Parameters.AddWithValue("?pid", player.Id);
            MySqlDataReader SkillDAOreader = command.ExecuteReader();
            bool isExists = SkillDAOreader.HasRows;
            SkillDAOreader.Close();

            if (!isExists)
            {
                cmdString = "INSERT INTO skills (PlayerId, SkillId) VALUES (?pid, ?sid)";
                command = new MySqlCommand(cmdString, SkillsDAOConnection);
                command.Parameters.AddWithValue("?sid", SkillId);
                command.Parameters.AddWithValue("?pid", player.Id);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Log.ErrorException("SaveQuest Error", ex);
                }
            }
        }

        public void SaveSkills(Player player)
        {
            foreach (var skill in player.Skills)
            {
                SaveSkill(player, skill);
            }
        }

        public List<int> LoadSkills(Player player)
        {
            string cmdString = "SELECT * FROM skills WHERE PlayerId=?pid";
            MySqlCommand command = new MySqlCommand(cmdString, SkillsDAOConnection);
            command.Parameters.AddWithValue("?pid", player.Id);
            MySqlDataReader SkillDAOreader = command.ExecuteReader();

            List<int> list = new List<int>();
            if (SkillDAOreader.HasRows)
            {
                while (SkillDAOreader.Read())
                {
                    list.Add(SkillDAOreader.GetInt32(2));
                }
            }
            SkillDAOreader.Close();

            return list;
        }
    }
}
