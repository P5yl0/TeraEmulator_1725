using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Communication.Interfaces;
using Data.Enums;
using Data.Structures.Player;
using Data.Structures.World.Requests;
using Network.Server;
using Tera.Controllers;
using Network;
using Utils;

namespace Tera.ActionEngine
{
    public class ActionEngine : IActionEngine
    {
        /// <summary>
        /// Collection of all active requests on the server.
        /// </summary>
        public readonly Dictionary<int, Request> WorldRequests = new Dictionary<int, Request>();
        public readonly object RequestsLock = new object();

        public void Init()
        {
        }

        public void RemoveRequest(Request request)
        {
            lock (RequestsLock) { UnregisterRequest(request); }
        }

        public Request GetRequest(int id)
        {
            lock (RequestsLock)
                return WorldRequests.ContainsKey(id) ? WorldRequests[id] : null;
        }

        public void AddRequest(Request request)
        {
            #region Guild creation pre checks
            var guildCreate = request as GuildCreate;
            if (guildCreate != null)
            {
                if (!Communication.Global.GuildService.CanUseName(guildCreate.GuildName))
                {
                    SystemMessages.
                        GuildNamesMustBeASingleStringOfCharactersBetween3And15InLengthAndMustBeDifferentFromAnyExistingGuildName
                        .Send(request.Owner);
                    return;
                }
                new SpSystemWindow(SystemWindow.Hide).Send(request.Owner);
                SystemMessages.AskingYourPartyMembersToAproveCreationOfThisGuild.Send(request.Owner);
            }

            #endregion

            lock (RequestsLock)
            {
                bool typeExists = false;
                if (request.Blocking)
                {
                    if (request.Owner.Requests.Any(item => item.Type == request.Type))
                    {
                        typeExists = true;
                    }
                    request.Owner.Requests.Add(request);
                }
                if (!typeExists)
                {
                    SendRequest(request);
                }
                else
                {
                    new SpSystemNotice("You cannot do this action yet! Please try again in 5 minutes.").Send(request.Owner.Connection);
                }
            }
        }

        public void ProcessRequest(int uid, bool isAccepted, Player arrivedFrom = null)
        {
            Request request;
            lock (RequestsLock)
            {
                if (!WorldRequests.ContainsKey(uid))
                    return;

                request = WorldRequests[uid];
                request.InProgress = true;
            }

            // Someone tried to accept/decline request that don't belongs to him
           if ((request.Target != null && request.Target != arrivedFrom) 
                || (request.Target == null && !request.Owner.Party.PartyMembers.Contains(arrivedFrom)))
                return;
            
            IRequestAction action = null;
            switch (request.Type)
            {
                case RequestType.PartyInvite:
                    action = new PartyAction(request);
                    break;
                case RequestType.DuelInvite:
                    action = new DuelAction(request);
                    break;
                case RequestType.GuildCreate:
                        action = new GuildAction(request, arrivedFrom);
                    break;
                case RequestType.GuildInvite:
                    action = new GuildInviteAction(request);
                    break;
                case RequestType.TradeStart:
                    action = new TradeAction(request);
                    break;
            }

            // process request
            try
            {
                if(action == null)
                    return;

                if (isAccepted)
                    action.Accepted();
                else
                    action.Declined();
            }
            catch(Exception e)
            {
                Log.Error("Process request#{0} exception: {1}", (int)request.Type, e.ToString());
            }
            finally
            {
                // Ensure that we remove request, no matter what happens
                if (!request.Blocking)
                    lock (RequestsLock)
                        UnregisterRequest(request);
            }
        }

        /// <summary>
        /// Send a request notification to both owner and target.
        /// </summary>
        /// <param name="request">Which request to send</param>
        private void SendRequest(Request request)
        {
            if (WorldRequests.ContainsKey(request.UID))
                return;

            WorldRequests.Add(request.UID, request);
            new SpCanSendRequest((int)request.Type).Send(request.Owner);

            if (request.Target == null || request.Target.Controller is DefaultController)
            {
                switch (request.Type)
                {
                    case RequestType.PartyInvite:
                    case RequestType.GuildInvite:
                        new SpRequestInvite(request).Send(request.Target);
                        break;
                    case RequestType.GuildCreate:
                        if (request is GuildCreate)
                        {
                            if (request.Owner.Party == null)
                                return;
                            foreach (var member in request.Owner.Party.PartyMembers.Where(member => member != request.Owner))
                                new SpRequestInvite(request).Send(member);
                        }
                        else
                            new SpRequestInvite(request).Send(request.Target);
                        break;
                    default:
                        new SpShowWindow(request).Send(request.Owner, request.Target);
                        break;
                }
                RemoveTimedOutRequest(request);
            }
            else
                ProcessRequest(request.UID, false, request.Target);
        }

        /// <summary>
        /// Remove all references to this request and release block if request was blocking.
        /// </summary>
        /// <param name="request">Request to remove</param>
        private void UnregisterRequest(Request request)
        {
            if (request == null)
                return;

            if (WorldRequests.ContainsKey(request.UID))
            {
                // this means that window is opened now
                WorldRequests.Remove(request.UID);
                new SpHideRequest(request).Send(request.Owner);

                if(request.Target != null)
                    new SpHideRequest(request).Send(request.Target);
            }
            // remove request from player's queue
            request.Owner.Requests.Remove(request);

            // if request is blocking, we may have other requests of this type pending
            if (request.Blocking)
            {
                    foreach (Request item in request.Owner.Requests)
                        if (item.Type == request.Type)
                        {
                            // if we have requests pending, send one of them them
                            SendRequest(item);
                            break;
                        }
            }
        }

        private async void RemoveTimedOutRequest(Request request)
        {
            if(request.Timeout == 0)
                return;

            await Task.Delay(request.Timeout);

            if(request.Blocking && !request.InProgress)
                RemoveRequest(request);
        }

        public void Action()
        {
            
        }
    }
}
