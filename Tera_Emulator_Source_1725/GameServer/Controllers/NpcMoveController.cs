using System;
using Communication;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Geometry;
using Data.Structures.Npc;
using Data.Structures.Objects;
using Data.Structures.World;
using Network.Server;

namespace Tera.Controllers
{
    class NpcMoveController
    {
        public Creature Creature;

        public Npc Npc;

        public Projectile Projectile;

        //

        public bool IsActive = false;

        protected Point3D TargetPosition;

        protected int TargetDistance = 0;

        protected Point3D MoveVector;

        protected bool IsMove = false;

        protected bool IsNewDirection = false;

        public NpcMoveController(Creature creature)
        {
            Creature = creature;
            Npc = creature as Npc;
            Projectile = creature as Projectile;

            TargetPosition = new Point3D();
            MoveVector = new Point3D();
        }

        public void Reset()
        {
            IsActive = false;
            IsMove = false;
        }

        public void Release()
        {
            Creature = null;
            Npc = null;
            Projectile = null;
            TargetPosition = null;
        }

        public void MoveTo(WorldPosition position, int distance = 0)
        {
            position.CopyTo(TargetPosition);
            TargetDistance = distance;
            Move();
        }

        public void MoveTo(Point3D position, int distance = 0)
        {
            position.CopyTo(TargetPosition);
            TargetDistance = distance;
            Move();
        }

        public void MoveTo(float x, float y, int distance = 0)
        {
            TargetPosition.X = x;
            TargetPosition.Y = y;
            TargetDistance = distance;
            Move();
        }

        protected void Move()
        {
            IsNewDirection = true;
            IsActive = true;
        }

        public void Stop()
        {
            IsActive = false;
        }

        public void Action(long elapsed)
        {
            if (IsMove)
            {
                MoveVector.Multiple(elapsed/1000f);
                Creature.Position.X += MoveVector.X;
                Creature.Position.Y += MoveVector.Y;

                if (Projectile != null)
                    Creature.Position.Z += MoveVector.Z;
                
                //Global.GeoService.FixZ(Npc.Position);

                IsMove = false;
            }

            if (!IsActive)
                return;

            Creature.Position.Heading = Geom.GetHeading(Creature.Position, TargetPosition);

            if (TargetDistance != 0)
            {
                double d = TargetPosition.DistanceTo2D(Creature.Position);
                double a = Creature.Position.Heading * Math.PI / 32768;

                TargetPosition.X = Creature.Position.X + (float)(d * Math.Cos(a));
                TargetPosition.Y = Creature.Position.Y + (float)(d * Math.Sin(a));

                TargetDistance = 0;
            }

            double distance = TargetPosition.DistanceTo2D(Creature.Position);

            if (distance < 10)
            {
                TargetPosition.CopyTo(Creature.Position);
                IsActive = false;
                return;
            }

            int speed = GetSpeed();

            if (Projectile == null && IsNewDirection)
            {
                IsNewDirection = false;
                Global.VisibleService.Send(Creature, new SpNpcMove(Creature, (short)speed, TargetPosition.X, TargetPosition.Y, TargetPosition.Z));
            }

            double angle = Creature.Position.Heading*Math.PI/32768;

            MoveVector.X = speed*(float) Math.Cos(angle);
            MoveVector.Y = speed*(float) Math.Sin(angle);

            if (Projectile != null)
                MoveVector.Z = speed*(TargetPosition.Z - Creature.Position.Z)/(float) distance;

            IsMove = true;
        }

        public void Resend(IConnection connection)
        {
            new SpNpcMove(Creature, (short) GetSpeed(), TargetPosition.X, TargetPosition.Y, TargetPosition.Z).Send(connection);
        }

        public int GetSpeed()
        {
            if (Npc != null)
                return Npc.Target == null
                            ? Npc.NpcTemplate.Shape.WalkSpeed
                            : Npc.NpcTemplate.Shape.RunSpeed;

            if (Projectile != null)
                return Projectile.Speed;

            return 100;
        }
    }
}
