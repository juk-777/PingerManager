using PingerManager.Logging;
using System;
using System.Threading.Tasks;

namespace PingerManager.Constructor
{
    public interface IProtocolProvider
    {
        Task<PingReply> PingAsync(DateTime pingDate, PingEntity pingEntity, ILogger logger);
    }
}
