using Data.Interfaces;
using Data.Structures.Player;

namespace Tera.Controllers
{
    class DefaultController : IController
    {
        public Player Player;

        public void Start(Player player)
        {
            Player = player;
        }

        public void Release()
        {
            Player = null;
        }

        public void Action()
        {
        }
    }
}
