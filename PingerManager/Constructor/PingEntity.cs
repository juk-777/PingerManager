using PingerManager.Config;

namespace PingerManager.Constructor
{
    public class PingEntity
    {
        public ConfigEntity ConfigEntity { get; set; }
        public IProtocolProvider ProtocolProvider { get; set; }
    }
}
