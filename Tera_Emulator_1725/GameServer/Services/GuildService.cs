using System;
using System.Collections.Generic;
using System.Linq;
using Communication.Interfaces;
using Data;
using Data.Enums;
using Data.Interfaces;
using Data.Structures.Guild;
using Data.Structures.Player;
using Network;
using Network.Server;
using Utils;

namespace Tera.Services
{
    public class GuildService : IGuildService
    {
        public object GuildsLock = new object();
        public Dictionary<int, List<Guild>> GuildListCache = new Dictionary<int, List<Guild>>();
        public long LastUpdateMilliseconds = Funcs.GetCurrentMilliseconds();

        public const int MaxGuildsInTab = 16;
        public const long UpdateTimeout = 10000;

        public void Init()
        {

        }

        public void Action()
        {
            if(Funcs.GetCurrentMilliseconds() - LastUpdateMilliseconds < UpdateTimeout)
                return;

            GuildListCache = new Dictionary<int, List<Guild>>();
            lock (GuildsLock)
            {
                int check = 1;
                foreach (KeyValuePair<int, Guild> guild in Cache.Guilds)
                {
                    if (!GuildListCache.ContainsKey(check))
                        GuildListCache.Add(check, new List<Guild>());

                    GuildListCache[check].Add(guild.Value);

                    if (GuildListCache[check].Count >= MaxGuildsInTab)
                        check++;
                }

                LastUpdateMilliseconds = Funcs.GetCurrentMilliseconds();
            }
        }

        public void AddNewGuild(List<Player> players, string guildName)
        {
            Guild g = new Guild
                          {
                              GuildLogo = "",
                              GuildName = guildName,
                              CreationDate = Funcs.GetRoundedUtc(),
                              GuildHistory = new List<HistoryEvent>(),
                              GuildRanks = new List<GuildMemberRank>()
                          };

            AddNewRank(g, new GuildMemberRank {RankId = 1, RankName = "Guildmaster"});
            AddNewRank(g, new GuildMemberRank {RankId = 2, RankName = "Recruit"});

            foreach (Player player in players)
            {
                AddMemberToGuild(player, g);
                SystemMessages.GuildHasBeenCreated(guildName).Send(player.Connection);
            }

            lock (GuildsLock)
                Cache.Guilds.Add(g.GuildId, g);

            Cache.UsedGuildNames.Add(g.GuildName.ToLower());
        }

        public void AddMemberToGuild(Player player, Guild guild, Player inviter = null)
        {
            if (guild == null || player == null)
                return;

            if(guild.GuildMembers != null && guild.GuildMembers.Count > 30)
                return;

            lock (guild.MembersLock)
            {
                if (guild.GuildMembers == null || guild.GuildMembers.Count == 0)
                    guild.GuildMembers = new Dictionary<Player, int> {{player, 1}};
                else
                    guild.GuildMembers.Add(player, 2);
            }

            AddHistoryEvent(guild,
                            new HistoryEvent
                                {
                                    Args = GuildHistoryStrings.UserXInvitedUserY(player.PlayerData.Name)
                                        /* TODO: 
                                         * inviter != null
                                            ? GuildHistoryStrings.UserXInvitedUserY(player.PlayerData.Name)
                                            : GuildHistoryStrings.GuildCreate(player.PlayerData.Name)*/
                                });

            player.Guild = guild;

            Communication.Global.VisibleService.Send(player,
                                                     new SpCharacterGuildInfo(player, guild.GuildName, "Recruit"));

            Communication.Global.RelationService.ResendRelation(player);
        }

        public void AddHistoryEvent(Guild guild, HistoryEvent hEvent, Player initiator = null)
        {
            if(guild == null)
                return;

            hEvent.Date = Funcs.GetRoundedUtc();

            guild.GuildHistory.Add(hEvent);

            //TODO update
        }

        public void CreateNewRank(Guild guild, string rankName)
        {
            if(guild == null)
                return;

            if(guild.GuildRanks.Count >= 10)
                return;

            if(guild.GuildRanks.FirstOrDefault(rank => rank.RankName.ToLower() == rankName.ToLower()) != null)
                return;

            AddNewRank(guild,
                       new GuildMemberRank
                           {
                               RankId = guild.GuildRanks.Last().RankId + 1,
                               RankName = rankName,
                               RankPrivileges = 0
                           });

            UpdateGuild(guild);
        }

