using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using PingerManager.Config;

namespace PingerManager.Constructor
{
    class TcpPing : IProtocolProvider
    {
        public async Task<string> Ping(DateTime dateTime, ConfigEntity configEntity)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    await tcpClient.ConnectAsync(configEntity.Host, configEntity.Port);

                    if (tcpClient.Connected)
                        return dateTime + " " + configEntity.Host + " " + IPStatus.Success;

                    return dateTime + " " + configEntity.Host + " " + IPStatus.BadOption;
                }
                catch (Exception)
                {
                    return dateTime + " " + configEntity.Host + " " + IPStatus.BadOption;
                }
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
