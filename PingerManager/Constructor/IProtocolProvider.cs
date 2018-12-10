using System;
using System.Threading.Tasks;

namespace PingerManager.Constructor
{
    public interface IProtocolProvider
    {
        Task<PingReply> Ping(DateTime pingDate, PingEntity pingEntity);
    }
}
