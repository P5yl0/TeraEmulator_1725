using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Data.Structures.Account;
using Data.Structures.Guild;
using Data.Structures.Player;
using ProtoBuf;
using Utils;
using Data.DAO;

namespace Data
{
    public class Cache
    {
        protected static Dictionary<string, Account> Accounts;
        public static Dictionary<int, Guild> Guilds; 

        public static List<string> UsedNames = new List<string>();
        public static List<string> UsedGuildNames = new List<string>();

        public static long LastSaveUts = Funcs.GetCurrentMilliseconds();
        public static long LastBackupUts = Funcs.GetCurrentMilliseconds();

        public static void LoadData()
        {
            LoadAccounts();
            LoadGuilds();
        }

        public static void LoadAccounts()
        {
            if (!File.Exists("cache.bin"))
            {
                Log.Warn("Data: Cache file not found!");
                Accounts = new Dictionary<string, Account>();
                return;
            }
            using (FileStream fs = File.OpenRead("cache.bin"))
            {
                Accounts = Serializer.DeserializeWithLengthPrefix<Dictionary<string, Account>>(fs, PrefixStyle.Fixed32);
            }
            if (Accounts == null)
                Accounts = new Dictionary<string, Account>();

            foreach (KeyValuePair<string, Account> account in Accounts)
            {
                DAOManager.accountDAO.SaveAccount(account.Value);
                foreach (Player player in account.Value.Players)
                {
                    DAOManager.playerDAO.SaveNewPlayer(player);
                    UsedNames.Add(player.PlayerData.Name.ToLower());

                    foreach(var quest in player.Quests.ToList())
                    {
                        DAOManager.questDAO.AddQuest(player, quest.Value);
                    }

                    foreach (var item in player.Inventory.Items)
                    {
                        DAOManager.inventoryDAO.AddItem(player, Enums.Item.StorageType.Inventory, item);
                    }
                }
            }
        }

        public static void LoadGuilds()
        {
            if (!File.Exists("guilds_cache.bin"))
            {
                Log.Warn("Data: guilds_cache file not found!");
                Guilds = new Dictionary<int, Guild>();
                return;
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            using (FileStream fs = File.OpenRead("guilds_cache.bin"))
            {
                Guilds = Serializer.DeserializeWithLengthPrefix<Dictionary<int, Guild>>(fs, PrefixStyle.Fixed32);
            }
            if (Guilds == null)
            {
                Guilds = new Dictionary<int, Guild>();
            }
            
            stopwatch.Stop();

            foreach (KeyValuePair<int, Guild> guild in Guilds)
            {
                UsedGuildNames.Add(guild.Value.GuildName.ToLower());
            }

            Log.Info("Cache: Loaded {0} guild_cache in {1}s - this wil be removed!"
                     , Guilds.Count
                     , (stopwatch.ElapsedMilliseconds / 1000.0).ToString("0.00"));

            RestorePlayerGuilds();

        }

        public static void SaveData()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            if (Funcs.GetCurrentMilliseconds() - Cache.LastBackupUts > 600000) // Backup Every 10 Min
            {
                //removed Cache Backup SaveDir
            }

            //save to cachefile
            using (FileStream fs = File.Create("cache.bin"))
                Serializer.SerializeWithLengthPrefix(fs, Accounts, PrefixStyle.Fixed32);

            using (FileStream fs = File.Create("guilds_cache.bin"))
                Serializer.SerializeWithLengthPrefix(fs, Guilds, PrefixStyle.Fixed32);

            stopwatch.Stop();

            Log.Info("Cache: Saved {0} accounts and {2} guilds in {1}s"
                , Accounts.Count
                , (stopwatch.ElapsedMilliseconds / 1000.0).ToString("0.00"), Guilds.Count);
        }

        public static Account GetAccount(string accountName)
        {
            string key = accountName.ToLower();

            if (!Accounts.ContainsKey(key))
                Accounts.Add(key, new Account {Username = accountName});

            return Accounts[key];
        }

        public static void RestorePlayerGuilds()
        {
            foreach (KeyValuePair<string, Account> account in Accounts)
                foreach (var pl in account.Value.Players)
                    if (pl.GuildIdAndRank.Key != 0 && Guilds.ContainsKey(pl.GuildIdAndRank.Key))
                    {
                        Guilds[pl.GuildIdAndRank.Key].GuildMembers.Add(pl, pl.GuildIdAndRank.Value);
                        pl.Guild = Guilds[pl.GuildIdAndRank.Key];
                    }
        }

        public static int LoadTotalOnlines()
        {
            return Accounts.Where(x => x.Value.IsOnline == true).ToList().Count;
        }
    }
}