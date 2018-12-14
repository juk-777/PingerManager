using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PingerManager.Constructor
{
    public class TcpPing : IProtocolProvider
    {
        public async Task<PingReply> Ping(DateTime pingDate, PingEntity pingEntity)
        {
            using (var tcpClient = new TcpClient())
            {
                try
                {
                    await tcpClient.ConnectAsync(new UriBuilder(pingEntity.ConfigEntity.Host).Host, pingEntity.ConfigEntity.Port);

                    return tcpClient.Connected ? new PingReply(pingDate, pingEntity, IPStatus.Success) : new PingReply(pingDate, pingEntity, IPStatus.BadOption);
                }
                catch (Exception)
                {
                    return new PingReply(pingDate, pingEntity, IPStatus.BadOption);
                }
            }
        }
    }
}
