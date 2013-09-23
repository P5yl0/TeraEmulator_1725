using System;
using System.Collections.Generic;
using System.Threading;
using Communication;
using Communication.Interfaces;
using Data.Enums;
using Data.Enums.Player;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Gather;
using Data.Structures.Npc;
using Data.Structures.Objects;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using Data.Structures.World;
using Data.Structures.World.Requests;
using Network;
using Network.Server;
using Tera.AiEngine;
using Tera.Controllers;
using Tera.Extensions;
using Utils;

namespace Tera.Services
{
    class FeedbackService : Global, IFeedbackService
    {
        public void ShowShutdownTicks()
        {
            for (int i = 60; i != 0; i--)
            {
                if (GameServer.TcpServer.Server.Clients.Count == 0)
                    break;

                if (i < 10 || i % 10 == 0)
                {
                    PlayerService.Send(
                        new SpSystemNotice("Attention! Server will be restarted after " + i + "sec!", i > 10 ? 5 : 1));

                    PlayerService.Send(
                        new SpChatMessage("Attention! Server will be restarted after " + i + "sec!", ChatType.System));
                }

                Console.WriteLine("!Server will be shutdown! Remaining seconds: " + i);
                Thread.Sleep(1000);
            }
        }

        public void OnCheckVersion(IConnection connection, int version)
        {
            new SpSendVersion(version).Send(connection);
        }

        public void OnAuthorized(IConnection connection)
        {
            //1725 EU
            new SendPacket("C06100000000D92C0200").Send(connection);
            new SendPacket("92B10100000001000000000001").Send(connection);
        }

        public void SendPlayerList(IConnection connection)
        {
            new SpSendCharacterList(connection.Account).Send(connection);
            if (connection.Account.UiSettings != null)
                new SpUISettings(connection.Account.UiSettings).Send(connection);
        }

        public void SendCheckNameResult(IConnection connection, string name, short type, CheckNameResult result)
        {
            new SpCharacterCheckNameResult(result.GetHashCode(), name, type).Send(connection);
        }

        public void SendCheckNameForUseResult(IConnection connection, string name, short type, bool result)
        {
            //1725 EU
            if (result)
                new SendPacket("525A01").Send(connection);
            else
                new SendPacket("525A00").Send(connection);
        }

        public void SendCreateCharacterResult(IConnection connection, bool result)
        {
            new SpCharacterCreateResult((byte)(result ? 1 : 0)).Send(connection);
        }

        public void SendInitialData(IConnection connection)
        {
            new SendPacket("FBB3010000000000000000").Send(connection); //1725 EU
            new SendPacket("F4B60000000000000000000000000000D49E9F4F00000000").Send(connection); //1725 EU

            new SpCharacterInit(connection.Player).Send(connection);

            new SpInventory(connection.Player).Send(connection);
            new SpSkillList(connection.Player).Send(connection);

            new SendPacket("F3AD").Send(connection); //1725 EU

            QuestEngine.ResendQuestData(connection.Player);

            new SpCharacterCraftStats(connection.Player).Send(connection);

            new SendPacket("07730500160001000000000000003C000000000016002200000000000000000022002E00FFFFFFFF000000002E003A00FFFFFFFF000000003A004600FFFFFFFF0000000046000000FFFFFFFF00000000").Send(connection); //1725 EU physical abnormals
            new SendPacket("8CD8").Send(connection); //1725 EU
            new SendPacket("16770000000000000000").Send(connection); //1725 EU

            new SendPacket("BC5500004843").Send(connection); //1725 EU
            new SendPacket("E0C406000000").Send(connection); //1725 EU
        }

        public void SendBindPoint(IConnection connection)
        {
            connection.Player.ClosestBindPoint = GetNearestBindPoint(connection.Player);
            new SpCharacterBind(connection.Player).Send(connection);
        }

        public void ExpChanged(Player player, long added)
        {
            new SpUpdateExp(player, added).Send(player.Connection);
        }

