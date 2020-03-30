using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Writer.Config;
using Writer.ConfigurationModels;
using Writer.ServiceBus;

namespace Writer
{
    class Program
    {
        private static ServiceBusConfig writerServiceBusConfig;
        private static ServiceBusConfig readerServiceBusConfig;
        private static WriterCosmosDbConfig writerCosmosDbConfig;

        static void Main(string[] args)
        {
            var builder = SetConfiguration();

            var serviceProvider = SetDependencyInjection();

            var serviceBusClient = serviceProvider.GetService<IServiceBusClient>();

            serviceBusClient.RegisterOnMessageHandlerAndReceiveMessages();
            
            while (true)
            {
                try
                {

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Press a Key to finish");
                    Console.ReadKey();
                    break;
                }
            }
        }

        private static IConfigurationRoot SetConfiguration()
        {
            readerServiceBusConfig = new ServiceBusConfig();
            writerServiceBusConfig = new ServiceBusConfig();
            writerCosmosDbConfig = new WriterCosmosDbConfig();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            builder.GetSection("WriterServiceBusConfig").Bind(writerServiceBusConfig);
            builder.GetSection("ReaderServiceBusConfig").Bind(readerServiceBusConfig);
            builder.GetSection("WriterCosmosDbConfig").Bind(writerCosmosDbConfig);

            return builder;
        }

        private static ServiceProvider SetDependencyInjection()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IServiceBusClient, ServiceBusClient>()
                .AddSingleton(writerServiceBusConfig)
                .AddSingleton(readerServiceBusConfig)
                .AddSingleton(writerCosmosDbConfig)
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
