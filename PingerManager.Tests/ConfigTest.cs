﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PingerManager.Config;
using PingerManager.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace PingerManager.Tests
{
    [TestClass]
    public class ConfigTest
    {
        private ConfigEntity ConfigEntity { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            ConfigEntity = new ConfigEntity
            {
                Host = "ya.ru",
                Period = 2,
                Protocol = Protocol.Icmp,
                Port = 0,
                ValidStatusCode = 0
            };
        }

        [TestMethod]
        public void ReadStream_Verify()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigStream = new Mock<IConfigStream>();
            mockConfigStream
                .Setup(x => x.ReadStream(mockConfiguration.Object))
                .Returns(It.IsAny<List<ConfigEntity>>());

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfigReader>(p => new ConfigReader(mockConfigStream.Object))
                .BuildServiceProvider();

            var configReader = serviceProvider.GetService<IConfigReader>();
            configReader.ReadConfig(mockConfiguration.Object);

            mockConfigStream.VerifyAll();
        }

        [TestMethod]
        public void ReadConfig_IsCorrect()
        {
            var configEntityListExp = new List<ConfigEntity> { ConfigEntity };
            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigStream = new Mock<IConfigStream>();
            mockConfigStream
                .Setup(x => x.ReadStream(mockConfiguration.Object))
                .Returns(configEntityListExp);

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfigReader>(p => new ConfigReader(mockConfigStream.Object))
                .BuildServiceProvider();

            var configReader = serviceProvider.GetService<IConfigReader>();
            IList<ConfigEntity> configEntityList = configReader.ReadConfig(mockConfiguration.Object);

            Assert.AreEqual(configEntityListExp[0].Host, configEntityList[0].Host);
            Assert.AreEqual(configEntityListExp[0].Period, configEntityList[0].Period);
            Assert.AreEqual(configEntityListExp[0].Protocol, configEntityList[0].Protocol);
            Assert.AreEqual(configEntityListExp[0].Port, configEntityList[0].Port);
            Assert.AreEqual(configEntityListExp[0].ValidStatusCode, configEntityList[0].ValidStatusCode);
        }

        [TestMethod]
        public void Verify_Config_True_Returned()
        {
            var configEntityList = new List<ConfigEntity> { ConfigEntity };
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.LogAsync(It.IsAny<LogParams>())).Returns(Task.CompletedTask);

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfigVerifier>(p => new ConfigVerifier(mockLogger.Object))
                .BuildServiceProvider();

            var configVerifier = serviceProvider.GetService<IConfigVerifier>();
            var result = configVerifier.Verify(configEntityList);

            Assert.True(result);
        }

        [TestMethod]
        public void Verify_Config_Host_Is_Null()
        {
            ConfigEntity.Host = null;
            var configEntityList = new List<ConfigEntity> { ConfigEntity };
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.LogAsync(It.IsAny<LogParams>())).Returns(Task.CompletedTask);

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfigVerifier>(p => new ConfigVerifier(mockLogger.Object))
                .BuildServiceProvider();

            var configVerifier = serviceProvider.GetService<IConfigVerifier>();
            var result = configVerifier.Verify(configEntityList);

            Assert.False(result);
        }

        [TestMethod]
        public void Verify_Config_Not_Correct_Host()
        {
            ConfigEntity.Host = "ya@ru";
            var configEntityList = new List<ConfigEntity> { ConfigEntity };
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.LogAsync(It.IsAny<LogParams>())).Returns(Task.CompletedTask);

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfigVerifier>(p => new ConfigVerifier(mockLogger.Object))
                .BuildServiceProvider();

            var configVerifier = serviceProvider.GetService<IConfigVerifier>();
            var result = configVerifier.Verify(configEntityList);

            Assert.False(result);
        }

        [TestMethod]
        public void Verify_Config_Not_Correct_Period()
        {
            ConfigEntity.Period = 0;
            var configEntityList = new List<ConfigEntity> { ConfigEntity };
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.LogAsync(It.IsAny<LogParams>())).Returns(Task.CompletedTask);

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfigVerifier>(p => new ConfigVerifier(mockLogger.Object))
                .BuildServiceProvider();

            var configVerifier = serviceProvider.GetService<IConfigVerifier>();
            var result = configVerifier.Verify(configEntityList);

            Assert.False(result);
        }

        [TestMethod]
        public void Verify_Config_Not_Correct_Port()
        {
            ConfigEntity.Protocol = Protocol.Tcp;
            ConfigEntity.Port = -1;
            var configEntityList = new List<ConfigEntity> { ConfigEntity };
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.LogAsync(It.IsAny<LogParams>())).Returns(Task.CompletedTask);

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfigVerifier>(p => new ConfigVerifier(mockLogger.Object))
                .BuildServiceProvider();

            var configVerifier = serviceProvider.GetService<IConfigVerifier>();
            var result = configVerifier.Verify(configEntityList);

            Assert.False(result);
        }

        [TestMethod]
        public void Verify_Config_Not_Correct_ValidStatusCode()
        {
            ConfigEntity.Protocol = Protocol.Http;
            ConfigEntity.ValidStatusCode = -1;
            var configEntityList = new List<ConfigEntity> { ConfigEntity };
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.LogAsync(It.IsAny<LogParams>())).Returns(Task.CompletedTask);

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfigVerifier>(p => new ConfigVerifier(mockLogger.Object))
                .BuildServiceProvider();

            var configVerifier = serviceProvider.GetService<IConfigVerifier>();
            var result = configVerifier.Verify(configEntityList);

            Assert.False(result);
        }
    }
}
