using System.Collections.Generic;
using System.Threading;
using Data.Enums;
using Data.Enums.Item;
using Data.Interfaces;
using Data.Structures.Quest;
using Data.Structures.World;
using Data.Structures.World.Party;
using Data.Structures.World.Requests;
using Utils;

namespace Data.Structures.Player
{
    [ProtoBuf.ProtoContract]
    public class Player : Creature.Creature
    {
        public int Id;
        public Creature.Creature ObservedCreature = null;
        public IConnection Connection;
        public IController Controller;
        public int PlayerId { get { return UID + 21081990; } }

        [ProtoBuf.ProtoMember(4)]
        public PlayerData PlayerData;
        public IVisible Visible;

        [ProtoBuf.ProtoMember(6)]
        public string AccountName = "";

        [ProtoBuf.ProtoMember(7)]
        public Storage Inventory = new Storage {StorageType = StorageType.Inventory};

        [ProtoBuf.ProtoMember(3)]
        public Storage CharacterWarehouse = new Storage{StorageType = StorageType.CharacterWarehouse, Size = 72*4, Money = 0};

        [ProtoBuf.ProtoMember(8)]
        public List<int> Skills = new List<int>();

        public Dictionary<int, long> SkillCooldowns = new Dictionary<int, long>();
        
        //[ProtoBuf.ProtoMember(42)]
        //public Dictionary<int, long> MountSkills = new Dictionary<int, long>();

        public short MovementByAdminCommand { get; set; }

        public object QuestsLock = new object();

        [ProtoBuf.ProtoMember(11)]
        public Dictionary<int, QuestData> Quests = new Dictionary<int, QuestData>();

        public Dialog CurrentDialog;

        public int PlayerMount;
 
        public Npc.Npc Pet;

        [ProtoBuf.ProtoMember(36)]
        public byte[] UiSettings;

        public PlayerMode PlayerMode = PlayerMode.Normal;

        public Queue<Creature.Creature> MarkedCreatures;

        public List<Request> Requests = new List<Request>();

        public ManualResetEvent TeleportLoadMapEvent;

        public Guild.Guild Guild;

        // 0 - no result, 1 - accepted, 2 - declined
        public byte PlayerGuildAccepted = 0;

        // How much praise player has given already
        public byte PraiseGiven = 0;

        // when the last praise was given
        public int LastPraise = -1;

        public WorldPosition ClosestBindPoint = null;

        public int PlayerCurrentBankSection = 0;

        public System.Timers.Timer SystemWindowsTimer;

        public List<string> BlockersInChat = new List<string>();

        public List<Player> Friends = new List<Player>();

        [ProtoBuf.ProtoMember(40)]
        public Dictionary<int, long> ItemCoodowns = new Dictionary<int, long>(); //CooldownGroup => Milliseconds

        [ProtoBuf.ProtoMember(37)]
        private PlayerCraftStats _playerCraftStats;

        public PlayerCraftStats PlayerCraftStats
        {
            get { return _playerCraftStats ?? (_playerCraftStats = new PlayerCraftStats(1)); }
        }

        public bool Debug;

        [ProtoBuf.ProtoMember(39)]
        public int CreationDate = Funcs.GetRoundedUtc();

        [ProtoBuf.ProtoMember(38)]
        public int LastOnlineUtc = Funcs.GetRoundedUtc();

        [ProtoBuf.ProtoMember(41)]
        public byte[] ZoneDatas;

        public int PlayerLevel = 1;

        [ProtoBuf.ProtoMember(34)]
        public long PlayerExp;

        [ProtoBuf.ProtoMember(35)]
        public List<int> Recipes = new List<int>(); 

        public long ExpRecoverable;

        public Party Party;

        public KeyValuePair<int, int> GuildIdAndRank;

        public KeyValuePair<int, int> CurrentEmotion = new KeyValuePair<int, int>();

        public Duel Duel;

        public long GetExpShown()
        {
            if (PlayerLevel == Data.PlayerExperience.Count - 1)
                return Data.PlayerExperience[PlayerLevel];
            return PlayerExp - Data.PlayerExperience[PlayerLevel - 1];
        }

        public long GetExpNeed()
        {
            if (PlayerLevel == Data.PlayerExperience.Count - 1)
                return Data.PlayerExperience[PlayerLevel];
            return Data.PlayerExperience[PlayerLevel] - Data.PlayerExperience[PlayerLevel - 1];
        }

        public override int GetLevel()
        {
            return PlayerLevel;
        }

        public override int GetModelId()
        {
            return PlayerData.SexRaceClass;
        }

        public override void Release()
        {
            ObservedCreature = null;

            Inventory.Release();
            Inventory = null;

            PlayerData = null;

            try
            {
                Visible.Stop();
                Visible.Release();
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
                //Already stoped
            }
            Visible = null;

            Skills = null;

            Quests = null;

            if (CurrentDialog != null)
                CurrentDialog.Release();

            if (Pet != null)
                Pet.Release();
            Pet = null;

            MarkedCreatures = null;

            TeleportLoadMapEvent = null;

            try
            {
                SystemWindowsTimer.Stop();
                SystemWindowsTimer.Dispose();
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }
            SystemWindowsTimer = null;

            BlockersInChat = null;

            Friends = null;

            base.Release();
        }

        public void ReleaseUniqueIds()
        {
            Inventory.ReleaseUniqueIds();

            ReleaseUniqueId();
        }

        [ProtoBuf.ProtoMember(100)]
        public AchivesStats AchivesStats = new AchivesStats();

        #region PROTOHOOKS
        // ReSharper disable UnusedMember.Local

        [ProtoBuf.ProtoMember(101)]
        private WorldPosition PositionHook
        {
            get { return Position; }
            set { Position = value; }
        }

        [ProtoBuf.ProtoMember(102)]
        public KeyValuePair<int, int> GuildIdAndRankHook
        {
            get
            {
                if (Guild != null) return new KeyValuePair<int, int>(Guild.GuildId, Guild.GuildMembers[this]);
                return new KeyValuePair<int, int>();
            }
            set { GuildIdAndRank = value; }
        }

        public int TemplateId
        {
            get { return PlayerData.SexRaceClass; }
        }

        // ReSharper restore UnusedMember.Local
        #endregion
    }
}