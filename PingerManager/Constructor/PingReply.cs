using System;
using System.Net.NetworkInformation;

namespace PingerManager.Constructor
{
    public class PingReply : EventArgs
    {
        public DateTime PingDate { get; }
        public PingEntity PingEntity { get; }
        public IPStatus Status { get; }

        public PingReply(DateTime pingDate, PingEntity pingEntity, IPStatus status)
        {
            PingDate = pingDate;
            PingEntity = pingEntity;
            Status = status;
        }
    }
}
