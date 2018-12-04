using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.FileExtensions;
//using Microsoft.Extensions.Configuration.Json;

namespace PingerManager.Config
{
    class AppSettingsStream : IConfigStream
    {
        public List<ConfigEntity> ReadStream()
        {
            List<ConfigEntity> configEntityList;

            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", false);
            //var configuration = builder.Build();

            //var aSection = new ConfigEntity();
            //var section = configuration.GetSection("ConfigEntity");
            //aSection = section.Get<ConfigEntity>();
            //Console.WriteLine($"Host: {aSection.Host}, Period: {aSection.Period}");
            //Console.WriteLine(configuration.GetConnectionString("ConfigEntity"));

            

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<ConfigEntity>));
            try
            {
                using (FileStream fs = new FileStream("appsettings.json", FileMode.OpenOrCreate))
                {
                    configEntityList = (List<ConfigEntity>)jsonFormatter.ReadObject(fs);
                    Console.WriteLine("Объекты десериализованы");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            Console.WriteLine("Объекты десериализованы");


            //Thread.Sleep(2000);

            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json");

            //IConfiguration config = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", true, true)
            //    .Build();


            //Console.WriteLine($" { config["Host"] }");


            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json");

            //var config = builder.Build();

            //var appConfig = config.GetSection("application").Get<Application>();

            //Console.WriteLine("Application Name : {appConfig.Name}");

            //configEntityList.Add(aSection);
            //Console.WriteLine("Конфигурация считана!");
            return configEntityList;
        }
    }
}
