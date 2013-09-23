using System.Collections.Generic;
using Data.Enums.SkillEngine;
using Data.Interfaces;
using Data.Structures.SkillEngine;

namespace Tera.AdminEngine.AdminCommands
{
    class Mount : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            try
            {
                switch (msg.Split(' ')[0])
                {
                    case "learnall":
                        foreach (KeyValuePair<int, Skill> skill in Data.Data.Skills[0][connection.Player.TemplateId])
                        {
                            if(skill.Value.Type == SkillType.Mount)
                                Communication.Global.SkillsLearnService.LearnMountSkill(connection.Player, skill.Value.Id);
                        }
                        break;
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
