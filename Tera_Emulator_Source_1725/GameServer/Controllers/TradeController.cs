using System.Collections.Generic;
using Data.Enums;
using Data.Structures.Player;
using Data.Structures.Template.Item;
using Data.Structures.World;
using Network;
using Network.Server;
using Utils;

namespace Tera.Controllers
{
    internal class TradeController : DefaultController
    {
        protected Player Player;
        protected Tradelist Tradelist;
        protected ShoppingCart Cart;

        public TradeController(Player player, Tradelist tradelist)
        {
            Player = player;
            Tradelist = tradelist;
            Cart = new ShoppingCart();
            new SpTradeList(Tradelist).Send(Player.Connection);
            new SpUpdateTrade(Player, Cart).Send(Player.Connection);
            new SpInventory(player, true).Send(Player.Connection);

            SystemMessages.TradeHasBegun.Send(Player.Connection);
        }

        public void RemoveFromBuyItems(int itemId, int count)
        {
            foreach (var shoppingItem in Cart.BuyItems)
            {
                if (shoppingItem.ItemTemplate.Id != itemId)
                    continue;

                if (shoppingItem.Count < count)
                    return;

                if (shoppingItem.Count > count)
                    shoppingItem.Count -= count;
                else
                    Cart.BuyItems.Remove(shoppingItem);

                new SpUpdateTrade(Player, Cart).Send(Player.Connection);
                return;
            }
        }

        public void RemoveFromSellItems(int itemId, int count)
        {
            lock (Player.Inventory.ItemsLock)
            {
                foreach (var shoppingItem in Cart.SellItems)
                {
                    if (shoppingItem.ItemTemplate.Id != itemId)
                        continue;

                    if (shoppingItem.Count < count)
                        return;

                    Communication.Global.StorageService.AddItem(Player, Player.Inventory, itemId, count);

                    if (shoppingItem.Count > count)
                        shoppingItem.Count -= count;
                    else
                        Cart.SellItems.Remove(shoppingItem);

                    new SpUpdateTrade(Player, Cart).Send(Player.Connection);
                    return;
                }
            }
        }

        public void BuyItem(int itemId, int count)
        {

            if (!Data.Data.ItemTemplates.ContainsKey(itemId))
            {
                new SpSystemNotice("Item not exist!").Send(Player.Connection);
                Log.Warn("NpcTradeController: Item {0} not exist!", itemId);
                return;
            }

            ItemTemplate item = Data.Data.ItemTemplates[itemId];

            if (item.BuyPrice == 0)
            {
                new SpSystemNotice("Item cost not set!").Send(Player.Connection);
                Log.Warn("Item cost not set fro item {0}", item.Id);
                return;
            }

            bool added = false;

            if (item.MaxStack > 1)
            {
                foreach (var buyItem in Cart.BuyItems)
                {
                    if (buyItem.ItemTemplate == item)
                    {
                        buyItem.AddCount(count);
                        added = true;
                    }
                }
            }

            if (!added)
            {
                if (Cart.BuyItems.Count >= 8)
                    return;

                Cart.BuyItems.Add(new ShoppingCart.ShoppingItem(item, count));
            }

            new SpUpdateTrade(Player, Cart).Send(Player.Connection);
        }

        public void SellItem(int itemId, int count, int slot)
        {
            if (!Data.Data.ItemTemplates.ContainsKey(itemId))
            {
                new SpSystemNotice("Item not exist!").Send(Player.Connection);
                return;
            }

            ItemTemplate item = Data.Data.ItemTemplates[itemId];

            if (!item.StoreSellable || item.SellPrice == 0)
            {
                SystemMessages.YouCantTradeItem("@item:" + item.Id).Send(Player.Connection);
                return;
            }

            if (Cart.SellItems.Count >= 8)
                return;

            StorageItem selectedItem;

            lock (Player.Inventory.ItemsLock)
            {
                if (!Player.Inventory.Items.ContainsKey(slot))
                    return;

                if (Player.Inventory.Items[slot].Amount < count)
                    return;

                selectedItem = Player.Inventory.Items[slot];

                if (Player.Inventory.Items[slot].Amount == count)
                    Player.Inventory.Items.Remove(slot);
                else
                    Player.Inventory.Items[slot].Amount -= count;
            }

            Cart.SellItems.Add(new ShoppingCart.ShoppingItem(item, count, selectedItem));

            new SpInventory(Player).Send(Player.Connection);
            new SpUpdateTrade(Player, Cart).Send(Player.Connection);
        }

        public void CompleteTraid(bool forced)
        {
            if (forced)
            {
                SystemMessages.TradeCanceled.Send(Player.Connection);

                lock (Player.Inventory.ItemsLock)
                    foreach (var shoppingItem in Cart.SellItems)
                        Communication.Global.StorageService.AddItem(Player, Player.Inventory, shoppingItem.ItemTemplate.Id,
                                                                              shoppingItem.Count);

                if (Communication.Global.PlayerService.IsPlayerOnline(Player))
                    new SpInventory(Player).Send(Player.Connection);

                new SpSystemWindow(SystemWindow.Hide).Send(Player.Connection);

                Player.CurrentDialog = null;
            }
            else
            {
                if (Player.Inventory.Money < Cart.GetBuyItemsPrice())
                {
                    SystemMessages.YouCantTrade.Send(Player.Connection);
                    return;
                }

                if (Communication.Global.StorageService.GetFreeSlots(Player.Inventory).Count < Cart.BuyItems.Count)
                {
                    SystemMessages.InventoryIsFull.Send(Player.Connection);
                    return;
                }

                Communication.Global.StorageService.RemoveMoney(Player, Player.Inventory, Cart.GetBuyItemsPrice());
                Communication.Global.StorageService.AddMoneys(Player, Player.Inventory, Cart.GetSellItemsPrice());

                foreach (var shoppingItem in Cart.BuyItems)
                    Communication.Global.StorageService.AddItem(Player, Player.Inventory, shoppingItem.ItemTemplate.Id,
                                                                          shoppingItem.Count);

                Cart.BuyItems = new List<ShoppingCart.ShoppingItem>();
                Cart.SellItems = new List<ShoppingCart.ShoppingItem>();

                new SpInventory(Player).Send(Player.Connection);
                new SpUpdateTrade(Player, Cart).Send(Player.Connection);

                SystemMessages.TradeCompleted.Send(Player.Connection);
            }
        }
    }
}
