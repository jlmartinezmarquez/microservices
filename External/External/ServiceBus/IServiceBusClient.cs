using System.Threading.Tasks;
using Models;

namespace External.ServiceBus
{
    public interface IServiceBusClient
    {
        Task SendMessageAsync(MessageModel originalMessage);
    }
}