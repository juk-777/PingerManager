﻿using System;
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

        public PingerBusinessLogic(IConfigReader configReader, IConfigVerifier configVerifier, IPingBuilder pingBuilder)
        {
            _configReader = configReader;
            _configVerifier = configVerifier;
            _pingBuilder = pingBuilder;
        }

        public async Task StartJob(CancellationToken token, ILogger logger)
        {
            try
            {
                logger.Log(MessageType.Info, "Запуск работы ...");

                if (token.IsCancellationRequested)
                    return;

                List<ConfigEntity> configEntityList = await Task.Run(() => _configReader.ReadConfig(logger), token);

                if (!await Task.Run(() => _configVerifier.Verify(configEntityList, logger), token))
                    throw new ArgumentException("Проверка завершена с ошибкой!");

                await Task.Run(() => _pingBuilder.Start(configEntityList, logger), token);
            }
            catch (Exception e)
            {
                logger.Log(MessageType.Error, e.Message);
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
