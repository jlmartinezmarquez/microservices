using System.Threading.Tasks;

namespace External.ServiceBus
{
    public interface IServiceBusClient
    {
        Task SendMessagesAsync(int numberOfMessagesToSend);
    }
}