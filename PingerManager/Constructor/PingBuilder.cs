using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PingerManager.Config;

namespace PingerManager.Constructor
{
    public class PingBuilder : IPingBuilder
    {
        private readonly List<Timer> _timers = new List<Timer>();
        private List<PingEntity> _pingEntities = new List<PingEntity>();

        public void Start(List<ConfigEntity> configEntityList)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nЗапускаем Pinger ...");
            Console.ForegroundColor = ConsoleColor.Gray;

            foreach (ConfigEntity configEntity in configEntityList)
            {
                

                Task.Run(() => BuildPing(configEntity));
            }
        }

        private void BuildPing(ConfigEntity configEntity)
        {
            //ServiceProvider serviceProvider = null;
            IProtocolProvider protocolProvider = null;

            if (configEntity.Protocol == "ICMP")
            {
                //serviceProvider = new ServiceCollection()
                //    .AddTransient<IProtocolProvider, IcmpPing>()
                //    .BuildServiceProvider();

                protocolProvider = new IcmpPing();
            }
            if (configEntity.Protocol == "TCP")
            {
                //serviceProvider = new ServiceCollection()
                //    .AddTransient<IProtocolProvider, TcpPing>()
                //    .BuildServiceProvider();

                protocolProvider = new TcpPing();
            }

            var pingEntity = new PingEntity
            {
                ConfigEntity = configEntity,
                //ProtocolProvider = serviceProvider.GetService<IProtocolProvider>()
                ProtocolProvider = protocolProvider
            };

            _pingEntities.Add(pingEntity);

            Ping(pingEntity);

            TimerCallback tm = Ping;
            _timers.Add(new Timer(tm, pingEntity, 0, (int)TimeSpan.FromSeconds(pingEntity.ConfigEntity.Period).TotalMilliseconds));
        }

        private async void Ping(object obj)
        {
            var pingEntity = (PingEntity)obj;

            try
            {
                var reply = await pingEntity.ProtocolProvider.Ping(DateTime.Now, pingEntity.ConfigEntity);
                Console.WriteLine(reply);
            }
            catch
            {
                Console.WriteLine(DateTime.Now + " " + pingEntity.ConfigEntity.Host + " " + IPStatus.BadOption);
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
                    foreach (var timer in _timers)
                    {
                        timer.Dispose();
                    }
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
