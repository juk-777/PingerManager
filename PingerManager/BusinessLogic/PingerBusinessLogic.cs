using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PingerManager.Config;
using PingerManager.Constructor;
using PingerManager.Log;

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

        public async Task StartJob(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;

            List<ConfigEntity> configEntityList = await Task.Run(() => _configReader.ReadConfig(), token);

            if (! await Task.Run(() => _configVerifier.Verify(configEntityList), token))
                throw new ArgumentException("Проверка завершена с ошибкой!");

            _pingBuilder.Pinged += ConsoleLogger.Log;
            _pingBuilder.Pinged += TxtLogger.Log;

            await Task.Run(() => _pingBuilder.Start(configEntityList), token);
        }

        #region IDisposable
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _pingBuilder.Pinged -= ConsoleLogger.Log;
                    _pingBuilder.Pinged -= TxtLogger.Log;
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
