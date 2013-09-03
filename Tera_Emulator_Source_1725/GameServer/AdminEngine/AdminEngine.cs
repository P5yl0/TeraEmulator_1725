using System;
using System.Collections.Generic;
using Communication.Interfaces;
using Data.Interfaces;
using Data.Structures.Player;
using Tera.AdminEngine.AdminCommands;
using Utils;

namespace Tera.AdminEngine
{
    class AdminEngine : IAdminEngine
    {
        public Dictionary<string, ACommand> AdminCommands = new Dictionary<string, ACommand>();
        public Dictionary<string, ACommand> UserCommands = new Dictionary<string, ACommand>();

        public Dictionary<IConnection, WaitMessageHandle> WaitValueHandles = new Dictionary<IConnection, WaitMessageHandle>();

        public AdminEngine()
        {
            AdminCommands.Add("additem", new AddItem());
            AdminCommands.Add("addskill", new AddSkill());

            AdminCommands.Add("addgold", new AddGold());
            AdminCommands.Add("speed", new Speed());
            AdminCommands.Add("setlevel", new SetLevel());
            AdminCommands.Add("jumpto", new JumpTo());
            AdminCommands.Add("notice", new Notice());
            AdminCommands.Add("systemnote", new SystemNote());

            AdminCommands.Add("reload", new Reload());
            AdminCommands.Add("spawn", new Spawn());
            AdminCommands.Add("checkgeo", new CheckGeo());
            AdminCommands.Add("quest", new Quest());
            AdminCommands.Add("craft", new Craft());
            AdminCommands.Add("goto", new GoTo());
            AdminCommands.Add("npc", new NpcCommand());
            AdminCommands.Add("camp", new CampfireInfo());
            AdminCommands.Add("mount", new Mount());
            AdminCommands.Add("viewmember", new ViewMember());

            AdminCommands.Add("script", new AdminCommands.Script());

            AdminCommands.Add("test", new Test());

            //User commands:

            UserCommands.Add("unstuck", new Unstuck());
        }

        public bool ProcessChatMessage(IConnection connection, string message)
        {
            string msg = message.Replace("<FONT>", "").Replace("</FONT>", "");

            if (WaitValueHandles.ContainsKey(connection))
            {
                WaitMessageHandle handle = WaitValueHandles[connection];
                handle.SendValue(msg);
                return true;
            }

            if (msg.Length < 2)
                return false;

            if (msg[0] == '!' && connection.Account.isGM == true)
            {
                string cmd = msg.Substring(1).Split(' ')[0];
                if (AdminCommands.ContainsKey(cmd))
                {
                    try
                    {
                        AdminCommands[cmd].Process(connection, msg.Substring(cmd.Length + 1).Trim());
                    }
                    catch(Exception ex)
                    {
                        Log.WarnException("AdminCommand: Process:", ex);
                    }
                    return true;
                }

            }
            else if (msg[0] == '!' || msg[0] == '.')
            {
                string cmd = msg.Substring(1).Split(' ')[0];
                if (UserCommands.ContainsKey(cmd))
                {
                    try
                    {
                        UserCommands[cmd].Process(connection, msg.Substring(cmd.Length + 1).Trim());
                    }
                    catch (Exception ex)
                    {
                        Log.WarnException("UserCommand: Process:", ex);
                    }
                    return true;
                }
            }

            return false;
        }

        public void Action()
        {
            
        }
    }
}
