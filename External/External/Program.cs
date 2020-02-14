using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Json;

namespace External
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IExternalApiClient, ExternalApiClient>()
                .BuildServiceProvider();

            Console.WriteLine("Press any key to start importing data from the External API");
            Console.ReadKey();
            
            var externalApiClient = serviceProvider.GetService<IExternalApiClient>();
            var authors = externalApiClient.GetRandomAuthors();
        }
    }
}
