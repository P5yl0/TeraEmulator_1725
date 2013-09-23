using System;
using Data.Enums.Craft;
using Data.Interfaces;
using Network.Server;

namespace Tera.AdminEngine.AdminCommands
{
    internal class Craft : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            string[] args = msg.Split(' ');

            switch (args[0])
            {
                case "start":
                    var cstat = (CraftStat)Enum.Parse(typeof(CraftStat), args[1]);
                    if (cstat.GetHashCode() > 6)
                        return;

                    new SpCraftWindow(cstat).Send(connection);
                    break;
                case "setstat":
                    var stat = (CraftStat) Enum.Parse(typeof (CraftStat), args[1]);

                    if (stat.GetHashCode() > 6)
                        return;

                    if (!connection.Player.PlayerCraftStats.CraftSkillCollection.ContainsKey(stat))
                        connection.Player.PlayerCraftStats.CraftSkillCollection.Add(stat, 1);

                    connection.Player.PlayerCraftStats.CraftSkillCollection[stat] = Convert.ToInt16(args[2]);

                    Communication.Global.CraftService.UpdateCraftStats(connection.Player);

                    break;

                case "addrecipe":
                    Communication.Global.CraftService.AddRecipe(connection.Player, Convert.ToInt32(args[1]));
                    break;
            }
        }
    }
}
