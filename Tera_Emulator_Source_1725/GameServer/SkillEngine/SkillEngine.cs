using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Communication;
using Communication.Interfaces;
using Communication.Logic;
using Data.Enums;
using Data.Enums.SkillEngine;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Geometry;
using Data.Structures.Npc;
using Data.Structures.Objects;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using Data.Structures.World;
using Network;
using Network.Server;
using Tera.Extensions;
using Tera.SkillEngine.Patches;
using Utils;

namespace Tera.SkillEngine
{
    internal class SkillEngine : Global, ISkillEngine
    {
        public AbnormalityProcessor AbnormalityProcessor = new AbnormalityProcessor();

        public void Init()
        {
            new MysticPatch().Patch();
        }

        public void UseSkill(IConnection connection, UseSkillArgs args)
        {
            Player player = connection.Player;
            Skill skill = args.GetSkill(player);

            if (skill == null)
                return;

            if (skill.ChargingStageList != null)
                args.IsChargeSkill = true;

            if (skill.Type == SkillType.Mount)
            {
                MountService.ProcessMount(player, skill.Id);
                new SpSkillCooldown(skill.Id, skill.Precondition.CoolTime).Send(player.Connection);
                return;
            }

            if (player.Attack != null && !player.Attack.IsFinished)
            {
                if (player.Attack.Args.IsDelaySkill)
                {
                    int stage = player.Attack.Stage;

                    if (player.Attack.Args.IsChargeSkill)
                    {
                        skill = player.Attack.Args.GetSkill(player);

                        if (skill.ChargingStageList != null && skill.ChargingStageList.ChargeStageList.Count > stage)
                        {
                            int useHp = (int) (skill.ChargingStageList.CostTotalHp*
                                               skill.ChargingStageList.ChargeStageList[stage].CostHpRate);

                            int useMp = (int) (skill.ChargingStageList.CostTotalMp*
                                               skill.ChargingStageList.ChargeStageList[stage].CostMpRate);

                            if (player.LifeStats.Hp <= useHp)
                            {
                                SystemMessages.YouCannotUseThatSkillAtTheMoment.Send(player.Connection);
                                stage = -1;
                            }
                            else if (player.LifeStats.Mp < useMp)
                            {
                                SystemMessages.NotEnoughMp.Send(player.Connection);
                                stage = -1;
                            }
                            else
                            {
                                if (useHp > 0)
                                    CreatureLogic.HpChanged(player, player.LifeStats.MinusHp(useHp));

                                if (useMp > 0)
                                    CreatureLogic.MpChanged(player, player.LifeStats.MinusMp(useMp));

                                args = new UseSkillArgs
                                           {
                                               SkillId = player.Attack.Args.SkillId + 10 + stage,
                                               StartPosition = player.Position.Clone(),
                                               TargetPosition = player.Attack.Args.TargetPosition,
                                           };
                            }
                        }
                        else
                            stage = -1;
                    }

                    player.Attack.Finish();

                    if (skill.BaseId == 20100 &&
                        (player.PlayerData.Class == PlayerClass.Berserker
                        || player.PlayerData.Class == PlayerClass.Lancer))
                    {
                        player.EffectsImpact.ResetChanges(player); //Set IsBlockFrontAttacks
                    }

                    if (player.Attack.Args.IsChargeSkill && stage != -1)
                        UseSkill(connection, args);

                    return;
                }

                if (args.SkillId / 10000 == player.Attack.Args.SkillId / 10000)
                {
                    player.Attack.Finish();

                    if (player.Attack.Args.Targets.Count == 0)
                        return;

                    args.Targets = new List<Creature>(player.Attack.Args.Targets);
                    ProcessSkill(player, args, skill);
                }

                return;
            }

            if (!CheckRequirements(player, skill))
                return;

            args.StartPosition.CopyTo(player.Position);

            ProcessSkill(player, args, skill);
        }

        public void UseSkill(IConnection connection, List<UseSkillArgs> argsList)
        {
            if (argsList.Count < 1)
                return;

            Player player = connection.Player;
            Skill skill = argsList[0].GetSkill(player);

            if (skill == null)
                return;

            if (!CheckRequirements(player, skill))
                return;

            if (player.Attack != null && !player.Attack.IsFinished)
                return;

            argsList[0].StartPosition.CopyTo(player.Position);

            ProcessSkill(player, argsList, skill);
        }

