using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.SkillEngine;
using Utils;

namespace Tera.SkillEngine.Effects
{
    abstract class EfDefault : IEffect
    {
        public Creature Creature;

        public Abnormality Abnormality;

        public AbnormalityEffect Effect;

        public bool IsUpdateStats = false;

        public long LastTick = Funcs.GetCurrentMilliseconds();

        //

        public virtual void Init()
        {
            //Instantly
            if (Effect.TickInterval == 0)
                Tick();
        }

        public virtual void Tick()
        {

        }

        public virtual void UpdateStats()
        {

        }

        //

        public void Action()
        {
            if (Effect.TickInterval == 0)
                return;

            long now = Funcs.GetCurrentMilliseconds();
            long nextTick = LastTick + Effect.TickInterval*1000;

            if (nextTick < now)
            {
                LastTick = nextTick;
                Tick();
            }
        }

        public virtual void SetImpact(CreatureEffectsImpact impact)
        {
            
        }

        public virtual void Release()
        {
            Creature = null;
            Abnormality = null;
            Effect = null;
        }
    }
}
