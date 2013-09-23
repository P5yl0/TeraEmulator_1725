using System.Collections.Generic;
using System.Threading.Tasks;
using Communication.Interfaces;
using Data.Structures.Player;
using Data.Structures.World;
using Network;
using Data.Structures.World.Requests;
using Network.Server;
using Tera.Controllers;
using Utils;

namespace Tera.Services
{
    class DuelService : IDuelService
    {
        protected List<Duel> Duels = new List<Duel>();
        protected object DuelsLock = new object();

        public void StartDuel(Player initiator, Player initiated, Request request)
        {
            if(initiator.Duel != null || initiated.Duel != null)
                return;

            Duel d = new Duel
                               {
                                   Initiator = initiator,
                                   Initiated = initiated,
                                   Request = request
                               };
            initiator.Duel = d;
            initiated.Duel = d;
            StartCondition(d);
        }

        public void ProcessDamage(Player player)
        {
            if(player.Duel == null)
                return;

            player.Duel.LastKickUtc = Funcs.GetCurrentMilliseconds();
        }

        public void FinishDuel(Player winner)
        {
            if(winner.Duel == null)
                return;

            Duel d = winner.Duel;

            SendDuelEnd(d);

            SystemMessages.DuelWon.Send(winner);
            SystemMessages.DuelLost.Send(d.Initiator.Equals(winner) ? d.Initiated : winner);

            d.Initiator.Duel = null;
            d.Initiated.Duel = null;

            Communication.Global.RelationService.ResendRelation(d.Initiator);
            Communication.Global.RelationService.ResendRelation(d.Initiated);

            if(d.Initiator.Controller is BattleController)
                ((BattleController)d.Initiator.Controller).RemoveTarget(d.Initiated);
            if (d.Initiated.Controller is BattleController)
                ((BattleController)d.Initiated.Controller).RemoveTarget(d.Initiator);

            lock (DuelsLock)
                Duels.Remove(d);
            Communication.Global.ActionEngine.RemoveRequest(d.Request);
        }

        public void PlayerLeaveWorld(Player player)
        {
            if (player.Duel != null)
                FinishDuel(player.Duel.Initiator.Equals(player) ? player.Duel.Initiated : player.Duel.Initiator);
        }

        private async void StartCondition(Duel duel)
        {
            new SpDuelCounter().Send(duel.Initiator);
            new SpDuelCounter().Send(duel.Initiated);

            await Task.Delay(5000);

            duel.LastKickUtc = Funcs.GetCurrentMilliseconds();

            Communication.Global.RelationService.ResendRelation(duel.Initiator);
            Communication.Global.RelationService.ResendRelation(duel.Initiated);

            lock (DuelsLock)
                Duels.Add(duel);
        }

        public void Action()
        {
            lock (DuelsLock)
            {
                for (int i = 0; i < Duels.Count; i++)
                {
                    if (Funcs.GetCurrentMilliseconds() - Duels[i].LastKickUtc >= 60000)
                    {
                        Duels[i].Initiator.Duel = null;
                        Duels[i].Initiated.Duel = null;

                        Communication.Global.RelationService.ResendRelation(Duels[i].Initiator);
                        Communication.Global.RelationService.ResendRelation(Duels[i].Initiated);

                        SystemMessages.DuelAutomaticallyEnded.Send(Duels[i].Initiator);
                        SystemMessages.DuelAutomaticallyEnded.Send(Duels[i].Initiated);

                        Duels.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        private void SendDuelEnd(Duel duel)
        {
            //todo Send hp bar remove
        }
    }
}
