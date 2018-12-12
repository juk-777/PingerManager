using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public class Logger : ILogger
    {
        public List<ILoggerProvider> Providers { get; }

        public Logger()
        {
            Providers = new List<ILoggerProvider>();
        }

        public async Task Log(LogParams logParams)
        {
            foreach (var provider in Providers)
            {
                await provider.Log(logParams);
            }
        }
    }

    public enum MessageType
    {
        Info = 1,
        Error
    }
}
