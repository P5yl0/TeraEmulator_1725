using Data.Interfaces;

namespace Tera.AdminEngine
{
    public class Script
    {
        public IConnection Connection;

        public virtual void Run(string args){}

    }
}