        public void ChangeMemberRank(Player initiator, int playerid, int newRank)
        {
            if(initiator.Guild == null)
                return;

            if(!GetLeader(initiator.Guild).Equals(initiator))
                return;

            if(initiator.Guild.GuildRanks.FirstOrDefault(rank => rank.RankId == newRank) == null)
                return;

            if(GetPlayerById(initiator.Guild, playerid) == null)
                return;

            initiator.Guild.GuildMembers[GetPlayerById(initiator.Guild, playerid)] = newRank;

            UpdateGuild(initiator.Guild);
        }

        private void AddNewRank(Guild guild, GuildMemberRank rank)
        {
            if(guild == null)
                return;

            if (rank.RankName == "Guildmaster")
                rank.RankPrivileges = 7;

            guild.GuildRanks.Add(rank);

            AddHistoryEvent(guild, new HistoryEvent {Args = GuildHistoryStrings.AddNewRank(rank.RankName)});

            UpdateGuild(guild);
        }

        public void KickMember(Player initiator, string playerName)
        {
            if(initiator.Guild == null)
                return;

            Player p = GetPlayerByName(initiator.Guild, playerName);

            if(p == null || p.Guild == null || !p.Guild.Equals(initiator.Guild) || !GetLeader(p.Guild).Equals(initiator))
                return;

            RemoveMember(p, initiator.Guild);
        }

        public void LeaveGuild(Player player, Guild guild)
        {
            RemoveMember(player, guild);
        }

        public void RemoveMember(Player player, Guild guild)
        {
            if(guild == null)
                return;

            lock (guild.MembersLock)
            {
                guild.GuildMembers.Remove(player);
                player.Guild = null;
            }

            if(Communication.Global.PlayerService.IsPlayerOnline(player))
                new SpGuildMemberList().Send(player);

            SystemMessages.YouLeftTheGuild(guild.GuildName).Send(player.Connection);

            if (GetLeader(guild).Equals(player) || guild.GuildMembers.Count <= 1)
            {
                DisbandGuild(GetLeader(guild), guild);
                return;
            }

            SendPacketToGuildMembers(SystemMessages.PlayerHasBeenRemovedFromTheGuild(player.PlayerData.Name), guild);

            UpdateGuild(guild);

            Communication.Global.RelationService.ResendRelation(player);
        }

        public void ChangeRankPrivileges(Player initiator, int rankId, int newPrivileges, string newName)
        {
            if(initiator.Guild == null)
                return;

            if(!GetLeader(initiator.Guild).Equals(initiator))
                return;

            GuildMemberRank r = initiator.Guild.GuildRanks.FirstOrDefault(rank => rank.RankId == rankId);

            if(r == null)
                return;

            r.RankPrivileges = newPrivileges;
            r.RankName = newName;

            UpdateGuild(initiator.Guild);
        }

        public void ChangeGuildLeader(Player initiator, string playerName)
        {
            if(initiator.Guild == null)
                return;

            if(!GetLeader(initiator.Guild).Equals(initiator))
                return;

            if(GetPlayerByName(initiator.Guild, playerName) == null)
                return;

            initiator.Guild.GuildMembers[initiator] = 2; //recruit allways
            initiator.Guild.GuildMembers[GetPlayerByName(initiator.Guild, playerName)] = 1;

            SendPacketToGuildMembers(SystemMessages.PlayerIsNowGuildmaster(playerName), initiator.Guild);

            UpdateGuild(initiator.Guild);
        }

        public void ChangeGuildIcon(Player initiator, Guild guild, byte[] newIcon)
        {
            throw new NotImplementedException();
        }

        public void DisbandGuild(Player initiator, Guild guild)
        {
            if(!GetLeader(guild).Equals(initiator))
                return;

            SendPacketToGuildMembers(SystemMessages.TheGuildHasBeenDisbanded(guild.GuildName), guild);

            lock (guild.MembersLock)
            {
                foreach (KeyValuePair<Player, int> guildMember in guild.GuildMembers)
                {
                    guildMember.Key.Guild = null;
                    SendGuildToPlayer(guildMember.Key);
                }
            }

            guild.GuildMembers = null;

            lock (GuildsLock)
                Cache.Guilds.Remove(guild.GuildId);

            if (Cache.UsedGuildNames.Contains(guild.GuildName.ToLower()))
                Cache.UsedGuildNames.Remove(guild.GuildName.ToLower());
        }

        public void ChangeAd(Player player, string newAd)
        {
            if(player.Guild == null || !GetLeader(player.Guild).Equals(player))
                return;

            if (newAd.Length > 60)
                return;

            player.Guild.GuildAd = newAd;

            SendGuildInformationToOnlineMembers(player.Guild);
        }

