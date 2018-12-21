using PingerManager.Logging;
using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PingerManager.Constructor
{
    public class TcpPing : IProtocolProvider
    {
        public async Task<PingReply> PingAsync(DateTime pingDate, PingEntity pingEntity, ILogger logger)
        {
            try
            {
                using (var tcpClient = new TcpClient())
                {
                    var host = new UriBuilder(pingEntity.ConfigEntity.Host).Host;
                    var port = pingEntity.ConfigEntity.Port;
                    await Task.WhenAny(tcpClient.ConnectAsync(host, port), Task.Delay(2000));

                    return !tcpClient.Connected ? new PingReply(pingDate, pingEntity, IPStatus.BadOption) : new PingReply(pingDate, pingEntity, IPStatus.Success);
                }
            }
            catch (Exception e)
            {
                logger.Log(new LogParams(MessageType.Warning, e.Message));
                return null;
            }
        }
    }
}
