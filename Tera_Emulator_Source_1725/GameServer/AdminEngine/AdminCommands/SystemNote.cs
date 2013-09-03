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
 * Class SystemNote
 * Allow GMs to send maintenance and shutdown commands
 * Usage : `systemnote {type} {time_till|shutdowntype} {time_last}
 * type string
 * time_till int | shutdowntype string
 * time_last int
 * Types : maint, shutdown
 * ------------------------------
 * Copyright (c) 2013 Uebari, formatme
 * TeraEmulator
 * Version: 1725-001 Beta
 * This source is Open under GPL License
 * --------------------------------
*/
namespace Tera.AdminEngine.AdminCommands
{
    class SystemNote : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            try
            {
                string[] args = msg.Split(' ');
                int minute = 60;
                int hour = 3600;
                int settime = 0;

                // What type of Message
                switch (args[0].ToLower())
                {
                    case "maint":
                        settime = int.Parse(args[1]); // Time Given...
                        if (settime >= hour)
                        {
                            string time = getDivisible(settime, hour);
                            string last = getDivisible(int.Parse(args[2]), hour);
                            new SpChatMessage("Maintance will begin in " + time + " Hour(s) and will last for " + last + " Hour(s)!", ChatType.System).Send(connection);
                            //new SpChatMessage("Maintance will begin in " + time + " Hour(s) and will last for " + last + " Hour(s)!", ChatType.Area).Send(connection); // Deprec
                            new SpChatMessage("Maintance will begin in " + time + " Hour(s) and will last for " + last + " Hour(s)!", ChatType.Notice).Send(connection);
                        } // Hours
                        else
                        {
                            string time = getDivisible(settime, minute);
                            string last = getDivisible(int.Parse(args[2]), minute);
                            new SpChatMessage("Maintance will begin in " + time + " Minute(s) and will last for " + last + " Minute(s)!", ChatType.System).Send(connection);
                            //new SpChatMessage("Maintance will begin in " + time + " Minute(s) and will last for " + last + " Minute(s)!", ChatType.Area).Send(connection); // Deprec
                            new SpChatMessage("Maintance will begin in " + time + " Minute(s) and will last for " + last + " Minute(s)!", ChatType.Notice).Send(connection);
                        } // Minutes
                        break;  // We are getting ready for maintenance
                    case "shutdown":
                        switch (args[1].ToLower())
                        {
                            case "graceful":
                                // System will grace for 1 minute...
                                new SpChatMessage("Server will shutdown in 1 Minute", ChatType.System).Send(connection);
                                Tera.GameServer.ShutdownServer();
                                break;
                            case "force":
                                Tera.GameServer.ShutdownServer();
                                //Tera.GameServer.ShutdownServerForce();
                                break;
                        }
                        break;
                    case "savecache":
                        new SpChatMessage("Manually saving Cache!", ChatType.Notice).Send(connection);
                        Data.Cache.SaveData();
                        break;
                }
            }
            catch (Exception e)
            {
                new SpChatMessage("Wrong Syntax!\n Type `systemnote {type} {option1} {option2}\nOptions are usually in seconds!", ChatType.Notice).Send(connection);
                Log.Warn(e.ToString());
            }
        }

        public string getDivisible(int input, int divide)
        {
            int value = input / divide;
            return value.ToString();
        }
    }
}
