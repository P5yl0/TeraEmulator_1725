using System.Linq;
using Data.Enums;
using Data.Structures.SkillEngine;

namespace Tera.SkillEngine.Patches
{
    class MysticPatch : ASkillPatch
    {
        public override void Patch()
        {
            PlayerClass = PlayerClass.Mystic;

            PatchAll(180100, PatchArunsVitae);
        }

        private void PatchArunsVitae(Skill skill)
        {
            skill.TargetingList.Last().AreaList.Last().DropItem = 8007 + skill.Level;
        }
    }
}