        public void OnPlayerEnterWorld(IConnection connection, Player player)
        {
            string uidHex = BitConverter.GetBytes(player.UID).ToHex()
                + BitConverter.GetBytes(ObjectFamily.Player.GetHashCode()).ToHex();

            new SendPacket("8FCD00000000").Send(connection); //1725 EU
            new SendPacket("0D9600000000").Send(connection); //1725 EU

            //new SpFriendList(pState.Player.Friends).Send(connection);
            //new SpFriendUpdate(pState.Player.Friends).Send(connection);

            new SendPacket("A7F20000000000000000").Send(connection); //1725 EU
            new SendPacket("03E70000000000000000").Send(connection); //1725 EU

            new SpCharacterPosition(player).Send(connection);

            FlyController flyController = player.Controller as FlyController;
            if (flyController != null)
                flyController.EndFly(player.Position.MapId);

            new SendPacket("E1EB" + uidHex + "7CC4000001FFFFFF7F").Send(connection); //1725 EU

            new SpCharacterStats(player).Send(connection);
            new SpCharacterGatherstats(player.PlayerCraftStats).Send(connection);

            CraftService.UpdateCraftRecipes(player);

            player.LastOnlineUtc = Funcs.GetRoundedUtc();
        }

        public void SendCreatureInfo(IConnection connection, Creature creature)
        {
            var player = creature as Player;
            if (player != null)
            {
                try
                {
                    new SpCharacterInfo(player, RelationService.GetRelation(connection.Player, player)).Send(connection);

                    if (player.PlayerMount != 0 && Data.Data.Mounts.ContainsKey(player.PlayerMount))
                        new SpMountShow(player, Data.Data.Mounts[player.PlayerMount].MountId, player.PlayerMount).Send(connection);
                }
                catch (Exception e)
                {
                    Log.Error("Exception " + e);
                }

                return;
            }

            var npc = creature as Npc;
            if (npc != null)
            {
                new SpNpcInfo(npc).Send(connection);

                if (npc.Ai != null && ((NpcAi)npc.Ai).MoveController.IsActive)
                    ((NpcAi)npc.Ai).MoveController.Resend(connection);

                return;
            }

            var gather = creature as Gather;
            if (gather != null)
            {
                new SpGatherInfo(gather).Send(connection);
                return;
            }

            var item = creature as Item;
            if (item != null)
            {
                new SpDropInfo(item).Send(connection);
                return;
            }

            var projectile = creature as Projectile;
            if (projectile != null)
            {
                new SpProjectile(projectile).Send(connection);
                return;
            }

            var campfire = creature as Campfire;
            if (campfire != null)
            {
                new SpCampfire(campfire).Send(connection);
                return;
            }

            Log.Error("SendCreatureInfo: Unknown creature type: {0}", creature.GetType().Name);
        }

        public void SendRemoveCreature(IConnection connection, Creature creature)
        {
            var player = creature as Player;
            if (player != null)
            {
                new SpRemoveCharacter(player).Send(connection);
                return;
            }

            var npc = creature as Npc;
            if (npc != null)
            {
                new SpRemoveNpc(npc, npc.LifeStats.IsDead() ? 5 : 1).Send(connection);
                return;
            }

            var gather = creature as Gather;
            if (gather != null)
            {
                new SpRemoveGather(gather).Send(connection);
                return;
            }

            var item = creature as Item;
            if (item != null)
            {
                new SpRemoveItem(item).Send(connection);
                return;
            }

            var projectile = creature as Projectile;
            if (projectile != null)
            {
                new SpRemoveProjectile(projectile).Send(connection);
                return;
            }

            var campfire = creature as Campfire;
            if (campfire != null)
            {
                new SpRemoveCampfire(campfire).Send(connection);
                return;
            }

            Log.Error("SendRemoveCreature: Unknown creature type: {0}", creature.GetType().Name);
        }

