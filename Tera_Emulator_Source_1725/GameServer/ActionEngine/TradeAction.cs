using Data.Structures.World.Requests;
using Network;
using Network.Server;
using Tera.Controllers;

namespace Tera.ActionEngine
{
    public class TradeAction : IRequestAction
    {
        private Request _request;

        public TradeAction(Request request)
        {
            _request = request;
        }

        public void Accepted()
        {
            // Start trade
            PlayerTradeController controller = new PlayerTradeController(_request);
            Communication.Global.ControllerService.SetController(_request.Owner, controller);
            Communication.Global.ControllerService.SetController(_request.Target, controller);

            // Close windows, but do not remove request yet
            new SpHideRequest(_request).Send(_request.Owner);
            new SpHideRequest(_request).Send(_request.Target);
        }

        public void Declined()
        {
            SystemMessages.UserRejectedATrade(_request.Target.PlayerData.Name).Send(_request.Owner);
            Communication.Global.ActionEngine.RemoveRequest(_request);
        }
    }
}
