using System;
using System.Collections.Generic;
using PingerManager.Config;

namespace PingerManager.BusinessLogic
{
    class PingerBusinessLogic : IPingerBusinessLogic
    {
        private readonly IConfigReader _configReader;

        public PingerBusinessLogic(IConfigReader configReader)
        {
            _configReader = configReader;
        }

        public void StartJob()
        {
            Console.WriteLine("\nНачинаю работу ...");

            Console.WriteLine("\nСчитывание конфигурации ...");
            List<ConfigEntity> configEntityList = _configReader.ReadConfig();

            foreach (ConfigEntity configEntity in configEntityList)
            {
                Console.WriteLine($"\nHost: {configEntity.Host} \nPeriod: {configEntity.Period} \nProtocol: {configEntity.Protocol} \nPort: {configEntity.Port} \nValidStatusCode: {configEntity.ValidStatusCode}");
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
