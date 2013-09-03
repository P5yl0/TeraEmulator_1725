using System;
using System.Collections.Generic;
using System.Linq;
using Communication.Interfaces;
using Data.Enums;
using Data.Enums.Item;
using Data.Structures.Player;
using Data.Structures.Template.Item;
using Data.Structures.World.Requests;
using Network;
using Network.Server;
using Tera.Controllers;
using Utils;

namespace Tera.Services
{
    internal class StorageService : IStorageService
    {
        public List<int> ThingsSlots = new List<int> {1, 3, 4, 5};

        public void Action()
        {
            
        }

        public void ShowPlayerStorage(Player player, StorageType storageType, bool shadowUpdate = true, int offset = 0)
        {
            switch (storageType)
            {
                case StorageType.Inventory:
                    new SpInventory(player, !shadowUpdate).Send(player.Connection);
                    break;
                case StorageType.CharacterWarehouse:
                    new SpShowWindow(new EmptyRequest(player, RequestType.RegularWarhouse)).Send(player);
                    new SpWarehouseItems(player, offset).Send(player);
                    break;
                case StorageType.Trade:
                    // Updates are sent by the controller
                    break;
            }
        }

        public void AddStartItemsToPlayer(Player player)
        {
            player.Inventory.Items.Add(20, new StorageItem {ItemId = 125, Amount = 5});
            player.Inventory.Items.Add(21, new StorageItem { ItemId = 8007, Amount = 3 });

            switch (player.PlayerData.Class)
            {
                case PlayerClass.Warrior:
                    player.Inventory.Items.Add(1, new StorageItem { ItemId = 10001, Amount = 1 });
                    player.Inventory.Items.Add(3, new StorageItem { ItemId = 15004, Amount = 1 });
                    player.Inventory.Items.Add(4, new StorageItem { ItemId = 15005, Amount = 1 });
                    player.Inventory.Items.Add(5, new StorageItem { ItemId = 15006, Amount = 1 });
                    break;
                case PlayerClass.Archer:
                    player.Inventory.Items.Add(1, new StorageItem { ItemId = 10006, Amount = 1 });
                    player.Inventory.Items.Add(3, new StorageItem { ItemId = 15004, Amount = 1 });
                    player.Inventory.Items.Add(4, new StorageItem { ItemId = 15005, Amount = 1 });
                    player.Inventory.Items.Add(5, new StorageItem { ItemId = 15006, Amount = 1 });
                    break;
                case PlayerClass.Slayer:
                    player.Inventory.Items.Add(1, new StorageItem { ItemId = 10003, Amount = 1 });
                    player.Inventory.Items.Add(3, new StorageItem { ItemId = 15004, Amount = 1 });
                    player.Inventory.Items.Add(4, new StorageItem { ItemId = 15005, Amount = 1 });
                    player.Inventory.Items.Add(5, new StorageItem { ItemId = 15006, Amount = 1 });
                    break;
                case PlayerClass.Berserker:
                    player.Inventory.Items.Add(1, new StorageItem { ItemId = 10004, Amount = 1 });
                    player.Inventory.Items.Add(3, new StorageItem { ItemId = 15001, Amount = 1 });
                    player.Inventory.Items.Add(4, new StorageItem { ItemId = 15002, Amount = 1 });
                    player.Inventory.Items.Add(5, new StorageItem { ItemId = 15003, Amount = 1 });
                    break;
                case PlayerClass.Lancer:
                    player.Inventory.Items.Add(1, new StorageItem { ItemId = 10002, Amount = 1 });
                    player.Inventory.Items.Add(3, new StorageItem { ItemId = 15001, Amount = 1 });
                    player.Inventory.Items.Add(4, new StorageItem { ItemId = 15002, Amount = 1 });
                    player.Inventory.Items.Add(5, new StorageItem { ItemId = 15003, Amount = 1 });
                    break;

                case PlayerClass.Sorcerer:
                    player.Inventory.Items.Add(1, new StorageItem { ItemId = 10005, Amount = 1 });
                    player.Inventory.Items.Add(3, new StorageItem { ItemId = 15007, Amount = 1 });
                    player.Inventory.Items.Add(4, new StorageItem { ItemId = 15008, Amount = 1 });
                    player.Inventory.Items.Add(5, new StorageItem { ItemId = 15009, Amount = 1 });
                    break;

                case PlayerClass.Mystic:
                    player.Inventory.Items.Add(1, new StorageItem { ItemId = 10008, Amount = 1 });
                    player.Inventory.Items.Add(3, new StorageItem { ItemId = 15007, Amount = 1 });
                    player.Inventory.Items.Add(4, new StorageItem { ItemId = 15008, Amount = 1 });
                    player.Inventory.Items.Add(5, new StorageItem { ItemId = 15009, Amount = 1 });
                    break;

                case PlayerClass.Priest:
                    player.Inventory.Items.Add(1, new StorageItem { ItemId = 10007, Amount = 1 });
                    player.Inventory.Items.Add(3, new StorageItem { ItemId = 15007, Amount = 1 });
                    player.Inventory.Items.Add(4, new StorageItem { ItemId = 15008, Amount = 1 });
                    player.Inventory.Items.Add(5, new StorageItem { ItemId = 15009, Amount = 1 });
                    break;
            }
        }

