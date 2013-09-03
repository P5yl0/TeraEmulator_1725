using Communication;
using Data.Interfaces;

namespace Tera.AdminEngine.AdminCommands
{
    class SetLevel : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            try
            {
                int level = int.Parse(msg) - 1;
                Global.PlayerService.SetExp(connection.Player, Data.Data.PlayerExperience[level], null);
            }
            catch
            {
                //Nothing
            }
        }
    }
}
