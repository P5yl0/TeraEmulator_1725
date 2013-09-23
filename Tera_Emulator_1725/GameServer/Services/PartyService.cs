using System.Collections.Generic;
using System.Linq;
using Communication.Interfaces;
using Data.Enums;
using Data.Interfaces;
using Data.Structures.Player;
using Data.Structures.World.Party;
using Network;
using Network.Server;

namespace Tera.Services
{
    class PartyService : IPartyService
    {
        private static List<Party> _partys = new List<Party>();
        public const byte MaxPlayersInParty = 5;

        public void Action()
        {
            //todo: checks
        }

        public bool CanPlayerJoinParty(Player player, Party party, bool sendErrors = true)
        {
            if (party.PartyMembers.Contains(player))
                return false;

            if (player.Party != null)
                return false;

            if (party.PartyMembers.Count >= MaxPlayersInParty)
            {
                if(sendErrors)
                    SystemMessages.ThePartyIsFull.Send(player);
                return false;
            }
            //todo: other checks

            return true;
        }

        public void KickPlayerFromParty(Player initiator, int playerId)
        {
            if (initiator.Party == null)
                return;

            Player player = GetPayerById(initiator.Party, playerId);

            if (player == null)
                return;

            if (player.Party == null)
                return;

            if (!IsPartyLeader(initiator))
                return;

            //TODO: Party vote
            RemovePlayerFromParty(player, ref initiator.Party);

        }

        public void LeaveParty(Player player)
        {
            //TODO system messages
            if(player.Party == null)
                return;

            RemovePlayerFromParty(player, ref player.Party);
        }

        public void AddNewParty(Player inviter, Player invited)
        {
            if (inviter.Party != null || invited.Party != null)
                return;

            _partys.Add(invited.Party = inviter.Party = new Party { PartyMembers = new List<Player> { inviter, invited } });

            UpdateParty(inviter.Party);
        }

        public void AddNewParty(Player inviter, List<Player> invitedPlayers)
        {
            AddNewParty(inviter, invitedPlayers[0]);

            for (int i = 1; i < invitedPlayers.Count; i++)
                AddPlayerToParty(invitedPlayers[i], ref inviter.Party);
        }

        public void AddPlayerToParty(Player invited, ref Party party)
        {
            if (!CanPlayerJoinParty(invited, party))
                return;

            lock (party.MemberLock)
            {
                party.PartyMembers.Add(invited);
                invited.Party = party;
            }

            UpdateParty(party);

            Communication.Global.RelationService.ResendRelation(invited);
        }

        public void RemovePlayerFromParty(Player player, ref Party party)
        {
            if (player.Party.PartyMembers.Count <= 2)
            {
                RemoveParty(ref player.Party);
                return;
            }

            lock (player.Party.MemberLock)
                player.Party.PartyMembers.Remove(player);

            SendPacketToPartyMembers(party, new SpPartyRemoveMember(player));

            UpdateParty(player.Party);

            player.Party = null;

            new SpPartyLeave().Send(player);
        }

        public void RemoveParty(ref Party party)
        {
            _partys.Remove(party);

            lock (party.MemberLock)
            {
                foreach (Player member in party.PartyMembers)
                {
                    member.Party = null;
                    new SpPartyLeave().Send(member);
                }
            }
            party = null;
        }

        public void PromotePlayer(Player promoter, int promotedId)
        {
            Player promoted = GetPayerById(promoter.Party, promotedId);

            if(promoted == null)
                return;

            if(!promoter.Party.Equals(promoted.Party))
                return;

            if (!IsPartyLeader(promoted) && IsPartyLeader(promoter))
            {
                lock (promoter.Party.MemberLock)
                {
                    promoter.Party.PartyMembers.Remove(promoted);
                    promoter.Party.PartyMembers.Insert(0, promoted);
                }
                //todo: sytem message
                UpdateParty(promoter.Party);
            }
        }

        public void SendPacketToPartyMembers(Party party, ISendPacket packet, Player sender = null)
        {
            if (party == null)
                return;

                lock (party.MemberLock)
                    foreach (Player member in party.PartyMembers)
                        if (!member.Equals(sender) && Communication.Global.PlayerService.IsPlayerOnline(member))
                            packet.Send(member.Connection);
        }

        public void UpdateParty(Party party)
        {
            if(party == null)
                return;

            SendPacketToPartyMembers(party, new SpPartyList(party.PartyMembers));
            SendLifestatsToPartyMembers(party);
            SendEffectsToPartyMembers(party);
        }

        public void SendLifestatsToPartyMembers(Party party)
        {
            if(party == null)
                return;

            lock (party.MemberLock)
                foreach (Player partyMember in party.PartyMembers)
                    if (Communication.Global.PlayerService.IsPlayerOnline(partyMember))
                        foreach (Player member in party.PartyMembers)
                            if (Communication.Global.PlayerService.IsPlayerOnline(member))
                                new SpPartyStats(member).Send(partyMember);
        }

        public void SendEffectsToPartyMembers(Party party)
        {
            if (party == null)
                return;

            //lock (party.MemberLock)
            //    foreach (Player partyMember in party.PartyMembers)
            //        if (Communication.Global.PlayerService.IsPlayerOnline(partyMember))
            //            foreach (Player member in party.PartyMembers)
            //                if (Communication.Global.PlayerService.IsPlayerOnline(member))
            //                    new SpPartyAbnormals(member).Send(partyMember);
        }

        public void SendMemberPositionToPartyMembers(Player player)
        {
            if(player.Party == null)
                return;

            SendPacketToPartyMembers(player.Party, new SpPartyMemberPosition(player));
        }

        public List<Player> GetOnlineMembers(Party party)
        {
            return party.PartyMembers.Where(member => Communication.Global.PlayerService.IsPlayerOnline(member)).ToList();
        }

        public void AddExp(Player player, long exp)
        {
            lock(player.Party.MemberLock)
                player.Party.Exp += exp;
        }

        public void ReleaseExp(Player player)
        {
            lock (player.Party.MemberLock)
            {
                long expPerMember = player.Party.Exp/player.Party.PartyMembers.Count;
                player.Party.Exp = 0;
                foreach (var member in player.Party.PartyMembers)
                    Communication.Global.PlayerService.AddExp(member, expPerMember, null);
            }
        }

        public void HandleChatMessage(Player player, string message)
        {
            if(player.Party == null)
            {
                SystemMessages.YoureNotInAParty.Send(player);
                return;
            }

            SendPacketToPartyMembers(player.Party, new SpChatMessage(player, message, ChatType.Party));
        }

        private bool IsPartyLeader(Player player)
        {
            if (player.Party == null)
                return false;

            if (!player.Party.PartyMembers[0].Equals(player))
                return false;

            return true;
        }

        private Player GetPayerById(Party party, int playerId)
        {
            return party.PartyMembers.FirstOrDefault(member => member.PlayerId == playerId);
        }
    }
}
