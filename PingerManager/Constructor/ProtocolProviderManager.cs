using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PingerManager.Config;
using PingerManager.Logging;

namespace PingerManager.Constructor
{
    public class ProtocolProviderManager : IProtocolProviderManager
    {
        private readonly ILogger _logger;
        private readonly ServiceProvider _serviceProvider;

        public ProtocolProviderManager(ILogger logger)
        {
            _logger = logger;
            _serviceProvider = PingerServiceProvider.ServiceProvider;
        }

        public IProtocolProvider GetProvider(ConfigEntity configEntity)
        {
            var protocolProviders = _serviceProvider.GetServices<IProtocolProvider>().ToList();
            try
            {
                switch (configEntity.Protocol)
                {
                    case "ICMP":
                        return protocolProviders.First(o => o.GetType() == typeof(IcmpPing));
                    case "TCP":
                        return protocolProviders.First(o => o.GetType() == typeof(TcpPing));
                    case "HTTP":
                        return protocolProviders.First(o => o.GetType() == typeof(HttpPing));
                    default:
                        throw new ArgumentException(DateTime.Now + " " + "Протокол не поддерживается!");
                }
            }
            catch (ArgumentException e)
            {
                _logger.Log(new LogParams(MessageType.Error, e.Message));
                throw;
            }
        }
    }
}
