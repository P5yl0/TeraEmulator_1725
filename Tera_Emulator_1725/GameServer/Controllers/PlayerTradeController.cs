using Data.Enums.Item;
using Data.Interfaces;
using Data.Structures;
using Data.Structures.Player;
using Data.Structures.World.Requests;
using Network;
using Network.Server;

namespace Tera.Controllers
{
    public class PlayerTradeController : IController
    {
        public Player Player1;
        public Player Player2;
        public Request Request;

        public const int MaxItems = 40;
        public Storage Storage1 = new Storage { Size = MaxItems, StorageType = StorageType.Trade, Money = 0};
        public Storage Storage2 = new Storage { Size = MaxItems, StorageType = StorageType.Trade, Money = 0};
        
        protected object TradeLock = new object();

        public PlayerTradeController(Request request)
        {
            lock (TradeLock)
            {
                Player1 = request.Owner;
                Player2 = request.Target;
                Request = request;

                SystemMessages.TradeHasBegun.Send(Player1, Player2);
                Communication.Global.StorageService.ShowPlayerStorage(Player1, StorageType.Inventory, false);
                Communication.Global.StorageService.ShowPlayerStorage(Player2, StorageType.Inventory, false);
            }
        }

        public void Start(Player player)
        {
            lock (TradeLock)
                UpdateWindow();
        }

        public void Release()
        {
            lock (TradeLock)
            {
                Communication.Global.ActionEngine.RemoveRequest(Request);
                Request = null;
            }
        }

        private void PreRelease()
        {
            lock (TradeLock)
            {
                if(IsTradeFinished())
                    return;

                new SpTradeHideWindow(Player1, Player2, Request.UID).Send(Player1, Player2);
                Communication.Global.StorageService.ShowPlayerStorage(Player1, StorageType.Inventory, false);
                Communication.Global.StorageService.ShowPlayerStorage(Player2, StorageType.Inventory, false);

                Storage1.Release();
                Storage2.Release();
            }
        }

        private void PostRelease()
        {
            Player1 = null;
            Player2 = null;
        }

        public void Action()
        { }

        public void Cancel(Player player)
        {
            lock (TradeLock)
            {
                if (IsTradeFinished())
                    return;

                SystemMessages.OpponentCanceledTheTrade(player.PlayerData.Name).Send(Player1, Player2);

                lock (Storage1.ItemsLock)
                {
                    foreach (var item in Storage1.Items.Values)
                        Communication.Global.StorageService.AddItem(Player1, Player1.Inventory, item);

                    Player1.Inventory.Money += Storage1.Money;
                }

                lock (Storage2.ItemsLock)
                {
                    foreach (var item in Storage2.Items.Values)
                        Communication.Global.StorageService.AddItem(Player2, Player2.Inventory, item);

                    Player2.Inventory.Money += Storage2.Money;
                }

                PreRelease();
                Communication.Global.ControllerService.SetController(Player1, new DefaultController());
                Communication.Global.ControllerService.SetController(Player2, new DefaultController());
                PostRelease();
            }
        }

        private void Accept()
        {
            SystemMessages.TradeCompleted.Send(Player1, Player2);

            lock (Storage1.ItemsLock)
            {
                foreach (var item in Storage1.Items.Values)
                    Communication.Global.StorageService.AddItem(Player2, Player2.Inventory, item);

                Player2.Inventory.Money += Storage1.Money;
            }

            lock (Storage2.ItemsLock)
            {
                foreach (var item in Storage2.Items.Values)
                    Communication.Global.StorageService.AddItem(Player1, Player1.Inventory, item);

                Player1.Inventory.Money += Storage2.Money;
            }

            PreRelease();
            Communication.Global.ControllerService.SetController(Player1, new DefaultController());
            Communication.Global.ControllerService.SetController(Player2, new DefaultController());
            PostRelease();
        }

        public void AddItem(Player arrivedFrom, int slot, int count)
        {
            lock (TradeLock)
            {
                if (IsTradeFinished())
                    return;

                // should be impossible, but it's better to check
                if (arrivedFrom != Player1 && arrivedFrom != Player2)
                    return;

                Storage storage = arrivedFrom.Equals(Player1) ? Storage1 : Storage2;
                if (storage.Locked)
                {
                    SystemMessages.TradeListLocked.Send(arrivedFrom);
                    return;
                }

                StorageItem item = Communication.Global.StorageService.GetItemBySlot(arrivedFrom.Inventory, slot + 20);
                if (item == null)
                    return;

                if (!Communication.Global.StorageService.AddItem(arrivedFrom, storage, item.ItemId, count))
                    return;

                CheckLock(storage.Equals(Storage1) ? Storage2 : Storage1);
                Communication.Global.StorageService.RemoveItem(arrivedFrom, arrivedFrom.Inventory, slot, count);
                SystemMessages.YouAddedItemNameXItemAmountToTrade(item.ItemId, count).Send(arrivedFrom);
                SystemMessages.OpponentAddedItemNameItemAmount(arrivedFrom.PlayerData.Name, item.ItemId, count).Send(
                    arrivedFrom.Equals(Player1) ? Player2 : Player1);
                UpdateWindow();
            }
        }

