using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Interfaces;
using Data.Enums.Player;
using Data.Structures.Player;
using Network.Server;
using Tera.Extensions;

namespace Tera.Services
{
    public class RelationService : IRelationService
    {
        public void Action()
        {

        }

        public PlayerRelation GetRelation(Player asker, Player asked)
        {
            if (asker.Duel != null && asked.Duel != null && asked.Duel.Equals(asker.Duel))
                return PlayerRelation.DuelEnemie;

            if (asker.Party != null && asked.Party != null && asked.Party.Equals(asker.Party))
                return PlayerRelation.PartyMember;

            if (asker.Guild != null && asked.Guild != null && asked.Guild.Equals(asker.Guild))
                return PlayerRelation.GuildMember;

            return PlayerRelation.Friendly;
        }

        public void ResendRelation(Player player)
        {
            if (player.VisiblePlayers != null)
            {
                player.VisiblePlayers.Each(
                    vplayer =>
                        {
                            new SpCharacterRelation(vplayer, GetRelation(player, vplayer)).Send(player);
                            new SpCharacterRelation(player, GetRelation(player, vplayer)).Send(vplayer);
                        });
            }
        }
    }
}
