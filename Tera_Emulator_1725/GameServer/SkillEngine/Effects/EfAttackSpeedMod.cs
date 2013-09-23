using Communication.Logic;
using Data.Enums;
using Data.Structures.Player;
using Network.Server;

namespace Tera.SkillEngine.Effects
{
    class EfAttackSpeedMod : EfDefault
    {
        public override void Init()
        {
            switch (Effect.Method)
            {
                case 2:
                    Creature.CreatureEffectsModifier.AttackSpeedMod += Effect.Value;
                    break;
                case 3: //Percent
                    Creature.CreatureEffectsModifier.AttackSpeedMod += Creature.GameStats.AttackSpeed * Effect.Value;
                    break;
                default:
                    Player player = Creature as Player;
                    if (player != null)
                        new SpChatMessage("Unknown method " + Effect.Method + " for LoseHp effect.", ChatType.System).Send(player);
                    break;
            }

            CreatureLogic.UpdateCreatureStats(Creature);
        }

        public override void Release()
        {
            switch (Effect.Method)
            {
                case 2:
                    Creature.CreatureEffectsModifier.AttackSpeedMod -= Effect.Value;
                    break;
                case 3: //Percent
                    Creature.CreatureEffectsModifier.AttackSpeedMod -= Creature.GameStats.AttackSpeed * Effect.Value;
                    break;
            }

            CreatureLogic.UpdateCreatureStats(Creature);

            base.Release();
        }
    }
}
