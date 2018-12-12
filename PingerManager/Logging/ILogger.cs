using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public interface ILogger
    {
        List<ILoggerProvider> Providers { get; }
        Task Log(LogParams logParams);
    }
}
