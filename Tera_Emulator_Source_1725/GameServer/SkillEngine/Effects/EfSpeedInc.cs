using Communication.Logic;
using Data.Enums;
using Data.Structures.Creature;
using Data.Structures.Player;
using Network.Server;

namespace Tera.SkillEngine.Effects
{
    class EfSpeedInc : EfDefault
    {
        public override void Init()
        {
            CreatureLogic.UpdateCreatureStats(Creature);
        }

        public override void SetImpact(CreatureEffectsImpact impact)
        {
            switch (Effect.Method)
            {
                case 2:
                    impact.MovementModificator += (short)Effect.Value;
                    break;
                case 3: //Percent
                    impact.MovementPercentModificator += Effect.Value;
                    break;
                default:
                    Player player = Creature as Player;
                    if (player != null)
                        new SpChatMessage("Unknown method " + Effect.Method + " for EfSpeedInc effect.", ChatType.System).Send(player);
                    break;
            }
        }

        public override void Release()
        {
            CreatureLogic.UpdateCreatureStats(Creature);

            base.Release();
        }
    }
}