        public bool AddItem(Player player, Storage storage, int itemId, int count, int slot = -1)
        {
            if (count < 0)
                return false;

            if (slot > (storage.StorageType == StorageType.Inventory ? storage.Size + 20 : storage.Size))
                return false;

            if ((storage.StorageType == StorageType.Inventory && slot < 20 && slot != -1))
                return false;

            if (slot < -1)
                return false;

            lock (storage.ItemsLock)
            {

                int stackSize = ItemTemplate.Factory(itemId).MaxStack;

                if (stackSize == 1 && storage.IsFull())
                {
                    SystemMessages.InventoryIsFull.Send(player);
                    return false;
                }

                if (stackSize == 1)
                {
                    if (slot == -1)
                        storage.Items.Add(storage.GetFreeSlot(), new StorageItem {ItemId = itemId, Amount = count});
                    else if (storage.Items.ContainsKey(slot))
                    {
                        SystemMessages.YouCantPutItemInInventory(ItemTemplate.Factory(itemId).Name).Send(player);
                        return false;
                    }
                    else
                        storage.Items.Add(slot, new StorageItem {ItemId = itemId, Amount = count});
                }
                else
                {
                    if (slot != -1)
                    {
                        // Certain slot + Stackable
                        if (storage.Items.ContainsKey(slot))
                        {
                            SystemMessages.YouCantPutItemInInventory(ItemTemplate.Factory(itemId).Name).Send(player);
                            return false;
                        }

                        storage.Items.Add(slot, new StorageItem { ItemId = itemId, Amount = count });
                    }
                    else
                    {
                        #region Any slot + Stackable
                        Dictionary<int, StorageItem> itemsById = storage.GetItemsById(itemId);

                        int canBeAdded =
                            itemsById.Values.Where(storageItem => storageItem.Amount < stackSize).Sum(
                                storageItem => stackSize - storageItem.Amount);

                        if (canBeAdded >= count)
                        {
                            foreach (var storageItem in itemsById.Values)
                            {
                                int added = Math.Min(stackSize - storageItem.Amount, count);
                                storageItem.Amount += added;
                                count -= added;
                                if (count == 0)
                                    break;
                            }
                        }
                        else
                        {
                            if (storage.IsFull() || count > GetFreeSlots(storage).Count*stackSize)
                            {
                                SystemMessages.InventoryIsFull.Send(player);
                                return false;
                            }

                            foreach (var storageItem in itemsById.Values)
                            {
                                int added = Math.Min(stackSize - storageItem.Amount, count);
                                storageItem.Amount += added;
                                count -= added;
                            }
                            while (count > 0)
                            {
                                int added = Math.Min(stackSize, count);
                                StorageItem item = new StorageItem {ItemId = itemId, Amount = added};
                                storage.Items.Add(storage.GetFreeSlot(), item);
                                count -= added;
                            }
                        }
                        #endregion
                    }
                }

                ShowPlayerStorage(player, storage.StorageType);
                return true;
            }
        }

