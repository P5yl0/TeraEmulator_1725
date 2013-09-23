using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication;
using Data.Enums;
using Data.Interfaces;
using Data.Structures.Player;
using Data.Structures.Creature;
using Network.Server;
using Communication.Logic;
using Network;
using Tera.SkillEngine;
using Utils;


/**
 * Class SetMode
 * Allows GMs to go into GodMode / Hide
 * Usage : `setmode {mode} {0|1}
 * 0 = False
 * 1 = True
 * ------------------------------
 * Copyright (c) 2013 Uebari, formatme
 * TeraEmulator
 * Version: 1725-001 Beta
 * This source is Open under GPL License
 * --------------------------------
*/
namespace Tera.AdminEngine.AdminCommands
{
    class SetMode : ACommand
    {
        protected Player Player;

        public override void Process(IConnection connection, string msg)
        {
            string[] args = msg.Split(' ');
        }
    }
}
