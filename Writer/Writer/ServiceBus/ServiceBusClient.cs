using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.ServiceBus;
using Writer.ConfigurationModels;
using Models;
using Writer.Config;

namespace Writer.ServiceBus
{
    public class ServiceBusClient : IServiceBusClient
    {
        private static ISubscriptionClient _subscriptionClient;
        private readonly CosmosClient _cosmosClient;

        public ServiceBusClient(ServiceBusConfig serviceBusConfig, WriterCosmosDbConfig writerCosmosDbConfig)
        {
            _subscriptionClient = new SubscriptionClient(serviceBusConfig.ConnectionString, serviceBusConfig.TopicName, serviceBusConfig.SubscriptionName);
            _cosmosClient = new CosmosClient(writerCosmosDbConfig.ConnectionString);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            if (IsThisMessageForMe(message)) 
            {
                // TODO: Save it on Cosmos DB
                
            }
            
            // Complete the message so that it is not received again.
            // This can be done only if the subscriptionClient is created in ReceiveMode.PeekLock mode (which is the default).
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

            // Note: Use the cancellationToken passed as necessary to determine if the subscriptionClient has already been closed.
            // If subscriptionClient has already been closed, you can choose to not call CompleteAsync() or AbandonAsync() etc.
            // to avoid unnecessary exceptions.
        }

        private static bool IsThisMessageForMe(Message message)
        {
            return message.UserProperties.ContainsKey(nameof(Subscriptor)) &&
                          (int) message.UserProperties[nameof(Subscriptor)] == (int)Subscriptor.Writer;
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
