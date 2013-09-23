using System;
using Communication.Interfaces;
using Data.Structures.Player;
using Data.Structures.World;
using Network.Server;
using Tera.Controllers;
using Tera.DungeonEngine.Dungeons;

namespace Tera.Services
{
    internal class TeleportService : ITeleportService
    {
        public static readonly WorldPosition IslandOfDawnSpawn = new WorldPosition
            {
                Heading = 32767,
                MapId = 13,
                X = 93492,
                Y = -88216,
                Z = -4523
            };

        public void StartPegasusFlight(Player player, int destinationId)
        {
            //todo money remove
            Communication.Global.ControllerService.SetController(player, new FlyController(player, destinationId));
        }

        public void ForceTeleport(Player player, WorldPosition position)
        {
            Communication.Global.MapService.PlayerLeaveWorld(player);

            player.Position.MapId = position.MapId;
            player.Position.X = position.X;
            player.Position.Y = position.Y;
            player.Position.Z = position.Z;
            player.Position.Heading = position.Heading;

            new SpCharacterBind(player).Send(player.Connection);
        }

        public void SwitchContinent(Player player, int continentId)
        {
            ForceTeleport(player, new WorldPosition
                {
                    MapId = continentId,
                    X = player.Position.X,
                    Y = player.Position.Y,
                    Z = player.Position.Z,
                    Heading = player.Position.Heading
                });
        }

        public WorldPosition GetBindPoint(Player player)
        {
            WorldPosition pos = IslandOfDawnSpawn.Clone();
            int mapId = 13;

            if (Communication.Global.MapService.IsDungeon(player.Position.MapId))
            {
                if (((ADungeon) player.Instance).ParentMapId != 0 &&
                    Data.Data.CampfireTemplates.ContainsKey(((ADungeon) player.Instance).ParentMapId))
                    mapId = ((ADungeon) player.Instance).ParentMapId;
            }
            else
                mapId = player.Position.MapId;

            if (Data.Data.CampfireTemplates.ContainsKey(mapId))
                foreach (var template in Data.Data.CampfireTemplates[mapId])
                {
                    if(template.Type == 1)
                        continue;

                    if (template.Position.DistanceTo(player.Position) < player.Position.DistanceTo(pos))
                        pos = template.Position.Clone();
                }

            pos.X += new Random().Next(75, 100);

            return pos;
        }

        public void Action()
        {
            
        }
    }
}
