using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace PingerManager.Logger
{
    class LoggerFactory : ILoggerFactory
    {
        public event EventHandler<PingReply> Logged;

        public void Log(object sender, PingReply pingReply)
        {
            OnLogged(pingReply);
        }

        protected virtual void OnLogged(PingReply e)
        {
            Logged?.Invoke(this, e);
        }
    }
}
