using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace PingerManager.Logger
{
    public interface ILoggerFactory
    {
        event EventHandler<PingReply> Logged;

        void Log(object sender, PingReply pingReply);
    }
}
