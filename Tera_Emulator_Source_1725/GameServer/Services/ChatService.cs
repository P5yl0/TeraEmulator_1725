using Communication;
using Communication.Interfaces;
using Data.Enums;
using Data.Interfaces;
using Data.Structures;
using Data.Structures.Player;
using Network.Server;

namespace Tera.Services
{
    class ChatService : Global, IChatService
    {
        public void ProcessMessage(IConnection connection, string message, ChatType type)
        {
            switch (type)
            {
                case ChatType.Notice:
                    break;
                case ChatType.PrivateWhispered:
                    new SpChatMessage(connection.Player, message, type).Send(connection);
                    break;
                case ChatType.Guild:
                    GuildService.HandleChatMessage(connection.Player, message);
                    break;
                case ChatType.Party:
                    PartyService.HandleChatMessage(connection.Player, message);
                    break;
                case ChatType.Say:
                    VisibleService.Send(connection.Player, new SpChatMessage(connection.Player, message, type));
                    break;
                default:
                    PlayerService.Send(new SpChatMessage(connection.Player, message, type));
                    break;
            }
        }

        public void ProcessPrivateMessage(IConnection connection, string playerName, string message)
        {
            Player pl = PlayerService.GetPlayerByName(playerName);

            if(pl == null)
                return;

            ProcessMessage(connection, message, ChatType.PrivateWhispered);

            new SpChatPrivate(connection.Player.PlayerData.Name, pl.PlayerData.Name, message).Send(pl);
        }

        public void SendChatInfo(IConnection connection, int type, string name)
        {
            Player pl = PlayerService.GetPlayerByName(name.Replace("[GM]",""));

            if (pl == null || pl.Equals(connection.Player))
                return;

            new SpChatInfo(pl, type).Send(connection);
        }

        public void Action()
        {
            
        }
    }
}