using System;
using PingerManager.Config;
using System.Collections.Generic;

namespace PingerManager.Constructor
{
    public interface IPingBuilder : IDisposable
    {
        void Start(List<ConfigEntity> configEntityList);
        event EventHandler<PingReply> Pinged;
    }
}
