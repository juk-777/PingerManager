using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PingerManager.Config;

namespace PingerManager.BusinessLogic
{
    class PingerBusinessLogic : IPingerBusinessLogic
    {
        private readonly IConfigReader _configReader;
        private readonly IConfigVerifier _configVerifier;

        public PingerBusinessLogic(IConfigReader configReader, IConfigVerifier configVerifier)
        {
            _configReader = configReader;
            _configVerifier = configVerifier;
        }

        public async Task StartJob(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;

            List<ConfigEntity> configEntityList = await Task.Run(() => _configReader.ReadConfig(), token);

            if (!_configVerifier.Verify(configEntityList))
                throw new ArgumentException("Проверка завершена с ошибкой!");


        }

        #region IDisposable
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    
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
