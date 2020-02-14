using System;
using System.Text;
using System.Threading.Tasks;

namespace External
{
    public static class ServiceBusClient
    {
        // TODO 1.- Keep going on over here following https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-how-to-use-topics-subscriptions
        // TODO 2.- Writer microservice, implement subscribing to the topic and just picking the messages for it
        // TODO 3.- Create the Cosmos DB's with Mongo DB in Azure Portal
        // TODO 4.- Implement Saving data on the Read DB on the Writer microservice
        // TODO 5.- Implement Reading data on the Writer microservice when there is a message asking for it on the topic, and put a message in the topic for the Reader microservice
        // TODO 6.- As the rest of the steps for the other microservices are pretty much the same, put the Writer and External microservices in Docker containers
        // TODO 7 - Kubernetes. Containers deployment to Azure

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    // Create a new message to send to the topic.
                    string messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    // Write the body of the message to the console.
                    Console.WriteLine($"Sending message: {messageBody}");

                    // Send the message to the topic.
                    await topicClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
