﻿using System;
using System.Text;
using System.Threading.Tasks;
using External.ConfigurationModels;
using Microsoft.Azure.ServiceBus;
using Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace External.ServiceBus
{
    public class ServiceBusClient : IServiceBusClient
    {
        // TODO 4.- Implement Saving data on the Write DB on the Writer microservice
        // TODO 5.- Implement Reading data on the Writer microservice when there is a message asking for it on the topic, and put a message in the topic for the Reader microservice
        // TODO 6.- As the rest of the steps for the other microservices are pretty much the same, put the Writer and External microservices in Docker containers
        // TODO 7 - Kubernetes. Containers deployment to Azure

        private static TopicClient _topicClient;

        public ServiceBusClient(ServiceBusConfig serviceBusConfig)
        {
            _topicClient = new TopicClient(serviceBusConfig.ConnectionString, serviceBusConfig.TopicName);
        }

        public async Task SendMessageAsync(MessageModel originalMessage)
        {
            try
            {
                // Create a new message to send to the topic.
                var messageBody = JsonSerializer.Serialize(originalMessage);
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                message.UserProperties.Add(nameof(Subscriptor), (int)Subscriptor.Writer);

                // Send the message to the topic.
                try
                {
                    await _topicClient.SendAsync(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
            finally
            {
                await _topicClient.CloseAsync();
            }
        }
    }
}
