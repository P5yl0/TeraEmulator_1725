using Communication.Logic;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Gather;
using Data.Structures.Npc;
using Data.Structures.Objects;
using Data.Structures.Player;
using Data.Structures.World;
using Tera.Extensions;

namespace Tera.Structures
{
    class Visible : IVisible
    {
        protected const int VisibleDistance = 4000;

        public Player Player { get; set; }

        protected bool IsActive = false;

        protected object UpdateLock = new object();

        public void Start()
        {
            lock (UpdateLock)
            {
                if (IsActive)
                    return;

                IsActive = true;
            }
        }

        public void Stop()
        {
            lock (UpdateLock)
                IsActive = false;

            ClearVisibleObjets();
        }

        public void Release()
        {
            ClearVisibleObjets();
            Player = null;
        }

        public static bool IsVisible(Player player, Creature creature)
        {
            if (creature is Player && !Communication.Global.PlayerService.IsPlayerOnline((Player)creature))
                return false;

            if (creature == null)
                return false;

            if (!(creature is Item) && !(creature is Gather) && !(creature is Campfire)
                && creature.LifeStats.IsDead())
                return false;

            if (creature is Gather && ((Gather)creature).CurrentGatherCounter <= 0)
                return false;

            double distance = player.Position.DistanceTo(creature.Position);
            if (distance > VisibleDistance)
                return false;

            if (creature is Npc && distance < 75)
                creature.Position.Z = player.Position.Z + 25;

            PlayerLogic.DistanceToCreatureRecalculated(player, creature, distance);

            return true;
        }

        public void Update()
        {
            if (!IsActive)
                return;

            lock (UpdateLock)
            {
                //Check for offline
                lock (Player.Instance.CreaturesLock)
                {
                    Player.VisiblePlayers.Each(CheckPlayer);

                    Player.Instance.Players.Each(CheckPlayer);
                    Player.Instance.Npcs.Each(CheckNpc);
                    Player.Instance.Items.Each(CheckItem);
                    Player.Instance.Projectiles.Each(CheckProjectile);
                    Player.Instance.Gathers.Each(CheckGather);
                    Player.Instance.Campfires.Each(CheckCampfire);
                }
            }
        }

        private void CheckPlayer(Player otherPlayer)
        {
            if (Player == otherPlayer)
                return;

            if (IsVisible(Player, otherPlayer))
            {
                if (!Player.VisiblePlayers.Contains(otherPlayer))
                {
                    Player.VisiblePlayers.Add(otherPlayer);
                    PlayerLogic.InTheVision(Player, otherPlayer);
                }
            }
            else
            {
                if (Player.VisiblePlayers.Contains(otherPlayer))
                {
                    Player.VisiblePlayers.Remove(otherPlayer);
                    PlayerLogic.OutOfVision(Player, otherPlayer);
                }
            }
        }

        private void CheckNpc(Npc npc)
        {
            if (IsVisible(Player, npc))
            {
                if (!Player.VisibleNpcs.Contains(npc))
                {
                    Player.VisibleNpcs.Add(npc);
                    npc.VisiblePlayers.Add(Player);
                    PlayerLogic.InTheVision(Player, npc);
                }
            }
            else
            {
                if (Player.VisibleNpcs.Contains(npc))
                {
                    Player.VisibleNpcs.Remove(npc);
                    npc.VisiblePlayers.Remove(Player);
                    PlayerLogic.OutOfVision(Player, npc);
                }
            }
        }

        private void CheckGather(Gather gather)
        {
            if(IsVisible(Player, gather))
            {
                if (!Player.VisibleGathers.Contains(gather))
                {
                    Player.VisibleGathers.Add(gather);
                    gather.VisiblePlayers.Remove(Player);
                    PlayerLogic.InTheVision(Player, gather);
                }
            }
            else
            {
                if (Player.VisibleGathers.Contains(gather))
                {
                    Player.VisibleGathers.Remove(gather);
                    gather.VisiblePlayers.Remove(Player);
                    PlayerLogic.OutOfVision(Player, gather);
                }
            }
        }

        private void CheckItem(Item item)
        {
            if (IsVisible(Player, item))
            {
                if (!Player.VisibleItems.Contains(item))
                {
                    Player.VisibleItems.Add(item);
                    item.VisiblePlayers.Add(Player);
                    PlayerLogic.InTheVision(Player, item);
                }
            }
            else
            {
                if (Player.VisibleItems.Contains(item))
                {
                    Player.VisibleItems.Remove(item);
                    if (item.VisiblePlayers != null)
                        item.VisiblePlayers.Remove(Player);
                    PlayerLogic.OutOfVision(Player, item);
                }
            }
        }

        private void CheckProjectile(Projectile projectile)
        {
            if (IsVisible(Player, projectile))
            {
                if (!Player.VisibleProjectiles.Contains(projectile))
                {
                    Player.VisibleProjectiles.Add(projectile);
                    projectile.VisiblePlayers.Add(Player);
                    PlayerLogic.InTheVision(Player, projectile);
                }
            }
            else
            {
                if (Player.VisibleProjectiles.Contains(projectile))
                {
                    Player.VisibleProjectiles.Remove(projectile);
                    projectile.VisiblePlayers.Remove(Player);
                    PlayerLogic.OutOfVision(Player, projectile);
                }
            }
        }

        private void CheckCampfire(Campfire campfire)
        {
            if (IsVisible(Player, campfire))
            {
                if (!Player.VisibleCampfires.Contains(campfire))
                {
                    Player.VisibleCampfires.Add(campfire);
                    campfire.VisiblePlayers.Add(Player);
                    PlayerLogic.InTheVision(Player, campfire);
                }
            }
            else
            {
                if (Player.VisibleCampfires.Contains(campfire))
                {
                    Player.VisibleCampfires.Remove(campfire);
                    campfire.VisiblePlayers.Remove(Player);
                    PlayerLogic.OutOfVision(Player, campfire);
                }
            }
        }

        private void ClearVisibleObjets()
        {
            lock (UpdateLock)
            {
                foreach (Npc npc in Player.VisibleNpcs)
                    if (npc.VisiblePlayers != null && npc.VisiblePlayers.Contains(Player))
                        npc.VisiblePlayers.Remove(Player);

                Player.VisibleNpcs.Clear();
                Player.VisiblePlayers.Clear();
                Player.VisibleCampfires.Clear();
                Player.VisibleItems.Clear();
                Player.VisibleProjectiles.Clear();
            }
        }
    }
}
