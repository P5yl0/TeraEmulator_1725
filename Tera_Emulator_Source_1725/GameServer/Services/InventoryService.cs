using System.Collections.Generic;
using Communication.Interfaces;
using Data.Enums;
using Data.Enums.Item;
using Data.Structures.Player;
using Data.Structures.Template.Item;
using Network;
using Network.Server;
using Utils;

namespace Tera.Services
{
    internal class InventoryService : IInventoryService
    {
        public List<int> ThingsSlots = new List<int> {1, 3, 4, 5};

        public void ShowPlayerInventory(Player player)
        {
            //TODO resend players stats and pvp rates
            new SpInventory(player, true).Send(player.Connection);
        }

        public void AddStartItemsToPlayer(Player player)
        {
            player.Inventory.Items.Add(20, new InventoryItem {ItemId = 125, Count = 5});
            player.Inventory.Items.Add(21, new InventoryItem {ItemId = 8007, Count = 3});

            switch (player.PlayerData.Class)
            {
                case PlayerClass.Warrior:
                    player.Inventory.Items.Add(1, new InventoryItem {ItemId = 10001, Count = 1});
                    player.Inventory.Items.Add(3, new InventoryItem {ItemId = 15004, Count = 1});
                    player.Inventory.Items.Add(4, new InventoryItem {ItemId = 15005, Count = 1});
                    player.Inventory.Items.Add(5, new InventoryItem {ItemId = 15006, Count = 1});
                    break;
                case PlayerClass.Archer:
                    player.Inventory.Items.Add(1, new InventoryItem {ItemId = 10006, Count = 1});
                    player.Inventory.Items.Add(3, new InventoryItem {ItemId = 15004, Count = 1});
                    player.Inventory.Items.Add(4, new InventoryItem {ItemId = 15005, Count = 1});
                    player.Inventory.Items.Add(5, new InventoryItem {ItemId = 15006, Count = 1});
                    break;
                case PlayerClass.Slayer:
                    player.Inventory.Items.Add(1, new InventoryItem {ItemId = 10003, Count = 1});
                    player.Inventory.Items.Add(3, new InventoryItem {ItemId = 15004, Count = 1});
                    player.Inventory.Items.Add(4, new InventoryItem {ItemId = 15005, Count = 1});
                    player.Inventory.Items.Add(5, new InventoryItem {ItemId = 15006, Count = 1});
                    break;
                case PlayerClass.Berserker:
                    player.Inventory.Items.Add(1, new InventoryItem {ItemId = 10004, Count = 1});
                    player.Inventory.Items.Add(3, new InventoryItem {ItemId = 15001, Count = 1});
                    player.Inventory.Items.Add(4, new InventoryItem {ItemId = 15002, Count = 1});
                    player.Inventory.Items.Add(5, new InventoryItem {ItemId = 15003, Count = 1});
                    break;
                case PlayerClass.Lancer:
                    player.Inventory.Items.Add(1, new InventoryItem {ItemId = 10002, Count = 1});
                    player.Inventory.Items.Add(3, new InventoryItem {ItemId = 15001, Count = 1});
                    player.Inventory.Items.Add(4, new InventoryItem {ItemId = 15002, Count = 1});
                    player.Inventory.Items.Add(5, new InventoryItem {ItemId = 15003, Count = 1});
                    break;

                case PlayerClass.Sorcerer:
                    player.Inventory.Items.Add(1, new InventoryItem { ItemId = 10005, Count = 1 });
                    player.Inventory.Items.Add(3, new InventoryItem { ItemId = 15007, Count = 1 });
                    player.Inventory.Items.Add(4, new InventoryItem { ItemId = 15008, Count = 1 });
                    player.Inventory.Items.Add(5, new InventoryItem { ItemId = 15009, Count = 1 });
                    break;

                case PlayerClass.Mystic:
                    player.Inventory.Items.Add(1, new InventoryItem { ItemId = 10008, Count = 1 });
                    player.Inventory.Items.Add(3, new InventoryItem { ItemId = 15007, Count = 1 });
                    player.Inventory.Items.Add(4, new InventoryItem { ItemId = 15008, Count = 1 });
                    player.Inventory.Items.Add(5, new InventoryItem { ItemId = 15009, Count = 1 });
                    break;

                case PlayerClass.Priest:
                    player.Inventory.Items.Add(1, new InventoryItem { ItemId = 10007, Count = 1 });
                    player.Inventory.Items.Add(3, new InventoryItem { ItemId = 15007, Count = 1 });
                    player.Inventory.Items.Add(4, new InventoryItem { ItemId = 15008, Count = 1 });
                    player.Inventory.Items.Add(5, new InventoryItem { ItemId = 15009, Count = 1 });
                    break;
            }
        }


