using Data.Structures.Player;
using Data.Structures.World.Requests;
using Network;
using Utils;

namespace Tera.ActionEngine
{
    public class GuildInviteAction : IRequestAction
    {
        private Player _target;
        private Player _owner;

        public GuildInviteAction(Request request)
        {
            _owner = request.Owner;
            _target = request.Target;
        }

        public void Accepted()
        {
            if(_target == null)
                return;

            if(_target.Guild != null)
            {
                SystemMessages.YoureAlreadyInAGuild.Send(_target);
                Declined();
                return;
            }

            if(_owner.Guild == null)
            {
                SystemMessages.NoSuchGuildExists.Send(_owner);
                Declined();
                return;
            }

            lock(_owner.Guild.MembersLock)
            {
                if(!_owner.Guild.GuildMembers.ContainsKey(_owner))
                {
                    Log.Error("{0} sent an invite to the guild that he is not member in!",_owner.PlayerData.Name);
                    Declined();
                    return;
                }

                // check whether this guild member can invite people to guild
                if (_owner.Guild.GuildMembers[_owner] > 0)
                {
                    Communication.Global.GuildService.AddMemberToGuild(_target, _owner.Guild, _owner);
                    SystemMessages.Name1AcceptedName2IntoTheGuild(_target.PlayerData.Name, _owner.PlayerData.Name).Send(_owner,_target);
                    //todo all guild members
                    SystemMessages.NameJoinedTheGuild(_target.PlayerData.Name).Send(_owner,_target);
                }
                else
                    Declined();
            }
        }

        public void Declined()
        {
            SystemMessages.Name1RejectedName2GuildApplication(_target.PlayerData.Name, _owner.PlayerData.Name).Send(_owner,_target);
        }
    }
}
