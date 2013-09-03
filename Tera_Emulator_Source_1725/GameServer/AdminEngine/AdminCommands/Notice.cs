using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication;
using Data.Enums;
using Data.Interfaces;
using Data.Structures.Player;
using Network.Server;
using Utils;

/**
 * Class JumpTo
 * Usage : `notice {message}
 * Allow GMs to send out messages to entire
 * server.
 * message string
 * ------------------------------
 * Copyright (c) 2013 Uebari, formatme
 * TeraEmulator
 * Version: 1725-001 Beta
 * This source is Open under GPL License
 * --------------------------------
*/
namespace Tera.AdminEngine.AdminCommands
{
    class Notice : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            try
            {
                string[] args = msg.Split('"');

                new SpChatMessage(args[0], ChatType.System).Send(connection);
            }
            catch (Exception e)
            {
                new SpChatMessage("Wrong Syntax!\n Type !notice \"{message\"", ChatType.Notice).Send(connection);
                Log.Warn(e.ToString());
            }
        }
    }
}
