using Communication;
using Communication.Logic;
using Data.Enums.SkillEngine;
using Data.Structures.Creature;
using Data.Structures.Objects;
using Tera.Controllers;
using Utils;

namespace Tera.AiEngine
{
    class ProjectileAi : DefaultAi
    {
        public Projectile Projectile;

        public NpcMoveController MoveController;

        protected long LastCallUts = Funcs.GetCurrentMilliseconds();

        protected long DieUts;

        public override void Init(Creature creature)
        {
            base.Init(creature);

            Projectile = (Projectile) creature;

            if (Projectile.TargetPosition != null)
            {
                MoveController = new NpcMoveController(creature);
                MoveController.MoveTo(Projectile.TargetPosition);
            }

            DieUts = Funcs.GetCurrentMilliseconds() + Projectile.Lifetime;
        }

        public override void Release()
        {
            base.Release();
            Projectile = null;

            if (MoveController != null)
                MoveController.Release();
            MoveController = null;
        }

        public override void Action()
        {
            if (Projectile.LifeStats.IsDead())
            {
                if (Projectile.VisiblePlayers.Count == 0)
                    CreatureLogic.ReleaseProjectile(Projectile);
                return;
            }

            long now = Funcs.GetCurrentMilliseconds();

            if (now > DieUts)
            {
                Projectile.LifeStats.Kill();
                return;
            }

            if (MoveController != null)
            {
                long elapsed = now - LastCallUts;
                LastCallUts = now;

                MoveController.Action(elapsed);
            }

            var targets = Global.VisibleService.FindTargets((Creature) Projectile.Parent, Projectile.Position,
                                                            Projectile.AttackDistance + 40, TargetingAreaType.EnemyOrPvP);

            if (targets.Count > 0)
            {
                Global.SkillEngine.UseProjectileSkill(Projectile);
                Projectile.LifeStats.Kill();
            }
        }
    }
}
