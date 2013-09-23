using System.Collections.Generic;
using Communication.Interfaces;
using Communication.Logic;
using Data.Enums.SkillEngine;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using Network;
using Network.Server;

namespace Tera.Services
{
    class SkillsLearnService : ISkillsLearnService
    {
        private Dictionary<int, int> MountSkillBooks = new Dictionary<int, int>();

        public void Init()
        {
            List<int> costs = new List<int>();

            #region cost

            double x1 = 2, y1 = 35,
                         x2 = 6, y2 = 112,
                         x3 = 10, y3 = 228;

            double a = (y3 - ((x3*(y2 - y1) + x2*y1 - x1*y2)/(x2 - x1)))/(x3*(x3 - x1 - x2) + x1*x2);
            double b = (y2 - y1)/(x2 - x1) - a*(x1 + x2);
            double c = ((x2*y1 - x1*y2)/(x2 - x1)) + a*x1*x2;

            for (int i = 0; i < 11; i++)
                costs.Add((int) (a * i * i + b * i + c));

            x1 = 10; y1 = 228;
            x2 = 14; y2 = 438;
            x3 = 26; y3 = 4416;

            a = (y3 - ((x3 * (y2 - y1) + x2 * y1 - x1 * y2) / (x2 - x1))) / (x3 * (x3 - x1 - x2) + x1 * x2);
            b = (y2 - y1) / (x2 - x1) - a * (x1 + x2);
            c = ((x2 * y1 - x1 * y2) / (x2 - x1)) + a * x1 * x2;

            for (int i = 11; i < 27; i++)
                costs.Add((int)(a * i * i + b * i + c));

            x1 = 26; y1 = 4416;
            x2 = 30; y2 = 7222;
            x3 = 38; y3 = 25890;

            a = (y3 - ((x3 * (y2 - y1) + x2 * y1 - x1 * y2) / (x2 - x1))) / (x3 * (x3 - x1 - x2) + x1 * x2);
            b = (y2 - y1) / (x2 - x1) - a * (x1 + x2);
            c = ((x2 * y1 - x1 * y2) / (x2 - x1)) + a * x1 * x2;

            for (int i = 27; i < 39; i++)
                costs.Add((int)(a * i * i + b * i + c));

            x1 = 38; y1 = 25890;
            x2 = 44; y2 = 64175;
            x3 = 60; y3 = 441966;

            a = (y3 - ((x3 * (y2 - y1) + x2 * y1 - x1 * y2) / (x2 - x1))) / (x3 * (x3 - x1 - x2) + x1 * x2);
            b = (y2 - y1) / (x2 - x1) - a * (x1 + x2);
            c = ((x2 * y1 - x1 * y2) / (x2 - x1)) + a * x1 * x2;

            for (int i = 39; i < 100; i++)
                costs.Add((int)(a * i * i + b * i + c));

            #endregion

            foreach (var skillsByTemplate in Data.Data.UserSkills)
            {
                List<int> toRemove = new List<int>();

                foreach (var userSkill in skillsByTemplate.Value)
                {
                    try
                    {
                        Skill skill = Data.Data.Skills[0][skillsByTemplate.Key][userSkill.Value.SkillId];

                        if (skill.Type == SkillType.Mount)
                            toRemove.Add(userSkill.Key);
                        else
                        {
                            if (userSkill.Value.SkillId == userSkill.Value.PrevSkillId)
                                userSkill.Value.PrevSkillId = 0;

                            userSkill.Value.Cost = costs[userSkill.Value.Level];
                        }
                    }
                    catch
                    {
                        toRemove.Add(userSkill.Key);
                    }
                }

                foreach (var i in toRemove)
                    skillsByTemplate.Value.Remove(i);
            }

            #region Skill books
            MountSkillBooks.Add(382, 111195);
            MountSkillBooks.Add(413, 111115);
            MountSkillBooks.Add(20, 111111);
            MountSkillBooks.Add(406, 111199);
            MountSkillBooks.Add(304, 111120);
            MountSkillBooks.Add(414, 111119);
            MountSkillBooks.Add(417, 111116);
            MountSkillBooks.Add(383, 111196);
            MountSkillBooks.Add(21, 111112);
            MountSkillBooks.Add(384, 111197);
            MountSkillBooks.Add(380, 111193);
            MountSkillBooks.Add(316, 111123);
            MountSkillBooks.Add(416, 111118);
            MountSkillBooks.Add(378, 111191);
            MountSkillBooks.Add(385, 111198);
            MountSkillBooks.Add(425, 111192);
            MountSkillBooks.Add(415, 111117);
            MountSkillBooks.Add(412, 111126);
            MountSkillBooks.Add(381, 111194);
            #endregion
        }

        public List<UserSkill> GetSkillLearnList(Player player)
        {
            List<UserSkill> skillList = new List<UserSkill>();

            foreach (var userSkill in Data.Data.UserSkills[player.TemplateId].Values)
            {
                if (!Data.Data.Skills[0][player.TemplateId].ContainsKey(userSkill.SkillId))
                    continue;

                bool add = true;

                int skillId = userSkill.SkillId;
                for (int i = 0 ; i < 20 ; i++)
                {
                    if (player.Skills.Contains(skillId))
                    {
                        add = false;
                        break;
                    }

                    skillId += 100; //NextLevel

                    if (!Data.Data.UserSkills[player.TemplateId].ContainsKey(skillId))
                        break;
                }

                if (add)
                    skillList.Add(userSkill);
            }

            return skillList;
        }

        public void BuySkill(Player player, int skillId, bool isActive)
        {
            if (!Data.Data.UserSkills[player.TemplateId].ContainsKey(skillId))
            {
                SystemMessages.YouFailedToLearnThatSkill.Send(player);
                return;
            }

            UserSkill skill = Data.Data.UserSkills[player.TemplateId][skillId];

            if (player.GetLevel() < skill.Level)
            {
                SystemMessages.YouFailedToLearnThatSkill.Send(player);
                return;
            }

            if (skill.PrevSkillId != 0 && !player.Skills.Contains(skill.PrevSkillId))
            {
                SystemMessages.YouFailedToLearnThatSkill.Send(player);
                return;
            }

            if (!Communication.Global.StorageService.RemoveMoney(player, player.Inventory, skill.Cost))
            {
                SystemMessages.YouFailedToLearnThatSkill.Send(player);
                return;
            }

            for (int i = 0; i < player.Skills.Count; i++)
            {
                if (player.Skills[i] == skill.PrevSkillId)
                {
                    player.Skills[i] = skillId;
                    PlayerLogic.SkillPurchased(player, skill);
                    return;
                }
            }

            if (player.Skills.Contains(skillId))
                return;
            player.Skills.Add(skillId);
            PlayerLogic.SkillPurchased(player, skill);
        }

        public void LearnMountSkill(Player player, int skillId)
        {
            if(!Data.Data.Skills[0][player.TemplateId].ContainsKey(skillId))
                return;

            if (player.Skills.Contains(skillId))
                return;

            player.Skills.Add(skillId);

            new SpSkillList(player).Send(player);

            Communication.Global.QuestEngine.OnPlayerLearnSkill(player);
        }

        public void UseSkillBook(Player player, int bookId)
        {
            if(MountSkillBooks.ContainsKey(bookId))
                LearnMountSkill(player, MountSkillBooks[bookId]);
            //todo other skill books (if there are any)
        }

        public void Action()
        {
            
        }
    }
}
