using System;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using Utils;

namespace Tera.AiEngine
{
    internal class DefaultAi : IAi
    {
        protected Creature Creature;

        protected Npc Npc;

        protected Player Player;

        protected Random Random;

        public virtual void Init(Creature creature)
        {
            Creature = creature;
            Npc = creature as Npc;
            Player = creature as Player;

            Random = new Random((int) (DateTime.Now.Ticks + Funcs.Random().Next(int.MinValue, (int) Math.Abs(creature.Position.X))));
        }

        public virtual void Release()
        {
            Creature = null;
            Npc = null;
            Player = null;
            Random = null;
        }

        public virtual void Action()
        {
            
        }

        public virtual void OnUseSkill(Skill skill)
        {
            
        }

        public virtual void OnAttack(Creature target)
        {

        }

        public virtual void OnAttacked(Creature attacker, int damage)
        {

        }

        public virtual Creature GetKiller()
        {
            return null;
        }

        public virtual void DealExp()
        {
            
        }

        public virtual void DistanceToCreatureRecalculated(Creature creature, double distance)
        {
            
        }
    }
}