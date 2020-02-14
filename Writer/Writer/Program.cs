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
        private static ServiceBusConfig _serviceBusConfig;

        static void Main(string[] args)
        {
            var builder = SetConfiguration();

            var serviceProvider = SetDependencyInjection();

            var serviceBusClient = serviceProvider.GetService<IServiceBusClient>();

            while (true)
            {
                try
                {
                    serviceBusClient.RegisterOnMessageHandlerAndReceiveMessages();
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
            _serviceBusConfig = new ServiceBusConfig();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            builder.GetSection("ServiceBusConfig").Bind(_serviceBusConfig);

            return builder;
        }

        private static ServiceProvider SetDependencyInjection()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IServiceBusClient, ServiceBusClient>()
                .AddSingleton(_serviceBusConfig)
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