        public void ChangeMotd(Player player, string newMotd)
        {
            if (player.Guild == null || GetPlayerRank(player).RankPrivileges < 3)
                return;

            if (newMotd.Length > 300)
                return;

            player.Guild.GuildMessage = newMotd;

            SendGuildInformationToOnlineMembers(player.Guild);

            SendPacketToGuildMembers(new SpChatMessage(newMotd, ChatType.Guild), player.Guild);
        }

        public void ChangeTitle(Player player, string newTitle)
        {
            if (player.Guild == null || !GetLeader(player.Guild).Equals(player))
                return;

            if (newTitle.Length > 40)
                return;

            player.Guild.GuildTitle = newTitle;

            SendGuildInformationToOnlineMembers(player.Guild);
        }

        public void OnPlayerEnterWorld(Player player)
        {
            if(player.Guild == null || player.Guild.GuildMessage.Length < 1)
                return;

            new SpChatMessage(player.Guild.GuildMessage, ChatType.Guild).Send(player);
        }

        public void SendGuildToPlayer(Player player)
        {
            if(player.Guild!= null)
                new SpGuildRanking(player).Send(player);

            new SpGuildMemberList(player.Guild).Send(player);
        }

        public void SendGuildHistory(Player player)
        {
            if (player.Guild == null)
                return;

            new SpGuildHistory(player.Guild).Send(player);
        }

        public void SendPacketToGuildMembers(ISendPacket packet, Guild guild, Player sender = null)
        {
            lock (guild.MembersLock)
                foreach (
                    KeyValuePair<Player, int> guildMember in
                        guild.GuildMembers.Where(
                            guildMember => Communication.Global.PlayerService.IsPlayerOnline(guildMember.Key) && !guildMember.Key.Equals(sender)))
                    packet.Send(guildMember.Key.Connection);
        }

        public void SendServerGuilds(Player player, int tabId)
        {
            if (GuildListCache.ContainsKey(tabId))
                new SpServerGuilds(GuildListCache[tabId], tabId, Cache.Guilds.Count, GuildListCache.Count).Send(player);
        }

        public void HandleChatMessage(Player sender, string message)
        {
            if(sender.Guild == null)
            {
                SystemMessages.YoureNotInAGuild.Send(sender);
                return;
            }

            SendPacketToGuildMembers(new SpChatMessage(sender, message, ChatType.Guild), sender.Guild);
        }

        public Player GetLeader(Guild guild)
        {
            if (guild == null)
                return null;

            return guild.GuildMembers.FirstOrDefault(player => player.Value == 1).Key;
        }

        public GuildMemberRank GetPlayerRank(Player player)
        {
            if (player.Guild == null)
                return null;

            return 
                player.Guild.GuildRanks.FirstOrDefault(
                    rank =>
                    rank.RankId == (player.Guild.GuildMembers.FirstOrDefault(member => member.Key.Equals(player)).Value));
        }

        private Player GetPlayerById(Guild guild, int playerId)
        {
            return guild.GuildMembers.FirstOrDefault(pl => pl.Key.PlayerId == playerId).Key;
        }

        private Player GetPlayerByName(Guild guild, string name)
        {
            return guild.GuildMembers.FirstOrDefault(pl => pl.Key.PlayerData.Name == name).Key;
        }

        public void UpdateGuild(Guild guild)
        {
            if(guild == null)
                return;

            if(guild.GuildMembers == null)
                return;

            lock (guild.MembersLock)
            {
                foreach (var member in guild.GuildMembers)
                {
                    if(Communication.Global.PlayerService.IsPlayerOnline(member.Key))
                    {
                        new SpGuildRanking(member.Key).Send(member.Key);
                        new SpGuildMemberList(guild).Send(member.Key);
                    }
                }
            }
        }

        public void PraiseGuild(string name)
        {
            lock (GuildsLock)
                foreach (Guild guild in Cache.Guilds.Values.Where(guild => guild.GuildName == name))
                    guild.Praises++;
        }
        
        public bool CanUseName(string name)
        {
            if (name.Length < 3 || name.Length > 15)
                return false;

            return !Cache.UsedGuildNames.Contains(name.ToLower());
        }

        private void SendGuildInformationToOnlineMembers(Guild guild)
        {
            if(guild == null)
                return;

            lock (guild.MembersLock)
                foreach (KeyValuePair<Player, int> member in guild.GuildMembers)
                    if (Communication.Global.PlayerService.IsPlayerOnline(member.Key))
                        new SpGuildRanking(member.Key).Send(member.Key);
        }
    }
}
