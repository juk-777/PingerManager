using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using PingerManager.Config;

namespace PingerManager.Constructor
{
    class TcpPing : IProtocolProvider
    {
        public async Task<PingReply> Ping(DateTime pingDate, ConfigEntity configEntity)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    await tcpClient.ConnectAsync(new UriBuilder(configEntity.Host).Host, configEntity.Port);

                    if (tcpClient.Connected)
                        return new PingReply(pingDate, configEntity, IPStatus.Success);

                    return new PingReply(pingDate, configEntity, IPStatus.BadOption);
                }
                catch (Exception)
                {
                    return new PingReply(pingDate, configEntity, IPStatus.BadOption);
                }
            }
        }
    }
}