        public void ShowRelogWindow(IConnection connection, int timeout)
        {
            new SpRelogWindow(timeout).Send(connection);
        }

        public void Relog(IConnection connection)
        {
            new SpRelog().Send(connection);
        }

        public void ShowExitWindow(IConnection connection, int timeout)
        {
            new SpExitWindow(timeout).Send(connection);
        }

        public void Exit(IConnection connection)
        {
            new SpExit().Send(connection);
        }

        public void AttackStageEnd(Creature creature)
        {
            VisibleService.Send(creature, new SpAttackEnd(creature, creature.Attack));
            VisibleService.Send(creature, new SpAttack(creature, creature.Attack));
        }

        public void AttackFinished(Creature creature)
        {
            VisibleService.Send(creature, new SpAttackEnd(creature, creature.Attack));
        }

        public void SendPlayerThings(Player player)
        {
            VisibleService.Send(player, new SpCharacterThings(player));
        }

        public void HpChanged(Player player, int diff, Creature attacker)
        {
            new SpUpdateHp(player, diff, attacker).Send(player.Connection);
        }

        public void MpChanged(Player player, int diff, Creature attacker)
        {
            new SpUpdateMp(player, diff, attacker).Send(player.Connection);
        }

        public void StaminaChanged(Player player, int diff)
        {
            new SpUpdateStamina(player).Send(player);
        }

        public void StatsUpdated(Player player)
        {
            new SpCharacterStats(player).Send(player.Connection);
        }

        public void PlayerMoved(Player player, float x1, float y1, float z1, short heading, float x2, float y2, float z2, PlayerMoveType moveType, short unk2, short unk3)
        {
            SpCharacterMove packet = new SpCharacterMove(player, x1, y1, z1, heading, x2, y2, z2, moveType, unk2, unk3);

            player.VisiblePlayers.Each(p => packet.Send(p.Connection));
        }

        public void PlayerLevelUp(Player player)
        {
            VisibleService.Send(player, new SpLevelUp(player));
        }

        public void SendCharRemove(IConnection connection)
        {
            new SpCharacterDelete().Send(connection);
        }

        public void SendLearSkillsDialog(Player player)
        {
            new SpTraidSkillList(player, SkillsLearnService.GetSkillLearnList(player)).Send(player);
            new SpSystemWindow(SystemWindow.LearSkills).Send(player);
            new SpShowWindow(new EmptyRequest(player, RequestType.SkillLearn)).Send(player);
        }

        public void SkillPurchased(Player player, UserSkill skill)
        {
            new SpSkillPurchased(skill).Send(player);
            new SpSkillList(player).Send(player);
            new SpTraidSkillList(player, SkillsLearnService.GetSkillLearnList(player)).Send(player);
        }

        public void PlayerDied(Player player)
        {
            WorldPosition bindPoint = GetNearestBindPoint(player);
            player.ClosestBindPoint = bindPoint;

            VisibleService.Send(player, new SpCharacterDeath(player, true));
            SystemMessages.YouAreDead(player.PlayerData.Name).Send(player);
            new SpDeathDialog(AreaService.GetSectionByCoords(bindPoint).NameId).Send(player);
        }

        private WorldPosition GetNearestBindPoint(Player player)
        {
            WorldPosition position = player.Position;
            var safeHeavens = new List<WorldPosition>();
            if (Data.Data.BindPoints.ContainsKey(position.MapId))
                safeHeavens.AddRange(Data.Data.BindPoints[position.MapId]);
            else
                foreach (var bindPoint in Data.Data.BindPoints)
                    safeHeavens.AddRange(bindPoint.Value);

            double min = double.MaxValue;
            WorldPosition nearest = null;
            foreach (var heaven in safeHeavens)
            {
                double distance = heaven.DistanceTo(position);
                if (distance > min)
                    continue;

                min = distance;
                nearest = heaven;
            }

            return nearest;
        }

        public void Action()
        {

        }
    }
}