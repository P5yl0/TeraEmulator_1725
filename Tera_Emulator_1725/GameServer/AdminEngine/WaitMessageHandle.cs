using System.Threading;
using Communication;
using Data.Interfaces;

namespace Tera.AdminEngine
{
    class WaitMessageHandle
    {
        protected ManualResetEvent ManualResetEvent;

        protected string Value = null;

        public string GetValue(IConnection connection)
        {
            using (ManualResetEvent = new ManualResetEvent(false))
            {
                var handles = ((AdminEngine) Global.AdminEngine).WaitValueHandles;

                if (handles.ContainsKey(connection))
                {
                    handles[connection].SendValue(null);
                    handles.Remove(connection);
                }

                handles.Add(connection, this);
                ManualResetEvent.WaitOne(10000);
                handles.Remove(connection);
            }

            return Value;
        }

        public void SendValue(string value)
        {
            Value = value;
            ManualResetEvent.Set();
        }
    }
}
