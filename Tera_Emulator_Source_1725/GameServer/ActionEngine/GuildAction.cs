using System.Linq;
using Communication;
using Data.Structures.Player;
using Data.Structures.World.Requests;
using System.Collections.Generic;
using Network;

namespace Tera.ActionEngine
{
    class GuildAction : IRequestAction
    {
        private readonly Player _owner;
        private readonly Player _target;
        private readonly string _guildName;
        private readonly Request _request;

        public GuildAction(Request request, Player target)
        {
            _request = request;

            _owner = request.Owner;
            _target = target;

            _guildName = ((GuildCreate)request).GuildName;
        }

        public void Accepted()
        {
            if (_owner.Party == null || _target.Party == null || _owner.Party != _target.Party)
                return;

            if (_target.Guild != null)
            {
                Declined();
                return;
            }

            if (_owner.Guild == null)
            {
                Global.GuildService.AddNewGuild(new List<Player> {_owner, _target}, _guildName);
                Global.StorageService.RemoveMoney(_owner, _owner.Inventory, 3000);
            }
            else
                Global.GuildService.AddMemberToGuild(_target, _owner.Guild, _owner);
            _target.PlayerGuildAccepted = 1;

            PartyFinished();
        }

        public void Declined()
        {
            if (_owner.Party == null || _target.Party == null || _owner.Party != _target.Party)
                return;

            SystemMessages.Name1RejectedName2GuildApplication(_target.PlayerData.Name, _owner.PlayerData.Name).Send(_owner, _target);
            _target.PlayerGuildAccepted = 2;

            PartyFinished();
        }

        // if everyone in party finished voting for/against guild, remove request
        private void PartyFinished()
        {
            lock (_owner.Party.MemberLock)
            {
                int pendingRequests = _owner.Party.PartyMembers.Count(member => member != _owner && member.PlayerGuildAccepted == 0);

                if (pendingRequests == 0)
                {
                    if(_owner.Guild == null)
                        SystemMessages.GuildCreationFailedPleaseTryAgain.Send(_owner);

                    foreach (var member in _owner.Party.PartyMembers)
                        member.PlayerGuildAccepted = 0;

                    Global.ActionEngine.RemoveRequest(_request);
                }
            }

        }
    }
}
