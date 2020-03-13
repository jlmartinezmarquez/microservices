using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Writer.ConfigurationModels;
using Writer.ServiceBus;

namespace Writer
{
    class Program
    {
        private static ServiceBusConfig _writerServiceBusConfig;
        private static ServiceBusConfig _readerServiceBusConfig;

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
            _readerServiceBusConfig = new ServiceBusConfig();
            _writerServiceBusConfig = new ServiceBusConfig();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            builder.GetSection("WriterServiceBusConfig").Bind(_writerServiceBusConfig);
            builder.GetSection("ReaderServiceBusConfig").Bind(_readerServiceBusConfig);

            return builder;
        }

        private static ServiceProvider SetDependencyInjection()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IServiceBusClient, ServiceBusClient>()
                .AddSingleton(_writerServiceBusConfig)
                .AddSingleton(_readerServiceBusConfig)
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
