using System;
using PingerManager.Config;
using System.Collections.Generic;
using PingerManager.Logging;

namespace PingerManager.Constructor
{
    public interface IPingBuilder : IDisposable
    {
        void Start(List<ConfigEntity> configEntityList, ILogger logger);
    }
}
