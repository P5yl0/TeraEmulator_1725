using Network.Server;

namespace Network.Client
{
    public class RpGetCharacterEquipment : ARecvPacket
    {
        public override void Read()
        {
            //nothing
        }

        public override void Process()
        {
            new SendPacket("92C6").Send(Connection);
        }
    }
}