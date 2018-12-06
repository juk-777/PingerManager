using System;
using System.Net.NetworkInformation;

namespace PingerManager.Constructor
{
    public class PingReply : EventArgs
    {
        public DateTime PingDate { get; }
        public string Host { get; }
        public IPStatus Status { get; }

        public PingReply(DateTime pingDate, string host, IPStatus status)
        {
            PingDate = pingDate;
            Host = host;
            Status = status;
        }
    }
}
