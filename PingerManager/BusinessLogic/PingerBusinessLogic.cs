using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PingerManager.Config;
using PingerManager.Constructor;
using PingerManager.Logging;

namespace PingerManager.BusinessLogic
{
    class PingerBusinessLogic : IPingerBusinessLogic
    {
        private readonly IConfigReader _configReader;
        private readonly IConfigVerifier _configVerifier;
        private readonly IPingBuilder _pingBuilder;
        private readonly ILoggerFactory _loggerFactory;

        public PingerBusinessLogic(IConfigReader configReader, IConfigVerifier configVerifier, IPingBuilder pingBuilder, ILoggerFactory loggerFactory)
        {
            _configReader = configReader;
            _configVerifier = configVerifier;
            _pingBuilder = pingBuilder;
            _loggerFactory = loggerFactory;
        }

        public async Task StartJob(CancellationToken token)
        {
            var logger = _loggerFactory.Logger;

            try
            {
                if (token.IsCancellationRequested)
                    return;

                List<ConfigEntity> configEntityList = await Task.Run(() => _configReader.ReadConfig(), token);

                if (!await Task.Run(() => _configVerifier.Verify(configEntityList), token))
                    throw new ArgumentException(DateTime.Now + " " + "Проверка завершена с ошибкой!");

                await Task.Run(() => _pingBuilder.Start(configEntityList), token);
            }
            catch (Exception e)
            {
                logger.Log(new LogParams(MessageType.Error, e.Message, MainLogPath.LogPath ?? "log_main.txt"));
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