        public void AddItemToPlayer(Player player, int itemId, int counter)
        {
            List<KeyValuePair<int, InventoryItem>> itemsById = player.Inventory.GetItemsById(itemId);

            int maxInStack = ItemTemplate.Factory(itemId).MaxStack;

            bool canAdd = false;
            foreach (KeyValuePair<int, InventoryItem> keyValuePair in itemsById)
            {
                if (keyValuePair.Value.Count + counter <= maxInStack)
                {
                    canAdd = true;
                    break;
                }
            }

            if ((GetFreeSlots(player).Count < (counter/maxInStack) + (!canAdd ? 1 : 0)))
            {
                SystemMessages.InventoryIsFull.Send(player.Connection);
                return;
            }

            if (counter > maxInStack)
            {
                for (int i = 0; i < counter/maxInStack; i++)
                    AddItemToPlayer(player, new InventoryItem {ItemId = itemId, Count = maxInStack});

                AddItemToPlayer(player, new InventoryItem {ItemId = itemId, Count = counter - counter/maxInStack});
            }
            else
                AddItemToPlayer(player, new InventoryItem {ItemId = itemId, Count = counter});

        }

        public void AddItemToPlayer(Player player, InventoryItem item)
        {
            int maxInStack = item.ItemTemplate.MaxStack;
            lock (player.Inventory.ItemsLock)
            {
                List<int> freeSlots = GetFreeSlots(player);

                if (item.ItemId == 20000000)
                {
                    player.Inventory.Money += item.Count;
                    new SpInventory(player).Send(player.Connection);
                    return;
                }

                if (freeSlots.Count <= 0 && CanDress(player, item)) //TODO isStackable
                {
                    SystemMessages.InventoryIsFull.Send(player.Connection);
                    return;
                }


                if (item.ItemTemplate.MaxStack > 1) // TODO isStackable
                {
                    for (int i = 20; i < player.Inventory.Size + 20; i++)
                    {
                        if (!player.Inventory.Items.ContainsKey(i))
                            continue;

                        if (player.Inventory.Items[i].ItemId == item.ItemId)
                        {
                            if (player.Inventory.Items[i].Count + item.Count <= maxInStack)
                            {
                                player.Inventory.Items[i].Count += item.Count;
                                new SpInventory(player).Send(player.Connection);
                                return;
                            }
                            item.Count -= (maxInStack - player.Inventory.Items[i].Count);
                            player.Inventory.Items[i].Count = maxInStack;
                        }
                    }

                    if (freeSlots.Count > 0)
                        player.Inventory.Items.Add(freeSlots[0], item);

                    new SpInventory(player).Send(player.Connection);
                    return;
                }

                player.Inventory.Items.Add(freeSlots[0], item);
                new SpInventory(player).Send(player.Connection);
            }
        }

        public void RemoveItemFromPlayer(Player player, int slot, int count)
        {
            slot += 20;

            lock (player.Inventory.ItemsLock)
            {
                if (!player.Inventory.Items.ContainsKey(slot))
                {
                    Log.Warn("InventoryService: Player {0} try to remove item from invalid slot {1}",
                             player.PlayerData.Name, slot);
                    return;
                }

                if (player.Inventory.Items[slot].Count < count)
                {
                    Log.Warn("InventoryService: Player {0} try to remove too many items",
                             player.PlayerData.Name);
                    return;
                }

                if (player.Inventory.Items[slot].Count == count)
                {
                    player.Inventory.Items.Remove(slot);
                }
                else if (player.Inventory.Items[slot].Count > count)
                {
                    player.Inventory.Items[slot].Count -= count;
                }

                new SpInventory(player).Send(player.Connection);
            }
        }

        public void RemoveItemFromPlayerById(Player player, int itemId, int counter)
        {
            lock (player.Inventory.ItemsLock)
            {
                for (int i = 20; i <= player.Inventory.Size + 19; i++)
                    if (player.Inventory.Items.ContainsKey(i))
                    {
                        if (player.Inventory.Items[i].ItemId == itemId)
                        {
                            if (player.Inventory.Items[i].Count <= counter)
                                player.Inventory.Items.Remove(i);
                            else
                                player.Inventory.Items[i].Count -= counter;

                            new SpInventory(player).Send(player.Connection);

                            break;
                        }
                    }
            }
        }

        public List<int> GetFreeSlots(Player player)
        {
            var freeSlots = new List<int>();

            for (int i = 20; i <= player.Inventory.Size + 19; i++)
                if (!player.Inventory.Items.ContainsKey(i))
                    freeSlots.Add(i);

            return freeSlots;
        }

        public void AddMoneys(Player player, long counter)
        {
            player.Inventory.Money += counter;
        }

        public void RemoveMoney(Player player, long counter)
        {
            if (player.Inventory.Money - counter >= 0)
                player.Inventory.Money -= counter;
            else
            {
                player.Inventory.Money = 0;
                Log.Warn("InventorService: Player {0} moneys can't be less than 0");
            }

            new SpInventory(player).Send(player.Connection);
        }

        public bool ContainsItem(Player player, int itemId, long counter = 1)
        {
            lock (player.Inventory.ItemsLock)
            {
                for (int i = 20; i <= player.Inventory.Size + 19; i++)
                    if (player.Inventory.Items.ContainsKey(i))
                    {
                        if (player.Inventory.Items[i].ItemId == itemId && player.Inventory.Items[i].Count >= counter)
                            return true;
                    }
            }
            return false;
        }

