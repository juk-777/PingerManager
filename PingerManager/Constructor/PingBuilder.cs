using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PingerManager.Config;
using PingerManager.Logging;

namespace PingerManager.Constructor
{
    public class PingBuilder : IPingBuilder
    {
        private readonly ILoggerFactory _loggerFactory;
        private ILogger _logger;
        private readonly List<Timer> _timers = new List<Timer>();
        private readonly ServiceProvider _serviceProvider;

        public PingBuilder(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _serviceProvider = PingerServiceProvider.ServiceProvider;
        }

        public void Start(List<ConfigEntity> configEntityList)
        {
            _logger = _loggerFactory.Logger;
            _logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Запускаем Pinger ...", MainLogPath.LogPath));

            foreach (ConfigEntity configEntity in configEntityList)
            {
                _logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Старт: " + configEntity.Host + " - " + configEntity.Protocol, MainLogPath.LogPath));
                Task.Run(() => BuildPing(configEntity));
            }
        }

        private void BuildPing(ConfigEntity configEntity)
        {
            var protocolProviders = _serviceProvider.GetServices<IProtocolProvider>().ToList();
            PingEntity pingEntity;

            try
            {
                switch (configEntity.Protocol)
                {
                    case "ICMP":
                        pingEntity = new PingEntity { ConfigEntity = configEntity, ProtocolProvider = protocolProviders.First(o => o.GetType() == typeof(IcmpPing)) };
                        break;
                    case "TCP":
                        pingEntity = new PingEntity { ConfigEntity = configEntity, ProtocolProvider = protocolProviders.First(o => o.GetType() == typeof(TcpPing)) };
                        break;
                    case "HTTP":
                        pingEntity = new PingEntity { ConfigEntity = configEntity, ProtocolProvider = protocolProviders.First(o => o.GetType() == typeof(HttpPing)) };
                        break;
                    default:
                        throw new ArgumentException(DateTime.Now + " " + "Протокол не поддерживается!");
                }
            }
            catch (ArgumentException e)
            {
                _logger.Log(new LogParams(MessageType.Error, e.Message, MainLogPath.LogPath));
                throw;
            }

            Ping(pingEntity);

            TimerCallback tm = Ping;
            _timers.Add(new Timer(tm, pingEntity, 0, (int)TimeSpan.FromSeconds(pingEntity.ConfigEntity.Period).TotalMilliseconds));
        }

        private async void Ping(object obj)
        {
            var pingEntity = (PingEntity)obj;

            try
            {
                var reply = await pingEntity.ProtocolProvider.Ping(DateTime.Now, pingEntity.ConfigEntity);
                _logger.Log(new LogParams(MessageType.Info, reply.PingDate + " " + reply.ConfigEntity.Host + " " + reply.Status, reply.ConfigEntity.LogPath));
            }
            catch
            {
                _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + pingEntity.ConfigEntity.Host + " " + IPStatus.BadOption, pingEntity.ConfigEntity.LogPath));
            }
        }

        #region IDisposable
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _timers?.ForEach(t => t.Dispose());
                    _serviceProvider?.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
