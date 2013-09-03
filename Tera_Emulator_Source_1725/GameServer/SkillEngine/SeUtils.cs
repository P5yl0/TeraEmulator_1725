using System.Collections.Generic;
using Communication;
using Data.Enums;
using Data.Enums.SkillEngine;
using Data.Structures.Creature;
using Data.Structures.Geometry;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using Data.Structures.World;
using Tera.Extensions;
using Utils;

namespace Tera.SkillEngine
{
    class SeUtils
    {
        public static int GetAttackDistance(Skill skill)
        {
            if (skill.TargetingList == null)
                return 0;

            int result = 0;

            for (int i = 0; i < skill.TargetingList.Count; i++)
            {
                if (skill.TargetingList[i].AreaList == null)
                    continue;

                for (int j = 0; j < skill.TargetingList[i].AreaList.Count; j++)
                {
                    int distance = (int) (skill.TargetingList[i].AreaList[j].MaxRadius +
                                          skill.TargetingList[i].AreaList[j].OffsetDistance);

                    if (distance > result)
                        result = distance; 
                }
            }

            return result;
        }

        public static int GetDuration(Skill skill)
        {
            int maxActionMoveCancelStartTime = 0;
            int maxAnimationDuration = 0;

            for (int i = 0; i < skill.Actions.Count; i++)
            {
                if (skill.Actions[i].MoveCancelStartTime > maxActionMoveCancelStartTime)
                    maxActionMoveCancelStartTime = skill.Actions[i].MoveCancelStartTime;

                if (skill.Actions[i].StageList == null)
                    continue;

                for (int j = 0; j < skill.Actions[i].StageList.Count; j++)
                {
                    for (int k = 0; k < skill.Actions[i].StageList[j].AnimationList.Count; k++)
                    {
                        var anim = skill.Actions[i].StageList[j].AnimationList[k];

                        if (anim.Animation.Duraction > maxAnimationDuration)
                            maxAnimationDuration = anim.Animation.Duraction;

                        if (anim.Duration > maxAnimationDuration)
                            maxAnimationDuration = anim.Duration;
                    }
                }
            }

            return (int) ((maxActionMoveCancelStartTime != 0 ? maxActionMoveCancelStartTime : maxAnimationDuration) / skill.TimeRate);
        }

        public static float CheckIntersections(Creature creature, short heading, Point3D moveVector, float distance)
        {
            if (distance <= 0f)
                return 0f;

            WorldPosition targetPosition = moveVector.Clone().Add(creature.Position).ToWorldPosition();

            double minDistance = distance;

            List<Creature> around = Global.VisibleService.FindTargets(creature, creature.Position, distance + 40, TargetingAreaType.All);
            for (int x = 0; x < around.Count; x++)
            {
                if (around[x] == creature)
                    continue;

                short diff = Geom.GetAngleDiff(heading, Geom.GetHeading(creature.Position, around[x].Position));
                if (diff > 90)
                    continue;

                double d = Geom.DistanceToLine(around[x].Position, creature.Position, targetPosition);

                if (d > 40)
                    continue;

                d = creature.Position.DistanceTo(around[x].Position) - 40;

                if (d <= 0)
                    return 0f;

                if (d < minDistance)
                    minDistance = d;
            }

            return (float)(minDistance / distance);
        }

        public static void DropItem(Creature creature, int itemId, int workParam, WorldPosition position, bool isDebug = false)
        {
            if (!isDebug)
                position =
                    Geom.GetNormal(position.Heading)
                        .Multiple(35)
                        .Add(position)
                        .SetZ(position.Z + 25)
                        .ToWorldPosition();

            creature.Instance.AddDrop(
                new Item
                {
                    Owner = creature as Player,
                    Npc = creature as Npc,

                    ItemId = itemId,
                    Count = 1,
                    Position = position,
                    Instance = creature.Instance,
                    WorkParam = workParam
                });
        }

        public static void DebugPoint(Creature creature, Point3D point)
        {
            DropItem(creature, 20000000, 0, point.ToWorldPosition(), true);

            Player player = creature as Player;
            if (player != null)
                player.Visible.Update();
        }

        public static void DebugPoint(Creature creature, WorldPosition position)
        {
            DropItem(creature, 20000000, 0, position.Clone(), true);

            Player player = creature as Player;
            if (player != null)
                player.Visible.Update();
        }

        public static void DebugLine(Creature creature, Point3D start, Point3D end)
        {
            const int iterations = 3;

            Point3D vector = Geom.GetNormal(Geom.GetHeading(start, end))
                .Multiple((float)(start.DistanceTo(end) / (iterations - 1)));

            for (int i = 0; i < iterations; i++)
            {
                DropItem(creature, 20000001, 0, start.ToWorldPosition(), true);
                start.Add(vector);
            }

            Player player = creature as Player;
            if (player != null)
                player.Visible.Update();
        }

        public static int CalculateDamage(Creature creature, Creature target, float skillAtk)
        {
            if (skillAtk <= 0.0)
                return 0;

            float atk = 1.0f;

            Player player = creature as Player;
            if (player != null)
            {
                atk = player.Attack.Args.IsItemSkill
                          ? 1
                          : player.GameStats.Attack;
            }

            if (target.GameStats.Defense <= 0)
                return (int) (atk*skillAtk);

            return (int) (atk*skillAtk/target.GameStats.Defense);
        }

        public static void UpdateAttackResult(Creature attacker, AttackResult result)
        {
            if(attacker is Player)
                PassivityProcessor.OnAttack((Player)attacker, result);
            if(result.Target is Player)
                PassivityProcessor.OnAttacked((Player)result.Target, result);
            TrySetCrit(attacker, result);
        }

        private static void TrySetCrit(Creature attacker, AttackResult result)
        {
            int chance = (attacker is Player) ? 10 : 30;
            chance += attacker.GameStats.CritChanse - result.Target.GameStats.CritResist;
            if (chance < 0)
                chance = 2;

            if (Funcs.Random().Next(0, 100) > chance)
                return;

            result.Damage *= attacker.GameStats.CritPower;
            result.AttackType = AttackType.Critical;
        }
    }
}
