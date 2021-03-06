﻿using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Writer.ConfigurationModels;
using Models;
using MongoDB.Driver;
using System.Text.Json;
using Writer.Config;

namespace Writer.ServiceBus
{
    public class ServiceBusClient : IServiceBusClient
    {
        private static ISubscriptionClient subscriptionClient;
        private readonly WriterCosmosDbConfig writerCosmosDbConfig;
        private readonly IMongoDatabase cosmosDatabase;

        public ServiceBusClient(ServiceBusConfig serviceBusConfig, WriterCosmosDbConfig writerCosmosDbConfig)
        {
            this.writerCosmosDbConfig = writerCosmosDbConfig;
            subscriptionClient = new SubscriptionClient(serviceBusConfig.ConnectionString, serviceBusConfig.TopicName, serviceBusConfig.SubscriptionName);

            var mongoClient = new MongoClient(writerCosmosDbConfig.ConnectionString);
            cosmosDatabase = mongoClient.GetDatabase(writerCosmosDbConfig.DataBaseId);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            if (IsThisMessageForMe(message))
            {
                try
                {
                    var deserialisedMessage = JsonSerializer.Deserialize<MessageModel>(Encoding.UTF8.GetString(message.Body));

                    var authorsCollection = cosmosDatabase.GetCollection<ExternalApiAuthor>(writerCosmosDbConfig.AuthorsCollectionId);
                    var booksCollection = cosmosDatabase.GetCollection<ExternalApiBook>(writerCosmosDbConfig.BooksCollectionId);

                    // Save it on Cosmos DB
                    await authorsCollection.InsertOneAsync(deserialisedMessage.Author, cancellationToken: token).ConfigureAwait(false);
                    await booksCollection.InsertManyAsync(deserialisedMessage.Books, cancellationToken: token).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            //TODO: Not sure whether we need to complete the msg if it wasn't for this subscriptor ....
            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

            // Note: Use the cancellationToken passed as necessary to determine if the subscriptionClient has already been closed.
            // If subscriptionClient has already been closed, you can choose to not call CompleteAsync() or AbandonAsync() etc.
            // to avoid unnecessary exceptions.
        }

        private static bool IsThisMessageForMe(Message message)
        {
            return message.UserProperties.ContainsKey(nameof(Subscriptor)) && (int) message.UserProperties[nameof(Subscriptor)] == (int)Subscriptor.Writer;
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
