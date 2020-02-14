namespace Writer.ServiceBus
{
    public interface IServiceBusClient
    {
        void RegisterOnMessageHandlerAndReceiveMessages();
    }
}