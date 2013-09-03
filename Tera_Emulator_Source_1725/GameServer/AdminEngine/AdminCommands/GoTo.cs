using System;
using Communication;
using Data.Enums;
using Data.Interfaces;
using Data.Structures.Player;
using Data.Structures.World;
using Network.Server;
using Tera.Services;
using Utils;

namespace Tera.AdminEngine.AdminCommands
{
    internal class GoTo : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            try
            {
                if (msg.Length == 0)
                {
                    new SpChatMessage("Maps:", ChatType.System).Send(connection);
                    foreach (var map in MapService.Maps)
                        new SpChatMessage("" + map.Key, ChatType.System).Send(connection);
                    return;
                }

                Player player = connection.Player;

                string[] options = msg.Split(' ');

                switch (options[0].ToLower())
                {
                    case "pos":
                        Global.TeleportService.ForceTeleport(player,
                                                             new WorldPosition
                                                                 {
                                                                     Heading = player.Position.Heading,
                                                                     MapId = player.Position.MapId,
                                                                     X = float.Parse(options[1]),
                                                                     Y = float.Parse(options[2]),
                                                                     Z = float.Parse(options[3])
                                                                 });
                        break;

                    case "islandofdawn":
                        Global.TeleportService.ForceTeleport(player,
                                                             new WorldPosition
                                                                 {
                                                                     Heading = 32767,
                                                                     MapId = 13,
                                                                     X = 93492,
                                                                     Y = -88216,
                                                                     Z = -4523
                                                                 });
                        break;
                    case "velica":
                        Global.TeleportService.ForceTeleport(player,
                                                             new WorldPosition
                                                                 {
                                                                     Heading = 22242,
                                                                     MapId = 1,
                                                                     X = 1600.987F,
                                                                     Y = 3041.877F,
                                                                     Z = 1743.736F
                                                                 });
                        break;
                    
                    case "castanica":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = -19232,
                                MapId = 7004,
                                X = 85811,
                                Y = 75075,
                                Z = 1892
                            });
                        break;
                    case "popolion":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = -8220,
                                MapId = 1,
                                X = -12336,
                                Y = 49937,
                                Z = 2872
                            });
                        break;
                    case "poraelinu":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 28476,
                                MapId = 1,
                                X = -34848,
                                Y = 33402,
                                Z = 2037
                            });
                        break;
                    case "lumbertown":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 24766,
                                MapId = 7001,
                                X = 91827,
                                Y = -2122,
                                Z = 714
                            });
                        break;
                    case "allemantheia":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = -18200,
                                MapId = 2,
                                X = -7072,
                                Y = -7251,
                                Z = 6701
                            });
                        break;
                    case "cresentia":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 24320,
                                MapId = 7001,
                                X = 97181,
                                Y = 19028,
                                Z = 3327
                            });
                        break;
                    case "tulufan":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 13350,
                                MapId = 7003,
                                X = -55703,
                                Y = -38479,
                                Z = 2476
                            });
                        break;
                    case "cutthroatharbor":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 8952,
                                MapId = 7004,
                                X = 48626,
                                Y = 70159,
                                Z = 105
                            });
                        break;
                    case "chebika":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = -14694,
                                MapId = 7003,
                                X = -34177,
                                Y = -21420,
                                Z = 276
                            });
                        break;
                    case "kaiator":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = -32354,
                                MapId = 3,
                                X = -13081,
                                Y = 8464,
                                Z = 4219
                            });
                        break;
                    case "zulifarfortress":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 16208,
                                MapId = 8001,
                                X = 38381,
                                Y = -3348,
                                Z = 1289
                            });
                        break;
                    case "habere":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 5,
                                MapId = 7022,
                                X = -5811,
                                Y = 59212,
                                Z = 5556
                            });
                        break;
                    case "kanastria":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 536,
                                MapId = 8001,
                                X = 43863,
                                Y = 19898,
                                Z = 2860
                            });
                        break;
                    case "pathfinderpost":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = -1152,
                                MapId = 3,
                                X = 12885,
                                Y = 24311,
                                Z = 134
                            });
                        break;
                    case "scytherafae":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 30078,
                                MapId = 3,
                                X = -56341,
                                Y = -24046,
                                Z = 2166
                            });
                        break;
                    case "dragonfall":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = -14236,
                                MapId = 3,
                                X = -36248,
                                Y = 7892,
                                Z = 4536
                            });
                        break;
                    case "tria":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 28278,
                                MapId = 7011,
                                X = 49593,
                                Y = -4503,
                                Z = 4355
                            });
                        break;
                    case "tralion":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 28874,
                                MapId = 7011,
                                X = 56071,
                                Y = -39064,
                                Z = 3074
                            });
                        break;
                    case "elenea":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 1085,
                                MapId = 7014,
                                X = -54555,
                                Y = -11760,
                                Z = 2181
                            });
                        break;
                    case "frontera":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = -25122,
                                MapId = 2,
                                X = -41305,
                                Y = 56028,
                                Z = 104
                            });
                        break;
                    case "acarum":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = 1953,
                                MapId = 7012,
                                X = -10661,
                                Y = -66969,
                                Z = 2518
                            });
                        break;
                    case "bleakrock":
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = -23409,
                                MapId = 7012,
                                X = -41742,
                                Y = -74748,
                                Z = 1264
                            });
                        break;
                    //old to be checked...
                    case "loc3":
                        Global.TeleportService.ForceTeleport(player,
                                                             new WorldPosition
                                                                 {
                                                                     Heading = 28242,
                                                                     MapId = 7001,
                                                                     X = 91217.6F,
                                                                     Y = -2416.63F,
                                                                     Z = 683.142F
                                                                 });
                        break;
                   //old to be checked
                    case "dang1":
                        Global.TeleportService.ForceTeleport(connection.Player, new WorldPosition
                        {
                            Heading = -32651,
                            MapId = 9036,
                            X =
                                BitConverter.ToSingle(
                                    "00438B47".HexSringToBytes(), 0),
                            Y =
                                BitConverter.ToSingle(
                                    "004027C6".HexSringToBytes(), 0),
                            Z =
                                BitConverter.ToSingle(
                                    "00002DC3".HexSringToBytes(), 0)
                        });
                        break;
                    case "prolog":
                        Global.TeleportService.ForceTeleport(player,
                                                             new WorldPosition
                                                                 {
                                                                     Heading = 180,
                                                                     MapId = 9015,
                                                                     X = BitConverter.ToSingle("00E0C745".HexSringToBytes(), 0),
                                                                     Y = BitConverter.ToSingle("003E5BC7".HexSringToBytes(), 0),
                                                                     Z = BitConverter.ToSingle("00001442".HexSringToBytes(), 0)
                                                                 });
                        break;

                    case "prolog_dang":
                        Global.TeleportService.ForceTeleport(player,
                                                             new WorldPosition
                                                                 {
                                                                     Heading = 0,
                                                                     MapId = 9016,
                                                                     X = BitConverter.ToSingle("31802FC7".HexSringToBytes(), 0),
                                                                     Y = BitConverter.ToSingle("341C6BC7".HexSringToBytes(), 0),
                                                                     Z = BitConverter.ToSingle("BDD8C1C5".HexSringToBytes(), 0)
                                                                 });
                        break;

                    case "all":
                        //Global.PlayerService.TeleportPlayer(PlayerService.GetPlayerByName(options[1]), player.Position);
                        break;
                    case "optional":
                        Global.TeleportService.ForceTeleport(player,
                                                             new WorldPosition
                                                                 {
                                                                     Heading = 32767,
                                                                     MapId = Convert.ToInt32(options[1]),
                                                                     X = Convert.ToInt32(options[2]),
                                                                     Y = Convert.ToInt32(options[3]),
                                                                     Z = Convert.ToInt32(options[4])
                                                                 });
                        break;

                    default:
                        int mapId = int.Parse(msg);
                        Global.TeleportService.ForceTeleport(connection.Player, MapService.Maps[mapId][0].Npcs[0].Position);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("AdminEngine: Speed:", ex);
            }
        }
    }
}
