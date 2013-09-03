using System;
using Communication;
using Communication.Logic;
using Data.Interfaces;
using Tera.Services;

namespace Tera.AdminEngine.AdminCommands
{
    class Unstuck : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            try
            {
                if (msg.Length > 0)
                {
                    if (msg == "!!!")
                    {
                        Global.TeleportService.ForceTeleport(connection.Player, TeleportService.IslandOfDawnSpawn);
                        return;
                    }

                    for (int i = 0; i < connection.Account.Players.Count; i++)
                    {
                        if (connection.Account.Players[i].PlayerData.Name.Equals(msg.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            var player = connection.Account.Players[i];
                            Global.TeleportService.ForceTeleport(player, TeleportService.IslandOfDawnSpawn);
                            return;
                        }
                    }
                }
                else
                    PlayerLogic.Unstuck(connection);
            }
            catch
            {
                //Nothing
            }
        }
    }
}
