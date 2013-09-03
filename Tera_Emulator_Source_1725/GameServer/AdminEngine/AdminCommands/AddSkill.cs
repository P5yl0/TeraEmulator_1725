using Data.Enums;
using Data.Interfaces;
using Network.Server;

namespace Tera.AdminEngine.AdminCommands
{
    public class AddSkill : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            try
            {
                var args = msg.Split(' ');
                int skillId;

                if(!int.TryParse(args[1], out skillId))
                {
                    new SpChatMessage("Failed to parse skill id!", ChatType.Notice).Send(connection);
                    return;
                }

                var target = Communication.Global.PlayerService.GetPlayerByName(args[0]);

                if(target == null)
                {
                    new SpChatMessage("Cannot find a player with such name!", ChatType.Notice).Send(connection);
                    return;
                }

                foreach (var id in Data.Data.Skills[0][connection.Player.TemplateId].Keys)
                    if(id == skillId)
                    {
                        Communication.Global.SkillsLearnService.LearnMountSkill(target, skillId);
                        new SpChatMessage("Successfully added skill!", ChatType.Notice).Send(connection);
                        new SpChatMessage("GM added you a new skill! Please check your skill list.", ChatType.Notice).Send(target);
                    }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
                //Nothing
            }
        }
    }
}
