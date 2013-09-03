using Communication.Logic;
using Data.Enums;
using Data.Structures.Player;
using Network.Server;

namespace Tera.SkillEngine.Effects
{
    class EfRegenHp : EfDefault
    {
        public override void Tick()
        {
            switch (Effect.Method)
            {
                case 2:
                    CreatureLogic.HpChanged(Creature, Creature.LifeStats.PlusHp((int)Effect.Value), Creature);
                    break;
                case 3: //Percent
                    CreatureLogic.HpChanged(Creature, Creature.LifeStats.PlusHp((int)(Creature.MaxHp * Effect.Value)), Creature);
                    break;
                default:
                    Player player = Creature as Player;
                    if (player != null)
                        new SpChatMessage("Unknown method " + Effect.Method + " for RegenHp effect.", ChatType.System).Send(player);
                    break;
            }
        }
    }
}
