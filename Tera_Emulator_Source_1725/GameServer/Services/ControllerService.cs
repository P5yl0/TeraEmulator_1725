using Communication.Interfaces;
using Data.Interfaces;
using Data.Structures;
using Data.Structures.Npc;
using Data.Structures.Player;
using Tera.Controllers;

namespace Tera.Services
{
    class ControllerService : IControllerService
    {
        public void PlayerEnterWorld(Player player)
        {
            if (!(player.Controller is FlyController))
                SetController(player, new DefaultController());
        }

        public void PlayerStartDialog(Player player, TeraObject o)
        {
            if (o is Npc)
                SetController(player, new DialogController(player, (Npc)o));
        }

        public void PlayerEndGame(Player player)
        {
            //todo Maybe better create a callback in IController? smth like OnEndGame?
            if(player.Controller is PlayerTradeController)
                ((PlayerTradeController)player.Controller).Cancel(player);
            SetController(player, null);
        }

        public void TrySetDefaultController(Player player)
        {
            if (player.Controller is DefaultController)
                return;

            SetController(player, new DefaultController());
        }

        public void SetController(Player player, IController controller)
        {
            if (player.Controller != null)
                player.Controller.Release();

            player.Controller = controller;

            if (player.Controller != null)
                player.Controller.Start(player);
        }

        public void Action()
        {
            
        }
    }
}
