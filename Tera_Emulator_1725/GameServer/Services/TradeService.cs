using Communication.Interfaces;
using Data.Structures.Player;
using Tera.Controllers;

namespace Tera.Services
{
    class TradeService : ITradeService
    {
        public void Action()
        { }

        public void AddItem(Player player, int slot, int count)
        {
            if(!(player.Controller is PlayerTradeController))
                return;

            ((PlayerTradeController) player.Controller).AddItem(player, slot, count);
        }

        public void RemoveItem(Player player, int slot, int count)
        {
            if (!(player.Controller is PlayerTradeController))
                return;

            ((PlayerTradeController)player.Controller).RemoveItem(player, slot, count);
        }

        public void ChangeMoney(Player player, long money)
        {
            if (!(player.Controller is PlayerTradeController))
                return;

            PlayerTradeController controller = (PlayerTradeController) player.Controller;
            Storage storage = player.Equals(controller.Player1) ? controller.Storage1 : controller.Storage2;

            if(money < storage.Money)
                controller.RemoveMoney(player, storage.Money - money);
            else if(money > storage.Money)
                controller.AddMoney(player, money - storage.Money);
        }

        public void Lock(Player player)
        {
            if (!(player.Controller is PlayerTradeController))
                return;

            ((PlayerTradeController)player.Controller).Lock(player);
        }

        public void Cancel(Player player)
        {
            if (!(player.Controller is PlayerTradeController))
                return;

            ((PlayerTradeController)player.Controller).Cancel(player);
        }
    }
}
