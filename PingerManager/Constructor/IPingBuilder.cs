using System;
using PingerManager.Config;
using System.Collections.Generic;
using System.Threading;

namespace PingerManager.Constructor
{
    public interface IPingBuilder : IDisposable
    {
        void Start(IEnumerable<ConfigEntity> configEntityList, CancellationToken token);
    }
}
