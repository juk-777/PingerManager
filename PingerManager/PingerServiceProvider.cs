using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PingerManager.BusinessLogic;
using PingerManager.Config;
using PingerManager.Constructor;
using PingerManager.Logging;

namespace PingerManager
{
    public static class PingerServiceProvider
    {
        public static ServiceProvider ServiceProvider { get; } = new ServiceCollection()
            .AddSingleton<IPingerBusinessLogic, PingerBusinessLogic>()
            .AddSingleton<IConfigReader, ConfigReader>()
            .AddSingleton<IConfigStream, AppSettingsStream>()
            .AddSingleton<IConfigurationBuilder, ConfigurationBuilder>()
            .AddSingleton<IConfigVerifier, ConfigVerifier>()
            .AddSingleton<IPingBuilder, PingBuilder>()
            .AddSingleton<ILoggerFactory, LoggerFactory>()
            .AddTransient<ILoggerProvider, ConsoleLoggerProvider>()
            .AddTransient<ILoggerProvider, TxtLoggerProvider>()
            .AddSingleton<ITxtLoggerWriter, TxtLoggerWriter>()
            .AddSingleton<ILogger, Logger>()
            .AddTransient<IProtocolProvider, IcmpPing>()
            .AddTransient<IProtocolProvider, TcpPing>()
            .AddTransient<IProtocolProvider, HttpPing>()
            .AddSingleton<IProtocolProviderManager, ProtocolProviderManager>()
            .BuildServiceProvider();
    }
}
