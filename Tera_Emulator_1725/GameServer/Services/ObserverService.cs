using Communication.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Player;
using Network.Server;
using Tera.Extensions;
using Utils;

namespace Tera.Services
{
    class ObserverService : IObserverService
    {
        public void AddObserved(Player player, Creature creature)
        {
            if (player == creature)
                return;

            if (creature.ObserversList.Contains(player))
                return;

            creature.ObserversList.Add(player);

            if (player.ObservedCreature == null)
            {
                player.ObservedCreature = creature;
                new DelayedAction(() => new SpNpcHpWindow(creature).Send(player.Connection), 1000);
            }
        }

        public void RemoveObserved(Player player, Creature creature)
        {
            if (player == creature)
                return;

            if (!creature.ObserversList.Contains(player))
                return;

            creature.ObserversList.Remove(player);
            new SpRemoveHpBar(creature).Send(player);

            if (player.ObservedCreature == creature)
                player.ObservedCreature = null;
        }

        public void NotifyHpChanged(Creature creature)
        {
            SpNpcHpMp packet = new SpNpcHpMp(creature);
            creature.ObserversList.Each(player => packet.Send(player.Connection));
        }

        public void NotifyMpChanged(Creature creature)
        {
            
        }

        public void Action()
        {
            
        }
    }
}
