using System;
using System.Collections.Generic;
using Communication.Interfaces;
using Data.Enums.SkillEngine;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Geometry;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.World;
using Tera.Extensions;

namespace Tera.Services
{
    class VisibleService : IVisibleService
    {
        public void Send(Creature creature, ISendPacket packet)
        {
            Player player = creature as Player;
            if (player != null)
            {
                if (player.Connection != null)
                    packet.Send(player.Connection);
            }

            creature.VisiblePlayers.Each(p => packet.Send(p.Connection));
        }

        public List<Creature> FindTargets(Creature creature, Point3D position, double distance, TargetingAreaType type)
        {
            return FindTargets(creature, position.X, position.Y, position.Z, distance, type);
        }

        public List<Creature> FindTargets(Creature creature, WorldPosition position, double distance, TargetingAreaType type)
        {
            return FindTargets(creature, position.X, position.Y, position.Z, distance, type);
        }

        public List<Creature> FindTargets(Creature creature, float x, float y, float z, double distance, TargetingAreaType type)
        {
            List<Creature> finded = new List<Creature>();

            switch (type)
            {
                case TargetingAreaType.Me:
                    finded.Add(creature);
                    break;
                case TargetingAreaType.EnemyOrPvP:
                case TargetingAreaType.Enemy:
                    if (creature is Player)
                    {
                        finded.AddRange(FindNpcs(creature, x, y, z, distance));
                        finded.AddRange(FindPlayers(creature, x, y, z, distance));
                    }
                    else
                        finded.AddRange(FindPlayers(creature, x, y, z, distance));
                    break;
                case TargetingAreaType.PvP:
                    break;
                case TargetingAreaType.AllIncludeVillager:
                    finded.Add(creature);
                    finded.AddRange(FindNpcs(creature, x, y, z, distance, true));
                    finded.AddRange(FindPlayers(creature, x, y, z, distance));
                    break;
                case TargetingAreaType.All:
                    finded.Add(creature);
                    finded.AddRange(FindNpcs(creature, x, y, z, distance));
                    finded.AddRange(FindPlayers(creature, x, y, z, distance));
                    break;
                case TargetingAreaType.AllyOnDeath:
                case TargetingAreaType.AllyVillager:
                case TargetingAreaType.Ally:
                case TargetingAreaType.Party:
                case TargetingAreaType.AllyExceptMe:
                case TargetingAreaType.PartyExceptMe:
                    break;
                case TargetingAreaType.AllExceptMe:
                    finded.AddRange(FindNpcs(creature, x, y, z, distance));
                    finded.AddRange(FindPlayers(creature, x, y, z, distance));
                    break;
                case TargetingAreaType.No:
                    return finded;
            }

            return finded;
        }

        public List<Player> FindPlayers(Creature creature, float x, float y, float z, double distance)
        {
            distance += creature.BodyRadius + 45; //45 is Player.BodyRadius
            double verticalDistance = distance*2;

            if (!(creature is Player))
                return
                    creature.VisiblePlayers.Select(
                        player => player.Position.DistanceTo(x, y) <= distance &&
                                  Math.Abs(z - player.Position.Z) < verticalDistance);
            return
                creature.VisiblePlayers.Select(
                    player =>
                    (player.Position.DistanceTo(x, y) <= distance &&
                     Math.Abs(z - player.Position.Z) < verticalDistance &&
                     ((Player) creature).Duel != null &&
                     ((Player) creature).Duel.Equals(player.Duel)));
        }

        public List<Npc> FindNpcs(Creature creature, float x, float y, float z, double distance, bool findVillagers = false)
        {
            distance += creature.BodyRadius;
            double verticalDistance = distance * 2;

            return creature.VisibleNpcs.Select(npc =>
                                               npc.Position.DistanceTo(x, y) <= distance + npc.BodyRadius
                                               && Math.Abs(z - npc.Position.Z) < verticalDistance
                                               && (!npc.NpcTemplate.IsVillager || findVillagers));
        }

        public void Action()
        {
            
        }
    }
}