        public void UseSkill(Npc npc, Skill skill)
        {
            if (npc.Target == null)
                return;

            npc.Position.Heading = Geom.GetHeading(npc.Position, npc.Target.Position);

            ProcessSkill(npc, new UseSkillArgs
                {
                    IsTargetAttack = false,
                    SkillId = skill.Id + 0x40000000 + (npc.NpcTemplate.HuntingZoneId << 16),
                    StartPosition = npc.Position.Clone(),
                }, skill);
        }

        public void MarkTarget(IConnection connection, Creature target, int skillId)
        {
            Player player = connection.Player;

            if (player.Attack == null || player.Attack.Args.SkillId != skillId)
                return;

            player.Attack.Args.Targets.Add(target);

            new SpMarkTarget(target, skillId).Send(connection);
        }

        public void ReleaseAttack(Player player, int attackUid, int type)
        {
            if (type == 1)
            {
                if (player.Attack.Args.IsChargeSkill)
                    player.Attack.Finish();
            }

            //TODO: Other need?
        }

        public void UseProjectileSkill(Projectile projectile)
        {
            Creature creature = (Creature) projectile.Parent;

            if (projectile.Skill == null)
                return;

            ProcessSkill(creature, new UseSkillArgs
                {
                    IsTargetAttack = true,
                    SkillId = projectile.SkillId,
                    StartPosition = projectile.Position.Clone(),
                }, projectile.Skill, projectile);
        }

        public void AttackFinished(Creature creature)
        {
            Player player = creature as Player;
            if (player == null)
                return;

            Skill skill = player.Attack.Args.GetSkill(player);

            if (skill == null)
                return;

            if (skill.Precondition.CoolTime <= 0)
                return;

            //int skillId = skill.Id/10000 * 10000 + 100 + skill.Id%100;

            new SpSkillCooldown(skill.Id, skill.Precondition.CoolTime).Send(player.Connection);
        }

        private bool CheckRequirements(Player player, Skill skill)
        {
            if (player.PlayerMount != 0
                //|| !player.Skills.Contains(skill.BaseId)
                || player.LifeStats.Hp < skill.Precondition.Cost.Hp
                )
            {
                SystemMessages.YouCannotUseThatSkillAtTheMoment.Send(player.Connection);
                return false;
            }

            if (skill.Precondition.CoolTime > 0)
            {
                long cooldownUtc = 0;

                if (!player.SkillCooldowns.ContainsKey(skill.Id))
                    player.SkillCooldowns.Add(skill.Id, 0);
                else
                    cooldownUtc = player.SkillCooldowns[skill.Id];

                long now = Funcs.GetCurrentMilliseconds();

                if (cooldownUtc > now)
                    return false;

                player.SkillCooldowns[skill.Id] = now + skill.Precondition.CoolTime;
            }

            if (player.LifeStats.Mp < skill.Precondition.Cost.Mp)
            {
                SystemMessages.NotEnoughMp.Send(player.Connection);
                return false;
            }

            return true;
        }

