using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Communication;
using Communication.Interfaces;
using Communication.Logic;
using Data;
using Data.Enums;
using Data.Enums.Player;
using Data.Interfaces;
using Data.Structures;
using Data.Structures.Gather;
using Data.Structures.Geometry;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.World;
using Network.Server;
using Tera.Controllers;
using Tera.Extensions;
using Tera.Structures;
using Utils;
using Data.DAO;
using Data.Structures.Account;
using Data.Enums.Item;

namespace Tera.Services
{
    class PlayerService : IPlayerService
    {
        public static List<Player> PlayersOnline = new List<Player>();

        public List<Player> GetOnline()
        {
            return PlayersOnline;
        }

        public void Send(ISendPacket packet)
        {
            PlayersOnline.Each(player => packet.Send(player.Connection));
        }


        public void InitPlayer(Player player, bool isProlog)
        {
            player.PlayerLevel = 1;
            while ((player.PlayerLevel + 1) != Data.Data.PlayerExperience.Count - 1
                && player.PlayerExp >= Data.Data.PlayerExperience[player.PlayerLevel])
                player.PlayerLevel++;

            if (player.Skills.Count == 0)
                for (int i = 0; i < Data.Data.DefaultSkillSets[player.TemplateId].SkillSet.Count; i++)
                    player.Skills.Add(Data.Data.DefaultSkillSets[player.TemplateId].SkillSet[i]);

            player.GameStats = CreatureLogic.InitGameStats(player);
            CreatureLogic.UpdateCreatureStats(player);

            AiLogic.InitAi(player);

            PlayersOnline.Add(player);
        }

        public List<Player> OnAuthorized(Account account)
        {
            var list = DAOManager.playerDAO.LoadAccountPlayers(account.Name);

            foreach (var player in list)
            {
                player.Inventory = DAOManager.inventoryDAO.LoadStorage(player, StorageType.Inventory);
                player.CharacterWarehouse = DAOManager.inventoryDAO.LoadStorage(player, StorageType.CharacterWarehouse);
                player.Quests = DAOManager.questDAO.LoadQuests(player);
                player.Skills = DAOManager.skillDAO.LoadSkills(player);
                //player.Guild = DAOManager.guildDAO.LoadPlayerGuild(player);

                if (account.isGM.Equals(true))
                { player.PlayerData.IsGM = true; }

            }
            return list;
        }

        public void PlayerEnterWorld(Player player)
        {
            new SpSystemNotice(Configuration.Server.GetWelcomeMessage(), 15).Send(player.Connection);
            Communication.Global.MountService.PlayerEnterWorld(player);
        }

        public void PlayerEndGame(Player player)
        {
            player.LastOnlineUtc = Funcs.GetRoundedUtc();

            if (player.Ai != null)
            {
                player.Ai.Release();
                player.Ai = null;
            }

            PlayersOnline.Remove(player);

            DAOManager.inventoryDAO.SaveStorage(player, player.Inventory);
            DAOManager.playerDAO.UpdatePlayer(player);
            DAOManager.questDAO.AddQuests(player);
            DAOManager.skillDAO.SaveSkills(player);
        }

        public CheckNameResult CheckName(string name, short type)
        {
            if (name.Length < 2)
                return CheckNameResult.MinimumLengthIs2;

            if (name.Length > 16)
                return CheckNameResult.MaximumLengthIs16;

            for (int i = 0; i < name.Length; i++)
                if (name[i] == ' ')
                    return CheckNameResult.YouCantUseSpacesInCharacterName;

            if (!Regex.IsMatch(name, "^[a-z]|[a-z].[a-z]+$", RegexOptions.IgnoreCase))
                return CheckNameResult.UnavaliableLatter;

            if (Cache.UsedNames.Contains(name.ToLower()))
                return CheckNameResult.ThisSsNotAcceptableCharacterName;

            return CheckNameResult.Ok;
        }

        public bool CheckNameForUse(string name, short type)
        {
            //TODO:

            return true;
        }

        public Player CreateCharacter(IConnection connection, PlayerData playerData)
        {
            Player player = new Player
            {
                PlayerData = playerData,
                AccountName = connection.Account.Name,
                Position = new WorldPosition
                {
                    MapId = 13,
                    X = 93492.0F,
                    Y = -88216.0F,
                    Z = -4523.0F,
                    Heading = unchecked((short)0x8000),
                },
            };

            player.Id = DAOManager.playerDAO.SaveNewPlayer(player);
            connection.Account.Players.Add(player);
            return player;
        }

        public void PlayerMoved(Player player, float x1, float y1, float z1, short heading, float x2, float y2, float z2, PlayerMoveType moveType, short unk2, short unk3)
        {
            player.Position.X = x1;
            player.Position.Y = y1;
            player.Position.Z = z1;
            player.Position.Heading = heading;

            if (player.Instance != null)
                player.Instance.OnMove(player);
        }

        public void StartDialog(Player player, TeraObject o)
        {
            //todo previous controller check
            Npc npc = o as Npc;
            if (npc != null)
                Global.ControllerService.SetController(player, new DialogController(player, npc));
        }