        public InventoryItem GetInventoryItemById(Player player, int id)
        {
            lock (player.Inventory.ItemsLock)
            {
                for (int i = 20; i <= player.Inventory.Size + 19; i++)
                    if (player.Inventory.Items.ContainsKey(i))
                    {
                        if (player.Inventory.Items[i].ItemId == id)
                            return player.Inventory.Items[i];
                    }
            }

            return null;
        }

        public void ColldownItems(Player player, int itemId)
        {
            int groupId = ItemTemplate.Factory(itemId).CoolTimeGroup;

            if (groupId == 0)
                return;

            lock (player.Inventory.ItemsLock)
            {
                foreach (KeyValuePair<int, InventoryItem> inventoryItem in player.Inventory.Items)
                    if (Data.Data.CoolDownGroups[groupId].Contains(inventoryItem.Value.ItemId))
                        inventoryItem.Value.SetLastUseTime();
            }
        }

        public void ReplaceItemInInventory(Player player, int @from, int to, bool isForDress = false)
        {
            lock (player.Inventory.ItemsLock)
            {
                if (!player.Inventory.Items.ContainsKey(@from))
                    return;

                if (player.Inventory.Size + 20 < to) //prevent hack
                    return;

                if (from < 20 && GetFreeSlots(player).Count == 0)
                {
                    SystemMessages.InventoryIsFull.Send(player.Connection);
                    return;
                }

                InventoryItem item = player.Inventory.Items[@from];

                if (isForDress)
                {
                    if (to == 0)
                        to = GetDressSlot(item.ItemId);

                    if (to == 0)
                        return;
                }

                if (to != 0 && to < 20)
                {
                    if (!CanDress(player, item, true))
                        return;
                    
                    switch (GetDressSlot(item.ItemId))
                    {
                        case 6:
                            if (to != 6 && to != 7)
                                return;
                            break;
                        case 8:
                            if (to != 8 && to != 9)
                                return;
                            break;
                        default:
                            if (to != GetDressSlot(item.ItemId))
                                return;
                            break;
                    }
                }

                player.Inventory.Items.Remove(@from);

                if (player.Inventory.Items.ContainsKey(to))
                {
                    InventoryItem item2 = player.Inventory.Items[to];
                    player.Inventory.Items.Remove(to);
                    player.Inventory.Items.Add(@from, item2);
                }

                if (to == 0)
                    for (int i = 20; i < player.Inventory.Size + 20; i++)
                        if (!player.Inventory.Items.ContainsKey(i))
                        {
                            to = i;
                            break;
                        }

                player.Inventory.Items.Add(to, item);

                if(@from <=20 || to <= 20)
                    Communication.Logic.CreatureLogic.UpdateCreatureStats(player);
            }

            new SpInventory(player).Send(player.Connection);

            if (ThingsSlots.Contains(@from) || ThingsSlots.Contains(to))
                Communication.Logic.PlayerLogic.SendPlayerThings(player);

        }

        public void UpdatePlayerInventory(Player player)
        {
            throw new System.NotImplementedException();
        }

        private bool CanDress(Player player, InventoryItem item, bool sendErrors = false)
        {
            if (GetDressSlot(item.ItemId) > 20)
                return false;

            if (item.ItemTemplate.RequiredClassesList.Count > 0
                && !item.ItemTemplate.RequiredClassesList.Contains(player.PlayerData.Class))
            {
                if (sendErrors)
                    SystemMessages.ThatItemIsUnavailableToYourClass.Send(player.Connection);
                return false;
            }

            if (item.ItemTemplate.RequiredLevel > player.GetLevel())
            {
                if (sendErrors)
                    SystemMessages.YouMustBeAHigherLevelToUseThat.Send(player.Connection);
                return false;
            }

            return true;
        }

        private short GetDressSlot(int itemId)
        {
            switch (ItemTemplate.Factory(itemId).ItemCategory)
            {
                case ItemCategory.bodyLeather:
                case ItemCategory.bodyRobe:
                case ItemCategory.bodyMail:
                    return 3;

                case ItemCategory.dual:
                case ItemCategory.axe:
                case ItemCategory.rod:
                case ItemCategory.staff:
                case ItemCategory.bow:
                case ItemCategory.lance:
                case ItemCategory.circle:
                case ItemCategory.twohand:
                    return 1;

                case ItemCategory.earring:
                    return 6;
                case ItemCategory.ring:
                    return 8;
                case ItemCategory.necklace:
                    return 10;

                case ItemCategory.feetRobe:
                case ItemCategory.feetLeather:
                case ItemCategory.feetMail:
                    return 5;

                case ItemCategory.handMail:
                case ItemCategory.handRobe:
                case ItemCategory.handLeather:
                    return 4;
            }
            return 0;
        }

        public void Action()
        {
            
        }
    }
}
