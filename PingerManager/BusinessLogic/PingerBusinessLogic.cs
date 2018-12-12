using System;
using System.Threading;
using System.Threading.Tasks;
using PingerManager.Config;
using PingerManager.Constructor;
using PingerManager.Logging;

namespace PingerManager.BusinessLogic
{
    public class PingerBusinessLogic : IPingerBusinessLogic
    {
        private readonly IConfigReader _configReader;
        private readonly IConfigVerifier _configVerifier;
        private readonly IPingBuilder _pingBuilder;
        private readonly ILogger _logger;

        public PingerBusinessLogic(IConfigReader configReader, IConfigVerifier configVerifier, IPingBuilder pingBuilder, ILogger logger)
        {
            _configReader = configReader;
            _configVerifier = configVerifier;
            _pingBuilder = pingBuilder;
            _logger = logger;
        }

        public async Task StartJob(CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();
                var configEntityList = await Task.Run(() => _configReader.ReadConfig(), token);

                token.ThrowIfCancellationRequested();
                if (!await Task.Run(() => _configVerifier.Verify(configEntityList), token))
                    throw new ArgumentException("Проверка завершена с ошибкой!");

                token.ThrowIfCancellationRequested();
                await Task.Run(() => _pingBuilder.Start(configEntityList, token), token);
            }
            catch (TaskCanceledException)
            {
                _logger?.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Отмена операции StartJob до её запуска ...", MainLogPath.LogPath ?? "log_main.txt"));
            }
            catch (OperationCanceledException)
            {
                _logger?.Log(new LogParams(MessageType.Info, DateTime.Now + " " + "Отмена операции StartJob ...", MainLogPath.LogPath ?? "log_main.txt"));
            }
            catch (Exception e)
            {
                _logger.Log(new LogParams(MessageType.Error, DateTime.Now + " " + e.Message, MainLogPath.LogPath ?? "log_main.txt"));
                throw;
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
                    _pingBuilder.Dispose();
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
