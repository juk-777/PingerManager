using PingerManager.Config;

namespace PingerManager.Constructor
{
    public interface IProtocolProviderManager
    {
        IProtocolProvider GetProvider(ConfigEntity configEntity);
    }
}
