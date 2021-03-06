﻿using System;
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
        private readonly IEventStoreConfiguration _config;

        public EventStoreRepository(IEventStoreConfiguration config)
        {
            _config = config;
        }

        public void PublishEvent(IEvent @event)
        {
            using (var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Parse(_config.IpAddress), _config.Port)))
            {
                connection.Connect();
                var data = @event.ToJsonBytes();
                var metadata = new byte[0];
                connection.AppendToStream("TaskScheduler", ExpectedVersion.Any,
                    new UserCredentials(_config.UserName, _config.Password), new EventData(@event.Id, @event.GetType().Name, true, data, metadata) );
            } 
        }
        
    }
}