        private void ProcessSkill(Creature creature, UseSkillArgs args, Skill skill, Projectile projectile = null)
        {
            bool isProjectileSkill = skill.Type == SkillType.Projectile || skill.Type == SkillType.Userslug;

            if (!isProjectileSkill)
            {
                if (skill.ChargingStageList == null || skill.ChargingStageList.ChargeStageList.Count == 0)
                {
                    if (skill.Precondition.Cost.Hp > 0)
                        CreatureLogic.HpChanged(creature, creature.LifeStats.MinusHp(skill.Precondition.Cost.Hp));

                    if (skill.Precondition.Cost.Mp > 0)
                        CreatureLogic.MpChanged(creature, creature.LifeStats.MinusMp(skill.Precondition.Cost.Mp));
                }

                if (!args.IsDelaySkill || args.IsDelayStart)
                {
                    if (args.TargetPosition.IsNull())
                    {
                        double angle = args.StartPosition.Heading*Math.PI/32768;

                        args.StartPosition.CopyTo(args.TargetPosition);

                        args.TargetPosition.X += 100*(float) Math.Cos(angle);
                        args.TargetPosition.Y += 100*(float) Math.Sin(angle);
                    }

                    // ReSharper disable ImplicitlyCapturedClosure
                    creature.Attack = new Attack(creature,
                                                 args,
                                                 () => GlobalLogic.AttackStageEnd(creature),
                                                 () => GlobalLogic.AttackFinished(creature));
                    // ReSharper restore ImplicitlyCapturedClosure

                    VisibleService.Send(creature, new SpAttack(creature, creature.Attack));

                    VisibleService.Send(creature, new SpAttackDestination(creature, creature.Attack));

                    if (!args.IsDelaySkill)
                        ProcessStages(creature, skill);
                    else
                    {
                        Player player = creature as Player;
                        if (player != null && skill.BaseId == 20100 &&
                            (player.PlayerData.Class == PlayerClass.Berserker
                            || player.PlayerData.Class == PlayerClass.Lancer))
                        {
                            player.EffectsImpact.ResetChanges(player); //Set IsBlockFrontAttacks
                        }
                    }

                    ProcessMove(creature, skill);
                }
            }
            else
            {
                creature.Attack.Args.IsTargetAttack = args.IsTargetAttack;
                creature.Attack.Args.Targets = args.Targets;

                ProcessProjectileTargets(creature, skill, projectile);
            }

            AiLogic.OnUseSkill(creature, skill);

            if (skill.ChargingStageList != null)
            {
                if (args.IsDelayStart)
                {
                    int uid = creature.Attack.UID;

                    ThreadPool.QueueUserWorkItem(
                        o =>
                            {
                                Thread.Sleep(750);

                                for (int i = 1; i < skill.ChargingStageList.ChargeStageList.Count; i++)
                                {
                                    if (creature.Attack.UID != uid)
                                        return;

                                    creature.Attack.NextStage();

                                    if (i != 3)
                                        Thread.Sleep(750);
                                }
                            });
                }
            }
            else
                ProcessTargets(creature, skill);
        }

        private void ProcessSkill(Creature creature, List<UseSkillArgs> argsList, Skill skill)
        {
            creature.Attack = new Attack(creature,
                                         argsList[0],
                                         () => GlobalLogic.AttackStageEnd(creature),
                                         () => GlobalLogic.AttackFinished(creature));

            VisibleService.Send(creature, new SpAttack(creature, creature.Attack));

            VisibleService.Send(creature, new SpAttackDestination(creature, creature.Attack));

            ProcessStages(creature, skill);

            ProcessMove(creature, skill);

            AiLogic.OnUseSkill(creature, skill);

            ProcessTargets(creature, skill);
        }

        private void ProcessStages(Creature creature, Skill skill)
        {
            int time = 0;
            List<int> durations = new List<int>();

            skill.Actions.Map(action => action.StageList.Map(stage =>
                {
                    stage.AnimationList.Map(anim =>
                        {
                            time +=
                                (int) (Math.Max(anim.Duration, anim.Animation.Get(a => a.Duraction, 0))/skill.TimeRate);
                        });

                    durations.Add(time);
                }));

            for (int i = 0; i < durations.Count - 1; i++)
                new DelayedAction(creature.Attack.NextStage, durations[i]);

            new DelayedAction(creature.Attack.Finish, time);
        }

        private async void ProcessMove(Creature creature, Skill skill)
        {
            bool checkIntersections = creature is Player && skill.Type != SkillType.Evade;

            AnimSeq anim = null;

            skill.Actions.Map(action => action.StageList.Map(stage => stage.AnimationList.Map(animation =>
                {
                    if (anim != null)
                        return;

                    if (animation.With(a => a.Animation).With(a => a.Distance).Get(d => d[6], 0f) < 1)
                        return;

                    anim = animation;
                })));

            if (anim == null)
                return;

            short heading = (short)(creature.Position.Heading + anim.Animation.Dir);

            int stepTime = (int)((anim.Animation.Duraction / skill.TimeRate) / 7);

            float movedDistance = 0f;

            for (int l = 0; l < 7; l++)
            {
                await Task.Delay(stepTime);

                if (creature.LifeStats.IsDead())
                    return;

                float stepDistance = anim.Animation.Distance[l] * anim.RootMotionXYRate - movedDistance;

                if (stepDistance <= 0.0)
                    continue;

                movedDistance += stepDistance;

                Point3D moved = Geom.GetNormal(heading).Multiple(stepDistance);

                if (checkIntersections && anim.Animation.Dir != 32768)
                {
                    float koef = SeUtils.CheckIntersections(creature, heading, moved, stepDistance);
                    moved.X *= koef;
                    moved.Y *= koef;
                }

                creature.Position.X += moved.X;
                creature.Position.Y += moved.Y;
            }
        }

