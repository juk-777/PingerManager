using System;
using System.Threading.Tasks;
using PingerManager.Config;

namespace PingerManager.Constructor
{
    public interface IProtocolProvider : IDisposable
    {
        Task<string> Ping(DateTime dateTime, ConfigEntity configEntity);
    }
}
