using System.Collections.Generic;
using Data.Enums.Item;
using Data.Interfaces;
using Data.Structures.Player;
using Utils;

namespace Data.Structures.Account
{
    [ProtoBuf.ProtoContract]
    public class Account : TeraObject
    {
        public IConnection Connection;

        public bool IsOnline
        {
            get { return Connection != null; }
        }

        [ProtoBuf.ProtoMember(1)]
        public int AccountId;

        [ProtoBuf.ProtoMember(2)]
        public string Username;
        public string Password;
        public string Email;
        public byte AccessLevel;
        public byte Membership;
        public bool isGM;
        public long LastOnlineUtc;
        public int Coins;
        public string Ip;
        public string EmailVerify;
        public string FirstName;
        public string LastName;
        public string PasswordRecovery;


        [ProtoBuf.ProtoMember(3)]
        public List<Player.Player> Players = new List<Player.Player>();

        [ProtoBuf.ProtoMember(4)]
        public byte[] UiSettings = null;

        [ProtoBuf.ProtoMember(10)]
        public Storage AccountWarehouse = new Storage{StorageType = StorageType.AccountWarehouse};

        public DelayedAction ExitAction;

        public override void Release()
        {
            base.Release();

            for (int i = 0; i < Players.Count; i++)
                Players[i].Release();

            Players = null;
        }
    }
}