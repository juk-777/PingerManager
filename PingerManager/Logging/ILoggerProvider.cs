using System;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public interface ILoggerProvider : IDisposable
    {
        Task LogAsync(LogParams logParams);
    }
}
