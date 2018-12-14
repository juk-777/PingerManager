﻿using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace PingerManager.Constructor
{
    public class HttpPing : IProtocolProvider
    {
        public async Task<PingReply> PingAsync(DateTime pingDate, PingEntity pingEntity)
        {
            var request = (HttpWebRequest)WebRequest.Create(new UriBuilder(pingEntity.ConfigEntity.Host).Uri);
            request.Timeout = (int)TimeSpan.FromSeconds(pingEntity.ConfigEntity.Period).TotalMilliseconds;

            try
            {
                using (var reply = (HttpWebResponse)await request.GetResponseAsync())
                {
                    return reply.StatusCode == (HttpStatusCode)pingEntity.ConfigEntity.ValidStatusCode ? new PingReply(pingDate, pingEntity, IPStatus.Success) : new PingReply(pingDate, pingEntity, IPStatus.BadOption);
                }
            }
            catch (WebException)
            {
                return new PingReply(pingDate, pingEntity, IPStatus.BadOption);
            }
        }
    }
}
