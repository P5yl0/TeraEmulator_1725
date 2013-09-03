using Communication.Logic;
using Data.Enums;
using Data.Structures.Player;
using Network.Server;

namespace Tera.SkillEngine.Effects
{
    class EfRegenMp : EfDefault
    {
        public override void Tick()
        {
            switch (Effect.Method)
            {
                case 2:
                    CreatureLogic.MpChanged(Creature, Creature.LifeStats.PlusMp((int) Effect.Value), Creature);
                    break;
                case 3: //Percent
                    CreatureLogic.MpChanged(Creature, Creature.LifeStats.PlusMp((int)(Creature.MaxMp * Effect.Value)), Creature);
                    break;
                default:
                    Player player = Creature as Player;
                    if (player != null)
                        new SpChatMessage("Unknown method " + Effect.Method + " for RegenMp effect.", ChatType.System).Send(player);
                    break;
            }
        }
    }
}
