using Communication;
using Data.Structures.Player;
using Data.Structures.World.Requests;

namespace Tera.ActionEngine
{
    /// <summary>
    /// Performs party request
    /// </summary>
    public class PartyAction : IRequestAction
    {
        private readonly Player _owner;
        private readonly Player _target;

        public PartyAction(Request request)
        {
            _owner = request.Owner;
            _target = request.Target;
        }

        public void Accepted()
        {
            if (_owner.Party == null && _target.Party == null)
                Global.PartyService.AddNewParty(_owner, _target);
            else if (_owner.Party != null)
                Global.PartyService.AddPlayerToParty(_target, ref _owner.Party);
            // TODO: what if owner does not have party and target already belongs to party? 
            // MetaWind: That's handeling by PartyService
        }

        public void Declined()
        { }
    }
}
