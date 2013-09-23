using Communication;
using Data.Enums;
using Data.Interfaces;
using Network.Server;

namespace Tera.AdminEngine.AdminCommands
{
    class CheckGeo : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            new SpChatMessage("Original Z: " + connection.Player.Position.Z, ChatType.System).Send(connection);

            float realZ = connection.Player.Position.Z;
            Global.GeoService.FixZ(connection.Player.Position);
            new SpChatMessage("Geo Z: " + connection.Player.Position.Z, ChatType.System).Send(connection);
            connection.Player.Position.Z = realZ;

            foreach (var geoLocation in Data.Data.GeoData)
            {
                if (geoLocation.CheckIntersect(connection.Player.Position.X, connection.Player.Position.Y))
                {
                    float z = geoLocation.GetZ(connection.Player.Position.X, connection.Player.Position.Y);
                    new SpChatMessage("Foreach Geo Z: " + z, ChatType.System).Send(connection);
                    return;
                }
            }
        }
    }
}