        private void ProcessProjectileTargets(Creature creature, Skill skill, Projectile projectile)
        {
            if (skill.ProjectileData.With(data => data.TargetingList) == null)
                return;

            skill.ProjectileData.TargetingList.Map(targeting => targeting.AreaList.Map(area =>
                {
                    if (area.Type == TargetingAreaType.PvP)
                        return;

                    ProcessArea(creature, skill, targeting, area, projectile);
                }));
        }

        private void ProcessTargets(Creature creature, Skill skill)
        {
            if (skill.TargetingList == null)
                return;

            for (int i = 0; i < skill.TargetingList.Count; i++)
            {
                if (skill.TargetingList[i].ProjectileSkillList != null &&
                    skill.TargetingList[i].ProjectileSkillList.Count > 0)
                {
                    for (int k = 0; k < skill.TargetingList[i].ProjectileSkillList.Count; k++)
                    {
                        ProjectileSkill projectileSkill = skill.TargetingList[i].ProjectileSkillList[k];

                        try
                        {
                            new DelayedAction(() => ProcessProjectileSkillList(creature, projectileSkill),
                                              (int) (skill.TargetingList[i].Time/skill.TimeRate));
                        }
                        catch (Exception ex)
                        {
                            Log.ErrorException("SkillEngine: ProcessTargets", ex);
                        }
                    }
                }

                if (skill.TargetingList[i].AreaList == null)
                    continue;

                for (int j = 0; j < skill.TargetingList[i].AreaList.Count; j++)
                {
                    if (skill.TargetingList[i].AreaList[j].Type == TargetingAreaType.PvP)
                        continue;

                    ProcessArea(creature, skill, skill.TargetingList[i], skill.TargetingList[i].AreaList[j]);
                }
            }
        }

        private void ProcessProjectileSkillList(Creature creature, ProjectileSkill projectileSkill)
        {
            Projectile projectile = new Projectile(creature, projectileSkill);
            CreatureLogic.CreateProjectile(creature, projectile);
        }

