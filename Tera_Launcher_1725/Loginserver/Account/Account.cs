using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService
{
    public class Account
    {
        public Account(string name)
        {
            Username = name;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int AccessLevel { get; set; }
        public string Membership { get; set; }
        public bool isGM { get; set; }
        public int LastOnlineUtc { get; set; }
        public int Coins { get; set; }
        public string Ip { get; set; }
        public string UiSettings { get; set; }
    }
}
