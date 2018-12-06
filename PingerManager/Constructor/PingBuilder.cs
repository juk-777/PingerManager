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
        private ServiceProvider _serviceProvider;
        public event EventHandler<PingReply> Pinged;

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
            _serviceProvider = null;

            switch (configEntity.Protocol)
            {
                case "ICMP":
                    _serviceProvider = new ServiceCollection().AddTransient<IProtocolProvider, IcmpPing>().BuildServiceProvider();
                    break;
                case "TCP":
                    _serviceProvider = new ServiceCollection().AddTransient<IProtocolProvider, TcpPing>().BuildServiceProvider();
                    break;
                case "HTTP":
                    _serviceProvider = new ServiceCollection().AddTransient<IProtocolProvider, HttpPing>().BuildServiceProvider();
                    break;
                default:
                    throw new ArgumentException("Протокол не поддерживается!");
            }

            var pingEntity = new PingEntity
            {
                ConfigEntity = configEntity,
                ProtocolProvider = _serviceProvider.GetService<IProtocolProvider>()
            };

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
                OnPinged(reply);
            }
            catch
            {
                OnPinged(new PingReply(DateTime.Now, pingEntity.ConfigEntity, IPStatus.BadOption));
            }
        }

        protected virtual void OnPinged(PingReply e)
        {
            Pinged?.Invoke(this, e);
        }

        #region IDisposable
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _timers?.ForEach(t => t.Dispose());
                    _serviceProvider?.Dispose();
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
