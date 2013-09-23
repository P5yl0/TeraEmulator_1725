using System.Collections.Generic;
using System.Linq;
using Communication;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using Tera.AiEngine;
using Tera.SkillEngine;
using Utils;

namespace Tera.Controllers
{
    class NpcBattleController
    {
        public Npc Npc;

        protected Dictionary<Creature, int> Damage = new Dictionary<Creature, int>();

        protected Dictionary<Creature, int> Hate = new Dictionary<Creature, int>();

        protected object Lock = new object();

        protected List<Skill> AttackSkills = new List<Skill>();

        protected List<Skill> MoveSkills = new List<Skill>();

        protected int SkillsCount = 0;

        protected Skill SelectedSkill = null;

        protected long LastMoveUts = 0;

        public NpcBattleController(Npc npc)
        {
            Npc = npc;

            if (!Data.Data.Skills.ContainsKey(npc.NpcTemplate.HuntingZoneId))
                return;

            if (!Data.Data.Skills[npc.NpcTemplate.HuntingZoneId].ContainsKey(npc.NpcTemplate.Id))
                return;

            var skills = Data.Data.Skills[npc.NpcTemplate.HuntingZoneId][npc.NpcTemplate.Id];

            foreach (var skill in skills.Values)
            {
                if ((skill.Id / 100) % 10 == 1)
                {
                    if (skill.TotalAtk > 0)
                        AttackSkills.Add(skill);
                }
                else
                    MoveSkills.Add(skill);
            }

            SkillsCount = AttackSkills.Count + MoveSkills.Count;
        }

        public void Reset()
        {
            Damage.Clear();
            Hate.Clear();
            SelectedSkill = null;
        }

        public void Release()
        {
            Npc = null;

            Damage.Clear();
            Damage = null;

            Hate.Clear();
            Hate = null;

            Lock = null;
        }

        public Creature GetKiller()
        {
            Creature creature = null;
            int maxDamage = 0;

            foreach (var d in Damage)
            {
                if (d.Value > maxDamage)
                {
                    creature = d.Key;
                    maxDamage = d.Value;
                }
            }

            return creature;
        }

        public void DealExp()
        {
            long totalExp = Data.Data.NpcExperience[Npc.GetLevel()];
            long totalDamage = Damage.Aggregate<KeyValuePair<Creature, int>, long>(0, (current, d) => current + d.Value);

            foreach (var d in Damage)
            {
                Player player = d.Key as Player;
                if (player == null)
                    continue;

                if (player.Party != null)
                    Global.PartyService.AddExp(player, totalExp * d.Value / totalDamage);
                else
                    Global.PlayerService.AddExp(player, totalExp * d.Value / totalDamage, Npc);
            }

            foreach (Player player in Damage.Select(d => d.Key as Player).Where(player => player != null && player.Party != null && player.Party.Exp > 0))
                Global.PartyService.ReleaseExp(player);
        }

        public void Action()
        {
            if (SkillsCount == 0)
                return;

            if (Npc.Attack != null && !Npc.Attack.IsFinished)
                return;

            lock (Lock)
            {
                Creature target = null;
                int maxHate = int.MinValue;

                foreach (var hate in Hate)
                {
                    if (hate.Key.Position.DistanceTo(Npc.Position) > 1500 || hate.Key.LifeStats.IsDead())
                    {
                        Damage.Remove(hate.Key);
                        Hate.Remove(hate.Key);
                        return;
                    }

                    if (hate.Value > maxHate)
                    {
                        target = hate.Key;
                        maxHate = hate.Value;
                    }
                }

                Npc.Target = target;
            }

            if (Npc.Target == null)
                return;

            if (SelectedSkill == null)
            {
                if (Funcs.Random().Next(0, 1000) < 250)
                {
                    Global.SkillEngine.UseSkill(Npc, MoveSkills[Funcs.Random().Next(0, MoveSkills.Count)]);
                    return;
                }

                SelectedSkill = AttackSkills[Funcs.Random().Next(0, AttackSkills.Count)];
            }

            int distance = SeUtils.GetAttackDistance(SelectedSkill);

            if (Npc.Position.DistanceTo(Npc.Target.Position.X, Npc.Target.Position.Y) > distance + 30)
            {
                long now = Funcs.GetCurrentMilliseconds();
                if (now > LastMoveUts + 1000)
                {
                    LastMoveUts = now;
                    ((NpcAi) Npc.Ai).MoveController.MoveTo(Npc.Target.Position, distance);
                }
                return;
            }

            ((NpcAi) Npc.Ai).MoveController.Stop();

            Global.SkillEngine.UseSkill(Npc, SelectedSkill);
            SelectedSkill = null;
        }

        public void AddDamage(Creature attacker, int damage)
        {
            lock (Lock)
            {
                if (!Damage.ContainsKey(attacker))
                    Damage.Add(attacker, damage);
                else
                    Damage[attacker] += damage;
            }
        }

        public void AddAggro(Creature attacker, int hate)
        {
            lock (Lock)
            {
                if (!Hate.ContainsKey(attacker))
                    Hate.Add(attacker, hate);
                else
                    Hate[attacker] += hate;
            }
        }

        public bool IsHateCreature(Creature creature)
        {
            return Hate.ContainsKey(creature);
        }
    }
}
