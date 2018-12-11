using System;
using PingerManager.Config;
using System.Collections.Generic;

namespace PingerManager.Constructor
{
    public interface IPingBuilder : IDisposable
    {
        void Start(IEnumerable<ConfigEntity> configEntityList);
    }
}