        public void ProgressDialog(Player player, int selectedIndex, int dialogUid)
        {
            DialogController dialogController = player.Controller as DialogController;
            if (dialogController != null)
                dialogController.Progress(selectedIndex, dialogUid);
        }

        public void PlayerEnterZone(Player player, byte[] zoneData)
        {
            player.ZoneDatas = zoneData;
            new SpCharacterZoneData(zoneData).Send(player.Connection);
            new SpZoneUnkAnswer2(BitConverter.ToInt32(zoneData, 5)).Send(player.Connection);
        }

        public void AddExp(Player player, long value, Npc npc = null)
        {
            value *= Configuration.GamePlay.GetServerRates();

            //todo rate modifiers
            if (player.GetLevel() >= Configuration.GamePlay.GetLevelCap())
            {
                new SpSystemNotice("Sorry, but now, level cap is " + Configuration.GamePlay.GetLevelCap()).Send(player);
                return;
            }

            SetExp(player, player.PlayerExp + value, npc);
        }

        public Player GetPlayerByName(string playerName)
        {
            try
            {
                return PlayersOnline.First(player => player.PlayerData.Name == playerName);
            }
            catch
            {

            }
            return null;
        }

        public void StartGather(Player player, long uid)
        {
            var gather = Uid.GetObject(uid) as Gather;

            if (gather == null)
                return;

            Global.ControllerService.SetController(player, new GatherableController(player, gather));
        }

        public bool IsPlayerOnline(Player player)
        {
            return PlayersOnline.Contains(player);
        }

        public void UnstuckPlayer(Player player)
        {
            Npc nearNpc = null;
            double distance = double.MaxValue;

            player.Instance.Npcs.Each(npc =>
            {
                double dist = npc.Position.FastDistanceTo(player.Position);
                if (dist < distance)
                {
                    distance = dist;
                    nearNpc = npc;
                }
            });

            var pos = TeleportService.IslandOfDawnSpawn;

            if (nearNpc != null)
            {
                pos = Geom.GetNormal((short)Funcs.Random().Next(0, short.MaxValue))
                    .Multiple(150)
                    .Add(nearNpc.Position)
                    .SetZ(nearNpc.Position.Z + 200)
                    .ToWorldPosition();

                pos.MapId = player.Position.MapId;
            }

            Global.TeleportService.ForceTeleport(player, pos);
        }

        public void SetExp(Player player, long add, Npc npc)
        {

            int maxLevel = Data.Data.PlayerExperience.Count - 1;

            long maxExp = Data.Data.PlayerExperience[maxLevel - 1];
            int level = 1;

            if (add > maxExp)
                add = maxExp;

            while ((level + 1) != maxLevel && add >= Data.Data.PlayerExperience[level])
                level++;

            long added = add - player.PlayerExp;

            if (level != player.PlayerLevel)
            {
                player.PlayerLevel = level;
                player.PlayerExp = add;
                PlayerLogic.LevelUp(player);
            }
            else
                player.PlayerExp = add;

            new SpUpdateExp(player, added, npc).Send(player.Connection);
        }

        #region NpcTraid

        public void RemoveSellItemsFromNpcTrade(Player player, int itemId, int itemCount, int dialogUid)
        {
            TradeController tradeController = player.Controller as TradeController;
            if (tradeController != null)
                tradeController.RemoveFromSellItems(itemId, itemCount);
        }

        public void CompleteNpcTraid(Player player, int dialogUid)
        {
            TradeController tradeController = player.Controller as TradeController;
            if (tradeController != null)
                tradeController.CompleteTraid(false);
        }

        public void InterruptNpcTraid(Player player)
        {
            TradeController tradeController = player.Controller as TradeController;
            if (tradeController != null)
                tradeController.CompleteTraid(true);
        }

        public void AddItemsToNpcSell(Player player, int itemId, int itemCounter, int slot)
        {
            TradeController tradeController = player.Controller as TradeController;
            if (tradeController != null)
                tradeController.SellItem(itemId, itemCounter, slot);
        }

        public void AddItemsToNpcBuy(Player player, int itemId, int itemCounter)
        {
            TradeController tradeController = player.Controller as TradeController;
            if (tradeController != null)
                tradeController.BuyItem(itemId, itemCounter);
        }

        public void RemoveBuyItemsFromNpcTrade(Player player, int itemId, int itemCount, int dialogUid)
        {
            TradeController tradeController = player.Controller as TradeController;
            if (tradeController != null)
                tradeController.RemoveFromBuyItems(itemId, itemCount);
        }

        #endregion

        public void Action()
        {
            for (int i = 0; i < PlayersOnline.Count; i++)
            {
                try
                {
                    if (PlayersOnline[i].Ai != null)
                        PlayersOnline[i].Ai.Action();

                    if (PlayersOnline[i].Visible != null)
                        PlayersOnline[i].Visible.Update();

                    if (PlayersOnline[i].Controller != null)
                        PlayersOnline[i].Controller.Action();
                }
                // ReSharper disable EmptyGeneralCatchClause
                catch
                // ReSharper restore EmptyGeneralCatchClause
                {
                    //Collection modified
                }

                if ((i & 511) == 0) // 2^N - 1
                    Thread.Sleep(1);
            }
        }
    }
}