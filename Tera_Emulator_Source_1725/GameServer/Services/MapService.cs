using System;
using System.Collections.Generic;
using System.Threading;
using Communication;
using Communication.Interfaces;
using Communication.Logic;
using Data.Enums.Item;
using Data.Structures;
using Data.Structures.Creature;
using Data.Structures.Gather;
using Data.Structures.Geometry;
using Data.Structures.Npc;
using Data.Structures.Objects;
using Data.Structures.Player;
using Data.Structures.Template;
using Data.Structures.Template.Gather;
using Data.Structures.Template.Item.CategorieStats;
using Data.Structures.World;
using Network;
using Network.Server;
using Tera.DungeonEngine.Dungeons;
using Tera.Extensions;
using Tera.Structures;
using Utils;

namespace Tera.Services
{
    class MapService : IMapService
    {
        public static bool SpawnSniffed = true;

        public static Dictionary<int, List<MapInstance>> Maps = new Dictionary<int, List<MapInstance>>();
        public static object MapLock = new object();

        public static Dictionary<int, Type> Dungeons = new Dictionary<int, Type>();

        protected static Dictionary<int, List<SpawnTemplate>> MergedSpawn = new Dictionary<int, List<SpawnTemplate>>();

        public void Action()
        {
            try
            {
                lock (MapLock)
                {
                    foreach (var map in Maps.Values)
                    {
                        for (int i = 0; i < map.Count; i++)
                        {
                            for (int j = 0; j < map[i].Npcs.Count; j++)
                            {
                                try
                                {
                                    map[i].Npcs[j].Ai.Action();
                                }
                                // ReSharper disable EmptyGeneralCatchClause
                                catch
                                // ReSharper restore EmptyGeneralCatchClause
                                {
                                    //Collection modified
                                }

                                if ((j & 1023) == 0) // 2^N - 1
                                    Thread.Sleep(1);
                            }

                            for (int j = 0; j < map[i].Gathers.Count; j++)
                            {
                                try
                                {
                                    map[i].Gathers[j].Ai.Action();
                                }
                                // ReSharper disable EmptyGeneralCatchClause
                                catch
                                // ReSharper restore EmptyGeneralCatchClause
                                {
                                    //Collection modified
                                }

                                if ((j & 1023) == 0) // 2^N - 1
                                    Thread.Sleep(1);
                            }

                            for (int j = 0; j < map[i].Projectiles.Count; j++)
                            {
                                try
                                {
                                    map[i].Projectiles[j].Ai.Action();
                                }
                                // ReSharper disable EmptyGeneralCatchClause
                                catch
                                // ReSharper restore EmptyGeneralCatchClause
                                {
                                    //Collection modified
                                }

                                if ((j & 1023) == 0) // 2^N - 1
                                    Thread.Sleep(1);
                            }

                            long now = Funcs.GetCurrentMilliseconds();

                            for (int j = 0; j < map[i].Campfires.Count; j++)
                            {
                                try
                                {
                                    var campfire = map[i].Campfires[j];

                                    if (!campfire.IsStationary && campfire.DespawnUts <= now)
                                    {
                                        map[i].Campfires.RemoveAt(j--);

                                        campfire.VisiblePlayers.Each(player =>
                                        {
                                            try
                                            {
                                                player.VisibleCampfires.Remove(campfire);
                                            }
                                            catch
                                            {
                                                //Already removed
                                            }
                                        });

                                        Global.VisibleService.Send(campfire, new SpRemoveCampfire(campfire));

                                        campfire.Release();
                                    }
                                }
                                // ReSharper disable EmptyGeneralCatchClause
                                catch
                                // ReSharper restore EmptyGeneralCatchClause
                                {
                                    //Collection modified
                                }

                                if ((j & 1023) == 0) // 2^N - 1
                                    Thread.Sleep(1);
                            }
                        }
                    }
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
                //Collection modified
            }
        }

        public void Init()
        {
            Dungeons.Add(9036, typeof(IoDDungeon));

            #region Merge_DataCenter_And_Sniffed_Spawn

            List<int> fullIds = new List<int>();

            foreach (KeyValuePair<int, List<SpawnTemplate>> keyValuePair in Data.Data.Spawns)
            {
                for (int i = 0; i < keyValuePair.Value.Count; i++)
                {
                    if (keyValuePair.Value[i].Type == 1023)
                    {
                        keyValuePair.Value.RemoveAt(i);
                        i--;
                    }
                }

                MergedSpawn.Add(keyValuePair.Key, keyValuePair.Value);

                foreach (SpawnTemplate spawnTemplate in keyValuePair.Value)
                    if (!fullIds.Contains(spawnTemplate.FullId))
                        fullIds.Add(spawnTemplate.FullId);
            }

            foreach (KeyValuePair<int, List<SpawnTemplate>> dcSpawn in Data.Data.DcSpawns)
            {
                if (!MergedSpawn.ContainsKey(dcSpawn.Key))
                {
                    MergedSpawn.Add(dcSpawn.Key, dcSpawn.Value);
                    continue;
                }


                foreach (SpawnTemplate spawnTemplate in dcSpawn.Value)
                {
                    if (!fullIds.Contains(spawnTemplate.FullId))
                        MergedSpawn[dcSpawn.Key].Add(spawnTemplate);
                }
            }

            #endregion


            foreach (KeyValuePair<int, List<SpawnTemplate>> dcSpawn in MergedSpawn)
                if (!IsDungeon(dcSpawn.Key))
                    SpawnMap(new MapInstance { MapId = dcSpawn.Key });
        }

        public void SpawnMap(MapInstance instance)
        {
            if (MergedSpawn.ContainsKey(instance.MapId))
                foreach (SpawnTemplate template in MergedSpawn[instance.MapId])
                    SpawnTeraObject(CreateNpc(template), instance);

            if (Data.Data.CampfireTemplates.ContainsKey(instance.MapId) && Data.Data.CampfireTemplates[instance.MapId] != null)
                foreach (CampfireSpawnTemplate campfireSpawnTemplate in Data.Data.CampfireTemplates[instance.MapId])
                    if (campfireSpawnTemplate.Type == 4)
                        SpawnTeraObject(new Campfire
                        {
                            Type = campfireSpawnTemplate.Type,
                            Status = campfireSpawnTemplate.Status,
                            Position = campfireSpawnTemplate.Position.Clone(),
                            IsStationary = true,
                        }, instance);

            foreach (GSpawnTemplate gSpawnTemplate in Data.Data.DcGatherSpawnTemplates)
                if (gSpawnTemplate.WorldPosition.MapId == instance.MapId)
                    SpawnTeraObject(CreateGather(gSpawnTemplate), instance);

            if (!Maps.ContainsKey(instance.MapId))
                Maps.Add(instance.MapId, new List<MapInstance>());

            Maps[instance.MapId].Add(instance);

        }

        public void SpawnTeraObject(TeraObject obj, MapInstance instance)
        {
            var creature = obj as Creature;
            if (creature != null)
            {
                lock (instance.CreaturesLock)
                {
                    if (obj is Npc)
                        instance.Npcs.Add((Npc)obj);
                    else if (obj is Player)
                        instance.Players.Add((Player)obj);
                    else if (obj is Campfire)
                        instance.Campfires.Add((Campfire)obj);
                    else if (obj is Gather)
                        instance.Gathers.Add((Gather)obj);
                    else if (obj is Item)
                        instance.Items.Add((Item)obj);
                    else if (obj is Projectile)
                        instance.Projectiles.Add((Projectile)obj);
                }

                creature.Instance = instance;
            }
        }

        public void DespawnTeraObject(TeraObject obj)
        {
            var creature = obj as Creature;
            if (creature != null)
            {
                lock (creature.Instance.CreaturesLock)
                {
                    if (creature is Npc)
                    {
                        creature.Instance.Npcs.Remove((Npc)obj);
                        creature.VisiblePlayers.Each(player =>
                        {
                            player.VisibleNpcs.Remove((Npc)obj);
                            PlayerLogic.OutOfVision(player, creature);
                        });
                    }
                    else if (creature is Player)
                    {
                        creature.Instance.Players.Remove((Player)obj);
                        creature.VisiblePlayers.Each(player =>
                        {
                            player.VisiblePlayers.Remove((Player)obj);
                            PlayerLogic.OutOfVision(player, creature);
                        });
                    }
                    else if (creature is Campfire)
                    {
                        creature.Instance.Campfires.Remove((Campfire)obj);
                        creature.VisiblePlayers.Each(player =>
                        {
                            player.VisibleCampfires.Remove((Campfire)obj);
                            PlayerLogic.OutOfVision(player, creature);
                        });
                    }
                    else if (creature is Gather)
                    {
                        creature.Instance.Gathers.Remove((Gather)obj);
                        creature.VisiblePlayers.Each(player =>
                        {
                            player.VisibleGathers.Remove((Gather)obj);
                            PlayerLogic.OutOfVision(player, creature);
                        });
                    }
                    else if (creature is Item)
                    {
                        creature.Instance.Items.Remove((Item)obj);
                        creature.VisiblePlayers.Each(player =>
                        {
                            player.VisibleItems.Remove((Item)obj);
                            PlayerLogic.OutOfVision(player, creature);
                        });
                    }
                    else if (creature is Projectile)
                    {
                        creature.Instance.Projectiles.Remove((Projectile)obj);
                        creature.VisiblePlayers.Each(player =>
                        {
                            player.VisibleProjectiles.Remove((Projectile)obj);
                            PlayerLogic.OutOfVision(player, creature);
                        });
                    }
                }

                if (!(creature is Player))
                    creature.Release();
            }
        }

        public void PlayerEnterWorld(Player player)
        {
            if (!Maps.ContainsKey(player.Position.MapId))
                lock (MapLock)
                    Maps.Add(player.Position.MapId, new List<MapInstance>());

            var instance = GetMapInstance(player, player.Position.MapId);

            if (!Maps[player.Position.MapId].Contains(instance))
                Maps[player.Position.MapId].Add(instance);

            SpawnTeraObject(player, instance);

            if (player.Visible != null)
            {
                player.Visible.Stop();
                player.Visible.Release();
                player.Visible = null;
            }

            player.Visible = new Visible { Player = player };
            player.Visible.Start();
        }

        public void CreateDrop(Npc npc, Player player)
        {
            if (Funcs.IsLuck(75))
            {
                var money = (int)(10 * Data.Data.NpcExperience[npc.GetLevel()] / Funcs.Random().Next(20, 60));

                player.Instance.AddDrop(
                    new Item
                    {
                        Owner = player,
                        Npc = npc,

                        ItemId = 20000000,
                        Count = money,
                        Position = Geom.RandomCirclePosition(npc.Position, Funcs.Random().Next(60, 100)),
                        Instance = player.Instance,
                    });
            }

            if (!Data.Data.Drop.ContainsKey(npc.NpcTemplate.FullId))
                return;

            List<int> items = Data.Data.Drop[npc.NpcTemplate.FullId];

            if (items == null)
                return;

            int count = 0;
            int rate = Funcs.Random().Next(0, 2500);

            if (rate < 10)
                count = 6;
            else if (rate < 30)
                count = 5;
            else if (rate < 90)
                count = 4;
            else if (rate < 270)
                count = 3;
            else if (rate < 600)
                count = 2;
            else if (rate < 1800)
                count = 1;

            if (items.Count < count)
                count = items.Count;

            for (int i = 0; i < count; i++)
            {
                int itemId = items[Funcs.Random().Next(0, items.Count)];

                if (!Data.Data.ItemTemplates.ContainsKey(itemId))
                    continue;

                player.Instance.AddDrop(
                    new Item
                    {
                        Owner = player,
                        Npc = npc,

                        ItemId = itemId,
                        Count = 1,
                        Position = Geom.RandomCirclePosition(npc.Position, Funcs.Random().Next(60, 100)),
                        Instance = player.Instance,
                    });
            }
        }

        public void PickUpItem(Player player, Item item)
        {
            if (item.Owner.Party == null && item.Owner != player)
            {
                SystemMessages.YouCantPickUpItem("@item:" + item.ItemId).Send(player.Connection);
                return;
            }
            if (item.Owner.Party != null && !item.Owner.Party.PartyMembers.Contains(player))
            {
                SystemMessages.YouCantPickUpItem("@item:" + item.ItemId).Send(player.Connection);
                return;
            }

            if (item.ItemId == 20000000)
            {
                Global.StorageService.AddMoneys(player, player.Inventory, item.Count);

                SystemMessages.YouReceiveMoney("" + item.Count).Send(player.Connection);
            }
            else
            {
                var template = Data.Data.ItemTemplates[item.ItemId];

                if (template.CombatItemType == CombatItemType.IMMEDIATE)
                {
                    var skillStat = template.CatigorieStat as SkillStat;

                    if (skillStat != null)
                    {
                        Global.SkillEngine.UseSkill(
                            player.Connection,
                            new UseSkillArgs
                            {
                                IsItemSkill = true,
                                SkillId = skillStat.SkillId,
                                StartPosition = player.Position.Clone(),
                            });
                    }
                }
                else
                {
                    if (
                        !Global.StorageService.AddItem(player, player.Inventory,
                                                       new StorageItem { ItemId = item.ItemId, Amount = item.Count }))
                    {
                        SystemMessages.YouCantPickUpItem("@item:" + item.ItemId).Send(player);
                        return;
                    }

                    SystemMessages.YouReceiveItemXItemAmount(
                        player.PlayerData.Name, "@item:" + item.ItemId, item.Count).Send(player.Connection);
                }
            }

            player.Instance.RemoveItem(item);
        }

        public void PlayerLeaveWorld(Player player)
        {
            if (player.Instance != null)
            {
                if (player.Instance.Players.Count <= 1 && IsDungeon(player.Instance.MapId))
                    DestructInstance(player.Instance);
                else
                    DespawnTeraObject(player);

                player.Instance = null;

                player.Visible.Stop();
                player.Visible.Release();
                player.Visible = null;
            }
        }

        public static bool TryPutCampfire(Player player, WorldPosition position)
        {
            bool putCampfire = true;

            player.Instance.Campfires.Each(campfire =>
            {
                if (campfire.Position.DistanceTo(player.Position) < 1000)
                    putCampfire = false;
            });

            if (!putCampfire)
            {
                SystemMessages.TheresAnotherCampfireNearHere.Send(player);
                return false;
            }

            player.Instance.Campfires.Add(new Campfire
            {
                Instance = player.Instance,
                Type = 1,
                Status = 0,
                Position =
                    Geom.GetNormal(position.Heading).Multiple(50).Add(player.Position)
                    .ToWorldPosition(),
                DespawnUts = Funcs.GetCurrentMilliseconds() + 1200000, //20 minutes
            });

            return true;
        }

        public static Npc CreateNpc(SpawnTemplate spawnTemplate)
        {
            var npc = new Npc
            {
                NpcId = spawnTemplate.NpcId,
                SpawnTemplate = spawnTemplate,
                NpcTemplate = Data.Data.NpcTemplates[spawnTemplate.Type][spawnTemplate.NpcId],

                Position = new WorldPosition
                {
                    MapId = spawnTemplate.MapId,
                    X = spawnTemplate.X,
                    Y = spawnTemplate.Y,
                    Z =
                        spawnTemplate.Z +
                        ((spawnTemplate.FullId == 6301151 ||
                          spawnTemplate.FullId == 6301152 ||
                          spawnTemplate.FullId == 6301153)
                             ? 0
                             : 25),
                    Heading = spawnTemplate.Heading,
                }
            };

            npc.BindPoint = npc.Position.Clone();

            npc.GameStats = CreatureLogic.InitGameStats(npc);
            CreatureLogic.UpdateCreatureStats(npc);

            AiLogic.InitAi(npc);

            return npc;
        }

        public Gather CreateGather(GSpawnTemplate gSpawn)
        {
            var gather = new Gather
            {
                Id = gSpawn.CollectionId,
                Position = new WorldPosition
                {
                    MapId = gSpawn.WorldPosition.MapId,
                    X = gSpawn.WorldPosition.X,
                    Y = gSpawn.WorldPosition.Y,
                    Z = gSpawn.WorldPosition.Z + 10,
                },
                CurrentGatherCounter = new Random().Next(1, 3), //todo gather counter
            };

            AiLogic.InitAi(gather);

            return gather;
        }

        private void DestructInstance(MapInstance instance)
        {
            instance.Release();
            Maps[instance.MapId].Remove(instance);
        }

        private MapInstance GetMapInstance(Player player, int mapId)
        {
            if (IsDungeon(mapId))
            {
                if (player.Party != null)
                    lock (player.Party.MemberLock)
                        foreach (Player partyMember in player.Party.PartyMembers)
                            if (Global.PlayerService.IsPlayerOnline(partyMember) && partyMember.Instance != null &&
                                partyMember.Instance.MapId == mapId)
                                return partyMember.Instance;

                var dungeon = ((ADungeon)Activator.CreateInstance(Dungeons[mapId]));
                dungeon.MapId = mapId;

                SpawnMap(dungeon);

                dungeon.Init();
                return dungeon;
            }

            if (Maps[mapId].Count > 0)
                return Maps[mapId][0];

            var ins = new MapInstance { MapId = mapId };
            SpawnMap(ins);

            return ins;
        }

        public bool IsDungeon(int mapId)
        {
            return Dungeons.ContainsKey(mapId);
        }
    }
}