        private async void ProcessArea(Creature creature, Skill skill, Targeting targeting, TargetingArea area,
                                       Projectile projectile = null)
        {
            try
            {
                bool isProjectileSkill = skill.Type == SkillType.Projectile || skill.Type == SkillType.Userslug;

                int skillId = creature.Attack.Args.SkillId;
                if (isProjectileSkill)
                    skillId += 20;

                if (targeting.Time > 0)
                    await Task.Delay((int) (targeting.Time/skill.TimeRate));
                int elapsed = targeting.Time;

                Player player = creature as Player;

                do
                {
                    try
                    {
                        if (creature.LifeStats.IsDead())
                            return;

                        if (area.DropItem != null)
                            creature.Instance.AddDrop(new Item
                                {
                                    Owner = player,

                                    ItemId = (int) area.DropItem,
                                    Count = 1,
                                    Position = Geom.ForwardPosition(creature.Position, 40),
                                    Instance = player.Instance,
                                });

                        Point3D center =
                            projectile != null
                                ? projectile.Position.ToPoint3D()
                                : Geom.GetNormal(creature.Position.Heading)
                                      .Multiple(area.OffsetDistance)
                                      .Add(creature.Position);

                        int count = 0;

                        List<Creature> targets =
                            creature.Attack.Args.Targets.Count > 0
                                ? creature.Attack.Args.Targets
                                : VisibleService.FindTargets(creature,
                                                             center,
                                                             projectile != null
                                                                 ? projectile.AttackDistance
                                                                 : area.MaxRadius,
                                                             area.Type);

                        foreach (Creature target in targets)
                        {
                            if (target != creature //Ignore checks for self-target
                                && !isProjectileSkill
                                && !creature.Attack.Args.IsItemSkill)
                            {
                                if (center.DistanceTo(target.Position) < area.MinRadius - 40)
                                    continue;

                                if (center.DistanceTo(target.Position) > area.MaxRadius)
                                    continue;

                                short diff = Geom.GetAngleDiff(creature.Attack.Args.StartPosition.Heading,
                                                               Geom.GetHeading(center, target.Position));

                                //diff from 0 to 180
                                //area.RangeAngel from 0 to 360
                                if (diff * 2 > (creature.Attack.Args.IsTargetAttack ? 90 : Math.Abs(area.RangeAngle) + 10))
                                    continue;
                            }

                            if (skill.TotalAtk > 0)
                            {
                                int damage = SeUtils.CalculateDamage(creature, target, skill.TotalAtk*area.Effect.Atk);

                                AttackResult result
                                    = new AttackResult
                                          {
                                              AttackType = AttackType.Normal,
                                              AttackUid = creature.Attack.UID,
                                              Damage = damage,
                                              Target = target,
                                          };

                                result.AngleDif = Geom.GetAngleDiff(creature.Attack.Args.StartPosition.Heading, result.Target.Position.Heading);
                                SeUtils.UpdateAttackResult(creature, result);

                                if (result.AttackType == AttackType.Block)
                                    VisibleService.Send(target, new SpAttackShowBlock(target, skillId));

                                VisibleService.Send(target, new SpAttackResult(creature, skillId, result));

                                AiLogic.OnAttack(creature, target);
                                AiLogic.OnAttacked(target, creature, result.Damage);

                                if (target is Player && ((Player)target).Duel != null && player != null &&
                                    ((Player)target).Duel.Equals(player.Duel) &&
                                    target.LifeStats.GetHpDiffResult(damage) < 1)
                                    DuelService.FinishDuel(player);
                                else
                                    CreatureLogic.HpChanged(target, target.LifeStats.MinusHp(result.Damage));
                            }

                            if (area.Effect.HpDiff > 0)
                            {
                                AttackResult result = new AttackResult {HpDiff = area.Effect.HpDiff, Target = target};

                                PassivityProcessor.OnHeal(player, result);
                                if(target is Player)
                                    PassivityProcessor.OnHealed((Player)target, result);

                                CreatureLogic.HpChanged(target, target.LifeStats.PlusHp(result.HpDiff),
                                                        creature);
                            }

                            if (area.Effect.MpDiff > 0)
                                CreatureLogic.MpChanged(target, target.LifeStats.PlusMp(area.Effect.MpDiff), creature);

                            if (area.Effect.AbnormalityOnCommon != null)
                                for (int i = 0; i < area.Effect.AbnormalityOnCommon.Count; i++)
                                    AbnormalityProcessor.AddAbnormality(target, area.Effect.AbnormalityOnCommon[i],
                                                                        creature);
                            if (player != null)
                            {
                                DuelService.ProcessDamage(player);

                                //MP regen on combo skill
                                if (skill.Id/10000 == 1 && player.GameStats.CombatMpRegen > 0)
                                {
                                    CreatureLogic.MpChanged(player, player.LifeStats.PlusMp(
                                        player.MaxMp*player.GameStats.CombatMpRegen/200));
                                }
                            }

                            if (++count == area.MaxCount) break;
                        }

                    }
                    catch (Exception ex)
                    {
                        Log.ErrorException("SkillEngine: ProcessAreaExc", ex);
                    }

                    if (targeting.Interval > 0)
                    {
                        await Task.Delay((int) (targeting.Interval/skill.TimeRate));
                        elapsed += targeting.Interval;
                    }

                } while (targeting.Interval > 0 && elapsed < targeting.Until);
            }
            catch (Exception ex)
            {
                Log.ErrorException("SkillEngine: ProcessArea", ex);
            }
        }

        public void Action()
        {
            AbnormalityProcessor.Action();
        }
    }
}
