using PingerManager.Config;
using System;

namespace PingerManager.Constructor
{
    public interface IProtocolProviderManager : IDisposable
    {
        IProtocolProvider GetProvider(ConfigEntity configEntity);
    }
}
