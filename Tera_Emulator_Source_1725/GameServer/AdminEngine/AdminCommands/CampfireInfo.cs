using System;
using Data.Enums;
using Data.Interfaces;
using Data.Structures.World;
using Network.Server;
using Utils;

namespace Tera.AdminEngine.AdminCommands
{
    class CampfireInfo : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            try
            {
                Campfire campfire = null;
                double distance = double.MaxValue;

                foreach (var camp in connection.Player.VisibleCampfires)
                {
                    double dist = camp.Position.DistanceTo(connection.Player.Position);
                    if (dist < distance)
                    {
                        campfire = camp;
                        distance = dist;
                    }
                }

                if (campfire == null)
                {
                    new SpChatMessage("Campfire in visible not found!", ChatType.System).Send(connection);
                    return;
                }

                new SpChatMessage("Unk1: " + campfire.Type, ChatType.System).Send(connection);
                new SpChatMessage("Unk2: " + campfire.Status, ChatType.System).Send(connection);
            }
            catch (Exception ex)
            {
                Log.ErrorException("AdminEngine: CampfireInfo", ex);
            }
        }
    }
}
