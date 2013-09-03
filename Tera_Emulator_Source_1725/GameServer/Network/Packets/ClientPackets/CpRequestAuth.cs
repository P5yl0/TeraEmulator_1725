using System.Text;


namespace Network.Client
{
    public class CpRequestAuth : ARecvPacket
    {
        protected string AccountName;
        protected string Session;

        public override void Read()
        {
            ReadH(); //unk1
            ReadH(); //unk2
            int length = ReadH();
            ReadB(5); //unk3
            ReadD(); //unk4
            AccountName = ReadS(); //AccountName !!! ???
            Session = Encoding.ASCII.GetString(ReadB(length));
        }

        public override void Process()
        {
            Communication.Logic.AccountLogic.TryAuthorize(Connection, AccountName, Session);
        }
    }
}