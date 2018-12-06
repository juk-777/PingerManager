using System;
using System.Threading.Tasks;
using PingerManager.Config;

namespace PingerManager.Constructor
{
    public interface IProtocolProvider
    {
        Task<PingReply> Ping(DateTime pingDate, ConfigEntity configEntity);
    }
}
