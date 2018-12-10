using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using PingerManager.Config;
using PingerManager.Logging;

namespace PingerManager.Constructor
{
    public class PingBuilder : IPingBuilder
    {
        private readonly ILogger _logger;
        private readonly IProtocolProviderManager _protocolProviderManager;
        private readonly List<Timer> _timers = new List<Timer>();
        

        public PingBuilder(ILogger logger, IProtocolProviderManager protocolProviderManager)
        {
            _logger = logger;
            _protocolProviderManager = protocolProviderManager;
        }

        public void Start(List<ConfigEntity> configEntityList)
        {
            _logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Запускаем Pinger ...", MainLogPath.LogPath));

            foreach (ConfigEntity configEntity in configEntityList)
            {
                _logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Старт: " + configEntity.Host + " - " + configEntity.Protocol, MainLogPath.LogPath));
                Task.Run(() => BuildPing(configEntity));
            }
        }

        public void BuildPing(ConfigEntity configEntity)
        {
            var pingEntity = new PingEntity { ConfigEntity = configEntity, ProtocolProvider = _protocolProviderManager.GetProvider(configEntity) };
            Ping(pingEntity);

            TimerCallback tm = Ping;
            _timers.Add(new Timer(tm, pingEntity, 0, (int)TimeSpan.FromSeconds(pingEntity.ConfigEntity.Period).TotalMilliseconds));
        }

        private async void Ping(object obj)
        {
            var pingEntity = (PingEntity)obj;

            try
            {
                var reply = await pingEntity.ProtocolProvider.Ping(DateTime.Now, pingEntity);
                _logger.Log(new LogParams(MessageType.Info, reply.PingDate + " " + reply.PingEntity.ConfigEntity.Host + " " + reply.Status, reply.PingEntity.ConfigEntity.LogPath));
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
