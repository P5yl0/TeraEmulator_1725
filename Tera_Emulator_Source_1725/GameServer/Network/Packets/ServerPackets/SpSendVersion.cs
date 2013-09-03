using System.IO;

namespace Network.Server
{
    public class SpSendVersion : ASendPacket
    {
        protected int Version;

        public SpSendVersion(int version)
        {
            Version = version;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteC(writer, (byte) (Version == OpCodes.Version ? 1 : 0));
        }
    }
}