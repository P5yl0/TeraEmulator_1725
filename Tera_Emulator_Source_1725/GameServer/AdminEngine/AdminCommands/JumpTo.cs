using System;
using Communication;
using Data.Enums;
using Data.Interfaces;
using Data.Structures.Player;
using Data.Structures.World;
using Network.Server;
using Tera.Services;
using Utils;

/**
 * Class JumpTo
 * Allows GM to Summon / Teleport Player or Party
 * Usage : `jumpto {type} {user}
 * type string
 * user string
 * Types : summon, teleport, psummon, teleport
 * ------------------------------
 * Copyright (c) 2013 Uebari, formatme
 * TeraEmulator
 * Version: 1725-001 Beta
 * This source is Open under GPL License
 * --------------------------------
*/
namespace Tera.AdminEngine.AdminCommands
{
    class JumpTo : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {

            try
            {
                string[] args = msg.Split(' ');
                
                // Our Target ?? OF COURSE!
                var target = Communication.Global.PlayerService.GetPlayerByName(args[1]);
                
                // Ourself
                Player player = connection.Player;

                switch (args[0].ToLower())
                {
                    // Summon Player
                    // The target shall come to us!
                    case "summon":
                        new SpChatMessage("You are summoning " + args[1], ChatType.Notice).Send(connection);
                        new SpChatMessage("Debug: Player - " + player.Position.ToString(), ChatType.Notice).Send(connection);
                        Global.TeleportService.ForceTeleport(target,
                            new WorldPosition
                            {
                                Heading = short.Parse(player.Position.Heading.ToString()),
                                MapId = int.Parse(player.Position.MapId.ToString()),
                                X = float.Parse(player.Position.X.ToString()),
                                Y = float.Parse(player.Position.Y.ToString()),
                                Z = float.Parse(player.Position.Z.ToString())
                            });
                        break;


                    // Teleport Player
                    case "teleport":
                        new SpChatMessage("You are teleporting to " + args[1], ChatType.Notice).Send(connection);
                        Global.TeleportService.ForceTeleport(player,
                            new WorldPosition
                            {
                                Heading = short.Parse(target.Position.Heading.ToString()),
                                MapId = int.Parse(target.Position.MapId.ToString()),
                                X = float.Parse(target.Position.X.ToString()),
                                Y = float.Parse(target.Position.Y.ToString()),
                                Z = float.Parse(target.Position.Z.ToString())
                            });
                        break;

                    /**
                     * Party functions have neither been tested nor implemented
                     * do not use the code.
                     */

                    // Summon Party
                    case "psummon":
                        break;

                    // Teleport Party
                    case "pteleport":
                        break;
                }
            }
            catch (Exception )//e)
            {
            }
        }
    }
}
