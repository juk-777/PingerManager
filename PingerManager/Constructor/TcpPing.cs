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
                    await Task.WhenAny(
                        tcpClient.ConnectAsync(new UriBuilder(pingEntity.ConfigEntity.Host).Host, pingEntity.ConfigEntity.Port),
                        Task.Delay(2000));

                    return !tcpClient.Connected ? new PingReply(pingDate, pingEntity, IPStatus.BadOption) : new PingReply(pingDate, pingEntity, IPStatus.Success);
                }
            }
            catch (Exception e)
            {
                logger.Log(new LogParams(MessageType.Warning, e.Message));
                return new PingReply(pingDate, pingEntity, IPStatus.BadOption);
            }
        }
    }
}
