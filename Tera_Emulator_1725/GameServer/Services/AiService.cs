using Communication.Interfaces;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Gather;
using Data.Structures.Npc;
using Data.Structures.Objects;
using Data.Structures.Player;
using Tera.AiEngine;

namespace Tera.Services
{
    class AiService : IAiService
    {
        public IAi CreateAi(Creature creature)
        {
            if (creature is Player)
                return new PlayerAi();

            if (creature is Npc)
                return new NpcAi();

            if (creature is Projectile)
                return new ProjectileAi();

            if (creature is Gather)
                return new GatherAi();

            return new DefaultAi();
        }

        public void Action()
        {
            
        }
    }
}
