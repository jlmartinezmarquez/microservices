using System;
using System.IO;
using External.ConfigurationModels;
using External.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace External
{
    class Program
    {
        private static ServiceBusConfig _serviceBusConfig;

        static void Main(string[] args)
        {
            var builder = SetConfiguration();

            var serviceProvider = SetDependencyInjection();

            //Console.WriteLine("Press any key to start importing data from the External API");
            //Console.ReadKey();
            
            var externalApiClient = serviceProvider.GetService<IExternalApiClient>();
            var authors = externalApiClient.GetRandomAuthors();

            var serviceBusClient = serviceProvider.GetService<IServiceBusClient>();
            serviceBusClient.SendMessagesAsync(1);
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
                .AddSingleton<IExternalApiClient, ExternalApiClient>()
                .AddSingleton<IServiceBusClient, ServiceBusClient>()
                .AddSingleton(_serviceBusConfig)
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
