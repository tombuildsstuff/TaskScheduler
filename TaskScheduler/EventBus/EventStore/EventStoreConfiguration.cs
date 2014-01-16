namespace TaskScheduler.EventBus.EventStore
{
    public class EventStoreConfiguration : IEventStoreConfiguration
    {
        public EventStoreConfiguration(string ipAddress, int port, string userName, string password)
        {
            Password = password;
            UserName = userName;
            Port = port;
            IpAddress = ipAddress;
        }

        public string IpAddress { get; private set; }
        public int Port { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}