using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PingerManager.Config;
using PingerManager.Logging;
using System.Collections.Generic;
using System.Linq;
using Assert = NUnit.Framework.Assert;

namespace PingerManager.Tests
{
    [TestClass]
    public class PingerManagerConfigTest
    {
        private ConfigEntity ConfigEntity { get; set; }
        //private LogParams LogParams { get; set; }
        private MainLogPath MainLogPath { get; set; }
        //private readonly ServiceProvider _serviceProvider;

        //public PingerManagerConfigTest()
        //{
        //    _serviceProvider = PingerServiceProvider.ServiceProvider;
        //}

        [TestInitialize]
        public void TestInitialize()
        {
            ConfigEntity = new ConfigEntity
            {
                Host = "ya.ru",
                Period = 2,
                Protocol = "ICMP",
                Port = 0,
                ValidStatusCode = 0,
                LogPath = "log_1.txt"
            };

            MainLogPath = new MainLogPath();
            MainLogPath.LogPath = "log_main.txt";
        }

        [TestMethod]
        public void ReadStream_Verify()
        {
            var mockConfigStream = new Mock<IConfigStream>();
            mockConfigStream
                .Setup(x => x.ReadStream())
                .Returns(It.IsAny<List<ConfigEntity>>());

            var configReader = new ConfigReader(mockConfigStream.Object);
            configReader.ReadConfig();

            mockConfigStream.VerifyAll();
        }

        [TestMethod]
        public void ReadConfig_IsCorrect()
        {
            List<ConfigEntity> configEntityListExp = new List<ConfigEntity> { ConfigEntity };
            var mockConfigStream = new Mock<IConfigStream>();
            mockConfigStream
                .Setup(x => x.ReadStream())
                .Returns(configEntityListExp);

            var configReader = new ConfigReader(mockConfigStream.Object);
            IList<ConfigEntity> configEntityList = configReader.ReadConfig();

            Assert.AreEqual(configEntityListExp[0].Host, configEntityList[0].Host);
            Assert.AreEqual(configEntityListExp[0].Period, configEntityList[0].Period);
            Assert.AreEqual(configEntityListExp[0].Protocol, configEntityList[0].Protocol);
            Assert.AreEqual(configEntityListExp[0].Port, configEntityList[0].Port);
            Assert.AreEqual(configEntityListExp[0].ValidStatusCode, configEntityList[0].ValidStatusCode);
            Assert.AreEqual(configEntityListExp[0].LogPath, configEntityList[0].LogPath);
        }

        [TestMethod]
        public void Verify_Config_True_Returned()
        {
            List<ConfigEntity> configEntityList = new List<ConfigEntity> { ConfigEntity };
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.Log(It.IsAny<LogParams>()));

            var configVerifier = new ConfigVerifier(mockLogger.Object);
            var result = configVerifier.Verify(configEntityList);

            Assert.True(result);
        }

    }
}
