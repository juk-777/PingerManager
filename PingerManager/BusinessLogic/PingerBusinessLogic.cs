﻿using System;
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

        public void StartJob(CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();
                var configEntityList = _configReader.ReadConfig();

                if (! _configVerifier.Verify(configEntityList))
                    throw new ArgumentException("Проверка завершена с ошибкой!");

                _pingBuilder.Start(configEntityList, token);
            }
            catch (TaskCanceledException) { _logger?.LogAsync(new LogParams(MessageType.Info, DateTime.Now + " " + "Отмена операции StartJob до её запуска ...")).GetAwaiter().GetResult(); }
            catch (OperationCanceledException) { _logger?.LogAsync(new LogParams(MessageType.Info, DateTime.Now + " " + "Отмена операции StartJob ...")).GetAwaiter().GetResult(); }
            catch (Exception e)
            {
                _logger.LogAsync(new LogParams(MessageType.Error, DateTime.Now + " " + e.Message)).GetAwaiter().GetResult();
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