        public void RemoveItem(Player arrivedFrom, int slot, int count)
        {
            lock (TradeLock)
            {
                if (IsTradeFinished())
                    return;

                if (arrivedFrom != Player1 && arrivedFrom != Player2)
                    return;

                Storage storage = arrivedFrom.Equals(Player1) ? Storage1 : Storage2;
                if (storage.Locked)
                {
                    SystemMessages.TradeListLocked.Send(arrivedFrom);
                    return;
                }

                StorageItem item = Communication.Global.StorageService.GetItemBySlot(storage, slot);
                if (item == null)
                    return;

                if (!Communication.Global.StorageService.AddItem(arrivedFrom, arrivedFrom.Inventory, item.ItemId, count))
                    return;

                CheckLock(storage.Equals(Storage1) ? Storage2 : Storage1);
                Communication.Global.StorageService.RemoveItem(arrivedFrom, storage, slot - 20, count);
                SystemMessages.YouRemovedItemNameXItemAmountToTrade(item.ItemId, item.Amount).Send(arrivedFrom);
                SystemMessages.OpponentRemovedItemNameItemAmount(arrivedFrom.PlayerData.Name, item.ItemId, count).Send(
                    arrivedFrom.Equals(Player1) ? Player2 : Player1);
                UpdateWindow();
            }
        }

        public void AddMoney(Player arrivedFrom, long money)
        {
            lock (TradeLock)
            {
                if (IsTradeFinished())
                    return;

                if (arrivedFrom != Player1 && arrivedFrom != Player2)
                    return;

                Storage storage = arrivedFrom.Equals(Player1) ? Storage1 : Storage2;
                if (storage.Locked)
                {
                    SystemMessages.TradeListLocked.Send(arrivedFrom);
                    return;
                }

                if (!Communication.Global.StorageService.RemoveMoney(arrivedFrom, arrivedFrom.Inventory, money))
                    return;

                Communication.Global.StorageService.AddMoneys(arrivedFrom, storage, money);

                CheckLock(arrivedFrom.Equals(Player1) ? Storage2 : Storage1);
                SystemMessages.YouOfferedMoney(storage.Money).Send(arrivedFrom);
                SystemMessages.OpponentOfferedMoney(arrivedFrom.PlayerData.Name, storage.Money).Send(
                    arrivedFrom.Equals(Player1) ? Player2 : Player1);
                UpdateWindow();
            }
        }

        public void RemoveMoney(Player arrivedFrom, long money)
        {
            lock (TradeLock)
            {
                if (IsTradeFinished())
                    return;

                if (arrivedFrom != Player1 && arrivedFrom != Player2)
                    return;

                Storage storage = arrivedFrom.Equals(Player1) ? Storage1 : Storage2;
                if (storage.Locked)
                {
                    SystemMessages.TradeListLocked.Send(arrivedFrom);
                    return;
                }

                if (!Communication.Global.StorageService.RemoveMoney(arrivedFrom, storage, money))
                    return;

                Communication.Global.StorageService.AddMoneys(arrivedFrom, arrivedFrom.Inventory, money);

                CheckLock(arrivedFrom.Equals(Player1) ? Storage2 : Storage1);
                SystemMessages.YouOfferedMoney(storage.Money).Send(arrivedFrom);
                SystemMessages.OpponentOfferedMoney(arrivedFrom.PlayerData.Name, storage.Money).Send(
                    arrivedFrom.Equals(Player1) ? Player2 : Player1);
                UpdateWindow();
            }
        }

        public void Lock(Player arrivedFrom)
        {
            lock (TradeLock)
            {
                if (IsTradeFinished())
                    return;

                if (arrivedFrom != Player1 && arrivedFrom != Player2)
                    return;

                if (arrivedFrom.Equals(Player1))
                {
                    Storage1.Locked = !Storage1.Locked;

                    if (Storage1.Locked)
                        SystemMessages.OpponentLockedTradeList(Player1.PlayerData.Name).Send(Player1, Player2);
                }
                else
                {
                    Storage2.Locked = !Storage2.Locked;

                    if (Storage2.Locked)
                        SystemMessages.OpponentLockedTradeList(Player2.PlayerData.Name).Send(Player1, Player2);
                }

                if (Storage1.Locked && Storage2.Locked)
                    Accept();
                else
                    UpdateWindow();
            }
        }

        private void UpdateWindow()
        {
            new SpTradeWindow(Player1, Player2, Storage1, Storage2, Request.UID).Send(Player1, Player2);
        }

        private void CheckLock(Storage storage)
        {
            if (!storage.Locked)
                return;

            storage.Locked = false;
            SystemMessages.TradeHasChangedPleaseCheckItAgain.Send(storage.Equals(Storage1) ? Player1 : Player2);
        }

        private bool IsTradeFinished()
        {
            if (Player1 == null || Player2 == null || Request == null)
                return true;
            return false;
        }
    }
}
