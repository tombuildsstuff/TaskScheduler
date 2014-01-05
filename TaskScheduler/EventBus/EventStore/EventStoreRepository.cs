using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Utils;
using EventStore.ClientAPI.SystemData;

namespace TaskScheduler.EventBus.EventStore
{
    public class EventStoreRepository : IEventStoreRepository
    {
        public void PublishEvent(IEvent @event)
        {
            using (var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113)))
            {
                connection.Connect();
                var data = Json.ToJsonBytes(@event);
                var metadata = new byte[0];
                connection.AppendToStreamAsync("TaskScheduler", ExpectedVersion.Any,
                    new UserCredentials("admin", "changeit"), new EventData(@event.Id, @event.GetType().Name, true, data, metadata) );
            } 
        }

    }
}
