using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using External.ConfigurationModels;
using External.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace External
{
    class Program
    {
        private static ServiceBusConfig _serviceBusConfig;

        static async Task Main(string[] args)
        {
            SetConfiguration();

            var serviceProvider = SetDependencyInjection();

            //Console.WriteLine("Press any key to start importing data from the External API");
            //Console.ReadKey();
            
            var externalApiClient = serviceProvider.GetService<IExternalApiClient>();
            var authors = externalApiClient.GetRandomAuthors();

            var serviceBusClient = serviceProvider.GetService<IServiceBusClient>();

            await SendData(authors, serviceBusClient);
        }

        private static async Task SendData(List<ExternalApiAuthor> authors, IServiceBusClient serviceBusClient)
        {
            foreach (var author in authors)
            {
                var message = new MessageModel
                {
                    Author = author,
                    Operation = ReadOrWrite.Write,
                };

                await serviceBusClient.SendMessageAsync(message);
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
                .AddSingleton<IExternalApiClient, ExternalApiClient>()
                .AddSingleton<IServiceBusClient, ServiceBusClient>()
                .AddSingleton(_serviceBusConfig)
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
