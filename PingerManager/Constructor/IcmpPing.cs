using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using PingerManager.Config;

namespace PingerManager.Constructor
{
    public class IcmpPing : IProtocolProvider
    {
        public async Task<string> Ping(DateTime dateTime, ConfigEntity configEntity)
        {
            using (Ping ping = new Ping())
            {
                var reply = await ping
                    .SendPingAsync(configEntity.Host, (int)TimeSpan.FromSeconds(configEntity.Period).TotalMilliseconds);

                return dateTime + " " + configEntity.Host + " " + reply.Status;
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
