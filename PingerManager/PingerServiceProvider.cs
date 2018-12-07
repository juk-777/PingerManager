using Microsoft.Extensions.DependencyInjection;
using PingerManager.BusinessLogic;
using PingerManager.Config;
using PingerManager.Constructor;

namespace PingerManager
{
    public static class PingerServiceProvider
    {
        public static ServiceProvider ServiceProvider { get; } = new ServiceCollection()
            .AddSingleton<IPingerBusinessLogic, PingerBusinessLogic>()
            .AddSingleton<IConfigReader, ConfigReader>()
            .AddSingleton<IConfigStream, AppSettingsStream>()
            .AddSingleton<IConfigVerifier, ConfigVerifier>()
            .AddSingleton<IPingBuilder, PingBuilder>()
            .AddTransient<IProtocolProvider, IcmpPing>()
            .AddTransient<IProtocolProvider, TcpPing>()
            .AddTransient<IProtocolProvider, HttpPing>()
            .BuildServiceProvider();
    }
}
