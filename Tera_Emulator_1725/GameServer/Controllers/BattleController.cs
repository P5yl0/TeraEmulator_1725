using System.Collections.Generic;
using System.Linq;
using Communication;
using Communication.Logic;
using Data.Enums;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Player;
using Network;
using Network.Server;
using Tera.SkillEngine;
using Utils;

namespace Tera.Controllers
{
    internal class BattleController : IController
    {
        public const int Timeout = 15000;

        protected Player Player;

        protected Dictionary<Creature, long> Targets = new Dictionary<Creature, long>();

        protected object TargetsLock = new object();

        public void AddTarget(Creature creature)
        {
            lock (TargetsLock)
            {
                if (Targets == null)
                    Targets = new Dictionary<Creature, long>();

                if (!Targets.ContainsKey(creature))
                {
                    Targets.Add(creature, Funcs.GetCurrentMilliseconds());

                    if (Targets.Count == 1)
                        SystemMessages.BattleBegan.Send(Player.Connection);
                }
                else
                    UpdateTarget(creature);
            }
        }

        public void Start(Player player)
        {
            Player = player;

            player.PlayerMode = PlayerMode.Armored;

            Global.VisibleService.Send(Player, new SpCharacterState(player));
            PassivityProcessor.OnBattleEnter(player);
            CreatureLogic.UpdateCreatureStats(Player);
        }

        public void Release()
        {
            if (Player != null)
            {
                Player.PlayerMode = PlayerMode.Normal;
                
                Global.VisibleService.Send(Player, new SpCharacterState(Player));
                CreatureLogic.UpdateCreatureStats(Player);

                SystemMessages.BattleEnded.Send(Player);

                lock (TargetsLock)
                    foreach (KeyValuePair<Creature, long> target in Targets)
                        Global.ObserverService.RemoveObserved(Player, target.Key);
            }

            Player = null;
            Targets.Clear();
            Targets = null;
        }

        public void Action()
        {
            if (Player.LifeStats.IsDead())
            {
                Global.ControllerService.SetController(Player, new DeathController());
                return;
            }

            lock (TargetsLock)
            {
                List<Creature> toRemove = (from target in Targets
                                           where
                                               target.Key.LifeStats.IsDead() ||
                                               !target.Key.VisiblePlayers.Contains(Player) ||
                                               Funcs.GetCurrentMilliseconds() - target.Value > Timeout
                                           select target.Key).ToList();

                foreach (Creature creature in toRemove)
                    RemoveTarget(creature);
            }

            if (Targets.Count == 0)
                Global.ControllerService.SetController(Player, new DefaultController());
        }

        public bool Contains(Creature creature)
        {
            return Targets.ContainsKey(creature);
        }

        public void RemoveTarget(Creature creature)
        {
            if (Contains(creature))
                lock (TargetsLock)
                {
                    Targets.Remove(creature);
                    Global.ObserverService.RemoveObserved(Player, creature);
                }
        }

        public void UpdateTarget(Creature creature)
        {
            if (Contains(creature))
                lock (TargetsLock)
                    Targets[creature] = Funcs.GetCurrentMilliseconds();
        }
    }
}
