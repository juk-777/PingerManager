using PingerManager.Config;
using System;
using System.Net.NetworkInformation;

namespace PingerManager.Constructor
{
    public class PingReply : EventArgs
    {
        public DateTime PingDate { get; }
        public ConfigEntity ConfigEntity { get; }
        public IPStatus Status { get; }

        public PingReply(DateTime pingDate, ConfigEntity configEntity, IPStatus status)
        {
            PingDate = pingDate;
            ConfigEntity = configEntity;
            Status = status;
        }
    }
}