        public bool AddItem(Player player, Storage storage, StorageItem item)
        {
            lock (storage.ItemsLock)
            {
                int maxStack = ItemTemplate.Factory(item.ItemId).MaxStack;
                int canStacked = 0;

                if (maxStack > 1)
                {
                    for (int i = 20; i < 20 + storage.Size; i++)
                    {
                        if (!storage.Items.ContainsKey(i)) continue;

                        if (storage.Items[i].ItemId == item.ItemId)
                        {
                            canStacked += maxStack - storage.Items[i].Amount;

                            if (canStacked >= item.Amount)
                                break;
                        }
                    }
                }

                if (canStacked < item.Amount && GetFreeSlots(storage).Count < 1)
                    return false;

                if (canStacked > 0)
                {
                    for (int i = 20; i < 20 + storage.Size; i++)
                    {
                        if (!storage.Items.ContainsKey(i)) continue;

                        if (storage.Items[i].ItemId == item.ItemId)
                        {
                            int put = maxStack - storage.Items[i].Amount;
                            if (item.Amount < put)
                                put = item.Amount;

                            storage.Items[i].Amount += put;
                            item.Amount -= put;

                            if (item.Amount <= 0)
                                break;
                        }
                    }
                }

                if (item.Amount > 0)
                    storage.Items.Add(storage.GetFreeSlot(), item);

                ShowPlayerStorage(player, storage.StorageType);
                return true;
            }
        }

        public bool RemoveItem(Player player, Storage storage, int slot, int counter)
        {
            if (counter < 0)
                return false;

            slot += 20;

            lock (storage.ItemsLock)
            {
                if (!storage.Items.ContainsKey(slot) || storage.Items[slot].Amount < counter)
                    return false;

                if (storage.Items[slot].Amount == counter)
                    storage.Items.Remove(slot);
                else if (storage.Items[slot].Amount > counter)
                    storage.Items[slot].Amount -= counter;

                ShowPlayerStorage(player, storage.StorageType, false);
            }
            return true;
        }

        public bool RemoveItemById(Player player, Storage storage, int itemId, int counter)
        {
            //todo rework
            for (int i = 20; i <= player.Inventory.Size + 19; i++)
                if (player.Inventory.Items.ContainsKey(i))
                {
                    if (player.Inventory.Items[i].ItemId == itemId)
                    {
                        if (player.Inventory.Items[i].Amount <= counter)
                            player.Inventory.Items.Remove(i);
                        else
                            player.Inventory.Items[i].Amount -= counter;

                        ShowPlayerStorage(player, storage.StorageType);
                        return true;
                    }
                }
            return false;
        }

        public void ReplaceItem(Player player, Storage storage, int @from, int to, bool isForDress = false, bool showStorage = true)
        {
            lock (storage.ItemsLock)
            {
                if (!storage.Items.ContainsKey(@from))
                    return;

                if (from < 20 && GetFreeSlots(storage).Count == 0)
                    return;

                StorageItem item = storage.Items[@from];

                if (isForDress && storage.StorageType == StorageType.Inventory)
                {
                    if (to == 0)
                        to = GetDressSlot(item.ItemId);

                    if (to == 0)
                        return;
                }

                if (to != 0 && to < 20 && storage.StorageType == StorageType.Inventory)
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

                storage.Items.Remove(@from);

                if (storage.Items.ContainsKey(to))
                {
                    StorageItem item2 = storage.Items[to];
                    storage.Items.Remove(to);
                    storage.Items.Add(@from, item2);
                }

                if (to == 0)
                    to = storage.GetFreeSlot();

                storage.Items.Add(to, item);

                if (@from <= 20 || to <= 20)
                    Communication.Logic.CreatureLogic.UpdateCreatureStats(player);
            }

            if (showStorage)
            {
                ShowPlayerStorage(player, storage.StorageType);
                if(@from <= 20 || to <= 20)
                    new SpCharacterThings(player).Send(player.Connection);
            }
        }

        public bool AddMoneys(Player player, Storage storage, long counter)
        {
            if (counter < 0)
                return false;

            storage.Money += counter;
            ShowPlayerStorage(player, storage.StorageType);

            return true;
        }

