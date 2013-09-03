using System;
using Data.Enums;
using Data.Structures.SkillEngine;

namespace Tera.SkillEngine.Patches
{
    abstract class ASkillPatch
    {
        protected PlayerClass PlayerClass;

        public abstract void Patch();

        protected void Patch(int skillId, Action<Skill> patchAction)
        {
            for (int sex = 0; sex < 2; sex++)
            {
                for (int race = 0; race < 6; race++)
                {
                    int sexRaceClass = 10101 + 200*race + 100*sex + PlayerClass.GetHashCode();

                    patchAction(Data.Data.Skills[0][sexRaceClass][skillId]);
                }
            }
        }

        protected void PatchAll(int baseSkillId, Action<Skill> patchAction)
        {
            for (int sex = 0; sex < 2; sex++)
            {
                for (int race = 0; race < 6; race++)
                {
                    int sexRaceClass = 10101 + 200 * race + 100 * sex + PlayerClass.GetHashCode();

                    if (!Data.Data.Skills[0].ContainsKey(sexRaceClass))
                        continue;

                    int levelShift = 0;

                    while (Data.Data.Skills[0][sexRaceClass].ContainsKey(baseSkillId + levelShift))
                    {
                        patchAction(Data.Data.Skills[0][sexRaceClass][baseSkillId + levelShift]);

                        levelShift += 100;
                    }
                }
            }
        }
    }
}
