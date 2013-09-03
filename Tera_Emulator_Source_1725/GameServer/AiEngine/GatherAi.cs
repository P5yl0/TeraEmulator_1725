using System;
using Data.Structures.Creature;
using Data.Structures.Gather;
using Utils;

namespace Tera.AiEngine
{
    class GatherAi : DefaultAi
    {
        public Gather Gather;

        public long DieUtc = Funcs.GetCurrentMilliseconds();

        public const int RespawnUtc = 30000;

        public override void Init(Creature creature)
        {
            base.Init(creature);

            Gather = (Gather) creature;
        }

        public override void Action()
        {
            if (Gather.CurrentGatherCounter == 0 && Funcs.GetCurrentMilliseconds() - DieUtc > RespawnUtc)
                Gather.CurrentGatherCounter = new Random().Next(1, 3);
        }

        public void ProcessGather()
        {
            Gather.CurrentGatherCounter--;

            if (Gather.CurrentGatherCounter <= 0)
            {
                Gather.CurrentGatherCounter = 0;
                DieUtc = Funcs.GetCurrentMilliseconds();
            }
        }
    }
}