        public bool RemoveMoney(Player player, Storage storage, long counter)
        {
            if (counter < 0)
                return false;

            if (storage.Money - counter >= 0)
                storage.Money -= counter;
            else
            {
                player.Inventory.Money = 0;
                Log.Warn("InventorService: Player {0} moneys can't be less than 0");
                ShowPlayerStorage(player, storage.StorageType);
                return false;
            }

            ShowPlayerStorage(player, storage.StorageType);
            return true;
        }

        public bool ContainsItem(Storage storage, int itemId, long counter)
        {
            lock (storage.ItemsLock)
            {
                for (int i = (storage.StorageType == StorageType.Inventory ? 20 : 0);
                     i <= storage.Size + (storage.StorageType == StorageType.Inventory ? 19 : 0);
                     i++)
                    if (storage.Items.ContainsKey(i))
                    {
                        if (storage.Items[i].ItemId == itemId && storage.Items[i].Amount >= counter)
                            return true;
                    }
            }
            return false;
        }

        public StorageItem GetItemById(Storage storage, int id)
        {
            lock (storage.ItemsLock)
            {
                for (int i = (storage.StorageType == StorageType.Inventory ? 20 : 0);
                     i <= storage.Size + (storage.StorageType == StorageType.Inventory ? 19 : 0);
                     i++)
                    if (storage.Items.ContainsKey(i))
                    {
                        if (storage.Items[i].ItemId == id)
                            return storage.Items[i];
                    }
            }
            return null;
        }

        public StorageItem GetItemBySlot(Storage storage, int slot)
        {
            lock (storage.ItemsLock)
            {
                if (!storage.Items.ContainsKey(slot))
                    return null;

                return storage.Items[slot];
            }
        }

        public bool PlaceItemToOtherStorage(Player player, Player second, Storage from, int fromSlot, Storage to, int toSlot, int count)
        {
            if (to.IsFull())
                return false;

            if (count < 0)
                return false;

            if (!(player.Controller is DefaultController) || !(second.Controller is DefaultController))
                return false;

            if(player == second)
            {
                StorageItem item = null;

                if(to.StorageType == StorageType.CharacterWarehouse)
                {
                    if(toSlot == -1)
                        toSlot = to.GetFreeSlot(player.PlayerCurrentBankSection * 72);

                    if(toSlot == 0 && to.Items.ContainsKey(toSlot))
                        toSlot = to.GetFreeSlot(player.PlayerCurrentBankSection * 72);

                    if (toSlot == to.LastIdRanged(player.PlayerCurrentBankSection * 72, (player.PlayerCurrentBankSection + 1) * 72 - 1))
                        toSlot = to.GetFreeSlot(player.PlayerCurrentBankSection * 72);
                }
                else
                    if ((toSlot == 0 && to.Items.ContainsKey(toSlot)) || toSlot == -1)
                        toSlot = to.GetFreeSlot();

                if (to.Items.ContainsKey(toSlot))
                {
                    if (!PlaceItemToOtherStorage(player, player, to, toSlot, player.Inventory, player.Inventory.GetFreeSlot(), to.Items[toSlot].Amount))
                        return false;
                }

                lock (from.ItemsLock)
                {
                    if(from.Items.ContainsKey(fromSlot))
                    {
                        item = from.Items[fromSlot];

                        if(item.Amount < count)
                            return false;

                        if (item.Amount == count)
                            from.Items.Remove(fromSlot);
                        else
                        {
                            from.Items[fromSlot].Amount -= count;
                            item = new StorageItem { ItemId = item.ItemId, Amount = count };
                        }
                    }
                }

                if (item == null)
                    return false;

                if (!AddItem(player, to, item.ItemId, count, toSlot))
                    AddItem(player, from, item.ItemId, count, fromSlot);
            }
            return false;
        }

        public List<int> GetFreeSlots(Storage storage)
        {
            var freeSlots = new List<int>();

            for (int i = (storage.StorageType == StorageType.Inventory ? 20 : 0);
                i <= storage.Size + (storage.StorageType == StorageType.Inventory ? 19 : 0);
                i++)
             if (!storage.Items.ContainsKey(i))
                 freeSlots.Add(i);

            return freeSlots;
        }

        private bool CanDress(Player player, StorageItem item, bool sendErrors = false)
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
    }
}
