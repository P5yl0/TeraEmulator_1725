using System;
using Data.Enums;
using Data.Interfaces;
using Network.Server;

namespace Tera.AdminEngine.AdminCommands
{
    class Quest : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            string[] args = msg.Split(' ');

            switch (args[0])
            {
                case "getstep":
                    new SpChatMessage(
                        "Current step for quest " + args[1] + " is " +
                        connection.Player.Quests[Convert.ToInt32(args[1])].Step, ChatType.System).Send(connection);
                    break;

                case "setstep":
                    connection.Player.Quests[Convert.ToInt32(args[1])].Step = Convert.ToInt32(args[2]);

                    new SpChatMessage(
                        "Current step for quest " + args[1] + " is updated and now equals " +
                        connection.Player.Quests[Convert.ToInt32(args[1])].Step, ChatType.System).Send(connection);
                    break;
            }
        }
    }
}
