using System;
using Data.Interfaces;
using Utils;

namespace Network.Server
{
    public class SendPacket
    {
        protected byte[] Data;

        public SendPacket(byte[] data, bool addLength = true)
        {
            if (addLength)
            {
                Data = new byte[data.Length + 2];
                byte[] length = BitConverter.GetBytes((short) Data.Length);
                Buffer.BlockCopy(length, 0, Data, 0, 2);
                Buffer.BlockCopy(data, 0, Data, 2, data.Length);
            }
            else
            {
                Data = new byte[data.Length];
                Buffer.BlockCopy(data, 0, Data, 0, data.Length);
            }
        }

        public SendPacket(string hex, bool addLength = true) : this(hex.HexSringToBytes(), addLength)
        {
        }

        public void Send(IConnection state)
        {
            Log.Debug("SendPacket Data:\n{0}", Data.FormatHex());
            state.PushPacket(Data);
        }
    }
}