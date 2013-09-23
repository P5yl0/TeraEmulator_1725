using System.Collections.Generic;
using Communication.Interfaces;
using Data.Enums;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.Template.Item;
using Data.Structures.Template.Item.CategorieStats;
using Utils;

namespace Tera.Services
{
    class StatsService : IStatsService
    {
        public static Dictionary<PlayerClass, Dictionary<int, CreatureBaseStats>> PlayerStats = new Dictionary<PlayerClass, Dictionary<int, CreatureBaseStats>>();

        public static Dictionary<int, Dictionary<int, CreatureBaseStats>> NpcStats = new Dictionary<int, Dictionary<int, CreatureBaseStats>>();

        public void Init()
        {
            for (int i = 0; i < 8; i++)
            {
                PlayerStats.Add((PlayerClass) i, new Dictionary<int, CreatureBaseStats>());

                CreatureBaseStats firstLevelStats = Data.Data.Stats[i];

                for (int j = 1; j < 100; j++)
                {
                    CreatureBaseStats stats = firstLevelStats.Clone();

                    stats.HpBase = (int) (stats.HpBase*(1 + (j - 1)*0.116));
                    stats.HpStamina = (int)(stats.HpStamina * (1 + (j - 1) * 0.14));

                    stats.MpBase = (int)(stats.MpBase * (1 + (j - 1) * 0.07));
                    stats.MpStamina = (int)(stats.MpStamina * (1 + (j - 1) * 0.12));

                    stats.Power += i - 1;
                    stats.Endurance += i - 1;

                    PlayerStats[stats.PlayerClass].Add(j, stats);
                }
            }

            for (int i = 8; i < Data.Data.Stats.Count; i++)
            {
                if (!NpcStats.ContainsKey(Data.Data.Stats[i].NpcHuntingZoneId))
                    NpcStats.Add(Data.Data.Stats[i].NpcHuntingZoneId, new Dictionary<int, CreatureBaseStats>());

                NpcStats[Data.Data.Stats[i].NpcHuntingZoneId].Add(Data.Data.Stats[i].NpcId, Data.Data.Stats[i]);
            }
        }

        public CreatureBaseStats InitStats(Creature creature)
        {
            Player player = creature as Player;
            if (player != null)
            {
                return GetBaseStats(player).Clone();
            }

            Npc npc = creature as Npc;
            if (npc != null)
            {
                return NpcStats[npc.NpcTemplate.HuntingZoneId][npc.NpcTemplate.Id].Clone();
            }

            Log.Error("StatsService: Unknown type: {0}.", creature.GetType().Name);
            return new CreatureBaseStats();
        }

        public CreatureBaseStats GetBaseStats(Player player)
        {
            return PlayerStats[player.PlayerData.Class][player.GetLevel()];
        }

        public void UpdateStats(Creature creature)
        {
            Player player = creature as Player;
            if (player != null)
            {
                UpdatePlayerStats(player);
                return;
            }

            Npc npc = creature as Npc;
            if (npc != null)
            {
                UpdateNpcStats(npc);
                return;
            }

            Log.Error("StatsService: Unknown type: {0}.", creature.GetType().Name);
        }

        private void UpdatePlayerStats(Player player)
        {
            CreatureBaseStats baseStats = GetBaseStats(player);
            baseStats.CopyTo(player.GameStats);

            int itemsAttack = 0,
                itemsDefense = 10,
                itemsImpact = 1,
                itemsBalance = 1;

            lock (player.Inventory.ItemsLock)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (!player.Inventory.Items.ContainsKey(i))
                    { continue; }

                    ItemTemplate itemTemplate = player.Inventory.Items[i].ItemTemplate;

                    EquipmentStat equipmentStat = itemTemplate.CatigorieStat as EquipmentStat;
                    if (equipmentStat != null)
                    {
                        itemsAttack += equipmentStat.MinAtk;
                        itemsDefense += equipmentStat.Def;
                        itemsImpact += equipmentStat.Impact;
                        itemsBalance += equipmentStat.Balance;
                    }

                    if (itemTemplate.Passivities != null)
                    {
                        foreach (var passivity in itemTemplate.Passivities)
                        {
                            player.GameStats.Passivities.Add(passivity);
                        }
                    }
                }
            }

            player.GameStats.Attack = (int) (baseStats.Attack + (0.03f*baseStats.Power + 3)*itemsAttack);
            player.GameStats.Defense = (int) (baseStats.Defense + (0.01f*baseStats.Endurance + 0.5)*itemsDefense);
            player.GameStats.Impact = (int) (itemsImpact*(0.01f*player.GameStats.ImpactFactor + 0.05));
            player.GameStats.Balance = (int) (itemsBalance*(0.01f*player.GameStats.BalanceFactor + 0.07));

            if (player.PlayerMount != 0 && Data.Data.Mounts.ContainsKey(player.PlayerMount))
            {
                player.GameStats.Movement = (short)Data.Data.Mounts[player.PlayerMount].SpeedModificator;
            }
            else
            {
                player.GameStats.Movement = baseStats.Movement;
            }

            player.EffectsImpact.ResetChanges(player);
            player.EffectsImpact.ApplyChanges(player.GameStats);
        }

        private void UpdateNpcStats(Npc npc)
        {
            npc.EffectsImpact.ResetChanges(npc);
            npc.EffectsImpact.ApplyChanges(npc.GameStats);
        }

        public void Action()
        {
            
        }
    }
}
