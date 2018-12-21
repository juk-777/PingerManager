using System;
using System.Collections.Generic;
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
        private CancellationToken _token;

        public PingBuilder(ILogger logger, IProtocolProviderManager protocolProviderManager)
        {
            _logger = logger;
            _protocolProviderManager = protocolProviderManager;
        }

        public void Start(IEnumerable<ConfigEntity> configEntityList, CancellationToken token)
        {
            _token = token;
            _logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Запускаем Pinger ..."));

            foreach (var configEntity in configEntityList)
            {
                _logger.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Старт: " + configEntity.Host + " - " + configEntity.Protocol));
                BuildPing(configEntity);
            }
        }

        private void BuildPing(ConfigEntity configEntity)
        {
            var pingEntity = new PingEntity { ConfigEntity = configEntity, ProtocolProvider = _protocolProviderManager.GetProvider(configEntity) };

            TimerCallback tm = Ping;
            var period = (int)TimeSpan.FromSeconds(pingEntity.ConfigEntity.Period).TotalMilliseconds;
            _timers.Add(new Timer(tm, pingEntity, 0, period));
        }

        private async void Ping(object obj)
        {
            var pingEntity = (PingEntity)obj;

            try
            {
                _token.ThrowIfCancellationRequested();
                var reply = await pingEntity.ProtocolProvider.PingAsync(DateTime.Now, pingEntity, _logger);
                if (reply != null)
                {
                    await _logger.LogAsync(new LogParams(MessageType.Info, reply.PingDate + " " + reply.PingEntity.ConfigEntity.Host + " " + reply.Status));
                }
            }
            catch (Exception e) when (e is TaskCanceledException || e is OperationCanceledException)
            {
                await _logger.LogAsync(new LogParams(MessageType.Info, DateTime.Now + " " + "Отмена пингера ..."));
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
