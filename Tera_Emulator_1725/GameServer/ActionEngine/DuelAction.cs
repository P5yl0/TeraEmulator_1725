using Data.Structures.World.Requests;
using Network;
using Network.Server;
using Tera.Controllers;

namespace Tera.ActionEngine
{
    /// <summary>
    /// Performs duel request
    /// </summary>
    public class DuelAction : IRequestAction
    {
        private Request request;

        public DuelAction(Request duelRequest)
        {
            request = duelRequest;
        }

        public void Accepted()
        {
            // decline duel if owner or target are already in battle
            if (request.Owner.Duel != null)
            {
                SystemMessages.TargetIsInCombat.Send(request.Owner);
                Communication.Global.ActionEngine.RemoveRequest(request);
                return;
            }
            if(request.Target.Duel != null)
            {
                SystemMessages.YouAreInADuelNow.Send(request.Target);
                Communication.Global.ActionEngine.RemoveRequest(request);
                return;
            }

            Communication.Global.DuelService.StartDuel(request.Owner, request.Target, request);
            new SpHideRequest(request).Send(request.Owner, request.Target);
        }

        public void Declined()
        {
            SystemMessages.TargetRejectedTheDuel(request.Target.PlayerData.Name).Send(request.Owner);
            Communication.Global.ActionEngine.RemoveRequest(request);
        }
    }
}
